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
//' Author:	Nguyen Thuy Dung
//' Create date:	28/05/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
    public class IBPS_BANK_TADController
    {

        public int AddIBPS_BANK_TAD(IBPS_BANK_TADInfo objTable)
        {
            return IBPS_BANK_TADDP.Instance().AddIBPS_BANK_TAD(objTable);
        }

        public int UpdateIBPS_BANK_TAD(IBPS_BANK_TADInfo objTable)
        {
            return IBPS_BANK_TADDP.Instance().UpdateIBPS_BANK_TAD(objTable);
        }

        public int DeleteIBPS_BANK_TAD(double BANK_MAP_ID)
        {
            return IBPS_BANK_TADDP.Instance().DeleteIBPS_BANK_TAD(BANK_MAP_ID);
        }

        public DataSet GetIBPS_BANK_TAD(double BANK_MAP_ID)
        {
            return IBPS_BANK_TADDP.Instance().GetIBPS_BANK_TAD(BANK_MAP_ID);
        }
    }

}
