/*---------------------------------------------------------------
        * Muc dich         : Tao cac ham cap nhat tham so kiem tra dien ve tu Swift:dien Oldkey/Failure/Duplicate 
        * Ngay tao         : 18/06/2008
        * Nguoi tao        : Hantt10
 *--------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.DataAccess;
using System.Data.OracleClient;
using System.Data;

namespace BR.BRBusinessObject
{
   public class SWIFT_IWCKHDP
    {
       private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();

        public SWIFT_IWCKHDP()
		{
		}
        public static SWIFT_IWCKHDP Instance()
		{
            return new SWIFT_IWCKHDP();
		}

        public int AddSWIFT_IWCKH(SWIFT_IWCKHInfo objTable)
        {
            string strSql = "Insert into swift_iwchk (FIELD,VAULE,DESCRIPTIONS) values (:pFIELD,:pVAULE,:pDESCRIPTIONS)";
            OracleParameter[] oraParam = {new OracleParameter("pFIELD", OracleType.NVarChar,5),
                                         new OracleParameter("pVAULE", OracleType.NVarChar,10),
                                         new OracleParameter("pDESCRIPTIONS", OracleType.NVarChar, 100)};
            oraParam[0].Value = objTable.FIELD;
            oraParam[1].Value = objTable.VAULE;
            oraParam[2].Value = objTable.DESCRIPTIONS;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1; ;
            }
        }

        public int UpdateSWIFT_IWCKH(SWIFT_IWCKHInfo objTable)
        {
            DataTable datTable = new DataTable();
            string strSQL = "Update swift_iwchk set DESCRIPTIONS =:pDESCRIPTIONS where FIELD = :pFIELD and VAULE=:pVAULE";
            OracleParameter[] oraParam = {new OracleParameter("pFIELD", OracleType.NVarChar,5),
                                         new OracleParameter("pVAULE", OracleType.NVarChar,10),
                                         new OracleParameter("pDESCRIPTIONS", OracleType.NVarChar, 100)};
            oraParam[0].Value = objTable.FIELD;
            oraParam[1].Value = objTable.VAULE;
            oraParam[2].Value = objTable.DESCRIPTIONS;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public int DeleteSWIFT_IWCKH(string iID,string strValue)
        {
            string strSql = "Delete from swift_iwchk where FIELD = :pFIELD AND VAULE = :pVALUE ";
            OracleParameter[] oraParam = { new OracleParameter("pFIELD", OracleType.NVarChar, 5),
                                            new OracleParameter("pVALUE", OracleType.NVarChar,10)};
            oraParam[0].Value = iID;
            oraParam[1].Value = strValue;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataSet GetSWIFT_IWCKH()
        {
            DataTable datTable = new DataTable();
            string strSql = "Select * from swift_iwchk";
            try
            {
                oraConn = conn.Connect();
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
