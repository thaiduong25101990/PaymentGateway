using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace BR.BRLib
{
    public class clsImport
    {
         //Khai bao mot Datatable de dung chung
        public DataTable m_dtCSV = new DataTable();        
        public Int32 m_iColumnCount = 0;




    //Thu tuc thu 1 (tao ra mot DataTable tam co dinh dang giong het file Excel)
        public void PopulateDataTableFromUploadedFile(System.IO.Stream strm)
        {
            System.IO.StreamReader srdr = new System.IO.StreamReader(strm);
            String strLine = String.Empty;
            Int32 iLineCount = 0;
            
            do
            {
                strLine = srdr.ReadLine();
                if (strLine == null)
                {
                    break;
                }
                if (0 == iLineCount++)
                {
                    m_dtCSV = this.CreateDataTableForCSVData(strLine);
                }
                this.AddDataRowToTable(strLine, m_dtCSV);
            } while (true);
        }




        //Thu tuc thu 2 (duoc goi boi 1)
        private DataTable CreateDataTableForCSVData(String strLine)
        {
            DataTable dt = new DataTable("CSVTable");
            String[] strVals = strLine.Split(new char[] { '\t' });
            Int32 m_iColumnCount = strVals.Length;
            int idx = 0;
            foreach (String strVal in strVals)
            {
                idx++;
                String strColumnName = "Column" + Convert.ToString(idx);
                dt.Columns.Add(strColumnName, Type.GetType("System.String"));
            }
            return dt;
        }





        //Thu tuc thu 3 (duoc goi boi 1)
        private DataRow AddDataRowToTable(String strCSVLine, DataTable dt)
        {

            String[] strVals = strCSVLine.Split(new char[] { '\t' });           
            Int32 iTotalNumberOfValues = strVals.Length;
             int idx = 0;
            DataRow drow = dt.NewRow();
            foreach (String strVal in strVals)
            {
                //String strColumnName = String.Format("Column-{0}", idx++);
                idx++;
                String strColumnName = "Column" + Convert.ToString(idx);
                drow[strColumnName] = strVal.Trim().ToString();
            }
            dt.Rows.Add(drow);
            return drow;
        }
        public static List<FileInfo> ScanFile(string DirPath)
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
            }           
            return list;
        }

        public static string OpenFile(string pathFile)
        {
            string result = String.Empty;
            try
            {
                StreamReader sr = new StreamReader(pathFile, Encoding.Default);
                // Doc tu dau den het file, tra lai kieu String
                result = sr.ReadToEnd();
                sr.Close();  
            }
            catch
            {                
                result = String.Empty;
            }
            return result;
        }

        public static void MoveFile(FileInfo file, string destPath)
        {
            DirectoryInfo dir = new DirectoryInfo(destPath);
            if (!dir.Exists)  // Neu thu muc chua ton tai thi Create
                dir.Create();
            CheckFileExists(file, destPath, file.Name);
            file.MoveTo(destPath + "\\" + file.Name); // Goi ham di chuyen file den thu muc can chuyen toi
        }
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
        public static bool ExportFile(string DirPath, string FileName, string content)
        {
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (File.Exists(FullName))
                {
                    File.Delete(FullName);
                    //WriteLogDB(LOG_Error, "Ten File bi trung lap: " + FileName, Query_ID);
                   // return false;
                }
                System.Text.Encoding enc = System.Text.Encoding.Default;
                //using (StreamWriter sw = File.CreateText(FullName))
                //{
                    StreamWriter sw;
                  
                    sw = new StreamWriter(FullName, false, enc);
                    sw.Write(content);
                    //sw.WriteLine(content);
                    sw.Close();
                    return true;
                //}
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }
        #endregion
        public static void CheckFileExists(FileInfo file, string destPath, string strFilename)
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


    }
}
