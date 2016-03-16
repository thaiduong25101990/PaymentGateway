using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
namespace BR.BRTTSB
{
    public partial class frmIQSNew2 : Form
    {
        public string strIQSContent = "";
        public string strMSG_TYPE = "";        
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        clsCheckInput clsCheck = new clsCheckInput();
        public List<int> listSelected;
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
            try
            {
                if (txtProductCode.Text.Trim().Length != 3 & txtProductCode.Text.Trim().Length != 0)
                {
                    MessageBox.Show("Invalid Product code length!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                strIQSContent = IQSContent;//txtIQSContent.Text.Trim();
                frmIQSlist.strIQSContent = strIQSContent;
                frmIQSlist.strMSG_TYPE = strMSG_TYPE;
                frmIQSlist.strProductCode = clsCheck.ConvertVietnamese(txtProductCode.Text.Trim().ToUpper());
                //this.Hide();
                frmIQSlist.ShowDialog();
                cmdSave.Enabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIQSNew2_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            txtIQSContent.Focus();
        }

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmIQSNew2_FormClosing(null, null);
                this.Close();
            }
            //khi nhan phim Enter
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
                    MessageBox.Show("Invalid product code length", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProductCode.Focus();
                }
            }
            txtProductCode.Text = clsCheck.ConvertVietnamese(txtProductCode.Text.Trim().ToUpper());
        }

        private void txtIQSContent_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
