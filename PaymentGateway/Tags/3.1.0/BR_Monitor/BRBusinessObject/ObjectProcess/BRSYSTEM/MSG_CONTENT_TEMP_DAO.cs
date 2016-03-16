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
    public class MSG_CONTENT_TEMPDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public MSG_CONTENT_TEMPDP()
        {
        }
        public static MSG_CONTENT_TEMPDP Instance()
        {
            return new MSG_CONTENT_TEMPDP();
        }

        public int AddMSG_CONTENT_TEMP(MSG_CONTENT_TEMPInfo objTable)
        {
            string strSql = "INSERT INTO MSG_CONTENT_TEMP (ACT_NUM, ACT_TYPE, MSG_TYPE, MSG_NO, ";
            strSql = strSql + "SEQ_NO, REF_NO, TELEX_NO, MSG_DIRECTION, AMOUNT, CCY, TRANS_DATE, SENDER, RECEIVER, DEPARTMENT, GW_TYPE, MSG_ID) ";
            strSql = strSql + " VALUES (:pACT_NUM,:pACT_TYPE,:pMSG_TYPE, :pMSG_NO, :pSEQ_NO, :pREF_NO, :pTELEX_NO, :pMSG_DIRECTION, :pAMOUNT, :pCCY, :pTRANS_DATE, :pSENDER, :pRECEIVER,";
            strSql = strSql + ":pDEPARTMENT, :pGW_TYPE, :pMSG_ID)";

            OracleParameter[] oraParam = {new OracleParameter("pACT_NUM", OracleType.NVarChar, 15),
                                         new OracleParameter("pACT_TYPE",OracleType.NVarChar,15),
                                         new OracleParameter("pMSG_TYPE",OracleType.NVarChar,70),
                                         new OracleParameter("pMSG_NO",OracleType.NVarChar,6),
                                         new OracleParameter("pSEQ_NO",OracleType.Number,10),
                                         new OracleParameter("pREF_NO",OracleType.Number,10),
                                         new OracleParameter("pTELEX_NO",OracleType.Number,10),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.NVarChar, 100),
                                         new OracleParameter("pAMOUNT", OracleType.NVarChar, 15),
                                         new OracleParameter("pCCY", OracleType.NVarChar, 15),
                                         new OracleParameter("pTRANS_DATE", OracleType.NVarChar, 15),
                                         new OracleParameter("pSENDER", OracleType.NVarChar, 15),
                                         new OracleParameter("pRECEIVER", OracleType.NVarChar, 15),
                                         new OracleParameter("pDEPARTMENT", OracleType.NVarChar, 15),
                                         new OracleParameter("pGW_TYPE", OracleType.NVarChar, 15),
                                         new OracleParameter("pMSG_ID", OracleType.NVarChar, 15)};

            oraParam[0].Value = objTable.ACT_NUM;
            oraParam[1].Value = objTable.ACT_TYPE;
            oraParam[2].Value = objTable.MSG_TYPE;
            oraParam[3].Value = objTable.MSG_NO;
            oraParam[4].Value = objTable.SEQ_NO;
            oraParam[5].Value = objTable.REF_NO;
            oraParam[6].Value = objTable.TELEX_NO;
            oraParam[7].Value = objTable.MSG_DIRECTION;
            oraParam[8].Value = objTable.AMOUNT;
            oraParam[9].Value = objTable.CCY;
            oraParam[10].Value = objTable.TRANS_DATE;
            oraParam[11].Value = objTable.SENDER;
            oraParam[12].Value = objTable.RECEIVER;
            oraParam[13].Value = objTable.DEPARTMENT;
            oraParam[14].Value = objTable.GW_TYPE;
            oraParam[15].Value = objTable.MSG_ID;

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

        public int ClearMSG_CONTENT_TEMP()
        {
            string strSql = "Truncate table MSG_CONTENT_TEMP";

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


        public DataSet GetMSG_CONTENT_TEMP(string dDate, string strDepartment, string strDirection, string strChannel)
        {
            DataSet datDs = new DataSet();
            string strSQL = " Select * FROM MSG_CONTENT_TEMP WHERE MSG_DIRECTION = '" + strDirection + "' AND GW_TYPE='" + strChannel + "'";
            strSQL = strSQL + " AND Upper(TO_CHAR(TRANS_DATE,'DD/MM/YYYY')) = '" + dDate + "' ";
            if (strDepartment.ToString() == "ALL")
                strSQL = strSQL + " ORDER BY ACT_NUM,ACT_TYPE,SEQ_NO ASC";
            else
                strSQL = strSQL + " AND Upper(DEPARTMENT)='" + strDepartment + "' ORDER BY ACT_NUM, ACT_TYPE, SEG_NO ASC";

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
