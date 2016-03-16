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
    public class SWIFT_RECONCILEController
    {

        public int AddSWIFT_RECONCILE(SWIFT_RECONCILEInfo objTable)
        {
            return SWIFT_RECONCILEDP.Instance().AddSWIFT_RECONCILE(objTable);
        }

        public int ClearSWIFT_RECONCILE(string dDate, string strDepartment)
        {
            return SWIFT_RECONCILEDP.Instance().ClearSWIFT_RECONCILE(dDate, strDepartment);
        }

        public DataSet GetSWIFT_RECONCILE(string dDate, string strDepartment, string strDirection, string strException, string strType)
        {
            return SWIFT_RECONCILEDP.Instance().GetSWIFT_RECONCILE(dDate, strDepartment, strDirection, strException,strType);
        }
        public int SWIFT_Reconcile(DateTime strDate)
        {
            return SWIFT_RECONCILEDP.Instance().SWIFT_Reconcile(strDate);
        }
        public int SWIFT_ReconcileSWIFT(DateTime strDate)
        {
            return SWIFT_RECONCILEDP.Instance().SWIFT_ReconcileSWIFT(strDate);
        }
        public int InsertSWIFT_MSG_REC_ALL(DateTime dtDate)
        {
            return SWIFT_RECONCILEDP.Instance().InsertSWIFT_MSG_REC_ALL(dtDate);
        }
        public int InsertSWIFT_MSG_REC_TOTAL(DateTime dtDate)
        {
            return SWIFT_RECONCILEDP.Instance().InsertSWIFT_MSG_REC_TOTAL(dtDate);
        }


    }


}
