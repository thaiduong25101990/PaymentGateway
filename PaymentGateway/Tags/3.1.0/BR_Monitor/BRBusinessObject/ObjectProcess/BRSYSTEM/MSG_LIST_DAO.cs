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
	public class MSG_LISTDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();	
		public MSG_LISTDP()
		{
		}
		public static MSG_LISTDP Instance()
		{
			return new MSG_LISTDP();
		}
		
		public int AddMSG_LIST(MSG_LISTInfo objTable)
		{
            string strSql = "Insert into MSG_LIST  (MSG_DEF_ID,MSG_DESCRIPTION,SIBS_MSG_LENGTH,GW_MSG_LENGTH) values (:pMSG_DEF_ID,:pMSG_DESCRIPTION,:pSIBS_MSG_LENGTH,:pGW_MSG_LENGTH)";
            OracleParameter[] oraParam = {new OracleParameter("pMSG_DEF_ID", OracleType.NVarChar, 20),
                                         new OracleParameter("pMSG_DESCRIPTION",OracleType.NVarChar,70),
                                         new OracleParameter("pSIBS_MSG_LENGTH",OracleType.Number,5),
                                         new OracleParameter("pGW_MSG_LENGTH", OracleType.Number,5)};

            oraParam[0].Value = objTable.MSG_DEF_ID;
            oraParam[1].Value = objTable.MSG_DESCRIPTION;
            oraParam[2].Value = objTable.SIBS_MSG_LENGTH;
            oraParam[3].Value = objTable.GW_MSG_LENGTH;

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
		
		public int UpdateMSG_LIST(MSG_LISTInfo objTable)
		{
            string strSql = "Update MSG_LIST  set MSG_DEF_ID=:pMSG_DEF_ID,MSG_DESCRIPTION=:pMSG_DESCRIPTION,";
            strSql = strSql + " SIBS_MSG_LENGTH=:pSIBS_MSG_LENGTH,GW_MSG_LENGTH=:pGW_MSG_LENGTH";
            strSql = strSql + " where MSG_LIST_ID=:pMSG_LIST_ID";
            OracleParameter[] oraParam = {new OracleParameter("pMSG_LIST_ID", OracleType.Number, 5),
                                         new OracleParameter("pMSG_DEF_ID", OracleType.NVarChar, 20),
                                         new OracleParameter("pMSG_DESCRIPTION",OracleType.NVarChar,70),
                                         new OracleParameter("pSIBS_MSG_LENGTH",OracleType.Number, 5),
                                         new OracleParameter("pGW_MSG_LENGTH", OracleType.Number, 5)};
			try
			{
                oraParam[0].Value = objTable.MSG_LIST_ID;
                oraParam[1].Value = objTable.MSG_DEF_ID;
                oraParam[2].Value = objTable.MSG_DESCRIPTION;
                oraParam[3].Value = objTable.SIBS_MSG_LENGTH;
                oraParam[4].Value = objTable.GW_MSG_LENGTH;

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

        public int DeleteMSG_LIST(int iID)
		{
            string strSql = "Delete from MSG_LIST where MSG_LIST_ID=:pMSG_LIST_ID";
            OracleParameter[] oraParam = { new OracleParameter("pMSG_LIST_ID", OracleType.Int32, 5) };
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

        public DataSet GetMSGList()
        {
            DataSet datDs = new DataSet();
            string strSql = "select list.MSG_LIST_ID, list.MSG_DEF_ID,list.MSG_DESCRIPTION,list.SIBS_MSG_LENGTH,list.GW_MSG_LENGTH from MSG_LIST list order by list.MSG_LIST_ID";
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
