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
    public class AREAInfo
    {
        private int _ID;
        private string _PROV_CODE;
        private string  _SHORTNAME;
        private string _FULLNAME;        
        private string _CITAD_MEMBER;
        public AREAInfo()
        {

        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string PROV_CODE
        {
            get { return _PROV_CODE; }
            set { _PROV_CODE = value; }
        }

        public string SHORTNAME
        {
            get { return _SHORTNAME; }
            set { _SHORTNAME = value; }
        }

        public string FULLNAME
        {
            get { return _FULLNAME; }
            set { _FULLNAME = value; }
        }

        public string CITAD_MEMBER
        {
            get { return _CITAD_MEMBER; }
            set { _CITAD_MEMBER = value; }
        }

    }
}
