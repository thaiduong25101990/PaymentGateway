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
    public class SWIFT_IWCKHInfo
    {

        private string _FIELD;
        private string _VAULE;
        private string _DESCRIPTIONS;

        public SWIFT_IWCKHInfo()
        {

        }

        public string FIELD
        {
            get
            {
                return _FIELD;
            }
            set
            {
                _FIELD = value;
            }
        }
        public string VAULE
        {
            get
            {
                return _VAULE;
            }
            set
            {
                _VAULE = value;
            }
        }
        public string DESCRIPTIONS
        {
            get
            {
                return _DESCRIPTIONS;
            }
            set
            {
                _DESCRIPTIONS = value;
            }
        }
    }


}
