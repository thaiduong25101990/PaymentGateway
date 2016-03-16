using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;

namespace BR.BRSYSTEM
{
    public partial class frmHistory : Form
    {
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        public string pForm_name;
        public frmHistory()
        {
            InitializeComponent();
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            try
            {
                Load_History();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Load_History()
        {
            try
            {
                DataTable dat = new DataTable();
                dat = objcontroluser_msg_log.History_User_Log(pForm_name);
                if (dat != null)
                {
                    dataGridView1.DataSource = dat;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
