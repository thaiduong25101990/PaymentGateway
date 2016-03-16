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
    public class STATUSController
    {
        public DataTable GET_STATUS(string pGWTYPE, out DataTable _dt)
        {
            return STATUSDP.Instance().GET_STATUS(pGWTYPE, out _dt);
        }

        public DataSet STATUS_ERROR_CODE(string pGWTYPE, out DataSet _ds)
        {
            return STATUSDP.Instance().STATUS_ERROR_CODE(pGWTYPE, out _ds);
        }
        //ham lay trang thai STATUS cua xu ly thu cong voi menh de where truyen vao
        public DataTable SWIFT_STATUS(string pWhere, out DataTable _dt)
        {
            return STATUSDP.Instance().SWIFT_STATUS(pWhere, out _dt);
        }
    }
}
