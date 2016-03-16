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
    public partial class frmJobStatus : frmBasedata
    {
        private clsLog objLog = new clsLog();
        private ALLCODEInfo objallcode = new ALLCODEInfo();
        private ALLCODEController objcontrolAllcode = new ALLCODEController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();

        private int iRow;
        //private static int selectedRow;
        //private bool NeedConfirm = true;        
        private static string sttrJobName;
        private static string strStatus;
        
        public frmJobStatus()
        {
            InitializeComponent();
        }
        /*---------------------------------------------------------------
        * Method           : frmJobStatus_Load(object sender, EventArgs e)
        * Muc dich         : Su kien form load
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmJobStatus_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            cmdStop.Enabled = false;
            cmdStart.Enabled = false;
            cmdRestart.Enabled = false;
            GetDataCombo();
            Getdatagrid();
        }
        /*---------------------------------------------------------------
        * Method           : Getdatagrid()
        * Muc dich         : Load du lieu la ten cac Job va trang thai len datagrid
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
                string strUserDB = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                DataSet datAllcode_job = new DataSet();
                datAllcode_job = objcontrolAllcode.GetJob(strUserDB);
                //dtrJob.DataSource = datAllcode.Tables[0];
                int i = 0;
                //dtrJob.Rows.Clear();
                while (i < datAllcode_job.Tables[0].Rows.Count)
                {
                    string strJobname = datAllcode_job.Tables[0].Rows[i]["JOB_NAME"].ToString();
                    string strEnabled = datAllcode_job.Tables[0].Rows[i]["ENABLED"].ToString();
                    string strStatus;
                    if (strEnabled == "TRUE")
                    {
                        strStatus = "Started";
                    }
                    else
                    {
                        strStatus = "Stop";
                    }
                    datDieukien.Rows.Add();
                    datDieukien.Rows[i].Cells["Order"].Value = Convert.ToString(i+1);
                    datDieukien.Rows[i].Cells["NAME"].Value = strJobname;
                    datDieukien.Rows[i].Cells["STATUS"].Value = strStatus;
                    //-------------------------------------------
                    //datDieukien.Rows[i].HeaderCell.Value = Convert.ToString(i);
                    datDieukien.Columns["Order"].Width = 30;
                    datDieukien.Columns["NAME"].Width = 320;
                    datDieukien.Columns["STATUS"].Width = 80;
                    datDieukien.Rows[i].ReadOnly = true;
                    //-------------------------------------------
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
        * Method           : GetDataCombo()
        * Muc dich         : Lay du lieu la thong tinn ten cac job len cac commobox
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void GetDataCombo()
        {
            try
            {
                string strUserDB = BR.BRBusinessObject.DATABASESInfo.strUserDB;//lay thong tin Username dang nhap database
                DataSet datAllcode = new DataSet();
                datAllcode = objcontrolAllcode.GetJob(strUserDB);
                cbJob.Items.Add("ALL");
                cbJob.SelectedIndex = 0;
                int f = 0;
                while (f < datAllcode.Tables[0].Rows.Count)
                {
                    string strJobname = datAllcode.Tables[0].Rows[f]["JOB_NAME"].ToString();
                    cbJob.Items.Add(strJobname);
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
       * Method           : dataGrid_MouseDown(object sender, MouseEventArgs e)
       * Muc dich         : Bat su kien click chuot vao hang trong DataGrid
       * Tham so          : 
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
       *--------------------------------------------------------------*/
        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  // Neu laf Chuot Trai
            {
                //cmdStop.Enabled = true;
                //cmdStart.Enabled = true;
                //cmdRestart.Enabled = true;
                // Neu la Chuot Trai chi cho chon NHIEU hang
                this.datDieukien.MultiSelect = false;
                this.datDieukien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                strStatus = datDieukien.CurrentRow.Cells[2].Value.ToString();
                if (strStatus == "Started")
                {
                    cmdStart.Enabled = false;
                    cmdStop.Enabled = true;
                    cmdRestart.Enabled = true;
                }
                else
                {
                    cmdStart.Enabled = true;
                    cmdStop.Enabled = false;
                    cmdRestart.Enabled = false;
                }
                sttrJobName = datDieukien.CurrentRow.Cells[1].Value.ToString();
            }
        }
        //thoat khoi form nay
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*---------------------------------------------------------------
       * Method           : dataGrid_MouseDown(object sender, MouseEventArgs e)
       * Muc dich         : Bat su kien click chuot vao hang trong DataGrid
       * Tham so          : 
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
       *--------------------------------------------------------------*/
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbJob.Text == "ALL")
                {
                    string strOwner = Common.Schema;
                    DataSet datAl = new DataSet();
                    datAl = objcontrolAllcode.GetJob(strOwner.ToUpper());
                    datDieukien.Rows.Clear();
                    int w = 0;
                    while (w < datAl.Tables[0].Rows.Count)
                    {
                        string strJob = datAl.Tables[0].Rows[w]["JOB_NAME"].ToString();
                        string strStat1 = datAl.Tables[0].Rows[w]["ENABLED"].ToString();
                        string strSTATUS1;
                        if (strStat1 == "TRUE")
                        {
                            strSTATUS1 = "Started";
                        }
                        else
                        {
                            strSTATUS1 = "Stop";
                        }
                        datDieukien.Rows.Add();
                        datDieukien.Rows[w].Cells["Order"].Value = Convert.ToString(w + 1);
                        datDieukien.Rows[w].Cells["NAME"].Value = strJob;
                        datDieukien.Rows[w].Cells["STATUS"].Value = strSTATUS1;                       
                        datDieukien.Columns["Order"].Width = 30;
                        datDieukien.Columns["NAME"].Width = 320;
                        datDieukien.Columns["STATUS"].Width = 80;
                        //datDieukien.Rows[w].Cells[0].Value = strJob;
                        //datDieukien.Rows[w].Cells[1].Value = strSTATUS1;
                        //datDieukien.Columns[0].Width = 320;
                        //datDieukien.Columns[1].Width = 80;
                        datDieukien.Rows[w].ReadOnly = true;
                        w = w + 1;
                    }
                }
                else
                {
                    datDieukien.Rows.Clear();
                    string strUserDB = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                    string strJobname = cbJob.Text.Trim();
                    DataSet datSearch = new DataSet();
                    datSearch = objcontrolAllcode.SearchJob(strUserDB, strJobname);
                    //string strJob_name = datSearch.Tables[0].Rows[0]["JOB_NAME"].ToString();
                    string strStat = datSearch.Tables[0].Rows[0]["ENABLED"].ToString();
                    string strSTATUS;
                    if (strStat == "TRUE")
                    {
                        strSTATUS = "Started";
                    }
                    else
                    {
                        strSTATUS = "Stop";
                    }
                    datDieukien.Rows.Add();
                    datDieukien.Rows[0].Cells["Order"].Value = "1";
                    datDieukien.Rows[0].Cells["NAME"].Value = strJobname;
                    datDieukien.Rows[0].Cells["STATUS"].Value = strSTATUS;
                    datDieukien.Columns["Order"].Width = 30;
                    datDieukien.Columns["NAME"].Width = 320;
                    datDieukien.Columns["STATUS"].Width = 80;
                    //datDieukien.Rows[0].Cells[0].Value = strJobname;
                    //datDieukien.Rows[0].Cells[1].Value = strSTATUS;
                    //datDieukien.Columns[0].Width = 320;
                    //datDieukien.Columns[1].Width = 80;
                    datDieukien.Rows[0].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
       * Method           : cmdStop_Click(object sender, EventArgs e)
       * Muc dich         : stop cac Job duoc chon trong luoi
       * Tham so          : 
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
       *--------------------------------------------------------------*/
        private void cmdStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (objcontrolAllcode.Stop_Job(sttrJobName) == 1)
                {
                    MessageBox.Show("Stop job successfully!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    datDieukien.Rows[iRow].Cells[2].Value = "Stop";
                    cmdRestart.Enabled = true;
                    cmdStart.Enabled = true;
                    cmdStop.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Can not stop Job", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    datDieukien.Rows[iRow].Cells[2].Value = "Started";
                    cmdRestart.Enabled = false;
                    cmdStart.Enabled = false;
                    cmdStop.Enabled = true;
                }
                //lay thong tin de ghilog----------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Job status";
                int Log_level = 1;
                string strWorked = "Stop_Job";
                string strTable = "dba_scheduler_jobs";
                string strOld_value = "";
                string strNew_value = "";
                //-----------------------------------------
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                    strWorked, strTable, strOld_value, strNew_value);
                //--------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
      * Method           : cmdStart_Click(object sender, EventArgs e)
      * Muc dich         : start cac Job duoc chon
      * Tham so          : 
      * Tra ve           : 
      * Ngay tao         : 10/07/2008
      * Nguoi tao        : QuyND
      * Ngay cap nhat    : 10/07/2008
      * Nguoi cap nhat   : QuyND(Nguyen duc quy)
      *--------------------------------------------------------------*/
        private void cmdStart_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (objcontrolAllcode.Start_Job(sttrJobName) == 1)
                {
                    MessageBox.Show("Start job successfully",Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    datDieukien.Rows[iRow].Cells[2].Value = "Started";
                    cmdStart.Enabled = false;
                    cmdRestart.Enabled = true;
                    cmdStop.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Can not start Job", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
                    datDieukien.Rows[iRow].Cells[2].Value = "Stop";
                    cmdStart.Enabled = true;
                    cmdRestart.Enabled = true;
                    cmdStop.Enabled = false;
                }
                //lay thong tin de ghilog----------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Job status";
                int Log_level = 1;
                string strWorked = "Start_job";
                string strTable = "dba_scheduler_jobs";
                string strOld_value = "";
                string strNew_value = "";
                //-----------------------------------------
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                    strWorked, strTable, strOld_value, strNew_value);
                //--------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
       * Method           : cmdRestart_Click(object sender, EventArgs e)
       * Muc dich         : goi ham start
       * Tham so          : 
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
       *--------------------------------------------------------------*/
        private void cmdRestart_Click(object sender, EventArgs e)
        {
            try
            {
                
                //goi ham start
                if (objcontrolAllcode.Stop_Job(sttrJobName) == 1)
                {
                    if (objcontrolAllcode.Start_Job(sttrJobName) == 1)
                    {
                        MessageBox.Show("Restart job successfully", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        datDieukien.Rows[iRow].Cells[2].Value = "Started";
                    }
                    else
                    {
                        MessageBox.Show("Can not restart job", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        datDieukien.Rows[iRow].Cells[2].Value = "Stop";
                    }
                }
                else
                {
                    MessageBox.Show("Can not restart job", Common.sCaption);
                    datDieukien.Rows[iRow].Cells[2].Value = "Stop";
                }                
                //-----------------------lay thong tin de ghilog----------------------
                DateTime dtLog = DateTime.Now;
                string strUser = BR.BRLib.Common.strUsername;
                string useride = BR.BRLib.Common.Userid;
                string strConten = "Job status";
                int Log_level = 1;
                string strWorked = "Restart_job";
                string strTable = "dba_scheduler_jobs";
                string strOld_value = "";
                string strNew_value = "";
                //-----------------------------------------
                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                    strWorked, strTable, strOld_value, strNew_value);
                //--------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //su kien thoat khoi form
        private void frmJobStatus_FormClosing(object sender, FormClosingEventArgs e)
        {

        }        

        private void dtrJob_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRow = e.RowIndex;
                strStatus = datDieukien.Rows[iRow].Cells[2].Value.ToString();
                if (strStatus == "Started")
                {
                    cmdStart.Enabled = false;
                    cmdStop.Enabled = true;
                    cmdRestart.Enabled = true;
                }
                else
                {
                    cmdStart.Enabled = true;
                    cmdStop.Enabled = false;
                    cmdRestart.Enabled = false;
                }
                sttrJobName = datDieukien.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

       
        private void dtrJob_Move(object sender, EventArgs e)
        {
            try
            {
                strStatus = datDieukien.CurrentRow.Cells[2].Value.ToString();
                if (strStatus == "Started")
                {
                    cmdStart.Enabled = false;
                    cmdStop.Enabled = true;
                    cmdRestart.Enabled = true;
                }
                else
                {
                    cmdStart.Enabled = true;
                    cmdStop.Enabled = false;
                    cmdRestart.Enabled = false;
                }
                sttrJobName = datDieukien.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtrJob_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtrJob_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRow = e.RowIndex;
                if (datDieukien.Rows[iRow].Cells[2].Value != null) { strStatus = datDieukien.Rows[iRow].Cells[2].Value.ToString(); }
                if (strStatus == "Started")
                {
                    cmdStart.Enabled = false;
                    cmdStop.Enabled = true;
                    cmdRestart.Enabled = true;
                }
                else
                {
                    cmdStart.Enabled = true;
                    cmdStop.Enabled = false;
                    cmdRestart.Enabled = false;
                }
                if (datDieukien.CurrentRow.Cells[1].Value != null) { sttrJobName = datDieukien.CurrentRow.Cells[1].Value.ToString(); }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmJobStatus_KeyDown(object sender, KeyEventArgs e)
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

        private void frmJobStatus_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dtrJob_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void datDieukien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
