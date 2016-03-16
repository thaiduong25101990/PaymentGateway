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
//' =============================================
//' Template: BusinessObject.xslt 17/10/2006
//' Author:	Nguyen duc quy
//' Create date:	06/006/2008 21:40
//' Description:
//' Revise History:
//' =============================================

namespace BR.BRBusinessObject
{
    public class MSG_XLSController
    {
        public DataTable MSG_XLS(string pGWTYPE, string pMSG_TYPE, string pMSG_DIRECTION)
        {
            return MSG_XLSDP.Instance().MSG_XLS(pGWTYPE, pMSG_TYPE, pMSG_DIRECTION);
        }
        public DataTable MSG_XLS_VCB(string pGWTYPE, string pMSG_TYPE, string pMSG_DIRECTION)
        {
            return MSG_XLSDP.Instance().MSG_XLS_VCB(pGWTYPE, pMSG_TYPE, pMSG_DIRECTION);
        }
        //-----------------------------------------------------------------------------------
        public DataTable COLUMNS_MSG_XLS(string pGWTYPE, string pMSG_TYPE, string pFIELD_NAME, string pMSG_DIRECTION)
        {
            return MSG_XLSDP.Instance().COLUMNS_MSG_XLS(pGWTYPE, pMSG_TYPE, pFIELD_NAME, pMSG_DIRECTION);
        }
        public DataTable COLUMNS_MSG_XLS_SWIFT(string pGWTYPE, string pMSG_TYPE, string pFIELD_NAME, string pMSG_DIRECTION)
        {
            return MSG_XLSDP.Instance().COLUMNS_MSG_XLS_SWIFT(pGWTYPE, pMSG_TYPE, pFIELD_NAME, pMSG_DIRECTION);
        }
        public DataTable Columns_Check(string pGWTYPE, string pMSG_TYPE, string pFIELD_NAME, string pMSG_DIRECTION)
        {
            return MSG_XLSDP.Instance().Columns_Check(pGWTYPE, pMSG_TYPE, pFIELD_NAME, pMSG_DIRECTION);
        }

        public DataTable MSG_XLS_IBPS(string pGWTYPE, string pMSG_DIRECTION)
        {
            return MSG_XLSDP.Instance().MSG_XLS_IBPS(pGWTYPE, pMSG_DIRECTION);
        }
    }
}
