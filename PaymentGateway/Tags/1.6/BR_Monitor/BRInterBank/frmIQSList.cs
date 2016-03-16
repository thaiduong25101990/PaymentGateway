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
namespace BR.BRInterBank
{
    public partial class frmIQSList : Form
    {
        #region dinh nghia ham,bien
        public string strIQSContent;
        public string strMsg_Type = "";
        public IQS_MSG_CONTENTInfo objInfoIQSList = new IQS_MSG_CONTENTInfo();
        IQS_MSG_CONTENTController objControlIQSList = new IQS_MSG_CONTENTController();
        VCB_MSG_CONTENTInfo objInfoCONTENT = new VCB_MSG_CONTENTInfo();
        VCB_MSG_CONTENTController objControlCONTENT = new VCB_MSG_CONTENTController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        TADInfo objInfoTAD = new TADInfo();
        TADController objControlTAD = new TADController();
        Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        public string strProductCode;
        private string strIQSTransNumber = "";
        public string strRMNumber;
        public string strMSGType;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        string HO;
        //private int iID = 0;
        public List<int> listSelected;
        #endregion

        public frmIQSList()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GET_CONTENT()
        {
            try
            {               
                DataTable dsCONTENT = new DataTable();
                string strTable_name = "VCB_MSG_CONTENT";
                dsCONTENT = objControlCONTENT.GetVCB_MSG_CONTENT_CONTENT(objInfoIQSList.MSG_ID.ToString(), strTable_name);
                if (dsCONTENT.Rows.Count == 0)
                { objInfoIQSList.MSGCONTENT = ""; }
                else
                {
                    objInfoIQSList.MSGCONTENT = dsCONTENT.Rows[0]["CONTENT"].ToString();
                }
                if (dsCONTENT.Rows.Count == 0)
                {
                    DataTable dsCONTENT1 = new DataTable();
                    string strTable_name1 = "VCB_MSG_ALL";
                    dsCONTENT1 = objControlCONTENT.GetVCB_MSG_CONTENT_CONTENT(objInfoIQSList.MSG_ID.ToString(), strTable_name1);
                    if (dsCONTENT1.Rows.Count == 0)
                    { objInfoIQSList.MSGCONTENT = ""; }
                    else
                    { objInfoIQSList.MSGCONTENT = dsCONTENT1.Rows[0]["CONTENT"].ToString(); }

                    if (dsCONTENT.Rows.Count == 0 && dsCONTENT1.Rows.Count == 0)
                    {
                        DataTable dsCONTENT2 = new DataTable();
                        string strTable_name2 = "VCB_MSG_ALL_HIS";
                        dsCONTENT2 = objControlCONTENT.GetVCB_MSG_CONTENT_CONTENT(objInfoIQSList.MSG_ID.ToString(), strTable_name2);
                        if (dsCONTENT2.Rows.Count == 0)
                        { objInfoIQSList.MSGCONTENT = ""; }
                        else
                        {
                            objInfoIQSList.MSGCONTENT = dsCONTENT2.Rows[0]["CONTENT"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIQSMessageList.Rows.Count != 0)
                {
                    DataTable ds = new DataTable();
                    ds = objControlTAD.GetTAD_HOST(Common.Userid);
                    if (ds.Rows.Count == 0)
                    { return; }
                    else
                    { HO = ds.Rows[0][0].ToString(); }
                    int d = 0;
                    while (d < dgvIQSMessageList.Rows.Count)
                    {
                        if (dgvIQSMessageList.Rows[d].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dgvIQSMessageList.Rows[d].Cells[0].Value.ToString() == "True")
                            {
                                objInfoIQSList.MSG_ID = Convert.ToInt32(dgvIQSMessageList.Rows[d].Cells["MSG_ID"].Value.ToString());
                                objInfoIQSList.QUERY_ID = Convert.ToInt32(dgvIQSMessageList.Rows[d].Cells["QUERY_ID"].Value.ToString());
                                objInfoIQSList.REF_NUMBER = dgvIQSMessageList.Rows[d].Cells["FIELD20"].Value.ToString();
                                if (dgvIQSMessageList.Rows[d].Cells["VALUE_DATE"].Value.ToString() != "")
                                {
                                    objInfoIQSList.ORG_TRANS_DATE = Convert.ToDateTime(dgvIQSMessageList.Rows[d].Cells["VALUE_DATE"].Value.ToString());
                                }
                                objInfoIQSList.TELLER_ID = Common.Userid;
                                objInfoIQSList.FROMBANK = "00" + HO.Trim();
                                if (dgvIQSMessageList.Rows[d].Cells["MSG_DIRECTION"].Value.ToString() == "VCB-SIBS")
                                {
                                    if (dgvIQSMessageList.Rows[d].Cells["BRANCH_B"].Value.ToString().Length == 3)
                                    {
                                        objInfoIQSList.TOBANK = "00" + dgvIQSMessageList.Rows[d].Cells["BRANCH_B"].Value.ToString();
                                    }
                                    else
                                    {
                                        objInfoIQSList.TOBANK = dgvIQSMessageList.Rows[d].Cells["BRANCH_B"].Value.ToString();
                                    }
                                    if (strMsg_Type == "TS")// khong cho tao dien tra soat theo chieu den "VCB-SIBS"
                                    {
                                        Common.ShowError("Could not create IQS message!", 3, MessageBoxButtons.OK);
                                        return;
                                    }
                                }
                                else
                                {
                                    if (dgvIQSMessageList.Rows[d].Cells["BRANCH_A"].Value.ToString().Length == 3)
                                    {
                                        objInfoIQSList.TOBANK = "00" + dgvIQSMessageList.Rows[d].Cells["BRANCH_A"].Value.ToString();
                                    }
                                    else
                                    {
                                        objInfoIQSList.TOBANK = dgvIQSMessageList.Rows[d].Cells["BRANCH_A"].Value.ToString();
                                    }
                                }

                                objInfoIQSList.INTERFACE = "VCB";
                                objInfoIQSList.DATECREATE = DateTime.Now;
                                objInfoIQSList.STATUS = 0;
                                GET_CONTENT();
                                objInfoIQSList.MSG_TYPE = strMsg_Type;
                                objInfoIQSList.GWOPTION = strIQSContent;
                                objInfoIQSList.PRODUCT_CODE = strProductCode;
                                objInfoIQSList.ORG_RM_NUMBER = dgvIQSMessageList.Rows[d].Cells["RM_NUMBER"].Value.ToString();

                                strMSGType = dgvIQSMessageList.Rows[d].Cells["MSG_TYPE"].Value.ToString();
                                if (dgvIQSMessageList.Rows[d].Cells["AMOUNT"].Value.ToString() != "")
                                {
                                    objInfoIQSList.AMOUNT = Convert.ToDouble(dgvIQSMessageList.Rows[d].Cells["AMOUNT"].Value.ToString());
                                }
                                objInfoIQSList.CCYCD = dgvIQSMessageList.Rows[d].Cells["CCYCD"].Value.ToString();

                                if (strMsg_Type == "TS" & objInfoIQSList.TOBANK == "00" + HO)
                                {
                                    Common.ShowError("Could not create IQS message!", 3, MessageBoxButtons.OK);
                                    return;
                                }
                                objControlIQSList.AddIQS_MSG_CONTENT_TTSP_VCB(objInfoIQSList, strIQSTransNumber, strMSGType);
                                dgvIQSMessageList.Rows[d].Cells[0].Value = null;
                                #region lay thong tin de ghilog----------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "VCB_IQS message";
                                int Log_level = 1;
                                string strWorked = "Create IQS mes";
                                string strTable = "IQS_MSG_CONTENT";
                                string strOld_value = "";
                                string strNew_value = objInfoIQSList.QUERY_ID.ToString();
                                //-----------------------------------------
                                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                #endregion
                            }
                        }
                        d = d + 1;
                    }
                    cmdSave.Enabled = false;
                    Common.ShowError("Create IQS content successfully!", 1, MessageBoxButtons.OK);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIQSList_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                dgvIQSMessageList.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dgvIQSMessageList.Columns[0].HeaderCell = dgvColumnHeader;
                dgvIQSMessageList.Columns[0].Width = 40;
                LoadData();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
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


        private void LoadData()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = objControlIQSList.GetIQS_MSG_CONTENT("VCB", listSelected);
                dgvIQSMessageList.DataSource = datDs.Tables[0];
                //-----------------------------------------------
                ColumnsRead(dgvIQSMessageList);
                ColumsHeader(dgvIQSMessageList);
                DataGridFormat();

                int f = 0;
                while (f < dgvIQSMessageList.Rows.Count)
                {
                    dgvIQSMessageList.Rows[f].Cells[0].Value = true;
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void DataGridFormat()
        {
            try
            {
                dgvIQSMessageList.Columns["AMOUNT"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                //---------------------------------------------                    
                dgvIQSMessageList.Columns["MSG_ID"].Visible = false;
                dgvIQSMessageList.Columns["QUERY_ID"].Visible = false;
                dgvIQSMessageList.Columns["MSG_TYPE"].Visible = false;
                dgvIQSMessageList.Columns["FIELD20"].Visible = false;
                dgvIQSMessageList.Columns["VALUE_DATE"].Visible = false;
                dgvIQSMessageList.Columns["TRANS_DATE"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvIQSMessageList.Columns["DATE_CREATE"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvIQSMessageList.Columns["BRANCH_A"].Width = 140;
                dgvIQSMessageList.Columns["BRANCH_B"].Width = 140;
                dgvIQSMessageList.Columns["RM_NUMBER"].Width = 140;
                dgvIQSMessageList.Columns["AMOUNT"].Width = 100;
                dgvIQSMessageList.Columns["TRANS_DATE"].Width = 100;
                dgvIQSMessageList.Columns["DATE_CREATE"].Width = 140;
                dgvIQSMessageList.Columns["CONTENT"].Width = 300;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
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
                    if (strColumns.Trim() != "BRANCH_A" && strColumns.Trim() != "BRANCH_B" && strColumns.Trim() != "FIELD20" && strColumns.Trim() != "FIELD21" && strColumns.Trim() != "DEPARTMENT")
                    { Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " "); }
                    else
                    {
                        if (strColumns.Trim() == "BRANCH_A")
                        { Datagrid.Columns[g].HeaderCell.Value = "SENDING BRANCH"; }
                        else if (strColumns.Trim() == "BRANCH_B")
                        { Datagrid.Columns[g].HeaderCell.Value = "RECEIVING BRANCH"; }
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
        
        private void enterKey(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)27)
            {
                frmIQSList_FormClosing(null, null);
                this.Close();
            }
        }

        private void frmIQSList_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void frmIQSList_KeyDown(object sender, KeyEventArgs e)
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
                { cmdSave.Focus(); }
                if ((this.ActiveControl) is TextBox)
                { (this.ActiveControl as TextBox).SelectAll(); }
            }
        }

        private void frmIQSList_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dgvIQSMessageList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dgvIQSMessageList.Rows.Count; i++)
                {
                    dgvIQSMessageList.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                }
            }
        }

        private void dgvIQSMessageList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dgvIQSMessageList.RowCount; i++)
                        {
                            this.dgvIQSMessageList.EndEdit();
                            string re_value = this.dgvIQSMessageList.Rows[i].Cells[0].EditedFormattedValue.ToString();
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
