using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.DataAccess;
using System.Data;
using System.Data.OracleClient;

namespace BR.BRBusinessObject
{
   public class SWIFT_NRECDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process conn = new Connect_Process();
        public string strError = "";

        public SWIFT_NRECDP()
		{
		}
        public static SWIFT_NRECDP Instance()
		{
            return new SWIFT_NRECDP();
		}
        public int AddSWIFT_NREC(SWIFT_NRECInfo objTable)
        {
            string strSql = "Insert into SWIFT_NREC (MSG_TYPE) values (:pMSG_TYPE)";
            OracleParameter[] oraParam = {//new OracleParameter("pMSG_ID", OracleType.Number, 10),
                                         new OracleParameter("pMSG_TYPE", OracleType.NVarChar,8)
                                         };
            //oraParam[0].Value = objTable.MSG_ID;
            oraParam[0].Value = objTable.MSG_TYPE;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1; ;
            }
        }

        public int DeleteSWIFT_NREC(int iID)
        {
            string strSql = "Delete from SWIFT_NREC where MSG_ID=:pMSG_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number, 10) };
            oraParam[0].Value = iID;
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

         ////////////////////////////////////////////////////////////
         // Muc dich:    Sua xau strSql gay loi khi click chuot vao 
         //              header load form lan dau         
         // Ngay sua:    31/07/2008
         // Nguoi sua:   Huypq
         // Dau vao:     
         // Dau ra:      
         ////////////////////////////////////////////////////////////
        public DataSet GetSWIFT_NREC()
        {
            DataTable datTable = new DataTable();
            //string strSql = "Select ibm.MSG_ID,substr(ibm.MSG_TYPE,3,3) from SWIFT_NREC ibm";
            string strSql = "Select ibm.MSG_ID,substr(ibm.MSG_TYPE,3,3) AS MSG_TYPE from SWIFT_NREC ibm";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        ////////////////////////////////////////////////////////////
        // Muc dich:    
        // Ngay sua:    31/07/2008
        // Nguoi sua:   Huypq
        // Dau vao:     
        // Dau ra:      
        ////////////////////////////////////////////////////////////
        public DataSet GetSWIFT_NRECSearch(string MSGType)
        {
            DataTable datTable = new DataTable();
            //string strSql = "select MSG_TYPE from SWIFT_NREC where MSG_TYPE like '%" + MSGType + "%'";
            string strSql = "select MSG_ID,substr(MSG_TYPE,3,3) AS MSG_TYPE " +
                            " from SWIFT_NREC where substr(MSG_TYPE,3,3) like '%" + MSGType + "%'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }
    }
}
