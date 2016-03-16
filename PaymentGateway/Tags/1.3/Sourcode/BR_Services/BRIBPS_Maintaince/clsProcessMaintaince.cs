using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using DllASCIIAS400;
using System.Runtime.InteropServices;

namespace BRIBPSMaintaince
{
    class ProcessMaintaince
    {
        #region Dinh nghia tham so
        //cac bien xac dinh 1 so gia tri mac dinh

        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;

        // Define GW Type
        private const int GW_TYPE_IBPS_RM = 1;
        // Dinh nghia cac bien
        public static string g_strSIBSHost; // Server IP
        public static ushort g_strSIBSPort; // Port 
        public const string g_GWType = "IBPS";
        public const string g_GWDept = "RM";

        // Define data type
        public const int GW_TYPE_CHAR = 0;
        public const int GW_TYPE_ZONED = 1;
        public const int GW_TYPE_PACKED = 2;
        public const int GW_TYPE_BINARY = 3;
        public const int GW_TYPE_NUMBER = 4;
        public const int GW_TYPE_DATE = 5;
        // Define field name

        private const string GW_MSG_LEN = "SKTMLEN";
        private const string GW_TRA_COD = "HDTXCD";
        private const string GW_ACT_COD = "HDACCD";
        private const string GW_SCEN_NO = "I13SSNO";
        private const string GW_ACQN_NO = "I13ACQN";
        private const string GW_USER_ID = "HDUSID";
        private const string GW_NO_REC = "HDNREC";
        private const string GW_MORE_REC = "HDMREC";
        private const string GW_ERR_COD = "HDRCD1";

        private const string GW_TYPE = "RVINTF";
        private const string GW_ACC_NUM = "RVACCT";
        private const string GW_ACC_TYPE = "RVATYP";
        private const string GW_SEQ3 = "RVSEQ3";
        private const string GW_REF_NUM = "RVREFN";
        private const string GW_MSG_TYPE = "RVMTYP";
        private const string GW_SEQ_NO = "RVSEQ#";
        private const string GW_MSG_NO = "RVMSG#";
        private const string GW_STATUS = "STATUS";

        private const int GW_TIMES_TRY_GET_BROKEN_MSG = 3;
        private const int GW_FIELD_CODE_LEN = 6;

        private const int GW_TIMES_TRY_TO_CONNECT = 2;

        private const int CV_MAX_LENGTH_STRING = 4600;
        private const int CV_MAX_SIBS_LENGTH_STRING = 4096;

        private const string m_SOCKHEAD = "SOCKHEAD";
        private const string m_DSPHEAD = "DSPHEAD";
        private const string m_MBASEHEAD = "MBASEHEAD";
        private const string m_ABCSHEAD = "ABCSHEAD";

        // Define string length
        private const int CV_SQL_LEN = 600;
        private const int CV_MAX_GETVAL_LEN = 4000;
        private const int CV_MAX_DATE_LEN = 30;


        // Variable for MB58903I
        string m_strGWType;
        string m_strAccNumber;
        string m_strAccType;
        string m_strSeq3;
        string m_strRefNumber;
        string m_strMsgType;
        string m_strSeqNo;
        string m_strMsgNo;

        // Variables for MB58903R
        string m_strMsgDef;
        long m_lMsgQueryID;

        int m_iErrCodPos;
        int m_iErrCodLen;

        string m_strMBTranscode;
        string m_strMBTranscode_Out;
        string m_strTranCode;
        string m_strModule;
        int m_iMsgSize;
        string m_strScenarioNum;
        string m_strAcquirer;
        string m_strGWType_Dept;

        #endregion


        #region  ProcessService- Tao DataTable chua ca Field de Inquiry

        /*---------------------------------------------------------------
        * Method           : ProcessService() 
        * Muc dich         : Lay tat cac cac phan he thuoc kenh thanh toan
        *                  : sau do lan luot goi cac ham de gui nhan dien tu SIBS GW theo tung phan he 
        *                  : Ham thuc hien goi mot ham de gui du lieu vao SIBS trong Dll
        * Tham so          :    
        *                  
        * Tra ve           : 
        * Ngay tao         : 07/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/

        public void ProcessService()
        {

            // Select de mo rong co the khai bao cac phan he moi cho IBPS
            // Lay cac phan he cua IBPS tu bang GWTYPE_DEPT
            DataTable tblGWTypeDept = new DataTable();
            string strSQL = " SELECT GWTYPE,DEPARTMENT FROM GWTYPE_DEPT Where GWTYPE='" + g_GWType + "' ";
            try
            {
                tblGWTypeDept = Lib.Select_ReturnDataTable(strSQL);

            int ilen = tblGWTypeDept.Rows.Count;
            if (ilen == 0)
                return;
            
                // Xu li lan luot cac phan he theo truong DEPARTMENT
                for (int i = 0; i < ilen; i++)
                {
                    try
                    {
                        Maintaince(g_GWType, tblGWTypeDept.Rows[i]["DEPARTMENT"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        Lib.WriteLogDB(LOG_Error, "Xay ra loi tai Process Service: " + ex.Message,1);
                        continue;
                    }
                }

                tblGWTypeDept.Clear();
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Xay ra loi tai Process Service: " + ex.Message, 1);
                return;
            }
        }
        #endregion

        /*---------------------------------------------------------------
        * Method           : Maintaince(string strGWtype, string strDept)
        * Muc dich         : Ham de xu li Maintaince
        * Tham so          : 
        *                   + strGWtype: Kenh xu cua Gateway
        *                   + strDept: Phan he
        * Tra ve           : voi
        * Ngay tao         : 29/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        #region Maintaince(string strGWtype, string strDept)

        public void Maintaince(string strGWtype, string strDept)
        {
            int iDetailStartPos = 0;//vi tri bat dau cua phan detail
            int iDetailLen = 0;//do dai 1 dien phan detail
            int iDetailNumber = 0;//so luong dien trong Message

            string szTempAcqn = "";
            //string strHeader = "";
            //string strDetail = "";
            string mszSIBSMsg = "";
            //// lay cac gia tri mac dinh cua phan he kenh thanh toan
            //GetGWTYPE_DEPT(strGWtype, strDept);

            DataTable tblMsgList = GetMsgList(strGWtype, strDept);
            int iCount = tblMsgList.Rows.Count;
            for (int i = 0; i < iCount; i++)
            {
                m_lMsgQueryID = Convert.ToInt64(tblMsgList.Rows[i]["QUERY_ID"]);
                //m_strGWType = tblMsgList.Rows[i]["GW_Type"].ToString();
                m_strAccNumber = tblMsgList.Rows[i]["RM_Number"].ToString();
                m_strAccType = tblMsgList.Rows[i]["Acct_Type"].ToString();
                m_strSeq3 = tblMsgList.Rows[i]["Seq3"].ToString();
                m_strRefNumber = tblMsgList.Rows[i]["Ref_Number"].ToString();
                m_strMsgType = tblMsgList.Rows[i]["Msg_Type"].ToString();
                m_strSeqNo = tblMsgList.Rows[i]["Seq_No"].ToString();
                m_strMsgNo = tblMsgList.Rows[i]["Msg_No"].ToString();

                try
                {
                    // Dat status la Pending dang cho Maintaince xu li dien
                    UpdateQueryStatus(2);

                    // Bat dau tao Message de gui vao SIBS

                    DataTable tblMsg;
                    tblMsg = new DataTable();
                    tblMsg = CreateMsgTable(strGWtype, strDept);

                    CCMessage cMessgeSock = new CCMessage();
                    // goi ham thuc hien convert du lieu, dinh dang chuan cho cau query, va gui ca qeury vao host
                    if (cMessgeSock.ConvertToAS400_Table(g_strSIBSHost, g_strSIBSPort, tblMsg))
                    {
                        tblMsg.Dispose();
                        DataTable tblMSG_DEF = new DataTable();
                        tblMSG_DEF = GetMSG_DEF(m_strMBTranscode_Out);
                                                
                        int headLength = GetGWHeadLength(m_SOCKHEAD, m_DSPHEAD, m_MBASEHEAD);

                        string strASCII = "";

                        unsafe
                        {
                            int* DetailStartPos = &iDetailStartPos;
                            int* DetailLen = &iDetailLen;
                            int* DetailNumber = &iDetailNumber;
                            // Nhan dien tu SIBS ve dien da duoc convert thanh kieu Ascii
                            strASCII = cMessgeSock.ReceiveMessageASCII(tblMSG_DEF, m_strMBTranscode_Out, DetailStartPos, DetailLen, DetailNumber);
                        }

                        int iReceived = strASCII.Length;
                        if (iReceived <= 0)
                        {
                            Lib.WriteLogDB(LOG_Error, "IBPS Maintaince - Nhan duoc dien bi rong", m_lMsgQueryID);
                            // Cap nhat trang thai 0 cho lan sau xu li lai
                            UpdateQueryStatus(0); // Cap nhat trang thai bang 0 de cho xu li sau
                            continue;
                        }
                        else
                        {
                            // Check the message is correct or not!			
                            szTempAcqn = strASCII.Substring(178, 12);
                            if (m_strAcquirer == szTempAcqn)
                            {
                                m_lMsgQueryID = Convert.ToInt64(szTempAcqn);
                            }
                            else
                            {
                                UpdateQueryStatus(-1);
                                Lib.WriteLogDB(LOG_Error, " Message error: Header not in maintaince ", m_lMsgQueryID);
                                // Neu dien bi loi thi cap nhat trang thai 
                                // Va chuyen sang xu li dien khac
                                continue;
                            }


                            string strLog = "Da nhan dien tu " + strGWtype + " " + strDept;
                            if (GetErrorCode(strASCII, ref strLog) != 0)
                            {
                                UpdateQueryStatus(-1);
                                Lib.WriteLogDB(LOG_Error, strLog, m_lMsgQueryID);
                                // Neu dien bi loi thi cap nhat trang thai 
                                // Va chuyen sang xu li dien khac
                                continue;
                            }                            

                            // Neu dien 
                            mszSIBSMsg = strASCII;

                            string strHeadContent = mszSIBSMsg.Substring(0, headLength);
                            string strContent = mszSIBSMsg.Substring(headLength);

                            

                            //kiem tra xem dien nay co bi trung so RM voi cac dien khac da nhan roi hay khong
                            //neu trung thi Cap nhat trang thai la -2 trong bang Query va khong insert vao bang
                            // Out nua
                            DataTable datQuery = new DataTable();
                            string strSqlquery = "select ISQ.QUERY_ID from IBPS_SIBS_QUERY ISQ where ISQ.Status = 1 And ISQ.QUERY_ID<> " + m_lMsgQueryID;
                            strSqlquery = strSqlquery + " And Trim(ISQ.Rm_Number) =  (select TRIM(IBPS_SIBS_QUERY.RM_NUMBER) From IBPS_SIBS_QUERY ";
                            strSqlquery = strSqlquery + " where QUERY_ID =  " + m_lMsgQueryID + ")";

                            datQuery = Lib.Select_ReturnDataTable(strSqlquery);

                            if (datQuery.Rows.Count > 1) // co  dien trung so RM trong bang query da duoc nhanh phan Maintail
                            {
                                UpdateQueryStatus(-2);
                            }
                            else
                            {
                                if (InsertMessageContent(strHeadContent, strContent))
                                {
                                    UpdateQueryStatus(1);
                                }
                                else
                                    UpdateQueryStatus(-1);
                            }
                        }
                    }

                    else
                    {
                        // Cap nhat trang thai 0 cho lan sau xu li lai
                        UpdateQueryStatus(-1); // Cap nhat trang thai bang 0 de cho xu li sau
                            
                        //don't connect to server
                        Lib.WriteLogDB(LOG_Error, "Khong the gui duoc Request toi Server SIBS", m_lMsgQueryID);
                    }

                }
                catch (Exception ex)
                {
                    UpdateQueryStatus(-1); 
                    Lib.WriteLogDB(LOG_Error, "Co loi xay ra trong Maintaince: " + ex.Message, m_lMsgQueryID);
                    continue;
                }
            }

            // Giai phong bo nho
            tblMsgList.Dispose();

        }

        #endregion

        /*---------------------------------------------------------------
        * Method           : InsertMessageContent(...)
        * Muc dich         : Insert dien vao bang SIBS_MSG_OUT
        * Tham so          : str_SIBS_Text - Noi dung dien SIBS
        * Tra ve           : bool (true: neu insert thanh cong, False nguoc lai
        * Ngay tao         : 09/05/2008
        * Nguoi tao        : TrungNV
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/

        private bool InsertMessageContent(string strHeader, string Content)
        {
            try
            {
               
                string strSQL = "INSERT INTO IBPS_SIBS_MSG_OUT (Query_ID, Msg_Def, Trans_Date, Status, Head_Content,Content) "
                            + " VALUES (:pQuery_ID,:pMsg_Def,:pTrans_Date,:pStatus,:pHeader,:pContent)";
                OracleParameter[] param = {
                                         new OracleParameter(":pQuery_ID",OracleType.Number ,20),
                                         new OracleParameter(":pMsg_Def",OracleType.NVarChar ,20),
                                         new OracleParameter(":pTrans_Date",OracleType.DateTime),
                                         new OracleParameter(":pStatus",OracleType.Number),
                                         new OracleParameter(":pHeader",OracleType.Clob),
                                         new OracleParameter(":pContent",OracleType.Clob)
                                          };
                param[0].Value = m_lMsgQueryID;
                param[1].Value = m_strMsgDef;
                param[2].Value = DateTime.Now;
                param[3].Value = 0;
                param[4].Value = strHeader;
                param[5].Value = Content;

                Lib.ExecuteNonQuery(strSQL, CommandType.Text, param);
                return true;
                }
            //}
            catch //(Exception ex)
            {
                return false;
            }
        }


        /*---------------------------------------------------------------
        * Method           : UpdateQueryStatus(...)
        * Muc dich         : Cap nhan trang thai dien tai IBPS_SIBS_QUERY
        * Tham so          : iStatus - trang thai cua dien
        * Tra ve           : void
        * Ngay tao         : 09/05/2008
        * Nguoi tao        : TrungNV
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/

        private void UpdateQueryStatus(int iStatus)
        {
            string strSQL = "UPDATE IBPS_SIBS_QUERY SET Status = '" + iStatus + "' WHERE Query_ID = '" + m_lMsgQueryID + "'";
            try
            {
                Lib.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*---------------------------------------------------------------
        * Method           : GetErrorCode(...)
        * Muc dich         : Lay gia tri loi cua dien nhan ve
        * Tham so          : iStatus - trang thai cua dien
        * Tra ve           : 
        *                     + szMsgASCII: Noi dung dien nhan ve
         *                    + szLog: Noi dung log
        * Ngay tao         : 09/05/2008
        * Nguoi tao        : TrungNV
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        private int GetErrorCode(string szMsgASCII, ref string szLog)
        {
            int lErrorCode = 0;
            string szResCode;
            string szTemp;
            szTemp = szLog;
            try
            {

                // Get error code in MBase Header
                szResCode = szMsgASCII.Substring(m_iErrCodPos + 3, m_iErrCodLen - 3);

                if (szResCode.ToString().Trim() != "")
                    lErrorCode = Convert.ToInt32(szResCode);
                else
                    lErrorCode = 0;
                if (lErrorCode > 0)
                {
                    szResCode = szMsgASCII.Substring(m_iErrCodPos + m_iErrCodLen, 50);

                    szTemp = szLog.Trim() + ": ." + szResCode.Trim();
                }
                else
                {
                    // Get error code in DSP Header
                    szResCode = szMsgASCII.Substring(99, 4);
                    lErrorCode = Convert.ToInt32(szResCode);
                    if (lErrorCode > 0)
                    {
                        szTemp = szLog.Trim() + ": Error in DSP header: .DSP" + lErrorCode.ToString();
                    }
                }
            }
            catch //(Exception ex)
            {
                lErrorCode = 0;
                //    throw new Exception("Khong the GetErrorCode khi nhan dien ve. "+ex.Message);
            }

            szLog = szTemp;

            return (lErrorCode);
        }

        /*---------------------------------------------------------------
        * Method           : GetMSG_DEF() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          :      
        *                    + m_strMBTranscode: Dieu kien cua cau lenh sql
        *                    + isDetail: xac dinh la lay gai tri chuan den phan chi tiet hay car phan Header
        * Tra ve           : DataTable
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        #region GetMSG_DEF - Lay danh sach khuon dang cac dien
        public DataTable GetMSG_DEF(string strMsgType, bool isDetail)
        {
            string strSQL = "";
            if (isDetail)
            {
                strSQL = " SELECT * FROM MSG_DEF WHERE MSG_DEF_ID='" + strMsgType + "' order by SIBS_POS"; 
                
            }
            else
                strSQL = " SELECT * FROM MSG_DEF WHERE "
                       + " MSG_DEF_ID='" + m_SOCKHEAD + "'"
                       + "OR MSG_DEF_ID='" + m_DSPHEAD + "'"
                       + "OR MSG_DEF_ID='" + m_MBASEHEAD + "'"
                       + "OR MSG_DEF_ID='" + strMsgType + "' order by SIBS_POS";


            return Lib.Select_ReturnDataTable(strSQL);
        }
        #endregion

        #region Lay du lieu dinh nghia khuon dang dien, do dai dien cho cau Request
        /*---------------------------------------------------------------
        * Method           : GetMSG_DEF() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          : string strSockHead, 
        *                  : string strDSPHead, 
        *                  : string strMBaseHead, 
        *                  : string m_strMBTranscode Dieu kien cua cau lenh sql
        *                  : isDetail bool xac dinh la lay gai tri chuan den phan chi tiet hay car phan Header
        * Tra ve           : DataTable
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        private DataTable GetMSG_DEF(string strMsgType)
        {

            try
            {
                DataTable tblGWType = new DataTable();
                string strSQL = "";
                if (strMsgType.Substring(0, 2) == "MB")
                {
                    strSQL = " Select A.MSG_DEF_ID, A.SIBS_FIELD_CODE, A.DEFAULT_VALUE, A.LENGTH, A.SIBS_POS, A.GW_POS,A.DATA_TYPE, B.*,C.* From MSG_DEF A,"
                           + " (Select sum(Sibs_msg_length) SIBS_Head_Length, sum(Gw_msg_length) GW_Head_Length  "
                           + " From MSG_LIST Where upper(Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), "
                           + "upper('" + m_DSPHEAD + "'), "
                           + " upper('" + m_MBASEHEAD + "'))) B, "
                           + "(Select sum(Sibs_msg_length) SIBS_Detail_Length, sum(Gw_msg_length) GW_Detail_Length  "
                           + " From MSG_LIST Where upper(Msg_Def_ID)=upper('" + strMsgType + "')) C "
                           + " Where upper(A.Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), "
                           + "upper('" + m_DSPHEAD + "'), "
                           + " upper('" + m_MBASEHEAD + "'), "
                           + " upper('" + strMsgType + "')) order by A.SIBS_POS ";
                }
                else
                {
                    strSQL = " Select A.MSG_DEF_ID, A.SIBS_FIELD_CODE, A.DEFAULT_VALUE, A.LENGTH, A.SIBS_POS, A.GW_POS,A.DATA_TYPE, B.*,C.* From MSG_DEF A,"
                           + " (Select sum(Sibs_msg_length) SIBS_Head_Length, sum(Gw_msg_length) GW_Head_Length  "
                           + " From MSG_LIST Where upper(Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), "
                           + "upper('" + m_DSPHEAD + "'), "
                           + " upper('" + m_ABCSHEAD + "'))) B, "
                           + "(Select sum(Sibs_msg_length) SIBS_Detail_Length, sum(Gw_msg_length) GW_Detail_Length  "
                           + " From MSG_LIST Where upper(Msg_Def_ID)=upper('" + strMsgType + "')) C "
                           + " Where upper(A.Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), "
                           + "upper('" + m_DSPHEAD + "'), "
                           + " upper('" + m_ABCSHEAD + "'), "
                           + " upper('" + strMsgType + "')) order by A.SIBS_POS ";

                }

                tblGWType = Lib.Select_ReturnDataTable(strSQL);
                if (tblGWType == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return tblGWType;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        #endregion

        #region CreateMsgTable - Tao DataTable chua ca Field de Inquiry

        /*---------------------------------------------------------------
        * Method           : CreateMsgTable() 
        * Muc dich         : Tao mot DataTable chua cac Field de request vao SIBS 
        *                  : Ham thuc hien goi mot ham de gui du lieu vao SIBS trong Dll
        * Tham so          :    string strGWtype,//Kenh thanh toan
        *                       string strDept //Phan he
        *                  
        * Tra ve           : DataTable
        * Ngay tao         : 07/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 07/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        public DataTable CreateMsgTable(string strGWtype, string strDept)
        {
            int i = 0;//bien chay
            string strSIBSFieldcode;
            string strDefaulValue;
            int iLength;
            int iGW_Post;
            int iDatatype;
            int iSIBSPost;
            string strDetail = "";
            string strQueury = "";
            DataTable tblMSG_DEF = new DataTable();
            // tao bang du lieu luu gia tri cua cau query
            DataTable tblAS400 = new DataTable("tblAS400");

            DataColumn[] datColumAS400 = {  new DataColumn("Content",Type.GetType("System.String")),
                                            new DataColumn("GWpos",Type.GetType("System.Int32")),
                                            new DataColumn("SIBSpos",Type.GetType("System.Int32")),
                                            new DataColumn("Lenght",Type.GetType("System.Int32")),
                                            new DataColumn("DataType",Type.GetType("System.Int32"))};

            tblAS400.Columns.AddRange(datColumAS400);

            try
            {
                GetGWTYPE_DEPT(strGWtype, strDept);
                tblMSG_DEF = GetMSG_DEF(m_strMBTranscode, false);

                // Test
                string strTest = "";
                while (i < tblMSG_DEF.Rows.Count)
                {
                    strDetail = "";
                    strSIBSFieldcode = tblMSG_DEF.Rows[i]["SIBS_FIELD_CODE"].ToString();
                    strDefaulValue = tblMSG_DEF.Rows[i]["DEFAULT_VALUE"].ToString();
                    iLength = Convert.ToInt32(tblMSG_DEF.Rows[i]["Length"].ToString());
                    iGW_Post = Convert.ToInt32(tblMSG_DEF.Rows[i]["GW_POS"].ToString());
                    iDatatype = Convert.ToInt32(tblMSG_DEF.Rows[i]["Data_Type"].ToString());
                    iSIBSPost = Convert.ToInt32(tblMSG_DEF.Rows[i]["SIBS_POS"].ToString());
                    strDetail = SetMemberValue(strDefaulValue, strSIBSFieldcode, iLength, iGW_Post, iDatatype).ToString();
                    strQueury = strQueury.ToString() + strDetail;
                    DataRow datRow = tblAS400.NewRow();
                    datRow["Content"] = strDetail;
                    datRow["GWpos"] = iGW_Post;
                    datRow["SIBSpos"] = iSIBSPost;
                    datRow["Lenght"] = iLength;
                    datRow["DataType"] = iDatatype;
                    //Add gia trij cua cau qeury vao bang
                    tblAS400.Rows.Add(datRow);
                    strTest += strDetail;
                    i = i + 1;
                }
                return tblAS400;
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Loi khi gui dien vao SIBS. " + ex.Message, m_lMsgQueryID);
                throw (ex);
            }
        }
        #endregion


        /*---------------------------------------------------------------
        * Method           : GetMsgList() 
        * Muc dich         : Lay danh sach cac dien theo phan he ma chua xu li Maintaince
        * Tham so          : 
        *                  : GWType - Kenh thanh toan
        *                  : Dept - Phan he
        * Tra ve           : DataTable
        * Ngay tao         : 09/05/2008
        * Nguoi tao        : TrungNV
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        #region GetMsgList - Lay danh sach cac dien
        public DataTable GetMsgList(string GWType, string Dept)
        {
            string strSQL = " SELECT QUERY_ID,RM_Number,Acct_Type,Seq3,Ref_Number,Msg_Type,Seq_No,Msg_No "
                          + " FROM IBPS_SIBS_QUERY WHERE STATUS=0 AND rownum <= 10 "
                          + " AND DEPARTMENT='" + Dept + "'";
            return Lib.Select_ReturnDataTable(strSQL);
        }
        #endregion


        /*---------------------------------------------------------------
         * Method           : SetMemberValue(...) 
         * Muc dich         : 
         *                      + 
         *                      + 
         *                      + 
         * Tham so          :   + szContent: Noi dung
         *                      + szSIBSCode: code trong dien SIBS
         *                      + lFieldLength; Do dai cua truong
         *                      + lFieldPos: Vi tri
         *                      + lDataType: kieu du lieu
         * Tra ve           : 
         * Ngay tao         : 09/05/2008
         * Nguoi tao        : TrungNV
         * Ngay cap nhat    : 09/05/2008
         * Nguoi cap nhat   : TrungNV
         *--------------------------------------------------------------*/
        public string SetMemberValue(string szContent, string szSIBSCode, int iFieldLength, int iFieldPos, long iDataType)
        {
            switch (szSIBSCode)
            {
                case GW_MSG_LEN:
                    // Xac dinh do dai cua dien
                    //m_iMsgSize = GetSIBSLength(m_SOCKHEAD, m_DSPHEAD, m_MBASEHEAD, m_strMBTranscode);
                    szContent = m_iMsgSize.ToString().Trim();
                    break;
                case GW_TRA_COD:
                    // Xac dinh ma TransCode             
                    szContent = m_strTranCode;
                    break;
                case GW_ACT_COD:
                    //              
                    szContent = "C";
                    break;
                case GW_SCEN_NO:
                    //             
                    szContent = m_strScenarioNum;
                    break;
                case GW_ACQN_NO:
                    // Lay m_strAcquirer voi do dai 12 ky tu       
                    m_strAcquirer = String.Format("{0:000000000000}", m_lMsgQueryID);
                    szContent = m_strAcquirer;
                    break;
                case GW_NO_REC:
                    //             
                    szContent = "001";
                    break;
                case GW_MORE_REC:
                    //             
                    szContent = "N";
                    break;
                case GW_ERR_COD:
                    //             
                    m_iErrCodPos = iFieldPos - 1;
                    m_iErrCodLen = iFieldLength;
                    break;
                case GW_USER_ID:
                    //             
                    szContent = "FPT" + m_strGWType + "";
                    break;
                case GW_TYPE:
                    //             
                    szContent = m_strGWType;
                    break;
                case GW_ACC_NUM:
                    //             
                    szContent = m_strAccNumber;
                    break;
                case GW_ACC_TYPE:
                    //             
                    szContent = m_strAccType;
                    break;

                case GW_SEQ3:
                    //             
                    szContent = m_strSeq3;
                    break;
                case GW_REF_NUM:
                    //             
                    szContent = m_strRefNumber;
                    break;
                case GW_MSG_TYPE:
                    //             
                    szContent = m_strMsgType;
                    break;
                case GW_SEQ_NO:
                    //             
                    szContent = m_strSeqNo;
                    break;
                case GW_MSG_NO:
                    //             
                    szContent = m_strMsgNo;
                    break;
                case GW_STATUS:
                    //             
                    szContent = "S";
                    break;

            }
            szContent = szContent.ToString().Trim();
            if (iDataType != GW_TYPE_CHAR)
            {
                int len = szContent.Length;
                int i = len;
                while (i < iFieldLength)
                {
                    szContent = "0" + szContent;
                    i = i + 1;
                }

            }
            else
            {
                int len = szContent.Length;
                int i = len;
                while (i < iFieldLength)
                {
                    szContent = szContent + " ";
                    i = i + 1;
                }
            }
            return szContent.ToString();
        }


        /*---------------------------------------------------------------
        * Method           : GetGWTYPE_DEPT() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          : strSource gia tri truong dua vao
        *                  : iLength do dai chuoi lay ra
        *                  : iDatatype kieu du lieu trong SIBS cua chuoi kys tu dau vao
        *                  : iBeginPost Vi tri dau tien cua ky tu
        * Tra ve           : Table
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/05/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        #region gan du lieu vao cac bien mac dinh m_strTransCode,m_ScenarioNum, m_strGWType_Dept, m_NumOfMsg
        private void GetGWTYPE_DEPT(string strGWType, string strDept)
        {

            // strConnString = getConnString();
            m_strGWType = strGWType;
            m_strModule = strDept;

            Exception exp = new Exception();
            DataTable datTBGWType = new DataTable();
            string strSQL = "select GD.FUNCTION_MAIN, GD.TRANS_CODE_MAIN_IN,GD.MBTRANS_CODE_MAIN,GD.MBTRANS_CODE_MAIN_OUT, GD.NUMOFMSG from Gwtype_Dept GD "
                          + " where GD.GWTYPE =  '" + strGWType + "'   and GD.DEPARTMENT = '" + strDept + "'";
            try
            {
                datTBGWType = new DataTable();
                //-----------------------------------------
                // lay cac gia tri mac dinh trong cau request
                datTBGWType = Lib.Select_ReturnDataTable(strSQL);
                if (datTBGWType == null)
                    throw (exp);
                if (datTBGWType.Rows.Count > 0)
                {
                    m_strScenarioNum = datTBGWType.Rows[0]["FUNCTION_MAIN"].ToString();
                    m_strTranCode = datTBGWType.Rows[0]["TRANS_CODE_MAIN_IN"].ToString();
                    m_strGWType_Dept = strDept;
                    m_strMBTranscode = datTBGWType.Rows[0]["MBTRANS_CODE_MAIN"].ToString();

                    m_strMBTranscode_Out = datTBGWType.Rows[0]["MBTRANS_CODE_MAIN_OUT"].ToString();
                    // m_strMsgNo = datTBGWType.Rows[0]["NUMOFMSG"].ToString();
                    m_strMsgDef = m_strMBTranscode_Out;
                    m_iMsgSize = GetSIBSLength(m_SOCKHEAD, m_DSPHEAD, m_MBASEHEAD, m_strMBTranscode);

                }
                datTBGWType.Clear();
                // ket thuc -----------------------------------------


                //-----------------------------------------
                // lay cac dia chi IP, port cua may cai SIBS
                strSQL = "SELECT GT.SIBSIP, GT.PORTIN FROM GWSERVICE_PORT GT WHERE GT.SERVICENAME='" + IBPSMaintaince.SERVICE_NAME + "'";
                datTBGWType = new DataTable();
                datTBGWType = Lib.Select_ReturnDataTable(strSQL);
                if (datTBGWType == null)
                    throw (exp);
                if (datTBGWType.Rows.Count > 0)
                {
                    g_strSIBSHost = datTBGWType.Rows[0]["SIBSIP"].ToString();
                    if (datTBGWType.Rows[0]["PORTIN"].ToString() != "")
                        g_strSIBSPort = (ushort)Convert.ToUInt16(datTBGWType.Rows[0]["PORTIN"].ToString() + "");
                }
                datTBGWType.Dispose();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        #region Lay do dai dien cho cau Request
        /*---------------------------------------------------------------
        * Method           : GetSIBSLength() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          : string strSockHead, 
                              string strDSPHead, 
                              string strMBaseHead, 
                              string strMBTranscode Dieu kien cua cau lenh sql
        * Tra ve           : int
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private int GetSIBSLength(string strSockHead,
                                  string strDSPHead,
                                  string strMBaseHead,
                                  string strMBTranscode)
        {
            int iSIBSlength = 0;
            //strConnString = getConnString();
            DataTable datTBGWType = new DataTable();
            string strSQL = "SELECT SUM(SIBS_MSG_LENGTH) AS LENGTH FROM MSG_LIST WHERE MSG_DEF_ID "
                   + " in ('" + strSockHead + "','" + strDSPHead + "','" + strMBaseHead + "','" + strMBTranscode + "')";
            Exception exp = new Exception();
            try
            {
                datTBGWType = Lib.Select_ReturnDataTable(strSQL);
                if (datTBGWType == null)
                    throw (exp);
                if (datTBGWType.Rows.Count > 0)
                    iSIBSlength = Convert.ToInt32(datTBGWType.Rows[0]["LENGTH"].ToString()) - 4;
                datTBGWType.Clear();
                return iSIBSlength;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        #region Lay do dai Header cua dien cho cau Request
        /*---------------------------------------------------------------
        * Method           : GetGWHeadLength() 
        * Muc dich         : Lay gia tri do dai cua Header dien cos day du do dai theo chuan SIBS
        * Tham so          : string strSockHead, 
                              string strDSPHead, 
                              string strMBaseHead
        * Tra ve           : int
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private int GetGWHeadLength(string strSockHead,
                                  string strDSPHead,
                                  string strMBaseHead)
        {
            int headLength = 0;
            //strConnString = getConnString();
            DataTable datTBGWType = new DataTable();
            string strSQL = "SELECT SUM(gw_msg_length) AS LENGTH FROM MSG_LIST WHERE MSG_DEF_ID "
                   + " in ('" + strSockHead + "','" + strDSPHead + "','" + strMBaseHead + "')";
            Exception exp = new Exception();
            try
            {
                datTBGWType = Lib.Select_ReturnDataTable(strSQL);
                if (datTBGWType == null)
                    throw (exp);
                if (datTBGWType.Rows.Count > 0)
                    headLength = Convert.ToInt32(datTBGWType.Rows[0]["LENGTH"].ToString());
                datTBGWType.Clear();
                datTBGWType.Dispose();
                return headLength;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

    }
    //End class
}
