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

namespace BR.BRIQS
{
    public partial class frmIQSMsgCondNew : frmBasedata
    {
        IQS_CONDITIONInfo objIQS = new IQS_CONDITIONInfo();
        IQS_CONDITIONController objctrl = new IQS_CONDITIONController();

        private bool NeedConfirm = true;
        private static bool strSucess = false;

        public frmIQSMsgCondNew()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMsg_type.Text == "")
                {
                    Common.ShowError("MSG_TYPE is Empty!", 2, MessageBoxButtons.OK);                    
                }
                else
                {
                    //kiem tra co du lieu trung khong
                    DataTable datSS = new DataTable();
                    datSS = objctrl.GetKT(txtMsg_type.Text.Trim(),cbDirection.Text);
                    if (datSS.Rows.Count == 0)//cho ADD
                    {
                        objIQS.MSG_TYPE = txtMsg_type.Text.Trim();
                        objIQS.GWTYPE = cbDirection.Text;                        
                    }                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMsg_type_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtMsg_type.Text==" ")
                {
                    txtMsg_type.Text="";
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdSave.Enabled = true;
                }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIQSMsgCondNew_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCDInfo_FormClosing(null,null);
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (txtMsg_type.Focused)
                {
                    cbDirection.Focus();
                    cbDirection.SelectAll();
                }
                else if (cbDirection.Focused)
                {
                    cmdSave.Focus();
                    cmdSave_Click(null, null);
                }

                strSucess = true;
            }
        }

        private void frmIQSMsgCondNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIQSMsgCondNew_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmIQSMsgCondNew_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
