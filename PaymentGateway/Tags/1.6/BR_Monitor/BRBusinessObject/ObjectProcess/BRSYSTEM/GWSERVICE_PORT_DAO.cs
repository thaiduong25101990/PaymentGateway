using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using BR.DataAccess;

namespace BR.BRBusinessObject
{
    public class GWSERVICE_PORTDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
		public GWSERVICE_PORTDP()
		{
		}
		public static GWSERVICE_PORTDP Instance()
		{
			return new GWSERVICE_PORTDP();
		}
		
		public int AddGWSERVICE_PORT(GWSERVICE_PORTInfo objTable)
		{
            string strSql = "Insert into GWSERVICE_PORT (SERVICENAME,SIBSIP,PORTIN,PORTOUT,FILETYPE,DESCRIPTION,TIMEDELAY) values (:pSERVICENAME,:pSIBSIP,:pPORTIN,:pPORTOUT,:pFILETYPE,:pDESCRIPTION,:pTIMEDELAY)";
            OracleParameter[] oraParam = {
                                         new OracleParameter("pSERVICENAME", OracleType.NVarChar, 30),
                                         new OracleParameter("pSIBSIP", OracleType.NVarChar, 30),
                                         new OracleParameter("pPORTIN", OracleType.NVarChar, 10),
                                         new OracleParameter("pPORTOUT", OracleType.NVarChar, 10),
                                         new OracleParameter("pFILETYPE", OracleType.Char, 1),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar , 255),
                                         new OracleParameter("pTIMEDELAY", OracleType.Number, 10)};

            oraParam[0].Value = objTable.SERVICENAME;
            oraParam[1].Value = objTable.SIBSIP;
            oraParam[2].Value = objTable.PORTIN;
            oraParam[3].Value = objTable.PORTOUT;
            oraParam[4].Value = objTable.FILETYPE;
            oraParam[5].Value = objTable.DESCRIPTION;
            oraParam[6].Value = objTable.TIMEDELAY;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1;;
            }
		}
		
		public int UpdateGWSERVICE_PORT(GWSERVICE_PORTInfo objTable)
		{
            string strSql = "Update GWSERVICE_PORT  set ID=:pID,SERVICENAME=:pSERVICENAME,";
            strSql = strSql + " SIBSIP=:pSIBSIP,PORTIN=:pPORTIN,";
            strSql = strSql + " PORTOUT=:pPORTOUT,FILETYPE=:pFILETYPE,DESCRIPTION=:pDESCRIPTION,TIMEDELAY=:pTIMEDELAY";
            strSql = strSql + " where ID=:pID";
            OracleParameter[] oraParam = {new OracleParameter("pID", OracleType.Number, 10),
                                         new OracleParameter("pSERVICENAME", OracleType.NVarChar, 30),
                                         new OracleParameter("pSIBSIP", OracleType.NVarChar, 30),
                                         new OracleParameter("pPORTIN", OracleType.NVarChar, 10),
                                         new OracleParameter("pPORTOUT", OracleType.NVarChar, 10),
                                         new OracleParameter("pFILETYPE", OracleType.Char, 1),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar , 255),
                                         new OracleParameter("pTIMEDELAY", OracleType.Number , 10)};

            oraParam[0].Value = objTable.ID;
            oraParam[1].Value = objTable.SERVICENAME;
            oraParam[2].Value = objTable.SIBSIP;
            oraParam[3].Value = objTable.PORTIN;
            oraParam[4].Value = objTable.PORTOUT;
            oraParam[5].Value = objTable.FILETYPE;
            oraParam[6].Value = objTable.DESCRIPTION;
            oraParam[7].Value = objTable.TIMEDELAY;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                 return -1;
            }
		}

        public int DeleteGWSERVICE_PORT(int iID)
		{
            string strSql = "Delete from GWSERVICE_PORT where ID=:pID";
            OracleParameter[] oraParam = { new OracleParameter("pID", OracleType.NVarChar, 10) };
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

        public DataSet GetGWSERVICE_PORT()
        {
            DataSet datDs = new DataSet();
            string strSql = "Select gsp.ID,gsp.SERVICENAME,gsp.SIBSIP,gsp.PORTIN,gsp.PORTOUT,gsp.FILETYPE,alStatus.content,gsp.TIMEDELAY,gsp.DESCRIPTION from GWSERVICE_PORT gsp ,(select alc.cdval, alc.content, alc.cdname from allcode alc Where alc.cdname = 'FileType') alStatus where gsp.FILETYPE = alStatus.cdval(+) order by gsp.SERVICENAME";            
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
