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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:39
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class IBPS_MSG_REC_TADController
    {

        public int AddIBPS_MSG_REC_TAD(IBPS_MSG_REC_TADInfo objTable)
        {
            return IBPS_MSG_REC_TADDP.Instance().AddIBPS_MSG_REC_TAD(objTable);
        }

        public int ClearIBPS_MSG_REC_TAD()
        {
            return IBPS_MSG_REC_TADDP.Instance().ClearIBPS_MSG_REC_TAD();
        }

        public DataSet GetIBPS_MSG_REC_TAD()
        {
            return IBPS_MSG_REC_TADDP.Instance().GetIBPS_MSG_REC_TAD();
        }

    }


}
