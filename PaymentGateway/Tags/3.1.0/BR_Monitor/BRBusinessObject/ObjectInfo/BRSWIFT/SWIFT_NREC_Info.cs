using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
   public class SWIFT_NRECInfo
    {
       private int _MSG_ID;
       private string _MSG_TYPE;
       public SWIFT_NRECInfo()
		{
			
		}

       public int MSG_ID
		{
			get
			{
                return _MSG_ID;
			}
			set
			{
                _MSG_ID = value;
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
    }
}
