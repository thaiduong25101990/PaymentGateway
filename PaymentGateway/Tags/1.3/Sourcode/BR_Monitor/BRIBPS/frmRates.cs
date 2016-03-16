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

namespace BR.BRIBPS
{
    public partial class frmRates : frmBasedata
    {
        private GetData objGetData = new GetData();
        private IBPS_FEEInfo objFee = new IBPS_FEEInfo();
        private IBPS_FEEController objControlFee = new IBPS_FEEController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private USERSController objBOUser = new USERSController();
        private BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        private bool insert = false;
        private bool isLoaded = false;
        
        public DataTable dtFee;
        public DateTime fromdate;
        public DateTime todate;
        public DateTime hour;
        public string trantype="";
        public string freetype="";
        private int iID = 0;
        private string sFixed;
        private string sRate;
        private string sMin;
        private string sMax;
        private string sCCYCD;        
        private int iRows;

        public frmRates()
        {
            InitializeComponent();
        }

        private void frmRates_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                isLoaded = true;
                LoadData();
                isLoaded = false;
                GetData();
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
         *////////////////////////////////////////////////////
        private void LoadData()
        {
            try
            {                
                //Loai tien
                if (!objGetData.FillDataComboBox(cbCCYCD, "CCYCD", "CCYCD", "currencycode",
                    "", "CCYCD", true, true, "ALL"))
                    return;
                if (!objGetData.FillDataComboBox(cbCCYCD1, "CCYCD", "CCYCD", "currencycode",
                    "", "CCYCD", true, true, "ALL"))
                    return;
                //Loai chi nhanh tinh phi
                if (!objGetData.FillDataComboBox(cbFeeBranchType, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='IBPS' AND CDNAME='TYPE_BRANCH_FEE'", "CDVAL", true, false, ""))
                    return;
                //Chi nhanh
                if (!objGetData.FillDataComboBox(cbBranch, "BANK_NAME", "SIBS_BANK_CODE", "IBPS_BANK_MAP",
                    "substr(gw_bank_code,3,3) = '302'", "BANK_NAME", true, true, "ALL"))
                    return;
                //LOAI GIAM PHI
                if (!objGetData.FillDataComboBox(cbFeeDiscType, "CONTENT", "CDVAL", "ALLCODE",
                    " GWTYPE='IBPS' AND CDNAME = 'FEE_DISC_TYPE' AND CDVAL<>'0'", "LSTORD", true, false, ""))
                    return;
                //CHON LOAI PHI KHI TINH PHI
                if (!objGetData.FillDataComboBox(cbFeeType1, "CONTENT", "CDVAL", "ALLCODE",
                    " GWTYPE='SYSTEM' AND CDNAME = 'FEETYPE'", "LSTORD", true, false, ""))
                    return;
                //LOAI PHI KHI KHAI BAO THAM SO
                if (!objGetData.FillDataComboBox(cbFeeType, "CONTENT", "CDVAL", "ALLCODE",
                    " GWTYPE='SYSTEM' AND CDNAME = 'FEETYPE'", "LSTORD", true, false, ""))
                    return;
                //LOAI GIAO DICH
                if (!objGetData.FillDataComboBox(cbTransType, "CONTENT", "CDVAL", "ALLCODE",
                    " GWTYPE='IBPS' AND CDNAME = 'TRANS_TYPE'", "LSTORD", true, false, ""))
                    return;
                cmdSave.Enabled = false;
                cmdEdit.Enabled = false;
                cbFeeDiscType.Enabled = true;
                dtFeeDiscTime.Enabled = true;
                txtFixed.Enabled = false;
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


        /* ///////////////////////////////////////////////////
         * Ham get data
         * created date:
         * created by:
         * Updated date:
         * Updated by:
         *////////////////////////////////////////////////////
        private void GetData()
        {
            try
            {
                DataSet dsData = new DataSet();
                if (cbTransType.SelectedValue.ToString()=="3")
                    dsData = objControlFee.GetIBPS_FEE(cbTransType.SelectedValue.ToString(),
                    "0");
                else
                    dsData = objControlFee.GetIBPS_FEE(cbTransType.SelectedValue.ToString(), 
                        cbFeeDiscType.SelectedValue.ToString());                
                dgView.Rows.Clear();
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    int h = 0;
                    while (h < dsData.Tables[0].Rows.Count)
                    {
                        dgView.Rows.Add();
                        dgView.Rows[h].Cells["ID"].Value = dsData.Tables[0].Rows[h]["ID"].ToString();
                        dgView.Rows[h].Cells["TRANS_TYPE"].Value = dsData.Tables[0].Rows[h]["TRANS_TYPE"].ToString();                        
                        dgView.Rows[h].Cells["FEEDISC_TYPE1"].Value = dsData.Tables[0].Rows[h]["FEEDISC_TYPE1"].ToString();
                        dgView.Rows[h].Cells["FEEDISC_TIME"].Value = dsData.Tables[0].Rows[h]["FEEDISC_TIME"].ToString();
                        dgView.Rows[h].Cells["FIXED_FEE"].Value = dsData.Tables[0].Rows[h]["FIXED_FEE"].ToString();
                        dgView.Rows[h].Cells["RATE_FEE"].Value = dsData.Tables[0].Rows[h]["RATE_FEE"].ToString();
                        dgView.Rows[h].Cells["MIN_FEE"].Value = dsData.Tables[0].Rows[h]["MIN_FEE"].ToString();
                        dgView.Rows[h].Cells["MAX_FEE"].Value = dsData.Tables[0].Rows[h]["MAX_FEE"].ToString();
                        dgView.Rows[h].Cells["CCYCD"].Value = dsData.Tables[0].Rows[h]["CCYCD"].ToString();
                        dgView.Rows[h].Cells["FEEDISC_TYPE"].Value = dsData.Tables[0].Rows[h]["FEEDISC_TYPE"].ToString();
                        h = h + 1;
                    }
                    h = 1;
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        while (h < dgView.Columns.Count)
                        {
                            dgView.Columns[h].ReadOnly = true;
                            h = h + 1;
                        }
                    }                    
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                }
                else
                {
                    cmdAdd.Enabled = true;
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
                cmdFeeCal.Enabled = false;
                cbTransType.Enabled = false;
                if (cbTransType.SelectedValue.ToString() == "3")
                {
                    cbFeeDiscType.Enabled = false;
                    dtFeeDiscTime.Enabled = false;
                }
                else
                {
                    cbFeeDiscType.Enabled = true;
                    //if (cbFeeDiscType.SelectedValue.ToString() == "0")
                    //{
                    //    dtFeeDiscTime.Enabled = false;
                    //}
                    //else
                    //{
                    //    dtFeeDiscTime.Enabled = true;
                    //}
                    dtFeeDiscTime.Enabled = true;
                }                
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    txtFixed.Enabled = true;                    
                    txtFixed.Text = "";
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                }
                else
                {
                    txtFixed.Enabled = false;                    
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;
                    txtRate.Text = "";
                    txtMin.Text = "";
                    txtMax.Text = "";
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
                cmdFeeCal.Enabled = false;
                cbTransType.Enabled = false;
                if (cbTransType.SelectedValue.ToString() == "3")
                {
                    cbFeeDiscType.Enabled = false;
                    dtFeeDiscTime.Enabled = false;
                }
                else
                {
                    cbFeeDiscType.Enabled = true;
                    //if (cbFeeDiscType.SelectedValue.ToString() == "0")
                    //{
                    //    dtFeeDiscTime.Enabled = false;
                    //}
                    //else
                    //{
                    //    dtFeeDiscTime.Enabled = true;
                    //}
                    dtFeeDiscTime.Enabled = true;
                }                
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    txtFixed.Enabled = true;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                }
                else
                {
                    txtFixed.Enabled = false;
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;
                }                
                dgView.Focus();
                dgView.Enabled = false;
                iID = Convert.ToInt32(dgView.CurrentRow.Cells["ID"].Value.ToString().Trim());
                txtID.Text = iID.ToString();
                cbCCYCD.SelectedValue = dgView.CurrentRow.Cells["CCYCD"].Value.ToString();
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    txtFixed.Text = dgView.CurrentRow.Cells["FIXED_FEE"].Value.ToString().Trim();                    
                    if (!string.IsNullOrEmpty(dgView.CurrentRow.Cells["RATE_FEE"].ToString()))
                        txtRate.Text = dgView.CurrentRow.Cells["RATE_FEE"].Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(dgView.CurrentRow.Cells["MIN_FEE"].ToString()))
                        txtMin.Text = dgView.CurrentRow.Cells["MIN_FEE"].Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(dgView.CurrentRow.Cells["MAX_FEE"].ToString()))
                        txtMax.Text = dgView.CurrentRow.Cells["MAX_FEE"].Value.ToString().Trim();
                }
                else
                {
                    if (!string.IsNullOrEmpty(dgView.CurrentRow.Cells["FIXED_FEE"].ToString()))
                        txtFixed.Text = dgView.CurrentRow.Cells["FIXED_FEE"].Value.ToString().Trim();
                    txtRate.Text = dgView.CurrentRow.Cells["RATE_FEE"].Value.ToString().Trim();
                    txtMin.Text = dgView.CurrentRow.Cells["MIN_FEE"].Value.ToString().Trim();
                    txtMax.Text = dgView.CurrentRow.Cells["MAX_FEE"].Value.ToString().Trim();
                }
                if (!string.IsNullOrEmpty(dgView.CurrentRow.Cells["FEEDISC_TYPE"].ToString()))
                {
                    if (dgView.CurrentRow.Cells["FEEDISC_TYPE"].ToString() != "0")
                    {
                        cbFeeDiscType.SelectedValue = dgView.CurrentRow.Cells["FEEDISC_TYPE"].Value.ToString();
                    }
                }
                if (cbTransType.SelectedValue.ToString() != "3")
                {
                    dtFeeDiscTime.Value = Convert.ToDateTime(dgView.CurrentRow.Cells["FEEDISC_TIME"].Value.ToString());
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
                if (cbCCYCD.SelectedValue.ToString() == "ALL")
                {
                    Common.ShowError("Select currency", 2, MessageBoxButtons.OK);
                    return false;
                }
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {                    
                    if (string.IsNullOrEmpty(txtFixed.Text))
                    {
                        Common.ShowError("Input Fixed fee", 2, MessageBoxButtons.OK);
                        return false;
                    }
                }
                //Phi theo ty le
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
                objFee.CCYCD = cbCCYCD.SelectedValue.ToString();
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    objFee.FIXED_FEE = Convert.ToDouble(txtFixed.Text.ToString());
                    if (string.IsNullOrEmpty(txtMax.Text))
                        objFee.MAX_FEE = 0;
                    else
                        objFee.MAX_FEE = Convert.ToDouble(txtMax.Text.ToString());
                    if (string.IsNullOrEmpty(txtMin.Text))
                        objFee.MIN_FEE = 0;
                    else
                        objFee.MIN_FEE = Convert.ToDouble(txtMin.Text.ToString());
                    if (string.IsNullOrEmpty(txtRate.Text))
                        objFee.RATE_FEE = 0;
                    else
                        objFee.RATE_FEE = Convert.ToDouble(txtRate.Text.ToString());                    
                }
                //Phi ty le
                else
                {
                    if (string.IsNullOrEmpty(txtFixed.Text))
                        objFee.FIXED_FEE = 0;
                    else
                        objFee.FIXED_FEE = Convert.ToDouble(txtFixed.Text.ToString());
                    objFee.MAX_FEE = Convert.ToDouble(txtMax.Text.ToString());
                    objFee.MIN_FEE = Convert.ToDouble(txtMin.Text.ToString());
                    objFee.RATE_FEE = Convert.ToDouble(txtRate.Text.ToString());                                        
                }                
                //Loai giao dich
                objFee.TRANS_TYPE = cbTransType.SelectedValue.ToString();
                //Loai mien giam phi
                if (cbTransType.SelectedValue.ToString() == "3")
                {
                    objFee.FEEDISC_TYPE = "0";
                    objFee.FEEDISC_TIME = dtFeeDiscTime.Value;
                }
                else
                {
                    objFee.FEEDISC_TYPE = cbFeeDiscType.SelectedValue.ToString();
                    objFee.FEEDISC_TIME = dtFeeDiscTime.Value;                    
                }                
                if (string.IsNullOrEmpty(txtID.Text))
                    objFee.ID = 0;
                else
                    objFee.ID = Convert.ToInt32(txtID.Text.ToString());

                //Kiem tra loai tien da co bieu phi
                DataSet dsData = new DataSet();
                dsData = objControlFee.CheckCCYCD(objFee.CCYCD, objFee.ID,
                    objFee.TRANS_TYPE, objFee.FEEDISC_TYPE);
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

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    //check data
                    if (!CheckData())
                    {
                        cmdSave.Enabled = true;
                        return;
                    }
                    //insert du lieu
                    if (insert == true)
                    {
                        Insert();
                    }
                    //cap nhat
                    else if (insert == false)
                    {
                        Update();
                    }
                    cbTransType.Enabled = true;
                    txtFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                    GetData();
                }
                else
                {
                    if (cbTransType.SelectedValue.ToString() == "3")
                    {
                        cbFeeDiscType.Enabled = false;
                        dtFeeDiscTime.Enabled = false;
                    }
                    else
                    {
                        cbFeeDiscType.Enabled = true;
                        //if (cbFeeDiscType.SelectedValue.ToString() == "0")
                        //{
                        //    dtFeeDiscTime.Enabled = false;
                        //}
                        //else
                        //{
                        //    dtFeeDiscTime.Enabled = true;
                        //}
                        dtFeeDiscTime.Enabled = true;
                    }
                    if (dgView.Rows.Count > 0)
                    {
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdSave.Enabled = false;
                    }
                    else
                    {
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = false;
                        cmdSave.Enabled = false;
                    }
                    dgView.Enabled = true;
                    cbTransType.Enabled = true;
                    cmdFeeCal.Enabled = true;
                    txtFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void Insert()
        {
            try
            {
                if (objControlFee.AddIBPS_FEE(objFee) == 1)
                {
                    MessageBox.Show("Insert data successfully", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    insert = false;
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdFeeCal.Enabled = true;                                        
                }
                else
                {
                    MessageBox.Show("Insert data Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);                    
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Update()
        {
            try
            {
                int iFeeType = 0;
                iFeeType = Convert.ToInt16(cbFeeType.SelectedValue.ToString());
                if (objControlFee.UpdateIBPS_FEE(objFee,iFeeType) == 1)
                {
                    MessageBox.Show("Update data successfully", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdFeeCal.Enabled = true;
                    dgView.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Update data Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);                    
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
                if (dtFromDate.Checked == false)
                {
                    dtToDate.Checked = false;
                }
                else
                {
                    dtToDate.Checked = true;
                }
                if (dtFromDate.Value > dtToDate.Value)
                {
                    dtFromDate.Value = dtToDate.Value;
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
                if (dtToDate.Checked == false)
                {
                    dtFromDate.Checked = false;
                }
                else
                {
                    dtFromDate.Checked = true;
                }
                if (dtToDate.Value < dtFromDate.Value)
                {
                    this.dtToDate.Value = dtFromDate.Value;
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
                
        private void cmdFeeCal_Click(object sender, EventArgs e)
        {
            try
            {                
                string strUserid = "";
                string strBranch = "";
                DataSet dtData = new DataSet();

                dtData = objBOUser.Userid_UD(Common.Userid);
                strUserid = dtData.Tables[0].Rows[0]["USERNAME"].ToString();
                strBranch = dtData.Tables[0].Rows[0]["BRANCH"].ToString().Substring(0, 3).PadLeft(5, '0');

                dtFee=objControlFee.CAL_FEE(dtFromDate.Value, dtToDate.Value,
                    Convert.ToInt16(cbFeeBranchType.SelectedValue.ToString()),
                    Convert.ToInt16(cbFeeType1.SelectedValue.ToString()),
                    cbBranch.SelectedValue.ToString(),
                    cbBranch.SelectedValue.ToString(),
                    cbCCYCD1.SelectedValue.ToString());
                if (dtFee != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "BK02";
                    frmPrint.HMdat = dtFee;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = dtFromDate.Text.ToString();
                    frmPrint.dtto = dtToDate.Text.ToString();
                    frmPrint.pFeeType = Convert.ToInt16(cbFeeType1.SelectedValue.ToString());
                    frmPrint.pFeeBranchType = Convert.ToInt16(cbFeeBranchType.SelectedValue.ToString());
                    frmPrint.pCCYCD = cbCCYCD1.SelectedValue.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.pUserid = strUserid;
                    DataSet ds = new DataSet();
                    ds =objControlFee.GetBRANCH8(cbBranch.SelectedValue.ToString());
                    if (ds!=null)
                    {
                        if (ds.Tables[0].Rows.Count>1)
                            frmPrint.pBranch8 = ds.Tables[0].Rows[0]["gw_bank_code"].ToString();
                        else
                            frmPrint.pBranch8 = ds.Tables[0].Rows[0]["gw_bank_code"].ToString();
                    }
                    else
                        frmPrint.pBranch8 = "";
                    frmPrint.ShowDialog();
                }
                else
                {
                    Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);                    
                }
            }
            catch 
            {
                Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);                    
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cbTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isLoaded==false)
                {
                    if (cbTransType.SelectedValue.ToString()== "3")
                    {
                        cbFeeDiscType.Enabled = false;
                        dtFeeDiscTime.Enabled = false;
                    }
                    else
                    {
                        cbFeeDiscType.Enabled = true;
                        //if (cbFeeDiscType.SelectedValue.ToString() == "0")
                        //{
                        //    dtFeeDiscTime.Enabled = false;
                        //}
                        //else
                        //{
                        //    dtFeeDiscTime.Enabled = true;
                        //    dtFeeDiscTime.Value = DateTime.Now;
                        //}
                        dtFeeDiscTime.Value = DateTime.Now;
                    }
                    txtFixed.Text = "";
                    txtRate.Text = "";
                    txtMin.Text = "";
                    txtMax.Text = "";
                    GetData();
                }
            }
            catch 
            {
                Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);                    
            }
        }

        private void cbFeeDiscType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isLoaded == false)
                {
                    //if (cbFeeDiscType.SelectedValue.ToString() == "0")
                    //{
                    //    dtFeeDiscTime.Enabled = false;
                    //}
                    //else
                    //{
                    //    dtFeeDiscTime.Enabled = true;
                    //}
                    //GetData();
                }
            }
            catch 
            {
                Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);                    
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFeeType.SelectedValue.ToString() == "1")
            {
                txtFixed.Enabled = true;                
                txtRate.Enabled = false;
                txtMin.Enabled = false;
                txtMax.Enabled = false;
            }
            else
            {
                txtFixed.Enabled = false;                
                txtRate.Enabled = true;
                txtMin.Enabled = true;
                txtMax.Enabled = true;
            }
            txtFixed.Text = "";
            txtRate.Text = "";
            txtMin.Text = "";
            txtMax.Text = "";
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

        private void Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        {
            objuser_msg_log.LOG_DATE = Logdate;
            objuser_msg_log.USERID = strUsername;
            objuser_msg_log.CONTENT = strContent;
            objuser_msg_log.STATUS = Log_level;
            objuser_msg_log.WORKED = strWorked;
            objuser_msg_log.TABLE_ACCESS = strTale_Access;
            objuser_msg_log.OLD_VALUE = strOld_value;
            objuser_msg_log.NEW_VALUE = strNew_value;
            objcontroluser_msg_log.AddUSER_MSG_LOG1(objuser_msg_log);
        }

        private void frmRates_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
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

        private void txtFixed_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cmdCalDetail_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserid = "";
                string strBranch = "";
                DataSet dtData = new DataSet();

                dtData = objBOUser.Userid_UD(Common.Userid);
                strUserid = dtData.Tables[0].Rows[0]["USERNAME"].ToString();
                strBranch = dtData.Tables[0].Rows[0]["BRANCH"].ToString().Substring(0, 3).PadLeft(5, '0');

                dtFee = objControlFee.CAL_FEE_DETAIL(dtFromDate.Value, dtToDate.Value,
                    Convert.ToInt16(cbFeeBranchType.SelectedValue.ToString()),
                    Convert.ToInt16(cbFeeType1.SelectedValue.ToString()),
                    cbBranch.SelectedValue.ToString(),
                    cbBranch.SelectedValue.ToString(),
                    cbCCYCD1.SelectedValue.ToString());
                if (dtFee != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "BK02_DETAIL";
                    frmPrint.HMdat = dtFee;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = dtFromDate.Text.ToString();
                    frmPrint.dtto = dtToDate.Text.ToString();
                    frmPrint.pFeeType = Convert.ToInt16(cbFeeType1.SelectedValue.ToString());
                    frmPrint.pFeeBranchType = Convert.ToInt16(cbFeeBranchType.SelectedValue.ToString());
                    frmPrint.pCCYCD = cbCCYCD1.SelectedValue.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.pUserid = strUserid;   
                    DataSet ds = new DataSet();
                    ds = objControlFee.GetBRANCH8(cbBranch.SelectedValue.ToString());
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 1)
                            frmPrint.pBranch8 = ds.Tables[0].Rows[0]["gw_bank_code"].ToString();
                        else
                            frmPrint.pBranch8 = ds.Tables[0].Rows[0]["gw_bank_code"].ToString();
                    }
                    else
                        frmPrint.pBranch8 = "";
                    frmPrint.ShowDialog();
                }
                else
                {
                    Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);
                }
            }
            catch
            {
                Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCalExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserid = "";
                string strBranch = "";
                DataSet dtData = new DataSet();

                dtData = objBOUser.Userid_UD(Common.Userid);
                strUserid = dtData.Tables[0].Rows[0]["USERNAME"].ToString();
                strBranch = dtData.Tables[0].Rows[0]["BRANCH"].ToString().Substring(0, 3).PadLeft(5, '0');

                dtFee = objControlFee.CAL_FEE_DETAIL_EXCEL(dtFromDate.Value, dtToDate.Value,
                    Convert.ToInt16(cbFeeBranchType.SelectedValue.ToString()),
                    Convert.ToInt16(cbFeeType1.SelectedValue.ToString()),
                    cbBranch.SelectedValue.ToString(),
                    cbBranch.SelectedValue.ToString(),
                    cbCCYCD1.SelectedValue.ToString());
                if (dtFee != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "BK02_DETAIL";
                    frmPrint.HMdat = dtFee;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = dtFromDate.Text.ToString();
                    frmPrint.dtto = dtToDate.Text.ToString();
                    frmPrint.pFeeType = Convert.ToInt16(cbFeeType1.SelectedValue.ToString());
                    frmPrint.pFeeBranchType = Convert.ToInt16(cbFeeBranchType.SelectedValue.ToString());
                    frmPrint.pCCYCD = cbCCYCD1.SelectedValue.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.pUserid = strUserid;
                    DataSet ds = new DataSet();
                    ds = objControlFee.GetBRANCH8(cbBranch.SelectedValue.ToString());
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 1)
                            frmPrint.pBranch8 = ds.Tables[0].Rows[0]["gw_bank_code"].ToString();
                        else
                            frmPrint.pBranch8 = ds.Tables[0].Rows[0]["gw_bank_code"].ToString();
                    }
                    else
                        frmPrint.pBranch8 = "";
                    frmPrint.ShowDialog();
                }
                else
                {
                    Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);
                }
            }
            catch
            {
                Common.ShowError("Error caculator free", 2, MessageBoxButtons.OK);
            }
        }
    }
}
