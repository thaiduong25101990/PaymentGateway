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
//' Create date:	12/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class EXCELController
    {
        public DataSet GetEXCEL(string pMSG_ID)
        {
            return EXCELDP.Instance().GetEXCEL(pMSG_ID);
        }
        public DataTable GetEXCEL1(string strUserID)
        {
            return EXCELDP.Instance().GetEXCEL1(strUserID);
        }
        public DataTable GetEXCEL_APP(string strUserID, string strMsg_direction)
        {
            return EXCELDP.Instance().GetEXCEL_APP(strUserID,strMsg_direction);
        }
        public int AddExcel(EXCELInfo objTable)
        {
            return EXCELDP.Instance().AddExcel(objTable);
        }
        public int DeleteRows(string Msg_id)
        {
            return EXCELDP.Instance().DeleteRows(Msg_id);
        }
        public int UpdateStatus(string Msg_id, string strUserID)
        {
            return EXCELDP.Instance().UpdateStatus(Msg_id, strUserID);
        }
        public DataTable Check_Excel(string pWhere)
        {
            return EXCELDP.Instance().Check_Excel(pWhere);
        }
        public DataTable Check_Excel_VCB(string pWhere)
        {
            return EXCELDP.Instance().Check_Excel_VCB(pWhere);
        }
        /*Dung cho IBPS*/
        public DataTable Check_Excel_IBPS(string pWhere)
        {
            return EXCELDP.Instance().Check_Excel_IBPS(pWhere);
        }
        public int AddExcel_IBPS(string pCHECK_KEY, string pCONTENT,string pFILE_NAME, string pTELLERID,string pTYPE)
        {
            return EXCELDP.Instance().AddExcel_IBPS(pCHECK_KEY, pCONTENT, pFILE_NAME, pTELLERID, pTYPE);
        }
        /*Dung cho SWIFT*/
        public DataTable Check_SWIFT_TEXT(string pWhere)
        {
            return EXCELDP.Instance().Check_SWIFT_TEXT(pWhere);
        }
        public int AddSWIFT_TEXT(string pCHECK_KEY, string pCONTENT, string pFILE_NAME, string pTELLERID, string pTYPE)
        {
            return EXCELDP.Instance().AddSWIFT_TEXT(pCHECK_KEY, pCONTENT, pFILE_NAME, pTELLERID, pTYPE);
        }
        public int UPDATESWIFT_TEXT(string pFILE_NAME, string pTELLERID, string pTYPE)
        {
            return EXCELDP.Instance().UPDATESWIFT_TEXT(pFILE_NAME, pTELLERID, pTYPE);
        }
    }
}
