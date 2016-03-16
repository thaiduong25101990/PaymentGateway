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
using System.Data.OleDb;
//DatHM
//Add : 09/07/2008

namespace BR.BRBusinessObject
{
    public class RPTRULEDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();       
       
        public RPTRULEDP()
		{
		}
        public static RPTRULEDP Instance()
		{
            return new RPTRULEDP();
		}
        public int AddRPTRULE(RPTRULEInfo objTable)
        {
            string strSql = "Insert into group_report (GROUPID,RPTNAME,ISVIEW,ISREFRESH,ISPRINT,ISEXPORT) values (:pGROUPID,:pRPTNAME,:pISVIEW,:pISREFRESH,:pISPRINT,:pISEXPORT)";
            OracleParameter[] oraParam = {new OracleParameter("pGROUPID", OracleType.Number , 10),                          
                                         new OracleParameter("pRPTNAME", OracleType.NVarChar, 12),
                                         new OracleParameter("pISVIEW", OracleType.Number , 1),
                                         new OracleParameter("pISREFRESH", OracleType.Number , 1),
                                         new OracleParameter("pISPRINT", OracleType.Number , 1),
                                         new OracleParameter("pISEXPORT", OracleType.Number , 1)
                                         };

            oraParam[0].Value = objTable.GROUPID ;
            oraParam[1].Value = objTable.RPTNAME ;
            oraParam[2].Value = objTable.ISVIEW ;
            oraParam[3].Value = objTable.ISREFRESH;
            oraParam[4].Value = objTable.ISPRINT;
            oraParam[5].Value = objTable.ISEXPORT;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1; 
            }
        }
        public int DeleteRPTRULE(int pgroup,string RptName)
        {

            string strSql = "Delete from group_report where groupid='" + pgroup + "' and rptname='" + RptName+"'";
            //OracleParameter[] oraParam = { new OracleParameter("pGROUPID", OracleType.Number , 10),
            //                               new OracleParameter("pRPTNAME", OracleType.NVarChar, 12)                                          
            //                             };
            //oraParam[0].Value = pgroup;
            //oraParam[1].Value = RptName;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetRule(int pgroupid, string pRptName)
        {
            DataSet datDs = new DataSet();
            string strSql = "select * from group_report g where g.groupid='" + pgroupid + "' and g.rptname='" + pRptName + "'";
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
        public DataSet GetRuleReport(string userid, string pRptName)
        {
            DataSet datDs = new DataSet();
            string strSql = "select a.* from group_report a,user_groups b where trim(a.groupid) =trim(b.groupid) and trim(b.userid)='" + userid + "' and trim(a.rptname)='" + pRptName + "'";
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
        public DataSet GetRuleMenuRpt(string userid, string gwType)
        {
            string strSql = "";
            DataSet datDs = new DataSet();
            if (gwType == "")
            {
                 strSql = "select b.* from menu a,group_menu b,user_groups c where a.menuid =b.menuid and c.groupid =b.groupid and trim(a.caption)='Reports' and  c.userid ='" + userid + "'";
            }
            else
            {
                 strSql = "select b.* from menu a,group_menu b,user_groups c where a.menuid =b.menuid and c.groupid =b.groupid and trim(a.caption)='Reports'and trim(a.gwtype)='" + gwType + "' and c.userid ='" + userid + "'";
            }
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
              

        

    }

}
