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
    public class SWIFT_BR_AUTOController
    {

        public int AddSWIFT_BR_AUTO(SWIFT_BR_AUTOInfo objTable)
        {
            return SWIFT_BR_AUTODP.Instance().AddSWIFT_BR_AUTO(objTable);
        }
        public DataTable Check_Br(string pWhere)
        {
            return SWIFT_BR_AUTODP.Instance().Check_Br(pWhere);
        }
    }
}
