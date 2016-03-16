using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Xml.Linq;
using System.Windows.Forms;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using BIDVWEB.Business.Web;
using BIDVWEB.Business.Reports;

namespace BIDVWEB.Business.UserRight
{
    public class clsUserRight
    {
        public string strError = "";
        private string strBrHo = "";
        private bool bError = false;
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsConnection objConn = new clsConnection();
        private OracleCommand oraCommand = new OracleCommand();
        private clsCommon objCommon = new clsCommon();
        private OracleHelper objOracleHelper = new OracleHelper();
        private UserEncrypt ObjEncrypt = new UserEncrypt();
        private GROUPS_BO objGroups_BO = new GROUPS_BO();
        private USER_MSG_LOG_Info objUser_Msg_Log_Info = new USER_MSG_LOG_Info();
        private USER_MSG_LOG_BO objUser_Msg_Log_BO = new USER_MSG_LOG_BO();
        private USERS_BO objUsers_BO = new USERS_BO();
        private clsReport objReport = new clsReport();


        #region properties
        public string Error
        {
            get { return this.strError; }
            set { this.strError = value; }
        }
        
        #endregion
                

        // Them moi NSD//////////////////////////////////////////////
        // Muc dich:    Them moi NSD, Cap nhat NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUserID:    
        //              sUserName:  
        //              sPassword:   
        //              sEmail:
        //              sMobile:
        //              sDes:
        //              sBranch:
        //              iStatus:
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public bool AddUser(string sUserID, string sUserName, string sPassword, string sEmail, 
            string sMobile, string sDes, string sBranch, int iStatus, bool bAdd )
        {   
            try
            {
                string strSQL = "";
                bError = false;

                strError = "";
                if (bAdd == true)
                {
                    strSQL = "insert into Users(USERID,USERNAME,PASSWORD,EMAIL,MOBILE," +
                    " DESCRIPTION,BRANCH,STATUS) values('" + objCommon.g_sConvert2Valid(sUserID, false) +
                    "','" + objCommon.g_sConvert2Valid(sUserName, false) + "','"
                    + ObjEncrypt.Encrypt(objCommon.g_sConvert2Valid(sPassword, false)) + "','" +
                    objCommon.g_sConvert2Valid(sEmail, false) + "','" +
                    objCommon.g_sConvert2Valid(sMobile, false) + "','" +
                    objCommon.g_sConvert2Valid(sDes, false) + "','" +
                    objCommon.g_sConvert2Valid(sBranch, false) + "'," + iStatus + ")";
                }
                else
                {
                    if (sPassword != "")
                    {
                        strSQL = "update Users set USERNAME='" +
                        objCommon.g_sConvert2Valid(sUserName, false) + "'," + "PASSWORD='" +
                        ObjEncrypt.Encrypt(objCommon.g_sConvert2Valid(sPassword, false)) + "'," +
                        "EMAIL='" + objCommon.g_sConvert2Valid(sEmail, false) + "'," +
                        "MOBILE='" + objCommon.g_sConvert2Valid(sMobile, false) + "'," +
                        "DESCRIPTION='" + objCommon.g_sConvert2Valid(sDes, false) + "'," +
                        "BRANCH='" + objCommon.g_sConvert2Valid(sBranch, false) + "'," +
                        "STATUS=" + iStatus +
                        " WHERE USERID = '" + objCommon.g_sConvert2Valid(sUserID, false) + "'";
                    }
                    else
                    {
                        strSQL = "update Users set " + 
                        "USERNAME='" + objCommon.g_sConvert2Valid(sUserName, false) + "'," + 
                        "EMAIL='" + objCommon.g_sConvert2Valid(sEmail, false) + "'," + 
                        "MOBILE='" + objCommon.g_sConvert2Valid(sMobile, false) + "'," + 
                        "DESCRIPTION='" + objCommon.g_sConvert2Valid(sDes, false) + "'," + 
                        "BRANCH='" + objCommon.g_sConvert2Valid(sBranch, false) + "'," + 
                        "STATUS=" + iStatus + 
                        " WHERE USERID = '" + objCommon.g_sConvert2Valid(sUserID, false) + "'";
                    }                    
                }                                
                if (!objDataAccess.ExecuteSQL(strSQL))
                {
                    strError = objDataAccess.strError;                    
                    return false;
                }                
                bError= true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                bError = false;
            }
            return bError;
        }


        // Check USERID, USERNAME////////////////////////////////////
        // Muc dich:    Check USERID, USERNAME
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool CheckUserID_Name(string sUserID, string sUserName, bool bAdd)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                if (bAdd == true)
                {
                    //Kiem tra trung UserID                
                    strSql = "SELECT USERID,USERNAME FROM USERS WHERE upper(USERID) = upper('" +
                        objCommon.g_sConvert2Valid(sUserID, false) + "')";
                    dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                    if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                    {
                        strError = "Mã NSD đã có, nhập mã NSD khác!";
                        return false;                        
                    }
                    else
                    {
                        bError = true;
                    }
                }                
                //Kiem tra trung UserName
                if (dsData != null)
                {
                    dsData.Clear();
                }
                if (bAdd == false)
                {                    
                    strSql = "SELECT USERID,USERNAME FROM USERS WHERE upper(USERNAME) = upper('" + 
                         objCommon.g_sConvert2Valid(sUserName,false) + "')" +
                        " AND USERID<>'" + sUserID + "'";
                }
                else
                {
                    strSql = "SELECT USERID,USERNAME FROM USERS WHERE upper(USERNAME) = upper('" +
                        objCommon.g_sConvert2Valid(sUserName, false) + "')";
                }                  
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    strError = "Tên NSD đã có, nhập tên NSD khác!";
                    return false;                    
                }
                else
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        // Them moi nhom/////////////////////////////////////////////
        // Muc dich:    Them moi nhom
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sGroupName:    
        //              bSupervisor:  
        //              sGWType:   
        //              sDepartment:
        //              sDes:        
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public bool AddGroup(string sGroupName, int iSupervisor, string sGWType,
            string sDepartment, string sDes, int iGroupID)
        {
            try
            {
                string strSQL = "";

                strError = "";
                if (iGroupID == 0)
                {
                    strSQL = "insert into GROUPS(GROUPNAME,ISSUPERVISOR,GWTYPE,DEPARTMENT,DESCRIPTION) " +
                    " values('" + objCommon.g_sConvert2Valid(sGroupName, false) +
                    "'," + iSupervisor + ",'" + objCommon.g_sConvert2Valid(sGWType, false) + "','" +
                    objCommon.g_sConvert2Valid(sDepartment, false) + "','" +
                    objCommon.g_sConvert2Valid(sDes, false) + "')";
                }
                else
                {
                    strSQL = "Update GROUPS set GROUPNAME='" + 
                        objCommon.g_sConvert2Valid(sGroupName, false) + "',ISSUPERVISOR=" + 
                        iSupervisor + ",GWTYPE='" + objCommon.g_sConvert2Valid(sGWType, false) + 
                        "',DEPARTMENT = '" + objCommon.g_sConvert2Valid(sDepartment, false) + 
                        "',DESCRIPTION= '" + objCommon.g_sConvert2Valid(sDes, false) + "'" +
                        " WHERE GROUPID = " + iGroupID;
                }

                if (!objDataAccess.ExecuteSQL(strSQL))
                {
                    strError = objDataAccess.strError;
                    return false;
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                bError = false;
            }
            return bError;
        }


        // Check GROUPNAME////////////////////////////////////
        // Muc dich:    Check GROUPNAME
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool CheckGroupName(string sGroupName, int iGroupID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";                
                //Kiem tra trung groupName

                if (iGroupID>0)
                {
                    strSql = "SELECT GROUPID,GROUPNAME FROM GROUPS WHERE upper(GROUPNAME) = '" +
                        objCommon.g_sConvert2Valid(sGroupName.ToUpper(), false) + "'" +
                        " AND GROUPID<>" + iGroupID + "";
                }
                else
                {
                    strSql = "SELECT GROUPID,GROUPNAME FROM GROUPS WHERE upper(GROUPNAME) = '" +
                        objCommon.g_sConvert2Valid(sGroupName.ToUpper(), false) + "'";
                }
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    strError = "Tên nhóm NSD đã có, nhập tên nhóm khác!";
                    return false;
                }
                else
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        // Lay thong tin NSD/////////////////////////////////////////
        // Muc dich:    Lay thong tin NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUserID: Ma NSD
        // Dau ra:      Dataset chua thong tin user        
        /////////////////////////////////////////////////////////////        
        public DataSet GetUserByID(string sUserID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT * FROM USERS WHERE USERID ='" + sUserID + "'";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }   
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Lay thong tin nhom////////////////////////////////////////
        // Muc dich:    Lay thong tin nhom
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUserID: Ma nhom
        // Dau ra:      Dataset chua thong tin group
        /////////////////////////////////////////////////////////////        
        public DataSet GetGroupByID(int iGroupID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT * FROM GROUPS WHERE GROUPID =" + iGroupID + "";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Lay thong tin tat ca NSD//////////////////////////////////
        // Muc dich:    Lay thong tin tat ca NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      Dataset chua thong tin cac user        
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllUser()
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT * FROM USERS ORDER BY USERID ASC";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        // Lay thong tin tat ca NSD trong Group//////////////////////
        // Muc dich:    Lay thong tin tat ca NSD trong group
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     bGroup: = True: Danh sach NSD 
        // Dau ra:      Dataset chua thong tin cac user        
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllUserGroup(Int32 iGroupID, string sBranch, bool bGroup)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                                
                //Danh sach NSD chua thuoc nhom
                if (bGroup == false)
                {
                    if (CHECK_GROUP_QTRI(iGroupID) == 1)
                        strSql = "SELECT ID,BRANCH,USERID,USERNAME FROM USERS WHERE " +                        
                        " USERID NOT IN (SELECT USERID FROM USER_GROUPS WHERE GROUPID=" + iGroupID + ")" +
                        " ORDER BY USERID ASC";
                    else
                        strSql = "SELECT ID,BRANCH,USERID,USERNAME FROM USERS WHERE " +
                        " LPAD(TRIM(BRANCH),5,'0') LIKE '%" + sBranch.PadLeft(5, '0') + "%' AND " + 
                        " USERID NOT IN (SELECT USERID FROM USER_GROUPS WHERE GROUPID=" + iGroupID + ")" +
                        " ORDER BY ID DESC";                                  
                }
                //Danh sach NSD thuoc nhom
                else
                {
                    if (CHECK_GROUP_QTRI(iGroupID) == 1)                    
                        strSql = "SELECT A.ID,A.BRANCH,A.USERID,A.USERNAME " +
                        " FROM USERS A INNER JOIN USER_GROUPS B ON A.USERID=B.USERID " +
                        " WHERE B.GROUPID=" + iGroupID + 
                        " AND UPPER(A.USERID)<>'ADMIN' ORDER BY A.USERID ASC";
                    else
                        strSql = "SELECT A.ID,A.BRANCH,A.USERID,A.USERNAME " +
                        " FROM USERS A INNER JOIN USER_GROUPS B ON A.USERID=B.USERID " +
                        " WHERE B.GROUPID=" + iGroupID + " ORDER BY A.USERID ASC";
                }
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Lay thong tin tat ca Group trong NSD//////////////////////
        // Muc dich:    Lay thong tin tat ca Group trong NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      Dataset chua thong tin cac user        
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllGroupUser(string sUserID, string sBranch, bool bUser)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                
                //Danh sach nhom cua chi nhanh khong NSD
                if (bUser == false)
                {
                    //Kiem tra chi nhanh la H.O
                    strBrHo = GetBrHo();
                    if (sBranch.Trim().PadLeft(5, '0') == strBrHo)
                    {
                        strSql = "SELECT GROUPID,GROUPNAME,BRANCHID " +
                        " FROM GROUPS WHERE GROUPID NOT IN " +
                        " (SELECT GROUPID FROM USER_GROUPS WHERE USERID='" + sUserID + "')" +
                        " ORDER BY GROUPID ASC";
                    }
                    else
                    {
                        strSql = "SELECT A.GROUPID,A.GROUPNAME,A.BRANCHID " +
                        " FROM GROUPS A WHERE A.GROUPID NOT IN " +
                        " (SELECT GROUPID FROM USER_GROUPS WHERE USERID='" + sUserID + "')" +
                        " AND BRANCHID LIKE '%" + sBranch.Trim().PadLeft(5, '0') + "%'" + 
                        " ORDER BY A.GROUPID ASC";
                    }
                }
                //Danh sach nhom co NSD hien tai
                else
                {
                    strSql = "SELECT A.GROUPID,A.GROUPNAME,A.BRANCHID " +
                    " FROM GROUPS A INNER JOIN USER_GROUPS B ON A.GROUPID=B.GROUPID " +
                    " WHERE B.USERID='" + sUserID + "' AND DECODE(A.ISADMIN,1,1,0)<>1 " +
                    " ORDER BY A.GROUPID ASC"; 
                }
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        // Lay thong tin tat ca Group cua NSD////////////////////////
        // Muc dich:    Lay thong tin tat ca Group cua NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      Dataset chua thong tin cac user        
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllGroup_User(string sUserID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                
                strSql = "SELECT A.GROUPID,A.GROUPNAME,A.BRANCHID " +
                    " FROM GROUPS A INNER JOIN USER_GROUPS B ON A.GROUPID=B.GROUPID " +
                    " WHERE B.USERID='" + sUserID + "'" +
                    " ORDER BY A.GROUPID ASC";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Lay thong tin tat ca group////////////////////////////////
        // Muc dich:    Lay thong tin tat ca group
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      Dataset chua thong tin cac group                
        /////////////////////////////////////////////////////////////
        public DataSet GetAllGroup(string sGWType, string sBranch)        
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                string strBranchCode = "";

                //Lay ma chi nhanh theo NSD
                strBranchCode = objReport.GetBranchCodeOfUserName(SessionHelper.RetrieveWithDefault("username", "").ToString());
                //Kiem tra chi nhanh la H.O
                strBrHo = GetBrHo();
                if (strBranchCode.Trim().PadLeft(5, '0') == strBrHo)
                {
                    if (sBranch=="ALL")
                        strSql = "SELECT GROUPID,GROUPNAME,GWTYPE,DEPARTMENT,ISADMIN,BRANCHID " +
                        " FROM GROUPS WHERE GWTYPE='" + sGWType + "' ORDER BY GROUPID ASC";
                    else
                        strSql = "SELECT GROUPID,GROUPNAME,GWTYPE,DEPARTMENT,ISADMIN,BRANCHID " + 
                        " FROM GROUPS WHERE GWTYPE='" + sGWType +
                        "' AND LPAD(BRANCHID,5,'0') = '" + sBranch.PadLeft(5,'0') + "' ORDER BY GROUPID ASC";
                }
                else
                {
                    if (sBranch == "ALL")
                        strSql = "SELECT GROUPID,GROUPNAME,GWTYPE,DEPARTMENT,ISADMIN,BRANCHID " + 
                        " FROM GROUPS WHERE BRANCHID like '%" + 
                        strBranchCode.Trim().PadLeft(5, '0') + "%'" +
                        " AND GWTYPE='" + sGWType + "' ORDER BY GROUPID ASC";
                    else
                        strSql = "SELECT GROUPID,GROUPNAME,GWTYPE,DEPARTMENT,ISADMIN,BRANCHID " +
                        " FROM GROUPS WHERE BRANCHID like '%" +
                        strBranchCode.Trim().PadLeft(5, '0') + "%'" +
                        " AND GWTYPE='" + sGWType +
                        "' AND LPAD(BRANCHID,5,'0') = '" + sBranch.PadLeft(5, '0') + 
                        "' ORDER BY GROUPID ASC";
                }                
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)                
                    return dsData;                
                else                
                    return null;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Add USER to GROUP ////////////////////////////////////////
        // Muc dich:    Add USER to GROUP
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool AddUserToGroup(int iGroup, string sUserID)
        {
            try
            {
                strError = "";                
                string strSql = "";
                strSql = "INSERT INTO USER_GROUPS(GROUPID,USERID) VALUES(" + 
                    iGroup + ",'" + objCommon.g_sConvert2Valid(sUserID,false) + "')";
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }                
                bError = true;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError= false;
            }
            return bError;
        }

        // Xoa phan quyen bao cao////////////////////////////////////
        // Muc dich:    Xoa phan quyen bao cao
        // Ngay tao:    07/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iGroup: Ma nhom
        //              iReportID: Ma bao cao
        // Dau ra:      Xoa thanh cong
        /////////////////////////////////////////////////////////////        
        public bool DelGrantReport(int iGroup, int iReportID)
        {
            try
            {
                strError = "";
                string strSql = "";
                strSql = "DELETE FROM GROUP_REPORT WHERE GROUPID=" +
                    iGroup + " AND ID_REPORT =" + iReportID + "";
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }

        // Xoa phan quyen chuc nang//////////////////////////////////
        // Muc dich:    Xoa phan quyen chuc nang
        // Ngay tao:    07/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iGroup: Ma nhom
        //              MENUID: MenuID
        // Dau ra:      Xoa thanh cong
        /////////////////////////////////////////////////////////////        
        public bool DelGrantMenu(int iGroup, string sReportID)
        {
            try
            {
                strError = "";                
                string strSql = "";
                strSql = "DELETE FROM GROUP_MENU WHERE GROUPID=" +
                    iGroup + " AND MENUID ='" + sReportID + "'";
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }

        // DEL USER to GROUP ////////////////////////////////////////
        // Muc dich:    DEL USER to GROUP
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool DelUserToGroup(int iGroup, string sUserID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "DELETE FROM USER_GROUPS WHERE GROUPID=" +
                    iGroup + " AND USERID ='" + objCommon.g_sConvert2Valid(sUserID, false) + "'";

                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }                
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }


        // Lay thong tin tat ca Menu cho USER////////////////////////
        // Muc dich:    Lay thong tin tat ca Menu cho User
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllMenuForUser(string sUserID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT A.*, B.PERMISSION FROM MENU A LEFT OUTER JOIN " + 
                    "(SELECT * FROM USER_FUNCTION WHERE USERID = '" + sUserID +"') B " +
                    " ON A.MENUID = B.MENUID WHERE A.KEY = '1'";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Lay thong tin tat ca Menu cho GROUP///////////////////////
        // Muc dich:    Lay thong tin tat ca Menu cho GROUP
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllMenuForGroup(Int32 iGroupID, string sBranch, string sGWType)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                
                //Kiem tra chi nhanh la H.O
                strBrHo = GetBrHo();
                if (sBranch.Trim().PadLeft(5, '0') == strBrHo)
                {
                    //Kiem tra nhom la admin khong hien thi chuc nang phan 
                    //quyen cho nhom
                    if (objGroups_BO.CHECK_GROUP_ADMIN(iGroupID.ToString()) == 1)
                    {
                        strSql = "SELECT A.*, B.ISINQUIRY, B.ISDELETE,B.ISINSERT,B.ISEDIT " +
                            " FROM MENU A LEFT OUTER JOIN " +
                            "(SELECT * FROM GROUP_MENU WHERE GROUPID = " + iGroupID + ") B " +
                            " ON A.MENUID = B.MENUID WHERE A.KEY = '1' AND A.ENABLE=1 " +
                            " AND A.GWTYPE = '" + sGWType + "' " + 
                            " AND DECODE(ISADMIN,2,2,0)<>2 ORDER BY A.MENUID ASC";
                    }
                    else
                    {
                        strSql = "SELECT A.*, B.ISINQUIRY, B.ISDELETE,B.ISINSERT,B.ISEDIT " +
                            " FROM MENU A LEFT OUTER JOIN " +
                            "(SELECT * FROM GROUP_MENU WHERE GROUPID = " + iGroupID + ") B " +
                            " ON A.MENUID = B.MENUID WHERE A.KEY = '1' AND A.ENABLE=1 " +
                            " AND A.GWTYPE = '" + sGWType + "' " + 
                            " ORDER BY A.MENUID ASC";
                    }
                }
                else
                {
                    strSql = "SELECT A.*, B.ISINQUIRY, B.ISDELETE,B.ISINSERT,B.ISEDIT " +
                        " FROM MENU A LEFT OUTER JOIN " +
                        "(SELECT * FROM GROUP_MENU WHERE GROUPID = " + iGroupID + ") B " +
                        " ON A.MENUID = B.MENUID WHERE A.KEY = '1' AND A.ENABLE=1 " +
                        " AND A.GWTYPE = '" + sGWType + "' " + 
                        " AND DECODE(ISADMIN,1,1,0)<>1 ORDER BY A.MENUID ASC";
                }
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Lay thong tin tat ca Menu cho GROUP///////////////////////
        // Muc dich:    Lay thong tin tat ca Menu cho GROUP
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public DataSet GetGroup_Report(int iGroupID, string sGwType)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT A.ID_REPORT, A.REPORTNAME, " +
                    "(A.REPORTNAME || ' - ' || A.TITLE) AS TITLE," +
                    "A.DESCRIPTION,A.ISSHOW,A.IORDER,A.TYPE,A.MENUID, " +
                    "A.URL,A.URLICON,A.SPNAME,A.SPNAME1, " +
                    " B.ISINQUIRY FROM LIST_REPORT A LEFT OUTER JOIN " +
                    "(SELECT * FROM GROUP_REPORT WHERE GROUPID = " + iGroupID + ") B " +
                    " ON A.ID_REPORT = B.ID_REPORT WHERE A.GWTYPE='" + sGwType + 
                    "' AND A.ISSHOW=1 ORDER BY A.ID_REPORT ASC";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    return dsData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        // Check Menu da dc cap quyen////////////////////////////////
        // Muc dich:    Check Menu da dc cap quyen
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool CheckPermissionUser(string sUserID, string sMenuID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT * FROM USER_FUNCTION WHERE USERID = '" + sUserID +
                     "' AND MENUID='" + sMenuID + "'";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count>0)
                {
                    bError = false;
                }
                else
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        // Check quyen cua user tren form////////////////////////////
        // Muc dich:    Check quyen cua user tren form
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool CheckPerForm(string sMenuID, 
            out int iInquiry, out int iDelete, out int iInsert, out int iEdit)
        {
            try
            {                
                DataSet dsData = new DataSet();
                DataRow dr;
                string sUserID = "";
                string strSql = "";
                strError = "";

                //sUserID = GetUserIDByUserName();                
                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                
                strSql ="SELECT sum(ISINQUIRY) as ISINQUIRY, sum(ISDELETE) as ISDELETE," + 
                "sum(ISINSERT) as ISINSERT, sum(ISEDIT) as ISEDIT " +
                "FROM GROUP_MENU WHERE MENUID='" + sMenuID + "' AND " +
                "GROUPID IN (SELECT GROUPID FROM USER_GROUPS WHERE USERID = '" + sUserID + "')";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    dr =dsData.Tables[0].Rows[0];
                    if (dr["ISINQUIRY"].ToString() != null && dr["ISINQUIRY"].ToString() != "") 
                    {
                        iInquiry = Convert.ToInt16(dr["ISINQUIRY"].ToString());
                    }
                    else { iInquiry = 0; }
                    if (dr["ISDELETE"] != null && dr["ISDELETE"].ToString() != "")
                    {
                        iDelete = Convert.ToInt16(dr["ISDELETE"].ToString());
                    }
                    else { iDelete = 0; }
                    if (dr["ISINSERT"] != null && dr["ISINSERT"].ToString() != "")
                    {
                        iInsert = Convert.ToInt16(dr["ISINSERT"].ToString());
                    }
                    else { iInsert = 0; }
                    if (dr["ISEDIT"] != null && dr["ISEDIT"].ToString() != "")
                    {
                        iEdit = Convert.ToInt16(dr["ISEDIT"].ToString());
                    }
                    else { iEdit = 0; }
                    bError = true;
                }
                else
                {
                    iInquiry = 0;
                    iDelete = 0;
                    iInsert = 0;
                    iEdit = 0;
                    bError = false;
                }
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                iInquiry = 0;
                iDelete = 0;
                iInsert = 0;
                iEdit = 0;
                return false;
            }
        }


        // Check Menu da dc cap quyen////////////////////////////////
        // Muc dich:    Check Menu da dc cap quyen
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool CheckPermissionGroup(int iGroupID, string sMenuID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";               
               
                strSql = "SELECT * FROM GROUP_MENU WHERE GROUPID = " + iGroupID +
                 " AND MENUID='" + sMenuID + "'";               
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    bError = false;
                }
                else
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        // Check Report da dc cap quyen////////////////////////////////
        // Muc dich:    Check Report da dc cap quyen
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool CheckPermissionReport(int iGroupID, int iReportID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";

                strSql = "SELECT * FROM GROUP_REPORT WHERE GROUPID = " + iGroupID +
                 " AND ID_REPORT=" + iReportID + "";
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    bError = false;
                }
                else
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        // Them quyen cho NSD ////////////////////////////////////////
        // Muc dich:    Them quyen cho NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool AddPermissionUser(string sUserID, string sMenuID, string sPer,bool bAdd)
        {
            try
            {
                strError = "";                
                string strSql = "";

                if (bAdd == true)
                {
                    strSql = "INSERT INTO USER_FUNCTION(USERID,MENUID,PERMISSION) VALUES('" +
                    sUserID + "','" + sMenuID + "','" + sPer + "')";
                }
                else
                {
                    strSql = "UPDATE USER_FUNCTION SET PERMISSION='" + sPer +
                    "' WHERE USERID='" + sUserID + "' AND MENUID='" + sMenuID + "'";
                }
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }                
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }


        // Them quyen cho NSD ////////////////////////////////////////
        // Muc dich:    Them quyen cho NSD
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool AddPermissionGroup(int iGroupID, string sMenuID, int iField1,
                int iField2, int iField3, int iField4, bool bAdd)
        {
            try
            {
                strError = "";
                string strSql = "";

                if (bAdd == true)
                {                    
                    strSql = "INSERT INTO GROUP_MENU(GROUPID,MENUID,ISINQUIRY,ISDELETE,ISINSERT,ISEDIT)" +
                        " VALUES(" + iGroupID + ",'" + sMenuID + "'," + iField1 + 
                        "," + iField2 + "," + iField3 + "," + iField4+ ")";                    
                }
                else
                {
                    strSql = "UPDATE GROUP_MENU SET ISINQUIRY=" + iField1 + ", ISDELETE=" + iField2 +
                        ", ISINSERT=" + iField3 + ", ISEDIT  =" + iField4 + 
                    " WHERE GROUPID=" + iGroupID + " AND MENUID='" + sMenuID + "'";
                }
                if (!objDataAccess.ExecuteSQL(strSql))
                {                    
                    strError = objDataAccess.strError;
                    return false;
                }
                //Ko cho xoa quyen phan quyen chuc nang cua nhom admin
                if ((objGroups_BO.CHECK_GROUP_ADMIN(iGroupID.ToString()) == 1) && sMenuID == "1304")
                {
                    strSql = "UPDATE GROUP_MENU SET ISINQUIRY=1,ISDELETE=1" +
                        ",ISINSERT=1, ISEDIT  =1 WHERE GROUPID=" + iGroupID + " AND MENUID='1304'";
                    if (!objDataAccess.ExecuteSQL(strSql))
                    {
                        strError = objDataAccess.strError;
                        return false;
                    }
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }


        // Them quyen bao cao cho Group//////////////////////////////
        // Muc dich:    Them quyen bao cao cho Group
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      
        /////////////////////////////////////////////////////////////        
        public bool AddPermissionReport(int iGroupID, int iReportID, int iField1, bool bAdd)
        {
            try
            {
                strError = "";
                string strSql = "";

                if (bAdd == true)
                {
                    strSql = "INSERT INTO GROUP_REPORT(GROUPID,ID_REPORT,ISINQUIRY)" +
                        " VALUES(" + iGroupID + "," + iReportID + "," + iField1 + ")";
                }
                else
                {
                    strSql = "UPDATE GROUP_REPORT SET ISINQUIRY=" + iField1 +
                    " WHERE GROUPID=" + iGroupID + " AND ID_REPORT=" + iReportID + "";
                }
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }

        // Lay UserID tu UserName ///////////////////////////////////
        // Muc dich:    Ghi log cua NSD
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     tDate:
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public string GetUserIDByUserName()
        {
            try
            {
                string strSQL = "";
                string sUserName = "";
                string sUID = "";
                DataSet ds = new DataSet();
                DataRow dr;
                strError = "";

                //lay UserID qua UserName
                sUserName = SessionHelper.RetrieveWithDefault("username", "").ToString();
                strSQL = "SELECT USERID FROM USERS WHERE USERNAME = '" + sUserName + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    sUID = dr["USERID"].ToString();
                }
                return sUID;
            }
            catch (Exception ex)
            {                
                strError = ex.Message;
                return "";
            }
        }

        // Ghi log /////////////////////////////////////////////////
        // Muc dich:    Ghi log cua NSD
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     tDate:
        //              sUserID:    
        //              sContent:  
        //              iStatus:   
        //              sWorker:
        //              sTableAccess:
        //              sOldValue:
        //              sNewValue:        
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public bool SaveUserLog(DateTime  tDate, string sUserID, string sContent, int iStatus,
            string sWorker, string sTableAccess, string sOldValue, string sNewValue)
        {
            try
            {
                //string strSQL = "";
                string sUserName = "";
                string sUID = "";
                DataSet ds = new DataSet();                
                strError = "";

                //lay UserID qua session
                sUserName = SessionHelper.RetrieveWithDefault("username", "").ToString();
                sUID = sUserName;
                //Cap nhat ngay log on cuoi cung
                objUser_Msg_Log_Info.CONTENT = sContent;
                objUser_Msg_Log_Info.LOG_DATE = tDate;
                //objUser_Msg_Log_Info.LOGID = "";
                objUser_Msg_Log_Info.NEW_VALUE = sNewValue;
                objUser_Msg_Log_Info.OLD_VALUE = sOldValue;
                objUser_Msg_Log_Info.STATUS = iStatus;
                objUser_Msg_Log_Info.TABLE_ACCESS = sTableAccess;
                objUser_Msg_Log_Info.USERID = sUID;
                objUser_Msg_Log_Info.WORKED = sWorker;
                if (objUser_Msg_Log_BO.AddUSER_MSG_LOG(objUser_Msg_Log_Info) < 0)
                {                    
                    return false;
                }
                //Cap nhat thoi gian log he thong cuoi cung vao bang users
                if (objUsers_BO.UPDATE_LASTDATE(sUID, DateTime.Now) < 0)
                {
                    return false;
                }

                //strSQL = "insert into USER_MSG_LOG(LOG_DATE,USERID,CONTENT,STATUS,WORKED,TABLE_ACCESS," +
                //" OLD_VALUE,NEW_VALUE) values('" +
                ////objCommon.g_ConvertDateToString(tDate, false) + "','" + 
                //Convert.ToDateTime(tDate) + "','" + 
                //sUID + "','" +
                //objCommon.g_sConvert2Valid(sContent, false) + "'," + iStatus + ",'" +
                //objCommon.g_sConvert2Valid(sWorker, false) + "','" +
                //objCommon.g_sConvert2Valid(sTableAccess, false) + "','" +
                //objCommon.g_sConvert2Valid(sOldValue, false) + "','" +
                //objCommon.g_sConvert2Valid(sNewValue, false) + "')";
                ////Cap nhat ngay log on cuoi cung
                //if (!objDataAccess.ExecuteSQL(strSQL))
                //{
                //    strError = objDataAccess.strError;
                //    return false;
                //}
                //strSQL = " Update USERS set LASTDATE =To_Date('" + DateTime.Now +
                //    "','MM/DD/YYYY') WHERE USERID = '" + sUserName.ToString().Trim() + "'";
                //if (!objDataAccess.ExecuteSQL(strSQL))
                //{
                //    strError = objDataAccess.strError;                    
                //    return false;
                //}                
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;                
                bError = false;
            }
            return bError;
        }


        // Xoa mot group/////////////////////////////////////////////
        // Muc dich:    Xoa mot group
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iID: Ma nhom        
        // Dau ra:      Xoa thanh cong
        /////////////////////////////////////////////////////////////        
        public bool DeleteGroup(int iID)
        {
            try
            {
                strError = "";
                string strSql = "";
                strSql = "DELETE FROM GROUPS WHERE GROUPID = " + iID + "";
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }


        // Xoa mot user//////////////////////////////////////////////
        // Muc dich:    Xoa mot user
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iID: Ma nhom        
        // Dau ra:      Xoa thanh cong
        /////////////////////////////////////////////////////////////        
        public bool DeleteUser(string sID)
        {
            try
            {
                strError = "";
                string strSql = "";
                DataSet ds = new DataSet();

                strSql = "select * from USER_PASS WHERE USERID = '" + sID + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    strSql = "DELETE from USER_PASS WHERE USERID = '" + sID + "'";
                    if (!objDataAccess.ExecuteSQL(strSql))
                    {
                        strError = objDataAccess.strError;
                        return false;
                    }
                    ds.Clear();
                }

                strSql = "select * from USER_MSG_LOG WHERE USERID = '" + sID + "'";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    strSql = " DELETE from USER_MSG_LOG WHERE USERID = '" + sID + "'";
                    if (!objDataAccess.ExecuteSQL(strSql))
                    {
                        strError = objDataAccess.strError;
                        return false;
                    }
                }
                strSql = " DELETE from USERS WHERE USERID = '" + sID + "'";
                if (!objDataAccess.ExecuteSQL(strSql))
                {
                    strError = objDataAccess.strError;
                    return false;
                }
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }

        //Ham kiem tra Group quan tri//////////////////////////////////
        //Mo ta:        Ham kiem tra Group quan tri
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Khong cho xoa
        ///////////////////////////////////////////////////////////////
        public int CHECK_GROUP_QTRI(Int32 iGroupID)
        {
            DataSet ds = new DataSet();
            string strSQL = "select GROUPID from GROUPS u where u.GROUPID=" + iGroupID +
                " AND u.ISADMIN = 1";
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

        // Check group, user trong bang User_Group///////////////////
        // Muc dich:    Check group, user trong bang User_Group
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iID: Ma nhom        
        // Dau ra:      Xoa thanh cong
        /////////////////////////////////////////////////////////////        
        public bool CheckUser_Group(string sID, bool bGroup)
        {
            try
            {
                strError = "";
                string strSql = "";
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                //Kiem tra bang USER_GROUPS
                if (bGroup == true)
                {
                    strSql = "Select * FROM USER_GROUPS WHERE GROUPID = " + Convert.ToInt16(sID) + "";
                }
                else
                {
                    strSql = "Select * FROM USER_GROUPS WHERE USERID = '" + sID + "'";
                }                
                ds = objDataAccess.dsGetDataSourceByStr(strSql,"");
                if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                {                    
                    strError = "";
                    return false;                 
                }                
                if (bGroup == true)
                {
                    //Kiem tra bang GROUP_MENU
                    strSql = "Select * FROM GROUP_MENU WHERE GROUPID = " + Convert.ToInt16(sID) + "";
                    ds1 = objDataAccess.dsGetDataSourceByStr(strSql, "");
                    if ((ds1 != null) && (ds1.Tables[0].Rows.Count > 0))
                    {
                        strError = "Nhóm đã được phân quyền chức năng";
                        return false;
                    }
                    //Kiem tra bang GROUP_REPORT
                    strSql = "Select * FROM GROUP_REPORT WHERE GROUPID = " + Convert.ToInt16(sID) + "";
                    ds2 = objDataAccess.dsGetDataSourceByStr(strSql, "");
                    if ((ds2 != null) && (ds2.Tables[0].Rows.Count > 0))
                    {
                        strError = "Nhóm đã được phân quyền báo cáo";
                        return false;
                    }
                }
                else
                {
                    //Kiem tra bang USER_MSG_LOG
                    //    strSql = "Select * FROM USER_MSG_LOG WHERE USERID = '" + sID + "'";
                    //    ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                    //    if (ds != null)
                    //    {
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {
                    //            strError = "";
                    //            return false;
                    //        }
                    //    }
                }                
                bError = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }

        public string GetBrHo()
        {
            DataSet ds = new DataSet();
            DataRow dr;
            string strBrHo;
            string strSQL = "SELECT * FROM ALLCODE WHERE CDNAME='BR_HO'";

            try
            {
                ds = objDataAccess.dsGetDataSourceByStr(strSQL, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    strBrHo=dr["CDVAL"].ToString(); 
                    return strBrHo.PadLeft(5,'0');
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
