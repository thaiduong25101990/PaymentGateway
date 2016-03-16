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
   
    public partial class frmVCBNostroAcc : frmBasedata
    {
        
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private BIDV_ACCOUNT_Info objInforAccount = new BIDV_ACCOUNT_Info();
        private BIDV_ACCOUNTController objcontrolAccount = new BIDV_ACCOUNTController();
        
        private int iRows;
        private string strStatus = "VIEW";
        private string strcbCurrencyOld = "";
        private string strAcountOld = "";
        private string strCCD;

        public frmVCBNostroAcc()
        {
            InitializeComponent();
        }
        
        #region //Control Events

        private void frmVCBNostroAcc_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            //Load data cbBranch
            if (!objGetData.FillDataComboBox(cbBranch, "BRAN_NAME", "SIBS_BANK_CODE", 
                "BRANCH","", "BRAN_NAME", true, false, ""))
                return;
            //Load data cbCurrency
            if (!objGetData.FillDataComboBox(cbCurrency, "ccycd", "ccycd",
                "Currency", "gwtype='VCB'", "ccycd", true, true, "ALL"))
                return;
            Refresh_Acc();
            string strWHERE = "";
            LoadDatagrid(strWHERE);
            CommandStatus(true);
            this.Text = "H.O’s Nostro Account at VCB";
        }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSearch.Enabled = a;
            cmdSave.Enabled = !a;
            cmdReject.Enabled = !a;
            dgAcct.Enabled = a;
        }

        private void frmVCBNostroAcc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cmdSave.Enabled)
                e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit", Common.sCaption);

        }
        
        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            mskAcctNo.Text = "";

            if (cbBranch.Items.Count==0 || cbBranch.SelectedItem.ToString() == "ALL")
            {
                lblBrName.Text  = "";
                return;
            }            
            lblBrName.Text = cbBranch.SelectedValue.ToString();
        }

        private void dgAcct_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            { 
                //LoadDatagrid();
                string strWHERE="";
                string strCCW; string strACOU;
                if (cbCurrency.Text.Trim() == "ALL")                
                    strCCW = "";
                else                
                    strCCW = "  and  Trim(CCYCD) = '" + cbCurrency.Text.Trim() + "'";                
                if (mskAcctNo.Text.Trim() == "")                
                    strACOU = "";                
                else                
                    strACOU = " and Trim(ACCOUNT) like '%" + mskAcctNo.Text.Trim() + "%'";                
                if (cbCurrency.Text.Trim() == "ALL" && mskAcctNo.Text.Trim() == "")                
                    strWHERE = "";                
                else 
                {
                    string strWhere1 = strACOU + strCCW;
                    strWHERE = "  Where  " + strWhere1.Substring(5);
                }
                LoadDatagrid(strWHERE);
            }
            catch(Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            strStatus = "ADD";
            txtID.Text  = "";            
            cmdReject.Enabled = true;
            cbCurrency.SelectedIndex = 0;
            cbBranch.SelectedIndex = 0;
            mskAcctNo.Text = "";
            cmdSearch.Enabled = false;
            cbCurrency.Focus();
            CommandStatus(false);
        }
      
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                strStatus = "EDIT";                
                cbCurrency.Text = dgAcct.Rows[iRows].Cells["CCYCD"].Value.ToString();
                mskAcctNo.Text = dgAcct.Rows[iRows].Cells["Account"].Value.ToString();

                strcbCurrencyOld = cbCurrency.Text;
                strAcountOld = mskAcctNo.Text;

                strCCD = cbCurrency.Text.Trim();
                cmdReject.Enabled = true;
                cmdSearch.Enabled = false;
                dgAcct.Enabled = false;
                cbCurrency.Focus();
                CommandStatus(false);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message,2,MessageBoxButtons.OK);
            }

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 0)
                {
                    cmdSave.Enabled = false;
                    CommandStatus(true);
                    return;
                }                    

                strStatus = "DELETE";
                objInforAccount.ID = Convert.ToInt32(dgAcct.Rows[iRows].Cells[0].Value.ToString());
                strcbCurrencyOld = dgAcct.Rows[iRows].Cells["CCYCD"].Value.ToString();
                strAcountOld = dgAcct.Rows[iRows].Cells["Account"].Value.ToString();               
                if (objcontrolAccount.Delete_BIDV(objInforAccount) == 1)
                {
                    Common.ShowError("Account has been deleted!", 1, MessageBoxButtons.OK);                    
                    CommandStatus(true);
                    Refresh_Acc();
                    //lay du lieu de ghi log
                    DateTime dtDateLogin = DateTime.Now;
                    string strContent = "VCB Nostro Account";
                    int iLoglevel = 1;
                    string strWorked = "Delete";
                    string strTable = "BIDV_ACCOUNT";
                    string strOld_value = strcbCurrencyOld + "/" + strAcountOld;
                    string strNew_value = "";
                    //goi ham ghilog
                    objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent,
                        iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                }
                else
                {
                    Common.ShowError("Delete Account error!", 2, MessageBoxButtons.OK);                    
                    cmdSave.Enabled = false;
                    CommandStatus(false);
                    return;
                }
                
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }            
        }

        private void  cmdSave_Click(object sender, EventArgs e)
        {
            try 
            {   
                if (Common.iSconfirm == 0)
                {
                    RefreshSave(); CommandStatus(true); return;
                }
                if (strStatus != "ADD")
                {
                    objInforAccount.ID = Convert.ToInt32(dgAcct.Rows[iRows].Cells[0].Value.ToString());
                }               
                mskAcctNo.Text = mskAcctNo.Text.Trim();
                //objInforAccount.ID = Convert.ToInt32(dgAcct.Rows[iRows].Cells[0].Value.ToString());
                objInforAccount.ACCOUNT = mskAcctNo.Text;
                objInforAccount.BRANCH = cbBranch.SelectedValue.ToString();
                objInforAccount.CCYCD = cbCurrency.SelectedValue.ToString();
       
                if (!Verify())
                {
                    RefreshSave();
                    return;
                }
                
                if (!Validate_One())
                {
                    RefreshSave();
                    return;
                }
                //kiem tra checkk trung ma CCYCD trong bang BIDV_ACOUNT
                DataTable datCCYCD = new DataTable();
                datCCYCD = objcontrolAccount.GET_BIDV("CCYCD", cbCurrency.Text.Trim());
                if (datCCYCD.Rows.Count != 0)
                {                
                    if (strStatus == "EDIT")//truong hop Edit du lieu thi kiem tra xem co chon CCYCD khac khong
                    {
                        if (cbCurrency.Text.Trim() != strCCD)//van la CCYCD ban dau
                        {                        
                            Common.ShowError("Currency has existed!", 4, MessageBoxButtons.OK);                            
                            RefreshSave();
                            CommandStatus(false);
                            return;
                        }
                    }
                    if (strStatus == "ADD")
                    {
                        Common.ShowError("Currency has existed!", 4, MessageBoxButtons.OK);                        
                        RefreshSave();
                        CommandStatus(false);
                        return;
                    }
                }

                //kiem tra checkk trung ma CCYCD trong bang BIDV_ACOUNT
                DataTable dtAccount = new DataTable();
                dtAccount = objcontrolAccount.GET_BIDV("ACCOUNT", mskAcctNo.Text.Trim());
                if (dtAccount.Rows.Count == 0)
                {
                    //khong sao ca
                }
                else
                {
                    if (strStatus == "EDIT")//truong hop Edit du lieu thi kiem tra xem co chon CCYCD khac khong
                    {
                        if (mskAcctNo.Text.Trim() == dgAcct.CurrentRow.Cells["ACCOUNT"].Value.ToString())//van la CCYCD ban dau
                        {
                        }
                        else
                        {
                            Common.ShowError("Account has existed!", 4, MessageBoxButtons.OK);                            
                            RefreshSave();
                            CommandStatus(false);
                            return;
                        }
                    }
                    if (strStatus == "ADD")
                    {
                        Common.ShowError("Account has existed!", 4, MessageBoxButtons.OK);                        
                        RefreshSave();
                        CommandStatus(false);
                        return;
                    }
                }
               
                if (strStatus == "ADD")
                {
                    if (objcontrolAccount.Insert_BIDV(objInforAccount) == 1)
                    {
                        Common.ShowError("Account has been inserted!", 1, MessageBoxButtons.OK);
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "VCB Nostro Account";
                        int iLoglevel = 1;
                        string strWorked = "Insert";
                        string strTable = "BIDV_ACCOUNT";
                        string strOld_value = "";
                        string strNew_value = cbCurrency.Text.Trim() + "/" + mskAcctNo.Text.Trim();
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, 
                            iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                    }
                    else
                    {
                        Common.ShowError("Add account error!", 2, MessageBoxButtons.OK);                        
                        CommandStatus(false);
                        return;
                    }
                }

                else if (strStatus == "EDIT")
                {                    
                    if (objcontrolAccount.Update_BIDV(objInforAccount) == 1)
                    {
                        Common.ShowError("Account has been updated!", 1, MessageBoxButtons.OK);                        
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "VCB Nostro Account";
                        int iLoglevel = 1;
                        string strWorked = "Update";
                        string strTable = "BIDV_ACCOUNT";
                        string strOld_value = strcbCurrencyOld + "/" + strAcountOld;
                        string strNew_value = cbCurrency.Text.Trim() + "/" + 
                            mskAcctNo.Text.Trim();
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid , strContent, 
                            iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                    }
                    else
                    {
                        Common.ShowError("Update Account error!", 2, MessageBoxButtons.OK);                        
                        CommandStatus(true);
                        return;
                    }
                }

                Refresh_Acc();
                CommandStatus(true);
            }
            catch(Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);                
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdReject_Click(object sender, EventArgs e)
        {
            try
            {
                cbCurrency.SelectedIndex=0;                
                mskAcctNo.Text = "";
                cmdAdd.Enabled = true;
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdSave.Enabled = false;              
                cmdReject.Enabled = false;
                cmdSearch.Enabled = true;
                dgAcct.Enabled = true;
                dgAcct_SelectionChanged(null, null);
                CommandStatus(true);
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void mskAcctNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);                
            }
        }
   
        #endregion 

        # region //Functions
        private void LoadDatagrid(string strWHERE)
        {
            DataTable datAcct = new DataTable();
            try
            {                
                datAcct = objcontrolAccount.Get_BIDV(strWHERE);                
                dgAcct.DataSource = datAcct;
                dgAcct.Columns["Branch"].Visible = false;
                dgAcct.Columns["ID"].Visible = false;
                dgAcct.Columns["CCYCD"].Width = 150;
                dgAcct.Columns["Account"].Width = 313;
                cmdReject.Enabled = false;
                if (dgAcct.Rows.Count > 0)
                {                   
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdSave.Enabled = false;

                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            finally
            {
                datAcct.Dispose();
            }

        }

        private void Refresh_Acc()
        {
            txtID.Text = "";
            cbCurrency.SelectedIndex = 0;
            cbBranch.SelectedIndex = 0;
            mskAcctNo.Text = "";

            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdReject.Enabled = false;

            btnSearch_Click(null, null);
            cbCurrency.Focus();
        }

        private void RefreshSave()
        {
            cmdAdd.Enabled = false;
            cmdEdit.Enabled = false;
            cmdDelete.Enabled = false;
            cmdSave.Enabled = true;
            cmdReject.Enabled = true;
            cbCurrency.Focus();
        }

        private bool Verify()
        {            
            if (cbCurrency.Text.Trim() == "ALL")
            {
                Common.ShowError("Currency Code is invalid!", 4, MessageBoxButtons.OK); 
                cbCurrency.Focus();
                CommandStatus(false);
                return false;
            }

            mskAcctNo.Text = mskAcctNo.Text.Trim();
            if (mskAcctNo.Text == "")
            {
                Common.ShowError("Account number is empty!", 4, MessageBoxButtons.OK);                 
                mskAcctNo.Focus();
                CommandStatus(false);
                return false;
            }           
            return true;
        }

        private bool Validate_One()
        {
            DataSet datAcct = new DataSet();

            datAcct = objcontrolAccount.SelectAccount(objInforAccount);
            if (datAcct.Tables[0].Rows.Count > 0)
            {
                if ((strStatus == "EDIT") &&
                    (datAcct.Tables[0].Rows[0]["ID"].ToString() == Convert.ToString(objInforAccount.ID)))
                {
                    return true;
                }
                Common.ShowError("Account has existed!", 4, MessageBoxButtons.OK);
                datAcct.Dispose();
                CommandStatus(false);
                return false;
            }
            return true;
        }

        /*---------------------------------------------------------------
        * Method           : KeyDownPress(object sender, KeyEventArgs e)
        * Muc dich         : Bắt sự kiện KeyDown trên form
        * Tham so          :  
        * Tra ve           : 
        * Ngay tao         : 06/08/2008
        * Nguoi tao        : HueMT
        * Ngay cap nhat    : 
        * Nguoi cap nhat   : 
        *--------------------------------------------------------------*/
        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    return;
                }
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
        # endregion

        private void dgAcct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgAcct_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void mskAcctNo_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mskAcctNo.Text.Trim()))
            {                
                return;
            }
            else
            {
                if (mskAcctNo.Text.Length > 15)
                {
                    Common.ShowError("Invalid account length!", 4, MessageBoxButtons.OK);
                    CommandStatus(false);
                    return;
                }
            }
        }

        private void frmVCBNostroAcc_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }    
}
