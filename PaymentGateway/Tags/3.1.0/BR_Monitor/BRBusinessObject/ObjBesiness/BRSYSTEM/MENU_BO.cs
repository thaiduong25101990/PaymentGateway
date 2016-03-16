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
	public class MENUController
	{
		
		public int AddMENU(MENUInfo objTable)
		{
			return MENUDP.Instance().AddMENU(objTable);
		}
		
		public int UpdateMENU(MENUInfo objTable)
		{
			return MENUDP.Instance().UpdateMENU(objTable);
		}
		
		public int DeleteMENU(string MENUID)
		{
			return MENUDP.Instance().DeleteMENU(MENUID);
		}

        public DataSet GetMENU(string iGroupid,string pGwtype)
        {
            return MENUDP.Instance().GetMENU(iGroupid, pGwtype);
        }
        public DataSet GetMENU1(string iGroupid)
        {
            return MENUDP.Instance().GetMENU1(iGroupid);
        }
        public DataSet GetMENU1()
        {
            return MENUDP.Instance().GetMENU1();
        }
        public DataSet GetMenuid(string pMenuid)
        {
            return MENUDP.Instance().GetMenuid(pMenuid);
        }
        public DataSet GetMenuid_Gwtype(string pCaption)
        {
            return MENUDP.Instance().GetMenuid_Gwtype(pCaption);
        }
        public DataSet GetMenuid_Gwtype_TYPE(string pCaption, string strGWTYPE)
        {
            return MENUDP.Instance().GetMenuid_Gwtype_TYPE(pCaption, strGWTYPE);
        }
        public DataSet GetMenuid_Gwtype_TYPE1(string pCaption, string strGWTYPE,string strMenuID)
        {
            return MENUDP.Instance().GetMenuid_Gwtype_TYPE1(pCaption, strGWTYPE, strMenuID);
        }
        public DataSet GetMenu_MenuID(string pMENUID)
        {
            return MENUDP.Instance().GetMenu_MenuID(pMENUID);
        }
        public DataTable Menu_get(string pParentid)
        {
            return MENUDP.Instance().Menu_get(pParentid);
        }
        public DataTable select_menuid()
        {
            return MENUDP.Instance().select_menuid();
        }

        public int Check_delete(string pMenuid)
        {
            return MENUDP.Instance().Check_delete(pMenuid);
        }
        public int Check_input(string pParentid)
        {
            return MENUDP.Instance().Check_input(pParentid);
        }

        public int Check_inputs(string pMenuid)
        {
            return MENUDP.Instance().Check_inputs(pMenuid);
        }

        public int Check_inputst(string pAssembly)
        {
            return MENUDP.Instance().Check_inputst(pAssembly);
        }
        public DataTable Menu_data(string pMenuid)
        {
            return MENUDP.Instance().Menu_data(pMenuid);
        }
    }
	
	
}
