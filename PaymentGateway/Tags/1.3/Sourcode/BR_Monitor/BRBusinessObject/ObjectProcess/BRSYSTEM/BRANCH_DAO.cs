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

namespace BR.BRBusinessObject
{
    public class BRANCHDP
    {
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();

        public BRANCHDP()
		{
		}
        public static BRANCHDP Instance()
		{
            return new BRANCHDP();
		}

        // nang cap
        public DataSet GetBRANCH_MAP(string pBANK_CODE)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            OracleParameter[] OraPara = { new OracleParameter("p_CurRETURN", OracleType.Cursor) ,
                                        new OracleParameter("pSIBSBANK_CODE", OracleType.VarChar,20)};
            OraPara[0].Direction = ParameterDirection.Output;
            OraPara[1].Value = pBANK_CODE;

            //DataTable datTable = new DataTable();
            string strSQL = "GW_PK_BRANCH.GET_BRANCH_NAME_ONE";  

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, OraPara);
            }
            catch 
            {
                return null;
            }
        }

        public DataTable CHECK_BRANCH_MAP(string pGW_BANK_CODE,string pSIBS_BANK_CODE)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select GW_BANK_CODE from ibps_bank_map where lpad(SIBS_BANK_CODE,5,'0')= lpad('" + pSIBS_BANK_CODE + "',5,'0') ";
            strSQL = strSQL + " and GW_BANK_CODE = '" + pGW_BANK_CODE + "' ";
            try
            {
                datTable = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Dispose();
                oraConn.Close();
                return datTable;
            }
            catch
            {
                return null;
            }
        }

        // BichNN add 03/08/2008
        public DataSet GetData()
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = " GW_PK_BRANCH.GET_BRANCH_NAME";

            OracleParameter[] OraPara = { new OracleParameter("p_CurRETURN", OracleType.Cursor) };
            OraPara[0].Direction = ParameterDirection.Output;

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, OraPara);
            }
            catch 
            {
                return null;
            }
        }

        // het nang cap





        public DataTable GetData_Leave(string pSIBS_BANK_CODE)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = " SELECT BRAN_NAME  FROM  Branch B where lpad(Trim(B.SIBS_BANK_CODE),5,'0')='" + pSIBS_BANK_CODE.PadLeft(5,'0') + "' ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetData(string strWhere)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = " SELECT SIBS_BANK_CODE,BRAN_NAME,BRAN_TYPE  FROM  Branch " + strWhere + " order by SIBS_BANK_CODE";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet Select(string strWhere)
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = " SELECT bra.SIBS_BANK_CODE,bra.BRAN_NAME,(select a.content from Allcode a where trim(a.cdname)='BRAN_TYPE' and trim(a.cdval)= trim(bra.bran_type)) as BRAN_TYPE,bra.address,bra.tel,bra.fax  FROM  Branch bra " + strWhere + " order by SIBS_BANK_CODE";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }


        public int Insert(BRANCHInfo  objBranchInfo)
        {
            string strSql = "Insert into Branch  (SIBS_BANK_CODE, BRAN_TYPE, BRAN_NAME, ADDRESS, TEL, FAX) values (:pSIBS_BANK_CODE, :pBRAN_TYPE, :pBRAN_NAME, :pADDRESS, :pTEL, :pFAX)";
            OracleParameter[] oraParam = {new OracleParameter("pSIBS_BANK_CODE", OracleType.VarChar  , 5),
                                         new OracleParameter("pBRAN_TYPE", OracleType.VarChar , 2),                                         
                                         new OracleParameter("pBRAN_NAME", OracleType.VarChar , 50),
                                         new OracleParameter("pADDRESS", OracleType.VarChar  , 255),
                                         new OracleParameter("pTEL", OracleType.VarChar , 30),
                                         new OracleParameter("pFAX", OracleType.VarChar  , 30)
                                         };
            oraParam[0].Value = objBranchInfo.SIBS_BANK_CODE ;
            oraParam[1].Value = objBranchInfo.BRAN_TYPE ;
            oraParam[2].Value = objBranchInfo.BRAN_NAME ;
            oraParam[3].Value = objBranchInfo.ADDRESS ;
            oraParam[4].Value = objBranchInfo.TEL ;
            oraParam[5].Value = objBranchInfo.FAX  ;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                oraConn.Dispose();
                throw (ex);
               
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        public int Update(BRANCHInfo objBranchInfo)
        {
            string strSql = "Update Branch  set SIBS_BANK_CODE=:pSIBS_BANK_CODE, BRAN_TYPE=:pBRAN_TYPE, BRAN_NAME=:pBRAN_NAME, ";
            strSql = strSql + " ADDRESS= :pADDRESS, TEL=:pTEL, FAX= :pFAX where SIBS_BANK_CODE=:pSIBS_BANK_CODE ";
            OracleParameter[] oraParam = {new OracleParameter("pSIBS_BANK_CODE", OracleType.VarChar  , 5),
                                         new OracleParameter("pBRAN_TYPE", OracleType.VarChar , 2),                                         
                                         new OracleParameter("pBRAN_NAME", OracleType.VarChar , 50),
                                         new OracleParameter("pADDRESS", OracleType.VarChar  , 255),
                                         new OracleParameter("pTEL", OracleType.VarChar , 30),
                                         new OracleParameter("pFAX", OracleType.VarChar  , 30)
                                         };
                                        
            oraParam[0].Value = objBranchInfo.SIBS_BANK_CODE ;
            oraParam[1].Value = objBranchInfo.BRAN_TYPE ;
            oraParam[2].Value = objBranchInfo.BRAN_NAME ;
            oraParam[3].Value = objBranchInfo.ADDRESS ;
            oraParam[4].Value = objBranchInfo.TEL ;
            oraParam[5].Value = objBranchInfo.FAX  ;

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
            finally
            {
                oraConn.Dispose();
            }
        }
        public int Delete(string strSIBS_BANK_CODE)
        {
            string strSql = "Delete from Branch where SIBS_BANK_CODE='" + strSIBS_BANK_CODE + "'";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);

            }
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataTable BRANCH_OtherBranch(string strBRANCH_TYPE, string strSIBS_BANK_CODE)
        {
            DataTable datTable = new DataTable();
            //string strSql = "select distinct ibm.BANK_MAP_ID,ibm.GW_BANK_CODE,ltrim(ibm.SIBS_BANK_CODE,'0') as SIBS_BANK_CODE,ibm.BANK_NAME,ibm.TELLERID ,ibm.DESCRIPTIONS,ibm.SHORT_NAME from ibps_bank_map ibm,";
            //strSql = strSql + "(select b.sibs_bank_code,b.bran_type,b.bran_name from branch b) bran ";
            //strSql = strSql + " where  trim(ibm.sibs_bank_code) " + strSign1 + "'-1' " + strWhere + "  and trim(bran.bran_type) " + strSign2 + " '1'"; // truoc ngay 30.08

            string strSql = "select  ID,SIBS_BANK_CODE ,BRAN_TYPE,BRAN_NAME,PROV_CODE,ADDRESS,TEL ,";
            strSql = strSql + "  FAX,EDATE,TESTKEY,STT_LH ,SBANK_CODE,RMSEQUEN ,TRANSDATE  from branch b";
            strSql = strSql + "  where trim(b.sibs_bank_code) <>'-1' and trim(b.bran_type)='" + strBRANCH_TYPE + "'";
            strSql = strSql + "  and LPAD(trim(sibs_bank_code),5,'0')= LPAD('" + strSIBS_BANK_CODE.Trim() + "',5,'0')";  
            
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
        
        }
    }

