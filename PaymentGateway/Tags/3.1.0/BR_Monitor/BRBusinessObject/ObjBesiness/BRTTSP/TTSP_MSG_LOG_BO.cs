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
//' Author:	Nguyen duc quy
//' Create date:	11/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class TTSP_MSG_LOGController
    {
        public DataSet GetTTSP_MSG_LOG(string pQUERY_ID)
        {
            return TTSP_MSG_LOGDP.Instance().GetTTSP_MSG_LOG(pQUERY_ID);
        }
        public DataSet SearchTTSP_MSG_LOG(string pWHERE, DateTime pDate_from, DateTime pDate_to)
        {
            return TTSP_MSG_LOGDP.Instance().SearchTTSP_MSG_LOG(pWHERE, pDate_from, pDate_to);
        }
        public DataTable Get_data(DateTime pDatetimeNow)
        {
            return TTSP_MSG_LOGDP.Instance().Get_data(pDatetimeNow);
        }

        public int ADD_TTSP_MSG_LOG(TTSP_MSG_LOGInfo objTable)
        {
            return TTSP_MSG_LOGDP.Instance().ADD_TTSP_MSG_LOG(objTable);
        }
    }
}
