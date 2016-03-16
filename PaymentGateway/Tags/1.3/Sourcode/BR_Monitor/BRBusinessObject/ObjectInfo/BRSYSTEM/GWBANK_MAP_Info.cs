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
	public class GWBANK_MAPInfo
	{
		
		private double _GWBANK_MAP_ID;
		private string _SIBS_BANK_CODE;
		private string _BANK_NAME;
		private string _GW_BANK_CODE;
		private double _BRANCH;
		private string _DESCRIPTION;
		
		public GWBANK_MAPInfo()
		{
			
		}
		
		public double GWBANK_MAP_ID
		{
			get
			{
				return _GWBANK_MAP_ID;
			}
			set
			{
				_GWBANK_MAP_ID = value;
			}
		}
		
		public string SIBS_BANK_CODE
		{
			get
			{
				return _SIBS_BANK_CODE;
			}
			set
			{
				_SIBS_BANK_CODE = value;
			}
		}
		
		public string BANK_NAME
		{
			get
			{
				return _BANK_NAME;
			}
			set
			{
				_BANK_NAME = value;
			}
		}
		
		public string GW_BANK_CODE
		{
			get
			{
				return _GW_BANK_CODE;
			}
			set
			{
				_GW_BANK_CODE = value;
			}
		}
		
		public double BRANCH
		{
			get
			{
				return _BRANCH;
			}
			set
			{
				_BRANCH = value;
			}
		}
		
		public string DESCRIPTION
		{
			get
			{
				return _DESCRIPTION;
			}
			set
			{
				_DESCRIPTION = value;
			}
		}
		
	}
	
	
}
