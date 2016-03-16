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
namespace  BR.BRBusinessObject
{
	public class STRANGE_CHARACTERDP
	{
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
		public STRANGE_CHARACTERDP()
		{
		}
		public static STRANGE_CHARACTERDP Instance()
		{
			return new STRANGE_CHARACTERDP();
		}
		
		public int AddSTRANGE_CHARACTER(STRANGE_CHARACTERInfo objTable)
		{
           
                string strSql = "Insert into STRANGE_CHARACTER (STRANGE_CHAR,REPLACE_CHAR,MSG_TYPE,DEPARTMENT,FIELD_CODE,MSG_DIRECTION,GWTYPE,Line,Position) values(:pSTRANGE_CHAR,:pREPLACE_CHAR,:pMSG_TYPE,:pDEPARTMENT,:pFIELD_CODE,:pMSG_DIRECTION,:pGWTYPE,:pLine,:pPosition) ";
                OracleParameter[] oraParam ={new OracleParameter("pSTRANGE_CHAR",OracleType.VarChar,10),
                                           new OracleParameter("pREPLACE_CHAR",OracleType.VarChar,10),
                                           new OracleParameter("pMSG_TYPE",OracleType.VarChar,10),
                                           new OracleParameter("pDEPARTMENT",OracleType.VarChar,10),
                                           new OracleParameter("pFIELD_CODE",OracleType.VarChar,10),
                                           new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,10),
                                           new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                           new OracleParameter("pLine",OracleType.VarChar ,2),
                                           new OracleParameter("pPosition",OracleType.VarChar,2)};
                oraParam[0].Value = objTable.STRANGE_CHAR;
                oraParam[1].Value = objTable.REPLACE_CHAR;
                oraParam[2].Value = objTable.MSG_TYPE;
                oraParam[3].Value = objTable.DEPARTMENT;
                oraParam[4].Value = objTable.FIELD_CODE;
                oraParam[5].Value = objTable.MSG_DIRECTION;
                oraParam[6].Value = objTable.GWTYPE;
                oraParam[7].Value = objTable.LINE;
                oraParam[8].Value = objTable.POSITION;
                try
                {
                    oraConn = conn.Connect();
                    if (oraConn == null)
                        return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

         }
		
		public int UpdateSTRANGE_CHARACTER(STRANGE_CHARACTERInfo objTable)
		{
            string strSql = "Update STRANGE_CHARACTER set ID=:pID,STRANGE_CHAR=:pSTRANGE_CHAR,REPLACE_CHAR=:pREPLACE_CHAR,MSG_TYPE=:pMSG_TYPE,DEPARTMENT=:pDEPARTMENT,FIELD_CODE=:pFIELD_CODE,MSG_DIRECTION=:pMSG_DIRECTION,GWTYPE=:pGWTYPE,Line=:pLine,Position=:pPosition where ID=:pID";
            OracleParameter[] oraParam ={new OracleParameter("pID",OracleType.Number,10),
                                           new OracleParameter("pSTRANGE_CHAR",OracleType.VarChar,10),
                                           new OracleParameter("pREPLACE_CHAR",OracleType.VarChar,10),
                                           new OracleParameter("pMSG_TYPE",OracleType.VarChar,10),
                                           new OracleParameter("pDEPARTMENT",OracleType.VarChar,10),
                                           new OracleParameter("pFIELD_CODE",OracleType.VarChar,10),
                                           new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,10),
                                           new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                           new OracleParameter("pLine",OracleType.VarChar ,2),
                                           new OracleParameter("pPosition",OracleType.VarChar ,2)};
            oraParam[0].Value = objTable.ID;
            oraParam[1].Value = objTable.STRANGE_CHAR;
            oraParam[2].Value = objTable.REPLACE_CHAR;
            oraParam[3].Value = objTable.MSG_TYPE;
            oraParam[4].Value = objTable.DEPARTMENT;
            oraParam[5].Value = objTable.FIELD_CODE;
            oraParam[6].Value = objTable.MSG_DIRECTION;
            oraParam[7].Value = objTable.GWTYPE;
            oraParam[8].Value = objTable.LINE;
            oraParam[9].Value = objTable.POSITION;
			try
			{
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		
		public int DeleteSTRANGE_CHARACTER(int iID)
		{
            string strSql = "Delete from STRANGE_CHARACTER where ID=:pID";
            OracleParameter[] oraParam = { new OracleParameter("pID",OracleType.Number,10)};
            oraParam[0].Value =iID;
			try
			{
                oraConn=conn.Connect();
                if (oraConn==null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn,CommandType.Text,strSql,oraParam);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

        public DataSet GetSTRANGE_CHARACTER()
        {
            try
            {
                DataSet datDs = new DataSet();
                string strSql = "Select sch.ID,sch.STRANGE_CHAR,sch.REPLACE_CHAR,sch.MSG_TYPE,sch.DEPARTMENT,sch.FIELD_CODE,sch.MSG_DIRECTION,sch.GWTYPE, sch.Line, sch.Position from strange_character sch";
                oraConn=conn.Connect();
                if (oraConn == null)
                    return null;
                return DataAcess.ExecuteDataset(oraConn,CommandType.Text,strSql,null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataSet GetSTRANGE_CHARACTERSearch(string strSQL)
        {
            DataSet datDs = new DataSet();
            string strSql = "Select sch.ID,sch.STRANGE_CHAR,sch.REPLACE_CHAR,sch.MSG_TYPE,sch.DEPARTMENT,sch.FIELD_CODE,sch.MSG_DIRECTION,sch.GWTYPE, sch.Line, sch.Position from strange_character sch " + strSQL + " order by sch.STRANGE_CHAR";
            // string strSql = "select * from Currency";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        // Check ky tu la da dc su dung trong 1 kenh chua
        public bool IDIsExisting(string strStrangeChar, string strMSGType, string strDepartment, string strDirection,string strField)
        {
            string strSql = "Select * from strange_character where trim(STRANGE_CHAR) = '" + strStrangeChar + "' AND MSG_TYPE = '";
                   strSql = strSql + strMSGType + "'" + "AND DEPARTMENT = '" + strDepartment + "' and MSG_DIRECTION = '" + strDirection + "'";
                   strSql = strSql + "AND FIELD_CODE = '" + strField + "'";
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
        // BichNN add 03/08/2008
        //Check trung tat ca cac truong
        public int GetData(STRANGE_CHARACTERInfo objTable)
        {
             string strSql = "";
             strSql = "SELECT * FROM STRANGE_CHARACTER ";
             strSql = strSql + " WHERE GWTYPE = '" + objTable.GWTYPE + "' ";
             strSql = strSql + " AND MSG_DIRECTION = '" + objTable.MSG_DIRECTION + "'";
             
            if (string.IsNullOrEmpty(objTable.STRANGE_CHAR))
             {
                 strSql = strSql + "AND STRANGE_CHAR IS NULL";
             }
             else 
            {
                if (objTable.STRANGE_CHAR == "'")
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = ''" + objTable.STRANGE_CHAR + "'";
                }
                else if (objTable.STRANGE_CHAR == "''")
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = '''" + objTable.STRANGE_CHAR + "'";
                }
                else
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = '" + objTable.STRANGE_CHAR + "'";
                }
             }
            if (string.IsNullOrEmpty(objTable.REPLACE_CHAR))
            {
                strSql = strSql + " AND REPLACE_CHAR IS NULL";
            }
            else
            {
                if (objTable.REPLACE_CHAR == "'")
                {
                    strSql = strSql + " AND trim(REPLACE_CHAR) = ''" + objTable.REPLACE_CHAR + "'";
                }
                else
                {
                    strSql = strSql + " AND trim(REPLACE_CHAR) = '" + objTable.REPLACE_CHAR + "'";
                }
            }
             if (string.IsNullOrEmpty(objTable.MSG_TYPE))
             { 
                 strSql = strSql + " AND MSG_TYPE IS NULL "; 
             }
             else { 
                 strSql = strSql + " AND trim(MSG_TYPE) = '" + objTable.MSG_TYPE + "'"; 
             }
             if (string.IsNullOrEmpty(objTable.FIELD_CODE))
             { 
                 strSql = strSql + " AND FIELD_CODE IS NULL " ; 
             }
             else { 
                 strSql = strSql + " AND trim(FIELD_CODE) = '" + objTable.FIELD_CODE + "'"; 
             }
            //Voi truong Department la rong
            if (string.IsNullOrEmpty(objTable.DEPARTMENT))
            {
                 strSql = strSql + " AND DEPARTMENT IS NULL ";
            }else
            {
                strSql = strSql + " AND trim(DEPARTMENT) = '" + objTable.DEPARTMENT + "'";              
            }
            if (string.IsNullOrEmpty(objTable.LINE))
            {
                strSql = strSql + " AND LINE IS NULL " ; 
            }
            else {
                strSql = strSql + " AND trim(LINE) = '" + objTable.LINE + "'"; 
            }
            if (string.IsNullOrEmpty(objTable.POSITION))
            {
                strSql = strSql + " AND POSITION IS NULL ";
            }
            else 
            {
                strSql = strSql + " AND trim(POSITION) = '" + objTable.POSITION  + "'"; 
            }
           DataSet ds = new DataSet();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int CheckSTRANGE_CHARACTER_Dif(STRANGE_CHARACTERInfo objTable)
        {
            string strSql = "";
            strSql = "SELECT * FROM STRANGE_CHARACTER ";
            strSql = strSql + " WHERE GWTYPE = '" + objTable.GWTYPE + "' ";
            strSql = strSql + " AND MSG_DIRECTION = '" + objTable.MSG_DIRECTION + "'";

            if (string.IsNullOrEmpty(objTable.STRANGE_CHAR))
            {
                strSql = strSql + "AND STRANGE_CHAR IS NULL";
            }
            else
            {
                if (objTable.STRANGE_CHAR == "'")
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = ''" + objTable.STRANGE_CHAR + "'";
                }
                else if (objTable.STRANGE_CHAR == "''")
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = '''" + objTable.STRANGE_CHAR + "'";
                }
                else
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = '" + objTable.STRANGE_CHAR + "'";
                }
            }
            //if (string.IsNullOrEmpty(objTable.REPLACE_CHAR))
            //{
            //    strSql = strSql + " AND REPLACE_CHAR IS NULL";
            //}
            //else
            //{
            //    if (objTable.REPLACE_CHAR == "'")
            //    {
            //        strSql = strSql + " AND trim(REPLACE_CHAR) = ''" + objTable.REPLACE_CHAR + "'";
            //    }
            //    else
            //    {
            //        strSql = strSql + " AND trim(REPLACE_CHAR) = '" + objTable.REPLACE_CHAR + "'";
            //    }
            //}
            if (string.IsNullOrEmpty(objTable.MSG_TYPE))
            {
                strSql = strSql + " AND MSG_TYPE IS NULL ";
            }
            else
            {
                strSql = strSql + " AND trim(MSG_TYPE) = '" + objTable.MSG_TYPE + "'";
            }
            if (string.IsNullOrEmpty(objTable.FIELD_CODE))
            {
                strSql = strSql + " AND FIELD_CODE IS NULL ";
            }
            else
            {
                strSql = strSql + " AND trim(FIELD_CODE) = '" + objTable.FIELD_CODE + "'";
            }
            //Voi truong Department la rong
            if (string.IsNullOrEmpty(objTable.DEPARTMENT))
            {
                strSql = strSql + " AND DEPARTMENT IS NULL ";
            }
            else
            {
                strSql = strSql + " AND trim(DEPARTMENT) = '" + objTable.DEPARTMENT + "'";
            }
            if (string.IsNullOrEmpty(objTable.LINE))
            {
                strSql = strSql + " AND LINE IS NULL ";
            }
            else
            {
                strSql = strSql + " AND trim(LINE) = '" + objTable.LINE + "'";
            }
            if (string.IsNullOrEmpty(objTable.POSITION))
            {
                strSql = strSql + " AND POSITION IS NULL ";
            }
            else
            {
                strSql = strSql + " AND trim(POSITION) = '" + objTable.POSITION + "'";
            }
            DataSet ds = new DataSet();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int CheckSTRANGE_CHARACTER_AtLeast(STRANGE_CHARACTERInfo objTable)
        {
            string strSql = "";
            strSql = "SELECT * FROM STRANGE_CHARACTER ";
            strSql = strSql + " WHERE GWTYPE = '" + objTable.GWTYPE + "' ";
            strSql = strSql + " AND MSG_DIRECTION = '" + objTable.MSG_DIRECTION + "'";

            if (string.IsNullOrEmpty(objTable.STRANGE_CHAR))
            {
                strSql = strSql + "AND STRANGE_CHAR IS NULL";
            }
            else
            {
                if (objTable.STRANGE_CHAR == "'")
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = ''" + objTable.STRANGE_CHAR + "'";
                }
                else if (objTable.STRANGE_CHAR == "''")
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = '''" + objTable.STRANGE_CHAR + "'";
                }
                else
                {
                    strSql = strSql + "AND trim(STRANGE_CHAR) = '" + objTable.STRANGE_CHAR + "'";
                }
            }
            //if (string.IsNullOrEmpty(objTable.REPLACE_CHAR))
            //{
            //    strSql = strSql + " AND REPLACE_CHAR IS NULL";
            //}
            //else
            //{
            //    if (objTable.REPLACE_CHAR == "'")
            //    {
            //        strSql = strSql + " AND trim(REPLACE_CHAR) = ''" + objTable.REPLACE_CHAR + "'";
            //    }
            //    else
            //    {
            //        strSql = strSql + " AND trim(REPLACE_CHAR) = '" + objTable.REPLACE_CHAR + "'";
            //    }
            //}
            //if (string.IsNullOrEmpty(objTable.MSG_TYPE))
            //{
            //    strSql = strSql + " AND MSG_TYPE IS NULL ";
            //}
            //else
            //{
            //    strSql = strSql + " AND trim(MSG_TYPE) = '" + objTable.MSG_TYPE + "'";
            //}
            //if (string.IsNullOrEmpty(objTable.FIELD_CODE))
            //{
            //    strSql = strSql + " AND FIELD_CODE IS NULL ";
            //}
            //else
            //{
            //    strSql = strSql + " AND trim(FIELD_CODE) = '" + objTable.FIELD_CODE + "'";
            //}
            //Voi truong Department la rong
            if (string.IsNullOrEmpty(objTable.DEPARTMENT))
            {
                strSql = strSql + " AND DEPARTMENT IS NULL ";
            }
            else
            {
                strSql = strSql + " AND trim(DEPARTMENT) = '" + objTable.DEPARTMENT + "'";
            }
            //if (string.IsNullOrEmpty(objTable.LINE))
            //{
            //    strSql = strSql + " AND LINE IS NULL ";
            //}
            //else
            //{
            //    strSql = strSql + " AND trim(LINE) = '" + objTable.LINE + "'";
            //}
            //if (string.IsNullOrEmpty(objTable.POSITION))
            //{
            //    strSql = strSql + " AND POSITION IS NULL ";
            //}
            //else
            //{
            //    strSql = strSql + " AND trim(POSITION) = '" + objTable.POSITION + "'";
            //}
            DataSet ds = new DataSet();
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

	}
	
	
}
