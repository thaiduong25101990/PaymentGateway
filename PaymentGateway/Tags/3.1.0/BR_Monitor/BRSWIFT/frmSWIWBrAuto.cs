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
    public partial class frmSWIWBrAuto : frmBasedata
    {
        #region khai bao cac lop trong bussiness
        private GetData objGetData = new GetData();
        private BR.BRBusinessObject.SWIFT_BRANCH_ACTION_Info objInfo = new BR.BRBusinessObject.SWIFT_BRANCH_ACTION_Info();
        private BR.BRBusinessObject.SWIFT_BRANCH_ACTIONController objControl = new BR.BRBusinessObject.SWIFT_BRANCH_ACTIONController();
        private BR.BRBusinessObject.SWIFT_AUTO_VALUE_Info objInfo_1 = new BR.BRBusinessObject.SWIFT_AUTO_VALUE_Info();
        private BR.BRBusinessObject.SWIFT_AUTO_VALUEController objControl_1 = new BR.BRBusinessObject.SWIFT_AUTO_VALUEController();
        private BR.BRBusinessObject.DATABASE_CLEANController objctrlDatabase = new DATABASE_CLEANController();
        private BR.BRBusinessObject.SWIFT_AUTO_VALUEController objAutovalue = new SWIFT_AUTO_VALUEController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        #endregion

        #region khai bao bien
        //private Boolean blnValue = false;
        public string strValue = "";
        string strError = "";
        //string strGetValueCode="";
        //string strGetValue = "";
        string strBranchCon = "";
        //string strCriatia = "";
        string strCriatia1 = "";
        private string strMessage= "";
        private int ifncount=0;
        private int iKeycount = 0;
        private string  strKeyold = "";
        //string strComFunc;
        private string strBrkeyword ="";
        string strRealMasg="";
        string strView="";
        string strBranchValue="";
        string strSubselect = "";
        #endregion

        public frmSWIWBrAuto()
        {
            InitializeComponent();
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

        private void frmSWIWBrAuto_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            ////////////////////////////////////////////////
            //QuanLD Sua 
            // lay du lieu tu bang Allcode  gan gia tri o cot Content vao truong ValueMember trong combo
            //
            ////////////////////////////////////////////////
            GetCombo(cboFunction, "SWIFT", "FUNCTION", "CONTENT", "DESCRIPTION");
            GetCombo(cboSubFunction, "SWIFT", "FUNCTION", "CONTENT", "DESCRIPTION");
            GetCombo(cboKeyword, "SWIFT", "BKEYWORD", "CONTENT", "CONTENT");
            GetCombo(cboOperation, "SWIFT", "OPERATION", "CONTENT", "CONTENT");
            GetCombo(cboExpression, "SYSTEM", "EXPRESSION", "CONTENT", "DESCRIPTION");


            //cboBranch1.Items.Add("");
            //cboBranch1.Items.Add("TC");
            //cboBranch1.Items.Add("LT");
            //cboBranch1.Items.Add("Other");
            //cboBranch1.Items.Add("TMPRBR");
            //cboBranch1.Items.Add("RECEIVER_BRAN");
            
            ///////////////////////////////////////////////
            //GetData.getDataCombo1(cboBranch1, "SIBS_BANK_CODE", "BRANCH");
            if (!objGetData.FillDataComboBox_Branch(cboBranch1))
                return;


          



            //GetData.getDataComboAllCode(cboBranch1, "CONTENT", "Department", "SYSTEM");
            GetCombo(cboFunction1, "SWIFT", "FUNCTION", "CONTENT", "DESCRIPTION");
            GetCombo(cboKeyword1, "SWIFT", "BKEYWORD", "CONTENT", "CONTENT");
            GetCombo(cboOperation1, "SWIFT", "OPERATION", "CONTENT", "CONTENT");
            GetCombo(cboModule, "SYSTEM", "Department", "CONTENT", "CONTENT");
            GetCboTable();         
            //Muc dich: Them xu ly thu cong
            //Ngay sua: 02/08/2008
            txtCriteria.Text = "";            
            txtCriteria1.ReadOnly = true;
            txtField1.ReadOnly = true;           
            txtBranch.ReadOnly = true;            
            txtBranchMess.ReadOnly = true;
            txtTableDesc.ReadOnly = true;
            Getcomment("SWIFT_BR_AUTO");            
            txtComMess.Text = "";
            strBranchCon = "";
            ClearText();
            cboFunction.Enabled = true;
            cboSubFunction.Enabled = true;
            cmdAdd.Enabled = true;

        }

        private void GetCboTable()
        {
            try
            {
                cboSelectTable.Items.Clear();
                cboSelectTable.Items.Add(" ");
                cboSelectTable.Items.Add("SWIFT_BR_AUTO");
                cboSelectTable.Items.Add("SWIFT_RMBR_AUTO");
                txtTableDesc.Text = "";
                if (cboBranch1.Text == "Other")
                {
                    cboSelectTable.Items.Add("DUAL");
                    //Gan tu dong bang dual
                    //29/08/2008
                    cboSelectTable.Text = "DUAL";
                    txtTableDesc.Text = "DUAL";
                    cboSelectTable.Enabled = false;
                    txtTableDesc.Enabled = false;
                }
                else
                {
                    cboSelectTable.Enabled = true;
                    txtTableDesc.Enabled = true;
                }
            //
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Getcomment(string strTableName)
        {
            try
            {
                DataSet datCom_table = new DataSet();
                datCom_table = objctrlDatabase.GetComment_table(strTableName);
                txtTableDesc.Text = datCom_table.Tables[0].Rows[0]["COMMENTS"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void ClearText()
        {
            try
            {
                #region refresh cac otext ve rong,combobox
                txtCriteria.Text = "";
                txtCriteria1.Text = "";
                txtCriteriaMess.Text = "";
                txtCriteriaName.Text = "";
                txtKeyword.Text = "";
                txtPriority.Text = "";
                cboBranch1.Text = "";
                cboExpression.Text = "";
                txtComMess.Text = "";
                txtRecMess.Text = "";                
                cboFunction.Text = "";
                cboFunction1.Text = "";
                cboKeyword.Text = "";
                cboKeyword1.Text = "";
                cboOperation.Text = "";
                cboOperation1.Text = "";
                txtValue.Text = "";
                cboExpression.Text="";
                cboFunction.Text="";
                cboOperation.Text="";
                cboKeyword.Text="";
                cboFunction1.Text = "";
                cboKeyword1.Text = "";
                cboOperation1.Text = "";
                cboExpression.Text = "";
                cboSubFunction.Text = "";
                txtCriteria.Text = "";
                #endregion
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////
            //Muc dich:            
            //Ngay sua:     01/08/2008
            //txtComFunc.Text = cboFunction.Text.Trim();
            //txtCriteria.Text = txtComFunc.Text.Trim();
            ifncount = 0;            
            txtCriteria.Text = "";
            cboKeyword.Text = "";
            cboSubFunction.Text = "";
            cboOperation.Text = "";
            txtValue.Text = "";
            strKeyold = "";
            iKeycount = 0;
            strRealMasg = cboFunction.SelectedValue.ToString().Trim();
            strView = cboFunction.Text.Trim();
            txtCriteria.Text = strView;
            //strComFunc = cboFunction.SelectedValue.ToString().Trim();
            //txtCriteria.Text = cboFunction.Text.Trim();
            ////////////////////////////////////////////////
        }

        private void cboKeyword_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboOperation.Text = "";
                txtValue.Text = "";

                if (cboKeyword.Text.Trim() != "")
                { cboFunction.Enabled = false; cboSubFunction.Enabled = false; }
                else
                {
                    cboFunction.Enabled = true; cboSubFunction.Enabled = true;
                    cboSubFunction.Text = ""; cboFunction.Text = "";
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
                            if (strKeyold != "")                            
                            {
                                strRealMasg = strRealMasg.Replace(strKeyold, cboKeyword.Text.Trim());
                                strView = strView.TrimEnd(')');
                                strView = GetCountNgoac(strView);
                                strView = strView.Replace(strKeyold, cboKeyword.Text.Trim());
                            }                            
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
                // bien luu lai cau mo ta cua dieu kien
                GetCriatia("", false);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOperation.Text.Trim().ToUpper().IndexOf("NULL") <= 0)
            { txtValue.Enabled = true; }
            else
            { txtValue.Enabled = false; }
            if  (cboKeyword.Text.Trim() !="" )
            {
                if (txtValue.Text.Trim() != "")
                {
                    txtCriteria.Text = strView + " "+ cboOperation.Text.Trim() + "'" + txtValue.Text + "'";
                    strBranchValue = strRealMasg + " " + cboOperation.Text;
                }
                else
                {
                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim();
                    strBranchValue = strRealMasg + " " + cboOperation.Text;
                }
            }
        }

        private void cmdClear1_Click(object sender, EventArgs e)
        {
            iKeycount = 0;
            strKeyold = "";            
            cboSubFunction.Text = "";
            cboFunction.Text = "";
            cboKeyword.Text = "";           
            txtCriteria.Text = "";
            cboFunction.Enabled = true;
            cboSubFunction.Enabled = true;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            strKeyold = "";
            iKeycount = 0;
            cboFunction.Enabled = true;
            cboSubFunction.Enabled = true;
            if (string.IsNullOrEmpty(txtCriteria.Text))
                return;                     
            if (txtComMess.Text.Trim()=="")
            {                
                txtComMess.Text = txtCriteria.Text.Trim();
                strBranchCon = strBranchValue.Trim();
            }
            else
            {
                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                {
                    Common.ShowError("You must input logical operator", 3, MessageBoxButtons.OK);
                    cboExpression.Focus();
                    return;
                }
                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")
                {
                    txtComMess.Text = txtComMess.Text + txtCriteria.Text.Trim();
                    strBranchCon = strBranchCon + strBranchValue.Trim();
                }
                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 2, 2).ToUpper() == "or".ToUpper() ||
                    strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 3, 3).ToUpper() == "and".ToUpper())
                {
                    txtComMess.Text = txtComMess.Text + " " + '\r' + '\n' + " " + txtCriteria.Text.Trim();
                    strBranchCon = strBranchCon + " " + strBranchValue;
                }   
            }
            //strComFunc = "";

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
            #region refresh otext ve rong
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

        private void GetCombo(ComboBox cboALLCODE,string strGWtype,string strCDName,string strValue, string strDes)
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

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboKeyword_TextChanged(object sender, EventArgs e)
        {
            txtCriteria.Text = FilltxtCriteria();
        }

        private void cboOperation1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFunction1.Text = ""; cboKeyword1.Text = "";
            if (string.IsNullOrEmpty(cboFunction.Text.Trim()))
                txtCriteria1.Text = cboKeyword.Text.Trim() + " " + cboOperation.Text.Trim();
            else
                txtCriteria1.Text = txtField1.Text.Trim() + " " + cboOperation1.Text.Trim();
        }

        private void cboFunction1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKeyword1.Text = "";
            txtCriteria1.Text = txtField1.Text.Trim() + " " + 
            cboOperation1.Text.Trim() + " " + cboFunction1.SelectedValue.ToString().Trim();
        }

        private void cboKeyword1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (txtTableDesc.Text)
            {

                case "DUAL":
                    txtCriteria1.Text =  cboOperation1.Text.Trim() +
                       " " + cboFunction1.SelectedValue.ToString().Trim().Replace("F", cboKeyword1.Text );
                    break;
                default:
                    if ((cboOperation1.Text.Trim() == "Like") || (cboOperation1.Text.Trim() == "Not Like"))
                    {
                        if (cboFunction1.Text == "")
                            txtCriteria1.Text = txtField1.Text.Trim() + " " + cboOperation1.Text.Trim() +
                          " %'$Par$'%";
                        else
                            txtCriteria1.Text = txtField1.Text.Trim() + " " + cboOperation1.Text.Trim() +
                         " %" + cboFunction1.SelectedValue.ToString().Trim().Replace("F", " '$Par$' ") + "%";
                    }
                    else
                       
                    {
                        if (cboFunction1.Text == "")
                            txtCriteria1.Text = txtField1.Text.Trim() + " " + cboOperation1.Text.Trim() +
                          " '$Par$' ";
                        else
                            txtCriteria1.Text = txtField1.Text.Trim() + " " + cboOperation1.Text.Trim() +
                          " " + cboFunction1.SelectedValue.ToString().Trim().Replace("F", " '$Par$' ") + "";
                    }
                    break;
            }                  
        }

        private void cboExpression_SelectedIndexChanged(object sender, EventArgs e)
         {
            //Chon toan tu "and, or" hoac "(,)"
            if (cboExpression.SelectedIndex>=0)
            {
                //Neu do dai xau dieu kien >0 
                if (strBranchCon.Length > 0)
                {
                    //Do dai xau dieu kien >1
                    if (strBranchCon.Trim().Length > 1)
                    {
                        //Kiem tra so dau "(" =  so dau ")"
                        if (GetCountChar(strBranchCon, "(") == GetCountChar(strBranchCon, ")"))                            
                        {
                            if(cboExpression.Text.Trim()==")")
                            {
                                return;
                            }
                            else if(cboExpression.Text.Trim()=="(")
                            {
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 3, 3).ToUpper() == "And".ToUpper() || 
                                    strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 2, 2).ToUpper() == "or".ToUpper())
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + " " + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + " " + cboExpression.SelectedValue.ToString().Trim();
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                                {
                                    return;
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + cboExpression.SelectedValue.ToString().Trim();
                                }
                            }
                            else if (cboExpression.Text.Trim().ToUpper() == "va".ToUpper() ||
                                cboExpression.Text.Trim().ToUpper() == "hoac".ToUpper())
                            {
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 3, 3).ToUpper() == "And".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 4)
                                            + " " + cboExpression.SelectedValue.ToString().Trim();
                                        txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 3)
                                            + " " + cboExpression.Text.Trim();                                    
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 2, 2).ToUpper() == "Or".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 3)
                                            + " " + cboExpression.SelectedValue.ToString().Trim();
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 5)
                                            + " " + cboExpression.Text.Trim();                                    
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")
                                {
                                    return;
                                }
                                else
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + " " + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + " " + cboExpression.SelectedValue.ToString().Trim();
                                }
                            }
                            else if (cboExpression.Text.Trim() == "")
                            {
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                                {

                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")
                                {                                   
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 1);
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 1);
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 3, 3).ToUpper() == "and".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 4);
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 3);
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 2, 2).ToUpper() == "or".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 3);
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 5);
                                }
                            }
                        }
                        //Neu count("(") > 0 dong thoi count("(")>count(")")
                        else if (GetCountChar(strBranchCon, "(") > GetCountChar(strBranchCon, ")"))
                        {
                            if(cboExpression.Text.Trim()==")")
                            {
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + cboExpression.SelectedValue.ToString().Trim();
                                }
                                //Neu chon "and, or, (" thi ko xu ly
                                else
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + cboExpression.SelectedValue.ToString().Trim();
                                }
                            }
                            else if(cboExpression.Text.Trim()=="(")
                            {
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                                {
                                    return;
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + cboExpression.SelectedValue.ToString().Trim(); 
                                }
                                else
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + " " + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon  + " " + cboExpression.SelectedValue.ToString().Trim(); 
                                }                                
                            }
                            else if (cboExpression.Text.Trim().ToUpper() == "va".ToUpper() ||
                                cboExpression.Text.Trim().ToUpper() == "hoac".ToUpper())
                            {
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")
                                {
                                    return;
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                                {
                                    txtComMess.Text = txtComMess.Text.Trim() + " " + cboExpression.Text.Trim();
                                    strBranchCon = strBranchCon + " " + cboExpression.SelectedValue.ToString().Trim(); 
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 3, 3).ToUpper() == "And".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 4)
                                            + " " + cboExpression.SelectedValue.ToString().Trim();
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 3)
                                        + " " + cboExpression.Text.Trim();
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 2, 2).ToUpper() == "Or".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 3)
                                            + " " + cboExpression.SelectedValue.ToString().Trim();
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 5)
                                        + " " + cboExpression.Text.Trim();
                                }   
                                    // them vao ngay 050908
                                else if (strBranchCon.Substring(strBranchCon.Length - 2, 2).ToUpper() != "OR" || strBranchCon.Substring(strBranchCon.Length - 2, 3).ToUpper() != "AND")
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length )
                                            + " " + cboExpression.SelectedValue.ToString().Trim();
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length)
                                        + " " + cboExpression.Text.Trim();
                                }
                            }
                            else if (cboExpression.Text.Trim() == "")
                            {
                                //
                                if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == ")")
                                { 

                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 1, 1) == "(")                                    
                                {                                  
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 1);
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 1);
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 3, 3).ToUpper() == "and".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 4);
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 3);
                                }
                                else if (strBranchCon.Trim().Substring(strBranchCon.Trim().Length - 2, 2).ToUpper() == "or".ToUpper())
                                {
                                    strBranchCon = strBranchCon.Trim().Substring(0, strBranchCon.Length - 3);
                                    txtComMess.Text = txtComMess.Text.Trim().Substring(0, txtComMess.Text.Trim().Length - 5);
                                }
                            }
                        }
                    }
                    //Chi co "(" thi cho them "("
                    else
                    {
                        if (cboExpression.Text.Trim() == "(")
                        {
                            txtComMess.Text = txtComMess.Text+ cboExpression.Text.Trim();
                            strBranchCon = strBranchCon + cboExpression.SelectedValue.ToString().Trim();
                        }
                    }
                }
                //Do dai cau dieu kien =0, chi cho chon "("
                else
                {                    
                    if (cboExpression.Text == "(")
                    {
                        txtComMess.Text = cboExpression.Text.Trim();
                        strBranchCon = cboExpression.SelectedValue.ToString().Trim();
                    }
                    else
                    {
                        txtComMess.Text = "";
                        strBranchCon = "";
                    }
                }
            }
        }

       private void cmdSave_Click(object sender, EventArgs e)
        {
            int intI = Check();
            if (intI < 0)
                return;
            else
            {
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show("Do you want to save data?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (DlgResult == DialogResult.Yes)
                {
                    //Check trung
                    if (objControl.IDIsExisting(txtCriteriaName.Text.Trim(), txtPriority.Text.Trim(), cboBranch1.Text.Trim()))
                    {
                        Common.ShowError("Criteria Name has already exist.", 3, MessageBoxButtons.OK);                        
                        txtCriteriaName.Text = "";
                        txtCriteriaName.Focus();
                        cmdsave1.Enabled = true;
                        return;
                    }
                    //check neu la chi nhanh trong danh sach
                    if (GetData.CheckExistBranch(cboBranch1.Text.Trim()))
                    {
                        objInfo.BRANCH = cboBranch1.Text.Trim().PadLeft(5,'0');
                    }
                    else
                    {
                        objInfo.BRANCH = cboBranch1.Text.Trim();
                    }
                    if (cboBranch1.Text.Trim() == "")
                        objInfo.BRANCH = "";


                    objInfo.KEY_WORD = txtKeyword.Text.Trim();
                    objInfo.MESSAGE = strMessage;// txtCriteriaMess.Text.Trim();
                    objInfo.PRIORITY =  txtPriority.Text.Trim();
                    objInfo.NAME = txtCriteriaName.Text.Trim();
                    objInfo.DESCPRIPTION = txtCriteriaMess.Text.Trim();
                    objInfo.DEPARTMENT = cboModule.Text.Trim();


                    if (objControl.AddSWIFT_BRANCH_ACTION(objInfo) > 0)
                    {
                        Common.ShowError("Data has saved successfully!", 1, MessageBoxButtons.OK);
                        #region lay bien de ghi log
                        DateTime dtDateLogin = DateTime.Now;
                        string strContent = "Swift - Auto module and branch criteria definition:";
                        int iLoglevel = 1;
                        string strWorked = "Insert";
                        string strTable = "SWIFT_BRANCH_ACTION";
                        string strOld_value = "";
                        string strNew_value = objInfo.KEY_WORD + "/" + objInfo.BRANCH + "/" + objInfo.MESSAGE + "/" + objInfo.PRIORITY + "/" + objInfo.NAME + "/" + objInfo.DESCPRIPTION + "/" + objInfo.DEPARTMENT;
                        #endregion
                        Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);
                        ClearText();
                        cmdClose_Click(sender, e);

                    }
                    else
                    {
                        Common.ShowError("Can not update database!", 3, MessageBoxButtons.OK);                        
                    }                   
                }
            }
           
        }

       /*---------------------------------------------------------------
       * Method           : Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
       * Muc dich         : Ham ghi log User dang nhap vao he thong
       * Tham so          : cac gia tri tuong ung voi bang User_msg_log
       * Tra ve           : 
       * Ngay tao         : 10/07/2008
       * Nguoi tao        : QuyND
       * Ngay cap nhat    : 10/07/2008
       * Nguoi cap nhat   : QuyND(Nguyen duc quy)
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


        ///////////////////////////////////////////////////////
        //Muc dich: 1.Kiem tra o txtKeyword, 
        //          2.Kiem tra xau Sql trong o txtCriteriaMess 
        //          co dung chuan sql        
        //Ngay sua: 01/08/2008        
        ///////////////////////////////////////////////////////
        private int Check()
        {            
            if (txtCriteriaName.Text.Length < 1)
            {
                Common.ShowError("CriteriaName is null", 3, MessageBoxButtons.OK);                
                txtCriteriaName.Focus();
                return -1;
            }
            if (txtPriority.Text.Length < 1)
            {
                Common.ShowError("Priority is null", 3, MessageBoxButtons.OK);                
                txtPriority.Focus();
                return -1;
            }


            if (txtCriteriaMess.Text.Length < 1)
            {
                Common.ShowError("Criteria Message is null", 3, MessageBoxButtons.OK);                
                txtCriteriaMess.Focus();
                return -1;
            }           
            //Kiem tra keyword
            if (txtKeyword.Text.Length < 1)
            {
                Common.ShowError("Keyword is null!", 3, MessageBoxButtons.OK);                 
                txtKeyword.Focus();
                return -1;
            }
            int iCheck = 0;
            //kiem tra keycode, message
            if (!objControl.CheckKeyword(txtKeyword.Text.ToString(),
                strMessage.ToString(), out strError, out iCheck))
            {
                Common.ShowError(strError, 3, MessageBoxButtons.OK);                  
                txtKeyword.Focus();
                return -1;
            }
            if (iCheck != 1)            
            {
                Common.ShowError("Criteria message is invalid", 3, MessageBoxButtons.OK);                 
                return -1;
            }
            return 1;
        }


      

        private void cmdClear_Click(object sender, EventArgs e)
        {
            strMessage = "";
            txtCriteriaMess.Text = "";
            txtKeyword.Text = "";
            strBrkeyword="";
            strSubselect = "";
        }


        private void cmdAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                int intCompare = -1;
                strBrkeyword=cboKeyword1.Text.Trim();

                string strcon = "";
                switch (cboSelectTable.Text)
                {
                    case "DUAL":
                        if (txtCriteriaMess.Text.Trim() == "")
                        {
                            strcon = "";
                        }
                        else
                            strcon = " And " + strMessage;

                        txtRecMess.Text = " Lay du lieu tai truong" + txtBranch.Text.Trim() + " dieu kien la truong " + txtField1.Text + " " + cboOperation1.Text.Trim() + cboFunction1.Text.Trim() + " " + (cboKeyword1.Text.Trim());
                        //txtCriteriaMess.Text = txtCriteriaMess.Text.Trim() + " " + txtCriteria1.Text.Trim();
                        strCriatia1 = "SELECT '$Par$' FROM DUAL";
                        txtRecMess.Text = strCriatia1;
                        strSubselect = " SELECT " + txtCriteria1.Text.Trim() + " FROM SWIFT_BRANCH_TEMP " + " Where Rownum =1 " + strcon + " ";
                        strMessage = strSubselect;
                        if (String.IsNullOrEmpty(txtKeyword.Text))
                            txtKeyword.Text = cboKeyword1.Text.Trim();
                        else
                        {
                            intCompare = txtKeyword.Text.IndexOf(cboKeyword1.Text.Trim());
                            if (intCompare < 0)
                                txtKeyword.Text = txtKeyword.Text.Trim() + "; " + cboKeyword1.Text.Trim();
                        }
                        txtCriteria1.Text = "";
                        break;
                    default:
                        txtRecMess.Text = " Lay du lieu tai truong" + txtBranch.Text.Trim() + " dieu kien la truong " + txtField1.Text + " " + cboOperation1.Text.Trim() + cboFunction1.Text.Trim() + " " + (cboKeyword1.Text.Trim());
                        //txtCriteriaMess.Text = txtCriteriaMess.Text.Trim() + " " + txtCriteria1.Text.Trim();
                        strCriatia1 = txtBranchMess.Text.Trim() + " Where " + txtCriteria1.Text.Trim();

                      
                        if (String.IsNullOrEmpty(txtKeyword.Text))
                            txtKeyword.Text = cboKeyword1.Text.Trim();
                        else
                        {
                            intCompare = txtKeyword.Text.IndexOf(cboKeyword1.Text.Trim());
                            if (intCompare < 0)
                                txtKeyword.Text = txtKeyword.Text.Trim() + "; " + cboKeyword1.Text.Trim();
                        }
                        txtCriteria1.Text = "";
                        //cboFunction1.Text = "";


                        //them ngay 26092008
                        // QuanLD
                        if (cboFunction1.SelectedValue.ToString().Substring(0, 5).ToUpper() == "INSTR")
                        {
                            string strFunction="";
                            strFunction = cboFunction1.SelectedValue.ToString().Replace("F", "'$Par$'");
                            strFunction = strFunction.Replace("$T", txtField1.Text);
                            strCriatia1 = txtBranchMess.Text.Trim() + " Where " + strFunction + " >1 and rownum =1";


                            if (String.IsNullOrEmpty(txtKeyword.Text))
                                txtKeyword.Text = cboKeyword1.Text.Trim();
                            else
                            {
                                intCompare = txtKeyword.Text.IndexOf(cboKeyword1.Text.Trim());
                                if (intCompare < 0)
                                    txtKeyword.Text = txtKeyword.Text.Trim() + "; " + cboKeyword1.Text.Trim();
                            }
                            txtCriteria1.Text = "";
                        }
                      
                            //het them
                        break;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClear2_Click(object sender, EventArgs e)
        {
            txtCriteria1.Text = "";
            strBrkeyword="";
            strSubselect = "";
            cboSelectTable.Text = "";
            cboFunction1.Text = "";
            cboOperation1.Text = "";
            strSubselect = "";
        }

     

        private void cmdComClear_Click(object sender, EventArgs e)
        {
            txtComMess.Text = "";
            strBranchCon = "";
        }

        private void cmdComAdd_Click(object sender, EventArgs e)
        {
            if (txtComMess.Text.Trim() == "")
            {
                return;
            }
            if (cboSelectTable.Text == "DUAL" && txtCriteriaMess.Text.Trim() != "")
            {
                if (strBranchCon.Trim() != "")
                {
                    strMessage = strSubselect + " AND " + strBranchCon;
                    txtCriteriaMess.Text = txtComMess.Text.Trim();
                }
                else
                {
                    strMessage = strSubselect + "  " + strBranchCon;
                    txtCriteriaMess.Text = txtComMess.Text.Trim();
                }
            }
            else
            {
                if (txtCriteriaMess.Text.Trim() != "")
                {
                    strMessage = strMessage + ";" + '\r' + '\n' + strBranchCon;
                    txtCriteriaMess.Text = txtCriteriaMess.Text + ";" + '\r' + '\n' + strBranchCon;// txtComMess.Text.Trim();
                    txtCriteriaMess.Text = txtCriteriaMess.Text.Trim() + " " + txtRecMess.Text.Trim();
                }
                else
                {
                    txtCriteriaMess.Text = txtComMess.Text.Trim();
                    strMessage = strBranchCon;
                }
            }            
            strBranchCon = "";
            txtComMess.Text = "";           
        }

        private void cmdRecAdd_Click(object sender, EventArgs e)
        {           
            if (txtCriteriaMess.Text.Trim() == "")
            {
                Common.ShowError("Please add common message first!", 3, MessageBoxButtons.OK);                
                return;
            }
            if (txtRecMess.Text.Trim() == "")
            { return; }           
            if (!string.IsNullOrEmpty(txtRecMess.Text))
            {
                if (txtCriteriaMess.Text.Trim() != "")
                {
                    if (cboSelectTable.Text.Trim() == "DUAL")
                    {                       
                        strMessage = strCriatia1 + ";" + ('\r') + ('\n') + strSubselect;
                        txtCriteriaMess.Text = strMessage;
                    }
                    else
                    {
                        strMessage = strCriatia1 + ";" + ('\r') + ('\n') + "SELECT " + cboKeyword1.Text.Trim() +
                        " FROM SWIFT_BRANCH_TEMP WHERE (" + strMessage.Trim() + ")";

                        txtCriteriaMess.Text = txtRecMess.Text.Trim() + ";" + ('\r') + ('\n') + "SELECT " + cboKeyword1.Text.Trim() +
                        " FROM SWIFT_BRANCH_TEMP WHERE (" + txtCriteriaMess.Text.Trim() + ")";
                    }
                }
                else
                {
                    if (strMessage.Trim() == "")
                    {
                        txtCriteriaMess.Text = txtRecMess.Text.Trim();
                        strMessage = "SELECT " + cboKeyword1.Text.Trim() +
                           " FROM SWIFT_BRANCH_TEMP";

                        txtCriteriaMess.Text = "SELECT " + cboKeyword1.Text.Trim() +
                        " FROM SWIFT_BRANCH_TEMP ";

                    }
                    else
                    {
                        txtCriteriaMess.Text = txtRecMess.Text.Trim();
                        strMessage = "SELECT " + cboKeyword1.Text.Trim() +
                           " FROM SWIFT_BRANCH_TEMP WHERE (" + strMessage.Trim() + ")";

                        txtCriteriaMess.Text = "SELECT " + cboKeyword1.Text.Trim() +
                        " FROM SWIFT_BRANCH_TEMP WHERE (" + txtCriteriaMess.Text.Trim() + ")";
                    }
                    
                }
            }
            else
            {
                if (txtCriteriaMess.Text.Trim() != "")
                {
                    strMessage = "SELECT " + cboKeyword1.Text.Trim() +
                    " FROM SWIFT_BRANCH_TEMP WHERE (" + strMessage.Trim() + ")";
                }
                
            }
            txtRecMess.Text = "";
            strCriatia1 = "";
            strSubselect = "";
            txtCriteria1.Text = "";
            cboFunction1.Text = "";
        }

        private void cmdComClear_Click_1(object sender, EventArgs e)
        {
            strBranchCon = "";
            txtComMess.Text = "";
            txtKeyword.Text = "";
        }

        private void cmdRecClear_Click(object sender, EventArgs e)
        {
            txtRecMess.Text = "";
            strCriatia1 = "";
            strBrkeyword="";
            strSubselect = "";
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
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);                
            }
        }

        private void cmdValidate_Click(object sender, EventArgs e)
        {
            int iCheck = 0;
            //Kiem tra keyword
            if (txtKeyword.Text.Length < 1)
            {
                Common.ShowError("Keyword is null", 3, MessageBoxButtons.OK);                
                txtKeyword.Focus();
                return ;
            }
            
            //////////////////////////////////////////
            //Muc dich: kiem tra keycode, message
            //Ngay tao: 01/08/2008
            //////////////////////////////////////////
            //if (!objControl.CheckKeyword(txtKeyword.Text.ToString(),
            //    txtCriteriaMess.Text.ToString(), out strError, out iCheck))
            if (!objControl.CheckKeyword(txtKeyword.Text.ToString(),
                strMessage.ToString(), out strError, out iCheck))
            {
                Common.ShowError(strError, 3, MessageBoxButtons.OK);                             
                return;
            }
            if (iCheck == 1)
            {
                Common.ShowError("Criteria message is valid", 1, MessageBoxButtons.OK);                   
            }
            else
            {
                Common.ShowError("Criteria message is valid", 3, MessageBoxButtons.OK);  
            }
        }

        private void txtValue_Leave(object sender, EventArgs e)
            
        {
            // bien luu lai cau mo ta cua dieu kien
            //strCriatia = "";

            string strTemp = "";
            strTemp = objAutovalue.GetSWIFT_AUTO_VALUE(txtValue.Text.Trim());
            strValue = txtValue.Text.Trim();
            if (cboKeyword.Text.Trim() == "")
                return;
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
                                    //Kiem tra neu Keyword la MSG_TYPE va Function, SubFunction la Length                                
                                    if (!string.IsNullOrEmpty(strRealMasg) && strRealMasg.ToUpper().IndexOf("MSG_TYPE") > 0 &&
                                        strRealMasg.ToUpper().IndexOf("LENGTH") >= 0)
                                    {
                                        strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( '" + strTemp.Trim() + "')";
                                        txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( '" + strValue.Trim() + "')";
                                    }
                                    else
                                    {
                                        strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( 'MT" + strTemp.Trim() + "')";
                                        txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( 'MT" + strValue.Trim() + "')";
                                    }
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
                                //Kiem tra neu Keyword la MSG_TYPE va Function, SubFunction la Length                                
                                if (!string.IsNullOrEmpty(strRealMasg) && strRealMasg.ToUpper().IndexOf("MSG_TYPE") > 0 &&
                                    strRealMasg.ToUpper().IndexOf("LENGTH") >= 0)
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( '" + strTemp.Trim() + "')";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( '" + strValue.Trim() + "')";
                                }
                                //Nguoc lai
                                else
                                {
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( 'MT" + strTemp.Trim() + "')";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( 'MT" + strValue.Trim() + "')";
                                }
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
                                    strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "( " + strTemp.Trim() + ")";
                                    txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "(" + strValue.Trim() + ")";
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
                                strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "(" + strTemp.Trim() + ")";
                                txtCriteria.Text = strView + " " + cboOperation.Text.Trim() + "( " + strValue.Trim() + ")";
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
                            strBranchValue = strRealMasg + " " + cboOperation.Text.Trim() + "(" + strTemp.Trim() + ")";
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

              

        private void txtValue_Validated(object sender, EventArgs e)
        {
            txtValue_Leave(sender, e);
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
                strView = strView + " ( " + cboSubFunction.Text.Trim() ;
                strView = GetCountNgoac(strView);
                txtCriteria.Text = strView;
                strRealMasg = strRealMasg.Replace("F", cboSubFunction.SelectedValue.ToString().Trim());
                //strComFunc = strComFunc.Replace("F", cboSubFunction.SelectedValue.ToString().Trim());
            }
            
        }


        private string  GetCountNgoac(string strIN)
        {
            int icount=0;
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

        ///////////////////////////////////////////////////////
        //Muc dich: Ham dem so ky tu strChar trong Xau
        //Ngay tao: 29/08/2008
        ///////////////////////////////////////////////////////
        private int GetCountChar(string strDoc, string strChar)
        {
            int icount = 0;
            for (int i = 0; i < strDoc.Length; i++)
            {
                if (strDoc.Substring(i, 1).ToUpper() == strChar.ToUpper())
                {
                    icount = icount + 1;
                }
            }
            return icount;
        }

        private string FilltxtCriteria()
        {
            string strReturn = "";
            
            try
            {
                if (txtCriteria.Text.Trim() == "")
                    ifncount = 0;

                txtCriteria.Text = txtCriteria.Text.TrimEnd(')');
                // dem so ham co trong chuoi de tang ky tu ")"
                if (cboSubFunction.Text.Trim() != "")
                {
                    ifncount = ifncount + 1;
                   
                }
                else txtCriteria.Text = "";
                strReturn = txtCriteria.Text + " ( " + cboKeyword.Text ;
                for (int i = 0; i < ifncount; i++ )
                {
                    strReturn = strReturn + ")";
                }
                    return strReturn;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); return "";
            }            
        }

        

        private void cboSelectTable_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtTableDesc.Text = cboSelectTable.Text;
            cboKeyword1.Text = "";
            cboOperation1.Text = "";
            switch (txtTableDesc.Text)
            {
                case "SWIFT_BR_AUTO":
                    txtField1.Text = "TMTREF";
                    //txtKeyword.ReadOnly = true;
                    txtBranch.Text = "TMPRBR";
                    cboOperation1.Text = "";
                    cboOperation1.Enabled = true;
                    break;
                case "SWIFT_RMBR_AUTO":
                    txtField1.Text = "ORG_BRAN";
                    //txtKeyword.ReadOnly = true;
                    txtBranch.Text = "RECEIVER_BRAN";

                    cboOperation1.Text = "";
                    cboOperation1.Enabled = true;
                    break;
                case "DUAL":
                    txtField1.Text = "";
                    //txtKeyword.ReadOnly = true;
                    txtBranch.Text = "";
                    cboOperation1.Text = "";
                    cboOperation1.Enabled = false;
                    break;
            }
            txtBranchMess.Text = "SELECT " + txtBranch.Text + "  FROM " +txtTableDesc.Text ;
            }

        private void cboBranch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCboTable();
        }


        //////////////////////////////////////////
        //Muc dich: khi nhan phim ESC
        //Nguoi tao: Huypq
        //Ngay tao: 01/08/2008
        //////////////////////////////////////////
        private void frmSWIWBrAuto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                if (MessageBox.Show("Do you want to exit?", Common.sCaption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }

        }
        

        private void frmSWIWBrAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
            }
            catch
            {
            }
        }

        private void frmSWIWBrAuto_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSWIWBrAuto_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        //////////////////////////////////////////

    }
}
