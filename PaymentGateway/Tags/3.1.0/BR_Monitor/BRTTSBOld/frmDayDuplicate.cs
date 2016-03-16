using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
//using BR.DataAccess;
using BR.BRLib;

namespace BR.BRTTSB
{
    public partial class frmDayDuplicate : Form
    {
        private DataSet datDs = new DataSet();
        private SYSVARInfo objInfo = new SYSVARInfo();
        private SYSVARController objControl = new SYSVARController();
        private int iID = 0;
        private clsCheckInput checkInput = new clsCheckInput();

        public frmDayDuplicate()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SYSVARInfo objInfo = new SYSVARInfo();
            objInfo.ID = iID;
            objInfo.VALUE = txtDayNumber.Text.Trim();

            try
            {
                if (GetData.IDIsExisting(true, "SYSVAR", "VALUE", txtDayNumber.Text.Trim(), ""))
                    {
                        MessageBox.Show("The number of duplicated day already existed!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        txtDayNumber.Text = "";
                        txtDayNumber.Focus();
                        cmdSave.Enabled = true;
                        return;
                    }
                    else if (!CheckID())
                    {
                        return;
                    }
                    objControl.UpdateSYSVARDayDuplicate(objInfo);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private bool CheckID()
        {
            bool result = true;
            string ID = txtDayNumber.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "You must input textbox!";
                result = false;
            }
            else if (ID.Length > 30)
            {
                MessageBox.Show("The max length of value is 30", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }
            return result;
        }

        private void txtDayNumber_Validated(object sender, EventArgs e)
        {
            if (!checkInput.IsNumeric(txtDayNumber.Text))
            {
                MessageBox.Show("You must input a number.", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDayNumber.Text = "";
                txtDayNumber.Focus();
            }
        }

        private void frmDayDuplicate_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmDayDuplicate_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmDayDuplicate_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
        }
    }
}
