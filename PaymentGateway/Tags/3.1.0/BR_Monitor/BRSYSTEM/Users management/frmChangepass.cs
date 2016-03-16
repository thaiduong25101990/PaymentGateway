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
    public partial class frmChangepass : frmBasedata
    {
        public static string password;
        public static string userid;

        private clsLog objLog = new clsLog();
        private USERSInfo objuser = new USERSInfo();
        private USERSController objcontroluser = new USERSController();
        private USER_PASSInfo objuserpass = new USER_PASSInfo();
        private USER_PASSController objcontroluserpass = new USER_PASSController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private UserEncrypt Encrypt = new UserEncrypt();

        public frmChangepass()
        {
            InitializeComponent();
        }       
        
        /*---------------------------------------------------------------
        * Muc dich         : goi ham Updatepassword() va ham Updatelogpassword()
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 26/54/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdok_Click(object sender, EventArgs e)
        {
            if (chekInput())
            {
                try
                {
                    if (Updatepassword())
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                    txtoldpass.Text = "";
                    txtnewpass.Text = "";
                    txtconfimpass.Text = "";
                    
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
            }           
        }
       
        /*---------------------------------------------------------------
        * Muc dich         : Lay thong tin cua user do ra va update lai thong tin ve password vao database
        * Tra ve           : 
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 26/54/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private bool Updatepassword()
        {
            try
            {
                string strUserID = Common.Userid;
                //=================================================
                string strPre_pass = Encrypt.Encrypt(txtnewpass.Text, Encrypt.sKeyUser);
                DataSet datCheck = new DataSet();
                datCheck = objcontroluserpass.CheckUSERS_PASS(strUserID, strPre_pass);// phai ma hoa
                if (datCheck == null || datCheck.Tables[0].Rows.Count == 0)//neu khong co gia tri trung
                {

                    int iID = Common.iID;
                    string strPassword = strPre_pass;
                    if (objcontroluser.GetUpdate_chengePass(iID, strPre_pass, DateTime.Now, DateTime.Now) == 1)//update lai thong tin trong bang user
                    {
                        Common.strPasswordUser = txtnewpass.Text;
                        MessageBox.Show("Your password has been changed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information );
                    }
                    else
                    {
                        MessageBox.Show("Update has failed!",Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Error );
                        return false; 
                    }                   
                    objuserpass.USER_ID = Common.Userid;
                    objuserpass.PRE_PASS = Encrypt.Encrypt(txtnewpass.Text, Encrypt.sKeyUser);
                    objuserpass.PASSTIME = DateTime.Now;
                    objcontroluserpass.AddUSER_PASS(objuserpass);                    
                    DateTime dtLog = DateTime.Now;
                    string strUser = strUserID;
                    string strConten = Encrypt.Encrypt(txtnewpass.Text, Encrypt.sKeyUser) + Encrypt.Encrypt(txtoldpass.Text, Encrypt.sKeyUser);
                    int Log_level = 1;
                    string strWorked = "Update";
                    string strTable = "USers";
                    string strOld_value = Encrypt.Encrypt(txtoldpass.Text, Encrypt.sKeyUser);
                    string strNew_value = Encrypt.Encrypt(txtnewpass.Text, Encrypt.sKeyUser);
                    ////-----------------------------------------
                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                        strWorked, strTable, strOld_value, strNew_value);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); return false;
            }
            return true;
        }
        /*---------------------------------------------------------------
        * Muc dich         : Ghi lai thong tin password cu khi thay doi
        * Tra ve           : 
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 26/54/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void SaveOldpassword(string iUserid ,string strPre_pass)
        {
            objuserpass.USER_ID = iUserid;
            objuserpass.PRE_PASS = strPre_pass;
            objuserpass.PASSTIME = DateTime.Today;
            objcontroluserpass.AddUSER_PASS(objuserpass);//dang loi sua lai

        }
        /*---------------------------------------------------------------
        * Ten ham          :chekInput();
        * Muc dich         : hàm kiểm tra dữ liệu nhập vào có phù hợp không
        * Tra ve           : 
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 26/54/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private bool chekInput()
        {
            bool result = true;

            int iPassLength = 0;
            if (!objcontroluser.CHECK_PASS_LENGTH(txtnewpass.Text.Trim(), out iPassLength))
            {
                MessageBox.Show("The password is not allow lengther than " + iPassLength + " character!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtnewpass.Focus();
                return result = false;
            }
            //Check ky tu co ky tu 1-9, a-z, A-Z
            int iType = 0;
            if (!objcontroluser.CHECK_PASS_STRING(txtnewpass.Text.Trim(), out iType))
            {
                if (iType == 0)
                    MessageBox.Show("Password input must be have character in  0-9, a-z, A-Z!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (iType == 1)
                    MessageBox.Show("Password input must be have character in 0-9!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (iType == 2)
                    MessageBox.Show("Password input must be have character in a-z, A-Z!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }



            if (txtoldpass.Text  == "")
            {
                MessageBox.Show("Old password is empty!",Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtoldpass.Focus();                
                return result = false;
            }
            else
            {
                if (txtoldpass.Text == Common.strPasswordUser)
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("Old password is not correct!", Common.sCaption,  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtoldpass.Focus();
                    txtoldpass.SelectAll();
                    return result = false;
                }
            }
            if (txtnewpass.Text == "")
            {
                MessageBox.Show("New password is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtnewpass.Focus();
                return result = false; 
            }
            if (txtconfimpass.Text == "")
            {
                MessageBox.Show(" Confirm password is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtconfimpass.Focus();
                return result = false;
            }
            if (txtoldpass.Text == txtnewpass.Text)
            {
                MessageBox.Show("New password is equal to old password!", Common.sCaption,  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtnewpass.Focus();
                txtnewpass.SelectAll();
                //txtnewpass.Text = "";
                return result = false; 
            }
            if (txtnewpass.Text != txtconfimpass.Text)
            {
                MessageBox.Show("Confirm password is not equal to new password!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //txtconfimpass.Text = "";                
                txtconfimpass.Focus();
                txtconfimpass.SelectAll();                
                return result = false; 
            }     
            //Mat khau moi phai khac n mat khau truoc do. n = PassTime trong bang sysvar
            DataSet dsSysTemp = new DataSet();
            SYSVARController objSysvarControl = new SYSVARController();
            dsSysTemp = objSysvarControl.GetSYSVAR_NAME("Passtime", "SYSTEM");
            
            int isysPasstime = Convert.ToInt16(dsSysTemp.Tables[0].Rows[0]["Value"].ToString());
            DataSet datUser_Pass = new DataSet();
            USER_PASSController UserCtrol = new USER_PASSController();
            datUser_Pass = UserCtrol.GetUSERS_PASS_NUMCHANGEPASS(Common.Userid, isysPasstime, Encrypt.Encrypt(txtnewpass.Text.Trim(), Encrypt.sKeyUser));
            
            if (datUser_Pass.Tables[0].Rows.Count == 1)
            {
                MessageBox.Show("New password is equal to " + Convert.ToString(isysPasstime) + " latest password(s)!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtnewpass.Focus();
                txtnewpass.SelectAll();
                txtconfimpass.Text  = "";
                //txtnewpass.Focus();
                return result = false; 
            }

            return result;
        }
        /*---------------------------------------------------------------
        * Muc dich         : hàm lay thong tin cua user dang nhap vao he thong
        * Tra ve           : 
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 26/54/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void frmChangepass_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");                                               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        private void frmChangepass_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
        #region  //enterKey(...): Bắt phím Enter để đăng nhập
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //ESC
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }
            if (e.KeyChar == (char)13)
            {
                if (txtoldpass.Focused)
                {
                    txtnewpass.Focus();
                    txtnewpass.SelectAll();
                }
                else if (txtnewpass.Focused)
                {
                    txtconfimpass.Focus();
                    txtconfimpass.SelectAll();
                }
                else if (txtconfimpass.Focused)
                {                    
                    cmdok_Click(null, null);
                }
            }
        }
        #endregion

        private void frmChangepass_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmChangepass_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
   
    }
}
