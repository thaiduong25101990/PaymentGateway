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

namespace BIDVWEB.BIDV_UC.Common
{
    public partial class WfrmListReport : System.Web.UI.Page
    {
        private string strError;        
        private clsListReport objListReport= new clsListReport();
        private clsDatatAccess objDataAccess = new clsDatatAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {   //Kiem tra load du lieu ban dau
                    if (!LoadData())
                    {
                        lblError.Text = strError;
                    }                    
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        //Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (CheckData())
                {
                    //Sua
                    if (Request["ID"] != "" && Request["ID"] != null)
                    {

                        if (objListReport.UpdateListReport(Convert.ToInt16(Request["ID"]),
                            Convert.ToInt16(Request["ID_REPORT"]), ddlBranch.SelectedValue.ToString(),
                            txtTitle.Text.ToString(),txtFunction1.Text.ToString(),
                            txtFunction2.Text.ToString(), txtFunction3.Text.ToString(),
                            txtFunction4.Text.ToString(), txtName1.Text.ToString(),
                            txtName2.Text.ToString(), txtName3.Text.ToString(), 
                            txtName4.Text.ToString(),false))
                        {
                            lblError.Text = "Cập nhật thành công!";
                        }
                        else
                        {
                            lblError.Text = objListReport.strError;
                        }
                    }
                    //Them moi
                    else
                    {
                        objListReport.UpdateListReport(0, Convert.ToInt16(Request["ID_REPORT"]),
                                ddlBranch.SelectedValue.ToString(),
                                txtTitle.Text.ToString(), txtFunction1.Text.ToString(),
                                txtFunction2.Text.ToString(), txtFunction3.Text.ToString(),
                                txtFunction4.Text.ToString(), txtName1.Text.ToString(),
                                txtName2.Text.ToString(), txtName3.Text.ToString(),
                                txtName4.Text.ToString(), true);
                    }
                }
                if (ddlBranch.Items.Count > 0)
                {
                    ViewGrid(ddlBranch.SelectedValue.ToString());
                }                
            }
            catch (Exception ex)
            {
                if (objListReport.strError != "")
                {
                    lblError.Text = objListReport.strError + ex.Message;
                }
                else
                {
                    lblError.Text = ex.Message;
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
            if (ddlBranch.Items.Count <= 0)
            {
                strError = "Chưa nhập chi nhánh!";
                return false;
            }
            if (this.txtTitle.Text.Trim() == "")
            {
                strError = "Nhập tiêu đề báo cáo!";
                return false;
            }
            if (this.txtFunction1.Text.Trim() == "")
            {
                strError = "Nhập tên chức vụ 1!";
                return false;
            }
            if (this.txtFunction2.Text.Trim() == "")
            {
                strError = "Nhập tên chức vụ 2!";
                return false;
            }
            if (this.txtFunction1.Text.Trim() == "")
            {
                strError = "Nhập tên người ký 1!";
                return false;
            }
            if (this.txtFunction2.Text.Trim() == "")
            {
                strError = "Nhập tên người ký 2!";
                return false;
            }            
            return true;
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
                lblError.Text = "";
                DropDownList ddlList;
                
                //Load chi nhanh ngan hang
                ddlList = objDataAccess.FillDataToDropDownList("BRANCH","STATUS = 1",ddlBranch,
                    "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE", false);                    
                if (ddlList != null)
                {
                    ddlBranch = ddlList;
                }
                else
                {
                    strError = "Chưa nhập chi nhánh!";
                }
                                
                if (ddlBranch.Items.Count > 0)
                {
                    if (!ViewGrid(ddlBranch.SelectedValue.ToString()))
                    {
                        return false;
                    }
                }               


                if (Request["ID"] != "" && Request["ID"] != null)
                {
                    ViewCurrentRecord(Convert.ToInt16(Request["ID"]));
                }                
                else
                {
                    if (grvData.Rows.Count > 0)
                    {
                        ViewCurrentRecord(Convert.ToInt16(grvData.Rows[0].Cells[0].Text));
                    }                                                          
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


        //Ham view thong tin///////////////////////////////////////////
        //Mo ta:        Ham view thong tin
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iReportID: Ma bao cao
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool ViewGrid(string sBankCode)
        {
            try
            {
                DataSet sv_dsData = new DataSet();

                //Xoa grvData                
                grvData.DataSource = null;
                grvData.DataBind();
                
                sv_dsData = objListReport.GetListReportBrach(sBankCode);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objListReport.strError;
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
        

        //Ham view thong tin bao cao///////////////////////////////////
        //Mo ta:        Ham view thong tin bao cao
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iReportID: Ma bao cao
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private void ViewCurrentRecord(int iID)
        {
            try
            {
                DataSet sv_dsData = new DataSet();
                //gv_isBool = false;

                txtTitle.Text = "";
                txtName1.Text = "";
                txtName2.Text = "";
                txtName3.Text = "";
                txtName4.Text = "";
                txtFunction1.Text = "";
                txtFunction2.Text = "";
                txtFunction3.Text = "";
                txtFunction4.Text = "";

                //Get dataset chua thong tin user
                sv_dsData = objListReport.GetReportByID(iID);
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objListReport.strError;
                }
                if (sv_dsData.Tables[0].Rows[0].Field<string>("TITLE") != "")
                {
                    this.txtTitle.Text = sv_dsData.Tables[0].Rows[0].Field<string>("TITLE");
                }
                else
                {
                    this.txtTitle.Text = sv_dsData.Tables[0].Rows[0].Field<string>("TITLE1");
                }                
                this.txtFunction1.Text = sv_dsData.Tables[0].Rows[0].Field<string>("FUNCTION1");
                this.txtFunction2.Text = sv_dsData.Tables[0].Rows[0].Field<string>("FUNCTION2");
                this.txtFunction3.Text = sv_dsData.Tables[0].Rows[0].Field<string>("FUNCTION3");
                this.txtFunction4.Text = sv_dsData.Tables[0].Rows[0].Field<string>("FUNCTION4");
                this.txtName1.Text = sv_dsData.Tables[0].Rows[0].Field<string>("USER1");
                this.txtName2.Text = sv_dsData.Tables[0].Rows[0].Field<string>("USER2");
                this.txtName3.Text = sv_dsData.Tables[0].Rows[0].Field<string>("USER3");
                this.txtName4.Text = sv_dsData.Tables[0].Rows[0].Field<string>("USER4");                
            }
            catch (Exception ex)
            {
                if (objListReport.strError != "" && objListReport.strError != null)
                {
                    strError = objListReport.strError + ex.Message;
                }
                else
                {
                    strError = ex.Message;
                }                
            }
        }
        

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Common/WfrmListReport.aspx");
        }

        //Change 
        protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ViewGrid(ddlBranch.SelectedValue.ToString());            
            if (grvData.Rows.Count > 0)
            {
                if (grvData.Rows[0].Cells[0].Text.Trim() != "" 
                    && grvData.Rows[0].Cells[0].Text.Trim() !=null)
                {
                    //ViewCurrentRecord(Convert.ToInt16(grvData.Rows[0].Cells[0].Text));
                }
            }
            lblError.Text = strError;
        }
                




    }
}
