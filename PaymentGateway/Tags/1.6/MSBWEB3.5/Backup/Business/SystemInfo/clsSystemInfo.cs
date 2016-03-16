using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;


namespace BIDVWEB.Business.SystemInfo
{
    public class clsSystemInfo
    {
        public string strError = "";
        private bool bError = false;
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsConnection objConn = new clsConnection();        
        private OracleCommand oraCommand = new OracleCommand();
        private clsCommon objCommon = new clsCommon();

        #region properties
        public string Error
        {
            get { return this.strError; }
            set { this.strError = value; }
        }

        #endregion


        // Them moi parameter////////////////////////////////////////
        // Muc dich:    Them moi parameter
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sSIBSBankCode:    
        //              iTime:          
        //              sDes:        
        // Dau ra:      Cap nhat thanh cong
        /////////////////////////////////////////////////////////////
        public bool AddParameter(string sSIBSBankCode, int iReportID, string sTime, string sDes,int iID, string strRN)
        {
            try
            {
                string strSql = "";

                strError = "";
                if (iID <= 0)
                {
                    strSql = "insert into SYSINFO(SIBS_BANK_CODE,TIME,DESCRIPTION,ID_REPORT,REPORTNAME) " +
                        " values('" + objCommon.g_sConvert2Valid(sSIBSBankCode, false) +
                        "','" + objCommon.g_sConvert2Valid(sTime, false) + "','" +
                        objCommon.g_sConvert2Valid(sDes, false) + "'," + iReportID + ",'" + strRN + "')";
                    
                }
                else
                {
                    strSql = "Update SYSINFO set TIME='" + objCommon.g_sConvert2Valid(sTime, false) +
                        "',DESCRIPTION='" + objCommon.g_sConvert2Valid(sDes, false) +
                        "',SIBS_BANK_CODE='" + objCommon.g_sConvert2Valid(sSIBSBankCode, false) +
                        "',ID_REPORT=" + iReportID + ", REPORTNAME= '" + strRN + "' WHERE ID=" + iID; 
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


        // Ham check tham so thoi gian tao bao cao theo chi nhanh///
        // Muc dich:    Ham check tham so thoi gian tao bao cao theo
        //              chi nhanh
        // Ngay tao:    06/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sSIBSBankCode:    
        // Dau ra:      True: khong ton tai, false: ton tai
        /////////////////////////////////////////////////////////////
        public bool CheckBranhID_ReportID(string sSIBSBankCode, int iReportID, Int32 iID,out Int32 iIDEdit)
        {
            iIDEdit = 0;
            try
            {
                string strSQL = "";
                DataSet ds = new DataSet();

                strError = "";
                if (iID > 0)
                {
                    strSQL = "SELECT ID FROM SYSINFO WHERE SIBS_BANK_CODE='"
                    + objCommon.g_sConvert2Valid(sSIBSBankCode, false) + "'" +
                    " AND ID_REPORT=" + iReportID + " AND ID<>" + iID;
                }
                else
                {
                    strSQL = "SELECT ID FROM SYSINFO WHERE SIBS_BANK_CODE='"
                    + objCommon.g_sConvert2Valid(sSIBSBankCode, false) + "'" +
                    " AND ID_REPORT=" + iReportID;                
                }                
                ds = objDataAccess.dsGetDataSourceByStr(strSQL,"");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    bError = false;
                    strError = "Chi nhánh này và báo cáo này đã nhập!";
                    iIDEdit = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
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


        // Lay thong tin mot ban ghi/////////////////////////////////
        // Muc dich:    Lay thong tin mot ban ghi
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sUserID: Ma nhom
        // Dau ra:      Dataset
        /////////////////////////////////////////////////////////////        
        public DataSet GetParameterByID(int iID)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                
                strSql = "SELECT A.* FROM SYSINFO A WHERE A.ID =" + iID + "";
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


        // Lay thong tin he thong////////////////////////////////////
        // Muc dich:    Lay thong tin tham so he thong
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     
        // Dau ra:      Dataset
        /////////////////////////////////////////////////////////////        
        public DataSet GetAllParameterSystem(string strBranch)
        {
            try
            {
                strError = "";
                DataSet dsData = new DataSet();
                string strSql = "";
                if (strBranch.Trim().ToUpper() == "ALL")
                {
                    strSql = "SELECT A.*, B.REPORTNAME, C.BRAN_NAME " +
                    " FROM (SYSINFO A INNER JOIN LIST_REPORT B " +
                    " ON A.ID_REPORT= B.ID_REPORT) INNER JOIN " +
                    " (SELECT * FROM BRANCH ) C " +
                    " ON A.SIBS_BANK_CODE = C.SIBS_BANK_CODE " +
                    " ORDER BY A.ID ";
                }
                else
                {
                    strSql = "SELECT A.*, B.REPORTNAME, C.BRAN_NAME " +
                    " FROM (SYSINFO A INNER JOIN LIST_REPORT B " +
                    " ON A.ID_REPORT= B.ID_REPORT) INNER JOIN " +
                    " (SELECT * FROM BRANCH WHERE SIBS_BANK_CODE='" + strBranch + "') C " +
                    " ON A.SIBS_BANK_CODE = C.SIBS_BANK_CODE " +
                    " ORDER BY A.ID ";
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


        // Xoa mot parameter/////////////////////////////////////////
        // Muc dich:    Xoa mot parameter
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strBankCode
        //              Chu y: chuoi nay ko co 2 dau "(", ")"
        // Dau ra:      Dataset
        /////////////////////////////////////////////////////////////        
        public bool DeleteParameter(int iID)
        {
            try
            {
                strError = "";                
                string strSql = "";
                strSql = "DELETE FROM SYSINFO WHERE ID = " + iID + "";
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



    }
}
