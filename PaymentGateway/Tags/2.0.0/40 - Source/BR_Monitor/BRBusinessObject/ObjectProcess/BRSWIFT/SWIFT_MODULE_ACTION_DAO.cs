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
using System.Data.OracleClient;
//using BR.BRLib;
using BR.DataAccess;


//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_MODULE_ACTIONDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public SWIFT_MODULE_ACTIONDP()
        {
        }
        public static SWIFT_MODULE_ACTIONDP Instance()
        {
            return new SWIFT_MODULE_ACTIONDP();
        }

        public int AddSWIFT_MODULE_ACTION(SWIFT_MODULE_ACTION_Info objTable)
        {
            string strSql = "Insert into SWIFT_MODULE_ACTION(KEY_WORD,DEPARTMENT,MESSAGE,PRIORITY,NAME, DESCRIPTION) values ";
            strSql = strSql + "(:pKEY_WORD,:pDEPARTMENT,:pMESSAGE,:pPRIORITY,:pNAME,:pDESCRIPTION)";
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,20),
                                           new OracleParameter("pDEPARTMENT",OracleType.NVarChar,10),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,255),
                                           new OracleParameter("pPRIORITY",OracleType.NVarChar,3),
                                           new OracleParameter("pNAME",OracleType.NVarChar,20),
                                           new OracleParameter("pDESCRIPTION",OracleType.NVarChar,255),};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.DEPARTMENT;
            oraParam[2].Value = objTable.MESSAGE;
            oraParam[3].Value = objTable.PRIORITY;
            oraParam[4].Value = objTable.NAME;
            oraParam[5].Value = objTable.DESCPRITION;
            try
            {

                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1;
            }
        }

        public int UpdateSWIFT_MODULE_ACTION(SWIFT_MODULE_ACTION_Info objTable)
        {
            string strSql = "UPDATE SWIFT_MODULE_ACTION SET KEY_WORD = :pKEY_WORD,DEPARTMENT = :pDEPARTMENT,MESSAGE = :pMESSAGE,PRIORITY = :pPRIORITY, NAME = :pNAME ";
            strSql = strSql + "WHERE PRM_ID = " + objTable.PRM_ID;
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,20),
                                           new OracleParameter("pDEPARTMENT",OracleType.NVarChar,10),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,255),
                                           new OracleParameter("pPRIORITY",OracleType.NVarChar,3),
                                           new OracleParameter("pNAME",OracleType.NVarChar,20)};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.DEPARTMENT;
            oraParam[2].Value = objTable.MESSAGE;
            oraParam[3].Value = objTable.PRIORITY;
            oraParam[4].Value = objTable.NAME;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1;
            }
        }

        public int DeleteSWIFT_MODULE_ACTION(int intKey)
        {
            string strSql = "Delete from SWIFT_MODULE_ACTION where PRM_ID=:pKey";
            OracleParameter[] oraParm = { new OracleParameter("pKey", OracleType.Char, 20) };
            oraParm[0].Value = intKey;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParm);
            }
            catch 
            {
                return -1;
            }
        }


        public DataSet GetSWIFT_MODULE_ACTION()
        {
            DataSet datDs = new DataSet();
            string strSql = "Select a.PRM_ID, a.NAME, a.DEPARTMENT, a.PRIORITY, a.KEY_WORD, a.MESSAGE, a.DESCRIPTION FROM SWIFT_MODULE_ACTION a";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                    return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return null;
            }
        }

        public int ValidateSWIFT_MODULE_ACTION(string strSql)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                else
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return -1;
            }
        }

        public bool IDIsExisting(string strCriteriaName, string strPriority, string strModul)
        {
            string strSql = "Select * from SWIFT_MODULE_ACTION where NAME = '" + strCriteriaName + "' and PRIORITY = '" + strPriority + "' and ";
            strSql = strSql + "DEPARTMENT = '" + strModul + "'";
            // string strSql = "select * from Currency";
            DataSet ds = new DataSet();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return true;
                ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int UpdateSWIFTMODULEACTION(SWIFT_MODULE_ACTION_Info objTable)
        {
            string strSql = "UPDATE SWIFT_MODULE_ACTION SET KEY_WORD = :pKEY_WORD,DEPARTMENT = :pDEPARTMENT,MESSAGE = :pMESSAGE,PRIORITY = :pPRIORITY, NAME = :pNAME, DESCRIPTION = :pDESCRIPTION ";
            strSql = strSql + "WHERE PRM_ID = " + objTable.PRM_ID;
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,20),
                                           new OracleParameter("pDEPARTMENT",OracleType.NVarChar,10),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,255),
                                           new OracleParameter("pPRIORITY",OracleType.NVarChar,3),
                                           new OracleParameter("pNAME",OracleType.NVarChar,20),
                                           new OracleParameter("pDESCRIPTION",OracleType.NVarChar,512)};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.DEPARTMENT;
            oraParam[2].Value = objTable.MESSAGE;
            oraParam[3].Value = objTable.PRIORITY;
            oraParam[4].Value = objTable.NAME;
            oraParam[5].Value = objTable.DESCPRITION;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1;
            }
        }
    }

}
