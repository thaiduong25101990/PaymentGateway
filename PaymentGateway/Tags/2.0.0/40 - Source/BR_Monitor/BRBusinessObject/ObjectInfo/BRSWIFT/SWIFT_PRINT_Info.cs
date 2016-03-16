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

namespace BR.BRBusinessObject
{
    public class SWIFTPRINTInfo
    {
        private string _QUERY_ID;
        private string _STATEMENT_ID;
        private string _BRANCH_A;
        private string _MSG_TYPE;
        private string _FIELD20;
        private string _FIELD21;
        private string _FIELD32;
        private string _FIELD57;
        private string _FIELD58;
        private string _FIELD59;
        private string _BRANCH_B;
        private string _DEPARTMENT;
        private string _ERROR_CODE;
        private string _TELLER_ID;
        private string _OFFICER_ID;
        private string _STATEMENT_TIME;
        private string _RECEIVING_TIME;
        public SWIFTPRINTInfo()
		{
			
		}
        public string QUERY_ID
        {
            get
            {
                return _QUERY_ID;
            }
            set
            {
                _QUERY_ID = value;
            }
        }
        public string STATEMENT_ID
        {
            get
            {
                return _STATEMENT_ID;
            }
            set
            {
                _STATEMENT_ID = value;
            }
        }
        public string BRANCH_A
        {
            get
            {
                return _BRANCH_A;
            }
            set
            {
                _BRANCH_A = value;
            }
        }
        public string MSG_TYPE
        {
            get
            {
                return _MSG_TYPE;
            }
            set
            {
                _MSG_TYPE = value;
            }
        }
        public string FIELD20
        {
            get
            {
                return _FIELD20;
            }
            set
            {
                _FIELD20 = value;
            }
        }
        public string FIELD21
        {
            get
            {
                return _FIELD21;
            }
            set
            {
                _FIELD21 = value;
            }
        }
        public string FIELD32
        {
            get
            {
                return _FIELD32;
            }
            set
            {
                _FIELD32 = value;
            }
        }
        public string FIELD57
        {
            get
            {
                return _FIELD57;
            }
            set
            {
                _FIELD57 = value;
            }
        }
        public string FIELD58
        {
            get
            {
                return _FIELD58;
            }
            set
            {
                _FIELD58 = value;
            }
        }
        public string FIELD59
        {
            get
            {
                return _FIELD59;
            }
            set
            {
                _FIELD59 = value;
            }
        }
        public string BRANCH_B
        {
            get
            {
                return _BRANCH_B;
            }
            set
            {
                _BRANCH_B = value;
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
        public string ERROR_CODE
        {
            get
            {
                return _ERROR_CODE;
            }
            set
            {
                _ERROR_CODE = value;
            }
        }
        public string TELLER_ID
        {
            get
            {
                return _TELLER_ID;
            }
            set
            {
                _TELLER_ID = value;
            }
        }
        public string OFFICER_ID
        {
            get
            {
                return _OFFICER_ID;
            }
            set
            {
                _OFFICER_ID = value;
            }
        }
        public string STATEMENT_TIME
        {
            get
            {
                return _STATEMENT_TIME;
            }
            set
            {
                _STATEMENT_TIME = value;
            }
        }
        public string RECEIVING_TIME
        {
            get
            {
                return _RECEIVING_TIME;
            }
            set
            {
                _RECEIVING_TIME = value;
            }
        }
    }
}
