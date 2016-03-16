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
using BIDVWEB.Business.Reports;


namespace BIDVWEB.BIDV_UC.ViewReport
{
    public partial class WfrmReportList : System.Web.UI.Page
    {
        private clsForm objForm = new clsForm();
        //private string strError = "";
        private clsListReport objListReport = new clsListReport();


        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                if (!IsPostBack)
                {
                    if (Request["rt"] != null)
                    {
                        ms_InitReportList(Convert.ToInt16(Request["rt"]));
                    }                    
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        /////////////////////////////////////////////////////
        //Ten ham:      ms_InitReportList
        //Muc dich:     Hien thi danh sach bao cao cua cac kenh
        //Ngay tao:     07/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iTypeGW: Ma kenh thanh toan
        //Dau ra:       
        /////////////////////////////////////////////////////
        private void ms_InitReportList(int iTypeGW)
        {
            DataSet sv_dsData  = new DataSet();
            string strGWType = "";
                        
            sv_dsData = objListReport.GetListReport(iTypeGW, out strGWType);
            //lblCaption.Text = "Danh sách báo cáo";
            lblCaption.Text = "Danh sách báo cáo kênh " + strGWType;
            if (sv_dsData != null && sv_dsData.Tables[0].Rows.Count>0)
            {                
                grvData.DataSource = sv_dsData.Tables[0];                
                grvData.DataBind();
                lblError.Text = "";                
            }
            else
            {
                lblError.Text = "Không có báo cáo!";
            }            
        }





    }
}
