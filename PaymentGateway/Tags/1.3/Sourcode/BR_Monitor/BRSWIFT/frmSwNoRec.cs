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
    public partial class frmSwNoRec : frmBasedata 
    {
        private SWIFT_NRECInfo objInfo = new SWIFT_NRECInfo();
        private SWIFT_NRECController objControl = new SWIFT_NRECController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private clsCheckInput checkInput = new clsCheckInput();
        private int iID = 0;
        private int iRows;
        private string pMSG_ID = "";
        private string pMSG_TYPE = "";

        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();        
        

        public frmSwNoRec()
        {
            InitializeComponent();
        }

        private void frmSwNoRec_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            this.Text = "Message don't receive into BR - SWIFT";
            LoadData();
           
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {   
            try
            {                
                if (Common.iSconfirm == 1)
                {
                    objInfo.MSG_ID = iID;
                    if (txtMSGType.Text.Length >= 2)
                    {
                        if (txtMSGType.Text.Substring(0, 2) != "MT")
                        {
                            objInfo.MSG_TYPE = "MT" + txtMSGType.Text;
                        }
                    }
                    if (string.IsNullOrEmpty(txtMSGType.Text.Trim()))
                    {
                        MessageBox.Show("Message type can not empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMSGType.Focus();
                        //////////////////////////////////////////
                        //Muc dich:     Set enable = true
                        //Nguoi sua:    Huypq
                        //Ngay sua:     31/07/2008
                        cmdSave.Enabled = true;
                        //////////////////////////////////////////
                        return;
                    }
                    if (txtMSGType.Text.Trim().Length < 3)
                    {
                        MessageBox.Show("Length of Message type must be 3 characters!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMSGType.Focus();
                        //////////////////////////////////////////
                        //Muc dich:     Set enable = true
                        //Nguoi sua:    Huypq
                        //Ngay sua:     31/07/2008
                        cmdSave.Enabled = true;
                        //////////////////////////////////////////
                        return;
                    }
                    if (GetData.IDIsExisting(false, "SWIFT_NREC", "MSG_TYPE", "MT" + txtMSGType.Text.Trim(), ""))
                    {
                        MessageBox.Show("This message type has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMSGType.Text = "";
                        cmdAdd.Enabled = false;
                        cmdSave.Enabled = true;
                        cmdDelete.Enabled = false;
                        return;
                    }
                    objControl.AddSWIFT_NREC(objInfo);
                    MessageBox.Show("Data has inserted successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadData();
                    cmdAdd.Enabled = true;
                    cmdDelete.Enabled = true;
                    //lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "Message do not receive";
                    int Log_level = 1;
                    string strWorked = "Insert";
                    string strTable = "SWIFT_NREC";
                    string strOld_value = "";
                    string strNew_value = objInfo.MSG_TYPE; 
                    //-----------------------------------------
                    Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                }
                ////////////////////////////////////////////////
                //Muc dich:     
                //Nguoi sua:    Huypq
                //Ngay sua:     31/07/2008                
                ////////////////////////////////////////////////
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
        

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            //txtMSGType.ReadOnly = false;
            txtMSGType.Text = "";
            txtMSGType.Focus();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet datDs = new DataSet();
                if (txtMSGType.Text != "")
                {                    
                    datDs = objControl.GetSWIFT_NRECSearch(txtMSGType.Text.Trim());
                }
                else
                {
                    datDs = objControl.GetSWIFT_NREC();
                }
                dgView.DataSource = datDs.Tables[0];
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
                //Muc dich:     Lay iID khi rowcount>0
                //Nguoi sua:    Huypq
                //Ngay sua:     31/07/2008                
                ////////////////////////////////////////////////
                if (dgView.RowCount > 0)
                {
                    iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                    ////////////////////////////////////////////////
                    //Muc dich:     Neu dong y xoa moi duoc xoa
                    //Nguoi sua:    Huypq
                    //Ngay sua:     31/07/2008                
                    ////////////////////////////////////////////////
                    if (Common.iSconfirm == 1)
                    {
                        objControl.DeleteSWIFT_NREC(iID);
                        //lay thong tin de ghilog----------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "Message do not receive";
                        int Log_level = 1;
                        string strWorked = "Delete";
                        string strTable = "SWIFT_NREC";
                        string strOld_value = pMSG_ID + "/" + "MT"+ pMSG_TYPE;
                        string strNew_value = "";
                        //-----------------------------------------
                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                    }   
                }                                        
                //}
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            LoadData();
            cmdAdd.Enabled = true;
            cmdDelete.Enabled = true;
            cmdSave.Enabled = false;
        }
        private void LoadData()
        {
            DataSet datDs = new DataSet();
            datDs = objControl.GetSWIFT_NREC();
            dgView.DataSource = datDs.Tables[0];
            if (datDs.Tables[0].Rows.Count == 0)
            {
                //Muc dich: Khi ko co ban ghi
                //Nguoi sua: Huypq
                //Ngay sua: 31/07/2008
                cmdDelete.Enabled = false;

                return;
            }
            else
            {
                //Muc dich: Khi co ban ghi
                //Nguoi sua: Huypq
                //Ngay sua: 31/07/2008
                cmdDelete.Enabled = true;

                dgView.Columns[0].Visible = false;
                dgView.Columns[1].HeaderText = "Message type";
                dgView.Columns[1].Width = 210;
            }
        }

        private void Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        {
            objuser_msg_log.LOG_DATE = Logdate;
            objuser_msg_log.USERID = strUsername;
            objuser_msg_log.CONTENT = strContent;
            objuser_msg_log.STATUS = Log_level;
            objuser_msg_log.WORKED = strWorked;
            objuser_msg_log.TABLE_ACCESS = strTale_Access;
            objuser_msg_log.OLD_VALUE = strOld_value;
            objuser_msg_log.NEW_VALUE = strNew_value;

            objcontroluser_msg_log.AddUSER_MSG_LOG1(objuser_msg_log);
        }

        private void txtMSGType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("You must input a number!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }            
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
                pMSG_ID = dgView.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                pMSG_TYPE = dgView.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
                pMSG_ID = dgView.Rows[iRows].Cells["MSG_ID"].Value.ToString();
                pMSG_TYPE = dgView.Rows[iRows].Cells["MSG_TYPE"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
