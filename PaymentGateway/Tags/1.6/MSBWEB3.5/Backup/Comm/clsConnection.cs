using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Web.Util;
using System.Windows;
using System.Windows.Forms;


namespace BIDVWEB.Comm
{
    public class clsConnection
    {
        #region Properties

        //Private Properties        
        private string m_strConnect;        
        private string m_strServer;
        private string m_strDatabase;
        private string m_strUserName;
        private string m_strPassword;
        private string m_strError;                      
        
        //public System.Data.OracleClient.OracleConnection oraConn;
        //public Oracle.DataAccess.Client.OracleConnection OracleConn;

        //public normal 
        public string strConnection
        {
            get { return this.m_strConnect; }
            set { this.m_strConnect = value; }
        }

        public string Error
        {
            get { return this.m_strError; }
            set { this.m_strError = value; }
        }

        public string ServerName
        {
            get { return this.m_strServer; }
            set { this.m_strServer = value; }
        }

        public string DatabaseName
        {
            get { return this.m_strDatabase; }
            set { this.m_strDatabase = value; }
        }

        public string UserName
        {
            get { return this.m_strUserName; }
            set { this.m_strUserName = value; }
        }

        public string Password
        {
            get { return this.m_strPassword; }
            set { this.m_strPassword = value; }
        }

        #endregion

        #region Methods
        //Public Methods
        
        //Constructor methods
        public clsConnection()        
        {
            //set null
            m_strConnect ="" ;
            m_strError = "" ;
            m_strServer = "";
            m_strDatabase = "" ;
            m_strUserName = "";
            m_strPassword = "";                        
        }


        //////////////////////////////////////////////////////////////
        //// Muc dich:    Mo connection
        //// Ngay tao:    05/2008
        //// Nguoi tao:   
        //// Dau vao:     
        //// Dau ra:      
        //////////////////////////////////////////////////////////////
        //public bool OpenConnection()
        //{
        //    bool boolResult = true;

        //    m_strConnect = ReadConfig("connectionStrings_Oracle");
            
        //    if (m_boolError)
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        if (oraConn.State ==System.Data.ConnectionState.Open)
        //        {
        //            oraConn.Close();
        //        }
        //        oraConn.ConnectionString = m_strConnect;
        //        oraConn.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        m_strError = ex.Message;
        //        boolResult = false;
        //    }
        //    return boolResult;
        //}

        //Lay ra 1 System.Data.OracleClient.OracleConnection 
        public System.Data.OracleClient.OracleConnection Connect()
        {
            System.Data.OracleClient.OracleConnection Conn =
                new System.Data.OracleClient.OracleConnection();
            m_strConnect = ReadConfig("connectionStrings_Oracle");
            try
            {
                if (Conn.State ==System.Data.ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.ConnectionString = m_strConnect;
                Conn.Open();
            }
            catch (Exception ex)
            {
                m_strError = ex.Message;
                return null;
            }
            return Conn;
        }
        

        //Lay ra 1 Oracle.DataAccess.Client.OracleConnection
        public Oracle.DataAccess.Client.OracleConnection ConnectOra()
        {
            Oracle.DataAccess.Client.OracleConnection Conn =
                new Oracle.DataAccess.Client.OracleConnection();
            m_strConnect = ReadConfig("connectionStrings_Oracle");
            m_strConnect = m_strConnect.Substring(0, m_strConnect.Length - 13);
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.ConnectionString = m_strConnect;
                Conn.Open();
            }
            catch (Exception ex)
            {
                m_strError = ex.Message;
                return null;
            }
            return Conn;
        }


        //////////////////////////////////////////////////////////////
        // Muc dich:    Dong connection
        // Ngay tao:    05/2008
        // Nguoi tao:   
        // Dau vao:     
        // Dau ra:      
        //////////////////////////////////////////////////////////////
        public bool CloseConnection(System.Data.OracleClient.OracleConnection oraConn)
        {
            bool boolResult = true;

            try
            {
                if (oraConn.State != System.Data.ConnectionState.Closed)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                m_strError = ex.Message;
                boolResult = false;
            }
            return boolResult;
        }


        //////////////////////////////////////////////////////////////
        // Muc dich:    Dong connection Oracle.DataAccess.Client.OracleConnection
        // Ngay tao:    05/2008
        // Nguoi tao:   
        // Dau vao:     
        // Dau ra:      
        //////////////////////////////////////////////////////////////
        //public bool CloseConnectionOra(Oracle.DataAccess.Client.OracleConnection oraConn)
        //{
        //    bool boolResult = true;

        //    try
        //    {
        //        if (oraConn.State != ConnectionState.Open)
        //        {
        //            oraConn.Close();
        //            oraConn.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        m_strError = ex.Message;
        //        boolResult = false;
        //    }
        //    return boolResult;
        //}


        ////////////////////////////////////////////////////////////
        // * Muc dich:    Doc xau connection tu file web.config
        // * Ngay tao:    05/2008
        // * Nguoi tao:   
        // * Dau vao:     strNodeName: Ten key luu xau connection tu file 
        // *              web.config
        // * Dau ra:      Xau connection
        ////////////////////////////////////////////////////////////         
        public string ReadConfig(string strNodeName)
        {
            string strResult = "";

            try
            {
                strResult = ConfigurationSettings.AppSettings[strNodeName];
                if (strResult == "")
                {                    
                    m_strError = "Chuỗi kết nối cơ sở dữ liệu rỗng";
                }
            }
            catch (Exception ex)
            {
                m_strError =  ex.Message;                
            }
            return strResult;
        }
        

        #endregion

    }
}
