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
    public class ALLCODEDP
    {
        private OracleConnection oraConn = new OracleConnection();
        private Connect_Process connect = new Connect_Process();
        //private OracleConnection cnn=new OracleConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\book1.xls;Extended Properties='Excel 8.0'");
        
        public ALLCODEDP()
        {
        }
        public static ALLCODEDP Instance()
        {
            return new ALLCODEDP();
        }

        //lay trang thai status va tad cua forward
        public DataSet STATUS_TAD(string pTELLERID)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                { return null; }
                OracleParameter[] orapra ={new OracleParameter("rSTATUS",OracleType.Cursor),
                                         new OracleParameter("rTAD_CLOSE",OracleType.Cursor),
                                         new OracleParameter("rTAD_ACTIVE",OracleType.Cursor),
                                         new OracleParameter("rTELLERID",OracleType.NVarChar,8)
                                        };

                orapra[0].Direction = ParameterDirection.Output;
                orapra[1].Direction = ParameterDirection.Output;
                orapra[2].Direction = ParameterDirection.Output;
                orapra[3].Value = pTELLERID;
                
                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.STATUS_TAD_HO", orapra);
                _ds.Tables[0].TableName = "STATUS";
                _ds.Tables[1].TableName = "TAD_CLOSE";
                _ds.Tables[2].TableName = "TAD_ACTIVE";                
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }

        public DataSet IBPS_STATUS()
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                { return null;}
                OracleParameter[] orapra ={new OracleParameter("rERROR_CODE",OracleType.Cursor),
                                         new OracleParameter("rSTATUS",OracleType.Cursor),
                                         new OracleParameter("rDEPARTMENT",OracleType.Cursor),
                                         new OracleParameter("rMSGDIRECTION",OracleType.Cursor),
                                         new OracleParameter("rFWSTS",OracleType.Cursor),
                                         new OracleParameter("rTAD",OracleType.Cursor),
                                         new OracleParameter("rCURRENCY",OracleType.Cursor),
                                         new OracleParameter("rPRINT_STS",OracleType.Cursor),
                                         new OracleParameter("rMSG_SRC",OracleType.Cursor)};
              
                orapra[0].Direction = ParameterDirection.Output;
                orapra[1].Direction = ParameterDirection.Output;
                orapra[2].Direction = ParameterDirection.Output;
                orapra[3].Direction = ParameterDirection.Output;
                orapra[4].Direction = ParameterDirection.Output;
                orapra[5].Direction = ParameterDirection.Output;
                orapra[6].Direction = ParameterDirection.Output;
                orapra[7].Direction = ParameterDirection.Output;
                orapra[8].Direction = ParameterDirection.Output;
                
                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.IBPS_STATUS", orapra);
                _ds.Tables[0].TableName = "ERROR_CODE";
                _ds.Tables[1].TableName = "STATUS";
                _ds.Tables[2].TableName = "DEPARTMENT";
                _ds.Tables[3].TableName = "MSGDIRECTION";
                _ds.Tables[4].TableName = "FWSTS";
                _ds.Tables[5].TableName = "TAD";
                _ds.Tables[6].TableName = "CURRENCY";
                _ds.Tables[7].TableName = "PRINT_STS";
                _ds.Tables[8].TableName = "MSG_SRC";
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }

        //lay cac trang thai statsu cho ttsp
        public DataSet TTSP_STATUS()
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                { return null; }
                OracleParameter[] orapra ={new OracleParameter("rERROR_CODE",OracleType.Cursor),
                                         new OracleParameter("rSTATUS",OracleType.Cursor),
                                         new OracleParameter("rDEPARTMENT",OracleType.Cursor),
                                         new OracleParameter("rMSGDIRECTION",OracleType.Cursor),                                                                               
                                         new OracleParameter("rCURRENCY",OracleType.Cursor)};

                orapra[0].Direction = ParameterDirection.Output;
                orapra[1].Direction = ParameterDirection.Output;
                orapra[2].Direction = ParameterDirection.Output;
                orapra[3].Direction = ParameterDirection.Output;
                orapra[4].Direction = ParameterDirection.Output;               

                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.TTSP_STATUS", orapra);
                _ds.Tables[0].TableName = "ERROR_CODE";
                _ds.Tables[1].TableName = "STATUS";
                _ds.Tables[2].TableName = "DEPARTMENT";
                _ds.Tables[3].TableName = "MSGDIRECTION";                
                _ds.Tables[4].TableName = "CURRENCY";
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }

        public DataSet VCB_STATUS()
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                { return null; }
                OracleParameter[] orapra ={new OracleParameter("rERROR_CODE",OracleType.Cursor),
                                         new OracleParameter("rSTATUS",OracleType.Cursor),
                                         new OracleParameter("rDEPARTMENT",OracleType.Cursor),
                                         new OracleParameter("rMSGDIRECTION",OracleType.Cursor),
                                         new OracleParameter("rMSG_SRC",OracleType.Cursor),                                         
                                         new OracleParameter("rCURRENCY",OracleType.Cursor),
                                         new OracleParameter("rPRINT_STS",OracleType.Cursor)};

                orapra[0].Direction = ParameterDirection.Output;
                orapra[1].Direction = ParameterDirection.Output;
                orapra[2].Direction = ParameterDirection.Output;
                orapra[3].Direction = ParameterDirection.Output;
                orapra[4].Direction = ParameterDirection.Output;
                orapra[5].Direction = ParameterDirection.Output;
                orapra[6].Direction = ParameterDirection.Output;     

                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.VCB_STATUS", orapra);
                _ds.Tables[0].TableName = "ERROR_CODE";
                _ds.Tables[1].TableName = "STATUS";
                _ds.Tables[2].TableName = "DEPARTMENT";
                _ds.Tables[3].TableName = "MSGDIRECTION";
                _ds.Tables[4].TableName = "MSG_SRC";                
                _ds.Tables[5].TableName = "CURRENCY";
                _ds.Tables[6].TableName = "PRINT_STS";
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }


        public DataSet GET_ALL_ALL_CODE(string sSWMSTS,string sPROCESSSTS,string sDEPARTMENT,
            string sMSG_SRC, string sMSG_DIRECTION, string sGETYPE, string sMSG_DIREC_S, string sMSG_DIREC_P)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pSWMSTS",OracleType.VarChar,15),
                                         new OracleParameter("pPROCESSSTS",OracleType.VarChar,15),
                                         new OracleParameter("pDEPARTMENT",OracleType.VarChar,15),
                                         new OracleParameter("pMSG_SRC",OracleType.VarChar,15),
                                         new OracleParameter("pMSG_DIRECTION",OracleType.VarChar,15),
                                         new OracleParameter("pGETYPE",OracleType.VarChar,10),
                                         new OracleParameter("pMSG_DIREC_S",OracleType.VarChar,50),
                                         new OracleParameter("pMSG_DIREC_P",OracleType.VarChar,50),
                                         //----------------------------------------------------------
                                         new OracleParameter("rSWMSTS",OracleType.Cursor),
                                         new OracleParameter("rPROCESSSTS",OracleType.Cursor),
                                         new OracleParameter("rERROR_CODE",OracleType.Cursor),
                                         new OracleParameter("rSTATUS",OracleType.Cursor),
                                         new OracleParameter("rDEPARTMENT",OracleType.Cursor),
                                         new OracleParameter("rCCYCD",OracleType.Cursor),
                                         new OracleParameter("rMSG_SRC",OracleType.Cursor),
                                         new OracleParameter("rMSG_DIRECTION",OracleType.Cursor),
                                         new OracleParameter("rPRINT_STS",OracleType.Cursor)};
                orapra[0].Value = sSWMSTS;
                orapra[1].Value = sPROCESSSTS;
                orapra[2].Value = sDEPARTMENT;
                orapra[3].Value = sMSG_SRC;
                orapra[4].Value = sMSG_DIRECTION;
                orapra[5].Value = sGETYPE;
                orapra[6].Value = sMSG_DIREC_S;
                orapra[7].Value = sMSG_DIREC_P;
                //---------------------------------
                orapra[8].Direction = ParameterDirection.Output;
                orapra[9].Direction = ParameterDirection.Output;
                orapra[10].Direction = ParameterDirection.Output;                
                orapra[11].Direction = ParameterDirection.Output;
                orapra[12].Direction = ParameterDirection.Output;
                orapra[13].Direction = ParameterDirection.Output;
                orapra[14].Direction = ParameterDirection.Output;
                orapra[15].Direction = ParameterDirection.Output;
                orapra[16].Direction = ParameterDirection.Output;
                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.GET_STATUS", orapra);
                _ds.Tables[0].TableName = "SWMSTS";
                _ds.Tables[1].TableName = "PROCESSSTS";
                _ds.Tables[2].TableName = "ERROR_CODE";
                _ds.Tables[3].TableName = "STATUS";
                _ds.Tables[4].TableName = "DEPARTMENT";
                _ds.Tables[5].TableName = "CCYCD";
                _ds.Tables[6].TableName = "MSG_SRC";
                _ds.Tables[7].TableName = "MSG_DIRECTION";
                _ds.Tables[8].TableName = "PRINT_STS";
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch 
            {
                return null;
            }
        }

        public DataSet SWMSTS_PROCESSSTS(string sSWMSTS, string sPROCESSSTS)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pSWMSTS",OracleType.VarChar,15),
                                         new OracleParameter("pPROCESSSTS",OracleType.VarChar,15), 
                                         new OracleParameter("rSWMSTS",OracleType.Cursor),
                                         new OracleParameter("rPROCESSSTS",OracleType.Cursor)};
                orapra[0].Value = sSWMSTS;
                orapra[1].Value = sPROCESSSTS;  
                orapra[2].Direction = ParameterDirection.Output;
                orapra[3].Direction = ParameterDirection.Output;

                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.GET_SWMSTS_PROCESSSTS", orapra);
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }

        //ham lay trang thai Processsts cua xu ly thu cong voi menh de where truyen vao
        public DataTable PROCESSTS_STATUS(string pWhere, out DataTable _dt)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return _dt = null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pWHERE",OracleType.VarChar,2000),
                                              new OracleParameter("rPROCESSSTS",OracleType.Cursor)
                                         };
                orapra[0].Value = pWhere;
                orapra[1].Direction = ParameterDirection.Output;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.PROCESSSTS_STATUS", orapra).Tables[0];
                _dt.TableName = "PROCESSSTS";
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return _dt = null;
            }
        }

        public DataTable RESEND_STATUS(string pWhere, out DataTable _dt)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return _dt = null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pWHERE",OracleType.VarChar,2000),
                                              new OracleParameter("rSTATUS",OracleType.Cursor)
                                         };
                orapra[0].Value = pWhere;
                orapra[1].Direction = ParameterDirection.Output;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.RESEND_STATUS", orapra).Tables[0];
                _dt.TableName = "STATUS";
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return _dt = null;
            }
        }


        //ham lay trang thai Swmsts cua xu ly thu cong voi menh de where truyen vao
        public DataTable SWMSTS_STATUS(string pWhere, out DataTable _dt)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return _dt = null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pWHERE",OracleType.VarChar,2000),
                                              new OracleParameter("rSWMSTS",OracleType.Cursor)
                                         };
                orapra[0].Value = pWhere;
                orapra[1].Direction = ParameterDirection.Output;

                _dt = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.SWMSTS_STATUS", orapra).Tables[0];
                _dt.TableName = "SWMSTS";
                oraConn.Close();
                oraConn.Dispose();
                return _dt;
            }
            catch
            {
                return _dt = null;
            }
        }


        public DataSet SWMSTS_PROCESSSTS(string sSWMSTS, string sPROCESSSTS, string sDIRECTION)
        {
            try
            {
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }
                OracleParameter[] orapra ={ new OracleParameter("pSWMSTS",OracleType.VarChar,15),
                                         new OracleParameter("pPROCESSSTS",OracleType.VarChar,15),                                         
                                         new OracleParameter("pDIRECTION",OracleType.VarChar,50),
                                         //----------------------------------------------------------
                                         new OracleParameter("rSWMSTS",OracleType.Cursor),
                                         new OracleParameter("rPROCESSSTS",OracleType.Cursor)};
                orapra[0].Value = sSWMSTS;
                orapra[1].Value = sPROCESSSTS;
                orapra[2].Value = sDIRECTION;                
                //---------------------------------
                orapra[3].Direction = ParameterDirection.Output;
                orapra[4].Direction = ParameterDirection.Output;
                
                DataSet _ds = new DataSet();
                _ds = DataAcess.ExecuteDataset(oraConn, CommandType.StoredProcedure, "GW_PK_STATUS.SWMSTS_PROCESSSTS_DIRECTION", orapra);
                oraConn.Close();
                oraConn.Dispose();
                return _ds;
            }
            catch
            {
                return null;
            }
        }


        public DataTable GetALLCODE(string pCDNAME, string strGWtype)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select a.cdval,a.id,a.content,a.gwtype from allcode a where upper(Trim(a.cdname))='" + pCDNAME.Trim().ToUpper();
                   strSQL =  strSQL + "' AND Trim(a.gwtype) ='" + strGWtype + "'" + "order by a.content";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }

        }

       
        public DataTable GetALLCODE_code1(string pCONTENT, string pCDNAME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select a.cdval from Allcode a where Trim(a.content)='" + pCONTENT + "' and Trim(a.cdname)='" + pCDNAME + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch
            {
                return null;
            }
        }
       
        public DataTable GetALLCODE_SWIFT(string pCONTENT, string pCDNAME,string pGWTPYE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select a.cdval from Allcode a where Trim(a.content)='" + pCONTENT + "' and Trim(a.cdname)='" + pCDNAME + "' and trim(a.gwtype)='" + pGWTPYE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }

        public DataTable GetALLCODE_DATA1(string pCONTENT, string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "select CDVAL from Allcode where Trim(CONTENT)='" + pCONTENT + "' and Trim(GWTYPE)='" + pGWTYPE + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];

            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetALLCODE_DATA(string pCDNAME, string pCDVAL)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }

            string strSQL = "select a.content from Allcode a where Trim(a.cdval)='" + pCDVAL + "' and  Trim(a.cdname)='" + pCDNAME + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }



        public DataTable GetALLCODE(string pCDNAME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select STATUS,NAME from STATUS a where trim(GWTYPE) ='" + pCDNAME + "' ";
            //string strSQL = "select a.cdval,a.id,a.content,a.gwtype from allcode a where trim(a.cdname) ='" + pCDNAME + "' order by a.content asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetALLCODE_D(string pCDNAME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select a.cdval,a.id,a.content,a.gwtype from allcode a where a.cdname='" + pCDNAME + "' order by a.content asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetALLCODE2(string pCDNAME, string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select a.cdval,a.id,a.content,a.gwtype,a.description from allcode a where Trim(a.cdname)='" + pCDNAME + "' and trim(a.gwtype) ='" + pGWTYPE + "' order by a.content asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

        public DataSet GetALLCODE21(string pCDNAME, string pGWTYPE)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select a.cdval,a.id,a.content,a.gwtype,a.description from allcode a where Trim(a.cdname)='" + pCDNAME + "' and trim(a.gwtype) ='" + pGWTYPE + "' and  trim(content)='TFBr-Parameter' order by a.content asc";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }

       
        //lay thong tin cua Job
        public DataSet GetJob(string pOWNER)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select DSJ.JOB_NAME,DSJ.ENABLED from DBA_SCHEDULER_JOBS DSJ Where Trim(DSJ.OWNER)='" + pOWNER.Trim() + "'  order by DSJ.JOB_NAME ASC";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        //Tim kiem theo ten Job
        public DataSet SearchJob(string pOWNER, string pJobname)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select DSJ.JOB_NAME,DSJ.ENABLED from DBA_SCHEDULER_JOBS DSJ Where Trim(DSJ.OWNER)='" + pOWNER + "' and Trim(DSJ.JOB_NAME)='" + pJobname + "'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet GetQueue(string pOWNER)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            string strSQL = "select Q.name,Q.ENQUEUE_ENABLED,Q.DEQUEUE_ENABLED from All_Queues Q where Q.OWNER='" + pOWNER + "' and Trim(Q.QUEUE_TYPE)='NORMAL_QUEUE'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        public DataSet SearchQueue(string pOWNER, string pQUEUENAME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select Q.name,Q.ENQUEUE_ENABLED,Q.DEQUEUE_ENABLED from All_Queues Q where Q.OWNER='" + pOWNER + "' and Q.NAME='" + pQUEUENAME + "' and Trim(Q.QUEUE_TYPE)='NORMAL_QUEUE'";

            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);
            }
            catch 
            {
                return null;
            }
        }
        //start Job
        public int Start_Job(string pJob_Name)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pJobName",OracleType.NVarChar,50)
                                               };
                oraParas[0].Value = pJob_Name;
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_START_STOP_JOB.Start_Job", oraParas);
                }
            }
            catch 
            {
                return -1;
            }
            return 1;
        }
        //stop Job
        public int Stop_Job(string pJob_Name)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pJobName",OracleType.VarChar,50)
                                               };
                oraParas[0].Value = pJob_Name;
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_START_STOP_JOB.Stop_Job", oraParas);
                }
            }
            catch 
            {
                return -1;
            }
            return 1;
        }
        public int Stop_Queue(string pQueueName)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pQueueName",OracleType.VarChar,50)
                                               };
                oraParas[0].Value = pQueueName;
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_START_STOP_QUEUE.Stop_Queue", oraParas);
                }
            }
            catch 
            {
                return -1;
            }
            return 1;
        }
        public int Start_Queue(string pQueueName)
        {
            int iResult = 0;
            try
            {
                OracleParameter[] oraParas ={new OracleParameter("pQueueName",OracleType.VarChar,100)
                                               };
                oraParas[0].Value = pQueueName;
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return -1;
                }
                else
                {
                    iResult = DataAcess.ExecuteNonQuery(oraConn, CommandType.StoredProcedure, "GW_PK_START_STOP_QUEUE.Start_Queue", oraParas);
                }
            }
            catch 
            {
                return -1;
            }
            return 1;
        }//select * from User_Queues
        public DataSet GetQ()
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTable = new DataTable();
            string strSQL = "select u.name,u.QUEUE_TABLE,u.QID,u.QUEUE_TYPE,u.max_retries,u.retry_delay,";
            strSQL = strSQL + "  u.ENQUEUE_ENABLED,u.DEQUEUE_ENABLED,u.RETENTION,u.USER_COMMENT from User_Queues u where Trim(u.QUEUE_TYPE)='NORMAL_QUEUE'";
            
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }
        //Lay truong Description cua trang thai STATUS trong bang Allcode ung voi 
        public DataSet GetALLCODE_Description(string strStatus, string strGWtype, string strContent)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataSet datTable = new DataSet();
            //string strSQL = "select a.cdval,a.id,a.content,a.gwtype from allcode a where Trim(a.cdname)='" + pCDNAME + "' AND Trim(a.gwtype) ='" + strGWtype + "'";
            string strSQL = "select t.description from allcode t where trim(t.cdname)='" + strStatus + "' and trim(t.Gwtype)='" + strGWtype + "' and trim(t.content)='" + strContent + "' order by t.description asc";
            try
            {
                return DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null);

            }
            catch 
            {
                return null;
            }
        }

        public DataTable GetALLCODE_LIST(string pstrGWType, string pCDNAME)
        {
            oraConn = connect.Connect();
            if (oraConn == null)
            {
                return null;
            }
            DataTable datTblReturn = new DataTable();
            DataTable datTable = new DataTable();
            DataRow datRow;

            string strSQL = "select CDVAL, CDNAME, CONTENT, DESCRIPTION from Allcode where gwtype ='" + pstrGWType + "' and CDNAME='" + pCDNAME + "' order by LSTORD";

            try
            {
                datTable = DataAcess.ExecuteDataset(oraConn, CommandType.Text, strSQL, null).Tables[0];
                for (int i = 0; i < datTable.Columns.Count; i++)
                {
                    DataColumn datColum = new DataColumn();
                    datColum.ColumnName = datTable.Columns[i].ColumnName;
                    datTblReturn.Columns.Add(datColum);

                }
                datRow = datTblReturn.NewRow();
                for (int k = 0; k < datTable.Columns.Count; k++)
                {
                    datRow[datTable.Columns[k].ColumnName] = "";
                  
                }
                datTblReturn.Rows.Add(datRow);
                for (int j = 0; j < datTable.Rows.Count; j++)
                {
                    datRow = datTblReturn.NewRow();
                    for(int k=0;k<datTable.Columns.Count; k++)
                    {
                        datRow[datTable.Columns[k].ColumnName] = datTable.Rows[j][datTable.Columns[k].ColumnName].ToString();
                       
                    }
                    datTblReturn.Rows.Add(datRow);
                }
                oraConn.Dispose();
                return datTblReturn;
               
            }
            catch 
            {
                return null;
            }
        }

    }
}
