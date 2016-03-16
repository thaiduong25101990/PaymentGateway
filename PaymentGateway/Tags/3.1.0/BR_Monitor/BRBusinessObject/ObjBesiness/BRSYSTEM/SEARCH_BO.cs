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
//' Create date:	10/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SEARCHController
    {//GetSEARCH_Operator
        public DataTable COLUMNS_SEARCH(string pGwtype, out DataTable _dt)
        {
            return SEARCHDP.Instance().COLUMNS_SEARCH(pGwtype, out _dt);
        }
        public DataSet GetSEARCH_Operator(string pOperator,string pGwtype)
        {
            return SEARCHDP.Instance().GetSEARCH_Operator(pOperator, pGwtype);
        }
        public DataTable Getdata(string pCDNAME, string pGwtype)
        {
            return SEARCHDP.Instance().Getdata(pCDNAME, pGwtype);
        }

        public DataTable Excute_Select(string pSelect)
        {
            return SEARCHDP.Instance().Excute_Select(pSelect);
        }
        /*Quynd cap nhat 20100318*/
        public DataTable dtSearch(string pGWTYPE)
        {
            return SEARCHDP.Instance().dtSearch(pGWTYPE);
        }
    }
}
