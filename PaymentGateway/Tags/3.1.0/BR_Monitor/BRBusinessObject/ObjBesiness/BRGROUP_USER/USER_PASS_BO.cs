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
	public class USER_PASSController
	{
		
		public int AddUSER_PASS(USER_PASSInfo objTable)
		{
			return USER_PASSDP.Instance().AddUSER_PASS(objTable);
		}
		
		public int UpdateUSER_PASS(USER_PASSInfo objTable)
		{
			return USER_PASSDP.Instance().UpdateUSER_PASS(objTable);
		}

        public int DeleteUSER_PASS(USER_PASSInfo objTable)
		{
			return USER_PASSDP.Instance().DeleteUSER_PASS( objTable);
		}
        public DataSet GetUSERS_PASS(int  pUSERID)
        {
            return USER_PASSDP.Instance().GetUSERS_PASS(pUSERID);
        }
        public DataSet CheckUSERS_PASS(string pUSERID, string pPASSWORD)
        {
            return USER_PASSDP.Instance().CheckUSERS_PASS(pUSERID, pPASSWORD);
        }
        public DataSet GetPRE_PASS(int pPASSTIME)
        {
            return USER_PASSDP.Instance().GetPRE_PASS(pPASSTIME);
        }
        public DataSet GetUSERS_PASS_STRING(string pUSERID)
        {
            return USER_PASSDP.Instance().GetUSERS_PASS_STRING(pUSERID);
        }
        public DataSet GetUSERS_PASS_NUMCHANGEPASS(string strUSERID, int iNumRow, string strCurrPass)
        {
            return USER_PASSDP.Instance().GetUSERS_PASS_NUMCHANGEPASS(strUSERID, iNumRow, strCurrPass);
        }
	}
	
	
}
