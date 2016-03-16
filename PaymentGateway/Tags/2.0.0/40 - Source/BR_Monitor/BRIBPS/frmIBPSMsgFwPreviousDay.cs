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

namespace BR.BRIBPS
{
    public partial class frmIBPSMsgFwPreviousDay : frmBasedata
    {
        IBPS_MSG_LOGController objctLog = new IBPS_MSG_LOGController();
        IBPS_MSG_LOGInfo objLogInfo = new IBPS_MSG_LOGInfo();

        IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
        IBPS_MSG_ALLInfo objContent = new IBPS_MSG_ALLInfo();
        IBPS_MSG_ALLController objControlContent = new IBPS_MSG_ALLController();
        IBPS_MSG_CONTENTController objCtrlCont = new IBPS_MSG_CONTENTController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        TADInfo objTAD = new TADInfo();
        TADController objControlTAD = new TADController();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader1 = new BR.BRLib.Common.DGVColumnHeader();
        //DGVColumnHeader2 dgvColumnHeader;
        public DataTable dtPrint;
        public DataTable dsPrint;
        private string Cells_columns;
        private int iRowscount;
        private int iError;
        private int iSelect;
        public string OnDate;
        string strTranDate = "";
        //----------------------------------
        private int existed;
        private string strMessage = "";
        private int iRows;
        //private int iRows1;
        //-------------------------------------
        string strAmount;
        string strResender;
        string strTrans;
        //-------------------------------------
        public frmIBPSMsgFwPreviousDay()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Locate_Controls()
        {
            try
            {
                cbAmount.SelectedIndex = 0; txtTAD.Enabled = false;
                dgView.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dgView.Columns[0].HeaderCell = dgvColumnHeader;
                dataSearch.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataSearch.Columns[0].HeaderCell = dgvColumnHeader1;
                dataSearch.Columns[0].Width = 26;
                dgView.Columns[0].Width = 26;
                cmdAdd.Visible = false; cmdSave.Visible = false; cmdEdit.Visible = false;
                cmdDelete.Visible = false; cmdRefresh.Visible = false; cmdForward.Enabled = false;  
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
                    cmdPrint.Enabled = true; cmdForward.Enabled = true;
                }
                label1.Text = "Total number of messages :" + dataSearch.Rows.Count;
                label12.Text = "Total number of messages :" + dgView.Rows.Count;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }  
        }


        private void frmIBPSMsgFwPreviousDay_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                Locate_Controls();
                Load_data();
                comModeTranDate.Text = comModeTranDate.Items[0].ToString();
                Enable_controls();
                //GetData();           
                OnDate = dateTimePicker1.Text.ToString().Trim();
                txtGW1.Focus();
                //cboHVLV.SelectedIndex = 0;
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
                dataSearch = clsDatagridviews.FREVIOUS_LOAD(dataSearch, Common.Userid,out dtPrint);
                dsPrint = ControlConten.FREVIOUS_PRINT_LOAD(Common.Userid);
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
        //ham forward dien---------------------------------------
        private void cmdForward_Click(object sender, EventArgs e)
        {
            iRowscount = 0;
            int pHL_Val;
            double iAmount;
            try
            {
                if (dgView.Rows.Count != 0)
                {                   
                    int d = 0;
                    while (d < dgView.Rows.Count)
                    {
                        //goi ham lay gia tri cao hay gia tri thap tu dong---------------------------
                        string ppTAD = dgView.Rows[d].Cells["TAD"].Value.ToString();
                        
                        string ppAmount = dgView.Rows[d].Cells["AMOUNT"].Value.ToString();
                        String[] M = ppAmount.Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);
                        string strAM = M[1];
                        if (M[1].Trim() == "00")
                        {
                            iAmount = Convert.ToDouble(ppAmount.Replace(",", "").Trim().Substring(0, ppAmount.Replace(",", "").Replace(".", "").Trim().Length - 2));
                        }
                        else
                        {
                            iAmount = Convert.ToDouble(ppAmount.Replace(",", "").Trim());
                        }
                        string datUUHD = objCtrlCont.Get_HV_LV(ppTAD, iAmount);
                        string strHVLV = datUUHD;//101001
                        if (strHVLV.Trim() == "101001")
                        {
                            pHL_Val = 1;
                        }
                        else
                        {
                            pHL_Val = 0;
                        }
                        //if (cboHVLV.Text.Trim() == "LV")
                        //{
                        //    pHL_Val = 1;
                        //}
                        //else
                        //{
                        //    pHL_Val = 0;
                        //}
                        //---------------------------------------------------------------------------
                        int pMSG_ID = Convert.ToInt32(dgView.Rows[d].Cells["MSG_ID"].Value.ToString());
                        string strTellerID = Common.Userid;
                        if (objControlContent.Forward_LV_HV(pMSG_ID, pHL_Val, strTellerID) == 1)
                        {
                            
                            objLogInfo.LOG_DATE = DateTime.Now;
                            objLogInfo.QUERY_ID = Convert.ToInt32(dgView.Rows[d].Cells["QUERY_ID"].Value.ToString());
                            objLogInfo.STATUS = 1;
                            objLogInfo.DESCRIPTIONS = Common.Userid + " resend old message ";
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
                        //lay thong tin de ghilog----------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "Resend old message";
                        int Log_level = 1;
                        string strWorked = "";
                        string strTable = "IBPS_BANK_MAP";
                        string strOld_value = "";
                        string strNew_value = objContent.QUERY_ID + "/" + objContent.PRE_TAD + "/" + objContent.TAD;
                        //-----------------------------------------
                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                        //--------------------------------------------------
                    }
                    if (iError == 0)//khong co du lieu nao loi thi thong bao
                    {
                        Common.ShowError("Message has just forwarded to other TAD successfull!", 1, MessageBoxButtons.OK);                        
                    }
                    else//co it nhat mot loi
                    {
                        string Msg = "Do you want view message forward error ?";
                        string title = Common.sCaption;
                        DialogResult DlgResult = new DialogResult();
                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (DlgResult == DialogResult.Yes)
                        {
                            frmMessageError frmerror = new frmMessageError();
                            frmerror.strCells_columns = Cells_columns;
                            frmerror.ShowDialog();
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }  
            label12.Text = "Total number of messages :" + dgView.Rows.Count;            
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
        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                cmdForward.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                cmdForward.Enabled = true;
            }
        }

        private void data1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
            if (e.RowIndex != -1)
            {
            }
        }

        private void frmIBPSMsgFwPreviousDay_KeyDown(object sender, KeyEventArgs e)
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
                                    dataSearch.Rows.RemoveAt(z);
                                }
                            }
                            else { z = z + 1; }
                        }
                        else { z = z + 1; }
                    }
                    if (strMessage.Trim() != "Msg_id")
                    {
                        MessageBox.Show(strMessage + "\r\n" + "has already exist", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                #endregion
                #region khong click chon
                else if (iSelect == 0)
                {
                    existed = 0;
                    strMessage = "Msg_id"; int x = 0;
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
                        dataSearch.Rows.RemoveAt(iRows);
                    }
                    if (strMessage.Trim() != "Msg_id")
                    {
                        MessageBox.Show(strMessage + "\r\n" + "has already exist", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                dgView.Rows.RemoveAt(m);
                            }
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

        private void dataSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
            if (e.RowIndex != -1) { iRows = e.RowIndex; }
        }

        private void dataSearch_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataSearch_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dataSearch.Rows.Count; i++)
                {
                    dataSearch.Rows[i].Cells[0].Value = dgvColumnHeader1.CheckAll;
                }
            }           
        }

        private void data1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }  
        }

        private void dgView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
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
                //-----------------------------------------------------------------------------
                string strAmount1 = txtAmount.Text.Trim();
                if (strAmount1 != "")               
                {
                    if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                    {
                        txtAmount.Text = Common.FormatCurrency(strAmount1.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }                   
                }                
                dataSearch.Rows.Clear();
                Search_Forward();               
                OnDate = dateTimePicker1.Text.ToString().Trim();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }  
        }
        private void Search_Forward()
        {
            try
            {
                #region Lay tao ra menh de where
                if (txtGW1.Text.Trim() == "" && txtGW2.Text.Trim() == "")
                {
                    strTrans = "";
                }
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
                    strTrans = "  and  " + txtGW1.Text.Trim().ToUpper() + " <=  upper(Trim(GW_TRANS_NUM))  and  upper(Trim(GW_TRANS_NUM))  <= " + txtGW2.Text.Trim().ToUpper() + "";
                }
                //-------------------------------------------------------------------------------------------------------------------
                if (txtReSender.Text.Trim() == "") { strResender = ""; } else { strResender = " and   upper(Trim(F22)) like '%" + txtReSender.Text.Trim().ToUpper() + "%'"; }
                //---------------------------------------------------------------------------------------------                
                if (cbAmount.Text.Trim() == "ALL")
                { strAmount = ""; }
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
                if (comModeTranDate.Text != "ALL")
                {
                    string pDatetimeNow = TransTime.Value.TimeOfDay.ToString();
                    String[] M = pDatetimeNow.Split(new String[] { ":" }, StringSplitOptions.None);
                    string pTimeSearch = M[0] + M[1];
                    strTranDate = " and To_char(TRANS_DATE,'HH24MM') " + comModeTranDate.Text + " LPAD('" + pTimeSearch + "',4,'0') ";
                }
                else
                {
                    strTranDate = "";
                }
                #endregion Lay tao ra menh de where
                string strWhere = strResender + strAmount + strTrans + strTranDate;
                DateTime dataU = DateTime.Now.Date;
                //string pDate_time = Convert.ToString(dataU);
                //String[] N = pDate_time.Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
                //string pDate = N[2].Replace("'", "") + N[1].Replace("'", "") + N[0].Replace("'", "");
                string pYear = dateTimePicker1.Value.Year.ToString();
                string pMonth = dateTimePicker1.Value.Month.ToString();
                string pDay = dateTimePicker1.Value.Day.ToString();
                if (pMonth.Length == 1) { pMonth = "0" + pMonth; }
                if (pDay.Length == 1) { pDay = "0" + pDay; }
                string pDate = pYear + pMonth + pDay;
                if (dateTimePicker1.Checked == false)
                {
                    dataSearch = clsDatagridviews.FREVIOUS_SEARCH(dataSearch, Common.Userid, "", out dtPrint, strWhere);
                    dataSearch = clsForward.Remove_rows_exits(dataSearch, dgView);
                    //datSearch = objControlContent.GetCntent_FR_FR(strWhere, dataU, Common.Userid);
                    //dtPrint = objControlContent.GetCntent_FR_FR_Previous(strWhere, dataU, Common.Userid);
                    dsPrint = ControlConten.FREVIOUS_PRINT(Common.Userid, "", strWhere);
                }
                else if (dateTimePicker1.Checked == true)
                {
                    dataSearch = clsDatagridviews.FREVIOUS_SEARCH(dataSearch, Common.Userid, pDate, out dtPrint, strWhere);
                    dataSearch = clsForward.Remove_rows_exits(dataSearch, dgView);
                    //datSearch = objControlContent.GetCntent_FR_FR_Date(strWhere, dateTimePicker1.Value, Common.Userid);
                    //dtPrint = objControlContent.GetCntent_FR_FR_Previous_date(strWhere, dateTimePicker1.Value , Common.Userid);
                    dsPrint = ControlConten.FREVIOUS_PRINT(Common.Userid, pDate, strWhere);
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
        private void frmIBPSMsgFwPreviousDay_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        private void Check_Select_Rows(DataGridView _dtgr)
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < _dtgr.Rows.Count)
                {
                    if (_dtgr.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtgr.Rows[b].Cells[0].Value.ToString() == "True")
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
        private void cmd_Pint_Click(object sender, EventArgs e)
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
                string Print = "IBPS_BM02";
                frmPrint.PrintType = Print;
                string rpttype = "PREVIOUSDAY";
                frmPrint.rpttype = rpttype;
                frmPrint.OnDate = OnDate;
                string teller = Common.Userid;
                frmPrint.teller = teller; 
                //frmPrint.CurrentTad = CurrentTad;
                //frmPrint.CurrentValue = CurrentValue;
                frmPrint.WindowState = FormWindowState.Maximized;
                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }       
        }

        private void dataSearch_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void data1_MouseMove(object sender, MouseEventArgs e)
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
            if (e.RowIndex != -1) { iRows = e.RowIndex; }
        }
    }   
}
