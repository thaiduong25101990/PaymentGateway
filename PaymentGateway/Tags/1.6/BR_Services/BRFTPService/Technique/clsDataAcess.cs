using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Transactions;


namespace BRFTPService.Technique
{
    /***************************************
     * Cung cap cac ham truy cap database
     ***************************************/
    public class DataAcess
    {
        public bool loginOK;
        private  OracleConnection m_orcConn = new OracleConnection();//Khoi connection
        private  Connect_Process connect = new Connect_Process();

        public DataAcess()
        {         
        }
        // Select du lieu vao mot DataTable
        public DataTable GetSelect(string strSQL)
        {
            OracleDataAdapter da=null;
            OracleConnection con=null;
            DataSet ds=new DataSet();
            try
            {                               
                con = connect.Connect();
                OracleCommand cmd = new OracleCommand(strSQL, con);
                cmd.CommandType = CommandType.Text;
                da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Dispose();
            }
            catch //(Exception ex)
            {
                if (con==null || con.State!=ConnectionState.Closed)
                {
                    con.Close();
                }
                return null;
            }
            return ds.Tables[0];
        }

        // ham thuc thi SQL
        public int ExcuteNonQuery(string strSQL)
        {
            OracleConnection conn = null;
            OracleCommand cmd = null;
            OracleTransaction trans=null;
            try
            {
                conn = connect.Connect();
                trans = conn.BeginTransaction();
                cmd = new OracleCommand(strSQL,conn,trans);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                trans.Commit();
                // Close Connection
                conn.Dispose();
                cmd.Dispose();
                trans.Dispose();
                return 1;
               
            }
            catch(Exception ex)
            {
                trans.Rollback();// RollBack
                if (conn != null)   { conn.Dispose(); }
                if (cmd != null)    { cmd.Dispose();  }
                if (trans != null)  { trans.Dispose();}
                //return -1;
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);                
            }
            
        }
        public int ExcuteNonQuery(string strSQL,OracleParameter[] param)
        {
            OracleConnection conn = null;
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                conn = connect.Connect();
                trans = conn.BeginTransaction();
                cmd = new OracleCommand(strSQL, conn, trans);
                cmd.CommandType = CommandType.Text;
                if (param != null) cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
                trans.Commit();
                // Close Connection
                conn.Dispose();
                cmd.Dispose();
                trans.Dispose();
                return 1;
               
            }
            catch (Exception ex)
            {
                trans.Rollback();// RollBack
                if (conn != null) { conn.Dispose(); }
                if (cmd != null) { cmd.Dispose(); }
                if (trans != null) { trans.Dispose(); }
                //return -1;
                throw new Exception("OracleTransaction xay ra loi, khong the chay ExecuteNonQuery" + ex.Message);
            }
          
        }
        // ham thuc thi Store
        public int ExcuteStore(string StoreName,OracleParameter[] param)
        {
            OracleConnection conn = null;
            OracleCommand cmd = null;
            OracleTransaction trans = null;
            try
            {
                conn = connect.Connect();
                trans = conn.BeginTransaction();
                cmd = new OracleCommand(StoreName, conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
                trans.Commit();
                // Close Connection
                conn.Dispose();
                cmd.Dispose();
                trans.Dispose();
                return 1;
            }
            catch //(Exception ex)
            {
                trans.Rollback();// RollBack
                if (conn != null) { conn.Dispose(); }
                if (cmd != null) { cmd.Dispose(); }
                if (trans != null) { trans.Dispose(); }
                return -1;

            }
            





        }
    }
}
