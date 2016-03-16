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

//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 11/06/2008
//' =============================================

namespace BR.BRBusinessObject
{
    public class IBPS_MSG_LOGDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public IBPS_MSG_LOGDP()
		{
		}
        public static IBPS_MSG_LOGDP Instance()
		{
            return new IBPS_MSG_LOGDP();
		}
        public DataSet GetIBPS_MSG_LOG(string pQUERY_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS,JOB_NAME,MSG_DIRECTION from IBPS_MSG_LOG  where QUERY_ID = '" + pQUERY_ID + "'";
            strSQL = strSQL + "   Union  select LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS,SERVICE,MSG_DIRECTION from IBPS_SVR_LOG  where QUERY_ID='" + pQUERY_ID + "'";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet SearchIBPS_MSG_LOG(string pWHERE,DateTime pDate_from,DateTime pDate_to)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,QUERY_ID,(select a.content from Allcode a where Trim(a.cdname)='STATUS' and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(STATUS)) as STATUS,DESCRIPTIONS,JOB_NAME from IBPS_MSG_LOG IML where To_char(Log_date,'YYYYMMDD') >= To_Char(:pDate_from,'YYYYMMDD') and To_char(Log_date,'YYYYMMDD') <= To_Char(:pDate_to,'YYYYMMDD') " + pWHERE + " ";
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
        public DataTable Get_data(DateTime pDatetimeNow)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,QUERY_ID,(select a.content from Allcode a where Trim(a.cdname)='STATUS' and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(STATUS)) as STATUS,DESCRIPTIONS,JOB_NAME from IBPS_MSG_LOG IML where To_char(to_date(Log_date), 'YYYYMMDD') = To_Char(to_date(:pDatetimeNow), 'YYYYMMDD')";
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

        public int ADD_MAG_LOG(IBPS_MSG_LOGInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "insert into IBPS_MSG_LOG( LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS,AREA,JOB_NAME,MSG_DIRECTION)";
            strSQL = strSQL + " values(:pLOG_DATE,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS,:pAREA,:pJOB_NAME,:pMSG_DIRECTION)";
            OracleParameter[] oraParas ={new OracleParameter("pLOG_DATE",OracleType.DateTime,8),
                                            new OracleParameter("pQUERY_ID",OracleType.Number,20),
                                            new OracleParameter("pSTATUS",OracleType.Number,1),
                                            new OracleParameter("pDESCRIPTIONS",OracleType.VarChar,1000),
                                            new OracleParameter("pAREA",OracleType.VarChar,50),
                                            new OracleParameter("pJOB_NAME",OracleType.VarChar,100),
                                            new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,10)
                                                };
            oraParas[0].Value = objTable.LOG_DATE;
            oraParas[1].Value = objTable.QUERY_ID;
            oraParas[2].Value = objTable.STATUS;
            oraParas[3].Value = objTable.DESCRIPTIONS;
            oraParas[4].Value = "";
            oraParas[5].Value = "";
            oraParas[6].Value = "";

            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
                oraConn.Close();
                oraConn.Dispose();
                return 1;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
    }
}
