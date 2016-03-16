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



namespace BRRECONSIDERead
{
    public partial class Reconside : ServiceBase
    {

        private bool isStop = false;
        private static string SERVICE_NAME;
        public Reconside()
        {
            InitializeComponent();
            //StartProcess();
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


                //GW_PK_RECONCILE.Reconcile_trace
               // WriteLogDB(LOG_Info, this.ServiceName + " onStart error: " + ex.Message, 1);
                base.OnStart(args);
            }
        }

        protected override void OnStop()
        {
        }

        private void OnService()
        {
            SERVICE_NAME = this.ServiceName;
            while (!isStop)
            {
                
                // Bat dau mot vong xu li moi
                StartProcess();
                Thread.Sleep(10000);
            }
        }

        public void StartProcess()
        {
            //string strPath = "";

            try
            {
                //lay thu muc import

                // quet file trong thu muc
                string ImportFolder = Path.GetFullPath(Get_ImportPath());
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
                        DataTable tblCheck = new DataTable();
                        // Neu lay duoc so Ref thi tiep tuc
                        string FileName = file.Name;
                        ReadSIBSReconcileFile(file);
                        // copy file sang thu muc backup
                        BackupFile(file, ImportFolder + "\\Backup");
                    }
                    catch {
                    }
                }
              
            }
            catch
            {
            }
        }
        private void ReadSIBSReconcileFile(FileInfo file)
        {
            //string strFile = "";
            //string strResult = "";
            string strPath = "";
            string strFileBlock = "";
            //string strNow = "";
            string strMessage = "";
            DateTime dsysdate;
            int i = 0;
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
                    /* filter "GWTYPR" */
                    strMessage = strFileBlock;

                    
                    OracleParameter[] parameters = new OracleParameter[]{
                    new OracleParameter("pFile_name",file.Name),
                    new OracleParameter("pMessage_text",strMessage),                                                    
                    new OracleParameter("pSysdate",OracleType.DateTime)
                    }; // Tao moi sanh sach cac Parameters
                    parameters[2].Value = DateTime.Now;
                    DataAcess.ExecuteNonQuery(CommandType.StoredProcedure,"GW_PK_RECONCILE.GW_EQ_REC_SIBS_OUT", parameters);
                        i=i+1;
                    }
                    catch
                    {

                        DataAcess.WriteLogDB(file.Name, "Loi khi dang doc file :" + file.Name + " den dong thu  " + i);
                        continue;
                    }
                }
                //strResult = sr.ReadToEnd();
                sr.Close();  // Close(file);
              
            }
            catch //(Exception ex)
            {

                DataAcess.WriteLogDB(file.Name, "Loi khi dang doc file :" +file.Name+ " den dong thu  " + i);
            }
        }


        // Ham kiem tra de lay cac tham so ve ma hoa ENCRYPT
        private string Get_ImportPath()
        {
            try
            {
                string strPath = "";

                DataTable datGWType = new DataTable();
                // Lay cac thong tin ve ENCRYPT
                string strCmd = "select Rec.Pname, Rec.Content, Rec.Gwtype from REC_PARAMETERS Rec where upper(Rec.Pname) =upper('PATH_SIBS_OUT')";

                datGWType = new DataTable();
                datGWType = DataAcess.ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    strPath= datGWType.Rows[0]["Content"].ToString();
                }
                datGWType.Clear();

                return strPath;
            }
            catch //(Exception ex)
            {
                //WriteLogDB(LOG_Error, " Get info GWTYPE  failed" + ex.Message, 1);
                return "";
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
        public void BackupFile(FileInfo file, string folder)
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

    }
}
