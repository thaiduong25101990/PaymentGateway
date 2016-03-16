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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:39
//' Description: Bang luu cac dien bi lech khi doi chieu kenh IBPS
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_MSG_REC_SAAInfo
    {
        private string _UMID;
        private string _TRANS_DATE;
        private string _VALUE_DATE;
        private string _CCY;
        private string _AMOUNT;
        private string _FORMAT;
        private string _STATUS;
        private string _SUFFIX;
        private string _RECEPTION_INFO;
        private string _EMISSION_INFO;
        private string _NETW_STATUS;
        private string _ORIG_INST_RP;
        private string _CREATION_DATE;
        private string _SENDER;
        private string _RECEIVER;
        private string _LOCATION;
        private string _MSG_DIRECTION;
        private string _REF_NO;



        public SWIFT_MSG_REC_SAAInfo()
        {
        }

        public string UMID
        {
            get
            {
                return _UMID;
            }
            set
            {
                _UMID = value;
            }
        }

        public string TRANS_DATE
        {
            get
            {
                return _TRANS_DATE;
            }
            set
            {
                _TRANS_DATE = value;
            }
        }

        public string VALUE_DATE
        {
            get
            {
                return _VALUE_DATE;
            }
            set
            {
                _VALUE_DATE = value;
            }
        }

        public string CCY
        {
            get
            {
                return _CCY;
            }
            set
            {
                _CCY = value;
            }
        }

        public string AMOUNT
        {
            get
            {
                return _AMOUNT;
            }
            set
            {
                _AMOUNT = value;
            }
        }

        public string FORMAT
        {
            get
            {
                return _FORMAT;
            }
            set
            {
                _FORMAT = value;
            }
        }

        public string STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                _STATUS = value;
            }
        }

        public string SUFFIX
        {
            get
            {
                return _SUFFIX;
            }
            set
            {
                _SUFFIX = value;
            }
        }

        public string RECEPTION_INFO
        {
            get
            {
                return _RECEPTION_INFO;
            }
            set
            {
                _RECEPTION_INFO = value;
            }
        }

        public string EMISSION_INFO
        {
            get
            {
                return _EMISSION_INFO;
            }
            set
            {
                _EMISSION_INFO = value;
            }
        }


        public string NETW_STATUS
        {
            get
            {
                return _NETW_STATUS;
            }
            set
            {
                _NETW_STATUS = value;
            }
        }

        public string ORIG_INST_RP
        {
            get
            {
                return _ORIG_INST_RP;
            }
            set
            {
                _ORIG_INST_RP = value;
            }
        }

        public string CREATION_DATE
        {
            get
            {
                return _CREATION_DATE;
            }
            set
            {
                _CREATION_DATE = value;
            }
        }

        public string SENDER
        {
            get
            {
                return _SENDER;
            }
            set
            {
                _SENDER = value;
            }
        }

        public string RECEIVER
        {
            get
            {
                return _RECEIVER;
            }
            set
            {
                _RECEIVER = value;
            }
        }

        public string LOCATION
        {
            get
            {
                return _LOCATION;
            }
            set
            {
                _LOCATION = value;
            }
        }

        public string MSG_DIRECTION
        {
            get
            {
                return _MSG_DIRECTION;
            }
            set
            {
                _MSG_DIRECTION = value;
            }
        }

        public string REF_NO
        {
            get
            {
                return _REF_NO;
            }
            set
            {
                _REF_NO = value;
            }
        }
    }
}