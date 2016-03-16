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
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class MSG_LOGController
	{
		
		public int AddMSG_LOG(MSG_LOGInfo objTable)
		{
			return MSG_LOGDP.Instance().AddMSG_LOG(objTable);
		}
		
		public int UpdateMSG_LOG(MSG_LOGInfo objTable)
		{
			return MSG_LOGDP.Instance().UpdateMSG_LOG(objTable);
		}
		
		public int DeleteMSG_LOG(MSG_LOGInfo objTable)
		{
			return MSG_LOGDP.Instance().DeleteMSG_LOG(objTable);
		}
        public DataTable GetMSG_LOG()
        {
            return MSG_LOGDP.Instance().GetMSG_LOG();
        }
        public DataTable GetMSG_LOG_S(DateTime pDateFrom,DateTime DateTo)
        {
            return MSG_LOGDP.Instance().GetMSG_LOG_S(pDateFrom, DateTo);
        }
        public DataTable GetMSG_LOG_S1(string strServiceName)
        {
            return MSG_LOGDP.Instance().GetMSG_LOG_S1(strServiceName);
        }
        public DataTable GetMSG_LOG_S2(string strServiceName, DateTime pDateFrom, DateTime DateTo)
        {
            return MSG_LOGDP.Instance().GetMSG_LOG_S2(strServiceName, pDateFrom, DateTo);
        }
		
	}
	
	
}
