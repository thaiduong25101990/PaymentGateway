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
    public class SWIFT_BR_AUTOInfo
    {
       private int _ID;
       private string  _TMTREF;
       private string _TMPRBR;

       public SWIFT_BR_AUTOInfo()
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
        public string TMTREF
        {
            get
            {
                return _TMTREF;
            }
            set
            {
                _TMTREF = value;
            }
        }
        public string TMPRBR
        {
            get
            {
                return _TMPRBR;
            }
            set
            {
                _TMPRBR = value;
            }
        }
    }
}
