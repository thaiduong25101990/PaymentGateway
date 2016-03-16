using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIDVWEB.Business.UserRight
{
    class clsUserRightInfo
    {
        private int _LOG_ID;
        private DateTime _LOG_DATE;
        private string _USER_NAME;
        private string _CONTENT;
        private int _STATUS;
        private string _WORKED;
        private string _TABLE_ACCESS;
        private string _OLD_VALUE;
        private string _NEW_VALUE;

        public clsUserRightInfo()
		{			
		}
        public int LOG_ID
        {
            get
            {
                return _LOG_ID;
            }
            set
            {
                _LOG_ID = value;
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
        public string USER_NAME
        {
            get
            {
                return _USER_NAME;
            }
            set
            {
                _USER_NAME = value;
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
