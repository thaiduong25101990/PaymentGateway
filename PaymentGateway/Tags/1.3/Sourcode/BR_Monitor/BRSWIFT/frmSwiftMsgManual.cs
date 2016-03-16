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

namespace BR.BRSWIFT
{
    public partial class frmSwiftMsgManual : Form
    {

        #region khai bao cac datatable
        private DataSet _dsAll_code;
        private DataTable _dtSearch;
        private DataSet _dsMaster;
        private string Where_Master = "";
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private DataTable _dtViews;
        private string pRows;//chuoi chua index cua cac hang       
        private string pMsg_id;//chua cac gia tri Msg_id        
        private string strRows;
        private static System.Data.DataTable _dtSearchAV;
        #endregion

        #region khai bao cac lop trong Bussiness
        public SWIFT_MSG_CONTENTInfo objcontent = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objControlContent= new SWIFT_MSG_CONTENTController();
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objAllcodeControl = new ALLCODEController();
        GROUPSInfo objInfoGroup = new GROUPSInfo();
        GROUPSController objControlGroup = new GROUPSController();
        SEARCHInfo objSearch = new SEARCHInfo();
        SEARCHController objsearchcontrol = new SEARCHController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        clsCheckInput clsCheck = new clsCheckInput();
        CURRENCYInfo objCurent = new CURRENCYInfo();
        CURRENCYController objCtrlcurrent = new CURRENCYController();
        USERSInfo ObjUser = new USERSInfo();
        USERSController ObjCtrlUser = new USERSController();
        #endregion

        #region khai bao cac bien va cac mang 
        public static string[] FielsName = { "Message type", "Transaction date", "Branch A", "Branch B", "Field 20", "Amount", "Currency ID", "OSN", "Status", "Message number","Teller ID" };
        public string[] FieldsNameDB = { "MSG_TYPE", "TRANS_DATE", "BRANCH_A", "BRANCH_B", "FIELD20", "AMOUNT", "CCYCD", "OSN", "STATUS", "MSG_NO", "TELLER_ID" };

        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        private bool controlKey = false;
        public static int iSearch;
        //private static int iVisible = 0;
        public int iTime;
        //private string status;
        private int isSupervisor;
        public static bool isSupervisorAndTeller = false;       
        public string strMsg_type;
        //private int iTeller;        
        //private int iKieu;
        public int iSelect;
        private string pRow_select;
        public int iRows;
        private int iRole = 0;
        //private string STATETEE;        
        private int iRows_count;
        private string strLOCKSTS;
        //private string strPro;
        //private string strSta;
        //private int ProCount;
        private string Field20;
        private int mn;
        private string Rows_Manu;
        //---------------------------------------------
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
        //private int iCount;
        //private int iCount1;
        private DataTable datExcel = new DataTable();
        private DataTable datExcel1 = new DataTable();
        //------------------mang chua bien-----------------------------

        #endregion

        public frmSwiftMsgManual()
        {
            InitializeComponent();
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

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search_dataMSG();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Search_dataMSG()
        {
            try
            {

                Format();
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Format_advance();
                Search_data();
                Cursor.Current = Cursors.Default;
                BR.BRLib.FomatGrid.Color_datagrid(dataMessage);
                //lay du lieu tu datagrid view ra datatable-----------de in dien---------------------------------------------
                if (dataMessage.Rows.Count != 0) { _dtViews = (DataTable)dataMessage.DataSource; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //ham kiem tra hai datetimepicker 
        private void FROM_TO_DATE()
        {
            try
            {
                if ((datefrom.Checked == false && dateto.Checked == false) || datefrom.Checked == false && dateto.Checked == true)
                { _dateFrom = date1.Value; _dateTo = dateto.Value; }
                else if (datefrom.Checked == true && dateto.Checked == true)
                { _dateFrom = datefrom.Value; _dateTo = dateto.Value; }
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
                FROM_TO_DATE();
                if (iSearch == 1)//tim kiem nang cao
                {
                    _dateFrom = date1.Value; _dateTo = dateto.Value;
                    //goi ham truyen vao la datagridview gia tri tra ve la mot menh de wherer
                    Where = clsDatagridviews.SQL_ADVANCE_MANUAL(datDieukien);
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    if (Where.Trim() == "")
                    {
                        dataMessage = clsDatagridviews.SEARCH_DATA_MANUAL_NORMAL(" Where "  + Where_Master, dataMessage);//Lay du lieu theo dieu kien where                 
                    }
                    else if (Where.Trim() != "")
                    { dataMessage = clsDatagridviews.SEARCH_DATA_MANUAL_NORMAL(Where + " and "  + Where_Master, dataMessage); }
                }
                else//tim kiem thong thuong
                {
                    string vdatef = "";
                    string vdatet = "";
                    if (datefrom.Checked == true) { vdatef = " TRANSDATE  >= " + Get_datetime(datefrom) + " and "; }
                    if (dateto.Checked == true) { vdatet = " TRANSDATE  <= " + Get_datetime(dateto) + " and "; }
                    //DateTime _dt = datefrom.Value.Month
                    Where = clsDatagridviews.Search_Normal_Process(grbSearch);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    if (Where.Trim() == "")
                    {
                        dataMessage = clsDatagridviews.SEARCH_DATA_MANUAL_NORMAL(" Where " + vdatef + vdatet + Where_Master, dataMessage);//Lay du lieu theo dieu kien where                 
                    }
                    else if (Where.Trim() != "")
                    { dataMessage = clsDatagridviews.SEARCH_DATA_MANUAL_NORMAL(Where + " and " + vdatef + vdatet + Where_Master, dataMessage); }
                }
                
                label12.Text = "Total number of messages : " + Convert.ToString(dataMessage.Rows.Count);
                if (dataMessage.Rows.Count == 0)
                {   cmdExport.Enabled = false; cmdProcess.Enabled = false;
                    cmdview.Enabled = false;    }
                else
                {   cmdExport.Enabled = true; cmdProcess.Enabled = true;
                    cmdview.Enabled = true;   }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private string Get_datetime(DateTimePicker _dt)
        {
            try
            {
                string vDay = "";
                string vMonth = "";
                string vYear = "";
                vDay = _dt.Value.Day.ToString();
                vMonth = _dt.Value.Month.ToString();
                vYear = _dt.Value.Year.ToString();
                if (vDay.Trim().Length == 1) { vDay = "0" + vDay; }
                if (vMonth.Trim().Length == 1) { vMonth = "0" + vMonth; }
                return vYear + vMonth + vDay;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        private void ColumnsRead(DataGridView Datagrid)
        {
            int b = 1;
            while (b < Datagrid.Columns.Count)
            {
                Datagrid.Columns[b].ReadOnly = true;
                b = b + 1;
            }
        }

        private void ColumsHeader(DataGridView Datagrid)
        {
            try
            {
                int g = 1;
                while (g < Datagrid.Columns.Count)
                {
                    string strColumns = Datagrid.Columns[g].HeaderText.ToString();
                    if (strColumns.Trim() != "BRANCH_A" && strColumns.Trim() != "BRANCH_B")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    }
                    else
                    {
                        if (strColumns.Trim() == "BRANCH_A")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "SENDER";
                        }
                        if (strColumns.Trim() == "BRANCH_B")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "RECEIVER";
                        }
                    }
                    if (strColumns.Trim() == "DEPARTMENT")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "MODULE";
                    }
                    if (strColumns.Trim() == "FIELD20")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "REF NUMBER";
                    }
                    if (strColumns.Trim() == "FIELD21")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "RELATED REF NO";
                    }
                    if (strColumns.Trim() == "CCYCD")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "CCY";
                    }
                    if (strColumns.Trim() == "OFFICER_ID")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "SUPERVISOR ID";
                    }

                    if (strColumns.Trim() == "SWIFTSTS")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = "MESSAGE STATUS";
                    }                   
                    Datagrid.ColumnHeadersHeight = 21;                    
                    Datagrid.Columns[0].Width = 40;                   
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void CheckIsApprove()
        {
            int iDem = 0;
            int f = 0;           
            while (f < dataMessage.Rows.Count)
            {
                if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                {                    
                    if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                    {
                        iDem = iDem + 1;                        
                    }
                }                
                f = f + 1;
            }
            if (iDem == 0)
            {
                //Hien thi form co nut OK voi nguoi dung la GDV
                if (dataMessage.CurrentRow.Cells["SWMSTS"].Value.ToString().Trim() == Common.WAITING || dataMessage.CurrentRow.Cells["SWMSTS"].Value.ToString().Trim() == Common.WAPPROVING || dataMessage.CurrentRow.Cells["STATUS"].Value.ToString().Trim() == "SENT" || dataMessage.CurrentRow.Cells["SWMSTS"].Value.ToString().Trim() == Common.NPROCESS)
                {
                    frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                    frmSMsgManualIn.isApprove = false;
                }
                //Hien thi form co nut Approve va Reject voi nguoi dung la KSV
                else if (dataMessage.CurrentRow.Cells["PSWMSTS"].Value.ToString().Trim() == Common.WAITING & dataMessage.CurrentRow.Cells["SWMSTS"].Value.ToString().Trim() == Common.WAPPROVING)
                {
                    frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                    frmSMsgManualIn.isApprove = true;
                }
                else if (dataMessage.CurrentRow.Cells["SWMSTS"].Value.ToString().Trim() == Common.WAPPROVING_RESENT)
                {
                    if (dataMessage.CurrentRow.Cells["PSWMSTS"].Value.ToString().Trim() == Common.NPROCESS || dataMessage.CurrentRow.Cells["STATUS"].Value.ToString().Trim() == "SENT")
                    {
                        frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                        frmSMsgManualIn.isApprove = true;
                    }
                }
            }
            else if (iDem > 0)
            {
                if (dataMessage.Rows.Count != 0)
                {
                    int d = 0;
                    while (d < dataMessage.Rows.Count)
                    {

                        if (dataMessage.Rows[d].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataMessage.Rows[d].Cells[0].Value.ToString() == "True")
                            {
                                //Hien thi form co nut OK voi nguoi dung la GDV
                                if (dataMessage.Rows[d].Cells["SWMSTS"].Value.ToString().Trim() == Common.WAITING || dataMessage.Rows[d].Cells["SWMSTS"].Value.ToString().Trim() == Common.WAPPROVING || dataMessage.Rows[d].Cells["STATUS"].Value.ToString().Trim() == "SENT" || dataMessage.Rows[d].Cells["SWMSTS"].Value.ToString().Trim() == Common.NPROCESS)
                                {
                                    frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                                    frmSMsgManualIn.isApprove = false;
                                }
                                //Hien thi form co nut Approve va Reject voi nguoi dung la KSV
                                else if (dataMessage.Rows[d].Cells["PSWMSTS"].Value.ToString().Trim() == Common.WAITING & dataMessage.Rows[d].Cells["SWMSTS"].Value.ToString().Trim() == Common.WAPPROVING)
                                {
                                    frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                                    frmSMsgManualIn.isApprove = true;
                                }
                                else if (dataMessage.Rows[d].Cells["SWMSTS"].Value.ToString().Trim() == Common.WAPPROVING_RESENT)
                                {
                                    if (dataMessage.Rows[d].Cells["PSWMSTS"].Value.ToString().Trim() == Common.NPROCESS || dataMessage.Rows[d].Cells["STATUS"].Value.ToString().Trim() == "SENT")
                                    {
                                        frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                                        frmSMsgManualIn.isApprove = true;
                                    }
                                }
                            }
                        }
                        dataMessage.Rows[d].Cells[0].Value = null;
                        d = d + 1;
                    }
                }
            }
        }
        //ham kiem tra xem su lua chon cac o check box
        private void Check_Select_Rows()
        {
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
                            iSelect = 1;
                            if (pRow_select == "")
                            {
                                pRow_select = dataMessage.Rows[b].Cells["QUERY_ID"].Value.ToString();
                            }
                            else
                            {
                                pRow_select = pRow_select + "," + dataMessage.Rows[b].Cells["QUERY_ID"].Value.ToString();
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
        private void Process_One()//chon dong ma nguoi nay di chuot len
        {
            try
            {
                if (iRows == -1)
                {
                    iRows = 0;
                }
                if (dataMessage.Rows.Count == 0)
                {
                    Common.ShowError("There is no message!", 3, MessageBoxButtons.OK);                    
                    return;
                }
                else
                {
                    frmSwiftMsgManualDup frmSMsgManualIn = new frmSwiftMsgManualDup(); //STATUS    
                    frmSMsgManualIn.strSTATUS = dataMessage.Rows[iRows].Cells["STATUS"].Value.ToString();
                    frmSMsgManualIn.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                    frmSMsgManualIn.strQUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                    frmSMsgManualIn.strBRANCH_A = dataMessage.Rows[iRows].Cells["BRANCH_A"].Value.ToString();
                    frmSMsgManualIn.strBRANCH_B = dataMessage.Rows[iRows].Cells["BRANCH_B"].Value.ToString();
                    frmSMsgManualIn.strAMOUNT = dataMessage.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                    frmSMsgManualIn.strstrTRANS_NO = dataMessage.Rows[iRows].Cells["FIELD20"].Value.ToString();
                    frmSMsgManualIn.strTRANS_DATE = dataMessage.Rows[iRows].Cells["TRANS_DATE"].Value.ToString();
                    frmSMsgManualIn.strRM_NUMBER = dataMessage.Rows[iRows].Cells["RM_NUMBER"].Value.ToString();
                    frmSMsgManualIn.strDEPARTMENT = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                    frmSMsgManualIn.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                    frmSMsgManualIn.strSWMSTS = dataMessage.Rows[iRows].Cells["SWIFTSTS"].Value.ToString();
                    //frmSMsgManualIn.strPSWMSTS = dataMessage.Rows[iRows].Cells["PSWMSTS"].Value.ToString();
                    frmSMsgManualIn.strCCYCD = dataMessage.Rows[iRows].Cells["CCYCD"].Value.ToString();
                    frmSMsgManualIn.strTELLERID = dataMessage.Rows[iRows].Cells["TELLER_ID"].Value.ToString();
                    frmSMsgManualIn.strMSG_TYPE = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                    frmSMsgManualIn.strMSG_SRC = dataMessage.Rows[iRows].Cells["MSG_SRC"].Value.ToString();
                    frmSMsgManualIn.strPROCESSSTS = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                    frmSMsgManualIn.strLOCKSTS = dataMessage.Rows[iRows].Cells["LOCKSTS"].Value.ToString();
                    strLOCKSTS = dataMessage.Rows[iRows].Cells["LOCKSTS"].Value.ToString();
                    frmSMsgManualIn.strLOCK_TELLERID = dataMessage.Rows[iRows].Cells["LOCK_TELLERID"].Value.ToString();
                    DataTable datt = new DataTable();
                    datt = objControlContent.Check_LOCKSTS(dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString(), "SWIFT_MSG_CONTENT");
                    if (datt.Rows[0]["LOCKSTS"].ToString().Trim() == "1")
                    {
                        Common.ShowError("This message is being processed by other user!", 2, MessageBoxButtons.OK);                        
                    }
                    else
                    {
                        //----------------------------------------------------------------------
                        objcontent.MSG_ID = Convert.ToInt32(dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString());
                        objcontent.LOCKSTS = "1";
                        objcontent.LOCK_TELLERID = Common.Userid;
                        objcontent.Table_Name = "SWIFT_MSG_CONTENT";
                        objControlContent.Lock_User(objcontent);
                        this.Hide();
                        frmSMsgManualIn.ShowDialog(this);
                        objcontent.MSG_ID = Convert.ToInt32(dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString());
                        objcontent.LOCKSTS = "";
                        objcontent.LOCK_TELLERID = "";
                        objcontent.Table_Name = "SWIFT_MSG_CONTENT";
                        objControlContent.Lock_User(objcontent);
                    }
                    if (Common.iCancel == 1)
                    {
                        dataMessage.Rows.RemoveAt(iRows);
                    }
                    this.Show();                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void UPdate_datagrid()
        {
            try
            {
                //---QUYND--UPDATE-------------------------------------------------------------------------
                if (iRole == 1)//quyen Teller (khi thu hien xong thi xoa dong do di)
                {
                    if (objcontent.OK == "True")
                    {
                        dataMessage.Rows.RemoveAt(iRows);
                    }
                }
                if (iRole == 0)//tung ung voi quyen Suppervisor
                {
                    if (objcontent.Reject == "True")
                    {                       
                        dataMessage.Rows.RemoveAt(iRows);
                    }
                    if (objcontent.Approve == "True")//phe duyet
                    {                        
                        dataMessage.Rows.RemoveAt(iRows);
                    }
                }
                if (iRole == 2)//quyen Teller hay la quyen cua Teller_Suppervisor
                {
                    if (objcontent.OK == "True")//thuc hien
                    {                       
                        dataMessage.Rows.RemoveAt(iRows);
                    }
                    if (objcontent.Reject == "True")//tu choi
                    {
                        dataMessage.Rows[iRows].Cells["TELLER_ID"].Value = objcontent.TELLER_ID;                       
                        dataMessage.Rows[iRows].Cells["SWIFTSTS"].Value = objcontent.SWMSTS1;
                        dataMessage.Rows[iRows].Cells["PSWMSTS"].Value = objcontent.PSWMSTS1;
                    }
                    if (objcontent.Approve == "True")//phe duyet
                    {                       
                        dataMessage.Rows.RemoveAt(iRows);
                    }
                }
                //-----------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void Log_messages()
        {
            try
            {
                Rows_Manu = "";
                Field20 = "";
                mn = 0;//so dien da bi Users khac log roi
                int g = 0;
                while (g < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[g].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[g].Cells[0].Value.ToString() == "True")
                        {
                            string dd = dataMessage.Rows[g].Cells["Tabl"].Value.ToString();
                            DataTable datt = new DataTable();
                            string strTAl = "";
                            if (dd.Trim() == "1")
                            {
                                strTAl = "SWIFT_MSG_CONTENT";
                            }
                            else if (dd.Trim() == "2")
                            {
                                strTAl = "SWIFT_MSG_ALL";
                            }
                            else if (dd.Trim() == "3")
                            {
                                strTAl = "SWIFT_MSG_ALL_HIS";
                            }
                            datt = objControlContent.Check_LOCKSTS(dataMessage.Rows[g].Cells["MSG_ID"].Value.ToString(), strTAl);
                            if (datt.Rows[0]["LOCKSTS"].ToString().Trim() == "1")
                            {
                                if (datt.Rows[0]["LOCK_TELLERID"].ToString().Trim() == Common.Userid.Trim())//bo qua
                                {
                                    //chinh Users cua minh
                                }
                                else if (datt.Rows[0]["LOCK_TELLERID"].ToString().Trim() == Common.Userid.Trim())
                                {
                                    mn = mn + 1;
                                    if (Field20 == "")//FIELD20
                                    {
                                        Field20 = dataMessage.Rows[g].Cells["FIELD20"].Value.ToString();
                                    }
                                    else
                                    {
                                        Field20 = Field20 + " ; " + dataMessage.Rows[g].Cells["FIELD20"].Value.ToString();
                                    }
                                    if (Rows_Manu.Trim() == "")
                                    {
                                        Rows_Manu = Convert.ToString(g);
                                    }
                                    else
                                    {
                                        Rows_Manu = Rows_Manu + "," + Convert.ToString(g);
                                    }
                                }
                            }
                            else
                            {
                                //----------------------------------------------------------------------
                                objcontent.MSG_ID = Convert.ToInt32(dataMessage.Rows[g].Cells["MSG_ID"].Value.ToString());
                                objcontent.LOCKSTS = "1";
                                objcontent.LOCK_TELLERID = Common.Userid;                                
                                objcontent.Table_Name = "SWIFT_MSG_CONTENT";                                
                                objControlContent.Lock_User(objcontent);//goi ham log Message                               
                            }
                        }
                    }
                    g = g + 1;
                }
                if (mn != 0)
                {
                    Common.ShowError(mn + " :messages " + "\r\n" + "REF NO : " + Field20 + "\r\n" + "This message is being processed by other user!", 3, MessageBoxButtons.OK);                    
                }
                if (Rows_Manu.Trim() != "")
                {
                    String[] M = Rows_Manu.Split(new String[] { "," }, StringSplitOptions.None);//cat chuoi
                    int k = M.Count<String>();
                    int v = 0;
                    while (v < k)
                    {
                        dataMessage.Rows[Convert.ToInt32(M[v])].Cells[0].Value = null;
                        v = v + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Return_null()
        {
            try
            {
                int b = 0;
                while (b < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[b].Cells[0].Value.ToString() == "True")
                        {
                            dataMessage.Rows[b].Cells[0].Value = null;                                                        
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
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            try
            {
                strRows = "";
                iRows_count = dataMessage.Rows.Count;
                objcontent.OK = "False";
                objcontent.Reject = "False";
                objcontent.Approve = "False";
                //----------------------------------------------------------------------
                pRows = "";//chuoi chua index cua cac hang                
                pMsg_id = "";
                //-----------------------------------------------------------------------                
                if (clsDatagridviews.Check_Select(dataMessage))
                {
                    int f = 0;
                    while (f < dataMessage.Rows.Count)
                    {
                        if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                            {                                
                                string strMsgid = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();                               
                                if (pMsg_id.Trim() == "")
                                { pMsg_id = strMsgid; }
                                else { pMsg_id = pMsg_id + "," + strMsgid; }    

                                if (pRows.Trim() == "")
                                { pRows = Convert.ToString(f); }
                                else { pRows = pRows + "," + Convert.ToString(f); }
                            }
                        }
                        f = f + 1;
                    }
                    DataTable _dtmessage = objControlContent.PROCESS_HANDICRAFT_SIBS_SWIFT( pMsg_id, pRows, Common.Userid, out _dtmessage);
                    strRows = _dtmessage.Rows[0]["ROWS_SELECT"].ToString();//chuoi cac dong da duoc xu ly hay da duoc users khac log roi
                    if (strRows == "")
                    {
                        Process();
                    }
                    if (strRows != "")//co dien da duoc xu ly hay da duoc log roi
                    {
                        Refresh_data();
                    }
                }
                else
                {
                    if (iRows == -1)
                    {
                        iRows = 0;                       
                        pMsg_id = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();                        
                        pRows = Convert.ToString(iRows);
                        DataTable _dtmessage = objControlContent.PROCESS_HANDICRAFT_SIBS_SWIFT( pMsg_id, pRows, Common.Userid, out _dtmessage);
                        strRows = _dtmessage.Rows[0]["ROWS_SELECT"].ToString();
                        if (strRows != "")//co dien da duoc xu ly hay da duoc log roi
                        {
                            Common.ShowError(1 + ":message is being processed by other user!" + "\r\n" + "You are refresh data", 3, MessageBoxButtons.OK);                           
                        }
                        else
                        { Process_one(iRows); }
                    }
                    else
                    {                        
                        pMsg_id = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();                       
                        pRows = Convert.ToString(iRows);
                        DataTable _dtmessage = objControlContent.PROCESS_HANDICRAFT_SIBS_SWIFT(pMsg_id, pRows, Common.Userid, out _dtmessage);
                        strRows = _dtmessage.Rows[0]["ROWS_SELECT"].ToString();
                        if (strRows != "")//co dien da duoc xu ly hay da duoc log roi
                        {
                            Common.ShowError(1 + ":message is being processed by other user!" + "\r\n" + "You are refresh data", 3, MessageBoxButtons.OK);
                        }
                        else
                        { Process_one(iRows); }
                    }
                }
                Search_dataMSG();

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            Lbtong.Text = Convert.ToString(dataMessage.Rows.Count);
            if (dataMessage.Rows.Count==0)
            {
                cmdProcess.Enabled = false;
                cmdview.Enabled = false;
            }           
        }


        private void Process()
        {
            try
            {
                frmSwiftMsgManualEdit frmSMsgManualEdit = new frmSwiftMsgManualEdit();
                frmSwiftMsgManualDup frmSMsgManualIn = new frmSwiftMsgManualDup();
                int f = 0;
                while (f < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            string m_vQUERY_ID = dataMessage.Rows[f].Cells["QUERY_ID"].Value.ToString();
                            DataTable _dt = new DataTable();
                            _dt = objControlContent.GET_PROCESSSTS(m_vQUERY_ID);
                            string m_vPROCESSS = _dt.Rows[0]["PROCESSSTS"].ToString().Trim();
                            string m_vMSG_SRC = _dt.Rows[0]["MSG_SRC"].ToString().Trim();

                            if (dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString() != Common.PROCESSSTS_REPAIR)
                            {
                                if (m_vPROCESSS != Common.PROCESSSTS_REPAIR)
                                {
                                    frmSMsgManualIn.strTable_name = "SWIFT_MSG_CONTENT";
                                    frmSMsgManualIn.strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                                    frmSMsgManualIn.strQUERY_ID = dataMessage.Rows[f].Cells["QUERY_ID"].Value.ToString();
                                    frmSMsgManualIn.strBRANCH_A = dataMessage.Rows[f].Cells["BRANCH_A"].Value.ToString();
                                    frmSMsgManualIn.strBRANCH_B = dataMessage.Rows[f].Cells["BRANCH_B"].Value.ToString();
                                    frmSMsgManualIn.strAMOUNT = dataMessage.Rows[f].Cells["AMOUNT"].Value.ToString();
                                    frmSMsgManualIn.strFIELD20 = dataMessage.Rows[f].Cells["FIELD20"].Value.ToString();
                                    frmSMsgManualIn.strTRANS_DATE = dataMessage.Rows[f].Cells["TRANS_DATE"].Value.ToString();
                                    frmSMsgManualIn.strRM_NUMBER = dataMessage.Rows[f].Cells["RM_NUMBER"].Value.ToString();
                                    frmSMsgManualIn.strDEPARTMENT = dataMessage.Rows[f].Cells["DEPARTMENT"].Value.ToString();
                                    frmSMsgManualIn.strSWMSTS = dataMessage.Rows[f].Cells["SWMSTS"].Value.ToString();
                                    frmSMsgManualIn.strPROCESSSTS = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString();
                                    frmSMsgManualIn.strCCYCD = dataMessage.Rows[f].Cells["CCYCD"].Value.ToString();
                                    frmSMsgManualIn.strMSG_TYPE = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString();
                                    frmSMsgManualIn.strSTATUS = dataMessage.Rows[f].Cells["STATUS"].Value.ToString();
                                    frmSMsgManualIn.strMSG_SRC = m_vMSG_SRC;
                                    frmSMsgManualIn.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                                    if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "1")
                                    {
                                        frmSMsgManualIn.strTable_name = "SWIFT_MSG_CONTENT";
                                    }
                                    if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "2")
                                    {
                                        frmSMsgManualIn.strTable_name = "SWIFT_MSG_ALL";
                                    }
                                    if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "3")
                                    {
                                        frmSMsgManualIn.strTable_name = "SWIFT_MSG_ALL_HIS";
                                    }
                                    this.Hide();
                                    frmSMsgManualIn.ShowDialog(this);
                                    if (Common.iOk == 1 || Common.iCancel == 1)
                                    {
                                        dataMessage.Rows.RemoveAt(f);
                                        Common.iOk = 0;
                                    }
                                }
                                else
                                {
                                    frmSMsgManualEdit.strTable_name = "SWIFT_MSG_CONTENT";
                                    frmSMsgManualEdit.strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                                    frmSMsgManualEdit.strQUERY_ID = dataMessage.Rows[f].Cells["QUERY_ID"].Value.ToString();
                                    frmSMsgManualEdit.strBRANCH_A = dataMessage.Rows[f].Cells["BRANCH_A"].Value.ToString();
                                    frmSMsgManualEdit.strBRANCH_B = dataMessage.Rows[f].Cells["BRANCH_B"].Value.ToString();
                                    frmSMsgManualEdit.strAMOUNT = dataMessage.Rows[f].Cells["AMOUNT"].Value.ToString();
                                    frmSMsgManualEdit.strFIELD20 = dataMessage.Rows[f].Cells["FIELD20"].Value.ToString();
                                    frmSMsgManualEdit.strTRANS_DATE = dataMessage.Rows[f].Cells["TRANS_DATE"].Value.ToString();
                                    frmSMsgManualEdit.strRM_NUMBER = dataMessage.Rows[f].Cells["RM_NUMBER"].Value.ToString();
                                    frmSMsgManualEdit.strDEPARTMENT = dataMessage.Rows[f].Cells["DEPARTMENT"].Value.ToString();
                                    frmSMsgManualEdit.strSWMSTS = dataMessage.Rows[f].Cells["SWMSTS"].Value.ToString();
                                    frmSMsgManualEdit.strPROCESSSTS = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString();
                                    frmSMsgManualEdit.strCCYCD = dataMessage.Rows[f].Cells["CCYCD"].Value.ToString();
                                    frmSMsgManualEdit.strMSG_TYPE = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString();
                                    frmSMsgManualEdit.strSTATUS = dataMessage.Rows[f].Cells["STATUS"].Value.ToString();
                                    frmSMsgManualEdit.strMSG_SRC = dataMessage.Rows[f].Cells["MSG_SRC"].Value.ToString();
                                    frmSMsgManualEdit.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                                    if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "1")
                                    {
                                        frmSMsgManualEdit.strTable_name = "SWIFT_MSG_CONTENT";
                                    }
                                    if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "2")
                                    {
                                        frmSMsgManualEdit.strTable_name = "SWIFT_MSG_ALL";
                                    }
                                    if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "3")
                                    {
                                        frmSMsgManualEdit.strTable_name = "SWIFT_MSG_ALL_HIS";
                                    }
                                    this.Hide();
                                    frmSMsgManualEdit.ShowDialog(this);
                                    if (Common.iOk == 1 || Common.iCancel == 1)
                                    {
                                        dataMessage.Rows.RemoveAt(f);
                                        Common.iOk = 0;
                                    }
                                }
                            }
                            else
                            {
                                frmSMsgManualEdit.strTable_name = "SWIFT_MSG_CONTENT";
                                frmSMsgManualEdit.strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                                frmSMsgManualEdit.strQUERY_ID = dataMessage.Rows[f].Cells["QUERY_ID"].Value.ToString();
                                frmSMsgManualEdit.strBRANCH_A = dataMessage.Rows[f].Cells["BRANCH_A"].Value.ToString();
                                frmSMsgManualEdit.strBRANCH_B = dataMessage.Rows[f].Cells["BRANCH_B"].Value.ToString();
                                frmSMsgManualEdit.strAMOUNT = dataMessage.Rows[f].Cells["AMOUNT"].Value.ToString();
                                frmSMsgManualEdit.strFIELD20 = dataMessage.Rows[f].Cells["FIELD20"].Value.ToString();
                                frmSMsgManualEdit.strTRANS_DATE = dataMessage.Rows[f].Cells["TRANS_DATE"].Value.ToString();
                                frmSMsgManualEdit.strRM_NUMBER = dataMessage.Rows[f].Cells["RM_NUMBER"].Value.ToString();
                                frmSMsgManualEdit.strDEPARTMENT = dataMessage.Rows[f].Cells["DEPARTMENT"].Value.ToString();
                                frmSMsgManualEdit.strSWMSTS = dataMessage.Rows[f].Cells["SWMSTS"].Value.ToString();
                                frmSMsgManualEdit.strPROCESSSTS = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString();
                                frmSMsgManualEdit.strCCYCD = dataMessage.Rows[f].Cells["CCYCD"].Value.ToString();
                                frmSMsgManualEdit.strMSG_TYPE = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString();
                                frmSMsgManualEdit.strSTATUS = dataMessage.Rows[f].Cells["STATUS"].Value.ToString();
                                frmSMsgManualEdit.strMSG_SRC = dataMessage.Rows[f].Cells["MSG_SRC"].Value.ToString();
                                frmSMsgManualEdit.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                                if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "1")
                                {
                                    frmSMsgManualEdit.strTable_name = "SWIFT_MSG_CONTENT";
                                }
                                if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "2")
                                {
                                    frmSMsgManualEdit.strTable_name = "SWIFT_MSG_ALL";
                                }
                                if (dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString() == "3")
                                {
                                    frmSMsgManualEdit.strTable_name = "SWIFT_MSG_ALL_HIS";
                                }
                                this.Hide();
                                frmSMsgManualEdit.ShowDialog(this);
                                if (Common.iOk == 1 || Common.iCancel == 1)
                                {
                                    dataMessage.Rows.RemoveAt(f);
                                    Common.iOk = 0;
                                }
                            }
                        }
                    }

                    if (frmSMsgManualIn.bIsCloseForm == true || frmSMsgManualEdit.bIsCloseForm == true)
                    {
                        this.Show();
                        Refresh_data();
                        return;
                    }
                    else
                    {
                        if (Common.iCancel == 0) { objControlContent.DELETE_PROCESS_HANDER("," + dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString() + ","); }
                        if (iRows_count == (dataMessage.Rows.Count - 1))
                        { }
                        else if (iRows_count == dataMessage.Rows.Count)
                        {
                            dataMessage.Rows[f].Cells[0].Value = null;
                            f = f + 1;
                        }
                        iRows_count = dataMessage.Rows.Count;
                        this.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        //Refresh ve null voi cac dong chua duoc xu ly
        private void Refresh_data()
        {
            try
            {
                int iMessage = 0;
                string Msg_id = "";               
                bool _bool = false;
                String[] M = strRows.Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
                int kCount = M.Count<String>();
                if (kCount == 1)
                {
                    if (M[0].Trim() == "")
                    {
                        iMessage = 0;
                    }
                    else
                    {
                        iMessage = kCount;
                    }
                }
                int f = 0;
                while (f < dataMessage.Rows.Count)
                {
                    _bool = false;
                    if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            int j = 0;
                            while (j < kCount)
                            {
                                if (M[j].Trim() != "")
                                {
                                    if (f == Convert.ToInt32(M[j]))
                                    {
                                        _bool = true;
                                        break;
                                    }
                                }
                                j = j + 1;
                            }
                            if (_bool == false)
                            {
                                dataMessage.Rows[f].Cells[0].Value = null;
                                Msg_id = Msg_id + "," + dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                            }
                        }
                    }
                    f = f + 1;
                }
                objControlContent.DELETE_PROCESS_HANDER(Msg_id + ",");
                if (iMessage != 0)
                {
                    Common.ShowError(iMessage + ":message is being processed by other user!" + "\r\n" + "You are refresh data!", 3, MessageBoxButtons.OK);                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Process_one(int iRows)
        {
            try
            {
                string m_vQUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                DataTable _dt = new DataTable();
                _dt = objControlContent.GET_PROCESSSTS(m_vQUERY_ID);
                string m_vPROCESSS = _dt.Rows[0]["PROCESSSTS"].ToString().Trim();
                string m_vMSG_SRC = _dt.Rows[0]["MSG_SRC"].ToString().Trim();

                frmSwiftMsgManualEdit frmSMsgManualEdit = new frmSwiftMsgManualEdit();
                frmSwiftMsgManualDup frmSMsgManualIn = new frmSwiftMsgManualDup();
                if (dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() != Common.PROCESSSTS_REPAIR)
                {
                    if (m_vPROCESSS != Common.PROCESSSTS_REPAIR)
                    {
                        frmSMsgManualIn.strTable_name = "SWIFT_MSG_CONTENT";
                        frmSMsgManualIn.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                        frmSMsgManualIn.strQUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                        frmSMsgManualIn.strBRANCH_A = dataMessage.Rows[iRows].Cells["BRANCH_A"].Value.ToString();
                        frmSMsgManualIn.strBRANCH_B = dataMessage.Rows[iRows].Cells["BRANCH_B"].Value.ToString();
                        frmSMsgManualIn.strAMOUNT = dataMessage.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                        frmSMsgManualIn.strFIELD20 = dataMessage.Rows[iRows].Cells["FIELD20"].Value.ToString();
                        frmSMsgManualIn.strTRANS_DATE = dataMessage.Rows[iRows].Cells["TRANS_DATE"].Value.ToString();
                        frmSMsgManualIn.strRM_NUMBER = dataMessage.Rows[iRows].Cells["RM_NUMBER"].Value.ToString();
                        frmSMsgManualIn.strDEPARTMENT = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                        frmSMsgManualIn.strSWMSTS = dataMessage.Rows[iRows].Cells["SWMSTS"].Value.ToString();
                        frmSMsgManualIn.strPROCESSSTS = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                        frmSMsgManualIn.strCCYCD = dataMessage.Rows[iRows].Cells["CCYCD"].Value.ToString();
                        frmSMsgManualIn.strMSG_TYPE = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                        frmSMsgManualIn.strSTATUS = dataMessage.Rows[iRows].Cells["STATUS"].Value.ToString();
                        frmSMsgManualIn.strMSG_SRC = m_vMSG_SRC;
                        frmSMsgManualIn.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                        this.Hide();
                        frmSMsgManualIn.ShowDialog(this);
                        if (Common.iOk == 1 || Common.iCancel == 1)
                        {
                            dataMessage.Rows.RemoveAt(iRows);
                            Common.iOk = 0;
                        }
                    }
                    else
                    {
                        frmSMsgManualEdit.strTable_name = "SWIFT_MSG_CONTENT";
                        frmSMsgManualEdit.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                        frmSMsgManualEdit.strQUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                        frmSMsgManualEdit.strBRANCH_A = dataMessage.Rows[iRows].Cells["BRANCH_A"].Value.ToString();
                        frmSMsgManualEdit.strBRANCH_B = dataMessage.Rows[iRows].Cells["BRANCH_B"].Value.ToString();
                        frmSMsgManualEdit.strAMOUNT = dataMessage.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                        frmSMsgManualEdit.strFIELD20 = dataMessage.Rows[iRows].Cells["FIELD20"].Value.ToString();
                        frmSMsgManualEdit.strTRANS_DATE = dataMessage.Rows[iRows].Cells["TRANS_DATE"].Value.ToString();
                        frmSMsgManualEdit.strRM_NUMBER = dataMessage.Rows[iRows].Cells["RM_NUMBER"].Value.ToString();
                        frmSMsgManualEdit.strDEPARTMENT = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                        frmSMsgManualEdit.strSWMSTS = dataMessage.Rows[iRows].Cells["SWMSTS"].Value.ToString();
                        frmSMsgManualEdit.strPROCESSSTS = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                        frmSMsgManualEdit.strCCYCD = dataMessage.Rows[iRows].Cells["CCYCD"].Value.ToString();
                        frmSMsgManualEdit.strMSG_TYPE = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                        frmSMsgManualEdit.strSTATUS = dataMessage.Rows[iRows].Cells["STATUS"].Value.ToString();
                        frmSMsgManualEdit.strMSG_SRC = dataMessage.Rows[iRows].Cells["MSG_SRC"].Value.ToString();
                        frmSMsgManualEdit.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                        this.Hide();
                        frmSMsgManualEdit.ShowDialog(this);
                        if (Common.iOk == 1 || Common.iCancel == 1)
                        {
                            dataMessage.Rows.RemoveAt(iRows);
                            Common.iOk = 0;
                        }
                    }
                }
                else
                {
                    frmSMsgManualEdit.strTable_name = "SWIFT_MSG_CONTENT";
                    frmSMsgManualEdit.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                    frmSMsgManualEdit.strQUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                    frmSMsgManualEdit.strBRANCH_A = dataMessage.Rows[iRows].Cells["BRANCH_A"].Value.ToString();
                    frmSMsgManualEdit.strBRANCH_B = dataMessage.Rows[iRows].Cells["BRANCH_B"].Value.ToString();
                    frmSMsgManualEdit.strAMOUNT = dataMessage.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                    frmSMsgManualEdit.strFIELD20 = dataMessage.Rows[iRows].Cells["FIELD20"].Value.ToString();
                    frmSMsgManualEdit.strTRANS_DATE = dataMessage.Rows[iRows].Cells["TRANS_DATE"].Value.ToString();
                    frmSMsgManualEdit.strRM_NUMBER = dataMessage.Rows[iRows].Cells["RM_NUMBER"].Value.ToString();
                    frmSMsgManualEdit.strDEPARTMENT = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                    frmSMsgManualEdit.strSWMSTS = dataMessage.Rows[iRows].Cells["SWMSTS"].Value.ToString();
                    frmSMsgManualEdit.strPROCESSSTS = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                    frmSMsgManualEdit.strCCYCD = dataMessage.Rows[iRows].Cells["CCYCD"].Value.ToString();
                    frmSMsgManualEdit.strMSG_TYPE = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                    frmSMsgManualEdit.strSTATUS = dataMessage.Rows[iRows].Cells["STATUS"].Value.ToString();
                    frmSMsgManualEdit.strMSG_SRC = dataMessage.Rows[iRows].Cells["MSG_SRC"].Value.ToString();
                    frmSMsgManualEdit.strAUTO = dataMessage.Rows[iRows].Cells["AUTO"].Value.ToString();
                    this.Hide();
                    frmSMsgManualEdit.ShowDialog(this);
                    if (Common.iOk == 1 || Common.iCancel == 1)
                    {
                        dataMessage.Rows.RemoveAt(iRows);
                        Common.iOk = 0;
                    }
                }
                if (frmSMsgManualIn.bIsCloseForm == true || Common.iCancel == 0 || frmSMsgManualEdit.bIsCloseForm == true)
                {
                    objControlContent.DELETE_PROCESS_HANDER("," + dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString() + ",");
                }
                this.Show();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }



        private Boolean CheckTellerID(int strQueryID)
        {
            bool result = true;           
            DataSet ds = new DataSet();
            ds = objControlContent.GetTellerID(strQueryID);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                string TellerID = ds.Tables[0].Rows[0][0].ToString();
                if (TellerID == Common.Userid)
                {
                    Common.ShowError("Have no authorities!", 3, MessageBoxButtons.OK);                    
                    result = false;
                }
            }
            return result;
        }
        private void cmdStatement_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrint frmPrinter = new frmPrint();
                frmPrinter.PrintType = "SWIFT_03";
                frmPrinter.strQueryIDSwift = Convert.ToInt32((dataMessage.CurrentRow.Cells["QUERY_ID"].Value.ToString()).Trim());
                frmPrinter.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
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
                                frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                                if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                                    {                                        
                                        frmSWIFT.strMSG_ID = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                                        //DatHM add
                                        frmSWIFT.strDepartment = dataMessage.Rows[f].Cells["DEPARTMENT"].Value.ToString();
                                        frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[f].Cells["MSG_DIRECTION"].Value.ToString();
                                        frmSWIFT.strMsg_type = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString();
                                        frmSWIFT._Processsts = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString(); 
                                        //end DatHM
                                        this.Hide();
                                        frmSWIFT.ShowDialog();
                                        this.Show();
                                        dataMessage.Rows[f].Cells[0].Value = null;
                                    }
                                }
                                if (frmSWIFT.bIsCloseForm == true)
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
                                {
                                    f = f + 1;
                                }
                            }
                        }
                        else
                        {
                            frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                            frmSWIFT.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                            //DatHM add
                            frmSWIFT.strDepartment = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                            frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
                            frmSWIFT.strMsg_type = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                            frmSWIFT._Processsts = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString(); 
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

        private void cmdadd_Click(object sender, EventArgs e)
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
        

        private void Locate_controls()
        {
            try
            {               
                datefrom.Checked = false;
                dateto.Checked = false;
                this.date1.Enabled = false;
                this.datefrom.MaxDate = DateTime.Now;
                this.datefrom.MaxDate = dateto.Value;
                this.dateto.MaxDate = DateTime.Now;
                this.cmdAdvance.Location = new Point(cmdNornal.Location.X, cmdNornal.Location.Y);
                //-------------------------------------------
                dataMessage.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataMessage.Columns[0].HeaderCell = dgvColumnHeader;
                dataMessage.Columns[0].Width = 26;
                //--------------------------------------------
                iSearch = 0;
                cmdview.Enabled = false;
                grbSearchnhanh.Hide();
                grbDieukien.Hide();
                //-----------------
                this.grbSearch.Location = new Point(grbSearchnhanh.Location.X, grbSearchnhanh.Location.Y);
                this.grbSearch.Size = new Size(grbSearchnhanh.Size.Width + grbDieukien.Size.Width + 2, grbSearchnhanh.Size.Height);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void frmSwiftMsgManual_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                CheckIsSupervisor();//check quyen teller hay supper 
                Locate_controls();
                Get_Status();//goi ham lay cac trang thai dien
                Load_data();//goi ham lay du lieu
                datefrom.Checked = false;
                dateto.Checked = false;
                BR.BRLib.FomatGrid.Color_datagrid(dataMessage);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Get_Status()
        {
            try
            {
                _dsAll_code = objAllcodeControl.GET_ALL_ALL_CODE("SWMSTS", "PROCESSSTS", "DEPARTMENT", "MSG_SRC", "MSGDIRECTION", "SWIFT", "", "");
                _dsMaster = _dsAll_code.Copy();
                Load_Combobox();
                Load_Combobox_Enable();//cac combobox mac dinh khi hien len se bi enable
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Load_Combobox_Enable()
        {
            try
            {                
                ALLCODEController objAllcode = new ALLCODEController();
                STATUSController objStatus = new STATUSController();                
                DataTable _dtProcess = new DataTable();
                DataTable _dtStatus = new DataTable();
                DataTable _dtSwmsts = new DataTable();
                #region Addcombobox
                // Combobox STATUS----------------------- 
                _dtStatus = objStatus.SWIFT_STATUS("Where STATUS in (" + Common.STATUS_SENT + "," + Common.STATUS_CONVERTED + ")", out _dtStatus);   
                //---------------------------------------------
                DataRow _Rstatus1 = _dtStatus.NewRow();
                _Rstatus1[0] = 99; _Rstatus1[1] = "ALL";
                _dtStatus.Rows.InsertAt(_Rstatus1, 0);
                //-------------------------------------------------
                cbStatus.DataSource = _dtStatus;
                cbStatus.DisplayMember = "NAME";
                cbStatus.ValueMember = "STATUS";                
                if (isSupervisor == 1)
                {
                    cbStatus.SelectedValue = Common.STATUS_CONVERTED;
                }
                else
                {
                    cbStatus.SelectedIndex = 0;
                }
                // Combobox SWMSTS----------------------- 
                _dtSwmsts = objAllcode.SWMSTS_STATUS("Where CDVAL in (" + Common.SWMSTS_NORMAL + "," + Common.SWMSTS_POSS_DUP + ")", out _dtSwmsts);   
                //---------------------------------------------
                DataRow _RsWM1 = _dtSwmsts.NewRow();
                _RsWM1[0] = ""; _RsWM1[1] = "ALL";
                _dtSwmsts.Rows.InsertAt(_RsWM1, 0);               
                //-------------------------------------------------
                cbMsg_status.DataSource = _dtSwmsts;
                cbMsg_status.DisplayMember = "CONTENT";
                cbMsg_status.ValueMember = "CDVAL";
                cbMsg_status.SelectedIndex = 0;
                #region Xu ly cho cac quyen
                if (isSupervisor == 1)
                {
                    _dtProcess = objAllcode.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_NACK + "," + Common.PROCESSSTS_OLD_ACKWAIT + "," + Common.PROCESSSTS_FAILED + "," + Common.PROCESSSTS_CLOSED + "," + Common.PROCESSSTS_WAITING + "," + Common.PROCESSSTS_WAITING + "," + Common.PROCESSSTS_REPAIR + ")", out _dtProcess);
                    DataRow _RsPRO1 = _dtProcess.NewRow();
                    _RsPRO1[0] = ""; _RsPRO1[1] = "ALL";
                    _dtProcess.Rows.InsertAt(_RsPRO1, 0);
                    DataRow _RsPRO2 = _dtProcess.NewRow();
                    _RsPRO2[0] = ""; _RsPRO2[1] = " ";
                    _dtProcess.Rows.InsertAt(_RsPRO2, 1);
                    cbProcess_Status.DataSource = _dtProcess;
                    cbProcess_Status.DisplayMember = "CONTENT";
                    cbProcess_Status.ValueMember = "CDVAL";
                    cbProcess_Status.SelectedIndex = 0;
                }
                else if (isSupervisor==2)
                {
                    _dtProcess = objAllcode.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_WAPPROVING + ")", out _dtProcess);                    
                    cbProcess_Status.DataSource = _dtProcess;
                    cbProcess_Status.DisplayMember = "CONTENT";
                    cbProcess_Status.ValueMember = "CDVAL";
                    cbProcess_Status.SelectedIndex = 0;
                    cbProcess_Status.Enabled = false;
                }
                else if (isSupervisor == 3)
                {
                    _dtProcess = objAllcode.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_NACK + "," + Common.PROCESSSTS_OLD_ACKWAIT + "," + Common.PROCESSSTS_FAILED + "," + Common.PROCESSSTS_CLOSED + "," + Common.PROCESSSTS_WAITING + "," + Common.PROCESSSTS_WAITING + "," + Common.PROCESSSTS_REPAIR + "," + Common.PROCESSSTS_WAPPROVING + ")", out _dtProcess);
                    DataRow _RsPRO1 = _dtProcess.NewRow();
                    _RsPRO1[0] = ""; _RsPRO1[1] = "ALL";
                    _dtProcess.Rows.InsertAt(_RsPRO1, 0);
                    DataRow _RsPRO2 = _dtProcess.NewRow();
                    _RsPRO2[0] = ""; _RsPRO2[1] = " ";
                    _dtProcess.Rows.InsertAt(_RsPRO2, 1);
                    cbProcess_Status.DataSource = _dtProcess;
                    cbProcess_Status.DisplayMember = "CONTENT";
                    cbProcess_Status.ValueMember = "CDVAL";
                    cbProcess_Status.SelectedValue = Convert.ToInt32(Common.PROCESSSTS_WAPPROVING);
                }                
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        // ham lay len cac combobox
        private void Load_Combobox()
        {
            try
            {
                //// Combobox SWMSTS-----------------------                 
                DataRow _dtr = _dsAll_code.Tables["SWMSTS"].NewRow();
                _dtr[0] = ""; _dtr[1] = "ALL";
                _dsAll_code.Tables["SWMSTS"].Rows.InsertAt(_dtr, 0);
                cbMsg_status.DataSource = _dsAll_code.Tables["SWMSTS"];
                cbMsg_status.DisplayMember = "CONTENT";
                cbMsg_status.ValueMember = "CDVAL";
                cbMsg_status.Text = "ALL";
                //// Combobox STATUS----------------------------                
                DataRow _dtr3 = _dsAll_code.Tables["STATUS"].NewRow();
                _dtr3[0] = 99; _dtr3[1] = "ALL";
                _dsAll_code.Tables["STATUS"].Rows.InsertAt(_dtr3, 0);
                cbStatus.DataSource = _dsAll_code.Tables["STATUS"];
                cbStatus.DisplayMember = "NAME";
                cbStatus.ValueMember = "STATUS";
                cbStatus.Text = "ALL";                
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
                if (isSupervisor == 1 )/*Chi co quyen teller*/
                {
                    Where_Master = " ((trim(Auto)='N'  and STATUS = " + Common.STATUS_CONVERTED + " and trim(SWMSTS)='" + Common.SWMSTS_POSS_DUP + "') ";
                    Where_Master = Where_Master + " or (trim(Auto) = 'N' and STATUS = 0 and trim(SWMSTS) = '1' ) ";
                    Where_Master = Where_Master  + " or ( Status = " + Common.STATUS_SENT + " and  Trim(PROCESSSTS) ";
                    Where_Master = Where_Master + " in ( '" + Common.PROCESSSTS_OLD_ACKWAIT + "','" + Common.PROCESSSTS_NACK + "','" + Common.PROCESSSTS_FAILED + "')) ";
                    Where_Master = Where_Master + " or ( Trim(PROCESSSTS) = '" + Common.PROCESSSTS_REPAIR + "')) ";
                    Where_Master = Where_Master + " and QUERY_ID not in (Select QUERY_ID from SWIFT_PROCESS ) and ((PROCESSSTS is null) or PROCESSSTS <> " + Common.PROCESSSTS_CLOSED + ") and MSG_DIRECTION = 'SIBS-SWIFT'";
                    
                }
                if (isSupervisor == 2)//Suppervisor
                {
                    Where_Master = " QUERY_ID in (select QUERY_ID from SWIFT_PROCESS) and MSG_DIRECTION = 'SIBS-SWIFT' ";
                   
                }
                if (isSupervisor == 3)
                {
                    Where_Master = " ((trim(Auto)='N'  and STATUS = " + Common.STATUS_CONVERTED + " and trim(SWMSTS)='" + Common.SWMSTS_POSS_DUP + "') ";
                    Where_Master = Where_Master + " or (trim(Auto) = 'N' and STATUS = 0 and trim(SWMSTS) = '1' ) ";
                    Where_Master = Where_Master + " or ( Status = " + Common.STATUS_SENT + " and  Trim(PROCESSSTS) ";
                    Where_Master = Where_Master + " in ( '" + Common.PROCESSSTS_OLD_ACKWAIT + "','" + Common.PROCESSSTS_NACK + "','" + Common.PROCESSSTS_FAILED + "')) ";
                    Where_Master = Where_Master + " or ( Trim(PROCESSSTS) = '" + Common.PROCESSSTS_REPAIR + "')) ";
                    Where_Master = Where_Master + " and ((PROCESSSTS is null) or PROCESSSTS <> " + Common.PROCESSSTS_CLOSED + ") and MSG_DIRECTION = 'SIBS-SWIFT'";
                }
                
                //dataMessage = clsDatagridviews.MESSAGE_CONTENT("Where " + Where_Master, dataMessage);//Lay toen bo dien trong bang content======                
                Search_data();
                label12.Text = "Total number of messages :" + Convert.ToString(dataMessage.Rows.Count);
                if (dataMessage.Rows.Count == 0)
                {   cmdExport.Enabled = false; cmdProcess.Enabled = false;                    
                    cmdview.Enabled = false; }
                else
                {   cmdExport.Enabled = true; cmdProcess.Enabled = true;                   
                    cmdview.Enabled = true;  }
                //lay du lieu tu datagrid view ra datatable-----------de in dien---------------------------------------------
                _dtViews = (DataTable)dataMessage.DataSource;
                //-----------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void CheckIsSupervisor()
        {
            //bool result = a;
            string UserID = Common.Userid;
            DataSet dsGroup = new DataSet();
            dsGroup = objControlGroup.GetGroup_IsSupervisor(UserID,Common.gGWTYPE);
            if (dsGroup.Tables[0].Rows.Count == 0)
            {
                return ;
            }
            else if (dsGroup.Tables[0].Rows.Count == 1)
            {
                int IsSupervisor = Convert.ToInt32(dsGroup.Tables[0].Rows[0][0].ToString());
                if (IsSupervisor == 1)
                {
                    isSupervisor = 1;                   
                }
                else if (IsSupervisor == 2)
                {
                    isSupervisor = 2;                   
                }
            }
            else if (dsGroup.Tables[0].Rows.Count == 2)
            {
                isSupervisor = 3;
            }           
        }      

        private void Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        {
            objuser_msg_log.LOG_DATE = Logdate;
            objuser_msg_log.USERID = strUsername;
            objuser_msg_log.CONTENT = strContent;
            objuser_msg_log.STATUS = Log_level;
            objuser_msg_log.WORKED = strWorked;
            objuser_msg_log.TABLE_ACCESS = strTale_Access;
            objuser_msg_log.OLD_VALUE = strOld_value;
            objuser_msg_log.NEW_VALUE = strNew_value;

            objcontroluser_msg_log.AddUSER_MSG_LOG(objuser_msg_log);
        }

        private void cboName_SelectedIndexChanged(object sender, EventArgs e)
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
                            if (pCl.ToUpper() == "STATUS") { cboStatus.SelectedValue = Common.STATUS_SENT; cboStatus.Enabled = false; }
                            else { cboStatus.Enabled = true; }
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

                #region quynd comment 20100319
                //cbCheck.Text = cbColumns.Text;
                //if (cbColumns.SelectedValue.ToString() == "TRANSDATE" || cbColumns.SelectedValue.ToString() == "VALUE_DATE" || cbColumns.SelectedValue.ToString() == "RECEIVING_TIME" || cbColumns.SelectedValue.ToString() == "SENDING_TIME")
                //{
                //    //iVisible = 1;  
                //    txtValue.Visible = false; cboStatus.Visible = false; dateValue.Visible = true;
                //    this.dateValue.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "STATUS")
                //{                    
                //    //iVisible = 2;  
                //    txtValue.Visible = false;  cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["STATUS"];
                //    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "STATUS";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false;cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["DEPARTMENT"];
                //    cboStatus.DisplayMember = "CONTENT";cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false;cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["CCYCD"];
                //    cboStatus.DisplayMember = "CCYCD";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_SRC")//-	Msg source
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSG_SRC"];
                //    cboStatus.DisplayMember = "CONTENT";
                //    cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);//PROCESSSTS
                //}
                //else if (cbColumns.SelectedValue.ToString() == "SWMSTS")//--	RM no.
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false;cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["SWMSTS"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "PROCESSSTS")//--	RM no.
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["PROCESSSTS"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")//--	RM no.
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false;cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSG_DIRECTION"];
                //    cboStatus.DisplayMember = "CONTENT"; 
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
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
                //-----------------------------------------------------------------    
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
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

        private void cmdRemoveAll_Click(object sender, EventArgs e)
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
                        frmSWIFT._Processsts = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString(); 
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

        private void cmdNornal_Click(object sender, EventArgs e)
        {
            iSearch = 0;
            try
            {                
                cmdAdvance.Show();
                grbSearchnhanh.Hide();
                grbDieukien.Hide();                
                //this.grbSearch.Location = new Point(10, grbSearch.Location.X);
                //this.grbSearch.Location = new Point(13, grbSearch.Location.Y);
                grbSearch.Show();
                cmdview.Enabled = false;
                datefrom.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAdvance_Click(object sender, EventArgs e)
        {
            iSearch = 1;
            try
            {                
                this.dateValue.MaxDate = DateTime.Now;                
                cmdAdvance.Hide();
                grbSearch.Hide();
                grbSearchnhanh.Show();
                grbDieukien.Show();
                #region Quynd comment 20100319
                //_dtSearch = objsearchcontrol.COLUMNS_SEARCH("SWIFT", out _dtSearch);
                //cbCheck.DataSource = _dtSearch;
                //cbCheck.DisplayMember = "FIELDNAME";
                //cbCheck.ValueMember = "OPERATOR";
                //cbColumns.DataSource = _dtSearch;
                //cbColumns.ValueMember = "FIELDCODE";
                //cbColumns.DisplayMember = "FIELDNAME";
                //cmdview.Enabled = false;
                #endregion

                #region Quyndcap nhat 20100319
                _dtSearchAV = objsearchcontrol.dtSearch("SWIFT");
                if (_dtSearchAV != null)
                {
                    cbColumns.DataSource = _dtSearchAV;
                    cbColumns.ValueMember = "FIELDCODE";
                    cbColumns.DisplayMember = "FIELDNAME";
                }
                #endregion

                dateto.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }       
      
       private void cbCurrency_KeyPress(object sender, KeyPressEventArgs e)
       {
           base.OnKeyPress(e);
           if (e.KeyChar == (int)Keys.Escape)
           {
               cbCurrency.SelectedIndex = -1;
               cbCurrency.Text = "";
               controlKey = true;
           }
           else

               if (Char.IsControl(e.KeyChar))
               { controlKey = true; }
               else
               { controlKey = false; }
       }
        

       private void cbCurrency_TextChanged(object sender, EventArgs e)
       {
           base.OnTextChanged(e);
           if (cbCurrency.Text != "" && !controlKey)
           {
              
               string matchText = cbCurrency.Text;
               int match = cbCurrency.FindString(matchText);
               // nếu tìm thấy thì chèn nó vào
               if (match != -1)
               {
                   cbCurrency.SelectedIndex = match;                   
                   //cbCurrency.SelectionStart = matchText.Length;
                   cbCurrency.SelectionLength = cbCurrency.Text.Length - cbCurrency.SelectionStart;
               }
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
               if (e.RowIndex != -1) { iRows = e.RowIndex; }
           }
           catch (Exception ex)
           {
               Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
           }
       }

       private void frmSwiftMsgManual_KeyDown(object sender, KeyEventArgs e)
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

       private void txtAmount_Leave(object sender, EventArgs e)
       {
           string strAmount = txtAmount.Text.Trim();
           if (strAmount == "")
           {

           }
           else
           {
               if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
               {
                   txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
               }
               else
               {
               }                
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
               if (datefrom.Checked == true)
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

      
       private void frmSwiftMsgManual_MouseMove(object sender, MouseEventArgs e)
       {
           Common.bTimer = 1;
       }

       private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
       {
           try
           {
               //if (cbStatus.SelectedValue.ToString().Trim() == Common.STATUS_CONVERTED)
               //{
               //    cbMsg_status.SelectedValue = Common.SWMSTS_POSS_DUP;                                      
               //}
               //else if (cbStatus.SelectedValue.ToString().Trim() == Common.STATUS_SENT)
               //{
               //    cbMsg_status.SelectedValue = Common.SWMSTS_NORMAL;
               //}
               //#region Combobox PROCESSSTS-----------------------
               //ALLCODEController objAllcode = new ALLCODEController();
               //DataTable _dt = new DataTable();
               //_dt = objAllcode.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_NACK + "," + Common.PROCESSSTS_ACKWAIT + "," + Common.PROCESSSTS_FAILED + ")", out _dt); 
               //DataRow _RsPRO1 = _dt.NewRow();
               //_RsPRO1[0] = ""; _RsPRO1[1] = "ALL";
               //_dt.Rows.InsertAt(_RsPRO1, 0);

               //DataRow _RsPRO2 = _dt.NewRow();
               //_RsPRO2[0] = ""; _RsPRO2[1] = " ";
               //_dt.Rows.InsertAt(_RsPRO2, 1);

               //cbProcess_Status.DataSource = _dt;
               //cbProcess_Status.DisplayMember = "CONTENT";
               //cbProcess_Status.ValueMember = "CDVAL";
               //cbProcess_Status.SelectedIndex = 0;
               //#endregion
           }
           catch (Exception ex)
           {
               Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
           }
       }

       private void frmSwiftMsgManual_MouseDown(object sender, MouseEventArgs e)
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

       private void cmdExport_Click(object sender, EventArgs e)
       {
           try
           {
               Check_Select_Rows();
               if (iSelect == 0)
               {
                   MessageBox.Show("There is no message select!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                       datExcel = objControlContent.dtExcel(M[j], out datExcel);
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
               MessageBox.Show(ex.Message);
           }
       }

       private void cbMsg_status_SelectedIndexChanged(object sender, EventArgs e)
       {

       }      

    }
}
