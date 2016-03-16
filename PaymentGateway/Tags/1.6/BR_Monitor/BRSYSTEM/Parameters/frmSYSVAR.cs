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
	public partial class frmSysvar: frmBasedata
    {
             
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private DataSet datDs = new DataSet();
        private SYSVARInfo objInfo = new SYSVARInfo();
        private SYSVARController objControl = new SYSVARController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private clsCheckInput clsCheck = new clsCheckInput();

        private int iID = 0;
        //private bool isInsert = false;
        private string strValue = "";
        private string strDescription = "";    
        private static int iRows;
        private int iLoad = 0;

        public frmSysvar()
		{
			InitializeComponent();
		}

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (Common.iSconfirm == 1)
            {
                if (String.IsNullOrEmpty(txtValue.Text.Trim()))
                {
                    Common.ShowError("You must input value!", 3, MessageBoxButtons.OK);
                    txtValue.Focus();
                    cmdEdit.Enabled = false;
                    cmdSave.Enabled = true;
                    return;
                }
                objInfo.ID = iID;
                objInfo.VALUE = clsCheck.ConvertVietnamese(txtValue.Text.Trim());
                objInfo.DESCRIPTION = clsCheck.ConvertVietnamese(txtDescription.Text.Trim());
                try
                {
                    if (txtName.Text.Trim().ToUpper() == "ROUTER_TAD")
                    {
                        if (txtValue.Text.Trim() != "0")
                        {
                            if (!CheckIBPS_ROUTER_TAD())
                            {
                                txtValue.Focus();
                                cmdEdit.Enabled = false;
                                cmdSave.Enabled = true;
                                return;
                            }
                            if (txtValue.Text.Trim().Length == 5)
                            {
                                objInfo.VALUE = txtValue.Text.Trim();
                            }
                            else
                            {
                                objInfo.VALUE = "00" + txtValue.Text.Trim();
                            }
                        }
                    }
                    if (objControl.UpdateSYSVAR(objInfo) == 1)
                    {
                        Common.ShowError("Update successfully!", 1, MessageBoxButtons.OK);
                        #region lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "System parameter";
                        int iLoglevel = 1;
                        string strWorked = "Update";
                        string strTable = "SYSVAR";
                        string strOld_value = strValue + "/" + strDescription;
                        string strNew_value = txtValue.Text + "/" + txtDescription.Text;
                        #endregion
                        objLog.GhiLogUser(dtDateLogin, BR.BRLib.Common.Userid, strContent,
                            iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                        LoadData();
                    }
                    else
                    {
                        Common.ShowError("Update data error!", 2, MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
                CommandStatus(true);
                txtValue.ReadOnly = true;
                txtDescription.ReadOnly = true;
            }
            else
            {
                txtValue.ReadOnly = true;
                txtDescription.ReadOnly = true;
                dgView.Select();
                dgView_CellContentClick(null, null);
                CommandStatus(true);
            }
        }


        private bool CheckIBPS_ROUTER_TAD()
        {
            bool result = true;
            string strSIBSBankCode;// = txtValue.Text.Trim();
            if (txtValue.Text.Trim().Length == 5)
            {
                strSIBSBankCode = txtValue.Text.Trim();
            }
            else
            {
                strSIBSBankCode = "00" + txtValue.Text.Trim();
            }
            DataSet ds = new DataSet();
            ds = objControl.GetIBPS_ROUTER_TAD(strSIBSBankCode, "1");
            if (ds.Tables[0].Rows.Count == 0)
            {
                Common.ShowError("Invalid value of ROUTER_TAD!", 3, MessageBoxButtons.OK);                
                //CommandStatus(false);
                result= false;
            }
            //else
            //{             
            //    return true;
            //}
            CommandStatus(false);            
            return result;
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                txtValue.ReadOnly = false;
                txtDescription.ReadOnly = false;
                txtValue.Focus();
                CommandStatus(false);
                strValue = txtValue.Text;
                strDescription = txtDescription.Text;               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadData()
        {
            try
            {
                iLoad = 1;
                DataSet datDs = new DataSet();
                datDs = objControl.GetSYSVAR();
                dgView.DataSource = datDs.Tables[0];
                if (datDs.Tables[0].Rows.Count != 0)
                {
                    dgView.Columns["ID"].Visible = false;
                    dgView.Columns["GWTYPE"].HeaderText = " Channel";
                    dgView.Columns["GWTYPE"].Width = 80;
                    dgView.Columns["VARNAME"].HeaderText = "Var name";
                    dgView.Columns["VARNAME"].Width = 200;
                    dgView.Columns["VALUE"].HeaderText = "Value";
                    dgView.Columns["VALUE"].Width = 100;
                    dgView.Columns["TYPE"].HeaderText = "Type";
                    dgView.Columns["TYPE"].Width = 70;
                    dgView.Columns["NOTE"].HeaderText = "Parameter";
                    dgView.Columns["NOTE"].Width = 100;
                    dgView.Columns["DESCRIPTION"].HeaderText = "Description";
                    dgView.Columns["DESCRIPTION"].Width = 300;
                }
                dgView.Rows[iRows].Selected = true;
                dgView.Rows[0].Selected = false;               
                iLoad = 0;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSysvar_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            LoadData();
            //Load data cboAppType
            if (!objGetData.FillDataComboBox(cboAppType, "Content", "cdval", "allcode",
                "CDName='APPTYPE'", "Content", true, false, ""))
                return;
            //Load data cboAppType
            if (!objGetData.FillDataComboBox(cboDataType, "Content", "cdval", "allcode",
                "CDName='DATATYPE'", "Content", true, false, ""))
                return;
            cboDataType.Enabled = false;
            cboAppType.Enabled = false;
            txtName.ReadOnly = true;
            txtNote.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtValue.ReadOnly = true;
            CommandStatus(true);
            dgView.Focus();
        }

        private void CommandStatus(bool a)
        {
            //cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSave.Enabled = !a;
            cmdCancel.Enabled = !a;
            dgView.Enabled = a;
        }

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { iRows = 0; }
            else
            { if (iLoad == 0) { iRows = e.RowIndex; } }

            iID = Convert.ToInt32(dgView.CurrentRow.Cells["ID"].Value.ToString());
            cboAppType.Text = dgView.CurrentRow.Cells["GWTYPE"].Value.ToString();
            txtName.Text = dgView.CurrentRow.Cells["VARNAME"].Value.ToString();
            txtValue.Text = dgView.CurrentRow.Cells["VALUE"].Value.ToString();
            cboDataType.Text = dgView.CurrentRow.Cells["TYPE"].Value.ToString();
            txtNote.Text = dgView.CurrentRow.Cells["NOTE"].Value.ToString();
            txtDescription.Text = dgView.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
        }

        private void frmSysvar_KeyDown(object sender, KeyEventArgs e)
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
                    if ((this.ActiveControl as Button).Name == "cmdSave")
                    {
                        cmdSave.Focus();                       
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            txtValue.ReadOnly = true;
            txtDescription.ReadOnly = true;            
            dgView.Select();
            //dgView_CellContentClick(null, null);
            CommandStatus(true);
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            txtValue.Text = clsCheck.ConvertVietnamese(txtValue.Text.Trim());
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            txtDescription.Text = clsCheck.ConvertVietnamese(txtDescription.Text.Trim());
        }

        private void frmSysvar_MouseMove(object sender, MouseEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }

        private void dgView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
	}
}
