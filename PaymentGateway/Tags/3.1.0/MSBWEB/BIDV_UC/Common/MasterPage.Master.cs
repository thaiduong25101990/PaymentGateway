using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using BIDVWEB.Business.UserRight;
using BIDVWEB.Business.Web;

namespace BIDVWEB.BIDV_UC.Common
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {        
        private clsMenu clsmnu = new clsMenu();
        private clsCommon objCommon = new clsCommon();
        private clsUser objUser = new clsUser();
        string strmnu = "";
        public string strError = "";

        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Load setupClientScript();
            //setupClientScript();            
            if (!Page.ClientScript.IsClientScriptBlockRegistered("ConfirmDeletion"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ConfirmDeletion",
                "<script language=JavaScript> function ConfirmDeletion() { " +
                "return confirm('Are you sure you wish to delete this record?'); " +
                " } </script>");
            }
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegisterClientScriptBlock", 
            // "document.write('RegisterClientScriptBlock');", true);

   

            

            //Lay thong tin UserID, UserName, Branch of UserID
            lblUser.Text = objUser.GetUID_UName_Branch();

            mnuTop = clsmnu.getMenuTop(mnuTop);
            strmnu = Request.QueryString["mnu"];
            if (strmnu!=null && strmnu != "")
            {
                SessionHelper.Store("mnu", strmnu);
                clsMenu.sMenutop = strmnu;
            }
            else
            {
                strmnu = SessionHelper.RetrieveWithDefault("mnu", "").ToString();
                clsMenu.sMenutop = strmnu;
            }
                                                            
            if (strmnu=="" || strmnu ==null)
            {
                
                strmnu = "1";
                objCommon.iMenutop = 1;
                mnuLeft = clsmnu.getMenuLeft(mnuLeft, strmnu.ToString());                
            }
            else
            {
                mnuLeft = clsmnu.getMenuLeft(mnuLeft, strmnu.ToString());
                objCommon.iMenutop = Convert.ToInt16(strmnu);                
            } 
        }

        //script
        private void SetupClientScript()
        {
            string js = @"
			<script language=JavaScript> 
			function ConfirmDeletion() 
			{
				return confirm('Are you sure you wish to delete this record?');
			}
			</script>";
            //Register the script
            if (!Page.ClientScript.IsClientScriptBlockRegistered("ConfirmDeletion"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ConfirmDeletion", js);
            }
        }
        
        
        
        
    }
}
