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
    public class DATABASE_CLEANDP
    {
         private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        //private OracleConnection cnn=new OracleConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\book1.xls;Extended Properties='Excel 8.0'");
        
        public DATABASE_CLEANDP()
		{
		}
        public static DATABASE_CLEANDP Instance()
		{
            return new DATABASE_CLEANDP();
		}
        public DataSet Delete_table(string pTableName)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "Delete from " + pTableName + "";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetTable()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select u.table_name from User_Tables u";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetColumns_table(string pTableName)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select uc.column_name,uc.comments from user_col_comments uc where uc.table_name='" + pTableName + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetValue_colum(string pTableName,string pColumnName)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select distinct " + pColumnName + "  from  " + pTableName + " ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public int Check_Query(string pQuery)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = pQuery ;

            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
            return 1;
        }
        public DataSet GetComment_table(string strTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select ut.comments from User_Tab_Comments ut where ut.table_name= '" + strTable + "'";//pQuery;

            try
            {
              return  DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }       
        }      
    }
}
