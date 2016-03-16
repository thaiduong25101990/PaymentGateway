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
namespace  BR.BRBusinessObject
{
	public class GWTYPE_CURRENCYDP
	{
		
		public GWTYPE_CURRENCYDP()
		{
		}
		public static GWTYPE_CURRENCYDP Instance()
		{
			return new GWTYPE_CURRENCYDP();
		}
		
		public int AddGWTYPE_CURRENCY(GWTYPE_CURRENCYInfo objTable)
		{
			try
			{
                return 0;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		
		public int UpdateGWTYPE_CURRENCY(GWTYPE_CURRENCYInfo objTable)
		{
			try
			{
                return 0;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

        public int DeleteGWTYPE_CURRENCY(int Currency_ID)
		{
			try
			{
                return 0;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		
	
		
	}
	
	
}
