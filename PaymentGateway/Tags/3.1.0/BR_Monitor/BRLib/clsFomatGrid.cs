using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Drawing;

namespace BR.BRLib
{
    public class FomatGrid
    {
        private DataGridView m_grdView = new DataGridView();

        public void FormatGrig(ref DataGridView grdList,DataSet dsSource, ArrayList arrID, ArrayList arrDescription, ArrayList arrWidth)
        {
            try
            {

                grdList.DataSource = dsSource.Tables[0];
                for (int i = 0; i < arrID.Count - 1; i++)
                {
                    grdList.Columns[Convert.ToInt32(arrID[i])].Width = Convert.ToInt32(arrWidth[i]);
                    grdList.Columns[Convert.ToInt32(arrID[i])].HeaderText = arrDescription[i].ToString();
                    grdList.Columns[Convert.ToInt32(arrID[i])].Width =Convert.ToInt32(arrWidth[i]);
                }
              
            }
            catch
            { 
            }
        }
        public static void Color_datagrid(DataGridView datagr)
        {
            try
            {
                int i = 0;
                while (i < datagr.Rows.Count)
                {
                    datagr.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "2")
                    {
                        datagr.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "1")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "0")
                    {
                        datagr.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    else if (datagr.Rows[i].Cells["STATUS"].Value.ToString() == "-1")
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
        public static void ColumnsRead0(DataGridView Datagrid)
        {
            int b = 0;
            while (b < Datagrid.Columns.Count)
            {
                Datagrid.Columns[b].ReadOnly = true;
                b = b + 1;
            }
        }
        public static void ColumnsRead1(DataGridView Datagrid)
        {
            int b = 1;
            while (b < Datagrid.Columns.Count)
            {
                Datagrid.Columns[b].ReadOnly = true;
                b = b + 1;
            }
        }

        public static void ColumsHeaderDataGridView(DataGridView Datagrid)
        {
            try
            {

                int g = 0;
                while (g < Datagrid.Columns.Count)
                {
                    string strColumns = Datagrid.Columns[g].HeaderText.ToString();

                        Datagrid.Columns[g].HeaderCell.Value = strColumns.Replace("_", " ");
                    //Datagrid.Columns[g].DefaultCellStyle.Format
                    Datagrid.ColumnHeadersHeight = 21;
                    //Datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
                    g = g + 1;
                }
            }
            catch
            {
            }
        }

    }

    public class AutoCompleteComboBox : ComboBox
    {
       
    }
}
