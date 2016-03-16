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
    public partial class frmGWTypeInfo : Form
    {
        /*---------------------------------------------------------------
      * Muc dich         : Khai bao cap nhat thong tin cho kenh thanh toan
      * Ngay tao         : 09/06/2008
      * Nguoi tao        : HaNTT10
      *--------------------------------------------------------------*/
        private clsLog objLog = new clsLog();
        private DataSet datDs = new DataSet();
        clsCheckInput checkInput = new clsCheckInput();
        public GWTYPEInfo objInfo = new GWTYPEInfo();
        private GWTYPEController objControl = new GWTYPEController();
        public GWTYPE_DETAILInfo objInfoGWDetail = new GWTYPE_DETAILInfo();
        private GWTYPE_DETAILController objControlGWDetail = new GWTYPE_DETAILController();
        private ALLCODEController objAllcode = new ALLCODEController();
        clsCheckInput clsCheck = new clsCheckInput();
        private static int iRows;
        private static string strDELETE_DETAIL = "";        
        public string strGWTYPEID;
        public string strGWTYPE;
        public string strGWTYPESTS;
        public string strMSG_NO;
        public string strDESCRIPTION;
        public string strCONNECTION;
        public string strDBLINK;
        //private int idtlID;

        DataSet ds = new DataSet();


        private int iInfoEdit=0;
        
        private int Duplicate;
        private string strtxtDBname;
        private string ADD_DETAIL; 
        
        private string DHCONNECT = "";
        private string DHGWTYPESTS = "";


        public frmGWTypeInfo()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        { 
            int iGw_ID;
            try
            {
                DialogResult isResult = MessageBox.Show("Do you want to save data?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (isResult == DialogResult.No)
                {
                    Enable_Text(false);
                    ClearText();
                    ViewGWTYPE_DTL();
                    cmdSave.Enabled = false;                    
                    return;
                }
                if (CheckUpdate() == false)
                    return;

                //---------------goi cac ham insert hoac delete du lieu trong database--------------------------

                if (iInfoEdit == 1)
                {
                    if (frmGWTYPE.isInsert)
                    {
                        INSERT_GWTYPE();
                    }
                    else
                    {
                        iGw_ID = Convert.ToInt32(strGWTYPEID);
                        Update_GWTYPE(iGw_ID);
                    }
                    iInfoEdit = 0;
                }

                // Cap nhat phan detail
                if (ADD_DETAIL == "ADD")
                {
                    if (CheckUpdateDetail() == false)
                        return;
                    Update_GWTYPE_DTL();
                    ADD_GWTYPE_DTL();
                }
                else if (ADD_DETAIL == "EDIT")
                {
                    if (CheckUpdateDetail() == false)
                        return;

                    Update_GWTYPE_DTL();
                    EDIT_GWTYPE_DTL();
                }
                ADD_DETAIL = "";
                if (strDELETE_DETAIL.Trim() != "")//Sava du lieu khi delete
                {
                    objControlGWDetail.DeleteGWTYPE_DETAIL1(strDELETE_DETAIL);   //ghi du lieu vao trong database                
                }

                if (tbGwtypeINFO.SelectedTab == tpdetail)
                {
                    cmdAddDetail.Enabled = true;
                    cmdEditDetail.Enabled = true;
                    if (dataGridView1.Rows.Count > 1)
                    {
                        dataGridView1.Focus();
                        dataGridView1.Rows[0].Selected = true;
                        cmdRemove.Enabled = true;
                    }
                }
                else
                {
                    cmdAddDetail.Enabled = false;
                    cmdEditDetail.Enabled = false;
                    cmdRemove.Enabled = false;
                }

                cmdSave.Enabled = false;
                Enable_Text(false);
                
                //--------------------------------------------------------------------------------------------
              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    
        private bool CheckUpdate()
        {
            try
            {
                if (String.IsNullOrEmpty(cboGWType.Text.Trim()))
                {
                    Common.ShowError("You must channel input data!", 3, MessageBoxButtons.OK);
                    
                    cboGWType.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }

                if (String.IsNullOrEmpty(txtNumberOfMes.Text.Trim()))
                {
                    Common.ShowError("You must Number of message in bulk file input data!", 3, MessageBoxButtons.OK);
                    
                    txtNumberOfMes.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }
                if (String.IsNullOrEmpty(cboStatus.Text.Trim()))
                {
                    Common.ShowError("You must status input data!", 3, MessageBoxButtons.OK);
                    cboStatus.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }
                if (String.IsNullOrEmpty(cboConection.Text.Trim()))
                {
                    Common.ShowError("You must conection input data!", 3, MessageBoxButtons.OK);
                    cboConection.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }
                if (Convert.ToInt16(txtNumberOfMes.Text) <= 0)
                {
                    Common.ShowError("Invalid number of messages!", 3, MessageBoxButtons.OK);
                    cmdSave.Enabled = true;
                    txtNumberOfMes.Text = "1";
                    txtNumberOfMes.Focus();
                    return false;
                }
                if (cboGWType.Text == Common.GW_CHANNEL_IBPS || cboGWType.Text == Common.GW_CHANNEL_VCB)//neu chon kenh thanh toan la IBPS
                {
                    //chi cho phep tao 1 dien /1 file
                    if (Convert.ToInt16(txtNumberOfMes.Text) != 1)//gia tri duoc chon khong duoc khac 1
                    {
                        Common.ShowError("Only one message per one file!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        txtNumberOfMes.Text = "1";
                        txtNumberOfMes.Focus();
                        return false;
                    }
                }
                else if (cboGWType.Text == Common.GW_CHANNEL_SWIFT)//neu chon la swift,vcb thi khac
                {
                    //cho phep tao nhieu dien / mot file
                    if (Convert.ToInt16(txtNumberOfMes.Text) < 1)
                    {
                        Common.ShowError("Number of message is greater than 1!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        txtNumberOfMes.Text = "";
                        txtNumberOfMes.Focus();
                        return false;
                    }
                }

                //kiem tra xem cboConection co bang DBLINK khong
                if (cboConection.Text.Trim() == "DBLINK")//neu bang DBLINK kiem tra rong
                {
                    if (txtDBname.Text.Trim() == "")
                    {
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        txtDBname.Focus();
                        return false;
                    }
                }

                DHCONNECT = "";
                DHGWTYPESTS = "";
                strCONNECTION = cboConection.Text.Trim();
                strGWTYPESTS = cboStatus.Text.Trim();

                //QUYND Update 20081119-------------------------------------------------------------------------
                if (strCONNECTION.Trim() != "")
                {
                    DataTable datAll = new DataTable();
                    datAll = objAllcode.GetALLCODE_code1(strCONNECTION, "Connection");
                  
                    DHCONNECT = datAll.Rows[0]["CDVAL"].ToString();//CONECTION
                }
                //--------------------------------->
                if (strGWTYPESTS.Trim() != "")
                {
                    DataTable datAll1 = new DataTable();
                    datAll1 = objAllcode.GetALLCODE_code1(strGWTYPESTS, "GWTYPESTS");
                    DHGWTYPESTS = datAll1.Rows[0]["CDVAL"].ToString();//GWTYPESTS
                }
                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        //neu nut Detail chua duoc nhan chon thi goi ham nay

        private bool CheckUpdateDetail()
        {
            try
            {
                if (String.IsNullOrEmpty(cboGWType.Text.Trim()))
                {
                    Common.ShowError("You must channel input data!", 3, MessageBoxButtons.OK);

                    cboGWType.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }

                if (String.IsNullOrEmpty(txtNumberOfMes.Text.Trim()))
                {
                    Common.ShowError("You must Number of message in bulk file input data!", 3, MessageBoxButtons.OK);

                    txtNumberOfMes.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }
                if (String.IsNullOrEmpty(cboStatus.Text.Trim()))
                {
                    Common.ShowError("You must status input data!", 3, MessageBoxButtons.OK);
                    cboStatus.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }
                if (String.IsNullOrEmpty(cboConection.Text.Trim()))
                {
                    Common.ShowError("You must conection input data!", 3, MessageBoxButtons.OK);
                    cboConection.Focus();
                    cmdSave.Enabled = true;
                    return false;
                }
                if (Convert.ToInt16(txtNumberOfMes.Text) <= 0)
                {
                    Common.ShowError("Invalid number of messages!", 3, MessageBoxButtons.OK);
                    cmdSave.Enabled = true;
                    txtNumberOfMes.Text = "1";
                    txtNumberOfMes.Focus();
                    return false;
                }
                if (cboGWType.Text == Common.GW_CHANNEL_IBPS || cboGWType.Text == Common.GW_CHANNEL_VCB)//neu chon kenh thanh toan la IBPS
                {
                    //chi cho phep tao 1 dien /1 file
                    if (Convert.ToInt16(txtNumberOfMes.Text) != 1)//gia tri duoc chon khong duoc khac 1
                    {
                        Common.ShowError("Only one message per one file!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        txtNumberOfMes.Text = "1";
                        txtNumberOfMes.Focus();
                        return false;
                    }
                }
                else if (cboGWType.Text == Common.GW_CHANNEL_SWIFT)//neu chon la swift,vcb thi khac
                {
                    //cho phep tao nhieu dien / mot file
                    if (Convert.ToInt16(txtNumberOfMes.Text) < 1)
                    {
                        Common.ShowError("Number of message is greater than 1!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        txtNumberOfMes.Text = "";
                        txtNumberOfMes.Focus();
                        return false;
                    }
                }

                //kiem tra xem cboConection co bang DBLINK khong
                if (cboConection.Text.Trim() == "DBLINK")//neu bang DBLINK kiem tra rong
                {
                    if (txtDBname.Text.Trim() == "")
                    {
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);
                        cmdSave.Enabled = true;
                        txtDBname.Focus();
                        return false;
                    }
                }

                DHCONNECT = "";
                DHGWTYPESTS = "";
                strCONNECTION = cboConection.Text.Trim();
                strGWTYPESTS = cboStatus.Text.Trim();

                //QUYND Update 20081119-------------------------------------------------------------------------
                if (strCONNECTION.Trim() != "")
                {
                    DataTable datAll = new DataTable();
                    datAll = objAllcode.GetALLCODE_code1(strCONNECTION, "Connection");

                    DHCONNECT = datAll.Rows[0]["CDVAL"].ToString();//CONECTION
                }
                //--------------------------------->
                if (strGWTYPESTS.Trim() != "")
                {
                    DataTable datAll1 = new DataTable();
                    datAll1 = objAllcode.GetALLCODE_code1(strGWTYPESTS, "GWTYPESTS");
                    DHGWTYPESTS = datAll1.Rows[0]["CDVAL"].ToString();//GWTYPESTS
                }
                if (txtFTPServer.Text.Trim() == "" &&
                 txtFTPPath.Text.Trim() == "" &&
                 txtPassword.Text.Trim() == "" &&
                 txtUser.Text.Trim() == "" &&
                 cboDirection.Text.Trim() == "" &&
                 cboFileType.Text.Trim() == "" &&
                 txtFolder.Text.Trim() == "" &&
                 txtDescriptionDetail.Text.Trim() == "" &&
                 cboFileType.SelectedIndex == 0 &&
                 cboDirection.SelectedIndex == 0 &&
                 cbFolderType.SelectedIndex == 0)
                    return false;

                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Load_data()
        {
            try
            {
                cboGWType.Text = strGWTYPE;
                cboStatus.Text = strGWTYPESTS;
                txtNumberOfMes.Text = strMSG_NO;
                txtDescription.Text = strDESCRIPTION;
                cboConection.Text = strCONNECTION;
                txtDBname.Text = strDBLINK;
                cboConection.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void frmGWTypeInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                cmdRemove.Enabled = false;
                //----------------------------------------------------
                //---Lay du lieu len combo kenh thanh toan

                GetData objcombo = new GetData();

                objcombo.FillDataComboBox(cboGWType, "CONTENT", "CDVAL", "ALLCODE", "CDNAME='GWTYPE'", "", true, false, " ");

                //---lay du lieu len combobox Filetype----------------------------
                objcombo.FillDataComboBox(cboFileType, "CONTENT", "CDVAL", "ALLCODE", "upper(CDNAME)='FILETYPE'", "", true, true, " ");
                //-----------------------lay du lieu len combobox Derection----------------------------
                objcombo.FillDataComboBox(cboDirection, "CONTENT", "CDVAL", "ALLCODE", "upper(CDNAME)=upper('FileDirection')", "", true, true, " ");
                //-----------------combobox status----------------------------------
                objcombo.FillDataComboBox(cboStatus, "CONTENT", "CDVAL", "ALLCODE", "upper(CDNAME)=upper('GWTYPESTS')", "", true, true, " ");

                //-----------------combo connection-----------------------------
                objcombo.FillDataComboBox(cboConection, "CONTENT", "CDVAL", "ALLCODE", "upper(CDNAME)=upper('Connection')", "", true, true, " ");
                //----------------------------------------------------------------------
                //----------------------cbFolderType-------------------------------------
                //lay len ten foleder type
                objcombo.FillDataComboBox(cbFolderType, "CONTENT", "CDVAL", "ALLCODE", "upper(CDNAME)=upper('FLDTYPE')", "", true, true, " ");
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                if (!frmGWTYPE.isInsert)
                {
                    Load_data();
                }
               // cmdCancel.Enabled = true;
                if (!frmGWTYPE.isInsert)
                {
                    cboGWType.Enabled = false;
                    cboGWType.Text = strGWTYPE.Trim();
                    LoadDataDetail();
                }
                else if (frmGWTYPE.isInsert)
                {
                    cboGWType.Enabled = true;
                }
                strtxtDBname = txtDBname.Text.Trim();
                cmdAddDetail.Enabled = false;
                cmdEditDetail.Enabled = false;
                cmdSave.Enabled = true;                              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void LoadDataDetail()
        {
            try
            {                
                dataGridView1.DataSource = null;
                ds = objControlGWDetail.GetGWTYPE_DETAIL(cboGWType.Text.Trim());
                                
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["DirectionT"].Visible = false;
                dataGridView1.Columns["FldtypeT"].Visible = false;
                dataGridView1.Columns["FiletypeT"].Visible = false;
                dataGridView1.Columns["FTPPASS"].Visible = false;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.Focus();
                    dataGridView1.Rows[0].Selected = true;

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LockTextBox(Boolean a)
        { 
            txtDescription.ReadOnly = a;
            txtNumberOfMes.ReadOnly = a;
            cboConection.Enabled = !a;
            cboGWType.Enabled = !a;
            cboStatus.Enabled = !a;

        }

        private void LockTextBoxDetail(Boolean a)
        {
            txtPassword.ReadOnly = a;
            txtUser.ReadOnly = a;
            txtFTPServer.ReadOnly = a;
            cboDirection.Enabled = !a;
            cboFileType.Enabled = !a;
            txtDescriptionDetail.ReadOnly = a;
            txtFolder.ReadOnly = a;
        }

        private void Check_Input()
        {
            try
            {
                //int i = 0;
                //while (i < dataGridView1.Rows.Count)
                //{
                //    if (    dataGridView1.Rows[i].Cells["FTPSERVER"].Value.ToString().Trim() == txtFTPServer.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["FTPUser"].Value.ToString().Trim() == txtUser.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["FTPPASS"].Value.ToString().Trim() == txtPassword.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["Filetype"].Value.ToString().Trim() == cboFileType.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["Direction"].Value.ToString().Trim() == cboDirection.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["fldtype"].Value.ToString().Trim() == cbFolderType.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["Folder"].Value.ToString().Trim() == txtFolder.Text.Trim()
                //         && dataGridView1.Rows[i].Cells["FTPPATH"].Value.ToString().Trim() == txtFTPPath.Text.Trim()
                //        )
                //    {
                //        Duplicate = 1;
                //    } 
                //    i = i + 1;
                //}
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                ClearText();
                txtFTPServer.Focus();
                ADD_DETAIL = "ADD";
                //--------------------------------
                Enable_Text(true); 
                cmdAddDetail.Enabled = false;
                cmdSave.Enabled = true;
                

            }
            catch (Exception ex)
            {
                
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }

        }

        private void ClearText()
        {
            txtFTPServer.Text = "";
            txtFTPPath.Text = "";
            txtPassword.Text = "";
            txtUser.Text = "";
            cboDirection.Text = "";
            cboFileType.Text = "";
            txtFolder.Text = "";
            txtDescriptionDetail.Text = "";
            cboFileType.SelectedIndex = 0;
            cboDirection.SelectedIndex = 0;
            cbFolderType.SelectedIndex = 0;
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    Common.ShowError("There is no data to delete!", 3, MessageBoxButtons.OK);
                }
                else
                {                    
                    if (dataGridView1.Rows[iRows].Cells["ID"].Value.ToString() == "")
                    {

                    }
                    else
                    {
                        if (strDELETE_DETAIL.Trim() == "")
                        {
                            strDELETE_DETAIL =  dataGridView1.Rows[iRows].Cells["ID"].Value.ToString();
                        }
                        else
                        {
                            strDELETE_DETAIL = strDELETE_DETAIL + "," + dataGridView1.Rows[iRows].Cells["ID"].Value.ToString();
                        }                       
                    }
                    dataGridView1.Rows.RemoveAt(iRows);//(iRows);
                }
                if (dataGridView1.Rows.Count == 0)
                {
                    cmdEditDetail.Enabled = false;
                    cmdRemove.Enabled = false;
                }
                else
                {
                    cmdEditDetail.Enabled = true;
                    cmdRemove.Enabled = true;
                }
                cmdSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        private void txtFrequencyTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK); 
            }
        }
        
        private void txtNumberOfMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK); 
            }
        }
        
        private void Check_Input_Edit()
        {
            try
            {
                int i = 0;
                while (i < dataGridView1.Rows.Count)
                {
                    if (i != iRows)
                    {
                        if (    dataGridView1.Rows[i].Cells["FTPSERVER"].Value.ToString().Trim() == txtFTPServer.Text.Trim()
                             && dataGridView1.Rows[i].Cells["FTPUser"].Value.ToString().Trim()      == txtUser.Text.Trim()
                             && dataGridView1.Rows[i].Cells["FTPPASS"].Value.ToString().Trim() == txtPassword.Text.Trim()
                             && dataGridView1.Rows[i].Cells["Filetype"].Value.ToString().Trim()  == cboFileType.Text.Trim()
                             && dataGridView1.Rows[i].Cells["Direction"].Value.ToString().Trim() == cboDirection.Text.Trim()
                             && dataGridView1.Rows[i].Cells["fldtype"].Value.ToString().Trim() == cbFolderType.Text.Trim()
                             && dataGridView1.Rows[i].Cells["Folder"].Value.ToString().Trim()    == txtFolder.Text.Trim()
                             && dataGridView1.Rows[i].Cells["FTPPATH"].Value.ToString().Trim()   == txtFTPPath.Text.Trim()
                            )
                        {
                            Duplicate = 1;
                        }
                    }

                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }
        
        private  void Enable_Text(bool bStatus )
        {
            //------------------------------------------------
            txtFTPServer.Enabled = bStatus;
            txtFTPPath.Enabled = bStatus;
            txtPassword.Enabled = bStatus;
            txtUser.Enabled = bStatus;
            cboDirection.Enabled = bStatus;
            cboFileType.Enabled = bStatus;
            txtFolder.Enabled = bStatus;
            txtDescriptionDetail.Enabled = bStatus;
            cboFileType.Enabled = bStatus;
            cboDirection.Enabled = bStatus;
            cbFolderType.Enabled = bStatus;
            //cmdSaveDTL.Enabled = bStatus;
            //-------------------------------------------------------
        }

        private void cmdEditDetail_Click(object sender, EventArgs e)
        {
            try
            {
                Enable_Text(true);
                //ViewGWTYPE_DTL();
                ADD_DETAIL = "EDIT";
                cmdEditDetail.Enabled = false;
                cmdSave.Enabled = true;
                txtFTPServer.Focus();
                

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                
            }
        }
        
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Common.ClearControl(this);
        }
        
        private void KeyDownPress(object sender, KeyEventArgs e)
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

                    if ((this.ActiveControl as Button).Name == "cmdSave")
                    {
                        cmdSave.Focus();
                        //cmdSave_Click(null, null);
                    }

                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

         private void cboConection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboConection.Text.Trim() == "DBLINK")
                {
                    txtDBname.Enabled = true;
                    txtDBname.Text = strtxtDBname;
                }
                else
                {
                    txtDBname.Enabled = false;
                    txtDBname.Text = "";
                }
               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }
        //QUYND
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                iRows = e.RowIndex;
                ViewGWTYPE_DTL();
                Enable_Text(false);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //QUYND
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;


                ADD_DETAIL = "";
                iRows = e.RowIndex;
                ViewGWTYPE_DTL();
                Enable_Text(false);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
        private void txtDBname_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDBname.Text = clsCheck.ConvertVietnamese(txtDBname.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void txtEncryptFunction_Leave(object sender, EventArgs e)
        {
          
        }

        private void txtDecryptFunction_Leave(object sender, EventArgs e)
        {            
          
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {            
            try
            {
                txtDescription.Text = clsCheck.ConvertVietnamese(txtDescription.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void txtFTPDirection_Leave(object sender, EventArgs e)
        {           
            try
            {
                txtFTPServer.Text = clsCheck.ConvertVietnamese(txtFTPServer.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {           
            try
            {
                txtUser.Text = clsCheck.ConvertVietnamese(txtUser.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void txtFolder_Leave(object sender, EventArgs e)
        {
            //txtFolder
            try
            {
                txtFolder.Text = clsCheck.ConvertVietnamese(txtFolder.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void txtDescriptionDetail_Leave(object sender, EventArgs e)
        {
            //txtDescriptionDetail
            try
            {
                txtDescriptionDetail.Text = clsCheck.ConvertVietnamese(txtDescriptionDetail.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
        }

        private void frmGWTypeInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void INSERT_GWTYPE()
        {

            try
            {
                int icheck;
                GWTYPEInfo objGWtype = new GWTYPEInfo();
                objControl = new GWTYPEController();
                objGWtype.CONNECTION = Convert.ToInt32(DHCONNECT);
                objGWtype.DBLINK = txtDBname.Text.Trim();
                objGWtype.DESCRIPTION = txtDescription.Text.Trim();
                objGWtype.GWTYPE = cboGWType.Text.Trim();
                objGWtype.GWTYPESTS = Convert.ToInt32(DHGWTYPESTS);
                objGWtype.MSG_NO = Convert.ToInt32(txtNumberOfMes.Text.Trim());

                icheck = objControl.AddGWTYPE(objGWtype);

                if (icheck == -1)
                {
                    Common.ShowError("Insert fail.", 2, MessageBoxButtons.OK);
                    //cmdDetail.Enabled = false;


                }
                else if (icheck == 1)
                {
                   // Common.ShowError("Insert successful!", 1, MessageBoxButtons.OK);
                    //cmdDetail.Enabled = true;
                    DateTime dtDateLogin = DateTime.Now;
                    string strContent = "Channel Parameter";
                    int iLoglevel = 1;
                    string strWorked = "Insert";
                    string strTable = "GWTYPE";
                    string strOld_value = "";
                    string strNew_value = objGWtype.CONNECTION + "/" + objGWtype.GWTYPESTS + "/" + objGWtype.MSG_NO + "/" + objGWtype.DBLINK;
                    //goi ham ghilog
                    objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel,
                        strWorked, strTable, strOld_value, strNew_value);
                }
                else if (icheck == 2)
                {
                    Common.ShowError("Chanel exits!", 3, MessageBoxButtons.OK);
                   // cmdDetail.Enabled = false;
                }
            }
            catch  (Exception ex )
            {
                throw ex;
            }
        }

        private void Update_GWTYPE(int lGwtypeID)
        {
            try
            {
                int icheck;
                GWTYPEInfo objGWtype = new GWTYPEInfo();
                objControl = new GWTYPEController();
                if (CheckUpdate() == false)
                    return;

                objGWtype.GWTYPEID = lGwtypeID;
                objGWtype.CONNECTION = Convert.ToInt32(DHCONNECT);
                objGWtype.DBLINK = txtDBname.Text.Trim();
                objGWtype.DESCRIPTION = txtDescription.Text.Trim();
                objGWtype.GWTYPE = cboGWType.Text.Trim();
                objGWtype.GWTYPESTS = Convert.ToInt32(DHGWTYPESTS);
                objGWtype.MSG_NO = Convert.ToInt32(txtNumberOfMes.Text.Trim());


                icheck = objControl.UpdateGWTYPE(objGWtype);

                if (icheck == -1)
                {
                    Common.ShowError("Update fail.", 2, MessageBoxButtons.OK);

                }
                else if (icheck == 1)
                {
                   // Common.ShowError("Update successful!", 1, MessageBoxButtons.OK);

                    DateTime dtDateLogin = DateTime.Now;
                    string strContent = "Channel Parameter";//strCONNECTION/
                    int iLoglevel = 1;
                    string strWorked = "Update";
                    string strTable = "GWTYPE";
                    string strOld_value = DHCONNECT + "/" + strGWTYPEID + "/" + strGWTYPE + "/" + DHGWTYPESTS + "/" + strMSG_NO + "/" + strDBLINK;
                    string strNew_value = objGWtype.CONNECTION + "/" + strGWTYPEID + "/" + strGWTYPE + "/" + objGWtype.GWTYPESTS + "/" + objGWtype.MSG_NO + "/" + objGWtype.DBLINK;
                    //goi ham ghilog
                    objLog.GhiLogUser(dtDateLogin, Common.Userid, strContent, iLoglevel,
                        strWorked, strTable, strOld_value, strNew_value);
                }
                else if (icheck == 2)
                {
                    Common.ShowError("Chanel exits!", 3, MessageBoxButtons.OK);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_GWTYPE_DTL()
        {
           
            try
            {
                // Thuc hien Insert vao bang detail
                    // Check gia tri detail da ton tai trong table cua
                    // Neu chua thi thuc hien insert
                if (ADD_DETAIL == "ADD")
                {
                    objInfoGWDetail.ID = 0;
                }
                else
                {
                    objInfoGWDetail.ID = Convert.ToInt32(dataGridView1.Rows[iRows].Cells["ID"].Value);
                }
                    
                    objInfoGWDetail.GWTYPE = cboGWType.Text;
                    objInfoGWDetail.FTPPATH = txtFTPPath.Text.Trim();
                    objInfoGWDetail.FTPUSER = txtUser.Text.Trim();
                    objInfoGWDetail.FTPPASS = txtPassword.Text.Trim();
                    if (cbFolderType.SelectedValue.ToString() != "ALL")
                       objInfoGWDetail.FLDTYPE = Convert.ToInt32(cbFolderType.SelectedValue.ToString());

                    objInfoGWDetail.FOLDER = txtFolder.Text.Trim();

                    if (cboDirection.SelectedValue.ToString() != "ALL")
                       objInfoGWDetail.DIRECTION = Convert.ToInt32(cboDirection.SelectedValue.ToString());


                    objInfoGWDetail.DESCRIPTION = txtDescriptionDetail.Text.Trim();
                    objInfoGWDetail.FTPSERVER = txtFTPServer.Text.Trim();
                    objInfoGWDetail.FILETYPE = cboFileType.SelectedValue.ToString();
                    objControlGWDetail.AddGWTYPE_DETAIL(objInfoGWDetail);
                    //objControlGWDetail.UpdateGWTYPE_DETAIL(objInfoGWDetail);              
              
                cmdAddDetail.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private bool EDIT_GWTYPE_DTL()
        {
            try
            {
                Duplicate = 0;
                if (String.IsNullOrEmpty(txtFTPServer.Text) & string.IsNullOrEmpty(txtUser.Text) & string.IsNullOrEmpty(txtPassword.Text) & string.IsNullOrEmpty(cboFileType.Text) & string.IsNullOrEmpty(cboDirection.Text) & string.IsNullOrEmpty(txtFolder.Text))
                {
                    MessageBox.Show("You must input data!", Common.sCaption);
                    return false;
                }
               // Check_Input_Edit();
                if (Duplicate == 0)//khong co du lieu nao trung
                {
                    UpdateGrid(iRows);
                    return true;
                }
                else
                {
                    MessageBox.Show("Data has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {

                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }

        }
        
        private bool ADD_GWTYPE_DTL()
        {
            try
            {

                Duplicate = 0;

                int count = dataGridView1.Rows.Count-1;
                if (String.IsNullOrEmpty(txtFTPServer.Text) && string.IsNullOrEmpty(txtUser.Text) && string.IsNullOrEmpty(txtPassword.Text) && string.IsNullOrEmpty(cboFileType.Text) && string.IsNullOrEmpty(cboDirection.Text) && string.IsNullOrEmpty(txtFolder.Text))
                {
                    Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);

                    return false;
                }
                else
                {
                    if (count >= 0)
                    {
                        
                        //kiem tra check trung du lieu khong cho Insert
                        //Check_Input();
                        if (Duplicate == 0)//khong bi trung du lieu
                        {
                            DataRow datrow;
                            datrow = ds.Tables[0].NewRow();
                            ds.Tables[0].Rows.Add(datrow);
                                //dataGridView1.Rows.Add();
                            dataGridView1.Rows[count].Cells["ID"].Value = "0";
                            UpdateGrid(count);
                           
                        }
                        else
                        {
                            Common.ShowError("Data has already existed!", 3, MessageBoxButtons.OK);
                            return false;
                        }
                    }
                }


                //----------------------------------------------------------------------------------
                if (dataGridView1.Rows.Count == 0)
                {
                    cmdEditDetail.Enabled = false;
                    cmdRemove.Enabled = false;
                }
                else
                {
                    cmdEditDetail.Enabled = true;
                    cmdRemove.Enabled = true;
                }
                return true;

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }

        private void tbGwtypeINFO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbGwtypeINFO.SelectedTab  == tpdetail)
            {
                cmdAddDetail.Enabled = true;
                cmdEditDetail.Enabled = true;
                if (dataGridView1.Rows.Count > 1)
                {
                    dataGridView1.Focus();
                    dataGridView1.Rows[0].Selected = true;
                    cmdRemove.Enabled = true;
                }
            }
            else
            {
                cmdAddDetail.Enabled = false;
                cmdEditDetail.Enabled = false;
                cmdRemove.Enabled = false;
            }
        }


        private void UpdateGrid( int iRow)
        {
            try
            {
                dataGridView1.Rows[iRow].Cells["GWtype"].Value = cboGWType.Text.Trim();
                dataGridView1.Rows[iRow].Cells["FTPSERVER"].Value = txtFTPServer.Text.Trim();
                dataGridView1.Rows[iRow].Cells["FTPUser"].Value = txtUser.Text.Trim();
                dataGridView1.Rows[iRow].Cells["FTPPASS"].Value = txtPassword.Text.Trim();
                dataGridView1.Rows[iRow].Cells["Filetype"].Value = cboFileType.Text.Trim();
                dataGridView1.Rows[iRow].Cells["Direction"].Value = cboDirection.Text.Trim();
                dataGridView1.Rows[iRow].Cells["fldtype"].Value = cbFolderType.Text.Trim();
                dataGridView1.Rows[iRow].Cells["Folder"].Value = txtFolder.Text.Trim();
                dataGridView1.Rows[iRow].Cells["Description"].Value = txtDescriptionDetail.Text.Trim();
                dataGridView1.Rows[iRow].Cells["FTPPATH"].Value = txtFTPPath.Text.Trim();
                //-------------------------------------------------------------------------------------                        
                if (cboDirection.Text.Trim() == "")
                {
                    dataGridView1.Rows[iRow].Cells["DirectionT"].Value = "";
                }
                else
                {

                    dataGridView1.Rows[iRow].Cells["DirectionT"].Value = cboDirection.SelectedValue;
                }
                //------------------------------------------------------------------------------------                        
                if (cbFolderType.Text.Trim() == "")
                {
                    dataGridView1.Rows[iRow].Cells["FldtypeT"].Value = "";
                }
                else
                {
                    dataGridView1.Rows[iRow].Cells["FldtypeT"].Value = cbFolderType.SelectedValue;
                }
                //-------------------------------------------------------------------------------------                        
                if (cboFileType.Text.Trim() == "")
                {
                    dataGridView1.Rows[iRow].Cells["FiletypeT"].Value = "";
                }
                else
                {
                    dataGridView1.Rows[iRow].Cells["FiletypeT"].Value = cboFileType.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void ViewGWTYPE_DTL()
        {
            try
            {
                
                if (dataGridView1.Rows.Count > 0)
                {
                    txtFTPPath.Text = dataGridView1.Rows[iRows].Cells["FTPPATH"].Value.ToString();
                    txtFTPServer.Text = dataGridView1.Rows[iRows].Cells["FTPSERVER"].Value.ToString();
                    txtUser.Text = dataGridView1.Rows[iRows].Cells["FTPUser"].Value.ToString();
                    txtPassword.Text = dataGridView1.Rows[iRows].Cells["FTPPASS"].Value.ToString();

                    if (dataGridView1.Rows[iRows].Cells["Filetype"].Value.ToString() == "")
                    {
                        cboFileType.SelectedIndex = 0;
                    }
                    else
                    {
                        cboFileType.SelectedValue = dataGridView1.Rows[iRows].Cells["FiletypeT"].Value.ToString();
                    }

                    if (dataGridView1.Rows[iRows].Cells["Direction"].Value.ToString() == "")
                    {
                        cboDirection.SelectedIndex = 0;
                    }
                    else
                    {
                        cboDirection.SelectedValue = dataGridView1.Rows[iRows].Cells["DirectionT"].Value.ToString();
                    }

                    if (dataGridView1.Rows[iRows].Cells["fldtype"].Value.ToString() == "")
                    {
                        cbFolderType.SelectedIndex = 0;
                    }
                    else
                    {
                        cbFolderType.SelectedValue = dataGridView1.Rows[iRows].Cells["FldtypeT"].Value.ToString();
                    }

                    txtFolder.Text = dataGridView1.Rows[iRows].Cells["Folder"].Value.ToString();
                    txtDescriptionDetail.Text = dataGridView1.Rows[iRows].Cells["Description"].Value.ToString();

                }
                else
                {
                    return;
                }
                cmdEditDetail.Enabled = true;
                
            }
            catch (Exception ex)
           {
                throw (ex);
            }
        }

        private void cboGWType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdSave.Enabled = true;

        }


        private void txtDBname_TextChanged(object sender, EventArgs e)
        {
            cmdSave.Enabled = true;
        }

        private void cboGWType_Validated(object sender, EventArgs e)
        {
            iInfoEdit = 1;
            cmdSave.Enabled = true;
        }

        private void cboGWType_Validated(object sender, CancelEventArgs e)
        {
            iInfoEdit = 1;
            cmdSave.Enabled = true;
        }

       
      
      
        
    }
}
