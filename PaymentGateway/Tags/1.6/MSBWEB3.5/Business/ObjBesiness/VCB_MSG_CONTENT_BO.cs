﻿using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
/*=============================================
 Template:
 Author:	
 Create date:	11/04/2010
 Description:
 Revise History:
 =============================================*/
namespace BIDVWEB.Business
{
    public class VCB_MSG_CONTENTController
    {        
        public int Update_Print_STS(VCB_MSG_CONTENTInfo objTable)
        {
            return VCB_MSG_CONTENTDP.Instance().Update_Print_STS(objTable);
        }

        public int Check_Print_STS(VCB_MSG_CONTENTInfo objTable)
        {
            return VCB_MSG_CONTENTDP.Instance().Check_Print_STS(objTable);
        }
    }
}
