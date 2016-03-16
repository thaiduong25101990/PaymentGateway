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

namespace BIDVWEB.BIDV_UC.SystemInfo
{
    public partial class WfrmSystemPara : System.Web.UI.Page
    {        
        private SYSPARA_BO objSysPara_BO = new SYSPARA_BO();
        private SYSPARA_Info objSysPara_Info = new SYSPARA_Info();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();        
        private clsCommon objCommon = new clsCommon();
        private string strError = "";       


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
                return;
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
                lblError.Text = "";

                //Gan image anh calendar cho textbox txtValue
                objCommon.gs_SetDate(txtValue, img1);
                //Load danh sach kieu du lieu
                ddlList = objDataAccess.FillDataToDropDownList("ALLCODE", "CDNAME = 'DATATYPE'",
                    ddlType, "CONTENT", "CONTENT", "CONTENT ASC", false);
                if (ddlList != null)
                {                   
                    //ddlType;
                }
                else
                {
                    strError = "Chưa nhập kiểu dữ liệu";
                    return false;
                }
                if (ddlType.Items.Count > 0)
                {
                    if (ddlType.SelectedItem.Text.ToString().ToUpper() == "DATE")
                        img1.Visible = true;
                    else
                        img1.Visible = false;
                }                            
                if (!ViewGrid())
                {
                    return false;
                }
                //view mot ban ghi
                if (!string.IsNullOrEmpty(Request["ID"]))
                {
                    ViewCurrentRecord(Convert.ToInt64(Request["ID"].ToString()));                    
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return false;
            }
        }


        //Ham view thong tin tham so he thong//////////////////////////
        //Mo ta:        Ham view thong tin tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private void ViewCurrentRecord(long iID)
        {
            try
            {
                DataSet dsData = new DataSet();
                DataRow dr;
                
                //Get dataset chua thong tin user
                dsData = objSysPara_BO.GetSysParaByID(iID);
                if (dsData == null && dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = "Lỗi khi hiển thị tham số";
                    return;
                }
                dr = dsData.Tables[0].Rows[0];
                this.txtName.Text = dr["VARNAME"].ToString();
                this.txtName.Enabled = false;
                this.txtValue.Text = dr["VALUE"].ToString();
                this.ddlType.SelectedValue = dr["TYPE"].ToString();
                this.txtNote.Text = dr["NOTE"].ToString();
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return;
            }
        }

        //Add user
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemInfo/WfrmSystemPara.aspx?mn=1102");
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
            int iInt1;            

            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (CheckData())
                {                    
                    //Sua
                    if (!string.IsNullOrEmpty(Request["ID"]))
                    {                        
                        iInt1 = objSysPara_BO.UpdateSYS_PARA(objSysPara_Info);
                        if (iInt1 == 1)
                        {
                            strError = "Cập nhật thành công!";
                        }
                        else
                        {
                            strError = "Lỗi khi cập nhật";
                        }                        
                    }
                    //Them moi
                    else
                    {
                        iInt1 = objSysPara_BO.AddSYSPARA(objSysPara_Info);                     
                        if (iInt1 == 1)
                        {
                            strError = "Thêm mới thành công";
                        }
                        else
                        {
                            strError = "Lỗi khi thêm mới";
                        }                        
                    }                    
                }                
                ViewGrid();
                objForm.MessboxWeb(this.Page, strError);
            }
            catch (Exception ex)
            {
                if (strError != "")
                {
                    strError = strError + ex.Message;
                }
                else
                {
                    strError = ex.Message;
                }
                objForm.MessboxWeb(this.Page, strError);
            }
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
                DataSet dsData = new DataSet();

                dsData = objSysPara_BO.GetAllSysPara();
                if (dsData == null && dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = "Lỗi khi hiển thị các tham số";
                    return false;
                }
                grvData.DataSource = dsData.Tables[0];
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

        //Ham xoa sysvar
        protected void btnDel_Click(object sender, EventArgs e)
        {
            bool iBool = false;
           
            try
            {
                if (grvData.Rows.Count > 0)
                {
                    //Kiem tra theo tung ban ghi da chon                    
                    long iID;
                    for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        iID = 0;
                        if (chkbox.Checked == true)
                        {
                            btnDel.Attributes.Remove("OnClick");
                            //Lay ID o cot 1
                            iID = Convert.ToInt64(grvData.Rows[i].Cells[1].Text);
                            if (objSysPara_BO.CheckParaUsing(iID) == 1)
                            {
                                strError = "Tham số này đang được sử dụng";
                                objForm.MessboxWeb(this.Page, strError);
                                return;
                            }
                            if (objSysPara_BO.DelParaByID(iID) != 1)
                            {
                                strError = "Lỗi khi xóa dữ liệu";
                                objForm.MessboxWeb(this.Page, strError);
                                return;
                            }
                            else
                                iBool = true;
                        }
                    }
                    if (iBool==true)
                        strError = "Xóa dữ liệu thành công";
                    ViewGrid();
                    objForm.MessboxWeb(this.Page, strError);
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }


        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            ViewGrid();            
            objForm.MessboxWeb(this.Page, strError);
        }


        //Kiem tra du lieu truoc khi cap nhat//////////////////////////
        //Mo ta:        Kiem tra du lieu truoc khi cap nhat
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Check thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool CheckData()
        {                       
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strError = "Nhập tên";
                return false;
            }
            if (string.IsNullOrEmpty(this.txtValue.Text.Trim()))
            {
                strError = "Nhập giá trị";
                return false;
            }
            if (ddlType.Items.Count <= 0)
            {
                strError = "Nhập kiểu dữ liệu";
                return false;
            }
            if (string.IsNullOrEmpty(ddlType.Text.ToString()))
            {
                strError = "Chọn kiểu dữ liệu";
                return false;
            }
            if (ddlType.SelectedItem.Text.ToUpper() == "INT" ||
               ddlType.SelectedItem.Text.ToUpper() == "LONG")
            {
                if (!objCommon.g_IsNumeric(txtValue.Text.ToString()))
                {
                    strError = "Nhập giá trị là dạng số!";
                    return false;
                } 
            }            
            else if (ddlType.SelectedItem.Text.ToUpper() == "DATE")
            {                
                if (!objCommon.g_IsDate(txtValue.Text))
                {
                    strError = "Nhập giá trị định dạng Ngày/Tháng/Năm (DD/MM/YYYY)!"; 
                    return false;
                }
            }

            //Kiem tra trung Name
            if (!string.IsNullOrEmpty(Request["ID"]))
            {   //Sua
                long iID=0;
                iID = Convert.ToInt64(Request["ID"].ToString());
                if (objSysPara_BO.CheckParaByName(txtName.Text.ToString(), iID)==1)
                {
                    strError = "Tên tham số này đã có";
                    return false;
                }                
            }
            else
            {
                if (objSysPara_BO.CheckParaByName(txtName.Text.ToString(),0)==1)
                {
                    strError = "Tên tham số này đã có";
                    return false;
                }
            }    
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                objSysPara_Info.ID = Convert.ToInt64(Request["ID"].ToString());
            }
            objSysPara_Info.GWTYPE = "SYSTEM";
            objSysPara_Info.VARNAME = txtName.Text.ToString();
            objSysPara_Info.VALUE = txtValue.Text.ToString();
            objSysPara_Info.TYPE = ddlType.SelectedValue.ToString();
            objSysPara_Info.NOTE = txtNote.Text.ToString();
            return true;
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.Items.Count > 0)
            {
                if (ddlType.SelectedItem.Text.ToString().ToUpper() == "DATE")
                    img1.Visible = true;
                else
                    img1.Visible = false;
            }
        }



    }
}
