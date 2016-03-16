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
	public class MSG_DEFDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();	
		public MSG_DEFDP()
		{
		}
		public static MSG_DEFDP Instance()
		{
			return new MSG_DEFDP();
		}
		
		public int AddMSG_DEF(MSG_DEFInfo objTable)
		{
            string strSql = "Insert into MSG_DEF  (MSG_DEF_ID,FIELD_NAME,DESCRIPTION,FIELD_CODE,SIBS_POS,GW_POS,LENGTH,DATA_TYPE,DEFAULT_VALUE,SIBS_FIELD_CODE,CHK) values (:pMSG_DEF_ID,:pFIELD_NAME,:pFIELD_DESCRIPTION,:pFIELD_CODE,:pSIBS_POS,:pGW_POS,:pLENGTH,:pDATA_TYPE,:pDEFAULT_VALUE,:pSIBS_FIELD_CODE,:pCHK)";
            OracleParameter[] oraParam = {new OracleParameter("pMSG_DEF_ID", OracleType.NVarChar, 15),
                                         new OracleParameter("pFIELD_NAME",OracleType.NVarChar,15),
                                         new OracleParameter("pFIELD_DESCRIPTION",OracleType.NVarChar,70),
                                         new OracleParameter("pFIELD_CODE",OracleType.NVarChar,6),
                                         new OracleParameter("pSIBS_POS",OracleType.Number,10),
                                         new OracleParameter("pGW_POS",OracleType.Number,10),
                                         new OracleParameter("pLENGTH",OracleType.Number,10),
                                         new OracleParameter("pDATA_TYPE",OracleType.Number,10),
                                         new OracleParameter("pDEFAULT_VALUE", OracleType.NVarChar, 100),
                                         new OracleParameter("pSIBS_FIELD_CODE", OracleType.NVarChar, 15),
                                         new OracleParameter("pCHK", OracleType.NVarChar, 1)};        
                oraParam[0].Value = objTable.MSG_DEF_ID;
                oraParam[1].Value = objTable.FIELD_NAME;
                oraParam[2].Value = objTable.FIELD_DESCRIPTION;
                oraParam[3].Value = objTable.FIELD_CODE;
                oraParam[4].Value = objTable.SIBS_POS;
                oraParam[5].Value = objTable.GW_POS;
                oraParam[6].Value = objTable.LENGTH;
                oraParam[7].Value = objTable.DATA_TYPE;
                oraParam[8].Value = objTable.DEFAULT_VALUE;
                oraParam[9].Value = objTable.SIBS_FIELD_CODE;
                oraParam[10].Value = objTable.CHK;
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

		public int UpdateMSG_DEF(MSG_DEFInfo objTable)
		{
            //string strSql = "Update Currency  set currency_code=:pcurrency_code, currency_Name=:pcurrency_Name,Note=:pNote,where currency_id=:pCurrency_id";
            string strSql = "Update MSG_DEF set MSG_DEF_ID=:pMSG_DEF_ID,FIELD_NAME=:pFIELD_NAME,";
            strSql = strSql+ "DESCRIPTION=:pFIELD_DESCRIPTION,FIELD_CODE=:pFIELD_CODE,";
            strSql = strSql + "SIBS_POS=:pSIBS_POS,GW_POS=:pGW_POS,LENGTH=:pLENGTH,DATA_TYPE=:pDATA_TYPE,";
            strSql = strSql + "DEFAULT_VALUE=:pDEFAULT_VALUE,SIBS_FIELD_CODE=:pSIBS_FIELD_CODE, CHK=:pCHK";
            strSql = strSql + " where FIELDID=:pFIELDID";
            OracleParameter[] oraParam = {new OracleParameter("pFIELDID", OracleType.Int16, 20),
                                         new OracleParameter("pMSG_DEF_ID", OracleType.NVarChar, 15),
                                         new OracleParameter("pFIELD_NAME",OracleType.NVarChar, 15),
                                         new OracleParameter("pFIELD_DESCRIPTION",OracleType.NVarChar, 210),
                                         new OracleParameter("pFIELD_CODE",OracleType.NVarChar, 6),
                                         new OracleParameter("pSIBS_POS",OracleType.Number, 10),
                                         new OracleParameter("pGW_POS",OracleType.Number, 10),
                                         new OracleParameter("pLENGTH",OracleType.Number, 10),
                                         new OracleParameter("pDATA_TYPE",OracleType.Number, 10),
                                         new OracleParameter("pDEFAULT_VALUE", OracleType.NVarChar, 100),
                                         new OracleParameter("pSIBS_FIELD_CODE", OracleType.NVarChar, 15),
                                         new OracleParameter("pCHK", OracleType.NVarChar, 1)};

			try
			{
                oraParam[0].Value = objTable.FIELD_ID;
                oraParam[1].Value = objTable.MSG_DEF_ID;
                oraParam[2].Value = objTable.FIELD_NAME;
                oraParam[3].Value = objTable.FIELD_DESCRIPTION;
                oraParam[4].Value = objTable.FIELD_CODE;
                oraParam[5].Value = objTable.SIBS_POS;
                oraParam[6].Value = objTable.GW_POS;
                oraParam[7].Value = objTable.LENGTH;
                oraParam[8].Value = objTable.DATA_TYPE;
                oraParam[9].Value = objTable.DEFAULT_VALUE;
                oraParam[10].Value = objTable.SIBS_FIELD_CODE;
                oraParam[11].Value = objTable.CHK;

 
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

        public int DeleteMSG_DEF(int intFIELD_ID)
		{
            string strSql = "Delete from MSG_DEF where FIELDID = :pFIELD_ID";
            OracleParameter[] oraParam = { new OracleParameter("pFIELD_ID", OracleType.Int16, 20) };
            oraParam[0].Value = intFIELD_ID;
            
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


        public DataSet GetMSGDef()
        {
            DataSet datDs = new DataSet();
            string strSql = "select def.FIELDID, def.MSG_DEF_ID,def.FIELD_NAME,def.FIELD_CODE,def.DESCRIPTION,def.SIBS_POS,def.GW_POS,def.LENGTH, a.CONTENT DATA_TYPE, ";
            strSql = strSql + "def.DEFAULT_VALUE,def.CHK from MSG_DEF def, (Select  ID ,CDVAL , CDNAME,CONTENT , GWTYPE , DESCRIPTION,LSTORD,DIRECTION from allcode where cdname = 'DATATYPE' and GWTYPE = 'SYSTEM') a where trim(a.CDVAL) = trim(def.DATA_TYPE)";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public DataSet GetMSGDef_MsgID(string strMsgDefID)
        {
            DataSet datDs = new DataSet();
            string strSql = "select def.FIELDID, def.MSG_DEF_ID,def.FIELD_NAME,def.FIELD_CODE,def.DESCRIPTION,def.SIBS_POS,def.GW_POS,def.LENGTH, a.CONTENT DATA_TYPE, ";
            strSql = strSql + "def.DEFAULT_VALUE,def.CHK from MSG_DEF def, (Select   ID ,CDVAL , CDNAME,CONTENT , GWTYPE , DESCRIPTION,LSTORD,DIRECTION from allcode where cdname = 'DATATYPE' and GWTYPE = 'SYSTEM') a where trim(def.MSG_DEF_ID) = trim('" + strMsgDefID + "') and trim(a.CDVAL) = trim(def.DATA_TYPE)";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataSet GetMSGDef_Combo()
        {
            DataSet datDs = new DataSet();
            string strSql = " select distinct(MSG_DEF_ID) from MSG_DEF order by MSG_DEF_ID  ASC";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
	}
	
	
}
