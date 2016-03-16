using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace BRIBPS_TR_Inquiry
{
    public partial class IBPSTR_Inquiry : ServiceBase
    {
        public static bool isStop = false;
        public static int timeSleep = 200;
        public static int countmessage = 0;
        public static DateTime sysdate;        
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;
        public static string SERVICE_NAME = "";
        public static string pSysdate="";

        public IBPSTR_Inquiry()
        {
            InitializeComponent();
            //ActiveProcess();
        }

        protected override void OnStart(string[] args)
        {
            SERVICE_NAME = this.ServiceName.Trim();
            Thread thrService = new Thread(new ThreadStart(ActiveProcess));
            thrService.Start();
        }

        protected override void OnStop()
        {
            isStop = true;
            Lib.WriteLogDB(0, SERVICE_NAME + " Onstop", 1);
        }

        private void ActiveProcess()
        {
            SERVICE_NAME = this.ServiceName;
            while (!isStop)
            {
                BeforeProcess();            
                MainProcess(); 
                Thread.Sleep(timeSleep);
            }
        }

        private void BeforeProcess()
        {
            try
            {
                DataTable _dt = new DataTable();
                string strCmd = "select Timedelay,to_char(sysdate,'YYYYMMDD') as Date_day from Gwservice_Port Where upper(SERVICENAME)=upper('BRIBPSTRInquiry')";
                _dt = Lib.Select_ReturnDataTable(strCmd);
                if (_dt.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(_dt.Rows[0]["Timedelay"]);
                    pSysdate = _dt.Rows[0]["Date_day"].ToString();
                }
                _dt.Clear();

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, " Can not get timeSleep " + ex.Message, 1);
                return;
            }
        }

        private void MainProcess()
        {
            try
            {                
                ProcessInquiry.Instance().ProcessService();
            }
            catch
            {
                Lib.WriteLogDB(0, "Error IBPSTR_Inquiry Service", 1);
            }
        }
    }
}
