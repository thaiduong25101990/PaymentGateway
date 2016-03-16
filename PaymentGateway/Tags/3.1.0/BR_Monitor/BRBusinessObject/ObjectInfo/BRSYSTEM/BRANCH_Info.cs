using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class BRANCHInfo
    {
        private int _ID;
        private string _SIBS_BANK_CODE;
        private string _BRAN_TYPE;
        private string _BRAN_APP;
        private string _BRAN_NAME;
        private string _PROV_CODE;

        private string _LONG_BRAN_NAME;
        private string _SECRET_KEY;
        private string _PUBLIC_KEY;
        private string _ADDRESS;
        private string _TEL;
        private string _FAX;

        private DateTime _EDATE;
        private int _STATUS;
        private string _SBANK_CODE;

        public BRANCHInfo()
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

        public string BRAN_TYPE
        {
            get
            {
                return _BRAN_TYPE;
            }
            set
            {
                _BRAN_TYPE = value;
            }
        }

        public string BRAN_APP
        {
            get
            {
                return _BRAN_APP;
            }
            set
            {
                _BRAN_APP = value;
            }
        }

        public string BRAN_NAME
        {
            get
            {
                return _BRAN_NAME;
            }
            set
            {
                _BRAN_NAME = value;
            }
        }

        public string PROV_CODE
        {
            get
            {
                return _PROV_CODE;
            }
            set
            {
                _PROV_CODE = value;
            }
        }

        public string LONG_BRAN_NAME
        {
            get
            {
                return _LONG_BRAN_NAME;
            }
            set
            {
                _LONG_BRAN_NAME = value;
            }
        }

        public string SECRET_KEY
        {
            get
            {
                return _SECRET_KEY;
            }
            set
            {
                _SECRET_KEY = value;
            }
        }

        public string PUBLIC_KEY
        {
            get
            {
                return _PUBLIC_KEY;
            }
            set
            {
                _PUBLIC_KEY = value;
            }
        }

        public string ADDRESS
        {
            get
            {
                return _ADDRESS;
            }
            set
            {
                _ADDRESS = value;
            }
        }

        public string TEL
        {
            get
            {
                return _TEL;
            }
            set
            {
                _TEL = value;
            }
        }

        public string FAX
        {
            get
            {
                return _FAX;
            }
            set
            {
                _FAX = value;
            }
        }

        public DateTime EDATE
        {
            get
            {
                return _EDATE;
            }
            set
            {
                _EDATE = value;
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

        public string SBANK_CODE
        {
            get
            {
                return _SBANK_CODE;
            }
            set
            {
                _SBANK_CODE = value;
            }
        }

    }
}
