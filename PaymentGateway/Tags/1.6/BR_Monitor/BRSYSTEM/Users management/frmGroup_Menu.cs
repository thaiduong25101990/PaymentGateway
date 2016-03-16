using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Text.RegularExpressions;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class frmGroup_Menu : frmBasedata
    {
        #region khai bao ham va tham so
        public int iEdit = 0;
        public static string strNodename;

        private GROUPSInfo objgroup = new GROUPSInfo();
        private GetData objGetData = new GetData();
        private GROUPSController objcontrolGroup = new GROUPSController();
        private GROUP_MENUInfo objgroup_menu = new GROUP_MENUInfo();
        private GROUP_MENUController objcontrolGroup_menu = new GROUP_MENUController();
        private MENUInfo objmenu = new MENUInfo();
        private MENUController objcotrolMenu = new MENUController();
        private GWTYPEInfo objGwtype = new GWTYPEInfo();
        private GWTYPEController objctrolGwtype = new GWTYPEController();
        
        private string strMenuid;
        
        #endregion

        public frmGroup_Menu()
        {
            InitializeComponent();
        }

        private void frmGroup_Menu_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            trvmenu.ForeColor = Color.Black;
            iInquiry = Common.iSelect;
            cmdDelete.Visible = false;
            //GetdataCombobox();
            if (!objGetData.FillDataComboBox(cboDepart, "GWTYPE", "GWTYPE", "GWTYPE",
                "", "GWTYPE", true, false, ""))
                return;
        }
        

        /*---------------------------------------------------------------
         * Method           : MenuItem(TreeNode trvmenu, string strParentID)
         * Muc dich         : Ham de add cac menu vao cac node tren treeview
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void MenuItem(TreeNode trvmenu, string strParentID)
        {
            try
            {
                string strGwtype=cboDepart.Text;
                TreeNode[] parent = new TreeNode[100];                
                DataSet datMenu = new DataSet();
                datMenu = objcontrolGroup_menu.GetMenu_treeview(strGwtype, strParentID);
                int b = 0;
                while (b < datMenu.Tables[0].Rows.Count)
                {
                    string strCaption = datMenu.Tables[0].Rows[b]["CAPTION"].ToString();
                    string strMenuid = datMenu.Tables[0].Rows[b]["MENUID"].ToString();
                    int Key = Convert.ToInt32(strMenuid);
                    parent[b] = new TreeNode(strCaption);
                    parent[b].Name = strMenuid;
                    trvmenu.Nodes.Add(parent[b]);                    
                    Submenu(parent[b], strMenuid);
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : Submenu(TreeNode Node, string strParent)
         * Muc dich         : Ham de add cac menu vao cac node tren treeview
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Submenu(TreeNode Node, string strParent)
        {
            try
            {
                string strGwtype1 = cboDepart.Text;
                TreeNode[] parent = new TreeNode[100];
                DataSet datNode = new DataSet();
                datNode = objcontrolGroup_menu.GetMenu_treeview(strGwtype1, strParent);
                int f = 0;
                while (f < datNode.Tables[0].Rows.Count)
                {
                    string strCap = datNode.Tables[0].Rows[f]["CAPTION"].ToString();
                    string strMe = datNode.Tables[0].Rows[f]["MENUID"].ToString();
                    parent[f] = new TreeNode(strCap);
                    parent[f].Name = strMe;
                    Node.Nodes.Add(parent[f]);                                       
                    MenuItem(parent[f], strMe);
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : Submenu(TreeNode Node, string strParent)
         * Muc dich         : Ham add menu "Menu" len node cua treeview dau tien sau do goi sang cac ham con
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void GetdataMenu()
        {

            try
            {               
                trvmenu.Nodes.Clear();               
                TreeNode[] parent = new TreeNode[100];
                parent[1] = new TreeNode("Menu");
                trvmenu.Nodes.Add(parent[1]);
                MenuItem(parent[1], "0000");
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : GetdataGroup(string strGwtype)
         * Muc dich         : Goi cac group thuoc kenh toan duoc chon o combobox vao luoi
         * Tham so          : strGwtype(ten cua kenh thanh toan)
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void GetdataGroup(string strGwtype)
        {
            dtgGroup.Columns.Clear();
            try
            {
                DataSet datGroup = new DataSet();
                datGroup = objcontrolGroup.GetGroup_Depart(strGwtype);
                int i = 0;
                while (i < datGroup.Tables[0].Rows.Count)
                {
                    DataGridViewCheckBoxColumn column0 = new DataGridViewCheckBoxColumn();
                    string strGroupname = datGroup.Tables[0].Rows[i]["GROUPNAME"].ToString();
                    
                    column0.Name = strGroupname;
                    //column1.Name = "DisableCheckBoxes";
                    dtgGroup.Columns.Add(column0);
                    dtgGroup.Columns[i].Width = 100;
                    //dataGridView1.Columns.Add(column1);

                    dtgGroup.RowCount = 4;
                    // doan ma tao ten dau de
                    dtgGroup.Rows[0].HeaderCell.Value = "Inquiry";
                    dtgGroup.Rows[1].HeaderCell.Value = "Add";
                    dtgGroup.Rows[2].HeaderCell.Value = "Edit";
                    dtgGroup.Rows[3].HeaderCell.Value = "Delete";
                    dtgGroup.Rows[0].ReadOnly = true;
                    dtgGroup.Rows[1].ReadOnly = true;
                    dtgGroup.Rows[2].ReadOnly = true;
                    dtgGroup.Rows[3].ReadOnly = true;                    
                    //---------------------------------
                    dtgGroup.AutoSize = false;
                    dtgGroup.AllowUserToAddRows = false;
                    dtgGroup.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    i = i + 1;
                }
                //if (datGroup.Tables[0].Rows.Count > 0)
                //{
                //    dtgGroup.Rows[1].Visible = false;
                //    dtgGroup.Rows[2].Visible = false;
                //    dtgGroup.Rows[3].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*---------------------------------------------------------------
        * Muc dich         : khi lua chon cac kenh thanh toan
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 05/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cboDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCombo = cboDepart.Text;
            cmdEdit.Enabled = false;
            GetdataGroup(strCombo);
            GetdataMenu();
        }
        /*---------------------------------------------------------------
        * Muc dich         : ghi du lieu vao trong bang GROUP_MENU trang thai Insert,Update,Delete
        * Tra ve           : Mot danh sach cac File - List<FileInfo>
        * Ngay tao         : 26/54/2008
        * Nguoi tao        : Quynd
        * Ngay cap nhat    : 05/06/2008
        * Nguoi cap nhat   : Quynd
        *--------------------------------------------------------------*/
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {    
                iEdit = 0;
                    //kiem tra xem co phai click vao node co ten "Menu " khong
                    if (strNodename == "Menu")
                    {
                        
                    }
                    else
                    {
                        if (Common.iSconfirm == 1)
                        {
                            //DataSet datMenu = new DataSet();
                            //datMenu = objcotrolMenu.GetMenuid(strNodename);
                            //string strMenuid = datMenu.Tables[0].Rows[0]["MENUID"].ToString();//lay ra thong tin menuid de khi add vao bang
                            //objgroup_menu.MENUID = strMenuid;
                            Save_data(strMenuid);//ghi du lieu
                            Sub_Group_menu(strMenuid);
                        }
                        else
                        {
                        }
                    }
                    trvmenu.Enabled = true;
                    cboDepart.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            cmdEdit.Enabled = true;
            GetdataGroup(cboDepart.Text);
            LoadData();
            trvmenu.SelectedNode.ForeColor = Color.Black;
            trvmenu.Focus();
        }
        //kiem tra cac menu con xem co khong thi ghi du lieu
        private void Sub_Group_menu(string MenuID)
        {
            try
            {
                DataSet datMenu = new DataSet();
                datMenu = objcontrolGroup_menu.GetMenu_treeview(cboDepart.Text.Trim(), MenuID);
                int b = 0;
                while (b < datMenu.Tables[0].Rows.Count)
                {
                    string pMenuid = datMenu.Tables[0].Rows[b]["MENUID"].ToString();
                    objgroup_menu.MENUID = pMenuid;
                    Save_data(pMenuid);
                    TSub_Group_menu(pMenuid);
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //kiem tra cac quyen nho hon
        private void TSub_Group_menu(string MenuID)
        {
            try
            {
                DataSet datNode = new DataSet();
                datNode = objcontrolGroup_menu.GetMenu_treeview(cboDepart.Text.Trim(), MenuID.Trim());
                int f = 0;
                while (f < datNode.Tables[0].Rows.Count)
                {
                    string pMenuid = datNode.Tables[0].Rows[f]["MENUID"].ToString();
                    objgroup_menu.MENUID = pMenuid;
                    Save_data(pMenuid);
                    Sub_Group_menu(pMenuid);
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }    



        //ham kiem tra cac ham de kiem tra group insert vao trong databases
        private void Save_data(string strMenuid)
        {
            try
            {
                objgroup_menu.MENUID = strMenuid;
                int j = 0;
                while (j < dtgGroup.Columns.Count)//theo cot truoc, duyet tung cot mot
                {
                    dtgGroup.RefreshEdit();
                    #region dong 1
                    if ((dtgGroup.Rows[0].Cells[j].Value == null))
                    {
                        objgroup_menu.ISINQUIRY = 0;
                    }
                    if ((dtgGroup.Rows[0].Cells[j].Value != null))
                    {
                        if (dtgGroup.Rows[0].Cells[j].Value.ToString() == "True")
                        {
                            objgroup_menu.ISINQUIRY = 1;
                        }
                        else
                        {
                            objgroup_menu.ISINQUIRY = 0;
                        }
                        // string user_name = datarole.Rows[k].HeaderCell.Value.ToString();
                    }
                    //objgroup_menu.ISINSERT = 0;
                    //objgroup_menu.ISEDIT = 0;
                    //objgroup_menu.ISDELETE = 0;
                    #endregion
                    #region dong 2
                    if ((dtgGroup.Rows[1].Cells[j].Value == null))
                    {
                        objgroup_menu.ISINSERT = 0;

                    }
                    if ((dtgGroup.Rows[1].Cells[j].Value != null))
                    {
                        if (dtgGroup.Rows[1].Cells[j].Value.ToString() == "True")
                        {
                            objgroup_menu.ISINSERT = 1;
                        }
                        else
                        {
                            objgroup_menu.ISINSERT = 0;
                        }
                        // string user_name = datarole.Rows[k].HeaderCell.Value.ToString();
                    }
                    #endregion
                    #region dong 3
                    if ((dtgGroup.Rows[2].Cells[j].Value == null))
                    {
                        objgroup_menu.ISEDIT = 0;

                    }
                    if ((dtgGroup.Rows[2].Cells[j].Value != null))
                    {
                        if (dtgGroup.Rows[2].Cells[j].Value.ToString() == "True")
                        {
                            objgroup_menu.ISEDIT = 1;
                        }
                        else
                        {
                            objgroup_menu.ISEDIT = 0;
                        }
                        // string user_name = datarole.Rows[k].HeaderCell.Value.ToString();
                    }
                    #endregion
                    #region dong 4
                    if ((dtgGroup.Rows[3].Cells[j].Value == null))
                    {
                        objgroup_menu.ISDELETE = 0;
                    }
                    if ((dtgGroup.Rows[3].Cells[j].Value != null))
                    {
                        if (dtgGroup.Rows[3].Cells[j].Value.ToString() == "True")
                        {
                            objgroup_menu.ISDELETE = 1;
                        }
                        else
                        {
                            objgroup_menu.ISDELETE = 0;
                        }
                        // string user_name = datarole.Rows[k].HeaderCell.Value.ToString();
                    }
                    #endregion
                    string strGroupname = dtgGroup.Columns[j].Name;
                    DataSet datGroup = new DataSet();
                    datGroup = objcontrolGroup.GetGROUPNAME(strGroupname);
                    int iGroupid = Convert.ToInt32(datGroup.Tables[0].Rows[0]["GROUPID"].ToString());
                    objgroup_menu.GROUPID = iGroupid;
                    objcontrolGroup.DeleteGROUPS(iGroupid, strMenuid);
                    if (!((objgroup_menu.ISDELETE == 0) && (objgroup_menu.ISEDIT == 0) &&
                        (objgroup_menu.ISINQUIRY == 0) && (objgroup_menu.ISINSERT == 0)))
                    {
                        objcontrolGroup_menu.AddGROUP_MENU(objgroup_menu);
                    }

                    j = j + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }



        /*---------------------------------------------------------------
         * Method           : treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
         * Muc dich         : Bat su kien click chuot vao cac node tren treeview
         * Tham so          : strGwtype(ten cua kenh thanh toan)
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                //trvmenu.ForeColor.Name. = Color.Black;
                dtgGroup.Rows[0].ReadOnly = true;
                dtgGroup.Rows[1].ReadOnly = true;
                dtgGroup.Rows[2].ReadOnly = true;
                dtgGroup.Rows[3].ReadOnly = true;
                strNodename = e.Node.Text;
                strMenuid = e.Node.Name;//lay ra menu ID
                iEdit = 0;
                cmdSave.Enabled = false;
                if (strNodename == "Menu")
                {
                    cmdEdit.Enabled = false;
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void LoadData()
        {
            try
            {
                cmdEdit.Enabled = true;                
                int g = 0;
                while (g < dtgGroup.Columns.Count)
                {
                    //refresh ve null
                    dtgGroup.Rows[0].Cells[g].Value = null;
                    dtgGroup.Rows[1].Cells[g].Value = null;
                    dtgGroup.Rows[2].Cells[g].Value = null;
                    dtgGroup.Rows[3].Cells[g].Value = null;
                    string strGroupName = dtgGroup.Columns[g].Name;
                    try
                    {
                        DataSet datGroupdd = new DataSet();
                        datGroupdd = objcontrolGroup_menu.GetMenu_groupdd(strMenuid, strGroupName);
                        if (datGroupdd.Tables[0].Rows.Count > 0)
                        {
                            int view = Convert.ToInt32(datGroupdd.Tables[0].Rows[0]["ISINQUIRY"].ToString());
                            int insert = Convert.ToInt32(datGroupdd.Tables[0].Rows[0]["ISINSERT"].ToString());
                            int edit = Convert.ToInt32(datGroupdd.Tables[0].Rows[0]["ISEDIT"].ToString());
                            int delete = Convert.ToInt32(datGroupdd.Tables[0].Rows[0]["ISDELETE"].ToString());


                            #region //so sanh de thu hien lenh
                            if (view == 1)
                            {
                                dtgGroup.Rows[0].Cells[g].Value = true;
                            }
                            else
                            {
                                dtgGroup.Rows[0].Cells[g].Value = null;
                            }
                            if (insert == 1)
                            {
                                dtgGroup.Rows[1].Cells[g].Value = true;
                            }
                            else
                            {
                                dtgGroup.Rows[1].Cells[g].Value = null;
                            }
                            if (edit == 1)
                            {
                                dtgGroup.Rows[2].Cells[g].Value = true;
                            }
                            else
                            {
                                dtgGroup.Rows[2].Cells[g].Value = null;
                            }
                            if (delete == 1)
                            {
                                dtgGroup.Rows[3].Cells[g].Value = true;
                            }
                            else
                            {
                                dtgGroup.Rows[3].Cells[g].Value = null;
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
                    }
                    g = g + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

       
        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }            
        }
    
        private void frmGroup_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (cmdSave.Enabled)
                    e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {       
                
                GetdataGroup(cboDepart.Text);
                LoadData();
                trvmenu.SelectedNode.ForeColor = Color.Red;
                //---------------------------------
                iEdit = 1;
                dtgGroup.Rows[0].ReadOnly = false;
                dtgGroup.Rows[1].ReadOnly = false;
                dtgGroup.Rows[2].ReadOnly = false;
                dtgGroup.Rows[3].ReadOnly = false;
                cmdEdit.Enabled = false;
                trvmenu.Enabled = false;
                cboDepart.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }  
        private void dtgGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

      
        private void dtgGroup_Enter(object sender, EventArgs e)
        {
            try
            {
                int s = 0;
                while (s < dtgGroup.Rows.Count)
                {
                    if (dtgGroup.Rows[s].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dtgGroup.Rows[s].Cells[0].Value.ToString() == "True")
                        {
                            
                        }
                    }
                    s = s + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //su kien click dau cach chon toan bo cac me nu
        private void dtgGroup_KeyDown(object sender, KeyEventArgs e)
         {
             try
             {
                 int h = 0;
                 int m = 0;
                 if (iEdit == 1)
                 {
                     if (e.KeyData == Keys.Space)
                     {

                         foreach (DataGridViewCell selectedCell in dtgGroup.SelectedCells)
                         {
                             h = selectedCell.RowIndex;
                             m = selectedCell.ColumnIndex;
                             if (dtgGroup.Rows[h].Cells[m].Value != null)// hang duoc chon
                             {
                                 if (dtgGroup.Rows[h].Cells[m].Value.ToString() == "True")
                                 {
                                     dtgGroup.Rows[h].Cells[m].Value = null;
                                 }
                                 else
                                 {
                                     dtgGroup.Rows[h].Cells[m].Value = true;
                                 }
                             }
                             else
                             {
                                 dtgGroup.Rows[h].Cells[m].Value = true;
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
             }
        }
       

        private void frmGroup_Menu_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdEdit_MouseDown(object sender, MouseEventArgs e)
        {
            trvmenu.SelectedNode.ForeColor = Color.Red;
        }

        private void dtgGroup_MouseLeave(object sender, EventArgs e)
        {
            Common.bTimer = 1;
        }

        private void trvmenu_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
        
    }
}
