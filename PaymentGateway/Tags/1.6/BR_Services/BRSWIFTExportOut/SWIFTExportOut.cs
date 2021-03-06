﻿/****************************************************************
 * File: GWSWIFTExport.cs
 * ----------------------------
 * Noi dung: 
 * - Xay dung cac ham xu ly cho Service SWIFT Import In
 * ----------------------------
 * Nguoi tao: TRUNGNV
 * FPT Infomation System - Banking Software Solution Center
 ****************************************************/
//=================================================================

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
using System.Reflection; // Ma hoa luc runtime
#endregion

namespace GWSWIFTExport
{
    public partial class SWIFTExport : ServiceBase
    {
        public static bool isStop = false;
        public static bool isEncrypt = false;
        public static string EncryptPath = "";
        public static string DecryptPath = "";
        public static int timeSleep = 200; // Dat thoi gian tre cho service, mac dinh la 200
        long Query_ID = 0;  

        #region Cac bien va Hang so

        private static string SERVICE_NAME = "";
        private const string GW_TYPE = "SWIFT";
        private const string MSG_TYPE = "SIBS-SWIFT";
        private const string key = "Security2008"; //Encrypt Key 
        private const string LOG_FOLDER = @"C:\ServiceLog";
        private const string LOG_FILENAME = "GWSWIFTExport_Service.log";
       
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;
        private string mFilename;
       
        // Duong dan file config - cau hinh Database
        private string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";

        private OracleConnection oraConnection = new OracleConnection();
        //private OracleCommand oraCommand;
       // private static string dateNow; // lay ngay thang theo dang yyyyMMdd
       // private static string timeNow; // Lay full time theo dang 208/04/15 10:30:25 PM

        #endregion

        public SWIFTExport()
        {            
            InitializeComponent();
            //test
            //SERVICE_NAME = this.ServiceName;
            OnService();
        }

        #region  Cac ham OnStart va OnStop
        protected override void OnStart(string[] args)
        {
            try
            {
                SERVICE_NAME = this.ServiceName;
                WriteLogDB(LOG_Info, this.ServiceName + " On Start",1);
                Thread th = new Thread(new ThreadStart(OnService));
                th.Start();

            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, this.ServiceName + " Error Try-Catch on Process: " + ex.Message,1);
                base.OnStart(args);
            }
        }

        protected override void OnStop()
        {
            WriteLogDB(LOG_Info, this.ServiceName + " On Stop...",1);
            isStop = true;
        }

        private void OnService()
        {
            SERVICE_NAME = this.ServiceName;
            while (!isStop)
            {
                // Kiem tra dieu kiem xem co ma hoa khong - Encrypt
                BeforeProcess();
                // Bat dau vong xu li moi
                StartProcess();
                Thread.Sleep(timeSleep); // Thoi gian tre la 1 giay
            }
        }


        #endregion


        /*------------------------------------------------------
         * GATEWAY CODE HERE
         * ----------------------------------------------------*/
        // Ham kiem tra de lay cac tham so ve ma hoa ENCRYPT
        private void BeforeProcess()
        {
            try
            {

                DataTable datGWType = new DataTable();

                // cap nhat moi 03/12/2008  lay thoi gian delay cua service tu bang GWService_port.
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRSWIFTExport')";
                datGWType = new DataTable();
                datGWType = ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();

                // het cap nhat.
                
                //// Lay cac thong tin ve ENCRYPT
                //strCmd = "SELECT ENCRYPT, ENCRYPTFUNCTION,DECRYPTFUNCTION,FILETIME From GWTYPE WHERE GWTYPE = 'SWIFT'";

                //datGWType = new DataTable();
                //datGWType = ExcuteDataTable(strCmd);
                //if (datGWType.Rows.Count > 0)
                //{
                //    //timeSleep = Convert.ToInt32(datGWType.Rows[0]["FILETIME"]);
                //    if (datGWType.Rows[0]["ENCRYPT"] + "" == "1")
                //    {
                //        isEncrypt = true;
                //    }
                //    else
                //        return;

                //    EncryptPath = datGWType.Rows[0]["ENCRYPTFUNCTION"].ToString() + "";
                //    DecryptPath = datGWType.Rows[0]["DECRYPTFUNCTION"].ToString() + "";  //dtReader["DECRYPTFUNCTION"].ToString();
                //}
                //datGWType.Clear();
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, " Khong the lay thong tin ve Encrypt trong GWTYPE " + ex.Message,1);
                return;
            }
        }

        //// Ham public hien Ma Hoa tu thu vien DLL
        //private string MakeEncrypt(string input)
        //{
        //    string result = "";
        //    Type theType = null;
        //    object theClass = null;
        //    try
        //    {
        //        // Load file DLL
        //        // Get Class & Create Instance

        //        Assembly a =
        //          Assembly.LoadFrom("SecurityEncrypt.dll");
        //        theClass = a.CreateInstance(EncryptPath);
        //        theType = a.GetType(EncryptPath);
        //        // with the reference to the dynamically 
        //        // created class you can invoke the method 
        //        object[] arguments = new object[1] { input };
        //        object retVal =
        //           theType.InvokeMember("Encrypt",
        //           BindingFlags.Default |
        //           BindingFlags.InvokeMethod,
        //           null,
        //           theClass,
        //           arguments);
        //        result = retVal.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Khong the ma hoa: {0}", ex.Message);
        //        result = null;
        //    }
        //    if (result == null)
        //        return input; // Neu ham ma hoa bi loi thi khong ma hoa
        //    return result;
        //}
            
        /*---------------------------------------------------------------
        * Method           : StartProcess() 
        * Muc dich         : Ham goi bat dau phuong thuc xu li Service
        * Tham so          : Khong co tham so
        * Tra ve           : void
        * Ngay tao         : 05/04/2008
        * Nguoi tao        : TrungNV3
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        #region  Ham bat dau xu li - StartProcess()
        public void StartProcess()
        {
            try
            {
                if (!isStop)
                {
                    //WriteLogDB(LOG_Info, SERVICE_NAME + " Bat dau xu li StartProcess()");
                    // Lay danh sach cac dien can xu li tu bang IBPS_Msg_Content
                    DataTable dtbList= new DataTable();
                    dtbList  = getMsgList();

                    // Neu danh sach Message la Ro^ng~ hoac Null
                    if (dtbList == null || dtbList.Rows.Count==0)
                    {
                        //WriteLogDB(LOG_Warning, SERVICE_NAME + " khong co du lieu tu bang SWIFT_MSG_OUTWARD");
                        return;  // Thoat khoi viec xu li khi danh sach rong
                    }
                    // Neu danh sach co dien thi bat dau xu li
                    else
                    {
                        string ExportFolder = getFolder();
                        if (String.IsNullOrEmpty(ExportFolder))
                        {
                            WriteLogDB(LOG_Error, SERVICE_NAME + " duong dan Folder Export cua SWIFT bi trong rong",1);
                            return;
                        }
                        int MsgCount = dtbList.Rows.Count;
                        long MSG_ID = 0;  
                        int NumOfMsg = 5; // Mac dinh gia tri la 5
                        NumOfMsg = getNumOfMsg(); // Lay gia tri tu Database - so dien export trong mot file
                        int count = 0;

                        // Doc lan luot de xuat NumOfMsg dien vao 1 file
                        while (count < MsgCount)
                        {

                            int beginCount = count;
                            int endCount = count + NumOfMsg;
                            StringBuilder CONTENT = new StringBuilder();
                            // Danh sach chua cac dien can xuat theo so dien trong 1 file
                            List<long> MsgGrouplist = new List<long>(); 
                            MsgGrouplist.Clear();
                            
                            if ((MsgCount - count < NumOfMsg) && (count <= MsgCount))
                            {
                                endCount = MsgCount;
                            }
                            for (int i = beginCount; i < endCount; i++)
                            {
                                Query_ID = Convert.ToInt64(dtbList.Rows[i]["Query_Id"]);
                                MSG_ID = Convert.ToInt64(dtbList.Rows[i]["MSG_ID"]);
                                string _CONTENT = dtbList.Rows[i]["CONTENT"].ToString();
                                
                                //cap nhat cho MSB
                                if (CONTENT.ToString().Trim().Length > 1)
                                {
                                    _CONTENT = "$" + _CONTENT;
                                }
                                //het cap nhat.

                                // Khong nen de dat trang thai Pending vi xu li 5 dien mot luc, neu bi loi thi trang thai mai mai la 2
                                // Dat trang thai Pending
                                OracleParameter[] paras = new OracleParameter[] { 
                                    new OracleParameter("pMSG_ID", OracleType.Number) ,
                                    new OracleParameter("pStatus", OracleType.Number) };
                                paras[0].Value = MSG_ID;
                                paras[1].Value = 2;
                                // Cap nhat trang thai Pending
                                ExecuteNonQuery("SWIFT_EXPORT.SWIFT_UPDATE_STATUS", paras);
                                if (!Check_message_In(Query_ID, "SIBS-SWIFT"))
                                {
                                    Insert_Index(Query_ID, "SIBS-SWIFT");
                                    // Add dien vao danh sach cac den can xuat
                                    MsgGrouplist.Add(MSG_ID);
                                    //CONTENT.Append(_CONTENT);
                                    if (_CONTENT.Trim() == "")
                                    {
                                        MsgGrouplist.Remove(MSG_ID);
                                    }
                                    else
                                        CONTENT.Append(FixLength(_CONTENT.Trim()));
                                }
                                else
                                {
                                    WriteLogDB(LOG_Warning, "Dien nay da duoc gui lan lai", Query_ID);
                                }
                            }
                            // Tang them gia tri bien dem
                            count = endCount;

                            // Tao ten file
                            string FileName = getFileName();
                            mFilename = FileName;
                            string msgCONTENT = "";
                            msgCONTENT = CONTENT.ToString();

                            //if (isEncrypt) 
                            //{
                            //    msgCONTENT = MakeEncrypt(msgCONTENT); 
                            //}
                            
                            // Export ra file
                            if (ExportFile(ExportFolder, FileName, msgCONTENT))
                            {
                                try
                                {  
                                    foreach (long lMsgID in MsgGrouplist)
                                    {
                                        try
                                        {
                                            // Goi ham cap nhat trang thai va sao chep HIS
                                            OracleParameter[] paras = new OracleParameter[] { new OracleParameter("pMSG_ID", OracleType.Number) ,
                                             new OracleParameter("pFILENAME", OracleType.VarChar,50) };
                                            paras[0].Value = lMsgID;
                                            paras[1].Value = mFilename;

                                            // Goi StoreProcedure lam cac cong viec sau:
                                            // Cap nhat truong MANUAL cua dien bang 2 va sao chep dien co MSG_ID = lMsgID sang bang Swift_Msg_Outward_His
                                            // Xoa dien tai bang Swift_Msg_Outward co ID bang lMsgID
                                            ExecuteNonQuery("SWIFT_EXPORT.SWIFT_UPDATE_CONTENT", paras);
                                        }
                                        catch //(Exception ex)
                                        {
                                            OracleParameter[] paras = new OracleParameter[] { 
                                            new OracleParameter("pMSG_ID", OracleType.Number) ,
                                            new OracleParameter("pStatus", OracleType.Number) };
                                            paras[0].Value = lMsgID;
                                            paras[1].Value = -1;
                                            // Cap nhat trang thai Pending
                                            ExecuteNonQuery("SWIFT_EXPORT.SWIFT_UPDATE_STATUS", paras);

                                            continue; // Neu bi loi chuyen sang dien khac
                                        }
                                    }                                    
                                }
                                catch (Exception)
                                {
                                    OracleParameter[] paras = new OracleParameter[] { 
                                    new OracleParameter("pMSG_ID", OracleType.Number) ,
                                    new OracleParameter("pStatus", OracleType.Number) };
                                    paras[0].Value = MSG_ID;
                                    paras[1].Value = -1;
                                    // Cap nhat trang thai Pending
                                    ExecuteNonQuery("SWIFT_EXPORT.SWIFT_UPDATE_STATUS", paras);

                                    continue; // Neu bi loi chuyen sang dien khac
                                }
                            }

                        }

                        // Giai phong bo nho
                        dtbList.Dispose();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, SERVICE_NAME + " : " + ex.Message,Query_ID);
                return;
            }
        }

        #endregion

        #region
        public int Insert_Index(long LID, string MSG_DIRECTION)
        {
            try
            {
                string strSQL = "insert into TF_SVR_INDEX(MSG_ID,MSG_DIRECTION) values(" + LID + ",'" + MSG_DIRECTION + "')";
                ExecuteNonQuery(strSQL);
                return 1;
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, "Bi loi khi insert du lieu vao bang TF_SVR_INDEX" + ex.Message, Query_ID);
                return -1;
            }
        }
        #endregion

        #region
        public bool Check_message_In(long LID, string MSG_DIRECTION)
        {
            bool bBool = true;
            try
            {
                DataTable datIndex = new DataTable();
                string strSQL = "select MSG_ID from TF_SVR_INDEX IB where IB.MSG_ID= " + LID + " and IB.MSG_DIRECTION = '" + MSG_DIRECTION + "' ";
                datIndex = Select_ReturnDataTable(strSQL);
                if (datIndex.Rows.Count == 1)
                {
                    bBool = true;
                }
                else if (datIndex.Rows.Count == 0)
                {
                    bBool = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, "Bi loi khi check du lieu" + ex.Message, Query_ID);
                bBool = false;
            }
            return bBool;
        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    MESSAGE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*---------------------------------------------------------------
         * Method           : getMsgList() 
         * Muc dich         : Ham lay danh sach cac dien tu bang SWIFT_MSG_OUTWARD
         * Tham so          : Khong co tham so
         * Tra ve           : DataTable 
         * Ngay tao         : 02/05/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 02/05/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham lay danh sach cac dien tu bang SWIFT_MSG_OUTWARD
        private DataTable getMsgList()
        {
            DataTable dtbResult = new DataTable();
            try
            {
                string strCmd = "SELECT MSG_ID,CONTENT, Query_Id  FROM SWIFT_MSG_OUTWARD "
                                + " WHERE AUTO='Y' and ROWNUM <=100 And Status=0 ORDER BY MSG_ID";
                dtbResult = Select_ReturnDataTable(strCmd);
                //dtReader = Select_ReturnReader(strCmd);
                return dtbResult;
                
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        #endregion



        /*---------------------------------------------------------------
         * Method           : FixLength() 
         * Muc dich         : Lay do dai theo boi cua 512
         * Tham so          : string content
         * Tra ve           : string 
         * Ngay tao         : 17/05/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 17/05/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham fix do dai cua dien
        private string FixLength(string content)
        {
            //Cap nhat cho MSB khong lay la boi so cua 512 byte

            return content;

            //Het cap nhat
            int length = content.Length;
            if (length % 512 == 0)
                return content;
            else
            {
                StringBuilder strResult = new StringBuilder();
                int newLength = 512*((length / 512) + 1);
                strResult.Append(content);
                for(int i=length;i<newLength;i++)
                {
                    strResult.Append(" "); // Chen them cac ky tu trong
                }
                // 
                return strResult.ToString();                
            }                      
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : getNumOfMsg() 
         * Muc dich         : Lay SWIFTNUMOFMESS (So dien trong 1 file) cho dien SWIFT Export
         * Tra ve           : Mot so nguyen - int
         * Ngay tao         : 17/05/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham lay so NumOfMsg trong bang SYSVAR
        public int getNumOfMsg()
        {
            // String Truy van Oracle de lay ra so ref hien tai ung voi ngay Today
            string strCmd = "SELECT SV.VALUE AS NumOfMsg FROM SYSVAR SV WHERE SV.GWTYPE='SWIFT' and VARNAME='SWIFTNUMOFMESS'";

            // Khoi tao so iNumOfMsg bang 5
            int iNumOfMsg = 5; // Mac dinh la 5
            DataTable datrbl = new DataTable();

            try
            {
                datrbl = Select_ReturnDataTable(strCmd);
                if (datrbl.Rows.Count>0)
                {
                    // Doc so NumOfMsg tu trong DataReader
                    iNumOfMsg = Convert.ToInt32(datrbl.Rows[0]["NumOfMsg"].ToString());
                    //Dong ket noi 
                }
                datrbl.Clear();

                //OracleDataReader dataReader = Select_ReturnReader(strCmd);
                //if (dataReader.HasRows)
                //{
                //    dataReader.Read();

                //    // Doc so NumOfMsg tu trong DataReader
                //    iNumOfMsg = Convert.ToInt32(dataReader["NumOfMsg"]);

                //    //Dong ket noi 
                //    dataReader.Close();
                //    dataReader.Dispose();
                //}               
            }
            catch //(Exception ex)
            {
                iNumOfMsg= 5; // Mac dinh neu khong lay duoc thi tra ve 5
            }
            return iNumOfMsg;
           
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : getFileNumber() 
         * Muc dich         : Lay FileNumber cho dien SWIFT Export
         * Tra ve           : Mot so kieu int 
         * Ngay tao         : 08/04/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham tao so FileNumber
        public int getFileNumber()
        {
            // Goi ham de lay ngay thang nam theo dinh dang: yyyyMMdd (vi du ngay 08/04/2008: 20080408)
            // Lay mot string theo ngay thang
            DateTime datetime = DateTime.Now;
            string strToday = datetime.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            
            // String Truy van Oracle de lay ra so ref hien tai ung voi ngay Today
            string strCmd = "SELECT VALUE AS FileNumber FROM SYSVAR WHERE VARNAME='SWIFTFILENUM' and GWTYPE='SWIFT' and NOTE='" + strToday + "'";

            // Khoi tao so Ref bang 0
            int FileNumber = 0;
            try
            {

                   DataTable datrbl = new DataTable();

                         datrbl = Select_ReturnDataTable(strCmd);
                         if (datrbl.Rows.Count > 0)
                         {
                             FileNumber = Convert.ToInt32(datrbl.Rows[0]["FileNumber"].ToString());
                         }

                //OracleDataReader dataReader = Select_ReturnReader(strCmd);
                //if (dataReader.HasRows)
                //{
                //    dataReader.Read();

                //    // Doc so Ref tu trong DataReader
                //    FileNumber = Convert.ToInt32(dataReader["FileNumber"]);

                //    //Dong ket noi 
                //    dataReader.Close();
                //    dataReader.Dispose();
                //}
                // Tang so ref len 1 so voi so Ref da ton tai truoc do
                FileNumber++;
                // Update so Ref vao trong bang Constant
                ExecuteNonQuery("UPDATE SYSVAR SET Value='" + FileNumber.ToString() + "', NOTE='" + strToday + "' WHERE VARNAME='SWIFTFILENUM' and GWTYPE='SWIFT' ");
            }
            catch //(Exception ex)
            {
                // Qua trinh lay so Ref xay ra loi
                throw new Exception("Khong lay duoc value cua SWIFTFILENUM trong SYSVAR");
            }
            return FileNumber;                       
        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    FILE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*---------------------------------------------------------------
         * Method           : getFolder() 
         * Muc dich         : Lay path Folder tu truong IMPORT_FOLDER cua bang TAD voi ma Bank_Code
         * Tham so          : Khong co tham so
         * Tra ve           : List<string> - Danh sach String
         * Ngay tao         : 05/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 09/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham lay danh sach cac Folder IBPSConvertIn tu bang CSDL TAD
        private string getFolder()
        {
            string pathFolder = null;
            try
            {
                // Select Export folder
                string strCmd = "SELECT Folder From GWTYPE_DETAIL Where GWTYPE='SWIFT' and DIRECTION='1' and FldTYPE='1' ";

                DataTable datrbl = new DataTable();

                datrbl = Select_ReturnDataTable(strCmd);
                if (datrbl.Rows.Count > 0)
                {
                    pathFolder = datrbl.Rows[0]["Folder"].ToString();
                }
                
                
                //OracleDataReader dtReader = Select_ReturnReader(strCmd);
                //if (dtReader != null && dtReader.HasRows)
                //{
                //    while (dtReader.Read())
                //    {
                //        pathFolder = dtReader["Folder"].ToString();                       
                //    }
                //    dtReader.Close();
                //    dtReader.Dispose();                   
                //}



                // Neu khong co du lieu tra ve null
                if (String.IsNullOrEmpty(pathFolder))
                    return null;
                // Neu co du lieu tra ve True
                pathFolder = Path.GetFullPath(pathFolder);
            }
            catch (Exception ex)
            {                
                throw new Exception("Khong the lay duoc danh sach Export Folder cua SWIFT trong bang GWTYPE. " + ex.Message);
            }
            return pathFolder;
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : getFileName() 
         * Muc dich         : Ham tao ten file
         * Tham so          : Khong co tham so
         * Tra ve           : mot ten file kieu string
         * Ngay tao         : 05/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 03/05/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Lay dang ten File de co the Export

        /*----------------------------
         * Lay dang ten File de co the Export
         */
        public string getFileName()
        {
            long currFileNumber = getFileNumber();
            string szBulkID;
            DateTime datetime = DateTime.Now;
            string fileName = "";
            if (currFileNumber < 10000)
                szBulkID = String.Format("{0:00000}", currFileNumber);
            else
                szBulkID = currFileNumber.ToString();
            fileName = "M" + datetime.ToString("ddMMyy", CultureInfo.CreateSpecificCulture("en-US")) + "." + szBulkID;

            return fileName;
        }

        #endregion


        /*---------------------------------------------------------------
         * Method: WriteLogFile 
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
        public bool ExportFile(string DirPath, string FileName, string content)
        {
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    WriteLogDB(LOG_Error, "Ten File bi trung lap: " + FileName, Query_ID);
                    return false;
                }
                using (StreamWriter sw = File.CreateText(FullName))
                {
                    sw.Write(content);
                    //sw.WriteLine(content);
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
         * Method           : MoveFile(FileInfo file, string destPath) 
         * Muc dich         : Di chuyen mot file den thu muc khac
         * Tham so          : 
         *                      + FileInfofile (la file can di chuyen)
         *                      + destPath: duong dan cua thu muc chua file chuyen
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region MoveFile(FileInfo file, string destPath)
        public void MoveFile(FileInfo file, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(destPath);
            if (!dir.Exists)  // Neu thu muc chua ton tai thi Create
                dir.Create();
            file.MoveTo(destPath + "\\" + file.Name); // Goi ham di chuyen file den thu muc can chuyen toi
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
        public bool WriteLogFile(int log_Level, string strContent)
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
        public void WriteLogDB(int LogLevel, string Content,long lQueryID)
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
                    parameters[2].Value = "GWSWIFTExport";
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
            catch { }
        }
        #endregion


        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    DATABASE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        // Ham tranh loi truy van Injection SQL
     

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
                // Neu thuc hien thanh cong
                oraConnection.Dispose();
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
         * Tham so          : string cmdText - cau lenh PL/SQL                
         * Tra ve           : void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         *--------------------------------------------------------------*/
        #region void ExecuteNonQuery(string cmdText)
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
                oraConnection.Dispose();
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
        public DataTable ExcuteDataTable(string cmdText)
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


              /*---------------------------------------------------------------
               * * Method           : Select_ReturnDataTable(...) 
         * Muc dich         : Thuc hien truy van Select thi Database Oracle 
         * Tham so          : cmdText kieu string - cau lenh truy van SELECT
         * Tra ve           : DataTable
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 06/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region DataTable Select_ReturnDataTable(string cmdText)
        public DataTable Select_ReturnDataTable(string cmdText)
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
            catch //(Exception ex)
            {
                dtb = null;
            }
            return dtb;
        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    KET NOI DATABASE       
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

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
        public OracleConnection OpenConnect()
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
            catch 
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
        public string getConnString()
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
        public string Decrypt(string toDecrypt)
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
        public string getKeyConfig(string key)
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
