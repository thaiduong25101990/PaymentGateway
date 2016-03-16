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

namespace BR.BRBusinessObject
{
    public class VCB_FEEDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        public VCB_FEEDP()
        {
        }
        public static VCB_FEEDP Instance()
        {
            return new VCB_FEEDP();
        }
        public DataTable Get_VCB_FEE(string sCCYCD)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL ="";

            if (sCCYCD != "ALL")
            {
                strSQL = "SELECT ID, FIXEDFEE,RATE," +
                "MINFEE,MAXFEE, CCYCD FROM VCB_FEE " +
                "WHERE CCYCD='" + sCCYCD + "'";
            }
            else
            {
                strSQL = "SELECT ID, FIXEDFEE,RATE," +
                "MINFEE,MAXFEE, CCYCD FROM VCB_FEE ";
            }

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public int DELETE_VCB_FEE(long pID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "DELETE VCB_FEE WHERE ID = " + pID;

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
        }

        public int ADD_VCB_FEE(VCB_FEE_Info objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "INSERT INTO VCB_FEE(FIXEDFEE,RATE," +
                "MINFEE,MAXFEE,CCYCD) VALUES " +
                "(:pFIXEDFEE,:pRATE,:pMINFEE,:pMAXFEE,:pCCYCD) ";
            OracleParameter[] Orapara = {new OracleParameter("pFIXEDFEE",OracleType.Number,20),
                                        new OracleParameter("pRATE",OracleType.Number,4),
                                        new OracleParameter("pMINFEE",OracleType.Number,20),
                                        new OracleParameter("pMAXFEE",OracleType.Number,20),
                                        new OracleParameter("pCCYCD",OracleType.Char,5)
                                        };

            Orapara[0].Value = objTable.FIXEDFEE;
            Orapara[1].Value = objTable.RATE;
            Orapara[2].Value = objTable.MINFEE;
            Orapara[3].Value = objTable.MAXFEE;
            Orapara[4].Value = objTable.CCYCD;

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
            }
            catch 
            {
                return -1;
            }
        }

        //************************************************
        //Muc dich: Check loai tien nay da nhap bieu phi?
        //
        //************************************************
        public DataSet CheckCCYCD(string pCCYCD, long pID)
        {
            oraConn = connect.Connect();
            string strSQL;
            
            if (oraConn == null)
            {
                return null;
            }
            if (pID == 0)
                strSQL = "select * FROM VCB_FEE " +
                "WHERE CCYCD='" + pCCYCD + "'";
            else
                strSQL = "select * FROM VCB_FEE " +
                "WHERE CCYCD='" + pCCYCD + "' AND ID <> " + pID;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
        }

        public int UPDATE_VCB_FEE(VCB_FEE_Info objTable, int iFeeType)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "";
            
            try
            {
                //Phi co dinh
                if (iFeeType == 1)
                {   
                    strSQL = "UPDATE VCB_FEE SET FIXEDFEE=:pFIXEDFEE WHERE ID = :pID";
                    OracleParameter[] Orapara = {new OracleParameter("pFIXEDFEE",OracleType.Number,20),                                     
                                                new OracleParameter("pID",OracleType.Number,20)
                                                };

                    Orapara[0].Value = objTable.FIXEDFEE;                 
                    Orapara[1].Value = objTable.ID;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                    
                }
                //Phi theo ty le
                else
                {
                    strSQL = "UPDATE VCB_FEE SET RATE=:pRATE,MINFEE=:pMINFEE,"+
                        "MAXFEE=:pMAXFEE WHERE ID = :pID";
                    OracleParameter[] Orapara = {new OracleParameter("pRATE",OracleType.Number,8),
                                            new OracleParameter("pMINFEE",OracleType.Number,20),
                                            new OracleParameter("pMAXFEE",OracleType.Number,20),
                                            new OracleParameter("pID",OracleType.Number,20)
                                            };

                    Orapara[0].Value = objTable.RATE;
                    Orapara[1].Value = objTable.MINFEE;
                    Orapara[2].Value = objTable.MAXFEE;
                    Orapara[3].Value = objTable.ID;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                }
            
                
            }
            catch 
            {
                return -1;
            }

        }
        
        public DataTable CAL_FEE(DateTime fromdate, DateTime todate, string pBranch,
                                int pFeeType, string pCCYCD)
        {
            try
            {
                string strSQL = "VCB_FEE_CAL";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),
                                              new OracleParameter("pBranch", OracleType.VarChar,5),
                                              new OracleParameter("pFeeType", OracleType.Number,2),
                                              new OracleParameter("pCCYCD", OracleType.VarChar,5),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranch;
                oraPara[3].Value = pFeeType;
                oraPara[4].Value = pCCYCD;
                oraPara[5].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraPara).Tables[0];
            }
            catch 
            {
                return null;
            }        
        }

        public DataTable CAL_FEE_DETAIL(DateTime fromdate, DateTime todate, string pBranch,
                                int pFeeType, string pCCYCD)
        {
            try
            {

                string strSQL = "VCB_FEE_CAL_DETAIL";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),
                                              new OracleParameter("pBranch", OracleType.VarChar,5),
                                              new OracleParameter("pFeeType", OracleType.Number,2),
                                              new OracleParameter("pCCYCD", OracleType.VarChar,5),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranch;
                oraPara[3].Value = pFeeType;
                oraPara[4].Value = pCCYCD;
                oraPara[5].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraPara).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public DataTable CAL_FEE_DETAIL_EXCEL(DateTime fromdate, DateTime todate, string pBranch,
                                int pFeeType, string pCCYCD)
        {
            try
            {

                string strSQL = "VCB_FEE_CAL_DETAIL_EXCEL";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),
                                              new OracleParameter("pBranch", OracleType.VarChar,5),
                                              new OracleParameter("pFeeType", OracleType.Number,2),
                                              new OracleParameter("pCCYCD", OracleType.VarChar,5),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranch;
                oraPara[3].Value = pFeeType;
                oraPara[4].Value = pCCYCD;
                oraPara[5].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraPara).Tables[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
