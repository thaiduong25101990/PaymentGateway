using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Text.RegularExpressions;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class frmJobLog : frmBasedata
    {
        #region Ham va bien
        GROUP_MENUController objControlGr = new GROUP_MENUController();
        private clsLog objLog = new clsLog();
        private ALLCODEInfo objAllcode = new ALLCODEInfo();
        private ALLCODEController objctrlAllcode = new ALLCODEController();
        private DATABASESInfo objDb = new DATABASESInfo();
        private IBPS_MSG_LOGInfo objIML = new IBPS_MSG_LOGInfo();
        private IBPS_MSG_LOGController objctrlIML = new IBPS_MSG_LOGController();
        private TTSP_MSG_LOGInfo objTML = new TTSP_MSG_LOGInfo();
        private TTSP_MSG_LOGController objctrlTML = new TTSP_MSG_LOGController();
        private VCB_MSG_LOGInfo objVML = new VCB_MSG_LOGInfo();
        private VCB_MSG_LOGController objctrlVML = new VCB_MSG_LOGController();
        private SWIFT_MSG_LOGInfo objSML = new SWIFT_MSG_LOGInfo();
        private SWIFT_MSG_LOGController objctrlSML = new SWIFT_MSG_LOGController();
        private string mstrChanel = "";
        private bool NeedConfirm = true;
        private static string strWHERE;
        #endregion

        public frmJobLog()
        {
            InitializeComponent();
        }

        /*---------------------------------------------------------------
      * Method           : frmJobLog_Load(object sender, EventArgs e)
      * Muc dich         : //formload
      * Tham so          : 
      * Tra ve           : 
      * Ngay tao         : 10/07/2008
      * Nguoi tao        : QuyND
      * Ngay cap nhat    : 10/07/2008
      * Nguoi cap nhat   : QuyND(Nguyen duc quy)
      *--------------------------------------------------------------*/
        private void frmJobLog_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico"); 
            Getjobcombo();//lay ten cac job co trong he thong
            cmdAdd.Visible = false;
            cmdEdit.Visible = false;
            cmdSave.Visible = false;
            cmdDelete.Visible = false;
            Load_combobox();            
            this.date_from.MaxDate = DateTime.Now;
            this.date_from.MaxDate = date_to.Value;
            this.date_to.MaxDate = DateTime.Now;
            Search_data();//lay du lieu trong bang Msg_log hien tai co trong ngay
        }

        private void Load_combobox()
        {
            try
            {
                DataTable _dt = new DataTable();
                _dt = objControlGr.Select_Gwtype(Common.Userid);
                if (_dt != null)
                {
                    cboGwtype.DataSource = _dt;
                    cboGwtype.DisplayMember = "Gwtype";
                    cboGwtype.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        /*---------------------------------------------------------------
     * Method           : cmdClose_Click(object sender, EventArgs e)
     * Muc dich         : thoat khoi form nay
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
         * Muc dich         : lay ten cac job trong he thong
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 17/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getjobcombo()
        {
            try
            {
                string strUserDB = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                DataSet datJob1 = new DataSet();
                datJob1 = objctrlAllcode.GetJob(strUserDB);
                cbJob_name.Items.Add("ALL");
                cbJob_name.SelectedIndex = 0;
                int ks = 0;
                while (ks < datJob1.Tables[0].Rows.Count)
                {
                    string strJobname = datJob1.Tables[0].Rows[ks]["JOB_NAME"].ToString();
                    cbJob_name.Items.Add(strJobname);
                    ks = ks + 1;
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
        * Muc dich         : lay thong tin trong cac bang _MSG_LOG
        * Tra ve           : 
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 17/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdSeach_Click(object sender, EventArgs e)
        {
            try
            {
                Search_data();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Search_data()
        {
            try
            {
                if (Regex.IsMatch(txtMessage.Text, @"^[0-9]*\z") == true)//neu chi la so
                {
                    string strMessage;
                    string strJobname;
                    string strJobn = cbJob_name.Text;
                    string strSS = strJobn.Substring(0, 3);
                    if (txtMessage.Text == "") { strMessage = ""; } else { strMessage = txtMessage.Text.Trim(); }
                    if (cbJob_name.Text == "ALL") { strJobname = ""; } else { strJobname = cbJob_name.Text; }

                    if (cbJob_name.Text == "ALL" && txtMessage.Text != "")
                    {
                        strWHERE = "AND QUERY_ID ='" + strMessage + "'";
                    }
                    if (cbJob_name.Text != "ALL" && txtMessage.Text == "")
                    {
                        strWHERE = "AND JOB_NAME ='" + strJobname + "'";
                    }
                    if (cbJob_name.Text != "ALL" && txtMessage.Text != "")
                    {
                        strWHERE = "AND JOB_NAME ='" + strJobname + "' AND QUERY_ID ='" + strMessage + "'";
                    }
                    if (cbJob_name.Text == "ALL" && txtMessage.Text == "")
                    {
                        strWHERE = "";
                    }
                    DataSet dat_MSG_LOG = new DataSet();
                    //-----------------------//-----------------------
                    if (cbJob_name.Text != "ALL" || (cbJob_name.Text == "ALL" && txtMessage.Text != ""))
                    {
                        #region job cua kenh thanh toan IBPS
                        if (cboGwtype.Text.Trim() == "IBPS")
                        {
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlIML.SearchIBPS_MSG_LOG(strWHERE, date_from.Value, date_to.Value);
                            int i = 0;
                            while (i < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[i]["JOB_NAME"].ToString();
                                //if (cbJob_name.Text == "ALL") { } else { }
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[i]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[i]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[i]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[i].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[i].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[i].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[i].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[i].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[i].HeaderCell.Value = Convert.ToString(i);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                dtg_MSG_LOG.AutoSize = false;
                                //------------------------------------------------

                                i = i + 1;
                            }
                        }
                        #endregion
                        #region    job cua kenh thanh toan SWIFT
                        if (cboGwtype.Text.Trim() == "SWIFT")
                        {
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlSML.SearchSWIFT_MSG_LOG(strWHERE, date_from.Value, date_to.Value);
                            int j = 0;
                            while (j < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[j]["JOB_NAME"].ToString();
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[j]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[j]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[j]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[j].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[j].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[j].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[j].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[j].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[j].HeaderCell.Value = Convert.ToString(j);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                //------------------------------------------------

                                j = j + 1;
                            }
                        }
                        #endregion
                        #region job cua kenh thanh toan VCB
                        if (cboGwtype.Text.Trim() == "VCB")//job cua kenh thanh toan VCB
                        {
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlVML.SearchVCB_MSG_LOG(strWHERE, date_from.Value, date_to.Value);
                            int k = 0;
                            while (k < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[k]["JOB_NAME"].ToString();
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[k]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[k]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[k]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[k].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[k].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[k].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[k].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[k].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[k].HeaderCell.Value = Convert.ToString(k);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                //------------------------------------------------

                                k = k + 1;
                            }
                        }
                        #endregion
                        #region job cua kenh thanh toan TTSP
                        if (cboGwtype.Text.Trim() == "TTSP")//job cua kenh thanh toan TTSP
                        {
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlTML.SearchTTSP_MSG_LOG(strWHERE, date_from.Value, date_to.Value);
                            int m = 0;
                            while (m < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[m]["JOB_NAME"].ToString();
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[m]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[m]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[m]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[m].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[m].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[m].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[m].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[m].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[m].HeaderCell.Value = Convert.ToString(m);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                //------------------------------------------------

                                m = m + 1;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region IBPS
                        if (cboGwtype.Text.Trim() == "IBPS")
                        {
                            string strWhere1 = "";
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlIML.SearchIBPS_MSG_LOG(strWhere1, date_from.Value, date_to.Value);
                            int i = 0;
                            while (i < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[i]["JOB_NAME"].ToString();
                                //if (cbJob_name.Text == "ALL") { } else { }
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[i]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[i]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[i]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[i].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[i].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[i].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[i].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[i].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[i].HeaderCell.Value = Convert.ToString(i);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                dtg_MSG_LOG.AutoSize = false;
                                //------------------------------------------------

                                i = i + 1;
                            }
                        }
                        #endregion
                        #region VCB
                        if (cboGwtype.Text.Trim() == "VCB")
                        {
                            string strWhere2 = "";
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlVML.SearchVCB_MSG_LOG(strWhere2, date_from.Value, date_to.Value);
                            int k = 0;
                            while (k < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[k]["JOB_NAME"].ToString();
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[k]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[k]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[k]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[k].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[k].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[k].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[k].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[k].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[k].HeaderCell.Value = Convert.ToString(k);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                //------------------------------------------------

                                k = k + 1;
                            }
                        }
                        #endregion
                        #region SWIFT
                        if (cboGwtype.Text.Trim() == "SWIFT")
                        {
                            string strWhere3 = "";
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlSML.SearchSWIFT_MSG_LOG(strWhere3, date_from.Value, date_to.Value);
                            int j = 0;
                            while (j < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[j]["JOB_NAME"].ToString();
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[j]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[j]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[j]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[j].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[j].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[j].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[j].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[j].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[j].HeaderCell.Value = Convert.ToString(j);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                //------------------------------------------------

                                j = j + 1;
                            }
                        }
                        #endregion
                        #region TTSP
                        if (cboGwtype.Text.Trim() == "TTSP")
                        {
                            string strWhere4 = "";
                            dtg_MSG_LOG.Rows.Clear();
                            dat_MSG_LOG = objctrlTML.SearchTTSP_MSG_LOG(strWhere4, date_from.Value, date_to.Value);
                            int m = 0;
                            while (m < dat_MSG_LOG.Tables[0].Rows.Count)
                            {
                                string strJobName = dat_MSG_LOG.Tables[0].Rows[m]["JOB_NAME"].ToString();
                                string strLog_date = dat_MSG_LOG.Tables[0].Rows[m]["LOG_DATE"].ToString();
                                string strStatus = dat_MSG_LOG.Tables[0].Rows[m]["STATUS"].ToString();
                                string strContent = dat_MSG_LOG.Tables[0].Rows[m]["DESCRIPTIONS"].ToString();
                                dtg_MSG_LOG.Rows.Add();
                                dtg_MSG_LOG.Rows[m].Cells[0].Value = strJobName;
                                dtg_MSG_LOG.Rows[m].Cells[1].Value = strLog_date;
                                if (strStatus == "1")
                                {
                                    dtg_MSG_LOG.Rows[m].Cells[2].Value = "Success";
                                }
                                else
                                {
                                    dtg_MSG_LOG.Rows[m].Cells[2].Value = "Error";
                                }
                                dtg_MSG_LOG.Rows[m].Cells[3].Value = strContent;
                                //---------------dinh dang luoi--------------------
                                dtg_MSG_LOG.Rows[m].HeaderCell.Value = Convert.ToString(m);
                                dtg_MSG_LOG.Columns[0].Width = 200;
                                dtg_MSG_LOG.Columns[1].Width = 120;
                                dtg_MSG_LOG.Columns[2].Width = 50;
                                dtg_MSG_LOG.Columns[3].Width = 400;
                                dtg_MSG_LOG.Columns[0].ReadOnly = true;
                                dtg_MSG_LOG.Columns[1].ReadOnly = true;
                                dtg_MSG_LOG.Columns[2].ReadOnly = true;
                                dtg_MSG_LOG.Columns[3].ReadOnly = true;
                                //------------------------------------------------

                                m = m + 1;
                            }
                        #endregion

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : frmJobLog_FormClosing(object sender, FormClosingEventArgs e)
        * Muc dich         : //su kien thoat khoi form
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmJobLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (NeedConfirm == true)
                    e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit", Common.sCaption);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmJobLog_KeyDown(object sender, KeyEventArgs e)
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

        private void date_from_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (date_from.Checked == false)
                {
                    date_to.Checked = false;
                }
                else
                {
                    date_to.Checked = true;
                }
                if (date_from.Value > date_to.Value)
                {
                    date_from.Value = date_to.Value;
                }                
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
                {
                    date_from.Checked = false;
                }
                else
                {
                    date_from.Checked = true;
                }
                if (date_to.Value < date_from.Value)
                {
                    this.date_to.Value = date_from.Value;
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmJobLog_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dtg_MSG_LOG_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
