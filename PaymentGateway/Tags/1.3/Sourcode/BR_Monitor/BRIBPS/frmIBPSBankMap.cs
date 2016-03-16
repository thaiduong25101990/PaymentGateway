using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRSYSTEM;
//using BR.DataAccess;
using BR.BRLib;
using BR.BRBusinessObject;
using System.Text.RegularExpressions;

namespace BR.BRIBPS
{
    public partial class frmIBPSBankMap : frmBasedata
    {
        /*---------------------------------------------------------------
         * Muc dich         : Quan ly danh sach ngan hang tham gia CITAD
         * Ngay tao         : 11/06/2008
         * Nguoi tao        : HaNTT10
         *--------------------------------------------------------------*/
        private bool isInsert = false;
        private DataSet datDs = new DataSet();
        DataSet dsSIBSBankCode = new DataSet();
        DataTable dtBranchOfBIDV = new DataTable();
        DataTable dtOtherBranch = new DataTable();

        #region dinh nghia cac ham trong lop BusinessObject
        private IBPS_BANK_MAPInfo objInfo = new IBPS_BANK_MAPInfo();
        private IBPS_BANK_MAPController objControl = new IBPS_BANK_MAPController();
        private BRANCHController objControlBRANCH = new BRANCHController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private SYSVARController objControlSYSVAR = new SYSVARController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        #endregion 

        #region dinh nghia cac bien
        private int iID = 0;
        clsCheckInput clsCheck = new clsCheckInput();
        public static bool isIBPSBankMap = false;
        public static bool isIBPSBankMapLog = false;
        private string sClose;
        private string Cancel;
        private bool isSave=false;
        private string strSIBSBankCode;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        //gia tri cu de ghi log--------------
        private string GWBankCode1 = "";
        private string SIBSBankCode1 = "";
        private string BankName1 = "";
        private string TellerID1 = "";
        private string Description1 = "";
        private string ShortBank1 = "";
        private int iRows;
        #endregion

        public frmIBPSBankMap()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {

                iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString().Trim());
                txtGWBankCode.Text = dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim();
                txtSIBSBankCode.Text = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                txtBankName.Text = dgView.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                txtTellerID.Text = dgView.CurrentRow.Cells["TELLERID"].Value.ToString().Trim();
                txtDescription.Text = dgView.CurrentRow.Cells["DESCRIPTIONS"].Value.ToString().Trim();
                txtShortBank.Text = dgView.CurrentRow.Cells["SHORT_NAME"].Value.ToString().Trim();
                //quynd Update 20081120----------------------------------------------------------------
                GWBankCode1 = dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim();
                SIBSBankCode1 = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                BankName1 = dgView.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                TellerID1 = dgView.CurrentRow.Cells["TELLERID"].Value.ToString().Trim();
                Description1 = dgView.CurrentRow.Cells["DESCRIPTIONS"].Value.ToString().Trim();
                ShortBank1 = dgView.CurrentRow.Cells["SHORT_NAME"].Value.ToString().Trim();                
                //-------------------------------------------------------------------------------------
                txtTellerID.Enabled = false;
                isInsert = false;
                LockTextBox(false);
                CommandStatus(false);
                txtGWBankCode.Focus();
                txtGWBankCode.SelectAll();
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
                iID = Convert.ToInt32(dgView.CurrentRow.Cells[0].Value.ToString());
                //quynd Update 20081120---------------------------------------------
                GWBankCode1 = dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim();
                SIBSBankCode1 = dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                BankName1 = dgView.CurrentRow.Cells["BANK_NAME"].Value.ToString().Trim();
                TellerID1 = dgView.CurrentRow.Cells["TELLERID"].Value.ToString().Trim();
                Description1 = dgView.CurrentRow.Cells["DESCRIPTIONS"].Value.ToString().Trim();
                ShortBank1 = dgView.CurrentRow.Cells["SHORT_NAME"].Value.ToString().Trim();
                //--------------------------------------------------------------------------
                if (Common.iSconfirm == 1)
                {
                    if (objControl.DeleteIBPS_BANK_MAP(iID) == 1)
                    {
                        MessageBox.Show("Delete successful!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        //-------------------------------------------------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "IBPS bank list";
                        int Log_level = 1;
                        string strWorked = "Delete";
                        string strTable = "IBPS_BANK_MAP";
                        string strOld_value = GWBankCode1 + "/" + SIBSBankCode1 + "/" + BankName1 + "/" + TellerID1 + "/" + Description1 + "/" + ShortBank1;
                        string strNew_value = "";
                        //---------------------------------------------------------------
                        Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            cboView_SelectedIndexChanged(null, null);
            CommandStatus(true);
           }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                isSave = true;
                objInfo.BANK_MAP_ID = iID;
                objInfo.GW_BANK_CODE = txtGWBankCode.Text;
                if (txtSIBSBankCode.Text.Trim().Length == 3)
                {
                    objInfo.SIBS_BANK_CODE = "00" + txtSIBSBankCode.Text.Trim();
                }
                else
                {
                    objInfo.SIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                }
                objInfo.BANK_NAME = txtBankName.Text;
                objInfo.TELLERID = txtTellerID.Text;
                objInfo.DESCRIPTIONS = txtDescription.Text;
                objInfo.SHORT_BANK_NAME = txtShortBank.Text;
                string pGW_BANK_CODE = txtGWBankCode.Text.Trim();
                string pSIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                if (Common.iSconfirm == 1)
                {
                    DataTable _dt = new DataTable();
                    _dt = objControlBRANCH.CHECK_BRANCH_MAP(pGW_BANK_CODE, pSIBS_BANK_CODE);
                    if (isInsert)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmdSave.Enabled = true;
                            return;
                        }
                    }
                    else 
                    {
                        if (_dt.Rows.Count > 1)
                        {
                            MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmdSave.Enabled = true;
                            return;
                        }
                    }

                }
                #region // Truong hop Insert
                if (isInsert)
                {
                    if (Common.iSconfirm == 1)
                    {
                        #region Kiem tra neu State Bank ID bang rong
                        if (string.IsNullOrEmpty(txtGWBankCode.Text.Trim()))
                        {
                            MessageBox.Show("You must input State Bank ID !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtGWBankCode.Focus();
                            CommandStatus(false);
                            //cmdSave.Enabled = true;
                            //btnCancel.Enabled = true;
                            return;
                        }
                        #endregion

                        if (string.IsNullOrEmpty(txtSIBSBankCode.Text.Trim()))
                        {
                            MessageBox.Show("You must input Branch!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSIBSBankCode.Focus();
                            CommandStatus(false);
                            return;
                        }
                        else if (txtSIBSBankCode.Text.ToString() != "-1")
                        {
                            if (!CheckIBPSBankLength())
                            {
                                txtGWBankCode.Text = "";
                                txtGWBankCode.Focus();
                                CommandStatus(false);
                                return;
                            }

                            #region //Kiem tra xem do co phai la chi nhanh cua BIDV hay khong

                            string strWhere = " and ltrim(ibm.sibs_bank_code,'0')=bran.sibs_bank_code(+) and trim(ibm.gw_bank_code)='" + txtGWBankCode.Text.Trim() + "' ";
                            //dtBranchOfBIDV = objControl.SearchViewIBPS_BANK_MAP(strWhere, "<>", "=");
                            dtBranchOfBIDV = objControlBRANCH.BRANCH_OtherBranch("1", txtSIBSBankCode.Text.Trim());
                            if (dtBranchOfBIDV.Rows.Count > 0)
                            {
                                txtTellerID.Text = "FPTIBPS" + txtSIBSBankCode.Text.Trim();
                                objInfo.TELLERID = txtTellerID.Text;
                            }
                            else
                            {
                                //Kiem tra xem do co phai la chi nhanh cua BIDV hay khong

                                string strOtherBranch = " and ltrim(ibm.sibs_bank_code,'0')=bran.sibs_bank_code(+) and trim(ibm.gw_bank_code)='" + txtGWBankCode.Text.Trim() + "' ";
                                dtOtherBranch = objControlBRANCH.BRANCH_OtherBranch("2", txtSIBSBankCode.Text.Trim());
                                if (dtOtherBranch.Rows.Count > 0)
                                {
                                    MessageBox.Show("This is not branch of MSB!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtBankName.Text = "";
                                    CommandStatus(false);
                                    txtSIBSBankCode.SelectAll();
                                    txtSIBSBankCode.Focus();
                                    return;
                                }
                                ////txtTellerID.Text = "FPTIBPS000";
                                //txtTellerID.Text = "FPTIBPS" + strSIBSBankCode;
                            }

                            #endregion

                            dsSIBSBankCode = objControlBRANCH.GetBRANCH_MAP(txtSIBSBankCode.Text.Trim());
                            if (dsSIBSBankCode.Tables[0].Rows.Count == 0)
                            {
                                MessageBox.Show("Branch doesn't exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSIBSBankCode.SelectAll();
                                txtSIBSBankCode.Focus();
                                CommandStatus(false);
                                return;
                            }
                            else
                            {

                                if (string.IsNullOrEmpty(objInfo.BANK_NAME.ToString().Trim()))
                                {
                                    txtBankName.Text = dsSIBSBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                                    objInfo.BANK_NAME = txtBankName.Text;
                                }
                                else
                                {
                                }
                            }

                            //if ((GetData.IDIsExisting(false, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), "")) ||
                            //       (GetData.IDIsExisting(false, "ibps_bank_map", "LPAD(SIBS_BANK_CODE,5,'0')", txtSIBSBankCode.Text.Trim().PadLeft(5, '0'), "")))
                            //{
                            //    MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtBankName.Text = "";
                            //    txtTellerID.Text = "";
                            //    //txtSIBSBankCode.Text = "";
                            //    txtSIBSBankCode.Focus();
                            //    CommandStatus(false);
                            //    return;
                            //}
                            //}



                            int intReturn = objControl.CheckData(objInfo.SIBS_BANK_CODE);
                            if (intReturn == 0)
                            {
                                MessageBox.Show("Invalid Branch!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                txtGWBankCode.Focus();
                                CommandStatus(false);
                                return;
                            }

                            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                            {
                                MessageBox.Show("You must input Bank Name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtBankName.Focus();
                                CommandStatus(false);
                                return;
                            }
                            //CheckExistData();
                            objControl.AddIBPS_BANK_MAP(objInfo);
                            MessageBox.Show("Save data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            //-------------------------------------------------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "IBPS bank list" + objInfo.GW_BANK_CODE;//+ objInfo.SIBS_BANK_CODE; 
                            int Log_level = 1;
                            string strWorked = "Insert";
                            string strTable = "IBPS_BANK_MAP";
                            string strOld_value = "";
                            string strNew_value = objInfo.GW_BANK_CODE + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.BANK_NAME + "/" + objInfo.TELLERID + "/" + objInfo.DESCRIPTIONS + "/" + objInfo.SHORT_BANK_NAME;
                            //---------------------------------------------------------------
                            Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            CommandStatus(true);
                        }
                        else
                        {
                            if ((GetData.IDIsExisting(false, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), "")) &&
                                   (GetData.IDIsExisting(false, "ibps_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), "")))
                            {
                                MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSIBSBankCode.Text = "";
                                txtSIBSBankCode.Focus();
                                CommandStatus(false);
                                return;
                            }
                            else // neu chua ton tai ban ghi 
                            {
                                txtTellerID.Text = "FPTIBPS000";
                                //txtBankName.Text = "";
                                //return;

                            }
                            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                            {
                                MessageBox.Show("You must input Bank Name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtBankName.Focus();
                                CommandStatus(false);
                                return;
                            }
                            int intInsert = objControl.AddIBPS_BANK_MAP(objInfo);
                            if (intInsert == 1)
                            {
                                MessageBox.Show("Save data successfully!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                CommandStatus(true);
                                //-------------------------------------------------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "IBPS bank list" + objInfo.GW_BANK_CODE;// +objInfo.SIBS_BANK_CODE; 
                                int Log_level = 1;
                                string strWorked = "Insert";
                                string strTable = "IBPS_BANK_MAP";
                                string strOld_value = "";
                                string strNew_value = objInfo.GW_BANK_CODE + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.BANK_NAME + "/" + objInfo.TELLERID + "/" + objInfo.DESCRIPTIONS + "/" + objInfo.SHORT_BANK_NAME;
                                //---------------------------------------------------------------
                                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            }
                            else
                            {
                                MessageBox.Show("Insert failed. Please check again!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CommandStatus(false);
                                return;
                            }
                        }
                    }
                    else
                    {
                        CommandStatus(true);
                        return;
                    }
                }//end if 
                #endregion

                #region // Neu la update
                else
                {
                    if (Common.iSconfirm == 1)
                    {
                        //CheckValidData();
                        //if (!CheckValidData())
                        //{
                        //    return;
                        //}
                        if (string.IsNullOrEmpty(txtGWBankCode.Text.Trim()))
                        {
                            MessageBox.Show("You must input State Bank ID !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtGWBankCode.Focus();
                            CommandStatus(false);
                            //cmdSave.Enabled = true;
                            //btnCancel.Enabled = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(txtSIBSBankCode.Text.Trim()))
                        {
                            MessageBox.Show("You must input Branch!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSIBSBankCode.Focus();
                            CommandStatus(false);
                            //cmdSave.Enabled = true;
                            //btnCancel.Enabled = true;
                            return;
                        }
                        else if (txtSIBSBankCode.Text.ToString() != "-1")
                        {
                            //ham kiem tra Branch do da ton tai trong bang Branch chua
                            dsSIBSBankCode = objControlBRANCH.GetBRANCH_MAP(txtSIBSBankCode.Text.Trim());
                            if (dsSIBSBankCode.Tables[0].Rows.Count == 0)
                            {
                                MessageBox.Show("Branch doesn't exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSIBSBankCode.SelectAll();
                                txtSIBSBankCode.Focus();
                                CommandStatus(false);
                                return;
                            }
                            else
                            {
                                //txtBankName.Text = dsSIBSBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                                objInfo.BANK_NAME = txtBankName.Text;
                            }

                            #region //Kiem tra xem do co phai la chi nhanh cua BIDV hay khong

                            string strWhere = " and ltrim(ibm.sibs_bank_code,'0')=bran.sibs_bank_code(+) and trim(ibm.gw_bank_code)='" + txtGWBankCode.Text.Trim() + "' ";
                            //dtBranchOfBIDV = objControl.SearchViewIBPS_BANK_MAP(strWhere, "<>", "=");
                            dtBranchOfBIDV = objControlBRANCH.BRANCH_OtherBranch("1", txtSIBSBankCode.Text.Trim());
                            if (dtBranchOfBIDV.Rows.Count > 0)
                            {
                                //txtTellerID.Text = "FPTIBPS" + txtSIBSBankCode.Text.Trim();
                                objInfo.TELLERID = txtTellerID.Text;
                            }
                            else
                            {
                                //Kiem tra xem do co phai la chi nhanh cua BIDV hay khong

                                string strOtherBranch = " and ltrim(ibm.sibs_bank_code,'0')=bran.sibs_bank_code(+) and trim(ibm.gw_bank_code)='" + txtGWBankCode.Text.Trim() + "' ";
                                dtOtherBranch = objControlBRANCH.BRANCH_OtherBranch("2", txtSIBSBankCode.Text.Trim());
                                if (dtOtherBranch.Rows.Count > 0)
                                {
                                    MessageBox.Show("This is not branch of MSB!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    CommandStatus(false);
                                    txtSIBSBankCode.SelectAll();
                                    txtSIBSBankCode.Focus();
                                    return;
                                }
                                ////txtTellerID.Text = "FPTIBPS000";
                                //txtTellerID.Text = "FPTIBPS" + strSIBSBankCode;
                            }

                            #endregion
                            //if ((GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim())) ||
                            //       (GetData.IDIsExisting(true, "ibps_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim().PadLeft(5, '0'), dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim().PadLeft(5, '0'))))
                            //{
                            //    MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtSIBSBankCode.Text = "";
                            //    txtSIBSBankCode.Focus();
                            //    CommandStatus(false);
                            //    return;
                            //}
                            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                            {
                                MessageBox.Show("You must input Bank Name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtBankName.Focus();
                                CommandStatus(false);
                                //cmdSave.Enabled = true;
                                //btnCancel.Enabled = true;
                                return;
                            }


                            int intValue = objControl.UpdateIBPS_BANK_MAP(objInfo);
                            if (intValue > 0)
                            {
                                MessageBox.Show("Update data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CommandStatus(true);
                                //-------------------------------------------------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "IBPS bank list" + objInfo.GW_BANK_CODE;//+ objInfo.SIBS_BANK_CODE; 
                                int Log_level = 1;
                                string strWorked = "Update";
                                string strTable = "IBPS_BANK_MAP";
                                string strOld_value = GWBankCode1 + "/" + SIBSBankCode1 + "/" + BankName1 + "/" + TellerID1 + "/" + Description1 + "/" + ShortBank1;
                                string strNew_value = objInfo.GW_BANK_CODE + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.BANK_NAME + "/" + objInfo.TELLERID + "/" + objInfo.DESCRIPTIONS + "/" + objInfo.SHORT_BANK_NAME;
                                //---------------------------------------------------------------
                                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            }
                            else if (intValue == 0)
                            {
                                MessageBox.Show("Duplicate data. Please check again!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CommandStatus(false);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Database Connection Errors", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CommandStatus(false);
                                return;
                            }
                        }
                        else
                        {//Neu == -1
                            if (GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim()))
                            {
                                MessageBox.Show("State bank ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtGWBankCode.Text = "";
                                txtGWBankCode.Focus();
                                CommandStatus(false);
                                return;
                            }
                            //if ((GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim())) &&
                            //        (GetData.IDIsExisting(true, "ibps_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim().PadLeft(5, '0'))))
                            //{
                            //    MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtSIBSBankCode.SelectAll();
                            //    txtSIBSBankCode.Focus();
                            //    CommandStatus(false);
                            //    //cmdAdd.Enabled = false;
                            //    //cmdEdit.Enabled = false;
                            //    //cmdSave.Enabled = true;
                            //    //btnCancel.Enabled = true;
                            //    //cmdDelete.Enabled = false;
                            //    return;
                            //}
                            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                            {
                                MessageBox.Show("You must input Bank Name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtBankName.Focus();
                                CommandStatus(false);
                                //cmdSave.Enabled = true;
                                //btnCancel.Enabled = true;
                                return;
                            }
                            int intValue = objControl.UpdateIBPS_BANK_MAP(objInfo);
                            if (intValue > 0)
                            {
                                MessageBox.Show("Update data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CommandStatus(true);
                                //-------------------------------------------------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "IBPS bank list" + objInfo.GW_BANK_CODE;//+ objInfo.SIBS_BANK_CODE; 
                                int Log_level = 1;
                                string strWorked = "Update";
                                string strTable = "IBPS_BANK_MAP";
                                string strOld_value = GWBankCode1 + "/" + SIBSBankCode1 + "/" + BankName1 + "/" + TellerID1 + "/" + Description1 + "/" + ShortBank1;
                                string strNew_value = objInfo.GW_BANK_CODE + "/" + objInfo.SIBS_BANK_CODE + "/" + objInfo.BANK_NAME + "/" + objInfo.TELLERID + "/" + objInfo.DESCRIPTIONS + "/" + objInfo.SHORT_BANK_NAME;
                                //---------------------------------------------------------------
                                Ghiloguser(dtLog, strUser, strConten, Log_level, strWorked, strTable, strOld_value, strNew_value);
                            }
                            else if (intValue == 0)
                            {
                                MessageBox.Show("Duplicate data. Please check again!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CommandStatus(false);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Database Connection Errors", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CommandStatus(false);
                                return;
                            }
                        }//END ELSE
                    }
                }
                #endregion

                LoadData();
                CommandStatus(true);
                txtTellerID.Enabled = true;
                txtGWBankCode.Enabled = true;
                txtGWBankCode.Focus();
                cboView_SelectedIndexChanged(null, null);
                ClearText();
                isSave = false;

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                isInsert = true;
                LockTextBox(false);
                ClearText();
                txtGWBankCode.Focus();
                CommandStatus(false);
                txtTellerID.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void frmIBPSBankMap_Load(object sender, EventArgs e) 
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                LoadData();
                LockTextBox(false);
                CommandStatus(true);
                cboView.DataSource = objAllcode.GetALLCODE("BankView", "SYSTEM");
                cboView.DisplayMember = "CONTENT";
                cboView.ValueMember = "CDVAL";
                ClearText();
                txtGWBankCode.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
         }

        private void CommandStatus(bool a)
        {
            cmdAdd.Enabled = a;
            cmdEdit.Enabled = a;
            cmdSave.Enabled = !a;
            btnCancel.Enabled = !a;
            cmdDelete.Enabled = a;
            cmdSearch.Enabled = a;
            cboView.Enabled = a;
            dgView.Enabled = a;
        }

        private void ColumsHeader(DataGridView Datagrid)
        {
            try
            {

                int g = 1;
                while (g < Datagrid.Columns.Count)
                {
                    string strColumns = Datagrid.Columns[g].HeaderText.ToString();
                    if (strColumns.Trim() != "GW_BANK_CODE" && strColumns.Trim() != "SIBS_BANK_CODE"
                     && strColumns.Trim() != "SHORT_NAME" && strColumns.Trim() != "TELLERID")
                    {
                        Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    }
                    else
                    {
                        if (strColumns.Trim() == "GW_BANK_CODE")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "STATE BANK ID";
                        }
                        else if (strColumns.Trim() == "SIBS_BANK_CODE")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "BRANCH";
                        }
                        else if (strColumns.Trim() == "SHORT_NAME")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "SHORT BANK NAME";
                        }
                        else if (strColumns.Trim() == "TELLERID")
                        {
                            Datagrid.Columns[g].HeaderCell.Value = "TELLER ID";
                        }
                    }
                     Datagrid.ColumnHeadersHeight = 21;
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadData()
        {
            DataSet datDs = new DataSet();
            datDs = objControl.GetIBPS_BANK_MAP();
            
            if (datDs == null && datDs.Tables[0].Rows.Count == 0)
            {
                 return;
            }
            else
            {
                dgView.DataSource = datDs.Tables[0];
                dgView.Columns["BANK_MAP_ID"].Visible = false;
            
                lblTong.Text = Convert.ToString(datDs.Tables[0].Rows.Count);
            }
            ColumsHeader(dgView);
            DataGridColumnsSize();
        }
        private void DataGridColumnsSize()
        {
          
            dgView.Columns[0].Visible = false;
            dgView.Columns[1].Width = 120;
            dgView.Columns[2].Width = 120;
            dgView.Columns[3].Width = 220;
            dgView.Columns[4].Width = 200;
            dgView.Columns[5].Width = 240;
            dgView.Columns[6].Width = 100;
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

        private void LoadBankTotalBranchOfBIDV()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objControl.GetIBPS_BANK_MAPTotalBank("<>");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    lblTong.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadBankTotalOtherBank()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objControl.GetIBPS_BANK_MAPTotalBank("=");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    lblTong.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private bool CheckIBPSBankLength()
        {
            bool result = true;
            string ID = txtGWBankCode.Text.Trim();
            string strLength = "IBPSBankLength";
            DataSet ds = new DataSet();
            ds = objControlSYSVAR.GetIBPSBankLength(strLength);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                int length = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (ID.Length != length)
                {
                    //ID = "The length of GW Bank Code is " + length ;
                    MessageBox.Show("State bank ID's length must be " + length + " numbers!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    result = false;
                }
            }
             CommandStatus(false);
            txtGWBankCode.Focus();
            return result;
        }

        private bool CheckValidData()
        {
            try
            {
                bool iResult = true;
                if (string.IsNullOrEmpty(txtGWBankCode.Text.Trim()))
                {
                    MessageBox.Show("You must input State Bank ID !", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGWBankCode.Focus();
                    CommandStatus(false);
                    return false;
                }
                if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                {
                    MessageBox.Show("You must input Bank Name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBankName.Focus();
                    CommandStatus(false);
                    return false;
                }
                if (txtSIBSBankCode.Text.ToString().Length > 8)
                {
                    MessageBox.Show("The max length of State bank ID is 12 and Branch is 5", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGWBankCode.Focus();
                    CommandStatus(false);
                    return false;
                }
                return iResult;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return false;
            }
        }


        private void LockTextBox(Boolean a)
        {
            txtGWBankCode.ReadOnly = a;
            txtSIBSBankCode.ReadOnly = a;
            txtBankName.ReadOnly = a;
            txtTellerID.ReadOnly = a;
            txtDescription.ReadOnly = a;
            txtShortBank.ReadOnly = a; 
        }

        private void ClearText()
        {
            txtGWBankCode.Text = "";
            txtSIBSBankCode.Text = "";
            txtBankName.Text = "";
            txtTellerID.Text = "";
            txtDescription.Text = "";
            txtShortBank.Text ="";  
        }
       
        private void cboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboView.Text.Trim() == "Branch of MSB")
                {
                    DataTable dtBranchOfBIDV = new DataTable();
                    string strWhere = " and lpad(ibm.sibs_bank_code,5,'0')=lpad(bran.sibs_bank_code,5,'0') ";
                    dtBranchOfBIDV = objControl.SearchViewIBPS_BANK_MAP(strWhere, "<>", "=");
                    dgView.DataSource = dtBranchOfBIDV;
                    lblTong.Text = dtBranchOfBIDV.Rows.Count.ToString();
                }
                else if (cboView.Text.Trim() == "ALL")
                {
                    LoadData();
                }
                else if (cboView.Text.Trim() == "Other Bank")
                {
                    DataTable dtOtherBranch = new DataTable();
                    dtOtherBranch = objControl.SearchViewIBPS_BANK_MAP("", "=", "=");
                    dgView.DataSource = dtOtherBranch;
                    lblTong.Text = dtOtherBranch.Rows.Count.ToString();

                }
                CommandStatus(true);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strStateBankCode; string strSIBSBankCode; string strBankName; string strTellerID; string strDescription; string strShortName;
                if (txtGWBankCode.Text == "") { strStateBankCode = ""; } else { strStateBankCode = " and  upper(trim(ibm.GW_BANK_CODE)) like '%" + txtGWBankCode.Text.Trim().ToUpper() + "%'"; }
                if (txtSIBSBankCode.Text == "") { strSIBSBankCode = ""; } else { strSIBSBankCode = " and  upper(trim(ibm.SIBS_BANK_CODE)) like '%" + txtSIBSBankCode.Text.Trim().ToUpper() + "%'"; }
                if (txtBankName.Text == "") { strBankName = ""; } else { strBankName = " and  upper(trim(ibm.BANK_NAME)) like '%" + txtBankName.Text.Trim().ToUpper() + "%'"; }
                if (txtTellerID.Text == "") { strTellerID = ""; } else { strTellerID = " and  upper(trim(ibm.TELLERID)) like '%" + txtTellerID.Text.Trim().ToUpper() + "%'"; }
                if (txtDescription.Text == "") { strDescription = ""; } else { strDescription = " and  upper(trim(ibm.DESCRIPTIONS)) like '%" + txtDescription.Text.Trim().ToUpper() + "%'"; }
                if (txtShortBank.Text == "") { strShortName = ""; } else { strShortName = " and  upper(trim(ibm.SHORT_NAME)) like '%" + txtShortBank.Text.Trim().ToUpper() + "%'"; }

                string strWHERE = strStateBankCode + strSIBSBankCode + strBankName + strTellerID + strDescription + strShortName;
                string strWhere1 = "";
                if (strWHERE == "")
                {
                    strWhere1 = strWHERE;
                }
                else
                {
                    strWhere1 = " Where " + strWHERE.Substring(5);
                }
                DataSet datSearch = new DataSet();
                datSearch = objControl.GetIBPS_BANK_MAPSearch(strWhere1);


                if (datSearch == null)
                {
                    dgView.DataSource = 0;
                    lblTong.Text = "0";
                    return;
                }
                //FormatGridView();
                
                dgView.DataSource = datSearch.Tables[0];
                lblTong.Text = Convert.ToString(datSearch.Tables[0].Rows.Count);
                if (dgView.RowCount > 0)
                {
                    CommandStatus(true);
                    ColumsHeader(dgView);
                    DataGridColumnsSize();
                }
                else
                {
                    CommandStatus(true);
                    ColumsHeader(dgView);
                    DataGridColumnsSize();
                    txtGWBankCode.Enabled = true;
                    MessageBox.Show("There is no suitable message!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void FormatGridView()
        {
            try
            {
                dgView.RowHeadersVisible = true;
                dgView.Columns[0].Visible = false;
                dgView.Columns[1].HeaderText = "State bank ID";
                dgView.Columns[1].Width = 100;
                dgView.Columns[2].HeaderText = "Branch";
                dgView.Columns[2].Width = 100;
                dgView.Columns[3].HeaderText = "Bank name";
                dgView.Columns[3].Width = 220;
                dgView.Columns[4].HeaderText = "Teller ID";
                dgView.Columns[4].Width = 100;
                dgView.Columns[5].HeaderText = "Description";
                dgView.Columns[5].Width = 240;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void txtSIBSBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F5)
                {
                    isIBPSBankMap = true;
                    frmView frmview = new frmView();
                    //this.Hide();
                    frmview.ShowDialog(this);
                    txtBankName.Text = frmview.objInfoBRANCH.BRAN_NAME;
                    txtSIBSBankCode.Text = frmview.objInfoBRANCH.SIBS_BANK_CODE;
                    //this.Show();
                    isIBPSBankMap = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
     

        private void frmIBPSBankMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               
                if (cmdSave.Enabled)
                    e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.ClearControl(this);
            //CommandStatus(true);
            dgView.Enabled = true;
            cboView.Enabled = true;
            cmdAdd.Enabled = true;
            if (dgView.RowCount >0)
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
            }  
            
            cmdSave.Enabled = false;
            txtGWBankCode.Focus();
            btnCancel.Enabled = false;
            cmdSearch.Enabled = true;
            cboView.Enabled = true;
            txtTellerID.Enabled = true;
            txtGWBankCode.Enabled = true;
        }

        private void txtGWBankCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strTXTX = "";
                if (Regex.IsMatch(txtGWBankCode.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                {//strTXTX
                    if (txtGWBankCode.Text.Trim().Length <= 12)
                    { strTXTX = txtGWBankCode.Text.Trim(); }
                    else
                    { txtGWBankCode.Text = strTXTX; }
                }
                else
                { txtGWBankCode.Text = ""; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        
        private void txtGWBankCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sClose == "1" || Cancel == "1")
                {
                    sClose = "0"; Cancel = "0";
                }
                else
                {
                    if (cmdSearch.Enabled == true)
                    { return; }
                    else
                    {
                        if (String.IsNullOrEmpty(txtGWBankCode.Text.Trim()))
                        { return; }
                        else
                        {
                            if (txtGWBankCode.Text.Trim().Length != 8)
                            {
                                MessageBox.Show("Invalid State bank ID length!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtGWBankCode.SelectAll();
                                txtGWBankCode.Focus();
                                return;
                            }
                            else
                            {
                                if (txtSIBSBankCode.Text.Trim() == "-1")
                                {
                                    if (isInsert == true)
                                    {
                                        if ((GetData.IDIsExisting(false, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), "")))
                                        {
                                            MessageBox.Show("State bank ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtGWBankCode.Text = "";
                                            txtGWBankCode.Focus();
                                            CommandStatus(false);
                                            return;
                                        }
                                    }
                                    else
                                    {

                                        if ((GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim())))
                                        {
                                            MessageBox.Show("State bank ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtGWBankCode.Text = "";
                                            txtGWBankCode.Focus();
                                            CommandStatus(false);
                                            return;
                                        }
                                    }
                                }
                                    /*Quynd update theo yeu cau cua msb*/
                                else if (txtSIBSBankCode.Text.Trim() != "-1" || txtSIBSBankCode.Text.Trim() != "-1")
                                {
                                    string pGW_BANK_CODE = "";
                                    string pSIBS_BANK_CODE = "";
                                    DataTable _dt = new DataTable();
                                    pGW_BANK_CODE = txtGWBankCode.Text.Trim();
                                    pSIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                                    _dt = objControlBRANCH.CHECK_BRANCH_MAP(pGW_BANK_CODE, pSIBS_BANK_CODE);
                                    //if (_dt.Rows.Count > 0)
                                    //{
                                    //    MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //    txtSIBSBankCode.Text = "";
                                    //    txtSIBSBankCode.Focus();
                                    //    CommandStatus(false);
                                    //    return;
                                    //}
                                }

                                #region Update theo yeu cau cua msb(Quynd comment)
                                //if (isInsert == true)
                                //{
                                //    if ((GetData.IDIsExisting(false, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), "")))
                                //    {
                                //        MessageBox.Show("State bank ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //        txtGWBankCode.Text = "";
                                //        txtGWBankCode.Focus();
                                //        CommandStatus(false);
                                //        return;
                                //    }
                                //}
                                //else
                                //{

                                //    if ((GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim())))
                                //    {
                                //        MessageBox.Show("State bank ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //        txtGWBankCode.Text = "";
                                //        txtGWBankCode.Focus();
                                //        CommandStatus(false);
                                //        return;
                                //    }
                                //}
                                #endregion
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtSIBSBankCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sClose == "1" || Cancel == "1")
                {
                    sClose = "0"; Cancel = "0";
                }
                else
                {
                    if (cmdSearch.Enabled == true)
                    { return; }
                    else
                    {
                        if (string.IsNullOrEmpty(txtSIBSBankCode.Text.Trim()))
                        { return; }
                        else
                        {
                            if (isSave == true)
                            { return; }
                            else
                            {
                                if (txtSIBSBankCode.Text.Trim() == "-1")
                                {
                                    #region Neu la Insert
                                    if (isInsert == true)
                                    {
                                        if ((GetData.IDIsExisting(false, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), "")) &&
                                           (GetData.IDIsExisting(false, "ibps_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), "")))
                                        {
                                            MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtSIBSBankCode.Text = "";
                                            txtSIBSBankCode.Focus();
                                            CommandStatus(false);
                                            return;
                                        }
                                        else // neu chua ton tai ban ghi 
                                        {
                                            txtTellerID.Text = "FPTIBPS000";
                                            txtBankName.Text = "";
                                            return;

                                        }
                                    }
                                    #endregion

                                    #region neu la Update
                                    else // neu la Update
                                    {
                                        if ((GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim())) &&
                                           (GetData.IDIsExisting(true, "ibps_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim(), dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim())))
                                        {
                                            MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtSIBSBankCode.SelectAll();
                                            txtSIBSBankCode.Focus();
                                            CommandStatus(false);

                                            return;
                                        }
                                    }
                                    #endregion
                                }

                                #region Truong hop txtSIBSBankCode.Text khac '-1'
                                else //neu txtSIBSBankCode != '-1'
                                {
                                    if (txtSIBSBankCode.Text.Trim().Length == 5)
                                    {
                                        strSIBSBankCode = txtSIBSBankCode.Text.Trim().Substring(2, 3);
                                    }
                                    else
                                    {
                                        strSIBSBankCode = txtSIBSBankCode.Text.Trim();
                                    }
                                    //ham kiem tra Branch do da ton tai trong bang Branch chua
                                    dsSIBSBankCode = objControlBRANCH.GetBRANCH_MAP(strSIBSBankCode);
                                    if (dsSIBSBankCode.Tables[0].Rows.Count == 0)
                                    {
                                        //MessageBox.Show("Branch doesn't exist!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //txtSIBSBankCode.SelectAll();
                                        //txtSIBSBankCode.Focus();
                                        //CommandStatus(false);
                                        //return;
                                    }
                                    else //ham kiem tra Branch do da ton tai trong bang Branch
                                    {
                                        string pGW_BANK_CODE = "";
                                        string pSIBS_BANK_CODE = "";
                                        DataTable _dt = new DataTable();
                                        pGW_BANK_CODE = txtGWBankCode.Text.Trim();
                                        pSIBS_BANK_CODE = txtSIBSBankCode.Text.Trim();
                                        #region Truong hop Insert
                                        if (isInsert == true)
                                        {
                                            #region Quynd comment lam theo yeu cau msb
                                            //if ((GetData.IDIsExisting(false, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), "")) ||
                                            //   (GetData.IDIsExisting(false, "ibps_bank_map", "LPAD(SIBS_BANK_CODE,5,'0')", txtSIBSBankCode.Text.Trim().PadLeft(5, '0'), "")))
                                            //{
                                            //    MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //    txtSIBSBankCode.Text = "";
                                            //    txtSIBSBankCode.Focus();
                                            //    CommandStatus(false);
                                            //    return;
                                            //}
                                            //else //neu chua bi trung ban ghi trong t.hop Insert
                                            //{
                                            //    if (dsSIBSBankCode.Tables[0].Rows[0]["BRAN_TYPE"].ToString() == "1")
                                            //    {
                                            //        txtBankName.Text = dsSIBSBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                                            //    }
                                            //    else
                                            //    {
                                            //        txtBankName.Text = "";
                                            //    }
                                            //}
                                            #endregion
                                            _dt = objControlBRANCH.CHECK_BRANCH_MAP(pGW_BANK_CODE, pSIBS_BANK_CODE);
                                            if (_dt.Rows.Count > 0)
                                            {
                                                MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtSIBSBankCode.Text = "";
                                                txtSIBSBankCode.Focus();
                                                CommandStatus(false);
                                                return;
                                            }
                                        }
                                        #endregion

                                        #region Truong hop Update
                                        else // neu la Update
                                        {
                                            #region Quynd comment update theo yeu cau cua msb
                                            //if ((GetData.IDIsExisting(true, "ibps_bank_map", "GW_BANK_CODE", txtGWBankCode.Text.Trim(), dgView.CurrentRow.Cells["GW_BANK_CODE"].Value.ToString().Trim())) ||
                                            //   (GetData.IDIsExisting(true, "ibps_bank_map", "SIBS_BANK_CODE", txtSIBSBankCode.Text.Trim().PadLeft(5, '0'), dgView.CurrentRow.Cells["SIBS_BANK_CODE"].Value.ToString().Trim().PadLeft(5, '0'))))
                                            //{
                                            //    MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            //    txtSIBSBankCode.Text = "";
                                            //    txtSIBSBankCode.Focus();
                                            //    CommandStatus(false);
                                            //    return;
                                            //}
                                            //else
                                            //{
                                            //    if (dsSIBSBankCode.Tables[0].Rows[0]["BRAN_TYPE"].ToString() == "1")
                                            //    {
                                            //        txtBankName.Text = dsSIBSBankCode.Tables[0].Rows[0]["BRAN_NAME"].ToString();
                                            //    }

                                            //}
                                            #endregion
                                            _dt = objControlBRANCH.CHECK_BRANCH_MAP(pGW_BANK_CODE, pSIBS_BANK_CODE);
                                            if (_dt.Rows.Count > 0)
                                            {
                                                //MessageBox.Show("Branch has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                //txtSIBSBankCode.Text = "";
                                                //txtSIBSBankCode.Focus();
                                                //CommandStatus(false);
                                                //return;
                                            }
                                        }
                                        #endregion
                                    }

                                    if (string.IsNullOrEmpty(txtGWBankCode.Text.Trim()))
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        #region //Kiem tra xem do co phai la chi nhanh cua BIDV hay khong

                                        string strWhere = " and ltrim(ibm.sibs_bank_code,'0')=bran.sibs_bank_code(+) and trim(ibm.gw_bank_code)='" + txtGWBankCode.Text.Trim() + "' ";
                                        //dtBranchOfBIDV = objControl.SearchViewIBPS_BANK_MAP(strWhere, "<>", "=");
                                        dtBranchOfBIDV = objControlBRANCH.BRANCH_OtherBranch("1", txtSIBSBankCode.Text.Trim());
                                        if (dtBranchOfBIDV.Rows.Count > 0)
                                        {
                                            txtTellerID.Text = "FPTIBPS" + txtSIBSBankCode.Text.Trim();
                                        }
                                        else
                                        {
                                            //Kiem tra xem do co phai la chi nhanh cua BIDV hay khong

                                            string strOtherBranch = " and ltrim(ibm.sibs_bank_code,'0')=bran.sibs_bank_code(+) and trim(ibm.gw_bank_code)='" + txtGWBankCode.Text.Trim() + "' ";
                                            dtOtherBranch = objControlBRANCH.BRANCH_OtherBranch("2", txtSIBSBankCode.Text.Trim());
                                            if (dtOtherBranch.Rows.Count > 0)
                                            {
                                                MessageBox.Show("This is not branch of MSB!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                CommandStatus(false);
                                                txtSIBSBankCode.SelectAll();
                                                txtSIBSBankCode.Focus();
                                                return;
                                            }
                                        }

                                        #endregion
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtGWBankCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    MessageBox.Show("You must input number!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            try
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
                        }
                    }
                    if ((this.ActiveControl) is TextBox)
                    {
                        (this.ActiveControl as TextBox).SelectAll();
                        return;
                    }

                    if ((this.ActiveControl) is MaskedTextBox)
                    {
                        (this.ActiveControl as MaskedTextBox).SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtBankName_Leave(object sender, EventArgs e)
        {
            txtBankName.Text = clsCheck.ConvertVietnamese(txtBankName.Text.Trim());
        }

        private void txtShortBank_Leave(object sender, EventArgs e)
        {
            txtShortBank.Text = clsCheck.ConvertVietnamese(txtShortBank.Text.Trim());
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            txtDescription.Text = clsCheck.ConvertVietnamese(txtDescription.Text.Trim());
        }

        private void btnCancel_MouseMove(object sender, MouseEventArgs e)
        {
            Cancel = "1";
        }

        private void cmdClose_MouseMove(object sender, MouseEventArgs e)
        {
            sClose = "1";
        }

        private void btnCancel_MouseUp(object sender, MouseEventArgs e)
        {
            Cancel = "0";
        }

        private void cmdClose_MouseUp(object sender, MouseEventArgs e)
        {
            sClose = "0";
        }

        private void frmIBPSBankMap_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdHistory_Click(object sender, EventArgs e)
        {            
            try
            {
                string ISearch = "";
                //string ISearch1 = "";
                ISearch = dgView.Rows[iRows].Cells["GW_BANK_CODE"].Value.ToString().Trim();
                //ISearch1 = dgView.Rows[iRows].Cells["SIBS_BANK_CODE"].Value.ToString().Trim();
                frmHistory frmViewLog = new frmHistory();
                frmViewLog.pForm_name = "IBPS bank list" + ISearch;
                frmViewLog.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdGetBranch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }

        }      
      
    }
}
