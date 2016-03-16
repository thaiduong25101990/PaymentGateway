using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using System.Configuration;
using System.Threading;
using BR.BRBusinessObject;


namespace BR.BRSYSTEM
{
    public partial class frmSelectGWType : Form
    {
        private clsLog objLog = new clsLog();
        private GROUPSInfo objgroup = new GROUPSInfo();
        private GROUPSController objcontrolgroup = new GROUPSController();
        private USERSController objCtrlUser = new USERSController();
        
        private static bool strSucess = false;
        private bool NeedConfirm = true;

        public frmSelectGWType()
        {
            InitializeComponent();
        }

        private void cmdcancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                objCtrlUser.UPDATE_LOGSTS(Common.Userid, "O");
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
        * Method           : cmdok_Click(object sender, EventArgs e)
        * Muc dich         : khi chon mot kenh thanh toan va su kien click vao nut OK
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdok_Click(object sender, EventArgs e)
        {
            int l = 0;
            while (l < grbtype.Controls.Count)
            {
                RadioButton rdbControl = new RadioButton();
                rdbControl = (RadioButton)grbtype.Controls[l];
                if (rdbControl.Checked == true)// doan ma kiem tra chon kenh thanh toan nao
                {
                    string strGwtype = rdbControl.Text;
                    Common.strGWTYPE = strGwtype;
                    this.DialogResult = DialogResult.OK;
                    //doan ma lay du lieu ghilog
                    DateTime dtDate = DateTime.Now;
                    string strUsername = Common.strUsername;
                    string strContent = "Loggin" + "," + strGwtype;
                    int iLog_level = 1;
                    string strWorked = "";
                    string strTable = "Users,Groups";
                    string strOld_value = "";
                    string strNew_value = "";

                    objLog.GhiLogUser(dtDate, strUsername, strContent, iLog_level,
                        strWorked, strTable, strOld_value, strNew_value);

                }
                l = l + 1;
            }
            strSucess = true;
        }

        /*---------------------------------------------------------------
        * Method           : frmSelectGWType_Load(object sender, EventArgs e)
        * Muc dich         : hien thi cac o radiobuton the hien cac kenh thanh toan
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmSelectGWType_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                string userid = Common.Userid;
                DataSet datgroup_user = new DataSet();
                datgroup_user = objcontrolgroup.GetGroup_Gwtype(userid);
                int i = 0;
                while (i < datgroup_user.Tables[0].Rows.Count)
                {                    
                    int Itcount = (grbtype.Width - ((datgroup_user.Tables[0].Rows.Count) * 10 + ((datgroup_user.Tables[0].Rows.Count - 1) * 10))) / 2;
                    string strGwtype = datgroup_user.Tables[0].Rows[i]["GWTYPE"].ToString();
                    RadioButton radiotype = new RadioButton();
                    radiotype.Name = "type" + i;
                    radiotype.Text = strGwtype;
                    radiotype.Top = i * 20 + Itcount - 10;
                    radiotype.Left = 50;
                    grbtype.Controls.Add(radiotype);
                    i = i + 1;
                }
                RadioButton rdbControl = new RadioButton();
                rdbControl = (RadioButton)grbtype.Controls[0];
                rdbControl.Checked = true;
                cmdok.Focus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        /*---------------------------------------------------------------
        * Method           : frmSelectGWType_FormClosing(object sender, FormClosingEventArgs e)
        * Muc dich         : bat su kien thoat khoi form
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmSelectGWType_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit", Common.sCaption);
                    if (Common.iSelectType == 5)
                    {
                        if (e.Cancel == false)
                        {
                            Common.iCloseT = 5;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        ///////////////////////////////////////////////////-/
        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCD_FormClosing(null,null);
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                //cmdok.Focus();
                cmdok_Click(null, null);
                strSucess = true;
            }
        }

    }
}
