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
//' Description: Bang luu cac dien duoc doc tu file text do SIBS tra ve
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class IBPS_RECONCILEInfo
    {
        private string _GW_TYPE;
        private string _ACC_NUM;
        private string _ACC_TYPE;
        private string _RUNNING_SEG_NO;
        private string _REF_NO;
        private string _MSG_TYPE;
        private string _SEG_NO;
        private string _MSG_NO;
        private string _CCY;
        private string _AMOUNT;
        private string _APP_CODE;
        private string _SENDER;
        private string _ORG_BANK;
        private string _RECEIVING_BRANCH;
        private string _RECEIVER;
        private string _VALUE_DATE;
        private string _JOURNAL_SEG_NO;
        private string _MSG_DIRECTION;
        private string _FROM_SYSTEM;
        private string _TO_SYSTEM;
        private string _TRANS_DATE;
        private string _EXCEPTION_TYPE;
    

        public IBPS_RECONCILEInfo ()
        {
        }

        public string GW_TYPE
        {
            get
            {
                return _GW_TYPE;
            }
            set
            {
                _GW_TYPE = value;
            }
        }

        public string ACC_NUM
        {
            get
            {
                return _ACC_NUM;
            }
            set
            {
                _ACC_NUM = value;
            }
        }

        public string ACC_TYPE
        {
            get
            {
                return _ACC_TYPE;
            }
            set
            {
                _ACC_TYPE = value;
            }
        }

        public string RUNNING_SEG_NO
        {
            get
            {
                return _RUNNING_SEG_NO;
            }
            set
            {
                _RUNNING_SEG_NO = value;
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

        public string MSG_TYPE
        {
            get
            {
                return _MSG_TYPE;
            }
            set
            {
                _MSG_TYPE = value;
            }
        }

        public string SEG_NO
        {
            get
            {
                return _SEG_NO;
            }
            set
            {
                _SEG_NO = value;
            }
        }

        public string MSG_NO
        {
            get
            {
                return _MSG_NO;
            }
            set
            {
                _MSG_NO = value;
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


        public string APP_CODE
        {
            get
            {
                return _APP_CODE;
            }
            set
            {
                _APP_CODE = value;
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

        public string ORG_BANK
        {
            get
            {
                return _ORG_BANK;
            }
            set
            {
                _ORG_BANK = value;
            }
        }

        public string RECEIVING_BRANCH
        {
            get
            {
                return _RECEIVING_BRANCH;
            }
            set
            {
                _RECEIVING_BRANCH = value;
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

        public string JOURNAL_SEG_NO
        {
            get
            {
                return _JOURNAL_SEG_NO;
            }
            set
            {
                _JOURNAL_SEG_NO = value;
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

        public string FROM_SYSTEM
        {
            get
            {
                return _FROM_SYSTEM;
            }
            set
            {
                _FROM_SYSTEM = value;
            }
        }

        public string TO_SYSTEM
        {
            get
            {
                return _TO_SYSTEM;
            }
            set
            {
                _TO_SYSTEM = value;
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

        public string EXCEPTION_TYPE
        {
            get
            {
                return _EXCEPTION_TYPE;
            }
            set
            {
                _EXCEPTION_TYPE = value;
            }
        }

    }
    
}
