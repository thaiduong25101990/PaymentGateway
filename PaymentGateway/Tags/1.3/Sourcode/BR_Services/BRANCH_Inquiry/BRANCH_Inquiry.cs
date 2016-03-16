using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace BRANCH_Inquiry
{
    public partial class VCB_Inquiry : ServiceBase
    {
        public static bool isStop = false;
        public static int timeSleep = 200;
        public static int countmessage = 0;
        public static DateTime sysdate;
        // Level cua noi dung ghi log
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;
        public static string SERVICE_NAME = "";

        public VCB_Inquiry()
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
                BeforeProcess();// Lay cac gia tri truoc khi xu li                
                MainProcess(); // Bat dau xu li
                Thread.Sleep(timeSleep);// dat do tre laf 1000 mini giay
            }
        }
        // Ham kiem tra de lay cac tham so truoc khi xu li
        private void BeforeProcess()
        {
            try
            {
                //Lay thoi gian delay trong file config
                timeSleep = Convert.ToInt32(Lib.getKeyConfig("Timedelay"));
                sysdate = Convert.ToDateTime(Lib.GetSysdate());
                countmessage = Lib.Sum_message();

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
                //Lib.WriteLogDB(LOG_Info, this.ServiceName + " Bat dau xu li - Start new process...");
                ProcessInquiry.Instance().ProcessService();
            }
            catch
            {
                Lib.WriteLogDB(0, "Error GWSWIFTRMInquiry Service", 1);
            }
        }
       

    }
}
