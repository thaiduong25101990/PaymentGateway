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
    public class DEPARTMENTInfo
    {

        private string _DEPT_CODE;
        private string _DEPT_NAME;
        private int _DEPT_ID;

        public DEPARTMENTInfo()
        {

        }

        public string DEPT_CODE
        {
            get { return _DEPT_CODE; }
            set { _DEPT_CODE = value; }
        }

        public string DEPT_NAME
        {
            get { return _DEPT_NAME; }
            set { _DEPT_NAME = value; }
        }

        public int DEPT_ID
        {
            get { return _DEPT_ID; }
            set { _DEPT_ID = value; }

        }
    }
}
