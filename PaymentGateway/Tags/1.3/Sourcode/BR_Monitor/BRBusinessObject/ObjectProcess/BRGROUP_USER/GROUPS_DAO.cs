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
using BR.DataAccess;


//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' Update nguyen duc quy 29/05/2008
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GROUPSDP
	{
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        //private OracleDataReader myReader;
		
		public GROUPSDP()
		{
		}
		public static GROUPSDP Instance()
		{
			return new GROUPSDP();
		}
		
		public int AddGROUPS(GROUPSInfo objTable)
		{
            int iResult = 0;
            string strSql = "Insert into GROUPS  (GROUPNAME,ISSUPERVISOR,GWTYPE,DEPARTMENT,DESCRIPTION) values (:pGROUPNAME,:pISSUPERVISOR,:pGWTYPE,:pDEPARTMENT,:pDESCRIPTION)";
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pGROUPNAME",OracleType.VarChar,20),
                                                new OracleParameter("pISSUPERVISOR",OracleType.Number,1) ,
                                                new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                                new OracleParameter("pDEPARTMENT",OracleType.VarChar,10),
                                                new OracleParameter("pDESCRIPTION",OracleType.VarChar,100)};

               
                oraParas[0].Value = objTable.GROUPNAME;
                oraParas[1].Value = objTable.ISSUPERVISOR;
                oraParas[2].Value = objTable.GWTYPE;
                oraParas[3].Value = objTable.DEPARTMENT;
                oraParas[4].Value = objTable.DESCRIPTION;                
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
                return iResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }            
		}
		
		public int UpdateGROUPS(GROUPSInfo objTable)
		{
            try
            {
                string strSql = "Update GROUPS  set GROUPNAME=:pGROUPNAME, ISSUPERVISOR=:pISSUPERVISOR,GWTYPE=:pGWTYPE,DEPARTMENT=:pDEPARTMENT,DESCRIPTION=:pDESCRIPTION where GROUPID=:pGROUPID";
                OracleParameter[] oraParas ={new OracleParameter("pGROUPID",OracleType.Number,10),
                                                new OracleParameter("pGROUPNAME",OracleType.VarChar,20),
                                                new OracleParameter("pISSUPERVISOR",OracleType.Number,1) ,
                                                new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                                new OracleParameter("pDEPARTMENT",OracleType.VarChar,10),
                                                new OracleParameter("pDESCRIPTION",OracleType.VarChar,100)
                                                };
                oraParas[0].Value = objTable.GROUPID;
                oraParas[1].Value = objTable.GROUPNAME;
                oraParas[2].Value = objTable.ISSUPERVISOR;
                oraParas[3].Value = objTable.GWTYPE;
                oraParas[4].Value = objTable.DEPARTMENT;
                oraParas[5].Value = objTable.DESCRIPTION;
                
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);

                }
                catch (Exception)
                {
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
                return 1;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
		}

        public DataSet DeleteGROUPS(int groupid,string pMenuid)
		{
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "delete  from group_menu where groupid='" + groupid + "' and menuid='" + pMenuid + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                
            }
            catch 
            {
                return null;
            }
		
		}
        public DataSet DeleteGROUPS_(int groupid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "delete  from groups where groupid='" + groupid + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }
        public DataSet DeleteGROUPS_Menu(int groupid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "delete  from Group_Menu where groupid='" + groupid + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }
        // ham lay du lieu day len treeview
        public DataSet GetGROUP()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select GROUPNAME from groups ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGROUP_USER(string pUSERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(g.groupname) from groups g,group_user gu where g.groupid=gu.groupid and gu.userid='" + pUSERID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGROUP_USER1(string pUSERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(g.groupname),g.groupid from groups g,user_groups gu where g.groupid=gu.groupid and gu.userid='" + pUSERID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGROUPNAME(string pGROUPNAME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select * from groups g where trim(g.groupname)='" + pGROUPNAME.Trim() + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetGroupID(int pGROUPID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select * from groups g where g.groupid='" + pGROUPID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);                
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetGroup_IsSupervisor(string strUserID,string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct g.issupervisor from groups g,user_groups u where g.groupid=u.groupid and u.userid='" + strUserID + "'   and trim(g.gwtype) = '" + pGWTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetGROUP_ID()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(GROUPNAME),GROUPID from groups ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGROUP_TYPE()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(GWTYPE) from groups ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
       
        public DataSet GetGROUP_DEPARTMENT()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(DEPTID) from groups ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               

            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGroup_Depart(string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(GROUPNAME) from groups g where g.gwtype='" + pGWTYPE + "' ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGroup_Gwtype(string pUserid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(gr.gwtype) from groups gr where gr.groupname in (select distinct (GROUPNAME) from GROUPS g, USERS u, USER_GROUPS ug where g.groupid = ug.groupid  and u.userid = ug.userid and u.userid = :pUSERID)";
            OracleParameter[] oraParas ={new OracleParameter("pUSERID",OracleType.VarChar,20) };
            oraParas[0].Value = pUserid;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
            }
            catch 
            {
                return null;
            }
        }
	}
	
	
}
