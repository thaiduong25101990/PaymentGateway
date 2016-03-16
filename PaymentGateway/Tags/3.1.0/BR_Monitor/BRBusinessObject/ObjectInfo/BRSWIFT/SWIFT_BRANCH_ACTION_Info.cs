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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_BRANCH_ACTION_Info
    {

        private double _PRM_ID;
        private string _KEY_WORD;
        private string _BRANCH;
        private string _MESSAGE;
        private string _PRIORITY;
        private string _NAME;
        private string _DESCPRIPTION;
        private string _DEPARTMENT;
        public SWIFT_BRANCH_ACTION_Info()
        {

        }

        public double PRM_ID
        {
            get
            {
                return _PRM_ID;
            }
            set
            {
                _PRM_ID = value;
            }
        }

        public string KEY_WORD
        {
            get
            {
                return _KEY_WORD;
            }
            set
            {
                _KEY_WORD = value;
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
        public string BRANCH
        {
            get
            {
                return _BRANCH;
            }
            set
            {
                _BRANCH = value;
            }
        }

        public string MESSAGE
        {
            get
            {
                return _MESSAGE;
            }
            set
            {
                _MESSAGE = value;
            }
        }

        public string PRIORITY
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

        public string NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                _NAME = value;
            }
        }

        public string DESCPRIPTION
        {
            get
            {
                return _DESCPRIPTION;
            }
            set
            {
                _DESCPRIPTION = value;
            }
        }

    }


}
