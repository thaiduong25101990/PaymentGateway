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
    public partial class frmGWBANK_MAP : frmBasedata 
    {
        private int iID = 0;
        private bool isInsert = false;
        private clsLog objLog = new clsLog();
        private DataSet datDs = new DataSet();
        private GWBANK_MAPInfo objInfo = new GWBANK_MAPInfo();
        private GWBANK_MAPController objControl = new GWBANK_MAPController();
        private clsCheckInput clsCheck = new clsCheckInput();

        public frmGWBANK_MAP()
        {
            InitializeComponent();
        }

        private void frmGWBANK_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            LoadData();
            LockTextBox(true);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            isInsert = false;
            LockTextBox(false);
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            iID = Convert.ToInt32(grdList.CurrentRow.Cells[0].Value.ToString());
            objControl.DeleteGWBANK_MAP(iID);
            LoadData();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            objInfo = new GWBANK_MAPInfo();
            objInfo.GWBANK_MAP_ID = iID;
            objInfo.SIBS_BANK_CODE = clsCheck.ConvertVietnamese(txtSIBSBankCode.Text.Trim());
            objInfo.BANK_NAME = clsCheck.ConvertVietnamese(txtBankName.Text.Trim());
            objInfo.GW_BANK_CODE = clsCheck.ConvertVietnamese(txtGWBankCode.Text.Trim());
            objInfo.BRANCH =Convert.ToInt32(txtBranch.Text.Trim());
            objInfo.DESCRIPTION = clsCheck.ConvertVietnamese(txtDescription.Text.Trim());

            if (isInsert == true)
            {
                if (GetData.IDIsExisting(false, "CURRENCY", "SHORTCD", txtSIBSBankCode.Text.Trim(), ""))
                {                    
                    Common.ShowError("SIBS Bank Code has already existed!", 3, MessageBoxButtons.OK);
                    txtSIBSBankCode.Text = "";
                    txtSIBSBankCode.Focus();
                    cmdAdd.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdSave.Enabled = true;
                    cmdDelete.Enabled = false;
                    return;
                }
                else if (!CheckID())
                {
                    return;
                }
                objControl.AddGWBANK_MAP(objInfo);
            }

            if (isInsert == false)
            {
                if (!CheckID())
                {
                    return;
                }
                objControl.UpdateGWBANK_MAP(objInfo);
            }
            LoadData();
            LockTextBox(true);
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isInsert = true;
            ClearText();
            txtSIBSBankCode.Focus();
            LockTextBox(false);
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            iID = Convert.ToInt32(grdList.CurrentRow.Cells["BANK_MAP_ID"].Value.ToString());
            txtSIBSBankCode.Text = grdList.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString();
            txtBankName.Text = grdList.CurrentRow.Cells["BANK_NAME"].Value.ToString();
            txtGWBankCode.Text = grdList.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString();
            txtBranch.Text = grdList.CurrentRow.Cells["BRANCH"].Value.ToString();
            txtDescription.Text = grdList.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
        }

        private void txtSIBSBankCode_Validated(object sender, EventArgs e)
        {
            if (txtSIBSBankCode.Text.Length > 20)
            {
                Common.ShowError("The maximium length is 20!", 3, MessageBoxButtons.OK);                
                txtSIBSBankCode.Text = "";
                txtSIBSBankCode.Focus();
            }
        }

        private void txtBankName_Validated(object sender, EventArgs e)
        {
            if (txtBankName.Text.Length > 100)
            {
                Common.ShowError("The maximium length is 100!", 3, MessageBoxButtons.OK);                
                txtBankName.Text = "";
                txtBankName.Focus();
            }
        }

        private void txtGWBankCode_Validated(object sender, EventArgs e)
        {
            if (txtGWBankCode.Text.Length > 20)
            {
                Common.ShowError("The maximium length is 5!", 3, MessageBoxButtons.OK);                
                txtGWBankCode.Text = "";
                txtGWBankCode.Focus();
            }
        }

        private void txtBranch_Validated(object sender, EventArgs e)
        {
            if (!clsCheck.IsNumeric(txtBranch.Text))
            {
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);                
                txtBranch.Text = "";
                txtBranch.Focus();
            }
            else if (txtBranch.Text.Length > 1)
            {
                Common.ShowError("The maximium length is 1!", 3, MessageBoxButtons.OK);                
                txtBranch.Text = "";
                txtBranch.Focus();
            }
        }

        private void txtDescription_Validated(object sender, EventArgs e)
        {
            if (txtDescription.Text.Length > 2)
            {
                Common.ShowError("The maximium length is 2!", 3, MessageBoxButtons.OK);                
                txtDescription.Text = "";
                txtDescription.Focus();
            }
        }

        private void ClearText()
        {
            txtSIBSBankCode.Text = "";
            txtGWBankCode.Text = "";
            txtDescription.Text = "";
            txtBranch.Text = "";
            txtBankName.Text = "";
        }

        private void LockTextBox(bool a)
        {
            txtBankName.ReadOnly = a;
            txtBranch.ReadOnly = a;
            txtDescription.ReadOnly = a;
            txtGWBankCode.ReadOnly = a;
            txtSIBSBankCode.ReadOnly = a;
        }

        

        private bool CheckID()
        {
            bool result = true;
            string ID = txtSIBSBankCode.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "You must input Var Type!";
                result = false;
            }
            else if (ID.Length > 10)
            {
                Common.ShowError("Var Type'length must be 10 character!", 3, MessageBoxButtons.OK);                
                result = false;
            }
           return result;
        }
        

        private void LoadData()
        {
            DataSet datDs = new DataSet();
            datDs = objControl.GetGWBANK_MAP();
            grdList.DataSource = datDs.Tables[0];
            grdList.Columns[0].Visible = false;
            grdList.Columns[1].HeaderText = "SIBS Bank code";
            grdList.Columns[1].Width = 100;
            grdList.Columns[2].HeaderText = "Bank name";
            grdList.Columns[2].Width = 150;
            grdList.Columns[3].HeaderText = "Bridge bank code";
            grdList.Columns[3].Width = 150;
            grdList.Columns[4].HeaderText = "Branch";
            grdList.Columns[4].Width = 100;
            grdList.Columns[5].HeaderText = "Description";
            grdList.Columns[5].Width = 50;
        }
    }
}
