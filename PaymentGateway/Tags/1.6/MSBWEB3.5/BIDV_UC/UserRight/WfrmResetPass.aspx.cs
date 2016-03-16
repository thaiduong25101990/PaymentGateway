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
using BIDVWEB.Business.Reports;
using BIDVWEB.Business.Web;

namespace BIDVWEB.BIDV_UC.UserRight
{
    public partial class WfrmResetPass : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDatatAccess = new clsDatatAccess();
        private USERS_BO objUsers_BO = new USERS_BO();        
        private USER_PASSInfo objUserPass = new USER_PASSInfo();
        private USER_PASS_BO objUserPass_BO = new USER_PASS_BO();
        private clsForm objForm = new clsForm();
        private clsReport objReport = new clsReport();
        private string strError = "";   
        private UserEncrypt Encrypt = new UserEncrypt();        
        private SYSPARA_BO objSysPara_BO = new SYSPARA_BO();
        private string strBranchCode = "";
        private string strBrHo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    string sMenuID = "";
                    int iShow;
                    Button btnAdd =new Button();
                    Button btnDel = new Button();

                    lblError.Text = "";
                    sMenuID = Request["mn"];
                    objForm.PageLoad(sMenuID, btnAdd, btnChange, btnDel, out iShow);
                    if (iShow > 0)
                    {
                        if (!LoadData())
                        {
                            objForm.MessboxWeb(this.Page, strError);
                            return;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            int iInt1;
            int iInt2;

            try
            {
                //Kiem tra du lieu
                if (!CheckData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                iInt2 = objUserPass_BO.AddUSER_PASS(objUserPass);
                iInt1 = objUsers_BO.UPDATE_PASS_RESET(ddlUser.SelectedValue.ToString(), 
                    txtPassword.Text,DateTime.Now,2);                
                if ( iInt1== 1 && iInt2==1)
                {                    
                    strError = "Mật khẩu đã được cấp lại";                    
                    txtPassword.Text = "";
                    txtRetype.Text = "";                    
                }
                else
                {   
                    txtPassword.Text = "";
                    txtRetype.Text = "";
                    strError = "Mật khẩu chưa được cấp lại";
                }
                objForm.MessboxWeb(this.Page, strError);
            }
            catch(Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        ////////////////////////////////////////////////////////////////
        // Muc dich:    Ham load data
        // Ngay tao:    07/08/2008
        // Dau vao:     NA
        // Dau ra:      True: Thanh cong, False: Loi        
        ////////////////////////////////////////////////////////////////
        private bool LoadData()
        {
            try
            {
                lblError.Text = "";
                //Chi cho phep chinh sua cac thong tin cua nhom
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Danh sach NSD
                //Kiem tra chi nhanh la H.O
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    objDatatAccess.FillDataToDropDownList("VIEW_USERS",
                    "LPAD(branch,5,'0') LIKE '%' || LPAD(" + strBranchCode + ",5,'0') || '%'",
                    ddlUser, "USERNAME", "USERID", "USERNAME ASC", false);
                }
                else
                {
                    objDatatAccess.FillDataToDropDownList("VIEW_USERS","",
                    ddlUser, "USERNAME", "USERID", "USERNAME ASC", false);
                }
                
                if (ddlUser.Items.Count > 0)
                {
                    lblUser.Text = objUsers_BO.GetUSERNAME(ddlUser.SelectedValue.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
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
            username = ddlUser.SelectedValue.ToString();

            if (ddlUser.Items.Count <= 0)
            {
                strError = "Nhập danh sách NSD";
                return false;
            }            
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                strError = "Nhập mật khẩu";
                return false;
            }
            if (string.IsNullOrEmpty(txtRetype.Text))
            {
                strError = "Nhập xác nhận mật khẩu";
                return false;
            }
            if (txtPassword.Text.Trim() != txtRetype.Text.Trim())
            {
                strError = "Nhập lại xác nhận mật khẩu";
                return false;
            }
            //Check do dai password
            int iPassLength = 0;
            if (!objUsers_BO.CHECK_PASS_LENGTH(txtPassword.Text.Trim(), out iPassLength))
            {
                strError = "Nhập mật khẩu có độ dài >= " + iPassLength;
                return false;
            }
            ////Kiem tra so lan trung pass
            //int iPassTime;
            //DataSet ds = new DataSet();
            //iPassTime = objSysPara_BO.GetPassTime();
            //ds = objUserPass_BO.CheckUSERS_PASS(username, txtPassword.Text.Trim());
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    strError = "Mật khẩu trùng với 1 trong " + iPassTime + " lần đổi gần nhất";
            //    return false;
            //}

            //Thong tin cap nhat bang USER_PASS
            objUserPass.USER_ID = ddlUser.SelectedValue.ToString();
            objUserPass.PRE_PASS = txtPassword.Text.Trim();
            objUserPass.PASSTIME = DateTime.Now;            
            return result;
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.Items.Count > 0)
            {
                lblUser.Text = objUsers_BO.GetUSERNAME(ddlUser.SelectedValue.ToString());
            }
        }

    }
}
