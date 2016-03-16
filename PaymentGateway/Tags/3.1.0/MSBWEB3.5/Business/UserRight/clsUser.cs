using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using BIDVWEB.Comm;
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.Web;

namespace BIDVWEB.Business.UserRight
{
    public class clsUser
    {
        clsDatatAccess objData = new clsDatatAccess();
        public string strError="";

        //////////////////////////////////////////////////////////////
        //Muc dich: Lay ma NSD, ten NSD, chi nhanh NSD hien tai
        //Ngay tao: 08/08/2008
        //////////////////////////////////////////////////////////////        
        public string GetUID_UName_Branch()
        {
            string strReturn = "";
            string sUserID = "";

            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                DataRow dr;

                sUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                strSql = "SELECT A.*, B.BRAN_NAME FROM USERS A INNER JOIN BRANCH B " + 
                    "ON A.BRANCH = B.SIBS_BANK_CODE WHERE A.USERID='" + sUserID + "'";
                ds = objData.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    strReturn = "NSD:" + dr["USERID"] + " - " + dr["USERNAME"] +
                        " - Chi nhánh:" + dr["BRAN_NAME"];
                }
                else
                {
                    return strReturn;
                }               
                return strReturn;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return strReturn;                
            }
        }

        // Lay chi nhanh cua user cho bao cao ///////////////////////
        // Muc dich:    Lay chi nhanh cua user cho bao cao
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUser: UserID
        //              bAdmin:=True neu la Admin, False nguoc lai
        // Dau ra:      Dataset
        /////////////////////////////////////////////////////////////
        public DataSet GetBranchByUser(string sUser, bool bAdmin)
        {
            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                if(bAdmin==true)
                    strSql = "SELECT SIBS_BANK_CODE FROM BRANCH ";
                else
                    strSql = "SELECT BRANCH FROM USERS WHERE USERNAME='" +
                        sUser + "'";
                ds = objData.dsGetDataSourceByStr(strSql, "");
                if (ds != null)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {                
                strError = ex.Message;
                return null;
            }
        }

        // Lay chi nhanh cua user_id cho bao cao ////////////////////
        // Muc dich:    Lay chi nhanh cua user_id cho bao cao
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUser: UserID
        // Dau ra:      Dataset
        /////////////////////////////////////////////////////////////
        public DataSet GetBranchByUserID(string sUserId)
        {
            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                strSql = "SELECT BRANCH FROM USERS WHERE USERID='" +
                        sUserId + "'";
                ds = objData.dsGetDataSourceByStr(strSql, "");
                if (ds != null)
                    return ds;
                else
                    return null;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        // Lay chi nhanh cua user_id cho bao cao ////////////////////
        // Muc dich:    Lay chi nhanh cua user_id cho bao cao
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUser: UserID
        // Dau ra:      Ten NSD
        /////////////////////////////////////////////////////////////
        public string GetUserNameByUserID(string sUserId)
        {
            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                strSql = "SELECT username FROM USERS WHERE USERID='" +
                        sUserId + "'";
                ds = objData.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count>0)
                    return ds.Tables[0].Rows[0]["username"].ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

    }
}
