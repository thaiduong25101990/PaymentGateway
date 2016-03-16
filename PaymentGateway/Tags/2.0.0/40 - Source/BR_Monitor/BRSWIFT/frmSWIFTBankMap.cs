using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using BR.BRLib;
using BR.BRBusinessObject;
using System.Text.RegularExpressions;

namespace BR.BRSWIFT
{
    public partial class frmSWIFTBankMap : frmBasedata
    {
        /*---------------------------------------------------------------
         * Muc dich         : Quan ly danh sach ma ngan hang kenh swift
         * Ngay tao         : 15/03/2010
         * Nguoi tao        : 
         *--------------------------------------------------------------*/
        private bool isInsert = false;
        private DataSet datDs = new DataSet();
        DataSet dsSIBSBankCode = new DataSet();
        DataTable dtBranchOfMSB = new DataTable();
        DataTable dtOtherBranch = new DataTable();

        #region dinh nghia cac ham trong lop BusinessObject
        private SWIFT_BANK_MAPInfo objInfo = new SWIFT_BANK_MAPInfo();
        private SWIFT_BANK_MAPController objControl = new SWIFT_BANK_MAPController();
        private BRANCHController objControlBRANCH = new BRANCHController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private SYSVARController objControlSYSVAR = new SYSVARController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        #endregion 

        #region dinh nghia cac bien
        private int iID = 0;
        clsCheckInput clsCheck = new clsCheckInput();
        public static bool isIBPSBankMap = false;
        public static bool isIBPSBankMapLog = false;
        private string sClose;
        private string Cancel;
        private bool isSave=false;
        private string strSIBSBankCode;
        private string SwiftBankCode1 = "";
        private string SIBSBankCode1 = "";
        private string BankName1 = "";
        private string TellerID1 = "";
        private string Description1 = "";        
        private int iRows;
        #endregion

        public frmSWIFTBankMap()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {

                iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString().Trim());
                txtSwiftBankCode.Text = dgView.CurrentRow.Cells["SWIFT_BANK_CODE"].Value.ToString().Trim();
                txtSIBSBankCode.Text = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                txtBankName.Text = dgView.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                txtTellerID.Text = dgView.CurrentRow.Cells["TELLERID"].Value.ToString().Trim();
                txtDescription.Text = dgView.CurrentRow.Cells["DESCRIPTION"].Value.ToString().Trim();
                //-------------------------------------------------------------------------------------                
                SwiftBankCode1 = dgView.CurrentRow.Cells["SWIFT_BANK_CODE"].Value.ToString().Trim();
                SIBSBankCode1 = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                BankName1 = dgView.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                TellerID1 = dgView.CurrentRow.Cells["TELLERID"].Value.ToString().Trim();
                Description1 = dgView.CurrentRow.Cells["DESCRIPTION"].Value.ToString().Trim();                
                //-------------------------------------------------------------------------------------
                txtTellerID.Enabled = false;
                isInsert = false;
                LockTextBox(false);
                CommandStatus(false);
                txtSIBSBankCode.Focus();
                txtSIBSBankCode.SelectAll();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {            
            try
            {
                iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());                
                SwiftBankCode1 = dgView.CurrentRow.Cells["SWIFT_BANK_CODE"].Value.ToString().Trim();
                SIBSBankCode1 = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                BankName1 = dgView.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                TellerID1 = dgView.CurrentRow.Cells["TELLERID"].Value.ToString().Trim();
                Description1 = dgView.CurrentRow.Cells["DESCRIPTION"].Value.ToString().Trim();                
                //--------------------------------------------------------------------------
                if (Common.iSconfirm == 1)
                {
                    objInfo.BANK_MAP_ID = iID;
                    if (objControl.DeleteSWIFT_BANK_MAP(objInfo) == 1)
                    {
                        MessageBox.Show("Delete successful!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //-------------------------------------------------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "SWIFT bank list";
                        int Log_level = 1;
                        string strWorked = "Delete";
                        string strTable = "SWIFT_BANK_MAP";
                        string strOld_value = SwiftBankCode1 + "/" + SIBSBankCode1 + "/" + 
                            BankName1 + "/" + TellerID1 + "/" + Description1;
                        string strNew_value = "";                        
                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            cboView_SelectedIndexChanged(null, null);
            CommandStatus(true);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                isSave = true;
                objInfo.BANK_MAP_ID = iID;
                objInfo.SWIFT_BANK_CODE = txtSwiftBankCode.Text;
                if (txtSIBSBankCode.Text.Trim().Length == 3)
                {
                    objInfo.SIBS_BANK_CODE = "00" + txtSIBSBankCode.Text.Trim();
                }
                else
                {
                    objInfo.SIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                }
                objInfo.BANK_NAME = txtBankName.Text;
                objInfo.TELLERID = txtTellerID.Text;
                objInfo.DESCRIPTION = txtDescription.Text;
                //Kiem tra du lieu
                if (!CheckValidData())                        
                    return;
                #region // Truong hop Insert
                if (isInsert)
                {
                    if (Common.iSconfirm == 1)
                    {       
                        int intInsert = objControl.AddSWIFT_BANK_MAP(objInfo);
                        if (intInsert == 1)
                        {
                            MessageBox.Show("Save data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CommandStatus(true);
                            //-------------------------------------------------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "SWIFT bank list" + objInfo.SWIFT_BANK_CODE;
                            int Log_level = 1;
                            string strWorked = "Insert";
                            string strTable = "SWIFT_BANK_MAP";
                            string strOld_value = "";
                            string strNew_value = objInfo.SWIFT_BANK_CODE + "/" + objInfo.SIBS_BANK_CODE + 
                                "/" + objInfo.BANK_NAME + "/" + objInfo.TELLERID + "/" + 
                                objInfo.DESCRIPTION;
                            //---------------------------------------------------------------
                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                        }
                        else
                        {
                            MessageBox.Show("Insert failed. Please check again!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CommandStatus(false);
                            return;
                        }                    
                    }
                    else
                    {
                        CommandStatus(true);
                        return;
                    }
                }//end if 
                #endregion

                #region // Neu la update
                else
                {
                    if (Common.iSconfirm == 1)
                    {                             
                        int intValue = objControl.UpdateSWIFT_BANK_MAP(objInfo);
                        if (intValue ==1)
                        {
                            MessageBox.Show("Update data successfully!", Common.sCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CommandStatus(true);
                            //-------------------------------------------------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "SWIFT bank list" + objInfo.SWIFT_BANK_CODE;
                            int Log_level = 1;
                            string strWorked = "Update";
                            string strTable = "SWIFT_BANK_MAP";
                            string strOld_value = SwiftBankCode1 + "/" + SIBSBankCode1 + "/" + 
                                BankName1 + "/" + TellerID1 + "/" + Description1;
                            string strNew_value = objInfo.SWIFT_BANK_CODE + "/" + 
                                objInfo.SIBS_BANK_CODE + "/" + objInfo.BANK_NAME + "/" + 
                                objInfo.TELLERID + "/" + objInfo.DESCRIPTION;
                            //---------------------------------------------------------------
                            Ghiloguser(dtLog, strUser, strConten, Log_level, 
                                strWorked, strTable, strOld_value, strNew_value);
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Please check again", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CommandStatus(false);
                            return;
                        }                        
                    }
                    else
                    {
                        CommandStatus(true);
                        return;
                    }
                }
                #endregion

                LoadData();
                CommandStatus(true);
                txtTellerID.Enabled = true;
                txtDescription.Enabled = true;
                txtBankName.Enabled = true;
                txtSwiftBankCode.Enabled = true;
                txtSIBSBankCode.Enabled = true;
                txtSIBSBankCode.Focus();                
                ClearText();
                isSave = false;
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
                isInsert = true;
                LockTextBox(false);
                ClearText();
                txtSIBSBankCode.Focus();
                CommandStatus(false);
                txtTellerID.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSWIFTBankMap_Load(object sender, EventArgs e) 
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                LoadData();
                LockTextBox(false);
                CommandStatus(true);
                cboView.DataSource = objAllcode.GetALLCODE("BankView", "SYSTEM");
                cboView.DisplayMember = "CONTENT";
                cboView.ValueMember = "CDVAL";
                ClearText();
                txtSwiftBankCode.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
         }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdSave.Enabled = !a;
            btnCancel.Enabled = !a;
            cmdDelete.Enabled = a;
            cmdSearch.Enabled = a;
            cboView.Enabled = a;
            dgView.Enabled = a;
        }

        private void ColumsHeader(DataGridView Datagrid)
        {
            try
            {

                int g = 1;
                while (g < Datagrid.Columns.Count)
                {
                    string strColumns = Datagrid.Columns[g].HeaderText.ToString();
                    if (strColumns.Trim() != "SWIFT_BANK_CODE" && strColumns.Trim() != "SIBS_BANK_CODE"
                     && strColumns.Trim() != "SHORT_NAME" && strColumns.Trim() != "TELLERID")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    }
                    else
                    {
                        if (strColumns.Trim() == "SWIFT_BANK_CODE")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "STATE BANK ID";
                        }
                        else if (strColumns.Trim() == "SIBS_BANK_CODE")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "BRANCH";
                        }
                        else if (strColumns.Trim() == "SHORT_NAME")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "SHORT BANK NAME";
                        }
                        else if (strColumns.Trim() == "TELLERID")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "TELLER ID";
                        }
                    }
                     Datagrid.ColumnHeadersHeight = 21;
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadData()
        {
            DataSet datDs = new DataSet();
            datDs = objControl.GetSWIFT_BANK_MAP_ALL("");
            
            if (datDs == null && datDs.Tables[0].Rows.Count == 0)            
                 return;            
            else
            {
                dgView.DataSource = datDs.Tables[0];
                dgView.Columns["BANK_MAP_ID"].Visible = false;
                dgView.Columns["TELLERID"].Visible = false;
                lblTong.Text = Convert.ToString(datDs.Tables[0].Rows.Count);
            }
            //ColumsHeader(dgView);
            DataGridColumnsSize();
        }

        private void DataGridColumnsSize()
        {
          
            dgView.Columns[0].Visible = false;
            dgView.Columns[1].Width = 120;
            dgView.Columns[2].Width = 120;
            dgView.Columns[3].Width = 220;
            dgView.Columns[4].Width = 200;
            dgView.Columns[5].Width = 240;            
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
               
        private bool CheckIBPSBankLength()
        {
            bool result = true;
            string ID = txtSIBSBankCode.Text.Trim();
            string strLength = "SIBSBankCodeLength";
            DataSet ds = new DataSet();
            ds = objControlSYSVAR.GetIBPSBankLength(strLength);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                int length = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (ID.Length != length)
                {                    
                    MessageBox.Show("SIBS bank code's length must be " + length + " numbers!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    result = false;
                }
            }
            CommandStatus(false);
            txtSwiftBankCode.Focus();
            return result;
        }

        private bool CheckValidData()
        {
            try
            {
                bool iResult = true;

                if (string.IsNullOrEmpty(txtSIBSBankCode.Text.Trim()))
                {
                    MessageBox.Show("You must input SIBS bank code !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSIBSBankCode.Focus();
                    CommandStatus(false);
                    return false;
                }
                if (string.IsNullOrEmpty(txtSwiftBankCode.Text.Trim()))
                {
                    MessageBox.Show("You must input swift bank code !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSwiftBankCode.Focus();
                    CommandStatus(false);
                    return false;
                }
                if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                {
                    MessageBox.Show("You must input Bank Name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBankName.Focus();
                    CommandStatus(false);
                    return false;
                }             
                //Kiem tra xem swift_bank_code co ton tai
                if (isInsert == true) //insert
                {
                    if ((GetData.IDIsExisting(false, "swift_bank_map", "SWIFT_BANK_CODE", txtSwiftBankCode.Text.Trim(), "")))
                    {
                        MessageBox.Show("Swift bank code has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSwiftBankCode.Text = "";
                        txtSwiftBankCode.Focus();
                        CommandStatus(false);
                        return false;
                    }
                }
                else //update
                {

                    if ((GetData.IDIsExisting(true, "swift_bank_map", "SWIFT_BANK_CODE", txtSwiftBankCode.Text.Trim(), dgView.CurrentRow.Cells["SWIFT_BANK_CODE"].Value.ToString().Trim())))
                    {
                        MessageBox.Show("Swift bank code has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSwiftBankCode.Text = "";
                        txtSwiftBankCode.Focus();
                        CommandStatus(false);
                        return false;
                    }
                }
                //Kiem tra xem sibs_bank_code co ton tai                
                if (isInsert == true) //insert
                {
                    if (GetData.IDIsExisting(false, "swift_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), ""))
                    {
                        MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSIBSBankCode.Text = "";
                        txtSIBSBankCode.Focus();
                        CommandStatus(false);
                        return false;
                    }                    
                }
                else //neu la Update
                {
                    if (GetData.IDIsExisting(true, "swift_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSIBSBankCode.Text = "";
                        txtSIBSBankCode.Focus();
                        CommandStatus(false);
                        return false;
                    }
                }
                if (isInsert == true) //insert
                    objInfo.BANK_MAP_ID = 0;
                else
                    iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString().Trim());
                objInfo.BANK_NAME = txtBankName.Text.ToString();
                objInfo.DESCRIPTION = txtDescription.Text.ToString();
                objInfo.SIBS_BANK_CODE = txtSIBSBankCode.Text.ToString();
                objInfo.SWIFT_BANK_CODE = txtSwiftBankCode.Text.ToString();
                objInfo.TELLERID = txtTellerID.Text.ToString();

                return iResult;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }

        private void LockTextBox(Boolean a)
        {
            txtSwiftBankCode.ReadOnly = a;
            txtSIBSBankCode.ReadOnly = a;
            txtBankName.ReadOnly = a;
            txtTellerID.ReadOnly = a;
            txtDescription.ReadOnly = a;            
        }

        private void ClearText()
        {
            txtSwiftBankCode.Text = "";
            txtSIBSBankCode.Text = "";
            txtBankName.Text = "";
            txtTellerID.Text = "";
            txtDescription.Text = "";            
        }
       
        private void cboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strWhere;
                if (cboView.Text.Trim() == "Branch of MSB")
                {
                    DataTable dtBranchOfMSB = new DataTable();
                    strWhere = " WHERE SWIFT_BANK_CODE LIKE 'MCOB%'";
                    dtBranchOfMSB = objControl.GetSWIFT_BANK_MAP_ALL(strWhere).Tables[0];
                    dgView.DataSource = dtBranchOfMSB;
                    lblTong.Text = dtBranchOfMSB.Rows.Count.ToString();
                }
                else if (cboView.Text.Trim() == "ALL")
                {
                    LoadData();
                }
                else if (cboView.Text.Trim() == "Other Bank")
                {
                    DataTable dtOtherBranch = new DataTable();
                    strWhere = " WHERE SWIFT_BANK_CODE NOT LIKE 'MCOB%'";
                    dtOtherBranch = objControl.GetSWIFT_BANK_MAP_ALL(strWhere).Tables[0];
                    dgView.DataSource = dtOtherBranch;
                    lblTong.Text = dtOtherBranch.Rows.Count.ToString();
                }
                CommandStatus(true);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strSwiftBankCode; 
                string strSIBSBankCode; 
                string strBankName; 
                string strTellerID; 
                string strDescription;
                string strWHERE = "";
                string strWhere1 = "";

                if (txtSwiftBankCode.Text == "")
                    strSwiftBankCode = ""; 
                else
                    strSwiftBankCode = " and  upper(trim(SWIFT_BANK_CODE)) like '%" + 
                        txtSwiftBankCode.Text.Trim().ToUpper() + "%'";
                if (txtSIBSBankCode.Text == "") 
                    strSIBSBankCode = ""; 
                else
                    strSIBSBankCode = " and  upper(trim(SIBS_BANK_CODE)) like '%" + 
                        txtSIBSBankCode.Text.Trim().ToUpper() + "%'"; 
                if (txtBankName.Text == "")
                    strBankName = "";
                else
                    strBankName = " and  upper(trim(BANK_NAME)) like '%" + 
                        txtBankName.Text.Trim().ToUpper() + "%'"; 
                if (txtTellerID.Text == "") 
                    strTellerID = ""; 
                else 
                    strTellerID = " and  upper(trim(TELLERID)) like '%" + 
                        txtTellerID.Text.Trim().ToUpper() + "%'"; 
                if (txtDescription.Text == "") 
                    strDescription = "";
                else
                    strDescription = " and  upper(trim(DESCRIPTION)) like '%" + 
                        txtDescription.Text.Trim().ToUpper() + "%'";

                strWHERE = strSwiftBankCode + strSIBSBankCode + 
                    strBankName + strTellerID + strDescription;                
                if (strWHERE == "")
                {
                    strWhere1 = strWHERE;
                }
                else
                {
                    strWhere1 = " Where " + strWHERE.Substring(5);
                }
                DataSet datSearch = new DataSet();
                datSearch = objControl.GetSWIFT_BANK_MAP_ALL(strWhere1);


                if (datSearch == null)
                {
                    dgView.DataSource = 0;
                    lblTong.Text = "0";
                    return;
                }                                
                dgView.DataSource = datSearch.Tables[0];
                lblTong.Text = Convert.ToString(datSearch.Tables[0].Rows.Count);
                if (dgView.RowCount > 0)
                {
                    CommandStatus(true);
                    //ColumsHeader(dgView);
                    DataGridColumnsSize();
                }
                else
                {
                    CommandStatus(true);
                    //ColumsHeader(dgView);
                    DataGridColumnsSize();
                    txtSwiftBankCode.Enabled = true;
                    MessageBox.Show("There is no suitable message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
                
        private void frmSWIFTBankMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               
                if (cmdSave.Enabled)
                    e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.ClearControl(this);            
            dgView.Enabled = true;
            cboView.Enabled = true;
            cmdAdd.Enabled = true;
            cmdSave.Enabled = false;
            cmdSearch.Enabled = true;
            btnCancel.Enabled = false;
            if (dgView.RowCount >0)
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
            }                          
            txtSIBSBankCode.Focus(); 
            txtTellerID.Enabled = true;
            txtSwiftBankCode.Enabled = true;
        }
        
        private void txtSwiftBankCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sClose == "1" || Cancel == "1")
                {
                    sClose = "0"; 
                    Cancel = "0";
                }
                else
                {
                    if (cmdSearch.Enabled == true)
                    { return; }
                    else
                    {
                        if (String.IsNullOrEmpty(txtSwiftBankCode.Text.Trim()))
                        { return; }
                        else
                        {
                            if (txtSwiftBankCode.Text.Trim().Length != 8)
                            {
                                MessageBox.Show("Invalid swift bank code length!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtSwiftBankCode.SelectAll();
                                txtSwiftBankCode.Focus();
                                return;
                            }
                            else
                            {
                                if (isInsert == true)
                                {
                                    if ((GetData.IDIsExisting(false, "swift_bank_map", "SWIFT_BANK_CODE", txtSwiftBankCode.Text.Trim(), "")))
                                    {
                                        MessageBox.Show("Swift bank code has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtSwiftBankCode.Text = "";
                                        txtSwiftBankCode.Focus();
                                        CommandStatus(false);
                                        return;
                                    }
                                }
                                else
                                {

                                    if ((GetData.IDIsExisting(true, "swift_bank_map", "SWIFT_BANK_CODE", txtSwiftBankCode.Text.Trim(), dgView.CurrentRow.Cells["SWIFT_BANK_CODE"].Value.ToString().Trim())))
                                    {
                                        MessageBox.Show("Swift bank code has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtSwiftBankCode.Text = "";
                                        txtSwiftBankCode.Focus();
                                        CommandStatus(false);
                                        return;
                                    }
                                }
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

        private void txtSIBSBankCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sClose == "1" || Cancel == "1")
                {
                    sClose = "0"; Cancel = "0";
                }
                else
                {
                    if (cmdSearch.Enabled == true)
                    { return; }
                    else
                    {
                        if (string.IsNullOrEmpty(txtSIBSBankCode.Text.Trim()))
                        { return; }
                        else
                        {
                            if (isSave == true)
                            { return; }
                            else
                            {                                
                                #region Neu la Insert
                                if (isInsert == true)
                                {
                                    if (GetData.IDIsExisting(false, "swift_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), ""))
                                    {
                                        MessageBox.Show("Sibs bank code has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtSIBSBankCode.Text = "";
                                        txtSIBSBankCode.Focus();
                                        CommandStatus(false);
                                        return;
                                    }
                                    else // neu chua ton tai ban ghi 
                                    {
                                        txtTellerID.Text = "FPTIBPS000";
                                        txtBankName.Text = "";
                                        return;
                                    }
                                }
                                #endregion

                                #region neu la Update
                                else // neu la Update
                                {
                                    if (GetData.IDIsExisting(true, "swift_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim()))
                                    {
                                        MessageBox.Show("Sibs bank code has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtSIBSBankCode.SelectAll();
                                        txtSIBSBankCode.Focus();
                                        CommandStatus(false);
                                        return;
                                    }
                                }
                                #endregion
                                                                
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

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            try
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
                        if ((this.ActiveControl as Button).Name == "cmdSave")
                        {
                            cmdSave.Focus();
                        }
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
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtBankName_Leave(object sender, EventArgs e)
        {
            txtBankName.Text = clsCheck.ConvertVietnamese(txtBankName.Text.Trim());
        }               

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            txtDescription.Text = clsCheck.ConvertVietnamese(txtDescription.Text.Trim());
        }

        private void btnCancel_MouseMove(object sender, MouseEventArgs e)
        {
            Cancel = "1";
        }

        private void cmdClose_MouseMove(object sender, MouseEventArgs e)
        {
            sClose = "1";
        }

        private void btnCancel_MouseUp(object sender, MouseEventArgs e)
        {
            Cancel = "0";
        }

        private void cmdClose_MouseUp(object sender, MouseEventArgs e)
        {
            sClose = "0";
        }

        private void frmSWIFTBankMap_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdHistory_Click(object sender, EventArgs e)
        {            
            try
            {
                string ISearch = "";
                //string ISearch1 = "";
                ISearch = dgView.Rows[iRows].Cells["SWIFT_BANK_CODE"].Value.ToString().Trim();
                //ISearch1 = dgView.Rows[iRows].Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                frmHistory frmViewLog = new frmHistory();
                frmViewLog.pForm_name = "IBPS bank list" + ISearch;
                frmViewLog.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void cmdGetBranch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }

        }   
      
    }
}

