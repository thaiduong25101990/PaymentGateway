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
	public class USER_PASSInfo
	{
		
		private int _ID;
		private string _USER_ID;
		private string _PRE_PASS;
		private DateTime _PASSTIME;
		
		public USER_PASSInfo()
		{
			
		}
		
		public int  ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}
		
		public string  USER_ID
		{
			get
			{
				return _USER_ID;
			}
			set
			{
				_USER_ID = value;
			}
		}
		
		public string PRE_PASS
		{
			get
			{
				return _PRE_PASS;
			}
			set
			{
				_PRE_PASS = value;
			}
		}

        public DateTime PASSTIME
		{
			get
			{
				return _PASSTIME;
			}
			set
			{
				_PASSTIME = value;
			}
		}
		
	}
	
	
}
