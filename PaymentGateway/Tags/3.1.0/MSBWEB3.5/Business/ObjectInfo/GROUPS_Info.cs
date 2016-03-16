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


namespace BIDVWEB.Business
{
	public class GROUPSInfo
	{
		
		private int  _GROUPID;
		private string _GROUPNAME;
        private int  _ISSUPERVISOR;
		private string _GWTYPE;
        private string  _DEPARTMENT;
        private string _DESCRIPTION;
        private int _ISADMIN;
        private string _BRANCHID;
	
		public GROUPSInfo()
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
       
		public string GROUPNAME
		{
			get
			{
				return _GROUPNAME;
			}
			set
			{
				_GROUPNAME = value;
			}
		}
        public int ISSUPERVISOR
        {
            get
            {
                return _ISSUPERVISOR;
            }
            set
            {
                _ISSUPERVISOR = value;
            }
        }

        public int ISADMIN
        {
            get
            {
                return _ISADMIN;
            }
            set
            {
                _ISADMIN = value;
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
        public string DEPARTMENT
        {
            get
            {
                return _DEPARTMENT;
            }
            set
            {
                _DEPARTMENT = value;
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
        public string BRANCHID
        {
            get
            {
                return _BRANCHID;
            }
            set
            {
                _BRANCHID = value;
            }
        }
		
	}
	
	
}
