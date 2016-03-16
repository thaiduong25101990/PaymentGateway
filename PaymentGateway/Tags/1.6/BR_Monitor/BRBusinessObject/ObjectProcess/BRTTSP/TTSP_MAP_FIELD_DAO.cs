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
//' Author:	Pham Van Dong
//' Create date:	02/02/2012
//' Description:
//' Revise History:
//' =============================================

namespace BR.BRBusinessObject
{
    public class TTSP_MAP_FIELD
    {
         private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        public TTSP_MAP_FIELD()
		{
		}

        public static TTSP_MAP_FIELD Instance()
		{
            return new TTSP_MAP_FIELD();
		}
        public DataSet GetMapFields(string strMsgType)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "SELECT * FROM TTSP_MAP_FIELD  WHERE MSG_TYPE='" + strMsgType + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
        }


    }
}
