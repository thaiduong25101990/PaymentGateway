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
	public class MSG_LOGInfo
	{
		
		private int _LOG_ID;
		private DateTime _LOG_DATE;
		private string _USERNAME;
		private string _CONTENT;
		private string _GWTYPE;
		private string _MSG_TYPE;
		private int _LOG_LEVEL;
		
		public MSG_LOGInfo()
		{
			
		}
		
		public int LOG_ID
		{
			get
			{
				return _LOG_ID;
			}
			set
			{
				_LOG_ID = value;
			}
		}
		
		public DateTime LOG_DATE
		{
			get
			{
				return _LOG_DATE;
			}
			set
			{
				_LOG_DATE = value;
			}
		}
		
		public string USERNAME
		{
			get
			{
				return _USERNAME;
			}
			set
			{
				_USERNAME = value;
			}
		}
		
		public string CONTENT
		{
			get
			{
				return _CONTENT;
			}
			set
			{
				_CONTENT = value;
			}
		}
		
		public string GWTYPE
		{
			get
			{
				return _GWTYPE;
			}
			set
			{
				_GWTYPE = value;
			}
		}
		
		public string MSG_TYPE
		{
			get
			{
				return _MSG_TYPE;
			}
			set
			{
				_MSG_TYPE = value;
			}
		}
		
		public int LOG_LEVEL
		{
			get
			{
				return _LOG_LEVEL;
			}
			set
			{
				_LOG_LEVEL = value;
			}
		}
		
	}
	
	
}
