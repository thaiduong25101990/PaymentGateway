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
namespace BR.BRBusinessObject
{    
    public class VCB_PARAMETERSDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();        
        
        public VCB_PARAMETERSDP()
        {
        }
        public static VCB_PARAMETERSDP Instance()
        {
            return new VCB_PARAMETERSDP();
        }
        public int Add(VCB_PARAMETERSInfo objTable)
        {
            int iResult = 0;
            try
            {
                string strSql = "Insert into VCB_PARAMETER  (DEPARTMENT,MSG_TYPE,BANK_CODE,GWTYPE) values (:pDEPARTMENT,:pMSG_TYPE,:pBANK_CODE,:pGWTYPE)";
                OracleParameter[] oraParas ={new OracleParameter("pMSG_TYPE",OracleType.VarChar,8),
                                            new OracleParameter("pBANK_CODE",OracleType.VarChar,12),
                                            new OracleParameter("pDEPARTMENT",OracleType.VarChar,10),
                                            new OracleParameter("pGWTYPE",OracleType.VarChar,10)};
                oraParas[0].Value = objTable.MSG_TYPE;
                oraParas[1].Value = objTable.BANK_CODE;
                oraParas[2].Value = objTable.DEPARTMENT;
                oraParas[3].Value = objTable.GWTYPE;
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
                    }
                }
                catch 
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
        }
        public int Update(VCB_PARAMETERSInfo objTable)
        {
            int iResult = 0;
            try
            {
                string strSql = "update VCB_PARAMETER  set DEPARTMENT = :pDEPARTMENT,MSG_TYPE   = :pMSG_TYPE, BANK_CODE  = :pBANK_CODE where ID=:pID";
                OracleParameter[] oraParas ={new OracleParameter("pMSG_TYPE",OracleType.VarChar,8),
                                            new OracleParameter("pBANK_CODE",OracleType.VarChar,12),
                                            new OracleParameter("pDEPARTMENT",OracleType.VarChar,10),
                                            new OracleParameter("pID",OracleType.VarChar,10)};
                oraParas[0].Value = objTable.MSG_TYPE;
                oraParas[1].Value = objTable.BANK_CODE;
                oraParas[2].Value = objTable.DEPARTMENT;
                oraParas[3].Value = objTable.ID;

                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParas);
                    }
                }
                catch 
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
        }        

        public int Delete(string pID)
        {
            int iResult = 0;
            try
            {
                string strSql = "Delete  from VCB_PARAMETER where ID = '" + pID + "'";                
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
                    }
                }
                catch 
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
        }
        public DataTable Search()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select VP.ID,VP.DEPARTMENT,VP.MSG_TYPE,VP.BANK_CODE,VP.GWTYPE from  VCB_PARAMETER VP ";          
            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable check(string Depart, string Channel, string Msg_type, string Bank_code)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select * from VCB_PARAMETER v where trim(v.department) ='" + Depart + "' and trim(v.msg_type)='" + Msg_type + "' and trim(v.bank_code)='" + Bank_code + "' and  trim(v.GWTYPE)='" + Channel + "'";

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
