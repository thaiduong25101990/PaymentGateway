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
    public class SWIFT_MSG_CONTENTController
    {
        //nang cap BR----------------------------------------------------------


        public DataTable Search_Content(string pMsg_id)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Search_Content(pMsg_id);
        }

        public DataSet SWIFT_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().SWIFT_CONTENT_SEARCH_ADVANCE(pWhere, out _dtContent);
        }

        public DataSet SWIFT_CONTENT(DateTime datefrom,DateTime dateto,string pWhere, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().SWIFT_CONTENT(datefrom, dateto, pWhere, out _dtContent);
        }
        //ham lay dien cho form resend lai dien trong ngay cua bang content
        public DataSet LOAD_DATA_RESEND(string pWhere, int pTeller, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().LOAD_DATA_RESEND(pWhere,pTeller, out _dtContent);
        }

        //ham lay dien cho form resend 
        public DataSet SEARCH_DATA_RESEND(DateTime datefrom, DateTime dateto, string pWhere, int pTeller, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().SEARCH_DATA_RESEND(datefrom, dateto,pWhere, pTeller, out _dtContent);
        }

        public DataSet MESSAGE_CONTENT( string pWhere, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().MESSAGE_CONTENT( pWhere, out _dtContent);
        }

        public DataSet MESSAGE_CONTENT_INWARD(string pWhere, int pTeller, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().MESSAGE_CONTENT_INWARD(pWhere,pTeller, out _dtContent);
        }

        public int DELETE_PROCESS_HANDER(string vMSG_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().DELETE_PROCESS_HANDER(vMSG_ID);
        }

        public DataSet MESSAGE_CONTENT_DATE(DateTime datefrom,DateTime dateto,string pWhere, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().MESSAGE_CONTENT_DATE(datefrom, dateto, pWhere, out _dtContent);
        }

        public DataSet MESSAGE_CONTENT_INSWARD_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, int pTeller, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().MESSAGE_CONTENT_INSWARD_SEARCH(datefrom, dateto, pWhere,pTeller, out _dtContent);
        }

        public DataTable PROCESS_HANDICRAFT(string vProcess, string vMsg_id, string vTable, string vRows, string vTeller_id, out DataTable _dtRows)
        {
            return SWIFT_MSG_CONTENTDP.Instance().PROCESS_HANDICRAFT(vProcess, vMsg_id, vTable, vRows, vTeller_id, out _dtRows);
        }

        public DataTable PROCESS_HANDICRAFT_SIBS_SWIFT(string vMsg_id,  string vRows, string vTeller_id, out DataTable _dtRows)
        {
            return SWIFT_MSG_CONTENTDP.Instance().PROCESS_HANDICRAFT_SIBS_SWIFT( vMsg_id,  vRows, vTeller_id, out _dtRows);
        }

        public DataTable PROCESS_HANDICRAFT_SUPPER(string vProcess, string vMsg_id, string vTable, string vRows, string vTeller_id, out DataTable _dtRows)
        {
            return SWIFT_MSG_CONTENTDP.Instance().PROCESS_HANDICRAFT_SUPPER(vProcess, vMsg_id, vTable, vRows, vTeller_id, out _dtRows);
        }

        //nang cap BR----------------------------------------------------------


        public DataTable SWIFT_STATUS(string pTable, string pMsg_id)
        {
            return SWIFT_MSG_CONTENTDP.Instance().SWIFT_STATUS(pTable, pMsg_id);
        }

        public int Lock_User(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Lock_User(objTable);
        }

        public DataTable Check_LOCKSTS(string pMsg_id, string pTable_NA)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Check_LOCKSTS(pMsg_id, pTable_NA);
        }
        

        public DataTable GetData_Pre(string pMsg_id, string pTable_NA)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GetData_Pre(pMsg_id, pTable_NA);
        }


        public DataTable swift_print_map(string strQID, string pTable_name)
        {
            return SWIFT_MSG_CONTENTDP.Instance().swift_print_map(strQID, pTable_name);
        }

        public DataTable swift_print_03(string strMsgID, string strMSG_TYPE, 
            string strMSGDIRECTION, string strDepartment)
        {
            return SWIFT_MSG_CONTENTDP.Instance().swift_print_03(strMsgID, strMSG_TYPE,
                strMSGDIRECTION, strDepartment);
        }
    
        public int UpdateSWIFT_MSG_CONTENTStatusT(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UpdateSWIFT_MSG_CONTENTStatusT(objTable);
        }

        public int UPDATE_SYSDATE(string pMSG_ID, string pTABLE)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UPDATE_SYSDATE(pMSG_ID, pTABLE);
        }

        public int UPDATE_SYSDATE_MSGDTL(string pQUERY_ID, string pTABLE)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UPDATE_SYSDATE_MSGDTL(pQUERY_ID, pTABLE);
        }

        public int Update_tellerID(string pQUERY_ID, string pTable_name)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Update_tellerID(pQUERY_ID, pTable_name);
        }

        public int UpdateSWIFT_MSG_CONTENTStatusSwiftMsgManualDup1(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UpdateSWIFT_MSG_CONTENTStatusSwiftMsgManualDup1(objTable);
        }

        public int UpdateSWIFT_MSG_Reject(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UpdateSWIFT_MSG_Reject(objTable);
        }

        public int UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(objTable);
        }

        public int UpdateSWIFT_MSG_CONTENT_T_L(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UpdateSWIFT_MSG_CONTENT_T_L(objTable);
        }

        public int UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(SWIFT_MSG_CONTENTInfo objTable, string pTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UpdateSWIFT_MSG_CONTENT_T_L_CLOSE(objTable, pTable);
        }

        public DataSet GetTellerID(int strQueryID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GetTellerID(strQueryID);
        }

        public int DeleteSWIFT_MSG_CONTENT_Temp()
        {
            return SWIFT_MSG_CONTENTDP.Instance().DeleteSWIFT_MSG_CONTENT_Temp();
        }
       
        //end dathm
        public string OnShowResult(DateTime pDate)
        {
            return SWIFT_MSG_CONTENTDP.Instance().OnShowResult(pDate);
        }

        public string OnShowResultIN(DateTime pDate)
        {
            return SWIFT_MSG_CONTENTDP.Instance().OnShowResultIN(pDate);
        }

        public int TF_RM_SVR_INDEX(string pQUERYID, string pTABLE)
        {
            return SWIFT_MSG_CONTENTDP.Instance().TF_RM_SVR_INDEX(pQUERYID, pTABLE);
        }

        public DataTable dtExcel(string pQuery_id, out DataTable dsview)
        {
            return SWIFT_MSG_CONTENTDP.Instance().dtExcel(pQuery_id, out dsview);
        }

        public int Update_Resend_Num(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Update_Resend_Num(objTable);
        }

        public int Get_Resend_Num(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Get_Resend_Num(objTable);
        }

        public int UPDATE_MSG_EDIT(SWIFT_MSG_CONTENTInfo objTable, string chrProcesssts)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UPDATE_MSG_EDIT(objTable, chrProcesssts);
        }

        public int INSERT_MESSAGE_EDIT(int iQuery_id, string pFIELD_CONTENT_ORIGIN, string pFIELD_CONTENT_EDIT,int vOrder)
        {
            return SWIFT_MSG_CONTENTDP.Instance().INSERT_MESSAGE_EDIT(iQuery_id, pFIELD_CONTENT_ORIGIN, pFIELD_CONTENT_EDIT, vOrder);
        }

        public DataTable GET_MESSAGE_EDIT(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_MESSAGE_EDIT(objTable);
        }

        public DataTable GET_PR_PROCESSSTS(string chrQUERY_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_PR_PROCESSSTS(chrQUERY_ID);
        }
        public DataTable GET_PROCESSSTS(string chrQUERY_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_PROCESSSTS(chrQUERY_ID);
        }

        public DataTable GET_SWIFT_MSG_EDIT(int iQuery_id)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_SWIFT_MSG_EDIT(iQuery_id);
        }

        public int Resend_message_swift(string pDEPARTMENT, string pFIELD20, string pQUERY_ID, string pMSG_DIRECTION)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Resend_message_swift(pDEPARTMENT, pFIELD20, pQUERY_ID, pMSG_DIRECTION);
        }

        public int Update_Print_STS(SWIFT_MSG_CONTENTInfo objTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Update_Print_STS(objTable);
        }
        public int Resend_message_swift_tc(string pQUERY_ID, string pMSGpTable)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Resend_message_swift_tc(pQUERY_ID, pMSGpTable);
        }
        //Khi cap nhat cho MSB
        public int Update_swift_process(string pQUERY_ID, string pTellerid)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Update_swift_process(pQUERY_ID, pTellerid);
        }

        public DataTable Load_process(string pQUERY_ID, string pTellerid)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Load_process(pQUERY_ID, pTellerid);
        }

        public int Delete_swift_process(string pQUERY_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().Delete_swift_process(pQUERY_ID);
        }
        public int DELETE_SWIFT_PROCESS_HANDER(string pMSG_ID,string TELLER_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().DELETE_SWIFT_PROCESS_HANDER(pMSG_ID, TELLER_ID);
        }
        public int UPDATE_CLOSE_MESSAGE(string pPROCESSSTS, string pQUERY_ID, string pTABLENAME)
        {
            return SWIFT_MSG_CONTENTDP.Instance().UPDATE_CLOSE_MESSAGE(pPROCESSSTS,pQUERY_ID, pTABLENAME);
        }

        public DataTable GET_MAP_FIELD(string chrMSG_TYPE)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_MAP_FIELD(chrMSG_TYPE);
        }

        public DataTable GET_SWIFT_PROCESS(string chrQUERY_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_SWIFT_PROCESS(chrQUERY_ID);
        }

        public DataTable GET_SWIFT_PROCESS_HANDER(string chrMSG_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().GET_SWIFT_PROCESS_HANDER(chrMSG_ID);
        }

        public DataSet SEARCH_DATA_MANUAL_NORMAL(string pWhere, out DataSet _dtContent)
        {
            return SWIFT_MSG_CONTENTDP.Instance().SEARCH_DATA_MANUAL_NORMAL(pWhere, out _dtContent);
        }
        public int CHECK_PROCESS(int pMSG_ID, int pQUERY_ID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().CHECK_PROCESS(pMSG_ID, pQUERY_ID);
        }

        public int CHECK_PROCESS_REPAIR(int pQUERY_ID,string pTELLERID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().CHECK_PROCESS_REPAIR(pQUERY_ID, pTELLERID);
        }
        public int DELETE_PROCESS_REPAIR(int pQUERY_ID, string pTELLERID)
        {
            return SWIFT_MSG_CONTENTDP.Instance().DELETE_PROCESS_REPAIR(pQUERY_ID, pTELLERID);
        }
    }
}
