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
    public class IBPS_FREEController
    {
        public DataTable GetIBPS_FREE(string pTRANSTYPE, string pFREETYPE)
        {
            return IBPS_FREEDP.Instance().GetIBPS_FREE(pTRANSTYPE, pFREETYPE);
        }

        public DataTable DELETEIBPS_FREE(string pTRANSTYPE, string pFREETYPE)
        {
            return IBPS_FREEDP.Instance().DELETEIBPS_FREE(pTRANSTYPE, pFREETYPE);
        }

        public int ADDIBPS_FREE(IBPS_FREEInfo objTable)
        {
            return IBPS_FREEDP.Instance().ADDIBPS_FREE(objTable);
        }
        public int UPDATEIBPS_FREE(IBPS_FREEInfo objTable)
        {
            return IBPS_FREEDP.Instance().UPDATEIBPS_FREE(objTable);
        }
        public DataTable FREE_NON_HARDFREE(Double hardfree,DateTime fromdate,DateTime todate)
        {
            return IBPS_FREEDP.Instance().FREE_NON_HARDFREE(hardfree, fromdate, todate);
        }
        public DataTable FREE_HARDFREE(DateTime fromdate, DateTime todate)
        {
            return IBPS_FREEDP.Instance().FREE_HARDFREE(fromdate, todate);
        }
        public DataTable FREE_HARDFREE_BEFORE_TIME(DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FREEDP.Instance().FREE_HARDFREE_BEFORE_TIME(fromdate, todate, hour);
        }
        public DataTable FREE_HARDFREE_AFTER_TIME(DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FREEDP.Instance().FREE_HARDFREE_AFTER_TIME(fromdate, todate, hour);
        }
        public DataTable FREE_NON_HARDFREE_BEFORE_TIME(Double hardfree, DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FREEDP.Instance().FREE_NON_HARDFREE_BEFORE_TIME(hardfree, fromdate, todate, hour);
        }
        public DataTable FREE_NON_HARDFREE_AFTER_TIME(Double hardfree, DateTime fromdate, DateTime todate, DateTime hour)
        {
            return IBPS_FREEDP.Instance().FREE_NON_HARDFREE_AFTER_TIME(hardfree, fromdate, todate, hour);
        }
        public DataTable CAL_FREE(DateTime fromdate, DateTime todate,
            string pCCYCD, int pType, string pBranch)
        {
            return IBPS_FREEDP.Instance().CAL_FREE(fromdate, todate,pCCYCD,pType,pBranch);
        }
    }
}
