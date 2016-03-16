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
    public class SWIFT_MSG_REC_SAAController
    {
        public int AddSWIFT_MSG_REC_SAA(SWIFT_MSG_REC_SAAInfo objTable)
        {
            return SWIFT_MSG_REC_SAADP.Instance().AddSWIFT_MSG_REC_SAA(objTable);
        }

        public int ClearSWIFT_MSG_REC_SAA(string dDate, string strDepartment)
        {
            return SWIFT_MSG_REC_SAADP.Instance().ClearSWIFT_MSG_REC_SAA(dDate, strDepartment);
        }

        public DataSet GetSWIFT_MSG_REC_SAA(string strDirection, string strDepartment)
        {
            //GetMSG_CONTENT_TEMP(string dDate, string strDepartment, string strDirection)
            return SWIFT_MSG_REC_SAADP.Instance().GetSWIFT_MSG_REC_SAA(strDirection, strDepartment);
        }
    }
}
