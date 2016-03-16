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

namespace BRANCH_Inquiry
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
                String pCHANNEL = "IBPS#SWIFT";/*Dung tao vong for de quet du lieu*/
                String[] pGWTYPE = pCHANNEL.Split(new String[] { "#" }, StringSplitOptions.None);
                for (int p = 0; p < pGWTYPE.Count<String>(); p++)
                {
                    string vGWTYPE = pGWTYPE[p].ToString();
                    DataTable _dt = new DataTable();

                    if (vGWTYPE == "SWIFT")/*Bang ma cua SWIFT*/
                    {
                        strSql = "Select SWBCOD,SWBNAM,SWBNA1,SWBRNM,SWBAD1,SWBAD2,SWBAD3,SWCTRY,SWCONT from swbicd ";
                    }
                    else if (vGWTYPE == "IBPS")/*Bang ma cua IBPS*/
                    {
                        strSql = "Select RBBRN,RBID,RBREGN,RBDESC,RBDES2,RBRSV1,RBRSV2 from rmsid ";
                    }
                    isresult = 0;
                    _dt = Lib.ExcuteDataTableODBC(strSql);
                    if (_dt != null)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                if (vGWTYPE == "SWIFT")/*Bang ma cua SWIFT*/
                                {
                                    INSERT_SWIFT_CODE(_dt.Rows[i]["SWBCOD"].ToString().Trim(),
                                        _dt.Rows[i]["SWBNAM"].ToString().Trim(),
                                        _dt.Rows[i]["SWBNA1"].ToString().Trim(),
                                        _dt.Rows[i]["SWBRNM"].ToString().Trim(),
                                        _dt.Rows[i]["SWBAD1"].ToString().Trim(),
                                        _dt.Rows[i]["SWBAD2"].ToString().Trim(),
                                        _dt.Rows[i]["SWBAD3"].ToString().Trim(),
                                        _dt.Rows[i]["SWCTRY"].ToString().Trim(),
                                        _dt.Rows[i]["SWCONT"].ToString().Trim());
                                }
                                else if (vGWTYPE == "IBPS")
                                {
                                    INSERT_IBPS_CODE(_dt.Rows[i]["RBBRN"].ToString().Trim(),
                                        _dt.Rows[i]["RBID"].ToString().Trim(),
                                        _dt.Rows[i]["RBREGN"].ToString().Trim(),
                                        _dt.Rows[i]["RBDESC"].ToString().Trim(),
                                        _dt.Rows[i]["RBDES2"].ToString().Trim(),
                                        _dt.Rows[i]["RBRSV1"].ToString().Trim(),
                                        _dt.Rows[i]["RBRSV2"].ToString().Trim());
                                }
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
            }
            catch
            {
                Lib.WriteLogDB(0, VCB_Inquiry.SERVICE_NAME + " Connect Server SIBS failed", 1);
            }
            return isresult;
        }

        public int INSERT_SWIFT_CODE(string pSWBCOD,
                                       string pSWBNAM,
                                       string pSWBNA1,
                                       string pSWBRNM,
                                       string pSWBAD1,
                                       string pSWBAD2,
                                       string pSWBAD3,
                                       string pSWCTRY,
                                       string pSWCONT)
        {

            OracleParameter[] oraParas ={new OracleParameter("pSWBCOD", OracleType.VarChar,12),
                                           new OracleParameter("pSWBNAM", OracleType.VarChar,35),
                                           new OracleParameter("pSWBNA1", OracleType.VarChar,35),
                                           new OracleParameter("pSWBRNM", OracleType.VarChar,35),
                                           new OracleParameter("pSWBAD1", OracleType.VarChar,35),
                                           new OracleParameter("pSWBAD2", OracleType.VarChar,35),
                                           new OracleParameter("pSWBAD3", OracleType.VarChar,35),
                                           new OracleParameter("pSWCTRY", OracleType.VarChar,35),
                                           new OracleParameter("pSWCONT", OracleType.VarChar,35)};
            try
            {
                oraParas[0].Value = pSWBCOD;
                oraParas[1].Value = pSWBNAM;
                oraParas[2].Value = pSWBNA1;
                oraParas[3].Value = pSWBRNM;
                oraParas[4].Value = pSWBAD1;
                oraParas[5].Value = pSWBAD2;
                oraParas[6].Value = pSWBAD3;
                oraParas[7].Value = pSWCTRY;
                oraParas[8].Value = pSWCONT;


                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_SWIFT_CODE", CommandType.StoredProcedure, oraParas);

            }
            catch (Exception ex)
            {
                Lib.WriteLogDB(0, "Insert false RM_SIBS_QUERY" + ex.Message, 1);
                return -1;
            }
        }


        public int INSERT_IBPS_CODE(string pRBBRN,
                                        string pRBID,
                                        string pRBREGN,
                                        string pRBDESC,
                                        string pRBDES2,
                                        string pRBRSV1,
                                        string pRBRSV2
)
        {

            OracleParameter[] oraParas ={new OracleParameter("pRBBRN", OracleType.VarChar,12),
                                           new OracleParameter("pRBID", OracleType.VarChar,35),
                                           new OracleParameter("pRBREGN", OracleType.VarChar,35),
                                           new OracleParameter("pRBDESC", OracleType.VarChar,35),
                                           new OracleParameter("pRBDES2", OracleType.VarChar,35),
                                           new OracleParameter("pRBRSV1", OracleType.VarChar,35),
                                           new OracleParameter("pRBRSV2", OracleType.VarChar,35)};
            try
            {
                oraParas[0].Value = pRBBRN;
                oraParas[1].Value = pRBID;
                oraParas[2].Value = pRBREGN;
                oraParas[3].Value = pRBDESC;
                oraParas[4].Value = pRBDES2;
                oraParas[5].Value = pRBRSV1;
                oraParas[6].Value = pRBRSV2;


                return Lib.ExecuteNonQuery("SIBS_QEURY_PROCESS.INSERT_IBPS_CODE", CommandType.StoredProcedure, oraParas);

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
