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
    public class IQS_MSG_LOSGController
    {
        public DataTable GetIQS_LOG(string pQUIRYID)
        {
            return IQS_MSG_LOGDP.Instance().GetIQS_LOG(pQUIRYID);
        }
    }
}
