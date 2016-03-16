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
//' Create date:	12/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 12/06/2008
//' =============================================

namespace BR.BRBusinessObject
{
    public class SWIFT_RMBR_AUTODP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        public SWIFT_RMBR_AUTODP()
		{
		}
        public static SWIFT_RMBR_AUTODP Instance()
		{
            return new SWIFT_RMBR_AUTODP();
		}
        public int ADDSWIFT_RMBR_AUTO(SWIFT_RMBR_AUTOInfo objTable)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pORG_BRAN",OracleType.VarChar,5),
                                                new OracleParameter("pRECEIVER_BRAN",OracleType.VarChar,5)};
                oraParas[0].Value = objTable.ORG_BRAN;
                oraParas[1].Value = objTable.RECEIVER_BRAN;
                string strSql = "Insert into Swift_RMBr_Auto  (ORG_BRAN,RECEIVER_BRAN) values (:pORG_BRAN,:pRECEIVER_BRAN)";
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

        public DataTable Check_RMBr(string pORG_BRAN, string pRECEIVER_BRAN)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select * from Swift_RMBr_Auto s where Trim(s.org_bran)='" + pORG_BRAN + "' ";//and Trim(s.receiver_bran)='" + pRECEIVER_BRAN + "'
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }


        public DataTable MAP_AUTO(string pORG_BRAN)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select SM.RECEIVER_BRAN from SWIFT_RMBR_AUTO SM where trim(SM.ORG_BRAN)=trim('" + pORG_BRAN + "') and rownum =1";            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public DataTable Check_RMBr()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select s.org_bran,s.receiver_bran from Swift_RMBr_Auto s";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable Get_Rmbr()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select s.org_bran,s.receiver_bran from Swift_RMBr_Auto s";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable Search(string pOrg_bran)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select s.org_bran,s.receiver_bran from Swift_RMBr_Auto s where Trim(s.org_bran)='" + pOrg_bran + "'";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public int Delete(string pORG_BRANOLD, string pRECEIVER_BRANOLD)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "Delete from  SWIFT_RMBR_AUTO  where Trim(ORG_BRAN)='" + pORG_BRANOLD + "' and Trim(RECEIVER_BRAN)='" + pRECEIVER_BRANOLD + "' ";
            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return 1;
            }
        }

        public int Update_Brauto(string pORG_BRANOLD, string pRECEIVER_BRANOLD,string pORG_BRAN, string pRECEIVER_BRAN)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "Update SWIFT_RMBR_AUTO S set S.ORG_BRAN='" + pORG_BRAN + "',S.RECEIVER_BRAN='" + pRECEIVER_BRAN + "' where Trim(ORG_BRAN)='" + pORG_BRANOLD + "' and Trim(RECEIVER_BRAN)='" + pRECEIVER_BRANOLD + "' ";
            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return 1;
            }
        }
    }
}
