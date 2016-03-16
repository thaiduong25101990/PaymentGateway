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

//' Author:	Le Duc Quan (QuanLD@fpt.com.vn)
//' Detail: Nguyen Thi Thu Ha (HaNTT10@fpt.com.vn)
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GWBANK_MAPDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
		public GWBANK_MAPDP()
		{
		}
		public static GWBANK_MAPDP Instance()
		{
			return new GWBANK_MAPDP();
		}
		
		public int AddGWBANK_MAP(GWBANK_MAPInfo objTable)
		{
            string strSql = "Insert into GWBANK_MAP (SIBS_BANK_CODE, BANK_NAME, GW_BANK_CODE, BRANCH, DESCRIPTION) values(:pSIBS_BANK_CODE, :pBANK_NAME, :pGW_BANK_CODE, :pBRANCH, :pDESCRIPTION)";
            OracleParameter[] oraParam={new OracleParameter("pSIBS_BANK_CODE",OracleType.NVarChar,20),
                                           new OracleParameter("pBANK_NAME",OracleType.NVarChar,100),
                                            new OracleParameter("pGW_BANK_CODE",OracleType.NVarChar,20),
                                            new OracleParameter("pBRANCH",OracleType.Int32,1),
                                            new OracleParameter("pDESCRIPTION",OracleType.NVarChar,2)};
            oraParam[0].Value = objTable.SIBS_BANK_CODE;
            oraParam[1].Value = objTable.BANK_NAME;
            oraParam[2].Value = objTable.GW_BANK_CODE;
            oraParam[3].Value = objTable.BRANCH;
            oraParam[4].Value = objTable.DESCRIPTION;
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
		
		public int UpdateGWBANK_MAP(GWBANK_MAPInfo objTable)
		{
            string strSql = "Update GWBANK_MAP set SIBS_BANK_CODE=:pSIBS_BANK_CODE, BANK_NAME=:pBANK_NAME, GW_BANK_CODE=:pGW_BANK_CODE, BRANCH=:pBRANCH, DESCRIPTION=:pDESCRIPTION where BANK_MAP_ID=:pBANK_MAP_ID";
            OracleParameter[] oraParam={new OracleParameter("pBANK_MAP_ID",OracleType.Int32,5),
                                           new OracleParameter("pSIBS_BANK_CODE",OracleType.NVarChar,20),
                                           new OracleParameter("pBANK_NAME",OracleType.NVarChar,100),
                                           new OracleParameter("pGW_BANK_CODE",OracleType.NVarChar,20),
                                           new OracleParameter("pBRANCH",OracleType.Int32,1),
                                           new OracleParameter("DESCRIPTION",OracleType.NVarChar,2)};
            oraParam[0].Value = objTable.GWBANK_MAP_ID;
            oraParam[1].Value = objTable.SIBS_BANK_CODE;
            oraParam[2].Value = objTable.BANK_NAME;
            oraParam[3].Value = objTable.GW_BANK_CODE;
            oraParam[4].Value = objTable.BRANCH;
            oraParam[5].Value = objTable.DESCRIPTION;
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

        public int DeleteGWBANK_MAP(int iID)
		{
            string strSql = "Delete from GWBANK_MAP where BANK_MAP_ID=:pBANK_MAP_ID";
            OracleParameter[] oraParam ={new OracleParameter("pBANK_MAP_ID",OracleType.Int32,5)};
            oraParam[0].Value=iID;
			try
			{
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn,CommandType.Text,strSql,oraParam);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

        public DataSet GetGWBANK_MAP()
        {
            DataSet datDs = new DataSet();
            string strSql = "Select bank.BANK_MAP_ID, bank.SIBS_BANK_CODE, bank.BANK_NAME, bank.GW_BANK_CODE, bank.BRANCH, bank.DESCRIPTION from GWBANK_MAP bank";
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
