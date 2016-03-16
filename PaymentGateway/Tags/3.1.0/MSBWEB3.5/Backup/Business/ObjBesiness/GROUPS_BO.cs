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
	public class GROUPS_BO
	{
		
		public int AddGROUPS(GROUPSInfo objTable)
		{
            return GROUPS_DAO.Instance().AddGROUPS(objTable);
		}
		
		public int UpdateGROUPS(GROUPSInfo objTable)
		{
            return GROUPS_DAO.Instance().UpdateGROUPS(objTable);
		}
		
		public DataSet DeleteGROUPS(int  GROUPID,string pMenuid)
		{
            return GROUPS_DAO.Instance().DeleteGROUPS(GROUPID, pMenuid);
		}
        public DataSet DeleteGROUPS_(int GROUPID)
        {
            return GROUPS_DAO.Instance().DeleteGROUPS_(GROUPID);
        }

        public DataSet GetGROUP()
        {
            return GROUPS_DAO.Instance().GetGROUP();
        }
        public DataSet GetGROUP_ID()
        {
            return GROUPS_DAO.Instance().GetGROUP_ID();
        }
        public DataSet GetGROUP_TYPE()
        {
            return GROUPS_DAO.Instance().GetGROUP_TYPE();
        }
        
        public DataSet GetGROUP_DEPARTMENT()
        {
            return GROUPS_DAO.Instance().GetGROUP_DEPARTMENT();
        }
        public DataSet GetGROUP_USER(string  pUSERID)
        {
            return GROUPS_DAO.Instance().GetGROUP_USER(pUSERID);
        }
        public DataSet GetGROUP_USER1(string pUSERID)
        {
            return GROUPS_DAO.Instance().GetGROUP_USER1(pUSERID);
        }
        public DataSet GetGROUPNAME(string pGROUPNAME)
        {
            return GROUPS_DAO.Instance().GetGROUPNAME(pGROUPNAME);
        }
        public DataSet GetGroupID(int pGROUPID)
        {
            return GROUPS_DAO.Instance().GetGroupID(pGROUPID);
        }
        public DataSet GetGroup_IsSupervisor(string strUserID)
        {
            return GROUPS_DAO.Instance().GetGroup_IsSupervisor(strUserID);
        }
        public DataSet GetGroup_Depart(string pGWTYPE)
        {
            return GROUPS_DAO.Instance().GetGroup_Depart(pGWTYPE);
        }
        public DataSet GetGroup_Gwtype(string pUserid)
        {
            return GROUPS_DAO.Instance().GetGroup_Gwtype(pUserid);
        }

        public int CHECK_GROUP_ADMIN(string pGroupID)
        {
            return GROUPS_DAO.Instance().CHECK_GROUP_ADMIN(pGroupID);
        }
	}
	
	
}
