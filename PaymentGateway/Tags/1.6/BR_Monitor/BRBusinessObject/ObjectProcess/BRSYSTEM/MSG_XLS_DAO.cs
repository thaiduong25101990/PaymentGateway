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
//' Author:	Nguyen duc quy
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 27/05/2008
//' =============================================

namespace BR.BRBusinessObject
{
    public class MSG_XLSDP
    {
         private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        //private OracleConnection cnn=new OracleConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\book1.xls;Extended Properties='Excel 8.0'");
        
        public MSG_XLSDP()
        {
        }
        public static MSG_XLSDP Instance()
        {
            return new MSG_XLSDP();
        }

        public DataTable MSG_XLS(string pGWTYPE,string pMSG_TYPE,string pMSG_DIRECTION)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select a.* from (SELECT  XLSCOL,'('||trim(SWIFT_FIELD_NAME)||') '||MAX(FIELD_DESCRIPTION) field,CHK FROM MSG_XLS ";
            strSQL = strSQL + "   WHERE GWTYPE='" + pGWTYPE + "' AND MSG_TYPE='" + pMSG_TYPE + "'  AND MSG_DIRECTION = '" + pMSG_DIRECTION + "'   AND UPPER(SUBSTR(XLSCOL,1,1))='F'";
            strSQL = strSQL + "   GROUP BY XLSCOL,SWIFT_FIELD_NAME,CHK)a order by To_number(Substr(a.XLSCOL,2)) asc";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }


        public DataTable MSG_XLS_VCB(string pGWTYPE, string pMSG_TYPE, string pMSG_DIRECTION)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select a.* from (SELECT XLSCOL,'('||trim(SWIFT_FIELD_NAME)||') '||MAX(FIELD_DESCRIPTION) field,CHK FROM MSG_XLS ";
            strSQL = strSQL + "   WHERE GWTYPE='" + pGWTYPE + "' AND MSG_TYPE='" + pMSG_TYPE + "'  AND MSG_DIRECTION = '" + pMSG_DIRECTION + "'  AND UPPER(SUBSTR(XLSCOL,1,1))='F'";
            strSQL = strSQL + "   GROUP BY XLSCOL,SWIFT_FIELD_NAME,CHK)a order by To_number(Substr(a.XLSCOL,2)) asc";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }



        public DataTable COLUMNS_MSG_XLS(string pGWTYPE, string pMSG_TYPE, string pFIELD_NAME, string pMSG_DIRECTION)
        {
            oraConn = connect.Connect();//cbbDirection
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select XLSCOL from MSG_XLS M where Trim(upper(M.FIELD_NAME))='" + pFIELD_NAME + "'and Trim(M.GWTYPE)='" + pGWTYPE + "' and Trim(M.MSG_TYPE)='" + pMSG_TYPE + "' AND MSG_DIRECTION = '" + pMSG_DIRECTION + "'";           
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable COLUMNS_MSG_XLS_SWIFT(string pGWTYPE, string pMSG_TYPE, string pFIELD_NAME, string pMSG_DIRECTION)
        {
            oraConn = connect.Connect();//cbbDirection
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select XLSCOL from MSG_XLS M where Trim(upper(M.FIELD_NAME))='" + pFIELD_NAME + "'and Trim(M.GWTYPE)='" + pGWTYPE + "' and Trim(M.MSG_TYPE)='" + pMSG_TYPE + "'  AND MSG_DIRECTION = '" + pMSG_DIRECTION + "'";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable Columns_Check(string pGWTYPE, string pMSG_TYPE, string pFIELD_NAME, string pMSG_DIRECTION)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select XLSCOL from MSG_XLS M where Trim(upper(M.SWIFT_FIELD_NAME))='" + pFIELD_NAME + "'and Trim(M.GWTYPE)='" + pGWTYPE + "' and Trim(M.MSG_TYPE)='" + pMSG_TYPE + "' AND MSG_DIRECTION = '" + pMSG_DIRECTION + "'";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public DataTable MSG_XLS_IBPS(string pGWTYPE, string pMSG_DIRECTION)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select a.* from (SELECT XLSCOL,'('||trim(SWIFT_FIELD_NAME)||') '||MAX(FIELD_DESCRIPTION) field,CHK FROM MSG_XLS ";
            strSQL = strSQL + "   WHERE GWTYPE='" + pGWTYPE + "' AND MSG_DIRECTION = '" + pMSG_DIRECTION + "'  AND UPPER(SUBSTR(XLSCOL,1,1))='F'";
            strSQL = strSQL + "   GROUP BY XLSCOL,SWIFT_FIELD_NAME,CHK)a order by To_number(Substr(a.XLSCOL,2)) asc";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch
            {
                return null;
            }
        }

    }
}
