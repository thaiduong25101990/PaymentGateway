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
	public class GROUPSController
	{
		
		public int AddGROUPS(GROUPSInfo objTable)
		{
			return GROUPSDP.Instance().AddGROUPS(objTable);
		}
		
		public int UpdateGROUPS(GROUPSInfo objTable)
		{
			return GROUPSDP.Instance().UpdateGROUPS(objTable);
		}
		
		public DataSet DeleteGROUPS(int  GROUPID,string pMenuid)
		{
            return GROUPSDP.Instance().DeleteGROUPS(GROUPID, pMenuid);
		}
        public DataSet DeleteGROUPS_(int GROUPID)
        {
            return GROUPSDP.Instance().DeleteGROUPS_(GROUPID);
        }

        public DataSet DeleteGROUPS_Menu(int groupid)
        {
            return GROUPSDP.Instance().DeleteGROUPS_Menu(groupid);
        }

        public DataSet GetGROUP()
        {
            return GROUPSDP.Instance().GetGROUP();
        }
        public DataSet GetGROUP_ID()
        {
            return GROUPSDP.Instance().GetGROUP_ID();
        }
        public DataSet GetGROUP_TYPE()
        {
            return GROUPSDP.Instance().GetGROUP_TYPE();
        }
        
        public DataSet GetGROUP_DEPARTMENT()
        {
            return GROUPSDP.Instance().GetGROUP_DEPARTMENT();
        }
        public DataSet GetGROUP_USER(string  pUSERID)
        {
            return GROUPSDP.Instance().GetGROUP_USER(pUSERID);
        }
        public DataSet GetGROUP_USER1(string pUSERID)
        {
            return GROUPSDP.Instance().GetGROUP_USER1(pUSERID);
        }
        public DataSet GetGROUPNAME(string pGROUPNAME)
        {
            return GROUPSDP.Instance().GetGROUPNAME(pGROUPNAME);
        }
        public DataSet GetGroupID(int pGROUPID)
        {
            return GROUPSDP.Instance().GetGroupID(pGROUPID);
        }
        public DataSet GetGroup_IsSupervisor(string strUserID, string pGWTYPE)
        {
            return GROUPSDP.Instance().GetGroup_IsSupervisor(strUserID, pGWTYPE);
        }
        public DataSet GetGroup_Depart(string pGWTYPE)
        {
            return GROUPSDP.Instance().GetGroup_Depart(pGWTYPE);
        }
        public DataSet GetGroup_Gwtype(string pUserid)
        {
            return GROUPSDP.Instance().GetGroup_Gwtype(pUserid);
        }
	}
	
	
}
