using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace BR.BRLib
{
    public class clsFile
    {
        #region Delete file(FileInfo file, string destPath)
        public void MoveFile(string file, string destPath)///FileInfo
        {
            File.Delete(destPath + "\\" + file);
        }
        #endregion

        #region Delete file(FileInfo file)
        public void Delete( string Path_file)///FileInfo
        {
            File.Delete(Path_file);
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

        #region Doc mot danh sach file tu Folder - ScanFile(string DirPath)
        public List<FileInfo> ScanFile(string DirPath, string file)
        {
            List<FileInfo> list = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(DirPath));
            try
            {
                // Scan file tu thu muc, voi cac file co dinh dang lay 
                // tu dateFileFormat trong lop Constant
                FileInfo[] files = dir.GetFiles(file);
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


        #region Ham ghi ra mot file Text
        public void ExportFile(string DirPath, string FileName, string content)
        {
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    MessageBox.Show("Ten File bi trung lap: " + FileName);
                    return;
                }
                using (StreamWriter sw = File.CreateText(FullName))
                {
                    sw.Write(content);
                    //sw.WriteLine(content);
                    sw.Close();
                    return;
                }
            }
            catch
            {
                return;
            }
        }
        #endregion

    }
}
