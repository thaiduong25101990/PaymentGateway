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
    public partial class frmSIBSPort : frmBasedata
    {
        #region bien va ham
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData(); 
        private DataSet datDs = new DataSet();
        private GWSERVICE_PORTInfo objInfo = new GWSERVICE_PORTInfo();
        private GWSERVICE_PORTController objControl = new GWSERVICE_PORTController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private clsCheckInput clsCheck = new clsCheckInput();
        private bool isInsert = false;
        private int iID = 0;        
        private string ServiceName1 = "";
        private string SIBSIP1 = "";
        private string SIBSPortIn1 = "";
        private string SIBSPortOut1 = "";
        private string FileType1 = "";
        private string Timedelay = "";
        private string Description1 = "";
        #endregion

        public frmSIBSPort()
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
                isInsert = false; txtServiceName.ReadOnly = true;
                txtSIBSIP.Focus();
                LockTextBox(false);               
                CommandStatus(false);
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
                if (Common.iSconfirm == 1)
                {
                    iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                    try
                    {
                        objControl.DeleteGWSERVICE_PORT(iID);
                        #region lay thong tin ghi log
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "Bridge service - SIBS port";
                        int Log_level = 1;
                        string strWorked = "Delete";
                        string strTable = "GWSERVICE_PORT";
                        string strOld_value = ServiceName1 + "/" + SIBSIP1 + "/" + 
                            SIBSPortIn1 + "/" + 
                            SIBSPortOut1 + "/" + FileType1 + "/" + Description1;
                        string strNew_value = "";
                        //---------------------------------------------------------------
                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                            strWorked, strTable, strOld_value, strNew_value);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                    }
                    LoadData();                   
                    CommandStatus(true);
                }
                else
                {
                    CommandStatus(true);
                    return;
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
                if (Common.iSconfirm == 1)
                {
                    if (string.IsNullOrEmpty(txtServiceName.Text.Trim()) || 
                        string.IsNullOrEmpty(txtSIBSIP.Text.Trim()) || 
                        string.IsNullOrEmpty(txtSIBSPortIn.Text.Trim()) || 
                        string.IsNullOrEmpty(txtSIBSPortOut.Text.Trim()))
                    {
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                        
                        CommandStatus(false);
                        return;
                    }
                    objInfo.ID = iID;
                    objInfo.SERVICENAME = clsCheck.ConvertVietnamese(txtServiceName.Text.Trim());
                    objInfo.SIBSIP = txtSIBSIP.Text.Trim();
                    objInfo.PORTIN = txtSIBSPortIn.Text.Trim();
                    objInfo.PORTOUT = txtSIBSPortOut.Text.Trim();
                    objInfo.FILETYPE = cboFileType.SelectedValue.ToString();
                    objInfo.DESCRIPTION = clsCheck.ConvertVietnamese(txtDescription.Text.Trim());
                    if (txtTime_delay.Text.Trim() == "") { objInfo.TIMEDELAY = 0; }
                    else
                    { objInfo.TIMEDELAY = Convert.ToInt32(txtTime_delay.Text.Trim()); }
                    if (Common.iSconfirm == 1)
                    {
                        #region neu la insert
                        if (isInsert == true)
                        {
                            if (GetData.IDIsExisting(false, "GWSERVICE_PORT", "SERVICENAME", 
                                txtServiceName.Text.Trim(), ""))
                            {
                                Common.ShowError("ID has already exist!", 3, MessageBoxButtons.OK);                                
                                txtServiceName.Text = "";
                                txtServiceName.Focus();
                                CommandStatus(false);
                                return;
                            }
                            else if (!CheckID())
                            { return; }
                            objControl.AddGWSERVICE_PORT(objInfo);
                            Common.ShowError("Data has inserted successfully!", 1, MessageBoxButtons.OK);                            
                            CommandStatus(true);
                            #region lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "Bridge service - SIBS port";
                            int Log_level = 1;
                            string strWorked = "Insert";
                            string strTable = "GWSERVICE_PORT";
                            string strOld_value = "";
                            string strNew_value = objInfo.SERVICENAME + "/" + objInfo.SIBSIP + 
                                "/" + objInfo.PORTIN + "/" + objInfo.PORTOUT + "/" + objInfo.FILETYPE + 
                                "/" + objInfo.DESCRIPTION;
                            //-----------------------------------------
                            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                strWorked, strTable, strOld_value, strNew_value);
                            #endregion

                        }
                        #endregion
                        #region Update du lieu
                        else if (isInsert == false)
                        {
                            if (!CheckID())
                            {
                                CommandStatus(false);
                                return;
                            }
                            objControl.UpdateGWSERVICE_PORT(objInfo);
                            Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);                            
                            CommandStatus(true);

                            #region lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "Bridge service - SIBS port";
                            int Log_level = 1;
                            string strWorked = "Update";
                            string strTable = "GWSERVICE_PORT";
                            string strOld_value = ServiceName1 + "/" + SIBSIP1 + "/" + SIBSPortIn1 + 
                                "/" + SIBSPortOut1 + "/" + FileType1 + "/" + Description1;
                            string strNew_value = objInfo.SERVICENAME + "/" + objInfo.SIBSIP + 
                                "/" + objInfo.PORTIN + "/" + objInfo.PORTOUT + "/" + objInfo.FILETYPE + 
                                "/" + objInfo.DESCRIPTION;
                            //-----------------------------------------
                            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                strWorked, strTable, strOld_value, strNew_value);
                            #endregion
                        }
                        #endregion
                        LoadData();
                        LockTextBox(true);
                        CommandStatus(true);
                        txtServiceName.ReadOnly = true;
                    }
                    else
                    {
                        ClearText();
                        CommandStatus(true);
                        return;
                    }
                }
                else
                {
                    ClearText();
                    LockTextBox(true);
                    CommandStatus(true);
                    return;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            LockTextBox(false);
            ClearText();
            CommandStatus(false);
            txtServiceName.ReadOnly = false;
            txtServiceName.Focus();
            isInsert = true;
            cmdCancel.Enabled = true;
        }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSave.Enabled = !a;
            cmdCancel.Enabled = !a;
            dgView.Enabled = a;
        }

        private void frmSIBSPort_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            if (!objGetData.FillDataComboBox(cboFileType, "CONTENT", "CDVAL", "ALLCODE",
                "CDNAME='FileType' AND GWTYPE='SYSTEM'", "CONTENT", true, false, ""))
                return;
            LoadData();
            LockTextBox(true);
            txtServiceName.ReadOnly = true;
            cmdCancel.Visible = true;
            CommandStatus(true);
        }

        private void LockTextBox(Boolean a)
        {
            txtDescription.ReadOnly = a;            
            txtSIBSIP.ReadOnly = a;
            txtSIBSPortIn.ReadOnly = a;
            txtSIBSPortOut.ReadOnly = a;
            txtTime_delay.ReadOnly = a;
            cboFileType.Enabled = !a;

        }
                
        private void ClearText()
        {
            txtSIBSPortOut.Text = "";
            txtSIBSPortIn.Text = "";
            txtSIBSIP.Text = "";
            txtServiceName.Text = "";
            txtDescription.Text = "";
            cboFileType.Text = "";
        }

        private void LoadData()
        {
            DataSet datDS = new DataSet();
            datDs = objControl.GetGWSERVICE_PORT();
            dgView.DataSource = datDs.Tables[0];
            if (datDs.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                dgView.Columns[0].Visible = false;
                #region do rong cac cot
                dgView.Columns["SERVICENAME"].HeaderText = "Service name";
                dgView.Columns["SERVICENAME"].Width = 200;
                dgView.Columns["SIBSIP"].HeaderText = "SIBS IP";
                dgView.Columns["SIBSIP"].Width = 120;
                dgView.Columns["PORTIN"].HeaderText = "Port in";
                dgView.Columns["PORTIN"].Width = 80;
                dgView.Columns["PORTOUT"].HeaderText = "Port out";
                dgView.Columns["PORTOUT"].Width = 80;
                dgView.Columns[5].Visible = false;
                dgView.Columns["FILETYPE"].HeaderText = "File type";
                dgView.Columns["FILETYPE"].Width = 100;
                dgView.Columns["DESCRIPTION"].HeaderText = "Description";
                dgView.Columns["DESCRIPTION"].Width = 250;
                dgView.Columns["TIMEDELAY"].HeaderText = "Time delay";
                dgView.Columns["TIMEDELAY"].Width = 100;
                #endregion
            }
        }
        
        private bool CheckID()
        {
            bool result = true;
            string ID = txtServiceName.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "You must input textbox!";
                result = false;
            }
            else if (ID.Length > 30)
            {
                Common.ShowError("The max length of value is 30!", 3, MessageBoxButtons.OK);                
                result = false;
            }
            return result;
        }        

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {               
                iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                txtServiceName.Text = dgView.CurrentRow.Cells["SERVICENAME"].Value.ToString();
                txtSIBSIP.Text = dgView.CurrentRow.Cells["SIBSIP"].Value.ToString();
                txtSIBSPortIn.Text = dgView.CurrentRow.Cells["PORTIN"].Value.ToString();
                txtSIBSPortOut.Text = dgView.CurrentRow.Cells["PORTOUT"].Value.ToString();
                cboFileType.SelectedValue = dgView.CurrentRow.Cells["FILETYPE"].Value.ToString();
                txtTime_delay.Text = dgView.CurrentRow.Cells["TIMEDELAY"].Value.ToString();
                txtDescription.Text = dgView.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                //---------------Update 20081120----------------------------------
                ServiceName1 = dgView.CurrentRow.Cells["SERVICENAME"].Value.ToString();
                SIBSIP1 = dgView.CurrentRow.Cells["SIBSIP"].Value.ToString();
                SIBSPortIn1 = dgView.CurrentRow.Cells["PORTIN"].Value.ToString();
                SIBSPortOut1 = dgView.CurrentRow.Cells["PORTOUT"].Value.ToString();
                FileType1 = dgView.CurrentRow.Cells["FILETYPE"].Value.ToString();
                Timedelay = dgView.CurrentRow.Cells["TIMEDELAY"].Value.ToString();
                Description1 = dgView.CurrentRow.Cells["DESCRIPTION"].Value.ToString();                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }       

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Common.ClearControl(this);
            if (dgView.Rows.Count == 0)
            {
                cmdAdd.Enabled = true; cmdEdit.Enabled = false;
                cmdDelete.Enabled = false; cmdSave.Enabled = false;
            }
            else
            {
                cmdAdd.Enabled = true; cmdEdit.Enabled = true;
                cmdDelete.Enabled = true; cmdSave.Enabled = false;
            }
            cmdCancel.Enabled = false;
            dgView.Enabled = true;
            LockTextBox(true);
            dgView.Focus();
            dgView_CellContentClick(null, null);
        }

       /*---------------------------------------------------------------
       * Method           : enterKey(object sender, KeyEventArgs e)
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
                    if ((this.ActiveControl as Button).Name == "cmdSave")
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

        private void frmSIBSPort_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        

        private void txtTime_delay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strTime_delay = "";
                if (Regex.IsMatch(txtTime_delay.Text.Trim(), @"^[0-9]*\z") == true)
                {
                    strTime_delay = txtTime_delay.Text.Trim();
                }
                else
                { txtTime_delay.Text = strTime_delay; }
            }
            catch(Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }      
    
    }
}
