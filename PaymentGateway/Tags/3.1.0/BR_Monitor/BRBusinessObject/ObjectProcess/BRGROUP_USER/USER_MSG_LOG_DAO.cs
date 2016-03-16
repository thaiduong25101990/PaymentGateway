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
//using BR.BRLib;

//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	27/05/2008
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class USER_MSG_LOGDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        

        public USER_MSG_LOGDP()
		{
		}
        public static USER_MSG_LOGDP Instance()
		{
            return new USER_MSG_LOGDP();
		}
        public int AddUSER_MSG_LOG(USER_MSG_LOGInfo objTable)
        {
           
            try
            {
                string strSql = "Insert into User_msg_log(LOG_DATE,USERID,CONTENT,STATUS,WORKED,TABLE_ACCESS,OLD_VALUE,NEW_VALUE) values (:pLOG_DATE,:pUSERID,:pCONTENT,:pSTATUS,:pWORKED,:pTABLE_ACCESS,:pOLD_VALUE,:pNEW_VALUE)";
                OracleParameter[] oraParas = { new OracleParameter("pLOG_DATE", OracleType.DateTime, 8),
                                             new OracleParameter("pUSERID", OracleType.NVarChar, 20),
                                             new OracleParameter("pCONTENT", OracleType.NVarChar, 255),
                                             new OracleParameter("pSTATUS", OracleType.Number, 1),
                                             new OracleParameter("pWORKED", OracleType.NVarChar, 50),
                                             new OracleParameter("pTABLE_ACCESS", OracleType.NVarChar, 50),
                                             new OracleParameter("pOLD_VALUE", OracleType.NVarChar, 1000),
                                             new OracleParameter("pNEW_VALUE", OracleType.NVarChar, 1000)
                                             };

                oraParas[0].Value = objTable.LOG_DATE;
                oraParas[1].Value = objTable.USERID;
                oraParas[2].Value = objTable.CONTENT;
                oraParas[3].Value = objTable.STATUS;
                oraParas[4].Value = objTable.WORKED;
                oraParas[5].Value = objTable.TABLE_ACCESS;
                oraParas[6].Value = objTable.OLD_VALUE;
                oraParas[7].Value = objTable.NEW_VALUE;
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        
                        return -1;
                    }
                    else
                    {
                        return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
                    }
                }
                catch 
                {
                    
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
               
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public int AddUSER_MSG_LOG1(USER_MSG_LOGInfo objTable)
        {

            try
            {
                string strSql = "Insert into User_msg_log(LOG_DATE,USERID,CONTENT,STATUS,WORKED,TABLE_ACCESS,OLD_VALUE,NEW_VALUE) values (Sysdate,:pUSERID,:pCONTENT,:pSTATUS,:pWORKED,:pTABLE_ACCESS,:pOLD_VALUE,:pNEW_VALUE)";
                OracleParameter[] oraParas = {new OracleParameter("pUSERID", OracleType.NVarChar, 20),
                                             new OracleParameter("pCONTENT", OracleType.NVarChar, 255),
                                             new OracleParameter("pSTATUS", OracleType.Number, 1),
                                             new OracleParameter("pWORKED", OracleType.NVarChar, 50),
                                             new OracleParameter("pTABLE_ACCESS", OracleType.NVarChar, 50),
                                             new OracleParameter("pOLD_VALUE", OracleType.NVarChar, 1000),
                                             new OracleParameter("pNEW_VALUE", OracleType.NVarChar, 1000)
                                             };

                oraParas[0].Value = objTable.USERID;
                oraParas[1].Value = objTable.CONTENT + "/" + "DIACHI IP:" + BR.BRLib.Common.IpLocal;
                oraParas[2].Value = objTable.STATUS;
                oraParas[3].Value = objTable.WORKED;
                oraParas[4].Value = objTable.TABLE_ACCESS;
                oraParas[5].Value = objTable.OLD_VALUE;
                oraParas[6].Value = objTable.NEW_VALUE;
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {

                        return -1;
                    }
                    else
                    {
                        return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
                    }
                }
                catch 
                {

                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
               
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public DataSet GetUser_msg_log(string pUserid,DateTime pDateFrom,DateTime pDateTo, string pContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "   select UML.LOG_DATE,UML.USERID,UML.CONTENT,(select a.content from Allcode a where Trim(a.cdname)='STATUS'";
            strSQL = strSQL + "   and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(UML.STATUS)) as STATUS,UML.WORKED,UML.TABLE_ACCESS,UML.OLD_VALUE,UML.NEW_VALUE from USER_MSG_LOG UML where UML.USERID= '" + pUserid + "' and To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') >= To_Char(to_date(:pDateFrom,'DD/MM/YYYY'), 'YYYYMMDD') and To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') <= To_Char(to_date(:pDateTo,'DD/MM/YYYY'), 'YYYYMMDD') and upper(UML.CONTENT) like '%" + pContent.ToUpper() + "%'";
            //and To_char(UML.LOG_DATE, 'YYYYMMDD') < To_Char(:pDateTo, 'YYYYMMDD')
            OracleParameter[] oraParas = { new OracleParameter("pDateFrom", OracleType.DateTime, 8),
                                           new OracleParameter("pDateTo", OracleType.DateTime, 8)
                                             };

            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;
            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetUser_msg_log(DateTime pDateFrom, DateTime pDateTo,string pContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select UML.LOG_DATE,UML.USERID,UML.CONTENT,(select a.content from Allcode a where Trim(a.cdname)='STATUS' and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(UML.STATUS)) as STATUS,UML.WORKED,UML.TABLE_ACCESS,UML.OLD_VALUE,UML.NEW_VALUE from USER_MSG_LOG UML where To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') >= To_Char(to_date(:pDateFrom,'DD/MM/YYYY'), 'YYYYMMDD') and To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') <= To_Char(to_date(:pDateTo,'DD/MM/YYYY'), 'YYYYMMDD') and upper(UML.CONTENT) like '%" + pContent.ToUpper() + "%'";
            //and To_char(UML.LOG_DATE, 'YYYYMMDD') < To_Char(:pDateTo, 'YYYYMMDD')
            OracleParameter[] oraParas = { new OracleParameter("pDateFrom", OracleType.DateTime, 8),
                                           new OracleParameter("pDateTo", OracleType.DateTime, 8)
                                             };

            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);

            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetUser_log()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select UML.LOG_DATE,UML.USERID,UML.CONTENT,(select a.content from Allcode a where Trim(a.cdname)='STATUS' and Trim(a.gwtype)='SYSTEM' and Trim(a.cdval)=Trim(UML.STATUS)) as";
            strSQL = strSQL + "   STATUS,UML.WORKED,UML.TABLE_ACCESS,UML.OLD_VALUE,UML.NEW_VALUE from USER_MSG_LOG UML  where To_char(UML.LOG_DATE,'YYYYMMDD')= To_char(Sysdate,'YYYYMMDD')";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetViewLogInfo(string pContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,USERID,WORKED,OLD_VALUE,NEW_VALUE from USER_MSG_LOG  where CONTENT like '" + pContent.Trim() + "%'";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataTable History_User_Log(string pCONTENT)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select LOG_DATE,USERID,WORKED,OLD_VALUE,NEW_VALUE from USER_MSG_LOG  where CONTENT  like '%" + pCONTENT.Trim() + "%' order by LOG_DATE desc";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
    }
}
