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
//' Create date:	11/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class VCB_MSG_CONTENTController
    {

        //nang cap
        public DataTable GetVCB_MSG_CONTENT_CONTENT(string strMsgID, string pTable_name)
        {
            return VCB_MSG_CONTENTDP.Instance().GetVCB_MSG_CONTENT_CONTENT(strMsgID, pTable_name);
        }

        public DataTable GetData_print_vcb(string strMsgID, string strMSG_TYPE,
            string strMSGDIRECTION, string strBranch, string strUserid)
        {
            return VCB_MSG_CONTENTDP.Instance().GetData_print_vcb(strMsgID, strMSG_TYPE, 
                strMSGDIRECTION,strBranch, strUserid);
        }

        public DataSet VCB_CONTENT_LOAD(out DataSet _dtContent)
        {
            return VCB_MSG_CONTENTDP.Instance().VCB_CONTENT_LOAD(out _dtContent);
        }

        public DataSet VCB_CONTENT_LOAD_RESEND(out DataSet _dtContent)
        {
            return VCB_MSG_CONTENTDP.Instance().VCB_CONTENT_LOAD_RESEND(out _dtContent);
        }

        public DataSet VCB_CONTENT_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            return VCB_MSG_CONTENTDP.Instance().VCB_CONTENT_SEARCH(datefrom, dateto, pWhere, out _dtContent);
        }

        public DataSet VCB_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
        {
            return VCB_MSG_CONTENTDP.Instance().VCB_CONTENT_SEARCH_ADVANCE(pWhere, out _dtContent);
        }

        public int Resend_message_vcb(string pQUERY_ID, string pMSG_DIRECTION)
        {
            return VCB_MSG_CONTENTDP.Instance().Resend_message_vcb(pQUERY_ID, pMSG_DIRECTION);
        }

        public int Update_Print_STS(VCB_MSG_CONTENTInfo objTable)
        {
            return VCB_MSG_CONTENTDP.Instance().Update_Print_STS(objTable);
        }
    }
}
