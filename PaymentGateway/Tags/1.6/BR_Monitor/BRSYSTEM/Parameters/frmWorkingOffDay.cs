using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject.ObjBesiness.BRSYSTEM;
using BR.BRBusinessObject.ObjectInfo.BRSYSTEM;
using BR.BRBusinessObject.ObjectProcess.BRSYSTEM;
using BR.BRLib;


namespace BR.BRSYSTEM.Parameters
{

    
    public partial class frmWorkingOffDay : frmBasedata
    {
        WorkingOffDayServices excObj = new WorkingOffDay_Dao();
        WorkingOffDay_info wofObj = new WorkingOffDay_info();
        private int iID = 0;
        //private bool isInsert = false;
        private string strValue = "";
        private string strDescription = "";
        private static int iRows;
        private int iLoad = 0;
        public static int isSave = 0;
       


        public frmWorkingOffDay()
        {
            InitializeComponent();
        }

        private void frmWorkingOffDay_Load(object sender, EventArgs e)
        {
            loadData();
            txtDescription.ReadOnly = true;
            dWorkingOffDay.Enabled = false;
         

        }
        private void loadData() {
            int iRows = 0;
            DataSet ds = new DataSet();
            txtDescription.ReadOnly = true;
            dWorkingOffDay.Enabled = false;
            try
            {
                ds = excObj.GetMsList();
                dgrListView.DataSource = ds.Tables[0];
                dgrListView.Focus();

                if (ds.Tables[0].Rows.Count != 0)
                {
                    dgrListView.Columns["ID"].Visible = false;
                    dgrListView.Columns["OFFDAY"].HeaderText = " Off Day";
                    dgrListView.Columns["OFFDAY"].Width = 80;
                    dgrListView.Columns["DESCRIPTION"].HeaderText = "Description";
                    dgrListView.Columns["DESCRIPTION"].Width = 300;
                    dgrListView.Rows[iRows].Selected = true;
                    dgrListView.Rows[0].Selected = true;
                }
                iLoad = 0;
            }
            catch (Exception ex)
            { 
                
            }
         
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWorkingOffDay_KeyDown(object sender, KeyEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    if ((this.ActiveControl as Button).Name == "cmdSave")
                    {
                        cmdSave.Focus();
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

      
        private void dgrListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) { iRows = 0; }
                else
                { if (iLoad == 0) { iRows = e.RowIndex; } }

                iID = Convert.ToInt32(dgrListView.CurrentRow.Cells["ID"].Value.ToString());
                txtDescription.Text = dgrListView.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                dWorkingOffDay.Value = (DateTime)dgrListView.CurrentRow.Cells["OFFDAY"].Value;
            }
            catch(Exception ex)
            {}
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isSave = 1;
            dWorkingOffDay.Enabled = true;
            txtDescription.ReadOnly = false;
            txtDescription.Text = "";
            CommandStatus(false);

        }
        private void CommandStatus(bool a)
        {
            //cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSave.Enabled = !a;

            dgrListView.Enabled = a;
        }
        private void cmdEdit_Click(object sender, EventArgs e)
        {
           
            isSave = 2;
            try
            {
                if (dgrListView.SelectedRows.Count==0)
                {
                    MessageBox.Show("None row selected", "Bridge");
                }
                txtDescription.ReadOnly = false;
                dWorkingOffDay.Enabled = true;
                txtDescription.Focus();
                CommandStatus(false);
               
            }
            catch (Exception ex)
            {
               
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)

        {
            if (String.IsNullOrEmpty(dWorkingOffDay.Text.Trim()))
            {
                Common.ShowError("You must input value!", 3, MessageBoxButtons.OK);
                dWorkingOffDay.Focus();
                cmdEdit.Enabled = false;
                cmdSave.Enabled = true;
                return;
            }
            if (isSave == 1) // insert
            {
                wofObj._dtOffDay = dWorkingOffDay.Value;
                wofObj._sDescription = txtDescription.Text;
                excObj.CreateMsg(wofObj);               
                CommandStatus(true);
                dWorkingOffDay.Enabled = false;
                txtDescription.ReadOnly = true;
            }
            else if (isSave == 2) // update
            {
                wofObj._dtOffDay = dWorkingOffDay.Value;
                wofObj._sDescription = txtDescription.Text;
                wofObj.id = int.Parse(dgrListView.SelectedRows[0].Cells[0].Value.ToString());
                excObj.EditMsg(wofObj);
                CommandStatus(true);
                dWorkingOffDay.Enabled = false;
                txtDescription.ReadOnly = true;
                dgrListView.Select();              
                CommandStatus(true);
            }
            loadData();
            isSave = 0;
            cmdAdd.Enabled = true;
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int iFieldID = int.Parse(dgrListView.SelectedRows[0].Cells[0].Value.ToString());
            excObj.DeleteMsg(iFieldID);
            Common.ShowError("Data has delete successfully!", 7, MessageBoxButtons.OK);
            loadData();
            isSave = 0;
        }

       
    }
}
