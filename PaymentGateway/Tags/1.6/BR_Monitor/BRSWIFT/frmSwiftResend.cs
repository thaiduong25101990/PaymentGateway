using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRSYSTEM;
using BR.BRLib;
using System.Text.RegularExpressions;
using System.Threading;

namespace BR.BRSWIFT
{
    public partial class frmSwiftResend : frmBasedata
    {
        private clsLog objLog = new clsLog();
        #region khia bao cac datatable
        public DataTable _dtColumns;
        public DataTable _dtGridview;
        private DataTable _dtSearch;
        private DataSet _dsAll_code;
        private DataSet _dsMaster;
        private DataSet _dts;
        private DataTable _dtViews;//data table de in dien---------------------------
        private DataTable datExcel = new DataTable();
        private DataTable datExcel1 = new DataTable();
        private string _MSG_DIRECTION_EDIT = "";
        private string _CONTENT = "";
        private string _QUERY_ID = "";
        private string _Table;
        #endregion

        #region khai bao cac lop trong bussiness
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();
        TADInfo objTad = new TADInfo();
        TADController objcontrolTad = new TADController();
        Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        SWIFT_MSG_CONTENTInfo objcontent = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objcontrolcontent = new SWIFT_MSG_CONTENTController();
        SEARCHInfo objSearch = new SEARCHInfo();
        SEARCHController objsearchcontrol = new SEARCHController();
        CURRENCYInfo objCurent = new CURRENCYInfo();
        CURRENCYController objCtrlcurrent = new CURRENCYController();
        clsCheckInput clsCheck = new clsCheckInput();
        #endregion

        #region khai bao cac bien
        String[] arrButton = { "", "", "", "", "" };
        private string strTXTX;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        //private int iKieu;
        private bool controlKey = false;
        //private string status;
        //datHM add
        public DataTable dsPrint;
        public string datetimefrom;
        public string datetimeto;
        public string phanhe;
        public string nhnhan;
        public string sotien;
        public string loaidien;
        public string isn;
        public string chieudien;
        public string nhgui;
        public string sogd;
        public string loaitien;
        public string osn;
        public string tcdien;
        public string ttdienden;
        public string ttdiendi;
        public string ttgw;
        public string kieuxuly;
        private int iCount;
        private int iCount1;
        private string strBRANCH_B;
        private int iSelect;
        private string pRow_select;        
        //-------------------------------------------------------------       
        public static int selectedRow;
        public static int iSearch;
        //private static int iVisible = 0;
        public int iRows;
        public int iTime;
        #endregion

        public frmSwiftResend()
        {
            InitializeComponent();
        }

        private void Enable_controls()
        {
            try
            {
                label12.Text = "Total number of messages :" + Convert.ToString(dataMessage.Rows.Count); 
                if (dataMessage.Rows.Count == 0)
                { cmdExport.Enabled = false; cmdview.Enabled = false; cmdPrint.Enabled = false; }
                else if (dataMessage.Rows.Count != 0)
                { cmdExport.Enabled = true; cmdview.Enabled = true; cmdPrint.Enabled = true; }
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
                this.Text = "Swift message management";
                dataMessage.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataMessage.Columns[0].HeaderCell = dgvColumnHeader;
                dataMessage.Columns[0].Width = 28;
                this.datefrom.MaxDate = DateTime.Now;
                this.datefrom.MaxDate = dateto.Value;
                this.dateto.MaxDate = DateTime.Now;
                this.cmdAdvance.Location = new Point(cmdNornal.Location.X, cmdNornal.Location.Y);
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                this.grbSearch.Location = new Point(grbSearchnhanh.Location.X, grbSearchnhanh.Location.Y);
                this.grbSearch.Size = new Size(grbSearchnhanh.Size.Width + grbDieukien.Size.Width + 2, grbSearchnhanh.Size.Height + 30);                           
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Load_data()
        {
            try
            {
                Search_data();
                //dataMessage = clsDatagridviews.MESSAGE_CONTENT_DATE(datefrom.Value, dateto.Value, "", dataMessage);//mac dinh lay dien trong bang content ngay hien tai lam viec                               
                _dtViews = (DataTable)dataMessage.DataSource;
                dsPrint = _dtViews;
                GetParam(); 
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
         * Muc dich         : Form khi load len goi ham lay du lieu
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 11/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 11/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void frmSwiftResend_Load(object sender, EventArgs e)
        {
            try
            {
                /*Form nay chi resend nhung dien co trang thai 
                 * STATUS = SENT and PROCESSSTS in (NACK,ACKWAIT)
                 * La dien Export ra file roi ma chuong trinh swift 
                 * chua tra ve gia tri thi la ACKWAIT,
                 * Con tra ve gia tri la NACK moi duoc resend o form nay
                 */
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                Locate_controls();//dinh vi cac control
                Load_data();// ham lay du lieu                     
                Enable_controls(); //an cac control            
                GET_ALL_CODE();  //lay toan bo cac trang thai cua SWIFT              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void GET_ALL_CODE()
        {
            try
            {                
                _dsAll_code = objallcodecontrol.GET_ALL_ALL_CODE("SWMSTS", "PROCESSSTS", "DEPARTMENT", "MSG_SRC", "MSGDIRECTION", "SWIFT", "", "");
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
                // Combobox SWMSTS-----------------------                    
                DataRow _dtr = _dsAll_code.Tables["SWMSTS"].NewRow();
                _dtr[0] = ""; _dtr[1] = "ALL";
                _dsAll_code.Tables["SWMSTS"].Rows.InsertAt(_dtr, 0);
                cbMsg_status.DataSource = _dsAll_code.Tables["SWMSTS"];                
                cbMsg_status.DisplayMember = "CONTENT";
                cbMsg_status.ValueMember = "CDVAL";                
                cbMsg_status.Text = "ALL";
               
                // Combobox PROCESSSTS-------------------------                            
                DataRow _dtr1 = _dsAll_code.Tables["PROCESSSTS"].NewRow();
                _dtr1[0] = ""; _dtr1[1] = "ALL";
                _dsAll_code.Tables["PROCESSSTS"].Rows.InsertAt(_dtr1, 0);
                //----------------------------------------------
                DataRow _dtr2 = _dsAll_code.Tables["PROCESSSTS"].NewRow();
                _dtr2[0] = ""; _dtr2[1] = " ";
                _dsAll_code.Tables["PROCESSSTS"].Rows.InsertAt(_dtr2, 1);
                //---------------------------------------------
                cbProcess_Status.DataSource = _dsAll_code.Tables["PROCESSSTS"];
                cbProcess_Status.DisplayMember = "CONTENT";
                cbProcess_Status.ValueMember = "CDVAL";
                cbProcess_Status.Text = "ALL";                

                // Combobox STATUS----------------------------                
                DataRow _dtr3 = _dsAll_code.Tables["STATUS"].NewRow();
                _dtr3[0] = 99; _dtr3[1] = "ALL";
                _dsAll_code.Tables["STATUS"].Rows.InsertAt(_dtr3, 0);
                cbStatus.DataSource = _dsAll_code.Tables["STATUS"];
                cbStatus.DisplayMember = "NAME";
                cbStatus.ValueMember = "STATUS";
                cbStatus.SelectedValue = Common.STATUS_SENT;
                //cbStatus.Text = "ALL";

                //cbdepartment--------------------------------
                DataRow _dtr4 = _dsAll_code.Tables["DEPARTMENT"].NewRow();
                _dtr4[0] = ""; _dtr4[1] = "ALL";
                _dsAll_code.Tables["DEPARTMENT"].Rows.InsertAt(_dtr4, 0);
                cbdepartment.DataSource = _dsAll_code.Tables["DEPARTMENT"];
                cbdepartment.DisplayMember = "CONTENT";
                cbdepartment.ValueMember = "CDVAL";
                cbdepartment.Text = "ALL";

                //cbCurrency-------------------------------------
                DataRow _dtr5 = _dsAll_code.Tables["CCYCD"].NewRow();
                _dtr5[0] = "ALL";
                _dsAll_code.Tables["CCYCD"].Rows.InsertAt(_dtr5, 0);
                cbCurrency.DataSource = _dsAll_code.Tables["CCYCD"];
                cbCurrency.DisplayMember = "CCYCD";
                cbCurrency.Text = "ALL";

                //--cboResource----------------------------------
                DataRow _dtr6 = _dsAll_code.Tables["MSG_SRC"].NewRow();
                _dtr6[0] = ""; _dtr6[1] = "ALL";
                _dsAll_code.Tables["MSG_SRC"].Rows.InsertAt(_dtr6, 0);
                cboResource.DataSource = _dsAll_code.Tables["MSG_SRC"];
                cboResource.DisplayMember = "CONTENT";
                cboResource.ValueMember = "CDVAL";
                cboResource.Text = "ALL";               
                //------cbmsg_direction---------------------------
                DataRow _dtr7 = _dsAll_code.Tables["MSG_DIRECTION"].NewRow();
                _dtr7[0] = ""; _dtr7[1] = "ALL";
                _dsAll_code.Tables["MSG_DIRECTION"].Rows.InsertAt(_dtr7, 0);
                cbmsg_direction.DataSource = _dsAll_code.Tables["MSG_DIRECTION"];
                cbmsg_direction.DisplayMember = "CONTENT";
                cbmsg_direction.ValueMember = "CDVAL";
                cbmsg_direction.Text = "ALL";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //tim kiem nang cao
        private void cmdAdvance_Click(object sender, EventArgs e)
        {
            iSearch = 1;
            try
            {                
                this.dateValue.MaxDate = DateTime.Now;
                //dinh vi cac control-------------------------------
                this.label12.Top = label12.Top - 30;
                this.dataMessage.Top = dataMessage.Top - 30;
                dataMessage.Height = dataMessage.Size.Height + 30;               
                cmdAdvance.Hide();
                grbSearch.Hide();
                grbSearchnhanh.Show();
                grbDieukien.Show();
                // goi ham lay du lieu ra cac combobox--------------               
                _dtSearch = objsearchcontrol.COLUMNS_SEARCH("SWIFT", out _dtSearch);
                cbCheck.DataSource = _dtSearch;
                cbCheck.DisplayMember = "FIELDNAME";
                cbCheck.ValueMember = "OPERATOR";
                cbColumns.DataSource = _dtSearch;
                cbColumns.ValueMember = "FIELDCODE";
                cbColumns.DisplayMember = "FIELDNAME";
                //--------------------------------------------------                
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
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        
        
        // tro ve macdinh la tim kiem thong thuong
        private void cmdNornal_Click(object sender, EventArgs e)
        {
            iSearch = 0;
            try
            {
                this.dataMessage.Top = dataMessage.Top + 30;
                dataMessage.Height = dataMessage.Size.Height - 30;
                this.label12.Top = label12.Top + 30;
                cmdAdvance.Show();
                grbSearchnhanh.Hide();
                grbDieukien.Hide();                
                this.grbSearch.Location = new Point(10, grbSearch.Location.X);
                this.grbSearch.Location = new Point(13, grbSearch.Location.Y);
                grbSearch.Show();
                ClearSimple();
                datefrom.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //xoa cac hang duoc chon trong luoi
        private void cmdremove_Click(object sender, EventArgs e)
        {
            try
            {
                clsDatagridviews.Remove_Rows(this.datDieukien);                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        // xoa toan bo thong tin tren luoi
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
                if (dataMessage.Rows.Count != 0)
                {
                    if (iRows != -1)
                    {
                        if (clsDatagridviews.Check_Select(dataMessage))
                        {
                            int f = 0;
                            while (f < dataMessage.Rows.Count)
                            {
                                if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                                    {
                                        frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                                        frmSWIFT.strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                                        //DatHM add
                                        frmSWIFT.strDepartment = dataMessage.Rows[f].Cells["DEPARTMENT"].Value.ToString();
                                        frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[f].Cells["MSG_DIRECTION"].Value.ToString();
                                        frmSWIFT.strMsg_type = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString(); 
                                        //end DatHM
                                        this.Hide();
                                        frmSWIFT.ShowDialog();
                                        this.Show();
                                        dataMessage.Rows[f].Cells[0].Value = null;
                                    }
                                }
                                f = f + 1;
                            }
                        }
                        else
                        {
                            frmSwiftResendInfo frmSWIFT = new frmSwiftResendInfo();
                            frmSWIFT.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                            //DatHM add
                            frmSWIFT.strDepartment = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                            frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
                            frmSWIFT.strMsg_type = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                            //end DatHM
                            this.Hide();
                            frmSWIFT.ShowDialog();
                            this.Show();
                            dataMessage.Rows[iRows].Cells[0].Value = null;
                        }
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
       * Muc dich         : Ad sang Datagridview
       * Ngay tao         : 26/54/2008
       * Nguoi tao        : Quynd
       * Ngay cap nhat    : 11/06/2008
       * Nguoi cap nhat   : Quynd
       *--------------------------------------------------------------*/
        private void cmdAdd1_Click(object sender, EventArgs e)
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

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        //ham lay du lieu theo dieu kien nsp vao
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Format_advance();
                Search_data();
                //lay du lieu tu datagrid view ra datatable-----------de in dien---------------------------------------------
                if (dataMessage.Rows.Count != 0) { _dtViews = (DataTable)dataMessage.DataSource; dsPrint = _dtViews; }
                
                //--bat dau gan cac otext co dinh de in-------------------------------------------------------
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //ham search du lieu------------------------------------------------------------------------------------
        private void Search_data()
        {
            try
            {
                string Where = "";
                strBRANCH_B = " case  when (MSG_DIRECTION = 'SWIFT-SIBS' and PROCESSSTS = '7') then  PRE_BRANCH ";
                strBRANCH_B = strBRANCH_B + " when (MSG_DIRECTION = 'SWIFT-SIBS' and PROCESSSTS = '8') then  PRE_BRANCH ";
                strBRANCH_B = strBRANCH_B + "  when (MSG_DIRECTION = 'SWIFT-SIBS' and (PROCESSSTS <> '8' or PROCESSSTS <> '7')) then  BRANCH_B ";
                strBRANCH_B = strBRANCH_B + "  when (MSG_DIRECTION = 'SIBS-SWIFT' and (PROCESSSTS <> '8' or PROCESSSTS <> '7'  or PROCESSSTS is null)) then  BRANCH_B  end ";
                //-----------------------------------------------------------------------------
                string strAmount1 = txtAmount.Text.Trim();
                if (strAmount1 != "")
                {
                    if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                    {
                        txtAmount.Text = Common.FormatCurrency(strAmount1.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }
                }
                //-----------------------------------------------------------------------------------------------                     
                Format();               
                if (iSearch == 1)//tim kiem nang cao
                {
                    //goi ham truyen vao la datagridview gia tri tra ve la mot menh de wherer
                    Where = clsDatagridviews.Search_Advance(datDieukien,out datetimefrom,out datetimeto, out phanhe, out nhnhan,
                    out sotien,out loaidien,out isn,out chieudien, out nhgui,out sogd,out loaitien,out osn, out tcdien,out ttdienden,
                    out ttdiendi,out ttgw,out kieuxuly);
                    dataMessage.DataSource = 0;
                    if (Where.Trim() != "")
                    {
                        dataMessage = clsDatagridviews.SWIFT_MSG_CONTENT_SEARCH_ADVANCE(Where, dataMessage);//Lay du lieu theo dieu kien where                 
                        }//Lay du lieu theo dieu kien where                 
                    label12.Text = "Total number of messages : 0";                    
                }
                else//tim kiem thong thuong
                {
                    Where = clsDatagridviews.Search_Normal(grbSearch, strBRANCH_B);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    dataMessage = clsDatagridviews.Getdata_Swift(datefrom.Value, dateto.Value, Where, dataMessage);
                    label12.Text = "Total number of messages : 0";
                    ResetParam();
                    GetParam();
                }
                
                Enable_controls();
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
                if (cbColumns.SelectedValue.ToString() == "TRANSDATE" || cbColumns.SelectedValue.ToString() == "VALUE_DATE" || cbColumns.SelectedValue.ToString() == "RECEIVING_TIME" || cbColumns.SelectedValue.ToString() == "SENDING_TIME")
                {
                    //iVisible = 1;
                    txtValue.Visible = false;
                    cboStatus.Visible = false;
                    dateValue.Visible = true;
                    this.dateValue.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "STATUS")
                {
                    string sds = _dsMaster.Tables[3].Namespace;
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true; 
                    cboStatus.DataSource = _dsMaster.Tables[3];
                    cboStatus.DisplayMember = "NAME";
                    cboStatus.ValueMember = "STATUS";                                       
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                {
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables[4];
                    cboStatus.DisplayMember = "CONTENT";
                    cboStatus.ValueMember = "CDVAL";                    
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                {
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DisplayMember = "CCYCD";
                    cboStatus.DataSource = _dsMaster.Tables[5];
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "MSG_SRC")//-	Msg source
                {
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables[6];                    
                    cboStatus.DisplayMember = "CONTENT";
                    cboStatus.ValueMember = "CDVAL";                    
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);//PROCESSSTS
                }
                else if (cbColumns.SelectedValue.ToString() == "SWMSTS")//--	RM no.
                {
                    //iVisible = 2;                    
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables[0];  
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "PROCESSSTS")//--	RM no.
                {
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables[1];  
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")//--	RM no.
                {
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables[7];  
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else
                {
                    //iVisible = 0;
                    txtValue.Visible = true;
                    cboStatus.Visible = false;
                    dateValue.Visible = false;
                }
                cbOperator.Items.Clear();
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
                //----------------------------------------------------------------- 
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
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dataMessage.RowCount; i++)
                        {
                            this.dataMessage.EndEdit();
                            string re_value = this.dataMessage.Rows[i].Cells[0].EditedFormattedValue.ToString();
                            _MSG_DIRECTION_EDIT = dataMessage.Rows[i].Cells["MSG_DIRECTION"].Value.ToString();
                            _QUERY_ID = dataMessage.Rows[i].Cells["QUERY_ID"].Value.ToString();
                            if (_MSG_DIRECTION_EDIT == "SIBS-SWIFT")
                            {
                                cmdResend.Enabled = false;
                            }
                            else
                            {
                                cmdResend.Enabled = true;
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

        private void dataMessage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdview.Enabled = true;
                cmdPrint.Enabled = true;
                if (e.RowIndex != -1) { iRows = e.RowIndex; }              
                Common.strQuery_id = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                _MSG_DIRECTION_EDIT = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
                _QUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                _Table = dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString();
                if (_MSG_DIRECTION_EDIT == "SWIFT-SIBS")
                {
                    cmdResend.Enabled = false;
                }
                else
                {
                    cmdResend.Enabled = true;
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

        private void dateSearch_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtMsg_type_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtMsg_type.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                {
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

        //Nguoi tao: HaNTT10
        //Ngay: 03/08/2007
        //Muc dich: tao chuc nang auto-complete doi voi combobox cbCurrency
        private void cbCurrency_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //Nguoi tao: HaNTT10
        //Ngay: 03/08/2007
        //Muc dich: tao chuc nang auto-complete doi voi combobox cbCurrency
        private void cbCurrency_TextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);
            if (cbCurrency.Text != "" && !controlKey)
            {
                // tìm item trùng khớp
                string matchText = cbCurrency.Text;
                int match = cbCurrency.FindString(matchText);

                // nếu tìm thấy thì chèn nó vào
                if (match != -1)
                {
                    cbCurrency.SelectedIndex = match;                  
                    cbCurrency.SelectionStart = matchText.Length;
                    cbCurrency.SelectionLength = cbCurrency.Text.Length - cbCurrency.SelectionStart;
                }
            }
        } 

        private void frmSwiftResend_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void txtAmount_Leave(object sender, EventArgs e)
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

        private void dataMessage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataMessage.Rows.Count != 0)
                {
                    if (e.RowIndex != -1)
                    {
                        frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                        frmSWIFT.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                        frmSWIFT.strDepartment = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                        frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
                        frmSWIFT.strMsg_type = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString(); 
                        this.Hide();
                        frmSWIFT.ShowDialog();
                        this.Show();
                    }
                }
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
        }

        public void GetParam()
        {
            datetimefrom = datefrom.Text.ToString().Trim();
            datetimeto = dateto.Text.ToString().Trim();
            phanhe = cbdepartment.Text.ToString().Trim();
            nhnhan = txtreceiver.Text.ToString().Trim();
            sotien = txtAmount.Text.ToString().Trim();
            loaidien = txtMsg_type.Text.ToString().Trim();
            isn = "";
            chieudien = cbmsg_direction.Text.ToString().Trim();
            nhgui = txtsender.Text.ToString().Trim();
            sogd = txtrefno.Text.ToString().Trim();
            loaitien = cbCurrency.Text.ToString().Trim();
            osn = txtOSN.Text.ToString().Trim();
            tcdien = "";
            if (chieudien == "SWIFT-SIBS")
            {
                ttdienden = cbMsg_status.Text.ToString().Trim();
                ttdiendi = "";
            }
            else if (chieudien == "SIBS-SWIFT")
            {
                ttdienden = "";
                ttdiendi = cbMsg_status.Text.ToString().Trim();
            }
            else
            {
                ttdienden = cbMsg_status.Text.ToString().Trim();
                ttdiendi = cbMsg_status.Text.ToString().Trim();
            }
            ttgw = cbStatus.Text.ToString().Trim();
            kieuxuly = cbProcess_Status.Text.Trim() ;
        }
        public void ResetParam()
        {
            datetimefrom = "";
            datetimeto = "";
            phanhe = "";
            nhnhan = "";
            sotien = "";
            loaidien = "";
            isn = "";
            chieudien = "";
            nhgui = "";
            sogd = "";
            loaitien = "";
            osn = "";
            tcdien = "";
            ttdienden = "";
            ttdiendi = "";
            ttgw = "";
            kieuxuly = "";
        }

        private void frmSwiftResend_KeyDown(object sender, KeyEventArgs e)
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

        private void frmSwiftResend_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }      

        private void frmSwiftResend_MouseDown(object sender, MouseEventArgs e)
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

        private void dataMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int h = 0;                
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

        private void datDieukien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }        

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                Check_Select_Rows();
                if (iSelect == 0)
                {
                    MessageBox.Show("There is no message select", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (iSelect == 1)
                {
                    Common.Export_excel = 0;
                    //goi ham lay du lieu dua vao query_id
                    String[] M = pRow_select.Split(new String[] { "/Q" }, StringSplitOptions.None);//cat chuoi
                    int kCount = M.Count<String>();
                    int j = 0;
                    while (j < kCount)
                    {
                        datExcel = objcontrolcontent.dtExcel(M[j], out datExcel);
                        datExcel1.Merge(datExcel);
                        datExcel.Clear();
                        j = j + 1;
                    }

                    Export_excel.dtTable = datExcel1;
                    BR.BRSYSTEM.PleaseWait Please = new PleaseWait();
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

        private void Check_Select_Rows()
        {
            iCount1 = 0;
            iCount = 0;//bien dem so dien duoc chon
            iSelect = 0;
            pRow_select = "";
            try
            {
                int b = 0;
                while (b < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[b].Cells[0].Value.ToString() == "True")
                        {
                            iCount = iCount + 1;
                            iSelect = 1;
                            if (pRow_select == "")
                            {
                                pRow_select = dataMessage.Rows[b].Cells["QUERY_ID"].Value.ToString();
                            }
                            else
                            {
                                if (iCount == ((iCount1 * 200) + 200))
                                {
                                    pRow_select = pRow_select + "/Q" + dataMessage.Rows[b].Cells["QUERY_ID"].Value.ToString();
                                    iCount1 = iCount1 + 1;
                                }
                                else
                                {
                                    pRow_select = pRow_select + "," + dataMessage.Rows[b].Cells["QUERY_ID"].Value.ToString();
                                }
                            }
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


        private void cbmsg_direction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SWMSTS_PROCESSSTS();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void SWMSTS_PROCESSSTS()
        {
            try
            {                
                if (cbmsg_direction.Text.Trim() == "ALL")
                {
                    _dts = objallcodecontrol.SWMSTS_PROCESSSTS("SWMSTS", "PROCESSSTS", "");
                }
                else if (cbmsg_direction.Text.Trim() == "SWIFT-SIBS")
                {
                    _dts = objallcodecontrol.SWMSTS_PROCESSSTS("SWMSTS", "PROCESSSTS", "SWIFT-SIBS");
                }
                else if (cbmsg_direction.Text.Trim() == "SIBS-SWIFT")
                {
                    _dts = objallcodecontrol.SWMSTS_PROCESSSTS("SWMSTS", "PROCESSSTS", "SIBS-SWIFT");
                }
                //-----------------------------------------------------------------------------------
                if (_dts != null)
                {
                    DataRow _dtr = _dts.Tables[1].NewRow();
                    _dtr[0] = ""; _dtr[1] = "ALL";
                    _dts.Tables[1].Rows.InsertAt(_dtr, 0);
                    //----------------------------------------------
                    DataRow _dtr1 = _dts.Tables[1].NewRow();
                    _dtr1[0] = ""; _dtr1[1] = " ";
                    _dts.Tables[1].Rows.InsertAt(_dtr1, 1);
                    //---------------------------------------------                
                    cbProcess_Status.DataSource = _dts.Tables[1];
                    cbProcess_Status.DisplayMember = "CONTENT";
                    cbProcess_Status.ValueMember = "CDVAL";
                    cbProcess_Status.Text = "ALL";
                    //-----------------------------------------------------------------------------------
                    DataRow _dtr3 = _dts.Tables[0].NewRow();
                    _dtr3[0] = ""; _dtr3[1] = "ALL";
                    _dts.Tables[0].Rows.InsertAt(_dtr3, 0);
                    //---------------------------------------------
                    cbMsg_status.DataSource = _dts.Tables[0];
                    cbMsg_status.DisplayMember = "CONTENT";
                    cbMsg_status.ValueMember = "CDVAL";
                    cbMsg_status.Text = "ALL";
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        private void cbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboStatus.Enabled = true;
                cbCheck.Text = cbColumns.Text;
                if (cbColumns.SelectedValue.ToString() == "TRANSDATE" || cbColumns.SelectedValue.ToString() == "VALUE_DATE" || cbColumns.SelectedValue.ToString() == "RECEIVING_TIME" || cbColumns.SelectedValue.ToString() == "SENDING_TIME")
                {
                    //iVisible = 1; 
                    txtValue.Visible = false; cboStatus.Visible = false; dateValue.Visible = true;
                    this.dateValue.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "STATUS")
                {
                    //iVisible = 2; 
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["STATUS"];
                    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "STATUS";
                    cboStatus.SelectedValue = Common.STATUS_SENT;
                    cboStatus.Enabled = false;
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "MSG_SRC")//-	Msg source
                {                    
                    //iVisible = 2; 
                    txtValue.Visible = false;
                    txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["MSG_SRC"];
                    cboStatus.DisplayMember = "CONTENT";   cboStatus.ValueMember = "CDVAL";
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);//PROCESSSTS
                }
                else if (cbColumns.SelectedValue.ToString() == "SWMSTS")//--	RM no.
                {
                    //iVisible = 2;
                    txtValue.Visible = false;  cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["SWMSTS"];
                    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "PROCESSSTS")//--	RM no.
                {
                    //iVisible = 2;
                    txtValue.Visible = false;
                    cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["PROCESSSTS"];
                    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                {
                    //iVisible = 2; 
                    txtValue.Visible = false; txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["DEPARTMENT"];
                    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")//--	RM no.
                {
                    //iVisible = 2;
                    txtValue.Visible = false; txtValue.Visible = false; cboStatus.Visible = true;
                    cboStatus.DataSource = _dsMaster.Tables["MSG_DIRECTION"];
                    cboStatus.DisplayMember = "CONTENT"; 
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                }
                else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                {
                    //iVisible = 2;
                    txtValue.Visible = false;cboStatus.Visible = true;                    
                    cboStatus.DataSource = _dsMaster.Tables["CCYCD"];
                    cboStatus.DisplayMember = "CCYCD";
                    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
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
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            frmPrint frmPrint = new frmPrint();
            string Print = "SWIFT_06";
            frmPrint.PrintType = Print;
            frmPrint.dtfrom = datetimefrom;
            frmPrint.dtto = datetimeto;
            frmPrint.phanhe = phanhe;
            frmPrint.nhnhan = nhnhan;
            frmPrint.sotien = sotien;
            frmPrint.loaidien = loaidien;
            frmPrint.isn = isn;
            frmPrint.chieudien = chieudien;
            frmPrint.nhgui = nhgui;
            frmPrint.sogd = sogd;
            frmPrint.loaitien = loaitien;
            frmPrint.osn = osn;
            frmPrint.tcdien = tcdien;
            frmPrint.ttdienden = ttdienden;
            frmPrint.ttdiendi = ttdiendi;
            frmPrint.ttgw = ttgw;
            frmPrint.kieuxuly = kieuxuly;
            frmPrint.HMdat = dsPrint;
            frmPrint.WindowState = FormWindowState.Maximized;
            frmPrint.ShowDialog();
        }

        private void dataMessage_KeyDown_1(object sender, KeyEventArgs e)
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
                                string pDEPARTMENT = dataMessage.Rows[d].Cells["DEPARTMENT"].Value.ToString();
                                string pFIELD20 = dataMessage.Rows[d].Cells["FIELD20"].Value.ToString();

                                if (objcontrolcontent.Resend_message_swift(pDEPARTMENT, pFIELD20,pQUERY_ID, pMSG_DIRECTION) == 1)
                                {
                                    MessageBox.Show("Resend message successfull!", Common.sCaption);
                                    #region Ghi log khi resend message
                                    DateTime dtDateLogin = DateTime.Now;
                                    string strContent = "Resend Message";
                                    int iLoglevel = 1;
                                    string strWorked = "Resend";
                                    string strTable = "SWIFT_MSG_CONTENT";
                                    string strOld_value = pQUERY_ID + pMSG_DIRECTION;
                                    string strNew_value = "";
                                    //goi ham ghilog
                                    objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent,
                                        iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                                    #endregion
                                }
                                else
                                {
                                    MessageBox.Show("Resend message error!", Common.sCaption);
                                }
                            }
                        }
                        d = d + 1;
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
