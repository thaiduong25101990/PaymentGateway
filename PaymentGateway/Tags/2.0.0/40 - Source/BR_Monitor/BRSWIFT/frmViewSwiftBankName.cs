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

namespace BR.BRSWIFT
{
    public partial class frmViewSwiftBankName : Form
    {
        public SWIFT_BANK_MAPInfo objInfo = new SWIFT_BANK_MAPInfo();
        public SWIFT_BANK_MAPController objControl = new SWIFT_BANK_MAPController();

        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        public frmViewSwiftBankName()
        {
            InitializeComponent();
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            try
            {
                objInfo.SIBS_BANK_CODE = dgView.CurrentRow.Cells[1].Value.ToString();
                objInfo.BANK_NAME = dgView.CurrentRow.Cells[3].Value.ToString();
                if (dgView.CurrentRow.Cells[1].Value.ToString().Trim() == "LT" || dgView.CurrentRow.Cells[1].Value.ToString().Trim() == "TC")
                {  
                }
                else
                {
                    objInfo.SWIFT_BANK_CODE = dgView.CurrentRow.Cells[2].Value.ToString();
                }               
                this.Close();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewSwiftBankName_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                DataSet datDs = new DataSet();
                datDs = objControl.GetSWIFT_BANK_MAP_ReceivedBranch();
                //dgView.DataSource = datDs.Tables[0];khong gan source nua
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    dgView.Rows.Add();
                    dgView.Rows[0].Cells[0].Value = "";
                    dgView.Rows[0].Cells["SIBSbankcode"].Value = "LT";
                    dgView.Rows[0].Cells["Bankname"].Value = "LUU TRU";
                    dgView.Rows.Add();
                    dgView.Rows[1].Cells[0].Value = "";
                    dgView.Rows[1].Cells["SIBSbankcode"].Value = "TC";
                    dgView.Rows[1].Cells["Bankname"].Value = "THU CONG";
                    int i = 1;
                    while (i < datDs.Tables[0].Rows.Count)
                    {
                        dgView.Rows.Add();
                        dgView.Rows[i+1].Cells["ID"].Value = datDs.Tables[0].Rows[i]["BANK_MAP_ID"].ToString();
                        dgView.Rows[i+1].Cells["SIBSbankcode"].Value = datDs.Tables[0].Rows[i]["SIBS_BANK_CODE"].ToString();
                        dgView.Rows[i+1].Cells["SWIFTbankcode"].Value = datDs.Tables[0].Rows[i]["SWIFT_BANK_CODE"].ToString();
                        dgView.Rows[i+1].Cells["Bankname"].Value = datDs.Tables[0].Rows[i]["BANK_NAME"].ToString();
                        i = i + 1;
                    }
                    dgView.Columns["ID"].Visible = false;
                    dgView.Columns[1].HeaderText = "SIBS bank code";
                    dgView.Columns[1].Width = 120;
                    dgView.Columns[2].HeaderText = "SWIFT bank code";
                    dgView.Columns[2].Width = 120;
                    dgView.Columns[3].HeaderText = "Bank name";
                    dgView.Columns[3].Width = 300;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                objInfo.SIBS_BANK_CODE = dgView.CurrentRow.Cells[1].Value.ToString();
                objInfo.BANK_NAME = dgView.CurrentRow.Cells[3].Value.ToString();               
                if (dgView.CurrentRow.Cells[1].Value.ToString().Trim() == "LT" || dgView.CurrentRow.Cells[1].Value.ToString().Trim() == "TC")
                {                    
                }
                else
                {
                    objInfo.SWIFT_BANK_CODE = dgView.CurrentRow.Cells[2].Value.ToString();
                }              
                this.Close();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }

        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {            
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                cmdSelect_Click(null, null);
            }
        }

        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }
      
        private void frmViewSwiftBankName_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmViewSwiftBankName_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
