using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using BR.DataAccess;
namespace BR.BRBusinessObject
{
   public class MSG_FIELDDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

       public MSG_FIELDDP()
        {
        }
        public static MSG_FIELDDP Instance()
        {
            return new MSG_FIELDDP();
        }


       // public DataSet GetMSG_FIELD(string pMSGType, string pGWTYPE)
       //{
       //    oraConn = connect.Connect();
       //    if (oraConn == null)
       //    {
       //        return null;
       //    }
       //    DataTable datTable = new DataTable();
       //    string strSQL = "SELECT distinct "+ pMSGType +" FROM  MSG_FIELD where GWTYPE='"+ pGWTYPE +"'";

       //    try
       //    {
       //        return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
       //    }
       //    catch 
       //    {
       //        return null;
       //    }
       //}

        public DataSet GetGWTYPE(string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            //DataTable datTable = new DataTable();
            string strSQL = "select distinct(G.GWTYPE) from MSG_FIELD G where upper(Trim(g.gwtype))='" + pGWTYPE + "' order by G.GWTYPE asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetMSGType(string pMsgType)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            //DataTable datTable = new DataTable();
            string strSQL = "select * from MSG_FIELD G where upper(Trim(g.MSG_TYPE))='" + pMsgType + "' order by G.MSG_TYPE asc";

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
