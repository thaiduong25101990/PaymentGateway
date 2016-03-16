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
	public class CURRENCY_CHANNELController
	{

        public int AddCURRENCY(CURRENCY_CHANNELInfo objTable)
        {
            return CURRENCY_CHANNELDP.Instance().AddCURRENCY(objTable);
        }
        public DataTable GetCurrency_Import(string pGwtype)
        {
            return CURRENCY_CHANNELDP.Instance().GetCurrency_Import(pGwtype);
        }
       
        public int UpdateCURRENCY(CURRENCY_CHANNELInfo objTable)
        {
            return CURRENCY_CHANNELDP.Instance().UpdateCURRENCY(objTable);
        }

        public int UpdateCURRENCY_STATUS(CURRENCY_CHANNELInfo objTable)
        {
            return CURRENCY_CHANNELDP.Instance().UpdateCURRENCY_STATUS(objTable);
        }

        public int DeleteCURRENCY(int iCurID,out string strOut)
		{
            return CURRENCY_CHANNELDP.Instance().DeleteCURRENCY(iCurID, out strOut);
		}

        public int DeleteCURRENCY(string strCCYCD, string strGWTYPE, string strPARTNER)
		{
            return CURRENCY_CHANNELDP.Instance().DeleteCURRENCY(strCCYCD,strGWTYPE,strPARTNER );
		}
        
		public DataSet GetCurrency()
		{
            return CURRENCY_CHANNELDP.Instance().GetCurrency();
		}
        public DataSet GetCurrency(CURRENCY_CHANNELInfo obj)
        {
            return CURRENCY_CHANNELDP.Instance().GetCurrency(obj);
        }

        public DataSet CheckEditCurrency(CURRENCY_CHANNELInfo obj)
        {
            return CURRENCY_CHANNELDP.Instance().CheckEditCurrency(obj);
        }

        public DataTable GetCurrency1()
        {
            return CURRENCY_CHANNELDP.Instance().GetCurrency1();
        }

        public DataSet GetCurrency_code(string pCCYCD)
        {
            return CURRENCY_CHANNELDP.Instance().GetCurrency_code(pCCYCD);
        }

        public DataSet GetCurrencySearch(string strSQL)
        {
            return CURRENCY_CHANNELDP.Instance().GetCurrencySearch(strSQL);
        }
        public DataSet GetCurrencySearch_Correct(string strSQL)
        {
            return CURRENCY_CHANNELDP.Instance().GetCurrencySearch_Correct(strSQL);
        }
	}
	
	
}
