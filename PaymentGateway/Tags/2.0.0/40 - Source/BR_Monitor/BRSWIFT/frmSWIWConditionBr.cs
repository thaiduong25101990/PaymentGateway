using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRSWIFT
{
    public partial class frmSWIWConditionBr : Form
    {
        #region ham va bien
        public static bool isInsert = false;
        private DataSet datDs = new DataSet();
        private SWIFT_BRANCH_ACTION_Info objInfo = new SWIFT_BRANCH_ACTION_Info();
        private SWIFT_BRANCH_ACTIONController objControl = new SWIFT_BRANCH_ACTIONController();       
        private clsCheckInput checkInput = new clsCheckInput();
        #endregion

        public frmSWIWConditionBr()
        {
            InitializeComponent();
        }

        private void tlbNew_Click(object sender, EventArgs e)
        {
            frmSWIWBrAuto SwiftAutoBranch = new frmSWIWBrAuto();
            this.Hide();
            SwiftAutoBranch.ShowDialog(this);
            this.Show();
            ///////////////////////////////////////-/
            //Muc dich: Load lai du lieu sau khi sua            
            //Ngay sua: 01/08/2008
            LoadData();
            /////////////////////////////////////////
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////-/
            //Muc dich: Kiem tra dgdSCBranch.rowcount<=0 
            //          => exit            
            //Ngay sua: 01/08/2008            
            if (dgdSCBranch.RowCount <= 0)
            {
                return;
            }
            if (dgdSCBranch.CurrentRow == null)
            {
                return;
            }
            ///////////////////////////////////////////////////
            int intI = 0;
            int intID = int.Parse(dgdSCBranch.CurrentRow.Cells[0].Value.ToString());
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure want to Delete?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    intI = objControl.DeleteSWIFT_BRANCH_ACTION(intID);
                    if (intI < 0)
                        MessageBox.Show("Cannot delete data!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Delete data successfuly!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //LoadData();
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LoadData();
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frmSWIWConditionBr_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                LoadData();
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
                
                DataSet datDs = new DataSet();
                datDs = objControl.GetSWIFT_BRANCH_ACTION();
                dgdSCBranch.DataSource = datDs.Tables[0];
                dgdSCBranch.Columns[0].Visible = false;
                dgdSCBranch.Columns["NAME"].HeaderText = "Name";
                dgdSCBranch.Columns["NAME"].Width = 100;
                dgdSCBranch.Columns["BRANCH"].HeaderText = "Branch";
                dgdSCBranch.Columns["BRANCH"].Width = 100;
                dgdSCBranch.Columns["DEPARTMENT"].HeaderText = "Module";
                dgdSCBranch.Columns["DEPARTMENT"].Width = 80;
                dgdSCBranch.Columns["PRIORITY"].HeaderText = "Priority";
                dgdSCBranch.Columns["PRIORITY"].Width = 50;
                dgdSCBranch.Columns["KEY_WORD"].HeaderText = "Keyword";
                dgdSCBranch.Columns["KEY_WORD"].Width = 100;
                dgdSCBranch.Columns["MESSAGE"].HeaderText = "Criteria Message";
                dgdSCBranch.Columns["MESSAGE"].Width = 100;
                dgdSCBranch.Columns["MESSAGE"].Visible = false;
                dgdSCBranch.Columns["DESCRIPTION"].HeaderText = "Criteria Message";
                dgdSCBranch.Columns["DESCRIPTION"].Width = 512;

                /////////////////////////////////////////////////-/
                //Muc dich: Xet rowcount                                
                //Ngay sua: 01/08/2008
                ///////////////////////////////////////////////////
                if (dgdSCBranch.RowCount > 0)
                { tlbDelete.Enabled = true; tlbEdit.Enabled = true; }
                else
                { tlbDelete.Enabled = false; tlbEdit.Enabled = false; }
                    
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
     

        private void tlbClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlbEdit_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////-/
            //Muc dich: Kiem tra dgdSCBranch.rowcount<=0 
            //          => exit            
            //Ngay sua: 01/08/2008            
            //Chu y:2 su kien Click va DoubleClick can kiem
            //      lai xem phai xet dgdSCBranch.RowCount? 
            if (dgdSCBranch.RowCount <= 0)
            {
                return;
            }
            if (dgdSCBranch.CurrentRow == null)
            {
                return;
            }
            Edit_MSG();
            ///////////////////////////////////////////////////

        }

        private void frmSWIWConditionBr_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        private void Edit_MSG()
        {
            try
            {
                frmSwiftEditBrAuto frmEdit = new frmSwiftEditBrAuto();
                frmEdit.dblPRM_ID = double.Parse(dgdSCBranch.CurrentRow.Cells["PRM_ID"].Value.ToString());
                frmEdit.strName = dgdSCBranch.CurrentRow.Cells["NAME"].Value.ToString();
                frmEdit.strBranch = dgdSCBranch.CurrentRow.Cells["BRANCH"].Value.ToString().TrimStart('0');
                frmEdit.strPriority = dgdSCBranch.CurrentRow.Cells["PRIORITY"].Value.ToString();
                frmEdit.strKeyword = dgdSCBranch.CurrentRow.Cells["KEY_WORD"].Value.ToString();
                frmEdit.strCriteria = dgdSCBranch.CurrentRow.Cells["MESSAGE"].Value.ToString();
                frmEdit.strDesMSG = dgdSCBranch.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                frmEdit.strDepartment = dgdSCBranch.CurrentRow.Cells["DEPARTMENT"].Value.ToString();
                this.Hide();
                frmEdit.ShowDialog(this);
                this.Show();
                /////////////////////////////////////////
                //Muc dich: Load lai du lieu sau khi sua            
                //Ngay sua: 01/08/2008
                LoadData();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgdSCBranch_DoubleClick(object sender, EventArgs e)
        {
            Edit_MSG();
        }

        private void frmSWIWConditionBr_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSWIWConditionBr_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
