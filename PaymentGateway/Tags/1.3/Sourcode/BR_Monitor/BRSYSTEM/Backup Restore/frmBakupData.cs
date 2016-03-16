using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Diagnostics;
using System.Threading;
using System.IO;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class frmBackupData : frmBasedata
    {
        #region khai bao cac ham va cac bien
        public static Process process1 = new Process();
        public static int mn = 0;//so dem

        private clsLog objLog = new clsLog();
        private BKTABLEInfo objBktable = new BKTABLEInfo();
        private BKTABLEController objcontrolBktable = new BKTABLEController();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private DataGridViewButtonColumn datbt = new DataGridViewButtonColumn();
        private DataGridViewTextBoxColumn dattextb = new DataGridViewTextBoxColumn();
        private DataGridViewCheckBoxColumn datcheckb = new DataGridViewCheckBoxColumn();
                
        //private int selectedRow = 0;        
        //private bool NeedConfirm = true;        
        //private bool blnflag=false ;
        private static bool isStop = false;
        //private static bool strSuccess = true;
        private static string strBktime;
        private static string strDatabase = BR.BRBusinessObject.DATABASESInfo.strDatabase;
        private static string strOracleServer = BR.BRBusinessObject.DATABASESInfo.strOracleServer;
        private static string strUsername = BR.BRBusinessObject.DATABASESInfo.strUserDB;
        private static string strPassword = BR.BRBusinessObject.DATABASESInfo.strPassword;
        //private static Guid RequestID = new Guid();
        private string mstrPathFile, mstrFileName, mstrTable, mstrQuery;        
        #endregion
        
        public frmBackupData()
        {
            InitializeComponent();
        }

        private void frmBackupData_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                LbPro.Text = "";
                this.datetfrom.MaxDate = DateTime.Now;
                this.datetfrom.MaxDate = datetto.Value;
                this.datetto.MaxDate = DateTime.Now;
                mn = 0;
                progressBar1.Maximum = 0;
                rdoCurrentmonth_CheckedChanged(null, null);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
       * Muc dich         : lay du lieu ten cac table trong bang BKTYPE voi dieu kien mac dinh la backup trong thang
       * Tra ve           : Mot danh sach cac File - List<FileInfo>
       * Ngay tao         : 26/54/2008
       * Nguoi tao        : Quynd
       * Ngay cap nhat    : 09/06/2008
       * Nguoi cap nhat   : Quynd
       *--------------------------------------------------------------*/
        private void getdatagrid()
        {
            try
            {
                datgridTable.Rows.Clear();
                string strBktype = Common.GW_BKTBL_BKTYPE_DUMP;//Chon kieu back up ra file .dump
                DataSet datBkcurrent = new DataSet();
                datBkcurrent = objcontrolBktable.GetBk_SearchBktype1(strBktype, strBktime);
                int b = 0;
                while (b < datBkcurrent.Tables[0].Rows.Count)
                {
                    datgridTable.Rows.Add();
                    string strTablename = datBkcurrent.Tables[0].Rows[b]["SOURCETBL"].ToString();
                    string strFile = datBkcurrent.Tables[0].Rows[b]["FILEPATH"].ToString();
                    string strExportdate = datBkcurrent.Tables[0].Rows[b]["LASTEXP"].ToString();
                    datgridTable.Rows[b].Cells[1].Value = strTablename;
                    datgridTable.Rows[b].Cells[2].Value = strFile;
                    datgridTable.Rows[b].Cells[3].Value = strExportdate;
                    b = b + 1;                   
                }                
                datgridTable.Columns[0].Width = 75;
                datgridTable.Columns[1].Width = 200;
                datgridTable.Columns[2].Width = 200;
                datgridTable.Columns[3].Width = 150;
                datgridTable.Columns[1].ReadOnly = true;
                datgridTable.Columns[2].ReadOnly = true;
                datgridTable.Columns[3].ReadOnly = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
       * Muc dich         : Tim kiem du lieu voi dieu kien chon
       * Tra ve           : Mot danh sach cac File - List<FileInfo>
       * Ngay tao         : 26/54/2008
       * Nguoi tao        : Quynd
       * Ngay cap nhat    : 09/06/2008
       * Nguoi cap nhat   : Quynd
       *--------------------------------------------------------------*/
        private void cmdsearch_Click(object sender, EventArgs e)
        {
            try
            {//strBktype
                if (rdoCurrentmonth.Checked == true)
                {
                    strBktime = "EOD";
                    getdatagrid();
                }
                else
                {
                    strBktime = "EOY";
                    getdatagrid();
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
       
        
      /*---------------------------------------------------------------
      * Muc dich         : Bat su kien khi chon check box
      * Tra ve           : Mot danh sach cac File - List<FileInfo>
      * Ngay tao         : 26/54/2008
      * Nguoi tao        : Quynd
      * Ngay cap nhat    : 09/06/2008
      * Nguoi cap nhat   : Quynd
      *--------------------------------------------------------------*/
        private void CheckBox_change(object sender, EventArgs e)
        {
           try
            {
                if (chbfromdate_todate.Checked )
                {
                    datetfrom.Enabled = true;
                    datetto.Enabled = true;
                    if (rdoCurrentmonth.Checked)
                    {
                        //Ngay dau thang
                        DateTime dtTmp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        datetfrom.Value = dtTmp;
                        datetto.Value  = Convert.ToDateTime(DateTime.Now.ToShortDateString())  ;
                    }
                    else
                    {

                        DateTime dtPrevTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        //Ngay dau nam
                        datetfrom.Value = dtPrevTime.AddMonths(-DateTime.Now.Month + 1);
                        datetto.Value = dtPrevTime.AddDays(-1);
                    }
                }
                else
                {
                    datetfrom.Enabled = false;
                    datetto.Enabled = false;
                }
            }
           catch (Exception ex)
           {
               Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
           }
          }
        /*---------------------------------------------------------------
      * Muc dich         : Back up du lieu
      * Tra ve           : Mot danh sach cac File - List<FileInfo>
      * Ngay tao         : 26/54/2008
      * Nguoi tao        : Quynd
      * Ngay cap nhat    : 09/06/2008
      * Nguoi cap nhat   : Quynd
      *--------------------------------------------------------------*/
        private void cmdBackup_Click(object sender, EventArgs e)
        {
                       
            //strSuccess = false;
            //cmdsearch.Enabled = true;
            cmdBackup.Enabled = true;
            progressBar1.Maximum = 10;
            progressBar1.Step = 1;
            
            LbPro.Text = "";
            try
            {
                if (!Verify())
                {
                    return;
                }
                string Msg = "Do you really want to export data?";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    isStop = false;
                    cmdBackup.Enabled = false;
                    //cmdsearch.Enabled = false;
                    cmdClose.Enabled = false;

                    #region //back up du lieu cua cac bang trong thang
                    if (rdoCurrentmonth.Checked == true)
                    {
                        if (chbfromdate_todate.Checked == true)
                        {

                            int z = 0;
                            string strFromTime;
                            string strToTime;

                            strFromTime = datetfrom.Value.ToString("yyyyMMdd");
                            strToTime = datetto.Value.ToString("yyyyMMdd");

                            while (z < datgridTable.Rows.Count)
                            {
                                if (datgridTable.Rows[z].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (datgridTable.Rows[z].Cells[0].Value.ToString() == "True")
                                    {
                                        //lay số liệu back up

                                        //if (z == 0)
                                        //{
                                        //    this.timer1.Enabled = true;
                                        //    this.timer1.Interval = 50;
                                        //    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                                        //}
                                        isStop = true;
                                        string strTable = datgridTable.Rows[z].Cells[1].Value.ToString();
                                        string strPathFile = datgridTable.Rows[z].Cells[2].Value.ToString();
                                        string strFileName = strTable + "_" + DateTime.Now.ToString("HHmm");

                                        char[] charsToTrim = { '\\' };
                                        strPathFile = strPathFile.TrimEnd(charsToTrim) + "\\" + strFromTime + "." + strToTime;

                                        string strQuery = "";
                                        strQuery = "'where to_date(trans_date,''dd/MM/yyyy'') between ''" + datetfrom.Value.ToString("dd/MM/yyyy") +
                                                "'' and ''" + datetto.Value.ToString("dd/MM/yyyy") + "'''";

                                        Backup(strPathFile, strFileName, strTable, strQuery);

                                        //lay thong tin de ghilog----------------------
                                        DateTime dtLog = DateTime.Now;
                                        string strUser = BR.BRLib.Common.strUsername;
                                        string useride = BR.BRLib.Common.Userid;
                                        string strConten = strPathFile;
                                        int Log_level = 1;
                                        string strWorked = "backup data";
                                        string strTable1 = strTable;
                                        string strOld_value = "";
                                        string strNew_value = "";
                                        //-----------------------------------------
                                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                            strWorked, strTable1, strOld_value, strNew_value);
                                        //--------------------------------------------------

                                    }
                                }
                                z = z + 1;
                            }

                        }
                        else//backup Table trong thang do
                        {
                            string strFromTime;
                            string strToTime;

                            DateTime dtTmp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            strFromTime = dtTmp.ToString("yyyyMMdd");
                            strToTime = DateTime.Now.ToString("yyyyMMdd");

                            int k = 0;
                            while (k < datgridTable.Rows.Count)
                            {
                                if (datgridTable.Rows[k].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (datgridTable.Rows[k].Cells[0].Value.ToString() == "True")
                                    {
                                        isStop = true;

                                        string strTable = datgridTable.Rows[k].Cells[1].Value.ToString();
                                        string strPathFile = datgridTable.Rows[k].Cells[2].Value.ToString();
                                        string strFileName = strTable + "_" + DateTime.Now.ToString("HHmm");

                                        char[] charsToTrim = { '\\' };
                                        strPathFile = strPathFile.TrimEnd(charsToTrim) + "\\" + strFromTime + "." + strToTime;

                                        string strQuery = "";
                                        strQuery = "'where to_date(trans_date,''dd/MM/yyyy'') between ''" + dtTmp.ToString("dd/MM/yyyy") +
                                                "'' and ''" + DateTime.Now.ToString("dd/MM/yyyy") + "'''";

                                        Backup(strPathFile, strFileName, strTable, strQuery);

                                        //lay thong tin de ghilog----------------------
                                        DateTime dtLog = DateTime.Now;
                                        string strUser = BR.BRLib.Common.strUsername;
                                        string useride = BR.BRLib.Common.Userid;
                                        string strConten = strPathFile;
                                        int Log_level = 1;
                                        string strWorked = "backup data";
                                        string strTable1 = strTable;
                                        string strOld_value = "";
                                        string strNew_value = "";
                                        //-----------------------------------------
                                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                            strWorked, strTable1, strOld_value, strNew_value);
                                        //--------------------------------------------------
                                        //if (k == 0)
                                        //{
                                        //    this.timer1.Enabled = true;
                                        //   // this.timer1.Interval = 50;
                                        //    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                                        //}

                                    }
                                }
                                k = k + 1;
                            }
                        }
                    }
                    #endregion
                    #region //back up cac thang truoc do
                    else
                    {
                        string strFromTime;
                        string strToTime;

                        strFromTime = datetfrom.Value.ToString("yyyyMMdd");
                        strToTime = datetto.Value.ToString("yyyyMMdd");

                        int s = 0;

                        while ((s < datgridTable.Rows.Count))
                        {


                            if (datgridTable.Rows[s].Cells[0].Value != null)// hang duoc chon
                            {
                                if (datgridTable.Rows[s].Cells[0].Value.ToString() == "True")
                                {
                                    //lay số liệu back up

                                    //if (s == 0)
                                    //{
                                    //    this.timer1.Enabled = true;
                                    //    this.timer1.Interval = 50;
                                    //    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                                    //}

                                    isStop = true;

                                    string strTable = datgridTable.Rows[s].Cells[1].Value.ToString();
                                    string strPathFile = datgridTable.Rows[s].Cells[2].Value.ToString();
                                    string strFileName = strTable + "_" + DateTime.Now.ToString("HHmm");

                                    //string strPathFile = datgridTable.CurrentRow.Cells[2].Value.ToString();
                                    char[] charsToTrim = { '\\' };
                                    strPathFile = strPathFile.TrimEnd(charsToTrim) + "\\" + strFromTime + "." + strToTime;

                                    string strQuery = "";
                                    strQuery = "'where to_date(trans_date,''dd/MM/yyyy'') between ''" + datetfrom.Value.ToString("dd/MM/yyyy") +
                                            "'' and ''" + datetto.Value.ToString("dd/MM/yyyy") + "'''";

                                    Backup(strPathFile, strFileName, strTable, strQuery);
                                    //this.timer1.Enabled = true;
                                    //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                                    //lay thong tin de ghilog----------------------

                                    DateTime dtLog = DateTime.Now;
                                    string strUser = BR.BRLib.Common.strUsername;
                                    string useride = BR.BRLib.Common.Userid;
                                    string strConten = strPathFile;
                                    int Log_level = 1;
                                    string strWorked = "backup data";
                                    string strTable1 = strTable;
                                    string strOld_value = "";
                                    string strNew_value = "";
                                    //-----------------------------------------
                                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                        strWorked, strTable1, strOld_value, strNew_value);
                                    //--------------------------------------------------

                                }
                            }
                            s = s + 1;
                        }
                    }
                    #endregion
                    if (isStop)
                        Common.ShowError("Backup successfully!",1, MessageBoxButtons.OK);                        
                    else
                        Common.ShowError("Please choose tables to backup!", 4, MessageBoxButtons.OK);                        
                    cmdBackup.Enabled = true;
                    // cmdsearch.Enabled = true;
                    cmdClose.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LbPro.Text = "";
        }
        /*---------------------------------------------------------------
        * Muc dich         : bat su kien click len luoi(vao nut Browse)
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 09/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void datgridTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (datgridTable.Columns[e.ColumnIndex].HeaderText == "Browse")
                {
                    int iRows = e.RowIndex;
                    saveFileDialog1.ShowDialog();
                    datgridTable.Rows[iRows].Cells[e.ColumnIndex - 1].Value = saveFileDialog1.FileName;
                    //---------------------------------------------------------------------------
                    string strPath =  saveFileDialog1.FileName;//lay duong dan file
                    string strFile = Path.GetFileName(strPath);//Lay ten file
                    String[] M = strFile.Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                    int k = M.Count<String>();
                    if (M[0] != "")                  
                    {
                        string strKieu = M[1];                        
                    }
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

/*---------------------------------------------------------------
  * Muc dich         : Ham kiem tra du lieu vao
  * Tra ve           : 
  * Ngay tao         : 05/08/2008
  * Nguoi tao        : HueMT
  * Ngay cap nhat    : 
  * Nguoi cap nhat   : 
  *--------------------------------------------------------------*/
        private bool Verify()
        {
            if (rdoCurrentmonth.Checked == true)
            {
                if (chbfromdate_todate.Checked == true)
                {
                    if (datetfrom.Value > datetto.Value ||
                                    datetfrom.Value.Month != DateTime.Now.Month
                                    || datetto.Value.Month != DateTime.Now.Month
                                    || datetfrom.Value > DateTime.Now
                                    || datetfrom.Value > DateTime.Now
                                    )
                    {                        
                        Common.ShowError("The date is invalid!", 2, MessageBoxButtons.OK);
                        return false;
                    }
                }
            }
            else
            {
                if (chbfromdate_todate.Checked == true)
                {
                    if (datetfrom.Value > datetto.Value || datetfrom.Value.Month >= DateTime.Now.Month || datetto.Value.Month >= DateTime.Now.Month)
                    {
                        Common.ShowError("Date is invalid!", 2, MessageBoxButtons.OK);                        
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("You must enter the period of time to back up", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                    chbfromdate_todate.Checked=true ;
                    return false;
                }            
            }
            return true;
        }

/*---------------------------------------------------------------
  * Muc dich         : Ham backup DB
  * Tra ve           : 
  * Ngay tao         : 05/08/2008
  * Nguoi tao        : HueMT
  * Ngay cap nhat    : 
  * Nguoi cap nhat   : 
  *--------------------------------------------------------------*/
        private  void Backup(string strPathFile, string strFileName, string strTable, string strQuery)
        {
            string strCmdLine;

            if (!Directory.Exists(strPathFile))
            {                
                Directory.CreateDirectory(strPathFile);
            }
            
            strQuery = strQuery.Trim();
            if (strQuery != "")
            {
                strQuery = " query=" + strQuery;
            }           

            strCmdLine = "/C " + " exp " + strUsername + "/" + strPassword + "@" + strDatabase + "  File=" + strPathFile + "\\" + strFileName +".dmp  tables=" + strTable + strQuery;
            process1.EnableRaisingEvents = false;
            process1.StartInfo.FileName = "CMD.exe";
            process1.StartInfo.Arguments = strCmdLine;
            process1.StartInfo.UseShellExecute = false;
            process1.StartInfo.RedirectStandardOutput = false ;//hien thi cua so cmd 
            process1.StartInfo.CreateNoWindow = false ;//tat cua so cmd sau khi exp xong
           
            process1.Start();
            process1.WaitForExit();
            objcontrolBktable.UpdateLASTEXP(DateTime.Now, strTable);
           
        }

        private void BackupThread(string strPathFile, string strFileName, string strTable, string strQuery)
        {
            //Guid RequestID= new Guid(
            mstrPathFile = strPathFile;
            mstrFileName = strFileName;
            mstrTable = strTable;
            mstrQuery = strQuery;
            Thread ts = new Thread(new ThreadStart(StartBackup));
            ts.Start();
        }

        private void StartBackup()
        {
            //blnflag = true;
            Backup(mstrPathFile, mstrFileName, mstrTable, mstrQuery);
            while (!IsProcessOpen("exp.exe"))
            {
                progressBar1.Increment(5);
                if (progressBar1.Value == progressBar1.Maximum)
                    progressBar1.Value = 0;
                Thread.Sleep(100);
            }
            progressBar1.Value = progressBar1.Maximum;
            MessageBox.Show("OK");
            progressBar1.Value = 0;
            //blnflag = false;
        }



        public bool IsProcessOpen(string name)
        {
            
            foreach (Process clsProcess in Process.GetProcesses())
            {                
                if (clsProcess.ProcessName.Contains(name))
                {                    
                    return true;
                }
            }            
            return false;
        }

        private void datetto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (datetto.Value < datetfrom.Value)
                {
                    this.datetto.Value = datetfrom.Value;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void rdoCurrentmonth_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoCurrentmonth.Checked)

                strBktime = Common.GW_BKTBL_BKTIME_EOD;

            else
                strBktime = Common.GW_BKTBL_BKTIME_EOY;

            getdatagrid();
            CheckBox_change(null, null);           
        }

        private void rdopriviousmonth_CheckedChanged(object sender, EventArgs e)
        {
            chbfromdate_todate.Checked = true;           
            CheckBox_change(null, null);               
        }

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape)&&(cmdClose.Enabled ))
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);                
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmBackupData_KeyDown(object sender, KeyEventArgs e)
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

        private void frmBackupData_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
