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
    public partial class WfrmSetFunctionToGroup : System.Web.UI.Page
    {
        //Lop user
        private clsUserRight objUser = new clsUserRight();
        //Lop truy nhap du lieu
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private GROUPS_BO objGroups_BO = new GROUPS_BO();
        private clsForm objForm = new clsForm();
        private clsReport objReport = new clsReport();
        private clsCommon objComm = new clsCommon();
        private string strError = "";
        private string strBranch = "";
        private string strGWType = "";
        private Int32 iGroup = 0;
        private string strBranchCode = "";
        private DropDownList ddlList;
        private string strWhere = "";
        private string strBrHo = "";
        

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
                    objForm.PageLoad(sMenuID, btnAdd, btnSave, btnDel,out iShow);
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
                //Load kenh thanh toan
                ddlGWType = objDataAccess.FillDataToDropDownList("ALLCODE"," CDNAME ='GWTYPE' ", 
                    ddlGWType, "CONTENT","CONTENT", "CONTENT ASC", false);
                //Lay ma chi nhanh theo NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh KHAC H.O
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    ddlBranch.Enabled = false;
                    if (strBranchCode.Length > 3)
                        ddlBranch.SelectedValue = strBranchCode.Trim().Substring(2, 3);
                    else
                        ddlBranch.SelectedValue = strBranchCode.Trim();
                }
                if (ddlBranch.Items.Count > 0)
                {                    
                    strWhere = " BRANCHID LIKE '%" + ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                    //Load nhom nguoi su dung
                    ddlList = objDataAccess.FillDataToDropDownList("GROUPS", strWhere,
                        ddlGroup, "GROUPNAME", "GROUPID", "GROUPNAME ASC", false);                    
                }
                //View danh sach menu
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
                DataRow dr;

                if (ddlBranch.Items.Count > 0)
                    strBranch = ddlBranch.SelectedValue.ToString();
                if (ddlGroup.Items.Count > 0)
                    iGroup = Convert.ToInt32(ddlGroup.SelectedValue.Trim());
                if (ddlGWType.Items.Count > 0)
                    strGWType = ddlGWType.SelectedValue.ToString();                
                //Danh sach menu theo nhom
                sv_dsData = objUser.GetAllMenuForGroup(iGroup,strBranch,strGWType);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objUser.strError;
                    return false;
                }
                grvData.Controls.Clear();
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();
                //
                for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                {
                    //dr = sv_dsData.Tables[0].Rows[i];
                    if (grvData.Rows[i].Cells[1].ToString().Trim() != "")
                    {
                        dr = objComm.g_GetDatarow(sv_dsData, grvData.Rows[i].Cells[1].Text.ToString(), "MENUID");
                        if (dr != null)
                        {
                            //Kiem tra cac checkbox quyen
                            CheckBox chkbox1 = (CheckBox)grvData.Rows[i].Cells[3].Controls[1];
                            CheckBox chkbox2 = (CheckBox)grvData.Rows[i].Cells[4].Controls[1];
                            CheckBox chkbox3 = (CheckBox)grvData.Rows[i].Cells[5].Controls[1];
                            CheckBox chkbox4 = (CheckBox)grvData.Rows[i].Cells[6].Controls[1];
                            if (dr["ISINQUIRY"] != null && dr["ISINQUIRY"].ToString() != "" &&
                                Convert.ToInt16(dr["ISINQUIRY"]) == 1)
                            {
                                chkbox1.Checked = true;
                            }                            
                            if (dr["ISDELETE"] != null && dr["ISDELETE"].ToString() != "" &&
                                Convert.ToInt16(dr["ISDELETE"]) == 1)
                            {
                                chkbox2.Checked = true;
                            }
                            if (dr["ISINSERT"] != null && dr["ISINSERT"].ToString() != "" &&
                                Convert.ToInt16(dr["ISINSERT"]) == 1)
                            {
                                chkbox3.Checked = true;
                            }
                            if (dr["ISEDIT"] != null && dr["ISEDIT"].ToString() != "" &&
                                Convert.ToInt16(dr["ISEDIT"]) == 1)
                            {
                                chkbox4.Checked = true;
                            }
                            //Neu truong METHOD = 1, chi cho xem
                            if (dr["METHOD"]!=null && dr["METHOD"].ToString()!="" &&
                                Convert.ToInt16(dr["METHOD"]) == 1)
                            {
                                chkbox2.Visible = false;
                                chkbox3.Visible = false;
                                chkbox4.Visible = false;
                            }
                            //Neu truong METHOD = 2, cho xem va them moi
                            if (dr["METHOD"] != null && dr["METHOD"].ToString() != "" &&
                                Convert.ToInt16(dr["METHOD"]) == 2)
                            {
                                chkbox2.Visible = false;
                                //chkbox4.Visible = false;
                            }
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
                            CheckBox chkbox2 = (CheckBox)grvData.Rows[i].Cells[4].Controls[1];
                            CheckBox chkbox3 = (CheckBox)grvData.Rows[i].Cells[5].Controls[1];
                            CheckBox chkbox4 = (CheckBox)grvData.Rows[i].Cells[6].Controls[1];
                            int ichk1 = 0;
                            int ichk2 = 0;
                            int ichk3 = 0;
                            int ichk4 = 0;
                            string sMenuID = "";
                            sMenuID = grvData.Rows[i].Cells[1].Text;
                            if (chkbox1.Checked == true) { ichk1 = 1; }
                            if (chkbox2.Checked == true) { ichk2 = 1; }
                            if (chkbox3.Checked == true) { ichk3 = 1; }
                            if (chkbox4.Checked == true) { ichk4 = 1; }                          
                            
                            //Them moi
                            if (objUser.CheckPermissionGroup(iGroupID, sMenuID))
                            {
                                if (!objUser.AddPermissionGroup(iGroupID, sMenuID, ichk1,ichk2,ichk3,ichk4, true))
                                {
                                    strError = objUser.strError;
                                    break;
                                }
                            }
                            //Sua
                            else
                            {
                                //Xoa
                                if (ichk1 == 0 && ichk2 == 0 && ichk3 == 0 && ichk4 == 0)
                                {
                                    if (!objUser.DelGrantMenu(iGroupID, sMenuID))
                                    {
                                        strError = objUser.strError;
                                        break;
                                    }
                                }
                                ////Them moi lai
                                //else if (ichk1 == 1 && ichk2 == 1 && ichk3 == 1 && ichk4 == 1)
                                //{
                                //    if (!objUser.AddPermissionGroup(iGroupID, sMenuID, ichk1, ichk2, ichk3, ichk4, true))
                                //    {
                                //        strError = objUser.strError;
                                //        break;
                                //    }
                                //}
                                //Cap nhat lai
                                else
                                {
                                    if (!objUser.AddPermissionGroup(iGroupID, sMenuID, ichk1, ichk2, ichk3, ichk4, false))
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
            for (int i=0; i<grvData.Rows.Count;i++)
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

        //Checkbox all xem trong Gridview///////////////////////////////
        //Mo ta:        Checkbox all trong Gridview
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void chkALLDel_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvData.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[4].Controls[1];
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
        protected void chkALLAdd_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvData.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[5].Controls[1];
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
        protected void chkALLEdit_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            for (int i = 0; i < grvData.Rows.Count; i++)
            {
                CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[6].Controls[1];
                if (checkbox.Checked == true)
                    chkbox.Checked = true;
                else
                    chkbox.Checked = false;
            }
        }

        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
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
                //Dieu kien loc theo chi nhanh cua NSD
                strWhere = " BRANCHID LIKE '%" + ddlBranch.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                //Load nhom nguoi su dung
                ddlList = objDataAccess.FillDataToDropDownList("GROUPS", strWhere,
                    ddlGroup, "GROUPNAME", "GROUPID", "GROUPNAME ASC", false);
                ViewGrid();
            }
        }       

        protected void ddlGWType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGWType.Items.Count > 0)
            {                
                ViewGrid();
            }
        }
    }
}
