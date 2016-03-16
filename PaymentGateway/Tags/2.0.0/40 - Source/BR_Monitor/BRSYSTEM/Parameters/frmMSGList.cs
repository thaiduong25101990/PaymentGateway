using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRBusinessObject;

namespace BR.BRSYSTEM
{
    public partial class frmMSGList: frmBasedata
    {
        #region Ham va bien
        private bool isInsert = false;
        private DataSet datDs = new DataSet();
        private MSG_LISTInfo objInfo = new MSG_LISTInfo();
        private MSG_LISTController objControl = new MSG_LISTController();
        private int iID = 0;
        private clsCheckInput clsCheck = new clsCheckInput();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();        
        private string MsgName1 = "";
        private string MsgDescription1 = "";
        private string SIBSMsgLength1 = "";
        private string GWMsgLength1 = "";
        #endregion

        public frmMSGList()
        {
            InitializeComponent();
        }   

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isInsert = true;
            ClearText();
            LockTextbox(false);
            CommandStatus(false);
            txtMsgName.Focus();        
        }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSave.Enabled = !a;
            cmdCancel.Enabled = !a;
            grdList.Enabled = a;
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            isInsert = false;
            LockTextbox(false);            
            txtMsgName.ReadOnly = true;
            CommandStatus(false);
            txtMsgName.Focus();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    iID = Convert.ToInt32(grdList.CurrentRow.Cells[0].Value.ToString());
                    objControl.DeleteMSG_LIST(iID);
                    MessageBox.Show("Delete data Successfull", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //------------------------------------------------------------------
                    #region lay du lieu de ghi log
                    DateTime dtDateLogin = DateTime.Now;
                    string strContent = "SIBS message length standard";
                    int iLoglevel = 1;
                    string strWorked = "Delete";
                    string strTable = "MSG_LIST";
                    string strOld_value = MsgName1 + "/" + MsgDescription1 + "/" + SIBSMsgLength1 + "/" + GWMsgLength1;
                    string strNew_value = "";
                    #endregion
                    Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                    //------------------------------------------------------------------
                    LoadData();
                    CommandStatus(true);
                }
                else
                {                    
                    CommandStatus(true);
                    return;
                }
            }
            catch { 
            }
           
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    if (CheckLength())
                    {
                        objInfo = new MSG_LISTInfo();
                        objInfo.MSG_LIST_ID = iID;
                        objInfo.MSG_DEF_ID = clsCheck.ConvertVietnamese(txtMsgName.Text.Trim());
                        objInfo.MSG_DESCRIPTION = clsCheck.ConvertVietnamese(txtMsgDescription.Text.Trim());
                        if (String.IsNullOrEmpty(clsCheck.ConvertVietnamese(txtSIBSMsgLength.Text.Trim())))
                            objInfo.SIBS_MSG_LENGTH = 0;
                        else
                            objInfo.SIBS_MSG_LENGTH = Convert.ToInt32(clsCheck.ConvertVietnamese(txtSIBSMsgLength.Text.Trim()));
                        if (String.IsNullOrEmpty(clsCheck.ConvertVietnamese(txtGWMsgLength.Text.Trim())))
                            objInfo.GW_MSG_LENGTH = 0;
                        else
                            objInfo.GW_MSG_LENGTH = Convert.ToInt32(clsCheck.ConvertVietnamese(txtGWMsgLength.Text.Trim()));
                        if (isInsert)
                        {

                            objControl.AddMSG_LIST(objInfo);
                            MessageBox.Show("Insert data Successfull", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            isInsert = false;
                            #region lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "SIBS message length standard";
                            int iLoglevel = 1;
                            string strWorked = "Insert";
                            string strTable = "MSG_LIST";
                            string strOld_value = "";
                            string strNew_value = objInfo.MSG_DEF_ID + "/" + objInfo.MSG_DESCRIPTION + "/" + objInfo.SIBS_MSG_LENGTH + "/" + objInfo.GW_MSG_LENGTH;
                            //goi ham ghilog
                            Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                            #endregion
                        }
                        else
                        {
                            objControl.UpdateMSG_LIST(objInfo);
                            MessageBox.Show("Update data Successfull", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            #region lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "SIBS message length standard";
                            int iLoglevel = 1;
                            string strWorked = "Update";
                            string strTable = "MSG_LIST";
                            string strOld_value = MsgName1 + "/" + MsgDescription1 + "/" + SIBSMsgLength1 + "/" + GWMsgLength1;
                            string strNew_value = objInfo.MSG_DEF_ID + "/" + objInfo.MSG_DESCRIPTION + "/" + objInfo.SIBS_MSG_LENGTH + "/" + objInfo.GW_MSG_LENGTH;
                            //goi ham ghilog
                            Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                            #endregion
                        }
                        LoadData();
                        LockTextbox(true);
                        CommandStatus(true);
                    }
                    else
                    {
                        txtMsgDescription.SelectAll();
                        CommandStatus(false);
                    }
                }
                else
                { CommandStatus(true); return; }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption);
            } 
       }

        /*---------------------------------------------------------------
                * Method           : Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
                * Muc dich         : Ham ghi log User dang nhap vao he thong
                * Tham so          : cac gia tri tuong ung voi bang User_msg_log
                * Tra ve           : 
                * Ngay tao         : 10/07/2008
                * Nguoi tao        : QuyND
                * Ngay cap nhat    : 10/07/2008
                * Nguoi cap nhat   : QuyND(Nguyen duc quy)
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

            objcontroluser_msg_log.AddUSER_MSG_LOG1(objuser_msg_log);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearText()
        {
            txtMsgName.Text = "";
            txtMsgDescription.Text = "";
            txtGWMsgLength.Text = "";
            txtSIBSMsgLength.Text = "";
            txtMsgName.Focus();
        }

        private void LoadData()
        {
            try
            {
                datDs = new DataSet();
                datDs = objControl.GetMSG_List();
                grdList.DataSource = datDs.Tables[0];
                grdList.Columns[0].Visible = false;
                grdList.Columns["MSG_DEF_ID"].HeaderText = "Message definition ID";
                grdList.Columns["MSG_DEF_ID"].Width = 150;
                grdList.Columns["MSG_DESCRIPTION"].HeaderText = "Message description";
                grdList.Columns["MSG_DESCRIPTION"].Width = 230;
                grdList.Columns["SIBS_MSG_LENGTH"].HeaderText = "Host message length";
                grdList.Columns["SIBS_MSG_LENGTH"].Width = 200;
                grdList.Columns["GW_MSG_LENGTH"].HeaderText = "Bridge message length";
                grdList.Columns["GW_MSG_LENGTH"].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption);
            }
        }

        private void frmMSGList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            LoadData();
            LockTextbox(true);
            CommandStatus(true);            
            grdList.Select();
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iID = Convert.ToInt32(grdList.CurrentRow.Cells["MSG_LIST_ID"].Value.ToString());
                txtMsgName.Text = grdList.CurrentRow.Cells["MSG_DEF_ID"].Value.ToString();
                txtMsgDescription.Text = grdList.CurrentRow.Cells["MSG_DESCRIPTION"].Value.ToString();
                txtSIBSMsgLength.Text = grdList.CurrentRow.Cells["SIBS_MSG_LENGTH"].Value.ToString();
                txtGWMsgLength.Text = grdList.CurrentRow.Cells["GW_MSG_LENGTH"].Value.ToString();
                //--Update QUYND 20081120---------------------------------------------
                MsgName1 = grdList.CurrentRow.Cells["MSG_DEF_ID"].Value.ToString();
                MsgDescription1 = grdList.CurrentRow.Cells["MSG_DESCRIPTION"].Value.ToString();
                SIBSMsgLength1 = grdList.CurrentRow.Cells["SIBS_MSG_LENGTH"].Value.ToString();
                GWMsgLength1 = grdList.CurrentRow.Cells["GW_MSG_LENGTH"].Value.ToString();
                //--------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LockTextbox(Boolean a)
        {
            txtMsgName.ReadOnly = a;
            txtMsgDescription.ReadOnly = a;
            txtSIBSMsgLength.ReadOnly = a;
            txtGWMsgLength.ReadOnly = a;
        }

        private bool CheckNull()
        {
            bool result = true;
            if (String.IsNullOrEmpty(txtMsgName.Text))
                result = false;
            return result;
        }
        private bool CheckLength()
        {
            bool result = true;
            string msgName = txtMsgName.Text;
            if (String.IsNullOrEmpty(msgName))
            {               
                MessageBox.Show("You must input Message definition ID!");
                txtMsgName.Focus();
                result = false;
            }
            else if (msgName.Length > 20)
            {
                MessageBox.Show("Message Name maximium length is 20.");
                result = false;
            }

            if (txtMsgDescription.Text.Trim().Length > 70)
            {
                MessageBox.Show("Message Description maximium length is 70.");
                txtMsgDescription.Focus();
                result = false;
            }
          
            return result;
        }
        

      

 
        private void grdList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!isInsert)
            {
                iID = Convert.ToInt32(grdList.CurrentRow.Cells["MSG_LIST_ID"].Value.ToString());
                txtMsgName.Text = grdList.CurrentRow.Cells["MSG_DEF_ID"].Value.ToString();
                txtMsgDescription.Text = grdList.CurrentRow.Cells["MSG_DESCRIPTION"].Value.ToString();
                txtSIBSMsgLength.Text = grdList.CurrentRow.Cells["SIBS_MSG_LENGTH"].Value.ToString();
                txtGWMsgLength.Text = grdList.CurrentRow.Cells["GW_MSG_LENGTH"].Value.ToString();
                //--Update QUYND 20081120---------------------------------------------
                MsgName1 = grdList.CurrentRow.Cells["MSG_DEF_ID"].Value.ToString();
                MsgDescription1 = grdList.CurrentRow.Cells["MSG_DESCRIPTION"].Value.ToString();
                SIBSMsgLength1 = grdList.CurrentRow.Cells["SIBS_MSG_LENGTH"].Value.ToString();
                GWMsgLength1 = grdList.CurrentRow.Cells["GW_MSG_LENGTH"].Value.ToString();
                //--------------------------------------------------------------------               
            }
        }

        private void frmMSGList_KeyDown(object sender, KeyEventArgs e)
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
                    cmdSave.Focus();
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Common.ClearControl(this);
            CommandStatus(true);
            txtMsgName.ReadOnly = true;
            LockTextbox(true);
            grdList.Focus();
            grdList_CellContentClick(null, null);
        }

        private void txtSIBSMsgLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmdSave.Enabled == true)
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    MessageBox.Show("You must input number!",Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                return;
            }
        }

        private void txtGWMsgLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmdSave.Enabled == true)
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    MessageBox.Show("You must input number!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                return;
            }
        }
    }
}
