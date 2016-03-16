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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:39
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class TTSP_RECONCILEController
    {

        public int AddTTSP_RECONCILE(TTSP_RECONCILEInfo objTable)
        {
            return TTSP_RECONCILEDP.Instance().AddTTSP_RECONCILE(objTable);
        }

        public int ClearTTSP_RECONCILE()
        {
            return TTSP_RECONCILEDP.Instance().ClearTTSP_RECONCILE();
        }

        public DataSet GetTTSP_RECONCILE(string dDate, string strDepartment, string strDirection, string strException, string strType,string strMsgType)
        {
            return TTSP_RECONCILEDP.Instance().GetTTSP_RECONCILE(dDate, strDepartment, strDirection, strException,strType,strMsgType);
        }
        public int TTSP_Reconcile(DateTime strDate)
        {
            return TTSP_RECONCILEDP.Instance().TTSP_Reconcile(strDate);
        }
        public int TTSP_Reconcile_TTSP(DateTime strDate)
        {
            return TTSP_RECONCILEDP.Instance().TTSP_Reconcile_TTSP(strDate);
        }

        public int InsertTTSP_MSG_REC_ALL(DateTime dtDate)
        {
            return TTSP_RECONCILEDP.Instance().InsertTTSP_MSG_REC_ALL(dtDate);
        }

        public int InsertTTSP_MSG_REC_TOTAL(DateTime dtDate)
        {
            return TTSP_RECONCILEDP.Instance().InsertTTSP_MSG_REC_TOTAL(dtDate);
        }

    }


}
