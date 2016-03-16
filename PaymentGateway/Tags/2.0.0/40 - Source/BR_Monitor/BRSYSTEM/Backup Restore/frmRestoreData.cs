using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.IO;
using BR.BRLib;
using System.Diagnostics;

namespace BR.BRSYSTEM
{
    public partial class frmRestoreData : frmBasedata
    {
        #region khai bao cac ham va cac bien        
        public static int iDem = 0;//so dem
        public static bool isStop = false;
        public static Process process1 = new Process();

        private clsLog objLog = new clsLog();
        private BKTABLEInfo objBktable = new BKTABLEInfo();
        private BKTABLEController objcontrolBktable = new BKTABLEController();
        private OpenFileDialog Openfile = new OpenFileDialog();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
                        
        //private bool NeedConfirm = true;
        //private static bool strSuccess = true;
        //private static string strBktype;
        
        #endregion

        public frmRestoreData()
        {
            InitializeComponent();
        }

        private void frmRestoreData_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            Getdata();
        }
        /*---------------------------------------------------------------
         * Muc dich         : Lay du lieu tu bang BKTABLE len luoi
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 09/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getdata()
        {
            try
            {
                string strBKTYPE = "1";
                datTable_Re.Rows.Clear();
                DataSet datTable = new DataSet();
                datTable = objcontrolBktable.Get_data1(strBKTYPE);
                int i = 0;
                while (i < datTable.Tables[0].Rows.Count)
                {
                    datTable_Re.Rows.Add();
                    string strTableName = datTable.Tables[0].Rows[i]["SOURCETBL"].ToString();
                    datTable_Re.Rows[i].Cells[1].Value = strTableName;
                    datTable_Re.Columns[0].Width = 52;
                    datTable_Re.Columns[1].Width = 230;
                    datTable_Re.Columns[2].Width = 240;
                    datTable_Re.Columns[3].Width = 80;
                    datTable_Re.Rows[i].Cells[3].Value = "Browse...";
                    //datTable_Re.Columns[3].
                    datTable_Re.Rows[i].Cells[1].ReadOnly = true;
                    datTable_Re.Rows[i].Cells[0].Value = false;                    
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //bat su kien vao nut
        private void datTable_Re_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (datTable_Re.Columns[e.ColumnIndex].HeaderText == "Select")
                {
                    int iRows = e.RowIndex;
                    Openfile.ShowDialog();
                    datTable_Re.Rows[iRows].Cells[e.ColumnIndex - 1].Value = Openfile.FileName;                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //thoat khoi form
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*---------------------------------------------------------------
         * Muc dich         : Kiem tra luoi xem co du lieu duoc chon khong, neu chon thi bachup
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 09/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdRestore_Click(object sender, EventArgs e)
        {
            try
            {
                string Msg = "Do you want to restore table";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {                   
                    #region
                    int k = 0;
                    isStop = false;
                    cmdRestore.Enabled = false;
                    cmdClose.Enabled = false;
                    while (k < datTable_Re.Rows.Count)
                    {
                        if (datTable_Re.Rows[k].Cells[0].Value.ToString() == "True")
                        {
                            
                            string strFile_path = datTable_Re.Rows[k].Cells[2].Value.ToString();
                            String[] M = strFile_path.Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                            int r = M.Count<String>();
                            string strFi = M[r - 1];
                            string strDirectory = strFi.ToUpper();//chuyen thanh chu hoa
                            if (File.Exists(strFile_path) == true && strDirectory == "DMP")//kiem tra dung ten file hay khong va dung file.dmp hay khong
                            {
                                isStop = true;
                                cmdRestore.Enabled = false;
                                string strTable = datTable_Re.Rows[k].Cells[1].Value.ToString();
                                string strPathFile = datTable_Re.Rows[k].Cells[2].Value.ToString();
                                //------------------------->
                                //lay ra thong tin ket noi databases
                                string strDatabase = BR.BRBusinessObject.DATABASESInfo.strDatabase;
                                string strOracleServer = BR.BRBusinessObject.DATABASESInfo.strOracleServer;
                                string strUsername = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                                string strPassword = BR.BRBusinessObject.DATABASESInfo.strPassword;
                                //---------------------------------------
                                process1.EnableRaisingEvents = false;
                                string strCmdLine;
                                strCmdLine = "/C " + " imp " + strUsername + "/" + strPassword + "@" + strDatabase + "  File=" + strPathFile + " ignore=Y full = N tables=" + strTable;
                                process1.StartInfo.FileName = "CMD.exe";
                                process1.StartInfo.Arguments = strCmdLine;
                                process1.StartInfo.UseShellExecute = false;
                                process1.StartInfo.RedirectStandardOutput = false ;
                                process1.StartInfo.RedirectStandardInput = false;

                                
                                process1.StartInfo.CreateNoWindow = false ;
                                process1.Start();

                                //BackgroundWorker abc = new BackgroundWorker()
                                //abc.
                                process1.WaitForExit();
                                //---------------lay thong tin de ghilog----------------------
                                DateTime
                                    dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "Restore database";
                                int Log_level = 1;
                                string strWorked = "Restore_database";
                                string strTable1 = "DB";
                                string strOld_value = "";
                                string strNew_value = "";
                                //-----------------------------------------
                                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                    strWorked, strTable1, strOld_value, strNew_value);
                                //--------------------------------------------------
                                //------------------------->
                                
                            }

                        }

                        k = k + 1;
                    }
                    if (isStop)
                        MessageBox.Show("Restore successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Please choose a table and a file to backup!",Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Exclamation );

                    cmdRestore.Enabled = true;
                    cmdClose.Enabled = true;
                    #endregion
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        
        private void frmRestoreData_KeyDown(object sender, KeyEventArgs e)
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
                    if ((this.ActiveControl as Button).Name == "cmdRestore")
                    {
                        cmdRestore.Focus();
                        cmdRestore_Click(null, null);
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmRestoreData_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
