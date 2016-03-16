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
//using BR.BRLib;
using BR.DataAccess;


//' =============================================



//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class MSG_EXCELDP
    {
        OracleConnection oraConn = new OracleConnection();
        Connect_Process conn = new Connect_Process();
        //private static OracleConnection m_orcConn = new OracleConnection();//Khoi connection
       // private static OracleDataReader myReader;
        //private static OracleDataAdapter myAdapter;
        private static BR.DataAccess.Connect_Process connect = new Connect_Process();
        public MSG_EXCELDP()
        {
        }
        public static MSG_EXCELDP Instance()
        {
            return new MSG_EXCELDP();
        }

        public int AddMSG_EXCEL(MSG_EXCELInfo objTable)
        {
            string strSql = "Insert into MSG_XLS (GWTYPE,MSG_TYPE,XLSCOL,FIELD_NAME,FIELD_DESCRIPTION, ROW_BEGIN,MAX_ROW,MAX_LENGTH,CHK,DATA_TYPE,SWIFT_FIELD_NAME,MSG_DIRECTION,DEFAULT_VALUE,ROW_NUM,PART_NUM) values ";
            strSql = strSql + "(:pGWTYPE,:pMSG_TYPE,:pXLSCOL,:pFIELD_NAME,:pFIELD_DECRIPTION,:pROW_BEGIN,:pMAX_ROW,:pMAX_LENGTH,:pCHK,:pDATA_TYPE,:pSWIFT_FIELD_NAME,:pMSG_DIRECTION,:pDEFAULT_VALUE,:pROW_NUM,:pPART_NUM)";
            OracleParameter[] oraParam ={  new OracleParameter("pGWTYPE",OracleType.NVarChar,10),
                                           new OracleParameter("pMSG_TYPE",OracleType.NVarChar,15),
                                           new OracleParameter("pXLSCOL",OracleType.NVarChar,3),
                                           new OracleParameter("pFIELD_NAME",OracleType.NVarChar,15),
                                           new OracleParameter("pFIELD_DECRIPTION",OracleType.NVarChar,70),
                                           new OracleParameter("pCHK",OracleType.Char,1),
                                           new OracleParameter("pROW_BEGIN",OracleType.Int32,10),
                                           new OracleParameter("pMAX_ROW",OracleType.Int32,10),
                                           new OracleParameter("pMAX_LENGTH",OracleType.Int32,10),
                                           new OracleParameter("pDATA_TYPE",OracleType.NVarChar,10),
                                           new OracleParameter("pSWIFT_FIELD_NAME",OracleType.NVarChar,10),
                                           new OracleParameter("pMSG_DIRECTION",OracleType.NVarChar,10),
                                           new OracleParameter("pDEFAULT_VALUE",OracleType.NVarChar,10),
                                           new OracleParameter("pROW_NUM",OracleType.Int32,10),
                                           new OracleParameter("pPART_NUM",OracleType.Int32,10)};

            oraParam[0].Value = objTable.GWTYPE;
            oraParam[1].Value = objTable.MSG_TYPE;
            oraParam[2].Value = objTable.XLSCOL;
            oraParam[3].Value = objTable.FIELD_NAME;
            oraParam[4].Value = objTable.FIELD_DECRIPTION;
            oraParam[5].Value = objTable.CHK;
            oraParam[6].Value = objTable.ROW_BEGIN;
            oraParam[7].Value = objTable.MAX_ROW;
            oraParam[8].Value = objTable.MAX_LENGTH;
            oraParam[9].Value = objTable.DATA_TYPE;
            oraParam[10].Value = objTable.SWIFT_FIELD_NAME;
            oraParam[11].Value = objTable.MSG_DIRECTION;
            oraParam[12].Value = objTable.DEFAULT_VALUE;
            oraParam[13].Value = objTable.ROW_NUM;
            oraParam[14].Value = objTable.PART_NUM;
            try
            {

                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1;
            }
        }

        public int UpdateMSG_EXCEL(MSG_EXCELInfo objTable)
        {
            string strSql = "Update MSG_XLS set GWTYPE = :pGWTYPE, MSG_TYPE = :pMSG_TYPE, XLSCOL = :pXLSCOL, ";
            strSql = strSql + "FIELD_NAME = :pFIELD_NAME,FIELD_DESCRIPTION = :pFIELD_DECRIPTION,CHK = :pCHK, ROW_BEGIN = :pROW_BEGIN, ";
            strSql = strSql + "MAX_ROW = :pMAX_ROW,MAX_LENGTH=:pMAX_LENGTH,DATA_TYPE = :pDATA_TYPE, SWIFT_FIELD_NAME = :pSWIFT_FIELD_NAME, ";
            strSql = strSql + "MSG_DIRECTION = :pMSG_DIRECTION,DEFAULT_VALUE = :pDEFAULT_VALUE, ROW_NUM = :pROW_NUM, PART_NUM = :pPART_NUM ";
            strSql = strSql + "where FIELD_ID = :pFIELD_ID";
            OracleParameter[] oraParam ={  new OracleParameter("pFIELD_ID",OracleType.Int16,20),
                                           new OracleParameter("pGWTYPE",OracleType.NVarChar,10),
                                           new OracleParameter("pMSG_TYPE",OracleType.NVarChar,15),
                                           new OracleParameter("pXLSCOL",OracleType.NVarChar,3),
                                           new OracleParameter("pFIELD_NAME",OracleType.NVarChar,15),
                                           new OracleParameter("pFIELD_DECRIPTION",OracleType.NVarChar,70),
                                           new OracleParameter("pCHK",OracleType.Char,1),
                                           new OracleParameter("pROW_BEGIN",OracleType.Int32,10),
                                           new OracleParameter("pMAX_ROW",OracleType.Int32,10),
                                           new OracleParameter("pMAX_LENGTH",OracleType.Int32,10),
                                           new OracleParameter("pDATA_TYPE",OracleType.NVarChar,10),
                                           new OracleParameter("pSWIFT_FIELD_NAME",OracleType.NVarChar,10),
                                           new OracleParameter("pMSG_DIRECTION",OracleType.NVarChar,10),
                                           new OracleParameter("pDEFAULT_VALUE",OracleType.NVarChar,10),
                                           new OracleParameter("pROW_NUM",OracleType.Int32,10),
                                           new OracleParameter("pPART_NUM",OracleType.Int32,10)};

            oraParam[0].Value = objTable.FIELD_ID;
            oraParam[1].Value = objTable.GWTYPE;
            oraParam[2].Value = objTable.MSG_TYPE;
            oraParam[3].Value = objTable.XLSCOL;
            oraParam[4].Value = objTable.FIELD_NAME;
            oraParam[5].Value = objTable.FIELD_DECRIPTION;
            oraParam[6].Value = objTable.CHK;
            oraParam[7].Value = objTable.ROW_BEGIN;
            oraParam[8].Value = objTable.MAX_ROW;
            oraParam[9].Value = objTable.MAX_LENGTH;
            oraParam[10].Value = objTable.DATA_TYPE;
            oraParam[11].Value = objTable.SWIFT_FIELD_NAME;
            oraParam[12].Value = objTable.MSG_DIRECTION;
            oraParam[13].Value = objTable.DEFAULT_VALUE;
            oraParam[14].Value = objTable.ROW_NUM;
            oraParam[15].Value = objTable.PART_NUM;
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParam);
            }
            catch 
            {
                return -1;
            }
        }

        public int DeleteMSG_EXCEL(int iFieldID)
        {
            string strSql = "Delete from MSG_XLS where FIELD_ID=:pFIELD_ID";
            OracleParameter[] oraParm = { new OracleParameter("pFIELD_ID", OracleType.Int16, 20)};
            oraParm[0].Value = iFieldID;

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return -1;
                    return DataAcess.ExecuteNonQuery(oraConn, CommandType.Text, strSql, oraParm);
            }
            catch 
            {
                return -1;
            }
        }

        public DataSet GetMSG_EXCEL_MsgType(string chrMsgType)
        {
            DataSet datDs = new DataSet();
            //string strSql = "Select a.FIELD_ID,GWTYPE,b.CONTENT MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DECRIPTION,c.CONTENT CHK,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,d.CONTENT DATA_TYPE";
            //strSql = strSql + " a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM from MSG_XLS a, ";
            //       strSql = strSql + "(select * from ALLCODE where cdname = 'MSGCHK') c, (select * from ALLCODE where cdname = 'DATATYPE') d where a.MSG_TYPE = trim('" + chrMsgType + "')";
            //       strSql = strSql + "and a.CHK = c.CDVAL and a.DATA_TYPE = d.CDVAL ";

            string strSql = "Select a.FIELD_ID,a.GWTYPE,a.MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DESCRIPTION,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,c.CONTENT CHK,d.CONTENT DATA_TYPE,a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM ";
            strSql = strSql + "from MSG_XLS a, (select * from ALLCODE where cdname = 'MSGCHK') c, ";
            strSql = strSql + "(select * from ALLCODE where cdname = 'DATATYPE') d where a.MSG_TYPE = trim('" + chrMsgType + "') and ";
            strSql = strSql + "trim(a.CHK) = trim(c.CDVAL) and trim(a.DATA_TYPE) = trim(d.CDVAL) ";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetMSG_EXCEL_GWType(string chrGWType)
        {
            DataSet datDs = new DataSet();
            //string strSql = "Select a.FIELD_ID,a.GWTYPE,b.CONTENT MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DECRIPTION,c.CONTENT CHK,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,d.CONTENT DATA_TYPE, ";
            //strSql = strSql + "a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM from MSG_XLS a, (select * from ALLCODE where cdname = 'GWTYPE') b, (select * from ALLCODE where cdname = 'MSGCHK') c, ";
            //strSql = strSql + "(select * from ALLCODE where cdname = 'DATATYPE') d where a.MSG_TYPE = trim('" + chrGWType + "') and trim(a.GWTYPE) = trim(b.CDVAL) ";
            //strSql = strSql + "and trim(a.CHK) = trim(c.CDVAL) and trim(a.DATA_TYPE) = trim(d.CDVAL) ";
            string strSql = "Select a.FIELD_ID,a.GWTYPE,a.MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DESCRIPTION,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,c.CONTENT CHK,d.CONTENT DATA_TYPE,a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM ";
            strSql = strSql + "from MSG_XLS a, (select * from ALLCODE where upper(trim(cdname)) = 'MSGCHK') c, ";
            strSql = strSql + "(select * from ALLCODE where upper(trim(cdname)) = 'DATATYPE') d ";
            if (chrGWType== "ALL")
                strSql = strSql + "where upper(trim(a.CHK)) = upper(trim(c.CDVAL)) and upper(trim(a.DATA_TYPE)) = upper(trim(d.CDVAL)) ";
            else
                strSql = strSql + "where upper(a.GWTYPE) like upper(trim('%" + chrGWType + "%')) and upper(trim(a.CHK)) = upper(trim(c.CDVAL)) and upper(trim(a.DATA_TYPE)) = upper(trim(d.CDVAL)) ";

            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch 
            {
                return null;
            }
        }


        public DataSet GetMSG_EXCELSearch(string strGWType,string strMsgType)
        {
            DataSet datDs = new DataSet();            
            string strSql = "Select a.FIELD_ID,a.GWTYPE,a.MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DESCRIPTION,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,c.CONTENT CHK,d.CONTENT DATA_TYPE,a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM ";
            strSql = strSql + "from MSG_XLS a, (select * from ALLCODE where upper(trim(cdname)) = 'MSGCHK') c, (select * from ALLCODE where upper(trim(cdname)) = 'DATATYPE') d ";
            if (strGWType == "ALL")
                strSql = strSql + "where upper(trim(a.MSG_TYPE)) like upper(trim('%" + strMsgType + "%')) and upper(trim(a.CHK)) = upper(trim(c.CDVAL)) and upper(trim(a.DATA_TYPE)) = upper(trim(d.CDVAL)) ";
            else
                strSql = strSql + "where upper(trim(a.MSG_TYPE)) like upper(trim('%" + strMsgType + "%')) and upper(trim(a.GWTYPE)) like trim('" + strGWType + "') and upper(trim(a.CHK)) = upper(trim(c.CDVAL)) and upper(trim(a.DATA_TYPE)) = upper(trim(d.CDVAL)) ";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataSet EXCELSearch(string strGWType, string strMsgType,string strDiretion)
        {
            DataSet datDs = new DataSet();
            string strSql = "Select a.FIELD_ID,a.GWTYPE,a.MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DESCRIPTION,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,c.CONTENT CHK,d.CONTENT DATA_TYPE,a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM ";
            strSql = strSql + "from MSG_XLS a, (select * from ALLCODE where upper(trim(cdname)) = 'MSGCHK') c, (select * from ALLCODE where upper(trim(cdname)) = 'DATATYPE') d ";
            if (strGWType == "ALL")
                strSql = strSql + "where upper(trim(a.MSG_TYPE)) like upper(trim('%" + strMsgType + "%')) and upper(trim(a.CHK)) = upper(trim(c.CDVAL)) and upper(trim(a.DATA_TYPE)) = upper(trim(d.CDVAL)) ";
            else
                strSql = strSql + "where upper(trim(a.MSG_TYPE)) like upper(trim('%" + strMsgType + "%')) and upper(trim(a.GWTYPE)) like trim('" + strGWType + "') and upper(trim(a.CHK)) = upper(trim(c.CDVAL)) and upper(trim(a.DATA_TYPE)) = upper(trim(d.CDVAL)) ";
            if (strDiretion != "ALL")
                strSql = strSql + " and upper(trim(a.MSG_DIRECTION)) = '" + strDiretion + "' ";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public DataSet GetMSG_EXCEL()
        {
            DataSet datDs = new DataSet();
            //string strSql = "select a.FIELD_ID,GWTYPE,b.CONTENT MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DECRIPTION,a.CONTENT CHK,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH from MSG_EXCEL a";
            string strSql = "Select a.FIELD_ID,a.GWTYPE,a.MSG_TYPE,a.XLSCOL,a.FIELD_NAME,a.FIELD_DESCRIPTION,a.ROW_BEGIN,MAX_ROW,a.MAX_LENGTH,c.CONTENT CHK,d.CONTENT DATA_TYPE,a.SWIFT_FIELD_NAME,a.MSG_DIRECTION,a.DEFAULT_VALUE,a.ROW_NUM,a.PART_NUM ";
            strSql = strSql + "from MSG_XLS a, (select * from ALLCODE where cdname = 'MSGCHK') c, ";
            strSql = strSql + "(select * from ALLCODE where cdname = 'DATATYPE') d where ";
            strSql = strSql + "trim(a.CHK) = trim(c.CDVAL) and trim(a.DATA_TYPE) = trim(d.CDVAL) ";
            try
            {
                oraConn = conn.Connect();
                if (oraConn == null)
                    return null;

                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSql, null);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }


}
