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
//' Create date:	10/06/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SEARCHInfo
    {
        private int _ID;
        private int _POSITION;
        private string _FIELDCODE;
        private string _FIELDNAME;
        private string _FIELDTYPE;
        private string _GWTYPE;
        private int _OPERATOR;

        public SEARCHInfo()
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
        public int POSITION
        {
            get
            {
                return _POSITION;
            }
            set
            {
                _POSITION = value;
            }
        }
        public string FIELDCODE
        {
            get
            {
                return _FIELDCODE;
            }
            set
            {
                _FIELDCODE = value;
            }
        }
        public string FIELDNAME
        {
            get
            {
                return _FIELDNAME;
            }
            set
            {
                _FIELDNAME = value;
            }
        }
        public string FIELDTYPE
        {
            get
            {
                return _FIELDTYPE;
            }
            set
            {
                _FIELDTYPE = value;
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
        public int OPERATOR
        {
            get
            {
                return _OPERATOR;
            }
            set
            {
                _OPERATOR = value;
            }
        }
    }
}
