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


namespace BIDVWEB.Business
{
	public class USERS_BO
	{
		
		public int AddUSERS(USERSInfo objTable)
		{
            return USERS_DAO.Instance().AddUSERS(objTable);
		}
		
		public int UpdateUSERS(USERSInfo objTable)
		{
            return USERS_DAO.Instance().UpdateUSERS(objTable);
		}
        public int GetUpdate_chengePass(int iID, string pPASSWORD, DateTime pPASTIME, DateTime pLASTDATE)
        {
            return USERS_DAO.Instance().GetUpdate_chengePass(iID, pPASSWORD,pPASTIME,pLASTDATE);
        }
        public int DeleteUSERS(int ID)
		{
            return USERS_DAO.Instance().DeleteUSERS(ID);
		}
        public DataSet GetUSERS()
        {
            return USERS_DAO.Instance().GetUSERS();
        }
        public DataSet GetUSERSGROUP(string groupname)
        {
            return USERS_DAO.Instance().GetUSERSGROUP(groupname);
        }
        public DataSet GetUSERS_PASS(string pUsername)
        {
            return USERS_DAO.Instance().GetUSERS_PASS(pUsername);
        }
        public DataTable GetUSERS_PASS1(string pUserid)
        {
            return USERS_DAO.Instance().GetUSERS_PASS1(pUserid);
        }
        public DataSet GetUSERS_BRANCH()
        {
            return USERS_DAO.Instance().GetUSERS_BRANCH();
        }
        public DataSet GetUSERS_BRANCHT()
        {
            return USERS_DAO.Instance().GetUSERS_BRANCHT();
        }
        public DataSet GetUSERSBR(string pBRANCH, string sBranchUser)
        {
            return USERS_DAO.Instance().GetUSERSBR(pBRANCH, sBranchUser);
        }
        public DataSet GetUsersByStr(string pBRANCH, string sBranchUser, string pUserName, string pUserID)
        {
            return USERS_DAO.Instance().GetUsersByStr(pBRANCH, sBranchUser, pUserName, pUserID);
        }
        public DataSet GET_USER_PASS(string pUSERNAME,string pPASSWORD)
        {
            return USERS_DAO.Instance().GET_USER_PASS(pUSERNAME, pPASSWORD);
        }
        public DataSet GetUSERS_GROUP(int  pGROUPID)
        {
            return USERS_DAO.Instance().GetUSERS_GROUP(pGROUPID);
        }
        public DataSet GetUSERSUPDATEPASS(string pUSERID)
        {
            return USERS_DAO.Instance().GetUSERSUPDATEPASS(pUSERID);
        }

        public DataTable GetRoll(string pUserID)
        {
            return USERS_DAO.Instance().GetRoll(pUserID);
        }
        public int UpdateUSERSTATUS(string strUserID,string strstatus)
        {
            return USERS_DAO.Instance().UpdateUSERSTATUS(strUserID, strstatus);
        }
        public int UPDATE_PASS(string pUserID, string pPassword, DateTime pPASTIME, DateTime pLASTDATE)
        {
            return USERS_DAO.Instance().UPDATE_PASS(pUserID, pPassword, pPASTIME, pLASTDATE);
        }
        public int UPDATE_PASS_RESET(string pUserID, string pPassword, DateTime pPASTIME, int pSTATUS)
        {
            return USERS_DAO.Instance().UPDATE_PASS_RESET(pUserID, pPassword, pPASTIME, pSTATUS);
        }
        public int UPDATE_PASS1(string pUserID, string pPassword, DateTime pPASTIME,
            DateTime pLASTDATE, int pSTATUS, int pCOUNTTIME)
        {
            return USERS_DAO.Instance().UPDATE_PASS1(pUserID, pPassword, pPASTIME, pLASTDATE,
                pSTATUS, pCOUNTTIME);
        }

        public int CHECK_OLDPASS(string pUserID, string pPassword)
        {
            return USERS_DAO.Instance().CHECK_OLDPASS(pUserID, pPassword);
        }
       
        public int GetSumUser_Branch(string sBranch)
        {
            return USERS_DAO.Instance().GetSumUser_Branch(sBranch);
        }

        public int CHECK_USER_ADMIN(string pUserID)
        {
            return USERS_DAO.Instance().CHECK_USER_ADMIN(pUserID);
        }

        public int UPDATE_LASTDATE(string pUserID, DateTime pLASTDATE)
        {
            return USERS_DAO.Instance().UPDATE_LASTDATE(pUserID, pLASTDATE);
        }
        public string GetUSERNAME(string sUserID)
        {
            return USERS_DAO.Instance().GetUSERNAME(sUserID);
        }

        public bool CHECK_USERNAME(string sUserID, string sUserName, bool bAdd)
        {
            return USERS_DAO.Instance().CHECK_USERNAME(sUserID, sUserName, bAdd);
        }
        public bool CHECK_PASS_LENGTH(string sPass, out int iNumber)
        {
            return USERS_DAO.Instance().CHECK_PASS_LENGTH(sPass, out iNumber);
        }
        public bool CHECK_PASS_STRING(string sPass, out int intNum)
        {
            return USERS_DAO.Instance().CHECK_PASS_STRING(sPass, out intNum);
        }
        public string GetBrHo()
        {
            return USERS_DAO.Instance().GetBrHo();
        }
    }
	
	
}
