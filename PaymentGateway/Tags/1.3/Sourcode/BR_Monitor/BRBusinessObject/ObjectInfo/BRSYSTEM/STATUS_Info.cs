using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class STATUSInfo
    {
        private int _STATUS;
        private string _GWTYPE;
        private string _NAME;
        public STATUSInfo()
        {

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
    }
}
