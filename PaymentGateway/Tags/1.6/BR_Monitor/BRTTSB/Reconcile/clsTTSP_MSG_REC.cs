using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using BR.BRBusinessObject;

namespace BR.BRTTSB.Reconcile
{
    public class clsTTSP_MSG_REC
    {
        // Lay du lieu cua GW
        public static DataTable GetTTSP_MSG_CONTENT(DateTime pDate)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),                                       
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;            
            param[1].Direction = ParameterDirection.Output;
            param[1].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_ttsp.getttsp_msg_content", param);
            }
            catch 
            {
                return null;
            }           
        }

        // Lay du lieu bang TTSP_MSG_REC 
        public static DataTable GetTTSP_MSG_REC(DateTime pDate, string strType, string strDirection, string strExpre,string strMsgType)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("vRec_type",OracleType.VarChar,20),
                                       new OracleParameter("vDirection",OracleType.VarChar,20),
                                       new OracleParameter("vExp_type",OracleType.VarChar,20),
                                       new OracleParameter("vMsg_type",OracleType.VarChar,20),                                        
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Value = strType;
            param[2].Value = strDirection;
            param[3].Value = strExpre;
            param[4].Value = strMsgType;
            param[5].Direction = ParameterDirection.Output;
            param[5].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_ttsp.getttsp_msg_rec", param);
            }
            catch 
            {
                return null;
            }            
        } 

        // Lay thong tin du lieu
        public static string GetTTSP_Index(DateTime pDate,string pREC_TYPE)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20)
                                       ,new OracleParameter("rec_type", OracleType.VarChar, 20)
                                       ,new OracleParameter("vIndex", OracleType.VarChar, 100)};
            param[0].Value = pDate.Date;
            param[1].Value = pREC_TYPE;
            param[2].Direction = ParameterDirection.InputOutput;
            param[2].Value = "";

            try
            {
                int iResult = GetData.ExcuteStore("gw_pk_rec_ttsp.ttsp_index", param);
                return param[2].Value.ToString();
            }
            catch
            {
                return "";
            }
            //return "";
        }
        // Day du lieu vao bang TTSP_CONTENT_TEMP(Manual)
        public static int Insert_TTSP_CONTENT_TEMP(DateTime pDate)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) };
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ttsp.insert_ttsp_msg_content_temp", param);
            }
            catch
            {
                return -1;
            }
           // return -1;
        }
        // Day du lieu vao bang TTSP_MSG_REC_TEMP(Manual) TTSP-BR
        public static int Insert_TTSP_MSG_REC_TEMP(DateTime pDate)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) };
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ttsp.insert_ttsp_msg_rec_temp", param);
            }
            catch
            {
                return -1;
            }
           // return -1;
        }
        // Day du lieu vao bang TTSP_MSG_REC_TEMP(Automatic) SIBS-BR 

        // Doi chieu SIBS-BR
        public static int RECONCILE_SIBS(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) 
                                      , new OracleParameter("pTellerID", OracleType.VarChar, 20) };

            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ttsp.reconcile_sibs", param);
            }
            catch
            {
                return -1;
            }
           // return -1;
        }
        // Doi chieu TTSP-BR
        public static int RECONCILE_TTSP(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) 
                                      , new OracleParameter("pTellerID", OracleType.VarChar, 20) };
            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ttsp.reconcile_ttsp", param);
            }
            catch
            {
                return -1;
            }
            //return -1;
        }
        // Doi chieu IQS-BR
        public static int RECONCILE_IQS(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) 
                                      , new OracleParameter("pTellerID", OracleType.VarChar, 20) };

            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ttsp.reconcile_iqs", param);
            }
            catch
            {
                return -1;
            }
           // return -1;
        }
    }
}
