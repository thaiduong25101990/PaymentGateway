using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BRFTPService.Technique
{
    public class File
    {
        /***********************************
         * Cac ham su dung cho File,Directory
         ***********************************/ 

        // Tim kiem file trong thu muc
        public static List<FileInfo> ScanFiles(string DirName,string Search)
        {
            List<FileInfo> lst = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(DirName));
            try
            {
                FileInfo[] FILEs = dir.GetFiles(Search);
                lst.AddRange(FILEs);
            }
            catch
            {
                lst = null;                
            }
            return lst;
        }
        
        // Move File
        public static int Move(FileInfo file, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(destPath);
                if (!dir.Exists) dir.Create();
                file.MoveTo(destPath + file.Name); // Move
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        // Delete File
        public static int Delete(string FileName)
        {
            try
            {
                System.IO.File.Delete(FileName);
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        // Exist File
        public static int Exist(string FileName)
        {
            if (System.IO.File.Exists(FileName) == true) return 1;
            return -1;
        }
        // BackUp File tu thu muc nay sang thu muc khac        
        public void Backup(FileInfo file,string DesDir)
        {
            try
            {
                // Khoi tao thu muc Backup

                DirectoryInfo DirBK = new DirectoryInfo(DesDir);

                if (!DirBK.Exists) DirBK.Create();

                // Goi ham Move file den thu muc Backup
                file.MoveTo(DirBK.FullName + "\\" + file.Name);
            }
            catch
            {
                Log.WriteLogFile(0, "Co loi khi Backup file" + file.FullName);
            }
        }
    }
}
