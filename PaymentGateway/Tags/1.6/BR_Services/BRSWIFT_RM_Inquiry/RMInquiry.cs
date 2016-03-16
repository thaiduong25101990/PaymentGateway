using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Data.OracleClient;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace GWSWIFTRMInquiry
{
    public partial class SWIFTRMInquiry : ServiceBase
    {
        
        //Bien xac dinh trang thai hoat dong cu service
        //=true service se dung hoat dong
        //=false service dang hoat dong.

        public static bool isStop = false;
        public static int timeSleep = 200; // Thoi gian tre cho service mac dinh la 200

        // Level cua noi dung ghi log
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;
        public static string SERVICE_NAME = "";
        public SWIFTRMInquiry()
        {
            InitializeComponent();
            
           //ActiveProcess(); // Test

        }

        protected override void OnStart(string[] args)
        {
            SERVICE_NAME = this.ServiceName.Trim();
            Thread thrService = new Thread(new ThreadStart(ActiveProcess));
            thrService.Start();
        }

        protected override void OnStop()
        {
            //Dat lai trang thai khi dung service
            isStop = true;
            Lib.WriteLogDB(0, SERVICE_NAME + " Onstop", 1);
        }
        #region Thuc hien goi cac ham xu ly cua service
        /*---------------------------------------------------------------
        * Method           : ActiveProcess() 
        * Muc dich         : Goi Ham thuc hien  qua trinh xu ly cu service
        * Tham so          :  
        *                  
        * Tra ve           : Void
        * Ngay tao         : 18/06/2008
        * Nguoi tao        : TrungNV
        * Ngay cap nhat    : 20/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        private void ActiveProcess()
        {
            SERVICE_NAME = this.ServiceName;
            while (!isStop)
            {
                // Lay cac gia tri truoc khi xu li
                BeforeProcess();
                // Bat dau xu li
                MainProcess();
                Thread.Sleep(timeSleep);// dat do tre laf 1000 mini giay
            }
        }
        #endregion

        #region Xu ly cua service


        // Ham kiem tra de lay cac tham so truoc khi xu li
        private void BeforeProcess()
        {
            try
            {
                DataTable datGWType = new DataTable();
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRSWIFTRMInquiry')";
                datGWType = new DataTable();
                datGWType = Lib.ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();


                //DataTable datGWType = new DataTable();
                //// Lay cac thong tin ve ENCRYPT
                //string strCmd = "SELECT FILETIME From GWTYPE WHERE GWTYPE = 'VCB'";

                //datGWType = new DataTable();
                //datGWType = Lib.ExcuteDataTable(strCmd);
                //if (datGWType.Rows.Count > 0)
                //{
                //    timeSleep = Convert.ToInt32(datGWType.Rows[0]["FILETIME"]);
                //}
                //datGWType.Clear();
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, " Can not get timeSleep " + ex.Message, 1);
                return;
            }
        }

        /*---------------------------------------------------------------
        * Method           : MainProcess() 
        * Muc dich         : thuc hien  qua trinh xu ly cua service
        * Tham so          :  
        *                  
        * Tra ve           : DataTable
        * Ngay tao         : 18/06/2008
        * Nguoi tao        : TrungNV
        * Ngay cap nhat    : 20/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        private void MainProcess()
        {
            try
            {
                //Lib.WriteLogDB(LOG_Info, this.ServiceName + " Bat dau xu li - Start new process...");
                ProcessInquiry.Instance().ProcessService();
            }
            catch
            {
                Lib.WriteLogDB(LOG_Error, "Error GWSWIFTRMInquiry Service", 1);
            }
        }
        #endregion
    }
}
