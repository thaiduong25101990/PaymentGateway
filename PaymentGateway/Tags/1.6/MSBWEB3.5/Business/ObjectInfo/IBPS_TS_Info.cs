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
    public class IBPS_TS_Info
    {
        private long  _ID;
        private DateTime _CREATEDATE;
        private long _QUERY_ID;        
        private string _BR_SEND;
        private string _BR_RECEIVE;
        private string _CONTENT_TS;
        private string _CONTENT;
        private long _ID_PARENT;
        private int _IORDER;
        private int _STATUS;
        private string _MSG_DIRECTION;
        private string _REFNO;
        private DateTime _UPDATEDATE;
        private string _KSV_SEND;
        private string _KSV_RECEIVE;
        private int _STSAPP;
        private string _SBT_ID;
        private string _BR_SEND_8;
        private string _BR_RECEIVE_8;

        public IBPS_TS_Info()
		{
			
		}
        public long ID
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

        public DateTime CREATEDATE
        {
            get
            {
                return _CREATEDATE;
            }
            set
            {
                _CREATEDATE = value;
            }
        }

        public long QUERY_ID
        {
            get
            {
                return _QUERY_ID;
            }
            set
            {
                _QUERY_ID = value;
            }
        }
                
        public string BR_SEND
        {
            get
            {
                return _BR_SEND;
            }
            set
            {
                _BR_SEND = value;
            }
        }

        public string BR_RECEIVE
        {
            get
            {
                return _BR_RECEIVE;
            }
            set
            {
                _BR_RECEIVE = value;
            }
        }

        public string CONTENT_TS
        {
            get
            {
                return _CONTENT_TS;
            }
            set
            {
                _CONTENT_TS = value;
            }
        }

        public string CONTENT
        {
            get
            {
                return _CONTENT;
            }
            set
            {
                _CONTENT = value;
            }
        }

        public long ID_PARENT
        {
            get
            {
                return _ID_PARENT;
            }
            set
            {
                _ID_PARENT = value;
            }
        }

        public int IORDER
        {
            get
            {
                return _IORDER;
            }
            set
            {
                _IORDER = value;
            }
        }

        public int STATUS
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

        public string REFNO
        {
            get
            {
                return _REFNO;
            }
            set
            {
                _REFNO = value;
            }
        }

        public DateTime UPDATEDATE
        {
            get
            {
                return _UPDATEDATE;
            }
            set
            {
                _UPDATEDATE = value;
            }
        }

        public string KSV_SEND
        {
            get
            {
                return _KSV_SEND;
            }
            set
            {
                _KSV_SEND = value;
            }
        }

        public string KSV_RECEIVE
        {
            get
            {
                return _KSV_RECEIVE;
            }
            set
            {
                _KSV_RECEIVE = value;
            }
        }

        public int STSAPP
        {
            get
            {
                return _STSAPP;
            }
            set
            {
                _STSAPP = value;
            }
        }

        public string SBT_ID
        {
            get
            {
                return _SBT_ID;
            }
            set
            {
                _SBT_ID = value;
            }
        }

        public string BR_SEND_8
        {
            get
            {
                return _BR_SEND_8;
            }
            set
            {
                _BR_SEND_8 = value;
            }
        }

        public string BR_RECEIVE_8
        {
            get
            {
                return _BR_RECEIVE_8;
            }
            set
            {
                _BR_RECEIVE_8 = value;
            }
        }
    }
}
