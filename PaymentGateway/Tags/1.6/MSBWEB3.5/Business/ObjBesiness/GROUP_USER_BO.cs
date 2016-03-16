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
	public class GROUP_USER_BO
	{
		
		public int AddGROUP_USER(GROUP_USERInfo objTable)
		{
            return GROUP_USER_DAO.Instance().AddGROUP_USER(objTable);
		}
		
		public int UpdateGROUP_USER(GROUP_USERInfo objTable)
		{
            return GROUP_USER_DAO.Instance().UpdateGROUP_USER(objTable);
		}

        public int DeleteGROUP_USER( int pGROUPID,string pUSERID)
        {
            return GROUP_USER_DAO.Instance().DeleteGROUP_USER(pGROUPID, pUSERID);
        }
        public int DeleteGroupUser(string pUSERID)
        {
            return GROUP_USER_DAO.Instance().DeleteGroupUser(pUSERID);
        }
        public DataSet GetGroup_user(int userid)
        {
            return GROUP_USER_DAO.Instance().GetGROUP_USER(userid);
        }
        public DataSet GetGroup_userDD(int  GroupID,string Userid)
        {
            return GROUP_USER_DAO.Instance().GetGroup_userDD(GroupID, Userid);
        }
        public DataSet GetGROUPDATA(int pGROUPID, string pUSERID)
        {
            return GROUP_USER_DAO.Instance().GetGROUPDATA(pGROUPID, pUSERID);
        }
        public DataSet GetGroup_Gwtype(string pGwtype)
        {
            return GROUP_USER_DAO.Instance().GetGroup_Gwtype(pGwtype);
        }
	
	
	}
	
	
}
