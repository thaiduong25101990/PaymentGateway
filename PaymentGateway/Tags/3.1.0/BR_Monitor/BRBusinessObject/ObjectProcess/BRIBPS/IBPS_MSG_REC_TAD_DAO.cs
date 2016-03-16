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
    public class IBPS_MSG_REC_TADDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public IBPS_MSG_REC_TADDP()
        {
        }
        public static IBPS_MSG_REC_TADDP Instance()
        {
            return new IBPS_MSG_REC_TADDP();
        }

        public int AddIBPS_MSG_REC_TAD(IBPS_MSG_REC_TADInfo objTable)
        {
            string strSql = "INSERT INTO IBPS_MSG_REC_TAD (REF_NO, TRANS_DATE, AMOUNT, CCY,SENDER, RECEIVER, STATUS) ";
            strSql = strSql + "VALUES (:pREF_NO, :pTRANS_DATE,:pAMOUNT,:pCCY,:pSENDER,:pRECEIVER,:pSTATUS) ";

            OracleParameter[] oraParam = {
                                         new OracleParameter("pREF_NO",OracleType.Number,10),
                                         new OracleParameter("pTRANS_DATE", OracleType.NVarChar, 15),
                                         new OracleParameter("pAMOUNT", OracleType.NVarChar, 15),
                                         new OracleParameter("pCCY", OracleType.NVarChar, 15),
                                         new OracleParameter("pSENDER", OracleType.NVarChar, 15),
                                         new OracleParameter("pRECEIVER", OracleType.NVarChar, 15),
                                         new OracleParameter("pSTATUS", OracleType.NVarChar, 15)};

            oraParam[0].Value = objTable.REF_NO;
            oraParam[1].Value = objTable.TRANS_DATE;
            oraParam[2].Value = objTable.AMOUNT;
            oraParam[3].Value = objTable.CCY;
            oraParam[4].Value = objTable.SENDER;
            oraParam[5].Value = objTable.RECEIVER;
            oraParam[6].Value = objTable.STATUS;


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

        public int ClearIBPS_MSG_REC_TAD()
        {
            string strSql = "DELETE FROM IBPS_MSG_REC_TAD";

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


        public DataSet GetIBPS_MSG_REC_TAD()
        {
            DataSet datDs = new DataSet();
            string strSQL = " Select * from IBPS_MSG_REC_TAD Where status='1'  order by REF_NO,TRANS_DATE,AMOUNT,SENDER,RECEIVER ASC";
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
