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
    public partial class frmTransferData : frmBasedata
    {
        #region khai bao ham va bien
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private BKTABLEInfo objBktable = new BKTABLEInfo();
        private BKTABLEController objcontrolBktable = new BKTABLEController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private IBPS_MSG_ALLInfo objIMA = new IBPS_MSG_ALLInfo();
        private IBPS_MSG_ALLController objctrlIMA = new IBPS_MSG_ALLController();
        private bool isStop = false;       
        private static bool strSuccess = true;
        #endregion

        public frmTransferData()
        {
            InitializeComponent();
        }

        //form load
        private void frmTransfer_data_Load(object sender, EventArgs e)
        {
            string strBkty = Common.GW_BKTBL_BKTYPE_DB;

            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");               
            //Load data cbBktime
            if (!objGetData.FillDataComboBox(cbBktime, "bktime", "bktime", "BKTABLE",
                "Trim(bktype)='" + strBkty + "'", "bktime", true, false, ""))
                return;
            GetdataGrid();
        }
      
        /*---------------------------------------------------------------
        * Muc dich         : Lay du lieu la cac ten Table trong bang BKtable voi dieu kien BKTYPE = 2
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 09/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void GetdataGrid()
        {
            try
            {
                datTable.Rows.Clear();
                string strBktype = Common.GW_BKTBL_BKTYPE_DB ;
                string strBktime = cbBktime.Text;
                DataSet datBktable = new DataSet();
                datBktable = objcontrolBktable.GetBKTABLE1(strBktype, strBktime);
                //datTable.DataSource = datBktable.Tables[0];
                int i = 0;
                while (i < datBktable.Tables[0].Rows.Count)
                {
                    datTable.Rows.Add();
                    string strSOURCETBL = datBktable.Tables[0].Rows[i]["SOURCETBL"].ToString();
                    string strDESTBL = datBktable.Tables[0].Rows[i]["DESTBL"].ToString();
                    //--------------------------------------------
                    datTable.Rows[i].Cells[1].Value = strSOURCETBL;
                    datTable.Rows[i].Cells[2].Value = strDESTBL;
                    datTable.Rows[i].Cells[0].Value = false;
                    //---------------------------------------------
                    i = i + 1;
                }
                datTable.Columns[0].Width = 75;
                datTable.Columns[1].Width = 200;
                datTable.Columns[2].Width = 200;
                datTable.Columns[1].ReadOnly = true;
                datTable.Columns[2].ReadOnly = true;
            }
            catch
            { 
            }
        }     
        
        private string  GetVarName(string strTableName)
        {
            string strVarname = "";

            if (strTableName == Common.GW_IBPS_MSG_CONTENT)
                strVarname = Common.GW_SYSVAR_IBPS_MSG_CONTENT;
            else if (strTableName == Common.GW_IBPS_MSGDTL)
                strVarname = Common.GW_SYSVAR_IBPS_MSGDTL;
            else if (strTableName == Common.GW_SWIFT_MSG_CONTENT)
                strVarname = Common.GW_SYSVAR_SWIFT_MSG_CONTENT;
            else if (strTableName == Common.GW_SWIFT_MSGDTL)
                strVarname = Common.GW_SYSVAR_SWIFT_MSGDTL;
            else if (strTableName == Common.GW_TTSP_MSG_CONTENT)
                strVarname = Common.GW_SYSVAR_TTSP_MSG_CONTENT;
            else if (strTableName == Common.GW_TTSP_MSGDTL)
                strVarname = Common.GW_SYSVAR_TTSP_MSGDTL;
            else if (strTableName == Common.GW_VCB_MSG_CONTENT)
                strVarname = Common.GW_SYSVAR_VCB_MSG_CONTENT;
            else if (strTableName == Common.GW_VCB_MSGDTL)
                strVarname = Common.GW_SYSVAR_VCB_MSGDTL;
            else if (strTableName == Common.GW_IQS_MSG_CONTENT)
                strVarname = Common.GW_SYSVAR_IQS_MSG_CONTENT;
            else
                strVarname = "";

            return strVarname;

        }

        /*---------------------------------------------------------------
        * Muc dich         : Ham tim kiem theo dieu kien chon lua o combobox
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 09/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdsearch_Click(object sender, EventArgs e)
        {
            try
            {
                datTable.Rows.Clear();
                string strBktype = "2";
                string strBkt = cbBktime.Text;
                DataSet datBksearch = new DataSet();
                datBksearch = objcontrolBktable.GetBk_Search(strBktype,strBkt);
                int j = 0;
                while (j < datBksearch.Tables[0].Rows.Count)
                {
                    datTable.Rows.Add();
                    string strSourcetbl = datBksearch.Tables[0].Rows[j]["SOURCETBL"].ToString();
                    string strDestbl = datBksearch.Tables[0].Rows[j]["DESTBL"].ToString();
                    //--------------------------------------------
                    datTable.Rows[j].Cells[1].Value = strSourcetbl;
                    datTable.Rows[j].Cells[2].Value = strDestbl;
                    //---------------------------------------------
                    j = j + 1;
                }
                datTable.Columns[0].Width = 75;
                datTable.Columns[1].Width = 200;
                datTable.Columns[2].Width = 200;
                datTable.Columns[1].ReadOnly = true;
                datTable.Columns[2].ReadOnly = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //thoat khoi form
        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*---------------------------------------------------------------
        * Muc dich         : Back up databases
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 09/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdbackup_Click(object sender, EventArgs e)
        {            
            try
            {
                int k = 0;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show("Do you want to backup database?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.No)
                {
                    return;
                }
                SYSVARController objSysvarController = new SYSVARController();
                while (k < datTable.Rows.Count)// duyet tung ban ghi trong Luoi
                {
                    //if (datTable.Rows[k].Cells[0].Value != null)// hang duoc chon
                    //{
                        if (datTable.Rows[k].Cells[0].Value.ToString() == "True")// dong duoc chon
                        {
                            isStop = true;
                            strSuccess = false;
                            string strTable1 = datTable.Rows[k].Cells[1].Value.ToString();
                            string strTable2 = datTable.Rows[k].Cells[2].Value.ToString();
                            string strWhere = "";

                            int iRowCount = 0;
                            iRowCount = objctrlIMA.CheckExist(strTable2);
                            if (iRowCount == 0)//neu bang du lieu de chua (khong co du lieu)
                            {
                                if (cbBktime.Text.Trim() == Common.GW_BKTBL_BKTIME_EOD)//tu bang content sang bang all, neu = EOD
                                {
                                    string strVarname;
                                    strVarname = GetVarName(strTable1);
                                    int iResult = 0;
                                    iResult = objSysvarController.UpdateSysvar(strVarname, Common.GW_SYSVAR_BACKUP_STATUS_PENDING, "Doing Backup... ");

                                    if (iResult == 1)
                                    {
                                        if (objctrlIMA.BackUpAll(strTable1, strTable2, "1") == -1)
                                        { strSuccess = false; }
                                        else
                                        {
                                            strSuccess = true;
                                            strWhere = " Where status = '1'";
                                            iResult = objSysvarController.UpdateSysvar(strVarname, Common.GW_SYSVAR_BACKUP_STATUS_OK, "Finish Backup");

                                            if (iResult == -1)
                                            {
                                                strSuccess = false;
                                            }
                                        }
                                    }
                                    else
                                    { strSuccess = false; }
                                }
                                else if (cbBktime.Text.Trim() == Common.GW_BKTBL_BKTIME_EOM)//tu bang  all sang bang All_his
                                {
                                    if (objctrlIMA.BackUpAll(strTable1, strTable2,"") == -1)
                                    {
                                        //bBackup = false; 
                                        strSuccess = false ;
                                    }
                                    else
                                    {
                                        //bBackup = true; 
                                        strSuccess = true;
                                    }
                                }
                            }
                            else// bang insert vao co du lieu thi kiem tra gia tri trung
                            {
                                if (cbBktime.Text.Trim() == Common.GW_BKTBL_BKTIME_EOD)//tu bang content sang bang all
                                {
                                    string strVarname;
                                    strVarname = GetVarName(strTable1);
                                    int iResult = 0;
                                    iResult = objSysvarController.UpdateSysvar(strVarname, Common.GW_SYSVAR_BACKUP_STATUS_PENDING, "Doing Backup... ");

                                    if (iResult == 1)
                                    {
                                        if (objctrlIMA.BackUp(strTable1, strTable2, "1") == -1)
                                        {
                                            //bBackup = false; 
                                            strSuccess = false ;
                                        }
                                        else
                                        {
                                            //bBackup = true; 
                                            strSuccess = true;
                                            strWhere = " Where status = '1'";
                                            iResult = objSysvarController.UpdateSysvar(strVarname, Common.GW_SYSVAR_BACKUP_STATUS_OK, "Finish Backup");
                                            if (iResult == -1)
                                            {
                                                //bBackup = false; 
                                                strSuccess = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //bBackup = false; 
                                        strSuccess = false;
                                    }
                                    
                                }
                                else if (cbBktime.Text.Trim() == Common.GW_BKTBL_BKTIME_EOM)//tu bang  all sang bang All_his
                                {
                                    if (objctrlIMA.BackUp(strTable1, strTable2, "") == -1)
                                    {
                                        //bBackup = false; 
                                        strSuccess = false ;
                                    }
                                    else
                                    {
                                        //bBackup = true; 
                                        strSuccess = true;
                                    }
                                }                                                                                          
                            }
                            if (strSuccess)
                            {
                                strWhere = " Where status = '1'";
                                objctrlIMA.Delete(strTable1, strWhere);
                            }
                            else
                            {
                                MessageBox.Show("Error occured when back up table " + strTable1  + "!" , Common.sCaption, MessageBoxButtons.OK,MessageBoxIcon.Error );
                                return;
                            }
                        }

                    k = k + 1;
                }

                if (isStop)
                {
                                        //---------------lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Transfer database";
                    int Log_level = 1;
                    string strWorked = "Transfer_database";
                    string strTable1 = "DB";
                    string strOld_value = "";
                    string strNew_value = "";
                    //-----------------------------------------
                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                        strWorked, strTable1, strOld_value, strNew_value);
                    MessageBox.Show("Backup successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                   
                else
                    MessageBox.Show("Please choose tables to backup!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //su kien thoat khoi form
        private void frmTransferData_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmdClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbBktime_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetdataGrid();
        }
       
        private void frmTransferData_KeyDown(object sender, KeyEventArgs e)
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

        private void frmTransferData_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
