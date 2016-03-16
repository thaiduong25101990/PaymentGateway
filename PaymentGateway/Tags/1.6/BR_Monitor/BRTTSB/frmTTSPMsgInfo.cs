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
using System.Text.RegularExpressions;
using BR.BRSYSTEM;

namespace BR.BRTTSB
{
    public partial class frmTTSPMsgInfo : BR.BRSYSTEM.frmBasedata
    {
        #region khai bao cac ham cac bien
        TTSP_MSG_LOGInfo objTTSPps_log = new TTSP_MSG_LOGInfo();
        TTSP_MSG_LOGController objcontrolTtsp_log = new TTSP_MSG_LOGController();
        TADController objcontrolTad = new TADController();

        SWIFT_BANK_MAPInfo objSwift_bank = new SWIFT_BANK_MAPInfo();
        SWIFT_BANK_MAPController objControl_bank = new SWIFT_BANK_MAPController();

        TTSP_MSG_CONTENTInfo objInfoCONTENT = new TTSP_MSG_CONTENTInfo();
        TTSP_MSG_CONTENTController objControlCONTENT = new TTSP_MSG_CONTENTController();

        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objCtrlBranch = new BRANCHController();
        //------------------------------------------------------------------------
        public DataTable dtMap;
        public string strMSG_ID;
        public string strQUERY_ID;
        public  string strSender;
        public  string strReceiving;
        public  string strAmount;
        public string strCCYCD;
        public  string strRmno;
        public  string strTransdate;
        public  string strDepartment;
        public  string strCONTENT;
        public  string strRefNo;
        public string strRMNUMBER;
        public string strMsgDirection;
        public string strMessageTYPE;
        //-------------------------------------------------------------------------
        string HO;
        private bool NeedConfirm = true;
        private static bool strSucess = false;
        public bool bIsCloseForm = false;
        private bool iClose = false;
        #endregion

        public frmTTSPMsgInfo()
        {
            InitializeComponent();
        }
        private void GetCONTENT()
        {
            try
            {
                //----------------------------------------------------------------------------------
                DataTable dsCONTENT = new DataTable();
                dsCONTENT = objControlCONTENT.GetTTSP_MSG_CONTENT_CONTENT(strMSG_ID);
                
                if (dsCONTENT.Rows.Count == 0)
                {
                    strCONTENT = "";
                }
                else
                {
                    try
                    {
                        strCONTENT = dsCONTENT.Rows[0]["CONTENT"].ToString();
                    }
                    catch
                    {
                        strCONTENT = "";
                    }
                }            
             }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void frmTTSPMsgInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                dateTimePicker1.Enabled = false;
                try
                {
                    GetCONTENT();
                    txtsending_bank.Text = strSender.Trim().Replace("\r", "").Replace("\r\n", "");
                    string strLenght = strSender.Length.ToString();
                    if (strLenght == "5")
                    {
                        if (strSender.Trim().Substring(0, 2) == "00")
                        {
                            DataSet datSend = new DataSet();
                            datSend = objCtrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                            if (datSend.Tables[0].Rows.Count == 0)
                            {
                                lbSend_bankname.Text = "";
                            }
                            else
                            {
                                lbSend_bankname.Text = datSend.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                            }
                        }
                    }
                    if (strLenght == "3")
                    {
                        DataSet datSend = new DataSet();
                        datSend = objCtrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datSend.Tables[0].Rows.Count == 0)
                        {
                            lbSend_bankname.Text = "";
                        }
                        else
                        {
                            lbSend_bankname.Text = datSend.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
                //-------------------------------//-------------------------------------
                txtreceiving_bank.Text = strReceiving.Trim().Replace("\r", "").Replace("\r\n", "");
                try
                {
                    string strResen = strReceiving.Trim().Replace("\r", "").Replace("\r\n", "");
                    string sreLeng = strResen.Length.ToString();
                    if (sreLeng == "5")
                    {
                        if (strResen.Trim().Substring(0, 2) == "00")
                        {
                            DataSet datRece_bank = new DataSet();
                            datRece_bank = objCtrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", ""));
                            if (datRece_bank.Tables[0].Rows.Count == 0)
                            {
                                lbRece_bankname.Text = "";
                            }
                            else
                            {
                                lbRece_bankname.Text = datRece_bank.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                            }
                        }
                    }
                    else if (sreLeng == "3")
                    {
                        DataSet datRece_bank = new DataSet();
                        datRece_bank = objCtrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datRece_bank.Tables[0].Rows.Count == 0)
                        {
                            lbRece_bankname.Text = "";
                        }
                        else
                        {
                            lbRece_bankname.Text = datRece_bank.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
                try
                {
                    if (strAmount.Trim() == "")
                    {
                        txtamount.Text = "";
                    }
                    else
                    {
                        txtamount.Text = Common.FormatCurrency(strAmount.Trim().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }
                    lbccycd.Text = strCCYCD.Trim().Replace("\r", "").Replace("\r\n", "");
                    txtrm_no.Text = strRmno.Trim().Replace("\r", "").Replace("\r\n", "");
                    try
                    {
                        String strStrans_date = strTransdate.Trim().Replace("\r", "").Replace("\r\n", "");
                        String[] E = strStrans_date.Split(new String[] { " " }, StringSplitOptions.None);//cat chuoi              
                        txttran_date.Text = E[0];
                        dateTimePicker1.Value = Convert.ToDateTime(E[0]);
                        txtdepartment.Text = strDepartment.Trim().Replace("\r", "").Replace("\r\n", "");
                        txtcontent.Text = strCONTENT;
                        txtRmno.Text = strRMNUMBER.ToLowerInvariant().Replace("\r", "").Replace("\r\n", "");//Rmno truong cuoi cung trong bang IBPS_MSG_CONTENT
                    }
                    catch
                    {                                                         
                        txttran_date.Text = DateTime.Now.ToString("DD/MM/YYY HH:mm:ss");
                        //dateTimePicker1.Value = Convert.ToDateTime(E[0]);
                        txtdepartment.Text = "TEST";
                        txtcontent.Text = strCONTENT;
                        txtRmno.Text = strRMNUMBER.ToLowerInvariant().Replace("\r", "").Replace("\r\n", "");//Rmno truong cuoi cung trong bang IBPS_MSG_CONTENT
                    }
                    //---------------cat chuoi lay thong tin cua nguoi nhan---------------------------
                    string strVDcontent = strCONTENT.Trim();

                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            //--------------------------------------------------------------------------------
            string strQUERY_ID1 = strQUERY_ID;
            GetHistory(strQUERY_ID1);//goi ham lay ra lich su dien
        }
        // lay ra thong tin lich su dien
        private void GetHistory(string QUERY_ID)
        {
            try
            {
                string strTtsp_msg_log = "";
                //lay du lieu tu bang IBPS_MSG_LOG
                DataSet datHistory = new DataSet();
                datHistory = objcontrolTtsp_log.GetTTSP_MSG_LOG(QUERY_ID);//kiem tra lai
                int g = 0;
                while (g < datHistory.Tables[0].Rows.Count)
                {
                    string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                    string strQUERY_ID = datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString();
                    string strJOB_NAME = datHistory.Tables[0].Rows[g]["JOB_NAME"].ToString();
                    string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                    string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();
                    //string strAREAID = datHistory.Tables[0].Rows[0]["AREAID"].ToString();

                    strTtsp_msg_log = strTtsp_msg_log + "\r\n" + " + : " + strLOG_DATE + "//" + strJOB_NAME + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;
                    //lay du lieu tu bang MSG_LOG
                    txthistory.Text = strTtsp_msg_log;
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
        //su kien click vao Tabcontrol
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
        //su kien thoat khoi form
        private void frmTTSPMsgInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (iClose == false)
                {
                    if (strSucess == false)
                    {
                        if (NeedConfirm == true)
                        {
                            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all TTSP Message Information windows ?", Common.sCaption);
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

        private void cmdiqs_Click(object sender, EventArgs e)
        {
            if (strMessageTYPE.Trim() == "MT195" || strMessageTYPE.Trim() == "MT196")
            {
                MessageBox.Show("Invalid message type!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (strMsgDirection.Trim() == "SIBS-TTSP" || strMsgDirection.Trim() == "TTSP-SIBS")
                {
                    string strMsg_Type = "";
                    frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                    //this.Hide();
                    selectIQS.ShowDialog();
                    strMsg_Type = selectIQS.strMSG_TYPE;
                    if (frmSelectTypeIQS.isOK == true)
                    {
                        frmIQSNew1 IQSNew1 = new frmIQSNew1();
                        IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(strMSG_ID);
                        IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                        IQSNew1.objInfoIQSNew1.REF_NUMBER = strRmno;
           
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
                        IQSNew1.objInfoIQSNew1.FROMBANK = HO;
                
                        if (txtRmno.Text.Trim().Length > 3)
                        {
                            IQSNew1.objInfoIQSNew1.TOBANK = txtRmno.Text.Trim().Substring(0, 3);
                        }

                        IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = txtRmno.Text.Trim();
                        IQSNew1.objInfoIQSNew1.MSGCONTENT = txtcontent.Text;
                        IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                        IQSNew1.strAmount = txtamount.Text.Trim();
                        IQSNew1.strCurrency = lbccycd.Text.Trim();
                        IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(txttran_date.Text.Trim());
                        IQSNew1.strMSG_TYPE = strMsg_Type;
                        if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                        {
                            MessageBox.Show("Could not create IQS message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        IQSNew1.ShowDialog();
                    }
                    else if (frmSelectTypeIQS.isOK == false)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid message type!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }                
            }            
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
             frmPrint frmPrint = new frmPrint();
            //DONGPV
            //string Print = "BM_TTSP01";             
            string Print = "MT103";             
            frmPrint.PrintType = Print;
            GetData_Print();
            if (dtMap == null || dtMap.Rows.Count == 0)
            {
                MessageBox.Show("Data not found!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                frmPrint.HMdat = dtMap;
                frmPrint.ShowDialog();
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

        private void frmTTSPMsgInfo_KeyDown(object sender, KeyEventArgs e)
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

        private void frmTTSPMsgInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        //purpose: Phuc vu in dien 
        private void GetData_Print()
        {
            try
            {
                DataTable datContent = new DataTable();
                datContent = objControlCONTENT.GetData_print_ttsp(strMSG_ID.Trim(), strMessageTYPE, strMsgDirection);
                if (datContent == null || datContent.Rows.Count == 0)
                {
                    dtMap = null;
                }
                else
                {
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
