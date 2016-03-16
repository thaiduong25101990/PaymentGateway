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
using BIDVWEB.Business.SearchMessage;
using BIDVWEB.Business.Web;

namespace BIDVWEB.BIDV_UC.SearchMessage
{
    public partial class WfrmVCB_TS : System.Web.UI.Page
    {
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsCommon objCommon = new clsCommon();
        private clsReport objReport = new clsReport();
        private VCB_TS_BO objVCB_TS_BO = new VCB_TS_BO();

        private string strWhere = "";                   //Menh de where
        private string strError = "";                   //Ghi loi 
        private string strDate = "";                    //Ngay tao  
        private string strQueryID = "";                 //So tham chieu  
        private string strStatus = "";                  //Trang thai
        private string strRefNo = "";                   //So tham chieu
        private string strSender = "";                  //Ngan hang gui
        private string strReceive = "";                 //Ngan hang nhan                
        private string strMsgDirection = "";            //Chieu dien
        private string strBranchCode = "";              //Ma chi nhanh cua NSD login
        private string strSourceBranch = "";            //Ma chi nhanh tao/nhan dien
        private string strSTSAPP = "";                  //Trang thai dien tra soat da duyet
        private DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string sMenuID = "";
                    int iShow;
                    //Button btnAdd = new Button();
                    Button btnSave = new Button();
                    Button btnDel = new Button();
                    
                    //Lay menu ID
                    sMenuID = Request["mn"];
                    //Goi ham phan quyen
                    objForm.PageLoad(sMenuID, btnAdd, btnSave, btnDel, out iShow);                    
                    //Neu NSD co quyen xem
                    if (iShow > 0)
                    {
                        //Load du lieu
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
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:              
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////
        private bool LoadData()
        {
            try
            {
                lblTotal.Text = "0";

                //Gan image anh calendar cho textbox txtDate
                objCommon.gs_SetDate(txtFromDate, img1);
                objCommon.gs_SetDate(txtToDate, img2);                  
                //Load ngay hien tai
                txtFromDate.Text = objCommon.GetSysDate();
                txtToDate.Text = objCommon.GetSysDate();
                //Trang thai
                objDataAccess.FillDataToDropDownList("ALLCODE", "CDNAME = 'TSSTS'",
                    ddlStatus, "CONTENT", "CDVAL", "CDVAL ASC", true);
                ddlStatus.SelectedIndex = -1;
                //Chi nhanh gui
                //Kiem tra chi nhanh la H.O
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                objDataAccess.FillDataToDropDownList("BRANCH", "SIBS_BANK_CODE='" + strBranchCode + "'",
                        ddlBrSend, "BRAN_NAME", "SIBS_BANK_CODE", "BRAN_NAME ASC", true);                
                //ddlBrSend.Enabled = false;                
                //Chi nhanh nhan
                objDataAccess.FillDataToDropDownList("BRANCH", "",
                    ddlBrReceive, "BRAN_NAME", "SIBS_BANK_CODE", "BRAN_NAME ASC", true);
                ddlBrReceive.SelectedValue = strBranchCode.Trim().PadLeft(3, '0');
                ddlBrReceive.Enabled = false;
                //Chieu dien
                objDataAccess.FillDataToDropDownList("ALLCODE", 
                    "UPPER(CDNAME)=UPPER('TSDirection')",
                    ddlMsgDirection, "CONTENT", "CDVAL", "CONTENT DESC", true);
                //Search
                SearchData();
                return true;
            }
            catch (Exception ex)
            { 
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
                return false;
            }
        }


        //Ham search dien//////////////////////////////////////////////
        //Mo ta:        Ham search dien
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        private void SearchData()
        {            
            try
            {
                //Kiem tra du lieu 
                if (!CheckData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                //Lay chi nhanh cua NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Ngay tao dien tra soat
                strDate = " CR_DATE>= '" + objCommon.g_FormatdateYYYYMMDD(txtFromDate.Text.ToString()) +
                    "' AND CR_DATE<= '" + objCommon.g_FormatdateYYYYMMDD(txtToDate.Text.ToString()) + "' ";
                //So tham chieu
                if (txtRefNo.Text.ToString()!="")
                    strRefNo = " AND REFNO LIKE '%" + txtRefNo.Text.ToString() + "%'";
                //NGAN HANG GUI                
                if (ddlBrSend.SelectedValue.ToString()!="ALL")
                {
                    strSender = " and BR_SEND = '" + ddlBrSend.SelectedValue.ToString() + "'";
                }
                //NGAN HANG NHAN
                if (ddlBrReceive.SelectedValue.ToString() != "ALL")
                {
                    strReceive = " and BR_RECEIVE = '" + ddlBrReceive.SelectedValue.ToString() + "'";
                }
                ////CHIEU DIEN
                //if (ddlMsgDirection.Text.ToString() != "ALL")
                //{
                //    strMsgDirection = " AND MSG_DIRECTION ='" +
                //        ddlMsgDirection.SelectedValue.ToString().ToUpper() + "'";
                //}           
                //TRANG THAI                
                if (ddlStatus.Text.Trim().ToString() != "ALL")
                {
                    strStatus = " and STATUS = " + ddlStatus.SelectedValue.ToString();
                }
                //Neu dien thuoc chi nhanh tao thi khong can duyet
                //dien thuoc chi nhanh nhan thi can duyet moi view dc
                if (ddlBrSend.SelectedValue.ToString() == "ALL")
                    strSTSAPP = " and STSAPP = 1"; //=1 da duyet
                //else
                //    strSTSAPP = " and NVL(STSAPP,0) = 0";
                
                //Cau dieu kien where
                strWhere = strWhere + strDate + strRefNo+ strSender + strReceive +
                    strMsgDirection + strStatus + strSourceBranch + strSTSAPP;

                //Fill du lieu gridview                                
                ds = objVCB_TS_BO.SearchAdvance(strWhere,out ds);
                if (ds != null)
                {                    
                    grvData.DataSource = ds.Tables[0];
                    grvData.DataBind();                    
                    lblTotal.Text = ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lblTotal.Text = "0";
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        //Kiem tra du lieu truoc khi search////////////////////////////
        //Mo ta:        Kiem tra du lieu truoc khi search
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        private bool CheckData()
        {
            try
            {
                //Tu ngay
                if (string.IsNullOrEmpty(txtFromDate.Text))
                {
                    strError = "Nhập giá trị từ ngày";
                    return false;
                }
                if (!objCommon.g_IsDate(txtFromDate.Text))
                {
                    strError = "Giá trị từ ngày chưa đúng định dạng";
                    return false;
                }
                //Den ngay
                if (string.IsNullOrEmpty(txtToDate.Text))
                {
                    strError = "Nhập giá trị đến ngày";
                    return false;
                }
                if (!objCommon.g_IsDate(txtToDate.Text))
                {
                    strError = "Giá trị đến ngày chưa đúng định dạng";
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../SearchMessage/WfrmViewVCB_TS.aspx?mn=" + Request["mn"].ToString());
            string strPage;
            strPage = "../SearchMessage/WfrmViewVCB_TS.aspx?mn=" + Request["mn"].ToString();
            Response.Write("<script>");
            Response.Write("window.open('" + strPage + "','_blank')");
            Response.Write("</script>");
        }

        //Ham thuc hien chuyen trang trong gridview////////////////////
        //Mo ta:        Ham thuc hien chuyen trang trong gridview
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void grvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvData.PageIndex = e.NewPageIndex;
            SearchData();
            objForm.MessboxWeb(this.Page, strError);
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

        protected void ddlBrSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Lay chi nhanh cua NSD
            strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
            if (ddlBrSend.SelectedValue.ToString() == "ALL")
            {
                ddlBrReceive.SelectedValue = strBranchCode.PadLeft(3, '0');
                ddlBrReceive.Enabled = false;
            }
            else
            {
                ddlBrReceive.SelectedValue = "ALL";
                ddlBrReceive.Enabled = true;
            }
            //Search
            SearchData();
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
        //<asp:BoundField DataField="MSG_DIRECTION" HeaderText="Chiều điện" >
        //    <HeaderStyle HorizontalAlign="Center" />
        //    <ItemStyle Width="70px" HorizontalAlign="Center"/>
        //</asp:BoundField>
    }
}
