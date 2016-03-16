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
    public class MSG_CONTENT_TEMPController
    {

        public int AddMSG_CONTENT_TEMP(MSG_CONTENT_TEMPInfo objTable)
        {
            return MSG_CONTENT_TEMPDP.Instance().AddMSG_CONTENT_TEMP(objTable);
        }

        public int ClearMSG_CONTENT_TEMP()
        {
            return MSG_CONTENT_TEMPDP.Instance().ClearMSG_CONTENT_TEMP();
        }

        public DataSet GetMSG_CONTENT_TEMP(string dDate, string strDepartment, string strDirection,string strChannel)
        {
            return MSG_CONTENT_TEMPDP.Instance().GetMSG_CONTENT_TEMP(dDate, strDepartment, strDirection, strChannel);
        }

    }


}
