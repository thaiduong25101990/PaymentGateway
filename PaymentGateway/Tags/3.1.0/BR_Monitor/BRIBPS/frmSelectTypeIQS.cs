using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using System.Data.OracleClient;
using System.Configuration;
using System.Threading;
using BR.BRBusinessObject;

// Form cho nguoi su dung lua chon tao dien Thong Bao hay dien Tra Soat
namespace BR.BRIBPS
{
    public partial class frmSelectTypeIQS : Form
    {
        private ALLCODEController objAllcode = new ALLCODEController();
        public string strMSG_TYPE = "";
        public static bool isTraSoat = true;
        public static bool isOK = true;

        //private bool NeedConfirm = true;
        public static bool strSucess = false;

        public frmSelectTypeIQS()
        {
            InitializeComponent();
        }

        private void frmSelectTypeIQS_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                cboType.DataSource = objAllcode.GetALLCODE("MSG_TYPE", "IQS");
                cboType.DisplayMember = "CONTENT";
                cboType.ValueMember = "CDVAL";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdok_Click(object sender, EventArgs e)
        {
            try
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
                    Common.ShowError("You must chose data!", 2, MessageBoxButtons.OK);                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdcancel_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Close();
        }

        
        private void enterKey(object sender, KeyPressEventArgs e)
        {            
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

                strSucess = true;
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
