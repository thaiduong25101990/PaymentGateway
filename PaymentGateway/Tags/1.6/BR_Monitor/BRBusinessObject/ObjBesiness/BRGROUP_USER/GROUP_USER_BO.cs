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
	public class GROUP_USERController
	{
		
		public int AddGROUP_USER(GROUP_USERInfo objTable)
		{
			return GROUP_USERDP.Instance().AddGROUP_USER(objTable);
		}
		
		public int UpdateGROUP_USER(GROUP_USERInfo objTable)
		{
			return GROUP_USERDP.Instance().UpdateGROUP_USER(objTable);
		}

        public int DeleteGROUP_USER( int pGROUPID,string pUSERID)
        {
            return GROUP_USERDP.Instance().DeleteGROUP_USER(pGROUPID, pUSERID);
        }
        public int DeleteGroupUser(string pUSERID)
        {
            return GROUP_USERDP.Instance().DeleteGroupUser( pUSERID);
        }
        public DataSet GetGroup_user(int userid)
        {
            return GROUP_USERDP.Instance().GetGROUP_USER(userid);
        }
        public DataSet GetGroup_userDD(int  GroupID,string Userid)
        {
            return GROUP_USERDP.Instance().GetGroup_userDD(GroupID, Userid);
        }
        public DataSet GetGROUPDATA(int pGROUPID, string pUSERID)
        {
            return GROUP_USERDP.Instance().GetGROUPDATA(pGROUPID, pUSERID);
        }
        public DataSet GetGroup_Gwtype(string pGwtype)
        {
            return GROUP_USERDP.Instance().GetGroup_Gwtype(pGwtype);
        }
        public DataSet GetGroup_Gwtype1(string pGwtype)
        {
            return GROUP_USERDP.Instance().GetGroup_Gwtype1(pGwtype);
        }
	
	}
	
	
}
