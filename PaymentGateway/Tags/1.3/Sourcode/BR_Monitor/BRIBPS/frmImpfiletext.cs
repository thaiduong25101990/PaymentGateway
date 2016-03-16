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
using System.IO;
using System.Text.RegularExpressions;

namespace BR.BRIBPS
{
    public partial class frmImpfiletext : Form
    {
        EXCELController objctrolExcel = new EXCELController();
        private static string pFoldername = "";
        private static string pFilename = "";

        public frmImpfiletext()
        {
            InitializeComponent();
        }

        private void frmImpfiletext_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
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

        private void cmdBrows_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog();
                f.InitialDirectory = Application.ExecutablePath;
                f.Filter = "Text files | *.txt | ALL | *.*";

                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (f.FileName != null && f.CheckFileExists == true)
                    {
                        this.txtFilepath.Text = f.FileName;
                        pFilename = f.SafeFileName;
                        pFoldername = f.FileName.Replace("\\" + pFilename, "");
                    }
                    else
                    {
                        MessageBox.Show("File name is null !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {                
                int count = 0;
                DirectoryInfo dir = new DirectoryInfo(pFoldername);
                List<FileInfo> list = new List<FileInfo>();
                list.Clear();
                list = clsImport.ScanFile(pFoldername);
                if (list == null || list.Count == 0)
                {
                    //MessageBox.Show("File not found in folder!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    FileInfo file = list[i];
                    try
                    {
                        string FileName = file.Name;                       
                        if (FileName.ToUpper().Trim() == pFilename.ToUpper().Trim())
                        {
                            count = 1;
                            string MsgContent = "";
                            MsgContent = clsImport.OpenFile(file.FullName);
                            if (string.IsNullOrEmpty(MsgContent))
                            {
                                MessageBox.Show("File is empty !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                clsImport.MoveFile(file, pFoldername + @"\Error");
                                break;
                            }
                            else
                            {
                                string strWhere = "";
                                string pKey = "";
                                string p110 = "";
                                string p026 = "";
                                string p027 = "";
                                //Lay mot so truong lam key de check trung
                                String[] Message = MsgContent.Split(new String[] { "#" }, StringSplitOptions.None);
                                int iCount = Message.Count<String>();
                                for (int k = 0; k < iCount; k++)
                                {
                                    #region lay du lieu lam key
                                    if (Message[k].ToString().Trim() != "")
                                    {
                                        string pVlue = Message[k].ToString().Substring(0, 3);
                                        if (pVlue == "110")
                                        {
                                            p110 = Message[k].ToString().Substring(3);
                                        }
                                        else if (pVlue == "026")
                                        {
                                            p026 = Message[k].ToString().Substring(3);
                                        }
                                        else if (pVlue == "027")
                                        {
                                            p027 = Convert.ToString(Convert.ToDouble(Message[k].ToString().Substring(3)));
                                        }
                                    }
                                    #endregion
                                }
                                pKey = p110 + p026 + p027;
                                strWhere = " where check_key = '" + pKey + "' and FILE_TYPE = 'TEXT'  ";
                                DataTable _dt = new DataTable();
                                _dt = objctrolExcel.Check_Excel_IBPS(strWhere);
                                if (_dt.Rows.Count == 0)//dien nay khong bi trung
                                {
                                    if (objctrolExcel.AddExcel_IBPS(pKey, MsgContent, FileName, Common.Userid, "TEXT") == -1)
                                    {
                                        MessageBox.Show("Import data error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                    else
                                    {
                                        
                                        MessageBox.Show("Import file text successfull!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    }

                                }
                                else

                                {
                                    MessageBox.Show("Message duplicate!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                file.Refresh();
                                BackupFile(file);
                            }
                        }
                        //if (count == 0)
                        //{
                        //    MessageBox.Show("File is empty "+vv+"!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                    }
                    catch (Exception ex)
                    {                       
                        continue;           
                    }

                }
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        public void BackupFile(FileInfo file)
        {
            try
            {
                // Khoi tao thu muc Backup
                // Constant.BACKUP_FOLDER: la ten thu muc Backup duoc dinh nghia trong lop Const
                string backupFolder = pFoldername + "\\Backup";
                DirectoryInfo DirBackup = new DirectoryInfo(backupFolder);

                if (!DirBackup.Exists)   // Neu thu muc chua ton tai thi Create
                    DirBackup.Create();

                clsImport.CheckFileExists(file, DirBackup.FullName, file.Name);
                // Goi ham Move file den thu muc Backup
                file.MoveTo(DirBackup.FullName + "\\" + file.Name);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtFilepath_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
