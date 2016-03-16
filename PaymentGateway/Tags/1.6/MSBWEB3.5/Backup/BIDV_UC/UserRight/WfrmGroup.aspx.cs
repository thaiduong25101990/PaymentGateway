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
    public partial class WfrmGroup : System.Web.UI.Page
    {        
        private int iStatus = 0;
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();        
        private GROUPSInfo objGroups_Info = new GROUPSInfo();
        private GROUPS_BO objGroups_BO = new GROUPS_BO();
        private clsReport objReport = new clsReport();
        private string strError = "";
        private string strBranchCode = "";
        private string strBrHo = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string sMenuID = "";
                    int iShow;
                    lblError.Text = "";
                    sMenuID = Request["mn"];
                    objForm.PageLoad(sMenuID, btnAdd, btnSave, btnDel, out iShow);                    
                    //btnDel.Attributes.Add("OnClick", "return confirm(\"Bạn chắc chắn muốn xóa?\")");
                    //btnDel.Attributes.Add("OnClick", "return ConfirmDelete();");
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


        //Load du lieu len form////////////////////////////////////////////
        //Mota:         Load du lieu len form
        //Ngay tao:     07/2008
        //Nguoi tao:    All
        //Dau vao:      
        //Dau ra:       Load thanh cong        
        ///////////////////////////////////////////////////////////////////
        private bool LoadData()
        {
            try
            {
                DropDownList ddlList;
                lblError.Text = "";                     

                //Load branch
                //Load chi nhanh ngan hang
                ddlList = objDataAccess.FillDataToDropDownList("BRANCH", "", ddlBranch,
                        "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE ASC", true);
                //Lay ma chi nhanh theo NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh la H.O
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {                    
                    chkAdmin.Enabled = false;
                    ddlBranch.Enabled = false;
                    if (strBranchCode.Length > 3)
                        ddlBranch.SelectedValue = strBranchCode.Substring(2, 3);
                    else
                        ddlBranch.SelectedValue = strBranchCode; 
                }
                //Load ddlGwtype
                ddlList = objDataAccess.FillDataToDropDownList("GWTYPE", "GWTYPE NOT IN ('IQS')", ddlGwtype,
                            "GWTYPE", "GWTYPE", "GWTYPE", false);
                if (ddlList != null)
                {
                    ddlGwtype = ddlList;
                }
                else
                {
                    strError = "Chưa nhập kênh thanh toán";
                }
                ////Load ddlDepart
                //ddlList = objDataAccess.FillDataToDropDownList("ALLCODE", "CDNAME='Department'",
                //    ddlDepart, "CONTENT", "CONTENT", "ID", false);
                //if (ddlList != null)
                //{
                //    ddlDepart = ddlList;
                //}
                //else
                //{
                //    strError = "Chưa nhập phân hệ";
                //}
                //Load ddlSupervisor
                //ddlSupervisor = objDataAccess.FillDataToDropDownList("ALLCODE", "CDNAME='Roll'",
                //    ddlSupervisor, "CONTENT", "CDVAL", "CDVAL ASC", false);
                if (!ViewGrid())
                {
                    return false;
                }
                //Truong hop edit
                if (Request["GROUPID"] != null && Convert.ToInt16(Request["GROUPID"]) != 0)
                {
                    ViewCurrentRecord(Convert.ToInt16(Request["GROUPID"]));
                }               
                lblError.Text = strError;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        //Ham view thong tin group/////////////////////////////////////
        //Mo ta:        Ham view thong tin group
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iGroupID: Ma nhom
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private void ViewCurrentRecord(int iGroupID)
        {
            try
            {
                DataSet sv_dsData = new DataSet();
                DataRow dr;                

                sv_dsData = objUser.GetGroupByID(iGroupID);
                dr = sv_dsData.Tables[0].Rows[0];
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objUser.strError;
                }
                this.txtGroupName.Text = sv_dsData.Tables[0].Rows[0].Field<string>("GROUPNAME");
                if (chkAdmin.Enabled == true)
                {
                    if (dr["ISADMIN"] != null && dr["ISADMIN"].ToString() != "")
                    {
                        if (dr["ISADMIN"].ToString() == "1")
                            chkAdmin.Checked = true;
                    }
                    else
                        chkAdmin.Checked = false;
                }
                if (dr["GWTYPE"] != null && dr["GWTYPE"].ToString() != "")
                {
                    ddlGwtype.SelectedValue = dr["GWTYPE"].ToString();
                }
                //if (dr["DEPARTMENT"] != null && dr["DEPARTMENT"].ToString() != "")
                //{
                //    ddlDepart.SelectedValue = dr["DEPARTMENT"].ToString();
                //}
                //if (dr["ISSUPERVISOR"] != null && dr["ISSUPERVISOR"].ToString() != "")
                //{
                //    ddlSupervisor.SelectedValue = dr["ISSUPERVISOR"].ToString();
                //}
                if (dr["BRANCHID"] != null && dr["BRANCHID"].ToString() != "")
                {
                    if(dr["BRANCHID"].ToString().Length>3)
                        ddlBranch.SelectedValue = dr["BRANCHID"].ToString().Substring(2,3);
                    else
                        ddlBranch.SelectedValue = dr["BRANCHID"].ToString();
                }
                this.txtDes.Text = sv_dsData.Tables[0].Rows[0].Field<string>("DESCRIPTION");
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }
                     
        
        //Check du lieu truoc khi cap nhat/////////////////////////////
        //Mo ta:        Check du lieu truoc khi cap nhat
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iGroupID: Ma nhom
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool CheckData()
        {
            try
            {
                if (ddlGwtype.Items.Count <= 0)
                {
                    strError = "Nhập kênh thanh toán";
                    return false;
                }
                if (ddlBranch.Items.Count <= 0)
                {
                    strError = "Nhập chi nhánh";
                    return false;
                }
                ////if (ddlDepart.Items.Count <= 0)
                ////{
                ////    strError = "Nhập phân hệ!";
                ////    return false;
                ////}
                if (this.txtGroupName.Text.Trim() == "")
                {
                    strError = "Nhập tên nhóm!";
                    return false;
                }
                //iStatus = Convert.ToInt16(ddlSupervisor.SelectedValue);                
                //Kiem tra GroupName trung
                if (Request["GROUPID"] != "" && Request["GROUPID"] != null
                            && Convert.ToInt16(Request["GROUPID"]) != 0)
                {
                    if (!objUser.CheckGroupName(txtGroupName.Text.ToString().Trim(),
                        Convert.ToInt16(Request["GROUPID"])))
                    {
                        strError = objUser.strError;
                        return false;
                    }
                    objGroups_Info.GROUPID = Convert.ToInt16(Request["GROUPID"]);
                }
                else
                {
                    if (!objUser.CheckGroupName(txtGroupName.Text.ToString().Trim(), 0))
                    {
                        strError = objUser.strError;
                        return false;
                    }
                    objGroups_Info.GROUPID = 0;
                }
                objGroups_Info.DESCRIPTION = txtDes.Text.ToString();
                objGroups_Info.GROUPNAME = txtGroupName.Text.ToString();
                if (chkAdmin.Enabled == true)
                {
                    if (chkAdmin.Checked == true)
                        objGroups_Info.ISADMIN = 1;
                    else
                        objGroups_Info.ISADMIN = 0;
                }
                else
                    objGroups_Info.ISADMIN = 0;
                //Gan du lieu cho doi tuong objGroups_Info
                //if (ddlDepart.SelectedValue != "")
                //{
                //    objGroups_Info.DEPARTMENT = ddlDepart.SelectedValue.ToString();
                //}
                //else
                //{
                //    objGroups_Info.DEPARTMENT = "";
                //}
                if (ddlGwtype.SelectedValue != "")
                {
                    objGroups_Info.GWTYPE = ddlGwtype.SelectedValue.ToString();
                }
                else
                {
                    objGroups_Info.GWTYPE = "";
                }
                //if (ddlSupervisor.SelectedValue != "")
                //{
                //    objGroups_Info.ISSUPERVISOR = Convert.ToInt16(ddlSupervisor.SelectedValue.ToString());
                //}
                //else
                //{
                //    objGroups_Info.ISSUPERVISOR = 0;
                //}
                //Lay ma chi nhanh theo NSD
                if (ddlBranch.Items.Count > 0)
                {
                    if (ddlBranch.SelectedItem.Text.Trim().ToUpper() == "ALL")
                    {
                        strError = "Chọn chi nhánh";
                        return false;
                    }
                    objGroups_Info.BRANCHID = ddlBranch.SelectedValue.ToString().PadLeft(5, '0');
                }
                else
                {
                    strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                    objGroups_Info.BRANCHID = strBranchCode.PadLeft(5, '0');
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Save Data///////////////////////////////////////////////////
        //Mo ta:        Save Data
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iGroupID: Ma nhom
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckData())
                {
                    //Sua
                    if (Request["GROUPID"] != "" && Request["GROUPID"] != null 
                        && Convert.ToInt16(Request["GROUPID"]) != 0)
                    {
                        //if (objUser.AddGroup(txtGroupName.Text.ToString(), iStatus, ddlGwtype.SelectedValue.ToString(), 
                        //    ddlDepart.SelectedValue.ToString(), txtDes.Text.ToString(), Convert.ToInt16(Request["GROUPID"])))
                        if (objGroups_BO.UpdateGROUPS(objGroups_Info)>0)
                        {
                            strError = "Cập nhật thành công!";
                        }
                        else
                        {
                            strError = objUser.strError;
                        }
                    }
                    //Them moi
                    else
                    {
                        //if (objUser.AddGroup(txtGroupName.Text.ToString(), iStatus, ddlGwtype.SelectedValue.ToString(),
                        //    ddlDepart.SelectedValue.ToString(), txtDes.Text.ToString(), 0))
                        if (objGroups_BO.AddGROUPS(objGroups_Info) > 0)
                        {                            
                            ////Lay ma chi nhanh theo NSD
                            //strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                            ////
                            //string sUserID="";
                            //sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                            ////Kiem tra chi nhanh la H.O
                            //strBrHo = objUser.GetBrHo();
                            //if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                            //{
                            //    //Lay GroupID vua tao
                            //    DataSet ds = new DataSet();
                            //    int iGroupID = 0;
                            //    ds = objGroups_BO.GetGROUPNAME(objGroups_Info.GROUPNAME);
                            //    if (ds!=null && ds.Tables[0].Rows.Count>0)
                            //    {
                            //        iGroupID = Convert.ToInt32(ds.Tables[0].Rows[0]["GROUPID"].ToString());
                            //        //Add NSD hien tai vao nhom vau tao
                            //        if (!objUser.AddUserToGroup(iGroupID, sUserID))
                            //        {
                            //            strError = objUser.strError;
                            //            return;
                            //        }
                            //    }
                            //}
                            strError = "Thêm mới thành công!";
                        }
                        else
                        {
                            strError = objUser.strError;
                        }
                    }                    
                }
                objForm.MessboxWeb(this.Page, strError);
                ViewGrid();
            }
            catch(Exception ex)
            {  
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);               
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserRight/WfrmGroup.aspx?mn=1202");
        }

        //Ham view thong tin///////////////////////////////////////////
        //Mo ta:        Ham view thong tin
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool ViewGrid()
        {
            try
            {
                DataSet dsData = new DataSet();

                if (ddlGwtype.Items.Count > 0)
                {
                    dsData = objUser.GetAllGroup(ddlGwtype.SelectedValue.ToString(),
                        ddlBranch.SelectedValue.ToString());
                    if (dsData == null && dsData.Tables[0].Rows.Count <= 0)
                    {
                        strError = objUser.strError;
                        return false;
                    }
                    grvData.DataSource = dsData.Tables[0];
                    grvData.DataBind();
                    return true;
                }
                else
                {
                    strError = "Chưa nhập kênh thanh toán";
                    return false;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        //Ham xoa parameter////////////////////////////////////////////
        //Mo ta:        Ham xoa parameter
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void btnDel_Click(object sender, EventArgs e)
        {
            bool bCheck = false;

            try
            {
                if (grvData.Rows.Count > 0)
                {
                    //Kiem tra theo tung ban ghi da chon                    
                    int ID;
                    for (int i = 0; i < grvData.Rows.Count ; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        ID = 0;
                        if (chkbox.Checked == true)
                        {
                            btnDel.Attributes.Remove("OnClick");
                            //Lay ID o cot 1
                            ID = Convert.ToInt16(grvData.Rows[i].Cells[1].Text.ToString());
                            if (objUser.CheckUser_Group(ID.ToString(),true))
                            {
                                //Kiem tra ko cho xoa nhom admin
                                if (objGroups_BO.CHECK_GROUP_ADMIN(ID.ToString()) == 1)
                                {
                                    strError = "Không được xóa nhóm Admin";
                                    objForm.MessboxWeb(this.Page, strError);
                                    return;
                                }
                                //Goi ham xoa
                                objUser.DeleteGroup(ID);
                                bCheck = true;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(objUser.strError))
                                    strError = "Còn NSD đang thuộc nhóm";
                                else
                                    strError = objUser.strError;
                                objForm.MessboxWeb(this.Page, strError);
                                bCheck = false;
                                return;
                            }
                        }
                    }
                    if (bCheck == true)
                    {
                        strError = "Xóa dữ liệu thành công";
                    }
                    objForm.MessboxWeb(this.Page, strError);
                    ViewGrid();
                    ResetControl();
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
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

        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            ViewGrid();
        }

        protected void ddlGwtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGwtype.Items.Count > 0)
            {
                ViewGrid();
            }
        }

        private void ResetControl()
        {
            txtDes.Text = "";
            txtGroupName.Text="";
            if (chkAdmin.Enabled == true)
                chkAdmin.Checked = false;
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Items.Count > 0)
            {
                ViewGrid();
            }
        }


    }
}
