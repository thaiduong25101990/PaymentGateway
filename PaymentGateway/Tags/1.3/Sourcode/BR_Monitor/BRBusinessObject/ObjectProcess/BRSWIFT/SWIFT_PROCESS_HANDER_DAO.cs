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
    public class SWIFT_PROCESS_HANDERDP
    {
          OracleConnection oraConn = new OracleConnection();
         Connect_Process connect = new Connect_Process();

         public SWIFT_PROCESS_HANDERDP()
         {
         }
         public static SWIFT_PROCESS_HANDERDP Instance()
         {
             return new SWIFT_PROCESS_HANDERDP();
         }

         public int UPDATE_SWIFT_PROCESS_HANDER(SWIFT_PROCESS_HANDERInfo objTable)
         {
             string strSQL = "Update  SWIFT_PROCESS_HANDER  set NEW_PROCESSSTS = :pNEW_PROCESSSTS,";
             strSQL = strSQL + "  NEW_BRANCH=:pNEW_BRANCH,NEW_DEPARTMENT=:pNEW_DEPARTMENT where MSG_ID = :pMSG_ID";
             OracleParameter[] oraParam = {new OracleParameter("pMSG_ID", OracleType.Number,20),                                                
                                                new OracleParameter("pNEW_BRANCH", OracleType.NVarChar,12),
                                                new OracleParameter("pNEW_DEPARTMENT", OracleType.NVarChar,10),
                                                new OracleParameter("pNEW_PROCESSSTS", OracleType.Number,3)
                                          };
             oraParam[0].Value = objTable.MSG_ID;            
             oraParam[1].Value = objTable.NEW_BRANCH;
             oraParam[2].Value = objTable.NEW_DEPARTMENT;
             oraParam[3].Value = objTable.NEW_PROCESSSTS;            

             try
             {
                 oraConn = connect.Connect();
                 if (oraConn == null)
                     return -1;
                 DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
                 oraConn.Close();
                 oraConn.Dispose();
                 return 1;
             }
             catch //(Exception ex)
             {
                 return -1;
             }
         }
         public int DELETE_SWIFT_PROCESS_HANDER(int pMSG_ID)
         {
             string strSQL = "DELETE SWIFT_PROCESS_HANDER where MSG_ID = " + pMSG_ID + "";
             try
             {
                 oraConn = connect.Connect();
                 if (oraConn == null)
                     return -1;
                 DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                 oraConn.Close();
                 oraConn.Dispose();
                 return 1;
             }
             catch //(Exception ex)
             {
                 return -1;
             }
         }
         public int DELETE_SWIFT_PROCESS(string pMSG_ID,string pUserid)
         {
             string strSQL = "DELETE SWIFT_PROCESS_HANDER where MSG_ID = '" + pMSG_ID.Trim() + "' and TELLER_ID = '" + pUserid + "' ";
             try
             {
                 oraConn = connect.Connect();
                 if (oraConn == null)
                     return -1;
                 DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                 oraConn.Close();
                 oraConn.Dispose();
                 return 1;
             }
             catch //(Exception ex)
             {
                 return -1;
             }
         }

         public int DELETE_SWIFT_PROCESS_HANDER_MANUAL(string  pUserID)
         {             
             try
             {
                 oraConn = connect.Connect();
                 if (oraConn == null)
                 { return -1; }
                 OracleParameter[] orapara = { new OracleParameter("pUserID", OracleType.VarChar, 20) };
                 orapara[0].Value = pUserID;
                 DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_PROCESS_HANDICRAFT.DELETE_HANDER_MANUAL", orapara);
                 oraConn.Close();
                 oraConn.Dispose();
                 return 1;
             }
             catch //(Exception ex)
             {
                 return -1;
             }
         }

         public int DELETE_SWIFT_PROCESS_HANDERT(string pUserID,string pNew_process)
         {
             try
             {
                 oraConn = connect.Connect();
                 if (oraConn == null)
                 { return -1; }
                 string strSQL = "Delete from swift_process_hander where TELLER_ID = '" + pUserID + "' and NEW_PROCESSSTS = " + pNew_process + "";
                 OracleParameter[] orapara = { new OracleParameter("pUserID", OracleType.VarChar, 20) };
                 orapara[0].Value = pUserID;
                 DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                 oraConn.Close();
                 oraConn.Dispose();
                 return 1;
             }
             catch //(Exception ex)
             {
                 return -1;
             }
         }
    }
}
