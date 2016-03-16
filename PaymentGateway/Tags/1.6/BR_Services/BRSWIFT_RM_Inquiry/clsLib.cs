using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using DllASCIIAS400;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Data;


namespace GWSWIFTRMInquiry
{
    public class Lib
    {
        private static string strConn = "";
        private static int icount = 0;
        private const string LOG_FOLDER = @"C:\ServiceLog";
        private const string LOG_FILENAME = "BRSWIFTRMInquiry_Service.log";
        // Duong dan file config - cau hinh Database
        private static string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";

        
        
        #region Ham GHI LOG ra mot file log
        /*---------------------------------------------------------------
         * Method: WriteLogFile 
         * Muc dich: Viet mot noi dung String vao File
         * Tham so:
         *  - int log_Level
         *  - string DirPath: Thu muc chua file log
         *  - string FileName: Ten file Log
         *  - string content: Noi dung ghi log
         *  Tra ve: True neu write thanh cong, tra ve False neu bi loi
         * Ngay tao: 02/04/2008
         * Nguoi tao: TrungNV3
         * * Ngay Sua: 02/04/2008
         * Nguoi tao: TrungNV3
         *--------------------------------------------------------------*/

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
        /*---------------------------------------------------------------
         * Method           : WriteLogDB(...) 
         * Muc dich         : Thuc hien ghi Service Log vao bang MSG_LOG trong Database Oracle 
         * Tham so          : 
         *                      + string Content: Noi dung Log             
         *                      + int ErrorLevel; Muc do quy uoc cho Log
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nha     : 05/05/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/

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
                    parameters[2].Value = "BRSWIFTRMInquiry";
                    parameters[3].Value = Content;
                    parameters[4].Value = "SIBS-SWIFT";
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
                catch //(Exception ex)
                {
                    // Neu qua trinh Insert bi loi thi RollBack
                    oraTransaction.Rollback();
                }
            }
            catch { };
        }
        #endregion

        #region Lay 1 doan text trong mang char
        /*---------------------------------------------------------------
       * Method           : GetStringSIBS() 
       * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
       * Tham so          : strSource gia tri truong dua vao
       *                  : iLength do dai chuoi lay ra
       *                  : iDatatype kieu du lieu trong SIBS cua chuoi kys tu dau vao
       *                  : iBeginPost Vi tri dau tien cua ky tu
       * Tra ve           : Mot chuoi gia tri co do dai=iLength
       * Mo ta noi dung   : 
       * Ngay tao         : 23/04/2008
       * Nguoi tao        : QuanLD
       * Ngay cap nhat    : 09/04/2008
       * Nguoi cap nhat   : QuanLD
       *--------------------------------------------------------------*/
        public static string GetStringSIBS(char[] charSource, int iBeginPost, int iLength, int iDatatype)
        {

            string strValue = "";
            int i = iBeginPost - 1;
            try
            {
                while (i < iBeginPost + iLength - 1)
                {
                    strValue = strValue + charSource[i];
                    i = i + 1;
                }

            }
            catch
            {
                return "";
            }
            return strValue;

        }
        #endregion

        #region Ham convert du lieu tu ASCII_AS400 bang
        /*---------------------------------------------------------------
        * Method           : ConvertASCII_AS400() 
        * Muc dich         : Convert du lieu tu ASCII sang AS400 de gui vao SIBS
        * Tham so          :
        *                  : strContext chuoi nguon
        *                  : iFieldLength
        *                  : iDataType
        * Tra ve           : String kieu AS400
        * Mo ta noi dung   : 
        * Ngay tao         : 25/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 25/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/


        public static string ConvertASCII_AS400(string strContent, int iFieldLength, int iDataType)
        {
            string strAS400Value = "";
            int iSISBLenght = 0;

            try
            {
                CCMessage cConvertAS400 = new CCMessage();
                byte[] byteAS400 = new byte[iFieldLength];
                char[] chrAS400 = new char[iFieldLength];
                switch (iDataType)
                {
                    case ProcessInquiry.GW_TYPE_PACKED:
                        iSISBLenght = (iFieldLength + 2) / 2;

                        break;
                    case ProcessInquiry.GW_TYPE_BINARY:
                        iSISBLenght = 4;

                        break;
                    default:
                        iSISBLenght = iFieldLength;
                        break;
                }
                unsafe
                {
                    IntPtr inb = new IntPtr((byte*)cConvertAS400.ConvertFieldToAs400(strContent, iFieldLength, iDataType));
                    Marshal.Copy(inb, byteAS400, 0, iFieldLength);// cConvertAS400.ConvertFieldToAs400(strContent, iFieldLength, iDataType);
                    for (int i = 0; i < iSISBLenght; i++)
                    {
                        // chrAS400[i] = Convert.ToChar(byteAS400[i]);
                        strAS400Value = strAS400Value + Convert.ToChar(byteAS400[i]);
                    }
                    Marshal.FreeHGlobal(inb);
                }

                return strAS400Value;



            }

            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        #region Ham convert du lieu tu AS400 - ASCII bang
        /*---------------------------------------------------------------
        * Method           : ConvertAS400_ASCII() 
        * Muc dich         : Lay ra du lieu tu DB gan vao 1 DataTable
        * Tham so          : strConnection Chuoi ket noi du lieu
        *                  : strContext Cau lenh query du lieu
        *                  
        * Tra ve           : String
        * Mo ta noi dung   : 
        * Ngay tao         : 25/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 25/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        public static string ConvertAS400_ASCII(string strContent, int iFieldLength, int iDataType, int iOption)
        {
            try
            {
                CCMessage cConvertASCII = new CCMessage();
                string strASCII;
                strASCII = cConvertASCII.ConvertFieldAscii(strContent, iFieldLength, iDataType, iOption);

                return strASCII;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion


        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    DATABASE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        #region Method ExecuteNonQuery (string StoreName, OracleParameter[] parameters)
        /*---------------------------------------------------------------
         * Method           : void ExecuteNonQuery(...) 
         * Muc dich         : Thuc hien lenh thuc thi Database Oracle
         *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
         * Tham so          : + stirng cmdText: Ten cua StoreProcedure             
         *                    + parameters: Mot danh sach OracleParameter
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
       
        public void ExecuteNonQuery(string StoreName, OracleParameter[] parameters)
        {
            OracleConnection oraConnection = OpenConnect();

            OracleTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            OracleCommand oraCommand = new OracleCommand(StoreName, oraConnection, oraTransaction);
            oraCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                oraCommand.Parameters.AddRange(parameters);
            try
            {
                oraCommand.ExecuteNonQuery();
                oraTransaction.Commit();

                // Sau khi xu li thanh cong
                oraConnection.Close();
                oraCommand.Dispose();
                oraTransaction.Dispose();
            }
            catch (Exception ex)
            {
                oraTransaction.Rollback(); // neu xay ra loi thi RollBack
                throw new Exception("OracleTransaction xay ra loi, ExecuteNonQuery Failed" + ex.Message);
            }

        }
        #endregion

        #region Method ExecuteNonQuery (string StoreName,, CommandType cmdType, OracleParameter[] parameters)

        /*---------------------------------------------------------------
         * Method           : void ExecuteNonQuery(...) 
         * Muc dich         : Thuc hien lenh thuc thi Database Oracle
         *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
         * Tham so          : + string cmdText: Ten cua StoreProcedure, hay chuoi text           
         *                    + parameters: Mot danh sach OracleParameter
         * Tra ve           : int -1 neu that bai
         * Ngay tao         : 08/04/2008
         * Nguoi tao        : QuanLD
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : QuanLD
         *--------------------------------------------------------------*/
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
        #endregion

        #region void ExecuteNonQuery(string cmdText)
        /*---------------------------------------------------------------
         * Method           : void ExecuteNonQuery(...) 
         * Muc dich         : Thuc hien lenh thuc thi Database Oracle 
         *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
         * Tham so          : string cmdText - cau lenh PL/SQL                
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        
        public void ExecuteNonQuery(string cmdText)
        {
            // Mo ket noi moi
            OracleConnection oraConnection = OpenConnect();

            // Mo Transction de kiem soat qua trinh thuc hien 
            OracleTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection, oraTransaction);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                oraCommand.ExecuteNonQuery();
                oraTransaction.Commit();

                // Dong ket noi khi hoan thanh
                // Sau khi xu li thanh cong
                oraConnection.Close();
                oraCommand.Dispose();
                oraTransaction.Dispose();
            }
            catch (Exception ex)
            {
                oraTransaction.Rollback();
                throw new Exception("OracleTransaction , ExecuteNonQuery fialed " + ex.Message);
            }

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
            OracleConnection oraConnection = OpenConnect();

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
            catch //(Exception ex)
            {
                tblValue = null;
                da.Dispose();
                oraConnection.Dispose();
                oraCommand.Dispose();
               //throw new Exception("Database Process have error when call SelectQueryCmd: " + ex.Message);
            }
            return tblValue;
        }
        #endregion

        /*---------------------------------------------------------------
         * Method           : OpenConnect() 
         * Muc dich         : Tao mot ket noi Connection toi Oracle DB
         * Tham so          : Khong co tham so
         * Tra ve           : Mot OracleConnection da duoc Open
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method OpenConnect() - Code
        public static OracleConnection OpenConnect()
        {

            //Quan LD 8/4/08
            //bat dau
            //if (strConn.Trim() == "")
                strConn = getConnString();
            OracleConnection connection = new OracleConnection(strConn);
            //connection.ConnectionString = getConnString();

            try
            {
                connection.Open();
                icount = 0;
                return connection;
                              
            }
            catch //(Exception ex)
            {
                icount++;
                if (icount < 4) // Cho so lan connect toi da la 3 lan
                {
                    strConn = getConnString();
                    OpenConnect();
                }
                icount = 0;

                WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
                return null;  //throw new Exception("Khong ket noi duoc CSDL: " + ex.Message);
            }

            //ket thuc cap nhat
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : getConnString() 
         * Muc dich         : Lay chuoi ket noi Database tu file cau hinh
         * Tham so          : Khong co tham so
         * Tra ve           : Mot string ket noi database
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 04/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method getConnString() - Code
        public static string getConnString()
        {
            string strConnect = "";
            try
            {
                string strDatabase = getKeyConfig("DatabaseName");
                // Giai ma Username

                string strUser = getKeyConfig("Username");
                // Giai ma Password
                string strPassword = Decrypt(getKeyConfig("Password"));
                strConnect = "Data Source=" + strDatabase + "; User Id=" + strUser + " ; Password=" + strPassword + " ; Integrated Security=No;";

            }
            catch (Exception ex)
            {
                throw new Exception("File config failed." + ex.Message);
            }
            return strConnect;
        }
        #endregion


        // ham giai ma        
        /********************************************************************
         * Method: public string Decrypt(string toDecrypt)
         * Muc dich: Giai Ma hoa ma mot Strig
         * Tham so: string toDecrypt
         * Tra ve: Mot string da duoc giai ma
         * Ngay tao: 20/03/2008
         * Nguoi tao: TrungNV3
         * Ngay cap nhat: 31/03/2008
         * Nguoi cap nhat: TrungNV3
        ********************************************************************/
        #region string Decrypt(string toDecrypt): Giai ma mot string
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

        #endregion


        /*---------------------------------------------------------------
         * Method           : getKeyConfig(string key) 
         * Muc dich         : Doc mot value key tu the Tag trong file cau hinh
         * Tham so          : string key
         * Tra ve           : Mot string la Value cua key 
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 04/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Method getKeyConfig(string key) - Code
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
        #endregion

        // End class
    }
}
