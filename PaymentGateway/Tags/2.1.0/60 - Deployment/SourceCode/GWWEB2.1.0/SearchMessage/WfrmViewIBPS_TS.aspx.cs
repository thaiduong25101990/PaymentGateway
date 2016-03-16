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
using BIDVWEB.Business.Web;
using BIDVWEB.Business.Reports;
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.SystemInfo;
using BIDVWEB.Business.SearchMessage;

namespace BIDVWEB.BIDV_UC.SearchMessage
{
    public partial class WfrmViewIBPS_TS : System.Web.UI.Page
    {
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsCommon objCommon = new clsCommon();
        private clsSearchIBPS objSearchIBPS = new clsSearchIBPS();
        private clsReport objReport = new clsReport();
        private IBPS_TS_Info objIBPS_TS_Info = new IBPS_TS_Info();
        private IBPS_TS_BO objIBPS_TS_BO = new IBPS_TS_BO();
        private clsUser objUser = new clsUser();
        private clsUserRight objRightUser = new clsUserRight();
     

        private string strError = "";
        private DataSet ds = new DataSet();
        private DataRow dr;
        private string strQuery_ID = "";
        private string strBranchCode = "";                          //Ma chi nhanh cua NSD login
        private string strUserID = "";                              //Ma NSD
        private string strvi = "";                                  //Ma vi
        private int iTel = 0;                                       //1 TellerID, 2 Suppervisor
        private string strID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string sMenuID = "";
                    int iShow;
                    bool isbool;
                    int iInquiry = 0;
                    int iDelete = 0;
                    int iInsert = 0;
                    int iEdit = 0;

                    Button btnAdd = new Button();
                    //Button btnSave = new Button();
                    Button btnDel = new Button();

                    //Lay menu ID
                    sMenuID = Request["mn"];
                    strQuery_ID = Request["ID"];
                    //Goi ham phan quyen
                    objForm.PageLoad(sMenuID, btnAdd, btnSave, btnDel, out iShow);
                    isbool = objRightUser.CheckPerForm(sMenuID, out iInquiry,
                        out iDelete, out iInsert, out iEdit);
                    if (iEdit == 0)
                    {
                        btnApprove.Enabled = false;
                        ddlStatus.Enabled = false;
                    }
                    else
                        btnApprove.Enabled = true;
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
            catch(Exception ex)
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
                string strWhere = "";
                DataSet dsData = new DataSet();
                DataRow dr;

                //Gan image anh calendar cho textbox txtDate
                objCommon.gs_SetDate(txtCreateDate, img1);                
                //Chi nhanh cua NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Trang thai
                objDataAccess.FillDataToDropDownList("ALLCODE", "CDNAME = 'TSSTS'",
                    ddlStatus, "CONTENT", "CDVAL", "CDVAL ASC", false);
                ddlStatus.SelectedValue ="1";                
                //Chieu dien
                objDataAccess.FillDataToDropDownList("ALLCODE", "CDNAME = 'TSDIRECTION'",
                    ddlDirection, "CONTENT", "CDVAL", "LSTORD ASC", false);                
                ddlDirection.Enabled = false;
                if (!string.IsNullOrEmpty(Request["vi"]))
                {
                    strvi = Request["vi"].ToString();
                    //Chi tiet
                    if (strvi == "0")
                    {
                        //Chi nhanh gui tra soat                                
                        objDataAccess.FillDataToDropDownList("VIEW_BRANCH", "",
                                ddlBrSend, "BRANCH_NAME", "SIBS_BANK_CODE", "BRANCH_NAME ASC", true);
                        ddlBrSend.Enabled = false;
                        ddlBrSend.SelectedValue = strBranchCode.Trim().PadLeft(3, '0');    
                    }
                    //Tra soat
                    else
                    {
                        //Chi nhanh gui tra soat                                
                        objDataAccess.FillDataToDropDownList("VIEW_BRANCH", "SIBS_BANK_CODE='" + strBranchCode + "'",
                                ddlBrSend, "BRANCH_NAME", "SIBS_BANK_CODE", "BRANCH_NAME ASC", true);
                        ddlBrSend.Enabled = false;
                        ddlBrSend.SelectedValue = strBranchCode.Trim().PadLeft(3, '0');
                    }
                }
                else
                {
                    //Chi nhanh gui tra soat                                
                    objDataAccess.FillDataToDropDownList("VIEW_BRANCH", "SIBS_BANK_CODE='" + strBranchCode + "'",
                            ddlBrSend, "BRANCH_NAME", "SIBS_BANK_CODE", "BRANCH_NAME ASC", true);
                    ddlBrSend.Enabled = false;
                    ddlBrSend.SelectedValue = strBranchCode.Trim().PadLeft(3, '0');
                }
                //Chi nhanh tra loi tra soat
                objDataAccess.FillDataToDropDownList("VIEW_BRANCH", "",
                    ddlBrReceive, "BRANCH_NAME", "SIBS_BANK_CODE", "BRANCH_NAME ASC", true);

                //Sua dien tra soat
                if (!string.IsNullOrEmpty(Request["ID"]))
                {
                    strWhere = Request["ID"].ToString();
                    dsData = objIBPS_TS_BO.ViewIBPS_TS(strWhere, out dsData);
                    txtCreateDate.Enabled = false;
                    txtRefNo.Enabled = false;
                    if (dsData != null)
                    { 
                        dr = dsData.Tables[0].Rows[0];
                        txtCreateDate.Text =objCommon.g_Formatdate(dr["CREATEDATE"].ToString(),true);
                        if (dr["BR_SEND"].ToString() != null)
                            ddlBrSend.SelectedValue = dr["BR_SEND"].ToString();
                        if (dr["BR_RECEIVE"].ToString() != null)
                            ddlBrReceive.SelectedValue = dr["BR_RECEIVE"].ToString();
                        if (dr["STATUS"].ToString() != null)
                            ddlStatus.SelectedValue = dr["STATUS"].ToString();
                        if (ddlStatus.SelectedValue.ToString() == "2")
                            btnApprove.Enabled = false;
                        if (dr["CONTENT_TS"].ToString() != null)
                            txtContent_TS.Text = dr["CONTENT_TS"].ToString();
                        if (dr["CONTENT"].ToString() != null)
                            txtContent.Text = dr["CONTENT"].ToString();
                        if (dr["ID"].ToString() != null)
                        {
                            txtID.Text = dr["ID"].ToString();
                            txtID_PARENT.Text = dr["ID"].ToString();
                        }                        
                        if (dr["IORDER"].ToString() != null)
                            txtIORDER.Text = dr["IORDER"].ToString();
                        if (dr["QUERY_ID"].ToString() != null)
                            txtQUERY_ID.Text = dr["QUERY_ID"].ToString();
                        if (dr["REFNO"].ToString() != null)
                            txtRefNo.Text = dr["REFNO"].ToString();
                        if (dr["CCYCD"].ToString() != null)
                            txtCCYCD.Text = dr["CCYCD"].ToString();
                        if (dr["AMOUNT"].ToString() != null)
                            txtAmount.Text = dr["AMOUNT"].ToString();
                        if (dr["TenDVYC"].ToString() != null)
                            txtDVYC.Text = dr["TenDVYC"].ToString();
                        if (dr["TenDVH"].ToString() != null)
                            txtDVH.Text = dr["TenDVH"].ToString();
                        if (dr["TKDVH"].ToString() != null)
                            txtTKDVH.Text = dr["TKDVH"].ToString();
                        if (dr["NHH"].ToString() != null)
                            txtNHH.Text = dr["NHH"].ToString();
                        if (dr["CNTD"] != null)
                            txtCNTD.Text = dr["CNTD"].ToString();
                        if (dr["CNND"] != null)
                            txtCNND.Text = dr["CNND"].ToString();
                        if (dr["SBT_ID"] != null)
                            txtSBT_ID.Text = dr["SBT_ID"].ToString();
                    }
                    if (!string.IsNullOrEmpty(Request["vi"]))
                    { 
                        strvi = Request["vi"].ToString();
                        //Chi tiet
                        if (strvi == "0")
                        {
                            txtContent_TS.Enabled = false;
                            ddlBrReceive.Enabled = false;
                            ddlStatus.Enabled = false;
                            btnSave.Enabled = false;
                            btnApprove.Enabled = false;                            
                        }
                        //Tra soat
                        else
                        {
                            txtContent_TS.Text = "";
                            //txtCreateDate.Text = objCommon.GetSysDate();
                            txtCreateDate.Enabled = false;
                            btnPrint.Enabled = false;
                        }
                    }
                }
                //Tao dien tra soat tu dien goc
                else if (!string.IsNullOrEmpty(Request["REFNO"]))
                {
                    txtCreateDate.Text = objCommon.GetSysDate();
                    txtRefNo.Text = Request["REFNO"].ToString();                    
                    FindByRM();
                    ddlStatus.Enabled = false;
                }
                //
                else
                {
                    txtCreateDate.Text = objCommon.GetSysDate();
                    txtIORDER.Text = "0";
                    txtID_PARENT.Text = "0";
                    ddlStatus.Enabled = false;
                    btnApprove.Enabled = false;
                    //ddlDirection.SelectedIndex = ;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                iTel = 1;
                if (CheckData())
                {
                    long sID;
                    if (txtID_PARENT.Text != "")
                    {
                        sID = Convert.ToInt64(txtID_PARENT.Text.ToString());
                        if (sID == 0) sID = -1;
                    }
                    else
                        sID = -1;
                    if (objIBPS_TS_BO.CheckSubMsg(sID) > 0)
                    {
                        strError = "Đã có 1 điện tra soát được tạo từ điện tra soát này!";
                        objForm.MessboxWeb(this.Page, strError);
                        return;
                    }
                    if (ddlStatus.SelectedValue.ToString() == "1")
                    {
                        if (objIBPS_TS_BO.Insert(objIBPS_TS_Info) > 0)
                        {
                            btnSave.Enabled = false;
                            btnPrint.Enabled = false;
                            txtContent.Text = objIBPS_TS_Info.CONTENT.ToString();
                            strError = "Thêm mới thành công!";
                        }
                        else
                        {
                            //strError = objUser.strError;
                        }
                    }
                    //////else
                    //////{
                    //////    if (objIBPS_TS_BO.Update(objIBPS_TS_Info) > 0)
                    //////    {
                    //////        strError = "Cập nhật trạng thái thành công!";
                    //////        btnSave.Enabled = false;
                    //////        ddlStatus.Enabled = false;
                    //////    }
                    //////    else
                    //////    {
                    //////        //strError = objUser.strError;
                    //////    }
                    //////}
                }
                objForm.MessboxWeb(this.Page, strError);             
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            FindByRM();
        }

        private void FindByRM()
        {
            try
            {
                DataSet dsFind = new DataSet();
                DataRow dr;
                if (txtRefNo.Text.ToString() != "")
                    dsFind = objIBPS_TS_BO.GetMSGByRM(txtRefNo.Text.ToString(), out dsFind);
                else
                    dsFind = null;
                if (dsFind != null && dsFind.Tables[0].Rows.Count > 0)
                {
                    dr = dsFind.Tables[0].Rows[0];
                    if (dr["ccycd"] != null)
                        txtCCYCD.Text = dr["ccycd"].ToString();
                    if (dr["AMOUNT"] != null)
                        txtAmount.Text = dr["AMOUNT"].ToString();
                    if (dr["TenDVYC"] != null)
                        txtDVYC.Text = dr["TenDVYC"].ToString();
                    if (dr["TenDVH"] != null)
                        txtDVH.Text = dr["TenDVH"].ToString();
                    if (dr["TKDVH"] != null)
                        txtTKDVH.Text = dr["TKDVH"].ToString();
                    if (dr["NHH"] != null)
                        txtNHH.Text = dr["NHH"].ToString();
                    if (dr["QUERY_ID"] != null)
                        txtQUERY_ID.Text = dr["QUERY_ID"].ToString();
                    if (dr["CNTD"] != null)
                        txtCNTD.Text = dr["CNTD"].ToString();
                    if (dr["CNND"] != null)
                        txtCNND.Text = dr["CNND"].ToString();
                }
                else
                {
                    objForm.MessboxWeb(this.Page, "Số tham chiếu không đúng!");
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                //string sReport = "WfrmViewIBPS_TS.aspx?mn=2102";
                Response.Redirect("../SearchMessage/WfrmIBPS_TS.aspx?mn=2102");
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
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
                if (txtRefNo.Text.ToString() == "")
                {
                    strError = "Nhập số tham chiếu";
                    return false;
                }
                else
                {
                    FindByRM();
                }
                if (txtQUERY_ID.Text.ToString()=="")
                {
                    strError = "Nhập số tham chiếu";
                    return false;
                }    
                if (txtCreateDate.Text.ToString()=="")
                {
                    strError = "Nhập ngày tạo";
                    return false;
                }
                if(!objCommon.g_IsDate(txtCreateDate.Text.ToString()))
                {
                    strError = "Nhập ngày báo cáo không đúng định dạng";
                    return false;
                }
                if (btnApprove.Enabled == true)
                {
                    if (iTel == 1)
                    {
                        if (ddlBrReceive.SelectedValue == "ALL")
                        {
                            strError = "Chọn chi nhánh nhận tra soát";
                            return false;
                        }
                        if (txtContent_TS.Text.ToString() == "")
                        {
                            if (ddlStatus.SelectedValue.ToString() == "1")
                            {
                                strError = "Nhập nội dung tra soát";
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (ddlBrReceive.SelectedValue == "ALL")
                    {
                        strError = "Chọn chi nhánh nhận tra soát";
                        return false;
                    }
                    if (txtContent_TS.Text.ToString() == "")
                    {
                        if (ddlStatus.SelectedValue.ToString() == "1")
                        {
                            strError = "Nhập nội dung tra soát";
                            return false;
                        }
                    }
                }
                if (iTel == 1)
                {                    
                    //if (ddlStatus.SelectedValue.ToString() != "1")
                    //{
                    //    strError = "Chọn lại trạng thái là active!";
                    //    return false;
                    //}
                    objIBPS_TS_Info.STSAPP = 0;
                }
                else
                {
                    //if (ddlStatus.SelectedValue.ToString() == "0")
                    //{
                    //    strError = "Chọn lại trạng thái là active!";
                    //    return false;
                    //}
                    objIBPS_TS_Info.STSAPP = 1;
                }
                //Chi nhanh cua NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                strUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();

                objIBPS_TS_Info.BR_RECEIVE = ddlBrReceive.SelectedValue.ToString();
                objIBPS_TS_Info.BR_SEND = ddlBrSend.SelectedValue.ToString();
                objIBPS_TS_Info.CONTENT_TS = txtContent_TS.Text.ToString();                
                if (txtIORDER.Text != "")
                    if (iTel==1)
                        objIBPS_TS_Info.IORDER = Convert.ToInt16(txtIORDER.Text.ToString()) + 1;
                    else
                        objIBPS_TS_Info.IORDER = Convert.ToInt16(txtIORDER.Text.ToString());
                else
                    objIBPS_TS_Info.IORDER = 1;
                //Tao tra soat
                if (iTel == 1)
                {
                    objIBPS_TS_Info.CONTENT =
                        objIBPS_TS_Info.IORDER.ToString().PadLeft(2, '0') + "/[Người tạo: " +
                        strUserID + "-" + DateTime.Now.ToString() + "] " + 
                        objIBPS_TS_Info.CONTENT_TS +  "\n" +
                        txtContent.Text.ToString();
                    objIBPS_TS_Info.KSV_SEND = "";
                    objIBPS_TS_Info.KSV_RECEIVE = "";
                }
                //Duyet tra soat
                else
                {
                    objIBPS_TS_Info.CONTENT = 
                        objIBPS_TS_Info.IORDER.ToString().PadLeft(2, '0') + "/[Người duyệt: " + 
                        strUserID + "-" + DateTime.Now.ToString() + "] " + 
                        txtContent.Text.ToString().Substring(2,txtContent.Text.ToString().Length-2);
                    //if (strBranchCode.PadLeft(5,'0') = )
                    objIBPS_TS_Info.KSV_SEND = strUserID;
                    objIBPS_TS_Info.KSV_RECEIVE = strUserID;
                }                
                objIBPS_TS_Info.CREATEDATE = Convert.ToDateTime(objCommon.g_Formatdate(txtCreateDate.Text.ToString(),true));
                objIBPS_TS_Info.UPDATEDATE = objIBPS_TS_Info.CREATEDATE;
                if (txtID.Text != "")
                {
                    objIBPS_TS_Info.ID = Convert.ToInt32(txtID.Text.ToString());
                    objIBPS_TS_Info.SBT_ID = txtSBT_ID.Text.ToString();
                }
                else
                {
                    objIBPS_TS_Info.ID = 0;
                    objIBPS_TS_Info.SBT_ID = objIBPS_TS_BO.GetSBT_ID_New();
                    txtSBT_ID.Text = objIBPS_TS_Info.SBT_ID;
                }
                if (txtID_PARENT.Text != "")
                    objIBPS_TS_Info.ID_PARENT = Convert.ToInt32(txtID_PARENT.Text.ToString());
                else
                    objIBPS_TS_Info.ID_PARENT = 0;
                objIBPS_TS_Info.MSG_DIRECTION = ddlDirection.SelectedValue.ToString();
                objIBPS_TS_Info.QUERY_ID =Convert.ToInt32(txtQUERY_ID.Text.ToString());
                objIBPS_TS_Info.REFNO = txtRefNo.Text.ToString();
                objIBPS_TS_Info.STATUS = Convert.ToInt16(ddlStatus.SelectedValue.ToString());
                
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                iTel = 2;
                if (CheckData())
                {
                    long sID;
                    if (txtID_PARENT.Text != "")
                    {
                        sID = Convert.ToInt64(txtID_PARENT.Text.ToString());
                        if (sID == 0) sID = -1;
                    }
                    else
                        sID = -1;                    
                    if (ddlStatus.SelectedValue.ToString() == "1")
                    {
                        if (objIBPS_TS_BO.CheckMsgApprove(sID) > 0)
                        {
                            strError = "Điện tra soát đã được duyệt!";
                            objForm.MessboxWeb(this.Page, strError);
                            return;
                        }
                        if (objIBPS_TS_BO.Insert(objIBPS_TS_Info) > 0)
                        {
                            btnApprove.Enabled = false;
                            btnSave.Enabled = false;
                            txtContent.Text = objIBPS_TS_Info.CONTENT.ToString();
                            strError = "Duyệt thành công!";
                        }                        
                    }
                    else
                    {
                        //Kiem tra dien tra soat da duyet cho closed
                        if (objIBPS_TS_BO.CheckMsgApprove(sID) > 0)
                        {
                            if (objIBPS_TS_BO.Update(objIBPS_TS_Info) > 0)
                            {
                                strError = "Closed điện tra soát!";
                                btnSave.Enabled = false;
                                ddlStatus.Enabled = false;
                            }
                        }
                        else
                        {
                            if (objIBPS_TS_BO.Insert(objIBPS_TS_Info) > 0)
                            {
                                btnApprove.Enabled = false;
                                btnSave.Enabled = false;
                                txtContent.Text = objIBPS_TS_Info.CONTENT.ToString();
                                strError = "Duyệt thành công!";
                            }
                        }
                    }
                }
                objForm.MessboxWeb(this.Page, strError);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        protected void txtContent_TS_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string sReport = "";
                string strValues = "";
                string sBranch = "";
                string sUserID = "";
                DataSet dsBranch = new DataSet();

                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                dsBranch = objUser.GetBranchByUserID(sUserID);
                sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                //Lay MSG_ID
                strID = Request["ID"].ToString();
                strValues = "IBPS|" + strID + "|" + sUserID + "|" + sBranch + "|RefCurGW_TS";
                //Luu danh sach gia tri vao session
                SessionHelper.Store("strValues", strValues);
                SessionHelper.Store("RN", "GW_TS");
                SessionHelper.Store("sArrayID", "");
                //In dien

                sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=GW_TS";
                Response.Write("<script>window.open('" + sReport + "')</script>");
                //Response.Redirect(sReport);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }
    }
}


