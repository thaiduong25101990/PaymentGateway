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
    public class SWIFT_MSG_REC_TEMPController
    {
        public int AddSWIFT_MSG_REC_TEMP(SWIFT_MSG_REC_TEMPInfo objTable)
        {
            return SWIFT_MSG_REC_TEMDP.Instance().AddSWIFT_MSG_REC_TEM(objTable);
        }

        public int ClearSWIFT_MSG_REC_TEM(string dDate, string strDepartment)
        {
            return SWIFT_MSG_REC_TEMDP.Instance().ClearSWIFT_MSG_REC_TEM(dDate, strDepartment);
        }

        public DataSet GetSWIFT_MSG_REC_TEM(string strDirection, string strDepartment)
        {
            //GetMSG_CONTENT_TEMP(string dDate, string strDepartment, string strDirection)
            return SWIFT_MSG_REC_TEMDP.Instance().GetSWIFT_MSG_REC_TEM(strDirection, strDepartment);
        }
    }
}
