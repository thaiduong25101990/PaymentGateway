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
	public class MSG_LISTInfo
	{
		
		private double _MSG_LIST_ID;
		private string _MSG_DEF_ID;
		private string _MSG_DESCRIPTION;
		private double _SIBS_MSG_LENGTH;
		private double _GW_MSG_LENGTH;
		
		public MSG_LISTInfo()
		{
			
		}
		
		public double MSG_LIST_ID
		{
			get
			{
				return _MSG_LIST_ID;
			}
			set
			{
				_MSG_LIST_ID = value;
			}
		}
		
		public string MSG_DEF_ID
		{
			get
			{
				return _MSG_DEF_ID;
			}
			set
			{
				_MSG_DEF_ID = value;
			}
		}
		
		public string MSG_DESCRIPTION
		{
			get
			{
				return _MSG_DESCRIPTION;
			}
			set
			{
				_MSG_DESCRIPTION = value;
			}
		}
		
		public double SIBS_MSG_LENGTH
		{
			get
			{
				return _SIBS_MSG_LENGTH;
			}
			set
			{
				_SIBS_MSG_LENGTH = value;
			}
		}
		
		public double GW_MSG_LENGTH
		{
			get
			{
				return _GW_MSG_LENGTH;
			}
			set
			{
				_GW_MSG_LENGTH = value;
			}
		}
		
	}
	
	
}
