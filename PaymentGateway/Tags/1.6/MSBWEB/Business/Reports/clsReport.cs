using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.OracleClient;
using System.Data.Common;
using System.Web;
using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using BIDVWEB.Comm; 
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using Microsoft.ReportingServices.WebServer;
using BIDVWEB.Business.Web;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using BIDVWEB.Business.UserRight;
//using CrystalReportViewerExportLib;
//using CrystalActiveXReportViewerLib11_5;
//using CRAXDDRT;

namespace BIDVWEB.Business.Reports
{
    public class clsReport
    {                
        public string gv_ReportTitle;
        public string strError;
        public string strNameReport;        
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsRunStore objRunStore = new clsRunStore();
        private clsCommon objComm = new clsCommon();
        private clsBranch objBranch = new clsBranch();
        public HttpContext Current;
        public ReportDocument greportDocument = new ReportDocument();
        private IBPS_MSG_CONTENTInfo objIBPS_Info = new IBPS_MSG_CONTENTInfo();
        private SWIFT_MSG_CONTENTInfo objSWIFT_Info = new SWIFT_MSG_CONTENTInfo();
        private VCB_MSG_CONTENTInfo objVCB_Info = new VCB_MSG_CONTENTInfo();
        private IBPS_MSG_CONTENTController objIBPS_BO = new IBPS_MSG_CONTENTController();
        private SWIFT_MSG_CONTENTController objSWIFT_BO = new SWIFT_MSG_CONTENTController();
        private VCB_MSG_CONTENTController objVCB_BO = new VCB_MSG_CONTENTController();
        private clsUser objUser = new clsUser();
        
        private TTSP_MSG_CONTENTInfo objTTSP_Info = new TTSP_MSG_CONTENTInfo(); //CANHDM
        private TTSP_MSG_CONTENTController objTTSP_BO = new TTSP_MSG_CONTENTController();//CANHDM
        //private CrystalActiveXReportViewerLib11_5.ReportSourceRouter objRSR = 
        //    new CrystalActiveXReportViewerLib11_5.ReportSourceRouter();
        //CrystalActiveXReportViewerLib11_5.CrystalActiveXReportViewer objCAXRV =
        //    new CrystalActiveXReportViewer();                

        # region Properties

        public string sgv_ReportTitle
        {
            get { return this.gv_ReportTitle; }
            set { this.gv_ReportTitle = value; }
        }

        public string strReportName
        {
            get { return this.strNameReport; }
            set { this.strNameReport = value; }
        }

        public string sError
        {
            get { return this.strError; }
            set { this.strError = value; }
        }
               

        #endregion


        //Ham Lay All CITAD////////////////////////////////////////////
        //Mo ta:        Ham Lay All CITAD
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        public string GetAllCitad()
        {
            try
            {                
                string strSql = "";
                string strTad = "";
                DataSet ds = new DataSet();
                DataRow dr;
                


                strSql = "SELECT TADID, TAD,SIBS_BANK_CODE FROM TAD";                
                ds = objDataAccess.dsGetDataSourceByStr(strSql,"");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        strTad = strTad + ",'" + dr["SIBS_BANK_CODE"].ToString() + "'";
                    }
                }
                if (strTad.Length > 0)
                {
                    if (strTad.Substring(0, 1) ==",")
                    {
                        strTad = strTad.Substring(1, strTad.Length - 1);
                    }                    
                    if (strTad.Substring(strTad.Length-1, 1) == ",")
                    {
                        strTad = strTad.Substring(0, strTad.Length - 2);
                    }
                }
                return strTad;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }


        //Ham gan cac parameter cho bao cao crystal////////////////////
        //Mo ta:        Ham gan cac parameter cho bao cao crystal
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      obj: Mang parameter
        //Dau ra:       Gan thanh cong cac parameter cho bao cao
        ///////////////////////////////////////////////////////////////
        public void g_InsertParamFields(ParameterFields ParamFields)
        {                    
            DataSet dsData = new DataSet();
            DataRow drData ;            
            string strSql="";
            string strReportProfile ="";            
            int iCount =0;
            long lngIDReport =0;

            //Kiem tra ten bao cao <> ""            
            if (strReportProfile.Trim() != "")
            {
                strSql = "SELECT ID_yFunction FROM AD_Function WHERE sFunctionCode = '" + strReportProfile + "'";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData.Tables.Count > 0)
                {
                    lngIDReport = Convert.ToInt32(dsData.Tables[0].Rows[0].Field<long>(0));
                }
            }
            strSql = "SELECT * FROM Report_Profile WHERE FK_yUser = " +
                        "(SELECT TOP 1 ID_yUser FROM AD_User WHERE sUserName = N'" + Current.User.Identity.Name.ToString() + "')";
            if (lngIDReport > 0)
            {
                strSql = strSql + " AND FK_yReport = " + lngIDReport.ToString();
            }
            dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");

            //Them 4 dong tham so nguoi phe duyet bao cao mac dinh vao 
            if (dsData.Tables.Count > 0)
            {
                drData = dsData.Tables[0].Rows[0];
            }
            else
            {
                strSql = "SELECT * FROM Report_Profile WHERE FK_yUser = (SELECT TOP 1 ID_yUser FROM AD_User WHERE sUserName = N'Administrator')";
                if (lngIDReport > 0)
                {
                    strSql += " AND FK_yReport = " + lngIDReport;
                }
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    drData = dsData.Tables[0].Rows[0];
                }
                else
                {
                    drData = dsData.Tables[0].NewRow();
                }
            }

            ParameterField paramField = new ParameterField();
            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();

            for (int i=1;i<=4;i++)
            {
                //Chuc vu nguoi phe duyet
                //ParameterField paramField;// = new ParameterField;
                paramField.ParameterFieldName = "rptTitle" + i.ToString();
                //ParameterDiscreteValue discreteVal;// = new ParameterDiscreteValue;
                discreteVal.Value = objComm.g_sCheckString(drData["sUserTitle"].ToString() + i.ToString());
                paramField.CurrentValues.Add(discreteVal);
                ParamFields.Add(paramField);

                //Ten nguoi phe duyet
                //ParameterField paramField = new ParameterField();
                paramField.ParameterFieldName = "rptName" + iCount.ToString();
                //ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                discreteVal.Value = objComm.g_sCheckString(drData["sUserName"].ToString() + iCount.ToString());
                paramField.CurrentValues.Add(discreteVal);
                ParamFields.Add(paramField);
            }                            
        }


        //Thuc hien tao Dataset cho bao cao crystal////////////////////
        //Mo ta:        Thuc hien tao Dataset cho bao cao crystal
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      ds: Dataset tra ra
        //              strNR: Ten bao cao
        //              strValue: Mang gia tri truyen cho store
        //              strValueReturn: Xau gia tri tra ve gan cho rpt
        //Dau ra:       True: Thanh cong, False: Ko thanh cong
        ///////////////////////////////////////////////////////////////        
        public bool DoCreateReport(out DataSet ds, string strNR, string strValue,
            out string strValueReturn)
        {           
            DataSet dsData = new DataSet();
            DataSet dsBranch = new DataSet();
            string sUserID = "";
            string sBranch = "";
            string strSPName = "";            
            string strNameReport = "";            
            string strDataType = "";
            char[] splitter = { '|' };
            string[] arrayValue;
            bool bError = false;            
            ds = null;
            strValueReturn = "";

            try
            {                                   
                strSPName = GetSPName(strNR,1);                
                if (!GetParaSP(strSPName, out strNameReport, out strDataType))
                {                    
                    return false;
                }                                
                //Goi ham tao dataset
                strSPName = GetSPName(strNR,2);
                objRunStore.RunStorePro(strSPName, strNameReport, strValue, strDataType,
                    out dsData, out strValueReturn);
                //Lay user id
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                //Luu danh sach gia tri vao session                    
                SessionHelper.Store("strValues", strValueReturn);
                //objDataAccess.RunStorePro(strSPName, strNameReport, strValue, strDataType, out dsData);
                //Kiem tra xem co du lieu
                if (dsData != null && dsData.Tables.Count > 0)
                {
                    ds = dsData;
                    //Cap nhat trang thai in dien di
                    if (strNR == "IBPS_PRINT_1")
                    {                        
                        dsBranch = objUser.GetBranchByUserID(sUserID);
                        sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                        if (CheckBranch_TAD(sBranch) > 0)
                        {
                            arrayValue = strValue.Split(splitter);
                            objIBPS_Info.MSG_ID = Convert.ToInt64(arrayValue[1].ToString());
                            if (objIBPS_BO.Check_Print_STS(objIBPS_Info) == 0)
                            {
                                objIBPS_Info.PRINT_STS = 1;
                                objIBPS_BO.Update_Print_STS(objIBPS_Info);
                            }
                        }
                    }
                    //Cap nhat trang thai in dien den
                    else if (strNR == "IBPS_PRINT_2")
                    {
                        arrayValue = strValue.Split(splitter);
                        objIBPS_Info.MSG_ID = Convert.ToInt64(arrayValue[1].ToString());
                        if (objIBPS_BO.Check_Print_STS(objIBPS_Info) == 0)
                        {
                            objIBPS_Info.PRINT_STS = 1;
                            objIBPS_BO.Update_Print_STS(objIBPS_Info);
                        }
                    }
                    //Cap nhat trang thai in dien swift di
                    else if (strNR == "SWIFT_PRINT")
                    {
                        arrayValue = strValue.Split(splitter);
                        objSWIFT_Info.MSG_ID = Convert.ToInt64(arrayValue[0].ToString());
                        //Dien di
                        if (objSWIFT_Info.MSG_DIRECTION == "SIBS-SWIFT")
                        {
                            dsBranch = objUser.GetBranchByUserID(sUserID);
                            sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                            if (sBranch.PadLeft(5, '0') == "00011")
                            {
                                if (objSWIFT_BO.Check_Print_STS(objSWIFT_Info) == 0)
                                {
                                    objSWIFT_Info.PRINT_STS = 1;
                                    objSWIFT_BO.Update_Print_STS(objSWIFT_Info);
                                }
                            }
                        }
                        //Dien den
                        else
                        {
                            if (objSWIFT_BO.Check_Print_STS(objSWIFT_Info) == 0)
                            {
                                objSWIFT_Info.PRINT_STS = 1;
                                objSWIFT_BO.Update_Print_STS(objSWIFT_Info);
                            }
                        }
                    }
                    //Cap nhat trang thai in dien swift den
                    else if (strNR == "SWIFT_PRINT_TF")
                    {
                        arrayValue = strValue.Split(splitter);
                        objSWIFT_Info.MSG_ID = Convert.ToInt64(arrayValue[0].ToString());
                        //Dien di
                        if (objSWIFT_Info.MSG_DIRECTION == "SIBS-SWIFT")
                        {
                            dsBranch = objUser.GetBranchByUserID(sUserID);
                            sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                            if (sBranch.PadLeft(5, '0') == "00011")
                            {
                                if (objSWIFT_BO.Check_Print_STS(objSWIFT_Info) == 0)
                                {
                                    objSWIFT_Info.PRINT_STS = 1;
                                    objSWIFT_BO.Update_Print_STS(objSWIFT_Info);
                                }
                            }
                        }
                        //Dien den
                        else
                        {
                            if (objSWIFT_BO.Check_Print_STS(objSWIFT_Info) == 0)
                            {
                                objSWIFT_Info.PRINT_STS = 1;
                                objSWIFT_BO.Update_Print_STS(objSWIFT_Info);
                            }
                        }
                    }
                    //Cap nhat trang thai in dien vcb den
                    else if (strNR == "VCB_PRINT_1")
                    {
                        arrayValue = strValue.Split(splitter);
                        objVCB_Info.MSG_ID = Convert.ToInt64(arrayValue[0].ToString());
                        if (objVCB_BO.Check_Print_STS(objVCB_Info) == 0)
                        {
                            objVCB_Info.PRINT_STS = 1;
                            objVCB_BO.Update_Print_STS(objVCB_Info);
                        }
                    }
                    //Cap nhat trang thai in dien vcb di
                    else if (strNR == "VCB_PRINT_2")
                    {
                        dsBranch = objUser.GetBranchByUserID(sUserID);
                        sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                        if (sBranch.PadLeft(5, '0') == "00011")
                        {
                            arrayValue = strValue.Split(splitter);
                            objVCB_Info.MSG_ID = Convert.ToInt64(arrayValue[0].ToString());
                            if (objVCB_BO.Check_Print_STS(objVCB_Info) == 0)
                            {
                                objVCB_Info.PRINT_STS = 1;
                                objVCB_BO.Update_Print_STS(objVCB_Info);
                            }
                        }
                    }
                    //canhdm: begin
                    //Cap nhat trang thai in dien TTSP den
                    else if (strNR == "TTSP_PRINT_1")
                    {
                        arrayValue = strValue.Split(splitter);
                        objTTSP_Info.MSG_ID = Convert.ToInt64(arrayValue[0].ToString());
                        if (objTTSP_BO.Check_Print_STS(objTTSP_Info) == 0)
                        {
                            objTTSP_Info.PRINT_STS = 1;
                            objTTSP_BO.Update_Print_STS(objTTSP_Info);
                        }
                    }
                    //Cap nhat trang thai in dien TTSP di
                    else if (strNR == "TTSP_PRINT_2")
                    {
                        dsBranch = objUser.GetBranchByUserID(sUserID);
                        sBranch = dsBranch.Tables[0].Rows[0]["BRANCH"].ToString();
                        if (sBranch.PadLeft(5, '0') == "00011")
                        {
                            arrayValue = strValue.Split(splitter);
                            objTTSP_Info.MSG_ID = Convert.ToInt64(arrayValue[0].ToString());
                            if (objTTSP_BO.Check_Print_STS(objTTSP_Info) == 0)
                            {
                                objTTSP_Info.PRINT_STS = 1;
                                objTTSP_BO.Update_Print_STS(objTTSP_Info);
                            }
                        }
                    }
                    //canhdm:end
                }
                else
                {
                    strError = objRunStore.strError;                    
                    //return false;
                }
                bError = true;
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return false;
            }            
        }
                

        public void gViewReport1Test(string sReportName,
            CrystalDecisions.Web.CrystalReportViewer ReportViewer,
            DataSet dsDataSet, string arrName, string arrValue,
            DataSet dsSubReport, out ReportDocument reportDoc)
        {
            strError = "";
            reportDoc = null;
            try
            {
                ParameterFields paramFields = new ParameterFields();
                ReportDocument reportDocument = new ReportDocument();
                ParameterDiscreteValue param = new ParameterDiscreteValue();
                ParameterValues values = new ParameterValues();
                string[] arrayName;
                string[] arrayValue;
                string sArrName = "";
                string sArrValue = "";
                char[] splitter = { '|' };

                //Lay duong dan file report
                reportDocument.Load(HttpContext.Current.Server.MapPath(sReportName));
                //for (int i = 0; i < (reportDocument.ParameterFields.Count) / 2; i++)
                //{
                //    sArrName = sArrName + reportDocument.ParameterFields[i].Name.ToString() + "|";                 
                //}
                for (int i = 0; i < (reportDocument.ParameterFields.Count) / 2; i++)
                {                    
                    sArrName = sArrName + "v" + reportDocument.ParameterFields[i].Name.ToString() + "|";
                }                
                sArrName = sArrName.Substring(0, sArrName.Length - 1);
                //sArrValue = arrValue + "|" + arrValue;
                //neu co bien dau vao
                if (sArrName != "" && sArrValue != "")
                {
                    //Tao mang tham so, mang gia tri 
                    arrayName = sArrName.Split(splitter);
                    arrayValue = arrValue.Split(splitter);
                    if (arrayName.Length == arrayValue.Length)
                    {
                        for (int i = 0; i <= arrayValue.Length - 1; i++)
                        {
                            ParameterField paramField = new ParameterField();
                            paramField.ParameterFieldName = arrayName[i];
                            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();                            
                            discreteVal.Value = arrayValue[i];
                            paramField.CurrentValues.Add(discreteVal);
                            paramFields.Add(paramField);
                        }
                    }
                }
                //Gan DataSource                
                reportDocument.SetDataSource(dsDataSet.Tables[0]);
                reportDocument.Refresh();
                ReportViewer.ParameterFieldInfo = paramFields;
                ReportViewer.ReportSource = reportDocument;                
                ReportViewer.DataBind();

                //Sua gan parameter trong reportDocument
                reportDoc = reportDocument;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }

        //Thuc hien gan cac parameter cho crystalreportviewer//////////
        //Mo ta:        Thuc hien gan cac parameter cho crystalreportviewer
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      sReportName: Tieu de report
        //              ReportViewer: 
        //              dsDataSet: Dataset chua du lieu
        //              arrName: Mang luu cac bien
        //              arrValue: Mang luu gia tri
        //              sFormular: Mang luu cong thuc
        //Dau ra:       Tao bao cao thanh cong
        ///////////////////////////////////////////////////////////////
        public void gViewReport1(string sReportName, 
            CrystalDecisions.Web.CrystalReportViewer ReportViewer,
            DataSet dsDataSet, string arrName, string arrValue, 
            DataSet dsSubReport,out ReportDocument reportDoc)
        {
            strError = "";
            reportDoc = null;
            try
            {
                ParameterFields paramFields = new ParameterFields();                
                ReportDocument reportDocument = new ReportDocument();
                ParameterDiscreteValue param = new ParameterDiscreteValue();
                ParameterValues values = new ParameterValues();
                string[] arrayName;
                string[] arrayValue;
                char[] splitter = { '|' };

                //Lay duong dan file report
                reportDocument.Load(HttpContext.Current.Server.MapPath(sReportName));
                reportDocument.SetDataSource(dsDataSet.Tables[0]);                
                reportDocument.Refresh();

                //neu co bien dau vao
                if (arrName != "" && arrValue != "")
                {                    
                    //Tao mang tham so, mang gia tri 
                    arrayName = arrName.Split(splitter);
                    arrayValue = arrValue.Split(splitter);
                    if (arrayName.Length == arrayValue.Length)
                    {
                        for (int i = 0; i <= arrayValue.Length - 1; i++)
                        {
                            ParameterField paramField = new ParameterField();
                            paramField.ParameterFieldName =  arrayName[i];
                            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                            if (arrayName[i].ToString().ToUpper() == "sBranchReport".ToUpper())
                            {
                                discreteVal.Value = objBranch.GetBranchName(arrayValue[i].ToString());
                                reportDocument.SetParameterValue(arrayName[i],
                                    objBranch.GetBranchName(arrayValue[i].ToString()));
                            }
                            else
                            {
                                if (arrayValue[i] == null || arrayValue[i].ToString().Trim() == "")
                                {
                                    discreteVal.Value = "";
                                    reportDocument.SetParameterValue(arrayName[i], "");
                                }
                                else
                                {
                                    discreteVal.Value = arrayValue[i];
                                    reportDocument.SetParameterValue(arrayName[i], arrayValue[i]);
                                }
                            }
                            paramField.CurrentValues.Add(discreteVal);
                            paramFields.Add(paramField);                            
                        }                        
                    }
                }
                //Add formular              

                //Gan DataSource Subreport
                //if (dsSubReport != null)
                //{
                //    reportDocument.Subreports[1].SetDataSource(dsSubReport.Tables[0]);                    
                //}
                
                //Gan DataSource                
                ReportViewer.ParameterFieldInfo = paramFields;
                ReportViewer.ReportSource = reportDocument;                
                //ReportViewer.DataBind();

                //Sua gan parameter trong reportDocument
                //ReportViewer.ParameterFieldInfo = paramFields;
                reportDoc = reportDocument;
                //ExportReport(reportDocument, "C:\\test.xls");  
                //ExportReport("Portable Document (PDF)", reportDocument);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }            
        }

        //Thuc hien in nhieu dien//////////////////////////////////////
        //Mo ta:        Thuc hien in nhieu dien
        //Ngay tao:     09/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strNameReport:  Tieu de report        
        //              ReportViewer:   CrystalReportViewer
        //              dsDataSet       dataset
        //Dau ra:       View ma hinh in dien
        ///////////////////////////////////////////////////////////////
        public void gViewReport2(string sReportName, 
            CrystalDecisions.Web.CrystalReportViewer ReportViewer,
            DataSet dsDataSet,
            out ReportDocument reportDoc)
        {
            strError = "";
            reportDoc = null;
            try
            {   
                ReportDocument reportDocument = new ReportDocument();
                //Lay duong dan file report
                reportDocument.Load(HttpContext.Current.Server.MapPath(sReportName));
                reportDocument.SetDataSource(dsDataSet.Tables[0]);
                reportDocument.Refresh();
                //Gan DataSource
                ReportViewer.ReportSource = reportDocument;
                ReportViewer.DataBind();
                reportDoc = reportDocument;
                ReportViewer.Dispose();

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }

        //Thuc hien tao Dataset cho bao cao MS/////////////////////////
        //Mo ta:        Thuc hien tao Dataset cho bao cao MS
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strNameReport: Tieu de report        
        //Dau ra:       Tra ra dataset
        ///////////////////////////////////////////////////////////////
        public bool GetDataReportMS(out DataSet ds)
        {
            string strSQLStatement = "";
            bool bError = false;
            DataSet dsData = new DataSet();

            try
            {
                strSQLStatement = "SELECT * FROM IBPS_MSG_CONTENT WHERE ROWNUM<=1000";                
                if (strSQLStatement != "" && strSQLStatement != null)
                {
                    dsData = objDataAccess.dsGetDataSourceByStr(strSQLStatement, "");
                    if (dsData.Tables[0].Rows.Count == 0)
                    {
                        bError = false;
                    }
                }
                else
                {
                    bError = false;
                }

                //Ket thuc viec tao dataset
                if (dsData != null && dsData.Tables.Count > 0)
                {
                    ds = dsData;
                }
                else
                {
                    ds = null;
                }
                bError = true;
                return bError;                
            }
            catch(Exception ex)
            {
                strError = ex.Message;
                ds = null;
                return false;
            }        
        }


        //Thuc hien tao bao cao MS/////////////////////////////////////
        //Mo ta:        Thuc hien tao bao cao MS
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Tao bao cao thanh cong
        ///////////////////////////////////////////////////////////////
        public void gViewReportMS(string sReportName, 
            Microsoft.Reporting.WebForms.ReportViewer msReportViewer, DataSet dsDataSet)
        {
            //rpvAbraKaDabra.LocalReport.ReportEmbeddedResource =
            //"rsWin101.rptProductList.rdlc";            
            //ReportDataSource rds = new ReportDataSource();
            //rds.Name = "dsProduct_dtProductList";
            //rds.Value = dsReport.Tables[0];
            //rpvAbraKaDabra.LocalReport.DataSources.Add(rds);            
            //rpvAbraKaDabra.RefreshReport();
            //dsDataSet


            //DataTable dtReport = new DataTable();
            //dtReport = dsDataSet.Tables[0];
            //ReportDataSource rds = new ReportDataSource();
            //rds.Name = "";
            //rds.Value=dtReport;

            //msReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            DataSet ds = new DataSet();
            ds = dsDataSet;
            ReportDataSource rds = new ReportDataSource("IBPS_MSG_CONTENT", ds.Tables[0]);            
            msReportViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(sReportName);            
            //msReportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("IBPS_MSG_CONTENT", dsDataSet.Tables[0]));
            msReportViewer.LocalReport.DataSources.Clear();
            msReportViewer.LocalReport.DataSources.Add(rds);
            msReportViewer.LocalReport.Refresh();            
            
            //Dim dtReport As New DataTable = GetDataTable("Rpt1")
            //LocalReport.ReportPath = "Rpt1.rdlc"
            //Dim rdsReport As New ReportDataSource
            //rdsReport.Name = "Rpt1Data"
            //rdsReport.Value = dtReport
            //ReportViewer1.LocalReport.DataSources.Add(rdsReport)
            //ReportViewer1.LocalReport.Refresh()

            //ReportViewer1.LocalReport.ReportPath = "c:\My Reports\Departments.rdlc"
            ////'Supply a DataTable corresponding to each report data source.
            //Dim myReportDataSource = New ReportDataSource("Departments", LoadDepartmentsData())
            //ReportViewer1.LocalReport.DataSources.Add(myReportDataSource)          

            //msReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //string ReportServerLoaction = "http://localhost:3497/ReportCrystal/";
            ////msReportViewer.ServerReport.ReportServerUrl = new Uri(ReportServerLocation);
            //msReportViewer.ServerReport.ReportPath = HttpContext.Current.Server.MapPath(sReportName);
            ////msReportViewer.ServerReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("IBPS_MSG_CONTENT", dsDataSet.Tables[0]));
            //msReportViewer.ShowParameterPrompts = false;
            //msReportViewer.ShowPrintButton = true;
            //msReportViewer.ServerReport.Refresh();
        }
        

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay tieu de bao cao
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra tieu de report
        ///////////////////////////////////////////////////////////////
        public string GetTitleReport(string strReportName, bool bRN)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strReportName.Trim() == "")
            {
                return "";
            }
            if (bRN == true)
            {
                //strSql = "SELECT (GWTYPE || ': ' || TITLE) AS TITLE " +                    
                //strSql = "SELECT (REPORTNAME || ': ' || TITLE) AS TITLE " +
                strSql = "SELECT TITLE FROM LIST_REPORT " +
                    "WHERE REPORTNAME = '" + strReportName + "'";
            }
            else
            {
                strSql = "SELECT TITLE " +
                    "FROM LIST_REPORT WHERE REPORTNAME = '" + strReportName + "'";
            }
            ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dr = ds.Tables[0].Rows[0];
                return dr["TITLE"].ToString();
            }
            else
            {
                return "";
            }            
        }

        //Ham kiem tra quyen xem bao cao theo chi nhanh////////////////
        //Mo ta:        Ham kiem tra quyen bao cao theo chi nhanh
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra quyen dc xem bao cao
        ///////////////////////////////////////////////////////////////
        public bool CheckPermissionReport(string strReportName, string sReportDate, out string strDayNum)
        {
            strDayNum = "";
            try
            {
                string strSql = "";
                string strBranch = "";
                DataSet ds = new DataSet();
                DataRow dr;

                strBranch = GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                strSql = "SELECT A.* FROM SYSINFO A WHERE A.REPORTNAME = '" + strReportName +
                    "' AND A.SIBS_BANK_CODE='" + strBranch + "' AND " +
                    "to_date(sysdate-to_number(A.time),'DD/MM/YYYY') > to_date('" + sReportDate + "','DD/MM/YYYY')";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    if (Convert.ToInt16(dr["TIME"]) > 0)
                    {
                        strDayNum = dr["TIME"].ToString();
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        //Lay ten control nhap ngay bao cao////////////////////////////
        //Mo ta:        Lay ten control nhap ngay bao cao
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra ten control ngay 
        ///////////////////////////////////////////////////////////////
        public string GetControlDate(string strReportName)
        {            
            try
            {
                string strSql = "";
                string strControl = "";
                DataSet ds = new DataSet();
                DataRow dr;

                strSql = "SELECT A.* FROM LIST_REPORT_PARA_CTL A WHERE A.REPORTNAME = '" + strReportName +
                    "' AND A.CTLNAME='textdate' and A.ENABLE=1 and A.ISCHECK_DATE=1";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    if (dr["CTLNAME"]==null)
                    {
                        return "";   
                    }
                    else
                    {                        
                        strControl = dr["CTLNAME"].ToString();
                        return strControl;
                    }                    
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }


        //Ham kiem tra quyen xem bao cao theo chi nhanh////////////////
        //Mo ta:        Ham kiem tra quyen bao cao theo chi nhanh
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra quyen dc xem bao cao
        ///////////////////////////////////////////////////////////////
        public int CheckViewReport_Branch(string sTableName)
        {           
            try
            {
                string strSql = "";
                string strBranch = "";
                DataSet ds = new DataSet();         


                strBranch = GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                if (sTableName.ToUpper() == "TAD")
                {
                    strBranch = GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                    strSql = "SELECT * FROM " + sTableName +
                    " WHERE LPAD(TRIM(SIBS_CODE),5,'0') = LPAD(TRIM('" + strBranch + "'),5,'0')";
                }
                else
                {
                    strSql = "SELECT * FROM " + sTableName +
                    " WHERE LPAD(TRIM(SIBS_BANK_CODE),5,'0') = LPAD(TRIM('" + strBranch + "'),5,'0')";
                }
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }


        //Ham lay ten sp cho bao cao///////////////////////////////////
        //Mo ta:        Ham lay ten sp cho bao cao
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra tieu de report
        ///////////////////////////////////////////////////////////////
        public string GetSPName(string strReportName, int iType)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strReportName.Trim() == "")
            {
                return "";
            }
            if (iType == 1)
            {
                strSql = "SELECT SPNAME FROM LIST_REPORT WHERE REPORTNAME = '" + strReportName + "'";
            }
            else
            {
                strSql = "SELECT SPNAME1 as SPNAME FROM LIST_REPORT WHERE REPORTNAME = '" + strReportName + "'";
            }
            ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dr = ds.Tables[0].Rows[0];
                return dr["SPNAME"].ToString();
            }
            else
            {
                return "";
            }
        }


        //Ham lay parameter cho SP cua bao cao/////////////////////////
        //Mo ta:        Ham lay parameter cho SP cua bao cao
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra tieu de report
        ///////////////////////////////////////////////////////////////
        public bool GetParaSP(string strReportName, out string strPara, 
            out string strDataType)
        {
            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                DataRow dr;

                strPara = "";
                strDataType = "";
                if (strReportName.Trim() == "")
                {
                    return false;
                }
                strSql = "SELECT * FROM LIST_REPORT_PARA_SP WHERE REPORTNAME = '" + 
                    strReportName + "' AND ISSHOW=1 " + " ORDER BY IORDER ASC " ;
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        if (strPara.Length>0)
                        {
                            strPara = strPara + "|" + dr["PARA_NAME"];
                            strDataType = strDataType + "|" + dr["PARA_TYPE"];                            
                        }
                        else
                        {
                            strPara = strPara + dr["PARA_NAME"];
                            strDataType = strDataType + dr["PARA_TYPE"];
                        }
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                strPara = "";
                strDataType = "";
                return false;
            }            
        }


        //Ham lay parameter cho file RPT///////////////////////////////
        //Mo ta:        Ham lay parameter cho file RPT      
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra tieu de report
        ///////////////////////////////////////////////////////////////
        public bool GetParaRPT(string strReportName, out string strPara)
        {
            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                DataRow dr;

                strPara = "";                
                if (strReportName.Trim() == "")
                {
                    return false;
                }
                strSql = "SELECT * FROM LIST_REPORT_PARA_RPT WHERE REPORTNAME = '" +
                    strReportName + "' AND ISSHOW=1 " + " ORDER BY IORDER ASC ";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        if (strPara.Length > 0)
                        {
                            strPara = strPara + "|" + dr["PARA_NAME"];                            
                        }
                        else
                        {
                            strPara = strPara + dr["PARA_NAME"];                            
                        }
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                strPara = "";                
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay bang ke tong quat
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strReportName: Ten report
        //Dau ra:       Tra ra tieu de report
        ///////////////////////////////////////////////////////////////
        public DataSet GetDataSwift05(string sDate)
        {            
            DataSet dsData = new DataSet();
            string strSPName = "";            
            string strNameReport = "";
            string strValue = "";            
            string strValueReturn = "";
            string strDataType = "";            

            try
            {
                strError = "";
                dsData = null;                

                strSPName = "GW_PK_SWIFT_REPORT.SWIFT05";
                strNameReport = "pTYPE|pTRAN_DATE|RefCurSWIFT05";
                strValue = "1|" + Convert.ToDateTime(objComm.g_Formatdate(sDate, false))
                    + "|RefCurSWIFT05";
                strDataType = "OracleDbType.Int16|OracleDbType.Date|" +
                    "OracleDbType.RefCursor";

                //Goi ham tao dataset
                objRunStore.RunStorePro(strSPName, strNameReport, strValue, strDataType, 
                    out dsData, out strValueReturn);
                if (dsData == null || dsData.Tables[0].Rows.Count == 0)
                {
                    strError = objRunStore.strError;
                    return null;
                }
                //Tra ve dataset
                return dsData;                                                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;                
            }
        }


        //Ham lay chi nhanh cua user login/////////////////////////////
        //Mo ta:        Ham lay chi nhanh cua user login
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strUser: Ten truy cap
        //Dau ra:       Tra ra chi nhanh cua nguoi truy cap
        ///////////////////////////////////////////////////////////////
        public string GetBranchOfUserName(string strUser)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strUser.Trim() == "")
            {
                return "";
            }
            else
            {
                strSql = "SELECT B.BRAN_NAME FROM USERS A LEFT JOIN BRANCH B " +
                    " ON A.BRANCH=B.SIBS_BANK_CODE WHERE A.USERNAME = '" + strUser + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["BRAN_NAME"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        //Ham lay chi nhanh cua user login/////////////////////////////
        //Mo ta:        Ham lay chi nhanh cua user login
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strUser: Ten truy cap
        //Dau ra:       Tra ra chi nhanh cua nguoi truy cap
        ///////////////////////////////////////////////////////////////
        public string GetBranchCodeOfUserName(string strUser)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strUser.Trim() == "")
            {
                return "";
            }
            else
            {
                strSql = "SELECT B.SIBS_BANK_CODE FROM USERS A LEFT JOIN BRANCH B " +
                    " ON A.BRANCH=B.SIBS_BANK_CODE WHERE A.USERID = '" + strUser + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["SIBS_BANK_CODE"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        //Get ma 8 so cua chi nhanh citad
        public string GetTAD8(string strTAD3)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strTAD3.Trim() == "")
            {
                return "";
            }
            else
            {
                strSql = "SELECT A.gw_bank_code FROM TAD A " +
                    "WHERE LPAD(A.sibs_code,5,'0') = '" + strTAD3.PadLeft(5,'0') + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["gw_bank_code"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        //Get ma 8 so cua chi nhanh citad
        public string GetBranch8(string strBranch3)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strBranch3.Trim() == "")
            {
                return "";
            }
            else
            {
                strSql = "SELECT A.gw_bank_code FROM IBPS_BANK_MAP A " +
                    "WHERE LPAD(A.sibs_bank_code,5,'0') = '" + strBranch3.PadLeft(5, '0') +
                    "' AND substr(A.gw_bank_code,3,3)='302'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["gw_bank_code"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        //Ham lay ten chi nhanh bao cao////////////////////////////////
        //Mo ta:        Ham lay ten chi nhanh bao cao
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strBranchCode: Ma chi nhanh
        //Dau ra:       Tra ra chi nhanh cua nguoi truy cap
        ///////////////////////////////////////////////////////////////
        public string GetBranchNameByCode(string strBranchCode)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            if (strBranchCode.Trim() == "")
            {
                return "";
            }
            else
            {
                strSql = "SELECT A.BRAN_NAME FROM BRANCH A WHERE A.SIBS_BANK_CODE = '" + strBranchCode + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["BRAN_NAME"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

         
        public void ExportReport(ReportDocument[] reportDocument,string strfile)
        {
            try
            {                
                DiskFileDestinationOptions diskOpts =
                ExportOptions.CreateDiskFileDestinationOptions();
                diskOpts.DiskFileName = strfile;
                ExportOptions[] exportOpts = new ExportOptions[reportDocument.Length];
                ExportOptions exportOpts1 = new ExportOptions();
                for (int i = 0; i < reportDocument.Length; i++)
                {
                    exportOpts[i].ExportFormatType = ExportFormatType.PortableDocFormat;
                    exportOpts[i].ExportDestinationType = ExportDestinationType.DiskFile;
                    exportOpts[i].ExportDestinationOptions = diskOpts;
                    reportDocument[i].Export(exportOpts[i]);
                }
                

                //////DiskFileDestinationOptions diskOpts =
                //////ExportOptions.CreateDiskFileDestinationOptions();
                //////diskOpts.DiskFileName = strfile;
                //////ExportOptions exportOpts = new ExportOptions();
                //////exportOpts.ExportFormatType = ExportFormatType.Excel;                
                //////exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                //////exportOpts.ExportDestinationOptions = diskOpts;
                //////reportDocument.Export(exportOpts);

                //////// Creates a new Excel Application
                //////Microsoft.Office.Interop.Excel.Application excelApp =
                //////    new Microsoft.Office.Interop.Excel.ApplicationClass();
                //////// Makes Excel visible to the user.
                //////excelApp.Visible = true;
                //////// The following code opens an existing workbook
                //////Microsoft.Office.Interop.Excel.Workbook excelWorkbook =
                //////    excelApp.Workbooks.Open(strfile, 0, false, 5, "", "", false,
                //////    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
                //////    false, 0, true, false, false);
                //////// The following gets the Worksheets collection
                //////Microsoft.Office.Interop.Excel.Sheets excelSheets = excelWorkbook.Worksheets;
                //////// The following gets Sheet1 for editing
                //////string currentSheet = "Sheet1";
                //////Microsoft.Office.Interop.Excel.Worksheet excelWorksheet =
                //////    (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
                //////Microsoft.Office.Interop.Excel.Range cell = excelWorksheet.UsedRange;
                //////cell.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //////cell.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //////cell.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //////cell.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //////excelWorkbook.Save();
                //////excelWorkbook.Close(Type.Missing, strfile, Type.Missing);
                //////exportOpts.DestinationOptions = diskOpts;
                //////excelWorkbook = null;
                //////excelSheets = null;
                //////excelApp = null;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }             
        }

        public void ExportReport(string strExport, ReportDocument crReportDocument)
        {
            MemoryStream oStream = new MemoryStream(); // using System.IO
            //Stream oStream; // using System.IO

            //switch (strExport) //this contains the value of the selected export format.
            //{
                //case "Rich Text (RTF)":
                //    oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.ContentType = "application/rtf";
                //case "Portable Document (PDF)":
                    oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                    Current.Response.Clear();
                    Current.Response.Buffer = true;
                    Current.Response.ContentType = "application/pdf";
                //case "MS Word (DOC)":
                //    oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.ContentType = "application/doc";
                //case "MS Excel (XLS)":
                //    oStream = (MemoryStream)crReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.ContentType = "application/vnd.ms-excel";            
            //}
            try
            {
                Current.Response.BinaryWrite(oStream.ToArray());
                Current.Response.End();
            }
            catch (Exception ex)
            {
                Current.Response.Write("<BR>");
                Current.Response.Write(ex.Message.ToString());
            }



            ////using (StreamWriter sw = new StreamWriter("C:\\test2.xls"))
            ////{
            ////    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            ////    {
            ////        //ReportViewer.RenderBeginTag(hw);
            ////        ReportViewer.RenderEndTag(hw);

            ////    }
            ////}   



            ////HttpContext.Current.Response.Buffer = false;
            ////HttpContext.Current.Response.ClearContent();
            ////HttpContext.Current.Response.ClearHeaders();
            ////reportDocument.ExportToHttpResponse(ExportFormatType.Excel, HttpContext.Current.Response, true, "AAAA");
            //////try
            //////{
            //////    reportDocument.ExportToHttpResponse(ExportFormatType.Excel, HttpContext.Current.Response, true, "AAAA");
            //////}
            //////catch (Exception ex)
            //////{
            //////    Console.WriteLine(ex.Message);
            //////    ex = null;
            //////}
        }

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham kiem tra chi nhanh la dau moi
        //Ngay tao:     04/2010
        //Nguoi tao:    Huypq7
        //Dau vao:      sBranch: Ma chi nhanh 3 so
        //Dau ra:       La chi nhanh dau moi
        ///////////////////////////////////////////////////////////////
        public int CheckBranch_TAD(string sBranch)
        {
            DataSet dsData = new DataSet();
            string strSql = "";

            try
            {
                strError = "";
                strSql = "SELECT * FROM TAD WHERE LPAD(SIBS_CODE,5,'0')=" + sBranch.PadLeft(5,'0');
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)        
                    return 1;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

    }

}
