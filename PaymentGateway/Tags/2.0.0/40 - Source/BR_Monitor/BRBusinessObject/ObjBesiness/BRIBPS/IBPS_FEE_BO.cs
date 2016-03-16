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
    public class IBPS_FEEController
    {
        public DataSet CheckCCYCD(string pCCYCD, long pID,
            string pTranType, string pDiscountType)
        {
            return IBPS_FEEDP.Instance().CheckCCYCD(pCCYCD,
                pID, pTranType, pDiscountType);
        }

        public DataSet GetBRANCH8(string pBRANCH)
        {
            return IBPS_FEEDP.Instance().GetBRANCH8(pBRANCH);
        }
        public DataSet GetIBPS_FEE(string pTRANS_TYPE, string pFEEDISC_TYPE)
        {
            return IBPS_FEEDP.Instance().GetIBPS_FEE(pTRANS_TYPE, pFEEDISC_TYPE);
        }

        public DataTable DeleteIBPS_FEE(int pID)
        {
            return IBPS_FEEDP.Instance().DeleteIBPS_FEE(pID);
        }

        public int AddIBPS_FEE(IBPS_FEEInfo objTable)
        {
            return IBPS_FEEDP.Instance().AddIBPS_FEE(objTable);
        }
        public int UpdateIBPS_FEE(IBPS_FEEInfo objTable, int iFeeType)
        {
            return IBPS_FEEDP.Instance().UpdateIBPS_FEE(objTable, iFeeType);
        }
        public DataTable FREE_NON_HARDFREE(Double hardfree,DateTime fromdate,DateTime todate)
        {
            return IBPS_FEEDP.Instance().FREE_NON_HARDFREE(hardfree, fromdate, todate);
        }
        public DataTable FREE_HARDFREE(DateTime fromdate, DateTime todate)
        {
            return IBPS_FEEDP.Instance().FREE_HARDFREE(fromdate, todate);
        }
        public DataTable FREE_HARDFREE_BEFORE_TIME(DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FEEDP.Instance().FREE_HARDFREE_BEFORE_TIME(fromdate, todate, hour);
        }
        public DataTable FREE_HARDFREE_AFTER_TIME(DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FEEDP.Instance().FREE_HARDFREE_AFTER_TIME(fromdate, todate, hour);
        }
        public DataTable FREE_NON_HARDFREE_BEFORE_TIME(Double hardfree, DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FEEDP.Instance().FREE_NON_HARDFREE_BEFORE_TIME(hardfree, fromdate, todate, hour);
        }
        public DataTable FREE_NON_HARDFREE_AFTER_TIME(Double hardfree, DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FEEDP.Instance().FREE_NON_HARDFREE_AFTER_TIME(hardfree, fromdate, todate, hour);
        }
        public DataTable CAL_FEE(DateTime fromdate, DateTime todate, int pBranchType, int pFeeType, string pBranch, string pBranch8, string pCCYCD)
        {
            return IBPS_FEEDP.Instance().CAL_FEE(fromdate, todate, pBranchType, pFeeType, pBranch, pBranch8, pCCYCD);
        }
        public DataTable CAL_FEE_DETAIL(DateTime fromdate, DateTime todate, int pBranchType, int pFeeType, string pBranch, string pBranch8, string pCCYCD)
        {
            return IBPS_FEEDP.Instance().CAL_FEE_DETAIL(fromdate, todate, pBranchType, pFeeType, pBranch, pBranch8, pCCYCD);
        }
        public DataTable CAL_FEE_DETAIL_EXCEL(DateTime fromdate, DateTime todate, int pBranchType, int pFeeType, string pBranch, string pBranch8, string pCCYCD)
        {
            return IBPS_FEEDP.Instance().CAL_FEE_DETAIL_EXCEL(fromdate, todate, pBranchType, pFeeType, pBranch, pBranch8, pCCYCD);
        }
    }
}
