using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
namespace BR.BRInterBank
{
    public partial class frmIQSNew2 : Form
    {
        public string strIQSContent = "";
        public string strMsg_Type = "";
        public List<int> listSelected;
        clsCheckInput clsCheck = new clsCheckInput();

        //private bool NeedConfirm = true;
        //private static bool strSucess = false;

        public frmIQSNew2()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim().Length != 3 & txtProductCode.Text.Trim().Length != 0)
            {
                Common.ShowError("Invalid Product code length!", 3, MessageBoxButtons.OK);                
                txtProductCode.Focus();
                return;
            }
            string IQSContent = clsCheck.ConvertVietnamese(txtIQSContent.Text.Trim());

            if (frmSelectTypeIQS.isTraSoat == true)
            {
                txtIQSContent.MaxLength = 500;                
            }
            else if (frmSelectTypeIQS.isTraSoat == false)
            {
                txtIQSContent.MaxLength = 1000;                
            }
           
            frmIQSList frmIQSlist = new frmIQSList();
            frmIQSlist.listSelected = this.listSelected;
            strIQSContent = IQSContent;
            frmIQSlist.strIQSContent = strIQSContent;
            frmIQSlist.strMsg_Type = strMsg_Type;
            frmIQSlist.strProductCode = clsCheck.ConvertVietnamese(txtProductCode.Text.Trim().ToUpper());            
            frmIQSlist.ShowDialog();
            cmdSave.Enabled = false;            
            this.Close();
        }

        private void frmIQSNew2_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                txtIQSContent.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

       
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {               
                this.Close();
            }            
            if (e.KeyChar == (char)13)
            {
                if (txtProductCode.Focused)
                {
                    txtIQSContent.Focus();
                    txtIQSContent.SelectAll();
                }
                if (txtIQSContent.Focused)
                {
                    cmdSave.Focus();
                }
                //strSucess = true;
            }
        }

        private void frmIQSNew2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmIQSNew2_KeyDown(object sender, KeyEventArgs e)
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
                    cmdSave.Focus();
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

        private void frmIQSNew2_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            {
                return;
            }
            else
            {
                if (txtProductCode.Text.Trim().Length != 3)
                {
                    Common.ShowError("Invalid Product code length!", 3, MessageBoxButtons.OK);                   
                    txtProductCode.Focus();
                }
            }
            txtProductCode.Text = clsCheck.ConvertVietnamese(txtProductCode.Text.Trim().ToUpper());
        }

    }
}
