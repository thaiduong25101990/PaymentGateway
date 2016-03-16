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
using BR.BRBusinessObject.ObjBesiness;
using BR.BRSYSTEM;
using BR.BRBusinessObject.ObjectInfo.BRIBPS;

namespace BR.BRIBPS
{
    public partial class frmIBPS_TAD_MAP : frmBasedata
    {
        private bool isInsert = false;
        private int iSelect;
        private bool isUpdate = false;
        
        IBPSTADMAPController tadMapControl = new IBPSTADMAPController();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeaderFrom = new BR.BRLib.Common.DGVColumnHeader();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeaderTo = new BR.BRLib.Common.DGVColumnHeader();
        private int iRows;
        IBPS_TAD_MAP_Info ibps_tad_map_info = new IBPS_TAD_MAP_Info();

        public frmIBPS_TAD_MAP()
        {
            InitializeComponent();

        }

        private void LoadData()
        {
            Load_combobox();
            Load_datadridview();
        }

        private void Load_combobox()
        {
            try
            {
                DataTable _dt = new DataTable();

                _dt = tadMapControl.GetComboboxData();

                if (_dt.Rows.Count > 0)
                {
                    cboTADHO.DataSource = _dt;
                    cboTADHO.DisplayMember = "TAD";
                    cboTADHO.ValueMember = "GW_BANK_CODE";
                    cboTADHO.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Load_datadridview()
        {
            try
            {
                string gw_Bank_Code = cboTADHO.SelectedValue.ToString();
                dgvBank.DataSource = tadMapControl.GetGata(gw_Bank_Code);

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmIBPS_TAD_MAP_Load(object sender, EventArgs e)
        {
            try
            {

                LoadData();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtgTADMAPFROM_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

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
                cmdAdd.Enabled = false;
                cmdSave.Enabled = true;
                isInsert = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Check_Select_Rows(DataGridView _dtg)
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < _dtg.Rows.Count)
                {
                    if (_dtg.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtg.Rows[b].Cells[0].Value.ToString() == "True")
                        { iSelect = 1; break; }
                    }
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtgTADMAPTO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    tadMapControl.DELETE(dgvBank.Rows[iRows].Cells[0].Value.ToString().Trim());
                    Common.ShowError("Delete data Successfully!", 2, MessageBoxButtons.OK);
                    //Search();
                    if (dgvBank.Rows.Count == 0)
                    {
                        cmdDelete.Enabled = false;
                    }
                    else
                    {
                        cmdDelete.Enabled = true;
                    }
                    cmdAdd.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdEdit.Enabled = true; 
                }
                else
                {
                    cmdDelete.Enabled = true;
                    cmdAdd.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdEdit.Enabled = true; 
                }
                Load_datadridview();
                clearVar();
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
                txtBankCode.Text = dgvBank.CurrentRow.Cells["BANK_CODE"].Value.ToString().Trim();
                txtBankCode.Enabled = false;

                txtBankName.Text = dgvBank.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                //txtNote.Text = dgvBank.CurrentRow.Cells["NOTE"].Value.ToString().Trim();
                cboTADHO.SelectedValue = dgvBank.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim();
                isInsert = false;
                isUpdate = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (Common.iSconfirm == 1)
            {
                if (txtBankCode.Text.Trim() == "")
                {
                    Common.ShowError("Message type is Empty!", 1, MessageBoxButtons.OK);
                    txtBankCode.Focus();
                    cmdSave.Enabled = true;
                    cmdAdd.Enabled = false;
                    cmdDelete.Enabled = false;
                    return;
                }
                try
                {
                    if (txtBankCode.Text == "")
                    {
                        MessageBox.Show("MSG_TYPE is Empty");
                    }
                    else
                    {
                        if (isInsert)
                        {
                            //kiem tra co du lieu trung khong
                            DataTable datSS = new DataTable();
                            //datSS = objctrl.GetKT(txtMsg_type.Text.Trim(), cbDirection.Text);
                            if (datSS.Rows.Count == 0)//cho ADD
                            {
                                ibps_tad_map_info.BANK_CODE = txtBankCode.Text.Trim();
                                ibps_tad_map_info.BANK_NAME = txtBankName.Text;
                                //ibps_tad_map_info.NOTE = txtNote.Text;
                                ibps_tad_map_info.GW_BANK_CODE = (string)cboTADHO.SelectedValue;
                                ibps_tad_map_info.TAD_CODE = ((string)cboTADHO.Text).Substring(0,5);
                                ibps_tad_map_info.UPDATETIME = DateTime.Now;
                                if (tadMapControl.ADD(ibps_tad_map_info) == 1)
                                {
                                    Common.ShowError("Insert data Successfull!", 1, MessageBoxButtons.OK);
                                    txtBankCode.Text = "";
                                    txtBankName.Text = "";
                                    //Search();
                                    cmdDelete.Enabled = true;
                                    cmdAdd.Enabled = true;
                                }
                                else
                                {
                                    cmdSave.Enabled = true;
                                    cmdDelete.Enabled = false;
                                    cmdAdd.Enabled = false;
                                }
                            }
                            else
                            {
                                Common.ShowError("Data has already exits!", 3, MessageBoxButtons.OK);
                                cmdSave.Enabled = true;
                                cmdDelete.Enabled = false;
                                cmdAdd.Enabled = false;
                            }
                        }
                        else
                        {

                            ibps_tad_map_info.BANK_CODE = txtBankCode.Text.Trim();
                            ibps_tad_map_info.BANK_NAME = txtBankName.Text;
                            //ibps_tad_map_info.NOTE = txtNote.Text;
                            ibps_tad_map_info.GW_BANK_CODE = (string)cboTADHO.SelectedValue;
                            ibps_tad_map_info.TAD_CODE = ((string)cboTADHO.Text).Substring(0, 5);
                            ibps_tad_map_info.UPDATETIME = DateTime.Now;
                            if (tadMapControl.UPDATE(ibps_tad_map_info) == 1)
                            {
                                Common.ShowError("Update data Successfull!", 1, MessageBoxButtons.OK);
                                txtBankCode.Enabled = true;
                                txtBankCode.Text = "";
                                txtBankName.Text = "";
                                //Search();
                                cmdDelete.Enabled = true;
                                cmdAdd.Enabled = true;
                                cmdEdit.Enabled = true;                                
                            }
                            else
                            {
                                cmdSave.Enabled = true;
                                cmdDelete.Enabled = false;
                                cmdAdd.Enabled = false;
                            }
                        }
                        Load_datadridview();                        
                    }
                    clearVar();
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
            }
            else
            {
                cmdDelete.Enabled = true;
                cmdAdd.Enabled = true;
                cmdSave.Enabled = false;
            }

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Common.ClearControl(this);
                //CommandStatus(true);
                dgvBank.Enabled = true;
                cmdAdd.Enabled = true;
                if (dgvBank.RowCount > 0)
                {
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                }

                cmdSave.Enabled = false;
                txtBankCode.Enabled = true;
                txtBankCode.Focus();
                cmdCancel.Enabled = false;
                clearVar();
            }
            catch (Exception ex)
            {
            }            
        }

        private void cboTADHO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isUpdate)
            Load_datadridview();
        }

        private void clearVar()
        {
            isInsert = false;
            isUpdate = false;
        }
    }
}
