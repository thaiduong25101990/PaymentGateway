using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace BRIBPS_TR_Inquiry
{
    public class ProcessInquiry
    {
        public static ProcessInquiry Instance()
        {
            return new ProcessInquiry();
        }

        public void ProcessService()
        {
            try
            {
                ///////*Lay du lieu ben TR day vao GW*/
                //////DataTable _dt = new DataTable();
                //////_dt = GetMessageSQL();/*Ham lay du lieu cua SWIFT*/
                //////if (_dt.Rows.Count > 0)
                //////{
                //////    InsertMessageOracle(_dt);
                //////}
                DataTable _dtIBPS = new DataTable();
                _dtIBPS = GetMessageSQL_IBPS();/*Ham lay du lieu cua IBPS*/
                if (_dtIBPS.Rows.Count > 0)
                {
                    InsertMessageOracle_IBPS(_dtIBPS);
                }

                ///////*Lay du lieu ben GW day vao TR*/                
                //////DataTable _dtOra = new DataTable();
                //////_dtOra = GetMessageOra();/*Ham lay du lieu cua SWIFT*/
                //////if (_dtOra.Rows.Count > 0)
                //////{
                //////    InsertMessageSQL(_dtOra);
                //////}
                
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(1, IBPSTR_Inquiry.SERVICE_NAME + " co loi xay ra khi Service Inquiry xu li." + ex.Message, 1);
                return;
            }
        }

        private DataTable GetMessageOra()
        { 
            try
            {
                string strSQL = "";
                DataTable _dtSQL = new DataTable();
                strSQL = strSQL + "SELECT MSG_ID,MSG_TYPE,MSG_DIRECTION,BRANCH_A,BRANCH_B,TRANS_DATE,VALUE_DATE,FIELD20,FIELD21,AMOUNT,CCY,FOREIGN_BANK,";
                strSQL = strSQL + "FOREIGN_BANK_NAME,PRIORITY,DELIVER_TYPE,CONTENT,STATUS,ACK_NAK,ACK_CONTENT,O_CONFIRM FROM SWIFT_TR where MSG_DIRECTION ='SWIFT-SIBS' and STATUS = 0 ";
                _dtSQL = Lib.ExcuteDataTable(strSQL);
                if (_dtSQL == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return _dtSQL;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private DataTable GetMessageOra_IBPS()
        {
            try
            {
                string strSQL = "";
                DataTable _dtSQL = new DataTable();
                strSQL = strSQL + "SELECT MSG_ID,MSG_DIRECTION ,TRANS_CODE,IBPS_TRANS_NUM,SIBS_TRANS_NUM,SENDER,RECEIVER,TRANS_DATE ,AMOUNT ,CCY,DESCRIPTION,CONTENT,STATUS";
                strSQL = strSQL + " FROM IBPS_TR where MSG_DIRECTION ='IBPS-SIBS' and STATUS = 0 ";
                _dtSQL = Lib.ExcuteDataTable(strSQL);
                if (_dtSQL == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return _dtSQL;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /*---------------------------------------------------------------
        * Method           : GetMessageSQL(...) 
        * Muc dich         : Thuc hien truy du lieu trong database SQL 
        * Tham so          : 
        * Tra ve           : DataTable
        * Ngay tao         : 04/02/2010
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 04/02/2010
        * Nguoi cap nhat   : QuyND
        *--------------------------------------------------------------*/
        private DataTable GetMessageSQL()
        {
            try
            {
                /*Lay du lieu ben TR day vao GW*/
                string strSQL = "";
                DataTable _dtSQL = new DataTable();
                strSQL = strSQL + "SELECT MSG_ID,MSG_TYPE,MSG_DIRECTION,BRANCH_A,BRANCH_B,TRANS_DATE,VALUE_DATE,FIELD20,FIELD21,AMOUNT,CCY,FOREIGN_BANK,";
                strSQL = strSQL + "FOREIGN_BANK_NAME,PRIORITY,DELIVER_TYPE,CONTENT,STATUS,ACK_NAK,ACK_CONTENT,O_CONFIRM FROM SWIFT_TR where MSG_DIRECTION ='SIBS-SWIFT' and STATUS = 0 ";
                _dtSQL = Lib.ExcuteSQLDataTable(strSQL);
                if (_dtSQL == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return _dtSQL;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /*---------------------------------------------------------------
      * Method           : GetMessageSQL(...) 
      * Muc dich         : Thuc hien truy du lieu trong database SQL 
      * Tham so          : 
      * Tra ve           : DataTable
      * Ngay tao         : 04/02/2010
      * Nguoi tao        : QuyND
      * Ngay cap nhat    : 04/02/2010
      * Nguoi cap nhat   : QuyND
      *--------------------------------------------------------------*/
        private DataTable GetMessageSQL_IBPS()
        {
            try
            {
                
                /*Lay du lieu ben TR day vao GW*/
                string strSQL = "";
                DataTable _dtSQL = new DataTable();
                strSQL = strSQL + "SELECT MSG_ID,MSG_DIRECTION ,TRANS_CODE,IBPS_TRANS_NUM,SIBS_TRANS_NUM,SENDER,RECEIVER,TRANS_DATE ,AMOUNT ,CCY,DESCRIPTION,CONTENT,STATUS";
                //strSQL = strSQL + " FROM IBPS_TR where MSG_DIRECTION ='SIBS-IBPS' and STATUS = 0 ";
                strSQL = strSQL + " FROM IBPS_TR where MSG_DIRECTION ='SIBS-IBPS' and STATUS = 0 and convert(varchar(8), TRANS_DATE,112) = '" + IBPSTR_Inquiry.pSysdate + "'";
                _dtSQL = Lib.ExcuteSQLDataTable(strSQL);
                if (_dtSQL == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return _dtSQL;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void InsertMessageOracle(DataTable _dtOra)
        {
            try
            {
                for (int i = 0; i < _dtOra.Rows.Count; i++)
                {
                    #region Lay du lieu cua cac truong co trong bang
                    int MSG_ID = Convert.ToInt32(_dtOra.Rows[i]["MSG_ID"].ToString());
                    string MSG_TYPE = _dtOra.Rows[i]["MSG_TYPE"].ToString();
                    string MSG_DIRECTION = _dtOra.Rows[i]["MSG_DIRECTION"].ToString();
                    string BRANCH_A = _dtOra.Rows[i]["BRANCH_A"].ToString();
                    string BRANCH_B = _dtOra.Rows[i]["BRANCH_B"].ToString();
                    DateTime TRANS_DATE = Convert.ToDateTime(_dtOra.Rows[i]["TRANS_DATE"].ToString());
                    DateTime VALUE_DATE = Convert.ToDateTime(_dtOra.Rows[i]["VALUE_DATE"].ToString());
                    string FIELD20 = _dtOra.Rows[i]["FIELD20"].ToString();
                    string FIELD21 = _dtOra.Rows[i]["FIELD21"].ToString();
                    Double AMOUNT = Convert.ToDouble(_dtOra.Rows[i]["AMOUNT"].ToString());
                    string CCY = _dtOra.Rows[i]["CCY"].ToString();
                    string FOREIGN_BANK = _dtOra.Rows[i]["FOREIGN_BANK"].ToString();
                    string FOREIGN_BANK_NAME = _dtOra.Rows[i]["FOREIGN_BANK_NAME"].ToString();
                    int PRIORITY = Convert.ToInt32(_dtOra.Rows[i]["PRIORITY"].ToString());
                    string DELIVER_TYPE = _dtOra.Rows[i]["DELIVER_TYPE"].ToString();
                    string CONTENT = _dtOra.Rows[i]["CONTENT"].ToString();
                    int STATUS = Convert.ToInt32(_dtOra.Rows[i]["STATUS"].ToString());
                    string ACK_NAK = _dtOra.Rows[i]["ACK_NAK"].ToString();
                    string ACK_CONTENT = _dtOra.Rows[i]["ACK_CONTENT"].ToString();
                    int O_CONFIRM = 0;
                    if (_dtOra.Rows[i]["O_CONFIRM"].ToString().Trim() == "")
                    {
                        O_CONFIRM = 0;
                    }
                    else
                    {
                        O_CONFIRM = Convert.ToInt32(_dtOra.Rows[i]["O_CONFIRM"].ToString());
                    }
                    string LINK_REF = "";
                    #endregion
                    InsertMessages(MSG_ID,
                                    MSG_TYPE,
                                    MSG_DIRECTION,
                                    BRANCH_A,
                                    BRANCH_B,
                                    TRANS_DATE,
                                    VALUE_DATE,
                                    FIELD20,
                                    FIELD21,
                                    AMOUNT,
                                    CCY,
                                    FOREIGN_BANK,
                                    FOREIGN_BANK_NAME,
                                    PRIORITY,
                                    DELIVER_TYPE,
                                    CONTENT,
                                    STATUS,
                                    ACK_NAK,
                                    ACK_CONTENT,
                                    O_CONFIRM,
                                    LINK_REF);
                }
                _dtOra.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        private void InsertMessageOracle_IBPS(DataTable _dtOra)
        {
            try
            {
                for (int i = 0; i < _dtOra.Rows.Count; i++)
                {
                    #region Lay du lieu cua cac truong co trong bang
                    int MSG_ID = Convert.ToInt32(_dtOra.Rows[i]["MSG_ID"].ToString());
                    string MSG_DIRECTION = _dtOra.Rows[i]["MSG_DIRECTION"].ToString();
                    string SENDER = _dtOra.Rows[i]["SENDER"].ToString();
                    string RECEIVER = _dtOra.Rows[i]["RECEIVER"].ToString();
                    DateTime TRANS_DATE = Convert.ToDateTime(_dtOra.Rows[i]["TRANS_DATE"].ToString());
                    string TRANS_CODE = _dtOra.Rows[i]["TRANS_CODE"].ToString();
                    string DESCRIPTION = _dtOra.Rows[i]["DESCRIPTION"].ToString();
                    Double AMOUNT = Convert.ToDouble(_dtOra.Rows[i]["AMOUNT"].ToString());
                    string CCY = _dtOra.Rows[i]["CCY"].ToString();
                    string IBPS_TRANS_NUM = _dtOra.Rows[i]["IBPS_TRANS_NUM"].ToString();
                    string SIBS_TRANS_NUM = _dtOra.Rows[i]["SIBS_TRANS_NUM"].ToString();
                    string CONTENT = _dtOra.Rows[i]["CONTENT"].ToString();
                    int STATUS = Convert.ToInt32(_dtOra.Rows[i]["STATUS"].ToString());

                    #endregion
                    InsertMessages_IBPS(MSG_ID,
                                    MSG_DIRECTION,
                                    SENDER,
                                    RECEIVER,
                                    TRANS_DATE,
                                    TRANS_CODE,
                                    DESCRIPTION,
                                    AMOUNT,
                                    CCY,
                                    IBPS_TRANS_NUM,
                                    SIBS_TRANS_NUM,
                                    CONTENT,
                                    STATUS
                                 );
                }
                _dtOra.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }       

        private void InsertMessageSQL(DataTable _dtOra)
        {
            try
            {
                for (int i = 0; i < _dtOra.Rows.Count; i++)
                {
                    Double AMOUNT = 0;
                    DateTime VALUE_DATE = new DateTime();
                    #region Lay du lieu cua cac truong co trong bang     
                    int MSG_ID = Convert.ToInt32(_dtOra.Rows[i]["MSG_ID"].ToString());
                    string MSG_TYPE = _dtOra.Rows[i]["MSG_TYPE"].ToString();
                    string MSG_DIRECTION = _dtOra.Rows[i]["MSG_DIRECTION"].ToString();
                    string BRANCH_A = _dtOra.Rows[i]["BRANCH_A"].ToString();
                    string BRANCH_B = _dtOra.Rows[i]["BRANCH_B"].ToString();
                    DateTime TRANS_DATE = Convert.ToDateTime(_dtOra.Rows[i]["TRANS_DATE"]);
                    if (_dtOra.Rows[i]["VALUE_DATE"].ToString() != "")
                    {                    
                        VALUE_DATE = Convert.ToDateTime(_dtOra.Rows[i]["VALUE_DATE"]);
                    }
                    string FIELD20 = _dtOra.Rows[i]["FIELD20"].ToString();
                    string FIELD21 = _dtOra.Rows[i]["FIELD21"].ToString();
                    if (_dtOra.Rows[i]["AMOUNT"].ToString() == "")
                    {
                        AMOUNT = 0;
                    }
                    else
                    {
                        AMOUNT = Convert.ToDouble(_dtOra.Rows[i]["AMOUNT"].ToString());
                    }
                    string CCY = _dtOra.Rows[i]["CCY"].ToString();
                    string FOREIGN_BANK = _dtOra.Rows[i]["FOREIGN_BANK"].ToString();
                    string FOREIGN_BANK_NAME = _dtOra.Rows[i]["FOREIGN_BANK_NAME"].ToString();
                    int PRIORITY = Convert.ToInt32(_dtOra.Rows[i]["PRIORITY"].ToString());
                    string DELIVER_TYPE = _dtOra.Rows[i]["DELIVER_TYPE"].ToString();
                    string CONTENT = _dtOra.Rows[i]["CONTENT"].ToString();
                    int STATUS = Convert.ToInt32(_dtOra.Rows[i]["STATUS"].ToString());
                    string ACK_NAK = _dtOra.Rows[i]["ACK_NAK"].ToString();
                    string ACK_CONTENT = _dtOra.Rows[i]["ACK_CONTENT"].ToString();
                    int O_CONFIRM = 0;
                    if (_dtOra.Rows[i]["O_CONFIRM"].ToString().Trim() == "")
                    {
                        O_CONFIRM = 0;
                    }
                    else
                    {
                        O_CONFIRM = Convert.ToInt32(_dtOra.Rows[i]["O_CONFIRM"].ToString());
                    }
                    string LINK_REF = "";
                    #endregion
                    InsertMessagesSQL(MSG_ID,
                                    MSG_TYPE,
                                    MSG_DIRECTION,
                                    BRANCH_A,
                                    BRANCH_B,
                                    TRANS_DATE,
                                    VALUE_DATE,
                                    FIELD20,
                                    FIELD21,
                                    AMOUNT,
                                    CCY,
                                    FOREIGN_BANK,
                                    FOREIGN_BANK_NAME,
                                    PRIORITY,
                                    DELIVER_TYPE,
                                    CONTENT,
                                    STATUS,
                                    ACK_NAK,
                                    ACK_CONTENT,
                                    O_CONFIRM,
                                    LINK_REF);
                }
                _dtOra.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //Ham update lai trang thai ben co so du lieu SQL
        private void Update_status(int iMSG_ID,int iSTATUS,string pTABLE,string pDIRECTION)
        {
            try
            {
                string strUpdate = "";
                strUpdate = "UPDATE " + pTABLE + " SET STATUS = @iSTATUS where MSG_ID = @iMSG_ID and MSG_DIRECTION = @pDIRECTION";
                SqlParameter[] oraParas ={new SqlParameter("iMSG_ID", SqlDbType.Int,20),
                                         new SqlParameter("iSTATUS", SqlDbType.Int,10),                                        
                                         new SqlParameter("pDIRECTION", SqlDbType.VarChar,10)};
                oraParas[0].Value = iMSG_ID;
                oraParas[1].Value = iSTATUS;
                oraParas[2].Value = pDIRECTION;
                Lib.SQLExecuteNonQuery(strUpdate, CommandType.Text, oraParas);
            }
            catch (Exception ex)
            {
                throw (ex);                
            }
        }


        private void InsertMessages(int pMSG_ID,
                string pMSG_TYPE,
                string pMSG_DIRECTION,
                string pBRANCH_A,
                string pBRANCH_B,
                DateTime pTRANS_DATE,
                DateTime pVALUE_DATE,
                string pFIELD20,
                string pFIELD21,
                Double pAMOUNT,
                string pCCY,
                string pFOREIGN_BANK,
                string pFOREIGN_BANK_NAME,
                int pPRIORITY,
                string pDELIVER_TYPE,
                string pCONTENT,
                int pSTATUS,
                string pACK_NAK,
                string pACK_CONTENT,
                int pO_CONFIRM,
                string pLINK_REF)
        {
            try
            {
                int iStatusI = 0;
                OracleParameter[] oraParas ={new OracleParameter("pMSG_TYPE", OracleType.VarChar,6),
                                               new OracleParameter("pMSG_DIRECTION", OracleType.VarChar,10),
                                               new OracleParameter("pBRANCH_A", OracleType.VarChar,11),
                                               new OracleParameter("pBRANCH_B", OracleType.VarChar,11),
                                               new OracleParameter("pTRANS_DATE", OracleType.DateTime),
                                               new OracleParameter("pVALUE_DATE", OracleType.DateTime),
                                               new OracleParameter("pFIELD20", OracleType.VarChar,30),
                                               new OracleParameter("pFIELD21", OracleType.VarChar,30),
                                               new OracleParameter("pAMOUNT", OracleType.Double,19),                                       
                                               new OracleParameter("pCCY", OracleType.VarChar,4),
                                               new OracleParameter("pFOREIGN_BANK", OracleType.VarChar,11),
                                               new OracleParameter("pFOREIGN_BANK_NAME", OracleType.VarChar,140),
                                               new OracleParameter("pPRIORITY", OracleType.Number,10),
                                               new OracleParameter("pDELIVER_TYPE", OracleType.VarChar,5),
                                               new OracleParameter("pCONTENT", OracleType.VarChar,4000),
                                               new OracleParameter("pSTATUS", OracleType.Number,10),
                                               new OracleParameter("pACK_NAK", OracleType.VarChar,1),
                                               new OracleParameter("pACK_CONTENT", OracleType.VarChar,4000),
                                               new OracleParameter("pO_CONFIRM", OracleType.Number,2),
                                               new OracleParameter("pLINK_REF", OracleType.VarChar,20),
                                               new OracleParameter("pMSG_ID", OracleType.Number,20),
                                               new OracleParameter("vOut", OracleType.Number,1)};
                
                
                oraParas[0].Value = pMSG_TYPE;
                oraParas[1].Value = pMSG_DIRECTION;
                oraParas[2].Value = pBRANCH_A;
                oraParas[3].Value = pBRANCH_B;
                oraParas[4].Value = pTRANS_DATE;
                oraParas[5].Value = pVALUE_DATE;
                oraParas[6].Value = pFIELD20;
                oraParas[7].Value = pFIELD21;
                oraParas[8].Value = pAMOUNT;
                oraParas[9].Value = pCCY;
                oraParas[10].Value = pFOREIGN_BANK;
                oraParas[11].Value = pFOREIGN_BANK_NAME;
                oraParas[12].Value = pPRIORITY;
                oraParas[13].Value = pDELIVER_TYPE;
                oraParas[14].Value = pCONTENT;
                oraParas[15].Value = pSTATUS;
                oraParas[16].Value = pACK_NAK;
                oraParas[17].Value = pACK_CONTENT;
                oraParas[18].Value = pO_CONFIRM;
                oraParas[19].Value = pLINK_REF;
                oraParas[20].Value = pMSG_ID;
                oraParas[21].Direction = ParameterDirection.Output;

                Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_SWIFT_TR",CommandType.StoredProcedure, oraParas);
                iStatusI = Convert.ToInt32(oraParas[21].Value.ToString());
                Update_status(pMSG_ID, iStatusI, "SWIFT_TR", "SIBS-SWIFT");

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        private void InsertMessages_IBPS(int pMSG_ID,
                                    string pMSG_DIRECTION,
                                    string pSENDER,
                                    string pRECEIVER,
                                    DateTime pTRANS_DATE,
                                    string pTRANS_CODE,
                                    string pDESCRIPTION,
                                    Double pAMOUNT,
                                    string pCCY,
                                    string pIBPS_TRANS_NUM,
                                    string pSIBS_TRANS_NUM,
                                    string pCONTENT,
                                    int pSTATUS)
        {
            try
            {
                int iStatusI = 0;
                OracleParameter[] oraParas ={  new OracleParameter("pMSG_ID", OracleType.Number,20),
                                               new OracleParameter("pMSG_DIRECTION", OracleType.VarChar,10),
                                               new OracleParameter("pTRANS_CODE", OracleType.VarChar,10),
                                               new OracleParameter("pIBPS_TRANS_NUM", OracleType.VarChar,6),
                                               new OracleParameter("pSIBS_TRANS_NUM", OracleType.VarChar,16),
                                               new OracleParameter("pSENDER", OracleType.VarChar,10),
                                               new OracleParameter("pRECEIVER", OracleType.VarChar,10),                                       
                                               new OracleParameter("pTRANS_DATE", OracleType.DateTime,8),
                                               new OracleParameter("pAMOUNT", OracleType.Double,19),
                                               new OracleParameter("pCCY", OracleType.VarChar,4),
                                               new OracleParameter("pDESCRIPTION", OracleType.VarChar,210),
                                               new OracleParameter("pCONTENT", OracleType.Clob),
                                               new OracleParameter("pSTATUS", OracleType.Number,3),
                                               new OracleParameter("vOut", OracleType.Number,1)};
                oraParas[0].Value = pMSG_ID;
                oraParas[1].Value = pMSG_DIRECTION;
                oraParas[2].Value = pTRANS_CODE;
                oraParas[3].Value = pIBPS_TRANS_NUM;
                oraParas[4].Value = pSIBS_TRANS_NUM;
                oraParas[5].Value = pSENDER;
                oraParas[6].Value = pRECEIVER;
                oraParas[7].Value = pTRANS_DATE;
                oraParas[8].Value = pAMOUNT;
                oraParas[9].Value = pCCY;
                oraParas[10].Value = pDESCRIPTION;
                oraParas[11].Value = pCONTENT;
                oraParas[12].Value = pSTATUS;              
                oraParas[13].Direction = ParameterDirection.Output;

                Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_IBPS_TR", CommandType.StoredProcedure, oraParas);
                iStatusI = Convert.ToInt32(oraParas[13].Value.ToString());
                Update_status(pMSG_ID, iStatusI, "IBPS_TR", "SIBS-IBPS");

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        private void InsertMessagesSQL(int pMSG_ID,
                string pMSG_TYPE,
                string pMSG_DIRECTION,
                string pBRANCH_A,
                string pBRANCH_B,
                DateTime pTRANS_DATE,
                DateTime pVALUE_DATE,
                string pFIELD20,
                string pFIELD21,
                Double pAMOUNT,
                string pCCY,
                string pFOREIGN_BANK,
                string pFOREIGN_BANK_NAME,
                int pPRIORITY,
                string pDELIVER_TYPE,
                string pCONTENT,
                int pSTATUS,
                string pACK_NAK,
                string pACK_CONTENT,
                int pO_CONFIRM,
                string pLINK_REF)
        {
            try
            {
                int iStatusI = 0;
                String strSQL = " Insert into SWIFT_TR(MSG_TYPE,MSG_DIRECTION,BRANCH_A,BRANCH_B,TRANS_DATE,VALUE_DATE,FIELD20,FIELD21,AMOUNT,CCY,FOREIGN_BANK,";
                strSQL = strSQL + "FOREIGN_BANK_NAME,PRIORITY,DELIVER_TYPE,CONTENT,STATUS,ACK_NAK,ACK_CONTENT,O_CONFIRM) values(";
                strSQL = strSQL + "@pMSG_TYPE,@pMSG_DIRECTION,@pBRANCH_A,@pBRANCH_B,@pTRANS_DATE,@pVALUE_DATE,@pFIELD20,@pFIELD21,@pAMOUNT,@pCCY,@pFOREIGN_BANK,";
                strSQL = strSQL + "@pFOREIGN_BANK_NAME,@pPRIORITY,@pDELIVER_TYPE,@pCONTENT,@pSTATUS,@pACK_NAK,@pACK_CONTENT,@pO_CONFIRM)";

                SqlParameter[] oraParas ={new SqlParameter("pMSG_TYPE", SqlDbType.VarChar,6),
                                               new SqlParameter("pMSG_DIRECTION", SqlDbType.VarChar,10),
                                               new SqlParameter("pBRANCH_A", SqlDbType.VarChar,11),
                                               new SqlParameter("pBRANCH_B", SqlDbType.VarChar,11),
                                               new SqlParameter("pTRANS_DATE", SqlDbType.DateTime,25),
                                               new SqlParameter("pVALUE_DATE", SqlDbType.DateTime,25),
                                               new SqlParameter("pFIELD20", SqlDbType.VarChar,30),
                                               new SqlParameter("pFIELD21", SqlDbType.VarChar,30),
                                               new SqlParameter("pAMOUNT", SqlDbType.Decimal,19),                                       
                                               new SqlParameter("pCCY", SqlDbType.VarChar,4),
                                               new SqlParameter("pFOREIGN_BANK", SqlDbType.VarChar,11),
                                               new SqlParameter("pFOREIGN_BANK_NAME", SqlDbType.VarChar,140),
                                               new SqlParameter("pPRIORITY", SqlDbType.Int,10),
                                               new SqlParameter("pDELIVER_TYPE", SqlDbType.VarChar,5),
                                               new SqlParameter("pCONTENT", SqlDbType.VarChar,4000),
                                               new SqlParameter("pSTATUS", SqlDbType.Int,10),
                                               new SqlParameter("pACK_NAK", SqlDbType.VarChar,1),
                                               new SqlParameter("pACK_CONTENT", SqlDbType.VarChar,4000),
                                               new SqlParameter("pO_CONFIRM", SqlDbType.Int,2)};


                oraParas[0].Value = pMSG_TYPE;
                oraParas[1].Value = pMSG_DIRECTION;
                oraParas[2].Value = pBRANCH_A;
                oraParas[3].Value = pBRANCH_B;
                oraParas[4].Value = pTRANS_DATE;
                if ((pVALUE_DATE.Year < 1900) || (pVALUE_DATE.Year < 3000))
                {
                    oraParas[5].Value = pTRANS_DATE;
                }
                else
                {
                    oraParas[5].Value = pVALUE_DATE;
                }
                
                oraParas[6].Value = pFIELD20;
                oraParas[7].Value = pFIELD21;
                oraParas[8].Value = pAMOUNT;
                oraParas[9].Value = pCCY;
                oraParas[10].Value = pFOREIGN_BANK;
                oraParas[11].Value = pFOREIGN_BANK_NAME;
                oraParas[12].Value = pPRIORITY;
                oraParas[13].Value = pDELIVER_TYPE;
                oraParas[14].Value = pCONTENT;
                oraParas[15].Value = pSTATUS;
                oraParas[16].Value = pACK_NAK;
                oraParas[17].Value = pACK_CONTENT;
                oraParas[18].Value = pO_CONFIRM;

                iStatusI = Lib.SQLExecuteNonQueryText(strSQL, CommandType.Text, oraParas);                
                Update_Ora(pMSG_ID, iStatusI);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Ham update lai trang thai ben co so du lieu SQL
        private void Update_Ora(int iMSG_ID, int iSTATUS)
        {
            try
            {
                string strUpdate = "";
                strUpdate = "UPDATE SWIFT_TR SET STATUS = :pSTATUS where MSG_ID = :pMSG_ID ";
                OracleParameter[] oraParas ={new OracleParameter("pMSG_ID", OracleType.Number,20),
                                           new OracleParameter("pSTATUS", OracleType.Number,10)};
                oraParas[0].Value = iMSG_ID;
                oraParas[1].Value = iSTATUS;
                Lib.ExecuteNonQuery(strUpdate, CommandType.Text, oraParas);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
