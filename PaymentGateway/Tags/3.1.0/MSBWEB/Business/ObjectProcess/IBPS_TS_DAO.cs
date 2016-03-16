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
using System.Data.OracleClient;
using BIDVWEB.Comm;
using BIDVWEB.Comm.DA;

namespace BIDVWEB.Business
{
    public class IBPS_TS_DAO
    {
        private OracleConnection oraConn;
        private clsConnection objConn = new clsConnection();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        public string strError = "";

        public IBPS_TS_DAO()
		{

		}

        public static IBPS_TS_DAO Instance()
		{
            return new IBPS_TS_DAO();
		}

        public int Insert(IBPS_TS_Info objTable)
        {
            int iResult;            
            string strSql = "GW_PK_IBPS_TS.IBPS_TS_Insert";
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pCREATEDATE",OracleType.DateTime,7) ,
                                            new OracleParameter("pQUERY_ID",OracleType.Number,20) ,
                                            new OracleParameter("pBR_SEND",OracleType.VarChar,12),
                                            new OracleParameter("pBR_RECEIVE",OracleType.VarChar,12),
                                            new OracleParameter("pCONTENT_TS",OracleType.NVarChar,2000),
                                            new OracleParameter("pSTATUS",OracleType.Number,1),
                                            new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,10),
                                            new OracleParameter("pCONTENT",OracleType.NVarChar,2000),                                            
                                            new OracleParameter("pID_PARENT",OracleType.Number,20),
                                            new OracleParameter("pIORDER",OracleType.Number,2),
                                            new OracleParameter("pREFNO",OracleType.VarChar,20),
                                            new OracleParameter("pKSV_SEND",OracleType.VarChar,50),
                                            new OracleParameter("pKSV_RECEIVE",OracleType.VarChar,50),
                                            new OracleParameter("pSTSAPP",OracleType.Number,1),
                                            new OracleParameter("pSBT_ID",OracleType.VarChar,20)};
                                                
                oraParas[0].Value = objTable.CREATEDATE;
                oraParas[1].Value = objTable.QUERY_ID;
                oraParas[2].Value = objTable.BR_SEND;
                oraParas[3].Value = objTable.BR_RECEIVE;
                oraParas[4].Value = objTable.CONTENT_TS;                
                oraParas[5].Value = objTable.STATUS;
                oraParas[6].Value = objTable.MSG_DIRECTION;
                oraParas[7].Value = objTable.CONTENT;
                oraParas[8].Value = objTable.ID_PARENT;
                oraParas[9].Value = objTable.IORDER;
                oraParas[10].Value = objTable.REFNO;
                oraParas[11].Value = objTable.KSV_SEND;
                oraParas[12].Value = objTable.KSV_RECEIVE;
                oraParas[13].Value = objTable.STSAPP;
                oraParas[14].Value = objTable.SBT_ID;
                
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                iResult = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return -1;
            }
        }

        public int Update(IBPS_TS_Info objTable)
        {

            int iResult;
            string strSql = "GW_PK_IBPS_TS.IBPS_TS_Update";

            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pID",OracleType.Number,20) ,
                                            new OracleParameter("pUPDATEDATE",OracleType.DateTime,7) ,
                                            new OracleParameter("pSTATUS",OracleType.Number,1)};

                oraParas[0].Value = objTable.ID;
                oraParas[1].Value = objTable.UPDATEDATE;
                oraParas[2].Value = objTable.STATUS;

                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                iResult = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iResult;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
        }

        //Ham tim kiem du lieu theo dieu kien////////////////////////
        // Muc dich:    Ham tim kiem du lieu theo dieu kien
        // Ngay tao:    10/2010
        // Nguoi tao:   Huypq7
        // Dau vao:     strWhere: Cau dieu kien where        
        // Dau ra:      Du lieu
        /////////////////////////////////////////////////////////////                
        public DataSet SearchAdvance(string pWHERE, out DataSet _dtContent)
        {
            try
            {
                string strSql = "GW_PK_IBPS_TS.Search";
                oraConn = objConn.Connect();
                if (oraConn == null)
                {
                    return _dtContent = null;
                }
                OracleParameter[] Oraparam = {new OracleParameter("pWHERE",OracleType.VarChar),
                                         new OracleParameter("pContent",OracleType.Cursor)};
                Oraparam[0].Value = pWHERE;
                Oraparam[1].Direction = ParameterDirection.Output;
                return _dtContent = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, Oraparam); 
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return _dtContent = null;
            }
        }

        public DataSet ViewIBPS_TS(string pID, out DataSet _dtContent)
        {
            try
            {
                string strSql = "GW_PK_IBPS_TS.ViewIBPS_TS";
                oraConn = objConn.Connect();
                if (oraConn == null)
                {
                    return _dtContent = null;
                }
                OracleParameter[] Oraparam = {new OracleParameter("pID",OracleType.VarChar),
                                         new OracleParameter("pContent",OracleType.Cursor)};
                Oraparam[0].Value = pID;
                Oraparam[1].Direction = ParameterDirection.Output;
                return _dtContent = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, Oraparam);                
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return _dtContent = null;
            }
        }


        //Ham lay thong tin dien can tra soat
        public DataSet GetMSGByRM(string sRM, out DataSet _dtContent)
        {
            string strSql = "GW_PK_IBPS_TS.GetMSGByRM";
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                return _dtContent = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pwhere",OracleType.NVarChar),
                                         new OracleParameter("pContent",OracleType.Cursor)};
            Oraparam[0].Value = sRM;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {
                return _dtContent = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, Oraparam);
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;                
                return _dtContent = null;
            }
        }

        //Ham kiem tra mot dien tra soat theo ID_parent
        public int CheckSubMsg(long sID)
        {
            string strSql = "GW_PK_IBPS_TS.CheckSubMSg";
            DataSet ds = new DataSet();
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pID",OracleType.Number),
                                         new OracleParameter("pCursor",OracleType.Cursor)};
            Oraparam[0].Value = sID;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {
                ds = clsDataAcessComm.ExecuteDataset(oraConn,
                    CommandType.StoredProcedure, strSql, Oraparam);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
        }

        //Ham kiem tra mot dien tra soat da duyet?
        public int CheckMsgApprove(long sID)
        {
            string strSql = "GW_PK_IBPS_TS.CheckMsgApprove";
            DataSet ds = new DataSet();
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pID",OracleType.Number),
                                         new OracleParameter("pCursor",OracleType.Cursor)};
            Oraparam[0].Value = sID;
            Oraparam[1].Direction = ParameterDirection.Output;

            try
            {
                ds = clsDataAcessComm.ExecuteDataset(oraConn,
                    CommandType.StoredProcedure, strSql, Oraparam);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
        }       
        
        //Ham lay thong tin dien can tra soat
        public int GetMSGByRM(string sRM)
        {

            int iBool = -1;
            try
            {

                return iBool;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
        }

        //Ham lay so lenh tra soat kenh VCB theo ngay?
        public string GetSBT_ID_New()
        {
            string strSQL = "Select NVL(Max(to_number(substr(A.SBT_ID,5,9))),0) SBT_ID from IBPS_TS A " +
                "WHERE SUBSTR(A.SBT_ID,5,6)=TO_CHAR(SYSDATE,'DDMMYY')";
            string sSBT_ID = "";
            DataSet ds = new DataSet();

            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    if (ds.Tables[0].Rows[0]["SBT_ID"].ToString() != "0")
                        sSBT_ID = "IBPS" +
                        (Convert.ToInt32(ds.Tables[0].Rows[0]["SBT_ID"].ToString()) + 1).ToString().PadLeft(9, '0');
                    else
                        sSBT_ID = "IBPS" + DateTime.Now.Day.ToString().PadLeft(2, '0') +
                            DateTime.Now.Month.ToString().PadLeft(2, '0') +
                        DateTime.Now.Year.ToString().Substring(2, 2) + "001";
                else
                    sSBT_ID = "IBPS" + DateTime.Now.Day.ToString().PadLeft(2, '0') +
                        DateTime.Now.Month.ToString().PadLeft(2, '0') +
                        DateTime.Now.Year.ToString().Substring(2, 2) + "001";
                return sSBT_ID;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }
    }
}
