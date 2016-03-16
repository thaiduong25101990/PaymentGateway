using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRSYSTEM;
using BR.BRBusinessObject;

namespace BR.BRIQS
{
    public partial class frmIQSMsgInfo : frmBasedata
    {
        ALLCODEInfo objAllcode = new ALLCODEInfo();
        ALLCODEController objctrolAllcode = new ALLCODEController();
        IQS_MSG_CONTENTInfo objIQS = new IQS_MSG_CONTENTInfo();
        IQS_MSG_CONTENTController objctrlIQS = new IQS_MSG_CONTENTController();
        IQS_CONDITIONInfo objCondition = new IQS_CONDITIONInfo();
        IQS_CONDITIONController objCtrlCondition = new IQS_CONDITIONController();
        Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        public DataTable dsPrint;
        public string Direction;
        public string chanel;
        public string Ondate;
        private int iRows;
        private int iSelect;
        public int selectedRow;

        public frmIQSMsgInfo()
        {
            InitializeComponent();
        }

        private void frmIQSMsgInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                Getdatacombo();
                Getdata();
                dataGridView1.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataGridView1.Columns[0].HeaderCell = dgvColumnHeader;
                dataGridView1.Columns[0].Width = 40;
                this.Text = "IQS Message Management";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        


         /*---------------------------------------------------------------
         * Method           : Getdata()
         * Muc dich         : lay thong tin dien tu bang IQS_MSG_CONTENT
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Getdata()
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
        
        // Lay du lieu do len combobox
        private void Getdatacombo()
        {
            try
            {
                // Lay dien vao STATUS                
                DataTable dtSTATUS =GetData.GetSelect("* from STATUS ").Tables[0];
                if (dtSTATUS.Rows.Count != 0)               
                {
                    int i = 0;
                    cbStatus.Items.Add("ALL");
                    cbStatus.SelectedIndex = 0;
                    while (i < dtSTATUS.Rows.Count)
                    {
                        string strNAME   = dtSTATUS.Rows[i]["NAME"].ToString();
                        string strSTATUS = dtSTATUS.Rows[i]["STATUS"].ToString();
                        cbStatus.Items.Add(strNAME);
                        cbStatus.ValueMember = strSTATUS;
                        i = i + 1;
                    }
                }
                //------------lay dien vao Channel-----------------------
                string strCDD = "GWTYPE";
                string strGWW = "SYSTEM";
                DataTable datChan = new DataTable();
                datChan = objctrolAllcode.GetALLCODE(strCDD, strGWW);
                if (datChan.Rows.Count != 0)                
                {
                    cbChannel.Items.Add("ALL");
                    cbChannel.SelectedIndex = 0;
                    int d = 0;
                    while (d < datChan.Rows.Count)
                    {
                        string strCCC = datChan.Rows[d]["CONTENT"].ToString();
                        cbChannel.Items.Add(strCCC);
                        d = d + 1;
                    }
                }
                
                //------------------------------------------------------------------
                cbDirection.SelectedIndex = 0;                
                string strCD3 = "MSG_TYPE_VIEW";
                string strGWTYE3 = "IQS";
                DataTable datDD1 = new DataTable();
                datDD1 = objctrolAllcode.GetALLCODE(strCD3, strGWTYE3);
                if (datDD1.Rows.Count == 0)
                { }
                else
                {
                    int g = 0;
                    cbType.Items.Add("ALL");
                    cbType.SelectedIndex = 0;
                    while (g < datDD1.Rows.Count)
                    {
                        string strCON = datDD1.Rows[g]["CONTENT"].ToString();
                        cbType.Items.Add(strCON);
                        g = g + 1;
                    }
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
        //Search du lieu
        private void cmdSearch_Click(object sender, EventArgs e)
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
                //sua lai ham nay----------------------------------------
                dataGridView1.DataSource = 0;
                string strStatus = ""; string strDrection = ""; string strType = ""; string strChannel = ""; string strDateCreate;
                strDateCreate = " and  to_char(imc.DATECREATE,'YYYYMMDD') =  '" + dtpDateCreate.Value.ToString("yyyyMMdd") + "'";
                if (cbChannel.Text.Trim() == "ALL") { strChannel = ""; } else { strChannel = " and  Trim(imc.INTERFACE) =  '" + cbChannel.Text.Trim() + "'"; }
                if (cbDirection.Text == "ALL") { strDrection = ""; } else { strDrection = " and  Trim(imc.MSG_DIRECTION) =  '" + cbDirection.Text.Trim() + "'"; }
                if (cbType.Text == "ALL") { strType = ""; }
                else
                {
                    DataTable datAD = new DataTable();
                    datAD = objctrolAllcode.GetALLCODE_code1(cbType.Text.Trim(), "MSG_TYPE_VIEW");
                    string strAAG = datAD.Rows[0]["CDVAL"].ToString();
                    strType = " and  Trim(MSG_TYPE) =  '" + strAAG + "'";
                }
                if (cbStatus.Text.Trim() == "ALL") { strStatus = ""; }
                else
                {
                    DataTable datALLL = GetData.GetSelect(" * from STATUS where NAME='" + cbStatus.Text.Trim() + "'").Tables[0];
                    string strCDVAA = datALLL.Rows[0]["STATUS"].ToString();
                    strStatus = " and  Trim(imc.STATUS) =  '" + strCDVAA + "'";
                }
                string strWhere = strStatus + strDrection + strType + strChannel + strDateCreate;
                string strWhere1 = "";
                if (strWhere == "")
                {
                }
                else
                {
                    strWhere1 = "   Where " + strWhere.Substring(5);
                }
                DataTable datSear = new DataTable();
                datSear = objctrlIQS.Search_IQS(strWhere1);
                dsPrint = datSear;
                Direction = cbDirection.Text.ToString().Trim();
                chanel = cbChannel.Text.ToString().Trim();
                Ondate = dtpDateCreate.Text.ToString().Trim();
                if (datSear.Rows.Count == 0)
                {
                    dataGridView1.DataSource = datSear;
                    FormatDataGridView();
                    ColumsHeader();
                    txtCount.Text = "0";
                    cmdprint.Enabled = false; cmdview.Enabled = false;
                    //Common.ShowError("There is no message!", 1, MessageBoxButtons.OK);
                }
                else
                {
                    dataGridView1.DataSource = datSear;
                    txtCount.Text = Convert.ToString(datSear.Rows.Count);
                    FormatDataGridView();
                    ColumnsRead();
                    ColumsHeader();
                    cmdprint.Enabled = true; cmdview.Enabled = true;
                    BR.BRLib.FomatGrid.Color_datagrid(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        // Cac thao tac tren luoi du lieu
        private void FormatDataGridView()
        {
            try
            {
                dataGridView1.Columns["msg_id"].Visible = false;
                dataGridView1.Columns["query_id"].Visible = false;
                dataGridView1.Columns["PRODUCT_CODE"].Visible = false;
                dataGridView1.Columns["MSG_TYPE"].Width = 110;
                dataGridView1.Columns["REF_NUMBER"].Width = 120;
                dataGridView1.Columns["FROMBANK"].Width = 140;
                dataGridView1.Columns["TOBANK"].Width = 140;
                dataGridView1.Columns["INTERFACE"].Width = 70;
                dataGridView1.Columns["DATECREATE"].Width = 120;
                dataGridView1.Columns["ORG_RM_NUMBER"].Width = 130;
                dataGridView1.Columns["STATUS"].Width = 90;
                dataGridView1.Columns["ORG_TRANS_DATE"].Width = 140;
                //dataGridView1.Columns["PRODUCT_CODE"].Width = 120;
                dataGridView1.Columns["AMOUNT"].Width = 140;
                dataGridView1.Columns["CCYCD"].Width = 40;
                dataGridView1.Columns["TELLER_ID"].Width = 70;
                dataGridView1.Columns["MSGCONTENT"].Width = 300;

                dataGridView1.Columns["REF_NUMBER"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["FROMBANK"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["TOBANK"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["INTERFACE"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["DATECREATE"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["ORG_RM_NUMBER"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["STATUS"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["ORG_TRANS_DATE"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["PRODUCT_CODE"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["CCYCD"].DefaultCellStyle = Common.GetCell_Center();
                dataGridView1.Columns["TELLER_ID"].DefaultCellStyle = Common.GetCell_Center();

                dataGridView1.Columns["ORG_TRANS_DATE"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns["DATECREATE"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss";
                dataGridView1.Columns["AMOUNT"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void ColumnsRead()
        {
            int b = 1;
            while (b < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns[b].ReadOnly = true;
                b = b + 1;
            }
        }
        private void ColumsHeader()
        {
            try
            {
                int g = 1;
                while (g < dataGridView1.Columns.Count)
                {
                    string strColumns = dataGridView1.Columns[g].HeaderText.ToString();
                    if (strColumns.Trim() != "FROMBANK" && strColumns.Trim() != "TOBANK" && strColumns.Trim() != "REF_NUMBER"
                     && strColumns.Trim() != "ORG_RM_NUMBER" && strColumns.Trim() != "DATECREATE"
                        && strColumns.Trim() != "CCYCD" && strColumns.Trim() != "INTERFACE" && strColumns.Trim() != "MSGCONTENT")
                    {
                        dataGridView1.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    }
                    else
                    {
                        if (strColumns.Trim() == "FROMBANK")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "SENDING BRANCH";
                        }
                        else if (strColumns.Trim() == "TOBANK")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "RECEIVING BRANCH";
                        }
                        else if (strColumns.Trim() == "INTERFACE")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "CHANNEL";
                        }

                        else if (strColumns.Trim() == "DATECREATE")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "DATE CREATE";
                        }
                        else if (strColumns.Trim() == "ORG_RM_NUMBER")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "ORG RM NUMBER";
                        }
                        else if (strColumns.Trim() == "CCYCD")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "CCY";
                        }
                        else if (strColumns.Trim() == "MSGCONTENT")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "IQS CONTENT";
                        }
                        else if (strColumns.Trim() == "INTERFACE")
                        {
                            dataGridView1.Columns[g].HeaderCell.Value = "CHANNEL";
                        }
                    }
                    //Datagrid.Columns[g].DefaultCellStyle.Format
                    dataGridView1.ColumnHeadersHeight = 21;
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        // Xu li su kien doi voi dien da chon
        private void cmdview_Click(object sender, EventArgs e)
        {
            try
            {
                int f = 0;
                int iDem = 0;
                frmIQSMsgInfo1 dg = new frmIQSMsgInfo1();
                while (f < dataGridView1.Rows.Count)
                {
                    if (dataGridView1.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGridView1.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            iDem = iDem + 1;
                        }
                    }                    
                    f = f + 1;
                }
                if (iDem == 0)
                {
                    #region
                    ////truyen bien--------------------------------
                    //dg.strFromBank = dataGridView1.Rows[iRows].Cells["FROMBANK"].Value.ToString();
                    //dg.strToBank = dataGridView1.Rows[iRows].Cells["TOBANK"].Value.ToString();
                    ////dg.strRmno = dataGridView1.Rows[iRows].Cells["F20"].Value.ToString();
                    //dg.strRmno = dataGridView1.Rows[iRows].Cells["REF_NUMBER"].Value.ToString();
                    //dg.strDatecreate = dataGridView1.Rows[iRows].Cells["DATECREATE"].Value.ToString();
                    //dg.strContent = dataGridView1.Rows[iRows].Cells["MSGCONTENT"].Value.ToString();
                    //dg.strQUERY_ID = dataGridView1.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                    ////--------------------------------------------
                    #endregion
                    dg.strQUERY_ID = dataGridView1.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                    dg.strIQSTransNum = dataGridView1.Rows[iRows].Cells["IQSTRANSNUM"].Value.ToString();
                    dg.strREFnumber = dataGridView1.Rows[iRows].Cells["REF_NUMBER"].Value.ToString();
                    dg.strRmno = dataGridView1.Rows[iRows].Cells["ORG_RM_NUMBER"].Value.ToString();
                    dg.strFromBank = dataGridView1.Rows[iRows].Cells["FROMBANK"].Value.ToString();
                    dg.strToBank = dataGridView1.Rows[iRows].Cells["TOBANK"].Value.ToString();
                    dg.strAmount = dataGridView1.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                    dg.strCCYCD = dataGridView1.Rows[iRows].Cells["CCYCD"].Value.ToString();
                    dg.strProductCode = dataGridView1.Rows[iRows].Cells["PRODUCT_CODE"].Value.ToString();
                    dg.strStatus = dataGridView1.Rows[iRows].Cells["STATUS"].Value.ToString();
                    dg.strTransDate = dataGridView1.Rows[iRows].Cells["ORG_TRANS_DATE"].Value.ToString();
                    dg.strDateCreate = dataGridView1.Rows[iRows].Cells["DATECREATE"].Value.ToString();
                    dg.strTellerID = dataGridView1.Rows[iRows].Cells["TELLER_ID"].Value.ToString();
                    dg.strContent = dataGridView1.Rows[iRows].Cells["MSGCONTENT"].Value.ToString();                   
                    dg.ShowDialog();
                }
                else
                {
                    if (dataGridView1.Rows.Count != 0)
                    {
                        int d = 0;
                        while (d < dataGridView1.Rows.Count)
                        {
                            if (dataGridView1.Rows[d].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dataGridView1.Rows[d].Cells[0].Value.ToString() == "True")
                                {
                                    dg.strQUERY_ID = dataGridView1.Rows[d].Cells["QUERY_ID"].Value.ToString();
                                    dg.strIQSTransNum = dataGridView1.Rows[d].Cells["IQSTRANSNUM"].Value.ToString();
                                    dg.strREFnumber = dataGridView1.Rows[d].Cells["REF_NUMBER"].Value.ToString();
                                    dg.strRmno = dataGridView1.Rows[d].Cells["ORG_RM_NUMBER"].Value.ToString();
                                    dg.strFromBank = dataGridView1.Rows[d].Cells["FROMBANK"].Value.ToString();
                                    dg.strToBank = dataGridView1.Rows[d].Cells["TOBANK"].Value.ToString();
                                    dg.strAmount = dataGridView1.Rows[d].Cells["AMOUNT"].Value.ToString();
                                    dg.strCCYCD = dataGridView1.Rows[d].Cells["CCYCD"].Value.ToString();
                                    dg.strProductCode = dataGridView1.Rows[d].Cells["PRODUCT_CODE"].Value.ToString();
                                    dg.strStatus = dataGridView1.Rows[d].Cells["STATUS"].Value.ToString();
                                    dg.strTransDate = dataGridView1.Rows[d].Cells["ORG_TRANS_DATE"].Value.ToString();
                                    dg.strDateCreate = dataGridView1.Rows[d].Cells["DATECREATE"].Value.ToString();
                                    dg.strTellerID = dataGridView1.Rows[d].Cells["TELLER_ID"].Value.ToString();
                                    dg.strContent = dataGridView1.Rows[d].Cells["MSGCONTENT"].Value.ToString();                                   
                                    dg.ShowDialog();
                                }
                            }
                            dataGridView1.Rows[d].Cells[0].Value = null;
                            if (dg.bIsCloseForm == true)
                            {
                                f = dataGridView1.Rows.Count;
                                int p = 0;
                                while (p < dataGridView1.Rows.Count)
                                {
                                    dataGridView1.Rows[p].Cells[0].Value = null;
                                    p = p + 1;
                                    dgvColumnHeader.CheckAll = false;
                                }
                                return;
                            }
                            else
                            {
                                d = d + 1;
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
                if (e.RowIndex == -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        for (int i = 0; i < this.dataGridView1.RowCount; i++)
                        {
                            this.dataGridView1.EndEdit();
                            string re_value = this.dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString();
                        }
                    }
                }                
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
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int numR, numC;
            DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                numR = hit.RowIndex;
                numC = hit.ColumnIndex;
                selectedRow = numR;
                dataGridView1.Rows[numR].Selected = false;
            }
            else return;
            // Neu chuot trai hien thi chi tiet khach hang
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    frmIQSMsgInfo1 dg = new frmIQSMsgInfo1();                    
                    dg.strQUERY_ID = dataGridView1.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                    //dg.strIQSTransNum = dataGridView1.Rows[iRows].Cells["REF_NUMBER"].Value.ToString();
                    dg.strIQSTransNum = dataGridView1.Rows[iRows].Cells["IQSTRANSNUM"].Value.ToString();
                    dg.strREFnumber = dataGridView1.Rows[iRows].Cells["REF_NUMBER"].Value.ToString();
                    dg.strRmno = dataGridView1.Rows[iRows].Cells["ORG_RM_NUMBER"].Value.ToString();
                    dg.strFromBank = dataGridView1.Rows[iRows].Cells["FROMBANK"].Value.ToString();
                    dg.strToBank = dataGridView1.Rows[iRows].Cells["TOBANK"].Value.ToString();
                    dg.strAmount = dataGridView1.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                    dg.strCCYCD = dataGridView1.Rows[iRows].Cells["CCYCD"].Value.ToString();
                    dg.strProductCode = dataGridView1.Rows[iRows].Cells["PRODUCT_CODE"].Value.ToString();
                    dg.strStatus = dataGridView1.Rows[iRows].Cells["STATUS"].Value.ToString();
                    dg.strTransDate = dataGridView1.Rows[iRows].Cells["ORG_TRANS_DATE"].Value.ToString();
                    dg.strDateCreate = dataGridView1.Rows[iRows].Cells["DATECREATE"].Value.ToString();
                    dg.strTellerID = dataGridView1.Rows[iRows].Cells["TELLER_ID"].Value.ToString();
                    dg.strContent = dataGridView1.Rows[iRows].Cells["MSGCONTENT"].Value.ToString();
                    dg.ShowDialog();
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
            }
        }
        // Xu li nut in
        private void cmdprint_Click(object sender, EventArgs e)
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
                    while (f < dataGridView1.Rows.Count)
                    {
                        if (dataGridView1.Rows[f].Cells[0].Value != null && dataGridView1.Rows[f].Cells[0].Value.ToString() == "True")// hang khong duoc chon
                        {
                            string strMSG_ID = dataGridView1.Rows[f].Cells["MSG_ID"].Value.ToString().Trim();
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
                string Print = "IQS_PRINT_ALL";
                frmPrint.PrintType = Print;
                frmPrint.Direction = Direction;
                frmPrint.OnDate = Ondate;
                frmPrint.chanel = chanel;
                //frmPrint.HMdat = datPrint;
                frmPrint.WindowState = FormWindowState.Maximized;
                frmPrint.ShowDialog();
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
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                    }
                }
                BR.BRLib.FomatGrid.Color_datagrid(dataGridView1);
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
                while (b < dataGridView1.Rows.Count)
                {
                    if (dataGridView1.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGridView1.Rows[b].Cells[0].Value.ToString() == "True")
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
    }
}
