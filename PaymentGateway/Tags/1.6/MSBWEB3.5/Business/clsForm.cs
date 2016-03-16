using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BIDVWEB.Business.UserRight;

namespace BIDVWEB.Business
{
    public class clsForm
    {
        //Lop user
        private clsUserRight objUser = new clsUserRight();
        public string strError = "";

        // MessboxWeb ////////////////////////////////////////////////
        // Muc dich:    MessboxWeb
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     strPage: ten trang co loi
        //              strMessage: Noi dung loi        
        // Dau ra:      Dua ra thong bao loi dang messagebox
        /////////////////////////////////////////////////////////////
        public void MessboxWeb(System.Web.UI.Page strPage, string strMessage)
        {
            if (!string.IsNullOrEmpty(strMessage))
            {
                strPage.Controls.Add(new LiteralControl(
                        "<script>window.alert('" + strMessage + "')</script>"));
            }
            else
                return;
        }

        // Page Load ////////////////////////////////////////////////
        // Muc dich:    Page Load
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:     sMenuID: MenuID
        //              btnAdd: Nut them moi
        //              btnSave: Nut ghi
        //              btnDel: Nut xoa
        //              iShow: = 1 : Hien thi du lieu
        //                     = 0 : Khong hien thi 
        // Dau ra:      
        /////////////////////////////////////////////////////////////
        public void PageLoad(string sMenuID, Button btnAdd, Button btnSave, 
            Button btnDel, out int iShow)
        {
            iShow = 0;
            try
            {
                int iInquiry = 0;
                int iDelete = 0;
                int iInsert = 0;
                int iEdit = 0;
                                
                strError = "";
                if (!objUser.CheckPerForm(sMenuID, out iInquiry,
                    out iDelete, out iInsert, out iEdit))
                {
                    strError = objUser.strError;
                }
                iShow = iInquiry;
                if (iDelete == 0)
                {
                    btnDel.Enabled = false;
                }
                if (iInsert == 0)
                {
                    btnAdd.Enabled = false;
                    btnSave.Enabled = false;
                }
                if (iEdit == 0)
                {
                    if (iInsert == 0)
                    {
                        btnSave.Enabled = false;
                    }                    
                }
            }
            catch (Exception ex)
            {                
                strError = ex.Message;
            }
        }
    }
}
