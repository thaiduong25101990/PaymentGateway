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
	public class SYSVARController
	{
		
		public int AddSYSVAR(SYSVARInfo objTable)
		{
            return SYSVARDP.Instance().AddSYSVAR(objTable);
		}
		
		public int UpdateSYSVAR(SYSVARInfo objTable)
		{
			return SYSVARDP.Instance().UpdateSYSVAR(objTable);
		}

        public int UpdateSYSVARDayDuplicate(SYSVARInfo objTable)
        {
            return SYSVARDP.Instance().UpdateSYSVARDayDuplicate(objTable);
        }

		public int DeleteSYSVAR(int ID)
		{
			return SYSVARDP.Instance().DeleteSYSVAR(ID);
		}

        public DataSet GetSYSVAR()
        { 
            return SYSVARDP.Instance().GetSYSVAR();
        }

        public DataSet GetSYSVAR_NAME(string strVarName, string strGWType)
        {
            return SYSVARDP.Instance().GetSYSVAR_NAME(strVarName, strGWType);
        }

        public DataSet GetIBPSBankLength(string strLength)
        {
            return SYSVARDP.Instance().GetIBPSBankLength(strLength);
        }
        public DataTable GetSTATEMENT_ID()
        {
            return SYSVARDP.Instance().GetSTATEMENT_ID();
        }
        public int UpdateGetSTATEMENT_ID(SYSVARInfo objInfo)
        {
            return SYSVARDP.Instance().UpdateGetSTATEMENT_ID(objInfo);
        }
        public DataSet GetIBPS_ROUTER_TAD(string strSIBSBankCode, string strStatus)
        {
            return SYSVARDP.Instance().GetIBPS_ROUTER_TAD(strSIBSBankCode, strStatus);
        }

        public int UpdateSysvar(string strVarName, string strValue, string strNote)
        {
            return SYSVARDP.Instance().UpdateSysvar(strVarName, strValue, strNote);
        }

        public DataTable Get_Ontime(string pVarName)
        {
            return SYSVARDP.Instance().Get_Ontime(pVarName);
        }
	}
	
	
}
