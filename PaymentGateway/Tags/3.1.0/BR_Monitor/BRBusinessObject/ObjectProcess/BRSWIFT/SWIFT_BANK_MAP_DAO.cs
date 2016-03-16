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
using BR.DataAccess;
using System.Data.OracleClient;

//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 27/05/2008
//' =============================================
namespace  BR.BRBusinessObject
{
	public class SWIFT_BANK_MAPDP
	{
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
		public SWIFT_BANK_MAPDP()
		{
		}
		public static SWIFT_BANK_MAPDP Instance()
		{
			return new SWIFT_BANK_MAPDP();
		}
		
		public int AddSWIFT_BANK_MAP(SWIFT_BANK_MAPInfo objTable)
		{
			try
			{
                int iResult;
                string strSql = "GW_PK_SWIFT_BANK_MAP.INSERT_SWIFT_BANK_MAP";
                OracleParameter[] oraParas ={new OracleParameter("pSIBS_BANK_CODE",OracleType.Char,5) ,
                                        new OracleParameter("pSWIFT_BANK_CODE",OracleType.Char,12) ,
                                        new OracleParameter("pBANK_NAME",OracleType.NVarChar,255),
                                        new OracleParameter("pDESCRIPTION",OracleType.NVarChar,255),
                                        new OracleParameter("pTELLERID",OracleType.NVarChar,255)};

                oraParas[0].Value = objTable.SIBS_BANK_CODE;
                oraParas[1].Value = objTable.SWIFT_BANK_CODE;
                oraParas[2].Value = objTable.BANK_NAME;
                oraParas[3].Value = objTable.DESCRIPTION;
                oraParas[4].Value = objTable.TELLERID;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iResult;
			}
			catch (Exception ex)
			{
				throw (ex);
                return -1;
			}
		}
		
		public int UpdateSWIFT_BANK_MAP(SWIFT_BANK_MAPInfo objTable)
		{
            try
            {
                int iResult;
                string strSql = "GW_PK_SWIFT_BANK_MAP.UPDATE_SWIFT_BANK_MAP";
                OracleParameter[] oraParas ={new OracleParameter("pBANK_MAP_ID",OracleType.Number,10) ,
                                        new OracleParameter("pSIBS_BANK_CODE",OracleType.Char,5) ,
                                        new OracleParameter("pSWIFT_BANK_CODE",OracleType.Char,12) ,
                                        new OracleParameter("pBANK_NAME",OracleType.NVarChar,255),
                                        new OracleParameter("pDESCRIPTION",OracleType.NVarChar,255),
                                        new OracleParameter("pTELLERID",OracleType.NVarChar,255)};

                oraParas[0].Value = objTable.BANK_MAP_ID;
                oraParas[1].Value = objTable.SIBS_BANK_CODE;
                oraParas[2].Value = objTable.SWIFT_BANK_CODE;
                oraParas[3].Value = objTable.BANK_NAME;
                oraParas[4].Value = objTable.DESCRIPTION;
                oraParas[5].Value = objTable.TELLERID;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iResult;
            }
            catch (Exception ex)
            {
                throw (ex);
                return -1;
            }
		}
		
		public int DeleteSWIFT_BANK_MAP(SWIFT_BANK_MAPInfo objTable)
		{
            try
            {
                int iResult;
                string strSql = "GW_PK_SWIFT_BANK_MAP.DELETE_SWIFT_BANK_MAP";
                OracleParameter[] oraParas ={new OracleParameter("pBANK_MAP_ID",OracleType.Number,10)};

                oraParas[0].Value = objTable.BANK_MAP_ID;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iResult;
            }
            catch (Exception ex)
            {
                throw (ex);
                return -1;
            }
		}

        //lay du lieu co dieu kien truy van
        public DataSet GetSWIFT_BANK_MAPA(string pSIBS_BANK_CODE)//lay ra ten ngan hang gui
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select SBM.BANK_MAP_ID,SBM.SIBS_BANK_CODE, SBM.SWIFT_BANK_CODE, SBM.BANK_NAME, SBM.DESCRIPTION, SBM.TELLERID from SWIFT_BANK_MAP SBM where Trim(SBM.SIBS_BANK_CODE)='" + pSIBS_BANK_CODE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetSWIFT_BANK_MAPB(string pSWIFT_BANK_CODE)//lay ra ten ngan hang nhan
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select SBM.BANK_MAP_ID,SBM.SIBS_BANK_CODE, SBM.SWIFT_BANK_CODE, SBM.BANK_NAME, SBM.DESCRIPTION, SBM.TELLERID from SWIFT_BANK_MAP SBM where  Trim(SBM.SWIFT_BANK_CODE)='" + pSWIFT_BANK_CODE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        //lay ra ten ngan hang nhan trong form frmSwiftMsgManualInfo
        public DataSet GetSWIFT_BANK_MAP_ReceivedBranch()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select SBM.BANK_MAP_ID,SBM.SIBS_BANK_CODE, SBM.SWIFT_BANK_CODE," +
                "(select b.bran_name from Branch b where LPAD(Trim(b.sibs_bank_code),5,'0') ="+
                " Trim(SBM.SIBS_BANK_CODE)) as BANK_NAME from SWIFT_BANK_MAP SBM " +
                " WHERE SBM.SWIFT_BANK_CODE LIKE 'MCOB%'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataTable Search_bank_Map(string pSIBS_BANK_CODE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select * from SWIFT_BANK_MAP B where B.SIBS_BANK_CODE= LPAD('" + pSIBS_BANK_CODE + "',5,'0')";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        //lay du lieu co dieu kien truy van
        public DataSet GetSWIFT_BANK_MAP_ALL(string pWhere)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
                return null;

            try
            {
                DataTable datTable = new DataTable();
                string strSQL = "select BANK_MAP_ID,SIBS_BANK_CODE,SWIFT_BANK_CODE, " +
                    "BANK_NAME,DESCRIPTION,TELLERID from SWIFT_BANK_MAP " + pWhere ;            
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
        }

				
	}
}
