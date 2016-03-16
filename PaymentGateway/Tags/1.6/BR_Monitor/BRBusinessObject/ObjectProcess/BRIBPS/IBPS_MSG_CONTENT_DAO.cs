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
//' Update      :   Nguyen duc quy 27/05/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class IBPS_MSG_CONTENTDP
    {
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process connect = new Connect_Process();        
        public IBPS_MSG_CONTENTDP()
        {
        }
        public static IBPS_MSG_CONTENTDP Instance()
        {
            return new IBPS_MSG_CONTENTDP();
        }

        //quynd
        public DataSet IBPS_CONTENT_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_MSG_CONTENT_SEARCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet IBPS_CONTENT_SEARCH_RS(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_MSG_CONTENT_SEARCH_RS", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet IBPS_CONTENT_SEARCH_ADVANCE( string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_MSG_CONTENT_ADVANCE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet IBPS_CONTENT_SEARCH_ADVANCE_RS(string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_MSG_CONTENT_ADVANCE_RS", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //lay dien cho forward cong citad
        public DataSet FORWARD_LOAD(out DataSet _dtContent,string pTELLERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pContent",OracleType.Cursor),
                                             new OracleParameter("pTELLERID",OracleType.NVarChar,8)
                                         };
            Oraparam[0].Direction = ParameterDirection.Output;
            Oraparam[1].Value = pTELLERID;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_FORWARD_BRANCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //lay dien cho forward 
        public DataSet FREVIOUS_SEARCH(out DataSet _dtContent, string pUserid,string pDate,string pWhere)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pDate",OracleType.VarChar,50),
                                             new OracleParameter("pUserid",OracleType.VarChar,20),
                                             new OracleParameter("pContent",OracleType.Cursor),
                                             new OracleParameter("pAll",OracleType.Cursor),
                                             new OracleParameter("pWhere",OracleType.VarChar,2000)
                                         };
            Oraparam[0].Value = pDate;
            Oraparam[1].Value = pUserid;
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Value = pWhere;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_PREVIOUS_SEARCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }
        //DatHM
        public DataTable FREVIOUS_PRINT(string pUserid, string pDate, string pWhere)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pDate",OracleType.VarChar,50),
                                             new OracleParameter("pUserid",OracleType.VarChar,20),
                                             new OracleParameter("pContent",OracleType.Cursor),
                                             new OracleParameter("pAll",OracleType.Cursor),
                                             new OracleParameter("pWhere",OracleType.VarChar,2000)
                                         };
            Oraparam[0].Value = pDate;
            Oraparam[1].Value = pUserid;
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Value = pWhere;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_PREVIOUS_PRINT", Oraparam).Tables[0] ;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable FREVIOUS_PRINT_LOAD(string pUserid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            OracleParameter[] Oraparam = {
                                             new OracleParameter("pUserid",OracleType.VarChar,20),
                                             new OracleParameter("pContent",OracleType.Cursor),
                                             new OracleParameter("pAll",OracleType.Cursor)
                                         };
            
            Oraparam[0].Value = pUserid;
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_PREVIOUS_PRINT_LOAD", Oraparam).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        //End DatHM
        //lay dien cho forward cong citad
        public DataSet FREVIOUS_LOAD(out DataSet _dtContent,string pUserid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pUserid",OracleType.VarChar,20),
                                             new OracleParameter("pContent",OracleType.Cursor),
                                             new OracleParameter("pAll",OracleType.Cursor)
                                         };
            Oraparam[0].Value =pUserid;
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_PREVIOUS_LOAD", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //lay dien cho forward cong citad
        public DataSet FORWARD_CURRENT_LOAD(out DataSet _dtContent,string pUserid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pUserid",OracleType.VarChar,20),
                                             new OracleParameter("pContent",OracleType.Cursor)
                                         };
            Oraparam[0].Value = pUserid;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_CURRENT_DAY_LOAD", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //lay dien cho forward cong citad
        public DataSet FORWARD_SEARCH_CURRENT(out DataSet _dtContent, string pWhere,string pUserid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pUserid",OracleType.VarChar,20),
                                             new OracleParameter("pWhere",OracleType.VarChar,2000),
                                             new OracleParameter("pContent",OracleType.Cursor)
                                         };
            Oraparam[0].Value = pUserid;
            Oraparam[1].Value = pWhere;
            Oraparam[2].Direction = ParameterDirection.Output;

            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_CURRENT_DAY_SEACH_HO", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        //lay dien cho forward cong citad
        public DataSet FORWARD_SEARCH(out DataSet _dtContent, string pWhere, string pTELLERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,2000),
                                             new OracleParameter("pContent",OracleType.Cursor),
                                             new OracleParameter("pTELLERID",OracleType.NVarChar)
                                         };
            Oraparam[0].Value = pWhere;
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Value = pTELLERID;
            try
            {
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_SEARCH_FORWARD_BRANCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }
        //quynd
        public DataSet IBPS_CONTENT_LOAD(out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_MSG_CONTENT", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet IBPS_CONTENT_LOAD_RESEND(out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.IBPS_MSG_CONTENT_RESEND", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataTable GetIBPS_MSG_DTL(long lMSG_ID)
        {
            try
            {
                DataTable tblReturn = new DataTable();
                OracleParameter[] oraPara = { new OracleParameter("pMSG_ID", OracleType.Number,20),
                                              new OracleParameter("pcurIBPSDTL", OracleType.Cursor)} ;
                oraPara[0].Value = lMSG_ID;
                oraPara[1].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.GET_IBPS_DTL", oraPara).Tables[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        //datHM
        public DataTable GetData_print_ibps(string strMsgID, string pBranch,string strUserid)
        {
            try
            {

                string strSQL = " GW_PK_IBPS_REPORT.IBPS_PRINT_MSG ";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pBranch", OracleType.VarChar,20),
                                              new OracleParameter("pMSG_ID", OracleType.Number,20),
                                              new OracleParameter("pUser", OracleType.VarChar,50),
                                              new OracleParameter("pcurBM_IBPS_MSG", OracleType.Cursor)};
                oraPara[0].Value = pBranch;
                oraPara[1].Value = strMsgID;
                oraPara[2].Value = strUserid;
                oraPara[3].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraPara).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void LoadDataSearch(string tableName, DataGridView dgvSearch, string tad)
        {
            string strSql = "select imc.MSG_ID,imc.QUERY_ID,imc.MSG_DIRECTION,imc.TRANS_CODE,imc.SENDER,imc.RECEIVER,imc.TRANS_DATE,imc.AMOUNT,imc.CCYCD,imc.STATUS,imc.ERR_CODE,imc.DEPARTMENT,imc.CONTENT,imc.TAD,imc.PRE_TAD  from IBPS_MSG_CONTENT imc Where imc.STATUS =1 and TAD='" + tad + "' order by imc.TRANS_DATE desc";
            oraConn = connect.Connect();
            OracleCommand cmdSelect = new OracleCommand(strSql, oraConn);
            OracleDataAdapter da = new OracleDataAdapter(cmdSelect);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, tableName);
            dt = ds.Tables[tableName];
            dgvSearch.DataSource = dt;
        }

        //public int UPDATE_Statement(string pMSG_ID)
        //{
        //    oraConn = connect.Connect();
        //    if (oraConn == null)
        //    {
        //        return -1;
        //    }
        //    string strSQL = "update SWIFT_MSG_CONTENT   set statement_id = 0 where msg_id='" + pMSG_ID + "'";
        //    try
        //    {
        //        return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
        //    }
        //    catch //(Exception ex)
        //    {
        //        return -1;
        //    }
        //}

        //DatHM Add cho muc dich in list dien IBPS

        public int AddIBPS_Q_Dblink_Out(IBPS_MSG_CONTENTInfo objTable,string m_vGW_BANK_CODE)
        {
            string strSql = "GW_PK_FORWARD.Forward_MSG_OUT";
            int ibool;

            OracleParameter[] oraParam = {
                                   new OracleParameter("pQueryID", OracleType.Number,10),
                                   new OracleParameter("pTad", OracleType.VarChar,5),
                                   new OracleParameter("pReTad", OracleType.VarChar,5),
                                   new OracleParameter("pHL_Val", OracleType.Number,5),
                                   new OracleParameter("pTellerID", OracleType.VarChar,20),
                                   new OracleParameter("pGW_BANK_CODE", OracleType.VarChar,8)};

            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.TAD;
            oraParam[2].Value = objTable.PRE_TAD;
            oraParam[3].Value = objTable.HV_LV;
            oraParam[4].Value = objTable.TELLERID;
            oraParam[5].Value = m_vGW_BANK_CODE;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                ibool= DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
                if (ibool != 1)
                    return -1;
                else
                {
                    objTable.PRINT_STS = 0;
                    return Update_Print_STS(objTable);
                }
            }
            catch //(Exception ex)
            {
                return -1; ;
            }
        }


        public int Forward_LV_HV(IBPS_MSG_CONTENTInfo objTable)
        {
            string strSql = "GW_PK_FORWARD.Forward_LowValue";

            OracleParameter[] oraParam = {new OracleParameter("pMSG_ID", OracleType.Number,10),
                                         new OracleParameter("pTellerID", OracleType.VarChar,20)};
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Value = objTable.TELLERID;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1; ;
            }
        }

        public int InsertIBPS_MSG_CONTENT_Temp(DateTime dtDate, string strType)
        {
            string strSQL = "BEGIN ";
            // Tim kiem theo bang CONTENT
            if (dtDate.Date == DateTime.Now.Date)
            {
                strSQL += "INSERT INTO IBPS_MSG_CONTENT_TEMP(msg_id,department,rm_number,msg_direction,trans_date,rec_type,status) ";
                strSQL += "(SELECT msg_id,department,LPAD(rm_number,19,'0') rm_number,msg_direction,trans_date,'SIBS-BR',status FROM IBPS_MSG_CONTENT WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND ((msg_direction='SIBS-IBPS' and status>-2)or (msg_direction='IBPS-SIBS' and status>-2)));";
            }
            // Tim kiem theo bang ALL
            if (dtDate.Month == DateTime.Now.Month)
            {
                strSQL += "INSERT INTO IBPS_MSG_CONTENT_TEMP(msg_id,department,rm_number,msg_direction,trans_date,rec_type,status) ";
                strSQL += "(SELECT msg_id,department,LPAD(rm_number,19,'0') rm_number,msg_direction,trans_date,'SIBS-BR',status FROM IBPS_MSG_ALL WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND ((msg_direction='SIBS-IBPS' and status>-2)or (msg_direction='IBPS-SIBS' and status>-2)));";
            }
            // Tim kiem theo bang ALL_HIS
            else
            {
                strSQL += "INSERT INTO IBPS_MSG_CONTENT_TEMP(msg_id,department,rm_number,msg_direction,trans_date,rec_type,status) ";
                strSQL += "(SELECT msg_id,department,LPAD(rm_number,19,'0') rm_number,msg_direction,trans_date,'SIBS-BR',status FROM IBPS_MSG_ALL_HIS WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND ((msg_direction='SIBS-IBPS' and status>-2)or (msg_direction='IBPS-SIBS' and status>-2)));";
            }
            strSQL += " END;";
            //strSQL = strSQL + "(SELECT department,rm_number,msg_direction FROM IBPS_MSG_CONTENT WHERE to_char(TRANS_DATE,'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "' AND STATUS <> -1)";
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
        
        ///***************************************
        // * Insert dien vao bang Content_Temp_TAD
        // ***************************************/
        public int InsertIBPS_MSG_CONTENT_Temp_TAD(DateTime dtDate, string strDepartment, string strTAD, string strGW_BANK_CODE, string strSIBS_BANK_CODE)
        {
            // strTAD like {'990','314'}

            string strSQL = "";
            // Quet toan bo dien IBPS khong loi~ ung voi TAD = SIBS_BANK_CODE or GW_BANK_CODE cua cong TAD ma co su dung DBLink
            // Dien di dua vao SIBS_BANK_CODE
            // Dien ve dua vao GW_BANK_CODE
            strSQL = " BEGIN ";
            strSQL += " INSERT INTO IBPS_MSG_CONTENT_TAD";
            strSQL += " (MSG_ID, department, GW_TRANS_NUM, GW_BANK_CODE, msg_direction, TAD, K1,trans_date,status) ";
            strSQL += " (SELECT MSG_ID,";
            strSQL += " department,";
            strSQL += " GW_TRANS_NUM,";
            strSQL += " '" + strGW_BANK_CODE + "',";
            strSQL += " msg_direction,";
            strSQL += " TAD,";
            strSQL += " to_char(TRANS_DATE, 'YYYYMMDD')||LPAD(GW_TRANS_NUM,6,'0') K1";
            strSQL += " ,trans_date,status";
            strSQL += " FROM IBPS_MSG_CONTENT ";// CONTENT
            strSQL += " WHERE to_char(TRANS_DATE, 'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "'";
            strSQL += " AND LTRIM(TAD,'0') = '" + strTAD + "'";
            strSQL += " AND STATUS>-2);";

            if (dtDate.Month == DateTime.Now.Month)
            {
                strSQL += " INSERT INTO IBPS_MSG_CONTENT_TAD";
                strSQL += " (MSG_ID, department, GW_TRANS_NUM, GW_BANK_CODE, msg_direction, TAD, K1,trans_date,status) ";
                strSQL += " (SELECT MSG_ID,";
                strSQL += " department,";
                strSQL += " GW_TRANS_NUM,";
                strSQL += " '" + strGW_BANK_CODE + "',";
                strSQL += " msg_direction,";
                strSQL += " TAD,";
                strSQL += " to_char(TRANS_DATE, 'YYYYMMDD')||LPAD(GW_TRANS_NUM,6,'0') K1";
                strSQL += " ,trans_date,status";
                strSQL += " FROM IBPS_MSG_ALL ";// ALL
                strSQL += " WHERE to_char(TRANS_DATE, 'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "'";
                strSQL += " AND LTRIM(TAD,'0') = '" + strTAD + "'";
                strSQL += " AND STATUS>-2);";
            }
            else
            {
                strSQL += " INSERT INTO IBPS_MSG_CONTENT_TAD";
                strSQL += " (MSG_ID, department, GW_TRANS_NUM, GW_BANK_CODE, msg_direction, TAD, K1,trans_date,status) ";
                strSQL += " (SELECT MSG_ID,";
                strSQL += " department,";
                strSQL += " GW_TRANS_NUM,";
                strSQL += " '" + strGW_BANK_CODE + "',";
                strSQL += " msg_direction,";
                strSQL += " TAD,";
                strSQL += " to_char(TRANS_DATE, 'YYYYMMDD')||LPAD(GW_TRANS_NUM,6,'0') K1";
                strSQL += " ,trans_date,status";
                strSQL += " FROM IBPS_MSG_ALL_HIS ";// ALL_HIS
                strSQL += " WHERE to_char(TRANS_DATE, 'YYYYMMDD') = '" + dtDate.ToString("yyyyMMdd") + "'";
                strSQL += " AND LTRIM(TAD,'0') = '" + strTAD + "'";
                strSQL += " AND STATUS>-2);";
            }
            strSQL += " End;";
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

        ///****************************************
        // * Insert dien tham gia doi chieu vao trong bang REC_TAD
        // * + Xu li gan nhan cong TAD { Non TAD: =XX }
        // ****************************************/ 
        public int InsertIBPS_MSG_REC_TAD(DateTime dtDate, string strTAD, string strDBLINK_NAME, string strDBLINK_HO)
        {
            /*******************************************************
             * Ki thuat bam nho cau lenh Insert = cac truong Option
             *******************************************************/

            // HV + Out: TAD = F21{IBPS}            
            string
             strSQL = "BEGIN insert into ibps_msg_rec_tad(sibs_trans_num  ,   hlv,	 sender,	 trans_date,	 receiver,	 org_bank,";
            strSQL += "	 receiving_branch,	 ccycd,	 amount,	 sender_name,	 sender_address,	 sender_account,	 receiver_name,";
            strSQL += "	 receiver_address,	 receiver_account,	 description,	 msg_direction,	 tad,	 k1)";
            strSQL += "	(select ho.F117,					ho.HLV,					ho.SENDER,					ho.TRANS_DATE,					ho.RECEIVER";
            strSQL += "	       ,ho.ORG_BANK      ,ho.RECEIVING_BRANCH   ,ho.CCYCD   ,ho.AMOUNT  ,ho.SENDER_NAME ,ho.SENDER_ADDRESS";
            strSQL += "				 ,ho.SENDER_ACCOUNT,ho.RECEIVER_NAME      ,ho.RECEIVER_ADDRESS    ,ho.RECEIVER_ACCOUNT";
            strSQL += "				 ,ho.DESCRIPTION   ,ho.DIRECTION          ,ho.TAD     ,mp1.k2 k1";
            strSQL += "	from (select F117,F3 HLV ,F7 SENDER,to_date(F102, 'YYYYMMDD') TRANS_DATE,F19 RECEIVER,F21 ORG_BANK";
            strSQL += "	       ,F22 RECEIVING_BRANCH,F26 CCYCD,F27 AMOUNT,F28 SENDER_NAME,F29 SENDER_ADDRESS,F30 SENDER_ACCOUNT";
            strSQL += "				 ,F31 RECEIVER_NAME,F32 RECEIVER_ADDRESS,F33 RECEIVER_ACCOUNT,F34 DESCRIPTION,substr(F35,1,1)DIRECTION";
            strSQL += "				 ,'" + strTAD + "' TAD     ,(f102 || f11 || f21) k1";
            strSQL += "				 from tblhhv11d@" + strDBLINK_HO + "	where f102 = '" + dtDate.ToString("yyyyMMdd") + "') ho,(select substr(mp.f12, 1, 8)||f110 k2,substr(mp.f12, 1, 8) || mp.f11 || mp.f21 k1";
            strSQL += "				 from TBLTRANSACTIONMSG_ORG@" + strDBLINK_NAME + " mp	where substr(mp.f12, 1, 8) = '" + dtDate.ToString("yyyyMMdd") + "' and f3='201001' ) mp1";
            strSQL += "				 where mp1.k1 = ho.k1);";
            strSQL += "";
            strSQL += "";


            // LV + Out: TAD = F21{IBPS}
            strSQL += " insert into ibps_msg_rec_tad(sibs_trans_num  ,   hlv,	 sender,	 trans_date,	 receiver,	 org_bank,";
            strSQL += "	 receiving_branch,	 ccycd,	 amount,	 sender_name,	 sender_address,	 sender_account,	 receiver_name,";
            strSQL += "	 receiver_address,	 receiver_account,	 description,	 msg_direction,	 tad,	 k1)";
            strSQL += "	(select ho.F117,					ho.HLV,					ho.SENDER,					ho.TRANS_DATE,					ho.RECEIVER";
            strSQL += "	       ,ho.ORG_BANK      ,ho.RECEIVING_BRANCH   ,ho.CCYCD   ,ho.AMOUNT  ,ho.SENDER_NAME ,ho.SENDER_ADDRESS";
            strSQL += "				 ,ho.SENDER_ACCOUNT,ho.RECEIVER_NAME      ,ho.RECEIVER_ADDRESS    ,ho.RECEIVER_ACCOUNT";
            strSQL += "				 ,ho.DESCRIPTION   ,ho.DIRECTION          ,ho.TAD     ,mp1.k2 k1";
            strSQL += "	from (select F117,F3 HLV ,F7 SENDER,to_date(F102, 'YYYYMMDD') TRANS_DATE,F19 RECEIVER,F21 ORG_BANK";
            strSQL += "	       ,F22 RECEIVING_BRANCH,F26 CCYCD,F27 AMOUNT,F28 SENDER_NAME,F29 SENDER_ADDRESS,F30 SENDER_ACCOUNT";
            strSQL += "				 ,F31 RECEIVER_NAME,F32 RECEIVER_ADDRESS,F33 RECEIVER_ACCOUNT,F34 DESCRIPTION,substr(F35,1,1)DIRECTION";
            strSQL += "				 ,'" + strTAD + "' TAD     ,(f102 || f11 || f21) k1";
            strSQL += "				 from tblhlv11d@" + strDBLINK_HO + "	where f102 = '" + dtDate.ToString("yyyyMMdd") + "') ho,(select substr(mp.f12, 1, 8)||f110 k2,substr(mp.f12, 1, 8) || mp.f11 || mp.f21 k1";
            strSQL += "				 from TBLTRANSACTIONMSG_ORG@" + strDBLINK_NAME + " mp	where substr(mp.f12, 1, 8) = '" + dtDate.ToString("yyyyMMdd") + "'  and f3='101001') mp1";
            strSQL += "				 where mp1.k1 = ho.k1);";
            strSQL += " END;";
            strSQL += "";

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

        public int DeleteIBPS_MSG_CONTENT_TAD()
        {
            string strSql = "DELETE FROM IBPS_MSG_CONTENT_TAD";

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

        public DataTable GetIBPS_MSG_CONTENT_CONTENT(string strMsgID, string pTable_name)
        {
            try
            {
                string strSQL = "select IMC.MSG_ID,IMC.CONTENT  from  " + pTable_name + " IMC where trim(IMC.MSG_ID)='" + strMsgID + "'";
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        ////end DatHM
        public string Get_HV_LV(string pBCode, double pAmount)
        {
            string strReturn = "";
            try
            {                
                OracleParameter[] oraParas ={new OracleParameter("pvBCode",OracleType.VarChar,5),
                                                new OracleParameter("pnAmount",OracleType.Number,20),
                                                 new OracleParameter("pHValue",OracleType.VarChar,10)
                                            };
                oraParas[0].Value = pBCode;
                oraParas[1].Value = pAmount;
                oraParas[2].Direction = ParameterDirection.Output;
                oraParas[2].Value = "conkhi";
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_Process.IBPS_TRGetTransCode", oraParas);
                strReturn = oraParas[2].Value.ToString();
                return strReturn;
            }
            catch //(Exception ex)
            {
                return "";
            }
        }


        /* Ten ham: Update_Print_STS
         * Mo ta: Ham cap nhat trang thai in dien
         * Ngay tao: 06/02/2010
         * Nguoi tao: Huypq7         
         */
        public int Update_Print_STS(IBPS_MSG_CONTENTInfo objTable)
        {
            string strSql = "GW_PK_IBPS_Process.IPBS_UPDATE_PRINT_STS";

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
                return -1; ;
            }
        }

        public int Resend_message_ibps(string pQUERY_ID, string pMSG_DIRECTION)
        {
            int iResult = 0;
            OracleParameter[] oraParam = {new OracleParameter("pQUERY_ID", OracleType.VarChar,20),
                                         new OracleParameter("pMSG_DIRECTION", OracleType.VarChar,20)
                                         };
            oraParam[0].Value = pQUERY_ID;
            oraParam[1].Value = pMSG_DIRECTION;          
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.RESEND_MESSAGE_IBPS", oraParam);
                return iResult;
            }
            catch (Exception ex)
            {
                return -1; ;
            }
        }

    }
}
