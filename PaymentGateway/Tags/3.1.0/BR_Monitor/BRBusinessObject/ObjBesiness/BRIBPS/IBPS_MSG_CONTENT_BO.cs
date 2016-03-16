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
//' Create date:	10/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class IBPS_MSG_CONTENTController
    {

        // Get message detail 
        //update 20081229
        // QuanLD
        public DataTable GetIBPS_MSG_DTL(long lMSG_ID)
        {
            return IBPS_MSG_CONTENTDP.Instance().GetIBPS_MSG_DTL(lMSG_ID);
        }

        public DataTable GetData_print_ibps(string strMsgID, string pBranch, string strUserid)
        {
            return IBPS_MSG_CONTENTDP.Instance().GetData_print_ibps(strMsgID, pBranch,strUserid);
        }

        
        public DataSet IBPS_CONTENT_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            return IBPS_MSG_CONTENTDP.Instance().IBPS_CONTENT_SEARCH(datefrom, dateto, pWhere, out _dtContent);
        }

        public DataSet IBPS_CONTENT_SEARCH_RS(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            return IBPS_MSG_CONTENTDP.Instance().IBPS_CONTENT_SEARCH_RS(datefrom, dateto, pWhere, out _dtContent);
        }

        public DataSet IBPS_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
        {
            return IBPS_MSG_CONTENTDP.Instance().IBPS_CONTENT_SEARCH_ADVANCE(pWhere, out _dtContent);
        }
        public DataSet IBPS_CONTENT_SEARCH_ADVANCE_RS(string pWhere, out DataSet _dtContent)
        {
            return IBPS_MSG_CONTENTDP.Instance().IBPS_CONTENT_SEARCH_ADVANCE_RS(pWhere, out _dtContent);
        }
        //ham lay dien cho trang thai forward
        public DataSet FORWARD_LOAD(out DataSet _dtContent,string pTELLERID)
        {
            return IBPS_MSG_CONTENTDP.Instance().FORWARD_LOAD(out _dtContent, pTELLERID);
        }

        //ham lay dien cho trang thai forward
        public DataSet FREVIOUS_LOAD(out DataSet _dtContent,string pUserid)
        {
            return IBPS_MSG_CONTENTDP.Instance().FREVIOUS_LOAD(out _dtContent, pUserid);
        }

        //ham lay dien cho trang thai forward
        public DataSet FREVIOUS_SEARCH(out DataSet _dtContent, string pUserid, string pDate, string pWhere)
        {
            return IBPS_MSG_CONTENTDP.Instance().FREVIOUS_SEARCH(out _dtContent, pUserid, pDate, pWhere);
        }
        //DatHM
        public DataTable FREVIOUS_PRINT(string pUserid, string pDate,string pWhere)
        {
            return IBPS_MSG_CONTENTDP.Instance().FREVIOUS_PRINT(pUserid, pDate, pWhere);
        }

        public DataTable FREVIOUS_PRINT_LOAD(string pUserid)
        {
            return IBPS_MSG_CONTENTDP.Instance().FREVIOUS_PRINT_LOAD(pUserid);
        }
        //End DatHM
        public DataSet FORWARD_CURRNET_LOAD(out DataSet _dtContent, string pUserid)
        {
            return IBPS_MSG_CONTENTDP.Instance().FORWARD_CURRENT_LOAD(out _dtContent, pUserid);
        }

        //ham lay dien cho trang thai forward
        public DataSet FORWARD_SEARCH_CURRENT(out DataSet _dtContent, string pWhere, string pUserid)
        {
            return IBPS_MSG_CONTENTDP.Instance().FORWARD_SEARCH_CURRENT(out _dtContent, pWhere, pUserid);
        }
        //ham lay dien cho trang thai forward
        public DataSet FORWARD_SEARCH(out DataSet _dtContent, string pWhere, string pTELLERID)
        {
            return IBPS_MSG_CONTENTDP.Instance().FORWARD_SEARCH(out _dtContent, pWhere, pTELLERID);
        }

        public DataSet IBPS_CONTENT_LOAD( out DataSet _dtContent)
        {
            return IBPS_MSG_CONTENTDP.Instance().IBPS_CONTENT_LOAD( out _dtContent);
        }

        public DataSet IBPS_CONTENT_LOAD_RESEND(out DataSet _dtContent)
        {
            return IBPS_MSG_CONTENTDP.Instance().IBPS_CONTENT_LOAD_RESEND(out _dtContent);
        }

        //---UPDATE TRUONG 
        //public int UPDATE_Statement(string pMSG_ID)
        //{
        //    return IBPS_MSG_CONTENTDP.Instance().UPDATE_Statement(pMSG_ID);
        //}

        public int AddIBPS_Q_Dblink_Out(IBPS_MSG_CONTENTInfo objTable, string m_vGW_BANK_CODE)
        {
            return IBPS_MSG_CONTENTDP.Instance().AddIBPS_Q_Dblink_Out(objTable, m_vGW_BANK_CODE);
        }

        public int Forward_LV_HV(IBPS_MSG_CONTENTInfo objTable)
        {
            return IBPS_MSG_CONTENTDP.Instance().Forward_LV_HV(objTable);
        }

        public int InsertIBPS_MSG_CONTENT_Temp(DateTime dtDate, string strType)
        {
            return IBPS_MSG_CONTENTDP.Instance().InsertIBPS_MSG_CONTENT_Temp(dtDate, strType);
        }


        public int InsertIBPS_MSG_CONTENT_Temp_TAD(DateTime dtDate, string strDepartment, string strTAD, string strGW_BANK_CODE, string strSIBS_BANK_CODE)
        {
            return IBPS_MSG_CONTENTDP.Instance().InsertIBPS_MSG_CONTENT_Temp_TAD(dtDate, strDepartment, strTAD, strGW_BANK_CODE, strSIBS_BANK_CODE);
        }

        public int InsertIBPS_MSG_REC_TAD(DateTime dtDate, string strTAD, string strDBLINK_NAME, string strDBLINK_HO)
        {
            return IBPS_MSG_CONTENTDP.Instance().InsertIBPS_MSG_REC_TAD(dtDate, strTAD, strDBLINK_NAME, strDBLINK_HO);
        }

        public DataTable GetIBPS_MSG_CONTENT_CONTENT(string strMsgID, string pTable_name)
        {
            return IBPS_MSG_CONTENTDP.Instance().GetIBPS_MSG_CONTENT_CONTENT(strMsgID, pTable_name);
        }

        public string Get_HV_LV(string pBCode, double pAmount)
        {
            return IBPS_MSG_CONTENTDP.Instance().Get_HV_LV(pBCode, pAmount);
        }

        public int Update_Print_STS(IBPS_MSG_CONTENTInfo objTable)
        {
            return IBPS_MSG_CONTENTDP.Instance().Update_Print_STS(objTable);
        }

        public int Resend_message_ibps(string pQUERY_ID,string pMSG_DIRECTION)
        {
            return IBPS_MSG_CONTENTDP.Instance().Resend_message_ibps(pQUERY_ID, pMSG_DIRECTION);
        }   
    }
}
