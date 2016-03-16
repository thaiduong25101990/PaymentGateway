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
    public class CURRENCY_CHANNELDP
    {
        private static OracleConnection oraConn = new OracleConnection();
        private static Connect_Process conn = new Connect_Process();

        public CURRENCY_CHANNELDP()
        {
        }

        public static CURRENCY_CHANNELDP Instance()
        {
            return new CURRENCY_CHANNELDP();
        }

        public int AddCURRENCY(CURRENCY_CHANNELInfo objTable)
        {
            string strSql = "Insert into Currency  (CCYCD,SHORTCD,DECIMALS,STATUS,PARTNER,GWTYPE) values (:pCCYCD,:pSHORTCD,:pDECIMALS,:pSTATUS,:pPARTNER,:pTYPE)";
            OracleParameter[] oraParam = {new OracleParameter("pCCYCD", OracleType.VarChar, 3),
                                         new OracleParameter("pSHORTCD", OracleType.Char, 2),                                         
                                         new OracleParameter("pDECIMALS", OracleType.Number, 2),
                                         new OracleParameter("pSTATUS", OracleType.Number, 1),
                                         new OracleParameter("pPARTNER", OracleType.NVarChar, 10),
                                         new OracleParameter("pTYPE", OracleType.NVarChar, 10)};
            oraParam[0].Value = objTable.CCYCD;
            oraParam[1].Value = objTable.SHORTCD;
            oraParam[2].Value = objTable.DECIMALS;
            oraParam[3].Value = objTable.STATUS;
            oraParam[4].Value = objTable.PARTNER;
            oraParam[5].Value = objTable.GWTYPE;

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

        //public int UpdateCURRENCY(CURRENCY_CHANNELInfo objTable, string CCYCD, string strGwtype)
        //{
        //    string strSql = "Update Currency  set CURRENCY=:pCURRENCY,DECIMALS=:pDECIMALS,STATUS=:pSTATUS,PARTNER=:pPARTNER where SHORTCD='" + CCYCD + "' and GWTYPE='" + strGwtype + "'";
        //    OracleParameter[] oraParam = { //new OracleParameter("pID",OracleType.Number,10),
        //                                 //new OracleParameter("pCCYCD", OracleType.Char, 3),
        //                                 //new OracleParameter("pSHORTCD", OracleType.Char, 2),
        //                                 new OracleParameter("pCURRENCY", OracleType.NVarChar, 30),
        //                                 new OracleParameter("pDECIMALS", OracleType.Number, 1),
        //                                 new OracleParameter("pSTATUS", OracleType.Number, 1),
        //                                 new OracleParameter("pPARTNER", OracleType.NVarChar, 10)};
        //    //new OracleParameter("pTYPE", OracleType.NVarChar, 10)};
        //    //oraParam[0].Value = objTable.ID;
        //    //oraParam[1].Value = objTable.CCYCD;
        //    //oraParam[2].Value = objTable.SHORTCD;
        //    oraParam[0].Value = objTable.CURRENCY;
        //    oraParam[1].Value = objTable.DECIMALS;
        //    oraParam[2].Value = objTable.STATUS;
        //    oraParam[3].Value = objTable.PARTNER;
        //    //oraParam[5].Value = objTable.GWTYPE;
        //    try
        //    {
        //        oraConn = conn.Connect();
        //        if (oraConn == null)
        //            return -1;
        //        return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        public int UpdateCURRENCY(CURRENCY_CHANNELInfo objTable)
        {
            string strSql = "Update Currency  set STATUS=:pSTATUS,PARTNER=:pPARTNER  where CCYCD=:pCCYCD and GWTYPE=:pGWTYPE AND ID=:pID";
            OracleParameter[] oraParam = { //new OracleParameter("pID",OracleType.Number,10),
                                         //new OracleParameter("pCCYCD", OracleType.Char, 3),
                                         //new OracleParameter("pSHORTCD", OracleType.Char, 2),
                                         new OracleParameter("pSTATUS", OracleType.Number, 1),
                                         new OracleParameter("pPARTNER", OracleType.NVarChar, 5),
                                         new OracleParameter("pCCYCD", OracleType.NVarChar, 3),
                                         new OracleParameter("pGWTYPE", OracleType.NVarChar, 30)//,
                                         //new OracleParameter("pDECIMALS", OracleType.Number , 1),
                                         //new OracleParameter("pSHORTCD", OracleType.NVarChar, 2)
                                         };
            
            oraParam[0].Value = objTable.STATUS ;
            oraParam[1].Value = objTable.PARTNER ;
            oraParam[2].Value = objTable.CCYCD ;
            oraParam[3].Value = objTable.GWTYPE ;
            //oraParam[4].Value = objTable.DECIMALS;
            //oraParam[5].Value = objTable.SHORTCD;

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


        public int UpdateCURRENCY_STATUS(CURRENCY_CHANNELInfo objTable)
        {
            string strSql = "Update Currency  set STATUS=:pSTATUS,PARTNER=:pPARTNER where ID=:pID";
            OracleParameter[] oraParam = { 
                                         new OracleParameter("pSTATUS", OracleType.Number, 1),
                                         new OracleParameter("pPARTNER", OracleType.VarChar, 5),
                                         new OracleParameter("pID", OracleType.Number , 10)
                                         };

            oraParam[0].Value = objTable.STATUS;
            oraParam[1].Value = objTable.PARTNER;
            oraParam[2].Value = objTable.ID;
            

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

        public int DeleteCURRENCY(int iID, out string strOut)
        {
            string strSql;
            strOut = "";            
            try
            {
                ///////////////////////////////////////////////////////-/
                //Muc dich: Kiem tra neu status = close moi xoa
                //Ngay sua: 02/08/2008
                /////////////////////////////////////////////////////////                

                DataSet ds = new DataSet();
                if (oraConn == null)
                    return -1;
                oraConn = conn.Connect();
                strSql = "SELECT ID,CCYCD,SHORTCD,DECIMALS,STATUS,PARTNER, GWTYPE  FROM Currency WHERE STATUS = 1 and id=" + iID;
                ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    strOut = "Currency is active!";
                    return -1;
                }

                oraConn = conn.Connect();
                strSql = "Delete from Currency where id=:pid";
                OracleParameter[] oraParam = { new OracleParameter("pid", OracleType.Number, 10) };
                oraParam[0].Value = iID;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch (Exception ex)
            {
                strOut = "";
                throw (ex);
            }
        }

        public int DeleteCURRENCY(string strCCYCD, string strGWTYPE, string strPARTNER)
        {
            string strSql = "Delete from CURRENCY where CCYCD=:pCCYCD and GWTYPE=:pGWTYPE and PARTNER=:pPARTNER";
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
            string strSql = "select ccy.ID,ccy.SHORTCD, ccy.CCYCD,ccy.CURRENCY,ccy.DECIMALS,ccy.STATUS,alStatus.Content CCYStatus,ccy.PARTNER,ccy.gwtype from Currency ccy, (select alc.cdval, alc.content, alc.cdname from allcode alc Where alc.cdname = 'Connection') allc, (select alc.cdval, alc.content, alc.cdname from allcode alc Where alc.cdname = 'CCYSTS') alStatus where ccy.status = allc.cdval(+) And ccy.status = alStatus.Cdval(+) order by ccy.SHORTCD";
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

        //public DataSet GetCurrency(CURRENCY_CHANNELInfo obj)
        //{
        //    DataSet datDs = new DataSet();
        //    string strSql = "Select * from Currency where CCYCD=:pCCYCD and PARTNER=:pPARTNER and STATUS=:pSTATUS";
        //    OracleParameter[] oraParam = {new OracleParameter("pCCYCD", OracleType.Number , 5),
        //                                 new OracleParameter("pPARTNER", OracleType.NVarChar, 5),                                                                             
        //                                 new OracleParameter("pSTATUS", OracleType.Char , 1)};
        //    oraParam[0].Value = obj.CCYCD ;
        //    oraParam[1].Value = obj.PARTNER;
        //    oraParam[2].Value = obj.STATUS;
           

        //    try
        //    {
        //        oraConn = conn.Connect();
        //        if (oraConn == null)
        //            return null;

        //        return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //        oraConn.Dispose();
        //    }
        //}

        public DataSet GetCurrency(CURRENCY_CHANNELInfo obj)
        {
            string strSql;
            strSql = "Select * from Currency where trim(CCYCD)='" + obj.CCYCD.Trim() + "' and trim(GWTYPE) ='" + obj.GWTYPE.Trim();
            if (obj.PARTNER != null)
                strSql =strSql + "' and trim(PARTNER)='" + obj.PARTNER.Trim() + "'";
            else
                strSql = strSql + "' and PARTNER is null";                 

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;           
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null );
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


        public DataTable GetCurrency_Import(string pGwtype)
        {
            string strSql;
            strSql = "select Currency.Ccycd from Currency where Currency.Gwtype='" + pGwtype + "'";
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
            finally
            {
                oraConn.Dispose();
            }
        }





        public DataSet CheckEditCurrency(CURRENCY_CHANNELInfo obj)
        {
            string strSql;
            strSql = "Select * from Currency where trim(CCYCD)='" + obj.CCYCD.Trim() + "' and trim(GWTYPE) ='" + obj.GWTYPE.Trim();
            if (obj.PARTNER != null)
                strSql = strSql + "' and trim(PARTNER)='" + obj.PARTNER.Trim() + "' And Id <>" + obj.ID ;
            else
                strSql = strSql + "' and PARTNER is null And Id <>" + obj.ID;

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
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataTable GetCurrency1()
        {
            string strSql = "select distinct(ccy.CCYCD)  from Currency ccy order by ccy.CCYCD";
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

        public DataSet GetCurrency_code(string pSHORTCD)
        {
            DataSet datDs = new DataSet();
            //string strSql = "select ccy.id, ccy.CCYCD, ccy.CURRENCY, ccy.STATUS, ccy.PARTNER, ccy.TYPE, ccy.DECIMALS from Currency ccy";
            //string strSql = "select ccy.ID, ccy.CCYCD,ccy.SHORTCD,ccy.CURRENCY,ccy.DECIMALS,ccy.STATUS,Decode(ccy.STATUS,1,'Active',0,'Closed') statusV,ccy.PARTNER,ccy.gwtype from Currency ccy";
            string strSql = "select * from currency cc where trim(cc.ccycd) ='" + pSHORTCD + "'";
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

        public DataSet GetCurrencySearch(string strWhere)
        {
           DataSet datDs = new DataSet();           
           string strSql = " select ccy.CCYCD,ccycd.SHORTCD,ccycd.currency,ccy.decimals,ccy.status, " ;
                    strSql = strSql + " (select Allcode.Content from Allcode where CDNAME='CCYSTS' and CDVAL=ccy.status And rownum=1) content, ";
                    strSql = strSql + " ccy.partner,ccy.GWType,ccy.ID from Currency ccy, Currencycode ccycd " + strWhere;
                    strSql = strSql + " and ccy.CCYCD = ccycd.CCYCD ";
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
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataSet GetCurrencySearch_Correct(string strWhere)
        {
            DataSet datDs = new DataSet();
       

            string strSql1 = "(select ccy.CCYCD,ccycd.SHORTCD, ccycd.currency, ccycd.decimals,ccy.status, acd.content, ccy.partner, ccy.GWType from Currency ccy, Currencycode ccycd, Allcode acd  " + strWhere;
            strSql1 = strSql1 + " and ccy.CCYCD = ccycd.CCYCD and ccy.status= acd.cdval and acd.cdname='CCYSTS' order by ccy.CCYCD, ccy.status,ccy.GWType ) A";

            string strSql="Select A.CCYCD,A.SHORTCD, A.currency, A.decimals,A.status, A.content, A.GWType from ";
            strSql = strSql + strSql1 + " group by CCYCD, GWType,SHORTCD,currency,decimals ,status,content ";
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
            finally
            {
                oraConn.Dispose();
            }
        }

        
    }
}
