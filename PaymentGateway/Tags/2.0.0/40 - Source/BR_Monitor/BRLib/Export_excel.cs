using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using Office_12 = Microsoft.Office.Core;
using Excel_12 = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace BR.BRLib
{    
    public class Export_excel
    {
        private static int iRM_NUMBER;
        private static int iRM_NUMBER1;
        private static int iRM_NUMBER2;
        private static int iRM_NUMBER3;
        private static int iRM_NUMBER4;
        public static System.Data.DataTable dtTable;
        
        public static void Export()
        {
            try
            {
                iRM_NUMBER = -1;
                ApplicationClass excel = new ApplicationClass();
                excel.Application.Workbooks.Add(true);
                Excel_12.Application oExcel_12 = null;                //Excel_12 Application
                Excel_12.Workbook oBook = null;                      // Excel_12 Workbook
                Excel_12.Sheets oSheetsColl = null;
                Object oMissing = System.Reflection.Missing.Value;

                if (dtTable.Rows.Count > 0)//neu datatable co du lieu moi thuc hien
                {
                    oExcel_12 = new Excel_12.Application();
                    oExcel_12.Visible = false;
                    oExcel_12.UserControl = true;
                    oBook = oExcel_12.Workbooks.Add(oMissing);
                    // Get worksheets collection
                    oSheetsColl = oExcel_12.Worksheets;
                    FillSheetByQuery(oSheetsColl, dtTable);
                    oExcel_12.Visible = true;
                }                
                Common.Export_excel = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private static void FillSheetByQuery(Excel_12.Sheets oSheetsColl, System.Data.DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    Excel_12.Worksheet oSheet = null;
                    Excel_12.Range oRange = null;
                    oSheet = (Excel_12.Worksheet)oSheetsColl.get_Item("Sheet1");
                    oSheet.Name = "Sheet1";

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        oRange = (Excel_12.Range)oSheet.Cells[1, j + 1];
                        //oRange.Value2 = dt.Columns[j].ColumnName;
                        if (dt.Columns[j].ColumnName == "RM_NUMBER")
                        {
                            iRM_NUMBER = j;
                            oRange.Value2 = dt.Columns[j].ColumnName;
                        }
                        else if (dt.Columns[j].ColumnName == "FIELD20")
                        {
                            iRM_NUMBER2 = j;
                            oRange.Value2 = dt.Columns[j].ColumnName;
                        }
                        else if (dt.Columns[j].ColumnName == "FIELD21")
                        {
                            iRM_NUMBER3 = j;
                            oRange.Value2 = dt.Columns[j].ColumnName;
                        }
                        else if (dt.Columns[j].ColumnName == "FIELD70")
                        {
                            iRM_NUMBER4 = j;
                            oRange.Value2 = dt.Columns[j].ColumnName;
                        }
                        else if (dt.Columns[j].ColumnName == "HEAD_CONTENT")
                        {
                            iRM_NUMBER1 = j;
                        }
                        else
                        {
                            oRange.Value2 = dt.Columns[j].ColumnName;
                        }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            oRange = (Excel_12.Range)oSheet.Cells[i + 2, j + 1];
                            if (j == iRM_NUMBER)
                            {
                                oRange.Value2 = "'" + dt.Rows[i][j].ToString();
                            }
                            else if (j == iRM_NUMBER2)
                            {
                                oRange.Value2 = "'" + dt.Rows[i][j].ToString();
                            }
                            else if (j == iRM_NUMBER3)
                            {
                                oRange.Value2 = "'" + dt.Rows[i][j].ToString();
                            }
                            else if (j == iRM_NUMBER4)
                            {
                                oRange.Value2 = "'" + dt.Rows[i][j].ToString();
                            }
                            else if (j == iRM_NUMBER1)//bo qua
                            { 
                            }
                            else
                            {
                                oRange.Value2 = dt.Rows[i][j].ToString();//HEAD_CONTENT
                            }


                        }
                    }
                    dtTable.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
