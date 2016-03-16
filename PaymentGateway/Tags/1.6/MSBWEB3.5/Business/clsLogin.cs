using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using BIDVWEB.Comm;

namespace BIDVWEB.Business
{
    public class clsLogin
    {
        UserEncrypt ObjEncrypt = new UserEncrypt();
        clsDatatAccess objDataAccess = new clsDatatAccess();        

        #region Properties

        //Private Properties 
        public string strError;
        private string strUserName;
        private string strPassword;
        private int intCountTimeLogin;
        private int intStatus;
        private DateTime strLastDate;
        private int intLogFail;
        
        //Public Properties 
        public string Error
        {
            get { return strError; }
            set { strError = value; }
        }

        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }

        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public int CountTimeLogin
        {
            get { return intCountTimeLogin; }
            set { intCountTimeLogin = value; }
        }

        public int iStatus
        {
            get { return intStatus; }
            set { intStatus = value; }
        }

        public int iLogFail
        {
            get { return intLogFail; }
            set { intLogFail = value; }
        }

        public DateTime sLastDate
        {
            get { return strLastDate; }
            set { strLastDate = value; }
        }

        #endregion
        

        #region Methods
        //Constructor methods

        //Set null
        public clsLogin()
        {            
            strError = "";
            strUserName = "";
            strPassword = "";
            intCountTimeLogin = 0;
            intStatus = 0;
        }

        ///////////////////////////////////////////////////////////////////
        //Mo ta:        Ham check User, password
        //Ngay tao:     05/2008
        //Nguoi tao:    All
        //Dau vao:      txtUser: 
        //              txtPassword
        //Dau ra:       True: Neu kiem tra thanh cong
        //              False: Nguoc lai            
        ///////////////////////////////////////////////////////////////////
        public bool CheckUserAndPassword(string txtUserID, string txtPassword)
        {
            strUserName = txtUserID;
            strPassword = txtPassword;
            bool boolUser = true;
            bool boolPassword = true;
            bool boolResult = true;            
           
            DataSet ds = new DataSet();
            DataTable dtTable = new DataTable();
            DataRow dtRow;
            strError = "";                
            
            try
            {
                string sql = "SELECT BRANCH,USERID,USERNAME,PASSWORD," + 
                "STATUS,PASSTIME,LASTDATE,COUNTTIME " +
                "FROM USERS WHERE USERID = '" + txtUserID.ToString() 
                    + "' AND ROWNUM=1 ";
                ds =objDataAccess.dsGetDataSourceByStr(sql, "");
                if (ds != null)
                {
                    dtTable = ds.Tables[0];
                }
                else
                {
                    strError = objDataAccess.strError;
                    return false;
                }                   
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }                       

            if (dtTable.Rows.Count < 1)
            {
                boolUser = false;
            }
            else
            {
                dtRow = dtTable.Rows[0];
                if (ObjEncrypt.Encrypt(strPassword.Trim()) != dtRow["PASSWORD"].ToString())
                {
                    boolPassword = false;
                }
                //Lay trang thai cua userid
                iStatus = Convert.ToInt16(dtRow["STATUS"]);
                //Lay so lan da dang nhap                           
                intCountTimeLogin = Convert.ToInt16(dtRow["COUNTTIME"].ToString().Trim()); 
                //Lay ngay dang nhap cuoi cung
                sLastDate = Convert.ToDateTime(dtRow["LASTDATE"].ToString());
            }

            
            if (!boolUser)
            {
                strError = "NSD không có trong hệ thống";
                iLogFail = 1;
                return false;
            }
            if (!boolPassword)
            {
                strError = "Mật khẩu không đúng";
                iLogFail = 1;
                return false;
            }
            if (iStatus == 3)
            {
                strError = "NSD bị khóa";
                return false;
            }                       
            return boolResult;
        }

        ///////////////////////////////////////////////////////////////////
        //Mota:         update truong counttime trung bang user 
        //Ngay tao:     07/2008
        //Nguoi tao:    All
        //Dau vao:      txtUser
        //Dau ra:       True: Cap nhat thanh cong
        //              False:  Khong thanh cong
        ///////////////////////////////////////////////////////////////////
        public bool UpdateCoutTimeLogin(string txtUser)
        {
            bool boolResult = true;
            int intCountTime = 1;

            try
            {
                string sql = "UPDATE USERS SET COUNTTIME = '" + intCountTime.ToString() + "' WHERE USERID = '" + txtUser.ToString().Trim() + "'";
                if (!objDataAccess.ExecuteSQL(sql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }            
            return boolResult;
        }


        ///////////////////////////////////////////////////////////////////
        //Mo ta:            Ham kiem tra password cu
        //Ngay tao:         05/2008
        //Nguoi tao:        All
        //Dau vao:          Username:
        //                  oldpassword:
        //Dau ra:           True: Thanh cong
        //                  False: Khong thanh cong
        ///////////////////////////////////////////////////////////////////
        private bool CheckOldPassword(string username, string oldpassword)
        {
            bool boolResult = true;
            bool OkUser = true;
            bool OkOldPassword = true;
         
            DataTable dtTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {               
                string sql = "SELECT PASSWORD FROM USERS WHERE  USERNAME = '" + username.ToString().Trim() + "'";
                ds = objDataAccess.dsGetDataSourceByStr(sql, "");
                if (ds != null)
                {
                    dtTable = ds.Tables[0];
                }
                else
                {
                    strError = objDataAccess.strError;
                    return false;
                }                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                boolResult = false;
            }

            ds.Dispose();
            if (dtTable.Rows.Count < 1)
            {
                OkUser = false;
            }
            else
            {
                foreach (DataRow dtRow in dtTable.Rows)
                {
                    if (ObjEncrypt.Encrypt(oldpassword) != dtRow["PASSWORD"].ToString())
                    {
                        OkOldPassword = false;
                    }
                }
            }


            if (!OkUser)
            {
                strError = "Tên đăng nhập không có trong hệ thống";
                return false;
            }
            if (!OkOldPassword)
            {
                strError = "Mật khẩu cũ không đúng";
                return false;
            }

            return boolResult;
        }


        ///////////////////////////////////////////////////////////////////
        //Mo ta:            Ham cap nhat password moi
        //Ngay tao:         05/2008
        //Nguoi tao:        All
        //Dau vao:          username:
        //                  newpassword
        //Dau ra:           True: Cap nhat thanh cong
        //                  False: Khong thanh cong
        ///////////////////////////////////////////////////////////////////
        private bool SetNewPassword(string username, string newpassword)
        {
            bool boolResult = true;            
            try
            {               
                string sql = "UPDATE  USERS SET PASSWORD = '" + 
                    ObjEncrypt.Encrypt(newpassword.ToString().Trim()) +
                    "',PASSTIME = To_Date('" + DateTime.Now + "','MM/DD/YYYY') WHERE USERNAME = '" + username.ToString().Trim() + "'";
                if (!objDataAccess.ExecuteSQL(sql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }                
            }
            catch (Exception ex)
            {
                strError = "Error:" + ex.Message;
                boolResult = false;
            }           
            return boolResult;
        }


        ///////////////////////////////////////////////////////////////////
        //Mo ta:            Ham doi password
        //Ngay tao:         05/2008
        //Nguoi tao:        All
        //Dau vao:          username:
        //                  oldpassword:
        //                  newpassword:
        //Dau ra:           True: Doi pass thanh cong
        //                  False: Doi pass khong thang cong
        ///////////////////////////////////////////////////////////////////
        public bool ChangePassword(string username, string oldpassword, string newpassword)
        {
            bool boolResult = true;
            if (CheckOldPassword(username, oldpassword))
            {
                boolResult = SetNewPassword(username, newpassword);
            }
            else
            {
                boolResult = false;
            }
            return boolResult;
        }

        #endregion

    }
}
