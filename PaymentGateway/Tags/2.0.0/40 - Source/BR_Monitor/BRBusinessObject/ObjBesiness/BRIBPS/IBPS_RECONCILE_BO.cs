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
    public class IBPS_RECONCILEController
    {

        public int AddIBPS_RECONCILE(IBPS_RECONCILEInfo objTable)
        {
            return IBPS_RECONCILEDP.Instance().AddIBPS_RECONCILE(objTable);
        }

        public int ClearIBPS_RECONCILE()
        {
            return IBPS_RECONCILEDP.Instance().ClearIBPS_RECONCILE();
        }

        
        public int IBPS_Reconcile(DateTime strDate)
        {
            return IBPS_RECONCILEDP.Instance().IBPS_Reconcile(strDate);
        }
        public int IBPS_Reconcile_TAD(DateTime strDate,string strTAD)
        {
            return IBPS_RECONCILEDP.Instance().IBPS_Reconcile_TAD(strDate,strTAD);
        }

        public int InsertIBPS_MSG_REC_ALL(DateTime dtDate)
        {
            return IBPS_RECONCILEDP.Instance().InsertIBPS_MSG_REC_ALL(dtDate);
        }

        public int InsertIBPS_MSG_REC_TOTAL(DateTime dtDate)
        {
            return IBPS_RECONCILEDP.Instance().InsertIBPS_MSG_REC_TOTAL(dtDate);
        }
    }


}
