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
using BR.DataAccess;

//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class DEPARTMENTDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();	
	
		public DEPARTMENTDP()
		{
		}
		public static DEPARTMENTDP Instance()
		{
			return new DEPARTMENTDP();
		}
		
		public int AddDEPARTMENT(DEPARTMENTInfo objTable)
		{
            string strSQL = "Insert into Department (Dept_code,Dept_name) values (:pDept_code,:pDept_name)";
            OracleParameter[] oraParam={new OracleParameter("pDept_code",OracleType.NVarChar,3),
                                        new OracleParameter("pDept_name",OracleType.NVarChar,50)};
            oraParam[0].Value=objTable.DEPT_CODE;
            oraParam[1].Value=objTable.DEPT_NAME;
			try
			{
                oraConn =conn.Connect();
                if (oraConn==null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn,CommandType.Text,strSQL,oraParam);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		
		public int UpdateDEPARTMENT(DEPARTMENTInfo objTable)
		{
            string strSQL = "Update Department set Dept_code=:pDept_code, Dept_name = :pDept_name where Dept_ID=:pDept_ID";
            OracleParameter[] oraParam ={new OracleParameter("pDept_ID",OracleType.Number,3),
                                            new OracleParameter("pDept_code",OracleType.NVarChar,3),
                                           new OracleParameter("pDept_name",OracleType.NVarChar,50)};
            oraParam[0].Value = objTable.DEPT_ID;
            oraParam[1].Value = objTable.DEPT_CODE;
            oraParam[2].Value = objTable.DEPT_NAME;
			try
			{
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		
		public int DeleteDEPARTMENT(int iID)
		{
            string strSQL = "Delete from Department where Dept_ID=:pDept_ID";
            OracleParameter[] oraParam = { new OracleParameter("pDept_ID", OracleType.Number, 3) };
            oraParam[0].Value = iID;
			try
			{
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

        public DataSet GetDEPARTMENT()
        {
            DataSet datDs = new DataSet();
            string strSQL = "Select * from DEPARTMENT";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text,strSQL, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
		
	}
	
	
}
