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
using System.Data.OracleClient;
//using BR.BRLib;
using BR.DataAccess;

namespace BR.BRBusinessObject
{
    public class IBPS_MSG_REC_TEMPController
    {
        public int AddIBPS_MSG_REC_TEMP(IBPS_MSG_REC_TEMPInfo objTable)
        {
            return IBPS_MSG_REC_TEMDP.Instance().AddIBPS_MSG_REC_TEM(objTable);
        }

        public int ClearIBPS_MSG_REC_TEM(DateTime dDate, string strDepartment)
        {
            return IBPS_MSG_REC_TEMDP.Instance().ClearIBPS_MSG_REC_TEM(dDate, strDepartment);
        }

        public DataSet GetIBPS_MSG_REC_TEM(string strDirection, string strDepartment)
        {
            
            return IBPS_MSG_REC_TEMDP.Instance().GetIBPS_MSG_REC_TEM(strDirection, strDepartment);
        }
    }
}
