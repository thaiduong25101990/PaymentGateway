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
    public partial class WfrmSearchIBPS : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsCommon objCommon = new clsCommon();
        private clsReport objReport = new clsReport();
        private clsSearchIBPS objSearchIBPS = new clsSearchIBPS();
        private string strWhere = "";                   //Menh de where

        private string strError = "";                   //Ghi loi 
        private string strFromDate = "";                //Tu ngay
        private string strToDate = "";                  //Den ngay
        private string strCreater = "";                 //Ngan hang gui
        private string strSender = "";                  //Ngan hang gui
        private string strReceive = "";                 //Ngan hang nhan
        private string strRM = "";                      //So RM
        private string strGwTransNum = "";              //So but toan
        private string strTransNum = "";                //So hieu giao dich
        private string strAmount = "";                  //So tien
        private string strCurrency = "";                //Loai tien
        private string strPreTad = "";                  //Cong citad
        private string strTAD = "";                     //Cong citad
        private string strDepartment = "";              //Phan he
        private string strMsgDirection = "";            //Chieu dien
        private string strStatus = "";                  //Trang thai
        private string strStatusForward = "";           //Trang thai forward        
        private string strPrintStatus = "";             //Trang thai in dien
        private string strBranchCode = "";              //Ma chi nhanh cua NSD login
        private string strSourceBranch = "";            //Ma chi nhanh tao/nhan dien
        private string strMsg_src = "";                 //Chieu dien
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
                //Tien te STATUS = 1 AND GWTYPE='IBPS'
                objDataAccess.FillDataToDropDownList("CURRENCY", " GWTYPE='IBPS' AND STATUS = 1 ",
                    ddlCurrency, "CCYCD", "CCYCD", "CCYCD ASC", true);
                ddlCurrency.SelectedIndex = -1;
                //Cong citad
                //Kiem tra chi nhanh la H.O
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                objDataAccess.FillDataToDropDownList("TAD", "",
                        ddlPreTad, "TAD", "SIBS_CODE", "TAD ASC", true);
                objDataAccess.FillDataToDropDownList("TAD", "",
                        ddlTad, "TAD", "SIBS_CODE", "TAD ASC", true);
                strBrHo = objUser.GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') != strBrHo)
                {
                    ddlTad.Enabled = false;
                    if (strBranchCode.Trim().PadLeft(5, '0') == "00040")
                    {
                        ddlTad.SelectedValue = "ALL";
                    }
                    else
                    {
                        ddlTad.SelectedValue = strBranchCode.Trim().PadLeft(5, '0');
                    }
                }
                ////////if (ddlTad.Items.Count > 0)
                ////////{
                ////////    ddlTad.Style["Height"] = "120px";
                ////////    ddlTad.ControlStyle.Height = 100;
                ////////}                
                //Phan he
                objDataAccess.FillDataToDropDownList("ALLCODE", 
                    "UPPER(CDNAME)=UPPER('Department') AND GWTYPE='SYSTEM'",
                    ddlDepartment, "CONTENT", "CONTENT", "CONTENT ASC", true);
                //Chieu dien
                objDataAccess.FillDataToDropDownList("ALLCODE", 
                    "GWTYPE = 'IBPS' AND UPPER(CDNAME)=UPPER('MSGDirection')",
                    ddlMsgDirection, "CONTENT", "CONTENT", "CONTENT DESC", false);
                //Trang thai
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "GWTYPE = 'SYSTEM' AND CDNAME='STATUS'",
                    ddlStatus, "CONTENT", "CDVAL", "CONTENT ASC", true);
                //Trang thai forward
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "GWTYPE = 'IBPS' AND CDNAME='FWSTS'",
                    ddlStatusForward, "CONTENT", "CDVAL", "CONTENT ASC", true);
                //Trang thai in dien
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "CDNAME='PRINTSTS'",
                    ddlPrintSTS, "CONTENT", "CDVAL", "CDVAL ASC", true);
                //Nguon dien
                objDataAccess.FillDataToDropDownList("ALLCODE",
                    "CDNAME='MSG_SRC' AND GWTYPE='IBPS'",
                    ddlMsg_src, "CONTENT", "CDVAL", "CDVAL ASC", true); 
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
                //NGAN HANG tao dien
                if (!string.IsNullOrEmpty(txtCreater.Text))
                {
                    strCreater = " and upper(Trim(F07)) like '%" +
                        txtCreater.Text.Trim().ToUpper() + "%'";
                }
                //NGAN HANG GUI                
                if (!string.IsNullOrEmpty(txtSender.Text))
                {
                    strSender = " and upper(Trim(F21)) like '%" +
                        txtSender.Text.Trim().ToUpper() + "%'";
                }
                //NGAN HANG NHAN
                if (!string.IsNullOrEmpty(txtReceiver.Text))
                {
                    strReceive = " and upper(Trim(F22)) like '%" + 
                        txtReceiver.Text.Trim().ToUpper() + "%'";
                }
                //SO RM
                if (!string.IsNullOrEmpty(txtRM.Text))
                {
                    if (chkRM.Checked == true)
                    {
                        strRM = " and upper(LTrim(RM_NUMBER,'0000')) = '" +
                            txtRM.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        strRM = " and upper(Trim(RM_NUMBER)) like '%" +
                            txtRM.Text.Trim().ToUpper() + "%'";
                    }
                }
                //SO BUT TOAN
                if (!string.IsNullOrEmpty(txtGwTransNum.Text))
                {
                    if (chkGwTransNum.Checked == true)
                    {
                        strGwTransNum = " and TO_CHAR(GW_TRANS_NUM) = '" +
                        txtGwTransNum.Text + "'";
                    }
                    else
                    {
                        strGwTransNum = " and TO_CHAR(GW_TRANS_NUM) LIKE '%" + 
                        txtGwTransNum.Text + "%'";
                    }
                }
                //SO HIEU GIAO DICH
                if (!string.IsNullOrEmpty(txtTransNum.Text))
                {
                    if (chkTransNum.Checked == true)
                    {
                        strTransNum = " and TO_CHAR(TRANS_REF) = '" +
                             txtTransNum.Text + "'";
                    }
                    else
                    {
                        strTransNum = " and TO_CHAR(TRANS_REF) LIKE '%" +
                            txtTransNum.Text + "%'";
                    }
                }
                //SO TIEN
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    strAmount = " and AMOUNT = " +
                        txtAmount.Text.ToString().Replace(",", "");
                }
                //LOAI TIEN
                if (!string.IsNullOrEmpty(ddlCurrency.Text))
                {
                    if (ddlCurrency.Text.ToString() != "ALL")
                    {
                        strCurrency = " and upper(trim(CCYCD)) like '%" +
                            ddlCurrency.Text.Trim().ToUpper() + "%'";
                    }
                }
                //CONG PRETAD
                if (!string.IsNullOrEmpty(ddlPreTad.Text))
                {
                    if (ddlPreTad.Text.ToString() != "ALL")
                    {
                        strPreTad = " and lpad(trim(PRE_TAD),5,'0') like '%' || lpad(" +
                            ddlPreTad.SelectedValue.ToString() + ",5,'0') || '%'";
                    }
                }
                //CONG CITAD
                if (!string.IsNullOrEmpty(ddlTad.Text))
                {
                    if (ddlTad.Text.ToString() != "ALL")
                    {
                        strTAD = " and lpad(trim(TAD),5,'0') like '%' || lpad(" +
                            ddlTad.SelectedValue.ToString() + ",5,'0') || '%'";
                    }
                }
                //PHAN HE
                if (!string.IsNullOrEmpty(ddlDepartment.Text))
                {
                    if (ddlDepartment.Text.ToString() != "ALL")
                    {
                        strDepartment = " and upper(trim(DEPARTMENT)) like '%" +
                            ddlDepartment.SelectedValue.ToString().ToUpper() + "%'";
                    }
                }                
                //CHIEU DIEN
                if (!string.IsNullOrEmpty(ddlMsgDirection.Text))
                {
                    if (ddlMsgDirection.Text.ToString() != "ALL")
                    {
                        strMsgDirection = " and upper(trim(MSG_DIRECTION)) like '%" +
                            ddlMsgDirection.SelectedValue.ToString().ToUpper() + "%'";
                    }
                }               
                //TRANG THAI
                if (!string.IsNullOrEmpty(ddlStatus.Text))
                {
                    if (ddlStatus.Text.Trim().ToString() != "ALL")
                    {
                        strStatus = " and STATUS = " + ddlStatus.SelectedValue.ToString();
                    }
                }
                //TRANG THAI FORWARD
                if (!string.IsNullOrEmpty(ddlStatusForward.Text))
                {
                    if (ddlStatusForward.Text.Trim().ToString() != "ALL")
                    {
                        strStatusForward = " and FWSTS = " +
                             ddlStatusForward.SelectedValue.ToString();
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
                //TRANG THAI IN DIEN
                if (!string.IsNullOrEmpty(ddlMsg_src.Text))
                {
                    if (ddlMsg_src.Text.Trim().ToString() != "ALL")
                    {
                        strMsg_src = " and MSG_SRC = " + ddlMsg_src.SelectedValue.ToString();
                    }
                }
                //Loc theo chi nhanh neu NSD khong thuoc H.O
                if (isHO == false)
                {
                    //if (ddlMsgDirection.Text.ToString() == "ALL")
                    //{
                    //    strSourceBranch = " AND (LPAD(TRIM(SOURCE_BRANCH),5,'0') LIKE '%" +
                    //        strBranchCode.PadLeft(5, '0') + "%')";
                    //}
                    //else
                    //{
                    //    if (ddlMsgDirection.Text.ToString() == "SIBS-IBPS")
                    //    {
                    //        strSourceBranch = " AND LPAD(TRIM(SOURCE_BRANCH),5,'0') LIKE '%" +
                    //        strBranchCode.PadLeft(5, '0') + "%'";
                    //    }
                    //    else
                    //    {
                    //        strSourceBranch = " AND LPAD(TRIM(SOURCE_BRANCH),5,'0') LIKE '%" +
                    //        strBranchCode.PadLeft(5, '0') + "%'";
                    //    }
                    //}
                    //----------------------
                    //Moi bo ngay 28/05/2010
                    //strTAD = "";                    
                    //Moi bo ngay 28/05/2010
                    //----------------------
                    if (strBranchCode.Trim().PadLeft(5, '0') == "00040")
                    {
                        strSourceBranch = " AND LPAD(SOURCE_BRANCH,5,'0') LIKE '%" + strBranchCode.PadLeft(5, '0') + "%'";
                    }
                    else
                    {
                        strSourceBranch = " AND DECODE(LPAD(TAD,5,'0')," + strBranchCode.PadLeft(5, '0') +
                        ",LPAD(TAD,5,'0'),LPAD(SOURCE_BRANCH,5,'0')) LIKE '%" + strBranchCode.PadLeft(5, '0') + "%'";
                    }
                    //strSourceBranch = " AND LPAD(SOURCE_BRANCH,5,'0') LIKE '%" + strBranchCode.PadLeft(5, '0') + "%'";
                }
                else
                {
                    //CONG CITAD
                    if (!string.IsNullOrEmpty(ddlTad.Text))
                    {
                        if (ddlTad.Text.ToString() != "ALL")
                        {
                            //----------------------
                            //Moi bo ngay 28/05/2010
                            //strTAD = "";
                            //Moi bo ngay 28/05/2010
                            //----------------------
                            strSourceBranch = " AND DECODE(LPAD(TAD,5,'0')," + ddlTad.SelectedValue.Trim().PadLeft(5, '0') +
                            ",LPAD(TAD,5,'0'),LPAD(SOURCE_BRANCH,5,'0')) LIKE '%" + ddlTad.SelectedValue.Trim().PadLeft(5, '0') + "%'";
                        }
                    }
                }
                //Cau dieu kien where
                strWhere = strWhere + strFromDate + strToDate + strCreater + strSender + strReceive + 
                    strAmount + strCurrency + strPreTad + strTAD + strDepartment + strMsgDirection +
                    strStatus + strStatusForward + strPrintStatus + strMsg_src + strRM + 
                    strSourceBranch + strGwTransNum + strTransNum;                

                //Fill du lieu gridview                                
                ds = objSearchIBPS.SearchAdvance(strWhere,
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
                if (Convert.ToDateTime(objCommon.g_Formatdate(txtToDate.Text,true)) < Convert.ToDateTime(objCommon.g_Formatdate(txtFromDate.Text,true)))
                {
                    strError = "Nhập giá trị từ ngày < = đến ngày";
                    return false;
                }
                

                //Kiem tra ma NH gui, nhan, so tien, so RM la so
                if (!string.IsNullOrEmpty(txtSender.Text))
                {
                    if (!objCommon.g_IsNumeric(txtSender.Text.ToString()))
                    {
                        strError = "Nhập ngân hàng gửi là số";
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtReceiver.Text))
                {
                    if (!objCommon.g_IsNumeric(txtReceiver.Text.ToString()))
                    {
                        strError = "Nhập ngân hàng nhận là số";
                        return false;
                    }
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
                if (!string.IsNullOrEmpty(txtRM.Text))
                {
                    if (!objCommon.g_IsNumeric(txtRM.Text.Trim().ToString()))
                    {
                        strError = "Nhập số RM là số";
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string sReport = "";
                string strValues = "";
                string sArrayID = "";
                string sMsg_ID = "";
                int iNum = 0;

                for (int i = 0; i < grvData.Rows.Count; i++)
                {
                    CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                    if (chkbox.Checked == true)
                    {
                        iNum = iNum + 1;
                        Label lblID = (Label)grvData.Rows[i].Cells[9].Controls[1];
                        //sQuery_ID = grvData.Rows[i].Cells[1].Text.ToString();
                        sMsg_ID = lblID.Text.ToString();
                        if (sArrayID.Length > 0)
                            sArrayID = sArrayID + "|" + sMsg_ID;
                        else
                            sArrayID = sArrayID + sMsg_ID;
                    }                    
                }
                if (iNum > 0)
                {
                    strValues = "|pcurBM_IBPS_MSG";
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("sArrayID", sArrayID);
                    SessionHelper.Store("strValues", strValues);
                    if (ddlMsgDirection.SelectedValue.ToString() == "SIBS-IBPS")
                    {
                        SessionHelper.Store("RN", "IBPS_PRINT_1");
                        sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=IBPS_PRINT_1";
                    }
                    else
                    {
                        SessionHelper.Store("RN", "IBPS_PRINT_2");
                        sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=IBPS_PRINT_2";
                    }
                    //In dien                    
                    Response.Write("<script>window.open('" + sReport + "')</script>");
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
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

        protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

                            //////<!--
                            //////<asp:BoundField DataField="PRINT_STS" HeaderText="TT in điện">
                            //////    <HeaderStyle HorizontalAlign="Center" />
                            //////    <ItemStyle Width="60px"  HorizontalAlign="Center" />
                            //////</asp:BoundField>
                            //////-->
                            //////<!--
                            //////<asp:TemplateField HeaderText="Tra soát">
                            //////    <ItemTemplate>
                            //////        <a href='../SearchMessage/WfrmViewIBPS_TS.aspx?mn=2102&REFNO=<%# Eval("RM_NUMBER") %>&vi=1'>
                            //////        Tra soát</a>
                            //////    </ItemTemplate>
                            //////    <HeaderStyle HorizontalAlign="Center" />
                            //////    <ItemStyle HorizontalAlign="Center" Width="50px" />
                            //////</asp:TemplateField>
                            //////-->

    }
}
