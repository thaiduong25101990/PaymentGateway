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
    public class TADMAPController
    {
        public int INSERT_TADMAP(TADMAPInfo ObjTable)
        {
            return TADMAPDP.Instance().INSERT_TADMAP(ObjTable);
        }

        public DataSet LOAD_DATA(string pGW_BANK_CODE,string pSIBS_BANK_CODE)
        {
            return TADMAPDP.Instance().LOAD_DATA(pGW_BANK_CODE,pSIBS_BANK_CODE);
        }

        public DataTable LOAD_COMBO()
        {
            return TADMAPDP.Instance().LOAD_COMBO();
        }

        public int DELETE_TADMAP(string pTADHO)
        {
            return TADMAPDP.Instance().DELETE_TADMAP(pTADHO);
        }
    }
}
