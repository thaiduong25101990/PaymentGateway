using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Data.OracleClient;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace BRIBPSMaintaince
{
    public partial class IBPSMaintaince : ServiceBase
    {
        public static bool isStop = false;
        public static int timeSleep = 2000; // Thoi gian tre cho service mac dinh la 200

        public static string SERVICE_NAME = "";

        public IBPSMaintaince()
        {
            InitializeComponent();
            ActiveProcess();

        }

        protected override void OnStart(string[] args)
        {
            SERVICE_NAME = this.ServiceName.Trim();
            isStop = false;
            Lib.WriteLogDB(1, "IBPSMaintaince OnStart",1);
            Thread thrService = new Thread(new ThreadStart(ActiveProcess));
            thrService.Start();
        }

        protected override void OnStop()
        {
            Lib.WriteLogDB(3, "IBPSMaintaince OnStop",1);
            //Dat lai trang thai khi dung service
            isStop = true;
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
        * Ngay cap nhat    : 18/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        private void ActiveProcess()
        {
            SERVICE_NAME = this.ServiceName.Trim();
            while (!isStop)
            {
                // Lay cac gia tri truoc khi xu li
                BeforeProcess();
                // Bat dau xu li
                MainProcess();
                Thread.Sleep(timeSleep);// dat do tre laf timeSleep mini giay
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
                string strCmd = "select Timedelay from Gwservice_Port Where upper(SERVICENAME)=upper('BRIBPSMaintaince')";
                datGWType = new DataTable();
                datGWType = Lib.Select_ReturnDataTable(strCmd);
                if (datGWType.Rows.Count > 0)
                {
                    timeSleep = Convert.ToInt32(datGWType.Rows[0]["Timedelay"]);
                }
                datGWType.Clear();
                //Het cap nhat


               
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(3, " Get Delay time failed " + ex.Message,1);
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
        * Ngay cap nhat    : 18/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        private void MainProcess()
        {
            try
            {
               
                new ProcessMaintaince().ProcessService();
            }
            catch
            {
                Lib.WriteLogDB(3, "GW-IBPS-Maintiance Service start failed",1);
            }



        }
        #endregion
    }
}
