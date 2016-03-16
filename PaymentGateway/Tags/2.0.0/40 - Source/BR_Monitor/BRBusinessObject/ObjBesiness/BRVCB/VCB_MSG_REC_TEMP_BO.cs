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
    /********************************************   
     * Doi tuong thao tac voi bang VCB_REC_TEMP
     ********************************************/     
    public class VCB_MSG_REC_TEMPController
    {
        /****************************************
         * Them du lieu vao bang VCB_REC_TEMP
         ****************************************/
        public int AddVCB_MSG_REC_TEMP(VCB_MSG_REC_TEMPInfo objTable)
        {
            return VCB_MSG_REC_TEMDP.Instance().AddVCB_MSG_REC_TEM(objTable);
        }
        /****************************************
         * Xoa bang VCB_REC_TEMP
         ****************************************/
        public int ClearVCB_MSG_REC_TEM(string dDate, string strDepartment)
        {
            return VCB_MSG_REC_TEMDP.Instance().ClearVCB_MSG_REC_TEM(dDate, strDepartment);
        }
        /****************************************
         * Lay du lieu tu bang VCB_REC_TEMP
         ****************************************/
        public DataSet GetVCB_MSG_REC_TEM(string strDirection, string strDepartment)
        {
            //GetMSG_CONTENT_TEMP(string dDate, string strDepartment, string strDirection)
            return VCB_MSG_REC_TEMDP.Instance().GetVCB_MSG_REC_TEM(strDirection, strDepartment);
        }
    }
}
