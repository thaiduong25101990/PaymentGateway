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
                return _dtContent = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_TTSP_MSG_CONTENT.TTSP_MSG_CONTENT_LOAD", Oraparam);
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
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output;
            Oraparam[3].Direction = ParameterDirection.Output;
            Oraparam[4].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pWhere;
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
                string strSQL = " gw_pk_ttsp_msg_content.get_msgdtl_rp ";

                //pmsg_id => :pmsg_id,
                //                       pmsg_type => :pmsg_type,
                //                       pmsgdirection => :pmsgdirection


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
            catch
            {
                return null;
            }
        }

        //het nang cap



    }
}
