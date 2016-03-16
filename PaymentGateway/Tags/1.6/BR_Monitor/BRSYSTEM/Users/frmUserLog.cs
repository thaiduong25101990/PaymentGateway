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

namespace BR.BRSYSTEM
{
    public partial class frmUserLog : frmBasedata
    {
        private GetData objGetData = new GetData();
        private USERSInfo objUser = new USERSInfo();
        private USERSController objUsercontrol = new USERSController();
        private USER_MSG_LOGInfo objUser_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontrolUser_log = new USER_MSG_LOGController();
                
        private string pContent="";

        public frmUserLog()
        {
            InitializeComponent();
        }
        
        /*---------------------------------------------------------------
        * Method           : frmUserLog_Load(object sender, EventArgs e)
        * Muc dich         : form load du lieu
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmUserLog_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                //Load data cbUser_name
                if (!objGetData.FillDataComboBox(cbUser_name, "USERNAME", "USERID", "Users",
                    "", "USERNAME", true, true, "ALL"))
                    return;
                Getdatagrid();
                cbUser_name.Focus();
                ColumsHeader(datUser_logMsg);
                //--------cho ngay thang khong qua ngay he thong----------
                this.date_from.MaxDate = DateTime.Now;
                this.date_from.MaxDate = date_to.Value;
                this.date_to.MaxDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void ColumsHeader(DataGridView Datagrid)
        {
            try
            {
                int g = 0;
                while (g < Datagrid.Columns.Count)
                {
                    string strColumns = Datagrid.Columns[g].HeaderText.ToString();
                    Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    Datagrid.Columns[0].Width = 120;
                    Datagrid.ColumnHeadersHeight = 21;
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        /*---------------------------------------------------------------
        * Method           : Getdatagrid()
        * Muc dich         : Ham lay tyoan bo du lieu o bang User_msg_log
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Getdatagrid()
        {
            try
            {
                DataSet datUser_log = new DataSet();
                datUser_log = objcontrolUser_log.GetUser_log();
                if (datUser_log != null) { datUser_logMsg.DataSource = datUser_log.Tables[0]; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : cmdSearch_Click(object sender, EventArgs e)
        * Muc dich         : ham search du lieu theo dieu kien truyen vao
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbUser_name.Text == "ALL")
                {
                    DateTime dat = DateTime.Now; datUser_logMsg.DataSource = 0;
                    string strUserid = Convert.ToString(cbUser_name.SelectedValue);
                    pContent = txtFormName.Text.Trim(); DataSet datUserSearch = new DataSet();
                    datUserSearch = objcontrolUser_log.GetUser_msg_log(date_from.Value, date_to.Value,pContent);
                    datUser_logMsg.DataSource = datUserSearch.Tables[0];
                }
                if (cbUser_name.Text != "ALL")
                {
                    DateTime dat = DateTime.Now; datUser_logMsg.DataSource = 0;
                    DataSet datUserID = new DataSet();
                    datUserID = objUsercontrol.GetUSERS_PASS(cbUser_name.Text);
                    string strUserid = datUserID.Tables[0].Rows[0]["USERID"].ToString();
                    pContent = txtFormName.Text.Trim();  DataSet datUserSearch = new DataSet();
                    datUserSearch = objcontrolUser_log.GetUser_msg_log(strUserid, date_from.Value, date_to.Value,pContent);                    
                    datUser_logMsg.DataSource = datUserSearch.Tables[0];
                }
                ColumsHeader(datUser_logMsg);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : cmdClose_Click(object sender, EventArgs e)
        * Muc dich         : su kien thoat khoi form
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*---------------------------------------------------------------
        * Method           : frmUserLog_FormClosing(object sender, FormClosingEventArgs e)
        * Muc dich         : su kien thoat khoi form
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmUserLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmUserLog_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            { this.Close(); }
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);
                if ((this.ActiveControl) is TextBox)
                { (this.ActiveControl as TextBox).SelectAll(); }
            }
        }

        private void date_from_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (date_from.Checked == false)
                { date_to.Checked = false; }
                else
                { date_to.Checked = true; }
                if (date_from.Value > date_to.Value)
                { date_from.Value = date_to.Value; }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void date_to_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (date_to.Checked == false)
                { date_from.Checked = false; }
                else
                { date_from.Checked = true; }
                if (date_to.Value < date_from.Value)
                { this.date_to.Value = date_from.Value; }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmUserLog_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
