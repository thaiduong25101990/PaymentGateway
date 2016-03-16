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
	public class GW_PARAMETERDP
	{
		
		public GW_PARAMETERDP()
		{
		}
		public static GW_PARAMETERDP Instance()
		{
			return new GW_PARAMETERDP();
		}
		
		public int AddGW_PARAMETER(GW_PARAMETERInfo objTable)
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
		
		public int UpdateGW_PARAMETER(GW_PARAMETERInfo objTable)
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

        public int DeleteGW_PARAMETER(GW_PARAMETERInfo objTable)
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
