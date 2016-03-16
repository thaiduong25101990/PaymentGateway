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
    public partial class WfrmUser : System.Web.UI.Page
    {        
        private int iStatus = 0;
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsCommon objCommon = new clsCommon();
        private clsForm objForm = new clsForm();
        private string strError = "";
        private clsBranch objBranch = new clsBranch();
        private USERSInfo objUserInfo = new USERSInfo();
        private USERS_BO objUser_BO = new USERS_BO();
        private USER_PASSInfo objUserPass = new USER_PASSInfo();
        private USER_PASS_BO objUserPass_BO = new USER_PASS_BO();
        private SYSPARA_BO objSysPara_BO = new SYSPARA_BO();
        private clsReport objReport = new clsReport();
        private string strBranchCode = "";
        private string pWhere = "";
        private string strBrHo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (!IsPostBack)
                {
                    string sMenuID = "";
                    int iShow;                    
                    sMenuID = Request["mn"];
                    objForm.PageLoad(sMenuID, btnAdd, btnSave, btnDel, out iShow);
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
               
                
        //Load Data/////////////////////////////////////////////////
        // Muc dich:    Load du lieu
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:              
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////
        private bool LoadData()
        {
            try
            {
                DropDownList ddlList;
                //Gan image anh calendar cho textbox txtDate
                //objCommon.gs_SetDate(txtLTDate, img1);
                //objCommon.gs_SetDate(txtLCPDate, img2);
                img1.Visible = false;
                img2.Visible = false;
                txtLTDate.Enabled = false;
                txtLCPDate.Enabled = false;
                ddlStatus.Enabled = false;

                //Load chi nhanh ngan hang
                ddlList = objDataAccess.FillDataToDropDownList("BRANCH", "",ddlBranch,
                        "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE ASC", true);
                if (ddlList != null)
                {
                    //ddlBranch = ddlList;                    
                }
                else
                {
                    strError = "Chưa nhập chi nhánh";
                }
                //Chi cho phep chinh sua cac thong tin cua nhom
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh la H.O
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    ddlBranch.SelectedValue = strBranchCode;
                    ddlBranch.Enabled = false;
                }

                //Load trang thai
                ddlList = objDataAccess.FillDataToDropDownList("ALLCODE", 
                    "upper(CDNAME) = upper('USERSTS')", ddlStatus,
                        "CONTENT", "CDVAL", "CDVAL ASC", false);
                if (ddlList != null)
                {                    
                    ddlStatus.SelectedValue = "2";
                }
                else
                {
                    strError = "Chưa nhập trạng thái";
                }
                //view mot ban ghi
                if (Request["USERID"] != "" && Request["USERID"] != null)
                {
                    ViewCurrentRecord(Request["USERID"]);
                }
                if (ddlBranch.Items.Count > 0 && ddlBranch.SelectedItem.Text.ToUpper() != "ALL")
                {
                    if (txtPrefix.Text.Trim() + txtUserID.Text.Trim().ToUpper() != "ADMIN")
                    {
                        txtPrefix.Text = ddlBranch.SelectedValue.ToString();
                    }
                }
                //Ten chi nhanh
                lblBranch.Text = objBranch.GetBranchName(ddlBranch.SelectedValue.ToString());
                //Load Data
                pWhere = "";
                if (!ViewGrid())
                {
                    return false;
                }                               
                              
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Ham view thong tin user//////////////////////////////////////
        //Mo ta:        Ham view thong tin user
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iUserID: Ma NSD
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private void ViewCurrentRecord(string iUserID)
        {
            try
            {
                DataSet sv_dsData = new DataSet();                
                DataRow dr;

                //txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirm.Enabled = false;
                txtPrefix.Enabled = false;
                ddlStatus.Enabled = true;
                ddlBranch.Enabled = false;
                
                //Get dataset chua thong tin user
                sv_dsData = objUser.GetUserByID(iUserID);
                if (sv_dsData.Tables[0].Rows.Count == 0)
                    return;
                dr = sv_dsData.Tables[0].Rows[0];
                if (sv_dsData ==null && sv_dsData.Tables[0].Rows.Count<=0)
                {
                    strError = objUser.strError;
                }                                            
                this.txtDes.Text = sv_dsData.Tables[0].Rows[0].Field<string>("DESCRIPTION");
                this.txtEmail.Text = sv_dsData.Tables[0].Rows[0].Field<string>("EMAIL");
                this.txtMobile.Text = sv_dsData.Tables[0].Rows[0].Field<string>("MOBILE");
                this.txtPassword.Text = sv_dsData.Tables[0].Rows[0].Field<string>("PASSWORD");
                if (dr["USERID"].ToString().Length > 3)
                {
                    if (dr["USERID"].ToString().Trim().ToUpper() == "ADMIN")
                    {
                        this.txtPrefix.Text = "";
                        this.txtUserID.Text = dr["USERID"].ToString();
                    }
                    else
                    {
                        this.txtPrefix.Text = dr["USERID"].ToString().Substring(0, 3);
                        this.txtUserID.Text = dr["USERID"].ToString().Substring(3, dr["USERID"].ToString().Length - 3);
                    }
                }                
                this.txtUserID.Enabled = false;
                this.txtUserName.Text = sv_dsData.Tables[0].Rows[0].Field<string>("USERNAME");
                if (dr["PASSTIME"].ToString() != null && dr["PASSTIME"].ToString() != "")
                {
                    this.txtLCPDate.Text = objCommon.g_Formatdate(dr["PASSTIME"].ToString(),false);
                }
                else
                {
                    this.txtLCPDate.Text = "";
                }
                if (dr["LASTDATE"].ToString() != null && dr["LASTDATE"].ToString() != "")
                {
                    this.txtLTDate.Text = objCommon.g_Formatdate(dr["LASTDATE"].ToString(), false);
                }
                else
                {
                    this.txtLTDate.Text = "";
                }   
                //this.txtUserName.Enabled = false;
                if (ddlBranch.Items.Count > 0 &&
                    sv_dsData.Tables[0].Rows[0].Field<string>("BRANCH") != "") 
                {
                    ddlBranch.SelectedValue = sv_dsData.Tables[0].Rows[0].Field<string>("BRANCH");
                }                
                if (string.IsNullOrEmpty(dr["STATUS"].ToString()))
                {
                    ddlStatus.SelectedIndex =-1;
                }
                else
                {
                    ddlStatus.SelectedValue =dr["STATUS"].ToString();
                    if (dr["STATUS"].ToString()=="2")
                    {
                        ddlStatus.Enabled = false;
                    }
                }                
            }   
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }
        
        //Add user
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserRight/WfrmUser.aspx?mn=1201");
        }

        //Cap nhat du lieu/////////////////////////////////////////////
        //Mo ta:        Cap nhat du lieu
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       cap nhat thanh cong        
        ///////////////////////////////////////////////////////////////
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            
            //btnSave.Attributes.Add("onClick", "return ConfirmDeletion();");
            ////return confirm('Are you sure you wish to delete this record?');
            //this.btnSave.Attributes.Add("onClick",
            //        "<script language=javascript>" +
            //        " window.parent.location='~/Default.aspx'; </script>");
            int iInt1;
            int iInt2;

            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (CheckData())
                {
                    string sUserID = "";
                    sUserID = txtPrefix.Text.ToString() + txtUserID.Text.ToString();                    
                    //Sua
                    if (Request["USERID"] != "" && Request["USERID"] != null)
                    {
                        //Kiem tra trung UserID, UserName
                        //if (!objUser.CheckUserID_Name(sUserID, txtUserName.Text.ToString(), false))
                        if (!objUser_BO.CHECK_USERNAME(sUserID, txtUserName.Text.ToString(), false))
                        {
                            strError = "Trùng mã NSD";
                        }
                        else
                        {
                            GetDataSave();                            
                            //if (objUser.AddUser(sUserID, txtUserName.Text.ToString(), 
                            //    txtPassword.Text.ToString(), txtEmail.Text.ToString(), 
                            //    txtMobile.Text.ToString(), txtDes.Text.ToString(), 
                            //    Convert.ToString(ddlBranch.SelectedValue), iStatus, false))
                            iInt1 = objUser_BO.UpdateUSERS(objUserInfo);                            
                            if (iInt1 ==1)
                            {
                                strError = "Cập nhật thành công!";
                            }
                            else
                            {
                                strError = "Lỗi khi cập nhật";
                            }
                        }                        
                    }
                    //Them moi
                    else
                    {
                        //Kiem tra trung UserID, UserName
                        if (!objUser_BO.CHECK_USERNAME(sUserID, txtUserName.Text.ToString(), true))
                        {
                            strError = "Trùng mã NSD";                            
                        }
                        else
                        {
                            GetDataSave();
                            //if (objUser.AddUser(sUserID, txtUserName.Text.ToString(), 
                            //    txtPassword.Text.ToString(), txtEmail.Text.ToString(), 
                            //    txtMobile.Text.ToString(), txtDes.Text.ToString(), 
                            //    Convert.ToString(ddlBranch.SelectedValue), iStatus, true))
                            iInt1 = objUser_BO.AddUSERS(objUserInfo);
                            iInt2 = objUserPass_BO.AddUSER_PASS(objUserPass);
                            if (iInt1 == 1 && iInt2 == 1)
                            {
                                strError = "Thêm mới thành công";
                            }
                            else
                            {
                                strError = "Lỗi khi thêm mới";
                            }
                        }
                    }
                }
                objForm.MessboxWeb(this.Page, strError);                
                ViewGrid();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        //Muc dich: Ham lay du lieu truoc khi save
        //Ngay tao: 07/2008
        //
        private void GetDataSave()
        {
            string sUserID = "";            

            //Sua
            if (!string.IsNullOrEmpty(Request["USERID"]))
            {
                sUserID = Request["USERID"].ToString();
                objUserInfo.DESCRIPTION = txtDes.Text.ToString();
                objUserInfo.EMAIL = txtEmail.Text.ToString();                                
                objUserInfo.MOBILE = txtMobile.Text.ToString();                
                objUserInfo.STATUS = iStatus;
                objUserInfo.USERID = sUserID;
                objUserInfo.LASTDATE = DateTime.Now;
                objUserInfo.USERNAME = txtUserName.Text.ToString();  
            }
            //Them moi
            else
            {
                sUserID = txtPrefix.Text.ToString() + txtUserID.Text.ToString();
                objUserInfo.BRANCH = ddlBranch.SelectedValue.ToString();
                objUserInfo.COUNTTIME = 0;
                objUserInfo.DESCRIPTION = txtDes.Text.ToString();
                objUserInfo.EMAIL = txtEmail.Text.ToString();
                //objUserInfo.ID =ID;
                objUserInfo.LASTDATE = DateTime.Now;
                objUserInfo.MOBILE = txtMobile.Text.ToString();
                objUserInfo.PASSTIME = DateTime.Now;
                objUserInfo.PASSWORD = txtPassword.Text.ToString();
                objUserInfo.STATUS = iStatus;
                objUserInfo.USERID = sUserID;
                objUserInfo.USERNAME = txtUserName.Text.ToString();  
              
                objUserPass.PRE_PASS = txtPassword.Text.ToString();
                objUserPass.USER_ID = sUserID;
                objUserPass.PASSTIME = DateTime.Now;
            }
        }


        //Kiem tra du lieu truoc khi cap nhat//////////////////////////
        //Mo ta:        Kiem tra du lieu truoc khi cap nhat
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iUserID: Ma NSD
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool CheckData()
        {
            if (this.ddlBranch.Items.Count <= 0)
            {
                strError = "Nhập chi nhánh";
                return false;
            }
            if (txtPrefix.Text == "")
            {
                if (txtUserID.Text.Trim().ToUpper() != "ADMIN")
                {
                    strError = "Chọn chi nhánh";
                    return false;
                }
            }
            //Them moi NSD
            if (string.IsNullOrEmpty(Request["USERID"]))
            {
                if (string.IsNullOrEmpty(this.txtUserID.Text.Trim()))
                {
                    strError = "Nhập mã NSD";
                    return false;
                }
                else
                {
                    //if (!objCommon.g_IsNumeric(txtUserID.Text.ToString()))
                    //{
                    //    strError = "Nhập mã NSD là số";
                    //    return false;
                    //}
                }
                if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
                {
                    strError = "Nhập tên";
                    return false;
                }
                if (string.IsNullOrEmpty(txtPrefix.Text.ToString()))
                {
                    strError = "Chọn chi nhánh";
                    return false;
                }
                if (ddlBranch.SelectedItem.Text.ToUpper() == "ALL")
                {
                    if (txtUserID.Text.Trim().ToUpper() != "ADMIN")
                    {
                        strError = "Chọn chi nhánh";
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
                {
                    strError = "Nhập mật khẩu";
                    return false;
                }
                if (string.IsNullOrEmpty(this.txtConfirm.Text.Trim()))
                {
                    strError = "Nhập xác nhận mật khẩu";
                    return false;
                }
                if (txtPassword.Text.Trim() != txtConfirm.Text.Trim())
                {
                    txtConfirm.Text = "";
                    strError = "Nhập lại mật khẩu xác nhận";
                    return false;
                }
                //Check do dai password
                int iPassLength = 0;
                if (!objUser_BO.CHECK_PASS_LENGTH(txtPassword.Text.Trim(), out iPassLength))
                {
                    strError = "Nhập mật khẩu có độ dài >= " + iPassLength;
                    return false;
                }
                //Kiem tra status
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
                {
                    strError = "Nhập tên";
                    return false;
                }
            }
            if (this.txtMobile.Text.ToString()!="")
            {
                if (!objCommon.g_IsNumeric(txtMobile.Text.ToString()))
                {
                    strError = "Nhập số điện thoại là số";
                    return false;
                }
            }          
            //Kiem tra email
            if (this.txtEmail.Text.Trim() != "")
            {
                if (this.txtEmail.Text.IndexOf("@") <= 0)
                {
                    strError = "Nhập email chưa đúng";
                    return false;
                }
            }
            //Kiem tra so NSD trong mot chi nhanh
            if (string.IsNullOrEmpty(Request["USERID"]))
            {
                int iAccountNumber=-1;
                int iAN_Branch;
                iAccountNumber = objSysPara_BO.GetAccountNumber();
                iAN_Branch = objUser_BO.GetSumUser_Branch(ddlBranch.SelectedValue.ToString());
                if (iAN_Branch != -1 && iAN_Branch >= iAccountNumber)
                {
                    strError = "Chi nhánh này đã đủ " + iAccountNumber + " NSD";
                    return false;
                }
            }
            iStatus =Convert.ToInt16(ddlStatus.SelectedValue);
            return true;
        }

        

        //Ham view thong tin bao cao///////////////////////////////////
        //Mo ta:        Ham view thong tin bao cao
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iReportID: Ma bao cao
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool ViewGrid()
        {
            try
            {
                string strUserID = "";
                string strUserName = "";

                if (!string.IsNullOrEmpty(txtPrefix.Text) &&
                    !string.IsNullOrEmpty(txtUserID.Text))
                {
                    strUserID = txtPrefix.Text.Trim() + txtUserID.Text.Trim();
                }
                else
                {

                    if (txtUserID.Text.Trim().ToUpper() == "ADMIN")
                        strUserID = txtUserID.Text.Trim();
                    else
                        strUserID = txtPrefix.Text.Trim() + txtUserID.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtUserName.Text))
                {
                    strUserName = txtUserName.Text.Trim();
                } 

                DataSet sv_dsData = new DataSet();
                //Chi cho phep chinh sua cac thong tin cua nhom
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh 
                if (ddlBranch.Items.Count > 0)
                {
                    if (string.IsNullOrEmpty(strUserID + strUserName))
                    {
                        if (ddlBranch.SelectedItem.Text.ToUpper() == "ALL")
                        {
                            sv_dsData = objUser.GetAllUser();
                        }
                        else
                        {
                            sv_dsData = objUser_BO.GetUSERSBR(ddlBranch.SelectedValue.ToString(), strBranchCode);
                        }
                    }
                    else
                    {
                        sv_dsData = objUser_BO.GetUsersByStr(ddlBranch.SelectedValue.ToString(), strBranchCode, strUserName,strUserID);
                    }
                }                
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count < 0)
                {
                    strError = objUser.strError;
                    return false;
                }
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Checkbox trong Gridview//////////////////////////////////////
        //Mo ta:        Checkbox trong Gridview
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
        {            
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            Response.Write(row.Cells[0].Text);
            if (checkbox.Checked == true)
            {
                btnDel.Attributes.Add("OnClick", "return confirm(\"Bạn chắc chắn muốn xóa?\")");
            }
            else
            {
                btnDel.Attributes.Remove("OnClick");
            }
        }

        //Ham xoa user
        protected void btnDel_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            bool iBool = false;            
            //Response.Redirect("~//UserRight/HTMLPage1.htm");
            try
            {
                if (grvData.Rows.Count > 0)
                {
                    //Kiem tra theo tung ban ghi da chon                    
                    string sID;
                    for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        sID = "";
                        if (chkbox.Checked == true)
                        {
                            btnDel.Attributes.Remove("OnClick");
                            //Lay ID o cot 1
                            sID = grvData.Rows[i].Cells[1].Text.ToString();
                            if (objUser.CheckUser_Group(sID, false))
                            {
                                //Kiem tra user admin, khong cho xoa
                                if (objUser_BO.CHECK_USER_ADMIN(sID) == 1)
                                {
                                    strError = "Không được xóa NSD admin";
                                    objForm.MessboxWeb(this.Page, strError);
                                    return;
                                }
                                //Goi ham xoa
                                if (!objUser.DeleteUser(sID))
                                    strError = objUser.strError;
                                else
                                    iBool = true;
                            }
                            else
                            {
                                strError = sID + " đang thuộc nhóm NSD";
                                objForm.MessboxWeb(this.Page, strError);
                                return;
                            }
                        }
                    }
                    if (iBool == true)
                    {
                        strError = "Xóa thành công";
                        ResetControl();
                    }
                    objForm.MessboxWeb(this.Page, strError);                    
                    ViewGrid();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (txtUserID.Text.ToString() == "")
            //{
            //    txtPrefix.Text = ddlBranch.SelectedValue.ToString();
            //}            

            //Recset control
            ResetControl();
            if (ddlBranch.Items.Count > 0)
            {
                //Ten chi nhanh
                lblBranch.Text = objBranch.GetBranchName(ddlBranch.SelectedValue.ToString());
                //Vie du lieu theo chi nhanh                
                ViewGrid();
                if (ddlBranch.SelectedItem.Text.ToUpper() != "ALL")
                {
                    if (txtUserID.Text.ToString() == "")
                    {
                        //Comment lai khong fix 3 ky tu dau tien cua USERID la ma chi nhanh nua
                       // txtPrefix.Text = ddlBranch.SelectedValue.ToString();
                    }
                }
            }
        }               

        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;            
            ViewGrid();
        }        

        protected void ddlStatus_TextChanged(object sender, EventArgs e)
        {
            //Sua thong tin NSD
            if (!string.IsNullOrEmpty(Request["USERID"]))
            {
                DataSet dsData = new DataSet();
                DataRow dr;
                string sUserID = "";
                sUserID = Request["USERID"].ToString();

                //Get dataset chua thong tin user
                dsData = objUser.GetUserByID(sUserID);
                dr = dsData.Tables[0].Rows[0];
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    if (dr["STATUS"].ToString() == "1" && 
                        ddlStatus.SelectedValue.ToString()=="2")
                    {
                        ddlStatus.SelectedValue = "1";
                    }
                    if (dr["STATUS"].ToString() == "3" &&
                        ddlStatus.SelectedValue.ToString() == "2")
                    {
                        ddlStatus.SelectedValue = "3";
                    }
                }
            }
        }

        private void ResetControl()
        {
            txtPrefix.Text = "";
            txtUserID.Text = "";
            txtMobile.Text = "";
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirm.Text = "";
            txtLTDate.Text = "";
            txtLCPDate.Text = "";
            txtDes.Text = "";            
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                                     
                ViewGrid();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

    }
}
