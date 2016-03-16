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
//DatHM add 09/07/2008
//Lam viec voi bang du lieu group_report 
namespace BR.BRBusinessObject
{
    public class RPTRULEInfo
    {
        private int _GROUPID;
        private string _RPTNAME;
        private int _ISVIEW;
        private int _ISREFRESH;
        private int _ISPRINT;
        private int _ISEXPORT;
        public RPTRULEInfo()
		{
			
		}

        public int GROUPID
		{
			get
			{
                return _GROUPID;
			}
			set
			{
                _GROUPID = value;
			}
		}
        public string RPTNAME
        {
            get
            {
                return _RPTNAME;
            }
            set
            {
                _RPTNAME = value;
            }
        }
        public int ISVIEW
        {
            get
            {
                return _ISVIEW;
            }
            set
            {
                _ISVIEW = value;
            }
        }
        public int ISREFRESH
        {
            get
            {
                return _ISREFRESH;
            }
            set
            {
                _ISREFRESH = value;
            }
        }
        public int ISPRINT
        {
            get
            {
                return _ISPRINT;
            }
            set
            {
                _ISPRINT = value;
            }
        }
        public int ISEXPORT
        {
            get
            {
                return _ISEXPORT;
            }
            set
            {
                _ISEXPORT = value;
            }
        }
    }
}
