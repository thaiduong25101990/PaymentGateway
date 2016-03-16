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
using Microsoft.Office.Interop.Excel;
using Office_12 = Microsoft.Office.Core;
using Excel_12 = Microsoft.Office.Interop.Excel;
using System.Threading;



namespace BR.BRInterBank
{
    public partial class frmVCBMsgList : frmBasedata
    {
        #region dinh nghia cac datatable && dataset
        private System.Data.DataTable _dtSearch;
        private System.Data.DataSet _dsAll_code;
        private System.Data.DataSet _dsMaster;
        private System.Data.DataTable _dtViews;//data table de in dien---------------------------
        public System.Data.DataTable dsPrint;
        private static System.Data.DataTable _dtSearchAV;
        #endregion

        #region dinh nghia cac ham trong lop BusinessObject
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();
        Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        TADInfo objTad = new TADInfo();
        TADController objcontrolTad = new TADController();
        VCB_MSG_CONTENTInfo objcontent = new VCB_MSG_CONTENTInfo();
        VCB_MSG_CONTENTController objcontrolcontent = new VCB_MSG_CONTENTController();
        SEARCHInfo objSearch = new SEARCHInfo();
        SEARCHController objsearchcontrol = new SEARCHController();
        CURRENCYInfo objCurent = new CURRENCYInfo();
        CURRENCYController objCtrlcurrent = new CURRENCYController();
        USERSInfo objUser = new USERSInfo();
        USERSController objctrlUser = new USERSController();
        clsCheckInput clsCheck = new clsCheckInput();
        #endregion

        #region dinh nghia  cac bien
        private static int iRow;
        frmIQSList frmIQSlist = new frmIQSList();        
        string HO;        
        public int selectedRow;
        public int iRows;
        private bool bIsCloseForm = false;        
        public string strMsg_Type = "";
        private string strTXTX;        
        private int iSelect;        
        private string strUserName;
        public string datetimefrom;
        public string datetimeto;
        public string Direction;
        public string msg_source;        
        private const int _TABLES = 10;     
        private const int _COLUMNS = 20;          
        private static int iSearch;
        #endregion

        public frmVCBMsgList()
        {
            InitializeComponent();
        }

        private void UserName()
        {
            try
            {
                string strUserID = Common.Userid;
                System.Data.DataTable datUU = new System.Data.DataTable();
                datUU = objctrlUser.GetUSERS_PASS1(strUserID.Trim());
                strUserName = datUU.Rows[0]["USERNAME"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Enable_controls_one()
        {
            try
            {
                //enable va dinh vi cac controls------------------------------------------------
                dataMessage.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataMessage.Columns[0].HeaderCell = dgvColumnHeader;
                dataMessage.Columns[0].Width = 40;
                cmdAdd.Visible = false; cmdEdit.Visible = false;
                cmdSave.Visible = false; cmdDelete.Visible = false; cbCheck.Enabled = false;
                this.Text = "VCB message management"; this.datefrom.MaxDate = DateTime.Now;
                this.datefrom.MaxDate = dateto.Value; this.dateto.MaxDate = DateTime.Now;
                this.cmdAdvance.Location = new System.Drawing.Point(cmdNornal.Location.X, cmdNornal.Location.Y);
                iSearch = 0;
                cmdview.Enabled = false;
                cmdIQS.Enabled = false;
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                this.grbSearch.Location = new System.Drawing.Point(grbSearchnhanh.Location.X, grbSearchnhanh.Location.Y);//grbDieukien
                this.grbSearch.Size = new Size(grbSearchnhanh.Size.Width + grbDieukien.Size.Width + 2, grbSearchnhanh.Size.Height);
                //-------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //khi bat dau load form
        private void frmVCBMsgList_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new System.Drawing.Icon(System.Windows.Forms.Application.StartupPath + @"\BRIDGE.ico");   
                UserName();//goi ham lay ten cua nguoi log vao
                Enable_controls_one();//an va dinh vi cac controls
                Load_data();//ham lay du lieu len luoi
                Vcb_status();//ham lay du lieu tat ca cac trang thai cua vcb    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Enable_controls()
        {
            try
            {
                label12.Text = "Total number of messages :" + Convert.ToString(dataMessage.Rows.Count);
                if (dataMessage.Rows.Count == 0)
                { cmdExport.Enabled = false; cmdview.Enabled = false; cmdIQS.Enabled = false; cmdPrint.Enabled = false; }
                else
                { cmdExport.Enabled = true; cmdview.Enabled = true; cmdIQS.Enabled = true; cmdPrint.Enabled = true; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //ham lay du lieu len luoi cua vcb
        private void Load_data()
        {
            try
            {
                dataMessage = clsDatagridviews.VCB_MSG_CONTENT_LOAD(dataMessage);
                _dtViews = (System.Data.DataTable)dataMessage.DataSource;//day ra mot datatable de in dien-------------                          
                dsPrint = _dtViews;
                GetParam(); 
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        } 

        // ham lay tat ca cac trang thai cua vcb
        private void Vcb_status()
        {
            try
            {
                _dsAll_code = clsStatus.GET_ALL_STATUS();
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
        * Ngay cap nhat    : 11/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void Getdatacombo()
        {
            try
            {
                // Combobox STATUS----------------------------                
                DataRow _dtr = _dsAll_code.Tables["STATUS"].NewRow();
                _dtr[0] = 99; _dtr[1] = "ALL";
                _dsAll_code.Tables["STATUS"].Rows.InsertAt(_dtr, 0);
                cbStatus.DataSource = _dsAll_code.Tables["STATUS"];
                cbStatus.DisplayMember = "NAME";
                cbStatus.ValueMember = "STATUS";
                cbStatus.Text = "ALL";
                // Combobox DEPARTMENT----------------------------                
                DataRow _dtr1 = _dsAll_code.Tables["DEPARTMENT"].NewRow();
                _dtr1[0] = ""; _dtr1[1] = "ALL";
                _dsAll_code.Tables["DEPARTMENT"].Rows.InsertAt(_dtr1, 0);
                cbdepartment.DataSource = _dsAll_code.Tables["DEPARTMENT"];
                cbdepartment.DisplayMember = "CONTENT";
                cbdepartment.ValueMember = "CDVAL";
                cbdepartment.Text = "ALL";
                // Combobox MSGDirection----------------------------                
                DataRow _dtr2 = _dsAll_code.Tables["MSGDIRECTION"].NewRow();
                _dtr2[0] = ""; _dtr2[1] = "ALL";
                _dsAll_code.Tables["MSGDIRECTION"].Rows.InsertAt(_dtr2, 0);
                cbMsgDirection.DataSource = _dsAll_code.Tables["MSGDIRECTION"];
                cbMsgDirection.DisplayMember = "CONTENT";
                cbMsgDirection.ValueMember = "CDVAL";
                cbMsgDirection.Text = "ALL";
                // Combobox MSG_SRC----------------------------                
                DataRow _dtr3 = _dsAll_code.Tables["MSG_SRC"].NewRow();
                _dtr3[0] = ""; _dtr3[1] = "ALL";
                _dsAll_code.Tables["MSG_SRC"].Rows.InsertAt(_dtr3, 0);
                cboResource.DataSource = _dsAll_code.Tables["MSG_SRC"];
                cboResource.DisplayMember = "CONTENT";
                cboResource.ValueMember = "CDVAL";
                cboResource.Text = "ALL";                
                // Combobox Currency----------------------------                
                DataRow _dtr5 = _dsAll_code.Tables["CURRENCY"].NewRow();
                _dtr5[0] = "ALL";
                _dsAll_code.Tables["CURRENCY"].Rows.InsertAt(_dtr5, 0);
                cbCurrency.DataSource = _dsAll_code.Tables["CURRENCY"];
                cbCurrency.DisplayMember = "CCYCD";
                cbCurrency.Text = "ALL";              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //remove cac hang duoc chon trong luoi
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
        //xoa toan bo luoi
        private void cmdremoveall_Click(object sender, EventArgs e)
        {
            try
            {
                datDieukien.Rows.Clear();
            }
            catch
            {
            }
        }
        //nhan nut de quay lai tim kiem binh thuong
        private void cmdNornal_Click(object sender, EventArgs e)
        {
            datefrom.Focus();
            iSearch = 0;
            try
            {                
                cmdAdvance.Show();
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                //this.buteXit.location = new point(300, butexit.lcoation.y); 
                this.grbSearch.Location = new System.Drawing.Point(10, grbSearch.Location.X);
                this.grbSearch.Location = new System.Drawing.Point(13, grbSearch.Location.Y);
                grbSearch.Show();
                ClearSimple();
                datefrom.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        // tim kiem nang cao
        private void cmdAdvance_Click(object sender, EventArgs e)
        {
            try
            {
                cbColumns.Focus();iSearch = 1;
                cmdAdvance.Hide(); grbSearch.Hide();grbSearchnhanh.Show();
                grbDieukien.Show();
                #region Quynd comment 20100319
                //_dtSearch = objsearchcontrol.COLUMNS_SEARCH("VCB", out _dtSearch);
                //cbCheck.DataSource = _dtSearch;
                //cbCheck.ValueMember = "OPERATOR";
                //cbCheck.DisplayMember = "FIELDNAME";
                //cbColumns.DataSource = _dtSearch;
                //cbColumns.ValueMember = "FIELDCODE";
                //cbColumns.DisplayMember = "FIELDNAME";
                #endregion

                #region Quyndcap nhat 20100319
                _dtSearchAV = objsearchcontrol.dtSearch("VCB");
                if (_dtSearchAV != null)
                {
                    cbColumns.DataSource = _dtSearchAV;
                    cbColumns.ValueMember = "FIELDCODE";
                    cbColumns.DisplayMember = "FIELDNAME";
                }
                #endregion

                ClearSimple();
                cbColumns.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void ClearSimple()
        {
            try
            {
                cmdview.Enabled = false;
                cmdIQS.Enabled = false;              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
               
       
        // Bat su kien click chuot vao hang trong DataGrid
        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void cmdview_Click(object sender, EventArgs e)
        {
            try
            {
                Check_Select_Rows();
                if (iSelect == 0)
                {
                    bIsCloseForm = false;
                    View_data(iRows);
                }
                else
                {
                    bIsCloseForm = false;
                    int d = 0;
                    while (d < dataMessage.Rows.Count)
                    {
                        if (dataMessage.Rows[d].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataMessage.Rows[d].Cells[0].Value.ToString() == "True")
                            {                                
                                View_data(d);
                            }
                        }
                        dataMessage.Rows[d].Cells[0].Value = null;
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
                        { d = d + 1; }
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
        * Ngay cap nhat    : 11/06/2008
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
                            if ((cbColumns.SelectedValue.ToString().Trim().ToUpper() == "BRANCH_A" ||
                                cbColumns.SelectedValue.ToString().Trim().ToUpper() == "BRANCH_B") && (txtValue.Text.Trim().Length < 5))
                            {
                                pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " Upper(Lpad('" + txtValue.Text.Replace(",", "") + "',5,'0')) ";
                            }
                            else if (cbColumns.SelectedValue.ToString().Trim().ToUpper() == "MSG_TYPE")
                            {
                                pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                pSearch = " Replace(" + cbColumns.SelectedValue.ToString() + ",'MT','') " + cbOperator.Text + " Replace('" + txtValue.Text.Replace(",", "") + "','MT','') ";
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
                    pSearch = cbColumns.SelectedValue.ToString() + " " + cbOperator.Text + " '" + cboStatus.SelectedValue.ToString() + "' ";
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


        private void Format_advance()
        {
            try
            {
                if (cbColumns.SelectedValue != null)
                {
                    if (cbColumns.SelectedValue.ToString() == "AMOUNT")
                    {
                        if (txtValue.Text.Trim() != "")
                        {
                            if (Regex.IsMatch(txtValue.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                txtValue.Text = Common.FormatCurrency(txtValue.Text.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                            }
                        }
                    }
                    else if (cbColumns.SelectedValue.ToString() == "MSG_TYPE")
                    {
                        if (txtValue.Text.Trim() != "")
                        {
                            if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                if (txtValue.Text.Trim().Length == 3)
                                {
                                    txtValue.Text = "MT" + txtValue.Text.Trim();
                                }
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


        /*---------------------------------------------------------------
         * Muc dich         : Ham tim kiem du lieu
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 12/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Format_advance();
                Format();
                Searchdata();
                Enable_controls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    { dataMessage = clsDatagridviews.VCB_MSG_CONTENT_SEARCH_ADVANCE(Where, dataMessage); }//Lay du lieu theo dieu kien where                 
                    if (dataMessage.Rows.Count != 0) { _dtViews = (System.Data.DataTable)dataMessage.DataSource; }
                }
                else//tim kiem thong thuong
                {
                    Where = clsDatagridviews.Search_Normal(grbSearch);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    dataMessage = clsDatagridviews.VCB_MSG_CONTENT_SEARCH(datefrom.Value, dateto.Value, Where, dataMessage);//Lay du lieu theo dieu kien where                 
                    if (dataMessage.Rows.Count != 0) { _dtViews = (System.Data.DataTable)dataMessage.DataSource; dsPrint = _dtViews; }                 
                    
                    ResetParam();
                    GetParam();
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
        //su kien thoat khoi form
 
        //HaNTT10
        private void cmdIQS_Click(object sender, EventArgs e)
        {
            #region
            //try
            //{
            //    int iDem = 0;
            //    int f = 0;
            //    List<int> listSelected = new List<int>();
            //    listSelected.Clear();
            //    while (f < dataMessage.Rows.Count)
            //    {
            //        if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
            //        {
            //            if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
            //            {
            //            iDem = iDem + 1;
            //            int QueryID = Convert.ToInt32(dataMessage.Rows[f].Cells[2].Value);
            //            listSelected.Add(QueryID);
            //            }
            //        }
            //        f = f + 1;
            //    }
            //    if (iDem == 1)
            //    {
            //        frmIQSNew1 IQSNew1 = new frmIQSNew1();
            //        IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString());
            //        IQSNew1.objInfoIQSNew1.F20 = dataMessage.CurrentRow.Cells[22].Value.ToString();
            //        IQSNew1.objInfoIQSNew1.FROMBANK = dataMessage.CurrentRow.Cells["SENDER"].Value.ToString();
            //        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString();
            //        IQSNew1.objInfoIQSNew1.RMACCOUNT = dataMessage.CurrentRow.Cells["RM_NUMBER"].Value.ToString();
            //        IQSNew1.objInfoIQSNew1.DATECREATE = Convert.ToDateTime(dataMessage.CurrentRow.Cells["TRANS_DATE"].Value.ToString());
            //        IQSNew1.objInfoIQSNew1.MSGCONTENT = dataMessage.CurrentRow.Cells["CONTENT"].Value.ToString();

            //        IQSNew1.ShowDialog();
            //    }
            //    if (iDem > 1)
            //    {
            //        while (iDem < dataMessage.Rows.Count)
            //        {
            //            frmIQSlist.objInfoIQSList.QUERY_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString());
            //            //frmIQSNew2 frmIqs2 = new frmIQSNew2();
            //            //frmIqs2.ShowDialog();
            //            iDem = iDem + 1;
            //        }
            //        //iDem = iDem + 1;

            //        frmIQSNew2 frmIqs2 = new frmIQSNew2();
            //        frmIqs2.listSelected = listSelected;
            //        frmIqs2.ShowDialog();
            //    }
            //}
            //catch
            //{
            //}
            #endregion
            try
            {
                if (dataMessage.Rows.Count == 0)
                {
                    Common.ShowError("There is no message!", 3, MessageBoxButtons.OK);                      
                    return;
                }
                else
                {
                    int iDem = 0;
                    int f = 0;
                    List<int> listSelected = new List<int>();
                    listSelected.Clear();
                    while (f < dataMessage.Rows.Count)
                    {
                        if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                        {                            
                            if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                            {                                
                                 if (dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString().Trim() == "MT195" || dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString().Trim() == "MT196")
                                {
                                    Common.ShowError("Invalid message type!", 3, MessageBoxButtons.OK);                                    
                                    return;
                                }
                                else
                                {
                                    iDem = iDem + 1;
                                    int QueryID = Convert.ToInt32(dataMessage.Rows[f].Cells["QUERY_ID"].Value);
                                    iRow = f;
                                    listSelected.Add(QueryID);
                                }
                               
                            }
                        }
                        dataMessage.Rows[f].Cells[0].Value = null;
                        f = f + 1;
                    }
                    if (iDem == 0)
                    {                        
                        if (dataMessage.CurrentRow.Cells["MSG_TYPE"].Value.ToString().Trim() == "MT195" || dataMessage.CurrentRow.Cells["MSG_TYPE"].Value.ToString().Trim() == "MT196")
                        {
                            Common.ShowError("Invalid message type!", 3, MessageBoxButtons.OK);                            
                            return;
                        }
                        else
                        {
                            frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                            selectIQS.ShowDialog();
                            strMsg_Type = selectIQS.strMSG_TYPE;
                            if (frmSelectTypeIQS.isOK == true)
                            {
                                frmIQSNew1 IQSNew1 = new frmIQSNew1();
                                IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["MSG_ID"].Value.ToString());
                                IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString());
                                IQSNew1.objInfoIQSNew1.REF_NUMBER = dataMessage.CurrentRow.Cells["FIELD20"].Value.ToString();
                                                              
                                System.Data.DataTable ds = new System.Data.DataTable();
                                ds = objcontrolTad.GetTAD_HOST(Common.Userid);
                                if (ds.Rows.Count == 0)
                                {
                                    return;
                                }
                                else
                                {
                                    HO = ds.Rows[0][0].ToString();
                                }
                                IQSNew1.objInfoIQSNew1.FROMBANK = HO; 

                                if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "VCB-SIBS")
                                {
                                    if (dataMessage.CurrentRow.Cells["BRANCH_B"].Value.ToString().Length == 5)
                                    {
                                        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_B"].Value.ToString().Substring(2, 3);
                                    }
                                    else
                                    {
                                        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_B"].Value.ToString();
                                    }
                                    if (strMsg_Type == "TS")
                                    {
                                        Common.ShowError("Could not create IQS message!", 2, MessageBoxButtons.OK);                                       
                                        return;
                                    }
                                }
                                else
                                {
                                    if (dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString().Length == 5)
                                    {
                                        if (dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                                        {
                                            IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString().Substring(2, 3);
                                        }
                                    }
                                    else
                                    {
                                        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString();
                                    }
                                }                                
                                IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(dataMessage.CurrentRow.Cells["VALUE_DATE"].Value.ToString());
                                IQSNew1.strAmount = dataMessage.CurrentRow.Cells["AMOUNT"].Value.ToString();
                                IQSNew1.strCurrency = dataMessage.CurrentRow.Cells["CCYCD"].Value.ToString();
                                IQSNew1.strMSGType = dataMessage.CurrentRow.Cells["MSG_TYPE"].Value.ToString();
                                IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                                IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = dataMessage.CurrentRow.Cells["RM_NUMBER"].Value.ToString();
                                //IQSNew1.objInfoIQSNew1.MSGCONTENT = dataMessage.CurrentRow.Cells["CONTENT"].Value.ToString(); //HaNTT10
                                IQSNew1.objInfoIQSNew1.MSG_TYPE = strMsg_Type;
                                IQSNew1.strMSG_TYPE = strMsg_Type;

                                if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                                {
                                    Common.ShowError("Could not create IQS message!", 2, MessageBoxButtons.OK);                                    
                                    return;
                                }                               
                                IQSNew1.ShowDialog();
                            }
                            else if (frmSelectTypeIQS.isOK == false)
                            {
                                return;
                            }
                        }
                    }
                    if (iDem == 1)
                    {
                        frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                        selectIQS.ShowDialog();
                        strMsg_Type = selectIQS.strMSG_TYPE;
                        if (frmSelectTypeIQS.isOK == true)
                        {
                            frmIQSNew1 IQSNew1 = new frmIQSNew1();
                            IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["MSG_ID"].Value.ToString());
                            IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString());
                            IQSNew1.objInfoIQSNew1.REF_NUMBER = dataMessage.Rows[iRow].Cells["FIELD20"].Value.ToString();
                            System.Data.DataTable ds = new System.Data.DataTable();
                            ds = objcontrolTad.GetTAD_HOST(Common.Userid);
                            if (ds.Rows.Count == 0)
                            {
                                return;
                            }
                            else
                            {
                                HO = ds.Rows[0][0].ToString();
                            }
                            IQSNew1.objInfoIQSNew1.FROMBANK = HO;                            
                            if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "VCB-SIBS")
                            {
                                if (dataMessage.CurrentRow.Cells["BRANCH_B"].Value.ToString().Length == 5)
                                {
                                    IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_B"].Value.ToString().Substring(2, 3);
                                }
                                else
                                {
                                    IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_B"].Value.ToString();
                                }
                                if (strMsg_Type == "TS")// khong cho tao dien tra soat theo chieu den "VCB-SIBS"
                                {
                                    Common.ShowError("Could not create IQS message!", 3, MessageBoxButtons.OK);                                    
                                    return;
                                }
                            }
                            else
                            {
                                if (dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString().Length == 5)
                                {
                                    if (dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                                    {
                                        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString().Substring(2, 3);
                                    }
                                }
                                else
                                {
                                    IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["BRANCH_A"].Value.ToString();
                                }
                            }
                            IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(dataMessage.Rows[iRow].Cells["VALUE_DATE"].Value.ToString());
                            IQSNew1.strAmount = dataMessage.Rows[iRow].Cells["AMOUNT"].Value.ToString();
                            IQSNew1.strCurrency = dataMessage.Rows[iRow].Cells["CCYCD"].Value.ToString();
                            IQSNew1.strMSGType = dataMessage.Rows[iRow].Cells["MSG_TYPE"].Value.ToString();
                            IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                            IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = dataMessage.Rows[iRow].Cells["RM_NUMBER"].Value.ToString();                            
                            IQSNew1.objInfoIQSNew1.MSG_TYPE = strMsg_Type;
                            IQSNew1.strMSG_TYPE = strMsg_Type;

                            if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                            {
                                Common.ShowError("Could not create IQS message!", 3, MessageBoxButtons.OK);                                
                                return;
                            }                            
                            IQSNew1.ShowDialog();
                        }
                        else if (frmSelectTypeIQS.isOK == false)
                        {
                            return;
                        }
                    }
                    if (iDem > 1)
                    {
                        while (iDem < dataMessage.Rows.Count)
                        {
                            frmIQSlist.objInfoIQSList.MSG_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["MSG_ID"].Value.ToString());
                            frmIQSlist.objInfoIQSList.QUERY_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString());
                            iDem = iDem + 1;
                        }                       
                        frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                        selectIQS.ShowDialog();
                        strMsg_Type = selectIQS.strMSG_TYPE;
                        if (frmSelectTypeIQS.isOK == true)
                        {
                            frmIQSNew2 frmIqs2 = new frmIQSNew2();
                            frmIqs2.listSelected = listSelected;
                            frmIqs2.strMsg_Type = strMsg_Type;
                            frmIqs2.ShowDialog();
                        }
                        else if (frmSelectTypeIQS.isOK == false)
                        {
                            return;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

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
                            System.Data.DataTable _dt = new System.Data.DataTable();
                            _dt = objsearchcontrol.Excute_Select(pContent);
                            txtValue.Visible = false; cboStatus.Visible = true;
                            cboStatus.DataSource = _dt;
                            cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                            this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                        }
                        else if (pControl == "dateTimePicker".ToUpper())/*Neu la dateTimePicker*/
                        {
                            txtValue.Visible = false; cboStatus.Visible = false; dateValue.Visible = true;
                            this.dateValue.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
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

                #region Quynd comment
                //cbCheck.Text = cbColumns.Text;
                //if (cbColumns.SelectedValue.ToString() == "TRANSDATE" || cbColumns.SelectedValue.ToString() == "VALUE_DATE" || cbColumns.SelectedValue.ToString() == "SENDING_TIME" || cbColumns.SelectedValue.ToString() == "RECEIVING_TIME")
                //{
                //    //iVisible = 1;
                //    txtValue.Visible = false; cboStatus.Visible = false;  dateValue.Visible = true;
                //    this.dateValue.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "STATUS")
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["STATUS"];
                //    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "STATUS";
                    
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "PRINT_STS")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["PRINT_STS"];
                //    cboStatus.DisplayMember = "CONTENT";
                //    cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["CURRENCY"];
                //    cboStatus.DisplayMember = "CCYCD";
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["DEPARTMENT"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSGDIRECTION"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "ERR_CODE")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["ERROR_CODE"];
                //    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "ERROR_CODE";
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_SRC")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSG_SRC"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else
                //{
                //    //iVisible = 0;
                //    txtValue.Visible = true;
                //    cboStatus.Visible = false;
                //    dateValue.Visible = false;
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
                ////-----------------------------------------------------------------   
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dataMessage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdview.Enabled = true;
                cmdPrint.Enabled = true;
                cmdIQS.Enabled = true;
                if (e.RowIndex != -1)
                {
                    iRows = e.RowIndex;
                    Common.strQuery_id = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
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
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            
           
            System.Data.DataTable datPrint = new System.Data.DataTable();
            DataRow[] datRow;
            DataRow datRow1;

            for (int i = 0; i < dsPrint.Columns.Count; i++)
            {
                DataColumn datColum = new DataColumn(dsPrint.Columns[i].ColumnName,dsPrint.Columns[i].DataType);
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
                string Print = "VCB_PRINT_ALL";
                frmPrint.PrintType = Print;
                frmPrint.dtfrom = datetimefrom;
                frmPrint.dtto = datetimeto;
                frmPrint.Direction = Direction;
                frmPrint.msg_source = msg_source; 
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
                cmdIQS.Enabled = true;
                if (e.RowIndex != -1) { iRows = e.RowIndex; }                
                Common.strQuery_id = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();               
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
                {
                    dateto.Checked = false;
                }
                else
                {
                    dateto.Checked = true;
                }
                if (datefrom.Value > dateto.Value)
                {
                    datefrom.Value = dateto.Value;
                }               
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
                {
                    datefrom.Checked = false;
                }
                else
                {
                    datefrom.Checked = true;
                }
                if (dateto.Value < datefrom.Value)
                {
                    this.dateto.Value = datefrom.Value;
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //kiem tra co phai la dang so khong(chi cho nhap so co do dai day so la 3)
        private void txtMsg_type_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtMsg_type.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                {//strTXTX
                    if (txtMsg_type.Text.Trim().Length <= 3)
                    {
                        strTXTX = txtMsg_type.Text.Trim();
                    }
                    else
                    {
                        txtMsg_type.Text = strTXTX;
                    }
                }
                else
                {
                    txtMsg_type.Text = "";
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        #region  //enterKey(...): Bắt phím Enter để đăng nhập
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                //frmCCYCD_FormClosing(null,null);
                this.Close();
            }
            //khi nhan phim Enter
            else if (e.KeyChar == (char)13)
            {
                //if (cmdAdvance.Enabled == true)
                //{
                //    cbbAmount.Focus();
                //    cbbAmount.SelectAll();
                    if (cbColumns.Focused)
                    {
                        cbOperator.Focus();
                        cbOperator.SelectAll();
                    }
                    if (cbOperator.Focused)
                    {
                        if (txtValue.Enabled == true)
                        {
                            txtValue.Focus();
                            txtValue.SelectAll();
                        }
                        else if (dateValue.Enabled == true)
                        {
                            dateValue.Focus();
                            dateValue.Select();
                        }
                        else if (cboStatus.Enabled == true)
                        {
                            cboStatus.Focus();
                            cboStatus.SelectAll();
                        }
                    }
                    else if (txtValue.Focused || dateValue.Focused || cboStatus.Focused)
                    {
                        cmdAdd1.Focus();
                        cmdAdd1_Click(null, null);
                    }
                    else if (cmdAdd1.Focused)
                    {
                        cmdremove.Focus();
                        cmdremove_Click(null, null);
                    }
                    else if (cmdremove.Focused)
                    {
                        cmdremoveall.Focus();
                        cmdremoveall_Click(null, null);
                    }
                    else if (cmdremoveall.Focused)
                    {
                        datDieukien.Focus();
                    }
                    else if (datDieukien.Focused)
                    {
                        cmdSearch.Focus();
                        cmdSearch_Click(null, null);
                    }
                //}
                //else if (cmdNornal.Enabled == true)
                //{
                //    datefrom.Focus();
                //    datefrom.Select();
                    if (datefrom.Focused)
                    {
                        dateto.Focus();
                    }
                    else if (dateto.Focused)
                    {
                        txtsend.Focus();
                        txtsend.SelectAll();
                    }
                    else if (txtsend.Focused)
                    {
                        txtreceiver.Focus();
                        txtreceiver.SelectAll();
                    }
                    else if (txtreceiver.Focused)
                    {
                        txtrefno.Focus();
                        txtrefno.SelectAll();
                    }
                    else if (txtrefno.Focused)
                    {
                        txtAmount.Focus();
                        txtAmount.SelectAll();
                    }
                    else if (txtAmount.Focused)
                    {
                        cbCurrency.Focus();
                        cbCurrency.SelectAll();
                    }
                    else if (cbCurrency.Focused)
                    {
                        txtMsg_type.Focus();
                        txtMsg_type.SelectAll();
                    }
                    else if (txtMsg_type.Focused)
                    {
                        cbMsgDirection.Focus();
                        cbMsgDirection.SelectAll();
                    }
                    else if (cbMsgDirection.Focused)
                    {
                        cbStatus.Focus();
                        cbStatus.SelectAll();
                    }
                    else if (cbStatus.Focused)
                    {
                        cbdepartment.Focus();
                        cbdepartment.SelectAll();
                    }
                    else if (cbdepartment.Focused)
                    {
                        cmdSearch.Focus();
                        cmdSearch_Click(null, null);
                    }
                //}
                //strSucess = true;
            }
        }
        #endregion

        private void frmVCBMsgList_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            string strAmount = txtAmount.Text.Trim();
            if (strAmount != "")            
            {
                if (Regex.IsMatch(txtAmount.Text.Replace(",","").Replace(".",""), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                }                            
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

        private void frmVCBMsgList_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);
                
                if ((this.ActiveControl) is System.Windows.Forms.TextBox)
                {
                    (this.ActiveControl as System.Windows.Forms.TextBox).SelectAll();
                }
            }
        }

        private void dataMessage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bIsCloseForm = false;
                if (e.RowIndex != -1) { View_data(iRows); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void View_data(int Rows)
        {
            try
            {
                frmVCBMsgInfo frmVCB = new frmVCBMsgInfo();
                frmVCB.strQUERY_ID = dataMessage.Rows[Rows].Cells["QUERY_ID"].Value.ToString();
                Common.strQuery_id = dataMessage.Rows[Rows].Cells["QUERY_ID"].Value.ToString();
                frmVCB.strMSG_ID = dataMessage.Rows[Rows].Cells["MSG_ID"].Value.ToString();
                frmVCB.strSender = dataMessage.Rows[Rows].Cells["BRANCH_A"].Value.ToString();
                frmVCB.strReceiving = dataMessage.Rows[Rows].Cells["BRANCH_B"].Value.ToString();
                frmVCB.strAmount = dataMessage.Rows[Rows].Cells["AMOUNT"].Value.ToString();
                frmVCB.strTransdate = dataMessage.Rows[Rows].Cells["TRANS_DATE"].Value.ToString();
                frmVCB.strDepartment = dataMessage.Rows[Rows].Cells["DEPARTMENT"].Value.ToString();
                frmVCB.strFIELD20 = dataMessage.Rows[Rows].Cells["FIELD20"].Value.ToString();
                frmVCB.strCCYCD = dataMessage.Rows[Rows].Cells["CCYCD"].Value.ToString();
                frmVCB.strRefNo = dataMessage.Rows[Rows].Cells[22].Value.ToString();
                frmVCB.strMSG_DIRECTION = dataMessage.Rows[Rows].Cells["MSG_DIRECTION"].Value.ToString();
                frmVCB.strRM_NUMBER = dataMessage.Rows[Rows].Cells["RM_NUMBER"].Value.ToString();
                frmVCB.strMsgDirection = dataMessage.Rows[Rows].Cells["MSG_DIRECTION"].Value.ToString();
                frmVCB.strMessageTYPE = dataMessage.Rows[Rows].Cells["MSG_TYPE"].Value.ToString();
                this.Hide();
                frmVCB.ShowDialog();
                this.Show();
                if (frmVCB.bIsCloseForm == true)
                { bIsCloseForm = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dataMessage_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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

        private void frmVCBMsgList_MouseMove(object sender, MouseEventArgs e)
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
                    Common.ShowError("There is no message select!", 3, MessageBoxButtons.OK);                    
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
            msg_source = "";
        }
        public void GetParam()
        {
            datetimefrom = datefrom.Text.Trim();
            datetimeto = dateto.Text.Trim();
            Direction = cbMsgDirection.Text.Trim();
            msg_source = cboResource.Text.Trim();  
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
    }
}
