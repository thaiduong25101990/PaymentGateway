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

namespace BIDVWEB.BIDV_UC.UserRight
{
    public partial class WfrmSetFunctionToUser : System.Web.UI.Page
    {
        private clsUserRight objUser = new clsUserRight();
        private clsDatatAccess objDataAccess = new clsDatatAccess();        
        private clsForm objForm = new clsForm();
        private string strError = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int iShow;
                    Button btnAdd = new Button();
                    Button btnDel = new Button();
                    string sMenuID = "";
                    sMenuID = Request["mn"];
                    lblError.Text = "";
                    objForm.PageLoad(sMenuID, btnAdd, btnSave, btnDel, out iShow);
                    if (iShow > 0)
                    {
                        if (!LoadData())
                        {
                            objForm.MessboxWeb(this.Page, strError);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        //Load Data/////////////////////////////////////////////////
        // Muc dich:    Load du lieu
        // Ngay tao:    05/2008
        // Nguoi tao:   Huypq7
        // Dau vao:              
        // Dau ra:      Load du lieu thanh cong
        /////////////////////////////////////////////////////////////
        private bool LoadData()
        {
            try
            {
                DropDownList ddlList;

                lblError.Text = "";
                //Load nhom nguoi su dung
                ddlList = objDataAccess.FillDataToDropDownList("USERS","",
                    ddlUser, "USERNAME", "USERID", "USERNAME ASC", false);
                if (ddlList != null)
                {
                    ddlUser = ddlList;
                }
                else
                {
                    strError = "Chưa nhập nhóm!";
                }
                //View danh sach user
                if (!ViewGrid())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        //Ham view thong tin user/////////////////////////////////////
        //Mo ta:        Ham view thong tin user
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool ViewGrid()
        {
            try
            {
                DataSet sv_dsData = new DataSet();

                sv_dsData = objUser.GetAllMenuForUser(Convert.ToString(ddlUser.SelectedValue));
                if (sv_dsData == null && sv_dsData.Tables[0].Rows.Count <= 0)
                {
                    strError = objUser.strError;
                    return false;
                }                
                grvData.DataSource = sv_dsData.Tables[0];
                grvData.DataBind();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiem tra du lieu truoc khi cap nhat
                if (CheckData())
                {
                    //Kiem tra theo tung ban ghi da chon
                    string sUserID = Convert.ToString(ddlUser.SelectedValue);
                    string sMenuID;

                    for (int i = 0; i <= grvData.Rows.Count - 1; i++)
                    {
                        CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];
                        if (chkbox.Checked == true)
                        {
                            TextBox txtPer = (TextBox)grvData.Rows[i].Cells[4].Controls[1];
                            sMenuID = Convert.ToString(grvData.Rows[i].Cells[1].Text);
                            if (txtPer.Text.ToString() != "")
                            {
                                //Them moi
                                if (objUser.CheckPermissionUser(sUserID,sMenuID))
                                {
                                    if (!objUser.AddPermissionUser(sUserID, sMenuID, txtPer.Text.ToString(),true))
                                    {
                                        strError = objUser.strError;
                                        break;
                                    }
                                }
                                //Sua
                                else
                                {
                                    if (!objUser.AddPermissionUser(sUserID, sMenuID, txtPer.Text.ToString(),false))
                                    {
                                        strError = objUser.strError;
                                        break;
                                    }                                    
                                }
                            }

                        }
                    }
                }
                objForm.MessboxWeb(this.Page, strError);
                ViewGrid();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objForm.MessboxWeb(this.Page, strError);
            }
        }


        //Kiem tra du lieu truoc khi cap nhat//////////////////////////
        //Mo ta:        Kiem tra du lieu truoc khi cap nhat
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      iUserID: Ma NSD
        //Dau ra:       View thanh cong        
        ///////////////////////////////////////////////////////////////
        private bool CheckData()
        {
            if (this.ddlUser.Items.Count <= 0)
            {
                strError = "Nhập nhóm!";
                return false;
            }            
            for (int i = 0; i <= grvData.Rows.Count - 1; i++)
            {
                CheckBox chkbox = (CheckBox)grvData.Rows[i].Cells[0].Controls[1];                
                if (chkbox.Checked == true)
                {
                    TextBox txtPer = (TextBox)grvData.Rows[i].Cells[4].Controls[1];
                    if (txtPer.Text.ToString() != "")
                    {
                        if (Convert.ToString(txtPer.Text) == "0" ||
                            Convert.ToString(txtPer.Text) == "1" ||
                            Convert.ToString(txtPer.Text) == "2" ||
                            Convert.ToString(txtPer.Text) == "3")
                        { 
                        }
                        else
                        {
                            i=i+1;
                            strError = "Nhập số cho dòng " + i + "!";
                            return false;
                        }
                    }
                    
                }
            }
            return true;
        }


        //Checkbox trong Gridview//////////////////////////////////////
        //Mo ta:        Checkbox trong Gridview
        //Ngay tao:     06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       
        ///////////////////////////////////////////////////////////////
        protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            Response.Write(row.Cells[0].Text);
        }


        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewGrid();
        }













    }
}
