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
    public class REPORT_PARA_DAO
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
        public REPORT_PARA_DAO()
		{

		}


        //Ham tao mot Instance cua doi tuong///////////////////////////
        //Mo ta:        Ham tao mot Instance cua doi tuong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Doi tuong SYSPARA_DAO
        ///////////////////////////////////////////////////////////////
        public static REPORT_PARA_DAO Instance()
		{
            return new REPORT_PARA_DAO();
		}


        //Ham them moi tham so he thong////////////////////////////////
        //Mo ta:        Ham them moi tham so he thong
        //Ngay tao:     08/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      objTable: Bang SYSVAR
        //Dau ra:       Them moi thanh cong
        ///////////////////////////////////////////////////////////////
        public int AddREPORT_PARA(REPORT_PARA_INFO objTable)
        {
            DataTable datTable = new DataTable();
            int iBool;
            string strSQL="Insert into SYSINFO(SIBS_BANK_CODE,TIME,DESCRIPTION,ID_REPORT,REPORTNAME)" +
                " values (:pSIBS_BANK_CODE,:pTIME,:pDESCRIPTION,:pID_REPORT,:pREPORTNAME)";
            OracleParameter[] oraParam = {new OracleParameter("pSIBS_BANK_CODE", OracleType.VarChar, 10),
                                         new OracleParameter("pTIME", OracleType.VarChar, 30),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar,200),
                                         new OracleParameter("pID_REPORT", OracleType.Number,5),
                                         new OracleParameter("pREPORTNAME", OracleType.VarChar,100)};
            oraParam[0].Value = objTable.SIBS_BANK_CODE;
            oraParam[1].Value = objTable.TIME;
            oraParam[2].Value = objTable.DESCRIPTION;
            oraParam[3].Value = objTable.ID_REPORT;
            oraParam[4].Value = objTable.REPORTNAME;

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
        public int UpdateREPORT_PARA(REPORT_PARA_INFO objTable)
        {
            int iBool;
            string strSQL = "Update SYSINFO SET SIBS_BANK_CODE =:pSIBS_BANK_CODE, " +
                " TIME =:pTIME,DESCRIPTION =:pDESCRIPTION,ID_REPORT =:pID_REPORT, " +
                " REPORTNAME=:pREPORTNAME WHERE ID = " + objTable.ID;

            OracleParameter[] oraParam = {new OracleParameter("pSIBS_BANK_CODE", OracleType.VarChar, 30),
                                         new OracleParameter("pTIME", OracleType.VarChar,30),
                                         new OracleParameter("pDESCRIPTION", OracleType.NVarChar,30),
                                         new OracleParameter("pID_REPORT", OracleType.Number,5),
                                         new OracleParameter("pREPORTNAME", OracleType.VarChar,100)
                                         };

            oraParam[0].Value = objTable.SIBS_BANK_CODE;
            oraParam[1].Value = objTable.TIME;
            oraParam[2].Value = objTable.DESCRIPTION;
            oraParam[3].Value = objTable.ID_REPORT;
            oraParam[4].Value = objTable.REPORTNAME;

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

    }
}
