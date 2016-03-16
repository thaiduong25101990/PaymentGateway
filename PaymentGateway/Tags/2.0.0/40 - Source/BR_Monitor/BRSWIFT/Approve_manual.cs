using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRSYSTEM;
using BR.BRBusinessObject;

namespace BR.BRSWIFT
{
    public partial class Approve_manual : frmBasedata
    {
        BR.BRLib.UserEncrypt Encrypt = new BR.BRLib.UserEncrypt();
        //-------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranch = new BRANCHController();
        //---------------------------------------------------------------------------------
        SWIFT_BANK_MAPInfo objWift_bank = new SWIFT_BANK_MAPInfo();
        SWIFT_BANK_MAPController objcontrolBank = new SWIFT_BANK_MAPController();
        GROUPSInfo objInfoGroup = new GROUPSInfo();
        GROUPSController objControlGroup = new GROUPSController();
        //----------------------------------------------------------------------------------   
        public string strSTATUS;
        public string strAUTO;
        public string strQUERY_ID;
        public string strSWMSTS;
        public string strMSG_ID;
        private string OFFICER_ID;
        private string strPSWMSTS = "";
        bool ctrtext = false;
        private int iRole = 0;
        private string Process;
        //----------------------------------------------------------------------------------
        //bien de xac dinh duoc xem nguoi GDV chon nut Release hay OK
        public string strRelease_ok;
        public string strManue;
        public string strPROCESS1;
        public string strPRE_PROCESSSTS1;
        public string strPRE_SWMSTS1;
        private int CLOD;
        // cac gia trin de ghi log-----------------------------------------------------------
        public int iQUERY_ID;        
        //-----------------------------------------------------------------------------------
        public Approve_manual()
        {
            InitializeComponent();
        }

        private void Approve_manual_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                CLOD = 0;
                Process = "";
                cmdAdd.Enabled = false;
                cmdDelete.Enabled = false;
                cmdEdit.Enabled = false;
                cmdClose.Enabled = false;
                cmdSave.Enabled = false;                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void cmdApprove_Click(object sender, EventArgs e)
        {
            try
            {
                CLOD = 1;
                if (Common.Userid == txtUserID.Text.Trim())
                {
                    Common.ShowError("Users have no right approver!", 3, MessageBoxButtons.OK);                    
                    return;
                }
                else
                {
                    Process = "A";
                    Check_OFFICER();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Approve_Opp()
        {
            try
            {
                //------------------------------------------------------------------------------------------
                objContent.TELLER_ID = Common.Userid;
                objContent.OFFICER_ID = OFFICER_ID;
                objContent.MSG_ID = Convert.ToInt32(strMSG_ID.Trim());
                objContent.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                //------------------------------------------------------------------------------------------
                //chi co trang thai dien di bi trung
                if (strRelease_ok.Trim() == "Release")//chon nut Release,tuc la dien duoc phep di tiep
                {
                    if (strManue.Trim() == "Same")//xu ly dien di bi trung
                    {
                        DataTable datSTA = new DataTable();                        
                        datSTA = objControlContent.SWIFT_STATUS("SWIFT_MSG_CONTENT", strMSG_ID);
                        if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_CLOSED)//da duoc approve roi (chuyen thanh CLOSE roi)
                        {
                            Common.ShowError("Message have been approved to close!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                             
                            return;
                        }
                        else//neu khong chuyen thanh CLOSE thi kiem tra tiep
                        {
                            if (datSTA.Rows[0]["SWMSTS"].ToString().Trim() == Common.SWMSTS_NORMAL)
                            {
                                Common.ShowError("Message have been approved to close!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                                 
                                return;
                            }
                        }
                        objContent.SWMSTS = strSWMSTS;
                        objContent.STATUS = strSTATUS;
                        objContent.AUTO = strAUTO.Trim();                        
                        objContent.PROCESSSTS = strPROCESS1;
                        objContent.PRE_PROCESSSTS = strPRE_PROCESSSTS1;
                       
                        if (objControlContent.UpdateSWIFT_MSG_CONTENT_T_L(objContent) == 1)
                        {
                            objControlContent.DELETE_PROCESS_HANDER("," + strMSG_ID + ",");
                            Common.ShowError("Data has approved successfully!", 1, MessageBoxButtons.OK);                             
                            objSWIFT_log.LOG_DATE = DateTime.Now;
                            objSWIFT_log.QUERY_ID = iQUERY_ID;
                            objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                            objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : process to : Release";
                            objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                            #region ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                            objSWIFT_log.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                            objSWIFT_log.DESCRIPTIONS = "User ID :" + txtUserID.Text.Trim() + " : Approve to: Release";
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
                            #endregion--------------------------------------------------
                            BR.BRLib.Common.iCancel = 1;
                            this.Close();
                        }
                        else
                        {
                            Common.ShowError("Data has approved Error!", 3, MessageBoxButtons.OK);                             
                            Common.iCancel = 0;
                        }
                    }                    
                }
                //co ca trang thai di bi trung va bi loi
                else if (strRelease_ok.Trim() == "Ok")//chon nut OK(co ca xu ly thu cong dien bi trung & bi loi)
                {
                    if (strManue.Trim() == "Same")//xu ly dien di bi trung
                    {
                        DataTable datSTA = new DataTable();
                        datSTA = objControlContent.SWIFT_STATUS("SWIFT_MSG_CONTENT", strMSG_ID);
                        if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_CLOSED)//da duoc approve roi (chuyen thanh CLOSE roi)
                        {
                            Common.ShowError("Message have been approved to close!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                             
                            return;
                        }
                        else//neu khong chuyen thanh CLOSE thi kiem tra tiep
                        {
                            if (datSTA.Rows[0]["SWMSTS"].ToString().Trim() == Common.SWMSTS_NORMAL)
                            {
                                Common.ShowError("Message have been approve to release!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                                 
                                return;
                            }
                        }           
                        //cap nhat cac trang thai nhu the nay
                        objContent.SWMSTS = Common.STATUS_SENT;
                        objContent.STATUS = Common.STATUS_ERROR;
                        objContent.PROCESSSTS = Common.PROCESSSTS_CLOSED;                       
                        objContent.OFFICER_ID = txtUserID.Text.Trim();
                        objContent.ERR_CODE = 2;
                        if (objControlContent.UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(objContent) == 1)
                        {
                            objControlContent.DELETE_PROCESS_HANDER("," + strMSG_ID + ",");
                            Common.ShowError("Data has approved successfully!", 1, MessageBoxButtons.OK);                             
                            //#region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                            objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : process to CLOSED";
                            objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                            objSWIFT_log.DESCRIPTIONS = "User ID :" + txtUserID.Text.Trim() + " : Approve to CLOSED";
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
                            Common.iCancel = 1;
                            this.Close();
                        }
                        else
                        {
                            Common.ShowError("Data has approved Error!", 3, MessageBoxButtons.OK);                             
                            Common.iCancel = 0;
                        }
                    }
                    else if (strManue.Trim() == "Plame")//xu ly dien di bi loi
                    {
                        DataTable datSTA = new DataTable();
                        //QUYND UPDATE 20081122---------------------------
                        datSTA = objControlContent.SWIFT_STATUS("SWIFT_MSG_CONTENT", strMSG_ID);
                        if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_CLOSED)//da duoc approve roi (chuyen thanh CLOSE roi)
                        {
                            Common.ShowError("Message have been approved to close!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                             
                            return;
                        }
                        else if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_FAILED)
                        {
                            Common.ShowError("Message have been process to failed!" + "\n\r" + "TELLER_ID :" + datSTA.Rows[0]["TELLER_ID"].ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                             
                            return;
                        }
                        objContent.SWMSTS = strSWMSTS;
                        objContent.STATUS = strSTATUS;
                        objContent.AUTO = strAUTO.Trim();
                        objContent.PROCESSSTS = strPROCESS1;
                        objContent.PRE_PROCESSSTS = strPRE_PROCESSSTS1;                      

                        if (objControlContent.UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(objContent,"SWIFT_MSG_CONTENT") == 1 )
                        {
                            objControlContent.DELETE_PROCESS_HANDER("," + strMSG_ID + ",");
                            Common.ShowError("Data has approved successfully!", 1, MessageBoxButtons.OK);                             
                            objSWIFT_log.LOG_DATE = DateTime.Now;
                            objSWIFT_log.QUERY_ID = iQUERY_ID;
                            objSWIFT_log.STATUS = Convert.ToInt32(strSTATUS);
                            objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : process to : CLOSED";
                            objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                            //#region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                            objSWIFT_log.DESCRIPTIONS = "User ID :" + txtUserID.Text + " : Approve to : CLOSED";
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
                            Common.iCancel = 1;
                            this.Close();
                        }
                        else
                        {
                            Common.ShowError("Data has approved Error!", 3, MessageBoxButtons.OK);                             
                            Common.iCancel = 0;
                        }
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
        private bool checkInput()
        {

            if (txtUserID.Text == "")
            {
                Common.ShowError("Username isEmpty!", 3, MessageBoxButtons.OK);                
                return ctrtext = false;
            }
            if (txtPassword.Text == "")
            {
                Common.ShowError("Password isEmpty!", 3, MessageBoxButtons.OK);                
                return ctrtext = false;
            }
            if (txtUserID.Text.Length > 30 || txtPassword.Text.Length > 30)
            {
                Common.ShowError("User or password not allow lengther than 30 characters!", 3, MessageBoxButtons.OK);                
                return ctrtext = false;
            }
            return ctrtext = true;
        }

        private void Check_OFFICER()
        {
            try
            {
                checkInput();
                if (ctrtext)
                {
                    string strPasswordmh = Encrypt.Encrypt(txtPassword.Text, Encrypt.sKeyUser);
                    DataSet datuser = new DataSet();
                    datuser = ObjCtrlUser.GET_USER_PASS(txtUserID.Text.Trim(), strPasswordmh);
                    if (datuser.Tables[0].Rows.Count != 0)//Users da co trong he thong
                    {
                        OFFICER_ID = datuser.Tables[0].Rows[0]["USERID"].ToString();
                        Check_Teller(OFFICER_ID.Trim());
                    }
                    else
                    {
                        Common.ShowError("UserID or Password is not correct!", 3, MessageBoxButtons.OK);                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Check_Teller(string strOFFICER_ID)
        {
            try
            {
                DataSet dsGroup = new DataSet();
                dsGroup = objControlGroup.GetGroup_IsSupervisor(strOFFICER_ID, Common.gGWTYPE);

                if (dsGroup.Tables[0].Rows.Count == 1)
                {
                    int IsSupervisor = Convert.ToInt32(dsGroup.Tables[0].Rows[0][0].ToString());
                    if (IsSupervisor == 1)
                    {
                        Common.ShowError("Users have no right approver!", 2, MessageBoxButtons.OK);                        
                    }
                    else if (IsSupervisor == 2)
                    {                        
                        if (Process == "A") { Approve_Opp(); }
                        else{ Reject_Opp(); }
                    }
                }
                else if (dsGroup.Tables[0].Rows.Count == 2)
                {                    
                    if (Process == "A") { Approve_Opp(); }
                    else { Reject_Opp(); }
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
                Process = "R";
                Common.iCancel = 0;
                this.Close();                            
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Reject_Opp()
        {
            try
            {
                if (iRole == 1)//khong duoc quyen Approve
                {
                    Common.ShowError("Users have no right approver!", 3, MessageBoxButtons.OK);                   
                }
                else
                {
                    if (OFFICER_ID.Trim() == Common.Userid)
                    {
                        Common.ShowError("Users have no right approver!", 3, MessageBoxButtons.OK);                       
                    }
                    else
                    {                        
                        objContent.MSG_ID = Convert.ToInt32(strMSG_ID);                        
                        objContent.SWMSTS = strPSWMSTS.Trim();
                        objContent.PSWMSTS = strSWMSTS.Trim();
                        objContent.OFFICER_ID = "";
                        objContent.TELLER_ID = "";

                        if (objControlContent.UpdateSWIFT_MSG_Reject(objContent) == 1)
                        {
                            Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);                            
                            this.Close();
                        }
                        else
                        {
                            Common.ShowError("Data has updated Error!", 2, MessageBoxButtons.OK);                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Approve_manual_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (CLOD == 1)
                {
                }
                else
                {
                    Common.iCancel = 0;
                }
                CLOD = 0;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }     
        }

        private void Approve_manual_KeyDown(object sender, KeyEventArgs e)
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
}
