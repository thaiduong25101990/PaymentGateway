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
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.Web;


namespace BIDVWEB.BIDV_UC
{
    public partial class WfrmLogin : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsLogin objclsLogin = new clsLogin();
        private clsForm objForm = new clsForm();
        private SYSPARA_BO objSysPara_BO = new SYSPARA_BO();
        private USERS_BO objUsers_BO = new USERS_BO();
        string strError="";

        protected void Page_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("", "BIDVWEB", MessageBoxButtons.YesNo);            

            //msg_button += " onClick=\"window.location='" + btnLocation + "'\"";
            //this.Controls.Add(new LiteralControl(
            //    "<script language=javascript>" +
            //    "    window.location='B';" +
            //    "</script>"));

            //yNlNOCJf9yk=         
            //string str = Encrypt.Decrypt("yNlNOCJf9yk=");







            if (!IsPostBack)
            {                
                //int iInquiry=0;
                //int iDelete=0;
                //int iInsert=0;a
                //int iEdit=0;
                
                //Session.Clear();
                //SessionHelper.Clear();  
                lblError.Text = "";                
                //if (!objUser.CheckPerForm( "1101", out iInquiry,
                //    out iDelete, out iInsert, out iEdit))
                //{
                //}                                               
            }                
        }
        

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Log in he thong
        //Ngay tao:     05/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      /
        //Dau ra:       /Dang nhap he thong thanh cong
        ///////////////////////////////////////////////////////////////
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool Authenticated = false;
            int iLoginTimePara=0;
            int iLoginTime;
            int iNumLoginDay;
            string sUser = "";
            //Lay user/pass tu man hinh login
            string username = txtUserID.Text.ToString();
            string password = txtPass.Text.ToString();
            //Lay UserName tu session  
            if (Session["username"]!=null)
            {
                sUser = Session["username"].ToString();
            }
            //Kiem tra so lan cho phep nhap userid/pass sai
            iLoginTimePara = objSysPara_BO.GetLoginTime();
            if (iLoginTimePara < 0)
            {
                objForm.MessboxWeb(this.Page, strError);                
                return;
            }
            if (Session["LoginTime"]==null)
            {
                iLoginTime = 0;                
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["LoginTime"].ToString()))
                {
                    if (sUser != username)                    
                        iLoginTime = 0;
                    else
                        iLoginTime = Convert.ToInt16(Session["LoginTime"].ToString());
                }
                else
                {
                    iLoginTime = 0;
                }
            }
            if (iLoginTime > iLoginTimePara-1)
            {
                if (username.ToUpper() != "ADMIN")
                {
                    //Cap nhat lai trang thai user thanh lock
                    if (objUsers_BO.UpdateUSERSTATUS(username, "3") != 1)
                    {
                        strError = "Lỗi khi cập nhật lại trạng thái NSD";
                        objForm.MessboxWeb(this.Page, strError);
                        return;
                    }
                }
                strError = "Bạn đã nhập sai Mã NSD hoặc mật khẩu " +
                    iLoginTimePara + " lần";
                objForm.MessboxWeb(this.Page, strError);
                return;
            }

            //SetCookie
            FormsAuthentication.SetAuthCookie(txtUserID.Text.ToString().Trim(), false);

            //Goi ham kiem tra userID/pass
            Authenticated = objclsLogin.CheckUserAndPassword(username, password);            
            //e.Authenticated = Authenticated;
            Session["username"] = txtUserID.Text.ToString();
            SessionHelper.Store("username", username);

            //Neu NSD khong dang nhap he thong qua so ngay quy dinh
            //thi tai khoan cua NSD se bi lock
            if (Authenticated == true)
            {
                iNumLoginDay = objSysPara_BO.GetNumLoginDay();
                if (string.IsNullOrEmpty(objclsLogin.sLastDate.ToString()))
                {
                    strError = "";
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                DateTime today = DateTime.Now;
                DateTime LastLogInDate;
                LastLogInDate = Convert.ToDateTime(objclsLogin.sLastDate.ToString());
                TimeSpan duration = new TimeSpan(iNumLoginDay, 0, 0, 0);
                DateTime answer = LastLogInDate.Add(duration);
                if (today > answer)
                {                    
                    //Cap nhat lai trang thai user thanh lock
                    if (objUsers_BO.UpdateUSERSTATUS(username, "3") != 1)
                    {
                        strError = "Lỗi khi cập nhật lại trạng thái NSD";
                        objForm.MessboxWeb(this.Page, strError);
                        return;
                    }
                    strError = "Bạn đã không đăng nhập hệ thống quá " +
                        iNumLoginDay + " ngày, tài khoản đã bị khóa";
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
            }

            if (Authenticated == false)
            {
                strError = objclsLogin.strError;
                //Luu so lan nhap userid/pass sai
                if (objclsLogin.iLogFail == 1)
                {
                    if (Session["LoginTime"] == null)
                    {
                        Session["LoginTime"] = "1";
                    }
                    else
                    {
                        iLoginTime = iLoginTime + 1;
                        Session["LoginTime"] = iLoginTime.ToString();
                    }
                }
                objForm.MessboxWeb(this.Page, strError);
                return;
            }
            else
            {
                //Xoa session luu so lan nhap userid/pass sai
                Session["LoginTime"] = null;
                //kiem tra co phai dang nhap lan dau tien khong
                //Chi dua vao trang thai Pending, bo so lan dang nhap = 0
                //if (objclsLogin.CountTimeLogin == 0 && objclsLogin.iStatus == 2)
                if (objclsLogin.iStatus == 2)
                {
                    //cap nhat truong CoutTimeLogin cua bang USERS la 1                
                    //objclsLogin.UpdateCoutTimeLogin(username);
                    //luu giu gia tri txtUserID vao bien session,gia tri nay dung trong viec thay doi mat khau                    
                    Session.Add("username", username);
                    //nhay den trang thay doi mat khau
                    Response.Redirect("~//UserRight/WfrmChangePass1.aspx");
                }
                else
                {
                    //luu giu gia tri txtUserID vao bien session,
                    //gia tri nay dung trong viec thay doi mat khau                    
                    Session.Add("username", username);
                    //Ghi log                    
                    if (objUser.SaveUserLog(DateTime.Now, "", "", 1, "Login", "USERS", "", ""))
                    {
                        //Nhay den trang Main                   
                        Response.Redirect("~//Common/Main.aspx");
                        //FormsAuthentication.RedirectFromLoginPage(username, false);
                    }
                    else
                    {
                        Response.Redirect("~/WfrmLogin.aspx");
                    }
                }
            }
        }

        protected void Page_Init(object Sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        }

        //onKeydown="if (event.keyCode == 8){return false;}"
    }
}
