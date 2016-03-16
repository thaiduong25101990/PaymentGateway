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
	public class SYSVARDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public SYSVARDP()
		{
		}
		public static SYSVARDP Instance()
		{
			return new SYSVARDP();
		}
		
		public int AddSYSVAR(SYSVARInfo objTable)
		{
            //string strSql = "Insert into SYSVAR(VAR_TYPE, VAR_NAME, VALUE, NOTE) values (:pVAR_TYPE, :pVAR_NAME, :pVALUE, :pNOTE)";
            string strSql = "Insert into SYSVAR (VAR_TYPE,VAR_NAME, VALUE, NOTE) values (:pVAR_TYPE,:pVAR_NAME, :pVALUE, :pNOTE)";
            OracleParameter[] oraParam ={new OracleParameter("pVAR_TYPE",OracleType.NVarChar,10),
                                           new OracleParameter("pVAR_NAME",OracleType.NVarChar,50),
                                           new OracleParameter("pVALUE",OracleType.NVarChar,30),
                                           new OracleParameter("pNOTE",OracleType.NVarChar,200)};
            //oraParam[0].Value = objTable.VAR_TYPE;
            //oraParam[1].Value = objTable.VAR_NAME;
            //oraParam[2].Value = objTable.VALUE;
            //oraParam[3].Value = objTable.NOTE;
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
		
		public int UpdateSYSVAR(SYSVARInfo objTable)
		{
            string strSql = "Update SYSVAR set ID=:pID, VALUE=:pVALUE, DESCRIPTION=:pDESCRIPTION where ID=:pID ";
            OracleParameter[] oraParam ={new OracleParameter("pID",OracleType.Int32,5),
                                         new OracleParameter("pVALUE",OracleType.VarChar,30),
                                         new OracleParameter("pDESCRIPTION",OracleType.VarChar,1000)};
			try
			{
                oraParam[0].Value = objTable.ID;
                oraParam[1].Value = objTable.VALUE;
                oraParam[2].Value = objTable.DESCRIPTION;

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


        public int UpdateSYSVARDayDuplicate(SYSVARInfo objTable)
        {
            string strSql = "Update SYSVAR set VALUE = '" + objTable.VALUE  + "' where VARTYPE='TTSP' and VARNAME = 'TTSP_DAYS'";
            //OracleParameter[] oraParam ={new OracleParameter("pVALUE",OracleType.NVarChar,30)};
            try
            {
                //oraParam[0].Value = objTable.VALUE;
               
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


		
		public int DeleteSYSVAR(int iID)
		{
            string strSql = "Delete from SYSVAR where ID=:pID";
            OracleParameter[] oraParm = { new OracleParameter("pID", OracleType.Int32, 5) };
            oraParm[0].Value = iID;
			try
			{
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParm);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

        public DataSet GetSYSVAR()
        {
            DataSet datDs = new DataSet();
            //string strSql = "Select sys.ID,sys.GWTYPE,sys.VARNAME, sys.VALUE,sys.TYPE, sys.NOTE from SYSVAR sys";
            string strSql = "Select sys.ID,sys.GWTYPE,sys.VARNAME, sys.VALUE,sys.TYPE, sys.NOTE,sys.DESCRIPTION from SYSVAR sys order by sys.VARNAME";
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

        public DataSet GetSYSVAR_NAME(string strVarName, string strGWType)
        {
            DataSet datDs = new DataSet();
            //string strSql = "Select sys.ID,sys.GWTYPE,sys.VARNAME, sys.VALUE,sys.TYPE, sys.NOTE from SYSVAR sys";
            string strSql = "Select sys.ID,sys.GWTYPE,sys.VARNAME, sys.VALUE,sys.TYPE, sys.NOTE from SYSVAR sys where trim(sys.VARNAME) = '" + strVarName.Trim() + "' and trim(sys.GWTYPE) = '" + strGWType.Trim() + "'";
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

        //GetIBPSBankLenght
        //Lay ra gia tri do dai cua truong GWBank_Code trong form IBPS_BANK_MAP
        public DataSet GetIBPSBankLength(string strLength)
        {
            DataSet datDs = new DataSet();
            //string strSql = "Select sys.ID,sys.GWTYPE,sys.VARNAME, sys.VALUE,sys.TYPE, sys.NOTE from SYSVAR sys";
            string strSql = "select t.value from sysvar t where trim(t.varname)='"+ strLength.Trim() + "'";
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
        public DataTable  GetSTATEMENT_ID()
        {
            oraConn = conn.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select * from sysvar where varname='STATEMENT_ID'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
            
        }
        public int UpdateGetSTATEMENT_ID(SYSVARInfo objTable)
        {
            string strSql = "Update SYSVAR set VALUE =:pVALUE where trim(varname)='STATEMENT_ID'";
            OracleParameter[] oraParam ={new OracleParameter("pVALUE",OracleType.NVarChar,30)};
            oraParam[0].Value = objTable.VALUE;
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

        public DataSet GetIBPS_ROUTER_TAD(string strSIBSBankCode, string strStatus)
        {
            DataSet datDs = new DataSet();
            string strSql = "select * from tad t where trim(t.sibs_bank_code)='" + strSIBSBankCode + "' and trim(t.status)='" + strStatus + "'";
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

        public int UpdateSysvar(string pVarName, string pValue, string pNote)
        {
           
            try
            {
                string strSql = "Update SYSVAR set VALUE =:pValue, Note=:pNote where Upper(varname)=Upper(:pVarName)";
                OracleParameter[] oraParam = { new OracleParameter("pValue", OracleType.NVarChar, 30),
                                         new OracleParameter("pVarName", OracleType.NVarChar, 30),
                                         new OracleParameter("pNote", OracleType.NVarChar, 100)
                                         };
                oraParam[0].Value = pValue;
                oraParam[1].Value = pVarName;
                oraParam[2].Value = pNote;
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

        public DataTable Get_Ontime(string pVarName)
        {
            string strSql = "select s.value from Sysvar s where Trim(s.varname) = '" + pVarName + "' and trim(s.gwtype)='SYSTEM'";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null).Tables[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



	}
	
	
}
