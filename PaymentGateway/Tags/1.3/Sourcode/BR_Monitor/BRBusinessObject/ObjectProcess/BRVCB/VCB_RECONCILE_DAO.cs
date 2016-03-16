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

namespace BR.BRBusinessObject
{
    public class VCB_RECONCILEDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public VCB_RECONCILEDP()
        {
        }
        public static VCB_RECONCILEDP Instance()
        {
            return new VCB_RECONCILEDP();
        }

        public int AddVCB_RECONCILE(VCB_RECONCILEInfo objTable)
        {
            string strSql = "INSERT INTO VCB_MSG_REC (GW_TYPE, ACC_NUM,";
            strSql = strSql + "ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSql = strSql + "RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE, EXCEPTION_TYPE)";
            strSql = strSql + " VALUES (:pGW_TYPE,:pACC_NUM,:pACC_TYPE, :pRUNNING_SEG_NO, :pREF_NO, :pMSG_TYPE, :pSEG_NO, :pMSG_NO, :pCCY, :pAMOUNT, :pAPP_CODE, :pSENDER, :pORG_BANK,)";
            strSql = strSql + ":pRECEIVING_BRANCH, :pRECEIVER, :pVALUE_DATE, :pJOURNAL_SEG_NO, :pMSG_DIRECTION, :pFROM_SYSTEM, :pTO_SYSTEM, :pTRANS_DATE, :pEXCEPTION_TYPE)";

            OracleParameter[] oraParam = {new OracleParameter("pGW_TYPE", OracleType.NVarChar, 10),
                                         new OracleParameter("pACC_NUM",OracleType.NVarChar,30),
                                         new OracleParameter("pACC_TYPE",OracleType.NVarChar,2),
                                         new OracleParameter("pRUNNING_SEG_NO",OracleType.NVarChar,10),
                                         new OracleParameter("pREF_NO",OracleType.Number,16),
                                         new OracleParameter("pMSG_TYPE",OracleType.Number,5),
                                         new OracleParameter("pSEG_NO",OracleType.Number,3),
                                         new OracleParameter("pMSG_NO", OracleType.NVarChar, 3),
                                         new OracleParameter("pCCY", OracleType.NVarChar, 4),
                                         new OracleParameter("pAMOUNT", OracleType.NVarChar, 18),
                                         new OracleParameter("pAPP_CODE", OracleType.NVarChar, 2),
                                         new OracleParameter("pSENDER", OracleType.NVarChar, 15),
                                         new OracleParameter("pORG_BANK", OracleType.NVarChar, 15),
                                         new OracleParameter("pRECEIVING_BRANCH", OracleType.NVarChar, 15),
                                         new OracleParameter("pRECEIVER", OracleType.NVarChar, 15),
                                         new OracleParameter("pVALUE_DATE", OracleType.DateTime, 15),
                                         new OracleParameter("pJOURNAL_SEG_NO", OracleType.NVarChar, 18),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.NVarChar, 2),
                                         new OracleParameter("pFROM_SYSTEM", OracleType.NVarChar, 10),
                                         new OracleParameter("pTO_SYSTEM", OracleType.NVarChar, 10),
                                         new OracleParameter("pTRANS_DATE", OracleType.DateTime, 15),
                                         new OracleParameter("pEXCEPTION_TYPE", OracleType.NVarChar, 1),
                                         new OracleParameter("pSESSION_NO", OracleType.NVarChar, 20),
                                         new OracleParameter("pOSN", OracleType.NVarChar, 20),
                                         new OracleParameter("pTRANS_NO", OracleType.NVarChar, 30)};

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
            oraParam[22].Value = objTable.SESSION_NO;
            oraParam[23].Value = objTable.OSN;
            oraParam[24].Value = objTable.TRANS_NO;
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

        public int ClearVCB_RECONCILE(string dDate, string strDepartment)
        {
            string strSql = "DELETE FROM VCB_MSG_REC";

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
        
        public DataSet GetVCB_RECONCILE(string dDate, string strDepartment, string strDirection, string strException, string strType,string strMsgType)
        {
            DataSet datDs = new DataSet();
            string strSQL = " SELECT VCB_MSG_REC.MSG_TYPE MSG_TYPE ,LTRIM(VCB_MSG_REC.ACC_NUM,'0') RM_NUMBER ,VCB_MSG_REC.REF_NO  ,VCB_MSG_REC.AMOUNT  ,VCB_MSG_REC.CCY  ,LTRIM(VCB_MSG_REC.Sender,'0') SENDER ,VCB_MSG_REC.Receiver, ";
            strSQL = strSQL + " VCB_MSG_REC.APP_CODE  ,VCB_MSG_REC.TRANS_DATE  ,VCB_MSG_REC.VALUE_DATE  ,A.CONTENT EXCEPTION_TYPE  , REC_TYPE,VCB_MSG_REC.MSG_ID";
            strSQL = strSQL + " FROM VCB_MSG_REC,(select CDVAL,CONTENT from allcode where CDNAME = 'RecView' AND GWTYPE = 'VCB') A WHERE VCB_MSG_REC.EXCEPTION_TYPE = A.CDVAL AND Upper(TO_CHAR(TRANS_DATE,'YYYYMMDD')) = '" + dDate + "'";
            
            if (strDirection !="ALL")
                strSQL = strSQL + "   AND Upper(MSG_DIRECTION) = '" + strDirection + "'";

            if (strDepartment != "ALL")
                strSQL = strSQL + " AND Upper(APP_CODE) = '" + strDepartment + "' AND (FROM_SYSTEM='SIBS' Or TO_SYSTEM='SIBS')";
            else
                strSQL = strSQL + " AND (FROM_SYSTEM='VCB' Or TO_SYSTEM='VCB')";

            if (strException != "ALL")
                strSQL = strSQL + " AND Upper(EXCEPTION_TYPE) = '" + strException + "'";

            if (strType != "ALL")
                strSQL = strSQL + " AND Upper(REC_TYPE) = '" + strType + "'";

            if (strMsgType!= "ALL")
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

        public int VCB_Reconcile(DateTime strDate)
        {
            //string strSql = "GW_PK_IBPS_Q_ConvertOut.Forward_LowValue";
            string strSql = "VCB_reconcile";

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

        public int VCB_Reconcile_VCB(DateTime strDate)
        {
            //string strSql = "GW_PK_IBPS_Q_ConvertOut.Forward_LowValue";
            string strSql = "VCB_reconcile_VCB";

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

        public int InsertVCB_MSG_REC_ALL(DateTime dtDate)
        {
            string strSQL = "BEGIN ";
            strSQL += " DELETE FROM VCB_MSG_REC_ALL WHERE to_char(TRANS_DATE,'DDMMYYYY')= " + dtDate.Date.ToString("ddMMyyyy")+ "; ";
            strSQL += " INSERT INTO VCB_MSG_REC_ALL(GW_TYPE, ACC_NUM,ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSQL += " RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE, EXCEPTION_TYPE,REC_TYPE,REC_TIME,TELLER_ID) ";
            strSQL += " (SELECT GW_TYPE, ACC_NUM,ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSQL += "  RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE, EXCEPTION_TYPE,REC_TYPE,REC_TIME,TELLER_ID FROM VCB_MSG_REC ";
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

        public int InsertVCB_MSG_REC_TOTAL(DateTime dtDate)
        {
            string strSQL = "INSERT INTO VCB_MSG_REC_TOTAL(rec_date,CCY, AMOUNT,rec_count, APP_CODE, MSG_DIRECTION,TRANS_DATE, EXCEPTION_TYPE) ";
            strSQL = strSQL + " (SELECT SYSDATE,CCY, SUM(AMOUNT),COUNT(*), APP_CODE, MSG_DIRECTION,TO_DATE(TRANS_DATE,'DD/MM/RRRR'), EXCEPTION_TYPE FROM VCB_MSG_REC ";
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
