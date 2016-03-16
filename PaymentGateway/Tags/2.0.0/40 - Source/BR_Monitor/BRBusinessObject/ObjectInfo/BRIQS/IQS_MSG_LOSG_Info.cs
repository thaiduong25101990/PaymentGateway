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
    public class IQS_MSG_LOSGInfo
    {
        private int _LOG_ID;
        private DateTime _LOG_DATE;
        private int _QUERY_ID;
        private int _STATUS;
        private string _DESCRIPTIONS;
        private string _JOB_NAME;
        private string _MSG_DIRECTION;
        public IQS_MSG_LOSGInfo()
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

        public int QUERY_ID
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
        public string DESCRIPTIONS
        {
            get
            {
                return _DESCRIPTIONS;
            }
            set
            {
                _DESCRIPTIONS = value;
            }
        }
        public string JOB_NAME
        {
            get
            {
                return _JOB_NAME;
            }
            set
            {
                _JOB_NAME = value;
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
        
    }
}
