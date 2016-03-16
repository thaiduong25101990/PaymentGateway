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
    public class SWIFT_MSG_CONTENTInfo
    {
        private double _AMOUNT;
        private string _CCYCD;
        private string _FOREIGN_BANK;
        private string _FOREIGN_BANK_NAME;
        private int _PRIORITY;
        private string _DELIVER_TYPE;
        private string _CONTENT;
        private string _CONTENT_ORIGIN;
        private static string _DEPARTMENT;
        private string _AUTO;
        private string _STATUS;
        private string _SWMSTS;
        private string _PSWMSTS;
        private int _ERR_CODE;
        private DateTime _RECEIVING_TIME;
        private DateTime _SENDING_TIME;
        private int _PRINTED_NO;
        private string _SESSION_NO;
        private string _OSN;
        private string _TRANS_NO;
        private string _MSG_NO;
        private string _SEQ_NO;
        private string _NAK_CONTENT;
        private static string _TELLER_ID;
        private static string _OFFICER_ID;
        private string _FILE_NAME;
        private long _MSG_ID;
        private long _QUERY_ID;
        private string _MSG_TYPE;
        private string _MSG_DIRECTION;
        private string _BRANCH_A;
        private static string _BRANCH_B;
        private DateTime _TRANS_DATE;
        private DateTime _VALUE_DATE;
        private string _FIELD20;
        private string _FIELD21;
        private string _PRE_BRANCH;
        private string _PRE_DEPT;
        private string _PROCESSSTS;
        private string _PRE_PROCESSSTS;
        private string _LOCKSTS;
        private string _LOCK_TELLERID;

        //-------------------------
        private static string _Reject;
        private static string _Approve;
        private static string _OK;
        private static string _AUTO1;
        private static string _STATUS1;
        private static string _SWMSTS1;
        private static string _PSWMSTS1;
        private static string _PRE_BRANCH1;
        private static string _PRE_DEPT1;
        private static string _PACKAGE;
        private static string _Table_Name;
        private int _PRINT_STS;

        private int _RESEND_NUM;
        

        public SWIFT_MSG_CONTENTInfo()
        {

        }

        public double AMOUNT
        {
            get { return _AMOUNT; }
            set { _AMOUNT = value; }
        }

        public string CCYCD
        {
            get { return _CCYCD; }
            set { _CCYCD = value; }
        }

        public string FOREIGN_BANK
        {
            get { return _FOREIGN_BANK; }
            set { _FOREIGN_BANK = value; }
        }

        public string FOREIGN_BANK_NAME
        {
            get { return _FOREIGN_BANK_NAME; }
            set { _FOREIGN_BANK_NAME = value; }
        }

        public int PRIORITY
        {
            get { return _PRIORITY; }
            set { _PRIORITY = value; }
        }

        public string DELIVER_TYPE
        {
            get { return _DELIVER_TYPE; }
            set { _DELIVER_TYPE = value; }
        }

        public string CONTENT
        {
            get { return _CONTENT; }
            set { _CONTENT = value; }
        }

        public string CONTENT_ORIGIN
        {
            get { return _CONTENT_ORIGIN; }
            set { _CONTENT_ORIGIN = value; }
        }

        public string DEPARTMENT
        {
            get { return _DEPARTMENT; }
            set { _DEPARTMENT = value; }
        }

        public string AUTO
        {
            get { return _AUTO; }
            set { _AUTO = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string SWMSTS
        {
            get { return _SWMSTS; }
            set { _SWMSTS = value; }
        }

        public string PSWMSTS
        {
            get { return _PSWMSTS; }
            set { _PSWMSTS = value; }
        }

        public int ERR_CODE
        {
            get { return _ERR_CODE; }
            set { _ERR_CODE = value; }
        }

        public DateTime RECEIVING_TIME
        {
            get { return _RECEIVING_TIME; }
            set { _RECEIVING_TIME = value; }
        }

        public DateTime SENDING_TIME
        {
            get { return _SENDING_TIME; }
            set { _SENDING_TIME = value; }
        }

        public int PRINTED_NO
        {
            get { return _PRINTED_NO; }
            set { _PRINTED_NO = value; }
        }

        public string SESSION_NO
        {
            get { return _SESSION_NO; }
            set { _SESSION_NO = value; }
        }

        public string OSN
        {
            get { return _OSN; }
            set { _OSN = value; }
        }

        public string TRANS_NO
        {
            get { return _TRANS_NO; }
            set { _TRANS_NO = value; }
        }

        public string MSG_NO
        {
            get { return _MSG_NO; }
            set { _MSG_NO = value; }
        }

        public string SEQ_NO
        {
            get { return _SEQ_NO; }
            set { _SEQ_NO = value; }
        }

        public string NAK_CONTENT
        {
            get { return _NAK_CONTENT; }
            set { _NAK_CONTENT = value; }
        }

        public string TELLER_ID
        {
            get { return _TELLER_ID; }
            set { _TELLER_ID = value; }
        }

        public string OFFICER_ID
        {
            get { return _OFFICER_ID; }
            set { _OFFICER_ID = value; }
        }

        public string FILE_NAME
        {
            get { return _FILE_NAME; }
            set { _FILE_NAME = value; }
        }

        public long MSG_ID
        {
            get { return _MSG_ID; }
            set { _MSG_ID = value; }
        }

        public long QUERY_ID
        {
            get { return _QUERY_ID; }
            set { _QUERY_ID = value; }
        }

        public string MSG_TYPE
        {
            get { return _MSG_TYPE; }
            set { _MSG_TYPE = value; }
        }

        public string MSG_DIRECTION
        {
            get { return _MSG_DIRECTION; }
            set { _MSG_DIRECTION = value; }
        }

        public string BRANCH_A
        {
            get { return _BRANCH_A; }
            set { _BRANCH_A = value; }
        }

        public string BRANCH_B
        {
            get { return _BRANCH_B; }
            set { _BRANCH_B = value; }
        }

        public DateTime TRANS_DATE
        {
            get { return _TRANS_DATE; }
            set { _TRANS_DATE = value; }
        }

        public DateTime VALUE_DATE
        {
            get { return _VALUE_DATE; }
            set { _VALUE_DATE = value; }
        }

        public string FIELD20
        {
            get { return _FIELD20; }
            set { _FIELD20 = value; }
        }

        public string FIELD21
        {
            get { return _FIELD21; }
            set { _FIELD21 = value; }
        }
        public  string Reject
        {
            get { return _Reject; }
            set { _Reject = value; }
        }
        public  string Approve
        {
            get { return _Approve; }
            set { _Approve = value; }
        }
        public  string OK
        {
            get { return _OK; }
            set { _OK = value; }
        }
       
        public string AUTO1
        {
            get { return _AUTO1; }
            set { _AUTO1 = value; }
        }
        public string STATUS1
        {
            get { return _STATUS1; }
            set { _STATUS1 = value; }
        }
        public string SWMSTS1
        {
            get { return _SWMSTS1; }
            set { _SWMSTS1 = value; }
        }
        public string PSWMSTS1
        {
            get { return _PSWMSTS1; }
            set { _PSWMSTS1 = value; }
        }//PRE_BRANCH
        public string PRE_BRANCH
        {
            get { return _PRE_BRANCH; }
            set { _PRE_BRANCH = value; }
        }
        public string PRE_DEPT
        {
            get { return _PRE_DEPT; }
            set { _PRE_DEPT = value; }
        }
        public string PRE_BRANCH1
        {
            get { return _PRE_BRANCH1; }
            set { _PRE_BRANCH1 = value; }
        }
        public string PRE_DEPT1
        {
            get { return _PRE_DEPT1; }
            set { _PRE_DEPT1 = value; }
        }
        
        public string PACKAGE
        {
            get { return _PACKAGE; }
            set { _PACKAGE = value; }
        }
        public string PROCESSSTS
        {
            get { return _PROCESSSTS; }
            set { _PROCESSSTS = value; }
        }//_Table_Name
        public string PRE_PROCESSSTS
        {
            get { return _PRE_PROCESSSTS; }
            set { _PRE_PROCESSSTS = value; }
        }
        public string Table_Name
        {
            get { return _Table_Name; }
            set { _Table_Name = value; }
        }
        public string LOCKSTS
        {
            get { return _LOCKSTS; }
            set { _LOCKSTS = value; }
        }
        public string LOCK_TELLERID
        {
            get { return _LOCK_TELLERID; }
            set { _LOCK_TELLERID = value; }
        }

        public int RESEND_NUM
        {
            get { return _RESEND_NUM; }
            set { _RESEND_NUM = value; }
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
