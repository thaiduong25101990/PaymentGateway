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
namespace BRReconcileIN
{
    public partial class ReconcileIN : ServiceBase
    {
        private string strSwiftPath;
        private string strVCBPath;
        private string SERVICE_NAME;
        bool isStop = false;

        public ReconcileIN()
        {
            InitializeComponent();
           // OnService();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                SERVICE_NAME = this.ServiceName;
                //WriteLogDB(LOG_Info, this.ServiceName + " On Start", 1);
                Thread th = new Thread(new ThreadStart(OnService));
                th.Start();

            }
            catch //(Exception ex)
            {
                // WriteLogDB(LOG_Info, this.ServiceName + " onStart error: " + ex.Message, 1);
                base.OnStart(args);
            }
        }

        protected override void OnStop()
        {
            isStop = false;
        }


        private void OnService()
        {
            SERVICE_NAME = this.ServiceName;
            while (!isStop)
            {
                // Lay cac thong so truoc khi xu li
                GetFolderImport();
                // Bat dau mot vong xu li moi
                SWIFT_REC_IN_Process(strSwiftPath);
                VCB_REC_IN_Process(strVCBPath);
                Thread.Sleep(1000);
            }
        }


        /*---------------------------------------------------------------
  * Method           : SWIFT_REC_IN_Process() 
  * Muc dich         : Xu li cac file trong Folder Import cua SWIFT
  * Tham so          : Khong co tham so
  * Tra ve           : void
  * Ngay tao         : 08/09/2008
  * Nguoi tao        : QuanLD
  * Ngay cap nhat    : 08/09/2008
  * Nguoi cap nhat   : QuanLD
  *--------------------------------------------------------------*/
        #region Ham bat dau xu li - StartProcess()
        public void SWIFT_REC_IN_Process(string strFolderIN)
        {
            int countMsg=0;
            string FileName;
            try
            {
                string ImportFolder = Path.GetFullPath(strFolderIN);
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
                    FileName = file.Name;
                    try
                    {
                        // Neu lay duoc so Ref thi tiep tuc
                         FileName = file.Name;
                        // Mo noi dung file Text cua dien 
                        string MsgContent = "";
                        MsgContent = OpenFile(file.FullName);
                        // Neu noi dung dien bi rong hoac file bi loi
                        if (string.IsNullOrEmpty(MsgContent))
                        {
                            // Chuyen file vao thu muc loi - Error   
                            //WriteLogDB(LOG_Info, "File is empty: " + file.FullName, 1);

                            continue;
                        }

                        // Neu noi dung dien ton tai thi thi tiep tuc xu li
                        else
                        {
                            ////Chieu dai cua dien SWIFT
                            //    int msgLength = MsgContent.Length;
                            //    // Vi tri bat dau cua dien SWIFT
                            //    string beginMsg = "${";// Convert.ToChar(1).ToString() + Convert.ToChar(123).ToString();

                            //    // Vi tri ket thuc cua dien SWIFT
                            //    string endMsg = "}$";//Convert.ToChar(125).ToString() + Convert.ToChar(3).ToString();

                            //    int countMsg = 0; // Bien dem so dien trong file
                            //    int indexPos = 0;// Bien luu vi tri
                            //    bool isError = false; // Bien luu trang thai file dien co bi loi hay khong

                            //    // Tach dien lan luot tung dien SWIFT trong noi dung file dien
                            //    countMsg = 0;

                            //Chieu dai cua dien SWIFT
                            int msgLength = MsgContent.Length;
                            // Vi tri bat dau cua dien SWIFT
                            string beginMsg = "${";// Convert.ToChar(1).ToString() + Convert.ToChar(123).ToString();

                            // Vi tri ket thuc cua dien SWIFT
                            string endMsg = "}$";//Convert.ToChar(125).ToString() + Convert.ToChar(3).ToString();

                             countMsg = 0; // Bien dem so dien trong file
                            int indexPos = 0;// Bien luu vi tri
                            bool isError = false; // Bien luu trang thai file dien co bi loi hay khong

                            // Tach dien lan luot tung dien SWIFT trong noi dung file dien
                            countMsg = 0;

                            // Tach dien lan luot tung dien SWIFT trong noi dung file dien
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
                                                    new OracleParameter("pFile_name",FileName),
                                                    new OracleParameter("pGWTYPE","SWIFT"),
                                                    new OracleParameter("pMessage_text",content),                                                    
                                                    new OracleParameter("pSysdate",OracleType.DateTime)
                                                    }; // Tao moi sanh sach cac Parameters
                                    parameters[3].Value = DateTime.Now;
                                    DataAcess.ExecuteNonQuery(CommandType.StoredProcedure, "GW_PK_RECONCILE.GW_EQ_REC_IN", parameters);
                                }
                                catch //(Exception ex)
                                {
                                    //throw ex;
                                    isError = true;
                                    DataAcess.WriteLogDB(FileName, "Loi khi dang doc den dong: " + countMsg + " trong file " + FileName );
                                    continue;
                                }
                                // Gan gia tri bien vi tri start moi

                            }
                            file.Refresh();
                            BackupFile(file, ImportFolder + "\\BACKUP");
                        }
                    }
                    catch //(Exception ex)
                    {
                        DataAcess.WriteLogDB(FileName, "Loi khi dang doc den dong: " + countMsg + " trong file " + FileName);
                       continue;        // Neu bi loi thoat khoi vong lap de xu li file khac        
                    }

                }

            }
            catch //(Exception ex)
            {               
                return;
            }

        }
        #endregion


        /*---------------------------------------------------------------
  * Method           : VCB_REC_IN_Process() 
  * Muc dich         : Xu li cac file trong Folder Import cua VCB danh cho doi chieu
  * Tham so          : Khong co tham so
  * Tra ve           : void
  * Ngay tao         : 08/09/2008
  * Nguoi tao        : QuanLD
  * Ngay cap nhat    : 08/09/2008
  * Nguoi cap nhat   : QuanLD
  *--------------------------------------------------------------*/
        #region Ham bat dau xu li - StartProcess()
        public void VCB_REC_IN_Process(string strFolderIN)
        {

            try
            {
                string FileName = "";
                string ImportFolder = Path.GetFullPath(strFolderIN);
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
                        // Neu lay duoc so Ref thi tiep tuc
                         FileName = file.Name;
                        // Mo noi dung file Text cua dien 
                        ReadVCBReconcileFile(file, FileName);
                     }
                    catch //(Exception ex)
                    {
                        DataAcess.WriteLogDB(FileName, "Loi khi dang doc file " + FileName);
                        continue;        // Neu bi loi thoat khoi vong lap de xu li file khac        
                    }
                    BackupFile(file, ImportFolder + "\\BACKUP");
                }
            }
            catch //(Exception ex)
            {
                return;
            }

        }
        #endregion


        private void ReadVCBReconcileFile(FileInfo file, string FileName)
        {
            //string strFile = "";
            //string strResult = "";
            string strPath = "";
            string strFileBlock = "";
            Int64 i = 0;
            string strMessage = "";
            DateTime dsysdate;
            /* Lay ngay nguoi su dung chon */

            /* Search File To Scan */
            dsysdate = DateTime.Now;
            try
            {
                strPath = file.FullName;
                /* Read File*/
                StreamReader sr = new StreamReader(strPath, Encoding.Default);
                while ((strFileBlock = sr.ReadLine()) != null)
                {
                    try
                    {
                        i = i + 1;
                        /* filter "GWTYPR" */
                        strMessage = strFileBlock.Trim();


                        OracleParameter[] parameters = new OracleParameter[]{
                                                    new OracleParameter("pFile_name",FileName),
                                                    new OracleParameter("pGWTYPE","VCB"),
                                                    new OracleParameter("pMessage_text",strMessage),                                                    
                                                    new OracleParameter("pSysdate",OracleType.DateTime)
                                                    }; // Tao moi sanh sach cac Parameters
                        parameters[3].Value = DateTime.Now;
                        DataAcess.ExecuteNonQuery(CommandType.StoredProcedure, "GW_PK_RECONCILE.GW_EQ_REC_IN", parameters);
                    }
                    catch {
                        DataAcess.WriteLogDB(FileName, "Loi khi dang doc file " + FileName + " dong thu " + i);
                        continue;
                    }
                }
                //strResult = sr.ReadToEnd();
                sr.Close();  // Close(file);

            }
            catch //(Exception ex)
            {
            }
        }


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
            return list;
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
        public  void BackupFile(FileInfo file, string folder)
        {
            try
            {
                // Khoi tao thu muc Backup
                // Constant.BACKUP_FOLDER: la ten thu muc Backup duoc dinh nghia trong lop Const

                string backupFolder;

                backupFolder = Path.GetFullPath(folder);
                DirectoryInfo DirBackup = new DirectoryInfo(backupFolder);

                if (!DirBackup.Exists)   // Neu thu muc chua ton tai thi Create
                    DirBackup.Create();

                // Goi ham Move file den thu muc Backup
                CheckFileExists(file, DirBackup.FullName, file.Name);
                file.MoveTo(DirBackup.FullName + "\\" + file.Name);
            }
            catch
            {
                //  WriteLogDB(LOG_Info, "Backup file failed " + file.FullName, 1);
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

        private void GetFolderImport()
        {
            try
            {
               // string strPath = "";

                DataTable datGWType = new DataTable();
                // Lay cac thong tin ve ENCRYPT
                string strCmd = "select Rec.Pname, Rec.Content, Rec.Gwtype from REC_PARAMETERS Rec where upper(Rec.Pname) in (upper('PATH_REC_SWIFT_IN'),upper('PATH_REC_VCB_IN'))";

                datGWType = new DataTable();
                datGWType = DataAcess.ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    for (int i = 0; i < datGWType.Rows.Count; i++)
                    {
                        if (datGWType.Rows[i]["Gwtype"].ToString().ToUpper() == "SWIFT")
                        {
                            strSwiftPath = datGWType.Rows[i]["Content"].ToString();
                        }
                        else
                        {
                            if (datGWType.Rows[i]["Gwtype"].ToString().ToUpper() == "VCB")
                            {
                                strVCBPath = datGWType.Rows[i]["Content"].ToString();
                            }
                        }

                        
                    }
                }
                datGWType.Clear();

                
            }
            catch //(Exception ex)
            {
                //WriteLogDB(LOG_Error, " Get info GWTYPE  failed" + ex.Message, 1);
                return ;
            }
        }
    }
}
