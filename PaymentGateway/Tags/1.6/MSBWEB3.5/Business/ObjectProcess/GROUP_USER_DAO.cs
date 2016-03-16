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
    public class GROUP_USER_DAO
    {
        OracleConnection oraConn;
        clsDatatAccess objDataAccess = new clsDatatAccess();
        clsConnection objConn = new clsConnection();
        public string strError="";

        public GROUP_USER_DAO()
        {
        }
        public static GROUP_USER_DAO Instance()
        {
            return new GROUP_USER_DAO();
        }

        public int AddGROUP_USER(GROUP_USERInfo objTable)
        {
            int iBool;
            string strSql = "Insert into USER_GROUPS (GROUPID, USERID) " + 
                " Values (:pGROUPID, :pUSERID)";
            try
            {
                OracleParameter[] oraParam ={new OracleParameter("pGROUPID",OracleType.Int32,10),
                                               new OracleParameter("pUSERID",OracleType.VarChar,10)};
                oraParam[0].Value = objTable.GROUPID;
                oraParam[1].Value = objTable.USERID;
            
                oraConn = objConn.Connect();
                if (oraConn == null)                                 
                    return -1;                
                iBool= clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
                
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return -1;
            }
        }

        public int UpdateGROUP_USER(GROUP_USERInfo objTable)
        {
            int iBool;
            string strSql = "Update GWBANK_MAP set GROUPID=:pGROUPID, USERID=:pUSERID where USERID= " 
                + objTable.USERID ;
            
            try
            {
                OracleParameter[] oraParam ={new OracleParameter("pGROUPID",OracleType.Int32,10),
                                           new OracleParameter("pUSERID",OracleType.VarChar,10)};
                oraParam[0].Value = objTable.GROUPID;
                oraParam[1].Value = objTable.USERID;

                oraConn = objConn.Connect();
                if (oraConn == null)
                {                
                    return -1;
                }
                iBool= clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
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

        public int DeleteGROUP_USER(int pGROUPID,string pUSERID)
        {
            string strSql = "Delete from USER_GROUPS where USERID = '" + pUSERID + 
                "' and GROUPID='" + pGROUPID + "'";
            
            try
            {
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return -1;
                }
                return 1;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }

        public int DeleteGroupUser(string pUSERID)
        {
            string strSql = "Delete from USER_GROUPS where Trim(USERID) = '" + pUSERID + "'";

            try
            {
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return -1;
                }
                return 1;   
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }

        public DataSet GetGROUP_USER(int USERID)
        {            
            string strSql = "Select * from user_groups where Trim(userid) = '" + USERID + "'";

            try
            {                
                return objDataAccess.dsGetDataSourceByStr( strSql, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return null;
            } 
        }
        public DataSet GetGroup_userDD(int GroupID,string Userid)
        {            
            string strSql = "Select * from user_groups where Trim(GROUPID) = '" + 
                GroupID + "' and Trim(USERID)='" + Userid + "'";
            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSql, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;               
                return null;
            } 
        }

        public DataSet GetGROUPDATA(int pGROUPID, string pUSERID)
        {            
            string strSql = "Select * from user_groups gu where gu.userid = '" + 
                pUSERID + "' and gu.groupid='" + pGROUPID + "'";
            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSql, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return null;
            } 
        }

        public DataSet GetGroup_Gwtype(string pGwtype)
        {            
            string strSql = "select g.groupname from groups g where g.gwtype='" + pGwtype + "'";
            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSql, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return null;
            } 
        }


    }


}
