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
	public class GW_PARAMETERInfo
	{
		
		private double _ID;
		private string _GWTYPE;
		private string _PARA_NAME;
		private string _COMMAND_TEXT;
		
		public GW_PARAMETERInfo()
		{
			
		}
		
		public double ID
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
		
		public string PARA_NAME
		{
			get
			{
				return _PARA_NAME;
			}
			set
			{
				_PARA_NAME = value;
			}
		}
		
		public string COMMAND_TEXT
		{
			get
			{
				return _COMMAND_TEXT;
			}
			set
			{
				_COMMAND_TEXT = value;
			}
		}
		
	}
	
	
}
