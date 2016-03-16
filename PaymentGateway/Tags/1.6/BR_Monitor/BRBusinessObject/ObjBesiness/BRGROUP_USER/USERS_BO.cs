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
	public class USERSController
	{
		
		public int AddUSERS(USERSInfo objTable)
		{
			return USERSDP.Instance().AddUSERS(objTable);
		}

        public bool CHECK_PASS_STRING(string sPass, out int intNum)
        {
            return USERSDP.Instance().CHECK_PASS_STRING(sPass, out intNum);
        }
        public bool CHECK_PASS_LENGTH(string sPass, out int iNumber)
        {
            return USERSDP.Instance().CHECK_PASS_LENGTH(sPass, out iNumber);
        }

		
		public int UpdateUSERS(USERSInfo objTable)
		{
            return USERSDP.Instance().UpdateUSERS(objTable);
		}
        public int GetUpdate_chengePass(int iID, string pPASSWORD, DateTime pPASTIME, DateTime pLASTDATE)
        {
            return USERSDP.Instance().GetUpdate_chengePass(iID, pPASSWORD,pPASTIME,pLASTDATE);
        }
        public int DeleteUSERS(int ID)
		{
            return USERSDP.Instance().DeleteUSERS(ID);
		}
        public DataSet GetUSERS()
        {
            return USERSDP.Instance().GetUSERS();
        }
        public DataSet GetUSERSGROUP(string groupname)
        {
            return USERSDP.Instance().GetUSERSGROUP(groupname);
        }
        public DataSet GetUSERS_PASS(string pUsername)
        {
            return USERSDP.Instance().GetUSERS_PASS(pUsername);
        }

        public DataSet USERS_PASS(string pUSERID)
        {
            return USERSDP.Instance().USERS_PASS(pUSERID);
        }

        public DataSet Userid_UD(string pUsername)
        {
            return USERSDP.Instance().Userid_UD(pUsername);
        }

        public DataTable GetUSERS_PASS1(string pUserid)
        {
            return USERSDP.Instance().GetUSERS_PASS1(pUserid);
        }
        public DataSet GetUSERS_BRANCH()
        {
            return USERSDP.Instance().GetUSERS_BRANCH();
        }
        public DataSet GetUSERS_BRANCHT()
        {
            return USERSDP.Instance().GetUSERS_BRANCHT();
        }
        public DataSet GetUSERSBR(string pBRANCH)
        {
            return USERSDP.Instance().GetUSERSBR(pBRANCH);
        }
        public DataSet GET_USER_PASS(string pUSERNAME,string pPASSWORD)
        {
            return USERSDP.Instance().GET_USER_PASS(pUSERNAME, pPASSWORD);
        }
        public DataTable LOGSTS(string pUSERNAME, string pPASSWORD)
        {
            return USERSDP.Instance().LOGSTS(pUSERNAME, pPASSWORD);
        }
        public int UPDATE_LOGSTS(string pUSERNAME, string pLOGSTS)
        {
            return USERSDP.Instance().UPDATE_LOGSTS(pUSERNAME,  pLOGSTS);
        }
        public DataSet GetUSERS_GROUP(int  pGROUPID)
        {
            return USERSDP.Instance().GetUSERS_GROUP(pGROUPID);
        }

        public DataTable Get_Groupid(string pGROUP_name)
        {
            return USERSDP.Instance().Get_Groupid(pGROUP_name);
        }

        public DataSet GetUSERSUPDATEPASS(string pUSERID)
        {
            return USERSDP.Instance().GetUSERSUPDATEPASS(pUSERID);
        }

        public DataTable GetRoll(string pUserID, string pGWtype)
        {
            return USERSDP.Instance().GetRoll(pUserID,pGWtype);
        }
        public int UpdateUSERSTATUS(string strUserID,string strstatus)
        {
            return USERSDP.Instance().UpdateUSERSTATUS(strUserID, strstatus);
        }
        
    }
	
	
}
