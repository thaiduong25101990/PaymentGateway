using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
//using System.Data.or;
using System.Configuration;
using System.Threading;
using BR.BRBusinessObject;

namespace BR.BRTTSB
{
    public partial class frmSelectTypeIQS : Form
    {
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;

        public string strMSG_TYPE = "";
        public static bool isTraSoat = true;
        public static bool isOK = true;
        private ALLCODEController objAllcode = new ALLCODEController();
        public frmSelectTypeIQS()
        {
            InitializeComponent();
        }

        private void frmSelectTypeIQS_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            cboType.DataSource = objAllcode.GetALLCODE("MSG_TYPE", "IQS");
            cboType.DisplayMember = "CONTENT";
            cboType.ValueMember = "CDVAL";
        }

        private void cmdok_Click(object sender, EventArgs e)
        { 
            isOK = true;
            if (cboType.Text.Trim() != "")
            {               
                strMSG_TYPE = cboType.SelectedValue.ToString();
                if (cboType.Text.Trim() == "DIEN TRA SOAT")
                {
                    isTraSoat = true;
                }
                else if (cboType.Text.Trim() == "DIEN THONG BAO")
                {
                    isTraSoat = false;
                }
                this.Close();
            }
            else if (string.IsNullOrEmpty(cboType.Text.Trim()))
            {
                MessageBox.Show("You must chose data!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        private void cmdcancel_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Close();
        }

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            ////khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                isOK = false;
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (cboType.Focused)
                {
                    cmdok.Focus();
                    cmdok_Click(null, null);
                }

                //strSucess = true;
            }
            return;
        }       

        private void frmSelectTypeIQS_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSelectTypeIQS_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
