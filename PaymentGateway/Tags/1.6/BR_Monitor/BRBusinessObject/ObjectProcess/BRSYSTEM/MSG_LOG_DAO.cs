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



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class MSG_LOGDP
	{

        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
		public MSG_LOGDP()
		{
		}
		public static MSG_LOGDP Instance()
		{
			return new MSG_LOGDP();
		}
		
		public int AddMSG_LOG(MSG_LOGInfo objTable)
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
		
		public int UpdateMSG_LOG(MSG_LOGInfo objTable)
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

        public int DeleteMSG_LOG(MSG_LOGInfo objTable)
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
        public DataTable GetMSG_LOG()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG where To_char(LOG_DATE,'YYYYMMDD') = To_char(sysdate,'YYYYMMDD') ";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= TF_SVR_LOG.STATUS) as STATUS from TF_SVR_LOG  where To_char(LOG_DATE,'YYYYMMDD') = To_char(sysdate,'YYYYMMDD')";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];               
            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetMSG_LOG_S(DateTime pDateFrom, DateTime pDateTo)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;//TF_SVR_LOG
            }
            string strSQL = "select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= IBPS_SVR_LOG.STATUS) as STATUS from IBPS_SVR_LOG  where to_date(LOG_DATE)>=to_date(:pDateFrom) and to_date(LOG_DATE)<=to_date(:pDateTo)";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG  where to_date(LOG_DATE)>=to_date(:pDateFrom) and to_date(LOG_DATE)<=to_date(:pDateTo)";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG  where to_date(LOG_DATE)>=to_date(:pDateFrom) and to_date(LOG_DATE)<=to_date(:pDateTo)";
            OracleParameter[] oraParas ={new OracleParameter("pDateFrom",OracleType.DateTime,7),
                                            new OracleParameter("pDateTo",OracleType.DateTime,7)
                                                };
            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetMSG_LOG_S1(string strServicename)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= IBPS_SVR_LOG.STATUS) as STATUS from IBPS_SVR_LOG  where Trim(SERVICE)=  '" + strServicename.ToUpper() + "'";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG  where Trim(SERVICE)='" + strServicename.ToUpper() + "'";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG  where Trim(SERVICE)='" + strServicename.ToUpper() + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetMSG_LOG_S2(string strServiceName, DateTime pDateFrom, DateTime pDateTo)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= IBPS_SVR_LOG.STATUS) as STATUS from IBPS_SVR_LOG ML where to_char(LOG_DATE,'YYYYMMDD')>=to_char(:pDateFrom,'YYYYMMDD') and to_char(LOG_DATE,'YYYYMMDD')<=to_char(:pDateTo,'YYYYMMDD') and Trim(SERVICE)='" + strServiceName.ToUpper() + "'";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG ML where to_char(LOG_DATE,'YYYYMMDD')>=to_char(:pDateFrom,'YYYYMMDD') and to_char(LOG_DATE,'YYYYMMDD')<=to_char(:pDateTo,'YYYYMMDD') and Trim(SERVICE)='" + strServiceName.ToUpper() + "'";
            strSQL = strSQL + "  union  select LOG_ID,LOG_DATE,SERVICE,DESCRIPTIONS,(select NAME from STATUS S where S.STATUS= RM_SVR_LOG.STATUS) as STATUS from RM_SVR_LOG ML where to_char(LOG_DATE,'YYYYMMDD')>=to_char(:pDateFrom,'YYYYMMDD') and to_char(LOG_DATE,'YYYYMMDD')<=to_char(:pDateTo,'YYYYMMDD') and Trim(SERVICE)='" + strServiceName.ToUpper() + "'";

            OracleParameter[] oraParas ={new OracleParameter("pDateFrom",OracleType.DateTime,7),
                                            new OracleParameter("pDateTo",OracleType.DateTime,7)                                            
                                                };
            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
		 
	}

	
	
}
