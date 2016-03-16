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
    public class VCB_FEEController
    {
        public DataTable Get_VCB_FEE(string sCCYCD)
        {
            return VCB_FEEDP.Instance().Get_VCB_FEE(sCCYCD);
        }

        public int DELETE_VCB_FEE(long pID)
        {
            return VCB_FEEDP.Instance().DELETE_VCB_FEE(pID);
        }

        public int ADD_VCB_FEE(VCB_FEE_Info objTable)
        {
            return VCB_FEEDP.Instance().ADD_VCB_FEE(objTable);
        }
        public int UPDATE_VCB_FEE(VCB_FEE_Info objTable, int iFeeType)
        {
            return VCB_FEEDP.Instance().UPDATE_VCB_FEE(objTable, iFeeType);
        }
        public DataTable CAL_FEE(DateTime fromdate, DateTime todate, string pBranch,
            int pFeeType, string pCCYCD)
        {
            return VCB_FEEDP.Instance().CAL_FEE(fromdate, todate, pBranch, pFeeType,pCCYCD);
        }
        public DataTable CAL_FEE_DETAIL(DateTime fromdate, DateTime todate, string pBranch,
            int pFeeType, string pCCYCD)
        {
            return VCB_FEEDP.Instance().CAL_FEE_DETAIL(fromdate, todate, pBranch, pFeeType, pCCYCD);
        }
        public DataTable CAL_FEE_DETAIL_EXCEL(DateTime fromdate, DateTime todate, string pBranch,
            int pFeeType, string pCCYCD)
        {
            return VCB_FEEDP.Instance().CAL_FEE_DETAIL_EXCEL(fromdate, todate, pBranch, pFeeType, pCCYCD);
        }
        public DataSet CheckCCYCD(string pCCYCD, long pID)
        {
            return VCB_FEEDP.Instance().CheckCCYCD(pCCYCD, pID);
        }
        
    }
}
