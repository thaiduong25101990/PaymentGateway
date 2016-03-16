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

namespace BR.BRSYSTEM
{
    public partial class frmReportList : BR.BRSYSTEM.frmBasedata
    {
        RPTRULEController objRuleMenuRpt = new RPTRULEController();
        //private RPTRULEController objRule = new RPTRULEController();
        private RPTMASTERInfo objrpt = new RPTMASTERInfo();
        private RPTMASTERController controlRPTMASTER = new RPTMASTERController();
        private RPTMASTERDP objrptmaster = new RPTMASTERDP();
        public static bool isInsert = false;
        private string strID;
        public static string strContent;

        public frmReportList()
        {
            InitializeComponent();

        }

        private void frmReportList_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            cmdSave.Visible = false;
            //cmdDelete.Enabled = false;
            //cmdEdit.Enabled = false;
            GetRuleMenuRpt();
            LoadDataGrid();
            LoadRuleReport();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isInsert = true;
            BR.BRSYSTEM.frmMenegerReport frmManegerRpt = new frmMenegerReport();
            frmManegerRpt.ShowDialog();
            cmdDelete.Enabled = true;
            cmdEdit.Enabled = true;
            cmdAdd.Enabled = true;
            cmdPermission.Enabled = true; 
            LoadDataGrid();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            isInsert = false;
            frmMenegerReport frmMenegerRpt = new frmMenegerReport();
            frmMenegerRpt.objrpt.RPTNAME = dgwRPT.CurrentRow.Cells[0].Value.ToString();
            frmMenegerRpt.objrpt.GWTYPE = dgwRPT.CurrentRow.Cells[1].Value.ToString();
            frmMenegerRpt.objrpt.DESCRIPTION = dgwRPT.CurrentRow.Cells[2].Value.ToString();
            strContent = dgwRPT.CurrentRow.Cells[0].Value.ToString();
            frmMenegerRpt.ShowDialog();
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = true;
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = controlRPTMASTER.GetRPTMASTER();
                dgwRPT.DataSource = datDs.Tables[0];
                //dgwRPT.Columns[0].Visible = false;
                dgwRPT.Columns[0].HeaderText = "Report Name";
                dgwRPT.Columns[0].Width = 120;
                dgwRPT.Columns[1].HeaderText = "Report Type";
                dgwRPT.Columns[1].Width = 90;
                dgwRPT.Columns[2].HeaderText = "Description";
                dgwRPT.Columns[2].Width = dgwRPT.Width -  210;
                dgwRPT.Enabled = true;
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                    cmdPermission.Enabled = false;
                    dgwRPT.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            strID = dgwRPT.CurrentRow.Cells[0].Value.ToString();
            try
            {
                if (Common.iSconfirm == 1)
                {
                    objrptmaster.DeleteRPTMASTER(strID);                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LoadDataGrid();
        }

        private void cmdPermission_Click(object sender, EventArgs e)
        {
            frmRepostRule frmRptRule = new frmRepostRule();
            strContent = dgwRPT.CurrentRow.Cells[0].Value.ToString();
            frmRptRule.ShowDialog();
        }
        private void LoadRuleReport()
        {
            int iGroup = Convert.ToInt32(Common.Groupid.ToString());

        }

        private void dgwRPT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // CheckRule();
        }
        private void GetRuleMenuRpt()
        {
            //bool inquiry=false;
            //bool delete = false;
            //bool insert = false;
            //bool edit = false;
            int numinquiry = 0;
            int numdelete = 0;
            int numinsert = 0;
            int numedit = 0;
            string gwType = Common.strGWTYPE;
            gwType = gwType.Trim();
            string userID = Common.Userid.ToString();
            userID = userID.Trim();
            DataSet dt = new DataSet();
            dt = objRuleMenuRpt.GetRuleMenuRpt(userID, gwType);
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Tables[0].Rows[i]["ISINQUIRY"].ToString()) == 1)
                    {
                        numinquiry = numinquiry + 1;
                    }
                    if (Convert.ToInt32(dt.Tables[0].Rows[i]["ISDELETE"].ToString()) == 1)
                    {
                        numdelete = numdelete + 1;
                    }
                    if (Convert.ToInt32(dt.Tables[0].Rows[i]["ISEDIT"].ToString()) == 1)
                    {
                        numedit = numedit + 1;
                    }
                    if (Convert.ToInt32(dt.Tables[0].Rows[i]["ISINSERT"].ToString()) == 1)
                    {
                        numinsert = numinsert + 1;
                    }

                }
            }
            //else
            //{
            //    inquiry = false;
            //    delete = false;
            //    edit = false;
            //    insert = false;
            //}
            if (numdelete > 0)
            {
                //delete = true;
                cmdDelete.Enabled = true;
            }
            else
            {
                cmdDelete.Enabled = false;
            }
            if (numedit > 0)
            {
                //edit = true;
                cmdEdit.Enabled = true;
            }
            else
            {
                cmdEdit.Enabled = false;
            }
            if (numinsert > 0)
            {
                //insert = true;
                cmdAdd.Enabled = true;
            }
            else
            {
                cmdAdd.Enabled = false;
            }
            //if (numinquiry > 0)
            //{
            //    inquiry = true;
            //}
            //else
            //{
            //    inquiry = false;
            //}
        }

        private void frmReportList_KeyDown(object sender, KeyEventArgs e)
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

        private void frmReportList_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}