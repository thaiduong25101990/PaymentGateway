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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 27/05/2008
//' =============================================
namespace  BR.BRBusinessObject
{
	public class USERSDP
	{

        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
		
		public USERSDP()
		{
		}
		public static USERSDP Instance()
		{
			return new USERSDP();
		}
		
		public int AddUSERS(USERSInfo objTable)
		{
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pBRANCH",OracleType.VarChar,3),
                                                new OracleParameter("pUSERID",OracleType.VarChar,20) ,
                                                new OracleParameter("pUSERNAME",OracleType.VarChar,100),
                                                new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                                new OracleParameter("pSTATUS",OracleType.Number,1),
                                                new OracleParameter("pPASSTIME",OracleType.DateTime,7),
                                                new OracleParameter("pMOBILE",OracleType.VarChar,20),
                                                new OracleParameter("pEMAIL",OracleType.VarChar,20),
                                                new OracleParameter("pDESCRIPTION",OracleType.VarChar,100),
                                                new OracleParameter("pLASTDATE",OracleType.DateTime,7)};

                
                oraParas[0].Value = objTable.BRANCH;
                oraParas[1].Value = objTable.USERID;
                oraParas[2].Value = objTable.USERNAME;
                oraParas[3].Value = objTable.PASSWORD;
                oraParas[4].Value = objTable.STATUS;
                oraParas[5].Value = objTable.PASSTIME;
                oraParas[6].Value = objTable.MOBILE;
                oraParas[7].Value = objTable.EMAIL;
                oraParas[8].Value = objTable.DESCRIPTION;
                oraParas[9].Value = objTable.PASSTIME;
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_USERS_ACCESS.User_Insert", oraParas);
                    }
                }
                catch 
                {                    
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
            finally
            {
                oraConn.Dispose();
            }
        }

        public int UpdateUSERSTATUS(string strUserID, string strStatus)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                string strSQL = "Update Users set status = " + strStatus + " where UserID ='" + strUserID.Trim() + "'";
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL);
            }
            catch 
            {
                
                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }
           
            return 1;

        }

        public int UPDATEUSER_PASS(string pUserID, string pPassword, DateTime pPASTIME, DateTime pLASTDATE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }

            DataTable datTable = new DataTable();
            string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME,U.LASTDATE=:pLASTDATE  where U.UserID=:pUserID";
            OracleParameter[] oraParas ={ new OracleParameter("pUserID",OracleType.Number,20),
                                                new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                        new OracleParameter("pPASTIME",OracleType.DateTime,7),
                                        new OracleParameter("pLASTDATE",OracleType.DateTime,7)};
            oraParas[0].Value = pUserID;
            oraParas[1].Value = pPassword;
            oraParas[2].Value = pPASTIME;
            oraParas[3].Value = pLASTDATE;

            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);
            }
            catch 
            {
                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }
            return 1;
        }
		public int UpdateUSERS(USERSInfo objTable)
		{            
            try
            {
                OracleParameter[] oraParas ={ new OracleParameter("pID",OracleType.Number,10),
                                                new OracleParameter("pBRANCH",OracleType.VarChar,3),
                                                new OracleParameter("pUSERID",OracleType.VarChar,20),                                                
                                                new OracleParameter("pUSERNAME",OracleType.VarChar,100),
                                                new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                                new OracleParameter("pSTATUS",OracleType.Number,1),
                                                new OracleParameter("pPASSTIME",OracleType.DateTime,7),
                                                new OracleParameter("pMOBILE",OracleType.VarChar,20),
                                                new OracleParameter("pEMAIL",OracleType.VarChar,20),
                                                new OracleParameter("pDESCRIPTION",OracleType.VarChar,100),
                                                new OracleParameter("pLASTDATE",OracleType.DateTime,7)
                                                };
                oraParas[0].Value = objTable.ID;
                oraParas[1].Value = objTable.BRANCH;
                oraParas[2].Value = objTable.USERID;                
                oraParas[3].Value = objTable.USERNAME;
                oraParas[4].Value = objTable.PASSWORD;
                oraParas[5].Value = objTable.STATUS;
                oraParas[6].Value = objTable.PASSTIME;
                oraParas[7].Value = objTable.MOBILE;
                oraParas[8].Value = objTable.EMAIL;
                oraParas[9].Value = objTable.DESCRIPTION;
                oraParas[10].Value = objTable.LASTDATE;

                //string strSQL = " UPDATE USERS   set BRANCH = '" + objTable.BRANCH + "', USERID = '" + objTable.USERID + "', USERNAME = '" + objTable.USERNAME + "',PASSWORD = '" + objTable.PASSWORD + "', STATUS = '" + objTable.STATUS + "', PASSTIME = '" + objTable.PASSTIME + "',MOBILE = '" + objTable.MOBILE + "',EMAIL = '" + objTable.EMAIL + "',DESCRIPTION = '" + objTable.DESCRIPTION + "',LASTDATE = '" + objTable.LASTDATE + "' where ID='" + objTable.ID + "'";
                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {                       
                        return -1;                       
                    }
                    //DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                    DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_USERS_ACCESS.User_Update", oraParas);

                }
                catch (Exception)
                {
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
                return 1;
               
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                oraConn.Dispose();
            }
		}
        public int GetUpdate_chengePass(int iID, string pPassword, DateTime pPASTIME, DateTime pLASTDATE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }

            DataTable datTable = new DataTable();
            string strSQL = "Update USERS U set U.PASSWORD=:pPASSWORD,U.PASSTIME=:pPASTIME,U.LASTDATE=:pLASTDATE  where U.ID=:pID";
            OracleParameter[] oraParas ={ new OracleParameter("pID",OracleType.Number,10),
                                                new OracleParameter("pPASSWORD",OracleType.NVarChar,150),
                                        new OracleParameter("pPASTIME",OracleType.DateTime,7),
                                        new OracleParameter("pLASTDATE",OracleType.DateTime,7)};
            oraParas[0].Value = iID;
            oraParas[1].Value = pPassword;
            oraParas[2].Value = pPASTIME;
            oraParas[3].Value = pLASTDATE;

            try
            {
               DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, oraParas);                
            }
            catch 
            {
                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }
            return 1;
        }
        public int DeleteUSERS(int ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "Delete from Users u where lpad(u.UserID,8,'0')=lpad('" + ID + "',8,'0')";

            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);                
                return 1;
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
        // hàm lấy toàn bộ thông tin của user có trong bảng.
        public DataSet GetUSERS()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select  ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE  from Users u";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
               
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        // hàm lấy dữ liệu với điều kiện
        // với điều kiện so sánh ở 3 bảng
        public DataSet GetUSERSGROUP(string groupname)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select  ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE  from Users u";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);             

            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        public DataSet GetUSERS_PASS(string pUsername)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select  ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE  from Users u where Trim(u.username)='" + pUsername + "'";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataSet USERS_PASS(string pUSERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataSet _ds = new DataSet();
            string strSQL = "select  ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE  from Users u where Trim(u.userid)='" + pUSERID + "'";
            try
            {
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
                oraConn.Dispose();
                oraConn.Close();
                return _ds;
            }
            catch
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataSet Userid_UD(string pUserid)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE from Users u where Trim(u.userid) ='" + pUserid + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataTable GetUSERS_PASS1(string pUserID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE from Users u where u.userid='" + pUserID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        public DataSet GetUSERS_BRANCH()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select Distinct(b.sibs_bank_code) from Branch  b order by b.sibs_bank_code ASC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        public DataSet GetUSERS_BRANCHT()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select distinct(u.branch) from Users u order by u.Branch ASC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        public DataSet GetUSERSBR(string pBRANCH)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE from Users u where u.branch='" + pBRANCH + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);                
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        // ham lay rausername va password
        public DataSet GET_USER_PASS(string pUserid, string pPASSWORD)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select u.id,u.userid,u.username,u.password,u.status, u.passtime, u.lastdate from users u where upper(u.userid)= upper('" + pUserid + "') and u.password='" + pPASSWORD + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);                
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        // ham lay rausername va password
        public DataTable LOGSTS(string pUserid, string pPASSWORD)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select LOGSTS from users u where upper(u.userid)= upper('" + pUserid + "') and u.password='" + pPASSWORD + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }
        // ham lay rausername va password
        public int UPDATE_LOGSTS(string pUserid,string pLOGSTS)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }

            DataTable datTable = new DataTable();
            string strSQL = "UPDATE  users u set LOGSTS= '" + pLOGSTS + "' where upper(u.userid)= upper('" + pUserid + "') ";

            try
            {
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
        public DataSet GetUSERS_GROUP(int  pGROUPID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select u.userid,u.username,(select content from allcode where cdname='USERSTS' and u.status=allcode.cdval) as status from Groups g,user_groups gu,users u where g.groupid=gu.groupid and gu.userid=u.userid and g.groupid='" + pGROUPID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);      
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }


        public DataTable Get_Groupid(string pGROUP_name)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select GROUPID  from groups where Upper(trim(groupname))=Upper('" + pGROUP_name + "')";

            try
            {
                datTable = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                return datTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
        }

        public DataSet GetUSERSUPDATEPASS(string pUSERID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            DataTable datTable = new DataTable();
            string strSQL = "select ID,BRANCH,USERID,USERNAME,PASSWORD,STATUS,PASSTIME,MOBILE,EMAIL,DESCRIPTION,LASTDATE from users u where Trim(u.userid)='" + pUSERID.Trim() + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }

        }

        public DataTable GetRoll(string pUserID,string pGWtype)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }

                DataTable datTable = new DataTable();
                string strSQL = "select Distinct (select a.content from Allcode a where Trim(a.cdname)='Roll' and trim(a.cdval)= g.issupervisor) as issupervisor from Users u, groups g, User_Groups ug where u.userid = ug.userid and ug.groupid = g.groupid and trim(u.userid) = '" + pUserID + "'  and trim(g.gwtype) ='" + pGWtype + "'";
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
            finally
            {
                oraConn.Dispose();
            }
           
        }

        public bool CHECK_PASS_LENGTH(string sPass, out int iNumber)
        {
            iNumber = 0;
            try
            {
                oraConn = connect.Connect();
                DataSet ds = new DataSet();
                string strSQL = "select u.VALUE from sysvar u where u.VARNAME='LEN_OF_PASS'";

                if (string.IsNullOrEmpty(sPass))
                    return false;
                ds = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null); //objDataAccess.Excutenonquery(strSQL, "");
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
            catch 
            {
                //strError = ex.Message;
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
                string sPassUpper = "";

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
            catch
            {
                //strError = ex.Message;
                return false;
            }
        }



	}
	
	
}
