using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OracleClient;
using System.Data;

namespace BRFTPService.Technique
{
    /***********************************
     * Cung cap cac ham ghi LOG
     ***********************************/
    public class Log
    {
        public static string LogFolder   = @"C:\ServiceLog";
        public static string LogFileName = @"GWFTP_Service.log";


        // Ghi Log vao File
        public static bool WriteLogFile(int log_Level, string strContent)
        {
            string DirPath = LogFolder;
            string FileName = LogFileName;
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);
            string FullName = DirPath + "\\" + FileName;
            FileInfo file = new FileInfo(FullName);
            try
            {
                if (System.IO.File.Exists(FullName))
                {
                    using (StreamWriter sw = System.IO.File.AppendText(FullName))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + strContent);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = System.IO.File.CreateText(FullName))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt") + ": " + strContent);
                        sw.Close();

                    }
                }
                WriteLogDB(log_Level, strContent);
                return true;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Can not Write to {0} file.", FullName);
                return false;
            }
        }

        // Ghi Log vao Database
        public static void WriteLogDB(int LogLevel, string Content)
        {

            // Mo ket noi database            
            DataAcess da=new DataAcess();
            try
            {                
                OracleParameter[] param = new OracleParameter[]
                {
                    new OracleParameter("pLogDate",OracleType.DateTime),
                    new OracleParameter("pService","FTPService"),
                    new OracleParameter("pContent",Content),
                    new OracleParameter("pStatus" ,LogLevel),
                };
                param[0].Value = DateTime.Now;

                string strSQL = "insert into MSG_LOG (Log_Date, Service, Content, Status)"
                              + " values (:pLogDate,:pService, :pContent,:pStatus)";
                da.ExcuteNonQuery(strSQL, param);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

    }
}
