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
	public class MENUInfo
	{
        private int _ID;
		private string _MENUID;
		private string _PARENTID;
		private string _CAPTION;
		private string _ASSEMBLY;
		private string _ASSEMBLYFILE;
		private string _METHOD;
		private double _ENABLE;
		private double _CTRL;
		private double _ALT;
		private string _KEY;
		private string _GWTYPE;
		private string _OPTIONDATA;
		private string _TOOLTIPTEXT;
		private double _ORDERMENU;
		
		public MENUInfo()
		{
			
		}
        public int ID
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
		
		public string MENUID
		{
			get
			{
				return _MENUID;
			}
			set
			{
				_MENUID = value;
			}
		}
		
		public string PARENTID
		{
			get
			{
				return _PARENTID;
			}
			set
			{
				_PARENTID = value;
			}
		}
		
		public string CAPTION
		{
			get
			{
				return _CAPTION;
			}
			set
			{
				_CAPTION = value;
			}
		}
		
		public string Assembly
		{
			get
			{
				return _ASSEMBLY;
			}
			set
			{
				_ASSEMBLY = value;
			}
		}
		
		public string ASSEMBLYFILE
		{
			get
			{
				return _ASSEMBLYFILE;
			}
			set
			{
				_ASSEMBLYFILE = value;
			}
		}
		
		public string METHOD
		{
			get
			{
				return _METHOD;
			}
			set
			{
				_METHOD = value;
			}
		}
		
		public double ENABLE
		{
			get
			{
				return _ENABLE;
			}
			set
			{
				_ENABLE = value;
			}
		}
		
		public double CTRL
		{
			get
			{
				return _CTRL;
			}
			set
			{
				_CTRL = value;
			}
		}
		
		public double ALT
		{
			get
			{
				return _ALT;
			}
			set
			{
				_ALT = value;
			}
		}
		
		public string KEY
		{
			get
			{
				return _KEY;
			}
			set
			{
				_KEY = value;
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
		
		public string OPTIONDATA
		{
			get
			{
				return _OPTIONDATA;
			}
			set
			{
				_OPTIONDATA = value;
			}
		}
		
		public string TOOLTIPTEXT
		{
			get
			{
				return _TOOLTIPTEXT;
			}
			set
			{
				_TOOLTIPTEXT = value;
			}
		}
		
		public double ORDERMENU
		{
			get
			{
				return _ORDERMENU;
			}
			set
			{
				_ORDERMENU = value;
			}
		}
		
	}
	
	
}
