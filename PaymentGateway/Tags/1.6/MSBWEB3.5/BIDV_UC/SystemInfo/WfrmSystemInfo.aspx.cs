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
using BIDVWEB.Business.Reports;
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.SystemInfo;
using BIDVWEB.Business.Web;

namespace BIDVWEB.BIDV_UC.SystemInfo
{
    public partial class WfrmSystemInfo : System.Web.UI.Page
    {              
        private clsSystemInfo objSysInfo = new clsSystemInfo();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private string strError = "";
        private clsCommon objCommon = new clsCommon();
        private clsUserRight objUser = new clsUserRight();
        private clsBranch objBranch = new clsBranch();
        private clsReport objReport = new clsReport();
        private REPORT_PARA_INFO objReport_Para_Info = new REPORT_PARA_INFO();
        private REPORT_PARA_BO objReport_Para_BO = new REPORT_PARA_BO();
        private string strBranchCode = "";
        private Int32 iIDEdit = 0;
        private string strBrHo = "";

        //Page_load
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
                //gv_isBool = false;

                lblError.Text = "";
                //Load chi nhanh ngan hang
                ddlList = objDataAccess.FillDataToDropDownList("BRANCH", "",
                    ddlBranch, "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE ASC ", true);                    
                if (ddlList != null)
                {
                    ddlBranch = ddlList;
                }
                if (ddlBranch.Items.Count > 0)
                {
                    //Chi cho phep chinh sua cac thong tin cua nhom
                    strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                    //Kiem tra chi nhanh la H.O
                    strBrHo = objUser.GetBrHo();
                    if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                    {
                        ddlBranch.SelectedValue = strBranchCode;
                        ddlBranch.Enabled = false;
                    }
                    if (ddlBranch.SelectedItem.Text.Trim().ToUpper() == "ALL")
                    {
                        lblBranch.Text = "Tất cả";
                    }
                    else
                    {
                        lblBranch.Text = objBranch.GetBranchName(ddlBranch.SelectedValue.ToString());
                    }
                }                

                //Load danh sach bao cao
                ddlList = null;
                ddlList = objDataAccess.FillDataToDropDownList("LIST_REPORT","ISSHOW = 1",
                    ddlReport,"REPORTNAME","ID_REPORT","ID_REPORT ASC",true);                    
                if (ddlList != null)
                {
                    ddlReport = ddlList;
                }
                if (ddlReport.Items.Count > 0)
                {
                    if (ddlReport.SelectedItem.Text.Trim().ToUpper() == "ALL")
                    {
                        lblReport.Text = "Tất cả";
                    }
                    else
                    {
                        lblReport.Text = objReport.GetTitleReport(ddlReport.SelectedItem.Text.Trim(), false);
                    }                    
                }
                //Load data
                if (!ViewGrid())
                {
                    return false;
                }    
                //Lay requetst 
                if (Request["ID"] != "" && Request["ID"] != null && Convert.ToInt16(Request["ID"]) != 0)
                {
                    ViewCurrentRecord(Convert.ToInt16(Request["ID"]));
                }                                         
                return true;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return false;
            }
        }


        //Ham view mot ban ghi//////////////////////////////////////
        //Mo ta:        Ham view mot ban ghi
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iUserID: Ma NSD
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private void ViewCurrentRecord(int iID)
        {
            try
            {
                DataSet sv_dsData = new DataSet();
                DataRow dr;

                //Get dataset chua thong tin PARAMETER
                sv_dsData = objSysInfo.GetParameterByID(iID);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objSysInfo.strError;
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                dr =sv_dsData.Tables[0].Rows[0];                
                if (ddlBranch.Items.Count > 0 && 
                    dr["SIBS_BANK_CODE"].ToString() != "" && dr["SIBS_BANK_CODE"] != null)
                {
                    ddlBranch.SelectedValue = dr["SIBS_BANK_CODE"].ToString();
                }
                if (ddlBranch.SelectedItem.Text.Trim().ToUpper() == "ALL")
                {
                    lblBranch.Text = "Tất cả";
                }
                else
                {
                    lblBranch.Text = objBranch.GetBranchName(ddlBranch.SelectedValue.ToString());
                }

                //Load lai du lieu
                ViewGrid();                
                
                if (ddlReport.Items.Count > 0 &&
                    dr["ID_REPORT"].ToString() != "" &&
                    dr["ID_REPORT"].ToString() != null)
                {
                    ddlReport.SelectedValue = dr["ID_REPORT"].ToString();
                }
                if (ddlReport.Items.Count > 0)
                {
                    if (ddlReport.SelectedItem.Text.Trim().ToUpper() == "ALL")
                    {
                        lblReport.Text = "Tất cả";
                    }
                    else
                    {
                        lblReport.Text = objReport.GetTitleReport(ddlReport.SelectedItem.Text.Trim(), false);
                    }                     
                }
                this.txtDes.Text = dr["DESCRIPTION"].ToString();
                this.txtTime.Text = dr["TIME"].ToString();
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return;
            }
        }

        //Add Parameter
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SystemInfo/WfrmSystemInfo.aspx?mn=1101");
        }

        //Save Parameter
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                
                //Kiem tra du lieu truoc khi cap nhat
                if (CheckData())
                {
                    //Sua
                    if (Request["ID"] != "" && Request["ID"] != null && Convert.ToInt16(Request["ID"]) != 0)
                    {
                        //if (objSysInfo.AddParameter(Convert.ToString(ddlBranch.SelectedValue),
                        //    Convert.ToInt16(ddlReport.SelectedValue), Convert.ToString(txtTime.Text),
                        //    txtDes.Text.ToString(), Convert.ToInt16(Request["ID"]), ddlReport.SelectedItem.Text.ToString()))
                        Save_SysInfo(false);
                    }
                    //Them moi
                    else
                    {
                        //if (objSysInfo.AddParameter(Convert.ToString(ddlBranch.SelectedValue),
                        //    Convert.ToInt16(ddlReport.SelectedValue), Convert.ToString(txtTime.Text),
                        //    txtDes.Text.ToString(), 0, ddlReport.SelectedItem.Text.ToString()))

                        
                        //////////////////////////////////////////////////////////////////////
                        //Chon tat ca cac chi nhanh///////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////
                        if (ddlBranch.SelectedItem.Text.ToUpper() == "ALL")
                        {                            
                            //----------------------------------------------------------------
                            //Chon tat ca cac bao cao-----------------------------------------
                            //----------------------------------------------------------------                         
                            if (ddlReport.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                for (int i = 1; i < ddlBranch.Items.Count; i++)
                                {                                    
                                    objReport_Para_Info.SIBS_BANK_CODE = ddlBranch.Items[i].Value.ToString();
                                    for (int j = 1; j < ddlReport.Items.Count; j++)
                                    {
                                        objReport_Para_Info.ID_REPORT = Convert.ToInt16(ddlReport.Items[j].Value.ToString());
                                        objReport_Para_Info.REPORTNAME = ddlReport.Items[j].Text.ToString();
                                        //Kiem tra neu da co thi khong them moi
                                        if (objSysInfo.CheckBranhID_ReportID(objReport_Para_Info.SIBS_BANK_CODE,
                                                Convert.ToInt16(objReport_Para_Info.ID_REPORT), 0, out iIDEdit))
                                        {
                                            //Them moi
                                            Save_SysInfo(true);
                                        }
                                        //Sua lai neu da ton tai
                                        else
                                        {
                                            objReport_Para_Info.ID = iIDEdit;
                                            //Sua
                                            Save_SysInfo(false);
                                        }
                                    }
                                }
                            }                            
                            //----------------------------------------------------------------
                            //Chon mot bao cao------------------------------------------------
                            //----------------------------------------------------------------
                            else
                            {
                                for (int i = 1; i < ddlBranch.Items.Count; i++)
                                {
                                    objReport_Para_Info.SIBS_BANK_CODE = ddlBranch.Items[i].Value.ToString();                                    
                                    //Kiem tra neu da co thi khong them moi
                                    if (objSysInfo.CheckBranhID_ReportID(objReport_Para_Info.SIBS_BANK_CODE,
                                            Convert.ToInt16(objReport_Para_Info.ID_REPORT), 0, out iIDEdit))
                                    {
                                        //Them moi
                                        Save_SysInfo(true);
                                    }
                                    //Sua lai neu da ton tai
                                    else
                                    {
                                        objReport_Para_Info.ID = iIDEdit;
                                        //Sua
                                        Save_SysInfo(false);
                                    }
                                }
                            }
                        }
                        //////////////////////////////////////////////////////////////////////
                        //Chon mot chi nhanh//////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////
                        else
                        {
                            //----------------------------------------------------------------
                            //Xet tung chi nhanh ---------------------------------------------
                            //----------------------------------------------------------------
                            if (ddlReport.SelectedItem.Text.ToUpper() == "ALL")
                            {                                
                                for (int j = 1; j < ddlReport.Items.Count; j++)
                                {
                                    objReport_Para_Info.ID_REPORT = Convert.ToInt16(ddlReport.Items[j].Value.ToString());
                                    objReport_Para_Info.REPORTNAME = ddlReport.Items[j].Text.ToString();
                                    //Kiem tra neu da co thi khong them moi
                                    if (objSysInfo.CheckBranhID_ReportID(objReport_Para_Info.SIBS_BANK_CODE,
                                            Convert.ToInt16(objReport_Para_Info.ID_REPORT), 0, out iIDEdit))
                                    {
                                        //Them moi
                                        Save_SysInfo(true);
                                    }
                                    //Sua lai neu da ton tai
                                    else
                                    {
                                        objReport_Para_Info.ID = iIDEdit;
                                        //Sua
                                        Save_SysInfo(false);
                                    }
                                }
                                
                            }
                            //----------------------------------------------------------------
                            //Mot chi nhanh---------------------------------------------------
                            //----------------------------------------------------------------
                            else
                            {
                                //Kiem tra neu da co thi khong them moi
                                if (objSysInfo.CheckBranhID_ReportID(objReport_Para_Info.SIBS_BANK_CODE,
                                        Convert.ToInt16(objReport_Para_Info.ID_REPORT), 0, out iIDEdit))
                                {
                                    //Them moi
                                    Save_SysInfo(true);
                                }
                                //Sua lai neu da ton tai
                                else
                                {
                                    objReport_Para_Info.ID = iIDEdit;
                                    //Sua
                                    Save_SysInfo(false);
                                }
                            }
                        }                        
                    }
                    strError = "Cập nhật thành công!";
                }
                ViewGrid();
                objForm.MessboxWeb(this.Page, strError);
                return;                
            }
            catch (Exception ex)
            {
                if (objSysInfo.strError != "")
                {
                    strError = objSysInfo.strError + ex.Message;
                }
                else
                {
                    strError = ex.Message;
                }
                objForm.MessboxWeb(this.Page, strError);
                return;
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
            int iID=0;

            if (string.IsNullOrEmpty(this.txtTime.Text))
            {
                strError = "Nhập thời gian!";
                return false;
            }
            else
            { 
                if (!objCommon.g_IsNumeric(txtTime.Text.ToString()))
                {
                    strError = "Thời gian phải là dạng số!";
                    return false;
                }                
            }
            if (this.ddlBranch.Items.Count <= 0)
            {
                strError = "Nhập chi nhánh!";
                return false;
            }            
            if (this.ddlReport.Items.Count <= 0)
            {
                strError = "Nhập danh sách báo cáo!";
                return false;
            }
            if (this.txtDes.Text.Trim() != "")
            {
                if (this.txtDes.Text.Length > 200)
                {
                    strError = "Nhập mô tả có độ dài <200!";
                    return false;
                }
            }
            if (Request["ID"] != "" && Request["ID"] != null && Convert.ToInt16(Request["ID"]) != 0)
            {
                if (ddlBranch.SelectedItem.Text.ToString().ToUpper() == "ALL")
                {
                    strError = "Chọn chi nhánh";
                    return false;
                }
                if (ddlReport.SelectedItem.Text.ToString().ToUpper() == "ALL")
                {
                    strError = "Chọn báo cáo";
                    return false;
                }
                iID = Convert.ToInt16(Request["ID"].ToString());
                objReport_Para_Info.ID = iID;
            }
            else
            {
                objReport_Para_Info.ID =0;
            }
            if (ddlBranch.SelectedItem.Text.ToUpper() != "ALL" &&
                ddlReport.SelectedItem.Text.ToUpper() != "ALL")
            {
                if (!objSysInfo.CheckBranhID_ReportID(Convert.ToString(ddlBranch.SelectedValue),
                    Convert.ToInt16(ddlReport.SelectedValue), iID, out iIDEdit))
                {
                    strError = objSysInfo.strError;
                    return false;
                }
            }

            //Cap nhat cac field cua doi tuong objReport_Para_Info
            objReport_Para_Info.DESCRIPTION = txtDes.Text.ToString();
            if (ddlReport.SelectedItem.Text.ToUpper() !="ALL")
            {
                objReport_Para_Info.ID_REPORT = Convert.ToInt16(ddlReport.SelectedValue);
                objReport_Para_Info.REPORTNAME=ddlReport.SelectedItem.Text.ToString();
            }
            else
            {
                objReport_Para_Info.ID_REPORT = 0;
                objReport_Para_Info.REPORTNAME="";
            }
            if (ddlBranch.SelectedValue != "")
            {
                objReport_Para_Info.SIBS_BANK_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                objReport_Para_Info.SIBS_BANK_CODE = "";
            }
            objReport_Para_Info.TIME=txtTime.Text.ToString();
            return true;
        }


        //Ham view thong tin /////////////////////////////////////////
        //Mo ta:        Ham view thong tin
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iReportID: Ma bao cao
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool ViewGrid()
        {
            lblError.Text = "";
            try
            {
                DataSet sv_dsData = new DataSet();
                if (ddlBranch.Items.Count <= 0)
                {
                    strError = "Nhập danh sách chi nhánh";
                    return false;
                }
                sv_dsData = objSysInfo.GetAllParameterSystem(ddlBranch.SelectedValue.ToString());
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objSysInfo.strError;
                    return false;
                }
                grvData.DataSource = null;
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();
                //txtDes.Text = "";
                //txtTime.Text="";
                return true;
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
            try
            {
                bool iBool = false;
                string sContent = "";
                string sOldValue = "";
                string sNewValue = "";
                if (grvData.Rows.Count > 0)
                {
                    //Kiem tra theo tung ban ghi da chon                    
                    int ID;
                    for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        ID = 0;
                        if (chkbox.Checked == true)
                        {
                            btnDel.Attributes.Remove("OnClick");
                            //Lay ID o cot 1
                            ID = Convert.ToInt16(grvData.Rows[i].Cells[1].Text.ToString());
                            //Noi dung 
                            sContent = "Delete, SysInfo";
                            //Gia tri moi
                            sNewValue = "";
                            //Gia tri cu: ID, Time, Description, 
                            sOldValue = grvData.Rows[i].Cells[1].Text.ToString() + "|" +
                                grvData.Rows[i].Cells[4].Text.ToString() + "|" +
                                grvData.Rows[i].Cells[5].Text.ToString();
                            //Ham ghi log
                            if (objUser.SaveUserLog(DateTime.Now, "", sContent, 1,
                                "Insert", "SYSINFO", sOldValue, sNewValue))
                            {                                
                                //Goi ham xoa
                                if (!objSysInfo.DeleteParameter(ID))
                                {
                                    strError = objSysInfo.strError;
                                    objForm.MessboxWeb(this.Page, strError);
                                    return;
                                }
                                else
                                    iBool = true;
                            } 
                        }
                    }
                    ViewGrid();
                    if (iBool == true)
                    {
                        strError = "Xóa thành công";
                        objForm.MessboxWeb(this.Page, strError);
                        return;
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

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Items.Count>0)
            {
                if (ddlBranch.SelectedItem.Text.Trim().ToUpper() == "ALL")
                {
                    lblBranch.Text = "Tất cả";
                }                
                else
                {
                    lblBranch.Text = objBranch.GetBranchName(ddlBranch.SelectedValue.ToString());
                }
                ViewGrid();
            }
        }

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReport.Items.Count > 0)
            {
                if (ddlReport.SelectedItem.Text.Trim().ToUpper() == "ALL")
                {
                    lblReport.Text = "Tất cả";
                }
                else
                {
                    lblReport.Text = objReport.GetTitleReport(ddlReport.SelectedItem.Text.Trim(), false);
                }                
            }
        }

        private void Save_SysInfo(bool isAdd)
        {
            string sContent = "";
            string sOldValue = "";
            string sNewValue = "";

            //Them moi
            if (isAdd == true)
            {
                //Thuc hien them moi
                if (objReport_Para_BO.AddREPORT_PARA(objReport_Para_Info) <= 0)
                {
                    strError = objSysInfo.strError;
                }
                //Gia tri moi
                sNewValue = Convert.ToString(ddlBranch.SelectedValue) + "|" +
                Convert.ToString(ddlReport.SelectedValue) + "|" +
                Convert.ToString(txtTime.Text) + "|" + txtDes.Text.ToString() + "|" +
                Convert.ToString(Request["ID"]);
            }
            //Sua
            else
            {
                //Sua
                if (objReport_Para_BO.UpdateREPORT_PARA(objReport_Para_Info) <= 0)
                {
                    strError = objSysInfo.strError;
                }
                //Gia tri moi
                sNewValue = Convert.ToString(objReport_Para_Info.SIBS_BANK_CODE) + "|" +
                Convert.ToString(objReport_Para_Info.ID_REPORT) + "|" +
                Convert.ToString(txtTime.Text) + "|" + txtDes.Text.ToString();
            }
            //Noi dung 
            sContent = "Insert,SysInfo";
            //Gia tri cu
            sOldValue = "";
            //Ham ghi log
            if (!objUser.SaveUserLog(DateTime.Now, "", sContent, 1,
                "Insert", "SYSINFO", sOldValue, sNewValue))
            {
                strError = objUser.strError;
            }
        }



    }
}
