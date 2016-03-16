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
using System.Data.SqlClient;

namespace BRIBPS_TR_Inquiry
{
    public class Lib
    {
        

        private static string strConn = "";
        private static int icount = 0;
        private const string LOG_FOLDER = @"C:\ServiceLog";
        private const string LOG_FILENAME = "SWIFTTRInquiry_Service.log";        
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
                OracleConnection oraConnection = OpenORACLEConnect();
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

        

        #region DataTable Select_ReturnDataTable(string cmdText)
        public static DataTable Select_ReturnDataTable(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable dtb = new DataTable();
            OracleDataAdapter da;

            // Khoi tao ket noi moi
            OracleConnection oraConnection = OpenORACLEConnect();

            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                da = new OracleDataAdapter(oraCommand);
                da.Fill(dtb);

                // Huy ket noi khi Select hoan thanh
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();

            }
            catch (Exception ex)
            {
                dtb = null;
                throw new Exception("Database Process have error when call SelectQueryCmd: " + ex.Message);
            }
            return dtb;
        }
        #endregion

        #region DataTable ExcuteDataTable(string cmdText)
        /*---------------------------------------------------------------
         * Method           : ExcuteDataTable(...) 
         * Muc dich         : Thuc hien truy van Select thi Database Oracle 
         * Tham so          : cmdText kieu string - cau lenh truy van SELECT
         * Tra ve           : DataTable
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : QuanLD
         * Ngay cap nhat    : 06/04/2008
         * Nguoi cap nhat   : QuanLD
         *--------------------------------------------------------------*/

         public static DataTable ExcuteDataTable(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable tblValue = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter();

            // Khoi tao ket noi moi
            OracleConnection oraConnection = OpenORACLEConnect();

            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                da = new OracleDataAdapter(oraCommand);
                da.Fill(tblValue);

                // Huy ket noi khi Select hoan thanh
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();

            }
            catch (Exception ex)
            {
                tblValue = null;
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();
                throw new Exception("Database Process have error when call SelectQueryCmd: " + ex.Message);
            }
            return tblValue;
        }
        #endregion

        public static DataTable ExcuteSQLDataTable(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable tblValue = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            // Khoi tao ket noi moi
            SqlConnection oraConnection = OpenSQLConnect();

            SqlCommand oraCommand = new SqlCommand(cmdText, oraConnection);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                da = new SqlDataAdapter(oraCommand);
                da.Fill(tblValue);

                // Huy ket noi khi Select hoan thanh
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();

            }
            catch (Exception ex)
            {
                tblValue = null;
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();
                throw new Exception("Database Process have error when call SelectQueryCmd: " + ex.Message);
            }
            return tblValue;
        }
        
       
        public static SqlConnection OpenSQLConnect()
        {          
            strConn = getConnString("SQL");
            SqlConnection connection = new SqlConnection(strConn);  
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
                    strConn = getConnString("SQL");
                    OpenSQLConnect();
                }
                icount = 0;
                WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
                return null;  
            }            
        }

        public static OracleConnection OpenORACLEConnect()
        {
            strConn = getConnString("ORACLE");
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
                    strConn = getConnString("ORACLE");
                    OpenORACLEConnect();
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
                if (strKeyCon.Trim() == "SQL")// Connect truc tiep vao DB2 host
                {
                    string strSERVER = getKeyConfig("SERVER_SQL");
                    string strDB_SQL = getKeyConfig("DB_SQL");  
                    string strUSER = getKeyConfig("USER_SQL");            
                    string strPASS = Decrypt(getKeyConfig("PASS_SQL"));

                    strConnect = "Data Source=" + strSERVER + ";database =" + strDB_SQL + ";User Id=" + strUSER + ";Password=" + strPASS + "; Integrated Security=no";
                }
                else if (strKeyCon.Trim() == "ORACLE")//Connect oracle gateway
                {
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

        public static int ExecuteNonQuery(string StoreName, CommandType cmdType, OracleParameter[] parameters)
        {
            int iReturn = 0;            
            OracleConnection oraConnection = OpenORACLEConnect();
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


        public static int SQLExecuteNonQuery(string StoreName, CommandType cmdType, SqlParameter[] parameters)
        {
            int iReturn = 0;
            SqlConnection oraConnection = OpenSQLConnect();
            if (oraConnection == null)
                return -1;
            SqlTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            SqlCommand oraCommand = new SqlCommand(StoreName, oraConnection, oraTransaction);
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

        public static int SQLExecuteNonQueryText(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            int iReturn = 0;
            SqlConnection oraConnection = OpenSQLConnect();
            if (oraConnection == null)
                return -1;
            SqlTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            SqlCommand oraCommand = new SqlCommand(cmdText, oraConnection, oraTransaction);
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
