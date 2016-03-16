using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BRFTPService.Data;
using BRFTPService.Technique;
using System.Data;

namespace BRFTPService.Technique
{
    public class GWService
    {
        public static GWServiceInfo ServiceInfo01 = new GWServiceInfo();
        public static GWServiceInfo ServiceInfo02 = new GWServiceInfo();
        
        public static int OnStartInfo()
        {
            DataTable dt;
            try
            {   
                DataAcess da = new DataAcess();
                dt = da.GetSelect("select id,SERVICE_NAME,TASK_NAME,DESCRIPTION,TIMEDELAY from GWSERVICE_FTP");
                BRFTPService.Data.DataSet.GWSERVICE_FTP.Rows.Clear();
                BRFTPService.Data.DataSet.GWSERVICE_FTP.Merge(dt);
                return 1;
            }
            catch(Exception ex)
            {
                Log.WriteLogFile(1,"Get parametter in GWService_FTP Error " + ex.ToString());
                return -1;
            }
        }
        public static int OnCheckInfo01()
        {
            try
            {
            DataRow dr = BRFTPService.Data.DataSet.GWSERVICE_FTP.Select("TASK_NAME = 'taskbase'")[0];
            GWService.ServiceInfo01.TimeDelay = (int)Convert.ToInt32(dr["TIMEDELAY"]);
            return 1;
            }
            catch(Exception ex)
            {
                Log.WriteLogFile(1,"Cannot get 'taskbase'"+ex.ToString()+"");
                GWService.ServiceInfo01.TimeDelay = 10000;
            }
            return -1;
        }
        public static int OnCheckInfo02()
        {
            try
            {
                DataRow dr = BRFTPService.Data.DataSet.GWSERVICE_FTP.Select("TASK_NAME = 'taskacknack'")[0];
                GWService.ServiceInfo01.TimeDelay = (int)Convert.ToInt32(dr["TIMEDELAY"]);
                return 1;
            }
            catch (Exception ex)
            {
                Log.WriteLogFile(1, "Cannot get 'taskacknack'" + ex.ToString() + "");
                GWService.ServiceInfo01.TimeDelay = 10000;
            }
            return -1;
        }
    }

    public class GWServiceInfo
    {
        public int TimeDelay;
    }
}
