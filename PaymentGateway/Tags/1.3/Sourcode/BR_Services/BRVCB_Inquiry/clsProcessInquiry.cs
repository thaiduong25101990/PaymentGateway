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
using System.Text.RegularExpressions;

namespace BRVCB_Inquiry
{
    public class ProcessInquiry
    {

        private string _20 = ""; // so Ref
        private string _32A = ""; // date,curency,mount
        private string _53B = ""; // so tk trich no
        private string _59 = ""; // so tk nguoi huong, ten va dia chi nguoi huong
        private string _57D = ""; // so tk bank nguoi huong, dc bank nguoi huong
        private string _57E = ""; // dia chi nguoi huong [not use]
        private string _70 = ""; // chi tiet thanh toan
        private string _71A = ":71A:OUR"; //chi tiet phi [ BEN-phi nguoi huong chiu, OUR-phi don vi chiu ]
        private string _50K = ""; // nguoi ra lenh        
        private string _Check_key = "";
        private string _Content = "";
        private int _Status = 1;
        private string _rmacno = "";//truong so RM

        public static ProcessInquiry Instance()
        {
            return new ProcessInquiry();
        }

        string strSql = "";//cau Sql connect vao host de lay ra dien VCB
        public void ProcessService()
        {
            try
            {
                Inquiry();
            }
            catch
            {
                Lib.WriteLogDB(0, " Error when Inquiry: ", 1);
            }
            Thread.Sleep(100); // Nghi 100 mili giay
        }

        public int Inquiry()
        {
            int isresult = 0;
            try
            {
                #region Ham lay ra ngay,thang, 2 so cuoi cua nam
                string strDAY = VCB_Inquiry.sysdate.Day.ToString();
                if (strDAY.Length == 1)
                { strDAY = "0" + strDAY; }
                string strMONTH = VCB_Inquiry.sysdate.Month.ToString();
                if (strMONTH.Length == 1)
                { strMONTH = "0" + strMONTH; }
                string strYEAR = VCB_Inquiry.sysdate.Year.ToString().Substring(2, 2);

                string strYMD = strYEAR + strMONTH + strDAY;//nam,thang,ngay
                string strDMY = strDAY + strMONTH + strYEAR;
                #endregion  
              
                DataTable _dt = new DataTable();
                strSql = strSql + "SELECT c.rdbr,b.tlbf09,b.tlbf01,a.rmpb40,a.rmbena,a.rmamt,a.rmcurr,a.rmsnme,c.rdefth,b.tlbafm,b.tlbrmk ";
                strSql = strSql + " FROM STDATTRN.RMMAST A, STDATTRN.TLLOG B, STDATTRN.RMDETL C ";
                strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2')) ";
                strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '  and b.TLBCOR='N'";
                //strSql = "SELECT rdbr,tlbf09,tlbf01,rmpb40,rmbena,rmamt,rmcurr,rmsnme,rdefth,tlbafm,tlbrmk from vcb_content_host";
                isresult = 0;
                _dt = Lib.ExcuteDataTableODBC(strSql);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            string str20A = "";
                            int iCount = VCB_Inquiry.countmessage + 1 + i;
                            if (iCount.ToString().Length < 2) { str20A = "00" + iCount; }
                            else { str20A = "0" + iCount; }
                            _20 = ":20:A" + strDMY + "_" + str20A ;
                            _32A = ":32A:" + strYMD + _dt.Rows[i]["rmcurr"].ToString().Trim() + _dt.Rows[i]["rmamt"].ToString().Trim().Replace(".", ",");
                            _50K = ":50K:" + _dt.Rows[i]["rmsnme"];
                            if (_dt.Rows[i]["rmcurr"].ToString().Trim() == "VND") { _53B = ":53B:/0681000012578"; }
                            if (_dt.Rows[i]["rmcurr"].ToString().Trim() == "USD") { _53B = ":53B:/0681370012414"; }
                            if (_dt.Rows[i]["rmcurr"].ToString().Trim() == "EUR") 
                            { 
                                _53B = ":53B:/0681140012429";
                            }
                            string ccycd = _dt.Rows[i]["rmcurr"].ToString().Trim();
                            string amt = _dt.Rows[i]["rmamt"].ToString();
                            if (_dt.Rows[i]["rmcurr"].ToString().Trim() == "GBP") { _53B = ":53B:/0681356666661"; }
                            if (_dt.Rows[i]["rmcurr"].ToString().Trim() == "AUD") { _53B = ":53B:/0681520012461"; }
                            _57D = ":57D:" + _dt.Rows[i]["rmpb40"];
                            _59 = ":59:/" + _dt.Rows[i]["rmbena"];                            
                            string str = _dt.Rows[i]["tlbafm"].ToString();// .Replace("\\b\\s{2,}\\b", "#");
                            String[] Arr = str.Split(new String[] { _dt.Rows[i]["rmcurr"].ToString().Trim() }, StringSplitOptions.None);
                            String strMess = Arr[1].Trim();
                            String [] strMess_Split = strMess.Split(new String[] { "    " }, StringSplitOptions.None);
                            string str59 = strMess_Split[0];                            
                            _70 = ":70:(" + _dt.Rows[i]["tlbf01"] + ")" + _dt.Rows[i]["rdefth"].ToString().Substring(3);
                            _71A = ":71A:OUR";                            
                            _Check_key = _dt.Rows[i]["tlbf01"].ToString() + _dt.Rows[i]["rmamt"].ToString();

                            _Content = _20.Trim() + "#" + _32A.Trim() + "#" + _50K.Trim() + "#" + _53B.Trim() + "#" + _57D.Trim() + "#" + _59.Trim() + "#" + str59 + "#" + _70.Trim() + "#" + _71A.Trim();
                            _20 = _20.Replace(":20:A", ""); _32A = _32A.Replace(":32A:", ""); _50K = _50K.Replace(":50K:", "");
                            _53B = _53B.Replace(":50K:", ""); _53B = _53B.Replace(":53B:/", ""); _57D = _57D.Replace(":57D:", "");
                            _59 = _59.Replace(":59:/", ""); _70 = _70.Replace(":70:", ""); _71A = _71A.Replace(":71A:", "");

                            _rmacno = _dt.Rows[i]["tlbf01"].ToString();

                            //if (Lib.Check_key(_Check_key) == 1)
                            //{
                                INSERT_VCB_INQUIRY_HOST(_20, _32A, _50K, _53B, _57D, _59, _70, _71A, _Check_key, 0, _Content, _rmacno, ccycd,Convert.ToDouble(amt));
                            //}                
                        }
                    }
                    else
                    {
                        Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Khong co dien trong host", 1);
                    }
                }
                else
                {
                    Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
                }
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
            }
            return isresult;
        }

        public int INSERT_VCB_INQUIRY_HOST(string pF20,
                                             string pF32A,
                                             string pF50K,
                                             string pF53B,
                                             string pF57D,
                                             string pF59,
                                             string pF70,
                                             string pF71A,
                                             string pCHECK_KEY,
                                             int pSTATUS,
                                             string pCONTENT,
                                             string pRMACNO,
            string ccycd,
            Double amt)
        {

            OracleParameter[] oraParas ={new OracleParameter("pF20", OracleType.NVarChar,10),
                                           new OracleParameter("pF32A", OracleType.NVarChar,50),
                                           new OracleParameter("pF50K", OracleType.NVarChar,50),
                                           new OracleParameter("pF53B", OracleType.NVarChar,20),
                                           new OracleParameter("pF57D", OracleType.NVarChar,100),
                                           new OracleParameter("pF59", OracleType.NVarChar,100),
                                           new OracleParameter("pF70", OracleType.NVarChar,20),
                                           new OracleParameter("pF71A", OracleType.NVarChar,3),
                                           new OracleParameter("pCHECK_KEY", OracleType.NVarChar,50),                                       
                                           new OracleParameter("pSTATUS", OracleType.Number,1),
                                           new OracleParameter("pCONTENT", OracleType.NVarChar,2000),
                                           new OracleParameter("pRMACNO", OracleType.NVarChar,20),
                                           new OracleParameter("pCCYCD", OracleType.VarChar,3),
                                           new OracleParameter("pAMT", OracleType.Double,19)};
            try
            {
                oraParas[0].Value = pF20;
                oraParas[1].Value = pF32A;
                oraParas[2].Value = pF50K;
                oraParas[3].Value = pF53B;
                oraParas[4].Value = pF57D;
                oraParas[5].Value = pF59;
                oraParas[6].Value = pF70;
                oraParas[7].Value = pF71A;
                oraParas[8].Value = pCHECK_KEY;
                oraParas[9].Value = pSTATUS;
                oraParas[10].Value = pCONTENT;
                oraParas[11].Value = pRMACNO;
                oraParas[12].Value = ccycd;
                oraParas[13].Value = amt;

                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_VCB_INQUIRY_HOST", CommandType.StoredProcedure, oraParas);

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, "Insert false RM_SIBS_QUERY" + ex.Message, 1);
                return -1;
            }
        }

        public int INSERT_VCB_CONTENT_HOST(string pRDBR,
                                             string pTLBF09,
                                             string pTLBF01,
                                             string pRMPB40,
                                             string pRMBENA,
                                             string pRMAMT,
                                             string pRMCURR,
                                             string pRMSNME,
                                             string pRDEFTH,
                                             string pTLBAFM,
                                             string pTLBRMK)
        {

            OracleParameter[] oraParas ={new OracleParameter("pRDBR", OracleType.NVarChar,1000),
                                           new OracleParameter("pTLBF09", OracleType.NVarChar,1000),
                                           new OracleParameter("pTLBF01", OracleType.NVarChar,1000),
                                           new OracleParameter("pRMPB40", OracleType.NVarChar,1000),
                                           new OracleParameter("pRMBENA", OracleType.NVarChar,1000),
                                           new OracleParameter("pRMAMT",  OracleType.NVarChar,1000),
                                           new OracleParameter("pRMCURR", OracleType.NVarChar,1000),
                                           new OracleParameter("pRMSNME", OracleType.NVarChar,1000),
                                           new OracleParameter("pRDEFTH", OracleType.NVarChar,1000),                                       
                                           new OracleParameter("pTLBAFM", OracleType.NVarChar,1000),
                                           new OracleParameter("pTLBRMK", OracleType.NVarChar,1000)};
      
            try
            {
                oraParas[0].Value = pRDBR;
                oraParas[1].Value = pTLBF09;
                oraParas[2].Value = pTLBF01;
                oraParas[3].Value = pRMPB40;
                oraParas[4].Value = pRMBENA;
                oraParas[5].Value = pRMAMT;
                oraParas[6].Value = pRMCURR;
                oraParas[7].Value = pRMSNME;
                oraParas[8].Value = pRDEFTH;
                oraParas[9].Value = pTLBAFM;
                oraParas[10].Value = pTLBRMK;

                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_VCB_CONTENT_HOST", CommandType.StoredProcedure, oraParas);

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, "Insert false RM_SIBS_QUERY" + ex.Message, 1);
                return -1;
            }
        }


    }
}
