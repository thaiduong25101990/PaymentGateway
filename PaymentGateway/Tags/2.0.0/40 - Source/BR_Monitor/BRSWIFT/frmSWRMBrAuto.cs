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
using BR.BRSYSTEM;
using System.Text.RegularExpressions;

namespace BR.BRSWIFT
{
    public partial class frmSWRMBrAuto : frmBasedata
    {
        BRANCHController objBranch = new BRANCHController();
        SWIFT_RMBR_AUTOController objRMBR_control = new SWIFT_RMBR_AUTOController();
        SWIFT_RMBR_AUTOInfo objInfo = new SWIFT_RMBR_AUTOInfo();
        //--------------------------------------------------------------------------
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        //--------------------------------------------------------------------------
        private string strBank1;
        private string strBank2;
        private int iAdd;
        private int iEdit;
        //private int iDelete;
        private int Cancel;
        private int iClose = 0;
        private int iRows;
        //private int Select_Save;
        private string pORG_BRANOLD;
        private string pRECEIVER_BRANOLD;

        public frmSWRMBrAuto()
        {
            InitializeComponent();
        }

        private void frmSWRMBrAuto_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            iAdd = 0;
            iEdit = 0;
            //iDelete = 0;
            cboBranch.Enabled = false;
            cboBranchDes.Enabled = false;
            Cancel = 0;
            iClose = 0;
            //------------------------------------------------------
            GetDatacombo();
            Getdatagrid_BRAUTO();
            this.Text = "Swift - Branch receive RM message";
        }
        private void Getdatagrid_BRAUTO()
        {
            try
            {
                grdView.Rows.Clear();
                DataTable datBR = new DataTable();
                datBR = objRMBR_control.Get_Rmbr();
                if (datBR == null || datBR.Rows.Count == 0)
                {
                    cmdAdd.Enabled = true;
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdSave.Enabled = false;
                    cmdcancel.Enabled = false;
                }
                else
                {
                    int i = 0;
                    while (i < datBR.Rows.Count)
                    {
                        grdView.Rows.Add();
                        grdView.Rows[i].Cells[0].Value = datBR.Rows[i]["ORG_BRAN"].ToString();
                        grdView.Rows[i].Cells[1].Value = datBR.Rows[i]["RECEIVER_BRAN"].ToString();
                        i = i + 1;
                    }
                    //cmdAdd.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdSave.Enabled = false;
                    cmdcancel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void  GetDatacombo()
        {
            try
            {
                DataSet datDS = new DataSet();
                datDS = objBranch.GetData();
                if (datDS.Tables[0].Rows.Count == 0 || datDS == null)
                {
                }
                else
                {
                    int i = 0;
                    while (i < datDS.Tables[0].Rows.Count)
                    {
                        cboBranch.Items.Add(datDS.Tables[0].Rows[i]["SIBS_BANK_CODE"].ToString());
                        i = i + 1;
                    }                   
                }
                DataSet datDSDesc = new DataSet();
                datDSDesc = objBranch.GetData();
                if (datDSDesc.Tables[0].Rows.Count == 0 || datDSDesc == null)
                {
                }
                else
                {
                    int j = 0;
                    while (j < datDSDesc.Tables[0].Rows.Count)
                    {
                        cboBranchDes.Items.Add(datDSDesc.Tables[0].Rows[j]["SIBS_BANK_CODE"].ToString());
                        j = j + 1;
                    }                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }       

        private void frmSWRMBrAuto_KeyDown(object sender, KeyEventArgs e)
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

        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strBran = cboBranch.Text.Trim();
                DataTable datBB = new DataTable();
                datBB = objBranch.GetData_Leave(strBran);
                if (datBB == null || datBB.Rows.Count == 0)
                {
                    MessageBox.Show("Branch does not exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBranch.Focus();
                }
                else
                {
                    lblBranchOri.Text = datBB.Rows[0]["BRAN_NAME"].ToString();
                }
                //lblBranchOri.Text = cboBranch.SelectedValue.ToString();
                if (Regex.IsMatch(cboBranch.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    strBank1 = cboBranch.Text.Trim();
                }
                else
                {
                    cboBranch.Text = strBank1;
                }
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboBranchDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strBran = cboBranchDes.Text.Trim();
                DataTable datBB = new DataTable();
                datBB = objBranch.GetData_Leave(strBran);
                if (datBB == null || datBB.Rows.Count == 0)
                {
                    MessageBox.Show("Branch does not exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboBranchDes.Focus();
                }
                else
                {
                    lblBranchReceiver.Text = datBB.Rows[0]["BRAN_NAME"].ToString();
                }
                //lblBranchReceiver.Text = cboBranchDes.SelectedValue.ToString();
                if (Regex.IsMatch(cboBranchDes.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    strBank2 = cboBranchDes.Text.Trim();
                }
                else
                {
                    cboBranchDes.Text = strBank2;
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
                    if (iAdd == 1)
                    {
                        objInfo.ORG_BRAN = cboBranch.Text.Trim();
                        objInfo.RECEIVER_BRAN = cboBranchDes.Text.Trim();
                        if (objRMBR_control.ADDSWIFT_RMBR_AUTO(objInfo) == 1)
                        {
                            MessageBox.Show("Insert is successful!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            grdView.Rows.Clear();
                            Getdatagrid_BRAUTO();
                            grdView.Enabled = true;
                            //lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "Swift RM branch condition";
                            int iLoglevel = 1;
                            string strWorked = "Insert";
                            string strTable = "SWIFT_RMBR_AUTO";
                            string strOld_value = "";
                            string strNew_value = cboBranch.Text + "/" + cboBranchDes.Text;
                            //goi ham ghilog
                            Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                        }
                        else
                        {
                            MessageBox.Show("Insert is Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboBranch.Enabled = false;
                            cboBranchDes.Enabled = false;
                        }

                    }
                    else if (iEdit == 1)
                    {
                        if (objRMBR_control.Update_Brauto(pORG_BRANOLD, pRECEIVER_BRANOLD, cboBranch.Text.Trim(), cboBranchDes.Text.Trim()) == 1)
                        {
                            MessageBox.Show("Update is successful!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            grdView.Rows[iRows].Cells[0].Value = cboBranch.Text.Trim();
                            grdView.Rows[iRows].Cells[1].Value = cboBranchDes.Text.Trim();
                            grdView.Enabled = true;                            
                            //lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "Swift RM branch condition";
                            int iLoglevel = 1;
                            string strWorked = "Update";
                            string strTable = "SWIFT_RMBR_AUTO";
                            string strOld_value = pORG_BRANOLD + "/" + pRECEIVER_BRANOLD;
                            string strNew_value = cboBranch.Text + "/" + cboBranchDes.Text;
                            //goi ham ghilog
                            Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                        }
                        else
                        {
                            MessageBox.Show("Update is Error!",Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboBranch.Enabled = false;
                            cboBranchDes.Enabled = false;
                        }
                    }
                }
                cboBranch.Enabled = false;
                cboBranchDes.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            if (grdView.Rows.Count == 0)
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
            cmdcancel.Enabled = false;
        }
        /*---------------------------------------------------------------
        * Method           : Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        * Muc dich         : Ham ghi log User dang nhap vao he thong
        * Tham so          : cac gia tri tuong ung voi bang User_msg_log
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
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
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    //-----------------------------
                    if (grdView.Rows.Count == 0)
                    {
                        cmdDelete.Enabled = false;
                        cmdEdit.Enabled = false;
                    }
                    else
                    {
                        cmdDelete.Enabled = true;
                        cmdEdit.Enabled = true;
                    }
                    cmdSave.Enabled = false;
                    cmdcancel.Enabled = false;
                    cmdAdd.Enabled = true;
                    iAdd = 0;
                    iEdit = 0;
                    //iDelete = 1;
                    //---------------------------------------
                    pORG_BRANOLD = grdView.Rows[iRows].Cells[0].Value.ToString();
                    pRECEIVER_BRANOLD = grdView.Rows[iRows].Cells[1].Value.ToString();
                    if (objRMBR_control.Delete(pORG_BRANOLD, pRECEIVER_BRANOLD) == 1)
                    {
                        MessageBox.Show("Delete is successful!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        grdView.Enabled = true;
                        //lay du lieu de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        //string strContent = "Delete SWIFT_RMBR_AUTO where ORG_BRAN=" + pORG_BRANOLD + "' and RECEIVER_BRAN= " + pRECEIVER_BRANOLD + " ";
                        string strContent = "Swift RM branch condition";
                        int iLoglevel = 1;
                        string strWorked = "Delete";
                        string strTable = "SWIFT_RMBR_AUTO";
                        string strOld_value = pORG_BRANOLD + "/" + pRECEIVER_BRANOLD;
                        string strNew_value = "";
                        //goi ham ghilog
                        Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                    }
                    else
                    {
                        MessageBox.Show("Update is Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        grdView.Enabled = true;
                    }
                }
                cboBranch.Enabled = false;
                cboBranchDes.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            Getdatagrid_BRAUTO();   
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //---------------------------------------

                cmdAdd.Enabled = false;
                cmdDelete.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSave.Enabled = true;
                cmdcancel.Enabled = true;
                //---------------------------------------
                iAdd = 0;
                iEdit = 1;
                //iDelete = 0;
                //-------------------------------------
                cboBranch.Enabled = true;
                cboBranchDes.Enabled = true;
                //--------------------------------------
                cboBranch.Text = grdView.Rows[iRows].Cells[0].Value.ToString();
                cboBranchDes.Text = grdView.Rows[iRows].Cells[1].Value.ToString();
                pORG_BRANOLD = grdView.Rows[iRows].Cells[0].Value.ToString();
                pRECEIVER_BRANOLD = grdView.Rows[iRows].Cells[1].Value.ToString();
                grdView.Enabled = false;
                //---------------------------------------
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
                //---------------------------------------
                cmdAdd.Enabled = false;
                cmdDelete.Enabled = false;
                cmdEdit.Enabled = false;
                cmdSave.Enabled = true;
                cmdcancel.Enabled = true;
                //---------------------------------------
                iAdd = 1;
                iEdit = 0;
                //iDelete = 0;
                //---------------------------------------
                cboBranch.Enabled = true;
                cboBranch.Text = "";
                //cboBranch.SelectedIndex = -1;
                cboBranchDes.Enabled = true;
                cboBranchDes.Text = "";
               // cboBranchDes.SelectedIndex = -1;
                cboBranch.Focus();
                grdView.Enabled = false;
                lblBranchOri.Text = "";
                lblBranchReceiver.Text = "";
                //----------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboBranch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Cancel == 1 || iClose == 1)
                {
                    Cancel = 0;
                    iClose = 0;
                }
                else
                {
                    string strBran = cboBranch.Text.Trim();
                    DataTable datBB = new DataTable();
                    datBB = objBranch.GetData_Leave(strBran);
                    if (datBB == null || datBB.Rows.Count == 0)
                    {
                        MessageBox.Show("Branch does not exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboBranch.Focus();
                    }
                    else
                    {
                        DataTable datS = new DataTable();
                        datS=objRMBR_control.Search(cboBranch.Text.Trim());
                        if (datS.Rows.Count == 0)
                        {
                            lblBranchOri.Text = datBB.Rows[0]["BRAN_NAME"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Branch has already exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cboBranch.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboBranchDes_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Cancel == 1 || iClose == 1)
                {
                    Cancel = 0;
                    iClose = 0;
                }
                else
                {
                    string strBran = cboBranchDes.Text.Trim();
                    DataTable datBB = new DataTable();
                    datBB = objBranch.GetData_Leave(strBran);
                    if (datBB == null || datBB.Rows.Count == 0)
                    {
                        MessageBox.Show("Branch does not exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboBranchDes.Focus();
                    }
                    else
                    {
                        lblBranchReceiver.Text = datBB.Rows[0]["BRAN_NAME"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdcancel_Click(object sender, EventArgs e)
        {
            try
            {
                cboBranch.Enabled = false;
                cboBranchDes.Enabled = false;
                if (grdView.Rows.Count == 0)
                {
                    cmdEdit.Enabled = false;
                    cmdDelete.Enabled = false;
                }
                else
                {
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                }
                cmdAdd.Enabled = true;
                cmdSave.Enabled = false;
                cmdcancel.Enabled = false;
                grdView.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdcancel_MouseMove(object sender, MouseEventArgs e)
        {
            Cancel = 1;    
        }

        private void cmdClose_MouseMove(object sender, MouseEventArgs e)
        {
            iClose = 1;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iRows = e.RowIndex;
        }

        private void grdView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            iRows = e.RowIndex;
        }

        private void cmdSave_Leave(object sender, EventArgs e)
        {
            //Select_Save = 1;
        }

        private void frmSWRMBrAuto_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
