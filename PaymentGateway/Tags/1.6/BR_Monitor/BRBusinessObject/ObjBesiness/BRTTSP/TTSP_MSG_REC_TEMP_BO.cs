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
    public class TTSP_MSG_REC_TEMPController
    {
        /******************************************
         * Day dien tu SIBS vao TTSP_MSG_REC_TEMP
         ******************************************/
        public int AddTTSP_MSG_REC_TEMP(TTSP_MSG_REC_TEMPInfo objTable)
        {
            return TTSP_MSG_REC_TEMDP.Instance().AddTTSP_MSG_REC_TEM(objTable);
        }
        /******************************************
         * Xoa bang TTSP_MSG_REC_TEMP
         ******************************************/
        public int ClearTTSP_MSG_REC_TEM()
        {
            return TTSP_MSG_REC_TEMDP.Instance().ClearTTSP_MSG_REC_TEM();
        }
        /******************************************
         * Lay du lieu TTSP_MSG_REC_TEMP
         ******************************************/
        public DataSet GetTTSP_MSG_REC_TEM(string strDirection, string strDepartment)
        {
            //GetMSG_CONTENT_TEMP(string dDate, string strDepartment, string strDirection)
            return TTSP_MSG_REC_TEMDP.Instance().GetTTSP_MSG_REC_TEM(strDirection, strDepartment);
        }
        
    }
}
