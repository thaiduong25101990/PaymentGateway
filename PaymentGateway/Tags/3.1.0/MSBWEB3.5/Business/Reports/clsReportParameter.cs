using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIDVWEB.Business.Reports
{
    public class clsReportParameter
    {
        private DateTime _tFromDate;
        private DateTime _tToDate;                        
        private string[] _parm = new string[10];
        private string _sCiTad;
        private string _sBranchA;
        private string _sBranchB;
        private string _sSession;
        private string _sTitle;

        public clsReportParameter()
		{			
		}

        public DateTime tFromDate
        {
            get
            {
                return _tFromDate;
            }
            set
            {
                _tFromDate = value;
            }
        }

        public DateTime tToDate
        {
            get
            {
                return _tToDate;
            }
            set
            {
                _tToDate = value;
            }
        }

        public string[] sparm
        {
            get
            {
                return _parm;
            }
            set
            {
                _parm = value;
            }
        }

        public string sCiTad
        {
            get
            {
                return _sCiTad;
            }
            set
            {
                _sCiTad = value;
            }
        }

        public string sBranchA
        {
            get
            {
                return _sBranchA;
            }
            set
            {
                _sBranchA = value;
            }
        }
        
        public string sBranchB
        {
            get
            {
                return _sBranchB;
            }
            set
            {
                _sBranchB = value;
            }
        }

        public string sSession
        {
            get
            {
                return _sSession;
            }
            set
            {
                _sSession = value;
            }
        }

        public string sTitle
        {
            get
            {
                return _sTitle;
            }
            set
            {
                _sTitle = value;
            }
        }

    }
}
