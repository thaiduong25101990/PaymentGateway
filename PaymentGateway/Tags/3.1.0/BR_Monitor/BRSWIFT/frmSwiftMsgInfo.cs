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

namespace BR.BRSWIFT
{
    public partial class frmSwiftMsgInfo : BR.BRSYSTEM.frmBasedata
    {
        #region khai bao cac lop trong Bussiness        
        private GetData objGetData = new GetData();
        public SWIFT_MSG_LOGInfo objSWIFT_log = new SWIFT_MSG_LOGInfo();
        SWIFT_MSG_LOGController objcontrolSWIFT_log = new SWIFT_MSG_LOGController();
        public SWIFT_MSG_CONTENTInfo objContent = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objControlContent = new SWIFT_MSG_CONTENTController();
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBranch = new BRANCHController();
        #endregion

        #region khai bao cac bien
        public string strMSG_ID;
        public  string strQUERY_ID;
        public bool bIsCloseForm = false;
        private bool iClose = false;
        private bool NeedConfirm = true;
        private static bool strSucess = false;        
        public string strLOCKSTS;
        public string strLOCK_TELLERID;
        string strAmount;
        public string strMsg_type;
        public string strDepartment;
        string strMapField;
        public string strMSG_DIRECTION;
        public bool isfrmSwiftMsgList=true;
        public string _Processsts = "";
        private string _Content="";
       
        public DataTable dtMap;
        #endregion

        public frmSwiftMsgInfo()
        {
            InitializeComponent();
        }
        private DataTable Getcontent()
        {
            try
            {
                DataTable datContent = new DataTable();
                datContent = objControlContent.Search_Content(strMSG_ID);
                return datContent;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return null;
            }
        }


        private void frmSwiftMsgInfo_Load(object sender, EventArgs e)
        {           
            cmdAdd.Visible = false;cmdDelete.Visible = false;
            cmdEdit.Visible = false;cmdSave.Visible = false;
            cmdClose.Show();
            try
            {                
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                if (!objGetData.FillDataComboBox(cbPrintSTS, "CONTENT", "CDVAL",
                    "ALLCODE", "cdname='PRINTSTS'", "CONTENT", true, false,""))
                    return;
                GET_MSG_DTL();
                string strQUERY_ID1 = strQUERY_ID.Replace("\r", "").Replace("\r\n", "").Trim();
                GetHistory(strQUERY_ID1);//goi ham lay ra lich su dien
                GetHistoryManualInfo(Convert.ToInt32(strQUERY_ID));
                if (_Processsts == Common.PROCESSSTS_REPAIR)
                {
                    tabInfomation.TabPages["tbOrgContent"].Show();
                    track_change();
                }
                else
                {
                    DataTable _dt = new DataTable();
                    _dt = objControlContent.GET_PR_PROCESSSTS(strQUERY_ID);
                    if (_dt.Rows[0]["PRE_PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_REPAIR || _dt.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_REPAIR)
                    {
                        track_change();
                    }
                    else
                    {
                        tabInfomation.TabPages["tbOrgContent"].Hide();
                        Load_orgContent(_Content);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Load_orgContent(string vCONTENT)
        {
            try
            {
                int count = 0;
                if (dtgOrgContent.Rows.Count > 0) { dtgOrgContent.Rows.Clear(); }
                String[] N = vCONTENT.Split(new String[] { "\r\n" }, StringSplitOptions.None);//cat chuoi

                for (int i = 0; i < N.Count<String>(); i++)
                {
                    if (dtgOrgContent.Rows.Count == 0)
                    {
                        /*Luoi dien Old*/
                        dtgOrgContent.Rows.Add();
                        dtgOrgContent.Rows[0].Cells["OrgContent"].Value = N[i];
                        dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                    }
                    else
                    {
                        count = dtgOrgContent.Rows.Count;
                        int k = 0;
                        while (k < count)
                        {
                            if (k == count - 1)
                            {
                                /*Luoi dien Old*/
                                dtgOrgContent.Rows.Add();
                                dtgOrgContent.Rows[count].Cells["OrgContent"].Value = N[i];
                                dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                            }
                            k = k + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void track_change()
        {
            try
            {
                int count = 0;
                if (dtgOrgContent.Rows.Count > 0) { dtgOrgContent.Rows.Clear(); }
                if (dtgEditContent.Rows.Count > 0) { dtgEditContent.Rows.Clear(); }
                DataTable _dt = new DataTable();
                _dt = objControlContent.GET_SWIFT_MSG_EDIT(Convert.ToInt32(strQUERY_ID));
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (dtgOrgContent.Rows.Count == 0)
                        {
                            /*Luoi dien Old*/
                            dtgOrgContent.Rows.Add();
                            dtgOrgContent.Rows[0].Cells["OrgContent"].Value = _dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString();
                            dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                            if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString() == "ADD")
                            {
                                dtgOrgContent.Rows[0].Visible = false;
                            }
                            /*Luoi dien Edit*/
                            dtgEditContent.Rows.Add();
                            dtgEditContent.Rows[0].Cells["EditContent"].Value = _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString();
                            dtgEditContent.Columns["EditContent"].ReadOnly = true;
                            if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString().Trim() != _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString().Trim())
                            {
                                dtgEditContent.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            count = dtgOrgContent.Rows.Count;
                            int k = 0;
                            while (k < count)
                            {
                                if (k == count - 1)
                                {
                                    /*Luoi dien Old*/
                                    dtgOrgContent.Rows.Add();
                                    dtgOrgContent.Rows[count].Cells["OrgContent"].Value = _dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString();
                                    dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                                    if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString() == "ADD")
                                    {
                                        dtgOrgContent.Rows[count].Visible = false;
                                    }
                                    /*Luoi dien Edit*/
                                    dtgEditContent.Rows.Add();
                                    dtgEditContent.Rows[count].Cells["EditContent"].Value = _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString();
                                    dtgEditContent.Columns["EditContent"].ReadOnly = true;
                                    if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString().Trim() != _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString().Trim())
                                    {
                                        dtgEditContent.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                }
                                k = k + 1;
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

        private void GET_MSG_DTL()
        {
            try 
            {
                DataTable datContent = new DataTable();
                datContent = Getcontent();
                if (datContent.Rows.Count > 0)
                {
                    txtRM_NUMBER.Text = datContent.Rows[0]["RM_NUMBER"].ToString().TrimStart('0');
                    txtMsgType.Text = datContent.Rows[0]["MSG_TYPE"].ToString();
                    txtsending_bank.Text = datContent.Rows[0]["BRANCH_A"].ToString().TrimStart('0');
                    txtreceiving_bank.Text = datContent.Rows[0]["BRANCH_B"].ToString().TrimStart('0');
                    strAmount = datContent.Rows[0]["AMOUNT"].ToString();
                    strQUERY_ID = datContent.Rows[0]["QUERY_ID"].ToString();
                    if (strAmount.Trim() == "")
                    {
                        txtamount.Text = "";
                    }
                    else
                    {
                        txtamount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    }
                    txtrm_no.Text = datContent.Rows[0]["FIELD20"].ToString();
                    txtStatus.Text = datContent.Rows[0]["STATUS"].ToString();
                    strDepartment = datContent.Rows[0]["DEPARTMENT"].ToString();
                    txtdepartment.Text = strDepartment;
                    lbCCYCD.Text = datContent.Rows[0]["CCYCD"].ToString();
                    lbSend_bankname.Text = datContent.Rows[0]["BRANCH_A_NAME"].ToString();
                    lbRecevi_name.Text = datContent.Rows[0]["BRANCH_B_NAME"].ToString();
                    txtcontent.Text = datContent.Rows[0]["CONTENT"].ToString().Trim();
                    _Content = datContent.Rows[0]["CONTENT"].ToString().Trim();
                    cbPrintSTS.SelectedValue = datContent.Rows[0]["PRINT_STS"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        // lay ra thong tin lich su dien
        private void GetHistory(string QUERY_ID)
        {
            try
            {
                string strSWIFT_msg_log = "";
                //lay du lieu tu bang IBPS_MSG_LOG
                DataSet datHistory = new DataSet();
                datHistory = objcontrolSWIFT_log.GetSWIFT_MSG_LOG(QUERY_ID);
                if (datHistory.Tables[0].Rows.Count != 0)                
                {
                    int g = 0;
                    while (g < datHistory.Tables[0].Rows.Count)
                    {
                        string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                        string strQUERY_ID = datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString();
                        string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                        string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();                       
                        strSWIFT_msg_log = strSWIFT_msg_log + "\r\n" + " + : " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;                        
                        txthistory.Text = strSWIFT_msg_log;
                        g = g + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //lay ra thong tin lich su dien
        private void GetHistoryManualInfo(int QUERY_ID)
        {
            try
            {
                string strSWIFT_msg_log = "";               
                DataSet datHistory = new DataSet();
                datHistory = objcontrolSWIFT_log.GetSWIFT_MSG_LOG_ManualInfo(QUERY_ID);
                if (datHistory.Tables[0].Rows.Count != 0)                
                {
                    int g = 0;
                    while (g < datHistory.Tables[0].Rows.Count)
                    {
                        string strLOG_DATE = datHistory.Tables[0].Rows[g]["LOG_DATE"].ToString();
                        string strQUERY_ID = datHistory.Tables[0].Rows[g]["QUERY_ID"].ToString();
                        string strSTATUS = datHistory.Tables[0].Rows[g]["STATUS"].ToString();
                        string strDESCRIPTIONS = datHistory.Tables[0].Rows[g]["DESCRIPTIONS"].ToString();                       
                        strSWIFT_msg_log = strSWIFT_msg_log + "\r\n" + ": " + strLOG_DATE + "//" + strQUERY_ID + "//" + strSTATUS + "//" + strDESCRIPTIONS;                        
                        txthistory.Text = strSWIFT_msg_log;
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
            iClose = true; this.Close(); iClose = false;
        }

      
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            frmPrint frmPrint = new frmPrint();            
            string Print;
            string strMap;
            if (strMsg_type.Trim().ToUpper() == "MT700" || strMsg_type.Trim() == "MT701" || strMsg_type.Trim() == "MT721")
            {
                Print = "SWIFT_PRINT_TF";
                frmPrint.PrintType = Print;
                frmPrint.strMsgID = strMSG_ID;
                frmPrint.loaidien = strMsg_type;
                frmPrint.chieudien = strMSG_DIRECTION;
                GetData_Print(strDepartment);
                if (dtMap == null || dtMap.Rows.Count == 0)
                {
                    Common.ShowError("Data not found!", 3, MessageBoxButtons.OK);
                }
                else
                {
                    frmPrint.HMdat = dtMap;
                    frmPrint.ShowDialog();
                }
            }
            else
            {
                Print = "SWIFT_PRINT";
                frmPrint.PrintType = Print;
                frmPrint.strMsgID = strMSG_ID;
                frmPrint.loaidien = strMsg_type;
                frmPrint.chieudien = strMSG_DIRECTION;
                GetData_Print(strDepartment);
                if (dtMap == null || dtMap.Rows.Count == 0)
                {
                    Common.ShowError("Data not found!", 3, MessageBoxButtons.OK);
                }
                else
                {
                    frmPrint.HMdat = dtMap;
                    frmPrint.ShowDialog();
                }
            }
            #region Comment
            //if (strDepartment.Trim() == "RM" || strDepartment.Trim() == "TR")
            //{
            //    Print = "SWIFT_PRINT";
            //    frmPrint.PrintType = Print;     
            //    frmPrint.strMsgID = strMSG_ID;
            //    frmPrint.loaidien = strMsg_type;
            //    frmPrint.chieudien = strMSG_DIRECTION;
            //    GetData_Print(strDepartment);
            //    if (dtMap == null || dtMap.Rows.Count == 0)
            //    {
            //        Common.ShowError("Data not found!", 3, MessageBoxButtons.OK);                    
            //    }
            //    else
            //    {
            //        frmPrint.HMdat = dtMap;
            //        frmPrint.ShowDialog();
            //    }
            //}
            //else
            //{
            //    Print = "SWIFT_PRINT_TF";
            //    frmPrint.PrintType = Print;
            //    frmPrint.strMsgID = strMSG_ID;
            //    frmPrint.loaidien = strMsg_type;
            //    frmPrint.chieudien = strMSG_DIRECTION;
            //    GetData_Print(strDepartment);
            //    if (dtMap == null || dtMap.Rows.Count == 0)
            //    {
            //        Common.ShowError("Data not found!", 3, MessageBoxButtons.OK);                    
            //    }
            //    else
            //    {
            //        frmPrint.HMdat = dtMap;
            //        frmPrint.ShowDialog();
            //    }
            //}
            //////else
            //////{
            //////    Print = "SWIFT_PRINT";
            //////    frmPrint.PrintType = Print;
            //////    Getcontent_Print();                
            //////    if (dtMap == null || dtMap.Rows.Count == 0)
            //////    {
            //////        MessageBox.Show("Data not found!", Common.sCaption); 
            //////    }
            //////    else
            //////    {
            //////        strMap = dtMap.Rows[0]["CONTENT"].ToString();
            //////        int x = strMap.IndexOf("{4:");
            //////        int y = strMap.Substring(x + "{4:".Length).IndexOf("{4:");
            //////        if (y > 0)
            //////        {
            //////            x = x + y + "{4:".Length;
            //////        }
            //////        int z = strMap.Substring(x).IndexOf("}");
            //////        string strContent;
            //////        if (z <= 0)
            //////        {
            //////            strContent = strMap.Substring(x + "{4:".Length + "\r\n".Length);
            //////        }
            //////        else
            //////        {
            //////            strContent = strMap.Substring(x + "{4:".Length + "\r\n".Length, z - "{4:\r\n".Length - "\r\n}".Length);
            //////        }
            //////        //------------------
            //////        string Content1 = ""; string Content2 = ""; string Content3 = ""; string Content4 = "";
            //////        string Content5 = ""; string Content6 = ""; string Content7 = ""; string Content8 = "";
            //////        string Content9 = "";
            //////        if (strContent.Length <= 1300)
            //////        {
            //////            Content1 = strContent;
            //////        }
            //////        else
            //////        {
            //////            Content1 = strContent.Substring(0, 1300);
            //////            if (strContent.Length <= 2600)
            //////            {
            //////                Content2 = strContent.Substring(1300);
            //////            }
            //////            else
            //////            {
            //////                Content2 = strContent.Substring(1300, 1300);
            //////                if (strContent.Length <= 3900)
            //////                {
            //////                    Content3 = strContent.Substring(2600);
            //////                }
            //////                else
            //////                {
            //////                    Content3 = strContent.Substring(2600, 1300);
            //////                    if (strContent.Length <= 5200)
            //////                    {
            //////                        Content4 = strContent.Substring(3900);
            //////                    }
            //////                    else
            //////                    {
            //////                        Content4 = strContent.Substring(3900,1300);
            //////                        if (strContent.Length <= 6500)
            //////                        {
            //////                            Content5 = strContent.Substring(5200);
            //////                        }
            //////                        else
            //////                        {
            //////                            Content5 = strContent.Substring(5200, 1300);
            //////                            if (strContent.Length <= 7800)
            //////                            {
            //////                                Content6 = strContent.Substring(6500);
            //////                            }
            //////                            else
            //////                            {
            //////                                Content6 = strContent.Substring(6500,1300);
            //////                                if (strContent.Length <= 9100)
            //////                                {
            //////                                    Content7 = strContent.Substring(7800);
            //////                                }
            //////                                else
            //////                                {
            //////                                    Content7 = strContent.Substring(7800, 1300);
            //////                                    if (strContent.Length <= 10400)
            //////                                    {
            //////                                        Content8 = strContent.Substring(9100);
            //////                                    }
            //////                                    else
            //////                                    {
            //////                                        Content8 = strContent.Substring(9100, 1300);
            //////                                        if (strContent.Length <= 11700)
            //////                                        {
            //////                                            Content9 = strContent.Substring(10400);
            //////                                        }
            //////                                        else
            //////                                        {
            //////                                            Content9 = strContent.Substring(10400, 1300);
            //////                                        }
            //////                                    }
            //////                                }
            //////                            }
            //////                        }
            //////                    }
            //////                }
            //////            }
            //////        }
                   
            //////        DataTable datPrint = new DataTable();
            //////        //DataRow[] datRow;
            //////        DataRow datRow1;
            //////        for (int i = 0; i < dtMap.Columns.Count; i++)
            //////        {
            //////            DataColumn datColum = new DataColumn(dtMap.Columns[i].ColumnName, dtMap.Columns[i].DataType);                        
            //////            datPrint.Columns.Add(datColum);
            //////        }
            //////        int f = 0;
            //////        while (f < dtMap.Rows.Count)
            //////        {
            //////                //datRow = dtMap.Rows[0]; 
            //////                datRow1 = datPrint.NewRow();
            //////                for (int j = 0; j < dtMap.Columns.Count; j++)
            //////                {
            //////                    if (dtMap.Columns[j].ColumnName.ToString().Trim()== "CONTENT")
            //////                    {
            //////                        datRow1[j] = strContent;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT1")
            //////                    {
            //////                        datRow1[j] = Content1;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT2")
            //////                    {
            //////                        datRow1[j] = Content2;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT3")
            //////                    {
            //////                        datRow1[j] = Content3;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT4")
            //////                    {
            //////                        datRow1[j] = Content4;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT5")
            //////                    {
            //////                        datRow1[j] = Content5;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT6")
            //////                    {
            //////                        datRow1[j] = Content6;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT7")
            //////                    {
            //////                        datRow1[j] = Content7;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT8")
            //////                    {
            //////                        datRow1[j] = Content8;
            //////                    }
            //////                    else if (dtMap.Columns[j].ColumnName.ToString().Trim() == "CONTENT9")
            //////                    {
            //////                        datRow1[j] = Content9;
            //////                    }
            //////                    else
            //////                    {
            //////                        datRow1[j] = dtMap.Rows[0][j] ;
            //////                    }
            //////                }
            //////                datPrint.Rows.Add(datRow1);                                               
            //////                f = f + 1;
            //////        }
            //////        frmPrint.HMdat = datPrint;
            //////        frmPrint.ShowDialog();
            //////    }                
            //////}   
            #endregion
        }

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {               
                this.Close();
            }
        }

        private void frmSwiftMsgInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iClose == false)
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                    {
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all SWIFT Message Information windows ?", Common.sCaption);
                        if (e.Cancel == true)
                        {
                            iClose = false;
                            bIsCloseForm = false;
                        }
                        else
                        {
                            bIsCloseForm = true;
                        }
                    }                   
                }
                else
                {
                    bIsCloseForm = false;
                }
            }
        }

        private void frmSwiftMsgInfo_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                iClose = true;
                this.Close();
                iClose = false;
            }
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    if ((this.ActiveControl as Button).Name == "cmdPrint")
                    {
                        cmdPrint.Focus();
                        cmdPrint_Click(null, null);
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }
        //DatHM
        //purpose : Lay du lieu phuc vu cho viec in map field truong dien giai dien swift
        private void Getcontent_Print()
        {
            try
            {
                DataTable datContent = new DataTable();
                string strTable_name = "SWIFT_MSG_CONTENT";
                datContent = objControlContent.swift_print_map(strQUERY_ID.Trim(), strTable_name);
                if (datContent == null || datContent.Rows.Count == 0)
                {
                    dtMap = null;
                }
                else
                {
                    /////////////////////////////*/
                    //Cap nhat trang thai in dien                    
                    objContent.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                    objContent.PRINT_STS = 1;
                    objControlContent.Update_Print_STS(objContent);
                    //////////////////////////////
                    dtMap = datContent;
                    strMapField = strTable_name;
                }
                if (datContent.Rows.Count == 0)
                {
                    DataTable datContent1 = new DataTable();
                    string strTable_name1 = "SWIFT_MSG_ALL";
                    datContent1 = objControlContent.swift_print_map(strQUERY_ID.Trim(), strTable_name1);
                    if (datContent1 == null || datContent1.Rows.Count == 0)
                    {
                        dtMap = null;
                    }
                    else
                    {
                        dtMap = datContent1;
                        strMapField = strTable_name1;
                    }
                    if (datContent1.Rows.Count == 0)
                    {
                        DataTable datContent2 = new DataTable();
                        string strTable_name2 = "SWIFT_MSG_ALL_HIS";
                        datContent2 = objControlContent.swift_print_map(strQUERY_ID.Trim(), strTable_name2);
                        if (datContent2 == null || datContent2.Rows.Count == 0)
                        {
                            dtMap = null;
                        }
                        else
                        {
                            dtMap = datContent2;
                            strMapField = strTable_name2;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //purpose: Phuc vu in dien RM
        private void GetData_Print(string strDepartment)
        {
            try
            {
                DataTable datContent = new DataTable();                
                datContent = objControlContent.swift_print_03(strMSG_ID,strMsg_type,
                    Common.strFullName, strDepartment);
                    //strMSG_DIRECTION, strDepartment);
                if (datContent == null || datContent.Rows.Count == 0)
                {
                    dtMap = null;
                }
                else
                {
                    /////////////////////////////*/
                    //Cap nhat trang thai in dien                    
                    objContent.QUERY_ID = Convert.ToInt32(strQUERY_ID.Trim());
                    objContent.PRINT_STS = 1;
                    objControlContent.Update_Print_STS(objContent);
                    //////////////////////////////
                    dtMap = datContent;                    
                }                
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //End DatHM
        private void frmSwiftMsgInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftMsgInfo_MouseDown(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void tabInfomation_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (tabInfomation.SelectedIndex == 0)
                {
                   cmdPrint.Show();                   
                }
                else
                {
                    cmdPrint.Hide();                   
                    cmdClose.Show();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

    }
}
