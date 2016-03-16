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
	public class GROUPS_DAO
	{
        private OracleConnection oraConn ;                
        private clsConnection objConn = new clsConnection();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        public string strError = "";

        public GROUPS_DAO()
		{
		}
        public static GROUPS_DAO Instance()
		{
            return new GROUPS_DAO();
		}
		
		public int AddGROUPS(GROUPSInfo objTable)
		{
            int iResult;
            //string strSql = "Insert into GROUPS (GROUPNAME,ISADMIN,ISSUPERVISOR,GWTYPE,DEPARTMENT,DESCRIPTION,BRANCHID) "
            //    + "values (:pGROUPNAME,:pISADMIN,:pISSUPERVISOR,:pGWTYPE,:pDEPARTMENT,:pDESCRIPTION,:pBRANCHID)";
            string strSql = "INSERT INTO GROUPS (GROUPNAME,ISADMIN,GWTYPE,DESCRIPTION,BRANCHID)"
                + " VALUES (:pGROUPNAME,:pISADMIN,:pGWTYPE,:pDESCRIPTION,:pBRANCHID)";
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pGROUPNAME",OracleType.NVarChar,20),
                                            new OracleParameter("pISADMIN",OracleType.Number,1) ,
                                            new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                            new OracleParameter("pDESCRIPTION",OracleType.NVarChar,100),
                                            new OracleParameter("pBRANCHID",OracleType.VarChar,5)};
                               
                oraParas[0].Value = objTable.GROUPNAME;
                oraParas[1].Value = objTable.ISADMIN;                
                oraParas[2].Value = objTable.GWTYPE;                
                oraParas[3].Value = objTable.DESCRIPTION;
                oraParas[4].Value = objTable.BRANCHID;
               
                oraConn = objConn.Connect();
                if (oraConn == null)                              
                    return -1;                    
                iResult = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iResult;                
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
		
		public int UpdateGROUPS(GROUPSInfo objTable)
		{

            int iBool;
            try
            {
                string strSql = "UPDATE GROUPS SET GROUPNAME=:pGROUPNAME, ISADMIN=:pISADMIN," +
                    "GWTYPE=:pGWTYPE,DESCRIPTION=:pDESCRIPTION WHERE GROUPID=:pGROUPID";
                OracleParameter[] oraParas ={new OracleParameter("pGROUPID",OracleType.Number,10),
                                                new OracleParameter("pGROUPNAME",OracleType.NVarChar,20),
                                                new OracleParameter("pISADMIN",OracleType.Number,1) ,                                                
                                                new OracleParameter("pGWTYPE",OracleType.VarChar,10),                                                
                                                new OracleParameter("pDESCRIPTION",OracleType.NVarChar,100)
                                                };
                oraParas[0].Value = objTable.GROUPID;
                oraParas[1].Value = objTable.GROUPNAME;
                oraParas[2].Value = objTable.ISADMIN;                
                oraParas[3].Value = objTable.GWTYPE;                
                oraParas[4].Value = objTable.DESCRIPTION;
                
                oraConn = objConn.Connect();
                if (oraConn == null)                                            
                    return -1;
                iBool =clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
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

        public DataSet DeleteGROUPS(int groupid,string pMenuid)
		{
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                oraConn.Close();
                oraConn.Dispose();
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "DELETE  FROM group_menu WHERE groupid='" + 
                groupid + "' and menuid='" + pMenuid + "'";

            try
            {
                return clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                oraConn.Close();
                oraConn.Dispose();
                return null;
            }
		
		}

        public DataSet DeleteGROUPS_(int groupid)
        {
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                oraConn.Close();
                oraConn.Dispose();
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "delete  from groups where groupid='" + groupid + "'";

            try
            {
                return clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                oraConn.Close();
                oraConn.Dispose();
                return null;
            }
        }

        // ham lay du lieu day len treeview
        public DataSet GetGROUP()
        {           
            string strSQL = "select GROUPNAME from groups ";
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


        public DataSet GetGROUP_USER(string pUSERID)
        {            
            string strSQL = "select distinct(g.groupname) from groups g,group_user gu " + 
                " where g.groupid=gu.groupid and gu.userid='" + pUSERID + "'";
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
        public DataSet GetGROUP_USER1(string pUSERID)
        {            
            string strSQL = "select distinct(g.groupname) from groups g,user_groups gu " + 
                " where g.groupid=gu.groupid and gu.userid='" + pUSERID + "'";
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

        public DataSet GetGROUPNAME(string pGROUPNAME)
        {
            string strSQL = "select * from groups g where g.groupname='" + pGROUPNAME + "'";

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

        public DataSet GetGroupID(int pGROUPID)
        {
            string strSQL = "select * from groups g where g.groupid='" + pGROUPID + "'";

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

        public DataSet GetGroup_IsSupervisor(string strUserID)
        {
            string strSQL = "select distinct g.issupervisor from groups g,user_groups u " + 
                " where g.groupid=u.groupid and u.userid='" + strUserID + "'";

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

        public DataSet GetGROUP_ID()
        {
            string strSQL = "select distinct(GROUPNAME) from groups ";

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

        public DataSet GetGROUP_TYPE()
        {            
            string strSQL = "select distinct(GWTYPE) from groups ";

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
       
        public DataSet GetGROUP_DEPARTMENT()
        {            
            string strSQL = "select distinct(DEPTID) from groups ";

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


        public DataSet GetGroup_Depart(string pGWTYPE)
        {            
            string strSQL = "select distinct(GROUPNAME) from groups g where g.gwtype='" + pGWTYPE + "'";

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


        public DataSet GetGroup_Gwtype(string pUserid)
        {
            DataSet ds = new DataSet();
            oraConn = objConn.Connect();
            if (oraConn == null)            
                return null;            

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(gr.gwtype) from groups gr " +
                " where gr.groupname in (select distinct (GROUPNAME) " +
                " from GROUPS g, USERS u, USER_GROUPS ug where g.groupid = ug.groupid " +
                "  and u.userid = ug.userid and u.userid = :pUSERID)";
            OracleParameter[] oraParas ={new OracleParameter("pUSERID",OracleType.VarChar,10) };
            oraParas[0].Value = pUserid;

            try
            {
                ds= clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
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


        //Ham kiem tra group admin/////////////////////////////////////
        //Mo ta:        Ham kiem tra group admin
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Khong cho xoa
        ///////////////////////////////////////////////////////////////
        public int CHECK_GROUP_ADMIN(string pGroupID)
        {
            DataSet ds = new DataSet();
            string strSQL = "select u.GROUPID from GROUPS u where Trim(u.GROUPID)=" + pGroupID.Trim() +
                " AND upper(u.GROUPNAME) = upper('admin')";
            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

	}
	
	
}
