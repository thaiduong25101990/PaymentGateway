using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using BR.BRLib;
using BR.BRBusinessObject;
//using BR.DataAccess;
using System.Data.OracleClient;


namespace BR.BRIBPS
{
    public partial class frmView : Form
    {
        public  IBPS_BANK_MAPInfo objInfo = new IBPS_BANK_MAPInfo();
        public BRANCHInfo objInfoBRANCH = new BRANCHInfo();
        public IBPS_BANK_MAPController objControl = new IBPS_BANK_MAPController();
        private SYSVARController objControlSYSVAR = new SYSVARController();
        private BRANCHDP objBRANCHDP = new BRANCHDP();
        private USER_MSG_LOGInfo objInfoUSER_MSG_LOG = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objControlUSER_MSG_LOG = new USER_MSG_LOGController();
        private int length = 0;
        public string strCitadID = "";

        private bool NeedConfirm = true;
        private static bool strSucess = false;

        //private frmTADInfo TADInfo = new frmTADInfo();
        public frmView()
        {
            InitializeComponent();
        }

        private void frmView_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                dgView.Focus();
                if (frmTADInfo.isTADInfo)
                {
                    LoadData();
                }
                if (frmIBPSBankMap.isIBPSBankMap)
                {
                    LoadBranch();
                }
                if (frmTADInfo.isTAD)
                {
                    this.Text = "Log information";
                    cmdSelect.Visible = false;
                    LoadDataTAD_USER_MSG_LOG();
                }
                if (frmIBPSBankMap.isIBPSBankMapLog)
                {
                    this.Text = "Log information";
                    cmdSelect.Visible = false;
                    LoadDataIBPSBankMap_USER_MSG_LOG();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void LoadData()
        {
            try
            {
                string strLength = "SIBSBankCodeLength";
                DataSet ds = new DataSet();
                ds = objControlSYSVAR.GetIBPSBankLength(strLength);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    length = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                }
                //length =5;
                DataSet datDs = new DataSet();
                datDs = objControl.GetIBPS_BANK_MAPInfo(length);
                FomatGrid.ColumsHeaderDataGridView(dgView);
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    dgView.DataSource = datDs.Tables[0];
                    //dgView.Columns[0].HeaderText = "State bank code";
                    dgView.Columns[0].Width = 110;
                    //dgView.Columns[1].HeaderText = "SIBS bank code";
                    dgView.Columns[1].Width = 110;
                    //dgView.Columns[2].HeaderText = "Bank name";
                    dgView.Columns[2].Width = 250;
                    //dgView.Columns[3].HeaderText = "Branch name";
                    dgView.Columns[3].Width = 250;
                    dgView.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        // Bichnn add 03/08/2008
        private void LoadBranch()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = objBRANCHDP.GetData(); 
                FomatGrid.ColumsHeaderDataGridView(dgView);
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    DataTable dt = new DataTable ();
                    DataRow row ;
                    dt = datDs.Tables[0];
                    row = dt.NewRow();
                    row[0] = "-1";
                    row[1] = "OTHER BANK";
                    dt.Rows.Add(row);
                    dt.DefaultView.Sort = "SIBS_BANK_CODE";
                    dgView.DataSource = dt;
                    dgView.Columns[0].Width = 110;
                    dgView.Columns[1].Width = 250;
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadDataTAD_USER_MSG_LOG()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = objControlUSER_MSG_LOG.GetViewLogInfo("IBPS TAD list");
                FomatGrid.ColumsHeaderDataGridView(dgView);
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    dgView.DataSource = datDs.Tables[0];
                    //dgView.Columns[0].HeaderText = "State bank code";
                    dgView.Columns[0].Width = 110;
                    //dgView.Columns[1].HeaderText = "SIBS bank code";
                    dgView.Columns[1].Width = 70;
                    //dgView.Columns[2].HeaderText = "Bank name";
                    dgView.Columns[2].Width = 70;
                    //dgView.Columns[3].HeaderText = "Branch name";
                    dgView.Columns[3].Width = 600;
                    dgView.Columns[4].Width = 600;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadDataIBPSBankMap_USER_MSG_LOG()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = objControlUSER_MSG_LOG.GetViewLogInfo("IBPS bank list");
                FomatGrid.ColumsHeaderDataGridView(dgView);
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    dgView.DataSource = datDs.Tables[0];
                    //dgView.Columns[0].HeaderText = "State bank code";
                    dgView.Columns[0].Width = 110;
                    //dgView.Columns[1].HeaderText = "SIBS bank code";
                    dgView.Columns[1].Width = 70;
                    //dgView.Columns[2].HeaderText = "Bank name";
                    dgView.Columns[2].Width = 70;
                    //dgView.Columns[3].HeaderText = "Branch name";
                    dgView.Columns[3].Width = 600;
                    dgView.Columns[4].Width = 600;
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

        private void cmdSelect_Click(object sender, EventArgs e)
        {
           
            if (frmTADInfo.isTADInfo == true)
            {
                objInfo.GW_BANK_CODE = dgView.CurrentRow.Cells[0].Value.ToString();
                objInfo.BANK_NAME = dgView.CurrentRow.Cells[2].Value.ToString();
                objInfo.SIBS_BANK_CODE = dgView.CurrentRow.Cells[1].Value.ToString();
                objInfoBRANCH.BRAN_NAME = dgView.CurrentRow.Cells[3].Value.ToString();
                strCitadID  = dgView.CurrentRow.Cells[4].Value.ToString();                
                frmTADInfo.isTADInfo = false;
                this.Close();
            }
            if (frmIBPSBankMap.isIBPSBankMap == true)
            {
                objInfoBRANCH.BRAN_NAME = dgView.CurrentRow.Cells["BRAN_NAME"].Value.ToString();
                objInfoBRANCH.SIBS_BANK_CODE = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString();                
                frmIBPSBankMap.isIBPSBankMap = false; 
                this.Close();
            }
        }

        private void dgView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (frmTADInfo.isTADInfo == true)
            {
                objInfo.GW_BANK_CODE = dgView.CurrentRow.Cells[0].Value.ToString();
                objInfo.BANK_NAME = dgView.CurrentRow.Cells[2].Value.ToString();
                objInfo.SIBS_BANK_CODE = dgView.CurrentRow.Cells[1].Value.ToString();
                objInfoBRANCH.BRAN_NAME = dgView.CurrentRow.Cells[3].Value.ToString();
                strCitadID = dgView.CurrentRow.Cells[4].Value.ToString();
                frmIBPSBankMap.isIBPSBankMap = false; 
                this.Close();
            }
            if (frmIBPSBankMap.isIBPSBankMap)
            {
                objInfoBRANCH.BRAN_NAME = dgView.CurrentRow.Cells["BRAN_NAME"].Value.ToString();
                objInfoBRANCH.SIBS_BANK_CODE = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString();
                frmTADInfo.isTADInfo = false;
                this.Close();
            }
        }
        private void frmView_KeyDown(object sender, KeyEventArgs e)
        {
            {
                Common.bTimer = 1;
                if (e.KeyData == Keys.Escape)
                    this.Close();
            }
        }
        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
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

        private void frmView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess)
                {
                    if (NeedConfirm)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            cmdSelect_Click(null, null);
        }

        private void dgView_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmView_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
