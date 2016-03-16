using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Data.OracleClient;
using System.ServiceProcess;
using System.Threading;
using System.Text;
//using System.Net.Sockets;

namespace BRIBPSInquiry
{
    public partial class IBPSInquiry : ServiceBase
    {
        //Bien xac dinh trang thai hoat dong cu service
        //=true service se dung hoat dong
        //=false service dang hoat dong.

        public static bool isStop = false;
        public static int timeSleep = 200; // Thoi gian tre cho service

        // Level cua noi dung ghi log
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;
        public static string SERVICE_NAME = "";
        public IBPSInquiry()
        {
            InitializeComponent();
           
        }

        protected override void OnStart(string[] args)
        {
            SERVICE_NAME = this.ServiceName;
            Thread thrService = new Thread(new ThreadStart(ActiveProcess));
            thrService.Start();
        }

        protected override void OnStop()
        {
            //Dat lai trang thai khi dung service
            isStop = true;
            Lib.WriteLogDB(3, SERVICE_NAME + " Onstop",1);
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

                // cap nhat moi 03/12/2008  lay thoi gian delay cua service tu bang GWService_port.
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRIBPSInquiry')";
                datGWType = new DataTable();
                datGWType = Lib.ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();
                //Het cap nhat



                // Lay cac thong tin ve ENCRYPT
                strCmd = "SELECT MSG_No From GWTYPE WHERE GWTYPE = 'IBPS'";
                datGWType = new DataTable();
                datGWType = Lib.ExcuteDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    //timeSleep = Convert.ToInt32(datGWType.Rows[0]["FILETIME"]);
                    ProcessInquiry.m_strMsgNo = datGWType.Rows[0]["MSG_No"].ToString();
                }
                datGWType.Clear();
            }
            catch //(Exception ex)
            {
                timeSleep = 2000;
                //Lib.WriteLogDB(3, " Khong the lay thong tin ve Encrypt trong GWTYPE " + ex.Message);
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
                //Lib.WriteLogDB(LOG_Info, this.ServiceName + " Bat dau xu li - Start new process...");
                ProcessInquiry.Instance().ProcessService();
            }
            catch
            {
                Lib.WriteLogDB(LOG_Error, "Co loi xay ra voi BRIBPSInquiry Service",1);
            }
        }
        #endregion
    }
}
