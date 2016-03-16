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
	public class IBPS_BANK_MAPController
	{

      //Nang cap

        public DataSet GetIBPS_BANK_MAP()
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAP();
        }


        // het nang cap

        public int AddIBPS_BANK_MAP(IBPS_BANK_MAPInfo objTable)
        {
            return IBPS_BANK_MAPDP.Instance().AddIBPS_BANK_MAP(objTable);
        }

        public int UpdateIBPS_BANK_MAP(IBPS_BANK_MAPInfo objTable)
        {
            return IBPS_BANK_MAPDP.Instance().UpdateIBPS_BANK_MAP(objTable);
        }

        public int DeleteIBPS_BANK_MAP(int iID)
        {
            return IBPS_BANK_MAPDP.Instance().DeleteIBPS_BANK_MAP(iID);
        }

        public DataSet GetIBPS_BANK_MAPTotalBank(string strSign)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAPTotalBank(strSign);
        }
        public DataSet GetIBPS_BANK_MAP_TELLERID(string pGW_BANK_MAP)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAP_TELLERID(pGW_BANK_MAP);
        }
        public DataSet GetIBPS_BANK_MAP_GWCODE(string pSIBS_BANK_CODE)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAP_GWCODE(pSIBS_BANK_CODE);
        }
        public DataSet GetIBPS_BANK_MAPInfo(int iSIBSBankCodeLength)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAPInfo(iSIBSBankCodeLength);
        }
        public DataSet GetIBPS_BANK_MAP_GWBankCode(int iSIBSBankCodeLength, string strGWBankCode)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAP_GWBankCode(iSIBSBankCodeLength, strGWBankCode);
        }
        public DataSet GetIBPS_BANK_MAPSearch(string strSQL)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAPSearch(strSQL);
        }
        public int CheckData(string strSIBS_BANK_CODE)
        {
            return IBPS_BANK_MAPDP.Instance().CheckData(strSIBS_BANK_CODE);
        }

        public DataSet GetIBPS_BANK_MAP_BankName(string pGW_BANK_CODE)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAP_BankName(pGW_BANK_CODE);
        }

        public DataSet Get_NAME(string pGW_BANK_CODE,string pSIBS_BANK_CODE)
        {
            return IBPS_BANK_MAPDP.Instance().Get_NAME(pGW_BANK_CODE, pSIBS_BANK_CODE);
        }

        public DataSet GetIBPS_BANK_MAPStateBankIDName(string strGWBankCode)
        {
            return IBPS_BANK_MAPDP.Instance().GetIBPS_BANK_MAPStateBankIDName(strGWBankCode);
        }

        public DataTable SearchViewIBPS_BANK_MAP(string strWhere, string strSign1, string strSign2)
        {
            return IBPS_BANK_MAPDP.Instance().SearchViewIBPS_BANK_MAP(strWhere, strSign1, strSign2);
        }

        public DataTable GET_BRANCH_HOST()
        {
            return IBPS_BANK_MAPDP.Instance().GET_BRANCH_HOST();
        }

        public string GET_BANK_NAME_TAD(string pGW_BANK_CODE)
        {
            return IBPS_BANK_MAPDP.Instance().GET_BANK_NAME_TAD(pGW_BANK_CODE);
        }

    }
}


