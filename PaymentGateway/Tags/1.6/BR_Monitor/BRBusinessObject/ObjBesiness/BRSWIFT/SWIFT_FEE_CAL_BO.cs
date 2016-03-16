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

namespace BR.BRBusinessObject
{
    public class SWIFT_FEEController
    {
        public DataSet Get_SWIFT_FEE()
        {
            return SWIFT_FEEDP.Instance().Get_SWIFT_FEE();
        }

        public int DELETE_SWIFT_FEE(int pID)
        {
            return SWIFT_FEEDP.Instance().DELETE_SWIFT_FEE(pID);
        }

        public int ADD_SWIFT_FEE(SWIFT_FEE_Info objTable)
        {
            return SWIFT_FEEDP.Instance().ADD_SWIFT_FEE(objTable);
        }
        public int UPDATE_SWIFT_FEE(SWIFT_FEE_Info objTable)
        {
            return SWIFT_FEEDP.Instance().UPDATE_SWIFT_FEE(objTable);
        }
        public DataTable CAL_FEE(DateTime fromdate, DateTime todate,
            string pBranch, int pFeeType)
        {
            return SWIFT_FEEDP.Instance().CAL_FEE(fromdate, todate, pBranch, pFeeType);
        }
        public DataSet CheckMsgType(string pMsgType, long pID)
        {
            return SWIFT_FEEDP.Instance().CheckMsgType(pMsgType, pID);
        }
    }
}
