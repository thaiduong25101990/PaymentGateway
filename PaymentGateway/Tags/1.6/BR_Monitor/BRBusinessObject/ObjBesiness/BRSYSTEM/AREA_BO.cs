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
    public class AREAController
    {
        public DataSet GetAREA(string strCondition)
        {
            return AREADP.Instance().GetAREA(strCondition);
        }
        /// <summary>
        /// BICHNN add 12/08/2008
        /// Get area data
        /// </summary>
        /// <param name="objAREAInfo"></param>
        /// <returns>DataSet</returns>
        public DataSet GetAREA()
        {
            return AREADP.Instance().GetAREA();
        }
        public DataSet GetAREA_GWBankCode(string strGWBankCode)
        {
            return AREADP.Instance().GetAREA_GWBankCode(strGWBankCode);
        }
        public int InsertAREA(AREAInfo objAREAInfo)
        {
            return AREADP.Instance().InsertAREA(objAREAInfo);
        }

        public int UpdateAREA(AREAInfo objAREAInfo)
        {
            return AREADP.Instance().UpdateAREA(objAREAInfo);
        }

        public int DeleteAREA(int iID)
        {
            return AREADP.Instance().DeleteAREA(iID);
        }
        public DataTable Search(string pPROV_CODE)
        {
            return AREADP.Instance().Search(pPROV_CODE);
        }
    }
}
