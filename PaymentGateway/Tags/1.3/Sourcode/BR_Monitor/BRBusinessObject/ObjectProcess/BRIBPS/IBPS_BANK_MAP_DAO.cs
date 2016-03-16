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
using System.Windows.Forms;
using System.Data.Odbc;

//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class IBPS_BANK_MAPDP
	{
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();
        /*Ham connect host*/
        private static OdbcConnection oraConnhost = new OdbcConnection();
        private static ConnectHost connhost = new ConnectHost();


		public IBPS_BANK_MAPDP()
		{
		}
		public static IBPS_BANK_MAPDP Instance()
		{
			return new IBPS_BANK_MAPDP();
		}

        //Nang cap
        public DataSet GetIBPS_BANK_MAP()
        {
            DataTable datTable = new DataTable();
            string strSql = "GW_PK_IBPS_BANK_MAP.GET_IBPS_BANK_MAP_ALL";
            OracleParameter[] oraPara = { new OracleParameter("pCReturn", OracleType.Cursor) };
            oraPara[0].Direction = ParameterDirection.Output;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraPara);
            }
            catch
            {
                return null;
            }
        }
        //Het nang cap





		
        public int AddIBPS_BANK_MAP(IBPS_BANK_MAPInfo objTable)
        {
            string strSql = "Insert into IBPS_BANK_MAP (GW_BANK_CODE,SIBS_BANK_CODE,BANK_NAME,TELLERID,DESCRIPTIONS,SHORT_NAME) values (:pGW_BANK_CODE,:pSIBS_BANK_CODE,:pBANK_NAME,:pTELLERID,:pDESCRIPTIONS,:pSHORT_BANK_NAME)";
            OracleParameter[] oraParam = {//new OracleParameter("pBANK_MAP_ID", OracleType.Number, 10),
                                         new OracleParameter("pGW_BANK_CODE", OracleType.NVarChar,12),
                                         new OracleParameter("pSIBS_BANK_CODE", OracleType.NVarChar,5),
                                         new OracleParameter("pBANK_NAME", OracleType.NVarChar, 255),
                                         new OracleParameter("pTELLERID", OracleType.NVarChar, 255),
                                         new OracleParameter("pDESCRIPTIONS", OracleType.NVarChar, 255),
                                         new OracleParameter("pSHORT_BANK_NAME", OracleType.NVarChar, 255)};   
            //oraParam[0].Value = objTable.BANK_MAP_ID;
            oraParam[0].Value = objTable.GW_BANK_CODE;
            oraParam[1].Value = objTable.SIBS_BANK_CODE;
            oraParam[2].Value = objTable.BANK_NAME;
            oraParam[3].Value = objTable.TELLERID;
            oraParam[4].Value = objTable.DESCRIPTIONS;
            oraParam[5].Value = objTable.SHORT_BANK_NAME;            
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

		public int UpdateIBPS_BANK_MAP(IBPS_BANK_MAPInfo objTable)
		{
            DataTable datTable = new DataTable();
            string strSQL = "Update IBPS_BANK_MAP set GW_BANK_CODE = :pGW_BANK_CODE,SIBS_BANK_CODE = :pSIBS_BANK_CODE,BANK_NAME =:pBANK_NAME, ";
            strSQL = strSQL + "TELLERID = :pTELLERID,DESCRIPTIONS = :pDESCRIPTION,SHORT_NAME = :pSHORT_BANK_NAME where BANK_MAP_ID=:pBANK_MAP_ID";
            OracleParameter[] oraParam = { new OracleParameter("pBANK_MAP_ID", OracleType.Number,10),
                                         new OracleParameter("pGW_BANK_CODE", OracleType.NVarChar,12),
                                         new OracleParameter("pSIBS_BANK_CODE", OracleType.NVarChar,5),
                                         new OracleParameter("pBANK_NAME", OracleType.NVarChar, 255),
                                         new OracleParameter("pTELLERID", OracleType.NVarChar, 255),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar, 255),
                                         new OracleParameter("pSHORT_BANK_NAME", OracleType.NVarChar, 255)};
            oraParam[0].Value = objTable.BANK_MAP_ID;
            oraParam[1].Value = objTable.GW_BANK_CODE;
            oraParam[2].Value = objTable.SIBS_BANK_CODE;
            oraParam[3].Value = objTable.BANK_NAME;
            oraParam[4].Value = objTable.TELLERID;
            oraParam[5].Value = objTable.DESCRIPTIONS;
            oraParam[6].Value = objTable.SHORT_BANK_NAME; 
            try
			{
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
			}
			catch 
			{
				return 0;
			}
		}
		
        public int DeleteIBPS_BANK_MAP(int iID)
        {
            string strSql = "Delete from IBPS_BANK_MAP where trim(BANK_MAP_ID)=:pBANK_MAP_ID";
            OracleParameter[] oraParam = { new OracleParameter("pBANK_MAP_ID", OracleType.Number, 10) };
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

        public DataSet GetIBPS_BANK_MAP(double BANK_MAP_ID)
        {
            DataTable datTable = new DataTable();
            OracleParameter[] oraPara = { new OracleParameter("pBANK_MAP_ID", OracleType.Number,20),
                                        new OracleParameter("pCReturn", OracleType.Cursor) };
            oraPara[1].Direction = ParameterDirection.Output;
            oraPara[0].Value = BANK_MAP_ID;
            string strSql = "GW_PK_IBPS_BANK_MAP.GET_IBPS_BANK_MAP_BMAPID";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSql, oraPara);
            }
            catch 
            {
                return null;
            }
        }


        public static void LoadDataSearch(string tableName, DataGridView dgvSearch,string strSign)
        {
            try
            {
                string strSql = "Select ibm.BANK_MAP_ID,ibm.GW_BANK_CODE,ibm.SIBS_BANK_CODE,ibm.BANK_NAME,ibm.TELLERID ,ibm.DESCRIPTIONS,ibm.SHORT_NAME from IBPS_BANK_MAP ibm where trim(ibm.sibs_bank_code) " + strSign + "'-1' order by ibm.GW_BANK_CODE";
                oraConn = conn.Connect();
                OracleCommand cmdSelect = new OracleCommand(strSql, oraConn);
                OracleDataAdapter da = new OracleDataAdapter(cmdSelect);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, tableName);
                dt = ds.Tables[tableName];
                dgvSearch.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //LblTong = Convert.ToString(ds.Tables[0].Rows.Count);
        }


        public DataSet GetIBPS_BANK_MAPcomboFilter()
        {
            DataTable datTable = new DataTable();
            string strSql = "Select  count(distinct BANK_NAME) from IBPS_BANK_MAP ibm order by ibm.BANK_NAME";
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

        public DataSet GetIBPS_BANK_MAPTotalBank(string strSign)
        {
            DataTable datTable = new DataTable();
            string strSql = "Select ibm.BANK_MAP_ID,ibm.GW_BANK_CODE,ibm.SIBS_BANK_CODE,ibm.BANK_NAME,ibm.TELLERID ,ibm.DESCRIPTIONS,ibm.SHORT_NAME from IBPS_BANK_MAP ibm where trim(sibs_bank_code) " + strSign + "'-1'";
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

// Ham tim kiem thong tin theo ma 3 so, ma 8 so, chi nhanh co phai cn dau moi ko? Co Citad ko?
// Con thieu tim kiem theo ten vung
        public DataSet GetIBPS_BANK_MAP_TAD(Char pSIBS_BANK_CODE, Char pGW_BANK_CODE, int pMain, int pTad)
        {
            DataTable datTable = new DataTable();
            string strSql = "Select a.BANK_MAP_ID, b.BANK_NAME, a.SIBS_BANK_CODE, a.GW_BANK_CODE, c.TAD_ID, a.MAIN";
                   strSql = strSql + " from IBPS_BANK_MAP a, GWBANK_MAP b, IBPS_BANK_TAD c where a.SIBS_BANK_CODE like '%" + pSIBS_BANK_CODE + "'";
                   strSql = strSql + " and trim(a.GW_BANK_CODE) like '%" + pGW_BANK_CODE + "' and a.BANK_MAP_ID = b.BANK_MAP_ID and a.BANK_MAP_ID = c.BANK_MAP_ID";
                   strSql = strSql + " and trim(c.MAIN) = " + pMain + " and trim(c.TAD) = " + pTad + "";   
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

        //lay ra Tellerid
        public DataSet GetIBPS_BANK_MAP_TELLERID(string pGW_BANK_CODE)
        {
            DataTable datTable = new DataTable();
            string strSql = "select IBM.BANK_MAP_ID,IBM.SIBS_BANK_CODE,IBM.GW_BANK_CODE,IBM.BANK_NAME,IBM.DESCRIPTIONS,IBM.TELLERID,IBM.CONTROL_UNIT from IBPS_BANK_MAP IBM where Trim(IBM.GW_BANK_CODE) ='" + pGW_BANK_CODE + "'";
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

        // Lay ra GW_BANK_CODE
        public DataSet GetIBPS_BANK_MAP_GWCODE(string pSIBS_BANK_CODE)
        {
            DataTable datTable = new DataTable();
            string strSql = "SELECT * FROM IBPS_BANK_MAP WHERE trim(SIBS_BANK_CODE) ='" + pSIBS_BANK_CODE + "'";
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


        public DataSet GetIBPS_BANK_MAPBankName(int iSIBSBankCodeLength)
        {
            DataTable datTable = new DataTable();
            string strSql = "select i.GW_BANK_CODE,i.SIBS_BANK_CODE,i.bank_name, ";
            strSql = strSql + " b.BRAN_NAME from ibps_bank_map i, branch b where trim(lpad(b.sibs_bank_code," + iSIBSBankCodeLength + ",'0')) = trim(i.sibs_bank_code) order by i.GW_BANK_CODE";
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
        /// <summary>
        /// BichNN add 14/08/2008
        /// </summary>
        /// <param name="iSIBSBankCodeLength"></param>
        /// <param name="strGWBankCode"></param>
        /// <returns></returns>
        public DataSet GetIBPS_BANK_MAPInfo(int iSIBSBankCodeLength)
        {
            DataTable datTable = new DataTable();
          
            string strSql = "select i.GW_BANK_CODE,case when trim(i.sibs_bank_code)='-1'then i.sibs_bank_code else substr(i.sibs_bank_code,-3) end as citadid,i.bank_name,b.BRAN_NAME,b.sibs_bank_code  ";
            strSql = strSql + " from ibps_bank_map i, branch b";
            strSql = strSql + " where trim(lpad(b.sibs_bank_code,5,'0')) = trim(i.sibs_bank_code) ";
            strSql = strSql + " order by i.GW_BANK_CODE";
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
        public DataSet GetIBPS_BANK_MAP_GWBankCode(int iSIBSBankCodeLength,string strGWBankCode)
        {
            DataTable datTable = new DataTable();
            string strSql = "select i.GW_BANK_CODE,i.SIBS_BANK_CODE,i.bank_name,b.BRAN_NAME from ibps_bank_map i, branch b where trim(lpad(b.sibs_bank_code," + iSIBSBankCodeLength + ",'0')) = trim(i.sibs_bank_code) and trim(i.GW_BANK_CODE)='" + strGWBankCode + "' order by i.GW_BANK_CODE";
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
        public int CheckData(string strSIBS_BANK_CODE)
        {
            oraConn = conn.Connect();
            int intReturn;
            if (oraConn == null)
            {
                return -1;
            }
            DataTable datTable = new DataTable();
            string strSQL = " SELECT COUNT(*) FROM  Branch WHERE trim(lpad(SIBS_BANK_CODE,5,'0')) = '" + strSIBS_BANK_CODE.Trim() + "'";

            try
            {
              intReturn= Convert.ToInt16(DataAcess.ExecuteScalar(oraConn, CommandType.Text, strSQL, null));
              return intReturn;
            }
            catch 
            {
                return -1;
            }
        }
        public DataSet GetIBPS_BANK_MAPSearch(string strSQL)
        {
            DataSet datDs = new DataSet();
            string strSql = "Select ibm.BANK_MAP_ID,ibm.GW_BANK_CODE,case when trim(sibs_bank_code)='-1'then sibs_bank_code else substr(sibs_bank_code,-3) end as sibs_bank_code,ibm.BANK_NAME,ibm.SHORT_NAME ,ibm.DESCRIPTIONS, ibm.TELLERID from IBPS_BANK_MAP ibm " + strSQL;
          
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
        // Lay ra GW_BANK_CODE
        public DataSet GetIBPS_BANK_MAP_BankName(string pGW_BANK_CODE)
        {
            //DataTable datTable = new DataTable();
            string strSql = "SELECT IBM.BANK_MAP_ID,IBM.SIBS_BANK_CODE,IBM.GW_BANK_CODE,IBM.BANK_NAME FROM IBPS_BANK_MAP IBM WHERE trim(IBM.GW_BANK_CODE) ='" + pGW_BANK_CODE + "' ";
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

        public DataSet Get_NAME(string pGW_BANK_CODE, string pSIBS_BANK_CODE)
        {
            //DataTable datTable = new DataTable();
            string strSql = "SELECT BANK_NAME FROM IBPS_BANK_MAP IBM WHERE trim(IBM.GW_BANK_CODE) ='" + pGW_BANK_CODE + "'  and lpad(SIBS_BANK_CODE,5,'0')=Lpad('" + pSIBS_BANK_CODE + "',5,'0')";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                DataSet ds = new DataSet();
                ds= DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                 return ds;
            }
            catch
            {
                return null;
            }
        }


        public DataSet GetIBPS_BANK_MAP_GWBankCode(string pGW_BANK_CODE)
        {
            DataTable datTable = new DataTable();
            string strSql = "select IBM.BANK_MAP_ID,IBM.SIBS_BANK_CODE,IBM.GW_BANK_CODE,IBM.BANK_NAME,IBM.DESCRIPTIONS,IBM.TELLERID,IBM.CONTROL_UNIT from IBPS_BANK_MAP IBM where Trim(IBM.GW_BANK_CODE) ='" + pGW_BANK_CODE + "'";
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

        public DataSet GetIBPS_BANK_MAPStateBankIDName(string strGWBankCode)
        {
            DataTable datTable = new DataTable();
            string strSql = "select i.GW_BANK_CODE,i.SIBS_BANK_CODE citadid,i.bank_name,b.BRAN_NAME,b.sibs_bank_code  ";
            strSql = strSql + " from ibps_bank_map i, branch b";
            strSql = strSql + " where trim(lpad(b.sibs_bank_code,5,'0')) = trim(Lpad(i.sibs_bank_code,5,'0')) and trim(i.GW_BANK_CODE)='" + strGWBankCode + "'";
            
            strSql = strSql + " order by i.GW_BANK_CODE";
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

        public DataTable Get_SIBS_BANK_CODE(string pGW_BANK_CODE)
        {
            DataTable datTable = new DataTable();
            string strSql = " select d.sibs_bank_code from IBPS_Bank_map d where  trim(d.gw_bank_code) ='" + pGW_BANK_CODE + "'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public DataTable SearchViewIBPS_BANK_MAP(string strWhere, string strSign1, string strSign2)
        {
            DataTable datTable = new DataTable();
        
            string strSql = "select distinct ibm.BANK_MAP_ID,ibm.GW_BANK_CODE,case when trim(ibm.sibs_bank_code)='-1'then ibm.sibs_bank_code else substr(ibm.sibs_bank_code,-3) end as sibs_bank_code,ibm.BANK_NAME,ibm.TELLERID ,ibm.DESCRIPTIONS,ibm.SHORT_NAME from ibps_bank_map ibm,";
            strSql = strSql + "(select b.sibs_bank_code,b.bran_type,b.bran_name from branch b) bran ";
            strSql = strSql + " where  trim(ibm.sibs_bank_code) " + strSign1 + "'-1' " + strWhere + "  and trim(bran.bran_type) " + strSign2 + " '1'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }


        /*Ham lay du lieu bang branch cua MSB  truc tiep trong host ra*/
        public DataTable GET_BRANCH_HOST()
        {
            DataTable _dt = new DataTable();

            string strSql = "select * from svparpv51";

            try
            {
                oraConnhost = connhost.OdbcConnect();
                if (oraConnhost == null)
                    return null;
                _dt = DataAcess.ExcuteDataTableODBC(strSql, oraConnhost);
                oraConnhost.Close();
                oraConnhost.Dispose();
                return _dt;
            }
            catch
            {
                return null;
            }
        }

        /*Ham lay du lieu bang branch cua MSB  truc tiep trong host ra*/
        public string GET_BANK_NAME_TAD(string pGW_BANK_CODE)
        {
            DataTable _dt = new DataTable();           
            try
            {
                string vOut = "";
                OracleParameter[] oraPara = { new OracleParameter("pGW_BANK_CODE", OracleType.VarChar,20),
                                        new OracleParameter("pBANK_NAME", OracleType.VarChar,100) };

                oraPara[0].Value = pGW_BANK_CODE;
                oraPara[1].Direction = ParameterDirection.Output;

                oraConn = conn.Connect();
                if (oraConn == null)
                    return "";
                DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_IBPS_MSG_CONTENT.GET_BANK_NAME_TAD", oraPara);
                vOut = oraPara[1].Value.ToString();
                oraConn.Dispose();
                oraConn.Close();
                return vOut;
            }
            catch
            {
                return "";
            }
        }
        
       
	}
	
	
}
