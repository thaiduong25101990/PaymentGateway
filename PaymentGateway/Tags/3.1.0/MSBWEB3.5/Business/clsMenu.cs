using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI.Design;
using System.Web.UI.Design.Util;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design.WebControls.ListControls;
using System.Web.UI.Design.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI.WebControls.WebParts;
using BIDVWEB.Comm;



namespace BIDVWEB.Business
{
    public class clsMenu
    {
        private string strError;
        public static string sMenutop;
        //private static Menu mnuTop;
        //private static Menu mnuLeft;
        //private static MenuItem mnuItemTop;
        //private static MenuItem mnuItemLeft;
        private clsForm objForm = new clsForm();
        private clsDatatAccess objDataAccess = new clsDatatAccess();        

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay du lieu menu top tu bang menu
        //Ngay tao:     05/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      /
        //Dau ra:       dataset
        ///////////////////////////////////////////////////////////////
        public DataSet dsGetDataMenuTop()
        {
            string str="";
            DataSet ds = new DataSet();           

            try
            {
                str = "SELECT MENUID,PARENTID,CAPTION,ASSEMBLY,METHOD,ENABLE,CTRL," + 
                "ALT,KEY,ORDERMENU,ISADMIN FROM MENU WHERE PARENTID='0000' AND " + 
                "ENABLE = 1 ORDER BY ORDERMENU";
                ds = objDataAccess.dsGetDataSourceByStr(str, "MENU");
                return ds;                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }        
        }


        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay du lieu menu left tu bang menu
        //Ngay tao:     05/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      /
        //Dau ra:       dataset
        ///////////////////////////////////////////////////////////////
        public DataSet dsGetDataMenuLeft(string strmnu, string strParentID, bool bBool)
        {
            string str = "";
            DataSet ds = new DataSet();            

            try
            {
                //Lay menu left cap 1
                if (bBool == true)
                {
                    str = "SELECT MENUID,PARENTID,CAPTION,ASSEMBLY,METHOD,ENABLE,CTRL," +
                    "ALT,KEY,ORDERMENU,ISADMIN FROM MENU WHERE CTRL=1 AND ALT = " + strmnu + 
                    " AND ENABLE = 1 ORDER BY ORDERMENU";
                }
                //Lay menu left cap 2
                else
                {
                    str = "SELECT MENUID,PARENTID,CAPTION,ASSEMBLY,METHOD,ENABLE,CTRL," +
                    "ALT,KEY,ORDERMENU,ISADMIN FROM MENU WHERE CTRL=2 AND PARENTID = " + 
                    strParentID + " AND ALT = " + strmnu + " AND ENABLE = 1 ORDER BY ORDERMENU";
                }
                ds = objDataAccess.dsGetDataSourceByStr(str, "MENU");
                return ds;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay menu top
        //Ngay tao:     05/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      mnutop:  Doi tuong menu
        //Dau ra:       Menu top
        ///////////////////////////////////////////////////////////////
        public Menu getMenuTop(Menu mnutop)
        {
            try
            {                
                DataSet sv_dsToolBar = new DataSet();             
                sv_dsToolBar = dsGetDataMenuTop();                                               
             
                mnutop.Items.Clear();
                foreach (DataRow sv_drRow in sv_dsToolBar.Tables[0].Rows)
                {
                    MenuItem mnuItem = new MenuItem();                    
                    mnuItem.Text = "&nbsp;&nbsp;&nbsp;" + sv_drRow["CAPTION"].ToString();
                    //mnuItem.NavigateUrl = new EventHandler("~/Common/frmList.aspx");
                    
                    //Menu khong co URL
                    if (sv_drRow["ASSEMBLY"].ToString() == "")
                    {                        
                        mnuItem.NavigateUrl = "~/Common/Main.aspx?mnu=" + sv_drRow["ORDERMENU"].ToString();                    
                    }
                    //Menu co URL
                    else
                    {
                        mnuItem.NavigateUrl = sv_drRow["ASSEMBLY"].ToString();
                    }                    
                    mnutop.Items.Add(mnuItem);
                }
                return mnutop;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }        
        }


        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay menu left
        //Ngay tao:     05/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      mnuleft: Doi tuong kieu menu
        //              strmnu: = 1: He thong, 2: Bao cao, 3: Thoat
        //Dau ra:       Menu Left
        ///////////////////////////////////////////////////////////////
        public Menu getMenuLeft(Menu mnuleft, string strmnu)
        {
            try
            {
                
                DataSet sv_dsToolBar = new DataSet();
                DataSet dsleft2 = new DataSet();

                sv_dsToolBar = dsGetDataMenuLeft(strmnu,"", true);

                mnuleft.Items.Clear();
                foreach (DataRow sv_drRow in sv_dsToolBar.Tables[0].Rows)
                {
                    MenuItem mnuItem = new MenuItem();
                    mnuItem.Text = "&nbsp;&nbsp;&nbsp;" + sv_drRow["CAPTION"].ToString();                                        
                    
                    //Them menu con
                    dsleft2 = dsGetDataMenuLeft(strmnu, sv_drRow["MENUID"].ToString(), false);
                    if (dsleft2.Tables[0].Rows.Count <= 0)
                    {
                        //mnuItem.NavigateUrl = sv_drRow["ASSEMBLY"].ToString();
                        //Kiem tra quyen                        
                        int iShow;
                        Button btnAdd = new Button();
                        Button btnSave = new Button();
                        Button btnDel = new Button();

                        objForm.PageLoad(sv_drRow["MENUID"].ToString(), btnAdd, btnSave,
                            btnDel, out iShow);
                        //Neu co quyen view
                        if (iShow > 0)
                        {
                            mnuItem.NavigateUrl = sv_drRow["ASSEMBLY"].ToString();
                        }
                        else
                        {
                            //mnuItem.NavigateUrl = "";
                            mnuItem.NavigateUrl = "~//Common/Main.aspx";
                        }
                    }
                    foreach (DataRow drRow2 in dsleft2.Tables[0].Rows)
                    {
                        MenuItem mnuItem2 = new MenuItem();
                        mnuItem2.Text = "&nbsp;&nbsp;" + drRow2["CAPTION"].ToString();
                        //Kiem tra quyen                        
                        int iShow;
                        Button btnAdd = new Button();
                        Button btnSave = new Button();
                        Button btnDel = new Button();

                        objForm.PageLoad(drRow2["MENUID"].ToString(), btnAdd, btnSave,
                            btnDel, out iShow);
                        //Neu co quyen view
                        if (iShow > 0)
                        {
                            mnuItem2.NavigateUrl = drRow2["ASSEMBLY"].ToString();
                        }
                        else
                        {
                            mnuItem2.NavigateUrl = "~//Common/Main.aspx";
                        }
                        mnuItem.ChildItems.Add(mnuItem2);
                    }
                    mnuleft.Items.Add(mnuItem);
                }
                return mnuleft;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }






    }
}
