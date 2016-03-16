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
    public class clsSearchIBPS
    {
        public string strError = "";
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private OracleConnection oraConn = new OracleConnection();
        private clsConnection connect = new clsConnection();

        
        //Ham tim kiem du lieu theo dieu kien////////////////////////
        // Muc dich:    Ham tim kiem du lieu theo dieu kien
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strWhere:   Cau dieu kien where khong co truong
        //                          FromDate, ToDate 
        //              pDateFrom:  Kieu DateTime
        //              pDateTo:    Kieu DateTime 
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////        
        //public DataSet SearchAdvance(string pWHERE, DateTime pDateFrom, DateTime pDateTo)        
        public DataSet SearchAdvance(string pWHERE, string pDateFrom, string pDateTo)        
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsData = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                string strSQL = "";
                
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }                            

                strSQL = "GW_PK_IBPS_SEARCH.Search_IBPS";                

                OracleParameter[] oraParas1 ={new OracleParameter("pFROMDATE",OracleType.VarChar),
                                             new OracleParameter("pTODATE",OracleType.VarChar),
                                             new OracleParameter("pWHERE",OracleType.VarChar),
                                             new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                oraParas1[0].Value = pDateFrom;
                oraParas1[1].Value = pDateTo;
                oraParas1[2].Value = pWHERE;
                oraParas1[3].Value = "IBPS_MSG_CONTENT";                
                oraParas1[4].Direction = ParameterDirection.Output;
                ds1 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas1);
                OracleParameter[] oraParas2 ={new OracleParameter("pFROMDATE",OracleType.VarChar),
                                             new OracleParameter("pTODATE",OracleType.VarChar),
                                             new OracleParameter("pWHERE",OracleType.VarChar),
                                             new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                oraParas2[0].Value = pDateFrom;
                oraParas2[1].Value = pDateTo;
                oraParas2[2].Value = pWHERE;
                oraParas2[3].Value = "IBPS_MSG_ALL";
                oraParas2[4].Direction = ParameterDirection.Output;
                ds2 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas2);                
                OracleParameter[] oraParas3 ={new OracleParameter("pFROMDATE",OracleType.VarChar),
                                             new OracleParameter("pTODATE",OracleType.VarChar),
                                             new OracleParameter("pWHERE",OracleType.VarChar),
                                             new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                oraParas3[0].Value = pDateFrom;
                oraParas3[1].Value = pDateTo;
                oraParas3[2].Value = pWHERE;
                oraParas3[3].Value = "IBPS_MSG_ALL_HIS";
                oraParas3[4].Direction = ParameterDirection.Output;
                ds3 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas3);

                if (ds1 != null && ds2 != null)
                {
                    ds = AddDataset(ds1, ds2);
                }
                else if (ds1 == null && ds2 == null)
                {
                    ds = null;
                }
                else
                {
                    if (ds1 == null)
                        ds = ds2;
                    else if (ds2 == null)
                        ds = ds1;
                }
                if (ds != null && ds3 != null)
                {
                    dsData = AddDataset(ds, ds3);
                }
                else if (ds == null && ds3 == null)
                {
                    dsData = null;
                }
                else
                {
                    if (ds == null)
                        dsData = ds3;
                    else if (ds3 == null)
                        dsData = ds;
                }

                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                if (dsData == null)
                    return null;
                else
                    return dsData;
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

        //Ham add du lieu tu 1 table vao 1 table trong dataset///////
        // Muc dich:    
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     dsData: Dataset 1
        //              ds:     Dataset 2
        // Dau ra:      Dataset moi
        /////////////////////////////////////////////////////////////                
        private DataSet AddDataset(DataSet dsData, DataSet ds)
        {            
            try
            {
                DataRow drData;

                if (dsData == null || dsData.Tables[0].Rows.Count==0)
                {
                    if (ds == null || ds.Tables[0].Rows.Count == 0)
                        return null;
                    else
                        return ds;
                }
                else
                {
                    if (ds == null || ds.Tables[0].Rows.Count == 0)
                        return dsData;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        drData = ds.Tables[0].Rows[j];
                        DataRow newRow = dsData.Tables[0].NewRow();
                                        
                        newRow["MSG_ID"] = drData["MSG_ID"];
                        newRow["QUERY_ID"] = drData["QUERY_ID"];
                        newRow["RM_NUMBER"] = drData["RM_NUMBER"];
                        //newRow["TRANS_NUM"] = drData["TRANS_NUM"];
                        //newRow["SENDER"] = drData["SENDER"];
                        //newRow["RECEIVER"] = drData["RECEIVER"];
                        //newRow["SOURCE_BRANCH"] = drData["SOURCE_BRANCH"];
                        newRow["AMOUNT"] = drData["AMOUNT"];
                        newRow["CCYCD"] = drData["CCYCD"];
                        //newRow["STATUS"] = drData["STATUS"];
                        //newRow["TRANS_DATE"] = drData["TRANS_DATE"];
                        //newRow["TRANS_CODE"] = drData["TRANS_CODE"];
                        //newRow["FILE_NAME"] = drData["FILE_NAME"];
                        //newRow["MSG_DIRECTION"] = drData["MSG_DIRECTION"];
                        newRow["GW_TRANS_NUM"] = drData["GW_TRANS_NUM"];
                        //newRow["SIBS_TRANS_NUM"] = drData["SIBS_TRANS_NUM"];
                        newRow["NHGUI"] = drData["NHGUI"];
                        newRow["NHNHAN"] = drData["NHNHAN"];
                        //newRow["ERR_CODE"] = drData["ERR_CODE"];
                        //newRow["TRANS_DESCRIPTION"] = drData["TRANS_DESCRIPTION"];
                        //newRow["DEPARTMENT"] = drData["DEPARTMENT"];
                        //newRow["TAD"] = drData["TAD"];
                        //newRow["PRE_TAD"] = drData["PRE_TAD"];
                        //newRow["TELLERID"] = drData["TELLERID"];
                        //newRow["PRETRAN_CODE"] = drData["PRETRAN_CODE"];
                        //newRow["PRETRANS_NUM"] = drData["PRETRANS_NUM"];
                        //newRow["FORWARDSTS"] = drData["FORWARDSTS"];
                        //newRow["FWSTS"] = drData["FWSTS"];
                        //newRow["FWTIME"] = drData["FWTIME"];
                        //newRow["RECEIVING_TIME"] = drData["RECEIVING_TIME"];
                        //newRow["SENDING_TIME"] = drData["SENDING_TIME"];
                        dsData.Tables[0].Rows.Add(newRow);
                    }
                    return dsData;
                }
            }
            catch (Exception ex)
            {
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
        public DataSet GetMsgIBPSByID(string sMsg_id)
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

                strSQL = "GW_PK_IBPS_SEARCH.SearchIBPSByMsgID";
                OracleParameter[] oraParas1 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                
                oraParas1[0].Value = "IBPS_MSG_CONTENT";
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

                    oraParas2[0].Value = "IBPS_MSG_ALL";
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

                        oraParas3[0].Value = "IBPS_MSG_ALL_HIS";
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
