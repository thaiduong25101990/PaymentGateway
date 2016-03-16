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


//' Template: BusinessObject.xslt 17/10/2006
//' Author:	Le Duc Quan
//' Create date:	19/04/2008 21:40
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class DEPARTMENTController
	{
		
		public int AddDEPARTMENT(DEPARTMENTInfo objTable)

		{
           
			return DEPARTMENTDP.Instance().AddDEPARTMENT(objTable);
		}
		
		public int UpdateDEPARTMENT(DEPARTMENTInfo objTable)
		{
			return DEPARTMENTDP.Instance().UpdateDEPARTMENT(objTable);
		}
		
		public int DeleteDEPARTMENT(int  iID)
		{
            return DEPARTMENTDP.Instance().DeleteDEPARTMENT(iID);
		}

        public DataSet GetDEPARTMENT()
        {
            return DEPARTMENTDP.Instance().GetDEPARTMENT();
        }
	}
	
	
}
