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
//' Create date:	18/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================


namespace BR.BRBusinessObject
{
    public class IBPS_MSG_ALLInfo
    {
        private string _RM_NUMBER;
        private int _MSG_ID;
        private int _QUERY_ID;
        private string _FILE_NAME;
        private string _MSG_DIRECTION;
        private string _TRANS_CODE;
        private int _GW_TRANS_NUM;
        private int _SIBS_TRANS_NUM;
        private string _SENDER;
        private string _RECEIVER;
        private DateTime _TRANS_DATE;
        private int _AMOUNT;
        private string _CCYCD;
        private int _STATUS;
        private int _ERR_CODE;
        private string _TRANS_DESCRIPTION;
        private string _DEPARTMENT;
        private string _CONTENT;
        private string _SOURCE_BRANCH;
        private int _TAD;
        private int _PRE_TAD;

        public IBPS_MSG_ALLInfo()
        {

        }

        public string RM_NUMBER
        {
            get { return _RM_NUMBER; }
            set { _RM_NUMBER = value; }
        }

        public int MSG_ID
        {
            get { return _MSG_ID; }
            set { _MSG_ID = value; }
        }

        public int QUERY_ID
        {
            get { return _QUERY_ID; }
            set { _QUERY_ID = value; }
        }

        public string FILE_NAME
        {
            get { return _FILE_NAME; }
            set { _FILE_NAME = value; }
        }

        public string MSG_DIRECTION
        {
            get { return _MSG_DIRECTION; }
            set { _MSG_DIRECTION = value; }
        }

        public string TRANS_CODE
        {
            get { return _TRANS_CODE; }
            set { _TRANS_CODE = value; }
        }

        public int GW_TRANS_NUM
        {
            get { return _GW_TRANS_NUM; }
            set { _GW_TRANS_NUM = value; }
        }

        public int SIBS_TRANS_NUM
        {
            get { return _SIBS_TRANS_NUM; }
            set { _SIBS_TRANS_NUM = value; }
        }

        public string SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public string RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public DateTime TRANS_DATE
        {
            get { return _TRANS_DATE; }
            set { _TRANS_DATE = value; }
        }

        public int AMOUNT
        {
            get { return _AMOUNT; }
            set { _AMOUNT = value; }
        }

        public string CCYCD
        {
            get { return _CCYCD; }
            set { _CCYCD = value; }
        }

        public int STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public int ERR_CODE
        {
            get { return _ERR_CODE; }
            set { _ERR_CODE = value; }
        }

        public string TRANS_DESCRIPTION
        {
            get { return _TRANS_DESCRIPTION; }
            set { _TRANS_DESCRIPTION = value; }
        }

        public string DEPARTMENT
        {
            get { return _DEPARTMENT; }
            set { _DEPARTMENT = value; }
        }

        public string CONTENT
        {
            get { return _CONTENT; }
            set { _CONTENT = value; }
        }

        public string SOURCE_BRANCH
        {
            get { return _SOURCE_BRANCH; }
            set { _SOURCE_BRANCH = value; }
        }

        public int TAD
        {
            get { return _TAD; }
            set { _TAD = value; }
        }

        public int PRE_TAD
        {
            get { return _PRE_TAD; }
            set { _PRE_TAD = value; }
        }

    } 


}
