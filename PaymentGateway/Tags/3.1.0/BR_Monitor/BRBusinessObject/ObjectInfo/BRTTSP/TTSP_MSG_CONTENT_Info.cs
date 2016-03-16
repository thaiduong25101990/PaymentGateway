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
//' Author:	Nguyen duc quy
//' Create date:	11/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class TTSP_MSG_CONTENTInfo
    {
        private int _MSG_ID;
        private int _QUERY_ID;
        private string _MSG_TYPE;
        private string _MSG_DIRECTION;
        private string _SENDER;
        private string _RECEIVER;
        private DateTime _TRAN_DATE;
        private DateTime _VALUE_DATE;
        private string _FIELD20;
        private string _FIELD21;
        private int _AMOUNT;
        private string _CCYCD;
        private int _STATUS;
        private int _ERR_CODE;
        private string _DEPARTMENT;
        private string _HEAD_CONTENT;
        private string _CONTENT;

        public TTSP_MSG_CONTENTInfo()
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
        
        public DateTime VALUE_DATE
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
        
        public string FIELD20
        {
            get
            {
                return _FIELD20;
            }
            set
            {
                _FIELD20 = value;
            }
        }
       
        public string FIELD21
        {
            get
            {
                return _FIELD21;
            }
            set
            {
                _FIELD21 = value;
            }
        }
        
        public int AMOUNT
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
        
        public string CCYCD
        {
            get
            {
                return _CCYCD;
            }
            set
            {
                _CCYCD = value;
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
        
        public int ERR_CODE
        {
            get
            {
                return _ERR_CODE;
            }
            set
            {
                _ERR_CODE = value;
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
        
        public string HEAD_CONTENT
        {
            get
            {
                return _HEAD_CONTENT;
            }
            set
            {
                _HEAD_CONTENT = value;
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
    }
}
