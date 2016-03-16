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
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace BR.BRBusinessObject
{
    public class SWIFT_AUTO_VALUE_Info
    {

        private int _PRM_ID;
        private string _KEY_WORD;
        private string _MESSAGE;

        public SWIFT_AUTO_VALUE_Info()
        {

        }

        public int PRM_ID
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

    }
}
