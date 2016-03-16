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
using System.Text.RegularExpressions;
using System.Threading;

namespace BR.BRIBPS
{
    public partial class frmIBPSResend : BR.BRSYSTEM.frmBasedata
    {
        private clsLog objLog = new clsLog();  

        #region dinh nghia cac datatable && dataset
        private DataTable _dtSearch;
        private DataSet _dsAll_code;
        private DataSet _dsMaster;   
        private DataTable _dtViews;
        public DataTable dsPrint;
        private static DataTable _dtSearchAV;
        #endregion

        #region dinh nghia cac ham trong lop BusinessObject
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();
        Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        USERSInfo objUser = new USERSInfo();
        USERSController objctrlUser = new USERSController();
        TADInfo objTad = new TADInfo();
        TADController objcontrolTad = new TADController();
        IBPS_MSG_CONTENTInfo objcontent = new IBPS_MSG_CONTENTInfo();
        IBPS_MSG_CONTENTController objcontrolcontent = new IBPS_MSG_CONTENTController();
        SEARCHInfo objSearch = new SEARCHInfo();
        SEARCHController objsearchcontrol = new SEARCHController();
        frmIQSList frmIQSlist = new frmIQSList();
        CURRENCYInfo objCurent = new CURRENCYInfo();
        CURRENCYController objCtrlcurrent = new CURRENCYController();
        clsCheckInput clsCheck = new clsCheckInput();
        IBPS_MSG_LOGController objctLog = new IBPS_MSG_LOGController();
        IBPS_MSG_LOGInfo objLogInfo = new IBPS_MSG_LOGInfo();
        #endregion

        #region dinh nghia  cac bien
        string HO;
        public static int iFlag = 0;
        public static int iLenght;
        public int selectedRow;    
        private static int iSearch;
        private static int iRow;
        private int iRows;
        private bool bIsCloseForm = false;         
        private int iSelect;        
        private string strUserName;
        public string datetimefrom;
        public string datetimeto;
        public string Direction = "";
        //-----------------------------------------------
        private string dtFromd;//ngay
        private string dtFromm;//thang 
        private string dtTod;//ngay
        private string dtTom;//thang 
        private string dtNowm;//thang
        private string dtNowd;//ngay
        private string dtFrom_one2;//dtFrom_one3
        private string dtFrom_one3;       
        #endregion

        public frmIBPSResend()
        {
            InitializeComponent();
        }
        private void UserName()
        {
            try
            {
                string strUserID = Common.Userid;
                DataTable datUU = new DataTable();
                datUU = objctrlUser.GetUSERS_PASS1(strUserID.Trim());
                strUserName = datUU.Rows[0]["USERNAME"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Enable_controls1()
        {
            try
            {
                //enable va dinh vi cac controls------------------------------------------------
                dataMessage.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataMessage.Columns[0].HeaderCell = dgvColumnHeader;
                dataMessage.Columns[0].Width = 40;
                cmdAdd.Visible = false; cbCheck.Enabled = false;
                cmdEdit.Visible = false; cmdSave.Visible = false;
                cmdDelete.Visible = false;
                this.datefrom.MaxDate = DateTime.Now;
                this.datefrom.MaxDate = dateto.Value;
                this.dateto.MaxDate = DateTime.Now;
                this.Text = "IBPS resend message management";
                iLenght = 0; iSearch = 0;
                this.cmdAdvance.Location = new Point(cmdNornal.Location.X, cmdNornal.Location.Y);
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                this.grbSearch.Location = new Point(grbSearchnhanh.Location.X, grbSearchnhanh.Location.Y);
                this.grbSearch.Size = new Size(grbSearch.Size.Width, grbSearchnhanh.Size.Height + 25);
                //-------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
         * Muc dich         : Form khi load len
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 10/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void frmIBPSResend_Load(object sender, EventArgs e)
        {
            try
            {                
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");                
                UserName();// lay thong tin ten nguoi dang nhap
                Enable_controls1();//ham enable va dinh vi cac controls           
                //Ham lay du lieu----------------------------------- --------------------------------------
                //dataMessage = clsDatagridviews.IBPS_MSG_CONTENT_LOAD_RESEND(dataMessage);
                //_dtViews = (DataTable)dataMessage.DataSource;//day ra mot datatable de in dien------------- 
                dsPrint = _dtViews;    
                GetParam(); 
                Enable_controls();
                // Lay het cac trang thai cua IBPS---------------------------------------------------------
                IBPS_STATUS();
                datefrom.Select();
                Searchdata();
                //cboStatus.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void IBPS_STATUS()
        {
            try
            {
                _dsAll_code = clsSTATUS.GET_ALL_STATUS();
                _dsMaster = _dsAll_code.Copy();
                Getdatacombo();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }



        /*---------------------------------------------------------------
         * Muc dich         : Ham lay du lieu len combobox trong truong hop tim kiem binh thuong
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 10/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getdatacombo()
        {
            try
            {
                cbHV_LV.SelectedIndex = 0;
                // Combobox STATUS----------------------------                
                DataRow _dtr = _dsAll_code.Tables["STATUS"].NewRow();
                _dtr[0] = 99; _dtr[1] = "ALL";
                _dsAll_code.Tables["STATUS"].Rows.InsertAt(_dtr, 0);
                cbStatus.DataSource = _dsAll_code.Tables["STATUS"];
                cbStatus.DisplayMember = "NAME";
                cbStatus.ValueMember = "STATUS";
                //cbStatus.SelectedValue = Common.STATUS_SENT;
                //cboStatus.Enabled = false;

                // Combobox DEPARTMENT----------------------------                
                DataRow _dtr1 = _dsAll_code.Tables["DEPARTMENT"].NewRow();
                _dtr1[0] = ""; _dtr1[1] = "ALL";
                _dsAll_code.Tables["DEPARTMENT"].Rows.InsertAt(_dtr1, 0);
                cbdepartment.DataSource = _dsAll_code.Tables["DEPARTMENT"];
                cbdepartment.DisplayMember = "CONTENT";
                cbdepartment.ValueMember = "CDVAL";
                cbdepartment.Text = "ALL";
                // Combobox MSGDirection----------------------------                
                //DataRow _dtr2 = _dsAll_code.Tables["MSGDIRECTION"].NewRow();
                //_dtr2[0] = ""; _dtr2[1] = "ALL";
                //_dsAll_code.Tables["MSGDIRECTION"].Rows.InsertAt(_dtr2, 0);
                cbMsgDirection.DataSource = _dsAll_code.Tables["MSGDIRECTION"];
                cbMsgDirection.DisplayMember = "CONTENT";
                cbMsgDirection.ValueMember = "CDVAL";
                cbMsgDirection.Text = "SIBS-IBPS";
                // Combobox FWSTS----------------------------                
                DataRow _dtr3 = _dsAll_code.Tables["FWSTS"].NewRow();
                _dtr3[0] = ""; _dtr3[1] = "ALL";
                _dsAll_code.Tables["FWSTS"].Rows.InsertAt(_dtr3, 0);
                cboFWStatus.DataSource = _dsAll_code.Tables["FWSTS"];
                cboFWStatus.DisplayMember = "CONTENT";
                cboFWStatus.ValueMember = "CDVAL";
                cboFWStatus.Text = "ALL";
                // Combobox TAD----------------------------                
                DataRow _dtr4 = _dsAll_code.Tables["TAD"].NewRow();
                _dtr4[0] = "ALL"; _dtr4[1] = "";
                _dsAll_code.Tables["TAD"].Rows.InsertAt(_dtr4, 0);
                cbtab.DataSource = _dsAll_code.Tables["TAD"];
                cbtab.DisplayMember = "TAD";
                cbtab.ValueMember = "SIBS_BANK_CODE";
                cbtab.Text = "ALL";
                // Combobox Currency----------------------------                
                DataRow _dtr5 = _dsAll_code.Tables["CURRENCY"].NewRow();
                _dtr5[0] = "ALL";
                _dsAll_code.Tables["CURRENCY"].Rows.InsertAt(_dtr5, 0);
                cbCurrency.DataSource = _dsAll_code.Tables["CURRENCY"];
                cbCurrency.DisplayMember = "CCYCD";
                cbCurrency.Text = "ALL";
                //MSG_SRC
                DataRow _dtr6 = _dsAll_code.Tables["MSG_SRC"].NewRow();
                _dtr6[0] = ""; _dtr6[1] = "ALL";
                _dsAll_code.Tables["MSG_SRC"].Rows.InsertAt(_dtr6, 0);
                cboMSG_SRC.DataSource = _dsAll_code.Tables["MSG_SRC"];
                cboMSG_SRC.DisplayMember = "CONTENT";
                cboMSG_SRC.ValueMember = "CDVAL";
                cboMSG_SRC.Text = "ALL";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Format()
        {
            try
            {
                if (txtAmount.Text.Trim() != "")
                {
                    if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                    {
                        txtAmount.Text = Common.FormatCurrency(txtAmount.Text.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Test()
        {
            try
            {
                DateTime dateNow = DateTime.Now;//thoi gian cua he thong
                string dtNowy = Convert.ToString(dateNow.Year);
                if (dateNow.Day.ToString().Length == 2)
                { dtNowd = Convert.ToString(dateNow.Day); }
                else if (dateNow.Day.ToString().Length == 1) { dtNowd = "0" + Convert.ToString(dateNow.Day); }
                if (dateNow.Month.ToString().Length == 2)
                { dtNowm = Convert.ToString(dateNow.Month); }
                else if (dateNow.Month.ToString().Length == 1) { dtNowm = "0" + Convert.ToString(dateNow.Month); }
                int dtNow = Convert.ToInt32(dtNowy + dtNowm + dtNowd);
                //-----------------------------------------------------------------------
                DateTime dateFrom = datefrom.Value;//thoi gian bat dau                
                string dtFromy = Convert.ToString(dateFrom.Year);
                if (dateFrom.Day.ToString().Length == 2)
                { dtFromd = Convert.ToString(dateFrom.Day); }
                else if (dateFrom.Day.ToString().Length == 1) { dtFromd = "0" + Convert.ToString(dateFrom.Day); }
                if (dateFrom.Month.ToString().Length == 2)
                { dtFromm = Convert.ToString(dateFrom.Month); }
                else if (dateFrom.Month.ToString().Length == 1) { dtFromm = "0" + Convert.ToString(dateFrom.Month); }
                int dtFrom = Convert.ToInt32(dtFromy + dtFromm + dtFromd);
                //-------------------------------------------------------------------------------------------
                DateTime dateTo = dateto.Value;//thoi gian ket thuc
                string dtToy = Convert.ToString(dateTo.Year);
                if (dateTo.Day.ToString().Length == 2)
                { dtTod = Convert.ToString(dateTo.Day); }
                else if (dateTo.Day.ToString().Length == 1) { dtTod = "0" + Convert.ToString(dateTo.Day); }
                if (dateTo.Month.ToString().Length == 2)
                { dtTom = Convert.ToString(dateTo.Month); }
                else if (dateTo.Month.ToString().Length == 1) { dtTom = "0" + Convert.ToString(dateTo.Month); }
                int dtTo = Convert.ToInt32(dtToy + dtTom + dtTod);
                //-------------------------------------------------------------------------------------------
                if (dtFrom == dtTo)//neu thoi gian hai tieu chi nay bang nhau
                {
                    if (dtFrom == dtNow)//trung ngay he thong(lay trong bang content)
                    {
                    }
                    else if (dtFrom < dtNow)//khac ngay he thong()
                    {
                        int dtNow_One = Convert.ToInt32(dtNowy + dtNowm + "01");
                        if ((dtFrom == dtNow_One) || (dtFrom > dtNow_One))//select trong bang All
                        {
                        }
                        else if (dtFrom < dtNow_One)//select trong bang All_his
                        {
                        }
                    }
                }
                else if (dtFrom < dtTo)//neu thoi gian from nho hon thoi gian to
                {
                    if (Convert.ToString((Convert.ToInt32(dtNowm) - 1)).Length == 1)
                    {
                        dtFrom_one3 = "0" + Convert.ToString((Convert.ToInt32(dtNowm) - 1));
                    }
                    else
                    {
                        dtFrom_one3 = Convert.ToString((Convert.ToInt32(dtNowm) - 1));
                    }
                    int dtNow_One = Convert.ToInt32(dtFromy + dtNowm + "01");
                    if ((dtTo > dtNow_One) || (dtTo == dtNow_One))//neu thoi gian cuoi cung bang ngay dau tien cua thang hien tai
                    {
                        if (Convert.ToString((Convert.ToInt32(dtTom) - 1)).Length == 1)
                        {
                            dtFrom_one2 = "0" + Convert.ToString((Convert.ToInt32(dtTom) - 1));
                        }
                        else
                        {
                            dtFrom_one2 = Convert.ToString((Convert.ToInt32(dtTom) - 1));
                        }
                        //----------------------------------------------------------------------------------
                        int dtFrom_one1 = Convert.ToInt32((dtToy + dtFrom_one2 + "01"));
                        if ((dtFrom > dtFrom_one1) || (dtFrom == dtFrom_one1))//select trong bang (All va All_HIS)
                        {
                            DateTime datennm = DateTime.Now;
                            if (dateto.Value.Day < datennm.Day)//all,All_his
                            {
                            }
                            else if (dateto.Value.Day == datennm.Day)//ba bang
                            {
                            }
                        }
                        else if (dtFrom < dtFrom_one1)//select trong bang (Content, All, All_his)
                        {
                            int dtYY = dateto.Value.Year;
                            int dtYYN = DateTime.Now.Year;
                            if (dtYY < dtYYN)//lay trong bang All_his
                            {
                            }
                            else if (dtYY == dtYYN)//select trong bang (Content, All, All_his)
                            {
                                DateTime datyy = DateTime.Now;
                                if (dateto.Value.Month < DateTime.Now.Month)//lay trong bang All_his
                                {
                                }
                                else if (dateto.Value.Month == datyy.Month)
                                {
                                    if (datefrom.Value.Month <= datyy.Month - 1)//content,
                                    {
                                        if (dateto.Value.Day < datyy.Day)//all,All_his
                                        {
                                        }
                                        else if (dateto.Value.Day == datyy.Day)//ba bang
                                        {
                                        }
                                    }
                                    else if (datefrom.Value.Month > datyy.Month - 1)//content
                                    {
                                    }
                                }
                            }
                        }
                    }//&& (dtTo > Convert.ToInt32((dtFromy + dtFrom_one3 + "01")))
                    else if ((dtTo < dtNow_One))//cung thang cua ngay he thong
                    {

                        int dtFrom_one1 = Convert.ToInt32((dtToy + dtFrom_one2 + "01"));
                        if ((dtFrom > dtFrom_one1) || (dtFrom == dtFrom_one1))//select trong bang (All_HIS)
                        {
                        }
                        else if (dtFrom < (Convert.ToInt32((dtToy + dtFrom_one2 + "01"))))//lay trong bang (all_HIS va All)
                        {
                            DateTime datt = DateTime.Now;
                            if (dateto.Value.Month < datt.Month)//lay trong bang all his
                            {
                            }
                            else if (dateto.Value.Month == datt.Month)// All_HIS,All
                            {
                            }
                        }
                    }
                    else if (dtTo < Convert.ToInt32((dtFromy + dtFrom_one2 + "01")))//lay trong bang all_his
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : Ham tim kiem du lieu
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 10/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Search_Click(object sender, EventArgs e)
        {          
            Format();
            Searchdata();
            Enable_controls();
        }

        private void Enable_controls()
        {
            try
            {
                label12.Text = "Total number of messages :" + Convert.ToString(dataMessage.Rows.Count);
                if (dataMessage.Rows.Count == 0)
                { cmdExport.Enabled = false; cmdview.Enabled = false; cmdPrint.Enabled = false; cmdResend.Enabled = false; }
                else
                { cmdExport.Enabled = true; cmdview.Enabled = true; cmdPrint.Enabled = true; cmdResend.Enabled = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Searchdata()
        {
            try
            {
                string Where = "";  
                if (iSearch == 1)//tim kiem nang cao
                {                    
                    Where = clsDatagridviews.SQL_ADVANCE(datDieukien);
                    dataMessage.DataSource = 0;
                    
                    label12.Text = "Total number of messages : 0";
                    if (Where.Trim() != "")
                    { dataMessage = clsDatagridviews.IBPS_MSG_CONTENT_SEARCH_ADVANCE_RS(Where.Trim(), dataMessage); }//Lay du lieu theo dieu kien where                 
                    if (dataMessage.Rows.Count != 0) { _dtViews = (DataTable)dataMessage.DataSource; dsPrint = _dtViews; }
                }
                else//tim kiem thong thuong
                {
                    Where = clsDatagridviews.Search_Normal(grbSearch);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    dataMessage = clsDatagridviews.IBPS_MSG_CONTENT_SEARCH_RS(datefrom.Value, dateto.Value, Where, dataMessage);//Lay du lieu theo dieu kien where                 
                    if (dataMessage.Rows.Count != 0) { _dtViews = (DataTable)dataMessage.DataSource; dsPrint = _dtViews; }
                    
                    ResetParam();
                    GetParam(); 
                }
                if (dataMessage.Rows.Count>0)
                {
                    FomatGrid.Color_datagrid(dataMessage);
                }
                label12.Text = "Total number of messages : " + Convert.ToString(dataMessage.Rows.Count);                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        //chuyen dang tim kiem nang cao
        private void cmdAdvance_Click(object sender, EventArgs e)
        {
            cbColumns.Focus();
            label12.Top = label12.Top - 20;
            Lbtong.Top = Lbtong.Top - 20;
            dataMessage.Top = dataMessage.Top - 20;
            dataMessage.Height = dataMessage.Height + 20;            
            iSearch = 1;
            #region Quynd comment 20100319
            //_dtSearch = objsearchcontrol.COLUMNS_SEARCH("IBPS", out _dtSearch);
            //cbCheck.DataSource = _dtSearch;
            //cbCheck.DisplayMember = "FIELDNAME";
            //cbCheck.ValueMember = "OPERATOR"; 
            //cbColumns.DataSource = _dtSearch;
            //cbColumns.ValueMember = "FIELDCODE";
            //cbColumns.DisplayMember = "FIELDNAME";
            #endregion

            try
            {
                #region Quyndcap nhat 20100319
                _dtSearchAV = objsearchcontrol.dtSearch("IBPS");
                if (_dtSearchAV != null)
                {
                    cbColumns.DataSource = _dtSearchAV;
                    cbColumns.ValueMember = "FIELDCODE";
                    cbColumns.DisplayMember = "FIELDNAME";
                }
                #endregion
                cmdAdvance.Hide();
                grbSearch.Hide();
                grbSearchnhanh.Show();
                grbDieukien.Show();               
                cbColumns.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        
        private void HeaderText(DataGridView Datagrid)
        {
            try
            {
                int c = 0;
                while (c < Datagrid.Rows.Count)
                {
                    Datagrid.Rows[c].HeaderCell.Value = Convert.ToString(c);
                    c = c + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        

       
        /*---------------------------------------------------------------
         * Muc dich         : lay du lieu len combobox
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 10/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/

        private void cmdNornal_Click(object sender, EventArgs e)
        {
            try
            {
                datefrom.Focus();
                label12.Top = label12.Top + 20;
                Lbtong.Top = Lbtong.Top + 20;
                dataMessage.Top = dataMessage.Top + 20;
                dataMessage.Height = dataMessage.Height - 20;
                iSearch = 0;

                cmdAdvance.Show();
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                this.grbSearch.Location = new Point(10, grbSearch.Location.X);
                this.grbSearch.Location = new Point(13, grbSearch.Location.Y);
                grbSearch.Show();
                datefrom.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
        * Muc dich         : Click vao combobox lay du lieu len com bo box khac
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 10/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cbbAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string pColumn = cbColumns.SelectedValue.ToString().Trim();
                for (int i = 0; i < _dtSearchAV.Rows.Count; i++)
                {
                    string pCl = _dtSearchAV.Rows[i]["FIELDCODE"].ToString().Trim();
                    if (pCl.ToUpper() == pColumn.ToUpper())
                    {
                        string pControl = _dtSearchAV.Rows[i]["CONTROL"].ToString().Trim().ToUpper();
                        string pContent = _dtSearchAV.Rows[i]["CONTENT"].ToString().Trim();
                        string pOpra = _dtSearchAV.Rows[i]["OPERATOR"].ToString().Trim();
                        if (pControl == "TextBox".ToUpper())/*Neu la TextBox*/
                        {
                            txtValue.Visible = true; dateValue.Visible = false; cboStatus.Visible = false;
                        }
                        else if (pControl == "Combobox".ToUpper())/*Neu la Combobox*/
                        {
                            DataTable _dt = new DataTable();
                            _dt = objsearchcontrol.Excute_Select(pContent);
                            txtValue.Visible = false; cboStatus.Visible = true;
                            cboStatus.DataSource = _dt;
                            cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                            this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                            //if (pCl.ToUpper() == "STATUS")
                            //{
                            //    cboStatus.SelectedValue = Common.STATUS_SENT;
                            //    cboStatus.Enabled = false;
                            //}
                        }
                        else if (pControl == "dateTimePicker".ToUpper())/*Neu la dateTimePicker*/
                        {
                            txtValue.Visible = false; cboStatus.Visible = false; dateValue.Visible = true;
                            this.dateValue.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                        }
                        /*Lay du lieu dieu kien*/
                        cbOperator.Items.Clear();
                        String[] M = pOpra.Split(new String[] { "," }, StringSplitOptions.None);
                        for (int j = 0; j < M.Count<String>(); j++)
                        {
                            cbOperator.Items.Add(M[j]);
                        }
                        cbOperator.SelectedIndex = 0;
                        break;
                    }
                }

                #region quynd comment 20100319
                //cboStatus.Enabled = true;
                //cbCheck.Text = cbColumns.Text;
                //if (cbColumns.SelectedValue.ToString() == "TRANSDATE" || cbColumns.SelectedValue.ToString() == "RECEIVING_TIME" || cbColumns.SelectedValue.ToString() == "SENDING_TIME")
                //{
                //    //iVisible = 1; 
                //    txtValue.Visible = false;  cboStatus.Visible = false;  dateValue.Visible = true;
                //    this.dateValue.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "STATUS")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["STATUS"];
                //    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "STATUS";
                //    cboStatus.SelectedValue = Common.STATUS_SENT;
                //    cboStatus.Enabled = false;
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["CURRENCY"];
                //    cboStatus.DisplayMember = "CCYCD"; 
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "PRINT_STS")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["PRINT_STS"];
                //    cboStatus.DisplayMember = "CONTENT";
                //    cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["DEPARTMENT"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSGDIRECTION"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "TAD")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["TAD"];
                //    cboStatus.DisplayMember = "TAD"; cboStatus.ValueMember = "SIBS_BANK_CODE";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "FWSTS")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["FWSTS"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "ERR_CODE")
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["ERROR_CODE"];
                //    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "ERROR_CODE";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else
                //{   
                //    //iVisible = 0; 
                //    txtValue.Visible = true; dateValue.Visible = false; cboStatus.Visible = false; 
                //}

                //cbOperator.Items.Clear();
                //if (cbCheck.SelectedValue != null)
                //{
                //    string strOPERATOR = cbCheck.SelectedValue.ToString();
                //    String[] M = strOPERATOR.Split(new String[] { "," }, StringSplitOptions.None);//cat chuoi
                //    int k = M.Count<String>();
                //    int j = 0;
                //    while (j < k)
                //    {
                //        cbOperator.Items.Add(M[j]);
                //        j = j + 1;
                //    }
                //    cbOperator.SelectedIndex = 0;
                //}
                //-----------------------------------------------------------------      
                #endregion 
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //ham removecac hang duoc chon trong luoi
        private void cmdremove_Click(object sender, EventArgs e)
        {
            try
            {
                int m = 0;
                while (m < datDieukien.Rows.Count)
                {
                    if (datDieukien.Rows[m].Cells[0].Value.ToString() == "False")// hang duoc chon
                    {
                        datDieukien.Rows.RemoveAt(m);//xoa dong duoc chon
                    }
                    else
                    {
                        m = m + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //xoa toen bo luoi
        private void cmdremoveall_Click(object sender, EventArgs e)
        {
            try
            {
                datDieukien.Rows.Clear();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        } 
        
        private void cmdview_Click(object sender, EventArgs e)
        {
            try
            {                
                Check_Select_Rows();//kiem tra xem co dong nao duoc check khong
                if (iSelect == 0)
                {
                    bIsCloseForm = false;
                    if (iRows == -1) { iRows = 0; }
                    View_data(iRows); }
                else
                {
                    int d = 0;
                    while (d < dataMessage.Rows.Count)
                    {
                        bIsCloseForm = false;
                        if (dataMessage.Rows[d].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataMessage.Rows[d].Cells[0].Value.ToString() == "True")
                            {  View_data(d); }
                        }                        
                        if (bIsCloseForm == true)
                        {
                            int p = 0;                            
                            while (p < dataMessage.Rows.Count)
                            {
                                dataMessage.Rows[p].Cells[0].Value = null;
                                p = p + 1;
                                dgvColumnHeader.CheckAll = false;
                            }
                            return;
                        }
                        else
                        {  d = d + 1; }
                    }
                }
                Format();
                Searchdata();
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private string Fomat_amount(string pAmount)
        {
            try
            {
                string pReturn = "";
                if (pAmount.Trim() != "")
                {
                    if (Regex.IsMatch(pAmount.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)
                    {
                        pReturn = Common.FormatCurrency(pAmount, Common.FORMAT_CURRENCY);
                    }
                }
                return pReturn;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return "";
            }
        }

        /*---------------------------------------------------------------
       * Muc dich         : Ad sang Datagridview
       * Tra ve           : Mot danh sach cac File - List<FileInfo>
       * Ngay tao         : 26/54/2008
       * Nguoi tao        : Quynd
       * Ngay cap nhat    : 10/06/2008
       * Nguoi cap nhat   : Quynd
       *--------------------------------------------------------------*/
        private void cmdAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                Add_datagridview();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Add_datagridview()
        {
            try
            {
                string pShow = "";
                string pSearch = "";
                if (cbColumns.SelectedValue.ToString() == "AMOUNT")
                {
                    txtValue.Text = Fomat_amount(txtValue.Text.Trim());
                }
                if (txtValue.Visible == true)/*Dang hien thi textbox*/
                {
                    if (cbOperator.Text.Trim() == "LIKE")/*Neu search theo menh de like*/
                    {
                        pShow = cbColumns.Text + " " + cbOperator.Text + " " + txtValue.Text + " ";
                        pSearch = cbColumns.SelectedValue.ToString() + " " + cbOperator.Text + " '%" + txtValue.Text.Replace(",", "") + "%' ";
                    }
                    else/*Khong su dung menh de like*/
                    {
                        if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                        {
                            #region /*Doan nay phai code cung*/
                            if ((cbColumns.SelectedValue.ToString().Trim().ToUpper() == "TAD" ||
                                cbColumns.SelectedValue.ToString().Trim().ToUpper() == "F07" ||
                                cbColumns.SelectedValue.ToString().Trim().ToUpper() == "F19" ||
                                cbColumns.SelectedValue.ToString().Trim().ToUpper() == "F21" ||
                                cbColumns.SelectedValue.ToString().Trim().ToUpper() == "F21") && (txtValue.Text.Trim().Length < 5))
                            {
                                pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " Upper(Lpad('" + txtValue.Text.Replace(",", "") + "',5,'0')) ";
                            }
                            else if (cbColumns.SelectedValue.ToString().Trim().ToUpper() == "RM_NUMBER")
                            {
                                pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " Upper(Lpad('" + txtValue.Text.Replace(",", "") + "',19,'0')) ";
                            }
                            else if (cbColumns.SelectedValue.ToString().Trim().ToUpper() == "AMOUNT")
                            {
                                pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                pSearch = "  " + cbColumns.SelectedValue.ToString() + " " + cbOperator.Text + " Upper('" + txtValue.Text.Replace(",", "") + "') ";
                            }
                            else
                            {
                                if (cbColumns.SelectedValue.ToString().Trim().ToUpper() == "AMOUNT")
                                {
                                    pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                    pSearch = "  " + cbColumns.SelectedValue.ToString() + " " + cbOperator.Text + " Upper('" + txtValue.Text.Replace(",", "") + "') ";
                                }
                                else
                                {
                                    pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                    pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " Upper('" + txtValue.Text.Replace(",", "") + "') ";
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            pShow = cbColumns.Text + " " + cbOperator.Text + " " + txtValue.Text + " ";
                            pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " '" + txtValue.Text.Replace(",", "") + "' ";
                        }
                    }
                }
                else if (cboStatus.Visible == true)/*Dang hien thi combobox*/
                {
                    pShow = cbColumns.Text + " " + cbOperator.Text + " " + cboStatus.Text + " ";
                    if (cbColumns.SelectedValue.ToString().Trim().ToUpper() == "TAD")
                    {
                        pSearch = cbColumns.SelectedValue.ToString() + " " + cbOperator.Text + " Lpad('" + cboStatus.SelectedValue.ToString() + "',5,'0') ";
                    }
                    else
                    {
                        pSearch = cbColumns.SelectedValue.ToString() + " " + cbOperator.Text + " '" + cboStatus.SelectedValue.ToString() + "' ";
                    }
                }
                else if (dateValue.Visible == true)/*Dang hien thi datetime*/
                {
                    #region Ngay thang
                    string pDay = ""; string pMonth = ""; string pYear = "";
                    if (dateValue.Value.Day.ToString().Length == 1) { pDay = "0" + dateValue.Value.Day.ToString(); }
                    else { pDay = dateValue.Value.Day.ToString(); }
                    if (dateValue.Value.Month.ToString().Length == 1) { pMonth = "0" + dateValue.Value.Month.ToString(); }
                    else { pMonth = dateValue.Value.Month.ToString(); }
                    pYear = dateValue.Value.Year.ToString();
                    string pValue = pYear + pMonth + pDay;
                    #endregion
                    pShow = cbColumns.Text + " " + cbOperator.Text + " " + dateValue.Text + " ";
                    if (cbColumns.SelectedValue.ToString().Trim().ToUpper() == "TRANSDATE")
                    {
                        pSearch = " " + cbColumns.SelectedValue.ToString() + "  " + cbOperator.Text + " '" + pValue + "' ";
                    }
                    else
                    {
                        pSearch = " To_char(" + cbColumns.SelectedValue.ToString() + ",'YYYYMMDD')  " + cbOperator.Text + " '" + pValue + "' ";
                    }
                }
                datDieukien = clsDatagridviews.AddDatagrid(pShow, pSearch, datDieukien);
                txtValue.Text = "";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        //ham add du lieu len datagrid search
        private void Condition_search()
        {
            string Condition = "";
            string Condition_one = "";
            string Condition_two = "";
            try
            {
                #region search theo like
                if (cbOperator.Text.Trim() == "LIKE")//MSG_TYPE
                {
                    Condition = clsDatagridviews.Search_condition(cbColumns, cbOperator, txtValue, cboStatus, dateValue, true);
                    String[] N = Condition.Split(new String[] { "$$$$" }, StringSplitOptions.None);//cat chuoi
                    Condition_one = N[0];
                    Condition_two = N[1];
                }
                #endregion
                #region khong phai search theo like
                else
                {
                    Condition = clsDatagridviews.Search_condition(cbColumns, cbOperator, txtValue, cboStatus, dateValue, false);
                    String[] N = Condition.Split(new String[] { "$$$$" }, StringSplitOptions.None);//cat chuoi 
                    Condition_one = N[0];
                    Condition_two = N[1];
                }
                #endregion
                // goi ham add vao datagridview mot dong 
                //gia tri truyen vao la hai chuoi va mot datagridview
                if (txtValue.Visible == true)
                {
                    if (txtValue.Text != "")
                    { datDieukien = clsDatagridviews.AddDatagrid(Condition_one, Condition_two, datDieukien); }
                }
                else
                {
                    datDieukien = clsDatagridviews.AddDatagrid(Condition_one, Condition_two, datDieukien);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        //thoat khoi form
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //HaNTT10
        private void cmdIQS_Click(object sender, EventArgs e)
        {
            
        }

        private void dataMessage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdview.Enabled = true;                
                cmdPrint.Enabled = true;
                cmdResend.Enabled = true;
                iRows = e.RowIndex;
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dataMessage.RowCount; i++)
                        {
                            this.dataMessage.EndEdit();
                            string re_value = this.dataMessage.Rows[i].Cells[0].EditedFormattedValue.ToString();
                        }
                    }
                }
                if (e.RowIndex != -1)
                {
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {

            DataTable datPrint = new DataTable();
            DataRow[] datRow;
            DataRow datRow1;

            for (int i = 0; i < dsPrint.Columns.Count; i++)
            {
                DataColumn datColum = new DataColumn(dsPrint.Columns[i].ColumnName, dsPrint.Columns[i].DataType);
                datPrint.Columns.Add(datColum);
            }

            try
            {
                frmPrint frmPrint = new frmPrint();
                Check_Select_Rows();
                if (iSelect == 0)//khong click chon otext
                {
                    frmPrint.HMdat = dsPrint;
                }
                else
                {
                    //objcontrolcontent.PRINT_TTSP_LIST_DELETE(); 
                    int f = 0;
                    while (f < dataMessage.Rows.Count)
                    {
                        if (dataMessage.Rows[f].Cells[0].Value != null && dataMessage.Rows[f].Cells[0].Value.ToString() == "True")// hang khong duoc chon
                        {
                            string strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString().Trim();
                            //int k = 0;

                            datRow = dsPrint.Select("MSG_ID=" + Convert.ToInt32(strMSG_ID));
                            datRow1 = datPrint.NewRow();
                            for (int j = 0; j < dsPrint.Columns.Count; j++)
                            {
                                datRow1[j] = datRow[0][j];
                            }
                            datPrint.Rows.Add(datRow1);

                        }
                        f = f + 1;
                    }
                    frmPrint.HMdat = datPrint;
                }
                string Print = "IBPS_PRINT_ALL";
                frmPrint.PrintType = Print;
                frmPrint.dtfrom = datetimefrom;
                frmPrint.dtto = datetimeto;
                frmPrint.Direction = Direction;
                frmPrint.WindowState = FormWindowState.Maximized;
                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void dataMessage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                cmdview.Enabled = true;               
                cmdPrint.Enabled = true;
                cmdResend.Enabled = true;
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (datefrom.Checked == false)
                {   dateto.Checked = false;  }
                else
                {  dateto.Checked = true;   }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateto.Checked == false)
                {   datefrom.Checked = false; }
                else { datefrom.Checked = true; }
                if (dateto.Value < datefrom.Value)
                {  this.dateto.Value = datefrom.Value; }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }            

        private void frmIBPSResend_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }


        #region  //enterKey(...): Bắt phím Enter để đăng nhập
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {               
                this.Close();
            }
            else if (e.KeyChar == (char)13)
            {
                if (cbColumns.Focused)
                {  cbOperator.Focus(); cbOperator.SelectAll(); }
                if (cbOperator.Focused)
                {
                    if (txtValue.Visible == true)
                    { txtValue.Focus(); txtValue.SelectAll(); }
                    else if (dateValue.Visible == true)
                    { dateValue.Focus(); dateValue.Select(); }
                    else if (cboStatus.Visible == true)
                    {  cboStatus.Focus();  cboStatus.SelectAll(); }
                }
                else if (txtValue.Focused || dateValue.Focused || cboStatus.Focused)
                { cmdAdd1.Focus();  cmdAdd1_Click(null, null); }
                else if (cmdAdd1.Focused)
                {   cmdremove.Focus(); cmdremove_Click(null, null); }
                else if (cmdremove.Focused)
                { cmdremoveall.Focus(); cmdremoveall_Click(null, null); }
                else if (cmdremoveall.Focused)
                { datDieukien.Focus(); }
                else if (datDieukien.Focused)
                { cmdSearch.Focus(); Search_Click(null, null); }
                if (datefrom.Focused)
                { dateto.Focus();  }
                else if (dateto.Focused)
                { txtsend.Focus(); txtsend.SelectAll(); }
                else if (txtsend.Focused)
                { txtreceiver.Focus(); txtreceiver.SelectAll(); }
                else if (txtreceiver.Focused)
                { cbtab.Focus(); cbtab.SelectAll(); }
                else if (cbtab.Focused)
                { txtrefno.Focus(); txtrefno.SelectAll(); }
                else if (txtrefno.Focused)
                { txtAmount.Focus(); txtAmount.SelectAll(); }
                else if (txtAmount.Focused)
                {
                    cbCurrency.Focus(); cbCurrency.SelectAll();
                }
                else if (cbCurrency.Focused)
                {
                    cbdepartment.Focus(); cbdepartment.SelectAll();
                }
                else if (cbdepartment.Focused)
                {
                    cbHV_LV.Focus(); cbHV_LV.SelectAll();
                }
                else if (cbHV_LV.Focused)
                {
                    cbMsgDirection.Focus(); cbMsgDirection.SelectAll();
                }
                else if (cbMsgDirection.Focused)
                {
                    cbStatus.Focus(); cbStatus.SelectAll();
                }
                else if (cbStatus.Focused)
                {
                    cboFWStatus.Focus(); cboFWStatus.SelectAll();
                }
                else if (cboFWStatus.Focused)
                {
                    txtSource_branch.Focus(); txtSource_branch.SelectAll();
                }
                else if (txtSource_branch.Focused)
                {
                    cmdSearch.Focus(); Search_Click(null, null);
                }               
                //strSucess = true;
            }
        }
        #endregion

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                string strAmount = txtAmount.Text.Trim();
                if (strAmount != "")
                {
                    if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                    {
                        txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Check_Select_Rows()
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[b].Cells[0].Value.ToString() == "True")
                        {
                            iSelect = 1; break;
                        }
                    }
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        private void dataMessage_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < dataMessage.Rows.Count; i++)
                    {
                        dataMessage.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }       

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < dataMessage.Rows.Count; i++)
                    {
                        dataMessage.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                    }
                }
                BR.BRLib.FomatGrid.Color_datagrid(dataMessage);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                
                cmdview.Enabled = true;               
                cmdPrint.Enabled = true;
                cmdResend.Enabled = true;
                if (e.RowIndex != -1)
                { iRows = e.RowIndex; }
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dataMessage.RowCount; i++)
                        {
                            this.dataMessage.EndEdit();
                            string re_value = this.dataMessage.Rows[i].Cells[0].EditedFormattedValue.ToString();
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void View_data(int Rows)
        {
            try
            {
                frmIBPSResendMsgInfo frmIbps = new frmIBPSResendMsgInfo();
                frmIbps.strMSG_ID = dataMessage.Rows[Rows].Cells["MSG_ID"].Value.ToString();
                frmIbps.strQUERY_ID = dataMessage.Rows[Rows].Cells["QUERY_ID"].Value.ToString();
                Common.strQuery_id = dataMessage.Rows[Rows].Cells["QUERY_ID"].Value.ToString();
                frmIbps.strSender = dataMessage.Rows[Rows].Cells["SENDER"].Value.ToString();
                frmIbps.strReceiving = dataMessage.Rows[Rows].Cells["RECEIVER"].Value.ToString();
                frmIbps.strAmount = dataMessage.Rows[Rows].Cells["AMOUNT"].Value.ToString();
                frmIbps.strRmno = dataMessage.Rows[Rows].Cells["RM_NUMBER"].Value.ToString();
                frmIbps.strTransdate = dataMessage.Rows[Rows].Cells["TRANS_DATE"].Value.ToString();
                frmIbps.strDepartment = dataMessage.Rows[Rows].Cells["DEPARTMENT"].Value.ToString();
                frmIbps.strStatus = dataMessage.Rows[Rows].Cells["STATUS"].Value.ToString();
                frmIbps.strRM_NUMBER = dataMessage.Rows[Rows].Cells["RM_NUMBER"].Value.ToString();
                frmIbps.strCurrency = dataMessage.Rows[Rows].Cells["CCYCD"].Value.ToString();
                frmIbps.strGW_TRANS_NUM = dataMessage.Rows[Rows].Cells["GW_TRANS_NUM"].Value.ToString();
                frmIbps.strTRANS_CODE = dataMessage.Rows[Rows].Cells["TRANS_CODE"].Value.ToString();
                frmIbps.strteller = dataMessage.Rows[Rows].Cells["TELLERID"].Value.ToString();
                frmIbps.strPreTranCode = dataMessage.Rows[Rows].Cells["PRETRAN_CODE"].Value.ToString();
                frmIbps.strPreTransNum = dataMessage.Rows[Rows].Cells["PRETRANS_NUM"].Value.ToString();
                frmIbps.strForwardStatus = dataMessage.Rows[Rows].Cells["FWSTS"].Value.ToString();
                frmIbps.strForwardTime = dataMessage.Rows[Rows].Cells["FWTIME"].Value.ToString();
                frmIbps.strMsgDirection = dataMessage.Rows[Rows].Cells["MSG_DIRECTION"].Value.ToString();
                frmIbps.strSendingTime = dataMessage.Rows[Rows].Cells["SENDING_TIME"].Value.ToString();
                frmIbps.strReceivingTime = dataMessage.Rows[Rows].Cells["RECEIVING_TIME"].Value.ToString();
                frmIbps.dtSTATUS = _dsAll_code.Tables["STATUS"];
                frmIbps.dtFWSTS = _dsMaster.Tables["FWSTS"];//PRINT_STS
                frmIbps.strPrin_STS = dataMessage.Rows[Rows].Cells["PRINT_STS"].Value.ToString();
                frmIbps.dtPRINTSTS = _dsMaster.Tables["PRINT_STS"];
                frmIbps.ShowDialog();
                dataMessage.Rows[Rows].Cells[0].Value = null;
                if (frmIbps.bIsCloseForm == true)
                { bIsCloseForm = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bIsCloseForm = false;
                if (e.RowIndex != -1)
                { iRows = e.RowIndex; View_data(iRows); }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdview.Enabled = true;               
                cmdPrint.Enabled = true;
                cmdResend.Enabled = true;
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIBPSResend_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dataMessage_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void datDieukien_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
       
        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                Common.Export_excel = 0;
                System.Data.DataTable datEx = new System.Data.DataTable();
                DataRow[] datRow;
                DataRow datRow1;
                for (int i = 0; i < _dtViews.Columns.Count; i++)
                {
                    DataColumn datColum = new DataColumn(_dtViews.Columns[i].ColumnName, _dtViews.Columns[i].DataType);
                    datEx.Columns.Add(datColum);
                }
                //-------------------------------------------------------------------------------------------------
                Check_Select_Rows();
                if (iSelect == 0)
                {
                    MessageBox.Show("There is no message select", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (iSelect == 1)
                {
                    int f = 0;
                    while (f < dataMessage.Rows.Count)
                    {
                        if (dataMessage.Rows[f].Cells[0].Value != null && dataMessage.Rows[f].Cells[0].Value.ToString() == "True")// hang khong duoc chon
                        {
                            string strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString().Trim();
                            //int k = 0;
                            datRow = _dtViews.Select("MSG_ID=" + Convert.ToInt32(strMSG_ID));
                            datRow1 = datEx.NewRow();
                            for (int j = 0; j < _dtViews.Columns.Count; j++)
                            {
                                datRow1[j] = datRow[0][j];
                            }
                            datEx.Rows.Add(datRow1);
                        }
                        f = f + 1;
                    }
                    //--------------------------------------------------------------------------------------
                    BR.BRSYSTEM.PleaseWait Please = new PleaseWait();
                    Export_excel.dtTable = datEx;
                    ThreadStart method = new ThreadStart(Export_excel.Export);
                    Thread thrd = new Thread(method);
                    thrd.Start();
                    if (thrd.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        thrd.Abort();
                    }
                    Please.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        public void ResetParam()
        {
            datetimefrom = "";
            datetimeto = "";
            Direction = "";
        }
        public void GetParam()
        {
            datetimefrom = datefrom.Text.Trim();
            datetimeto = dateto.Text.Trim();
            Direction = cbMsgDirection.Text.Trim();
        }

        private void dataMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int h = 0;
                //int m = 0;
                if (dataMessage.Rows.Count > 0)
                {
                    if (e.KeyData == Keys.Space)
                    {
                        foreach (DataGridViewRow selectedCell in dataMessage.SelectedRows)
                        {
                            h = selectedCell.Cells[0].RowIndex;
                            if (dataMessage.Rows[h].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dataMessage.Rows[h].Cells[0].Value.ToString() == "True")
                                {
                                    dataMessage.Rows[h].Cells[0].Value = null;
                                }
                                else
                                {
                                    dataMessage.Rows[h].Cells[0].Value = "True";
                                }
                            }
                            else
                            {
                                dataMessage.Rows[h].Cells[0].Value = "True";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdResend_Click(object sender, EventArgs e)
        {
            try
            {
                string Msg = "Do you want to resend message ?";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    Resend_messages();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Resend_messages()
        {
            try
            {
                int iResenderror = 0;
                string pqueryResenderro = "";
                int iCountResend = 0;
                Check_Select_Rows();
                if (iSelect == 0)
                {
                    MessageBox.Show("No message select!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                { 
                     int d = 0;
                     while (d < dataMessage.Rows.Count)
                     {                         
                         if (dataMessage.Rows[d].Cells[0].Value != null)
                         {
                             if (dataMessage.Rows[d].Cells[0].Value.ToString() == "True")
                             {
                                 string pQUERY_ID = dataMessage.Rows[d].Cells["QUERY_ID"].Value.ToString();
                                 string pMSG_DIRECTION = dataMessage.Rows[d].Cells["MSG_DIRECTION"].Value.ToString();
                                 if (objcontrolcontent.Resend_message_ibps(pQUERY_ID, pMSG_DIRECTION) == 1)
                                 {
                                     //MessageBox.Show("Resend message successfull!", Common.sCaption);
                                     #region Ghi log khi resend message
                                     DateTime dtDateLogin = DateTime.Now;
                                     string strContent = "Resend Message";
                                     int iLoglevel = 1;
                                     string strWorked = " Resend";
                                     string strTable = "IBPS_MSG_CONTENT";
                                     string strOld_value = pQUERY_ID + pMSG_DIRECTION;
                                     string strNew_value = "";
                                     //goi ham ghilog
                                     objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent,
                                         iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                                     #endregion
                                     iCountResend = iCountResend + 1;                                    
                                     objLogInfo.LOG_DATE = DateTime.Now;
                                     objLogInfo.QUERY_ID = Convert.ToInt32(pQUERY_ID);
                                     objLogInfo.STATUS = 1;
                                     objLogInfo.DESCRIPTIONS = Common.Userid + " Resend message ";
                                     objctLog.ADD_MAG_LOG(objLogInfo);
                                 }
                                 else
                                 {
                                     //MessageBox.Show("Resend message error!", Common.sCaption);
                                     iResenderror = iResenderror + 1;
                                     if (pqueryResenderro == "")
                                     {
                                         pqueryResenderro = pQUERY_ID;
                                     }
                                     else
                                     {
                                         pqueryResenderro = pqueryResenderro + "," + pQUERY_ID;
                                     }
                                     objLogInfo.LOG_DATE = DateTime.Now;
                                     objLogInfo.QUERY_ID = Convert.ToInt32(pQUERY_ID);
                                     objLogInfo.STATUS = 1;
                                     objLogInfo.DESCRIPTIONS = Common.Userid + " Resend message ";
                                     objctLog.ADD_MAG_LOG(objLogInfo);
                                 } 
                                
                             }
                         }
                         d = d + 1;
                     }                     
                     if (iResenderror == 0)//Obi loi
                     {
                         MessageBox.Show("Resend messages successfull!", Common.sCaption);
                     }
                     else
                     {
                         MessageBox.Show(iCountResend + ":Resend messages successfull!" + "\r\n" + iResenderror + "Resend message error! ", Common.sCaption);
                     }
                     Format();
                     Searchdata();
                     Enable_controls();
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cbMsgDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbMsgDirection.Text == "SIBS-IBPS")
                {
                    datefrom.Enabled = true;
                    dateto.Enabled = true;
                }
                else
                {
                    datefrom.Enabled = false;
                    dateto.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }
}
