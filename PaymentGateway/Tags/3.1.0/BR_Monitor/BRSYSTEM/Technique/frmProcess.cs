using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BR.BRSYSTEM.Technique
{
    public partial class frmProcess : Form
    {
        public int iCount;
        public int Ex;

        public frmProcess()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            iCount=0;
            Ex = 1000;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 1000;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
        }

        public void OnTime(object sender, EventArgs e)
        {
            if (iCount < Ex) { progressBar1.Value = (int)(iCount * progressBar1.Maximum / Ex); }
            this.Show();
        }
        /****************************
         * Public Timer others thread
         ****************************/ 
        public void OnTimeThread(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            if (iCount < Ex) { progressBar1.Value = (int)(iCount * progressBar1.Maximum / Ex); }            
        }
    }
}
