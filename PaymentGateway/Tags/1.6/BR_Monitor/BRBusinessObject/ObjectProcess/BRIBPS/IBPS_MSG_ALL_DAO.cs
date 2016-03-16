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
//' =============================================
//' Author:	Nguyen duc quy
//' Create date:	18/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 27/05/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class IBPS_MSG_ALLDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public IBPS_MSG_ALLDP()
        {
        }
        public static IBPS_MSG_ALLDP Instance()
        {
            return new IBPS_MSG_ALLDP();
        }
       
        public int BackUp(string pTABLENAME1, string pTABLENAME2, string pMSGStatus)
        {
            try
            {
                string strFields;
                strFields = GetFields(pTABLENAME1);

                string strSQL = "insert into " + pTABLENAME2 + "(" + strFields + ")";
                strSQL = strSQL + " (select " + strFields + " from " + pTABLENAME1 + " IMC where IMC.status='1' and IMC.QUERY_ID not in (select IMA.QUERY_ID from " + pTABLENAME2 + " IMA) ";

                strFields = strFields.ToUpper();

                if ((pMSGStatus != "") && (strFields.IndexOf("STATUS") != -1))
                    strSQL = strSQL + " and status =" + pMSGStatus + ")";
                else
                    strSQL = strSQL + ")";
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }

                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;

            }
            finally
            {
                oraConn.Dispose();
            }

        }
        public int BackUpAll(string pTABLENAME1, string pTABLENAME2, string pMSGStatus)//Backup1
        {
            try
            {
                string strFields;
                strFields = GetFields(pTABLENAME1);

                string strSQL = "Insert into " + pTABLENAME2 + "(" + strFields + ")";
                strSQL = strSQL + " (Select " + strFields + " from " + pTABLENAME1;

                if ((pMSGStatus != "") && (strFields.IndexOf("STATUS") != -1))
                    strSQL = strSQL + " where status = " + pMSGStatus + ")";
                else
                    strSQL = strSQL + ")";

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                //  strSQL = "insert into " + pTABLENAME2 + " (select * from " + pTABLENAME1 + " Where STATUS= 1)";
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }

        }
       
        public int Delete(string pTABLENAME, string pWhere)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                DataTable datTable = new DataTable();
                string strSQL = "delete from " + pTABLENAME + " " + pWhere;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
           
        }

       
        public int Forward_LV_HV(int pMSG_ID, int pHL_Val, string strTellerID)
        {
            
            OracleParameter[] oraParam = { new OracleParameter("pMSG_ID", OracleType.Number, 10),
                                           new OracleParameter("pHL_Val", OracleType.Number, 10),                                           
                                          new OracleParameter("pTellerID", OracleType.VarChar, 20)};
            oraParam[0].Value = pMSG_ID;
            oraParam[1].Value = pHL_Val;
            oraParam[2].Value = strTellerID;


            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_FORWARD.Forward_Failmsg", oraParam);

            }
            catch 
            {
                return -1; ;
            }
        }

        private string GetFields(string strTableName)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return "";
                }

                string strFields = "";
                string strSQL = "select COLUMN_NAME from user_tab_columns where table_name='" + strTableName.Trim() + "'";
                DataSet dsData = new DataSet();

                dsData = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    strFields = strFields + dsData.Tables[0].Rows[i]["COLUMN_NAME"] + ", ";
                }

                char[] strTmp = { ' ', ',' };
                strFields = strFields.TrimEnd(strTmp);

                return strFields;
            }
            catch
            {
                return "";
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        //Kiem tra bang co du lieu khong
        public int CheckExist(string pTABLENAME)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                DataSet dsData = new DataSet();
                string strSQL = " select count(QUERY_ID) RowCount from " + pTABLENAME;

                dsData = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                int iResult = -1;

                iResult = Convert.ToInt32(dsData.Tables[0].Rows[0]["RowCount"]);
                return iResult;
            }
            catch 
            {
                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
    }
}
