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
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GWMESSAGEController
	{
		
		public int AddGWMESSAGE(GWMESSAGEInfo objTable)
		{
			return GWMESSAGEDP.Instance().AddGWMESSAGE(objTable);
		}
		
		public int UpdateGWMESSAGE(GWMESSAGEInfo objTable)
		{
			return GWMESSAGEDP.Instance().UpdateGWMESSAGE(objTable);
		}

        public int DeleteGWMESSAGE(GWMESSAGEInfo objTable)
		{
			return GWMESSAGEDP.Instance().DeleteGWMESSAGE(objTable);
		}
		
		
	}
	
	
}
