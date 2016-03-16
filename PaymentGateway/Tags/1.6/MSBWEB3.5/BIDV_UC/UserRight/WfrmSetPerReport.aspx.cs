
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
    public partial class WfrmSetPerReport : System.Web.UI.Page
    {
        //Lop user
        private clsUserRight objUser = new clsUserRight();
        //Lop truy nhap du lieu
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsReport objReport = new clsReport();
        private clsCommon objComm = new clsCommon();
        private string strError = "";
        private string strBranchCode = "";
        private string strBranch = "";
        private DropDownList ddlList;
        private string strWhere = "";
        private string strGwType = "";
        private string strBrHo = "";
        private Int32 iGroup = 0;
        

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
                    strWhere = " BRANCHID LIKE '%" + ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                    //Load nhom nguoi su dung
                    ddlList = objDataAccess.FillDataToDropDownList("GROUPS", strWhere, ddlGroup,
                                "GROUPNAME", "GROUPID", "GROUPNAME ASC", false);                    
                }                
                //Load kenh thanh toan
                ddlList = objDataAccess.FillDataToDropDownList("ALLCODE",
                    "CDNAME='GWTYPE' AND CONTENT<>'IQS'", ddlType,
                    "CONTENT", "CONTENT", "CDVAL ASC", false);
                if (ddlList != null)
                {
                    ddlType = ddlList;
                }
                else
                {
                    strError = "Chưa nhập kênh thanh toán";
                    return false;
                }
                //View danh sach user
                if (ddlGroup.Items.Count > 0)
                {
                    if (!ViewGrid())
                    {
                        return false;
                    }
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
                DataRow dr;

                if (ddlGroup.Items.Count <= 0 && ddlType.Items.Count <= 0)
                {
                    return false;
                }
                if (ddlGroup.Items.Count > 0)
                    iGroup = Convert.ToInt32(ddlGroup.SelectedValue.Trim());
                if (ddlType.Items.Count > 0)
                    strGwType = ddlType.SelectedValue.ToString();                
                //Danh sach menu theo nhom
                sv_dsData = objUser.GetGroup_Report(iGroup, strGwType);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objUser.strError;
                    return false;
                }
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();
                //
                for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                {
                    //dr = sv_dsData.Tables[0].Rows[i];
                    dr = objComm.g_GetDatarow(sv_dsData, grvData.Rows[i].Cells[1].Text.ToString(), "ID_REPORT");
                    if (dr != null)
                    {   //Kiem tra cac checkbox quyen
                        CheckBox chkbox1 = (CheckBox)grvData.Rows[i].Cells[3].Controls[1];
                        if (!string.IsNullOrEmpty(dr["ISINQUIRY"].ToString()) && 
                            Convert.ToInt16(dr["ISINQUIRY"]) == 1)
                        {
                            chkbox1.Checked = true;
                        }                    
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iCount = 0;
            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (CheckData())
                {
                    //Kiem tra theo tung ban ghi da chon
                    int iGroupID = Convert.ToInt16(ddlGroup.SelectedValue);
                    for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        if (chkbox.Checked == true)
                        {
                            CheckBox chkbox1 = (CheckBox)grvData.Rows[i].Cells[3].Controls[1];                            
                            int ichk1 = 0;                            
                            int iReportID = 0;
                            if (grvData.Rows[i].Cells[1].Text.Trim() != "")
                            {
                                iReportID = Convert.ToInt16(grvData.Rows[i].Cells[1].Text);
                            }                            
                            if (chkbox1.Checked == true) { ichk1 = 1; }                            

                            //Them moi
                            if (objUser.CheckPermissionReport(iGroupID, iReportID))
                            {
                                if (!objUser.AddPermissionReport(iGroupID, iReportID, ichk1, true))
                                {
                                    strError = objUser.strError;
                                    break;
                                }
                            }
                            //Sua
                            else
                            {
                                if (ichk1 == 1)
                                {
                                    //Them moi lai
                                    if (!objUser.AddPermissionReport(iGroupID, iReportID, ichk1, false))
                                    {
                                        strError = objUser.strError;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Xoa
                                    if (!objUser.DelGrantReport(iGroupID, iReportID))
                                    {
                                        strError = objUser.strError;
                                        break;
                                    }
                                }
                            }
                            iCount = iCount + 1;
                        }
                    }
                }
                if (iCount > 0)
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
        //Dau vao:      
        //Dau ra:       True: thanh cong, false: ko thanh cong
        ///////////////////////////////////////////////////////////////
        private bool CheckData()
        {
            try
            {
                if (this.ddlGroup.Items.Count <= 0)
                {
                    strError = "Nhập nhóm!";
                    objForm.MessboxWeb(this.Page, strError);
                    strError = "";
                    return false;
                }
                if (ddlType.Items.Count <= 0)
                {
                    strError = "Nhập kênh thanh toán";
                    objForm.MessboxWeb(this.Page, strError);
                    strError = "";
                    return false;
                }
                return true;
            }
            catch(Exception ex)
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

        //Checkbox all trong Gridview//////////////////////////////////
        //Mo ta:        Checkbox all trong Gridview
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

        //Checkbox all xem trong Gridview///////////////////////////////
        //Mo ta:        Checkbox all trong Gridview
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void chkALLView_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvData.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[3].Controls[1];
                if (checkbox.Checked == true)
                    chkbox.Checked = true;
                else
                    chkbox.Checked = false;
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ViewGrid())
            {
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ViewGrid())
            {
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Items.Count > 0)
            {
                strWhere = " BRANCHID LIKE '%" + ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                //Load nhom nguoi su dung
                ddlList = objDataAccess.FillDataToDropDownList("GROUPS", strWhere, ddlGroup,
                            "GROUPNAME", "GROUPID", "GROUPNAME ASC", false);
                ViewGrid();
            }
        }



















    }
}
