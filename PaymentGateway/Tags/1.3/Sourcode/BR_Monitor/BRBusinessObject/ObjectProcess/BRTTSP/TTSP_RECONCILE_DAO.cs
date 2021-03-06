﻿using System.Diagnostics;
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

namespace BR.BRBusinessObject
{
    public class TTSP_RECONCILEDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public TTSP_RECONCILEDP()
        {
        }
        public static TTSP_RECONCILEDP Instance()
        {
            return new TTSP_RECONCILEDP();
        }

        public int AddTTSP_RECONCILE(TTSP_RECONCILEInfo objTable)
        {
            string strSql = "INSERT INTO TTSP_MSG_REC (GW_TYPE, ACC_NUM,";
            strSql = strSql + "ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSql = strSql + "RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE, EXCEPTION_TYPE)";
            strSql = strSql + " VALUES (:pGW_TYPE,:pACC_NUM,:pACC_TYPE, :pRUNNING_SEG_NO, :pREF_NO, :pMSG_TYPE, :pSEG_NO, :pMSG_NO, :pCCY, :pAMOUNT, :pAPP_CODE, :pSENDER, :pORG_BANK,)";
            strSql = strSql + ":pRECEIVING_BRANCH, :pRECEIVER, :pVALUE_DATE, :pJOURNAL_SEG_NO, :pMSG_DIRECTION, :pFROM_SYSTEM, :pTO_SYSTEM, :pTRANS_DATE, :pEXCEPTION_TYPE)";

            OracleParameter[] oraParam = {new OracleParameter("pGW_TYPE", OracleType.NVarChar, 15),
                                         new OracleParameter("pACC_NUM",OracleType.NVarChar,15),
                                         new OracleParameter("pACC_TYPE",OracleType.NVarChar,70),
                                         new OracleParameter("pRUNNING_SEG_NO",OracleType.NVarChar,6),
                                         new OracleParameter("pREF_NO",OracleType.Number,10),
                                         new OracleParameter("pMSG_TYPE",OracleType.Number,10),
                                         new OracleParameter("pSEG_NO",OracleType.Number,10),
                                         new OracleParameter("pMSG_NO", OracleType.NVarChar, 100),
                                         new OracleParameter("pCCY", OracleType.NVarChar, 15),
                                         new OracleParameter("pAMOUNT", OracleType.NVarChar, 15),
                                         new OracleParameter("pAPP_CODE", OracleType.NVarChar, 15),
                                         new OracleParameter("pSENDER", OracleType.NVarChar, 15),
                                         new OracleParameter("pORG_BANK", OracleType.NVarChar, 15),
                                         new OracleParameter("pRECEIVING_BRANCH", OracleType.NVarChar, 15),
                                         new OracleParameter("pRECEIVER", OracleType.NVarChar, 15),
                                         new OracleParameter("pVALUE_DATE", OracleType.NVarChar, 15),
                                         new OracleParameter("pJOURNAL_SEG_NO", OracleType.NVarChar, 15),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.NVarChar, 15),
                                         new OracleParameter("pFROM_SYSTEM", OracleType.NVarChar, 15),
                                         new OracleParameter("pTO_SYSTEM", OracleType.NVarChar, 15),
                                         new OracleParameter("pTRANS_DATE", OracleType.NVarChar, 15),
                                         new OracleParameter("pEXCEPTION_TYPE", OracleType.NVarChar, 15)};

            oraParam[0].Value = objTable.GW_TYPE;
            oraParam[1].Value = objTable.ACC_NUM;
            oraParam[2].Value = objTable.ACC_TYPE;
            oraParam[3].Value = objTable.RUNNING_SEG_NO;
            oraParam[4].Value = objTable.REF_NO;
            oraParam[5].Value = objTable.MSG_TYPE;
            oraParam[6].Value = objTable.SEG_NO;
            oraParam[7].Value = objTable.MSG_NO;
            oraParam[8].Value = objTable.CCY;
            oraParam[9].Value = objTable.AMOUNT;
            oraParam[10].Value = objTable.APP_CODE;
            oraParam[11].Value = objTable.SENDER;
            oraParam[12].Value = objTable.ORG_BANK;
            oraParam[13].Value = objTable.RECEIVING_BRANCH;
            oraParam[14].Value = objTable.RECEIVER;
            oraParam[15].Value = objTable.VALUE_DATE;
            oraParam[16].Value = objTable.JOURNAL_SEG_NO;
            oraParam[17].Value = objTable.MSG_DIRECTION;
            oraParam[18].Value = objTable.FROM_SYSTEM;
            oraParam[19].Value = objTable.TO_SYSTEM;
            oraParam[20].Value = objTable.TRANS_DATE;
            oraParam[21].Value = objTable.EXCEPTION_TYPE;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int ClearTTSP_RECONCILE()
        {
            string strSql = "DELETE FROM TTSP_MSG_REC";

            //if (strDepartment == "ALL")
            //    strSql = strSql + " AND Upper(APP_CODE)='" + strDepartment + "' AND(FROM_SYSTEM='SIBS' or TO_SYSTEM='SIBS'";
            //else
            //    strSql = strSql + " AND(FROM_SYSTEM='IBPS' or TO_SYSTEM='IBPS') ";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public DataSet GetTTSP_RECONCILE(string dDate, string strDepartment, string strDirection, string strException,string strType,string strMsgType)
        {
            DataSet datDs = new DataSet();
            string strSQL = " SELECT TTSP_MSG_REC.MSG_ID, TTSP_MSG_REC.ACC_NUM,TTSP_MSG_REC.REF_NO,TTSP_MSG_REC.MSG_TYPE,TTSP_MSG_REC.APP_CODE,TTSP_MSG_REC.Sender, TTSP_MSG_REC.Receiver, ";
            strSQL = strSQL + " TTSP_MSG_REC.Amount, TTSP_MSG_REC.CCY, TTSP_MSG_REC.TRANS_DATE , TTSP_MSG_REC.VALUE_DATE, A.CONTENT EXCEPTION_TYPE, TTSP_MSG_REC.REC_TYPE";
            strSQL = strSQL + " FROM TTSP_MSG_REC, (select CDVAL,CONTENT from allcode where CDNAME = 'RecView' AND GWTYPE = 'TTSP') A WHERE TTSP_MSG_REC.EXCEPTION_TYPE = A.CDVAL AND Upper(TO_CHAR(TRANS_DATE,'YYYYMMDD')) = '" + dDate + "'";
            if (strDirection != "ALL")
                strSQL = strSQL + "   AND Upper(MSG_DIRECTION) = '" + strDirection + "' ";

            if (strDepartment != "ALL")
                strSQL = strSQL + " AND Upper(APP_CODE) = '" + strDepartment + "' AND (FROM_SYSTEM='TTSP' Or TO_SYSTEM='TTSP')";
            else
                strSQL = strSQL + " AND (FROM_SYSTEM='TTSP' Or TO_SYSTEM='TTSP')";

            if (strException != "ALL")
                strSQL = strSQL + " AND Upper(EXCEPTION_TYPE) = '" + strException + "'";

            if (strType != "ALL")
                strSQL = strSQL + " AND Upper(REC_TYPE) = '" + strType + "'";

            if (strMsgType != "ALL")
                strSQL = strSQL + " AND Upper(MSG_TYPE) = '" + strMsgType + "'";

            strSQL = strSQL + " ORDER BY EXCEPTION_TYPE";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int TTSP_Reconcile(DateTime strDate)
        {
            //string strSql = "GW_PK_IBPS_Q_ConvertOut.Forward_LowValue";
            string strSql = "TTSP_RECONCILE";

            OracleParameter[] oraParam = { new OracleParameter("pDate", OracleType.DateTime, 15) };
            oraParam[0].Value = strDate;
            try
            {
                oraConn = oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch 
            {
                return -1; ;
            }
        }

        public int TTSP_Reconcile_TTSP(DateTime strDate)
        {
            //string strSql = "GW_PK_IBPS_Q_ConvertOut.Forward_LowValue";
            string strSql = "TTSP_RECONCILE_TTSP";

            OracleParameter[] oraParam = { new OracleParameter("pDate", OracleType.DateTime, 15) };
            oraParam[0].Value = strDate;
            try
            {
                oraConn = oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch 
            {
                return -1; ;
            }
        }

        public int InsertTTSP_MSG_REC_ALL(DateTime dtDate)
        {
            string 
            strSQL = " BEGIN ";
            strSQL +=  " DELETE FROM TTSP_MSG_REC_ALL WHERE TO_CHAR(TRANS_DATE,'DDMMYYYY')= '" + dtDate.Date.ToString("ddMMyyyy")+ "';";
            strSQL += " INSERT INTO TTSP_MSG_REC_ALL(GW_TYPE, ACC_NUM,ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSQL += " RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE, EXCEPTION_TYPE,REC_TYPE,REC_TIME,TELLER_ID) ";
            strSQL += " (SELECT GW_TYPE, ACC_NUM,ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSQL += "  RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE, EXCEPTION_TYPE,REC_TYPE,REC_TIME,TELLER_ID FROM TTSP_MSG_REC ";
            strSQL += " WHERE TO_CHAR(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "');";
            strSQL += " END; ";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
        }

        public int InsertTTSP_MSG_REC_TOTAL(DateTime dtDate)
        {
            string strSQL = "INSERT INTO TTSP_MSG_REC_TOTAL(rec_date,CCY, AMOUNT,rec_count, APP_CODE, MSG_DIRECTION,TRANS_DATE, EXCEPTION_TYPE) ";
            strSQL = strSQL + " (SELECT SYSDATE,CCY, SUM(AMOUNT),COUNT(*), APP_CODE, MSG_DIRECTION,TO_DATE(TRANS_DATE,'DD/MM/RRRR'), EXCEPTION_TYPE FROM TTSP_MSG_REC ";
            strSQL = strSQL + " WHERE TO_CHAR(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' GROUP BY CCY,APP_CODE, MSG_DIRECTION,TO_DATE(TRANS_DATE,'DD/MM/RRRR'), EXCEPTION_TYPE)";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
        }



    }
}
