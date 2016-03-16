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
    public class SWIFT_BRANCH_ACTIONDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();        

        public SWIFT_BRANCH_ACTIONDP()
        {
        }
        public static SWIFT_BRANCH_ACTIONDP Instance()
        {
            return new SWIFT_BRANCH_ACTIONDP();
        }

        public int AddSWIFT_BRANCH_ACTION(SWIFT_BRANCH_ACTION_Info objTable)
        {
            string strSql = "Insert into SWIFT_BRANCH_ACTION(KEY_WORD,BRANCH,MESSAGE,PRIORITY,NAME, DESCRIPTION, DEPARTMENT) values ";
            strSql = strSql + "(:pKEY_WORD,:pBRANCH,:pMESSAGE,:pPRIORITY,:pNAME,:pDESCRIPTION,:pDEPARTMENT)";
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,200),
                                           new OracleParameter("pBRANCH",OracleType.NVarChar,10),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,4000),
                                           new OracleParameter("pPRIORITY",OracleType.NVarChar,3),
                                           new OracleParameter("pNAME",OracleType.NVarChar,20),
                                           new OracleParameter("pDESCRIPTION",OracleType.NVarChar,4000),
                                           new OracleParameter("pDEPARTMENT",OracleType.NVarChar,20)};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.BRANCH;
            oraParam[2].Value = objTable.MESSAGE;
            oraParam[3].Value = objTable.PRIORITY;
            oraParam[4].Value = objTable.NAME;
            oraParam[5].Value = objTable.DESCPRIPTION;
            oraParam[6].Value = objTable.DEPARTMENT;
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

        public int UpdateSWIFT_BRANCH_ACTION(SWIFT_BRANCH_ACTION_Info objTable)
        {
            string strSql = "UPDATE SWIFT_BRANCH_ACTION SET KEY_WORD = :pKEY_WORD,BRANCH = :pBRANCH,MESSAGE = :pMESSAGE,PRIORITY = :pPRIORITY, NAME = :pNAME , DESCRIPTION = :pDESCRIPTION , DEPARTMENT = :pDEPARTMENT ";
            strSql = strSql + "WHERE PRM_ID = " + objTable.PRM_ID;
            OracleParameter[] oraParam ={  new OracleParameter("pKEY_WORD",OracleType.NVarChar,200),
                                           new OracleParameter("pBRANCH",OracleType.NVarChar,10),
                                           new OracleParameter("pMESSAGE",OracleType.NVarChar,4000),
                                           new OracleParameter("pPRIORITY",OracleType.NVarChar,3),
                                           new OracleParameter("pNAME",OracleType.NVarChar,20),
                                           new OracleParameter("pDESCRIPTION",OracleType.NVarChar,4000),
                                           new OracleParameter("pDEPARTMENT",OracleType.NVarChar,20)};

            oraParam[0].Value = objTable.KEY_WORD;
            oraParam[1].Value = objTable.BRANCH;
            oraParam[2].Value = objTable.MESSAGE;
            oraParam[3].Value = objTable.PRIORITY;
            oraParam[4].Value = objTable.NAME;
            oraParam[5].Value = objTable.DESCPRIPTION;
            oraParam[6].Value = objTable.DEPARTMENT;
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

        public int DeleteSWIFT_BRANCH_ACTION(int intID)
        {
            string strSql = "Delete from SWIFT_BRANCH_ACTION where PRM_ID=:pID";
            OracleParameter[] oraParm = { new OracleParameter("pID", OracleType.Int32, 5) };
            oraParm[0].Value = intID;
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


        public DataSet GetSWIFT_BRANCH_ACTION()
        {
            DataSet datDs = new DataSet();
            string strSql = "Select a.PRM_ID, a.NAME, LTRIM(a.BRANCH,'0') BRANCH,a.DEPARTMENT, a.PRIORITY, a.KEY_WORD, a. MESSAGE, a.DESCRIPTION  FROM SWIFT_BRANCH_ACTION a";
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

        public int ValidateSWIFT_BRANCH_ACTION(string strSql)
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

        public bool IDIsExisting(string strCriteriaName, string strPriority, string strBranch)
        {
            string strSql = "Select * from SWIFT_BRANCH_ACTION where NAME = '" + strCriteriaName + 
                "' and PRIORITY = '" + strPriority + "' and ";
            strSql = strSql + "BRANCH = '" + strBranch + "'";
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


        ///////////////////////////////////////////////////////////-/
        //Muc dich: Ham kiem tra Keyword co trong Message?
        //          Kiem tra xau Message co dung chuan SQL        
        //Ngay tao: 01/08/2008
        //Tham so:  strKeyword: xau Keyword
        //          strMessage: xau Messafe
        //Dau ra:   True: thanh cong, false: ko thanh cong
        /////////////////////////////////////////////////////////////
        public bool CheckKeyword(string strKeyword, string strMessage,
            out string strError, out int iCheck)
        {
            string strSql = "";
            string strSelect = "";
            string strDrop = "";
            DataSet ds = new DataSet();
            char[] splitter = { ';' };
            string[] arrKeyword;
            int iID=0;

            strError = "";
            iCheck = 0;
            try
            {
                //Tao cac phan tu mang
                strKeyword = strKeyword.ToUpper();
                arrKeyword = strKeyword.Split(splitter);
                strMessage = strMessage.ToUpper();
                
                for (int i = 0; i <= arrKeyword.Length - 1; i++)
                {
                    if (strMessage.IndexOf(arrKeyword[i].ToString().Trim()) < 0)
                    {
                        strError = "Keyword not in Message";
                        return false;
                    }
                }                                
                //Tao bang temp
                strSql = strSql + " CREATE GLOBAL TEMPORARY TABLE SWIFT_BRANCH_TEMP (";
                for (int i = 0; i <= arrKeyword.Length - 1; i++)
                {
                    if (i < arrKeyword.Length - 1)
                    {
                        strSql = strSql + arrKeyword[i].ToString().Trim() + " VARCHAR2(2000),";
                    }
                    else
                    {
                        strSql = strSql + arrKeyword[i].ToString().Trim() + " VARCHAR2(2000)";
                    }
                }                
                strSql = strSql + ") ON COMMIT PRESERVE ROWS ";                
                //Select bang temp voi menh de where = criteria message           
                if (strMessage.Trim().Substring(0, 6).ToUpper() == "SELECT")
                {
                    strSelect = strMessage;
                }                
                else
                {
                    strSelect = " SELECT * FROM SWIFT_BRANCH_TEMP WHERE " + strMessage;
                }
                strDrop = " DROP TABLE SWIFT_BRANCH_TEMP ";
                //iID = RunSPCheckMessage(strSql, strSelect, strDrop, out bCheck);
                iID= RunSPCheckMessage(strSql, strSelect, strDrop, out iCheck);
                if (iID < 0)
                {
                    strError = "Criteria message is invalid";
                    return false;
                }
                return true;
            }
            catch 
            {                
                return false;
            } 
        }


        ///////////////////////////////////////////////////////////-/
        //Muc dich: Ham run SP check xau sql        
        //Ngay tao: 01/08/2008
        //Tham so:  strCreate: xau tao bang temp
        //          strSelect: xau select
        //          strDrop: xau drop bang temp
        //          iCheck: gia tri tra ra
        //Dau ra:   True: thanh cong, false: ko thanh cong
        /////////////////////////////////////////////////////////////
        public int RunSPCheckMessage(string strCreate, string strSelect, 
            string strDrop, out int iCheck)
		{
            int iResult = 0;
            iCheck = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pCreateTable",OracleType.VarChar,3500),
                                                new OracleParameter("pSelectTable",OracleType.VarChar,3500) ,
                                                new OracleParameter("pDropTable",OracleType.VarChar,3500)                                            
                                                ,new  OracleParameter("pOut",OracleType.Int16,2)
                                            };
                oraParas[0].Value = strCreate;
                oraParas[1].Value = strSelect;
                oraParas[2].Value = strDrop;
                oraParas[3].Value = 0;
                oraParas[3].Direction = ParameterDirection.Output;
                iCheck = 0;
                try
                {
                    oraConn = conn.Connect();
                    if (oraConn == null)
                    {
                        iCheck = 0;
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_SWIFT_CHECK_PARA.CHECK_SWIFT_PARA", oraParas);
                        iCheck = Convert.ToInt16(oraParas[3].Value);
                    }
                }
                catch 
                {
                    iCheck = 0;
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();
                }
                return iResult;               
            }
            catch (Exception ex)
            {
                throw (ex);
            }            
        }
       
    }
}
