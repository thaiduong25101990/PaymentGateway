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
using System.IO;

namespace BIDVWEB.BIDV_UC.SearchMessage
{
    public partial class WfrmMessageManagement : System.Web.UI.Page
    {
        private clsCommon objCommon = new clsCommon();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private string strError = "";                   //Ghi loi 
        private clsForm objForm = new clsForm();
        private DataSet ds = new DataSet();
        private clsSearchTTSP objSearchTTSP = new clsSearchTTSP();


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

                objDataAccess.FillDataToDropDownList("Currency", " Trim(Currency.gwtype)='VCB'  AND STATUS = 1  ", ddlCurrency, "CCYCD", "CCYCD", "CCYCD ASC", true);
                objDataAccess.FillDataToDropDownList("STATUS", "", ddlStatus, "NAME", "STATUS", "NAME ASC", true);
                objDataAccess.FillDataToDropDownList("Allcode", "Upper(CDNAME) = 'MSGDIRECTION' and Upper(GWTYPE) = 'TTSP'", ddlMSGDirection, "CONTENT", "CDVAL", "CONTENT ASC", true);

                objDataAccess.FillDataToDropDownList("Allcode", "Upper(CDNAME) = 'DEPARTMENT' ", ddlModule, "CONTENT", "CDVAL", "CONTENT ASC", true);

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
                if (!CheckDataSMS())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }

                #region get_strWhere

                //string strDate = "";
                //strDate = "where to_char(TRANS_DATE,'yyyyMMdd') >= '" + Convert.ToDateTime(txtFromDate).ToString("yyyyMMdd") +
                //    "' AND to_char(TRANS_DATE,'yyyyMMdd') <= '" + Convert.ToDateTime(txtToDate).ToString("yyyyMMdd") + "' ";

                //So tham chieu
                string strRefNo = "";
                if (txtRefno.Text.ToString() != "")
                    strRefNo = " AND upper(FIELD20) LIKE '%" + txtRefno.Text.ToString().ToUpper() + "%'";
                
                //MA GUI
                string strSender = "";
                if (txtSender.Text.ToString() != "")
                    strSender = " AND upper(SENDER) LIKE '%" + txtSender.Text.ToString().ToUpper() + "%'";
                
                //MA NHAN
                string strReceiver = "";
                if (txtReceiver.Text.ToString() != "")
                    strReceiver = " AND upper(RECEIVER) LIKE '%" + txtReceiver.Text.ToString().ToUpper() + "%'";
                //Amount
                string strAmount = "";
                if (txtAmount.Text.ToString() != "")
                    strAmount = " AND AMOUNT = " + txtAmount.Text.ToString();

                //Currency
                string strCurrency = "";
                if (ddlCurrency.SelectedValue.ToString() != "ALL")
                {
                    strCurrency = " and CCYCD = '" + ddlCurrency.SelectedValue.ToString() + "'";
                }

                //RMNo
                string strRMNo = "";
                if (txtRMNo.Text.ToString() != "")
                    strRMNo = " AND RM_NUMBER = " + txtRMNo.Text.ToString().Trim();
                
                //MSG Direction
                string strMSGDirection = "";
                if (ddlMSGDirection.SelectedValue.ToString() != "ALL")
                {
                    strMSGDirection = " and MSG_DIRECTION = '" + ddlMSGDirection.SelectedItem.Text+ "'";
                }

                //status
                string strStatus = "";
                if (ddlStatus.SelectedValue.ToString() != "ALL")
                {
                    strStatus = " and STATUS = '" + ddlStatus.SelectedValue.ToString() + "'";
                }

                //Module
                string strModule = "";
                if (ddlModule.SelectedValue.ToString() != "ALL")
                {
                    strModule = " and DEPARTMENT = '" + ddlModule.SelectedItem.Text.Trim() + "'";//ddlModule.SelectedValue.ToString()
                }

                //MSG Type
                string strMSGType = "";
                if (txtMSGType.Text.ToString() != "")
                    strMSGType = " AND MSG_EXPTYPE = " + txtMSGType.Text.ToString();

                //Cau dieu kien where
                string strWhere = "";
                strWhere = strRefNo + strSender + strReceiver + strAmount + strCurrency + strRMNo + strMSGDirection + strStatus + strModule + strMSGType;
                if (strWhere.Trim() != "") strWhere = " where 1= 1 " + strWhere;
                
                #endregion get_strWhere

                //Fill du lieu gridview
                ds = objSearchTTSP.SearchAdvance(strWhere, convertString(txtFromDate.Text), convertString(txtToDate.Text));

                ds.Tables[1].Merge(ds.Tables[2]);
                ds.Tables[0].Merge(ds.Tables[1]);

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
                Process(ex, txtFromDate.Text + txtToDate.Text);
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        public static string convertString(string sDate)
        {
            //27/05/2012
            string sReturn="";
            sReturn = sDate.Substring(6, 4) + sDate.Substring(3, 2) + sDate.Substring(0, 2);

            return sReturn;
        }

        public static void Process(Exception ex, string sDate)
        {
            StreamWriter myStreamWriter = null;
            String strPath = "C:\\logs\\";
            String strFilename = "";

            try
            {
                strFilename = String.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";
                if (Directory.Exists(strPath))
                {

                }
                else
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + strFilename))
                {
                    myStreamWriter = File.CreateText(strPath + strFilename);
                }
                else
                {
                    myStreamWriter = File.AppendText(strPath + strFilename);
                }
                myStreamWriter.WriteLine(System.DateTime.Now);
                myStreamWriter.WriteLine(ex.Message + "\n" + ex.StackTrace);

                myStreamWriter.Write(myStreamWriter.NewLine);
                myStreamWriter.WriteLine("Source : Unknown source" + "CANHDM" + sDate);
                myStreamWriter.WriteLine("Type : Runtime error");
                myStreamWriter.WriteLine("Code : Undefined");
                myStreamWriter.WriteLine("Description :" + ex.Message);
                myStreamWriter.WriteLine("StackTrace :" + ex.StackTrace.ToString());
                myStreamWriter.Write(myStreamWriter.NewLine);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                myStreamWriter.Flush();
                myStreamWriter.Close();

            }
        }

        //Kiem tra du lieu truoc khi search////////////////////////////
        //Mo ta:        Kiem tra du lieu truoc khi search
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        private bool CheckDataSMS()
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

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlMSGDirection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {

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
           
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            { // ban MINH gui
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
                        Label lblID = (Label)grvData.Rows[i].Cells[7].Controls[1];
                        
                         // MAP MSG_ID --> QUERY_ID
                        DataTable tblMSG_ID = objDataAccess.dsGetDataSourceByStr("SELECT * FROM TTSP_MSG_CONTENT where msg_id = " + lblID.Text.ToString(), "TTSP_MSG_CONTENT").Tables[0];
                        //strValues = Request["ID"] + "|" + txtMsgType.Text.ToString() + "|" +
                        sMsg_ID = tblMSG_ID.Rows[0]["QUERY_ID"].ToString();
                        //sMsg_ID = lblID.Text.ToString();
                        
                        if (sArrayID.Length > 0)
                            sArrayID = sArrayID + "|" + sMsg_ID;
                        else
                            sArrayID = sArrayID + sMsg_ID;
                    }
                }
                if (iNum > 0)
                {
                    strValues = "|pCurContent";
                    //Luu danh sach gia tri vao session
                    SessionHelper.Store("sArrayID", sArrayID);
                    SessionHelper.Store("strValues", strValues);
                    if (ddlMSGDirection.SelectedValue.ToString() == "TTSP-SIBS")
                    {
                        SessionHelper.Store("RN", "TTSP_PRINT_1");
                        sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=TTSP_PRINT_1";
                    }
                    else
                    {
                        SessionHelper.Store("RN", "TTSP_PRINT_2");
                        sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=TTSP_PRINT_2";
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
    }
}
