using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using BR.BRBusinessObject;

namespace BR.BRInterBank.Reconcile
{
    public class clsVCB_MSG_REC
    {
        // Lay du lieu cua GW
        public static DataTable GetVCB_MSG_CONTENT(DateTime pDate)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),                                       
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Direction = ParameterDirection.Output;
            param[1].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_vcb.getvcb_msg_content", param);
            }
            catch //(Exception ex)
            {
                return null;
            }
            //return null;
        }

        // Get Result VCB_MSG_REC
        public static DataTable GetVCB_MSG_REC(DateTime pDate, string strType, string strDirection, string strDP,string strExp,string strMsgType)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),
                                       new OracleParameter("vRec_type",OracleType.VarChar,20),
                                       new OracleParameter("vDirection",OracleType.VarChar,20),
                                       new OracleParameter("vDepartment",OracleType.VarChar,20),
                                       new OracleParameter("vExp",OracleType.VarChar,20),
                                       new OracleParameter("vMsgtype",OracleType.VarChar,20),
                                       new OracleParameter("vReturn",OracleType.Cursor,20)};
            param[0].Value = pDate.Date;
            param[1].Value = strType;
            param[2].Value = strDirection;
            param[3].Value = strDP;
            param[4].Value = strExp;
            param[5].Value = strMsgType;
            param[6].Direction = ParameterDirection.Output;
            param[6].Value = null;
            try
            {
                return GetData.ExcuteSelect("gw_pk_rec_vcb.getvcb_msg_rec", param);
            }
            catch //(Exception ex)
            {
                return null;
            }
            //return null;
        }

        // Chi so Index cua VCB        
        public static string GetVCB_Index(DateTime pDate, string pREC_TYPE)
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
                int iResult = GetData.ExcuteStore("gw_pk_rec_vcb.rec_index", param);
                return param[2].Value.ToString();
            }
            catch
            {
                return "";
            }
            //return "";
        }

        // day va thay the du lieu vao bang content_temp(Manual)
        public static int InsertVCB_MSG_CONTENT_Temp(DateTime pDate)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) };
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_vcb.insertvcb_msg_content_temp", param);
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return -1;
        }

        // day va thay the du lieu vao bang rec_temp(Manual): TR
        public static int InsertVCB_MSG_REC_TEMP(DateTime pDate)
        {
            OracleParameter[] param = { new OracleParameter("pDate", OracleType.DateTime, 20) };
            param[0].Value = pDate.Date;
            try
            {
                return GetData.ExcuteStore("gw_pk_rec_vcb.insertvcb_msg_rec_temp", param);
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return -1;
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
                return GetData.ExcuteStore("gw_pk_rec_vcb.reconcile_sibs", param);
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
                return GetData.ExcuteStore("gw_pk_rec_vcb.reconcile_tr", param);
            }
            catch
            {
                return -1;
            }
        }

        // Ham doi chieu dien RECONCILE_SWIFT
        public static int RECONCILE_VCB(DateTime pDate, string vTellerID)
        {
            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20),                                       
                                       new OracleParameter("pTellerID",OracleType.VarChar,20)};
            param[0].Value = pDate.Date;
            param[1].Value = vTellerID;

            try
            {
                return GetData.ExcuteStore("gw_pk_rec_vcb.reconcile_vcb", param);
            }
            catch
            {
                return -1;
            }
        }
    }
}
