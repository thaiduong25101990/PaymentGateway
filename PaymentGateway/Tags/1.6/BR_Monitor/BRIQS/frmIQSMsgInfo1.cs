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
using BR.BRLib;

namespace BR.BRIQS
{
    public partial class frmIQSMsgInfo1 : frmBasedata
    {
        #region Ham va bien
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranh = new BRANCHController();
        IQS_MSG_LOSGInfo objMsg_log = new IQS_MSG_LOSGInfo();
        IQS_MSG_CONTENTController objctrlIQS = new IQS_MSG_CONTENTController();
        IQS_MSG_LOSGController objCtrlMsg_log = new IQS_MSG_LOSGController();
        private bool NeedConfirm = true;        
        public bool bIsCloseForm = false;
        private bool iClose = false;        
        public string strFromBank;
        public string strToBank;
        public string strRmno;
        public string strDateCreate;
        public string strContent;
        public string strQUERY_ID;
        public string strIQSTransNum;
        public string strAmount;
        public string strCCYCD;
        public string strProductCode;
        public string strStatus;
        public string strTransDate;
        public string strTellerID;
        public string strMSGCONTENT;
        public string strREFnumber;        
        private static bool strSucess = false;
        #endregion

        public frmIQSMsgInfo1()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            iClose = true;
            this.Close();
            iClose = false;
        }

        private void frmIQSMsgInfo1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                Getdatatextbox();
                cmdPrint.Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Getdatatextbox()
        {
            
            try
            {
                dtpTransactionDate.Enabled = false;
                txtIQSTransNumber.Text = strIQSTransNum.Trim().Replace("\r", "").Replace("\r\n", "");
                txtREFnumber.Text = strREFnumber.Trim().Replace("\r", "").Replace("\r\n", "");
                txtRMRef.Text = strRmno.Trim().Replace("\r", "").Replace("\r\n", "");
                txtSendingBranch.Text = strFromBank.Trim().Replace("\r", "").Replace("\r\n", "");
                txtReceivingBranch.Text = strToBank.Trim().Replace("\r", "").Replace("\r\n", "");                
                if (strAmount.Trim() == "")
                {
                    txtAmount.Text = "";
                }
                else
                {
                    txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                }
                lblAmount.Text = strCCYCD.Trim().Replace("\r", "").Replace("\r\n", "");
                txtProductCode.Text = strProductCode.Trim().Replace("\r", "").Replace("\r\n", "");
                txtStatus.Text = strStatus.Trim().Replace("\r", "").Replace("\r\n", "");
                dtpTransactionDate.Text = strTransDate.Trim().Replace("\r", "").Replace("\r\n", "");
                dtpDate.Value = Convert.ToDateTime(strDateCreate.Trim().Replace("\r", "").Replace("\r\n", ""));
                txtTellerID.Text = strTellerID.Trim().Replace("\r", "").Replace("\r\n", "");
                txtcontent.Text = strContent.Trim().Replace("\r", "").Replace("\r\n", "");

                //lay ra chi nhanh nhan dien va gui dien tu Bang Breanch-----
                //chi nhanh gui dien-----------------------------------------
                DataSet datBranh = new DataSet();
                datBranh = objctrlBranh.GetBRANCH_MAP(strFromBank.Trim().Replace("\r", "").Replace("\r\n", ""));
                if (datBranh.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                        lblSendingBranch.Text = datBranh.Tables[0].Rows[0]["BRAN_NAME"].ToString().Trim().Replace("\r", "").Replace("\r\n", "");
                }
                //chi nhanh nhan dien-----------------------------------------
                DataSet datBranh1 = new DataSet();
                datBranh1 = objctrlBranh.GetBRANCH_MAP(strToBank.Trim().Replace("\r", "").Replace("\r\n", ""));
                if (datBranh1.Tables[0].Rows.Count != 0)
                {
                    lblReceivingBranch.Text = datBranh1.Tables[0].Rows[0]["BRAN_NAME"].ToString().Trim().Replace("\r", "").Replace("\r\n", "");
                }
                //-----------------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            GetHistory(strQUERY_ID.Replace("\r", "").Replace("\r\n", "").Trim());
        }
        private void GetHistory(string QUERY_ID)
        {
            try
            {
                string strIbps_msg_log = "";
                //lay du lieu tu bang IBPS_MSG_LOG
                DataTable datHistory = new DataTable();
                datHistory = objCtrlMsg_log.GetIQS_LOG(QUERY_ID);
                int g = 0;
                while (g < datHistory.Rows.Count)
                {
                    string strLOG_DATE = datHistory.Rows[g]["LOG_DATE"].ToString();
                    string strQUERY_ID = datHistory.Rows[g]["QUERY_ID"].ToString();
                    string strSTATUS = datHistory.Rows[g]["STATUS"].ToString();
                    string strDESCRIPTIONS = datHistory.Rows[g]["DESCRIPTIONS"].ToString();
                   
                    strIbps_msg_log = strIbps_msg_log + "\r\n" + ": " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;
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

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCDInfo_FormClosing(null,null);
                this.Close();
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            string Msg_Type;
            DataTable dt = new DataTable();
            int strID = Convert.ToInt32(strQUERY_ID);
            dt = objctrlIQS.Get_MSGTYPE(strID);
            if (dt.Rows.Count > 0)
            {
                Msg_Type = dt.Rows[0]["MSG_TYPE"].ToString().Trim();
                frmPrint print = new frmPrint();
                print.strQueryIQS = strID;
                if (Msg_Type.ToString() == "MT199")
                {
                    print.PrintType = "IQS_03";
                    print.ShowDialog();
                }
                else
                {
                    print.PrintType = "IQS_02";
                    print.ShowDialog();
                }                             
            }
            else
            {
                Common.ShowError("Data not found!", 2, MessageBoxButtons.OK);                
            }
        }

        private void frmIQSMsgInfo1_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmIQSMsgInfo1_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmIQSMsgInfo1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (iClose == false)
                {
                    if (strSucess == false)
                    {
                        if (NeedConfirm == true)
                        {
                            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all IQS Message Information windows ?", Common.sCaption);
                            if (e.Cancel == true)
                            { iClose = false; bIsCloseForm = false; }
                            else
                            { bIsCloseForm = true; }
                        }
                    }
                    else
                    { bIsCloseForm = false; }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                { cmdPrint.Show(); }
                else
                { cmdPrint.Hide(); cmdClose.Show(); }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

    }
}
