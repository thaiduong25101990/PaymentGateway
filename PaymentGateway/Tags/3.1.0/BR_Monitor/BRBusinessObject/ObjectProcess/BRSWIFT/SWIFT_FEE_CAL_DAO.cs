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
    public class SWIFT_FEEDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();

        public SWIFT_FEEDP()
        {
        }
        public static SWIFT_FEEDP Instance()
        {
            return new SWIFT_FEEDP();
        }
        public DataSet Get_SWIFT_FEE()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select ID,MSG_TYPE, FIXED_FEE,RATE_FEE," +
            "MIN_FEE,MAX_FEE,CCYCD FROM SWIFT_FEE ORDER BY MSG_TYPE";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public int DELETE_SWIFT_FEE(int pID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "DELETE SWIFT_FEE WHERE ID = " + pID;

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return -1;
            }
        }

        public int ADD_SWIFT_FEE(SWIFT_FEE_Info objTable)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "insert into SWIFT_FEE(FIXED_FEE,RATE_FEE," +
                "MIN_FEE,MAX_FEE,MSG_TYPE,CCYCD) values " +
                "(:pFIXED_FEE,:pRATE_FEE,:pMIN_FEE,:pMAX_FEE,:pMSG_TYPE,:pCCYCD) ";
            OracleParameter[] Orapara = {new OracleParameter("pFIXED_FEE",OracleType.Number,20),
                                        new OracleParameter("pRATE_FEE",OracleType.Number,4),
                                        new OracleParameter("pMIN_FEE",OracleType.Number,20),
                                        new OracleParameter("pMAX_FEE",OracleType.Number,20),
                                        new OracleParameter("pMSG_TYPE",OracleType.VarChar,8),
                                        new OracleParameter("pCCYCD",OracleType.VarChar,3)
                                        };

            Orapara[0].Value = objTable.FIXED_FEE;
            Orapara[1].Value = objTable.RATE_FEE;
            Orapara[2].Value = objTable.MIN_FEE;
            Orapara[3].Value = objTable.MAX_FEE;
            Orapara[4].Value = objTable.MSG_TYPE;
            Orapara[5].Value = objTable.CCYCD;

            try
            {
                return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
            }
            catch 
            {
                return -1;
            }
        }

        public int UPDATE_SWIFT_FEE(SWIFT_FEE_Info objTable)
        {
            oraConn = connect.Connect();
            string strSQL = "";
            try
            {
                if (oraConn == null)
                {
                    return -1;
                }
                //Phi theo ty le
                if (objTable.CCYCD=="")
                {
                    strSQL = "Update SWIFT_FEE SET " +
                        "RATE_FEE=:pRATE_FEE,MIN_FEE=:pMIN_FEE," +
                        "MAX_FEE=:pMAX_FEE,MSG_TYPE=:pMSG_TYPE " +
                        "WHERE ID = :pID";
                    OracleParameter[] Orapara = {new OracleParameter("pRATE_FEE",OracleType.Number,8),
                                                new OracleParameter("pMIN_FEE",OracleType.Number,20),
                                                new OracleParameter("pMAX_FEE",OracleType.Number,20),
                                                new OracleParameter("pID",OracleType.Number,20),
                                                new OracleParameter("pMSG_TYPE",OracleType.VarChar,8)
                                                };
                                        
                    Orapara[0].Value = objTable.RATE_FEE;
                    Orapara[1].Value = objTable.MIN_FEE;
                    Orapara[2].Value = objTable.MAX_FEE;
                    Orapara[3].Value = objTable.ID;
                    Orapara[4].Value = objTable.MSG_TYPE;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                }
                else
                {
                    strSQL = "Update SWIFT_FEE SET FIXED_FEE=:pFIXED_FEE," +
                        "MSG_TYPE=:pMSG_TYPE,CCYCD=:pCCYCD WHERE ID = :pID";
                    OracleParameter[] Orapara = {new OracleParameter("pFIXED_FEE",OracleType.Number,20),
                                                new OracleParameter("pID",OracleType.Number,20),
                                                new OracleParameter("pMSG_TYPE",OracleType.VarChar,8),
                                                new OracleParameter("pCCYCD",OracleType.VarChar,3)
                                                };

                    Orapara[0].Value = objTable.FIXED_FEE;
                    Orapara[1].Value = objTable.ID;
                    Orapara[2].Value = objTable.MSG_TYPE;
                    Orapara[3].Value = objTable.CCYCD;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, Orapara);
                }                
            }
            catch 
            {
                return -1;
            }
        }
        
        public DataTable CAL_FEE(DateTime fromdate, DateTime todate,
            string pBranch, int pFeeType)
        {
            try
            {

                DataSet ds = new DataSet();
                string strSQL = "SWIFT_FEE_CAL";
                oraConn = connect.Connect();

                OracleParameter[] oraPara = { new OracleParameter("pFromDate", OracleType.DateTime,7),
                                              new OracleParameter("pToDate", OracleType.DateTime,7),
                                              new OracleParameter("pBranch", OracleType.VarChar,5),
                                              new OracleParameter("pFeeType", OracleType.Number,5),
                                              new OracleParameter("RefCurBK02", OracleType.Cursor)};
                oraPara[0].Value = fromdate;
                oraPara[1].Value = todate;
                oraPara[2].Value = pBranch;
                oraPara[3].Value = pFeeType;
                oraPara[4].Direction = ParameterDirection.Output;

                oraConn = connect.Connect();
                if (oraConn == null)
                    return null;

                ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraPara);
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch 
            {
                return null;
            }
        }

        public DataSet CheckMsgType(string pMsgType, long pID)
        {
            oraConn = connect.Connect();
            string strSQL;
            if (oraConn == null)
            {
                return null;
            }
            if (pID == 0)
                strSQL = "select ID,MSG_TYPE, FIXED_FEE,RATE_FEE," +
                "MIN_FEE,MAX_FEE FROM SWIFT_FEE " +
                "WHERE MSG_TYPE='" + pMsgType + "'"+
                " ORDER BY MSG_TYPE";
            else
                strSQL = "select ID,MSG_TYPE, FIXED_FEE,RATE_FEE," +
                "MIN_FEE,MAX_FEE FROM SWIFT_FEE " +
                "WHERE MSG_TYPE='" + pMsgType + "' AND ID<>" + pID +
                " ORDER BY MSG_TYPE";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch
            {
                return null;
            }
        }

    }
}
