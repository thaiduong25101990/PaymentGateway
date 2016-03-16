﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void About_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void About_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
        }
    }
}
