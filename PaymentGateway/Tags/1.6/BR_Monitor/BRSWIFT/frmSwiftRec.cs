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
using BR.BRSWIFT.Reconcile;
using System.IO;
using BR.BRLib;

namespace BR.BRSWIFT
{
    public partial class frmSwiftRec : BR.BRSYSTEM.frmBasedata
    {
        /****************************
         * Cac doi tuong dieu khien
         ****************************/
        private GetData objGetData = new GetData();
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();

        MSG_CONTENT_TEMPInfo dsMsgTemp = new MSG_CONTENT_TEMPInfo();
        MSG_CONTENT_TEMPController dsMsgTempControl = new MSG_CONTENT_TEMPController();

        IBPS_MSG_REC_TEMPInfo dsMsgRec = new IBPS_MSG_REC_TEMPInfo();
        IBPS_MSG_REC_TEMPController dsMsgRecControl = new IBPS_MSG_REC_TEMPController();

        SWIFT_MSG_REC_TEMPInfo dsMsgSwiftRec = new SWIFT_MSG_REC_TEMPInfo();
        SWIFT_MSG_REC_TEMPController dsMsgSwiftRecControl = new SWIFT_MSG_REC_TEMPController();

        IBPS_MSG_REC_TADInfo dsMsgRecTad = new IBPS_MSG_REC_TADInfo();
        IBPS_MSG_REC_TADController dsMsgRecTadControl = new IBPS_MSG_REC_TADController();

        SWIFT_RECONCILEInfo objSWIFTSRec = new SWIFT_RECONCILEInfo();
        SWIFT_RECONCILEController objSWIFTRecControl = new SWIFT_RECONCILEController();

        SWIFT_MSG_CONTENTInfo objSwift = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objSwiftControl = new SWIFT_MSG_CONTENTController();

        IBPS_MSG_CONTENTInfo objIBPS = new IBPS_MSG_CONTENTInfo();
        IBPS_MSG_CONTENTController objIBPSControl = new IBPS_MSG_CONTENTController();

        SWIFT_MSG_REC_SAAInfo objSwiftSaaInfo = new SWIFT_MSG_REC_SAAInfo();
        SWIFT_MSG_REC_SAAController objSwiftSaaControl = new SWIFT_MSG_REC_SAAController();

        IBPS_BANK_MAPController objIBPSBankControl = new IBPS_BANK_MAPController();

        SYSVARController objSysvarControl = new SYSVARController();
                
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        public string strDirection;
        public string strDepartment;
        public string strExpre;
        public DateTime strDate;
        public DateTime dtmDate;
        public string strType;
        string[] vTypeS = new string[2] { "SIBS-BR" , "BR-SIBS" };
        string[] vTypeW = new string[2] { "SWIFT-BR", "BR-SWIFT" };
        string[] vTypeR = new string[2] { "TR-BR"   , "BR-TR" };
        public int intI;
        public int intResult;
        public int intResult1;        
        public string strExportFolder = ""; //Duong dan den thu muc co chua file text
        public string strFileName = ""; //Ten file
        //int iSIBS = 0; 
        //int iSWIFT = 0;
        //string strPath = "";
        //string strFolderName = "";
        //Boolean blnFlag; //Co xac dinh co can doc lai file ko?
        //string vResultSIBS = "";// Biến lưu trữ kết quả đối chiếu SIBS
        //string vResultSWIFT = "";// Biến lưu trữ kết quả đối chiếu TTSP
        //-------------------DatHM-------------------------------------
        public DataTable dtReconcile;
        public string OnDate;
        public string str_SFOTF="";
        public string str_SFORMSIBS="";
        public string str_SFORMFILE="";
        public string str_SFOTR="";
        public string str_GWROTF="";
        public string str_GWRORMSIBS="";
        public string str_GWRORMFILE="";
        public string str_GWROTR="";
        public string str_GWSOTF="";
        public string str_GWSORM="";
        public string str_GWSOTR="";
        public string str_SWRO="";
        public string str_SWSI="";
        public string str_GWRITF="";
        public string str_GWRIRMSW="";
        public string str_GWRIRMFILE="";
        public string str_GWRITR="";
        public string str_GWRIOther="";
        public string str_GWSITF="";
        public string str_GWSIRM="";
        public string str_GWSITR="";
        public string str_SIBSITF="";
        public string str_SIBSIRM="";
        public string str_SIBSITR="";
        //-----------------------End DatHM-----------------------------
        
        public frmSwiftRec()
        {
            InitializeComponent();
        }
        /* On Load */
        private void frmSwiftRec_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            cmdPrint.Enabled = false; 
            cbSIBSTF_GW.SelectedIndex = 0;
            cbSIBSRM_GW.SelectedIndex = 0;
            cbSWIFT_GW.SelectedIndex = 0;
            cbTR_GW.SelectedIndex = 0;
            
            this.pickerDate.MaxDate = DateTime.Now;
            /* Load to comboBox */
            
            ////cbdepartment.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbdepartment, "CONTENT", "Department", "SYSTEM");
            ////cbdepartment.SelectedIndex = 0;            
            ////cbdirection.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbdirection, "CONTENT", "DIRECTION", "SWIFT");
            ////cbdirection.SelectedIndex = 0;
            ////cbtype.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbtype, "CONTENT", "TYPE_REC", "SWIFT");
            ////cbtype.SelectedIndex = 0;            
            ////cbview.Items.Add("ALL");
            ////GetData.getDataComboAllCode(cbview, "CONTENT", "RecView", "SWIFT");
            if (!objGetData.FillDataComboBox(cbdepartment, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SYSTEM' and cdname='Department'", "CONTENT", true, true, "ALL"))
                return;            
            if (!objGetData.FillDataComboBox(cbdirection, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SWIFT' and cdname='DIRECTION'", "CONTENT", true, true, "ALL"))
                return;
            if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SWIFT' and cdname='RecView'", "CONTENT", true, true, "ALL"))
                return;
            if (!objGetData.FillDataComboBox(cbtype, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SWIFT' and cdname='TYPE_REC'", "CONTENT", true, true, "ALL"))
                return;
            

            datMessage.DataSource = GetData.GetSelect(" '' RM_NUMBER,'' REF_NUMBER,'' MODULE,'' SENDER,'' RECEIVER,'' AMOUNT,'' CCYCD,'' TRANS_DATE,'' VALUE_DATE,'' STATUS,'' REC_TYPE from dual where rownum=0").Tables[0];
        }

        /* On Close */
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*******************************************
         * Ham xu li su kien khi kich nut Reconcile
         * Le Hoang : 25-08-08
         *******************************************/
        private void cmdReconcile_Click(object sender, EventArgs e)
        {
            //int iResult = 0;
            //int iResult1 = 0;

            // Kiem tra thong tin do user dien vao
            if (Check() < 0) { return; }
            // Lay thong tin truong Direction            
            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            }
            else strDirection = cbdirection.Text.Trim();

            // Lay thong tin truong Department           
            strDepartment = cbdepartment.Text.Trim();
            // Lay thong tin truong Date            
            strDate = pickerDate.Value;
            // Lay thong tin truong Type

            strType = "ALL";
            if (cbtype.Text.Trim() == vTypeS[0] || cbtype.Text.Trim() == vTypeS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text.Trim() == vTypeW[0] || cbtype.Text.Trim() == vTypeW[1]) { strType = "SWIFT-BR"; }
            if (cbtype.Text.Trim() == vTypeR[0] || cbtype.Text.Trim() == vTypeR[1]) { strType = "TR-BR"; }
            // Lay thong tin truong View            
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            // Xóa tất cả các kết quả đối chiếu trước
            // objSWIFTRecControl.ClearSWIFT_RECONCILE(dtmDate.Date.ToString(), strDepartment);
            // Đẩy điện có trong GW vào bảng SWIFT_msg_content_temp
            //dtmDate = pickerDate.Value.Date;
            //iResult = objSwiftControl.InsertSWIFT_MSG_CONTENT_Temp(dtmDate, "ALL", " ");

            // Đối chiếu
            //iResult = ReconcileSIBS(strDate, "ALL", "ALL");
            //iResult1 = ReconcileSWIFT(strDate, "ALL", "ALL");
            int iResult = clsSWIFT_MSG_REC.RECONCILE_SIBS(pickerDate.Value,Common.Userid);
            if (iResult < 0) { MessageBox.Show("Cannot Reconcile SIBS ", Common.sCaption); }
            int iResult1 = clsSWIFT_MSG_REC.RECONCILE_TR(pickerDate.Value, Common.Userid);
            if (iResult1 < 0) { MessageBox.Show("Cannot Reconcile TR ", Common.sCaption); }
            int iResult2 = clsSWIFT_MSG_REC.RECONCILE_SWIFT(pickerDate.Value, Common.Userid);
            if (iResult2 < 0) { MessageBox.Show("Cannot Reconcile SWIFT ", Common.sCaption); }

            if ((iResult >= 0) && (iResult1 >= 0) && (iResult2 >= 0)) { MessageBox.Show("Reconcile Successfully ! ", Common.sCaption); }
            
            //LoadData();
        }

        /****************************************
         * Ham thuc hien doi chieu SIBS-BR(SWIFT)
         ****************************************/
        private int ReconcileSIBS(DateTime dtmDate, string strDepart, string strDirec)
        {
            //string strSql = "";
            //int intResult1 = 0; 
            //int intResult2 = 0;
            intResult = BR.BRLib.Common.GW_RECONCILE_SUCCESS;

            //Xóa bảng lưu điện dùng để đối chiếu
            //intResult = objSwiftControl.DeleteSWIFT_MSG_CONTENT_Temp();
            //Đẩy điện có trong GW vào bảng SWIFT_msg_content_temp
            //intResult1 = objSwiftControl.InsertSWIFT_MSG_CONTENT_Temp(dtmDate, "ALL", " ");

            //if (intResult < 0 && intResult1 < 0) return intResult;
            //else
            //{
                /*[Doi chieu]*/
                intResult = objSWIFTRecControl.SWIFT_Reconcile(dtmDate);
                //return intResult;
                if (intResult < 0) 
                { 
                    MessageBox.Show("Cannot reconcile!", Common.sCaption); 
                    return intResult; 
                }
                else
                {
                    // Đẩy vào bảng ALL
                    intResult = objSWIFTRecControl.InsertSWIFT_MSG_REC_ALL(dtmDate);
                    if (intResult < 0) 
                    { 
                        MessageBox.Show("Cannot Insert SWIFT_MSG_REC_ALL!", Common.sCaption); 
                        return intResult; 
                    }
                    else // Đẩy vào bảng TOTAL
                    {
                        intResult = objSWIFTRecControl.InsertSWIFT_MSG_REC_TOTAL(dtmDate);
                        if (intResult < 0) 
                        { 
                            MessageBox.Show("Cannot Insert SWIFT_MSG_REC_TOTAL!", Common.sCaption); 
                            return intResult; 
                        }
                        return intResult;
                    }
                }
            //}          
            //return intResult;
        }

      

        /****************************************
         * Ham Reconcile SWIFT
         ****************************************/
        private int ReconcileSWIFT(DateTime dtmDate, string strDepart, string strDirec)
        {

            //string strSql = "";
            //iSWIFT = 0;
            //int intResult1 = 0;
            //int intResult2 = 0;

            //Xóa bảng SWIFT_content_temp
            intResult = objSwiftControl.DeleteSWIFT_MSG_CONTENT_Temp();
            //Đẩy điện lưu trong GW vào bảng content
            //intResult = objSwiftControl.InsertSWIFT_MSG_CONTENT_Temp(dtmDate, "SWIFT-BR");

            //if (intResult < 0) return intResult;
            //else
            //{
                // Đối chiếu
                intResult = objSWIFTRecControl.SWIFT_ReconcileSWIFT(dtmDate);
                //return intResult;
                if (intResult < 0) { MessageBox.Show("Cannot reconcile!", Common.sCaption); return intResult; }
                else
                {
                    // Đẩy vào bảng ALL
                    intResult = objSWIFTRecControl.InsertSWIFT_MSG_REC_ALL(dtmDate);
                    if (intResult < 0) { MessageBox.Show("Cannot Insert SWIFT_MSG_REC_ALL!", Common.sCaption); return intResult; }
                    else
                    {
                        // Đẩy vào bảng TOTAL
                        intResult = objSWIFTRecControl.InsertSWIFT_MSG_REC_TOTAL(dtmDate);
                        if (intResult < 0) { MessageBox.Show("Cannot Insert SWIFT_MSG_REC_TOTAL!", Common.sCaption); return intResult; }

                        return intResult;
                    }
                }
            //}
            //return intResult;
        }
        /************************************
         * Hien thi noi dung len luoi SIBS-BR         
         ************************************/
        private void LoadData()
        
        {
            /*Kiem tra thong tin do user dien vao*/
            if (Check() < 0) { return; }
            /* Lay thong tin truong Direction[CDVAL]. ex: I-O*/

            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable dt;
                dt = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = dt.Rows[0]["CDVAL"].ToString();
            }
            else
                strDirection = cbdirection.Text.Trim();

            /* Lay thong tin truong Department */
            
            strDepartment = cbdepartment.Text.Trim();

            /* Lay thong tin truong Date */

            strDate = pickerDate.Value;

            /* Lay thong tin truong Type */

            strType = "ALL";
            if (cbtype.Text.Trim() == vTypeS[0] || cbtype.Text.Trim() == vTypeS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text.Trim() == vTypeW[0] || cbtype.Text.Trim() == vTypeW[1]) { strType = "SWIFT-BR"; }
            if (cbtype.Text.Trim() == vTypeR[0] || cbtype.Text.Trim() == vTypeR[1]) { strType = "TR-BR"; }
            /* Lay thong tin truong View[CDVAL]. Ex : G: GW have,S:SWIFT have */

            if (cbview.Text.Trim() != "ALL")
            {
                DataTable dt;
                dt = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = dt.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();


            DataTable dt1;
            DataTable dt2;// Print
            //if (strDirection == "Inward") strDirection = "I"; else strDirection = "O";
            dt1 = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(strDate, strType, strDirection, strDepartment, strExpre);
            dtReconcile = dt1;//DatHM add de in Reconcile
            try
            {
                dt2 = GetData.GetSelect(" * FROM SWIFT_REC_TOTAL WHERE NS=" + pickerDate.Value.ToString("yyyyMMdd") + " order by id asc ").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                     str_SFOTF = dt2.Rows[0]["VALUE"].ToString();
                     str_SFORMSIBS = dt2.Rows[1]["VALUE"].ToString();
                     str_SFORMFILE = dt2.Rows[2]["VALUE"].ToString();
                     str_SFOTR = dt2.Rows[3]["VALUE"].ToString();
                     str_GWROTF = dt2.Rows[4]["VALUE"].ToString();
                     str_GWRORMSIBS = dt2.Rows[5]["VALUE"].ToString();
                     str_GWRORMFILE = dt2.Rows[6]["VALUE"].ToString();
                     str_GWROTR = dt2.Rows[7]["VALUE"].ToString();
                     str_GWSOTF = dt2.Rows[8]["VALUE"].ToString();
                     str_GWSORM = dt2.Rows[9]["VALUE"].ToString();
                     str_GWSOTR = dt2.Rows[10]["VALUE"].ToString();
                     str_SWRO = dt2.Rows[11]["VALUE"].ToString();
                     str_SWSI = dt2.Rows[12]["VALUE"].ToString();
                     str_GWRITF = dt2.Rows[13]["VALUE"].ToString();
                     str_GWRIRMSW = dt2.Rows[14]["VALUE"].ToString();
                     str_GWRIRMFILE = dt2.Rows[15]["VALUE"].ToString();
                     str_GWRITR = dt2.Rows[16]["VALUE"].ToString();
                     str_GWRIOther = dt2.Rows[17]["VALUE"].ToString();
                     str_GWSITF = dt2.Rows[18]["VALUE"].ToString();
                     str_GWSIRM = dt2.Rows[19]["VALUE"].ToString();
                     str_GWSITR = dt2.Rows[20]["VALUE"].ToString();
                     str_SIBSITF = dt2.Rows[21]["VALUE"].ToString();
                     str_SIBSIRM = dt2.Rows[22]["VALUE"].ToString();
                     str_SIBSITR = dt2.Rows[23]["VALUE"].ToString();  

                }
                OnDate = pickerDate.Text.ToString(); 
            }
            catch { dt2 = null; }
            
            if (dt1.Rows.Count >= 0)
            {

                datMessage.DataSource = dt1;
                datMessage.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                datMessage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                
                datMessage.Columns["AMOUNT"].Width = 180;
                datMessage.Columns["AMOUNT"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
            }
            else
            {
                datMessage.DataSource = null;
                MessageBox.Show("There aren't any data to display!", Common.sCaption);
                return;
            }
            //DatHM add
            if (dt1.Rows.Count > 0)
            {
                cmdPrint.Enabled = true;
            }
            else
            {
                cmdPrint.Enabled = false;
            }
            //DatHM add
        }


        /**********************************************
         * Ham kiem tra lai gia tri cua cac o checkbox
         **********************************************/
        private int Check()
        {
            if (string.IsNullOrEmpty(cbdirection.Text.Trim()))
            {
                MessageBox.Show("You must choice Direction", Common.sCaption);
                cbdirection.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbdepartment.Text.Trim()))
            {
                MessageBox.Show("You must choice Department", Common.sCaption);
                cbdepartment.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(strDate.ToString()))
            {
                MessageBox.Show("You must choice Date", Common.sCaption);
                pickerDate.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbtype.Text.Trim()))
            {
                MessageBox.Show("You must choice Type", Common.sCaption);
                cbtype.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbview.Text.Trim()))
            {
                MessageBox.Show("You must choice View", Common.sCaption);
                cbview.Focus();
                return -1;
            }
            return 1;
        }

        /**********************************************
         * Xu ly su kien thay doi ngay
         **********************************************/
        private void pickerDate_ValueChanged(object sender, EventArgs e)
        {
            strDate = pickerDate.Value;
        }

        /***************************************
         * Messages Reconcile System
         * HoangLA         
         * 17-08-08
         ***************************************/


        /***************************************
         * Su Kien Load du lieu len Grid
         ***************************************/
        private void OnSearch(object sender, EventArgs e)
        {
            /* Lay thong tin truong Direction[CDVAL]. ex: I-O*/

            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            }
            else strDirection = cbdirection.Text.Trim();

            /* Lay thong tin truong Department */
            strDepartment = cbdepartment.Text.Trim();
            /* Lay thong tin truong Date */
            strDate = pickerDate.Value;
            /* Lay thong tin truong Type */
            strType = "ALL";
            if (cbtype.Text.Trim() == vTypeS[0] || cbtype.Text.Trim() == vTypeS[1]) { strType = "SIBS-BR";  }
            if (cbtype.Text.Trim() == vTypeW[0] || cbtype.Text.Trim() == vTypeW[1]) { strType = "SWIFT-BR"; }

            /* Lay thong tin truong View[CDVAL]. Ex : G: GW have,S:SWIFT have */

            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            LoadData();
        }
        /****************************************          
         * Thay doi ComboDepartment 
         ****************************************/
        private void OnChangeDP(object sender, EventArgs e)
        {
            strDepartment = cbdepartment.Text;
        }

        /****************************************          
         * Thay doi ComType => ComView
         ****************************************/
        private void OnChangeType(object sender, EventArgs e)
        {
            //objallcodecontrol.GetALLCODE_Direc();
            //cbview.Items.Clear();
            if (cbtype.Text.Trim() == "ALL")
            {
                ////cbview.Items.Add("ALL");
                ////GetData.getDataComboAllCode(cbview, "CONTENT", "RecView", "SWIFT");
                //////cbview.SelectedValue = 0;
                ////cbview.SelectedIndex = 0;
                if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SWIFT' and cdname='RecView'","CONTENT", true, true, "ALL"))
                    return;
            }
            else
            {
                string strType = "";
                if (cbtype.Text.Trim() == vTypeS[0] || cbtype.Text.Trim() == vTypeS[1]) 
                    strType = "SIBS-BR";
                if (cbtype.Text.Trim() == vTypeW[0] || cbtype.Text.Trim() == vTypeW[1]) 
                    strType = "SWIFT-BR";

                ////cbview.Items.Add("ALL");
                ////GetData.getDataComboAllCode_Direc(cbview, "CONTENT", "RecView", "SWIFT", strType);
                ////cbview.SelectedValue = 0;
                ////cbview.SelectedIndex = 0;
                if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SWIFT' and cdname='RecView' and DIRECTION='" + strType + "'",
                    "CONTENT", true, true, "ALL"))
                    return;
            }
            this.strType = cbtype.Text;
        }
        /****************************************          
         * Thay doi ComboView
         ****************************************/
        private void cbview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbview.Text.Trim() == "System.Data.DataRowView")
                return;
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else
                strExpre = cbview.Text.Trim();
            //LoadData();            
        }

        private void OnDirection(object sender, EventArgs e)
        {
            if (cbdirection.Text == "SIBS-SWIFT") { strDirection = "O"; };
            if (cbdirection.Text == "SWIFT-SIBS") { strDirection = "I"; };
            if (cbdirection.Text == "SIBS-SWIFT")
            {
                cbtype.DataSource = null;
                cbtype.Items.Clear();
                cbtype.Items.Add("ALL");
                cbtype.Items.Add("SIBS-BR");
                cbtype.Items.Add("BR-SWIFT");
                cbtype.SelectedIndex = 0;
            }
            if (cbdirection.Text == "SWIFT-SIBS")
            {
                cbtype.DataSource = null;
                cbtype.Items.Clear();
                cbtype.Items.Add("ALL");
                cbtype.Items.Add("SWIFT-BR");
                cbtype.Items.Add("BR-SIBS");
                cbtype.SelectedIndex = 0;
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }

        private void frmSwiftRec_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }


    
        // Kiem tra lai du lieu doi chieu
        private void OnCheckData(object sender, EventArgs e)
        {
            try
            {
                txtSIBS_RM.Text  = clsSWIFT_MSG_REC.GetSWIFT_Index(pickerDate.Value, cbSIBSRM_GW.Text);

                txtSIBS_TF.Text = clsSWIFT_MSG_REC.GetSWIFT_Index(pickerDate.Value, cbSIBSTF_GW.Text);

                txtSWIFT_GW.Text = clsSWIFT_MSG_REC.GetSWIFT_Index(pickerDate.Value, cbSWIFT_GW.Text);

                txtTR_GW.Text    = clsSWIFT_MSG_REC.GetSWIFT_Index(pickerDate.Value, cbTR_GW.Text);
            }
            catch
            {
                
            }
        }
        // Lay du lieu dung de doi chieu
        private void OnGetData(object sender, EventArgs e)
        {
            int iResult = clsSWIFT_MSG_REC.InsertSWIFT_MSG_CONTENT_Temp(pickerDate.Value);
            if (iResult < 0) { MessageBox.Show("Cannot Insert SWIFT_MSG_CONTENT_temp ", Common.sCaption); }
            int iResult1 = clsSWIFT_MSG_REC.InsertSWIFT_MSG_REC_TEMP(pickerDate.Value);
            if (iResult1 < 0) { MessageBox.Show("Cannot Insert SWIFT_MSG_REC_temp ", Common.sCaption); }
            if (iResult > 0 && iResult1 > 0) { MessageBox.Show("Get data successfully !! ", Common.sCaption); }
        }

        private void OnTimers(object sender, EventArgs e)
        {
            try { lbInformation.Text = "Total messsages : " + datMessage.Rows.Count.ToString(); }
            catch { lbInformation.Text = ""; }
        }

        private void OnSWIFTRec(object sender, EventArgs e)
        {
            DataTable dt=null;
            if (cbSWIFT_GW.Text=="BR->SWIFT")
            {
            dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value,"SWIFT-BR", "O", "ALL","ALL");            
            }
            if (cbSWIFT_GW.Text == "BR<-SWIFT")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "SWIFT-BR", "I", "ALL","ALL");
            }
            datMessage.DataSource = dt;
        }

        private void OnTR(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbTR_GW.Text == "TR->BR")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "TR-BR", "O", "ALL","ALL");
            }
            if (cbTR_GW.Text == "TR<-BR")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "TR-BR", "I", "ALL","ALL");
            }
            datMessage.DataSource = dt;
        }

        private void OnSIBS(object sender, EventArgs e)
        {

        }

        private void OnSIBSRM(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbSIBSRM_GW.Text == "SIBS.RM->BR")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "SIBS-BR", "O", "RM","ALL");
            }
            if (cbSIBSRM_GW.Text == "SIBS.RM<-BR")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "SIBS-BR", "I", "RM","ALL");
            }
            datMessage.DataSource = dt;
        }

        private void OnSIBSTF(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbSIBSTF_GW.Text == "SIBS.TF->BR")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "SIBS-BR", "O", "TF","ALL");
            }
            if (cbSIBSTF_GW.Text == "SIBS.TF<-BR")
            {
                dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_REC(pickerDate.Value, "SIBS-BR", "I", "TF","ALL");
            }
            datMessage.DataSource = dt;
        }
               

        private void OnGW(object sender, EventArgs e)
        {
            DataTable dt = clsSWIFT_MSG_REC.GetSWIFT_MSG_CONTENT(pickerDate.Value);
            datMessage.DataSource = dt;
        }

        private void cmdPrint_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmPrint frmPrint = new frmPrint();
                string Print = "SWIFT_PRINT_RECONCILE";
                frmPrint.PrintType = Print;
                frmPrint.HMdat = dtReconcile;
                frmPrint.str_GWRIOther = str_GWRIOther;
                frmPrint.str_GWRIRMFILE = str_GWRIRMFILE;
                frmPrint.str_GWRIRMSW = str_GWRIRMSW;
                frmPrint.str_GWRITF = str_GWRITF;
                frmPrint.str_GWRITR = str_GWRITR;
                frmPrint.str_GWRORMFILE = str_GWRORMFILE;
                frmPrint.str_GWRORMSIBS = str_GWRORMSIBS;
                frmPrint.str_GWROTF = str_GWROTF;
                frmPrint.str_GWROTR = str_GWROTR;
                frmPrint.str_GWSIRM = str_GWSIRM;
                frmPrint.str_GWSITF = str_GWSITF;
                frmPrint.str_GWSITR = str_GWSITR;
                frmPrint.str_GWSORM = str_GWSORM;
                frmPrint.str_GWSOTF = str_GWSOTF;
                frmPrint.str_GWSOTR = str_GWSOTR;
                frmPrint.str_SFORMFILE = str_SFORMFILE;
                frmPrint.str_SFORMSIBS = str_SFORMSIBS;
                frmPrint.str_SFOTF = str_SFOTF;
                frmPrint.str_SFOTR = str_SFOTR;
                frmPrint.str_SIBSIRM = str_SIBSIRM;
                frmPrint.str_SIBSITF = str_SIBSITF;
                frmPrint.str_SIBSITR = str_SIBSITR;
                frmPrint.str_SWRO = str_SWRO;
                frmPrint.str_SWSI = str_SWSI;
                frmPrint.OnDate = OnDate;
                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Error:" + ex, Common.sCaption);
            }
        }
        // Search ALL
        private void OnALL(object sender, EventArgs e)
        {

        }


    }
}
