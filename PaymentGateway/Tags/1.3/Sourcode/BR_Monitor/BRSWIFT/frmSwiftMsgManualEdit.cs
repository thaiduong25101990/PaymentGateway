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

namespace BR.BRSWIFT
{
    public partial class frmSwiftMsgManualEdit : Form
    {
        #region khai bao ca lop trong Bussiness
        //private int isSupervisor;
        public SWIFT_MSG_CONTENTInfo objContent = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objControlContent = new SWIFT_MSG_CONTENTController();
        //--------------------------------------------------------------------------------
        private ALLCODEController objAllcode = new ALLCODEController();
        //--------------------------------------------------------------------------------
        SWIFT_MSG_LOGInfo objSWIFT_log = new SWIFT_MSG_LOGInfo();
        SWIFT_MSG_LOGController objcontrolSWIFT_log = new SWIFT_MSG_LOGController();
        //--------------------------------------------------------------------------------
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        //--------------------------------------------------------------------------------
        USERSInfo ObjUser = new USERSInfo();
        USERSController ObjCtrlUser = new USERSController();
        GROUPSInfo objInfoGroup = new GROUPSInfo();
        GROUPSController objControlGroup = new GROUPSController();
        //--------------------------------------------------------------------------------
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranch = new BRANCHController();
        //--------------------------------------------------------------------------------
        SWIFT_BANK_MAPInfo objWift_bank = new SWIFT_BANK_MAPInfo();
        SWIFT_BANK_MAPController objcontrolBank = new SWIFT_BANK_MAPController();
        //--------------------------------------------------------------------------------
        ALLCODEInfo objAll = new ALLCODEInfo();
        ALLCODEController ObjctrlAll = new ALLCODEController();
        #endregion

        #region khai bao cac bien       
        public string strSTATUS;
        public string strMSG_ID;
        public string strQUERY_ID;
        public string strAUTO;
        public string strBRANCH_A;
        public string strBRANCH_B;
        public string strAMOUNT;
        public string strFIELD20;          
        public string strTRANS_DATE;
        public string strDEPARTMENT;
        public string strCONTENT;
        private string strCONTENT_ORIGIN;
        public string strFOREIGN_BANK;
        public string strCCYCD;
        public string strMSG_DIRECTION;
        public string strMSG_TYPE;
        public string strTELLERID;
        public string strSWMSTS;
        public string strPSWMSTS;
        public string strstrTRANS_NO;
        public string strRM_NUMBER;
        public string strMSG_SRC;
        public string strPROCESSSTS;       
        public bool bIsCloseForm = false;
        private bool iClose = false;
        public string strLOCKSTS;
        public string strLOCK_TELLERID;
        private string strIbps_msg_log;
        public string strTable_name;

        #endregion

        private int iTeller = 0;

        public Boolean isFailed=true;

        public frmSwiftMsgManualEdit()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {         
                /*Threo yeu cau cua MSB thi se kiem tra nhung dien nao da duoc sua
                 * thi moi co nhu cau duyet 
                 * con khong thi chi can xu ly mot tay
                 */

                /*Lam sau*/

                iClose = true;

                /*Quynd comment de lam theo yeu cau cua MSB
                 * Neu dong close dien thi cap nhat trang thai dien la close
                 */
                #region Ghilog                
                /*Xu ly du lieu luon*/
                /*Ghi log vao bang du lieu user_msg_log*/
                objuser_msg_log.LOG_DATE = DateTime.Now;
                objuser_msg_log.OLD_VALUE = strPROCESSSTS;
                objuser_msg_log.NEW_VALUE = cboStatus.SelectedValue.ToString();
                objuser_msg_log.STATUS = 1;
                objuser_msg_log.TABLE_ACCESS = strTable_name;
                objuser_msg_log.USERID = Common.Userid;
                objuser_msg_log.WORKED = cboStatus.Text + "message";
                objcontroluser_msg_log.AddUSER_MSG_LOG(objuser_msg_log);
                #endregion

                if (objControlContent.UPDATE_CLOSE_MESSAGE(Common.PROCESSSTS_CLOSED, strQUERY_ID, strTable_name) == 1)
                {
                    MessageBox.Show("Closed message successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = 1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Closed message successfully";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                    iClose = true;
                    Common.iCancel = 0;
                    objContent.OK = "False";
                    objContent.Reject = "False";
                    objContent.Approve = "False";
                    Refresh_log();
                    this.Close();
                    iClose = false;
                }
                else
                {
                    MessageBox.Show("Closed message error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = -1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Closed message error";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                }


                #region /*Quynd comment
                /////////*lay du lieu ghi vao bang MSG_LOG******************/
                ////////objSWIFT_log.LOG_DATE = DateTime.Now;
                ////////objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                ////////objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);

                
                ////////-------------------------------------------------------  
                ////////Approve_manual frmAppro = new Approve_manual();
                ////////frmAppro.strMSG_ID = strMSG_ID.Trim();
                ////////frmAppro.strQUERY_ID = strQUERY_ID.Trim();
                ////////frmAppro.strRelease_ok = "Ok";
                //////////xu ly dien di bi trung
                ////////if (strAUTO.Trim() == "N" && strSTATUS.Trim() == Common.STATUS_CONVERTED && strSWMSTS.Trim() == Common.SWMSTS_POSS_DUP)
                ////////{
                ////////    frmAppro.strManue = "Same";//xu ly dien di bi trung
                ////////    frmAppro.strAUTO = strAUTO.Trim();
                ////////    frmAppro.strSTATUS = Common.STATUS_SENT;
                ////////    frmAppro.strSWMSTS = Common.SWMSTS_NORMAL;
                ////////    frmAppro.strPROCESS1 = Common.PROCESSSTS_CLOSED;
                ////////    frmAppro.strPRE_PROCESSSTS1 = "";                    
                ////////    //-----------------------------------------------------------------------------------  
                ////////    DataTable datCheck = new DataTable();
                ////////    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                ////////    objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                ////////    objSWIFT_log.DESCRIPTIONS = "Message status :possible duplicate";
                ////////    datCheck = objcontrolSWIFT_log.SELECT_SWIFT_MSG_LOG(objSWIFT_log);
                ////////    if (datCheck.Rows.Count == 0)
                ////////    {
                ////////        objSWIFT_log.LOG_DATE = DateTime.Now;
                ////////        objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                ////////        objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                ////////        objSWIFT_log.DESCRIPTIONS = "Message status :possible duplicate";
                ////////        objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                ////////    }
                ////////    //------------------------------------------------------------------------------------
                ////////    frmAppro.ShowDialog();                    
                ////////    if (Common.iCancel == 1)
                ////////    {
                ////////        this.Close();
                ////////    }                    
                ////////}
                //////////Xu ly dien di bi loi
                ////////else if (strSTATUS.Trim() == Common.STATUS_SENT && (strPROCESSSTS.Trim() == Common.PROCESSSTS_NACK || strPROCESSSTS.Trim() == Common.PROCESSSTS_ACKWAIT || strPROCESSSTS.Trim() == Common.PROCESSSTS_FAILED))
                ////////{
                ////////    if (cboStatus.SelectedValue.ToString() == Common.PROCESSSTS_FAILED)//khong phai duyet truc tiep
                ////////    {
                ////////        #region 4
                ////////        DataTable datSTA = new DataTable();
                ////////        datSTA = objControlContent.SWIFT_STATUS("SWIFT_MSG_CONTENT", strMSG_ID);
                ////////        if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_FAILED)//trang thai la FAILED roi khong xu ly duoc nua
                ////////        {
                ////////            Common.ShowError("Message have been processed!" + "\n\r" + "TELLER_ID :" + datSTA.Rows[0]["TELLER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                            
                ////////            return;
                ////////        }       
                ////////        objContent.PRE_PROCESSSTS = strPROCESSSTS;
                ////////        objContent.PROCESSSTS = cboStatus.SelectedValue.ToString();                        
                ////////        objContent.MSG_ID = Convert.ToInt32(strMSG_ID.Trim());
                ////////        objContent.TELLER_ID = Common.Userid;                        
                ////////        //goi ham Update du lieu--------------------------------
                ////////        if (objControlContent.UpdateSWIFT_MSG_CONTENTStatusSwiftMsgManualDup1(objContent) == 1)
                ////////        {
                ////////            Common.ShowError("Process successful!", 1, MessageBoxButtons.OK);                            
                ////////            objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : process to :" + cboStatus.Text;
                ////////            objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                ////////            //---------------Ghi log vao bang userlog----------------------------------------------
                ////////            DateTime dtLog = DateTime.Now;
                ////////            string strUser = BR.BRLib.Common.strUsername;
                ////////            string useride = BR.BRLib.Common.Userid;
                ////////            //string strConten = "";
                ////////            //int Log_level = 1;
                ////////            //string strWorked = "Process";
                ////////            //string strTable = "SWIFT_MSG_CONTENT";
                ////////            //string strOld_value = "";
                ////////            //string strNew_value = "";
                ////////            //-----------------------------------------
                ////////            Common.iCancel = 1;
                ////////            this.Close();
                ////////        }
                ////////        else
                ////////        {
                ////////            Common.ShowError("Process Error!", 2, MessageBoxButtons.OK);                           
                ////////            Common.iCancel = 0;
                ////////        }
                ////////        #endregion
                ////////    }
                ////////    else if (cboStatus.SelectedValue.ToString() == Common.PROCESSSTS_CLOSED)//can phai duyet truc tiep
                ////////    {
                ////////        #region 5
                ////////        frmAppro.strManue = "Plame";//xu ly dien di bi loi                        
                ////////        frmAppro.strPROCESS1 = cboStatus.SelectedValue.ToString();
                ////////        frmAppro.strPRE_PROCESSSTS1 = strPROCESSSTS;
                ////////        //-----------------------------------------------------------------------------------                       
                ////////        frmAppro.strAUTO = strAUTO.Trim();
                ////////        frmAppro.strSTATUS = strSTATUS;                       
                ////////        frmAppro.strSWMSTS = strSWMSTS;   
                ////////        frmAppro.iQUERY_ID = Convert.ToInt32(strQUERY_ID);
                ////////        frmAppro.strSTATUS = strSTATUS;
                ////////        //-------------------------------------------------------------------------------------
                ////////        frmAppro.ShowDialog();
                ////////        if (Common.iCancel == 1)
                ////////        {
                ////////            this.Close();
                ////////        }
                ////////        #endregion 
                ////////    }
                ////////}  
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            iClose = false;
        }
        //ham goi ghi log vao bang User_log
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
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            iClose = true;
            Common.iCancel = 0;
            objContent.OK = "False";
            objContent.Reject = "False";
            objContent.Approve = "False";
            Refresh_log();
            this.Close();
            iClose = false;
        }

        private void Refresh_log()
        {
            try
            {
                objControlContent.DELETE_SWIFT_PROCESS_HANDER(strMSG_ID, Common.Userid);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Getcontent()
        {
            try
            {
                DataTable datContent = new DataTable();
                //string strTable_name = "SWIFT_MSG_CONTENT";
                datContent = objControlContent.Search_Content(strMSG_ID.Trim());
                if (datContent == null || datContent.Rows.Count == 0)
                { strCONTENT = ""; }
                else
                {
                    strCONTENT = datContent.Rows[0]["CONTENT"].ToString();
                    strCONTENT_ORIGIN = datContent.Rows[0]["CONTENT_ORIGIN"].ToString();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Status()
        {
            try
            {
                DataTable _dt = new DataTable();
                if (strAUTO.Trim() == "N" && strSTATUS.Trim() == Common.STATUS_CONVERTED && strSWMSTS.Trim() == Common.SWMSTS_POSS_DUP)
                {
                    cmdRelease.Enabled = true;
                    cmdOK.Enabled = false;    
                    _dt.Rows.Clear();
                    _dt = ObjctrlAll.PROCESSTS_STATUS(" Where cdval = "+Common.PROCESSSTS_CLOSED+" ",out _dt);
                    DataRow _Rstatus1 = _dt.NewRow();
                    _Rstatus1[0] = ""; _Rstatus1[1] = " ";
                    _dt.Rows.InsertAt(_Rstatus1, 0);
                    cboStatus.DataSource = _dt;
                    cboStatus.DisplayMember = "CONTENT";
                    cboStatus.ValueMember = "CDVAL";
                    cboStatus.SelectedIndex = 0;                    
                }
                else if (strAUTO.Trim() == "N" && strSTATUS.Trim() == Common.STATUS_CONVERTED && strSWMSTS.Trim() == Common.SWMSTS_NORMAL)
                {
                    cmdRelease.Enabled = true;
                    cmdOK.Enabled = false;
                    _dt.Rows.Clear();
                    _dt = ObjctrlAll.PROCESSTS_STATUS(" Where cdval in (" + Common.PROCESSSTS_CLOSED + "," + Common.PROCESSSTS_FAILED + ") ", out _dt);
                    DataRow _Rstatus1 = _dt.NewRow();
                    _Rstatus1[0] = ""; _Rstatus1[1] = " ";
                    _dt.Rows.InsertAt(_Rstatus1, 0);
                    cboStatus.DataSource = _dt;
                    cboStatus.DisplayMember = "CONTENT";
                    cboStatus.ValueMember = "CDVAL";
                    cboStatus.SelectedIndex = 0;
                }
                //Xu ly dien di bi loi-----------------------------------------------------------------
                else if (strSTATUS.Trim() == Common.STATUS_SENT && (strPROCESSSTS.Trim() == Common.PROCESSSTS_OLD_ACKWAIT || strPROCESSSTS.Trim() == Common.PROCESSSTS_NACK || strPROCESSSTS.Trim() == Common.PROCESSSTS_FAILED || strPROCESSSTS.Trim() == Common.PROCESSSTS_REPAIR))
                {
                    if (strPROCESSSTS.Trim() == Common.PROCESSSTS_FAILED)
                    {
                        cmdRelease.Visible = false;
                        cmdOK.Enabled = true;
                        _dt.Rows.Clear();
                        _dt = ObjctrlAll.PROCESSTS_STATUS(" Where cdval = " + Common.PROCESSSTS_CLOSED + " ", out _dt);
                        cboStatus.DataSource = _dt;
                        cboStatus.DisplayMember = "CONTENT";
                        cboStatus.ValueMember = "CDVAL";
                        cboStatus.SelectedIndex = 0;

                    }
                    else if (strPROCESSSTS.Trim() == Common.PROCESSSTS_OLD_ACKWAIT)
                    {
                        cmdRelease.Enabled = true;
                        cmdOK.Enabled = false;
                        _dt.Rows.Clear();
                        _dt = ObjctrlAll.PROCESSTS_STATUS(" Where cdval in (" + Common.PROCESSSTS_CLOSED + "," + Common.PROCESSSTS_FAILED + ") ", out _dt);
                        cboStatus.DataSource = _dt;
                        cboStatus.DisplayMember = "CONTENT";
                        cboStatus.ValueMember = "CDVAL";
                        cboStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        //LAM THEO YEU CAU CUA MSB DI HET TAT CA NHUNG DIEN NAY
                        /*Them doan nay*/
                        cmdRelease.Enabled = true;
                        cmdOK.Enabled = false;
                        _dt.Rows.Clear();
                        _dt = ObjctrlAll.PROCESSTS_STATUS(" Where cdval in (" + Common.PROCESSSTS_CLOSED + "," + Common.PROCESSSTS_FAILED + ") ", out _dt);
                        DataRow _Rstatus1 = _dt.NewRow();
                        _Rstatus1[0] = ""; _Rstatus1[1] = " ";
                        _dt.Rows.InsertAt(_Rstatus1, 0);
                        cboStatus.DataSource = _dt;
                        cboStatus.DisplayMember = "CONTENT";
                        cboStatus.ValueMember = "CDVAL";
                        cboStatus.SelectedIndex = 0;

                        /*Comment doan nay*/

                        //cmdRelease.Visible = false;
                        //cmdOK.Enabled = true;
                        //_dt.Rows.Clear();
                        //_dt = ObjctrlAll.PROCESSTS_STATUS(" Where cdval in (" + Common.PROCESSSTS_CLOSED + "," + Common.PROCESSSTS_FAILED + ") ",out _dt);
                        //cboStatus.DataSource = _dt;
                        //cboStatus.DisplayMember = "CONTENT";
                        //cboStatus.ValueMember = "CDVAL";
                        //cboStatus.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void frmSwiftMsgManualEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                iClose = false; Common.iCancel = 0; cmdRelease.Enabled = false; cmdOK.Enabled = false;
                //xu ly dien di bi trung--------------------------------------------------------------
                Status();//kiem tra trang thai cua dien de load combobox   
                //---------------------------------------------------------------
                Getcontent();//chi lay dien trong bang content     
                cboStatus.Enabled = true;                         
                LoadData();//lay du lieu
                CheckIsSupervisor();               
                GetHistory(Convert.ToInt32(strQUERY_ID));
                cboStatus.Focus();
                if (cboStatus.Text.Trim() == "")
                { cmdOK.Enabled = false; }
                Check_Approver();

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Check_Approver()
        {
            try
            {
                DataTable _dtR = new DataTable();//Kiem tra xem co du lieu trong bang Swift_process hay khong
                _dtR = objControlContent.GET_SWIFT_PROCESS(strQUERY_ID);
                if (_dtR.Rows.Count == 0)/*Khong co du lieu ton tai trong */
                {
                    if (iTeller == 1)
                    {
                        cmdApprove.Visible = false;
                        cmdReject.Visible = false;
                        cmdRelease.Enabled = true;
                        cboStatus.Enabled = true;
                    }
                    else if (iTeller == 2)
                    {
                        this.cmdReject.Location = new Point(cmdOK.Location.X, cmdOK.Location.Y);
                        this.cmdApprove.Location = new Point(cmdRelease.Location.X, cmdRelease.Location.Y);
                        cmdOK.Enabled = false;
                        cmdRelease.Enabled = false;
                        cboStatus.Enabled = false;
                    }
                    else if (iTeller == 3)
                    {
                        DataTable _dt = new DataTable();
                        _dt = objControlContent.Load_process(strQUERY_ID, Common.Userid);
                        if (_dt.Rows.Count == 0)
                        {
                            cmdApprove.Visible = false;
                            cmdReject.Visible = false;
                            cmdRelease.Enabled = true;
                            cboStatus.Enabled = true;
                        }
                        else
                        {
                            this.cmdReject.Location = new Point(cmdOK.Location.X, cmdOK.Location.Y);
                            this.cmdApprove.Location = new Point(cmdRelease.Location.X, cmdRelease.Location.Y);
                            cmdOK.Enabled = false;
                            cmdRelease.Enabled = false;
                            cboStatus.Enabled = false;

                        }
                    }
                }
                else/*co du lieu ton tai trong */
                {
                    if (_dtR.Rows[0]["TELLER_ID"].ToString().Trim() == Common.Userid)/*Neu la cua user do xu ly khong duoc Approve*/
                    {
                        MessageBox.Show("You can not approve this message, that you create! ", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.cmdReject.Location = new Point(cmdOK.Location.X, cmdOK.Location.Y);
                        this.cmdApprove.Location = new Point(cmdRelease.Location.X, cmdRelease.Location.Y);
                        cmdOK.Enabled = false;
                        cmdRelease.Enabled = false;
                        cmdReject.Enabled = false;
                        cmdApprove.Enabled = false;
                        cboStatus.Enabled = false;
                    }
                    else
                    {
                        this.cmdReject.Location = new Point(cmdOK.Location.X, cmdOK.Location.Y);
                        this.cmdApprove.Location = new Point(cmdRelease.Location.X, cmdRelease.Location.Y);
                        cmdOK.Enabled = false;
                        cmdRelease.Enabled = false;
                        cboStatus.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void CheckIsSupervisor()
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
                if (IsSupervisor == 1)//Teller
                {
                    iTeller = 1;
                    string pProcess = strPROCESSSTS.Trim();
                    if (pProcess == Common.PROCESSSTS_WAITING || pProcess == Common.PROCESSSTS_NACK || pProcess == Common.PROCESSSTS_ACKWAIT || pProcess == Common.PROCESSSTS_FAILED)
                    {
                        cmdOK.Enabled = true;
                        cboStatus.Enabled = true;
                    }
                }
                else if (IsSupervisor == 2)//Super
                {
                    iTeller = 2;              
                }
            }
            else if (dsGroup.Tables[0].Rows.Count == 2)//Super and teller
            {
                iTeller = 3;
                string pProcess = strPROCESSSTS.Trim();
                if (pProcess == Common.PROCESSSTS_WAITING || pProcess == Common.PROCESSSTS_NACK || pProcess == Common.PROCESSSTS_ACKWAIT || pProcess == Common.PROCESSSTS_FAILED)
                {
                    cmdOK.Enabled = true;
                    cboStatus.Enabled = true;
                }
            }
        }

        private void track_change()
        {
            try
            {
                int count = 0;
                if (dtgOrgContent.Rows.Count > 0) { dtgOrgContent.Rows.Clear(); }
                if (dtgEditContent.Rows.Count > 0) { dtgEditContent.Rows.Clear(); }
                DataTable _dt = new DataTable();
                _dt = objControlContent.GET_SWIFT_MSG_EDIT(Convert.ToInt32(strQUERY_ID));
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (dtgOrgContent.Rows.Count == 0)
                        {
                            /*Luoi dien Old*/
                            dtgOrgContent.Rows.Add();
                            dtgOrgContent.Rows[0].Cells["OrgContent"].Value = _dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString();
                            dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                            if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString() == "ADD")
                            {
                                dtgOrgContent.Rows[0].Visible = false;
                            }
                            /*Luoi dien Edit*/
                            dtgEditContent.Rows.Add();
                            dtgEditContent.Rows[0].Cells["EditContent"].Value = _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString();
                            dtgEditContent.Columns["EditContent"].ReadOnly = true;
                            if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString().Trim() != _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString().Trim())
                            {
                                dtgEditContent.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            count = dtgOrgContent.Rows.Count;
                            int k = 0;
                            while (k < count)
                            {
                                if (k == count - 1)
                                {
                                    /*Luoi dien Old*/
                                    dtgOrgContent.Rows.Add();
                                    dtgOrgContent.Rows[count].Cells["OrgContent"].Value = _dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString();
                                    dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                                    if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString() == "ADD")
                                    {
                                        dtgOrgContent.Rows[count].Visible = false;
                                    }
                                    /*Luoi dien Edit*/
                                    dtgEditContent.Rows.Add();
                                    dtgEditContent.Rows[count].Cells["EditContent"].Value = _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString();
                                    dtgEditContent.Columns["EditContent"].ReadOnly = true;
                                    if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString().Trim() != _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString().Trim())
                                    {
                                        dtgEditContent.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                }
                                k = k + 1;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private String GetRow(string pRowsValue)
        {
            try
            {
                string vReturn = "";
                for (int i = 1; i < pRowsValue.Trim().Length; i++)
                {
                    if (pRowsValue.Trim().Substring(i, 1) == ":")
                    {
                        vReturn = pRowsValue.Trim().Substring(0, i);
                        break;
                    }
                }
                return vReturn;
            }
            catch (Exception ex)
            { 
                return "";
            }
        }
        private void LoadData()
        {
            try
            {               
                txtSendingBank.Text = strBRANCH_A;
                txtReceivingBank.Text = strBRANCH_B;
                if (strAMOUNT.Trim() == "")
                { txtAmount.Text = ""; }
                else
                { txtAmount.Text = Common.FormatCurrency(strAMOUNT.Trim(), Common.FORMAT_CURRENCY); }
                txtRMNo.Text = strFIELD20;
                txtTransDate.Text = strTRANS_DATE;
                txtDepartment.Text = strDEPARTMENT;
                track_change();
                //txtcontent.Text = strCONTENT_ORIGIN;
                //txtNewContent.Text = strCONTENT;
                lbCurrecy_name.Text = strCCYCD.Trim();
                txtRM_NUMBER.Text = strRM_NUMBER;
                txtMsgType.Text = strMSG_TYPE;
                //----------------------------------------------------
                //--------ngan hang gui----------------------------------------------------
                DataSet datSend = new DataSet();
                datSend = objcontrolBank.GetSWIFT_BANK_MAPA(strBRANCH_A.Trim().Replace("\r", "").Replace("\r\n", ""));
                if (datSend.Tables[0].Rows.Count == 0)
                { lbSend_Bankname.Text = ""; }
                else
                {
                    lbSend_Bankname.Text = datSend.Tables[0].Rows[0]["BANK_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                }
                //---------------Lay ten ngan hang tu bang khac---------------------//
                if (Regex.IsMatch(strBRANCH_A.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    if (strBRANCH_A.Length == 5)
                    {
                        DataSet datRece = new DataSet();
                        datRece = objctrlBranch.GetBRANCH_MAP(strBRANCH_A.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));
                        if (datRece.Tables[0].Rows.Count == 0)
                        { lbSend_Bankname.Text = ""; }
                        else
                        { lbSend_Bankname.Text = datRece.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", ""); }
                    }
                    else if (strBRANCH_A.Length == 3)
                    {
                        DataSet datRece = new DataSet();
                        datRece = objctrlBranch.GetBRANCH_MAP(strBRANCH_A.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datRece.Tables[0].Rows.Count == 0)
                        { lbSend_Bankname.Text = ""; }
                        else
                        {
                            lbSend_Bankname.Text = datRece.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                }
                //--------ngan hang nhan---------------------------------------------------
                DataSet datRece5 = new DataSet();
                datRece5 = objcontrolBank.GetSWIFT_BANK_MAPB(strBRANCH_B.Trim().Replace("\r", "").Replace("\r\n", ""));
                if (datRece5.Tables[0].Rows.Count == 0)
                { lbReceiving_name.Text = ""; }
                else
                {
                    lbReceiving_name.Text = datRece5.Tables[0].Rows[0]["BANK_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                }
                //---------------Lay ten ngan hang tu bang khac---------------------//
                if (Regex.IsMatch(strBRANCH_B.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    if (strBRANCH_B.Length == 5)
                    {
                        DataSet datRece1 = new DataSet();
                        datRece1 = objctrlBranch.GetBRANCH_MAP(strBRANCH_B.Trim().Replace("\r", "").Replace("\r\n", "").Substring(2, 3));
                        if (datRece1.Tables[0].Rows.Count == 0)
                        { lbReceiving_name.Text = ""; }
                        else
                        {
                            lbReceiving_name.Text = datRece1.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }                      
                    }
                    else if (strBRANCH_B.Length == 3)
                    {
                        DataSet datRece1 = new DataSet();
                        datRece1 = objctrlBranch.GetBRANCH_MAP(strBRANCH_B.Trim().Replace("\r", "").Replace("\r\n", ""));
                        if (datRece1.Tables[0].Rows.Count == 0)
                        {
                            lbReceiving_name.Text = "";
                        }
                        else
                        {
                            lbReceiving_name.Text = datRece1.Tables[0].Rows[0]["BRAN_NAME"].ToString().Replace("\r", "").Replace("\r\n", "");
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void CheckTellerID(int strQueryID)
        {
            DataSet ds = new DataSet();
            ds = objControlContent.GetTellerID(strQueryID);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                string TellerID = ds.Tables[0].Rows[0][0].ToString();
                if (TellerID != Common.Userid)
                {
                    Common.ShowError("Have no permission to approve!", 3, MessageBoxButtons.OK);                    
                }
            }
        }

        // Ham lay ra lich su cua dien
        private void GetHistory(int QUERY_ID)
        {
            try
            {
                //lay du lieu tu bang SWIFT_MSG_LOG
                DataSet datHistory = new DataSet();
                datHistory = objcontrolSWIFT_log.GetSWIFT_MSG_LOG_ManualInfo(QUERY_ID);
                if (datHistory.Tables[0].Rows.Count != 0)
                {
                    int g = 0;
                    while (g < datHistory.Tables[0].Rows.Count)
                    {
                        string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                        int strQUERY_ID = Convert.ToInt32(datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString());
                        string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                        string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();
                        string strAREAID = datHistory.Tables[0].Rows[g]["LOG_ID"].ToString();
                        strIbps_msg_log = strIbps_msg_log + "\r\n" + " + : " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS + "//" + strAREAID;
                        //lay du lieu tu bang MSG_LOG                        
                        g = g + 1;
                    }
                    txthistory.Text = strIbps_msg_log;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                /*Sua theo yeu cau cua MSB*/
                /*Them doan nay*/
                if (cboStatus.Text.Trim() == "")//dine nay di tiep
                {
                    cmdRelease.Enabled = true;//hien cac nut Release
                    cmdOK.Enabled = false;
                }
                else
                {
                    cmdRelease.Enabled = false;//an nut Release nay di
                    cmdOK.Enabled = true;
                }
                /*comment doan nay*/
                //#region xu ly dien di bi trung
                //if (strAUTO.Trim() == "N" && strSTATUS.Trim() == Common.STATUS_CONVERTED && strSWMSTS.Trim() == Common.SWMSTS_POSS_DUP)
                //{
                //    if (cboStatus.Text.Trim() == "")//dine nay di tiep
                //    {
                //        cmdRelease.Enabled = true;//hien cac nut Release
                //        cmdOK.Enabled = false;
                //    }
                //    else
                //    {
                //        cmdRelease.Enabled = false;//an nut Release nay di
                //        cmdOK.Enabled = true;
                //    }
                //}
                //#endregion
                //#region Xu ly dien di bi loi
                //else if (strSTATUS.Trim() == Common.STATUS_SENT && (strPROCESSSTS.Trim() == Common.PROCESSSTS_NACK || strPROCESSSTS.Trim() == Common.PROCESSSTS_ACKWAIT || strPROCESSSTS.Trim() == Common.PROCESSSTS_FAILED))
                //{
                //    cmdRelease.Visible = true;
                //}
                //#endregion
                LoadDescription();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void LoadDescription()
        {
            DataSet ds = new DataSet();
            if (cboStatus.Text.Trim() != "")
            {
                ds = objAllcode.GetALLCODE_Description("SWMSTS", "SWIFT", cboStatus.Text);
                if (ds.Tables[0].Rows.Count != 0)
                {                
                    lblStatusDescription.Text = ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        private void frmSwiftMsgManualEdit_KeyDown(object sender, KeyEventArgs e)
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

                }
            }
        }

        private void frmSwiftMsgManualEdit_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftMsgManualEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iClose == false)
            {
                if (Common.iCancel == 0)
                {
                    e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all SWIFT manual process Information windows ?", Common.sCaption);
                    if (e.Cancel == true)
                    {
                        iClose = false;
                        bIsCloseForm = false;
                    }
                    else
                    {
                        bIsCloseForm = true;
                        Refresh_log();
                    }
                }
            }
        }
        //chi co xu ly thu cong dien di bi trung
        private void cmdRelease_Click(object sender, EventArgs e)
        {
            try
            {
                #region /*Comment lai theo de lam theo yeu cau cua MSB*/
                //iClose = true;                
                //Approve_manual frmAppro = new Approve_manual();
                //frmAppro.strRelease_ok = "Release";
                //frmAppro.strManue = "Same";               
                //frmAppro.strSTATUS = strSTATUS;
                //frmAppro.strSWMSTS = Common.SWMSTS_NORMAL;          
                //frmAppro.strAUTO = strAUTO.Trim();
                //frmAppro.strQUERY_ID = strQUERY_ID.Trim();
                //frmAppro.strMSG_ID = strMSG_ID.Trim();
                //frmAppro.strPROCESS1 = strPROCESSSTS;
                //frmAppro.strPRE_PROCESSSTS1 = "";
                //#region /*Ghi log vao database*/
                //frmAppro.iQUERY_ID = Convert.ToInt32(strQUERY_ID);
                //frmAppro.strSTATUS = strSTATUS;
                //DataTable datCheck = new DataTable();
                //objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                //objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                //objSWIFT_log.DESCRIPTIONS = "Message status :possible duplicate";
                //datCheck = objcontrolSWIFT_log.SELECT_SWIFT_MSG_LOG(objSWIFT_log);
                //if (datCheck.Rows.Count == 0)
                //{
                //    objSWIFT_log.LOG_DATE = DateTime.Now;
                //    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                //    objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                //    objSWIFT_log.DESCRIPTIONS = "Message status :possible duplicate";
                //    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                //}
                //#endregion

                ////-------------------------------------------------------   
                //frmAppro.ShowDialog();
                //if (BR.BRLib.Common.iCancel == 1)
                //{
                //    this.Close();
                //}
                ////---------------------------------------------------
                //iClose = false;
                #endregion

                /*
                 * Sua theo yeeu cau cua MSB
                 * -Nhung dien bi trung,dien export file ma swift chua gui ve la ACK hay NACK dang la trang thai ACKWAIT
                 * -Nhung dien bi loi,nhung dien co trang thai la NACK
                 * Thi deu co the gui lai tiep
                 * Neu dien co trang thai PROCESSSTS la REPAIR deu phai xu ly hai tay
                 */
                /*Dien nay xu ly hai tay
                 * Dien nay cap nhat vao bang processha...
                 */
                /*Neu dien co nguon tu file text hay excel thi deu xu ly hai tay*/
                if (strMSG_SRC == "2" || strMSG_SRC == "3")
                {
                    Swift_doudble();/*Xu ly hai tay*/
                }
                else
                {
                    /*Xu ly dien Repair*/
                    if (strPROCESSSTS == Common.PROCESSSTS_REPAIR)
                    {
                        Swift_doudble();/*Xu ly hai tay*/
                    }
                    /*Xu ly khong can hay tay
                     * Dien nay lay dien tu outward hist day len out ward va cap nha trang thai = 0 o bang content
                     */
                    else
                    {
                        Swift_redend();/*Xu ly mot tay*/
                    }
                }
                Refresh_log();
                #region
                //iClose = true;
                //Approve_manual frmAppro = new Approve_manual();
                //frmAppro.strRelease_ok = "Release";
                //frmAppro.strManue = "Same";
                //frmAppro.strSTATUS = strSTATUS;
                //frmAppro.strSWMSTS = Common.SWMSTS_NORMAL;
                //frmAppro.strAUTO = strAUTO.Trim();
                //frmAppro.strQUERY_ID = strQUERY_ID.Trim();
                //frmAppro.strMSG_ID = strMSG_ID.Trim();
                //frmAppro.strPROCESS1 = strPROCESSSTS;
                //frmAppro.strPRE_PROCESSSTS1 = "";
                //#region /*Ghi log vao database*/
                //frmAppro.iQUERY_ID = Convert.ToInt32(strQUERY_ID);
                //frmAppro.strSTATUS = strSTATUS;
                //DataTable datCheck = new DataTable();
                //objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                //objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                //objSWIFT_log.DESCRIPTIONS = "Message status :possible duplicate";
                //datCheck = objcontrolSWIFT_log.SELECT_SWIFT_MSG_LOG(objSWIFT_log);
                //if (datCheck.Rows.Count == 0)
                //{
                //    objSWIFT_log.LOG_DATE = DateTime.Now;
                //    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                //    objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                //    objSWIFT_log.DESCRIPTIONS = "Message status :possible duplicate";
                //    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                //}
                //#endregion

                ////-------------------------------------------------------   
                //frmAppro.ShowDialog();
                //if (BR.BRLib.Common.iCancel == 1)
                //{
                //    this.Close();
                //}
                ////---------------------------------------------------
                //iClose = false;
                #endregion

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Swift_redend()
        {
            try
            {
                if (objControlContent.Resend_message_swift_tc(strQUERY_ID, strTable_name) == 1)
                {
                    MessageBox.Show("Send swift successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = 1;
                    objSWIFT_log.DESCRIPTIONS = "Send swift successfully";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                    Refresh_log();
                    iClose = true;
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Send swift error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = -1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Send swift error";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }

        }

        private void Swift_doudble()
        {
            try
            {
                if (objControlContent.Update_swift_process(strQUERY_ID, Common.Userid) == 1)
                {
                    if (strMSG_SRC == "2" || strMSG_SRC == "3")/*Dien import file*/
                    {
                        MessageBox.Show("Send imported message to approve successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Send repaired message to approve successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = 1;
                    if (strMSG_SRC == "2" || strMSG_SRC == "3")/*Dien import file*/
                    {
                        objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Send imported message to approve successfully";
                    }
                    else
                    {
                        objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Send repaired message to approve successfully";
                    }
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                    Refresh_log();
                    iClose = true;
                    Common.iCancel = 0;
                    objContent.OK = "False";
                    objContent.Reject = "False";
                    objContent.Approve = "False";
                    Refresh_log();
                    this.Close();
                    iClose = false;
                }
                else
                {
                    if (strMSG_SRC == "2" || strMSG_SRC == "3")/*Dien import file*/
                    {
                        MessageBox.Show("Send imported message to approve error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Send repaired message to approve error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = -1;
                    if (strMSG_SRC == "2" || strMSG_SRC == "3")/*Dien import file*/
                    {
                        objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Send imported message to approve error";
                    }
                    else
                    {
                        objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Send repaired message to approve error";
                    }
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSwiftMsgManualEdit_MouseDown(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (objControlContent.Resend_message_swift_tc(strQUERY_ID, strTable_name) == 1)
                {
                    MessageBox.Show("Repaired successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = 1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Repaired successfully";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                    iClose = true;
                    Common.iCancel = 0;
                    objContent.OK = "False";
                    objContent.Reject = "False";
                    objContent.Approve = "False";
                    Refresh_log();
                    this.Close();
                    iClose = false;
                }
                else
                {
                    MessageBox.Show("Repaired error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = -1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Repaired error";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (objControlContent.Delete_swift_process(strQUERY_ID) == 1)
                {
                    MessageBox.Show("Reject repaired successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = 1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Reject repaired successfull";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                    iClose = true;
                    Common.iCancel = 0;
                    objContent.OK = "False";
                    objContent.Reject = "False";
                    objContent.Approve = "False";
                    Refresh_log();
                    this.Close();
                    iClose = false;
                }
                else
                {
                    MessageBox.Show("Reject repaired error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID);
                    objSWIFT_log.STATUS = -1;
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Reject repaired error";
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }

}
