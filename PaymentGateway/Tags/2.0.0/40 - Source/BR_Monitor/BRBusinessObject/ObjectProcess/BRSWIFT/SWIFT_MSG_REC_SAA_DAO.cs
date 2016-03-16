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
    public class SWIFT_MSG_REC_SAADP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public SWIFT_MSG_REC_SAADP()
        {
        }
        public static SWIFT_MSG_REC_SAADP Instance()
        {
            return new SWIFT_MSG_REC_SAADP();
        }

        public int AddSWIFT_MSG_REC_SAA(SWIFT_MSG_REC_SAAInfo objTable)
        {
            string strSql = "INSERT INTO SWIFT_MSG_REC_SAA (UMID, VALUE_DATE,CCY,  AMOUNT, FORMAT, STATUS, SUFFIX,RECEPTION_INFO,EMISSION_INFO, ";
            strSql = strSql + "NETW_STATUS,ORIG_INST_RP,CREATION_DATE,SENDER,RECEIVER,LOCATION,MSG_DIRECTION,REF_NO,TRANS_DATE) VALUES (";
            strSql = strSql + ":pUMID, :pVALUE_DATE,:pCCY,  :pAMOUNT, :pFORMAT, :pSTATUS, :pSUFFIX,:pRECEPTION_INFO,:pEMISSION_INFO,";
            strSql = strSql + ":pNETW_STATUS,:pORIG_INST_RP,:pCREATION_DATE,:pSENDER,:pRECEIVER,:pLOCATION,:pMSG_DIRECTION,:pREF_NO,:pTRANS_DATE)";
            OracleParameter[] oraParam = {new OracleParameter("pUMID", OracleType.VarChar, 35),
                                         new OracleParameter("pVALUE_DATE",OracleType.DateTime,18),
                                         new OracleParameter("pCCY",OracleType.VarChar,4),
                                         new OracleParameter("pAMOUNT",OracleType.VarChar,126),
                                         new OracleParameter("pFORMAT",OracleType.VarChar,18),
                                         new OracleParameter("pSTATUS",OracleType.VarChar,14),
                                         new OracleParameter("pSUFFIX",OracleType.VarChar,17),
                                         new OracleParameter("pRECEPTION_INFO", OracleType.VarChar, 26),
                                         new OracleParameter("pEMISSION_INFO", OracleType.VarChar, 26),
                                         new OracleParameter("pNETW_STATUS", OracleType.VarChar, 23),
                                         new OracleParameter("pORIG_INST_RP", OracleType.VarChar, 23),
                                         new OracleParameter("pCREATION_DATE", OracleType.DateTime, 18),
                                         new OracleParameter("pSENDER", OracleType.VarChar, 12),
                                         new OracleParameter("pRECEIVER", OracleType.VarChar, 12),
                                         new OracleParameter("pLOCATION", OracleType.VarChar, 25),////
                                         new OracleParameter("pMSG_DIRECTION", OracleType.VarChar, 2),
                                         new OracleParameter("pREF_NO", OracleType.VarChar, 17),
                                         new OracleParameter("pTRANS_DATE", OracleType.DateTime, 18)};

            oraParam[0].Value = objTable.UMID;
            oraParam[1].Value = objTable.VALUE_DATE;
            oraParam[2].Value = objTable.CCY;
            oraParam[3].Value = objTable.AMOUNT;
            oraParam[4].Value = objTable.FORMAT;
            oraParam[5].Value = objTable.STATUS;
            oraParam[6].Value = objTable.SUFFIX;
            oraParam[7].Value = objTable.RECEPTION_INFO;
            oraParam[8].Value = objTable.EMISSION_INFO;
            oraParam[9].Value = objTable.NETW_STATUS;
            oraParam[10].Value = objTable.ORIG_INST_RP;
            oraParam[11].Value = objTable.CREATION_DATE;
            oraParam[12].Value = objTable.SENDER;
            oraParam[13].Value = objTable.RECEIVER;
            oraParam[14].Value = objTable.LOCATION;
            oraParam[15].Value = objTable.MSG_DIRECTION;
            oraParam[16].Value = objTable.REF_NO;
            oraParam[17].Value = objTable.TRANS_DATE;


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

        public int ClearSWIFT_MSG_REC_SAA(string dDate, string strDepartment)
        {
            string strSql = "DELETE FROM SWIFT_MSG_REC_SAA WHERE TO_CHAR(TRANS_DATE, 'YYYYMMDD') = '" + dDate + "'";

            if (strDepartment == "ALL")
                strSql = strSql + " AND Upper(APP_CODE)='" + strDepartment + "' AND(FROM_SYSTEM='SIBS' or TO_SYSTEM='SIBS'";
            else
                strSql = strSql + " AND(FROM_SYSTEM='IBPS' or TO_SYSTEM='IBPS') ";

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


        public DataSet GetSWIFT_MSG_REC_SAA(string strDirection, string strDepartment)
        {
            DataSet datDs = new DataSet();
            string strSQL = "Select * from SWIFT_MSG_REC_SAA Where Upper(APP_CODE)='" + strDepartment + "' AND Upper(MSG_DIRECTION) ='" + strDirection + "' order by ACC_NUM, ACC_TYPE, SEG_NO ASC";

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

    }
}