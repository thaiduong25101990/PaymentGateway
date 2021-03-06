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
using BIDVWEB.Business.UserRight;

namespace BIDVWEB.Business
{
	public class USERS_DAO
	{
        private clsUserRight objUser = new clsUserRight();
        private OracleConnection oraConn;
        private UserEncrypt Encrypt = new UserEncrypt();        
        private clsConnection objConn = new clsConnection();
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsCommon objCommon = new clsCommon();
        public string strError = "";
        private string strBrHo = "";

        public USERS_DAO()
		{

		}
        public static USERS_DAO Instance()
		{
            return new USERS_DAO();
		}
		
		public int AddUSERS(USERSInfo objTable)
		{
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pBRANCH",OracleType.VarChar,3),
                                new OracleParameter("pUSERID",OracleType.VarChar,20) ,
                                new OracleParameter("pUSERNAME",OracleType.NVarChar,100),
                                new OracleParameter("pPASSWORD",OracleType.VarChar,150),
                                new OracleParameter("pSTATUS",OracleType.Number,1),
                                new OracleParameter("pPASSTIME",OracleType.DateTime,7),
                                new OracleParameter("pMOBILE",OracleType.VarChar,20),
                                new OracleParameter("pEMAIL",OracleType.VarChar,20),
                                new OracleParameter("pDESCRIPTION",OracleType.NVarChar,100),
                                new OracleParameter("pLASTDATE",OracleType.DateTime,7),
                                new OracleParameter("pCOUNTTIME",OracleType.Number,7)};
                
                oraParas[0].Value = objTable.BRANCH;
                oraParas[1].Value = objTable.USERID;
                oraParas[2].Value = objTable.USERNAME;
                if (!string.IsNullOrEmpty(objTable.PASSWORD))
                    oraParas[3].Value = Encrypt.Encrypt(objTable.PASSWORD);
                else
                    oraParas[3].Value = objTable.PASSWORD;
                oraParas[4].Value = objTable.STATUS;
                oraParas[5].Value = objTable.PASSTIME;
                oraParas[6].Value = objTable.MOBILE;
                oraParas[7].Value = objTable.EMAIL;
                oraParas[8].Value = objTable.DESCRIPTION;
                oraParas[9].Value = objTable.PASSTIME;
                oraParas[10].Value = objTable.COUNTTIME;
              
                oraConn = objConn.Connect();
                if (oraConn == null)                    
                    return -1;                    
                iResult = clsDataAcessComm.ExecuteNonQuery(oraConn, 
                    CommandType.StoredProcedure, "GW_PK_USERS_ACCESS.User_Insert", oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }                
                return iResult;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }                
                strError = ex.Message;                    
                return -1;
            }                                                                
        }

        public int UpdateUSERSTATUS(string strUserID, string strStatus)
        {            
            try
            {                
                string strSQL = "Update Users set status = " + strStatus + 
                    " where UserID ='" + strUserID.Trim() + "'";
                if (!objDataAccess.ExecuteSQL(strSQL))
                    return -1;
                else
                    return 1;                
            }
            catch (Exception ex)
            {                
                strError = ex.Message;                
                return -1;
            }                        
        }


        //Ham cap nhat thoi gian log cuoi cung/////////////////////////
        //Mo ta:        Ham cap nhat thoi gian log cuoi cung
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Thanh cong
        ///////////////////////////////////////////////////////////////
        public int UPDATE_LASTDATE(string pUserID, DateTime pLASTDATE)
        {
            int iBool;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                string strSQL = "Update USERS U set U.LASTDATE=:pLASTDATE " +
                    " where U.UserID=:pUserID";
                OracleParameter[] oraParas ={ new OracleParameter("pUserID",OracleType.VarChar,20),                                                    
                                            new OracleParameter("pLASTDATE",OracleType.DateTime,7)                                            
                                            };
                oraParas[0].Value = pUserID;                                
                oraParas[1].Value = pLASTDATE;
                iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
        }



        //Ham thay doi pass////////////////////////////////////////////
        //Mo ta:        Ham thay doi pass
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Thanh cong
        ///////////////////////////////////////////////////////////////
        public int UPDATE_PASS(string pUserID, string pPassword, DateTime pPASTIME, DateTime pLASTDATE)
        {
            int iBool;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)            
                    return -1;            

                DataTable datTable = new DataTable();
                //string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME," + 
                //" U.LASTDATE=:pLASTDATE  where U.UserID=:pUserID";
                string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME " + 
                    " where U.UserID=:pUserID";
                OracleParameter[] oraParas ={ new OracleParameter("pUserID",OracleType.VarChar,20),
                                                    new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                            new OracleParameter("pPASTIME",OracleType.DateTime,7)
                                            //,new OracleParameter("pLASTDATE",OracleType.DateTime,7)
                                            };
                oraParas[0].Value = pUserID;
                oraParas[1].Value =Encrypt.Encrypt(pPassword);
                oraParas[2].Value = pPASTIME;
                //oraParas[3].Value = pLASTDATE;
                iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;                
                return -1;
            }           
        }


        //Ham thay doi pass khi reset pass/////////////////////////////
        //Mo ta:        Ham thay doi pass khi reset pass
        //              Thuc hien cap nhat mat khau, thoi gian thay 
        //              doi pass, dat lai trang thai la pending
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Thanh cong
        ///////////////////////////////////////////////////////////////
        public int UPDATE_PASS_RESET(string pUserID, string pPassword, DateTime pPASTIME, int pSTATUS)
        {
            int iBool;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return -1;
                
                string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME, " +
                    "U.STATUS=:pSTATUS where U.UserID=:pUserID";
                OracleParameter[] oraParas ={ new OracleParameter("pUserID",OracleType.VarChar,20),
                                                    new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                            new OracleParameter("pPASTIME",OracleType.DateTime,7),
                                            new OracleParameter("pSTATUS",OracleType.Number,1)
                                            };
                oraParas[0].Value = pUserID;
                oraParas[1].Value = Encrypt.Encrypt(pPassword);
                oraParas[2].Value = pPASTIME;
                oraParas[3].Value = pSTATUS;                
                iBool = clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
        }

		public int UpdateUSERS(USERSInfo objTable)
		{
            int iBool;
            try
            {
                OracleParameter[] oraParas ={ //new OracleParameter("pID",OracleType.Number,10),
                                                //new OracleParameter("pBRANCH",OracleType.VarChar,3),
                                                new OracleParameter("pUSERID",OracleType.VarChar,20),                                                
                                                new OracleParameter("pUSERNAME",OracleType.NVarChar,100),
                                                //new OracleParameter("pPASSWORD",OracleType.VarChar,150),
                                                new OracleParameter("pSTATUS",OracleType.Number,1),
                                                //new OracleParameter("pPASSTIME",OracleType.DateTime,7),
                                                new OracleParameter("pMOBILE",OracleType.VarChar,20),
                                                new OracleParameter("pEMAIL",OracleType.VarChar,20),
                                                new OracleParameter("pDESCRIPTION",OracleType.NVarChar,100),
                                                new OracleParameter("pLASTDATE",OracleType.DateTime,7)
                                                };
                //oraParas[0].Value = objTable.ID;
                //oraParas[1].Value = objTable.BRANCH;
                oraParas[0].Value = objTable.USERID;                
                oraParas[1].Value = objTable.USERNAME;
                //oraParas[4].Value = objTable.PASSWORD;
                oraParas[2].Value = objTable.STATUS;
                //oraParas[6].Value = objTable.PASSTIME;
                oraParas[3].Value = objTable.MOBILE;
                oraParas[4].Value = objTable.EMAIL;
                oraParas[5].Value = objTable.DESCRIPTION;
                oraParas[6].Value = objTable.LASTDATE;
                
                oraConn = objConn.Connect();
                if (oraConn == null)                    
                    return -1;                                                               
                iBool= clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, 
                        "GW_PK_USERS_ACCESS.User_Update", oraParas);
                             
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;               
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }
		}
        public int GetUpdate_chengePass(int iID, string pPassword, DateTime pPASTIME, DateTime pLASTDATE)
        {
            int iBool;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)            
                    return -1;                

                DataTable datTable = new DataTable();
                string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME," +
                    "U.LASTDATE=:pLASTDATE  where U.ID=:pID";
                OracleParameter[] oraParas ={ new OracleParameter("pID",OracleType.Number,20),
                                            new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                            new OracleParameter("pPASTIME",OracleType.DateTime,7),
                                            new OracleParameter("pLASTDATE",OracleType.DateTime,7)};
                oraParas[0].Value = iID;
                oraParas[1].Value = pPassword;
                oraParas[2].Value = pPASTIME;
                oraParas[3].Value = pLASTDATE;
                iBool=clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;                
                return -1;
            }            
        }


        public int DeleteUSERS(int ID)
        {
            oraConn = objConn.Connect();
            if (oraConn == null)
            {
                return -1;
            }
                        
            string strSQL = "Delete from Users u where u.UserID='" + ID + "'";

            try
            {
                clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                
                return 1;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                oraConn.Close();
                oraConn.Dispose();
                return -1;
            }            
        }

        // hàm lấy toàn bộ thông tin của user có trong bảng.
        public DataSet GetUSERS()
        {
            string strSQL = "select * from Users u";

            try
            {
                return objDataAccess.dsGetDataSourceByStr(strSQL,"");
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return null;
            }
        }

        // hàm lấy dữ liệu với điều kiện
        // với điều kiện so sánh ở 3 bảng
        public DataSet GetUSERSGROUP(string groupname)
        {            
            string strSQL = "select * from Users u";

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

        public DataSet GetUSERS_PASS(string pUsername)
        {           
            string strSQL = "select * from Users u where Trim(u.username)='" + pUsername + "'";

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

        public DataTable GetUSERS_PASS1(string pUserID)
        {            
            string strSQL = "select * from Users u where u.userid='" + pUserID + "'";
            DataSet ds = new DataSet();

            try
            {
                ds= objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                strError = ex.Message;               
                return null;
            }
        }


        public DataSet GetUSERS_BRANCH()
        {            
            string strSQL = "select Distinct(b.sibs_bank_code) from Branch  b";

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

        public DataSet GetUSERS_BRANCHT()
        {            
            string strSQL = "select distinct(u.branch) from Users u";

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

        //Ham lay danh sach NSD thuoc chi nhanh////////////////////////
        //Mo ta:        Ham lay danh sach NSD thuoc chi nhanh
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      pBRANCH: Ma chi nhanh
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        public DataSet GetUSERSBR(string pBRANCH, string sBranchUser)
        {           

            string strSQL;
            string strBrHo = "";

            strBrHo = objUser.GetBrHo();
            if (pBRANCH.PadLeft(5, '0').ToString() != strBrHo)
            {
                if (sBranchUser.PadLeft(5, '0').ToString() == strBrHo)
                {
                    strSQL = "select * from Users u where u.branch='" + pBRANCH +
                    "' ORDER BY ID, USERID ASC ";
                }
                else
                {
                    strSQL = "select * from Users u where u.branch='" + pBRANCH +
                    "' AND USERID NOT IN (SELECT DISTINCT A.USERID FROM USER_GROUPS A INNER JOIN GROUPS B " +
                    " ON A.GROUPID =B.GROUPID WHERE B.ISADMIN =1) ORDER BY ID, USERID ASC ";
                }
            }
            else
            {
                strSQL = "select * from Users u where u.branch='" +
                    pBRANCH + "' ORDER BY ID, USERID ASC ";
            }
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


        //Ham lay danh sach NSD theo dieu kien//////////////////////////
        //Mo ta:        Ham lay danh sach NSD theo dieu kien
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      pWhere: Dieu kien
        //              pBRANCH: Ma chi nhanh
        //              sUserName: Ten NSD
        //              sUserID: Ma NSD
        //Dau ra:       Danh sach NSD
        ///////////////////////////////////////////////////////////////
        public DataSet GetUsersByStr(string pBRANCH, string sBranchUser, string sUserName, string sUserID)
        {

            string strSQL;
            strBrHo = objUser.GetBrHo();
            DataSet ds = new DataSet();

            
            if (pBRANCH.ToString() == "ALL")
            {
                strSQL = "select U.ID, u.BRANCH,u.USERID,u.USERNAME,u.DESCRIPTION " +
                " from Users u where u.USERID LIKE '%' ||:pUserID || '%' and " +
                " u.USERNAME like '%' || :pUserName || '%' ORDER BY u.USERID ASC ";
            }
            else if (pBRANCH.PadLeft(5, '0').ToString() == strBrHo)
            {                
                strSQL = "select U.ID, u.BRANCH,u.USERID,u.USERNAME,u.DESCRIPTION " +
                " from Users u where u.USERID LIKE '%' ||:pUserID || '%' and " +
                " u.USERNAME like '%' || :pUserName || '%' " +
                " AND u.branch='" + pBRANCH + "'" + 
                " ORDER BY u.USERID ASC ";
            }
            else
            {
                if (sBranchUser.PadLeft(5, '0').ToString() == strBrHo)
                    strSQL = "select U.ID, u.BRANCH,u.USERID,u.USERNAME,u.DESCRIPTION " +
                    " from Users u where u.USERID LIKE '%' ||:pUserID || '%' and " +
                    " u.USERNAME like '%' || :pUserName || '%' " +
                    " AND u.branch='" + pBRANCH + "'" + 
                    " ORDER BY u.USERID ASC ";
                else
                    strSQL = "select U.ID, u.BRANCH,u.USERID,u.USERNAME,u.DESCRIPTION " +
                    " from Users u where u.USERID LIKE '%' ||:pUserID || '%' and " +
                    " u.USERNAME like '%' || :pUserName || '%' " +
                    " AND u.branch='" + pBRANCH + "'" +
                    " AND u.USERID NOT IN (SELECT DISTINCT A.USERID FROM USER_GROUPS A INNER JOIN GROUPS B " +
                    " ON A.GROUPID =B.GROUPID WHERE B.ISADMIN =1) " + 
                    " ORDER BY u.USERID ASC ";                    
            }
            try
            {
                //return objDataAccess.dsGetDataSourceByStr(strSQL, "");                                
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return null;
                OracleParameter[] oraParas ={ new OracleParameter("pUserName",OracleType.NVarChar,10),
                                            new OracleParameter("pUserID",OracleType.VarChar,100)
                                            };
                oraParas[0].Value = sUserName;
                oraParas[1].Value = sUserID;
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return null;
            }
        }


        // ham lay rausername va password
        public DataSet GET_USER_PASS(string pUserid, string pPASSWORD)
        {           
            string strSQL = "select u.id,u.userid,u.username,u.password,u.status, " +
                "u.passtime, u.lastdate from users u where upper(u.userid)= upper('" + 
                pUserid + "') and u.password='" + pPASSWORD + "'";

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

        public DataSet GetUSERS_GROUP(int  pGROUPID)
        {            
            string strSQL = "select u.userid,u.username,u.status from Groups g," +
                "user_groups gu,users u where g.groupid=gu.groupid and " +
                "gu.userid=u.userid and g.groupid='" + pGROUPID + "'";

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
                

        public DataSet GetUSERSUPDATEPASS(string pUSERID)
        {            
            string strSQL = "select * from users u where Trim(u.userid)='" + pUSERID.Trim() + "'";

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

        public DataTable GetRoll(string pUserID)
        {
            string strSQL = "select Distinct (select a.content from Allcode a " +
                "where Trim(a.cdname)='Roll' and trim(a.cdval)= g.issupervisor) " +
                " as issupervisor from Users u, groups g, User_Groups ug " +
                " where u.userid = ug.userid and ug.groupid = g.groupid and " + 
                " trim(u.userid) = '" + pUserID + "'";
            DataSet ds = new DataSet();

            try
            {                
                
                ds= objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return null;
            }
        }


        //Ham kiem tra pass cu////////////////////////////////////////////
        //Mo ta:        Ham kiem tra pass cu
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Thanh cong
        ///////////////////////////////////////////////////////////////
        public int CHECK_OLDPASS(string pUserID, string pPassword)
        {            
            DataSet ds = new DataSet();
            string strSQL = "select * from users u where Trim(u.userid)='" + pUserID.Trim() +
                "' AND upper(u.PASSWORD) = upper('" + Encrypt.Encrypt(pPassword.Trim()) + "')";
            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL,"");                    
                if (ds != null && ds.Tables[0].Rows.Count > 0)               
                    return 1;                
                else                
                    return -1;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }            
        }

        //Ham thay doi pass lan dau khi login vao he thong/////////////
        //Mo ta:        Ham thay doi pass lan dau khi login vao he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Thanh cong
        ///////////////////////////////////////////////////////////////
        public int UPDATE_PASS1(string pUserID, string pPassword, DateTime pPASTIME, 
            DateTime pLASTDATE, int pSTATUS, int pCOUNTTIME)
        {
            int iBool;

            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)            
                    return -1;            
                
                string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME," +
                    " U.LASTDATE=:pLASTDATE, U.STATUS=:pSTATUS, U.COUNTTIME= U.COUNTTIME + :pCOUNTTIME " + 
                    " where to_char(U.UserID)=:pUserID";
                OracleParameter[] oraParas ={ new OracleParameter("pUserID",OracleType.VarChar,20),
                                            new OracleParameter("pPASSWORD",OracleType.VarChar,150),
                                            new OracleParameter("pPASTIME",OracleType.DateTime,7),
                                            new OracleParameter("pLASTDATE",OracleType.DateTime,7),
                                            new OracleParameter("pSTATUS",OracleType.Number),
                                            new OracleParameter("pCOUNTTIME",OracleType.Number)};
                oraParas[0].Value = pUserID;
                oraParas[1].Value = Encrypt.Encrypt(pPassword);
                oraParas[2].Value = pPASTIME;
                oraParas[3].Value = pLASTDATE;                
                oraParas[4].Value = pSTATUS;                
                oraParas[5].Value = pCOUNTTIME;

                iBool=clsDataAcessComm.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return iBool;
            }
            catch (Exception ex)
            {                
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return -1;
            }            
        }                

        //Ham lay thong tin all tham so he thong///////////////////////
        //Mo ta:        Ham lay thong tin all tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iID: Ma tham so he thong trong bang SYSVAR
        //Dau ra:       Them moi thanh cong
        ///////////////////////////////////////////////////////////////
        public int GetSumUser_Branch(string sBranch)
        {
            DataSet ds = new DataSet();
            DataRow dr;
            string strSQL = "SELECT count(*) AS SUM FROM USERS WHERE upper(BRANCH)=upper('" + 
                sBranch + "')";

            try
            {                
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return Convert.ToInt16(dr["SUM"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                return -1;
            }
        }

        //Ham kiem tra user admin//////////////////////////////////////
        //Mo ta:        Ham kiem tra user admin
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Khong cho xoa
        ///////////////////////////////////////////////////////////////
        public int CHECK_USER_ADMIN(string pUserID)
        {           
            DataSet ds = new DataSet();
            string strSQL = "select * from users u where Trim(u.userid)='" + pUserID.Trim() +
                "' AND upper(u.USERNAME) = upper('admin')";
            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)                
                    return 1;                
                else                
                    return -1;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;               
                return -1;
            }
        }

        //Ham kiem tra do dai pass/////////////////////////////////////
        //Mo ta:        Ham kiem tra do dai pass
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        public bool CHECK_PASS_LENGTH(string sPass, out int iNumber)
        {
            iNumber = 0;
            try
            {
                DataSet ds = new DataSet();
                string strSQL = "select u.VALUE from sysvar u where u.VARNAME='LEN_OF_PASS'";

                if (string.IsNullOrEmpty(sPass))
                    return false;
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    if (Convert.ToInt16(ds.Tables[0].Rows[0]["VALUE"].ToString()) > sPass.Length)
                    {
                        iNumber = Convert.ToInt16(ds.Tables[0].Rows[0]["VALUE"].ToString());
                        return false;
                    }
                    else
                        return true;
                else
                    return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Ham kiem tra xau pass////////////////////////////////////////
        //Mo ta:        Ham kiem tra xau pass co:
        //              - Cac ky tu so: 1 -> 9
        //              - Cac ky tu chu hoa A -> Z
        //              - Cac ky tu chu thuong a -> z
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        public bool CHECK_PASS_STRING(string sPass, out int intNum)
        {
            intNum = 0;
            try
            {
                bool isNumber = false;
                bool isChar = false;
                int intChar = 0;
                string sPassUpper="";

                if (string.IsNullOrEmpty(sPass))                
                    return false;                
                //Kiem tra co ky tu so                
                foreach (char c in sPass)
                {
                    intChar = (int)c;
                    if (intChar >= 48 && intChar <= 57)
                    {
                        isNumber = true;
                        break;
                    }
                }
                //Upper
                sPassUpper = sPass.ToUpper();
                //Kiem tra co ky tu a-z, A-Z                
                foreach (char c in sPassUpper)
                {
                    intChar = (int)c;
                    if (intChar >= 65 && intChar <= 90)
                    {
                        isChar = true;
                        break;
                    }
                }
                if (isNumber == true && isChar == true)
                    return true;
                else
                {
                    if (isNumber == false)
                        intNum = 1;
                    else if (isChar == false)
                        intNum = 2;
                    return false;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }



        //Ham lay ten NSD theo UserID//////////////////////////////////
        //Mo ta:        Ham lay ten NSD theo UserID
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        public string GetUSERNAME(string sUserID)
        {
            string strSQL = "select * from Users u where u.USERID='" + sUserID + "'";
            DataSet ds = new DataSet();
            string strUserName = "";

            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strUserName= ds.Tables[0].Rows[0]["USERNAME"].ToString();
                }
                return strUserName;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }


        //Check USERID, USERNAME///////////////////////////////////////
        //Mo ta:        Check USERID, USERNAME
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Thanh cong
        ///////////////////////////////////////////////////////////////
        public bool CHECK_USERNAME(string sUserID, string sUserName, bool bAdd)
        {            
            DataSet ds = new DataSet();
            string strSQL;
            try
            {
                oraConn = objConn.Connect();
                if (oraConn == null)
                    return false;

                //Kiem tra trung UserID 
                if (bAdd == true)
                {
                    strSQL = "SELECT USERID,USERNAME FROM USERS WHERE USERID =:pUserID";
                    OracleParameter[] oraParas ={ new OracleParameter("pUserID",OracleType.VarChar,20)};
                    oraParas[0].Value = sUserID;                    
                    ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        strError = "Mã NSD đã có, nhập mã NSD khác!";
                        if (oraConn.State == ConnectionState.Open)
                        {
                            oraConn.Close();
                            oraConn.Dispose();
                        }
                        return false;
                    }                    
                }
                //Bo check ten vi hien tai dang nhap he thong theo ma NSD

                //////Kiem tra trung UserName   
                ////if (ds != null)
                ////{
                ////    ds.Clear();
                ////}
                ////if (bAdd == true)
                ////{
                ////    strSQL = "SELECT USERID,USERNAME FROM USERS WHERE USERNAME=:pUserName";
                ////    OracleParameter[] oraParas = {new OracleParameter("pUserName",OracleType.NVarChar,100)};
                ////    oraParas[0].Value = sUserName;
                ////    ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
                ////    if (ds != null && ds.Tables[0].Rows.Count > 0)
                ////    {
                ////        strError = "Tên NSD đã có, nhập tên NSD khác!";
                ////        if (oraConn.State == ConnectionState.Open)
                ////        {
                ////            oraConn.Close();
                ////            oraConn.Dispose();
                ////        }
                ////        return false;
                ////    }   
                ////}
                ////else
                ////{
                ////    strSQL = "SELECT USERID,USERNAME FROM USERS WHERE USERNAME=:pUserName " +
                ////        " AND USERID=:pUserID";
                ////    OracleParameter[] oraParas ={ new OracleParameter("pUserName",OracleType.NVarChar,10),                                                    
                ////                            new OracleParameter("pUserID",OracleType.VarChar,100)                                            
                ////                            };
                ////    oraParas[0].Value = sUserID;
                ////    oraParas[1].Value = sUserName;
                ////    ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.Text, strSQL, oraParas);
                ////    if (ds != null && ds.Tables[0].Rows.Count > 1)
                ////    {
                ////        strError = "Tên NSD đã có, nhập tên NSD khác!";
                ////        if (oraConn.State == ConnectionState.Open)
                ////        {
                ////            oraConn.Close();
                ////            oraConn.Dispose();
                ////        }
                ////        return false;
                ////    } 
                ////}        
                return true;
            }
            catch (Exception ex)
            {
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return false;
            }
        }

        public string GetBrHo()
        {
            DataSet ds = new DataSet();
            DataRow dr;
            string strSQL = "SELECT * FROM ALLCODE WHERE CDNAME='BR_HO'";

            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["CDVAL"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }
	}
	
	
}
