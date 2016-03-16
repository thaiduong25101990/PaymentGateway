using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using BR.BRBusinessObject;
using System.Net;
using BR.BRLib;


namespace BR.BRSYSTEM
{
    public partial class frmServiceStatus : frmBasedata
    {
        public static int selectedRow;
        public static string strStatus;
        public static string sttrServiceName1;

        private int iRows;
        private int Restast;
        //private bool bStart = true;
        //private bool NeedConfirm = true;
        private string strService1;

        private clsLog objLog = new clsLog();
        private ServiceController ServiceCtrl = new ServiceController();        
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        //khai bao mot mang de chua cac Service
        string[] Service = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                               "", "", "", "", "", "", "" };        
        

        public frmServiceStatus()
        {
            InitializeComponent();            
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*---------------------------------------------------------------
        * Method           : frmServiceStatus_Load(object sender, EventArgs e)
        * Muc dich         : //form load
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmServiceStatus_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                cbSevice.Items.Add("ALL");
                cbSevice.SelectedIndex = 0;  
                Getdatagrid();
                txtIP.Focus();               
            }
            catch(Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }            
        }
        
        /*---------------------------------------------------------------
        * Method           : GetService(string strServiceName,string strServerName)
        * Muc dich         : lay ten cua cac service
        * Tham so          : strServiceName,strServerName
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void GetService(string strServiceName,string strServerName)
        {
            try
            {  
                ServiceController[] services = ServiceController.GetServices(strServerName);
                foreach (ServiceController x in services)
                {
                    if (x.DisplayName == strServiceName)
                    {
                        if (x.Status == System.ServiceProcess.
                                      ServiceControllerStatus.Running)                        
                            x.Stop();                        
                        else                       
                            x.Start();                        
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
       * Method           : Start_stop_Service(string strServiceName, string strServerName)
       * Muc dich         : Start_stop_Service
       * Tham so          : strServiceName,strServerName
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
       *--------------------------------------------------------------*/
        private void Start_stop_Service(string strServiceName, string strServerName)
        {
            int iSucessfull = 0;
            int iStop = 0;
            try
            {                
                ServiceController[] services = ServiceController.GetServices(strServerName);
                foreach (ServiceController x in services)
                {
                    if (x.DisplayName.ToUpper() == strServiceName.ToUpper())
                    {
                        if (x.Status == System.ServiceProcess.
                                      ServiceControllerStatus.Running)
                        {
                            x.Stop();//stopservice 
                            iStop = 1;
                        }
                        else
                        {
                            x.Start();//start Service
                            iSucessfull = 1;
                        }
                    }
                }

                if (iStop == 1 && iSucessfull== 0)
                {
                    dtrService.Rows[iRows].Cells[1].Value = "Stoped";//Starting
                    cmdStart.Enabled = true;
                    cmdRestart.Enabled = false;
                    cmdStop.Enabled = false;
                    if (Restast == 0)
                    {                        
                        Common.ShowError("Stop Servie " + strServiceName.ToUpper() + "successful",
                            7, MessageBoxButtons.OK);
                    }
                }               
                if (iSucessfull == 1 && iStop == 0)
                {
                    dtrService.Rows[iRows].Cells[1].Value = "Started";
                    cmdStart.Enabled = false;
                    cmdRestart.Enabled = true;
                    cmdStop.Enabled = true;
                    if (Restast == 0)
                    {
                        Common.ShowError("Open Servie " + strServiceName.ToUpper() + "successful",
                            7, MessageBoxButtons.OK);                        
                    }
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }       

        /*---------------------------------------------------------------
        * Method           : Getdatagrid()
        * Muc dich         : //lay du lieu len data grid
        * Tham so          : strServiceName,strServerName
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Getdatagrid()
        {
            try
            {
                cmdStart.Enabled = false;
                cmdStop.Enabled = false;
                cmdRestart.Enabled = false;
                dtrService.Rows.Clear();
                if (cbSevice.Text.Trim() != "ALL")
                {
                    int n = 0;
                    while (n < cbSevice.Items.Count)
                    {
                        string strServiceName = cbSevice.Items[n].ToString();
                        ServiceCtrl.DisplayName = strServiceName;//sau thay bang strServiceName
                        string strSta = ServiceCtrl.Status.ToString();
                        dtrService.Rows.Add();
                        dtrService.Rows[n].Cells[0].Value = strServiceName;
                        dtrService.Rows[n].Cells[1].Value = strSta;
                        dtrService.Columns[0].Width = 250;
                        dtrService.Columns[1].Width = 70;
                        dtrService.Columns[0].ReadOnly = true;
                        dtrService.Columns[1].ReadOnly = true;

                        n = n + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        /*---------------------------------------------------------------
        * Method           : dataGrid_MouseDown(object sender, MouseEventArgs e)
        * Muc dich         : // Bat su kien click chuot vao hang trong DataGrid
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            int numR, numC;
            DataGridView.HitTestInfo hit = dtrService.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                numR = hit.RowIndex;
                numC = hit.ColumnIndex;
                selectedRow = numR;
                dtrService.Rows[numR].Selected = true;
            }
            else return;
            if (e.Button == MouseButtons.Left)  // Neu laf Chuot Trai
            {
               
                // Neu la Chuot Trai chi cho chon NHIEU hang
                this.dtrService.MultiSelect = false;
                this.dtrService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                dtrService.Rows[numR].Selected = false;
                sttrServiceName1 = dtrService.CurrentRow.Cells[0].Value.ToString();
                strStatus = dtrService.CurrentRow.Cells[1].Value.ToString();               
            }
        }

        private void Search_All()
        {
            try
            {
                strService1 = "";
                ServiceController[] services;
                //kiem tra xem service co hoat dong hay khong
                string strServ = txtIP.Text.Trim();                
                string strService = cbSevice.Text.Trim();
                
                try
                {
                    services = ServiceController.GetServices(strServ);
                }
                catch (Exception ex)
                {                    
                    Common.ShowError("Cannot open Service Control Manager on computer " + strServ + "" + 
                        "\r\n" + " This operation might require other privileges " + ex.Message,2,MessageBoxButtons.OK);
                    return;
                }
                dtrService.Rows.Clear();                
                dtrService.Columns[0].Width = 235;                
                foreach (ServiceController x in services)
                {                    
                    if (x.DisplayName.ToUpper().Substring(0, 2) == "BR")
                    {
                        if (strService1.Trim() == "")
                        {
                            strService1 = x.DisplayName;
                        }
                        else
                        {
                            strService1 = strService1 + "/" + x.DisplayName;
                        }
                        //strService
                        if (x.Status == System.ServiceProcess.
                                ServiceControllerStatus.Running)
                        {
                            if (dtrService.Rows.Count == 0)
                            {
                                dtrService.Rows.Add();
                                dtrService.Rows[0].Cells[0].Value = x.DisplayName;
                                dtrService.Rows[0].Cells[1].Value = "Started";
                                dtrService.Columns[0].ReadOnly = true;
                                dtrService.Columns[1].ReadOnly = true;
                            }
                            else
                            {
                                int count = dtrService.Rows.Count;
                                int j = 0;
                                while (j < dtrService.Rows.Count)
                                {
                                    if (j == count - 1)
                                    {
                                        dtrService.Rows.Add();
                                        dtrService.Rows[count].Cells[0].Value = x.DisplayName;
                                        dtrService.Rows[count].Cells[1].Value = "Started";
                                        dtrService.Rows[count].Cells[1].ReadOnly = false;
                                        dtrService.Columns[0].ReadOnly = true;
                                        dtrService.Columns[1].ReadOnly = true;
                                    }
                                    j = j + 1;
                                }
                            }
                        }
                        else
                        {
                            if (dtrService.Rows.Count == 0)
                            {
                                dtrService.Rows.Add();
                                dtrService.Rows[0].Cells[0].Value = x.DisplayName;
                                dtrService.Rows[0].Cells[1].Value = "Stoped";
                                dtrService.Columns[0].ReadOnly = true;
                                dtrService.Columns[1].ReadOnly = true;
                            }
                            else
                            {
                                int count1 = dtrService.Rows.Count;
                                int g = 0;
                                while (g < dtrService.Rows.Count)
                                {
                                    if (g == count1 - 1)
                                    {
                                        dtrService.Rows.Add();
                                        dtrService.Rows[count1].Cells[0].Value = x.DisplayName;
                                        dtrService.Rows[count1].Cells[1].Value = "Stoped";
                                        dtrService.Columns[0].ReadOnly = true;
                                        dtrService.Columns[1].ReadOnly = true;
                                    }
                                    g = g + 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            //goi ham day du lieu vao com bo box
            Add_service();
        }

        private void Add_service()
        {
            try
            {
                cbSevice.Items.Clear();
                if (strService1.Trim() != "")
                {
                    //cat chuoi
                    String[] M = strService1.Split(new String[] { "/" }, StringSplitOptions.None);                    
                    int k = M.Count<String>();
                    cbSevice.Items.Add("ALL");
                    cbSevice.SelectedIndex = 0;
                    int i = 0;
                    while (i < k)
                    {
                        cbSevice.Items.Add(M[i]);
                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        /*---------------------------------------------------------------
        * Method           : cmdSearch_Click(object sender, EventArgs e)
        * Muc dich         : kiem tra xem service co hoat dong hay khong
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSevice.Text.Trim() == "ALL")
                {
                    Search_All();
                }
                else
                {
                    Search_One();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }      
        }

        private void Search_One()
        {
            try
            {
                ServiceController[] services;
                //kiem tra xem service co hoat dong hay khong
                string strServ = txtIP.Text.Trim();                
                string strService = cbSevice.Text.Trim();
                
                try
                {
                    services = ServiceController.GetServices(strServ);
                }
                catch (Exception ex)
                {                    
                    Common.ShowError("Cannot open Service Control Manager on computer " + strServ + "" + 
                        "\r\n" + " This operation might require other privileges ; " + ex.Message , 2, MessageBoxButtons.OK);
                    return;
                }
                dtrService.Rows.Clear();
                dtrService.Rows.Add();
                dtrService.Columns[0].Width = 235;
                dtrService.Rows[0].Cells[0].ReadOnly = false;
                foreach (ServiceController x in services)
                {
                    if (x.DisplayName.ToUpper() == strService.ToUpper().ToUpper())
                    {
                        if (x.Status == System.ServiceProcess.
                                      ServiceControllerStatus.Running)
                        {
                            //x.Stop();//stopservice
                            dtrService.Rows[0].Cells[0].Value = cbSevice.Text.Trim();
                            dtrService.Rows[0].Cells[1].Value = "Started";
                            dtrService.Rows[0].Cells[1].ReadOnly = false;
                            dtrService.Rows[0].Cells[1].ReadOnly = true;
                            cmdStop.Enabled = true;
                            cmdStart.Enabled = true;
                            cmdRestart.Enabled = true;
                        }
                        else
                        {
                            //x.Start();//start Service
                            dtrService.Rows[0].Cells[0].Value = cbSevice.Text.Trim();
                            dtrService.Rows[0].Cells[1].Value = "Stoped";
                            dtrService.Rows[0].Cells[1].ReadOnly = true;
                            dtrService.Rows[0].Cells[0].ReadOnly = true;
                            cmdStop.Enabled = false;
                            cmdStart.Enabled = true;
                            cmdRestart.Enabled = false;
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : cmdStop_Click(object sender, EventArgs e)
        * Muc dich         : //stop ca service
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdStop_Click(object sender, EventArgs e)
        {
            try
            {
                //bStart = false;
                //------lay thong tin service-----------------------
                string strServer = txtIP.Text;
                string strService = dtrService.Rows[iRows].Cells[0].Value.ToString();
                Start_stop_Service(strService, strServer);
                //--------------------------------------------------
                //lay service tu may server
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Stop Service";
                int Log_level = 1;
                string strWorked = "Stop_Service";
                string strTable = "";
                string strOld_value = "";
                string strNew_value = "";

                //Ghi log
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                    strWorked, strTable, strOld_value, strNew_value);
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : cmdStart_Click(object sender, EventArgs e)
        * Muc dich         : //start cac service
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdStart_Click(object sender, EventArgs e)
        {
            try
            {
                //bStart = true;
                //-----------------------------------------------
                string strServer = txtIP.Text;
                string strService = dtrService.Rows[iRows].Cells[0].Value.ToString();
                Start_stop_Service(strService, strServer.Trim());
                //-----------------------------------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Start Service";
                int Log_level = 1;
                string strWorked = "Start_Service";
                string strTable = "";
                string strOld_value = "";
                string strNew_value = "";
                //-----------------------------------------
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                    strWorked, strTable, strOld_value, strNew_value);
                //--------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : cmdRestart_Click(object sender, EventArgs e)
        * Muc dich         : ///restart cac service
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdRestart_Click(object sender, EventArgs e)
        {
            try
            {
                Restast = 1;
                //-------------------------------------
                string strServer = txtIP.Text;
                string strService = cbSevice.Text;
                Start_stop_Service(strService, strServer);//stop
                Start_stop_Service(strService, strServer);//start                
                Common.ShowError("Restart Servie successful",7,MessageBoxButtons.OK);
                //------------------------------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Restart service";
                int Log_level = 1;
                string strWorked = "ReStart_Service";
                string strTable = "";
                string strOld_value = "";
                string strNew_value = "";
                //-----------------------------------------
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                    strWorked, strTable, strOld_value, strNew_value);
                //--------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            Restast = 0;
        }

        private void frmServiceStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        /*---------------------------------------------------------------
        * Method           : Getdata()
        * Muc dich         : //lay lai trang thai cua service
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Getdata()
        {
            try
            {
                //kiem tra xem service co hoat dong hay khong
                string strServ = txtIP.Text;
                string strService = cbSevice.Text;
                
                ServiceController[] services = ServiceController.GetServices(strServ);
                dtrService.Rows.Clear();
                dtrService.Rows.Add();
                dtrService.Rows[0].Cells[0].Value = cbSevice.Text;
                dtrService.Columns[0].Width = 235;
                dtrService.Rows[0].Cells[0].ReadOnly = false;
                foreach (ServiceController x in services)
                {
                    if (x.DisplayName == strService)
                    {
                        if (x.Status == System.ServiceProcess.
                                      ServiceControllerStatus.Running)
                        {
                            //x.Stop();//stopservice
                            dtrService.Rows[0].Cells[1].Value = "Started";
                            dtrService.Rows[0].Cells[1].ReadOnly = false;
                            cmdStop.Enabled = true;
                            cmdStart.Enabled = false;
                            cmdRestart.Enabled = true;
                        }
                        else
                        {
                            //x.Start();//start Service
                            dtrService.Rows[0].Cells[1].Value = "Stoped";
                            dtrService.Rows[0].Cells[1].ReadOnly = false;
                            cmdStop.Enabled = false;
                            cmdStart.Enabled = true;
                            cmdRestart.Enabled = false;
                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cbSevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dtrService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
                sttrServiceName1 = dtrService.Rows[iRows].Cells[0].Value.ToString();
                strStatus = dtrService.Rows[iRows].Cells[1].Value.ToString();
                if (strStatus.Trim() == "Started")
                {
                    cmdRestart.Enabled = true;
                    cmdStop.Enabled = true;
                    cmdStart.Enabled = false;
                }
                else if (strStatus.Trim() == "Stoped")
                {
                    cmdRestart.Enabled = false;
                    cmdStop.Enabled = false;
                    cmdStart.Enabled = true;
                }
                else
                {
                    cmdRestart.Enabled = false;
                    cmdStop.Enabled = false;
                    cmdStart.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtrService_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
                sttrServiceName1 = dtrService.Rows[iRows].Cells[0].Value.ToString();
                strStatus = dtrService.Rows[iRows].Cells[1].Value.ToString();
                if (strStatus.Trim() == "Started")
                {
                    cmdRestart.Enabled = true;
                    cmdStop.Enabled = true;
                    cmdStart.Enabled = false;
                }
                else if (strStatus.Trim() == "Stoped")
                {
                    cmdRestart.Enabled = false;
                    cmdStop.Enabled = false;
                    cmdStart.Enabled = true;
                }
                else
                {
                    cmdRestart.Enabled = false;
                    cmdStop.Enabled = false;
                    cmdStart.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmServiceStatus_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    if ((this.ActiveControl as Button).Name == "cmdSearch")
                    {
                        cmdSearch_Click(null, null);
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }        
       

        private void frmServiceStatus_MouseDown(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmServiceStatus_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dtrService_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
