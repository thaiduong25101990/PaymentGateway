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
//' Author:	Nguyen duc quy
//' Create date:	06/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================

namespace BR.BRBusinessObject
{
    public class MSG_CONTENT_TEMPInfo
    {
        private string _GW_TYPE;
        private string _ACT_NUM;
        private string _ACT_TYPE;
        private string _MSG_TYPE;
        private string _MSG_NO;
        private string _SEQ_NO;
        private string _REF_NO;
        private string _TELEX_NO;
        private string _MSG_DIRECTION;
        private string _AMOUNT;
        private string _CCY;
        private string _TRANS_DATE;
        private string _SENDER;
        private string _RECEIVER;
        private string _DEPARTMENT;
        private string _MSG_ID;

        public MSG_CONTENT_TEMPInfo()
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

        public string ACT_NUM
        {
            get
            {
                return _ACT_NUM;
            }
            set
            {
                _ACT_NUM = value;
            }
        }

        public string ACT_TYPE
        {
            get
            {
                return _ACT_TYPE;
            }
            set
            {
                _ACT_TYPE = value;
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

        public string SEQ_NO
        {
            get
            {
                return _SEQ_NO;
            }
            set
            {
                _SEQ_NO = value;
            }
        }

        public string TELEX_NO
        {
            get
            {
                return _TELEX_NO;
            }
            set
            {
                _TELEX_NO = value;
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

        public string DEPARTMENT
        {
            get
            {
                return _DEPARTMENT;
            }
            set
            {
                _DEPARTMENT = value;
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

        public string MSG_ID
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

    }

}


