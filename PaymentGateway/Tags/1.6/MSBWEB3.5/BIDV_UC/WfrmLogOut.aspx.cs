using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
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
    public partial class WfrmLogOut : System.Web.UI.Page
    {               
        clsUserRight objUser = new clsUserRight();
        private clsForm objForm = new clsForm();
        string strError = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            //Save log out
            if (objUser.SaveUserLog(DateTime.Now, "", "", 1, "Logout", "USERS", "", ""))
            {
                //Goi ham logout authentication cua form
                FormsAuthentication.SignOut();
                //Xoa cache
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Response.Buffer = true;
                Response.Expires = 0;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                Response.CacheControl = "no-cache";
                //this.Controls.Add(new LiteralControl(
                //    "<script language=javascript>" +
                //    "    window.parent.location='~/Default.aspx';" +
                //    "</script>"));

                //Xoa session
                Session.Clear();
                Session.Abandon();
                SessionHelper.Clear();
                Response.Redirect("~/Default.aspx");                                


                //this.Session.Abandon();
                //for (int i = 0; i < this.Response.Cookies.Count; i++)
                //{
                //    this.Response.Cookies[i].Expires = DateTime.Now;
                //}
                //for (int i = 0; i < this.Request.Cookies.Count; i++)
                //{
                //    this.Request.Cookies[i].Expires = DateTime.Now;
                //}
                //Response.Expires = 0;
                //FormsAuthentication.SignOut();
                //Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now - (new TimeSpan(24, 0, 0));
                //Request.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now - (new TimeSpan(24, 0, 0));
                //this.Response.Redirect("~/Default.aspx");
            }
            else
            {
                strError = objUser.strError;
                objForm.MessboxWeb(this.Page, strError);
                return;
            }
        }

        protected void Page_Init(object Sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        }

    }
}
