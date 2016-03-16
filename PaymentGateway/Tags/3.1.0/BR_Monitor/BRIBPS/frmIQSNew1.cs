using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRLib;
namespace BR.BRIBPS
{
    public partial class frmIQSNew1 : Form
    {
        #region khai bao ham
        public IQS_MSG_CONTENTInfo objInfoIQSNew1 = new IQS_MSG_CONTENTInfo();
        IQS_MSG_CONTENTController objControl = new IQS_MSG_CONTENTController();
        IBPS_MSG_CONTENTInfo objInfoCONTENT = new IBPS_MSG_CONTENTInfo();
        IBPS_MSG_CONTENTController objControlCONTENT = new IBPS_MSG_CONTENTController();
        IBPS_BANK_MAPInfo objBank_map = new IBPS_BANK_MAPInfo();
        IBPS_BANK_MAPController objControlBank_map = new IBPS_BANK_MAPController();
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranch = new BRANCHController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        TADInfo objInfoTAD = new TADInfo();
        TADController objControlTAD = new TADController();
        #endregion

        #region bien
        //private string RefNo = "";
        //private string FromBank = "";
        //private string ToBank = "";
        //private string RMRef = "";
        //private DateTime date;
        //private string IBPScontent = "";
        //private string IQScontent = "";        
        public string strMSG_TYPE;
        public string strAmount;
        public string strCurrency;
        public string dtTransDate;
        private string strIQSTransNumber = "";
        public string strMSGType;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        clsCheckInput clsCheck = new clsCheckInput();
        public static string dat = Convert.ToString(DateTime.Now);
        private static String[] M = dat.Split(new String[] { " " }, StringSplitOptions.None);//cat chuoi
        string giay =M[1];
        string g = M[0];
        private int iID = 0;
        #endregion

        public frmIQSNew1()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {         
                if (txtProductCode.Text.Trim().Length < 3 & txtProductCode.Text.Trim().Length != 0)
                {
                    Common.ShowError("Invalid Product code length!", 2, MessageBoxButtons.OK);                    
                    txtProductCode.Focus();
                    return;
                }
                if (frmSelectTypeIQS.isTraSoat == true)
                { txtIQSContent.MaxLength = 500; }
                else if (frmSelectTypeIQS.isTraSoat == false)
                { txtIQSContent.MaxLength = 1000; }
                if (String.IsNullOrEmpty(txtIQSContent.Text.Trim()))
                {
                    MessageBox.Show("You must input data!", Common.sCaption);
                    txtIQSContent.Focus();
                    cmdSave.Enabled = true;
                    return;
                }
                string IQSContent = clsCheck.ConvertVietnamese(txtIQSContent.Text.Trim());
                iID = objInfoIQSNew1.QUERY_ID;
                if (cboFromBank.Text.Trim().Length == 3)
                { objInfoIQSNew1.FROMBANK = "00" + cboFromBank.Text.Trim(); }
                else
                { objInfoIQSNew1.FROMBANK = cboFromBank.Text.Trim(); }
                if (cboToBank.Text.Trim().Length == 3)
                { objInfoIQSNew1.TOBANK = "00" + cboToBank.Text.Trim(); }
                else
                { objInfoIQSNew1.TOBANK = cboToBank.Text.Trim(); }
                
                objInfoIQSNew1.REF_NUMBER = txtIQSTransNumber.Text.Trim(); //txtRMRef.Text.Trim();
                objInfoIQSNew1.ORG_RM_NUMBER = txtRMRef.Text.Trim(); 
                objInfoIQSNew1.ORG_TRANS_DATE = dtpTransactionDate.Value;
                objInfoIQSNew1.TELLER_ID = txtTellerID.Text.Trim();
                objInfoIQSNew1.INTERFACE = "IBPS";                
                objInfoIQSNew1.DATECREATE = dtpDate.Value; 
                
                objInfoIQSNew1.STATUS = 0;
                objInfoIQSNew1.MSGCONTENT = txtIBPSContent.Text.Trim();//IQSContent ;
                objInfoIQSNew1.GWOPTION = IQSContent; 
                objInfoIQSNew1.MSG_DIRECTION = "BR-IQS";
                
                objInfoIQSNew1.MSG_TYPE = strMSG_TYPE;
                objInfoIQSNew1.PRODUCT_CODE = clsCheck.ConvertVietnamese(txtProductCode.Text.Trim().ToUpper());
                if (txtAmount.Text.Trim().Length != 0)
                { objInfoIQSNew1.AMOUNT = Convert.ToDouble(txtAmount.Text.Trim()); }
                objInfoIQSNew1.CCYCD = lblAmount.Text.Trim();                
                objControl.AddIQS_MSG_CONTENT_TTSP_VCB(objInfoIQSNew1, strIQSTransNumber, strMSGType);                
                Common.ShowError("Create IQS content successfully!", 1, MessageBoxButtons.OK);               
                
                LockTextbox(true);
                txtIQSContent.ReadOnly = true;
                cmdSave.Enabled = false;
                #region lay thong tin de ghilog----------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "IBPS_IQS message";
                int Log_level = 1;
                string strWorked = "Create IQS mes";
                string strTable = "IQS_MSG_CONTENT";
                string strOld_value = "";
                string strNew_value = objInfoIQSNew1.QUERY_ID.ToString();
                //-----------------------------------------
                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                #endregion

                this.Close();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void GET_CONTENT()
        {
            try
            {
                DataTable dsCONTENT = new DataTable();
                string strTable_name = "IBPS_MSG_CONTENT";
                dsCONTENT = objControlCONTENT.GetIBPS_MSG_CONTENT_CONTENT(objInfoIQSNew1.MSG_ID.ToString(), strTable_name);
                if (dsCONTENT.Rows.Count == 0)
                { txtIBPSContent.Text = ""; }
                else
                { txtIBPSContent.Text = dsCONTENT.Rows[0]["CONTENT"].ToString(); }
                if (dsCONTENT.Rows.Count == 0)
                {
                    DataTable dsCONTENT1 = new DataTable();
                    string strTable_name1 = "IBPS_MSG_ALL";
                    dsCONTENT1 = objControlCONTENT.GetIBPS_MSG_CONTENT_CONTENT(objInfoIQSNew1.MSG_ID.ToString(), strTable_name1);
                    if (dsCONTENT1.Rows.Count == 0)
                    { txtIBPSContent.Text = ""; }
                    else
                    { txtIBPSContent.Text = dsCONTENT1.Rows[0]["CONTENT"].ToString(); }
                    if (dsCONTENT.Rows.Count == 0 && dsCONTENT1.Rows.Count == 0)
                    {
                        DataTable dsCONTENT2 = new DataTable();
                        string strTable_name2 = "IBPS_MSG_ALL_HIS";
                        dsCONTENT2 = objControlCONTENT.GetIBPS_MSG_CONTENT_CONTENT(objInfoIQSNew1.MSG_ID.ToString(), strTable_name2);
                        if (dsCONTENT2.Rows.Count == 0)
                        { txtIBPSContent.Text = ""; }
                        else
                        { txtIBPSContent.Text = dsCONTENT2.Rows[0]["CONTENT"].ToString(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIQSNew1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                GET_CONTENT();
                LockTextbox(true);
                iID = objInfoIQSNew1.QUERY_ID;
                cboFromBank.Text = objInfoIQSNew1.FROMBANK.Replace("\r", "").Replace("\r\n", "");//ma ngan hang gui
                cboToBank.Text = objInfoIQSNew1.TOBANK.Replace("\r", "").Replace("\r\n", "");//ma ngan hang nhan
                txtRMRef.Text = objInfoIQSNew1.ORG_RM_NUMBER;
                objInfoIQSNew1.DATECREATE = DateTime.Now;
                dtpDate.Value = Convert.ToDateTime(objInfoIQSNew1.DATECREATE.ToString());
                if (strAmount.Trim() == "")
                { txtAmount.Text = ""; }
                else
                {
                    txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                }
                lblAmount.Text = strCurrency;
                objInfoIQSNew1.MSG_TYPE = strMSG_TYPE;
                txtTellerID.Text = objInfoIQSNew1.TELLER_ID;
                string strTransdate = objInfoIQSNew1.ORG_TRANS_DATE.ToString();
                String strStrans_date = strTransdate.Replace("\r", "").Replace("\r\n", "");
                String[] S = strStrans_date.Split(new String[] { " " }, StringSplitOptions.None);//cat chuoi              
                dtpTransactionDate.Value = Convert.ToDateTime(strTransdate);                
                txtIQSTransNumber.Text = strIQSTransNumber;                

                #region Load ten Sending bank
                try
                {
                    string strSender = objInfoIQSNew1.FROMBANK;
                    cboFromBank.Text = strSender.Trim().Replace("\r", "").Replace("\r\n", "");
                    //string strSender1 = strSender;
                    string strLenght = strSender.Length.ToString();
                    if (strLenght == "5")
                    {
                        if (strSender.Trim().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                        {
                            DataSet datSend = new DataSet();
                            datSend = objctrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                            if (datSend.Tables[0].Rows.Count == 0)
                            {
                                lblSendingBranch.Text = "";
                            }
                            else
                            {
                                lblSendingBranch.Text = datSend.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                            }
                        }
                    }
                    if (strLenght == "3")
                    {
                        DataSet datSend = new DataSet();
                        datSend = objctrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datSend.Tables[0].Rows.Count == 0)
                        {
                            lblSendingBranch.Text = "";
                        }
                        else
                        {
                            lblSendingBranch.Text = datSend.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                #endregion
                //-------------------------------//-------------------------------------
                #region Load ten Sending bank
                try
                {
                    string strReceiving = objInfoIQSNew1.TOBANK;

                    cboToBank.Text = strReceiving.Trim().Replace("\r", "").Replace("\r\n", "");
                    string strResen = strReceiving.Trim().Replace("\r", "").Replace("\r\n", "");
                    string sreLeng = strResen.Length.ToString();
                    if (sreLeng == "5")
                    {
                        if (strResen.Trim().Substring(0, 2) == "00")
                        {
                            DataSet datRece_bank = new DataSet();
                            datRece_bank = objctrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));

                            if (datRece_bank.Tables[0].Rows.Count == 0)
                            {
                                lblReceivingBranch.Text = "";
                            }
                            else
                            {
                                lblReceivingBranch.Text = datRece_bank.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                            }
                        }
                    }
                    else if (sreLeng == "3")
                    {
                        DataSet datRece_bank = new DataSet();
                        datRece_bank = objctrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", ""));

                        if (datRece_bank.Tables[0].Rows.Count == 0)
                        {
                            lblReceivingBranch.Text = "";
                        }
                        else
                        {
                            lblReceivingBranch.Text = datRece_bank.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
                #endregion               
                txtIQSContent.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void ClearTextBox()
        {
            txtIBPSContent.Text = "";
            txtIQSContent.Text = "";
            txtRMRef.Text = "";
            cboFromBank.Text = "";            
            cboToBank.Text = "";
            dtpDate.Text = "";
        }
        private void LockTextbox(Boolean a)
        {
            txtRMRef.ReadOnly = a;
            txtIBPSContent.ReadOnly = a;
            cboToBank.Enabled = !a;            
            cboFromBank.Enabled = !a;            
            txtAmount.ReadOnly = a;
            txtTellerID.ReadOnly = a;
            dtpTransactionDate.Enabled = !a;
            txtIQSTransNumber.ReadOnly = a;
            dtpDate.Enabled = !a;
        }

        /*---------------------------------------------------------------
     * Muc dich         : Ham ghi log
     * Tra ve           : Mot danh sach cac File - List<FileInfo>
     * Ngay cap nhat    : 11/07/2008
     * Nguoi cap nhat   : HaNTT10
     * Ngay  tao  : 31/04/2008
     * Nguoi tao   : Quynd
     *--------------------------------------------------------------*/
        private void Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        {
            objuser_msg_log.LOG_DATE = Logdate;
            objuser_msg_log.USERID = strUsername;
            objuser_msg_log.CONTENT = strContent;
            objuser_msg_log.STATUS = Log_level;
            objuser_msg_log.WORKED = strWorked;
            objuser_msg_log.TABLE_ACCESS = strTale_Access;
            objuser_msg_log.OLD_VALUE = strOld_value;
            objuser_msg_log.NEW_VALUE = strNew_value;

            objcontroluser_msg_log.AddUSER_MSG_LOG(objuser_msg_log);
        }
       

        private void frmIQSNew1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void frmIQSNew1_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;            
        }

        private void frmIQSNew1_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            { return; }
            else
            {
                if (txtProductCode.Text.Trim().Length != 3)
                {
                    Common.ShowError("Invalid product code length!", 2, MessageBoxButtons.OK);
                    txtProductCode.Focus();
                }
            }
            txtProductCode.Text = clsCheck.ConvertVietnamese(txtProductCode.Text.Trim().ToUpper());
        }

        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            { frmIQSNew1_FormClosing(null, null); this.Close(); }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (txtProductCode.Focused)
                { txtIQSContent.Focus(); txtIQSContent.SelectAll(); }
                else if (txtIQSContent.Focused)
                { cmdSave.Focus(); }                
                //strSucess = true;
            }
        }
    }
}
