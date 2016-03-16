using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRSYSTEM;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class frmCCYCD : frmBasedata
    {
        #region khai bao ham va bien
        public static bool isInsert = false;
        public int d = 0;
        private clsLog objLog = new clsLog(); 
        private DataSet datDs = new DataSet();
        private CURRENCYInfo objInfo = new CURRENCYInfo();
        private CURRENCYController objControl = new CURRENCYController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private CURRENCY_CHANNELController objCurrencyChannel = new CURRENCY_CHANNELController();
        private clsCheckInput clsCheck = new clsCheckInput();
        private int iID = 0;
        private int iRows;
        private bool NeedConfirm = true;
        private static bool strSucess = false;                
        private string CurrencyCode1 = "";
        private string MonCode1 = "";
        private string CcyName1 = "";
        private string Decimal1 = "";
        #endregion

        public frmCCYCD()
        {
            InitializeComponent();            
        }


        private void frmCCYCD_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                //LoadData();
                cmdSearch_Click(sender, e);
                CommandStatus(true);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
       

        //private void LoadData()
        //{
        //    DataSet datDs = new DataSet();
        //    datDs = objControl.GetCurrency();
        //    if (datDs != null) { dgdCurrency.DataSource = datDs.Tables[0]; }
        //    ////////////////////////////////////////////////////-/
        //    //Muc dich: Format datagridview
        //    //          Kiem tra rowcount>0
        //    //Ngay sua: 02/08/2008
        //    FormatDataGridView();
        //    if (dgdCurrency.RowCount > 0)
        //    { cmdDelete.Enabled = true; cmdEdit.Enabled = true; }
        //    else
        //    { cmdDelete.Enabled = false; cmdEdit.Enabled = false; }
        //}

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strCurrencyCode; string strMonCode; string strDecimal; string strCcyName;
                if (txtCurrencyCode.Text == "")
                    strCurrencyCode = "";
                else
                    strCurrencyCode = " and  upper(trim(ccy.SHORTCD)) like '%" +
                        clsCheck.ConvertVietnamese(txtCurrencyCode.Text.Trim().ToUpper()) + "%'";
                if (txtMonCode.Text == "ALL")
                    strMonCode = "";
                //////////////////////////////////////////////////////-/
                //Bat ky tu "'" thay bang "''"
                //Ngay sua: 02/08/2008
                //else { strMonCode = "  and trim(ccy.CCYCD) like '%" + txtMonCode.Text.Trim() + "%'"; }
                else 
                    strMonCode = "  and upper(trim(ccy.CCYCD)) like '%" + 
                    clsCheck.ConvertVietnamese(txtMonCode.Text.Trim().Replace("'", "''").ToUpper()) + "%'";
                ////////////////////////////////////////////////////////
                if (txtDecimal.Text == "ALL") 
                    strDecimal = "";
                else 
                    strDecimal = " and  upper(trim(ccy.DECIMALS)) like '%" + 
                        txtDecimal.Text.Trim().ToUpper() + "%'";
                if (txtCcyName.Text == "ALL") 
                    strCcyName = "";
                //////////////////////////////////////////////////////-/
                //Bat ky tu "'" thay bang "''"
                //Ngay sua: 02/08/2008
                //else { strCcyName = " and  trim(ccy.CURRENCY) like '%" + txtCcyName.Text.Trim() + "%'"; }                
                else 
                    strCcyName = " and  upper(trim(ccy.CURRENCY)) like '%" + 
                    clsCheck.ConvertVietnamese(txtCcyName.Text.Trim().Replace("'", "''").ToUpper()) + "%'";
                ////////////////////////////////////////////////////////

                string strWHERE = strCurrencyCode + strMonCode + strDecimal + strCcyName;
                DataSet datSearch = new DataSet();
                datSearch = objControl.GetCurrencySearch(strWHERE);

                ////////////////////////////////////////////////-/
                //Muc dich: Format datagridview
                //          Kiem tra dataset<>null
                //Ngay sua: 02/08/2008                    
                if (datSearch == null)
                { dgdCurrency.DataSource = 0; return; }
                dgdCurrency.DataSource = datSearch.Tables[0];
                FormatDataGridView();                
                if (dgdCurrency.RowCount > 0)
                { cmdEdit.Enabled = true; cmdDelete.Enabled = true; }
                else
                { cmdEdit.Enabled = false; cmdDelete.Enabled = false; }
                //////////////////////////////////////////////////

                if (datSearch.Tables[0].Rows.Count == 0)
                {                    
                    Common.ShowError("There are no suitable currencies to display", 
                        1, MessageBoxButtons.OK);
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

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////-/
            //Muc dich: Kiem tra rowcount>0 => 
            //          sua ban ghi hien tai                
            //Ngay sua: 02/08/2008                
            if (dgdCurrency.RowCount <= 0)
            {
                cmdAdd.Enabled = true; cmdDelete.Enabled = true;
                cmdSave.Enabled = false; cmdCancel.Enabled = false;
                cmdEdit.Enabled = true; return;
            }           
            try
            {
                if (iRows == -1)
                {
                    txtCurrencyCode.Text = dgdCurrency.Rows[0].Cells["SHORTCD"].Value.ToString();
                    txtMonCode.Text = dgdCurrency.Rows[0].Cells["CCYCD"].Value.ToString();
                    txtCcyName.Text = dgdCurrency.Rows[0].Cells["CURRENCY"].Value.ToString();
                    txtDecimal.Text = dgdCurrency.Rows[0].Cells["DECIMALS"].Value.ToString();
                }
                else
                {
                    txtCurrencyCode.Text = dgdCurrency.Rows[iRows].Cells["SHORTCD"].Value.ToString();
                    txtMonCode.Text = dgdCurrency.Rows[iRows].Cells["CCYCD"].Value.ToString();
                    txtCcyName.Text = dgdCurrency.Rows[iRows].Cells["CURRENCY"].Value.ToString();
                    txtDecimal.Text = dgdCurrency.Rows[iRows].Cells["DECIMALS"].Value.ToString();
                }                
                CurrencyCode1 = txtCurrencyCode.Text;
                MonCode1 = txtMonCode.Text;
                CcyName1 = txtCcyName.Text;
                Decimal1 = txtDecimal.Text;
                
                isInsert = false;
                CommandStatus(false);               
                LockTextBox(false);
                txtCurrencyCode.ReadOnly = true;
                txtMonCode.ReadOnly = true;               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////-/
            //Muc dich: Kiem tra rowcount>0 => lay iID
            //          Kiem tra co dong y xoa ko?
            //          Kiem tra currentrow>0
            //Ngay sua: 02/08/2008
            if (dgdCurrency.RowCount <= 0)
            {
                cmdSave.Enabled = false; cmdCancel.Enabled = false;
                return;
            }
            if (Common.iSconfirm != 1)
            {
                cmdSave.Enabled = false; cmdCancel.Enabled = false;
                return;
            }
            if (dgdCurrency.CurrentRow == null)
            {
                cmdSave.Enabled = false; cmdCancel.Enabled = false;
                return;
            }
            //////////////////////////////////////////-/
            if (iRows == -1)
            {
                iID = Convert.ToInt32(dgdCurrency.Rows[0].Cells["ID"].Value.ToString());
                CurrencyCode1 = dgdCurrency.Rows[0].Cells["SHORTCD"].Value.ToString();
                MonCode1 = dgdCurrency.Rows[0].Cells["CCYCD"].Value.ToString();
                CcyName1 = dgdCurrency.Rows[0].Cells["CURRENCY"].Value.ToString();
                Decimal1 = dgdCurrency.Rows[0].Cells["DECIMALS"].Value.ToString();
            }
            else
            {
                iID = Convert.ToInt32(dgdCurrency.Rows[iRows].Cells["ID"].Value.ToString());
                CurrencyCode1 = dgdCurrency.Rows[iRows].Cells["SHORTCD"].Value.ToString();
                MonCode1 = dgdCurrency.Rows[iRows].Cells["CCYCD"].Value.ToString();
                CcyName1 = dgdCurrency.Rows[iRows].Cells["CURRENCY"].Value.ToString();
                Decimal1 = dgdCurrency.Rows[iRows].Cells["DECIMALS"].Value.ToString();
            }

            try
            {               
                DataSet dsCURRENCY_CHANNEL = new DataSet();
                dsCURRENCY_CHANNEL = objCurrencyChannel.GetCurrency_code(dgdCurrency.CurrentRow.Cells["CCYCD"].Value.ToString().Trim());
                if (dsCURRENCY_CHANNEL.Tables[0].Rows.Count == 0)
                {
                    objControl.DeleteCURRENCY(iID);
                    Common.ShowError("Delete successfully!", 1, MessageBoxButtons.OK);
                    #region lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Currency";
                    int Log_level = 1;
                    string strWorked = "Delete";
                    string strTable =  "CURRENCYCODE";
                    string strOld_value = CurrencyCode1 + "/" + MonCode1 + "/" + 
                        CcyName1 + "/" + Decimal1;
                    string strNew_value = "";
                    //-----------------------------------------
                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, strWorked, 
                        strTable, strOld_value, strNew_value);
                    #endregion
                }
                else
                {                    
                    Common.ShowError("This currency is in used!", 4, MessageBoxButtons.OK);
                    CommandStatus(true);
                    return;
                }              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }          
            CommandStatus(true);
            cmdSearch_Click(sender, e);
            //            LoadData();           
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isInsert = true;
            ClearText();
            CommandStatus(false);           
            txtCurrencyCode.ReadOnly = false;
            txtMonCode.ReadOnly = false;
            txtCurrencyCode.Focus();           
        }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a; cmdEdit.Enabled = a;
            cmdDelete.Enabled = a; cmdSave.Enabled = !a;
            cmdCancel.Enabled = !a; cmdSearch.Enabled = a;
            dgdCurrency.Enabled = a;
        }

        private void ClearText()
        {
            txtCcyName.Text = ""; txtCurrencyCode.Text = "";
            txtDecimal.Text = ""; txtMonCode.Text = "";
        }

        private void dgdCurrency_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
            }
        }

        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////-/
            //Muc dich: Kiem tra co dong y save hay ko
            //Ngay sua: 02/08/2008
            if (Common.iSconfirm != 1)
            {
                if (txtMonCode.ReadOnly == true)                
                    txtMonCode.ReadOnly = false;                
                if (txtCurrencyCode.ReadOnly == true)                
                    txtCurrencyCode.ReadOnly = false;                
                CommandStatus(false);
                return;
            }
            ///////////////////////////////////////////////
            if (!Common.IsNumeric(txtDecimal.Text.Trim()))
            {
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);
                txtDecimal.Focus(); CommandStatus(false);
                return;
            }
            if (String.IsNullOrEmpty(txtCurrencyCode.Text.Trim()))
            {
                Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                CommandStatus(false); txtCurrencyCode.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtMonCode.Text.Trim()))
            {
                Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                CommandStatus(false); txtMonCode.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtCcyName.Text.Trim()))
            {
                Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                CommandStatus(false);
                txtCcyName.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtDecimal.Text.Trim()))
            {
                Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                CommandStatus(false);
                txtDecimal.Focus();
                return;
            }
            else
            {
                objInfo.SHORTCD = clsCheck.ConvertVietnamese(txtCurrencyCode.Text.ToString().Trim().ToUpper());
                objInfo.CCYCD = clsCheck.ConvertVietnamese(txtMonCode.Text.ToString().Trim().ToUpper());
                objInfo.DECIMALS = int.Parse(txtDecimal.Text.ToString().Trim());
                objInfo.CURRENCY = clsCheck.ConvertVietnamese(txtCcyName.Text.ToString().Trim().ToUpper());
                if (isInsert == true)
                {
                    if (GetData.IDCCYIsExisting(false, txtCurrencyCode.Text.Trim().ToUpper(),
                        txtMonCode.Text.Trim().ToUpper()))
                    {
                        Common.ShowError("Currency has already existed!", 1, MessageBoxButtons.OK);
                        CommandStatus(false);
                        txtCurrencyCode.Focus();
                        return;
                    }
                    else if (!CheckID())
                    {
                        CommandStatus(false);
                        return;
                    }
                    objControl.AddCURRENCY(objInfo);
                    Common.ShowError("Data has inserted successfully!", 1, MessageBoxButtons.OK);
                    #region lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Currency";
                    int Log_level = 1;
                    string strWorked = "Insert";
                    string strTable = "CURRENCYCODE";
                    string strOld_value = "";
                    string strNew_value = iID.ToString() + "/" + objInfo.SHORTCD + "/" +
                        objInfo.CCYCD + "/" + objInfo.DECIMALS + "/" + objInfo.CURRENCY;
                    //-----------------------------------------
                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, strWorked,
                        strTable, strOld_value, strNew_value);
                    #endregion
                }
                else if (isInsert == false)
                {
                    txtCurrencyCode.ReadOnly = false;
                    txtMonCode.ReadOnly = false;
                    if (!CheckID())
                    {
                        CommandStatus(false);
                        return;
                    }
                    objControl.UpdateCURRENCY(objInfo, txtCurrencyCode.Text.Trim(),
                        txtMonCode.Text.Trim());
                    Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);                    

                    #region lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Currency";
                    int Log_level = 1;
                    string strWorked = "Update";
                    string strTable = "CURRENCYCODE";
                    string strOld_value = CurrencyCode1 + "/" + MonCode1 + "/" +
                        CcyName1 + "/" + Decimal1;
                    string strNew_value = iID.ToString() + "/" + objInfo.SHORTCD + "/" +
                        objInfo.CCYCD + "/" + objInfo.CURRENCY + "/" + objInfo.DECIMALS;
                    //-----------------------------------------
                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, strWorked,
                        strTable, strOld_value, strNew_value);
                    #endregion
                }
            }
            #region Enable controls
            CommandStatus(true);
            txtCurrencyCode.ReadOnly = false;
            txtMonCode.ReadOnly = false;
            txtCurrencyCode.ReadOnly = false;
            txtMonCode.ReadOnly = false;
            #endregion
            txtCurrencyCode.Focus();
            ClearText();
            cmdSearch_Click(sender, e); 
            //LoadData();
        }


        private bool CheckID()
        {
            bool result = true;
            string ID = txtMonCode.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "You must input Monetary code!";
                result = false;
            }

            if (txtCurrencyCode.Text.Length != 2)
            {
                Common.ShowError("Invalid currency code!", 3, MessageBoxButtons.OK);                
                txtCurrencyCode.Focus();
                result = false;
            }
            if (txtMonCode.Text.Length != 3)
            {
                Common.ShowError("Invalid currency code!", 3, MessageBoxButtons.OK);                
                txtMonCode.Focus();
                result = false;
            }
            return result;
        }

        private void LockTextBox(Boolean a)
        {
            txtCurrencyCode.ReadOnly = a;
            txtDecimal.ReadOnly = a;
            txtMonCode.ReadOnly = a;
            txtCcyName.ReadOnly = a;
        }

        private void dgdCurrency_CellEnter(object sender, DataGridViewCellEventArgs e)
        {          
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ///////////////////////////////////////////////////-/
        //Muc dich: Format Datagridview
        //Ngay tao:02/08/2008
        private void FormatDataGridView()
        {
            try
            {
                dgdCurrency.RowHeadersVisible = true;
                dgdCurrency.Columns[0].Visible = false;
                dgdCurrency.Columns["SHORTCD"].HeaderText = "Currency number";
                dgdCurrency.Columns["SHORTCD"].Width = 80;
                dgdCurrency.Columns["CCYCD"].HeaderText = "Currency code";
                dgdCurrency.Columns["CCYCD"].Width = 80;
                dgdCurrency.Columns["CURRENCY"].HeaderText = "Currency Name";
                dgdCurrency.Columns["CURRENCY"].Width = 422;
                dgdCurrency.Columns["DECIMALS"].HeaderText = "Decimals";
                dgdCurrency.Columns["DECIMALS"].Width = 80;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Muc dich: Ham kiem tra nhap so
        private void txtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }

        //Muc dich: Ham kiem tra nhap so
        private void txtCurrencyCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCurrencyCode.ReadOnly == false)
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);                    
                }
            }
            else            
                return;            
        }

        ///////////////////////////////////////////////////-/
        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)            
                this.Close();            
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (txtCurrencyCode.Focused)
                { txtMonCode.Focus(); txtMonCode.SelectAll(); }
                else if (txtMonCode.Focused)
                { txtDecimal.Focus(); txtDecimal.SelectAll(); }
                else if (txtDecimal.Focused)
                { txtCcyName.Focus(); txtCcyName.SelectAll(); }
                else if (txtCcyName.Focused)
                {
                    if (cmdSearch.Enabled == false)
                    { cmdSave.Focus(); }
                    else
                    { cmdSearch.Focus(); cmdSearch_Click(null, null); }
                }
                strSucess = true;
            }
        }

        private void frmCCYCD_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true & cmdSave.Enabled == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", 
                            Common.sCaption);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Common.ClearControl(this);
            dgdCurrency.Enabled = true;
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = true;
            cmdCancel.Enabled = false;
            cmdSearch.Enabled = true;
            txtCurrencyCode.Focus();
            txtCurrencyCode.ReadOnly = false;
            txtMonCode.ReadOnly = false;
        }      

        private void txtMonCode_Leave(object sender, EventArgs e)
        {
            txtMonCode.Text = clsCheck.ConvertVietnamese(txtMonCode.Text.Trim().ToUpper());
        }

        private void txtCcyName_Leave(object sender, EventArgs e)
        {
            txtCcyName.Text = clsCheck.ConvertVietnamese(txtCcyName.Text.Trim().ToUpper());
        }

        private void frmCCYCD_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmCCYCD_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dgdCurrency_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    
    }
}
