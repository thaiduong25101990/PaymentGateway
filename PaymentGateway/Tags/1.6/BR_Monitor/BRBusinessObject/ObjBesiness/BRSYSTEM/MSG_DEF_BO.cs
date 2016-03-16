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
	public class MSG_DEFController
	{
		
		public int AddMSG_DEF(MSG_DEFInfo objTable)
		{
			return MSG_DEFDP.Instance().AddMSG_DEF(objTable);
		}
		
		public int UpdateMSG_DEF(MSG_DEFInfo objTable)
		{
			return MSG_DEFDP.Instance().UpdateMSG_DEF(objTable);
		}

        public int DeleteMSG_DEF(int intFIELD_ID)
		{
            return MSG_DEFDP.Instance().DeleteMSG_DEF(intFIELD_ID);
		}

        public DataSet GetMSG_DEF()
        {
            return MSG_DEFDP.Instance().GetMSGDef();
        }

        public DataSet GetMSGDef_MsgID(string strMsgDefID)
        {
            return MSG_DEFDP.Instance().GetMSGDef_MsgID(strMsgDefID);
        }

        public DataSet GetMSGDef_Combo()
        {
            return MSG_DEFDP.Instance().GetMSGDef_Combo();
        }
	}
	
	
}
