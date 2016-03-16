/****************************************************************
 * File: clsProcessInquiry.cs
 * ----------------------------
 * Noi dung: 
 * - Xay dung lop xu ly cho Service GWSWIFTRMInquiry
 ***************************************************************/
//=================================================================

#region Using Area
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Data.OracleClient;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using DllASCIIAS400;
#endregion

namespace GWSWIFTRMInquiry
{
    public class ProcessInquiry
    {
        public static ProcessInquiry Instance()
        {
            return new ProcessInquiry();
        }

        #region Dinh nghia tham so
        //cac bien xac dinh 1 so gia tri mac dinh

        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;

        public static string g_strSIBSHost;
        public static ushort g_strSIBSPort;
        
        public string m_strGWType;
        private string m_strGWType_Dept; 
        private string m_strTransCode;
        private string m_ScenarioNum;
        private string m_strMBTranscode;
        private string m_strMBTranscode_Out;
        private string m_NumOfMsg;
        private int m_iMoreRecordPos;


        private string m_strAccNumber = "";
        private string m_strAccType = "";
        private string m_strSeq3 = "";
        private string m_strRefNumber = "";
        private string m_strMsgType = "";
        private string m_strSeqNo = "";
        private string m_strMsgNo = "";

        private int mlErrCodPos;
        private int mlErrCodLen;

        // Dinh nghia cac bien mac dinh luu ten truong theo chuan cua dien SIBS
        // Dung de gan cac gia tri mac dinh khi tao cau Request
        private const string m_SOCKHEAD = "SOCKHEAD";
        private const string m_DSPHEAD = "DSPHEAD";
        private const string m_MBASEHEAD = "MBASEHEAD";
        private const string m_ABCSHEAD = "ABCSHEAD";
        private const string GW_MSG_LEN = "SKTMLEN";
        private const string GW_TRA_COD = "HDTXCD";
        private const string GW_ACT_COD = "HDACCD";
        private const string GW_SCEN_NO = "I13SSNO";
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
        private const string GW_Acquirer = "I13ACQN";
        private const int GW_FIELD_CODE_LEN = 6;

        // dinh nghia kieu du lieu cua cac truong trong SIBS
        public const int GW_TYPE_CHAR = 0;
        public const int GW_TYPE_ZONED = 1;
        public const int GW_TYPE_PACKED = 2;
        public const int GW_TYPE_BINARY = 3;
        public const int GW_TYPE_NUMBER = 4;
        public const int GW_TYPE_DATE = 5;

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
        * Ngay cap nhat    : 07/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        public void ProcessService()
        {
            //Lib.WriteLogDB(LOG_Info, "Bat dau tao cau Inquiry cua SWIFTRMInquiry");
            string[] GWTypes = new string[] {"SWIFT"};
            string GWDept="RM";
            foreach (string GWType in GWTypes)
            {
                try
                {
                    Inquiry(GWType, GWDept);                    
                }
                catch//(Exception ex)
                {
                    Lib.WriteLogDB(LOG_Error, " Error when Inquiry: " + GWType + " - " + GWDept,1);
                    continue;
                }
                Thread.Sleep(100); // Nghi 100 mili giay
            }
        }
        #endregion

        #region CreateInquiryDataTable - Tao DataTable chua ca Field de Inquiry

        /*---------------------------------------------------------------
        * Method           : CreateInquiryDataTable() 
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

        public DataTable CreateInquiryDataTable(string strGWtype, string strDept)
        {
            int i = 0;//bien chay
            string strSIBSFieldcode;
            string strDefaulValue;
            int iLength;
            int iGW_Post;
            int iDatatype;
            int iSIBSPost;
            string strDetail = "";
            //string strQueury = "";
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
                    //strQueury = strQueury.ToString() + strDetail;
                    DataRow datRow = tblAS400.NewRow();
                    datRow["Content"] = strDetail;
                    datRow["GWpos"] = iGW_Post;
                    datRow["SIBSpos"] = iSIBSPost;
                    datRow["Lenght"] = iLength;
                    datRow["DataType"] = iDatatype;
                    //Add gia trij cua cau qeury vao bang
                    tblAS400.Rows.Add(datRow);
                    i = i + 1;
                }
                return tblAS400;
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Connect SIBS false. " + ex.Message,1);
                throw (ex);
            }
        }
        #endregion

        #region Inquiry

        /*---------------------------------------------------------------
        * Method           : Inquiry(string strGWtype, string strDept) 
        * Muc dich         : Tao mot cau query de request vao SIBS
        * Tham so          : string strGWtype,
        *                  : string strDept
        * Tra ve           : int
        *                  : 0 loi; 1: thanh cong
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 23/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        public int Inquiry(string strGWtype, string strDept)
        {
            int isresult = 0; //bien tra ra cho ham            

            try
            {
                bool bMoreInQueue = true;
                // Put Accquirer field by GWType

                if (!InitMemberVariable(strGWtype, strDept)) // neu con dien co phan header nhung chua co phan detail thi thoat ra
                    return 0;

                DataTable tblMSG_DEF = new DataTable();
                //Tao cau query luu vao 1 bang
                tblMSG_DEF = CreateInquiryDataTable(strGWtype, strDept);

                // Khoi tao cac bien de nhan du lieu
                int iDetailStartPos = 0;//vi tri bat dau cua phan detail
                int iDetailLen = 0;//do dai 1 dien phan detail
                int iDetailNumber = 0;//so luong dien trong Message
                string szAcquirer = m_strGWType_Dept;
                string szLastAcqn = szAcquirer;
                string szTempAcqn = "";
                string strAscii = "";
                string strHeader = "";
                string strDetail = "";
                string strMessageInsert = "";

                // Bat dau tao Message gui dien Inquiry
                CCMessage cMessgeSock = new CCMessage();
                // goi ham thuc hien convert du lieu, dinh dang chuan cho cau query, va gui ca qeury vao host
                try
                {
                    if (cMessgeSock.ConvertToAS400_Table(g_strSIBSHost, g_strSIBSPort, tblMSG_DEF))
                    {
                        tblMSG_DEF.Clear();
                        tblMSG_DEF = new DataTable();
                        tblMSG_DEF = GetMSG_DEF(m_strMBTranscode_Out);

                        while (bMoreInQueue)
                        {
                            bMoreInQueue = false;
                            unsafe
                            {
                                int* DetailStartPos = &iDetailStartPos;
                                int* DetailLen = &iDetailLen;
                                int* DetailNumber = &iDetailNumber;
                                // Nhan dien tu SIBS ve dien da duoc convert thanh kieu Ascii
                                strAscii = cMessgeSock.ReceiveMessageASCII(tblMSG_DEF, m_strMBTranscode_Out, DetailStartPos, DetailLen, DetailNumber);
                            }
                            szTempAcqn = strAscii.Substring(178, 12);
                            string strLog = "";
                            if (GetErrorCode(strAscii, ref strLog) != 0)
                            {
                                if (strAscii.IndexOf("No records found.") > 0)
                                {
                                }
                                else
                                {
                                    Lib.WriteLogDB(LOG_Warning, strLog, 1);
                                }
                                if (bMoreInQueue)
                                    continue;
                                else
                                    break;
                            }
                            //xoa du lieu trong bang
                            tblMSG_DEF.Clear();
                            tblMSG_DEF = new DataTable();

                            strHeader = "";
                            strHeader = strAscii.Substring(0, iDetailStartPos - 1);

                            //lay dinh dang chuan dien cho phan detail
                            tblMSG_DEF = GetMSG_DEF(m_strMBTranscode_Out, true);
                            for (int i = 0; i < iDetailNumber; i++)
                            {
                                strDetail = strAscii.Substring(iDetailStartPos - 1, iDetailLen);
                                strMessageInsert = strHeader + strDetail;
                                INSERT_RM_SIBS_QUERY(m_strMBTranscode_Out, strGWtype, strDept, DateTime.Now, strMessageInsert);
                                iDetailStartPos = iDetailStartPos + iDetailLen;
                            }

                            if (strAscii.Substring(m_iMoreRecordPos, 1) == "Y")
                            {
                                bMoreInQueue = true;
                            }
                           

                        }
                        isresult = 1;
                    }
                    else
                    {
                        //don't connect to server
                        Lib.WriteLogDB(LOG_Error, SWIFTRMInquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
                    }
                }
                catch //(Exception ex)
                {
                    Lib.WriteLogDB(LOG_Error, SWIFTRMInquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
                }
            }
            catch //(Exception ex)
            {
                return 0;
            }
            return isresult;
        }
        #endregion

        #region gan du lieu vao cac bien mac dinh m_strTransCode,m_ScenarioNum, m_strGWType_Dept, m_NumOfMsg
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
        * Ngay cap nhat    : 23/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private void GetGWTYPE_DEPT(string strGWType, string strDept)
        {
            m_strGWType = strGWType;

            // strConnString = getConnString();
            Exception exp = new Exception();
            DataTable datTBGWType = new DataTable();
            string strSQL = "select GD.FUNCTION_IN, GD.TRANS_CODE_INQ_IN, GD.GWTYPE_DEPT, GD.MBTRANS_CODE_INQ, GD.NUMOFMSG, MBTRANS_CODE_INQ_OUT from Gwtype_Dept GD ";
            strSQL = strSQL + " where GD.GWTYPE =  '" + strGWType + "'   and GD.DEPARTMENT = '" + strDept + "'";
            try
            {
                datTBGWType = new DataTable();
                //-----------------------------------------
                // lay cac gia tri mac dinh trong cau request
                datTBGWType = Lib.ExcuteDataTable(strSQL);
                if (datTBGWType == null)
                    throw (exp);
                if (datTBGWType.Rows.Count > 0)
                {
                    m_ScenarioNum = datTBGWType.Rows[0]["FUNCTION_IN"].ToString();
                    m_strTransCode = datTBGWType.Rows[0]["TRANS_CODE_INQ_IN"].ToString();
                    m_strGWType_Dept = datTBGWType.Rows[0]["GWTYPE_DEPT"].ToString();
                    m_strMBTranscode = datTBGWType.Rows[0]["MBTRANS_CODE_INQ"].ToString();
                    m_strMBTranscode_Out = datTBGWType.Rows[0]["MBTRANS_CODE_INQ_OUT"].ToString();
                    m_NumOfMsg = datTBGWType.Rows[0]["NUMOFMSG"].ToString();
                }
                datTBGWType.Clear();
                
                //-----------------------------------------
                //-----------------------------------------
                // lay cac dia chi IP, port cua may cai SIBS
                strSQL = "SELECT GWP.SIBSIP,GWP.PORTIN FROM GWSERVICE_PORT GWP WHERE GWP.SERVICENAME='" + SWIFTRMInquiry.SERVICE_NAME + "'";
                datTBGWType = new DataTable();
                datTBGWType = Lib.ExcuteDataTable(strSQL);
                if (datTBGWType == null)
                    throw (exp);
                if (datTBGWType.Rows.Count > 0)
                {
                    g_strSIBSHost = datTBGWType.Rows[0]["SIBSIP"].ToString();
                    if (datTBGWType.Rows[0]["PORTIN"].ToString() != "")
                        g_strSIBSPort = (ushort)Convert.ToUInt16(datTBGWType.Rows[0]["PORTIN"].ToString() + "");
                }
                datTBGWType.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        #region gan du lieu vao cac bien mac dinh m_strTransCode,m_ScenarioNum, m_strGWType_Dept
        /*---------------------------------------------------------------
        * Method           : SetMemberValue() 
        * Muc dich         : Gan gia tri mac dinh cho cac truong theo cau truc dien SIBS
        * Tham so          : szContent gia tri truong dua vao
        *                  : iLength do dai chuoi lay ra
        *                  : iDatatype kieu du lieu trong SIBS cua chuoi kys tu dau vao
        *                  : iBeginPost Vi tri dau tien cua ky tu
         *                 : szSIBSCode Ma truong dien SIBS
        * Tra ve           : String
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 23/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private string SetMemberValue(string szContent,
                                       string szSIBSCode,
                                       int iFieldLength,
                                       int iFieldPos,
                                       int iDataType)
        {

            switch (szSIBSCode)
            {
                case GW_MSG_LEN: // truong nay trong dien xac dinh do dai doan message dua vao 
                    szContent = GetSIBSLength(m_SOCKHEAD, m_DSPHEAD, m_MBASEHEAD, m_strMBTranscode) + "";
                    break;

                case GW_TRA_COD: // truong nay trong dien xac dinh ma transcode  
                    szContent = m_strTransCode + "";
                    break;

                case GW_ACT_COD: //  
                    szContent = "I";
                    break;

                case GW_SCEN_NO: //  Xac dinh 
                    szContent = m_ScenarioNum + "";
                    break;

                case GW_NO_REC: //  Xac dinh so luong dien toi da trong 1 file dien nhan ve tu SIBS cua Inquiry 
                    szContent = m_NumOfMsg + "";
                    break;

                case GW_MORE_REC:
                    m_iMoreRecordPos = iFieldPos - 1;
                    if (m_strAccNumber == "")
                    {
                        szContent = "N"; // 
                    }
                    else
                    {
                        szContent = "Y";
                    }
                    break;

                case GW_ERR_COD:
                    mlErrCodPos = iFieldPos - 1;
                    mlErrCodLen = iFieldLength;
                    break;

                case GW_USER_ID:
                    szContent = "FPT" + m_strGWType + "";
                    break;
                case GW_TYPE:
                    szContent = m_strGWType + "";
                    break;

                case GW_ACC_NUM:
                    szContent = m_strAccNumber + "";
                    break;

                case GW_ACC_TYPE:
                    szContent = m_strAccType + "";
                    break;

                case GW_SEQ3:
                    szContent = m_strSeq3 + "";
                    break;

                case GW_REF_NUM:
                    szContent = m_strRefNumber + "";
                    break;

                case GW_MSG_TYPE:
                    szContent = m_strMsgType + "";
                    break;

                case GW_SEQ_NO:
                    szContent = m_strSeqNo + "";
                    break;

                case GW_MSG_NO:
                    szContent = m_strMsgNo + "";
                    break;

                case GW_STATUS:
                    szContent = "P";
                    break;

                case GW_Acquirer:
                    szContent = m_strGWType_Dept;
                    break;


            }

            szContent = szContent + "";
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
            return szContent + "";

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
        * Ngay cap nhat    : 23/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private DataTable GetMSG_DEF(string strMBTranscode)
        {

            string strSQL = "";
            DataTable datTBGWType = new DataTable();

            if (strMBTranscode.Substring(0, 2) == "MB")
            {
                strSQL = " Select A.MSG_DEF_ID, A.SIBS_FIELD_CODE, A.DEFAULT_VALUE, A.LENGTH, A.SIBS_POS, A.GW_POS,A.DATA_TYPE, B.*,C.* From MSG_DEF A,";
                strSQL = strSQL + " (Select sum(Sibs_msg_length) SIBS_Head_Length, sum(Gw_msg_length) GW_Head_Length  ";
                strSQL = strSQL + " From MSG_LIST Where upper(Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), ";
                strSQL = strSQL + "upper('" + m_DSPHEAD + "'), ";
                strSQL = strSQL + " upper('" + m_MBASEHEAD + "'))) B, ";
                strSQL = strSQL + "(Select sum(Sibs_msg_length) SIBS_Detail_Length, sum(Gw_msg_length) GW_Detail_Length  ";
                strSQL = strSQL + " From MSG_LIST Where upper(Msg_Def_ID)=upper('" + strMBTranscode + "')) C ";
                strSQL = strSQL + " Where upper(A.Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), ";
                strSQL = strSQL + "upper('" + m_DSPHEAD + "'), ";
                strSQL = strSQL + " upper('" + m_MBASEHEAD + "'), ";
                strSQL = strSQL + " upper('" + strMBTranscode + "')) order by A.SIBS_POS ";
            }
            else
            {
                strSQL = " Select A.MSG_DEF_ID, A.SIBS_FIELD_CODE, A.DEFAULT_VALUE, A.LENGTH, A.SIBS_POS, A.GW_POS,A.DATA_TYPE, B.*,C.* From MSG_DEF A,";
                strSQL = strSQL + " (Select sum(Sibs_msg_length) SIBS_Head_Length, sum(Gw_msg_length) GW_Head_Length  ";
                strSQL = strSQL + " From MSG_LIST Where upper(Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), ";
                strSQL = strSQL + "upper('" + m_DSPHEAD + "'), ";
                strSQL = strSQL + " upper('" + m_ABCSHEAD + "'))) B, ";
                strSQL = strSQL + "(Select sum(Sibs_msg_length) SIBS_Detail_Length, sum(Gw_msg_length) GW_Detail_Length  ";
                strSQL = strSQL + " From MSG_LIST Where upper(Msg_Def_ID)=upper('" + strMBTranscode + "')) C ";
                strSQL = strSQL + " Where upper(A.Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), ";
                strSQL = strSQL + "upper('" + m_DSPHEAD + "'), ";
                strSQL = strSQL + " upper('" + m_ABCSHEAD + "'), ";
                strSQL = strSQL + " upper('" + strMBTranscode + "')) order by A.SIBS_POS ";

            }



            try
            {
                datTBGWType = Lib.ExcuteDataTable(strSQL);
                if (datTBGWType == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return datTBGWType;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        #endregion

        #region Lay du lieu dinh nghia khuon dang dien, do dai dien cho cau Request
        /*---------------------------------------------------------------
        * Method           : GetMSG_DEF() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          : string m_strMBTranscode Dieu kien cua cau lenh sql
        *                  : isDetail bool xac dinh la lay gia tri chuan dien phan chi tiet hay ca? phan Header
        * Tra ve           : DataTable
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 23/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        private DataTable GetMSG_DEF(string strMBTranscode, bool isDetail)
        {

            //strConnString = getConnString();
            //if (strConnString == "")
            //    return null;
            string strSQL = "";
            DataTable datTBGWType = new DataTable();
            if (isDetail)
            {
                strSQL = " SELECT MD.MSG_DEF_ID, MD.FIELD_NAME, MD.SIBS_POS, MD.LENGTH, MD.DEFAULT_VALUE, MD.DATA_TYPE, MD.SIBS_FIELD_CODE, MD.GW_POS FROM Msg_Def MD";
                strSQL = strSQL + " Where upper(MD.Msg_Def_ID)= ";
                strSQL = strSQL + " upper('" + strMBTranscode + "') order by MD.SIBS_POS ";
            }
            else
            {
                strSQL = " SELECT MD.MSG_DEF_ID, MD.FIELD_NAME, MD.SIBS_POS, MD.LENGTH, MD.DEFAULT_VALUE, MD.DATA_TYPE, MD.SIBS_FIELD_CODE, MD.GW_POS FROM Msg_Def MD";
                strSQL = strSQL + " Where upper(MD.Msg_Def_ID) in (upper('" + m_SOCKHEAD + "'), ";
                strSQL = strSQL + "upper('" + m_DSPHEAD + "'), ";
                strSQL = strSQL + " upper('" + m_MBASEHEAD + "'), ";
                strSQL = strSQL + " upper('" + strMBTranscode + "')) order by MD.SIBS_POS ";
            }


            try
            {
                datTBGWType = Lib.ExcuteDataTable(strSQL);
                if (datTBGWType == null)
                {
                    Exception exp = new Exception();
                    throw (exp);
                }
                return datTBGWType;
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
            string strSQL = "SELECT SUM(SIBS_MSG_LENGTH) AS LENGTH FROM MSG_LIST WHERE MSG_DEF_ID ";
            strSQL = strSQL + " in ('" + strSockHead + "','" + strDSPHead + "','" + strMBaseHead + "','" + strMBTranscode + "')";
            Exception exp = new Exception();
            try
            {
                datTBGWType = Lib.ExcuteDataTable(strSQL);
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

        #region Insert dien vao bang SIBS_QUERY

        /*---------------------------------------------------------------
        * Method           : INSERT_RM_SIBS_QUERY() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          : string strMBTranscode Dieu kien cua cau lenh sql
        *                  : string pGW_TYPE: Kenh thanh toan
        *                  : string pGW_Dept: Phan he
        *                  : DateTime pTRANS_DATE: Thoi gian giao dich
        *                  : string pCONTENT: Noi dung dien Inquiry                  
        * Tra ve           : int
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 18/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        public int INSERT_RM_SIBS_QUERY(string pMBTrancode,
                                    string pGW_TYPE,
                                    string pGW_Dept,
                                    DateTime pTRANS_DATE,
                                    string pCONTENT)
        {
            OracleParameter[] oraParas ={new OracleParameter("pMBTrancode", OracleType.NVarChar,20),
                                       new OracleParameter("pGW_TYPE", OracleType.NVarChar,10),
                                       new OracleParameter("pGW_DEPT", OracleType.NVarChar,4),
                                       new OracleParameter("pTRANS_DATE", OracleType.DateTime),
                                       new OracleParameter("pCONTENT", OracleType.Clob)};
            try
            {
                oraParas[0].Value = pMBTrancode;
                oraParas[1].Value = pGW_TYPE;
                oraParas[2].Value = pGW_Dept;
                oraParas[3].Value = pTRANS_DATE;
                oraParas[4].Value = pCONTENT;
                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.PRE_INS_SIBS_QUEUE", CommandType.StoredProcedure, oraParas);

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Insert false RM_SIBS_QUERY" + ex.Message, 1);
                return -1;
            }
        }

        #endregion
        #region Kiem tra xem co con dien phan header nao chua lay phan Maintail khong

        /*---------------------------------------------------------------
        * Method           : InitMemberVariable() 
        * Muc dich         : Lay gia tri cua 1 field dien cos day du do dai theo chuan SIBS
        * Tham so          : string GWType, Keenh Thanh toan 
        *                  : string Dept phan he
                            
        * Tra ve           : bool
        *                  : true neu khong con dien nao chua lay phan Maintaince Status=0
         *                  : fale neeus con 
        * Mo ta noi dung   : 
        * Ngay tao         : 23/04/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 09/04/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        public bool InitMemberVariable(string GWType, string Dept)
        {
            bool isReturn = false;
            string strSQL = "SELECT Query_ID FROM RM_SIBS_QUERY Where GWTYPE= '" + GWType + "' and DEPARTMENT='" + Dept + "' And Status=0 ";
            DataTable tblQuery = new DataTable();

            try
            {
                tblQuery = Lib.ExcuteDataTable(strSQL);
                if (tblQuery.Rows.Count == 0)
                {
                    isReturn = true;
                }
                else
                    isReturn = false;
            }

            catch (Exception ex)
            {
                Lib.WriteLogDB(2, "InitMemberVariable false check RM_SIBS_Query" + ex.Message, 1);
                isReturn = false;
            }
            return isReturn;

        }

        #endregion

        #region lay ra truong dien loi

        /*************************************************************************************
         * Method           : GetErrorCode() 
         * Muc dich         : Doc mot value key tu the Tag trong file cau hinh
         * Tham so          : string szMsgASCII, 
         *                  : ref string szLog
         * Tra ve           : Mot string la Value cua key 
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : QuanLD
         * Ngay cap nhat    : 04/04/2008
         * Nguoi cap nhat   : QuanLD
         * --
         *************************************************************************************/
        long GetErrorCode(string szMsgASCII, ref string szLog)
        {
            int lErrorCode = 0;
            string szResCode;
            string szTemp;


            szTemp = szLog;
            // Get error code in MBase Header
            szResCode = szMsgASCII.Substring(mlErrCodPos + 3, mlErrCodLen - 3);
            if (szResCode.Trim() != "")
                lErrorCode = Convert.ToInt32(szResCode);

            if (lErrorCode > 0)
            {
                szResCode = szMsgASCII.Substring(mlErrCodPos + mlErrCodLen, 50);
                szTemp = szLog + szResCode.Trim();
            }
            else
            {
                // Get error code in DSP Header
                szResCode = szMsgASCII.Substring(99, 4);
                if (szResCode.Trim() != "")
                    lErrorCode = Convert.ToInt32(szResCode);
                if (lErrorCode > 0)
                {
                    szTemp = "Co loi trong DSP header: " + szLog + lErrorCode.ToString();
                }
            }
            szLog = szTemp;

            return (lErrorCode);
        }

        #endregion

        #region lay ra truong trong chuoi dien nhan duoc

        /*************************************************************************************
         * Method           : GetMemberValue
         * Muc dich         : Lay gia tri tung truong cua dien  gan vao 1 mang Parameter
         * Tham so          : string szContent, 
						      string szSIBSCode, 
							  int lFieldLength, 
							  int lFieldPos
         * Tra ve           : mot mang tham so oracleParameter
         * Ngay tao         : 04/04/2008
         * Nguoi tao        : QuanLD
         * Ngay cap nhat    : 04/04/2008
         * Nguoi cap nhat   : QuanLD
         * --
         *************************************************************************************/

        private string GetMemberValue(string szContent,
                                               string szSIBSCode,
                                               int lFieldLength,
                                               int lFieldPos)
        {

            string mszAccNumber, mszAccType, mszSeq3, mszRefNumber, mszMsgType, mszSeqNo, mszMsgNo;

            if (GW_ACC_NUM.Trim() == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszAccNumber = szContent.Substring(lFieldPos, lFieldLength);
            }
            else if (GW_ACC_TYPE.Trim() == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszAccType = szContent.Substring(lFieldPos, lFieldLength);
            }
            else if (GW_SEQ3 == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszSeq3 = szContent.Substring(lFieldPos, lFieldLength);
            }
            else if (GW_REF_NUM == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszRefNumber = szContent.Substring(lFieldPos, lFieldLength);
            }
            else if (GW_MSG_TYPE == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszMsgType = szContent.Substring(lFieldPos, lFieldLength);
            }
            else if (GW_SEQ_NO == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszSeqNo = szContent.Substring(lFieldPos, lFieldLength);
            }
            else if (GW_MSG_NO == szSIBSCode.Substring(0, GW_FIELD_CODE_LEN))
            {
                mszMsgNo = szContent.Substring(lFieldPos, lFieldLength);
            }
            return " ";
        }
        #endregion

    }
}
