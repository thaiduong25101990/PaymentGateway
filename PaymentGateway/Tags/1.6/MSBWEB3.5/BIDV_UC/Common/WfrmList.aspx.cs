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


namespace BIDVWEB.BIDV_UC.Common
{
    public partial class WfrmList : System.Web.UI.Page
    {
        //HttpContext current;
        string strError = "";
        string mstrTable ="";               //Ten bang can thuc hien
        string mstrHyperlinkColumn ="";     //Ten cot hien thi HyperLink (Dung loc hien thi)
        string mstrURL ="";                 //Ten URL can truyen sang
        string mstrID_User ="";             //Ma nguoi dung
        int mintPageCount =0;               //So ban ghi ton tai trong 1 trang
        //string mstrSelected ="";          //Hien thi nut chon        
        string mstrIDName ="";              //Ten ID can hien thi
        string mstrSELECT ="";              //Luu tru bien Tim kiem              
        //Doi tuong search
        private clsSearchCommon objSearch = new clsSearchCommon();
        private clsDatatAccess obiDataAccess = new clsDatatAccess();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            string strWhere="";


            //if (current.User.Identity.Name == "")
            //{
            //    //break;
            //}
            //Page.RegisterHiddenField("__EVENTTARGET", "btnSearch");
            try
            {
                //Ten bang lay du lieu
                mstrTable = Request["Table"];
                if (mstrTable.Length == 0) 
                {
                    //Tạm thời
                    mstrTable = "USERS"; 
                }
                //Ten cot hien thi HyperLink
                mstrHyperlinkColumn = Request["Display"];
                if (mstrHyperlinkColumn.Length == 0)
                {   
                    mstrHyperlinkColumn = "ID";
                }
                //Ma nguoi dung
                mstrID_User = Request["ID_yUser"];
                //Ten URL can truyen sang
                mstrURL = Request["Hyperlink"];                
                if (mstrURL.Length == 0) 
                { 
                    mstrURL = "UserRight/WfrmUser.aspx";
                }
                //So ban hien thi tren trang
                if (Request["PageCount"] == "")
                {
                    mintPageCount = 20;
                }
                else
                {
                    mintPageCount = Convert.ToInt32(Request["PageCount"]);
                }                                             
                //ID
                mstrIDName =Request["IDName"];
                if (mstrIDName.Length == 0)
                {
                    mstrIDName = "ID";
                }                                
                //Lay bien SELECT cua trang tim kiem                
                //mstrSELECT = objSearch.BuildSelect(mstrTable, marrCaption, mstrID, mstrWidth, False, mstrPos, mstrFormatCurrency);                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }

            //Load form
            if (!IsPostBack)
            {
                lblCaption.Text = "";
                if (Convert.ToBoolean(Session["Report"]) != true)
                {
                    //string str ="";
                    //if (Request.QueryString["Table"].ToString() != "")
                    //{
                    //    if (Request.QueryString["Table"].IndexOf("(") > 0)
                    //    {
                    //        str = Request.QueryString["Table"].Substring(0, Request.QueryString["Table"].IndexOf("("));
                    //    }
                    //    else
                    //    {
                    //        str = Request.QueryString["Table"];
                    //    }
                    //}
                    //else
                    //{
                    //    str = "";
                    //}
                    ////if (!gobjUserRight.CheckPerr(current.User.Identity.Name, Session["arrFuncWithPer"], "", this.btnAdd, this.btnAdd, this.btnDel, str))
                    ////{    
                    ////    gwShowError("", "", true);
                    ////    break;
                    ////}
                }
                try
                {
                    lblError.Visible = false;
                    if (lblCaption.Text == null || lblCaption.Text=="") 
                    {
                        lblCaption.Text = objSearch.GetTableCaption(mstrTable);
                        lblCaption.Text = lblCaption.Text.ToUpper();
                        lblCaption.Text = "BIDVWEB - " + lblCaption.Text;
                    }
                    //Hiển thị Grid
                    ViewGrid(strWhere);                    
                    objSearch.FillToHtmlDdl(grvList, mstrTable, ddlField);
                    ddlOperator=objSearch.FillddlOperator(ddlOperator);
                    Session["bSearch"] = false;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;                    
                }
            }
        }


        ///////////////////////////////////////////////////////////////
        //Mo ta:        Lay du lieu hien thi du lieu len Datagrid
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strWhere: Chuoi dieu kien
        //Dau ra:       view du lieu thanh cong
        ///////////////////////////////////////////////////////////////
        private void ViewGrid(string strWhere)
        {
            string strSQL="";
            DataSet sv_dsData = new DataSet();
            int intRecordCount = 0;
            //int intX=0;
            //int intY=0;            
            //Utils objUtil As New Utils           

            //1. Tim kiem du lieu theo tieu chi bai toan 
            strSQL = mstrSELECT + strWhere;
            obiDataAccess.dsGetDataSourceByStr(strSQL, "");            
            //2. Xem view Grid                        
            //sv_dsData = objSearch.ViewGrid(grvList, marrCaption, strSQL, 
            //    mstrHyperlinkColumn, mstrURL, mintPageCount, mstrWidth, false, 
            //    mstrIDName, true , bDelete, Request("Table"), 
            //    "&ReportKind=" & Request("ReportKind"), 
            //    intRecordCount, mstrPos, mstrFormatCurrency, mstrID_User);           
            if (intRecordCount == 0)
            {
                this.lblRecordCount.Text = "Không có bản ghi nào";
            }
            else 
            {
                this.lblRecordCount.Text = "Tổng số bản ghi " + Convert.ToString(intRecordCount);
            }            
        }       
        

        //Ham set visible cua cac control
        private void ControlButton(Int16 intStatus)
        {
            switch (intStatus)
            {
                case 0:
                    btnAdd.Visible=false;
                    btnDel.Visible=false;
                    break;
                case 1:
                    btnAdd.Visible=true;
                    btnDel.Visible=true;
                    break;                
                default:
                    btnAdd.Visible=true;
                    btnDel.Visible=true;
                    break;
            }
        }







    }
}
