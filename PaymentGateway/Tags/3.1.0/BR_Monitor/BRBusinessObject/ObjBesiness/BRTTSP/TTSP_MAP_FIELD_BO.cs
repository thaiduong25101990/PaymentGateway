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
//' Author:	        Pham Van Dong
//' Create date:	02/02/2012
//' Description:
//' Revise History:
//' =============================================

namespace BR.BRBusinessObject
{
    public class TTSP_MAP_FIELD_BOController
    {
        public DataSet GetMapFields(string strMsgType)
        {
            return TTSP_MAP_FIELD.Instance().GetMapFields(strMsgType);
        }

    }
}
