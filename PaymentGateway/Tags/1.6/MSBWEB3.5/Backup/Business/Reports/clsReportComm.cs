using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BIDVWEB.Comm;
using BIDVWEB.Business;
using BIDVWEB.Business.Reports;

namespace BIDVWEB.Business.Reports
{
    public class clsReportComm
    {
        protected clsDatatAccess objDataAccess = new clsDatatAccess();
        private clsReport objReport = new clsReport();
        public string strError = "";


        //Ham lay danh sach control form dieu kien bao cao/////////////
        //Mo ta:        Ham lay danh sach control form dieu kien bao cao
        //Ngay tao:     07/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strRN: Ten bao cao
        //Dau ra:       Tra ra dataset chua danh sach control
        ///////////////////////////////////////////////////////////////
        public DataSet GetCtlReport( string strRN)
        {
            try
            {
                string strSql = "";
                DataSet ds = new DataSet();
                ds = null;

                strSql = "SELECT * FROM LIST_REPORT_PARA_CTL WHERE REPORTNAME = '" + 
                          strRN + "' ORDER BY IORDER ";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");                
                return ds;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }


        //Ham lay parameter cursor cho sp//////////////////////////////
        //Mo ta:        Ham lay parameter cursor cho sp
        //Ngay tao:     07/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strRN: Ten bao cao
        //Dau ra:       Tra ra ten cursor
        ///////////////////////////////////////////////////////////////
        public string GetParaCursor(string strRN)
        {
            try
            {
                string strSql = "";
                string strCursor = "";
                DataSet ds = new DataSet();
                DataRow dr;

                strSql = "SELECT * FROM LIST_REPORT_PARA_SP WHERE REPORTNAME = '" +
                          strRN + "' AND ISSHOW =1 ORDER BY IORDER ";
                ds = objDataAccess.dsGetDataSourceByStr(strSql, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                    strCursor = dr["PARA_NAME"].ToString();
                }
                return strCursor;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }

        //Ham add htmltablerow/////////////////////////////////////////////
        //Mo ta:        Ham add htmltablerow
        //Ngay tao:     07/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iRows: So dong can them
        //              tblMain: Ten htmltable can them
        //Dau ra:       Tra ra htmltable can them
        ///////////////////////////////////////////////////////////////
        public void AddTableRow(int iRows, HtmlTable tblMain)
        {
            for (int i = 1; i <= iRows; i++)
            {
                //Tao them table row
                HtmlTableRow trNew = new HtmlTableRow();
                //Tao them table cell
                HtmlTableCell tcell1 = new HtmlTableCell();
                trNew.Cells.Add(tcell1);
                HtmlTableCell tcell2 = new HtmlTableCell();
                trNew.Cells.Add(tcell2);
                HtmlTableCell tcell3 = new HtmlTableCell();
                trNew.Cells.Add(tcell3);
                HtmlTableCell tcell4 = new HtmlTableCell();
                trNew.Cells.Add(tcell4);
                tblMain.Rows.Add(trNew);
            }
        }

        //Ham lay tieu de bao cao///////////////////////////////////////
        //Mo ta:        Ham lay tieu de bao cao
        //Ngay tao:     07/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      lblCaption: label gan tieu de bao cao
        //              tblMain: Ten htmltable can them
        //Dau ra:       Tra ra tieu de bao cao gan voi lblCaption
        ///////////////////////////////////////////////////////////////
        public void GetReportTitle(Label lblCaption, string strRN)
        {
            if (strRN != "" && strRN != null)
            {
                lblCaption.Text = objReport.GetTitleReport(strRN,true);
            }
            else
            {
                lblCaption.Text = "";
            }
        }

        //private  AddControl(int count)
        //{

        //    HtmlTable tbl = new HtmlTable();
        //    HtmlTableRow r = new HtmlTableRow();
        //    HtmlTableCell c = new HtmlTableCell();

        //    tbl.BgColor = "WhiteSmoke";

        //    r.Cells.Add(c);
        //    c = new HtmlTableCell();
        //    r.Cells.Add(c);
        //    c = new HtmlTableCell();
        //    r.Cells.Add(c);
        //    c = new HtmlTableCell();
        //    r.Cells.Add(c);
        //    c = new HtmlTableCell();
        //    r.Cells.Add(c);
        //    tbl.Rows.Add(r);        
        //    Panel p = CHECKBOX_PLACE(count);
        //    TextBox name = new TextBox();
        //    name.ID = "name" + count;
        //    name.AutoPostBack = false;
        //    HtmlInputText password = new HtmlInputText("password");
        //    password.ID = "password" + count;
        //    DropDownList designation = new DropDownList();
        //    designation.ID = "designation" + count;
        //    for (int i = 0; i < desgDSET.Length; i++)
        //        designation.Items.Add(desgDSET[i]);
        //    CheckBox assign = new CheckBox();
        //    assign.ID = "" + count;
        //    assign.CheckedChanged += new System.EventHandler(this.ASSIGN_CHECKED);

        //    tbl.Rows[0].Cells[0].Controls.Add(name);
        //    tbl.Rows[0].Cells[1].Controls.Add(password);
        //    tbl.Rows[0].Cells[2].Controls.Add(designation);
        //    tbl.Rows[0].Cells[3].Controls.Add(p);

        //    Panel subPanel = new Panel();
        //    subPanel.ID = "subPanel" + count;
        //    subPanel.Controls.Add(tbl);
        //    return subPanel;

        //}
    }
}
