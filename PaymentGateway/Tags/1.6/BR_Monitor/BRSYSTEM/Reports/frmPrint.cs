using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Enterprise;     
namespace BR.BRSYSTEM
{   
    
    public partial class frmPrint : frmBasedata
    {
        #region khai bao cac bien
        private CrystalDecisions.CrystalReports.Engine.ReportDocument myReport;

        public string strMsgID;
        public string strQueryID;
        public int strQueryIDSwift;
        public int strQueryIDTTSP;
        public string PrintType;
        public int strQueryVCB;
        public int strSwiftInfo;
        public int strStatementID_reprint;
        public string  StatementDate_reprint;
        public int strQueryIQS;        
        public DataTable  HMdat;        
        public string dtfrom;
        public string dtto;
        public string pBranch;
        public string pBranch8;
        public string pUserid;        
        public int pFeeType;
        public int pFeeBranchType;
        public string pCCYCD;
        public string phanhe;
        public string nhnhan;
        public string sotien;
        public string loaidien;
        public string isn;
        public string chieudien;                
        public string nhgui;
        public string sogd;
        public string loaitien;
        public string osn;
        public string tcdien;
        public string ttdienden;
        public string ttdiendi;
        public string ttgw;
        public string kieuxuly;
        //end
        public string CurrentTad;
        public string CurrentValue;
        public string Direction;
        public string msg_source;
        //public string statement;
        public string direction_excell;
        public string rpttype;
        public string view;
        public string chanel;
        public string OnDate;
        public string teller;
        //phuc vu cho in bao cao doi chieu
        public string str_SFOTF;
        public string str_SFORMSIBS ;
        public string str_SFORMFILE ;
        public string str_SFOTR ;
        public string str_GWROTF ;
        public string str_GWRORMSIBS ;
        public string str_GWRORMFILE ;
        public string str_GWROTR ;
        public string str_GWSOTF ;
        public string str_GWSORM;
        public string str_GWSOTR ;
        public string str_SWRO ;
        public string str_SWSI ;
        public string str_GWRITF;
        public string str_GWRIRMSW;
        public string str_GWRIRMFILE;
        public string str_GWRITR;
        public string str_GWRIOther ;
        public string str_GWSITF ;
        public string str_GWSIRM ;
        public string str_GWSITR;
        public string str_SIBSITF ;
        public string str_SIBSIRM ;
        public string str_SIBSITR;
        #endregion

        public frmPrint()
        
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                string path = Application.StartupPath;               
                path = path + "\\rpt" + "\\" + PrintType + ".rpt";
                myReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();              
                if (PrintType == "BM_IBPS01")
                {
                    myReport.Load(path);                                             
                    myReport.SetDataSource(HMdat);
                }
                //DONGPV
                else if (PrintType == "MT103")
                {
                    myReport.Load(path);
                    myReport.SetDataSource(HMdat);                    
                }
                else if (PrintType == "IBPS_PRINT_1")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();

                    parameter1.ParameterFieldName = "pBranch";
                    parameter2.ParameterFieldName = "pMSG_ID";
                    parameter3.ParameterFieldName = "pUser";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();

                    paramvalue1.Value = pBranch;
                    paramvalue2.Value = strMsgID;
                    paramvalue3.Value = pUserid;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "IBPS_PRINT_2")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();

                    parameter1.ParameterFieldName = "PBRANCH";
                    parameter2.ParameterFieldName = "PMSG_ID";
                    parameter3.ParameterFieldName = "pUser";
                    
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();

                    paramvalue1.Value = pBranch;
                    paramvalue2.Value = strMsgID;
                    paramvalue3.Value = pUserid;     

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);                    

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;                    
                }
                else if (PrintType == "IBPS_PRINT_ALL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    parameter1.ParameterFieldName = "PDATEFROM";
                    parameter2.ParameterFieldName = "PDATETO";
                    parameter3.ParameterFieldName = "PDIRECTION";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();

                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = Direction;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters; 
                }
                else if (PrintType == "IBPS_PRINT_RECONCILE")
                {
                    myReport.Load(path);
                    myReport.SetDataSource(HMdat);
                    myReport.Refresh();
                }
                else if (PrintType == "TTSP_PRINT_RECONCILE")
                {
                    myReport.Load(path);
                    myReport.SetDataSource(HMdat);
                    myReport.Refresh();
                }
                else if (PrintType == "VCB_PRINT_RECONCILE")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();

                    parameter1.ParameterFieldName = "PDIRECTION";
                    parameter2.ParameterFieldName = "PONDATE";
                    parameter3.ParameterFieldName = "PVIEW";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();

                    paramvalue1.Value = Direction;
                    paramvalue2.Value = dtfrom;
                    paramvalue3.Value = view;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                    myReport.Refresh();
                }
                else if (PrintType == "SWIFT_PRINT_RECONCILE")
                {
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    ParameterField parameter6 = new ParameterField();
                    ParameterField parameter7 = new ParameterField();
                    ParameterField parameter8 = new ParameterField();
                    ParameterField parameter9 = new ParameterField();
                    ParameterField parameter10 = new ParameterField();
                    ParameterField parameter11 = new ParameterField();
                    ParameterField parameter12 = new ParameterField();
                    ParameterField parameter13 = new ParameterField();
                    ParameterField parameter14 = new ParameterField();
                    ParameterField parameter15 = new ParameterField();
                    ParameterField parameter16 = new ParameterField();
                    ParameterField parameter17 = new ParameterField();
                    ParameterField parameter18 = new ParameterField();
                    ParameterField parameter19 = new ParameterField();
                    ParameterField parameter20 = new ParameterField();
                    ParameterField parameter21 = new ParameterField();
                    ParameterField parameter22 = new ParameterField();
                    ParameterField parameter23 = new ParameterField();
                    ParameterField parameter24 = new ParameterField();
                    ParameterField parameter25 = new ParameterField();

                    parameter1.ParameterFieldName = "P_SFOTF";
                    parameter2.ParameterFieldName = "P_SFORMSIBS";
                    parameter3.ParameterFieldName = "P_SFORMFILE";
                    parameter4.ParameterFieldName = "P_SFOTR";
                    parameter5.ParameterFieldName = "P_GWROTF";
                    parameter6.ParameterFieldName = "P_GWRORMSIBS";
                    parameter7.ParameterFieldName = "P_GWRORMFILE";
                    parameter8.ParameterFieldName = "P_GWROTR";
                    parameter9.ParameterFieldName = "P_GWSOTF";
                    parameter10.ParameterFieldName = "P_GWSORM";
                    parameter11.ParameterFieldName = "P_GWSOTR";
                    parameter12.ParameterFieldName = "P_SWRO";
                    parameter13.ParameterFieldName = "P_SWSI";
                    parameter14.ParameterFieldName = "P_GWRITF";
                    parameter15.ParameterFieldName = "P_GWRIRMSW";
                    parameter16.ParameterFieldName = "P_GWRIRMFILE";
                    parameter17.ParameterFieldName = "P_GWRITR";
                    parameter18.ParameterFieldName = "P_GWRIOther";
                    parameter19.ParameterFieldName = "P_GWSITF";
                    parameter20.ParameterFieldName = "P_GWSIRM";
                    parameter21.ParameterFieldName = "P_GWSITR";
                    parameter22.ParameterFieldName = "P_SIBSITF";
                    parameter23.ParameterFieldName = "P_SIBSIRM";
                    parameter24.ParameterFieldName = "P_SIBSITR";
                    parameter25.ParameterFieldName = "PONDATE";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue6 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue7 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue8 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue9 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue10 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue11 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue12 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue13 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue14 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue15 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue16 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue17 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue18 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue19 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue20 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue21 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue22 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue23 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue24 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue25 = new ParameterDiscreteValue(); 

                    paramvalue1.Value = str_SFOTF;
                    paramvalue2.Value = str_SFORMSIBS;
                    paramvalue3.Value = str_SFORMFILE;
                    paramvalue4.Value = str_SFOTR;
                    paramvalue5.Value = str_GWROTF;
                    paramvalue6.Value = str_GWRORMSIBS;
                    paramvalue7.Value = str_GWRORMFILE;
                    paramvalue8.Value = str_GWROTR;
                    paramvalue9.Value = str_GWSOTF;
                    paramvalue10.Value = str_GWSORM;
                    paramvalue11.Value = str_GWSOTR;
                    paramvalue12.Value = str_SWRO;
                    paramvalue13.Value = str_SWSI;
                    paramvalue14.Value = str_GWRITF;
                    paramvalue15.Value = str_GWRIRMSW;
                    paramvalue16.Value = str_GWRIRMFILE;
                    paramvalue17.Value = str_GWRITR;
                    paramvalue18.Value = str_GWRIOther;
                    paramvalue19.Value = str_GWSITF;
                    paramvalue20.Value = str_GWSIRM;
                    paramvalue21.Value = str_GWSITR;
                    paramvalue22.Value = str_SIBSITF;
                    paramvalue23.Value = str_SIBSIRM;
                    paramvalue24.Value = str_SIBSITR;
                    paramvalue25.Value = OnDate;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);
                    parameter6.CurrentValues.Add(paramvalue6);
                    parameter7.CurrentValues.Add(paramvalue7);
                    parameter8.CurrentValues.Add(paramvalue8);
                    parameter9.CurrentValues.Add(paramvalue9);
                    parameter10.CurrentValues.Add(paramvalue10);
                    parameter11.CurrentValues.Add(paramvalue11);
                    parameter12.CurrentValues.Add(paramvalue12);
                    parameter13.CurrentValues.Add(paramvalue13);
                    parameter14.CurrentValues.Add(paramvalue14);
                    parameter15.CurrentValues.Add(paramvalue15);
                    parameter16.CurrentValues.Add(paramvalue16);
                    parameter17.CurrentValues.Add(paramvalue17);
                    parameter18.CurrentValues.Add(paramvalue18);
                    parameter19.CurrentValues.Add(paramvalue19);
                    parameter20.CurrentValues.Add(paramvalue20);
                    parameter21.CurrentValues.Add(paramvalue21);
                    parameter22.CurrentValues.Add(paramvalue22);
                    parameter23.CurrentValues.Add(paramvalue23);
                    parameter24.CurrentValues.Add(paramvalue24);
                    parameter25.CurrentValues.Add(paramvalue25);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);
                    parameters.Add(parameter6);
                    parameters.Add(parameter7);
                    parameters.Add(parameter8);
                    parameters.Add(parameter9);
                    parameters.Add(parameter10);
                    parameters.Add(parameter11);
                    parameters.Add(parameter12);
                    parameters.Add(parameter13);
                    parameters.Add(parameter14);
                    parameters.Add(parameter15);
                    parameters.Add(parameter16);
                    parameters.Add(parameter17);
                    parameters.Add(parameter18);
                    parameters.Add(parameter19);
                    parameters.Add(parameter20);
                    parameters.Add(parameter21);
                    parameters.Add(parameter22);
                    parameters.Add(parameter23);
                    parameters.Add(parameter24);
                    parameters.Add(parameter25);

                    myReport.Load(path);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                    myReport.Refresh();
                }
                else if (PrintType == "IBPS_BM02")
                {
                    myReport.Load(path);

                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    parameter1.ParameterFieldName = "PRPTTYPE";
                    parameter2.ParameterFieldName = "PONDATE";
                    parameter3.ParameterFieldName = "PTELLER";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    paramvalue1.Value = rpttype;
                    paramvalue2.Value = OnDate;
                    paramvalue3.Value = teller;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters; 
                }
            
                else if (PrintType == "SWIFT_03")
                {
                    myReport.Load(path);                

                    myReport.SetDataSource(HMdat);                    
                }
                else if (PrintType == "SWIFT_03_R")
                {
                    myReport.Load(path);                    
                    myReport.SetDataSource(HMdat); 
                }               
                else if (PrintType == "SWIFT_RM_R")
                {
                    myReport.Load(path);                   
                    myReport.SetDataSource(HMdat);

                }
                else if (PrintType == "SWIFT_PRINT")
                {                    
                    myReport.Load(path);

                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    parameter1.ParameterFieldName = "PMSG_ID";
                    parameter2.ParameterFieldName = "PMSG_TYPE";
                    parameter3.ParameterFieldName = "PMSGDIRECTION";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    paramvalue1.Value = strMsgID;
                    paramvalue2.Value = loaidien;
                    paramvalue3.Value = chieudien;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "SWIFT_PRINT_TF")
                {
                    myReport.Load(path);

                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    parameter1.ParameterFieldName = "PMSG_ID";
                    parameter2.ParameterFieldName = "PMSG_TYPE";
                    parameter3.ParameterFieldName = "PMSGDIRECTION";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    paramvalue1.Value = strMsgID;
                    paramvalue2.Value = loaidien;
                    paramvalue3.Value = chieudien;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                //DONGPV
                else if (PrintType == "TTSP_PRINT_2")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    parameter1.ParameterFieldName = "PMSG_ID";
                    parameter2.ParameterFieldName = "PMSG_TYPE";
                    parameter3.ParameterFieldName = "PMSGDIRECTION";
                    parameter4.ParameterFieldName = "PBRANCH";
                    parameter5.ParameterFieldName = "PUSER";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    paramvalue1.Value = strMsgID;
                    paramvalue2.Value = loaidien;
                    paramvalue3.Value = chieudien;
                    paramvalue4.Value = pBranch;
                    paramvalue5.Value = pUserid;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }              
                else if (PrintType == "TTSP_PRINT_ALL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();

                    parameter1.ParameterFieldName = "PDATEFROM";
                    parameter2.ParameterFieldName = "PDATETO";
                    parameter3.ParameterFieldName = "PDIRECTION";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();

                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = Direction;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters; 
                    //myReport.Refresh();                     
                    
                }
                else if (PrintType == "TAD_PRINT")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    parameter1.ParameterFieldName = "PTAD";
                    parameter2.ParameterFieldName = "PVALUE";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    paramvalue1.Value = CurrentTad ;
                    paramvalue2.Value = CurrentValue;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2); 
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;  

                }
                else if (PrintType == "VCB_PRINT_1")
                {
                    myReport.Load(path);

                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    parameter1.ParameterFieldName = "PMSG_ID";
                    parameter2.ParameterFieldName = "PMSG_TYPE";
                    parameter3.ParameterFieldName = "PMSGDIRECTION";
                    parameter4.ParameterFieldName = "PBRANCH";
                    parameter5.ParameterFieldName = "PUSER";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    paramvalue1.Value = strMsgID;
                    paramvalue2.Value = loaidien;
                    paramvalue3.Value = chieudien;
                    paramvalue4.Value = pBranch;
                    paramvalue5.Value = pUserid;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "VCB_PRINT_2")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    parameter1.ParameterFieldName = "PMSG_ID";
                    parameter2.ParameterFieldName = "PMSG_TYPE";
                    parameter3.ParameterFieldName = "PMSGDIRECTION";
                    parameter4.ParameterFieldName = "PBRANCH";
                    parameter5.ParameterFieldName = "PUSER";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    paramvalue1.Value = strMsgID;
                    paramvalue2.Value = loaidien;
                    paramvalue3.Value = chieudien;
                    paramvalue4.Value = pBranch;
                    paramvalue5.Value = pUserid;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);
                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "VCB_PRINT")
                {
                    myReport.Load(path);
                    myReport.SetDataSource(HMdat);
                }
                else if (PrintType == "VCB_PRINT_ALL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();

                    parameter1.ParameterFieldName = "PDATEFROM";
                    parameter2.ParameterFieldName = "PDATETO";
                    parameter3.ParameterFieldName = "PDIRECTION";
                    parameter4.ParameterFieldName = "PMSGSOURCE";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();

                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = Direction;
                    paramvalue4.Value = msg_source;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters; 
                    
                }        
                else if (PrintType == "SWIFT_08")
                {
                    myReport.Load(path);
                    //myReport.SetParameterValue("PID", strSwiftInfo);
                    myReport.SetDataSource(HMdat);
                }
                else if (PrintType == "SWIFT_09")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();
                    ParameterField parameter1 = new ParameterField();
                    parameter1.ParameterFieldName = "PDIRECTION";
                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    paramvalue1.Value = direction_excell;
                    parameter1.CurrentValues.Add(paramvalue1);
                    parameters.Add(parameter1);
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters; 
                }
                else if (PrintType == "SWIFT_06")
                {
                    myReport.Load(path);                                       
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1= new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    ParameterField parameter6 = new ParameterField();
                    ParameterField parameter7 = new ParameterField();
                    ParameterField parameter8 = new ParameterField();
                    ParameterField parameter9 = new ParameterField();
                    ParameterField parameter10 = new ParameterField();
                    ParameterField parameter11 = new ParameterField();
                    ParameterField parameter12 = new ParameterField();
                    ParameterField parameter13 = new ParameterField();
                    ParameterField parameter14 = new ParameterField();
                    ParameterField parameter15 = new ParameterField();
                    ParameterField parameter16 = new ParameterField();
                    ParameterField parameter17 = new ParameterField();

                    parameter1.ParameterFieldName = "PDATEFROM";
                    parameter2.ParameterFieldName = "PDATETO";
                    parameter3.ParameterFieldName = "PPHANHE";
                    parameter4.ParameterFieldName = "PNHNHAN";
                    parameter5.ParameterFieldName = "PAMOUNT";
                    parameter6.ParameterFieldName = "PCCYCD";
                    parameter7.ParameterFieldName = "PISN";
                    parameter8.ParameterFieldName = "PCHIEUDIEN";
                    parameter9.ParameterFieldName = "PNHGUI";
                    parameter10.ParameterFieldName = "PSOGD";
                    parameter11.ParameterFieldName = "PLOAIDIEN";
                    parameter12.ParameterFieldName = "POSN";
                    parameter13.ParameterFieldName = "PTCDIEN";
                    parameter14.ParameterFieldName = "PTTDIENDEN";
                    parameter15.ParameterFieldName = "PTTDIENDI";
                    parameter16.ParameterFieldName = "PTTDIENGW";
                    parameter17.ParameterFieldName = "PKIEUXULY";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue6 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue7 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue8 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue9 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue10 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue11 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue12 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue13 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue14 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue15 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue16 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue17 = new ParameterDiscreteValue();

                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = phanhe;
                    paramvalue4.Value = nhnhan;
                    paramvalue5.Value = sotien;
                    paramvalue6.Value = loaitien;
                    paramvalue7.Value = isn;              
                    paramvalue8.Value = chieudien;
                    paramvalue9.Value = nhgui;
                    paramvalue10.Value = sogd;
                    paramvalue11.Value = loaidien;
                    paramvalue12.Value = osn;
                    paramvalue13.Value = tcdien;
                    paramvalue14.Value = ttdienden;
                    paramvalue15.Value = ttdiendi;
                    paramvalue16.Value = ttgw;
                    paramvalue17.Value = kieuxuly;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);
                    parameter6.CurrentValues.Add(paramvalue6);
                    parameter7.CurrentValues.Add(paramvalue7);
                    parameter8.CurrentValues.Add(paramvalue8);
                    parameter9.CurrentValues.Add(paramvalue9);
                    parameter10.CurrentValues.Add(paramvalue10);
                    parameter11.CurrentValues.Add(paramvalue11);
                    parameter12.CurrentValues.Add(paramvalue12);
                    parameter13.CurrentValues.Add(paramvalue13);
                    parameter14.CurrentValues.Add(paramvalue14);
                    parameter15.CurrentValues.Add(paramvalue15);
                    parameter16.CurrentValues.Add(paramvalue16);
                    parameter17.CurrentValues.Add(paramvalue17);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);
                    parameters.Add(parameter6);
                    parameters.Add(parameter7);
                    parameters.Add(parameter8);
                    parameters.Add(parameter9);
                    parameters.Add(parameter10);
                    parameters.Add(parameter11);
                    parameters.Add(parameter12);
                    parameters.Add(parameter13);
                    parameters.Add(parameter14);
                    parameters.Add(parameter15);
                    parameters.Add(parameter16);
                    parameters.Add(parameter17);
               
                    myReport.SetDataSource(HMdat);                    
                    crystalReportViewer1.ParameterFieldInfo = parameters;                    
                }                
                else if (PrintType == "IQS_PRINT_ALL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();

                    parameter1.ParameterFieldName = "PDIRECTION";
                    parameter2.ParameterFieldName = "PONDATE";
                    parameter3.ParameterFieldName = "PCHANEL";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();

                    paramvalue1.Value = Direction;
                    paramvalue2.Value = OnDate;
                    paramvalue3.Value = chanel;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);                    

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    
                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;                    
                }     
                else if (PrintType == "SWIFT_08_R")
                {
                    myReport.Load(path);
                    myReport.SetDataSource(HMdat);
                }
                else if (PrintType == "BK02" || PrintType == "BK02_DETAIL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    ParameterField parameter6 = new ParameterField();
                    ParameterField parameter7 = new ParameterField();
                    ParameterField parameter8 = new ParameterField();

                    parameter1.ParameterFieldName = "PFROMDATE";
                    parameter2.ParameterFieldName = "PTODATE";
                    parameter3.ParameterFieldName = "pBranchType";
                    parameter4.ParameterFieldName = "pFeeType";
                    parameter5.ParameterFieldName = "pBranch";
                    parameter6.ParameterFieldName = "pBranch8";
                    parameter7.ParameterFieldName = "pCCYCD";
                    parameter8.ParameterFieldName = "pTeller";

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue6 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue7 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue8 = new ParameterDiscreteValue();

                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = pFeeBranchType;
                    paramvalue4.Value = pFeeType;
                    paramvalue5.Value = pBranch;
                    paramvalue6.Value = pBranch;
                    paramvalue7.Value = pCCYCD;
                    paramvalue8.Value = pUserid;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);
                    parameter6.CurrentValues.Add(paramvalue6);
                    parameter7.CurrentValues.Add(paramvalue7);
                    parameter8.CurrentValues.Add(paramvalue8);

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);
                    parameters.Add(parameter6);
                    parameters.Add(parameter7);
                    parameters.Add(parameter8);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "VCB_FEE_CAL" || 
                    PrintType == "VCB_FEE_CAL_DETAIL_EXCEL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();


                    parameter1.ParameterFieldName = "PFROMDATE";
                    parameter2.ParameterFieldName = "PTODATE";
                    parameter3.ParameterFieldName = "PBRANCH";
                    parameter4.ParameterFieldName = "PFEETYPE";
                    parameter5.ParameterFieldName = "PCCYCD";



                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();


                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = pBranch;
                    paramvalue3.Value = pFeeType;
                    paramvalue3.Value = pCCYCD;


                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);



                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);


                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "VCB_FEE_CAL_DETAIL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();


                    parameter1.ParameterFieldName = "PFROMDATE";
                    parameter2.ParameterFieldName = "PTODATE";
                    parameter3.ParameterFieldName = "PBRANCH";
                    parameter4.ParameterFieldName = "PFEETYPE";
                    parameter5.ParameterFieldName = "PCCYCD";



                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();


                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = pBranch;
                    paramvalue3.Value = pFeeType;
                    paramvalue3.Value = pCCYCD;


                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);



                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);


                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                else if (PrintType == "SWIFT_FEE_CAL")
                {
                    myReport.Load(path);
                    ParameterFields parameters = new ParameterFields();

                    ParameterField parameter1 = new ParameterField();
                    ParameterField parameter2 = new ParameterField();
                    ParameterField parameter3 = new ParameterField();
                    ParameterField parameter4 = new ParameterField();
                    ParameterField parameter5 = new ParameterField();
                    
                    parameter1.ParameterFieldName = "PFROMDATE";
                    parameter2.ParameterFieldName = "PTODATE";
                    parameter3.ParameterFieldName = "PBRANCH";
                    parameter4.ParameterFieldName = "PFEETYPE";
                    parameter5.ParameterFieldName = "PCCYCD";                   

                    ParameterDiscreteValue paramvalue1 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue2 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue3 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue4 = new ParameterDiscreteValue();
                    ParameterDiscreteValue paramvalue5 = new ParameterDiscreteValue();
                    
                    paramvalue1.Value = dtfrom;
                    paramvalue2.Value = dtto;
                    paramvalue3.Value = pBranch;
                    paramvalue4.Value = pFeeType;
                    paramvalue5.Value = pCCYCD;

                    parameter1.CurrentValues.Add(paramvalue1);
                    parameter2.CurrentValues.Add(paramvalue2);
                    parameter3.CurrentValues.Add(paramvalue3);
                    parameter4.CurrentValues.Add(paramvalue4);
                    parameter5.CurrentValues.Add(paramvalue5);                    

                    parameters.Add(parameter1);
                    parameters.Add(parameter2);
                    parameters.Add(parameter3);
                    parameters.Add(parameter4);
                    parameters.Add(parameter5);

                    myReport.SetDataSource(HMdat);
                    crystalReportViewer1.ParameterFieldInfo = parameters;
                }
                TableLogOnInfos myTableLogonInfos = new CrystalDecisions.Shared.TableLogOnInfos();
                TableLogOnInfo myTableLogonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
                ConnectionInfo myConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
                myConnectionInfo.AllowCustomConnection = true;
                //myConnectionInfo.IntegratedSecurity = false ;
                myConnectionInfo.DatabaseName = BR.BRBusinessObject.DATABASESInfo.strDatabase;
                myConnectionInfo.ServerName = BR.BRBusinessObject.DATABASESInfo.strOracleServer;
                //myConnectionInfo.ServerName = "//10.1.99.13:1521";
                myConnectionInfo.Password = BR.BRBusinessObject.DATABASESInfo.strPassword;
                myConnectionInfo.UserID = BR.BRBusinessObject.DATABASESInfo.strUserDB; 
                myTableLogonInfo.ConnectionInfo = myConnectionInfo;

                for (int ind = 0; ind < myReport.Database.Tables.Count; ind++)
                {              
                    myTableLogonInfos.Add(myTableLogonInfo);
                }          
                crystalReportViewer1.LogOnInfo = myTableLogonInfos;
                
                crystalReportViewer1.ReportSource = myReport;
                                
                  
                cmdAdd.Visible = false;
                cmdDelete.Visible = false;
                cmdEdit.Visible = false;
                cmdSave.Visible = false;
                cmdClose.Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); this.Close();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
        }

        private void frmPrint_KeyDown(object sender, KeyEventArgs e)
        {
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

        private void crystalReportViewer1_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1; 
        }

        private void frmPrint_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1; 
        }

        private void crystalReportViewer1_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }
      
    }
}
