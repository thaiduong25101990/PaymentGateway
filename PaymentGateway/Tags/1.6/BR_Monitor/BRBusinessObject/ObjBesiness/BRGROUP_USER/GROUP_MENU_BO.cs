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
	public class GROUP_MENUController
	{
		
		public int AddGROUP_MENU(GROUP_MENUInfo objTable)
		{
			return GROUP_MENUDP.Instance().AddGROUP_MENU(objTable);
		}
		
		public int UpdateGROUP_MENU(GROUP_MENUInfo objTable)
		{
            return GROUP_MENUDP.Instance().UpdateGROUP_MENU(objTable);
		}

        public int DeleteGROUP_MENU(GROUP_MENUInfo objTable)
		{
            return GROUP_MENUDP.Instance().DeleteGROUP_MENU(objTable);
		}
        public DataSet GetMenu_group(string pGwtype)
        {
            return GROUP_MENUDP.Instance().GetMenu_group(pGwtype);
        }
        public DataSet GetMenu(string PGwtype)
        {
            return GROUP_MENUDP.Instance().GetMenu(PGwtype);
        }
        public DataSet GetMenu_treeview(string PGwtype,string Pparentid)
        {
            return GROUP_MENUDP.Instance().GetMenu_treeview(PGwtype, Pparentid);
        }

        public DataSet GetMenu_groupdd(string pMenuid,string pGroupname)
        {
            return GROUP_MENUDP.Instance().GetMenu_groupdd(pMenuid, pGroupname);
        }
        public DataSet CheckUserlogin(string pUserid,string pMenuid)
        {
            return GROUP_MENUDP.Instance().CheckUserlogin(pUserid, pMenuid);
        }

        public DataSet CheckEnable_Menu(string pUserid, string pMenuid,string pGWTYPE)
        {
            return GROUP_MENUDP.Instance().CheckEnable_Menu(pUserid, pMenuid, pGWTYPE);
        }
        //kiem tra xem user do duoc vao cac kenh thanh toan nao
        public DataTable Select_Gwtype(string pUserid)
        {
            return GROUP_MENUDP.Instance().Select_Gwtype(pUserid);
        }
        
	}
	
	
}
