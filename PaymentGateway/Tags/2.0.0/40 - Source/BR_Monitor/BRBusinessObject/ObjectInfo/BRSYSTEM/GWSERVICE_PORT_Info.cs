using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
    public class GWSERVICE_PORTInfo
    {
        private int _ID;
        private string _SERVICENAME;
        private string _SIBSIP;
        private string _PORTIN;
        private string _PORTOUT;
        private string _FILETYPE;
        private int _TIMEDELAY;
        private string _DESCRIPTION;

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

        public string SERVICENAME
        {
            get
            {
                return _SERVICENAME;
            }
            set
            {
                _SERVICENAME = value;
            }
        }

        public string SIBSIP
        {
            get
            {
                return _SIBSIP;
            }
            set
            {
                _SIBSIP = value;
            }
        }

        public string PORTIN
        {
            get
            {
                return _PORTIN;
            }
            set
            {
                _PORTIN = value;
            }
        }

        public string PORTOUT
        {
            get
            {
                return _PORTOUT;
            }
            set
            {
                _PORTOUT = value;
            }
        }

        public string FILETYPE
        {
            get
            {
                return _FILETYPE;
            }
            set
            {
                _FILETYPE = value;
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
        public int TIMEDELAY
        {
            get
            {
                return _TIMEDELAY;
            }
            set
            {
                _TIMEDELAY = value;
            }
        }

    }
}
