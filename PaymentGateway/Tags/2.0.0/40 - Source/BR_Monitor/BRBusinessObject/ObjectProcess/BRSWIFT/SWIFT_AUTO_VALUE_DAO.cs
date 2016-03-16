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
    public class SWIFT_AUTO_VALUEDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        public SWIFT_AUTO_VALUEDP()
        {
        }
        public static SWIFT_AUTO_VALUEDP Instance()
        {
            return new SWIFT_AUTO_VALUEDP();
        }


        public bool CheckCode(string strKeycode, int iID)
        {
            string strSQL = "";
            DataTable datTbl = new DataTable();
            oraConn = conn.Connect();
            if (oraConn == null)
                return false;
            if (iID == 0)
            {
                strSQL = " Select KEY_WORD from SWIFT_AUTO_VALUE where KEY_WORD= '" + strKeycode + "'";
            }
            else
            {
                strSQL = " Select KEY_WORD from SWIFT_AUTO_VALUE where KEY_WORD = '" + strKeycode + "' And prm_id <> " + iID;
            }

            try
            {
                datTbl = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                if (datTbl.Rows.Count == 0)
                    return true;
                else
                    return false;

            }
            catch //(Exception ex)
            { return true; }
        }

        public int AddSWIFT_AUTO_VALUE(SWIFT_AUTO_VALUE_Info objTable)
        {
            string strSql = "Insert into SWIFT_AUTO_VALUE(KEY_WORD,MESSAGE) values ";
            strSql = strSql + "(:pKEY_WORD,:pMESSAGE)";
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,20),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,100)};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.MESSAGE;

            try
            {

                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }

        public int UpdateSWIFT_AUTO_VALUE(SWIFT_AUTO_VALUE_Info objTable)
        {
            string strSql = "UPDATE SWIFT_AUTO_VALUE SET MESSAGE = :pMESSAGE ";
            strSql = strSql + "WHERE KEY_WORD = :pKEY_WORD" ;
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,20),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,500)};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.MESSAGE;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch //(Exception ex )
            {
                return -1;
            }
         
        }

        public int DeleteSWIFT_AUTO_VALUE(int intPRM_ID)
        {
            string strSql = "Delete from SWIFT_AUTO_VALUE where PRM_ID=:pPRM_ID";
            OracleParameter[] oraParm = { new OracleParameter("pPRM_ID", OracleType.Int32, 5) };
            oraParm[0].Value = intPRM_ID;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParm);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }


        public DataSet GetSWIFT_AUTO_VALUE()
        {
            DataSet datDs = new DataSet();
            string strSql = "Select a.PRM_ID,a.KEY_WORD, a.MESSAGE FROM SWIFT_AUTO_VALUE a";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public string GetSWIFT_AUTO_VALUE(string pKeyword)
        {
            DataSet datDs = new DataSet();
            string strSql = "select  Key_word, Message from SWIFT_AUTO_VALUE where upper(trim(Key_word))=Upper(trim('" + pKeyword + "'))";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return "";

                datDs = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                if (datDs.Tables[0].Rows.Count > 0)
                {
                    return datDs.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                    return "";
            }
            catch //(Exception ex)
            {
                return "";
            }
        }

        public int ValidateSWIFT_AUTO_VALUE(string strSql)
        {
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                else
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch //(Exception ex)
            {
                return -1;
            }
        }
    }
}
