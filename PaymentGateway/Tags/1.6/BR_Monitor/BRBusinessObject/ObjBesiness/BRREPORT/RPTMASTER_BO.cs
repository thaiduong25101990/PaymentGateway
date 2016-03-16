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
    public class RPTMASTERController
    {
        public int AddRPTMASTER(RPTMASTERInfo objTable)
        {
            return RPTMASTERDP.Instance().AddRPTMASTER(objTable);
        }

        public int UpdateRPTMASTER(string strID,RPTMASTERInfo objTable)
        {
            return RPTMASTERDP.Instance().UpdateRPTMASTER(strID ,objTable);
        }

        public int DeleteRPTMASTER(string strID)
        {
            return RPTMASTERDP.Instance().DeleteRPTMASTER(strID);
        }

        public DataSet GetRPTMASTER()
        {
            return RPTMASTERDP.Instance().GetRPTMASTER();
        }
        //public DataSet GetReportType(string strRptType)
        public DataSet GetReportType(string strRptType,string userID)
        {
            //return RPTMASTERDP.Instance().GetReportType(strRptType);
            return RPTMASTERDP.Instance().GetReportType(strRptType, userID);
        }
        public DataSet GetParam(string RptName)
        {
            return RPTMASTERDP.Instance().GetParam(RptName);
        }
        public DataTable GetDataCombo(string SQL)
        {
            return RPTMASTERDP.Instance().GetDataCombo(SQL);
        }
        //public DataSet GetGroup(string strGwtype)
        public DataSet GetGroup()
        {
            //return RPTMASTERDP.Instance().GetGroup(strGwtype);
            return RPTMASTERDP.Instance().GetGroup();
        }
    }
}
