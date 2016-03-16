using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using BR.BRLib;




namespace BR.DataAccess
{
    //********************************************
    // Lop xu ly viec connect vao database
    // Ham GetConfig_Info lay cac thong tin ket noi duoc config san trong
    // Ham ConnectionString lay ra chuoi ket noi 
    // Ham Connect() thuc hien connect vao co so du lieu
    //*******************************************
    public class Connect_Process
    {
        public static string strDatabase;
        public static string strOrcServer;
        public static string strPass;
        public static string strUser;
        //-----
        public Connect_Info Conn_Info = new Connect_Info();
        UserEncrypt objEncrypt = new UserEncrypt();
        //********************************
        //Lay ve chuoi connection 
        public String ConnectionString()
        {
            GetConfig_Info();
            String strConnect = "";
            strConnect = "Data Source=" + Conn_Info.DatabaseName + ";User Id=" + Conn_Info.User + " ;Password=" + Conn_Info.Password + " ;Integrated Security=no;";
            //--------------------------------------
            strDatabase = Conn_Info.DatabaseName;
            strOrcServer = Conn_Info.strOrcServer;
            strUser = Conn_Info.User;
            strPass = Conn_Info.Password;
            //--------------------------------------
            return strConnect;
        }

        // Lay cac gia tri tu file config
        Connect_Info GetConfig_Info()
        {            
            
            Conn_Info.strOrcServer = ConfigSettings.ReadSetting("ServerName");
            Conn_Info.DatabaseName = ConfigSettings.ReadSetting("DBName");
            Conn_Info.User = ConfigSettings.ReadSetting("Username");
            Common.Schema = ConfigSettings.ReadSetting("Username");
            Conn_Info.Password = objEncrypt.Decrypt(ConfigSettings.ReadSetting("Password"), objEncrypt.sKeyDB);//ConfigSettings.ReadSetting("Password");//;
            return Conn_Info;
        }
        //Lay ra 1 connection 
        public OracleConnection Connect()
        {
            OracleConnection Conn = new OracleConnection();
            String strConnect_string = ConnectionString();
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();

                }
                Conn.ConnectionString = strConnect_string;
                Conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect failed " + ex.Message, Common.sCaption);
                return null;
            }
            return Conn;
        }
    }


    public class Connect_Info
    {
        private String m_strDatabase;
        private String m_strOrcServer;
        private String m_strPass;
        private String m_strUser;


        // Lay ten server

        public string strOrcServer
        {
            get
            {
                return this.m_strOrcServer;
            }
            set
            {
                this.m_strOrcServer = value;
            }
        }
        //Lay ten database
        public string DatabaseName
        {
            get
            {
                return this.m_strDatabase;
            }
            set
            {
                this.m_strDatabase = value;
            }
        }
        //Lay username
        public string User
        {
            get
            {
                return this.m_strUser;
            }
            set
            {
                this.m_strUser = value;
            }
        }
        //Lay Password

        public string Password
        {
            get
            {
                return this.m_strPass;
            }
            set
            {
                this.m_strPass = value;
            }
        }
    }

}
