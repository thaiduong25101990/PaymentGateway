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
//using BR.BRLib;
using BR.DataAccess;

//' =============================================

//' Author:	Le Duc Quan (QuanLD@fpt.com.vn)
//' Detail: Nguyen Thuy Dung 
//' Create date:	28/05/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class GROUP_USERDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public GROUP_USERDP()
        {
        }
        public static GROUP_USERDP Instance()
        {
            return new GROUP_USERDP();
        }

        public int AddGROUP_USER(GROUP_USERInfo objTable)
        {
            string strSql = "Insert into USER_GROUPS (GROUPID, USERID) Values (:pGROUPID, :pUSERID)";
            OracleParameter[] oraParam ={new OracleParameter("pGROUPID",OracleType.Int32,10),
                                           new OracleParameter("pUSERID",OracleType.VarChar,20)};
            oraParam[0].Value = objTable.GROUPID;
            oraParam[1].Value = objTable.USERID;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int UpdateGROUP_USER(GROUP_USERInfo objTable)
        {
            string strSql = "Update GWBANK_MAP set GROUPID=:pGROUPID, USERID=:pUSERID where USERID= " + objTable.USERID ;
            OracleParameter[] oraParam ={new OracleParameter("pGROUPID",OracleType.Int32,10),
                                           new OracleParameter("pUSERID",OracleType.VarChar,20)};
            oraParam[0].Value = objTable.GROUPID;
            oraParam[1].Value = objTable.USERID;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int DeleteGROUP_USER(int pGROUPID,string pUSERID)
        {
            string strSql = "Delete from USER_GROUPS where USERID = '" + pUSERID + "' and GROUPID='" + pGROUPID + "'";
            
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public int DeleteGroupUser(string pUSERID)
        {
            string strSql = "Delete from USER_GROUPS where Trim(USERID) = '" + pUSERID.Trim() + "'";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public DataSet GetGROUP_USER(int USERID)
        {
            DataSet datDs = new DataSet();
            string strSql = "Select * from user_groups where Trim(userid) = '" + USERID + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                    return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet GetGroup_userDD(int GroupID,string Userid)
        {
            DataSet datDs = new DataSet();
            string strSql = "Select * from user_groups where Trim(GROUPID) = '" + GroupID + "' and Trim(USERID)='" + Userid + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataSet GetGROUPDATA(int pGROUPID, string pUSERID)
        {
            DataSet datDs = new DataSet();
            string strSql = "Select * from user_groups gu where trim(gu.userid) = '" + pUSERID.Trim() + "' and trim(gu.groupid)='" + pGROUPID + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataSet GetGroup_Gwtype(string pGwtype)
        {
            DataSet datDs = new DataSet();
            string strSql = "select g.groupname from groups g where g.gwtype='" + pGwtype + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataSet GetGroup_Gwtype1(string pGwtype)
        {
            DataSet datDs = new DataSet();
            string strSql = "select g.groupname,g.groupid from groups g where g.gwtype='" + pGwtype + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }


    }


}
