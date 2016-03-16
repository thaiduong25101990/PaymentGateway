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
    public class IBPS_TRANS_MAPController
    {
        public DataTable Getdata(string pGW_TRANS_CODE)
        {
            return IBPS_TRANS_MAPDP.Instance().Getdata(pGW_TRANS_CODE);
        }
    }
}
