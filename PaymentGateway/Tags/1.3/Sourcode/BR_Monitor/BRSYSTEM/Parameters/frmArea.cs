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
    public partial class frmArea : frmBasedata 
    {
        public frmArea()
        {
            InitializeComponent();
        }
        #region  dinh nghia ham ,bien
        private GetData objGetData = new GetData();
        private clsLog objLog = new clsLog();
        private AREAInfo objAREAInfo = new AREAInfo();
        private AREAController objAREAController = new AREAController();        
        private clsCheckInput clsCheck = new clsCheckInput();        
        private ALLCODEController objAllcode = new ALLCODEController();
        private int iID;
        private int iRows;        
        //Cac tham so ghi log
        private DateTime tLogDate = DateTime.Now;
        private int iLogLevel = 1;
        private string sLogContent = "IBPS AREA";        
        private string sLogWorked = "";
        private string sLogTable = "AREA";
        private string sLogOldValue = "";
        private string sLogNewValue = "";
        #endregion      


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Set control
        // Tham so:     NA
        // Tra ve:      
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void RefreshControl()
        {
            cmdSave.Enabled = false; cmdAdd.Enabled = true; cmdSearch.Enabled = true; cmdCancel.Enabled = false;
            if (dgArea.RowCount == 0)
            { cmdEdit.Enabled = false; cmdDelete.Enabled = false; }
            else
            { cmdEdit.Enabled = true; cmdDelete.Enabled = true; }
            if (iSadd == true)
            {
                txtProvinceCode.Text = ""; txtShortname.Text = ""; txtFullName.Text = "";
                cboType.Text = "ALL";
                cmdAdd.Enabled = false; cmdEdit.Enabled = false; cmdDelete.Enabled = false;
                cmdSave.Enabled = true; cmdSearch.Enabled = false; txtProvinceCode.Focus();
                cmdCancel.Enabled = true;
            }
            else if (iSupdate == true)
            {
                cmdAdd.Enabled = false; cmdEdit.Enabled = false;
                cmdDelete.Enabled = false; cmdSave.Enabled = true;
                cmdSearch.Enabled = false; txtShortname.Focus();
                cmdCancel.Enabled = true;
            }
        }
                
        /////////////////////////////////////////////////////////-/
        // Mo ta:       Load DataGridView
        // Tham so:     bSearch: True: Search, False: Get full
        // Tra ve:      
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void LoadDataGridSearch(bool bSearch)
        {
            string strProvinceCode; string strShortName; string strFullName; string strType;
            DataSet dsData = new DataSet();

            if (txtProvinceCode.Text == "") 
                strProvinceCode = ""; 
            else 
                strProvinceCode = " and upper(trim(Area.PROV_CODE)) like '%" + 
                clsCheck.ConvertVietnamese(txtProvinceCode.Text.Trim().ToUpper()) + "%'"; 
            if (txtShortname.Text == "")
                strShortName = ""; 
            else 
                strShortName = " and  upper(trim(Area.SHORTNAME)) like '%" + 
                Common.ConvertString(clsCheck.ConvertVietnamese(txtShortname.Text.Trim().ToUpper())) + "%'"; 
            if (txtFullName.Text == "")  
                strFullName = ""; 
            else 
                strFullName = " and  upper(trim(Area.FULLNAME)) like '%" + 
                Common.ConvertString(clsCheck.ConvertVietnamese(txtFullName.Text.Trim().ToUpper())) + "%'"; 
            if (cboType.Text == "ALL" || cboType.SelectedValue.ToString()=="ALL")  
                strType = ""; 
            else 
                strType = " and upper(trim(Area.CITAD_MEMBER)) like "  + 
                          "(select a.cdval from allcode a where upper(trim(a.cdname))='AREA'" + 
                          "and upper(trim(a.content))= '" + cboType.Text.Trim().ToUpper() + "')"; 
            string strCondition = strProvinceCode + strShortName + strFullName + strType;
            if (bSearch == true)
            {
                if (strCondition != "")
                {
                    if (strCondition.Substring(1, 3) == "and")
                        strCondition = strCondition.Substring(4);
                }
            }
            else
                strCondition = "";
            dsData = objAREAController.GetAREA(strCondition);
            if (dsData != null) { dgArea.DataSource = dsData.Tables[0]; }
            FormatDataGridView();

            if (dsData.Tables[0].Rows.Count > 0)
            { cmdDelete.Enabled = true; cmdEdit.Enabled = true; }
            else
            { cmdDelete.Enabled = false; cmdEdit.Enabled = false; }
            cmdAdd.Enabled = true;
            cmdSave.Enabled = false;            
            txtProvinceCode.Focus();            
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Format DataGridView
        // Tham so:     NA
        // Tra ve:      
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void FormatDataGridView()
        {
           dgArea.RowHeadersVisible = true;
           dgArea.ColumnHeadersVisible = true;
           dgArea.Columns[0].Visible = false;
           dgArea.Columns["PROV_CODE"].HeaderText = "Province code";
           dgArea.Columns["PROV_CODE"].Width = 70;
           dgArea.Columns["SHORTNAME"].HeaderText = "Short name";
           dgArea.Columns["SHORTNAME"].Width = 80;
           dgArea.Columns["FULLNAME"].HeaderText = "Full name";
           dgArea.Columns["FULLNAME"].Width = 250;
           dgArea.Columns["CITAD_MEMBER"].HeaderText = "Citad member Value";
           dgArea.Columns["CITAD_MEMBER"].Visible = false;
           dgArea.Columns["CITAD_MEMBER"].HeaderText = "Citad member";
           dgArea.Columns["CITAD_MEMBER"].Width = 150;          
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Check data before save
        // Tham so:     NA
        // Tra ve:      True: Successfull
        //              False: Not successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private bool Verify()
        {
            DataSet dsData = new DataSet();

            if (txtProvinceCode.Text.Length < 2)
            {                
                Common.ShowError("Province code must include 2 characters!", 
                    1, MessageBoxButtons.OK);
                txtProvinceCode.Focus();
                return false;
            }
            
            txtShortname.Text = txtShortname.Text.Trim();
            if (txtShortname.Text == "")
            {
                Common.ShowError("Short name is emty!", 1, MessageBoxButtons.OK);
                txtShortname.Focus();
                return false;
            }

            txtFullName.Text = txtFullName.Text.Trim();
            if (txtFullName.Text == "")
            {
                Common.ShowError("Full name is emty!", 1, MessageBoxButtons.OK);                
                txtFullName.Focus();
                return false;
            }

            if (cboType.Text == "ALL")
            {
                Common.ShowError("Type is invalid!", 1, MessageBoxButtons.OK);
                cboType.Focus();
                return false;
            }

            //Kiem tra trung code khi them moi
            if (iSadd == true)
            {
                dsData = objAREAController.GetAREA(" PROV_CODE = " + txtProvinceCode.Text);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    txtProvinceCode.Focus();
                    Common.ShowError("Province code has existed!", 1, MessageBoxButtons.OK);
                    return false;
                }
            }
            else if (iSupdate == true)
            {
                if (GetData.IDIsExisting(true, "AREA", "PROV_CODE", 
                    txtProvinceCode.Text.Trim().ToUpper(), 
                    dgArea.CurrentRow.Cells["PROV_CODE"].Value.ToString()))
                {
                    txtProvinceCode.Focus();                    
                    Common.ShowError("Province code has already existed!", 3, MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;            
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Save data 
        // Tham so:     NA
        // Tra ve:      True: Successfull
        //              False: Not successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private bool SaveData()
        {
            int iResult = 0;

            //Cac gia tri moi
            objAREAInfo.PROV_CODE = txtProvinceCode.Text;
            objAREAInfo.SHORTNAME = txtShortname.Text;
            objAREAInfo.FULLNAME = txtFullName.Text;
            objAREAInfo.CITAD_MEMBER = cboType.SelectedValue.ToString();            
            //Tham so ghi log gia tri moi
            sLogNewValue =  txtProvinceCode.Text.Trim() + "/" + txtShortname.Text + "/" +
                            txtFullName.Text.Trim() + "/" +
                            cboType.SelectedValue.ToString().Trim(); 
            if (iSadd == true)                     
            {
                sLogWorked = "INSERT";
                sLogOldValue = "";
                iResult = objAREAController.InsertAREA(objAREAInfo);
                iSadd = false;
            }
            //Update
            else if (iSupdate == true)
            {                
                if (dgArea.CurrentRow != null)
                {
                    sLogOldValue = dgArea.CurrentRow.Cells["PROV_CODE"].Value.ToString() + "/" +
                                    dgArea.CurrentRow.Cells["SHORTNAME"].Value.ToString() + "/" +
                                    dgArea.CurrentRow.Cells["FULLNAME"].Value.ToString() + "/" +
                                    dgArea.CurrentRow.Cells["CITAD_MEMBER"].Value.ToString();
                    objAREAInfo.ID = Convert.ToInt16(dgArea.CurrentRow.Cells["ID"].Value);
                }
                else
                {
                    sLogOldValue = dgArea.Rows[0].Cells["PROV_CODE"].Value.ToString() + "/" +
                                    dgArea.Rows[0].Cells["SHORTNAME"].Value.ToString() + "/" +
                                    dgArea.Rows[0].Cells["FULLNAME"].Value.ToString() + "/" +
                                    dgArea.Rows[0].Cells["CITAD_MEMBER"].Value.ToString();
                    objAREAInfo.ID = Convert.ToInt16(dgArea.Rows[0].Cells["ID"].Value);
                }
                //objAREAInfo.ID = iID;
                sLogWorked = "UPDATED";
                iResult = objAREAController.UpdateAREA(objAREAInfo);
                iSupdate = false;
            }
            //goi ham ghilog
            objLog.GhiLogUser(tLogDate, Common.Userid, sLogContent, iLogLevel,
                sLogWorked, sLogTable, sLogOldValue, sLogNewValue);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Delete
        // Tham so:     NA
        // Tra ve:      True: Successfull
        //              False: Not successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private bool Delete()
        {
            int iResult=0;

            if (Common.iSconfirm == 1)
                if (dgArea.CurrentRow == null)
                {
                    Common.ShowError("You have not select row!", 1, MessageBoxButtons.OK);
                    return false;
                }
            //Cac tham so ghi log
            sLogNewValue = "";
            sLogWorked = "Delete";
            sLogOldValue = dgArea.CurrentRow.Cells["PROV_CODE"].Value.ToString() + "/" +
                            dgArea.CurrentRow.Cells["SHORTNAME"].Value.ToString() + "/" +
                            dgArea.CurrentRow.Cells["FULLNAME"].Value.ToString() + "/" +
                            dgArea.CurrentRow.Cells["CITAD_MEMBER"].Value.ToString();
            iResult = objAREAController.DeleteAREA(Convert.ToInt32(dgArea.CurrentRow.Cells[0].Value.ToString()));

            if (iResult == 1)
            { 
                //goi ham ghilog
                objLog.GhiLogUser(tLogDate, Common.Userid, sLogContent, iLogLevel, 
                    sLogWorked, sLogTable, sLogOldValue, sLogNewValue);                
                return true;
            }
            else
                return false;

        }
        
        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)          
                this.Close();          
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);
                if ((this.ActiveControl) is Button)                
                    return;                
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
                        
        private void txtProvinceCode_KeyPress(object sender, KeyPressEventArgs e)
        {        
            if (!Common.IsNumeric(e.KeyChar.ToString()))
                e.Handled = true;
        }
        
        private void frmArea_Load(object sender, EventArgs e)
        {   
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                iSadd = false;
                iSupdate = false;
                //Load Data combobox
                if (!objGetData.FillDataComboBox(cboType, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='IBPS' AND CDNAME='AREA'", "CONTENT", true,false,""))
                    return;
                //Load datagridview
                LoadDataGridSearch(false);
                RefreshControl();                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {            
            LoadDataGridSearch(true);
            if (dgArea.Rows.Count == 0)
            {
                Common.ShowError("There is no suitable area!", 1, MessageBoxButtons.OK);                
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            iSadd = true;
            RefreshControl();                        
        }       

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                iSupdate = true;
                RefreshControl();                
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
                if (Common.iSconfirm == 0)                                    
                {
                    cmdSave.Enabled = false;
                    return;   
                }
                if (dgArea.RowCount == 0)
                {
                    cmdSave.Enabled = false;
                    return;
                }
                if (dgArea.CurrentRow ==null)
                {                    
                    Common.ShowError("Please choose a province to delete!",1,MessageBoxButtons.OK);
                    return;
                }
                if (Delete())
                {
                    Common.ShowError("Data has been deleted!", 1, MessageBoxButtons.OK);
                    LoadDataGridSearch(false);
                    RefreshControl();                                     
                }
                else
                {
                    Common.ShowError("Error occured when deleting data!", 2, MessageBoxButtons.OK);                    
                }
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
                if (Common.iSconfirm == 0)
                {
                    iSadd = false;
                    iSupdate = false;
                    RefreshControl();
                    return;
                }
                //Kiem tra data truoc khi save
                if (!Verify())
                {
                    cmdSave.Enabled = true;
                    return;
                }
                if (!SaveData())
                {                    
                    Common.ShowError("Error occured when inserting data!", 2, MessageBoxButtons.OK);
                    return;
                }
                Common.ShowError("Save successfully!", 1, MessageBoxButtons.OK);
                //LoadDatagrid();
                LoadDataGridSearch(false);
                RefreshControl();                
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
            
        private void frmArea_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cmdSave.Enabled)
                return;
            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
           
        }
               
        private void txtShortname_Leave(object sender, EventArgs e)
        {
           txtShortname.Text = clsCheck.ConvertVietnamese(txtShortname.Text);         
        }

        private void txtFullName_Leave(object sender, EventArgs e)
        {
           txtFullName.Text = clsCheck.ConvertVietnamese(txtFullName.Text);
        }

        private void txtProvinceCode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProvinceCode.Text.Trim()) || cmdSave.Enabled == false)           
                return;          
            else
            {                
                string strProvinCode = "";
                strProvinCode = " trim(Area.PROV_CODE)='"+ txtProvinceCode.Text.Trim() +"'";

                DataSet dsArea = new DataSet();
                dsArea = objAREAController.GetAREA(strProvinCode);
                if (dsArea.Tables[0].Rows.Count==0 || dsArea==null)
                    return;               
                else
                {
                    iID=Convert.ToInt32(dsArea.Tables[0].Rows[0]["ID"].ToString());
                    txtShortname.Text = dsArea.Tables[0].Rows[0]["SHORTNAME"].ToString();
                    txtFullName.Text = dsArea.Tables[0].Rows[0]["FULLNAME"].ToString();
                    cboType.Text = dsArea.Tables[0].Rows[0]["CITAD_MEMBER"].ToString();                    
                }
            }
        }

        private void frmArea_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        
        private void dgArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgArea.Rows.Count == 0)
                    return;
                if (dgArea.CurrentRow == null)
                {
                    txtProvinceCode.Text = dgArea.Rows[0].Cells["PROV_CODE"].Value.ToString();
                    txtShortname.Text = dgArea.Rows[0].Cells["SHORTNAME"].Value.ToString();
                    txtFullName.Text = dgArea.Rows[0].Cells["FULLNAME"].Value.ToString();
                    cboType.SelectedValue = dgArea.Rows[0].Cells["CITAD_MEMBER"].Value.ToString();                    
                }
                else
                {
                    txtProvinceCode.Text = dgArea.CurrentRow.Cells["PROV_CODE"].Value.ToString();
                    txtShortname.Text = dgArea.CurrentRow.Cells["SHORTNAME"].Value.ToString();
                    txtFullName.Text = dgArea.CurrentRow.Cells["FULLNAME"].Value.ToString();
                    cboType.SelectedValue = dgArea.CurrentRow.Cells["CITAD_MEMBER"].Value.ToString();                    
                }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);                
            }
        }

        private void dgArea_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            iSadd = false;
            iSupdate = false;
            RefreshControl();
        }
        
    }
}
