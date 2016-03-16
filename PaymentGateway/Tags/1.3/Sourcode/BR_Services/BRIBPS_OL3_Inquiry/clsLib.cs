using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;

namespace BRIBPS_OL3_Inquiry
{
    public class Lib
    {

        public static string vHost_lib;//Thu vien de lay du lieu
        public static string vRMHIST;//Thu vien de lay du lieu cua bang RMHIST
        public static string vRMHIST_DATE;//Ngay thuc hien lay trong bang HIST
        public static string vIBM_QUERY;//Kiem tra xem lay ngay hien tai hay qua khu


        private static string strConn = "";
        private static int icount = 0;
        private const string LOG_FOLDER = @"C:\ServiceLog";
        private const string LOG_FILENAME = "BRVCBInquiry_Service.log";        
        private static string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";

        #region Ham GHI LOG ra mot file log       

        public static bool WriteLogFile(int log_Level, string strContent)
        {
            string DirPath = LOG_FOLDER;
            string FileName = LOG_FILENAME;
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    using (StreamWriter sw = File.AppendText(FullName))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + strContent);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(FullName))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + strContent);
                        sw.Close();

                    }
                }
                return true;
            }
            catch
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }
        #endregion

        #region  void  WriteLogDB(int LogLevel, string Content)
        public static void WriteLogDB(int LogLevel, string Content, long lQueryID)
        {
            try
            {
                // Mo ket noi database            
                OracleTransaction oraTransaction;
                OracleConnection oraConnection = OpenConnect();
                oraTransaction = oraConnection.BeginTransaction();
                try
                {

                    OracleParameter[] parameters = new OracleParameter[]
                {
                    new OracleParameter("pLogDate",OracleType.DateTime),
                    new OracleParameter("pQUERY_ID", OracleType.Number,20),
                    new OracleParameter("pJOB_NAME",OracleType.VarChar,50),
                    new OracleParameter("pDESCRIPTIONS", OracleType.VarChar,1000 ),
                    new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,20),
                    new OracleParameter("pSTATUS",OracleType.Number,2),
                };

                    parameters[0].Value = DateTime.Now;
                    parameters[1].Value = lQueryID;
                    parameters[2].Value = "BRVCBInquiry";
                    parameters[3].Value = Content;
                    parameters[4].Value = "SIBS-VCB";
                    parameters[5].Value = LogLevel;
                    string strSQL = "insert into RM_svr_log (Log_Date,QUERY_ID,STATUS,DESCRIPTIONS,SERVICE,MSG_DIRECTION)"
                                  + " values (:pLogDate,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS,:pJOB_NAME,:pMSG_DIRECTION)";

                    OracleCommand oraCommand = new OracleCommand(strSQL, oraConnection, oraTransaction);
                    oraCommand.Parameters.AddRange(parameters);
                    oraCommand.CommandType = CommandType.Text;
                    oraCommand.ExecuteNonQuery();
                    oraTransaction.Commit();
                    // Huy ket noi sau khi hoan thanh
                    oraConnection.Dispose();
                    oraCommand.Dispose();
                }
                catch 
                {                    
                    oraTransaction.Rollback();
                }
            }
            catch { };
        }
        #endregion

        #region DataTable ExcuteDataTable(string cmdText)

        public static DataTable ExcuteDataTableODBC(string cmdText)
        {
            DataTable tblValue = new DataTable();
            OdbcDataAdapter da = new OdbcDataAdapter();
            OdbcConnection DB2Connection = new OdbcConnection();
            DB2Connection = OpenODBCConnect();//Goi ham ket noi dung ODBC
            OdbcCommand oraCommand = new OdbcCommand(cmdText, DB2Connection);
            oraCommand.CommandType = CommandType.Text;

            try
            {
                da = new OdbcDataAdapter(oraCommand);
                da.Fill(tblValue);                
                da.Dispose();
                oraCommand.Dispose();
                oraCommand.Dispose();
            }
            catch(Exception ex)
            {
                tblValue = null;
                da.Dispose();
                oraCommand.Dispose();
                oraCommand.Dispose();                
            }
            return tblValue;
        }
        #endregion

        #region DataTable ExcuteDataTable(string cmdText)

        public static DataTable ExcuteDataTableORA(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable tblValue = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter();
            // Khoi tao ket noi moi
            OracleConnection oraConnection = OpenConnect();

            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                da = new OracleDataAdapter(oraCommand);
                da.Fill(tblValue);
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();

            }
            catch
            {
                tblValue = null;
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();
            }
            return tblValue;
        }
        #endregion

       
        public static OdbcConnection OpenODBCConnect()
        {          
            strConn = getConnString("ODBC");
            OdbcConnection connection = new OdbcConnection(strConn);  
            try
            {
                connection.Open();
                icount = 0;
                return connection;
            }
            catch(Exception ex)
            {
                //icount++;
                //if (icount < 4) 
                //{
                //    strConn = getConnString("ODBC");
                //    OpenConnect();
                //}
                //icount = 0;
                WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
                return null;  
            }            
        }
       
        public static OracleConnection OpenConnect()
        {
            strConn = getConnString("DBORA");
            OracleConnection connection = new OracleConnection(strConn);
            try
            {
                connection.Open();
                icount = 0;
                return connection;

            }
            catch(Exception ex)
            {
                icount++;
                if (icount < 4)
                {
                    strConn = getConnString("DBORA");
                    OpenConnect();
                }
                icount = 0;

                WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
                return null;
            }
        }

        public static string getConnString(string strKeyCon)
        {
            string strConnect = "";
            try
            {
                if (strKeyCon.Trim() == "ODBC")// Connect truc tiep vao DB2 host
                {                    
                    string strHOST = getKeyConfig("IP_HOST");              
                    string strUSER = getKeyConfig("USER_HOST");
                    string strPASS = Decrypt(getKeyConfig("PASS_HOST"));   
                    string strDSN = getKeyConfig("DSN_HOST");  
                    strConnect = @"DSN=" + strDSN + ";UID=" + strUSER + ";PWD=" + strPASS + "";
                }
                else if (strKeyCon.Trim() == "DBORA")//Connect oracle gateway
                {       
                    vRMHIST = getKeyConfig("RMHIST");
                    vRMHIST_DATE = getKeyConfig("RMHIST_DATE");
                    vIBM_QUERY = getKeyConfig("IBM_QUERY");
                    vHost_lib = getKeyConfig("LIB_HOST");
                    string strDTABASE = getKeyConfig("DatabaseName");
                    string strUSERNAME = getKeyConfig("Username");
                    string strPASSWORD = Decrypt(getKeyConfig("Password"));
                    strConnect = "Data Source=" + strDTABASE + "; User Id=" + strUSERNAME + " ; Password=" + strPASSWORD + " ; Integrated Security=No;";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File config failed." + ex.Message);
            }
            return strConnect;
        }
               
        public static string getKeyConfig(string key)
        {
            string result = "";
            // load config document 
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(strConfigFilePath);
            }
            catch (System.IO.FileNotFoundException ex)
            {

                throw new Exception("Can not File Service config : " + ex.Message);
            }
            // retrieve ServicesConfig node
            XmlNode node = doc.SelectSingleNode("//DatabaseConfig");
            if (node == null)
            {
                throw new InvalidOperationException("Can not File Service config .");
            }
            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem == null)
                {
                    // add value for key
                    throw new Exception(" File Service config Failed.");
                }
                else
                {
                    // key was not found so create the 'add' element
                    // and set it's key/value attributes
                    result = elem.Attributes.GetNamedItem("value").Value;
                }
            }
            catch
            {


            }
            return result;
        }
        
        public static string Decrypt(string toDecrypt)
        {
            string key = "FPTSecurity2008"; //Encrypt Key 
            string result = "";
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                result = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                throw new Exception("Can't Decode from Input");
                //MsgBox.Show("Can't decode User and Password in Config File", "Message error");
            }
            return result;
        } 
        
        public static string GetSysdate()
        {
            string strSQL_Sysdate = "Select sysdate from dual ";            
            string strSysdate = "";
            try
            {
                
                DataTable _dt = new DataTable();
                _dt = Lib.ExcuteDataTableORA(strSQL_Sysdate);
                if (_dt != null)
                {
                    strSysdate = _dt.Rows[0]["sysdate"].ToString();
                }
                else
                {
                    Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1);
                }
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1);
            }
            return strSysdate;
        }

        public static DataTable GETTAD()
        {
            string strSQL = "select SIBS_BANK_CODE,GW_BANK_CODE from tad";
           
            try
            {

                DataTable _dt = new DataTable();
                _dt = Lib.ExcuteDataTableORA(strSQL);
                return _dt;
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1);
                return null;
            }
            
        }

        public static int Check_key(string Keycode)
        {
            string strSQL = "Select MSG_ID from VCB_INQUIRY_HOST where Trim(CHECK_KEY) ='" + Keycode.Trim() + "'  and TRANSDATE = to_char(sysdate,'DDMMYYYY')";            
            try
            {
                DataTable _dt = new DataTable();
                _dt = Lib.ExcuteDataTableORA(strSQL);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0) { return -1; }
                    else { return 1; }
                }
                else
                {
                    Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1); return -1;                    
                }
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1); return -1;
            }           
        }

        public static int Sum_message()
        {
            string strSQL = "Select count(MSG_ID)MSG_ID from VCB_INQUIRY_HOST where  TRANSDATE = to_char(sysdate,'DDMMYYYY')";
            int iCount = 0;
            try
            {
                DataTable _dt = new DataTable();
                _dt = Lib.ExcuteDataTableORA(strSQL);
                if (_dt != null)
                {
                    return iCount = Convert.ToInt32(_dt.Rows[0]["MSG_ID"].ToString());
                }
                else
                {
                    Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1); return iCount = 0;
                }
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect database failed", 1); return iCount = 0;
            }
        }

        public static int ExecuteNonQuery(string StoreName, CommandType cmdType, OracleParameter[] parameters)
        {
            int iReturn = 0;            
            OracleConnection oraConnection = OpenConnect();
            if (oraConnection == null)
                return -1;
            OracleTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            OracleCommand oraCommand = new OracleCommand(StoreName, oraConnection, oraTransaction);
            oraCommand.CommandType = cmdType;

            if (parameters != null)
                oraCommand.Parameters.AddRange(parameters);
            try
            {
                iReturn = oraCommand.ExecuteNonQuery();
                oraTransaction.Commit();
                // Sau khi xu li thanh cong
                oraConnection.Close();
                oraCommand.Dispose();
                oraTransaction.Dispose();
                oraConnection.Dispose();

            }
            catch (Exception ex)
            {
                oraTransaction.Rollback(); // neu xay ra loi thi RollBack
                oraConnection.Close();
                oraCommand.Dispose();
                oraTransaction.Dispose();
                oraConnection.Dispose();
                throw new Exception("OracleTransaction xay ra loi, ExecuteNonQuery Failed" + ex.Message);
            }
            return iReturn;
        }
    }
}
