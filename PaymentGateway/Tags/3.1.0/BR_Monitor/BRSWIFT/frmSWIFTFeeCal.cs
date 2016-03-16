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

namespace BR.BRSWIFT
{
    public partial class frmSWIFTFeeCal : frmBasedata
    {
        private GetData objGetData = new GetData();
        private SWIFT_FEE_Info objFEE = new SWIFT_FEE_Info();
        private SWIFT_FEEController objControlFEE = new SWIFT_FEEController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();

        private bool insert = false;
        public DataTable dtFEE;
        public DateTime fromdate;
        public DateTime todate;
        public DateTime hour;
        public string trantype="";
        public string FEEtype="";
        public int hardFEE=0;
        public int maxFEE=0;
        public int minFEE=0;        
        private int iID = 0;
        private int iRows;        
        private string sMsgType;
        private string sFixed;
        private string sRate;
        private string sMin;
        private string sMax;
        private string sCCYCD;
        
        public frmSWIFTFeeCal()
        {
            InitializeComponent();
        }

        private void frmSWIFTFeeCal_Load(object sender, EventArgs e)
        {
            try
            {
                dgView.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dgView.Columns[0].HeaderCell = dgvColumnHeader;
                dgView.Columns[0].Width = 26;

                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                //Load du lieu cho combobox
                if (!objGetData.FillDataComboBox(cbFeeType, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME= 'FEETYPE'", "CDVAL", true, false, ""))
                    return;
                if (!objGetData.FillDataComboBox(cbCCYCD, "CCYCD", "CCYCD", "currencycode",
                    "", "CCYCD", true, true, "ALL"))
                    return;
                if (!objGetData.FillDataComboBox(cbBranch, "BRAN_NAME", "SIBS_BANK_CODE",
                    "BRANCH", "", "BRAN_NAME", true, true, "ALL"))
                    return;
                Getdata();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Getdata()
        {
            try
            {
                DataSet dsData = new DataSet();
                dsData = objControlFEE.Get_SWIFT_FEE();

                dgView.Rows.Clear();
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    int h = 0;
                    while (h < dsData.Tables[0].Rows.Count)
                    {
                        dgView.Rows.Add();
                        dgView.Rows[h].Cells["ID"].Value = dsData.Tables[0].Rows[h]["ID"].ToString();
                        dgView.Rows[h].Cells["MSG_TYPE"].Value = dsData.Tables[0].Rows[h]["MSG_TYPE"].ToString();
                        dgView.Rows[h].Cells["FIXED_FEE"].Value = dsData.Tables[0].Rows[h]["FIXED_FEE"].ToString();
                        dgView.Rows[h].Cells["RATE_FEE"].Value = dsData.Tables[0].Rows[h]["RATE_FEE"].ToString();
                        dgView.Rows[h].Cells["MIN_FEE"].Value = dsData.Tables[0].Rows[h]["MIN_FEE"].ToString();
                        dgView.Rows[h].Cells["MAX_FEE"].Value = dsData.Tables[0].Rows[h]["MAX_FEE"].ToString();
                        dgView.Rows[h].Cells["CCYCD"].Value = dsData.Tables[0].Rows[h]["CCYCD"].ToString();
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
                    //Tong so ban ghi
                    lblSum.Text = Convert.ToString(dsData.Tables[0].Rows.Count);
                }               
                
                if (dgView.Rows.Count==0)
                {                                     
                    cmdEdit.Enabled = false;                    
                    cmdPhi.Enabled = false;
                    cmdDelete.Enabled = false;
                }
                else if (dgView.Rows.Count > 0)
                {                    
                    cmdEdit.Enabled = true;             
                    cmdPhi.Enabled = true;
                    cmdDelete.Enabled = true;                                  
                }
                cmdAdd.Enabled = true; 
                cmdSave.Enabled = false;

                txtFeeFixed.Enabled = false;
                txtMax.Enabled = false;
                txtMin.Enabled = false;
                txtRate.Enabled = false;
                txtMsgType.Enabled = false;
                dgView.Focus();
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
                cmdDelete.Enabled = false;
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    txtFeeFixed.Enabled = true;
                    cbCCYCD.Enabled = true;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                }
                //Phi theo ty le
                else
                {
                    txtFeeFixed.Enabled = false;
                    cbCCYCD.Enabled = false;
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;
                }                
                txtMsgType.Enabled = true;

                txtFeeFixed.Text = "";
                txtRate.Text = "";
                txtMin.Text = "";
                txtMax.Text = "";
                txtMsgType.Text = "";
                txtMsgType.Focus();
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
                cmdDelete.Enabled = false;
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    txtFeeFixed.Enabled = true;
                    cbCCYCD.Enabled = true;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;                    
                }
                //Phi theo ty le
                else
                {
                    txtFeeFixed.Enabled = false;
                    cbCCYCD.Enabled = false;
                    txtRate.Enabled = true;
                    txtMin.Enabled = true;
                    txtMax.Enabled = true;
                }
                cbFeeType.Enabled = false;
                txtMsgType.Enabled = true;
                dgView.Focus();
                dgView.Enabled = false;
                iID = Convert.ToInt32(dgView.CurrentRow.Cells["ID"].Value.ToString().Trim());
                txtID.Text = iID.ToString();
                txtMsgType.Text = dgView.CurrentRow.Cells["MSG_TYPE"].Value.ToString().Trim();
                txtFeeFixed.Text = dgView.CurrentRow.Cells["FIXED_FEE"].Value.ToString().Trim();
                txtRate.Text = dgView.CurrentRow.Cells["RATE_FEE"].Value.ToString().Trim();
                txtMin.Text = dgView.CurrentRow.Cells["MIN_FEE"].Value.ToString().Trim();
                txtMax.Text = dgView.CurrentRow.Cells["MAX_FEE"].Value.ToString().Trim();
                txtMsgType.Focus();                
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
                        Insert();
                    else
                        Update_Rate();

                    Getdata();
                }
                else
                {
                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMax.Enabled = false;
                    txtMin.Enabled = false;
                    txtMsgType.Enabled = false;
                    dgView.Enabled = true;
                    cbFeeType.Enabled = true;
                  
                    cmdAdd.Enabled = true; 
                    cmdSave.Enabled = false;   
                    if (dgView.Rows.Count == 0)
                    {
                        cmdDelete.Enabled = false;
                        cmdEdit.Enabled = false;
                        cmdPhi.Enabled = false;     
                    }
                    else
                    {
                        cmdDelete.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdPhi.Enabled = true;
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
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    if (cbCCYCD.SelectedValue.ToString() == "ALL")
                    {
                        Common.ShowError("Input currency", 2, MessageBoxButtons.OK);
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtFeeFixed.Text))
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
                if (string.IsNullOrEmpty(txtMsgType.Text))
                {
                    Common.ShowError("Input Msg type", 2, MessageBoxButtons.OK);
                    return false;
                }
                //Phi co dinh
                if (cbFeeType.SelectedValue.ToString() == "1")
                {
                    objFEE.FIXED_FEE = Convert.ToDouble(txtFeeFixed.Text.ToString());
                    if (string.IsNullOrEmpty(txtMax.Text))
                        objFEE.MAX_FEE = 0;
                    else
                        objFEE.MAX_FEE = Convert.ToDouble(txtMax.Text.ToString());
                    if (string.IsNullOrEmpty(txtMin.Text))
                        objFEE.MIN_FEE = 0;
                    else
                        objFEE.MIN_FEE = Convert.ToDouble(txtMin.Text.ToString());
                    if (string.IsNullOrEmpty(txtRate.Text))
                        objFEE.RATE_FEE = 0;
                    else
                        objFEE.RATE_FEE = Convert.ToDouble(txtRate.Text.ToString());
                    objFEE.CCYCD = cbCCYCD.SelectedValue.ToString();
                }
                //Phi ty le
                else
                {
                    if (string.IsNullOrEmpty(txtFeeFixed.Text))
                        objFEE.FIXED_FEE = 0;
                    else
                        objFEE.FIXED_FEE = Convert.ToDouble(txtFeeFixed.Text.ToString());
                    objFEE.MAX_FEE = Convert.ToDouble(txtMax.Text.ToString());
                    objFEE.MIN_FEE = Convert.ToDouble(txtMin.Text.ToString());
                    objFEE.RATE_FEE = Convert.ToDouble(txtRate.Text.ToString());
                    objFEE.CCYCD = "";
                }
                objFEE.MSG_TYPE = txtMsgType.Text.ToString().ToUpper();
                if (string.IsNullOrEmpty(txtID.Text))
                    objFEE.ID = 0;
                else
                    objFEE.ID = Convert.ToInt32(txtID.Text.ToString());

                //Kiem tra loai dien da ton tai
                DataSet dsData = new DataSet();
                dsData = objControlFEE.CheckMsgType(objFEE.MSG_TYPE, objFEE.ID);
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        Common.ShowError("Msg Type is existed", 2, MessageBoxButtons.OK);
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
                if (objControlFEE.ADD_SWIFT_FEE(objFEE) == 1)
                {
                    MessageBox.Show("Insert data successfully", 
                        Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmdAdd.Enabled = false;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdPhi.Enabled = true;
                    cmdDelete.Enabled = true;
                    cbFeeType.Enabled = true;

                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                    txtMsgType.Enabled = false;
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
                if (objControlFEE.UPDATE_SWIFT_FEE(objFEE) == 1)
                {
                    MessageBox.Show("Update data successfully", 
                        Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cmdAdd.Enabled = false;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdPhi.Enabled = true;
                    cmdDelete.Enabled = true;
                    dgView.Enabled = true;
                    cbFeeType.Enabled = true;

                    txtFeeFixed.Enabled = false;
                    txtRate.Enabled = false;
                    txtMin.Enabled = false;
                    txtMax.Enabled = false;
                    txtMsgType.Enabled = false;
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

                dtFEE = objControlFEE.CAL_FEE(datefrom.Value, dateto.Value,
                    cbBranch.SelectedValue.ToString(), 
                    Convert.ToInt16(cbFeeType.SelectedValue.ToString()));
                if (dtFEE != null)
                {
                    frmPrint frmPrint = new frmPrint();
                    string Print = "SWIFT_FEE_CAL";
                    frmPrint.HMdat = dtFEE;
                    frmPrint.PrintType = Print;
                    frmPrint.WindowState = FormWindowState.Maximized;
                    frmPrint.dtfrom = datefrom.Text.ToString();
                    frmPrint.dtto = dateto.Text.ToString();
                    frmPrint.pBranch = cbBranch.SelectedValue.ToString();
                    frmPrint.pFeeType = Convert.ToInt16(cbFeeType.SelectedValue.ToString());
                    frmPrint.pCCYCD = cbCCYCD.SelectedValue.ToString();
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

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmSWIFTFeeCal_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean isDel = false;
                if (Common.iSconfirm == 1)
                {
                    for (int i = 0; i < dgView.Rows.Count; i++)
                    {
                        //if (i > 0)
                        //{
                            if (dgView.Rows[i].Cells[0].Value.ToString().ToUpper() == "TRUE")
                            {
                                iID = Convert.ToInt32(dgView.Rows[i].Cells[1].Value.ToString());
                                sMsgType = dgView.CurrentRow.Cells["MSG_TYPE"].Value.ToString().Trim();
                                sFixed = dgView.CurrentRow.Cells["FIXED_FEE"].Value.ToString().Trim();
                                sRate = dgView.CurrentRow.Cells["RATE_FEE"].Value.ToString().Trim();
                                sMax = dgView.CurrentRow.Cells["MAX_FEE"].Value.ToString().Trim();
                                sMin = dgView.CurrentRow.Cells["MIN_FEE"].Value.ToString().Trim();
                                //sCCYCD = dgView.CurrentRow.Cells["CCYCD"].Value.ToString().Trim();

                                objFEE.ID = iID;
                                if (objControlFEE.DELETE_SWIFT_FEE(iID) == 1)
                                {
                                    isDel = true;
                                    DateTime dtLog = DateTime.Now;
                                    string strUser = BR.BRLib.Common.strUsername;
                                    string useride = BR.BRLib.Common.Userid;
                                    string strConten = "SWIFT FEE CAL";
                                    int Log_level = 1;
                                    string strWorked = "Delete";
                                    string strTable = "SWIFT_FEE";
                                    string strOld_value = sMsgType + "/" + sFixed + "/" +
                                        sRate + "/" + sMax + "/" + sMin + "/" + sCCYCD;
                                    string strNew_value = "";
                                    Ghiloguser(dtLog, strUser, strConten, Log_level,
                                        strWorked, strTable, strOld_value, strNew_value);
                                }
                            }
                        //}
                    }
                    if (dgView.Rows.Count == 0)
                        cmdDelete.Enabled = false;
                    else
                        cmdDelete.Enabled = true;
                    Getdata();
                    if (isDel == true)
                    {
                        MessageBox.Show("Delete successful!", Common.sCaption,
                                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void txtMsgType_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void txtMsgType_KeyPress(object sender, KeyPressEventArgs e)
        {
            //
        }

        private void cbFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Phi co dinh
            if (cbFeeType.SelectedValue.ToString()=="1")
                cbCCYCD.Enabled = true;
            //Phi theo ty le
            else
                cbCCYCD.Enabled = false;                
        } 
    }
}
