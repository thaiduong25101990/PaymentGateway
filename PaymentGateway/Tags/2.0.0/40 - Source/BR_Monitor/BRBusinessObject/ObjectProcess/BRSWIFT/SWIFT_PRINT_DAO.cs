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

    public class SWIFTPRINTDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process connect = new Connect_Process();
        public SWIFTPRINTDP()
		{
		}
        public static SWIFTPRINTDP Instance()
        {
            return new SWIFTPRINTDP();
        }
      

        public DataSet Search(string pCondition, DateTime pDateFrom, DateTime pDateTo)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "select distinct statement_id,to_char(statement_time,'DD/MM/YYYY') as statement_time,teller_name from swift_iw_statement ";           
            
            strSQL = strSQL + " where to_date(statement_time)>= ";
            strSQL = strSQL + "to_date(:pDateFrom) and to_date(statement_time)<= to_date(:pDateTo) ";
            strSQL = strSQL + pCondition;
            strSQL = strSQL + " order by statement_time";

             OracleParameter[] oraParas ={new OracleParameter("pDateFrom",OracleType.DateTime,7),
                                            new OracleParameter("pDateTo",OracleType.DateTime,7)
                                        }; 
            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet LoaddataGrid(DateTime pDateFrom, DateTime pDateTo)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "select distinct statement_id,to_char(statement_time,'DD/MM/YYYY') as statement_time,teller_name from swift_iw_statement ";           
            
            strSQL = strSQL + "  where to_date(statement_time)>= ";
            strSQL = strSQL + "to_date(:pDateFrom) and to_date(statement_time)<= to_date(:pDateTo) order by statement_time";
                        
             OracleParameter[] oraParas ={new OracleParameter("pDateFrom",OracleType.DateTime,7),
                                            new OracleParameter("pDateTo",OracleType.DateTime,7)
                                        };
            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet LoadSWIFT_IN_RM(DateTime pDateFrom, DateTime pDateTo)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "select distinct statement_id,to_char(statement_time,'DD/MM/YYYY') as statement_time,teller_id from swift_in_rm ";

            strSQL = strSQL + "  where to_date(statement_time)>= ";
            strSQL = strSQL + "to_date(:pDateFrom) and to_date(statement_time)<= to_date(:pDateTo) order by statement_time";
            OracleParameter[] oraParas ={new OracleParameter("pDateFrom",OracleType.DateTime,7),
                                            new OracleParameter("pDateTo",OracleType.DateTime,7)
                                        };
            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }
      
        public int Delete_Resend(string pQUERYID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "Delete from SWIFT_IW_STATEMENT S where S.QUERY_ID='" + pQUERYID + "' and  S.Statement_Id = (select Max(Statement_Id) from SWIFT_IW_STATEMENT where QUERY_ID='" + pQUERYID + "')";
            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);

            }
            catch //(Exception ex)
            {
                return -1;
            }
        }
    
        public DataTable Search_QueryID(string pQUERY_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            //string strSQL = "select * from SWIFT_IW_STATEMENT SM Where Trim(SM.QUERY_ID)='" + pQUERY_ID.Trim() + "'";
            string strSQL = "select QUERY_ID,RECEIVING_TIME,STATEMENT_ID,BRANCH_A ,MSG_TYPE,FIELD20,FIELD21,";
            strSQL = strSQL + "  FIELD32,FIELD57,FIELD58,FIELD59,BRANCH_B,DEPARTMENT,ERROR_CODE,TELLER_ID,";
            strSQL = strSQL + "  OFFICER_ID,STATEMENT_TIME,MSG_STATUS,TELLER_NAME,STATEMENT_TYPE  from ";
            strSQL = strSQL + "  SWIFT_IW_STATEMENT SM Where Trim(SM.QUERY_ID)='" + pQUERY_ID.Trim() + "'";
            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch //(Exception ex)
            {
                return null;
            }
        }
     

        public DataSet SearchSWIFT_IN_RM(string pCondition, DateTime pDateFrom, DateTime pDateTo)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "select distinct statement_id,to_char(statement_time,'DD/MM/YYYY') as statement_time,teller_id from SWIFT_IN_RM ";

            strSQL = strSQL + " where to_date(statement_time)>= ";
            strSQL = strSQL + "to_date(:pDateFrom) and to_date(statement_time)<= to_date(:pDateTo) ";
            strSQL = strSQL + pCondition;
            strSQL = strSQL + " order by statement_time";

            OracleParameter[] oraParas ={new OracleParameter("pDateFrom",OracleType.DateTime,7),
                                            new OracleParameter("pDateTo",OracleType.DateTime,7)
                                        };
            oraParas[0].Value = pDateFrom;
            oraParas[1].Value = pDateTo;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetData_Reprint(int StatementID, string time_print)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = " select * from SWIFT_IW_STATEMENT a where trim(a.statement_id)=trim(" + StatementID + ") and substr(to_char(STATEMENT_TIME,'DD/MM/YYYY'),0,10)= substr('" + time_print + "',0,10)";          

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable GetData_Reprint_RM(int StatementID, string time_print)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = " select * from SWIFT_IN_RM a where trim(a.statement_id)=trim(" + StatementID + ") and substr(to_char(STATEMENT_TIME,'DD/MM/YYYY'),0,10)= substr('" + time_print + "',0,10)";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public int Update_statement_type(string pQuery_id)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }

            string strSQL = "UPDATE SWIFT_IW_STATEMENT ST set ST.STATEMENT_TYPE='RESEND_UD' where ST.QUERY_ID='" + pQuery_id + "' and ST.Statement_Type='RESEND'";

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);

            }
            catch //(Exception ex)
            {
                return -1;
            }
        }
        public DataTable PRINT_STATEMENT_MANUAL(string teller)
        {
            try
            {
                string strSQL = " gw_pk_SWIFT_report.swift_03 ";

                OracleParameter[] oraParas ={new OracleParameter("pTellerID",OracleType.VarChar,8),                                            
                                            new OracleParameter("RefCur_SWIFT_03",OracleType.Cursor)};

                oraParas[0].Value = teller;              
                oraParas[1].Direction = ParameterDirection.Output;


                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas).Tables[0];
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataTable PRINT_STATEMENT_RESEND(string teller)
        {
            try
            {
                string strSQL = " gw_pk_SWIFT_report.swift_03_resend ";

                OracleParameter[] oraParas ={new OracleParameter("pTellerID",OracleType.VarChar,8),                                            
                                            new OracleParameter("RefCur_SWIFT_03_RESEND",OracleType.Cursor)};

                oraParas[0].Value = teller;
                oraParas[1].Direction = ParameterDirection.Output;


                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas).Tables[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
