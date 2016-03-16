using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BR.DataAccess;
using System.Data.OracleClient;

namespace BR.BRBusinessObject
{
   public class GWTYPE_DETAILDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public GWTYPE_DETAILDP()
		{
		}
        
        public static GWTYPE_DETAILDP Instance()
		{
            return new GWTYPE_DETAILDP();
		}

        public int AddGWTYPE_DETAIL(GWTYPE_DETAILInfo objTable)
       {
           string strSql = "GW_PK_GWTYPE_DETAIL.INSERT_UPDATE_GWTYPE_DTL";
           OracleParameter[] oraParam = {new OracleParameter("pID", OracleType.Number, 10),
                                         new OracleParameter("pGWTYPE", OracleType.NVarChar, 10),
                                         new OracleParameter("pFOLDER", OracleType.NVarChar, 100),
                                         new OracleParameter("pDIRECTION", OracleType.Number, 1),
                                         new OracleParameter("pFLDTYPE", OracleType.Number, 1),
                                         new OracleParameter("pFTPPATH", OracleType.NVarChar, 100),
                                         new OracleParameter("pFTPUSER", OracleType.NVarChar, 30),
                                         new OracleParameter("pFTPPASS", OracleType.NVarChar, 30),
                                         new OracleParameter("pFILETYPE", OracleType.VarChar, 10),
                                         new OracleParameter("pFTPSERVER", OracleType.VarChar, 100),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar , 255)};

           oraParam[0].Value = objTable.ID;
           oraParam[1].Value = objTable.GWTYPE;
           oraParam[2].Value = objTable.FOLDER;
           oraParam[3].Value = objTable.DIRECTION;
           oraParam[4].Value = objTable.FLDTYPE;
           oraParam[5].Value = objTable.FTPPATH;
           oraParam[6].Value = objTable.FTPUSER;
           oraParam[7].Value = objTable.FTPPASS;
           if (objTable.FILETYPE == "ALL")
               oraParam[8].Value = null;
           else
               oraParam[8].Value = objTable.FILETYPE;

           oraParam[9].Value = objTable.FTPSERVER;
           oraParam[10].Value = objTable.DESCRIPTION;

            try
           {
               oraConn = conn.Connect();
               if (oraConn == null)
                   return -1;
               return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
           }
           catch (Exception ex)
           {
               throw ex ;
           }
       }
        
        public int UpdateGWTYPE_DETAIL(GWTYPE_DETAILInfo objTable)
        {
            string strSql = "Update GWTYPE_DETAIL  set GWTYPE=:pGWTYPE,";
            strSql = strSql + " FOLDER=:pFOLDER,DIRECTION=:pDIRECTION,";
            strSql = strSql + " FLDTYPE=:pFLDTYPE,FTPPATH=:pFTPPATH, ";
            strSql = strSql + " FTPUSER=:pFTPUSER,FTPPASS=:pFTPPASS,FILETYPE=:pFILETYPE,DESCRIPTION=:pDESCRIPTION,FTPSERVER=:pFTPSERVER";
            strSql = strSql + " where ID=:pID";
            OracleParameter[] oraParam = {
                                         new OracleParameter("pID", OracleType.Number),
                                         new OracleParameter("pGWTYPE", OracleType.NVarChar, 10),
                                         new OracleParameter("pFOLDER", OracleType.NVarChar, 100),
                                         new OracleParameter("pDIRECTION", OracleType.Number, 1),
                                         new OracleParameter("pFLDTYPE", OracleType.Number, 1),
                                         new OracleParameter("pFTPPATH", OracleType.NVarChar, 100),
                                         new OracleParameter("pFTPUSER", OracleType.NVarChar, 30),
                                         new OracleParameter("pFTPPASS", OracleType.NVarChar, 30),
                                         new OracleParameter("pFILETYPE", OracleType.VarChar, 1),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar , 255),
                                         new OracleParameter("pFTPSERVER", OracleType.NVarChar, 50)
                                         };
            oraParam[0].Value = objTable.ID;
            oraParam[1].Value = objTable.GWTYPE;
            oraParam[2].Value = objTable.FOLDER;
            oraParam[3].Value = objTable.DIRECTION;
            oraParam[4].Value = objTable.FLDTYPE;
            oraParam[5].Value = objTable.FTPPATH;
            oraParam[6].Value = objTable.FTPUSER;
            oraParam[7].Value = objTable.FTPPASS;
            oraParam[8].Value = objTable.FILETYPE;
            oraParam[9].Value = objTable.DESCRIPTION;
            oraParam[10].Value = objTable.FTPSERVER;
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

        public DataSet GetGWTYPE_DETAIL(string GWType)
        {
            DataSet datDs = new DataSet();
            string strSql = "GW_PK_GWTYPE_DETAIL.GET_GWTYPE_DTL";
            OracleParameter[] oraParam = {new OracleParameter("pCreturn", OracleType.Cursor),
                                         new OracleParameter("pGWTPE", OracleType.NVarChar, 10)};

            oraParam[0].Direction = ParameterDirection.Output;
            oraParam[1].Value = GWType;

                try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch 
            {
                return null;
            }
        }

        public bool DeleteGWTYPE_DETAIL(string strGWType)
        {
            string strSql = " delete from gwtype_detail WHERE ID = '" + strGWType + "'";           
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return false;
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
                return true; 
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteGWTYPE_DETAIL1(string strGWType)
        {
            string strSql = " delete from gwtype_detail WHERE ID in (" + strGWType + ")";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return false;
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public DataSet GetGWTYPE_DETAILExist(string strGWType)
        {
            string strSql = " select *  from gwtype_detail WHERE Trim(GWTYPE) = '" + strGWType + "'"; 
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                //return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataTable GWTYPE_SELECT(int pID)
        {
            string strSql = " select *  from gwtype_detail WHERE ID = " + pID + "";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                //return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
    }
}
