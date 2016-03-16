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


namespace BR.BRSYSTEM
{
    public partial class frmRestoreDB : frmBasedata
    {
        private clsLog objLog = new clsLog();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private TADInfo objtab = new TADInfo();
        private TADController objcontrolt = new TADController();

        public frmRestoreDB()
        {
            InitializeComponent();
        }
        /*---------------------------------------------------------------
         * Muc dich         : Restore database
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 07/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdRestore_Click(object sender, EventArgs e)
        {
            try
            {
                string Msg = "Do you want to restore?";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {

                    string strPath_file = txtfile.Text;
                    String[] M = strPath_file.Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                    int r = M.Count<String>();
                    string strFi = M[r - 1];
                    string strDirectory = strFi.ToUpper();//chuyen thanh chu hoa
                    if (File.Exists(strPath_file) == true && strDirectory == "DMP")//kiem tra su ton tai cua file va dinh dang cua file
                    {
                        //lay ra thong tin ket noi databases
                        string strDatabase = BR.BRBusinessObject.DATABASESInfo.strDatabase;
                        string strOracleServer = BR.BRBusinessObject.DATABASESInfo.strOracleServer;
                        string strUsername = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                        string strPassword = BR.BRBusinessObject.DATABASESInfo.strPassword;

                        //---------------------------------------
                        System.Diagnostics.Process process1;
                        process1 = new System.Diagnostics.Process();
                        process1.EnableRaisingEvents = false;

                        string strCmdLine;
                        strCmdLine = "/C " + " imp " + strUsername + "/" + strPassword + "@" + strDatabase + "  File=" + txtfile.Text + "  Owner=" + strDatabase + "  Statistics=none ";
                        process1.StartInfo.FileName = "CMD.exe";
                        process1.StartInfo.Arguments = strCmdLine;
                        process1.StartInfo.UseShellExecute = false;
                        process1.StartInfo.RedirectStandardOutput = true;
                        process1.StartInfo.CreateNoWindow = true;
                        process1.Start();
                        //lay thong tin de ghilog----------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "";
                        int Log_level = 1;
                        string strWorked = "Restore Database";
                        string strTable = "";
                        string strOld_value = "";
                        string strNew_value = "";
                        //-----------------------------------------
                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                            strWorked, strTable, strOld_value, strNew_value);
                        //--------------------------------------------------
                    }
                    else
                    {
                        MessageBox.Show("Format of file is invalid!", Common.sCaption);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
         * Muc dich         : Ham lay duong dan cung nhu file de back up
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 09/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdBrows_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != "")
            {
                txtfile.Text = openFileDialog1.FileName;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRestoreDB_KeyDown(object sender, KeyEventArgs e)
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

        private void frmRestoreDB_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmRestoreDB_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
        }

    }
}
