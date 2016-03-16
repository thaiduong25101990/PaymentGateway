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
    public class IBPS_FEEInfo
    {
        private int _ID;
        private string _TRANS_TYPE;
        private string _FEEDISC_TYPE;
        private DateTime _FEEDISC_TIME;
        private double _FIXED_FEE;
        private double _RATE_FEE;
        private double _MIN_FEE;
        private double _MAX_FEE;
        private string _CCYCD;

        public IBPS_FEEInfo()
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
        public string TRANS_TYPE
        {
            get
            {
                return _TRANS_TYPE;
            }
            set
            {
                _TRANS_TYPE = value;
            }
        }
        public string FEEDISC_TYPE
        {
            get
            {
                return _FEEDISC_TYPE;
            }
            set
            {
                _FEEDISC_TYPE = value;
            }
        }
        public DateTime FEEDISC_TIME
        {
            get
            {
                return _FEEDISC_TIME;
            }
            set
            {
                _FEEDISC_TIME = value;
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
