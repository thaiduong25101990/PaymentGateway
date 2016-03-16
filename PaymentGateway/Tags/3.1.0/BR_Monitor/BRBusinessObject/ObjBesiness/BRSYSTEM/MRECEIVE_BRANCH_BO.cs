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
    public class MRECEIVE_BRANCHController
    {
        public int UPDATE_MRECIVER(string pTRAN_NO)
        {
            return MRECEIVE_BRANCHDP.Instance().UPDATE_MRECIVER(pTRAN_NO);
        }
    }
}
