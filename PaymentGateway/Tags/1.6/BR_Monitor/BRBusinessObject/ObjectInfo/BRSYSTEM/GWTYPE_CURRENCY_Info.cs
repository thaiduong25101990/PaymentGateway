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
	public class GWTYPE_CURRENCYInfo
	{
		
		private double _CURRENCY_ID;
		private string _GWTYPE;
		private double _STATUS;
		private double _DECIMAL_GW;
		private string _NOTE;
		
		public GWTYPE_CURRENCYInfo()
		{
			
		}
		
		public double CURRENCY_ID
		{
			get
			{
				return _CURRENCY_ID;
			}
			set
			{
				_CURRENCY_ID = value;
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
		
		public double STATUS
		{
			get
			{
				return _STATUS;
			}
			set
			{
				_STATUS = value;
			}
		}
		
		public double DECIMAL_GW
		{
			get
			{
				return _DECIMAL_GW;
			}
			set
			{
				_DECIMAL_GW = value;
			}
		}
		
		public string NOTE
		{
			get
			{
				return _NOTE;
			}
			set
			{
				_NOTE = value;
			}
		}
		
	}
	
	
}
