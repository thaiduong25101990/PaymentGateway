using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIDVWEB.Business
{
    public class REPORT_PARA_INFO
    {
        private long _ID;
        private string _SIBS_BANK_CODE;
        private string _TIME;
        private string _DESCRIPTION;
        private int _ID_REPORT;
        private string _REPORTNAME;

        public REPORT_PARA_INFO()
		{
			
		}
		
		public long  ID
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

        public int ID_REPORT
        {
            get
            {
                return _ID_REPORT;
            }
            set
            {
                _ID_REPORT = value;
            }
        }

        public string REPORTNAME
        {
            get
            {
                return _REPORTNAME;
            }
            set
            {
                _REPORTNAME = value;
            }
        }
    }
}
