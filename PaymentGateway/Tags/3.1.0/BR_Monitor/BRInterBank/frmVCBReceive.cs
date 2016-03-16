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
using BR.BRSYSTEM;
using System.Text.RegularExpressions;

namespace BR.BRInterBank
{
    public partial class frmVCBReceive : frmBasedata
    {
        ALLCODEInfo objAllcode = new ALLCODEInfo();
        ALLCODEController objCtrlAllcode = new ALLCODEController();
        VCB_PARAMETERSInfo objVCB = new VCB_PARAMETERSInfo();
        VCB_PARAMETERSController objCtrlVCB = new VCB_PARAMETERSController();
        private int iRows;
        clsCheckInput clsCheck = new clsCheckInput();
        //-------------------------------------------------------------------
        private int Add;
        private int Edit;
        private int Delete;
        private string strDelete = "";
        private int Count;
        private string strMsg_type;
        //-------------------------------------------------------------------
        public frmVCBReceive()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVCBReceive_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                Add = 0; Edit = 0; Delete = 0;
                //-------------------------------------------------
                cmdCancel.Enabled = false; cmdSave.Enabled = false; cbbModule.Enabled = false;
                txtMsg_type.Enabled = false; txtBankcode.Enabled = false; cbChennal.Enabled = false;
                //-------------------------------------------------
                Load_Combobox();
                Load_data();
                iRows = 0;
                if (dataSearch.Rows.Count != 0)
                {
                    cmdEdit.Enabled = true; cmdDelete.Enabled = true;
                    views_data();
                }
                else
                {
                    cmdEdit.Enabled = false; cmdDelete.Enabled = false;
                }
                dataSearch.Columns["ID"].Visible = false;
                this.Text = "MSG Parameters";
                //-------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Load_Combobox()
        {
            try
            {
                //-------------------------------------------------
                DataTable datAll = new DataTable();
                datAll = objCtrlAllcode.GetALLCODE_D("Department").Tables[0];
                //-------------------------------------------------
                int i = 0;
                while (i < datAll.Rows.Count)
                {
                    if (datAll.Rows[i]["CONTENT"].ToString() != "Other")
                    {
                        string strContent = datAll.Rows[i]["CONTENT"].ToString();
                        cbbModule.Items.Add(strContent);
                    }
                    i = i + 1;
                }                
                DataTable datAll1 = new DataTable();
                datAll1 = objCtrlAllcode.GetALLCODE_D("GWTYPE").Tables[0];
                //-------------------------------------------------
                int j = 0;
                while (j < datAll1.Rows.Count)
                {
                    if (datAll1.Rows[j]["CONTENT"].ToString() != "Other")
                    {
                        string strContent = datAll1.Rows[j]["CONTENT"].ToString();
                        cbChennal.Items.Add(strContent);
                    }
                    j = j + 1;
                }
                //-------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Load_data()
        {
            try
            {
                dataSearch.Rows.Clear();
                DataTable datVCB = new DataTable();
                datVCB = objCtrlVCB.Search();
                if (datVCB.Rows.Count != 0)
                {                    
                    int j=0;
                    while (j < datVCB.Rows.Count)
                    {                        
                        string Depart = datVCB.Rows[j]["DEPARTMENT"].ToString();
                        string Msg_type = datVCB.Rows[j]["MSG_TYPE"].ToString();
                        string Bank_code = datVCB.Rows[j]["BANK_CODE"].ToString();
                        string ID = datVCB.Rows[j]["ID"].ToString();
                        string Channel = datVCB.Rows[j]["GWTYPE"].ToString();                        
                        dataSearch.Rows.Add();
                        dataSearch.Rows[j].Cells["DEPARTMENT"].Value = Depart;
                        dataSearch.Rows[j].Cells["MSG_TYPE"].Value = Msg_type;
                        dataSearch.Rows[j].Cells["BANK_CODE"].Value = Bank_code;
                        dataSearch.Rows[j].Cells["ID"].Value = ID;
                        dataSearch.Rows[j].Cells["GWTYPE"].Value = Channel;
                        j = j + 1;
                    }
                    Read_only();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows = e.RowIndex; }
            views_data();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows = e.RowIndex; }         
                views_data();            
        }
        private void views_data()
        {
            try
            {
                if (dataSearch.Rows[iRows].Cells["DEPARTMENT"].Value != null)
                {
                    cbbModule.Text = dataSearch.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                    txtMsg_type.Text = dataSearch.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                    txtBankcode.Text = dataSearch.Rows[iRows].Cells["BANK_CODE"].Value.ToString();
                    cbChennal.Text = dataSearch.Rows[iRows].Cells["GWTYPE"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Add = 0; Edit = 1; Delete = 0;
                //--------------------------------------------
                dataSearch.Enabled = false;
                cbbModule.Enabled = true;
                cbChennal.Enabled = true;
                txtMsg_type.Enabled = true;
                txtBankcode.Enabled = true;
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                cbbModule.Focus();
                //---------------------------------------------
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
               if(Common.iSconfirm == 1)
               {
                    Add = 0; Edit = 0; Delete = 1;
                    //--------------------------------------------
                    dataSearch.Enabled = false;
                    strDelete = dataSearch.Rows[iRows].Cells["ID"].Value.ToString();
                    dataSearch.Rows.RemoveAt(iRows);
                    cbbModule.Text = "";
                    txtMsg_type.Text = "";
                    txtBankcode.Text = "";
                    cmdAdd.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdSave.Enabled = true;
                    cmdCancel.Enabled = true;
                    Delete_Vcb();
                    Read_only();
                    //---------------------------------------------
                }
                else
                {
                    views_data();
                    Add = 0; Edit = 0; Delete = 0;
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                }
               if (dataSearch.Rows.Count == 0)
               {
                   cbbModule.Text = "";
                   txtMsg_type.Text = "";
                   txtBankcode.Text = "";
                   //------------------------
                   cbbModule.Enabled = false;
                   txtMsg_type.Enabled = false;
                   txtBankcode.Enabled = false;
                   //------------------------
                   cmdAdd.Enabled = true;
                   cmdEdit.Enabled = false;
                   cmdDelete.Enabled = false;
                   cmdSave.Enabled = false;
                   cmdCancel.Enabled = false;
               }
               else
               {
                   views_data();
                   //------------------------
                   cbbModule.Enabled = false;
                   txtMsg_type.Enabled = false;
                   txtBankcode.Enabled = false;
                   //------------------------
                   cmdAdd.Enabled = true;
                   cmdEdit.Enabled = true;
                   cmdDelete.Enabled = true;
                   cmdSave.Enabled = false;
                   cmdCancel.Enabled = false;
               }
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
                Add = 1; Edit = 0; Delete = 0;
                //--------------------------------------------
                dataSearch.Enabled = false;
                cbbModule.Text = "";
                txtMsg_type.Text = "";
                txtBankcode.Text = "";
                cbbModule.Enabled = true;
                cbChennal.Enabled = true;
                txtMsg_type.Enabled = true;
                txtBankcode.Enabled = true;
                cmdAdd.Enabled = false;
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                cbbModule.Focus();
                //---------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Refresh_controls()
        {
            try
            {
                #region An cac controls
                dataSearch.Enabled = true;
                if (Delete == 1)
                {
                    Load_data();
                    Read_only();
                }
                //--------------------------------------------
                if (dataSearch.Rows.Count == 0)
                {
                    cbbModule.Text = "";
                    txtMsg_type.Text = "";
                    txtBankcode.Text = "";
                    //------------------------
                    cbbModule.Enabled = false;
                    cbChennal.Enabled = false;
                    txtMsg_type.Enabled = false;
                    txtBankcode.Enabled = false;
                    //------------------------
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                }
                else
                {
                    views_data();
                    //------------------------
                    cbbModule.Enabled = false;
                    cbChennal.Enabled = false;
                    txtMsg_type.Enabled = false;
                    txtBankcode.Enabled = false;
                    //------------------------
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Refresh_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Inser_Vcb()
        {
            try
            {
                //objVCB,objCtrlVCB
                objVCB.DEPARTMENT = cbbModule.Text.Trim();
                objVCB.MSG_TYPE = txtMsg_type.Text.Trim();
                objVCB.BANK_CODE = txtBankcode.Text.Trim();
                objVCB.GWTYPE = cbChennal.Text.Trim();
                if (objCtrlVCB.Add(objVCB) == 1)
                {
                    Common.ShowError("Insert data successful!", 1, MessageBoxButtons.OK);                    
                    Load_data();
                }
                else
                {
                    Common.ShowError("Insert data successful!", 1, MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }  
        }
        private void Edit_Vcb()
        {
            try
            {
                objVCB.ID = Convert.ToInt32(dataSearch.Rows[iRows].Cells["ID"].Value.ToString());
                objVCB.DEPARTMENT = cbbModule.Text.Trim();
                objVCB.MSG_TYPE = txtMsg_type.Text.Trim();
                objVCB.BANK_CODE = txtBankcode.Text.Trim();
                objVCB.GWTYPE = cbChennal.Text.Trim();
                if (objCtrlVCB.Update(objVCB) == 1)
                {
                    Common.ShowError("Update data successful!", 1, MessageBoxButtons.OK);                    
                    dataSearch.Rows[iRows].Cells["DEPARTMENT"].Value = cbbModule.Text.Trim();
                    dataSearch.Rows[iRows].Cells["MSG_TYPE"].Value = txtMsg_type.Text.Trim();
                    dataSearch.Rows[iRows].Cells["BANK_CODE"].Value = txtBankcode.Text.Trim();
                    dataSearch.Rows[iRows].Cells["GWTYPE"].Value = cbChennal.Text.Trim();  
                }
                else
                {
                    Common.ShowError("Update data Error!", 3, MessageBoxButtons.OK);                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }
        private void Read_only()
        {
            try
            {
                dataSearch.Columns["DEPARTMENT"].ReadOnly = true;
                dataSearch.Columns["MSG_TYPE"].ReadOnly = true;
                dataSearch.Columns["BANK_CODE"].ReadOnly = true;
                dataSearch.Columns["GWTYPE"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }
        
        private void Delete_Vcb()
        {
            try
            {                
                if (objCtrlVCB.Delete(strDelete) == 1)
                {
                    Common.ShowError("Delete successfully!", 1, MessageBoxButtons.OK);                      
                }
                else
                {
                    Common.ShowError("Delete data error!", 2, MessageBoxButtons.OK);                    
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
                    if (String.IsNullOrEmpty(txtMsg_type.Text.Trim()))
                    {
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                        txtMsg_type.Focus();
                        cmdAdd.Enabled = false;
                        cmdEdit.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdSave.Enabled = true;
                        cmdCancel.Enabled = true;
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankcode.Text.Trim()))
                    {
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                        txtBankcode.Focus();
                        cmdAdd.Enabled = false;
                        cmdEdit.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdSave.Enabled = true;
                        cmdCancel.Enabled = true;
                        return;
                    }
                    txtBankcode.Text = clsCheck.ConvertVietnamese(txtBankcode.Text.ToUpper());
                    Count = 0;
                    dataSearch.Enabled = true;
                    if (Add == 1)//insert du lieu
                    {
                        //check trung du lieu
                        DataTable datT = new DataTable();
                        datT = objCtrlVCB.check(cbbModule.Text.Trim(), cbChennal.Text.Trim(), txtMsg_type.Text.Trim(), txtBankcode.Text.Trim());
                        if (datT.Rows.Count == 0)//khong bi trung
                        {
                            Inser_Vcb();
                        }
                        else
                        {
                            Common.ShowError("Data has already exist!", 3, MessageBoxButtons.OK);
                            dataSearch.Enabled = false;
                            cmdSave.Enabled = true;
                            return;
                        }
                    }
                    else if (Edit == 1)//sua du lieu
                    {
                        int m = 0;
                        while (m < dataSearch.Rows.Count)
                        {
                            if (m != iRows)
                            {
                                string strDe = dataSearch.Rows[m].Cells["DEPARTMENT"].Value.ToString();
                                string strMSg = dataSearch.Rows[m].Cells["MSG_TYPE"].Value.ToString();
                                string strBan = dataSearch.Rows[m].Cells["BANK_CODE"].Value.ToString();
                                string strChan = dataSearch.Rows[m].Cells["GWTYPE"].Value.ToString();
                                if (txtBankcode.Text.Trim() == strBan.Trim() && txtMsg_type.Text.Trim() == strMSg.Trim() && cbbModule.Text.Trim() == strDe.Trim() && cbChennal.Text.Trim() == strChan.Trim())
                                {
                                    Count = 1;
                                }
                            }
                            m = m + 1;
                        }
                        if (Count == 1)
                        {
                            Common.ShowError("Data has already exist!", 3, MessageBoxButtons.OK);
                            dataSearch.Enabled = false;
                            cmdSave.Enabled = true;
                            return;
                        }
                        else
                        {
                            Edit_Vcb();
                        }
                    }
                    else if (Delete == 1)//xoa du lieu
                    {
                        //Delete_Vcb();
                    }
                    if (dataSearch.Rows.Count == 0)
                    {
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdSave.Enabled = false;
                        cmdCancel.Enabled = false;
                        cbbModule.Enabled = false;
                        cbChennal.Enabled = false;
                        txtBankcode.Enabled = false;
                        txtMsg_type.Enabled = false;
                    }
                    else
                    {
                        cbbModule.Text = dataSearch.Rows[0].Cells["DEPARTMENT"].Value.ToString();
                        txtMsg_type.Text = dataSearch.Rows[0].Cells["MSG_TYPE"].Value.ToString();
                        txtBankcode.Text = dataSearch.Rows[0].Cells["BANK_CODE"].Value.ToString();
                        cbChennal.Text = dataSearch.Rows[0].Cells["GWTYPE"].Value.ToString();
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdDelete.Enabled = true;
                        cmdSave.Enabled = false;
                        cmdCancel.Enabled = false;
                        cbbModule.Enabled = false;
                        cbChennal.Enabled = false;
                        txtBankcode.Enabled = false;
                        txtMsg_type.Enabled = false;
                    }
                    Read_only();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);  
            }
        }

        private void frmVCBReceive_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmVCBReceive_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void txtMsg_type_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtMsg_type.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    if (txtMsg_type.Text.Trim().Length <= 3)
                    {
                        strMsg_type = txtMsg_type.Text.Trim();
                    }
                    else
                    {
                        txtMsg_type.Text = strMsg_type;
                    }
                }
                else
                {
                    txtMsg_type.Text = strMsg_type;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtBankcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBankcode_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBankcode.Text = clsCheck.ConvertVietnamese(txtBankcode.Text.ToUpper());
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }
}
