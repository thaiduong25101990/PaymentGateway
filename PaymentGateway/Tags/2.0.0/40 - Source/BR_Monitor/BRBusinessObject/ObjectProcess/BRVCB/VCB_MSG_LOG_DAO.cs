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
    public class VCB_MSG_LOGDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public VCB_MSG_LOGDP()
		{
		}
        public static VCB_MSG_LOGDP Instance()
		{
            return new VCB_MSG_LOGDP();
		}
        public DataSet GetVCB_MSG_LOG(string pQUERY_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,JOB_NAME,QUERY_ID,STATUS,DESCRIPTIONS from VCB_MSG_LOG  where QUERY_ID ='" + pQUERY_ID + "'";
            strSQL = strSQL + " Union  select LOG_DATE,SERVICE,QUERY_ID,STATUS,DESCRIPTIONS from IBPS_SVR_LOG  where QUERY_ID ='" + pQUERY_ID + "'";
            strSQL = strSQL + " Union  select LOG_DATE,SERVICE,QUERY_ID,STATUS,DESCRIPTIONS from RM_svr_log  where QUERY_ID ='" + pQUERY_ID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet SearchVCB_MSG_LOG(string pWHERE, DateTime pDate_from, DateTime pDate_to)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,JOB_NAME,QUERY_ID, (select a.content from Allcode a where Trim(a.cdname)='STATUS' and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(STATUS)) as STATUS,DESCRIPTIONS,CHK from VCB_MSG_LOG VML where To_char(Log_date, 'YYYYMMDD') >= To_Char(:pDate_from, 'YYYYMMDD') and To_char(Log_date, 'YYYYMMDD') <= To_Char(:pDate_to, 'YYYYMMDD') " + pWHERE + "";
            OracleParameter[] oraParas ={new OracleParameter("pDate_from",OracleType.DateTime,7),
                                            new OracleParameter("pDate_to",OracleType.DateTime,7)
                                                };
            oraParas[0].Value = pDate_from;
            oraParas[1].Value = pDate_to;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);

            }
            catch //(Exception ex)
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
            string strSQL = "select LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS,JOB_NAME from VCB_MSG_LOG IML where To_char(to_date(Log_date), 'YYYYMMDD') = To_Char(to_date(:pDatetimeNow), 'YYYYMMDD')";
            OracleParameter[] oraParas ={new OracleParameter("pDatetimeNow",OracleType.DateTime,7)                                            
                                                };
            oraParas[0].Value = pDatetimeNow;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public int ADD_VCB_MSG_LOG(VCB_MSG_LOGInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            int iResult = 0;
            string strSQL = "Insert into VCB_MSG_LOG(LOG_DATE,QUERY_ID,STATUS,DESCRIPTIONS,CHK,JOB_NAME,MSG_DIRECTION) values";
            strSQL = strSQL + "(:pLOG_DATE,:pQUERY_ID,:pSTATUS,:pDESCRIPTIONS,:pCHK,:pJOB_NAME,:pMSG_DIRECTION)";

            OracleParameter[] oraParas ={new OracleParameter("pLOG_DATE",OracleType.DateTime,8),
                                            new OracleParameter("pQUERY_ID",OracleType.Number,20),
                                            new OracleParameter("pSTATUS",OracleType.Number,1),
                                            new OracleParameter("pDESCRIPTIONS",OracleType.VarChar,1000),
                                            new OracleParameter("pCHK",OracleType.VarChar,1),
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
                oraConn.Dispose();
                oraConn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
