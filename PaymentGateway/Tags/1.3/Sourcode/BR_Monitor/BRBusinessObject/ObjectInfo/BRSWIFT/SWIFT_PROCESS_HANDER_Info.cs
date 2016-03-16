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

namespace BR.BRBusinessObject
{
    public class SWIFT_PROCESS_HANDERInfo
    {
        private int _MSG_ID;
        private string _TELLER_ID;
        private string _NEW_BRANCH;
        private string _NEW_DEPARTMENT;
        private int _NEW_PROCESSSTS;
        public SWIFT_PROCESS_HANDERInfo()
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
        public string TELLER_ID
        {
            get
            {
                return _TELLER_ID;
            }
            set
            {
                _TELLER_ID = value;
            }
        }
        public string NEW_BRANCH
        {
            get
            {
                return _NEW_BRANCH;
            }
            set
            {
                _NEW_BRANCH = value;
            }
        }

        public string NEW_DEPARTMENT
        {
            get
            {
                return _NEW_DEPARTMENT;
            }
            set
            {
                _NEW_DEPARTMENT = value;
            }
        }

        public int NEW_PROCESSSTS
        {
            get
            {
                return _NEW_PROCESSSTS;
            }
            set
            {
                _NEW_PROCESSSTS = value;
            }
        }
    }
}
