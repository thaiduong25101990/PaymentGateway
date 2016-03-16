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

//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class IBPS_BANK_TADDP
	{
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

		public IBPS_BANK_TADDP()
		{
		}
		public static IBPS_BANK_TADDP Instance()
		{
			return new IBPS_BANK_TADDP();
		}

        //Ham them moi 1 dong vao bang IBPS_BANK_TAD
        public int AddIBPS_BANK_TAD(IBPS_BANK_TADInfo objTable)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Insert into IBPS_BANK_TAD(BANK_MAP_ID,GW_BANK_CODE,TAD_ID,MAIN,TAD) VALUES ";
            strSQL = strSQL + "(:bank_map_id,:pgw_bank_code,:ptad_id,:pmain,:ptad)";
            OracleParameter[] oraParam = { new OracleParameter(":pbank_map_id", OracleType.Int32, 5),
                                         new OracleParameter(":pgw_bank_code", OracleType.NVarChar, 20),
                                         new OracleParameter(":ptad_id", OracleType.NVarChar, 20),
                                         new OracleParameter(":pmain", OracleType.NVarChar, 20),
                                         new OracleParameter(":ptad ", OracleType.NVarChar, 20)};
            oraParam[0].Value = objTable.BANK_MAP_ID;
            oraParam[1].Value = objTable.GW_BANK_CODE;
            oraParam[2].Value = objTable.TAD_ID;
            oraParam[3].Value = objTable.MAIN;
            oraParam[4].Value = objTable.TAD;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        //Ham cap nhat vao bang IBPS_BANK_TAD
        public int UpdateIBPS_BANK_TAD(IBPS_BANK_TADInfo objTable)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update IBPS_BANK_TAD set BANK_MAP_ID = :bank_map_id, GW_BANK_CODE = :pgw_bank_code,TAD_ID = :ptad_id, ";
                   strSQL = strSQL + "MAIN = :pmain, TAD = :ptad Where BANK_MAP_ID = '" + objTable.BANK_MAP_ID + "'";
            
            OracleParameter[] oraParam = { new OracleParameter(":pbank_map_id", OracleType.Int32, 5),
                                         new OracleParameter(":pgw_bank_code", OracleType.NVarChar, 20),
                                         new OracleParameter(":ptad_id", OracleType.NVarChar, 20),
                                         new OracleParameter(":pmain", OracleType.NVarChar, 20),
                                         new OracleParameter(":ptad ", OracleType.NVarChar, 20)};
            oraParam[0].Value = objTable.BANK_MAP_ID;
            oraParam[1].Value = objTable.GW_BANK_CODE;
            oraParam[2].Value = objTable.TAD_ID;
            oraParam[3].Value = objTable.MAIN;
            oraParam[4].Value = objTable.TAD;
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public int DeleteIBPS_BANK_TAD(double BANK_MAP_ID)
		{
            DataTable datTable = new DataTable();
            string strSql = "Delete from IBPS_BANK_TAD where BANK_MAP_ID = '" + BANK_MAP_ID + "'";
			try
			{
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
			}
			catch// (Exception ex)
			{
				return -1;
			}
		}

        public DataSet GetIBPS_BANK_TAD(double BANK_MAP_ID)
        {
            DataTable datTable = new DataTable();
            string strSql = "Select * from IBPS_BANK_TAD where BANK_MAP_ID = '" + BANK_MAP_ID + "'";
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
    }
}

