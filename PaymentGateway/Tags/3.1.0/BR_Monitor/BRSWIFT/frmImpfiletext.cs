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

namespace BR.BRSWIFT
{
    public partial class frmImpfiletext : Form
    {
        EXCELController objctrolExcel = new EXCELController();
        private static string pFilename = "";
        private static string pFoldername = "";
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
                f.Filter = "Text files | *.txt";

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
                int CountSusscess = 0;
                int count = 0;
                string pMessageDup = "";
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
                        int countDup = 0;
                        string FileName = file.Name;
                        if (FileName.ToUpper() == pFilename.ToUpper())
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
                                int msgLength = MsgContent.Length;
                                string beginMsg = "{";// Convert.ToChar(1).ToString() + Convert.ToChar(123).ToString();
                                //string beginMsg = " {";//Vi tri bat dau
                                string endMsg = "}$";//vi tri ket thuc

                                int countMsg = 0; // Bien dem so dien trong file
                                int indexPos = 0;// Bien luu vi tri
                                bool isError = false; // Bien luu trang thai file dien co bi loi hay khong
                                countMsg = 0;
                                while (indexPos != -1 && indexPos < msgLength)
                                {
                                    countMsg++; 
                                    int newPos;
                                    if (countMsg == 1)
                                    { newPos = 0; }
                                    else
                                    {
                                        newPos = MsgContent.IndexOf(beginMsg, indexPos);
                                    }
                                    if ((newPos == -1 || newPos == 0) && countMsg > 1) break;
                                    int newEndPos = MsgContent.IndexOf(endMsg, newPos);                                    
                                    string content;
                                    if (newEndPos == -1 && msgLength > 0)
                                    {
                                        newEndPos = msgLength;
                                        content = MsgContent.Substring(newPos);
                                    }
                                    else
                                    {
                                        content = MsgContent.Substring(newPos, newEndPos - newPos + 1);
                                    }
                                    if (newEndPos == -1) break;
                                    // Gan vi tri dien moi
                                    indexPos = newEndPos;
                                  
                                    #region Lay du lieu de check trung
                                    string vFIELD20 = GET_FIELD_SWIFT(content.Trim(), ":20:");
                                    string vFIELD21 = GET_FIELD_SWIFT(content.Trim(), ":21:");
                                    string vFIELD32A = GET_FIELD_SWIFT(content.Trim(), ":32A:");
                                    string vMSG_TYPE = "";
                                    String[] N = content.Trim().Split(new String[] { "{2:I" }, StringSplitOptions.None);//cat chuoi
                                    if (N[1] != null)
                                    {
                                        vMSG_TYPE = "MT" + N[1].Trim().Substring(0, 3);
                                    }
                                    string pKey = vFIELD20 + vFIELD21 + vFIELD32A + vMSG_TYPE;
                                    string strWhere = " where check_key = '" + pKey + "' and FILE_TYPE = 'TEXT'  ";
                                    #endregion
                                    DataTable _dt = new DataTable();
                                    _dt = objctrolExcel.Check_SWIFT_TEXT(strWhere);
                                    if (_dt.Rows.Count == 0)
                                    {
                                        try
                                        {
                                            if (objctrolExcel.AddSWIFT_TEXT(pKey, content.Trim(), file.Name, Common.Userid, "TEXT") == -1)
                                            {
                                                //MessageBox.Show("Import data error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                continue;
                                            }
                                            else
                                            {
                                                CountSusscess = CountSusscess + 1;
                                            }
                                           
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                if (objctrolExcel.AddSWIFT_TEXT(pKey, content.Trim(), file.Name, Common.Userid, "TEXT") == -1)
                                                {
                                                    continue;
                                                }
                                            }
                                            catch (Exception ex1)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        countDup = countDup + 1;
                                        if (pMessageDup == "")
                                        {
                                            pMessageDup = vFIELD20;
                                        }
                                        else
                                        {
                                            pMessageDup = pMessageDup + "\r\n" + vFIELD20;
                                        }
                                       // MessageBox.Show("Message duplicate!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }                                    
                                }
                                /*Update moi*/
                                //objctrolExcel.UPDATESWIFT_TEXT(file.Name, Common.Userid, "TEXT");
                                if (countDup == 0)
                                {
                                    MessageBox.Show(CountSusscess + ": Message import data successfull!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                }
                                else
                                {
                                    if (CountSusscess == 0)
                                    {
                                        MessageBox.Show(countDup + ": Mesaages duplicate " + "\r\n" + pMessageDup, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        MessageBox.Show(CountSusscess + ": Messages import data successfull!" + "\r\n" + countDup + ": Mesaages duplicate " + "\r\n" + pMessageDup, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                file.Refresh();
                                BackupFile(file);
                            }
                        }
                    }
                    catch (Exception ex)
                    {                       
                        continue;           
                    }

                }
                if (count == 0)
                {
                    MessageBox.Show("File is empty !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private string GET_FIELD_SWIFT(string pCONTENT,string pFIELD_CODE)
        {
            try
            {
                string pValue = "";
                string pCONTENT_N = "";
                String[] Message = pCONTENT.Split(new String[] { pFIELD_CODE }, StringSplitOptions.None);
                pCONTENT_N = Message[1].ToString();
                for (int i = 0; i < pCONTENT_N.Length; i++)
                {
                    string pchar = pCONTENT_N.Substring(i, 3);
                    if (pchar == "\r\n:")
                    {
                        pValue = pCONTENT_N.Substring(0, i).Replace("\r\n","");
                        break;
                    }
                }
                return pValue;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string Getdatetime()
        {
            try
            {
                string vFolderY;
                string pYear = DateTime.Now.Year.ToString();
                string pMonth = DateTime.Now.Month.ToString();
                string pDay = DateTime.Now.Day.ToString();
                if (pMonth.Length == 1)
                {
                    pMonth = "0" + pMonth;
                }
                if (pDay.Length == 1)
                {
                    pDay = "0" + pDay;
                }
                return vFolderY = pYear + pMonth + pDay;
            }
            catch 
            {
                return "";
            }
        }


        public void BackupFile(FileInfo file)
        {
            try
            {
                string vFolderY = Getdatetime();
                /**/
                // Khoi tao thu muc Backup
                // Constant.BACKUP_FOLDER: la ten thu muc Backup duoc dinh nghia trong lop Const
                string backupFolder = pFoldername + "\\Backup\\" + vFolderY;
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


    }
}
