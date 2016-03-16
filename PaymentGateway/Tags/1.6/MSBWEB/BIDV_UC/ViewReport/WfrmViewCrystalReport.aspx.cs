using System;
using System.IO;
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
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using BIDVWEB.Business.Web;
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.Reports;
using BIDVWEB.Business.SearchMessage;
//using CrystalReportViewerWebReportSourceLib;
//using CrystalReportViewerExportLib;
//using CrystalActiveXReportViewerLib11_5;


namespace BIDVWEB.BIDV_UC.ViewReport
{
    public partial class WfrmViewCrystalReport : System.Web.UI.Page
    {
        private clsForm objForm = new clsForm();
        private string strError = "";
        private clsReport objReport = new clsReport();
        private clsUser objUser = new clsUser();
        private clsSearchSwift objSwift = new clsSearchSwift();

        protected void Page_Load(object sender, EventArgs e)
        {   
            try
            {
                if (!IsPostBack)
                {                    
                    ddlFileType.Items.Add("Rich Text (RTF)");
                    ddlFileType.Items.Add("Portable Document (PDF)");
                    ddlFileType.Items.Add("MS Word (DOC)");
                    ddlFileType.Items.Add("MS Excel (XLS)");
                }
                //set Crystal ReportViewer
                SetReportViewer();
                MemoryStream oStream = new MemoryStream();
                //Get the report document
                ReportDocument reportDocument = new ReportDocument();
                //Stop buffering the response
                Response.Buffer = false;
                //Clear the response content and headers
                Response.ClearContent();
                Response.ClearHeaders();
                //if (!ms_InitTest(out reportDocument))
                if (!ms_InitReportCondition(out reportDocument))
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }
                oStream = (System.IO.MemoryStream)reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);                
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                reportDocument.Dispose();
                //Response.End();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }

        //Goi ham set cac thuoc tinh cho ReportViewer//////////////////
        //Mo ta:        Goi ham set cac thuoc tinh cho ReportViewer
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Set thanh cong
        ///////////////////////////////////////////////////////////////
        private void SetReportViewer()
        {            
            //CrystalReportViewer1.PrintMode            
            //CrystalReportViewer1.ReuseParameterValuesOnRefresh = true;
            //CrystalReportViewer1.EnableParameterPrompt = false;
            //CrystalReportViewer1.ShowAllPageIds = true;
            CrystalReportViewer1.AutoDataBind = true;                        
            CrystalReportViewer1.DisplayGroupTree = false;            
            CrystalReportViewer1.ShowFirstPage();
            CrystalReportViewer1.ShowNextPage();
            CrystalReportViewer1.ShowPreviousPage();
            CrystalReportViewer1.ShowLastPage();
            //CrystalReportViewer1.BestFitPage = false;
            //CrystalReportViewer1.HasCrystalLogo = false;
            //CrystalReportViewer1.HasToggleGroupTreeButton = false;
            //CrystalReportViewer1.HasRefreshButton = false;
            //CrystalReportViewer1.HasSearchButton = false;
            //CrystalReportViewer1.DisplayPage = true;
            //CrystalReportViewer1.DisplayToolbar = true;
            //CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
            CrystalReportViewer1.HasExportButton = false;
        }

        private bool ms_InitTest(out ReportDocument reportDocument)
        {
            string sUserID = "";
            string sBranch = "";
            string sFileName = "";
            string sReportName = "";
            string strValues = "";
            string sArrayID = "";
            string[] sArrayValueID;
            char[] splitter = { '|' };
            string strValuesReturn = "";
            string strNameReport = "";
            string strValuesrpt = "";
            string strDataType = "";
            DataSet mds = new DataSet();
            reportDocument = null;

            try
            {
                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                //Lay ten bao cao tu session
                sReportName = SessionHelper.RetrieveWithDefault("RN", "").ToString();
                //Lay mang gia tri cho parameter tu session
                strValues = SessionHelper.RetrieveWithDefault("strValues", "").ToString();
                //Lay mang gia tri Query_ID tu session
                sArrayID = SessionHelper.RetrieveWithDefault("sArrayID", "").ToString();
                if (string.IsNullOrEmpty(sArrayID))
                {
                    if (!objReport.DoCreateReport(out mds, sReportName, strValues,
                        out strValuesReturn))
                    {
                        strError = objReport.strError;
                        return false;
                    }
                    //Duong dan file bao cao
                    sFileName = "../ReportCrystal/" + sReportName + ".rpt";
                    //Danh sach parameter cua bao cao 
                    strValuesrpt = strValuesReturn.Substring(0, strValuesReturn.LastIndexOf('|'));
                    if (sReportName.Trim().ToUpper() == "IBPS_PRINT_1" ||
                        sReportName.Trim().ToUpper() == "IBPS_PRINT_2" ||
                        sReportName.Trim().ToUpper() == "VCB_PRINT_1" ||
                        sReportName.Trim().ToUpper() == "VCB_PRINT_2" ||
                        
                        //canhdm
                        sReportName.Trim().ToUpper() == "TTSP_PRINT_1" ||
                        sReportName.Trim().ToUpper() == "TTSP_PRINT_2" ||
                        sReportName.Trim().ToUpper() == "BM_TTSP04" ||
                        sReportName.Trim().ToUpper() == "BM_TTSP05" ||
                        sReportName.Trim().ToUpper() == "BM_TTSP02" ||

                        //
                        sReportName.Trim().ToUpper() == "SWIFT_PRINT" ||
                        sReportName.Trim().ToUpper() == "SWIFT_PRINT_TF" ||
                        sReportName.Trim().ToUpper() == "GW_TS")
                    {

                    }
                    else
                    {
                        //if (!objReport.GetParaRPT(sReportName, out strNameReport))
                        //{
                        //    strError = objReport.strError;
                        //    return false;
                        //}

                        //for (int i = 0; i < (reportDocument.ParameterFields.Count) / 2; i++)
                        //{
                        //    strNameReport = strNameReport + "v" + reportDocument.ParameterFields[i].Name.ToString() + "|";
                        //    strDataType = strDataType + reportDocument.ParameterFields[i].ParameterValueType.ToString() + "|";
                        //    strValuesrpt = strValuesrpt + reportDocument.ParameterFields[i].CurrentValues.ToString() + "|";
                        //}
                    }
                    //Ham tao bao cao
                    objReport.gViewReport1Test(sFileName, CrystalReportViewer1, mds,
                            strNameReport, strValuesrpt, null, out reportDocument);
                }               
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        
        //Goi ham tao bao cao//////////////////////////////////////////
        //Mo ta:        Goi ham tao bao cao
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Tao bao cao thanh cong
        ///////////////////////////////////////////////////////////////
        private bool ms_InitReportCondition(out ReportDocument reportDocument)
        {
            string sUserID = "";
            string sUserName = "";
            string sBranch = "";
            string sFileName ="";
            string sReportName ="";                                                                                                              
            string strValues = "";
            string sArrayID = "";
            string[] sArrayValueID;            
            char[] splitter = { '|' };
            string strValuesReturn = "";
            string strNameReport = "";
            string strValuesrpt = "";            
            DataSet mds = new DataSet();            
            reportDocument = null;
            
            try
            {                
                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                sUserName = objUser.GetUserNameByUserID(sUserID);
                //Lay ten bao cao tu session
                sReportName = SessionHelper.RetrieveWithDefault("RN", "").ToString();
                //Lay mang gia tri cho parameter tu session
                strValues = SessionHelper.RetrieveWithDefault("strValues", "").ToString();
                //Lay mang gia tri Query_ID tu session
                sArrayID = SessionHelper.RetrieveWithDefault("sArrayID", "").ToString();
                if (string.IsNullOrEmpty(sArrayID))
                {
                    if (!objReport.DoCreateReport(out mds, sReportName, strValues,
                        out strValuesReturn))
                    {
                        strError = objReport.strError;
                        return false;
                    }
                    //Duong dan file bao cao
                    sFileName = "../ReportCrystal/" + sReportName + ".rpt";
                    //Danh sach parameter cua bao cao 
                    strValuesrpt = strValuesReturn.Substring(0, strValuesReturn.LastIndexOf('|'));
                    if (sReportName.Trim().ToUpper() == "IBPS_PRINT_1" ||
                        sReportName.Trim().ToUpper() == "IBPS_PRINT_2" ||
                        sReportName.Trim().ToUpper() == "VCB_PRINT_1" ||
                        sReportName.Trim().ToUpper() == "VCB_PRINT_2" ||

                        //canhdm
                        sReportName.Trim().ToUpper() == "TTSP_PRINT_1" ||
                        sReportName.Trim().ToUpper() == "TTSP_PRINT_2" ||
                        sReportName.Trim().ToUpper() == "BM_TTSP04" ||
                        sReportName.Trim().ToUpper() == "BM_TTSP05" ||
                        sReportName.Trim().ToUpper() == "BM_TTSP02" ||
                        //
                        sReportName.Trim().ToUpper() == "SWIFT_PRINT" ||
                        sReportName.Trim().ToUpper() == "SWIFT_PRINT_TF" || 
                        sReportName.Trim().ToUpper() == "GW_TS")
                    {

                    }
                    else
                    {
                        if (!objReport.GetParaRPT(sReportName, out strNameReport))
                        {
                            strError = objReport.strError;
                            return false;
                        }
                    }
                    //Ham tao bao cao
                    objReport.gViewReport1(sFileName, CrystalReportViewer1, mds,
                            strNameReport, strValuesrpt, null, out reportDocument);
                }
                //In nhieu dien
                else
                {
                    if (strValues.Substring(0, 1) != "|")
                    {
                        for (int k = 0; k < strValues.Length; k++)
                        {
                            if (strValues[k].ToString() == "|")
                            {
                                strValues = strValues.Substring(k, strValues.Length - k);
                                break;
                            }
                        }
                    }
                    string strValuesDetail = "";
                    sArrayValueID = sArrayID.Split(splitter);
                    //Gan lai mang ID
                    SessionHelper.Store("sArrayID", sArrayID);
                    DataSet dsDataAll = new DataSet();
                    DataRow drData;
                    DataSet[] mds1 = new DataSet[sArrayValueID.Length];
                    for (int i = 0; i < sArrayValueID.Length; i++)
                    {
                        strValuesDetail = "";
                        DataSet dsBranch = new DataSet();
                        dsBranch = objUser.GetBranchByUserID(sUserID);
                        sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                        
                        if (sReportName == "IBPS_PRINT_1")  
                        {
                            strValuesDetail = sBranch + "|" + sArrayValueID[i].ToString() +
                                "|" + sUserName + strValues;
                        }
                        else if (sReportName == "IBPS_PRINT_2")
                        {
                            strValuesDetail = sBranch + "|" + sArrayValueID[i].ToString() +
                                "|" + sUserName + strValues;
                        }

                        if (sReportName == "VCB_PRINT_1")
                        {
                            strValuesDetail = "|" + "|" + "|" + sBranch +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "VCB_PRINT_2")
                        {
                            strValuesDetail = "|" + "|" + "|" + sBranch +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }

                        //CANHDM
                        else if (sReportName == "TTSP_PRINT_1")
                        {
                            strValuesDetail = "|" + "MT103" +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "TTSP_PRINT_2")
                        {
                            strValuesDetail = "|" + "MT103" +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "BM_TTSP04")
                        {
                            strValuesDetail = "|" + "|" + "|" + sBranch +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "BM_TTSP05")
                        {
                            strValuesDetail = "|" + "|" + "|" + sBranch +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "BM_TTSP02")
                        {
                            strValuesDetail = "|" + "|" + "|" + sBranch +
                                "|" + sUserName + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        ///////////////////////////////////////////////////////////////




                        else if (sReportName == "SWIFT_PRINT")
                        {
                            DataSet dsMsgType = new DataSet();
                            string strMsgType = "";
                            dsMsgType = objSwift.GetMsgSWIFTByID(sArrayValueID[i].ToString());
                            strMsgType = dsMsgType.Tables[0].Rows[0]["MSG_TYPE"].ToString();
                            strValuesDetail = "|" + strMsgType + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "SWIFT_PRINT_TF")
                        {
                            DataSet dsMsgType = new DataSet();
                            string strMsgType = "";
                            dsMsgType = objSwift.GetMsgSWIFTByID(sArrayValueID[i].ToString());
                            strMsgType = dsMsgType.Tables[0].Rows[0]["MSG_TYPE"].ToString();
                            strValuesDetail = "|" + strMsgType + strValues;
                            strValuesDetail = sArrayValueID[i].ToString() + strValuesDetail;
                        }
                        else if (sReportName == "GW_TS")
                        {
                            strValuesDetail = sBranch + "|" + sArrayValueID[i].ToString() +
                                "|" + sUserName + strValues;
                        }

                        if (i == 0)
                        {
                            if (!objReport.DoCreateReport(out dsDataAll, sReportName, 
                                strValuesDetail, out strValuesReturn))
                            {
                                strError = objReport.strError;
                                return false;
                            }
                        }
                        else
                        {
                            if (!objReport.DoCreateReport(out mds, sReportName, 
                                strValuesDetail, out strValuesReturn))
                            {
                                strError = objReport.strError;
                                return false;
                            }
                        }
                        //mds1[i] = mds;
                        //Add data row vao datatable
                        if (i > 0 && mds != null && mds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < mds.Tables[0].Rows.Count; j++)
                            {
                                drData = mds.Tables[0].Rows[j];
                                DataRow newRow = dsDataAll.Tables[0].NewRow();    
                            
                                if (sReportName.Trim().ToUpper() == "SWIFT_PRINT")
                                {
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["query_ID"] = drData["query_ID"];
                                    newRow["Msg_type"] = drData["Msg_type"];
                                    newRow["msg_name"] = drData["msg_name"];
                                    newRow["field_id"] = drData["field_id"];
                                    newRow["field_name"] = drData["field_name"];
                                    newRow["field_value"] = drData["field_value"];
                                    newRow["sender"] = drData["sender"];
                                    newRow["sendername"] = drData["sendername"];
                                    newRow["receiver"] = drData["receiver"];
                                    newRow["receivername"] = drData["receivername"];
                                    newRow["session_no"] = drData["session_no"];
                                    newRow["seq_no"] = drData["seq_no"];
                                    newRow["STATUS"] = drData["STATUS"];
                                    newRow["priority"] = drData["priority"];
                                    newRow["ref_no"] = drData["ref_no"];
                                    newRow["PRINT_STS"] = drData["PRINT_STS"];
                                    newRow["receiving_time"] = drData["receiving_time"];
                                    newRow["sending_time"] = drData["sending_time"];
                                    newRow["MSGDIRECTION"] = drData["MSGDIRECTION"];
                                    newRow["EXTFIELD01"] = drData["EXTFIELD01"];
                                    newRow["EXTFIELD02"] = drData["EXTFIELD02"];
                                    newRow["ORDERSHOW"] = drData["ORDERSHOW"];
                                }
                                else if (sReportName.Trim().ToUpper() == "SWIFT_PRINT_TF")
                                {
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["query_ID"] = drData["query_ID"];
                                    newRow["Msg_type"] = drData["Msg_type"];
                                    newRow["msg_name"] = drData["msg_name"];
                                    newRow["SENDER"] = drData["SENDER"];
                                    newRow["SENDERNAME"] = drData["SENDERNAME"];
                                    newRow["RECEIVER"] = drData["RECEIVER"];
                                    newRow["RECEIVERNAME"] = drData["RECEIVERNAME"];
                                    newRow["SESSION_NO"] = drData["SESSION_NO"];
                                    newRow["SEQ_NO"] = drData["SEQ_NO"];
                                    newRow["STATUS"] = drData["STATUS"];
                                    newRow["PRIORITY"] = drData["PRIORITY"];
                                    newRow["REF_NO"] = drData["REF_NO"];
                                    newRow["STATUS"] = drData["STATUS"];
                                    newRow["PRINT_STS"] = drData["PRINT_STS"];
                                    newRow["RECEIVING_TIME"] = drData["RECEIVING_TIME"];
                                    newRow["SENDING_TIME"] = drData["SENDING_TIME"];
                                    newRow["MSGDIRECTION"] = drData["MSGDIRECTION"];
                                    newRow["CONTENT01"] = drData["CONTENT01"];
                                    newRow["CONTENT02"] = drData["CONTENT02"];
                                    newRow["CONTENT03"] = drData["CONTENT03"];
                                    newRow["CONTENT04"] = drData["CONTENT04"];
                                    newRow["CONTENT05"] = drData["CONTENT05"];
                                    newRow["CONTENT06"] = drData["CONTENT06"];
                                    newRow["CONTENT07"] = drData["CONTENT07"];
                                    newRow["CONTENT08"] = drData["CONTENT08"];
                                    newRow["CONTENT09"] = drData["CONTENT09"];
                                    newRow["CONTENT10"] = drData["CONTENT10"];
                                }
                                else if (sReportName.Trim().ToUpper() == "IBPS_PRINT_1")
                                {
                                    newRow["branchname"] = drData["branchname"];
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["RM_NUMBER"] = drData["RM_NUMBER"];
                                    newRow["MSG_DIRECTION"] = drData["MSG_DIRECTION"];
                                    newRow["SRCBRANCH"] = drData["SRCBRANCH"];
                                    newRow["SRCBRANCHNAME"] = drData["SRCBRANCHNAME"];
                                    newRow["RECEIBRANCH"] = drData["RECEIBRANCH"];
                                    newRow["RECEIBRANCHNAME"] = drData["RECEIBRANCHNAME"];
                                    newRow["TRANSDATE"] = drData["TRANSDATE"];
                                    newRow["Receiving_Time"] = drData["Receiving_Time"];
                                    newRow["SEnding_Time"] = drData["SEnding_Time"];
                                    newRow["PreTAD"] = drData["PreTAD"];
                                    newRow["PreTADName"] = drData["PreTADName"];
                                    newRow["PRINT_STATUS"] = drData["PRINT_STATUS"];
                                    newRow["BUTTOAN"] = drData["BUTTOAN"];
                                    newRow["SOGD"] = drData["SOGD"];
                                    newRow["Sender"] = drData["Sender"];
                                    newRow["BankSenderName"] = drData["BankSenderName"];
                                    newRow["Recerver"] = drData["Recerver"];
                                    newRow["BankReceiName"] = drData["BankReceiName"];
                                    newRow["sendname"] = drData["sendname"];
                                    newRow["sendaddress"] = drData["sendaddress"];
                                    newRow["sendacc"] = drData["sendacc"];
                                    newRow["AtBankSend"] = drData["AtBankSend"];
                                    newRow["AtBankSendName"] = drData["AtBankSendName"];
                                    newRow["receiname"] = drData["receiname"];
                                    newRow["receiaddress"] = drData["receiaddress"];
                                    newRow["receiacc"] = drData["receiacc"];
                                    newRow["AtBankRecei"] = drData["AtBankRecei"];
                                    newRow["AtBankReceiName"] = drData["AtBankReceiName"];
                                    newRow["STATUS"] = drData["STATUS"];
                                    newRow["AMOUNT"] = drData["AMOUNT"];
                                    newRow["CCY"] = drData["CCY"];
                                    newRow["NOIDUNG"] = drData["NOIDUNG"];
                                    newRow["Descriptions"] = drData["Descriptions"];
                                    newRow["TellerID"] = drData["TellerID"];
                                    newRow["TellerName"] = drData["TellerName"];
                                    newRow["TRANS_CODE"] = drData["TRANS_CODE"];
                                    newRow["CCYNAME"] = drData["CCYNAME"];
                                    newRow["GRPID"] = drData["GRPID"];
                                }
                                else if (sReportName.Trim().ToUpper() == "IBPS_PRINT_2")
                                {
                                    newRow["branchname"] = drData["branchname"];
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["RM_NUMBER"] = drData["RM_NUMBER"];
                                    newRow["MSG_DIRECTION"] = drData["MSG_DIRECTION"];
                                    newRow["SRCBRANCH"] = drData["SRCBRANCH"];
                                    newRow["SRCBRANCHNAME"] = drData["SRCBRANCHNAME"];
                                    newRow["RECEIBRANCH"] = drData["RECEIBRANCH"];
                                    newRow["RECEIBRANCHNAME"] = drData["RECEIBRANCHNAME"];
                                    newRow["TRANSDATE"] = drData["TRANSDATE"];
                                    newRow["Receiving_Time"] = drData["Receiving_Time"];
                                    newRow["SEnding_Time"] = drData["SEnding_Time"];
                                    newRow["PreTAD"] = drData["PreTAD"];
                                    newRow["PreTADName"] = drData["PreTADName"];
                                    newRow["PRINT_STATUS"] = drData["PRINT_STATUS"];
                                    newRow["BUTTOAN"] = drData["BUTTOAN"];
                                    newRow["SOGD"] = drData["SOGD"];
                                    newRow["Sender"] = drData["Sender"];
                                    newRow["BankSenderName"] = drData["BankSenderName"];
                                    newRow["Recerver"] = drData["Recerver"];
                                    newRow["BankReceiName"] = drData["BankReceiName"];
                                    newRow["sendname"] = drData["sendname"];
                                    newRow["sendaddress"] = drData["sendaddress"];
                                    newRow["sendacc"] = drData["sendacc"];
                                    newRow["AtBankSend"] = drData["AtBankSend"];
                                    newRow["AtBankSendName"] = drData["AtBankSendName"];
                                    newRow["receiname"] = drData["receiname"];
                                    newRow["receiaddress"] = drData["receiaddress"];
                                    newRow["receiacc"] = drData["receiacc"];
                                    newRow["AtBankRecei"] = drData["AtBankRecei"];
                                    newRow["AtBankReceiName"] = drData["AtBankReceiName"];
                                    newRow["STATUS"] = drData["STATUS"];
                                    newRow["AMOUNT"] = drData["AMOUNT"];
                                    newRow["CCY"] = drData["CCY"];
                                    newRow["NOIDUNG"] = drData["NOIDUNG"];
                                    newRow["Descriptions"] = drData["Descriptions"];
                                    newRow["TellerID"] = drData["TellerID"];
                                    newRow["TellerName"] = drData["TellerName"];
                                    newRow["TRANS_CODE"] = drData["TRANS_CODE"];
                                    newRow["CCYNAME"] = drData["CCYNAME"];
                                    newRow["GRPID"] = drData["GRPID"];
                                }
                                else if (sReportName.Trim().ToUpper() == "BM_TTSP01")
                                {
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["STATUS"] = drData["STATUS"];
                                    newRow["MSG_DIRECTION"] = drData["MSG_DIRECTION"];
                                    newRow["BRANCH_SENDER"] = drData["BRANCH_SENDER"];
                                    newRow["BRANCH_RECEIVE"] = drData["BRANCH_RECEIVE"];
                                    newRow["MSG_TYPE"] = drData["MSG_TYPE"];
                                    newRow["SENDER"] = drData["SENDER"];
                                    newRow["RECEIVER"] = drData["RECEIVER"];
                                    newRow["FIELD_NAME"] = drData["FIELD_NAME"];
                                    newRow["FIELD_VALUE"] = drData["FIELD_VALUE"];
                                    newRow["V_FIELD_NAME"] = drData["V_FIELD_NAME"];
                                    newRow["V_MSG_NAME"] = drData["V_MSG_NAME"];
                                    newRow["FIELD_ORDER"] = drData["FIELD_ORDER"];
                                }
                                else if (sReportName.Trim().ToUpper() == "VCB_PRINT_1")
                                {
                                    newRow["NGAY_32A"] = drData["NGAY_32A"];
                                    newRow["RM_NUMBER"] = drData["RM_NUMBER"];
                                    newRow["SENDER"] = drData["SENDER"];
                                    newRow["RECEIVER"] = drData["RECEIVER"];
                                    newRow["SENDERNAME"] = drData["SENDERNAME"];
                                    newRow["RECEIVERNAME"] = drData["RECEIVERNAME"];
                                    newRow["SOLENH"] = drData["SOLENH"];
                                    newRow["REF_NO"] = drData["REF_NO"];                                    
                                    newRow["receiving_time"] = drData["receiving_time"];
                                    newRow["sending_time"] = drData["sending_time"];
                                    newRow["NGUOIRALENH"] = drData["NGUOIRALENH"];
                                    newRow["TKNGUOIRALENH"] = drData["TKNGUOIRALENH"];
                                    newRow["DCNGUOIRALENH"] = drData["DCNGUOIRALENH"];
                                    newRow["NGUOIHUONG"] = drData["NGUOIHUONG"];
                                    newRow["TK_HUONG"] = drData["TK_HUONG"];
                                    newRow["DIACHI"] = drData["DIACHI"];
                                    newRow["AMOUNT"] = drData["AMOUNT"];
                                    newRow["Status"] = drData["Status"];
                                    newRow["print_status"] = drData["print_status"];
                                    newRow["CONTENT"] = drData["CONTENT"];
                                    newRow["TK_GHINO"] = drData["TK_GHINO"];
                                    newRow["TEN_TK"] = drData["TEN_TK"];
                                    newRow["CCY"] = drData["CCY"];
                                    newRow["ORG_AMOUNT"] = drData["ORG_AMOUNT"];
                                    newRow["LOAIPHI"] = drData["LOAIPHI"];
                                    newRow["TIENPHI"] = drData["TIENPHI"];
                                    newRow["DVTIENPHI"] = drData["DVTIENPHI"];                                    
                                    newRow["NH_HUONG"] = drData["NH_HUONG"];
                                    newRow["BRNAME"] = drData["BRNAME"];
                                    newRow["TELLERNAME"] = drData["TELLERNAME"];
                                    newRow["CCYNAME"] = drData["CCYNAME"];
                                    newRow["TELLERID"] = drData["TELLERID"];
                                    newRow["SIBS_TELLERID"] = drData["SIBS_TELLERID"];                                    
                                    newRow["GRPID"] = drData["GRPID"];
                                }
                                else if (sReportName.Trim().ToUpper() == "VCB_PRINT_2")
                                {
                                    newRow["NGAY_32A"] = drData["NGAY_32A"];
                                    newRow["RM_NUMBER"] = drData["RM_NUMBER"];
                                    newRow["SENDER"] = drData["SENDER"];
                                    newRow["RECEIVER"] = drData["RECEIVER"];
                                    newRow["SENDERNAME"] = drData["SENDERNAME"];
                                    newRow["RECEIVERNAME"] = drData["RECEIVERNAME"];
                                    newRow["SOLENH"] = drData["SOLENH"];
                                    newRow["REF_NO"] = drData["REF_NO"];        
                                    newRow["receiving_time"] = drData["receiving_time"];
                                    newRow["sending_time"] = drData["sending_time"];
                                    newRow["NGUOIRALENH"] = drData["NGUOIRALENH"];
                                    newRow["TKNGUOIRALENH"] = drData["TKNGUOIRALENH"];
                                    newRow["DCNGUOIRALENH"] = drData["DCNGUOIRALENH"];
                                    newRow["NGUOIHUONG"] = drData["NGUOIHUONG"];
                                    newRow["TK_HUONG"] = drData["TK_HUONG"];
                                    newRow["DIACHI"] = drData["DIACHI"];
                                    newRow["AMOUNT"] = drData["AMOUNT"];
                                    newRow["Status"] = drData["Status"];
                                    newRow["print_status"] = drData["print_status"];
                                    newRow["CONTENT"] = drData["CONTENT"];
                                    newRow["TK_GHINO"] = drData["TK_GHINO"];
                                    newRow["TEN_TK"] = drData["TEN_TK"];
                                    newRow["CCY"] = drData["CCY"];
                                    newRow["ORG_AMOUNT"] = drData["ORG_AMOUNT"];
                                    newRow["LOAIPHI"] = drData["LOAIPHI"];
                                    newRow["TIENPHI"] = drData["TIENPHI"];
                                    newRow["DVTIENPHI"] = drData["DVTIENPHI"]; 
                                    newRow["NH_HUONG"] = drData["NH_HUONG"];
                                    newRow["BRNAME"] = drData["BRNAME"];
                                    newRow["TELLERNAME"] = drData["TELLERNAME"];
                                    newRow["CCYNAME"] = drData["CCYNAME"];
                                    newRow["TELLERID"] = drData["TELLERID"];
                                    newRow["SIBS_TELLERID"] = drData["SIBS_TELLERID"];
                                    newRow["GRPID"] = drData["GRPID"];
                                }
                                else if (sReportName.Trim().ToUpper() == "TTSP_PRINT_1")
                                {
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["MSG_DATE"] = drData["MSG_DATE"];
                                    newRow["MSG_TYPE"] = drData["MSG_TYPE"];
                                    newRow["MSG_REF"] = drData["MSG_REF"];
                                    newRow["MSG_STATUS"] = drData["MSG_STATUS"];
                                    newRow["TXN_CODE"] = drData["TXN_CODE"];
                                    newRow["TXN_REF"] = drData["TXN_REF"];
                                    newRow["APC_BANK_ID"] = drData["APC_BANK_ID"];
                                    newRow["APC_BANK_CODE"] = drData["APC_BANK_CODE"];
                                    newRow["APC_BANK_NAME"] = drData["APC_BANK_NAME"];
                                    newRow["APC_ACC"] = drData["APC_ACC"];
                                    newRow["APC_NAME"] = drData["APC_NAME"];
                                    newRow["APC_ADDRESS"] = drData["APC_ADDRESS"];
                                    newRow["APC_IDNO"] = drData["APC_IDNO"];
                                    newRow["APC_IDDATE"] = drData["APC_IDDATE"];
                                    newRow["APC_IDPLACE"] = drData["APC_IDPLACE"];
                                    newRow["BEN_BANK_ID"] = drData["BEN_BANK_ID"];
                                    newRow["BEN_BANK_CODE"] = drData["BEN_BANK_CODE"];
                                    newRow["BEN_BANK_NAME"] = drData["BEN_BANK_NAME"];
                                    newRow["BEN_ACC"] = drData["BEN_ACC"];
                                    newRow["BEN_NAME"] = drData["BEN_NAME"];
                                    newRow["BEN_ADDRESS"] = drData["BEN_ADDRESS"];
                                    newRow["BEN_IDNO"] = drData["BEN_IDNO"];
                                    newRow["BEN_IDDATE"] = drData["BEN_IDDATE"];
                                    newRow["BEN_IDPLACE"] = drData["BEN_IDPLACE"];
                                    newRow["VALUE_DATE"] = drData["VALUE_DATE"];
                                    newRow["CREATE_DATE"] = drData["CREATE_DATE"];
                                    newRow["AMOUNT"] = drData["AMOUNT"];
                                    newRow["CURR_CODE"] = drData["CURR_CODE"];
                                    newRow["CHARGE_CODE"] = drData["CHARGE_CODE"];
                                    newRow["PAYMENT_DETAIL"] = drData["PAYMENT_DETAIL"];
                                    newRow["CREATEDBY"] = drData["CREATEDBY"];
                                    newRow["CREATE_TIME"] = drData["CREATE_TIME"];
                                    newRow["APPROVEDBY"] = drData["APPROVEDBY"];
                                    newRow["APPROVE_TIME"] = drData["APPROVE_TIME"];
                                    newRow["MSGDIR"] = drData["MSGDIR"];
                                    newRow["PRINTED"] = drData["PRINTED"];
                                    newRow["PRINT_TIME"] = drData["PRINT_TIME"];
                                    newRow["sender_ID"] = drData["sender_ID"];
                                    newRow["receiver_id"] = drData["receiver_id"];
                                    newRow["sender_name"] = drData["sender_name"];
                                    newRow["receiver_name"] = drData["receiver_name"];
                                    newRow["TRANSMIT_TIME"] = drData["TRANSMIT_TIME"];
                                    newRow["CONFIRM_TIME"] = drData["CONFIRM_TIME"];


                                    //newRow = drData;
                                }
                                else if (sReportName.Trim().ToUpper() == "TTSP_PRINT_2")
                                {
                                    newRow["MSG_ID"] = drData["MSG_ID"];
                                    newRow["MSG_DATE"] = drData["MSG_DATE"];
                                    newRow["MSG_TYPE"] = drData["MSG_TYPE"];
                                    newRow["MSG_REF"] = drData["MSG_REF"];
                                    newRow["MSG_STATUS"] = drData["MSG_STATUS"];
                                    newRow["TXN_CODE"] = drData["TXN_CODE"];
                                    newRow["TXN_REF"] = drData["TXN_REF"];
                                    newRow["APC_BANK_ID"] = drData["APC_BANK_ID"];
                                    newRow["APC_BANK_CODE"] = drData["APC_BANK_CODE"];
                                    newRow["APC_BANK_NAME"] = drData["APC_BANK_NAME"];
                                    newRow["APC_ACC"] = drData["APC_ACC"];
                                    newRow["APC_NAME"] = drData["APC_NAME"];
                                    newRow["APC_ADDRESS"] = drData["APC_ADDRESS"];
                                    newRow["APC_IDNO"] = drData["APC_IDNO"];
                                    newRow["APC_IDDATE"] = drData["APC_IDDATE"];
                                    newRow["APC_IDPLACE"] = drData["APC_IDPLACE"];
                                    newRow["BEN_BANK_ID"] = drData["BEN_BANK_ID"];
                                    newRow["BEN_BANK_CODE"] = drData["BEN_BANK_CODE"];
                                    newRow["BEN_BANK_NAME"] = drData["BEN_BANK_NAME"];
                                    newRow["BEN_ACC"] = drData["BEN_ACC"];
                                    newRow["BEN_NAME"] = drData["BEN_NAME"];
                                    newRow["BEN_ADDRESS"] = drData["BEN_ADDRESS"];
                                    newRow["BEN_IDNO"] = drData["BEN_IDNO"];
                                    newRow["BEN_IDDATE"] = drData["BEN_IDDATE"];
                                    newRow["BEN_IDPLACE"] = drData["BEN_IDPLACE"];
                                    newRow["VALUE_DATE"] = drData["VALUE_DATE"];
                                    newRow["CREATE_DATE"] = drData["CREATE_DATE"];
                                    newRow["AMOUNT"] = drData["AMOUNT"];
                                    newRow["CURR_CODE"] = drData["CURR_CODE"];
                                    newRow["CHARGE_CODE"] = drData["CHARGE_CODE"];
                                    newRow["PAYMENT_DETAIL"] = drData["PAYMENT_DETAIL"];
                                    newRow["CREATEDBY"] = drData["CREATEDBY"];
                                    newRow["CREATE_TIME"] = drData["CREATE_TIME"];
                                    newRow["APPROVEDBY"] = drData["APPROVEDBY"];
                                    newRow["APPROVE_TIME"] = drData["APPROVE_TIME"];
                                    newRow["MSGDIR"] = drData["MSGDIR"];
                                    newRow["PRINTED"] = drData["PRINTED"];
                                    newRow["PRINT_TIME"] = drData["PRINT_TIME"];
                                    newRow["sender_ID"] = drData["sender_ID"];
                                    newRow["receiver_id"] = drData["receiver_id"];
                                    newRow["sender_name"] = drData["sender_name"];
                                    newRow["receiver_name"] = drData["receiver_name"];
                                    newRow["TRANSMIT_TIME"] = drData["TRANSMIT_TIME"];
                                    newRow["CONFIRM_TIME"] = drData["CONFIRM_TIME"];



                                    //newRow = drData;
                                }
                                dsDataAll.Tables[0].Rows.Add(newRow);
                            }
                        }
                    }
                    //Duong dan file bao cao
                    sFileName = "../ReportCrystal/" + sReportName + ".rpt";                    
                    //Ham tao bao cao
                    objReport.gViewReport2(sFileName, CrystalReportViewer1, dsDataAll,
                        out reportDocument);
                    //Ham tao bao cao
                    //objReport.gViewReport1(sFileName, CrystalReportViewer1, mds,
                    //        strNameReport, strValuesrpt, null, out reportDocument);
                }
                return true;
            }
            catch (Exception ex)
            {   
                strError= ex.Message;
                return false;                
            }
        }


        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream oStream = new MemoryStream(); // using System.IO
                ReportDocument crReportDocument = new ReportDocument();
                
                if (!ms_InitReportCondition(out crReportDocument))
                {
                    objForm.MessboxWeb(this.Page, strError);
                    return;
                }                                               

                
                ////Response.Buffer = false;                
                ////Response.ClearContent();
                ////Response.ClearHeaders();
                ////try
                ////{
                ////    crReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "AAAA");                                        
                ////    //(MemoryStream)CrystalReportViewer1.ReportSource.e(ExportFormatType.ExcelRecord));
                ////}
                ////catch (Exception ex)
                ////{
                ////    Console.WriteLine(ex.Message);
                ////    ex = null;
                ////}

                                
                //// Stop buffering the response
                //Response.Buffer = false;
                //// Clear the response content and headers
                //Response.ClearContent();
                //Response.ClearHeaders();                
                //// Export the Report to Response stream in PDF format and file name Customers
                //crReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Customers");



                string strExport = ddlFileType.SelectedItem.Text;
                strExport = "Portable Document (PDF)";
                if (strExport.Trim() == "Rich Text (RTF)")
                {
                    oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);                                   
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/rtf";
                }
                else if (strExport.Trim() == "Portable Document (PDF)")
                {
                    oStream = (System.IO.MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    //oStream = (System.IO.MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                }
                else if (strExport.Trim() == "MS Word (DOC)")
                {
                    oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/doc";

                    //oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.ContentType = "application/msword";                    
                }
                else if (strExport.Trim() == "MS Excel (XLS)")
                {                    
                    //oStream = (System.IO.MemoryStream)((ReportDocument)CrystalReportViewer1.ReportSource.ExportToStream(ExportFormatType.Excel));
                    //oStream1.Read(dataArray, 0, Convert.ToInt32(oStream1.Length));
                    //FileStream fileStream = new FileStream(fileName, System.IO.FileMode.Create);
                    //fileStream.Write(dataArray, 0, dataArray.Length);
                    //fileStream.Close();
                    //oStream1.Close();

                    oStream = (System.IO.MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                }
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<BR>");
                Response.Write(ex.Message.ToString());
            }
        }

    }
}

