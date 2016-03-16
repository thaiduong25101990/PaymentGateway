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
using BR.DataAccess;
using System.Data.OracleClient;
using BR.BRLib;

//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class USER_PASSDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        BR.BRLib.UserEncrypt Encrypt = new BR.BRLib.UserEncrypt();
		
		public USER_PASSDP()
		{
		}
		public static USER_PASSDP Instance()
		{
			return new USER_PASSDP();
		}
		
		public int AddUSER_PASS(USER_PASSInfo objTable)
		{
            DataTable datTable = new DataTable();
            string strSQL = "Insert into User_pass(Userid,Password,passtime) values (:pUSERID,:pPASSWORD,:pPASSTIME)";
            OracleParameter[] oraParam = {new OracleParameter("pUSERID", OracleType.VarChar, 20),
                                         new OracleParameter("pPASSWORD", OracleType.VarChar, 100),
                                         new OracleParameter("pPASSTIME", OracleType.DateTime,7)};
            oraParam[0].Value = objTable.USER_ID;
            oraParam[1].Value = objTable.PRE_PASS;
            oraParam[2].Value = objTable.PASSTIME;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
                    //"GW_PK_USER_PASS_ACCESS.Insert_User_Pass"
			}
			catch 
			{
				return -1;
			}
		}
		
//Ham update passwords gan nhat cho user 
		public int UpdateUSER_PASS(USER_PASSInfo objTable)
		{
            DataTable datTable = new DataTable();
            string strSQL = "Update User_pass set user_id=:pUser_ID, pre_pass =:pPre_pass, passtime =:pPasstime where "  ;
            strSQL = strSQL + " User_ID = '" + objTable.USER_ID + "' and passtime = (select max(passtime) from user_pass where user_id = '" + objTable.USER_ID + "')";
          
             OracleParameter[] oraParam = { new OracleParameter("pUser_ID", OracleType.Int32, 5),
                                         new OracleParameter("pPre_pass", OracleType.NVarChar, 100),
                                         new OracleParameter("pPasstime ", OracleType.Int32,2)};
            
            oraParam[0].Value = objTable.USER_ID;
            oraParam[1].Value = objTable.PRE_PASS;
            oraParam[2].Value = objTable.PASSTIME;
			try
			{
                oraConn = connect.Connect();
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);

			}
			catch 
			{
				return -1;
			}
		}

//Ham delete toan bo passwords cua userID nay 
        public int DeleteUSER_PASS(USER_PASSInfo objTable)
		{
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }

            DataTable datTable = new DataTable();
            string strSQL = "Delete from User_pass up where up.userid='" + objTable.USER_ID + "'";

			try
			{
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                
			}
			catch 
			{
				return -1;
			}
		}

//Ham get toan bo password cua userid nay
        public DataSet GetUSERS_PASS(int pUSERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select * from User_pass up where up.user_id='" + pUSERID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);                
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetUSERS_PASS_STRING(string pUSERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select * from User_pass up where up.userid='" + pUSERID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetUSERS_PASS_NUMCHANGEPASS(string strUSERID,int iNumRow,string strCurrPass )
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            //string strSQL = "select * from User_pass up where up.userid='" + USERID + "'";
            //string strSQL = "select u.* from (select * from user_pass where userid='" + strUSERID + "' order by PassTime desc) u where rownum <= " + Convert.ToString(iNumRow) + " and password='" + strCurrPass + "'";
            string strSQL = "select A.* from " +
                    "(select UP1.* from (select UP.* from User_Pass UP " +
                    " where UP.USERID =:pUser_ID order by UP.ID desc) UP1 " +
                    " where Rownum < (select To_number(sv.value) + 1 " +
                    " from sysvar sv where Trim(sv.varname) = 'Passtime' And " +
                    " Trim(sv.gwtype) = 'SYSTEM')) A " +
                    " WHERE upper(A.PASSWORD) = upper(:pPre_pass)";
            OracleParameter[] oraParam = { new OracleParameter("pUser_ID", OracleType.VarChar, 10),
                                             new OracleParameter("pPre_pass", OracleType.VarChar, 100)};

            oraParam[0].Value = strUSERID;
            oraParam[1].Value = strCurrPass;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetPRE_PASS(int pPASSTIME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select * from User_pass up where up.passtime='" + pPASSTIME + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);              
            }
            catch 
            {
                return null;
            }
        }

// Ham kiem tra x password gan nhat cua UserID nay, x:So ngay cho phep trung Password lay trong bang Sysval
        public DataSet CheckUSERS_PASS(string pUSERID, string pPASSWORD)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select UP1.PASSWORD from (select UP.PASSWORD from User_Pass UP where UP.USERID = :pUser_ID order by UP.ID desc) UP1 where Rownum < (select To_number(sv.value) + 1 from sysvar sv where Trim(sv.varname) = 'sysPasswordtime' And Trim(sv.gwtype) = 'SYSTEM') and UP1.PASSWORD = :pPre_pass";
            OracleParameter[] oraParam = { new OracleParameter("pUser_ID", OracleType.VarChar, 5),
                                         new OracleParameter("pPre_pass", OracleType.VarChar, 100)};

            oraParam[0].Value = pUSERID;
            oraParam[1].Value = pPASSWORD;
            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch 
            {
                return null;
            }
        }
	}
	
	
}
