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
    public class ALLCODEInfo
    {
     private int _ID;
     private string _CDVAL;
     private string _CDNAME;
     private string _CONTENT;
     private string  _GWTYPE;
     private string _DESCRIPTION;
     private int _LSTORD;
        public ALLCODEInfo()
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
        public string CDVAL
        {
            get
            {
                return _CDVAL;
            }
            set
            {
                _CDVAL = value;
            }
        }
        public string CDNAME
        {
            get
            {
                return _CDNAME;
            }
            set
            {
                _CDNAME = value;
            }
        }
        public string CONTENT
        {
            get
            {
                return _CONTENT;
            }
            set
            {
                _CONTENT = value;
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
        public string DESCRIPTION
        {
            get
            {
                return _DESCRIPTION;
            }
            set
            {
                _DESCRIPTION = value;
            }
        }
        public int LSTORD
        {
            get
            {
                return _LSTORD;
            }
            set
            {
                _LSTORD = value;
            }
        }
        
    }
    
}
