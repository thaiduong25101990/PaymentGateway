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
using BR.BRTTSB.Reconcile;

namespace BR.BRTTSB
{
    public partial class frmTTSPRec : BR.BRSYSTEM.frmBasedata
    {
        /****************************
        * Khai bao doi tuong cuc bo
        *****************************/
        private GetData objGetData = new GetData();
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();

        MSG_CONTENT_TEMPInfo dsMsgTemp = new MSG_CONTENT_TEMPInfo();
        MSG_CONTENT_TEMPController dsMsgTempControl = new MSG_CONTENT_TEMPController();

        TTSP_MSG_REC_TEMPInfo dsMsgRec = new TTSP_MSG_REC_TEMPInfo();
        TTSP_MSG_REC_TEMPController dsMsgRecControl = new TTSP_MSG_REC_TEMPController();

        SWIFT_MSG_REC_TEMPInfo dsMsgSwiftRec = new SWIFT_MSG_REC_TEMPInfo();
        SWIFT_MSG_REC_TEMPController dsMsgSwiftRecControl = new SWIFT_MSG_REC_TEMPController();

        IBPS_MSG_REC_TADInfo dsMsgRecTad = new IBPS_MSG_REC_TADInfo();
        IBPS_MSG_REC_TADController dsMsgRecTadControl = new IBPS_MSG_REC_TADController();

        TTSP_RECONCILEInfo objTTSPRec = new TTSP_RECONCILEInfo();
        TTSP_RECONCILEController objTTSPRecControl = new TTSP_RECONCILEController();

        SWIFT_MSG_CONTENTInfo objSwift = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objSwiftControl = new SWIFT_MSG_CONTENTController();

        TTSP_MSG_CONTENTInfo objTTSP = new TTSP_MSG_CONTENTInfo();
        TTSP_MSG_CONTENTController objTTSPControl = new TTSP_MSG_CONTENTController();

        IBPS_BANK_MAPController objIBPSBankControl = new IBPS_BANK_MAPController();

        IQS_CONDITIONController objIQSControl = new IQS_CONDITIONController();

        SYSVARController objSysvarControl = new SYSVARController();

        /*********************************
         * Khai bao bien 
         *********************************/
        //private bool NeedConfirm = true;
        public string strDirection;
        public string strDepartment;
        public string strExpre;
        public DateTime strDate;
        public string strType;
        public string strMsgType;
        public string[] vTypesT = new string[2] { "TTSP-BR" , "BR-TTSP"};
        public string[] vTypesS = new string[2] { "SIBS-BR" , "BR-SIBS" };
        public string[] vTypesR = new string[2] { "TR-BR"   , "BR-TR" };

        public string[] vMsgTypes = new string[5] {"MT100","MT103","MT195","MT196","MT199"};

        public int intI;
        int iSIBS = 0; 
        //int iTTSP = 0;
        //string vResultSIBS = "";// Biến lưu trữ kết quả đối chiếu SIBS
        string vResultTTSP = "";// Biến lưu trữ kết quả đối chiếu TTSP

        public int intResult;
        public int intResult1;
        public string strExportFolder = "";/* Bien thu muc chua file SIBS [PATH_REC]*/ 
        public string strFileName = ""; /* Bien su dung de luu ten file */
        string strPath = "";/**/
        string strDBLink_Name = "";/*dblink_name*/
        //Boolean blnFlag; /* bit doc file */
        public DataTable dtReconcile;//DatHM add them phuc vu cho in doi chieu
        /*********************************
         * Khoi tao
         *********************************/
        public frmTTSPRec()
        {
            InitializeComponent();
        }

        /*********************************
         * Su kien kich nut Rec_Load
         *********************************/
        private void frmTTSPRec_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            cmdPrint.Enabled = false; 
            //blnFlag = true;
            cbSIBS_GW.SelectedIndex = 0;
            cbTTSP_GW.SelectedIndex = 0;
            cbIQS_GW.SelectedIndex = 0;            
            pickerDate.MaxDate = DateTime.Now;            
                        
            ////cbdepartment.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbdepartment, "CONTENT", "Department", "SYSTEM");
            ////cbdepartment.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbdepartment, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='SYSTEM' and cdname='Department'", "CONTENT", true, true, "ALL"))
                return;                        
            
            ////cbdirection.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbdirection, "CONTENT", "DIRECTION", "TTSP");
            ////cbdirection.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbdirection, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='TTSP' and cdname='DIRECTION'", "CONTENT", true, true, "ALL"))
                return;
                        
            ////cbtype.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbtype, "CONTENT", "TYPE_REC", "TTSP");
            ////cbtype.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbtype, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='TTSP' and cdname='TYPE_REC'", "CONTENT", true, true, "ALL"))
                return;
            

            #region MsgType
            cbMsgType.Items.Clear();
            cbMsgType.Items.Add("ALL");
            cbMsgType.Items.Add("MT100");
            cbMsgType.Items.Add("MT103");
            cbMsgType.Items.Add("MT195");
            cbMsgType.Items.Add("MT196");
            cbMsgType.Items.Add("MT199");
            cbMsgType.SelectedIndex = 0;
            #endregion

            
            ////cbview.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbview, "CONTENT", "RecView", "TTSP");
            ////cbview.SelectedIndex = 0;
            if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='TTSP' and cdname='RecView'", "CONTENT", true, true, "ALL"))
                return;


            /* Get path txt in SIBS Server*/
            //DataSet dsSysTemp = new DataSet();
            //dsSysTemp = objSysvarControl.GetSYSVAR_NAME("PATH_REC", "SYSTEM");
            //if (dsSysTemp.Tables[0].Rows.Count == 0) MessageBox.Show("Cannot find reconcile file in sysvar(gw)!");
            //else strExportFolder = dsSysTemp.Tables[0].Rows[0]["VALUE"].ToString();
            strExportFolder = GetData.GetRecParameter("PATH_REC", "SYSTEM", "PATH");
            if (strExportFolder == "") { MessageBox.Show("Cannot find PATH_REC SIBS-TTSP"); return; }
            // Load dữ liệu trắng
            datMessage.DataSource = GetData.GetSelect(" '' RM_NUMBER,'' REF_NUMBER,'' MODULE,'' SENDER,'' RECEIVER,'' AMOUNT,'' CCYCD,'' TRANS_DATE,'' VALUE_DATE,'' STATUS,'' REC_TYPE from dual where rownum=0").Tables[0];

            pickerDate.Focus();
        }             

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*********************************
         * Su kien kich nut Reconcile
         *********************************/
        private void cmdReconcile_Click(object sender, EventArgs e)
        {
            //int iResult = 0;
            //int iResult1 = 0;
            
            /* Check Input Data*/
            //if (Check() < 0)
            //    return;

            // Lay thong tin truong Direction
            #region Direction
            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            } else strDirection = cbdirection.Text.Trim();
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
            if (cbtype.Text.Trim() == vTypesS[0] || cbtype.Text.Trim() == vTypesS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text.Trim() == vTypesT[0] || cbtype.Text.Trim() == vTypesT[1]) { strType = "TTSP-BR"; }
            if (cbtype.Text.Trim() == vTypesR[0] || cbtype.Text.Trim() == vTypesR[1]) { strType = "TR-BR"; }
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
            // Lay thong tin MsgType
            #region MsgType
            strMsgType = cbMsgType.Text;
            #endregion
                        
            int iResult = clsTTSP_MSG_REC.RECONCILE_SIBS(pickerDate.Value,Common.Userid);
            if (iResult == -1) { MessageBox.Show("Cannot insert_rec_temp", Common.sCaption); }
            int iResult1 = clsTTSP_MSG_REC.RECONCILE_TTSP(pickerDate.Value, Common.Userid);
            if (iResult1 == -1) { MessageBox.Show("Cannot insert_content_temp", Common.sCaption); }
            int iResult2 = clsTTSP_MSG_REC.RECONCILE_IQS(pickerDate.Value, Common.Userid);
            if (iResult2 == -1) { MessageBox.Show("Cannot insert_content_temp", Common.sCaption); }
            if ((iResult >= 0) && (iResult1 >= 0) && (iResult2 >= 0)) { MessageBox.Show("Reconcile Successfully ! ", Common.sCaption); }

            
        }
        /***********************************
         * LoadData : Load du lieu len luoi
         ***********************************/ 
        private void LoadData()
        {
            // Lay thong tin truong Direction
            #region Direction
            string strDirection = "ALL";
            if (cbdirection.Text == "SIBS-TTSP") { strDirection = "O"; };
            if (cbdirection.Text == "TTSP-SIBS") { strDirection = "I"; };
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
            if (cbtype.Text.Trim() == vTypesS[0] || cbtype.Text.Trim() == vTypesS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text.Trim() == vTypesT[0] || cbtype.Text.Trim() == vTypesT[1]) { strType = "TTSP-BR"; }
            if (cbtype.Text.Trim() == vTypesR[0] || cbtype.Text.Trim() == vTypesR[1]) { strType = "TR-BR"; }
            #endregion
            // Lay thong tin truong View
            #region View
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable dt1;
                dt1 = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = dt1.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            #endregion
            // Lay thong tin MsgType
            #region MsgType
            strMsgType = cbMsgType.Text;
            #endregion            
            
            DataTable dt;
            dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, strType, strDirection, strExpre,strMsgType);
            dtReconcile = dt;//DatHM add de in Reconcile            
            datMessage.DataSource = dt;

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
            catch
            { 
            }
        }

        /*********************************
         * Reconcile SIBS - GW(TTSP) RM
         *********************************/
        private int ReconcileSIBS(DateTime dtmDate, string strDepart, string strDirec)
        {
           // string strSql = "";
            intResult = BR.BRLib.Common.GW_RECONCILE_SUCCESS;
            iSIBS = 0;

                            
                // Đẩy điện cần đối chiếu vào bảng TTSP_Content_temp 
                // intResult = objTTSPControl.InsertTTSP_MSG_CONTENT_Temp(dtmDate, "SIBS-BR");
                if (intResult < 0){MessageBox.Show("Cannot insert TTSP_MSG_CONTENT_TEMP");return intResult;}
                else
                {                    
                    // Đối chiếu
                    intResult = objTTSPRecControl.TTSP_Reconcile(dtmDate);
                    if (intResult < 0){MessageBox.Show("Cannot reconcile!");return intResult;}
                    else                    
                    {intResult = objTTSPRecControl.InsertTTSP_MSG_REC_ALL(dtmDate);
                        if (intResult < 0){MessageBox.Show("Cannot Insert TTSP_MSG_REC_ALL!");return intResult;}
                        else
                        {
                            intResult = objTTSPRecControl.InsertTTSP_MSG_REC_TOTAL(dtmDate);
                            if (intResult < 0){MessageBox.Show("Cannot Insert TTSP_MSG_REC_TOTAL!");return intResult;}
                            // Thống kế kết quả đối chiếu
                                                     
                            return intResult;
                        }
                    }
                }
           
            // return intResult;

        }
              
         
        /* Kiem tra combo */
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

        
        /************************************
         * Auto filter data when change combo
         ************************************/
        private void cbview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /*Xu ly su kien nhan nut*/
        /**************************************
         * Su kien khi kich hoat nut Reconcile
         **************************************/
        #region Search
        private void OnSearch(object sender, EventArgs e)
        {
            // Lay thong tin truong Direction
            #region Direction
            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            }
            else strDirection = cbdirection.Text.Trim();
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
            if (cbtype.Text.Trim() == vTypesS[0] || cbtype.Text.Trim() == vTypesS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text.Trim() == vTypesT[0] || cbtype.Text.Trim() == vTypesT[1]) { strType = "TTSP-BR"; }
            if (cbtype.Text.Trim() == vTypesR[0] || cbtype.Text.Trim() == vTypesR[1]) { strType = "TR-BR"; }
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

            LoadData();
        }
        #endregion
               

        /************************************
         * Su kien Change MsgType
         ************************************/
        private void OnMsgType(object sender, EventArgs e)
        {
            strMsgType = cbMsgType.Text;
        }

        private void OnSIBS(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbSIBS_GW.Text == "SIBS->BR")
            {
                dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, "SIBS-BR", "O", "ALL","ALL");
            }
            if (cbSIBS_GW.Text == "SIBS<-BR")
            {
                dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, "SIBS-BR", "I", "ALL","ALL");
            }
            datMessage.DataSource = dt;
        }

        private void OnTTSP(object sender, EventArgs e)
        {
            MessageBox.Show(vResultTTSP, Common.sCaption);
        }

        private void OnDate(object sender, EventArgs e)
        {
            strDate = pickerDate.Value;
        }

        private void OnDirection(object sender, EventArgs e)
        {

        }

        private void OnView(object sender, EventArgs e)
        {

        }

        private void OnDepartment(object sender, EventArgs e)
        {
            strDepartment = cbdepartment.Text;
        }

        private void OnType(object sender, EventArgs e)
        {

        }

        private void cbtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OnTimer(object sender, EventArgs e)
        {
            try
            {
                lbInformation.Text = " Total messages : " + datMessage.Rows.Count.ToString();
            }
            catch 
            {
                lbInformation.Text = "";
            };
        }
        /********************************************
         * Ham kiem tra lai du lieu doi chieu
         ********************************************/ 
        private void OnCheckData(object sender, EventArgs e)
        {
            try
            {
                txtSIBS_GW.Text = clsTTSP_MSG_REC.GetTTSP_Index(pickerDate.Value, cbSIBS_GW.Text);

                txtTTSP_GW.Text = clsTTSP_MSG_REC.GetTTSP_Index(pickerDate.Value, cbTTSP_GW.Text);

                txtIQS_GW.Text = clsTTSP_MSG_REC.GetTTSP_Index(pickerDate.Value, cbIQS_GW.Text);
            }
            catch
            {

            }

        }
        /********************************************
         * Get du lieu ve de doi chieu
         ********************************************/
        private void OnGetData(object sender, EventArgs e)
        {
            int iResult = clsTTSP_MSG_REC.Insert_TTSP_MSG_REC_TEMP(pickerDate.Value);
            if (iResult < 0) { MessageBox.Show("Cannot insert_rec_temp", Common.sCaption); }
            int iResult1 = clsTTSP_MSG_REC.Insert_TTSP_CONTENT_TEMP(pickerDate.Value);
            if (iResult1 < 0) { MessageBox.Show("Cannot insert_content_temp", Common.sCaption); }
            if (iResult > 0 && iResult1 > 0) { MessageBox.Show("Get data successfully !! ", Common.sCaption); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btInfoTTSP_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbTTSP_GW.Text == "BR->TTSP")
            {
                dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, "TTSP-BR", "O", "ALL","ALL");
            }
            if (cbTTSP_GW.Text == "BR<-TTSP")
            {
                dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, "TTSP-BR", "I", "ALL","ALL");
            }
            datMessage.DataSource = dt;            
        }

        private void OnTR(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbIQS_GW.Text == "IQS->BR")
            {
                dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, "IQS-BR", "O", "ALL","ALL");
            }
            if (cbIQS_GW.Text == "IQS<-BR")
            {
                dt = clsTTSP_MSG_REC.GetTTSP_MSG_REC(pickerDate.Value, "IQS-BR", "I", "ALL","ALL");
            }
            datMessage.DataSource = dt;
        }
        // GW
        private void OnGW(object sender, EventArgs e)
        {
            DataTable dt = clsTTSP_MSG_REC.GetTTSP_MSG_CONTENT(pickerDate.Value);
            datMessage.DataSource = dt;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrint frmPrint = new frmPrint();
                string Print = "TTSP_PRINT_RECONCILE";
                frmPrint.PrintType = Print;
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
