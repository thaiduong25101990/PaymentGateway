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
    public partial class frmSWIWAutoValue :frmBasedata
    {
        #region khai bao cac lop va cac bien
        ALLCODEInfo objAllcode = new ALLCODEInfo();
        ALLCODEController objCtrlAllcode = new ALLCODEController();
        DATABASE_CLEANController objctrlDatabase = new DATABASE_CLEANController();
        SWIFT_AUTO_VALUE_Info objValue = new SWIFT_AUTO_VALUE_Info();
        SWIFT_AUTO_VALUEController objCtrlValue = new SWIFT_AUTO_VALUEController();
        private static bool iSelect = false;
        private static string sreWHEREE;        
        public string strCriate="";
        public string strKeycode="";
        public int iID = 0;
        private bool isEdit = false;
        #endregion

        public frmSWIWAutoValue()
        {
            InitializeComponent();
        }

        private void frmSWIWAutoValue_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                cmdV2.Enabled = false;
                iSelect = false;
                Getcombobox();
                if (strKeycode.Trim() != "")
                {
                    cbKeyword.Text = strKeycode;
                    txtCriteriaMessage.Text = strCriate;
                    isEdit = true;
                    cmdSave.Enabled = true;
                }
                else
                    isEdit = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
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



        // ham lay du lieu len cac combobox
        private void Getcombobox()
        {
            try
            {
                //lay du lieu len combobox cbKeyword
                string strCdname = "VKEYWORD";
                string strGwtype = "SWIFT";
                GetCombo(cbKeyword, strGwtype, strCdname, "DESCRIPTION", "CONTENT");
                GetCombo(cbTable, strGwtype, "VTABLE", "DESCRIPTION", "CONTENT");
                GetCombo(cbOpera, strGwtype, "OPERATION", "CONTENT", "CONTENT");
                GetCombo(cbExpression, "SYSTEM", "EXPRESSION", "CONTENT", "CONTENT");             

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //click vao combobox cbTable
        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                if (cbTable.SelectedIndex < 0 || cbTable.Text == "System.Data.DataView" || cbTable.Text == "System.Data.DataRowView")
                    return;

                txtTableDescription.Text = cbTable.SelectedValue.ToString();
                string strTableName = cbTable.Text;
                GetField(strTableName);
                GetField1(strTableName);
                Create_select(cbTable.Text, cbField.Text);
                Getcomment(cbTable.Text);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham lay ra y nghia cua bang(mo ta cua bang la gi)
        private void Getcomment(string strTableName)
        {
            try
            {
                if (cbTable.SelectedIndex < 1 || cbTable.Text == "System.Data.DataView" || cbTable.Text == "System.Data.DataRowView")
                    return;
                DataSet datCom_table = new DataSet();
                datCom_table = objctrlDatabase.GetComment_table(strTableName);
                txtTableDescription.Text = datCom_table.Tables[0].Rows[0]["COMMENTS"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //lay du lieu la ten cac truong cua bang
        private void GetField1(string strTab)
        {
            try
            {
                DataSet datColumn3 = new DataSet();
                datColumn3 = objctrlDatabase.GetColumns_table(strTab);
                cbField1.DataSource = datColumn3.Tables[0];
                cbField1.DisplayMember = "COLUMN_NAME";
                cbField1.ValueMember = "COMMENTS";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //lay du lieu la ten cac truong cua bang
        private void GetField(string strTable)
        {
            try
            {
                DataSet datColumn = new DataSet();
                datColumn = objctrlDatabase.GetColumns_table(strTable);
                cbField.DataSource = datColumn.Tables[0];
                cbField.DisplayMember = "COLUMN_NAME";
                cbField.ValueMember = "COMMENTS";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //click vao combobox cbField1 lay gia tri tuong uong ra combobox txtValue
        private void cbField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbField1.SelectedIndex < 0 || cbField1.Text == "System.Data.DataView" || cbField1.Text == "System.Data.DataRowView") 
                    return;

                txtFieldDescription1.Text = cbField1.Text.ToString();
                string strTableName = cbTable.Text;
                string strColumnName = cbField1.Text;
                DataSet datValue = new DataSet();
                datValue = objctrlDatabase.GetValue_colum(strTableName, strColumnName);
                cbValue.Items.Clear();
                cbValue.Items.Add("");

                for (int i = 0; i < datValue.Tables[0].Rows.Count; i++)
                {
                    cbValue.Items.Add(datValue.Tables[0].Rows[i][strColumnName].ToString());
                }
                //    cbValue.DataSource = datValue.Tables[0];
                //cbValue.DisplayMember = strColumnName;
                if (iSelect == true)
                {
                    if (cbValue.Text != "")
                    {
                        txtCriteria1.Text = sreWHEREE + "  " + cbExpression.Text + " " + cbField1.Text + " " + cbOpera.Text + " '" + cbValue.Text + "'";
                       // Where(txtCriteria1.Text);
                    }
                }
                else
                {
                    if (cbValue.Text != "")
                    {
                        Create_where(cbField1.Text, cbOpera.Text, cbValue.Text);
                    }                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham thuc hien tao cau select 
        private void Create_select(string strTable,string strColumn)
        {
            try
            {
                txtCriteria.Text=" Select  " + strColumn + "  From  " + strTable +"";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //click vao combobox column field
        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbField.SelectedIndex < 0 || cbField.Text == "System.Data.DataView" )
                    return;

                Create_select(cbTable.Text, cbField.Text);
                txtFieldDescription.Text = cbField.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham tao menh de where
        private void Create_where(string strColum,string strToan,string strValue)
        {
            try
            {
                txtCriteria1.Text = "Where   " + strColum + "  " + strToan + "   '" + strValue + "' ";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //click vao combobox tao them menh de where
        private void cbExpression_SelectedIndexChanged(object sender, EventArgs e)
        {
            sreWHEREE = txtCriteria1.Text;
            iSelect = true;
            try
            {
                if (cbExpression.SelectedIndex < 0 || cbExpression.Text == "System.Data.DataView" || cbExpression.Text == "System.Data.DataRowView")
                    return;

                string strWhere = txtCriteria1.Text;
                Where(strWhere);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Where(string pWhere)
        {

            try
            {
                if (pWhere != "")//otex co du lieu
                {
                    txtCriteria1.Text = pWhere + " " + cbExpression.Text + "";
                }
                else
                {
                    if (cbValue.Text != "")
                    {
                        txtCriteria1.Text = " Where " + cbField1.Text + " " + cbOpera.Text + " " + cbValue.Text + "";
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cbOpera_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strValue = "";
            strValue = cbValue.Text;
            try
            {
                if (cbOpera.Text == "Like" || cbOpera.Text == "Not Like")
                { strValue = "%" + strValue + "%"; }


                if (cbOpera.SelectedIndex < 0 || cbOpera.Text == "System.Data.DataView" || cbOpera.Text == "System.Data.DataRowView")
                    return;

                if (iSelect == true)
                {
                    if (cbValue.Text.Trim() == "")
                    { txtCriteria1.Text = sreWHEREE + "  " + cbExpression.Text + " " + cbField1.Text + " " + cbOpera.Text ; }
                    else
                    { txtCriteria1.Text = sreWHEREE + "  " + cbExpression.Text + " " + cbField1.Text + " " + cbOpera.Text + " ('" + strValue + "')"; }
                    
                }
                else
                {
                    if (cbValue.Text != "")
                    {
                        Create_where(cbField1.Text, cbOpera.Text, cbValue.Text);
                    }                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //cho xuong dieu kien
        private void cmdV_Click(object sender, EventArgs e)
        {
            cmdV2.Enabled = true;
            cmdSave.Enabled = true;
            try
            {
                txtCriteriaMessage.Text = txtCriteria.Text;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdV2_Click(object sender, EventArgs e)
        {
            cmdV2.Enabled = false;
            try
            {
                if (txtCriteriaMessage.Text.IndexOf("Where") > 0)
                {
                    txtCriteriaMessage.Text = txtCriteriaMessage.Text  + txtCriteria1.Text;
                }
                else
                { txtCriteriaMessage.Text = txtCriteriaMessage.Text + " Where " + txtCriteria1.Text; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtCriteria.Text = "";
                cbTable.Text = "";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClear1_Click(object sender, EventArgs e)
        {
            try
            {
                txtCriteria1.Text = "";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClear3_Click(object sender, EventArgs e)
        {
            try
            {
                txtCriteriaMessage.Text = "";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cbKeyword_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbKeyword.SelectedIndex < 0 || cbKeyword.Text == "System.Data.DataView" || cbKeyword.Text == "System.Data.DataRowView")
                    return;
                txtKeyword.Text = cbKeyword.Text;
                txtKeywordDescription.Text = cbKeyword.SelectedValue.ToString();
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
        // ham kiem tra cau lenh truy van cos dung khong
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //kiem tr cau lenh co dung hay khong
                if (objctrlDatabase.Check_Query(txtCriteriaMessage.Text) == -1)
                {
                    Common.ShowError("Query language is not exact!", 3, MessageBoxButtons.OK);                    
                }
                else
                {
                    Common.ShowError("Query language is exact!", 3, MessageBoxButtons.OK);                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //su kien cua combobox value
        private void cbValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (iSelect == true)
                {
                    string strValue = "";
                    strValue = cbValue.Text;
            
                if (cbOpera.Text == "Like" || cbOpera.Text == "Not Like")
                { strValue = "%" + strValue + "%"; }

                    if (cbValue.Text == "")
                    {
                        txtCriteria1.Text = txtCriteria1.Text;
                    }
                    else
                    {
                        txtCriteria1.Text = sreWHEREE + "  " + cbExpression.Text + " " + cbField1.Text + " " + cbOpera.Text + " ('" + strValue + "')";
                        //Where1(txtCriteria1.Text);
                    }
                }
                else
                {
                    if (cbValue.Text == "")
                    {
                        txtCriteria1.Text = "";
                    }
                    else
                    {
                        Create_where(cbField1.Text, cbOpera.Text, cbValue.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Where1(string strWhere)
        {
            try
            {
                txtCriteria1.Text = strWhere + " " + cbField1.Text + " " + cbOpera.Text + " " + cbValue.Text + "";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham ghi du lieu vao trong DB 
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 0)
                {
                    return;
                }

                //string Msg = "Do you really want to Save?";
                string title = Common.sCaption;
                int intI = 0;
                if (CheckKeycode(cbKeyword.Text.Trim(), iID) == false)
                {
                    Common.ShowError("Keyword is duplicate!", 3, MessageBoxButtons.OK);                    
                    return;
                }
                //DialogResult DlgResult = new DialogResult();               
                {
                    objValue.KEY_WORD = txtKeyword.Text;
                    objValue.MESSAGE = txtCriteriaMessage.Text;
                    if (isEdit)
                    {
                        intI = objCtrlValue.UpdateSWIFT_AUTO_VALUE(objValue);
                        isEdit = false;
                    }
                    else
                    { intI = objCtrlValue.AddSWIFT_AUTO_VALUE(objValue); }

                    if (intI < 0)
                    {
                        Common.ShowError("Can not save data!", 2, MessageBoxButtons.OK);                            
                        cmdSave.Enabled = true;
                    }
                    else
                    {
                        Common.ShowError("Save data successfuly!", 1, MessageBoxButtons.OK);                            
                        ClearText();
                    }                    
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); return;
            }
        }

        private void ClearText()
        {
            txtCriteria.Text = "";
            txtCriteria1.Text = "";
            txtCriteriaMessage.Text = "";
            txtKeyword.Text = "";
            txtKeywordDescription.Text = "";
            txtTableDescription.Text = "";
        }

        private bool CheckKeycode(string strKeycode, int PMiID) 
        {
            try
            {
                return objCtrlValue.CheckCode(strKeycode, PMiID);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); return false;
            }
        }

        private void frmSWIWAutoValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        private void frmSWIWAutoValue_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmSWIWAutoValue_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
      

    }
}
