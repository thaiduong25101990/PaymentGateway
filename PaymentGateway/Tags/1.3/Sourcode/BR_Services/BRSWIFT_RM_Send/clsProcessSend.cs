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
using System.Threading;

namespace BRSWIFTRMSend
{
    public class ProcessSend
    {
        public static ProcessSend Instance()
        {
            return new ProcessSend();
        }

        #region Dinh nghia tham so
        //cac bien xac dinh 1 so gia tri mac dinh
        private int iNumofMess = 100;
        private const int LOG_Info = 1;
        private const int LOG_Warning = 2;
        private const int LOG_Error = 0;

        public static string g_strSIBSHost;
        public static ushort g_strSIBSPort;

        private string m_strSending;


        // Dinh nghia cac bien mac dinh luu ten truong theo chuan cua dien SIBS
        // Dung de gan cac gia tri mac dinh khi tao cau Request
        private const string m_SOCKHEAD = "SOCKHEAD";
        private const string m_DSPHEAD = "DSPHEAD";
        private const string m_MBASEHEAD = "MBASEHEAD";
        private const string m_ABCSHEAD = "ABCSHEAD";

        // Variable for MB58903I
        string m_strGWType;
        string m_strRefNo;
        string m_strMsgType;
        string m_strMsgNo;
        string m_strOldRefNo;
        string m_strLinkRef;
        string m_strOldLinkRef;
        string m_strOldMsgType;

        // Variables for MB58903R
        string m_strMsgIn;
        string m_strMsgOut;
        // string m_strSIBSMsg;
        // DateTime m_dTransDate;
        //int m_iStatus;
        // Bien luu gia tri ID cua dien tai bang SIBS_MSG_IN

        private long m_lMsgID=0;
        private long lID = 0;

        #endregion

        #region  ProcessService- Tao DataTable chua ca Field de SendMessage

        /*---------------------------------------------------------------
        * Method           : ProcessService() 
        * Muc dich         : Lay du lieu trong bang SIBS_MSG_IN cac bang ghi co trang thai Status=0
        *                  : Thuc hien goi cac ham su ly de day dien vao SIBS, Update trang thai cho cac bang du lieu sau khi gui dien
        * Tham so          : string strGWType
        *                  
        * Tra ve           : DataTable chua du lieu dien
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 13/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        public void ProcessService()
        {

            //dinh nghia cac bien luu gia tri tuong ung voi bang SIBS_MSG_IN

            m_strGWType = "";
            m_strRefNo = "";
            m_strMsgType = "";
            m_strMsgNo = "";
            m_strOldRefNo = "";
            m_strLinkRef = "";
            m_strOldLinkRef = "";
            m_strOldMsgType = "";

            // Variables for MB58903R
            m_strMsgIn = "";
            m_strMsgOut = "";
            //m_strSIBSMsg = "";
            DateTime m_dTransDate;
            // m_iStatus = 0;
            m_lMsgID = 0;

            string strHeadSending = "";
            string strContentSending = "";
            int ilen = 0;
            string strSql = "SELECT ID,t.msg_id, t.msg_type, t.ref_no,t.gwtype,  t.msg_def_in, t.msg_def_out,t.trans_date,  ";
            strSql = strSql + " t.head_content,t.content, t.status,t.seq_no,  t.link_ref from RM_SIBS_MSG_IN t ";
            strSql = strSql + " Where t.status=0 And rownum < " + iNumofMess + " ORDER BY t.msg_id ";
            DataTable tblSIBSIN = new DataTable();

            DataTable tblMSG_DEF = new DataTable();

            try
            {
                tblSIBSIN = Lib.ExcuteDataTable(strSql);
                ilen = tblSIBSIN.Rows.Count;
                if (ilen > 0)
                {
                    for (int i = 0; i < ilen; i++)
                    {

                        try
                        {
                            if (tblSIBSIN.Rows[i]["MSG_ID"].ToString() != "")
                                m_lMsgID = Convert.ToInt64(tblSIBSIN.Rows[i]["MSG_ID"].ToString());

                            lID = Convert.ToInt64(tblSIBSIN.Rows[i]["ID"].ToString());
                            m_strMsgType = tblSIBSIN.Rows[i]["MSG_TYPE"].ToString().Trim();
                            m_strRefNo = tblSIBSIN.Rows[i]["REF_NO"].ToString().Trim();
                            m_strGWType = tblSIBSIN.Rows[i]["GWTYPE"].ToString().Trim(); ;
                            m_strMsgIn = tblSIBSIN.Rows[i]["MSG_DEF_IN"].ToString().Trim();
                            m_strMsgOut = tblSIBSIN.Rows[i]["MSG_DEF_OUT"].ToString().Trim();
                            m_dTransDate = Convert.ToDateTime(tblSIBSIN.Rows[i]["TRANS_DATE"]);
                            strHeadSending = tblSIBSIN.Rows[i]["HEAD_CONTENT"].ToString();
                            strContentSending = tblSIBSIN.Rows[i]["CONTENT"].ToString();
                            m_strSending = strHeadSending + strContentSending;
                            m_strLinkRef = tblSIBSIN.Rows[i]["LINK_REF"].ToString();



                            // Cap nhat trang thai Pending
                            UpdateRMSIBSMsgIN(2);
                            UpdateSWIFTContent(2);
                            if (!Check_message_Send(m_lMsgID, "SWIFT-SIBS"))//kiem tra xem trong bang co ton tai gia tri hay khong
                            {
                                //neu chua ton tai thi cap nhat vao bang
                                Insert_Index(m_lMsgID, "SWIFT-SIBS");
                                //Kiem tra dinh dang chuan cua dien la MBase hay ABCS
                                if (m_strMsgIn.Substring(0, 2) == "MB")
                                {
                                    // lay dinh dang chuan trong bang MSG_Def
                                    tblMSG_DEF = GetMSG_DEF(m_SOCKHEAD, m_DSPHEAD, m_MBASEHEAD, m_strMsgIn, false);
                                }
                                else
                                {
                                    // lay dinh dang chuan trong bang MSG_Def
                                    tblMSG_DEF = GetMSG_DEF(m_SOCKHEAD, m_DSPHEAD, m_ABCSHEAD, m_strMsgIn, false);
                                }


                                DataTable tblASCii = new DataTable();
                                tblASCii = CreateMessage(tblMSG_DEF, m_strSending);

                                bool isSent = SendMessage(tblASCii, m_strGWType, m_strMsgOut);
                                // Neu chua send duoc thi cap nhat lai trang thai bang 0
                                if (!isSent)
                                {
                                    UpdateRMSIBSMsgIN(-1);
                                    UpdateSWIFTContent(-1);
                                }
                            }
                            else
                            {
                                Lib.WriteLogDB(LOG_Warning, "Dien nay da duoc gui lan thu hai", m_lMsgID);
                            }
                        }
                        catch //(Exception ex)
                        {
                            continue;
                        }
                        if (i > 0 && (i % 5 == 0))
                            Thread.Sleep(5); // Cu send 5 dien lai nghi 10 mili giay

                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Error When Get Data fron SIBS_MSG_IN" + ex.Message, m_lMsgID);
            }
            finally
            {
                tblSIBSIN.Clear();
            }
        }
        #endregion


        #region
        public int Insert_Index(long LID,string MSG_DIRECTION)
        {
            int iSult = 0;
            try
            {
                string strSQL = "insert into RM_SVR_INDEX(MSG_ID,MSG_DIRECTION) values(:pLID,'SWIFT-SIBS')";
                OracleParameter[] Orapara = { new OracleParameter("pLID", OracleType.Number, 20)
                                            };
                Orapara[0].Value = LID;                
                iSult = Lib.ExecuteNonQuery(strSQL, CommandType.Text, Orapara);
                return iSult;
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Bi loi khi insert du lieu vao bang IBPS_SVR_INDEX" + ex.Message, m_lMsgID);
                return -1;
            }
        }
        #endregion

        #region
        public bool Check_message_Send(long LID,string MSG_DIRECTION)
        {
            bool bBool = true;
            try
            {
                DataTable datIndex = new DataTable();
                string strSQL = "select MSG_ID from RM_SVR_INDEX RM where RM.MSG_ID= " + LID + "  and RM.MSG_DIRECTION ='" + MSG_DIRECTION + "' ";
                datIndex = Lib.ExcuteDataTable(strSQL);
                if (datIndex.Rows.Count == 1)
                {
                    bBool = true;
                }
                else if (datIndex.Rows.Count == 0)
                {
                    bBool = false;
                }
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Bi loi khi" + ex.Message, m_lMsgID);
                bBool = false;
            }
            return bBool;
        }
        #endregion


        #region  CreateMessage-

        /*---------------------------------------------------------------
        * Method           : CreateMessage() 
        * Muc dich         : Lay du lieu trong bang SIBS_MSG_IN cac bang ghi co trang thai Status=0
        *                  : 
        * Tham so          : string strGWType
        *                  
        * Tra ve           : DataTable chua du lieu dien
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 13/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        public DataTable CreateMessage(DataTable tblMSG_DEF, string strMessage)
        {
            //dinh nghia cac bien luu gia tri tuong ung voi bang SIBS_MSG_IN
            int i = 0;//bien chay
            string strSIBSFieldcode;
            string strDefaulValue;
            int iLength;
            int iGW_Post;
            int iDatatype;
            int iSIBSPost;
            string strDetail = "";
            string strQueury = "";

            // tao bang du lieu luu gia tri cua cau query
            DataTable tblAS400 = new DataTable("tblAS400");

            DataColumn[] datColumAS400 = {  new DataColumn("Content",Type.GetType("System.String")),
                                            new DataColumn("GWpos",Type.GetType("System.Int32")),
                                            new DataColumn("SIBSpos",Type.GetType("System.Int32")),
                                            new DataColumn("Lenght",Type.GetType("System.Int32")),
                                            new DataColumn("DataType",Type.GetType("System.Int32"))};

            tblAS400.Columns.AddRange(datColumAS400);

            int ilen = 0;
            ilen = tblMSG_DEF.Rows.Count;
            try
            {
                if (ilen <= 0)
                    return null;

                while (i < tblMSG_DEF.Rows.Count)
                {
                        strDetail = "";
                        strSIBSFieldcode = tblMSG_DEF.Rows[i]["SIBS_FIELD_CODE"].ToString();
                        strDefaulValue = tblMSG_DEF.Rows[i]["DEFAULT_VALUE"].ToString();
                        iLength = Convert.ToInt32(tblMSG_DEF.Rows[i]["Length"].ToString());
                        iGW_Post = Convert.ToInt32(tblMSG_DEF.Rows[i]["GW_POS"].ToString());
                        iDatatype = Convert.ToInt32(tblMSG_DEF.Rows[i]["Data_Type"].ToString());
                        iSIBSPost = Convert.ToInt32(tblMSG_DEF.Rows[i]["SIBS_POS"].ToString());

                        if (strMessage.Length < iGW_Post + iLength)
                        {
                            strDetail = "";
                            if (i == 190)
                            {
                                i = 190;
                            }
                            if (strMessage.Length > iGW_Post - 1)
                            {
                                strDetail = strMessage.Substring(iGW_Post - 1);
                            }

                            if (iDatatype != 0)
                            {

                                for (int j = strDetail.Length; j < iLength; j++)
                                {
                                    strDetail = "0" + strDetail;
                                }

                            }
                            else
                            {
                                for (int j = strDetail.Length; j < iLength; j++)
                                {
                                    strDetail = strDetail + " ";
                                }
                            }

                        }
                        else
                            strDetail = strMessage.Substring(iGW_Post - 1, iLength); //SetMemberValue(strDefaulValue, strSIBSFieldcode, iLength, iGW_Post, iDatatype).ToString();


                        if (m_strGWType != "IBS")
                        {
                            if (strSIBSFieldcode == "I13ACQN")
                            {
                                int len222 = m_lMsgID.ToString().Length;
                                int i11 = len222;
                                strDetail = m_lMsgID.ToString();
                                while (i11 < iLength)
                                {
                                    strDetail = "0" + strDetail.ToString();
                                    i11 = i11 + 1;
                                }

                            }
                        }

                        strQueury = strQueury.ToString() + strDetail;
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
                
              
            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(LOG_Error, "Error When Get Data fron SIBS_MSG_IN" + ex.Message, m_lMsgID);
            }

            return tblAS400;
        }
        #endregion

        #region  SendMessage-

        /*---------------------------------------------------------------
        * Method           : SendMessage() 
        * Muc dich         : Convert dien theo chuan sau do gui dien vao SIBS
        *                  : Nhan dien tra loi
        *                  : 
        * Tham so          : string strGWType
        *                  
        * Tra ve           : Bool
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 13/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        public bool SendMessage(DataTable tblMSG_DEF, string strGW_TYPE, string strMBTransCodeOut)
        {
            GetGWTYPE(strGW_TYPE);

            string szErr = "";
            int ilen = tblMSG_DEF.Rows.Count;
            int iDetailStartPos = 0;//vi tri bat dau cua phan detail
            int iDetailLen = 0;//do dai 1 dien phan detail
            int iDetailNumber = 0;//so luong dien trong Message
            string strAscii = "";
            int lErrorCode = 0;
            //dinh nghia cac bien luu gia tri tuong ung voi bang SIBS_MSG_IN
            CCMessage cMessgeSock = new CCMessage();
            try
            {
                if (ilen <= 0)
                    return false;


                if (cMessgeSock.ConvertToAS400_Table(g_strSIBSHost, g_strSIBSPort, tblMSG_DEF))
                {
                    tblMSG_DEF = new DataTable();
                    tblMSG_DEF = GetMSG_DEF(strMBTransCodeOut);

                    unsafe
                    {
                        int* DetailStartPos = &iDetailStartPos;
                        int* DetailLen = &iDetailLen;
                        int* DetailNumber = &iDetailNumber;
                        // Nhan dien tu SIBS ve dien da duoc convert thanh kieu Ascii
                        strAscii = cMessgeSock.ReceiveMessageASCII(tblMSG_DEF, strMBTransCodeOut, DetailStartPos, DetailLen, DetailNumber);
                        tblMSG_DEF.Clear();
                    }
                    lErrorCode = GetErrorCode(strAscii, ref  szErr);
                    if (strAscii.Length <= 0)
                    {
                        lErrorCode = -1;
                    }
                    if (lErrorCode != 0)
                    {
                        if (strAscii.IndexOf("Transaction timeout") > 0)
                        {
                            UpdateSWIFTContent(-1,22);
                        }
                        else
                        {
                            UpdateSWIFTContent(-1);
                        }
                        UpdateRMSIBSMsgIN(-1);
                        
                        string strLog = "Message with Number " + m_lMsgID + " type " + m_strMsgType + " sent to interface " + m_strGWType + "  with error code " + lErrorCode + szErr + " : " + strAscii;
                        Lib.WriteLogDB(LOG_Info, strLog, m_lMsgID);
                    }
                    else
                    {
                        UpdateRMSIBSMsgIN(1);

                        string strSql;
                        OracleParameter[] oraPara ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
                        switch (m_strGWType)
                        {

                            case "SWIFT":
                                strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                                oraPara[0].Value = 1;
                                oraPara[1].Value = m_lMsgID;

                                Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);


                                OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
                                strSql = "UPDATE SWIFT_MSG_ALL Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                                oraPara1[0].Value = 1;
                                oraPara1[1].Value = m_lMsgID;

                                Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);


                                OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
                                strSql = "UPDATE SWIFT_MSG_ALL_HIS Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                                oraPara2[0].Value = 1;
                                oraPara2[1].Value = m_lMsgID;

                                Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);

                                break;
                            default:
                                UpdateSWIFTContent(1);
                                break;

                        }

                        string strLog = "Message with Number " + m_strLinkRef + " type " + m_strMsgType + " sent to interface " + m_strGWType + "  successfully.";
                        Lib.WriteLogDB(LOG_Info, strLog, m_lMsgID);
                    }


                }

                else
                {
                    string strLog = "Message with Number " + m_lMsgID + " type " + m_strMsgType + " sent to interface " + m_strGWType + "  Can not connect Host";
                    Lib.WriteLogDB(LOG_Info, strLog, m_lMsgID);
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                UpdateRMSIBSMsgIN(-1);
                UpdateSWIFTContent(-1);
                Lib.WriteLogDB(LOG_Error, "Error When Get Data fron SIBS_MSG_IN" + ex.Message, m_lMsgID);
                return false;
            }

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
        private DataTable GetMSG_DEF(string strSockHead,
                                      string strDSPHead,
                                      string strMBaseHead,
                                      string strMBTranscode,
                                      bool isDetail)
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
                strSQL = strSQL + " Where upper(MD.Msg_Def_ID) in (upper('" + strSockHead.Trim() + "'), ";
                strSQL = strSQL + "upper('" + strDSPHead.Trim() + "'), ";
                strSQL = strSQL + " upper('" + strMBaseHead.Trim() + "'), ";
                strSQL = strSQL + " upper('" + strMBTranscode.Trim() + "')) order by MD.SIBS_POS ";

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

        #region Ham lay m_strOldMsgType, m_strOldRefNo m_strMsgNo gia tri trong dien
        /*---------------------------------------------------------------
        * Method           : GetMsgNo() 
        * Muc dich         : La ra cac gia tri m_strOldMsgType, m_strOldRefNo m_strMsgNo theo 1 vi tri da duwox fix
        *                  : Nhan dien tra loi
        *                  : 
        * Tham so          : string strASCII noi dung dien
        *                  
        * Tra ve           : Void
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 13/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/
        void GetMsgNo(string strASCII)
        {
            int iMsgNoPos = 731, iMsgNoLen = 3;
            int iRefNoPos = 706, iRefNoLen = 20;
            int iMsgTypePos = 726, iMsgTypeLen = 5;


            m_strOldMsgType = strASCII.Substring(iMsgTypePos, iMsgTypeLen);
            m_strOldRefNo = strASCII.Substring(iRefNoPos, iRefNoLen);
            m_strMsgNo = strASCII.Substring(iMsgNoPos, iMsgNoLen);

            m_strOldRefNo = m_strOldRefNo.Trim();

            m_strOldLinkRef = m_strLinkRef.Trim();

            m_strMsgNo = m_strMsgNo.Trim();

        }
        #endregion

        #region Ham lay gia tri xac dinh dien loi
        /*---------------------------------------------------------------
        * Method           : GetErrorCode() 
        * Muc dich         : La ra cac gia tri m_strOldMsgType, m_strOldRefNo m_strMsgNo theo 1 vi tri da duwox fix
        *                  : Nhan dien tra loi
        *                  : 
        * Tham so          : string  strASCII, nooi dung dien nhan ve 
        *                 : ref string  szErr Noi dung loi duoc mo ta boi SIBS
        *                  
        * Tra ve           : int
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 18/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/
        int GetErrorCode(string strASCII, ref string szErr)
        {
            int lErrorCode = 0;
            string szResCode;

            // Get error code n MBase Header
            szResCode = strASCII.Substring(361, 4);

            if (Lib.ISnumber(szResCode))
            {
                lErrorCode = Convert.ToInt32(szResCode);
            }
            else
            {
                lErrorCode = 0;
            }


            if (lErrorCode > 0)
            {
                szErr = strASCII.Substring(365, 50);

            }
            else
            {
                // Get error code in DSP Header
                szResCode = strASCII.Substring(99, 4);

                if (Lib.ISnumber(szResCode))
                {
                    lErrorCode = Convert.ToInt32(szResCode);
                }
                else
                {
                    lErrorCode = 0;
                }

                if (lErrorCode > 0)
                {
                    szErr = "Error in DSP header: DSP" + lErrorCode;
                }
                else
                {
                    szErr = "";
                }
            }

            return (lErrorCode);
        }

        #endregion


        #region Ham dien gia tri cho 1 doan
        /*---------------------------------------------------------------
        * Method           : PumpIn() 
        * Muc dich         : 
        *                  : 
        *                  : 
        * Tham so          : string strSending, Chuoi dich 
        *                  : string strMsgNo, chuoi nguon nhet vaof chuoi dich
        *                  int iMsgNoPos, vi tri bat dau
        *                  int iMsgNoLen do dai
        *                 : 
        *                  
        * Tra ve           : String
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 13/05/2008
        * Nguoi cap nhat   : QuanLD
        *--------------------------------------------------------------*/

        private string PumpIn(string strSending, string strMsgNo, int iMsgNoPos, int iMsgNoLen)
        {
            strSending.Remove(iMsgNoPos, iMsgNoLen);
            strSending.Insert(iMsgNoPos, strMsgNo);
            return strSending;
        }

        #endregion

        #region Lay 1 so gia tri mac dinh
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
        private void GetGWTYPE(string strGWType)
        {

            m_strGWType = strGWType;

            // strConnString = getConnString();
            Exception exp = new Exception();
            DataTable datTBGWType = new DataTable();
            string strSQL = "";
            try
            {

                //-----------------------------------------
                // lay cac dia chi IP, port cua may cai SIBS
                strSQL = "SELECT GT.SIBSIP, GT.PORTIN FROM GWSERVICE_PORT GT WHERE GT.SERVICENAME='" + SWIFTRMSend.SERVICE_NAME + "'";
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

        #region Ham Update du lieu vao bang RM_SIBS_MSG_IN
        /*---------------------------------------------------------------
        * Method           : UpdateRMSIBSMsgIN() 
        * Muc dich         : 
        *                  : 
        *                  : 
        * Tham so          : int iStatus trang thai
        *                  
        * Tra ve           : int
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 17/06/2008
        * Nguoi cap nhat   : TrungNV
        *--------------------------------------------------------------*/

        private int UpdateRMSIBSMsgIN(int iStatus)
        {
            string strSql = "";
            try
            {
                strSql = "UPDATE RM_SIBS_MSG_IN Set Status=:pStatus Where ID=:pMsg_ID ";
                OracleParameter[] oraPara ={new OracleParameter("pStatus",OracleType.Int32), 
                                           new OracleParameter("pMsg_ID",OracleType.Int32)};
                oraPara[0].Value = iStatus;
                oraPara[1].Value = lID;
                return Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);

            }
            catch
            {
                Lib.WriteLogDB(LOG_Error, "Update RM_SIBS_MSG_IN Error", m_lMsgID);
                return -1;
            }

        }

        #endregion

        #region Ham Update du lieu vao bang UpdateSWIFTContent
        /*---------------------------------------------------------------
        * Method           : UpdateSWIFTContent() 
        * Muc dich         : 
        *                  : 
        *                  : 
        * Tham so          : int status trang thai
        *                 : 
        *                  
        * Tra ve           : int iResult
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 17/06/2008
       
        *--------------------------------------------------------------*/

        private int UpdateSWIFTContent(int status, int ierr_code )
        {
            int iResult = 0;
            try
            {
                string strSql;
                OracleParameter[] oraPara ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
                if (status == -1)
                {
                    switch (m_strGWType)
                    {

                        case "SWIFT":
                            strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus, Err_Code= " +  ierr_code + " Where QUERY_ID =:pQueryID ";
                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);

                            OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_All Set Status=:pStatus, Err_Code= " + ierr_code + "  Where QUERY_ID =:pQueryID ";
                            oraPara1[0].Value = status;
                            oraPara1[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);

                              OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                              strSql = "UPDATE SWIFT_MSG_All_HIS Set Status=:pStatus, Err_Code= " + ierr_code + "  Where QUERY_ID =:pQueryID ";
                            oraPara2[0].Value = status;
                            oraPara2[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);
                            break;
                        case "IBS":
                            strSql = "UPDATE TTSP_MSG_CONTENT Set Status=:pStatus , Err_Code=" + ierr_code + "  Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                        case "VCB":
                            strSql = "UPDATE VCB_MSG_CONTENT Set Status=:pStatus, Err_Code= " + ierr_code + "  Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                    }
                }
                else if (status == 1)
                {
                    switch (m_strGWType)
                    {

                        case "SWIFT":
                            strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
             
                            strSql = "UPDATE SWIFT_MSG_ALL Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                            oraPara1[0].Value = status;
                            oraPara1[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);
                            OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
             
                            strSql = "UPDATE SWIFT_MSG_ALL_HIS Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                            oraPara2[0].Value = status;
                            oraPara2[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);
                            break;
                        case "IBS":
                            strSql = "UPDATE TTSP_MSG_CONTENT Set Status=:pStatus , Sending_Time= sysdate Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                        case "VCB":
                            strSql = "UPDATE VCB_MSG_CONTENT Set Status=:pStatus , Sending_Time= sysdate Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                    }
                  
                }
                else
                {
                    switch (m_strGWType)
                    {

                        case "SWIFT":
                            strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus Where QUERY_ID =:pQueryID ";
                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
             
                            strSql = "UPDATE SWIFT_MSG_ALL Set Status=:pStatus Where QUERY_ID =:pQueryID ";
                            oraPara1[0].Value = status;
                            oraPara1[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);
                            OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
             
                            strSql = "UPDATE SWIFT_MSG_ALL_HIS Set Status=:pStatus Where QUERY_ID =:pQueryID ";
                            oraPara2[0].Value = status;
                            oraPara2[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);
                           
                            break;
                        case "IBS":
                            strSql = "UPDATE TTSP_MSG_CONTENT Set Status=:pStatus Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                        case "VCB":
                            strSql = "UPDATE VCB_MSG_CONTENT Set Status=:pStatus Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                    }
                }
                return iResult;
            }
            catch
            {
                Lib.WriteLogDB(LOG_Error, "Update SWIFT_MSG_CONTENT Error", m_lMsgID);
                return -1;
            }
        }
        #endregion

        #region Ham Update du lieu vao bang UpdateSWIFTContent
        /*---------------------------------------------------------------
        * Method           : UpdateSWIFTContent() 
        * Muc dich         : 
        *                  : 
        *                  : 
        * Tham so          : int status trang thai
        *                 : 
        *                  
        * Tra ve           : int iResult
        * Ngay tao         : 13/05/2008
        * Nguoi tao        : QuanLD
        * Ngay cap nhat    : 17/06/2008
       
        *--------------------------------------------------------------*/

        private int UpdateSWIFTContent(int status)
        {
            int iResult = 0;
            try
            {
                string strSql;
                OracleParameter[] oraPara ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };
                if (status == -1)
                {
                    switch (m_strGWType)
                    {

                        case "SWIFT":
                            strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus, Err_Code=6 Where QUERY_ID =:pQueryID ";
                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);

                            OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_All Set Status=:pStatus, Err_Code=6 Where QUERY_ID =:pQueryID ";
                            oraPara1[0].Value = status;
                            oraPara1[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);

                            OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_All_HIS Set Status=:pStatus, Err_Code=6 Where QUERY_ID =:pQueryID ";
                            oraPara2[0].Value = status;
                            oraPara2[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);
                            break;
                        case "IBS":
                            strSql = "UPDATE TTSP_MSG_CONTENT Set Status=:pStatus ,Err_Code=6 Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                        case "VCB":
                            strSql = "UPDATE VCB_MSG_CONTENT Set Status=:pStatus,Err_Code=6 Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                    }
                }
                else if (status == 1)
                {
                    switch (m_strGWType)
                    {

                        case "SWIFT":
                            strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_ALL Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                            oraPara1[0].Value = status;
                            oraPara1[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);
                            OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_ALL_HIS Set Status=:pStatus, Sending_Time= sysdate Where QUERY_ID =:pQueryID ";
                            oraPara2[0].Value = status;
                            oraPara2[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);
                            break;
                        case "IBS":
                            strSql = "UPDATE TTSP_MSG_CONTENT Set Status=:pStatus , Sending_Time= sysdate Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                        case "VCB":
                            strSql = "UPDATE VCB_MSG_CONTENT Set Status=:pStatus , Sending_Time= sysdate Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                    }

                }
                else
                {
                    switch (m_strGWType)
                    {

                        case "SWIFT":
                            strSql = "UPDATE SWIFT_MSG_CONTENT Set Status=:pStatus Where QUERY_ID =:pQueryID ";
                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            OracleParameter[] oraPara1 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_ALL Set Status=:pStatus Where QUERY_ID =:pQueryID ";
                            oraPara1[0].Value = status;
                            oraPara1[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara1);
                            OracleParameter[] oraPara2 ={    new OracleParameter("pStatus",OracleType.Int32), 
                                                        new OracleParameter("pQueryID",OracleType.Int32)
                                                   };

                            strSql = "UPDATE SWIFT_MSG_ALL_HIS Set Status=:pStatus Where QUERY_ID =:pQueryID ";
                            oraPara2[0].Value = status;
                            oraPara2[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara2);

                            break;
                        case "IBS":
                            strSql = "UPDATE TTSP_MSG_CONTENT Set Status=:pStatus Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                        case "VCB":
                            strSql = "UPDATE VCB_MSG_CONTENT Set Status=:pStatus Where QUERY_ID =:pQueryID ";

                            oraPara[0].Value = status;
                            oraPara[1].Value = m_lMsgID;

                            iResult = Lib.ExecuteNonQuery(strSql, CommandType.Text, oraPara);
                            break;
                    }
                }
                return iResult;
            }
            catch
            {
                Lib.WriteLogDB(LOG_Error, "Update SWIFT_MSG_CONTENT Error", m_lMsgID);
                return -1;
            }
        }
        #endregion


    }
}





