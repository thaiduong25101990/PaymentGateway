using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using BR.BRLib;
using System.Data;
using System.Data.Odbc;


namespace BR.DataAccess
{
    public class ConnectHost
    {
        public static string strHostDSN;
        public static string strPass;
        public static string strUser;
        //-----
        public ConnectInfo ConnInfo = new ConnectInfo();
        UserEncrypt objEncrypt = new UserEncrypt();
        //********************************
        //Lay ve chuoi connection 
        public String ConnectionString()
        {
            GetConfigInfo();
            String strConnect = "";            
            strHostDSN = ConnInfo.strHostDSN;
            strUser = ConnInfo.User;
            strPass = ConnInfo.Password;
            /*Choui connect vao he thong HOST*/
            strConnect = @"DSN=" + strHostDSN + ";UID=" + strUser + ";PWD=" + strPass + "";
            return strConnect;
        }
        
        ConnectInfo GetConfigInfo()
        {

            ConnInfo.strHostDSN = ConfigSettings.ReadSetting("IBM.DSN");
            ConnInfo.User = ConfigSettings.ReadSetting("IBM.User");
            ConnInfo.Password = objEncrypt.Decrypt(ConfigSettings.ReadSetting("IBM.Pass"), objEncrypt.sKeyDB);
            return ConnInfo;
        }


        //Lay ra 1 connection 
        public OdbcConnection OdbcConnect()
        {
            string strConn = ConnectionString();
            OdbcConnection Conn = new OdbcConnection(strConn);            
            try
            {
                if (Conn.State ==  ConnectionState.Open)
                {
                    Conn.Close();
                }                
                Conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect host failed " + ex.Message, Common.sCaption);
                return null;
            }
            return Conn;
        }
    }

    public class ConnectInfo
    {       
        private String m_strHostDNS;
        private String m_strPass;
        private String m_strUser;
        public string strHostDSN
        {
            get
            { return this.m_strHostDNS; }
            set
            { this.m_strHostDNS = value; }
        }
       
        public string User
        {
            get
            { return this.m_strUser; }
            set
            { this.m_strUser = value; }
        }
        
        public string Password
        {
            get
            { return this.m_strPass; }
            set
            { this.m_strPass = value; }
        }
    }
}
