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
    public class SYSPARA_BO
    {

        public int AddSYSPARA(SYSPARA_Info objTable)
        {
            return SYSPARA_DAO.Instance().AddSYS_PARA(objTable);
        }

        public int UpdateSYS_PARA(SYSPARA_Info objTable)
        {
            return SYSPARA_DAO.Instance().UpdateSYS_PARA(objTable);
        }

        public DataSet GetSysParaByID(long iID)
        {
            return SYSPARA_DAO.Instance().GetSysParaByID(iID);
        }

        public DataSet GetAllSysPara()
        {
            return SYSPARA_DAO.Instance().GetAllSysPara();
        }

        public int DelParaByID(long iID)
        {
            return SYSPARA_DAO.Instance().DelParaByID(iID);
        }

        public int CheckParaByName(string sName, long iID)
        {
            return SYSPARA_DAO.Instance().CheckParaByName(sName, iID);
        }

        public int CheckParaUsing(long iID)
        {
            return SYSPARA_DAO.Instance().CheckParaUsing(iID);
        }

        public int GetAccountNumber()
        {
            return SYSPARA_DAO.Instance().GetAccountNumber();
        }

        public int GetNumLoginDay()
        {
            return SYSPARA_DAO.Instance().GetNumLoginDay();
        }

        public int GetLoginTime()
        {
            return SYSPARA_DAO.Instance().GetLoginTime();            
        }

        public int GetPassTime()
        {
            return SYSPARA_DAO.Instance().GetPassTime();
        }

        
    }

}
