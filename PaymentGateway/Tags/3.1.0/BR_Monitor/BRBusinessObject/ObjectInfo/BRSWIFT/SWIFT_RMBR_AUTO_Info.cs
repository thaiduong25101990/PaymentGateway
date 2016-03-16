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
    public class SWIFT_RMBR_AUTOInfo
    {
       private string _ORG_BRAN;
       private string _RECEIVER_BRAN;
       public SWIFT_RMBR_AUTOInfo()
        {

        }
       public string ORG_BRAN
        {
            get
            {
                return _ORG_BRAN;
            }
            set
            {
                _ORG_BRAN = value;
            }
        }
       public string RECEIVER_BRAN
       {
           get
           {
               return _RECEIVER_BRAN;
           }
           set
           {
               _RECEIVER_BRAN = value;
           }
       }

    }
}
