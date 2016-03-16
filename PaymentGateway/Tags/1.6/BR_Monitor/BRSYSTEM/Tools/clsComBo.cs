using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace BR.BRSYSTEM.Tools
{
    public class ComBo
    {
        // Set du lieu cho ComboBox
        public static void SetValues(ref ComboBox cb,object[] Values)
        {
            cb.Items.Clear();
            cb.Items.Add("");
            for (int i = 0; i < Values.Length; i++)
            {
                cb.Items.Add(Values[i]);
            }
            cb.SelectedIndex = 0;
        }
        public static void SetValues(ref ComboBox cb, DataTable dt, string Name)
        {
            cb.Items.Clear();
            cb.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cb.Items.Add(dt.Rows[i][Name]);
            }
            cb.SelectedIndex = 0;
        }
        public static void SetValues(ref ComboBox cb, DataTable dt, int Pos)
        {
            cb.Items.Clear();
            cb.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cb.Items.Add(dt.Rows[i][Pos]);
            }
            cb.SelectedIndex = 0;
        }
    }
}
