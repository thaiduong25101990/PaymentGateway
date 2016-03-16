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

namespace BR.BRSYSTEM
{
    public partial class frmMSGDef : frmBasedata
    {
        #region ham ,bien
        private int iFieldID = 0;
        private bool isInsert = false;
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();        
        private DataSet datDs = new DataSet();
        private MSG_DEFInfo objInfo = new MSG_DEFInfo();
        private MSG_DEFController objControl = new MSG_DEFController();
        private clsCheckInput clsCheck = new clsCheckInput();        
        private ALLCODEInfo objallcode = new ALLCODEInfo();
        private ALLCODEController objctrolall = new ALLCODEController();      
        private string FieldID1 = "";        
        private string MsgID1 = "";
        private string FieldName1 = "";
        private string FieldCode1 = "";
        private string FieldDescription1 = "";
        private string SIBSPosition1 = "";
        private string GWPosition1 = "";
        private string Length1 = "";
        private string DataType1 = "";
        private string DefaultValue1 = "";        
        private string CHK1 = "";
        #endregion

        public frmMSGDef()
        {
            InitializeComponent();
        }

        private void frmMSGDef_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            //Load data combobox cboDataType
            if (!objGetData.FillDataComboBox(cboDataType, "CONTENT", "CDVAL", "allcode",
                "CDNAME='DATATYPE'", "CONTENT", true, false, ""))
                return;
            //Load data combobox cboDataType
            if (!objGetData.FillDataComboBox(cboMSGDefID, "MSG_DEF_ID", "MSG_DEF_ID", "MSG_DEF",
                "", "MSG_DEF_ID", true, true, "ALL"))
                return;
            if (!objGetData.FillDataComboBox(txtMsgID, "MSG_DEF_ID", "MSG_DEF_ID", "MSG_DEF",
             "", "MSG_DEF_ID", true,false,""))
                return;
            EnableTextbox(false);
            CommandStatus(true);
        }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSave.Enabled = !a;
            cmdCancel.Enabled = !a;
            grdList.Enabled = a;
        }

       
        private void ClearText()
        {            
            txtDefaultValue.Text = "";
            txtFieldCode.Text = "";
            txtFieldDescription.Text = "";
            txtFieldName.Text = "";
            txtGWPosition.Text = "";
            txtLength.Text = "";         
            txtSIBSPosition.Text = "";
            txtMsgID.Text = "";
            cboMSGDefID.Text = "";
            cboMSGDefID.Focus();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isInsert = true;
            EnableTextbox(true);
            ClearText();
            cboMSGDefID.Enabled = false;
            CommandStatus(false);
            txtMsgID.Focus();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                isInsert = false;
                EnableTextbox(true);
                CommandStatus(false);
                cboMSGDefID.Enabled = false;
                txtMsgID.Focus();                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (BR.BRLib.Common.iSconfirm == 1)
            {
                iFieldID = int.Parse(grdList.CurrentRow.Cells[0].Value.ToString());                
                objControl.DeleteMSG_DEF(iFieldID);
                Common.ShowError("Data has delete successfully!", 7, MessageBoxButtons.OK);                
                //lay du lieu de ghi log
                DateTime dtDateLogin = DateTime.Now;
                string strContent = "Host message field standard";
                int iLoglevel = 1;
                string strWorked = "Delete";
                string strTable = "MSG_DEF";
                string strOld_value = FieldID1 + "/" + MsgID1 + "/" + FieldName1 + "/" + 
                    FieldCode1 + "/" + FieldDescription1 + "/" + SIBSPosition1 + "/" + 
                    Length1 + "/" + DataType1 + "/" + DefaultValue1 + "/" + CHK1;
                string strNew_value = "";
                //goi ham ghilog
                objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, 
                    iLoglevel, strWorked, strTable, strOld_value, strNew_value); 
                //------------------------------------------------------------------------------------------------
                //LoadData();
                Load_data_combobox(cboMSGDefID.Text.Trim());
                CommandStatus(true);
            }
            else {                
                ClearText();
                EnableTextbox(false);
                CommandStatus(true);
                return;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (BR.BRLib.Common.iSconfirm == 1)
                {
                    if (String.IsNullOrEmpty(txtMsgID.Text.Trim()) || 
                        String.IsNullOrEmpty(txtFieldName.Text.Trim()))
                    {                        
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                        txtFieldName.Focus();  
                        cmdSave.Enabled = true;
                        return;
                    }
                    objInfo = new BR.BRBusinessObject.MSG_DEFInfo();
                    objInfo.FIELD_ID = iFieldID;
                    objInfo.MSG_DEF_ID = clsCheck.ConvertVietnamese(txtMsgID.Text.Trim());
                    objInfo.FIELD_NAME = clsCheck.ConvertVietnamese(txtFieldName.Text.Trim());
                    objInfo.FIELD_DESCRIPTION = clsCheck.ConvertVietnamese(txtFieldDescription.Text.Trim());
                    objInfo.FIELD_CODE = clsCheck.ConvertVietnamese(txtFieldCode.Text.Trim());
                    if (txtSIBSPosition.Text.Trim() != "") 
                    {
                        objInfo.SIBS_POS = Convert.ToInt32(clsCheck.ConvertVietnamese(txtSIBSPosition.Text.Trim()));
                    }
                    if (txtGWPosition.Text.Trim()!="")
                    {
                        objInfo.GW_POS = Convert.ToInt32(clsCheck.ConvertVietnamese(txtGWPosition.Text.Trim()));
                    }
                    else { objInfo.GW_POS = 0; }
                    if (cboDataType.SelectedValue.ToString()!="")
                    {
                        objInfo.LENGTH = Convert.ToInt32(txtLength.Text);
                    }
                    else { objInfo.LENGTH = 0; }
                    if (cboDataType.SelectedValue.ToString()!="")
                    {
                        objInfo.DATA_TYPE = Convert.ToInt32(cboDataType.SelectedValue.ToString());
                    }
                    else { objInfo.DATA_TYPE = 0; }
                     objInfo.DEFAULT_VALUE = clsCheck.ConvertVietnamese(txtDefaultValue.Text.Trim());
                    //chuong trinh hien tai SIBS_FIELD_CODE = Field Name
                    objInfo.SIBS_FIELD_CODE = clsCheck.ConvertVietnamese(txtFieldName.Text.Trim());
                    objInfo.CHK = txtCHK.Text.Trim();

                    if (isInsert)
                    {
                        if (GetData.IDIsExisting(false, "MSG_DEF", "MSG_DEF_ID", cboMSGDefID.Text.Trim(), ""))
                        {                            
                            Common.ShowError("Message definition ID has already exist!", 3, MessageBoxButtons.OK);
                            cboMSGDefID.Text = "";
                            cboMSGDefID.Enabled = true;
                            cboMSGDefID.Focus();                            
                            CommandStatus(false);
                            return;
                        }
                        else if (!CheckID())
                        {
                            return;
                        }
                        objControl.AddMSG_DEF(objInfo);
                        Common.ShowError("Data has inserted successfully!", 7, MessageBoxButtons.OK);                        
                        CommandStatus(true);
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "SIBS message field standard";
                        int iLoglevel = 1;
                        string strWorked = "Insert";
                        string strTable = "MSG_DEF";
                        string strOld_value = "";
                        string strNew_value = objInfo.FIELD_ID + "/" + objInfo.MSG_DEF_ID + 
                            "/" + objInfo.FIELD_NAME + "/" + objInfo.FIELD_DESCRIPTION + "/" + 
                            objInfo.FIELD_CODE + "/" + objInfo.SIBS_POS + "/" + objInfo.GW_POS + 
                            "/" + objInfo.LENGTH + "/" + objInfo.DATA_TYPE + "/" + 
                            objInfo.DEFAULT_VALUE + "/" + objInfo.CHK;                        
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel, 
                            strWorked, strTable, strOld_value, strNew_value);                                               
                    }
                    else 
                    {
                        if (!CheckID())
                        {
                            return;
                        }
                        objControl.UpdateMSG_DEF(objInfo);
                        Common.ShowError("Data has inserted successfully!", 7, MessageBoxButtons.OK);                        
                        CommandStatus(true);
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "SIBS message field standard";
                        int iLoglevel = 1;
                        string strWorked = "Update";
                        string strTable = "MSG_DEF";
                        string strOld_value = FieldID1 + "/" + MsgID1 + "/" + FieldName1 + "/" + 
                            FieldDescription1 + "/" + FieldCode1 + "/" + SIBSPosition1 + "/" + 
                            GWPosition1 +"/"+ Length1 + "/" + DataType1 + "/" + DefaultValue1 + "/"+ CHK1;
                        string strNew_value = objInfo.FIELD_ID + "/" + objInfo.MSG_DEF_ID + 
                            "/" + objInfo.FIELD_NAME + "/" + objInfo.FIELD_DESCRIPTION + 
                            "/" + objInfo.FIELD_CODE + "/" + objInfo.SIBS_POS + "/" + 
                            objInfo.GW_POS + "/" + objInfo.LENGTH + "/" + objInfo.DATA_TYPE + 
                            "/" + objInfo.DEFAULT_VALUE + "/" + objInfo.CHK;                        
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel, 
                            strWorked, strTable, strOld_value, strNew_value); 
                    }
                    //LoadData();
                    Load_data_combobox(cboMSGDefID.Text.Trim());
                    ClearText();                    
                    CommandStatus(true);
                    EnableTextbox(false);
                    cboMSGDefID.Enabled = true;
                    isInsert = false;
                }
                else
                {
                    CommandStatus(true);
                    cboMSGDefID.Enabled = true;
                    ClearText();
                    EnableTextbox(false);
                    return;
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

        private void EnableTextbox(Boolean a)
        {
            txtFieldName.Enabled = a;
            txtFieldCode.Enabled = a;
            txtFieldDescription.Enabled = a;
            txtDefaultValue.Enabled = a;
            cboDataType.Enabled = a;
            txtSIBSPosition.Enabled = a;
            txtGWPosition.Enabled = a;
            txtLength.Enabled = a;
            txtCHK.Enabled = a;
            txtMsgID.Enabled = a;
            //cboMSGDefID.Enabled = !a;
        }                

       
                
        
        private void txtMsgDefID_Validated(object sender, EventArgs e)
        {
            if (cboMSGDefID.Text.Length > 15)
            {
                Common.ShowError("The Bridge message maximium length is 15", 3, MessageBoxButtons.OK);                
                cboMSGDefID.Text = "";
                cboMSGDefID.Focus();
            }
        }
                

        #region Kiem tra Ma Message Definition ID da co trong DB chua
        private bool CheckID()
        {
            bool result = true;
            string ID = txtMsgID.Text.Trim();
            if (String.IsNullOrEmpty(ID))
            {
                Common.ShowError("You input Message definition ID", 3, MessageBoxButtons.OK);
                result = false;
            }
            else if (ID.Length > 15)
            {                
                Common.ShowError("Message Name Maximium length is 15", 3, MessageBoxButtons.OK);
                result = false;
            }
            return result;
        }
        #endregion

        private void cboMSGDefID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMSGDefID.SelectedIndex < 0)
                return;
            if (cboMSGDefID.Text.Trim() == "ALL")
            {
                Load_data_combobox(cboMSGDefID.Text.Trim());
            }
            else
               {
                 string strMsgDefID = cboMSGDefID.Text.Trim();
                 try
                 {
                     Load_data_combobox(strMsgDefID);
                 }
                 catch (Exception ex)
                 {
                     Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                 }
            }
        }

        private void Load_data_combobox(string strMsgDefID)
        {
            try
            {
                datDs = new DataSet();
                if (cboMSGDefID.Text.Trim()=="ALL")
                   datDs= objControl.GetMSG_DEF();
                else
                    datDs = objControl.GetMSGDef_MsgID(strMsgDefID);
                grdList.ClearSelection();

                grdList.DataSource = datDs.Tables[0];
                grdList.Columns["FIELDID"].Visible = false;
                grdList.Columns["MSG_DEF_ID"].HeaderText = "MSG DEF ID";
                grdList.Columns["MSG_DEF_ID"].Width = 150;
                grdList.Columns["FIELD_NAME"].HeaderText = "Field name";
                grdList.Columns["FIELD_NAME"].Width = 100;
                grdList.Columns["FIELD_CODE"].HeaderText = "Field code";
                grdList.Columns["FIELD_CODE"].Width = 80;
                grdList.Columns["DESCRIPTION"].HeaderText = "Decription";
                grdList.Columns["DESCRIPTION"].Width = 150;
                grdList.Columns["SIBS_POS"].HeaderText = "Host Pos";
                grdList.Columns["SIBS_POS"].Width = 100;
                grdList.Columns["GW_POS"].HeaderText = "Bridge Pos";
                grdList.Columns["GW_POS"].Width = 70;
                grdList.Columns["LENGTH"].HeaderText = "Length";
                grdList.Columns["LENGTH"].Width = 70;
                grdList.Columns["DATA_TYPE"].HeaderText = "Data type";
                grdList.Columns["DATA_TYPE"].Width = 100;
                grdList.Columns["DEFAULT_VALUE"].HeaderText = "Default value";
                grdList.Columns["DEFAULT_VALUE"].Width = 80;
                grdList.Columns["CHK"].HeaderText = "CHK";
                grdList.Columns["CHK"].Width = 50;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

       



        private void LoadText(int intRow)
        {
            try
            {
                iFieldID = Convert.ToInt32(grdList.Rows[intRow].Cells["FIELDID"].Value.ToString().Trim());
                //cboMSGDefID.Text = grdList.Rows[intRow].Cells["MSG_DEF_ID"].Value.ToString().Trim();
                txtMsgID.Text = grdList.Rows[intRow].Cells["MSG_DEF_ID"].Value.ToString().Trim();
                txtFieldName.Text = grdList.Rows[intRow].Cells["FIELD_NAME"].Value.ToString().Trim();
                txtFieldCode.Text = grdList.Rows[intRow].Cells["FIELD_CODE"].Value.ToString().Trim();
                txtFieldDescription.Text = grdList.Rows[intRow].Cells["DESCRIPTION"].Value.ToString().Trim();
                txtSIBSPosition.Text = grdList.Rows[intRow].Cells["SIBS_POS"].Value.ToString().Trim();
                txtGWPosition.Text = grdList.Rows[intRow].Cells["GW_POS"].Value.ToString().Trim();
                txtLength.Text = grdList.Rows[intRow].Cells["LENGTH"].Value.ToString().Trim();
                cboDataType.Text = grdList.Rows[intRow].Cells["DATA_TYPE"].Value.ToString().Trim();
                txtDefaultValue.Text = grdList.Rows[intRow].Cells["DEFAULT_VALUE"].Value.ToString().Trim();
                txtCHK.Text = grdList.Rows[intRow].Cells["CHK"].Value.ToString().Trim();
                //quynd Update20081120------------------------------------------------------------
                FieldID1 = grdList.Rows[intRow].Cells["FIELDID"].Value.ToString().Trim();
                MsgID1 = grdList.Rows[intRow].Cells["MSG_DEF_ID"].Value.ToString().Trim();
                FieldName1 = grdList.Rows[intRow].Cells["FIELD_NAME"].Value.ToString().Trim();
                FieldCode1 = grdList.Rows[intRow].Cells["FIELD_CODE"].Value.ToString().Trim();
                FieldDescription1 = grdList.Rows[intRow].Cells["DESCRIPTION"].Value.ToString().Trim();
                SIBSPosition1 = grdList.Rows[intRow].Cells["SIBS_POS"].Value.ToString().Trim();
                GWPosition1 = grdList.Rows[intRow].Cells["GW_POS"].Value.ToString().Trim();
                Length1 = grdList.Rows[intRow].Cells["LENGTH"].Value.ToString().Trim();
                DataType1 = grdList.Rows[intRow].Cells["DATA_TYPE"].Value.ToString().Trim();
                DefaultValue1 = grdList.Rows[intRow].Cells["DEFAULT_VALUE"].Value.ToString().Trim();
                CHK1 = grdList.Rows[intRow].Cells["CHK"].Value.ToString().Trim();
                //--------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 3, MessageBoxButtons.OK);
            }

        }
        private void grdList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;

                iFieldID = Convert.ToInt32(grdList.CurrentRow.Cells[0].Value.ToString());
                int intRow = e.RowIndex;
                LoadText(intRow);
            
           
        }

     

        private void frmMSGDef_KeyDown(object sender, KeyEventArgs e)
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
                    //Goi su kien Click, nhung chua lam duoc                    
                    if ((this.ActiveControl as Button).Name == "cmdSave")                    
                        cmdSave.Focus();                        
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
            CommandStatus(true);
            cboMSGDefID.Enabled = true;
            grdList.Focus();
            Load_data_combobox(cboMSGDefID.Text.Trim());
            EnableTextbox(false);
        }

        private void txtCHK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);                
            }
        }

        private void txtSIBSPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;                
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);
            }
        }

        private void txtGWPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;                
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);
            }
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;                
                Common.ShowError("You must input number!", 3, MessageBoxButtons.OK);
            }
        }

        private void frmMSGDef_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        
    }

}

