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
    public class IBPS_FEEDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public IBPS_FEEDP()
        {

        }
        public static IBPS_FEEDP Instance()
        {
            return new IBPS_FEEDP();
        }

        public DataSet GetBRANCH8(string pBRANCH)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select * from ibps_bank_map a where " +
                "substr(a.gw_bank_code,3,3) ='302' and a.sibs_bank_code=" + 
                pBRANCH.PadLeft(5,'0');
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetIBPS_FEE(string pTRANS_TYPE, string pFEEDISC_TYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "";
            strSQL = "SELECT ID,TRANS_TYPE,FEEDISC_TIME,FIXED_FEE,FEEDISC_TYPE," +
                "(SELECT CONTENT FROM ALLCODE WHERE GWTYPE='IBPS' AND " +
                "CDNAME='FEE_DISC_TYPE' AND CDVAL=A.FEEDISC_TYPE) AS FEEDISC_TYPE1," +
                "RATE_FEE,MIN_FEE,MAX_FEE,CCYCD FROM IBPS_FEE A WHERE TRANS_TYPE ='" +
                pTRANS_TYPE + "' ORDER BY A.CCYCD, A.FEEDISC_TYPE";
            // AND FEEDISC_TYPE = '" + pFEEDISC_TYPE + "'
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        //************************************************
        //Muc dich: Check loai tien nay da nhap bieu phi?
        //
        //************************************************
        public DataSet CheckCCYCD(string pCCYCD, long pID,
            string pTranType, string pDiscountType)
        {
            oraConn = connect.Connect();
            string strSQL;

            if (oraConn == null)
            {
                return null;
            }
            if (pID == 0)
                strSQL = "select * FROM IBPS_FEE WHERE TRANS_TYPE= '" + pTranType + 
                    "' AND FEEDISC_TYPE='" + pDiscountType + "' AND CCYCD='" + 
                    pCCYCD + "'";
            else
                strSQL = "select * FROM IBPS_FEE WHERE TRANS_TYPE= '" + pTranType + 
                    "' AND FEEDISC_TYPE='" + pDiscountType + "' AND CCYCD='" + 
                    pCCYCD + "' AND ID <> " + pID;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
        }

        public DataTable DeleteIBPS_FEE(int pID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "DELETE IBPS_FEE WHERE ID = " + pID;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }


        public int AddIBPS_FEE(IBPS_FEEInfo objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "INSERT INTO IBPS_FEE(TRANS_TYPE,FEEDISC_TYPE,FEEDISC_TIME," +
            "FIXED_FEE,RATE_FEE,MIN_FEE,MAX_FEE,CCYCD) VALUES(:pTRANS_TYPE,:pFEEDISC_TYPE," +
            ":pFEEDISC_TIME,:pFIXED_FEE,:pRATE_FEE,:pMIN_FEE,:pMAX_FEE,:pCCYCD) ";
            OracleParameter[] Orapara = {new OracleParameter("pTRANS_TYPE",OracleType.VarChar,20),
                                        new OracleParameter("pFEEDISC_TYPE",OracleType.VarChar,20),
                                        new OracleParameter("pFEEDISC_TIME",OracleType.DateTime,7),
                                        new OracleParameter("pFIXED_FEE",OracleType.Number,20),
                                        new OracleParameter("pRATE_FEE",OracleType.Number,20),
                                        new OracleParameter("pMIN_FEE",OracleType.Number,20),
                                        new OracleParameter("pMAX_FEE",OracleType.Number,20),
                                        new OracleParameter("pCCYCD",OracleType.Char,5)
                                        };

            Orapara[0].Value = objTable.TRANS_TYPE;
            Orapara[1].Value = objTable.FEEDISC_TYPE;
            Orapara[2].Value = objTable.FEEDISC_TIME;
            Orapara[3].Value = objTable.FIXED_FEE;
            Orapara[4].Value = objTable.RATE_FEE;
            Orapara[5].Value = objTable.MIN_FEE;
            Orapara[6].Value = objTable.MAX_FEE;
            Orapara[7].Value = objTable.CCYCD;

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
            }
            catch 
            {
                return -1;
            }
        }

        public int UpdateIBPS_FEE(IBPS_FEEInfo objTable, int iFeeType)
        {
            try
            {
                string strSQL = "";
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                //Neu la dieu chuyen von
                if (objTable.TRANS_TYPE == "3")
                {
                    //Neu la phi co dinh
                    if (iFeeType == 1)
                    {
                        strSQL = "UPDATE IBPS_FEE SET FIXED_FEE=:pFIXED_FEE,CCYCD=:pCCYCD WHERE ID =:pID";
                        OracleParameter[] Orapara = {new OracleParameter("pFIXED_FEE",OracleType.Number,20),
                                            new OracleParameter("pID",OracleType.Number,20),
                                            new OracleParameter("pCCYCD",OracleType.Char,3)
                                            };
                        Orapara[0].Value = objTable.FIXED_FEE;
                        Orapara[1].Value = objTable.ID;
                        Orapara[2].Value = objTable.CCYCD;
                        return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                    }
                    //Phi theo ty le
                    else
                    {
                        strSQL = "UPDATE IBPS_FEE SET RATE_FEE=:pRATE_FEE,MIN_FEE=:pMIN_FEE," +
                                 "MAX_FEE=:pMAX_FEE, CCYCD =:pCCYCD WHERE ID =:pID";
                        OracleParameter[] Orapara = {new OracleParameter("pRATE_FEE",OracleType.Number,20),
                                                     new OracleParameter("pMIN_FEE",OracleType.Number,20),
                                                     new OracleParameter("pMAX_FEE",OracleType.Number,20),
                                                     new OracleParameter("pID",OracleType.Number,20),
                                                     new OracleParameter("pCCYCD",OracleType.Char,3)
                                            };
                        Orapara[0].Value = objTable.RATE_FEE;
                        Orapara[1].Value = objTable.MIN_FEE;
                        Orapara[2].Value = objTable.MAX_FEE;
                        Orapara[3].Value = objTable.ID;
                        Orapara[4].Value = objTable.CCYCD;
                        return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                    }
                }
                //Neu la HV, LV
                else
                {
                    //Loai giam phi = 0
                    if (objTable.FEEDISC_TYPE == "0")
                    {
                        //Phi co dinh
                        if (iFeeType == 1)
                        {
                            strSQL = "UPDATE IBPS_FEE SET FIXED_FEE=:pFIXED_FEE,CCYCD=:pCCYCD,"+
                                "FEEDISC_TYPE=:pFEEDISC_TYPE WHERE ID =:pID";
                            OracleParameter[] Orapara = {new OracleParameter("pFIXED_FEE",OracleType.Number,20),
                                            new OracleParameter("pFEEDISC_TYPE",OracleType.VarChar,20),
                                            new OracleParameter("pID",OracleType.Number,20),
                                            new OracleParameter("pCCYCD",OracleType.Char,3)
                                            };
                            Orapara[0].Value = objTable.FIXED_FEE;
                            Orapara[1].Value = objTable.FEEDISC_TYPE;
                            Orapara[2].Value = objTable.ID;
                            Orapara[3].Value = objTable.CCYCD;
                            return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                        }
                        //Phi ty le
                        else
                        {
                            strSQL = "UPDATE IBPS_FEE SET RATE_FEE=:pRATE_FEE,MIN_FEE=:pMIN_FEE," +
                                 "MAX_FEE=:pMAX_FEE,FEEDISC_TYPE=:pFEEDISC_TYPE,CCYCD=:pCCYCD " +
                                 "WHERE ID =:pID";
                            OracleParameter[] Orapara = {new OracleParameter("pRATE_FEE",OracleType.Number,20),
                                                     new OracleParameter("pMIN_FEE",OracleType.Number,20),
                                                     new OracleParameter("pMAX_FEE",OracleType.Number,20),
                                                     new OracleParameter("pID",OracleType.Number,20),
                                                     new OracleParameter("pFEEDISC_TYPE",OracleType.VarChar,20),
                                                     new OracleParameter("pCCYCD",OracleType.Char,3)
                                            };
                            Orapara[0].Value = objTable.RATE_FEE;
                            Orapara[1].Value = objTable.MIN_FEE;
                            Orapara[2].Value = objTable.MAX_FEE;
                            Orapara[3].Value = objTable.ID;
                            Orapara[4].Value = objTable.FEEDISC_TYPE;
                            Orapara[5].Value = objTable.CCYCD;
                            return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                        }
                    }
                    //Truoc gio giam phi hoac sau gio giam phi
                    else
                    { 
                        //Phi co dinh
                        if (iFeeType == 1)
                        {
                            strSQL = "UPDATE IBPS_FEE SET FIXED_FEE=:pFIXED_FEE,CCYCD=:pCCYCD," +
                                "FEEDISC_TYPE=:pFEEDISC_TYPE,FEEDISC_TIME=:pFEEDISC_TIME WHERE ID =:pID";
                            OracleParameter[] Orapara = {new OracleParameter("pFIXED_FEE",OracleType.Number,20),
                                            new OracleParameter("pFEEDISC_TYPE",OracleType.VarChar,20),
                                            new OracleParameter("pID",OracleType.Number,20),
                                            new OracleParameter("pCCYCD",OracleType.Char,3),
                                            new OracleParameter("pFEEDISC_TIME",OracleType.DateTime,20)
                                            };
                            Orapara[0].Value = objTable.FIXED_FEE;
                            Orapara[1].Value = objTable.FEEDISC_TYPE;
                            Orapara[2].Value = objTable.ID;
                            Orapara[3].Value = objTable.CCYCD;
                            Orapara[4].Value = objTable.FEEDISC_TIME;
                            return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                        }
                        //Phi ty le
                        else
                        {
                            strSQL = "UPDATE IBPS_FEE SET RATE_FEE=:pRATE_FEE,MIN_FEE=:pMIN_FEE," +
                                 "MAX_FEE=:pMAX_FEE,FEEDISC_TYPE=:pFEEDISC_TYPE," +
                                 "FEEDISC_TIME=:pFEEDISC_TIME,CCYCD=:pCCYCD WHERE ID =:pID";
                            OracleParameter[] Orapara = {new OracleParameter("pRATE_FEE",OracleType.Number,20),
                                                     new OracleParameter("pMIN_FEE",OracleType.Number,20),
                                                     new OracleParameter("pMAX_FEE",OracleType.Number,20),
                                                     new OracleParameter("pID",OracleType.Number,20),
                                                     new OracleParameter("pFEEDISC_TYPE",OracleType.VarChar,20),
                                                     new OracleParameter("pFEEDISC_TIME",OracleType.DateTime,20),
                                                     new OracleParameter("pCCYCD",OracleType.Char,3)
                                            };
                            Orapara[0].Value = objTable.RATE_FEE;
                            Orapara[1].Value = objTable.MIN_FEE;
                            Orapara[2].Value = objTable.MAX_FEE;
                            Orapara[3].Value = objTable.ID;
                            Orapara[4].Value = objTable.FEEDISC_TYPE;
                            Orapara[5].Value = objTable.FEEDISC_TIME;
                            Orapara[6].Value = objTable.CCYCD;
                            return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                        }
                    }
                }
            }
            catch 
            {
                return -1;
            }
        }
        

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
        public DataTable CAL_FEE(DateTime fromdate, DateTime todate,
            int pBranchType, int pFeeType, string pBranch, string pBranch8, string pCCYCD)
        {
            try
            {

                string strSQL = "BK02";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),                                              
                                              new OracleParameter("pBranchType", OracleType.Int16,2),
                                              new OracleParameter("pFeeType", OracleType.Int16,2),
                                              new OracleParameter("pBranch", OracleType.VarChar,12),
                                              new OracleParameter("pBranch8", OracleType.VarChar,12),                                              
                                              new OracleParameter("pCCYCD", OracleType.VarChar,3),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranchType;
                oraPara[3].Value = pFeeType;
                oraPara[4].Value = pBranch;
                oraPara[5].Value = pBranch8;
                oraPara[6].Value = pCCYCD;
                oraPara[7].Direction = ParameterDirection.Output;

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

        public DataTable CAL_FEE_DETAIL(DateTime fromdate, DateTime todate,
            int pBranchType, int pFeeType, string pBranch, string pBranch8, string pCCYCD)
        {
            try
            {

                string strSQL = "BK02_DETAIL";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),                                              
                                              new OracleParameter("pBranchType", OracleType.Int16,2),
                                              new OracleParameter("pFeeType", OracleType.Int16,2),
                                              new OracleParameter("pBranch", OracleType.VarChar,12),
                                              new OracleParameter("pBranch8", OracleType.VarChar,12),                                              
                                              new OracleParameter("pCCYCD", OracleType.VarChar,3),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranchType;
                oraPara[3].Value = pFeeType;
                oraPara[4].Value = pBranch;
                oraPara[5].Value = pBranch8;
                oraPara[6].Value = pCCYCD;
                oraPara[7].Direction = ParameterDirection.Output;

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

        public DataTable CAL_FEE_DETAIL_EXCEL(DateTime fromdate, DateTime todate,
            int pBranchType, int pFeeType, string pBranch, string pBranch8, string pCCYCD)
        {
            try
            {

                string strSQL = "BK02_DETAIL_EXCEL";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),                                              
                                              new OracleParameter("pBranchType", OracleType.Int16,2),
                                              new OracleParameter("pFeeType", OracleType.Int16,2),
                                              new OracleParameter("pBranch", OracleType.VarChar,12),
                                              new OracleParameter("pBranch8", OracleType.VarChar,12),                                              
                                              new OracleParameter("pCCYCD", OracleType.VarChar,3),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranchType;
                oraPara[3].Value = pFeeType;
                oraPara[4].Value = pBranch;
                oraPara[5].Value = pBranch8;
                oraPara[6].Value = pCCYCD;
                oraPara[7].Direction = ParameterDirection.Output;

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
