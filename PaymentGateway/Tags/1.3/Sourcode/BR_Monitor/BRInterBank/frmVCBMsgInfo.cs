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
using BR.BRSYSTEM;
using System.Text.RegularExpressions;

namespace BR.BRInterBank
{
    public partial class frmVCBMsgInfo : BR.BRSYSTEM.frmBasedata
    {
        private GetData objGetData = new GetData();
        VCB_MSG_LOGInfo objVcb_log = new VCB_MSG_LOGInfo();
        VCB_MSG_LOGController objcontrolVcb_log = new VCB_MSG_LOGController();
        EXCELInfo objExcel = new EXCELInfo();
        EXCELController objControlExcel = new EXCELController();
        VCB_MSG_CONTENTInfo objInfoCONTENT = new VCB_MSG_CONTENTInfo();
        VCB_MSG_CONTENTController objControlCONTENT = new VCB_MSG_CONTENTController();
        USERSController objBOUser = new USERSController();
        TADController objcontrolTad = new TADController();

        public bool bIsCloseForm = false;
        private bool iClose = false;
        SWIFT_BANK_MAPInfo objSwift_bank = new SWIFT_BANK_MAPInfo();
        SWIFT_BANK_MAPController objControl_bank = new SWIFT_BANK_MAPController();
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranch = new BRANCHController();

        string HO;

        private bool NeedConfirm = true;
        private static bool strSucess = false;

        //-0-------------------------------------------------
        public DataTable dtMap;
        public  string strMSG_ID;
        public  string strQUERY_ID;
        public  string strSender;//ma ngan hang gui
        public  string strReceiving;//ma ngan hang nhan
        public  string strAmount;//so tien
        public  string strRmno;
        public  string strTransdate;//ngay
        public  string strDepartment;//
        public  string strCONTENT;
        public  string strFIELD20;//Rmno
        public  string strRefNo;
        public string strCCYCD;
        public string strMSG_DIRECTION;//chieu cua dien
        public string strRM_NUMBER;
        public string strMsgDirection;
        public string strMessageTYPE;
        public string strBranch;
        public string strUserid;
        private string strPrintSTS = "0";
        
        //-----------------------------------------------------
        
        public frmVCBMsgInfo()
        {
            InitializeComponent();
        }
        private void GET_CONTENT()
        {
            try
            {
                DataTable dsCONTENT = new DataTable();
                dsCONTENT = objControlCONTENT.GetVCB_MSG_CONTENT_CONTENT(strMSG_ID, "VCB");
                if (dsCONTENT.Rows.Count == 0)
                {
                    strCONTENT = "";
                }
                else
                {
                    strCONTENT = dsCONTENT.Rows[0]["CONTENT"].ToString();
                    strPrintSTS = dsCONTENT.Rows[0]["PRINT_STS"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void frmVCBMsgInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                dateTimePicker1.Enabled = false;
                if (!objGetData.FillDataComboBox(cbPrintSTS, "CONTENT", "CDVAL",
                    "ALLCODE", "cdname='PRINTSTS'", "CONTENT", true, false, ""))
                    return;
                try
                {
                    GET_CONTENT();

                    //----lay thong tin truyen sang tu frmList
                    txtsending_bank.Text = strSender.Replace("\r", "").Replace("\r\n", "");//ma ngan hang gui
                    txtreceiving_bank.Text = strReceiving.Replace("\r", "").Replace("\r\n", "");//ma ngan hang nhan
                    if (strAmount.Trim() == "")
                    {
                        txtamount.Text = "";
                    }
                    else
                    {
                        txtamount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }                    
                    lbCCYCD.Text = strCCYCD.Replace("\r", "").Replace("\r\n", "");//loai tien
                    txtrm_no.Text = strFIELD20.Replace("\r", "").Replace("\r\n", "");//Rmno
                    txttran_date.Text = strTransdate.Replace("\r", "").Replace("\r\n", "");
                    dateTimePicker1.Value =Convert.ToDateTime(strTransdate.Replace("\r", "").Replace("\r\n", ""));
                    txtdepartment.Text = strDepartment.Replace("\r", "").Replace("\r\n", "");//phan he
                    txtcontent.Text = strCONTENT;
                    txtRmno.Text = strRM_NUMBER.Replace("\r", "").Replace("\r\n", "");
                    cbPrintSTS.SelectedValue = strPrintSTS;
                    //lblRefNo.Text = frmVCBMsgList.strRefNo;//HaNTT10
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
                
                    //------lay  ra ten ngan hang gui-----lay trong bang SWIFT_BANH_MAP-------------
                    string strMsgid = strMSG_ID.Trim().Replace("\r", "").Replace("\r\n", "");
                    DataSet datMap = new DataSet();
                    datMap = objControl_bank.GetSWIFT_BANK_MAPB(strSender.Trim().Replace("\r", "").Replace("\r\n", "").Replace("00", ""));
                    if (datMap.Tables[0].Rows.Count == 0 || datMap==null)
                    {
                        lbSend_bank_name.Text = "";
                    }
                    else
                    {
                        lbSend_bank_name.Text = datMap.Tables[0].Rows[0]["BANK_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                    }
                    if (lbSend_bank_name.Text.Trim() == "")
                    {
                        if (strSender.Trim().Length == 5)
                        {
                            if (strSender.Trim().Substring(0, 2) == "00")
                            {
                                //map o bang bRank------------------------------------------------------------------
                                DataSet datRece = new DataSet();
                                datRece = objctrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));
                                if (datRece.Tables[0].Rows.Count == 0 || datRece == null)
                                {
                                    lbSend_bank_name.Text = "";
                                }
                                else
                                {
                                    lbSend_bank_name.Text = datRece.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                                    txtsending_bank.Text = strSender.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3);
                                }
                            }
                        }
                        if (strSender.Trim().Length == 3)
                        {
                            if (Regex.IsMatch(strSender.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                //map o bang bRank------------------------------------------------------------------
                                DataSet datRece = new DataSet();
                                datRece = objctrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                                if (datRece.Tables[0].Rows.Count == 0 || datRece == null)
                                {
                                    lbSend_bank_name.Text = "";
                                }
                                else
                                {
                                    lbSend_bank_name.Text = datRece.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                                    txtsending_bank.Text = strSender.Trim().Replace("\r", "").Replace("\r\n", "");
                                }
                            }
                        }
                    }
                    //----------------------------------------------------------------------------------------------------
                    //---------lay ra ten ngan hang nhan--------Lay trong bang BRANCH-------------------------------------
                    DataSet datMap1 = new DataSet();
                    datMap1 = objControl_bank.GetSWIFT_BANK_MAPB(strReceiving.Trim().Replace("\r", "").Replace("\r\n", "").Replace("00", ""));
                    if (datMap1.Tables[0].Rows.Count == 0 || datMap1 == null)
                    {
                        lbRece_name.Text = "";
                    }
                    else
                    {
                        lbRece_name.Text = datMap1.Tables[0].Rows[0]["BANK_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                    }
                    if (lbRece_name.Text.Trim() == "")
                    {
                        if (strReceiving.Trim().Length == 5)
                        {
                            if (strReceiving.Trim().Substring(0, 2) == "00")
                            {
                                //map o bang bRank------------------------------------------------------------------
                                DataSet datRece2 = new DataSet();
                                datRece2 = objctrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));
                                if (datRece2.Tables[0].Rows.Count == 0 || datRece2 == null)
                                {
                                    lbRece_name.Text = "";
                                }
                                else
                                {
                                    lbRece_name.Text = datRece2.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                                    txtreceiving_bank.Text = strReceiving.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3);
                                }
                            }
                        }
                        if (strReceiving.Trim().Length == 3)
                        {
                            if (Regex.IsMatch(strReceiving.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                DataSet datRece2 = new DataSet();
                                datRece2 = objctrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", ""));
                                if (datRece2.Tables[0].Rows.Count == 0 || datRece2 == null)
                                {
                                    lbRece_name.Text = "";
                                }
                                else
                                {
                                    lbRece_name.Text = datRece2.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                                    txtreceiving_bank.Text = strReceiving.Trim().Replace("\r", "").Replace("\r\n", "");
                                }
                            }
                        }
                    }
                  
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            string strQUERY_ID1 = strQUERY_ID.Replace("\r", "").Replace("\r\n", "");
            GetHistory(strQUERY_ID1);//goi ham lay ra lich su dien
        }

        // lay ra thong tin lich su dien
        private void GetHistory(string QUERY_ID)
        {
            try
            {
                string strVcb_msg_log="";
                //lay du lieu tu bang IBPS_MSG_LOG
                DataSet datHistory = new DataSet();
                datHistory = objcontrolVcb_log.GetVCB_MSG_LOG(QUERY_ID);//kiem tra lai
                int g = 0;
                while (g < datHistory.Tables[0].Rows.Count)
                {
                    string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                    string strQUERY_ID = datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString();
                    string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                    string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();

                    strVcb_msg_log = strVcb_msg_log + "\r\n" + " + : " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;
                    //lay du lieu tu bang MSG_LOG
                    txthistory.Text = strVcb_msg_log;
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
        //su kien chon cac Tabcontrol
        private void tabInfomation_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (tabInfomation.SelectedIndex == 0)
                {
                    //cmdiqs.Show(); 
                    cmdPrint.Show();
                    this.cmdPrint.Location = new Point(cmdClose.Location.X - cmdClose.Size.Width - 5, cmdClose.Location.Y);
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
        private void cmdiqs_Click(object sender, EventArgs e)
        {
           
            if (strMessageTYPE.Trim() == "MT195" || strMessageTYPE.Trim() == "MT196")
            {
                Common.ShowError("Invalid message type!", 2, MessageBoxButtons.OK);                
                return;
            }
            else
            {
                string  strMsg_Type = "";
                frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();                
                selectIQS.ShowDialog();
                strMsg_Type = selectIQS.strMSG_TYPE;
                if (frmSelectTypeIQS.isOK == true)
                {
                    frmIQSNew1 IQSNew1 = new frmIQSNew1();
                    IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(strMSG_ID);
                    IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    IQSNew1.objInfoIQSNew1.REF_NUMBER = txtrm_no.Text.Trim();                    
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
                    
                    if (strMsgDirection == "VCB-SIBS")
                    {
                        if (txtreceiving_bank.Text.Trim().Length == 5)
                        {
                            if (txtreceiving_bank.Text.Trim().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                            {
                                IQSNew1.objInfoIQSNew1.TOBANK = txtreceiving_bank.Text.Trim().Substring(2, 3);
                            }
                        }
                        else
                        {
                            IQSNew1.objInfoIQSNew1.TOBANK = txtreceiving_bank.Text.Trim();
                        }
                        if (strMsg_Type == "TS")// khong cho tao dien tra soat theo chieu den "VCB-SIBS"
                        {
                            Common.ShowError("Could not create IQS message!", 3, MessageBoxButtons.OK);                             
                            return;
                        }
                    }
                    else
                    {
                        if (txtsending_bank.Text.Trim().Length == 5)
                        {
                            if (txtsending_bank.Text.Trim().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                            {
                                IQSNew1.objInfoIQSNew1.TOBANK = txtsending_bank.Text.Trim().Substring(2, 3);
                            }
                        }
                        else
                        {
                            IQSNew1.objInfoIQSNew1.TOBANK = txtsending_bank.Text.Trim();
                        }
                    }
                    IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = txtRmno.Text.Trim();//txtrm_no.Text;
                    IQSNew1.objInfoIQSNew1.MSGCONTENT = txtcontent.Text;
                    IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                    IQSNew1.strAmount = txtamount.Text.Trim();
                    IQSNew1.strCurrency = lbCCYCD.Text.Trim();
                    IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(txttran_date.Text.Trim());
                    IQSNew1.strMSG_TYPE = strMsg_Type;

                    if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                    {
                        Common.ShowError("Could not create IQS message!", 3, MessageBoxButtons.OK);                         
                        return;
                    }
                    IQSNew1.ShowDialog();
                }
                else if (frmSelectTypeIQS.isOK == false)
                {
                    return;
                }
            }
            //this.Show();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
           
            frmPrint frmPrint = new frmPrint();            
            //string Print = "VCB_PRINT";

            string Print = "";            
            DataSet dtData = new DataSet();

            if (strMSG_DIRECTION == "VCB-SIBS")
                Print = "VCB_PRINT_1";
            else
                Print = "VCB_PRINT_2";
            frmPrint.PrintType = Print;
            frmPrint.strMsgID = strMSG_ID;
            frmPrint.loaidien = strMessageTYPE;
            frmPrint.chieudien = strMsgDirection;            
            dtData = objBOUser.Userid_UD(Common.Userid);
            strUserid = dtData.Tables[0].Rows[0]["USERNAME"].ToString();
            strBranch = dtData.Tables[0].Rows[0]["BRANCH"].ToString().Substring(0, 3).PadLeft(5, '0');
            frmPrint.pBranch = strBranch;
            frmPrint.pUserid = strUserid;

            GetData_Print();
            if (dtMap == null || dtMap.Rows.Count == 0)
            {
                MessageBox.Show("Data not found!", Common.sCaption);
            }
            else
            {
                frmPrint.HMdat = dtMap;
                frmPrint.ShowDialog();
            }
            
        }

        private void tabInfomation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

     
        private void frmVCBMsgInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (iClose == false)
                {
                    if (strSucess == false)
                    {
                        if (NeedConfirm == true)
                        {
                            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all VCB Message Information windows ?", Common.sCaption);
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

        private void frmVCBMsgInfo_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmVCBMsgInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        //purpose: Phuc vu in dien 
        private void GetData_Print()
        {
            try
            {
                DataTable datContent = new DataTable();
                datContent = objControlCONTENT.GetData_print_vcb(strMSG_ID, strMessageTYPE, 
                    strMSG_DIRECTION,strBranch,strUserid);
                if (datContent == null || datContent.Rows.Count == 0)
                {
                    dtMap = null;
                }
                else
                {
                    /////////////////////////////*/
                    //Cap nhat trang thai in dien                    
                    objInfoCONTENT.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                    objInfoCONTENT.PRINT_STS = 1;
                    objControlCONTENT.Update_Print_STS(objInfoCONTENT);
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
