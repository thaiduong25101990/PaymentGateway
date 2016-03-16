using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Data.OracleClient;
using System.Threading;
//using FTP;
using System.Globalization;
using System.Xml;
using System.Security.Cryptography;
using BRFTPService.Technique;

namespace BRFTPService
{
    public partial class FTPService : ServiceBase
    {
        public static bool isStop = false;

        // Cac bien va Hang so
        private const string LOG_FOLDER = @"C:\ServiceLog"; // Thu muc chua file ghi log mac dinh la C:\ServiceLog
        private const string LOG_FILENAME = "BRFTP_Service.log"; // File ghi log cho service

        // Const Log Level
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;

        // Ten cua Service
        public static string SERVICE_NAME = "";
        // Duong dan file config - cau hinh Database
        // Khi service chay duoc dat trong C:\WINDOWS\System32
        private string strConfigFilePath = System.Environment.CurrentDirectory + "\\BRService.conf";

        public FTPService()
        {
            InitializeComponent();
            SERVICE_NAME = this.ServiceName;

             // Test service, khi cai dat thi bo doan code dong nay
             OnService01(); // Test
             OnService02();
        }

        // Ham Start Service
        protected override void OnStart(string[] args)
        {
            try
            {
                Log.WriteLogFile(LOG_Info, this.ServiceName + " On Start");
                //GWService.OnStartInfo();
                Thread task01 = new Thread(new ThreadStart(OnService01)); task01.Start();
                Thread task02 = new Thread(new ThreadStart(OnService02)); task02.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogFile(LOG_Error, this.ServiceName + " Co loi xay ra khi Onstart " + ex.Message);
                base.OnStart(args);
            }
        }

        // Ham Stop Service
        protected override void OnStop()
        {
            Log.WriteLogFile(LOG_Info, this.ServiceName + " On Stop");
            isStop = true;
        }
        
        // Ham chay Service
        private void OnService01()
        {
            GWService.OnStartInfo();
            GWService.OnCheckInfo01();
            
            while (!isStop) // Neu khong co lenh dung
            {
                try
                {
                    Thread.Sleep(GWService.ServiceInfo01.TimeDelay);
                    // Cac kenh can FTP
                    ExportSWIFT();
                    ImportSWIFT();
                    GetReconcileSWIFT();
                    //GetRMRCON();                    
                }
                catch //(Exception ex)
                {                    
                    continue;
                }
            }
        }
        private void OnService02()
        {
            GWService.OnStartInfo();
            GWService.OnCheckInfo02();

            while (!isStop) // Neu khong co lenh dung
            {
                try
                {
                    Thread.Sleep(GWService.ServiceInfo02.TimeDelay);
                    GetACK_SWIFT();
                }
                catch //(Exception ex)
                {
                    continue;
                }
            }
        } 
        
        // Quet tu dong Export File SWIFT
        private void ExportSWIFT()
        {
            FTPInfo ftpinfo = new FTPInfo();
            DataTable dtFTP = null;
            DataRow[] rows  = null;
            //string backupFolder = "";
            //string errorFolder  = "";
            GWFLDs gwFLD        = null;
            FTPFOLDERS ftpFLD   = null;

            try
            {
                // Lay cac thong tin ve FTP Export
                string strSQL = "SELECT FLDTYPE,FOLDER,FTPSERVER,FTPPATH, FTPUSER, FTPPASS FROM GWTYPE_DETAIL WHERE GWTYPE = 'SWIFT' AND DIRECTION=1 ";
                // Select thong tin FTP: Server, Path ,User, Password
                
                DataAcess da = new DataAcess();
                dtFTP = da.GetSelect(strSQL);
                
                gwFLD = new GWFLDs();// Group Folder Gateway
                ftpFLD = new FTPFOLDERS();// Group Folder FTP Server
                rows = dtFTP.Select("FLDTYPE = 1");
                gwFLD.Normal    = rows[0]["FOLDER"].ToString();
                
                ftpFLD.Host     = rows[0]["FTPSERVER"].ToString();
                ftpFLD.Username = rows[0]["FTPUSER"].ToString();
                ftpFLD.Password = rows[0]["FTPPASS"].ToString();

                ftpFLD.Normal = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 2"); 
                gwFLD.BackUp = rows[0]["FOLDER"].ToString();
                ftpFLD.BackUp = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 3");
                gwFLD.Error = rows[0]["FOLDER"].ToString();
                ftpFLD.Error = rows[0]["FTPPATH"].ToString();                
                
            }
            catch //(Exception ex)
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc FOLDER, FTPUSER, FTPPASS trong bang GWTYPE voi SWIFT");
                return;
            }
            // Kiem tra thong ve FTP, neu mot trong cac thong tin bi rong thi return
            if (ftpFLD.Host == "" || ftpFLD.Username == "" || ftpFLD.Password == "" || ftpFLD.Normal == "" )
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc thong tin FTP tu bang GWTYPE voi SWIFT");
                return;
            }

            // Scan File in Export Folder to upload FTP
            List<FileInfo> lstFiles = Technique.File.ScanFiles(gwFLD.Normal,"*.*");
            // Check
            if (lstFiles == null || lstFiles.Count == 0) return;

            int IsUpLoad = 0; int IsMove = 0;
            FTP ftp = new FTP();
            if (ftp.LogIn(ftpFLD.Host, ftpFLD.Username, ftpFLD.Password) < 0) { Log.WriteLogFile(LOG_Error, "Cannot LogIn FTP Server"); return; }

            for (int FileCnt = 0; FileCnt < lstFiles.Count; FileCnt++)
            {
                IsUpLoad = 0;
                string FTPPathNM = ftpFLD.Normal + lstFiles[FileCnt].Name;
                string GWPathNM  = gwFLD.Normal  + lstFiles[FileCnt].Name;
                string GWPathBK  = gwFLD.BackUp  + lstFiles[FileCnt].Name;
                string GWPathER  = gwFLD.Error   + lstFiles[FileCnt].Name;

                if (ftp.Exist(FTPPathNM) < 0) IsUpLoad = ftp.UpLoad(GWPathNM,FTPPathNM);

                // UpLoad Successfully => Backup
                if (IsUpLoad>0)
                {
                    if (Technique.File.Exist(GWPathBK) == 1) Technique.File.Delete(GWPathBK);
                    IsMove = Technique.File.Move(lstFiles[FileCnt], gwFLD.BackUp);
                    if (IsMove < 0) { Log.WriteLogFile(2, "Cannot Move BackUp File Export SWIFT"); }
                }
                // UpLoad Error => Error Folder
                else
                {
                    if (Technique.File.Exist(GWPathER) == 1) Technique.File.Delete(GWPathER);
                    Technique.File.Move(lstFiles[FileCnt], gwFLD.Error);
                    if (IsMove < 0) { Log.WriteLogFile(2, "Cannot Move Error File Export SWIFT"); }
                }
            }
        }
        // Quet tu dong Import File SWIFT
        private void ImportSWIFT()
        {
            FTPInfo ftpinfo = new FTPInfo();
            DataTable dtFTP = null;
            DataTable dtRec = null;
            DataRow[] rows = null;
            //string backupFolder = "";
            //string errorFolder = "";
            string ReconcileFolder = "";
            GWFLDs gwFLD = null;
            FTPFOLDERS ftpFLD = null;

            try
            {
                // Lấy các thông tin về SWIFT Import + Thư mục Reconcile SWIFT IN
                // Select thông tin FTP: Server, Path ,User, Password
                // Để thực hiện cơ chế multi copy *-> RECON\SWIFT *-> SWIFT\Import
                string strSQL = "SELECT FLDTYPE,FOLDER,FTPSERVER,FTPPATH, FTPUSER, FTPPASS FROM GWTYPE_DETAIL WHERE GWTYPE = 'SWIFT' AND DIRECTION=2 AND ID = 419";
                DataAcess da = new DataAcess();
                dtFTP = da.GetSelect(strSQL);

                gwFLD = new GWFLDs();     // Group Folder Gateway
                ftpFLD = new FTPFOLDERS();// Group Folder FTP Server
                rows = dtFTP.Select("FLDTYPE = 1");
                gwFLD.Normal = rows[0]["FOLDER"].ToString();

                ftpFLD.Host     = rows[0]["FTPSERVER"].ToString();
                ftpFLD.Username = rows[0]["FTPUSER"].ToString();
                ftpFLD.Password = rows[0]["FTPPASS"].ToString();

                ftpFLD.Normal   = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 2");
                gwFLD.BackUp    = rows[0]["FOLDER"].ToString();
                ftpFLD.BackUp   = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 3");
                gwFLD.Error     = rows[0]["FOLDER"].ToString();
                ftpFLD.Error    = rows[0]["FTPPATH"].ToString();

                // Lấy thông tin dữ liệu của Thư mục Reconcile 
                string strSQL1 = "SELECT FLDTYPE,FOLDER,FTPSERVER,FTPPATH, FTPUSER, FTPPASS FROM GWTYPE_DETAIL WHERE GWTYPE = 'SWIFT' AND DIRECTION=5 ";
                dtRec = da.GetSelect(strSQL1);
                ReconcileFolder = dtRec.Rows[0]["FOLDER"].ToString();
            }
            catch //(Exception ex)
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc FOLDER, FTPUSER, FTPPASS trong bang GWTYPE voi SWIFT");
                return;
            }
            // Kiem tra thong ve FTP, neu mot trong cac thong tin bi rong thi return
            if (ftpFLD.Host == "" || ftpFLD.Username == "" || ftpFLD.Password == "" || ftpFLD.Normal == "")
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc thong tin FTP tu bang GWTYPE voi SWIFT");
                return;
            }

            // Scan File in Import Folder to upload FTP           

            int IsDownLoad = 0; int IsRename = 0;
            FTP ftp = new FTP();
            if (ftp.LogIn(ftpFLD.Host, ftpFLD.Username, ftpFLD.Password) < 0) { Log.WriteLogFile(LOG_Error, "Cannot LogIn FTP Server"); return; }
            List<string> lstFTPFiles = ftp.ScanFiles(ftpFLD.Normal);

            for (int FileCnt = 0; FileCnt < lstFTPFiles.Count; FileCnt++)
            {
                IsDownLoad = 0;
                string FTPPathNM = ftpFLD.Normal + lstFTPFiles[FileCnt];
                string FTPPathBK = ftpFLD.BackUp + lstFTPFiles[FileCnt];
                string FTPPathER = ftpFLD.Error  + lstFTPFiles[FileCnt];
                string GWPathNM  = gwFLD.Normal  + lstFTPFiles[FileCnt];
                string GWPathRE  = ReconcileFolder + lstFTPFiles[FileCnt];

                // Multi Copy
                IsDownLoad = ftp.DownLoad(GWPathNM, FTPPathNM);
                IsDownLoad = ftp.DownLoad(GWPathRE, FTPPathNM);
                
                // Download Successfully => Backup
                if (IsDownLoad > 0)
                {
                    IsRename = ftp.Rename(FTPPathNM, FTPPathBK);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename BackUp File SWIFT Import "); }
                }
                // Download Error => Error Folder
                else
                {
                    ftp.Rename(FTPPathNM, FTPPathER);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename Error File SWIFT Import"); }
                }
            }            
        }
        // Quet tu dong Get File Reconcile SWIFT
        private void GetReconcileSWIFT()
        {
            FTPInfo ftpinfo = new FTPInfo();
            DataTable dtFTP = null;
            DataRow[] rows = null;
            //string backupFolder = "";
            //string errorFolder = "";
            GWFLDs gwFLD = null;
            FTPFOLDERS ftpFLD = null;

            try
            {
                // Lay cac thong tin ve FTP Export
                string strSQL = "SELECT FLDTYPE,FOLDER,FTPSERVER,FTPPATH, FTPUSER, FTPPASS FROM GWTYPE_DETAIL WHERE GWTYPE = 'SWIFT' AND DIRECTION=3 ";
                // Select thong tin FTP: Server, Path ,User, Password

                DataAcess da = new DataAcess();
                dtFTP = da.GetSelect(strSQL);

                gwFLD = new GWFLDs();     // Group Folder Gateway
                ftpFLD = new FTPFOLDERS();// Group Folder FTP Server
                rows = dtFTP.Select("FLDTYPE = 1");
                gwFLD.Normal    = rows[0]["FOLDER"].ToString();

                ftpFLD.Host     = rows[0]["FTPSERVER"].ToString();
                ftpFLD.Username = rows[0]["FTPUSER"].ToString();
                ftpFLD.Password = rows[0]["FTPPASS"].ToString();

                ftpFLD.Normal = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 2");
                gwFLD.BackUp = rows[0]["FOLDER"].ToString();
                ftpFLD.BackUp = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 3");
                gwFLD.Error = rows[0]["FOLDER"].ToString();
                ftpFLD.Error = rows[0]["FTPPATH"].ToString();

            }
            catch //(Exception ex)
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc FOLDER, FTPUSER, FTPPASS trong bang GWTYPE voi SWIFT");
                return;
            }
            // Kiem tra thong ve FTP, neu mot trong cac thong tin bi rong thi return
            if (ftpFLD.Host == "" || ftpFLD.Username == "" || ftpFLD.Password == "" || ftpFLD.Normal == "")
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc thong tin FTP tu bang GWTYPE voi SWIFT");
                return;
            }

            // Scan File in Import Folder to upload FTP           

            int IsDownLoad = 0; int IsRename = 0;
            FTP ftp = new FTP();
            if (ftp.LogIn(ftpFLD.Host, ftpFLD.Username, ftpFLD.Password) < 0) { Log.WriteLogFile(LOG_Error, "Cannot LogIn FTP Server"); return; }
            List<string> lstFTPFiles = ftp.ScanFiles(ftpFLD.Normal);

            for (int FileCnt = 0; FileCnt < lstFTPFiles.Count; FileCnt++)
            {
                IsDownLoad = 0;
                string FTPPathNM = ftpFLD.Normal + lstFTPFiles[FileCnt];
                string FTPPathBK = ftpFLD.BackUp + lstFTPFiles[FileCnt];
                string FTPPathER = ftpFLD.Error + lstFTPFiles[FileCnt];
                string GWPathNM = gwFLD.Normal + lstFTPFiles[FileCnt];

                //if (ftp.Exist(lstFiles[FileCnt].Name) < 0)// FTP => GW
                IsDownLoad = ftp.DownLoad(GWPathNM, FTPPathNM);

                // Download Successfully => Backup
                if (IsDownLoad > 0)
                {
                    IsRename = ftp.Rename(FTPPathNM, FTPPathBK);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename [" + FTPPathNM + "]->" + FTPPathBK); }
                }
                // Download Error => Error Folder
                else
                {
                    IsRename = ftp.Rename(FTPPathNM, FTPPathER);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename [" + FTPPathNM + "]->" + FTPPathER); }
                }
            }            
        }
        // Quet tu dong Get File ACK SWIFT
        private void GetACK_SWIFT()
        {
            FTPInfo ftpinfo = new FTPInfo();
            DataTable dtFTP = null;
            DataRow[] rows = null;
            //string backupFolder = "";
            //string errorFolder = "";
            GWFLDs gwFLD = null;
            FTPFOLDERS ftpFLD = null;

            try
            {
                // Lay cac thong tin ve FTP Export
                string strSQL = "SELECT FLDTYPE,FOLDER,FTPSERVER,FTPPATH, FTPUSER, FTPPASS FROM GWTYPE_DETAIL WHERE GWTYPE = 'SWIFT' AND DIRECTION=4 ";
                // Select thong tin FTP: Server, Path ,User, Password

                DataAcess da = new DataAcess();
                dtFTP = da.GetSelect(strSQL);

                gwFLD = new GWFLDs();     // Group Folder Gateway
                ftpFLD = new FTPFOLDERS();// Group Folder FTP Server
                rows = dtFTP.Select("FLDTYPE = 1");
                gwFLD.Normal = rows[0]["FOLDER"].ToString();

                ftpFLD.Host = rows[0]["FTPSERVER"].ToString();
                ftpFLD.Username = rows[0]["FTPUSER"].ToString();
                ftpFLD.Password = rows[0]["FTPPASS"].ToString();

                ftpFLD.Normal = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 2");
                gwFLD.BackUp = rows[0]["FOLDER"].ToString();
                ftpFLD.BackUp = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 3");
                gwFLD.Error = rows[0]["FOLDER"].ToString();
                ftpFLD.Error = rows[0]["FTPPATH"].ToString();

            }
            catch //(Exception ex)
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc FOLDER, FTPUSER, FTPPASS trong bang GWTYPE voi SWIFT");
                return;
            }
            // Kiem tra thong ve FTP, neu mot trong cac thong tin bi rong thi return
            if (ftpFLD.Host == "" || ftpFLD.Username == "" || ftpFLD.Password == "" || ftpFLD.Normal == "")
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc thong tin FTP tu bang GWTYPE voi SWIFT");
                return;
            }

            // Scan File in Import Folder to upload FTP           

            int IsDownLoad = 0; int IsRename = 0;
            FTP ftp = new FTP();
            if (ftp.LogIn(ftpFLD.Host, ftpFLD.Username, ftpFLD.Password) < 0) { Log.WriteLogFile(LOG_Error, "Cannot LogIn FTP Server"); return; }
            List<string> lstFTPFiles = ftp.ScanFiles(ftpFLD.Normal);

            for (int FileCnt = 0; FileCnt < lstFTPFiles.Count; FileCnt++)
            {
                IsDownLoad = 0;
                string FTPPathNM = ftpFLD.Normal + lstFTPFiles[FileCnt];
                string FTPPathBK = ftpFLD.BackUp + lstFTPFiles[FileCnt];
                string FTPPathER = ftpFLD.Error + lstFTPFiles[FileCnt];
                string GWPathNM = gwFLD.Normal + lstFTPFiles[FileCnt];

                //if (ftp.Exist(lstFiles[FileCnt].Name) < 0)// FTP => GW
                IsDownLoad = ftp.DownLoad(GWPathNM, FTPPathNM);
                if (IsDownLoad < 0) { Log.WriteLogFile(LOG_Error, "Cannot download "); }
                // Download Successfully => Backup
                if (IsDownLoad > 0)
                {
                    ftp.Rename(FTPPathNM, FTPPathBK);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename BackUp File SWIFT ACK"); }
                }
                // Download Error => Error Folder
                else
                {
                    ftp.Rename(FTPPathNM, FTPPathER);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename Error File SWIFT ACK"); }
                }
            }
        }
        // Quet tu dong Get RMRCON 
        private void GetRMRCON()
        {
            FTPInfo ftpinfo = new FTPInfo();
            DataTable dtFTP = null;
            DataRow[] rows = null;
            //string backupFolder = "";
            //string errorFolder = "";
            GWFLDs gwFLD = null;
            FTPFOLDERS ftpFLD = null;

            try
            {
                // Lay cac thong tin ve FTP Export
                string strSQL = "SELECT FLDTYPE,FOLDER,FTPSERVER,FTPPATH, FTPUSER, FTPPASS FROM GWTYPE_DETAIL WHERE GWTYPE = 'SYSTEM' AND DIRECTION=3 ";
                // Select thong tin FTP: Server, Path ,User, Password

                DataAcess da = new DataAcess();
                dtFTP = da.GetSelect(strSQL);

                gwFLD = new GWFLDs();     // Group Folder Gateway
                ftpFLD = new FTPFOLDERS();// Group Folder FTP Server
                rows = dtFTP.Select("FLDTYPE = 1");
                gwFLD.Normal = rows[0]["FOLDER"].ToString();

                ftpFLD.Host = rows[0]["FTPSERVER"].ToString();
                ftpFLD.Username = rows[0]["FTPUSER"].ToString();
                ftpFLD.Password = rows[0]["FTPPASS"].ToString();

                ftpFLD.Normal = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 2");
                gwFLD.BackUp = rows[0]["FOLDER"].ToString();
                ftpFLD.BackUp = rows[0]["FTPPATH"].ToString();

                rows = dtFTP.Select("FLDTYPE = 3");
                gwFLD.Error = rows[0]["FOLDER"].ToString();
                ftpFLD.Error = rows[0]["FTPPATH"].ToString();

            }
            catch //(Exception ex)
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc FOLDER, FTPUSER, FTPPASS trong bang GWTYPE voi SWIFT");
                return;
            }
            // Kiem tra thong ve FTP, neu mot trong cac thong tin bi rong thi return
            if (ftpFLD.Host == "" || ftpFLD.Username == "" || ftpFLD.Password == "" || ftpFLD.Normal == "")
            {
                Log.WriteLogFile(LOG_Error, "Khong the lay duoc thong tin FTP tu bang GWTYPE voi SWIFT");
                return;
            }

            // Scan File in Import Folder to upload FTP

            int IsDownLoad = 0; int IsRename = 0;
            FTP ftp = new FTP();
            if (ftp.LogIn(ftpFLD.Host, ftpFLD.Username, ftpFLD.Password) < 0) { Log.WriteLogFile(LOG_Error, "Cannot LogIn FTP Server"); return; }
            List<string> lstFTPFiles = ftp.ScanFiles(ftpFLD.Normal);

            for (int FileCnt = 0; FileCnt < lstFTPFiles.Count; FileCnt++)
            {
                IsDownLoad = 0;
                string FTPPathNM = ftpFLD.Normal + lstFTPFiles[FileCnt];
                string FTPPathBK = ftpFLD.BackUp + lstFTPFiles[FileCnt];
                string FTPPathER = ftpFLD.Error + lstFTPFiles[FileCnt];
                string GWPathNM = gwFLD.Normal + lstFTPFiles[FileCnt];

                //if (ftp.Exist(lstFiles[FileCnt].Name) < 0)// FTP => GW
                IsDownLoad = ftp.DownLoad(GWPathNM, FTPPathNM);
                if (IsDownLoad < 0) { Log.WriteLogFile(LOG_Error, "Cannot download "); }
                // Download Successfully => Backup
                if (IsDownLoad > 0)
                {
                    ftp.Rename(FTPPathNM, FTPPathBK);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename BackUp File RMRCON"); }
                }
                // Download Error => Error Folder
                else
                {
                    ftp.Rename(FTPPathNM, FTPPathER);
                    if (IsRename < 0) { Log.WriteLogFile(2, "Cannot Rename Error File RMRCON"); }
                }
            }
        }         
    }
}
