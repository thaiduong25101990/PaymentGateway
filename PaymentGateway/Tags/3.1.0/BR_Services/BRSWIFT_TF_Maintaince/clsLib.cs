using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using DllASCIIAS400;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace BRSWIFTTFMaintaince
{
    public class Lib
    {
        private const string LOG_FOLDER = @"C:\ServiceLog";
        private const string LOG_FILENAME = "BRSWIFTTFMaintaince_Service.log";

        public const int LOG_LEVEL_1 = 1;
        public const int LOG_LEVEL_2 = 2;
        public const int LOG_LEVEL_3 = 3;

        // Duong dan file config - cau hinh Database
        private static string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";


        /*---------------------------------------------------------------
         * Method: WriteFile 
         * Muc dich: Viet mot noi dung String vao File
         * Tham so:
         *  - string DirPath
         *  - string FileName
         *  - string content
         *  Tra ve: True neu write thanh cong, tra ve False neu bi loi
         * Ngay tao: 02/04/2008
         * Nguoi tao: TrungNV3
         *--------------------------------------------------------------*/
        #region Ham ghi ra mot file Text
        public static bool WriteFile(string DirPath, string FileName, string strContent)
        {
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    WriteLogDB(1, "File bi trung lap: " + FileName,1);
                    return false;
                }
                using (StreamWriter sw = File.CreateText(FullName))
                {
                    sw.WriteLine(strContent);
                    sw.Close();
                    return true;
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }
        #endregion



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
         *--------------------------------------------------------------*/
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
            catch //(Exception ex)
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }
        #endregion

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
        #region void  WriteLogDB(int LogLevel, string Content)
        public static void WriteLogDB(int LogLevel, string Content,long lQueryID)
        {

            try
            {
                // Mo ket noi database            
                OracleTransaction oraTransaction;
                OracleConnection oraConnection = OpenConnect();
                oraTransaction = oraConnection.BeginTransaction();
                try
                {
                    Content = SafeQuery(Content);
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
                    parameters[2].Value = "BRSWIFTTFMaintaince";
                    parameters[3].Value = Content;
                    parameters[4].Value = "SIBS-SWIFT";
                    parameters[5].Value = LogLevel;
                    string strSQL = "insert into TF_svr_log (Log_Date,QUERY_ID,STATUS,DESCRIPTIONS,SERVICE,MSG_DIRECTION)"
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
            catch { }
        }
        #endregion


        #region *      Xoa Tam Thoi      *
        ///*---------------------------------------------------------------
        // * Method: WriteFile 
        // * Muc dich: Viet mot noi dung String vao File
        // * Tham so:
        // *  - string DirPath
        // *  - string FileName
        // *  - string content
        // *  Tra ve: True neu write thanh cong, tra ve False neu bi loi
        // * Ngay tao: 02/04/2008
        // * Nguoi tao: TrungNV3
        // *--------------------------------------------------------------*/
        //#region Ham ghi ra mot file Text
        //public static bool WriteFile(string DirPath, string FileName, string content)
        //{
        //    if (!Directory.Exists(DirPath))
        //        Directory.CreateDirectory(DirPath);
        //    string FullName = DirPath + "\\" + FileName;
        //    FileInfo file = new FileInfo(FullName);
        //    try
        //    {
        //        if (File.Exists(FullName))
        //        {
        //            WriteLogFile("File bi trung lap: " + FileName);
        //            return false;
        //        }
        //        using (StreamWriter sw = File.CreateText(FullName))
        //        {
        //            sw.WriteLine(content);
        //            sw.Close();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Can not Write to {0} file.", FullName);
        //        return false;
        //    }
        //}
        //#endregion


        ///*---------------------------------------------------------------
        // * Method: WriteLogFile 
        // * Muc dich: Viet mot noi dung String vao File
        // * Tham so:
        // *  - string DirPath
        // *  - string FileName
        // *  - string content
        // *  Tra ve: True neu write thanh cong, tra ve False neu bi loi
        // * Ngay tao: 02/04/2008
        // * Nguoi tao: TrungNV3
        // *--------------------------------------------------------------*/
        //#region Ham GHI LOG ra mot file log
        //public static bool WriteLogFile(string content)
        //{
        //    string DirPath = LOG_FOLDER;
        //    string FileName = LOG_FILENAME;
        //    if (!Directory.Exists(DirPath))
        //        Directory.CreateDirectory(DirPath);
        //    string FullName = DirPath + "\\" + FileName;
        //    FileInfo file = new FileInfo(FullName);
        //    try
        //    {
        //        if (File.Exists(FullName))
        //        {
        //            using (StreamWriter sw = File.AppendText(FullName))
        //            {
        //                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + content);
        //                sw.Close();
        //                return true;
        //            }
        //        }
        //        using (StreamWriter sw = File.CreateText(FullName))
        //        {
        //            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + content);
        //            sw.Close();
        //            return true;
        //        }
        //        WriteLog(content,1);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Can not Write to {0} file.", FullName);
        //        return false;
        //    }
        //}
        //#endregion


        ///*---------------------------------------------------------------
        // * Method           : WriteLog(...) 
        // * Muc dich         : Thuc hien ghi Service Log vao bang MSG_LOG trong Database Oracle 
        // * Tham so          :   + string ServiceName; Ten Service
        // *                      + string Content: Noi dung Log             
        // *                      + int ErrorLevel; Muc do quy uoc cho Log
        // * Tra ve           : void
        // * Ngay tao         : 04/04/2008
        // * Nguoi tao        : TrungNV3
        // * Ngay cap nha     : 06/04/2008
        // * Nguoi cap nhat   : TrungNV3
        // *--------------------------------------------------------------*/
        //#region void  Ham ghi Log vao Database - WriteLog
        //public static void WriteLog(string Content, int LogLevel)
        //{
        //    // Mo ket noi database  
        //    DateTime LogDate = DateTime.Now;


        //        OracleTransaction oraTransaction;            
        //        OracleConnection oraConnection = new OracleConnection(ProcessMaintaince.Instance().getConnString());
        //        try
        //        {
        //            oraConnection.Open();
        //        }
        //        catch
        //        {
        //            throw new Exception("Khong ket noi duoc Database");
        //        }
        //        oraTransaction = oraConnection.BeginTransaction();
        //        OracleCommand oraCommand = new OracleCommand("INSERT_MSG_LOG", oraConnection, oraTransaction);
        //        oraCommand.CommandType = CommandType.StoredProcedure;

        //        // Chuan hoa SQL tranh loi Injection SQL
        //        Content = SafeQuery(Content);

        //        // Khoi tao cac Parameter: 
        //        // Log_Date: Ngay gio ghi Log
        //        // Service: Ten Service
        //        // Content: Noi dung ghi log
        //        // Error_Level: Dinh muc quan trong cua Log
        //        OracleParameter[] parameters = new OracleParameter[]{
        //                                        new OracleParameter("Log_Date",OracleType.DateTime),
        //                                        new OracleParameter("Service",SERVICE_NAME),
        //                                        new OracleParameter("Content",Content),
        //                                        new OracleParameter("log_level",LogLevel)};
        //        parameters[0].Value = LogDate;
        //        // Add cac Parameter vao Command va Execute
        //        oraCommand.Parameters.AddRange(parameters);
        //   try
        //   {

        //            oraCommand.ExecuteNonQuery();
        //        oraTransaction.Commit();

        //        // Huy ket noi sau khi hoan thanh
        //        oraConnection.Dispose();
        //        oraCommand.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Neu qua trinh Insert bi loi thi RollBack
        //        oraTransaction.Rollback();
        //        throw new Exception("Khong the thuc hien viec ghi Log - " + ex.Message);
        //    }
        //}
        //#endregion


        ///*---------------------------------------------------------------
        // * Method           : getKeyConfig(string key) 
        // * Muc dich         : Doc mot value key tu the Tag trong file cau hinh
        // * Tham so          : string key
        // * Tra ve           : Mot string la Value cua key 
        // * Ngay tao         : 04/04/2008
        // * Nguoi tao        : TrungNV3
        // * Ngay cap nhat    : 28/04/2008
        // * Nguoi cap nhat   : QuanLD
        // *--------------------------------------------------------------*/
        //#region Method getKeyConfig(string key) - Code
        //public static string getKeyConfig(string key)
        //{
        //    string m_strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";
        //    string result = "";
        //    // load config document 
        //    XmlDocument doc = new XmlDocument();
        //    try
        //    {
        //        doc.Load(m_strConfigFilePath);
        //    }
        //    catch (System.IO.FileNotFoundException ex)
        //    {

        //        throw new Exception("Khong tim thay File cau hinh cua he thong Service: " + ex.Message);
        //    }
        //    // retrieve ServicesConfig node
        //    XmlNode node = doc.SelectSingleNode("//DatabaseConfig");
        //    if (node == null)
        //    {
        //        throw new InvalidOperationException("Khong tim thay tag DatabaseConfig trong file cau hinh.");
        //    }
        //    try
        //    {
        //        // select the 'add' element that contains the key
        //        XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
        //        if (elem == null)
        //        {
        //            // add value for key
        //            throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");
        //        }
        //        else
        //        {
        //            // key was not found so create the 'add' element
        //            // and set it's key/value attributes
        //            result = elem.Attributes.GetNamedItem("value").Value;
        //        }
        //    }
        //    catch
        //    {
        //        WriteFile(System.Environment.CurrentDirectory, "GW_Service.log", "Khong tim thay key " + key + " trong file cau hinh he thong service.");
        //        throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");

        //    }
        //    return result;
        //}
        //#endregion


        #endregion

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


        #region  * Xoa  Tam thoi  *
        ///*---------------------------------------------------------------
        // * Method           : void ExecuteNonQuery(...) 
        // * Muc dich         : Thuc hien lenh thuc thi Database Oracle
        // *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
        // * Tham so          : + stirng cmdText: Ten cua StoreProcedure             
        // *                    + parameters: Mot danh sach OracleParameter
        // * Tra ve           : void
        // * Ngay tao         : 04/04/2008
        // * Nguoi tao        : TrungNV3
        // * Ngay cap nhat    : 08/04/2008
        // * Nguoi cap nhat   : TrungNV3
        // *--------------------------------------------------------------*/
        //#region Method ExecuteNonQuery (string StoreName, OracleParameter[] parameters)
        //public static void ExecuteNonQuery(string StoreName, OracleParameter[] parameters)
        //{
        //    string strConn = ProcessMaintaince.Instance().getConnString();
        //    OracleConnection oraConnection = new OracleConnection(strConn);            

        //    OracleTransaction oraTransaction; 
        //    try{
        //     oraConnection.Open();
        //    }
        //    catch
        //    {
        //            throw new Exception("Khong ket noi duoc Database");
        //    }
        //    oraTransaction = oraConnection.BeginTransaction();
        //    OracleCommand oraCommand = new OracleCommand(StoreName, oraConnection, oraTransaction);
        //    oraCommand.CommandType = CommandType.StoredProcedure;
        //    if (parameters != null)
        //        oraCommand.Parameters.AddRange(parameters);
        //    try
        //    {
        //        oraCommand.ExecuteNonQuery();
        //        oraTransaction.Commit();
        //        oraConnection.Dispose();
        //        oraCommand.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        oraTransaction.Rollback(); // neu xay ra loi thi RollBack
        //        throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
        //    }

        //}
        //#endregion
        //#region Ham lay du lieu tu DB vao bang
        ///*---------------------------------------------------------------
        //* Method           : ExcuteTable() 
        //* Muc dich         : Lay ra du lieu tu DB gan vao 1 DataTable
        //* Tham so          : strConnection Chuoi ket noi du lieu
        //*                  : strContext Cau lenh query du lieu
        //*                  
        //* Tra ve           : Table
        //* Mo ta noi dung   : 
        //* Ngay tao         : 25/04/2008
        //* Nguoi tao        : QuanLD
        //* Ngay cap nhat    : 25/04/2008
        //* Nguoi cap nhat   : QuanLD
        //*--------------------------------------------------------------*/
        //public static DataTable ExcuteTable(string strConnection, string strContext)
        //{
        //    DataSet datDs = new DataSet();
        //    try
        //    {
        //        OracleDataAdapter oraDa = new OracleDataAdapter(strContext, strConnection);
        //        oraDa.Fill(datDs);
        //        //huy 
        //        oraDa.Dispose();
        //        return datDs.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}
        //#endregion
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
                    case ProcessMaintaince.GW_TYPE_PACKED:
                        iSISBLenght = (iFieldLength + 2) / 2;

                        break;
                    case ProcessMaintaince.GW_TYPE_BINARY:
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

        // Ham tranh loi truy van Injection SQL
        #region Ham tranh loi Injection Query
        // Tranh loi Injection SQL
        public static string SafeQuery(string inputQuery)
        {
            return inputQuery.Replace("'", "''");
        }
        #endregion

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
        #region Method ExecuteNonQuery (string StoreName, OracleParameter[] parameters)
        public static void ExecuteNonQuery(string StoreName, OracleParameter[] parameters)
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
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
            }

        }
        #endregion

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
        #region Method ExecuteNonQuery (string StoreName, OracleParameter[] parameters)
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
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
            }
            return iReturn;
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : void ExecuteNonQuery(...) 
         * Muc dich         : Thuc hien lenh thuc thi Database Oracle 
         *                    ma khong phai truy van Select - Vi du: Insert, Update, Delete
         * Tham so          : string cmdText - cau lenh PL/SQL                
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        #region void ExecuteNonQuery(string cmdText)
        public static void ExecuteNonQuery(string cmdText)
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
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
            }

        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : Select_ReturnDataTable(...) 
         * Muc dich         : Thuc hien truy van Select thi Database Oracle 
         * Tham so          : cmdText kieu string - cau lenh truy van SELECT
         * Tra ve           : DataTable
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 06/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region DataTable Select_ReturnDataTable(string cmdText)
        public static DataTable Select_ReturnDataTable(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable dtb = new DataTable();
            OracleDataAdapter da;

            // Khoi tao ket noi moi
            OracleConnection oraConnection = OpenConnect();

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


        /*---------------------------------------------------------------
         * Method           : SelectDataReader(...) 
         * Muc dich         : Thuc hien truy van Select thi Database Oracle 
         * Tham so          : cmdText kieu string - cau lenh truy van SELECT
         * Tra ve           : OracleDataReader
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 06/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region static OracleDataReader Select_ReturnReader(string cmdText)
        public static OracleDataReader Select_ReturnReader(string cmdText)
        {

            // Tao mot DataReader
            OracleDataReader dr;
            try
            {
                // Khoi tao ket noi moi cho database
                OracleConnection oraConnection = OpenConnect();

                OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
                oraCommand.CommandType = CommandType.Text;
                // Thuc hien lenh oraCommand
                dr = oraCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                dr = null;
                throw new Exception("Co loi xay ra khi Select_ReturnReader: " + ex.Message);
            }
            return dr;
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
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = getConnString();
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                int count = 0;
                // Cho so lan connect toi da la 3 lan
                while (count < 3)
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)

                        return connection;
                    count++;
                }

            }
            catch //(Exception ex)
            {
                WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
            }
            return null;
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
                throw new Exception("Khong the doc va giai ma duoc ConnectionString tu file cau hinh." + ex.Message);
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

                throw new Exception("Khong tim thay File cau hinh cua he thong Service: " + ex.Message);
            }
            // retrieve ServicesConfig node
            XmlNode node = doc.SelectSingleNode("//DatabaseConfig");
            if (node == null)
            {
                throw new InvalidOperationException("Khong tim thay tag DatabaseConfig trong file cau hinh.");
            }
            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem == null)
                {
                    // add value for key
                    throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");
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
