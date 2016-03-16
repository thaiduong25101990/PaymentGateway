using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using BR.DataAccess;
using System.Data;
namespace BR.BRBusinessObject
{
    public class IQS_MSG_CONTENTDP
    {
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();
        string strSQL;
        public IQS_MSG_CONTENTDP()
        {
        }
        public static IQS_MSG_CONTENTDP Instance()
        {
            return new IQS_MSG_CONTENTDP();
        }

        public int AddIQS_MSG_CONTENT(IQS_MSG_CONTENTInfo objTable)
      {                                                 //,ORG_TRANS_DATE
            //string strSql = "Insert into IQS_MSG_CONTENT (QUERY_ID,FROMBANK,TOBANK,INTERFACE,DATECREATE,RM_ACCOUNT,STATUS,MSGCONTENT,GWOPTION,MSG_DIRECTION,MSG_TYPE) values (:pQUERY_ID,:pFROMBANK,:pTOBANK,:pINTERFACE,:pDATECREATE,:pRM_ACCOUNT,:pSTATUS,:pMSGCONTENT,:pGWOPTION,:pMSG_DIRECTION,:pMSG_TYPE)";
          string strSql = "Insert into IQS_MSG_CONTENT (QUERY_ID,FROMBANK,TOBANK,INTERFACE,DATECREATE,STATUS,MSGCONTENT,GWOPTION,MSG_DIRECTION,MSG_TYPE,ORG_RM_NUMBER,TELLER_ID,ORG_TRANS_DATE,REF_NUMBER,PRODUCT_CODE,AMOUNT,CCYCD) values (:pQUERY_ID,:pFROMBANK,:pTOBANK,:pINTERFACE,:pDATECREATE,:pSTATUS,:pMSGCONTENT,:pGWOPTION,:pMSG_DIRECTION,:pMSG_TYPE,:pORG_RM_NUMBER,:pTELLER_ID,:pORG_TRANS_DATE,:pREF_NUMBER,:pPRODUCT_CODE,:pAMOUNT,:pCCYCD)";
            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.Number, 10),
                                        //
                                         new OracleParameter("pFROMBANK", OracleType.VarChar,8),
                                         new OracleParameter("pTOBANK", OracleType.VarChar, 8),
                                         new OracleParameter("pINTERFACE", OracleType.VarChar, 10),
                                         new OracleParameter("pDATECREATE", OracleType.DateTime),
                                         //new OracleParameter("pREF_NUMBER", OracleType.VarChar, 20),
                                         new OracleParameter("pSTATUS", OracleType.Number, 1),
                                         new OracleParameter("pMSGCONTENT", OracleType.VarChar, 2000),
                                         new OracleParameter("pGWOPTION", OracleType.VarChar, 1000),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.VarChar, 10),
                                         new OracleParameter("pMSG_TYPE", OracleType.VarChar, 8),
                                         new OracleParameter("pORG_RM_NUMBER", OracleType.VarChar, 20),
                                         new OracleParameter("pTELLER_ID", OracleType.VarChar, 10),
                                         new OracleParameter("pORG_TRANS_DATE", OracleType.DateTime),
                                         new OracleParameter("pREF_NUMBER", OracleType.VarChar,20),
                                         new OracleParameter("pPRODUCT_CODE", OracleType.VarChar,3),
                                         new OracleParameter("pAMOUNT", OracleType.Number),
                                         new OracleParameter("pCCYCD", OracleType.VarChar,3)};
            oraParam[0].Value = objTable.QUERY_ID;
            oraParam[1].Value = objTable.FROMBANK;
            oraParam[2].Value = objTable.TOBANK;
            oraParam[3].Value = objTable.INTERFACE;
            oraParam[4].Value = objTable.DATECREATE;
            oraParam[5].Value = 0;
            oraParam[6].Value = objTable.MSGCONTENT;
            oraParam[7].Value = objTable.GWOPTION;
            oraParam[8].Value = "BR-IQS";
            oraParam[9].Value = objTable.MSG_TYPE;
            oraParam[10].Value = objTable.ORG_RM_NUMBER;
            oraParam[11].Value = objTable.TELLER_ID;
            oraParam[12].Value = objTable.ORG_TRANS_DATE;
            oraParam[13].Value = objTable.REF_NUMBER;
            oraParam[14].Value = objTable.PRODUCT_CODE;
            oraParam[15].Value = objTable.AMOUNT;
            oraParam[16].Value = objTable.CCYCD;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1; ;
            }
        }

        public int AddIQS_MSG_CONTENT_TTSP_VCB(IQS_MSG_CONTENTInfo objTable)
        {
            string strSql = "Insert into IQS_MSG_CONTENT (QUERY_ID,REF_NUMBER,FROMBANK,TOBANK,INTERFACE,DATECREATE,STATUS,MSGCONTENT,GWOPTION,MSG_DIRECTION,MSG_TYPE,ORG_RM_NUMBER,TELLER_ID,ORG_TRANS_DATE) values (:pQUERY_ID,:pREF_NUMBER,:pFROMBANK,:pTOBANK,:pINTERFACE,:pDATECREATE,:pSTATUS,:pMSGCONTENT,:pGWOPTION,:pMSG_DIRECTION,:pMSG_TYPE,:pORG_RM_NUMBER,:pTELLER_ID,:pORG_TRANS_DATE)";
            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.Number, 10),
                                         new OracleParameter("pREF_NUMBER", OracleType.VarChar,20),
                                         new OracleParameter("pFROMBANK", OracleType.VarChar,8),
                                         new OracleParameter("pTOBANK", OracleType.VarChar, 8),
                                         new OracleParameter("pINTERFACE", OracleType.VarChar, 10),
                                         new OracleParameter("pDATECREATE", OracleType.DateTime),
                                         //new OracleParameter("pRM_ACCOUNT", OracleType.VarChar, 20),
                                         new OracleParameter("pSTATUS", OracleType.Number, 1),
                                         new OracleParameter("pMSGCONTENT", OracleType.VarChar, 2000),
                                         new OracleParameter("pGWOPTION", OracleType.VarChar, 1000),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.VarChar, 10),
                                         new OracleParameter("pMSG_TYPE", OracleType.VarChar, 8)};
            oraParam[0].Value = objTable.QUERY_ID;
            oraParam[1].Value = objTable.REF_NUMBER;
            //oraParam[2].Value = objTable.FROMBANK;
            //oraParam[3].Value = objTable.TOBANK;
            oraParam[2].Value = "00990";
            oraParam[3].Value = objTable.FROMBANK;
            oraParam[4].Value = objTable.INTERFACE;
            oraParam[5].Value = objTable.DATECREATE;
            //oraParam[5].Value = objTable.RM_ACCOUNT;
            oraParam[6].Value = 0; //objTable.STATUS;
            oraParam[7].Value = objTable.MSGCONTENT;
            oraParam[8].Value = objTable.GWOPTION;
            oraParam[9].Value = "BR-IQS";// objTable.MSG_DIRECTION;
            oraParam[10].Value = objTable.MSG_TYPE;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1; ;
            }
        }

        // Update bang IQS_MSG_CONTENT trong kenh IBPS trong form IQSNew1
        public int UpdateIQS_MSG_CONTENT_IBPS_New1(IQS_MSG_CONTENTInfo objTable)
        {
            DataTable datTable = new DataTable();
            //string strSQL = "Update IQS_MSG_CONTENT set GWOPTION=:pGWOPTION where QUERY_ID=:pQUERY_ID ";
            string strSQL = "Insert into IQS_MSG_CONTENT (QUERY_ID,FROMBANK,TOBANK,STATUS,GWOPTION,MSG_DIRECTION) values (:pQUERY_ID,:pFROMBANK,:pTOBANK,:pSTATUS,:pGWOPTION,:pMSG_DIRECTION)";
            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.Number,10),
                                         new OracleParameter("pFROMBANK", OracleType.VarChar, 8),
                                         new OracleParameter("pTOBANK", OracleType.VarChar, 8),
                                         new OracleParameter("pSTATUS", OracleType.Number,1),
                                         new OracleParameter("pGWOPTION", OracleType.VarChar, 1000),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.VarChar, 10)};
            oraParam[0].Value = objTable.QUERY_ID;
            oraParam[1].Value=objTable.FROMBANK;
            oraParam[2].Value = objTable.TOBANK;
            oraParam[3].Value = 0;
            oraParam[4].Value = objTable.GWOPTION;
            oraParam[5].Value = "BR-IQS";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }


        public int UpdateIQS_MSG_CONTENT_IBPS(IQS_MSG_CONTENTInfo objTable)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update IQS_MSG_CONTENT set MSG_ID =:pMSG_ID, F20 = :pF20,FROMBANK =:pFROMBANK, ";
            strSQL = strSQL + "TOBANK = :pTOBANK,INTERFACE = :pINTERFACE,DATECREATE=:pDATECREATE,RM_ACCOUNT=:pRM_ACCOUNT,STATUS=:pSTATUS,MSGCONTENT=:pMSGCONTENT,GWOPTION=:pGWOPTION,MSG_TYPE = :pMSG_TYPE where MSG_ID =:pMSG_ID";
            OracleParameter[] oraParam = {new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pF20", OracleType.NVarChar,20),
                                         new OracleParameter("pFROMBANK", OracleType.NVarChar,8),
                                         new OracleParameter("pTOBANK", OracleType.NVarChar, 8),
                                         new OracleParameter("pINTERFACE", OracleType.NVarChar, 10),
                                         new OracleParameter("pDATECREATE", OracleType.DateTime),
                                         new OracleParameter("pRM_ACCOUNT", OracleType.NVarChar, 20),
                                         new OracleParameter("pSTATUS", OracleType.Number),
                                         new OracleParameter("pMSGCONTENT", OracleType.NVarChar, 2000),
                                         new OracleParameter("pGWOPTION", OracleType.NVarChar, 1000),
                                         new OracleParameter("pMSG_TYPE", OracleType.VarChar, 8)};
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.F20;
            oraParam[2].Value = objTable.FROMBANK;
            oraParam[3].Value = objTable.TOBANK;
            oraParam[4].Value = objTable.INTERFACE;
            oraParam[5].Value = objTable.DATECREATE;
            oraParam[6].Value = objTable.REF_NUMBER;
            oraParam[7].Value = objTable.STATUS;
            oraParam[8].Value = objTable.MSGCONTENT;
            oraParam[9].Value = objTable.GWOPTION;
            oraParam[10].Value = objTable.MSG_TYPE;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public DataSet GetIQS_MSG_CONTENT(string Interface, int QueryID)
        {
            DataSet datDs = new DataSet();
            string strSQL = "select imc.MSG_ID,imc.F20,imc.FROMBANK,imc.TOBANK,imc.INTERFACE,imc.DATECREATE,imc.RM_ACCOUNT,imc.STATUS,imc.MSGCONTENT,imc.GWOPTION  from IQS_MSG_CONTENT imc where imc.INTERFACE='" + Interface + "' and imc.QUERY_ID= " + QueryID;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet GetIQS_MSG_CONTENT(string Interface, List<int> QueryIDList)
        {
            DataSet datDs = new DataSet();
            DataTable dtb = new DataTable();
            DataColumn[] dataCols = new DataColumn[]
           {
               //new DataColumn("MSG_ID"),
               new DataColumn("QUERY_ID"),
               new DataColumn("F20"),
               new DataColumn("FROMBANK"),
               new DataColumn("TOBANK"),
               new DataColumn("INTERFACE"),
               new DataColumn("DATECREATE"),
               new DataColumn("ORG_RM_NUMBER"),
               new DataColumn("STATUS"),
               new DataColumn("MSGCONTENT"),
               new DataColumn("GWOPTION"),
               new DataColumn("TELLER_ID"),
               new DataColumn("ORG_TRANS_DATE"),
               new DataColumn("MSG_TYPE")
           };
            dtb.Columns.AddRange(dataCols);
            try
            {
                string strCondition;
                strCondition = "";
                //HaNTT10 sua ngay 10.09.2008
                for (int i = 0; i < QueryIDList.Count-1; i++)
                {
                    strCondition = strCondition + QueryIDList[i] + ",";
                }
                strCondition = strCondition + QueryIDList[QueryIDList.Count - 1];
                strCondition = "(" + strCondition + ")";
                oraConn = conn.Connect();
                if (Interface == "IBPS")
                {
                    //strSQL = "select IMC.MSG_ID,IMC.QUERY_ID,IMC.FILE_NAME,IMC.MSG_DIRECTION,IMC.TRANS_CODE,IMC.GW_TRANS_NUM,IMC.SIBS_TRANS_NUM,IMC.SENDER,IMC.RECEIVER,IMC.TRANS_DATE,IMC.AMOUNT,IMC.CCYCD,IMC.STATUS,IMC.ERR_CODE,IMC.TRANS_DESCRIPTION,IMC.DEPARTMENT,IMC.CONTENT,IMC.SOURCE_BRANCH,IMC.TAD,IMC.PRE_TAD,IMC.RM_NUMBER,IMC.TELLERID from IBPS_MSG_CONTENT IMC ";
                    //strSQL = strSQL + " where trim(imc.QUERY_ID) in " + strCondition;

                    //strSQL = "select MSG_ID,QUERY_ID,ltrim(RM_NUMBER,'0000') as RM_NUMBER,SENDER,RECEIVER,AMOUNT,CCYCD,(select a.content from Allcode a where trim(a.cdname)='STATUS'  and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS,TRANS_DATE,TRANS_CODE,FILE_NAME, MSG_DIRECTION,GW_TRANS_NUM,SIBS_TRANS_NUM";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,TRANS_DESCRIPTION,DEPARTMENT,ltrim(SOURCE_BRANCH,'00') as SOURCE_BRANCH,ltrim(TAD,'00') as TAD,  PRE_TAD,TELLERID,PRETRAN_CODE,PRETRANS_NUM,(select a.content from Allcode a where trim(a.cdname)='FWSTS' and trim(a.cdval)= trim(FWSTS)) as FORWARDSTS,FWSTS,FWTIME,'HaNTT10' as TELLER_NAME,RECEIVING_TIME,SENDING_TIME from IBPS_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,ltrim(RM_NUMBER,'0000') as RM_NUMBER,SENDER,RECEIVER,AMOUNT,CCYCD,(select a.content from Allcode a where trim(a.cdname)='STATUS'  and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS,TRANS_DATE,TRANS_CODE,FILE_NAME, MSG_DIRECTION,GW_TRANS_NUM,SIBS_TRANS_NUM ";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,TRANS_DESCRIPTION,DEPARTMENT,ltrim(SOURCE_BRANCH,'00') as SOURCE_BRANCH,ltrim(TAD,'00') as TAD,  PRE_TAD,TELLERID,PRETRAN_CODE,PRETRANS_NUM,(select a.content from Allcode a where trim(a.cdname)='FWSTS' and trim(a.cdval)= trim(FWSTS)) as FORWARDSTS,FWSTS,FWTIME,'HaNTT10' as TELLER_NAME,RECEIVING_TIME,SENDING_TIME from IBPS_MSG_all  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,ltrim(RM_NUMBER,'0000') as RM_NUMBER,SENDER,RECEIVER,AMOUNT,CCYCD,(select a.content from Allcode a where trim(a.cdname)='STATUS'  and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS,TRANS_DATE,TRANS_CODE,FILE_NAME, MSG_DIRECTION,GW_TRANS_NUM,SIBS_TRANS_NUM ";
                    //strSQL = strSQL + " ,(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,TRANS_DESCRIPTION,DEPARTMENT,ltrim(SOURCE_BRANCH,'00') as SOURCE_BRANCH,ltrim(TAD,'00') as TAD,  PRE_TAD,TELLERID,PRETRAN_CODE,PRETRANS_NUM,(select a.content from Allcode a where trim(a.cdname)='FWSTS' and trim(a.cdval)= trim(FWSTS)) as FORWARDSTS,FWSTS,FWTIME,'HaNTT10' as TELLER_NAME,RECEIVING_TIME,SENDING_TIME from Ibps_Msg_All_His  where   trim(QUERY_ID) in " + strCondition;

                    //strSQL = "select MSG_ID,QUERY_ID,'990' as BRANCH_A,decode(msg_direction,'SIBS-IBPS',substr(SENDER,-3),'IBPS-SIBS',SENDER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLERID,CONTENT";
                    //strSQL = strSQL + " from IBPS_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,'990' as BRANCH_A,decode(msg_direction,'SIBS-IBPS',substr(SENDER,-3),'IBPS-SIBS',SENDER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLERID,CONTENT";
                    //strSQL = strSQL + " from IBPS_MSG_all  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,'990' as BRANCH_A,decode(msg_direction,'SIBS-IBPS',substr(SENDER,-3),'IBPS-SIBS',SENDER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLERID,CONTENT";
                    //strSQL = strSQL + " from Ibps_Msg_All_His  where   trim(QUERY_ID) in " + strCondition;

                    strSQL = "select MSG_ID,QUERY_ID,'990' as BRANCH_A,substr(trim(rm_number),5,3) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLERID,CONTENT";
                    strSQL = strSQL + " from IBPS_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    strSQL = strSQL + "select MSG_ID,QUERY_ID,'990' as BRANCH_A,substr(trim(rm_number),5,3) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLERID,CONTENT";
                    strSQL = strSQL + " from IBPS_MSG_all  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    strSQL = strSQL + "select MSG_ID,QUERY_ID,'990' as BRANCH_A,substr(trim(rm_number),5,3) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLERID,CONTENT";
                    strSQL = strSQL + " from Ibps_Msg_All_His  where   trim(QUERY_ID) in " + strCondition;
                }
                else if (Interface == "VCB")
                {
                    //strSQL = "select VMC.MSG_ID,VMC.QUERY_ID,VMC.MSG_TYPE,VMC.MSG_DIRECTION,VMC.BRANCH_A,VMC.BRANCH_B,VMC.TRANS_DATE,VMC.VALUE_DATE,VMC.FIELD20,VMC.FIELD21,VMC.AMOUNT,VMC.CCYCD,VMC.STATUS,VMC.ERR_CODE,VMC.DEPARTMENT,VMC.Header_Content,VMC.CONTENT,VMC.FILE_NAME,VMC.PRIORITY,VMC.FOREIGN_BANK,VMC.FOREIGN_BANK_NAME,VMC.TRANS_NO,VMC.RM_NUMBER from VCB_MSG_CONTENT VMC ";
                    //strSQL = strSQL + " where trim(VMC.QUERY_ID) in " + strCondition;

                    //strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,MSG_DIRECTION,decode(msg_direction,'SIBS-VCB',substr(branch_a,-3),'VCB-SIBS',branch_a) as BRANCH_A,decode(msg_direction,'VCB-SIBS',substr(BRANCH_B,-3),'SIBS-VCB',BRANCH_B) as BRANCH_B,AMOUNT,CCYCD,ltrim(RM_NUMBER,'0000')as RM_NUMBER,FIELD20,TRANS_DATE,VALUE_DATE,FIELD21";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='STATUS' and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS,(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,DEPARTMENT,Header_Content,  FILE_NAME,PRIORITY,FOREIGN_BANK,FOREIGN_BANK_NAME,TRANS_NO,'HaNTT10' as TELLER_NAME from VCB_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,MSG_DIRECTION,decode(msg_direction,'SIBS-VCB',substr(branch_a,-3),'VCB-SIBS',branch_a) as BRANCH_A,decode(msg_direction,'VCB-SIBS',substr(BRANCH_B,-3),'SIBS-VCB',BRANCH_B) as BRANCH_B,AMOUNT,CCYCD,ltrim(RM_NUMBER,'0000')as RM_NUMBER,FIELD20,TRANS_DATE,VALUE_DATE,FIELD21 ";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='STATUS' and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS,(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,DEPARTMENT,Header_Content, FILE_NAME,PRIORITY,FOREIGN_BANK,FOREIGN_BANK_NAME,TRANS_NO,'HaNTT10' as TELLER_NAME from VCB_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,MSG_DIRECTION,decode(msg_direction,'SIBS-VCB',substr(branch_a,-3),'VCB-SIBS',branch_a) as BRANCH_A,decode(msg_direction,'VCB-SIBS',substr(BRANCH_B,-3),'SIBS-VCB',BRANCH_B) as BRANCH_B,AMOUNT,CCYCD,ltrim(RM_NUMBER,'0000')as RM_NUMBER,FIELD20,TRANS_DATE,VALUE_DATE,FIELD21 ";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='STATUS' and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS,(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,DEPARTMENT,Header_Content,  FILE_NAME,PRIORITY,FOREIGN_BANK,FOREIGN_BANK_NAME,TRANS_NO,'HaNTT10' as TELLER_NAME from VCB_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;
                    //string strSQL = "";


                    //strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(msg_direction,'SIBS-VCB',substr(BRANCH_A,-3),'VCB-SIBS',BRANCH_A) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE,MSG_DIRECTION from VCB_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";
                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(msg_direction,'SIBS-VCB',substr(BRANCH_A,-3),'VCB-SIBS',BRANCH_A) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE,MSG_DIRECTION from VCB_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";
                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(msg_direction,'SIBS-VCB',substr(BRANCH_A,-3),'VCB-SIBS',BRANCH_A) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE,MSG_DIRECTION from VCB_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;
                    //HaNTT10 ngay 26/09/2008
                    strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,decode(msg_direction,'SIBS-VCB',substr(BRANCH_A,-3),'VCB-SIBS',BRANCH_A) as BRANCH_A,decode(msg_direction,'VCB-SIBS',substr(BRANCH_B,-3),'SIBS-VCB',BRANCH_B) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    strSQL = strSQL + ",FIELD20,VALUE_DATE,MSG_DIRECTION from VCB_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,decode(msg_direction,'SIBS-VCB',substr(BRANCH_A,-3),'VCB-SIBS',BRANCH_A) as BRANCH_A,decode(msg_direction,'VCB-SIBS',substr(BRANCH_B,-3),'SIBS-VCB',BRANCH_B) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    strSQL = strSQL + ",FIELD20,VALUE_DATE,MSG_DIRECTION from VCB_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,decode(msg_direction,'SIBS-VCB',substr(BRANCH_A,-3),'VCB-SIBS',BRANCH_A) as BRANCH_A,decode(msg_direction,'VCB-SIBS',substr(BRANCH_B,-3),'SIBS-VCB',BRANCH_B) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    strSQL = strSQL + ",FIELD20,VALUE_DATE,MSG_DIRECTION from VCB_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;
                }
                else if (Interface == "TTSP")
                {
                    //strSQL = "select TMC.MSG_ID,TMC.QUERY_ID,TMC.MSG_TYPE,TMC.MSG_DIRECTION,TMC.SENDER,TMC.RECEIVER,TMC.TRANS_DATE,TMC.VALUE_DATE,TMC.FIELD20,TMC.FIELD21,TMC.AMOUNT,TMC.CCYCD,TMC.STATUS,TMC.ERR_CODE,TMC.DEPARTMENT,TMC.HEAD_CONTENT,TMC.CONTENT,TMC.RM_NUMBER from TTSP_MSG_CONTENT TMC ";
                    //strSQL = strSQL + " where trim(TMC.QUERY_ID) in " + strCondition;

                    //strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,Ltrim(RM_NUMBER,'0000') as RM_NUMBER,FIELD20,SENDER,RECEIVER,AMOUNT,CCYCD,TRANS_DATE,VALUE_DATE,(select a.content from Allcode a where trim(a.cdname)='STATUS' and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,MSG_DIRECTION,FIELD21,DEPARTMENT,HEAD_CONTENT,'HaNTT10'  as TELLER_NAME from TTSP_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,Ltrim(RM_NUMBER,'0000') as RM_NUMBER,FIELD20,SENDER,RECEIVER,AMOUNT,CCYCD,TRANS_DATE,VALUE_DATE,(select a.content from Allcode a where trim(a.cdname)='STATUS' and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,MSG_DIRECTION,FIELD21,DEPARTMENT,HEAD_CONTENT,'HaNTT10'  as TELLER_NAME from TTSP_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,Ltrim(RM_NUMBER,'0000') as RM_NUMBER,FIELD20,SENDER,RECEIVER,AMOUNT,CCYCD,TRANS_DATE,VALUE_DATE,(select a.content from Allcode a where trim(a.cdname)='STATUS' and  trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(STATUS)) as STATUS ";
                    //strSQL = strSQL + ",(select a.content from Allcode a where trim(a.cdname)='ERROR_CODE' and trim(a.cdval)= trim(ERR_CODE)) as ERR_CODE,MSG_DIRECTION,FIELD21,DEPARTMENT,HEAD_CONTENT,'HaNTT10'  as TELLER_NAME from TTSP_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;

                    //strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(msg_direction,'TTSP-SIBS',substr(SENDER,-3),'SIBS-TTSP',SENDER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(msg_direction,'TTSP-SIBS',substr(SENDER,-3),'SIBS-TTSP',SENDER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(msg_direction,'TTSP-SIBS',substr(SENDER,-3),'SIBS-TTSP',SENDER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;

                    //strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,substr(trim(rm_number),5,3) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";
                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,substr(trim(rm_number),5,3) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";
                    //strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,substr(trim(rm_number),5,3) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    //strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;

                    //HaNTT10 sua ngay 01.10.2008
                    strSQL = "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(MSG_DIRECTION,'SIBS-TTSP',SENDER,'TTSP-SIBS',RECEIVER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_CONTENT  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(MSG_DIRECTION,'SIBS-TTSP',SENDER,'TTSP-SIBS',RECEIVER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_ALL  where   trim(QUERY_ID) in " + strCondition + " union all ";

                    strSQL = strSQL + "select MSG_ID,QUERY_ID,MSG_TYPE,'990' as BRANCH_A,decode(MSG_DIRECTION,'SIBS-TTSP',SENDER,'TTSP-SIBS',RECEIVER) as BRANCH_B,ltrim(RM_NUMBER,'0000')as RM_NUMBER,AMOUNT,CCYCD,TRANS_DATE,'" + DateTime.Now + "' as DATE_CREATE,'" + BR.BRLib.Common.Userid + "' as TELLER_ID,CONTENT";
                    strSQL = strSQL + ",FIELD20,VALUE_DATE from TTSP_MSG_ALL_HIS where   trim(QUERY_ID) in " + strCondition;
                }
                datDs = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                //OracleDataReader dtReader = DataAcess.ExecuteReader(oraConn, CommandType.Text, strSQL);



                //foreach (int QueryID in QueryIDList)
                //{
                //    oraConn = conn.Connect();
                //    string strSQL = "select imc.QUERY_ID,imc.F20,imc.FROMBANK,imc.TOBANK,imc.INTERFACE,imc.DATECREATE,imc.RM_ACCOUNT,imc.STATUS,imc.MSGCONTENT,imc.GWOPTION  from IQS_MSG_CONTENT imc where imc.INTERFACE='" + Interface + "' and imc.QUERY_ID= " + QueryID;
                //    OracleDataReader dtReader = DataAcess.ExecuteReader(oraConn, CommandType.Text, strSQL);
                //    while (dtReader.Read())
                //    {
                //        DataRow dtR = dtb.NewRow();
                //        //IQS_MSG_CONTENTInfo objTable = new IQS_MSG_CONTENTInfo();
                //        dtR[0] = Convert.ToInt32(dtReader["QUERY_ID"]);
                //        dtR[1] = dtReader["F20"].ToString();
                //        dtR[2] = dtReader["FROMBANK"].ToString();
                //        dtR[3] = dtReader["TOBANK"].ToString();
                //        dtR[4] = dtReader["INTERFACE"].ToString();
                //        dtR[5] = dtReader["DATECREATE"].ToString();
                //        dtR[6] = dtReader["RM_ACCOUNT"].ToString();
                //        dtR[7] = dtReader["STATUS"].ToString();
                //        dtR[8] = dtReader["MSGCONTENT"].ToString();
                //        dtR[9] = dtReader["GWOPTION"].ToString();
                //        dtb.Rows.Add(dtR);
                //    }
                //}

                //datDs.Tables.Add(dtb);
                return datDs;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataSet GetIQS_MSG_CONTENTList(string Interface)
        {
            DataSet datDs = new DataSet();
            string strSQL = "select imc.MSG_ID,imc.F20,imc.FROMBANK,imc.TOBANK,imc.INTERFACE,imc.DATECREATE,imc.RM_ACCOUNT,imc.STATUS,imc.MSGCONTENT,imc.GWOPTION  from IQS_MSG_CONTENT imc where imc.INTERFACE='" + Interface + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetIQS()
        {
            //string strSQL = "select imc.MSG_ID,imc.query_id,imc.ref_number,imc.FROMBANK,imc.TOBANK,imc.INTERFACE,imc.DATECREATE,imc.org_rm_number,(select a.content from Allcode a where trim(a.cdname)='STATUS' and trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(imc.status)) as STATUS,imc.MSGCONTENT from IQS_MSG_CONTENT imc  ";
            //string strSQL = "select MSG_ID,QUERY_ID,(select a.content from Allcode a where trim(a.cdname)='MSG_TYPE' and trim(gwtype)='IQS'  and trim(a.cdval)= trim(MSG_TYPE)) as MSG_TYPE, REF_NUMBER,ORG_RM_NUMBER,substr(FROMBANK,-3) as FROMBANK,substr(TOBANK,-3) as TOBANK,AMOUNT,CCYCD,PRODUCT_CODE,(select a.content from Allcode a where trim(a.cdname)='STATUS' and trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(status)) as STATUS,ORG_TRANS_DATE,DATECREATE,INTERFACE,TELLER_ID,MSGCONTENT from IQS_MSG_CONTENT where to_char(to_date(datecreate,'DD/MM/YYYY'),'DD/MM/YYYY') = to_char(to_date(Sysdate,'DD/MM/YYYY'),'DD/MM/YYYY') order by MSG_TYPE ";//'"+ DateTime.sy +"'
            string strSQL = "select MSG_ID,QUERY_ID,(select a.content from Allcode a where trim(a.cdname)='MSG_TYPE_VIEW' and trim(gwtype)='IQS'  and trim(a.cdval)= trim(MSG_TYPE)) as MSG_TYPE,IQSTRANSNUM, REF_NUMBER,ORG_RM_NUMBER,AMOUNT,CCYCD,substr(FROMBANK,-3) as FROMBANK,substr(TOBANK,-3) as TOBANK,PRODUCT_CODE,(select NAME from STATUS a where trim(a.STATUS)= IQS_MSG_CONTENT.STATUS ) as STATUS,ORG_TRANS_DATE,DATECREATE,INTERFACE,TELLER_ID,MSGCONTENT from IQS_MSG_CONTENT where to_char(datecreate,'DD/MM/YYYY')=to_char(sysdate,'DD/MM/YYYY') order by MSG_TYPE ";//'"+ DateTime.sy +"'
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable Search_IQS(string strWHERE)
        {
            //select MSG_ID,QUERY_ID, REF_NUMBER,ORG_RM_NUMBER,substr(FROMBANK,-3) as FROMBANK,substr(TOBANK,-3) as TOBANK,AMOUNT,CCYCD,PRODUCT_CODE,(select a.content from Allcode a where trim(a.cdname)='STATUS' and trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(status)) as STATUS,ORG_TRANS_DATE,DATECREATE,INTERFACE,TELLER_ID,MSGCONTENT from IQS_MSG_CONTENT
            //string strSQL = "select imc.MSG_ID,imc.query_id,imc.ref_number,imc.FROMBANK,imc.TOBANK,imc.INTERFACE,imc.DATECREATE,imc.org_rm_number,(select a.content from Allcode a where trim(a.cdname)='STATUS' and trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(imc.status)) as STATUS,imc.MSGCONTENT from IQS_MSG_CONTENT imc  " + strWHERE + "";
            //string strSQL = "select MSG_ID,QUERY_ID,(select a.content from Allcode a where trim(a.cdname)='MSG_TYPE' and trim(gwtype)='IQS'  and trim(a.cdval)= trim(MSG_TYPE)) as MSG_TYPE, REF_NUMBER,ORG_RM_NUMBER,substr(FROMBANK,-3) as FROMBANK,substr(TOBANK,-3) as TOBANK,AMOUNT,CCYCD,PRODUCT_CODE,(select a.content from Allcode a where trim(a.cdname)='STATUS' and trim(gwtype)='SYSTEM'  and trim(a.cdval)= trim(status)) as STATUS,ORG_TRANS_DATE,DATECREATE,INTERFACE,TELLER_ID,MSGCONTENT from IQS_MSG_CONTENT imc " + strWHERE + "  order by MSG_TYPE";
            string strSQL = "select MSG_ID,QUERY_ID,(select a.content from Allcode a where trim(a.cdname)='MSG_TYPE_VIEW' and trim(gwtype)='IQS'  and trim(a.cdval)= trim(MSG_TYPE)) as MSG_TYPE,IQSTRANSNUM, REF_NUMBER,ORG_RM_NUMBER,AMOUNT,CCYCD,substr(FROMBANK,-3) as FROMBANK,substr(TOBANK,-3) as TOBANK,PRODUCT_CODE,(select a.name from status a where a.status=imc.STATUS and rownum<=1) as STATUS,ORG_TRANS_DATE,DATECREATE,INTERFACE,TELLER_ID,MSGCONTENT from IQS_MSG_CONTENT imc " + strWHERE + "  order by MSG_TYPE";//HaNTT10 thay doi ngay 29/09/2008
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable Get_MSGTYPE(int strQueryID)
        {
            string strSQL = "select MSG_TYPE from IQS_MSG_CONTENT a where trim(a.query_id)='" + strQueryID + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        //public DataTable IQSGetTrannum(IQS_MSG_CONTENTInfo objTable)
        //{
        //    //string strSQL = "select distinct (I.Tad) from TAD I where I.Status in (select a.cdval from Allcode a  where a.cdname = 'CITADSTS' and a.content = 'CLOSED')";
        //    try
        //    {
        //        OracleParameter[] oraParas ={new OracleParameter("pMSG_Type",OracleType.NVarChar,50)
        //                                       };
        //        oraParas[0].Value = objTable.MSG_TYPE;

        //        oraConn = conn.Connect();
        //        if (oraConn == null)
        //        {
        //            return null;
        //        }
        //        return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "IQSGETTRANNUM.IQSGetTrannum", oraParas).Tables[0];

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public string IQSGetTrannum(IQS_MSG_CONTENTInfo objTable)
        {
            string strSql = "IQSGetTrannum"; //"TESTTHU";// "IQSGetTrannum";
            string strReturn = "";

            OracleParameter[] oraParam ={new OracleParameter("pType",OracleType.VarChar,3),
                                         new OracleParameter("pTrannum",OracleType.VarChar,20)
                                                   };
            oraParam[0].Value = objTable.MSG_TYPE;
            oraParam[1].Direction = ParameterDirection.InputOutput;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
                strReturn = oraParam[1].Value.ToString();
                return strReturn;
            }
            catch //(Exception ex)
            {
                return null; ;
            }
        }

        public int AddIQS_MSG_CONTENT_TTSP_VCB(IQS_MSG_CONTENTInfo objTable, string strIQSTransNumber, string strMSG_TYPE)
        {
            string strSql = "GW_PK_IQS_TTSP_Q_ConvertIN.GWConvertIQS";
            OracleParameter[] oraParam = { new OracleParameter("pQUERY_ID", OracleType.Number,10),
                                         new OracleParameter("pINTERFACE", OracleType.VarChar,10),
                                         new OracleParameter("pSENDER", OracleType.VarChar,8),
                                         new OracleParameter("pRECEIVER", OracleType.VarChar,8),
                                         new OracleParameter("pREFNUMBER", OracleType.VarChar,20),
                                         new OracleParameter("pRMNUMBER", OracleType.VarChar,20),
                                         new OracleParameter("pIQSTRANNUM", OracleType.VarChar,20),
                                         new OracleParameter("pVALUEDATE", OracleType.DateTime),
                                         new OracleParameter("pORGMSG", OracleType.VarChar,2000),
                                         new OracleParameter("pGWOPTION", OracleType.VarChar,1000),
                                         new OracleParameter("pMSGTYPE", OracleType.VarChar,8),
                                         new OracleParameter("pORGMSGTYPE", OracleType.VarChar,6),
                                         new OracleParameter("pTellerID", OracleType.VarChar,10),
                                         new OracleParameter("pPRDCODE", OracleType.VarChar,3),
                                         new OracleParameter("pAMOUNT", OracleType.Number,19),
                                         new OracleParameter("pCCYCD", OracleType.VarChar,3)};

            oraParam[0].Value = objTable.QUERY_ID;
            oraParam[1].Value = objTable.INTERFACE;
            oraParam[2].Value = objTable.FROMBANK;
            oraParam[3].Value = objTable.TOBANK;
            oraParam[4].Value = objTable.REF_NUMBER;
            oraParam[5].Value = objTable.ORG_RM_NUMBER;
            oraParam[6].Value = strIQSTransNumber;
            oraParam[7].Value = objTable.ORG_TRANS_DATE;
            oraParam[8].Value = objTable.MSGCONTENT;
            oraParam[9].Value = objTable.GWOPTION;
            oraParam[10].Value = objTable.MSG_TYPE;
            oraParam[11].Value = strMSG_TYPE;
            oraParam[12].Value = objTable.TELLER_ID;
            oraParam[13].Value = objTable.PRODUCT_CODE;
            oraParam[14].Value = Convert.ToDouble(objTable.AMOUNT);
            oraParam[15].Value = objTable.CCYCD;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1; 
            }
        }
    }
}
