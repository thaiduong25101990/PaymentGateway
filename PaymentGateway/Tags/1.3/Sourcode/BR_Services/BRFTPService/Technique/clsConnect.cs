using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace BRFTPService.Technique
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
        //********************************
        //Lay ve chuoi connection 
        public String ConnectionString()
        {            
            String strConnect = "";
            strDatabase = Config.getKeyConfig("DatabaseName");
            strUser = Config.getKeyConfig("Username");
            strPass = Encrypt.Decrypt(Config.getKeyConfig("Password"));            
            
            strConnect = "Data Source=" + strDatabase + ";User Id=" + strUser + " ;Password=" + strPass + " ;Integrated Security=no;";
            return strConnect;
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
            catch //(Exception ex)
            {
                //MessageBox.Show("Connect failed " + ex.Message, "Gateway");
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
