using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Transactions;
using BR.BRLib;

namespace BR.DataAccess
{

    public class DataAcess : OracleHelper
    {
        public bool loginOK;
        private  OracleConnection m_orcConn = new OracleConnection();//Khoi connection
        private  Connect_Process connect = new Connect_Process();


        // *****************************************************
        // Muc dich:   Huy bo 1 ket noi
        // Dau vao:
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //*****************************************************

        public void Dispose()
        {
            if (m_orcConn.State == ConnectionState.Open)
            {
                m_orcConn.Close();
            }
            m_orcConn.Dispose(); 
        }
        // *****************************************************
        // Muc dich:   tra ve mot doi tuong Dataset
        // Dau vao:
        // strSql: Ten thu tuc duoc tao trong Database Oracle
        // strTable: Ten bang
        // Tra ve:   Mot Dataset da duoc luu gia tri
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //*****************************************************

        // return Oracle Adapter
        public DataSet ReturnDataSet(string strSql, string strTable)
        {
            DataSet datDS = new DataSet();

            OracleDataAdapter orclAdapter = new OracleDataAdapter();
         try
            {
                m_orcConn = connect.Connect();
                orclAdapter = new OracleDataAdapter(strSql,m_orcConn );
                orclAdapter.Fill(datDS, strTable);
                m_orcConn.Close();
                m_orcConn.Dispose();
            }
            catch
            {
                m_orcConn.Close();
                m_orcConn.Dispose();
                return null;
            }
            return datDS;
        }

        // *****************************************************
        // Muc dich:   tra ve mot doi tuong DataAdapter
        // Dau vao:
        // strSql: Ten thu tuc duoc tao trong Database Oracle
        // para: Mang tham so truy vao
        // Tra ve:   Mot DataAdarter da duoc luu gia tri
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //*****************************************************

        // return Oracle Adapter
        public OracleDataAdapter Return_Adapter(string strSql,params OracleParameter[] para)
        {

            OracleDataAdapter orclAdapter = new OracleDataAdapter();
            OracleCommand cmd;
           
            try
            {   m_orcConn = connect.Connect();
                cmd  = new OracleCommand(strSql,m_orcConn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                foreach (OracleParameter one_para in para)
                {
                    cmd.Parameters.Add(one_para);
                }
                orclAdapter = new OracleDataAdapter(cmd);
                m_orcConn.Close();
                m_orcConn.Dispose();
            }
            catch 
            {
                m_orcConn.Close();
                m_orcConn.Dispose();
                return null;
            }
            return orclAdapter;
        }

        // *****************************************************
        // Muc dich:   tra ve mot doi tuong DataAdapter
        // Dau vao:
        // strSql: Ten thu tuc duoc tao trong Database Oracle
        // para: Mang tham so truy vao
        // Tra ve:   Mot DataAdarter da duoc luu gia tri
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //*****************************************************

        // return Oracle Adapter
        public OracleDataAdapter Adapter_Command(string StoreNameSelect, 
                                                string StoreNameIn, 
                                                string StoreNameUP, 
                                                string StoreNameDel,  
                                                OracleParameter[] parasel,  
                                                OracleParameter[] paraIN, 
                                                OracleParameter[] paraUP, 
                                                OracleParameter[] paraDel)
        {

            OracleDataAdapter orclAdapter = new OracleDataAdapter();
            try
            {
                m_orcConn = connect.Connect();

                orclAdapter.SelectCommand = new OracleCommand(StoreNameSelect, m_orcConn );
                
                    if (parasel != null)
                    {
                        orclAdapter.SelectCommand.CommandType = CommandType.StoredProcedure; 
                        foreach (OracleParameter one_para in parasel)
                        {
                            orclAdapter.SelectCommand.Parameters.Add(one_para);
                        }
                    }

                    orclAdapter.InsertCommand = new OracleCommand(StoreNameIn, m_orcConn);
                    foreach (OracleParameter one_para in paraIN)
                    {
                        orclAdapter.InsertCommand.Parameters.Add(one_para);
                    }
                    orclAdapter.InsertCommand.CommandType = CommandType.StoredProcedure; 

                    orclAdapter.UpdateCommand = new OracleCommand(StoreNameUP, m_orcConn);
                    foreach (OracleParameter one_para in paraUP)
                    {
                        orclAdapter.UpdateCommand.Parameters.Add(one_para);
                    }
                    orclAdapter.UpdateCommand.CommandType = CommandType.StoredProcedure; 

                    orclAdapter.DeleteCommand = new OracleCommand(StoreNameDel, m_orcConn);
                    foreach (OracleParameter one_para in paraDel)
                    {
                        orclAdapter.DeleteCommand.Parameters.Add(one_para);
                    }
                    orclAdapter.DeleteCommand.CommandType = CommandType.StoredProcedure; 
            }
                catch (Exception ex)
                      
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return null;
            }
            return orclAdapter;
        

        }

        // *****************************************************
        // Muc dich:   tra ve mot doi tuong DataAdapter
        // Dau vao:
        // strSql: Ten thu tuc duoc tao trong Database Oracle
        // Tra ve:   Mot DataAdarter da duoc luu gia tri
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //*****************************************************
        public OracleDataAdapter Return_Adapter(string strSql)
        {
            OracleDataAdapter orlda = new OracleDataAdapter();
            try
            {
                m_orcConn = connect.Connect();
                orlda = new OracleDataAdapter(strSql, m_orcConn);
            }
            catch 
            {
                return null;
            }
            return orlda;
        }

        // *****************************************************
        // Muc dich:   Thuc hien mot thu tuc
        // Dau vao:
        // strSql: Ten thu tuc duoc tao trong Database Oracle
        // para: Mang tham so truy vao
        // Tra ve:  True: neu thuc hien duoc
        //          False: neu khong thuc hien duoc
        // Ngay tao: 12-4-2008
        // Nguoi tao:    QuanLD 
        //*****************************************************

        public bool Excute_Oracle(string strSQL,params OracleParameter[] paras)
        {            
            OracleCommand cmd;
            try
            {
                m_orcConn = connect.Connect();
                cmd = new OracleCommand(strSQL, m_orcConn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
                cmd.ExecuteNonQuery();
                m_orcConn.Close();
                m_orcConn.Dispose(); 
                return true;
            }
            catch
            {
                m_orcConn.Close();
                m_orcConn.Dispose();
                return false;
            }

        }

        public int Excute_NotClose(string strSQL, CommandType cmdType ,params OracleParameter[] paras)
        {
            OracleConnection oraConnection = new OracleConnection();
            oraConnection = connect.Connect();
            OracleTransaction oracleTransaction;
            oracleTransaction = oraConnection.BeginTransaction();
            OracleCommand cmd;
            try
            {

                if (m_orcConn.State != ConnectionState.Open)
                {
                    m_orcConn = connect.Connect();
                }

                cmd = new OracleCommand(strSQL, m_orcConn);
                cmd.CommandType = cmdType;
                if (paras != null)
               
                {
                    foreach (OracleParameter para in paras)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
                
                cmd.ExecuteNonQuery();
                oracleTransaction.Commit();
               return 1;
            }
            catch
            {                
                return -1;
            }

        }







        // *****************************************************
        // Muc dich         :   Thuc hien excute motham trong DB
        // Dau vao          :
        //      strFndName      : Ten ham truyen vao
        //      iUpdate         : Bien dieu khien 1 insert, 2 Update, 3 detele
        //      strTable        : Ten bang
        //      para            : Mang tham so truy vao
        //      datDs           : Dataset truyen vao
        // Tra ve:  True    : neu thuc hien duoc
        //          False   : neu khong thuc hien duoc
        // Ngay tao         : 14-4-2008
        // Nguoi tao        : QuanLD 
        //*****************************************************

        public bool ExcuteStore( int iUpdate ,string strFndName,string strTable, DataSet datDs, params OracleParameter[] paras)
        {
            
            try
            {
                OracleDataAdapter oraDa = new OracleDataAdapter();
                m_orcConn = connect.Connect();
                if (m_orcConn == null )
                {
                    MessageBox.Show("Connect failed ", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false ;
                }

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = m_orcConn;
                cmd.CommandText = strFndName;
                cmd.CommandType = CommandType.StoredProcedure;
                oraDa.UpdateCommand = cmd;
                foreach (OracleParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
                m_orcConn.Close();
                m_orcConn.Dispose();
                return true;
            }
                
            catch//(Exception ex)
            {
                m_orcConn.Close();
                m_orcConn.Dispose();
                return false;
            }
           
        }


        #region "Ham thuc hien lay cau truy van"
        public void ExecuteNonQuery(string strSQL)
        {
            OracleConnection oraConnection = new OracleConnection();
            oraConnection = connect.Connect();
            OracleCommand oraCommand = new OracleCommand();
            OracleTransaction oraTransaction;
            oraTransaction = oraConnection.BeginTransaction();
            oraCommand = new OracleCommand(strSQL, oraConnection, oraTransaction);
            oraCommand.CommandType = CommandType.Text;
            try
            {
                oraCommand.ExecuteNonQuery();
                oraTransaction.Commit();
                oraConnection.Close();
                oraConnection.Dispose();
            }
            catch (Exception ex)
            {
                oraTransaction.Rollback();
                throw new Exception("Error OracleTransaction: " + ex.Message);
            }

        }
        #endregion

    

    }
}
