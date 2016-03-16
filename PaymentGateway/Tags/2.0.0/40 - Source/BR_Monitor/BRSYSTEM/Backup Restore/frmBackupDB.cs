using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Text.RegularExpressions;
using BR.BRLib;
using System.IO;
using System.Threading;
using System.Timers;
using System.Net;

namespace BR.BRSYSTEM
{
    public partial class frmBackupDB : frmBasedata
    {
        #region khai bao cac ham va bien
        clsFile classfile = new clsFile();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        DATABASESInfo objdatabase = new DATABASESInfo();
        TADInfo objtab = new TADInfo();
        TADController objcontrolt = new TADController();        
        public static bool isStop = false;        
        public static int mn = 0;
       // private bool NeedConfirm = true;
        public static System.Diagnostics.Process process1 = new System.Diagnostics.Process();
        public static bool bSuccess=true;
        public static string pathFile = "";
        private int iProcess;
        public static bool iClose = true;
        #endregion

        public frmBackupDB()
        {
            InitializeComponent();
        }

        private void cmdclose_Click(object sender, EventArgs e)
        {
            try
            {               
                    this.Close();               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : Ham ghi log
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 06/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/


        
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

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //"dmp files (*.dmp)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.Filter = "dmp files (*.dmp)|*.dmp";//dat kieu duoi cho file gi lai
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                txtPathfile.Text = saveFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //Ham kiem tra dau vao----------------------------------------------------
        private bool checkInput()
        {
            string strFile = "DMP";
            bool isResult = false;
            if (txtPathfile.Text == "")
            {
                Common.ShowError("Path of file is null!", 3, MessageBoxButtons.OK);                
                return false;
            }

            if (txtPathfile.Text.Contains("'") || txtPathfile.Text.Contains("''"))
            {
                Common.ShowError("Path includes invalid character!", 
                    3, MessageBoxButtons.OK);                
            }

            String strTextpath = txtPathfile.Text;
            String[] M = strTextpath.Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
            int k = M.Count<String>();
            if (k > 2)
            {
                isResult= false;
            }
            if (k == 1)
            {
                isResult= false;
            }
            if (k == 2)
            {
                if (M[1].ToUpper() != strFile)//kiem tra xem co dung chuan .dmp khong
                {
                    isResult= false;
                }
                else
                {
                    isResult= true;
                    //kiem tra khi cat chuoi vowi ki tu \

                }
            }
      return isResult;
        }
        /*---------------------------------------------------------------
        * Muc dich         : goi ham back updatabase
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 31/54/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdbackup_Click(object sender, EventArgs e)
        {            
            if (checkInput())
            {
                pathFile = txtPathfile.Text;
                string Msg = "Do you really want to Backup database";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    //goi ham tinh thoi gian
                    bSuccess = false;
                    this.timer1.Enabled = true;
                    this.timer1.Interval = 2000;
                    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);   
                    //------------------------------------------------------------
                    cmdbackup.Enabled = false; txtPathfile.ReadOnly = true;
                    iProcess = 1; lbPlease.Visible = true; cmdBrowse.Enabled = false;
                    Application.DoEvents();
                    Cursor.Current = Cursors.WaitCursor; 
                    osProgress1.AutoProgress = true;
                    ThreadStart method = new ThreadStart(frmBackupDB.Backupdb);
                    Thread thrd = new Thread(method);
                    thrd.Start();
                    if (thrd.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        thrd.Abort();
                    }
                    #region lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Backup database";
                    int Log_level = 1;
                    string strWorked = "Backup database";
                    string strTable = "";
                    string strOld_value = "";
                    string strNew_value = "";
                    Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                    #endregion
                    this.ControlBox = true;                                       
                }
                else { Cursor.Current = Cursors.Default; }
            }           
        }

        public static void Backupdb()
        {
            try
            {   
                #region Lay thong tin dang nhap vao database
                string strDatabase = BR.BRBusinessObject.DATABASESInfo.strDatabase;
                string strOracleServer = BR.BRBusinessObject.DATABASESInfo.strOracleServer;
                string strUsername = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                string strPassword = BR.BRBusinessObject.DATABASESInfo.strPassword;
                #endregion
                process1.EnableRaisingEvents = false;
                string strCmdLine;
                strCmdLine = "/C " + " exp " + strUsername + "/" + strPassword + "@" + strDatabase + "  File=" + pathFile + "  Owner=" + strUsername + "  Statistics=none ";
                process1.StartInfo.FileName = "CMD.exe";//goi cua so command line
                process1.StartInfo.Arguments = strCmdLine;//truyen cau lenh cho cua so command line
                process1.StartInfo.UseShellExecute = false;//dat thuoc tinh cho command line
                process1.StartInfo.RedirectStandardOutput = false;
                process1.StartInfo.CreateNoWindow = true;//an cua so command line
                process1.Start();//bat dau chay cau lenh bang command line                   
                process1.WaitForExit();//bat su kien Exit cua cua so command line   
                bSuccess = false;
                if (iClose == true)
                { Common.ShowError("Backup database successfully!", 1, MessageBoxButtons.OK); }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            finally
            {
                Thread.CurrentThread.Abort();                
            }
        }
     

        private void frmBackup_database_Load(object sender, EventArgs e)
        {
            try
            {
                iClose = true;
                iProcess = 0; lbPlease.Visible = false; 
                cmdAdd.Visible = false; cmdSave.Visible = false;
                cmdDelete.Visible = false; cmdEdit.Visible = false;
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                string dsds = "hdjshd";
                string strTest1 = dsds.ToUpper();//chuyen chuoi thanh chuoi chu hoa
                frmBackupDB frmbup = new frmBackupDB();
                frmbup.MaximizeBox = false;
                LbPro.Text = "";
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

        private void frmBackupDB_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
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

        private void frmBackupDB_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmBackupDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iProcess == 1) 
            {
                e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit this process!", Common.sCaption);                
                //Common.ShowError("Can not close form because it is in process!", 3, MessageBoxButtons.OK); 
                if (e.Cancel == false)//neu chon yes(Thoat form)
                    //if()
                    bSuccess = false;
                string sHostName = Dns.GetHostName();
                System.Diagnostics.Process[] dg = System.Diagnostics.Process.GetProcesses(sHostName);
                if (System.Diagnostics.Process.GetProcessesByName("EXP", sHostName).Length > 0)
                {                   
                    int j = 1;
                    while (j < System.Diagnostics.Process.GetProcesses(sHostName).Count())
                    {
                        if (dg[j].ProcessName.ToUpper()=="EXP")
                        {
                            dg[j].Kill();
                            iClose = false;
                            classfile.Delete(txtPathfile.Text.Trim());
                        }                        
                        j = j + 1;
                    }
                }  
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (bSuccess == false)
                {
                    osProgress1.AutoProgress = false;
                    Cursor.Current = Cursors.Default;
                    txtPathfile.ReadOnly = false; iProcess = 1;
                    cmdbackup.Enabled = true; cmdBrowse.Enabled = true;
                    bSuccess = true;
                }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
       
    }
}
