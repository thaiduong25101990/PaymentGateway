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

namespace BR.BRTTSB
{
    public partial class frmTTSPMsgList : BR.BRSYSTEM.frmBasedata
    {
        #region dinh nghia cac datatable && dataset
        private DataTable _dtSearch;
        private DataSet _dsAll_code;
        private DataSet _dsMaster;
        private DataTable _dtViews;
        //System.Data.DataTable datExcel;
        #endregion
        
        #region dinh nghia cac ham trong lop BusinessObject
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();
        Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        USERSInfo objUser = new USERSInfo();
        USERSController objctrlUser = new USERSController();
        TADInfo objTad = new TADInfo();
        TADController objcontrolTad = new TADController();
        TTSP_MSG_CONTENTInfo objcontent = new TTSP_MSG_CONTENTInfo();
        TTSP_MSG_CONTENTController objcontrolcontent = new TTSP_MSG_CONTENTController();
        SEARCHInfo objSearch = new SEARCHInfo();
        SEARCHController objsearchcontrol = new SEARCHController();
        CURRENCYInfo objCurent = new CURRENCYInfo();
        CURRENCYController objCtrlcurrent = new CURRENCYController();
        MRECEIVE_BRANCHInfo objMreciver = new MRECEIVE_BRANCHInfo();
        MRECEIVE_BRANCHController objCtrlMre = new MRECEIVE_BRANCHController();
        TTSP_MSG_LOGInfo objLog = new TTSP_MSG_LOGInfo();
        TTSP_MSG_LOGController ObjCtrlLog = new TTSP_MSG_LOGController();
        clsCheckInput clsCheck = new clsCheckInput();
        frmIQSList frmIQSlist = new frmIQSList();
        #endregion

        #region dinh nghia  cac bien
        private static int iRow;
        //private static int iVisible = 0;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        private int iSelect;
        private bool bIsCloseForm = false;
        string HO;
        public static int selectedRow;
        public string strMsg_Type = "";
        public string strMsg_type;
        //private int iKieu;
        public DataTable dsPrint;
        private string strUserName;
        public string datetimefrom;
        public string datetimeto;
        public string Direction="";
        //private string status;
        private int iSelect_rows;
        private int strResend_Succ;
        private int strResend_Erro;        
        public static int iSearch;
        #endregion
        
        public frmTTSPMsgList()
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


        private void Locate_controls()
        {
            try
            {
                cmdAdd.Visible = false; dateTimePicker1.Visible = false; cmdEdit.Visible = false;
                cmdSave.Visible = false; cmdDelete.Visible = false; cbCheck.Visible = false;
                dataMessage.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataMessage.Columns[0].HeaderCell = dgvColumnHeader;
                dataMessage.Columns[0].Width = 40;
                this.datefrom.MaxDate = DateTime.Now;
                this.datefrom.MaxDate = dateto.Value;
                this.dateto.MaxDate = DateTime.Now;
                this.cmdAdvance.Location = new Point(cmdNornal.Location.X, cmdNornal.Location.Y);
                iSearch = 0; cmdview.Enabled = false; cmdIQS.Enabled = false;
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                this.grbSearch.Location = new Point(grbSearchnhanh.Location.X, grbSearchnhanh.Location.Y);
                this.grbSearch.Size = new Size(grbSearchnhanh.Size.Width + grbDieukien.Size.Width + 2, grbSearchnhanh.Size.Height);
                if (Common.strTTSP_Resend == "Resend")//goi form xu ly dien thu cong cua thanh toan song phuong
                {
                    this.Text = "TTSP resend messages management";
                    cmdPrint.Visible = false;
                    //dinh vi cac control---------------------------------------------------------------
                    label10.Location = new Point(label9.Location.X, label9.Location.Y);
                    label13.Location = new Point(label8.Location.X, label8.Location.Y);
                    label8.Location = new Point(label11.Location.X, label11.Location.Y);
                    txtRMno.Location = new Point(cbMsgDirection.Location.X, cbMsgDirection.Location.Y);
                    txtMsg_type.Location = new Point(cbdepartment.Location.X, cbdepartment.Location.Y);
                    cbdepartment.Location = new Point(cbStatus.Location.X, cbStatus.Location.Y);
                    label9.Visible = false;
                    label11.Visible = false;
                    datefrom.ShowCheckBox = true;
                    dateto.ShowCheckBox = true;
                    datefrom.Checked = false;
                    dateto.Checked = false;
                    cbMsgDirection.Visible = false;
                    cbStatus.Visible = false;
                    cmdIQS.Visible = false;
                    //----------------------------------------------------------------------------------
                    this.cmdRelease.Location = new Point(cmdExport.Location.X, cmdExport.Location.Y);
                    this.cmdExport.Location = new Point(cmdPrint.Location.X, cmdPrint.Location.Y);                    
                }
                else if (Common.strTTSP_Resend == "Views")//goi man hinh quan ly dien cua thanh toan song phuong
                {
                    this.Text = "TTSP Messages management";                    
                }
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
        private void frmTTSPMsgList_Load(object sender, EventArgs e)
        {
            try
            {               
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                UserName();//lay thong tin cua nguoi dang nhap
                Locate_controls();//dinh vi controls
                Get_status();//ham lay toan bo cac trang thai cua form  
                Load_data();//ham lay dien len luoi
                Enable_controls();
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
                { cmdExport.Enabled = false; cmdview.Enabled = false; cmdRelease.Enabled = false; cmdPrint.Enabled = false; }
                else
                { cmdExport.Enabled = true; cmdview.Enabled = true; cmdRelease.Enabled = true; cmdPrint.Enabled = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        //ham lay cac trang thai cua dien
        private void Get_status()
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

        //lay dien len luoi
        private void Load_data()
        {
            try
            {
                if (Common.strTTSP_Resend == "Resend")//goi form xu ly dien thu cong cua thanh toan song phuong
                {
                    GetdatagridAdvance_Resend();
                }
                else if (Common.strTTSP_Resend == "Views")//goi man hinh quan ly dien cua thanh toan song phuong
                {
                    GetdatagridAdvance();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void GetdatagridAdvance_Resend()
        {
            try
            {
                dataMessage = clsDatagridviews.TTSP_MSG_CONTENT_RESEND(dataMessage);
                _dtViews = (System.Data.DataTable)dataMessage.DataSource;//day ra mot datatable de in dien-------------                          
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }         
        }
       
       
        //-	Mặc định khi mở form sẽ hiển thị thông tin toàn bộ các điện của TTSP đã được xử lý xong trong ngày tại bảng IBPS_MSG_CONTENT.
        private void GetdatagridAdvance()
        {
            try
            {
                dataMessage = clsDatagridviews.TTSP_MSG_CONTENT_LOAD(dataMessage);
                _dtViews = (System.Data.DataTable)dataMessage.DataSource;//day ra mot datatable de in dien-------------                          
                dsPrint = _dtViews;
                GetParam(); 
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

        /*---------------------------------------------------------------
        * Muc dich         : Ad sang Datagridview
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 11/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdadd_Click(object sender, EventArgs e)
        {
            try
            {
                txtValue.Text = txtValue.Text.ToUpper();
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
                Condition_search();
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


        //Remove cac hang duoc chon
        private void cmdremove_Click(object sender, EventArgs e)
        {
            try
            {
                int m = 0;
                while (m < datDieukien.Rows.Count)
                {
                    if (datDieukien.Rows[m].Cells[0].Value.ToString() == "False")// hang duoc chon
                    { datDieukien.Rows.RemoveAt(m); }
                    else
                    { m = m + 1; }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //xoa toan bo du lieu tren luoi
        private void cmdremoveall_Click(object sender, EventArgs e)
        {
            try
            { datDieukien.Rows.Clear(); }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //tim kiem nang cao
        private void cmdAdvance_Click(object sender, EventArgs e)
        {
            //dataMessage.ColumnHeadersVisible = false;
            cbColumns.Focus();
            iSearch = 1;
            try
            {               
                cmdAdvance.Hide();
                grbSearch.Hide();
                grbSearchnhanh.Show();
                grbDieukien.Show();
                // goi ham lay du lieu ra cac combobox--------------               
                _dtSearch = objsearchcontrol.COLUMNS_SEARCH("TTSP", out _dtSearch);
                cbCheck.DataSource = _dtSearch;
                cbCheck.ValueMember = "OPERATOR";
                cbCheck.DisplayMember = "FIELDNAME";
                cbColumns.DataSource = _dtSearch;
                cbColumns.ValueMember = "FIELDCODE";
                cbColumns.DisplayMember = "FIELDNAME";  
                ClearSimple();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        // quay lai form tim kiem binh thuong
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
                this.grbSearch.Location = new Point(10, grbSearch.Location.X);
                this.grbSearch.Location = new Point(13, grbSearch.Location.Y);
                grbSearch.Show();
                ClearSimple();
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

      

        /*---------------------------------------------------------------
       * Muc dich         : Click vao combobox lay du lieu len com bo box khac
       * Tra ve           : Mot danh sach cac File - List<FileInfo>
       * Ngay tao         : 26/54/2008
       * Nguoi tao        : Quynd
       * Ngay cap nhat    : 11/06/2008
       * Nguoi cap nhat   : Quynd
       *--------------------------------------------------------------*/
        private void cbbAmount_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                cbCheck.Text = cbColumns.Text;
                if (cbColumns.SelectedValue.ToString() == "TRANSDATE" || cbColumns.SelectedValue.ToString() == "VALUE_DATE" || cbColumns.SelectedValue.ToString() == "SENDING_TIME" || cbColumns.SelectedValue.ToString() == "RECEIVING_TIME")
                {
                    //iVisible = 1;
                    txtValue.Visible = false; cboStatus.Visible = false; dateValue.Visible = true;
                    this.dateValue.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "STATUS")
                {
                    //iVisible = 2;
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["STATUS"];
                    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "STATUS";

                    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                {
                    //iVisible = 2; 
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["CURRENCY"];
                    cboStatus.DisplayMember = "CCYCD";
                    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                {
                    //iVisible = 2; 
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["DEPARTMENT"];
                    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")
                {
                    //iVisible = 2; 
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["MSGDIRECTION"];
                    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "ERR_CODE")
                {
                    //iVisible = 2; 
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["ERROR_CODE"];
                    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "ERROR_CODE";
                    this.cboStatus.Location = new System.Drawing.Point(txtValue.Location.X, txtValue.Location.Y);
                }               
                else
                {
                    //iVisible = 0;
                    txtValue.Visible = true;
                    cboStatus.Visible = false;
                    dateValue.Visible = false;
                }
                cbOperator.Items.Clear();
                if (cbCheck.SelectedValue != null)
                {
                    string strOPERATOR = cbCheck.SelectedValue.ToString();
                    String[] M = strOPERATOR.Split(new String[] { "," }, StringSplitOptions.None);//cat chuoi
                    int k = M.Count<String>();
                    int j = 0;
                    while (j < k)
                    {
                        cbOperator.Items.Add(M[j]);
                        j = j + 1;
                    }
                    cbOperator.SelectedIndex = 0;
                }
                //-----------------------------------------------------------------   
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
                frmTTSPMsgInfo frmTTSP = new frmTTSPMsgInfo();
                frmTTSP.strMSG_ID = dataMessage.Rows[Rows].Cells["MSG_ID"].Value.ToString();
                frmTTSP.strQUERY_ID = dataMessage.Rows[Rows].Cells["QUERY_ID"].Value.ToString();//de co dieu kien tim lichj su dien
                frmTTSP.strSender = dataMessage.Rows[Rows].Cells["SENDER"].Value.ToString();
                frmTTSP.strReceiving = dataMessage.Rows[Rows].Cells["RECEIVER"].Value.ToString();
                frmTTSP.strAmount = dataMessage.Rows[Rows].Cells["AMOUNT"].Value.ToString();
                frmTTSP.strRmno = dataMessage.Rows[Rows].Cells["FIELD20"].Value.ToString();
                frmTTSP.strTransdate = dataMessage.Rows[Rows].Cells["TRANS_DATE"].Value.ToString();
                frmTTSP.strDepartment = dataMessage.Rows[Rows].Cells["DEPARTMENT"].Value.ToString();
                frmTTSP.strCCYCD = dataMessage.Rows[Rows].Cells["CCYCD"].Value.ToString();
                frmTTSP.strRMNUMBER = dataMessage.Rows[Rows].Cells["RM_NUMBER"].Value.ToString();
                frmTTSP.strMsgDirection = dataMessage.Rows[Rows].Cells["MSG_DIRECTION"].Value.ToString();
                frmTTSP.strMessageTYPE = dataMessage.Rows[Rows].Cells["MSG_TYPE"].Value.ToString();
                this.Hide();
                frmTTSP.ShowDialog();
                dataMessage.Rows[Rows].Cells[0].Value = null;
                this.Show();
                if (frmTTSP.bIsCloseForm == true)
                { bIsCloseForm = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //hien thi form xem thong tin dien valich su dien
        private void cmdview_Click(object sender, EventArgs e)
        {
            try
            {
                Check_Select_Rows();
                if (iSelect == 0)
                {
                    bIsCloseForm = false;
                    if (iRow != -1)
                    { View_data(iRow); }
                }
                else
                {
                    int d = 0;
                    while (d < dataMessage.Rows.Count)
                    {
                        bIsCloseForm = false;
                        if (dataMessage.Rows[d].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataMessage.Rows[d].Cells[0].Value.ToString() == "True")
                            { View_data(d); }
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
        //tim kiem du lieu theo dieu kien dua vao
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
                Format();
                if (Common.strTTSP_Resend == "Resend")//goi form xu ly dien thu cong cua thanh toan song phuong
                {  Searchdata_resend(); }
                else if (Common.strTTSP_Resend == "Views")//goi man hinh quan ly dien cua thanh toan song phuong
                {  Searchdata();
                    //lay du lieu tu datagrid view ra datatable-----------de in dien---------------------------------------------
                if (dataMessage.Rows.Count != 0) { _dtViews = (DataTable)dataMessage.DataSource; dsPrint = _dtViews; }                    
                     
                    //--bat dau gan cac otext co dinh de in-------------------------------------------------------
                }
                
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Searchdata_resend()
        {
            try
            {
                string Where = "";
                if (iSearch == 1)//tim kiem nang cao
                {
                    //goi ham truyen vao la datagridview gia tri tra ve la mot menh de wherer
                    Where = clsDatagridviews.Search_Advance(datDieukien, out datetimefrom, out datetimeto, out Direction);
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    if (Where.Trim() != "")
                    {
                        dataMessage = clsDatagridviews.TTSP_MSG_CONTENT_SEARCH_ADVANCE(Where, dataMessage);//Lay du lieu theo dieu kien where                 
                    }
                }
                else//tim kiem thong thuong
                {
                    Where = clsDatagridviews.Search_Normal(grbSearch);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    dataMessage = clsDatagridviews.TTSP_MSG_CONTENT_SEARCH_RESEND(datefrom.Value, dateto.Value, Where, dataMessage);//Lay du lieu theo dieu kien where                 
                }
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
                    //goi ham truyen vao la datagridview gia tri tra ve la mot menh de wherer
                    Where = clsDatagridviews.Search_Advance(datDieukien, out datetimefrom, out datetimeto, out Direction);
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    if (Where.Trim() != "")
                    {
                        dataMessage = clsDatagridviews.TTSP_MSG_CONTENT_SEARCH_ADVANCE(Where, dataMessage);//Lay du lieu theo dieu kien where                 
                    }
                }
                else//tim kiem thong thuong
                {
                    Where = clsDatagridviews.Search_Normal(grbSearch);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    dataMessage = clsDatagridviews.TTSP_MSG_CONTENT_SEARCH(datefrom.Value, dateto.Value, Where, dataMessage);//Lay du lieu theo dieu kien where                 
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

        private void cmdIQS_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataMessage.Rows.Count == 0)
                {
                    MessageBox.Show("There is no message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    MessageBox.Show("Invalid message type!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else
                                {
                                    if (dataMessage.Rows[f].Cells["MSG_DIRECTION"].Value.ToString() == "SIBS-TTSP" || dataMessage.Rows[f].Cells["MSG_DIRECTION"].Value.ToString() == "TTSP-SIBS")
                                    {
                                        iDem = iDem + 1;
                                        int QueryID = Convert.ToInt32(dataMessage.Rows[f].Cells["QUERY_ID"].Value);
                                        iRow = f;
                                        listSelected.Add(QueryID);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid message type!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
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
                            MessageBox.Show("Invalid message type!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                        {
                            if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "SIBS-TTSP" || dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "TTSP-SIBS")
                            {
                                frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                                //this.Hide();
                                selectIQS.ShowDialog();
                                strMsg_Type = selectIQS.strMSG_TYPE;
                                if (frmSelectTypeIQS.isOK == true)
                                {
                                    frmIQSNew1 IQSNew1 = new frmIQSNew1();
                                    IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["MSG_ID"].Value.ToString());
                                    IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString());
                                    IQSNew1.objInfoIQSNew1.REF_NUMBER = dataMessage.CurrentRow.Cells["FIELD20"].Value.ToString();
                                    DataTable ds = new DataTable();
                                    ds = objcontrolTad.GetTAD_HOST(Common.Userid);
                                    if (ds.Rows.Count == 0)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        HO = ds.Rows[0][0].ToString();
                                    }
                                    IQSNew1.objInfoIQSNew1.FROMBANK = HO; //dataMessage.CurrentRow.Cells["SENDER"].Value.ToString();
                                    if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "SIBS-TTSP")
                                    {
                                        if (dataMessage.CurrentRow.Cells["SENDER"].Value.ToString().Length == 5)
                                        {
                                            if (dataMessage.CurrentRow.Cells["SENDER"].Value.ToString().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                                            {
                                                IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["SENDER"].Value.ToString().Substring(2, 3);
                                            }
                                        }
                                        else
                                        {
                                            IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["SENDER"].Value.ToString();
                                        }
                                    }
                                    else if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "TTSP-SIBS")
                                    {
                                        if (dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString().Length == 5)
                                        {
                                            if (dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                                            {
                                                IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString().Substring(2, 3);
                                            }
                                        }
                                        else
                                        {
                                            IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString();
                                        }
                                    }
                                    IQSNew1.strAmount = dataMessage.CurrentRow.Cells["AMOUNT"].Value.ToString();
                                    IQSNew1.strCurrency = dataMessage.CurrentRow.Cells["CCYCD"].Value.ToString();
                                    IQSNew1.strMSGType = dataMessage.CurrentRow.Cells["MSG_TYPE"].Value.ToString();
                                    IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                                    IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = dataMessage.CurrentRow.Cells["RM_NUMBER"].Value.ToString();
                                    IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(dataMessage.CurrentRow.Cells["VALUE_DATE"].Value.ToString());
                                    IQSNew1.objInfoIQSNew1.MSG_TYPE = strMsg_Type;
                                    IQSNew1.strMSG_TYPE = strMsg_Type;

                                    if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                                    {
                                        MessageBox.Show("Could not create IQS message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }

                                    IQSNew1.ShowDialog();
                                }
                                else if (frmSelectTypeIQS.isOK == false)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid message type!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    else if (iDem == 1)
                    {
                        frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                        //this.Hide();
                        selectIQS.ShowDialog();
                        strMsg_Type = selectIQS.strMSG_TYPE;
                        if (frmSelectTypeIQS.isOK == true)
                        {
                            frmIQSNew1 IQSNew1 = new frmIQSNew1();
                            IQSNew1.objInfoIQSNew1.MSG_ID = Convert.ToInt32(dataMessage.Rows[iRow].Cells["MSG_ID"].Value.ToString());
                            IQSNew1.objInfoIQSNew1.QUERY_ID = Convert.ToInt32(dataMessage.Rows[iRow].Cells["QUERY_ID"].Value.ToString());
                            IQSNew1.objInfoIQSNew1.REF_NUMBER = dataMessage.Rows[iRow].Cells["FIELD20"].Value.ToString();
                            DataTable ds = new DataTable();
                            ds = objcontrolTad.GetTAD_HOST(Common.Userid);
                            if (ds.Rows.Count == 0)
                            {
                                return;
                            }
                            else
                            {
                                HO = ds.Rows[0][0].ToString();
                            }
                            IQSNew1.objInfoIQSNew1.FROMBANK = HO; //dataMessage.Rows[iRow].Cells["SENDER"].Value.ToString();
                            if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "SIBS-TTSP")
                            {
                                if (dataMessage.CurrentRow.Cells["SENDER"].Value.ToString().Length == 5)
                                {
                                    if (dataMessage.CurrentRow.Cells["SENDER"].Value.ToString().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                                    {
                                        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["SENDER"].Value.ToString().Substring(2, 3);
                                    }
                                }
                                else
                                {
                                    IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["SENDER"].Value.ToString();
                                }
                            }
                            else if (dataMessage.CurrentRow.Cells["MSG_DIRECTION"].Value.ToString() == "TTSP-SIBS")
                            {
                                if (dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString().Length == 5)
                                {
                                    if (dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString().Substring(0, 2) == "00")//kiem tra co dung chi nhanh ngan hang khong
                                    {
                                        IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString().Substring(2, 3);
                                    }
                                }
                                else
                                {
                                    IQSNew1.objInfoIQSNew1.TOBANK = dataMessage.CurrentRow.Cells["RECEIVER"].Value.ToString();
                                }
                            }
                            //IQSNew1.objInfoIQSNew1.MSGCONTENT = dataMessage.Rows[iRow].Cells["CONTENT"].Value.ToString();
                            IQSNew1.strAmount = dataMessage.Rows[iRow].Cells["AMOUNT"].Value.ToString();
                            IQSNew1.strCurrency = dataMessage.Rows[iRow].Cells["CCYCD"].Value.ToString();
                            IQSNew1.strMSGType = dataMessage.Rows[iRow].Cells["MSG_TYPE"].Value.ToString();
                            IQSNew1.objInfoIQSNew1.TELLER_ID = Common.Userid;
                            IQSNew1.objInfoIQSNew1.ORG_RM_NUMBER = dataMessage.Rows[iRow].Cells["RM_NUMBER"].Value.ToString();
                            IQSNew1.objInfoIQSNew1.ORG_TRANS_DATE = Convert.ToDateTime(dataMessage.Rows[iRow].Cells["VALUE_DATE"].Value.ToString());
                            IQSNew1.objInfoIQSNew1.MSG_TYPE = strMsg_Type;
                            IQSNew1.strMSG_TYPE = strMsg_Type;

                            if (strMsg_Type == "TS" & IQSNew1.objInfoIQSNew1.TOBANK.PadLeft(5, '0') == HO.PadLeft(5, '0'))
                            {
                                MessageBox.Show("Could not create IQS message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            //frmIQSNew2 frmIqs2 = new frmIQSNew2();
                            //frmIqs2.ShowDialog();
                            iDem = iDem + 1;
                        }
                        //iDem = iDem + 1;

                        frmSelectTypeIQS selectIQS = new frmSelectTypeIQS();
                        //this.Hide();
                        selectIQS.ShowDialog();
                        strMsg_Type = selectIQS.strMSG_TYPE;
                        if (frmSelectTypeIQS.isOK == true)
                        {
                            frmIQSNew2 frmIqs2 = new frmIQSNew2();
                            frmIqs2.listSelected = listSelected;
                            frmIqs2.strMSG_TYPE = strMsg_Type;
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

        private void dataMessage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    iRow = e.RowIndex;
                    cmdview.Enabled = true;
                    cmdPrint.Enabled = true;
                    cmdIQS.Enabled = true;
                    Common.strQuery_id = dataMessage.Rows[iRow].Cells["QUERY_ID"].Value.ToString();
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
                            //if (dataMessage.Rows[f].Cells[0].Value.ToString() == "False")
                            //{
                            string strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString().Trim();
                            //objcontrolcontent.PRINT_TTSP_LIST_CHECK(strQUERY_ID); 
                            //for (int k = 0; k < dsPrint.Tables[0].Rows.Count;k++ )
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
                string Print = "TTSP_PRINT_ALL";
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
                cmdIQS.Enabled = true;
                if (e.RowIndex != -1) { iRow = e.RowIndex; }                
             }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Date_select()
        {
            try
            {
                if (datefrom.Checked == false)
                { dateto.Checked = false; }
                else
                { dateto.Checked = true; }
                if (datefrom.Value > dateto.Value)
                { datefrom.Value = dateto.Value; } 
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
                Date_select();             
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
                Date_select();   
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
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {               
                cmdSearch_Click(null, null);
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
                    if (dataMessage.Rows[b].Cells[0].Value != null)
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

        private void frmTTSPMsgList_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);
                
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void dataMessage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                { View_data(iRow); }
               
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

        private void frmTTSPMsgList_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void Select_rows()
        {
            try
            {
                iSelect_rows = 0;
                int k = 0;
                while (k < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[k].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[k].Cells[0].Value.ToString() == "True")
                        {
                            iSelect_rows = 1;
                        }
                    }
                    k = k + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void cmdRelease_Click(object sender, EventArgs e)
        {
            try
            {
                Select_rows();
                strResend_Succ = 0;
                strResend_Erro = 0;
                //cho click chon thi moi duoc Release
                if (iSelect_rows == 1)
                {
                    int n = 0;
                    while (n < dataMessage.Rows.Count)
                    {
                        if (dataMessage.Rows[n].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataMessage.Rows[n].Cells[0].Value.ToString() == "True")// hang duoc chon
                            {
                                //goi ham lay du lieu de update trong bang MRECIVER_BRANH
                                string strTran_no = dataMessage.Rows[n].Cells["TRANS_NO"].Value.ToString();
                                if (objCtrlMre.UPDATE_MRECIVER(strTran_no.Trim()) == 1)
                                {
                                    objLog.QUERY_ID = Convert.ToInt32(dataMessage.Rows[n].Cells["QUERY_ID"].Value.ToString());
                                    objLog.STATUS = Convert.ToInt32(dataMessage.Rows[n].Cells["QUERY_ID"].Value.ToString());
                                    objLog.DESCRIPTIONS = "User ID" + Common.Userid + "Resend message ,Ref no:" + dataMessage.Rows[n].Cells["FIELD20"].Value.ToString();
                                    ObjCtrlLog.ADD_TTSP_MSG_LOG(objLog);                                    
                                    dataMessage.Rows.RemoveAt(n);
                                    if (strResend_Succ == 0)
                                    { strResend_Succ = 1; }
                                    else
                                    { strResend_Succ = strResend_Succ + 1; }
                                }
                                else
                                {                                    
                                    n = n + 1;
                                    if (strResend_Erro == 0)
                                    {
                                        strResend_Erro = 1;
                                    }
                                    else
                                    {
                                        strResend_Erro = strResend_Erro + 1;
                                    }                                    
                                }
                            }
                            else
                            {
                                n = n + 1;
                            }
                        }
                        else
                        {
                            n = n + 1;
                        }
                    }
                    if (strResend_Erro == 0)
                    {
                        MessageBox.Show(strResend_Succ + " :Resend message sucessfull!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (strResend_Erro != 0)
                    {
                        MessageBox.Show(strResend_Succ + " :Resend message sucessfull!" + "\r\n" + strResend_Erro + " :Resend message Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }                    
                }
                else if (iSelect_rows == 0)
                {
                    MessageBox.Show("There is no message select", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
           // Lbtong.Text = Convert.ToString(dataMessage.Rows.Count);
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

    }
}
