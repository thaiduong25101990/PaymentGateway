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

namespace BR.BRSYSTEM
{
    public partial class frmBranch : frmBasedata 
    {
        public frmBranch()
        {
            InitializeComponent();
        }
        #region bien ,cac ham
        private clsLog objLog = new clsLog();
        private BRANCHController objBranchController = new BRANCHController();
        private BRANCHInfo objBranch = new BRANCHInfo();
        private CURRENCYController objControlCURRENCY = new CURRENCYController();
        private clsCheckInput clsCheck = new clsCheckInput();
        private GetData objGetData = new GetData();   
        private string BranchCode = "";
        private string BranchName = "";
        private string Type = "";
        //Cac tham so ghi log
        private DateTime tLogDate = DateTime.Now;
        private int iLogLevel = 1;
        private string sLogContent = "Branch Management";        
        private string sLogWorked = "";
        private string sLogTable = "BRANCH";
        private string sLogOldValue = "";
        private string sLogNewValue = "";
        #endregion

        #region Functions
        private void LoadDatagrid()
        {        
            try
            {
                DataSet dsBranch = new DataSet();
                BRANCHController objBranchControler = new BRANCHController();
                BRANCHInfo objBranchInfo = new BRANCHInfo();

                string strCondition;
                strCondition = " Where SIBS_BANK_CODE like '%" + txtBranchCode.Text.Trim() + "%'";
                strCondition = strCondition + " and upper(trim(BRAN_NAME)) like '%" + 
                    txtBranchName.Text.Trim().ToUpper() + "%'";
                if (cboType.SelectedValue.ToString() != "ALL")
                    strCondition = strCondition + " and upper(trim(BRAN_TYPE)) = '" + 
                        cboType.SelectedValue.ToString().ToUpper() + "'";
                dsBranch = objBranchControler.Select(strCondition);
                //dgvBranch.DataSource = dsBranch.Tables[0];
                dgvBranch.Columns.Clear();
                dgvBranch.Rows.Clear();

                dgvBranch.Columns.Add("SIBS_BANK_CODE", "Branch code");
                dgvBranch.Columns.Add("BRAN_NAME","Branch name");
                dgvBranch.Columns.Add("BRAN_TYPE", "Branch type");
                //dgvBranch.Columns.Add("BRAN_TYPE","Branch type");                
                for (int i = 0; i < dsBranch.Tables[0].Rows.Count; i++)
                {
                    dgvBranch.Rows.Add();
                    dgvBranch.Rows[i].HeaderCell.Value = i;
                    dgvBranch.Rows[i].Cells[0].Value = dsBranch.Tables[0].Rows[i]["SIBS_BANK_CODE"].ToString();
                    dgvBranch.Rows[i].Cells[1].Value = dsBranch.Tables[0].Rows[i]["BRAN_NAME"].ToString();
                    dgvBranch.Rows[i].Cells[2].Value = dsBranch.Tables[0].Rows[i]["BRAN_TYPE"].ToString();
                }
                if (dgvBranch.Rows.Count > 0)
                {                    
                    cmdDelete.Enabled = true;
                    cmdEdit.Enabled = true;
                }
                else
                {
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false ;
                }
                FormatDataGridView();
                lblTotalRow.Text = "Total number of branches: " + dsBranch.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void FormatDataGridView()
        {
            dgvBranch.RowHeadersVisible = true;
            dgvBranch.ColumnHeadersVisible = true;

            dgvBranch.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBranch.Columns[0].Width = 120;
            //dgvBranch.Columns[0].DefaultHeaderCellType.a
            dgvBranch.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;           
            dgvBranch.Columns[1].Width = 302;
            dgvBranch.Columns[2].Width = 120; 
        }

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)            
                this.Close();            
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);
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

        private bool CheckRowChoosing()
        {
            if (dgvBranch.RowCount == 0)
                return false;
            else
            {
                if (dgvBranch.SelectedRows == null)
                    return false;
            }
            //Gan o day
            return true;
        }

        private void View()
        {
            frmBranchInfo frm = new frmBranchInfo();
            frm.strSIBS_BANK_CODE = dgvBranch.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString();
            frm.strMode = "VIEW";
            frm.ShowDialog();
        }

        private bool Delete()
        {
            int iResult = 0;
            BranchCode = dgvBranch.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString();
            BranchName = dgvBranch.CurrentRow.Cells["BRAN_NAME"].Value.ToString();
            Type = dgvBranch.CurrentRow.Cells["BRAN_TYPE"].Value.ToString();
            if (Common.iSconfirm == 1)
            {
                DataSet dsStatusTTSP = new DataSet();
                dsStatusTTSP = objControlCURRENCY.GetCurrencyStatusTTSP(dgvBranch.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString());
                if (dsStatusTTSP.Tables[0].Rows.Count==0)
                {
                    iResult = objBranchController.Delete(dgvBranch.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString());
                }
                else
                {                    
                    Common.ShowError("Can not delete because this branch is in used!", 4, MessageBoxButtons.OK);
                    cmdDelete.Enabled = true;
                    return false;
                }
            }               
            if (iResult == 1)
            {
                //lay du lieu de ghi log                
                sLogWorked = "Delete";
                sLogOldValue = BranchCode + "/" + BranchName + "/" + Type;                
                //goi ham ghilog
                objLog.GhiLogUser(tLogDate, Common.Userid, sLogContent, iLogLevel, 
                    sLogWorked, sLogTable, sLogOldValue, sLogNewValue); 

                LoadDatagrid();
                return true;
            }
            else
                return false;

        }
              
        private void RefreshButton()
        {
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
        }
        #endregion

        private void frmBranch_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");  
 
            //Load data combobox
            if (!objGetData.FillDataComboBox(cboType, "CONTENT", "CDVAL", "ALLCODE",
            "GWTYPE='SYSTEM' AND CDNAME='BRAN_TYPE'", "CONTENT", true, true, "ALL"))
                return;
            //Load datagridview
            LoadDatagrid();
            this.Text = "SIBS Branch management";
        }

       
        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {           
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;                
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmBranchInfo frm = new frmBranchInfo();
            frm.strMode = "ADD";
            frm.ShowDialog();
            LoadDatagrid();
            RefreshButton();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (!CheckRowChoosing())
            {                
                Common.ShowError("Please choose a row to edit!", 4, MessageBoxButtons.OK);
                return;
            }
            frmBranchInfo frm = new frmBranchInfo();
            frm.strSIBS_BANK_CODE = dgvBranch.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString();
            frm.strMode = "EDIT";
            frm.ShowDialog();
            LoadDatagrid();
            RefreshButton();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();    
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            LoadDatagrid();
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            View();
        }

        private void dgvBranch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            View();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (Common.iSconfirm == 0)
                return;
            if (!CheckRowChoosing())
            {                
                Common.ShowError("Please choose a row to edit!", 4, MessageBoxButtons.OK);
                return;
            }
            if (!Delete())
                return;
            else
            {
                Common.ShowError("Delete data successfully!", 1, MessageBoxButtons.OK);                
                LoadDatagrid();
            }
        }

        private void txtBranchName_Leave(object sender, EventArgs e)
        {
            txtBranchName.Text = clsCheck.ConvertVietnamese(txtBranchName.Text.Trim());
        }
       

    }
}
