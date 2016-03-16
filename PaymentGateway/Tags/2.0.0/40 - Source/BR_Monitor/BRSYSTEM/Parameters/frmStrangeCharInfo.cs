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
    public partial class frmStrangeCharInfo : Form
    {
        #region khai bao

        public int iLoad;
        public string strID;
        public string strSTRANGE_CHAR;
        public string strREPLACE_CHAR;
        public string strMSG_TYPE;
        public string strDEPARTMENT;
        public string strFIELD_CODE;
        public string strMSG_DIRECTION;
        public string strGWTYPE;
        public string strLINE;
        public string strPOSITION;
        //bien dung de load lai du lieu tren form StrangeChar
        public static bool isCancel = false;

        public STRANGE_CHARACTERInfo objInfo = new STRANGE_CHARACTERInfo();

        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private DataSet datDs = new DataSet();        
        private STRANGE_CHARACTERController objControl = new STRANGE_CHARACTERController();
        private ALLCODEInfo objallcode = new ALLCODEInfo();
        private ALLCODEController objallcodecontrol = new ALLCODEController();
        private MSG_FIELDInfo objInfoMSG_FIELD = new MSG_FIELDInfo();
        private MSG_FIELDController objControlMSG_FIELD = new MSG_FIELDController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private clsCheckInput clsCheck = new clsCheckInput();

        private static int iID;
        //---QuyNd capo nhat ngay 30/07/2008
        
        #endregion

        public frmStrangeCharInfo()
        {
            InitializeComponent();
        }

        private void frmStrangeCharInfo_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            //Load data cboGWType
            if (!objGetData.FillDataComboBox(cboGWType, "GWTYPE", "GWTYPE", "GWTYPE",
                "GWTYPESTS='1'", "GWTYPE", true, false, ""))
                return;
            //Load data cboDepartment
            if (!objGetData.FillDataComboBox(cboDepartment, "Content", "CDVAL", "Allcode",
                "CDName='Department'", "Content", true, true, ""))
                return;
            if (frmStrangeChar.isInsert == true)
            {
                ClearTextBox();
            }
            else if (frmStrangeChar.isInsert == false)
            {
                cboGWType.Enabled = false;
                iID = Convert.ToInt32(strID);
                txtStrangeChar.Text = strSTRANGE_CHAR;
                txtReplaceChar.Text = strREPLACE_CHAR;

                cboDepartment.Text = strDEPARTMENT;
                cboDirection.Text = strMSG_DIRECTION;
                cboGWType.Text = strGWTYPE;
                cboMessageType.Text = strMSG_TYPE;
                txtLine.Text = strLINE.ToString();
                txtStartPosition.Text = strPOSITION.ToString();
                txtStrangeChar.Focus();
            }
            else
                cboGWType.Enabled = true;
           
            if (iLoad == 1)//addd
            {
                //cboGWType.Enabled = true;
                cboDirection.Enabled = true;
                cboDepartment.Enabled = true;  //HaNTT10 sua ngay 18.09.2008               
            }
            else//edit
            {
                //cboGWType.Enabled = false;
                DataSet dsGWType = new DataSet();
                dsGWType = objControlMSG_FIELD.GetGWTYPE(cboGWType.Text.Trim().ToUpper());
                if (cboGWType.Text.Trim() == "SWIFT" || dsGWType.Tables[0].Rows.Count == 0)
                {
                    this.cboMessageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
                    cboMessageType.Text = strMSG_TYPE;
                    txtFiled.Visible = true;
                    txtFiled.Text = strFIELD_CODE;
                    listField.Visible = false;
                }
                else
                {                   
                    listField.SelectionMode = SelectionMode.One;
                    listField.Select();
                    listField.SelectedItem = true;
                    listField.Text = strFIELD_CODE;
                }
            }
            if (cboGWType.Text == "IBPS")
            {
                cboMessageType.Enabled = false;
            }
            else { cboMessageType.Enabled = true; }

            if (frmStrangeChar.isLock)
            {
                this.cboDirection.DropDownStyle = ComboBoxStyle.DropDown;
                LockTextBox(true);
                cboDepartment.Enabled = false; //HaNTT10 sua ngay 18.09.2008
                txtLine.ReadOnly = true;
                txtStartPosition.ReadOnly = true;
                cmdSave.Enabled = false;
            }
            else if (frmStrangeChar.isLock == false)
            {
                LockTextBox(false);
                if (cboDirection.Text.Trim().ToUpper() == "SWIFT-SIBS") //HaNTT10 sua ngay 18.09.2008
                {
                    cboDepartment.Text = "";
                    cboDepartment.Enabled = false;
                }
                else
                {
                    cboDepartment.Enabled = true;
                }               
            }
            cboGWType.Focus();            
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            isCancel = true;
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strFIELD="";
                string strConten = "";
                isCancel = false;
                
                if (String.IsNullOrEmpty(cboGWType.Text) ||  
                    String.IsNullOrEmpty(cboDirection.Text.Trim()))
                {
                    Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                    
                    cmdSave.Enabled = true;
                    return;
                }
                
                else
                {
                    if (txtStrangeChar.Text == "")
                    {
                        Common.ShowError("You must input data!", 3, MessageBoxButtons.OK);                        
                        txtStrangeChar.Focus();
                        return;
                    }
                    else if (txtStrangeChar.Text == " ")
                    {
                        Common.ShowError("Space character must be replace by 'blank'!", 3,
                            MessageBoxButtons.OK);                        
                        txtReplaceChar.Text = "blank";
                        txtReplaceChar.Focus();                        
                    }
                    else
                    {
                        if (txtReplaceChar.Text.Trim() == txtStrangeChar.Text.Trim())
                        {
                            Common.ShowError("Replace character is the same as Stranger character!", 
                                3, MessageBoxButtons.OK);                            
                            txtReplaceChar.Focus();
                            txtReplaceChar.SelectAll();
                            return;
                        }
                        //}
                        DataSet dsGWType = new DataSet();
                        dsGWType = objControlMSG_FIELD.GetGWTYPE(cboGWType.Text.Trim().ToUpper());

                        if (cboGWType.Text == "SWIFT" || dsGWType.Tables[0].Rows.Count == 0)
                        {
                            if (!string.IsNullOrEmpty(txtLine.Text.Trim()) & 
                                string.IsNullOrEmpty(txtFiled.Text.Trim()))
                            {
                                Common.ShowError("You must input Field value!",3, MessageBoxButtons.OK);                                
                                txtLine.Text = "";
                                txtFiled.Focus();
                                return;
                            }
                            if (string.IsNullOrEmpty(txtLine.Text.Trim()) & 
                                !string.IsNullOrEmpty(txtStartPosition.Text.Trim()))
                            {
                                Common.ShowError("You must input Line value!", 3, MessageBoxButtons.OK);                                
                                txtStartPosition.Text = "";
                                txtLine.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtLine.Text.Trim()) & 
                                string.IsNullOrEmpty(listField.Text.Trim()))
                            {
                                Common.ShowError("You must input Field value!", 3, 
                                    MessageBoxButtons.OK);                                
                                txtLine.Text = "";
                                listField.Focus();
                                return;
                            }
                            if (string.IsNullOrEmpty(txtLine.Text.Trim()) & 
                                !string.IsNullOrEmpty(txtStartPosition.Text.Trim()))
                            {
                                Common.ShowError("You must input Line value!", 3, 
                                    MessageBoxButtons.OK);
                                txtStartPosition.Text = "";
                                txtLine.Focus();
                                return;
                            }
                        }

                        objInfo.ID = iID;
                        objInfo.STRANGE_CHAR = txtStrangeChar.Text;
                        objInfo.REPLACE_CHAR = txtReplaceChar.Text;
                        if (cboMessageType.Text.Trim().Length > 2)
                        {
                            if (cboMessageType.Text.Trim().Substring(0, 2) != "MT")
                            {
                                objInfo.MSG_TYPE = "MT" + cboMessageType.Text.Trim();
                            }
                            else
                            {
                                objInfo.MSG_TYPE = cboMessageType.Text;
                            }
                        }
                        objInfo.DEPARTMENT = cboDepartment.Text;

                        objInfo.MSG_DIRECTION = cboDirection.Text;
                        objInfo.GWTYPE = cboGWType.Text;
                        objInfo.LINE = txtLine.Text.Trim().ToString();
                        objInfo.POSITION = txtStartPosition.Text.Trim().ToString();
                        if (cboGWType.Text == "SWIFT" || dsGWType.Tables[0].Rows.Count == 0)
                        {                            
                            if (txtFiled.Text.Trim().Length > 1)
                            {
                                if (txtFiled.Text.Trim().Substring(0, 1) == "F")
                                {
                                    objInfo.FIELD_CODE = txtFiled.Text.Trim().Substring(1);
                                }
                                else
                                {
                                    objInfo.FIELD_CODE = txtFiled.Text.Trim();
                                }
                            }
                            if (frmStrangeChar.isInsert)
                            {
                                if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                {
                                    return;
                                }
                                else

                                    objControl.AddSTRANGE_CHARACTER(objInfo);

                                //lay thong tin de ghilog----------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                int Log_level = 1;
                                strConten = "Strange character";
                                string strWorked = "Insert";
                                string strTable = "STRANGECHAR";
                                string strOld_value = "";
                                string strNew_value = iID.ToString() + "/" + objInfo.STRANGE_CHAR + 
                                    "/" + objInfo.REPLACE_CHAR + "/" + objInfo.MSG_TYPE + "/" + 
                                    objInfo.DEPARTMENT + "/" + objInfo.MSG_DIRECTION + "/" + 
                                    objInfo.GWTYPE + "/" + objInfo.LINE + "/" + objInfo.POSITION;
                                //-----------------------------------------
                                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                    strWorked, strTable, strOld_value, strNew_value);
                                Common.ShowError("Data has inserted successfully!", 1, 
                                    MessageBoxButtons.OK); 
                                strConten = " Insert data into Strange Characters ";
                            }
                            else //neu la Update  voi GWTYPE="SWIFT"
                            {
                                if (txtStrangeChar.Text.Trim() == "'")
                                {
                                    //kiem tra neu chua sua du lieu thi van cho luu nguyen gia tri cu
                                    if (cboGWType.Text.Trim() == strGWTYPE && 
                                        cboDirection.Text.Trim() == strMSG_DIRECTION && 
                                        cboDepartment.Text.Trim() == strDEPARTMENT
                                        && txtStrangeChar.Text.Trim() == strSTRANGE_CHAR && 
                                        txtReplaceChar.Text.Trim() == strREPLACE_CHAR && 
                                        cboMessageType.Text.Trim() == strMSG_TYPE
                                        && txtFiled.Text.Trim() == strFIELD_CODE && 
                                        txtLine.Text.Trim() == strLINE && 
                                        txtStartPosition.Text.Trim() == strPOSITION)
                                    {
                                    }
                                    else if (cboGWType.Text.Trim() == strGWTYPE && 
                                        cboDirection.Text.Trim() == strMSG_DIRECTION && 
                                        cboDepartment.Text.Trim() == strDEPARTMENT
                                        && txtStrangeChar.Text.Trim() == strSTRANGE_CHAR)
                                    {
                                    }
                                    else
                                    {
                                        if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                        {
                                            return;
                                        }
                                    }
                                }
                                else// ko phai la dau nhay don "'"
                                {
                                    if (cboGWType.Text.Trim() == strGWTYPE && 
                                        cboDirection.Text.Trim() == strMSG_DIRECTION && 
                                        cboDepartment.Text.Trim() == strDEPARTMENT
                                        && txtStrangeChar.Text.Trim() == strSTRANGE_CHAR && 
                                        txtReplaceChar.Text.Trim() == strREPLACE_CHAR && 
                                        cboMessageType.Text.Trim() == strMSG_TYPE
                                        && txtFiled.Text.Trim() == strFIELD_CODE && 
                                        txtLine.Text.Trim() == strLINE && 
                                        txtStartPosition.Text.Trim() == strPOSITION)
                                    {
                                    }
                                    else if (cboGWType.Text.Trim() == strGWTYPE && 
                                        cboDirection.Text.Trim() == strMSG_DIRECTION && 
                                        cboDepartment.Text.Trim() == strDEPARTMENT
                                        && txtStrangeChar.Text.Trim() == strSTRANGE_CHAR)
                                    {
                                    }
                                    else
                                    {
                                        if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                        {
                                            return;
                                        }
                                    }
                                }
                                objControl.UpdateSTRANGE_CHARACTER(objInfo);

                                //lay thong tin de ghilog----------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                int Log_level = 1;
                                strConten = "Strange character";
                                string strWorked = "Update";
                                string strTable = "STRANGECHAR";
                                string strOld_value = strSTRANGE_CHAR + "/" + strREPLACE_CHAR + 
                                    "/" + strMSG_TYPE + "/" + strDEPARTMENT + "/" + strFIELD_CODE + 
                                    "/" + strMSG_DIRECTION + "/" + strGWTYPE + "/" + strLINE + 
                                    "/" + strPOSITION;
                                string strNew_value = objInfo.STRANGE_CHAR + "/" + 
                                    objInfo.REPLACE_CHAR + "/" + objInfo.MSG_TYPE + "/" + 
                                    objInfo.DEPARTMENT + "/" + objInfo.FIELD_CODE + "/" + 
                                    objInfo.MSG_DIRECTION + "/" + objInfo.GWTYPE + "/" + 
                                    objInfo.LINE + "/" + objInfo.POSITION;
                                //-----------------------------------------
                                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                    strWorked, strTable, strOld_value, strNew_value);
                                Common.ShowError("Data has updated successfully!", 
                                    1, MessageBoxButtons.OK);                                
                                strConten = " Update data into Strange Characters ";
                            }
                        }
                        else //ko phai la kenh SWIFT
                        {
                            // Neu la Insert voi GWTYPE khong la "SWIFT"
                            if (frmStrangeChar.isInsert)
                            {
                                if (listField.SelectedItems.Count > 0)
                                {
                                    for (int i = 0; i < listField.SelectedItems.Count; i++)
                                    {
                                        strFIELD += listField.SelectedItems[i].ToString() + ";";
                                    }
                                    string[] strArrayField = strFIELD.Split(';');
                                    for (int j = 0; j < strArrayField.Count() - 1; j++)
                                    {                                       
                                        if (strArrayField[j].ToString().Trim().Length > 1)
                                        {
                                            if (strArrayField[j].ToString().Trim().Substring(0, 1) == "F")
                                            {
                                                objInfo.FIELD_CODE = strArrayField[j].ToString().Trim().Substring(1);
                                            }
                                            else
                                            {
                                                objInfo.FIELD_CODE = strArrayField[j].ToString();
                                            }
                                        }
                                        
                                        if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                        {
                                            return;
                                        }
                                        else
                                            objControl.AddSTRANGE_CHARACTER(objInfo);

                                        //lay thong tin de ghilog----------------------
                                        DateTime dtLog = DateTime.Now;
                                        string strUser = BR.BRLib.Common.strUsername;
                                        string useride = BR.BRLib.Common.Userid;
                                        int Log_level = 1;
                                        strConten = "Strange character";
                                        string strWorked = "Insert";
                                        string strTable = "STRANGECHAR";
                                        string strOld_value = "";
                                        string strNew_value = iID.ToString() + "/" + objInfo.STRANGE_CHAR + 
                                            "/" + objInfo.REPLACE_CHAR + "/" + objInfo.MSG_TYPE + "/" + 
                                            objInfo.DEPARTMENT + "/" + objInfo.MSG_DIRECTION + "/" + 
                                            objInfo.GWTYPE + "/" + objInfo.LINE + "/" + objInfo.POSITION;
                                        //-----------------------------------------
                                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                            strWorked, strTable, strOld_value, strNew_value);
                                    }
                                }
                                else //neu ko co du lieu trong Listview
                                {
                                    objInfo.FIELD_CODE = "";
                                    if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                    {
                                        return;
                                    }
                                    else
                                        objControl.AddSTRANGE_CHARACTER(objInfo);

                                    //lay thong tin de ghilog----------------------
                                    DateTime dtLog = DateTime.Now;
                                    string strUser = BR.BRLib.Common.strUsername;
                                    string useride = BR.BRLib.Common.Userid;
                                    int Log_level = 1;
                                    strConten = "Strange character";
                                    string strWorked = "Insert";
                                    string strTable = "STRANGECHAR";
                                    string strOld_value = "";
                                    string strNew_value = iID.ToString() + "/" + objInfo.STRANGE_CHAR + 
                                        "/" + objInfo.REPLACE_CHAR + "/" + objInfo.MSG_TYPE + "/" + 
                                        objInfo.DEPARTMENT + "/" + objInfo.MSG_DIRECTION + "/" + 
                                        objInfo.GWTYPE + "/" + objInfo.LINE + "/" + objInfo.POSITION;
                                    //-----------------------------------------
                                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                        strWorked, strTable, strOld_value, strNew_value);
                                }
                                Common.ShowError("Inserted successfully!", 1, MessageBoxButtons.OK);                                
                                ClearTextBox();
                                strConten = " Insert data into Strange Characters ";
                            }
                            // Neu la Update voi GWTYPE khong la "SWIFT"
                            else
                            {
                                if (listField.Text.Trim().Length > 1)
                                {
                                    if (listField.Text.Trim().Substring(0, 1) == "F")
                                    {
                                        objInfo.FIELD_CODE = listField.Text.Trim().Substring(1);
                                    }
                                    else
                                    {
                                        objInfo.FIELD_CODE = listField.Text;
                                    }
                                }
                                if (txtStrangeChar.Text.Trim() == "'")
                                {
                                    if (cboGWType.Text.Trim() == strGWTYPE & 
                                        cboDirection.Text.Trim() == strMSG_DIRECTION & 
                                        cboDepartment.Text.Trim() == strDEPARTMENT
                                      & txtStrangeChar.Text.Trim() == strSTRANGE_CHAR)
                                    {
                                        if (cboGWType.Text.Trim() == strGWTYPE & 
                                            cboDirection.Text.Trim() == strMSG_DIRECTION & 
                                            cboDepartment.Text.Trim() == strDEPARTMENT
                                          & txtStrangeChar.Text.Trim() == strSTRANGE_CHAR & 
                                          txtReplaceChar.Text.Trim() == strREPLACE_CHAR & 
                                          cboMessageType.Text.Trim() == strMSG_TYPE
                                          & listField.Text.Trim() == strFIELD_CODE & 
                                          txtLine.Text.Trim() == strLINE & 
                                          txtStartPosition.Text.Trim() == strPOSITION)
                                        {
                                        }
                                    }
                                    else
                                    {
                                        if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                        {
                                            return;
                                        }
                                        else
                                        {

                                        }
                                    }                                    
                                }
                                else //neu ko la ky tu nhay don "'"
                                {
                                    if (cboGWType.Text.Trim() == strGWTYPE & 
                                        cboDirection.Text.Trim() == strMSG_DIRECTION & 
                                        cboDepartment.Text.Trim() == strDEPARTMENT
                                        & txtStrangeChar.Text.Trim() == strSTRANGE_CHAR & 
                                        txtReplaceChar.Text.Trim() == strREPLACE_CHAR & 
                                        cboMessageType.Text.Trim() == strMSG_TYPE
                                        & listField.Text.Trim() == strFIELD_CODE & 
                                        txtLine.Text.Trim() == strLINE & 
                                        txtStartPosition.Text.Trim() == strPOSITION)
                                    {
                                    }
                                    else
                                    {
                                        //CheckSTRANGE_CHARACTER(); //kiem tra trung du lieu
                                        if (CheckSTRANGE_CHARACTER()) //kiem tra trung du lieu
                                        {
                                            return;
                                        }
                                    }
                                }

                                objControl.UpdateSTRANGE_CHARACTER(objInfo);
                                //lay thong tin de ghilog----------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                int Log_level = 1;
                                strConten = "Strange character";
                                string strWorked = "Update";
                                string strTable = "STRANGECHAR";
                                string strOld_value = strSTRANGE_CHAR + "/" + strREPLACE_CHAR + "/" + 
                                    strMSG_TYPE + "/" + strDEPARTMENT + "/" + strFIELD_CODE + "/" + 
                                    strMSG_DIRECTION + "/" + strGWTYPE + "/" + strLINE + "/" + strPOSITION;
                                string strNew_value = objInfo.STRANGE_CHAR + "/" + 
                                    objInfo.REPLACE_CHAR + "/" + objInfo.MSG_TYPE + "/" + 
                                    objInfo.DEPARTMENT + "/" + objInfo.FIELD_CODE + "/" + 
                                    objInfo.MSG_DIRECTION + "/" + objInfo.GWTYPE + "/" + 
                                    objInfo.LINE + "/" + objInfo.POSITION;
                                //-----------------------------------------
                                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                    strWorked, strTable, strOld_value, strNew_value);
                                Common.ShowError("Data has updated successfully!", 1, MessageBoxButtons.OK);                                
                                strConten = " Update data into Strange Characters ";
                            }
                        }
                        //
                    }
                }
                LockTextBox(false);
                cboDepartment.Enabled = true;  //HaNTT10 sua ngay 18.09.2008
                this.Close();
                
              }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private bool CheckSTRANGE_CHARACTER()
        {
            bool result = false; //chua ton tai du lieu trung
            if (objControl.CheckSTRANGE_CHARACTER_Record(objInfo) > 0)
            {
                Common.ShowError("This strange character has already existed!", 
                    3, MessageBoxButtons.OK);                
                txtStrangeChar.Focus();
                cmdSave.Enabled = true;
                return true;
            }           
            else if( objInfo.ID.ToString() != strID) 
            {
                if (objControl.CheckSTRANGE_CHARACTER_Dif(objInfo) > 0)
                {
                    Common.ShowError("Conflict strange character!",
                    3, MessageBoxButtons.OK);                    
                    txtStrangeChar.Focus();
                    cmdSave.Enabled = true;
                    return true;
                }                
            }
            return result;
        }       

        
        public void LockTextBox(Boolean a)
        {
            txtStrangeChar.ReadOnly = a;
            txtReplaceChar.ReadOnly = a;
            cboMessageType.Enabled = !a;           
            listField.Enabled = !a;
            txtFiled.Enabled = !a;
            cboDirection.Enabled = !a;
            if (frmStrangeChar.isInsert)
            cboGWType.Enabled = !a;
            
            txtLine.ReadOnly = a;
            txtStartPosition.ReadOnly = a;
        }

        private void ClearTextBox()
        {
            listField.Text = "";
            txtLine.Text = "";
            cboMessageType.Text = "";
            txtReplaceChar.Text = "";
            txtStartPosition.Text = "";
            txtStrangeChar.Text = "";
            cboDepartment.Text = "";
            cboDirection.Text = "";
            cboGWType.Text = "";
        }

        private void txtLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!",
                    3, MessageBoxButtons.OK);                
            }          
        }

        private void txtStartPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!",
                    3, MessageBoxButtons.OK);                
            }           
        }

        private void txtMSGType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!",
                    3, MessageBoxButtons.OK);
            }
        }      

        
        private void cboGWType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (frmStrangeChar.isLock )
                {
                    return;
                }               
                else
                {
                    this.cboGWType.DropDownStyle = ComboBoxStyle.DropDownList;
                    //-------------------------------------------------------------
                    this.cboDirection.DropDownStyle = ComboBoxStyle.DropDownList;
                    string strGWTYPE = cboGWType.Text.Trim();                    
                    DataSet datAllcode1 = new DataSet();
                    //cboDirection.Items.Clear();
                    listField.Items.Clear();
                    GetData objCombo =new GetData ();

                    objCombo.FillDataComboBox(cboDirection,"Content","cdval","ALLCODE",
                         "Trim(cdname) = 'MSGDirection' and trim(gwtype) = '" + cboGWType.Text + "'",
                         "Content",true,true," ");

                    if (cboGWType.Text == "IBPS")
                    {
                        cboMessageType.Enabled = false;
                        txtFiled.Visible = false;
                        listField.Visible = true;
                        this.cboMessageType.DropDownStyle = ComboBoxStyle.DropDown;
                        listField.Items.Clear();
                        GetData.getDataListBox(listField, "FIELD_NAME", "MSG_FIELD", "GWTYPE", cboGWType.Text, "MSG_TYPE", "");
                    }
                    else if (cboGWType.Text == "SWIFT")
                    {
                        listField.Visible = false; // HaNTT10
                        txtFiled.Visible = true;
                        cboMessageType.Enabled = true;
                        this.cboMessageType.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    else
                    {
                        DataSet dsGWType = new DataSet();
                        dsGWType = objControlMSG_FIELD.GetGWTYPE(cboGWType.Text.Trim().ToUpper());
                        if (dsGWType.Tables[0].Rows.Count == 0)
                        {
                            this.cboMessageType.DropDownStyle = ComboBoxStyle.DropDown;
                            cboMessageType.Items.Clear();
                            listField.Visible = false; // HaNTT10
                            txtFiled.Visible = true;
                        }
                        else
                        {
                            this.cboMessageType.DropDownStyle = ComboBoxStyle.DropDownList;

                            ////cboMessageType.SelectedItem = 1;
                            ////GetData.getDataComboWhere(cboMessageType, "MSG_TYPE", "MSG_FIELD", "GWTYPE", cboGWType.Text);
                            ////cboMessageType.Items.Add("");
                            ////cboMessageType.SelectedIndex = 0;
                            ////cboMessageType.Sorted = true;
                            if (!objGetData.FillDataComboBox(cboMessageType, "MSG_TYPE", "MSG_TYPE",
                                "MSG_FIELD", "GWTYPE='" + cboGWType.Text + "'", "MSG_TYPE", true, false, ""))
                                return;
                            listField.Visible = true;
                            txtFiled.Visible = false;
                        }
                        cboMessageType.Enabled = true;                       
                    }
                }
                if (frmStrangeChar.isInsert == false)
                {
                    //this.cboDirection.DropDownStyle = ComboBoxStyle.DropDown;
                    if (strMSG_DIRECTION == "SWIFT-SIBS")
                    {
                        cboDirection.Text= "SWIFT-SIBS";
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cboDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboGWType_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.cboGWType.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboDepartment_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.cboGWType.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        
        private void cboMessageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboMessageType.Text))
            {
                listField.Items.Clear();
            }
            else
            {
                listField.Items.Clear();
                GetData.getDataListBox(listField, "FIELD_NAME", "MSG_FIELD", "GWTYPE", 
                    cboGWType.Text, "MSG_TYPE", cboMessageType.Text);
            }
        }

        private void enterKey(object sender, KeyPressEventArgs e)
        {
             //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (txtStrangeChar.Focused)
                {
                    txtReplaceChar.Focus();
                    txtReplaceChar.SelectAll();
                }
                else if (txtReplaceChar.Focused)
                {
                    txtLine.Focus();
                    txtLine.SelectAll();
                }
                else if (txtLine.Focused)
                {
                    txtStartPosition.Focus();
                    txtStartPosition.SelectAll();
                }
            }
        }

        private void KeyDownPress(object sender, KeyEventArgs e)
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

        private void cboMessageType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!",
                    3, MessageBoxButtons.OK);                
            }
        }

        private void txtReplaceChar_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtReplaceChar.Text.Trim()))
            {
                return;
            }
            else
            {
                if (txtReplaceChar.Text.Trim() == txtStrangeChar.Text.Trim())
                {
                    Common.ShowError("Replace character is the same as Strange character!",
                    3, MessageBoxButtons.OK);                    
                    txtReplaceChar.Focus();
                    txtReplaceChar.SelectAll();
                    return;
                }
            }
            txtReplaceChar.Text = clsCheck.ConvertVietnamese(txtReplaceChar.Text.Trim());
        }

        private void txtFiled_Leave(object sender, EventArgs e)
        {
            if (txtFiled.Text.Trim().Length > 1)
            {
                if (txtFiled.Text.Trim().Substring(0, 1) == "F")
                {
                    objInfo.FIELD_CODE = txtFiled.Text.Trim().Substring(1);
                }
                else
                {
                    objInfo.FIELD_CODE = txtFiled.Text.Trim();
                }
            }
            
        }

        private void txtLine_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtLine.Text.Trim()))
            {
                return;
            }
            else
            {
                if (cboGWType.Text.Trim() == "SWIFT")
                {
                    if (txtFiled.Text.Trim() == "")
                    {
                        Common.ShowError("You must input Field value!",
                            3, MessageBoxButtons.OK);                        
                        txtLine.Text = "";
                        txtFiled.Focus();
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(listField.Text.Trim()))
                    {
                        Common.ShowError("You must input Field value!",
                            3, MessageBoxButtons.OK);                        
                        txtLine.Text = "";
                        listField.Focus();
                        return;
                    }
                }
            }
        }

        private void txtStartPosition_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtStartPosition.Text.Trim()))
            {
                cmdSave.Enabled = true;
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtLine.Text.Trim()))
                {
                    Common.ShowError("You must input Line value!",
                            3, MessageBoxButtons.OK);                    
                    txtStartPosition.Text = "";
                    txtLine.Focus();
                    cmdSave.Enabled = true;
                    return;
                }
                if (cboGWType.Text.Trim() == "SWIFT")
                {
                    if (txtFiled.Text.Trim() == "")
                    {
                        Common.ShowError("You must input Field value!",
                            3, MessageBoxButtons.OK);                        
                        txtStartPosition.Text = "";
                        txtFiled.Focus();
                        cmdSave.Enabled = true;
                        return;
                    }
                }
                else
                {
                    if (listField.Items.Count == 0)
                    {
                        Common.ShowError("You must input Field value!",
                              3, MessageBoxButtons.OK);                         
                        listField.Focus();
                        cmdSave.Enabled = true;
                        return;
                    }
                }
            }
        }

        private void txtStrangeChar_Leave(object sender, EventArgs e)
        {
            if (txtStrangeChar.Text == " ")
            {
                txtStrangeChar.Text = clsCheck.ConvertVietnamese(txtStrangeChar.Text);
            }
            else
            {
                txtStrangeChar.Text = clsCheck.ConvertVietnamese(txtStrangeChar.Text);
            }
        }

        private void listField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listField.Focus();
                listField.Select();                
            }
        }

        private void frmStrangeCharInfo_MouseMove(object sender, MouseEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }

        private void cboDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDirection.Text.Trim().ToUpper() == "SWIFT-SIBS")
            {
                cboDepartment.Text = "";
                cboDepartment.Enabled = false;
            }
            else
            {
                cboDepartment.Enabled = true;
            }
        }

    }
}
