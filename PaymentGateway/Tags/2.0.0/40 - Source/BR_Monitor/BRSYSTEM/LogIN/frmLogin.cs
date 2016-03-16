using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using BR.BRBusinessObject;
using BR.BRLib;
using System.Text.RegularExpressions;



namespace BR.BRSYSTEM
{
    
    public partial class frmLogin : Form
    {
        
        public static USERSInfo objuser = new USERSInfo();
        
        private clsLog objLog = new clsLog();        
        private DataSet dtset =new DataSet() ;
        private USERSController objcontroluser = new USERSController();
        private GROUPSInfo objgroup = new GROUPSInfo();
        private GROUPSController objcontrolgroup = new GROUPSController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private SYSVARInfo objAllSysvar = new SYSVARInfo();
        private SYSVARController ObjctrLSysvar = new SYSVARController();
        private UserEncrypt Encrypt = new UserEncrypt();
        TADController objControl = new TADController();
        USERSController objBOUser = new USERSController();

        private int iLogNum = 0;
        private int iLockNum = 0;
        private bool ctrtext = false;
        private bool NeedConfirm = true;
        private static bool strSucess = false;
        private string UserLog;        
        private string strLastUserID = "";
                
        public frmLogin()
        {
            InitializeComponent();
        }

        #region // ok click
        /*---------------------------------------------------------------
        * Method           : cmdLogin_Click(object sender, EventArgs e)
        * Muc dich         : Goi Ham thuc hien  qua trinh truyen tham so de login vao he thong
        * Tham so          :  
        * Tra ve           : DataTable
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            #region Ham lay gia gia tri dau la hoi so chinh cua ngan hang
            int iHO = 0;
            string strHO = "";
            DataTable _dt = new DataTable();
            _dt = objControl.GET_HO_TAD();
            if (_dt == null)
            {
                return;
            }
            #endregion

            string pBranch;
            DataSet datUL = new DataSet();
            datUL = objcontroluser.GetUSERSUPDATEPASS(txtUser.Text.Trim());
            if (datUL.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(" UserID or Password is not correct!", Common.sCaption);
                return;
            }
            else
            { 
                pBranch = datUL.Tables[0].Rows[0]["BRANCH"].ToString();
            }
            #region Kiem tra user phai cua hoi so hay khong
            String[] N = strHO.Split(new String[] { "/" }, StringSplitOptions.None);//cat chuoi
            int iCountHO = N.Count();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                string pHOV = _dt.Rows[i]["SIBS_BANK_CODE"].ToString();
                if (pBranch.Trim().Substring(0, 3) != pHOV.Substring(2))
                {
                    if (pBranch.Trim().Substring(0, 3) != pHOV.Substring(2))
                    {
                        iHO = 1;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    iHO = 0;
                    break;
                }
            }
            if (_dt.Rows.Count == 0)
            {
                iHO = 1;
            }
            #endregion

            if (iHO == 1)
            {
                MessageBox.Show("You have no authority to access!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();  return;
            }
            else
            {
                Common.bTrue = false;
                string strUser = txtUser.Text;
                string strPass = txtPassword.Text;
                //-------------------Lay du lieu nhan dang do la user admin ---------------------------------
                DataSet dtAddmin = new DataSet();
                dtAddmin = ObjctrLSysvar.GetSYSVAR_NAME("ADMIN_ID", "SYSTEM");
                Common.pUser_Addmin = dtAddmin.Tables[0].Rows[0]["VALUE"].ToString();
                //-------------------------------------------------------------------------------------------
                if (!Login(strUser, strPass))
                {
                    if (iLockNum == 3)
                    {
                        if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_LOCKED) != 1)
                        {
                            DataSet dsSysTemp = new DataSet();
                            SYSVARController objSysvarControl = new SYSVARController();
                            dsSysTemp = objSysvarControl.GetSYSVAR_NAME("LoginTime", "SYSTEM");
                            MessageBox.Show("Error occured when updating database", Common.sCaption);
                            dsSysTemp.Dispose();
                            this.DialogResult = DialogResult.Cancel;
                        }
                        MessageBox.Show("You have entered an incorrect password three times. This account was locked!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }
                    if (iLogNum == 3)
                    {
                        MessageBox.Show("You have entered an incorrect account three times!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.DialogResult = DialogResult.Cancel;
                    }
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                }
            }                
            strSucess = true;
         }
        #endregion
        //**************************************************************
        
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Common.bTrue = true;
            this.DialogResult = DialogResult.Cancel;               
        }        
       
        //****************************************************
        #region// ham checkinput de dang nhap
        /*---------------------------------------------------------------
        * Method           : cmdLogin_Click(object sender, EventArgs e)
        * Muc dich         : Goi Ham thuc hien  Kiem tra User va Password truyen vao
        * Tham so          : txtUserName,TxtPassword
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private bool checkInput()
        {

            if (txtUser.Text == "")
            {
                MessageBox.Show("Username isEmpty", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return ctrtext=false ;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password isEmpty", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ctrtext=false;
            }
            if (txtUser.Text.Length > 30 || txtPassword.Text.Length > 30)
            {
                MessageBox.Show("User or password not allow lengther than 30 characters", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ctrtext = false;
            }
            return ctrtext= true;
        }
        #endregion       
        /*---------------------------------------------------------------
        * Method           : enterKey(object sender, KeyEventArgs e)
        * Muc dich         : Bắt sự kiện KeyDown trên form
        * Tham so          :  
        * Tra ve           : 
        * Ngay tao         : 06/08/2008
        * Nguoi tao        : HueMT
        * Ngay cap nhat    : 
        * Nguoi cap nhat   : 
        *--------------------------------------------------------------*/

        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                    //Goi su kien Click, nhung chua lam duoc                   
                    if ((this.ActiveControl as Button).Name == "cmdLogin")
                    {
                        cmdLogin_Click(null, null);
                    }                                        
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }
    
        /*---------------------------------------------------------------
        * Method           : Login(string strUser, string strPass)
        * Muc dich         : Ham dang nhap vao he thong
        * Tham so          : strUser va strPassword
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private bool Login(string strUser, string strPass)
        {
            
            try
            {
                checkInput();
                if (ctrtext)
                {
                    string strPasswordmh = Encrypt.Encrypt(strPass, Encrypt.sKeyUser);
                    DataSet datuser = new DataSet();
                    datuser = objcontroluser.GET_USER_PASS(strUser, strPasswordmh);
                    string strUserStatus = "";

                    if (datuser.Tables[0].Rows.Count != 0)//co thong tin dung,dang nhap thah cong
                    {
                        try
                        {
                            DataSet dtData = new DataSet();
                            dtData = objBOUser.Userid_UD(strUser);
                            Common.strFullName = dtData.Tables[0].Rows[0]["USERNAME"].ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Get message error!");
                        }

                        //check xem Users nay da co nguoi nao su dung de dang nhap chua
                        DataTable datLog = new DataTable();
                        datLog = objcontroluser.LOGSTS(strUser, strPasswordmh);
                        UserLog = datLog.Rows[0]["LOGSTS"].ToString();
                        if (UserLog.Trim() == "O" || UserLog.Trim() == "")//duoc phep dang nhap vao
                        {
                            if (Common.iLogout == 5)
                            {
                                objcontroluser.UPDATE_LOGSTS(Common.Userid, "O");
                            }
                            //update truong LOGSTS=I khi dang nhap vao user do
                            if (txtUser.Text != Common.pUser_Addmin)
                            {
                                objcontroluser.UPDATE_LOGSTS(strUser, "I/" + Common.IpLocal);
                            }
                            #region  Common.iLogout == 1
                            if (Common.iLogout == 1)
                            {
                                strUserStatus = datuser.Tables[0].Rows[0]["STATUS"].ToString();
                                DialogResult dlgResult = new DialogResult();
                                if ((strUserStatus == Common.GW_USERS_STATUS_ACTIVE) || (strUserStatus == Common.GW_USERS_STATUS_PENDING))
                                {
                                    DateTime dat = DateTime.Now;
                                    Common.Userid = txtUser.Text.Trim();
                                    Common.strPasswordUser = txtPassword.Text.Trim();
                                    Common.iID = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());

                                    DateTime dtLastDate = Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                    DateTime dtPassTime = Convert.ToDateTime(datuser.Tables[0].Rows[0]["PassTime"]);

                                    //lay du lieu de ghi log
                                    DateTime dtDateLogin = DateTime.Now;
                                    string strContent = "Log Off";
                                    int iLoglevel = 1;
                                    string strWorked = "Login";
                                    string strTable = "USERS";
                                    string strOld_value = "";
                                    string strNew_value = "";
                                    //goi ham ghilog
                                    objLog.GhiLogUser(dtDateLogin, txtUser.Text.Trim(), strContent, 
                                        iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                                    //Sau thoi gian n ngay khong dang nhap, user se bi khoa. n la value cau tham so NumLoginDay trong bang sysvar
                                    DataSet dsSysTemp = new DataSet();
                                    SYSVARController objSysvarControl = new SYSVARController();
                                    dsSysTemp = objSysvarControl.GetSYSVAR_NAME("NumLoginDay", "SYSTEM");

                                    if ((DateTime.Now - dtLastDate).Days >= Convert.ToInt32(dsSysTemp.Tables[0].Rows[0]["Value"]))
                                    {
                                        //Khoa nguoi dung
                                        if (Common.Userid.Trim() == Common.pUser_Addmin.Trim())//users admin
                                        {
                                            return true;
                                        }
                                        else//neu la user binh thuong
                                        {
                                            if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_LOCKED) != 1)
                                            {
                                                MessageBox.Show("Error occured when updating database", Common.sCaption);
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }
                                            MessageBox.Show("You have not logged in for " + dsSysTemp.Tables[0].Rows[0]["Value"].ToString() + " days. Your account was locked!", Common.sCaption);
                                            datuser.Dispose();
                                            this.DialogResult = DialogResult.Cancel;
                                            return false;
                                        }
                                    }

                                    //Neu nguoi dung dang nhap lan dau: status=pending
                                    if (strUserStatus == Common.GW_USERS_STATUS_PENDING)
                                    {
                                        dlgResult = MessageBox.Show("You must change password at the first time login. Do you want to change password?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (dlgResult == DialogResult.Yes)
                                        {
                                            if (!ChangePass())
                                            {
                                                MessageBox.Show("You have not changed password successfully!", Common.sCaption);
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }
                                            //Doi status = Active
                                            if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_ACTIVE) != 1)
                                            {
                                                MessageBox.Show("Error occured when updating database", Common.sCaption);
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }

                                        }
                                        else
                                        {
                                            this.DialogResult = DialogResult.Cancel;
                                            datuser.Dispose();
                                            dsSysTemp.Dispose();
                                            return false;
                                        }

                                    }


                                    //Nguoi dung phai doi Pass sau n thang
                                    dsSysTemp = objSysvarControl.GetSYSVAR_NAME("sysPasswordtime", "SYSTEM");
                                    //Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                    int isysPasswordtime = Convert.ToInt16(dsSysTemp.Tables[0].Rows[0]["Value"].ToString());


                                    int iMonth = (DateTime.Now.Year - dtPassTime.Year) * 12 + (DateTime.Now.Month - dtPassTime.Month) + (DateTime.Now.Day - dtPassTime.Day >= 0 ? 1 : (-1));

                                    if (iMonth >= isysPasswordtime)
                                    {                                        
                                        if (!ChangePass())
                                        {
                                            MessageBox.Show("You have not changed password successfully!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                            datuser.Dispose();
                                            dsSysTemp.Dispose();
                                            return false;
                                        }                                       
                                    }                                    
                                    int iID1 = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());
                                    Common.iID = iID1;
                                    string userid = datuser.Tables[0].Rows[0]["USERID"].ToString();
                                    Common.Userid = userid;
                                    Common.strUsername = strUser;

                                    //kiem tr cac nhom do co cung mot kenh thanh toan khong
                                    DataSet datGroupss = new DataSet();
                                    datGroupss = objcontrolgroup.GetGroup_Gwtype(userid);
                                    if (datGroupss.Tables[0].Rows.Count == 0)//khong vao kenh thanh toan nao
                                    {
                                        Common.CountGWtype = 0;
                                    }
                                    if (datGroupss.Tables[0].Rows.Count == 1)// vao cung mot kenh thanh toan
                                    {
                                        Common.CountGWtype = 1;
                                        string strGwtype = datGroupss.Tables[0].Rows[0]["GWTYPE"].ToString();
                                        Common.strGWTYPE = strGwtype;
                                    }
                                    if (datGroupss.Tables[0].Rows.Count > 1)// vao nhieu kenh thanh toan khaca nhau
                                    {
                                        Common.CountGWtype = 2;
                                    }
                                    datGroupss.Dispose();
                                    this.DialogResult = DialogResult.OK;
                                }
                                else if (strUserStatus == Common.GW_USERS_STATUS_LOCKED)
                                {
                                    MessageBox.Show("User is locked!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }
                            #endregion
                            #region Common.iLogout == 5
                            else if (Common.iLogout == 5)
                            {
                                if (txtUser.Text.Trim() == Common.strUserID1)
                                {
                                    Common.iLogin = 10;
                                    this.Close();
                                }
                                else
                                {
                                    Common.iLogin = 11;
                                    strUserStatus = datuser.Tables[0].Rows[0]["STATUS"].ToString();
                                    DialogResult dlgResult = new DialogResult();
                                    if ((strUserStatus == Common.GW_USERS_STATUS_ACTIVE) || (strUserStatus == Common.GW_USERS_STATUS_PENDING))
                                    {
                                        DateTime dat = DateTime.Now;
                                        Common.Userid = txtUser.Text.Trim();
                                        Common.strPasswordUser = txtPassword.Text.Trim();
                                        Common.iID = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());

                                        DateTime dtLastDate = Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                        DateTime dtPassTime = Convert.ToDateTime(datuser.Tables[0].Rows[0]["PassTime"]);

                                        //lay du lieu de ghi log
                                        DateTime dtDateLogin = DateTime.Now;
                                        string strContent = "Log Off";
                                        int iLoglevel = 1;
                                        string strWorked = "Login";
                                        string strTable = "USERS";
                                        string strOld_value = "";
                                        string strNew_value = "";
                                        //goi ham ghilog
                                        objLog.GhiLogUser(dtDateLogin, txtUser.Text.Trim(), strContent, 
                                            iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                                        //Sau thoi gian n ngay khong dang nhap, user se bi khoa. n la value cau tham so NumLoginDay trong bang sysvar
                                        DataSet dsSysTemp = new DataSet();
                                        SYSVARController objSysvarControl = new SYSVARController();
                                        dsSysTemp = objSysvarControl.GetSYSVAR_NAME("NumLoginDay", "SYSTEM");

                                        if ((DateTime.Now - dtLastDate).Days >= Convert.ToInt32(dsSysTemp.Tables[0].Rows[0]["Value"]))
                                        {
                                            //Khoa nguoi dung
                                            if (Common.Userid.Trim() == Common.pUser_Addmin.Trim())//users admin
                                            {
                                                return false;
                                            }
                                            else //user thuong
                                            {
                                                if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_LOCKED) != 1)
                                                {
                                                    MessageBox.Show("Error occured when updating database", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                                MessageBox.Show("You have not logged in for " + dsSysTemp.Tables[0].Rows[0]["Value"].ToString() + " days. Your account was locked!", Common.sCaption);
                                                datuser.Dispose();
                                                this.DialogResult = DialogResult.Cancel;
                                                return false;
                                            }
                                        }

                                        //Neu nguoi dung dang nhap lan dau: status=pending
                                        if (strUserStatus == Common.GW_USERS_STATUS_PENDING)
                                        {
                                            dlgResult = MessageBox.Show("You must change password at the first time login. Do you want to change password?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (dlgResult == DialogResult.Yes)
                                            {
                                                if (!ChangePass())
                                                {
                                                    MessageBox.Show("You have not changed password successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                                //Doi status = Active
                                                if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_ACTIVE) != 1)
                                                {
                                                    MessageBox.Show("Error occured when updating database", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                this.DialogResult = DialogResult.Cancel;
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }
                                        }

                                        //Nguoi dung phai doi Pass sau n thang
                                        dsSysTemp = objSysvarControl.GetSYSVAR_NAME("sysPasswordtime", "SYSTEM");
                                        //Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                        int isysPasswordtime = Convert.ToInt16(dsSysTemp.Tables[0].Rows[0]["Value"].ToString());


                                        int iMonth = (DateTime.Now.Year - dtPassTime.Year) * 12 + (DateTime.Now.Month - dtPassTime.Month) + (DateTime.Now.Day - dtPassTime.Day >= 0 ? 1 : (-1));

                                        if (iMonth >= isysPasswordtime)
                                        {
                                            dlgResult = MessageBox.Show("Your current password is expired. Do you want to change password?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (dlgResult == DialogResult.Yes)
                                            {
                                                if (!ChangePass())
                                                {
                                                    MessageBox.Show("You have not changed password successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                this.DialogResult = DialogResult.Cancel;
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }
                                        }

                                        //Kiem tra thoi gian dang nhap cuoi
                                        //Update Lasttime truy cap 

                                        int iID1 = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());
                                        Common.iID = iID1;
                                        string userid = datuser.Tables[0].Rows[0]["USERID"].ToString();
                                        Common.Userid = userid;
                                        Common.strUsername = strUser;

                                        //kiem tr cac nhom do co cung mot kenh thanh toan khong
                                        DataSet datGroupss = new DataSet();
                                        datGroupss = objcontrolgroup.GetGroup_Gwtype(userid);
                                        if (datGroupss.Tables[0].Rows.Count == 0)//khong vao kenh thanh toan nao
                                        {
                                            Common.CountGWtype = 0;
                                        }
                                        if (datGroupss.Tables[0].Rows.Count == 1)// vao cung mot kenh thanh toan
                                        {
                                            Common.CountGWtype = 1;
                                            string strGwtype = datGroupss.Tables[0].Rows[0]["GWTYPE"].ToString();
                                            Common.strGWTYPE = strGwtype;
                                        }
                                        if (datGroupss.Tables[0].Rows.Count > 1)// vao nhieu kenh thanh toan khaca nhau
                                        {
                                            Common.CountGWtype = 2;
                                        }
                                        datGroupss.Dispose();
                                        this.DialogResult = DialogResult.OK;
                                    }
                                    else if (strUserStatus == Common.GW_USERS_STATUS_LOCKED)
                                    {
                                        MessageBox.Show("User is locked!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return false;
                                    }
                                    this.Close();
                                }
                            }
                            #endregion
                        }
                        else //users nay dang co nguoi su dung
                        {
                            if (UserLog.Trim() == "I/" + Common.IpLocal)
                            {
                                #region  Common.iLogout == 1
                                if (Common.iLogout == 1)
                                {
                                    strUserStatus = datuser.Tables[0].Rows[0]["STATUS"].ToString();
                                    DialogResult dlgResult = new DialogResult();
                                    if ((strUserStatus == Common.GW_USERS_STATUS_ACTIVE) || (strUserStatus == Common.GW_USERS_STATUS_PENDING))
                                    {
                                        DateTime dat = DateTime.Now;
                                        Common.Userid = txtUser.Text.Trim();
                                        Common.strPasswordUser = txtPassword.Text.Trim();
                                        Common.iID = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());

                                        DateTime dtLastDate = Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                        DateTime dtPassTime = Convert.ToDateTime(datuser.Tables[0].Rows[0]["PassTime"]);

                                        //lay du lieu de ghi log
                                        DateTime dtDateLogin = DateTime.Now;
                                        string strContent = "Log Off";
                                        int iLoglevel = 1;
                                        string strWorked = "Login";
                                        string strTable = "USERS";
                                        string strOld_value = "";
                                        string strNew_value = "";
                                        //goi ham ghilog
                                        objLog.GhiLogUser(dtDateLogin, txtUser.Text.Trim(), strContent, iLoglevel, 
                                            strWorked, strTable, strOld_value, strNew_value);

                                        //Sau thoi gian n ngay khong dang nhap, user se bi khoa. n la value cau tham so NumLoginDay trong bang sysvar
                                        DataSet dsSysTemp = new DataSet();
                                        SYSVARController objSysvarControl = new SYSVARController();
                                        dsSysTemp = objSysvarControl.GetSYSVAR_NAME("NumLoginDay", "SYSTEM");

                                        if ((DateTime.Now - dtLastDate).Days >= Convert.ToInt32(dsSysTemp.Tables[0].Rows[0]["Value"]))
                                        {
                                            //Khoa nguoi dung
                                            if (Common.Userid.Trim() == Common.pUser_Addmin.Trim())//users admin
                                            {
                                                return false;
                                            }
                                            else//neu la user binh thuong
                                            {
                                                if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_LOCKED) != 1)
                                                {
                                                    MessageBox.Show("Error occured when updating database", Common.sCaption);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                                MessageBox.Show("You have not logged in for " + dsSysTemp.Tables[0].Rows[0]["Value"].ToString() + " days. Your account was locked!", Common.sCaption);
                                                datuser.Dispose();
                                                this.DialogResult = DialogResult.Cancel;
                                                return false;
                                            }
                                        }

                                        //Neu nguoi dung dang nhap lan dau: status=pending
                                        if (strUserStatus == Common.GW_USERS_STATUS_PENDING)
                                        {
                                            dlgResult = MessageBox.Show("You must change password at the first time login. Do you want to change password?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (dlgResult == DialogResult.Yes)
                                            {
                                                if (!ChangePass())
                                                {
                                                    MessageBox.Show("You have not changed password successfully!", Common.sCaption);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                                //Doi status = Active
                                                if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_ACTIVE) != 1)
                                                {
                                                    MessageBox.Show("Error occured when updating database", Common.sCaption);
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }

                                            }
                                            else
                                            {
                                                this.DialogResult = DialogResult.Cancel;
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }

                                        }


                                        //Nguoi dung phai doi Pass sau n thang
                                        dsSysTemp = objSysvarControl.GetSYSVAR_NAME("sysPasswordtime", "SYSTEM");
                                        //Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                        int isysPasswordtime = Convert.ToInt16(dsSysTemp.Tables[0].Rows[0]["Value"].ToString());


                                        int iMonth = (DateTime.Now.Year - dtPassTime.Year) * 12 + (DateTime.Now.Month - dtPassTime.Month) + (DateTime.Now.Day - dtPassTime.Day >= 0 ? 1 : (-1));

                                        if (iMonth >= isysPasswordtime)
                                        {
                                            
                                            if (!ChangePass())
                                            {
                                                MessageBox.Show("You have not changed password successfully!", Common.sCaption);
                                                datuser.Dispose();
                                                dsSysTemp.Dispose();
                                                return false;
                                            }
                                            
                                        }

                                        //Kiem tra thoi gian dang nhap cuoi
                                        //Update Lasttime truy cap 

                                        int iID1 = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());
                                        Common.iID = iID1;
                                        string userid = datuser.Tables[0].Rows[0]["USERID"].ToString();
                                        Common.Userid = userid;
                                        Common.strUsername = strUser;

                                        //kiem tr cac nhom do co cung mot kenh thanh toan khong
                                        DataSet datGroupss = new DataSet();
                                        datGroupss = objcontrolgroup.GetGroup_Gwtype(userid);
                                        if (datGroupss.Tables[0].Rows.Count == 0)//khong vao kenh thanh toan nao
                                        {
                                            Common.CountGWtype = 0;
                                        }
                                        if (datGroupss.Tables[0].Rows.Count == 1)// vao cung mot kenh thanh toan
                                        {
                                            Common.CountGWtype = 1;
                                            string strGwtype = datGroupss.Tables[0].Rows[0]["GWTYPE"].ToString();
                                            Common.strGWTYPE = strGwtype;
                                        }
                                        if (datGroupss.Tables[0].Rows.Count > 1)// vao nhieu kenh thanh toan khaca nhau
                                        {
                                            Common.CountGWtype = 2;
                                        }
                                        datGroupss.Dispose();
                                        this.DialogResult = DialogResult.OK;
                                    }
                                    else if (strUserStatus == Common.GW_USERS_STATUS_LOCKED)
                                    {
                                        MessageBox.Show("User is locked!", Common.sCaption);
                                        return false;
                                    }
                                }
                                #endregion
                                #region Common.iLogout == 5
                                else if (Common.iLogout == 5)
                                {
                                    if (txtUser.Text.Trim() == Common.strUserID1)
                                    {
                                        Common.iLogin = 10;
                                        this.Close();
                                    }
                                    else
                                    {
                                        Common.iLogin = 11;
                                        strUserStatus = datuser.Tables[0].Rows[0]["STATUS"].ToString();
                                        DialogResult dlgResult = new DialogResult();
                                        if ((strUserStatus == Common.GW_USERS_STATUS_ACTIVE) || (strUserStatus == Common.GW_USERS_STATUS_PENDING))
                                        {
                                            DateTime dat = DateTime.Now;
                                            Common.Userid = txtUser.Text.Trim();
                                            Common.strPasswordUser = txtPassword.Text.Trim();
                                            Common.iID = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());

                                            DateTime dtLastDate = Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                            DateTime dtPassTime = Convert.ToDateTime(datuser.Tables[0].Rows[0]["PassTime"]);

                                            //lay du lieu de ghi log
                                            DateTime dtDateLogin = DateTime.Now;
                                            string strContent = "Log Off";
                                            int iLoglevel = 1;
                                            string strWorked = "Login";
                                            string strTable = "USERS";
                                            string strOld_value = "";
                                            string strNew_value = "";
                                            //goi ham ghilog
                                            objLog.GhiLogUser(dtDateLogin, txtUser.Text.Trim(), strContent, iLoglevel,
                                                strWorked, strTable, strOld_value, strNew_value);

                                            //Sau thoi gian n ngay khong dang nhap, user se bi khoa. n la value cau tham so NumLoginDay trong bang sysvar
                                            DataSet dsSysTemp = new DataSet();
                                            SYSVARController objSysvarControl = new SYSVARController();
                                            dsSysTemp = objSysvarControl.GetSYSVAR_NAME("NumLoginDay", "SYSTEM");

                                            if ((DateTime.Now - dtLastDate).Days >= Convert.ToInt32(dsSysTemp.Tables[0].Rows[0]["Value"]))
                                            {
                                                //Khoa nguoi dung
                                                if (Common.Userid.Trim() == Common.pUser_Addmin.Trim())//users admin
                                                {
                                                    return false;
                                                }
                                                else //user thuong
                                                {
                                                    if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_LOCKED) != 1)
                                                    {
                                                        MessageBox.Show("Error occured when updating database", Common.sCaption);
                                                        datuser.Dispose();
                                                        dsSysTemp.Dispose();
                                                        return false;
                                                    }
                                                    MessageBox.Show("You have not logged in for " + dsSysTemp.Tables[0].Rows[0]["Value"].ToString() + " days. Your account was locked!", Common.sCaption);
                                                    datuser.Dispose();
                                                    this.DialogResult = DialogResult.Cancel;
                                                    return false;
                                                }
                                            }

                                            //Neu nguoi dung dang nhap lan dau: status=pending
                                            if (strUserStatus == Common.GW_USERS_STATUS_PENDING)
                                            {
                                                dlgResult = MessageBox.Show("You must change password at the first time login. Do you want to change password?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (dlgResult == DialogResult.Yes)
                                                {
                                                    if (!ChangePass())
                                                    {
                                                        MessageBox.Show("You have not changed password successfully!", Common.sCaption);
                                                        datuser.Dispose();
                                                        dsSysTemp.Dispose();
                                                        return false;
                                                    }
                                                    //Doi status = Active
                                                    if (objcontroluser.UpdateUSERSTATUS(txtUser.Text.Trim(), Common.GW_USERS_STATUS_ACTIVE) != 1)
                                                    {
                                                        MessageBox.Show("Error occured when updating database", Common.sCaption);
                                                        datuser.Dispose();
                                                        dsSysTemp.Dispose();
                                                        return false;
                                                    }

                                                }
                                                else
                                                {
                                                    this.DialogResult = DialogResult.Cancel;
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                            }

                                            //Nguoi dung phai doi Pass sau n thang
                                            dsSysTemp = objSysvarControl.GetSYSVAR_NAME("sysPasswordtime", "SYSTEM");
                                            //Convert.ToDateTime(datuser.Tables[0].Rows[0]["LastDate"]);
                                            int isysPasswordtime = Convert.ToInt16(dsSysTemp.Tables[0].Rows[0]["Value"].ToString());


                                            int iMonth = (DateTime.Now.Year - dtPassTime.Year) * 12 + (DateTime.Now.Month - dtPassTime.Month) + (DateTime.Now.Day - dtPassTime.Day >= 0 ? 1 : (-1));

                                            if (iMonth >= isysPasswordtime)
                                            {
                                                dlgResult = MessageBox.Show("Your current password is expired. Do you want to change password?", Common.sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (dlgResult == DialogResult.Yes)
                                                {
                                                    if (!ChangePass())
                                                    {
                                                        MessageBox.Show("You have not changed password successfully!", Common.sCaption);
                                                        datuser.Dispose();
                                                        dsSysTemp.Dispose();
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    this.DialogResult = DialogResult.Cancel;
                                                    datuser.Dispose();
                                                    dsSysTemp.Dispose();
                                                    return false;
                                                }
                                            }

                                            //Kiem tra thoi gian dang nhap cuoi
                                            //Update Lasttime truy cap 

                                            int iID1 = Convert.ToInt32(datuser.Tables[0].Rows[0]["ID"].ToString());
                                            Common.iID = iID1;
                                            string userid = datuser.Tables[0].Rows[0]["USERID"].ToString();
                                            Common.Userid = userid;
                                            Common.strUsername = strUser;

                                            //kiem tr cac nhom do co cung mot kenh thanh toan khong
                                            DataSet datGroupss = new DataSet();
                                            datGroupss = objcontrolgroup.GetGroup_Gwtype(userid);
                                            if (datGroupss.Tables[0].Rows.Count == 0)//khong vao kenh thanh toan nao
                                            {
                                                Common.CountGWtype = 0;
                                            }
                                            if (datGroupss.Tables[0].Rows.Count == 1)// vao cung mot kenh thanh toan
                                            {
                                                Common.CountGWtype = 1;
                                                string strGwtype = datGroupss.Tables[0].Rows[0]["GWTYPE"].ToString();
                                                Common.strGWTYPE = strGwtype;
                                            }
                                            if (datGroupss.Tables[0].Rows.Count > 1)// vao nhieu kenh thanh toan khaca nhau
                                            {
                                                Common.CountGWtype = 2;
                                            }
                                            datGroupss.Dispose();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else if (strUserStatus == Common.GW_USERS_STATUS_LOCKED)
                                        {
                                            MessageBox.Show("User is locked!", Common.sCaption);
                                            return false;
                                        }
                                        this.Close();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("This user is already logged in!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(" UserID or Password is not correct!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (txtUser.Text == Common.pUser_Addmin)
                        {
                            iLockNum = 0;
                            return false;
                        }
                        else
                        {
                            ++iLogNum;
                            if (iLogNum == 1)
                            {
                                strLastUserID = txtUser.Text;
                            }
                            if (txtUser.Text == strLastUserID)
                            {
                                ++iLockNum;
                            }
                            else
                            {
                                strLastUserID = txtUser.Text;
                                iLockNum = 0;
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            { 
            }
            return true;
        }
        
        /*---------------------------------------------------------------
        * Method           : frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        * Muc dich         : bat su kien thoat khoi form
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit!", Common.sCaption);
                    if (Common.iLogout == 5)
                    {
                        if (e.Cancel == false)//thoat
                        {
                            Common.iClose = 5;
                        }
                    }                   
                }
                else
                {
                    if (Common.iLogout == 5 && Common.bTrue == true)
                    {                       
                        Common.iLogin = 10;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private bool ChangePass()
        {
            frmChangepass frmChangePass = new frmChangepass();
            frmChangePass.ShowDialog();
            if (frmChangePass.DialogResult == DialogResult.Cancel)
                return false;
            return true;

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            if (Common.iLogout == 5)//khi goi for Logout ra
            {
                strSucess = true;
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
     

    }
}
