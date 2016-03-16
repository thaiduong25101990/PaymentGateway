using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib; 
using BR.BRBusinessObject;
namespace BR.BRSYSTEM
{
    
    public partial class frmMenegerReport : BR.BRSYSTEM.frmBasedata  
    {
        public string strContent;
        //private bool isInsert = false;
        public RPTMASTERInfo objrpt = new RPTMASTERInfo();
        public RPTMASTERController rptcontrol = new RPTMASTERController();
        public GWTYPEController gwtype = new GWTYPEController();
        clsCheckInput clsCheck = new clsCheckInput();
        public frmMenegerReport()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmMenegerReport_Load(object sender, EventArgs e)
        {
            this.Icon= new Icon(Application.StartupPath + @"\BRIDGE.ico");
            GetGatewayType();
            cmdDelete.Visible = false;
            cmdEdit.Visible = false;
            cmdAdd.Visible = false;
            cmdSave.Enabled = true;
            txtRptName.Text = objrpt.RPTNAME;            
            cboTypeGW.Text = objrpt.GWTYPE;           
            txtDetail.Text = objrpt.DESCRIPTION; 
            
            txtRptName.Focus();
            txtRptName.Select();
        }
        //dathm add 03/07/2008        
        private void GetGatewayType()
        {
            try
            {
                DataSet daGWtype = new DataSet();
                daGWtype = gwtype.GetGwtype();                
                
                int i = 0;
                if (frmReportList.isInsert == false)
                {
                    while (i < daGWtype.Tables[0].Rows.Count)
                    {
                        string strContent1 = daGWtype.Tables[0].Rows[i]["GWTYPE"].ToString();
                        cboTypeGW.Items.Add(strContent1);
                        cboTypeGW.SelectedValue = objrpt.GWTYPE;
                        i = i + 1;
                    }
                }
                else
                {
                    while (i < daGWtype.Tables[0].Rows.Count)
                    {
                        string strContent1 = daGWtype.Tables[0].Rows[i]["GWTYPE"].ToString();
                        cboTypeGW.Items.Add(strContent1);
                        cboTypeGW.SelectedIndex = 0;
                        i = i + 1;
                    }  
                }
            }
            catch
            { 
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            
            objrpt.RPTNAME = clsCheck.ConvertVietnamese(txtRptName.Text.Trim().ToUpper())  ;
            objrpt.GWTYPE = clsCheck.ConvertVietnamese(cboTypeGW.Text.Trim());
            objrpt.DESCRIPTION = clsCheck.ConvertVietnamese(txtDetail.Text.Trim().ToUpper()) ;
            string strContent = frmReportList.strContent;
            try
            {
                if (frmReportList.isInsert == true)
                {
                    if (Common.iSconfirm == 1)
                    {
                        if (String.IsNullOrEmpty(txtRptName.Text.Trim()))
                        {
                            MessageBox.Show("You must input Report Name", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmdSave.Enabled = true;
                        }
                        else
                        {
                            if (GetData.IDIsExisting(false, "RPTMASTER", "RPTNAME", txtRptName.Text.Trim(), ""))
                            {
                                MessageBox.Show("Report Name has already exist.", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtRptName.Text = "";
                                txtRptName.Focus();
                                cmdSave.Enabled = true;
                            }
                            else
                            {
                                rptcontrol.AddRPTMASTER(objrpt);
                                MessageBox.Show("New report has been add succesfull", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        cmdSave.Enabled = true; 
                    }
                }
                else if (frmReportList.isInsert == false)
                {
                    if (String.IsNullOrEmpty(txtRptName.Text.Trim()))
                    {
                        MessageBox.Show("You must input Report Name", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmdSave.Enabled = true;
                    }
                    else
                    {
                        rptcontrol.UpdateRPTMASTER(strContent, objrpt);
                        MessageBox.Show("Report has been update", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }                
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmMenegerReport_KeyDown(object sender, KeyEventArgs e)
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
                    cmdSave.Focus();
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmMenegerReport_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void txtRptName_Leave(object sender, EventArgs e)
        {
            txtRptName.Text = clsCheck.ConvertVietnamese(txtRptName.Text.Trim().ToUpper());
        }

        private void txtDetail_Leave(object sender, EventArgs e)
        {
            txtDetail.Text = clsCheck.ConvertVietnamese(txtDetail.Text.Trim().ToUpper());
        }
      
    }
}
