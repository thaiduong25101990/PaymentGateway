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

//' ============================================= 


//' Template: InfoClass.xslt 17/10/2006 
//' Author: Le Duc Quan 
//' Create date: 19/04/2008 21:39 
//' Description: 
//' Revise History: 
//' ============================================= 
namespace  BR.BRBusinessObject
{
    public class CURRENCY_CHANNELInfo
    {
        private double _ID;
        private string _CCYCD;
        private string _SHORTCD;
        private string _CURRENCY;
        private double _DECIMALS;
        private double _STATUS;
        private string _GWTYPE;
        private string _PARTNER;

        public CURRENCY_CHANNELInfo()
        {

        }


        public double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string CCYCD
        {
            get { return _CCYCD; }
            set { _CCYCD = value; }
        }

        public string SHORTCD
        {
            get { return _SHORTCD; }
            set { _SHORTCD = value; }
        }



        public string CURRENCY
        {
            get { return _CURRENCY; }
            set { _CURRENCY = value; }
        }

        public double DECIMALS
        {
            get { return _DECIMALS; }
            set { _DECIMALS = value; }
        }

        public double STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string GWTYPE
        {
            get { return _GWTYPE; }
            set { _GWTYPE = value; }
        }

        public string PARTNER
        {
            get { return _PARTNER; }
            set { _PARTNER = value; }
        }
    }

    
} 