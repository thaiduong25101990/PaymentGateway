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
using BR.BRBusinessObject;
namespace BR.BRBusinessObject
{
    public class RPTRULEController
    {
        public int AddRPTRULE(RPTRULEInfo objTable)
        {
            return RPTRULEDP.Instance().AddRPTRULE(objTable);
        }
        public int DeleteRPTRULE(int groupid, string RptName)
        {
            return RPTRULEDP.Instance().DeleteRPTRULE(groupid, RptName);
        }
        public DataSet GetRule(int pgroupid, string pRptName)
        {
            return RPTRULEDP.Instance().GetRule(pgroupid, pRptName);
        }
        public DataSet GetRuleReport(string userid, string pRptName)
        {
            return RPTRULEDP.Instance().GetRuleReport(userid, pRptName);
        }
        public DataSet GetRuleMenuRpt(string userid, string gwType)
        {
            return RPTRULEDP.Instance().GetRuleMenuRpt(userid, gwType);
        }      
    }
}
