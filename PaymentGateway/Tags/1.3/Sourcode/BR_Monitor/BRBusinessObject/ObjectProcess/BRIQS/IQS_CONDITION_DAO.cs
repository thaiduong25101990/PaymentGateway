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
    public class IQS_CONDITIONDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        //private OracleConnection cnn=new OracleConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\book1.xls;Extended Properties='Excel 8.0'");
        
        public IQS_CONDITIONDP()
		{
		}
        public static IQS_CONDITIONDP Instance()
		{
            return new IQS_CONDITIONDP();
		}
        public int ADDIQS(IQS_CONDITIONInfo objTable)
        {
            string strSql = "Insert into IQS_CONDITION(MSG_TYPE,GWTYPE) values (:pTYPE,:pGWTYPE)";
            OracleParameter[] oraParam = {new OracleParameter("pTYPE", OracleType.VarChar,10),
                                         new OracleParameter("pGWTYPE", OracleType.VarChar,10)};
           
            oraParam[0].Value = objTable.MSG_TYPE;
            oraParam[1].Value = objTable.GWTYPE;
            

            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1; ;
            }
        }
        public int DELETE(string ID)
        {
            string strSql = "Delete from IQS_CONDITION IC where IC.ID='" + ID + "'";
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                    return -1;
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return -1; ;
            }
        }
        public DataTable GetIQS()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select IC.ID,IC.MSG_TYPE,IC.GWTYPE from IQS_CONDITION IC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetIQS_DISTIN()
        {            
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select Distinct(IC.MSG_TYPE) from IQS_CONDITION IC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetIQS_DISTIN1()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select Distinct(IC.MSG_TYPE) from IQS_CONDITION IC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }
        public DataTable Search(string strWhere)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select IC.ID,IC.MSG_TYPE,IC.GWTYPE from IQS_CONDITION IC  " + strWhere + "";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch 
            {
                return null;
            }
        }
        /****************************************
         * Search dien IQS Ex: TTSP, MT195
         * HoangLA
         ****************************************/
        public DataTable Search1(string Gwtype,string Type)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select IC.ID,IC.MSG_TYPE,IC.GWTYPE from IQS_CONDITION IC where IC.GWTYPE='" + Gwtype + "' and IC.MSG_TYPE='" + Type + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetKT(string Type, string Gwtype)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select IC.ID,IC.MSG_TYPE,IC.GWTYPE from IQS_CONDITION IC where IC.GWTYPE='" + Gwtype + "' and IC.MSG_TYPE='" + Type + "'";

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
