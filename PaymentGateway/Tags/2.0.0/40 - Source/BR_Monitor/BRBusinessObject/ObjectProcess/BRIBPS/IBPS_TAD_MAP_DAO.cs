using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BR.DataAccess;
using System.Data.OracleClient;

namespace BR.BRBusinessObject.ObjectProcess.BRIBPS
{
    public class IBPS_TAD_MAP_DP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        public static IBPS_TAD_MAP_DP Instance()
        {
            return new IBPS_TAD_MAP_DP();
        }

        public DataTable GetComboboxData()
        {
            oraConn = connect.Connect();
            if (oraConn == null) return null;
            DataTable datTable = new DataTable();
            string strSQL = "select T.GW_BANK_CODE,T.SIBS_CODE||' - ' ||TAD|| ' - '|| T.tad_name as TAD from TAD T";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }


        public DataTable GetData(string gwBankCode)
        {
            oraConn = connect.Connect();
            if (oraConn == null) return null;
            DataTable datTable = new DataTable();
            string strSQL = "select I.BANK_CODE,I.TAD_CODE,I.BANK_NAME,I.UPDATETIME,I.GW_BANK_CODE from IBPS_TAD_MAP I where GW_BANK_CODE='"+gwBankCode+"'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public int DELETE(string ID)
        {
            string strSql = "Delete from IBPS_TAD_MAP ITM where ITM.BANK_CODE='" + ID + "'";
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch
            {
                return -1; ;
            }
        }

        internal int ADD(BR.BRBusinessObject.ObjectInfo.BRIBPS.IBPS_TAD_MAP_Info ibps_tad_map_info)
        {
            string strSql = "Insert into IBPS_TAD_MAP(BANK_CODE,TAD_CODE,UPDATETIME,NOTE,BANK_NAME,GW_BANK_CODE) values (:pBANKCODE,:pTADCODE,:pUPDATETIME,:pNOTE,:pBANKNAME,:pGWBANKCODE)";
            OracleParameter[] oraParam = {new OracleParameter("pBANKCODE", OracleType.VarChar,3),
                                         new OracleParameter("pTADCODE", OracleType.VarChar,5),
                                         new OracleParameter("pUPDATETIME", OracleType.DateTime,20),
                                         new OracleParameter("pNOTE", OracleType.VarChar,200),
                                         new OracleParameter("pBANKNAME", OracleType.VarChar,200),
                                         new OracleParameter("pGWBANKCODE", OracleType.VarChar,8)};

            oraParam[0].Value = ibps_tad_map_info.BANK_CODE;
            oraParam[1].Value = ibps_tad_map_info.TAD_CODE;
            oraParam[2].Value = ibps_tad_map_info.UPDATETIME;
            oraParam[3].Value = ibps_tad_map_info.NOTE;
            oraParam[4].Value = ibps_tad_map_info.BANK_NAME;
            oraParam[5].Value = ibps_tad_map_info.GW_BANK_CODE;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch
            {
                return -1; ;
            }
        }

        internal int UPDATE(BR.BRBusinessObject.ObjectInfo.BRIBPS.IBPS_TAD_MAP_Info ibps_tad_map_info)
        {
            string strSql = "UPDATE IBPS_TAD_MAP SET TAD_CODE=:pTADCODE, UPDATETIME=:pUPDATETIME, NOTE=:pNOTE, BANK_NAME=:pBANKNAME, GW_BANK_CODE=:pGWBANKCODE WHERE BANK_CODE=:pBANKCODE";
            OracleParameter[] oraParam = {new OracleParameter("pTADCODE", OracleType.VarChar,5),
                                         new OracleParameter("pUPDATETIME", OracleType.DateTime,20),
                                         new OracleParameter("pNOTE", OracleType.VarChar,200),
                                         new OracleParameter("pBANKNAME", OracleType.VarChar,200),
                                         new OracleParameter("pBANKCODE", OracleType.VarChar,5),
                                         new OracleParameter("pGWBANKCODE", OracleType.VarChar,8)};

            
            oraParam[0].Value = ibps_tad_map_info.TAD_CODE;
            oraParam[1].Value = ibps_tad_map_info.UPDATETIME;
            oraParam[2].Value = ibps_tad_map_info.NOTE;
            oraParam[3].Value = ibps_tad_map_info.BANK_NAME;
            oraParam[4].Value = ibps_tad_map_info.BANK_CODE;
            oraParam[5].Value = ibps_tad_map_info.GW_BANK_CODE;

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch
            {
                return -1; ;
            }
        }
    }
}
