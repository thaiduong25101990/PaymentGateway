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
//' =============================================
//' Template: BusinessObject.xslt 17/10/2006
//' Author:	Nguyen duc quy
//' Create date:	06/006/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class ALLCODEController
    {

        //----------------------------------------------------------------
        public DataSet GET_ALL_ALL_CODE(string sSWMSTS, string sPROCESSSTS, string sDEPARTMENT,
            string sMSG_SRC, string sMSG_DIRECTION, string sGETYPE, string sMSG_DIREC_S, string sMSG_DIREC_P)
        {
            return ALLCODEDP.Instance().GET_ALL_ALL_CODE(sSWMSTS, sPROCESSSTS, sDEPARTMENT,
            sMSG_SRC, sMSG_DIRECTION, sGETYPE, sMSG_DIREC_S, sMSG_DIREC_P);
        }

        //ham lay trang thai Processsts cua xu ly thu cong voi menh de where truyen vao
        public DataTable PROCESSTS_STATUS(string pWhere,out DataTable _dt)
        {
            return ALLCODEDP.Instance().PROCESSTS_STATUS(pWhere,out _dt);
        }

        public DataTable RESEND_STATUS(string pWhere, out DataTable _dt)
        {
            return ALLCODEDP.Instance().RESEND_STATUS(pWhere, out _dt);
        }

        //ham lay trang thai Swmsts cua xu ly thu cong voi menh de where truyen vao
        public DataTable SWMSTS_STATUS(string pWhere, out DataTable _dt)
        {
            return ALLCODEDP.Instance().SWMSTS_STATUS(pWhere, out _dt);
        }

        public DataSet SWMSTS_PROCESSSTS(string sSWMSTS, string sPROCESSSTS, string sDIRECTION)
        {
            return ALLCODEDP.Instance().SWMSTS_PROCESSSTS(sSWMSTS, sPROCESSSTS, sDIRECTION);
        }

        public DataSet SWMSTS_PROCESSSTS(string sSWMSTS, string sPROCESSSTS)
        {
            return ALLCODEDP.Instance().SWMSTS_PROCESSSTS(sSWMSTS, sPROCESSSTS);
        }

        //lay trang thai status va tad cua forward
        public DataSet STATUS_TAD(string pTELLERID)
        {
            return ALLCODEDP.Instance().STATUS_TAD( pTELLERID);
        }

        //lay trang thai STATUS cho IBPS
        public DataSet IBPS_STATUS()
        {
            return ALLCODEDP.Instance().IBPS_STATUS();
        }
        //lay trang thai STATUS cho VCB
        public DataSet VCB_STATUS()
        {
            return ALLCODEDP.Instance().VCB_STATUS();
        }

        //lay trang thai STATUS cho TTSP
        public DataSet TTSP_STATUS()
        {
            return ALLCODEDP.Instance().TTSP_STATUS();
        }


        //-----------------------------------------------------------------
        public DataSet GetQ()
        {
            return ALLCODEDP.Instance().GetQ();
        }


        public DataSet GetALLCODE21(string pCDNAME, string pGWTYPE)
        {
            return ALLCODEDP.Instance().GetALLCODE21(pCDNAME, pGWTYPE);
        }

        //public DataSet GetALLCODE3(string pCDNAME, string pGWTYPE, string pDirection)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE3(pCDNAME, pGWTYPE,pDirection);
        //}
        //public DataTable GetALLCODE4(string pCONTENT, string pGWTYPE, string pDirection)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE4(pCONTENT, pGWTYPE, pDirection);
        //}
        //public DataTable GetALLCODE5(string pCONTENT, string pGWTYPE)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE5(pCONTENT, pGWTYPE);
        //}
        //DataTable GetALLCODE5(string pCONTENT, string pGWTYPE)





        public DataTable GetALLCODE(string pCDNAME, string strGWtype)
        {
            return ALLCODEDP.Instance().GetALLCODE(pCDNAME, strGWtype);
        }
        //public DataTable GetALLCODE(string pCDNAME, string strGWtype, string strCDVal)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE(pCDNAME, strGWtype, strCDVal);
        //}
        //public DataSet GetALLCODE_Area(string pCDNAME, string CdVal, string strGWtype)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE_Area(pCDNAME,CdVal, strGWtype);
        //}
        //public DataSet GetALLCODE_Direc(string pCDNAME, string strGWTYPE, string strDirection)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE_Direc(pCDNAME, strGWTYPE, strDirection);
        //}

        //public DataSet GetALLCODE_Direc1(string pCDNAME, string strGWTYPE, string strDirection)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE_Direc1(pCDNAME, strGWTYPE, strDirection);
        //}

        //public DataTable GetALLCODE_code(string pCONTENT)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE_code(pCONTENT);
        //}
        public DataTable GetALLCODE_code1(string pCONTENT, string pCDNAME)
        {
            return ALLCODEDP.Instance().GetALLCODE_code1(pCONTENT, pCDNAME);
        }

        //public DataTable GetALLCODE_SYSTEM(string pCONTENT, string pCDNAME)
        //{
        //    return ALLCODEDP.Instance().GetALLCODE_SYSTEM(pCONTENT, pCDNAME);
        //}

        public DataTable GetALLCODE_SWIFT(string pCONTENT, string pCDNAME, string pGWTPYE)
        {
            return ALLCODEDP.Instance().GetALLCODE_SWIFT(pCONTENT, pCDNAME, pGWTPYE);
        }
        //public DataTable GEEET(string pCDVAL, string pCDNAME, string pGWTYPE)
        //{
        //    return ALLCODEDP.Instance().GEEET(pCDVAL, pCDNAME, pGWTYPE);
        //}

        public DataSet GetALLCODE_DATA(string pCDNAME, string pCDVAL)
        {
            return ALLCODEDP.Instance().GetALLCODE_DATA(pCDNAME, pCDVAL);
        }
        public DataTable GetALLCODE_DATA1(string pCONTENT, string pGWTYPE)
        {
            return ALLCODEDP.Instance().GetALLCODE_DATA1(pCONTENT, pGWTYPE);
        }


        public DataTable GetALLCODE(string pCDNAME)
        {
            return ALLCODEDP.Instance().GetALLCODE(pCDNAME);
        }
        public DataSet GetALLCODE_D(string pCDNAME)
        {
            return ALLCODEDP.Instance().GetALLCODE_D(pCDNAME);
        }
        public DataSet GetJob(string pOWNER)
        {
            return ALLCODEDP.Instance().GetJob(pOWNER);
        }
        public DataSet SearchJob(string pOWNER, string pJobname)
        {
            return ALLCODEDP.Instance().SearchJob(pOWNER, pJobname);
        }

        public DataSet GetQueue(string pOWNER)
        {
            return ALLCODEDP.Instance().GetQueue(pOWNER);
        }
        public DataSet SearchQueue(string pOWNER, string pQUEUENAME)
        {
            return ALLCODEDP.Instance().SearchQueue(pOWNER, pQUEUENAME);
        }
        public int Start_Job(string pJob_Name)
        {
            return ALLCODEDP.Instance().Start_Job(pJob_Name);
        }
        public int Stop_Job(string pJob_Name)
        {
            return ALLCODEDP.Instance().Stop_Job(pJob_Name);
        }
        public int Stop_Queue(string pQueue_Name)
        {
            return ALLCODEDP.Instance().Stop_Queue(pQueue_Name);
        }
        public int Start_Queue(string pQueue_Name)
        {
            return ALLCODEDP.Instance().Start_Queue(pQueue_Name);
        }
        public DataSet GetALLCODE_Description(string strStatus, string strGWtype, string strContent)
        {
            return ALLCODEDP.Instance().GetALLCODE_Description(strStatus, strGWtype, strContent);
        }
        public DataTable GetALLCODE_LIST(string pstrGWtype, string strGWtype)
        {
            return ALLCODEDP.Instance().GetALLCODE_LIST(pstrGWtype, strGWtype);
        }
        //public DataTable Search_T_S(string pWHERE)
        //{
        //    return ALLCODEDP.Instance().Search_T_S(pWHERE);
        //}
    }
}
