/*---------------------------------------------------------------
        * Muc dich         : Cap nhat tham so kiem tra dien ve tu Swift:dien Oldkey/Failure/Duplicate 
        * Ngay tao         : 18/06/2008
        * Nguoi tao        : Hantt10
 *--------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRSWIFT
{
    public partial class frmSWIWChk : frmBasedata
    {
        #region khai bao cac ham va bien
        private bool isInsert = false;        
        private SWIFT_IWCKHInfo objInfo = new SWIFT_IWCKHInfo();
        private SWIFT_IWCKHController objControl = new SWIFT_IWCKHController();        
        private clsCheckInput checkInput = new clsCheckInput();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();

        private bool NeedConfirm = true;
        private static bool strSucess = false;
        //---------------------------------------
        private string Field = "";
        private string Value = "";
        private string Descriptions = "";
        #endregion

        public frmSWIWChk()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////-/
            //Muc dich: Kiem tra rowcount>0
            //          CurrentRow<>null
            //Ngay sua: 01/08/2008
            if (dgView.RowCount <= 0)
            {
                return;
            }
            if (dgView.CurrentRow == null)
            {
                return;
            }
            /////////////////////////////////////////////////////
            try
            {                
                isInsert = false;
                LockTextBox(false);
                txtField.ReadOnly = true;
                txtValue.ReadOnly = true;
                txtDescriptions.Focus();
                cmdSave.Enabled = true;
                cmdDelete.Enabled = false;
                cmdEdit.Enabled = false;
                cmdAdd.Enabled = false;
                dgView.ReadOnly = true;
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {            
            //Muc dich: Kiem tra rowcount>0
            //          CurrentRow<>null
            //Ngay sua: 01/08/2008
            if (dgView.RowCount <= 0)
            {
                return;
            }
            if (dgView.CurrentRow == null)
            {
                return;
            }
            /////////////////////////////////////////////////
            try
            {
                if (Common.iSconfirm == 1)
                {
                    string iID = dgView.CurrentRow.Cells[0].Value.ToString();
                    string strValue = dgView.CurrentRow.Cells[1].Value.ToString();

                    objControl.DeleteSWIFT_IWCKH(iID,strValue);
                    DateTime dtDateLogin = DateTime.Now;
                    //string strContent = "Delete from swift_iwchk";//iID + "/" + strValue;
                    string strContent = "Swift condition";
                    int iLoglevel = 1;
                    string strWorked = "Delete";
                    string strTable = "SWIFT_IWCKH";
                    string strOld_value = iID + "/" + strValue;
                    string strNew_value = "";
                    //goi ham ghilog
                    Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LoadData();
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = true;
        }
   
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Muc dich: Kiem tra neu dong y save
                //Nguoi sua: huypq
                //Ngay sua: 31/07/20008
                if (Common.iSconfirm == 1)
                {
                    if (String.IsNullOrEmpty(txtField.Text) || String.IsNullOrEmpty(txtValue.Text))
                    {
                        //Muc dich: Kiem tra neu dong y save
                        //Nguoi sua: huypq
                        //Ngay sua: 31/07/20008
                        cmdSave.Enabled = true;
                        cmdAdd.Enabled = false;
                        cmdEdit.Enabled = false;
                        cmdDelete.Enabled = false;
                        Common.ShowError("You must input value!", 3, MessageBoxButtons.OK);
                        return;
                    }
                    objInfo.FIELD = txtField.Text.Trim().ToUpper();
                    objInfo.VAULE = txtValue.Text.Trim().ToUpper();
                    objInfo.DESCRIPTIONS = txtDescriptions.Text.Trim();

                    if (isInsert == true)
                    {
                        //Muc dich: Kiem tra phai dung ham upper, vi 
                        //          cac gia tri dc chuyen thanh chu hoa
                        //Nguoi sua: huypq
                        //Ngay sua: 31/07/20008
                        //if (GetData.ID2IsExisting(false, "SWIFT_IWCHK", "FIELD", "VAULE",
                        //    txtField.Text.Trim(), txtValue.Text.Trim(), "", ""))
                        if (GetData.ID2IsExisting(false, "SWIFT_IWCHK", "FIELD", "VAULE",
                            txtField.Text.Trim().ToUpper(), txtValue.Text.Trim().ToUpper(), "", ""))
                        {
                            Common.ShowError("ID has already exist.", 3, MessageBoxButtons.OK);
                            txtField.Text = "";
                            txtField.Focus();
                            cmdAdd.Enabled = false;
                            cmdEdit.Enabled = false;
                            cmdSave.Enabled = true;
                            cmdDelete.Enabled = false;
                            return;
                        }
                        ////////////////////////////////////////////////
                        //Muc dich: Ham tra ra gia tri -1 la loi thi ko 
                        //          dc thong bao thanh cong
                        //Nguoi sua: huypq
                        //Ngay sua: 31/07/20008
                        //objControl.AddSWIFT_IWCKH(objInfo);
                        int iID = 0;
                        iID = objControl.AddSWIFT_IWCKH(objInfo);
                        if (iID > 0)
                        {
                            Common.ShowError("Data has inserted successfully!", 1, MessageBoxButtons.OK);
                            //lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "Swift condition";
                            int iLoglevel = 1;
                            string strWorked = "Insert";
                            string strTable = "SWIFT_IWCKH";
                            string strOld_value = "";
                            string strNew_value = txtField.Text + "/" + txtValue.Text + "/" + txtDescriptions.Text;
                            //goi ham ghilog
                            Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                        }
                        else
                        {
                            Common.ShowError("Cannot update data!", 2, MessageBoxButtons.OK);
                        }
                        ////////////////////////////////////////////////

                    }
                    else if (isInsert == false)
                    {
                        //txtField.ReadOnly=true;
                        if (!CheckID())
                        {
                            return;
                        }
                        int intI;
                        intI = objControl.UpdateSWIFT_IWCKH(objInfo);
                        if (intI > 0)
                        {
                            Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);
                            //lay du lieu de ghi log
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "Swift condition";
                            int iLoglevel = 1;
                            string strWorked = "Update";
                            string strTable = "swift_iwchk";
                            string strOld_value = Field + "/" + Value + "/" + Descriptions;
                            string strNew_value = txtField.Text + "/" + txtValue.Text + "/" + txtDescriptions.Text;
                            //goi ham ghilog
                            Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                        }
                        else
                        {
                            Common.ShowError("Cannot update data!", 2, MessageBoxButtons.OK);
                        }
                    }
                    LoadData();
                    LockTextBox(true);
                }
                else
                {                    
                }
                if (dgView.Rows.Count == 0)
                {
                    cmdAdd.Enabled = true; cmdEdit.Enabled = false; cmdDelete.Enabled = false;
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdAdd.Enabled = true; cmdEdit.Enabled = true; cmdDelete.Enabled = true;
                    cmdSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
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

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            LockTextBox(false);
            ClearText();
            txtField.Focus();
            isInsert = true;
        }

        private void frmSWIWChk_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                LoadData();
                LockTextBox(true);
                cmdSave.Enabled = false;
                this.Text = "Swift Condition - Oldkey/Failure/Duplicate";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private bool CheckID()
        {
            bool result = true;
            string ID = txtField.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "You must input textbox!";
                result = false;
            }
            else if (ID.Length > 5)
            {
                Common.ShowError("The max length of value is 5", 3, MessageBoxButtons.OK);                
                result = false;
            }
            return result;
        }
        private void LoadData()
        {
            DataSet datDs = new DataSet();
            datDs = objControl.GetSWIFT_IWCKH();
            dgView.DataSource = datDs.Tables[0];
            if (datDs.Tables[0].Rows.Count == 0)
            {
                //Muc dich: Neu ko co ban ghi
                //Nguoi sua: huypq
                //Ngay sua: 31/07/2008
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;

                return;
            }
            else
            {
                //Muc dich: Neu co ban ghi
                //Nguoi sua: huypq
                //Ngay sua: 31/07/2008
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;

                dgView.Columns[0].HeaderText = "Field/ Block";
                dgView.Columns[0].Width = 140;
                dgView.Columns[1].HeaderText = "Value";
                dgView.Columns[1].Width = 140;
                dgView.Columns[2].HeaderText = "Descriptions";
                dgView.Columns[2].Width = 250;
            }
        }
        private void LockTextBox(Boolean a)
        {
            txtField.ReadOnly = a;
            txtValue.ReadOnly = a;
            txtDescriptions.ReadOnly = a;
        }
        private void ClearText()
        {
            txtDescriptions.Text = "";
            txtField.Text = "";
            txtValue.Text = "";
        }

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ////////////////////////////////////////////////-/
            //Muc dich: Kiem tra rowcount>0
            //Ngay sua:02/08/2008
            if (dgView.RowCount <= 0)
            { 
                return;
            }
            if (dgView.CurrentRow == null)
            {
                return;
            }
            //////////////////////////////////////////////////
            txtField.Text = dgView.CurrentRow.Cells[0].Value.ToString();
            txtValue.Text = dgView.CurrentRow.Cells[1].Value.ToString();
            txtDescriptions.Text = dgView.CurrentRow.Cells[2].Value.ToString();
            //-----------------------------------------------------------------
            Field = dgView.CurrentRow.Cells[0].Value.ToString();
            Value = dgView.CurrentRow.Cells[1].Value.ToString();
            Descriptions = dgView.CurrentRow.Cells[2].Value.ToString();
        }               

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCD_FormClosing(null,null);
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (txtField.Focused)
                {
                    txtValue.Focus();
                    txtValue.SelectAll();
                }
                else if (txtValue.Focused)
                {
                    txtDescriptions.Focus();
                    txtDescriptions.SelectAll();
                }
                else if (txtDescriptions.Focused)
                {
                    cmdSave.Focus();
                    //cmdSave_Click(null, null);
                }

                strSucess = true;
            }
        }

        private void frmSWIWChk_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSWIWChk_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSWIWChk_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
