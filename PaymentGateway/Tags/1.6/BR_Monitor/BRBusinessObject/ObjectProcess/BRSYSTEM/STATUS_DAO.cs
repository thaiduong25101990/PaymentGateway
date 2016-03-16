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
    public class STATUSDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process connect = new Connect_Process();
        public STATUSDP()
        {
        }
        public static STATUSDP Instance()
        {
            return new STATUSDP();
        }
        public DataTable GET_STATUS(string pGWTYPE,out DataTable _dt)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dt = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pGWTYPE",OracleType.VarChar,10)
                                         ,new OracleParameter("pS_RETURN",OracleType.Cursor)                                         
                                         };
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[0].Value = pGWTYPE;

            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS_ERROR_CODE.GET_STATUS", Oraparam).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch //(Exception ex)
            {
                oraConn.Close();
                oraConn.Dispose();
                return _dt = null;
            }
        }

        //ham lay ca hai trang thai status va error_code
        public DataSet STATUS_ERROR_CODE(string pGWTYPE, out DataSet _ds)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _ds = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("rSTATUS",OracleType.Cursor)
                                         ,new OracleParameter("rERROR_CODE",OracleType.Cursor) 
                                         ,new OracleParameter("rFWSTS",OracleType.Cursor) 
                                         ,new OracleParameter("rPRINT_STS",OracleType.Cursor) 
                                         };
            Oraparam[0].Direction = ParameterDirection.Output;
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[2].Direction = ParameterDirection.Output; 
            Oraparam[3].Direction = ParameterDirection.Output; 
            try
            {
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.STATUS_ERROR_CODE", Oraparam);
                _ds.Tables[0].TableName = "STATUS";
                _ds.Tables[1].TableName = "ERROR_CODE";
                _ds.Tables[2].TableName = "FWSTS";
                _ds.Tables[3].TableName = "PRINT_STS";

                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch //(Exception ex)
            {
                oraConn.Close();
                oraConn.Dispose();
                return _ds = null;
            }

        }

        //ham lay trang thai Processsts cua xu ly thu cong voi menh de where truyen vao
        public DataTable SWIFT_STATUS(string pWhere, out DataTable _dt)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return _dt = null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pWHERE",OracleType.VarChar,2000),
                                              new OracleParameter("rSTATUS",OracleType.Cursor)
                                         };
                orapra[0].Value = pWhere;
                orapra[1].Direction = ParameterDirection.Output;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.MANUAL_STATUS", orapra).Tables[0];
                _dt.TableName = "STATUS";
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return _dt = null;
            }
        }

    }
}
