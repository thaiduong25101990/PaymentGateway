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
using System.Data.OracleClient;
//using BR.BRLib;
using BR.DataAccess;

//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GWTYPEDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
		public GWTYPEDP()
		{
		}
		public static GWTYPEDP Instance()
		{
			return new GWTYPEDP();
		}
		
		public int AddGWTYPE(GWTYPEInfo objTable)
		{
            string strSql = "GW_PK_GWTYPE.INSERT_GWTYPE";
            OracleParameter[] oraParam = {new OracleParameter("pGWTYPE", OracleType.VarChar, 10),
                                         new OracleParameter("pCONECTION", OracleType.Number, 1),
                                         new OracleParameter("pGWTYPESTS", OracleType.Number, 1),
                                         new OracleParameter("pMSG_NO", OracleType.Number, 3),
                                         new OracleParameter("pDESCRIPTION", OracleType.VarChar , 255),
                                         new OracleParameter("pDBLINK", OracleType.VarChar , 255),
                                         new OracleParameter("pcheck", OracleType.Int32 , 3)};

            oraParam[0].Value = objTable.GWTYPE;
            oraParam[1].Value = objTable.CONNECTION;
            oraParam[2].Value = objTable.GWTYPESTS;
            oraParam[3].Value = objTable.MSG_NO;
            oraParam[4].Value = objTable.DESCRIPTION;
            oraParam[5].Value = objTable.DBLINK;
            oraParam[6].Direction = ParameterDirection.Output;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
                    return Convert.ToInt32(oraParam[6].Value);
            }
            catch //(Exception ex)
            {
                return -1;;
            }
		}
		
		public int UpdateGWTYPE(GWTYPEInfo objTable)
		{



            string strSql = "GW_PK_GWTYPE.UPDATE_GWTYPE";
            OracleParameter[] oraParam = {new OracleParameter("pGWTYPEID", OracleType.Number, 10),
                                         new OracleParameter("pGWTYPE", OracleType.VarChar, 10),
                                         new OracleParameter("pCONECTION", OracleType.Number, 1),
                                         new OracleParameter("pGWTYPESTS", OracleType.Number, 1),
                                         new OracleParameter("pMSG_NO", OracleType.Number, 3),
                                         new OracleParameter("pDESCRIPTION", OracleType.VarChar , 255),
                                         new OracleParameter("pDBLINK", OracleType.VarChar , 255),
                                         new OracleParameter("pcheck", OracleType.Int32 , 3)};
            oraParam[0].Value = objTable.GWTYPEID;
            oraParam[1].Value = objTable.GWTYPE;
            oraParam[2].Value = objTable.CONNECTION;
            oraParam[3].Value = objTable.GWTYPESTS;
            oraParam[4].Value = objTable.MSG_NO;
            oraParam[5].Value = objTable.DESCRIPTION;
            oraParam[6].Value = objTable.DBLINK;
            oraParam[7].Direction = ParameterDirection.Output;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
                return Convert.ToInt32(oraParam[7].Value);
            }
            catch 
            {
                 return -1;
            }
		}

        public int DeleteGWTYPE(int iID)
		{

            string strSql = "Delete from GWTYPE where GWTYPEID=:pGWTYPEID and GWTYPESTS = 2";
            //string strSql = "Delete from GWTYPE where GWTYPEID=:pGWTYPEID";
            OracleParameter[] oraParam = { new OracleParameter("pGWTYPEID", OracleType.NVarChar, 10) };
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

        public DataSet GetGWTYPE()
        {
            DataSet datDs = new DataSet();

            string strSql = "GW_PK_GWTYPE.GET_GWTYPE_ALL";

            try
            {
                OracleParameter[] oraParam = { new OracleParameter("vCReturn", OracleType.Cursor) };
                oraParam[0].Direction = ParameterDirection.Output;
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        //public DataSet GetGWTYPESearch(string strSQL)
        public DataSet GetGWTYPESearch(string strSQL)
        {
            DataSet datDs = new DataSet();
            string strSql = "GW_PK_GWTYPE.GET_GWTYPE_SEARCH";

            try
            {
                OracleParameter[] oraParam = {new OracleParameter("vCReturn", OracleType.Cursor) ,
                                             new OracleParameter("pCondition", OracleType.VarChar,2000) };
                oraParam[0].Direction = ParameterDirection.Output;
                oraParam[1].Value = strSQL;
                oraConn = conn.Connect();

                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
           

        public DataSet GetGWTYPEName()
        {
            DataSet datDs = new DataSet();
            string strSql = "Select distinct gwt.GWTYPE from Allcode gwt order by gwt.GWTYPE asc";
            //string strSql = "select alc.content GWType from allcode alc where alc.cdname='GWTYPE';";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetGWTYPE_GROUP(string type)
        {
            DataSet datDs = new DataSet();
            string strSql = "select type.GWTYPE_ID,type.GWTYPE_NAME,type.IMPORTPATH,type.EXPORTPATH,type.ID,type.ENCRYPT,type.ENCRYPTFUNCTION,type.DECRYPTFUNCTION,type.CONNECTION,type.PARAMETER from GWTYPE type where type.GWTYPE_ID='" + type + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                    return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGwtype()
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select G.GWTYPE from GWTYPE G order by G.GWTYPE asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetGwtype(string pGWTYPE)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select G.GWTYPE from GWTYPE G where Trim(g.gwtype)='" + pGWTYPE + "' order by G.GWTYPE asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public bool CheckChannelData(string strGWTYPE)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return false;
            }
            string strSQL_ALL = "select count(*) from " + strGWTYPE + "_MSG_ALL";
            string strSQL_ALL_HIS = "select count(*) from " + strGWTYPE + "_MSG_ALL_HIS";
            string strSQL_CONTENT = "select count(*) from " + strGWTYPE + "_MSG_CONTENT";

            try
            {
                int intCountCONTENT = 0;
                int intALL_HIS = 0;
                int intALL = 0;
                intALL = Convert.ToInt32(DataAcess.ExecuteScalar(oraConn, CommandType.Text, strSQL_ALL, null));
                oraConn = conn.Connect();
                intALL_HIS = Convert.ToInt32(DataAcess.ExecuteScalar(oraConn, CommandType.Text, strSQL_ALL_HIS, null));
                oraConn = conn.Connect();
                intCountCONTENT = Convert.ToInt32(DataAcess.ExecuteScalar(oraConn, CommandType.Text, strSQL_CONTENT, null));
                if ((intCountCONTENT > 0) || (intALL_HIS > 0) || (intALL > 0))
                {
                    return true;
                }
                else {
                    return false; 
                }
            }
            catch 
            {
                return false;
            }
        }
        public DataTable GetGwtype_ID(string pGWTYPE)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }           
            string strSQL = "select G.GWTYPEID from GWTYPE G where Trim(g.gwtype)='" + pGWTYPE + "' order by G.GWTYPE asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
    }
	
	
}
