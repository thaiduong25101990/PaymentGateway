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
    public partial class frmSwiftINMsgManual : Form
    {
        #region dinh nghia cac datatable && dataset
        private DataSet _dsAll_code;
        private DataTable _dtSearch;
        private DataSet _dsMaster;
        private string Where_Master = "";
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private string pRows;//chuoi chua index cua cac hang
        private string pTable_name;//chua cac table vd 1 la bang content,2 la bang all
        private string pProcess;//chua cac gia tri Process
        private string pMsg_id;//chua cac gia tri Msg_id
        private string strRows_two;
        //--dung cho truong hop co ca hai quyen ----------------------------------------------------
        private string pRows_two;
        private string pTable_name_two;
        private string pProcess_two;
        private string pMsg_id_two;
        private DataTable _dtViews;
        private string strRows;
        private DataTable datExcel = new DataTable();
        private DataTable datExcel1 = new DataTable();
        private static System.Data.DataTable _dtSearchAV;
        #endregion 
        
        #region dinh nghia cac lop trong bussiness
        SWIFT_PROCESS_HANDERController objHander = new SWIFT_PROCESS_HANDERController();
        public SWIFT_MSG_CONTENTInfo objcontent = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objControlContent = new SWIFT_MSG_CONTENTController();
        //----------------------------------------------------------------------------------
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objAllcodeControl = new ALLCODEController();
        //----------------------------------------------------------------------------------
        GROUPSInfo objInfoGroup = new GROUPSInfo();
        GROUPSController objControlGroup = new GROUPSController();
        //----------------------------------------------------------------------------------
        SEARCHInfo objSearch = new SEARCHInfo();
        SEARCHController objsearchcontrol = new SEARCHController();
        //----------------------------------------------------------------------------------
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        //----------------------------------------------------------------------------------
        SWIFTPRINTController objSwiftPrint = new SWIFTPRINTController();
        SWIFTPRINTInfo SwiftPrintInfo = new SWIFTPRINTInfo();
        //----------------------------------------------------------------------------------
        SYSVARController objsysvar = new SYSVARController();
        SYSVARInfo objInfo = new SYSVARInfo();
        //----------------------------------------------------------------------------------        
        SWIFT_MSG_LOGInfo objSWIFT_log = new SWIFT_MSG_LOGInfo();
        SWIFT_MSG_LOGController objcontrolSWIFT_log = new SWIFT_MSG_LOGController();
        //----------------------------------------------------------------------------------
        CURRENCYInfo objCurent = new CURRENCYInfo();
        CURRENCYController objCtrlcurrent = new CURRENCYController();
        clsCheckInput clsCheck = new clsCheckInput();
        //----------------------------------------------------------------------------------
        USERSInfo objUser = new USERSInfo();
        USERSController objctrlUser = new USERSController();
        //----------------------------------------------------------------------------------
        SWIFT_MSG_CONTENTController objSwiftMsgController = new SWIFT_MSG_CONTENTController();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        //----------------------------------------------------------------------------------
        USERSInfo ObjUser = new USERSInfo();
        USERSController ObjCtrlUser = new USERSController();
        #endregion 

        #region dinh nghia cac bien       
        public static int iSearch;
        public int iTime;
        private int isSupervisor;
        public static bool isSupervisorAndTeller = false;
        private int iSelect;
        public string STATEMENT_ID;
        public string strMsg_type;
        public int STATEMENT;
        public int QUERY;
        public int iRows;
        private int iRole = 0;
        private string strUserName = Common.Userid;
        private string STATETEE;
        private int iRows_count;
        private int pTeller_Supp;
        private int iSTATUS;
        private int Groups_Sucess;
        private int Groups_Error;
        private int iSelect_rows;
        private int iOK_STATUS;        
        private string pRow_select;
        private int iCount = 0;
        private int iCount1;        
        #endregion

        public frmSwiftINMsgManual()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Delete_process_hander();
            this.Close();
        }

        private void Delete_SWIFT_PROCESS_HANDER()
        {
            try
            {
                objHander.DELETE_SWIFT_PROCESS_HANDER_MANUAL(Common.Userid);
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
                //goi ham de xoa ban log khi xu ly thu cong trong bang SWIFT_PROCESS_
                Delete_SWIFT_PROCESS_HANDER();
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
                                        frmSWIFT.strMsg_type = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString();
                                        frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[f].Cells["MSG_DIRECTION"].Value.ToString();
                                        frmSWIFT._Processsts = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString(); 
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
                            frmSWIFT.strMsg_type = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                            frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
                            frmSWIFT._Processsts = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString(); 
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

        private void cmdStatement_Click(object sender, EventArgs e)
        {            
            //Xu ly thu cong kieu moi bang cach goi ham package
            try
            {
                frmPrint frmPrinter = new frmPrint();
                DataTable dtPrint_statement = new DataTable();
                frmPrinter.PrintType = "SWIFT_03";

                if (Common.strSTATUS_IN == "Manual")//xu ly thu cong trong truong hop manual
                {
                    dtPrint_statement = objSwiftPrint.PRINT_STATEMENT_MANUAL(strUserName);
                }
                else if(Common.strSTATUS_IN == "Resend")//xu ly thu cong trong truong hop Resend
                {
                    dtPrint_statement = objSwiftPrint.PRINT_STATEMENT_RESEND(strUserName);
                }
                if (dtPrint_statement.Rows.Count > 0)
                {
                    frmPrinter.WindowState = FormWindowState.Maximized;
                    frmPrinter.HMdat = dtPrint_statement;
                    frmPrinter.ShowDialog();
                }
                else
                {                    
                    Common.ShowError("There isn't valid messages status to Statement!", 3, MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }       
        

        private void Process()
        {
            try
            {
                frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                int f = 0;
                while (f < dataMessage.Rows.Count)
                {                    
                    if (dataMessage.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            if (dataMessage.Rows[f].Cells["Tabl"].Value.ToString() == "1")
                            { frmSMsgManualIn.strTable_name = "SWIFT_MSG_CONTENT"; }
                            else if (dataMessage.Rows[f].Cells["Tabl"].Value.ToString() == "2")
                            { frmSMsgManualIn.strTable_name = "SWIFT_MSG_ALL"; }
                            else if (dataMessage.Rows[f].Cells["Tabl"].Value.ToString() == "3")
                            { frmSMsgManualIn.strTable_name = "SWIFT_MSG_ALL_HIS"; }                            
                            frmSMsgManualIn.strMsg_id = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                            frmSMsgManualIn.strQUERY_ID = dataMessage.Rows[f].Cells["QUERY_ID"].Value.ToString();
                            frmSMsgManualIn.strSender = dataMessage.Rows[f].Cells["BRANCH_A"].Value.ToString();
                            frmSMsgManualIn.strReceiving = dataMessage.Rows[f].Cells["BRANCH_B"].Value.ToString();
                            frmSMsgManualIn.strAmount = dataMessage.Rows[f].Cells["AMOUNT"].Value.ToString();
                            frmSMsgManualIn.strF20 = dataMessage.Rows[f].Cells["FIELD20"].Value.ToString();
                            frmSMsgManualIn.strTrans_date = dataMessage.Rows[f].Cells["TRANS_DATE"].Value.ToString();
                            frmSMsgManualIn.strRM_NUMBER = dataMessage.Rows[f].Cells["RM_NUMBER"].Value.ToString();
                            frmSMsgManualIn.strDepartment = dataMessage.Rows[f].Cells["DEPARTMENT"].Value.ToString();                            
                            frmSMsgManualIn.strSWMSTS = dataMessage.Rows[f].Cells["SWMSTS"].Value.ToString();
                            frmSMsgManualIn.strPROCESSSTS = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString();
                            frmSMsgManualIn.strCCYCD = dataMessage.Rows[f].Cells["CCYCD"].Value.ToString();                            
                            frmSMsgManualIn.strMsg_type = dataMessage.Rows[f].Cells["MSG_TYPE"].Value.ToString();                            
                            frmSMsgManualIn.strSTATUS = dataMessage.Rows[f].Cells["STATUS"].Value.ToString();
                            frmSMsgManualIn.strMSG_SRC = dataMessage.Rows[f].Cells["MSG_SRC"].Value.ToString();
                            frmSMsgManualIn.strMSG_DIRECTION = dataMessage.Rows[f].Cells["MSG_DIRECTION"].Value.ToString();
                            this.Hide();
                            frmSMsgManualIn.ShowDialog(this);
                        }
                    }
                    if (Common.iOk == 1)
                    {  dataMessage.Rows.RemoveAt(f);                    
                    Common.iOk = 0;
                    this.Show();
                    }
                    else if (frmSMsgManualIn.bIsCloseForm == true)
                    {   this.Show(); Refresh_data(); return; }
                    else if (Common.iCancel == 0)
                    {
                        if (dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAITING)
                        {
                            //objControlContent.DELETE_PROCESS_HANDER("," + dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString() + ",");
                        }
                        this.Show(); dataMessage.Rows[f].Cells[0].Value = null; f = f + 1;
                    }
                    else
                    {
                        if (iRows_count == dataMessage.Rows.Count)
                        {                           
                            f = f + 1;
                        }
                        iRows_count = dataMessage.Rows.Count;
                    }
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
                frmSwiftMsgManualInfo frmSMsgManualIn = new frmSwiftMsgManualInfo();
                if (dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString() == "1")
                { frmSMsgManualIn.strTable_name = "SWIFT_MSG_CONTENT"; }
                else if (dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString() == "2")
                { frmSMsgManualIn.strTable_name = "SWIFT_MSG_ALL"; }
                else if (dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString() == "3")
                { frmSMsgManualIn.strTable_name = "SWIFT_MSG_ALL_HIS"; } 
                
                frmSMsgManualIn.strMsg_id = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                frmSMsgManualIn.strQUERY_ID = dataMessage.Rows[iRows].Cells["QUERY_ID"].Value.ToString();
                frmSMsgManualIn.strSender = dataMessage.Rows[iRows].Cells["BRANCH_A"].Value.ToString();
                frmSMsgManualIn.strReceiving = dataMessage.Rows[iRows].Cells["BRANCH_B"].Value.ToString();
                frmSMsgManualIn.strAmount = dataMessage.Rows[iRows].Cells["AMOUNT"].Value.ToString();
                frmSMsgManualIn.strF20 = dataMessage.Rows[iRows].Cells["FIELD20"].Value.ToString();
                frmSMsgManualIn.strTrans_date = dataMessage.Rows[iRows].Cells["TRANS_DATE"].Value.ToString();
                frmSMsgManualIn.strRM_NUMBER = dataMessage.Rows[iRows].Cells["RM_NUMBER"].Value.ToString();
                frmSMsgManualIn.strDepartment = dataMessage.Rows[iRows].Cells["DEPARTMENT"].Value.ToString();
                frmSMsgManualIn.strSWMSTS = dataMessage.Rows[iRows].Cells["SWMSTS"].Value.ToString();
                frmSMsgManualIn.strPROCESSSTS = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                frmSMsgManualIn.strCCYCD = dataMessage.Rows[iRows].Cells["CCYCD"].Value.ToString();
                frmSMsgManualIn.strMsg_type = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                frmSMsgManualIn.strSTATUS = dataMessage.Rows[iRows].Cells["STATUS"].Value.ToString();
                frmSMsgManualIn.strMSG_SRC = dataMessage.Rows[iRows].Cells["MSG_SRC"].Value.ToString();
                frmSMsgManualIn.strMSG_DIRECTION = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
                
                this.Hide();
                frmSMsgManualIn.ShowDialog(this);
                if (Common.iOk == 1)
                {
                    dataMessage.Rows.RemoveAt(iRows);                    
                    Common.iOk = 0;
                }
                if (frmSMsgManualIn.bIsCloseForm == true )
                {
                    if (dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAITING)
                    {
                        objControlContent.DELETE_PROCESS_HANDER("," + dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString() + ",");
                    }
                }                
                this.Show();
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


        //Refresh ve null voi cac dong chua duoc xu ly
        private void Refresh_data()
        {
            try
            {
                int iMessage=0;
                string Msg_id = "";
                //string Message = "";
                bool _bool = false;
                String[] M = strRows.Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
                int kCount = M.Count<String>();
                if (kCount == 1)
                {
                    if (M[0].Trim() == "")
                    { iMessage = 0; }
                    else { iMessage = kCount; }
                }
                else { iMessage = kCount; }
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
                    Common.ShowError(iMessage + ":message is being processed by other user!" + "\r\n" + "You are refresh data", 3, MessageBoxButtons.OK);                    
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
                pTable_name = "";//chua cac table vd 1 la bang content,2 la bang all
                pProcess = "";//chua cac gia tri Process
                pMsg_id = "";
                pRows_two = "";//chuoi chua index cua cac hang
                pTable_name_two = "";//chua cac table vd 1 la bang content,2 la bang all
                pProcess_two = "";//chua cac gia tri Process
                pMsg_id_two = "";
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
                                string strTable = dataMessage.Rows[f].Cells["Tabl"].Value.ToString();
                                string strMsgid = dataMessage.Rows[f].Cells["MSG_ID"].Value.ToString();
                                string strProcess = dataMessage.Rows[f].Cells["PROCESSSTS"].Value.ToString();
                                #region supper
                                if (strProcess == Common.PROCESSSTS_WAPPROVING || strProcess == Common.PROCESSSTS_WAPPROVING_RESENT)
                                {
                                    if (pMsg_id_two.Trim() == "")
                                    { pMsg_id_two = strMsgid; }
                                    else { pMsg_id_two = pMsg_id_two + "," + strMsgid; }

                                    if (pProcess_two.Trim() == "")
                                    { pProcess_two = strProcess; }
                                    else { pProcess_two = pProcess_two + "," + strProcess; }

                                    if (pTable_name_two.Trim() == "")
                                    { pTable_name_two = strTable; }
                                    else { pTable_name_two = pTable_name_two + "," + strTable; }

                                    if (pRows_two.Trim() == "")
                                    { pRows_two = Convert.ToString(f); }
                                    else { pRows_two = pRows_two + "," + Convert.ToString(f); }
                                }
                                #endregion
                                #region Teller
                                else if (strProcess == Common.PROCESSSTS_WAITING || strProcess == Common.PROCESSSTS_AUTO || strProcess == Common.PROCESSSTS_APPROVED)
                                {
                                    if (pMsg_id.Trim() == "")
                                    { pMsg_id = strMsgid; }
                                    else { pMsg_id = pMsg_id + "," + strMsgid; }

                                    if (pProcess.Trim() == "")
                                    { pProcess = strProcess; }
                                    else { pProcess = pProcess + "," + strProcess; }

                                    if (pTable_name.Trim() == "")
                                    { pTable_name = strTable; }
                                    else { pTable_name = pTable_name + "," + strTable; }

                                    if (pRows.Trim() == "")
                                    { pRows = Convert.ToString(f); }
                                    else { pRows = pRows + "," + Convert.ToString(f); }
                                }
                                #endregion
                            }
                        }
                        f = f + 1;
                    }
                    DataTable _dtmessage = objControlContent.PROCESS_HANDICRAFT(pProcess, pMsg_id, pTable_name, pRows,Common.Userid, out _dtmessage);
                    if (_dtmessage == null)
                    { strRows = ""; }
                    else if (_dtmessage != null)
                    {strRows = _dtmessage.Rows[0]["ROWS_SELECT"].ToString();}
                    //--------------------------------------------------------------------------------------------------------------------------------
                    DataTable _dtmessage_two = objControlContent.PROCESS_HANDICRAFT_SUPPER(pProcess_two, pMsg_id_two, pTable_name_two, pRows_two, Common.Userid, out _dtmessage);
                    if (_dtmessage_two == null)
                    { strRows_two = ""; }
                    else if (_dtmessage_two != null)
                    { strRows_two = _dtmessage.Rows[0]["ROWS_SELECT"].ToString();  }
                    if (strRows == "" && strRows_two == "")
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
                        if (dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAITING || dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAPPROVING_RESENT)
                        {
                            pTable_name_two = dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString();
                            pMsg_id_two = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                            pProcess_two = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                            pRows_two = Convert.ToString(iRows);
                        }
                        else if (dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAITING || dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_AUTO || dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_APPROVED)
                        {
                            pTable_name = dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString();
                            pMsg_id = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                            pProcess = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                            pRows = Convert.ToString(iRows);
                        }
                        DataTable _dtmessage = objControlContent.PROCESS_HANDICRAFT(pProcess, pMsg_id, pTable_name, pRows, Common.Userid, out _dtmessage);
                        if (_dtmessage == null)
                        { strRows = ""; }
                        else if (_dtmessage != null)
                        { strRows = _dtmessage.Rows[0]["ROWS_SELECT"].ToString(); }
                        //--------------------------------------------------------------------------------------------------------------------------------
                        DataTable _dtmessage_two = objControlContent.PROCESS_HANDICRAFT_SUPPER(pProcess_two, pMsg_id_two, pTable_name_two, pRows_two, Common.Userid, out _dtmessage);
                        if (_dtmessage_two == null)
                        { strRows_two = ""; }
                        else if (_dtmessage_two != null)
                        { strRows_two = _dtmessage.Rows[0]["ROWS_SELECT"].ToString(); }
                        if (strRows != "" || strRows_two != "")//co dien da duoc xu ly hay da duoc log roi
                        {
                            Common.ShowError(1 + ":message is being processed by other user!" + "\r\n" + "You are refresh data", 3, MessageBoxButtons.OK);
                           
                        }
                        else
                        { Process_one(iRows); }
                    }
                    else
                    {
                        if (dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAPPROVING || dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAPPROVING_RESENT)
                        {
                            pTable_name_two = dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString();
                            pMsg_id_two = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                            pProcess_two = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                            pRows_two = Convert.ToString(iRows);
                        }
                        else if (dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_WAITING || dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_AUTO || dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString() == Common.PROCESSSTS_APPROVED)
                        {
                            pTable_name = dataMessage.Rows[iRows].Cells["Tabl"].Value.ToString();
                            pMsg_id = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                            pProcess = dataMessage.Rows[iRows].Cells["PROCESSSTS"].Value.ToString();
                            pRows = Convert.ToString(iRows);
                        }
                        DataTable _dtmessage = objControlContent.PROCESS_HANDICRAFT(pProcess, pMsg_id, pTable_name, pRows, Common.Userid, out _dtmessage);
                        if (_dtmessage == null)
                        { strRows = ""; }
                        else if (_dtmessage != null)
                        { strRows = _dtmessage.Rows[0]["ROWS_SELECT"].ToString(); }
                        //--------------------------------------------------------------------------------------------------------------------------------
                        DataTable _dtmessage_two = objControlContent.PROCESS_HANDICRAFT_SUPPER(pProcess_two, pMsg_id_two, pTable_name_two, pRows_two, Common.Userid, out _dtmessage);
                        if (_dtmessage_two == null)
                        { strRows_two = ""; }
                        else if (_dtmessage_two != null)
                        { strRows_two = _dtmessage.Rows[0]["ROWS_SELECT"].ToString(); }
                        if (strRows != "" || strRows_two != "")//co dien da duoc xu ly hay da duoc log roi
                        {
                            Common.ShowError(1 + ":message is being processed by other user!" + "\r\n" + "You are refresh data", 3, MessageBoxButtons.OK);
                             }
                        else
                        { Process_one(iRows); }
                    }
                }
                //----------------------------------------------------------------------               
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            //Lbtong.Text = Convert.ToString(dataMessage.Rows.Count);
            label12.Text = "Total number of messages : " + Convert.ToString(dataMessage.Rows.Count);
            if (dataMessage.Rows.Count == 0)
            {                
                cmdview.Enabled = false;
                cmdProcess.Enabled = false;
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
                Format_advance();                
                Search_data();
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
                //if (isSupervisor == 3) { Where_Master = "  trim(AUTO)='N'  and  Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_WAITING + "','" + Common.PROCESSSTS_WAPPROVING + "')  and C.MSG_ID = H.MSG_ID(+) and MSG_DIRECTION = 'SWIFT-SIBS' and C.MSG_ID not in (select MSG_ID from SWIFT_PROCESS_HANDER where NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING + "')"; }
                string Where = "";
                FROM_TO_DATE();               
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
                if (iSearch == 1)//tim kiem nang cao
                {
                    _dateFrom = date1.Value; _dateTo = dateto.Value;
                    Where = clsDatagridviews.SQL_ADVANCE(datDieukien);
                    clsDatagridviews.SQL_ADVANCE(datDieukien);
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    if (Where.Trim() != "")
                    {
                        if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien,lay tai 3 bang
                        {
                            if (Where.Trim() == "")
                            { dataMessage = clsDatagridviews.SEARCH_DATA_RESEND(_dateFrom, _dateTo, "Where " + Where_Master, isSupervisor, dataMessage); }//Lay du lieu theo dieu kien where  
                            else if (Where.Trim() != "")
                            { 
                                dataMessage = clsDatagridviews.SEARCH_DATA_RESEND(_dateFrom, _dateTo, Where + " and " + Where_Master, isSupervisor, dataMessage); 
                            }
                        }
                        else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong, chi lay tai bang content
                        {
                            if (Where.Trim() == "")
                            {
                                dataMessage = clsDatagridviews.MESSAGE_CONTENT_INSWARD_SEARCH(_dateFrom, _dateTo, " Where  " + Where_Master,isSupervisor, dataMessage);
                            }
                            else if (Where.Trim() != "")
                            { dataMessage = clsDatagridviews.MESSAGE_CONTENT_INSWARD_SEARCH(_dateFrom, _dateTo, Where + " and  " + Where_Master, isSupervisor, dataMessage); }
                        }
                    }
                }
                else//tim kiem thong thuong
                {
                    Where = clsDatagridviews.Search_Normal_manual(grbSearch, "BRANCH_B", isSupervisor);//ham tra ra menh de where
                    dataMessage.DataSource = 0;
                    label12.Text = "Total number of messages : 0";
                    if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien,lay tai 3 bang
                    {
                        if (Where.Trim() == "")
                        { dataMessage = clsDatagridviews.SEARCH_DATA_RESEND(_dateFrom, _dateTo, "Where " + Where_Master, isSupervisor, dataMessage); }//Lay du lieu theo dieu kien where  
                        else if (Where.Trim() != "")
                        { dataMessage = clsDatagridviews.SEARCH_DATA_RESEND(_dateFrom, _dateTo, Where + " and " + Where_Master, isSupervisor, dataMessage); }
                    }
                    else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong, chi lay tai bang content
                    {
                        if (Where.Trim() == "")
                        {
                            dataMessage = clsDatagridviews.MESSAGE_CONTENT_INSWARD_SEARCH(_dateFrom, _dateTo, " Where  " + Where_Master, isSupervisor, dataMessage);
                        }
                        else if (Where.Trim() != "")
                        { dataMessage = clsDatagridviews.MESSAGE_CONTENT_INSWARD_SEARCH(_dateFrom, _dateTo, Where + " and  " + Where_Master, isSupervisor, dataMessage); }
                    }
                }               
                
                label12.Text = "Total number of messages : " + Convert.ToString(dataMessage.Rows.Count);
                Enable_Controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Enable_Controls()
        {
            try
            {
                if (dataMessage.Rows.Count == 0)
                {
                    cmdExport.Enabled = false; cmdProcess.Enabled = false;
                    cmdApp.Enabled = false; //cmdStatement.Enabled = false;
                    cmdview.Enabled = false;
                }
                else
                {
                    cmdExport.Enabled = true; cmdProcess.Enabled = true;
                    cmdApp.Enabled = true; //cmdStatement.Enabled = true;
                    cmdview.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void Edit_Columns()
        {
            try
            {
                dataMessage.Columns[0].Width = 28;
                dataMessage.Columns["MSG_TYPE"].Width = 73;
                dataMessage.Columns["BRANCH_A"].Width = 110;
                dataMessage.Columns["BRANCH_B"].Width = 110;
                dataMessage.Columns["TRANS_DATE"].Width = 90;
                //dataMessage.Columns["VALUE_DATE"].Width = 90;
                dataMessage.Columns["DEPARTMENT"].Width = 60;
                dataMessage.Columns["FIELD20"].Width = 132;
                dataMessage.Columns["FIELD21"].Width = 132;
                dataMessage.Columns["RECEIVING_TIME"].Width = 120;
                dataMessage.Columns["SENDING_TIME"].Width = 125;
                dataMessage.Columns["CCYCD"].Width = 50;
                dataMessage.Columns["AUTO"].Width = 45;
                dataMessage.Columns["RM_NUMBER"].Width = 110;
                dataMessage.Columns["AMOUNT"].Width = 130;
                //dataMessage.Columns["BRANCH_B"].Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void frmSwiftINMsgManual_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                CheckIsSupervisor();//check quyen user dang nhap
                Locate_controls();
                FROM_TO_DATE();              
                Get_Status();//lay toan bo cac trang thai cua form
                Load_data();
                Resend_Manual();//goi ham check xem la xu ly thu cong hay resen
                BR.BRLib.FomatGrid.Color_datagrid(dataMessage);
                /*Sua theoo yeu cau cua msb*/
                if (Common.strSTATUS_IN == "Resend")
                {
                    cmdApp.Visible = false;
                    cmdStatement.Visible = true;
                    //cmdStatement.Visible = false;
                }
                Delete_process_hander();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //objHander
        private void Delete_process_hander()
        {
            try
            {
                string pPROCESS="";
                if (Common.strSTATUS_IN == "Resend")
                {
                    pPROCESS = "88";
                }
                else
                {
                    pPROCESS = "99";
                }
                objHander.DELETE_SWIFT_PROCESS_HANDERT(Common.Userid, pPROCESS);
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
                ALLCODEController objStatus = new ALLCODEController();
                DataTable _dtProcess = new DataTable();
                DataTable _dtStatus = new DataTable();
                /*Lam phan nay de cho co the resend lai duoc nhung dien loi*/
                if (Common.strSTATUS_IN == "Resend")
                {
                    _dtStatus = objStatus.RESEND_STATUS("Where STATUS in (" + Common.STATUS_SENT + "," + Common.STATUS_ERROR + ")", out _dtStatus);
                    cbStatus.DataSource = _dtStatus;
                    cbStatus.DisplayMember = "CONTENT";
                    cbStatus.ValueMember = "CDVAL";
                    cbStatus.SelectedValue = Common.STATUS_SENT;
                }
                /*88888888888888888888888888888888888888888888888888888888*/
                #region quyen Teller
                if (isSupervisor == 1)
                {                                  
                    if (Common.strSTATUS_IN == "Resend")
                    {                       
                        /*Xu ly mot tay*/
                        cbMsg_status.SelectedValue = Common.SWMSTS_NORMAL;
                        cbMsg_status.Enabled = false;
                        _dtProcess = objStatus.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_AUTO + "," + Common.PROCESSSTS_APPROVED + ")", out _dtProcess);                  
                        DataRow _dtr1 = _dtProcess.NewRow();
                        _dtr1[0] = ""; _dtr1[1] = "ALL";
                        _dtProcess.Rows.InsertAt(_dtr1, 0);
                       
                        cbProcess_Status.DataSource = _dtProcess;
                        cbProcess_Status.DisplayMember = "CONTENT";
                        cbProcess_Status.ValueMember = "CDVAL";
                        cbProcess_Status.SelectedIndex = 0;                       
                        //cbStatus.SelectedValue = Common.STATUS_SENT;
                        //cbStatus.Enabled = false;
                        
                    }                    
                    else if (Common.strSTATUS_IN == "Manual")
                    {
                        cbStatus.SelectedValue = Common.STATUS_CONVERTED;                       
                        cbStatus.Enabled = false;                                             
                        cbProcess_Status.DataSource = _dsAll_code.Tables["PROCESSSTS"];
                        cbProcess_Status.DisplayMember = "CONTENT";
                        cbProcess_Status.ValueMember = "CDVAL";
                        cbProcess_Status.SelectedValue = Common.PROCESSSTS_WAITING;
                        cbProcess_Status.Enabled = false;
                      
                    }
                }
                #endregion
                #region Supper
                if (isSupervisor == 2)
                {                   
                    if (Common.strSTATUS_IN == "Resend")
                    {
                        #region Comment
                        //cbStatus.SelectedValue = Common.STATUS_SENT;//SENT
                        //cbStatus.Enabled = false;                        
                        //cbMsg_status.SelectedValue = Common.SWMSTS_NORMAL;//NORMAL
                        //cbMsg_status.Enabled = false;  
                        //cbProcess_Status.DataSource = _dsAll_code.Tables["PROCESSSTS"];
                        //cbProcess_Status.DisplayMember = "CONTENT";
                        //cbProcess_Status.ValueMember = "CDVAL";
                        //cbProcess_Status.SelectedValue = Common.PROCESSSTS_WAPPROVING_RESENT;//WAPPROVING-RESENT
                        //cbProcess_Status.Enabled = false;
                        #endregion
                        /*Xu ly mot tay*/
                        cbMsg_status.SelectedValue = Common.SWMSTS_NORMAL;
                        cbMsg_status.Enabled = false;
                        _dtProcess = objStatus.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_AUTO + "," + Common.PROCESSSTS_APPROVED + ")", out _dtProcess);
                        DataRow _dtr1 = _dtProcess.NewRow();
                        _dtr1[0] = ""; _dtr1[1] = "ALL";
                        _dtProcess.Rows.InsertAt(_dtr1, 0);

                        cbProcess_Status.DataSource = _dtProcess;
                        cbProcess_Status.DisplayMember = "CONTENT";
                        cbProcess_Status.ValueMember = "CDVAL";
                        cbProcess_Status.SelectedIndex = 0;
                        //cbStatus.SelectedValue = Common.STATUS_SENT;
                        //cbStatus.Enabled = false;
                    }
                    else if (Common.strSTATUS_IN == "Manual")
                    {
                        #region comment
                        //cbStatus.SelectedValue = Common.STATUS_CONVERTED;//CONVERTED
                        //cbStatus.Enabled = false;                        
                        //cbMsg_status.Text = "ALL";  
                        //cbProcess_Status.DataSource = _dsAll_code.Tables["PROCESSSTS"];
                        //cbProcess_Status.DisplayMember = "CONTENT";
                        //cbProcess_Status.ValueMember = "CDVAL";
                        //cbProcess_Status.SelectedValue = Common.PROCESSSTS_WAPPROVING;//WAPPROVING;
                        //cbProcess_Status.Enabled = false;  
                        #endregion

                        cbStatus.SelectedValue = Common.STATUS_CONVERTED;                       
                        cbStatus.Enabled = false;                                           
                        cbProcess_Status.DataSource = _dsAll_code.Tables["PROCESSSTS"];
                        cbProcess_Status.DisplayMember = "CONTENT";
                        cbProcess_Status.ValueMember = "CDVAL";
                        cbProcess_Status.SelectedValue = Common.PROCESSSTS_WAPPROVING;
                        cbProcess_Status.Enabled = false;
                    }                    
                }
                #endregion
                #region quyen Teller hay la quyen cua Teller_Suppervisor
                if (isSupervisor == 3)
                {                    
                    if (Common.strSTATUS_IN == "Resend")
                    {
                        #region comment
                        //cbStatus.SelectedValue = Common.STATUS_SENT;
                        //cbStatus.Enabled = false;
                        ////---------------------------------------------------
                        //cbMsg_status.SelectedValue = Common.SWMSTS_NORMAL; //NORMAL
                        //cbMsg_status.Enabled = false;      
                        ////PROCESSSTS-------------------------------------------
                        //_dtProcess = objStatus.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_AUTO + "," + Common.PROCESSSTS_APPROVED + "," + Common.PROCESSSTS_WAPPROVING_RESENT + ")", out _dtProcess);  
                        ////---------------------------------------------
                        //DataRow _dtf1 = _dtProcess.NewRow();
                        //_dtf1[0] = ""; _dtf1[1] = "ALL";
                        //_dtProcess.Rows.InsertAt(_dtf1, 0);
                        ////----------------------------------------------                      
                        //cbProcess_Status.DataSource = _dtProcess;
                        //cbProcess_Status.DisplayMember = "CONTENT";
                        //cbProcess_Status.ValueMember = "CDVAL";
                        //cbProcess_Status.SelectedIndex = 0;
                        #endregion
                        /*Xu ly mot tay*/
                        cbMsg_status.SelectedValue = Common.SWMSTS_NORMAL;
                        cbMsg_status.Enabled = false;
                        _dtProcess = objStatus.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_AUTO + "," + Common.PROCESSSTS_APPROVED + ")", out _dtProcess);
                        DataRow _dtr1 = _dtProcess.NewRow();
                        _dtr1[0] = ""; _dtr1[1] = "ALL";
                        _dtProcess.Rows.InsertAt(_dtr1, 0);

                        cbProcess_Status.DataSource = _dtProcess;
                        cbProcess_Status.DisplayMember = "CONTENT";
                        cbProcess_Status.ValueMember = "CDVAL";
                        cbProcess_Status.SelectedIndex = 0;
                        //cbStatus.SelectedValue = Common.STATUS_SENT;
                        //cbStatus.Enabled = false;
                    }
                    else if (Common.strSTATUS_IN == "Manual")
                    {
                        cbStatus.SelectedValue = Common.STATUS_CONVERTED;
                        cbStatus.Enabled = false;                        
                        _dtProcess = objStatus.PROCESSTS_STATUS("Where CDVAL in (" + Common.PROCESSSTS_WAITING + "," + Common.PROCESSSTS_WAPPROVING + ")", out _dtProcess);                      
                        DataRow _dtT11 = _dtProcess.NewRow();
                        _dtT11[0] = ""; _dtT11[1] = "ALL";
                        _dtProcess.Rows.InsertAt(_dtT11, 0);                        
                        cbProcess_Status.DataSource = _dtProcess;
                        cbProcess_Status.DisplayMember = "CONTENT";
                        cbProcess_Status.ValueMember = "CDVAL";
                        cbProcess_Status.SelectedValue = Common.PROCESSSTS_WAPPROVING;
                        
                        #region comment
                        //cbStatus.SelectedValue = Common.STATUS_CONVERTED;                      
                        //cbStatus.Enabled = false;                                           
                        //cbProcess_Status.DataSource = _dsAll_code.Tables["PROCESSSTS"];
                        //cbProcess_Status.DisplayMember = "CONTENT";
                        //cbProcess_Status.ValueMember = "CDVAL";
                        //cbProcess_Status.SelectedValue = Common.PROCESSSTS_WAITING;
                        //cbProcess_Status.Enabled = false;
                        #endregion
                    }
                }
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
                if (Common.strSTATUS_IN == "Manual")
                {
                    DataRow _dtr3 = _dsAll_code.Tables["STATUS"].NewRow();
                    _dtr3[0] = 99; _dtr3[1] = "ALL";
                    _dsAll_code.Tables["STATUS"].Rows.InsertAt(_dtr3, 0);
                    cbStatus.DataSource = _dsAll_code.Tables["STATUS"];
                    cbStatus.DisplayMember = "NAME";
                    cbStatus.ValueMember = "STATUS";
                    cbStatus.Text = "ALL";
                }

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
        //ham lay du lieu len luoi
        private void Load_data()
        {
            try
            {
                /*Neu theo yeu cau cua msb thi dung bang swift_process_hen.. lam check khoa ma thoi*/
                /*Khi xu ly xong thi xoa ban ghi do di*/
                FROM_TO_DATE();
                if (isSupervisor == 1)//quyen Teller 
                {                                   
                    if (Common.strSTATUS_IN == "Resend")/*Mot tay*/
                    { 
                        //Where_Master = " Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_AUTO + "','" + Common.PROCESSSTS_APPROVED + "') and C.MSG_ID = H.MSG_ID(+)  and MSG_DIRECTION = 'SWIFT-SIBS' and STATUS = 1 and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER where (TELLER_ID = '" + Common.Userid + "' and NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING_RESENT + "') or TELLER_ID <> '" + Common.Userid + "' )";
                        Where_Master = " Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_AUTO + "','" + Common.PROCESSSTS_APPROVED + "') and C.MSG_ID = H.MSG_ID(+)  and MSG_DIRECTION = 'SWIFT-SIBS' and (STATUS = 1 or STATUS = -1) and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER )";//where (TELLER_ID = '" + Common.Userid + "' and NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING_RESENT + "') or TELLER_ID <> '" + Common.Userid + "' )";
                    }
                    else if (Common.strSTATUS_IN == "Manual")/*Hai tay voi dien excel con lai mot tay*/
                    { 
                        //Where_Master = " trim(AUTO)='N' and C.MSG_ID = H.MSG_ID(+) and PROCESSSTS = '" + Common.PROCESSSTS_WAITING + "' and MSG_DIRECTION = 'SWIFT-SIBS'  and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER where (TELLER_ID = '" + Common.Userid + "' and NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING + "') or TELLER_ID <> '" + Common.Userid + "' )"; 
                        Where_Master = " trim(AUTO)='N' and C.MSG_ID = H.MSG_ID(+) and PROCESSSTS = '" + Common.PROCESSSTS_WAITING + "' and MSG_DIRECTION = 'SWIFT-SIBS'  and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER) ";
                    }                    
                }
                if (isSupervisor == 2)//Suppervisor
                {                    
                    /*Neu theo yeu cau cua msb thi khong can xu ly hai ta doi voi dien ve ok*/
                    /*Supper khong load du lieu phai comment doan duoi vao*/
                    if (Common.strSTATUS_IN == "Resend")//goi form gui lai dien
                    {
                        Where_Master = " Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_AUTO + "','" + Common.PROCESSSTS_APPROVED + "') and C.MSG_ID = H.MSG_ID(+)  and MSG_DIRECTION = 'SWIFT-SIBS' and (STATUS = 1 or STATUS = -1) and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER )";
                        //Where_Master = "  C.MSG_ID = H.MSG_ID(+) and MSG_DIRECTION = 'SWIFT-SIBS' and  C.MSG_ID in (select MSG_ID from SWIFT_PROCESS_HANDER where NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING_RESENT + "' and Trim(Teller_Id) <> '" + Common.Userid.Trim() + "')";
                    }
                    else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                    { 
                        /*Theo yeu cau cua msb thi xu ly thu cong chi can xu ly mot tay thi supper van duoc xu ly*/
                        //Where_Master = " trim(AUTO)='N' and C.MSG_ID = H.MSG_ID(+) and PROCESSSTS = '" + Common.PROCESSSTS_WAITING + "' and MSG_DIRECTION = 'SWIFT-SIBS'  ";
                        /*Lay cho supper xu ly*/
                        //Where_Master = " trim(AUTO)='N' and STATUS = 0 and C.MSG_ID = H.MSG_ID(+) and MSG_DIRECTION = 'SWIFT-SIBS' and C.MSG_ID in (select MSG_ID from SWIFT_PROCESS_HANDER where NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING + "' and Trim(Teller_Id) <> '" + Common.Userid.Trim() + "')"; 
                        Where_Master = " trim(AUTO)='N' and STATUS = 0 and C.MSG_ID = H.MSG_ID(+) and MSG_DIRECTION = 'SWIFT-SIBS' and C.MSG_ID in (select MSG_ID from SWIFT_PROCESS_HANDER where NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING + "')";                          
                    }                                  
                }
                if (isSupervisor == 3)//quyen Teller and Suppervisor
                {
                    /*Neu theo yeu cau cua msb thi khong can xu ly hai ta doi voi dien ve ok*/
                    /*Supper khong load du lieu phai comment doan duoi vao*/
                    /*Lay du lieu nhu phan teller*/
                    if (Common.strSTATUS_IN == "Resend")//goi form gui lai dien
                    { 
                        //Where_Master = " STATUS = 1  and Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_APPROVED + "','" + Common.PROCESSSTS_WAPPROVING_RESENT + "','" + Common.PROCESSSTS_AUTO + "')  and C.MSG_ID = H.MSG_ID(+) and MSG_DIRECTION = 'SWIFT-SIBS' and C.MSG_ID not in (select MSG_ID from SWIFT_PROCESS_HANDER where ((NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING_RESENT + "' and TELLER_ID = '" + Common.Userid.Trim() + "') or (NEW_PROCESSSTS is null and TELLER_ID <>'" + Common.Userid.Trim() + "')))"; 
                        //Where_Master = " Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_AUTO + "','" + Common.PROCESSSTS_APPROVED + "') and C.MSG_ID = H.MSG_ID(+)  and MSG_DIRECTION = 'SWIFT-SIBS' and STATUS = 1 and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER )";
                        Where_Master = " Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_AUTO + "','" + Common.PROCESSSTS_APPROVED + "') and C.MSG_ID = H.MSG_ID(+)  and MSG_DIRECTION = 'SWIFT-SIBS' and (STATUS = 1 or STATUS = -1) and C.MSG_ID not in(select MSG_ID from SWIFT_PROCESS_HANDER )";
                    }
                    else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                    {
                        Where_Master = " trim(AUTO)='N' and C.MSG_ID = H.MSG_ID(+) and PROCESSSTS = '" + Common.PROCESSSTS_WAITING + "' and MSG_DIRECTION = 'SWIFT-SIBS'  ";
                        /*xu ly thu cong thi can mot tay len chi can xu ly mot tay*/
                        //Where_Master = "  trim(AUTO)='N'  and  Trim(PROCESSSTS) in ('" + Common.PROCESSSTS_WAITING + "','" + Common.PROCESSSTS_WAPPROVING + "')  and C.MSG_ID = H.MSG_ID(+) and MSG_DIRECTION = 'SWIFT-SIBS' and C.MSG_ID not in (select MSG_ID from SWIFT_PROCESS_HANDER where ((NEW_PROCESSSTS = '" + Common.PROCESSSTS_WAPPROVING + "' and TELLER_ID = '" + Common.Userid.Trim() + "') or (NEW_PROCESSSTS is null and TELLER_ID <>'" + Common.Userid.Trim() + "')))"; 
                    }
                }
                #region comment
                //if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                //{
                //    dataMessage = clsDatagridviews.LOAD_DATA_RESEND("Where " + Where_Master, isSupervisor, dataMessage);//lay toan bo dien co trong ngay,tai bang content
                //}
                //else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                //{
                //    dataMessage = clsDatagridviews.MESSAGE_CONTENT_INWARD("Where " + Where_Master, isSupervisor, dataMessage);//Lay toan bo dien trong bang content
                //}
                #endregion

                Search_data();
                //lay du lieu tu datagrid view ra datatable-----------de in dien---------------------------------------------
                _dtViews = (DataTable)dataMessage.DataSource;
                //-----------------------------------------------------------------------------------------------------------
                label12.Text = "Total number of messages :" + Convert.ToString(dataMessage.Rows.Count);
               
                Enable_Controls();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Resend_Manual()
        {
            try
            {
                if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                {
                    this.Text = "Swift resend messages inward";

                    datefrom.Checked = true;
                    dateto.Checked = true;
                }
                else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                {
                    this.Text = "Swift inward manual process";
                    datefrom.Checked = false;
                    dateto.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //ham dinh vi cac control cua form
        private void Locate_controls()
        {
            try
            {
                cbCheck.Visible = false; datefrom.Checked = false;
                cmdStatement.Enabled = true; dateto.Checked = false;
                Common.strApprove = "";
                this.date1.Enabled = false;
                this.datefrom.MaxDate = DateTime.Now;
                this.datefrom.MaxDate = dateto.Value;
                this.dateto.MaxDate = DateTime.Now;
                //--------------------------------------------------------
                dataMessage.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataMessage.Columns[0].HeaderCell = dgvColumnHeader;
                dataMessage.Columns[0].Width = 26;
                //--------------dinh vi control---------------
                this.cmdAdvance.Location = new Point(cmdNornal.Location.X, cmdNornal.Location.Y);                
                iSearch = 0;
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
        

        private void dataMessage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //goi ham de xoa ban log khi xu ly thu cong trong bang SWIFT_PROCESS_
                Delete_SWIFT_PROCESS_HANDER();
                if (dataMessage.Rows.Count != 0)
                { 
                    iRows = e.RowIndex;
                    if (iRows != -1)
                    {
                        frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                        frmSWIFT.strMSG_ID = dataMessage.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                        frmSWIFT.strMsg_type = dataMessage.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
                        frmSWIFT.strMSG_DIRECTION = dataMessage.Rows[iRows].Cells["MSG_DIRECTION"].Value.ToString();
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
                                pShow = cbColumns.Text + " " + cbOperator.Text + " '" + txtValue.Text + "' ";
                                pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " Upper('" + txtValue.Text.Replace(",", "") + "') ";
                            }
                            #endregion
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
                                pShow = cbColumns.Text + " " + cbOperator.Text + " " + txtValue.Text + " ";
                                pSearch = "  Trim(" + cbColumns.SelectedValue.ToString() + ") " + cbOperator.Text + " '" + txtValue.Text.Replace(",", "") + "' ";
                            }
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
                    if (txtValue.Text.Trim() != "")
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
        

        private void CheckIsSupervisor()
        {
            //bool result = a;
            string UserID = Common.Userid;
            DataSet dsGroup = new DataSet();
            dsGroup = objControlGroup.GetGroup_IsSupervisor(UserID,Common.gGWTYPE);
            if (dsGroup.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else if (dsGroup.Tables[0].Rows.Count == 1)
            {
                int IsSupervisor = Convert.ToInt32(dsGroup.Tables[0].Rows[0][0].ToString());
                if (IsSupervisor == 1)//neu la 1 la teller
                {
                    isSupervisor = 1;
                    cmdApp.Visible = false;
                    cmdProcess.Enabled = true;
                    //cmdExport.Location = new Point(cmdApp.Location.X + 4, cmdApp.Location.Y);
                }
                else if (IsSupervisor == 2)//neu la 2 la suppervisor
                {
                    isSupervisor = 2;
                    cmdApp.Visible = true;
                    
                    cmdProcess.Visible = true;
                    if (Common.strSTATUS_IN == "Resend")
                    {
                        //cmdStatement.Visible = false;
                        cmdApp.Location = new Point(cmdExport.Location.X - 87, cmdview.Location.Y);
                    }
                    //cmdProcess.Location = new Point(cmdview.Location.X-87, cmdview.Location.Y);
                    //cmdApp.Location = new Point(cmdProcess.Location.X -88, cmdProcess.Location.Y);
                    //cmdExport.Location = new Point(cmdApp.Location.X - 84, cmdApp.Location.Y);
                }
            }
            else if (dsGroup.Tables[0].Rows.Count == 2)//co ca hai quyen teller va suppervisor
            {
                isSupervisor = 3;
                cmdview.Enabled = true;
                cmdApp.Enabled = true;
                cmdProcess.Enabled = true;
                cmdExport.Enabled = true;
                cmdStatement.Enabled = true;
            }           
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
                //    txtValue.Visible = false;
                //    cboStatus.Visible = false;
                //    dateValue.Visible = true;
                //    this.dateValue.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "STATUS")
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["STATUS"];
                //    cboStatus.DisplayMember = "NAME"; cboStatus.ValueMember = "STATUS";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);                    
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_SRC")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false;txtValue.Visible = false;
                //    cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSG_SRC"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}               
                //else if (cbColumns.SelectedValue.ToString() == "SWMSTS")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["SWMSTS"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "PROCESSSTS")
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false;cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["PROCESSSTS"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "DEPARTMENT")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; txtValue.Visible = false;
                //    cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["DEPARTMENT"];
                //    cboStatus.DisplayMember = "CONTENT"; cboStatus.ValueMember = "CDVAL";
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "MSG_DIRECTION")
                //{
                //    //iVisible = 2;
                //    txtValue.Visible = false;txtValue.Visible = false;cboStatus.Visible = true;
                //    cboStatus.DataSource = _dsMaster.Tables["MSG_DIRECTION"];
                //    cboStatus.DisplayMember = "CONTENT"; 
                //    this.cboStatus.Location = new Point(txtValue.Location.X, txtValue.Location.Y);
                //}
                //else if (cbColumns.SelectedValue.ToString() == "CCYCD")
                //{
                //    //iVisible = 2; 
                //    txtValue.Visible = false; cboStatus.Visible = true;                    
                //    cboStatus.DataSource = _dsMaster.Tables["CCYCD"];
                //    cboStatus.DisplayMember = "CCYCD"; 
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
              

        private void cmdNornal_Click(object sender, EventArgs e)
        {
            iSearch = 0;
            try
            {
                if (dataMessage.Rows.Count == 0)
                {
                    cmdview.Enabled = false;
                }
                else
                {
                    cmdview.Enabled = true;
                }               
                //this.dataMessage.Top = dataMessage.Top + 28;
                //this.dataMessage.Height = dataMessage.Height - 28;
                //this.label12.Top = label12.Top + 28;
                //this.label12.Height = label12.Height + 28;
                //this.Lbtong.Top = Lbtong.Top + 28;
                //this.Lbtong.Height = Lbtong.Height + 28;
                cmdAdvance.Show();
                grbSearchnhanh.Hide();
                grbDieukien.Hide();                
                //this.grbSearch.Location = new Point(10, grbSearch.Location.X);
                //this.grbSearch.Location = new Point(13, grbSearch.Location.Y);
                grbSearch.Show();                
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
                #region code
                if (dataMessage.Rows.Count == 0)
                { cmdview.Enabled = false;}
                else {cmdview.Enabled = true; }
                //--------------------------ngay hien tai cua he thong-----------
                this.dateValue.MaxDate = DateTime.Now;
                //---------------------------------------------------------------
                //this.grbSearch.Size = new Size(grbSearchnhanh.Size.Width + grbDieukien.Size.Width + 2, grbSearchnhanh.Size.Height + 30);
                //this.dataMessage.Top = dataMessage.Top - 28;
                //this.dataMessage.Height = dataMessage.Height + 28;
                //this.label12.Top = label12.Top - 28;
                //this.label12.Height = label12.Height - 28;
                //this.Lbtong.Top = Lbtong.Top - 28;
                //this.Lbtong.Height = Lbtong.Height - 28;                
                cmdAdvance.Hide();
                grbSearch.Hide();
                grbSearchnhanh.Show();
                grbDieukien.Show();
                #endregion

                #region Quynd comment 20100319
                //_dtSearch = objsearchcontrol.COLUMNS_SEARCH("SWIFT", out _dtSearch);
                //cbCheck.DataSource = _dtSearch;
                //cbCheck.DisplayMember = "FIELDNAME";
                //cbCheck.ValueMember = "OPERATOR";
                //cbColumns.DataSource = _dtSearch;
                //cbColumns.ValueMember = "FIELDCODE"; 
                //cbColumns.DisplayMember = "FIELDNAME";                               
                //cbColumns.Focus();
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

                cbColumns.Focus();

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

        private void dataMessage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
       

        private void frmSwiftINMsgManual_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                dataMessage_KeyDown(sender, e);
                Delete_process_hander();
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
            if (strAmount != "")            
            {
                if (Regex.IsMatch(txtAmount.Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                }               
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
                if (dateto.Checked == true && datefrom.Checked == true)
                {
                    if (dateto.Value < datefrom.Value)
                    {
                        this.dateto.Value = datefrom.Value;
                    }                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        private void txtStatement_ID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtStatement_ID.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    STATETEE = txtStatement_ID.Text;
                }
                else
                {
                    txtStatement_ID.Text = STATETEE;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void check_Teller_Group()
        {
            try
            {
                pTeller_Supp = 0;
                int h = 0;
                while (h < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[h].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[h].Cells[0].Value.ToString() == "True")
                        {

                            if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien
                            {
                                if (dataMessage.Rows[h].Cells["PROCESSSTS"].Value.ToString() != Common.PROCESSSTS_WAPPROVING_RESENT)
                                {
                                    //neu co mot dien khong duoc Approver thi bao loi
                                    pTeller_Supp = 1;
                                }
                            }
                            else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                            {
                                if (dataMessage.Rows[h].Cells["PROCESSSTS"].Value.ToString() != Common.PROCESSSTS_WAPPROVING)
                                {
                                    //neu co mot dien khong duoc Approver thi bao loi
                                    pTeller_Supp = 1;
                                }
                            }                            
                        }
                    }
                    h = h + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
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

        private void cmdApp_Click(object sender, EventArgs e)
        {
            try
            {
                Select_rows();//xem co duoc chon hang nao khong
                if (iSelect_rows == 1)
                {                    
                    string Msg = "Do you want to approve ?";
                    string title = Common.sCaption;
                    DialogResult DlgResult = new DialogResult();
                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DlgResult == DialogResult.Yes)
                    {
                        check_Teller_Group();
                        if (pTeller_Supp == 0)//duoc phep Approver
                        {
                            if (iRole == 0 || iRole == 2)//co quyen duyet ban ghi nay
                            {
                                //Log_messages();
                                App_Groups();
                                Return_null();
                            }
                            else
                            {
                                Common.ShowError("Users have no right approver!", 3, MessageBoxButtons.OK);                                
                            }
                        }
                        else
                        {
                            Common.ShowError("Invalid message status to process !", 3, MessageBoxButtons.OK);                            
                        }
                    }
                }
                else if (iSelect_rows == 0)
                {
                    Common.ShowError("There is no message select!", 3, MessageBoxButtons.OK);                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            if (dataMessage.Rows.Count == 0)
            {
                cmdApp.Enabled = false;
            }
        }

        private void GetSTATUS(string strSTS)
        {
            try
            {
                DataTable datSTR = new DataTable();
                datSTR = objAllcodeControl.GetALLCODE_DATA1(strSTS.Trim(), "SWIFT");
                iSTATUS = Convert.ToInt32(datSTR.Rows[0]["CDVCAL"].ToString());
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void App_Groups()
        {
            try
            {
                Groups_Sucess = 0;
                Groups_Error = 0;
                int c = 0;
                while (c < dataMessage.Rows.Count)
                {
                    if (dataMessage.Rows[c].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataMessage.Rows[c].Cells[0].Value.ToString() == "True")
                        {
                            iOK_STATUS = 0;
                            DataTable datSTA = new DataTable();
                            string strTabl = dataMessage.Rows[c].Cells["Tabl"].Value.ToString();
                            //QUYND UPDATE20081122-----------------------------------
                            string pTable = "";
                            if (strTabl.Trim() == "1")//bang SWIFT_MSG_CONTENT
                            {
                                pTable = "SWIFT_MSG_CONTENT";
                            }
                            else if (strTabl.Trim() == "2")//bang SWIFT_MSG_ALL
                            {
                                pTable = "SWIFT_MSG_ALL";
                            }
                            else if (strTabl.Trim() == "3")//bang SWIFT_MSG_ALL_HIS
                            {
                                pTable = "SWIFT_MSG_ALL_HIS";
                            }
                            //-------------------------------------------------------
                            string strState = dataMessage.Rows[c].Cells["STATUS"].Value.ToString();
                            iSTATUS = Convert.ToInt32(strState);
                            //GetSTATUS(strState);bo doan code nay                                                    
                            //---lay du lieu ghi vao bang MSG_LOG--------------------
                            objSWIFT_log.LOG_DATE = DateTime.Now;
                            objSWIFT_log.QUERY_ID = Convert.ToInt32(dataMessage.Rows[c].Cells["QUERY_ID"].Value.ToString());
                            objSWIFT_log.STATUS = iSTATUS;
                            #region
                            objcontent.MSG_ID = Convert.ToInt32(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString());
                            objcontent.BRANCH_B = dataMessage.Rows[c].Cells["BRANCH_B"].Value.ToString();
                            objcontent.DEPARTMENT = dataMessage.Rows[c].Cells["DEPARTMENT"].Value.ToString();
                            objcontent.OFFICER_ID = Common.Userid;
                            #endregion

                            #region Resend dien -------------------------------------
                            if (Common.strSTATUS_IN == "Resend")//goi for gui lai dien(co the gui lai nhieu lan)
                            {
                                //--QUYND UPDATE20081122------------------------------
                                datSTA = objControlContent.SWIFT_STATUS(pTable, dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString());//FIELD20
                                if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_APPROVED)//da duoc approve roi
                                {
                                    Common.ShowError("Message have been approved!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "Ref number :" + dataMessage.Rows[c].Cells["FIELD20"].Value.ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                                       
                                    iOK_STATUS = 1;
                                }                               
                                //----------------------------------------------------
                                if (iOK_STATUS == 0)//cho phep thuc hien
                                {
                                    objcontent.STATUS = Common.STATUS_CONVERTED;
                                    objcontent.AUTO = "N";
                                    objcontent.PROCESSSTS = Common.PROCESSSTS_APPROVED;
                                   
                                    if (dataMessage.Rows[c].Cells["SWMSTS"].Value.ToString().Trim() == "")
                                    {
                                        objcontent.SWMSTS = "";
                                    }
                                    else
                                    {
                                        objcontent.SWMSTS = dataMessage.Rows[c].Cells["SWMSTS"].Value.ToString().Trim();// datSWM.Rows[0]["CDVAL"].ToString();
                                    }
                                    //-----------------------------------------------------------                    
                                    if (dataMessage.Rows[c].Cells["MSG_SRC"].Value.ToString().Trim() == "2")//goi ham cua chi hien
                                    {
                                        objcontent.PACKAGE = "GW_PK_SWIFT_Q_EXCEL_IN.SWEXCELPROCESS";
                                    }
                                    else if (dataMessage.Rows[c].Cells["MSG_SRC"].Value.ToString().Trim() == "1")
                                    {
                                        objcontent.PACKAGE = "GW_PK_SWIFT_Q_CONVERTIN.Processhander";
                                    }
                                    //---------------------------------------------------------
                                    if (objControlContent.UpdateSWIFT_MSG_CONTENTStatusT(objcontent) == 1)
                                    {
                                        
                                        objcontent.MSG_ID = Convert.ToInt32(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString());
                                        objcontent.LOCKSTS = "";
                                        objcontent.LOCK_TELLERID = "";
                                        objcontent.Table_Name = "SWIFT_MSG_CONTENT";
                                        objControlContent.Lock_User(objcontent);
                                        //-----------------------------------------------------------------------
                                        Groups_Sucess = Groups_Sucess + 1;
                                        Common.iOk = 1;
                                        #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                                        objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : Approve resend to branch:" + dataMessage.Rows[c].Cells["BRANCH_B"].Value.ToString() + " , Department :" + dataMessage.Rows[c].Cells["DEPARTMENT"].Value.ToString();
                                        objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                                        //---------------Ghi log vao bang userlog----------------------------------------------
                                        DateTime dtLog = DateTime.Now;
                                        string strUser = BR.BRLib.Common.strUsername;
                                        string useride = BR.BRLib.Common.Userid;
                                        string strConten = "Resend Messages";
                                        int Log_level = 1;
                                        string strWorked = "Approve";
                                        string strTable = "SWIFT_MSG_CONTENT";
                                        string strOld_value = "";
                                        string strNew_value = "";
                                        //-----------------------------------------
                                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                        #endregion
                                        objHander.DELETE_SWIFT_PROCESS_HANDER(Convert.ToInt32(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString()));
                                        dataMessage.Rows.RemoveAt(c);
                                    }
                                    else
                                    {
                                        Groups_Error = Groups_Error + 1;
                                       
                                        c = c + 1;
                                    }
                                }
                                else
                                {
                                    c = c + 1;
                                }
                            }
                            #endregion

                            #region xu ly thu cong-----------------------------------
                            else if (Common.strSTATUS_IN == "Manual")//xu ly thu cong
                            {
                                //--QUYND UPDATE 20081122-----------------------------
                                datSTA = objControlContent.SWIFT_STATUS(pTable, dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString());
                                if (datSTA.Rows[0]["PROCESSSTS"].ToString().Trim() == Common.PROCESSSTS_APPROVED)
                                {
                                    Common.ShowError("Message have been approved!" + "\n\r" + "OFFICER_ID :" + datSTA.Rows[0]["OFFICER_ID"].ToString() + "\n\r" + "Ref number :" + dataMessage.Rows[c].Cells["FIELD20"].Value.ToString() + "\n\r" + "You must reset data!", 3, MessageBoxButtons.OK);                                       
                                    iOK_STATUS = 1;
                                }
                               
                                if (iOK_STATUS == 0)//cho phep thuc hien
                                {
                                    objcontent.STATUS = Convert.ToString(iSTATUS);
                                    objcontent.AUTO = "N";
                                    objcontent.PROCESSSTS = Common.PROCESSSTS_APPROVED;                    
                                    if (dataMessage.Rows[c].Cells["SWMSTS"].Value.ToString().Trim() == Common.SWMSTS_OLDKEY_FAILURE || dataMessage.Rows[c].Cells["SWMSTS"].Value.ToString().Trim() == Common.SWMSTS_POSS_DUP)
                                    {
                                        objcontent.SWMSTS = Common.SWMSTS_NORMAL;
                                    }
                                    else
                                    {                                        
                                        if (dataMessage.Rows[c].Cells["SWMSTS"].Value.ToString().Trim() == "")
                                        {
                                            objcontent.SWMSTS = "";
                                        }
                                        else
                                        {
                                            objcontent.SWMSTS = dataMessage.Rows[c].Cells["SWMSTS"].Value.ToString().Trim();
                                        }
                                    }
                                    if (dataMessage.Rows[c].Cells["MSG_SRC"].Value.ToString().Trim() == "2")//goi ham cua chi hien
                                    {
                                        objcontent.PACKAGE = "GW_PK_SWIFT_Q_EXCEL_IN.SWEXCELPROCESS";
                                    }
                                    else if (dataMessage.Rows[c].Cells["MSG_SRC"].Value.ToString().Trim() == "1")
                                    {
                                        objcontent.PACKAGE = "GW_PK_SWIFT_Q_CONVERTIN.Processhander";
                                    }
                                    //---------------------------------------------------------
                                    if (objControlContent.UpdateSWIFT_MSG_CONTENTStatusT(objcontent) == 1)
                                    {
                                        
                                        objcontent.MSG_ID = Convert.ToInt32(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString());
                                        objcontent.LOCKSTS = "";
                                        objcontent.LOCK_TELLERID = "";
                                        objcontent.Table_Name = "SWIFT_MSG_CONTENT";
                                        objControlContent.Lock_User(objcontent);
                                        //---------------------------------------------------------------------
                                        Groups_Sucess = Groups_Sucess + 1;
                                        Common.iOk = 1;
                                        #region//ghi log vao bang SWIFT_MSG_LOG--------------------------------------------------
                                        objSWIFT_log.DESCRIPTIONS = "User ID :" + Common.Userid + " : Approve send to branch :" + dataMessage.Rows[c].Cells["BRANCH_B"].Value.ToString() + " , Department :" + dataMessage.Rows[c].Cells["DEPARTMENT"].Value.ToString();
                                        objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                                        //neu approver thanh cong cap nhat vao bang MSG_CONTENT,ALL,ALL_HIS truong TRANS_SATE=sysdate Yeu cau ngay 2008.10.22
                                        if (objControlContent.UPDATE_SYSDATE(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString().Trim(), "SWIFT_MSG_CONTENT") != 1)
                                        {
                                            //---------------Ghi log vao bang userlog----------------------------------------------
                                            DateTime dtLog = DateTime.Now;
                                            string strUser = BR.BRLib.Common.strUsername;
                                            string useride = BR.BRLib.Common.Userid;
                                            string strConten = "REF NUMBER:" + dataMessage.Rows[c].Cells["FIELD20"].Value.ToString();
                                            int Log_level = 1;
                                            string strWorked = "Update Sysdate Error";
                                            string strTable = "SWIFT_MSG_CONTENT";
                                            string strOld_value = "";
                                            string strNew_value = "";
                                            //-----------------------------------------
                                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                        }
                                        if (objControlContent.UPDATE_SYSDATE(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString().Trim(), "SWIFT_MSG_ALL") != 1)
                                        {
                                            //---------------Ghi log vao bang userlog----------------------------------------------
                                            DateTime dtLog = DateTime.Now;
                                            string strUser = BR.BRLib.Common.strUsername;
                                            string useride = BR.BRLib.Common.Userid;
                                            string strConten = "REF NUMBER:" + dataMessage.Rows[c].Cells["FIELD20"].Value.ToString();
                                            int Log_level = 1;
                                            string strWorked = "Update Sysdate Error";
                                            string strTable = "SWIFT_MSG_ALL";
                                            string strOld_value = "";
                                            string strNew_value = "";
                                            //-----------------------------------------
                                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                        }
                                        if (objControlContent.UPDATE_SYSDATE(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString().Trim(), "SWIFT_MSG_ALL_HIS") != 1)
                                        {
                                            //---------------Ghi log vao bang userlog----------------------------------------------
                                            DateTime dtLog = DateTime.Now;
                                            string strUser = BR.BRLib.Common.strUsername;
                                            string useride = BR.BRLib.Common.Userid;
                                            string strConten = "REF NUMBER:" + dataMessage.Rows[c].Cells["FIELD20"].Value.ToString();
                                            int Log_level = 1;
                                            string strWorked = "Update Sysdate Error";
                                            string strTable = "SWIFT_MSG_ALL_HIS";
                                            string strOld_value = "";
                                            string strNew_value = "";
                                            //-----------------------------------------
                                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                        }
                                        objControlContent.UPDATE_SYSDATE_MSGDTL(dataMessage.Rows[c].Cells["QUERY_ID"].Value.ToString().Trim(), "SWIFT_MSGDTL");
                                        objControlContent.UPDATE_SYSDATE_MSGDTL(dataMessage.Rows[c].Cells["QUERY_ID"].Value.ToString().Trim(), "SWIFT_MSGDTL_ALL");
                                        objControlContent.UPDATE_SYSDATE_MSGDTL(dataMessage.Rows[c].Cells["QUERY_ID"].Value.ToString().Trim(), "SWIFT_MSGDTL_ALL_HIS");                                    
                                        #endregion                                        
                                        objHander.DELETE_SWIFT_PROCESS_HANDER(Convert.ToInt32(dataMessage.Rows[c].Cells["MSG_ID"].Value.ToString()));
                                        dataMessage.Rows.RemoveAt(c);                                        

                                    }
                                    else
                                    {
                                        c = c + 1;
                                        Groups_Error = Groups_Error + 1;
                                        
                                    }
                                }
                                else
                                {
                                    c = c + 1;
                                }
                            }
                            #endregion

                        }
                        else
                        {
                            c = c + 1;
                        }
                    }
                    else
                    {
                        c = c + 1;
                    }

                }
                if (Groups_Error == 0)
                {
                    Common.ShowError(Groups_Sucess + " :Data has approved successfully!", 1, MessageBoxButtons.OK);                    
                }
                else if (Groups_Error != 0)
                {
                    Common.ShowError(Groups_Sucess + " :Data has approved successfully!" + "\r\n" + Groups_Error + "Data has approved Error!", 1, MessageBoxButtons.OK);                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            label12.Text = "Total number of messages :" + Convert.ToString(dataMessage.Rows.Count);
        }

        private void frmSwiftINMsgManual_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSwiftINMsgManual_MouseDown(object sender, MouseEventArgs e)
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

        private void datefrom_ValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (datefrom.Checked == true)
                {
                    dateto.Checked = true;
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

    }
}
