using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BIDVWEB.Comm;
using BIDVWEB.Business;

namespace BIDVWEB.BIDV_UC.UserRight
{
    public partial class WfrmChangePass1 : System.Web.UI.Page
    {
        private USERS_BO objUsers_BO = new USERS_BO();
        private USER_PASSInfo objUserPass = new USER_PASSInfo();
        private USER_PASS_BO objUserPass_BO = new USER_PASS_BO();
        private clsForm objForm = new clsForm();
        private string strError = "";
        private SYSPARA_BO objSysPara_BO = new SYSPARA_BO();
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            //Response.Redirect("~/Default.aspx");             
            Response.Redirect("~/WfrmLogOut.aspx"); 
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            string username = "";
            username = (string)Session["username"];
            int iInt1;
            int iInt2;
            
            try
            {   
                //Kiem tra du lieu truoc khi cap nhat
                if (!CheckData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                iInt1 = objUsers_BO.UPDATE_PASS1(username, txtPassword.Text,
                    DateTime.Now,DateTime.Now,1,1);
                iInt2 = objUserPass_BO.AddUSER_PASS(objUserPass);
                if (iInt1>=1 && iInt2>=1)
                {   
                    strError = "Mật khẩu đã được thay đổi";
                    txtOldPassword.Text = "";
                    txtPassword.Text = "";
                    txtRetype.Text = "";                    
                    Response.Redirect("~//Common/Main.aspx");
                }
                else
                {   
                    txtOldPassword.Text = "";
                    txtPassword.Text = "";
                    txtRetype.Text = "";
                    strError = "Lỗi khi đổi mật khẩu";
                }
                objForm.MessboxWeb(this.Page, strError);
            }
            catch (Exception ex)
            {
                strError =ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        ////////////////////////////////////////////////////////////////
        // Muc dich:    Ham kiem tra du lieu truoc khi nhap
        // Ngay tao:    07/08/2008
        // Dau vao:     NA
        // Dau ra:      True: Thanh cong, False: Loi        
        ////////////////////////////////////////////////////////////////
        private bool CheckData()
        {
            bool result = true;
            string username = "";

            //Lay username tu session
            username = (string)Session["username"];
            //Kiem tra nhap mat khau cu
            if (string.IsNullOrEmpty(txtOldPassword.Text) )
            {
                strError = "Nhập mật khẩu cũ";
                return false;
            }
            //Kiem tra nhap mat khau cu moi
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                strError = "Nhập mật khẩu mới";
                return false;
            }
            //Check do dai password
            int iPassLength = 0;
            if (!objUsers_BO.CHECK_PASS_LENGTH(txtPassword.Text.Trim(), out iPassLength))
            {
                strError = "Nhập mật khẩu có độ dài >= " + iPassLength;
                return false;
            }
            //Check ky tu co ky tu 1-9, a-z, A-Z
            int iType = 0;
            if (!objUsers_BO.CHECK_PASS_STRING(txtPassword.Text.Trim(),out iType))
            {
                if (iType == 0)
                    strError = "Nhập mật khẩu phải có ký tự số và chữ (0-9, a-z, A-Z)";
                else if (iType == 1)
                    strError = "Mật khẩu chưa có chứa ký tự số (0-9)";
                else if (iType == 2)
                    strError = "Mật khẩu chưa có ký tự chữ (a-z, A-Z)";
                return false;
            }
            //Kiem tra nhap xac nhan mat khau
            if (string.IsNullOrEmpty(txtRetype.Text))
            {
                strError = "Nhập xác nhận mật khẩu";
                return false;
            }
            if(txtPassword.Text.Trim()!= txtRetype.Text.Trim())
            {
                strError = "Nhập lại xác nhận mật khẩu";
                return false;
            }
            if (txtOldPassword.Text.Trim() == txtPassword.Text.Trim())
            {
                strError = "Mật khẩu mới trùng mật khẩu cũ";
                return false;
            }
            //Kiem tra mat khau cu
            if (objUsers_BO.CHECK_OLDPASS(username, txtOldPassword.Text.Trim()) != 1)
            {
                strError = "Mật khẩu cũ không đúng";
                return false;
            }            
            //Kiem tra so lan trung pass
            int iPassTime;
            DataSet ds = new DataSet();
            iPassTime = objSysPara_BO.GetPassTime();
            ds = objUserPass_BO.CheckUSERS_PASS(username, txtPassword.Text.Trim());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                strError = "Mật khẩu trùng với 1 trong " + iPassTime + " lần đổi gần nhất";
                return false;
            }
            //Cap nhat thong tin bang USER_PASS
            objUserPass.USER_ID = username;
            objUserPass.PRE_PASS = txtPassword.Text.Trim();
            objUserPass.PASSTIME = DateTime.Now;
            return result;
        }
    }
}
