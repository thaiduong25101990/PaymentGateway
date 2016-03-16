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
    public class IBPS_MSG_REC_TADInfo
    {
        private string _REF_NO;
        private string _TRANS_DATE;
        private string _CCY;
        private string _AMOUNT;
        private string _SENDER;
        private string _RECEIVER;
        private string _STATUS;

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
    }
}