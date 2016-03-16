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
	public class USER_PASS_BO
	{
		
		public int AddUSER_PASS(USER_PASSInfo objTable)
		{
            return USER_PASS_DAO.Instance().AddUSER_PASS(objTable);
		}
		
		public int UpdateUSER_PASS(USER_PASSInfo objTable)
		{
            return USER_PASS_DAO.Instance().UpdateUSER_PASS(objTable);
		}

        public int DeleteUSER_PASS(USER_PASSInfo objTable)
		{
            return USER_PASS_DAO.Instance().DeleteUSER_PASS(objTable);
		}
        public DataSet GetUSERS_PASS(int  pUSERID)
        {
            return USER_PASS_DAO.Instance().GetUSERS_PASS(pUSERID);
        }
        public DataSet CheckUSERS_PASS(string pUSERID, string pPASSWORD)
        {
            return USER_PASS_DAO.Instance().CheckUSERS_PASS(pUSERID, pPASSWORD);
        }
        public DataSet GetPRE_PASS(int pPASSTIME)
        {
            return USER_PASS_DAO.Instance().GetPRE_PASS(pPASSTIME);
        }
        public DataSet GetUSERS_PASS_STRING(string pUSERID)
        {
            return USER_PASS_DAO.Instance().GetUSERS_PASS_STRING(pUSERID);
        }
        public DataSet GetUSERS_PASS_NUMCHANGEPASS(string strUSERID, int iNumRow, string strCurrPass)
        {
            return USER_PASS_DAO.Instance().GetUSERS_PASS_NUMCHANGEPASS(strUSERID, iNumRow, strCurrPass);
        }
	}
	
	
}
