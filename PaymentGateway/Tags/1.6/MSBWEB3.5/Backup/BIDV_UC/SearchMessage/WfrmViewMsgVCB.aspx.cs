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
    public partial class WfrmViewMsgVCB : System.Web.UI.Page
    {
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsForm objForm = new clsForm();
        private clsCommon objCommon = new clsCommon();
        private clsSearchVCB objSearchVCB = new clsSearchVCB();
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
                string strAmount = "";

                ds = objSearchVCB.GetMsgVCBByID(strMsg_ID);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    if (dr["RM_NUMBER"] != null)
                        txtRM.Text = dr["RM_NUMBER"].ToString();
                    if (dr["BRANCH_A"] != null)
                        txtSender.Text = dr["BRANCH_A"].ToString();
                    if (dr["TENNHG"] != null)
                        lblSender.Text = dr["TENNHG"].ToString();
                    if (dr["BRANCH_B"] != null)
                        txtReceiver.Text = dr["BRANCH_B"].ToString();
                    if (dr["TENNHN"] != null)
                        lblReceiver.Text = dr["TENNHN"].ToString();
                    if (dr["AMOUNT"] != null && dr["AMOUNT"].ToString().Trim() != "")
                    {
                        strAmount = Convert.ToDecimal(dr["AMOUNT"]).ToString("c");
                        txtAmount.Text = strAmount.Replace("$", "");
                        //txtAmount.Text = string.Format("{0:0,0.0}", dr["AMOUNT"].ToString());
                    }
                    if (dr["CCYCD"] != null)
                        lblCCY.Text = dr["CCYCD"].ToString();
                    if (dr["FIELD20"] != null)
                        txtRefNo.Text = dr["FIELD20"].ToString();
                    if (dr["TRANS_DATE"] != null)
                        txtTransDate.Text = dr["TRANS_DATE"].ToString();
                    if (dr["DEPARTMENT"] != null)
                        txtDepartment.Text = dr["DEPARTMENT"].ToString();
                    //Trang thai in dien
                    if (dr["PRINT_STS"] != null)
                        txtPrintSTS.Text = dr["PRINT_STS"].ToString();
                    if (dr["STATUS"] != null)
                        txtStatus.Text = dr["STATUS"].ToString();
                    //Loai dien
                    if (dr["MSG_TYPE"] != null)
                        txtMsgType.Text = dr["MSG_TYPE"].ToString();
                    //Chieu dien
                    if (dr["MSG_DIRECTION"] != null)
                        txtMsgDirection.Text = dr["MSG_DIRECTION"].ToString();
                    if (dr["CONTENT"] != null)
                        txtContent.Text = dr["CONTENT"].ToString();

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
                string sUserID = "";
                string sBranch = "";
                string sUserName = "";
                DataSet dsBranch = new DataSet();

                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                dsBranch = objUser.GetBranchByUserID(sUserID);
                sUserName = objUser.GetUserNameByUserID(sUserID);
                sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();

                if (txtMsgDirection.Text.ToString() == "VCB-SIBS")
                {
                    strValues = Request["ID"] + "|" + txtMsgType.Text.ToString() + "|" +
                        txtMsgDirection.Text.ToString() + "|" + sBranch +
                        "|" + sUserName + "|pCurContent";
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("strValues", strValues);
                    SessionHelper.Store("RN", "VCB_PRINT_1");
                    SessionHelper.Store("sArrayID", "");
                    //In dien                                
                    sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=VCB_PRINT_1";
                }
                else
                {
                    strValues = Request["ID"] + "|" + txtMsgType.Text.ToString() + "|" +
                        txtMsgDirection.Text.ToString()+ "|" + sBranch +
                        "|" + sUserName + "|pCurContent";
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("strValues", strValues);
                    SessionHelper.Store("RN", "VCB_PRINT_2");
                    SessionHelper.Store("sArrayID", "");
                    //In dien                                
                    sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=VCB_PRINT_2";
                }                
                //Response.Write("<script>window.open('" + sReport + "')</script>");
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
