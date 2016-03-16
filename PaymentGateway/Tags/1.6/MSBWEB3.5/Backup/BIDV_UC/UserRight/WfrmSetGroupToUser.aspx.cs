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
    public partial class WfrmSetGroupToUser : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDataAccess = new clsDatatAccess();        
        private clsForm objForm = new clsForm();
        private clsReport objReport = new clsReport();
        private string strError = "";
        private string strWhere = "";
        private string strBranchCode = "";
        private string strBranch = "";
        private string strUserID = "";
        private string strBrHo = "";
        private DropDownList ddlList;



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                if (!IsPostBack)
                {
                    int iShow;
                    Button btnAdd = new Button();
                    Button btnDel = new Button();
                    string sMenuID = "";
                    sMenuID = Request["mn"];                    
                    objForm.PageLoad(sMenuID, btnLeft, btnRight, btnDel, out iShow);
                    if (iShow > 0)
                    {
                        if(!LoadData())
                            objForm.MessboxWeb(this.Page, strError);
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
                //Load chi nhanh ngan hang
                ddlList = objDataAccess.FillDataToDropDownList("BRANCH", "", ddlBranch,
                        "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE ASC", false);                                
                //Chi cho phep chinh sua cac thong tin cua nhom
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh la H.O
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    ddlBranch.Enabled = false;
                    if (strBranchCode.Length > 3)
                        ddlBranch.SelectedValue = strBranchCode.Substring(2, 3);
                    else
                        ddlBranch.SelectedValue = strBranchCode;                    
                }
                //Lay dieu kien load danh sach nguoi su dung
                if (ddlBranch.Items.Count > 0)
                {
                    if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                    {
                        strWhere = " UPPER(USERID)<>'ADMIN' AND LPAD(TRIM(BRANCH),5,'0') LIKE '%" +
                        ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                    }
                    else
                    {
                        strWhere = "";
                    }                    
                    //Load nguoi su dung
                    ddlList = objDataAccess.FillDataToDropDownList("VIEW_USERS",
                        strWhere, ddlUser, "USERNAME", "USERID", "USERNAME ASC", false);                
                }       
                //View danh sach user
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


        //Ham view thong tin user/////////////////////////////////////
        //Mo ta:        Ham view thong tin user
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool ViewGrid()
        {
            try
            {
                DataSet sv_dsData = new DataSet();
                if (ddlBranch.Items.Count > 0)
                    strBranch = ddlBranch.SelectedValue.ToString();
                if (ddlUser.Items.Count > 0)
                    strUserID = ddlUser.SelectedValue.ToString();
                sv_dsData = objUser.GetAllGroupUser(strUserID,strBranch, false);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objUser.strError;
                    return false;
                }                
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();

                if (ddlUser.Items.Count > 0)
                {
                    sv_dsData.Tables.Clear();
                    sv_dsData = objUser.GetAllGroupUser(strUserID, strBranch, true);
                    if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                    {
                        strError = objUser.strError;
                        return false;
                    }
                    grvData1.DataSource = sv_dsData.Tables[0];
                    grvData1.DataBind();
                }

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            bool iBool = false;
            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (!CheckData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                else
                {
                    //Kiem tra theo tung ban ghi da chon
                    string sUserID = Convert.ToString(ddlUser.SelectedValue);
                    int iGroupID;
                    for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        iGroupID = 0;
                        if (chkbox.Checked == true)
                        {
                            iGroupID = Convert.ToInt16(grvData.Rows[i].Cells[1].Text);
                            if (!objUser.AddUserToGroup(iGroupID, sUserID))
                            {
                                objForm.MessboxWeb(this.Page, strError);
                                return;
                            }
                            iBool = true;
                        }
                    }
                    if (iBool == true)
                    {
                        strError = "Cập nhật thành công";
                        objForm.MessboxWeb(this.Page, strError);
                    }
                }
                ViewGrid();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            bool iBool = false;
            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (!CheckData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                else
                {
                    //Kiem tra theo tung ban ghi da chon
                    string sUserID = Convert.ToString(ddlUser.SelectedValue);
                    int iGroupID;
                    for (int i = 0; i <= grvData1.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData1.Rows[i].Cells[0].Controls[1];
                        iGroupID = 0;
                        if (chkbox.Checked == true)
                        {                            
                            iGroupID = Convert.ToInt16(grvData1.Rows[i].Cells[1].Text);
                            if (!objUser.DelUserToGroup(iGroupID, sUserID))
                            {
                                objForm.MessboxWeb(this.Page, strError);
                                return;
                            }
                            iBool = true;
                        }
                    }
                    if (iBool == true)
                    {
                        strError = "Cập nhật thành công";
                        objForm.MessboxWeb(this.Page, strError);
                    }
                }
                ViewGrid();
            }
            catch (Exception ex)
            {
                if (objUser.strError != "")
                {
                    strError = objUser.strError + ex.Message;
                }
                else
                {
                    strError = ex.Message;
                }
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
            if (this.ddlUser.Items.Count <= 0)
            {
                strError = "Nhập nhóm!";
                objForm.MessboxWeb(this.Page, strError);
                strError = "";
                return false;
            }
            return true;
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
        }              

        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        protected void grvData1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData1.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        //Checkbox all trong Gridview grvData//////////////////////////
        //Mo ta:        Checkbox all trong Gridview grvData
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void chkALL_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvData.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                if (checkbox.Checked == true)
                    chkbox.Checked = true;
                else
                    chkbox.Checked = false;
            }
        }

        //Checkbox all trong Gridview grvData1/////////////////////////
        //Mo ta:        Checkbox all trong Gridview grvData1
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void chkALL1_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvData1.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvData1.Rows[i].Cells[0].Controls[1];
                if (checkbox.Checked == true)
                    chkbox.Checked = true;
                else
                    chkbox.Checked = false;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Items.Count > 0)
            {
                //Chi cho phep chinh sua cac thong tin cua nhom
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    strWhere = " UPPER(USERID)<>'ADMIN' AND LPAD(TRIM(BRANCH),5,'0') LIKE '%" +
                    ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                }
                else
                {
                    strWhere = "";
                }                 
                //Load nguoi su dung
                ddlList = objDataAccess.FillDataToDropDownList("VIEW_USERS",
                    strWhere, ddlUser, "USERNAME", "USERID", "USERNAME ASC", false);
                ViewGrid();
            }
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewGrid();
        }








    }
}
