using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Threading;
using BR.BRIBPS.Technical;

namespace BR.BRIBPS.Branch
{
    public partial class frmExtractData : Form
    {
        public System.Windows.Forms.DateTimePicker pickerDate;
        
        
        
        public frmExtractData()
        {
            InitializeComponent();
        }
        /***********************************
         * Sự kiện Load Form hiện hành
         ***********************************/ 
        private void OnLoad(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico"); 
            DataSet ds;
            string vSQL = "";
            vSQL =   " SIBS_CODE TAD"
                 + ",(select count(1) from ibps_msg_content where to_char(trans_date, 'DDMMYYYY') = '" + pickerDate.Value.Date.ToString("ddMMyyyy") + "' and TAD = tad.SIBS_CODE )  CONTENT_"
                 + ",(select count(1) from ibps_msg_all     where to_char(trans_date, 'DDMMYYYY') = '" + pickerDate.Value.Date.ToString("ddMMyyyy") + "' and TAD = tad.SIBS_CODE) ALL_  "
                 + ",(select count(1) from ibps_msg_all_his where to_char(trans_date, 'DDMMYYYY') = '" + pickerDate.Value.Date.ToString("ddMMyyyy") + "' and TAD = tad.SIBS_CODE ) ALL_HIS_ "
                 + " from tad where status = 1 and DBLink is not null "; /*and connection = 1*/
            try{
            ds = GetData.GetSelect(vSQL);
            dtgTAD.DataSource = ds.Tables[0];
            }
            catch
            {
                dtgTAD.DataSource = null;
            }
            txtFilepath.Text = "C:\\";
        }
        /***********************************
         * Trích chọn dữ liệu cho CN
         ***********************************/
        private void OnExtract(object sender, EventArgs e)
        {
            try
            {
                int isDblink = 0;

                Technical.clsThread.listTAD.Clear();// Xoa du lieu trong list
                for (int i = 0; i < dtgTAD.Rows.Count; i++)
                {
                    if (dtgTAD.Rows[i].Cells["TAD"].ToString() != "")
                    {
                        Technical.clsThread.listTAD.Add(dtgTAD.Rows[i].Cells["TAD"].Value.ToString());
                        Technical.clsThread.pDate = pickerDate.Value;
                    }
                }
                // Khởi tạo Thread
                if (chkDBlink.Checked)
                {
                    isDblink = 1;
                    ThreadStart method = new ThreadStart(clsThread.ExtractData);
                    Thread thrd = new Thread(method);
                    thrd.Start();
                }
                else
                {
                    isDblink = 2;
                    clsThread.ExtractData(txtFilepath.Text, isDblink);
                }
                //MessageBox.Show("Extract", BR.BRLib.Common.sCaption);
            }
                
            catch { MessageBox.Show("Extract => Error", BR.BRLib.Common.sCaption); };
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnDate(object sender, EventArgs e)
        {
            OnLoad(sender,e);
        }

        private void txtFilepath_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkDBlink_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDBlink.Checked)
            {
                txtFilepath.Enabled = true;
                cmdBrows.Enabled = true;
            }
            else
            {
                txtFilepath.Enabled = true;
                cmdBrows.Enabled = true;
            }
        }

     

        private void cmdBrows_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog f = new FolderBrowserDialog();
                //f.InitialDirectory = Application.ExecutablePath;
               // f.Filter = "Text files | *.txt | ALL | *.*";

                if (f.ShowDialog() == DialogResult.OK)
                {
                    txtFilepath.Text = f.SelectedPath;
                    //if (f.FileName != null && f.CheckFileExists == true)
                    //{
                    //    this.txtFilepath.Text = f.FileName;
                    //    pFilename = f.SafeFileName;
                    //    pFoldername = f.FileName.Replace("\\" + pFilename, "");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("File name is null !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }

            }
            catch (Exception ex)
            {
                // Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }
}
