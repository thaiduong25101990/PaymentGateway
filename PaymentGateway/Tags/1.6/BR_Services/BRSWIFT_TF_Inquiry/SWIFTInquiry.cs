using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace BRSWIFTTFInquiry
{
    public partial class SWIFTTFInquiry : ServiceBase
    {
        public static bool isStop = false;
        public static int timeSleep = 200; // Thoi gian tre cho service mac dinh la 200

        public static string SERVICE_NAME = "";
        public SWIFTTFInquiry()
        {
            InitializeComponent();
           
            // Test Service
            //ActiveProcess();// Test

        }               
        
        protected override void OnStart(string[] args)
        {
            SERVICE_NAME = this.ServiceName.Trim();
            Lib.WriteLogDB(1, SERVICE_NAME + " start service",1);
            Thread thrService = new Thread(new ThreadStart(ActiveProcess));
            thrService.Start();
        }

        protected override void OnStop()
        {
            //Dat lai trang thai khi dung service
            isStop = true;
            Lib.WriteLogDB(2, SERVICE_NAME + " stop service",1);
        }
        #region Thuc hien goi cac ham xu ly cua service
        /*---------------------------------------------------------------
        * Method           : ActiveProcess() 
        * Muc dich         : Goi Ham thuc hien  qua trinh xu ly cu service
        * Tham so          :  
        *                  
        * Tra ve           : DataTable
        * Ngay tao         : 07/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 07/05/2008
        * Nguoi cap nhat   : QuanLD
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
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRSWIFTTFInquiry')";
                datGWType = new DataTable();
                datGWType = Lib.Select_ReturnDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();


                //DataTable datGWType = new DataTable();
                //// Lay cac thong tin ve ENCRYPT
                //string strCmd = "SELECT FILETIME From GWTYPE WHERE GWTYPE = 'SWIFT'";

                //datGWType = new DataTable();
                //datGWType = Lib.Select_ReturnDataTable(strCmd);
                //if (datGWType.Rows.Count > 0)
                //{
                //    timeSleep = Convert.ToInt32(datGWType.Rows[0]["FILETIME"]);
                //}
                //datGWType.Clear();
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(3, " Khong the lay thong tin ve Encrypt trong GWTYPE " + ex.Message,1);
                return;
            }
        }

        /*---------------------------------------------------------------
        * Method           : MainProcess() 
        * Muc dich         : thuc hien  qua trinh xu ly cua service
        * Tham so          :  
        *                  
        * Tra ve           : DataTable
        * Ngay tao         : 07/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 07/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private void MainProcess()
        {
            try
            {
                
                ProcessInquiry.Instance().ProcessService();
            }
            catch 
            {
                Lib.WriteLogDB(0, SERVICE_NAME + " bi loi khi thuc hien MainProcess",1);
            }
            
            
          
        }
        #endregion

    }
}
