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

namespace BR.BRBusinessObject
{
    public class ERROR_CODEContrller
    {
        public DataTable GET_ERROR_CODE(string strGWTYPE, out DataTable _dt)
        {
            return ERROR_CODEDP.Instance().GET_ERROR_CODE(strGWTYPE, out _dt);
        }
    }
}
