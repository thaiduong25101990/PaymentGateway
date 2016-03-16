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
using System.Text.RegularExpressions;

namespace BR.BRSYSTEM
{
    public partial class frmStrangeChar : frmBasedata
    {
        #region bien va cac ham
        public static bool isInsert = false;
        public static bool isLock = false;
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private DataSet datDs = new DataSet();
        private STRANGE_CHARACTERInfo objInfo = new STRANGE_CHARACTERInfo();
        private STRANGE_CHARACTERController objControl = new STRANGE_CHARACTERController();
        private ALLCODEInfo objallcode = new ALLCODEInfo();
        private ALLCODEController objallcodecontrol = new ALLCODEController();
        private clsCheckInput clsCheck = new clsCheckInput();
        private int iID = 0;        
        private int iRows;
        private string strTXTX;
        #endregion

        public frmStrangeChar()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {                
                isInsert = true;
                frmStrangeCharInfo SCInfo = new frmStrangeCharInfo();
                SCInfo.iLoad = 1;
                isLock = false;
                SCInfo.ShowDialog();
                cmdAdd.Enabled = true; cmdEdit.Enabled = true; cmdDelete.Enabled = true;
                if (frmStrangeCharInfo.isCancel == false)
                {
                    txtField.Text = ""; txtMsgType.Text = ""; txtStrangeChar.Text = "";
                    LoadData();
                }
                else
                { cmdSearch_Click(null, null); }
                txtStrangeChar.Focus();                
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
            try
            {
                isLock = false;
                frmStrangeCharInfo SCInfo = new frmStrangeCharInfo();
                if (dgView.CurrentRow == null)
                {
                    dgView.Focus();
                }
                //---Quynd Update 30/07/2008
                SCInfo.strID = dgView.Rows[iRows].Cells[0].Value.ToString();
                SCInfo.strSTRANGE_CHAR = dgView.Rows[iRows].Cells[1].Value.ToString();
                SCInfo.strREPLACE_CHAR = dgView.Rows[iRows].Cells[2].Value.ToString();
                SCInfo.strMSG_TYPE = dgView.Rows[iRows].Cells[3].Value.ToString();
                SCInfo.strDEPARTMENT = dgView.Rows[iRows].Cells[4].Value.ToString();
                SCInfo.strFIELD_CODE = dgView.Rows[iRows].Cells[5].Value.ToString();
                SCInfo.strMSG_DIRECTION = dgView.Rows[iRows].Cells[6].Value.ToString();
                SCInfo.strGWTYPE = dgView.Rows[iRows].Cells[7].Value.ToString();
                SCInfo.strLINE = dgView.Rows[iRows].Cells[8].Value.ToString();
                SCInfo.strPOSITION = dgView.Rows[iRows].Cells[9].Value.ToString();
                SCInfo.iLoad = 0;
                //------------------------------------------------------------------
                isInsert = false;
                SCInfo.ShowDialog();
                cmdAdd.Enabled = true;
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdSearch_Click(null, null);
                txtStrangeChar.Focus();
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
                dgView.Select();
                string sSTRANGE_CHAR = dgView.Rows[iRows].Cells[1].Value.ToString();
                string sREPLACE_CHAR = dgView.Rows[iRows].Cells[2].Value.ToString();
                string sMSG_TYPE = dgView.Rows[iRows].Cells[3].Value.ToString();
                string sDEPARTMENT = dgView.Rows[iRows].Cells[4].Value.ToString();
                string sFIELD_CODE = dgView.Rows[iRows].Cells[5].Value.ToString();
                string sMSG_DIRECTION = dgView.Rows[iRows].Cells[6].Value.ToString();
                string sGWTYPE = dgView.Rows[iRows].Cells[7].Value.ToString();
                string sLINE = dgView.Rows[iRows].Cells[8].Value.ToString();
                string sPOSITION = dgView.Rows[iRows].Cells[9].Value.ToString();

                if (Common.iSconfirm == 1)
                {
                    iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                    if (objControl.DeleteSTRANGE_CHARACTER(iID) == 1)
                    {
                        Common.ShowError("Delete successfully!", 1, MessageBoxButtons.OK);                        
                    }
                }
                else 
                {
                    return;
                }
                #region lay thong tin de ghilog----------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Strange character";
                int Log_level = 1;
                string strWorked = "Delete";
                string strTable = "STRANGECHAR";
                string strOld_value = sSTRANGE_CHAR + "/" + sREPLACE_CHAR + "/" + 
                    sMSG_TYPE + "/" + sDEPARTMENT + "/" + sFIELD_CODE + "/" + 
                    sMSG_DIRECTION + "/" + sGWTYPE + "/" + sLINE + "/" + sPOSITION;
                string strNew_value = "";
                //-----------------------------------------
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                    strWorked, strTable, strOld_value, strNew_value);
                #endregion
                LoadData();
                txtStrangeChar.Focus();
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
                dgView.DataSource = 0;
                string strStrangeChar; string strMsgType; string strGWType; string strDepartment;
                string strField;
                //-----------------------------------------QUYND UPDATE 30/07/2008-------
                if (txtStrangeChar.Text.Trim() == "") { strStrangeChar = ""; }
                else 
                {
                    if (txtStrangeChar.Text.Trim() == "'")
                    {
                        strStrangeChar = " and  upper(trim(sch.STRANGE_CHAR)) = ''" + 
                            txtStrangeChar.Text.Trim().ToUpper() + "'";
                    }
                    else if (txtStrangeChar.Text.Trim() == "''")
                    {
                        strStrangeChar = " and  upper(trim(sch.STRANGE_CHAR)) = '''" + 
                            txtStrangeChar.Text.Trim().ToUpper() + "'";
                    }
                    else if (txtStrangeChar.Text.Trim() == "%")
                    {
                        strStrangeChar = " and  upper(trim(sch.STRANGE_CHAR)) = '" + 
                            txtStrangeChar.Text.Trim().ToUpper() + "'";
                    }
                    else if (txtStrangeChar.Text.Trim() == "%%")
                    {
                        strStrangeChar = " and  upper(trim(sch.STRANGE_CHAR)) = '" + 
                            txtStrangeChar.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        strStrangeChar = " and  upper(trim(sch.STRANGE_CHAR)) like '%" + 
                            txtStrangeChar.Text.Trim().ToUpper() + "%'";
                    }
                }
                string strMsg;
                if (txtMsgType.Text.Trim().Length == 5)
                {
                    strMsg = txtMsgType.Text.Trim().Substring(txtMsgType.Text.Trim().Length - 3, 3);
                }
                else
                {
                    strMsg = txtMsgType.Text.Trim();
                }
                if (txtMsgType.Text == "")
                    strMsgType = ""; 
                else 
                    strMsgType = " and  upper(Substr(Trim(sch.MSG_TYPE),3)) like '%" + strMsg + "%'";                
                if (cboGWType.Text.Trim() == "ALL") 
                    strGWType = ""; 
                else 
                    strGWType = "  and  upper(trim(sch.GWType)) = '" + 
                        cboGWType.Text.Trim().ToUpper() + "'"; 
                if (cboDepartment.Text.Trim() == "ALL") 
                    strDepartment = ""; 
                else 
                    strDepartment = "  and  upper(trim(sch.DEPARTMENT)) = '" + 
                        cboDepartment.Text.Trim().ToUpper() + "'";                 
                if (txtField.Text.Trim() == "") 
                    strField = ""; 
                else 
                    strField = " and  upper(trim(sch.FIELD_CODE)) like '%" + 
                        txtField.Text.Trim().ToUpper() + "%'"; 

                string strWHERE = "" + strStrangeChar + strMsgType + strGWType + 
                    strDepartment + strField;
                string strWhere1;
                //---------------------------------------------------------------------
                DataSet datSearch = new DataSet();
                if (strWHERE.Trim() == "")
                {
                    strWhere1 = "";                   
                }
                else
                {
                    string strDD = strWHERE.Substring(6);
                    strWhere1 = "  Where  "+  strDD;
                }

                datSearch = objControl.GetSTRANGE_CHARACTERSearch(strWhere1);
                if (datSearch.Tables[0].Rows.Count == 0)
                {
                    Common.ShowError("There is no suitable result!", 1, MessageBoxButtons.OK);
                    cmdAdd.Enabled = true; cmdEdit.Enabled = false; cmdDelete.Enabled = false;
                    return;
                }
                else
                {
                    dgView.DataSource = datSearch.Tables[0];
                    Width_columns();
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true ;
                    cmdDelete.Enabled = true ;                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Width_columns()
        {
            try
            {
                #region do rong cua cac cot
                dgView.Columns[0].Visible = false;
                dgView.Columns["STRANGE_CHAR"].HeaderText = "Strange character";
                dgView.Columns["STRANGE_CHAR"].Width = 100;
                dgView.Columns["REPLACE_CHAR"].HeaderText = "Replace character";
                dgView.Columns["REPLACE_CHAR"].Width = 100;
                dgView.Columns["MSG_TYPE"].HeaderText = "Message type";
                dgView.Columns["MSG_TYPE"].Width = 100;
                dgView.Columns["DEPARTMENT"].HeaderText = "Module";
                dgView.Columns["DEPARTMENT"].Width = 100;
                dgView.Columns["FIELD_CODE"].HeaderText = "Field code";
                dgView.Columns["FIELD_CODE"].Width = 100;
                dgView.Columns["MSG_DIRECTION"].HeaderText = "Message direction";
                dgView.Columns["MSG_DIRECTION"].Width = 100;
                dgView.Columns["GWTYPE"].HeaderText = "Channel";
                dgView.Columns["GWTYPE"].Width = 100;
                dgView.Columns["Line"].HeaderText = "Line";
                dgView.Columns["Line"].Width = 100;
                dgView.Columns["Position"].HeaderText = "Position";
                dgView.Columns["Position"].Width = 100;
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void frmStrangeChar_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                //Load data cboDepartment
                if (!objGetData.FillDataComboBox(cboDepartment, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='SYSTEM' AND CDNAME='Department'", "CONTENT", true, true, "ALL"))
                    return;
                //Load data cboDepartment
                if (!objGetData.FillDataComboBox(cboGWType, "GWTYPE", "GWTYPE", "GWTYPE",
                    "GWTYPESTS='1'", "GWTYPE", true, true, "ALL"))
                    return;
                LoadData();
                cboGWType.Select();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        //------------QuyND update 30/07/2008---------        
        public void LoadData()
        {
            try
            {
                DataSet datDs = new DataSet();
                string strStrangeChar; string strMsgType; string strGWType; string strDepartment;
                string strField;
                string strMsg;
                if (clsCheck.ConvertVietnamese(txtStrangeChar.Text.Trim()) == "") 
                { 
                    strStrangeChar = ""; 
                } else { 
                    strStrangeChar = " and  upper(trim(sch.STRANGE_CHAR)) like '%" + 
                        clsCheck.ConvertVietnamese(txtStrangeChar.Text.Trim().ToUpper()) + "%'"; 
                }
                
                if (txtMsgType.Text.Trim().Length == 5)
                {
                    strMsg = txtMsgType.Text.Trim().Substring(txtMsgType.Text.Trim().Length - 3, 3);
                }
                else
                {
                    strMsg = clsCheck.ConvertVietnamese(txtMsgType.Text.Trim());
                }
                if (clsCheck.ConvertVietnamese(txtMsgType.Text.Trim()) == "") 
                    strMsgType = ""; 
                else 
                    strMsgType = " and  upper(Substr(Trim(sch.MSG_TYPE),3)) like '%" + strMsg + "%'";                
                if (cboGWType.Text.Trim() == "ALL") 
                    strGWType = "";
                else 
                    strGWType = "  and  upper(trim(sch.GWType)) = '" + 
                        cboGWType.Text.Trim().ToUpper() + "'";
                if (cboDepartment.Text.Trim() == "ALL") 
                    strDepartment = ""; 
                else 
                    strDepartment = "  and  upper(trim(sch.DEPARTMENT)) = '" + 
                        cboDepartment.Text.Trim().ToUpper() + "'";
                if (txtField.Text.Trim() == "") 
                    strField = ""; 
                else 
                    strField = " and  upper(trim(sch.FIELD_CODE)) like '%" + 
                        txtField.Text.Trim().ToUpper() + "%'";

                string strWHERE = "" + strStrangeChar + strMsgType + strGWType + 
                    strDepartment + strField;
                string strWhere1;
                //-------------------------------------------------------------------
                DataSet datSearch = new DataSet();
                if (strWHERE.Trim() == "")
                {
                    strWhere1 = "";
                }
                else
                {
                    string strDD = strWHERE.Substring(6);
                    strWhere1 = "  Where  " + strDD;
                }
                datDs = objControl.GetSTRANGE_CHARACTERSearch(strWhere1);
                if (datDs != null) { dgView.DataSource = datDs.Tables[0]; }
                FomatGrid.ColumsHeaderDataGridView(dgView);
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;                   
                    return;
                }
                else
                {
                    Width_columns();
                    //dgView.SelectedRows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgView.Rows.Count == 0)
                {
                    Common.ShowError("There is no message!", 1, MessageBoxButtons.OK);                    
                    return;
                }
                else
                {
                    isLock = true;
                    frmStrangeCharInfo SCInfo = new frmStrangeCharInfo();                   
                    SCInfo.strID = dgView.Rows[iRows].Cells[0].Value.ToString();
                    SCInfo.strSTRANGE_CHAR = dgView.Rows[iRows].Cells[1].Value.ToString();
                    SCInfo.strREPLACE_CHAR = dgView.Rows[iRows].Cells[2].Value.ToString();
                    SCInfo.strMSG_TYPE = dgView.Rows[iRows].Cells[3].Value.ToString();
                    SCInfo.strDEPARTMENT = dgView.Rows[iRows].Cells[4].Value.ToString();
                    SCInfo.strFIELD_CODE = dgView.Rows[iRows].Cells[5].Value.ToString();
                    SCInfo.strMSG_DIRECTION = dgView.Rows[iRows].Cells[6].Value.ToString();
                    SCInfo.strGWTYPE = dgView.Rows[iRows].Cells[7].Value.ToString();
                    SCInfo.strLINE = dgView.Rows[iRows].Cells[8].Value.ToString();
                    SCInfo.strPOSITION = dgView.Rows[iRows].Cells[9].Value.ToString();                    
                    SCInfo.ShowDialog();
                    SCInfo.LockTextBox(true);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtMsgType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtMsgType.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                {//strTXTX
                    if (txtMsgType.Text.Trim().Length <= 3)
                    { strTXTX = txtMsgType.Text.Trim(); }
                    else
                    { txtMsgType.Text = strTXTX; }
                }
                else
                { txtMsgType.Text = ""; }
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
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

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
                    if ((this.ActiveControl as Button).Name == "cmdSearch")
                    {
                        cmdSearch_Click(null, null);
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {

            Common.ClearControl(this);
            dgView.Enabled = true;
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;            
            cmdDelete.Enabled = true;            
            cmdSearch.Enabled = true;
            
        }

        private void txtStrangeChar_Leave(object sender, EventArgs e)
        {
            txtStrangeChar.Text = clsCheck.ConvertVietnamese(txtStrangeChar.Text.Trim());
        }

        private void txtMsgType_Leave(object sender, EventArgs e)
        {
            txtMsgType.Text = clsCheck.ConvertVietnamese(txtMsgType.Text.Trim());
        }

        private void txtField_Leave(object sender, EventArgs e)
        {
            txtField.Text = clsCheck.ConvertVietnamese(txtField.Text.Trim());
        }

        private void frmStrangeChar_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
