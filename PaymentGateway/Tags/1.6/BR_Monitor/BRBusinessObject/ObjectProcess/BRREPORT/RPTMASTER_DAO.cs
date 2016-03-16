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
//DatHM
//Add : 04/07/2008
namespace BR.BRBusinessObject
{
    public class RPTMASTERDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public RPTMASTERDP()
		{
		}
        public static RPTMASTERDP Instance()
		{
            return new RPTMASTERDP();
		}
        public int AddRPTMASTER(RPTMASTERInfo objTable)
        {
            string strSql = "Insert into RPTMASTER (RPTNAME,GWTYPE,DESCRIPTION) values (:pRPTNAME,:pGWTYPE,:pDESCRIPTION)";
            OracleParameter[] oraParam = {new OracleParameter("pRPTNAME", OracleType.NVarChar, 20),                          
                                         new OracleParameter("pGWTYPE", OracleType.NVarChar, 10),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar , 100)};

            oraParam[0].Value = objTable.RPTNAME;
            oraParam[1].Value = objTable.GWTYPE;
            oraParam[2].Value = objTable.DESCRIPTION;    

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1; ;
            }
        }
        public int UpdateRPTMASTER(string strID,RPTMASTERInfo objTable)
        {
            //string strSql = "Update RPTMASTER  set GWTYPE=:pGWTYPE,";
            //strSql = strSql + " DESCRIPTION=:pDESCRIPTION,";
            //strSql = strSql + " RPTNAME=:pRPTNAME";
            //strSql = strSql + " where RPTNAME='"+ strID +"'" ;
            string strSql = "Begin ";
            strSql = strSql +"Update RPTMASTER  set GWTYPE=:pGWTYPE,";
            strSql = strSql + " DESCRIPTION=:pDESCRIPTION,";
            strSql = strSql + " RPTNAME=:pRPTNAME";
            strSql = strSql + " where RPTNAME='" + strID + "';";
            strSql = strSql + " Update rptparameter set RPTNAME=:pRPTNAME";
            strSql = strSql + " where RPTNAME='" + strID + "';";
            strSql = strSql + " Update group_report set RPTNAME=:pRPTNAME";
            strSql = strSql + " where RPTNAME='" + strID + "';";
            strSql = strSql + " End;";
            OracleParameter[] oraParam = {new OracleParameter("pRPTNAME", OracleType.NVarChar, 20), 
                                          new OracleParameter("pGWTYPE", OracleType.NVarChar, 10),                              
                                                               
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar , 100)};
            oraParam[0].Value = objTable.RPTNAME;
            oraParam[1].Value = objTable.GWTYPE;
            oraParam[2].Value = objTable.DESCRIPTION;      
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
        public int DeleteRPTMASTER(string strID)
        {
            string strSql = "Begin";
            strSql = strSql + " Delete from RPTMASTER where RPTNAME=:pRPTNAME;";
            strSql = strSql + " Delete from RPTPARAMETER where RPTNAME=:pRPTNAME;";
            strSql = strSql + " Delete from group_report where RPTNAME=:pRPTNAME;";
            strSql = strSql + " end;";
            //string strSql = "Delete from RPTMASTER where RPTNAME=:pRPTNAME ";
            OracleParameter[] oraParam = { new OracleParameter("pRPTNAME", OracleType.NVarChar, 12) };
            oraParam[0].Value = strID;
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
        public DataSet GetRPTMASTER()
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataSet datTable = new DataSet();
            string strSQL = "select RPTNAME,GWTYPE,DESCRIPTION from RPTMASTER order by RPTNAME"; 

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        //public DataSet GetReportType(string strRptType)
        public DataSet GetReportType(string strRptType,string userID)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataSet datTable = new DataSet();
            //string strSQL = "select RPTNAME,DESCRIPTION from RPTMASTER where GWTYPE='"+ strRptType +"'" ;
            string strSQL = "select distinct(b.rptname),c.description from user_groups a,group_report b,rptmaster c where a.groupid =b.groupid and trim(c.rptname)=trim(b.rptname)and c.gwtype='" + strRptType + "'and a.userid='" + userID + "'and b.isview=1 order by b.rptname";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet GetParam(string RptName)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataSet datTable = new DataSet();
            string strSQL = "select * from RPTPARAMETER where RPTNAME='" + RptName + "' order by LSTORD asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetDataCombo(string SQL)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = SQL;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        //public DataSet GetGroup(string strGwtype)
        public DataSet GetGroup()
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataSet datTable = new DataSet();
            //string strSQL = "select distinct(GROUPNAME),GROUPID from groups g where g.gwtype ='" + strGwtype + "'";
            string strSQL = "select distinct(GROUPNAME),GROUPID from groups ";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
    }
}
