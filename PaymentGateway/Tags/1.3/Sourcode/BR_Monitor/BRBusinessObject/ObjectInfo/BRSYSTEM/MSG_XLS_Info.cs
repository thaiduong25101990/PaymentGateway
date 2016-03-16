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
    public class MSG_XLSInfo
    {
        private int _FIELD_ID;
        private string _GWTYPE;
        private string _MSG_TYPE;
        private string _XLSCOL;
        private string _FIELD_NAME;//
        private string _FIELD_DESCRIPTION;
        private string _CHK;
        private int _ROW_BEGIN;
        private int _MAX_ROW;
        private int _MAX_LENGTH;
        private string _DATA_TYPE;
        private string _SWIFT_FIELD_NAME;
        private string _MSG_DIRECTION;
        private string _DEFAULT_VALUE;
        private int _ROW_NUM;
        private int _PART_NUM;
        public MSG_XLSInfo()
        {
        }
        public int FIELD_ID
        {
            get
            {
                return _FIELD_ID;
            }
            set
            {
                _FIELD_ID = value;
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
        public string MSG_TYPE
        {
            get
            {
                return MSG_TYPE;
            }
            set
            {
                _MSG_TYPE = value;
            }
        }
        public string XLSCOL
        {
            get
            {
                return _XLSCOL;
            }
            set
            {
                _XLSCOL = value;
            }
        }
        public string FIELD_NAME
        {
            get
            {
                return _FIELD_NAME;
            }
            set
            {
                _FIELD_NAME = value;
            }
        }
        public string FIELD_DESCRIPTION
        {
            get
            {
                return _FIELD_DESCRIPTION;
            }
            set
            {
                _FIELD_DESCRIPTION = value;
            }
        }
        public string CHK
        {
            get
            {
                return _CHK;
            }
            set
            {
                _CHK = value;
            }
        }
        public int ROW_BEGIN
        {
            get
            {
                return _ROW_BEGIN;
            }
            set
            {
                _ROW_BEGIN = value;
            }
        }
        public int MAX_ROW
        {
            get
            {
                return _MAX_ROW;
            }
            set
            {
                _MAX_ROW = value;
            }
        }
        public int MAX_LENGTH
        {
            get
            {
                return _MAX_LENGTH;
            }
            set
            {
                _MAX_LENGTH = value;
            }
        }
        public string DATA_TYPE
        {
            get
            {
                return _DATA_TYPE;
            }
            set
            {
                _DATA_TYPE = value;
            }
        }
        public string SWIFT_FIELD_NAME
        {
            get
            {
                return _SWIFT_FIELD_NAME;
            }
            set
            {
                _SWIFT_FIELD_NAME = value;
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
        public string DEFAULT_VALUE
        {
            get
            {
                return _DEFAULT_VALUE;
            }
            set
            {
                _DEFAULT_VALUE = value;
            }
        }
        public int ROW_NUM
        {
            get
            {
                return _ROW_NUM;
            }
            set
            {
                _ROW_NUM = value;
            }
        }
        public int PART_NUM
        {
            get
            {
                return _PART_NUM;
            }
            set
            {
                _PART_NUM = value;
            }
        }
       
    }
}
