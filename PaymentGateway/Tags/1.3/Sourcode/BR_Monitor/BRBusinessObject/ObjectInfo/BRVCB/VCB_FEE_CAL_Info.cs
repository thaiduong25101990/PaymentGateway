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
    public class VCB_FEE_Info
    {

        private long _ID;
        private double _FIXEDFEE;
        private double _FIXEDFEE1;
        private double _RATE;
        private double _MINFEE;
        private double _MAXFEE;
        private string _CCYCD;
        private string _CCYCD1;
        
        public VCB_FEE_Info()
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
       
        public double FIXEDFEE
        {
            get
            {
                return _FIXEDFEE;
            }
            set
            {
                _FIXEDFEE = value;
            }
        }

        public double FIXEDFEE1
        {
            get
            {
                return _FIXEDFEE1;
            }
            set
            {
                _FIXEDFEE1 = value;
            }
        }

        public double RATE
        {
            get
            {
                return _RATE;
            }
            set
            {
                _RATE = value;
            }
        }
        public double MINFEE
        {
            get
            {
                return _MINFEE;
            }
            set
            {
                _MINFEE = value;
            }
        }

        public double MAXFEE
        {
            get
            {
                return _MAXFEE;
            }
            set
            {
                _MAXFEE = value;
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
        public string CCYCD1
        {
            get
            {
                return _CCYCD1;
            }
            set
            {
                _CCYCD1 = value;
            }
        }
    }
}
