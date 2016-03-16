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
    public class VCB_RECONCILEController
    {

        public int AddVCB_RECONCILE(VCB_RECONCILEInfo objTable)
        {
            return VCB_RECONCILEDP.Instance().AddVCB_RECONCILE(objTable);
        }

        public int ClearVCB_RECONCILE(string dDate, string strDepartment)
        {
            return VCB_RECONCILEDP.Instance().ClearVCB_RECONCILE(dDate, strDepartment);
        }

        public DataSet GetVCB_RECONCILE(string dDate, string strDepartment, string strDirection, string strException,string strType,string strMsgType)
        {
            return VCB_RECONCILEDP.Instance().GetVCB_RECONCILE(dDate, strDepartment, strDirection, strException, strType,strMsgType);
        }

        public int VCB_Reconcile(DateTime strDate)
        {
            return VCB_RECONCILEDP.Instance().VCB_Reconcile(strDate);
        }

        public int VCB_Reconcile_VCB(DateTime strDate)
        {
            return VCB_RECONCILEDP.Instance().VCB_Reconcile_VCB(strDate);
        }

        public int InsertVCB_MSG_REC_ALL(DateTime dtDate)
        {
            return VCB_RECONCILEDP.Instance().InsertVCB_MSG_REC_ALL(dtDate);
        }

        public int InsertVCB_MSG_REC_TOTAL(DateTime dtDate)
        {
            return VCB_RECONCILEDP.Instance().InsertVCB_MSG_REC_TOTAL(dtDate);
        }

    }


}
