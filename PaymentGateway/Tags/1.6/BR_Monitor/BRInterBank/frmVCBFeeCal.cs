using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRInterBank
{
    public partial class frmVCBFeeCal : frmBasedata
    {
        private GetData objGetData = new GetData();
        private VCB_FEE_Info objFEE = new VCB_FEE_Info();
        private VCB_FEEController objControlFEE = new VCB_FEEController();
        private BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();

        private bool insert = false;
        private bool isLoaded = false;

        public DataTable dtFEE;
        public DateTime fromdate;
        public DateTime todate;
        public int hardFEE=0;
        public int maxFEE=0;
        public int minFEE=0;
        private int iFeeType = 0;
        private int iRows;

        public frmVCBFeeCal()
        {
            InitializeComponent();
        }

        private void frmVCBFeeCal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                isLoaded = true;
                LoadData();
                isLoaded = false;
                Getdata("ALL");
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /* ///////////////////////////////////////////////////
         * Ham Load data
         * created date:
         * created by:
         * Updated date:
         * Updated by:
         */
        ///////////////////////////////////////////////////
        private void LoadData()
        {
            try
            {
                if (!objGetData.FillDataComboBox(cbCCYCD, "CCYCD", "CCYCD", "currencycode",
                    "", "CCYCD", true, true, "ALL"))
                    return;
                if (!objGetData.FillDataComboBox(cbBranch, "BRAN_NAME", "SIBS_BANK_CODE", "BRANCH",
                    "", "BRAN_NAME", true, true, "ALL"))
                    return;
                if (!objGetData.FillDataComboBox(cbFeeType, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME= 'FEETYPE'", "CDVAL", true, false, ""))
                    return;
                cmdSave.Enabled = false;
                cmdEdit.Enabled = false;
                txtFeeFixed.Enabled = false;
                txtRate.Enabled = false;
                txtMin.Enabled = false;
                txtMax.Enabled = false;
                dgView.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Getdata(string sCCYCD)
        {
            try
            {                
                DataTable _dt = new DataTable();
                _dt = objControlFEE.Get_VCB_FEE(sCCYCD);
                dgView.Rows.Clear();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    int h = 0;
                    while (h < _dt.Rows.Count)
                    {
                        dgView.Rows.Add();
                        dgView.Rows[h].Cells["ID"].Value = _dt.Rows[h]["ID"].ToString();
                        dgView.Rows[h].Cells["FIXEDFEE"].Value = _dt.Rows[h]["FIXEDFEE"].ToString();
                        dgView.Rows[h].Cells["RATE"].Value = _dt.Rows[h]["RATE"].ToString();
                        dgView.Rows[h].Cells["MINFEE"].Value = _dt.Rows[h]["MINFEE"].ToString();
                        dgView.Rows[h].Cells["MAXFEE"].Value = _dt.Rows[h]["MAXFEE"].ToString();
                        dgView.Rows[h].Cells["CCYCD"].Value = _dt.Rows[h]["CCYCD"].ToString();
                        h = h + 1;
                    }
                    h = 1;
                    if (_dt.Rows.Count > 0)
                    {
                        while (h < dgView.Columns.Count)
                        {
                            dgView.Columns[h].ReadOnly = true;
                            h = h + 1;
                        }
                    }                    
                    cmdEdit.Enabled = true;
                }
                else
                {                    
                    cmdEdit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                insert = true;
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSave.Enabled = true;                
                cmdPhi.Enabled = false;
                cbCCYCD.Enabled = true;
                txtFeeFixed.Text = "";
                txtRate.Text = "";
                txtMin.Text = "";
                txtMax.Text = "";
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    
                    txtFeeFixed.Enabled = true;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;                
                }
                //Phi ty le
                else
                {
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;                
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                insert = false;
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSave.Enabled = true;                
                cmdPhi.Enabled = false;
                cbFeeType.Enabled = true;                
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    txtFeeFixed.Enabled = true;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                }
                //Phi ty le
                else
                {
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;
                }
                txtID.Text = dgView.CurrentRow.Cells["ID"].Value.ToString().Trim();
                txtFeeFixed.Text = dgView.CurrentRow.Cells["FIXEDFEE"].Value.ToString().Trim();
                txtRate.Text = dgView.CurrentRow.Cells["RATE"].Value.ToString().Trim();
                txtMin.Text = dgView.CurrentRow.Cells["MINFEE"].Value.ToString().Trim();
                txtMax.Text = dgView.CurrentRow.Cells["MAXFEE"].Value.ToString().Trim();
                cbCCYCD.SelectedValue = dgView.CurrentRow.Cells["CCYCD"].Value.ToString().Trim();
                cbCCYCD.Enabled = false;
                dgView.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    if (!CheckData())
                    {                       
                        cmdSave.Enabled = true;                        
                        return;
                    }
                    if (insert == true)//insert du lieu                    
                    {
                        Insert();
                        Getdata("ALL");
                    }
                    else
                    {
                        Update_Rate();
                        Getdata("ALL");
                    }
                }
                else
                {
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMax.Enabled = false;
                    txtMin.Enabled = false;
                    cmdAdd.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdPhi.Enabled = true;
                    cbCCYCD.Enabled = true;
                    if (insert == true)
                    {                       
                        if (dgView.Rows.Count>0)
                            cmdEdit.Enabled = true;
                        else
                            cmdEdit.Enabled = false;
                    }
                    else
                    {                                              
                        cmdEdit.Enabled = true;                        
                        cbFeeType.Enabled = true;
                        dgView.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private Boolean CheckData()
        {
            try
            {
                //if (dateto.Value.GetDateTimeFormats('dd/MM/yyyy') < datefromGetDateTimeFormats('dd/MM/yyyy'))
                //{
                //    Common.ShowError("Input 'To date' >= 'From date'", 2, MessageBoxButtons.OK);
                //    return false;
                //}

                if (cbCCYCD.SelectedValue.ToString() == "ALL")
                {
                    Common.ShowError("Select currency", 2, MessageBoxButtons.OK);
                    return false;
                }
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    if (string.IsNullOrEmpty(txtFeeFixed.Text))
                    {
                        Common.ShowError("Input Fixed fee", 2, MessageBoxButtons.OK);
                        return false;
                    }                    
                }
                else
                {
                    if (string.IsNullOrEmpty(txtRate.Text))
                    {
                        Common.ShowError("Input Rate", 2, MessageBoxButtons.OK);
                        return false;
                    }
                    if (!Common.IsNumeric(txtRate.Text.Trim(), false))
                    {
                        Common.ShowError("You must input a number for RATE", 2, MessageBoxButtons.OK);
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtMax.Text))
                    {
                        Common.ShowError("Input Max", 2, MessageBoxButtons.OK);
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtMin.Text))
                    {
                        Common.ShowError("Input Min", 2, MessageBoxButtons.OK);
                        return false;
                    }
                }
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    iFeeType = 1;
                    objFEE.FIXEDFEE = Convert.ToDouble(txtFeeFixed.Text.ToString());
                    objFEE.CCYCD = cbCCYCD.SelectedValue.ToString();                    
                    objFEE.MAXFEE = 0;
                    objFEE.MINFEE = 0;
                    objFEE.RATE = 0;                    
                }
                //Phi ty le
                else
                {
                    iFeeType = 2;
                    objFEE.FIXEDFEE = 0;
                    objFEE.CCYCD = cbCCYCD.SelectedValue.ToString();
                    objFEE.MAXFEE = Convert.ToDouble(txtMax.Text.ToString());
                    objFEE.MINFEE = Convert.ToDouble(txtMin.Text.ToString());
                    objFEE.RATE = Convert.ToDouble(txtRate.Text.ToString());
                }                
                if (string.IsNullOrEmpty(txtID.Text))
                    objFEE.ID = 0;
                else
                    objFEE.ID = Convert.ToInt32(txtID.Text.ToString());

                //Kiem tra loai tien da co bieu phi
                DataSet dsData = new DataSet();
                dsData = objControlFEE.CheckCCYCD(objFEE.CCYCD, objFEE.ID);
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        Common.ShowError("Type Currency is existed", 2, MessageBoxButtons.OK);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }

        private void Insert()
        {
            try
            {   
                if (objControlFEE.ADD_VCB_FEE(objFEE) == 1)
                {
                    MessageBox.Show("Insert data successfully", 
                        Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdPhi.Enabled = true;
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;                    
                }
                else
                {
                    MessageBox.Show("Insert data Error!", Common.sCaption,
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);                    
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Update_Rate()
        {
            try
            {                
                if (objControlFEE.UPDATE_VCB_FEE(objFEE, iFeeType) == 1)
                {
                    MessageBox.Show("Update data successfully", 
                        Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdPhi.Enabled = true;
                    cbCCYCD.Enabled = true;
                    dgView.Enabled = true;
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;                    
                }
                else
                {
                    MessageBox.Show("Update data Error!", Common.sCaption, 
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (datefrom.Checked == false)
                {
                    dateto.Checked = false;
                }
                else
                {
                    dateto.Checked = true;
                }
                if (datefrom.Value > dateto.Value)
                {
                    datefrom.Value = dateto.Value;
                }
                else
                {
                }
            }
            catch(Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateto.Checked == false)
                {
                    datefrom.Checked = false;
                }
                else
                {
                    datefrom.Checked = true;
                }
                if (dateto.Value < datefrom.Value)
                {
                    this.dateto.Value = datefrom.Value;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }               

        private void cmdPhi_Click(object sender, EventArgs e)
        {
            try
            {                   
                dtFEE=objControlFEE.CAL_FEE(datefrom.Value, dateto.Value,
                    cbBranch.SelectedValue.ToString(), 
                    Convert.ToInt16( cbFeeType.SelectedValue.ToString()),
                    cbCCYCD.SelectedValue.ToString());
                if (dtFEE != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "VCB_FEE_CAL";
                    frmPrint.HMdat = dtFEE;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = datefrom.Text.ToString();
                    frmPrint.dtto = dateto.Text.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.ShowDialog();
                }
                else
                {
                    Common.ShowError("Error caculator FEE", 2, MessageBoxButtons.OK);                    
                }
            }
            catch 
            {
                Common.ShowError("Error caculator FEE", 2, MessageBoxButtons.OK);                    
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void txtFeeFixed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }
        private void txtMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFeeType.SelectedValue.ToString() == "1")
            {
                if (cmdPhi.Enabled == false)
                {
                    txtFeeFixed.Enabled = true;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                }
            }
            else
            {
                if (cmdPhi.Enabled == false)
                {
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;
                }
            }
        }

        private void cbCCYCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCCYCD.Enabled = true;
            DataTable _dt = new DataTable();
            _dt = objControlFEE.Get_VCB_FEE(cbCCYCD.SelectedValue.ToString());
            if (_dt.Rows.Count > 0)
            {
                
            }
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows = e.RowIndex; }
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < this.dgView.RowCount; i++)
                    {
                        this.dgView.EndEdit();
                        string re_value = this.dgView.Rows[i].Cells[0].EditedFormattedValue.ToString();
                    }
                }
            }
        }

        private void dgView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < dgView.Rows.Count; i++)
                    {
                        dgView.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int h = 0;

                if (dgView.Rows.Count > 0)
                {
                    if (e.KeyData == Keys.Space)
                    {
                        foreach (DataGridViewRow selectedCell in dgView.SelectedRows)
                        {
                            h = selectedCell.Cells[0].RowIndex;
                            if (dgView.Rows[h].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dgView.Rows[h].Cells[0].Value.ToString() == "True")
                                {
                                    dgView.Rows[h].Cells[0].Value = null;
                                }
                                else
                                {
                                    dgView.Rows[h].Cells[0].Value = "True";
                                }
                            }
                            else
                            {
                                dgView.Rows[h].Cells[0].Value = "True";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdPrintDetail_Click(object sender, EventArgs e)
        {
            try
            {
                dtFEE = objControlFEE.CAL_FEE_DETAIL(datefrom.Value, dateto.Value,
                    cbBranch.SelectedValue.ToString(),
                    Convert.ToInt16(cbFeeType.SelectedValue.ToString()),
                    cbCCYCD.SelectedValue.ToString());
                if (dtFEE != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "VCB_FEE_CAL_DETAIL";
                    frmPrint.HMdat = dtFEE;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = datefrom.Text.ToString();
                    frmPrint.dtto = dateto.Text.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.ShowDialog();
                }
                else
                {
                    Common.ShowError("Error caculator FEE", 2, MessageBoxButtons.OK);
                }
            }
            catch
            {
                Common.ShowError("Error caculator FEE", 2, MessageBoxButtons.OK);
            }
        }

        private void cmdPrintExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dtFEE = objControlFEE.CAL_FEE_DETAIL_EXCEL(datefrom.Value, dateto.Value,
                    cbBranch.SelectedValue.ToString(),
                    Convert.ToInt16(cbFeeType.SelectedValue.ToString()),
                    cbCCYCD.SelectedValue.ToString());
                if (dtFEE != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "VCB_FEE_CAL_DETAIL_EXCEL";
                    frmPrint.HMdat = dtFEE;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = datefrom.Text.ToString();
                    frmPrint.dtto = dateto.Text.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.ShowDialog();
                }
                else
                {
                    Common.ShowError("Error caculator FEE", 2, MessageBoxButtons.OK);
                }
            }
            catch
            {
                Common.ShowError("Error caculator FEE", 2, MessageBoxButtons.OK);
            }
        }

    }
}
