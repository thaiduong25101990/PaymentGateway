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
/*=============================================
 Author:
 Create date:	11/04/2010
 Description:
 Revise History:
 =============================================*/
namespace BIDVWEB.Business
{
    public class VCB_MSG_CONTENTInfo
    {

        private long _MSG_ID;
        private long _QUERY_ID;
        private string _MSG_TYPE;
        private string _MSG_DIRECTION;
        private string _BRANCH_A;
        private string _BRANCH_B;
        private DateTime _TRANS_DATE;
        private DateTime _VALUE_DATE;
        private string _FIELD20;
        private string _FIELD21;
        private int _AMOUNT;
        private string _CCYCD;
        private int _STATUS;
        private int _ERR_CODE;
        private string _DEPARTMENT;
        private string _HEADER_CONTENT;
        private string _CONTENT;
        private string _FILE_NAME;
        private int _PRIORITY;
        private string _FOREIGN_BANK;
        private string _FOREIGN_BANK_NAME;
        private string _TRANS_NO;
        private int _PRINT_STS;

        public VCB_MSG_CONTENTInfo()
        {

        }

        public long MSG_ID
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
        
        public string BRANCH_A
        {
            get
            {
                return _BRANCH_A;
            }
            set
            {
                _BRANCH_A = value;
            }
        }
        
        public string BRANCH_B
        {
            get
            {
                return _BRANCH_B;
            }
            set
            {
                _BRANCH_B = value;
            }
        }
       
        public DateTime TRANS_DATE
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
        
        public string HEADER_CONTENT
        {
            get
            {
                return _HEADER_CONTENT;
            }
            set
            {
                _HEADER_CONTENT = value;
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
        
        public string FILE_NAME
        {
            get
            {
                return _FILE_NAME;
            }
            set
            {
                _FILE_NAME = value;
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
        
        public string FOREIGN_BANK
        {
            get
            {
                return _FOREIGN_BANK;
            }
            set
            {
                _FOREIGN_BANK = value;
            }
        }
        
        public string FOREIGN_BANK_NAME
        {
            get
            {
                return _FOREIGN_BANK_NAME;
            }
            set
            {
                _FOREIGN_BANK_NAME = value;
            }
        }
        
        public string TRANS_NO
        {
            get
            {
                return _TRANS_NO;
            }
            set
            {
                _TRANS_NO = value;
            }
        }

        public int PRINT_STS
        {
            get
            {
                return _PRINT_STS;
            }
            set
            {
                _PRINT_STS = value;
            }
        }
        
    }
}
