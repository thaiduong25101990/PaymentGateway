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

namespace BR.BRSYSTEM
{
    public partial class frmResetPass : frmBasedata
    {
        #region cac ham ba bien
        private clsLog objLog = new clsLog();
        USERSInfo objuser = new USERSInfo();
        USERSController objcontroluser = new USERSController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        UserEncrypt Encrypt = new UserEncrypt();
        private static string  strOld_value = "";
        clsCheckInput clsCheck = new clsCheckInput();
        #endregion

        public frmResetPass()
        {
            InitializeComponent();
        }

          /*---------------------------------------------------------------
        * Ten ham          : Verify();
        * Muc dich         : hàm kiểm tra dữ liệu nhập trên form
        * Tra ve           : 
        * Ngay tao         : 08/04/2008
        * Nguoi tao        : HueMT
        * Ngay cap nhat    :
        * Nguoi cap nhat   :
        *--------------------------------------------------------------*/
        private bool Verify()
        {            
            if (txtUserID.Text.Trim() == "")
            {
                MessageBox.Show("UserID is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserID.Focus();                
                return false;
            }

            if(txtNewPass.Text=="")
            {
                MessageBox.Show("New password is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();                
                return false;
            }

            if(txtConfirmPass.Text=="")
            {
                MessageBox.Show("Confirm password is empty", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();                
                return false;
            }
            
            if (txtNewPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Confirm password must be similar with new password!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //txtconfimpass.Text = "";                
                txtConfirmPass.Focus();
                txtConfirmPass.SelectAll();                
                return false; 
            }  
      
            return true;
        }


          /*---------------------------------------------------------------
        * Ten ham          : CheckValidate();
        * Muc dich         : hàm kiểm tra dữ liệu nhập trên DB
        * Tra ve           : 
        * Ngay tao         : 08/04/2008
        * Nguoi tao        : HueMT
        * Ngay cap nhat    :
        * Nguoi cap nhat   :
        *--------------------------------------------------------------*/
        private bool CheckValidate()
        {
            if (!GetUser(txtUserID.Text.Trim()))
            {
                MessageBox.Show("User is not existent!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserID.Focus();
                txtUserID.SelectAll();                
                return false;
            }  
            return true ;
        }

        /*---------------------------------------------------------------
         * Ten ham          : GetUser();
         * Muc dich         : Lay thong tin User
         * Tra ve           : 
         * Ngay tao         : 08/04/2008
         * Nguoi tao        : HueMT
         * Ngay cap nhat    :
         * Nguoi cap nhat   :
         *--------------------------------------------------------------*/
        private bool GetUser(string strUserID)
        {
            try
            {
                DataSet datUser = new DataSet();
                objuser.USERID = strUserID;
                datUser = objcontroluser.GetUSERSUPDATEPASS(objuser.USERID);
                if (datUser.Tables[0].Rows.Count == 0)
                { datUser.Dispose(); return false; }
                objuser.ID = Convert.ToInt32(datUser.Tables[0].Rows[0]["ID"]);
                objuser.USERID = datUser.Tables[0].Rows[0]["UserID"].ToString();
                objuser.USERNAME = datUser.Tables[0].Rows[0]["UserName"].ToString();
                objuser.PASSWORD = datUser.Tables[0].Rows[0]["PASSWORD"].ToString();
                objuser.STATUS = Convert.ToInt16(datUser.Tables[0].Rows[0]["Status"].ToString());
                objuser.MOBILE = datUser.Tables[0].Rows[0]["MOBILE"].ToString();
                objuser.BRANCH = datUser.Tables[0].Rows[0]["BRANCH"].ToString();
                objuser.DESCRIPTION = datUser.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                objuser.EMAIL = datUser.Tables[0].Rows[0]["EMAIL"].ToString();
                objuser.PASSTIME = Convert.ToDateTime(datUser.Tables[0].Rows[0]["PASSTIME"].ToString());
                objuser.LASTDATE = Convert.ToDateTime(datUser.Tables[0].Rows[0]["LASTDATE"].ToString());
                strOld_value = objuser.PASSWORD;
                datUser.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }

       
        private bool UpdateUser()
        {
            try
            {
                objuser.PASSWORD = Encrypt.Encrypt(clsCheck.ConvertVietnamese(txtNewPass.Text), Encrypt.sKeyUser);
                objuser.STATUS = Convert.ToInt16(Common.GW_USERS_STATUS_PENDING);
                objuser.PASSTIME = DateTime.Now;
                if (objcontroluser.UpdateUSERS(objuser) == -1)
                {
                    MessageBox.Show("Update failed"); return false;
                }
                MessageBox.Show("You have just changed password for UserID " + clsCheck.ConvertVietnamese(txtUserID.Text.Trim()) + " successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }

        private bool ResetPass()
        {
            try
            {
                if (!Verify())
                    return false;
                if (!CheckValidate())
                    return false;
                if (!UpdateUser())
                {
                    return false;
                }                
                DateTime dtLog = DateTime.Now;
                string strUser = txtUserID.Text.Trim();
                string strConten = Encrypt.Encrypt(txtNewPass.Text, Encrypt.sKeyUser);
                int Log_level = 1;
                string strWorked = "Reset Password";
                string strTable = "Users";
                string strNew_value = Encrypt.Encrypt(txtNewPass.Text, Encrypt.sKeyUser);
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                    strWorked, strTable, strOld_value, strNew_value);
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }
                

        /*---------------------------------------------------------------
        * Method           : enterKey(object sender, KeyPressEventArgs e)
        * Muc dich         : Bắt phím Enter để đăng nhập
        * Tham so          :  
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        //enterKey(...): Bắt phím Enter để đăng nhập
        private void enterKey(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }           
            if (e.KeyChar == (char)13)
            {
                if (txtUserID.Focused)
                {
                    txtNewPass.Focus();
                    txtNewPass.SelectAll();
                }
                else if (txtNewPass.Focused)
                {
                    txtConfirmPass.Focus();
                    txtConfirmPass.SelectAll();
                }
                else if (txtConfirmPass.Focused)
                {
                    cmdok_Click(null, null);
                }
            }
        }
        private void cmdok_Click(object sender, EventArgs e)
        {
            try
            {
                if (ResetPass())
                {
                    this.DialogResult = DialogResult.OK;
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }           
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmResetPass_Load(object sender, EventArgs e)
        {            
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");

                txtUserID.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtUserID_Leave(object sender, EventArgs e)
        {
            txtUserID.Text = clsCheck.ConvertVietnamese(txtUserID.Text.Trim());
            txtUserName.Text = "";
            
            if(txtUserID.Text!="")
            {
                if (GetUser(txtUserID.Text ))
                {
                    txtUserName.Text  = objuser.USERNAME;
                }                
            }            
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmResetPass_KeyDown(object sender, KeyEventArgs e)
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
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmResetPass_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }        
}
