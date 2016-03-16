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
	public class MSG_TYPEDP
	{
		
		public MSG_TYPEDP()
		{
		}
		public static MSG_TYPEDP Instance()
		{
			return new MSG_TYPEDP();
		}
		
		public int AddMSG_TYPE(MSG_TYPEInfo objTable)
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
		
		public int UpdateMSG_TYPE(MSG_TYPEInfo objTable)
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
		
		public int DeleteMSG_TYPE(int MSG_ID)
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
