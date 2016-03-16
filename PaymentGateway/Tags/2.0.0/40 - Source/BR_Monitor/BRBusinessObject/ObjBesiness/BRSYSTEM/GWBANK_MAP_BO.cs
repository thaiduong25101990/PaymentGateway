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
	public class GWBANK_MAPController
	{
		
		public int AddGWBANK_MAP(GWBANK_MAPInfo objTable)
		{
			return GWBANK_MAPDP.Instance().AddGWBANK_MAP(objTable);
		}
		
		public int UpdateGWBANK_MAP(GWBANK_MAPInfo objTable)
		{
			return GWBANK_MAPDP.Instance().UpdateGWBANK_MAP(objTable);
		}

        public int DeleteGWBANK_MAP(int iID)
		{
            return GWBANK_MAPDP.Instance().DeleteGWBANK_MAP(iID);
		}

        public DataSet GetGWBANK_MAP()
        {
            return GWBANK_MAPDP.Instance().GetGWBANK_MAP();
        }
	}
	
	
}
