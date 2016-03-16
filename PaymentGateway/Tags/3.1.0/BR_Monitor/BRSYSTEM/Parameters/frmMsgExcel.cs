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
    public partial class frmMsgExcel : frmBasedata
    {
        #region khai bao cac ham va cac tham so,bien
        
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private DataSet datDs = new DataSet();
        private MSG_EXCELInfo objInfo = new MSG_EXCELInfo();
        private MSG_EXCELController objControl = new MSG_EXCELController();
        private ALLCODEInfo objallcode = new ALLCODEInfo();
        private ALLCODEController objctrolall = new ALLCODEController();
        private clsCheckInput clsCheck = new clsCheckInput();

        private int iFieldID = 0;
        private bool isInsert = false;
        //private string strTemp = "";        
        private string GWType = "";
        private string MsgType = "";
        private string ExcelCol = "";
        private string FieldName = "";
        private string FieldDecrip = "";
        private string RowBegin = "";
        private string MaxRow = "";
        private string MaxLength = "";
        private string Check = "";
        private string DataType = "";
        private string SwiftField = "";
        private string Direction = "";
        private string Default = "";
        private string RowNum = "";
        private string PartNum = "";
        #endregion

        public frmMsgExcel()
        {
            InitializeComponent();
        }


        private void ClearText()
        {
            cboGWType.Text = "";
            txtMsgType.Text = "";
            txtExcelCol.Text = "";
            txtFieldName.Text = "";
            txtFieldDecrip.Text = "";
            txtRowBegin.Text = "";
            txtMaxRow.Text = "";
            txtMaxLength.Text = "";
            cboCheck.Text = "";
            cboDataType.Text = "";
            txtSwiftField.Text = "";
            cboDirection.Text = "";
            txtDefault.Text = "";
            txtRowNum.Text = "";
            txtPartNum.Text = "";
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LockTextbox(false);
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                cmdDelete.Enabled = false;
                cmdEdit.Enabled = false;
                cmdAdd.Enabled = false;
                dgdListMsg.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void LoadData()
        {
            try
            {
                datDs = new DataSet();
                datDs = objControl.GetMSG_EXCEL();
                dgdListMsg.DataSource = datDs.Tables[0];
                FormatGridView();
                if (datDs.Tables[0].Rows.Count <= 0)
                {
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdAdd.Enabled = false;
                    //dgdListMsg.DataSource = null;
                    ClearText();
                    dgdListMsg.Enabled = false;
                }
                else
                {
                    dgdListMsg.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdAdd.Enabled = true;
                    dgdListMsg.SelectedRows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void FormatGridView()
        {
            dgdListMsg.RowHeadersVisible = true;
            dgdListMsg.Columns[0].Visible = false;
            dgdListMsg.Columns[1].HeaderText = "Channel";
            dgdListMsg.Columns[1].Width = 100;
            dgdListMsg.Columns[2].HeaderText = "Message type";
            dgdListMsg.Columns[2].Width = 100;
            dgdListMsg.Columns[3].HeaderText = "Excel column";
            dgdListMsg.Columns[3].Width = 70;
            dgdListMsg.Columns[4].HeaderText = "Field name";
            dgdListMsg.Columns[4].Width = 150;
            dgdListMsg.Columns[5].HeaderText = "Description";
            dgdListMsg.Columns[5].Width = 200;
            dgdListMsg.Columns[6].HeaderText = "Row begin";
            dgdListMsg.Columns[6].Width = 70;
            dgdListMsg.Columns[7].HeaderText = "Max row";
            dgdListMsg.Columns[7].Width = 70;
            dgdListMsg.Columns[8].HeaderText = "Max length";
            dgdListMsg.Columns[8].Width = 70;
            dgdListMsg.Columns[9].HeaderText = "Check";
            dgdListMsg.Columns[9].Width = 70;
            dgdListMsg.Columns[10].HeaderText = "Data type";
            dgdListMsg.Columns[10].Width = 100;
            dgdListMsg.Columns[11].HeaderText = "Swift field name";
            dgdListMsg.Columns[11].Width = 100;
            dgdListMsg.Columns[12].HeaderText = "Direction";
            dgdListMsg.Columns[12].Width = 100;
            dgdListMsg.Columns[13].HeaderText = "Default value";
            dgdListMsg.Columns[13].Width = 100;
            dgdListMsg.Columns[14].HeaderText = "Row num";
            dgdListMsg.Columns[14].Width = 70;
            dgdListMsg.Columns[15].HeaderText = "Part num";
            dgdListMsg.Columns[15].Width = 70;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search_data();
        }

        private void Search_data()
        {
            try
            {
                string strGWType = cboGWTypeSearch.Text.Trim();
                string strMsgType = clsCheck.ConvertVietnamese(txtMsgTypeSearch.Text.Trim());
                string strDirection = cboSDirection.Text.Trim();

                datDs = new DataSet();
                if (strGWType.Trim() == "ALL")
                    strGWType = "%";
                //if (String.IsNullOrEmpty(strMsgType))
                //    datDs = objControl.GetMSG_EXCEL_GWType(strGWType);
                //else
                //    datDs = objControl.GetMSG_EXCELSearch(strGWType, strMsgType);
                if (String.IsNullOrEmpty(strMsgType))
                    strMsgType = "%";
                datDs = objControl.EXCELSearch(strGWType, strMsgType, strDirection);

                dgdListMsg.DataSource = datDs.Tables[0];
                FormatGridView();
                if (datDs.Tables[0].Rows.Count <= 0)
                {
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    dgdListMsg.Enabled = false;
                    ClearText();
                }
                else
                {
                    dgdListMsg.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    dgdListMsg.SelectedRows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMsgExcel_Load_1(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            isInsert = true;
            //Load data cboGWTypeSearch
            if (!objGetData.FillDataComboBox(cboGWTypeSearch, "CONTENT", "CDVAL", "allcode",
                "CDNAME='GWTYPE'", "CONTENT", true, true, "ALL"))
                return;
            //Load data cboGWType
            if (!objGetData.FillDataComboBox(cboGWType, "CONTENT", "CDVAL", "allcode",
                "CDNAME='GWTYPE'", "CONTENT", true, false, ""))
                return;
            //Load data cboCheck
            if (!objGetData.FillDataComboBox(cboCheck, "CONTENT", "CDVAL", "allcode",
                "CDNAME='MSGCHK'", "CONTENT", true, false, ""))
                return;
            //Load data cboDataType
            if (!objGetData.FillDataComboBox(cboDataType, "CONTENT", "CDVAL", "allcode",
                "CDNAME='DATATYPE'", "CONTENT", true, false, ""))
                return;
            //Load data cboDirection
            if (!objGetData.FillDataComboBox(cboDirection, "CONTENT", "CDVAL", "allcode",
                "CDNAME='MSGDirection'", "CONTENT", true, false, ""))
                return;            
            LoadData();
            dgdListMsg.Focus();
            LockTextbox(true);
        }

        private void GetCombo(string strCdname, ComboBox cboALLCODE)
        {
            try
            {
                DataSet dtsGetCombo = new DataSet();
                dtsGetCombo = objctrolall.GetALLCODE_D(strCdname);
                cboALLCODE.DataSource = dtsGetCombo.Tables[0];
                cboALLCODE.DisplayMember = "CONTENT";
                cboALLCODE.ValueMember = "CDVAL";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LockTextbox(Boolean a)
        {
            txtRowBegin.ReadOnly = a;
            txtMsgType.ReadOnly = a;
            txtMaxRow.ReadOnly = a;
            txtExcelCol.ReadOnly = a;
            txtMaxLength.ReadOnly = a;
            txtFieldName.ReadOnly = a;
            cboCheck.Enabled = !a;
            txtFieldDecrip.ReadOnly = a;
            cboDataType.Enabled = !a;
            txtSwiftField.ReadOnly = a;
            cboDirection.Enabled = !a;
            txtDefault.ReadOnly = a;
            txtRowNum.ReadOnly = a;
            txtPartNum.ReadOnly = a;
            cboGWType.Enabled = !a;
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click_2(object sender, EventArgs e)
        {
            isInsert = true;
            ClearText();
            LockTextbox(false);
            cmdAdd.Enabled = false;
            cmdEdit.Enabled = false;
            cmdSave.Enabled = true;
            cmdCancel.Enabled = true;
            cmdDelete.Enabled = false;
            cmdSearch.Enabled = false;
            dgdListMsg.Enabled = false;
            cboGWType.Focus();
        }

        private void cmdEdit_Click_1(object sender, EventArgs e)
        {
            isInsert = false;
            LockTextbox(false);
            cmdAdd.Enabled = false;
            cmdEdit.Enabled = false;
            cmdSave.Enabled = true;
            cmdCancel.Enabled = true;
            cmdDelete.Enabled = false;
            cmdSearch.Enabled = false;
            dgdListMsg.Enabled = false;
            cboGWType.Focus();
        }

        private void cmdDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    if (dgdListMsg.Rows.Count != 0)
                    {
                        iFieldID = int.Parse(dgdListMsg.CurrentRow.Cells[0].Value.ToString());
                        if (objControl.DeleteMSG_EXCEL(iFieldID) == 1)
                        {
                            Common.ShowError("Delete successfully!", 1, MessageBoxButtons.OK);                            
                            //lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "Excel message";
                            int iLoglevel = 1;
                            string strWorked = "Delete";
                            string strTable = "MSG_XLS";
                            string strOld_value = GWType + "/" + MsgType + "/" + ExcelCol + 
                                "/" + FieldName + "/" + FieldDecrip + "/" + RowBegin + "/" + 
                                MaxRow + "/" + MaxLength + "/" + Check + "/" + DataType + "/" + 
                                SwiftField + "/" + Direction + "/" + Default + "/" + RowNum + 
                                "/" + PartNum;
                            string strNew_value = "";
                            //goi ham ghilog
                            objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel,
                                strWorked, strTable, strOld_value, strNew_value);
                        }
                    }
                    LoadData();
                    #region enable cac controls
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdDelete.Enabled = true;
                    cmdSearch.Enabled = true;
                    dgdListMsg.Enabled = true;
                    #endregion
                }
                else
                {
                    #region Enable cac control
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdDelete.Enabled = true;
                    cmdCancel.Enabled = false;
                    cmdSearch.Enabled = true;
                    dgdListMsg.Enabled = true;
                    #endregion
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    if (String.IsNullOrEmpty(cboGWType.Text.Trim()) || 
                        String.IsNullOrEmpty(txtMsgType.Text.Trim()))
                    {                        
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        cmdCancel.Enabled = true;
                        cmdDelete.Enabled = false;
                        cmdAdd.Enabled = false;
                        cmdEdit.Enabled = false;
                        cmdSearch.Enabled = false;
                        dgdListMsg.Enabled = false;
                        return;
                    }

                    objInfo = new MSG_EXCELInfo();
                    objInfo.FIELD_ID = iFieldID;
                    objInfo.GWTYPE = cboGWType.Text.Trim();
                    objInfo.MSG_TYPE = clsCheck.ConvertVietnamese(txtMsgType.Text.Trim());
                    objInfo.XLSCOL = clsCheck.ConvertVietnamese(txtExcelCol.Text.Trim());
                    objInfo.FIELD_NAME = clsCheck.ConvertVietnamese(txtFieldName.Text.Trim());
                    objInfo.FIELD_DECRIPTION = clsCheck.ConvertVietnamese(txtFieldDecrip.Text.Trim());
                    objInfo.CHK = cboCheck.SelectedValue.ToString();
                    string strTemp;
                    if (String.IsNullOrEmpty(txtRowBegin.Text.Trim()))
                    {
                        objInfo.ROW_BEGIN = 0;
                    }
                    else
                    {
                        strTemp = txtRowBegin.Text.Trim();
                        objInfo.ROW_BEGIN = Convert.ToInt32(strTemp);
                    }

                    if (String.IsNullOrEmpty(txtMaxRow.Text.Trim()))
                    {
                        objInfo.MAX_ROW = 0;
                    }
                    else
                    {
                        strTemp = txtMaxRow.Text.Trim();
                        objInfo.MAX_ROW = Convert.ToInt32(strTemp);
                    }
                    //objInfo.MAX_ROW = Convert.ToInt32(txtMaxRow.Text.Trim());
                    if (String.IsNullOrEmpty(txtMaxLength.Text.Trim()))
                    {
                        objInfo.MAX_LENGTH = 0;
                    }
                    else
                    {
                        strTemp = txtMaxLength.Text.Trim();
                        objInfo.MAX_LENGTH = Convert.ToInt32(strTemp);
                    }
                    //objInfo.MAX_LENGTH = Convert.ToInt32(txtMaxLength.Text.Trim());
                    objInfo.DATA_TYPE = cboDataType.SelectedValue.ToString();
                    objInfo.SWIFT_FIELD_NAME = clsCheck.ConvertVietnamese(txtSwiftField.Text.Trim());
                    objInfo.MSG_DIRECTION = cboDirection.Text.Trim();
                    objInfo.DEFAULT_VALUE = clsCheck.ConvertVietnamese(txtDefault.Text.Trim());
                    if (String.IsNullOrEmpty(txtRowNum.Text.Trim()))                    
                        objInfo.ROW_NUM = 0;                    
                    else
                    {
                        strTemp = txtRowNum.Text.Trim();
                        objInfo.ROW_NUM = Convert.ToInt32(strTemp);
                    }                    
                    if (String.IsNullOrEmpty(txtPartNum.Text.Trim()))                    
                        objInfo.PART_NUM = 0;                    
                    else
                    {
                        strTemp = txtPartNum.Text.Trim();
                        objInfo.PART_NUM = Convert.ToInt32(strTemp);
                    }                    

                    if (isInsert)
                    {
                        objControl.AddMSG_EXCEL(objInfo);
                        Common.ShowError("Insert successful!", 1, MessageBoxButtons.OK);                        
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdSave.Enabled = false;
                        cmdCancel.Enabled = false;
                        cmdSearch.Enabled = true;
                        dgdListMsg.Enabled = true;
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "Excel message";
                        int iLoglevel = 1;
                        string strWorked = "Insert";
                        string strTable = "MSG_XLS";
                        string strOld_value = "";
                        string strNew_value = objInfo.FIELD_ID + "/" + objInfo.GWTYPE + "/" + 
                            objInfo.MSG_TYPE + "/" + objInfo.XLSCOL + "/" + objInfo.FIELD_NAME + 
                            "/" + objInfo.FIELD_DECRIPTION + "/" + objInfo.CHK + "/" + 
                            objInfo.ROW_BEGIN + "/" + objInfo.MAX_ROW + "/" + objInfo.MAX_LENGTH + 
                            "/" + objInfo.DATA_TYPE + "/" + objInfo.SWIFT_FIELD_NAME + "/" + 
                            objInfo.MSG_DIRECTION + "/" + objInfo.DEFAULT_VALUE + "/" + 
                            objInfo.ROW_NUM + "/" + objInfo.PART_NUM;
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel,
                            strWorked, strTable, strOld_value, strNew_value);
                    }
                    else
                    {
                        objControl.UpdateMSG_EXCEL(objInfo);
                        Common.ShowError("Update successful!", 1, MessageBoxButtons.OK);                        
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdSave.Enabled = false;
                        cmdCancel.Enabled = false;
                        cmdSearch.Enabled = true;
                        dgdListMsg.Enabled = true;
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "Excel message";
                        int iLoglevel = 1;
                        string strWorked = "Update";
                        string strTable = "MSG_XLS";
                        string strOld_value = objInfo.FIELD_ID + "/" + GWType + "/" + MsgType + 
                            "/" + ExcelCol + "/" + FieldName + "/" + FieldDecrip + "/" + 
                            Check + "/" + RowBegin + "/" + MaxRow + "/" + MaxLength + "/" + 
                            DataType + "/" + SwiftField + "/" + Direction + "/" + Default + "/" + 
                            RowNum + "/" + PartNum;
                        string strNew_value = objInfo.FIELD_ID + "/" + objInfo.GWTYPE + "/" + 
                            objInfo.MSG_TYPE + "/" + objInfo.XLSCOL + "/" + objInfo.FIELD_NAME + 
                            "/" + objInfo.FIELD_DECRIPTION + "/" + objInfo.CHK + "/" + 
                            objInfo.ROW_BEGIN + "/" + objInfo.MAX_ROW + "/" + objInfo.MAX_LENGTH + 
                            "/" + objInfo.DATA_TYPE + "/" + objInfo.SWIFT_FIELD_NAME + "/" + 
                            objInfo.MSG_DIRECTION + "/" + objInfo.DEFAULT_VALUE + "/" + 
                            objInfo.ROW_NUM + "/" + objInfo.PART_NUM;
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel,
                            strWorked, strTable, strOld_value, strNew_value);
                    }
                    //LoadData();
                    Search_data();
                    LockTextbox(true);
                }
                else
                {
                    ClearText();
                    #region Enable cac controls
                    LockTextbox(true);
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    dgdListMsg.Enabled = true;
                    #endregion
                    return;
                }
                ClearText();
                #region Enable cac controls
                cmdAdd.Enabled = true;
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdSave.Enabled = false;
                cmdCancel.Enabled = false;
                dgdListMsg.Enabled = true;
                dgdListMsg.Select();
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void dgdListMsg_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            isInsert = false;
            
            //-----------------------------------------------------------------
        }

        private void dgdListMsg_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            isInsert = false;
            ViewGrid();
         
        }

        private void ViewGrid()
        {
            try
            {
                iFieldID = int.Parse(dgdListMsg.CurrentRow.Cells["FIELD_ID"].Value.ToString());
                cboGWType.Text = dgdListMsg.CurrentRow.Cells["GWTYPE"].Value.ToString();
                txtMsgType.Text = dgdListMsg.CurrentRow.Cells["MSG_TYPE"].Value.ToString();
                txtExcelCol.Text = dgdListMsg.CurrentRow.Cells["XLSCOL"].Value.ToString();
                txtFieldName.Text = dgdListMsg.CurrentRow.Cells["FIELD_NAME"].Value.ToString();
                txtFieldDecrip.Text = dgdListMsg.CurrentRow.Cells["FIELD_DESCRIPTION"].Value.ToString();
                txtRowBegin.Text = dgdListMsg.CurrentRow.Cells["ROW_BEGIN"].Value.ToString();
                txtMaxRow.Text = dgdListMsg.CurrentRow.Cells["MAX_ROW"].Value.ToString();
                txtMaxLength.Text = dgdListMsg.CurrentRow.Cells["MAX_LENGTH"].Value.ToString();
                cboCheck.Text = dgdListMsg.CurrentRow.Cells["CHK"].Value.ToString();
                cboDataType.Text = dgdListMsg.CurrentRow.Cells["DATA_TYPE"].Value.ToString();
                txtSwiftField.Text = dgdListMsg.CurrentRow.Cells["SWIFT_FIELD_NAME"].Value.ToString();
                cboDirection.Text = dgdListMsg.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString();
                txtDefault.Text = dgdListMsg.CurrentRow.Cells["DEFAULT_VALUE"].Value.ToString();
                txtRowNum.Text = dgdListMsg.CurrentRow.Cells["ROW_NUM"].Value.ToString();
                txtPartNum.Text = dgdListMsg.CurrentRow.Cells["PART_NUM"].Value.ToString();
                //quynd Update 20081120--------------------------------------------
                GWType = dgdListMsg.CurrentRow.Cells["GWTYPE"].Value.ToString();
                MsgType = dgdListMsg.CurrentRow.Cells["MSG_TYPE"].Value.ToString();
                ExcelCol = dgdListMsg.CurrentRow.Cells["XLSCOL"].Value.ToString();
                FieldName = dgdListMsg.CurrentRow.Cells["FIELD_NAME"].Value.ToString();
                FieldDecrip = dgdListMsg.CurrentRow.Cells["FIELD_DESCRIPTION"].Value.ToString();
                RowBegin = dgdListMsg.CurrentRow.Cells["ROW_BEGIN"].Value.ToString();
                MaxRow = dgdListMsg.CurrentRow.Cells["MAX_ROW"].Value.ToString();
                MaxLength = dgdListMsg.CurrentRow.Cells["MAX_LENGTH"].Value.ToString();
                Check = dgdListMsg.CurrentRow.Cells["CHK"].Value.ToString();
                DataType = dgdListMsg.CurrentRow.Cells["DATA_TYPE"].Value.ToString();
                SwiftField = dgdListMsg.CurrentRow.Cells["SWIFT_FIELD_NAME"].Value.ToString();
                Direction = dgdListMsg.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString();
                Default = dgdListMsg.CurrentRow.Cells["DEFAULT_VALUE"].Value.ToString();
                RowNum = dgdListMsg.CurrentRow.Cells["ROW_NUM"].Value.ToString();
                PartNum = dgdListMsg.CurrentRow.Cells["PART_NUM"].Value.ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
 
        }
        private void cboGWType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                ////DataSet dtsGetCombo = new DataSet();
                ////dtsGetCombo = objctrolall.GetALLCODE2("MSGDirection", cboGWType.Text.Trim());
                ////cboDirection.DataSource = dtsGetCombo.Tables[0];
                ////cboDirection.DisplayMember = "CONTENT";
                ////cboDirection.ValueMember = "CDVAL";                
                ////if (cboGWType.Text.Trim() == "System.Data.DataRowView")
                ////    return;
                ////if (!objGetData.FillDataComboBox(cboGWType, "CONTENT", "CDVAL", "ALLCODE",
                ////    "CDNAME='MSGDirection' and GWTYPE='" + cboGWType.Text.Trim() + "'", 
                ////    "CONTENT", true, false, ""))
                ////    return;

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    if (cmdSearch.Enabled == true)
                    {
                        cmdSearch_Click(null, null);
                    }
                    else if (cmdSave.Enabled == true)
                    {
                        cmdSave.Focus();
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

        private void cmdCancel_Click_1(object sender, EventArgs e)
        {           
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = true;
            cmdCancel.Enabled = false;
            cmdSearch.Enabled = true;
            dgdListMsg.Enabled = true;
            dgdListMsg.Focus();
            dgdListMsg_CellContentClick_1(null, null);
            LockTextbox(true);
        }

        private void txtRowBegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }

        private void txtPartNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }

        private void txtRowNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;                
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }

        private void txtMaxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);                
            }
        }

        private void txtMaxRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);                
            }
        }

        private void dgdListMsg_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmMsgExcel_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cboGWTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!objGetData.FillDataComboBox(cboSDirection, "CONTENT", "CONTENT", "allcode",
                "CDNAME='DIRECTION' and GWTYPE = '" + cboGWTypeSearch.Text.Trim() + "' ", "CONTENT", true, true, "ALL"))
                    return;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 3, MessageBoxButtons.OK);
            }
        }
    }
}

