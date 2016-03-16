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
namespace  BR.BRBusinessObject
{
	public class IBPS_BANK_MAPInfo
	{
		
		private int _BANK_MAP_ID;
        private string _SIBS_BANK_CODE;
        private string _GW_BANK_CODE;
        private string _BANK_NAME;
        private string _DESCRIPTIONS;
        private string _TELLERID;
        private string _SHORT_BANK_NAME;
		
		
		public IBPS_BANK_MAPInfo()
		{
			
		}
		
		public int BANK_MAP_ID
		{
			get
			{
				return _BANK_MAP_ID;
			}
			set
			{
				_BANK_MAP_ID = value;
			}
		}
		
		public string GW_BANK_CODE
		{
			get
			{
				return _GW_BANK_CODE;
			}
			set
			{
				_GW_BANK_CODE = value;
			}
		}

        public string SIBS_BANK_CODE
        {
            get
            {
                return _SIBS_BANK_CODE;
            }
            set
            {
                _SIBS_BANK_CODE = value;
            }
        }

        public string BANK_NAME
		{
			get
			{
                return _BANK_NAME;
			}
			set
			{
                _BANK_NAME = value;
			}
		}

        public string TELLERID
		{
			get
			{
                return _TELLERID;
			}
			set
			{
                _TELLERID = value;
			}
		}

        public string  DESCRIPTIONS
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

        public string SHORT_BANK_NAME
        {
            get
            {
                return _SHORT_BANK_NAME;
            }
            set
            {
                _SHORT_BANK_NAME = value;
            }
        }
		
	}
	
	
}
