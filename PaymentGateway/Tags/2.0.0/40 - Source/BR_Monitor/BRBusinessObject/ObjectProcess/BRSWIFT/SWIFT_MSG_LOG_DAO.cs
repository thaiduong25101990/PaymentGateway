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
using BR.BRLib;
using System.Data.OracleClient;

//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 11/06/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_MSG_LOGDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        UserEncrypt Encrypt = new UserEncrypt();
        public SWIFT_MSG_LOGDP()
		{
		}
        public static SWIFT_MSG_LOGDP Instance()
		{
            return new SWIFT_MSG_LOGDP();
		}
        public DataSet GetSWIFT_MSG_LOG(string pQUERY_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select LOG_ID,LOG_DATE,QUERY_ID,JOB_NAME,STATUS,DESCRIPTIONS from SWIFT_MSG_LOG  where QUERY_ID='" + pQUERY_ID + "'";
            strSQL = strSQL + "  Union  select LOG_ID,LOG_DATE,QUERY_ID,SERVICE,STATUS,DESCRIPTIONS from RM_SVR_LOG  where QUERY_ID='" + pQUERY_ID + "'";
            strSQL = strSQL + "  Union  select LOG_ID,LOG_DATE,QUERY_ID,SERVICE,STATUS,DESCRIPTIONS from TF_SVR_LOG  where QUERY_ID='" + pQUERY_ID + "'  order by LOG_DATE ASC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public int ADD_SWIFT_MSG_LOG(SWIFT_MSG_LOGInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            DataTable datTable = new DataTable();
            string strSQL = "insert into SWIFT_MSG_LOG (LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS) values (Sysdate,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS)";
            OracleParameter[] oraParas ={new OracleParameter("pQUERY_ID",OracleType.Number,20),
                                            new OracleParameter("pSTATUS",OracleType.Number,1),
                                            new OracleParameter("pDESCRIPTIONS",OracleType.VarChar,1000)
                                                };
           
            oraParas[0].Value = objTable.QUERY_ID;
            oraParas[1].Value = objTable.STATUS;
            oraParas[2].Value = objTable.DESCRIPTIONS;
            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
            }
            catch 
            {
                return -1;
            }
        }


        public int ADD_SWIFT_MSG_LOG_DATE(SWIFT_MSG_LOGInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            DataTable datTable = new DataTable();
            string strSQL = "insert into SWIFT_MSG_LOG (LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS) values (:pLOG_DATE,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS)";
            OracleParameter[] oraParas ={new OracleParameter("pLOG_DATE",OracleType.DateTime,7),
                                            new OracleParameter("pQUERY_ID",OracleType.Number,20),
                                            new OracleParameter("pSTATUS",OracleType.Number,1),
                                            new OracleParameter("pDESCRIPTIONS",OracleType.VarChar,1000)
                                                };
            oraParas[0].Value = objTable.LOG_DATE;
            oraParas[1].Value = objTable.QUERY_ID;
            oraParas[2].Value = objTable.STATUS;
            oraParas[3].Value = objTable.DESCRIPTIONS;
            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
            }
            catch 
            {
                return -1;
            }
        }

        public DataTable SELECT_SWIFT_MSG_LOG(SWIFT_MSG_LOGInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select  QUERY_ID,STATUS,DESCRIPTIONS from  SWIFT_MSG_LOG where  trim(QUERY_ID)=:pQUERY_ID and  trim(STATUS)=:pSTATUS  and  trim(DESCRIPTIONS)=:pDESCRIPTIONS";
            OracleParameter[] oraParas ={new OracleParameter("pQUERY_ID",OracleType.Number,20),
                                            new OracleParameter("pSTATUS",OracleType.Number,1),
                                            new OracleParameter("pDESCRIPTIONS",OracleType.VarChar,1000)
                                                };

            oraParas[0].Value = objTable.QUERY_ID;
            oraParas[1].Value = objTable.STATUS;
            oraParas[2].Value = objTable.DESCRIPTIONS;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas).Tables[0];
            }
            catch 
            {
                return null;
            }
        }



        public DataSet SearchSWIFT_MSG_LOG(string pWHERE,DateTime pDate_from,DateTime pDate_to)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select LOG_DATE,QUERY_ID,(select a.content from Allcode a where Trim(a.cdname)='STATUS' and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(STATUS)) as STATUS,DESCRIPTIONS,JOB_NAME from SWIFT_MSG_LOG SML where To_char(Log_date, 'YYYYMMDD') >= To_Char(:pDate_from, 'YYYYMMDD') and To_char(Log_date, 'YYYYMMDD') <= To_Char(:pDate_to, 'YYYYMMDD') " + pWHERE + " ";
            OracleParameter[] oraParas ={new OracleParameter("pDate_from",OracleType.DateTime,7),
                                            new OracleParameter("pDate_to",OracleType.DateTime,7)
                                                };
            oraParas[0].Value = pDate_from;
            oraParas[1].Value = pDate_to;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
            }
            catch 
            {
                return null;
            }
        }
        //tao ham log cho form frmSwiftMsgManualInfo
        public DataSet GetSWIFT_MSG_LOG_ManualInfo(int pQUERY_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_ID,LOG_DATE,QUERY_ID,JOB_NAME,STATUS,DESCRIPTIONS from SWIFT_MSG_LOG  where QUERY_ID='" + pQUERY_ID + "'";
            strSQL = strSQL + "  Union  select LOG_ID,LOG_DATE,QUERY_ID,SERVICE,STATUS,DESCRIPTIONS from RM_SVR_LOG  where QUERY_ID='" + pQUERY_ID + "'";
            strSQL = strSQL + "  Union  select LOG_ID,LOG_DATE,QUERY_ID,SERVICE,STATUS,DESCRIPTIONS from TF_SVR_LOG  where QUERY_ID='" + pQUERY_ID + "'  order by LOG_DATE ASC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataTable Get_data(DateTime pDatetimeNow)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS,JOB_NAME from SWIFT_MSG_LOG IML where To_char(to_date(Log_date), 'YYYYMMDD') = To_Char(to_date(:pDatetimeNow), 'YYYYMMDD')";
            OracleParameter[] oraParas ={new OracleParameter("pDatetimeNow",OracleType.DateTime,7)                                            
                                                };
            oraParas[0].Value = pDatetimeNow;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
  //      LOG_ID       NUMBER(10) not null,
  //LOG_DATE     DATE not null,
  //QUERY_ID     NUMBER(10) not null,
  //STATUS       NUMBER(1) not null,
  //DESCRIPTIONS VARCHAR2(1000)
    }
}
