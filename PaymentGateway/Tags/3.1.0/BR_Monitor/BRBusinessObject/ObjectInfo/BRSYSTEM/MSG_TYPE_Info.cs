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
	public class MSG_TYPEInfo
	{
		
		private double _MSG_ID;
		private string _MSG_CODE;
		private string _MSG_NAME;
		
		public MSG_TYPEInfo()
		{
			
		}
		
		public double MSG_ID
		{
			get
			{
				return _MSG_ID;
			}
			set
			{
				_MSG_ID = value;
			}
		}
		
		public string MSG_CODE
		{
			get
			{
				return _MSG_CODE;
			}
			set
			{
				_MSG_CODE = value;
			}
		}
		
		public string MSG_NAME
		{
			get
			{
				return _MSG_NAME;
			}
			set
			{
				_MSG_NAME = value;
			}
		}
		
	}
	
	
}
