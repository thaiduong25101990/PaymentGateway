using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BR.BRLib;
using System.Collections;
using System.Management;
using System.Security.Cryptography;


namespace BR.BRSYSTEM
{
    public partial class frmKeyCode : Form
    {        
        private UserEncrypt objEncrypt = new UserEncrypt();
        private clsHardDrive objHardDrive = new clsHardDrive();
        private clsFile objFile = new clsFile();

        public frmKeyCode()
        {
            InitializeComponent();
        }


        private void frmKeyCode_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");             
            try
            {
                this.Text = Common.sCaption + " monitor";            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmKeyCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common.iClose_Exit = 1;
        }

        private bool Check_Input()
        {
            bool isSult = true;
            try
            {
                if (txtBranch.Text.Trim() == "")
                {
                    Common.ShowError("Branch is Empty!",3,MessageBoxButtons.OK);                    
                    txtBranch.Focus();
                    return isSult = false;
                }
                if (txtAddress.Text == "")
                {
                    Common.ShowError("Address is Empty!", 3, MessageBoxButtons.OK);                    
                    txtAddress.Focus();
                    return isSult = false;
                }
                if (txtNwekey.Text == "")
                {
                    Common.ShowError("Keycode is Empty!", 3, MessageBoxButtons.OK);                    
                    txtNwekey.Focus();
                    return isSult = false;
                }
                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                return isSult = false;
            }           
            return isSult;
        }

        private bool check_Decode()
        {
            bool isOK = true;
            try
            {
                string InputStr = txtBranch.Text + "/" + txtAddress.Text;
                string Decode_input = objEncrypt.Decrypt(txtNwekey.Text, objEncrypt.sKeySetup);
                if (InputStr == Decode_input)
                {
                    if (objEncrypt.Encrypt(InputStr, objEncrypt.sKeySetup) == txtNwekey.Text)                    
                        isOK = true;                    
                    else                    
                        isOK = false;                    
                }
                else                
                    isOK = false;                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            return isOK;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Common.iCode_key = 0;
            try
            {
                if (Check_Input())
                {
                    //goi ham kiem traxem thong tin co dung hay kong
                    if (check_Decode())
                    {
                        Common.iCode_key = 1;
                        string strHDD = objEncrypt.Encrypt(objHardDrive.GetHDD_Serial(), objEncrypt.sKeySetup);                        
                        //Tao ra mot file .ini
                        objFile.ExportFile(Common.FilePath, Common.File_key, strHDD);
                        this.Close();
                    }
                    else
                        Common.ShowError("This key is not valid!", 3, MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Common.iClose_Exit = 1;
            this.Close();
        } 
    }
    
}
