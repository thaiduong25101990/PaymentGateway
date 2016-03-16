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
//' Update: Quynd 12/06/2008
//' =============================================
namespace  BR.BRBusinessObject
{
	public class SWIFT_BANK_MAPInfo
	{

        private int _BANK_MAP_ID;
        private string _SIBS_BANK_CODE;
        private string _SWIFT_BANK_CODE;
        private string _BANK_NAME;
        private string _DESCRIPTION;
        private string _TELLERID;

        public SWIFT_BANK_MAPInfo()
        {

        }

        public int BANK_MAP_ID
        {
            get { return _BANK_MAP_ID; }
            set { _BANK_MAP_ID = value; }
        }

        public string SIBS_BANK_CODE
        {
            get { return _SIBS_BANK_CODE; }
            set { _SIBS_BANK_CODE = value; }
        }

        public string SWIFT_BANK_CODE
        {
            get { return _SWIFT_BANK_CODE; }
            set { _SWIFT_BANK_CODE = value; }
        }

        public string BANK_NAME
        {
            get { return _BANK_NAME; }
            set { _BANK_NAME = value; }
        }

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public string TELLERID
        {
            get { return _TELLERID; }
            set { _TELLERID = value; }
        } 
    
	}
	
	
}
