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
using System.Collections;
using System.Linq;

namespace BRIBPS_OL3_Inquiry
{
    public class ProcessInquiry
    {
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
                String pCHANNEL = "OL2#OL3";/*Dung tao vong for de quet du lieu*/
                String[] pGWTYPE = pCHANNEL.Split(new String[] { "#" }, StringSplitOptions.None);
                for (int p = 0; p < pGWTYPE.Count<String>(); p++)
                {
                    string vSP = pGWTYPE[p].ToString();
                    if (vSP == "OL2")/*Bang ma cua SWIFT*/
                    {
                        GET_VCB_OL2();
                    }
                    else if (vSP == "OL3")
                    {
                        GET_IBPS_OL3();
                    }
                }
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
            }
            return isresult;
        }
        private void GET_VCB_OL2()
        {
            try
            {
                strSql = "";
                string m_vRMHIST_DATE = "";
                string pLibHost = Lib.vHost_lib;
                string vRMHIST = Lib.vRMHIST;
                int iRMHIST_DATE = Convert.ToInt32(Lib.vRMHIST_DATE);
                string vIBM_QUERY = Lib.vIBM_QUERY;
                DataTable _dt = new DataTable();
                /*Neu moi truong that thi them hai cau lenh nay  and a.rmcbnk= '30005'  and b.tlbbrc=11   */
                if (vIBM_QUERY == "0")//Lay du lieu 
                {
                    m_vRMHIST_DATE = "SYSDATE";
                    /*Lay lai noi dung cua truong 50k */
                    strSql = strSql + "Select e.CTTCIF,e.CTNAME,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C," + pLibHost + ".CFTNAM e ";
                    strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2' and a.rmcbnk= '30005' )) ";//(a.rmprdc='OL2' and a.rmcbnk= '30005' )
                    strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '    and b.TLBCOR='N' and a.RMACIF = e.CTTCIF  ";//and b.tlbbrc=11 
                    strSql = strSql + "  and e.CTTCIF not in (select CFCIFN from " + pLibHost + ".CFMAST)  ";
                    
                    //strSql = strSql + "Select c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    //strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    //strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C ";
                    //strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2'   and a.rmcbnk= '30005' )) ";
                    //strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '  and b.tlbbrc=11   and b.TLBCOR='N' ";         
                }
                else if (vIBM_QUERY == "1")//Lay du lieu bang hist
                {
                    m_vRMHIST_DATE = Lib.vRMHIST_DATE;

                    strSql = strSql + "Select e.CTTCIF,e.CTNAME,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLHIST B, " + vRMHIST + ".RMHIST C," + pLibHost + ".CFTNAM e ";
                    strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2' and a.rmcbnk= '30005' )) ";
                    strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' ' and b.tlbbrc=11  and b.TLBCOR='N' and b.TLBTDT = " + iRMHIST_DATE + " ";
                    strSql = strSql + "  and a.RMACIF = e.CTTCIF  and e.CTTCIF not in (select CFCIFN from " + pLibHost + ".CFMAST)  ";

                    //strSql = strSql + "Select c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    //strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    //strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLHIST B, " + vRMHIST + ".RMHIST C ";
                    //strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2')) ";
                    //strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '  and b.TLBCOR='N' and b.TLBTDT = " + iRMHIST_DATE + " ";        // and b.TLBTDT = 50410         
                }                
                
                _dt = Lib.ExcuteDataTableODBC(strSql);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {                            
                            /*Lay du lieu check trung la so RM*/
                            //if (_dt.Rows[i]["CTTCIF"].ToString().Trim().Substring(0, 3) == "295")
                            //{
                                INSERT_IBPS_OL3("VCB",
                                    _dt.Rows[i]["TLBF01"].ToString().Trim(),
                                    _dt.Rows[i]["RMBENA"].ToString().Trim(),
                                    _dt.Rows[i]["RMAMT"].ToString().Trim(), 
                                    _dt.Rows[i]["CTNAME"].ToString().Trim(), //                                    _dt.Rows[i]["RMSNME"].ToString().Trim(),
                                    _dt.Rows[i]["RMACNO"].ToString().Trim(),
                                    _dt.Rows[i]["TLBAFM"].ToString().Trim(),
                                    _dt.Rows[i]["TLBRMK"].ToString().Trim(),
                                    _dt.Rows[i]["TLBF09"].ToString().Trim(),
                                    _dt.Rows[i]["TLBID"].ToString().Trim(),
                                    _dt.Rows[i]["RMPRDC"].ToString().Trim(),
                                    _dt.Rows[i]["RDEFTH"].ToString().Trim(),
                                    _dt.Rows[i]["RMPB40"].ToString().Trim(),
                                    _dt.Rows[i]["TLBDEL"].ToString().Trim(),
                                    _dt.Rows[i]["TLBCOR"].ToString().Trim(),
                                    _dt.Rows[i]["RMDIS7"].ToString().Trim(),
                                    _dt.Rows[i]["RMSTS7"].ToString().Trim(),
                                    "OL2",
                                    _dt.Rows[i]["rmcurr"].ToString().Trim(),
                                    _dt.Rows[i]["rdbr"].ToString().Trim(),
                                    m_vRMHIST_DATE
                                    );
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
                DataTable _dtOl2 = new DataTable();
                strSql = "";
                /*Lay tiep dien OL2,OL3 cua VCB trong ngay*/
                if (vIBM_QUERY == "0")//Lay du lieu 
                {
                    m_vRMHIST_DATE = "SYSDATE";
                    strSql = strSql + "Select d.CFCIFN,d.CFNA1, d.CFNA1A,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C," + pLibHost + ".CFMAST d ";
                    strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2'  and a.rmcbnk= '30005')) ";//(a.rmprdc='OL2'  and a.rmcbnk= '30005')
                    strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '  and b.tlbbrc=11   and b.TLBCOR='N' and a.RMACIF = d.CFCIFN ";//and b.tlbbrc=11
                }
                else if (vIBM_QUERY == "1")//Lay du lieu bang hist
                {
                    strSql = strSql + "Select d.CFCIFN,d.CFNA1, d.CFNA1A,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLHIST B, " + vRMHIST + ".RMHIST C," + pLibHost + ".CFMAST d  ";
                    strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2' and a.rmcbnk= '30005' )) ";
                    strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' ' and b.tlbbrc=11  and b.TLBCOR='N' and b.TLBTDT = " + iRMHIST_DATE + "  and a.RMACIF = d.CFCIFN ";        // and b.TLBTDT = 50410         
                }
                _dtOl2 = Lib.ExcuteDataTableODBC(strSql);
                if (_dtOl2 != null)
                {
                    if (_dtOl2.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtOl2.Rows.Count; i++)
                        {
                            /*Lay du lieu check trung la so RM*/
                            INSERT_IBPS_OL3("VCB",
                                _dtOl2.Rows[i]["TLBF01"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMBENA"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMAMT"].ToString().Trim(),
                                (_dtOl2.Rows[i]["CFNA1"].ToString().Trim() + _dtOl2.Rows[i]["CFNA1A"].ToString().Trim()).Replace("\0",""), //_dtOl2.Rows[i]["RMSNME"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMACNO"].ToString().Trim(),
                                _dtOl2.Rows[i]["TLBAFM"].ToString().Trim(),
                                _dtOl2.Rows[i]["TLBRMK"].ToString().Trim(),
                                _dtOl2.Rows[i]["TLBF09"].ToString().Trim(),
                                _dtOl2.Rows[i]["TLBID"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMPRDC"].ToString().Trim(),
                                _dtOl2.Rows[i]["RDEFTH"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMPB40"].ToString().Trim(),
                                _dtOl2.Rows[i]["TLBDEL"].ToString().Trim(),
                                _dtOl2.Rows[i]["TLBCOR"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMDIS7"].ToString().Trim(),
                                _dtOl2.Rows[i]["RMSTS7"].ToString().Trim(),
                                "OL2",
                                _dtOl2.Rows[i]["rmcurr"].ToString().Trim(),
                                _dtOl2.Rows[i]["rdbr"].ToString().Trim(),
                                m_vRMHIST_DATE
                                );
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
                DataTable _dtOl22 = new DataTable();
                strSql = "";
                if (vIBM_QUERY == "0")//Lay du lieu 
                {
                    m_vRMHIST_DATE = "SYSDATE";//Neu la moi truong that thay STHISTRN == SVHISPV51
                    /*Lay lai noi dung cua truong 50k */
                    strSql = strSql + "Select c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,a.rmcurr,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " FROM " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, SVHISPV51.RMHIST C ";
                    strSql = strSql + " WHERE ((b.tlbf09='120201005' and  (b.TLBTCD='8163' or b.TLBTCD= '8173' or b.TLBTCD='8178')) or  (a.rmprdc='OL2' and a.rmcbnk= '30005' )) ";//(a.rmprdc='OL2' and a.rmcbnk= '30005' )
                    strSql = strSql + " and b.tlbf01=a.rmacno and c.rdacct=b.tlbf01 and c.rdtxid='CRRM' and b.tltxok='Y' and  b.TLBDEL=' '    and b.TLBCOR='N' ";//and b.tlbbrc=11 
                    strSql = strSql + "  and a.RMDIS7 <> a.RMSTS7 ";
                    _dtOl22 = Lib.ExcuteDataTableODBC(strSql);
                    if (_dtOl22 != null)
                    {
                        if (_dtOl22.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dtOl22.Rows.Count; i++)
                            {
                                UPDATE_IBPS_OL3(_dtOl22.Rows[i]["RMACNO"].ToString().Trim(),
                                                   _dtOl22.Rows[i]["RDEFTH"].ToString().Trim(),
                                                   "OL2",
                                                   "1",
                                                   " ");
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
                UPDATE_IBPS_OL3(" ", " ", "OL2", "ALL"," ");/*Update lai trang thai =0 de convert dien*/
            }
            catch(Exception ex)
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
            }
        }

        public int UPDATE_IBPS_OL3(string pRMACNO,
                                       string pRDEFTH,
                                       string pMANUFACTURES,
                                       string pUPDATE,
                                       string pRDBR)
        {

            OracleParameter[] oraParas ={new OracleParameter("pRMACNO", OracleType.VarChar,20),                                          
                                           new OracleParameter("pRDEFTH", OracleType.VarChar,220),                                           
                                           new OracleParameter("pMANUFACTURES", OracleType.VarChar,3),
                                           new OracleParameter("pUPDATE", OracleType.VarChar,3),
                                           new OracleParameter("pRDBR_HOST", OracleType.VarChar,5)};
            try
            {
                oraParas[0].Value = pRMACNO;
                oraParas[1].Value = pRDEFTH;
                oraParas[2].Value = pMANUFACTURES;
                oraParas[3].Value = pUPDATE;
                oraParas[4].Value = pRDBR;

                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.UPDATE_IBPS_OL3", CommandType.StoredProcedure, oraParas);

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, "Update false IBPS_OL3" + ex.Message, 1);
                return -1;
            }
        }

        private void GET_IBPS_OL3()
        {
            try
            {
                strSql = "";
                string m_vRMHIST_DATE = "";
                string pLibHost = Lib.vHost_lib;
                string vRMHIST = Lib.vRMHIST;
                int iRMHIST_DATE = Convert.ToInt32(Lib.vRMHIST_DATE);
                string vIBM_QUERY = Lib.vIBM_QUERY;
                //string pSIBS_BANK_CODE = "";
                //DataTable _dtTAD = new DataTable();
                //_dtTAD = Lib.GETTAD();                
                strSql = "";
                DataTable _dt = new DataTable();
                if (vIBM_QUERY == "0")//Lay du lieu 
                {
                    //m_vRMHIST_DATE = "SYSDATE";
                    //strSql = strSql + "Select a.rmacif,e.CTTCIF,e.CTNAME,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    //strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    //strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C," + pLibHost + ".CFTNAM e ";
                    //strSql = strSql + " WHERE a.rmpabr = 11   and a.rmprdc = 'OL3'  and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    //strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' and ";
                    //strSql = strSql + " left(b.TLBID, 5) = 'PC011' and b.tlbdel = '' and b.tlbcor = 'N' and a.rmdis7 = a.rmsts7 and a.RMACIF = e.CTTCIF  ";
                    //strSql = strSql + "  and e.CTTCIF not in (select CFCIFN from " + pLibHost + ".CFMAST) and b.TLBTCD= 8163  ORDER BY b.tlbf01,c.rdbr desc";
                    m_vRMHIST_DATE = "SYSDATE";
                    strSql = strSql + "Select a.rmpabr,a.rmacif,e.CTTCIF,e.CTNAME,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C," + pLibHost + ".CFTNAM e ";
                    strSql = strSql + " WHERE a.rmprdc = 'OL3'  and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' and a.RMACIF = e.CTTCIF";
                    strSql = strSql + " and b.tlbdel = '' and b.tlbcor = 'N' ";
                    //strSql = strSql + " b.tlbdel = '' and b.tlbcor = 'N' and a.rmdis7 = a.rmsts7 and a.RMACIF = e.CTTCIF  ";
                    //strSql = strSql + "  and e.CTTCIF not in (select CFCIFN from " + pLibHost + ".CFMAST) and b.TLBTCD= 8163  ORDER BY b.tlbf01,c.rdbr desc";
                }
                else
                {
                    m_vRMHIST_DATE = Lib.vRMHIST_DATE;
                    strSql = strSql + "Select  e.CTTCIF,e.CTNAME,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLHIST B, " + pLibHost + ".RMHIST C," + pLibHost + ".CFTNAM e ";
                    strSql = strSql + " WHERE a.rmpabr = 11   and a.rmprdc = 'OL3'   and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' and ";
                    strSql = strSql + " left(b.TLBID, 5) = 'PC011' and b.tlbdel = '' and b.tlbcor = 'N' and a.rmdis7 = a.rmsts7 and b.TLBTDT = " + iRMHIST_DATE + " ";
                    strSql = strSql + "  and e.CTTCIF not in (select CFCIFN from " + pLibHost + ".CFMAST)  ORDER BY b.tlbf01,c.rdbr desc";
                }
                _dt = Lib.ExcuteDataTableODBC(strSql);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            /*Lay du lieu check trung la so RM*/

                            INSERT_IBPS_OL3(_dt.Rows[i]["rmpabr"].ToString().Trim(),
                                _dt.Rows[i]["TLBF01"].ToString().Trim(),
                                _dt.Rows[i]["RMBENA"].ToString().Trim(),
                                _dt.Rows[i]["RMAMT"].ToString().Trim(),
                                _dt.Rows[i]["CTNAME"].ToString().Trim(),
                                //_dt.Rows[i]["RMSNME"].ToString().Trim(),
                                _dt.Rows[i]["RMACNO"].ToString().Trim(),
                                _dt.Rows[i]["TLBAFM"].ToString().Trim(),
                                _dt.Rows[i]["TLBRMK"].ToString().Trim(),
                                _dt.Rows[i]["TLBF09"].ToString().Trim(),
                                _dt.Rows[i]["TLBID"].ToString().Trim(),
                                _dt.Rows[i]["RMPRDC"].ToString().Trim(),
                                _dt.Rows[i]["RDEFTH"].ToString().Trim(),
                                _dt.Rows[i]["RMPB40"].ToString().Trim(),
                                _dt.Rows[i]["TLBDEL"].ToString().Trim(),
                                _dt.Rows[i]["TLBCOR"].ToString().Trim(),
                                _dt.Rows[i]["RMDIS7"].ToString().Trim(),
                                _dt.Rows[i]["RMSTS7"].ToString().Trim(),
                                "OL3",
                                "",
                                _dt.Rows[i]["rdbr"].ToString().Trim(),
                                m_vRMHIST_DATE);
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
                /**/
                DataTable _dtOl3 = new DataTable();
                strSql = "";
                if (vIBM_QUERY == "0")//Lay du lieu 
                {
                    //m_vRMHIST_DATE = "SYSDATE";
                    //strSql = strSql + "Select d.CFCIFN,d.CFNA1, d.CFNA1A,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    //strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    //strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C," + pLibHost + ".CFMAST d ";
                    //strSql = strSql + " WHERE a.rmpabr = 11   and a.rmprdc = 'OL3'  and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    //strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' and ";
                    //strSql = strSql + " left(b.TLBID, 5) = 'PC011' and b.tlbdel = '' and b.tlbcor = 'N' and a.rmdis7 = a.rmsts7 and a.RMACIF = d.CFCIFN and b.TLBTCD= 8163 ";

                    m_vRMHIST_DATE = "SYSDATE";
                    strSql = strSql + "Select a.rmpabr,d.CFCIFN,d.CFNA1, d.CFNA1A,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, " + pLibHost + ".RMDETL C," + pLibHost + ".CFMAST d ";
                    strSql = strSql + " WHERE a.rmprdc = 'OL3'  and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' and a.RMACIF = d.CFCIFN";
                    strSql = strSql + " and b.tlbdel = '' and b.tlbcor = 'N' ";
                    //strSql = strSql + " b.tlbdel = '' and b.tlbcor = 'N' and a.rmdis7 = a.rmsts7 and a.RMACIF = d.CFCIFN and b.TLBTCD= 8163 ";
                    
                }
                else
                {
                    m_vRMHIST_DATE = Lib.vRMHIST_DATE;
                    strSql = strSql + "Select  d.CFCIFN,d.CFNA1, d.CFNA1A,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLHIST B, " + pLibHost + ".RMHIST C," + pLibHost + ".CFMAST d ";
                    strSql = strSql + " WHERE a.rmpabr = 11   and a.rmprdc = 'OL3'   and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' and ";
                    strSql = strSql + " left(b.TLBID, 5) = 'PC011' and b.tlbdel = '' and b.tlbcor = 'N' and a.rmdis7 = a.rmsts7 and b.TLBTDT = " + iRMHIST_DATE + "  and a.RMACIF = d.CFCIFN ";
                    //strSql = strSql + "  and e.CTTCIF not in (select CFCIFN from " + pLibHost + ".CFMAST)  ORDER BY b.tlbf01,c.rdbr desc";
                }
                _dtOl3 = Lib.ExcuteDataTableODBC(strSql);
                if (_dtOl3 != null)
                {
                    if (_dtOl3.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtOl3.Rows.Count; i++)
                        {
                            /*Lay du lieu check trung la so RM*/

                            INSERT_IBPS_OL3(_dtOl3.Rows[i]["rmpabr"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBF01"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMBENA"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMAMT"].ToString().Trim(),
                                (_dtOl3.Rows[i]["CFNA1"].ToString().Trim() + _dtOl3.Rows[i]["CFNA1A"].ToString().Trim()).Replace("\0", ""),
                                //_dtOl3.Rows[i]["RMSNME"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMACNO"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBAFM"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBRMK"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBF09"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBID"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMPRDC"].ToString().Trim(),
                                _dtOl3.Rows[i]["RDEFTH"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMPB40"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBDEL"].ToString().Trim(),
                                _dtOl3.Rows[i]["TLBCOR"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMDIS7"].ToString().Trim(),
                                _dtOl3.Rows[i]["RMSTS7"].ToString().Trim(),
                                "OL3",
                                "",
                                _dtOl3.Rows[i]["rdbr"].ToString().Trim(),
                                m_vRMHIST_DATE);
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
                /*Moi them*/
                DataTable _dtOl33 = new DataTable();
                strSql = "";
                if (vIBM_QUERY == "0")//Lay du lieu 
                {
                    m_vRMHIST_DATE = "SYSDATE";//Neu la moi truong that thay STHISTRN == SVHISPV51                    
                    strSql = strSql + "Select a.rmpabr,a.rmacif,c.rdbr,b.tlbf01,a.rmbena,a.rmamt rmamt,a.rmsnme rmsnme,a.rmacno,b.tlbafm,b.tlbrmk,b.tlbf09,b.TLBID,";
                    strSql = strSql + "substring(a.rmprdc, 3, 1) rmprdc,c.rdefth,a.rmpb40,b.Tlbdel,b.TlbCor,a.rmdis7,a.rmsts7";
                    strSql = strSql + " From " + pLibHost + ".RMMAST A, " + pLibHost + ".TLLOG B, SVHISPV51.RMHIST C ";
                    strSql = strSql + " WHERE a.rmprdc = 'OL3'  and a.rmcurr = 'VND'  and (b.tlbf09 = '120101001' or b.tlbf09 = '280601002') ";
                    strSql = strSql + " and b.tlbf01 = a.rmacno   and c.rdacct = b.tlbf01 and c.rdtxid = 'CRRM' and b.tltxok = 'Y' ";
                    strSql = strSql + " and b.tlbdel = '' and b.tlbcor = 'N' and a.RMDIS7 <> a.RMSTS7 ";

                    _dtOl33 = Lib.ExcuteDataTableODBC(strSql);
                    if (_dtOl33 != null)
                    {
                        if (_dtOl33.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dtOl33.Rows.Count; i++)
                            {
                                UPDATE_IBPS_OL3(_dtOl33.Rows[i]["RMACNO"].ToString().Trim(),
                                                   _dtOl33.Rows[i]["RDEFTH"].ToString().Trim(),
                                                   "OL3",
                                                   "1",
                                                   _dtOl33.Rows[i]["rdbr"].ToString().Trim());
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
                UPDATE_IBPS_OL3(" ", " ", "OL3", "ALL"," ");/*Update lai trang thai =0 de convert dien*/
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
            }
        }

        public int INSERT_IBPS_OL3(string pRDBR,
                                   string pTLBF01,
                                   string pRMBENA,
                                   string pRMAMT,
                                   string pRMSNME,
                                   string pRMACNO,
                                   string pTLBAFM,
                                   string pTLBRMK,
                                   string pTLBF09,
                                   string pTLBID,
                                   string pRMPRDC,
                                   string pRDEFTH,
                                   string pRMPB40,
                                   string pTLBDEL,
                                   string pTLBCOR,
                                   string pRMDIS7,
                                   string pRMSTS7,
                                   string pMANUFACTURES,
                                   string pRMCURR,
                                   string pRDBR_HOST,
                                   string pRMHIST_DATE)
        {

            OracleParameter[] oraParas ={new OracleParameter("pRDBR", OracleType.VarChar,10),
                                           new OracleParameter("pTLBF01", OracleType.VarChar,20),
                                           new OracleParameter("pRMBENA", OracleType.VarChar,20),
                                           new OracleParameter("pRMAMT", OracleType.VarChar,20),
                                           new OracleParameter("pRMSNME", OracleType.VarChar,100),
                                           new OracleParameter("pRMACNO", OracleType.VarChar,20),
                                           new OracleParameter("pTLBAFM", OracleType.VarChar,2000),
                                           new OracleParameter("pTLBRMK", OracleType.VarChar,2),
                                           new OracleParameter("pTLBF09", OracleType.VarChar,10),                                       
                                           new OracleParameter("pTLBID", OracleType.VarChar,8),
                                           new OracleParameter("pRMPRDC", OracleType.VarChar,1),
                                           new OracleParameter("pRDEFTH", OracleType.VarChar,220),
                                           new OracleParameter("pRMPB40", OracleType.VarChar,50),
                                           new OracleParameter("pTLBDEL", OracleType.VarChar,15),
                                           new OracleParameter("pTLBCOR", OracleType.VarChar,1),                                       
                                           new OracleParameter("pRMDIS7", OracleType.VarChar,8),
                                           new OracleParameter("pRMSTS7", OracleType.VarChar,8),
                                           new OracleParameter("pMANUFACTURES", OracleType.VarChar,3),
                                           new OracleParameter("pRMCURR", OracleType.VarChar,3),
                                           new OracleParameter("pRDBR_HOST", OracleType.VarChar,5),
                                           new OracleParameter("pDATE", OracleType.VarChar,8)};
            try
            {
                oraParas[0].Value = pRDBR;
                oraParas[1].Value = pTLBF01;
                oraParas[2].Value = pRMBENA;
                oraParas[3].Value = pRMAMT;
                oraParas[4].Value = pRMSNME;
                oraParas[5].Value = pRMACNO;
                oraParas[6].Value = pTLBAFM;
                oraParas[7].Value = pTLBRMK;
                oraParas[8].Value = pTLBF09;
                oraParas[9].Value = pTLBID;
                oraParas[10].Value = pRMPRDC;
                oraParas[11].Value = pRDEFTH;
                oraParas[12].Value = pRMPB40;
                oraParas[13].Value = pTLBDEL;
                oraParas[14].Value = pTLBCOR;
                oraParas[15].Value = pRMDIS7;
                oraParas[16].Value = pRMSTS7;
                oraParas[17].Value = pMANUFACTURES;
                oraParas[18].Value = pRMCURR;
                oraParas[19].Value = pRDBR_HOST;
                oraParas[20].Value = pRMHIST_DATE;
                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_IBPS_OL3", CommandType.StoredProcedure, oraParas);

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
