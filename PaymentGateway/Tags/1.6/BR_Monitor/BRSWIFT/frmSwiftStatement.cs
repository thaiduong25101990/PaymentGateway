using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM ;
using BR.BRBusinessObject ;
using BR.BRLib;
using System.Text.RegularExpressions;


namespace BR.BRSWIFT
{
    public partial class frmSwiftStatement : BR.BRSYSTEM.frmBasedata
    {
        #region ham va bien
        public int iRows;
        public DataTable dt_Print;  
        private ErrorProvider errorProvider = new ErrorProvider();
        private GetData objGetData = new GetData();
        private SWIFTPRINTController objController = new SWIFTPRINTController();
        private SWIFTPRINTInfo objInfo = new SWIFTPRINTInfo();
        #endregion

        public frmSwiftStatement()
        {
            InitializeComponent();
        }

        private void frmSwiftStatement_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");                
                if (!objGetData.FillDataComboBox(cboRPTType, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SWIFT' and cdname='RPTTYPE'", "CONTENT", true, false, ""))
                    return;
                cboRPTType.Text = "Statement";

                //--------cho ngay thang khong qua ngay he thong----------
                this.pickerFrom.MaxDate = DateTime.Now;
                this.pickerFrom.MaxDate = pickerTo.Value;
                this.pickerTo.MaxDate = DateTime.Now;
                //------------------------------------------
                cmdAdd.Visible = false; cmdDelete.Visible = false;
                cmdEdit.Visible = false; cmdSave.Visible = false;
                lblNumber.Visible = false;

                if (cboRPTType.Text.Trim() == "Statement") //HaNTT10 them ngay 26/09/2008
                {
                    this.Text = "Swift manual statement management";
                    LoaddataGrid(DateTime.Now, DateTime.Now);
                }
                else
                { 
                    this.Text = "Swift inward_RM report";
                    LoadDataSWIFT_IN_RM(DateTime.Now, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            
            dgvSwift.Refresh();  
            string Condition = "";
            if (pickerFrom.Value > pickerTo.Value)
            {
                MessageBox.Show("Invalid value date input", Common.sCaption);
            }
            else if (IsNumberic(txtStatement) == false)
            {
                txtStatement.Clear();  
                txtStatement.Focus();  
            }
            //else if (IsNumberic(txtTellerID) == false)
            //{
            //    txtTellerID.Clear();  
            //    txtTellerID.Focus();  
            //}
            else
            {
                //if (txtStatement.Text != string.Empty && txtTellerID.Text != string.Empty)
                //{
                //    Condition = " and upper(statement_id) like '%" + txtStatement.Text.Trim() + "%' and upper(teller_id) like '%" + txtTellerID.Text.Trim() + "%'";
                //}
                //else if (txtStatement.Text != string.Empty && txtTellerID.Text == string.Empty)
                //{
                //    Condition = " and upper(statement_id) like '%" + txtStatement.Text.Trim() + "%'";
                //}
                //else if (txtStatement.Text == string.Empty && txtTellerID.Text != string.Empty)
                //{
                //    Condition = " and upper(teller_id) like '%" + txtTellerID.Text.Trim() + "%'";
                //}
                //else
                //{
                //    Condition = "";
                //}
                if (cboRPTType.Text.Trim() == "Statement")
                {
                    if (txtStatement.Text != string.Empty && txtTellerID.Text != string.Empty)
                    {
                        Condition = " and statement_id = '" + txtStatement.Text.Trim() + "' and TELLER_NAME = '" + txtTellerID.Text.Trim() + "'";
                    }
                    else if (txtStatement.Text != string.Empty && txtTellerID.Text == string.Empty)
                    {
                        Condition = " and statement_id = '" + txtStatement.Text.Trim() + "'";
                    }
                    else if (txtStatement.Text == string.Empty && txtTellerID.Text != string.Empty)
                    {
                        Condition = " and TELLER_NAME = '" + txtTellerID.Text.Trim() + "'";
                    }
                    else
                    {
                        Condition = "";
                    }
                }
                else
                {
                    if (txtStatement.Text != string.Empty && txtTellerID.Text != string.Empty)
                    {
                        Condition = " and statement_id = '" + txtStatement.Text.Trim() + "' and TELLER_ID = '" + txtTellerID.Text.Trim() + "'";
                    }
                    else if (txtStatement.Text != string.Empty && txtTellerID.Text == string.Empty)
                    {
                        Condition = " and statement_id = '" + txtStatement.Text.Trim() + "'";
                    }
                    else if (txtStatement.Text == string.Empty && txtTellerID.Text != string.Empty)
                    {
                        Condition = " and TELLER_ID = '" + txtTellerID.Text.Trim() + "'";
                    }
                    else
                    {
                        Condition = "";
                    }
                }
            }
            if (cboRPTType.Text.Trim() == "Statement")
            {
                DataSet dt = new DataSet();
                dt = objController.Search(Condition.Trim(), pickerFrom.Value, pickerTo.Value);
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    string strsttt = dt.Tables[0].Rows[0]["statement_time"].ToString();
                    dgvSwift.DataSource = dt.Tables[0];
                    lblNumber.Text = dt.Tables[0].Rows.Count.ToString();
                    cmdReprint.Enabled = true;
                    dgvSwift.Enabled = true;
                    ColumnsRead(dgvSwift);
                    ColumsHeader(dgvSwift);
                }
                else
                {
                    cmdReprint.Enabled = false;
                    MessageBox.Show("There isn't record", Common.sCaption);
                    dgvSwift.DataSource = dt.Tables[0];
                    lblNumber.Text = "0";
                    dgvSwift.Enabled = false;
                    ColumsHeader(dgvSwift);
                }
            }
            else
            {
                DataSet dt = new DataSet();
                dt = objController.SearchSWIFT_IN_RM(Condition.Trim(), pickerFrom.Value, pickerTo.Value);
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    string strsttt = dt.Tables[0].Rows[0]["statement_time"].ToString();
                    dgvSwift.DataSource = dt.Tables[0];
                    lblNumber.Text = dt.Tables[0].Rows.Count.ToString();
                    cmdReprint.Enabled = true;
                    dgvSwift.Enabled = true;
                    ColumnsRead(dgvSwift);
                    ColumsHeader(dgvSwift);
                }
                else
                {
                    cmdReprint.Enabled = false;
                    MessageBox.Show("There is no record!", Common.sCaption);
                    dgvSwift.DataSource = dt.Tables[0];
                    lblNumber.Text = "0";
                    dgvSwift.Enabled = false;
                    ColumsHeader(dgvSwift);
                }
            }

        }
        public void LoaddataGrid(DateTime fromdate,DateTime todate)
        {
            DataSet datagrid = new DataSet();
            datagrid = objController.LoaddataGrid(fromdate, todate);
            if (datagrid != null && datagrid.Tables[0].Rows.Count>0)
            {
                dgvSwift.DataSource = datagrid.Tables[0];
                lblNumber.Text = datagrid.Tables[0].Rows.Count.ToString();
                dgvSwift.Enabled = true;
                cmdReprint.Enabled = true; 
                ColumnsRead(dgvSwift);
                ColumsHeader(dgvSwift);
            }
            else
            {
                cmdReprint.Enabled = false;
                lblNumber.Text = "0";
                dgvSwift.DataSource = datagrid.Tables[0];
                ColumsHeader(dgvSwift);
                dgvSwift.Enabled = false;
            }
        }

        public void LoadDataSWIFT_IN_RM(DateTime fromdate, DateTime todate)
        {
            DataSet datagrid = new DataSet();
            datagrid = objController.LoadSWIFT_IN_RM(fromdate, todate);
            if (datagrid != null && datagrid.Tables[0].Rows.Count > 0)
            {
                dgvSwift.DataSource = datagrid.Tables[0];
                lblNumber.Text = datagrid.Tables[0].Rows.Count.ToString();
                dgvSwift.Enabled = true;
                cmdReprint.Enabled = true;
                ColumnsRead(dgvSwift);
                ColumsHeader(dgvSwift);
            }
            else
            {
                cmdReprint.Enabled = false;
                lblNumber.Text = "0";
                dgvSwift.DataSource = datagrid.Tables[0];
                ColumsHeader(dgvSwift);
                dgvSwift.Enabled = false;
            }
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

        private void ColumsHeader(DataGridView Datagrid)
        {
            try
            {

                int g = 0;
                while (g < Datagrid.Columns.Count)
                {
                    string strColumns = Datagrid.Columns[g].HeaderText.ToString();
                    if (strColumns.Trim() != "BRANCH_A" && strColumns.Trim() != "BRANCH_B")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    }
                    if (strColumns.Trim() == "STATEMENT_ID")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "STATEMENT NO";
                    }
                    if (strColumns.Trim() == "TELLER_NAME")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "TELLER ID";
                    }     
                    Datagrid.Columns[g].Width = 240;                      
                      
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgvSwift_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iRows = e.RowIndex;
        }

        private void cmdReprint_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSwift.Select();
                frmPrint frm = new frmPrint();
                //frm.strStatementID_reprint = Convert.ToInt32(dgvSwift.CurrentRow.Cells["STATEMENT_ID"].Value.ToString().Trim());//HaNTT10 sua
                int StatementID_reprint = Convert.ToInt32(dgvSwift.CurrentRow.Cells["STATEMENT_ID"].Value.ToString().Trim());
                string strDate = dgvSwift.CurrentRow.Cells["STATEMENT_TIME"].Value.ToString().Trim();
                string date = Convert.ToString(strDate.Substring(0, 10));
                String[] M = date.Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
                string M0;
                string M1;
                if (M[0].Trim().Length == 1)
                { M0 = "0" + M[0]; }
                else
                { M0 = M[0]; }
                if (M[1].Trim().Length == 1)
                { M1 = "0" + M[1]; }
                else
                { M1 = M[1]; }
                string dat = M0 + "/" + M1 + "/" + M[2];
                //frm.StatementDate_reprint = dat;
                if (cboRPTType.Text.Trim() == "Statement") //HaNTT10 them ngay 26/09/2008
                {
                    frm.PrintType = "SWIFT_03_R";
                    dt_Print=objController.GetData_Reprint(StatementID_reprint, dat);
                    frm.HMdat = dt_Print;
                }
                else
                {
                    frm.PrintType = "SWIFT_RM_R";
                    dt_Print = objController.GetData_Reprint_RM(StatementID_reprint, dat);
                    frm.HMdat = dt_Print;
                }
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        public bool IsNumberic(Control Ctrl)
        {
            //string d = txtusername.Text;
            bool result = false;
            string input = Ctrl.Text;
            result = Regex.IsMatch(input, @"^[0-9]*\z");

            if (result == false)
            {
                Common.ShowError("Data input must be number!", 3, MessageBoxButtons.OK);              
            }
            return result;
        }


        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCD_FormClosing(null,null);
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (pickerFrom.Focused)
                {
                    txtStatement.Focus();
                    txtStatement.SelectAll();
                }
                else if (txtStatement.Focused)
                {
                    pickerTo.Focus();
                }
                else if (pickerTo.Focused)
                {
                    txtTellerID.Focus();
                    txtTellerID.SelectAll();
                }

                else if (txtTellerID.Focused)
                {
                    cmdSearch.Focus();
                    cmdSearch_Click(null, null);
                }

                //strSucess = true;
            }
        }

        private void pickerFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (pickerFrom.Checked == false)
                {
                    pickerTo.Checked = false;
                }
                else
                {
                    pickerTo.Checked = true;
                }
                if (pickerFrom.Value > pickerTo.Value)
                {
                    pickerFrom.Value = pickerTo.Value;
                }               
            }
            catch
            {
            }
        }

        private void pickerTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (pickerTo.Checked == false)
                {
                    pickerFrom.Checked = false;
                }
                else
                {
                    pickerFrom.Checked = true;
                }
                if (pickerTo.Value < pickerFrom.Value)
                {
                    this.pickerTo.Value = pickerFrom.Value;
                }
            }
            catch
            {
            }
        }

        private void frmSwiftStatement_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftStatement_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cboRPTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRPTType.Text.Trim() == "Statement")
            {
                this.Text = "Swift manual statement management";
                LoaddataGrid(DateTime.Now, DateTime.Now);
            }
            else
            { //LoadDataSWIFT_IN_RM
                this.Text = "Swift inward_RM report";
                LoadDataSWIFT_IN_RM(DateTime.Now, DateTime.Now);
            }
        }

        
    }
}
