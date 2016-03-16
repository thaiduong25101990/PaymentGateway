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
//' Create date:	11/006/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class VCB_MSG_LOGController
    {
        public DataSet GetVCB_MSG_LOG(string pQUERY_ID)
        {
            return VCB_MSG_LOGDP.Instance().GetVCB_MSG_LOG(pQUERY_ID);
        }
        public DataSet SearchVCB_MSG_LOG(string pWHERE, DateTime pDate_from, DateTime pDate_to)
        {
            return VCB_MSG_LOGDP.Instance().SearchVCB_MSG_LOG(pWHERE, pDate_from, pDate_to);
        }
        public DataTable Get_data(DateTime pDatetimeNow)
        {
            return VCB_MSG_LOGDP.Instance().Get_data(pDatetimeNow);
        }
        public int ADD_VCB_MSG_LOG(VCB_MSG_LOGInfo objTable)
        {
            return VCB_MSG_LOGDP.Instance().ADD_VCB_MSG_LOG(objTable);
        }
    }
}
