using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.OracleClient;
using System.Data.Common;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using BIDVWEB.Comm; 


namespace BIDVWEB.Business
{
    public class clsSearchCommon
    {
        static string strError = "";
        static bool bError = false;
        static string mstrSQL = "";
        clsDatatAccess objDataAccess = new clsDatatAccess();

        ///////////////////////////////////////////////////////////////////
        //Mota:         Ham khoi tao
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////////
        //public void Nnew()
        //{
        //    this.Nnew();
        //}

        ///////////////////////////////////////////////////////////////////
        //Mota:         Insert cac toan tu vao dropdownlist
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      DdllistHtml: Control html
        //Dau ra:       Insert thanh cong        
        ///////////////////////////////////////////////////////////////////
        public DropDownList FillddlOperator(DropDownList DdllistHtml)
        {
            DdllistHtml.Items.Clear();            
            DdllistHtml.Items.Add(new ListItem(" LIKE", " LIKE"));
            DdllistHtml.Items.Add(new ListItem(" NOT LIKE", " NOT LIKE"));
            DdllistHtml.Items.Add(new ListItem(" >", " >"));
            DdllistHtml.Items.Add(new ListItem(" <", " <"));
            DdllistHtml.Items.Add(new ListItem(" =", " ="));
            DdllistHtml.Items.Add(new ListItem(" >=", " >="));
            DdllistHtml.Items.Add(new ListItem(" <=", " <="));
            DdllistHtml.Items.Add(new ListItem(" <>", " <>"));
            return DdllistHtml;
        }


        ///////////////////////////////////////////////////////////////////
        //Mota:         get ten bang du lieu
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strTable: Ten bang(khong phai ten mo ta tieng viet)
        //Dau ra:       Lay ra ten bang (ten tieng viet)       
        ///////////////////////////////////////////////////////////////////
        public string GetTableCaption(string strTable)
        {
            DataSet dsData =new DataSet();
            DataRow drData;
            string strTableCaption  = "";
            string tempTable="";
            if (strTable.IndexOf("(") > 0) 
            {
                tempTable = strTable.Substring(0, strTable.IndexOf("("));
            }
            else
            {
                tempTable = strTable;
            }

            mstrSQL = "SELECT SCAPTION FROM SYSTABLES WHERE ID LIKE '" + tempTable + "'";
            dsData = objDataAccess.dsGetDataSourceByStr(mstrSQL, strTable);            
            if (dsData.Tables[0].Rows.Count > 0)
            {
                drData = dsData.Tables[0].Rows[0];
                if (dsData.Tables[0].Rows[0]. Field<string>(0)!="" &&
                    dsData.Tables[0].Rows[0].Field<string>(0) !=null)
                //if (drData[0].ToString() != "" && drData[0].ToString() != null)
                {
                    strTableCaption = Convert.ToString(drData[0]);
                    //strTableCaption = dsData.Tables[0].Rows[0].Field<string>(0);
                }
            }
            else
            {
                strTableCaption = "Chua nhap ten bang";
            }            
            dsData = null;
            return strTableCaption;
        }


        ///////////////////////////////////////////////////////////////////
        //Mota:         hien thi du lieu tren Datagrid
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      StrTableName: Ten bang
        //              drDataGrid: Datagird
        //              DdllistHtml: Dropdownlist
        //Dau ra:       gan du lieu thanh cong
        ///////////////////////////////////////////////////////////////////
        //public void FillToHtmlDdl(DataGrid drDataGrid, string StrTableName, HtmlSelect DdllistHtml)
        public bool FillToHtmlDdl(GridView grvGrid, string StrTableName, DropDownList ddlField)
        {
            try
            {
                DataSet dsData = new DataSet();
                string Strsql = "";
                string strVar1 = "";
                string strVar2 = "";
                string tempTable = "";


                if (StrTableName.IndexOf("(") > 0)
                {
                    tempTable = StrTableName.Substring(0, StrTableName.IndexOf("("));
                }
                else
                {
                    tempTable = StrTableName;
                }

                Strsql = "SELECT PK_sColumn,sCaption FROM sys_Columns " + " WHERE FP_sTable = '" + tempTable + "' AND yOrder >0 Order by yOrder";
                dsData = objDataAccess.dsGetDataSourceByStr(Strsql, "");
                ddlField.Items.Clear();

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dsData.Tables[0].Rows.Count - 1; i++)
                    {
                        DataRow dr;
                        dr = dsData.Tables[0].Rows[i];
                        if (dr[1] == null)
                        {
                            strVar1 = "";
                        }
                        else
                        {
                            strVar1 = dr[1].ToString();
                        }
                        if (dr[0] == null)
                        {
                            strVar2 = "";
                        }
                        else
                        {
                            strVar2 = dr[0].ToString();
                        }
                        if (strVar1.Trim() == "")
                        {
                            strVar1 = strVar2;
                        }
                        for (int j = 0; j <= grvGrid.Columns.Count - 1; j++)
                        {
                            if ((grvGrid.Columns[j].HeaderText == strVar1) && (grvGrid.Columns[j].Visible == true))
                            {
                                ddlField.Items.Add(new ListItem(strVar1, strVar2));
                            }
                        }
                        ddlField.Items.Add(new ListItem(strVar1, strVar2));
                    }
                }
                bError = true;
            }
            catch(Exception ex)
            {
                strError = ex.Message;
                bError = false;
            }
            return bError;
        }


        public DataSet ViewGrid(GridView drDataGrid, Array[] arrCaption, string strSQL,
            string strHyperLinkColumn, string strURL, Int16 intPageCount, string StrWidth, 
            bool bArr, string strIDName, bool bShowLink, bool bDelete, string strTableName, 
            string sAddReQuest, Int16 intRecordCount, string strPosition, string strFormatCurrency,
            string mstrID_User)
        {
            //Dim drDataCaption As SqlDataReader;
            DataSet drDataset = new DataSet();
            ////HyperLinkColumn dtHp = new HyperLinkColumn();
            ////BoundColumn[] dtBoundColumn;
            ////BoundColumn dtBoundColumnID;        
            //////Dim arrCaption As New ArrayList
            ////Int16 intX;
            ////Int16 intY;
            ////Array arWidth;
            ////Array arrPos;
            ////Array arrFormatCurrency; //Mang format tien
            ////string strNumber  = "0000000000"; //Lưu trữ số         
            ////bool checkLastPage;
            ////arWidth = StrWidth.Split(",");        
            ////arrPos = strPosition.Split(",");
            ////arrFormatCurrency = strFormatCurrency.Split(",");


            
            //////0. Xu ly strSQL theo chi nhanh
            ////string strChildSQL;
            ////HttpContext current;
            ////current = HttpContext.Current();
            ////if (current.Session("UserName") != "Admin")
            ////{
            ////    if (current.Session("strWhereViewGrid") != "")
            ////    {
            ////        bool checksBranch ;         //Kiểm tra xem có trường sBranchCode trong bảng không
            ////        DataSet drDataReader = new DataSet();
            ////        string strSQLforCheck ="";

            ////        strSQLforCheck = "select count(*) from CO_sysColumns WHERE FP_sTable='" + 
            ////            strTableName + "' and PK_sColumn='sBranchCode'";
            ////        drDataReader = objDataAccess.dsGetDataSourceByStr(strSQLforCheck, "");
            ////        if (drDataReader.Tables[0].Rows.Count > 0 )
            ////        {
            ////                if (drDataReader.Tables[0].Rows[0](0) == 0)
            ////                {
            ////                    checksBranch = false;
            ////                }
            ////                else
            ////                {
            ////                    checksBranch = true;
            ////                }
            ////        }
            ////        drDataReader = null;
            ////        if (checksBranch )    //Nếu có trường sBranchCode thì phân quyền
            ////        {
            ////            if (strSQL.IndexOf("WHERE")>0) // Khi đã có điều kiện rồi
            ////            {
            ////                strSQL = strSQL + " AND " + current.Session("strWhereViewGrid");
            ////            }
            ////            else    // Khi chưa có điều kiệnk
            ////            {
            ////                strSQL = strSQL + " WHERE " + current.Session("strWhereViewGrid");
            ////            }
            ////        }
            ////    }
            ////}
            //////1. Lấy dữ liệu từ Database
            ////checkLastPage = false;
            ////if (drDataGrid.CurrentPageIndex = drDataGrid.PageCount - 1) { checkLastPage = true;}
            ////drDataset = objDataAccess.dsGetDataSourceByStr(strSQL, "");
            ////intRecordCount = drDataset.Tables[0].Rows.Count;
            ////if (checkLastPage && bDelete)
            ////{
            ////    if (drDataset.Tables[0].Rows.Count % intPageCount == 0)
            ////    {
            ////        if (drDataset.Tables[0].Rows.Count && drDataGrid.CurrentPageIndex > 0 )
            ////        {
            ////            drDataGrid.CurrentPageIndex = drDataGrid.CurrentPageIndex - 1;
            ////        }
            ////    }            
            ////}

            //////2. Gán dữ liệu vào Grid
            ////drDataGrid.Height.Pixel(20);
            //////Tự tạo cột động
            ////drDataGrid.AutoGenerateColumns = false;
            //////Xử lý cột ID
            ////drDataGrid.Columns(1).Visible = false;
            //////Tạo cột HyperLink
            ////if (strHyperLinkColumn.Length > 0 )
            ////{
            ////    //Trường hợp riêng của TuanDM khi hyperlink là cột mức lương tối thiểu trong danh mục đơn giản
            ////    if (strHyperLinkColumn = "cMoney" && drDataGrid.ID = "dgChiTietDanhMuc")
            ////    {
            ////        dtHp.DataTextFormatString = "{0:###,###,##0.00}";
            ////    }            
            ////    //Xac dinh vi tri sap xep
            ////    if (arrPos.Length > 1)
            ////    {
            ////        if (strHyperLinkColumn != "ID")
            ////        {
            ////            if (arrPos.GetValue(1) == "C")
            ////            {
            ////                dtHp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            ////            }
            ////        }
            ////        else
            ////        {
            ////            if (arrPos.GetValue(0) == "C")
            ////            {
            ////                dtHp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            ////            }
            ////        }
            ////    }
                
            ////    //Nếu trường link có kiểu ngày thì format
            ////    if (strHyperLinkColumn.Chars(0) == "t")
            ////    {
            ////        dtHp.DataTextFormatString = "{0:dd/MM/yyyy}";
            ////    }
            ////    dtHp.DataTextField = strHyperLinkColumn;
            ////    if (mstrID_User != "")
            ////    {
            ////        dtHp.DataNavigateUrlFormatString = strURL + "?" + strIDName + "={0}" +
            ////            sAddReQuest + "&ID_yUser=" + mstrID_User;
            ////    }
            ////    else
            ////    {
            ////        dtHp.DataNavigateUrlFormatString = strURL + "?" + strIDName + "={0}" + sAddReQuest;
            ////    }
            ////    dtHp.DataNavigateUrlField = "ID";
            ////    dtHp.HeaderText = "";
            ////    drDataGrid.Columns.Add(dtHp);
            ////    dtHp.Visible = bShowLink;
            ////}
            //////Hiển thị các cột ràng buộc
            ////intY = 0;
            ////Int16 intTmp =0;
            ////Int16 dtBoundColumn[drDataset.Tables[0].Columns.Count - 1];
            ////for (intX = 1; intx <=drDataset.Tables[0].Columns.Count - 1; intX++)
            ////{
            ////    if (drDataset.Tables[0].Columns(intX).ColumnName == "ID" || 
            ////        drDataset.Tables[0].Columns(intX).ColumnName == strHyperLinkColumn)
            ////    {
            ////        //Tru cot ID va cot de hien thi HyperLink
            ////        dtBoundColumn(intY) = new BoundColumn();
            ////        dtBoundColumn(intY).DataField = drDataset.Tables(0).Columns(intX).ColumnName;
            ////        string sWidth;
            ////        if (arWidth.Length <= 1 )
            ////        {
            ////            sWidth = 100;
            ////        }
            ////        else
            ////        {
            ////            sWidth = arWidth.GetValue(intX);
            ////        }
            ////        if (sWidth = null || Convert.ToInt16(sWidth)>0)
            ////        {
            ////            sWidth = 100;
            ////        }
            ////        dtBoundColumn(intY).ItemStyle.Width = System.Web.UI.WebControls.Unit.Pixel(sWidth);
            ////        switch( drDataset.Tables("TblTmp").Columns(intX).DataType.FullName)
            ////        {
            ////            case "System.DateTime":
            ////                dtBoundColumn(intY).DataFormatString = "{0:dd/MM/yyyy}";                 
            ////                dtBoundColumn(intY).ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ////                break;
            ////            case "System.Decimal":
            ////                dtBoundColumn(intY).DataFormatString = "{0:###,###,###,##0}";                
            ////                if (arrFormatCurrency.Length > 1)
            ////                {
            ////                    if (arrFormatCurrency.GetValue(intX) != "0")
            ////                    {
            ////                        dtBoundColumn(intY).DataFormatString = "{0:###,###,##0." +
            ////                            strNumber.Substring(1, CInt(arrFormatCurrency.GetValue(intX))) + "}";
            ////                    }
            ////                }
            ////                dtBoundColumn(intY).ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ////                break;
            ////            case "System.Int16":
            ////                dtBoundColumn(intY).ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ////                break;
            ////            case "System.Int32":
            ////                dtBoundColumn(intY).ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ////                break;
            ////            case "System.Integer":
            ////                dtBoundColumn(intY).ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ////                break;
            ////        }
            ////        //Xac dinh vi tri sap xep
            ////        if (arrPos.Length > 1 )
            ////        {
            ////            if (arrPos.GetValue(intX) == "C")
            ////            {
            ////                dtBoundColumn(intY).ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            ////            }
            ////        }
            ////        drDataGrid.Columns.Add(dtBoundColumn(intY));
            ////        intY = intY + 1;
            ////    }
            ////    else
            ////    {
            ////        intTmp = intX;
            ////    }
            ////}
            //////Xử lý caption bằng mảng
            ////for (intX = 0;intX <= arrCaption.Count - 1; intX++)
            ////{
            ////    if (arrCaption.Count == drDataGrid.Columns.Count - 2)
            ////    {
            ////        if (intX == (drDataGrid.Columns.Count - 2 )) 
            ////        { 
            ////            break;
            ////        }
            ////    }
            ////    if (intX == drDataGrid.Columns.Count - 2) 
            ////    { 
            ////        break;
            ////    }
            ////    if (intX < intTmp)
            ////    {
            ////        if (strHyperLinkColumn != "ID" )
            ////        {
            ////            drDataGrid.Columns(intX + 2).HeaderText = arrCaption.Item(intX);
            ////        }
            ////        else
            ////        {
            ////            drDataGrid.Columns(intX + 2).HeaderText = arrCaption.Item(intX) + "";
            ////        }
            ////        drDataGrid.Columns(2).HeaderText = arrCaption.Item(1);
            ////    }
            ////    else
            ////    {
            ////        if (strHyperLinkColumn != "ID" )
            ////        {
            ////            drDataGrid.Columns(intX + 2).HeaderText = arrCaption.Item(intX + 1) ;
            ////            drDataGrid.Columns(2).HeaderText = arrCaption.Item(1);
            ////        }
            ////        else
            ////        {
            ////            drDataGrid.Columns(intX + 2).HeaderText = arrCaption.Item(intX);
            ////        }
            ////    }
            ////}
            //////Số bản ghi hiển thị theo trang và ràng buộc dữ liệu
            ////if (intPageCount > 0)
            ////{
            ////    drDataGrid.PageSize = intPageCount;
            ////    drDataGrid.AllowPaging = True;
            ////    drDataGrid.PagerStyle.Mode = PagerMode.NumericPages;
            ////}
            ////drDataGrid.DataSource = drDataset.Tables[0];
            ////drDataGrid.DataBind();            
            return drDataset;
        
        }

    

    }
}
