using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.BRBusinessObject;
using System.Data.OracleClient;
using System.Data;

namespace BR.BRIBPS.Reconcile
{
    public class clsIBPS_MSG_REC
    {
        
        
        // Lay du lieu dien cua GW        
        public static DataTable GetIBPS_MSG_CONTENT(DateTime pDate)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),                                       
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Direction = ParameterDirection.Output;
            param[1].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_ibps.getibps_msg_content", param);
            }
            catch //(Exception ex)
            {
                return null;
            }
            //return null;
        }
        
        // Get Result SWIFT_MSG_REC
        public static DataTable GetIBPS_MSG_REC(DateTime pDate, string strType, string strDirection, string strExpre,string strTAD,string strDepartment)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("vRec_type",OracleType.VarChar,20),
                                       new OracleParameter("vDirection",OracleType.VarChar,20),
                                       new OracleParameter("vExp_type",OracleType.VarChar,20),
                                       new OracleParameter("vTAD",OracleType.VarChar,20),
                                       new OracleParameter("vDepartment",OracleType.VarChar,20),
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Value = strType;
            param[2].Value = strDirection;
            param[3].Value = strExpre;
            param[4].Value = strTAD;
            param[5].Value = strDepartment;
            param[6].Direction = ParameterDirection.Output;
            param[6].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_ibps.getibps_msg_rec", param);
            }
            catch //(Exception ex)
            {
                return null;
            }
            //return null;
        }

        public static string GetIBPS_Index(DateTime pDate, string vRec_type, string sTAD)
        {
            try
            {
                OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20)
                                          ,new OracleParameter("rec_type",OracleType.VarChar,50)
                                          ,new OracleParameter("pTAD",OracleType.VarChar,50)
                                          ,new OracleParameter("vIndex",OracleType.VarChar,50)};
                param[0].Value = pDate.Date;
                param[1].Value = vRec_type;
                param[2].Value = sTAD;
                param[3].Direction = ParameterDirection.InputOutput;
                param[3].Value = "";
                GetData.ExcuteStore("gw_pk_rec_ibps.rec_index", param);
                return param[3].Value.ToString();
            }
            catch
            {
                return "";
            }
            //return "";
        }
        
        // day va thay the du lieu vao bang content_temp(Manual)
        public static int InsertIBPS_MSG_CONTENT_Temp(DateTime pDate)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20)};
            param[0].Value = pDate.Date;            
            
            
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.insertibps_msg_content_temp", param);
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return -1;
        }
        // day va thay the du lieu vao bang content_tad(Manual)
        public static int InsertIBPS_MSG_CONTENT_TAD(DateTime dtDate,string strTAD)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) 
                                      , new OracleParameter("pTad", OracleType.VarChar, 20) 
                                      };
            param[0].Value = dtDate.Date;
            param[1].Value = strTAD;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.insertibps_msg_content_tad", param);
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return -1;            
        }
        // day va thay the du lieu vao bang rec_temp(Automatic)

        // day va thay the du lieu vao bang rec_tad(Manual)
        public static int InsertIBPS_MSG_REC_TAD(DateTime pDate, string vTAD, string vDBLinkName)
        {
           
            OracleParameter[] param = {new OracleParameter("pdate",OracleType.DateTime,20) 
                                      ,new OracleParameter("vtad",OracleType.VarChar,20)
                                      ,new OracleParameter("vdblink",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;
            param[1].Value = vTAD;
            param[2].Value = vDBLinkName;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.insertibps_msg_rec_tad", param);
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return -1;
        }

        // day va thay the du lieu vao bang rec_temp(Manual)
        public static int InsertIBPS_MSG_REC_Temp(DateTime pDate)
        {
            OracleParameter[] param = { new OracleParameter("pdate", OracleType.DateTime, 20) };
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.insertibps_msg_rec_temp", param);
            }
            catch             
            {
                return -1;
            }
        }

        // Ham doi chieu dien RECONCILE_SIBS
        public static int RECONCILE_SIBS(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("pTellerID",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.reconcile_sibs", param);
            }
            catch 
            {
                return -1;
            }
        }

        // Ham doi chieu dien RECONCILE_TR
        public static int RECONCILE_TR(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("pTellerID",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.reconcile_tr", param);
            }
            catch
            {
                return -1;
            }
        }

        // Ham doi chieu dien RECONCILE_IBPS
        public static int RECONCILE_IBPS(DateTime pDate, string vTAD,string vTellerID)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("pTAD",OracleType.VarChar,20),
                                       new OracleParameter("pTellerID",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;
            param[1].Value = vTAD;
            param[2].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_ibps.reconcile_ibps", param);
            }
            catch
            {
                return -1;
            }
        }
    }
}
