using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using BR.BRLib;

namespace BR.BRIBPS
{
    public class clsForward
    {

        public static DataGridView Remove_rows_exits(DataGridView _dtV,DataGridView _dtF)
        {
            try
            {
                int count ;
                int i = 0;
                while (i < _dtV.Rows.Count)
                {
                    count = 0;
                    string vMSG_ID = _dtV.Rows[i].Cells["MSG_ID1"].Value.ToString();
                    int j = 0;
                    while (j < _dtF.Rows.Count)
                    {
                        if (vMSG_ID == _dtF.Rows[j].Cells["MSG_ID"].Value.ToString())
                        {
                            _dtV.Rows.RemoveAt(i);
                            count = 1;
                            break;
                        }
                        j = j + 1;
                    }
                    if (count == 0) { i = i + 1; }
                }
                return _dtV;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtV = null;
            }
        }


        public static DataGridView Add_forward(int Rows,DataGridView _dtgr,DataGridView _dtgrAd)
        {
            try
            {
                string strTRANS_CODE = _dtgr.Rows[Rows].Cells["TRANS_CODE1"].Value.ToString();
                string strGW_TRANS = _dtgr.Rows[Rows].Cells["GW_TRANS_NUM1"].Value.ToString();
                string strRM_NUMBER = _dtgr.Rows[Rows].Cells["RM_NUMBER1"].Value.ToString();
                string strSender = _dtgr.Rows[Rows].Cells["SENDER1"].Value.ToString();
                string strResemd = _dtgr.Rows[Rows].Cells["RECEIVER1"].Value.ToString();
                string strTRANS_DATE = _dtgr.Rows[Rows].Cells["TRANS_DATE1"].Value.ToString();
                string strAmount = _dtgr.Rows[Rows].Cells["AMOUNT1"].Value.ToString();
                string strCCYCD = _dtgr.Rows[Rows].Cells["CCYCD1"].Value.ToString();
                string strStatus = _dtgr.Rows[Rows].Cells["STATUS1"].Value.ToString();
                string strTAD = _dtgr.Rows[Rows].Cells["TAD1"].Value.ToString();
                string strPRE_TAD = _dtgr.Rows[Rows].Cells["PRE_TAD1"].Value.ToString();
                string strMSG_ID = _dtgr.Rows[Rows].Cells["MSG_ID1"].Value.ToString();
                string strQuery_Id = _dtgr.Rows[Rows].Cells["Query_Id1"].Value.ToString();
                _dtgrAd = Add_datagrid(strTRANS_CODE,
                                        strGW_TRANS, strRM_NUMBER,
                                        strSender, strResemd, strTRANS_DATE,
                                        strAmount, strCCYCD, strStatus, strTAD,
                                        strPRE_TAD, strMSG_ID, strQuery_Id, _dtgrAd);
                //dataSearch.Rows.RemoveAt(Rows);
                return _dtgrAd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtgrAd = null;
            }
        }

        public static DataGridView Add_datagrid(string strTRANS_CODE,
        string strGW_TRANS, string strRM_NUMBER, string strSender,
        string strResemd, string strTRANS_DATE, string strAmount,
        string strCCYCD, string strStatus, string strTAD, string strPRE_TAD,
        string strMSG_ID, string strQuery_Id,DataGridView _dtg)
        {
            try
            {
                int count;
                if (_dtg.Rows.Count == 0)
                {
                    _dtg = Add_data(0, strTRANS_CODE, strGW_TRANS, strRM_NUMBER, strSender, strResemd, strTRANS_DATE, strAmount,
                        strCCYCD, strStatus, strTAD, strPRE_TAD,
                        strMSG_ID, strQuery_Id, _dtg);
                }
                else
                {
                    count = _dtg.Rows.Count; int k = 0;
                    while (k < count)
                    {
                        if (k == count - 1)
                        {
                            _dtg = Add_data(count, strTRANS_CODE, strGW_TRANS, strRM_NUMBER, strSender, strResemd, strTRANS_DATE, strAmount,
                            strCCYCD, strStatus, strTAD, strPRE_TAD,
                            strMSG_ID, strQuery_Id, _dtg);
                        }
                        k = k + 1;
                    }
                }
                return _dtg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtg = null;
            }
            //Visible_Columns1(dgView);
            //ColumnsRead(dgView);
            //BR.BRLib.FomatGrid.Color_datagrid(dgView);
        }

        public static DataGridView Add_data(int Rows, string strTRANS_CODE,
        string strGW_TRANS, string strRM_NUMBER, string strSender,
        string strResemd, string strTRANS_DATE, string strAmount,
        string strCCYCD, string strStatus, string strTAD, string strPRE_TAD,
        string strMSG_ID, string strQuery_Id,DataGridView _dtgrid)
        {
            try
            {
                _dtgrid.Rows.Add();
                _dtgrid.Rows[Rows].Cells["TRANS_CODE"].Value = strTRANS_CODE;
                _dtgrid.Rows[Rows].Cells["GW_TRANS_NUM"].Value = strGW_TRANS;
                _dtgrid.Rows[Rows].Cells["RM_NUMBER"].Value = strRM_NUMBER;
                _dtgrid.Rows[Rows].Cells["SENDER"].Value = strSender;
                _dtgrid.Rows[Rows].Cells["RECEIVER"].Value = strResemd;
                _dtgrid.Rows[Rows].Cells["TRANS_DATE"].Value = strTRANS_DATE;
                _dtgrid.Rows[Rows].Cells["AMOUNT"].Value = strAmount;
                _dtgrid.Rows[Rows].Cells["CCYCD"].Value = strCCYCD;
                _dtgrid.Rows[Rows].Cells["STATUS"].Value = strStatus;
                _dtgrid.Rows[Rows].Cells["TAD"].Value = strTAD;
                _dtgrid.Rows[Rows].Cells["PRE_TAD"].Value = strPRE_TAD;
                _dtgrid.Rows[Rows].Cells["MSG_ID"].Value = strMSG_ID;
                _dtgrid.Rows[Rows].Cells["Query_Id"].Value = strQuery_Id;
                _dtgrid.Columns["AMOUNT"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                _dtgrid.Columns["TRANS_DATE"].DefaultCellStyle.Format = "dd/MM/yyyy";
                return _dtgrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtgrid = null;
            }
        }


        public static DataGridView Remove_forward(int Rows, DataGridView _dtr, DataGridView _dtv)
        {
            try
            {
                string strTRANS_CODE = _dtr.Rows[Rows].Cells["TRANS_CODE"].Value.ToString();
                string strGW_TRANS = _dtr.Rows[Rows].Cells["GW_TRANS_NUM"].Value.ToString();
                string strRM_NUMBER = _dtr.Rows[Rows].Cells["RM_NUMBER"].Value.ToString();
                string strSender = _dtr.Rows[Rows].Cells["SENDER"].Value.ToString();
                string strResemd = _dtr.Rows[Rows].Cells["RECEIVER"].Value.ToString();
                string strTRANS_DATE = _dtr.Rows[Rows].Cells["TRANS_DATE"].Value.ToString();
                string strAmount = _dtr.Rows[Rows].Cells["AMOUNT"].Value.ToString();
                string strCCYCD = _dtr.Rows[Rows].Cells["CCYCD"].Value.ToString();
                string strStatus = _dtr.Rows[Rows].Cells["STATUS"].Value.ToString();
                string strTAD = _dtr.Rows[Rows].Cells["TAD"].Value.ToString();
                string strPRE_TAD = _dtr.Rows[Rows].Cells["PRE_TAD"].Value.ToString();
                string strMSG_ID = _dtr.Rows[Rows].Cells["MSG_ID"].Value.ToString();
                string strQuery_Id = _dtr.Rows[Rows].Cells["Query_Id"].Value.ToString();
                _dtv = Add_datagrid_Select(strTRANS_CODE, strGW_TRANS, strRM_NUMBER, strSender,
                strResemd, strTRANS_DATE, strAmount, strCCYCD, strStatus, strTAD, strPRE_TAD,
                strMSG_ID, strQuery_Id, _dtv);
                return _dtv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtv = null;
            }
        }

        public static DataGridView Add_datagrid_Select(string strTRANS_CODE,
      string strGW_TRANS, string strRM_NUMBER, string strSender,
      string strResemd, string strTRANS_DATE, string strAmount,
      string strCCYCD, string strStatus, string strTAD, string strPRE_TAD,
      string strMSG_ID, string strQuery_Id,DataGridView _dtv)
        {
            try
            {
                int count;
                if (_dtv.Rows.Count == 0)
                {
                   _dtv = Add_remove_data(0, strTRANS_CODE,
                        strGW_TRANS, strRM_NUMBER, strSender,
                        strResemd, strTRANS_DATE, strAmount,
                        strCCYCD, strStatus, strTAD, strPRE_TAD,
                        strMSG_ID, strQuery_Id, _dtv);
                }
                else
                {
                    count = _dtv.Rows.Count; int k = 0;
                    while (k < count)
                    {
                        if (k == count - 1)
                        {
                          _dtv =  Add_remove_data(count, strTRANS_CODE,
                            strGW_TRANS, strRM_NUMBER, strSender,
                            strResemd, strTRANS_DATE, strAmount,
                            strCCYCD, strStatus, strTAD, strPRE_TAD,
                            strMSG_ID, strQuery_Id, _dtv);
                        }
                        k = k + 1;
                    }
                }
                return _dtv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtv = null;
            }
            //Enable_controls();
            //Visible_Columns(dataSearch);
            //ColumnsRead(dataSearch);
            //ColumsHeader(dataSearch);
            //BR.BRLib.FomatGrid.Color_datagrid1(dataSearch);
        }

       public static DataGridView Add_remove_data(int Rows, string strTRANS_CODE,
       string strGW_TRANS, string strRM_NUMBER, string strSender,
       string strResemd, string strTRANS_DATE, string strAmount,
       string strCCYCD, string strStatus, string strTAD, string strPRE_TAD,
       string strMSG_ID, string strQuery_Id, DataGridView _dtv)
        {
            try
            {
                _dtv.Rows.Add();
                _dtv.Rows[Rows].Cells["TRANS_CODE1"].Value = strTRANS_CODE;
                _dtv.Rows[Rows].Cells["GW_TRANS_NUM1"].Value = strGW_TRANS;
                _dtv.Rows[Rows].Cells["RM_NUMBER1"].Value = strRM_NUMBER;
                _dtv.Rows[Rows].Cells["SENDER1"].Value = strSender;
                _dtv.Rows[Rows].Cells["RECEIVER1"].Value = strResemd;
                _dtv.Rows[Rows].Cells["TRANS_DATE1"].Value = strTRANS_DATE;
                _dtv.Rows[Rows].Cells["AMOUNT1"].Value = strAmount;
                _dtv.Rows[Rows].Cells["CCYCD1"].Value = strCCYCD;
                _dtv.Rows[Rows].Cells["STATUS1"].Value = strStatus;
                _dtv.Rows[Rows].Cells["TAD1"].Value = strTAD;
                _dtv.Rows[Rows].Cells["PRE_TAD1"].Value = strPRE_TAD;
                _dtv.Rows[Rows].Cells["MSG_ID1"].Value = strMSG_ID;
                _dtv.Rows[Rows].Cells["Query_Id1"].Value = strQuery_Id;
                _dtv.Columns["AMOUNT1"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                _dtv.Columns["TRANS_DATE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
                return _dtv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtv = null;
            }
        }

    }
}
