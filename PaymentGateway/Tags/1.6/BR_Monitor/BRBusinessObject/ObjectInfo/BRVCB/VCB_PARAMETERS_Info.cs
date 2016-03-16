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
    public class VCB_PARAMETERSInfo
    {
        private int _ID;
        private string _DEPARTMENT;
        private string _MSG_TYPE;
        private string _BANK_CODE;
        private string _GWTYPE;
        public VCB_PARAMETERSInfo()
        {
        }
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
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
        public string BANK_CODE
        {
            get
            {
                return _BANK_CODE;
            }
            set
            {
                _BANK_CODE = value;
            }
        }

        public string GWTYPE
        {
            get
            {
                return _GWTYPE;
            }
            set
            {
                _GWTYPE = value;
            }
        }

    }
}
