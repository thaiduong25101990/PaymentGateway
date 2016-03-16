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
    public class SWIFT_MODULE_ACTIONController
    {

        public int AddSWIFT_MODULE_ACTION(SWIFT_MODULE_ACTION_Info objTable)
        {
            return SWIFT_MODULE_ACTIONDP.Instance().AddSWIFT_MODULE_ACTION(objTable);
        }

        public int UpdateSWIFT_MODULE_ACTION(SWIFT_MODULE_ACTION_Info objTable)
        {
            return SWIFT_MODULE_ACTIONDP.Instance().UpdateSWIFT_MODULE_ACTION(objTable);
        }

        public int UpdateSWIFTMODULEACTION(SWIFT_MODULE_ACTION_Info objTable)
        {
            return SWIFT_MODULE_ACTIONDP.Instance().UpdateSWIFTMODULEACTION(objTable);
        }
        public int DeleteSWIFT_MODULE_ACTION(int intKey)
        {
            return SWIFT_MODULE_ACTIONDP.Instance().DeleteSWIFT_MODULE_ACTION(intKey);
        }

        public DataSet GetSWIFT_MODULE_ACTION()
        {
            return SWIFT_MODULE_ACTIONDP.Instance().GetSWIFT_MODULE_ACTION();
        }

        public int ValidateSWIFT_MODULE_ACTION(string strSql)
        {
            return SWIFT_MODULE_ACTIONDP.Instance().ValidateSWIFT_MODULE_ACTION(strSql);
        }
        public bool IDIsExisting(string strCriteriaName, string strPriority, string strModul)
        {
            return SWIFT_MODULE_ACTIONDP.Instance().IDIsExisting(strCriteriaName, strPriority, strModul);
        }
    }
}
