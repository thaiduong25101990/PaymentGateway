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
    public class USER_MSG_LOG_Info
    {
        private int _LOGID;
        private DateTime _LOG_DATE;
        private string _USERID;
        private string _CONTENT;
        private int _STATUS;
        private string _WORKED;
        private string _TABLE_ACCESS;
        private string _OLD_VALUE;
        private string _NEW_VALUE;

        public USER_MSG_LOG_Info()
		{			
		}
        public int LOGID
        {
            get
            {
                return _LOGID;
            }
            set
            {
                _LOGID = value;
            }
        }
        public DateTime LOG_DATE
        {
            get
            {
                return _LOG_DATE;
            }
            set
            {
                _LOG_DATE = value;
            }
        }
        public string USERID
        {
            get
            {
                return _USERID;
            }
            set
            {
                _USERID = value;
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
        public string WORKED
        {
            get
            {
                return _WORKED;
            }
            set
            {
                _WORKED = value;
            }
        }
        public string TABLE_ACCESS
        {
            get
            {
                return _TABLE_ACCESS;
            }
            set
            {
                _TABLE_ACCESS = value;
            }
        }
        public string OLD_VALUE
        {
            get
            {
                return _OLD_VALUE;
            }
            set
            {
                _OLD_VALUE = value;
            }
        }
        public string NEW_VALUE
        {
            get
            {
                return _NEW_VALUE;
            }
            set
            {
                _NEW_VALUE = value;
            }
        }
    }
}
