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
	public class GW_PARAMETERController
	{
		
		public int AddGW_PARAMETER(GW_PARAMETERInfo objTable)
		{
			return GW_PARAMETERDP.Instance().AddGW_PARAMETER(objTable);
		}
		
		public int UpdateGW_PARAMETER(GW_PARAMETERInfo objTable)
		{
			return GW_PARAMETERDP.Instance().UpdateGW_PARAMETER(objTable);
		}

        public int DeleteGW_PARAMETER(GW_PARAMETERInfo objTable)
		{
			return GW_PARAMETERDP.Instance().DeleteGW_PARAMETER(objTable);
		}
		
		
	}
	
	
}
