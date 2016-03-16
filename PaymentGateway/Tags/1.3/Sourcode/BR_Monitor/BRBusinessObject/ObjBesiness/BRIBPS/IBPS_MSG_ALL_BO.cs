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
//' Create date:	18/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
        public class IBPS_MSG_ALLController
        {
          
            public int BackUp(string pTABLENAME1, string pTABLENAME2, string pMSGStatus)
            {
                return IBPS_MSG_ALLDP.Instance().BackUp(pTABLENAME1, pTABLENAME2, pMSGStatus);
            }
            public int BackUpAll(string pTABLENAME1, string pTABLENAME2, string pMSGStatus)
            {
                return IBPS_MSG_ALLDP.Instance().BackUpAll(pTABLENAME1, pTABLENAME2, pMSGStatus);
            }
            public int Delete(string pTABLENAME, string pWhere)
            {
                return IBPS_MSG_ALLDP.Instance().Delete(pTABLENAME, pWhere);
            }
           
           
            public int Forward_LV_HV(int pMsg_ID, int pHL_Val, string strTellerID)
            {
                return IBPS_MSG_ALLDP.Instance().Forward_LV_HV(pMsg_ID, pHL_Val, strTellerID);
            }
            public int CheckExist(string pTABLENAME)
            {
                return IBPS_MSG_ALLDP.Instance().CheckExist(pTABLENAME);
            }
        } 

}
