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
    public partial class frmSWIWConditionMdl : frmBasedata
    {
        #region ham va bien
        public static bool isInsert = false;
        private DataSet datDs = new DataSet();
        private SWIFT_MODULE_ACTION_Info objInfo = new SWIFT_MODULE_ACTION_Info();
        private SWIFT_MODULE_ACTIONController objControl = new SWIFT_MODULE_ACTIONController();      
        private clsCheckInput checkInput = new clsCheckInput();
        private int iFieldID = 0;
        #endregion

        public frmSWIWConditionMdl()
        {
            InitializeComponent();
        }


        private void frmSWIWConditionMdl_Load(object sender, EventArgs e)
        {
            
        }

        private void tlbNew_Click(object sender, EventArgs e)
        {
            frmSWIWModulAuto GWSwiftModulAuto = new frmSWIWModulAuto();
            this.Hide();
            GWSwiftModulAuto.ShowDialog(this);
            this.Show();
            /////////////////////////////////////////
            //Muc dich: Load lai du lieu sau khi sua            
            //Ngay sua: 01/08/2008
            LoadData();
            /////////////////////////////////////////
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////////
            //Muc dich: Kiem tra dgdSCBranch.rowcount<=0 
            //          => exit            
            //Ngay sua: 01/08/2008            
            if (dgdSCModule.RowCount <= 0)
            {
                return;
            }
            if (dgdSCModule.CurrentRow == null)
            {
                return;
            }
            ///////////////////////////////////////////////////
            int intI = 0;
            //strKeyWord = dgdSCModule.CurrentRow.Cells[1].Value.ToString();
            iFieldID = Convert.ToInt32(dgdSCModule.CurrentRow.Cells[0].Value.ToString());
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure want to Delete?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {                    
                    intI = objControl.DeleteSWIFT_MODULE_ACTION(iFieldID);
                    if (intI < 0)
                        MessageBox.Show("Cannot delete data!",Common.sCaption);
                    else
                        MessageBox.Show("Delete data successfuly!",Common.sCaption);
                    //LoadData();
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LoadData();
        }



        private void LoadData()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = objControl.GetSWIFT_MODULE_ACTION();
                dgdSCModule.DataSource = datDs.Tables[0];
                dgdSCModule.Columns[0].Visible = false;
                dgdSCModule.Columns[1].HeaderText = "Name";
                dgdSCModule.Columns[1].Width = 100;
                dgdSCModule.Columns[2].HeaderText = "Module";
                dgdSCModule.Columns[2].Width = 70;
                dgdSCModule.Columns[3].HeaderText = "Priority";
                dgdSCModule.Columns[3].Width = 50;
                dgdSCModule.Columns[4].HeaderText = "Keyword";
                dgdSCModule.Columns[4].Width = 150;
                dgdSCModule.Columns[5].HeaderText = "Criteria Message";
                dgdSCModule.Columns[5].Visible=false;
                dgdSCModule.Columns[6].HeaderText = "Criteria Message";
                dgdSCModule.Columns[6].Width = 500;
                //////////////////////////////////////////
                //Muc dich: Kiem rowcount>0                
                //Ngay sua: 01/08/2008
                if (dgdSCModule.RowCount > 0)
                {                    
                    tlbDelete.Enabled = true;
                    tlbEdit.Enabled = true;
                }
                else
                {
                    tlbDelete.Enabled = false;
                    tlbEdit.Enabled = false;
                }
                //////////////////////////////////////////
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

   

        private void frmSWIWConditionMdl_Load_1(object sender, EventArgs e)
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
            if (dgdSCModule.RowCount <= 0)
            {
                return;
            }
            if (dgdSCModule.CurrentRow == null)
            {
                return;
            }
            ///////////////////////////////////////////////////
            EditModulAction();
            /////////////////////////////////////////
        }

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCDInfo_FormClosing(null,null);
                this.Close();
            }
           
        }

      
        private void EditModulAction()
        {
            try
            {
                frmSwiftEditMdlAuto frmEdit = new frmSwiftEditMdlAuto();
                frmEdit.dblPRM_ID = double.Parse(dgdSCModule.CurrentRow.Cells["PRM_ID"].Value.ToString());
                frmEdit.strName = dgdSCModule.CurrentRow.Cells["NAME"].Value.ToString();
                frmEdit.strModule = dgdSCModule.CurrentRow.Cells["DEPARTMENT"].Value.ToString();
                frmEdit.strPriority = dgdSCModule.CurrentRow.Cells["PRIORITY"].Value.ToString();
                frmEdit.strKeyword = dgdSCModule.CurrentRow.Cells["KEY_WORD"].Value.ToString();
                frmEdit.strCriteria = dgdSCModule.CurrentRow.Cells["MESSAGE"].Value.ToString();
                frmEdit.strDESC = dgdSCModule.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                this.Hide();
                frmEdit.ShowDialog(this);
                this.Show();
                ///////////////////////////////////////-/
                //Muc dich: Load lai du lieu sau khi sua            
                //Ngay sua: 01/08/2008
                LoadData();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgdSCModule_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                iFieldID = Convert.ToInt32(dgdSCModule.CurrentRow.Cells[0].Value.ToString());
                EditModulAction();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSWIWConditionMdl_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSWIWConditionMdl_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
