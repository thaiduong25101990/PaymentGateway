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
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 11/06/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_MSG_CONTENTDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public SWIFT_MSG_CONTENTDP()
        {
        }
        public static SWIFT_MSG_CONTENTDP Instance()
        {
            return new SWIFT_MSG_CONTENTDP();
        }
        //nang cap BR---------------------------------------------------------------------------

        public DataSet SWIFT_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.SWIFT_MSG_CONTENT_ADVANCE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataTable Search_Content(string pMsg_id)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " GW_PK_SWIFT_MSG_CONTENT.GET_CONTENT_ONE ";

            OracleParameter[] Oraparam = {new OracleParameter("pMSG_ID",OracleType.Number,20),
                                         new OracleParameter("pContent",OracleType.Cursor)};
            Oraparam[0].Value = pMsg_id;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, Oraparam).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public DataSet SWIFT_CONTENT( DateTime datefrom,DateTime dateto,string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),
                                         new OracleParameter("pdatefrom",OracleType.DateTime,8),
                                         new OracleParameter("pdateto",OracleType.DateTime,8),
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_SWIFT_MESSAGE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //ham lay dien cho form resend lai dien trong ngay cua bang content
        public DataSet LOAD_DATA_RESEND(string pWhere,int pTeller, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000), 
                                         new OracleParameter("pTeller",OracleType.Number,1),  
                                         new OracleParameter("pContent",OracleType.Cursor),                                         
                                         new OracleParameter("pColumns",OracleType.Cursor)
                                         };
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;           
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = pTeller;
            


            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_CONTENT_RESEND", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //ham lay dien cho form resend 
        public DataSet SEARCH_DATA_RESEND(DateTime datefrom, DateTime dateto, string pWhere, int pTeller, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000), 
                                         new OracleParameter("pTeller",OracleType.Number,1),  
                                         new OracleParameter("pdatefrom",OracleType.DateTime,8),
                                         new OracleParameter("pdateto",OracleType.DateTime,8),
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor)
                                         };
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[5].Direction = ParameterDirection.Output;
            Oraparam[6].Direction = ParameterDirection.Output;
            Oraparam[7].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = pTeller;
            Oraparam[2].Value = datefrom;
            Oraparam[3].Value = dateto;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_SEARCH_RESEND", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public int DELETE_PROCESS_HANDER(string vMSG_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pMsg_id",OracleType.VarChar,3500)                                        
                                         };
            Oraparam[0].Value = vMSG_ID;
            
            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_PROCESS_HANDICRAFT.DELETE_SWIFT_PROCESS_HANDER", Oraparam);
                oraConn.Close();
                oraConn.Dispose();
                return 1;
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public DataSet MESSAGE_CONTENT(string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),                                         
                                         new OracleParameter("pContent",OracleType.Cursor),                                         
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;            
            Oraparam[0].Value = pWhere;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_CONTENT", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet MESSAGE_CONTENT_INWARD(string pWhere, int pTeller, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),  
                                         new OracleParameter("pTeller",OracleType.Number,1),                                         
                                         new OracleParameter("pContent",OracleType.Cursor),                                         
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = pTeller;
           

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_CONTENT_INWARD", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet MESSAGE_CONTENT_DATE(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
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
                                         new OracleParameter("pColumns",OracleType.Cursor)
                                         };
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = datefrom;
            Oraparam[2].Value = dateto;


            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_CONTENT_DATE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet MESSAGE_CONTENT_INSWARD_SEARCH(DateTime datefrom, DateTime dateto, string pWhere,int pTeller, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),  
                                        new OracleParameter("pTeller",OracleType.Number,1),
                                         new OracleParameter("pdatefrom",OracleType.DateTime,8),
                                         new OracleParameter("pdateto",OracleType.DateTime,8),
                                         new OracleParameter("pContent",OracleType.Cursor),                                         
                                         new OracleParameter("pColumns",OracleType.Cursor)
                                         };
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[5].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
            Oraparam[1].Value = pTeller;
            Oraparam[2].Value = datefrom;
            Oraparam[3].Value = dateto;


            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.GET_CONTENT_INWARD_SEARCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataTable PROCESS_HANDICRAFT(string vProcess, string vMsg_id, string vTable, string vRows,string vTeller_id, out DataTable _dtRows)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtRows = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pProcess",OracleType.VarChar,2000),  
                                         new OracleParameter("pMsg_id",OracleType.VarChar,2000),
                                         new OracleParameter("pTable",OracleType.VarChar,2000),
                                         new OracleParameter("pRows",OracleType.VarChar,2000),
                                         new OracleParameter("pTeller_id",OracleType.VarChar,28),
                                         new OracleParameter("pReturn_Rows",OracleType.Cursor)
                                         };
            Oraparam[5].Direction = ParameterDirection.Output;
            Oraparam[0].Value = vProcess;
            Oraparam[1].Value = vMsg_id;
            Oraparam[2].Value = vTable;
            Oraparam[3].Value = vRows;
            Oraparam[4].Value = vTeller_id;

            try
            {
                _dtRows = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_PROCESS_HANDICRAFT.GET_PROCESS", Oraparam).Tables[0];
                 oraConn.Close();
                 oraConn.Dispose();
                 return _dtRows;
            }
            catch //(Exception ex)
            {
                return _dtRows = null;
            }
        }

        public DataTable PROCESS_HANDICRAFT_SIBS_SWIFT(string vMsg_id, string vRows, string vTeller_id, out DataTable _dtRows)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtRows = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pMsg_id",OracleType.VarChar,2000),                                        
                                         new OracleParameter("pRows",OracleType.VarChar,2000),
                                         new OracleParameter("pTeller_id",OracleType.VarChar,28),
                                         new OracleParameter("pReturn_Rows",OracleType.Cursor)
                                         };
            Oraparam[3].Direction = ParameterDirection.Output;           
            Oraparam[0].Value = vMsg_id;            
            Oraparam[1].Value = vRows;
            Oraparam[2].Value = vTeller_id;

            try
            {
                _dtRows = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_PROCESS_HANDICRAFT.GET_PROCESS_SIBS_SWIFT", Oraparam).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dtRows;
            }
            catch //(Exception ex)
            {
                return _dtRows = null;
            }
        }

        public DataTable PROCESS_HANDICRAFT_SUPPER(string vProcess, string vMsg_id, string vTable, string vRows, string vTeller_id, out DataTable _dtRows)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtRows = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pProcess",OracleType.VarChar,2000),  
                                         new OracleParameter("pMsg_id",OracleType.VarChar,2000),
                                         new OracleParameter("pTable",OracleType.VarChar,2000),
                                         new OracleParameter("pRows",OracleType.VarChar,2000),                                         
                                         new OracleParameter("pReturn_Rows",OracleType.Cursor)
                                         };
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[0].Value = vProcess;
            Oraparam[1].Value = vMsg_id;
            Oraparam[2].Value = vTable;
            Oraparam[3].Value = vRows;
            

            try
            {
                _dtRows = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_PROCESS_HANDICRAFT.GET_PROCESS_SUPPER", Oraparam).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dtRows;
            }
            catch //(Exception ex)
            {
                return _dtRows = null;
            }
        }


        //nang cap BR---------------------------------------------------------------------------

      
        public DataTable GetData_Pre(string pMsg_id,string pTable_NA)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select B.PRE_PROCESSSTS  from  " + pTable_NA + "  B Where trim(B.MSG_ID)='" + pMsg_id + "' ";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable SWIFT_STATUS(string pTable,string pMsg_id)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select B.PROCESSSTS,B.TELLER_ID,B.OFFICER_ID,B.SWMSTS  from  " + pTable + "  B Where trim(B.MSG_ID)='" + pMsg_id.Trim() + "' ";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Check_LOCKSTS(string pMsg_id, string pTable_NA)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select B.LOCKSTS,B.LOCK_TELLERID  from  " + pTable_NA + "  B Where trim(B.MSG_ID)='" + pMsg_id + "' ";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }     
      

       
        //DatHM add 11/09/2008 
        public DataTable swift_print_map(string strQID, string pTable_name)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;            }
           
            string strSQL = " select a.branch_a,a.branch_b,p.bank_name_a,q.bank_name_b,y.E_MSG_NAME,a.msg_type,a.content,msg_direction,'' as CONTENT1,'' as CONTENT2,'' as CONTENT3,'' as CONTENT4,'' as CONTENT5,'' as CONTENT6,'' as CONTENT7,'' as CONTENT8,'' as CONTENT9,";
            strSQL = strSQL + " (SELECT A4.CONTENT FROM ALLCODE A4 WHERE UPPER(A4.GWTYPE)='SWIFT' AND UPPER(A4.CDNAME)='SWMSTS' AND TRIM(A.SWMSTS)=TRIM(A4.CDVAL) AND ROWNUM=1) AS SWMSTS,";
            strSQL = strSQL + " (SELECT A4.NAME as CONTENT FROM STATUS A4 WHERE TRIM(A.STATUS)=TRIM(A4.STATUS) AND ROWNUM=1) AS STATUS,";
            strSQL = strSQL + " (SELECT A4.CONTENT FROM ALLCODE A4 WHERE UPPER(A4.GWTYPE)='SWIFT' AND UPPER(A4.CDNAME)='PROCESSSTS' AND TRIM(A.PROCESSSTS)=(A4.CDVAL) AND ROWNUM=1) AS PROCESSSTS ";
            strSQL = strSQL + " from (select query_id,msg_type,branch_a,branch_b,content,status,msg_direction,SWMSTS,PROCESSSTS from " + pTable_name + " where trim(query_id)='" + strQID + "' and rownum=1)a";
            strSQL = strSQL + " left join(select lpad(a.sibs_bank_code,5,'0') as sibs_bank_code,a.BRAN_NAME bank_name_a from branch a )P on trim(P.sibs_bank_code)=trim(a.branch_a)";
            strSQL = strSQL + " left join(select lpad(a.sibs_bank_code,5,'0') as sibs_bank_code,a.BRAN_NAME bank_name_b from branch a)Q on trim(Q.sibs_bank_code)=trim(a.branch_b)";
            strSQL = strSQL + " left join msgtype Y on substr(trim(a.msg_type),3,5)=trim(Y.msg_type)";
            

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable swift_print_03(string strMsgID, string strMSG_TYPE,
            string strMSGDIRECTION, string strDepartment)
        {
            try
            {   string strSQL = "";

            if (strMSG_TYPE.Trim() == "MT700" || strMSG_TYPE.Trim() == "MT701" || strMSG_TYPE.Trim() == "MT721")
            {
                strSQL = "GW_PK_SWIFT_REPORT.SWIFT_PRINT_MSG_TF";  
            }
            else
            {
                strSQL = "GW_PK_SWIFT_REPORT.SWIFT_PRINT_MSG";
            }

            //if (strDepartment == "RM" || strDepartment == "TR")
            //        strSQL = "GW_PK_SWIFT_REPORT.SWIFT_PRINT_MSG";
            //    else
            //        strSQL = "GW_PK_SWIFT_REPORT.SWIFT_PRINT_MSG_TF";      

                OracleParameter[] oraParas ={new OracleParameter("pMSG_ID",OracleType.Number,20),
                                            new OracleParameter("pmsg_type",OracleType.VarChar,20),
                                            new OracleParameter("pmsgdirection",OracleType.VarChar,20),
                                            new OracleParameter("pcurcontent",OracleType.Cursor)};

                oraParas[0].Value = strMsgID;
                oraParas[1].Value = strMSG_TYPE;
                oraParas[2].Value = strMSGDIRECTION;
                oraParas[3].Direction = ParameterDirection.Output;
                
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        //co che Lock ban ghi khi xu ly thu cong
        public int Lock_User(SWIFT_MSG_CONTENTInfo objTable)
        {
            DataTable datTable = new DataTable();
            string pTable = objTable.Table_Name;
            string strSQL = "Update " + pTable + " set LOCKSTS=:pLOCKSTS,LOCK_TELLERID=:pLOCK_TELLERID  where MSG_ID=:pMSG_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pLOCKSTS", OracleType.Char,1),
                                         new OracleParameter("pLOCK_TELLERID", OracleType.VarChar,8)
                                         };
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.LOCKSTS;
            oraParam[2].Value = objTable.LOCK_TELLERID;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }
        public int UPDATE_SWIFT_PROCESS_HANDER(SWIFT_MSG_CONTENTInfo objTable)
        {
            DataTable datTable = new DataTable();
            string pTable = objTable.Table_Name;
            string strSQL = "Update " + pTable + "  set PRE_PROCESSSTS = :pPRE_PROCESSSTS,PROCESSSTS = :pPROCESSSTS,";
            strSQL = strSQL + "  BRANCH_B = :pBRANCH_B, DEPARTMENT = :pDEPARTMENT,TELLER_ID = :pTELLER_ID,";
            strSQL = strSQL + "  PRE_BRANCH=:pPRE_BRANCH,PRE_DEPT=:pPRE_DEPT where MSG_ID = :pMSG_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pBRANCH_B", OracleType.NVarChar,12),
                                         new OracleParameter("pPROCESSSTS", OracleType.Char,2),
                                         new OracleParameter("pPRE_PROCESSSTS", OracleType.Char,5),
                                         new OracleParameter("pTELLER_ID", OracleType.VarChar,20),
                                         new OracleParameter("pDEPARTMENT", OracleType.NVarChar,10),
                                         new OracleParameter("pPRE_BRANCH", OracleType.VarChar,20),
                                         new OracleParameter("pPRE_DEPT", OracleType.VarChar,20),
                                         };
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.BRANCH_B;
            oraParam[2].Value = objTable.PROCESSSTS;
            oraParam[3].Value = objTable.PRE_PROCESSSTS;
            oraParam[4].Value = objTable.TELLER_ID;
            oraParam[5].Value = objTable.DEPARTMENT;//ma department moi
            oraParam[6].Value = objTable.PRE_BRANCH;//ghi lai ma chi nhanh cu
            oraParam[7].Value = objTable.PRE_DEPT;// ghi lai ma department cu

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

      

        //Ham cap nhat trang thai dien cho form frmSwiftMsgManualInfo voi nguoi dung la KSV
        public int UpdateSWIFT_MSG_CONTENTStatusT(SWIFT_MSG_CONTENTInfo objTable)
        {
            string strSql = objTable.PACKAGE;
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pBranch_B", OracleType.NVarChar,12),
                                         new OracleParameter("pDepartment", OracleType.NVarChar,10),
                                         new OracleParameter("pAuto", OracleType.Char,1),
                                         new OracleParameter("pSWMSTS", OracleType.Char,5),
                                         new OracleParameter("pOfficerID", OracleType.VarChar,20),
                                         new OracleParameter("pPROCESSSTS", OracleType.VarChar,5),                                         
                                         new OracleParameter("pSTATUS", OracleType.VarChar,5)};

            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.BRANCH_B;
            oraParam[2].Value = objTable.DEPARTMENT;
            oraParam[3].Value = objTable.AUTO;
            oraParam[4].Value = objTable.SWMSTS;
            oraParam[5].Value = objTable.OFFICER_ID;
            oraParam[6].Value = objTable.PROCESSSTS;           
            oraParam[7].Value = objTable.STATUS;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        public int Update_tellerID(string pQUERY_ID, string pTable_name)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update   " + pTable_name + "  set TELLER_ID = '' where QUERY_ID='" + pQUERY_ID + "'";           

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public int UPDATE_SYSDATE(string pMSG_ID,string pTABLE)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update   " + pTABLE + "   s set s.trans_date=sysdate , s.transdate= To_char(sysdate,'YYYYMMDD'),TELLER_ID='' where s.msg_id ='" + pMSG_ID + "' ";

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public int UPDATE_SYSDATE_MSGDTL(string pQUERY_ID, string pTABLE)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update   " + pTABLE + "  SM set SM.Trans_Date = sysdate where SM.QUERY_ID= " + pQUERY_ID + "";

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        //Update du lieu cho truong Status trong form frmSwiftMsgManualDup
        public int UpdateSWIFT_MSG_CONTENTStatusSwiftMsgManualDup1(SWIFT_MSG_CONTENTInfo objTable, string SWMsts)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update SWIFT_MSG_CONTENT set STATUS = :pSTATUS,SWMSTS = '" + SWMsts + "' where MSG_ID=:pMSG_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pSTATUS", OracleType.Char,1)
                                         //new OracleParameter("pDEPARTMENT", OracleType.NVarChar,10)
                                         };
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.STATUS;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }


      
        //*Update du lieu cho truong SWMSTS trong form frmSwiftMsgManualDup (quan ly dien di bi trung cua GDV)
        public int UpdateSWIFT_MSG_CONTENTStatusSwiftMsgManualDup1(SWIFT_MSG_CONTENTInfo objTable)
        {
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pTELLER_ID", OracleType.VarChar,20),
                                         new OracleParameter("pPROCESSSTS", OracleType.VarChar,5),
                                         new OracleParameter("pPRE_PROCESSSTS", OracleType.VarChar,5)
                                         };
            oraParam[0].Value = objTable.MSG_ID;            
            oraParam[1].Value = objTable.TELLER_ID;
            oraParam[2].Value = objTable.PROCESSSTS;
            oraParam[3].Value = objTable.PRE_PROCESSSTS;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.UPDATE_SWIFT_CONTENT", oraParam);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        //ham khi KSV thu hien Reject 
        public int UpdateSWIFT_MSG_Reject(SWIFT_MSG_CONTENTInfo objTable)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update SWIFT_MSG_CONTENT set PSWMSTS=:pPSWMSTS,SWMSTS = :pSWMSTS,OFFICER_ID= :pOFFICER_ID,TELLER_ID= :pTELLER_ID where MSG_ID=:pMSG_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pPSWMSTS", OracleType.Char,2),
                                         new OracleParameter("pSWMSTS", OracleType.Char,5),
                                         new OracleParameter("pOFFICER_ID", OracleType.VarChar,20),
                                         new OracleParameter("pTELLER_ID", OracleType.VarChar,20)};
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.PSWMSTS;
            oraParam[2].Value = objTable.SWMSTS;
            oraParam[3].Value = objTable.OFFICER_ID;
            oraParam[4].Value = objTable.TELLER_ID;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch // (Exception ex)
            {
                return -1;
            }
        }

          // ham thuc hien (KSV) Appove cac trang thai(Dien trung,Dien loi)
        public int UpdateSWIFT_MSG_CONTENT_T_L(SWIFT_MSG_CONTENTInfo objTable)
        {
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,20),
                                           new OracleParameter("pQuery_ID", OracleType.Number,20),
                                           new OracleParameter("pAuto", OracleType.Char,1),
                                         new OracleParameter("pSWMSTS", OracleType.VarChar,5),
                                         new OracleParameter("pStatus", OracleType.Char,5),
                                         new OracleParameter("pAppverID", OracleType.Char,20),
                                         new OracleParameter("pPROCESSSTS", OracleType.VarChar,5),
                                         new OracleParameter("pPRE_PROCESSSTS", OracleType.VarChar,5)};
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.QUERY_ID;
            oraParam[2].Value = objTable.AUTO;
            oraParam[3].Value = objTable.SWMSTS;
            oraParam[4].Value = objTable.STATUS;
            oraParam[5].Value = objTable.OFFICER_ID;
            oraParam[6].Value = objTable.PROCESSSTS;
            oraParam[7].Value = objTable.PRE_PROCESSSTS;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_Q_CONVERTOUT.SWIFT_RESEND", oraParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // ham thuc hien (KSV) Appove cac trang thai(Dien trung,Dien loi)
        public int UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(SWIFT_MSG_CONTENTInfo objTable,string pTable)
        {
            string strSQL = " Update  " + pTable + "  set AUTO=:pAUTO ,SWMSTS=:pSWMSTS,STATUS=:pSTATUS,OFFICER_ID=:pOFFICER_ID,PROCESSSTS=:pPROCESSSTS,PRE_PROCESSSTS=:pPRE_PROCESSSTS where MSG_ID=:pMSG_ID and QUERY_ID=:pQUERY_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,20),
                                           new OracleParameter("pQUERY_ID", OracleType.Number,20),
                                           new OracleParameter("pAUTO", OracleType.Char,1),
                                         new OracleParameter("pSWMSTS", OracleType.VarChar,5),
                                         new OracleParameter("pSTATUS", OracleType.Char,5),
                                         new OracleParameter("pOFFICER_ID", OracleType.Char,20),
                                         new OracleParameter("pPROCESSSTS", OracleType.VarChar,5),
                                         new OracleParameter("pPRE_PROCESSSTS", OracleType.VarChar,5)};
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.QUERY_ID;
            oraParam[2].Value = objTable.AUTO;
            oraParam[3].Value = objTable.SWMSTS;
            oraParam[4].Value = objTable.STATUS;
            oraParam[5].Value = objTable.OFFICER_ID;
            oraParam[6].Value = objTable.PROCESSSTS;
            oraParam[7].Value = objTable.PRE_PROCESSSTS;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        // ham thuc hien (KSV) Appove cac trang thai(Dien trung,Dien loi)
        public int UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(SWIFT_MSG_CONTENTInfo objTable)
        {
            string sSQL = " Update SWIFT_MSG_CONTENT set SWMSTS=:pSWMSTS,STATUS=:pSTATUS,OFFICER_ID=:pAppverID, ";
            sSQL = sSQL + "  PROCESSSTS=:pPROCESSSTS,ERR_CODE=:pERR_CODE where MSG_ID=:pMSG_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number,20),                                           
                                         new OracleParameter("pSWMSTS", OracleType.VarChar,5),
                                         new OracleParameter("pSTATUS", OracleType.Char,5),
                                         new OracleParameter("pAppverID", OracleType.Char,20),
                                         new OracleParameter("pPROCESSSTS", OracleType.VarChar,5),
                                         new OracleParameter("pERR_CODE", OracleType.Number,3)};
            oraParam[0].Value = objTable.MSG_ID;           
            oraParam[1].Value = objTable.SWMSTS;
            oraParam[2].Value = objTable.STATUS;
            oraParam[3].Value = objTable.OFFICER_ID;
            oraParam[4].Value = objTable.PROCESSSTS;
            oraParam[5].Value = objTable.ERR_CODE;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, sSQL , oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        // hàm lấy thông tin của TellerID có trong swift_msg_content cho form 
        public DataSet GetTellerID(int strQueryID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select smc.teller_id from swift_msg_content smc where trim(smc.QUERY_ID) =" + strQueryID + "";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public int DeleteSWIFT_MSG_CONTENT_Temp()
        {
            string strSql = "DELETE FROM SWIFT_MSG_CONTENT_Temp";

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /*************************************************
         * Hàm đẩy điện lưu trong GW tham gia đối chiếu
        *************************************************/



        /****************************************
        * Chuc nang doi chieu SWIFT <-> GW
        * HoangLA
        * 14-08-08
        *****************************************/
        public int InsertSW_GW_MSG_CONTENT_TMP(DateTime dtDate, string strDepartment)
        {
            string strSQL = "INSERT INTO SWIFT_MSG_CONTENT_TEMP(MSG_ID,msg_type,msg_direction,branch_a,branch_b,trans_date,field20,department,MUR,ISN,rm_number,SEG_NO,MSG_NO,AMOUNT,VALUE_DATE ,CCYCD,Status ) ";
            if (dtDate.Date == DateTime.Now.Date)
            {
                if (strDepartment == "ALL")
                { strSQL = strSQL + "(SELECT MSG_ID,msg_type,msg_direction,LPAD(BRANCH_A,12,'0'),LPAD(BRANCH_B,12,'0'),trans_date,field20,nvl(department,'XX'),TRANS_NO,ISN,RM_NUMBER,SEQ_NO,MSG_NO,AMOUNT,VALUE_DATE,CCYCD,Status FROM SWIFT_MSG_CONTENT WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND STATUS <> '-1' AND SWMSTS = '0')"; }
                else strSQL = strSQL + "(SELECT MSG_ID,msg_type,msg_direction,LPAD(BRANCH_A,12,'0'),LPAD(BRANCH_B,12,'0'),trans_date,field20,nvl(department,'XX'),TRANS_NO,ISN,RM_NUMBER,SEQ_NO,MSG_NO,AMOUNT,VALUE_DATE,CCYCD,Status FROM SWIFT_MSG_CONTENT WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' and department = '" + strDepartment + "' AND STATUS <> '-1' AND SWMSTS = '0')";
            }
            else
            {
                if (dtDate.Month == DateTime.Now.Month)
                {if (strDepartment == "ALL")
                { strSQL = strSQL + "(SELECT MSG_ID,msg_type,msg_direction,LPAD(BRANCH_A,12,'0'),LPAD(BRANCH_B,12,'0'),trans_date,field20,nvl(department,'XX'),TRANS_NO,ISN,RM_NUMBER,SEQ_NO,MSG_NO,AMOUNT,VALUE_DATE,CCYCD,Status FROM SWIFT_MSG_ALL WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND STATUS <> '-1' AND SWMSTS = '0')"; }
                else strSQL = strSQL + "(SELECT MSG_ID,msg_type,msg_direction,LPAD(BRANCH_A,12,'0'),LPAD(BRANCH_B,12,'0'),trans_date,field20,nvl(department,'XX'),TRANS_NO,ISN,RM_NUMBER,SEQ_NO,MSG_NO,AMOUNT,VALUE_DATE,CCYCD,Status FROM SWIFT_MSG_ALL WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' and department = '" + strDepartment + "' AND STATUS <> '-1' AND SWMSTS = '0')";
                }
                else
                {
                    if (strDepartment == "ALL")
                    { strSQL = strSQL + "(SELECT MSG_ID,msg_type,msg_direction,LPAD(BRANCH_A,12,'0'),LPAD(BRANCH_B,12,'0'),trans_date,field20,nvl(department,'XX'),TRANS_NO,ISN,RM_NUMBER,SEQ_NO,MSG_NO,AMOUNT,VALUE_DATE,CCYCD,Status FROM SWIFT_MSG_ALL_HIS WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND STATUS <> '-1' AND SWMSTS = '0')"; }
                    else strSQL = strSQL + "(SELECT MSG_ID,msg_type,msg_direction,LPAD(BRANCH_A,12,'0'),LPAD(BRANCH_B,12,'0'),trans_date,field20,nvl(department,'XX'),TRANS_NO,ISN,RM_NUMBER,SEQ_NO,MSG_NO,AMOUNT,VALUE_DATE,CCYCD,Status FROM SWIFT_MSG_ALL_HIS WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' and department = '" + strDepartment + "' AND STATUS <> '-1' AND SWMSTS = '0')";
                }
            }
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }
       
        public string OnShowResult(DateTime pDate )
        {
          
              oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20)
                                    ,new OracleParameter("vText",OracleType.VarChar,3000)};
            param[1].Direction = ParameterDirection.InputOutput;
            param[0].Value = pDate.Date;
            param[1].Value = "";
            int iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_RECONCILE_SWIFT.Swift_MESSAGE_OUT", param);
            return param[1].Value.ToString();
            

        }

        public string OnShowResultIN(DateTime pDate)
        {
         
            oraConn = connect.Connect();
            if (oraConn == null)
                return null;

            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20)
                                    ,new OracleParameter("vText",OracleType.VarChar,3000)};
            param[1].Direction = ParameterDirection.InputOutput;
            param[0].Value = pDate.Date;
            param[1].Value = "";
            int iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_RECONCILE_SWIFT.Swift_MESSAGE_IN", param);
            return param[1].Value.ToString();
            

        }

        public int TF_RM_SVR_INDEX(string pQUERYID,string pTABLE)
        {
            string strSQL = "delete from  " + pTABLE + " KK  where  KK.MSG_ID='" + pQUERYID + "'";            
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public DataTable dtExcel(string pQuery_id, out DataTable dsview)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return dsview = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("vQUERYID",OracleType.VarChar,4000)
                                         ,new OracleParameter("vReturn",OracleType.Cursor),
                                         };
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pQuery_id;
            

            try
            {
                return dsview = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "SW_EXP_EXCEL", Oraparam).Tables[0];
            }
            catch //(Exception ex)
            {
                return dsview = null;
            }
        }

        /* Ten ham: Update_Resend_Num
         * Mo ta: Ham cap nhat so lan resend dien
         * Ngay tao: 07/02/2010
         * Nguoi tao: Huypq7         
         */
        public int Update_Resend_Num(SWIFT_MSG_CONTENTInfo objTable)
        {
            //string strSql = "GW_PK_IBPS_Process.IPBS_UPDATE_PRINT_STS";

            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.Number,20)};            
            oraParam[0].Value = objTable.QUERY_ID;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_PROCESS.SWIFT_UPDATE_RESEND_NUM", oraParam);
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        /* Ten ham: Update_Resend_Num
         * Mo ta: Ham cap nhat so lan resend dien
         * Ngay tao: 07/02/2010
         * Nguoi tao: Huypq7         
         */
        public int Get_Resend_Num(SWIFT_MSG_CONTENTInfo objTable)
        {         
            OracleParameter[] oraParam = { new OracleParameter("pQUERY_ID", OracleType.Number, 20) ,
                                           new OracleParameter("pNUM", OracleType.Number, 5) };
            int iNum = 0;

            oraParam[0].Value = objTable.QUERY_ID;
            oraParam[1].Value = objTable.RESEND_NUM;
            oraParam[1].Direction = ParameterDirection.InputOutput;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iNum = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_PROCESS.SWIFT_GET_RESEND_NUM", oraParam);
                if (iNum != 1)
                    return -1;
                else
                    return Convert.ToInt16(oraParam[1].Value.ToString());
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        public int UPDATE_MSG_EDIT(SWIFT_MSG_CONTENTInfo objTable,string chrProcesssts)
        {
            try
            {
                string vSQL1 = "";
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                //string vSQL = "Delete from SWIFT_MSG_EDIT where QUERY_ID = '" + objTable.QUERY_ID + "'";    
                OracleParameter[] oraParamc = { new OracleParameter("pQUERY_ID", OracleType.Number, 10) ,
                                               new OracleParameter("pOUT", OracleType.Number, 5) };
                int mCOunt = 0;
                oraParamc[0].Value = objTable.QUERY_ID;                
                oraParamc[1].Direction = ParameterDirection.InputOutput;

                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.CHECK_EDIT_MESSAGE", oraParamc);
                mCOunt = Convert.ToInt32(oraParamc[1].Value.ToString());
                
                if (mCOunt == 5)
                {
                    return mCOunt;
                }
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                if (objTable.PROCESSSTS != chrProcesssts.Trim())/**/
                {
                    DataTable _dt = new DataTable();
                    _dt = GET_PR_PROCESSSTS(Convert.ToString(objTable.QUERY_ID));                   
                    if (_dt.Rows[0]["PRE_PROCESSSTS"].ToString().Trim() != chrProcesssts.Trim())
                    {
                        oraConn = connect.Connect();
                        if (oraConn == null)
                            return -1;
                        if (objTable.Table_Name == "1") { vSQL1 = "Update swift_msg_content set CONTENT_ORIGIN = CONTENT where  QUERY_ID ='" + objTable.QUERY_ID + "'"; }
                        if (objTable.Table_Name == "2") { vSQL1 = "Update swift_msg_all set CONTENT_ORIGIN = CONTENT where  QUERY_ID ='" + objTable.QUERY_ID + "'"; }
                        if (objTable.Table_Name == "3") { vSQL1 = "Update swift_msg_all_his set CONTENT_ORIGIN = CONTENT where  QUERY_ID ='" + objTable.QUERY_ID + "'"; }
                        DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, vSQL1, null);
                    }
                }               

                OracleParameter[] oraParam = { new OracleParameter("pCONTENT", OracleType.Clob) ,
                                               new OracleParameter("pCONTENT_ORIGIN", OracleType.Clob),
                                               new OracleParameter("pTABLE_NAME", OracleType.VarChar,1),
                                               new OracleParameter("pQUERY_ID", OracleType.VarChar,20)
                                             };
                int iNum = 0;

                oraParam[0].Value = objTable.CONTENT;
                oraParam[1].Value = objTable.CONTENT_ORIGIN;
                oraParam[2].Value = objTable.Table_Name;
                oraParam[3].Value = objTable.QUERY_ID;                

                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                        return -1;
                    DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.UPDATE_MSG_CONTENT_EDIT", oraParam);
                    oraConn.Close();
                    oraConn.Dispose();
                    return 1;

                }
                catch (Exception ex)
                {
                    return -1; ;
                }

            }
            catch (Exception ex)
            {
                return -1; 
            }
        }



        public DataTable GET_MESSAGE_EDIT(SWIFT_MSG_CONTENTInfo objTable)
        {
            try
            {
                string strSQL = "select CONTENT,CONTENT_ORIGIN,PROCESSSTS from  " + objTable.Table_Name + "  where QUERY_ID=" + objTable.QUERY_ID + "";
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                DataTable _dt = new DataTable();
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GET_PR_PROCESSSTS(string chrQUERY_ID)
        {
            try
            {
                string strSQL = "select PRE_PROCESSSTS,PROCESSSTS from  SWIFT_MSG_CONTENT  where QUERY_ID=" + chrQUERY_ID + "";
                strSQL = strSQL + " union select PRE_PROCESSSTS,PROCESSSTS from  SWIFT_MSG_ALL  where QUERY_ID=" + chrQUERY_ID + "";
                strSQL = strSQL + " union select PRE_PROCESSSTS,PROCESSSTS from  SWIFT_MSG_ALL_HIS  where QUERY_ID=" + chrQUERY_ID + "";
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                DataTable _dt = new DataTable();
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GET_PROCESSSTS(string chrQUERY_ID)
        {
            try
            {
                string strSQL = "select PROCESSSTS,MSG_SRC from  SWIFT_MSG_CONTENT  where QUERY_ID=" + chrQUERY_ID + "";
                strSQL = strSQL + " union select PROCESSSTS,MSG_SRC from  SWIFT_MSG_ALL  where QUERY_ID=" + chrQUERY_ID + "";
                strSQL = strSQL + " union select PROCESSSTS,MSG_SRC from  SWIFT_MSG_ALL_HIS  where QUERY_ID=" + chrQUERY_ID + "";
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                DataTable _dt = new DataTable();
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int INSERT_MESSAGE_EDIT(int iQuery_id, string pFIELD_CONTENT_ORIGIN,
            string pFIELD_CONTENT_EDIT, int vOrder)
        {
            try
            {                
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                string strSQL = "insert into SWIFT_MSG_EDIT(QUERY_ID,FIELD_CONTENT_ORIGIN,FIELD_CONTENT_EDIT,ORDER_BY) ";
                strSQL = strSQL + " values(:pQuery_id ,:pFIELD_CONTENT_ORIGIN,:pFIELD_CONTENT_EDIT,:pORDER_BY)";
                OracleParameter[] oraParam = { new OracleParameter("pQuery_id", OracleType.Number,20) ,
                                               new OracleParameter("pFIELD_CONTENT_ORIGIN", OracleType.VarChar,100),
                                               new OracleParameter("pFIELD_CONTENT_EDIT", OracleType.VarChar,100),
                                               new OracleParameter("pORDER_BY", OracleType.Number,3)};

                oraParam[0].Value = iQuery_id;
                oraParam[1].Value = pFIELD_CONTENT_ORIGIN;
                oraParam[2].Value = pFIELD_CONTENT_EDIT;
                oraParam[3].Value = vOrder;

                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
                oraConn.Close();
                oraConn.Dispose();
                return 1;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public DataTable GET_SWIFT_MSG_EDIT(int iQuery_id)
        {
            try
            {
                string strSQL = "select B.FIELD_CONTENT_ORIGIN,B.FIELD_CONTENT_EDIT from ( ";
                strSQL = strSQL + " select FIELD_CONTENT_ORIGIN,FIELD_CONTENT_EDIT,ORDER_BY from  SWIFT_MSG_EDIT  where QUERY_ID='" + iQuery_id + "'  ";
                strSQL = strSQL + " union ";
                strSQL = strSQL + " select FIELD_CONTENT_ORIGIN,FIELD_CONTENT_EDIT,ORDER_BY from  SWIFT_MSG_EDIT_ALL  where QUERY_ID='" + iQuery_id + "' )B ";
                strSQL = strSQL + " order by B.ORDER_BY ASC";

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                DataTable _dt = new DataTable();
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;

            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public int Resend_message_swift(string pDEPARTMENT, string pFIELD20, string pQUERY_ID, string pMSG_DIRECTION)
        {
            int iResult = 0;
            OracleParameter[] oraParam = {new OracleParameter("pDEPARTMENT", OracleType.Number,5),
                                         new OracleParameter("pFIELD20", OracleType.Number,20),
                                             new OracleParameter("pQUERY_ID", OracleType.Number,20),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.Number,20)
                                         };
            oraParam[0].Value = pDEPARTMENT;
            oraParam[1].Value = pFIELD20;
            oraParam[2].Value = pQUERY_ID;
            oraParam[3].Value = pMSG_DIRECTION;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.RESEND_MESSAGE_SWIFT", oraParam);
                return iResult;
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        /* Ten ham: Update_Print_STS
         * Mo ta: Ham cap nhat trang thai in dien
         * Ngay tao: 06/02/2010
         * Nguoi tao: Huypq7         
         */
        public int Update_Print_STS(SWIFT_MSG_CONTENTInfo objTable)
        {
            string strSql = "GW_PK_SWIFT_MSG_CONTENT.SWIFT_UPDATE_PRINT_STS";

            OracleParameter[] oraParam = {new OracleParameter("pPrint_STS", OracleType.Number,1),
                                         new OracleParameter("pQUERY_ID", OracleType.Number,20)};
            oraParam[0].Value = objTable.PRINT_STS;
            oraParam[1].Value = objTable.QUERY_ID;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        public int Resend_message_swift_tc(string pQUERY_ID, string pTable)
        {
            int iResult = 0;
            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.Number,5),
                                         new OracleParameter("pTable", OracleType.VarChar,20)
                                         };
            oraParam[0].Value = pQUERY_ID;
            oraParam[1].Value = pTable;
          
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.RESEND_MESSAGE_TC", oraParam);
                return iResult;
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        public int Update_swift_process(string pQUERY_ID, string pTellerid)
        {
            int iResult = 0;
            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.VarChar,10),
                                         new OracleParameter("pTELLER_ID", OracleType.VarChar,20)
                                         };
            oraParam[0].Value = pQUERY_ID;
            oraParam[1].Value = pTellerid;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_APPROVE.UPDATE_SWIFT_PROCESS", oraParam);
                return iResult;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public DataTable Load_process(string pQUERY_ID, string pTellerid)
        {
            int iResult = 0;
            string strSQL = "select QUERY_ID from SWIFT_PROCESS where QUERY_ID = '" + pQUERY_ID + "' and TELLER_ID <> '" + pTellerid + "'";
            DataTable _dt = new DataTable();
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Delete_swift_process(string pQUERY_ID)
        {
            int iResult = 0;
            string strSQL = "delete from SWIFT_PROCESS where QUERY_ID = '" + pQUERY_ID + "'";          
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                oraConn.Close();
                oraConn.Dispose();
                return iResult;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int DELETE_SWIFT_PROCESS_HANDER(string pMSG_ID, string TELLER_ID)
        {
            int iResult = 0;
            string strSQL = "delete from SWIFT_PROCESS_HANDER where MSG_ID = '" + pMSG_ID + "' and TELLER_ID = '" + TELLER_ID + "'";
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                oraConn.Close();
                oraConn.Dispose();
                return iResult;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int UPDATE_CLOSE_MESSAGE(string pPROCESSSTS,string pQUERY_ID, string pTABLENAME)
        {
            int iResult = 0;
            string strSQL = "";
            if (pTABLENAME=="SWIFT_MSG_CONTENT")
            {
                strSQL = "Update SWIFT_MSG_CONTENT set PROCESSSTS= '" + pPROCESSSTS + "' where QUERY_ID='" + pQUERY_ID + "'";
            }
            else if (pTABLENAME == "SWIFT_MSG_ALL")
            {
                strSQL = "Update SWIFT_MSG_ALL set PROCESSSTS= '" + pPROCESSSTS + "' where QUERY_ID='" + pQUERY_ID + "'";
            }
            else if (pTABLENAME == "SWIFT_MSG_ALL_HIS")
            {
                strSQL = "Update SWIFT_MSG_ALL_HIS set PROCESSSTS= '" + pPROCESSSTS + "' where QUERY_ID='" + pQUERY_ID + "'";
            }
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                oraConn.Close();
                oraConn.Dispose();
                return iResult;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public DataTable GET_MAP_FIELD(string chrMSG_TYPE)
        {
            int iResult = 0;
            string strSQL = "select FIELD_CODE from SWIFT_MAP_FIELD S where S.MSG_TYPE ='" + chrMSG_TYPE + "'";          
            try
            {
                DataTable _dt = new DataTable();
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable GET_SWIFT_PROCESS(string chrQUERY_ID)
        {
            int iResult = 0;
            string strSQL = "select QUERY_ID,TELLER_ID,New_Process from SWIFT_PROCESS where QUERY_ID= '" + chrQUERY_ID + "'";
            try
            {
                DataTable _dt = new DataTable();
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GET_SWIFT_PROCESS_HANDER(string chrMSG_ID)
        {
            int iResult = 0;
            string strSQL = "select TELLER_ID from swift_process_hander where MSG_ID= '" + chrMSG_ID + "' and New_processsts = 7";
            try
            {
                DataTable _dt = new DataTable();
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /*Ham lay dien cho xu ly thu cong dien di*/
        public DataSet SEARCH_DATA_MANUAL_NORMAL(string pWhere, out DataSet _dtContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),                                           
                                         new OracleParameter("pContent",OracleType.Cursor),                                         
                                         new OracleParameter("pColumns",OracleType.Cursor)
                                         };
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;           


            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.SWIFT_PROCESS_MANUAL", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }        

        public int CHECK_PROCESS(int pMSG_ID, int pQUERY_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return 1;
            }
            int vOut = 0;
            OracleParameter[] Oraparam = {new OracleParameter("pMSG_ID",OracleType.Number,10),                                           
                                         new OracleParameter("pQUERY_ID",OracleType.Number,10),
                                         new OracleParameter("pOUT",OracleType.Number,5)
                                         };

            Oraparam[0].Value = pMSG_ID;
            Oraparam[1].Value = pQUERY_ID;
            Oraparam[2].Direction = ParameterDirection.Output;


            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_PROCESS_HANDICRAFT.CHECK_PROCESS", Oraparam);
                vOut = Convert.ToInt32(Oraparam[2].Value.ToString());
                oraConn.Dispose();
                oraConn.Close();
                return vOut;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int CHECK_PROCESS_REPAIR(int pQUERY_ID, string pTELLERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return 1;
            }
            int vOut = 0;
            OracleParameter[] Oraparam = {new OracleParameter("pQUERY_ID",OracleType.Number,10),
                                         new OracleParameter("pTELLERID",OracleType.VarChar,20),
                                         new OracleParameter("pOUT",OracleType.Number,5)
                                         };
            
            Oraparam[0].Value = pQUERY_ID;
            Oraparam[1].Value = pTELLERID;
            Oraparam[2].Direction = ParameterDirection.Output;


            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_MSG_CONTENT.CHECK_PROCESS_REPAIR", Oraparam);
                vOut = Convert.ToInt32(Oraparam[2].Value.ToString());
                oraConn.Dispose();
                oraConn.Close();
                return vOut;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public int DELETE_PROCESS_REPAIR(int pQUERY_ID, string pTELLERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return 1;
            }            
            string strSQL = "Delete from swift_repair_process where QUERY_ID= " + pQUERY_ID + " and TELLER_ID ='" + pTELLERID + "'";
            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                
                oraConn.Dispose();
                oraConn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
