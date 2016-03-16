using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.OracleClient;
using BR.DataAccess;



namespace BR.BRBusinessObject
{
    public class TADMAPDP
    {
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();

        public TADMAPDP()
		{
		}
        public static TADMAPDP Instance()
		{
            return new TADMAPDP();
		}

        public int INSERT_TADMAP(TADMAPInfo ObjTable)
        {
            string strSql = "GW_PK_TADMAP.INSERT_TADMAP";
            OracleParameter[] oraPara = { new OracleParameter("pTAD", OracleType.VarChar,12),
                                        new OracleParameter("pTADHO", OracleType.VarChar,12),
                                        new OracleParameter("pNOTE", OracleType.VarChar,100)};
            oraPara[0].Value = ObjTable.TAD;
            oraPara[1].Value = ObjTable.TADHO;
            oraPara[2].Value = ObjTable.NOTE;            
            int i = 0;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                i = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraPara);
                oraConn.Close();
                oraConn.Dispose();
                return i;
            }
            catch
            {
                return -1;
            }
        }


        public DataSet LOAD_DATA(string pGW_BANK_CODE, string pSIBS_BANK_CODE)
        {
            string strSql = "GW_PK_TADMAP.GET_DATA";
            OracleParameter[] oraPara = { new OracleParameter("pGW_BANK_CODE", OracleType.VarChar,12),
                                          new OracleParameter("pSIBS_BANK_CODE", OracleType.VarChar,12),
                                        new OracleParameter("pTAD", OracleType.Cursor),                                        
                                        new OracleParameter("pTADMAP", OracleType.Cursor)};
            oraPara[0].Value = pGW_BANK_CODE;
            oraPara[1].Value = pSIBS_BANK_CODE;
            oraPara[2].Direction = ParameterDirection.Output;
            oraPara[3].Direction = ParameterDirection.Output;
            
            DataSet _ds = new DataSet();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraPara);
                _ds.Tables[0].TableName = "TAD";              
                _ds.Tables[1].TableName = "TADMAP";              

                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }

        public DataTable LOAD_COMBO()
        {
            string strSql = "GW_PK_TADMAP.LOAD_HO";
            OracleParameter[] oraPara = { new OracleParameter("vTADHO", OracleType.Cursor) };
            oraPara[0].Direction = ParameterDirection.Output;

            DataTable _dt = new DataTable();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraPara).Tables[0];               
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return null;
            }
        }

        public int DELETE_TADMAP(string pTADHO)
        {
            string strSql = "GW_PK_TADMAP.DELETE_TADMAP";
            OracleParameter[] oraPara = { new OracleParameter("pTADHO", OracleType.VarChar,12) };
            oraPara[0].Value = pTADHO;
            DataTable _dt = new DataTable();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraPara);
                oraConn.Close();
                oraConn.Dispose();
                return 1;
            }
            catch
            {
                return -1;
            }
        }

    }
}
