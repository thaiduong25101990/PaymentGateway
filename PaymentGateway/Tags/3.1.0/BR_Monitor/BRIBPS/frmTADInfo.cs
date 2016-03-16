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
using System.Text.RegularExpressions;
//using BR.DataAccess;
using System.Data.OracleClient;

namespace BR.BRIBPS
{
    public partial class frmTADInfo : frmBasedata 
    {
        /*---------------------------------------------------------------
        * Muc dich         : Quan ly cac cong tham gia CITAD
        * Ngay tao         : 14/06/2008
        * Nguoi tao        : HaNTT10
        *--------------------------------------------------------------*/
        private bool isInsert = false;
        private bool isLoadData = false;
        private DataSet datDs = new DataSet();
        DataSet dsArea_GWBankCode = new DataSet();
        private TADInfo objInfo = new TADInfo();
        private GetData objGetData = new GetData();
        private TADController objControl = new TADController();
        private BRANCHInfo objInfoBRANCH = new BRANCHInfo();
        private BRANCHController objControlBRANCH = new BRANCHController();
        private CURRENCYController objControlCURRENCY = new CURRENCYController();
        private ALLCODEController objAllcode = new ALLCODEController();
        //private BRANCHController objControlBRANCH = new BRANCHController();
        public IBPS_BANK_MAPInfo objInfoIBPS_Bank_Map = new IBPS_BANK_MAPInfo();
        public IBPS_BANK_MAPController objControlIBPS_Bank_Map = new IBPS_BANK_MAPController();
        public AREAController objAREAController = new AREAController();
        private SYSVARController objControlSYSVAR = new SYSVARController();

        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private string strSBV;
        //private string strSBV_TAD;
        private int m_irow;
        private bool isSave=false;
        private static int iRow;
        private static int iID = 0;
        private clsCheckInput checkInput = new clsCheckInput();
        private int length = 0;
        public static bool isTADInfo = false;
        public static string strGW_BANK_CODE;
        private string sClose;
        private string AreaView;
        private static string REEEEE;
        private bool NeedConfirm = true;
        private static bool strSucess = false;
        private string Cancel;
        private string strtxtSIBSBankCode;
        clsCheckInput clsCheck = new clsCheckInput();
        public static bool isTAD = false;
        private int kCout;
        private int Count1;
        //Du lieu cu de ghi log--------------------------------
         private string strID = "";
         private string strCitadID = "";
         //private string strGWBankCode = "";
         private string strExportPath = "";
         private string strImportPath = "";
         private string strFTPDirectory = "";
         private string strUser1 = "";
         private string strPassword = "";
         private string strSBV_TAD = "";
         private string strCCYCD = "";
         private string strGWBankCode1 = "";
         private string strSIBSBankCode = "";
         private string strConnection = "";
         private string strDBLink = "";
         private string strFunction = "";     
        //-----------------------------------------------------
        public frmTADInfo()
        {
            InitializeComponent();
            txtSIBSBankCode.Enabled = false;
            lblSIBSBankCode.Enabled = false;
            //lblGWBankCode.Text = lblSIBSBankCode.Text;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                kCout = 1;
                Count1 = iRow;
                strtxtSIBSBankCode = txtSIBSBankCode.Text.Trim();
                txtGWBankCode.Enabled = true;
                txtAmount.Enabled = true;
                txtImportPath.Enabled = true;
                txtExportPath.Enabled = true;
                txtFTPDirectory.Enabled = true;
                //txtGWBankCode.Enabled = false;
                txtUser.Enabled = true;
                txtPassword.Enabled = true;
                cboArea.Enabled = false;
                cboCCYCD.Enabled = true;
                mskHour.Enabled = true;
                //----------------------------------
                strGW_BANK_CODE = txtGWBankCode.Text.Trim();
                this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                isInsert = false;
                txtGWBankCode.Focus();
                LockTextBox(false);
                CommandStatus(false);
                //-------------------------
                //cmdSave.Enabled = true;
                //cmdDelete.Enabled = false;
                //cmdEdit.Enabled = false;
                //cmdAdd.Enabled = false;
                //dgView.Enabled = false;
                //cmdCancel.Enabled = true; 
                //LoadData();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (Common.iSconfirm == 1)
                {
                    if (dgView.CurrentRow.Cells[12].Value.ToString() != "CLOSED")
                    {
                        MessageBox.Show("Can not delete because the status of this CITAD is not closed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //cmdAdd.Enabled = true;
                        //cmdEdit.Enabled = true;
                        //cmdDelete.Enabled = true;
                        CommandStatus(true);
                        return;
                    }
                    if (objControl.DeleteTAD(iID) == 1)
                    {
                        MessageBox.Show("Delete successful", Common.sCaption);
                        CommandStatus(true);
                    }
                   // iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                    if (cboAreaView.Text.Trim() == "ALL")
                    {
                        LoadData();
                    }
                    else if (cboAreaView.Text.Trim() != "")
                    {
                        Getdateta(cboAreaView.SelectedValue.ToString());
                        //LoadBankTotal();
                    }
                 
                    CommandStatus(true);
                    LoadData();

                    //lay thong tin de ghilog----------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string useride = BR.BRLib.Common.Userid;
                    string strConten = "IBPS TAD list";
                    int Log_level = 1;
                    string strWorked = "Delete";
                    string strTable = "TAD";
                    string strOld_value = strID + "/" + strCitadID + "/" + strGWBankCode1 + "/" + strExportPath + "/" + strImportPath + "/" + strFTPDirectory + "/" + strUser1 + "/" + strPassword + "/" + strSBV_TAD + "/" + strCCYCD + "/" + strSIBSBankCode + "/" + strConnection + "/" + strDBLink + "/" + strFunction;
                    string strNew_value = iID.ToString();
                    //-----------------------------------------
                    Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                }
                else
                {
                   
                    CommandStatus(true);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                isSave = true;
                m_irow = dgView.CurrentRow.Index;
                //int f = 0;
                if (Common.iSconfirm==1)
                {
                    if (txtSBV_TAD.Text.Trim() == "")
                    {
                        MessageBox.Show("You must input data!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSBV_TAD.Focus();
                        cmdAdd.Enabled = false;
                        cmdSave.Enabled = true;
                        cmdDelete.Enabled = false;
                        cmdEdit.Enabled = false;
                        return;
                    }
                    else
                    {
                    if ((string.IsNullOrEmpty(txtGWBankCode.Text.Trim()) || (string.IsNullOrEmpty(txtCitadID.Text.Trim()))))
                    {
                        MessageBox.Show("You must input data!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGWBankCode.Focus();
                        CommandStatus(false);
                        return;
                    }                    
                    else
                    {
                        #region // HaNTT10 them vao ngay 24/10/2008
                        if (cboFunction.Text.Trim().ToUpper() == "HEAD OFFICE")
                        {
                            DataTable dtMainBranch = new DataTable();
                            dtMainBranch = objControl.GetTAD_CheckHeadOffice(txtCitadID.Text.Trim());
                            if (dtMainBranch.Rows.Count == 0 )//|| dtMainBranch.Rows.Count == 1)
                            {
                                //return;
                            }
                            else
                            {
                                MessageBox.Show("Existed Head Office!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cboFunction.Focus();
                                cboFunction.SelectAll();
                                CommandStatus(false);
                                return;
                            }
                        }
                        #endregion
                        else if (cboFunction.Text.Trim() == "Main Branch" || cboFunction.Text.Trim() == "Both (HO and MB)")
                        {
                            DataTable dtMainBranch = new DataTable();
                            dtMainBranch = objControl.GetTAD_CheckMainBranch(txtGWBankCode.Text.Trim());
                            if (dtMainBranch.Rows.Count == 0)
                            {
                                //return;
                            }
                            else
                            {
                                MessageBox.Show("Existed main branch in this area!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cboFunction.Focus();
                                cboFunction.SelectAll();
                                CommandStatus(false);
                                return;
                            }
                        }

                        dsArea_GWBankCode = objAREAController.GetAREA_GWBankCode(txtGWBankCode.Text.Trim());
                        if (dsArea_GWBankCode.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("This area is not a CITAD member!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);//HaNTT10 sua ngay 08.10.2008
                            cboArea.Text = "";
                            txtGWBankCode.Focus();
                            CommandStatus(false);
                            return;
                        }
                        else
                        {
                            cboArea.Text = dsArea_GWBankCode.Tables[0].Rows[0]["FULLNAME"].ToString();
                        }

                        string strLength = "SIBSBankCodeLength";
                        DataSet dsIBPSBankLength = new DataSet();
                        dsIBPSBankLength = objControlSYSVAR.GetIBPSBankLength(strLength);
                        if (dsIBPSBankLength.Tables[0].Rows.Count == 0)
                        {
                            return;
                        }
                        else
                        {
                            length = Convert.ToInt32(dsIBPSBankLength.Tables[0].Rows[0][0].ToString());

                        }
                        DataSet dsGWBankCode = new DataSet();
                        dsGWBankCode = objControlIBPS_Bank_Map.GetIBPS_BANK_MAP_GWBankCode(length, txtGWBankCode.Text.Trim());
                        if (dsGWBankCode.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("Invalid State bank code!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtGWBankCode.Focus();
                            CommandStatus(false);
                            return;
                        }
                        #region //----Du lieu de in sert----------------------------------------
                        objInfo.TADID = iID;
                        objInfo.TAD = txtCitadID.Text.Trim();
                        objInfo.TAD_NAME = lblGWBankCode.Text.Trim();
                        objInfo.GW_BANK_CODE = clsCheck.ConvertVietnamese(txtGWBankCode.Text.Trim());
                        objInfo.EXPORT_FOLDER = clsCheck.ConvertVietnamese(txtExportPath.Text.Trim());
                        objInfo.IMPORT_FOLDER = clsCheck.ConvertVietnamese(txtImportPath.Text.Trim());
                        objInfo.FTPPATH = clsCheck.ConvertVietnamese(txtFTPDirectory.Text.Trim());
                        objInfo.FTPUSER = clsCheck.ConvertVietnamese(txtUser.Text.Trim());
                        objInfo.FPTPASS = clsCheck.ConvertVietnamese(txtPassword.Text.Trim());
                        
                        string strFunction;
                        strFunction = cboFunction.SelectedValue.ToString();
                        char chrFunction;
                        chrFunction = Convert.ToChar(strFunction);
                        objInfo.FUNCTION = strFunction;
                        objInfo.DBLINK = clsCheck.ConvertVietnamese(txtDBLink.Text.Trim()); 
                        objInfo.SBV_TADID = txtSBV_TAD.Text;//QUYND them vi cap nhat them truong SBV_TADID
                        if (cboArea.Text.Trim() == "")
                        {
                            objInfo.AREA = "";
                        }
                        else
                        {
                            DataTable datAre = new DataTable();
                            datAre = objAREAController.Search(cboArea.Text.Trim());
                            objInfo.AREA = datAre.Rows[0]["PROV_CODE"].ToString();
                            //objInfo.AREA = Convert.ToInt32(cboArea.SelectedValue);//Convert.ToInt32(cboArea.Text.Trim());.ValueMember
                        }
                        objInfo.CONNECTION = Convert.ToInt32(cboConnection.SelectedValue.ToString());
                        if (mskHour.Text.Trim() == ":")
                        {
                            objInfo.TIME = "";
                        }
                        else
                        {
                            objInfo.TIME = mskHour.Text.Trim();
                        }
                        if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
                        {
                            objInfo.AMOUNT = 0;
                        }
                        else
                        {
                            objInfo.AMOUNT = Convert.ToDouble(txtAmount.Text.Trim());
                        }
                        objInfo.CCYCD = cboCCYCD.Text.Trim();
                        if (cboCitadStatus.Text == "")
                        {
                            objInfo.STATUS = 0;
                        }
                        else if (cboCitadStatus.Text != "")
                        {
                            objInfo.STATUS = Convert.ToInt32(cboCitadStatus.SelectedValue.ToString()); //Convert.ToInt32(cboCitadStatus.Text.Trim());
                        }
                        //objInfo.SIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                        if (txtSIBSBankCode.Text.Trim().Length == 5)
                        {
                            objInfo.SIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                        }
                        else
                        {
                            objInfo.SIBS_BANK_CODE = "00" + txtSIBSBankCode.Text.Trim();
                        }
                        if (chkHVLVAllow.Checked == false)
                        {
                            objInfo.SET_LOW_VALUE = 0;
                        }
                        else
                        {
                            objInfo.SET_LOW_VALUE = 1;
                        }
                        #endregion
                        //--Neu truong txtGWBankCode co du lieu trung voi du lieu ban dau thi khong kiem tra nua
                        #region Kiem tra trung 1-----------
                        if (strGW_BANK_CODE == txtGWBankCode.Text.Trim())//chi trong truong hop update
                        {                            
                            if (!CheckID())
                            {
                                return;
                            }                           

                            DataTable datSBV = new DataTable();

                            string strWhere = strWhere = " and trim(SBV_TADID) <> " + txtSBV_TAD.Text.ToString().Trim(); 
                            
                            datSBV = objControl.Search_Sbv(txtSBV_TAD.Text.Trim(), txtGWBankCode.Text.Trim(), strWhere);
                            if (datSBV.Rows.Count != 0)
                            {
                                MessageBox.Show("SBV tad exists!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSBV_TAD.Focus();
                                CommandStatus(false);
                                return;
                            }
                            else
                            {
                            }

                            if (objControl.UpdateTAD(objInfo) == 1)
                            {
                                MessageBox.Show("Data has updated successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CommandStatus(true);
                                LockTextBox(true);
                                
                            }
                            else
                            {
                                MessageBox.Show("Data has updated Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CommandStatus(false);
                            }
                            //lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "IBPS TAD list";
                            int Log_level = 1;
                            string strWorked = "Update";
                            string strTable = "TAD";
                            string strOld_value = strCitadID + "/" + strGWBankCode1 + "/" + "00" + strSIBSBankCode + "/" + strExportPath + "/" + strImportPath + "/" + strFTPDirectory + "/" + strUser1 + "/" + strPassword + "/" + strSBV_TAD + "/" + strCCYCD + "/" + strConnection + "/" + strFunction + "/" + strDBLink;
                            string strNew_value = objInfo.TAD + "/" + objInfo.GW_BANK_CODE + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.EXPORT_FOLDER + "/" + objInfo.IMPORT_FOLDER + "/" + objInfo.FTPPATH + "/" + objInfo.FTPUSER + "/" + objInfo.FPTPASS + "/" + objInfo.SBV_TADID + "/" + objInfo.CCYCD + "/" + objInfo.CONNECTION +"/" + objInfo.FUNCTION + "/" + objInfo.DBLINK;
                            //-----------------------------------------
                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            LoadData();
                            dgView.Enabled = true;
                            
                        }
                        #endregion  -------------------------------------

                        #region
                        else//neu khong trung thi kiem tra xem trong database co du lieu khong
                        {
                            //kiem tra du lieu co bi trung khong 
                            DataTable datTAD_TAD = new DataTable();
                            datTAD_TAD = objControl.GetTAD_TAD(txtGWBankCode.Text.Trim());
                            if (datTAD_TAD.Rows.Count != 0)//neu da ton tai roi thi khong duoc update nua
                            {
                                MessageBox.Show("State Bank ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtGWBankCode.Focus();
                                txtGWBankCode.SelectAll();
                                CommandStatus(false);
                            }
                            else
                            {
                                if (isInsert)
                                {

                                    DataTable datSBV = new DataTable();
                                    string strWhere = "";
                                    datSBV = objControl.Search_Sbv(txtSBV_TAD.Text.Trim(), txtGWBankCode.Text.Trim(), strWhere);
                                    if (datSBV.Rows.Count != 0)
                                    {
                                        MessageBox.Show("SBV tad exists!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtSBV_TAD.Focus();
                                        CommandStatus(false);
                                        return;
                                    }                                    
                                    if (GetData.IDIsExisting(false, "TAD", "TAD", txtCitadID.Text.Trim(), ""))
                                    //if (GetData.ID2IsExisting(false, "TAD","GW_BANK_CODE", "SIBS_BANK_CODE", txtGWBankCode.Text.Trim(),"00" + txtSIBSBankCode.Text.Trim()))
                                    {
                                        MessageBox.Show("State Bank ID has already existed in CITAD list!", Common.sCaption);
                                        txtCitadID.Text = "";
                                        txtGWBankCode.Focus();
                                        txtGWBankCode.SelectAll();
                                        CommandStatus(false);
                                        return;
                                    }
                                    if (objControl.AddTAD(objInfo) == 1)
                                    {
                                        MessageBox.Show("Data has inserted successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.None);
                                        CommandStatus(true);
                                        LockTextBox(false);
                                        //dgView.Rows[iRow].Cells[0].Selected = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Data has inserted Error!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtGWBankCode.Focus();
                                        CommandStatus(false);
                                    }

                                    //lay thong tin de ghilog----------------------
                                    DateTime dtLog = DateTime.Now;
                                    string strUser = BR.BRLib.Common.strUsername;
                                    string useride = BR.BRLib.Common.Userid;
                                    string strConten = "IBPS TAD list" + objInfo.GW_BANK_CODE;
                                    int Log_level = 1;
                                    string strWorked = "Insert";
                                    string strTable = "TAD";
                                    string strOld_value = "";
                                    string strNew_value = objInfo.TAD + "/" + objInfo.TAD_NAME + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.GW_BANK_CODE + "/" + objInfo.EXPORT_FOLDER + "/" + objInfo.IMPORT_FOLDER + "/" + objInfo.FTPPATH + "/" + objInfo.FTPUSER + "/" + objInfo.FPTPASS + "/" + objInfo.FUNCTION + "/" + objInfo.DBLINK;
                                    //-----------------------------------------
                                    Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);

                                }
                                else //neu la UPDATE
                                {
                                    if (!CheckID())
                                    {
                                        return;
                                    }

                                    DataTable datSBV = new DataTable();
                                    string strWhere = " and trim(SBV_TADID) <> " + strSBV.ToString().Trim();
                                    datSBV = objControl.Search_Sbv(txtSBV_TAD.Text.Trim(), txtGWBankCode.Text.Trim(), strWhere);
                                    if (datSBV.Rows.Count != 0)
                                    {
                                        MessageBox.Show("SBV tad exists!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtSBV_TAD.Focus();
                                        CommandStatus(false);
                                        return;
                                    }
                                    
                                    if (objControl.UpdateTAD(objInfo) == 1)
                                    {
                                        MessageBox.Show("Data has updated successfully", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.None);
                                        CommandStatus(true);
                                        //lay thong tin de ghilog----------------------
                                        DateTime dtLog = DateTime.Now;
                                        string strUser = BR.BRLib.Common.strUsername;
                                        string useride = BR.BRLib.Common.Userid;
                                        string strConten = "IBPS TAD list";
                                        int Log_level = 1;
                                        string strWorked = "Update";
                                        string strTable = "TAD";
                                        string strOld_value = strID + "/" + strCitadID + "/" + strGWBankCode1 + "/" + strExportPath + "/" + strImportPath + "/" + strFTPDirectory + "/" + strUser1 + "/" + strPassword + "/" + strSBV_TAD + "/" + strCCYCD + "/" + strSIBSBankCode + "/" + strConnection + "/" + strDBLink + "/" + strFunction;
                                        string strNew_value = objInfo.TAD + "/" + objInfo.TAD_NAME + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.GW_BANK_CODE + "/" + objInfo.EXPORT_FOLDER + "/" + objInfo.IMPORT_FOLDER + "/" + objInfo.FTPPATH + "/" + objInfo.FTPUSER + "/" + objInfo.FPTPASS + "/" + objInfo.FUNCTION + "/" + objInfo.DBLINK;
                                        //-----------------------------------------
                                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Data has updated Error", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        CommandStatus(false);
                                    } 
                                }
                                LoadData();
                                LockTextBox(true);
                                CommandStatus(true);
                                if (cboAreaView.Text.Trim() == "ALL")
                                {
                                    LoadData();
                                }
                                else if (cboAreaView.Text.Trim() != "")
                                {
                                    Getdateta(cboAreaView.SelectedValue.ToString());
                                    //LoadBankTotal();
                                }                               
                            }
                        }
                        #endregion
                    }
                    }
                }
                else
                {
                    //ClearText();
                    LockTextBox(true);
                    CommandStatus(true);                    
                    return;
                }
                //dgView.Rows[iRow].Cells[0].Selected = true;
                dgView.Rows[m_irow].Selected = true;               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            kCout = 0;
        }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdDelete.Enabled = a;
            cmdSave.Enabled = !a;
            cmdCancel.Enabled = !a;
            dgView.Enabled = a;
            cboAreaView.Enabled = a;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                kCout = 0;
                cboCCYCD.Enabled = false;
                txtGWBankCode.Enabled = true;
                txtGWBankCode.Focus();
                txtAmount.Enabled = true;
                txtImportPath.Enabled = true;
                txtExportPath.Enabled = true;
                txtFTPDirectory.Enabled = true;
                txtUser.Enabled = true;
                txtPassword.Enabled = true;
                //cboCCYCD.Enabled = true;
                mskHour.Enabled = true;
                txtSBV_TAD.Text = "";
                strSBV = "";
                strGW_BANK_CODE = "";
                this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                LockTextBox(false);
                ClearText();
                cboArea.Enabled = false;
                isInsert = true;
                //dgView.Enabled = false;
                //cmdAdd.Enabled = false;
                //cmdEdit.Enabled = false;
                //cmdSave.Enabled = true;
                //cmdDelete.Enabled = false;
                //cboAreaView.Enabled = false;
                ////LoadData();
                //cmdCancel.Enabled = true;
                CommandStatus(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdImportPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && folderBrowserDialog1.SelectedPath != "")
                txtImportPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void cmdExportPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && folderBrowserDialog1.SelectedPath != "")
                txtExportPath.Text = folderBrowserDialog1.SelectedPath;
        }

        /*---------------------------------------------------------------
         * Muc dich         : Ham ghi log
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay cap nhat    : 11/07/2008
         * Nguoi cap nhat   : HaNTT10
         * Ngay  tao  : 31/54/2008
         * Nguoi tao   : Quynd
         *--------------------------------------------------------------*/
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

        private bool CheckID()
        {
            bool result = true;
            string ID = txtCitadID.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "You must input textbox!";
                result = false;
            }
            else if (ID.Length > 30)
            {
                MessageBox.Show("The max length of value is 30 characters");
                result = false;
            }
            return result;
        }
        private void LoadData()
        {
            dgView.MultiSelect = false;
            DataSet dtSet = new DataSet();
            dtSet = objControl.GetTADInfo();
            dgView.DataSource = dtSet.Tables[0];
            if (dtSet.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                FormatGridView(dtSet);
            }
        }

        private void LockTextBox(Boolean a)
        {
           // //txtCitadID.ReadOnly = a;
           // txtExportPath.ReadOnly = a;
           // txtFTPDirectory.ReadOnly = a;
           // txtGWBankCode.ReadOnly = a;
           // txtImportPath.ReadOnly = a;
           // txtPassword.ReadOnly = a;
           // txtSIBSBankCode.ReadOnly = a;
           // txtUser.ReadOnly = a;
           // //cboArea.Enabled = !a;
           // cboConnection.Enabled = !a;
           //// chkHOST.Enabled = !a;
           // chkHVLVAllow.Enabled = !a;
           // //chkMain.Enabled = !a;
           // txtAmount.ReadOnly = a;
           // mskHour.Enabled = !a;
           // cmdExportPath.Enabled = !a;
           // cmdImportPath.Enabled = !a;
           // cboCCYCD.Enabled = !a;
           // cboCitadStatus.Enabled = !a;
           // txtSIBSBankCode.Enabled = !a;
           // //txtDBLink.Enabled = !a;
           // cboFunction.Enabled = !a; 


            //txtCitadID.ReadOnly = a;
            txtExportPath.Enabled = !a;
            txtFTPDirectory.Enabled = !a;
            txtGWBankCode.Enabled=!a;
            txtImportPath.Enabled = !a;
            txtPassword.Enabled = !a;
            txtSIBSBankCode.Enabled = !a;
            txtUser.Enabled = !a;
            txtSBV_TAD.Enabled = !a;
            //cboArea.Enabled = !a;
            cboConnection.Enabled = !a;
            // chkHOST.Enabled = !a;
            chkHVLVAllow.Enabled = !a;
            //chkMain.Enabled = !a;
            txtAmount.Enabled = !a;
            mskHour.Enabled = !a;
            cmdExportPath.Enabled = !a;
            cmdImportPath.Enabled = !a;
            cboCCYCD.Enabled = !a;
            cboCitadStatus.Enabled = !a;
            txtSIBSBankCode.Enabled = !a;
            //txtDBLink.Enabled = !a;
            cboFunction.Enabled = !a; 
        }
        private void frmTADInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                kCout = 0;
                isLoadData = true;
                AreaView = "0";
                sClose = "0";
                LoadData();                
                cboConnection.DataSource = objAllcode.GetALLCODE("Connection", "SYSTEM");
                cboConnection.DisplayMember = "CONTENT";
                cboConnection.ValueMember = "CDVAL";
                //GetCurrency2
                //GetData.getDataCombo1(cboCCYCD, "CCYCD", "CURRENCY");

                DataTable dtCURRENCY = new DataTable();
                dtCURRENCY = objControlCURRENCY.GetCurrency2("IBPS");
                cboCCYCD.DataSource = dtCURRENCY;
                cboCCYCD.DisplayMember = "CCYCD";

                int strStatus = Convert.ToInt32(objInfo.STATUS);
                DataTable datAll_code = new DataTable();
                datAll_code = objAllcode.GetALLCODE("CITADSTS", "IBPS");
                cboCitadStatus.DataSource = datAll_code;
                cboCitadStatus.DisplayMember = "CONTENT";
                cboCitadStatus.ValueMember = "CDVAL";
                cboCitadStatus.SelectedIndex = strStatus - 1;

                
                DataSet dsArea = new DataSet();
                dsArea = objAREAController.GetAREA();  
                int i = 0;
                //cboArea.Items.Add("ALL");
                while (i < dsArea.Tables[0].Rows.Count)
                {
                    string strContent = dsArea.Tables[0].Rows[i]["FULLNAME"].ToString();
                    string strPROV_CODE = dsArea.Tables[0].Rows[i]["PROV_CODE"].ToString();
                    cboArea.Items.Add(strContent);
                    cboArea.ValueMember = strPROV_CODE;
                    i = i + 1;
                }
                //Area view
                DataRow row; 
                row = dsArea.Tables[0].NewRow();
                                
                row["PROV_CODE"] = "0";
                row["SHORTNAME"] = "ALL";
                row["FULLNAME"] = "ALL";
                row["CITAD_MEMBER"] = "1";
                row["CONTENT"] = "ALL";
                dsArea.Tables[0].Rows.Add(row); 

                cboAreaView.DataSource = dsArea.Tables[0];
                cboAreaView.DisplayMember = "FULLNAME";
                cboAreaView.ValueMember = "PROV_CODE";
                 
                //cboAreaView.Items.Add("ALL");
                //if (dsArea.Tables[0].Rows.Count > 0)
                //{
                //    cboArea.SelectedIndex = 0;
                //}
                //else
                //{ 
                //}
                cboAreaView.SelectedIndex = cboAreaView.Items.Count - 1 ;
                //cboAreaView.Sorted = true;
                LockTextBox(true);
                //GetData.getDataComboWhere(cboArea, "Content", "allcode", "CDName", "Area");
                cboCitadStatus.Enabled = false;
                //GetData.getDataComboWhere(cboAreaView, "Content", "allcode", "CDName", "Area");
                
                
                // Load combobox Function
                if (!objGetData.FillDataComboBox(cboFunction, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='IBPS' AND CDNAME='FUNCTION'", "CONTENT", true, false, ""))
                    return;

                string strTad = objControl.GetROUTER_TAD();
                //if ((!Convert.ToInt32(strTad).Equals(0)) || (strTad.Equals("")))
                if (strTad == "0")
                {
                    //chkCentralized.Checked = true;
                    chkCentralized.Checked = false;
                }
                else
                {
                    chkCentralized.Checked = true;
                }
                cboCCYCD.Text = "VND";
                chkCentralized.Enabled = false;
                txtDBLink.Enabled = false;
                txtCitadID.Enabled = false;
                CommandStatus(true);
                dgView.Select();
                //dgView_CellContentClick(null, null);
                //cmdCancel.Enabled = false; 
             }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void ClearText()
        {
            txtAmount.Text = "";
            txtCitadID.Text = "";
            txtExportPath.Text = "";
            txtFTPDirectory.Text = "";
            txtGWBankCode.Text = "";
            txtImportPath.Text = "";
            txtPassword.Text = "";
            txtSIBSBankCode.Text = "";
            mskHour.Text = "";
            txtUser.Text = "";
            //chkHOST.Checked = false;
            chkHVLVAllow.Checked = false;
            //chkMain.Checked = false;
            lblGWBankCode.Text = "";
            lblSIBSBankCode.Text = "";
            cboCCYCD.Text = "";
            cboConnection.Text = "";
            txtDBLink.Text = "";
            cboFunction.Text = ""; 
        }

        private void txtGWBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                frmView frmview = new frmView();
                isTADInfo = true;
                frmview.ShowDialog(this);
                if (frmview.objInfo.GW_BANK_CODE == null)
                {
                    return;
                }
                else
                {
                    txtGWBankCode.Text = frmview.objInfo.GW_BANK_CODE;
                    txtSIBSBankCode.Text = frmview.objInfo.SIBS_BANK_CODE;
                    lblSIBSBankCode.Text = frmview.objInfoBRANCH.BRAN_NAME;
                    lblGWBankCode.Text = frmview.objInfo.BANK_NAME;
                    txtCitadID.Text = "TAD" + frmview.strCitadID;
                    this.Show();
                    isTADInfo = false;
                }
            }
            
        }

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int HVLVallow = 0;
            try
            {
                #region
                //iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                //txtCitadID.Text = dgView.CurrentRow.Cells[1].Value.ToString();
                ////lblGWBankCode.Text = dgView.CurrentRow.Cells[2].Value.ToString();
                ////lblSIBSBankCode.Text = dgView.CurrentRow.Cells[13].Value.ToString();
                ////lblGWBankCode.Text = dgView.CurrentRow.Cells[2].Value.ToString();
                //lblSIBSBankCode.Text = dgView.CurrentRow.Cells[2].Value.ToString();
                //txtGWBankCode.Text = dgView.CurrentRow.Cells[3].Value.ToString();
                //txtExportPath.Text = dgView.CurrentRow.Cells[4].Value.ToString();
                //txtImportPath.Text = dgView.CurrentRow.Cells[5].Value.ToString();
                //txtFTPDirectory.Text = dgView.CurrentRow.Cells[6].Value.ToString();
                //txtUser.Text = dgView.CurrentRow.Cells[7].Value.ToString();
                //txtPassword.Text = dgView.CurrentRow.Cells[8].Value.ToString();


                //mskHour.Text = dgView.CurrentRow.Cells[10].Value.ToString() + "";
                //cboCCYCD.Text = dgView.CurrentRow.Cells[17].Value.ToString();
                //string strAAM = dgView.CurrentRow.Cells[11].Value.ToString();
                ////-----format tien-------------------------------------------------
                ////if (cboCCYCD.Text.Trim() == "VND")
                ////{ txtAmount.Text = Common.FormatCurrency(strAAM.Replace("\r", "").Replace("\r\n", ""), "{0:#,###}"); }
                ////else { txtAmount.Text = Common.FormatCurrency(strAAM.Replace("\r", "").Replace("\r\n", ""), "{0:#,###.##}"); }
                ////-----------------------------------------------------------------------------------------               
                //txtAmount.Text = Common.FormatCurrency(strAAM.Trim(), Common.FORMAT_CURRENCY);
                ////cboCitadStatus.Text = dgView.CurrentRow.Cells[13].Value.ToString();
                //string CitadStatus = dgView.CurrentRow.Cells[12].Value.ToString();
                //cboCitadStatus.Text = CitadStatus;

                //lblSIBSBankCode.Text = dgView.CurrentRow.Cells[2].Value.ToString();
                //txtGWBankCode.Text = dgView.CurrentRow.Cells[3].Value.ToString();
                //txtSIBSBankCode.Text = dgView.CurrentRow.Cells[13].Value.ToString();
                //DataSet dsGWBankCode = new DataSet();
                //dsGWBankCode = objControlIBPS_Bank_Map.GetIBPS_BANK_MAP_BankName(txtGWBankCode.Text.Trim());
                //if (dsGWBankCode.Tables[0].Rows.Count == 0)
                //{
                //    lblGWBankCode.Text = "";
                //}
                //else
                //{
                //    //lblSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["SIBS_BANK_CODE"].ToString();
                //    lblGWBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["BANK_NAME"].ToString();
                //}

                //if (!string.IsNullOrEmpty(dgView.CurrentRow.Cells[14].Value.ToString()))
                //{
                //    HVLVallow = Convert.ToInt32(dgView.CurrentRow.Cells[14].Value.ToString());
                //}
                //else
                //{
                //    HVLVallow = 0;
                //}

                //if (HVLVallow == 1)
                //{
                //    chkHVLVAllow.Checked = true;
                //}
                //else
                //{
                //    chkHVLVAllow.Checked = false;
                //}
                //string connection = dgView.CurrentRow.Cells["CONNECTION"].Value.ToString().Trim();
                //if (connection == "")
                //{
                //    this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                //    cboConnection.Text = "";
                //}
                //else
                //{
                //    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                //    cboConnection.Text = connection;
                //}

                //string strArea = dgView.CurrentRow.Cells["AREA"].Value.ToString().Trim();
                //if (strArea == "")
                //{
                //    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                //    cboArea.Text = "";
                //}
                //else
                //{
                //    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                //    cboArea.Text = strArea;
                //}

                //cboConnection.Text = dgView.CurrentRow.Cells["CONNECTION"].Value.ToString().Trim();
                //txtDBLink.Text = dgView.CurrentRow.Cells[20].Value.ToString();
                ////cboFunction.Text = dgView.CurrentRow.Cells[19].Value.ToString();
                //cboFunction.Text = dgView.CurrentRow.Cells["FUNCTION"].Value.ToString();
                ////cboArea.Text 

                #endregion
                iRow = e.RowIndex;
                iID = Convert.ToInt32(dgView.Rows[iRow].Cells[0].Value.ToString());
                txtCitadID.Text = dgView.Rows[iRow].Cells[1].Value.ToString();               
                lblSIBSBankCode.Text = dgView.Rows[iRow].Cells[2].Value.ToString();
                txtGWBankCode.Text = dgView.Rows[iRow].Cells[3].Value.ToString();
                txtExportPath.Text = dgView.Rows[iRow].Cells[4].Value.ToString();
                txtImportPath.Text = dgView.Rows[iRow].Cells[5].Value.ToString();
                txtFTPDirectory.Text = dgView.Rows[iRow].Cells[6].Value.ToString();
                txtUser.Text = dgView.Rows[iRow].Cells[7].Value.ToString();
                txtPassword.Text = dgView.Rows[iRow].Cells[8].Value.ToString();
                txtSBV_TAD.Text = dgView.Rows[iRow].Cells[21].Value.ToString();
                strSBV = dgView.Rows[iRow].Cells[21].Value.ToString();
                mskHour.Text = dgView.Rows[iRow].Cells[10].Value.ToString() + "";
                cboCCYCD.Text = dgView.Rows[iRow].Cells[17].Value.ToString();
                string strAAM = dgView.Rows[iRow].Cells[11].Value.ToString();
                //-----format tien-------------------------------------------------
                //if (cboCCYCD.Text.Trim() == "VND")
                //{ txtAmount.Text = Common.FormatCurrency(strAAM.Replace("\r", "").Replace("\r\n", ""), "{0:#,###}"); }
                //else { txtAmount.Text = Common.FormatCurrency(strAAM.Replace("\r", "").Replace("\r\n", ""), "{0:#,###.##}"); }
                //-----------------------------------------------------------------------------------------               
                if (string.IsNullOrEmpty(strAAM.Trim()))
                {
                    txtAmount.Text = "";
                }
                else
                {
                    txtAmount.Text = Common.FormatCurrency(strAAM.Trim(), Common.FORMAT_CURRENCY);
                }
                //cboCitadStatus.Text = dgView.Rows[iRow].Cells[13].Value.ToString();
                string CitadStatus = dgView.Rows[iRow].Cells[12].Value.ToString();
                cboCitadStatus.Text = CitadStatus;

                lblSIBSBankCode.Text = dgView.Rows[iRow].Cells[2].Value.ToString();
                txtGWBankCode.Text = dgView.Rows[iRow].Cells[3].Value.ToString();
                txtSIBSBankCode.Text = dgView.Rows[iRow].Cells[13].Value.ToString();
                DataSet dsGWBankCode = new DataSet();
                dsGWBankCode = objControlIBPS_Bank_Map.GetIBPS_BANK_MAP_BankName(txtGWBankCode.Text.Trim());
                if (dsGWBankCode.Tables[0].Rows.Count == 0)
                {
                    lblGWBankCode.Text = "";
                }
                else
                {
                    //lblSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["SIBS_BANK_CODE"].ToString();
                    lblGWBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["BANK_NAME"].ToString();
                }
                DataTable dsBranchName = new DataTable();
                dsBranchName = objControlBRANCH.GetData_Leave(txtSIBSBankCode.Text.Trim());
                if (dsBranchName.Rows.Count == 0)
                {
                    lblSIBSBankCode.Text = "";
                }
                else
                {
                    //lblSIBSBankCode.Text = dsBranchName.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                    lblSIBSBankCode.Text = dsBranchName.Rows[0]["BRAN_NAME"].ToString();
                }

                if (!string.IsNullOrEmpty(dgView.Rows[iRow].Cells[14].Value.ToString()))
                {
                    HVLVallow = Convert.ToInt32(dgView.Rows[iRow].Cells[14].Value.ToString());
                }
                else
                {
                    HVLVallow = 0;
                }

                if (HVLVallow == 1)
                {
                    chkHVLVAllow.Checked = true;
                }
                else
                {
                    chkHVLVAllow.Checked = false;
                }
                string connection = dgView.Rows[iRow].Cells["CONNECTION"].Value.ToString().Trim();
                if (connection == "")
                {
                    this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboConnection.Text = "";
                }
                else
                {
                    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboConnection.Text = connection;
                }

                string strArea = dgView.Rows[iRow].Cells["AREA"].Value.ToString().Trim();
                if (strArea == "")
                {
                    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboArea.Text = "";
                }
                else
                {
                    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboArea.Text = strArea;
                }

                cboConnection.Text = dgView.Rows[iRow].Cells["CONNECTION"].Value.ToString().Trim();
                txtDBLink.Text = dgView.Rows[iRow].Cells[20].Value.ToString();
                //cboFunction.Text = dgView.Rows[iRow].Cells[19].Value.ToString();
                cboFunction.Text = dgView.Rows[iRow].Cells["FUNCTION"].Value.ToString();
                
                //----------QUYND UPDATE----20081120--------------------------------------------------
                strID = dgView.Rows[iRow].Cells[0].Value.ToString();
                strCitadID = dgView.Rows[iRow].Cells[1].Value.ToString();                
                strExportPath = dgView.Rows[iRow].Cells[4].Value.ToString();
                strImportPath = dgView.Rows[iRow].Cells[5].Value.ToString();
                strFTPDirectory = dgView.Rows[iRow].Cells[6].Value.ToString();
                strUser1 = dgView.Rows[iRow].Cells[7].Value.ToString();
                strPassword = dgView.Rows[iRow].Cells[8].Value.ToString();
                strSBV_TAD = dgView.Rows[iRow].Cells[21].Value.ToString();
                strCCYCD = dgView.Rows[iRow].Cells[17].Value.ToString();
                strGWBankCode1 = dgView.Rows[iRow].Cells[3].Value.ToString();
                strSIBSBankCode = dgView.Rows[iRow].Cells[13].Value.ToString();
                strConnection = dgView.Rows[iRow].Cells["CONNECTION"].Value.ToString().Trim();
                strDBLink = dgView.Rows[iRow].Cells[20].Value.ToString();                
                strFunction = dgView.Rows[iRow].Cells["FUNCTION"].Value.ToString();
                //------------------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cboCCYCD.Enabled = false;
            txtDBLink.Enabled = false;
        }

        private void cboAreaView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chkHVLVAllow.Enabled = false;
                txtGWBankCode.Enabled = false;
                txtSIBSBankCode.Enabled = false;
                cboArea.Enabled = false;
                cboConnection.Enabled = false;
                txtAmount.Enabled = false;
                txtImportPath.Enabled = false;
                txtExportPath.Enabled = false;
                txtFTPDirectory.Enabled = false;
                txtUser.Enabled = false;
                txtPassword.Enabled = false;
                
                mskHour.Enabled = false;
                txtDBLink.Enabled = false;
                cboCitadStatus.Enabled = false;
                cboFunction.Enabled = false;
                cmdImportPath.Enabled = false;
                cmdExportPath.Enabled = false;
                //------------------------------
                CommandStatus(true);
                //cmdSave.Enabled = false;
                //cmdDelete.Enabled = true;
                //cmdEdit.Enabled = true;
                //cmdAdd.Enabled = true;
                if (cboAreaView.Text.Trim() == "ALL")
                {
                    LoadData();
                }
                else if (cboAreaView.Text.Trim() != "")
                {
                    Getdateta(cboAreaView.SelectedValue.ToString());
                    //LoadBankTotal();
                }
                cboCCYCD.Enabled = false;
                dgView.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Getdateta(string  strValue)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objControl.GetTAD_View(strValue);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    return;
                //}
                //else
                //{
                    FormatGridView(ds);
                //}
            }
            catch
            { 
            }
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("You must input a number!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
            
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text.Trim() == "")
            {
                cboCCYCD.Enabled = false;
            }
            else
            {
                cboCCYCD.Enabled = true;
            }
            
        }

        private void cboCCYCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadData == true)
            {
                return;
            }
            string strAmount = txtAmount.Text.Trim();
            if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {

                MessageBox.Show("You must input amount value!", Common.sCaption);
                txtAmount.Focus();
                return;
            }
            if (cboCCYCD.Text.Trim() == "VND")
            {
                txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), "{0:#,###}");
            }
            else
            {
                txtAmount.Text = Common.FormatCurrency(strAmount.Replace("\r", "").Replace("\r\n", ""), "{0:#,###.##}");
            }
        }

        private void FormatGridView(DataSet ds)
        {
            //SBV Tad ID
            dgView.DataSource = ds.Tables[0];            
            dgView.Columns[0].Visible = false;
            dgView.Columns[1].HeaderText = "Citad ID";
            dgView.Columns[1].Width = 70;
            dgView.Columns[2].HeaderText = "Tad name";
            dgView.Columns[2].Width = 250;
            dgView.Columns[3].HeaderText = "State bank ID";
            dgView.Columns[3].Width = 100;
            dgView.Columns[4].HeaderText = "Export path";
            dgView.Columns[4].Width = 150;
            dgView.Columns[5].HeaderText = "Import path";
            dgView.Columns[5].Width = 150;
            dgView.Columns[6].HeaderText = "FTP path";
            dgView.Columns[6].Width = 100;
            dgView.Columns[7].HeaderText = "FTP user";
            dgView.Columns[7].Width = 100;
            dgView.Columns[8].HeaderText = "FTP password";
            dgView.Columns[8].Width = 100;
            dgView.Columns[9].HeaderText = "Area";
            dgView.Columns[9].Width = 100;
            dgView.Columns[10].HeaderText = "Time";
            dgView.Columns[10].Width = 50;
            dgView.Columns[11].HeaderText = "Amount";
            dgView.Columns[11].Width = 100;
            dgView.Columns[12].HeaderText = "Status";
            dgView.Columns[12].Width = 70;
            dgView.Columns[13].HeaderText = "Receiver Branch";
            dgView.Columns[13].Width = 120;
            dgView.Columns[14].HeaderText = "Set low value";
            dgView.Columns[14].Width = 100;
            dgView.Columns[15].Visible = false;
            dgView.Columns[16].Visible = false;
            dgView.Columns[17].HeaderText = "Currency";
            dgView.Columns[17].Width = 80;
            dgView.Columns[18].HeaderText = "Connection";
            dgView.Columns[18].Width = 100;
            dgView.Columns[19].HeaderText = "Function";
            dgView.Columns[19].Width = 120;
            dgView.Columns[20].HeaderText = "DBLink";
            dgView.Columns[20].Width = 120;
            dgView.Columns[21].HeaderText = "SBV Tad ID";
            dgView.Columns[21].Width = 100;
            dgView.Columns["AMOUNT"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
            lblTotalTAD.Text = Convert.ToString(ds.Tables[0].Rows.Count);
            //----------------neu la update du lieu----------------------
            if (kCout == 1)
            {
                Select_ted();
            }
            //-----------------------------------------------------------
        }
        private void Select_ted()
        {
            int HVLVallow = 0;
            try
            {
                iID = Convert.ToInt32(dgView.Rows[Count1].Cells[0].Value.ToString());
                txtCitadID.Text = dgView.Rows[Count1].Cells[1].Value.ToString();
                //lblGWBankCode.Text = dgView.Rows[iRow].Cells[2].Value.ToString();
                //lblSIBSBankCode.Text = dgView.Rows[iRow].Cells[13].Value.ToString();
                //lblGWBankCode.Text = dgView.Rows[iRow].Cells[2].Value.ToString();
                lblSIBSBankCode.Text = dgView.Rows[Count1].Cells[2].Value.ToString();
                txtGWBankCode.Text = dgView.Rows[Count1].Cells[3].Value.ToString();
                txtExportPath.Text = dgView.Rows[Count1].Cells[4].Value.ToString();
                txtImportPath.Text = dgView.Rows[Count1].Cells[5].Value.ToString();
                txtFTPDirectory.Text = dgView.Rows[Count1].Cells[6].Value.ToString();
                txtUser.Text = dgView.Rows[Count1].Cells[7].Value.ToString();
                txtPassword.Text = dgView.Rows[Count1].Cells[8].Value.ToString();
                txtSBV_TAD.Text = dgView.Rows[Count1].Cells[21].Value.ToString();
                strSBV = dgView.Rows[Count1].Cells[21].Value.ToString();
                mskHour.Text = dgView.Rows[Count1].Cells[10].Value.ToString() + "";
                cboCCYCD.Text = dgView.Rows[Count1].Cells[17].Value.ToString();
                string strAAM = dgView.Rows[Count1].Cells[11].Value.ToString();
                //-----format tien-------------------------------------------------               
                //-----------------------------------------------------------------------------------------               
                if (string.IsNullOrEmpty(strAAM.Trim()))
                {
                    txtAmount.Text = "";
                }
                else
                {
                    txtAmount.Text = Common.FormatCurrency(strAAM.Trim(), Common.FORMAT_CURRENCY);
                }
                //cboCitadStatus.Text = dgView.Rows[iRow].Cells[13].Value.ToString();
                string CitadStatus = dgView.Rows[Count1].Cells[12].Value.ToString();
                cboCitadStatus.Text = CitadStatus;

                lblSIBSBankCode.Text = dgView.Rows[Count1].Cells[2].Value.ToString();
                txtGWBankCode.Text = dgView.Rows[Count1].Cells[3].Value.ToString();
                txtSIBSBankCode.Text = dgView.Rows[Count1].Cells[13].Value.ToString();
                DataSet dsGWBankCode = new DataSet();
                dsGWBankCode = objControlIBPS_Bank_Map.GetIBPS_BANK_MAP_BankName(txtGWBankCode.Text.Trim());
                if (dsGWBankCode.Tables[0].Rows.Count == 0)
                {
                    lblGWBankCode.Text = "";
                }
                else
                {
                    //lblSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["SIBS_BANK_CODE"].ToString();
                    lblGWBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["BANK_NAME"].ToString();
                }

                if (!string.IsNullOrEmpty(dgView.Rows[Count1].Cells[14].Value.ToString()))
                {
                    HVLVallow = Convert.ToInt32(dgView.Rows[Count1].Cells[14].Value.ToString());
                }
                else
                {
                    HVLVallow = 0;
                }

                if (HVLVallow == 1)
                {
                    chkHVLVAllow.Checked = true;
                }
                else
                {
                    chkHVLVAllow.Checked = false;
                }
                string connection = dgView.Rows[Count1].Cells["CONNECTION"].Value.ToString().Trim();
                if (connection == "")
                {
                    this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboConnection.Text = "";
                }
                else
                {
                    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboConnection.Text = connection;
                }

                string strArea = dgView.Rows[Count1].Cells["AREA"].Value.ToString().Trim();
                if (strArea == "")
                {
                    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboArea.Text = "";
                }
                else
                {
                    this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboArea.Text = strArea;
                }

                cboConnection.Text = dgView.Rows[Count1].Cells["CONNECTION"].Value.ToString().Trim();
                txtDBLink.Text = dgView.Rows[Count1].Cells[20].Value.ToString();
                cboFunction.Text = dgView.Rows[Count1].Cells["FUNCTION"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAmount.Text))
                txtAmount.Text = Common.FormatCurrency(txtAmount.Text.Trim(), Common.FORMAT_CURRENCY);
            else
                return;
        }

        private void mskHour_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sClose == "1" || AreaView == "1")
                {
                    sClose = "0"; AreaView = "0";
                }
                else
                {
                    if (!string.IsNullOrEmpty(mskHour.Text))
                    {
                        System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"[0-2][0-3]\:[0-5][0-9]");
                        string strAAS = mskHour.Text;
                        String[] N = strAAS.Split(new String[] { ":" }, StringSplitOptions.None);//cat chuoi
                        if (!String.IsNullOrEmpty(N[0].Trim())) //HaNTT10 sua ngay 08.10.2008
                        {

                            if (Convert.ToInt32(N[0]) > 23)
                            {
                                MessageBox.Show("Please provide the time in hh:mm format", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                mskHour.Focus();
                                mskHour.SelectAll();
                            }
                            else
                            {
                                if (Convert.ToInt32(N[1]) > 59)
                                {
                                    MessageBox.Show("Please provide the time in hh:mm format", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    mskHour.Focus();
                                    mskHour.SelectAll();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            //Common.ClearControl(this);
            lblGWBankCode.Text = "";
            lblSIBSBankCode.Text = "";
            LockTextBox(true);
            dgView.Enabled = true;
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            dgView.Focus();
            txtGWBankCode.Focus();
            cmdDelete.Enabled = true;
            cmdCancel.Enabled = false;
            cboAreaView.Enabled = true;
            CommandStatus(true);
        }
        //private void enterKey(object sender, KeyPressEventArgs e)
        //{
        //    ////khi nhan phim ESC
        //    //if (e.KeyChar == (char)27)
        //    //{
        //    //    frmIBPSBankMap_FormClosing(null, null);
        //    //    this.Close();
        //    //}
        //    //khi nhan phim Enter
        //    if (e.KeyChar == (char)13)
        //    {
        //        if (txtGWBankCode.Focused)
        //        {
        //            txtSIBSBankCode.Focus();
        //            txtSIBSBankCode.SelectAll();
        //        }
        //        else if (txtSIBSBankCode.Focused)
        //        {
        //            cboArea.Focus();
        //            cboArea.SelectAll();
        //        }
        //        else if (cboArea.Focused)
        //        {
        //            cboConnection.Focus();
        //            cboConnection.SelectAll();
        //        }
        //        else if (cboConnection.Focused)
        //        {
        //            cboFunction.Focus();
        //            cboFunction.SelectAll();
        //        }
        //        else if (cboFunction.Focused)
        //        {
        //            chkHVLVAllow.Focus();
        //           // txtSIBSBankCode.SelectAll();
        //        }
        //        else if (chkHVLVAllow.Focused)
        //        {
        //            cboCitadStatus.Focus();
        //            //cboCitadStatus(null, null);
        //        }
        //        else if (cboCitadStatus.Focused)
        //        {
        //            txtDBLink.Focus();
        //            txtDBLink.SelectAll();
        //        }
        //        else if (txtDBLink.Focused)
        //        {
        //            txtAmount.Focus();
        //            txtAmount.SelectAll();
        //        }
        //        else if (txtAmount.Focused)
        //        {
        //            cboCCYCD.Focus();
        //           // cmdAdd_Click(null, null);
        //        }
        //        else if (cboCCYCD.Focused)
        //        {
        //            mskHour.Focus();
        //            mskHour.SelectAll();
        //        }
        //        else if (mskHour.Focused)
        //        {
        //            txtImportPath.Focus();
        //            txtImportPath.SelectAll();
        //        }
        //        else if (txtImportPath.Focused)
        //        {
        //            txtExportPath.Focus();
        //            txtExportPath.SelectAll();
        //        }
        //        else if (txtExportPath.Focused)
        //        {
        //            txtFTPDirectory.Focus();
        //            txtFTPDirectory.SelectAll();
        //        }
        //        else if (txtFTPDirectory.Focused)
        //        {
        //            txtUser.Focus();
        //            txtUser.SelectAll();
        //        }
        //        else if (txtUser.Focused)
        //        {
        //            txtPassword.Focus();
        //            txtPassword.SelectAll();
        //        }
        //        //strSucess = true;
        //    }
        //}

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    //Goi su kien Click, nhung chua lam duoc :(
                    //(this.ActiveControl as Button)
                    //cmdLogin_Click
                    //if ((this.ActiveControl as Button).Name == "cmdSearch")
                    //{
                    //    cmdSearch_Click(null, null);
                    //}

                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

        private void txtSIBSBankCode_Leave(object sender, EventArgs e)
        {
            try
            {
                    if (cmdSave.Enabled == true)
                {
                    if (sClose == "1" || AreaView == "1" || Cancel == "1")
                    { sClose = "0"; Cancel = "0"; AreaView = "0"; }
                    else
                    {
                        if (String.IsNullOrEmpty(txtSIBSBankCode.Text.Trim()))
                        {
                            return;
                        }
                        else
                        {
                            DataSet dsSIBSBankCode = new DataSet();
                            string strSIBSBankCode;
                            if (txtSIBSBankCode.Text.Trim().Length == 5)
                            {
                                strSIBSBankCode = txtSIBSBankCode.Text.Trim().Substring(2, 3);
                            }
                            else
                            {
                                strSIBSBankCode = txtSIBSBankCode.Text.Trim();
                            }
                            dsSIBSBankCode = objControlBRANCH.GetBRANCH_MAP(strSIBSBankCode);
                            if (dsSIBSBankCode.Tables[0].Rows.Count == 0)
                            {
                                MessageBox.Show("Don't exist Receiver branch!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //txtGWBankCode.Focus();
                                txtSIBSBankCode.SelectAll();
                                cmdSave.Enabled = true;
                                cmdAdd.Enabled = false;
                                cmdEdit.Enabled = false;
                                cmdDelete.Enabled = false;
                                cmdCancel.Enabled = true;
                                txtSIBSBankCode.Text = REEEEE;
                                txtSIBSBankCode.Focus();
                                return;
                            }
                            else
                            {
                                //if (strtxtSIBSBankCode == txtSIBSBankCode.Text.Trim())
                                //{
                                //khong lam gi ca
                                lblSIBSBankCode.Text = dsSIBSBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                               
                            }
                        }
                    }
                   
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConnection.Text.Trim() == "DBLINK")
            {
                txtDBLink.Enabled = true;
            }
            else
            {
                txtDBLink.Enabled = false;
            }
        }

        private void txtGWBankCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sClose == "1" || AreaView == "1" || Cancel == "1")
                { sClose = "0"; Cancel = "0"; AreaView = "0"; }
                else
                {
                    //cmdCancel.TabStop = true;
                    //cmdClose.TabStop = true;
                    if (txtGWBankCode.Text.Length != 8)
                    {
                        MessageBox.Show("Invalid State Bank ID length!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtGWBankCode.Text.Trim()) || cmdSave.Enabled == false)
                        {
                            return;
                        }
                        else
                        {
                            DataSet dsGWBankCode = new DataSet();
                            dsGWBankCode = objControlIBPS_Bank_Map.GetIBPS_BANK_MAPStateBankIDName(txtGWBankCode.Text.Trim());
                            if (dsGWBankCode.Tables[0].Rows.Count == 0)
                            {
                                MessageBox.Show("State Bank ID does not exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cboArea.Text = "";
                                txtGWBankCode.SelectAll();
                                txtGWBankCode.Focus();
                                cmdSave.TabStop = false;
                                return;
                            }
                            else
                            {
                                //lblGWBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["bank_name"].ToString();
                                //if (dsGWBankCode.Tables[0].Rows[0]["citadid"].ToString().Length == 5)
                                //{
                                //    txtSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["citadid"].ToString().Substring(2, 3);
                                //}
                                //else
                                //{
                                //    txtSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["citadid"].ToString();
                                //}
                                //lblSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                                //txtCitadID.Text = "TAD" + dsGWBankCode.Tables[0].Rows[0]["sibs_bank_code"].ToString();
                            }
                            #region
                      
                            #endregion

                            dsArea_GWBankCode = objAREAController.GetAREA_GWBankCode(txtGWBankCode.Text.Trim());
                            if (dsArea_GWBankCode.Tables[0].Rows.Count == 0)
                            {
                                MessageBox.Show("Do not exist this area!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                cboArea.Text = "";
                                txtGWBankCode.Focus();
                                return;
                            }
                            else
                            {
                                cboArea.Text = dsArea_GWBankCode.Tables[0].Rows[0]["FULLNAME"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   
        private void cmdClose_MouseMove(object sender, MouseEventArgs e)
        {
            sClose = "1";
        }

        private void cboAreaView_MouseMove(object sender, MouseEventArgs e)
        {
            AreaView = "1";
        }

        private void cmdClose_MouseUp(object sender, MouseEventArgs e)
        {
            sClose = "0";           
        }

        private void cboAreaView_MouseUp(object sender, MouseEventArgs e)
        {
            AreaView = "0";
        }

        private void txtGWBankCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet dsGWBankCode = new DataSet();
                dsGWBankCode = objControlIBPS_Bank_Map.GetIBPS_BANK_MAPStateBankIDName(txtGWBankCode.Text.Trim());
                if (dsGWBankCode.Tables[0].Rows.Count == 0 || dsGWBankCode == null)
                {
                    lblGWBankCode.Text = "";
                    txtSIBSBankCode.Text = "";
                    lblSIBSBankCode.Text = "";
                    txtCitadID.Text = "";
                    REEEEE = "";
                }
                else
                {
                    
                    lblGWBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["bank_name"].ToString();
                    txtSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["citadid"].ToString();
                    lblSIBSBankCode.Text = dsGWBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                    //txtCitadID.Text = "TAD" + dsGWBankCode.Tables[0].Rows[0]["sibs_bank_code"].ToString();
                    REEEEE = txtSIBSBankCode.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmTADInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true & cmdSave.Enabled == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
                }
                else
                {

                }
            }
            catch
            {

            }
        }

        private void cmdCancel_MouseMove(object sender, MouseEventArgs e)
        {
            Cancel = "1";
        }

        private void txtSBV_TAD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtSBV_TAD.Text.Trim() == "")
                //{
                //    txtSBV_TAD.Text = "";
                //    strSBV_TAD = "";
                //}
                //else
                //{
                //    if (Regex.IsMatch(txtSBV_TAD.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                //    {
                //        strSBV_TAD = txtSBV_TAD.Text;
                //    }
                //    else
                //    {
                //        txtSBV_TAD.Text = strSBV_TAD;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCitadID_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtSBV_TAD_Leave(object sender, EventArgs e)
        {
            try
            {
                if (isSave == true)
                {
                    return;
                }
                else
                {
                    txtSBV_TAD.Text = clsCheck.ConvertVietnamese(txtSBV_TAD.Text);
                    if (sClose == "1" || AreaView == "1" || Cancel == "1")
                    { sClose = "0"; Cancel = "0"; AreaView = "0"; }
                    else
                    {
                        if (strSBV == txtSBV_TAD.Text.Trim())
                        {
                        }
                        else
                        {
                            DataTable datSBV = new DataTable();
                            string strWhere = "";
                            datSBV = objControl.Search_Sbv(txtSBV_TAD.Text.Trim(), txtGWBankCode.Text.Trim(), strWhere);
                            if (datSBV.Rows.Count != 0)
                            {
                                MessageBox.Show("SBV tad exists!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSBV_TAD.Focus();
                                CommandStatus(false);
                                return;
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDBLink_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDBLink.Text = clsCheck.ConvertVietnamese(txtDBLink.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtImportPath_Leave(object sender, EventArgs e)
        {
            try
            {
                txtImportPath.Text = clsCheck.ConvertVietnamese(txtImportPath.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtExportPath_Leave(object sender, EventArgs e)
        {
            try
            {
                txtExportPath.Text = clsCheck.ConvertVietnamese(txtExportPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFTPDirectory_Leave(object sender, EventArgs e)
        {
            try
            {
                txtFTPDirectory.Text = clsCheck.ConvertVietnamese(txtFTPDirectory.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Text = clsCheck.ConvertVietnamese(txtPassword.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iRow = e.RowIndex;
        }

        private void cmdHistory_Click(object sender, EventArgs e)
        {
            isTAD = true;
            frmView frmViewLogInfoTAD = new frmView();
            frmViewLogInfoTAD.ShowDialog();
        }

        private void dgView_DoubleClick(object sender, EventArgs e)
        {
            isTAD = true;
            frmView frmViewLogInfoTAD = new frmView();
            frmViewLogInfoTAD.ShowDialog();
        }

        private void txtSIBSBankCode_TextChanged(object sender, EventArgs e)
        {

        }      
    }
}
