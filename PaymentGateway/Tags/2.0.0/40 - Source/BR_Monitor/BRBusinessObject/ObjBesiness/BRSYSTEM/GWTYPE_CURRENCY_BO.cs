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
	public class GWTYPE_CURRENCYController
	{
		
		public int AddGWTYPE_CURRENCY(GWTYPE_CURRENCYInfo objTable)
		{
			return GWTYPE_CURRENCYDP.Instance().AddGWTYPE_CURRENCY(objTable);
		}
		
		public int UpdateGWTYPE_CURRENCY(GWTYPE_CURRENCYInfo objTable)
		{
			return GWTYPE_CURRENCYDP.Instance().UpdateGWTYPE_CURRENCY(objTable);
		}
		
		public int DeleteGWTYPE_CURRENCY(int Currency_ID)
		{
            return GWTYPE_CURRENCYDP.Instance().DeleteGWTYPE_CURRENCY(Currency_ID);
		}
		
		
	}
	
	
}
