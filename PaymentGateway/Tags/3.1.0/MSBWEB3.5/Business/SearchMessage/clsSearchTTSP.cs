using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using BIDVWEB.Comm.DA;
using System.IO;

namespace BIDVWEB.Business.SearchMessage
{
    public class clsSearchTTSP
    {
        public string strError = "";
        private clsDatatAccess objDataAccess = new clsDatatAccess();
        private OracleConnection oraConn = new OracleConnection();
        private clsConnection connect = new clsConnection();
                

        //Ham tim kiem du lieu theo dieu kien////////////////////////
        // Muc dich:    Ham tim kiem du lieu theo dieu kien
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strWhere: Cau dieu kien where khong co truong
        //                        FromDate, ToDate 
        //              pDateFrom: Kieu DateTime
        //              pDateTo:   Kieu DateTime 
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////
        public DataSet SearchAdvance(string pWhere, string datefrom, string dateto)
        {
            try
            {
                DataSet ds = new DataSet();
                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }

                string strSQL = "GW_PK_TTSP_MSG_CONTENT.TTSP_MSG_CONTENT_SEARCH";
               
                OracleParameter[] Oraparam = {new OracleParameter("pWhere",OracleType.VarChar,4000),
                                         new OracleParameter("pdatefrom",OracleType.VarChar,10),
                                         new OracleParameter("pdateto",OracleType.VarChar,10),
                                         new OracleParameter("pContent",OracleType.Cursor),
                                         new OracleParameter("pAll",OracleType.Cursor),
                                         new OracleParameter("pAll_his",OracleType.Cursor),
                                         new OracleParameter("pColumns",OracleType.Cursor),
                                         };
                Oraparam[3].Direction = ParameterDirection.Output;
                Oraparam[4].Direction = ParameterDirection.Output;
                Oraparam[5].Direction = ParameterDirection.Output;
                Oraparam[6].Direction = ParameterDirection.Output;
                Oraparam[0].Value = pWhere;
                Oraparam[1].Value = datefrom;//Convert.ToDateTime(datefrom).ToString("dd-MM-yyyy");
                Oraparam[2].Value = dateto;//Convert.ToDateTime(dateto).ToString("dd-MM-yyyy");

                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, Oraparam);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                Process(ex);
                if (oraConn.State == ConnectionState.Open)
                {
                    oraConn.Close();
                    oraConn.Dispose();
                }
                strError = ex.Message;
                return null;
            }
        }

        public static void Process(Exception ex)
        {
            StreamWriter myStreamWriter = null;
            String strPath = "C:\\logs\\";
            String strFilename = "";

            try
            {
                strFilename = String.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";
                if (Directory.Exists(strPath))
                {

                }
                else
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + strFilename))
                {
                    myStreamWriter = File.CreateText(strPath + strFilename);
                }
                else
                {
                    myStreamWriter = File.AppendText(strPath + strFilename);
                }
                myStreamWriter.WriteLine(System.DateTime.Now);
                myStreamWriter.WriteLine(ex.Message + "\n" + ex.StackTrace);

                myStreamWriter.Write(myStreamWriter.NewLine);
                myStreamWriter.WriteLine("Source : Unknown source");
                myStreamWriter.WriteLine("Type : Runtime error");
                myStreamWriter.WriteLine("Code : Undefined");
                myStreamWriter.WriteLine("Description :" + ex.Message);
                myStreamWriter.WriteLine("StackTrace :" + ex.StackTrace.ToString());
                myStreamWriter.Write(myStreamWriter.NewLine);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                myStreamWriter.Flush();
                myStreamWriter.Close();
              
            }
        }
        //Ham lay thong tin dien theo QUERY_ID///////////////////////
        // Muc dich:    Ham lay thong tin dien theo QUERY_ID
        // Ngay tao:    08/2008
        // Nguoi tao:   Huypq7
        // Dau vao:             
        // Dau ra:      
        /////////////////////////////////////////////////////////////
        public DataSet GetMsgTTSPByID(string sMsg_id)
        {
            try
            {
                string strSQL = "";
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();

                oraConn = connect.Connect();
                if (oraConn == null)
                {
                    return null;
                }

                strSQL = "GW_PK_TTSP_MSG_CONTENT.GET_MESSAGE_ONE";
                OracleParameter[] oraParas1 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                oraParas1[0].Value = "TTSP_MSG_CONTENT";
                oraParas1[1].Value = sMsg_id;
                oraParas1[2].Direction = ParameterDirection.Output;
                ds = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas1);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    OracleParameter[] oraParas2 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                    oraParas2[0].Value = "TTSP_MSG_ALL";
                    oraParas2[1].Value = sMsg_id;
                    oraParas2[2].Direction = ParameterDirection.Output;
                    ds1 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas2);
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        return ds1;
                    }
                    else
                    {
                        OracleParameter[] oraParas3 ={new OracleParameter("pTABLE",OracleType.VarChar),
                                             new OracleParameter("pMSG_ID",OracleType.VarChar),
                                             new OracleParameter("pCursor",OracleType.Cursor)
                                             };
                        oraParas3[0].Value = "TTSP_MSG_ALL_HIS";
                        oraParas3[1].Value = sMsg_id;
                        oraParas3[2].Direction = ParameterDirection.Output;
                        ds2 = clsDataAcessComm.ExecuteDataset(oraConn, CommandType.StoredProcedure, strSQL, oraParas3);
                        if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                        {
                            return ds2;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }
    }
}
