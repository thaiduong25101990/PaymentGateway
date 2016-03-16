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
	public class MSG_LISTController
	{
		
		public int AddMSG_LIST(MSG_LISTInfo objTable)
		{
			return MSG_LISTDP.Instance().AddMSG_LIST(objTable);
		}
		
		public int UpdateMSG_LIST(MSG_LISTInfo objTable)
		{
			return MSG_LISTDP.Instance().UpdateMSG_LIST(objTable);
		}

        public int DeleteMSG_LIST(int iID)
		{
            return MSG_LISTDP.Instance().DeleteMSG_LIST(iID);
		}

        public DataSet GetMSG_List()
        {
            return MSG_LISTDP.Instance().GetMSGList();
        }
	}
	
	
}
