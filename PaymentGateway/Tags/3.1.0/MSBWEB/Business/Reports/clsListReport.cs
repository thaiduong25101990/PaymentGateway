using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.Web;

namespace BIDVWEB.Business.Reports
{
    public class clsListReport
    {
        public string strError = "";
        private bool bError = false;
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsConnection objConn = new clsConnection();
        private OracleCommand oraCommand = new OracleCommand();
        private clsCommon objCommon = new clsCommon();
        private clsUserRight objUser = new clsUserRight();

        #region properties
        public string Error
        {
            get { return this.strError; }
            set { this.strError = value; }
        }

        #endregion

        // Them moi mot bao cao//////////////////////////////////////
        // Muc dich:    Them moi mot bao cao
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sTitle:    
        //              sFunction1,sFunction2:  
        //              sFunction3,sFunction4:   
        //              sUser1, sUser2:
        //              sUser3, sUser4:        
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public bool AddListReport(string sTitle, string sFunction1,
            string sFunction2, string sFunction3, string sFunction4, string sUser1,
            string sUser2, string sUser3, string sUser4)
        {
            try
            {
                string strSQL = "";

                strError = "";
                strSQL = "insert into LIST_REPORT(TITLE,FUNCTION1,FUNCTION2," +
                    "FUNCTION3, FUNCTION4,USER1,USER2,USER3,USER4) " +
                    " values('" + objCommon.g_sConvert2Valid(sTitle, false) +
                    objCommon.g_sConvert2Valid(sFunction1, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction2, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction3, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction4, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser1, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser2, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser3, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser4, false) + "')";
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


        // Lay thong tin mot bao cao/////////////////////////////////
        // Muc dich:    Lay thong tin mot bao cao
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iReportID: Ma nhom
        // Dau ra:      Dataset chua thong tin bao cao
        /////////////////////////////////////////////////////////////        
        public DataSet GetReportByID(int iID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT A.*, B.TITLE TITLE1 FROM LIST_REPORT_DETAIL A LEFT OUTER JOIN " +
                    "LIST_REPORT B ON A.ID_REPORT = B.ID_REPORT WHERE A.ID =" + iID + "";
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


        // Cap nhat bao cao//////////////////////////////////////////
        // Muc dich:    Cap nhat bao cao
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sTitle:    
        //              sFunction1,sFunction2:  
        //              sFunction3,sFunction4:   
        //              sUser1, sUser2:
        //              sUser3, sUser4:        
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public bool UpdateListReport(int iID, int iReportID, string sBankCode, string sTitle, 
            string sFunction1,string sFunction2, string sFunction3, string sFunction4, 
            string sUser1,string sUser2, string sUser3, string sUser4,bool bAdd)
        {
            try
            {
                string strSQL = "";

                strError = "";
                if (bAdd == true)
                {
                    strSQL = "INSERT INTO LIST_REPORT_DETAIL(ID_REPORT,SIBS_BANK_CODE,TITLE," +
                    "FUNCTION1,FUNCTION2,FUNCTION3,FUNCTION4," +
                    "USER1,USER2,USER3,USER4) VALUES(" + iReportID + ",'" +
                    objCommon.g_sConvert2Valid(sBankCode, false) + "','" +
                    objCommon.g_sConvert2Valid(sTitle, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction1, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction2, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction3, false) + "','" +
                    objCommon.g_sConvert2Valid(sFunction4, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser1, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser2, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser3, false) + "','" +
                    objCommon.g_sConvert2Valid(sUser4, false) + "')";                        
                }
                else
                {
                    strSQL = "Update LIST_REPORT_DETAIL SET TITLE= '" + objCommon.g_sConvert2Valid(sTitle, false) +
                    "',FUNCTION1='" + objCommon.g_sConvert2Valid(sFunction1, false) +
                    "',FUNCTION2='" + objCommon.g_sConvert2Valid(sFunction2, false) +
                    "',FUNCTION3='" + objCommon.g_sConvert2Valid(sFunction3, false) +
                    "',FUNCTION4='" + objCommon.g_sConvert2Valid(sFunction4, false) +
                    "',USER1='" + objCommon.g_sConvert2Valid(sUser1, false) +
                    "',USER2='" + objCommon.g_sConvert2Valid(sUser2, false) +
                    "',USER3='" + objCommon.g_sConvert2Valid(sUser3, false) +
                    "',USER4='" + objCommon.g_sConvert2Valid(sUser4, false) +
                    "' WHERE ID = " + iID;
                }
                if (!objDataAccess.ExecuteSQL(strSQL))
                {
                    strError = objDataAccess.strError;
                    return  false;
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

        // Lay danh sach bao cao/////////////////////////////////////
        // Muc dich:    Lay danh sach bao cao
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     iReportID: Ma nhom
        // Dau ra:      Dataset chua thong tin bao cao
        /////////////////////////////////////////////////////////////        
        public DataSet GetListReport(int iType, out string strGWType)
        {
            strGWType="";
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                DataRow dr;
                string strSql = "";
                string strUserID = "";
                string strGroupID = "";
                string strBranch = "";

                //strUserID = objUser.GetUserIDByUserName();
                strUserID = SessionHelper.RetrieveWithDefault("username", "").ToString();
                dsData = objUser.GetAllGroup_User(strUserID);
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dsData.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsData.Tables[0].Rows[i];
                        if (i==0)
                        {
                            strGroupID = strGroupID + dr["GROUPID"];
                        }
                        else
                        {
                            strGroupID = strGroupID + "," + dr["GROUPID"];
                        }
                    }                        
                }

                strSql = "SELECT ID_REPORT,REPORTNAME,(REPORTNAME || ' - ' || TITLE) AS TITLE1 ," +
                "TITLE,URL,URLICON, GWTYPE " +
                " FROM LIST_REPORT WHERE LIST_REPORT.TYPE =" + iType + " AND ISSHOW = 1 " +
                " AND LIST_REPORT.ID_REPORT IN (SELECT DISTINCT ID_REPORT FROM GROUP_REPORT " +
                " WHERE ISINQUIRY=1 AND GROUPID IN (" + strGroupID + "))" + 
                " ORDER BY ID_REPORT";

                dsData.Clear();
                dsData = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        strGWType = dsData.Tables[0].Rows[0]["GWTYPE"].ToString();
                    }
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


        // Lay danh sach bao cao theo chi nhanh//////////////////////
        // Muc dich:    Lay danh sach bao cao theo chi nhanh
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sBankCode: Ma chi nhanh
        // Dau ra:      Dataset chua thong tin bao cao
        /////////////////////////////////////////////////////////////        
        public DataSet GetListReportBrach(string sBankCode)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                strSql = "SELECT B.ID,B.SIBS_BANK_CODE,A.ID_REPORT,B.TITLE,B.FUNCTION1,B.FUNCTION2, " +
                    "B.FUNCTION3,B.FUNCTION4,B.USER1,B.USER2,B.USER3,B.USER4, A.REPORTNAME " +
                    " FROM LIST_REPORT A LEFT OUTER JOIN " +
                    "(SELECT * FROM LIST_REPORT_DETAIL WHERE SIBS_BANK_CODE=" + sBankCode + ") B " +
                    " ON A.ID_REPORT = B.ID_REPORT " + 
                    "WHERE A.ISSHOW = 1 ORDER BY A.ID_REPORT ";                
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

    }
}
