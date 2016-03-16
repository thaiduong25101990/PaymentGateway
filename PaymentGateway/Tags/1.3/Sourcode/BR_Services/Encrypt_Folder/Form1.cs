using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Demo_Encrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Encode_Click(object sender, EventArgs e)
        {
            txt_Output.Text = ENCRYPT.SecurityEncrypt.Encrypt(txt_Input.Text);
            
        }

        private void btn_Decode_Click(object sender, EventArgs e)
        {
            try
            {
                txt_Output.Text = DECRYPT.Security.Decrypt(txt_Input.Text);
            }
            catch
            {
                MessageBox.Show("Can't decode from Input");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
