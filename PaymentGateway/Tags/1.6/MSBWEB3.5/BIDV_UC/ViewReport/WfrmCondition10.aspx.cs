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
using BIDVWEB.Business.Reports;
using BIDVWEB.Business.Web;

namespace BIDVWEB.BIDV_UC.ViewReport
{
    public partial class WfrmCondition10 : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsCommon objCommon = new clsCommon();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsReport objReport = new clsReport();
        private clsReportComm objReportComm = new clsReportComm();
        private clsForm objForm = new clsForm();
        private UserEncrypt Encrypt = new UserEncrypt();                
        private string strError = "";
        private string strBrHo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                //if (!IsPostBack)
                //{   //Kiem tra load du lieu ban dau
                //    if (!LoadData())
                //    {
                //        objForm.MessboxWeb(this.Page, strError);
                //        return;
                //    }
                //}
                //Kiem tra load du lieu ban dau
                if (!LoadData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
            }
            catch (Exception ex)
            {                
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        //Load Data//////////////////////////////////////////////////
        // Muc dich:    Load du lieu
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:              
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////
        private bool LoadData()
        {
            try
            {                
                string strRN = "";              //Ten bao cao   
                string strTableName = "";       //Bang Branch hoac tad de check NSD theo chi nhanh

                //Ten bao cao
                strRN = Request["RN"].ToString();
                //Lay Title
                objReportComm.GetReportTitle(lblCaption, strRN);                
                //Load controls dieu kien
                LoadControls(strRN, out strTableName);
                //Kiem tra quyen xem bao cao theo chi nhanh
                if (!string.IsNullOrEmpty(strTableName))
                {
                    if (objReport.CheckViewReport_Branch(strTableName)!=1)
                    {
                        strError = "Chi nhánh không có quyền xem báo cáo này";
                        btnPreview.Visible = false;
                        //objForm.MessboxWeb(this.Page, strError);
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

        //Preview report
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {                
                string sReport = "";
                //Kiem tra dieu kien
                if (!CheckSetData())
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                //Bao cao                
                sReport = "../ViewReport/WfrmViewCrystalReport.aspx?ReportKind=" + 
                          Request["RN"].ToString(); 
                Response.Write("<script>window.open('" + sReport + "')</script>"); 
                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        // Kiem tra dieu kien////////////////////////////////////////
        // Muc dich:    Kiem tra dieu kien
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:              
        // Dau ra:      Thanh cong: True, Khong thanh cong: false
        /////////////////////////////////////////////////////////////
        private bool CheckSetData()
        {
            try
            {
                DataSet dsData = new DataSet(); //
                DataRow dr;                     //datarow            
                string strRN = "";              //Ten bao cao
                string strValues = "";          //Luu gia tri cac tieu chi
                string strCursor = "";          //Cursor
                string strDayNum = "";          //So ngay cho phep lay bao cao                
                string sControlDate = "";       //Ten control date
                string sReportDate = "";        //Ngay bao cao
                
                //Ten bao cao
                strRN = Request["RN"].ToString();
                //Luu vao session
                SessionHelper.Store("RN", strRN);
                //Lay ten control ngay bao cao
                sControlDate = objReport.GetControlDate(strRN);                
                //Lay cac control hien thi tren form dieu kien               
                dsData = objReportComm.GetCtlReport(strRN);
                for (int i = 0; i <= dsData.Tables[0].Rows.Count - 1; i++)
                {
                    dr = dsData.Tables[0].Rows[i];
                    if (dr["CTLNAME"].ToString() == "textbox")
                    {
                        TextBox txttmp = (TextBox)tblMain.Rows[2 + i].Cells[1].Controls[0];                        
                        if (string.IsNullOrEmpty(txttmp.Text))
                        {
                            //Xu ly phien
                            if (dr["ISESSION"].ToString() == "1")
                            {
                                strValues = strValues + " |";
                            }
                            else
                            {
                                if (dr["ISNOTNULL"].ToString() == "1")
                                {
                                    strError = "Nhập dữ liệu " + dr["CAPTION"].ToString();
                                    return false;
                                }
                                else
                                {
                                    strValues = strValues + " |";
                                }
                            }
                        }
                        else
                        {
                            //Xu ly phien
                            if (dr["ISESSION"].ToString() == "1" && txttmp.Text.Trim()=="0")                            
                                strValues = strValues + "0|";                            
                            else                            
                                strValues = strValues + txttmp.Text.Trim() + "|";
                        }
                    }
                    else if (dr["CTLNAME"].ToString() == "textenable")
                    {
                        TextBox txttmp = (TextBox)tblMain.Rows[2 + i].Cells[1].Controls[0];
                        strValues = strValues + txttmp.Text.Trim() + "|";
                    }
                    else if (dr["CTLNAME"].ToString() == "textdate")
                    {
                        TextBox txttmp = (TextBox)tblMain.Rows[2 + i].Cells[1].Controls[0];
                        if (string.IsNullOrEmpty(txttmp.Text))
                        {
                            strError = "Nhập ngày báo cáo";
                            return false;
                        }
                        else
                        {
                            if (!objCommon.g_IsDate(txttmp.Text))
                            {
                                strError = "Nhập ngày báo cáo chưa đúng định dạng";
                                return false;
                            }

                            //Ngay lay bao cao
                            if ((dr["ISCHECK_DATE"] != null) && (Convert.ToInt16(dr["ISCHECK_DATE"].ToString()) == 1))
                            {
                                sReportDate = objCommon.g_Formatdate(txttmp.Text.Trim(), true);
                            }
                        }
                        //strValues = strValues + txttmp.Text.Trim() + "|";

                        //Xu ly lai truong hop ngay thang 08/03/2011
                        strValues = strValues + objCommon.g_Formatdate(txttmp.Text.Trim(), true) + "|";                        

                        //Convert.ToDateTime(objCommon.g_Formatdate(txttmp.Text.Trim(),false)) + "|";                                        
                    }
                    else if (dr["CTLNAME"].ToString() == "dropdownlist")
                    {
                        DropDownList ddltmp = (DropDownList)tblMain.Rows[2 + i].Cells[1].Controls[0];
                        if (ddltmp.Items.Count <= 0)
                        {
                            strError = "Chọn giá trị từ danh sách";
                            return false;
                        }
                        if (ddltmp.SelectedValue.ToString().ToUpper() != "ALL".ToUpper())
                        {
                            strValues = strValues + ddltmp.SelectedValue + "|";
                        }
                        else
                        {
                            //strValues = strValues + objReport.GetAllCitad() + "|";
                            strValues = strValues + "ALL|";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(sReportDate))         
                {
                    ////Kiem tra quyen xem bao cao theo thoi gian
                    //if (!objReport.CheckPermissionReport(strRN, sReportDate, out strDayNum))
                    //{
                    //    strError = "Chỉ có quyền xem báo cáo trong " + strDayNum + " ngày gần nhất";
                    //    return false;
                    //}
                }
                //Lay ten cursor tra ra du lieu bao cao
                strCursor = objReportComm.GetParaCursor(strRN);
                //Gan ten cursor vao danh sach gia tri truyen vao SP
                if (strValues.Substring(strValues.Length - 1, 1) == "|")
                {
                    strValues = strValues + strCursor;
                }
                else
                {
                    strValues = strValues + "|" + strCursor;
                }
                //Luu danh sach gia tri vao session
                SessionHelper.Store("strValues", strValues);
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        private void LoadControls(string strRN, out string strTable)
        {
            strTable = "";
            DataSet dsData = new DataSet(); //
            DataRow dr;                     //datarow            
            int iCtrl = 0;                  //So control can them                


            try
            {               

                //Dataset chua control form dieu kien
                dsData = objReportComm.GetCtlReport(strRN);
                if (dsData != null)
                {
                    iCtrl = dsData.Tables[0].Rows.Count;
                    //Them so row
                    objReportComm.AddTableRow(iCtrl, tblMain);
                    for (int i = 0; i <= iCtrl - 1; i++)
                    {
                        dr = dsData.Tables[0].Rows[i];
                        Label objlbl = new Label();
                        objlbl.ID = "label" + i;                            //ID
                        objlbl.Text = dr["CAPTION"].ToString();             //Caption
                        tblMain.Rows[2 + i].Cells[0].Controls.Add(objlbl);  //Add label

                        //Kieu textbox, disable
                        if (dr["CTLNAME"].ToString() == "textenable")
                        {
                            objlbl.Visible = false;
                        }
                        //Kieu textbox
                        if (dr["CTLNAME"].ToString() == "textbox")
                        {
                            TextBox objtxt = new TextBox();
                            objtxt.ID = Convert.ToString("textbox" + i);
                            if (dr["CTLLENGTH"] != null && dr["CTLLENGTH"].ToString() != "")
                                objtxt.MaxLength = Convert.ToInt16(dr["CTLLENGTH"].ToString());
                            else
                                objtxt.MaxLength = 50;
                            objtxt.Width = 140;
                            tblMain.Rows[2 + i].Cells[1].Controls.Add(objtxt);
                        }
                        //Kieu textbox, disable
                        else if (dr["CTLNAME"].ToString() == "textenable")
                        {
                            TextBox objtxt = new TextBox();
                            objtxt.ID = Convert.ToString("textbox" + i);
                            if (dr["CTLLENGTH"] != null && dr["CTLLENGTH"].ToString() != "")
                                objtxt.MaxLength = Convert.ToInt16(dr["CTLLENGTH"].ToString());
                            else
                                objtxt.MaxLength = 50;
                            objtxt.Width = 140;
                            objtxt.Enabled = false;
                            objtxt.Text = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                            tblMain.Rows[2 + i].Cells[1].Controls.Add(objtxt);
                            Label objLabel = new Label();
                            objLabel.ID = "objlabel" + i;
                            objLabel.Text = objReport.GetBranchOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                            objLabel.Visible = false;
                            tblMain.Rows[2 + i].Cells[1].Controls.Add(objLabel);
                            objtxt.Visible = false;
                        }
                        //Kieu textbox (nhap ngay thang)
                        else if (dr["CTLNAME"].ToString() == "textdate")
                        {
                            TextBox objtxt = new TextBox();
                            objtxt.ID = Convert.ToString("textbox" + i);
                            objtxt.Width = 120;
                            if (dr["CTLLENGTH"] != null && dr["CTLLENGTH"].ToString() != "")
                                objtxt.MaxLength = Convert.ToInt16(dr["CTLLENGTH"].ToString());
                            else
                                objtxt.MaxLength = 10;
                            tblMain.Rows[2 + i].Cells[1].Controls.Add(objtxt);
                            Image objimg = new Image();
                            objimg.ID = Convert.ToString("image" + i);
                            objimg.ImageUrl = "~/Images/insertdate.gif";
                            tblMain.Rows[2 + i].Cells[1].Controls.Add(objimg);
                            objCommon.gs_SetDate(objtxt, objimg);
                            objtxt.Text = objCommon.GetSysDate();
                        }
                        //Kieu dropdowlist
                        else if (dr["CTLNAME"].ToString() == "dropdownlist")
                        {
                            string strBranch = "";
                            bool iBool = false;
                            DropDownList objdll = new DropDownList();
                            DropDownList ddl = new DropDownList();
                            objdll.ID = Convert.ToString("dropdownlist" + i);
                            objdll.Width = 250;
                            //objdll.AutoPostBack = true;
                            tblMain.Rows[2 + i].Cells[1].Controls.Add(objdll);
                            strBranch = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());

                            if (dr["ENABLE"].ToString() == "2")
                            {
                                ddl = objDataAccess.FillDataToDropDownList(dr["STABLE"].ToString(),
                                        dr["SWHERE"].ToString(), objdll, dr["SFIELD1"].ToString(),
                                        dr["SFIELD2"].ToString(), dr["SFIELD1"].ToString(), true);
                                if (ddl != null)
                                {
                                    objdll = ddl;
                                }
                            }
                            else
                            {
                                //Kiem tra chi nhanh la H.O
                                strBrHo = objUser.GetBrHo();
                                if (strBranch.Trim().PadLeft(5, '0') == strBrHo)
                                {
                                    iBool = true;
                                }
                                //Xu ly phien
                                if (dr["ISESSION"].ToString() == "1")
                                {
                                    ddl = objDataAccess.FillDataToDropDownList(dr["STABLE"].ToString(),
                                            dr["SWHERE"].ToString(), objdll, dr["SFIELD1"].ToString(),
                                            dr["SFIELD2"].ToString(), dr["SFIELD1"].ToString(), false);
                                }
                                else
                                {
                                    ddl = objDataAccess.FillDataToDropDownList(dr["STABLE"].ToString(),
                                            dr["SWHERE"].ToString(), objdll, dr["SFIELD1"].ToString(),
                                            dr["SFIELD2"].ToString(), dr["SFIELD1"].ToString(), iBool);
                                }
                                if (ddl != null)
                                {
                                    objdll = ddl;
                                }
                                objdll.Enabled = iBool;
                                if (iBool == false)
                                {
                                    if (dr["STABLE"].ToString().ToUpper() == "BRANCH")
                                        objdll.SelectedValue = strBranch;
                                    else
                                    {
                                        //objdll.SelectedValue = strBranch.PadLeft(5, '0');
                                        if (dr["STABLE"].ToString().ToUpper() == "TAD")
                                        {
                                            if (objdll.SelectedValue.ToString().Length == 8)
                                            {
                                                string strTad8 = "";
                                                strTad8 = objReport.GetTAD8(strBranch);
                                                if (strTad8 != "")
                                                    objdll.SelectedValue = strTad8;
                                            }
                                            else
                                            {
                                                objdll.SelectedValue = strBranch.PadLeft(5, '0');
                                            }

                                        }
                                        else if (dr["STABLE"].ToString().ToUpper() == "VIEW_IBPS_BANK_MAP")
                                        { 
                                            string strTad8 = "";
                                            strTad8 = objReport.GetBranch8(strBranch);
                                            //if (strBranch + "-" + strTad8== ddl.)
                                            objdll.SelectedValue = strBranch + "-" + strTad8;
                                        }
                                    }
                                }
                                //Lay ten chi nhanh de check NSD theo chi nhanh
                                if (dr["IFILTER_BRANCH"] != null && dr["IFILTER_BRANCH"].ToString() != "")
                                {
                                    if (dr["IFILTER_BRANCH"].ToString() == "1")
                                    {
                                        strTable = dr["STABLE"].ToString();
                                        //Label objlblBranch = new Label();
                                        //objlblBranch.ID = "label10" + i;                            //ID
                                        //objlblBranch.Text = "";// dr["CAPTION"].ToString();         //Caption
                                        //tblMain.Rows[2 + i].Cells[0].Controls.Add(objlblBranch);    //Add label
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                strError = ex.Message;
            }
        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //Winform
            //Process.Start(path);
            //Webform
            //System.Diagnostics.Process.Start("C:\\CrystalReportViewer1.pdf");  

            //System.Diagnostics.Process.Start("IExplore.exe", "C:\\CrystalReportViewer1.pdf");

            //Set the appropriate ContentType.

            //•	"text/HTML"
            //•	"image/GIF"
            //•	"image/JPEG"
            //•	"text/plain"
            //•	"Application/msword" (for Microsoft Word files)
            //•	"Application/x-msexcel" (for Microsoft Excel files)

            //Response.ContentType = "Application/pdf";
            ////Get the physical path to the file.
            ////string FilePath = MapPath("acrobat.pdf");
            //string FilePath = "C:\\CrystalReportViewer1.pdf";
            ////Write the file directly to the HTTP content output stream.
            //Response.WriteFile(FilePath);
            //Response.End();
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {            
            //Response.WriteFile(Encrypt.Decrypt("GmP6iEZLrHg="));
            //objForm.MessboxWeb(this.Page, Encrypt.Decrypt("GmP6iEZLrHg="));
        }

    }
}
