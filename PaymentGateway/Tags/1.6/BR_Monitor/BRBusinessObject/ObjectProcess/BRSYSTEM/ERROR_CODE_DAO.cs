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
    public class ERROR_CODEDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process connect = new Connect_Process();
        public ERROR_CODEDP()
        {
        }
        public static ERROR_CODEDP Instance()
        {
            return new ERROR_CODEDP();
        }
        public DataTable GET_ERROR_CODE(string strGWTYPE, out DataTable _dt)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dt = null;
            }
            OracleParameter[] Oraparam = {new OracleParameter("pGWTYPE",OracleType.VarChar,10)
                                         ,new OracleParameter("pE_RETURN",OracleType.Cursor)                                         
                                         };
            Oraparam[1].Direction = ParameterDirection.Output;
            Oraparam[0].Value = strGWTYPE;

            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS_ERROR_CODE.GET_ERROR_CODE", Oraparam).Tables[0];
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
    }
}
