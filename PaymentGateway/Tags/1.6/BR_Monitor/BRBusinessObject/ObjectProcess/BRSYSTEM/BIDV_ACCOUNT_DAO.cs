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
    public class BIDV_ACCOUNTDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        public BIDV_ACCOUNTDP()
		{
		}
        public static BIDV_ACCOUNTDP Instance()
        {
            return new BIDV_ACCOUNTDP();
        }

        public int AddACCOUNT(BIDV_ACCOUNT_Info objTable)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pCCYCD",OracleType.VarChar,3),
                                                new OracleParameter("pBRANCH",OracleType.VarChar,10) ,
                                                new OracleParameter("pACCOUNT",OracleType.VarChar,100),
                                                };


                oraParas[0].Value = objTable.CCYCD;
                oraParas[1].Value = objTable.BRANCH;
                oraParas[2].Value = objTable.ACCOUNT;         
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_BIDV_ACCOUNT_ACCESS.ACCOUNT_Insert", oraParas);
                    }
                }
                catch //(Exception ex)
                {
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
                return iResult;

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

        public int UpdateACCOUNT(BIDV_ACCOUNT_Info objTable)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pID",OracleType.Number,19),
                                                new OracleParameter("pCCYCD",OracleType.VarChar,3),
                                                new OracleParameter("pBRANCH",OracleType.VarChar,10) ,
                                                new OracleParameter("pACCOUNT",OracleType.VarChar,100),
                                                };

                oraParas[0].Value  = objTable.ID ;
                oraParas[1].Value = objTable.CCYCD;
                oraParas[2].Value = objTable.BRANCH;
                oraParas[3].Value = objTable.ACCOUNT;
                
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_BIDV_ACCOUNT_ACCESS.ACCOUNT_Update", oraParas);
                }
                
                return iResult;

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

        public int DeleteACCOUNT(BIDV_ACCOUNT_Info objTable)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pID",OracleType.Number,19)                                                
                                                };


                oraParas[0].Value = objTable.ID ;
            

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_BIDV_ACCOUNT_ACCESS.ACCOUNT_Delete", oraParas);
                }

                return iResult;

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
        public DataSet SelectAccount(BIDV_ACCOUNT_Info objTable)
        {

            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }           
            OracleParameter[] oraParas ={new OracleParameter("pCCYCD",OracleType.VarChar,3),
                                                new OracleParameter("pBRANCH",OracleType.VarChar,10) ,
                                                new OracleParameter("pACCOUNT",OracleType.VarChar,100),
                                                 new OracleParameter("crsTmp",OracleType.Cursor)
                                                };


            oraParas[0].Value = objTable.CCYCD;
            oraParas[1].Value = objTable.BRANCH;
            oraParas[2].Value = objTable.ACCOUNT;
            oraParas[3].Direction = ParameterDirection.Output;              

            DataSet datResult = new DataSet();
            datResult = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_BIDV_ACCOUNT_ACCESS.ACCOUNT_Select", oraParas);         
            oraConn.Dispose();
            return datResult;           
            
        }

        public DataTable Get_BIDV(string pWHERE)//QUYND
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                string strSQL = " select ID,CCYCD,BRANCH,ACCOUNT from BIDV_ACCOUNT  " + pWHERE + "";                
                return  DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int Insert_BIDV(BIDV_ACCOUNT_Info objTable)//QUYND
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                string strSQL = " insert into BIDV_ACCOUNT(CCYCD,ACCOUNT,BRANCH) values ('" + objTable.CCYCD + "','" + objTable.ACCOUNT + "','990')";
               
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);

                //oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        public int Update_BIDV(BIDV_ACCOUNT_Info objTable)//QUYND
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                string strSQL = "UPDATE BIDV_ACCOUNT set CCYCD =:pCCYCD ,ACCOUNT =:pACCOUNT  Where ID=:pID";
                OracleParameter[] oraParas ={new OracleParameter("pID",OracleType.Number,19),
                                                new OracleParameter("pCCYCD",OracleType.VarChar,3),                                                
                                                new OracleParameter("pACCOUNT",OracleType.VarChar,100)                                                
                                                };
                oraParas[0].Value = objTable.ID;
                oraParas[1].Value = objTable.CCYCD;
                oraParas[2].Value = objTable.ACCOUNT;

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);

               // oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int Delete_BIDV(BIDV_ACCOUNT_Info objTable)//QUYND
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                string strSQL = "DELETE From BIDV_ACCOUNT Where ID=:pID";
                OracleParameter[] oraParas ={new OracleParameter("pID",OracleType.Number,19)                                                                                          
                                                };
                oraParas[0].Value = objTable.ID;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);

                //oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public DataTable GET_BIDV(string strField, string pCCYCD)//QUYND
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                //string strSQL = "select * from BIDV_ACCOUNT B where Trim(B.CCYCD)='" + pCCYCD + "'";
                string strSQL = "select ID,CCYCD,ACCOUNT,BRANCH from BIDV_ACCOUNT B where Trim(" + strField + ")='" + pCCYCD + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

                //oraConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
