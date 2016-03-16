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
    public class STRANGE_CHARACTERInfo
    {

        private int _ID;
        private string _STRANGE_CHAR;
        private string _REPLACE_CHAR;
        private string _MSG_TYPE;
        private string _DEPARTMENT;
        private string _FIELD_CODE;
        private string _MSG_DIRECTION;
        private string _GWTYPE;
        private string _LINE;
        private string _POSITION;

        public STRANGE_CHARACTERInfo()
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

        public string STRANGE_CHAR
        {
            get
            {
                return _STRANGE_CHAR;
            }
            set
            {
                _STRANGE_CHAR = value;
            }
        }

        public string REPLACE_CHAR
        {
            get
            {
                return _REPLACE_CHAR;
            }
            set
            {
                _REPLACE_CHAR = value;
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

        public string FIELD_CODE
        {
            get
            {
                return _FIELD_CODE;
            }
            set
            {
                _FIELD_CODE = value;
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

        public string LINE
        {
            get
            {
                return _LINE;
            }
            set
            {
                _LINE = value;
            }
        }

        public string POSITION
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
    }


}
