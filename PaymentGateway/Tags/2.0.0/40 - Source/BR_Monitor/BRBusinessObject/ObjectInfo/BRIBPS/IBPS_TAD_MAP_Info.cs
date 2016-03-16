using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject.ObjectInfo.BRIBPS
{
    public class IBPS_TAD_MAP_Info
    {        
        private string _BANK_CODE;
        private string _TAD_CODE;
        private DateTime _UPDATETIME;
        private string _NOTE;
        private string _BANK_NAME;
        private string _GW_BANK_CODE;

        public IBPS_TAD_MAP_Info()
        { }
               
        public string BANK_CODE
        {
            get { return _BANK_CODE; }
            set { _BANK_CODE = value; }
        }

        public string TAD_CODE
        {
            get { return _TAD_CODE; }
            set { _TAD_CODE = value; }
        }

        public DateTime UPDATETIME
        {
            get { return _UPDATETIME; }
            set { _UPDATETIME = value; }
        }

        public string NOTE
        {
            get { return _NOTE; }
            set { _NOTE = value; }
        }

        public string BANK_NAME
        {
            get { return _BANK_NAME; }
            set { _BANK_NAME = value; }
        }

        public string GW_BANK_CODE
        {
            get { return _GW_BANK_CODE; }
            set { _GW_BANK_CODE = value; }
        }
    }
}
