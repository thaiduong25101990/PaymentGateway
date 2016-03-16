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
using BR.BRSYSTEM;
using System.Text.RegularExpressions;

namespace BR.BRIBPS
{
    public partial class frmIBPSMsgFw : frmBasedata
    {

        private DataSet _dsAll_code;

        IBPS_MSG_LOGController objctLog = new IBPS_MSG_LOGController();
        IBPS_MSG_LOGInfo objLogInfo = new IBPS_MSG_LOGInfo();

        IBPS_MSG_CONTENTInfo objContent = new IBPS_MSG_CONTENTInfo();
        IBPS_MSG_CONTENTController objControlContent = new IBPS_MSG_CONTENTController();

        IBPS_BANK_MAPInfo objBank_map = new IBPS_BANK_MAPInfo();
        IBPS_BANK_MAPController objctrMap = new IBPS_BANK_MAPController();

        TADInfo objTAD = new TADInfo();
        TADController objControlTAD = new TADController();

        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();

        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader1 = new BR.BRLib.Common.DGVColumnHeader();
        
        private string Cells_columns;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        private int iRowscount;
        private int iError;
        private int iSelect;
        private string strBIEN;
        string strAmount; 
        string strResender; 
        string strTrans;
        string strTranDate;
        private string strTAD_OLD;
        private int iRows;
        private int iRows1;
        public string CurrentTad;
        public string CurrentValue;
        //----------------------------------
        private int existed;
        private string strMessage = "";
        //-------------------------------------
        //DGVColumnHeader dgvColumnHeader;
        //int iID = 0;
        public DataTable dsPrint;
        public frmIBPSMsgFw()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        //ham kiem tra xem tren dataGrid chon qua mot dong hay khong
        private void Checked_Els()
        {
            try
            {
                int k = 0;
                while (k < dgView.Rows.Count)
                {
                    if (dgView.Rows[k].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dgView.Rows[k].Cells[0].Value.ToString() == "True")
                        {
                            iRowscount = iRowscount + 1;//so dem de neu lon hon mot thi hien thi thong bao kieu form
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
        //ham goi thuc hien forword dien
        private void cmdForward_Click(object sender, EventArgs e)
        {
            iRowscount = 0;
            try
            {
                if (string.IsNullOrEmpty(cboTAD.Text) & string.IsNullOrEmpty(cboNewTAD.Text))
                {
                    Common.ShowError("You must fill data!", 3, MessageBoxButtons.OK);                    
                }
                else
                {

                    if (dgView.Rows.Count != 0)
                    {
                        int d = 0;
                        while (d < dgView.Rows.Count)
                        {
                            objContent.QUERY_ID = Convert.ToInt32(dgView.Rows[d].Cells["QUERY_ID"].Value.ToString());
                            objContent.MSG_ID = Convert.ToInt32(dgView.Rows[d].Cells["msg_id"].Value.ToString());
                            objContent.PRE_TAD = "00" + dgView.Rows[d].Cells["TAD"].Value.ToString();
                            //-------------------------------------------------------------
                            //DataTable datTT = new DataTable();
                            //datTT = objControlTAD.GetTAD_SIBS_BANK_CODE("TAD" + cboNewTAD.Text.Trim());
                            //string strTAD = datTT.Rows[0]["SIBS_BANK_CODE"].ToString();
                            String[] N = cboNewTAD.SelectedValue.ToString().Split(new String[] { "#" }, StringSplitOptions.None);//cat chuoi
                            string m_vSIBS_BANK_CODE = N[0].ToString();
                            string m_vGW_BANK_CODE = N[1].ToString();

                            //--------------------------------------------------------------
                            objContent.TAD = m_vSIBS_BANK_CODE; //cboNewTAD.Text.Trim();
                            
                            //--------------------------kiem tra co chon gia tri cao hay thap-----------
                            if (HV_LV.Text.Trim() == "HV" || HV_LV.Text.Trim() == "LV")
                            {
                                if (HV_LV.Text.Trim() == "HV")
                                { objContent.HV_LV = 0; }//truyen gia tri khi chon HV
                                if (HV_LV.Text.Trim() == "LV")
                                { objContent.HV_LV = 1; }//truyen gia tri khi chon LV                                           
                            }
                            else
                            {
                            }
                            objContent.TELLERID = Common.Userid;
                            //--------------------------------------------------------------------------
                            if (objControlContent.AddIBPS_Q_Dblink_Out(objContent, m_vGW_BANK_CODE) == 1)
                            {                                
                                objLogInfo.LOG_DATE = DateTime.Now;
                                objLogInfo.QUERY_ID = Convert.ToInt32(dgView.Rows[d].Cells["QUERY_ID"].Value.ToString());
                                objLogInfo.STATUS = 1;
                                objLogInfo.DESCRIPTIONS = Common.Userid + " forward message ";
                                objctLog.ADD_MAG_LOG(objLogInfo);
                                dgView.Rows.RemoveAt(d);
                            }
                            else
                            {                                
                                string strSENDER = dgView.Rows[d].Cells["SENDER"].Value.ToString();
                                string strRECEIVER = dgView.Rows[d].Cells["RECEIVER"].Value.ToString();
                                string strAMOUNT = dgView.Rows[d].Cells["AMOUNT"].Value.ToString();
                                string strCCYCD = dgView.Rows[d].Cells["CCYCD"].Value.ToString();
                                string strRM_NUMBER = dgView.Rows[d].Cells["RM_NUMBER"].Value.ToString();
                                Cells_columns = Cells_columns + "\r\n" + "------------------------------------" + "\r\n" + "RM_NUMBER :" + strRM_NUMBER + "  SENDER :" + strSENDER + "  RECEIVER :" + strRECEIVER + "  AMOUNT :" + strAMOUNT + "  CCYCD :" + strCCYCD + "  Error";
                                iError = 1;
                                d = d + 1;
                            }                           
                        }
                        //----kiem tra xem co su lieu nao noi khong-------------
                        if (iError == 0)//khong co du lieu nao loi thi thong bao
                        {
                            MessageBox.Show("Message has just forwarded to other TAD successfull!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else//co it nhat mot loi
                        {
                            string Msg = "Do you want view message forward error ?";
                            string title = Common.sCaption;
                            DialogResult DlgResult = new DialogResult();
                            DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (DlgResult == DialogResult.Yes)
                            {
                                frmMessageError frmerror = new frmMessageError();
                                frmerror.strCells_columns = Cells_columns;
                                frmerror.ShowDialog();
                            }
                            else
                            {

                            }
                        }
                    }                    
                    //lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Forward TAD";
                    int Log_level = 1;
                    string strWorked = "";
                    string strTable = "IBPS_BANK_MAP";
                    string strOld_value = "";
                    string strNew_value = objContent.QUERY_ID + "/" + objContent.PRE_TAD + "/" + objContent.TAD;
                    //-----------------------------------------
                    Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                    //--------------------------------------------------


                    //cmdSave.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            if(dgView.Rows.Count==0)
            {
                cmdRemove_Message.Enabled = false;
                cmdRemoveall_Message.Enabled = false;
            }
            label12.Text = "Total number of messages :" + Convert.ToString(dgView.Rows.Count);
            BR.BRLib.FomatGrid.Color_datagrid(dgView);
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

        private void Locate_controls()
        {
            try
            {
                cbAmount.SelectedIndex = 0;
                HV_LV.Visible = false;
                label3.Visible = false;
                dateTimePicker1.Enabled = false;
                dgView.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dgView.Columns[0].HeaderCell = dgvColumnHeader;
                dgView.Columns[0].Width = 26;
                dataSearch.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataSearch.Columns[0].HeaderCell = dgvColumnHeader1;
                dataSearch.Columns[0].Width = 26;                
                cmdAdd.Visible = false; cmdDelete.Visible = false;
                cmdSave.Visible = false; cmdEdit.Visible = false;                
                cmdAddall_Message.Enabled = false; cmdAdd_Message.Enabled = false;
                cmdRemove_Message.Enabled = false; cmdRemoveall_Message.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        private void frmIBPSMsgFw_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                Locate_controls();
                cmdForward.Enabled = false;
                Status_tad();
                comModeTranDate.Text = comModeTranDate.Items[0].ToString();
                if (cboTAD.Text.Trim() != "")
                {
                    Search_Forward();
                }
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            #region TransDate
            if (comModeTranDate.Text.ToUpper() == "ALL")
            {
                TransTime.Enabled = false;
            }
            else
            {
                TransTime.Enabled = true;
            }
            #endregion
        }


        private void Status_tad()
        {
            try
            {
                //lay cong tad va status
                _dsAll_code = clsSTATUS.STATUS_TAD(Common.Userid);
                Add_combobox();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Add_combobox()
        {
            try
            {
                HV_LV.Items.Add("HV"); HV_LV.Items.Add("LV");
                HV_LV.SelectedIndex = 0;
                // Combobox TAD_CLOSE----------------------------                
                DataRow _dtr = _dsAll_code.Tables["TAD_CLOSE"].NewRow();
                //_dtr[0] = " "; _dtr[1] = "";
                //_dsAll_code.Tables["TAD_CLOSE"].Rows.InsertAt(_dtr, 0);
                cboTAD.DataSource = _dsAll_code.Tables["TAD_CLOSE"];
                cboTAD.DisplayMember = "TAD";
                cboTAD.ValueMember = "SIBS_BANK_CODE";
                if (_dsAll_code.Tables["TAD_CLOSE"].Rows.Count > 0)
                {
                    cboTAD.SelectedIndex = 0;
                }
                //Combobox TAD_ACTIVE----------------------------  
                DataRow _dtr1 = _dsAll_code.Tables["TAD_ACTIVE"].NewRow();
                _dtr1[0] = " "; _dtr1[1] = "";
                _dsAll_code.Tables["TAD_ACTIVE"].Rows.InsertAt(_dtr1, 0);
                cboNewTAD.DataSource = _dsAll_code.Tables["TAD_ACTIVE"];
                cboNewTAD.DisplayMember = "TAD";
                cboNewTAD.ValueMember = "SIBS_BANK_CODE";
                cboNewTAD.SelectedIndex = 0;               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void ColumnsRead(DataGridView Datagrid)
        {
            try
            {
                int b = 1;
                while (b < Datagrid.Columns.Count)
                {
                    Datagrid.Columns[b].ReadOnly = true;
                    b = b + 1;
                }
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
                        if (strColumns.Trim() == "DEPARTMENT")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "MODULE";
                        }
                    }                   
                    Datagrid.ColumnHeadersHeight = 21;                    
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Visible_Columns1(DataGridView dataGrid)
        {
            try
            {               
                dataGrid.Columns["TAD"].Visible = false;
                dataGrid.Columns["PRE_TAD"].Visible = false;
                dataGrid.Columns["MSG_ID"].Visible = false;
                dataGrid.Columns["Query_Id"].Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Visible_Columns(DataGridView dataGrid)
        {
            try
            {              
                dataGrid.Columns["TAD1"].Visible = false;
                dataGrid.Columns["PRE_TAD1"].Visible = false;
                dataGrid.Columns["MSG_ID1"].Visible = false;
                dataGrid.Columns["Query_Id1"].Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
       
        private void LoadData()
        {
            try
            {
                dataSearch = clsDatagridviews.FORWARD_LOAD(dataSearch,out dsPrint,Common.Userid);                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows1 = e.RowIndex; }
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dgView.RowCount; i++)
                        {
                            this.dgView.EndEdit();
                            string re_value = this.dgView.Rows[i].Cells[0].EditedFormattedValue.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dgView.Rows.Count; i++)
                {
                    dgView.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                }
            }
            BR.BRLib.FomatGrid.Color_datagrid(dgView);
        }

        private void cboTAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IBPS_MSG_CONTENTDP.LoadDataSearch("IBPS_MSG_CONTENT", dgView, cboTAD.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboTAD_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cboTAD.Text.Trim() == "" || Regex.IsMatch(cboTAD.Text.Trim(), @"^[0-9]*\z") == true)
                {
                    strBIEN = cboTAD.Text;
                    if (cboTAD.Text.Length <= 3)
                    { strBIEN = cboTAD.Text; }
                    else
                    { cboTAD.Text = strBIEN; }
                }
                else
                { cboTAD.Text = strBIEN; }
                if (cboTAD.Text == " ")
                {   cmdForward.Enabled = false; }
                else
                {
                    if (cboNewTAD.Text == " ")
                    { cmdForward.Enabled = false; }
                    else
                    { cmdForward.Enabled = true; }
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboNewTAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboNewTAD.Text.Trim() == "")
                {
                    cmdForward.Enabled = false;
                }
                else
                {
                    if (dgView.Rows.Count == 0)
                    {
                        cmdForward.Enabled = false;
                    }
                    else
                    { cmdForward.Enabled = true; }
                }                          
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

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
                if (cboTAD.Focused)
                {
                    cboNewTAD.Focus();
                    cboNewTAD.SelectAll();
                }
                else if (cboNewTAD.Focused)
                {
                    HV_LV.Focus();
                    HV_LV.SelectAll();
                }
                else if (HV_LV.Focused)
                {
                    cmdForward.Focus();
                    cmdForward_Click(null, null);
                }

                //strSucess = true;
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            CurrentTad = cboNewTAD.Text.Trim();
            CurrentValue = HV_LV.Text.Trim(); 
            DataTable datPrint = new DataTable();
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
                Check_Select_Rows(dgView);
                if (iSelect == 0)//khong click chon otext
                {
                    //frmPrint.HMdat = dsPrint;
                    int f = 0;
                    while (f < dgView.Rows.Count)
                    {                        

                            string strMSG_ID = dgView.Rows[f].Cells["MSG_ID"].Value.ToString().Trim();
                            //int k = 0;

                            datRow = dsPrint.Select("MSG_ID=" + Convert.ToInt32(strMSG_ID));
                            datRow1 = datPrint.NewRow();
                            for (int j = 0; j < dsPrint.Columns.Count; j++)
                            {
                                datRow1[j] = datRow[0][j];
                            }
                            datPrint.Rows.Add(datRow1);
                        
                        f = f + 1;
                    }
                    frmPrint.HMdat = datPrint;
                }
                else
                {   
                    int f = 0;
                    while (f < dgView.Rows.Count)
                    {
                        if (dgView.Rows[f].Cells[0].Value != null && dgView.Rows[f].Cells[0].Value.ToString() == "True")// hang khong duoc chon
                        {

                            string strMSG_ID = dgView.Rows[f].Cells["MSG_ID"].Value.ToString().Trim();                           
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
                string Print = "TAD_PRINT";
                frmPrint.PrintType = Print;
                frmPrint.CurrentTad = CurrentTad;
                frmPrint.CurrentValue = CurrentValue; 
                frmPrint.WindowState = FormWindowState.Maximized;
                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }      
        }

        //ham kiem tra xem co du lieu nao check khong
        private void Check_Select_Rows(DataGridView _dtg)
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < _dtg.Rows.Count)
                {
                    if (_dtg.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtg.Rows[b].Cells[0].Value.ToString() == "True")
                        { iSelect = 1; break; }
                    }
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIBPSMsgFw_KeyDown(object sender, KeyEventArgs e)
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

        //click vao nut search du lieu
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTAD.Text.Trim() != "")
                {
                    string strAmount1 = txtAmount.Text.Trim();
                    if (strAmount1 != "")
                    {
                        if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            txtAmount.Text = Common.FormatCurrency(strAmount1.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                        }
                    }
                    Search_Forward();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }  
     
        // ham search du lieu
        private void Search_Forward()
        {
            try
            {
                strTranDate = "";
                #region Lay tao ra menh de where
                if (cboTAD.Text.Trim() == "")
                { strTAD_OLD = ""; }
                else
                {
                    strTAD_OLD = "  and  upper(Trim(Tad)) = '" + cboTAD.SelectedValue + "'";                    
                }
                if (txtGW1.Text.Trim() == "" && txtGW2.Text.Trim() == "")
                { strTrans = ""; }
                else if (txtGW1.Text.Trim() != "" && txtGW2.Text.Trim() == "")
                {
                    strTrans = " and  upper(Trim(GW_TRANS_NUM)) = " + txtGW1.Text.Trim().ToUpper() + "";
                }
                else if (txtGW1.Text.Trim() == "" && txtGW2.Text.Trim() != "")
                {
                    strTrans = " and  upper(Trim(GW_TRANS_NUM)) = " + txtGW2.Text.Trim().ToUpper() + "";
                }
                else if (txtGW1.Text.Trim() != "" && txtGW2.Text.Trim() != "")
                {
                    strTrans = "  and  " + txtGW1.Text.Trim().ToUpper() + " <=   upper(Trim(GW_TRANS_NUM)) and  upper(Trim(GW_TRANS_NUM))  <= " + txtGW2.Text.Trim().ToUpper() + "";
                }                
                if (cbAmount.Text.Trim() == "ALL")
                {  strAmount = ""; }
                else
                {
                    if (txtAmount.Text.Trim() == "") { strAmount = ""; }
                    else
                    {
                        String[] M = txtAmount.Text.Trim().Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);
                        string strAM = M[1];
                        if (M[1].Trim() == "00")
                        {
                            strAmount = " and  upper(Trim(AMOUNT))  " + cbAmount.Text.Trim() + M[0].Trim() + "." + M[1] + "";
                        }
                        else
                        {
                            strAmount = " and  upper(Trim(AMOUNT))  " + cbAmount.Text.Trim() + txtAmount.Text.Trim().Replace(",", "") + "";
                        }
                    }
                }                
                if (txtReSender.Text.Trim() == "") { strResender = ""; } else { strResender = " and   upper(Trim(F22)) like '%" + txtReSender.Text.Trim().ToUpper() + "%'"; }
                //---------------------------------------------------------------------------------------------                
                if (txtAmount.Text.Trim() == "") { strAmount = ""; }
                else
                {
                    String[] M = txtAmount.Text.Trim().Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);
                    string strAM = M[1];
                    if (M[1].Trim() == "00")
                    {
                        strAmount = " and  upper(Trim(AMOUNT)) >= " + M[0].Trim() + "." + M[1] + "";
                    }
                    else
                    {
                        strAmount = " and  upper(Trim(AMOUNT)) >= " + txtAmount.Text.Trim().Replace(",", "") + "";
                    }
                }
                if (comModeTranDate.Text != "ALL")
                {
                    string pDatetimeNow = TransTime.Value.TimeOfDay.ToString();
                    String[] N = pDatetimeNow.Split(new String[] { ":" }, StringSplitOptions.None);
                    string pTimeSearch = N[0] + N[1];
                    strTranDate = " and To_char(TRANS_DATE,'HH24MM') " + comModeTranDate.Text + " LPAD('" + pTimeSearch + "',4,'0') ";
                }               
                #endregion
                string strWhere = strResender + strAmount + strTrans + strTAD_OLD + strTranDate;
                dataSearch = clsDatagridviews.FORWARD_SEARCH(dataSearch, strWhere, out dsPrint,Common.Userid);
                dataSearch = clsForward.Remove_rows_exits(dataSearch, dgView);
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }    
        }
        private void Remove_Seach()
        {
            try
            {
                int g = 0;
                while (g < dgView.Rows.Count)//
                {
                    int h = 0;
                    while (h < dataSearch.Rows.Count)
                    {
                        if (dgView.Rows[g].Cells["MSG_ID"].Value.ToString() == dataSearch.Rows[h].Cells["MSG_ID1"].Value.ToString())
                        {
                            dataSearch.Rows.RemoveAt(h);
                        }
                        else
                        {
                            h = h + 1;
                        }
                    }
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAddall_Message_Click(object sender, EventArgs e)
        {            
            try
            {
                strMessage = "Msg_id:"; existed = 0; int i = 0;
                while (i < dataSearch.Rows.Count)
                {
                    existed = 0;
                    int x = 0;
                    while (x < dgView.Rows.Count)
                    {
                        if (dataSearch.Rows[i].Cells["MSG_ID1"].Value.ToString() == dgView.Rows[x].Cells["MSG_ID"].Value.ToString())
                        {
                            strMessage = strMessage + "; ;" + dataSearch.Rows[i].Cells["MSG_ID1"].Value.ToString();
                            existed = 1;
                        }
                        x = x + 1;
                    }
                    if (existed == 1)
                    { i = i + 1; }
                    else
                    {
                        dgView = clsForward.Add_forward(i, dataSearch, dgView);
                        dataSearch.Rows.RemoveAt(i);
                    }                    
                }
                if (strMessage.Trim() != "Msg_id:")
                {
                    Common.ShowError(strMessage + "\r\n" + "has already exist!", 2, MessageBoxButtons.OK);                    
                }
                clsDatagridviews.Color_datagrid(dataSearch);
                clsDatagridviews.Color_datagrid1(dgView);
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }      
        }        

        private void dataSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dataSearch.RowCount; i++)
                        {
                            this.dataSearch.EndEdit();
                            string re_value = this.dataSearch.Rows[i].Cells[0].EditedFormattedValue.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }

        private void dataSearch_CellEnter(object sender, DataGridViewCellEventArgs e)
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

        private void cmdAdd_Message_Click(object sender, EventArgs e)
        {            
            try
            {
                Check_Select_Rows(dataSearch);
                #region co click chon
                if (iSelect == 1)
                {
                    strMessage = "Msg_id"; int z = 0;                    
                    while (z < dataSearch.Rows.Count)
                    {
                        if (dataSearch.Rows[z].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dataSearch.Rows[z].Cells[0].Value.ToString() == "True")
                            {
                                existed = 0; int x = 0;
                                while (x < dgView.Rows.Count)
                                {
                                    if (dataSearch.Rows[z].Cells["MSG_ID1"].Value.ToString() == dgView.Rows[x].Cells["MSG_ID"].Value.ToString())
                                    {
                                        strMessage = strMessage + "\r\n" + dataSearch.Rows[z].Cells["MSG_ID1"].Value.ToString();
                                        existed = 1;
                                    }
                                    x = x + 1;
                                }
                                if (existed == 1) { z = z + 1; }
                                else
                                {
                                    dgView = clsForward.Add_forward(z, dataSearch, dgView);
                                    //Add_forward(z); 
                                    dataSearch.Rows.RemoveAt(z); }
                            }
                            else { z = z + 1; }
                        }
                        else { z = z + 1; }
                    }
                    if (strMessage.Trim() != "Msg_id")
                    {
                        Common.ShowError(strMessage + "\r\n" + "has already exist!", 2, MessageBoxButtons.OK);
                    }
                }
                #endregion
                #region khong click chon
                else if (iSelect == 0)
                {
                    existed = 0;
                    strMessage = "Msg_id";int x = 0;
                    while (x < dgView.Rows.Count)
                    {
                        if (dataSearch.Rows[iRows].Cells["MSG_ID1"].Value.ToString() == dgView.Rows[x].Cells["MSG_ID"].Value.ToString())
                        {
                            strMessage = strMessage + "\r\n" + dataSearch.Rows[iRows].Cells["MSG_ID1"].Value.ToString();
                            existed = 1;
                        }
                        x = x + 1;
                    }                    
                    if (existed != 1)
                    {
                        dgView = clsForward.Add_forward(iRows, dataSearch, dgView);
                        //Add_forward(iRows); 
                        dataSearch.Rows.RemoveAt(iRows); }
                    if (strMessage.Trim() != "Msg_id")
                    {
                        Common.ShowError(strMessage + "\r\n" + "has already exist!", 2, MessageBoxButtons.OK);                        
                    }
                }
                #endregion
                clsDatagridviews.Color_datagrid(dataSearch);
                clsDatagridviews.Color_datagrid1(dgView);
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }           
        }

        private void Enable_controls()
        {
            try
            {
                if (dataSearch.Rows.Count == 0)
                { cmdAdd_Message.Enabled = false; cmdAddall_Message.Enabled = false; }
                else
                { cmdAdd_Message.Enabled = true; cmdAddall_Message.Enabled = true; }
                if (dgView.Rows.Count == 0)
                {
                    cmdRemove_Message.Enabled = false; cmdRemoveall_Message.Enabled = false;
                    cmdPrint.Enabled = false; cmdForward.Enabled = false;
                }
                else
                {
                    cmdRemove_Message.Enabled = true; cmdRemoveall_Message.Enabled = true;
                    cmdPrint.Enabled = true;
                    if (cboNewTAD.Text.Trim() == "")
                    { cmdForward.Enabled = false; }
                    else { cmdForward.Enabled = false; }
                }
                label4.Text = "Total number of messages :" + dataSearch.Rows.Count;
                label12.Text = "Total number of messages :" + dgView.Rows.Count;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }

        private void cmdRemove_Message_Click(object sender, EventArgs e)
        {
            try
            {
                existed = 0; int m = 0;
                while (m < dgView.Rows.Count)
                {
                    if (dgView.Rows[m].Cells[0].Value != null)
                    {
                        if (dgView.Rows[m].Cells[0].Value.ToString() == "True")
                        {
                            int x = 0;
                            while (x < dataSearch.Rows.Count)
                            {
                                if (dataSearch.Rows[x].Cells["MSG_ID1"].Value.ToString() == dgView.Rows[m].Cells["MSG_ID"].Value.ToString())
                                { existed = 1; }
                                x = x + 1;
                            }
                            if (existed == 1) { dgView.Rows.RemoveAt(m); }
                            else
                            {
                                dataSearch = clsForward.Remove_forward(m, dgView, dataSearch);                                
                                dgView.Rows.RemoveAt(m); }
                        }
                        else { m = m + 1; }
                    }
                    else { m = m + 1; }                    
                }
                clsDatagridviews.Color_datagrid(dataSearch);
                clsDatagridviews.Color_datagrid1(dgView);
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }            
        }     

        private void cmdRemoveall_Message_Click(object sender, EventArgs e)
        {
            try
            {
                existed = 0; int m = 0;
                while (m < dgView.Rows.Count)
                {                    
                    int x = 0;
                    while (x < dataSearch.Rows.Count)
                    {
                        if (dataSearch.Rows[x].Cells["MSG_ID1"].Value.ToString() == dgView.Rows[m].Cells["MSG_ID"].Value.ToString())
                        { existed = 1; }
                        x = x + 1;
                    }
                    if (existed == 1) { dgView.Rows.RemoveAt(m); }
                    else
                    {
                        dataSearch = clsForward.Remove_forward(m, dgView, dataSearch);                        
                        dgView.Rows.RemoveAt(m);
                    }
                }
                clsDatagridviews.Color_datagrid(dataSearch);
                clsDatagridviews.Color_datagrid1(dgView);
                Enable_controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }      
        }

        private void dataSearch_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < dataSearch.Rows.Count; i++)
                    {
                        dataSearch.Rows[i].Cells[0].Value = dgvColumnHeader1.CheckAll;
                    }
                }
                BR.BRLib.FomatGrid.Color_datagrid1(dataSearch);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }
        private void chan()
        {
            iRowscount = 0;
            try
            {
                if (string.IsNullOrEmpty(cboTAD.Text) & string.IsNullOrEmpty(cboNewTAD.Text))
                {
                    Common.ShowError("You must fill data!", 3, MessageBoxButtons.OK);                    
                }
                else
                {

                    if (dgView.Rows.Count != 0)
                    {
                        Checked_Els();//kiem tra so hang duoc chon
                        //goi ham xu ly xem la chon nhieu hang hay mot hang de hien thi cau thong bao
                        if (iRowscount == 1)
                        {
                            int d = 0;
                            while (d < dgView.Rows.Count)
                            {

                                if (dgView.Rows[d].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dgView.Rows[d].Cells[0].Value.ToString() == "True")
                                    {
                                        objContent.MSG_ID = Convert.ToInt32(dgView.Rows[d].Cells["msg_id"].Value.ToString());
                                        objContent.PRE_TAD = "00" + dgView.Rows[d].Cells["TAD"].Value.ToString();
                                        //-------------------------------------------------------------
                                        //DataTable datTT = new DataTable();
                                        //datTT = objControlTAD.GetTAD_SIBS_BANK_CODE("TAD" + cboNewTAD.Text.Trim());
                                        //string strTAD = datTT.Rows[0]["SIBS_BANK_CODE"].ToString();
                                        String[] N = cboNewTAD.SelectedValue.ToString().Split(new String[] { "#" }, StringSplitOptions.None);//cat chuoi
                                        string m_vSIBS_BANK_CODE = N[0].ToString();
                                        string m_vGW_BANK_CODE = N[1].ToString();
                                        //--------------------------------------------------------------
                                        objContent.TAD = m_vSIBS_BANK_CODE; //cboNewTAD.Text.Trim();
                                        if (HV_LV.Text.Trim() == "HV" || HV_LV.Text.Trim() == "LV")
                                        {
                                            if (HV_LV.Text.Trim() == "HV")
                                            { objContent.HV_LV = 0; }//truyen gia tri khi chon HV
                                            if (HV_LV.Text.Trim() == "LV")
                                            { objContent.HV_LV = 1; }//truyen gia tri khi chon LV                                           
                                        }
                                        else
                                        {

                                            objContent.HV_LV = DBNull.Value;
                                        }
                                        objContent.TELLERID = Common.Userid;
                                        if (objControlContent.AddIBPS_Q_Dblink_Out(objContent, m_vGW_BANK_CODE) == 1)
                                        {
                                            dgView.Rows.RemoveAt(d);
                                            Common.ShowError("Message has just forwarded to other TAD successfull!", 1, MessageBoxButtons.OK);                                            
                                        }
                                        else
                                        {
                                            Common.ShowError("Message has not forwarded to other TAD!", 1, MessageBoxButtons.OK);                                                                                        
                                            d = d + 1;
                                        }
                                        //objControlContent.UpdateIBPS_MSG_CONTENT_ForwardTAD(objContent); msg_id
                                    }
                                    else
                                    {
                                        d = d + 1;
                                    }
                                }
                                else
                                {
                                    d = d + 1;
                                }
                            }
                        }
                        else if (iRowscount > 1)//so dong duoc chon lon hon 1(Nghia la chon nhieu dong)
                        {
                            iError = 0;
                            int d = 0;
                            while (d < dgView.Rows.Count)
                            {

                                if (dgView.Rows[d].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dgView.Rows[d].Cells[0].Value.ToString() == "True")
                                    {
                                        objContent.MSG_ID = Convert.ToInt32(dgView.Rows[d].Cells["msg_id"].Value.ToString());
                                        objContent.PRE_TAD = "00" + dgView.Rows[d].Cells["TAD"].Value.ToString();
                                        //-------------------------------------------------------------
                                        //DataTable datTT = new DataTable();
                                        //datTT = objControlTAD.GetTAD_SIBS_BANK_CODE("00" + cboNewTAD.Text.Trim());
                                        //string strTAD = datTT.Rows[0]["SIBS_BANK_CODE"].ToString();
                                        String[] N = cboNewTAD.SelectedValue.ToString().Split(new String[] { "#" }, StringSplitOptions.None);//cat chuoi
                                        string m_vSIBS_BANK_CODE = N[0].ToString();
                                        string m_vGW_BANK_CODE = N[1].ToString();
                                        //--------------------------------------------------------------
                                        objContent.TAD = m_vSIBS_BANK_CODE; //cboNewTAD.Text.Trim();
                                        //--------------------------kiem tra co chon gia tri cao hay thap-----------
                                        if (HV_LV.Text.Trim() == "HV" || HV_LV.Text.Trim() == "LV")
                                        {
                                            if (HV_LV.Text.Trim() == "HV")
                                            { objContent.HV_LV = 0; }//truyen gia tri khi chon HV
                                            if (HV_LV.Text.Trim() == "LV")
                                            { objContent.HV_LV = 1; }//truyen gia tri khi chon LV                                           
                                        }
                                        else
                                        {
                                        }
                                        objContent.TELLERID = Common.Userid;
                                        //--------------------------------------------------------------------------
                                        if (objControlContent.AddIBPS_Q_Dblink_Out(objContent, m_vGW_BANK_CODE) == 1)
                                        {
                                            dgView.Rows.RemoveAt(d);
                                        }
                                        else
                                        {                                                                                    
                                            string strSENDER = dgView.Rows[d].Cells["SENDER"].Value.ToString();
                                            string strRECEIVER = dgView.Rows[d].Cells["RECEIVER"].Value.ToString();
                                            string strAMOUNT = dgView.Rows[d].Cells["AMOUNT"].Value.ToString();
                                            string strCCYCD = dgView.Rows[d].Cells["CCYCD"].Value.ToString();
                                            string strRM_NUMBER = dgView.Rows[d].Cells["RM_NUMBER"].Value.ToString();
                                            Cells_columns = Cells_columns + "\r\n" + "------------------------------------" + "\r\n" + "RM_NUMBER :" + strRM_NUMBER + "  SENDER :" + strSENDER + "  RECEIVER :" + strRECEIVER + "  AMOUNT :" + strAMOUNT + "  CCYCD :" + strCCYCD + "  Error";
                                            iError = 1;
                                            d = d + 1;
                                        }
                                        //objControlContent.UpdateIBPS_MSG_CONTENT_ForwardTAD(objContent); msg_id
                                    }
                                    else
                                    {
                                        d = d + 1;
                                    }
                                }
                                else
                                {
                                    d = d + 1;
                                }
                            }
                            //----kiem tra xem co su lieu nao noi khong-------------
                            if (iError == 0)//khong co du lieu nao loi thi thong bao
                            {
                                Common.ShowError("Message has just forwarded to other TAD successfull!", 1, MessageBoxButtons.OK);                               
                            }
                            else//co it nhat mot loi
                            {
                                string Msg = "Do you want view message forward error ?";
                                string title = Common.sCaption;
                                DialogResult DlgResult = new DialogResult();
                                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult == DialogResult.Yes)
                                {
                                    frmMessageError frmerror = new frmMessageError();
                                    frmerror.strCells_columns = Cells_columns;
                                    frmerror.ShowDialog();
                                }
                                else
                                {

                                }
                            }
                        }                        
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "Forward TAD";
                        int Log_level = 1;
                        string strWorked = "";
                        string strTable = "IBPS_BANK_MAP";
                        string strOld_value = "";
                        string strNew_value = objContent.QUERY_ID + "/" + objContent.PRE_TAD + "/" + objContent.TAD;
                        //-----------------------------------------
                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                        //--------------------------------------------------


                        //cmdSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }

        private void frmIBPSMsgFw_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dataSearch_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dgView_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAmount.Text.Trim() == "ALL")
            {
                txtAmount.Enabled = false;
            }
            else
            {
                txtAmount.Enabled = true;
            }
            
        }

        private void dgView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows1 = e.RowIndex; }
        }
         
    }
    
}
