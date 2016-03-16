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
//DatHM add 04/07/2008
//Purpose : Lam viec voi du lieu bang dang ky bao cao RPTMASTER
namespace BR.BRBusinessObject
{
    public class RPTMASTERInfo
    {
        private string _RPTNAME;
        private string _GWTYPE;
        private string _DESCRIPTION;
        private string _RIGHTS;
        public RPTMASTERInfo()
		{
			
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
        public string RIGHTS
        {
            get
            {
                return _RIGHTS;
            }
            set
            {
                _RIGHTS = value;
            }
        }
    }
}
