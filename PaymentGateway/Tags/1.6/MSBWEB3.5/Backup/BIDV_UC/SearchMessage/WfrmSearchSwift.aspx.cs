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
    public partial class WfrmSearchSwift : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsUser objUserName = new clsUser();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsCommon objCommon = new clsCommon();
        private clsReport objReport = new clsReport();
        private clsSearchSwift objSearchSwift = new clsSearchSwift();
        private string strWhere = "";                   //Menh de where

        private string strError = "";                   //Ghi loi         
        private string strBank = "";                    //Ngan hang gui        
        private string strBranchReceiver = "";          //Chi nhanh nhan
        private string strAmount = "";                  //So tien
        private string strCurrency = "";                //Loai tien
        private string strRef = "";                     //So Ref   
        private string strDepartment = "";              //So Ref   
        private string strMsgType = "";                 //Loai dien                
        private string strMsgDirection = "";            //Chieu dien  
        private string strPrintStatus = "";             //Trang thai in dien
        private string strBranchCode = "";              //Ma chi nhanh cua NSD login
        private string strSourceBranch = "";            //Ma chi nhanh tao/nhan dien
        private DataSet ds = new DataSet();
        private string strBrHo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string sMenuID = "";
                    int iShow;
                    Button btnAdd = new Button();
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

                //Tien te STATUS = 1 AND GWTYPE='SWIFT'
                objDataAccess.FillDataToDropDownList1("CURRENCY", " GWTYPE='SWIFT' AND STATUS = 1 ",
                    ddlCurrency, "CCYCD", "CCYCD", "CCYCD ASC", 3);                                
                //Chieu dien
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "GWTYPE = 'SWIFT' AND CDNAME='MSGDirection'",
                    ddlMsgDirection, "CONTENT", "CONTENT", "CONTENT ASC", false);
                //Trang thai in dien
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "CDNAME='PRINTSTS'",
                    ddlPrintSTS, "CONTENT", "CDVAL", "CDVAL ASC", true); 
                //Chi nhanh
                objDataAccess.FillDataToDropDownList("BRANCH", "", ddlBranchReceiver,
                    "SIBS_BANK_CODE", "SIBS_BANK_CODE", "SIBS_BANK_CODE ASC", true);
                //Phan he
                objDataAccess.FillDataToDropDownList("ALLCODE", 
                    "gwtype ='SYSTEM' AND cdname='Department'", ddlDepartement,
                    "CONTENT", "CONTENT", "CDVAL ASC", true);
                //DIEN DI
                if (ddlMsgDirection.Text.ToString().ToUpper() == "SIBS-SWIFT")
                {
                    //Kiem tra chi nhanh la H.O
                    strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                    strBrHo = objUser.GetBrHo();
                    if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                    {
                        ddlBranchReceiver.Enabled = false;
                        if (strBranchCode.Length > 3)
                            ddlBranchReceiver.SelectedValue = strBranchCode.Substring(2, 3);
                        else
                            ddlBranchReceiver.SelectedValue = strBranchCode;
                    }
                    ddlBranchReceiver.Visible = true;
                    lblBranch.Visible = true;
                    lblBranch.Text = "Chi nhánh gửi";
                    lblBank.Text = "Ngân hàng nhận";
                    lblMsgDirection.Text = "(Điện đi)";
                }
                //DIEN DEN
                else
                {
                    ////Kiem tra chi nhanh la H.O
                    //strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                    //if (strBranchCode.Length>3)
                    //    ddlBranchReceiver.SelectedValue = strBranchCode.Substring(2,3);
                    //else
                    //    ddlBranchReceiver.SelectedValue = strBranchCode;
                    ddlBranchReceiver.Visible = true;
                    lblBranch.Visible = true;
                    lblBranch.Text = "Chi nhánh nhận";
                    lblBank.Text = "Ngân hàng gửi";
                    lblMsgDirection.Text = "(Điện đến)";
                }
                //Tieng anh, viet
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "GWTYPE = 'SYSTEM' AND CDNAME='LANG'",
                    ddlLang, "CONTENT", "CDVAL", "CDVAL ASC", false); 
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
            bool isHO = false;          //Neu chi nhanh la H.O( Hoi so chinh)
            try
            {
                //Kiem tra du lieu 
                if (!CheckData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                //Kiem tra chi nhanh la H.O
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') == strBrHo)
                {
                    isHO = true;
                }   
                //So tien
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    strAmount = " and AMOUNT = " +
                        txtAmount.Text.ToString().Replace(",", "");
                }
                //Loai tien
                if (!string.IsNullOrEmpty(ddlCurrency.Text))
                {
                    if (ddlCurrency.SelectedItem.Text.ToString() == "")
                    {
                        strCurrency = " and trim(CCYCD) IS NULL";
                    }
                    else if (ddlCurrency.SelectedItem.Text.ToString() == "ALL")
                    {

                    }
                    else
                    {
                        strCurrency = " and upper(trim(CCYCD)) like '%" +
                            ddlCurrency.Text.Trim().ToUpper() + "%'";
                    }
                }
                //So Ref
                if (!string.IsNullOrEmpty(txtRef.Text))
                {
                    strRef = " and upper(Trim(FIELD20)) like '%" +
                        txtRef.Text.Trim().ToUpper() + "%'";
                }
                //Loai dien
                if (!string.IsNullOrEmpty(txtMsgType.Text))
                {
                    strMsgType = " and upper(Trim(MSG_TYPE)) like '%" +
                        txtMsgType.Text.Trim().ToUpper() + "%'";
                }
                
                //Chieu dien
                if (!string.IsNullOrEmpty(ddlMsgDirection.Text))
                {                    
                    strMsgDirection = " and upper(trim(MSG_DIRECTION)) like '%" +
                        ddlMsgDirection.SelectedValue.ToString().ToUpper() + "%'";                    
                }
                //Phan he
                if (!string.IsNullOrEmpty(ddlDepartement.Text))
                {
                    if (ddlDepartement.Text.Trim().ToString() != "ALL")
                    {
                        strDepartment = " and trim(DEPARTMENT) like '%" +
                            ddlDepartement.SelectedValue.ToString().ToUpper() + "%'";
                    }
                }
                //TRANG THAI IN DIEN
                if (!string.IsNullOrEmpty(ddlPrintSTS.Text))
                {
                    if (ddlPrintSTS.Text.Trim().ToString() != "ALL")
                    {
                        strPrintStatus = " and PRINT_STS = " + ddlPrintSTS.SelectedValue.ToString();
                    }
                }
                //DIEN DI
                if (ddlMsgDirection.Text.ToString().ToUpper() == "SIBS-SWIFT")
                {
                    if (!string.IsNullOrEmpty(txtBank.Text))
                    {
                        strBank = " and upper(Trim(BRANCH_B)) like '%" +
                            txtBank.Text.Trim().ToUpper() + "%'";
                    }
                    if (!string.IsNullOrEmpty(ddlBranchReceiver.Text))
                    {
                        if (ddlBranchReceiver.Text.ToString() != "ALL")
                        {
                            strBranchReceiver = " and upper(Trim(BRANCH_A)) like '%" +
                            ddlBranchReceiver.Text.ToString().ToUpper() + "%'";
                        }
                    }
                }
                //DIEN DEN
                else
                {
                    if (!string.IsNullOrEmpty(txtBank.Text))
                    {
                        strBank = " and upper(Trim(BRANCH_A)) like '%" +
                            txtBank.Text.Trim().ToUpper() + "%'";
                    }
                    if (!string.IsNullOrEmpty(ddlBranchReceiver.Text))
                    {
                        if (ddlBranchReceiver.Text.ToString() != "ALL")
                        {
                            strBranchReceiver = " and upper(Trim(BRANCH_B)) like '%" +
                            ddlBranchReceiver.Text.ToString().ToUpper() + "%'";
                        }
                    }
                }
              
                //Loc theo chi nhanh neu NSD khong thuoc H.O
                if (isHO == false)
                {
                    if (ddlMsgDirection.Text.ToString() == "SIBS-SWIFT")
                    {
                        strSourceBranch = " AND LPAD(TRIM(BRANCH_A),5,'0') LIKE '%" +
                        strBranchCode.PadLeft(5, '0') + "%'";
                    }
                    else
                    {
                        ////Cho phep cac chi nhanh co quyen xem 
                        ////dien cua chi nhanh khac
                        //strSourceBranch = " AND LPAD(TRIM(BRANCH_B),5,'0') LIKE '%" +
                        //strBranchCode.PadLeft(5, '0') + "%'";
                    }                    
                }
                //Phan he
                //strDepartment = " AND UPPER(TRIM(DEPARTMENT))='RM'";

                //Cau dieu kien where
                strWhere = strWhere + strAmount + strCurrency + strRef + strMsgType +
                      strMsgDirection + strPrintStatus + strBank + strDepartment + 
                      strBranchReceiver + strSourceBranch;
                ds = objSearchSwift.SearchAdvance(strWhere,                   
                    objCommon.g_FormatdateYYYYMMDD(txtFromDate.Text), 
                    objCommon.g_FormatdateYYYYMMDD(txtToDate.Text));
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
            SearchData();
            objForm.MessboxWeb(this.Page, strError);
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
                //Nhap tu ngay
                if (string.IsNullOrEmpty(txtFromDate.Text))
                {
                    strError = "Nhập giá trị từ ngày";
                    return false;
                }
                //Nhap den ngay
                if (string.IsNullOrEmpty(txtToDate.Text))
                {
                    strError = "Nhập giá trị đến ngày";
                    return false;
                }
                if (!objCommon.g_IsDate(txtFromDate.Text))
                {
                    strError = "Nhập giá trị từ ngày chưa đúng định dạng";
                    return false;
                }
                if (!objCommon.g_IsDate(txtToDate.Text))
                {
                    strError = "Nhập giá trị đến ngày chưa đúng định dạng";
                    return false;
                }
                //Tu ngay <= den ngay
                if (Convert.ToDateTime(objCommon.g_Formatdate(txtToDate.Text, true)) < Convert.ToDateTime(objCommon.g_Formatdate(txtFromDate.Text, true)))
                {
                    strError = "Nhập giá trị từ ngày < = đến ngày";
                    return false;
                }


                //Kiem tra dinh dang tien
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (!objCommon.g_IsCurrency(txtAmount.Text.ToString()))
                    {
                        strError = "Nhập số tiền chưa đúng định dạng";
                        return false;
                    }
                    strAmount = Convert.ToDecimal(txtAmount.Text).ToString("c");
                    txtAmount.Text = strAmount.Replace("$", "");
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string sReport = "";
                string strValues = "";
                string sArrayID = "";  
                string sMsg_ID="";
                string strMsgType1 = "";
                string strMsgType2 = "";
                string sUserID = "";
                string sUserName = "";
                int iNum = 0;

                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                sUserName = objUserName.GetUserNameByUserID(sUserID);

                for (int i = 0; i < grvData.Rows.Count ; i++)
                {
                    CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                    if (chkbox.Checked == true)
                    {
                        iNum = iNum + 1;
                        Label lblID = (Label)grvData.Rows[i].Cells[9].Controls[1];
                        //sMsg_ID = grvData.Rows[i].Cells[1].Text.ToString();
                        sMsg_ID = lblID.Text.ToString();
                        if (sArrayID.Length > 0)
                            sArrayID = sArrayID + "|" + sMsg_ID;
                        else
                            sArrayID = sArrayID + sMsg_ID;                        
                    }
                    if (strMsgType1 == "")
                        strMsgType1 = grvData.Rows[i].Cells[2].Text.ToString();                    
                    strMsgType2 = grvData.Rows[i].Cells[2].Text.ToString();
                    if (strMsgType2 != "")
                    {
                        if (strMsgType1 != strMsgType2)
                        {
                            strError = "Tìm kiếm theo loại điện khi in nhiều điện";
                            objForm.MessboxWeb(this.Page, strError);
                            return;
                        }
                    }
                }
                if (iNum > 0)
                {
                    strValues = "|" + sUserName + "|pCurContent";
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("sArrayID", sArrayID);
                    SessionHelper.Store("strValues", strValues);
                    //if (ddlDepartement.SelectedValue.ToString() == "RM" || 
                    //    ddlDepartement.SelectedValue.ToString() == "TR")
                    if (strMsgType1 == "MT700" || strMsgType1 == "MT701" || strMsgType1 == "MT721")
                    {
                        SessionHelper.Store("RN", "SWIFT_PRINT_TF");
                        //In dien
                        sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=SWIFT_PRINT_TF";
                        Response.Write("<script>window.open('" + sReport + "')</script>");                        
                    }
                    else
                    {
                        SessionHelper.Store("RN", "SWIFT_PRINT");
                        //In dien
                        sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=SWIFT_PRINT";
                        Response.Write("<script>window.open('" + sReport + "')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void ddlMsgDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DIEN DI
            if (ddlMsgDirection.Text.ToString().ToUpper() == "SIBS-SWIFT")
            {
                ddlBranchReceiver.Visible = true;
                lblBranch.Visible = true;
                lblBranch.Text = "Chi nhánh gửi";
                lblBank.Text = "Ngân hàng nhận";
                lblMsgDirection.Text = "(Điện đi)";
            }
            //DIEN DEN
            else
            {
                ////Kiem tra chi nhanh la H.O
                //strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //if (strBranchCode.Length > 3)
                //    ddlBranchReceiver.SelectedValue = strBranchCode.Substring(2, 3);
                //else
                //    ddlBranchReceiver.SelectedValue = strBranchCode;
                ddlBranchReceiver.Visible = true;
                lblBranch.Visible = true;
                lblBranch.Text = "Chi nhánh nhận";
                lblBank.Text = "Ngân hàng gửi";
                lblMsgDirection.Text = "(Điện đến)";
            }
            lblTotal.Text = "0";
            grvData.DataSource = null;
            grvData.DataBind();
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strAmount = "";
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (!objCommon.g_IsCurrency(txtAmount.Text.ToString()))
                    {
                        strError = "Nhập số tiền chưa đúng định dạng";
                        objForm.MessboxWeb(this.Page, strError);
                        return;
                    }
                    strAmount = Convert.ToDecimal(txtAmount.Text).ToString("c");
                    txtAmount.Text = strAmount.Replace("$", "");
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
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


                            //////<asp:TemplateField HeaderText="Tra soát">
                            //////    <ItemTemplate>
                            //////        <a href='../SearchMessage/WfrmViewSWIFT_TS.aspx?mn=2302&REFNO=<%# Eval("FIELD20") %>&vi=1'>
                            //////        Tra soát</a>
                            //////    </ItemTemplate>
                            //////    <HeaderStyle HorizontalAlign="Center" />
                            //////    <ItemStyle HorizontalAlign="Center" Width="50px" />
                            //////</asp:TemplateField>
    }
}
