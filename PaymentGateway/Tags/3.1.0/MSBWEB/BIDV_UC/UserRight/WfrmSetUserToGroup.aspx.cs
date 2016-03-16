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
using BIDVWEB.Business.Reports;

namespace BIDVWEB.BIDV_UC.UserRight
{
    public partial class WfrmSetUserToGroup : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsReport objReport = new clsReport();
        private string strError = "";
        private string strBranchCode = "";
        private DropDownList ddlList;
        private string strWhere = "";
        private string strBrHo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (!IsPostBack)
                {
                    int iShow;
                    Button btnAdd = new Button();
                    Button btnDel = new Button();
                    string sMenuID = "";
                    sMenuID = Request["mn"];                    
                    objForm.PageLoad(sMenuID,btnLeft, btnRight, btnDel, out iShow);
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
                //Load chi nhanh ngan hang
                ddlList = objDataAccess.FillDataToDropDownList("BRANCH", "", ddlBranch,
                        "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE ASC", false);
                //Lay ma chi nhanh theo NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh KHAC H.O
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    ddlBranch.Enabled = false;
                    if (strBranchCode.Length > 3)
                        ddlBranch.SelectedValue = strBranchCode.Substring(2, 3);
                    else
                        ddlBranch.SelectedValue = strBranchCode;
                }
                if (ddlBranch.Items.Count > 0)
                {
                    strWhere = "BRANCHID like '%" + ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                }
                //Load nhom nguoi su dung
                ddlList = objDataAccess.FillDataToDropDownList("GROUPS",strWhere,
                ddlGroup,"GROUPNAME", "GROUPID", "GROUPNAME ASC", false);                
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
                string strBranch = "";
                Int32 iGroup = 0;

                if (ddlBranch.Items.Count > 0)
                    strBranch = ddlBranch.SelectedValue.ToString();
                if (ddlGroup.Items.Count>0)
                    iGroup =Convert.ToInt32(ddlGroup.SelectedValue);
                //Danh sach NSD chua thuoc nhom
                sv_dsData = objUser.GetAllUserGroup(iGroup,strBranch, false);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count < 0)
                {
                    strError = objUser.strError;
                    return false;
                }
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();
                //Danh sach NSD thuoc nhom
                if (ddlGroup.Items.Count > 0)
                {
                    sv_dsData.Tables.Clear();
                    sv_dsData = objUser.GetAllUserGroup(iGroup, strBranch, true);
                    if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count < 0)
                    {
                        strError = objUser.strError;
                        return false;
                    }
                    grvUserGroup.DataSource = sv_dsData.Tables[0];
                    grvUserGroup.DataBind();
                }               
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
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
                //Kiem tra theo tung ban ghi da chon
                int iGroupID = Convert.ToInt16(ddlGroup.SelectedValue);
                string sUserID;
                for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                {
                    CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                    sUserID = "";
                    if (chkbox.Checked == true)
                    {                            
                        sUserID = grvData.Rows[i].Cells[1].Text.ToString();
                        if (!objUser.AddUserToGroup(iGroupID, sUserID))
                        {
                            strError = objUser.strError;
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
                ViewGrid();                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
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
            try
            {
                if (this.ddlGroup.Items.Count <= 0)
                {
                    strError = "Nhập nhóm!";
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

        //Checkbox all trong Gridview grvUserGroup/////////////////////
        //Mo ta:        Checkbox all trong Gridview grvUserGroup
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void chkALL1_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvUserGroup.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvUserGroup.Rows[i].Cells[0].Controls[1];
                if (checkbox.Checked == true)
                    chkbox.Checked = true;
                else
                    chkbox.Checked = false;
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewGrid();            
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
                //Kiem tra theo tung ban ghi da chon
                int iGroupID = Convert.ToInt16(ddlGroup.SelectedValue);
                string sUserID;
                for (int i = 0; i <= grvUserGroup.Rows.Count - 1; i++)
                {
                    CheckBox chkbox = (CheckBox) grvUserGroup.Rows[i].Cells[0].Controls[1];
                    sUserID="";
                    if (chkbox.Checked== true)
                    {
                        //lblError.Text= grvUserGroup.Rows[i].Cells[1].Text;
                        sUserID=grvUserGroup.Rows[i].Cells[1].Text.ToString();
                        if (!objUser.DelUserToGroup(iGroupID, sUserID))
                        {
                            strError = objUser.strError;
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
                ViewGrid();
            }            
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        protected void grvUserGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvUserGroup.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Items.Count > 0)
            {
                strWhere = "BRANCHID like '%" + ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                //Load nhom nguoi su dung
                ddlList = objDataAccess.FillDataToDropDownList("GROUPS", strWhere,
                ddlGroup, "GROUPNAME", "GROUPID", "GROUPNAME ASC", false);
                ViewGrid(); 
            }
        }





















        //<HeaderTemplate>
        //    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" 
        //        OnCheckedChanged="Chk_CheckedChanged1" />
        //</HeaderTemplate>

        //int ID = Convert.ToInt32(((Label)row.Cells[0].Controls[1]).Text);


    }
}
