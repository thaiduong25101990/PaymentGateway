#region Using Area
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Data.OracleClient;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Reflection;
#endregion
namespace BRReconcileIN
{
    public class DataAcess
    {
        private static string strConn ="";
        private static int icount;
        private static string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";
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
        public static int ExecuteNonQuery(CommandType cmdType, string StoreName, OracleParameter[] parameters)
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
        #region Method OpenConnect() - Code
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

        public static OracleConnection OpenConnect()
        {

            //Quan LD 8/4/08
            //bat dau


           // if (strConn.Trim() == "")
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

                // WriteLogFile(3, "Connect database failed: ConnectionString is " + connection.ConnectionString);
                return null;  //throw new Exception("Khong ket noi duoc CSDL: " + ex.Message);
            }

            //ket thuc cap nhat
        }
        #endregion

        #region Method getConnString() - Code
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



        #region Method getKeyConfig(string key) - Code
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

        #region string Decrypt(string toDecrypt): Giai ma mot string
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
         * Method           : SelectQueryStore(...) 
         * Muc dich         : Thuc hien truy van Select thi Database Oracle 
         * Tham so          : cmdText kieu string - cau lenh truy van SELECT
         * Tra ve           : DataTable
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 06/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region DataTable ExcuteDataTable(string cmdText)
        public static DataTable ExcuteDataTable(string cmdText)
        {
            // Tao mot DataTable moi
            DataTable dtb = new DataTable();
            OracleDataAdapter da;

            // Khoi tao ket noi moi
            OracleConnection oraConnection = DataAcess.OpenConnect();

            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                da = new OracleDataAdapter(oraCommand);
                da.Fill(dtb);

                // Huy ket noi khi Select hoan thanh
                oraConnection.Dispose();
                oraCommand.Dispose();
                da.Dispose();
            }
            catch (Exception ex)
            {
                dtb = null;
                throw new Exception("Database Process have error when call SelectQueryCmd: " + ex.Message);
            }
            return dtb;
        }
        #endregion

        // ghi log file dien
        public static void WriteLogDB(string filename, string pDescription)
        {
            try
            {

                OracleParameter[] parameters = new OracleParameter[]{
                    new OracleParameter("pREC_NAME",filename),
                    new OracleParameter("pMessage_text",pDescription) }; // Tao moi sanh sach cac Parameters

                DataAcess.ExecuteNonQuery(CommandType.StoredProcedure, "GW_PK_RECONCILE.Reconcile_trace", parameters);

            }
            catch
            {
            }
        }

    }
   
}
