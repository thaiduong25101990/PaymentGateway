using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using BR.BRBusinessObject;
using System.IO;
using BR.BRLib;
using BR.BRInterBank.Reconcile;


namespace BR.BRInterBank
{
    public partial class frmVCBRec : BR.BRSYSTEM.frmBasedata
    {
        /*************************************
         * Khai bao doi tuong
         *************************************/
        private GetData objGetData = new GetData();
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();

        VCB_MSG_REC_TEMPInfo dsMsgVCBRec = new VCB_MSG_REC_TEMPInfo();
        VCB_MSG_REC_TEMPController dsMsgVCBRecControl = new VCB_MSG_REC_TEMPController();

        IBPS_MSG_REC_TADInfo dsMsgRecTad = new IBPS_MSG_REC_TADInfo();
        IBPS_MSG_REC_TADController dsMsgRecTadControl = new IBPS_MSG_REC_TADController();

        VCB_RECONCILEInfo objVCBRec = new VCB_RECONCILEInfo();
        VCB_RECONCILEController objVCBRecControl = new VCB_RECONCILEController();

        VCB_MSG_CONTENTInfo objVCB = new VCB_MSG_CONTENTInfo();
        VCB_MSG_CONTENTController objVCBControl = new VCB_MSG_CONTENTController();

        IBPS_BANK_MAPController objIBPSBankControl = new IBPS_BANK_MAPController();

        SYSVARController objSysvarControl = new SYSVARController();

        /**********************************
         * Khai bao bien
         **********************************/
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;

        public string strDirection;
        public string strDepartment;
        public string strExpre;
        public DateTime strDate;
        //public DateTime dtmDate;
        public string strType;
        public string strMsgType;
        public int intI;
        //int iSIBS = 0;
        //int iVCB = 0;
        public int intResult;
        public string strExportFolder = ""; /* Duong dan thu muc chua file txt */
        public string strFileName = ""; /*FileName*/
        //string strPath = "";
        public string direction;
        //public string OnDate;
        public string dtfrom;
        public string view;
        //Boolean blnFlag; /* bIsReadFileAgain */
        string[] strV = new string[2] { "BR-VCB" , "VCB-BR"};
        string[] strS = new string[2] { "BR-SIBS", "SIBS-BR" };
        string[] strR = new string[2] { "BR-TR"  , "TR-BR" };
        string[] strMsgTypes = new string[3] {"MT103","MT202","MT199"};
        //string vResultSIBS = "";// Biến lưu trữ kết quả đối chiếu SIBS
        //string vResultVCB = "";// Biến lưu trữ kết quả đối chiếu VCB
        public DataTable dtReconcile;//DatHM add them phuc vu cho in doi chieu

        public frmVCBRec()
        {
            InitializeComponent();
        }

        /**************************************
         * Su kien Load Form
         **************************************/
        private void frmVCBRec_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            cmdPrint.Enabled = false; 
            //blnFlag = true; /* On Flag */
            //txtTotal.ReadOnly = true;
            //cbInOut.Text = "O";
            
            pickerDate.MaxDate = DateTime.Now;
            cbSIBS_GW.SelectedIndex = 0;
            cbVCB_GW.SelectedIndex = 0;
            cbTR_GW.SelectedIndex = 0;
            //datMessage.ReadOnly = true;
            strDate = pickerDate.Value;

            
            ////cbdepartment.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbdepartment, "CONTENT", "Department", "SYSTEM");
            ////cbdepartment.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbdepartment, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='SYSTEM' and cdname='Department'", "CONTENT", true, true, "ALL"))
                return;                                    
            ////cbdirection.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbdirection, "CONTENT", "DIRECTION", "VCB");
            ////cbdirection.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbdirection, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='VCB' and cdname='Department'", "CONTENT", true, true, "ALL"))
                return;
                                    
            ////cbtype.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbtype, "CONTENT", "TYPE_REC", "VCB");
            ////cbtype.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbtype, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='VCB' and cdname='TYPE_REC'", "CONTENT", true, true, "ALL"))
                return;
            

            #region MsgType
            cbMsgType.Items.Clear();
            cbMsgType.Items.Add("ALL");
            cbMsgType.Items.Add("MT103");
            cbMsgType.Items.Add("MT202");
            cbMsgType.Items.Add("MT199");
            cbMsgType.SelectedIndex = 0;
            #endregion

            // Lay duong dan den thu muc luu file text o may chu
            #region PATH_REC
            //DataSet dsSysTemp = new DataSet();
            //dsSysTemp = objSysvarControl.GetSYSVAR_NAME("PATH_REC", "SYSTEM");
            //if (dsSysTemp.Tables[0].Rows.Count == 0)
            //    MessageBox.Show("Cannot find reconcile file!");
            //else strExportFolder = dsSysTemp.Tables[0].Rows[0]["VALUE"].ToString();
            strExportFolder = GetData.GetRecParameter("PATH_REC", "SYSTEM", "PATH");
            if (strExportFolder == "") { MessageBox.Show("Cannot find PATH_REC SIBS-VCB"); return; }
            pickerDate.Focus();
            #endregion

            //objVCBRecControl.ClearVCB_RECONCILE(strDate.Date.ToString(), strDepartment);
        }
        /**************************************
         * Su kien Close Form
         **************************************/
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**************************************
         * Su kien kich hoat nut Reconcile
         **************************************/
        private void cmdReconcile_Click(object sender, EventArgs e)
        {
            
            //txtTotal.Text = "";
            // Kiem tra thong tin comboBox
            if (Check() < 0) return;
            // Lay thong tin truong Direction
            #region Direction
            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            }   else strDirection = cbdirection.Text.Trim();
            #endregion
            // Lay thong tin truong Department
            #region Department
            strDepartment = cbdepartment.Text.Trim();
            #endregion            
            // Lay thong tin truong Date
            #region Date
            strDate = pickerDate.Value;
            #endregion            
            // Lay thong tin truong Type
            #region Type
            strType = "ALL";
            if (cbtype.Text == strV[0] || cbtype.Text == strV[1]) { strType = "VCB-BR"; }
            if (cbtype.Text == strS[0] || cbtype.Text == strS[1]) { strType = "SIBS-BR";}
            if (cbtype.Text == strR[0] || cbtype.Text == strR[1]) { strType = "TR-BR"; }
            #endregion            
            // Lay thong tin truong View
            #region View
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            #endregion
            
            int iResult = clsVCB_MSG_REC.RECONCILE_SIBS(pickerDate.Value,Common.Userid);
            if (iResult < 0) MessageBox.Show("Cannot Reconcile with SIBS!");
            int iResult1 = clsVCB_MSG_REC.RECONCILE_VCB(pickerDate.Value, Common.Userid); ;
            if (iResult1 < 0) MessageBox.Show("Cannot Reconcile with VCB!");
            int iResult2 = clsVCB_MSG_REC.RECONCILE_TR(pickerDate.Value, Common.Userid); ;
            if (iResult2 < 0) MessageBox.Show("Cannot Reconcile with TR!");

            if ((iResult >= 0) && (iResult1 >= 0)) { MessageBox.Show("Reconcile successfully!!", Common.sCaption); }
            
            //LoadData();
            datMessage.DataSource = null;
        }

        /*******************************************
         * Reconcile VCB - GW 
         *******************************************/ 
        private int ReconcileVCB(DateTime dtmDate, string strDepart, string strDirec)
        {
            int intResult1 = 0;
            //iVCB = 0;
            intResult = BR.BRLib.Common.GW_RECONCILE_SUCCESS;
                    
            // Xóa bảng SWIFT_MSG_CONTENT_TEMP
            // intResult = objVCBControl.DeleteVCB_MSG_CONTENT_Temp();

            // Doc cac dien da nhan vao GW vao bang VCB_MSG_CONTENT_TEMP
            // intResult1 = objVCBControl.InsertVCB_MSG_CONTENT_Temp(dtmDate, "VCB-BR");
            if (intResult < 0 && intResult1<0) return intResult;
            else
            {
                // Đối chiếu
                intResult = objVCBRecControl.VCB_Reconcile_VCB(dtmDate);
                //return intResult;
                if (intResult < 0)
                {MessageBox.Show("Cannot Reconcile VCB");return intResult;}
                else
                {
                    // Lưu vào bảng ALL
                    intResult = objVCBRecControl.InsertVCB_MSG_REC_ALL(dtmDate); 
                    if (intResult < 0)
                    {MessageBox.Show("Cannot Insert VCB_MSG_REC_ALL!");return intResult;}
                    else
                    {
                        // Lưu vào bảng TOTAL
                        intResult = objVCBRecControl.InsertVCB_MSG_REC_TOTAL(dtmDate);
                        if (intResult < 0){MessageBox.Show("Cannot Insert VCB_MSG_REC_TOTAL!");return intResult;}
                        // Thống kế kết quả đối chiếu
                        
                        return intResult;
                    }
                }
            }            
            //return intResult;
        }

        /*******************************************
         * Reconcile SIBS-BR 
         *******************************************/ 
        private int ReconcileSIBS(DateTime dtmDate, string strDepart, string strDirec)
        {
            //int intResult1 = 0;
            //int intResult2 = 0;
            //iSIBS = 0;

            intResult = BR.BRLib.Common.GW_RECONCILE_SUCCESS;
                        
            // Xoa bang VCB_MSG_CONTENT_TEMP
            //intResult = objVCBControl.DeleteVCB_MSG_CONTENT_Temp();            
            if (intResult < 0) return intResult;
            else
            {
                // Đối chiếu
                intResult = objVCBRecControl.VCB_Reconcile(dtmDate);
                 if (intResult < 0)
                {MessageBox.Show("Cannot Reconcile VCB");return intResult;}
                else
                {
                    // Lưu điện vào bảng ALL
                    intResult = objVCBRecControl.InsertVCB_MSG_REC_ALL(dtmDate);
                    if (intResult < 0){MessageBox.Show("Cannot Insert VCB_MSG_REC_ALL!");return intResult;}
                    // Lưu điện vào bảng TOTAL
                    else
                    {
                        intResult = objVCBRecControl.InsertVCB_MSG_REC_TOTAL(dtmDate);
                        if (intResult < 0){MessageBox.Show("Cannot Insert VCB_MSG_REC_TOTAL!");return intResult;}
                        // Thống kế kết quả đối chiếu
                        
                        return intResult;
                    }
                }

            }
            
            //return intResult;
        }

        /*******************************************
         * Doc file txt do VCB tra ve
         *******************************************/         

        /*******************************************
         * Doc file txt cua SIBS 
         *******************************************/ 
        
        /**********************************
         * Load du lieu len DataGrid
         **********************************/
        private void LoadData()
        {

            // Lay thong tin truong Direction
            #region Direction
            string strDirection = "ALL";
            if (cbdirection.Text == "SIBS-VCB") { strDirection = "O"; }
            if (cbdirection.Text == "VCB-SIBS") { strDirection = "I"; }
            #endregion
            // Lay thong tin truong Department
            #region Department
            strDepartment = cbdepartment.Text.Trim();
            #endregion
            // Lay thong tin truong Date
            #region Date
            strDate = pickerDate.Value;
            #endregion
            // Lay thong tin truong Type
            #region Type
            strType = "ALL";
            if (cbtype.Text == strV[0] || cbtype.Text == strV[1]) { strType = "VCB-BR"; }
            if (cbtype.Text == strS[0] || cbtype.Text == strS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text == strR[0] || cbtype.Text == strR[1]) { strType = "TR-BR"; }
            #endregion
            // Lay thong tin truong View
            #region View
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable dtView;
                dtView = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = dtView.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            #endregion
            // Lay thong tin truong MsgType
            #region MsgType
            strMsgType = cbMsgType.Text;
            #endregion

            DataTable dt;
            //if (strDirection == "Inward") strDirection = "I"; else strDirection = "O";
            dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, strType, strDirection, strDepartment,strExpre,strMsgType);
            datMessage.DataSource = dt;
            dtReconcile = dt;
            try
            {
                //DatHM add
                if (dt.Rows.Count > 0)
                {
                    cmdPrint.Enabled = true;
                }
                else
                {
                    cmdPrint.Enabled = false;
                }
                //DatHM add
            }
            catch { }
            
        }

        
        /**********************************
         * Kiem tra thong tin comboBox
         **********************************/ 
        private int Check()
        {
            if (string.IsNullOrEmpty(cbdirection.Text.Trim()))
            {
                MessageBox.Show("You must choice Direction");
                cbdirection.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbdepartment.Text.Trim()))
            {
                MessageBox.Show("You must choice Department");
                cbdepartment.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(strDate.ToString()))
            {
                MessageBox.Show("You must choice Date");
                pickerDate.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbtype.Text.Trim()))
            {
                MessageBox.Show("You must choice Type");
                cbtype.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbview.Text.Trim()))
            {
                MessageBox.Show("You must choice View");
                cbview.Focus();
                return -1;
            }
            return 1;
        }

        /**************************************
         * Su kien thay doi ngay
         **************************************/
        private void pickerDate_ValueChanged_1(object sender, EventArgs e)
        {
            strDate = pickerDate.Value;
        }

        /**************************************
         * Su kien kich hoat nut Search
         **************************************/
        private void OnSearch(object sender, EventArgs e)
        {
            try { LoadData(); }
            catch { MessageBox.Show("Cannot view Data"); }
        }

        /**************************************
         * Su kien thay doi direction
         **************************************/
        private void OnDirection(object sender, EventArgs e)
        {
            if (cbdirection.Text == "SIBS-VCB")
            {
                cbtype.DataSource = null;
                cbtype.Items.Clear();
                cbtype.Items.AddRange(new string[3]{"ALL", "SIBS-BR", "BR-VCB"}); 
                cbtype.SelectedIndex = 0; 
                cbview.SelectedIndex = 0; 
            }
            if (cbdirection.Text == "VCB-SIBS")
            {
                cbtype.DataSource = null;
                cbtype.Items.Clear(); 
                cbtype.Items.AddRange(new string[3]{"ALL", "VCB-BR", "BR-SIBS"}); 
                cbtype.SelectedIndex = 0; 
                cbview.SelectedIndex = 0; 
            }
            strDirection = cbdirection.Text;
        }
        /**************************************
         * Su kien thay doi MsgType
         **************************************/
        private void OnMsgType(object sender, EventArgs e)
        {
            strMsgType = cbMsgType.Text;
        }

        private void OnSIBS(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbSIBS_GW.Text == "SIBS->BR")
            {
                dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, "SIBS-BR", "O", "ALL","ALL","ALL");
            }
            if (cbSIBS_GW.Text == "SIBS<-BR")
            {
                dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, "SIBS-BR", "I", "ALL", "ALL", "ALL");
            }
            datMessage.DataSource = dt;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 0, 4, 5, 6, 7, 8, 9 }, style);
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 1, 2 }, style1);
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.Alignment = DataGridViewContentAlignment.MiddleRight;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 3 }, style2);
            Tools.DataGrid.SetWidth(ref datMessage, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new int[] { 80, 140, 140, 200, 80, 60, 220, 140, 140, 80 });
        }

        private void OnVCB(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbVCB_GW.Text == "BR->VCB")
            {
                dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, "VCB-BR", "O", "ALL", "ALL", "ALL");
            }
            if (cbVCB_GW.Text == "BR<-VCB")
            {
                dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, "VCB-BR", "I", "ALL", "ALL", "ALL");
            }
            datMessage.DataSource = dt;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 0, 4, 5, 6, 7, 8, 9 }, style);            
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 1, 2 }, style1);
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.Alignment = DataGridViewContentAlignment.MiddleRight;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 3 }, style2);
            Tools.DataGrid.SetWidth(ref datMessage, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new int[] { 80, 140, 140, 200, 80, 60, 220, 140, 140, 80 });
        }

        private void OnDate(object sender, EventArgs e)
        {
            strDate = pickerDate.Value;
        }

        private void OnType(object sender, EventArgs e)
        {
            string vType = "";
            ////cbview.Items.Clear();
            if (cbtype.Text.Trim() == "ALL")
            {
                ////cbview.Items.Add("ALL");
                ////GetData.getDataComboAllCode(cbview, "CONTENT", "RecView", "VCB");
                //////cbview.SelectedValue = 0;
                ////cbview.SelectedIndex = 0;
                ////vType = "ALL";
                if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='VCB' and cdname='RecView'", "CONTENT", true, true, "ALL"))
                    return;

            }
            else
            {                
                if (cbtype.Text.Trim() == strS[0] || cbtype.Text.Trim() == strS[1])
                { vType = "SIBS-BR"; }
                else { vType = "VCB-BR"; }
                ////cbview.Items.Add("ALL");
                ////GetData.getDataComboAllCode_Direc(cbview, "CONTENT", "RecView", "VCB", vType);
                //////cbview.SelectedValue = 0;
                ////cbview.SelectedIndex = 0;
                if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='VCB' and cdname='RecView' and DIRECTION='" + vType + "'", 
                    "CONTENT", true, true, "ALL"))
                    return;
            }
            strType = vType;   
        }

        private void OnView(object sender, EventArgs e)
        {
            if (cbview.Text.Trim() == "System.Data.DataRowView")
                return;
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            try
            {
                lbInformation.Text = "Total messages : " + datMessage.Rows.Count.ToString();
            }
            catch { }
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }
        /*************************
         * Check du lieu
         *************************/ 
        private void OnCheckData(object sender, EventArgs e)
        {
            try
            {
                txtSIBS_GW.Text = clsVCB_MSG_REC.GetVCB_Index(pickerDate.Value, cbSIBS_GW.Text);

                txtVCB_GW.Text = clsVCB_MSG_REC.GetVCB_Index(pickerDate.Value, cbVCB_GW.Text);

                txtTR_GW.Text = clsVCB_MSG_REC.GetVCB_Index(pickerDate.Value, cbTR_GW.Text);                
            }
            catch
            {

            }
        }
        // Lay du lieu 
        private void OnGetData(object sender, EventArgs e)
        {
            int iResult = clsVCB_MSG_REC.InsertVCB_MSG_CONTENT_Temp(pickerDate.Value);
            if (iResult < 0) { MessageBox.Show("Cannot Insert VCB_MSG_CONTENT_temp ", Common.sCaption); }
            int iResult1 = clsVCB_MSG_REC.InsertVCB_MSG_REC_TEMP(pickerDate.Value);
            if (iResult1 < 0) { MessageBox.Show("Cannot Insert VCB_MSG_REC_temp ", Common.sCaption); }
            if (iResult > 0 && iResult1 > 0) { MessageBox.Show("Get data successfully !! ", Common.sCaption); }
        }

        private void OnGW(object sender, EventArgs e)
        {
            DataTable dt = clsVCB_MSG_REC.GetVCB_MSG_CONTENT(pickerDate.Value);
            datMessage.DataSource = dt;
        }

        private void OnTR(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbTR_GW.Text == "TR->BR")
            {
                dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, "TR-BR", "O", "ALL", "ALL", "ALL");
            }
            if (cbTR_GW.Text == "TR<-BR")
            {
                dt = clsVCB_MSG_REC.GetVCB_MSG_REC(pickerDate.Value, "TR-BR", "I", "ALL", "ALL", "ALL");
            }
            datMessage.DataSource = dt;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 0, 4, 5, 6, 7, 8, 9 }, style);
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 1, 2 }, style1);
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.Alignment = DataGridViewContentAlignment.MiddleRight;
            Tools.DataGrid.SetStyles(ref datMessage, new int[] { 3 }, style2);
            Tools.DataGrid.SetWidth(ref datMessage, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new int[] { 80, 140, 140, 200, 80, 60, 220, 140, 140, 80 });
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrint frmPrint = new frmPrint();
                string Print = "VCB_PRINT_RECONCILE";
                frmPrint.PrintType = Print;
                view = cbview.Text.Trim();
                dtfrom = pickerDate.Text.ToString();
                direction = cbdirection.Text.Trim();
                frmPrint.view = view;
                frmPrint.dtfrom = dtfrom;
                frmPrint.Direction = direction;
                frmPrint.HMdat = dtReconcile;
                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Error:" + ex, Common.sCaption);
            }
        }
    }
}
