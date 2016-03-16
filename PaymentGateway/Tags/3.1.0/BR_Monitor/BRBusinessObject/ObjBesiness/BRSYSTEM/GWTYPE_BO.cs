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
	public class GWTYPEController
	{
		
		public int AddGWTYPE(GWTYPEInfo objTable)
		{
			return GWTYPEDP.Instance().AddGWTYPE(objTable);
		}
		
		public int UpdateGWTYPE(GWTYPEInfo objTable)
		{
			return GWTYPEDP.Instance().UpdateGWTYPE(objTable);
		}

        public int DeleteGWTYPE(int iID)
		{
			return GWTYPEDP.Instance().DeleteGWTYPE(iID);
		}

        public DataSet GetGwtype()
        {
            return GWTYPEDP.Instance().GetGwtype();
        }
        public DataSet GetGwtype(string pGWTYPE)
        {
            return GWTYPEDP.Instance().GetGwtype(pGWTYPE);
        }
        public DataSet GetGWTYPE()
        {
            return GWTYPEDP.Instance().GetGWTYPE();
        }
        public DataSet GetGWTYPESearch(string strSQL)
        {
            return GWTYPEDP.Instance().GetGWTYPESearch(strSQL);
        }
        public DataSet GetGWTYPE_GROUP(string type)
        {
            return GWTYPEDP.Instance().GetGWTYPE_GROUP(type);
        }
        public DataSet GetGWTYPEName()
        {
            return GWTYPEDP.Instance().GetGWTYPEName();
        }
        public DataTable GetGwtype_ID(string pGWTYPE)
        {
            return GWTYPEDP.Instance().GetGwtype_ID(pGWTYPE);
        }
        //Check dien con co o cac kenh ko
        public bool CheckChannelData(string strGWTYPE)
        {
            return GWTYPEDP.Instance().CheckChannelData(strGWTYPE); 
        }
	}
	
	
}
