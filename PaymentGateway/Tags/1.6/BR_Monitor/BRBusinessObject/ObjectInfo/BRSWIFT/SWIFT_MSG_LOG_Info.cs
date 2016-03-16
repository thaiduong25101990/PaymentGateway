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
//' Create date:	11/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_MSG_LOGInfo
    {
        private int _LOG_ID;
        private DateTime _LOG_DATE;
        private int _QUERY_ID;
        private int _STATUS;
        private string _DESCRIPTIONS;
        
        public SWIFT_MSG_LOGInfo()
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
        
        
    }
}
