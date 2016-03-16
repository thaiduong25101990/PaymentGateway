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
namespace BR.BRBusinessObject
{
    public class SWIFT_AUTO_VALUEController
    {

        public int AddSWIFT_AUTO_VALUE(SWIFT_AUTO_VALUE_Info objTable)
        {
            return SWIFT_AUTO_VALUEDP.Instance().AddSWIFT_AUTO_VALUE(objTable);
        }

        public int UpdateSWIFT_AUTO_VALUE(SWIFT_AUTO_VALUE_Info objTable)
        {
            return SWIFT_AUTO_VALUEDP.Instance().UpdateSWIFT_AUTO_VALUE(objTable);
        }

        public int DeleteSWIFT_AUTO_VALUE(int intPRM_ID)
        {
            return SWIFT_AUTO_VALUEDP.Instance().DeleteSWIFT_AUTO_VALUE(intPRM_ID);
        }

        public DataSet GetSWIFT_AUTO_VALUE()
        {
            return SWIFT_AUTO_VALUEDP.Instance().GetSWIFT_AUTO_VALUE();
        }

        public string GetSWIFT_AUTO_VALUE(string pKeyword)
        {
            return SWIFT_AUTO_VALUEDP.Instance().GetSWIFT_AUTO_VALUE(pKeyword);
        }

        public int ValidateSWIFT_AUTO_VALUE(string strSql)
        {
            return SWIFT_AUTO_VALUEDP.Instance().ValidateSWIFT_AUTO_VALUE(strSql);
        }
        public bool CheckCode(string strKeycode, int iID)
        {
            return SWIFT_AUTO_VALUEDP.Instance().CheckCode(strKeycode, iID);
        }

    }
}
