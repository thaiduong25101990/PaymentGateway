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
    public class MRECEIVE_BRANCHDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();        
        
        public MRECEIVE_BRANCHDP()
        {
        }
        public static MRECEIVE_BRANCHDP Instance()
        {
            return new MRECEIVE_BRANCHDP();
        }

        public int UPDATE_MRECIVER(string pTRAN_NO)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }            
            string strSQL = "Update MRECEIVE_BRANCH MB set MB.TRAN_DATE = To_date(sysdate,'DD/MM/YY') where Trim(MB.TRAN_NO)='" + pTRAN_NO + "'";          
            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return 1;
            }
        }


    }
}
