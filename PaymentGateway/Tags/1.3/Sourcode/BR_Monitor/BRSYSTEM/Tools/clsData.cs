using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.BRSYSTEM.Tools
{
    /***************************************
     * Lớp phục vụ thao tác với matrix
     ***************************************/
    public class Data
    {          
        // Merge 2 DataTable voi nhau
        public static DataTable Merger(DataTable sc,DataTable ds)
        {
            // Merge 2 table voi nhau : so luong cot max :100
            DataTable a = sc.Copy();
            DataTable b = ds.Copy();
            for (int i = 0; i < a.Columns.Count; i++) a.Columns[i].ColumnName = i.ToString("00");
            for (int i = 0; i < b.Columns.Count; i++) b.Columns[i].ColumnName = i.ToString("00");
            a.Merge(b);
            return a;
        }
        // Gan Type cho DataTable
        public static void SetType(ref DataTable dt,int Pos,Type Type)
        {
            // Merge 2 table voi nhau : so luong cot max :100
            DataTable a = dt.Clone();
            DataRow dr;
            a.Columns[Pos].DataType = Type;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = a.NewRow();
                for (int j = 0; j < dt.Columns.Count;j++ )
                {
                    if (j == Pos)
                    {
                        dr[j] = Convert.ChangeType(dt.Rows[i][j], Type);
                    }
                    else
                    {
                        dr[j] = dt.Rows[i][j];
                    }
                }
                a.Rows.Add(dr);
            }
            dt = a;
        }
        public static void SetTypes(ref DataTable dt,int[] POSs,Type[] TYPEs)
        {
            // Merge 2 table voi nhau : so luong cot max :100
            DataTable a = dt.Clone();
            DataRow dr;
            Boolean IsChange = false;
            for (int i = 0; i < POSs.Length; i++) { a.Columns[POSs[i]].DataType = TYPEs[i]; }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = a.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    IsChange = false;
                    foreach (int k in POSs)
                    {
                        if (j == k)
                        { 
                            dr[j] = Convert.ChangeType(dt.Rows[i][j], TYPEs[k]); IsChange = true; break; 
                        }
                    }
                    if (IsChange == false) { dr[j] = dt.Rows[i][j]; };
                }
                a.Rows.Add(dr);
            }
            dt = a;
        }        
        // Gan them mot cot vao DataTable 
        public static void InsColumn(ref DataTable dt,int Pos,string Name,Type type,object[] Values)
        {
            int LenRow = dt.Rows.Count;
            int LenCol = dt.Columns.Count + 1;
            DataTable a = new DataTable();
            // Create New Colunms
            // int i = 0;
            for (int i = 0; i < LenCol; i++)
            {
                a.Columns.Add();
                if(i == Pos)
                {
                    a.Columns[i].DataType = type;
                    a.Columns[i].ColumnName = Name;
                }
                if (i > Pos)
                {
                    a.Columns[i].DataType = dt.Columns[i - 1].DataType;
                    a.Columns[i].ColumnName = dt.Columns[i - 1].ColumnName;
                }
                if (i < Pos)
                {
                    a.Columns[i].DataType = dt.Columns[i].DataType;
                    a.Columns[i].ColumnName = dt.Columns[i].ColumnName;
                }
            }
            // Build Data
            DataRow dr;
            for (int i = 0; i < LenRow;i++ )
            {
                dr = a.NewRow();
                for (int j = 0; j < LenCol; j++)
                {
                    if (j == Pos)
                    {
                        if (i < Values.Length)
                        {
                            dr[j] = Convert.ChangeType(Values[i], a.Columns[j].DataType);
                        }
                        else 
                        { 
                            dr[j] = null; 
                        }
                    }
                    if (j > Pos)
                    {
                        dr[j] = Convert.ChangeType(dt.Rows[i][j - 1], a.Columns[j].DataType);
                    }
                    if (j < Pos)
                    {
                        dr[j] = Convert.ChangeType(dt.Rows[i][j], a.Columns[j].DataType);
                    }
                }
                a.Rows.Add(dr);
            }
            dt = a;
        }
    }
}
