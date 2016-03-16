using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using utilities;
using BR.BRLib;
using BR.BRSYSTEM;
using BR.BRBusinessObject;

namespace BR.BRSWIFT
{
    public partial class frmSwiftEditBrAuto : frmBasedata
    {
        public double dblPRM_ID = 0;
        public string strName = "";
        public string strBranch = "";
        public string strDepartment = "";
        public string strKeyword = "";
        public string strPriority = "";
        public string strCriteria = "";
        string strError = "";
        public string strDesMSG = "";

        private GetData objGetData = new GetData();
        private SWIFT_BRANCH_ACTION_Info objInfo = new SWIFT_BRANCH_ACTION_Info();
        private SWIFT_BRANCH_ACTIONController objControl = new SWIFT_BRANCH_ACTIONController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        //----Luu gia tri de ghi log-------------------------------------------------
        public string sName = "";
        public string Priority = "";
        public string Keyword = "";
        public string Criteria = "";
        public string Branch = "";
        public string Description = "";
        public string Module = "";
        //---------------------------------------------------------------------------
        public frmSwiftEditBrAuto()
        {
            InitializeComponent();
        }

        private void frmSwiftEditBrAuto_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                ////GetData.getDataCombo1(cboBranch, "SIBS_BANK_CODE", "BRANCH");
                if (!objGetData.FillDataComboBox(cboBranch, "SIBS_BANK_CODE", "SIBS_BANK_CODE", 
                    "BRANCH","", "SIBS_BANK_CODE", true, false, ""))
                    return;

                GetCombo(cboModule, "SYSTEM", "Department", "CONTENT", "CONTENT");
                txtName.Text = strName;
                txtPriority.Text = strPriority;
                txtKeyword.Text = strKeyword;
                txtCriteria.Text = strCriteria;
                cboBranch.Text = strBranch;
                txtDescription.Text = strDesMSG;
                cboModule.Text = strDepartment;
                //QUYND Update 20081126 gia tri cu de ghi log--------
                sName = strName;
                Priority = strPriority;
                Keyword = strKeyword;
                Criteria = strCriteria;
                Branch = strBranch;
                Description = strDesMSG;
                Module = strDepartment;
                //----------------------------------------------------
                if (!GetData.CheckExistBranch(cboBranch.Text.Trim()))
                {
                    cboBranch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void GetCombo(ComboBox cboALLCODE, string strGWtype, string strCDName, string strValue, string strDes)
        {
            try
            {
                ALLCODEController objAllcode = new ALLCODEController();

                DataTable dattblAlcode = new DataTable();

                dattblAlcode = objAllcode.GetALLCODE_LIST(strGWtype, strCDName);
                cboALLCODE.DataSource = dattblAlcode;
                cboALLCODE.ValueMember = strValue;
                cboALLCODE.DisplayMember = strDes;

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void cmdClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        ///////////////////////////////////////////////////////
        //Muc dich: 1.Sau khi kiem tra 1 gia tri, neu dung thi
        //          phai kiem tra tiep,
        //          2.Kiem tra o txtKeyword, 
        //          3.Kiem tra xau Sql trong o txtCriteria co 
        //          dung dinh dang
        //Nguoi sua: huypq
        //Ngay sua: 01/08/2008        
        ///////////////////////////////////////////////////////        
        private int Check()
        {        
            if (txtName.Text.Length < 1)
            {
                Common.ShowError("Criteria name is null!", 3, MessageBoxButtons.OK);                
                txtName.Focus();
                return -1;         
            }
            //else
            //    return 1;               
 
            if (txtPriority.Text.Length < 1)
            {
                Common.ShowError("Priority is null!", 3, MessageBoxButtons.OK);                
                txtPriority.Focus();
                return -1;                
            }
            //else
            //    return 1;
            if (txtCriteria.Text.Length < 1)
            {
                Common.ShowError("Criteria Message is null", 3, MessageBoxButtons.OK);               
                txtCriteria.Focus();
                return -1;
            }
           
            int iCheck = 0;
            //Kiem tra keyword
            if (txtKeyword.Text.Length < 1)
            {
                Common.ShowError("Keyword is null", 3, MessageBoxButtons.OK);               
                txtKeyword.Focus();
                return -1;
            }
            //kiem tra keycode, message
            if (!objControl.CheckKeyword(txtKeyword.Text.ToString(),
                txtCriteria.Text.ToString(), out strError, out iCheck))
            {
                Common.ShowError(strError, 3, MessageBoxButtons.OK);                
                txtKeyword.Focus();
                return -1;
            }
            if (iCheck != 1)            
            {
                Common.ShowError("Criteria message is invalid", 3, MessageBoxButtons.OK);                
                return -1;
            }
            return 1;
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


        private void cmdSave1_Click_1(object sender, EventArgs e)
        {
            int intI = Check();
            if (intI < 0)
                return;
            else
            {
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show("Do you want to save data?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    objInfo.PRM_ID = dblPRM_ID;
                    objInfo.BRANCH = cboBranch.Text.Trim();
                    objInfo.KEY_WORD = txtKeyword.Text.Trim();
                    objInfo.MESSAGE = txtCriteria.Text.Trim();
                    objInfo.PRIORITY = txtPriority.Text.Trim();
                    objInfo.NAME = txtName.Text.Trim();
                    objInfo.DESCPRIPTION = txtDescription.Text.Trim();
                    objInfo.DEPARTMENT = cboModule.Text.Trim();

                    intI = objControl.UpdateSWIFT_BRANCH_ACTION(objInfo);
                    if (intI == -1)
                        Common.ShowError("Can not update database!", 2, MessageBoxButtons.OK);                        
                    else
                    {
                        Common.ShowError("Data has saved successfully!", 1, MessageBoxButtons.OK);                        
                        //--------------------------------------------------------
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "Swift - Edit auto module and branch auto condition:";                        
                        int iLoglevel = 1;
                        string strWorked = "Update";
                        string strTable = "SWIFT_BRANCH_ACTION";
                        string strOld_value = sName + "/" + Priority + "/" + Keyword + "/" + Criteria + "/" + Branch + "/" + Description + "/" + Module;
                        string strNew_value = txtName.Text + "/" + txtPriority.Text + "/" + txtKeyword.Text + "/" + txtCriteria.Text + "/" + cboBranch.Text + "/" + txtDescription.Text + "/" + cboModule.Text;
                        //goi ham ghilog
                        Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                       
                            this.Close();
                       
                    }
                }
            }
        }

        private void cmdClose1_Click_1(object sender, EventArgs e)
        {
             this.Close();
        }


        //////////////////////////////////////////
        //Muc dich: O txtPriority Chi nhap so
        //Nguoi tao: Huypq
        //Ngay tao: 01/08/2008
        //////////////////////////////////////////
        private void txtPriority_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);               
            }
        }

        //////////////////////////////////////////
        //Muc dich: khi nhan phim ESC
        //Nguoi tao: Huypq
        //Ngay tao: 01/08/2008
        //////////////////////////////////////////
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                if (MessageBox.Show("Do you want to exit?", Common.sCaption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void frmSwiftEditBrAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               
               e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSwiftEditBrAuto_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftEditBrAuto_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
