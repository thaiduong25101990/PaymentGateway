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
    public partial class frmReportPara : frmBasedata 
    {
        private clsLog objLog = new clsLog();
        private RPTPARAMETER_BO objReportPara = new RPTPARAMETER_BO();
        private RPTPARAMETER_Info objReportPara_Info = new RPTPARAMETER_Info();        
        private string sError = "";

        //Cac tham so ghi log
        private DateTime tLogDate = DateTime.Now;
        private string sLogContent = "SYSTEM RPTPARAMETER";
        private int iLogLevel = 1;
        private string sLogWorked = "";
        private string sLogTable = "RPTPARAMETER";
        private string sLogOldValue = "";
        private string sLogNewValue = "";
        
        public frmReportPara()
        {
            InitializeComponent();
        }

        private void frmReportPara_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");

            iSadd = false;
            iSupdate = false;
            LoadCombobox();
            LoadDataGridView(cboReport.SelectedValue.ToString().Trim());
            LoadParaDetail();
            SetControl();            
        }


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Fill du lieu len datagridview
        // Tham so:     sReportName: Ten bao cao
        // Tra ve:      Cac tham so cua bao cao
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void LoadDataGridView(string sReportName)
        {
            try
            {                
                DataTable dtReportPara = new DataTable();
                DataGridViewContentAlignment pAlignCenter = new DataGridViewContentAlignment();
                pAlignCenter = DataGridViewContentAlignment.MiddleCenter;
                objReportPara.GET_RPTPARAMETER(sReportName, out dtReportPara);
                dgView.DataSource = dtReportPara;
                if (dtReportPara != null)
                {
                    dgView.ColumnHeadersHeight = 50;
                    dgView.Columns["ID"].HeaderText = "ID";
                    dgView.Columns["RPTNAME"].HeaderText = "Report Name";                    
                    dgView.Columns["CRLNAME"].HeaderText = "Control Type";
                    dgView.Columns["CRLTYPE"].HeaderText = "Data Type";
                    dgView.Columns["CRLLENGH"].HeaderText = "Length";
                    dgView.Columns["CAPTION"].HeaderText = "Caption";
                    dgView.Columns["SQL"].HeaderText = "SQL string";
                    dgView.Columns["LSTORD"].HeaderText = "Order";
                    dgView.Columns["OPTALL"].HeaderText = "Option all";
                    dgView.Columns["RPTNAME"].HeaderCell.Style.Alignment = pAlignCenter;
                    dgView.Columns["CRLNAME"].HeaderCell.Style.Alignment = pAlignCenter;
                    dgView.Columns["CRLTYPE"].HeaderCell.Style.Alignment = pAlignCenter;
                    dgView.Columns["CRLLENGH"].HeaderCell.Style.Alignment = pAlignCenter;
                    dgView.Columns["CAPTION"].HeaderCell.Style.Alignment = pAlignCenter;
                    dgView.Columns["SQL"].HeaderCell.Style.Alignment = pAlignCenter;
                    dgView.Columns["LSTORD"].HeaderCell.Style.Alignment = pAlignCenter;                    
                    

                    dgView.Columns["ID"].Width = 30;
                    dgView.Columns["ID"].Visible = false;
                    dgView.Columns["RPTNAME"].Width = 80;
                    dgView.Columns["RPTNAME"].Visible = false;
                    dgView.Columns["CRLNAME"].Width = 200;
                    dgView.Columns["CRLTYPE"].Width = 100;
                    dgView.Columns["CRLLENGH"].Width = 80;
                    dgView.Columns["CAPTION"].Width = 200;
                    dgView.Columns["SQL"].Width = 100;
                    dgView.Columns["LSTORD"].Width = 100;
                    dgView.Columns["OPTALL"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (sError == "")
                    sError = ex.Message;
                Common.ShowError(sError,2, MessageBoxButtons.OK);
            }
        }


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Fill du lieu len combobox
        // Tham so:     NA
        // Tra ve:      Du lieu dc load ra combobox
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void LoadCombobox()
        {
            try
            {                
                DataSet dsData  = new DataSet();
                DataTable dtData = new DataTable();
                RPTMASTERController objRPTMASTER = new RPTMASTERController();
                ALLCODEDP objAllCode = new ALLCODEDP();

                //List ot report
                dsData = objRPTMASTER.GetRPTMASTER();
                cboReport.DataSource = dsData.Tables[0];
                cboReport.DisplayMember = "RPTNAME";
                cboReport.ValueMember = "RPTNAME";

                //List of control type               
                dtData = objAllCode.GetALLCODE("control", "SYSTEM");
                cboCtrlType.DataSource = dtData;
                cboCtrlType.DisplayMember = "CONTENT";
                cboCtrlType.ValueMember = "CONTENT";

                //List of data type
                dtData = objAllCode.GetALLCODE("datatyperpt", "SYSTEM");
                cboDataType.DataSource = dtData;
                cboDataType.DisplayMember = "CONTENT";
                cboDataType.ValueMember = "CONTENT";
            }
            catch (Exception ex)
            {
                if (sError == "")
                    sError = ex.Message;
                Common.ShowError(sError,2, MessageBoxButtons.OK);
            }            
        }

        private void cboReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReport.Items.Count > 0)
            {
                LoadDataGridView(cboReport.SelectedValue.ToString().Trim());
                LoadParaDetail();
                SetControl();
            }            
        }      

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadParaDetail();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            iSadd = true;
            SetControl();
        }

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Kiem tra du lieu truoc khi cap nhat
        // Tham so:     NA
        // Tra ve:      True: successfull
        //              False: not successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////     
        private Boolean CheckDataBeforeSave()
        {
            if (cboReport.Items.Count == 0)
            {
                sError = "Input list of report!";
                cboReport.Focus();
                return false;
            }
            if (cboCtrlType.Items.Count == 0)
            {
                sError = "Input control type!";
                cboCtrlType.Focus();
                return false;
            }
            if (cboDataType.Items.Count == 0)
            {
                sError = "Input data type!";
                cboDataType.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCaption.Text.Trim()))
            {
                sError = "Input caption!";
                txtCaption.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLength.Text.Trim()))
            {
                sError = "Input length!";
                txtLength.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOrder.Text.Trim()))
            {
                sError = "Input order!";
                txtOrder.Focus();
                return false;
            }
            if (cboCtrlType.SelectedValue.ToString().Trim() == "picker" && 
                 cboDataType.SelectedValue.ToString().Trim() !="date")
            {
                sError = "Input control type and data type is date!";
                cboDataType.Focus();
                return false;
            }
            else if (cboCtrlType.SelectedValue.ToString().Trim() != "picker" &&
                 cboDataType.SelectedValue.ToString().Trim() == "date")
            {
                sError = "Input control type and data type is date!";
                cboCtrlType.Focus();
                return false;
            }
            if (!Common.IsNumeric(txtLength.Text.ToString()))
            {
                sError = "Input length is number!";
                txtLength.Focus();
                return false;
            }
            if (!Common.IsNumeric(txtOrder.Text.ToString()))
            {
                sError = "Input order is number!";
                txtOrder.Focus();
                return false;
            }
            return true;
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            iSupdate = true;
            SetControl();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 0)
                {
                    if (iSadd == true)
                        iSadd = false;
                    else if (iSupdate == true)
                        iSupdate = false;
                    SetControl();
                    return;
                }
                SaveData();                
            }
            catch (Exception ex)
            {
                SetControl();
                sError = ex.Message;                
                Common.ShowError(sError,2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgView.Rows.Count > 0 && Common.iSconfirm == 1)
                {                    
                    //Set lai cac tham so ghi log
                    sLogWorked = "DELETE";                    
                    sLogNewValue = "";
                    if (dgView.CurrentRow != null)
                    {
                        sLogOldValue =  dgView.CurrentRow.Cells["CAPTION"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["CRLLENGH"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["CRLNAME"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["CRLTYPE"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["LSTORD"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["RPTNAME"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["SQL"].Value.ToString() + "/" +
                                        dgView.CurrentRow.Cells["OPTALL"].Value.ToString();
                        objReportPara_Info.ID = Convert.ToInt64(dgView.CurrentRow.Cells["ID"].Value);
                    }
                    else
                    {
                        sLogOldValue =  dgView.Rows[0].Cells["CAPTION"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["CRLLENGH"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["CRLNAME"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["CRLTYPE"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["LSTORD"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["RPTNAME"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["SQL"].Value.ToString() + "/" +
                                        dgView.Rows[0].Cells["OPTALL"].Value.ToString();
                        objReportPara_Info.ID = Convert.ToInt64(dgView.Rows[0].Cells["ID"].Value);
                    }   
                    //Goi ham xoa
                    objReportPara.Delete_RptParameter(objReportPara_Info);                    
                    //goi ham ghilog
                    objLog.GhiLogUser(tLogDate, Common.Userid, sLogContent, iLogLevel,
                        sLogWorked, sLogTable, sLogOldValue, sLogNewValue);
                    //Set lai cac tham so ghi log
                    sLogWorked = "";
                    sLogOldValue = "";
                    sLogNewValue = "";
                    //Load lai datagridview
                    LoadDataGridView(cboReport.SelectedValue.ToString().Trim());
                }
                SetControl();
                sError = "Delete successfully!";
                Common.ShowError(sError, 1, MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                SetControl();
                sError = ex.Message;
                Common.ShowError(sError,2,MessageBoxButtons.OK);
            }
        }        

        /////////////////////////////////////////////////////////-/
        // Mo ta:       Load dong du lieu tren gridview len control
        // Tham so:     NA
        // Tra ve:      Successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void LoadParaDetail()
        {
            try
            {
                if (dgView.Rows.Count <= 0)
                    return;
                if (dgView.CurrentRow == null)
                {
                    cboCtrlType.SelectedValue = dgView.Rows[0].Cells[2].Value.ToString();
                    cboDataType.SelectedValue = dgView.Rows[0].Cells[3].Value.ToString();
                    txtLength.Text = dgView.Rows[0].Cells[4].Value.ToString();
                    txtCaption.Text = dgView.Rows[0].Cells[5].Value.ToString();
                    txtSqlString.Text = dgView.Rows[0].Cells[6].Value.ToString();
                    txtOrder.Text = dgView.Rows[0].Cells[7].Value.ToString();
                    if (dgView.Rows[0].Cells[8].Value.ToString() == "1")
                        chkALL.Checked = true;
                    else
                    {
                        chkALL.Checked = false;
                        //chkALL.Enabled = false;
                    }

                }
                else
                {
                    cboCtrlType.SelectedValue = dgView.CurrentRow.Cells[2].Value.ToString();
                    cboDataType.SelectedValue = dgView.CurrentRow.Cells[3].Value.ToString();
                    txtLength.Text = dgView.CurrentRow.Cells[4].Value.ToString();
                    txtCaption.Text = dgView.CurrentRow.Cells[5].Value.ToString();
                    txtSqlString.Text = dgView.CurrentRow.Cells[6].Value.ToString();
                    txtOrder.Text = dgView.CurrentRow.Cells[7].Value.ToString();
                    if (dgView.CurrentRow.Cells[8].Value.ToString() == "1")
                        chkALL.Checked = true;
                    else
                        chkALL.Checked = false;
                }
            }
            catch(Exception ex)
            {
                sError = ex.Message;
                Common.ShowError(sError,2, MessageBoxButtons.OK);
            }
        }


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Thiet lap cac thuoc tinh cho control
        // Tham so:     NA
        // Tra ve:      Successfull
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void SetControl()
        {
            if (iSadd == false && iSupdate == false)
            {
                cboReport.Enabled = true;
                cboCtrlType.Enabled = false;
                cboDataType.Enabled = false;
                txtCaption.Enabled = false;
                txtOrder.Enabled = false;
                txtLength.Enabled = false;
                txtSqlString.Enabled = false;
                chkALL.Enabled = false;
                if (dgView.Rows.Count == 0)
                {
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                }
                else
                {
                    cmdDelete.Enabled = true;
                    cmdEdit.Enabled = true;
                }
                cmdAdd.Enabled = true;                
                cmdSave.Enabled = false;
            }
            //Chon nut them moi
            else if (iSadd == true)
            {
                cboReport.Enabled = true;
                cboCtrlType.Enabled = true;
                cboDataType.Enabled = true;
                txtCaption.Enabled = true;
                txtOrder.Enabled = true;
                txtLength.Enabled = true;
                txtSqlString.Enabled = true;
                if (cboCtrlType.SelectedValue.ToString() == "combobox")
                    chkALL.Enabled = true;
                else
                    chkALL.Enabled = false;
                //cboCtrlType.SelectedValue = -1;
                //cboDataType.SelectedValue = -1;
                txtCaption.Text = "";
                txtLength.Text = "";
                txtSqlString.Text = "";
                txtOrder.Text = "";
                cboReport.Focus();
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdSave.Enabled = true;
            }
            //Chon nut sua
            else if (iSupdate == true)
            {
                cboReport.Enabled = false;
                cboCtrlType.Enabled = true;
                cboDataType.Enabled = true;
                txtCaption.Enabled = true;
                txtOrder.Enabled = true;
                txtLength.Enabled = true;
                txtSqlString.Enabled = true;
                if (cboCtrlType.SelectedValue.ToString() == "combobox")
                    chkALL.Enabled = true;
                else
                    chkALL.Enabled = false;
                txtCaption.Focus();
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdSave.Enabled = true;
            }            
        }


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Cap nhat du lieu
        // Tham so:     NA
        // Tra ve:      Successfull   
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void SaveData()
        {
            int iOptAll = 0;

            if (CheckDataBeforeSave() == false)
            {
                Common.ShowError(sError,1, MessageBoxButtons.OK);
                cmdSave.Enabled = true;
                return;
            }
            //Gan cac gia tri moi cho objReportPara_Info
            objReportPara_Info.OPTALL = 0;
            objReportPara_Info.CAPTION = txtCaption.Text.Trim();
            objReportPara_Info.CRLLENGH = Convert.ToInt16(txtLength.Text);
            objReportPara_Info.CRLNAME = cboCtrlType.SelectedValue.ToString().Trim();
            objReportPara_Info.CRLTYPE = cboDataType.SelectedValue.ToString().Trim();
            objReportPara_Info.LSTORD = Convert.ToInt16(txtOrder.Text.Trim());
            objReportPara_Info.RPTNAME = cboReport.SelectedValue.ToString().Trim();
            objReportPara_Info.SQL = txtSqlString.Text.Trim();
            if (cboCtrlType.SelectedValue.ToString() == "combobox" || chkALL.Checked == true)
            {            
                objReportPara_Info.OPTALL = 1;
                iOptAll = 1;
            }
              
            //Tham so ghi log gia tri moi
            sLogNewValue = txtCaption.Text.Trim() + "/" + txtLength.Text + "/" +
                            cboCtrlType.SelectedValue.ToString().Trim() + "/" +
                            cboDataType.SelectedValue.ToString().Trim() + "/" +
                            txtOrder.Text.Trim() + "/" +
                            cboReport.SelectedValue.ToString().Trim() + "/" +
                            txtSqlString.Text.Trim() + "/" + iOptAll.ToString();                

            //Them moi
            if (iSadd == true)
            {    
                objReportPara.Insert_RptParameter(objReportPara_Info);
                iSadd = false;
                //Gia tri ghi log
                sLogWorked = "INSERT";
                sLogOldValue = "";                
            }
            //Sua
            if (iSupdate == true)
            {   
                //Gia tri ghi log
                sLogWorked = "UPDATE";
                if (dgView.CurrentRow != null)
                {
                    sLogOldValue = dgView.CurrentRow.Cells["CAPTION"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["CRLLENGH"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["CRLNAME"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["CRLTYPE"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["LSTORD"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["RPTNAME"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["SQL"].Value.ToString() + "/" +
                                    dgView.CurrentRow.Cells["OPTALL"].Value.ToString();
                    objReportPara_Info.ID = Convert.ToInt64(dgView.CurrentRow.Cells["ID"].Value);
                }
                else
                {
                    sLogOldValue = dgView.Rows[0].Cells["CAPTION"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["CRLLENGH"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["CRLNAME"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["CRLTYPE"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["LSTORD"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["RPTNAME"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["SQL"].Value.ToString() + "/" +
                                    dgView.Rows[0].Cells["OPTALL"].Value.ToString();
                    objReportPara_Info.ID = Convert.ToInt64(dgView.Rows[0].Cells["ID"].Value);
                }
                //Ham cap nhat
                objReportPara.Update_RptParameter(objReportPara_Info);
                iSupdate = false;
            }
            //goi ham ghilog
            objLog.GhiLogUser(tLogDate, Common.Userid, sLogContent, iLogLevel,
                sLogWorked, sLogTable, sLogOldValue, sLogNewValue);
            //Set lai cac tham so ghi log
            sLogWorked = "";
            sLogOldValue = "";
            sLogNewValue = "";
            //
            LoadDataGridView(cboReport.SelectedValue.ToString().Trim());
            LoadParaDetail();
            SetControl();
            sError = "Updated successfull!";
            Common.ShowError(sError, 1, MessageBoxButtons.OK);
        }


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Xu ly su kien nhan phim enter
        // Tham so:       
        // Tra ve:      Successfull        
        // Ngay tao:    01/2009
        // Nguoi tao:   Huypq7
        ///////////////////////////////////////////////////////////
        private void frmReportPara_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Nhan phim Esc
            if (e.KeyChar ==(char)27)          
                this.Close();            
            //Nhan phim Enter
            else if (e.KeyChar == (char)13)
            {
                if (cboReport.Focused)
                    txtCaption.Focus();
                else if (txtCaption.Focused)
                    txtLength.Focus();
                else if (txtLength.Focused)
                    cboCtrlType.Focus();
                else if (cboCtrlType.Focused)
                    cboDataType.Focus();
                else if (cboDataType.Focused)
                    txtOrder.Focus();
                else if (txtOrder.Focused)
                    chkALL.Focus();                
                else if (chkALL.Focused)
                    txtSqlString.Focus();
                else if (txtSqlString.Focused)
                {
                    if (cmdSave.Enabled == true)
                        cmdSave.Focus();
                }
            }
        }

        private void dgView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Chua xu ly keycode
            LoadParaDetail();
        }

        private void dgView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Chua xu ly keycode
            LoadParaDetail();
        }

        private void cboCtrlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControl();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (iSadd == true)
                    iSadd = false;
                else if (iSupdate == true)
                    iSupdate = false;
                SetControl();
            }
            catch (Exception ex)
            {
                Common.ShowError(sError, 1, MessageBoxButtons.OK);
            }
        }
                
              
    }
}
