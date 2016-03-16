using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BR.BRSYSTEM.Tools
{
    /*********************************   
     * Format doi tuong DataGrid
     *********************************/ 
    public class DataGrid
    {
        // Gan nhan cho cot cua DataGridView 
        public static void SetColumsName(ref DataGridView dataGrid,int Pos,string Name)
        {  
                dataGrid.Columns[Pos].Name = Name;
                dataGrid.Columns[Pos].HeaderText = Name;
        }
        public static void SetColumsNames(ref DataGridView dataGrid,int[] POSs,string[] NAMEs)
        {            
            for (int i = 0; i < POSs.Length; i++)
            {
                dataGrid.Columns[POSs[i]].Name = NAMEs[i];
                dataGrid.Columns[POSs[i]].HeaderText = NAMEs[i];
            }
        }

        // Gan Format cho cot DataGridView
        public static void SetStyle(ref DataGridView dataGrid, int Pos,DataGridViewCellStyle Style)
        {
            dataGrid.Columns[Pos].DefaultCellStyle = Style;
        }        
        public static void SetStyles(ref DataGridView dataGrid, int[] POSs, DataGridViewCellStyle Style)
        {
            for (int i = 0; i < POSs.Length; i++)
            {
                dataGrid.Columns[POSs[i]].DefaultCellStyle = Style;
            }
        }
        
    }
}
