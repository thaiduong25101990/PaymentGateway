using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRBusinessObject
{
   public class IQS_MSG_CONTENTInfo
    {
          private int _MSG_ID;
          private int _QUERY_ID;
          private string _F20;
          private string _FROMBANK;
          private string _TOBANK;
          private string _INTERFACE;
          private DateTime _DATECREATE;
          private string _REF_NUMBER;
          private int _STATUS;
          private DateTime _DATEGW;
          private string _MSGCONTENT;
          private string _GWOPTION;
          private string _IQSOPTION;
          private string _MSG_DIRECTION;
          private string _MSG_TYPE;
          private DateTime _ORG_TRANS_DATE;
          private string _TELLER_ID;
          private string _ORG_RM_NUMBER;
          private string _PRODUCT_CODE;
          private double _AMOUNT;
          private string _CCYCD;

       public IQS_MSG_CONTENTInfo()
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
       public int QUERY_ID
       {
           get
           {
               return _QUERY_ID;
           }
           set
           {
               _QUERY_ID = value;
           }
       }
       public int STATUS
       {
           get
           {
               return _STATUS;
           }
           set
           {
               _STATUS = value;
           }
       }
       public DateTime DATECREATE
       {
           get
           {
               return _DATECREATE;
           }
           set
           {
               _DATECREATE = value;
           }
       }
       public DateTime DATEGW
       {
           get
           {
               return _DATEGW;
           }
           set
           {
               _DATEGW = value;
           }
       }
       public string F20
       {
           get
           {
               return _F20;
           }
           set
           {
               _F20 = value;
           }
       }
       public string TOBANK
       {
           get
           {
               return _TOBANK;
           }
           set
           {
               _TOBANK = value;
           }
       }
       public string FROMBANK
       {
           get
           {
               return _FROMBANK;
           }
           set
           {
               _FROMBANK = value;
           }
       }
       public string INTERFACE
       {
           get
           {
               return _INTERFACE;
           }
           set
           {
               _INTERFACE = value;
           }
       }
       public string REF_NUMBER
       {
           get
           {
               return _REF_NUMBER;
           }
           set
           {
               _REF_NUMBER = value;
           }
       }
       public string MSGCONTENT
       {
           get
           {
               return _MSGCONTENT;
           }
           set
           {
               _MSGCONTENT = value;
           }
       }
       public string GWOPTION
       {
           get
           {
               return _GWOPTION;
           }
           set
           {
               _GWOPTION = value;
           }
       }
       public string IQSOPTION
       {
           get
           {
               return _IQSOPTION;
           }
           set
           {
               _IQSOPTION = value;
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


       public DateTime ORG_TRANS_DATE
       {
           get
           {
               return _ORG_TRANS_DATE;
           }
           set
           {
               _ORG_TRANS_DATE = value;
           }
       }

       public string TELLER_ID
       {
           get
           {
               return _TELLER_ID;
           }
           set
           {
               _TELLER_ID = value;
           }
       }

       public string ORG_RM_NUMBER
       {
           get
           {
               return _ORG_RM_NUMBER;
           }
           set
           {
               _ORG_RM_NUMBER = value;
           }
       }
        //PRODUCT_CODE
       public string PRODUCT_CODE
       {
           get
           {
               return _PRODUCT_CODE;
           }
           set
           {
               _PRODUCT_CODE = value;
           }
       }
       public double AMOUNT
       {
           get { return _AMOUNT; }
           set { _AMOUNT = value; }
       }
       public string CCYCD
       {
           get { return _CCYCD; }
           set { _CCYCD = value; }
       }
    }
}
