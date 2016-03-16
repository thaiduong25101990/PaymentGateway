using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using BIDVWEB.Comm.DA;

namespace BIDVWEB.Business.SearchMessage
{
    public class clsSearchSwift
    {
        public string strError = "";
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private OracleConnection oraConn = new OracleConnection();
        private clsConnection connect = new clsConnection();
                

        //Ham tim kiem du lieu theo dieu kien////////////////////////
        // Muc dich:    Ham tim kiem du lieu theo dieu kien
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strWhere: Cau dieu kien where khong co truong
        //                        FromDate, ToDate 
        //              pDateFrom: Kieu DateTime
        //              pDateTo:   Kieu DateTime 
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////
        public DataSet SearchAdvance(string pWHERE, string pDateFrom, string pDateTo)
        {
            try
            {
                DataSet ds = new DataSet();
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                ////DIEN DI
                //if (pWHERE.IndexOf("SIBS-SWIFT") < 0)                
                //{
                //    pWHERE = pWHERE + " AND STATUS=1 ";                    
                //}
                string strSQL = "GW_PK_SWIFT_SEARCH.Search_SWIFT";
                OracleParameter[] oraParas ={new OracleParameter("pFROMDATE",OracleType.VarChar),
                                             new OracleParameter("pTODATE",OracleType.VarChar),
                                             new OracleParameter("pWHERE",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                oraParas[0].Value = pDateFrom;
                oraParas[1].Value = pDateTo;
                oraParas[2].Value = pWHERE;
                oraParas[3].Direction = ParameterDirection.Output;
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return null;
            }
        }

        //Ham lay thong tin dien theo QUERY_ID///////////////////////
        // Muc dich:    Ham lay thong tin dien theo QUERY_ID
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:             
        // Dau ra:      
        /////////////////////////////////////////////////////////////
        public DataSet GetMsgSWIFTByID(string sMsg_id)
        {
            try
            {
                string strSQL = "";
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }

                strSQL = "GW_PK_SWIFT_SEARCH.SearchSWIFTByMsgID";
                OracleParameter[] oraParas1 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                oraParas1[0].Value = "SWIFT_MSG_CONTENT";
                oraParas1[1].Value = sMsg_id;
                oraParas1[2].Direction = ParameterDirection.Output;
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas1);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    OracleParameter[] oraParas2 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                    oraParas2[0].Value = "SWIFT_MSG_ALL";
                    oraParas2[1].Value = sMsg_id;
                    oraParas2[2].Direction = ParameterDirection.Output;
                    ds1 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas2);
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        return ds1;
                    }
                    else
                    {
                        OracleParameter[] oraParas3 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                        oraParas3[0].Value = "SWIFT_MSG_ALL_HIS";
                        oraParas3[1].Value = sMsg_id;
                        oraParas3[2].Direction = ParameterDirection.Output;
                        ds2 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas3);
                        if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                        {
                            return ds2;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

    }
}
