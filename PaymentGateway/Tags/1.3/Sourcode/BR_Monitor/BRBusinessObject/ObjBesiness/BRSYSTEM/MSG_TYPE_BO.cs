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
	public class MSG_TYPEController
	{
		
		public int AddMSG_TYPE(MSG_TYPEInfo objTable)
		{
			return MSG_TYPEDP.Instance().AddMSG_TYPE(objTable);
		}
		
		public int UpdateMSG_TYPE(MSG_TYPEInfo objTable)
		{
			return MSG_TYPEDP.Instance().UpdateMSG_TYPE(objTable);
		}
		
		public int DeleteMSG_TYPE(int MSG_ID)
		{
            return MSG_TYPEDP.Instance().DeleteMSG_TYPE(MSG_ID);
		}
		
		
	}
	
	
}
