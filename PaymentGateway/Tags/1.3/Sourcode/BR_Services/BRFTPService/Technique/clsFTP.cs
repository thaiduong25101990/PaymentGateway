using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterpriseDT.Net.Ftp;
using EnterpriseDT.Util;
using System.Windows.Forms;

namespace BRFTPService.Technique
{
    /*********************************************
     * Ham cung cap cac phuong thuc FTP Server
     *********************************************/
    public class FTP
    {
        FTPClient ftp = null;
        private string Host = "";
        private string UserName = "";
        private string Password = "";

        public int LogIn(string Host,string UserName,string Password)
        {
            try
            {
                this.Host     = Host;
                this.UserName = UserName;
                this.Password = Password;
                return 1;
            }
            catch
            {
                //MessageBox.Show("Can not connect","FTP");
                return -1;
            }
            //return -1;
        }

        public int UpLoad(string LocalFile, string RemoteFile)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();                
                ftp.Login(this.UserName, this.Password);
                ftp.TransferType = FTPTransferType.BINARY;
                ftp.Put(LocalFile, RemoteFile);
                return 1;
            }
            catch(Exception ex)
            {
                Log.WriteLogFile(2, "Cannot UpLoad " + ex.ToString());
                return -1;
            }
        }

        public int DownLoad(string LocalFile, string RemoteFile)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();                
                ftp.Login(this.UserName, this.Password);
                ftp.TransferType = FTPTransferType.BINARY;
                ftp.Get(LocalFile, RemoteFile);                
                return 1;
            }
            catch(Exception ex)
            {
                Log.WriteLogFile(2,"Cannot Download " + ex.ToString());
                return -1;
            }
        }

        public int Exist(string RemoteFile)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();
                ftp.Login(this.UserName, this.Password);
                if (ftp.Exists(RemoteFile)) return 1;
                return -1;
            }
            catch
            {
                return -1;
            }
        }

        public int Dispose()
        {
            ftp.Quit();
            return 1;
        }

        public List<string> ScanFiles(string RemoteDir)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();
                ftp.Login(this.UserName, this.Password);
                FTPFile[] FILEs = ftp.DirDetails(RemoteDir);

                // Len
                List<string> ListFile = new List<string>();
                foreach (FTPFile File in FILEs) { if (File.Dir != true) { ListFile.Add(File.Name); } }
                return ListFile;
            }
            catch (Exception ex)
            {
                Log.WriteLogFile(2, "Cannot ScanFile over FTP " + ex.ToString());
                return null;
            }
        }

        public int Delete(string RemoteFile)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();
                ftp.Login(this.UserName, this.Password);
                ftp.Delete(RemoteFile);
                return 1;
            }
            catch(Exception ex)
            {
                Log.WriteLogFile(2, "Cannot Delete " + ex.ToString());
                return -1;
            }
        }

        public int MkDir(string RemoteDir)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();
                ftp.Login(this.UserName, this.Password);
                ftp.TransferType = FTPTransferType.BINARY;
                ftp.MkDir(RemoteDir);
                return 1;
            }
            catch
            {
                return -1;
            }               
        }

        public int Rename(string srcPath,string desPath)
        {
            try
            {
                ftp = new FTPClient();
                ftp.RemoteHost = this.Host;
                ftp.Connect();
                ftp.Login(this.UserName, this.Password);
                ftp.TransferType = FTPTransferType.BINARY;
                ftp.Rename(srcPath, desPath);
                return 1;
            }
            catch (Exception ex)
            {
                Log.WriteLogFile(2, "Cannot Rename " + ex.ToString());
                return -1;
            }
        }
    }

    public class FTPInfo
    {
        public string Host = "";
        public string Username = "";
        public string Password = "";
    }
}
