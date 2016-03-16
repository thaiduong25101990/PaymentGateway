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
    public partial class WfrmViewMsgIBPS : System.Web.UI.Page
    {
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsCommon objCommon = new clsCommon();
        private clsSearchIBPS objSearchIBPS = new clsSearchIBPS();
        private clsUser objUser = new clsUser();
        private string strError = "";
        private DataSet ds = new DataSet();
        private DataRow dr;
        private string strMsg_ID = "";

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
                    strMsg_ID = Request["ID"];
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
                ds = objSearchIBPS.GetMsgIBPSByID(strMsg_ID);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string strAmount = "";

                    dr = ds.Tables[0].Rows[0];
                    if (dr["TRANS_CODE"]!=null)
                        txtTransCode.Text = dr["TRANS_CODE"].ToString();
                    if (dr["TRANSCODE_DES"] != null)
                        lblDes.Text = dr["TRANSCODE_DES"].ToString();
                    if (dr["GW_TRANS_NUM"] != null)
                        txtRefNo.Text = dr["GW_TRANS_NUM"].ToString();
                    if (dr["NHGUI"] != null)
                        txtSender.Text = dr["NHGUI"].ToString();
                    if (dr["NHNG"] != null)
                        lblSender.Text = dr["NHNG"].ToString();
                    if (dr["NHNHAN"] != null)
                        txtReceiver.Text = dr["NHNHAN"].ToString();
                    if (dr["NHNN"] != null)
                        lblReceiver.Text = dr["NHNN"].ToString();
                    if (dr["AMOUNT"] != null && dr["AMOUNT"].ToString().Trim()!="")
                    {
                        strAmount = Convert.ToDecimal(dr["AMOUNT"]).ToString("c");
                        txtAmount.Text = strAmount.Replace("$", "");                        
                    }
                    if (dr["CCYCD"] != null)
                        lblCCY.Text = dr["CCYCD"].ToString();
                    if (dr["RM_NUMBER"] != null && dr["RM_NUMBER"].ToString()!="")
                    {
                        if (dr["RM_NUMBER"].ToString().Substring(0, 4) == "0000")
                            txtRM.Text = dr["RM_NUMBER"].ToString().Substring(4, dr["RM_NUMBER"].ToString().Length - 4);
                        else
                            txtRM.Text = dr["RM_NUMBER"].ToString();
                    }
                    if (dr["TRANS_DATE"] != null)
                    {
                        txtTransDate.Text = dr["TRANS_DATE"].ToString();
                    }
                    if (dr["DEPARTMENT"] != null)
                        txtDepartment.Text = dr["DEPARTMENT"].ToString();                    
                    if (dr["TRANGTHAI"] != null)
                        txtStatus.Text = dr["TRANGTHAI"].ToString();
                    //Trang thai in dien
                    if (dr["PRINT_STS"] != null)
                        txtPrintSTS.Text = dr["PRINT_STS"].ToString();
                    if (dr["SENDING_TIME"] != null)
                        txtSendingTime.Text = dr["SENDING_TIME"].ToString();
                    if (dr["RECEIVING_TIME"] != null)
                        txtReceivingTime.Text = dr["RECEIVING_TIME"].ToString();
                    if (dr["PRETRAN_CODE"] != null)
                        txtOldTransCode.Text = dr["PRETRAN_CODE"].ToString();
                    if (dr["PRETRANS_NUM"] != null)
                        txtOldGwTranNum.Text = dr["PRETRANS_NUM"].ToString();
                    //Chieu dien
                    if (dr["MSG_DIRECTION"] != null)
                        txtMsgDirection.Text = dr["MSG_DIRECTION"].ToString();                    
                    //CHUA MAP
                    if (dr["TELLERID"] != null)
                        txtTeller.Text = dr["TELLERID"].ToString();                    
                    if (dr["TRANGTHAIFORWARD"] != null)
                        txtForwardStatus.Text = dr["TRANGTHAIFORWARD"].ToString();
                    if (dr["FWTIME"] != null)
                        txtForwardTime.Text = dr["FWTIME"].ToString();
                    if (dr["TENNG"] != null)
                        txtSenderName.Text = dr["TENNG"].ToString();
                    if (dr["DCNG"] != null)
                        txtSenderAddress.Text = dr["DCNG"].ToString();
                    if (dr["TKNG"] != null)
                        txtSenderAccount.Text = dr["TKNG"].ToString();
                    if (dr["NHNG"] != null)
                        txtSenderBank.Text = dr["NHNG"].ToString();
                    if (dr["TENNN"] != null)
                        txtReceiverName.Text = dr["TENNN"].ToString();
                    if (dr["DCNN"] != null)
                        txtReceiverAddress.Text = dr["DCNN"].ToString();
                    if (dr["TKNN"] != null)
                        txtReceiverAcount.Text = dr["TKNN"].ToString();
                    if (dr["NHNN"] != null)
                        txtReceiverBank.Text = dr["NHNN"].ToString();
                    if (dr["CONTENT"] != null)
                    {
                        txtContent.Text = dr["CONTENT"].ToString();                        
                    }
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {                
                string sReport = "";
                string strValues = "";
                string sBranch = "";
                string sUserID = "";
                string sUserName = "";
                DataSet dsBranch = new DataSet();

                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                dsBranch = objUser.GetBranchByUserID(sUserID);
                sUserName = objUser.GetUserNameByUserID(sUserID);
                sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                //Lay MSG_ID
                strMsg_ID = Request["ID"].ToString();
                ds = objSearchIBPS.GetMsgIBPSByID(strMsg_ID);
                if (txtMsgDirection.Text.ToString() == "SIBS-IBPS")
                {
                    strValues = sBranch + "|" + Request["ID"] + "|" + sUserName + "|pcurBM_IBPS_MSG";
                    //strValues = ds.Tables[0].Rows[0]["QUERY_ID"].ToString() + "|RefCurBM_IBPS01";                
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("strValues", strValues);
                    SessionHelper.Store("RN", "IBPS_PRINT_1");
                    SessionHelper.Store("sArrayID", "");
                    //In dien                                
                    //sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=BM_IBPS01";
                    sReport = "~/ViewReport/WfrmViewCrystalReport.aspx?ReportKind=IBPS_PRINT_1";
                    //Response.Write("<script>window.open('" + sReport + "')</script>");
                    //<a href='../SearchMessage/WfrmViewMsgIBPS.aspx?mn=2101&ID=<%# Eval("QUERY_ID") %>' target="_blank">
                }
                else
                {
                    strValues = sBranch + "|" + Request["ID"] + "|" + sUserName + "|pcurBM_IBPS_MSG";
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("strValues", strValues);
                    SessionHelper.Store("RN", "IBPS_PRINT_2");
                    SessionHelper.Store("sArrayID", "");
                    //In dien                                
                    sReport = "~/ViewReport/WfrmViewCrystalReport.aspx?ReportKind=IBPS_PRINT_2";
                }               
                
                Response.Redirect(sReport);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }
        
    }
}
