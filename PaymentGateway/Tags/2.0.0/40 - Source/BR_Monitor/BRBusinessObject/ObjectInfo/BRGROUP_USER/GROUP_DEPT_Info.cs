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


//' Template: InfoClass.xslt 17/10/2006
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class GROUP_DEPTInfo
	{
		
		private double _GROUPID;
		private double _GWTYPE_DEPT_ID;
		
		public GROUP_DEPTInfo()
		{
			
		}
		
		public double GROUPID
		{
			get
			{
				return _GROUPID;
			}
			set
			{
				_GROUPID = value;
			}
		}
		
		public double GWTYPE_DEPT_ID
		{
			get
			{
				return _GWTYPE_DEPT_ID;
			}
			set
			{
				_GWTYPE_DEPT_ID = value;
			}
		}
		
	}
	
	
}
