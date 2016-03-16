/****************************************************************
 * File: SWIFTImport.cs
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
using System.Reflection;
#endregion

namespace GWSWIFTImport
{
    public partial class SWIFTImport : ServiceBase
    {
        // Kiem soat hoat dong cho service
        public static bool isStop = false;
        // Cac thong so ma hoa
        public static bool isEncrypt = false;
        public static string EncryptPath = "";
        public static string DecryptPath = "";
        private const string key = "Security2008"; //Encrypt Key 
        // Thoi gian tre
        public static int timeSleep = 200; // Thoi gian tre cho service

        #region Cac bien va Hang so

        private static string SERVICE_NAME = "";
        private const string GW_TYPE = "SWIFT";
        private const string MSG_TYPE = "SWIFT-SIBS";
        private const string LOG_FOLDER = @"C:\ServiceLog"; // Co the thay doi
        private const string LOG_FILENAME = "SWIFTImport_Service.log";
        
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;

        // Danh sach cac Folder duoc chua trong mot Dictionary<Loai_Folder, Gia_Tri>
        private Dictionary<string, string> Folders = new Dictionary<string, string>();

        // Duong dan file config - cau hinh Database
        private string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";
   
        #endregion
        public SWIFTImport()
        {            
            InitializeComponent();
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
                WriteLogDB(LOG_Info, this.ServiceName + " onStart error: " + ex.Message,1);
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
                // Lay cac thong so truoc khi xu li
                BeforeProcess();
                    
                // Bat dau mot vong xu li moi
                StartProcess();
                Thread.Sleep(timeSleep);
            }
        }


        #endregion


        /*------------------------------------------------------
         * GATEWAY CODE HERE - SWIFT ImportIn Service
         * ----------------------------------------------------*/


        // Ham kiem tra de lay cac tham so ve ma hoa ENCRYPT
        private void BeforeProcess()
        {
            try
            {

                DataTable datGWType = new DataTable();



                // cap nhat moi 03/12/2008  lay thoi gian delay cua service tu bang GWService_port.
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRSWIFTImport')";
                datGWType = new DataTable();
                datGWType = ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();

                // het cap nhat.


                //// Lay cac thong tin ve ENCRYPT
                //strCmd = "SELECT ENCRYPT, ENCRYPTFUNCTION,DECRYPTFUNCTION, FILETIME From GWTYPE WHERE GWTYPE = 'SWIFT'";

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
                WriteLogDB(LOG_Error, " Get info GWTYPE  failed" + ex.Message,1);
                return;
            }
        }
        

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    MESSAGE PROCESS          
         * 
         *+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*---------------------------------------------------------------
        * Method           : StartProcess() 
        * Muc dich         : Xu li cac file trong Folder Import cua SWIFT
        * Tham so          : Khong co tham so
        * Tra ve           : void
        * Ngay tao         : 05/04/2008
        * Nguoi tao        : TrungNV3
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : TrungNV3
        *--------------------------------------------------------------*/
        #region Ham bat dau xu li - StartProcess() 
        public void StartProcess()
        {

            try
            {
                string nn = Convert.ToChar(125).ToString() + Convert.ToChar(3).ToString();
                string bbb= Convert.ToChar(1).ToString() + Convert.ToChar(123).ToString();
                if (!getFolderList())
                {
                    WriteLogDB(LOG_Error, SERVICE_NAME + " Get info GWTYPE Failed", 1);
                    return;
                }

                if (!isStop)  // Lay path cua cac thu muc Import, Backup, Error
                {
                    string ImportFolder = Path.GetFullPath(Folders["Import"]);
                    DirectoryInfo dir = new DirectoryInfo(ImportFolder);
                    // Danh sach cac file co trong thu muc Import
                    List<FileInfo> list = new List<FileInfo>();
                    list.Clear();

                    // Quet cac file co trong thu muc Import Folder
                    list = ScanFile(ImportFolder);

                    if (list == null || list.Count == 0)
                    {

                        return;
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        FileInfo file = list[i];
                        try
                        {

                            string FileName = file.Name;


                            // Mo noi dung file Text cua dien 
                            string MsgContent = "";
                            MsgContent = OpenFile(file.FullName);
                            //if (isEncrypt == true)
                            //{
                            //    MsgContent = MakeDecrypt(MsgContent);
                            //}
                            //// Neu noi dung dien bi rong hoac file bi loi

                            if (string.IsNullOrEmpty(MsgContent))
                            {
                                // Chuyen file vao thu muc loi - Error   
                                WriteLogDB(LOG_Info, "File is empty: " + file.FullName, 1);

                                MoveFile(file, Folders["Error"]);

                                // Thoat khoi vong lap de xu li file khac
                                continue;
                            }

                            // Neu noi dung dien ton tai thi thi tiep tuc xu li
                            else
                            {
                                //Chieu dai cua dien SWIFT
                                int msgLength = MsgContent.Length;
                                // Vi tri bat dau cua dien SWIFT
                                string beginMsg = "${";// Convert.ToChar(1).ToString() + Convert.ToChar(123).ToString();

                                // Vi tri ket thuc cua dien SWIFT
                                string endMsg = "}$";//Convert.ToChar(125).ToString() + Convert.ToChar(3).ToString();

                                int countMsg = 0; // Bien dem so dien trong file
                                int indexPos = 0;// Bien luu vi tri
                                bool isError = false; // Bien luu trang thai file dien co bi loi hay khong

                                // Tach dien lan luot tung dien SWIFT trong noi dung file dien
                                countMsg = 0;
                                while (indexPos != -1 && indexPos < msgLength)
                                {
                                    countMsg++; // Tang bien dem so dien len 1
                                    int newPos;
                                    if (countMsg == 1)
                                    { newPos = 0; }
                                    else
                                    {
                                        newPos = MsgContent.IndexOf(beginMsg, indexPos) + 1;
                                    }


                                    // Neu khong tim thay vi tri bat dau dien moi thi ket thuc
                                    if (newPos == -1) break;
                                    int newEndPos = MsgContent.IndexOf(endMsg, newPos);
                                    // Neu khong tim thay vi tri ket thuc dien moi thi ket thuc


                                    //cap nhat cho MSB

                                    string content;
                                    if (newEndPos == -1 && msgLength > 0)
                                    {
                                        newEndPos = msgLength;
                                        content = MsgContent.Substring(newPos);
                                    }
                                    else
                                    {
                                        content = MsgContent.Substring(newPos, newEndPos - newPos + 1);
                                    }

                                    // Het cap nhat


                                    if (newEndPos == -1) break;
                                    // Gan vi tri dien moi
                                    indexPos = newEndPos;
                                    // Tao Parameter va Chuan hoa String Query tranh loi Injection SQL
                                    // string content = MsgContent.Substring(newPos, newEndPos - newPos + 2);

                                    //  string content = MsgContent.Substring(newPos, newEndPos - newPos + 1);



                                    //Test ghi tung dien ra file de kiem tra
                                    //ExportFile(@"C:\", countMsg.ToString() + ".txt", content);

                                    try
                                    {
                                        OracleParameter[] parameters = new OracleParameter[]{
                                                    new OracleParameter("pFILE_NAME",FileName),
                                                    new OracleParameter("pMSG_TYPE",MSG_TYPE),
                                                    new OracleParameter("pCONTENT",(content.Trim())),                                                    
                                                    new OracleParameter("pSYS_DATE",OracleType.DateTime)
                                                    }; // Tao moi sanh sach cac Parameters
                                        parameters[3].Value = DateTime.Now;
                                        ExecuteNonQuery("GW_PK_SWIFT_Q_CONVERTIN.SWIFT_EN_CONVERT_IN", parameters);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            OracleParameter[] parameters1 = new OracleParameter[]{
                                                    new OracleParameter("pFILE_NAME",FileName),
                                                    new OracleParameter("pMSG_TYPE",MSG_TYPE),
                                                    new OracleParameter("pCONTENT",(content.Trim())),                                                    
                                                    new OracleParameter("pSYS_DATE",OracleType.DateTime)
                                                    }; // Tao moi sanh sach cac Parameters
                                            parameters1[3].Value = DateTime.Now;
                                            ExecuteNonQuery("GW_PK_SWIFT_Q_CONVERTIN.SWIFT_EN_CONVERT_IN", parameters1);
                                        }
                                        catch (Exception ex1)
                                        {
                                            //throw ex;
                                            isError = true;
                                            WriteLogDB(LOG_Error, "Message nonum " + countMsg + " in file " + FileName + " Err: " + ex1.Message + "Failed." + content.Substring(1, 200), 1);
                                            continue;
                                        }
                                    }
                                    // Gan gia tri bien vi tri start moi

                                }
                                file.Refresh();

                                if (isError) // Neu co mot dien loi trong file thi chuyen sang thu muc Error
                                    MoveFile(file, Path.GetFullPath(Folders["Error"]));

                                else // Xu li file thanh cong va Backup file vao thu muc
                                {
                                    BackupFile(file);
                                    //WriteLogDB(LOG_Info, " backup file " + file.Name + " in SWIFT_Import_IN and Backup file success");
                                }
                                // Ghi log sau khi xu li file thanh cong
                                //WriteLog(DateTime.Now, "Da luu noi dung file " + file.Name + " vao bang IBPS_MSG_Content va Backup file", log_level);
                            }

                        }
                        catch (Exception ex)
                        {
                            // Thuc hien viec ghi Log khi file bi loi
                            WriteLogDB(LOG_Info, "Can not process file: " + file.Name + " Err: " + ex.Message + ", Err: " + ex.Message, 1);
                            continue;        // Neu bi loi thoat khoi vong lap de xu li file khac        
                        }

                    }

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Info, "Err when  Import " + ex.Message, 1);
                return;
            }

        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    FILE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*---------------------------------------------------------------
         * Method           : getFolderList() 
         * Muc dich         : Lay danh sach Folder tu truong EXPORT_FOLDER cua bang TAD
         * Tham so          : Khong co tham so
         * Tra ve           : List<string> - Danh sach String
         * Ngay tao         : 05/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 09/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham lay danh sach cac Folder Import,Backup,Error tu bang GWTYPE 
        private bool getFolderList()
        {
            Folders.Clear(); // Xoa danh sach file de lay lai
            try
            {
                // Select theo thu tu uu tien PRIORITY
                string strCmd = "SELECT Distinct Folder,FldType From GWTYPE_DETAIL Where GWTYPE='SWIFT' and DIRECTION='2' ";
                DataTable datTable = new DataTable();
                datTable = ExcuteDataTable(strCmd);
                if (datTable.Rows.Count > 0)

                {
                    try
                    {
                        for (int i = 0; i < datTable.Rows.Count; i++)
                        {
                            if (Convert.ToInt16(datTable.Rows[i]["FldType"]) == 1)
                                Folders.Add("Import", datTable.Rows[i]["Folder"].ToString());
                            // Neu truong FldTYPE la 1 tuc la thu muc la Backup
                            if (Convert.ToInt16(datTable.Rows[i]["FldType"]) == 2)
                                Folders.Add("Backup", datTable.Rows[i]["Folder"].ToString());
                            // Neu truong FldTYPE la 1 tuc la thu muc la Error
                            if (Convert.ToInt16(datTable.Rows[i]["FldType"]) == 3)
                                Folders.Add("Error", datTable.Rows[i]["Folder"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLogDB(LOG_Info, "Get path failed: " + ex.Message,1);
                    }
                    datTable.Clear();
                    return true;
                } 
                else
                {
                    datTable.Clear();
                    return false;}
                    
          }
            catch (Exception ex)
            {
                WriteLogDB(LOG_Info, "Get path failed: " + ex.Message,1);
                
                return false;
            }
        }
        #endregion



        /*---------------------------------------------------------------
         * Method           : scanFile(string DirPath) 
         * Muc dich         : Lay mot danh sach file trong thu muc theo dinh dang
         * Tham so          : + DirPath: duong dan cua thu muc
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Doc mot danh sach file tu Folder - ScanFile(string DirPath)
        public List<FileInfo> ScanFile(string DirPath)
        {
            List<FileInfo> list = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(DirPath));
            try
            {
                // Scan file tu thu muc, voi cac file co dinh dang lay 
                // tu dateFileFormat trong lop Constant
                FileInfo[] files = dir.GetFiles();
                list.AddRange(files);
            }
            catch //(Exception ex)
            {
                list = null;
                //new DatabaseProcess().WriteLog(DateTime.Now, Constant.SERVICE_NAME, "Co loi khi scan file trong thu muc " + dir.FullName, Constant.LEVEL_ERROR_SCAN_FILE);
            }
            Thread.Sleep(1000);
            return list;
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
        #region Di chuyen mot file sang thu muc khac - MoveFile
        public void MoveFile(FileInfo file, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(destPath);
            if (!dir.Exists)  // Neu thu muc chua ton tai thi Create
                dir.Create();
            CheckFileExists(file,destPath, file.Name);
            file.MoveTo(destPath + "\\" + file.Name); // Goi ham di chuyen file den thu muc can chuyen toi
        }
        #endregion

        /*---------------------------------------------------------------
         * Method           : BackupFile 
         * Muc dich         : Backup file sang mot thuc muc Backup
         * Tham so          : FileInfo file: file can Backup
         * Tra ve           : Tra ve kieu void
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham Backup mot file - BackupFile(FileInfo file)
        public void BackupFile(FileInfo file)
        {
            try
            {
                // Khoi tao thu muc Backup
                // Constant.BACKUP_FOLDER: la ten thu muc Backup duoc dinh nghia trong lop Const

                string backupFolder ;
                try
                {
                }
                catch
                {
                    backupFolder = "C:\\Swiftbackup";
                }
                backupFolder = Path.GetFullPath(Folders["Backup"]);
                DirectoryInfo DirBackup = new DirectoryInfo(backupFolder);

                if (!DirBackup.Exists)   // Neu thu muc chua ton tai thi Create
                    DirBackup.Create();

                // Goi ham Move file den thu muc Backup
                CheckFileExists(file,DirBackup.FullName, file.Name);
                file.MoveTo(DirBackup.FullName + "\\" + file.Name);
            }
            catch
            {
                WriteLogDB(LOG_Info, "Backup file failed " + file.FullName,1);
            }
        }
        #endregion

        /*---------------------------------------------------------------
         * Method           : OpenFile(string pathFie) 
         * Muc dich         : Open mot file Text
         * Tham so          : string pathFie - Duong dan cua file
         * Tra ve           : Tra ve noi dung cua File kieu String
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : TrungNV3
         * Ngay cap nhat    : 08/04/2008
         * Nguoi cap nhat   : TrungNV3
         *--------------------------------------------------------------*/
        #region Ham Open file Text : OpenFile(string pathFile)
        public string OpenFile(string pathFile)
        {
            string result = String.Empty;
            try
            {
                StreamReader sr = new StreamReader(pathFile, Encoding.Default);
                // Doc tu dau den het file, tra lai kieu String
                result = sr.ReadToEnd();
                sr.Close();  // Dong lai stream reader file              

                // Mot cach doc file khac, co the khong dung den
                //FileStream fs = File.OpenRead(pathFie);
                //byte[] data = new byte[fs.Length];
                //fs.Read(data, 0, data.Length);
                //result = Encoding.Default.GetString(data, 0, data.Length);
                //fs.Close();                
            }
            catch
            {
                // Neu viec doc file bi loi thi tra ve mot xau rong StringEmpty
                result = String.Empty;
            }
            return result;
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
                    WriteLogDB(LOG_Error, "File is duplicate: " + FileName,1);
                    return false;
                }
                using (StreamWriter sw = File.CreateText(FullName))
                {
                    sw.WriteLine(content);
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
                    parameters[2].Value = "BRSWIFTImport";
                    parameters[3].Value = Content;
                    parameters[4].Value = "SWIFT-SIBS";
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
            catch
            { }




        }
        #endregion

        /*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         * 
         *    DATABASE PROCESS          
         * 
         * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            

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

       // Ham public hien Giai Ma Hoa tu thu vien DLL
        //private string MakeDecrypt(string input)
        //{
        //    string result = "";
        //    Type theType = null;
        //    object theClass = null;
        //    try
        //    {
        //        // Load file DLL
        //        // Get Class & Create Instance
        //        Assembly a =
        //            Assembly.LoadFrom("SecurityEncrypt.dll");
        //        theClass = a.CreateInstance(DecryptPath);
        //        theType = a.GetType(DecryptPath);
        //        // with the reference to the dynamically 
        //        // created class you can invoke the method 
        //        object[] arguments = new object[1] { input };
        //        object retVal =
        //           theType.InvokeMember("Decrypt",
        //           BindingFlags.Default |
        //           BindingFlags.InvokeMethod,
        //           null,
        //           theClass,
        //           arguments);
        //        result = retVal.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Decrypt failed: {0}", ex.Message);
        //        result = null;
        //    }
        //    if (result == null)
        //        return input; // Neu ham ma hoa bi loi thi khong ma hoa
        //    return result;
        //}

      

     

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
                throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");

            }
            return result;
        }
        #endregion

        // End class
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
        public void CheckFileExists( FileInfo file,string destPath, string strFilename)
        {
            bool isExists = false;
            string strName;
           // FileInfo file = new FileInfo(destPath + "\\" + strFilename);
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

    }
}
