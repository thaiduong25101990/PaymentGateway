using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class BIDV_ACCOUNT_Info
    {
        private int _ID;
        private string _CCYCD;
        private string _ACCOUNT;
        private string _BRANCH;

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

        public string ACCOUNT
        {
            get
            {
                return _ACCOUNT;
            }
            set
            {
                _ACCOUNT = value;
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
            
    }
}
