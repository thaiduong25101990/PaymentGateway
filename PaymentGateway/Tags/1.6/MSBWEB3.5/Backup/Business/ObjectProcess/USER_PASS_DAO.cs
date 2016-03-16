using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.OracleClient;
using BIDVWEB.Comm;
using BIDVWEB.Comm.DA;

namespace BIDVWEB.Business
{
	public class USER_PASS_DAO
    {
        private OracleConnection oraConn ;
        private clsConnection objConn = new clsConnection();
        private UserEncrypt Encrypt = new UserEncrypt();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        public string strError = "";

        public USER_PASS_DAO()
		{
		}
        public static USER_PASS_DAO Instance()
		{
            return new USER_PASS_DAO();
		}
		
		public int AddUSER_PASS(USER_PASSInfo objTable)
		{
            DataTable datTable = new DataTable();
            int iBool;

            try
            {
                string strSQL = "Insert into User_pass(Userid,Password,passtime) values (:pUSERID,:pPASSWORD,:pPASSTIME)";
                OracleParameter[] oraParam = {new OracleParameter("pUSERID", OracleType.VarChar, 10),
                                             new OracleParameter("pPASSWORD", OracleType.VarChar, 100),
                                             new OracleParameter("pPASSTIME", OracleType.DateTime,7)};
                oraParam[0].Value = objTable.USER_ID;
                if (!string.IsNullOrEmpty(objTable.PRE_PASS))
                    oraParam[1].Value = Encrypt.Encrypt(objTable.PRE_PASS);
                else
                    oraParam[1].Value = objTable.PRE_PASS;
                oraParam[2].Value = objTable.PASSTIME;
            
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                iBool= clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
			}
			catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
				return -1;
			}
		}
		
        //Ham update passwords gan nhat cho user 
		public int UpdateUSER_PASS(USER_PASSInfo objTable)
		{
            DataTable datTable = new DataTable();
            int iBool;

            try
            {
                string strSQL = "Update User_pass set user_id=:pUser_ID, pre_pass =:pPre_pass, passtime =:pPasstime where "  ;
                strSQL = strSQL + " User_ID = '" + objTable.USER_ID + "' and passtime = (select max(passtime) from user_pass where user_id = '" + objTable.USER_ID + "')";
              
                 OracleParameter[] oraParam = { new OracleParameter("pUser_ID", OracleType.Int32, 10),
                                             new OracleParameter("pPre_pass", OracleType.NVarChar, 100),
                                             new OracleParameter("pPasstime ", OracleType.DateTime,7)};
                
                oraParam[0].Value = objTable.USER_ID;
                oraParam[1].Value = objTable.PRE_PASS;
                oraParam[2].Value = objTable.PASSTIME;
			
                oraConn = objConn.Connect();
                iBool= clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
			}
			catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
				return -1;
			}
		}

//Ham delete toan bo passwords cua userID nay 
        public int DeleteUSER_PASS(USER_PASSInfo objTable)
		{
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                return -1;
            }

            DataTable datTable = new DataTable();
            string strSQL = "Delete from User_pass up where up.userid='" + objTable.USER_ID + "'";

			try
			{
                return clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                
			}
			catch (Exception ex)
            {
                strError = ex.Message;
				return -1;
			}
		}

//Ham get toan bo password cua userid nay
        public DataSet GetUSERS_PASS(int pUSERID)
        {
            string strSQL = "select * from User_pass up where up.user_id='" + 
                pUSERID + "'";

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        public DataSet GetUSERS_PASS_STRING(string pUSERID)
        {
            string strSQL = "select * from User_pass up where up.userid='" + pUSERID + "'";

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        public DataSet GetUSERS_PASS_NUMCHANGEPASS(string strUSERID,int iNumRow,string strCurrPass )
        {
            //string strSQL = "select * from User_pass up where up.userid='" + USERID + "'";
            string strSQL = "select u.* from (select * from user_pass where userid='" + 
                strUSERID + "' order by PassTime desc) u where rownum <= " + 
                Convert.ToString(iNumRow) + " and password='" + strCurrPass + "'";

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        public DataSet GetPRE_PASS(int pPASSTIME)
        {
            string strSQL = "select * from User_pass up where up.passtime='" + pPASSTIME + "'";

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        // Ham kiem tra x password gan nhat cua UserID nay, 
        // x:So ngay cho phep trung Password lay trong bang Sysvar
        public DataSet CheckUSERS_PASS(string pUSERID, string pPASSWORD)
        {
            DataSet ds = new DataSet();

            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)                
                    return null;                

                DataTable datTable = new DataTable();
                string strSQL = "select A.* from " + 
                    "(select UP1.* from (select UP.* from User_Pass UP " + 
                    " where UP.USERID =:pUser_ID order by UP.ID desc) UP1 " +
                    " where Rownum < (select To_number(sv.value) + 1 " +
                    " from sysvar sv where Trim(sv.varname) = 'Passtime' And " +
                    " Trim(sv.gwtype) = 'SYSTEM')) A " +
                    " WHERE A.PASSWORD = :pPre_pass";
                OracleParameter[] oraParam = { new OracleParameter("pUser_ID", OracleType.VarChar, 10),
                                             new OracleParameter("pPre_pass", OracleType.VarChar, 100)};

                oraParam[0].Value = pUSERID;
                oraParam[1].Value = Encrypt.Encrypt(pPASSWORD);
                ds= clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParam);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return null;
            }
        }
	}
	
	
}
