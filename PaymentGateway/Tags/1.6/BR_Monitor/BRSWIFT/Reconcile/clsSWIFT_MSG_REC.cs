using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using BR.BRBusinessObject;

namespace BR.BRSWIFT.Reconcile
{
    public class clsSWIFT_MSG_REC
    {
        // Lay du lieu cua GW
        public static DataTable GetSWIFT_MSG_CONTENT(DateTime pDate)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),                                       
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Direction = ParameterDirection.Output;
            param[1].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_swift.getswift_msg_content", param);
            }
            catch 
            {
                return null;
            }
           
        }
        
        // Get Result SWIFT_MSG_REC
        //public static DataTable GetSWIFT_MSG_REC(DateTime pDate, string strType, string strDirection, string strDP)
        //{
        //    OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
        //                               new OracleParameter("vRec_type",OracleType.VarChar,20),
        //                               new OracleParameter("vDirection",OracleType.VarChar,20),
        //                               new OracleParameter("vDepartment",OracleType.VarChar,20),                                       
        //                               new OracleParameter("vReturn",OracleType.Cursor,20)};
        //    param[0].Value = pDate.Date;
        //    param[1].Value = strType;
        //    param[2].Value = strDirection;
        //    param[3].Value = strDP;            
        //    param[4].Direction = ParameterDirection.Output;
        //    param[4].Value = null;
        //    try
        //    {
        //        return GetData.ExcuteSelect("gw_pk_rec_swift.getswift_msg_rec",param);                
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return null;
        //}

        public static DataTable GetSWIFT_MSG_REC(DateTime pDate, string strType, string strDirection, string strDP, string strExpre)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("vRec_type",OracleType.VarChar,20),
                                       new OracleParameter("vDirection",OracleType.VarChar,20),
                                       new OracleParameter("vDepartment",OracleType.VarChar,20),
                                       new OracleParameter("vExpre",OracleType.VarChar,5),
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Value = strType;
            param[2].Value = strDirection;
            param[3].Value = strDP;
            param[4].Value = strExpre;
            param[5].Direction = ParameterDirection.Output;
            param[5].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_swift.getswift_msg_rec", param);
            }
            catch 
            {
                return null;
            }
           
        }
        // Chi so Index cua SWIFT        
        public static string GetSWIFT_Index(DateTime pDate, string pREC_TYPE)
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
                int iResult = GetData.ExcuteStore("gw_pk_rec_swift.rec_index", param);
                return param[2].Value.ToString();
            }
            catch
            {
                return "";
            }            
        }

        // day va thay the du lieu vao bang content_temp(Manual)
        public static int InsertSWIFT_MSG_CONTENT_Temp(DateTime pDate)
        {
            OracleParameter[] param = { new OracleParameter("pDate",OracleType.DateTime,20)};
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_swift.insertswift_content_temp", param);
            }
            catch 
            {
                return -1;
            }
           
        }
                
        // day va thay the du lieu vao bang rec_temp(Manual): TR
        public static int InsertSWIFT_MSG_REC_TEMP(DateTime pDate)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20)};
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_swift.insertswift_msg_rec_temp",param);
            }
            catch 
            {
                return -1;
            }
            
        }
                
        // day va thay the du lieu vao bang rec_temp: RM,TF,SWIFT (Automatic)
        
        // Ham doi chieu dien RECONCILE_SIBS
        public static int RECONCILE_SIBS(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("pTellerID",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_swift.reconcile_sibs", param);
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
                return GetData.ExcuteStore("gw_pk_rec_swift.reconcile_tr", param);
            }
            catch
            {
                return -1;
            }
        }

        // Ham doi chieu dien RECONCILE_SWIFT
        public static int RECONCILE_SWIFT(DateTime pDate,string vTellerID)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),                                       
                                       new OracleParameter("pTellerID",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;            
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_swift.reconcile_swift", param);
            }
            catch
            {
                return -1;
            }
        }
    }
}
