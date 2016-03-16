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

//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	11/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 11/06/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class TTSP_MSG_CONTENTDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        public TTSP_MSG_CONTENTDP()
		{
		}
        public static TTSP_MSG_CONTENTDP Instance()
		{
            return new TTSP_MSG_CONTENTDP();
		}
        //Namg cap

        // Nang cap

        public DataSet TTSP_CONTENT_LOAD(out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pContent",OracleType.Cursor),                                        
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            Oraparam[0].Direction = ParameterDirection.Output;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {                
                string strProName = "GW_PK_TTSP_MSG_CONTENT.TTSP_MSG_CONTENT";

                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strProName, Oraparam);

            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet TTSP_CONTENT_RESEND(out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pContent",OracleType.Cursor),                                        
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            Oraparam[0].Direction = ParameterDirection.Output;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_TTSP_MSG_CONTENT.TTSP_MSG_CONTENT_RESEND", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet TTSP_CONTENT_SEARCH_RESEND(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),
                                         new OracleParameter("pdatefrom",OracleType.DateTime,7),
                                         new OracleParameter("pdateto",OracleType.DateTime,7),
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[5].Direction = ParameterDirection.Output;
            Oraparam[6].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = datefrom;
            Oraparam[2].Value = dateto;


            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_TTSP_MSG_CONTENT.TTSP_SEARCH_RESEND", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //quynd
        public DataSet TTSP_CONTENT_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            //DONGPV
            //OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),
            //                             new OracleParameter("pdatefrom",OracleType.DateTime,7),
            //                             new OracleParameter("pdateto",OracleType.DateTime,7),
            //                             new OracleParameter("pContent",OracleType.Cursor),
            //                             new OracleParameter("pAll",OracleType.Cursor),
            //                             new OracleParameter("pAll_his",OracleType.Cursor),
            //                             new OracleParameter("pColumns",OracleType.Cursor),
            //                             };

            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),
                                         new OracleParameter("pdatefrom",OracleType.DateTime,8),
                                         new OracleParameter("pdateto",OracleType.DateTime,8),
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            //Oraparam[0].Value = pWhere.Replace("SENDER", "SEND_CODE").Replace("RECEIVER", "RECEIVE_CODE").Replace("FIELD20", "F20").Replace("AMOUNT", "F32AS3").Replace("CCYCD", "F32AS2").Replace("STATUS", "STATUS_CODE");
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = datefrom;
            Oraparam[2].Value = dateto;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[5].Direction = ParameterDirection.Output;
            Oraparam[6].Direction = ParameterDirection.Output;                                 

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_TTSP_MSG_CONTENT.TTSP_MSG_CONTENT_SEARCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet TTSP_CONTENT_SEARCH_ADVANCE_RESEND(string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),                                        
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_TTSP_MSG_CONTENT.TTSP_SEARCH_RESEND_ADVANCE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }


        public DataSet TTSP_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),                                        
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };

            //Oraparam[0].Value = pWhere.Replace("SENDER", "SEND_CODE").Replace("RECEIVER", "RECEIVE_CODE").Replace("FIELD20", "F20").Replace("AMOUNT", "F32AS3").Replace("CCYCD", "F32AS2").Replace("STATUS", "STATUS_CODE");
            Oraparam[0].Value = pWhere;
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Direction = ParameterDirection.Output;
           
            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_TTSP_MSG_CONTENT.TTSP_MSG_CONTENT_ADVANCE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }


        //nang cap----------------------------------------------------------------------


        public DataTable GetTTSP_MSG_CONTENT_CONTENT(string lMsgID)
        {
            try
            {
                string strSQL = "GW_PK_TTSP_MSG_CONTENT.GET_MESSAGE_ONE";
                OracleParameter[] oraParas ={new OracleParameter("pMSG_ID",OracleType.Number,20),
                                            new OracleParameter("pcurcontent",OracleType.Cursor)
                                                };
                oraParas[0].Value = lMsgID;
                oraParas[1].Direction= ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas).Tables[0];
            }
            catch
            {
                return null;
            }
        }


        public DataTable GetData_print_ttsp(string strMsgID, string strMSG_TYPE, string strMSGDIRECTION)
        {
            try
            {
                //DONGPV
                string strSQL = "GW_PK_TTSP_MSG_CONTENT.GET_MSGDTL_RP";
                OracleParameter[] oraParas ={new OracleParameter("pMSG_ID",OracleType.Number,20),
                                            new OracleParameter("pMSG_TYPE",OracleType.VarChar,20),
                                            new OracleParameter("pMSGDirection",OracleType.VarChar,20),
                                            new OracleParameter("pCurContent",OracleType.Cursor)};

                oraParas[0].Value = strMsgID;
                oraParas[1].Value = strMSG_TYPE;
                oraParas[2].Value = strMSGDIRECTION;
                oraParas[3].Direction = ParameterDirection.Output;

                //string strSQL = "GW_PK_TTSP_REPORT.TTSP_PRINT_MSG";
                //OracleParameter[] oraParas ={new OracleParameter("pMSG_ID",OracleType.Number,20),
                //                            new OracleParameter("pmsg_type",OracleType.VarChar,20),
                //                            new OracleParameter("pmsgdirection",OracleType.VarChar,20),
                //                            new OracleParameter("pBranch",OracleType.VarChar,12),
                //                            new OracleParameter("pUser",OracleType.VarChar,50),
                //                            new OracleParameter("pcurcontent",OracleType.Cursor)};

                //oraParas[0].Value = strMsgID;
                //oraParas[1].Value = strMSG_TYPE;
                //oraParas[2].Value = strMSGDIRECTION;
                //oraParas[3].Value = "0201";
                //oraParas[4].Value = "346347";
                //oraParas[5].Direction = ParameterDirection.Output;



                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        
        //public DataTable GetData_print_ttsp(string strMsgID, string pTable_Name, string pDtl_name)
        //{
        //    try
        //    {
        //        string strSQL = " SELECT M.branch_sender,N.branch_receive,K.sender,K.receiver,K.msg_type,K.msg_direction,K.field_name,";
        //        strSQL = strSQL + " K.field_value,V.V_FIELD_NAME,Y.V_MSG_NAME,Q.field_order,B.CONTENT AS STATUS";
        //        strSQL = strSQL + " FROM (select X.*, T.FIELD_NAME, T.field_value,T.ROW_NUM";
        //        strSQL = strSQL + " from (select * from (select query_id,sender,receiver,msg_direction,msg_type, status";
        //        strSQL = strSQL + " from " + pTable_Name + " where trim(query_id) = trim(" + strMsgID + "))";
        //        strSQL = strSQL + " where rownum = 1) X";
        //        strSQL = strSQL + " left join (select u.query_id,u.FIELD_NAME,u.field_value,u.ROW_NUM from (select max(query_id) query_id,FIELD_NAME,";
        //        strSQL = strSQL + " max(decode(PART_ROW,0,FIELD_VALUE,'')) || ' ' || max(decode(PART_ROW,1,FIELD_VALUE,'')) || ' ' ||";
        //        strSQL = strSQL + " max(decode(PART_ROW,2,FIELD_VALUE,'')) field_value,max(ROW_NUM) ROW_NUM";
        //        strSQL = strSQL + " from " + pDtl_name + " where trim(query_id) = trim(" + strMsgID + ") and FIELD_NAME not in ('32A') group by FIELD_NAME, ROW_NUM";
        //        strSQL = strSQL + " union";
        //        strSQL = strSQL + " select max(query_id) query_id,max(FIELD_NAME) FIELD_NAME,max(decode(row_num,1,FIELD_VALUE,'')) || ' ' ||";
        //        strSQL = strSQL + " max(decode(row_num,2,FIELD_VALUE,'')) || ' ' || max(decode(row_num,3,FIELD_VALUE,'')) field_value,max(ROW_NUM) ROW_NUM";
        //        strSQL = strSQL + " from " + pDtl_name + " where trim(query_id) = trim(" + strMsgID + ") and FIELD_NAME = '32A' group by FIELD_NAME ) U";
        //        strSQL = strSQL + " ) T on trim(t.query_id) = trim(X.query_id)) K";
        //        strSQL = strSQL + " left join (select FIELD_NAME, V_FIELD_NAME, msg_type from msgfield) V on upper(trim(K.FIELD_NAME)) = upper(trim(V.FIELD_NAME))";
        //        strSQL = strSQL + " and substr(trim(K.msg_type),3,5) = trim(V.msg_type)";
        //        strSQL = strSQL + " left join msgtype Y on substr(trim(K.msg_type), 3, 5) = trim(Y.msg_type)";
        //        strSQL = strSQL + " left join (SELECT sibs_bank_code, bran_name branch_sender FROM BRANCH) M ON TRIM(K.sender) = TRIM(M.sibs_bank_code)";
        //        strSQL = strSQL + " left join (SELECT sibs_bank_code, bran_name branch_receive FROM BRANCH) N ON TRIM(K.receiver) = TRIM(N.sibs_bank_code)";
        //        strSQL = strSQL + " LEFT JOIN (SELECT CONTENT, CDVAL FROM allcode WHERE upper(gwtype)='SYSTEM' and upper(cdname)='STATUS') B ON trim(K.status)= trim(B.cdval)";
        //        strSQL = strSQL + " LEFT JOIN (select distinct B.swift_trans_code,B.map_direction, A.swift_field_name,A.field_order,A.ROW_NUM";
        //        strSQL = strSQL + " from SWIFT_FIELD_MAP A INNER JOIN SWIFT_MSG_MAP B ON A.msg_map_id= B.MSG_MAP_ID) Q ON UPPER(TRIM(K.msg_type))=UPPER(TRIM(Q.swift_trans_code)) AND";
        //        strSQL = strSQL + " DECODE(K.msg_direction,'SIBS-TTSP',1,0) = Q.map_direction AND UPPER(TRIM(K.field_name)) =UPPER(TRIM(Q.swift_field_name)) AND";
        //        strSQL = strSQL + " K.ROW_NUM = Q.ROW_NUM ORDER BY Q.field_order,K.field_name,K.ROW_NUM";

        //        oraConn = connect.Connect();
        //        if (oraConn == null)
        //        {
        //            return null;
        //        }
        //        return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //het nang cap



    }
}
