/****************************************************************
 * File: IBPSExport.cs
 * ----------------------------
 * Noi dung: 
 * - Xay dung cac ham xu ly cho Service IBPS_Export_Out
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

namespace BRIBPSExport
{
    public partial class IBPSExport : ServiceBase
    {
        public static bool isStop = false;
        public static bool isEncrypt = false;
        public static string EncryptPath ="";
        public static string DecryptPath = "";
        public static int timeSleep = 120; // Thoi gian tre cho service,mac dinh 200

        #region Cac bien va Hang so
       
        private const string key = "Security2008"; //Encrypt Key 

        private const string MSG_TYPE = "SIBS-IBPS";  // Chieu cua Gateway va kenh thanh toan
        private const string BACKUP_FOLDER = "Backup";  // Thu muc Backup
        private const string ERROR_FOLDER = "ExpError"; // Thu muc Error
        private const string LOG_FOLDER = @"C:\ServiceLog"; // Thu muc chua file ghi log mac dinh la C:\ServiceLog
        private const string LOG_FILENAME = "BRIBPSExport_Service.log"; // File ghi log cho service

        // Cac hang so ghi Log Level
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;
        private long Query_ID;
       // private string mFileName;
        // Ten cua Service
        public static string SERVICE_NAME = "";
        // Duong dan file config - cau hinh Database
        // Khi service chay duoc dat trong C:\WINDOWS\System32
        private string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";

        #endregion

        public IBPSExport()
        {
          InitializeComponent();
          //OnService();
        }

        #region  Cac ham OnStart va OnStop

        protected override void OnStart(string[] args)
        {
            try
            {
                //WriteLogDB(LOG_Info, this.ServiceName + " On Start");
                Thread th = new Thread(new ThreadStart(OnService));
                th.Start();

            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, this.ServiceName + " Err When Onstart service " + ex.Message,1);
                base.OnStart(args);
            }
        }

        protected override void OnStop()
        {
            WriteLogDB(LOG_Info, this.ServiceName + " On Stop",1);
            isStop = true;
        }

        private void OnService()
        {
            while (!isStop)
            {
                // Cac thong tin truoc khi bat dau
                BeforeProcess();
                // Bat dau vong xu li moi
                StartProcess();
                Thread.Sleep(timeSleep);// Dat thoi gian tre
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
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRIBPSExport')";
                datGWType = new DataTable();
                datGWType = Select_ReturnDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();

                // het cap nhat.


                //// Lay cac thong tin ve ENCRYPT
                //strCmd = "SELECT ENCRYPT, ENCRYPTFUNCTION,DECRYPTFUNCTION, FILETIME From GWTYPE WHERE GWTYPE = 'IBPS'";
                //datGWType = new DataTable();
                //datGWType = Select_ReturnDataTable(strCmd);
                //if (datGWType.Rows.Count > 0)
                //{
                //    //timeSleep = Convert.ToInt32(datGWType.Rows[0]["FILETIME"]) ;
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
                WriteLogDB(LOG_Error, " Can not get Encrypt GWTYPE: Err " + ex.Message,1);
                return;
            }
        }

       
        ////// Ham public hien Ma Hoa tu thu vien DLL
        ////private string MakeEncrypt(string input)
        ////{
        ////    string result = "";
        ////    Type theType = null;
        ////    object theClass = null;
        ////    try
        ////    {
        ////        // Load file DLL
        ////        // Get Class & Create Instance

        ////        Assembly a =
        ////           Assembly.LoadFrom("SecurityEncrypt.dll");
        ////        theClass = a.CreateInstance(EncryptPath);
        ////        theType = a.GetType(EncryptPath);
        ////        // with the reference to the dynamically 
        ////        // created class you can invoke the method 
        ////        object[] arguments = new object[1] { input };
        ////        object retVal =
        ////           theType.InvokeMember("Encrypt",
        ////           BindingFlags.Default |
        ////           BindingFlags.InvokeMethod,
        ////           null,
        ////           theClass,
        ////           arguments);
        ////        result = retVal.ToString();

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Console.WriteLine("Khong the ma hoa: {0}", ex.Message);
        ////        result = null;
        ////    }
        ////    if (result == null)
        ////        return input; // Neu ham ma hoa bi loi thi khong ma hoa
        ////    return result;
        ////}

        /*---------------------------------------------------------------
        * Method           : StartProcess() 
        * Muc dich         : Ham goi bat dau phuong thuc xu li Service
        * Tham so          : Khong co tham so
        * Tra ve           : void
        * Ngay tao         : 05/04/2008
        * Nguoi tao        : TrungNV3
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : TrungNV3
        *--------------------------------------------------------------*/
        #region  Ham StartProcess()
        public void StartProcess()
        {
            try
            {
                SERVICE_NAME = this.ServiceName;
                if (!isStop)
                {
                    // Lay danh sach cac dien can xu li tu bang IBPS_Msg_Content
                    DataTable dtbList = new DataTable();
                    dtbList = getMsgList();
                    // Neu danh sach Message la Ro^ng~ hoac Null
                    if (dtbList == null || dtbList.Rows.Count == 0)
                    {
                        return;  // Thoat khoi viec xu li khi danh sach rong
                    }
                    // Neu danh sach co dien thi bat dau xu li
                    
                    else
                    {
                        int MsgCount = dtbList.Rows.Count;
                        for(int i=0;i<MsgCount;i++)
                        {
                            try
                            {
                                Query_ID = Convert.ToInt64(dtbList.Rows[i]["Query_ID"]);
                                long Msg_ID = Convert.ToInt64(dtbList.Rows[i]["MSG_ID"]);
                                int refNum = Convert.ToInt32(dtbList.Rows[i]["GW_TRANS_NUM"]);
                                string bank_code = dtbList.Rows[i]["F21"].ToString();
                                string content = dtbList.Rows[i]["CONTENT"].ToString();
                                string pathFolder = getFolder(bank_code);
                                string FileName = getFileName(refNum);

                                       
                                // Them thu muc ngay thang
                                if (pathFolder != null)
                                {
                                   // pathFolder += "\\" +;
                                    // Dat trang thai Pending
                                    //string cmdUpdatePending = "UPDATE IBPS_Msg_Content SET Status='2' WHERE MSG_ID='" + Msg_ID + "'";
                                    string cmdUpdatePending="GW_PK_SVR_IBPS_EXPORT.UPDATE_STATUS";

                                    OracleParameter[] Param_Pending = {new OracleParameter("pStatus",OracleType.Number,2),
                                                       new OracleParameter("pMSG_ID",OracleType.Number,20),
                                                       new OracleParameter("pType",OracleType.Number,2),
                                                       new OracleParameter("pFilename", OracleType.VarChar,50)};
                                    Param_Pending[0].Value = 2;
                                    Param_Pending[1].Value = Msg_ID;
                                    Param_Pending[2].Value = 2;
                                    Param_Pending[3].Value = "";
                                    ExecuteNonQuery(cmdUpdatePending, Param_Pending);

                                    if (!Check_message_In(Query_ID, "SIBS-IBPS"))
                                    {
                                  
                                        Insert_Index(Query_ID, "SIBS-IBPS");
                                        try
                                        {
                                            // Kiem tra neu nhu ma hoa thi ma hoa truoc khi export
                                            //if (isEncrypt)
                                            //{
                                            //    content = MakeEncrypt(content);
                                            //}

                                            // Export ra file
                                            if (ExportFile(pathFolder, FileName, content))
                                            {
                                                WriteLogDB(LOG_Info, "Finish export file: " + FileName, Query_ID);
                                                // Neu xuat file thanh cong cap nhat trang thai status la 4
                                                //string cmdUpdate = "UPDATE IBPS_Msg_Content SET Status='1', Sending_Time =sysdate , FILE_NAME ='" + FileName + "' WHERE MSG_ID='" + Msg_ID + "'";
                                                string cmdUpdate = "GW_PK_SVR_IBPS_EXPORT.UPDATE_STATUS";
                                                OracleParameter[] Param_Success = {new OracleParameter("pStatus",OracleType.Number,2),
                                                       new OracleParameter("pMSG_ID",OracleType.Number,20),
                                                       new OracleParameter("pType",OracleType.Number,2),
                                                       new OracleParameter("pFilename", OracleType.VarChar,50)};
                                                Param_Success[0].Value = 1;
                                                Param_Success[1].Value = Msg_ID;
                                                Param_Success[2].Value = 1;
                                                Param_Success[3].Value = FileName;
                                                ExecuteNonQuery(cmdUpdate, Param_Success);

                                            }
                                            else
                                            {
                                                WriteLogDB(LOG_Info, "Can not export Message: MSGID = " + Msg_ID, Query_ID);
                                                // Neu file khong the export cap nhat status la 3
                                                //string cmdUpdateEror = "UPDATE IBPS_Msg_Content SET Status='-1',ERR_CODE=6 WHERE MSG_ID='" + Msg_ID + "'";
                                                string cmdUpdateEror = "GW_PK_SVR_IBPS_EXPORT.UPDATE_STATUS";
                                                OracleParameter[] Param_Err = {new OracleParameter("pStatus",OracleType.Number,2),
                                                       new OracleParameter("pMSG_ID",OracleType.Number,20),
                                                       new OracleParameter("pType",OracleType.Number,2),
                                                       new OracleParameter("pFilename", OracleType.VarChar,50)};
                                                Param_Err[0].Value = -1;
                                                Param_Err[1].Value = Msg_ID;
                                                Param_Err[2].Value = -1;
                                                Param_Err[3].Value = "";

                                                ExecuteNonQuery(cmdUpdateEror, Param_Err);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Neu bi loi dat lai trang thai la 0
                                            string cmdUpdateEror = "GW_PK_SVR_IBPS_EXPORT.UPDATE_STATUS";
                                            OracleParameter[] Param_Err = {new OracleParameter("pStatus",OracleType.Number,2),
                                                       new OracleParameter("pMSG_ID",OracleType.Number,20),
                                                       new OracleParameter("pType",OracleType.Number,2),
                                                       new OracleParameter("pFilename", OracleType.VarChar,50)};
                                            Param_Err[0].Value = -1;
                                            Param_Err[1].Value = Msg_ID;
                                            Param_Err[2].Value = -1;
                                            Param_Err[3].Value = "";

                                            ExecuteNonQuery(cmdUpdateEror, Param_Err);
                                        }
                                    }
                                    else
                                    {
                                        WriteLogDB(LOG_Warning, "Dien nay da duoc gui lan thu hai", Query_ID);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Neu xay ra loi voi thu muc nao do, thuc hien ghi log                                
                                WriteLogDB(LOG_Error, "Err When export file: " + ex.Message, Query_ID);
                                continue;
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
                WriteLogDB(LOG_Error, SERVICE_NAME + " Err message: " + ex.Message, Query_ID);
            }
        }
        #endregion

        #region Insert gia tri Index vao bang de
        public int Insert_Index(long LID, string MSG_DIRECTION)
        {           
            try
            {
                string strSQL = "insert into IBPS_SVR_INDEX(MSG_ID,MSG_DIRECTION) values(" + LID + ",'" + MSG_DIRECTION + "')";
                ExecuteNonQuery(strSQL);
                return 1;
            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Error, "Bi loi khi insert du lieu vao bang IBPS_SVR_INDEX" + ex.Message, Query_ID);
                return -1;
            }
        }
        #endregion

        #region Kiem tra xem dien da tung duoc export hay chua
        public bool Check_message_In(long LID, string MSG_DIRECTION)
        {
            bool bBool = true;
            try
            {
                DataTable datIndex = new DataTable();
                string strSQL = "select MSG_ID from IBPS_SVR_INDEX IB where IB.MSG_ID= " + LID + " and IB.MSG_DIRECTION = '" + MSG_DIRECTION + "' ";
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
                WriteLogDB(LOG_Error, "Bi loi khi" + ex.Message, Query_ID);
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
         * Muc dich         : Lay danh sach cac IBPS-Message tu bang IBPS_Msg_Content
         * Tham so          : Khong co tham so
         * Tra ve           : DataTable 
         * Ngay tao         : 02/05/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 02/05/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham lay danh sach cac Message tu bang CSDL IBPS_Msg_Content
        private DataTable getMsgList()
        {
            DataTable dtbResult = new DataTable();
            try
            {
             //string strCmd = "   SELECT IBPSMSG.MSG_ID,IBPSMSG.GW_TRANS_NUM, IBPSMSG.TAD,   IBPSMSG.CONTENT,       IBPSMSG.QUERY_ID, trans_date ";
             //   strCmd = strCmd + " From IBPS_Msg_Content IBPSMSG WHERE MSG_DIRECTION = 'SIBS-IBPS'   and Status = '0' ";
             //   strCmd = strCmd + "  And Err_Code = 0   and RowNum <= 100   And TRANSDATE =";
             //   strCmd = strCmd + " to_char(sysdate, 'YYYYMMDD')   And LPAD(IBPSMSG.TAD, 5, '0') in       (select LPAD(IBM.SIBS_BANK_CODE, 5, 0) ";
             //   strCmd = strCmd + " from IBPS_BANK_MAP IBM         where IBM.GW_BANK_CODE in (select TAD.GW_BANK_CODE from TAD where TAD.Connection = 3 and status=1 )) ";

                string strcmd = "GW_PK_SVR_IBPS_EXPORT.GET_IBPS_MSG_LIST";
                OracleParameter[] pram = { new OracleParameter("curReturn", OracleType.Cursor) };
                pram[0].Direction = ParameterDirection.Output;

                dtbResult = ExcuteDataTable(strcmd, CommandType.StoredProcedure, pram);
                return dtbResult;

            }
            catch (Exception ex)
            {
                return null;
            } 
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
        private string getFolder(string Bank_Code)
        {
            string pathFolder = null;
            try
            {
                //Chu y: Thu muc IMPORT trong TAD la thu muc GW Export ra
                string strCmd = "SELECT IMPORT_FOLDER From TAD WHERE GW_Bank_Code = '" + Bank_Code + "' and (Status ='1')";
                DataTable datTbl = new DataTable();
                datTbl = Select_ReturnDataTable(strCmd);

                if (datTbl.Rows.Count>0)
                {
                    string str_temp = datTbl.Rows[0]["IMPORT_FOLDER"].ToString();
                    datTbl.Clear();
                    return str_temp;
                }
               

            }
            catch (Exception ex)
            {
         //      WriteLogDB(LOG_Error, "Can not get IMPORT_FOLDER From TAD  BankCode = " + Bank_Code,1);
                throw ex;
            }
            return pathFolder;
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : getFileName(long refNum) 
         * Muc dich         : Xay dung Ten file de Export dua vao so Ref va ngay thang nam
         * Tham so          : So refNum kieu long
         * Tra ve           : Mot string Ten File
         * Ngay tao         : 05/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 02/05/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham xay dung Ten file de Export
        /*----------------------------
         * Lay dang ten File theo ngay gio hien tai va so Ref: ******dd.MMy
         */
        public static string getFileName(long refNum)
        {
            string strFile;
            DateTime datetime = DateTime.Now;
            string fileName = "";
            string ref_number = String.Format("{0:000000}", refNum);
            strFile = DateTime.Now.Year.ToString().Substring(3);
            fileName = ref_number + datetime.ToString("dd.MM" + strFile, CultureInfo.CreateSpecificCulture("en-US"));

            return fileName;
        }

        #endregion


        /*----------------------------
         * Lay ten thu muc theo ngay gio hien tai dang NamThangNgay: 20080415
         */
        public static string dateFolderName
        {
            get
            {
                DateTime datetime = DateTime.Now;
                return datetime.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));

            }
        }
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
        public bool ExportFile(string DirPath, string FileName, string content)
        {
            // DirPath += "\\" + dateFolderName;

            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    WriteLogDB(1, "File is Duplicate: " + FileName, Query_ID);
                    CheckFileExists(file, DirPath, FileName);

                }

                //Thu 

                FileStream ostrm = new FileStream(FullName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(ostrm, Encoding.Default);
                sw.WriteLine(content);
                sw.Close();
                return true;
                //Het thu

                //using (StreamWriter sw = File.CreateText(FullName))
                // {

                //     sw.WriteLine(content);
                //     sw.Close();
                //     return true;
                // }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : CheckFileExists(string destPath) 
         * Muc dich         : Kiem tra xem File co ton tai khong neu co thi Rename di
         * Tham so          : 
         *                      + FileInfofile (la file can di chuyen)
         *                      + destPath: duong dan cua thu muc chua file chuyen
         * Tra ve           : void
         * Ngay tao         : 11/08/2008
         * Nguoi tao        : QuanLD
         * Ngay cap nhat    : 11/08/2008
         * Nguoi cap nhat   : Quanld
         *--------------------------------------------------------------*/
        #region CheckFileExists( string destPath)
        public void CheckFileExists(FileInfo file, string destPath, string strFilename)
        {
            bool isExists = false;
            string strName;
            //FileInfo file = new FileInfo(destPath + "\\" + strFilename);
            strName = strFilename;
            int increate = 0;
            int iLen = 0;
            iLen = strName.Length;
            while (!isExists)
            {
                if (!File.Exists(destPath + "\\" + strName))
                {
                    isExists = true;
                    file.MoveTo(destPath + "\\" + strName);
                    break;
                }
                else
                {
                    increate = increate + 1;
                    strName = strFilename + "." + increate.ToString();
                }
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
               // WriteLogDB(log_Level, strContent);
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
        public void WriteLogDB(int LogLevel, string Content, long lQueryID)
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
                    new OracleParameter("pSERVICE",OracleType.VarChar,50),
                    new OracleParameter("pDESCRIPTIONS", OracleType.VarChar,2000 ),
                    new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,20),
                    new OracleParameter("pSTATUS",OracleType.Number,2),
                };

                    parameters[0].Value = DateTime.Now;
                    parameters[1].Value = lQueryID;
                    parameters[2].Value = SERVICE_NAME;
                    parameters[3].Value = Content;
                    parameters[4].Value = "SIBS-IBPS";
                    parameters[5].Value = LogLevel;
                    string strSQL = "insert into IBPS_SVR_LOG (Log_Date,QUERY_ID,STATUS,DESCRIPTIONS,SERVICE,MSG_DIRECTION)"
                                  + " values (:pLogDate,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS,:pSERVICE,:pMSG_DIRECTION)";



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
        #region Hàm tránh lỗi Injection Query
        // Tranh loi Injection SQL
        public string SafeQuery(string inputQuery)
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
                throw ex;
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
        #region DataTable SelectQueryCmd(string cmdText)
        public DataTable SelectQueryCmd(string cmdText)
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

        #region DataTable Select_ReturnDataTable(string cmdText)
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


        #region DataTable ExcuteDataTable(string cmdText, CommandType cmdType, params OracleParameter[] para)
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

        public DataTable ExcuteDataTable(string cmdText, CommandType cmdType, params OracleParameter[] para)
        {
            // Tao mot DataTable moi
            DataTable tblValue = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter();

            // Khoi tao ket noi moi
            OracleConnection oraConnection = OpenConnect();

            OracleCommand oraCommand = new OracleCommand(cmdText, oraConnection);
            oraCommand.CommandType = cmdType;
            try
            {

                if (para != null)
                    oraCommand.Parameters.AddRange(para);
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
            catch //(Exception ex)
            {
                WriteLogFile(3,"Connect database failed: ConnectionString is " + connection.ConnectionString);
               // throw new Exception("Khong ket noi duoc CSDL: " + ex.Message);
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

                throw new Exception("Can not find Config: " + strConfigFilePath + ex.Message);
            }
            // retrieve ServicesConfig node
            XmlNode node = doc.SelectSingleNode("//DatabaseConfig");
            if (node == null)
            {
                throw new InvalidOperationException("Can not Field DatabaseConfig in file config.");
            }
            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem == null)
                {
                    // add value for key
                    throw new Exception("Can not key " + key + " in file service config.");
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
               // WriteLogDB(LOG_Error, "Can not key " + key + " in file service config.");
                throw new Exception("Can not key " + key + " in file service config.");

            }
            return result;
        }
        #endregion

        // End class

    }
}
