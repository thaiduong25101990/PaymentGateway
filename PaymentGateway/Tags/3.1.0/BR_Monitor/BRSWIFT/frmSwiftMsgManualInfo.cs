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

namespace BR.BRSWIFT
{
    public partial class frmSwiftMsgManualInfo : Form
    {
        #region khai bao cac lop trong bussiness
        public SWIFT_MSG_CONTENTInfo objInfo = new SWIFT_MSG_CONTENTInfo();
        private SWIFT_MSG_CONTENTController objControl = new SWIFT_MSG_CONTENTController();
        SWIFT_PROCESS_HANDERController objHander = new SWIFT_PROCESS_HANDERController();
        SWIFT_PROCESS_HANDERInfo objInfoHander = new SWIFT_PROCESS_HANDERInfo();
        GROUPSInfo objInfoGroup = new GROUPSInfo();
        GROUPSController objControlGroup = new GROUPSController();
        //----------------------------------------------------------------------------------
        IBPS_MSG_CONTENTController objctrlIB = new IBPS_MSG_CONTENTController();
        //----------------------------------------------------------------------------------
        SWIFT_MSG_LOGInfo objSWIFT_log = new SWIFT_MSG_LOGInfo();
        SWIFT_MSG_LOGController objcontrolSWIFT_log = new SWIFT_MSG_LOGController();
        //----------------------------------------------------------------------------------
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        //----------------------------------------------------------------------------------
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranch = new BRANCHController();
        //----------------------------------------------------------------------------------
        USERSInfo ObjUser = new USERSInfo();
        USERSController ObjCtrlUser = new USERSController();
        //----------------------------------------------------------------------------------
        ALLCODEInfo objAllcodein = new ALLCODEInfo();
        ALLCODEController objAllcode = new ALLCODEController();
        //----------------------------------------------------------------------------------
        SWIFTPRINTInfo objPRINT = new SWIFTPRINTInfo();
        SWIFTPRINTController ObjCtrlPrint = new SWIFTPRINTController();
        //----------------------------------------------------------------------------------
        SWIFT_BANK_MAPInfo objWift_bank = new SWIFT_BANK_MAPInfo();
        SWIFT_BANK_MAPController objcontrolBank = new SWIFT_BANK_MAPController();
        SWIFT_RMBR_AUTOInfo objAuto = new SWIFT_RMBR_AUTOInfo();
        SWIFT_RMBR_AUTOController objCtrlAuto = new SWIFT_RMBR_AUTOController();
        //----------------------------------------------------------------------------------
        SWIFT_MSG_CONTENTController objControlContent = new SWIFT_MSG_CONTENTController();
        #endregion

        #region khai bao cac bien
        private int isSupervisor;        
        //private int iID = 0;
        //private int QueryID = 0;
        //private string auto = "N";
        public Boolean isResend = true;
        public Boolean isApprove = true;
        private string strBank;
        //----------------------------------------------------------------------------------
        public string strMsg_id;
        public string strQUERY_ID;
        public string strSender;
        public string strReceiving;//update lai trong datatbase strReceiving
        public string strAmount;
        public string strF20;
        public string strTrans_date;
        public string strteller;
        public string strDepartment;//update lai trong datatbase
        public string strSTATUS;
        public string strContent;
        public string strAuto;
        public string strSWMSTS;
        public string strPROCESSSTS;
        public string strNEW_PROCESSSTS;       
        public string strCCYCD;
        public string strMsg_type;
        public string strMSG_DIRECTION;
        private string strResen;
        public string strRM_NUMBER;
        public string strMSG_SRC;        
        public DataTable dtMap;
        public string strMapField;
        public DateTime dSENDING_TIME;
        private string Log_Branch;
        //----------------------------------------------------------------------------------
        public bool bIsCloseForm = false;
        private bool iClose = false;
        public string strTable_name;//Ten bang du lieu       
        //----------------------------------------------------------------------------------
        public string strLOCKSTS;
        public string strLOCK_TELLERID;        
        //private string Table_Name;
        #endregion 


        public frmSwiftMsgManualInfo()
        {
            InitializeComponent();
        }
        private void NEW_PROCESSSTS()
        {            
            try
            {                
                DataTable datNEW_PROCESSSTS = new DataTable();
                datNEW_PROCESSSTS = objControl.GetData_Pre(strMsg_id.Trim(), strTable_name);
                strNEW_PROCESSSTS = datNEW_PROCESSSTS.Rows[0]["NEW_PROCESSSTS"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //lay truong content tuong ung trong bang du lieu
        private void Getcontent()
        {
            try
            {               
                DataTable datContent = new DataTable();
                datContent = objControl.Search_Content(strMsg_id.Trim());
                if (datContent != null)
                {
                    strContent = datContent.Rows[0]["CONTENT"].ToString();
                    txtcontent.Text = strContent;
                }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void frmSwiftMsgManualInfo_Load(object sender, EventArgs e)
        {
            try
            {
                /*Neu theo yeu caau cua MSB thi khong can xu ly hai tay */
                /*Phai hoi lai la supper co quyen xu ly dien hay khong*/
                /*de co can check supper hay khong*/
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                Common.iCancel = 0;
                Common.iOk = 0;
                Getcontent();//lay truong content trong bang khi truyen sang-----------------
                iClose = false;
                cmdApprove.Enabled = false; cmdOK.Enabled = false;  cmdReject.Enabled = false;               
                objInfo.OK = "False"; objInfo.Reject = "False"; objInfo.Approve = "False";               
                CheckIsSupervisor();//check quyen user dang nhap                
                txtReceivedBranch.Focus();
                //-----------------------------------------------------------------------
                cboDepartment.DataSource = objAllcode.GetALLCODE("Department", "SYSTEM");
                cboDepartment.DisplayMember = "CONTENT";
                cboDepartment.ValueMember = "CDVAL";
                //------------------------------------------------------------------------
                LoadData();//lay du lieu day len otext
                Supper_Tell();//kiem tra du lieu va quyen tuong ung               
                GetHistory(Convert.ToInt32(strQUERY_ID));//goi ham lay ra lich su dien
                //------------------------------------------------------------------------                
                cmdCancel.Focus();
                cmdApprove.BackColor = Color.White;
                cmdReject.BackColor = Color.White;
                cmdOK.BackColor = Color.White;
                cmdCancel.BackColor = Color.Green;                
                //------------------------------------------------------------------------
                if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                {
                    this.Text = "Swift resend messages";
                    cmdApprove.Visible = false;
                    cmdReject.Visible = false;
                    cboDepartment.Enabled = true;
                    txtReceivedBranch.Enabled = true;
                }
                else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                {  /*Neu la xu ly thu cong thi chi thuc hien mot tay*/
                    this.Text = "Swift inward manual process";
                    if (isSupervisor == 3)
                    {
                        Check_approve();
                    }
                }
                if (strSTATUS == "-1")
                {
                    cmdOK.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Check_approve()
        {
            try
            {
                DataTable _dt = new DataTable();
                _dt = objControl.GET_SWIFT_PROCESS_HANDER(strMsg_id);
                if (_dt.Rows.Count == 0)
                {
                    cmdApprove.Enabled = false;
                    cmdReject.Enabled = false;
                    cmdOK.Enabled = true;
                    txtReceivedBranch.Enabled = true;
                    cboDepartment.Enabled = true;
                }
                else
                {
                    if (_dt.Rows[0]["TELLER_ID"].ToString().Trim() == Common.Userid)/*Neu la cua user do xu ly khong duoc Approve*/
                    {
                        MessageBox.Show("You can not approve this message, that you create! ", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //this.cmdReject.Location = new Point(cmdOK.Location.X, cmdOK.Location.Y);
                        //this.cmdApprove.Location = new Point(cmdRelease.Location.X, cmdRelease.Location.Y);
                        cmdOK.Enabled = false;                        
                        cmdReject.Enabled = false;
                        cmdApprove.Enabled = false;
                        cboDepartment.Enabled = false;
                        txtReceivedBranch.Enabled = false;
                    }
                    else
                    {
                        //this.cmdReject.Location = new Point(cmdOK.Location.X, cmdOK.Location.Y);
                        //this.cmdApprove.Location = new Point(cmdRelease.Location.X, cmdRelease.Location.Y);
                        cboDepartment.Enabled = false;
                        txtReceivedBranch.Enabled = false;
                        cmdOK.Enabled = false;
                        cmdReject.Enabled = true;
                        cmdApprove.Enabled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham xu ly cac su kien
        private void Supper_Tell()
        {
            try
            {
                #region ---------supervisor---------------------------
                if (isSupervisor == 2)//supervisor
                {
                    if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                    {
                        if (strPROCESSSTS.Trim() == "8")//WAPPROVING-RESENT
                        {                           
                            cmdApprove.Enabled = true;
                            cmdReject.Enabled = true;
                            cmdOK.Enabled = false;
                            txtReceivedBranch.Enabled = false;
                            txtReceivedBranch.BackColor = Color.White;                            
                            cboDepartment.Enabled = false;
                            cboDepartment.BackColor = Color.White;
                        }                        
                        else
                        {
                            txtReceivedBranch.Enabled = false;
                            cboDepartment.Enabled = false;
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            cmdOK.Enabled = false;
                        }
                    }
                    else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                    {
                        if (strPROCESSSTS.Trim() == "7")//WAPPROVING
                        {
                            cmdApprove.Enabled = true;
                            cmdReject.Enabled = true;
                            cmdOK.Enabled = false;
                            txtReceivedBranch.Enabled = false;
                            txtReceivedBranch.BackColor = Color.White;
                            cboDepartment.Enabled = false;
                            cboDepartment.BackColor = Color.White;
                        }
                        else
                        {
                            txtReceivedBranch.Enabled = false;
                            cboDepartment.Enabled = false;
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            cmdOK.Enabled = false;
                        }
                    }
                }
                #endregion
                #region ---------Teller---------------------------
                if (isSupervisor == 1)//Teller
                {
                    if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                    {                       
                        if (strSTATUS.Trim() == "1" && strPROCESSSTS.Trim() != "8")                        
                        {
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            if (cboDepartment.Text.Trim() == "Other")
                            {
                                cmdOK.Enabled = false;
                            }
                            else
                            {
                                cmdOK.Enabled = true;
                                txtReceivedBranch.Enabled = true;
                                cboDepartment.Enabled = true;
                            }
                        }
                        if (strPROCESSSTS.Trim() == "")
                        {
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            cmdOK.Enabled = true;
                            txtReceivedBranch.Enabled = true;
                            cboDepartment.Enabled = true;
                        }                     
                    }
                    else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                    {
                        if (strPROCESSSTS.Trim() == "6")//duoc phep thuc hien 
                        {
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            txtReceivedBranch.Enabled = true;
                            cboDepartment.Enabled = true;
                            //----------------------------------------
                            if (cboDepartment.Text.Trim() == "Other")
                            {
                                cmdOK.Enabled = false;
                            }
                            else
                            {
                                cmdOK.Enabled = true;
                            }
                            //----------------------------------------
                        }
                        else//khong duoc phep thuc hien
                        {
                            txtReceivedBranch.Enabled = false;
                            cboDepartment.Enabled = false;
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            cmdOK.Enabled = false;
                        }
                    }
                }
                #endregion ---------Teller and supervisor---------------------------
                #region---Teller and supervisor---------------------
                if (isSupervisor == 3)//Teller and supervisor
                {
                    if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                    {                        
                        if (strSTATUS.Trim() == "1" )//chon cac dien co trang thai la SENT
                        {
                            //----------------------------------------
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            if (cboDepartment.Text.Trim() == "Other")
                            {
                                cmdOK.Enabled = false;
                            }
                            else
                            {
                                cmdOK.Enabled = true;
                            }
                            //-----------------------------------------
                            if (strPROCESSSTS.Trim() == "8")//duoc phep thuc hien 
                            {
                                cmdApprove.Enabled = true;
                                cmdReject.Enabled = true;
                                cmdOK.Enabled = false;
                                txtReceivedBranch.Enabled = false;
                                txtReceivedBranch.BackColor = Color.White;
                                cboDepartment.Enabled = false;
                                cboDepartment.BackColor = Color.White;
                            }
                            else
                            {
                                cmdApprove.Enabled = false;
                                cmdReject.Enabled = false;
                                cmdOK.Enabled = true;
                                txtReceivedBranch.Enabled = true;
                                cboDepartment.Enabled = true;
                            }
                        }
                        else
                        {
                            txtReceivedBranch.Enabled = false;
                            cboDepartment.Enabled = false;
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            cmdOK.Enabled = false;
                        }                       
                    }
                    else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                    {
                        if (strPROCESSSTS.Trim() == "6" || strPROCESSSTS.Trim() == "7")//duoc phep thuc hien 
                        {
                            if (strPROCESSSTS.Trim() == "6")//duoc phep thuc hien 
                            {
                                cmdApprove.Enabled = false;
                                cmdReject.Enabled = false;
                                txtReceivedBranch.Enabled = true;
                                cboDepartment.Enabled = true;
                                if (cboDepartment.Text.Trim() == "Other")
                                {
                                    cmdOK.Enabled = false;
                                }
                                else
                                {
                                    cmdOK.Enabled = true;
                                }
                            }
                            if (strPROCESSSTS.Trim() == "7")//duoc phep thuc hien 
                            {
                                cmdApprove.Enabled = true;
                                cmdReject.Enabled = true;
                                cmdOK.Enabled = false;
                                txtReceivedBranch.Enabled = false;
                                txtReceivedBranch.BackColor = Color.White;
                                cboDepartment.Enabled = false;
                                cboDepartment.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            cmdApprove.Enabled = false;
                            cmdReject.Enabled = false;
                            cmdOK.Enabled = false;
                            txtReceivedBranch.Enabled = false;
                            cboDepartment.Enabled = false;
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void CheckIsSupervisor()
        {
            try
            {
                string UserID = Common.Userid;
                DataSet dsGroup = new DataSet();
                dsGroup = objControlGroup.GetGroup_IsSupervisor(UserID, Common.gGWTYPE);
                if (dsGroup.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else if (dsGroup.Tables[0].Rows.Count == 1)
                {
                    int IsSupervisor = Convert.ToInt32(dsGroup.Tables[0].Rows[0][0].ToString());
                    if (IsSupervisor == 1)//neu la 1 la teller
                    {
                        isSupervisor = 1;
                    }
                    else if (IsSupervisor == 2)//neu la 2 la suppervisor
                    {
                        isSupervisor = 2;
                    }
                }
                else if (dsGroup.Tables[0].Rows.Count == 2)//co ca hai quyen teller va suppervisor
                {
                    isSupervisor = 3;//co ca hai quyen
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Refresh_logMessage()
        {
            try
            {
                objHander.DELETE_SWIFT_PROCESS(strMsg_id, Common.Userid);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            iClose = true;
            Common.iOk = 0;
            Common.iCancel = 0;
            objInfo.OK = "False";
            objInfo.Reject = "False";
            objInfo.Approve = "False";
            Refresh_logMessage();
            this.Close();
            iClose = false;
        }

        private void cmdReject_Click(object sender, EventArgs e)
        {
            try
            {
                iClose = true;
                //---lay du lieu ghi vao bang MSG_LOG--------------------
                objSWIFT_log.LOG_DATE = DateTime.Now;
                objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                //-------------------------------------------------------      
                #region//ghi log vao bang SWIFT_MSG_LOG---------------------
                if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                { objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : Resend reject"; }
                else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                { objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : reject"; }                
                objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                //---------------Ghi log vao bang userlog----------------------------------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "";
                int Log_level = 1;
                string strWorked = "Reject";
                string strTable = "SWIFT_PROCESS_HANDER";
                string strOld_value = "";
                string strNew_value = "";
                //-----------------------------------------
                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                //--------------------------------------------------------------------------------
                objInfo.Reject = "True";
                DataTable datSK = new DataTable();
                datSK = ObjCtrlPrint.Search_QueryID(strQUERY_ID.Trim());
                if (datSK.Rows.Count != 0)//co tao  bang ke voi dien nay
                {   ObjCtrlPrint.Delete_Resend(strQUERY_ID);  }                
                cmdReject.Enabled = false;
                #endregion                
                // Xoa du lieu o bang SWIFT_PROCESS_HANDER
                if (objHander.DELETE_SWIFT_PROCESS_HANDER(Convert.ToInt32(strMsg_id)) == 1)
                {
                    Common.ShowError("Data has rejected successfully!", 1, MessageBoxButtons.OK);                    
                    Common.iOk = 1;
                    this.Close();
                }
                else
                {
                    Common.ShowError("Data has rejected error!", 2, MessageBoxButtons.OK);                    
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            iClose = false;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            iClose = true;
            try
            {
                string pNEWBRANCH = txtReceivedBranch.Text;
                if (string.IsNullOrEmpty(txtReceivedBranch.Text.Trim()) || String.IsNullOrEmpty(cboDepartment.Text.Trim()))
                {
                    Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                    
                    txtReceivedBranch.Focus();
                    cmdApprove.BackColor = Color.White;
                    cmdReject.BackColor = Color.White;
                    cmdOK.BackColor = Color.White;
                    cmdCancel.BackColor = Color.White;
                    return;
                }
                else
                {
                    if (txtReceivedBranch.Text.Trim() != "" && cboDepartment.Text != "")
                    {
                        #region Kiem tra
                        if (Regex.IsMatch(txtReceivedBranch.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            DataSet dataBranch = new DataSet();
                            dataBranch = objctrlBranch.GetBRANCH_MAP(txtReceivedBranch.Text.Trim());
                            if (dataBranch.Tables[0].Rows.Count == 0 || dataBranch == null)
                            {
                                Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                                
                                txtReceivedBranch.Focus();
                                cmdApprove.BackColor = Color.White;
                                cmdReject.BackColor = Color.White;
                                cmdOK.BackColor = Color.White;
                                cmdCancel.BackColor = Color.White;
                                return;
                            }
                        }
                        else
                        {
                            if (txtReceivedBranch.Text.Trim().ToUpper() == "LT" )
                            {
                                
                            }
                            else if(txtReceivedBranch.Text.Trim().ToUpper() == "TC")
                            {
                                Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                                
                                txtReceivedBranch.Focus();
                                cmdApprove.BackColor = Color.White;
                                cmdReject.BackColor = Color.White;
                                cmdOK.BackColor = Color.White;
                                cmdCancel.BackColor = Color.White;
                                return;
                            }
                        }
                        #endregion
                        /*Theo yeu cau cua MSB thi cap nhat lai thang nay*/
                        /*Khi xu ly goi ngay lai ham cua nut approver la xong */
                        #region Gui lai dien
                        if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                        {
                            if (strSTATUS.Trim() == Common.STATUS_SENT || strSTATUS.Trim() == Common.STATUS_ERROR)//trang thai send
                            {                                
                                string strGD;
                                string pReciver = txtReceivedBranch.Text.Trim();
                                //////////////////////////////////////////////////////////////////////////
                                //DialogResult DlgResult = new DialogResult();
                                //DlgResult = MessageBox.Show("Do you want to save data?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                //if (DlgResult == DialogResult.Yes)
                                //{
                                objInfo.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                                Common.ShowError("You resended " + objControl.Get_Resend_Num(objInfo), 3, MessageBoxButtons.OK);
                                //////////////////////////////////////////////////////////////////////////
                                if (strReceiving.Trim().Length == 5)
                                { strGD = strReceiving.Trim().Substring(2, 3); }
                                else 
                                { strGD = strReceiving.Trim(); }

                                if (strGD == txtReceivedBranch.Text.Trim() && strDepartment.Trim() == cboDepartment.Text.Trim() && strSTATUS != "-1")
                                {

                                    Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                                    txtReceivedBranch.Focus(); cmdApprove.BackColor = Color.White; cmdReject.BackColor = Color.White;
                                    cmdOK.BackColor = Color.White; cmdCancel.BackColor = Color.White;
                                    return;

                                }
                                else
                                {
                                    // Lay gia tri de ghi log----------------------------------------------------
                                    objSWIFT_log.LOG_DATE = dSENDING_TIME;
                                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                                    objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                                    // --------------------------------------------------------------------------
                                    if (strReceiving.Length == 5)
                                    { objSWIFT_log.DESCRIPTIONS = "Message auto sent to branch:" + strReceiving.Substring(2, 3) + ",department:" + strDepartment; }
                                    else if (strReceiving.Length == 3)
                                    { objSWIFT_log.DESCRIPTIONS = "Message auto sent to branch:" + strReceiving + ",department:" + strDepartment; }
                                    else
                                    { objSWIFT_log.DESCRIPTIONS = "Message auto sent to branch:" + strReceiving + ",department:" + strDepartment; }
                                    //ghi log gia tri ban dau khi service cap nhat trang  thai la auto------------------------
                                    DataTable datLog = new DataTable();
                                    datLog = objcontrolSWIFT_log.SELECT_SWIFT_MSG_LOG(objSWIFT_log);
                                    if (datLog.Rows.Count == 0)
                                    {
                                        if (strPROCESSSTS.Trim() == Common.PROCESSSTS_AUTO)//chi co dien tu dong moi update
                                        { objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG_DATE(objSWIFT_log); }
                                    }
                                    //----------------------------------------------------------------------------------------                                   
                                    objInfoHander.MSG_ID = Convert.ToInt32(strMsg_id);
                                    objInfoHander.NEW_DEPARTMENT = cboDepartment.Text.Trim();
                                    objInfoHander.NEW_PROCESSSTS = Convert.ToInt32(Common.PROCESSSTS_RS);// Convert.ToInt32(Common.PROCESSSTS_WAPPROVING_RESENT);
                                    if (cboDepartment.Text.Trim() == "RM")
                                    {
                                        DataTable datMap = new DataTable();
                                        datMap = objCtrlAuto.MAP_AUTO(txtReceivedBranch.Text.Trim());
                                        if (datMap.Rows.Count == 0)
                                        {
                                            objInfoHander.NEW_BRANCH = txtReceivedBranch.Text.Trim();
                                            Log_Branch = txtReceivedBranch.Text.Trim();
                                        }
                                        else
                                        {
                                            objInfoHander.NEW_BRANCH = datMap.Rows[0]["RECEIVER_BRAN"].ToString();
                                            Log_Branch = datMap.Rows[0]["RECEIVER_BRAN"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        objInfoHander.NEW_BRANCH = txtReceivedBranch.Text.Trim();
                                        Log_Branch = txtReceivedBranch.Text.Trim();
                                    }
                                    //UPDATE DU LIEU VAO BANG SWIFT_PROCESS_HANDER-------------------------------
                                    if (objHander.UPDATE_SWIFT_PROCESS_HANDER(objInfoHander) == 1)
                                    {
                                        Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);
                                        Common.iOk = 1;
                                        #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                                        objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                                        objSWIFT_log.STATUS = 1;
                                        objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : Process resend  to branch:" + Log_Branch + " , Department :" + cboDepartment.Text;
                                        objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                                        //---------------Ghi log vao bang userlog----------------------------------------------
                                        DateTime dtLog = DateTime.Now;
                                        string strUser = BR.BRLib.Common.strUsername;
                                        string useride = BR.BRLib.Common.Userid;
                                        string strConten = "";
                                        int Log_level = 1;
                                        string strWorked = "Process";
                                        string strTable = "SWIFT_MSG_CONTENT";
                                        string strOld_value = "";
                                        string strNew_value = "";
                                        //-----------------------------------------
                                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                        //--------------------------------------------------
                                        objInfo.OK = "True";
                                        txtReceivedBranch.Text = strResen;
                                        txtReceivedBranch.Enabled = false;
                                        txtReceivingBank.Text = strResen;
                                        cboDepartment.Enabled = false;
                                        cmdOK.Enabled = false;
                                        #endregion----------------------------
                                        //UPDATE CHO DATHM 20081018---------------------------------                                            
                                        ObjCtrlPrint.Update_statement_type(strQUERY_ID);
                                        //------------------------------------------------------------
                                        //Cap nhat so lan resend
                                        //Date: 07/02/2010                                        
                                        objControl.Update_Resend_Num(objInfo);
                                        //------------------------------------------------------------
                                        this.Close();
                                    }
                                    else
                                    {
                                        Common.ShowError("Data has updated Error!", 2, MessageBoxButtons.OK);
                                        txtReceivedBranch.Text = strResen;
                                        txtReceivedBranch.Enabled = true;
                                        cboDepartment.Enabled = true;
                                        cmdOK.Enabled = true;
                                    }
                                }
                                Approve_Resend(pReciver, cboDepartment.Text.Trim());
                                //Refresh_logMessage();
                            }
                        }
                        #endregion Gui lai dien
                        #region Gui xu ly thu cong
                        else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                        {
                            //---lay du lieu ghi vao bang MSG_LOG--------------------
                            objSWIFT_log.LOG_DATE = DateTime.Now;
                            objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                            objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);  
                            //kiem tra xem ma phan he ma chi nhanh co khac cu khong------------------------------------------
                            string strGD;
                            if (strReceiving.Trim().Length == 5)
                            {  strGD = strReceiving.Trim().Substring(2, 3);  }
                            else { strGD = strReceiving.Trim(); }                            
                            #region Neu chan ma Brank la Luu tru(LT) hoac thu cong (TC)
                            if (txtReceivedBranch.Text.Trim().ToUpper() == "LT")
                            {
                                try
                                {
                                    // Lay du lieu de update------------------------------------------------------
                                    objInfoHander.MSG_ID = Convert.ToInt32(strMsg_id);
                                    objInfoHander.NEW_DEPARTMENT = cboDepartment.Text.Trim();
                                    //objInfoHander.NEW_PROCESSSTS = Convert.ToInt32(Common.PROCESSSTS_WAPPROVING);
                                    if (strMSG_SRC.Trim() == "1")/*Xu ly mot tay*/
                                    {
                                        objInfoHander.NEW_PROCESSSTS = Convert.ToInt32(Common.PROCESSSTS_MN);
                                    }
                                    else
                                    {
                                        objInfoHander.NEW_PROCESSSTS = Convert.ToInt32(Common.PROCESSSTS_WAPPROVING);
                                    }
                                    // ---------------------------------------------------------------------------
                                    if (strGD == txtReceivedBranch.Text.Trim() && strDepartment.Trim() == cboDepartment.Text.Trim())
                                    {
                                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                                        
                                        txtReceivedBranch.Focus();
                                        cmdApprove.BackColor = Color.White;
                                        cmdReject.BackColor = Color.White;
                                        cmdOK.BackColor = Color.White;
                                        cmdCancel.BackColor = Color.White;
                                        return;
                                    }
                                    if (strPROCESSSTS.Trim() == Common.PROCESSSTS_WAITING)
                                    {                                                                            
                                        if (cboDepartment.Text.Trim() == "RM")
                                        {
                                            DataTable datMap = new DataTable();
                                            datMap = objCtrlAuto.MAP_AUTO(txtReceivedBranch.Text.Trim());
                                            if (datMap.Rows.Count == 0)
                                            {   objInfoHander.NEW_BRANCH = txtReceivedBranch.Text.Trim();
                                                Log_Branch = txtReceivedBranch.Text.Trim(); }
                                            else
                                            {   objInfoHander.NEW_BRANCH = datMap.Rows[0]["RECEIVER_BRAN"].ToString();                                                
                                                Log_Branch = datMap.Rows[0]["RECEIVER_BRAN"].ToString(); }
                                        }
                                        else
                                        {   objInfoHander.NEW_BRANCH = txtReceivedBranch.Text.Trim();                                            
                                            Log_Branch = txtReceivedBranch.Text.Trim(); }                                        
                                        // ham update du lieu vao bang SWIFT_PROCESS_HANDER---------------
                                        if (objHander.UPDATE_SWIFT_PROCESS_HANDER(objInfoHander) == 1)
                                        {
                                            Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);                                                                                    
                                            Common.iOk = 1;
                                            #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                                            objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : process send to branch:" + Log_Branch + " , Department :" + cboDepartment.Text;
                                            objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                                            //---------------Ghi log vao bang userlog----------------------------------------------
                                            DateTime dtLog = DateTime.Now;
                                            string strUser = BR.BRLib.Common.strUsername;
                                            string useride = BR.BRLib.Common.Userid;
                                            string strConten = "";
                                            int Log_level = 1;
                                            string strWorked = "Process";
                                            string strTable = "SWIFT_MSG_CONTENT";
                                            string strOld_value = "";
                                            string strNew_value = "";
                                            //-----------------------------------------
                                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                            //--------------------------------------------------
                                            objInfo.OK = "True";
                                            txtReceivedBranch.Text = strResen;
                                            txtReceivedBranch.Enabled = false;
                                            txtReceivingBank.Text = strResen;
                                            cboDepartment.Enabled = false;
                                            cmdOK.Enabled = false;
                                            #endregion
                                            this.Close();
                                        }
                                        else
                                        {
                                            Common.ShowError("Data has updated Error!", 1, MessageBoxButtons.OK);                                             
                                            txtReceivedBranch.Text = strResen;
                                            txtReceivedBranch.Enabled = true;
                                            cboDepartment.Enabled = true;
                                            cmdOK.Enabled = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                                }
                            }
                            #endregion

                            #region Chon ma chi nhanh khong phai la TC Hoac LT
                            else
                            {
                                DataSet datt = new DataSet();
                                datt = objctrlBranch.GetBRANCH_MAP(txtReceivedBranch.Text.Trim());
                                if (datt == null || datt.Tables[0].Rows.Count == 0)
                                {
                                    Common.ShowError("Check branch received again! ", 3, MessageBoxButtons.OK);                                    
                                    txtReceivedBranch.Enabled = true; cboDepartment.Enabled = true; 
                                    cmdOK.Enabled = true;
                                    txtReceivedBranch.Focus();
                                    cmdApprove.BackColor = Color.White; cmdReject.BackColor = Color.White; cmdOK.BackColor = Color.White;
                                    cmdCancel.BackColor = Color.White;
                                }
                                else
                                {
                                    if (strPROCESSSTS.Trim() == Common.PROCESSSTS_WAITING)
                                    {
                                        objInfoHander.MSG_ID = Convert.ToInt32(strMsg_id);
                                        objInfoHander.NEW_DEPARTMENT = cboDepartment.Text.Trim();
                                        if (strMSG_SRC.Trim() == "1")/*Xu ly mot tay*/
                                        {
                                            objInfoHander.NEW_PROCESSSTS = Convert.ToInt32(Common.PROCESSSTS_MN);
                                        }
                                        else
                                        {
                                            objInfoHander.NEW_PROCESSSTS = Convert.ToInt32(Common.PROCESSSTS_WAPPROVING);
                                        }
                                        if (cboDepartment.Text.Trim() == "RM")
                                        {
                                            DataTable datMap = new DataTable();
                                            datMap = objCtrlAuto.MAP_AUTO(txtReceivedBranch.Text.Trim());
                                            if (datMap.Rows.Count == 0)
                                            {   objInfoHander.NEW_BRANCH = txtReceivedBranch.Text.Trim();                                                
                                                Log_Branch = txtReceivedBranch.Text.Trim(); }
                                            else
                                            {   objInfoHander.NEW_BRANCH = datMap.Rows[0]["RECEIVER_BRAN"].ToString();                                               
                                                Log_Branch = datMap.Rows[0]["RECEIVER_BRAN"].ToString(); }
                                        }
                                        else
                                        {   objInfoHander.NEW_BRANCH = txtReceivedBranch.Text.Trim();    
                                            Log_Branch = txtReceivedBranch.Text.Trim(); }
                                        // ham update du lieu vao bang SWIFT_PROCESS_HANDER---------------
                                        if (objHander.UPDATE_SWIFT_PROCESS_HANDER(objInfoHander) == 1)
                                        {
                                            Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);                                            
                                            Common.iOk = 1;
                                            #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                                            objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : process send to branch :" + Log_Branch + " , Department :" + cboDepartment.Text;
                                            objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                                            //---------------Ghi log vao bang userlog----------------------------------------------
                                            DateTime dtLog = DateTime.Now;
                                            string strUser = BR.BRLib.Common.strUsername;
                                            string useride = BR.BRLib.Common.Userid;
                                            string strConten = "";
                                            int Log_level = 1;
                                            string strWorked = "Process";
                                            string strTable = "SWIFT_MSG_CONTENT";
                                            string strOld_value = "";
                                            string strNew_value = "";
                                            //-----------------------------------------
                                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                            //--------------------------------------------------
                                            objInfo.OK = "True";
                                            txtReceivedBranch.Text = strResen;
                                            txtReceivedBranch.Enabled = false;
                                            txtReceivingBank.Text = strResen;
                                            cboDepartment.Enabled = false;
                                            cmdOK.Enabled = false;
                                            #endregion
                                            this.Close();
                                        }
                                        else
                                        {
                                            Common.ShowError("Data has updated Error!", 2, MessageBoxButtons.OK);                                            
                                            txtReceivedBranch.Text = strResen;
                                            txtReceivedBranch.Enabled = true;
                                            cboDepartment.Enabled = true;
                                            cmdOK.Enabled = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            if (strMSG_SRC.Trim() == "1")
                            {
                                Approve_manual(pNEWBRANCH, cboDepartment.Text,0);
                            }
                            //Refresh_logMessage();
                        }
                    }
                    #endregion 
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            iClose = false;
        }

        private void cmdApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Approve_manual(txtReceivedBranch.Text.Trim(), cboDepartment.Text.Trim(),1);
                Refresh_logMessage();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            iClose = false;
        }

        private void Approve_Resend(string pNBranch,string pNDepartment)
        { 
            try
            {
                iClose = true;
                cmdReject.Enabled = false;
                #region Cac gia tri-------------------------------------
                //---lay du lieu ghi vao bang MSG_LOG--------------------
                objSWIFT_log.LOG_DATE = DateTime.Now;
                objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                #endregion//-------------------------------------------------------
                if (cmdOK.Enabled == true)
                {
                    Common.ShowError("Invalid input data!", 3, MessageBoxButtons.OK);
                    return;
                }

                #region
                objInfo.MSG_ID = Convert.ToInt32(strMsg_id);
                if (Regex.IsMatch(pNBranch.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    objInfo.BRANCH_B = "00" + pNBranch.Trim();//txtReceivedBranch                   
                }
                else
                {
                    objInfo.BRANCH_B = pNBranch.Trim();
                }
                objInfo.DEPARTMENT = pNDepartment;
                objInfo.OFFICER_ID = Common.Userid;
                #endregion

                #region Resend dien -------------------------------------
                if (Common.strSTATUS_IN == "Resend")
                {
                    objInfo.STATUS = Common.STATUS_CONVERTED;
                    objInfo.AUTO = "N";
                    objInfo.PROCESSSTS = Common.PROCESSSTS_APPROVED;
                    objInfo.SWMSTS = strSWMSTS;
                    //-----------------------------------------------------------                    
                    if (strMSG_SRC.Trim() == "2")//goi ham cua chi hien
                    {
                        objInfo.PACKAGE = "GW_PK_SWIFT_Q_EXCEL_IN.SWEXCELPROCESS";
                    }
                    else if (strMSG_SRC.Trim() == "1" || strMSG_SRC.Trim() == "3")
                    {
                        objInfo.PACKAGE = "GW_PK_SWIFT_Q_CONVERTIN.Processhander";
                    }
                    //---------------------------------------------------------
                    //---------Xoa du lieu trong bang TF_SVR_INDEX hay RM_SVR_INDEX--------
                    if (strDepartment.Trim() == "RM")
                    {
                        objControl.TF_RM_SVR_INDEX(strQUERY_ID, "RM_SVR_INDEX");
                    }
                    else if (strDepartment.Trim() == "TF")
                    {
                        objControl.TF_RM_SVR_INDEX(strQUERY_ID, "TF_SVR_INDEX");
                    }
                    //----------------------------------------------------------------------
                    if (objControl.UpdateSWIFT_MSG_CONTENTStatusT(objInfo) == 1)
                    {
                        //Common.ShowError("Data has approved successfully!", 1, MessageBoxButtons.OK);
                        /*Khong xoa dong nay de lai de in bang ke*/
                        //objHander.DELETE_SWIFT_PROCESS_HANDER(Convert.ToInt32(strMsg_id));
                        Common.iOk = 1;
                        #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                        objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : Approve resend to branch:" + txtReceivedBranch.Text + " , Department :" + cboDepartment.Text;
                        objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                        //---------------Ghi log vao bang userlog----------------------------------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "";
                        int Log_level = 1;
                        string strWorked = "Resend";
                        string strTable = "SWIFT_MSG_CONTENT";
                        string strOld_value = "";
                        string strNew_value = "";
                        //-----------------------------------------
                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                        //-----Xoa trong ban ghi luu giu lai Teller ID------------------                        
                        objControl.Update_tellerID(strQUERY_ID, strTable_name);
                        //--------------------------------------------------------------
                        objInfo.Approve = "True";
                        cmdApprove.Enabled = false;
                        #endregion

                        this.Close();
                    }
                    else
                    {
                        if (strMSG_SRC.Trim() == "2")//goi ham cua chi hien
                        {
                            Common.ShowError("Resend message error!", 2, MessageBoxButtons.OK);
                            cmdApprove.Enabled = true;
                            cmdReject.Enabled = true;
                        }
                        else if (strMSG_SRC.Trim() == "1")
                        {
                            Common.ShowError("Module may be not correct please check again !", 3, MessageBoxButtons.OK);
                            cmdApprove.Enabled = true;
                            cmdReject.Enabled = true;
                        }
                    }
                }
                #endregion     
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*Theo yeu cau cua MSB*/
        private void Approve_manual(string pNBranch,string pNDepartment,int iDelete)
        { 
            try
            {                           
                if (cmdOK.Enabled == true)
                {
                    Common.ShowError("Invalid input data!", 3, MessageBoxButtons.OK); return;
                }

                #region                
                objInfo.MSG_ID = Convert.ToInt32(strMsg_id);
                if (Regex.IsMatch(pNBranch.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    objInfo.BRANCH_B = "00" + pNBranch;//txtReceivedBranch                   
                }
                else
                {
                    objInfo.BRANCH_B = pNBranch;                    
                }
                objInfo.DEPARTMENT = pNDepartment;
                objInfo.OFFICER_ID = Common.Userid;
                #endregion
                if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                { 
                    #region neu chon la luu tru
                    if (txtReceivedBranch.Text.Trim() == "LT")//thi phai cap nhat thanh trang thai STATUS='SENT'//ngay 2008.10.02
                    {
                        objInfo.STATUS = strSTATUS;
                        objInfo.AUTO = "N"; objInfo.PROCESSSTS = Common.PROCESSSTS_APPROVED;
                        if (strSWMSTS.Trim() == Common.SWMSTS_OLDKEY_FAILURE || strSWMSTS.Trim() == Common.SWMSTS_POSS_DUP)
                        { objInfo.SWMSTS = Common.SWMSTS_NORMAL; }
                        else
                        { objInfo.SWMSTS = strSWMSTS;  }

                        if (strMSG_SRC.Trim() == "2")//goi ham cua chi hien
                        {
                            objInfo.PACKAGE = "GW_PK_SWIFT_Q_EXCEL_IN.SWEXCELPROCESS";
                        }
                        else if (strMSG_SRC.Trim() == "1")
                        {
                            objInfo.PACKAGE = "GW_PK_SWIFT_Q_CONVERTIN.Processhander";
                        }
                        //---------------------------------------------------------
                        if (objControl.UpdateSWIFT_MSG_CONTENTStatusT(objInfo) == 1)
                        {
                            /*Khong xoa du lieu de in bang ke*/
                            if (iDelete == 1)
                            {
                                objHander.DELETE_SWIFT_PROCESS_HANDER(Convert.ToInt32(strMsg_id));
                            }
                            //neu approver thanh cong cap nhat vao bang MSG_CONTENT,ALL,ALL_HIS truong TRANS_SATE=sysdate Yeu cau ngay 2008.10.22
                            if (objControl.UPDATE_SYSDATE(strMsg_id.Trim(), strTable_name) == 1)
                            {
                                //---------------Ghi log vao bang userlog----------------------------------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "REF NUMBER:" + txtRMNo.Text;
                                int Log_level = 1;
                                string strWorked = "Update Sysdate Error";
                                string strTable = "SWIFT_MSG_CONTENT";
                                string strOld_value = "";
                                string strNew_value = "";
                                //-----------------------------------------
                                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            }     
                            //-------------------------------------------------------------------------------------------------------------------
                            Common.ShowError("Data has approved successfully!", 1, MessageBoxButtons.OK);                            
                            Common.iOk = 1;
                            #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                           
                            DateTime dtLog1 = DateTime.Now;
                            string strUser1 = BR.BRLib.Common.strUsername;
                            string useride1 = BR.BRLib.Common.Userid;
                            string strConten1 = "REF NUMBER:" + txtRMNo.Text;
                            int Log_level1 = 1;
                            string strWorked1 = "Approve";
                            string strTable1 = "SWIFT_MSG_CONTENT";
                            string strOld_value1 = "";
                            string strNew_value1 = "";
                            //-----------------------------------------
                            Ghiloguser(dtLog1, strUser1, strConten1, Log_level1, strWorked1, strTable1, strOld_value1, strNew_value1);
                            //--------------------------------------------------
                            objInfo.Approve = "True";                            
                            #endregion
                            iClose = true;
                            Common.iOk = 0;
                            Common.iCancel = 0;
                            objInfo.OK = "False";
                            objInfo.Reject = "False";
                            objInfo.Approve = "False";
                            this.Close();
                            iClose = false;
                        }                       
                    }
                    #endregion
                    #region neu khong chon xu ly la luu tru
                    else 
                    {
                        objInfo.STATUS = strSTATUS;
                        objInfo.AUTO = "N"; objInfo.PROCESSSTS = Common.PROCESSSTS_APPROVED;                   
                        if (strSWMSTS.Trim() == Common.SWMSTS_OLDKEY_FAILURE || strSWMSTS.Trim() == Common.SWMSTS_POSS_DUP)
                        { objInfo.SWMSTS = Common.SWMSTS_NORMAL; }
                        else
                        {  objInfo.SWMSTS = strSWMSTS;}
                        if (strMSG_SRC.Trim() == "2")//goi ham cua chi hien
                        {
                            objInfo.PACKAGE = "GW_PK_SWIFT_Q_EXCEL_IN.SWEXCELPROCESS";
                        }
                        else if (strMSG_SRC.Trim() == "1")
                        {
                            objInfo.PACKAGE = "GW_PK_SWIFT_Q_CONVERTIN.Processhander";
                        }
                        //---------------------------------------------------------
                        if (objControl.UpdateSWIFT_MSG_CONTENTStatusT(objInfo) == 1)
                        {
                            /*De lai de in bang ke*/
                            if (iDelete == 1)
                            {
                                objHander.DELETE_SWIFT_PROCESS_HANDER(Convert.ToInt32(strMsg_id));
                            }
                            //neu approver thanh cong cap nhat vao bang MSG_CONTENT,ALL,ALL_HIS truong TRANS_SATE=sysdate Yeu cau ngay 2008.10.22
                            if (objControl.UPDATE_SYSDATE(strMsg_id.Trim(), "SWIFT_MSG_CONTENT") != 1)
                            {
                                //---------------Ghi log vao bang userlog----------------------------------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "REF NUMBER:" + txtRMNo.Text;
                                int Log_level = 1;
                                string strWorked = "Update Sysdate Error";
                                string strTable = "SWIFT_MSG_CONTENT";
                                string strOld_value = "";
                                string strNew_value = "";
                                //-----------------------------------------
                                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            }                                                                                  
                            Common.iOk = 1;
                            #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------                           
                            DateTime dtLog2 = DateTime.Now;
                            string strUser2 = BR.BRLib.Common.strUsername;
                            string useride2 = BR.BRLib.Common.Userid;
                            string strConten2 = "REF NUMBER:" + txtRMNo.Text;
                            int Log_level2 = 1;
                            string strWorked2 = "Approve";
                            string strTable2 = "SWIFT_MSG_CONTENT";
                            string strOld_value2 = "";
                            string strNew_value2 = "";
                            //-----------------------------------------
                            Ghiloguser(dtLog2, strUser2, strConten2, Log_level2, strWorked2, strTable2, strOld_value2, strNew_value2);
                            //--------------------------------------------------
                            objInfo.Approve = "True";                            
                            #endregion
                            iClose = true;
                            Common.iOk = 0;
                            Common.iCancel = 0;
                            objInfo.OK = "False";
                            objInfo.Reject = "False";
                            objInfo.Approve = "False";
                            this.Close();
                            iClose = false;
                        }                       
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadData()
        {
            try
            {
                if (Regex.IsMatch(strReceiving.Trim(), @"^[0-9]*\z") == true)
                {
                    if (strReceiving.Trim().Length == 3)
                    { txtReceivedBranch.Text = strReceiving.Replace("\r", "").Replace("\r\n", ""); }
                    else
                    { txtReceivedBranch.Text = strReceiving.Replace("00", ""); }
                }
                else
                {
                    txtReceivedBranch.Text = strReceiving.Replace("\r", "").Replace("\r\n", "");
                }
                txtSendingBank.Text = strSender.Replace("\r", "").Replace("\r\n", "");//ma ngan hang gui
                txtReceivingBank.Text = strReceiving.Replace("\r", "").Replace("\r\n", "");//ma ngan hang nhan               
                if (strAmount.Trim() == "")
                { txtAmount.Text = ""; }
                else
                { txtAmount.Text = Common.FormatCurrency(strAmount.Trim(), Common.FORMAT_CURRENCY); }
                txtRMNo.Text = strF20.Replace("\r", "").Replace("\r\n", "");
                txtTransDate.Text = strTrans_date.Replace("\r", "").Replace("\r\n", "");
                txtDepartment.Text = strDepartment.Replace("\r", "").Replace("\r\n", "");
                cboDepartment.Text = strDepartment.Replace("\r", "").Replace("\r\n", "");
                txtcontent.Text = strContent;
                txtRM_NUMBER.Text = strRM_NUMBER;
                txtMsgType.Text = strMsg_type;                
                lbCCYCD.Text = strCCYCD.Replace("\r", "").Replace("\r\n", "");                
                //--------ngan hang gui----------------------------------------------------
                if (strSender.Trim() != "")
                {
                    DataSet datSend = new DataSet();
                    datSend = objcontrolBank.GetSWIFT_BANK_MAPA(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                    if (datSend.Tables[0].Rows.Count == 0)
                    { label3.Text = ""; }
                    else
                    {
                        label3.Text = datSend.Tables[0].Rows[0]["BANK_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                    }                    
                    if (strSender.Trim().Length == 5)
                    {
                        DataSet datRece = new DataSet();
                        datRece = objctrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));
                        if (datRece.Tables[0].Rows.Count == 0)
                        { label3.Text = ""; }
                        else
                        { label3.Text = datRece.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", ""); }
                    }
                    else if (strSender.Trim().Length == 3)
                    {
                        DataSet datRece = new DataSet();
                        datRece = objctrlBranch.GetBRANCH_MAP(strSender.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datRece.Tables[0].Rows.Count == 0)
                        { label3.Text = ""; }
                        else
                        { label3.Text = datRece.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", ""); }
                    }

                }
                if (strReceiving.Trim() != "")
                {
                    //--------ngan hang nhan---------------------------------------------------
                    DataSet datRece5 = new DataSet();
                    datRece5 = objcontrolBank.GetSWIFT_BANK_MAPB(strReceiving.Trim().Replace("\r", "").Replace("\r\n", ""));
                    if (datRece5.Tables[0].Rows.Count == 0)
                    { lbRecevi_name.Text = ""; }
                    else
                    {
                        lbRecevi_name.Text = datRece5.Tables[0].Rows[0]["BANK_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                    }                    
                    if (strReceiving.Trim().Length == 5)
                    {
                        DataSet datRece1 = new DataSet();
                        datRece1 = objctrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));
                        if (datRece1.Tables[0].Rows.Count == 0)
                        { lbRecevi_name.Text = ""; }
                        else
                        {
                            lbRecevi_name.Text = datRece1.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                    else if (strReceiving.Trim().Length == 3)
                    {
                        DataSet datRece1 = new DataSet();
                        datRece1 = objctrlBranch.GetBRANCH_MAP(strReceiving.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datRece1.Tables[0].Rows.Count == 0)
                        { lbRecevi_name.Text = ""; }
                        else
                        {
                            lbRecevi_name.Text = datRece1.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                    if (txtReceivedBranch.Text.Trim() == txtReceivingBank.Text.Trim())
                    { lblBranchName.Text = lbRecevi_name.Text; }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //lay ra thong tin lich su dien
        private void GetHistory(int QUERY_ID)
        {
            try
            {
                string strSWIFT_msg_log = "";
                //lay du lieu tu bang SWIFT_MSG_LOG
                DataSet datHistory = new DataSet();
                datHistory = objcontrolSWIFT_log.GetSWIFT_MSG_LOG_ManualInfo(QUERY_ID);
                if (datHistory.Tables[0].Rows.Count == 0)
                { return; }
                else
                {
                    int g = 0;
                    while (g < datHistory.Tables[0].Rows.Count)
                    {
                        string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                        string strQUERY_ID = datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString();
                        string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                        string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();
                        strSWIFT_msg_log = strSWIFT_msg_log + "\r\n" + ": " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;
                        //lay du lieu tu bang MSG_LOG
                        txthistory.Text = strSWIFT_msg_log;
                        g = g + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

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

            objcontroluser_msg_log.AddUSER_MSG_LOG1(objuser_msg_log);
        }


        private void txtReceivedBranch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F5)
                {
                    frmViewSwiftBankName frmview = new frmViewSwiftBankName();
                    //this.Hide();
                    frmview.ShowDialog();
                    if (frmview.objInfo.SIBS_BANK_CODE != null)
                    {                                   
                        txtReceivedBranch.Text = frmview.objInfo.SIBS_BANK_CODE.Replace("00", "");
                        strResen = frmview.objInfo.SIBS_BANK_CODE;                        
                        lblBranchName.Text = frmview.objInfo.BANK_NAME;                     
                    }                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSwiftMsgManualInfo_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (Common.iCancel == 0)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyCode == Keys.Return)
                {

                    SelectNextControl(this.ActiveControl, true, true, true, true);

                    if ((this.ActiveControl) is Button)
                    {
                    }
                    if ((this.ActiveControl) is TextBox)
                    {
                        (this.ActiveControl as TextBox).SelectAll();
                    }
                    if (cmdApprove.Focused)
                    {
                        cmdApprove.BackColor = Color.Green;
                    }
                    else
                    {
                        cmdApprove.BackColor = Color.White;
                    }
                    if (cmdReject.Focused)
                    {
                        cmdReject.BackColor = Color.Green;
                    }
                    else
                    {
                        cmdReject.BackColor = Color.White;
                    }
                    if (cmdCancel.Focused)
                    {
                        cmdCancel.BackColor = Color.Green;
                    }
                    else
                    {
                        cmdCancel.BackColor = Color.White;
                    }
                    if (cmdOK.Focused)
                    {
                        cmdOK.BackColor = Color.Green;
                    }
                    else
                    {
                        cmdOK.BackColor = Color.White;
                    }
                }                
                
            }
        }

        private void txtReceivedBranch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtReceivedBranch.Text.Trim().Length < 2)//neu do dai nho hon hai cho an nut ok
                {
                    cmdOK.Enabled = false;
                    txtReceivedBranch.Focus();
                    return;
                }
                if (txtReceivedBranch.Text.Trim().Length == 2)//thi bat dau moi tinh
                {
                    if (txtReceivedBranch.Text.Trim().ToUpper() == "LT")//tat ca duoc
                    {
                        cmdOK.Enabled = true;//tat ca duoc
                        lblBranchName.Text = "LUU TRU";
                        cboDepartment.Focus();
                        return;
                    }
                    if (txtReceivedBranch.Text.Trim().ToUpper() == "TC")//tat ca khong duoc
                    {
                        cmdOK.Enabled = false;
                        lblBranchName.Text = "THU CONG";
                        cboDepartment.Focus();
                        return;
                    }
                    if (txtReceivedBranch.Text.Trim().ToUpper() != "LT" || txtReceivedBranch.Text.Trim().ToUpper() != "TC")
                    {
                        if (cboDepartment.Text.Trim() == "Other")
                        { cmdOK.Enabled = false; txtReceivedBranch.Focus(); }
                        else
                        {
                            //YEU CAU CHINH SUA 2008.11.06
                            if (cboDepartment.Text.Trim() == "TR")
                            {
                                cmdOK.Enabled = false;
                                txtReceivedBranch.Focus();
                                return;
                            }
                            else
                            { cmdOK.Enabled = true; }
                        }
                    }
                }
                if (txtReceivedBranch.Text.Trim().Length > 2)
                {
                    if (cboDepartment.Text.Trim() == "Other")
                    {
                        if (Regex.IsMatch(txtReceivedBranch.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            //strBank = txtReceivedBranch.Text.Trim();
                            if (txtReceivedBranch.Text.Length <= 3)
                            {
                                strBank = txtReceivedBranch.Text.Trim();
                                //lay ra ten ngan hang tuong ung voi ma ngan hang
                                DataSet dataBranch = new DataSet();
                                dataBranch = objctrlBranch.GetBRANCH_MAP(txtReceivedBranch.Text.Trim());
                                if (dataBranch == null || dataBranch.Tables[0].Rows.Count == 0)
                                { lblBranchName.Text = ""; }
                                else
                                {
                                    lblBranchName.Text = "";
                                    lblBranchName.Text = dataBranch.Tables[0].Rows[0]["BRAN_NAME"].ToString();                                    
                                    txtReceivedBranch.Focus();
                                    return;
                                }
                            }
                            if (txtReceivedBranch.Text.Length > 3)
                            { txtReceivedBranch.Text = strBank; }
                        }
                        else
                        {
                            cmdOK.Enabled = false;
                            txtReceivedBranch.Focus();
                            return;
                        }
                        cmdOK.Enabled = false;
                        cboDepartment.Focus();
                        return;
                    }
                    else
                    {
                        if (Regex.IsMatch(txtReceivedBranch.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                        {                         
                            if (txtReceivedBranch.Text.Length <= 3)
                            {
                                strBank = txtReceivedBranch.Text.Trim();
                                //lay ra ten ngan hang tuong ung voi ma ngan hang
                                DataSet dataBranch = new DataSet();
                                dataBranch = objctrlBranch.GetBRANCH_MAP(txtReceivedBranch.Text.Trim());
                                if (dataBranch == null || dataBranch.Tables[0].Rows.Count == 0)
                                { lblBranchName.Text = ""; }
                                else
                                {
                                    lblBranchName.Text = "";
                                    lblBranchName.Text = dataBranch.Tables[0].Rows[0]["BRAN_NAME"].ToString();                                    
                                    if (cboDepartment.Text.Trim() == "TR")
                                    {
                                        cmdOK.Enabled = false;
                                        cboDepartment.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        cmdOK.Enabled = true;
                                        txtReceivedBranch.Focus();
                                        return;
                                    }
                                }
                            }
                            if (txtReceivedBranch.Text.Length > 3)
                            { txtReceivedBranch.Text = strBank; }
                        }
                        else
                        {
                            cmdOK.Enabled = false;
                            txtReceivedBranch.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {//Other
            try
            {
                if (txtReceivedBranch.Text.Trim().ToUpper() == "LT")//tat ca duoc
                { cmdOK.Enabled = true; return; }
                if (txtReceivedBranch.Text.Trim().ToUpper() == "TC")//tat ca khong duoc
                {
                    cmdOK.Enabled = false;
                    cboDepartment.Focus();
                    return;
                }
                if (txtReceivedBranch.Text.Trim().ToUpper() != "LT" || txtReceivedBranch.Text.Trim().ToUpper() != "TC")
                { 
                    if (cboDepartment.Text.Trim() == "Other")
                    {                        
                        cmdOK.Enabled = false;
                        cboDepartment.Focus();
                        return;
                    }
                    else
                    {
                        //YEU CAU CHINH SUA 2008.11.06
                        //if (cboDepartment.Text.Trim() == "TR")
                        //{ cmdOK.Enabled = false; cboDepartment.Focus(); return; }
                        //else
                        //{ cmdOK.Enabled = true; return; }
                        cmdOK.Enabled = true; return;
                    }
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSwiftMsgManualInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftMsgManualInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iClose == false)
            {
                e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all SWIFT manual process Information windows ?", Common.sCaption);
                if (e.Cancel == true)
                { iClose = false; bIsCloseForm = false; }
                else
                { bIsCloseForm = true; Refresh_logMessage(); }
            }
        }

        private void txtReceivedBranch_Leave(object sender, EventArgs e)
        {
            try
            {
                txtReceivedBranch.Text = txtReceivedBranch.Text.ToUpper();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSwiftMsgManualInfo_MouseDown(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdApprove_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                cmdCancel.BackColor = Color.White;
                cmdReject.BackColor = Color.White;
                cmdApprove.BackColor = Color.Green;
                cmdApprove.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdApprove_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void cmdApprove_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                cmdApprove.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        

        private void frmSwiftMsgManualInfo_Keypress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmSwiftMsgManualInfo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    if (cmdApprove.Focused)
                    { cmdApprove.BackColor = Color.Green; }
                    else
                    { cmdApprove.BackColor = Color.White; }
                    if (cmdReject.Focused)
                    { cmdReject.BackColor = Color.Green; }
                    else
                    { cmdReject.BackColor = Color.White; }
                    if (cmdCancel.Focused)
                    { cmdCancel.BackColor = Color.Green; }
                    else
                    { cmdCancel.BackColor = Color.White; }
                    if (cmdOK.Focused)
                    { cmdOK.BackColor = Color.Green; }
                    else
                    { cmdOK.BackColor = Color.White; }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (cmdApprove.Focused)
                    { cmdApprove.BackColor = Color.Green; }
                    else
                    { cmdApprove.BackColor = Color.White; }
                    if (cmdReject.Focused)
                    { cmdReject.BackColor = Color.Green; }
                    else
                    { cmdReject.BackColor = Color.White; }
                    if (cmdCancel.Focused)
                    { cmdCancel.BackColor = Color.Green; }
                    else
                    { cmdCancel.BackColor = Color.White; }
                    if (cmdOK.Focused)
                    { cmdOK.BackColor = Color.Green; }
                    else
                    { cmdOK.BackColor = Color.White; }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (cmdApprove.Focused)
                    { cmdApprove.BackColor = Color.Green; }
                    else
                    { cmdApprove.BackColor = Color.White; }
                    if (cmdReject.Focused)
                    { cmdReject.BackColor = Color.Green; }
                    else
                    { cmdReject.BackColor = Color.White; }
                    if (cmdCancel.Focused)
                    { cmdCancel.BackColor = Color.Green; }
                    else
                    { cmdCancel.BackColor = Color.White; }
                    if (cmdOK.Focused)
                    { cmdOK.BackColor = Color.Green; }
                    else
                    { cmdOK.BackColor = Color.White; }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdReject_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                cmdCancel.BackColor = Color.White;
                cmdApprove.BackColor = Color.White;
                cmdReject.BackColor = Color.Green;
                cmdReject.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdReject_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                cmdReject.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCancel_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                cmdReject.BackColor = Color.White;
                cmdApprove.BackColor = Color.White;
                cmdOK.BackColor = Color.White;
                cmdCancel.BackColor = Color.Green;
                cmdCancel.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCancel_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                cmdCancel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdOK_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                cmdCancel.BackColor = Color.White;
                cmdOK.BackColor = Color.Green;
                cmdOK.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdOK_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                cmdOK.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtReceivedBranch_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                cmdApprove.BackColor = Color.White;
                cmdReject.BackColor = Color.White;
                cmdOK.BackColor = Color.White;
                cmdCancel.BackColor = Color.White;
                txtReceivedBranch.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboDepartment_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                cmdApprove.BackColor = Color.White;
                cmdReject.BackColor = Color.White;
                cmdOK.BackColor = Color.White;
                cmdCancel.BackColor = Color.White;
                cboDepartment.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPrint frmPrint = new frmPrint();
            string Print;
            string strMap;
            if (strDepartment.Trim() == "RM" || strDepartment.Trim() == "TR")
            {
                Print = "SWIFT_PRINT";
                frmPrint.PrintType = Print;
                frmPrint.strMsgID = strMsg_id;
                frmPrint.loaidien = strMsg_type;
                frmPrint.chieudien = strMSG_DIRECTION;
                GetData_Print(strDepartment);
                if (dtMap == null || dtMap.Rows.Count == 0)
                { MessageBox.Show("Data not found!", Common.sCaption); }
                else
                {
                    frmPrint.HMdat = dtMap;
                    frmPrint.ShowDialog();
                }
            }
            else
            {
                Print = "SWIFT_PRINT_TF";
                frmPrint.PrintType = Print;
                frmPrint.strMsgID = strMsg_id;
                frmPrint.loaidien = strMsg_type;
                frmPrint.chieudien = strMSG_DIRECTION;
                GetData_Print(strDepartment);
                if (dtMap == null || dtMap.Rows.Count == 0)
                {
                    Common.ShowError("Data not found!", 3, MessageBoxButtons.OK);
                }
                else
                {
                    frmPrint.HMdat = dtMap;
                    frmPrint.ShowDialog();
                }
            }
            //////else
            //////{
            //////    Print = "SWIFT_08_R";
            //////    frmPrint.PrintType = Print;
            //////    Getcontent_Print();
            //////    if (dtMap == null || dtMap.Rows.Count == 0)
            //////    { MessageBox.Show("Data not found", Common.sCaption); }
            //////    else
            //////    {
            //////        strMap = dtMap.Rows[0]["CONTENT"].ToString();
            //////        int x = strMap.IndexOf("{4:");
            //////        int y = strMap.Substring(x + "{4:".Length).IndexOf("{4:");
            //////        if (y > 0)
            //////        {
            //////            x = x + y + "{4:".Length;
            //////        }
            //////        int z = strMap.Substring(x).IndexOf("}");
            //////        string strContent = strMap.Substring(x + "{4:".Length + "\r\n".Length, z - "{4:\r\n".Length - "\r\n}".Length);
            //////        //------------------
            //////        string Content1 = ""; string Content2 = ""; string Content3 = ""; string Content4 = "";
            //////        string Content5 = ""; string Content6 = ""; string Content7 = ""; string Content8 = "";
            //////        string Content9 = "";
            //////        if (strContent.Length <= 1300)
            //////        {
            //////            Content1 = strContent;
            //////        }
            //////        else
            //////        {
            //////            Content1 = strContent.Substring(0, 1300);
            //////            if (strContent.Length <= 2600)
            //////            { Content2 = strContent.Substring(1300); }
            //////            else
            //////            {
            //////                Content2 = strContent.Substring(1300, 1300);
            //////                if (strContent.Length <= 3900)
            //////                { Content3 = strContent.Substring(2600); }
            //////                else
            //////                {
            //////                    Content3 = strContent.Substring(2600, 1300);
            //////                    if (strContent.Length <= 5200)
            //////                    { Content4 = strContent.Substring(3900); }
            //////                    else
            //////                    {
            //////                        Content4 = strContent.Substring(3900, 1300);
            //////                        if (strContent.Length <= 6500)
            //////                        { Content5 = strContent.Substring(5200); }
            //////                        else
            //////                        {
            //////                            Content5 = strContent.Substring(5200, 1300);
            //////                            if (strContent.Length <= 7800)
            //////                            { Content6 = strContent.Substring(6500); }
            //////                            else
            //////                            {
            //////                                Content6 = strContent.Substring(6500, 1300);
            //////                                if (strContent.Length <= 9100)
            //////                                { Content7 = strContent.Substring(7800); }
            //////                                else
            //////                                {
            //////                                    Content7 = strContent.Substring(7800, 1300);
            //////                                    if (strContent.Length <= 10400)
            //////                                    { Content8 = strContent.Substring(9100); }
            //////                                    else
            //////                                    {
            //////                                        Content8 = strContent.Substring(9100, 1300);
            //////                                        if (strContent.Length <= 11700)
            //////                                        { Content9 = strContent.Substring(10400); }
            //////                                        else
            //////                                        { Content9 = strContent.Substring(10400, 1300); }
            //////                                    }
            //////                                }
            //////                            }
            //////                        }
            //////                    }
            //////                }
            //////            }
            //////        }

            //////        //------------------
            //////        DataTable datPrint = new DataTable();
            //////        //DataRow[] datRow;
            //////        DataRow datRow1;
            //////        for (int i = 0; i < dtMap.Columns.Count; i++)
            //////        {
            //////            DataColumn datColum = new DataColumn(dtMap.Columns[i].ColumnName, dtMap.Columns[i].DataType);
            //////            datPrint.Columns.Add(datColum);
            //////        }
            //////        int f = 0;
            //////        while (f < dtMap.Rows.Count)
            //////        {
            //////            //datRow = dtMap.Rows[0]; 
            //////            datRow1 = datPrint.NewRow();
            //////            for (int j = 0; j < dtMap.Columns.Count; j++)
            //////            {
            //////                if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT")
            //////                { datRow1[j] = strContent; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT1")
            //////                { datRow1[j] = Content1; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT2")
            //////                { datRow1[j] = Content2; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT3")
            //////                { datRow1[j] = Content3; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT4")
            //////                { datRow1[j] = Content4; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT5")
            //////                { datRow1[j] = Content5; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT6")
            //////                { datRow1[j] = Content6; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT7")
            //////                { datRow1[j] = Content7; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT8")
            //////                { datRow1[j] = Content8; }
            //////                else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT9")
            //////                { datRow1[j] = Content9; }
            //////                else
            //////                { datRow1[j] = dtMap.Rows[0][j]; }
            //////            }
            //////            datPrint.Rows.Add(datRow1);
            //////            f = f + 1;
            //////        }
            //////        frmPrint.HMdat = datPrint;
            //////    }
            //////}            
        }
        //purpose: Phuc vu in dien RM
        private void GetData_Print(string strDepartment)
        {
            try
            {
                DataTable datContent = new DataTable();
                string strDtl_name = "SWIFT_MSGDTL";
                string strTable_name = "SWIFT_MSG_CONTENT";
                datContent = objControlContent.swift_print_03(strQUERY_ID.Trim(), strTable_name,
                    strDtl_name, strDepartment);
                if (datContent == null || datContent.Rows.Count == 0)
                { dtMap = null; }
                else
                {                    dtMap = datContent;
                }
                if (datContent.Rows.Count == 0)
                {
                    DataTable datContent1 = new DataTable();
                    string strTable_name1 = "SWIFT_MSG_ALL";
                    string strDtl_name1 = "SWIFT_MSGDTL_ALL";
                    datContent1 = objControlContent.swift_print_03(strQUERY_ID.Trim(), strTable_name1, 
                        strDtl_name1,strDepartment);
                    if (datContent1 == null || datContent1.Rows.Count == 0)
                    { dtMap = null; }
                    else
                    { dtMap = datContent1; }
                    if (datContent1.Rows.Count == 0)
                    {
                        DataTable datContent2 = new DataTable();
                        string strTable_name2 = "SWIFT_MSG_ALL_HIS";
                        string strDtl_name2 = "SWIFT_MSGDTL_ALL_HIS";
                        datContent2 = objControlContent.swift_print_03(strQUERY_ID.Trim(), strTable_name2, 
                            strDtl_name2,strDepartment);
                        if (datContent2 == null || datContent2.Rows.Count == 0)
                        { dtMap = null; }
                        else
                        { dtMap = datContent2; }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //End DatHM
        //DatHM
        //purpose : Lay du lieu phuc vu cho viec in map field truong dien giai dien swift
        private void Getcontent_Print()
        {
            try
            {
                DataTable datContent = new DataTable();
                string strTable_name = "SWIFT_MSG_CONTENT";
                datContent = objControlContent.swift_print_map(strQUERY_ID.Trim(), strTable_name);
                if (datContent == null || datContent.Rows.Count == 0)
                { dtMap = null; }
                else
                { dtMap = datContent; strMapField = strTable_name; }
                if (datContent.Rows.Count == 0)
                {
                    DataTable datContent1 = new DataTable();
                    string strTable_name1 = "SWIFT_MSG_ALL";
                    datContent1 = objControlContent.swift_print_map(strQUERY_ID.Trim(), strTable_name1);
                    if (datContent1 == null || datContent1.Rows.Count == 0)
                    { dtMap = null; }
                    else
                    { dtMap = datContent1; strMapField = strTable_name1; }
                    if (datContent1.Rows.Count == 0)
                    {
                        DataTable datContent2 = new DataTable();
                        string strTable_name2 = "SWIFT_MSG_ALL_HIS";
                        datContent2 = objControlContent.swift_print_map(strQUERY_ID.Trim(), strTable_name2);
                        if (datContent2 == null || datContent2.Rows.Count == 0)
                        { dtMap = null; }
                        else
                        { dtMap = datContent2; strMapField = strTable_name2; }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }      
        //purpose: Phuc vu in dien RM
    }
}

