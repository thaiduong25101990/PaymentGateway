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
    public class SWIFT_MSG_LOGController
    {
        public DataSet GetSWIFT_MSG_LOG(string pQUERY_ID)
        {
            return SWIFT_MSG_LOGDP.Instance().GetSWIFT_MSG_LOG(pQUERY_ID);
        }
        public DataSet SearchSWIFT_MSG_LOG(string pWHERE, DateTime pDate_from, DateTime pDate_to)
        {
            return SWIFT_MSG_LOGDP.Instance().SearchSWIFT_MSG_LOG(pWHERE, pDate_from, pDate_to);
        }
        //GetSWIFT_MSG_LOG_ManualInfo
        public DataSet GetSWIFT_MSG_LOG_ManualInfo(int pQUERY_ID)
        {
            return SWIFT_MSG_LOGDP.Instance().GetSWIFT_MSG_LOG_ManualInfo(pQUERY_ID);
        }
        public DataTable Get_data(DateTime pDatetimeNow)
        {
            return SWIFT_MSG_LOGDP.Instance().Get_data(pDatetimeNow);
        }
        public int ADD_SWIFT_MSG_LOG(SWIFT_MSG_LOGInfo objTable)
        {
            return SWIFT_MSG_LOGDP.Instance().ADD_SWIFT_MSG_LOG(objTable);
        }
        public int ADD_SWIFT_MSG_LOG_DATE(SWIFT_MSG_LOGInfo objTable)
        {
            return SWIFT_MSG_LOGDP.Instance().ADD_SWIFT_MSG_LOG_DATE(objTable);
        }
        public DataTable SELECT_SWIFT_MSG_LOG(SWIFT_MSG_LOGInfo objTable)
        {
            return SWIFT_MSG_LOGDP.Instance().SELECT_SWIFT_MSG_LOG(objTable);
        }
    }
}
