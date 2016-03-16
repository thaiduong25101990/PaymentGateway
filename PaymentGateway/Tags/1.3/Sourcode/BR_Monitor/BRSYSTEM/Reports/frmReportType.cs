using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRLib;
using System.Collections;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace BR.BRSYSTEM
{
    public partial class frmReportType : BR.BRSYSTEM.frmBasedata  
    {
        private RPTRULEController objRule = new RPTRULEController();
        //private RPTMASTERInfo objrpt = new RPTMASTERInfo();
        private RPTMASTERController controlRPTMASTER = new RPTMASTERController();
        private RPTMASTERDP objrptmaster = new RPTMASTERDP();
        //public static bool isInsert = false;
        public string strRrtType;
        public string RptName;
        public string strValue;
        Button cboYes = new Button();
        Button cboCalcel = new Button();
        Form frm = new Form();
        DataSet dsParam = new DataSet();
        public frmReportType()        
        {
            InitializeComponent();            
        }

        private void frmReportType_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            cmdAdd.Visible = false;
            cmdDelete.Visible = false;
            cmdEdit.Visible = false;
            cmdSave.Visible = false;
            cmdRefresh.Enabled = false;
            cmdExport.Enabled = false;
            cmdPrint.Enabled = false;
            cmdPrint.Visible = false;
            cmdExport.Visible = false; 
            this.Text = Common.gGWTYPE + "  REPORT";
            LoadDataGrid();                  

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private void LoadDataGrid()
        {
            try
            {
                string userID = Common.Userid;
                userID = userID.Trim();
                strRrtType = Common.gGWTYPE;  
                DataSet datDs = new DataSet();
                datDs = controlRPTMASTER.GetReportType(strRrtType,userID); 
                dgvReportType.DataSource = datDs.Tables[0];       
                dgvReportType.Columns[0].HeaderText = "Report Name";
                dgvReportType.Columns[0].Width = 100;
                dgvReportType.Columns[1].HeaderText = "Description";
                dgvReportType.Columns[1].Width = dgvReportType.Width - 100;
                dgvReportType.Enabled = true ; 
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    cmdRefresh.Enabled = false;
                    dgvReportType.Enabled = false; 
                }
                ColumnsRead(dgvReportType);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message,2,MessageBoxButtons.OK);
            }
        }
       
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            frm = new Form();            
            frm.Text = "Input Parameter";
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.AutoSize = false;
            frm.MaximizeBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            frm.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            RptName = dgvReportType.CurrentRow.Cells[0].Value.ToString();
            RptName = RptName.Trim();
            string strType;
            int strlengh;
            string strCaption;
            string strName;
            int MaxLbl;//Do dai max cua Label
            int WidthControl=100;//Do rong cua Control
            int HeightControl = 25;//Chieu cao cua Control
            int DictinctControl = 30;//Khoang cach cao giua cac coltrol
            dsParam = controlRPTMASTER.GetParam(RptName);
            if (dsParam != null && dsParam.Tables[0].Rows.Count > 0)
            {
                //---------Dung doan nay de xac dinh do dai max cua Label
                MaxLbl = dsParam.Tables[0].Rows[0]["CAPTION"].ToString().Trim().Length;                 
                for (int i = 0; i <= dsParam.Tables[0].Rows.Count - 1; i++)
                {
                    if (MaxLbl < dsParam.Tables[0].Rows[i]["CAPTION"].ToString().Trim().Length)
                    {
                        MaxLbl = dsParam.Tables[0].Rows[i]["CAPTION"].ToString().Trim().Length;
                    }
                }

                //--------------------
                MaxLbl = MaxLbl * 8;
                for (int i = 0; i <= dsParam.Tables[0].Rows.Count - 1; i++)
                {
                    Label objlbl = new Label();
                    strType = dsParam.Tables[0].Rows[i]["CRLTYPE"].ToString();
                    strlengh = Convert.ToInt32(dsParam.Tables[0].Rows[i]["CRLLENGH"].ToString());
                    strCaption = dsParam.Tables[0].Rows[i]["CAPTION"].ToString();
                    strName = dsParam.Tables[0].Rows[i]["CRLNAME"].ToString();
                    string strCombo = dsParam.Tables[0].Rows[i]["SQL"].ToString().Trim();//add them muc dich load Combo
                    objlbl.Name = Convert.ToString("Label" + i);
                    objlbl.Text = strCaption.Trim();
                    objlbl.Width = MaxLbl;
                    objlbl.Height = HeightControl;
                    //objlbl.Width = strCaption.Trim().Length;     
                    objlbl.Location = new Point(frm.Location.X + 20, frm.Location.Y + 20 + i * DictinctControl);
                    frm.Controls.Add(objlbl);

                    if ((strName.Trim()).ToLower() == "textbox")
                    {
                        TextBox objtxt = new TextBox();
                        objtxt.Name = Convert.ToString("textbox" + i);
                        objtxt.MaxLength = strlengh;
                        //objtxt.Width = 100;
                        objtxt.Width = WidthControl;
                        objlbl.Height = HeightControl;
                        objtxt.Location = new Point(frm.Location.X + 20 + MaxLbl + 20, frm.Location.Y + 15 + i * DictinctControl);                        
                        //------------------  
                        if (RptName == "SWIFT_RM")
                            {
                                objtxt.Text = Common.Userid.ToString().Trim();
                                objtxt.Enabled = false;
                            }
                        //---------------
                        frm.Controls.Add(objtxt); 
                    }
                    else if ((strName.Trim()).ToLower() == "combobox")
                    {
                        ComboBox objcombo = new ComboBox();
                        objcombo.Name = Convert.ToString("combobox" + i);
                        DataTable dt = new DataTable();
                        //-----------------
                        if (strCombo!="")
                        {    
                            int k=0;
                            dt = controlRPTMASTER.GetDataCombo(strCombo);
                            if (dsParam.Tables[0].Rows[i]["OPTALL"].ToString() == "1")                            
                                objcombo.Items.Add("ALL");
                            while (k < dt.Rows.Count)
                            {
                                string strContent1 = dt.Rows[k]["COLUMNNAME"].ToString();
                                objcombo.Items.Add(strContent1);                                
                                k = k + 1;
                            }
                            objcombo.SelectedIndex = 0;
                        }
                        
                        //------------------
                        objcombo.Width = WidthControl;
                        objlbl.Height = HeightControl;
                        objcombo.Location = new Point(frm.Location.X + 20 + MaxLbl + 20, frm.Location.Y + 15 + i * DictinctControl);
                        frm.Controls.Add(objcombo);
                    }
                    else if ((strName.Trim()).ToLower() == "picker")
                    {
                        DateTimePicker objdatetime = new DateTimePicker();
                        objdatetime.Name = Convert.ToString("picker" + i);
                        objdatetime.Format = DateTimePickerFormat.Custom;
                        objdatetime.CustomFormat = "dd/MM/yyyy";
                        objdatetime.Width = WidthControl;
                        objlbl.Height = HeightControl;
                        objdatetime.Location = new Point(frm.Location.X + 20 + MaxLbl + 20, frm.Location.Y + 15 + i * DictinctControl);
                        frm.Controls.Add(objdatetime);
                    }
                }
                      
                cboYes.Name = "cboOK";
                cboYes.Height = 30;
                cboYes.Width = 80;
                cboYes.Height = 30;
                cboYes.Text = "OK";
                cboCalcel.Name = "cboCalcel";
                cboCalcel.Text = "Cancel";
                cboCalcel.Width = 80;
                cboCalcel.Height = 30;
                frm.Controls.Add(cboYes);
                frm.Controls.Add(cboCalcel);
                frm.Controls["cboOK"].Location = new Point(frm.Location.X + 20, frm.Location.Y + 20 + (dsParam.Tables[0].Rows.Count) * DictinctControl);
                frm.Controls["cboCalcel"].Location = new Point(frm.Location.X + 110, frm.Location.Y + 20 + (dsParam.Tables[0].Rows.Count) * DictinctControl);
                //frm.Controls["cboCalcel"].Location = new Point(frm.Location.X + 40 + MaxLbl , frm.Location.Y + 20 + (dsParam.Tables[0].Rows.Count) * DictinctControl);
                frm.Width = MaxLbl + 60 + WidthControl ;
                //frm.Controls["cboCalcel"].Location = new Point(frm.Location.X + 110, 50);
                frm.Height = frm.Controls["cboOK"].Height + (dsParam.Tables[0].Rows.Count) * HeightControl +(dsParam.Tables[0].Rows.Count - 1) * 15 + 60;                
                frm.ShowDialog();
            }
            else
            {
                frmReport frmReport = new frmReport();
                frmReport.NameRPT = RptName;
                frmReport.ShowDialog(); 
            }

        }

        
        public void CheckRule()
        {
            int numrefresh = 0;
            int numexport = 0;
            int numview = 0;
            int numprint = 0;
            string strContent = dgvReportType.CurrentRow.Cells[0].Value.ToString();
            strContent = strContent.Trim();
            string userID = Common.Userid.ToString();
            DataSet dtRule = new DataSet();
            dtRule = objRule.GetRuleReport(userID, strContent);
            if (dtRule.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dtRule.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtRule.Tables[0].Rows[i]["ISVIEW"].ToString()) == 1)
                    {
                        numview = numview + 1;
                    }
                    if (Convert.ToInt32(dtRule.Tables[0].Rows[i]["ISREFRESH"].ToString()) == 1)
                    {
                        numrefresh = numrefresh + 1;
                    }
                    if (Convert.ToInt32(dtRule.Tables[0].Rows[i]["ISEXPORT"].ToString()) == 1)
                    {
                        numexport = numexport + 1;
                    }
                    if (Convert.ToInt32(dtRule.Tables[0].Rows[i]["ISPRINT"].ToString()) == 1)
                    {
                        numprint = numprint + 1;
                    }

                }
            }
          
            if (numrefresh > 0)
            {
               
                cmdRefresh.Enabled = true;
            }
            else 
            {
                cmdRefresh.Enabled = false;
            }
            if (numexport > 0)
            {
               
                cmdExport.Enabled = true;
            }
            else
            {
                cmdExport.Enabled = false;
            }
            if (numprint > 0)
            {
               
                cmdPrint.Enabled = true;
            }
            else
            {
                cmdPrint.Enabled = false;
            }

        }

        private void dgvReportType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CheckRule();
        }
        private void cboCalcel_Click(object sender, EventArgs e)
        {
            frm.Close(); 
        }
        private void cboYes_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            strValue = "";
            for (int i = 0; i <= dsParam.Tables[0].Rows.Count - 1; i++)
            {
               string strNameControl = dsParam.Tables[0].Rows[i]["CRLNAME"].ToString();
               if ((strNameControl.Trim()).ToLower() == "textbox")
               {
                   //Kiem tra kieu du lieu nhap vao ko la so
                   if (dsParam.Tables[0].Rows[i]["CRLTYPE"].ToString().ToUpper() == "CURRENCY" ||
                       dsParam.Tables[0].Rows[i]["CRLTYPE"].ToString().ToUpper() == "NUMBER")
                   {
                       if (!string.IsNullOrEmpty(frm.Controls["textbox" + i].Text) &&
                           !Common.IsNumeric(frm.Controls["textbox" + i].Text.Trim(), false))
                       {
                           Common.ShowError("Data type is incorrect",1,MessageBoxButtons.OK);
                           return;
                       }

                   }
                   strValue = strValue + frm.Controls["textbox" + i].Text.Trim().ToUpper() + "|";  
               }
               else if ((strNameControl.Trim()).ToLower() == "combobox")
               {
                   strValue = strValue + frm.Controls["combobox" + i].Text.Trim().ToUpper() + "|";                    
               }
               else if ((strNameControl.Trim()).ToLower() == "picker")
               {
                   strValue = strValue + frm.Controls["picker" + i].Text.ToString()  + "|";  
               }
            }
            strValue = strValue.Substring(0, strValue.Length - 1);
            frmReport.sArrValue = strValue;
            frmReport.NameRPT = RptName;
            frm.Close(); 
            frmReport.ShowDialog(); 
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            //RptName = dgvReportType.CurrentRow.Cells[0].Value.ToString().Trim();
            //export(RptName);
        }
        public void export(string filename)
        {
            filename = filename +".rpt";
            
            string path = Application.StartupPath;
            path = path + "\\temp" + "\\" + filename ;
            
           
            Process.Start(path);
           
              
        }
        public void ColumnsRead(DataGridView Datagrid)
        {
            int b = 0;
            while (b < Datagrid.Columns.Count)
            {
                Datagrid.Columns[b].ReadOnly = true;
                b = b + 1;
            }
        }

        private void frmReportType_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
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

        private void frmReportType_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
    
}
