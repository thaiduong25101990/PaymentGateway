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
    public class IBPS_FREEDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        public IBPS_FREEDP()
        {
        }
        public static IBPS_FREEDP Instance()
        {
            return new IBPS_FREEDP();
        }
        public DataTable GetIBPS_FREE(string pTRANSTYPE, string pFREETYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select FROMDATE,TODATE,TRANSTYPE,FREETYPE,FREETIME,";
            strSQL = strSQL + "HARDFREE,PERCENTFREE,Minfree,Maxfree from Ibps_Free  where TRANSTYPE ='" + pTRANSTYPE + "' and FREETYPE = '" + pFREETYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public DataTable DELETEIBPS_FREE(string pTRANSTYPE, string pFREETYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "Delete Ibps_Free  where TRANSTYPE ='" + pTRANSTYPE + "' and FREETYPE in (" + pFREETYPE + ")";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public int ADDIBPS_FREE(IBPS_FREEInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "insert into Ibps_Free(FROMDATE,TODATE,TRANSTYPE,FREETYPE,FREETIME,HARDFREE,PERCENTFREE,Minfree,Maxfree) values ";
            strSQL = strSQL + " (:pFROMDATE,:pTODATE,:pTRANSTYPE,:pFREETYPE,:pFREETIME,:pHARDFREE,:pPERCENTFREE,:pMINFREE,:pMAXFREE) ";
            OracleParameter[] Orapara = {new OracleParameter("pFROMDATE",OracleType.DateTime,7),
                                        new OracleParameter("pTODATE",OracleType.DateTime,7),
                                        new OracleParameter("pTRANSTYPE",OracleType.VarChar,20),
                                        new OracleParameter("pFREETYPE",OracleType.VarChar,10),
                                        new OracleParameter("pFREETIME",OracleType.DateTime,7),
                                        new OracleParameter("pHARDFREE",OracleType.Number,20),
                                        new OracleParameter("pPERCENTFREE",OracleType.Number,4),
                                        new OracleParameter("pMINFREE",OracleType.Number,20),
                                        new OracleParameter("pMAXFREE",OracleType.Number,20)                                       
                                        };

            Orapara[0].Value = objTable.FROMDATE;
            Orapara[1].Value = objTable.TODATE;
            Orapara[2].Value = objTable.TRANSTYPE;
            Orapara[3].Value = objTable.FREETYPE;
            Orapara[4].Value = objTable.FREETIME;
            Orapara[5].Value = objTable.HARDFREE;
            Orapara[6].Value = objTable.PERCENTFREE;
            Orapara[7].Value = objTable.MINFREE;
            Orapara[8].Value = objTable.MAXFREE;


            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
            }
            catch 
            {
                return -1;
            }
        }

        public int UPDATEIBPS_FREE(IBPS_FREEInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "Update Ibps_Free set FROMDATE = :pFROMDATE,TODATE =:pTODATE, TRANSTYPE=:pTRANSTYPE,FREETYPE=:pFREETYPE,";
            strSQL = strSQL + "FREETIME=:pFREETIME,HARDFREE=:pHARDFREE,PERCENTFREE=:pPERCENTFREE,MINFREE=:pMINFREE,MAXFREE=:pMAXFREE  where TRANSTYPE =:pTRANSTYPE";
            OracleParameter[] Orapara = { new OracleParameter("pFROMDATE",OracleType.DateTime,7),
                                        new OracleParameter("pTODATE",OracleType.DateTime,7),                       
                                        new OracleParameter("pTRANSTYPE",OracleType.VarChar,20),
                                        new OracleParameter("pFREETYPE",OracleType.VarChar,10),
                                        new OracleParameter("pFREETIME",OracleType.DateTime,7),
                                        new OracleParameter("pHARDFREE",OracleType.Number,20),
                                        new OracleParameter("pPERCENTFREE",OracleType.Number,4),
                                        new OracleParameter("pMINFREE",OracleType.Number,20),
                                        new OracleParameter("pMAXFREE",OracleType.Number,20)                                        
                                        };

            Orapara[0].Value = objTable.FROMDATE;
            Orapara[1].Value = objTable.TODATE;
            Orapara[2].Value = objTable.TRANSTYPE;
            Orapara[3].Value = objTable.FREETYPE;
            Orapara[4].Value = objTable.FREETIME;
            Orapara[5].Value = objTable.HARDFREE;
            Orapara[6].Value = objTable.PERCENTFREE;
            Orapara[7].Value = objTable.MINFREE;
            Orapara[8].Value = objTable.MAXFREE;

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
            }
            catch 
            {
                return -1;
            }

        }
        //DatHM
        public DataTable FREE_NON_HARDFREE(Double hardfree, DateTime fromdate, DateTime todate)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select U.SumLV * " + hardfree + " as SumLV,U.NumLV,V.SumHV * " + hardfree + " as SumHV,V.NumHV,X.BRAN_NAME,X.SIBS_BANK_CODE,Y.SumDCV * " + hardfree + " as SumDCV,Y.NumDCV";
            strSQL = strSQL + " from (select ID,SIBS_BANK_CODE,BRAN_NAME from branch )X left join (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='101001' group by T.SOURCE_BRANCH";
            strSQL = strSQL + " )U on X.SIBS_BANK_CODE=substr(U.SOURCE_BRANCH,3,3) left join(select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='201001' group by T.SOURCE_BRANCH )V";
            strSQL = strSQL + " on X.SIBS_BANK_CODE=substr(V.SOURCE_BRANCH,3,3) ";

            //
            strSQL = strSQL + " left join(select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV";
            strSQL = strSQL + " from(select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " )T group by T.SOURCE_BRANCH )Y on X.SIBS_BANK_CODE=substr(Y.SOURCE_BRANCH,3,3)";
            OracleParameter[] Orapra ={new OracleParameter("fromdate",OracleType.DateTime,7),
                                     new OracleParameter("todate",OracleType.DateTime,7)};
            Orapra[0].Value = fromdate;
            Orapra[1].Value = todate;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, Orapra).Tables[0];
            }
            catch 
            {
                return null;
            }

        }
        public DataTable FREE_HARDFREE(DateTime fromdate, DateTime todate)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,X.SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV";
            strSQL = strSQL + " from (select ID,SIBS_BANK_CODE,BRAN_NAME from branch )X left join (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1";
            strSQL = strSQL + " and transdate>= To_char(:fromdate,'YYYYMMDD') and transdate<= To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='101001' group by T.SOURCE_BRANCH";
            strSQL = strSQL + " )U on X.SIBS_BANK_CODE=substr(U.SOURCE_BRANCH,3,3) left join (select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='201001' group by T.SOURCE_BRANCH )V";
            strSQL = strSQL + " on X.SIBS_BANK_CODE=substr(V.SOURCE_BRANCH,3,3) ";

            strSQL = strSQL + " left join(select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV";
            strSQL = strSQL + " from(select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " )T group by T.SOURCE_BRANCH )Y on X.SIBS_BANK_CODE=substr(Y.SOURCE_BRANCH,3,3)";
            OracleParameter[] Orapra ={new OracleParameter("fromdate",OracleType.DateTime,7),
                                     new OracleParameter("todate",OracleType.DateTime,7)};
            Orapra[0].Value = fromdate;
            Orapra[1].Value = todate;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, Orapra).Tables[0];
            }
            catch 
            {
                return null;
            }

        }
        public DataTable FREE_HARDFREE_BEFORE_TIME(DateTime fromdate, DateTime todate, DateTime hour)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,X.SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV";
            strSQL = strSQL + " from (select ID,SIBS_BANK_CODE,BRAN_NAME from branch )X left join (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>= To_char(:fromdate,'YYYYMMDD') and transdate<= To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='101001' group by T.SOURCE_BRANCH";
            strSQL = strSQL + " )U on X.SIBS_BANK_CODE=substr(U.SOURCE_BRANCH,3,3) left join(select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='201001' group by T.SOURCE_BRANCH )V";
            strSQL = strSQL + " on X.SIBS_BANK_CODE=substr(V.SOURCE_BRANCH,3,3) ";

            strSQL = strSQL + " left join(select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV";
            strSQL = strSQL + " from(select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " )T group by T.SOURCE_BRANCH )Y on X.SIBS_BANK_CODE=substr(Y.SOURCE_BRANCH,3,3)";
            OracleParameter[] Orapra ={new OracleParameter("fromdate",OracleType.DateTime,7),
                                     new OracleParameter("todate",OracleType.DateTime,7),
                                      new OracleParameter("hour",OracleType.DateTime,7)
                                      };
            Orapra[0].Value = fromdate;
            Orapra[1].Value = todate;
            Orapra[2].Value = hour;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }

        }
        public DataTable FREE_HARDFREE_AFTER_TIME(DateTime fromdate, DateTime todate, DateTime hour)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,X.SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV";
            strSQL = strSQL + " from (select ID,SIBS_BANK_CODE,BRAN_NAME from branch )X left join (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>= To_char(:fromdate,'YYYYMMDD') and transdate<= To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='101001' group by T.SOURCE_BRANCH";
            strSQL = strSQL + " )U on X.SIBS_BANK_CODE=substr(U.SOURCE_BRANCH,3,3) left join(select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='201001' group by T.SOURCE_BRANCH )V";
            strSQL = strSQL + " on X.SIBS_BANK_CODE=substr(V.SOURCE_BRANCH,3,3) ";

            strSQL = strSQL + " left join(select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV";
            strSQL = strSQL + " from(select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " )T group by T.SOURCE_BRANCH )Y on X.SIBS_BANK_CODE=substr(Y.SOURCE_BRANCH,3,3)";
            OracleParameter[] Orapra ={new OracleParameter("fromdate",OracleType.DateTime,7),
                                     new OracleParameter("todate",OracleType.DateTime,7),
                                      new OracleParameter("hour",OracleType.DateTime,7)
                                      };
            Orapra[0].Value = fromdate;
            Orapra[1].Value = todate;
            Orapra[2].Value = hour;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }

        }
        public DataTable FREE_NON_HARDFREE_BEFORE_TIME(Double hardfree, DateTime fromdate, DateTime todate, DateTime hour)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select U.SumLV * " + hardfree + " as SumLV,U.NumLV,V.SumHV * " + hardfree + " as SumHV,V.NumHV,X.BRAN_NAME,X.SIBS_BANK_CODE,Y.SumDCV * " + hardfree + " as SumDCV,Y.NumDCV";
            strSQL = strSQL + " from (select ID,SIBS_BANK_CODE,BRAN_NAME from branch )X left join (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='101001' group by T.SOURCE_BRANCH";
            strSQL = strSQL + " )U on X.SIBS_BANK_CODE=substr(U.SOURCE_BRANCH,3,3) left join(select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')<=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='201001' group by T.SOURCE_BRANCH )V";
            strSQL = strSQL + " on X.SIBS_BANK_CODE=substr(V.SOURCE_BRANCH,3,3) ";

            strSQL = strSQL + " left join(select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV";
            strSQL = strSQL + " from(select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " )T group by T.SOURCE_BRANCH )Y on X.SIBS_BANK_CODE=substr(Y.SOURCE_BRANCH,3,3)";
            OracleParameter[] Orapra ={new OracleParameter("fromdate",OracleType.DateTime,7),
                                     new OracleParameter("todate",OracleType.DateTime,7),
                                      new OracleParameter("hour",OracleType.DateTime,7)
                                      };
            Orapra[0].Value = fromdate;
            Orapra[1].Value = todate;
            Orapra[2].Value = hour;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, Orapra).Tables[0];
            }
            catch 
            {
                return null;
            }

        }
        public DataTable FREE_NON_HARDFREE_AFTER_TIME(Double hardfree, DateTime fromdate, DateTime todate, DateTime hour)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select U.SumLV * " + hardfree + " as SumLV,U.NumLV,V.SumHV * " + hardfree + " as SumHV,V.NumHV,X.BRAN_NAME,X.SIBS_BANK_CODE,Y.SumDCV * " + hardfree + " as SumDCV,Y.NumDCV";
            strSQL = strSQL + " from (select ID,SIBS_BANK_CODE,BRAN_NAME from branch )X left join (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='101001' group by T.SOURCE_BRANCH";
            strSQL = strSQL + " )U on X.SIBS_BANK_CODE=substr(U.SOURCE_BRANCH,3,3) left join(select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH from(";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') union all";
            strSQL = strSQL + " select MSG_ID,TRANS_CODE,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS where status=1 and to_char(TRANS_DATE,'HH24MISS')>=to_char(:hour,'HH24MISS') ";
            strSQL = strSQL + " and transdate>=To_char(:fromdate,'YYYYMMDD') and transdate<=To_char(:todate,'YYYYMMDD') )T";
            strSQL = strSQL + " where T.TRANS_CODE='201001' group by T.SOURCE_BRANCH )V";
            strSQL = strSQL + " on X.SIBS_BANK_CODE=substr(V.SOURCE_BRANCH,3,3) ";

            strSQL = strSQL + " left join(select T.SOURCE_BRANCH,Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV";
            strSQL = strSQL + " from(select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_CONTENT";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " union all";
            strSQL = strSQL + " select MSG_ID,AMOUNT,SOURCE_BRANCH from IBPS_MSG_ALL_HIS";
            strSQL = strSQL + " where status=1 and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'022'),3,3)";
            strSQL = strSQL + " and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(CONTENT,'019'),3,3)='101'and transdate>=To_char(:fromdate ,'YYYYMMDD') and transdate<=To_char(:todate ,'YYYYMMDD')";
            strSQL = strSQL + " )T group by T.SOURCE_BRANCH )Y on X.SIBS_BANK_CODE=substr(Y.SOURCE_BRANCH,3,3)";
            OracleParameter[] Orapra ={new OracleParameter("fromdate",OracleType.DateTime,7),
                                     new OracleParameter("todate",OracleType.DateTime,7),
                                      new OracleParameter("hour",OracleType.DateTime,7)
                                      };
            Orapra[0].Value = fromdate;
            Orapra[1].Value = todate;
            Orapra[2].Value = hour;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, Orapra).Tables[0];
            }
            catch 
            {
                return null;
            }

        }
        public DataTable CAL_FREE(DateTime fromdate, DateTime todate,
            string pCCYCD, int pType, string pBranch)
        {
            try
            {

                string strSQL = "GW_PK_IBPS_REPORT.BK02";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),
                                              new OracleParameter("pCCYCD", OracleType.VarChar,5),
                                              new OracleParameter("pType", OracleType.Int16,2),
                                              new OracleParameter("pBranch", OracleType.VarChar,10),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pCCYCD;
                oraPara[3].Value = pType;
                oraPara[4].Value = pBranch;
                oraPara[5].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraPara).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
    }
}
