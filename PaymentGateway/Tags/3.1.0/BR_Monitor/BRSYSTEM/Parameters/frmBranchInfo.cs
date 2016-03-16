using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Diagnostics;
using System.Threading;
using System.IO;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class frmBranchInfo : frmBasedata  
    {
        public string strSIBS_BANK_CODE;
        public string strMode;

        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private CURRENCYController objControlCURRENCY = new CURRENCYController();
        private BRANCHController objBranchController = new BRANCHController();
        private BRANCHInfo objBranch = new BRANCHInfo();
        private clsCheckInput objCheckInput = new clsCheckInput();

        private string BranchCode = "";
        private string BranchName = "";
        private string Address = "";
        private string Phone = "";
        private string Fax = "";
        private string Type = "";

        public frmBranchInfo()
        {
            InitializeComponent();
        }
 
        #region //Functions
        private bool Verify()
        {
            txtBranchCode.Text= txtBranchCode.Text.Trim();
            if (txtBranchCode.Text == "")
            {                
                Common.ShowError("Branch code is empty!", 4, MessageBoxButtons.OK);
                txtBranchCode.Focus();
                return false;
            }
            txtBranchName.Text= txtBranchName.Text.Trim();
            if (txtBranchName.Text == "")
            {
                Common.ShowError("Branch name is empty!", 4, MessageBoxButtons.OK);                
                txtBranchName.Focus();
                return false;
            }
            if (cboType.Text == "")
            {                
                Common.ShowError("Branch type is empty!", 4, MessageBoxButtons.OK);
                cboType.Focus();
                return false;
            }
            return true ;
        }

        private bool Validate_one()
        {

            //Kiem tra ma Branch co chua           
            try
            {
                DataSet dsData = new DataSet();

                string strCondition = " where SIBS_BANK_CODE = '" + txtBranchCode.Text + "'";
                dsData = objBranchController.Select(strCondition);

                if (dsData == null)
                    return false;
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    Common.ShowError("Branch code has existed!", 4, MessageBoxButtons.OK);
                    txtBranchCode.SelectAll();
                    txtBranchCode.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }

        private bool Insert()
        {
            try 
            {
                objBranch.SIBS_BANK_CODE = txtBranchCode.Text;
                objBranch.BRAN_NAME = txtBranchName.Text;
                objBranch.ADDRESS = txtAddress.Text;
                objBranch.TEL = txtPhone.Text;
                objBranch.FAX = txtFax.Text;
                objBranch.BRAN_TYPE = cboType.SelectedValue.ToString();
                
                int iResult=objBranchController.Insert(objBranch );
                if (iResult == -1)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
                return false;
            }            
        }

        private bool Update_branch()
        {
            try
            {
                objBranch.SIBS_BANK_CODE = txtBranchCode.Text;
                objBranch.BRAN_NAME = txtBranchName.Text;
                objBranch.ADDRESS = txtAddress.Text;
                objBranch.TEL = txtPhone.Text;
                objBranch.FAX = txtFax.Text;
                objBranch.BRAN_TYPE = cboType.SelectedValue.ToString();

                int iResult = objBranchController.Update(objBranch);
                if (iResult == -1)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
                return false;
            }            
        }     

        private void LoadData()
        {
            string strCondition = " where SIBS_BANK_CODE='" + strSIBS_BANK_CODE + "'";
            DataSet dsData = new DataSet();
            dsData = objBranchController.Select(strCondition);

            if (dsData == null)
                return;

            if (dsData.Tables[0].Rows.Count == 0)
                return;

            txtBranchCode.Text=dsData.Tables[0].Rows[0]["SIBS_BANK_CODE"].ToString();
            txtBranchName.Text=dsData.Tables[0].Rows[0]["BRAN_NAME"].ToString();
            txtAddress.Text=dsData.Tables[0].Rows[0]["ADDRESS"].ToString();
            txtPhone.Text=dsData.Tables[0].Rows[0]["TEL"].ToString();
            txtFax.Text=dsData.Tables[0].Rows[0]["FAX"].ToString();            
            cboType.Text = dsData.Tables[0].Rows[0]["BRAN_TYPE"].ToString();
            //-----------------------------------------------------------------------
            BranchCode = dsData.Tables[0].Rows[0]["SIBS_BANK_CODE"].ToString();
            BranchName = dsData.Tables[0].Rows[0]["BRAN_NAME"].ToString();
            Address = dsData.Tables[0].Rows[0]["ADDRESS"].ToString();
            Phone = dsData.Tables[0].Rows[0]["TEL"].ToString();
            Fax = dsData.Tables[0].Rows[0]["FAX"].ToString();
            Type = dsData.Tables[0].Rows[0]["BRAN_TYPE"].ToString();            
        }

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)            
                this.Close();            
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)                
                    return;                
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                    return;
                }
                if ((this.ActiveControl) is MaskedTextBox)
                {
                    (this.ActiveControl as MaskedTextBox).SelectAll();
                    return;
                }
            }
        }
        #endregion


        #region //Control Events
        private void frmBranchInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                //Load data combobox;
                if (!objGetData.FillDataComboBox(cboType, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME='BRAN_TYPE' AND GWTYPE='SYSTEM'", "CONTENT", true, false, ""))
                    return;
                
                if (strMode != "ADD")
                {
                    LoadData();
                }
                if (strMode == "VIEW")
                {                    
                    txtBranchCode.ReadOnly = true;
                    txtBranchName.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtFax.ReadOnly = true;
                    cboType.Enabled = false;
                    cmdSave.Enabled = false;
                }
                else if (strMode == "ADD")
                {
                    txtBranchCode.Enabled = true;
                    txtBranchName.Enabled = true;
                    txtAddress.Enabled = true;
                    txtPhone.Enabled = true;
                    txtFax.Enabled = true;
                    cboType.Enabled = true;
                    cmdSave.Enabled = true;
                    txtBranchCode.SelectAll();
                }
                else //if (strMode == "EDIT")
                {
                    //txtBranchCode.Enabled = false;
                    txtBranchCode.ReadOnly = true;
                    txtBranchName.Enabled = true;
                    txtAddress.Enabled = true;
                    txtPhone.Enabled = true;
                    txtFax.Enabled = true;
                    cboType.Enabled = true;
                    cmdSave.Enabled = true;
                    txtBranchName.SelectAll();

                    DataSet dsStatusTTSP = new DataSet();
                    dsStatusTTSP = objControlCURRENCY.GetCurrencyStatusTTSP(txtBranchCode.Text.Trim());
                    if (dsStatusTTSP.Tables[0].Rows.Count > 0)
                    {
                        cboType.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            cmdSave.Enabled = true;

            if (Common.iSconfirm == 0)
                return;
            if(!Verify())
                return ;
            if (strMode == "ADD")
            {
                if (!Validate_one())
                    return;

                if (!Insert())
                {                    
                    Common.ShowError("Error occured when insert data!", 1, MessageBoxButtons.OK);
                    return;
                }

                Common.ShowError("Insert data successfully!", 1, MessageBoxButtons.OK);
                //lay du lieu de ghi log
                DateTime dtDateLogin = DateTime.Now;
                string strContent = "Branch Management";
                int iLoglevel = 1;
                string strWorked = "Insert";
                string strTable = "BRANCH";
                string strOld_value = "";
                string strNew_value = txtBranchCode.Text + "/" + txtBranchName.Text + "/" + txtAddress.Text + "/" + txtPhone.Text + "/" + txtFax.Text + "/" + cboType.Text;
                //goi ham ghilog
                objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel,
                    strWorked, strTable, strOld_value, strNew_value); 
            }
            else if (strMode == "EDIT")
            {
                if (!Update_branch())
                {
                    Common.ShowError("Error occured when update data!", 2, MessageBoxButtons.OK);                    
                    return;
                }                
                Common.ShowError("Update data successfully!", 1, MessageBoxButtons.OK);
                //lay du lieu de ghi log
                DateTime dtDateLogin = DateTime.Now;
                string strContent = "Branch Management";
                int iLoglevel = 1;
                string strWorked = "Update";
                string strTable = "BRANCH";
                string strOld_value = BranchCode + "/" + BranchName + "/" + Address + "/" + Phone + "/" + Fax + "/" + Type;
                string strNew_value = txtBranchCode.Text + "/" + txtBranchName.Text + "/" + txtAddress.Text + "/" + txtPhone.Text + "/" + txtFax.Text + "/" + cboType.Text;
                //goi ham ghilog
                objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel, 
                    strWorked, strTable, strOld_value, strNew_value); 
            }
            cmdSave.Enabled = false ;
            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBranchInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cmdSave.Enabled)
                return;
            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);           
        }

        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;                
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);
            }
        }
     
        private void txtBranchName_Leave(object sender, EventArgs e)
        {
            txtBranchName.Text = objCheckInput.ConvertVietnamese(txtBranchName.Text.Trim());
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.Text = objCheckInput.ConvertVietnamese(txtAddress.Text.Trim());
        }

        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
        {           
        }
        #endregion

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            txtPhone.Text = objCheckInput.ConvertVietnamese(txtPhone.Text.Trim());
        }

        private void txtFax_Leave(object sender, EventArgs e)
        {
            txtFax.Text = objCheckInput.ConvertVietnamese(txtFax.Text.Trim());
        }
    }
}
