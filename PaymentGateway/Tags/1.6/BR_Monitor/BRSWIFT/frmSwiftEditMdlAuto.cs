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
    public partial class frmSwiftEditMdlAuto : frmBasedata
    {
        public double dblPRM_ID = 0;
        public string strName = "";
        public string strModule = "";
        public string strKeyword = "";
        public string strPriority = "";
        public string strCriteria = "";
        public string strDESC = "";
        public string strError = "";

        private GetData objGetData = new GetData();
        private SWIFT_MODULE_ACTION_Info objInfo = new SWIFT_MODULE_ACTION_Info();
        private SWIFT_MODULE_ACTIONController objControl = new SWIFT_MODULE_ACTIONController();        

        public frmSwiftEditMdlAuto()
        {
            InitializeComponent();
        }

        private void cmdSave1_Click(object sender, EventArgs e)
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
                    objInfo.DEPARTMENT = cboModule.Text.Trim();
                    objInfo.KEY_WORD = txtKeyword.Text.Trim();
                    objInfo.MESSAGE = txtCriteria.Text.Trim();
                    objInfo.PRIORITY = txtPriority.Text.Trim();
                    objInfo.NAME = txtName.Text.Trim();
                    objInfo.DESCPRITION = txtDescription.Text;

                    intI = objControl.UpdateSWIFTMODULEACTION(objInfo);
                    if (intI == -1)
                        Common.ShowError("Database has updated unsuccessfully!", 1, MessageBoxButtons.OK);                        
                    else
                    {
                        Common.ShowError("Data has saved successfully!", 1, MessageBoxButtons.OK);                                                
                        DialogResult DlgExit = new DialogResult();
                        DlgExit = MessageBox.Show("Do you want to exit?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (DlgResult == DialogResult.Yes)
                        {
                            this.Close();
                        }
                      
                    }
               
                }
            }
        }

        private void frmSwiftEditMdlAuto_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");                   
                ////GetData.getDataComboAllCode(cboModule, "CONTENT", "Department", "SYSTEM");
                ////cboModule.Text = strModule;
                if (!objGetData.FillDataComboBox(cboModule, "CONTENT", "CONTENT", "ALLCODE",
                    "gwtype='SYSTEM' and cdname='Department'", "CONTENT", true, false, ""))
                    return;
                txtName.Text = strName;
                txtPriority.Text = strPriority;
                txtKeyword.Text = strKeyword;
                txtCriteria.Text = strCriteria;                
                txtDescription.Text = strDESC;
                txtName.Focus();
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
                Common.ShowError("Criteria Name is null", 3, MessageBoxButtons.OK);                   
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
                Common.ShowError("Criteria Message is null!", 3, MessageBoxButtons.OK);                   
                txtCriteria.Focus();
                return -1;
            }
            //else
            //    return 1;

            if (cboModule.Text.Length < 1)
            {
                Common.ShowError("Module is null!", 3, MessageBoxButtons.OK);                 
                cboModule.Focus();
                return -1;
            }
            //else
            //    return 1;

            //Kiem tra keyword
            if (txtKeyword.Text.Length < 1)
            {
                Common.ShowError("Keyword is null", 3, MessageBoxButtons.OK);                   
                txtKeyword.Focus();
                return -1;
            }
            //kiem tra keycode, message
            int iCheck;
            if (!objGetData.CheckKeyword(txtKeyword.Text.ToString(),
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

        private void frmSwiftEditMdlAuto_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftEditMdlAuto_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }       

    }
}
