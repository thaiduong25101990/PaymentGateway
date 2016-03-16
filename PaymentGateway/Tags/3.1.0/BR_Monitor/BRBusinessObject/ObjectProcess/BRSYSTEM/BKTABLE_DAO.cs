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
//' Create date:	09/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 09/05/2008
//' =============================================

namespace BR.BRBusinessObject
{
    public class BKTABLEDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public BKTABLEDP()
		{
		}
        public static BKTABLEDP Instance()
		{
            return new BKTABLEDP();
		}
        
        // ham lay du lieu la ten cac bang va BKTYPE=2;
        public DataSet GetBKTABLE(string pBKTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select distinct(trim(bk.bktime)) BKTIME from BKTABLE bk where Trim(bk.bktype)='" + pBKTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
             }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet Get_data()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet Get_data1(string strBKTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk where bk.bktype='" + strBKTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet GetBKTABLE1(string pBKTYPE,string pBKTIME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk where bk.bktype='" + pBKTYPE + "' and bk.bktime='" + pBKTIME + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        // ham tim kiem cac bang theo dieu kien dua vao
        public DataSet GetBk_Search(string pBKTYPE,string pBKTIME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.sourcetbl,bk.destbl from BKTABLE bk where bk.bktime='" + pBKTIME + "' and  bk.bktype='" + pBKTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);                
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        // ham tim kiem cac bang theo dieu kien dua vao
        public DataSet GetBk_SearchBktype(string pBKTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk where bk.bktime='" + pBKTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
               
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        // ham tim kiem cac bang theo dieu kien dua vao
        public DataSet GetBk_Bktime(string pBKTIME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk where Trim(bk.bktime)='" + pBKTIME + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataSet GetBk_SearchBktype1(string pBKTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk where bk.bktime='" + pBKTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
               
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        public DataSet GetBk_SearchBktype1(string pBKTYPE,string pBKTIME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.sourcetbl,bk.destbl,bk.bktype,bk.bktime,bk.filepath,bk.lastclean,bk.lastexp from BKTABLE bk where bk.bktime like '%" + pBKTIME + "%' and bk.bktype like '%" + pBKTYPE + "%'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        // ham tim kiem cac bang theo dieu kien dua vao
        public DataSet GetBk_SearchDestbl(string pBKTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select bk.DESTBL from BKTABLE bk where trim(bk.bktime) like '%" + pBKTYPE + "%'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);              
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
        // ham lay ra nhung du lieu bi trung
        public DataSet GetBKTABLE_DATA(string pTable1,string pTable2)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select * from :pTable2 in (select * from :pTable1)";
            OracleParameter[] oraParam = { new OracleParameter("pTable1", OracleType.VarChar, 100),
                                         new OracleParameter("pTable2", OracleType.VarChar, 100)};

            oraParam[0].Value = pTable1;
            oraParam[1].Value = pTable2;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public int UpdateLASTEXP(DateTime dtLastExp, string strSOURCETBL)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                string strSQL = "Update BKTABLE set LASTEXP = '" + dtLastExp + "' where UPPER(SOURCETBL) =UPPER('" + strSOURCETBL.Trim() + "')";
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL);
            }
            catch //(Exception ex)
            {

                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }
            return 1;
        }

        public int UpdateField(string strFieldUpdate, string strValueUpdate, string strFieldKey,string strValueKey)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                string strSQL = "Update BKTABLE set " + strFieldUpdate + " = '" + strValueUpdate + "' where UPPER(" + strFieldKey + ") =UPPER('" + strValueKey + "')";
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL);
            }
            catch //(Exception ex)
            {

                return -1;
            }
            finally
            {
                oraConn.Dispose();
            }
            return 1;
        }

    }
}
