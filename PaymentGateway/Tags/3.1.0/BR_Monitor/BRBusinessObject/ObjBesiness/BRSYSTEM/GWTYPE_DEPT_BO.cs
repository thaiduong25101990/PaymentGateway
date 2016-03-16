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
	public class GWTYPE_DEPTController
	{
		
		public int AddGWTYPE_DEPT(GWTYPE_DEPTInfo objTable)
		{
			return GWTYPE_DEPTDP.Instance().AddGWTYPE_DEPT(objTable);
		}
		
		public int UpdateGWTYPE_DEPT(GWTYPE_DEPTInfo objTable)
		{
			return GWTYPE_DEPTDP.Instance().UpdateGWTYPE_DEPT(objTable);
		}

        public int DeleteGWTYPE_DEPT(GWTYPE_DEPTInfo objTable)
		{
			return GWTYPE_DEPTDP.Instance().DeleteGWTYPE_DEPT(objTable);
		}
		
		
	}
	
	
}
