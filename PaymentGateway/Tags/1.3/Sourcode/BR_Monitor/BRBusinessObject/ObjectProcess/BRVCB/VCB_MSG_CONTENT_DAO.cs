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
    public class VCB_MSG_CONTENTDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        public VCB_MSG_CONTENTDP()
        {
        }
        public static VCB_MSG_CONTENTDP Instance()
        {
            return new VCB_MSG_CONTENTDP();
        }

        // Nang cap

        public DataSet VCB_CONTENT_LOAD(out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_VCB_MSG_CONTENT.VCB_MSG_CONTENT_LOAD", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet VCB_CONTENT_LOAD_RESEND(out DataSet _dtContent)
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
                _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_VCB_MSG_CONTENT.VCB_MSG_CONTENT_LOAD_RESEND", Oraparam);
                oraConn.Close();
                oraConn.Dispose();
                return _dtContent;
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }



        //quynd
        public DataSet VCB_CONTENT_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_VCB_MSG_CONTENT.VCB_MSG_CONTENT_SEARCH", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }

        public DataSet VCB_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_VCB_MSG_CONTENT.VCB_MSG_CONTENT_ADVANCE", Oraparam);
            }
            catch //(Exception ex)
            {
                return _dtContent = null;
            }
        }


        //nang cap----------------------------------------------------------------------






        public DataTable GetVCB_MSG_CONTENT_CONTENT(string strMsgID, string pTable_name)
        {
            try
            {
                string strSQL = "GW_PK_VCB_MSG_CONTENT.GET_MESSAGE_ONE";
                OracleParameter[] oraParas ={new OracleParameter("pMSG_ID",OracleType.Number,20),
                                            new OracleParameter("pcurcontent",OracleType.Cursor)
                                                };
                oraParas[0].Value = strMsgID;
                oraParas[1].Direction = ParameterDirection.Output;

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


        public DataTable GetData_print_vcb(string strMsgID, string strMSG_TYPE, 
            string strMSGDIRECTION, string strBranch, string strUserid)
        {
            try
            {
                //string strSQL = " gw_pk_VCB_msg_content.get_msgdtl_rp ";
                string strSQL = " GW_PK_VCB_REPORT.VCB_PRINT_MSG ";

                //pmsg_id => :pmsg_id,
                //                       pmsg_type => :pmsg_type,
                //                       pmsgdirection => :pmsgdirection


                OracleParameter[] oraParas ={new OracleParameter("pMSG_ID",OracleType.Number,20),
                                            new OracleParameter("pmsg_type",OracleType.VarChar,20),
                                            new OracleParameter("pmsgdirection",OracleType.VarChar,20),
                                            new OracleParameter("pBranch",OracleType.VarChar,12),
                                            new OracleParameter("pUser",OracleType.VarChar,50),
                                            new OracleParameter("pcurcontent",OracleType.Cursor)};

                oraParas[0].Value = strMsgID;
                oraParas[1].Value = strMSG_TYPE;
                oraParas[2].Value = strMSGDIRECTION;
                oraParas[3].Value = strBranch;
                oraParas[4].Value = strUserid;
                oraParas[5].Direction = ParameterDirection.Output;

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

        public int Resend_message_vcb(string pQUERY_ID, string pMSG_DIRECTION)
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
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_VCB_MSG_CONTENT.RESEND_MESSAGE_VCB", oraParam);
                return iResult;
            }
            catch (Exception ex)
            {
                return -1; ;
            }
        }

        /* Ten ham: Update_Print_STS
         * Mo ta: Ham cap nhat trang thai in dien
         * Ngay tao: 06/02/2010
         * Nguoi tao: Huypq7         
         */
        public int Update_Print_STS(VCB_MSG_CONTENTInfo objTable)
        {
            string strSql = "GW_PK_VCB_MSG_CONTENT.VCB_UPDATE_PRINT_STS";

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
       
        
       
    }
}
