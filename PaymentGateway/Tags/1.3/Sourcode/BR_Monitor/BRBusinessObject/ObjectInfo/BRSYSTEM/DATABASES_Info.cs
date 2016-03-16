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


namespace BR.BRBusinessObject
{
    public class DATABASESInfo
    {

        //-----------------------------
        public static string strDatabase = Connect_Process.strDatabase;
        public static string strOracleServer = Connect_Process.strOrcServer;
        public static string strUserDB = Connect_Process.strUser;
        public static string strPassword = Connect_Process.strPass;
        

    }
}
