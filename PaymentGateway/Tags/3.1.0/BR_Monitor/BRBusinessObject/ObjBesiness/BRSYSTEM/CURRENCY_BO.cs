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
//' Create date:	19/04/2008 21:39
//' Description:
//' Revise History:
//' =============================================
namespace  BR.BRBusinessObject
{
	public class CURRENCYController
	{
		
		public int AddCURRENCY(CURRENCYInfo objTable)
		{
			return CURRENCYDP.Instance().AddCURRENCY(objTable);
		}

        public int UpdateCURRENCY(CURRENCYInfo objTable,string CCYCD, string ShortName)
        {
            return CURRENCYDP.Instance().UpdateCURRENCY(objTable, CCYCD, ShortName);
        }

        public int DeleteCURRENCY(int iCurID)
		{
            return CURRENCYDP.Instance().DeleteCURRENCY(iCurID);
		}

		public DataSet GetCurrency()
		{
			return CURRENCYDP.Instance().GetCurrency();
		}
        public DataTable GetCurrency1()
        {
            return CURRENCYDP.Instance().GetCurrency1();
        }

        public DataTable GetCurrency_Vcb()
        {
            return CURRENCYDP.Instance().GetCurrency_Vcb();
        }

        public DataSet GetCurrency3()
        {
            return CURRENCYDP.Instance().GetCurrency3();
        }

        public DataTable GetCurrency2(string strGWTYPE)
        {
            return CURRENCYDP.Instance().GetCurrency2(strGWTYPE);
        }

        public DataSet GetCurrency_code(string pCCYCD)
        {
            return CURRENCYDP.Instance().GetCurrency_code(pCCYCD);
        }

        public DataSet GetCurrency_code2(string pShortCD)
        {
            return CURRENCYDP.Instance().GetCurrency_code2(pShortCD);
        }

        public DataSet GetCurrencySearch(string strSQL)
        {
            return CURRENCYDP.Instance().GetCurrencySearch(strSQL);
        }

        public DataSet GetCurrencyStatusTTSP(string strPartner)
        {
            return CURRENCYDP.Instance().GetCurrencyStatusTTSP(strPartner);
        }
	}
	
	
}
