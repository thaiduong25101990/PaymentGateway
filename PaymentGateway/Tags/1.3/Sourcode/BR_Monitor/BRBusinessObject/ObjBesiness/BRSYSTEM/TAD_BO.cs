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
	public class TADController
	{
		
		public int AddTAD(TADInfo objTable)
		{
			return TADDP.Instance().AddTAD(objTable);
		}
		
		public int UpdateTAD(TADInfo objTable)
		{
			return TADDP.Instance().UpdateTAD(objTable);
		}

        public int DeleteTAD(int TAD_ID)
		{
            return TADDP.Instance().DeleteTAD(TAD_ID);
		}
        public DataSet GetTAD1()
        {
            return TADDP.Instance().GetTAD1();
        }
        public DataSet GetTAD2()
        {
            return TADDP.Instance().GetTAD2();
        }
        public DataSet GetTAD()
        {
            return TADDP.Instance().GetTAD();
        }

        public DataSet GetTADInfo()
        {
            return TADDP.Instance().GetTADInfo();
        }
        public DataTable Search_Sbv(string pSBV_TADID,string pGWBankCode,string strWhere)
        {
            return TADDP.Instance().Search_Sbv(pSBV_TADID, pGWBankCode, strWhere);
        }

        public DataSet GetTADBranch(string SIBSBankCode)
        {
            return TADDP.Instance().GetTADBranch(SIBSBankCode);
        }
        //Lay ra ma cua hoi so chinh trong form IQSNew1
        public DataTable GetTAD_HOST(string strUserID)
        {
            return TADDP.Instance().GetTAD_HOST(strUserID);
        }
 
        public DataTable GetTADName()
        {
            return TADDP.Instance().GetTADName();
        }

        public int AddIBPSHVLV(TADInfo objTable)
        {
            return TADDP.Instance().AddIBPSHVLV(objTable);
        }

        public int UpdateIBPSHVLV(TADInfo objTable)
        {
            return TADDP.Instance().UpdateIBPSHVLV(objTable);
        }

        public int DeleteIBPSHVLV(int TAD_ID)
        {
            return TADDP.Instance().DeleteIBPSHVLV(TAD_ID);
        }

        public DataSet GetIBPSHVLV()
        {
            return TADDP.Instance().GetIBPSHVLV();
        }
        public DataTable Get_Tad()
        {
            return TADDP.Instance().Get_Tad();
        }
        public DataTable GetTadFW()
        {
            return TADDP.Instance().GetTadFW();
        }
         public DataSet GetTAD_View(string strArea)
        {
            return TADDP.Instance().GetTAD_View(strArea);
        }
         public DataTable GetTAD_ERROR(string strSTATUS)
         {
             return TADDP.Instance().GetTAD_ERROR(strSTATUS);
         }
         public DataTable GetTAD_SIBS_BANK_CODE(string strTAD)
         {
             return TADDP.Instance().GetTAD_SIBS_BANK_CODE(strTAD);
         }
         public DataSet GetTAD_DBLink_Name()
         {
             return TADDP.Instance().GetTAD_DBLink_Name();
         }
         public string GetROUTER_TAD()
         {
             return TADDP.Instance().GetROUTER_TAD();
         }

         public DataTable GetTAD_TAD(string pGW_BANK_CODE)
         {
             return TADDP.Instance().GetTAD_TAD(pGW_BANK_CODE);
         }
         public DataTable GetTAD_CHECK(string pSIBS_BANK_CODE)
         {
             return TADDP.Instance().GetTAD_CHECK(pSIBS_BANK_CODE);
         }
         public DataTable GetTAD_CheckMainBranch(string pGW_BANK_CODE)
         {
             return TADDP.Instance().GetTAD_CheckMainBranch(pGW_BANK_CODE);
         }
         public DataTable GetTAD_CheckHeadOffice(string pTAD)
         {
             return TADDP.Instance().GetTAD_CheckHeadOffice(pTAD);
         }
         public DataTable GET_HO_TAD()
         {
             return TADDP.Instance().GET_HO_TAD();
         }
	}
	
	
}
