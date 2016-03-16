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
//' Create date:	12/06/2008 21:40
//' Description:
//' Revise History:
//' Update      :   Nguyen duc quy 12/06/2008
//' =============================================
namespace BR.BRBusinessObject
{
    public class EXCELDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        
        public EXCELDP()
		{
		}
        public static EXCELDP Instance()
		{
            return new EXCELDP();
		}
        public DataSet GetEXCEL(string pMSG_ID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select E.MSG_ID,E.F1,E.F2,E.F3,E.F4,E.F5,E.F6,E.F7,E.F8,E.F9,E.F10,E.F11,E.F12,E.F13,E.F14,E.F15,E.F16,E.F17,E.FILE_NAME,E.TRANS_DATE,E.STATUS,E.GWTYPE,E.MSG_DIRECTION from EXCEL E WHERE E.MSG_ID='" + pMSG_ID + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);               
            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetEXCEL1(string strUserID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select E.MSG_ID,E.F1,E.F2,E.F3,E.F4,E.F5,E.F6,E.F7,E.F8,E.F9,E.F10,E.F11,E.F12,E.F13,E.F14,E.F15,E.F16,E.F17,E.FILE_NAME,E.TRANS_DATE,(select a.content from Allcode a where a.cdname='STATUS' and trim(a.gwtype)='SWIFT'  and Trim(a.cdval)= E.STATUS) as STATUS,E.GWTYPE,E.MSG_DIRECTION,E.teller_id from EXCEL E where  E.Status='2' and Trim(E.TELLER_ID)<>'" + strUserID + "'   order by MSG_ID ASC ";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataTable GetEXCEL_APP(string strUserID,string strMsg_direction)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select E.MSG_ID,E.F1,E.F2,E.F3,E.F4,E.F5,E.F6,E.F7,E.F8,E.F9,E.F10,E.F11,E.F12,E.F13,E.F14,E.F15,E.F16,E.F17,E.FILE_NAME,E.TRANS_DATE,(select a.content from Allcode a where a.cdname='STATUS' and trim(a.gwtype)='SWIFT'  and Trim(a.cdval)= E.STATUS) as STATUS,E.GWTYPE,E.MSG_DIRECTION,E.teller_id from EXCEL E where  E.Status='2' and Trim(E.TELLER_ID)<>'" + strUserID + "' and trim(E.Msg_Direction)='" + strMsg_direction + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public int AddExcel(EXCELInfo objTable)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("F1",OracleType.VarChar,200) ,
                                                new OracleParameter("F2",OracleType.VarChar,200),
                                                new OracleParameter("F3",OracleType.VarChar,200),
                                                new OracleParameter("F4",OracleType.VarChar,200),
                                                new OracleParameter("F5",OracleType.VarChar,200),
                                                new OracleParameter("F6",OracleType.VarChar,200),
                                                new OracleParameter("F7",OracleType.VarChar,200),
                                                new OracleParameter("F8",OracleType.VarChar,200),
                                                new OracleParameter("F9",OracleType.VarChar,200),
                                                new OracleParameter("F10",OracleType.VarChar,200),
                                                new OracleParameter("F11",OracleType.VarChar,200),
                                                new OracleParameter("F12",OracleType.VarChar,200),
                                                new OracleParameter("F13",OracleType.VarChar,200),
                                                new OracleParameter("F14",OracleType.VarChar,200),
                                                new OracleParameter("F15",OracleType.VarChar,200),
                                                new OracleParameter("F16",OracleType.VarChar,200),
                                                new OracleParameter("F17",OracleType.VarChar,200),
                                                new OracleParameter("FILE_NAME",OracleType.VarChar,100),                                                
                                                new OracleParameter("STATUS",OracleType.Number,1),
                                                new OracleParameter("GWTYPE",OracleType.VarChar,20),
                                                new OracleParameter("MSG_DIRECTION",OracleType.VarChar,10),
                                                new OracleParameter("TYPE",OracleType.VarChar,20),
                                                new OracleParameter("TELLER_ID",OracleType.VarChar,20),
                                                new OracleParameter("OFFICER_ID",OracleType.VarChar,20)};
                
                oraParas[0].Value = objTable.F1;
                oraParas[1].Value = objTable.F2;
                oraParas[2].Value = objTable.F3;
                oraParas[3].Value = objTable.F4;
                oraParas[4].Value = objTable.F5;
                oraParas[5].Value = objTable.F6;
                oraParas[6].Value = objTable.F7;
                oraParas[7].Value = objTable.F8;
                oraParas[8].Value = objTable.F9;
                oraParas[9].Value = objTable.F10;
                oraParas[10].Value = objTable.F11;
                oraParas[11].Value = objTable.F12;
                oraParas[12].Value = objTable.F13;
                oraParas[13].Value = objTable.F14;
                oraParas[14].Value = objTable.F15;
                oraParas[15].Value = objTable.F16;
                oraParas[16].Value = objTable.F17;
                oraParas[17].Value = objTable.FILE_NAME;
                oraParas[18].Value = objTable.STATUS;
                oraParas[19].Value = objTable.GWTYPE;
                oraParas[20].Value = objTable.MSG_DIRECTION;
                oraParas[21].Value = objTable.TYPE;
                oraParas[22].Value = objTable.TELLER_ID;
                oraParas[23].Value = objTable.OFFICER_ID;               

                try
                {
                    oraConn = connect.Connect();
                    if (oraConn == null)
                    {
                        return -1;
                    }
                    else
                    {
                        iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_EXCEL_ACCESS.Excel_Insert", oraParas);
                    }
                }
                catch 
                {
                    return -1;
                }
                finally
                {
                    oraConn.Dispose();

                }
                return iResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int DeleteRows(string Msg_id)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "Delete from Excel e where e.msg_id='" + Msg_id + "'";
            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);               
                return 1;
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return 1;
        }
        public int UpdateStatus(string Msg_id,string strUserID)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return -1;
            }
            string strSQL = "update Excel e set e.status = 0,e.officer_id='" + strUserID + "' where e.msg_id='" + Msg_id + "'";
            try
            {
                DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSQL, null);
                return 1;
            }
            catch //(Exception ex)
            {
                return -1;
            }
            //return 1;
        }
        public DataTable Check_Excel(string pWhere)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select * from Excel " + pWhere;
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];               
            }
            catch //(Exception ex)
            {
                return null;
            }          
        }
        public DataTable Check_Excel_VCB(string pWhere)
        {
            DataTable _dt = new DataTable();
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select * from Excel " + pWhere + " and to_char(TRANS_DATE,'YYYYMMDD') = To_char(Sysdate,'YYYYMMDD')";
            try
            {
               _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
               oraConn.Close();
               oraConn.Dispose();
               return _dt;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        public DataTable Check_Excel_IBPS(string pWhere)
        {
            DataTable _dt = new DataTable();
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select * from IBPS_EXCEL " + pWhere + " and TRANSDATE = To_char(Sysdate,'YYYYMMDD') ";
            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return null;
            }
        }

        public int AddExcel_IBPS(string pCHECK_KEY, string pCONTENT,
            string pFILE_NAME, string pTELLERID,string pTYPE)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pCHECK_KEY",OracleType.VarChar,50) ,
                                                new OracleParameter("pCONTENT",OracleType.VarChar,2000),
                                                new OracleParameter("pFILE_NAME",OracleType.VarChar,100),
                                                new OracleParameter("pTELLERID",OracleType.VarChar,8),
                                                new OracleParameter("pFILE_TYPE",OracleType.VarChar,5)
                                            };

                oraParas[0].Value = pCHECK_KEY;
                oraParas[1].Value = pCONTENT;
                oraParas[2].Value = pFILE_NAME;
                oraParas[3].Value = pTELLERID;
                oraParas[4].Value = pTYPE;

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_EXCEL_ACCESS.Excel_Insert_Ibps", oraParas);
                    oraConn.Close();
                    oraConn.Dispose();
                    return iResult;
                }
            }
            catch
            {
                return -1;
            }
        }


        public DataTable Check_SWIFT_TEXT(string pWhere)
        {
            DataTable _dt = new DataTable();
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = " select * from SWIFT_TEXT " + pWhere + " ";
            try
            {
                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return null;
            }
        }

        public int AddSWIFT_TEXT(string pCHECK_KEY, string pCONTENT,
            string pFILE_NAME, string pTELLERID, string pTYPE)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pCHECK_KEY",OracleType.VarChar,50) ,
                                                new OracleParameter("pCONTENT",OracleType.Clob),
                                                new OracleParameter("pFILE_NAME",OracleType.VarChar,100),
                                                new OracleParameter("pTELLERID",OracleType.VarChar,8),
                                                new OracleParameter("pFILE_TYPE",OracleType.VarChar,4)
                                            };

                oraParas[0].Value = pCHECK_KEY;
                oraParas[1].Value = pCONTENT;
                oraParas[2].Value = pFILE_NAME;
                oraParas[3].Value = pTELLERID;
                oraParas[4].Value = pTYPE;

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_EXCEL_ACCESS.Insert_swift_text", oraParas);
                    oraConn.Close();
                    oraConn.Dispose();
                    return iResult;
                }
            }
            catch
            {
                return -1;
            }
        }

        public int UPDATESWIFT_TEXT(string pFILE_NAME, string pTELLERID, string pTYPE)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pFILE_NAME",OracleType.VarChar,100),
                                                new OracleParameter("pTELLERID",OracleType.VarChar,8),
                                                new OracleParameter("pFILE_TYPE",OracleType.VarChar,4)
                                            };
                
                oraParas[0].Value = pFILE_NAME;
                oraParas[1].Value = pTELLERID;
                oraParas[2].Value = pTYPE;
                oraConn = connect.Connect();

                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_EXCEL_ACCESS.Update_swift_text", oraParas);
                    oraConn.Close();
                    oraConn.Dispose();
                    return iResult;
                }
            }
            catch
            {
                return -1;
            }
        }

    }
}
