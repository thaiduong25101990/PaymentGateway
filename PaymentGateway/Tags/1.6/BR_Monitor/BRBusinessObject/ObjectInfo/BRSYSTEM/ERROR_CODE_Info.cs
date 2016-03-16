using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class ERROR_CODEInfo
    {
        private int _ERROR_CODE;
        private string _NAME;
        private string _GWTYPE;
        private string _DESCRIPTION;
        public ERROR_CODEInfo()
        {

        }
        public int ERROR_CODE
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
        public string NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                _NAME = value;
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
    }
}
