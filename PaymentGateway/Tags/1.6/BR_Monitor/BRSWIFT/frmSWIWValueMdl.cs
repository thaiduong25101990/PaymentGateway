using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
//using BR.DataAccess;
using BR.BRLib;

namespace BR.BRSWIFT
{
    public partial class frmSWIWValueMdl : Form
    { 
        private SWIFT_AUTO_VALUE_Info objInfo = new SWIFT_AUTO_VALUE_Info();
        private SWIFT_AUTO_VALUEController objControl = new SWIFT_AUTO_VALUEController();
        public string gGetValueCode = "";
        public string gGetValue = "";
        public frmSWIWValueMdl()
        {
            InitializeComponent();
        }

        private void frmSWIWValueMdl_Load(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void LoadData()
        {
            try
            {
               DataSet datDs = new DataSet();
                datDs = objControl.GetSWIFT_AUTO_VALUE();
                grdList.DataSource = datDs.Tables[0];
                grdList.Columns[0].Visible = false;
                grdList.Columns[1].HeaderText = "Name";
                grdList.Columns[1].Width = 100;
                grdList.Columns[2].HeaderText = "Criteria message";
                grdList.Columns[2].Width = 700;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            //strKeyWord = grdList.CurrentRow.Cells[1].Value.ToString();
            int intPRM_ID = int.Parse(grdList.CurrentRow.Cells[0].Value.ToString());
            int intI = 0;
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Do you really want to Delete?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    intI = objControl.DeleteSWIFT_AUTO_VALUE(intPRM_ID);
                    if (intI < 0)
                        MessageBox.Show("Cannot delete data!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Delete data successfuly!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LoadData();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        

        private void tsbNew_Click(object sender, EventArgs e)
        {
            try
            {
                frmSWIWAutoValue SwiftAutoValue = new frmSWIWAutoValue();
                this.Hide();
                SwiftAutoValue.ShowDialog(this);
                this.Show();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSWIWValueMdl_Load_1(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            LoadData();
        }

        private void tsbClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
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

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //KEY_WORD, a.MESSAGE 
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < grdList.Rows.Count)
                {
                    gGetValueCode = grdList.Rows[e.RowIndex].Cells["KEY_WORD"].Value.ToString();
                    gGetValue = grdList.Rows[e.RowIndex].Cells["MESSAGE"].Value.ToString();
                    Close();
                }
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSWIWValueMdl_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (grdList.CurrentRow.Index >= 0 && grdList.CurrentRow.Index < grdList.Rows.Count)
                {

                    gGetValueCode = grdList.Rows[grdList.CurrentRow.Index].Cells["KEY_WORD"].Value.ToString();
                    gGetValue = grdList.Rows[grdList.CurrentRow.Index].Cells["MESSAGE"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            frmSWIWAutoValue SwiftAutoValue = new frmSWIWAutoValue();
            this.Hide();
            SwiftAutoValue.strKeycode = grdList.Rows[grdList.CurrentRow.Index].Cells["KEY_WORD"].Value.ToString();
            SwiftAutoValue.strCriate = grdList.Rows[grdList.CurrentRow.Index].Cells["MESSAGE"].Value.ToString();
            SwiftAutoValue.iID = Convert.ToInt32(grdList.Rows[grdList.CurrentRow.Index].Cells["PRM_ID"].Value.ToString());
            SwiftAutoValue.ShowDialog(this);
            this.Show();
        }

        private void frmSWIWValueMdl_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSWIWValueMdl_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }



       
     }
}