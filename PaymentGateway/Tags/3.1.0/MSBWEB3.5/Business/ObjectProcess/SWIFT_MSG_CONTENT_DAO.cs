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
using BIDVWEB.Comm;
using System.Data.OracleClient;
/*=============================================
 Author:
 Create date:	11/04/2010
 Description:
 Revise History:
 =============================================*/
namespace BIDVWEB.Business
{
    public class SWIFT_MSG_CONTENTDP
    {
        private OracleConnection oraConn;
        private clsConnection objConn = new clsConnection();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        
        public SWIFT_MSG_CONTENTDP()
        {
        }
        public static SWIFT_MSG_CONTENTDP Instance()
        {
            return new SWIFT_MSG_CONTENTDP();
        }
        
        /*//////////////////////////////////////////////////
        Ten ham: Update_Print_STS
        Mo ta: Ham cap nhat trang thai in dien
        Ngay tao: 06/02/2010
        Nguoi tao: Huypq7         
        //////////////////////////////////////////////////*/
        public int Update_Print_STS(SWIFT_MSG_CONTENTInfo objTable)
        {
            string strSql = "GW_PK_SWIFT.SWIFT_UPDATE_PRINT_STS";

            OracleParameter[] oraParam = {new OracleParameter("pPrint_STS", OracleType.Number,1),
                                         new OracleParameter("pMSG_ID", OracleType.Number,20)};
            oraParam[0].Value = objTable.PRINT_STS;
            oraParam[1].Value = objTable.MSG_ID;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                return clsDatatAccess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        /* //////////////////////////////////////////////////
         * Ten ham: Check_Print_STS
         * Mo ta: Ham kiem tra trang thai in dien
         * Ngay tao: 06/02/2010
         * Nguoi tao: Huypq7         
         //////////////////////////////////////////////////*/
        public int Check_Print_STS(SWIFT_MSG_CONTENTInfo objTable)
        {
            string strSql = "GW_PK_SWIFT.CHECK_SWIFT_PRINT_STS";

            OracleParameter[] oraParam = {new OracleParameter("pMSG_ID", OracleType.Number,20),
                                          new OracleParameter("pPrint_STS", OracleType.Number,1)
                                         };
            oraParam[0].Value = objTable.MSG_ID;
            oraParam[1].Direction = ParameterDirection.Output;

            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                clsDatatAccess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, strSql, oraParam);
                return Convert.ToInt16(oraParam[1].Value.ToString());
            }
            catch (Exception ex)
            {
                return -1; ;
            }
        }  
    }
}
