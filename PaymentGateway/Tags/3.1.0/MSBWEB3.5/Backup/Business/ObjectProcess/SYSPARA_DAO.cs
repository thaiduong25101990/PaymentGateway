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
using BIDVWEB.Comm;
using BIDVWEB.Comm.DA;

namespace BIDVWEB.Business
{
    public class SYSPARA_DAO
    {
        private System.Data.OracleClient.OracleConnection oraConn;
        private clsConnection objConn = new clsConnection();
        private UserEncrypt Encrypt = new UserEncrypt();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        public string strError = "";

        //Ham khoi tao doi tuong///////////////////////////////////////
        //Mo ta:        Ham khoi tao doi tuong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        public SYSPARA_DAO()
		{

		}


        //Ham tao mot Instance cua doi tuong///////////////////////////
        //Mo ta:        Ham tao mot Instance cua doi tuong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Doi tuong SYSPARA_DAO
        ///////////////////////////////////////////////////////////////
        public static SYSPARA_DAO Instance()
		{
            return new SYSPARA_DAO();
		}


        //Ham them moi tham so he thong////////////////////////////////
        //Mo ta:        Ham them moi tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      objTable: Bang SYSVAR
        //Dau ra:       Them moi thanh cong
        ///////////////////////////////////////////////////////////////
        public int AddSYS_PARA(SYSPARA_Info objTable)
        {
            DataTable datTable = new DataTable();
            int iBool;
            string strSQL = "Insert into SYSVAR(GWTYPE,VARNAME,VALUE,TYPE,NOTE) " +
                "values (:pGWTYPE,:pVARNAME,:pVALUE,:pTYPE,:pNOTE)";
            OracleParameter[] oraParam = {new OracleParameter("pGWTYPE", OracleType.VarChar, 10),
                                         new OracleParameter("pVARNAME", OracleType.NVarChar, 30),
                                         new OracleParameter("pVALUE", OracleType.NVarChar,30),
                                         new OracleParameter("pTYPE", OracleType.VarChar,30),
                                         new OracleParameter("pNOTE", OracleType.NVarChar,100)};
            oraParam[0].Value = objTable.GWTYPE;
            oraParam[1].Value = objTable.VARNAME;
            oraParam[2].Value = objTable.VALUE;
            oraParam[3].Value = objTable.TYPE;
            oraParam[4].Value = objTable.NOTE;

            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                iBool= clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return -1;
            }
        }


        //Ham cap nhat tham so he thong////////////////////////////////
        //Mo ta:        Ham cap nhat tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      objTable: Bang SYSVAR
        //Dau ra:       Them moi thanh cong
        ///////////////////////////////////////////////////////////////
        public int UpdateSYS_PARA(SYSPARA_Info objTable)
        {
            int iBool;
            string strSQL = "Update SYSVAR SET VARNAME =:pVARNAME, " +
                " VALUE =:pVALUE,TYPE =:pTYPE,NOTE =:pNOTE WHERE ID = " + objTable.ID;

            OracleParameter[] oraParam = {new OracleParameter("pVARNAME", OracleType.NVarChar, 30),
                                         new OracleParameter("pVALUE", OracleType.NVarChar,30),
                                         new OracleParameter("pTYPE", OracleType.VarChar,30),
                                         new OracleParameter("pNOTE", OracleType.NVarChar,100)
                                         };                       
            
            oraParam[0].Value = objTable.VARNAME;
            oraParam[1].Value = objTable.VALUE;
            oraParam[2].Value = objTable.TYPE;
            oraParam[3].Value = objTable.NOTE;

            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParam);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return -1;
            }
        }


        //Ham lay thong tin mot tham so he thong///////////////////////
        //Mo ta:        Ham lay thong tin mot tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong trong bang SYSVAR
        //Dau ra:       Them moi thanh cong
        ///////////////////////////////////////////////////////////////
        public DataSet GetSysParaByID(long iID)
        {
            string strSQL = "SELECT * FROM SYSVAR WHERE ID = " + iID;

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return null;
            }
        }


        //Ham lay thong tin all tham so he thong///////////////////////
        //Mo ta:        Ham lay thong tin all tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong trong bang SYSVAR
        //Dau ra:       Them moi thanh cong
        ///////////////////////////////////////////////////////////////
        public DataSet GetAllSysPara()
        {
            string strSQL = "SELECT * FROM SYSVAR ORDER BY ID";

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL, "");
            }
            catch (Exception ex)
            {
                strError = ex.Message;                 
                return null;
            }
        }

        //Ham xoa mot tham so he thong/////////////////////////////////
        //Mo ta:        Ham xoa mot tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong trong bang SYSVAR
        //Dau ra:       Xoa thanh cong
        ///////////////////////////////////////////////////////////////
        public int DelParaByID(long iID)
        {
            string strSQL = "DELETE FROM SYSVAR WHERE ID = " + iID;

            try
            {                
                if (!objDataAccess.ExecuteSQL(strSQL))
                {
                    strError = objDataAccess.strError;
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }

        //Ham check trung ten tham so//////////////////////////////////
        //Mo ta:        Ham check trung ten tham so
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong trong bang SYSVAR
        //Dau ra:       Xoa thanh cong
        ///////////////////////////////////////////////////////////////
        public int CheckParaByName(string sName, long iID)
        {
            string strSQL;
            DataSet ds = new DataSet();

            try
            {
                //them moi
                if (iID == 0)
                {
                    strSQL  = "SELECT * FROM SYSVAR " + 
                        "WHERE upper(VARNAME) = upper('" + sName + "')";
                }
                //Sua
                else
                {
                    strSQL = "SELECT * FROM SYSVAR " + 
                        "WHERE upper(VARNAME) = upper('" + sName + "')" +
                        " AND ID<>" + iID;
                }
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
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
                strError = ex.Message;                
                return -1;
            }
        }


        //Ham check tham so dang duoc su dung//////////////////////////
        //Mo ta:        Ham check tham so dang duoc su dung
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong trong bang SYSVAR
        //Dau ra:       Dang dc su dung
        ///////////////////////////////////////////////////////////////
        public int CheckParaUsing(long iID)
        {
            string strSQL;
            DataSet ds = new DataSet();

            try
            {
                strSQL = "SELECT * FROM SYSVAR WHERE ISNOTDEL =1 AND ID=" + iID;               
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
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
                strError = ex.Message;
                return -1;
            }
        }


        //Ham lay tham so NSD cua tung chi nhanh///////////////////////
        //Mo ta:        Ham lay tham so NSD cua tung chi nhanh
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Gia tri cua bien
        ///////////////////////////////////////////////////////////////
        public int GetAccountNumber()
        {
            DataSet ds = new DataSet();
            DataRow dr;
            string strSQL = "SELECT * FROM SYSVAR WHERE upper(VARNAME)=upper('AccountNumber')";

            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return Convert.ToInt16(dr["VALUE"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }


        //Ham lay tham so so ngay NSD chua log in vao he thong/////////
        //Mo ta:        Ham lay tham so so ngay NSD chua log in vao HT
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Gia tri cua bien
        ///////////////////////////////////////////////////////////////
        public int GetNumLoginDay()
        {
            DataSet ds = new DataSet();
            DataRow dr;
            string strSQL = "SELECT * FROM SYSVAR WHERE upper(VARNAME)=upper('NumLoginDay')";
            
            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");                
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return Convert.ToInt16(dr["VALUE"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }


        //Ham lay tham so so lan NSD log in vao HT sai user/pass///////
        //Mo ta:        Ham lay tham so so lan NSD log in vao HT 
        //              sai user/pass
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Gia tri cua bien
        ///////////////////////////////////////////////////////////////
        public int GetLoginTime()
        {
            DataSet ds = new DataSet();
            DataRow dr;
            string strSQL = "SELECT * FROM SYSVAR WHERE upper(VARNAME)=upper('LoginTime')";
                        
            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                { 
                    dr = ds.Tables[0].Rows[0];
                    return Convert.ToInt16(dr["VALUE"].ToString());
                }
                else
                {
                    return -1;
                }                                
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }

        //Ham lay tham so so lan doi pass ko dc trung nhau/////////////
        //Mo ta:        Ham lay tham so so lan doi pass ko dc 
        //              trung nhau
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Gia tri cua bien
        ///////////////////////////////////////////////////////////////
        public int GetPassTime()
        {            
            DataSet ds = new DataSet();
            DataRow dr;
            string strSQL = "SELECT * FROM SYSVAR WHERE upper(VARNAME)=upper('Passtime')";

            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");                
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return Convert.ToInt16(dr["VALUE"]);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }
    }
}
