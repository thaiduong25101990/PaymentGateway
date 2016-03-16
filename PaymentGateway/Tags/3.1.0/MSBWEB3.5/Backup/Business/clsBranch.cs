using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BIDVWEB.Comm;

namespace BIDVWEB.Business
{
    public class clsBranch
    {
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        public string strError = "";


        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay ten chi nhanh theo ma chi nhanh
        //Ngay tao:     05/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strID: Ma chi nhanh
        //Dau ra:       Ten chi nhanh
        ///////////////////////////////////////////////////////////////
        public string GetBranchName(string strID)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            DataRow dr;

            try
            {
                strSql = "SELECT BRAN_NAME FROM BRANCH WHERE LPAD(TRIM(SIBS_BANK_CODE),5,'0')=LPAD(TRIM('" + 
                    strID + "'),5,'0')";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    return dr["BRAN_NAME"].ToString();
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
