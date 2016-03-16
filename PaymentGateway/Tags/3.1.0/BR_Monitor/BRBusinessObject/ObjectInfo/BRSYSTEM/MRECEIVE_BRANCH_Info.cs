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
    public class MRECEIVE_BRANCHInfo
    {
        private int _MSG_ID;
        private string _TRAN_NO;
        private int _PRIORITY;
        private DateTime _TRAN_DATE;
        private string _BRAN_A;
        private string _BRAN_B;
        private string _MSGTEXT;
        private string _MSGTEXT1;
        private string _MSGTEXT2;
        private string _MSGTEXT3;
        private string _TEXT_NUM;
        private string _TRANSMITTED;
        private string _TRANSMIT_TIME;
        private string _CREATE_TIME;
        private string _ERROR;
        private string _DESCRIPTION;
        private string _DIRECTION;
        private string _MSG_CLOB;
        private string _BRAN_ORIGIN;

        public MRECEIVE_BRANCHInfo()
        {

        }
        public int MSG_ID
        {
            get
            {
                return _MSG_ID;
            }
            set
            {
                _MSG_ID = value;
            }
        }
        public string TRAN_NO
        {
            get
            {
                return TRAN_NO;
            }
            set
            {
                _TRAN_NO = value;
            }
        }
        public int PRIORITY
        {
            get
            {
                return _PRIORITY;
            }
            set
            {
                _PRIORITY = value;
            }
        }
        public DateTime TRAN_DATE
        {
            get
            {
                return _TRAN_DATE;
            }
            set
            {
                _TRAN_DATE = value;
            }
        }
        public string BRAN_A
        {
            get
            {
                return _BRAN_A;
            }
            set
            {
                _BRAN_A = value;
            }
        }
        public string BRAN_B
        {
            get
            {
                return _BRAN_B;
            }
            set
            {
                _BRAN_B = value;
            }
        }
        public string MSGTEXT
        {
            get
            {
                return _MSGTEXT;
            }
            set
            {
                _MSGTEXT = value;
            }
        }
        public string MSGTEXT1
        {
            get
            {
                return MSGTEXT1;
            }
            set
            {
                _MSGTEXT1 = value;
            }
        }
        public string MSGTEXT2
        {
            get
            {
                return _MSGTEXT2;
            }
            set
            {
                _MSGTEXT2 = value;
            }
        }
        public string MSGTEXT3
        {
            get
            {
                return _MSGTEXT3;
            }
            set
            {
                _MSGTEXT3 = value;
            }
        }
        public string TEXT_NUM
        {
            get
            {
                return _TEXT_NUM;
            }
            set
            {
                _TEXT_NUM = value;
            }
        }
        public string TRANSMITTED
        {
            get
            {
                return _TRANSMITTED;
            }
            set
            {
                _TRANSMITTED = value;
            }
        }
        public string TRANSMIT_TIME
        {
            get
            {
                return _TRANSMIT_TIME;
            }
            set
            {
                _TRANSMIT_TIME = value;
            }
        }
        public string CREATE_TIME
        {
            get
            {
                return _CREATE_TIME;
            }
            set
            {
                _CREATE_TIME = value;
            }
        }

        public string ERROR
        {
            get
            {
                return _ERROR;
            }
            set
            {
                _ERROR = value;
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
        public string DIRECTION
        {
            get
            {
                return _DIRECTION;
            }
            set
            {
                _DIRECTION = value;
            }
        }
        public string MSG_CLOB
        {
            get
            {
                return _MSG_CLOB;
            }
            set
            {
                _MSG_CLOB = value;
            }
        }
        public string BRAN_ORIGIN
        {
            get
            {
                return _BRAN_ORIGIN;
            }
            set
            {
                _BRAN_ORIGIN = value;
            }
        }

    }
}
