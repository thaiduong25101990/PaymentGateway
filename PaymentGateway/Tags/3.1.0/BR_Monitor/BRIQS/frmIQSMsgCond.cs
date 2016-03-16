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
using BR.BRSYSTEM;

namespace BR.BRIQS
{
    public partial class frmIQSMsgCond : frmBasedata
    {
        IQS_CONDITIONInfo objIQS = new IQS_CONDITIONInfo();
        IQS_CONDITIONController objctrl = new IQS_CONDITIONController();
        BRANCHInfo objBranch = new BRANCHInfo();
        BRANCHController objctrlBr = new BRANCHController();
        public int selectedRow;
        public int iRows;

        private bool NeedConfirm = true;
        private static bool strSucess = false;

        public frmIQSMsgCond()
        {
            InitializeComponent();
        }

        private void frmIQSMsgCond_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                GetData();
                cbDirection.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham load du lieu toan bo cua IQS len datagrid
        private void GetData()
        {
            try
            {
                DataTable datIQS = new DataTable();
                datIQS = objctrl.GetIQS();
                if (datIQS.Rows.Count != 0)
                {
                    datIQS_CONDITION.DataSource = datIQS;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Search()
        {
            try
            {
                datIQS_CONDITION.DataSource = 0;
                string strType = ""; string strGwtype = "";
                if (txtMsg_type.Text.Trim() == "") { strType = ""; } else { strType = " and  Trim(IC.MSG_TYPE) =  '" + txtMsg_type.Text.Trim() + "'"; }
                if (cbDirection.Text == "ALL") { strGwtype = ""; } else { strType = " and  Trim(IC.GWTYPE) =  '" + cbDirection.Text.Trim() + "'"; }
                string strWhere = strType + strGwtype;
                string strWhere1 = "";
                if (strWhere == "")
                {
                }
                else
                {
                    strWhere1 = "Where  " + strWhere.Substring(5);
                }
                DataTable datSearch = new DataTable();
                datSearch = objctrl.Search(strWhere1);
                if (datSearch == null || datSearch.Rows.Count == 0)
                {
                    datIQS_CONDITION.DataSource = 0;
                }
                else
                {
                    datIQS_CONDITION.DataSource = datSearch;
                    datIQS_CONDITION.Columns[0].Visible = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void cmdAdd_Click(object sender, EventArgs e)
        {            
            cmdAdd.Enabled = false;
            cmdSave.Enabled = true;
        }

        private void datIQS_CONDITION_Click(object sender, EventArgs e)
        {
           
        }
        //su kien click vao cell
        private void datIQS_CONDITION_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 1)
                {
                    objctrl.DELETE(datIQS_CONDITION.Rows[iRows].Cells[0].Value.ToString().Trim());
                    Common.ShowError("Delete data Successfully!", 2, MessageBoxButtons.OK);                    
                    Search();
                    if (datIQS_CONDITION.Rows.Count == 0)
                    {
                        cmdDelete.Enabled = false;                        
                    }
                    else
                    {
                        cmdDelete.Enabled = true;
                    }
                    cmdAdd.Enabled = true;
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdDelete.Enabled = true;
                    cmdAdd.Enabled = true;
                    cmdSave.Enabled = false;
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

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (Common.iSconfirm == 1)
            {
                if (txtMsg_type.Text.Trim() == "")
                {
                    Common.ShowError("Message type is Empty!", 1, MessageBoxButtons.OK);                     
                    txtMsg_type.Focus();
                    cmdSave.Enabled = true;
                    cmdAdd.Enabled = false;
                    cmdDelete.Enabled = false;
                    return;
                }
                try
                {
                    if (txtMsg_type.Text == "")
                    {
                        MessageBox.Show("MSG_TYPE is Empty");
                    }
                    else
                    {
                        //kiem tra co du lieu trung khong
                        DataTable datSS = new DataTable();
                        datSS = objctrl.GetKT(txtMsg_type.Text.Trim(), cbDirection.Text);
                        if (datSS.Rows.Count == 0)//cho ADD
                        {
                            if (cbDirection.Text.Trim() != "ALL")
                            {
                                objIQS.MSG_TYPE = txtMsg_type.Text.Trim();
                                objIQS.GWTYPE = cbDirection.Text;
                                if (objctrl.ADDIQS(objIQS) == 1)
                                {
                                    Common.ShowError("Insert data Successfull!", 1, MessageBoxButtons.OK);                                     
                                    txtMsg_type.Text = "";
                                    Search();
                                    cmdDelete.Enabled = true;
                                    cmdAdd.Enabled = true;
                                }
                                else
                                {
                                    cmdSave.Enabled = true;
                                    cmdDelete.Enabled = false;
                                    cmdAdd.Enabled = false;
                                }
                            }
                            else
                            {
                                Common.ShowError("Data input not like All !", 3, MessageBoxButtons.OK);                                 
                                cmdSave.Enabled = true;
                                cmdDelete.Enabled = false;
                                cmdAdd.Enabled = false;
                            }
                        }
                        else
                        {
                            Common.ShowError("Data has already exits!", 3, MessageBoxButtons.OK);                            
                            cmdSave.Enabled = true;
                            cmdDelete.Enabled = false;
                            cmdAdd.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                }
            }
            else
            {
                cmdDelete.Enabled = true;
                cmdAdd.Enabled = true;
                cmdSave.Enabled = false;
            }
         
        }
       
       
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {                
                this.Close();
            }           
            if (e.KeyChar == (char)13)
            {
                if (txtMsg_type.Focused)
                {
                    cbDirection.Focus();
                    cbDirection.SelectAll();
                }
                else if (cbDirection.Focused)
                {
                    cmdSearch.Focus();
                    button1_Click(null, null);
                }

                strSucess = true;
            }
        }

        private void frmIQSMsgCond_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmIQSMsgCond_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmIQSMsgCond_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
