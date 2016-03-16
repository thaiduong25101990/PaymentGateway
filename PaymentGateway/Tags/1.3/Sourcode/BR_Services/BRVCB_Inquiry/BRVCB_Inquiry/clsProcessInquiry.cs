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
        private string _msg_direction = "";
        private string _Check_key = "";

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
                string strDAY = VCB_Inquiry.sysdate.Day.ToString();//1
                if (strDAY.Length == 1)
                {
                    strDAY = "0" + strDAY;
                }
                string strMONTH = VCB_Inquiry.sysdate.Month.ToString();//0
                if (strMONTH.Length == 1)
                {
                    strMONTH = "0" + strMONTH;
                }
                string strYEAR = VCB_Inquiry.sysdate.Year.ToString().Substring(2, 2);//2
                //-------------------------------
                DataTable _dt = new DataTable();
                //strSql = "Select * FROM SVDATPV51.RMMAST";
                strSql = strSql + "SELECT c.rdbr,b.tlbf09,b.tlbf01,a.rmpb40,a.rmbena,a.rmamt,a.rmcurr,a.rmsnme,c.rdefth,b.tlbafm,b.tlbrmk ";
                strSql = strSql + " FROM SVDATPV51.RMMAST A, SVDATPV51.TLLOG B, SVDATPV51.RMDETL C ";
                strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2' and a.rmcbnk= '30005')) ";
                strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '  and b.tlbbrc=11 and b.TLBCOR='N'";
                isresult = 0;
                _dt = Lib.ExcuteDataTableODBC(strSql);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            _20 = ":20:A" + strDAY + strMONTH + strYEAR;
                            _32A = ":32A:" + strYEAR + strMONTH + strDAY + _dt.Rows[i]["rmamt"] + _dt.Rows[i]["rmbena"].ToString().Replace(".", ",");
                            _50K = ":50K:" + _dt.Rows[i]["rmcurr"];
                            if (_dt.Rows[i]["rmamt"].ToString() == "VND") _53B = ":53B:/0681000012578";
                            if (_dt.Rows[i]["rmamt"].ToString() == "USD") _53B = ":53B:/0681370012414";
                            if (_dt.Rows[i]["rmamt"].ToString() == "EUR") _53B = ":53B:/0681140012429";
                            if (_dt.Rows[i]["rmamt"].ToString() == "GBP") _53B = ":53B:/0681356666661";
                            if (_dt.Rows[i]["rmamt"].ToString() == "AUD") _53B = ":53B:/0681520012461";
                            _57D = ":57D:" + _dt.Rows[i]["tlbf01"].ToString();
                            _59 = ":59:/" + _dt.Rows[i]["rmpb40"].ToString();
                            _70 = ":70:(" + _dt.Rows[i]["tlbf09"].ToString() + ")" + _dt.Rows[i]["rmsnme"].ToString().Substring(3);
                            _Check_key = "";
                            INSERT_VCB_INQUIRY_HOST(_20, _32A, _50K, _53B, _57D, _59, _70, _71A, _Check_key, 1);

                            i = i + 1;
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
                                             int pSTATUS)
        {

            OracleParameter[] oraParas ={new OracleParameter("pF20", OracleType.NVarChar,10),
                                       new OracleParameter("pF32A", OracleType.NVarChar,50),
                                       new OracleParameter("pF50K", OracleType.NVarChar,50),
                                       new OracleParameter("pF53B", OracleType.NVarChar,20),
                                       new OracleParameter("pF57D", OracleType.NVarChar,100),
                                       new OracleParameter("pF59", OracleType.NVarChar,100),
                                       new OracleParameter("pF70", OracleType.NVarChar,20),
                                       new OracleParameter("pF71A", OracleType.NVarChar,3),
                                       new OracleParameter("pCHECK_KEY", OracleType.NVarChar,100),                                       
                                       new OracleParameter("pSTATUS", OracleType.Number,1)};
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

                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_VCB_INQUIRY_HOST", CommandType.StoredProcedure, oraParas);

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, "Insert false RM_SIBS_QUERY" + ex.Message, 1);
                return -1;
            }
        }
    }
}
