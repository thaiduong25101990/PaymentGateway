using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using BR.BRLib;
using System.Text.RegularExpressions;
using System.Drawing;

namespace BR.BRIBPS
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

        public static void Color_datagrid(DataGridView datagr)
        {
            try
            {
                int i = 0;
                while (i < datagr.Rows.Count)
                {
                    if (datagr.Rows[i].Cells["STATUS1"].Value.ToString() == "PENDING")
                    {
                        datagr.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (datagr.Rows[i].Cells["STATUS1"].Value.ToString() == "SENT")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (datagr.Rows[i].Cells["STATUS1"].Value.ToString() == "CONVERTED")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    else if (datagr.Rows[i].Cells["STATUS1"].Value.ToString() == "ERROR")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Color_datagrid1(DataGridView datagr)
        {
            try
            {
                int i = 0;
                while (i < datagr.Rows.Count)
                {
                    if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "PENDING")
                    {
                        datagr.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "SENT")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "CONVERTED")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    else if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "ERROR")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView IBPS_MSG_CONTENT_SEARCH(DateTime _datefrom, DateTime _dateto, string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.IBPS_CONTENT_SEARCH(_datefrom, _dateto, pWhere, out _dsContent);
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
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static DataGridView IBPS_MSG_CONTENT_SEARCH_RS(DateTime _datefrom, DateTime _dateto, string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.IBPS_CONTENT_SEARCH_RS(_datefrom, _dateto, pWhere, out _dsContent);
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
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView IBPS_MSG_CONTENT_SEARCH_ADVANCE(string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.IBPS_CONTENT_SEARCH_ADVANCE(pWhere, out _dsContent);
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
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static DataGridView IBPS_MSG_CONTENT_SEARCH_ADVANCE_RS(string pWhere, DataGridView _dtGridviews)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.IBPS_CONTENT_SEARCH_ADVANCE_RS(pWhere, out _dsContent);
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
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay du lieu day leb datatgrid dung chung cho forward
        public static DataGridView FORWARD_CURRENT_SEARCH(DataGridView _dtGridviews, string pWhere,string pUserid,out DataTable _dt)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.FORWARD_SEARCH_CURRENT(out _dsContent, pWhere, pUserid);
                _dt = _dsContent.Tables[0];
                int h = 0;
                while (h < _dsContent.Tables[0].Rows.Count)
                {
                    _dtGridviews.Rows.Add();
                    _dtGridviews.Rows[h].Cells["TRANS_CODE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_CODE"].ToString();
                    _dtGridviews.Rows[h].Cells["GW_TRANS_NUM1"].Value = _dsContent.Tables[0].Rows[h]["GW_TRANS_NUM"].ToString();
                    _dtGridviews.Rows[h].Cells["RM_NUMBER1"].Value = _dsContent.Tables[0].Rows[h]["RM_NUMBER"].ToString();
                    _dtGridviews.Rows[h].Cells["SENDER1"].Value = _dsContent.Tables[0].Rows[h]["SENDER"].ToString();
                    _dtGridviews.Rows[h].Cells["RECEIVER1"].Value = _dsContent.Tables[0].Rows[h]["RECEIVER"].ToString();
                    _dtGridviews.Rows[h].Cells["TRANS_DATE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_DATE"].ToString();
                    _dtGridviews.Rows[h].Cells["AMOUNT1"].Value = Common.FormatCurrency(_dsContent.Tables[0].Rows[h]["AMOUNT"].ToString().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    _dtGridviews.Rows[h].Cells["CCYCD1"].Value = _dsContent.Tables[0].Rows[h]["CCYCD"].ToString();
                    _dtGridviews.Rows[h].Cells["STATUS1"].Value = _dsContent.Tables[0].Rows[h]["STATUS"].ToString();
                    _dtGridviews.Rows[h].Cells["TAD1"].Value = _dsContent.Tables[0].Rows[h]["TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["PRE_TAD1"].Value = _dsContent.Tables[0].Rows[h]["PRE_TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["MSG_ID1"].Value = _dsContent.Tables[0].Rows[h]["MSG_ID"].ToString();
                    _dtGridviews.Rows[h].Cells["Query_Id1"].Value = _dsContent.Tables[0].Rows[h]["Query_Id"].ToString();
                    h = h + 1;
                }
                _dtGridviews.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                _dtGridviews.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
                //----------------------------------------------------------------------------    
                _dtGridviews.Columns["TAD1"].Visible = false;
                _dtGridviews.Columns["PRE_TAD1"].Visible = false;
                _dtGridviews.Columns["MSG_ID1"].Visible = false;
                _dtGridviews.Columns["Query_Id1"].Visible = false;
                Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------               
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                _dt = null;
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung cho forward
        public static DataGridView FORWARD_CURRENT_LOAD(DataGridView _dtGridviews,string pUserid,out DataTable _dt)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.FORWARD_CURRNET_LOAD(out _dsContent, pUserid);
                _dt = _dsContent.Tables[0];
                int h = 0;
                while (h < _dsContent.Tables[0].Rows.Count)
                {
                    _dtGridviews.Rows.Add();
                    _dtGridviews.Rows[h].Cells["TRANS_CODE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_CODE"].ToString();
                    _dtGridviews.Rows[h].Cells["GW_TRANS_NUM1"].Value = _dsContent.Tables[0].Rows[h]["GW_TRANS_NUM"].ToString();
                    _dtGridviews.Rows[h].Cells["RM_NUMBER1"].Value = _dsContent.Tables[0].Rows[h]["RM_NUMBER"].ToString();
                    _dtGridviews.Rows[h].Cells["SENDER1"].Value = _dsContent.Tables[0].Rows[h]["SENDER"].ToString();
                    _dtGridviews.Rows[h].Cells["RECEIVER1"].Value = _dsContent.Tables[0].Rows[h]["RECEIVER"].ToString();
                    _dtGridviews.Rows[h].Cells["TRANS_DATE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_DATE"].ToString();
                    _dtGridviews.Rows[h].Cells["AMOUNT1"].Value = Common.FormatCurrency(_dsContent.Tables[0].Rows[h]["AMOUNT"].ToString().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    _dtGridviews.Rows[h].Cells["CCYCD1"].Value = _dsContent.Tables[0].Rows[h]["CCYCD"].ToString();
                    _dtGridviews.Rows[h].Cells["STATUS1"].Value = _dsContent.Tables[0].Rows[h]["STATUS"].ToString();
                    _dtGridviews.Rows[h].Cells["TAD1"].Value = _dsContent.Tables[0].Rows[h]["TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["PRE_TAD1"].Value = _dsContent.Tables[0].Rows[h]["PRE_TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["MSG_ID1"].Value = _dsContent.Tables[0].Rows[h]["MSG_ID"].ToString();
                    _dtGridviews.Rows[h].Cells["Query_Id1"].Value = _dsContent.Tables[0].Rows[h]["Query_Id"].ToString();
                    h = h + 1;
                }
                _dtGridviews.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                _dtGridviews.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
                //----------------------------------------------------------------------------    
                _dtGridviews.Columns["TAD1"].Visible = false;
                _dtGridviews.Columns["PRE_TAD1"].Visible = false;
                _dtGridviews.Columns["MSG_ID1"].Visible = false;
                _dtGridviews.Columns["Query_Id1"].Visible = false;
                Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------               
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                _dt = null;
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay du lieu day leb datatgrid dung chung cho forward
        public static DataGridView FREVIOUS_SEARCH(DataGridView _dtGridviews, string pUserid,string pDate,out DataTable _dt,string pWhere)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.FREVIOUS_SEARCH(out _dsContent, pUserid, pDate, pWhere);
                _dsContent.Tables[0].Merge(_dsContent.Tables[1]);
                _dt = _dsContent.Tables[0];
                int h = 0;
                while (h < _dsContent.Tables[0].Rows.Count)
                {
                    _dtGridviews.Rows.Add();
                    _dtGridviews.Rows[h].Cells["TRANS_CODE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_CODE"].ToString();
                    _dtGridviews.Rows[h].Cells["GW_TRANS_NUM1"].Value = _dsContent.Tables[0].Rows[h]["GW_TRANS_NUM"].ToString();
                    _dtGridviews.Rows[h].Cells["RM_NUMBER1"].Value = _dsContent.Tables[0].Rows[h]["RM_NUMBER"].ToString();
                    _dtGridviews.Rows[h].Cells["SENDER1"].Value = _dsContent.Tables[0].Rows[h]["SENDER"].ToString();
                    _dtGridviews.Rows[h].Cells["RECEIVER1"].Value = _dsContent.Tables[0].Rows[h]["RECEIVER"].ToString();
                    _dtGridviews.Rows[h].Cells["TRANS_DATE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_DATE"].ToString();
                    _dtGridviews.Rows[h].Cells["AMOUNT1"].Value = Common.FormatCurrency(_dsContent.Tables[0].Rows[h]["AMOUNT"].ToString().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    _dtGridviews.Rows[h].Cells["CCYCD1"].Value = _dsContent.Tables[0].Rows[h]["CCYCD"].ToString();
                    _dtGridviews.Rows[h].Cells["STATUS1"].Value = _dsContent.Tables[0].Rows[h]["STATUS"].ToString();
                    _dtGridviews.Rows[h].Cells["TAD1"].Value = _dsContent.Tables[0].Rows[h]["TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["PRE_TAD1"].Value = _dsContent.Tables[0].Rows[h]["PRE_TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["MSG_ID1"].Value = _dsContent.Tables[0].Rows[h]["MSG_ID"].ToString();
                    _dtGridviews.Rows[h].Cells["Query_Id1"].Value = _dsContent.Tables[0].Rows[h]["Query_Id"].ToString();
                    h = h + 1;
                }
                if (_dsContent.Tables[0].Rows.Count > 0)
                {
                    _dtGridviews.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                    _dtGridviews.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                //----------------------------------------------------------------------------    
                _dtGridviews.Columns["TAD1"].Visible = false;
                _dtGridviews.Columns["PRE_TAD1"].Visible = false;
                _dtGridviews.Columns["MSG_ID1"].Visible = false;
                _dtGridviews.Columns["Query_Id1"].Visible = false;
                Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------               
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                _dt = null;
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay du lieu day leb datatgrid dung chung cho forward
        public static DataGridView FREVIOUS_LOAD(DataGridView _dtGridviews,string pUserid,out DataTable _dt)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.FREVIOUS_LOAD(out _dsContent, pUserid);
                _dsContent.Tables[0].Merge(_dsContent.Tables[1]);
                _dt = _dsContent.Tables[0];
                int h = 0;
                while (h < _dsContent.Tables[0].Rows.Count)
                {
                    _dtGridviews.Rows.Add();
                    _dtGridviews.Rows[h].Cells["TRANS_CODE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_CODE"].ToString();
                    _dtGridviews.Rows[h].Cells["GW_TRANS_NUM1"].Value = _dsContent.Tables[0].Rows[h]["GW_TRANS_NUM"].ToString();
                    _dtGridviews.Rows[h].Cells["RM_NUMBER1"].Value = _dsContent.Tables[0].Rows[h]["RM_NUMBER"].ToString();
                    _dtGridviews.Rows[h].Cells["SENDER1"].Value = _dsContent.Tables[0].Rows[h]["SENDER"].ToString();
                    _dtGridviews.Rows[h].Cells["RECEIVER1"].Value = _dsContent.Tables[0].Rows[h]["RECEIVER"].ToString();
                    _dtGridviews.Rows[h].Cells["TRANS_DATE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_DATE"].ToString();
                    _dtGridviews.Rows[h].Cells["AMOUNT1"].Value = Common.FormatCurrency(_dsContent.Tables[0].Rows[h]["AMOUNT"].ToString().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    _dtGridviews.Rows[h].Cells["CCYCD1"].Value = _dsContent.Tables[0].Rows[h]["CCYCD"].ToString();
                    _dtGridviews.Rows[h].Cells["STATUS1"].Value = _dsContent.Tables[0].Rows[h]["STATUS"].ToString();
                    _dtGridviews.Rows[h].Cells["TAD1"].Value = _dsContent.Tables[0].Rows[h]["TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["PRE_TAD1"].Value = _dsContent.Tables[0].Rows[h]["PRE_TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["MSG_ID1"].Value = _dsContent.Tables[0].Rows[h]["MSG_ID"].ToString();
                    _dtGridviews.Rows[h].Cells["Query_Id1"].Value = _dsContent.Tables[0].Rows[h]["Query_Id"].ToString();
                    h = h + 1;
                }
                _dtGridviews.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                _dtGridviews.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
                //----------------------------------------------------------------------------    
                _dtGridviews.Columns["TAD1"].Visible = false;
                _dtGridviews.Columns["PRE_TAD1"].Visible = false;
                _dtGridviews.Columns["MSG_ID1"].Visible = false;
                _dtGridviews.Columns["Query_Id1"].Visible = false;
                Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------               
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                _dt = null;
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        // ham lay du lieu day leb datatgrid dung chung cho forward
        public static DataGridView FORWARD_LOAD(DataGridView _dtGridviews,out DataTable _dt,string pTELLERID)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.FORWARD_LOAD(out _dsContent, pTELLERID);
                _dt = new DataTable();
                _dt = _dsContent.Tables[0];
                int h = 0;
                while (h < _dsContent.Tables[0].Rows.Count)
                {
                    _dtGridviews.Rows.Add();
                    _dtGridviews.Rows[h].Cells["TRANS_CODE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_CODE"].ToString();
                    _dtGridviews.Rows[h].Cells["GW_TRANS_NUM1"].Value = _dsContent.Tables[0].Rows[h]["GW_TRANS_NUM"].ToString();
                    _dtGridviews.Rows[h].Cells["RM_NUMBER1"].Value = _dsContent.Tables[0].Rows[h]["RM_NUMBER"].ToString();
                    _dtGridviews.Rows[h].Cells["SENDER1"].Value = _dsContent.Tables[0].Rows[h]["SENDER"].ToString();
                    _dtGridviews.Rows[h].Cells["RECEIVER1"].Value = _dsContent.Tables[0].Rows[h]["RECEIVER"].ToString();
                    _dtGridviews.Rows[h].Cells["TRANS_DATE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_DATE"].ToString();
                    _dtGridviews.Rows[h].Cells["AMOUNT1"].Value = Common.FormatCurrency(_dsContent.Tables[0].Rows[h]["AMOUNT"].ToString().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    _dtGridviews.Rows[h].Cells["CCYCD1"].Value = _dsContent.Tables[0].Rows[h]["CCYCD"].ToString();
                    _dtGridviews.Rows[h].Cells["STATUS1"].Value = _dsContent.Tables[0].Rows[h]["STATUS"].ToString();
                    _dtGridviews.Rows[h].Cells["TAD1"].Value = _dsContent.Tables[0].Rows[h]["TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["PRE_TAD1"].Value = _dsContent.Tables[0].Rows[h]["PRE_TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["MSG_ID1"].Value = _dsContent.Tables[0].Rows[h]["MSG_ID"].ToString();
                    _dtGridviews.Rows[h].Cells["Query_Id1"].Value = _dsContent.Tables[0].Rows[h]["Query_Id"].ToString();
                    h = h + 1;
                }
                _dtGridviews.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                _dtGridviews.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";               
                //----------------------------------------------------------------------------    
                _dtGridviews.Columns["TAD1"].Visible = false;
                _dtGridviews.Columns["PRE_TAD1"].Visible = false;
                _dtGridviews.Columns["MSG_ID1"].Visible = false;
                _dtGridviews.Columns["Query_Id1"].Visible = false;
                Color_datagrid(_dtGridviews);
                //goi ham add cac combobox vao luoi----------------------------------------------------               
                int j = 1;
                while (j < _dtGridviews.Columns.Count)
                {
                    _dtGridviews.Columns[j].ReadOnly = true;
                    j = j + 1;
                }
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                _dt = null;
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return _dtGridviews = null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung cho forward
        public static DataGridView FORWARD_SEARCH(DataGridView _dtGridviews,string pWhere,out DataTable _dt,string pTELLERID)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.FORWARD_SEARCH(out _dsContent, pWhere, pTELLERID);
                _dt = _dsContent.Tables[0];
                _dtGridviews.Rows.Clear();
                int h = 0;
                while (h < _dsContent.Tables[0].Rows.Count)
                {
                    _dtGridviews.Rows.Add();
                    _dtGridviews.Rows[h].Cells["TRANS_CODE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_CODE"].ToString();
                    _dtGridviews.Rows[h].Cells["GW_TRANS_NUM1"].Value = _dsContent.Tables[0].Rows[h]["GW_TRANS_NUM"].ToString();
                    _dtGridviews.Rows[h].Cells["RM_NUMBER1"].Value = _dsContent.Tables[0].Rows[h]["RM_NUMBER"].ToString();
                    _dtGridviews.Rows[h].Cells["SENDER1"].Value = _dsContent.Tables[0].Rows[h]["SENDER"].ToString();
                    _dtGridviews.Rows[h].Cells["RECEIVER1"].Value = _dsContent.Tables[0].Rows[h]["RECEIVER"].ToString();
                    _dtGridviews.Rows[h].Cells["TRANS_DATE1"].Value = _dsContent.Tables[0].Rows[h]["TRANS_DATE"].ToString();
                    _dtGridviews.Rows[h].Cells["AMOUNT1"].Value = Common.FormatCurrency(_dsContent.Tables[0].Rows[h]["AMOUNT"].ToString().Replace("\r", "").Replace("\r\n", ""), Common.FORMAT_CURRENCY);
                    _dtGridviews.Rows[h].Cells["CCYCD1"].Value = _dsContent.Tables[0].Rows[h]["CCYCD"].ToString();
                    _dtGridviews.Rows[h].Cells["STATUS1"].Value = _dsContent.Tables[0].Rows[h]["STATUS"].ToString();
                    _dtGridviews.Rows[h].Cells["TAD1"].Value = _dsContent.Tables[0].Rows[h]["TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["PRE_TAD1"].Value = _dsContent.Tables[0].Rows[h]["PRE_TAD"].ToString();
                    _dtGridviews.Rows[h].Cells["MSG_ID1"].Value = _dsContent.Tables[0].Rows[h]["MSG_ID"].ToString();
                    _dtGridviews.Rows[h].Cells["Query_Id1"].Value = _dsContent.Tables[0].Rows[h]["Query_Id"].ToString();
                    h = h + 1;
                }
                if (_dsContent.Tables[0].Rows.Count > 0)
                {
                    _dtGridviews.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                    _dtGridviews.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    //----------------------------------------------------------------------------    
                    _dtGridviews.Columns["TAD1"].Visible = false;
                    _dtGridviews.Columns["PRE_TAD1"].Visible = false;
                    _dtGridviews.Columns["MSG_ID1"].Visible = false;
                    _dtGridviews.Columns["Query_Id1"].Visible = false;
                    Color_datagrid(_dtGridviews);

                    int j = 1;
                    while (j < _dtGridviews.Columns.Count)
                    {
                        _dtGridviews.Columns[j].ReadOnly = true;
                        j = j + 1;
                    }                   
                }
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                _dt = null;
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView IBPS_MSG_CONTENT_LOAD(DataGridView _dtGridviews)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.IBPS_CONTENT_LOAD( out _dsContent);                
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
                return _dtGridviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ham lay du lieu day leb datatgrid dung chung
        public static DataGridView IBPS_MSG_CONTENT_LOAD_RESEND(DataGridView _dtGridviews)
        {
            try
            {
                IBPS_MSG_CONTENTController ControlConten = new IBPS_MSG_CONTENTController();
                DataSet _dsContent;
                //----------------------------------------------------------------------------
                _dsContent = ControlConten.IBPS_CONTENT_LOAD_RESEND(out _dsContent);
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
                DataSet _ds = new DataSet();
                _ds = clsSTATUS.STATUS_ERROR_CODE();

                DataGridViewComboBoxColumn _cbProcesssts = new DataGridViewComboBoxColumn();
                _cbProcesssts.Name = "STS";
                _cbProcesssts.HeaderText = "STATUS";
                _cbProcesssts.DataSource = _ds.Tables["STATUS"];
                _cbProcesssts.DataPropertyName = "STATUS";
                _cbProcesssts.ValueMember = "STATUS";
                _cbProcesssts.DisplayMember = "NAME";
                _cbProcesssts.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["STATUS"].DisplayIndex, _cbProcesssts);
                _dtGrid.Columns["STATUS"].Visible = false;
                //add cot Error_code--------------------------------------------------------               
                DataGridViewComboBoxColumn _cbError_code = new DataGridViewComboBoxColumn();
                _cbError_code.HeaderText = "ERROR_CODE";
                _cbError_code.DataSource = _ds.Tables["ERROR_CODE"]; 
                _cbError_code.DataPropertyName = "ERR_CODE";
                _cbError_code.ValueMember = "ERROR_CODE";
                _cbError_code.DisplayMember = "NAME";
                _cbError_code.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["ERR_CODE"].DisplayIndex, _cbError_code);
                _dtGrid.Columns["ERR_CODE"].Visible = false;
                //add cot Error_code--------------------------------------------------------               
                DataGridViewComboBoxColumn _cbFwsts = new DataGridViewComboBoxColumn();
                _cbFwsts.HeaderText = "FORWARD_STATUS";
                _cbFwsts.DataSource = _ds.Tables["FWSTS"];
                _cbFwsts.DataPropertyName = "FWSTS";
                _cbFwsts.ValueMember = "CDVAL";
                _cbFwsts.DisplayMember = "CONTENT";
                _cbFwsts.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["FWSTS"].DisplayIndex, _cbFwsts);
                _dtGrid.Columns["FWSTS"].Visible = false;
                //add cot Print sts--------------------------------------------------------               
                DataGridViewComboBoxColumn _cbPrsts = new DataGridViewComboBoxColumn();
                _cbPrsts.HeaderText = "PRINT_STATUS";
                _cbPrsts.DataSource = _ds.Tables["PRINT_STS"];
                _cbPrsts.DataPropertyName = "PRINT_STS";
                _cbPrsts.ValueMember = "CDVAL";
                _cbPrsts.DisplayMember = "CONTENT";
                _cbPrsts.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                _dtGrid.Columns.Insert(_dtGrid.Columns["PRINT_STS"].DisplayIndex, _cbPrsts);
                _dtGrid.Columns["PRINT_STS"].Visible = false;   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public static String Search_Advance(DataGridView _dtgrs, 
            out string datetimefrom,out string datetimeto, out string Direction)
        {
            string _dateFrom = "";
            string _dateTo = "";
            string _Direc = "";
            string _date = ""; string _dateft = "";
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
                                    if (M[1].Trim() == "<=" || M[1].Trim() == "<") { _dateTo = M[2].Replace("'", ""); }
                                    if (M[1].Trim() == ">=" || M[1].Trim() == ">") { _dateFrom = M[2].Replace("'", ""); ; }
                                    if (M[1].Trim() == "=") { _dateFrom = M[2].Replace("'", ""); _dateTo = M[2].Replace("'", ""); _date = "="; _dateft = M[2].Replace("'", ""); }
                                    Where = Where + " and " + M[0] + M[1] + "'" + pDate + "'";
                                }
                                else
                                {
                                    Where = Where + " and " + "To_char(" + M[0] + ",'YYYYMMDD')" + M[1] + "'" + pDate + "'";
                                }
                            }
                            else if (M[0].Trim()=="AMOUNT")
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
                                    Where = Where + " and   trim(AMOUNT)  " + M[1] + "'" + M[2].Replace(",", "").Replace("'", "").Replace("''", "") + "'";
                                }                               
                            }
                            else if (M[0].Trim() == "STATUS" || M[0].Trim() == "SWMSTS" || M[0].Trim() == "PROCESSSTS" || M[0].Trim() == "MSG_SRC" || M[0].Trim() == "ERR_CODE" || M[0].Trim() == "PRINT_STS")
                            {
                                string pAllcode = _dtgrs.Rows[j].Cells[2].Value.ToString();
                                Where = Where + " and " + M[0] + M[1] + pAllcode;
                            }
                            else if (M[0].Trim() == "SENDER")
                            {
                                Where = Where + " and upper(F07)" + M[1] + "'" + M[2].ToUpper().Replace("'", "").Replace("''", "") + "'";
                            }
                            else if (M[0].Trim() == "RECEIVER")
                            {
                                Where = Where + " and upper(F22)" + M[1] + "'" + M[2].ToUpper().Replace("'", "").Replace("''", "") + "'";
                            }
                            else if (M[0].Trim() == "RM_NUMBER")
                            {
                                Where = Where + " and upper(ltrim(" + M[0] + ",'0000'))" + M[1] + "'" + M[2].ToUpper().Replace("'", "").Replace("''", "") + "'";
                            }
                            else if (M[0].Trim() == "MSG_DIRECTION")
                            {
                                _Direc = M[2];
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].ToUpper().Replace("'", "").Replace("''", "") + "'";
                            }
                            else
                            {
                                Where = Where + " and upper(" + M[0] + ")" + M[1] + "'" + M[2].ToUpper().Replace("'", "").Replace("''", "") + "'";
                            }
                        }
                    }
                    j = j + 1;
                }
                if (_date == "=") { _dateFrom = _dateft; _dateTo = _dateft; }
                if (Where.Trim() == "")
                {
                    datetimefrom = _dateFrom; datetimeto = _dateTo; Direction = _Direc;
                    return "";
                }
                else
                {
                    for (int i = 0; i <= Where.Length; i = i + 1)
                    {
                        string pStart = Where.Substring(i, 1);
                        if (pStart == "d")
                        { Where = Where.Substring(i + 1); break; }
                    }
                    datetimefrom = _dateFrom; datetimeto = _dateTo; Direction = _Direc;
                    return "Where " + Where;
                }
            }
            catch (Exception ex)
            {
                datetimefrom = ""; datetimeto = "";  Direction = "";
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }


        //ham tra ra menh de where cho search normal
        public static String Search_Normal(GroupBox _gr)
        {
            try
            {
                string Where = "";
                string Where_return = "";
                clsCheckInput clsCheck = new clsCheckInput();
                string strSource_branch = ""; string strTrans_num = "";
                string strSender = ""; string strcbtab = ""; string strHV_LV = "";
                string strRece = ""; string strRefno = ""; string strAmount = ""; string strCurrency = "";
                string strDepartment = ""; string strMsg_dire = "";
                string strStatus = ""; string strFWStatus = ""; string strMsg_src = "";
                int k = 0;
                while (k < _gr.Controls.Count)
                {
                    #region Neu la textbox-----------------------------------------------------------------------
                    if (_gr.Controls[k] is TextBox)
                    {
                        if (_gr.Controls[k].Name == "txtsend")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strSender = ""; } else { strSender = " and upper(Trim(F07)) like '%" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("'", "").Replace("''", "").ToUpper()) + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtreceiver")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strRece = ""; } else { strRece = " and upper(Trim(F22)) like '%" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Trim().Replace("'", "").Replace("''", "").ToUpper()) + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtrefno")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strRefno = ""; } else { strRefno = " and upper(Trim(RM_NUMBER)) like '%" + clsCheck.ConvertVietnamese(_gr.Controls[k].Text.Replace("'", "").Replace("''", "").Trim().ToUpper()) + "%'"; }
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
                                    if (M[1].Trim() == "00") { strAmount = " and AMOUNT = " + M[0].Trim() + "." + M[1] + ""; }
                                    else
                                    {
                                        strAmount = " and AMOUNT = '" + _gr.Controls[k].Text.Trim().Replace(",", "") + "'";
                                    }
                                }
                                else
                                {
                                    strAmount = " and trim(AMOUNT) = '" + _gr.Controls[k].Text.Replace("'", "").Replace("''", "") + "'";
                                }
                            }
                        }                        
                        else if (_gr.Controls[k].Name == "txtSource_branch")//txtTrans_num
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strSource_branch = ""; } else { strSource_branch = " and upper(lpad(SOURCE_BRANCH,5,'0')) = '" + _gr.Controls[k].Text.Trim().Replace("'", "").Replace("''", "").ToUpper().PadLeft(5,'0') + "'"; }
                        }
                        else if (_gr.Controls[k].Name == "txtTrans_num")
                        {
                            if (_gr.Controls[k].Text.Trim() == "") { strTrans_num = ""; } else { strTrans_num = " and upper(ltrim(GW_TRANS_NUM,'00')) = '" + _gr.Controls[k].Text.Trim().Replace("'", "").Replace("''", "").ToUpper() + "'"; }
                        }
                    }
                    #endregion

                    #region Neu la combobox--------------------------------------------------------------------
                    else if (_gr.Controls[k] is ComboBox)
                    {
                        ComboBox cbBox = (ComboBox)_gr.Controls[k];
                        if (_gr.Controls[k].Name == "cbCurrency")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strCurrency = ""; } else { strCurrency = " and upper(Trim(CCYCD)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbdepartment")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strDepartment = ""; } else { strDepartment = " and upper(Trim(DEPARTMENT)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbMsgDirection")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strMsg_dire = ""; } else { strMsg_dire = " and upper(Trim(MSG_DIRECTION)) like '%" + _gr.Controls[k].Text.Trim().ToUpper() + "%'"; }
                        }
                        else if (_gr.Controls[k].Name == "cbStatus")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strStatus = ""; }
                            else
                            {
                                strStatus = " and upper(Trim(STATUS)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cboMSG_SRC")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strStatus = ""; }
                            else
                            {
                                strMsg_src = " and upper(Trim(MSG_SRC)) =  '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbHV_LV")
                        {
                            if (_gr.Controls[k].Text.Trim() == "ALL") { strHV_LV = ""; }
                            else
                            {
                                if (_gr.Controls[k].Text.Trim() == "LV")
                                {
                                    strHV_LV = " and upper(Trim(TRANS_CODE)) = '101001'";
                                }
                                else
                                {
                                    strHV_LV = " and upper(Trim(TRANS_CODE)) = '201001'";
                                }                               
                            }
                        }
                        else if (_gr.Controls[k].Name == "cboFWStatus")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strFWStatus = ""; }
                            else
                            {
                                strFWStatus = " and upper(Trim(FWSTS)) = '" + cbBox.SelectedValue + "'";
                            }
                        }
                        else if (_gr.Controls[k].Name == "cbtab")
                        {
                            if (_gr.Controls[k].Text == "ALL") { strcbtab = ""; }                            
                            else
                            {
                                strcbtab = " and upper(Trim(TAD)) = '" + cbBox.SelectedValue + "'";
                            }
                        }
                    }
                    #endregion
                    k = k + 1;
                }
                Where = strSource_branch + strTrans_num + strSender + strcbtab + strHV_LV + strRece + strRefno + strAmount + strCurrency + strDepartment + strMsg_dire + strStatus + strFWStatus + strMsg_src;
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


        //ham add them mot dong vao datagridview--------------------------------------------------------------
        public static DataGridView AddDatagrid(string Condition_one, string Condition_two, DataGridView _dtgrs)
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
                    _dtgrs.Columns[1].Width = 480;
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
                    if (pColumns == "SENDER")
                    {
                        if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            if (txtValue.Text.Trim().Length == 3)
                            { Condition = "F07" + " " + cbOperator.Text + " '%00" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                            else
                            { Condition = "F07" + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                        }
                        else { Condition = "F07" + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                    }
                    else if (pColumns == "RECEIVER")
                    {
                        if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            if (txtValue.Text.Trim().Length == 3)
                            { Condition = "F22" + " " + cbOperator.Text + " '%00" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                            else
                            { Condition = "F22" + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                        }
                        else { Condition = "F22" + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                    }
                    else if (pColumns == "TAD")
                    {
                        Condition = pColumns + " " + cbOperator.Text + " '%00" + clsCheck.ConvertVietnamese(cboStatus.Text.Trim()) + "%'"; 
                    }          
                    else
                    {
                        if (txtValue.Visible == false)
                        { Condition = pColumns + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(cboStatus.Text.Trim()) + "%'"; }
                        else
                        { Condition = pColumns + " " + cbOperator.Text + " '%" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "%'"; }
                    }
                }
                else if (_bol == false)
                {
                    string pColumns = cbColumns.SelectedValue.ToString();
                    if (pColumns == "TRANSDATE" || pColumns == "SENDING_TIME" || pColumns == "RECEIVING_TIME" || pColumns == "FWTIME")//neu search co dang datetime
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
                    else if (pColumns == "STATUS" || pColumns == "ERR_CODE" || pColumns == "FWSTS" || pColumns == "PRINT_STS")//phai search theo gia tri sealectedvalue
                    {
                        Condition = pColumns + " " + cbOperator.Text + " '" + cboStatus.Text + "'";
                        Condition_two = cboStatus.SelectedValue.ToString();
                    }
                    else if (pColumns == "TAD")
                    {
                        Condition = pColumns + " " + cbOperator.Text + " '00" + clsCheck.ConvertVietnamese(cboStatus.Text.Trim()) + "'";
                    }   
                    else
                    {
                        if (pColumns == "AMOUNT") { Condition = pColumns + " " + cbOperator.Text + " " + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + ""; }
                        else if (pColumns == "SENDER")
                        {
                            if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                if (txtValue.Text.Trim().Length == 3)
                                { Condition = "F07" + " " + cbOperator.Text + " '00" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                                else { Condition = "F07" + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                            }
                            else
                            { Condition = cbColumns.SelectedValue + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                        }                       
                        else if (pColumns == "RECEIVER")
                        {
                            if (Regex.IsMatch(txtValue.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                            {
                                if (txtValue.Text.Trim().Length == 3)
                                { Condition = "F22" + " " + cbOperator.Text + " '00" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                                else { Condition = "F22" + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                            }
                            else
                            { Condition = cbColumns.SelectedValue + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                        }                       
                        else
                        {
                            if (txtValue.Visible == false)
                            { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(cboStatus.Text.Trim()) + "'"; }
                            else
                            { Condition = pColumns + " " + cbOperator.Text + " '" + clsCheck.ConvertVietnamese(txtValue.Text.Trim()) + "'"; }
                        }
                    }
                }
                return Condition + "$$$$" + Condition_two;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

    }
}
