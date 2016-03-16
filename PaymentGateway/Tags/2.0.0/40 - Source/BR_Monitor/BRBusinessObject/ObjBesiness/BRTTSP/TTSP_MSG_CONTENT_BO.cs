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
    public class TTSP_MSG_CONTENTController
    {
        //Nang cap

        public DataTable GetTTSP_MSG_CONTENT_CONTENT(string lMsgID)
        {
            return TTSP_MSG_CONTENTDP.Instance().GetTTSP_MSG_CONTENT_CONTENT(lMsgID);
        }


        public DataTable GetData_print_ttsp(string strMsgID, string strMSG_TYPE, string strMSGDIRECTION)
        {
            return TTSP_MSG_CONTENTDP.Instance().GetData_print_ttsp(strMsgID, strMSG_TYPE, strMSGDIRECTION);
        }

        public DataSet TTSP_CONTENT_RESEND(out DataSet _dtContent)
        {
            return TTSP_MSG_CONTENTDP.Instance().TTSP_CONTENT_RESEND(out _dtContent);
        }

        public DataSet TTSP_CONTENT_LOAD(out DataSet _dtContent)
        {
            return TTSP_MSG_CONTENTDP.Instance().TTSP_CONTENT_LOAD(out _dtContent);
        }

        public DataSet TTSP_CONTENT_SEARCH_RESEND(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            return TTSP_MSG_CONTENTDP.Instance().TTSP_CONTENT_SEARCH_RESEND(datefrom, dateto, pWhere, out _dtContent);
        }

        public DataSet TTSP_CONTENT_SEARCH(DateTime datefrom, DateTime dateto, string pWhere, out DataSet _dtContent)
        {
            return TTSP_MSG_CONTENTDP.Instance().TTSP_CONTENT_SEARCH(datefrom, dateto, pWhere, out _dtContent);
        }

        public DataSet TTSP_CONTENT_SEARCH_ADVANCE(string pWhere, out DataSet _dtContent)
        {
            return TTSP_MSG_CONTENTDP.Instance().TTSP_CONTENT_SEARCH_ADVANCE(pWhere, out _dtContent);
        }

        public DataSet TTSP_CONTENT_SEARCH_ADVANCE_RESEND(string pWhere, out DataSet _dtContent)
        {
            return TTSP_MSG_CONTENTDP.Instance().TTSP_CONTENT_SEARCH_ADVANCE_RESEND(pWhere, out _dtContent);
        }

        // het nang cap

        
      

    }
}
