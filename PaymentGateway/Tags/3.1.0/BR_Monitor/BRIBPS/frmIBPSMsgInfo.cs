using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRSYSTEM;
using BR.BRLib;
namespace BR.BRIBPS
{
    public partial class frmIBPSMsgInfo : frmBasedata
    {
        #region dinh nghia cac ham trong lop BusinessObject
        IBPS_MSG_CONTENTInfo objTable = new IBPS_MSG_CONTENTInfo();
        IBPS_MSG_LOGController objcontrolIbps_log = new IBPS_MSG_LOGController();
        IBPS_MSG_CONTENTController objControlCONTENT = new IBPS_MSG_CONTENTController();
        IBPS_BANK_MAPController objControlBank_map = new IBPS_BANK_MAPController();
        IBPS_TRANS_MAPController objCtrlTrans = new IBPS_TRANS_MAPController();
        TADController objcontrolTad = new TADController();
        USERSController objBOUser = new USERSController();

        private DataTable tblBankname = new DataTable();
        #endregion 

        #region dinh nghia cac bien
        string HO;
        private bool NeedConfirm = true;
        private static bool strSucess = false;
        public bool bIsCloseForm = false;
        private bool iClose = false;
        //-------------------------------------------------------------
        public DataTable dtFWSTS;
        public DataTable dtSTATUS;//table trang thai status cua form 
        public DataTable dtMap;
        public DataTable dtPRINTSTS;
        public string strMSG_ID;        
        public string strQUERY_ID;
        public string strSender;
        public string strReceiving;
        public string strAmount;
        public string strteller;
        public string strRmno;
        public string strTransdate;
        public string strDepartment;
        public string strStatus;
        public string strSend_receipt;
        public string strCONTENT;
        public string strRM_NUMBER;
        public string strCurrency;
        public string strTRANS_CODE;
        public string strGW_TRANS_NUM;
        public string strSenderBank;
        public string strReceiverBank;
        public string strPreTranCode;
        public string strPreTransNum;
        public string strForwardStatus;
        public string strForwardTime;
        public string strMsgDirection;
        public string strSendingTime;
        public string strReceivingTime;
        private string strSendingBank;
        public string strPrin_STS;
        public string strSOURCE_BRANCH;
        public string strBranch;
        public string strUserid;
        public string strMSG_SRC;
        

        private DataTable dtBranch = new DataTable();
        #endregion 

        public frmIBPSMsgInfo()
        {
            InitializeComponent();
        }
        private DataTable GET_CONTENT(long lMsg_ID)
        {
            try
            {
                DataTable dsCONTENT = new DataTable();
                dsCONTENT = objControlCONTENT.GetIBPS_MSG_DTL(lMsg_ID);
                return dsCONTENT;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return null;
            }
        }
        //khi load  form
        private void frmIBPSMsgInfo_Load(object sender, EventArgs e)
        {
            cmdAdd.Visible = false;cmdDelete.Visible = false;
            cmdEdit.Visible = false;cmdSave.Visible = false;
            txtstatus.Visible = false;
            long lMsg_ID;
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                tblBankname= GetBankname();
                DataTable dtCONTENT = new DataTable();
                lMsg_ID = Convert.ToInt64(strMSG_ID);
                dtCONTENT = GET_CONTENT(lMsg_ID);

                strCONTENT = dtCONTENT.Rows[0]["Content"].ToString();

                if (dtCONTENT == null || dtCONTENT.Rows.Count ==0 )
                    return;
                
                dateTimePicker1.Enabled = false;
                // dua du lieu vao cac otext--------------------
                dateTimePicker1.Text = strTransdate;
                txtRelationNumber.Text = strGW_TRANS_NUM.Replace("\r", "").Replace("\r\n", "");//so but toan                
                txtreceiving_bank.Text = strReceiving.Replace("\r", "").Replace("\r\n", "");//ma ngan hang nhan
                txtrm_no.Text = strRmno.Replace("\r", "").Replace("\r\n", "");//truong Rmno
                txtcontent.Text = strCONTENT;//truong content
                txtdepartment.Text = strDepartment.Replace("\r", "").Replace("\r\n", "");
                txtstatus.Text = strStatus.Replace("\r", "").Replace("\r\n", "");//trang thai
                cbStatus.DataSource = dtSTATUS;
                cbStatus.ValueMember = "STATUS";
                cbStatus.DisplayMember = "NAME";
                cbStatus.SelectedValue = strStatus; 
                cbStatus.Enabled = false;
                cbFwsts.DataSource = dtFWSTS;
                cbFwsts.ValueMember = "CDVAL";
                cbFwsts.DisplayMember = "CONTENT";
                cbFwsts.SelectedValue = strForwardStatus;
                cbFwsts.Enabled = false;

                cboPRINTSTS.DataSource = dtPRINTSTS;
                cboPRINTSTS.ValueMember = "CDVAL";
                cboPRINTSTS.DisplayMember = "CONTENT";
                cboPRINTSTS.SelectedValue = strPrin_STS;
                cboPRINTSTS.Enabled = false;

                txtteller_ID.Text = strteller;
                txtPreTranCode.Text = strPreTranCode.Trim();
                txtPreTranNum.Text = strPreTransNum.Trim();
                txtForwardStatus.Text = strForwardStatus.Trim();
                txtForwardTime.Text = strForwardTime.Trim();

                //-----lay du lieu HV/LV----------------------------
                txtTransactioncode.Text = dtCONTENT.Rows[0]["F003"].ToString();//ma tran code
                DataTable datHVLV = new DataTable();//lay ra so HV/LV
                if (strTRANS_CODE.Trim() != "")
                {
                    datHVLV = objCtrlTrans.Getdata(strTRANS_CODE.Trim());
                    if (datHVLV != null || datHVLV.Rows.Count > 0)
                    {
                        lbTran_code.Text = datHVLV.Rows[0]["DESCRIPTION"].ToString();
                    }
                }
                //--------format kieu tien te---------------- Quynd Update               
                if (strAmount.Trim() == "")
                {
                    txtamount.Text = "";
                }
                else
                {
                    txtamount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                }
               
                
                
                //------ngan hang nhan------Map o bang Bankmap------------------
                lbReceiving_name.Text = GetIBPSBankname(strReceiving.Trim());

                //Lay thong tin mã và tên của trường Sending bank  F021
                strSendingBank = dtCONTENT.Rows[0]["F021"].ToString();
                txtsending_bank.Text = strSendingBank;
                lbSend_Bankname.Text = objControlBank_map.GET_BANK_NAME_TAD(strSendingBank.Trim());// GetIBPSBankname(strSendingBank.Trim());
                //----------------------------------------------------------------
                //Lay thong tin ten ngan hang cua nguoi phat lenh co tai khoan "F007"

                strSenderBank = dtCONTENT.Rows[0]["F007"].ToString();
                if (strMsgDirection.Trim().ToUpper() == "SIBS-IBPS")
                {
                    if (strMSG_SRC == "3" || strMSG_SRC == "2")
                    {
                        txtSender.Text = GetIBPSBankname(strSenderBank.Trim());
                    }
                    else
                    {
                        DataTable _dt = new DataTable();
                        _dt = objControlBank_map.Get_NAME(strSenderBank.Trim(), strSOURCE_BRANCH).Tables[0];
                        if (_dt.Rows.Count != 0)
                        {
                            txtSender.Text = _dt.Rows[0]["BANK_NAME"].ToString();
                        }
                    }
                }
                else
                {
                    txtSender.Text = GetIBPSBankname(strSenderBank.Trim());
                }
                //----------------------------------------------------------------
                //Lay thong tin ten ngan hang cua nguoi gui lenh co tai khoan '019'
                // string strQuiryid = strQUERY_ID.Trim().Replace("\r", "").Replace("\r\n", "");
                strReceiverBank = dtCONTENT.Rows[0]["F019"].ToString();
                txtReceiver.Text = GetIBPSBankname(strReceiverBank.Trim());
                //----------------------------nguoi gui-------------------------------------------------               
               // string strFIELD_NAME1 = "028";
                txtname_send.Text = dtCONTENT.Rows[0]["F028"].ToString();
                //-------------------------address-------------------------------------------
                //string strFIELD_NAME2 = "029";
                txtaddress_sen.Text = dtCONTENT.Rows[0]["F029"].ToString();
                //-----------------------Acount------------------------------------------
                //string strFIELD_NAME3 = "030";
                txtaccount_sen.Text = dtCONTENT.Rows[0]["F030"].ToString();//.TrimStart('0')
                //------------------------nguoi nhan---------------------------------------
                //string strFIELD_NAME4 = "031";
                txtname_rece.Text = dtCONTENT.Rows[0]["F031"].ToString();
                //-------------------------address-------------------------------------------
                //string strFIELD_NAME5 = "032";
                txtaddress_rece.Text = dtCONTENT.Rows[0]["F032"].ToString();
                //-------------------------Acount--------------------------------------------
                //string strFIELD_NAME6 = "033";
                txtaccount_rece.Text= dtCONTENT.Rows[0]["F033"].ToString();
                //-----------------------------------------------------------------------------                
                //dua vao truong Sender lay ra truong Teller
                //--------------------------------------------------------------------------------------
                DataSet datIbank_map = new DataSet();
                datIbank_map = objControlBank_map.GetIBPS_BANK_MAP_TELLERID(strSender);
                txtSendingTime.Text = strSendingTime;
                txtReceivingTime.Text = strReceivingTime;
                //----------------------------------------------------
                //txtsen_receipt_time.Text = frmIBPSMsgList.strSend_receipt;
                string strQUERY_ID1 = strQUERY_ID.Replace("\r", "").Replace("\r\n", "");
                //----------lay ra ten ngan hang gui ngan hang nhan-------------              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            lbCurrecy_name.Text = strCurrency.Replace("\r", "").Replace("\r\n", "");//hien thi loai tien te 
            //----------------------------------------------------------------
            GetHistory(strQUERY_ID);//goi ham lay ra lich su dien
        }

        // Lay gia tri ten Branch, GWBankcode, SIBSBankcode
        private DataTable GetBankname()
        {            
            try
            {
                dtBranch = objControlBank_map.GetIBPS_BANK_MAP().Tables[0];
                return dtBranch;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return null;
            }
        }
        // Lay gia tri ten Branch, GWBankcode, SIBSBankcode
        private string GetIBPSBankname(string strBankcode)
        {
            DataRow[] datRow;
            DataTable _dt = new DataTable();
            string strReturn="";
            try
            {                
                if (strBankcode.Trim() != "")
                {
                    datRow = tblBankname.Select("GW_BANK_CODE=" + strBankcode.Trim());                    
                    if (datRow.Length > 0)
                    {
                        strReturn = datRow[0]["bank_name"].ToString();
                    }                    
                }
                return strReturn;
            }
            catch (Exception ex)
            {
                _dt = objControlBank_map.GetIBPS_BANK_MAP_BankName(strBankcode.Trim()).Tables[0];
                strReturn = _dt.Rows[0]["bank_name"].ToString();
                return strReturn;
            }
        }

      // lay ra thong tin lich su dien
        private void GetHistory(string QUERY_ID)//lay ra thong tin lich su dien
        {
            try
            {
                string strIbps_msg_log = "";
                //lay du lieu tu bang IBPS_MSG_LOG
                DataSet datHistory = new DataSet();
                datHistory = objcontrolIbps_log.GetIBPS_MSG_LOG(QUERY_ID);
                int g = 0;
                while (g < datHistory.Tables[0].Rows.Count)
                {
                    string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                    string strQUERY_ID = datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString();
                    string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                    string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();
                    strIbps_msg_log = strIbps_msg_log + "\r\n" + " + : " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;
                    //lay du lieu tu bang MSG_LOG
                    txthistory.Text = strIbps_msg_log;
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            iClose = true;
            this.Close();
            iClose = false;
        }
        //khi chon cac tab controlkhac nhau
        private void tabInfomation_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (tabInfomation.SelectedIndex == 0)
                {
                    //cmdiqs.Show(); 
                    cmdPrint.Show();
                    this.cmdPrint.Location = new Point(cmdClose.Location.X - cmdiqs.Size.Width - 5, cmdClose.Location.Y);
                }
                else
                {
                    cmdiqs.Hide(); cmdPrint.Hide();
                    this.cmdPrint.Location = new Point(cmdClose.Location.X - cmdClose.Size.Width - 2, cmdClose.Location.Y);
                    cmdClose.Show();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrint frmPrinter = new frmPrint();
                //string Print = "BM_IBPS01";

                string Print = "";
                DataSet dtData = new DataSet();
                
                if (strMsgDirection == "SIBS-IBPS")
                    Print = "IBPS_PRINT_1";
                else
                    Print = "IBPS_PRINT_2";                
                frmPrinter.PrintType = Print;
                frmPrinter.strMsgID = strMSG_ID;
                dtData = objBOUser.Userid_UD(Common.Userid);
                strUserid = dtData.Tables[0].Rows[0]["USERNAME"].ToString();
                strBranch =dtData.Tables[0].Rows[0]["BRANCH"].ToString().Substring(0,3).PadLeft(5,'0');
                frmPrinter.pBranch = strBranch;
                frmPrinter.pUserid = strUserid;

                GetData_Print(strBranch);
                if (dtMap == null || dtMap.Rows.Count == 0)
                {
                    Common.ShowError("Data not found!", 3, MessageBoxButtons.OK);
                }
                else
                {
                    frmPrinter.HMdat = dtMap;
                    frmPrinter.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //su kien thoat khoi form
        //hien thi form tao IQS
        private void cmdiqs_Click_1(object sender, EventArgs e)
        {
            try
            {
                string strMsg_Type = "";
                frmSelectTypeIQS frmSelectIQS = new frmSelectTypeIQS();
                //this.Hide();
                frmSelectIQS.ShowDialog();
                if (frmSelectTypeIQS.isOK == true)
                {

                    strMsg_Type = frmSelectIQS.strMSG_TYPE;
                    frmIQSNew1 IQSNew1 = new frmIQSNew1();
                    IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(strMSG_ID);
                    IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                    DataTable ds = new DataTable();
                    ds = objcontrolTad.GetTAD_HOST(Common.Userid);
                    if (ds.Rows.Count == 0)
                    {
                        return;
                    }
                    else
                    {
                        HO = ds.Rows[0][0].ToString();
                    }
                    IQSNew1.objInfoIQSNew1.FROMBANK = HO.Trim();
                    if (txtrm_no.Text.Trim().Length > 3)
                    {
                        IQSNew1.objInfoIQSNew1.TOBANK = txtrm_no.Text.Trim().Substring(0, 3); //txtsending_bank.Text;
                    }
                    IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = txtrm_no.Text.Trim();
                    IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                    IQSNew1.strAmount = txtamount.Text.Trim();
                    IQSNew1.strCurrency = lbCurrecy_name.Text.Trim();
                    IQSNew1.objInfoIQSNew1.MSGCONTENT = txtcontent.Text.Trim();
                    IQSNew1.strMSG_TYPE = strMsg_Type;
                    if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                    {
                        MessageBox.Show("Could not create IQS message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(dateTimePicker1.Text.Trim());
                    IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(dateTimePicker1.Value);
                    //IQSNew1.objInfoIQSNew1.TELLER_ID = txtteller_ID.Text.Trim();
                    IQSNew1.ShowDialog();
                }
                else if (frmSelectTypeIQS.isOK == false)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCDInfo_FormClosing(null,null);
                this.Close();
            }
        }

        private void frmIBPSMsgInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (iClose == false)
                {
                    if (strSucess == false)
                    {
                        if (NeedConfirm == true)
                        {
                            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all IBPS Message Information windows ?", Common.sCaption);
                            if (e.Cancel == true)
                            {
                                iClose = false;
                                bIsCloseForm = false;
                            }
                            else
                            {
                                bIsCloseForm = true;
                            }
                        }
                        else
                        {
                            //bIsCloseForm = false;
                        }
                    }
                    else
                    {
                        bIsCloseForm = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIBPSMsgInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.bTimer = 1;
                if (e.KeyCode == Keys.Return)
                {
                    SelectNextControl(this.ActiveControl, true, true, true, true);

                    if ((this.ActiveControl) is Button)
                    {
                        if ((this.ActiveControl as Button).Name == "cmdPrint")
                        {
                            cmdPrint.Focus();
                            cmdPrint_Click(null, null);
                        }
                    }
                    if ((this.ActiveControl) is TextBox)
                    {
                        (this.ActiveControl as TextBox).SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIBPSMsgInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        //purpose: Phuc vu in dien 
        private void GetData_Print(string pBranch)
        {
            try
            {
                DataTable datContent = new DataTable();
                string strDtl_name = "IBPS_MSGDTL";
                string strTable_name = "IBPS_MSG_CONTENT";
                datContent = objControlCONTENT.GetData_print_ibps(strMSG_ID.Trim(), pBranch,strUserid);
                if (datContent == null || datContent.Rows.Count == 0)
                {
                    dtMap = null;
                }
                else
                {
                    /////////////////////////////*/
                    //Cap nhat trang thai in dien                    
                    objTable.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                    objTable.PRINT_STS = 1;
                    objControlCONTENT.Update_Print_STS(objTable);
                    //////////////////////////////
                    dtMap = datContent;
                }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //End DatHM
    }
}
