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
    /*************************************************************
     * Doi tuong lam viec voi database cua bang ttsp_msg_rec_temp
     * HoangLA
     *************************************************************/
    public class TTSP_MSG_REC_TEMDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        
        public TTSP_MSG_REC_TEMDP()
        {
        }
        public static TTSP_MSG_REC_TEMDP Instance()
        {
            return new TTSP_MSG_REC_TEMDP();
        }

        /*************************************************************
         * Insert dien tu SIBS vao REC_TEMP         
         *************************************************************/
        public int AddTTSP_MSG_REC_TEM(TTSP_MSG_REC_TEMPInfo objTable)
        {
            string strSql = "INSERT INTO TTSP_MSG_REC_TEMP (GW_TYPE, ACC_NUM, ";
            strSql = strSql + "ACC_TYPE, RUNNING_SEG_NO, REF_NO, MSG_TYPE, SEG_NO, MSG_NO, CCY, AMOUNT, APP_CODE, SENDER, ORG_BANK, ";
            strSql = strSql + "RECEIVING_BRANCH, RECEIVER, VALUE_DATE, JOURNAL_SEG_NO, MSG_DIRECTION, FROM_SYSTEM, TO_SYSTEM, TRANS_DATE,REC_TYPE)";
            strSql = strSql + " VALUES (:pGW_TYPE,:pACC_NUM,:pACC_TYPE, :pRUNNING_SEG_NO, :pREF_NO, :pMSG_TYPE, :pSEG_NO, :pMSG_NO, :pCCY, :pAMOUNT, :pAPP_CODE, :pSENDER, :pORG_BANK,";
            strSql = strSql + ":pRECEIVING_BRANCH, :pRECEIVER, :pVALUE_DATE, :pJOURNAL_SEG_NO, :pMSG_DIRECTION, :pFROM_SYSTEM, :pTO_SYSTEM, :pTRANS_DATE,:pREC_TYPE)";

            OracleParameter[] oraParam = {new OracleParameter("pGW_TYPE", OracleType.VarChar, 10),
                                         new OracleParameter("pACC_NUM",OracleType.VarChar,19),
                                         new OracleParameter("pACC_TYPE",OracleType.VarChar,50),
                                         new OracleParameter("pRUNNING_SEG_NO",OracleType.Number,10),
                                         new OracleParameter("pREF_NO",OracleType.VarChar,16),
                                         new OracleParameter("pMSG_TYPE",OracleType.VarChar,15),
                                         new OracleParameter("pSEG_NO",OracleType.Number,10),
                                         new OracleParameter("pMSG_NO", OracleType.Number, 10),
                                         new OracleParameter("pCCY", OracleType.VarChar, 4),
                                         new OracleParameter("pAMOUNT", OracleType.Number, 18),
                                         new OracleParameter("pAPP_CODE", OracleType.VarChar, 15),
                                         new OracleParameter("pSENDER", OracleType.VarChar, 12),
                                         new OracleParameter("pORG_BANK", OracleType.VarChar, 12),
                                         new OracleParameter("pRECEIVING_BRANCH", OracleType.VarChar, 11),
                                         new OracleParameter("pRECEIVER", OracleType.VarChar, 12),////
                                         new OracleParameter("pVALUE_DATE", OracleType.DateTime, 12),
                                         new OracleParameter("pJOURNAL_SEG_NO", OracleType.Number, 18),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.VarChar, 1),
                                         new OracleParameter("pFROM_SYSTEM", OracleType.VarChar, 10),
                                         new OracleParameter("pTO_SYSTEM", OracleType.VarChar, 10),
                                         new OracleParameter("pTRANS_DATE", OracleType.DateTime, 12),
                                         new OracleParameter("pREC_TYPE", OracleType.VarChar, 12)
                                         };

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
            oraParam[21].Value = objTable.REC_TYPE;

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

        /*************************************************************
         * Xoa bang REC_TEMP        
         *************************************************************/
        public int ClearTTSP_MSG_REC_TEM()
        {
            string strSql = "DELETE FROM TTSP_MSG_REC_TEMP";

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

        /*************************************************************
         * Lay du lieu tu bang REC_TEMP       
         *************************************************************/
        public DataSet GetTTSP_MSG_REC_TEM(string strDirection, string strDepartment)
        {
            DataSet datDs = new DataSet();
            string strSQL = "Select * from TTSP_MSG_REC_TEMP Where Upper(APP_CODE)='" + strDepartment + "' AND Upper(MSG_DIRECTION) ='" + strDirection + "' order by ACC_NUM, ACC_TYPE, SEG_NO ASC";

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