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
	public class GROUP_MENUInfo
	{
		
		private int  _GROUPID;
		private string _MENUID;
		private int _ISINQUIRY;
        private int _ISDELETE;
        private int _ISINSERT;
        private int _ISEDIT;
		
		public GROUP_MENUInfo()
		{
			
		}

        public int GROUPID
		{
			get
			{
				return _GROUPID;
			}
			set
			{
				_GROUPID = value;
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

        public int ISINQUIRY
		{
			get
			{
				return _ISINQUIRY;
			}
			set
			{
				_ISINQUIRY = value;
			}
		}

        public int ISDELETE
		{
			get
			{
				return _ISDELETE;
			}
			set
			{
				_ISDELETE = value;
			}
		}

        public int ISINSERT
		{
			get
			{
				return _ISINSERT;
			}
			set
			{
				_ISINSERT = value;
			}
		}

        public int ISEDIT
		{
			get
			{
				return _ISEDIT;
			}
			set
			{
				_ISEDIT = value;
			}
		}
		
	}
	
	
}
