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
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GROUP_MENUDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();		
		
		public GROUP_MENUDP()
		{
		}
		public static GROUP_MENUDP Instance()
		{
			return new GROUP_MENUDP();
		}
		
		public int AddGROUP_MENU(GROUP_MENUInfo objTable)
		{
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pGROUPID",OracleType.Number,10) ,
                                                new OracleParameter("pMENUID",OracleType.NVarChar,4),
                                                new OracleParameter("pISINQUIRY",OracleType.Number,1),
                                                new OracleParameter("pISDELETE",OracleType.Number,1),
                                                new OracleParameter("pISINSERT",OracleType.Number,1),
                                                new OracleParameter("pISEDIT",OracleType.Number,1)};

                oraParas[0].Value = objTable.GROUPID;
                oraParas[1].Value = objTable.MENUID;
                oraParas[2].Value = objTable.ISINQUIRY;
                oraParas[3].Value = objTable.ISDELETE;
                oraParas[4].Value = objTable.ISINSERT;
                oraParas[5].Value = objTable.ISEDIT;
                try
                {
                    oraConn = conn.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_GROUP_MENU_ACCESS.GROUP_MENU_Insert", oraParas);

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
		
		public int UpdateGROUP_MENU(GROUP_MENUInfo objTable)
		{
			try
			{
                return 0;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

        public int DeleteGROUP_MENU(GROUP_MENUInfo objTable)
		{
			try
			{
                return 0;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
        public DataSet GetMenu_group(string pGwtype)
        {
            DataSet datDs = new DataSet();
            string strSql = "select * from Menu m,Group_Menu gm,groups g where g.groupid=gm.groupid and gm.menuid=m.menuid and g.gwtype='" + pGwtype + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetMenu(string PGwtype)
        {
            DataSet datDs = new DataSet();
            string strSql = "select m.caption from Menu m where m.gwtype='GWSYSTEM'  or m.gwtype='GWTYPE' or m.gwtype='" + PGwtype + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetMenu_treeview(string PGwtype,string Pparentid)//QuyND update 29/07/2008
        {
            DataSet datDs = new DataSet();
            string strSql = "select m.caption,m.menuid from Menu m where (m.gwtype='GWSYSTEM'  or m.gwtype='" + PGwtype + "') and m.parentid='" + Pparentid + "' and m.menuid  not in(select mn.menuid from menu mn where Trim(mn.caption)='System')  and m.menuid  not in(select mn.menuid from menu mn where Trim(mn.caption)='Help')";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataSet GetMenu_groupdd(string pMenuid,string pGroupname)
        {
            DataSet datDs = new DataSet();
            string strSql = "select gm.isinquiry,gm.isdelete,gm.isinsert,gm.isedit from groups g,group_menu gm where g.groupid=gm.groupid and gm.menuid='" + pMenuid + "' and g.groupname='"+pGroupname+"'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet CheckUserlogin(string pUserid,string pMenuid)
        {
            DataSet datDs = new DataSet();
            string strSql = "select GM.ISINQUIRY,GM.ISDELETE,GM.ISINSERT,GM.ISEDIT from Group_Menu GM where GM.Menuid='" + pMenuid + "' and  GM.GROUPID in(select Distinct(UG.GROUPID) from User_Groups UG where UG.USERID='" + pUserid + "')";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet CheckEnable_Menu(string pUserid, string pMenuid, string pGWTYPE)
        {
            DataSet datDs = new DataSet();
            string strSql = "select distinct(gm.groupid) from users u,groups g,User_Groups ug,menu m,group_menu gm where u.userid=ug.userid and ug.groupid=g.groupid and g.groupid=gm.groupid and m.menuid= gm.menuid and Trim(u.userid)='" + pUserid + "' and Trim(m.menuid)='" + pMenuid + "'and Trim(gm.isinquiry)='1' ";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable Select_Gwtype(string pUserid)
        {
            DataTable _dt = new DataTable();
            string strSql = "select GWTYPE from gwtype";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
                oraConn.Dispose();
                oraConn.Close();
                return _dt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       
	}
	
	
}
