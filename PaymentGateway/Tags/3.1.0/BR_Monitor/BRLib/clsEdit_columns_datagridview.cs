using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace BR.BRLib
{
    public class clsEdit_columns_datagridview
    {
        //dinh dang do rong cac cot cua datatgridview
        //_dtgrid: datagridview
        //_dt: Datatable dinh ngia do dai cua cac cot
        public static void Edit_Columns_Datagrid(DataGridView _dtgrid, DataTable _dt)
        {
            try
            {
                int i = 1;
                while (i < _dtgrid.Columns.Count)
                {
                    int j = 0;
                    while (j < _dt.Columns.Count)
                    {
                        if (i == j+1)
                        {
                            _dtgrid.Columns[i].Width = Convert.ToInt32(_dt.Rows[0][j].ToString());
                            _dtgrid.Columns[i].DefaultCellStyle = Common.GetCell_Center();
                        }
                        j = j + 1;
                    }
                    i = i + 1;
                }
                _dtgrid.Columns["MSG_ID"].Visible = false;
                _dtgrid.Columns["QUERY_ID"].Visible = false;
                _dtgrid.Columns["RECEIVING_TIME"].DefaultCellStyle.Format = "dd/MM/yyyy  HH:mm:ss";
                _dtgrid.Columns["SENDING_TIME"].DefaultCellStyle.Format = "dd/MM/yyyy  HH:mm:ss";
                _dtgrid.Columns["TRANS_DATE"].DefaultCellStyle.Format = "dd/MM/yyyy";
                _dtgrid.Columns["AMOUNT"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }
}
