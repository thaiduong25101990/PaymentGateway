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
//' Create date:	10/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 27/05/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class SEARCHDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public SEARCHDP()
		{
		}
        public static SEARCHDP Instance()
		{
            return new SEARCHDP();
		}
        public DataTable COLUMNS_SEARCH(string pGwtype,out DataTable _dt)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return _dt = null;
            }
            OracleParameter[] Orapara ={new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                      new OracleParameter("pCOLUMNS_NAME",OracleType.Cursor)};
            Orapara[1].Direction = ParameterDirection.Output;
            Orapara[0].Value = pGwtype;

            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_COLUMNS_SEARCH.COLUMNS_SEARCH", Orapara).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch 
            {
                return _dt = null;
            }
        }
        public DataSet GetSEARCH_Operator(string pOPERATOR,string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select s.OPERATOR from search s where Trim(s.fieldname)='" + pOPERATOR + "' and Trim(s.gwtype) ='" + pGWTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);              
            }
            catch 
            {
                return null;
            }
        }
        public DataTable Getdata(string pCDNAME, string pGwtype)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select * from Search s where Trim(s.fieldcode)='" + pCDNAME + "' and s.gwtype='" + pGwtype + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        //Ham lay du lieu theo menh de select
        public DataTable Excute_Select(string pSelect)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable _dt = new DataTable();
            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, pSelect, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return null;
            }
        }
        /*Quynd cap nhat 20100318*/
        public DataTable dtSearch(string pGWTYPE)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                DataTable _dt = new DataTable();
                OracleParameter[] Orapara ={new OracleParameter("pGWTYPE",OracleType.VarChar,10),
                                      new OracleParameter("pSEARCH",OracleType.Cursor)};
                Orapara[0].Value = pGWTYPE;
                Orapara[1].Direction = ParameterDirection.Output;
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_SEARCH.SEARCH", Orapara).Tables[0];
                _dt.TableName = "SEARCH";
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return null;
            }
        }

    }
}
