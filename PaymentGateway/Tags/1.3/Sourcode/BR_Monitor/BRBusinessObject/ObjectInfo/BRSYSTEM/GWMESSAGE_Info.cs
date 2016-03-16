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
	public class GWMESSAGEInfo
	{
		
		private string _ERR_CODE;
		private string _ERR_TYPE;
		private string _MSG_TYPE;
		private string _APP_ERR_CODE;
		private string _APP_ERR_MSG;
		
		public GWMESSAGEInfo()
		{
			
		}
		
		public string ERR_CODE
		{
			get
			{
				return _ERR_CODE;
			}
			set
			{
				_ERR_CODE = value;
			}
		}
		
		public string ERR_TYPE
		{
			get
			{
				return _ERR_TYPE;
			}
			set
			{
				_ERR_TYPE = value;
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
		
		public string APP_ERR_CODE
		{
			get
			{
				return _APP_ERR_CODE;
			}
			set
			{
				_APP_ERR_CODE = value;
			}
		}
		
		public string APP_ERR_MSG
		{
			get
			{
				return _APP_ERR_MSG;
			}
			set
			{
				_APP_ERR_MSG = value;
			}
		}
		
	}
	
	
}
