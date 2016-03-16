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
	public class TADInfo
	{
		
		private int _TADID;
		private string _TAD;
        private string _TAD_NAME;
		private string _GW_BANK_CODE;
        private string _EXPORT_FOLDER;
        private string _IMPORT_FOLDER;
        private string _FTPPATH;
        private string _FTPUSER;
        private string _FPTPASS;
        private string _AREA;
        private string  _TIME;
        private double _AMOUNT;
        private int _STATUS;
        private string _SIBS_BANK_CODE;
        private int _SET_LOW_VALUE;
        private string _CCYCD;
        private int _CONNECTION;
        private string _FUNCTION;
        private string _DBLINK;
        private string _SBV_TADID;
		public TADInfo()
		{
			
		}
		
		public int TADID
		{
			get
			{
				return _TADID;
			}
			set
			{
				_TADID = value;
			}
		}

        public int CONNECTION
        {
            get
            {
                return _CONNECTION;
            }
            set
            {
                _CONNECTION = value;
            }
        }

		public string TAD
		{
			get
			{
				return _TAD;
			}
			set
			{
				_TAD = value;
			}
		}

        public string TAD_NAME
		{
			get
			{
                return _TAD_NAME;
			}
			set
			{
                _TAD_NAME = value;
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
		
		public string EXPORT_FOLDER
		{
			get
			{
				return _EXPORT_FOLDER;
			}
			set
			{
				_EXPORT_FOLDER = value;
			}
		}
		
		public string IMPORT_FOLDER
		{
			get
			{
				return _IMPORT_FOLDER;
			}
			set
			{
				_IMPORT_FOLDER = value;
			}
		}

        public string FTPPATH
		{
			get
			{
                return _FTPPATH;
			}
			set
			{
                _FTPPATH = value;
			}
		}

        public string FTPUSER
		{
			get
			{
                return _FTPUSER;
			}
			set
			{
                _FTPUSER = value;
			}
		}

        public string FPTPASS
		{
			get
			{
                return _FPTPASS;
			}
			set
			{
                _FPTPASS = value;
			}
		}

        public string AREA
        {
            get
            {
                return _AREA;
            }
            set
            {
                _AREA = value;
            }
        }

        public string FUNCTION
        {
            get
            {
                return _FUNCTION;
            }
            set
            {
                _FUNCTION = value;
            }
        }

        public string TIME
        {
            get
            {
                return _TIME;
            }
            set
            {
                _TIME = value;
            }
        }

        public double AMOUNT
        {
            get
            {
                return _AMOUNT;
            }
            set
            {
                _AMOUNT = value;
            }
        }

        public int STATUS
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

        public string DBLINK
        {
            get
            {
                return _DBLINK;
            }
            set
            {
                _DBLINK = value;
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

        public int SET_LOW_VALUE
        {
            get
            {
                return _SET_LOW_VALUE;
            }
            set
            {
                _SET_LOW_VALUE = value;
            }
        }

        public string CCYCD
        {
            get
            {
                return _CCYCD;
            }
            set
            {
                _CCYCD = value;
            }
        }
        //_SBV_TADID
        public string SBV_TADID
        {
            get
            {
                return _SBV_TADID;
            }
            set
            {
                _SBV_TADID = value;
            }
        }
	}
	
	
}
