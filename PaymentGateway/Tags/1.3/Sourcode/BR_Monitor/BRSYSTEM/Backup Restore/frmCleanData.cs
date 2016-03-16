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
    public partial class frmCleanData : frmBasedata
    {
        #region khai bao bian va cac ham
        public static Process process1 = new Process();
        public static bool strSuccess = true;

        private clsLog objLog = new clsLog();
        private BKTABLEInfo objBktable = new BKTABLEInfo();
        private BKTABLEController objcontrolBktable = new BKTABLEController();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private DataGridViewButtonColumn datbt = new DataGridViewButtonColumn();
        private DataGridViewTextBoxColumn dattextb = new DataGridViewTextBoxColumn();
        private DataGridViewCheckBoxColumn datcheckb = new DataGridViewCheckBoxColumn();
        private DATABASE_CLEANController objDatacleans = new DATABASE_CLEANController();
        
        //private static int iDem=0;
        //private bool blnFile = false;
        private bool isStop = false;
        //private bool NeedConfirm = true;
        #endregion

        public frmCleanData()
        {
            InitializeComponent();
        }
        //ham load
        private void frmCleanData_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            Getdatagrid();
        }
      /*---------------------------------------------------------------
      * Muc dich         : lay du lieu ten cac table trong bang BKTYPE voi dieu kien mac dinh la backup trong thang
      * Tra ve           : Mot danh sach cac File - List<FileInfo>
      * Ngay tao         : 26/54/2008
      * Nguoi tao        : Quynd
      * Ngay cap nhat    : 09/06/2008
      * Nguoi cap nhat   : Quynd
      *--------------------------------------------------------------*/
        private void Getdatagrid()
        {
            try
            {
                string strBktimme =Common.GW_BKTBL_BKTIME_EOY ;
                //string strBktype = "2";
                dattable.Rows.Clear();
                DataSet datBkcurrent = new DataSet();
                //datBkcurrent = objcontrolBktable.GetBKTABLE1(strBktype, strBktimme);
                datBkcurrent = objcontrolBktable.GetBk_Bktime(strBktimme);//.GetBKTABLE1(strBktype, strBktimme);
                int b = 0;
                while (b < datBkcurrent.Tables[0].Rows.Count)
                {
                    dattable.Rows.Add();
                    dattable.Rows[b].Cells[0].Value = false;
                    dattable.Rows[b].Cells[1].Value = datBkcurrent.Tables[0].Rows[b]["DESTBL"].ToString();
                    dattable.Rows[b].Cells[2].Value = datBkcurrent.Tables[0].Rows[b]["FILEPATH"].ToString();
                    dattable.Rows[b].Cells[3].Value = datBkcurrent.Tables[0].Rows[b]["LASTCLEAN"].ToString();
                    dattable.Rows[b].Cells[4].Value = Convert.ToString(DateTime.Now);
                    b = b + 1;
                }
                dattable.Columns[0].Width = 50;
                dattable.Columns[1].Width = 150;
                dattable.Columns[2].Width = 200;
                dattable.Columns[3].Width = 170;
                dattable.Columns[4].Width = 170;
                
                dattable.Columns[1].ReadOnly = true;
                dattable.Columns[2].ReadOnly = true;
                dattable.Columns[3].ReadOnly = true;
                dattable.Columns[4].ReadOnly = true;
              //--------------------------------------
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
      * Muc dich         : Ham clean data
      * Tra ve           : Mot danh sach cac File - List<FileInfo>
      * Ngay tao         : 26/54/2008
      * Nguoi tao        : Quynd
      * Ngay cap nhat    : 09/06/2008
      * Nguoi cap nhat   : Quynd
      *--------------------------------------------------------------*/
        private void cmdclean_Click(object sender, EventArgs e)
        {
            try
            {
                string Msg = "Do you really want to Clean data";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    #region Ham clean data
                    //blnFile = true;
                    isStop = false;
                    cmdclean.Enabled = false;
                    cmdClose.Enabled = false;

                    int k = 0;
                    while (k < dattable.Rows.Count)// duyet tung ban ghi trong Luoi
                    {
                        if (dattable.Rows[k].Cells[0].Value.ToString() == "True")// dong duoc chon
                        {
                            string strFile_db = dattable.Rows[k].Cells[2].Value.ToString();
                            char[] charsToTrim = { '\\' };
                            strFile_db = strFile_db.TrimEnd(charsToTrim) + "\\";

                            if (!Directory.Exists(strFile_db))//kiem tra thu muc co ton tai khong
                            {
                                Directory.CreateDirectory(strFile_db);
                            }
                            //--------------//---------------
                            isStop = true;
                            //iDem = 0;
                            strSuccess = false;
                            isStop = true;
                            //----------------------------------
                            string strTable = dattable.Rows[k].Cells[1].Value.ToString();
                            string strPath = dattable.Rows[k].Cells[2].Value.ToString();
                            string strFileName = strTable + "_CL_" + DateTime.Now.ToString("yyyyMMdd") + ".dmp";

                            //lay thong tin Username va Password dang nhap vao database
                            string strDatabase = BR.BRBusinessObject.DATABASESInfo.strDatabase;
                            string strOracleServer = BR.BRBusinessObject.DATABASESInfo.strOracleServer;
                            string strUsername = BR.BRBusinessObject.DATABASESInfo.strUserDB;
                            string strPassword = BR.BRBusinessObject.DATABASESInfo.strPassword;
                            //---------------------------------------
                            process1.EnableRaisingEvents = false;
                            string strCmdLine;
                            strCmdLine = "/C " + " exp " + strUsername + "/" + strPassword + "@" + strDatabase;
                            strCmdLine = strCmdLine + "  File=" + strPath + "\\" + strFileName + "  tables=" + strTable + "";

                            process1.StartInfo.FileName = "CMD.exe";
                            process1.StartInfo.Arguments = strCmdLine;
                            process1.StartInfo.UseShellExecute = false;
                            process1.StartInfo.RedirectStandardOutput = false;
                            process1.StartInfo.CreateNoWindow = false;
                            process1.Start();

                            process1.WaitForExit();
                            //----------------goi ham xoa du lieu o bang do di------------
                            DataSet datClean = new DataSet();
                            datClean = objDatacleans.Delete_table(strTable);
                            //-------------------------------------------------------------
                            //------------------lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "backup table " + strPath + "Delete" + strTable;
                            int Log_level = 1;
                            string strWorked = "Back up data,delete";
                            string strTable1 = "dba_scheduler_jobs";
                            string strOld_value = strTable;
                            string strNew_value = "";
                            //-----------------------------------------------------------------
                            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                strWorked, strTable1, strOld_value, strNew_value);
                            //-----------------------------------------------------------------
                            //Update ngay back up
                            objcontrolBktable.UpdateField("LASTCLEAN", DateTime.Now.ToString("dd/MM/yyyy"), "DESTBL", strTable);
                        }

                        k = k + 1;
                    }

                    if (isStop)
                    {
                        MessageBox.Show("Clean data successfully", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Please choose tables to clean!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    cmdclean.Enabled = true;
                    cmdClose.Enabled = true;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
        * Muc dich         : bat su kien click len luoi(vao nut Browse)
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 09/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void dattable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dattable.Columns[e.ColumnIndex].HeaderText == "Browse")
                {
                    int iRows = e.RowIndex;
                    saveFileDialog1.ShowDialog();
                    dattable.Rows[iRows].Cells[e.ColumnIndex - 1].Value = saveFileDialog1.FileName;
                    //---------------------------------------------------------------------------
                    string strPath = saveFileDialog1.FileName;//lay duong dan file
                    string strFile = Path.GetFileName(strPath);//Lay ten file
                    String[] M = strFile.Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                    int k = M.Count<String>();
                    if (M[0] == "")
                    {
                    }
                    else
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

        private void frmCleanData_KeyDown(object sender, KeyEventArgs e)
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

        private void frmCleanData_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
