using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRBusinessObject;
using BR.BRSYSTEM;


namespace BR.BRSWIFT
{
    public partial class frmSWIWModulAuto :Form
    {
        #region Ham va bien
        public Boolean blnValue = false;
        public string strValue = "";
        public string strError = "";
        private BR.BRBusinessObject.SWIFT_MODULE_ACTION_Info objInfo = new BR.BRBusinessObject.SWIFT_MODULE_ACTION_Info();
        private BR.BRBusinessObject.SWIFT_MODULE_ACTIONController objControl = new BR.BRBusinessObject.SWIFT_MODULE_ACTIONController();
        private BR.BRBusinessObject.SWIFT_AUTO_VALUE_Info objInfo_1 = new BR.BRBusinessObject.SWIFT_AUTO_VALUE_Info();
        private BR.BRBusinessObject.SWIFT_AUTO_VALUEController objControl_1 = new BR.BRBusinessObject.SWIFT_AUTO_VALUEController();
        private BR.BRBusinessObject.GetData objGetData = new BR.BRBusinessObject.GetData();
        private BR.BRBusinessObject.SWIFT_AUTO_VALUEController objAutovalue = new SWIFT_AUTO_VALUEController();       
        string strBranchCon = "";
        string strView = "";
        string strRealMasg = "";        
        int iKeycount = 0;
        string strKeyold = "";
        string strBranchValue = "";
        #endregion

        public frmSWIWModulAuto()
        {
            InitializeComponent();
        }

       

        private void cboFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKeyword.Text.Trim() == "System.Data.DataRowView")
                return;
            iKeycount = 0; txtCriteria.Text = "";
            cboKeyword.Text = ""; cboSubFunction.Text = "";
            cboOperation.Text = ""; txtValue.Text = "";
            strKeyold = ""; iKeycount = 0;
            strRealMasg = cboFunction.SelectedValue.ToString().Trim();
            strView = cboFunction.Text.Trim();
            txtCriteria.Text = strView;       
        }

        private void cmdClear1_Click(object sender, EventArgs e)
        {
            txtCriteria.Text = null;
           // strCriatia = "";
        }

        private void cboKeyword_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboOperation.Text = "";
            txtValue.Text = "";

            if (cboKeyword.Text.Trim() != "")
            {
                cboFunction.Enabled = false;
                cboSubFunction.Enabled = false;
            }
            else
            {
                cboFunction.Enabled = true;
                cboSubFunction.Enabled = true;
            }

            if (cboFunction.Text.Trim() == "" && cboSubFunction.Text.Trim() == "")
            {
                strView = "";
                strRealMasg = "";
                strRealMasg = " " + cboKeyword.Text.Trim();
                strView = strView + " " + cboKeyword.Text;
            }
            else
            {
                if (iKeycount > 0)
                {
                    if (strRealMasg != "")
                    {
                        strRealMasg = strRealMasg.Replace(strKeyold, cboKeyword.Text.Trim());
                        strView = strView.TrimEnd(')');
                        strView = GetCountNgoac(strView);
                        strView = strView.Replace(strKeyold, cboKeyword.Text.Trim());
                    }
                    else
                    {
                        strRealMasg = strRealMasg + "  " + cboKeyword.Text.Trim();
                        strView = strView.TrimEnd(')');
                        strView = strView + " " + cboKeyword.Text;
                        strView = GetCountNgoac(strView);
                    }
                }
                else
                {
                    if (strRealMasg != "")
                    { strRealMasg = strRealMasg.Replace("F", cboKeyword.Text.Trim()); }
                    else
                    { strRealMasg = strRealMasg + "  " + cboKeyword.Text.Trim(); }

                    strView = strView.TrimEnd(')');
                    strView = strView + " " + cboKeyword.Text;
                    strView = GetCountNgoac(strView);
                }
            }
            iKeycount = iKeycount + 1;
            strKeyold = cboKeyword.Text.Trim();
            txtCriteria.Text = strView;
        }

        private void cboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboKeyword.Text.Trim() != "")
            {
                if (txtValue.Text.Trim() != "")
                {
                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + " '" + txtValue.Text + "'";
                    strBranchValue = strRealMasg + " " + cboOperation.Text;
                }
                else
                {
                    txtCriteria.Text = strView + " "+ cboOperation.Text.Trim();
                    strBranchValue = strRealMasg + " "  + cboOperation.Text;

                }
            }            
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            strKeyold = "";
            iKeycount = 0;
            cboFunction.Enabled = true;
            cboSubFunction.Enabled = true;

            if (txtCriteriaMess.Text.Trim() == "")
            {

                txtCriteriaMess.Text = txtCriteria.Text.Trim();
                strBranchCon = strBranchValue;
            }
            else
            {
                txtCriteriaMess.Text = txtCriteriaMess.Text + " " + '\r' + '\n' +  txtCriteria.Text.Trim();
                strBranchCon = strBranchCon + " "  + strBranchValue;
            }
            //////////////////////////////////////////////////////////////////////////////////////            
            //Ngay sua:     01/08/2008
            int intCompare = -1;
            if (String.IsNullOrEmpty(txtKeyword.Text))
                txtKeyword.Text = cboKeyword.Text.Trim();
            else
            {
                intCompare = txtKeyword.Text.IndexOf(cboKeyword.Text.Trim());
                if (intCompare < 0)
                    txtKeyword.Text = txtKeyword.Text.Trim() + " ; " + cboKeyword.Text.Trim();
            }
            #region refresh textbox ve null
            cboFunction.Text = "";
            cboSubFunction.Text = "";
            cboOperation.Text = "";
            cboKeyword.Text = "";
            txtValue.Text = "";
            strValue = "";
            strBranchValue = "";
            cboExpression.Text = "";
            txtCriteria.Text = "";
            #endregion
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            strBranchCon = "";
            txtKeyword.Text = "";
            txtCriteriaMess.Text = "";           
        }

        private void cmdValidate_Click(object sender, EventArgs e)
        {
           

            int iCheck = 0;
            //Kiem tra keyword
            if (txtKeyword.Text.Length < 1)
            {
                MessageBox.Show("Keyword is null!", Common.sCaption);
                return;
            }
            //Criteria message
            if (txtCriteriaMess.Text.Length < 1)
            {
                MessageBox.Show("Criteria message is null!", Common.sCaption);
                return;
            }
            //kiem tra keycode, message            
            if (!objGetData.CheckKeyword(txtKeyword.Text.ToString(),
                strBranchCon, out strError, out iCheck))
            {
                MessageBox.Show(strError, Common.sCaption);                
                return;
            }
            if (iCheck == 1)
            {
                MessageBox.Show("Criteria message is valid", Common.sCaption);
            }
            else
            {
                MessageBox.Show("Criteria message is invalid", Common.sCaption);
            }

        }

        private int Check()
        {
            if (txtCriteriaName.Text.Length < 1)
            {
                MessageBox.Show("CriteriaName is null", Common.sCaption);
                txtCriteriaName.Focus();
                return -1;
            }

            if (txtPriority.Text.Length < 1)
            {
                MessageBox.Show("Priority is null", Common.sCaption);
                txtPriority.Focus();
                return -1;
            }

            if (cboModule.Text.Length < 1)
            {
                MessageBox.Show("Module is null", Common.sCaption);
                cboModule.Focus();
                return -1;
            }

            //Kiem tra keyword
            if (txtKeyword.Text.Length < 1)
            {
                MessageBox.Show("Keyword is null", Common.sCaption);
                txtKeyword.Focus();
                return -1;
            }
            //Criteria message
            if (txtCriteriaMess.Text.Length < 1)
            {
                MessageBox.Show("Criteria message is null", Common.sCaption);                
                return -1;
            }
            //kiem tra keycode, message
            int iCheck;
            if (!objGetData.CheckKeyword(txtKeyword.Text.ToString(),
                strBranchCon, out strError, out iCheck))
            {
                MessageBox.Show(strError, Common.sCaption);
                txtKeyword.Focus();
                return -1;
            }
            if (iCheck != 1)
            {
                MessageBox.Show("Criteria message is invalid", Common.sCaption);
                return -1;
            }
           return 1;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            int intI = Check();
            if (intI < 0)
                return;
            else
            {
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show("Do you want to save?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (DlgResult == DialogResult.Yes)
                {
                    //Check trung
                    if (objControl.IDIsExisting(txtCriteriaName.Text.Trim(), txtPriority.Text.Trim(), cboModule.Text.Trim()))
                    {
                        MessageBox.Show("Criteria Name has already exist.", Common.sCaption);
                        txtCriteriaName.Text = "";
                        txtCriteriaName.Focus();
                        cmdSave.Enabled = true;
                        return;
                    }
                    objInfo.DEPARTMENT = cboModule.Text.Trim();
                    objInfo.KEY_WORD = txtKeyword.Text.Trim();
                    objInfo.MESSAGE = strBranchCon;
                    objInfo.DESCPRITION = txtCriteriaMess.Text.Trim();
                    objInfo.PRIORITY = txtPriority.Text.Trim();
                    objInfo.NAME = txtCriteriaName.Text.Trim();

                    if (objControl.AddSWIFT_MODULE_ACTION(objInfo) > 0)
                    {
                        MessageBox.Show("Data has saved successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Can not update database!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    ClearText();
                }                
            }
            //ClearText();
        }

        private void cmdValue_Click(object sender, EventArgs e)
        {
            frmSWIWValueMdl GWSwiftValueMdl = new frmSWIWValueMdl();
            this.Hide();
            GWSwiftValueMdl.ShowDialog(this);
            this.Show();
            txtValue.Text = GWSwiftValueMdl.gGetValueCode;
            strValue = GWSwiftValueMdl.gGetValue;
            txtValue_Leave(sender, e);

            
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboExpression_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboExpression.SelectedIndex >= 0)
            {
                txtCriteriaMess.Text = txtCriteriaMess.Text.Trim() + " " + cboExpression.Text.Trim();
                strBranchCon = strBranchCon + " " + cboExpression.SelectedValue.ToString().Trim();
            }            
        }

        private void GetCombo(ComboBox cboALLCODE)
        {
            try
            {
                DataSet dtsGetCombo = new DataSet();
                dtsGetCombo = objControl_1.GetSWIFT_AUTO_VALUE();
                cboALLCODE.DataSource = dtsGetCombo.Tables[0];
                cboALLCODE.DisplayMember = "KEY_WORD";
                cboALLCODE.ValueMember = "MESSAGE";

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmSWIWModulAuto_Load_1(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            GetCombo(cboKeyword, "SWIFT", "MKEYWORD", "CONTENT", "CONTENT");
            GetCombo(cboOperation, "SWIFT", "OPERATION", "CONTENT", "CONTENT");
            GetCombo(cboExpression, "SYSTEM", "EXPRESSION", "CONTENT", "DESCRIPTION");
            GetCombo(cboModule, "SYSTEM", "Department", "CONTENT", "CONTENT");
            GetCombo(cboFunction, "SWIFT", "FUNCTION", "CONTENT", "DESCRIPTION");
            GetCombo(cboSubFunction, "SWIFT", "FUNCTION", "CONTENT", "DESCRIPTION");
            txtCriteria.Text = "";
            blnValue = false;
            ClearText();
            cboFunction.Enabled = true;
            cboSubFunction.Enabled = true;
         
        }


        private void ClearText()
        {
            #region Refresh otext ve null
            txtCriteria.Text = "";
            txtCriteriaMess.Text = "";
            txtCriteriaName.Text = "";
            txtKeyword.Text = "";
            txtPriority.Text = "";
            cboExpression.Text = "";
            cboFunction.Text = "";
            cboSubFunction.Text = "";
            cboKeyword.Text = "";
            cboModule.Text = "";
            cboOperation.Text = "";            
            txtValue.Text = "";
            #endregion
        }


        //////////////////////////////////////////
        //Muc dich: O txtPriority Chi nhap so        
        //Ngay tao: 01/08/2008
        //////////////////////////////////////////
        private void txtPriority_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("You must input a number!", Common.sCaption);
            }
        }

        private void cmdClearCriteria_Click(object sender, EventArgs e)
        {
            iKeycount = 0;
            strKeyold = "";
           // strCriatia = "";
            cboSubFunction.Text = "";
            cboFunction.Text = "";
            cboKeyword.Text = "";
            
            txtCriteria.Text = "";
            cboFunction.Enabled = true;
            cboSubFunction.Enabled = true;
        }


        private void GetCombo(ComboBox cboALLCODE, string strGWtype, string strCDName, string strValue, string strDes)
        {
            try
            {
                ALLCODEController objAllcode = new ALLCODEController();
                DataTable dattblAlcode = new DataTable();                
                dattblAlcode = objAllcode.GetALLCODE_LIST(strGWtype, strCDName);
                cboALLCODE.DataSource = dattblAlcode;
                cboALLCODE.ValueMember = strValue;
                cboALLCODE.DisplayMember = strDes;

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {           
            string strTemp = "";
            strTemp = objAutovalue.GetSWIFT_AUTO_VALUE(txtValue.Text.Trim());
            strValue = txtValue.Text.Trim();

            if (strTemp != "")
            { GetCriatia(strValue, true); }
            else
                GetCriatia(strValue, false);
        }



        private void GetCriatia(string strValue, bool isselect)
        {

            string strTemp = "";
            strTemp = objAutovalue.GetSWIFT_AUTO_VALUE(txtValue.Text.Trim());
            try
            {
                if (!isselect)
                {
                    strTemp = strValue;
                    if (cboKeyword.Text.Trim().ToUpper() == "MSG_TYPE")
                    {// 
                        if (strValue.Trim().Length >= 2)
                        {
                            if (strValue.Trim().Substring(0, 2) == "MT")
                            {
                                if (txtValue.Text.Trim() == "")
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                                }
                                else
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( '" + strTemp.Trim() + "')";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( '" + strValue.Trim() + "')";
                                }
                            }
                            else
                            {
                                if (txtValue.Text.Trim() == "")
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                                }
                                else
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( 'MT" + strTemp.Trim() + "')";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( 'MT" + strValue.Trim() + "')";
                                }
                            }
                        }
                        else
                        {
                            if (txtValue.Text.Trim() == "")
                            {
                                strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                                txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                            }
                            else
                            {
                                strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( 'MT" + strTemp.Trim() + "')";
                                txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( 'MT" + strValue.Trim() + "')";
                            }
                        }
                    }
                    else
                    {
                        if (txtValue.Text.Trim() == "")
                        {
                            strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                            txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                        }
                        else
                        {
                            strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( '" + strTemp.Trim() + "')";
                            txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( '" + strValue.Trim() + "')";
                        }
                    }

                }
                else
                {
                    if (cboKeyword.Text.Trim().ToUpper() == "MSG_TYPE")
                    {// 
                        if (strValue.Trim().Length >= 2)
                        {
                            if (strValue.Trim().Substring(0, 2) == "MT")
                            {
                                if (txtValue.Text.Trim() == "")
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                                }
                                else
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( " + strTemp.Trim() + ")";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( " + strValue.Trim() + ")";
                                }
                            }
                            else
                            {
                                if (txtValue.Text.Trim() == "")
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                                }
                                else
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( MT" + strTemp.Trim() + ")";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( MT" + strValue.Trim() + ")";
                                }
                            }
                        }
                        else
                        {
                            if (txtValue.Text.Trim() == "")
                            {
                                strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                                txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                            }
                            else
                            {
                                strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "(MT" + strTemp.Trim() + ")";
                                txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( MT" + strValue.Trim() + ")";
                            }
                        }
                    }
                    else
                    {
                        if (txtValue.Text.Trim() == "")
                        {
                            strBranchValue = strRealMasg + " " + cboOperation.Text.Trim();
                            txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                        }
                        else
                        {
                            strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( " + strTemp.Trim() + ")";
                            txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "(" + strValue.Trim() + ")";
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        

        private void txtValue_Leave(object sender, EventArgs e)
        {
            txtValue_TextChanged(sender, e);

        }

        private void cboSubFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            iKeycount = 0;
            strKeyold = "";
            cboKeyword.Text = "";
            cboOperation.Text = "";
            txtValue.Text = "";
            iKeycount = 0;

            if (string.IsNullOrEmpty(cboFunction.Text.ToString()))
            {
                strView = cboSubFunction.Text.Trim();
                txtCriteria.Text = strView;
                strRealMasg = cboSubFunction.SelectedValue.ToString().Trim();

            }
            else
            {
                strView = strView.TrimEnd(')');
                strView = strView + " ( " + cboSubFunction.Text.Trim();
                strView = GetCountNgoac(strView);
                txtCriteria.Text = strView;
                strRealMasg = strRealMasg.Replace("F", cboSubFunction.SelectedValue.ToString().Trim());                
            }
            
        }

        private string GetCountNgoac(string strIN)
        {
            int icount = 0;
            for (int i = 0; i < strIN.Length; i++)
            {
                if (strIN.Substring(i, 1) == "(")
                {
                    icount = icount + 1;
                }
            }

            if (icount > 0)
            {
                for (int i = 0; i < icount; i++)
                {
                    strIN = strIN + ")";
                }

            }
            return strIN;
        }

        private void frmSWIWModulAuto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            Common.bTimer = 1;
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        private void frmSWIWModulAuto_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
