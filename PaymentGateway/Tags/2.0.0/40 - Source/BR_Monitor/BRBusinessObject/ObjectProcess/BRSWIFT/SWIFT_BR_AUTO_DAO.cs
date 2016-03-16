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
    public class SWIFT_BR_AUTODP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        public SWIFT_BR_AUTODP()
		{
		}
        public static SWIFT_BR_AUTODP Instance()
		{
            return new SWIFT_BR_AUTODP();
		}

        public int AddSWIFT_BR_AUTO(SWIFT_BR_AUTOInfo objTable)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pTMTREF",OracleType.VarChar,20),
                                                new OracleParameter("pTMPRBR",OracleType.VarChar,3)};                
                oraParas[0].Value = objTable.TMTREF;
                oraParas[1].Value = objTable.TMPRBR;
                string strSql = "Insert into SWIFT_BR_AUTO  (TMTREF,TMPRBR) values (:pTMTREF,:pTMPRBR)";
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

        public DataTable Check_Br(string pWhere)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select ID,TMTREF,TMPRBR from Swift_Br_Auto " + pWhere;
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
