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

namespace BIDVWEB.Business
{
    public class SYSPARA_Info
    {
        private long _ID;
        private string _GWTYPE;
        private string _VARNAME;
        private string _VALUE;
        private string _TYPE;
        private string _NOTE;

        public SYSPARA_Info()
		{
			
		}
		
		public long  ID
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

        public string VARNAME
		{
			get
			{
                return _VARNAME;
			}
			set
			{
                _VARNAME = value;
			}
		}

        public string VALUE
		{
			get
			{
                return _VALUE;
			}
			set
			{
                _VALUE = value;
			}
		}

        public string TYPE
        {
            get
            {
                return _TYPE;
            }
            set
            {
                _TYPE = value;
            }
        }
        
        public string NOTE
        {
            get
            {
                return _NOTE;
            }
            set
            {
                _NOTE = value;
            }
        }

    }
}
