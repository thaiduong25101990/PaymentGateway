using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRBusinessObject;

namespace BR.BRSYSTEM
{
    public partial class frmServiceLog : frmBasedata
    {
        MSG_LOGInfo objMsg_log = new MSG_LOGInfo();
        MSG_LOGController objCtrlLog = new MSG_LOGController();
        public frmServiceLog()
        {
            InitializeComponent();
        }

        private void frmServiceLog_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                datFrom.Checked = true; datto.Checked = true;
                cbServicename.SelectedIndex = 0;
                GetData();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham lay du lieu toan bo cua cac service len  datatgrid
        private void GetData()
        {
            try
            {
                DataTable datMsg_log = new DataTable();
                datMsg_log = objCtrlLog.GetMSG_LOG();
                if (datMsg_log != null)
                {
                    datGrService.DataSource = datMsg_log;
                    Columns_header();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Columns_header()
        {
            try
            {
                datGrService.Columns["LOG_ID"].Width = 80;
                datGrService.Columns["LOG_DATE"].Width = 130;
                datGrService.Columns["SERVICE"].Width = 150;
                datGrService.Columns["DESCRIPTIONS"].Width = 130;
                datGrService.Columns["STATUS"].Width = 120;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdsearch_Click(object sender, EventArgs e)
        {
            try
            {
                datGrService.DataSource = 0;
                if (cbServicename.Text == "ALL" && datFrom.Checked == false)
                {
                    DataTable datMsg_log = new DataTable();
                    datMsg_log = objCtrlLog.GetMSG_LOG();
                    if (datMsg_log.Rows.Count == 0)
                    {
                        MessageBox.Show("There is no suitable record with search condition!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        datGrService.DataSource = datMsg_log;
                        Columns_header();                        
                    }
                }
                if (cbServicename.Text == "ALL" && datFrom.Checked == true)
                {
                    DataTable datSear = new DataTable();
                    datSear = objCtrlLog.GetMSG_LOG_S(datFrom.Value, datto.Value);
                    if (datSear.Rows.Count == 0)
                    {
                        MessageBox.Show("There is no suitable record with search condition!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        datGrService.DataSource = datSear;
                        Columns_header();
                    }
                }
                if (cbServicename.Text != "ALL" && datFrom.Checked == false)
                {   
                    DataTable datLL = new DataTable();
                    datLL = objCtrlLog.GetMSG_LOG_S1(cbServicename.Text.Trim());
                    if (datLL.Rows.Count == 0)
                    {
                        MessageBox.Show("There is no suitable record with search condition!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        datGrService.DataSource = datLL;
                        Columns_header();
                    }
                }
                if (cbServicename.Text != "ALL" && datFrom.Checked == true)
                {
                    DataTable datMLL = new DataTable();
                    datMLL = objCtrlLog.GetMSG_LOG_S2(cbServicename.Text.Trim(), datFrom.Value, datto.Value);
                    if (datMLL.Rows.Count == 0)
                    {
                        MessageBox.Show("There is no suitable record with search condition!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        datGrService.DataSource = datMLL;
                        Columns_header();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void datFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (datFrom.Checked == false)
                {
                    datto.Checked = false;
                }
                else
                {
                    datto.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void datto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (datto.Checked == false)
                {
                    datFrom.Checked = false;
                }
                else
                {
                    datFrom.Checked = true;
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

        private void frmServiceLog_KeyDown(object sender, KeyEventArgs e)
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
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmServiceLog_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void datGrService_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
