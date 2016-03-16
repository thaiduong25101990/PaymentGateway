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
    public class SWIFT_FEE_Info
    {
        private long _ID;
        private double _FIXED_FEE;
        private double _RATE_FEE;
        private double _MIN_FEE;
        private double _MAX_FEE;
        private string _MSG_TYPE;
        private string _CCYCD;

        public SWIFT_FEE_Info()
        {

        }

        public long ID
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

        public double FIXED_FEE
        {
            get
            {
                return _FIXED_FEE;
            }
            set
            {
                _FIXED_FEE = value;
            }
        }

        public double RATE_FEE
        {
            get
            {
                return _RATE_FEE;
            }
            set
            {
                _RATE_FEE = value;
            }
        }
        public double MIN_FEE
        {
            get
            {
                return _MIN_FEE;
            }
            set
            {
                _MIN_FEE = value;
            }
        }

        public double MAX_FEE
        {
            get
            {
                return _MAX_FEE;
            }
            set
            {
                _MAX_FEE = value;
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
    }
}
