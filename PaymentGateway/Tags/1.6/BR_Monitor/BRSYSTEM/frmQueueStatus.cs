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

namespace BR.BRSYSTEM
{
    public partial class frmQueueStatus : frmBasedata
    {
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private ALLCODEInfo objQueue = new ALLCODEInfo();
        private ALLCODEController objCtrlQueue = new ALLCODEController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();

        private int iRow;        
        private static string strQueue_Name;
        //Tham so ghi log
        private int iLogLevel = 1;
        private DateTime dLogDate = DateTime.Now;
        private string sLogUser = Common.strUsername;
        private string sLogUserID = Common.Userid;
        private string strConten = "";
        private string strWorked = "";
        private string strTable = "";
        private string strOld_value = "";
        private string strNew_value = "";
        
        
        public frmQueueStatus()
        {
            InitializeComponent();
        }

        private void frmQueueStatus_Load(object sender, EventArgs e)
        {
            try
            {
                string strUserDB = DATABASESInfo.strUserDB;

                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");                
                //Load data cbQueue
                if (!objGetData.FillDataComboBox(cbQueue, "name", "name", "All_Queues",
                    "OWNER='" + strUserDB + "' and Trim(QUEUE_TYPE)='NORMAL_QUEUE'",
                    "name", true, true, "ALL"))
                    return;
                Getdatagrid();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }          

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (objCtrlQueue.Start_Queue(strQueue_Name) == 1)
                {                    
                    Common.ShowError("Start sucessfully!",1,MessageBoxButtons.OK);
                    datQueues.Rows[iRow].Cells[1].Value = "Started";
                    datQueues.Rows[iRow].Cells[2].Value = "Started";
                    cmdStart.Enabled = false;
                    cmdStop.Enabled = true;
                    cmdRestart.Enabled = true;
                }
                else
                {                    
                    Common.ShowError("Start failed!", 2, MessageBoxButtons.OK);
                }                
                //Ghi log
                strConten = "Queue status";                
                strWorked = "Start_Queue";
                strTable = "All_Queue";
                strOld_value = "";
                strNew_value = "";
                
                objLog.GhiLogUser(dLogDate, sLogUser, strConten, iLogLevel, 
                    strWorked, strTable, strOld_value, strNew_value);                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Stop queue
                if (objCtrlQueue.Stop_Queue(strQueue_Name) == 1)
                {
                    Common.ShowError("Stop sucessfull!", 1, MessageBoxButtons.OK);                    
                    datQueues.Rows[iRow].Cells[1].Value = "Stoped";
                    datQueues.Rows[iRow].Cells[2].Value = "Stoped";
                    cmdStart.Enabled = true;
                    cmdStop.Enabled = false;
                    cmdRestart.Enabled = false;
                }
                else
                {                    
                    Common.ShowError("Stop failed!", 2, MessageBoxButtons.OK);
                }
                //Ghi log                
                strConten = "Queue status";                
                strWorked = "Stop_Queue";
                strTable = "All_Queue";
                strOld_value = "";
                strNew_value = "";
                objLog.GhiLogUser(dLogDate, sLogUser, strConten, iLogLevel,
                    strWorked, strTable, strOld_value, strNew_value);                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (objCtrlQueue.Stop_Queue(strQueue_Name) == 1)
                {
                    if (objCtrlQueue.Start_Queue(strQueue_Name) == 1)
                    {
                        Common.ShowError("Restart Sucessfully!", 1, MessageBoxButtons.OK);                        
                        datQueues.Rows[iRow].Cells[1].Value = "Started";
                        datQueues.Rows[iRow].Cells[2].Value = "Started";
                        cmdStart.Enabled = false;
                        cmdStop.Enabled = true;
                        cmdRestart.Enabled = true;
                    }
                    else
                    {                        
                        Common.ShowError("Restart failed!", 2, MessageBoxButtons.OK);
                        datQueues.Rows[iRow].Cells[1].Value = "Stoped";
                        datQueues.Rows[iRow].Cells[2].Value = "Stoped";
                    }
                }
                else
                {
                    Common.ShowError("Restart failed!", 2, MessageBoxButtons.OK);                    
                }
                //Ghi log
                strConten = "Queue status";                
                strWorked = "ReStart_Queue";
                strTable = "All_Queue";
                strOld_value = "";
                strNew_value = "";
                objLog.GhiLogUser(dLogDate, sLogUser, strConten, iLogLevel,
                    strWorked, strTable, strOld_value, strNew_value);                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //searching 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbQueue.Text == "ALL")
                {
                    Getdatagrid();
                }
                else
                {
                    datQueues.Rows.Clear();
                    string strUserDB = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                    string strQueueName = cbQueue.Text;
                    DataSet datQueueSearch = new DataSet();
                    datQueueSearch = objCtrlQueue.SearchQueue(strUserDB, strQueueName);
                    datQueues.Rows.Add();
                    string strQueue_name = datQueueSearch.Tables[0].Rows[0]["NAME"].ToString();
                    string strEnQueue_Enable = datQueueSearch.Tables[0].Rows[0]["ENQUEUE_ENABLED"].ToString();
                    string strDeQueue_Enable = datQueueSearch.Tables[0].Rows[0]["DEQUEUE_ENABLED"].ToString();
                    datQueues.Rows[0].Cells[0].Value = strQueue_name;
                    if (strEnQueue_Enable == "YES")
                    { datQueues.Rows[0].Cells[1].Value = "Started"; }
                    else
                    { datQueues.Rows[0].Cells[1].Value = "Stoped"; }
                    if (strDeQueue_Enable == "YES")
                    { datQueues.Rows[0].Cells[2].Value = "Started"; }
                    else
                    { datQueues.Rows[0].Cells[2].Value = "Stoped"; }
                    datQueues.Columns[0].ReadOnly = true;
                    datQueues.Columns[1].ReadOnly = true;
                    datQueues.Columns[2].ReadOnly = true;
                    
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


        // Bat su kien click chuot vao hang trong DataGrid
        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {

        }
        //lay du lieu len datagrid
        private void Getdatagrid()
        {
            try
            {
                DataSet dataQ = new DataSet();
                dataQ = objCtrlQueue.GetQ();
                int g = 0;
                datQueues.Rows.Clear();
                while (g < dataQ.Tables[0].Rows.Count)
                {
                    datQueues.Rows.Add();
                    string strQueueN = dataQ.Tables[0].Rows[g]["NAME"].ToString();
                    datQueues.Rows[g].Cells[0].Value = strQueueN;
                    string strEnQueue = dataQ.Tables[0].Rows[g]["ENQUEUE_ENABLED"].ToString();
                    if (strEnQueue.Trim() == "YES")
                    { datQueues.Rows[g].Cells[1].Value = "Started"; }
                    else
                    { datQueues.Rows[g].Cells[1].Value = "Stoped"; }
                    string strDeQueue = dataQ.Tables[0].Rows[g]["DEQUEUE_ENABLED"].ToString();
                    if (strDeQueue.Trim() == "YES")
                    { datQueues.Rows[g].Cells[2].Value = "Started"; }
                    else
                    { datQueues.Rows[g].Cells[2].Value = "Stoped"; }
                    g = g + 1;
                }
                datQueues.Columns[0].ReadOnly = true;
                datQueues.Columns[1].ReadOnly = true;
                datQueues.Columns[2].ReadOnly = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void datQueues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int iRow =0;
                if (e.RowIndex != -1) { iRow = e.RowIndex; }
                string strEnQ = datQueues.Rows[iRow].Cells[1].Value.ToString();
                strQueue_Name = datQueues.Rows[iRow].Cells[0].Value.ToString();
                if (strEnQ == "Started")
                {
                    cmdStop.Enabled = true;
                    cmdStart.Enabled = false;
                    cmdRestart.Enabled = false;
                }
                else
                {
                    cmdStop.Enabled = false;
                    cmdStart.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void datQueues_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRow = e.RowIndex; }
                string strEnQ = "";
                if (datQueues.Rows[iRow].Cells[1].Value != null) { strEnQ = datQueues.Rows[iRow].Cells[1].Value.ToString(); }
                if (datQueues.Rows[iRow].Cells[0].Value != null) { strQueue_Name = datQueues.Rows[iRow].Cells[0].Value.ToString(); }
                if (strEnQ == "Started")
                {
                    cmdStop.Enabled = true; cmdStart.Enabled = false; cmdRestart.Enabled = false;
                }
                else
                { cmdStop.Enabled = false; cmdStart.Enabled = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmQueueStatus_KeyDown(object sender, KeyEventArgs e)
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
                    if ((this.ActiveControl as Button).Name == "cmdSearch")
                    {
                        button1_Click(null, null);
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

        private void frmQueueStatus_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void datQueues_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
