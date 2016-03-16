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
	public class USERSInfo
	{
		private int _ID;
		private string _BRANCH;
		private string _USERID;
		private string _USERNAME;
		private string _PASSWORD;
		private int _STATUS;
		private DateTime _PASSTIME;
		private string _MOBILE;
		private string _EMAIL;
		private string _DESCRIPTION;
		private DateTime _LASTDATE;
        private int _COUNTTIME;
		
 	
		public USERSInfo()
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
        public string BRANCH
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
        public string USERID
        {
            get
            {
                return _USERID;
            }
            set
            {
                _USERID = value;
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
		
		public string PASSWORD
		{
			get
			{
				return _PASSWORD;
			}
			set
			{
				_PASSWORD = value;
			}
		}
						
		public int  STATUS
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

        public DateTime PASSTIME
		{
			get
			{
                return _PASSTIME;
			}
			set
			{
                _PASSTIME = value;
			}
		}		
		
		public string MOBILE
		{
			get
			{
				return _MOBILE;
			}
			set
			{
				_MOBILE = value;
			}
		}
		
		public string EMAIL
		{
			get
			{
				return _EMAIL;
			}
			set
			{
				_EMAIL = value;
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
		
		public DateTime LASTDATE
		{
			get
			{
				return _LASTDATE;
			}
			set
			{
				_LASTDATE = value;
			}
		}

        public int COUNTTIME
        {
            get
            {
                return _COUNTTIME;
            }
            set
            {
                _COUNTTIME = value;
            }
        }
		
	}
	
	
}
