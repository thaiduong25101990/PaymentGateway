using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using BR.BRLib;
using System.Text.RegularExpressions;


namespace BR.BRSWIFT
{
    public class clsDatagridviews
    {

        public static String SQL_ADVANCE(DataGridView datDieukien)
        {
            try
            {
                string pWhere = "";
                if (datDieukien.Rows.Count > 0)
                {
                    for (int i = 0; i < datDieukien.Rows.Count; i++)
                    {
                        if (datDieukien.Rows[i].Cells["CheckBox"].Value != null)// hang duoc chon
                        {
                            if (datDieukien.Rows[i].Cells["CheckBox"].Value.ToString() == "True")
                            {
                                if (pWhere == "") { pWhere = datDieukien.Rows[i].Cells["VALUE"].Value.ToString(); }
                                else { pWhere = pWhere + " and " + datDieukien.Rows[i].Cells["VALUE"].Value.ToString(); }
                            }
                        }
                    }
                    return pWhere = "  Where " + pWhere;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static String SQL_ADVANCE_MANUAL(DataGridView datDieukien)
        {
            try
            {
                string pWhere = "";
                if (datDieukien.Rows.Count > 0)
                {
                    for (int i = 0; i < datDieukien.Rows.Count; i++)
                    {
                        if (datDieukien.Rows[i].Cells["CheckBox"].Value != null)// hang duoc chon
                        {
                            if (datDieukien.Rows[i].Cells["CheckBox"].Value.ToString() == "True")
                            {
                                string m_vValue = datDieukien.Rows[i].Cells["VALUE"].Value.ToString();
                                String[] N = m_vValue.Split(new String[] { " " }, StringSplitOptions.None);//cat chuoi
                                if (N[0].ToString() == "PROCESSSTS")
                                {
                                    if (pWhere.Trim() == "")
                                    {
                                        pWhere = " SWIFT_MSG_CONTENT.QUERY_ID in (Select QUERY_ID from SWIFT_PROCESS Where  NEW_PROCESS =  " + N[2].ToString().Trim() + ")  ";
                                    }
                                    else
                                    {
                                        pWhere = pWhere + " and  SWIFT_MSG_CONTENT.QUERY_ID in (Select QUERY_ID from SWIFT_PROCESS Where  NEW_PROCESS =  '" + N[2].ToString().Trim() + "')  ";
                                    }
                                }
                                else
                                {
                                    if (pWhere == "") { pWhere = datDieukien.Rows[i].Cells["VALUE"].Value.ToString(); }
                                    else { pWhere = pWhere + " and " + datDieukien.Rows[i].Cells["VALUE"].Value.ToString(); }
                                }
                            }
                        }
                    }
                    return pWhere = "  Where " + pWhere;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView Getdata_Swift(DateTime _datefrom,DateTime _dateto, string pWhere, DataGridView _dtGridviews)
        {
            try
            { 

                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.SWIFT_CONTENT(_datefrom, _dateto, pWhere, out _dsContent);
                _dsContent.Tables[1].Merge(_dsContent.Tables[2]);
                _dsContent.Tables[0].Merge(_dsContent.Tables[1]);          
                _dtGridviews.DataSource = _dsContent.Tables[0];                
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[3]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView SWIFT_MSG_CONTENT_SEARCH_ADVANCE(string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.SWIFT_CONTENT_SEARCH_ADVANCE(pWhere, out _dsContent);
                _dsContent.Tables[1].Merge(_dsContent.Tables[2]);
                _dsContent.Tables[0].Merge(_dsContent.Tables[1]);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[3]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ham lay dien cho form resend lai dien trong ngay cua bang content
        public static DataGridView LOAD_DATA_RESEND(string pWhere, int pTeller, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.LOAD_DATA_RESEND(pWhere, pTeller, out _dsContent);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[1]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay dien cho form resend lai dien trong ngay cua bang content
        public static DataGridView SEARCH_DATA_RESEND(DateTime datefrom,DateTime dateto, string pWhere, int pTeller, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.SEARCH_DATA_RESEND(datefrom, dateto,pWhere, pTeller, out _dsContent);
                _dsContent.Tables[1].Merge(_dsContent.Tables[2]);
                _dsContent.Tables[0].Merge(_dsContent.Tables[1]);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[3]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView MESSAGE_CONTENT(string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.MESSAGE_CONTENT(pWhere, out _dsContent);                
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[1]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView MESSAGE_CONTENT_INWARD(string pWhere, int pTeller, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.MESSAGE_CONTENT_INWARD(pWhere, pTeller, out _dsContent);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[1]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



        public static DataGridView MESSAGE_CONTENT_DATE(DateTime datefrom, DateTime dateto, string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.MESSAGE_CONTENT_DATE(datefrom, dateto, pWhere, out _dsContent);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[1]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        public static DataGridView SEARCH_DATA_MANUAL_NORMAL(string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.SEARCH_DATA_MANUAL_NORMAL(pWhere, out _dsContent);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[1]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



        public static DataGridView MESSAGE_CONTENT_INSWARD_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, int pTeller, DataGridView _dtGridviews)
        {
            try
            {
                SWIFT_MSG_CONTENTController ControlConten = new SWIFT_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.MESSAGE_CONTENT_INSWARD_SEARCH(datefrom, dateto, pWhere,pTeller, out _dsContent);
                _dtGridviews.DataSource = _dsContent.Tables[0];
                //----------------------------------------------------------------------------
                clsEdit_columns_datagridview.Edit_Columns_Datagrid(_dtGridviews, _dsContent.Tables[1]);//ok
                FomatGrid.Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------
                Add_Combobox(_dtGridviews);
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                _dtGridviews.Columns["Tabl"].Visible = false;
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        //ham ad them combobox de lamf mask cho cac cot khac
        public static void Add_Combobox(DataGridView _dtGrid)
        {
            try
            {
                //add cot processsts-------------------------------------------------------
                DataTable _dt = new DataTable();
                _dt = clsStatus.Get_Status();
                DataGridViewComboBoxColumn _cbProcesssts = new DataGridViewComboBoxColumn();
                _cbProcesssts.Name = "STATUS_NAME";
                _cbProcesssts.HeaderText = "STATUS";
                _cbProcesssts.DataSource = _dt;
                _cbProcesssts.DataPropertyName = "STATUS";              
                _cbProcesssts.ValueMember = "STATUS";
                _cbProcesssts.DisplayMember = "NAME";
                _cbProcesssts.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["STATUS"].DisplayIndex, _cbProcesssts);
                _dtGrid.Columns["STATUS"].Visible = false;               
                //add cot Error_code--------------------------------------------------------
                DataTable _dtE = new DataTable();
                _dtE = clsStatus.Get_Error_code();
                DataGridViewComboBoxColumn _cbError_code = new DataGridViewComboBoxColumn();
                _cbError_code.Name = "ERROR_CODE_NAME";
                _cbError_code.HeaderText = "ERROR_CODE";
                _cbError_code.DataSource = _dtE;
                _cbError_code.DataPropertyName = "ERR_CODE";
                _cbError_code.ValueMember = "ERROR_CODE";
                _cbError_code.DisplayMember = "NAME";
                _cbError_code.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["ERR_CODE"].DisplayIndex, _cbError_code);
                _dtGrid.Columns["ERR_CODE"].Visible = false;
                ////--------------------------------------------------------------
                DataSet _dts = new DataSet();
                _dts = clsStatus.Get_Processsts_Swmsts();
                DataGridViewComboBoxColumn _cbProce_code = new DataGridViewComboBoxColumn();
                _cbProce_code.Name = "PROCESSSTS_NAME";
                _cbProce_code.HeaderText = "PROCESS STATUS";
                _cbProce_code.DataSource = _dts.Tables[1];
                _cbProce_code.DataPropertyName = "PROCESSSTS";
                _cbProce_code.ValueMember = "CDVAL";
                _cbProce_code.DisplayMember = "CONTENT";
                _cbProce_code.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["PROCESSSTS"].DisplayIndex, _cbProce_code);
                _dtGrid.Columns["PROCESSSTS"].Visible = false;
                //--------------------------------------------------------------
                DataGridViewComboBoxColumn _cbSwms_code = new DataGridViewComboBoxColumn();
                _cbSwms_code.Name = "SWMSTS_NAME";
                _cbSwms_code.HeaderText = "MESSAGE STATUS";
                _cbSwms_code.DataSource = _dts.Tables[0];
                _cbSwms_code.DataPropertyName = "SWMSTS";
                _cbSwms_code.ValueMember = "CDVAL";
                _cbSwms_code.DisplayMember = "CONTENT";
                _cbSwms_code.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["SWMSTS"].DisplayIndex, _cbSwms_code);
                _dtGrid.Columns["SWMSTS"].Visible = false;                
                _dtGrid.Columns["VALUE_DATE"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //ham xoa dong khi truyen mot datagrid vao luoi
        public static void Remove_Rows(DataGridView _dtgr)
        {
            try
            {
                int m = 0;
                while (m < _dtgr.Rows.Count)
                {
                    if (_dtgr.Rows[m].Cells[0].Value.ToString() == "False")// hang duoc chon
                    {
                        _dtgr.Rows.RemoveAt(m);//xoa dong duoc chon
                    }
                    else
                    {
                        m = m + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //ham kiem tra xem co dong nao duoc check khong
        public static bool Check_Select(DataGridView _dtgrv)
        {
            try
            {
                bool bCheck = false;            
                int f = 0;
                while (f < _dtgrv.Rows.Count)
                {
                    if (_dtgrv.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtgrv.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            bCheck = true;
                            break;
                        }
                    }                    
                    f = f + 1;
                }
                return bCheck;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }


        public static void Views_message(DataGridView _dtgrve,Form _form)
        {
            try
            {
                frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                int f = 0;
                int d = 0;
                while (d < _dtgrve.Rows.Count)
                {
                    if (_dtgrve.Rows[d].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtgrve.Rows[d].Cells[0].Value.ToString() == "True")
                        {
                            //----------------------------------------------------------------------------------
                            frmSWIFT.strMSG_ID = _dtgrve.Rows[d].Cells["MSG_ID"].Value.ToString();                            
                            //----------------------------------------------------------------------------------
                            frmSWIFT.isfrmSwiftMsgList = true;
                            frmSWIFT.ShowDialog();
                        }
                    }
                    _dtgrve.Rows[d].Cells[0].Value = null;
                    if (frmSWIFT.bIsCloseForm == true)
                    {
                        f = _dtgrve.Rows.Count;
                        int p = 0;
                        while (p < _dtgrve.Rows.Count)
                        {
                            _dtgrve.Rows[p].Cells[0].Value = null;
                            p = p + 1;
                        }
                        return;
                    }
                    else
                    {
                        d = d + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void CellDoubleClick(DataGridView _dtgrve, Form _form,int iRow)
        {
            try
            {
                frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                int f = 0;
                frmSWIFT.strMSG_ID = _dtgrve.Rows[iRow].Cells["MSG_ID"].Value.ToString();                
                frmSWIFT.isfrmSwiftMsgList = true;
                frmSWIFT.ShowDialog();
                //----------------------------------------------------------------------------------
                _dtgrve.Rows[iRow].Cells[0].Value = null;
                if (frmSWIFT.bIsCloseForm == true)
                {
                    f = _dtgrve.Rows.Count;
                    int p = 0;
                    while (p < _dtgrve.Rows.Count)
                    {
                        _dtgrve.Rows[p].Cells[0].Value = null;
                        p = p + 1;
                    }
                    return;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static void CellDoubleClick(DataGridView _dtGrvew,int _iRows)
        {
            try
            {
                frmSwiftMsgInfo frmSWIFT = new frmSwiftMsgInfo();
                //-----------------------------------------------------------------------------------
                frmSWIFT.strMSG_ID = _dtGrvew.Rows[_iRows].Cells["MSG_ID"].Value.ToString();                
                //-----------------------------------------------------------------------------------
                frmSWIFT.isfrmSwiftMsgList = true;
                frmSWIFT.ShowDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
        }
        //ham tra ra menh de where cho search normal
        public static String Search_Normal(GroupBox _gr, string strBRANCH_B)
        {
            try
            {
                string Where = "";
                string Where_return = "";
                clsCheckInput clsCheck = new clsCheckInput();
                string strSender = ""; string strProcess_Status = ""; string strResource = "";
                string strRece = ""; string strRefno = ""; string strAmount = ""; string strCurrency = "";
                string strDepartment = ""; string strMsg_dire = ""; string strAck_nack = ""; string strOsn = "";
                string strStatus = ""; string strMsg_type = "";
                int k = 0;
                while (k < _gr.Controls.Count)
                {
                    #region Neu la textbox-----------------------------------------------------------------------
                    if (_gr.Controls[k] is TextBox)
                    {
                        if (_gr.Controls[k].Name == "txtsender")
                        {
                            if (Regex.IsMatch(_gr.Controls[k].Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) = lpad('" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "',5,'0')"; }
                            }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtreceiver")
                        {
                            if (Regex.IsMatch(_gr.Controls[k].Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and     upper(Trim(" + strBRANCH_B + ")) = lpad('" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "',5,'0')"; }
                            }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and     upper(Trim(" + strBRANCH_B + ")) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtrefno")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strRefno = ""; } else { strRefno = " and    upper(Trim(FIELD20)) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtAmount")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") {strAmount = "";}
                            else
                            {
                                if (Regex.IsMatch(_gr.Controls[k].Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                {
                                    String[] M = _gr.Controls[k].Text.Trim().Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                                    string strAM = M[1];
                                    if (M[1].Trim() == "00"){strAmount = " and   AMOUNT = " + M[0].Trim() + "." + M[1] + ""; }
                                    else
                                    {
                                        strAmount = " and   AMOUNT = '" + _gr.Controls[k].Text.Trim().Replace(",", "") + "'";
                                    }
                                }
                                else
                                {
                                    strAmount = " and   trim(AMOUNT) = '" + _gr.Controls[k].Text.Replace("''", "").Replace("'", "") + "'";
                                }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtMsg_type")
                        {
                            string strMsg;
                            if (_gr.Controls[k].Text.Trim().Length == 5)
                            { strMsg = _gr.Controls[k].Text.Trim().Substring(_gr.Controls[k].Text.Trim().Length - 3, 3); }
                            else { strMsg = "MT" + _gr.Controls[k].Text.Trim(); }
                            if (_gr.Controls[k].Text.Trim() == "") { strMsg_type = ""; }
                            else { strMsg_type = " and   upper(Trim(MSG_TYPE)) = '" + strMsg.ToUpper().Replace("''", "").Replace("'", "") + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtOSN")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strOsn = ""; } else { strOsn = " and    upper(Trim(OSN)) = '" + _gr.Controls[k].Text.Trim().ToUpper().Replace("''", "").Replace("'", "") + "' "; }
                        }
                    }
                    #endregion

                    #region Neu la combobox--------------------------------------------------------------------
                    else if (_gr.Controls[k] is ComboBox)
                    {
                        ComboBox cbBox = (ComboBox)_gr.Controls[k];
                        if (_gr.Controls[k].Name == "cbCurrency")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strCurrency = ""; } else { strCurrency = " and   upper(Trim(CCYCD)) = '" + _gr.Controls[k].Text.Trim().ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbdepartment")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strDepartment = ""; } else { strDepartment = " and   upper(Trim(DEPARTMENT)) = '" + _gr.Controls[k].Text.Trim().ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbmsg_direction")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strMsg_dire = ""; } else { strMsg_dire = " and   upper(Trim(MSG_DIRECTION)) = '" + _gr.Controls[k].Text.Trim().ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbStatus")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strStatus = ""; }
                            else
                            {
                                strStatus = " and   upper(Trim(STATUS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cboResource")
                        {
                            if (_gr.Controls[k].Text.Trim() == "ALL") { strResource = ""; }
                            else
                            {
                                strResource = " and   upper(Trim(MSG_SRC)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbMsg_status")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strAck_nack = ""; }
                            else
                            {
                                strAck_nack = " and   upper(Trim(SWMSTS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbProcess_Status")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strProcess_Status = ""; }
                            else if (_gr.Controls[k].Text.Trim() == "") { strProcess_Status = " and   upper(Trim(PROCESSSTS)) is null"; }
                            else
                            {
                                strProcess_Status = " and   upper(Trim(PROCESSSTS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                    }
                    #endregion
                    k = k + 1;
                }
                Where = strSender + strRece + strRefno + strAmount + strCurrency + strDepartment + strMsg_dire + strStatus + strAck_nack + strOsn + strMsg_type + strResource + strProcess_Status;
                if (Where.Trim() == "")
                {
                    Where_return = "";
                }
                else if (Where.Trim() != "")
                {
                    for (int i = 0; i <= Where.Length; i = i + 1)
                    {
                        string pStart = Where.Substring(i, 1);
                        if (pStart == "d")
                        { Where = Where.Substring(i + 1); break; }
                    }
                    Where_return = "Where " + Where;
                }

                return Where_return; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static String Search_Normal_Process(GroupBox _gr)
        {
            try
            {
                string Where = "";
                string Where_return = "";
                clsCheckInput clsCheck = new clsCheckInput();
                string strSender = ""; string strProcess_Status = ""; string strResource = "";
                string strRece = ""; string strRefno = ""; string strAmount = ""; string strCurrency = "";
                string strDepartment = ""; string strMsg_dire = ""; string strAck_nack = ""; string strOsn = "";
                string strStatus = ""; string strMsg_type = "";
                int k = 0;
                while (k < _gr.Controls.Count)
                {
                    #region Neu la textbox-----------------------------------------------------------------------
                    if (_gr.Controls[k] is TextBox)
                    {
                        if (_gr.Controls[k].Name == "txtsender")
                        {
                            if (Regex.IsMatch(_gr.Controls[k].Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) = lpad('" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "',5,'0')"; }
                            }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtreceiver")
                        {
                            if (Regex.IsMatch(_gr.Controls[k].Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and     upper(Trim(BRANCH_B)) = lpad('" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "',5,'0')"; }
                            }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and     upper(Trim(BRANCH_B)) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtrefno")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strRefno = ""; } else { strRefno = " and    upper(Trim(FIELD20)) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtAmount")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strAmount = ""; }
                            else
                            {
                                if (Regex.IsMatch(_gr.Controls[k].Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                {
                                    String[] M = _gr.Controls[k].Text.Trim().Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                                    string strAM = M[1];
                                    if (M[1].Trim() == "00") { strAmount = " and   AMOUNT = " + M[0].Trim() + "." + M[1] + ""; }
                                    else
                                    {
                                        strAmount = " and   AMOUNT = '" + _gr.Controls[k].Text.Trim().Replace(",", "") + "'";
                                    }
                                }
                                else
                                {
                                    strAmount = " and   trim(AMOUNT) = '" + _gr.Controls[k].Text.Replace("''", "").Replace("'", "") + "'";
                                }
                            }
                        }                        
                        else if (_gr.Controls[k].Name == "txtMsg_type")
                        {
                            string strMsg;
                            if (_gr.Controls[k].Text.Trim().Length == 5)
                            { strMsg = _gr.Controls[k].Text.Trim().Substring(_gr.Controls[k].Text.Trim().Length - 3, 3); }
                            else { strMsg = "MT" + _gr.Controls[k].Text.Trim(); }
                            if (_gr.Controls[k].Text.Trim() == "") { strMsg_type = ""; }
                            else { strMsg_type = " and   upper(Trim(MSG_TYPE)) = '" + strMsg.ToUpper().Replace("''", "").Replace("'", "") + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtOSN")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strOsn = ""; } else { strOsn = " and    upper(Trim(OSN)) = '" + _gr.Controls[k].Text.Trim().ToUpper().Replace("''", "").Replace("'", "") + "' "; }
                        }
                    }
                    #endregion

                    #region Neu la combobox--------------------------------------------------------------------
                    else if (_gr.Controls[k] is ComboBox)
                    {
                        ComboBox cbBox = (ComboBox)_gr.Controls[k];
                        if (_gr.Controls[k].Name == "cbCurrency")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strCurrency = ""; } else { strCurrency = " and   upper(Trim(CCYCD)) = '" + _gr.Controls[k].Text.Trim().ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbdepartment")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strDepartment = ""; } else { strDepartment = " and   upper(Trim(DEPARTMENT)) = '" + _gr.Controls[k].Text.Trim().ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbmsg_direction")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strMsg_dire = ""; } else { strMsg_dire = " and   upper(Trim(MSG_DIRECTION)) = '" + _gr.Controls[k].Text.Trim().ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbStatus")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strStatus = ""; }
                            else
                            {
                                strStatus = " and   upper(Trim(STATUS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cboResource")
                        {
                            if (_gr.Controls[k].Text.Trim() == "ALL") { strResource = ""; }
                            else
                            {
                                strResource = " and   upper(Trim(MSG_SRC)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbMsg_status")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strAck_nack = ""; }
                            else
                            {
                                strAck_nack = " and   upper(Trim(SWMSTS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbProcess_Status")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strProcess_Status = ""; }
                            else if (_gr.Controls[k].Text.Trim() == "") { strProcess_Status = " and   upper(Trim(PROCESSSTS)) is null"; }
                            else
                            {
                                if (cbBox.SelectedValue.ToString() == "7")
                                {
                                    strProcess_Status = " and  SWIFT_MSG_CONTENT.QUERY_ID in (Select QUERY_ID from SWIFT_PROCESS Where  NEW_PROCESS =  '" + cbBox.SelectedValue + "')  ";
                                }
                                else
                                {
                                    strProcess_Status = " and   upper(Trim(PROCESSSTS)) =  '" + cbBox.SelectedValue + "'";
                                }
                            }
                        }
                    }
                    #endregion
                    k = k + 1;
                }
                Where = strSender + strRece + strRefno + strAmount + strCurrency + strDepartment + strMsg_dire + strStatus + strAck_nack + strOsn + strMsg_type + strResource + strProcess_Status;
                if (Where.Trim() == "")
                {
                    Where_return = "";
                }
                else if (Where.Trim() != "")
                {
                    for (int i = 0; i <= Where.Length; i = i + 1)
                    {
                        string pStart = Where.Substring(i, 1);
                        if (pStart == "d")
                        { Where = Where.Substring(i + 1); break; }
                    }
                    Where_return = "Where " + Where;
                }

                return Where_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }


        public static String Search_Normal_manual(GroupBox _gr, string strBRANCH_B, int pTeller)
        {
            try
            {
                string Where = "";
                string Where_return = "";
                clsCheckInput clsCheck = new clsCheckInput();
                string strSender = ""; string strProcess_Status = ""; string strResource = "";
                string strRece = ""; string strRefno = ""; string strAmount = ""; string strCurrency = "";
                string strDepartment = ""; string strMsg_dire = ""; string strAck_nack = ""; string strOsn = "";
                string strStatus = ""; string strMsg_type = "";
                int k = 0;
                while (k < _gr.Controls.Count)
                {
                    #region Neu la textbox-----------------------------------------------------------------------
                    if (_gr.Controls[k] is TextBox)
                    {
                        if (_gr.Controls[k].Name == "txtsend")
                        {
                            //if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) like '%" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().ToUpper()) + "%'"; }
                            if (Regex.IsMatch(_gr.Controls[k].Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) = lpad('" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "',5,'0')"; }
                            }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and    upper(Trim(BRANCH_A)) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtreceiver")
                        {
                            if (Regex.IsMatch(_gr.Controls[k].Text.Trim(), @"^[0-9]*\z") == true)/*Neu hoan toan la so*/
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and     upper(Trim(" + strBRANCH_B + ")) = lpad('" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "',5,'0')"; }
                            }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and     upper(Trim(" + strBRANCH_B + ")) = '" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("''", "").Replace("'", "").ToUpper()) + "'"; }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtrefno")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strRefno = ""; } else { strRefno = " and    upper(Trim(FIELD20)) like '%" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().ToUpper()) + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtAmount")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strAmount = ""; }
                            else
                            {
                                if (Regex.IsMatch(_gr.Controls[k].Text.Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                {
                                    String[] M = _gr.Controls[k].Text.Trim().Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                                    string strAM = M[1];
                                    if (M[1].Trim() == "00") { strAmount = " and   AMOUNT = " + M[0].Trim() + "." + M[1] + ""; }
                                    else
                                    {
                                        strAmount = " and   AMOUNT = '" + _gr.Controls[k].Text.Trim().Replace(",", "") + "'";
                                    }
                                }
                                else
                                {
                                    strAmount = " and   trim(AMOUNT) = '" + _gr.Controls[k].Text + "'";
                                }
                            }
                        }
                        else if (_gr.Controls[k].Name == "txtMsg_type")
                        {
                            string strMsg;
                            if (_gr.Controls[k].Text.Trim().Length == 5)
                            { strMsg = _gr.Controls[k].Text.Trim().Substring(_gr.Controls[k].Text.Trim().Length - 3, 3); }
                            else { strMsg = "MT" + _gr.Controls[k].Text.Trim(); }
                            if (_gr.Controls[k].Text.Trim() == "") { strMsg_type = ""; }
                            else { strMsg_type = " and   upper(Trim(MSG_TYPE)) = '" + strMsg.ToUpper() + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtOSN")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strOsn = ""; } else { strOsn = " and    upper(Trim(OSN)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%' "; }
                        }
                    }
                    #endregion

                    #region Neu la combobox--------------------------------------------------------------------
                    else if (_gr.Controls[k] is ComboBox)
                    {
                        ComboBox cbBox = (ComboBox)_gr.Controls[k];
                        if (_gr.Controls[k].Name == "cbCurrency")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strCurrency = ""; } else { strCurrency = " and   upper(Trim(CCYCD)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbdepartment")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strDepartment = ""; } 
                            else 
                            {
                                if (pTeller == 2)
                                {
                                    strDepartment = "  and   upper(Trim(NEW_DEPARTMENT)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'";
                                }
                                else
                                {
                                    strDepartment = "  and   upper(Trim(DEPARTMENT)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'";
                                }
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbmsg_direction")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strMsg_dire = ""; } else { strMsg_dire = " and   upper(Trim(MSG_DIRECTION)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbStatus")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strStatus = ""; }
                            else
                            {
                                strStatus = " and   upper(Trim(STATUS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cboResource")
                        {
                            if (_gr.Controls[k].Text.Trim() == "ALL") { strResource = ""; }
                            else
                            {
                                strResource = " and   upper(Trim(MSG_SRC)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbMsg_status")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strAck_nack = ""; }
                            else
                            {
                                strAck_nack = " and   upper(Trim(SWMSTS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbProcess_Status")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strProcess_Status = ""; }
                            else if (_gr.Controls[k].Text.Trim() == "") { strProcess_Status = " and   upper(Trim(PROCESSSTS)) is null"; }
                            else
                            {
                                if (cbBox.SelectedValue.ToString() == "7")
                                {
                                    strProcess_Status = " and  C.MSG_ID in (Select MSG_ID from SWIFT_PROCESS_HANDER Where  NEW_PROCESSSTS =  '" + cbBox.SelectedValue + "')  ";
                                }
                                else
                                {
                                    strProcess_Status = " and   upper(Trim(PROCESSSTS)) =  '" + cbBox.SelectedValue + "'";
                                }
                            }
                        }
                        //else if (_gr.Controls[k].Name == "cbProcess_Status")
                        //{
                        //    if (_gr.Controls[k].Text == "ALL") { strProcess_Status = ""; }
                        //    else if (_gr.Controls[k].Text.Trim() == "")
                        //    {
                        //        if (pTeller == 2)
                        //        {
                        //            strProcess_Status = " and   upper(Trim(NEW_PROCESSSTS)) is null";
                        //        }
                        //        else { strProcess_Status = " and   upper(Trim(PROCESSSTS)) is null"; }
                        //    }
                        //    else
                        //    {
                        //        if (pTeller == 2)
                        //        {
                        //            strProcess_Status = " and   upper(Trim(NEW_PROCESSSTS)) =  '" + cbBox.SelectedValue + "'";
                        //        }
                        //        else
                        //        {
                        //            strProcess_Status = " and   upper(Trim(PROCESSSTS)) =  '" + cbBox.SelectedValue + "'";
                        //        }
                        //    }
                        //}
                    }
                    #endregion
                    k = k + 1;
                }
                Where = strSender + strRece + strRefno + strAmount + strCurrency + strDepartment + strMsg_dire + strStatus + strAck_nack + strOsn + strMsg_type + strResource + strProcess_Status;
                if (Where.Trim() == "")
                {
                    Where_return = "";
                }
                else if (Where.Trim() != "")
                {
                    for (int i = 0; i <= Where.Length; i = i + 1)
                    {
                        string pStart = Where.Substring(i, 1);
                        if (pStart == "d")
                        { Where = Where.Substring(i + 1); break; }
                    }
                    Where_return = "Where " + Where;
                }

                return Where_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        //ham tra ra menh de where cho search Advance-----------------------------------------------------------
        public static String Search_Advance(DataGridView _dtgrs,out string datetimefrom,out string datetimeto, out string phanhe, out string nhnhan,
        out string sotien,out string loaidien,out string isn,out string chieudien, out string nhgui,
        out string sogd,out string loaitien,out string osn, out string tcdien,out string ttdienden,
        out string ttdiendi,out string ttgw,out string kieuxuly)
        {
            try
            {
                //khai bao bien tra ra de in dien------------------------------------------------------------
                string _datetimefrom = ""; string _datetimeto = ""; string _phanhe = ""; string _nhnhan = "";
                string _sotien = ""; string _loaidien = ""; string _isn = ""; string _chieudien = ""; string _nhgui = "";
                string _sogd = ""; string _loaitien = ""; string _osn = ""; string _tcdien = ""; string _ttdienden = "";
                string _ttdiendi = ""; string _ttgw = ""; string _kieuxuly = "";
                string _date = ""; string _dateft = "";
                //-------------------------------------------------------------------------------------------
                string Where = "";
                int j = 0;
                while (j < _dtgrs.Rows.Count)
                {
                    if (_dtgrs.Rows[j].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtgrs.Rows[j].Cells[0].Value.ToString() == "True")
                        {
                            string Clause = _dtgrs.Rows[j].Cells[1].Value.ToString();
                            String[] M = Clause.Split(new String[] { " " }, StringSplitOptions.None);//cat chuoi
                            if (M[0].Trim() == "VALUE_DATE" || M[0].Trim() == "SENDING_TIME" || M[0].Trim() == "RECEIVING_TIME" || M[0].Trim() == "TRANSDATE")
                            {
                                String[] N = M[2].Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
                                string pDate = N[2].Replace("'", "") + N[1].Replace("'", "") + N[0].Replace("'", "");
                                if (M[0].Trim() == "TRANSDATE")
                                {
                                    if (M[1].Trim() == "<=" || M[1].Trim() == "<") { _datetimeto = M[2].Replace("'", ""); }
                                    if (M[1].Trim() == ">=" || M[1].Trim() == ">") { _datetimefrom = M[2].Replace("'", ""); ; }
                                    if (M[1].Trim() == "=") { _datetimefrom = M[2].Replace("'", ""); _datetimeto = M[2].Replace("'", ""); _date = "="; _dateft = M[2].Replace("'", ""); }
                                    Where = Where + " and " + M[0] + M[1] + "'" + pDate + "'";
                                }
                                else
                                {
                                    Where = Where + " and " + "To_char(" + M[0] + ",'YYYYMMDD')" + M[1] + "'" + pDate + "'";
                                }
                            }
                            else if (M[0].Trim() == "STATUS" || M[0].Trim() == "SWMSTS" || M[0].Trim() == "PROCESSSTS" || M[0].Trim() == "MSG_SRC" || M[0].Trim() == "ERR_CODE" || M[0].Trim() == "PRINT_STS")
                            {                               
                                string pAllcode = _dtgrs.Rows[j].Cells[2].Value.ToString();
                                if (M[0].Trim() == "STATUS") { _ttgw = pAllcode; }
                                Where = Where + " and " + M[0] + M[1] + pAllcode;
                            }
                            else if (M[0].Trim() == "AMOUNT")
                            {
                                if (Regex.IsMatch(M[2].Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                {
                                    String[] N = M[2].Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                                    string strAM = N[1];
                                    if (N[1].Trim() == "00") { _sotien = M[2]; Where = Where + " and   AMOUNT " + M[1] + "'" + N[0].Trim() + "." + N[1] + "'"; }
                                    else
                                    {
                                        _sotien = M[2];
                                        Where = Where + " and   AMOUNT  " + M[1] + "'" + M[2].Replace(",", "") + "'";
                                    }
                                }
                                else
                                {
                                    _sotien = M[2];
                                    Where = Where + " and   trim(AMOUNT)  " + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                                }
                            } 
                            else if (M[0].Trim() == "RM_NUMBER")
                            {
                                Where = Where + " and upper(ltrim(" + M[0] + ",'0000'))" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            #region ------------bien in------------------------------
                            else if (M[0].Trim() == "DEPARTMENT")
                            {
                                _phanhe = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "BRANCH_A")
                            {
                                _nhgui = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "BRANCH_B")
                            {
                                _nhnhan = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "OSN")
                            {
                                _osn = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "ISN")
                            {
                                _isn = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "MSG_TYPE")
                            {
                                _loaidien = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "CCYCD")
                            {
                                _loaitien = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else if (M[0].Trim() == "FIELD20")
                            {
                                _sogd = M[2].Replace("'", "");
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            #endregion
                            else
                            {
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                        }
                    }
                    j = j + 1;
                }
                if (_date == "=") { _datetimefrom = _dateft; _datetimeto = _dateft; }
                if (Where.Trim() == "")
                {
                    datetimefrom = _datetimefrom; datetimeto = _datetimeto; phanhe = _phanhe; nhnhan = _nhnhan;
                    sotien = _sotien; loaidien = _loaidien; isn = _isn; chieudien = _chieudien; nhgui = _nhgui;
                    sogd = _sogd; loaitien = _loaitien; osn = _osn; tcdien = _tcdien; ttdienden = _ttdienden;
                    ttdiendi = _ttdiendi; ttgw = _ttgw; kieuxuly = _kieuxuly;
                    return ""; }
                else
                {
                    for (int i = 0; i <= Where.Length; i = i + 1)
                    {
                        string pStart = Where.Substring(i, 1);
                        if (pStart == "d")
                        { Where = Where.Substring(i + 1); break; }
                    }
                    datetimefrom = _datetimefrom; datetimeto = _datetimeto; phanhe = _phanhe; nhnhan = _nhnhan;
                    sotien = _sotien; loaidien = _loaidien; isn = _isn; chieudien = _chieudien; nhgui = _nhgui;
                    sogd = _sogd; loaitien = _loaitien; osn = _osn; tcdien = _tcdien; ttdienden = _ttdienden;
                    ttdiendi = _ttdiendi; ttgw = _ttgw; kieuxuly = _kieuxuly;
                    return "Where " + Where;
                }
            }
            catch (Exception ex)
            {
                datetimefrom = ""; datetimeto = ""; phanhe = ""; nhnhan = "";
                sotien = ""; loaidien = ""; isn = ""; chieudien = ""; nhgui = "";
                sogd = ""; loaitien = ""; osn = ""; tcdien = ""; ttdienden = "";
                ttdiendi = ""; ttgw = ""; kieuxuly = "";
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }


        public static String Search_Advance_manual(DataGridView _dtgrs, int pTeller)
        {
            try
            {
                string Where = "";
                int j = 0;
                while (j < _dtgrs.Rows.Count)
                {
                    if (_dtgrs.Rows[j].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtgrs.Rows[j].Cells[0].Value.ToString() == "True")
                        {
                            string Clause = _dtgrs.Rows[j].Cells[1].Value.ToString();
                            String[] M = Clause.Split(new String[] { " " }, StringSplitOptions.None);//cat chuoi
                            if (M[0].Trim() == "VALUE_DATE" || M[0].Trim() == "SENDING_TIME" || M[0].Trim() == "RECEIVING_TIME" || M[0].Trim() == "TRANSDATE")
                            {
                                String[] N = M[2].Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
                                string pDate = N[2].Replace("'", "") + N[1].Replace("'", "") + N[0].Replace("'", "");
                                if (M[0].Trim() == "TRANSDATE")
                                {
                                    Where = Where + " and " + M[0] + M[1] + "'" + pDate + "'";
                                }
                                else
                                {
                                    Where = Where + " and " + "To_char(" + M[0] + ",'YYYYMMDD')" + M[1] + "'" + pDate + "'";
                                }
                            }
                            else if (M[0].Trim() == "STATUS" || M[0].Trim() == "SWMSTS" || M[0].Trim() == "PROCESSSTS" || M[0].Trim() == "MSG_SRC" || M[0].Trim() == "ERR_CODE")
                            {
                                string pAllcode = _dtgrs.Rows[j].Cells[2].Value.ToString();
                                if (M[0].Trim() == "PROCESSSTS")
                                {
                                    if (pTeller == 2)
                                    {
                                        Where = Where + " and " + "NEW_PROCESSSTS" + M[1] + pAllcode;
                                    }
                                    else
                                    {
                                        Where = Where + " and " + "PROCESSSTS" + M[1] + pAllcode;
                                    }
                                }
                                else
                                {                                    
                                    Where = Where + " and " + M[0] + M[1] + pAllcode;
                                }
                            }
                            else if (M[0].Trim() == "AMOUNT")
                            {
                                if (Regex.IsMatch(M[2].Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                {
                                    String[] N = M[2].Replace(",", "").Split(new String[] { "." }, StringSplitOptions.None);//cat chuoi
                                    string strAM = N[1];
                                    if (N[1].Trim() == "00") { Where = Where + " and   AMOUNT " + M[1] + "'" + N[0].Trim() + "." + N[1] + "'"; }
                                    else
                                    {
                                        Where = Where + " and   AMOUNT  " + M[1] + "'" + M[2].Replace(",", "") + "'";
                                    }
                                }
                                else
                                {
                                    Where = Where + " and   trim(AMOUNT)  " + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                                }
                            } 
                            else if (M[0].Trim() == "DEPAERTMENT" )
                            {
                                if (pTeller == 2)
                                {
                                    Where = Where + " and upper(NEW_DEPARTMENT)" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                                }
                                else
                                {
                                    Where = Where + " and upper(DEPARTMENT)" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                                }
                            }
                            else if (M[0].Trim() == "BRANCH_B")
                            {
                                if (pTeller == 2)
                                {
                                    Where = Where + " and Lpad(upper(NEW_BRANCH),5,'0')" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                                }
                                else
                                {
                                    Where = Where + " and upper(BRANCH_B)" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                                }
                            }
                            else if (M[0].Trim() == "RM_NUMBER")
                            {
                                Where = Where + " and upper(ltrim(" + M[0] + ",'0000'))" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                            else
                            {
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].Replace(",", "").Replace("''", "").Replace("'", "") + "'";
                            }
                        }
                    }
                    j = j + 1;
                }
                if (Where.Trim() == "")
                { return ""; }
                else 
                {
                    for (int i = 0; i <= Where.Length; i = i + 1)
                    {
                        string pStart = Where.Substring(i, 1);
                        if (pStart == "d")
                        { Where = Where.Substring(i + 1); break; }
                    }
                    return "Where " + Where; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }


        //ham tra ra mot menh de de ad vao datagrid-------------------------------------------------------------
        public static String Search_condition(ComboBox cbColumns, ComboBox cbOperator, TextBox txtValue,
            ComboBox cboStatus, DateTimePicker dateValue, bool _bol)
        {
            try
            {
                string Condition = "";
                string Condition_two = "";
                clsCheckInput clsCheck = new clsCheckInput();
                if (_bol == true)
                {
                    string pColumns = cbColumns.SelectedValue.ToString();
                    if (pColumns == "BRANCH_A" || pColumns == "BRANCH_B")
                    {
                        if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            if (txtValue.Text.Trim().Length == 3)
                            { Condition = pColumns + " " + cbOperator.Text + " '%00" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                            else
                            { Condition = pColumns + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                        }
                        else { Condition = pColumns + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                    }
                    else if (pColumns == "MSG_TYPE")
                    {
                        if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            if (txtValue.Text.Trim().Length == 3)
                            { Condition = pColumns + " " + cbOperator.Text + " '%MT" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                            else { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                        }
                        else { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                    }
                    else
                    {
                        if (txtValue.Visible == false)
                        {Condition = pColumns + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(cboStatus.Text.Trim()) + "%'"; }
                        else
                        { Condition = pColumns + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                    }
                }
                else if (_bol == false)
                {
                    string pColumns = cbColumns.SelectedValue.ToString();
                    if (pColumns == "TRANSDATE" || pColumns == "SENDING_TIME" || pColumns == "VALUE_DATE" || pColumns == "RECEIVING_TIME")//neu search co dang datetime
                    {
                        string day = "";
                        string Month = "";
                        if (dateValue.Value.Day.ToString().Length == 1)
                        { day = "0" + dateValue.Value.Day.ToString(); }
                        else { day = dateValue.Value.Day.ToString(); }
                        if (dateValue.Value.Month.ToString().Length == 1)
                        { Month = "0" + dateValue.Value.Month.ToString(); }
                        else { Month = dateValue.Value.Month.ToString(); }
                        Condition = pColumns + " " + cbOperator.Text + " '" + day + "/" + Month + "/" + dateValue.Value.Year + "'";
                    }
                    else if (pColumns == "STATUS" || pColumns == "PROCESSSTS" || pColumns == "ERR_CODE" || pColumns == "MSG_SRC" || pColumns == "SWMSTS" || pColumns == "PRINT_STS")//phai search theo gia tri sealectedvalue
                    {
                        Condition = pColumns + " " + cbOperator.Text + " '" + cboStatus.Text + "'";
                        Condition_two = cboStatus.SelectedValue.ToString();
                    }
                    else
                    {
                        if (pColumns == "AMOUNT") { Condition = pColumns + " " + cbOperator.Text + " " + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + ""; }
                        else if (pColumns == "BRANCH_A" || pColumns == "BRANCH_B")
                        {
                            if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                if (txtValue.Text.Trim().Length == 3)
                                { Condition = pColumns + " " + cbOperator.Text + " '00" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                                else { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                            }
                            else
                            {Condition = cbColumns.SelectedValue + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                        }
                        else if (pColumns == "MSG_TYPE")
                        {
                            if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                if (txtValue.Text.Trim().Length == 3)
                                { Condition = pColumns + " " + cbOperator.Text + " 'MT" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                                else { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                            }
                            else { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                        }
                        else
                        {
                            if (txtValue.Visible == false)
                            { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(cboStatus.Text.Trim()) + "'"; }
                            else
                            { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'";  }
                        }
                    }
                }
                return Condition + "$$$$" + Condition_two;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        //ham add them mot dong vao datagridview--------------------------------------------------------------
        public static DataGridView AddDatagrid(string Condition_one, string Condition_two,DataGridView _dtgrs)
        {
            try
            {
                int count;
                if (_dtgrs.Rows.Count == 0)
                {
                    _dtgrs.Rows.Add();
                    _dtgrs.Rows[0].Cells[1].Value = Condition_one;
                    _dtgrs.Rows[0].Cells[2].Value = Condition_two;
                    _dtgrs.Columns[0].Width = 50;
                    _dtgrs.Columns[1].Width = 520;
                    _dtgrs.Columns[1].ReadOnly = true;
                    _dtgrs.Rows[0].Cells[0].Value = true;                    
                }
                else
                {
                    count = _dtgrs.Rows.Count;
                    int k = 0;
                    while (k < count)//duyet tung dong trong datagrid
                    {
                        string strData = _dtgrs.Rows[k].Cells[1].Value.ToString();//lay ra du lieu                      
                        if (strData != Condition_one)
                        {
                            if (k == count - 1)
                            {
                                //ad du lieu vao combobox
                                _dtgrs.Rows.Add();
                                _dtgrs.Rows[count].Cells[1].Value = Condition_one;
                                _dtgrs.Rows[count].Cells[2].Value = Condition_two;
                                _dtgrs.Columns[0].Width = 50;
                                _dtgrs.Columns[1].Width = 480;
                                _dtgrs.Columns[1].ReadOnly = true;
                                _dtgrs.Rows[count].Cells[0].Value = true;                                
                            }
                        }
                        k = k + 1;
                    }
                }
                return _dtgrs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtgrs = null;
            }
        }
       
        
    }
}
