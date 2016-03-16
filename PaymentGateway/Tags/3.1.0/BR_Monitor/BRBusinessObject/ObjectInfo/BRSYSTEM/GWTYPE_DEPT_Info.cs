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
	public class GWTYPE_DEPTInfo
	{
		
		private double _ID;
		private string _GWTYPE;
		private string _DEPT_CODE;
		private string _FUNCTION_IN;
		private string _FUNCTION_MAIN;
		private string _GWTYPE_DEPT;
		private string _TRANS_CODE_IN;
		private string _TRANS_CODE_MAIN;
		
		public GWTYPE_DEPTInfo()
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
		
		public string DEPT_CODE
		{
			get
			{
				return _DEPT_CODE;
			}
			set
			{
				_DEPT_CODE = value;
			}
		}
		
		public string FUNCTION_IN
		{
			get
			{
				return _FUNCTION_IN;
			}
			set
			{
				_FUNCTION_IN = value;
			}
		}
		
		public string FUNCTION_MAIN
		{
			get
			{
				return _FUNCTION_MAIN;
			}
			set
			{
				_FUNCTION_MAIN = value;
			}
		}
		
		public string GWTYPE_DEPT
		{
			get
			{
				return _GWTYPE_DEPT;
			}
			set
			{
				_GWTYPE_DEPT = value;
			}
		}
		
		public string TRANS_CODE_IN
		{
			get
			{
				return _TRANS_CODE_IN;
			}
			set
			{
				_TRANS_CODE_IN = value;
			}
		}
		
		public string TRANS_CODE_MAIN
		{
			get
			{
				return _TRANS_CODE_MAIN;
			}
			set
			{
				_TRANS_CODE_MAIN = value;
			}
		}
		
	}
	
	
}
