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
using BIDVWEB.Comm;
using BIDVWEB.Comm.DA;
using System.Data.OracleClient;

namespace BIDVWEB.Business
{
    public class USER_MSG_LOG_DAO
    {
        private System.Data.OracleClient.OracleConnection oraConn;
        private clsConnection objConn = new clsConnection();
        private UserEncrypt Encrypt = new UserEncrypt();
        public string strError = "";

        public USER_MSG_LOG_DAO()
		{
		}
        public static USER_MSG_LOG_DAO Instance()
		{
            return new USER_MSG_LOG_DAO();
		}
        public int AddUSER_MSG_LOG(USER_MSG_LOG_Info objTable)
        {           
            int iBool;

            try
            {
                string strSql = "Insert into User_msg_log(LOG_DATE,USERID,CONTENT,STATUS,WORKED," +
                    " TABLE_ACCESS,OLD_VALUE,NEW_VALUE) values (:pLOG_DATE,:pUSERID,:pCONTENT," +
                    " :pSTATUS,:pWORKED,:pTABLE_ACCESS,:pOLD_VALUE,:pNEW_VALUE)";
                OracleParameter[] oraParas = { new OracleParameter("pLOG_DATE", OracleType.DateTime, 7),
                                             new OracleParameter("pUSERID", OracleType.NVarChar, 10),
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
                
                oraConn = objConn.Connect();
                if (oraConn == null)                                     
                    return -1;
                iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);                
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
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


        //public int UPDATE_USER_MSG_LOG(string strUserID, DateTime LastDate)
        //{
        //    int iBool;

        //    try
        //    {
        //        string strSql = "update User_msg_log set (LOG_DATE,USERID,CONTENT,STATUS,WORKED," +
        //            " TABLE_ACCESS,OLD_VALUE,NEW_VALUE) values (:pLOG_DATE,:pUSERID,:pCONTENT," +
        //            " :pSTATUS,:pWORKED,:pTABLE_ACCESS,:pOLD_VALUE,:pNEW_VALUE)";
        //        OracleParameter[] oraParas = { new OracleParameter("pLOG_DATE", OracleType.DateTime, 7),
        //                                     new OracleParameter("pUSERID", OracleType.NVarChar, 10),
        //                                     new OracleParameter("pCONTENT", OracleType.NVarChar, 255),
        //                                     new OracleParameter("pSTATUS", OracleType.Number, 1),
        //                                     new OracleParameter("pWORKED", OracleType.NVarChar, 50),
        //                                     new OracleParameter("pTABLE_ACCESS", OracleType.NVarChar, 50),
        //                                     new OracleParameter("pOLD_VALUE", OracleType.NVarChar, 1000),
        //                                     new OracleParameter("pNEW_VALUE", OracleType.NVarChar, 1000)
        //                                     };

        //        oraParas[0].Value = objTable.LOG_DATE;
        //        oraParas[1].Value = objTable.USERID;
        //        oraParas[2].Value = objTable.CONTENT;
        //        oraParas[3].Value = objTable.STATUS;
        //        oraParas[4].Value = objTable.WORKED;
        //        oraParas[5].Value = objTable.TABLE_ACCESS;
        //        oraParas[6].Value = objTable.OLD_VALUE;
        //        oraParas[7].Value = objTable.NEW_VALUE;

        //        oraConn = objConn.Connect();
        //        if (oraConn == null)
        //            return -1;
        //        iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
        //        if (oraConn.State == ConnectionState.Open)
        //        {
        //            oraConn.Close();
        //            oraConn.Dispose();
        //        }
        //        return iBool;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        if (oraConn.State == ConnectionState.Open)
        //        {
        //            oraConn.Close();
        //            oraConn.Dispose();
        //        }
        //        return -1;
        //    }
        //}


        public DataSet GetUser_msg_log(string pUserid,DateTime pDateFrom,DateTime pDateTo)
        {
            DataSet ds =new DataSet();
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)                
                    return null;                                
                string strSQL = "   select UML.LOG_DATE,UML.USERID,UML.CONTENT,UML.STATUS," +
                " UML.WORKED,UML.TABLE_ACCESS,UML.OLD_VALUE,UML.NEW_VALUE " + 
                " from USER_MSG_LOG UML where UML.USERID= '" + pUserid + 
                "' and To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') >= To_Char(to_date(:pDateFrom,'DD/MM/YYYY'), 'YYYYMMDD') and To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') <= To_Char(to_date(:pDateTo,'DD/MM/YYYY'), 'YYYYMMDD') ";
                //and To_char(UML.LOG_DATE, 'YYYYMMDD') < To_Char(:pDateTo, 'YYYYMMDD')
                OracleParameter[] oraParas = { new OracleParameter("pDateFrom", OracleType.DateTime, 7),
                                           new OracleParameter("pDateTo", OracleType.DateTime, 7)
                                             };
                oraParas[0].Value = pDateFrom;
                oraParas[1].Value = pDateTo;
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return null;
            }
        }
        public DataSet GetUser_msg_log(DateTime pDateFrom, DateTime pDateTo)
        {
            DataSet ds = new DataSet();

            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)                
                    return null;                                
                string strSQL = "select UML.LOG_DATE,UML.USERID,UML.CONTENT,UML.STATUS,UML.WORKED,UML.TABLE_ACCESS,UML.OLD_VALUE,UML.NEW_VALUE from USER_MSG_LOG UML where To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') >= To_Char(to_date(:pDateFrom,'DD/MM/YYYY'), 'YYYYMMDD') and To_char(to_date(UML.LOG_DATE,'DD/MM/YYYY'), 'YYYYMMDD') <= To_Char(to_date(:pDateTo,'DD/MM/YYYY'), 'YYYYMMDD') ";
                //and To_char(UML.LOG_DATE, 'YYYYMMDD') < To_Char(:pDateTo, 'YYYYMMDD')
                OracleParameter[] oraParas = { new OracleParameter("pDateFrom", OracleType.DateTime, 7),
                                           new OracleParameter("pDateTo", OracleType.DateTime, 7)
                                             };

                oraParas[0].Value = pDateFrom;
                oraParas[1].Value = pDateTo;
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return null;
            }
        }
        public DataSet GetUser_log()
        {
            DataSet ds = new DataSet();
            
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                DataTable datTable = new DataTable();
                string strSQL = "Select UML.LOG_DATE,UML.USERID,UML.CONTENT,UML.STATUS," +
                    "UML.WORKED,UML.TABLE_ACCESS,UML.OLD_VALUE,UML.NEW_VALUE " + 
                    " from USER_MSG_LOG UML ";
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return null;
            }
        }
    }
}
