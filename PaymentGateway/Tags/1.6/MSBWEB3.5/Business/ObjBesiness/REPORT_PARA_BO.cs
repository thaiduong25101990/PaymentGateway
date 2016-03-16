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
namespace BIDVWEB.Business
{
    public class REPORT_PARA_BO
    {
        public int AddREPORT_PARA(REPORT_PARA_INFO objTable)
        {
            return REPORT_PARA_DAO.Instance().AddREPORT_PARA(objTable);
        }

        public int UpdateREPORT_PARA(REPORT_PARA_INFO objTable)
        {
            return REPORT_PARA_DAO.Instance().UpdateREPORT_PARA(objTable);
        }
    }
}
