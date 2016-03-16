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
    public class IBPS_FREEInfo
    {
        
        private string _TRANSTYPE;
        private string _FREETYPE;
        private DateTime _FREETIME;
        private int _HARDFREE;
        private double _PERCENTFREE;
        private int _MINFREE;
        private int _MAXFREE;
        private DateTime _FROMDATE;
        private DateTime _TODATE;

        public IBPS_FREEInfo()
        {

        }        
       
        public string TRANSTYPE
        {
            get
            {
                return _TRANSTYPE;
            }
            set
            {
                _TRANSTYPE = value;
            }
        }
        public string FREETYPE
        {
            get
            {
                return _FREETYPE;
            }
            set
            {
                _FREETYPE = value;
            }
        }
        public DateTime FREETIME
        {
            get
            {
                return _FREETIME;
            }
            set
            {
                _FREETIME = value;
            }
        }
        public int HARDFREE
        {
            get
            {
                return _HARDFREE;
            }
            set
            {
                _HARDFREE = value;
            }
        }

        public double PERCENTFREE
        {
            get
            {
                return _PERCENTFREE;
            }
            set
            {
                _PERCENTFREE = value;
            }
        }
        public int MINFREE
        {
            get
            {
                return _MINFREE;
            }
            set
            {
                _MINFREE = value;
            }
        }

        public int MAXFREE
        {
            get
            {
                return _MAXFREE;
            }
            set
            {
                _MAXFREE = value;
            }
        }

        public DateTime FROMDATE
        {
            get
            {
                return _FROMDATE;
            }
            set
            {
                _FROMDATE = value;
            }
        }
        public DateTime TODATE
        {
            get
            {
                return _TODATE;
            }
            set
            {
                _TODATE = value;
            }
        }

    }
}
