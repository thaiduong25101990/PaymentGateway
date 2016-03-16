using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BR.BRInterBank.Tools
{
    public class CSS
    {
        // Get CSS cho DataGrid
        public static DataGridViewCellStyle GetCurrenCCY()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleRight;
            style.Format = "c";
            return style;
        }
    }
}
