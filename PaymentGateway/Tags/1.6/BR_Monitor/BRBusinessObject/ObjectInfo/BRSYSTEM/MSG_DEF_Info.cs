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
    public class MSG_DEFInfo
    {

        private int _FIELD_ID;
        private string _MSG_DEF_ID;
        private string _FIELD_NAME;
        private string _FIELD_DESCRIPTION;
        private string _FIELD_CODE;
        private int _SIBS_POS;
        private int _GW_POS;
        private int _LENGTH;
        private int _DATA_TYPE;
        private string _DEFAULT_VALUE;
        private string _SIBS_FIELD_CODE;
        private string _CHK;


        public MSG_DEFInfo()
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

        public string MSG_DEF_ID
        {
            get
            {
                return _MSG_DEF_ID;
            }
            set
            {
                _MSG_DEF_ID = value;
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

        public int SIBS_POS
        {
            get
            {
                return _SIBS_POS;
            }
            set
            {
                _SIBS_POS = value;
            }
        }

        public int GW_POS
        {
            get
            {
                return _GW_POS;
            }
            set
            {
                _GW_POS = value;
            }
        }

        public int LENGTH
        {
            get
            {
                return _LENGTH;
            }
            set
            {
                _LENGTH = value;
            }
        }

        public int DATA_TYPE
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

        public string SIBS_FIELD_CODE
        {
            get
            {
                return _SIBS_FIELD_CODE;
            }
            set
            {
                _SIBS_FIELD_CODE = value;
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


    }


}
