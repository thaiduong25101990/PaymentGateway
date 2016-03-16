using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using BR.BRBusinessObject;
using BR.BRLib;



namespace BR.BRSYSTEM
{
    public partial class frmReport : Form
    {
        public string NameRPT;        
        public string sArrValue;
        public string strSubreport;
        private string strSql, sArrName, sArrDataType, strGWType;       

        public frmReport()
        {
            InitializeComponent();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            clsRunStore  objRunStore =new clsRunStore();
            try
            {
                strGWType = Common.gGWTYPE;
                strSql = "GW_PK_" + strGWType + "_REPORT." + NameRPT.Trim();
                ReportDocument docReport = new ReportDocument();

                //string path = System.Environment.CurrentDirectory;
                string path = Application.StartupPath;
                path = path + "\\rpt" + "\\" + NameRPT + ".rpt";
                docReport.Load(path);
                int k = docReport.Subreports.Count;
                if (k > 0)
                {
                    strSubreport = docReport.Subreports[0].Name.ToString().Trim();                    
                    strSubreport = strSubreport.Substring(0, strSubreport.Length - 4);
                }

                sArrName = "";
                sArrDataType = "";
                if (k > 0)
                {
                    for (int i = 0; i < (docReport.ParameterFields.Count) / 3; i++)
                    {
                        sArrName = sArrName + "v" + docReport.ParameterFields[i].Name.ToString() + "|";                        
                        sArrDataType = sArrDataType + docReport.ParameterFields[i].ParameterValueType.ToString() + "|";
                    }
                }
                else
                {
                    for (int i = 0; i < (docReport.ParameterFields.Count) / 2; i++)
                    {
                        sArrName = sArrName + "v" + docReport.ParameterFields[i].Name.ToString() + "|";                        
                        sArrDataType = sArrDataType + docReport.ParameterFields[i].ParameterValueType.ToString() + "|";
                    }
                }
                sArrValue = sArrValue + "|null";
                sArrName = sArrName + "RefCur" + NameRPT;
                sArrDataType = sArrDataType + "OracleDbType.RefCursor";
                DataSet dtReport = new DataSet();

                if (k > 0)
                {
                    DataSet dtSubReport = new DataSet();
                    //objRunStore.RunStorePro(strSql, sArrName, sArrValue, sArrDataType, out dtReport);
                    //objRunStore.RunStorePro(strSubreport, sArrName, sArrValue, sArrDataType, out dtSubReport);
                    objRunStore.RunStoreProcedure(strSql, sArrName, sArrValue, sArrDataType, out dtReport);
                    gViewSubReport1(ReportViewer, docReport, dtReport, dtSubReport, sArrName, sArrValue);
                }
                else
                {
                    //objRunStore.RunStorePro(strSql, sArrName, sArrValue, sArrDataType, out dtReport);
                    objRunStore.RunStoreProcedure(strSql, sArrName, sArrValue, sArrDataType, out dtReport);
                    gViewReport1(ReportViewer, docReport, dtReport, sArrName, sArrValue);
                }
                //ExportReport(docReport, NameRPT);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        public void gViewReport1(CrystalDecisions.Windows.Forms.CrystalReportViewer ReportViewer, ReportDocument reportDocument, DataSet dsDataSet, string arrName, string arrValue)
        {
            
            try
            {
                ParameterFields paramFields = new ParameterFields();                
                ParameterDiscreteValue param = new ParameterDiscreteValue();
                ParameterValues values = new ParameterValues();
                string[] arrayName;
                string[] arrayValue;
                char[] splitter = { '|' };
           
                if (arrName != "" && arrValue != "")
                {
                    //Tao mang tham so, mang gia tri 
                    arrayName = arrName.Split(splitter);
                    arrayValue = arrValue.Split(splitter);
                    if (arrayName.Length == arrayValue.Length)
                    {
                        for (int i = 0; i < arrayValue.Length - 1; i++)
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
                reportDocument.SetDataSource(dsDataSet.Tables[0]);                
                reportDocument.Refresh();   
                ReportViewer.ParameterFieldInfo = paramFields;
                ReportViewer.ReportSource = reportDocument; 
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        public void ExportReport(ReportDocument reportDocument,string strfile)
        {
            try
            {
                //string filename = System.Environment.CurrentDirectory;
                string filename = Application.StartupPath;
                filename = filename + "\\temp" + "\\" + strfile;
                DiskFileDestinationOptions diskOpts =
                ExportOptions.CreateDiskFileDestinationOptions();
                ExportOptions exportOpts = new ExportOptions();
                exportOpts.ExportFormatType = ExportFormatType.ExcelRecord;
                exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                diskOpts.DiskFileName = filename;
                exportOpts.ExportDestinationOptions = diskOpts;
                reportDocument.Export(exportOpts);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        public void gViewSubReport1(CrystalDecisions.Windows.Forms.CrystalReportViewer ReportViewer, ReportDocument reportDocument, DataSet dsDataSet, DataSet dsSubReport, string arrName, string arrValue)
        {            
            try
            {
                ParameterFields paramFields = new ParameterFields();
                ParameterFields SubparamFields = new ParameterFields();
                ParameterDiscreteValue param = new ParameterDiscreteValue();
                ParameterValues values = new ParameterValues();
                string[] arrayName;
                string[] arrayValue;
                char[] splitter = { '|' };

                if (arrName != "" && arrValue != "")
                {
                    //Tao mang tham so, mang gia tri 
                    arrayName = arrName.Split(splitter);
                    arrayValue = arrValue.Split(splitter);
                    if (arrayName.Length == arrayValue.Length)
                    {
                        for (int i = 0; i < arrayValue.Length - 1; i++)
                        {
                            ParameterField paramFieldSub = new ParameterField();
                            ParameterField paramField = new ParameterField();
                            paramField.ParameterFieldName = arrayName[i];
                            paramFieldSub.ParameterFieldName = arrayName[i];
                            ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                            ParameterDiscreteValue SubdiscreteVal = new ParameterDiscreteValue();
                            discreteVal.Value = arrayValue[i];
                            SubdiscreteVal.Value = arrayValue[i];
                            paramField.CurrentValues.Add(discreteVal);
                            paramFieldSub.CurrentValues.Add(SubdiscreteVal);
                            paramFields.Add(paramField);
                            SubparamFields.Add(paramFieldSub);
                            //reportDocument.Subreports[0].ParameterFields.Add(SubparamFields);
                        }

                    }
                }               
                reportDocument.SetDataSource(dsDataSet.Tables[0]);
                reportDocument.Subreports[0].SetDataSource(dsSubReport.Tables[0]);                   
                reportDocument.Refresh();
                ReportViewer.ReportSource = reportDocument;
                ReportViewer.ParameterFieldInfo = paramFields;
                ReportViewer.ParameterFieldInfo = SubparamFields;  
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmReport_KeyDown(object sender, KeyEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

        private void frmReport_MouseMove(object sender, MouseEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
        }
    }
}
