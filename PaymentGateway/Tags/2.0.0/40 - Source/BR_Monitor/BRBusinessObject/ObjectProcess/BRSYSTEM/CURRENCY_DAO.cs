using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Data.OracleClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
//using BR.BRLib;
using BR.DataAccess;



//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:39
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class CURRENCYDP
	{

        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
		public CURRENCYDP()
		{
		}
		public static CURRENCYDP Instance()
		{
			return new CURRENCYDP();
		}

        public int AddCURRENCY(CURRENCYInfo objTable)
		{
            string strSql = "Insert into CURRENCYCODE(CCYCD,SHORTCD,CURRENCY,DECIMALS) values (:pCCYCD,:pSHORTCD,:pCURRENCY,:pDECIMALS)";
            OracleParameter[] oraParam = {new OracleParameter("pCCYCD", OracleType.VarChar, 3),
                                         new OracleParameter("pSHORTCD", OracleType.Char, 2),
                                         new OracleParameter("pCURRENCY", OracleType.NVarChar, 50),
                                         new OracleParameter("pDECIMALS", OracleType.Number, 1)};
            oraParam[0].Value = objTable.CCYCD;
            oraParam[1].Value = objTable.SHORTCD;
            oraParam[2].Value = objTable.CURRENCY;
            oraParam[3].Value = objTable.DECIMALS;
            
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

        public int UpdateCURRENCY(CURRENCYInfo objTable, string CCYCD, string ShortName)
        {
            /////////////////////////////////////////////////////////////-/
            //Muc dich: Sua cau lenh sql, loi do lay field ko dung
            //Ngay sua: 03/08/2008
            //string strSql = "Update CURRENCYCODE  set CURRENCY=:pCURRENCY,DECIMALS=:pDECIMALS " +
            //    " where upper(CCYCD)=upper('" + CCYCD + "') and upper(SHORTCD)=upper('" + ShortName + "')";            
            string strSql = "Update CURRENCYCODE  set CURRENCY=:pCURRENCY,DECIMALS=:pDECIMALS " +
                " where upper(SHORTCD)=upper('" + CCYCD + "') and upper(CCYCD)=upper('" + ShortName + "')";
            ///////////////////////////////////////////////////////////////
            OracleParameter[] oraParam = {new OracleParameter("pCURRENCY", OracleType.NVarChar, 50),
                                          new OracleParameter("pDECIMALS", OracleType.Number, 1)};

            oraParam[0].Value = objTable.CURRENCY;
            oraParam[1].Value = objTable.DECIMALS;

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

        public int DeleteCURRENCY(int iID)
		{
            string strSql = "Delete from CURRENCYCODE where id=:pid";
            OracleParameter[] oraParam = { new OracleParameter("pid", OracleType.Number, 10) };
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

        public int DeleteCURRENCY(string strCCYCD, string strGWTYPE, string strPARTNER)
        {
            string strSql = "Delete from CURRENCYCODE where CCYCD=:pCCYCD and GWTYPE=:pGWTYPE and PARTNER=:pPARTNER";
            OracleParameter[] oraParam = { new OracleParameter("pCCYCD", OracleType.NVarChar , 3),
                                             new OracleParameter("pGWTYPE", OracleType.NVarChar , 10),
                                             new OracleParameter("pPARTNER", OracleType.NVarChar , 10)
                                         };
            oraParam[0].Value = strCCYCD;
            oraParam[1].Value = strGWTYPE;
            oraParam[2].Value = strPARTNER;
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

        
        public DataSet GetCurrency()
        {
            DataSet datDs = new DataSet();
            //string strSql = "select ccy.id, ccy.CCYCD, ccy.CURRENCY, ccy.STATUS, ccy.PARTNER, ccy.TYPE, ccy.DECIMALS from Currency ccy";
            //string strSql = "select ccy.ID, ccy.CCYCD,ccy.SHORTCD,ccy.CURRENCY,ccy.DECIMALS,ccy.STATUS,Decode(ccy.STATUS,1,'Active',0,'Closed') statusV,ccy.PARTNER,ccy.gwtype from Currency ccy";
            string strSql = "select ccy.ID,ccy.SHORTCD,ccy.CCYCD,ccy.CURRENCY," +
                "ccy.DECIMALS from CURRENCYCODE ccy order by ccy.SHORTCD";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn,CommandType.Text,strSql,null)  ;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataTable GetCurrency1()
        {
            string strSql = "select distinct(ccy.CCYCD)  from Currencycode ccy order by ccy.CCYCD";
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

        public DataTable GetCurrency_Vcb()
        {
            string strSql = "select Distinct(cc.ccycd) from Currency cc where trim(cc.gwtype)='VCB' order by cc.CCYCD";
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


        public DataSet GetCurrency3()
        {
            DataSet datDs = new DataSet();
            //string strSql = "select ccy.id, ccy.CCYCD, ccy.CURRENCY, ccy.STATUS, ccy.PARTNER, ccy.TYPE, ccy.DECIMALS from Currency ccy";
            //string strSql = "select ccy.ID, ccy.CCYCD,ccy.SHORTCD,ccy.CURRENCY,ccy.DECIMALS,ccy.STATUS,Decode(ccy.STATUS,1,'Active',0,'Closed') statusV,ccy.PARTNER,ccy.gwtype from Currency ccy";
            string strSql = "select ccy.ID,ccy.SHORTCD,ccy.CCYCD,ccy.CURRENCY," +
                "ccy.DECIMALS from CURRENCYCODE ccy order by ccy.CCYCD";
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
        public DataTable GetCurrency2(string strGWTYPE)
        {
            string strSql = "select distinct(ccy.CCYCD)  from Currency ccy  Where Trim(ccy.gwtype)='" + strGWTYPE + "' order by ccy.CCYCD";
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
        public DataSet GetCurrency_code(string pCCYCD)
        {
            DataSet datDs = new DataSet();
            //string strSql = "select ccy.id, ccy.CCYCD, ccy.CURRENCY, ccy.STATUS, ccy.PARTNER, ccy.TYPE, ccy.DECIMALS from Currency ccy";
            //string strSql = "select ccy.ID, ccy.CCYCD,ccy.SHORTCD,ccy.CURRENCY,ccy.DECIMALS,ccy.STATUS,Decode(ccy.STATUS,1,'Active',0,'Closed') statusV,ccy.PARTNER,ccy.gwtype from Currency ccy";
            string strSql = "select cc.ID,cc.gwtype from currencycode cc where trim(cc.ccycd)='" + pCCYCD + "'";
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

        public DataSet GetCurrency_code2(string pShortCD)
        {
            DataSet datDs = new DataSet();
            //string strSql = "select ccy.id, ccy.CCYCD, ccy.CURRENCY, ccy.STATUS, ccy.PARTNER, ccy.TYPE, ccy.DECIMALS from Currency ccy";
            //string strSql = "select ccy.ID, ccy.CCYCD,ccy.SHORTCD,ccy.CURRENCY,ccy.DECIMALS,ccy.STATUS,Decode(ccy.STATUS,1,'Active',0,'Closed') statusV,ccy.PARTNER,ccy.gwtype from Currency ccy";
            string strSql = "select * from currencycode cc where upper(trim(cc.SHORTCD))='" + pShortCD.Trim().ToUpper() + "'";
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

        public DataSet GetCurrencySearch(string strSQL)
        {
            DataSet datDs = new DataSet();
            string strSql = "select ccy.ID, ccy.SHORTCD,ccy.CCYCD,ccy.CURRENCY,ccy.DECIMALS" +
                " from Currencycode ccy where 1=1 " + strSQL + " order by ccy.SHORTCD";
           // string strSql = "select * from Currency";
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
        //Kiem tra Status cua kenh TTSP cua Partner trong bang Branch
        public DataSet GetCurrencyStatusTTSP(string strPartner)
        {
            DataSet datDs = new DataSet();
            string strSql = "select ccy.ID, ccy.SHORTCD,ccy.CCYCD,ccy.DECIMALS" +
                " from Currency ccy where upper(trim(ccy.gwtype))='TTSP' and trim(ccy.status)=1 and trim(ccy.partner)='" + strPartner + "' order by ccy.SHORTCD";
            // string strSql = "select * from Currency";
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
