using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRLib;
using System.Text.RegularExpressions;

namespace BR.BRSYSTEM
{
    public partial class frmGroup_User : frmBasesdataFrame
    {
        #region khai bao bien ham
        private GetData objGetData = new GetData();
        private clsLog objLog = new clsLog();
        GROUPSInfo objGroup = new GROUPSInfo();
        GROUPSController objcontrolGroup = new GROUPSController();
        USERSInfo objUser = new USERSInfo();
        USERSController objcontrolUser = new USERSController();
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objControlallcode = new ALLCODEController();
        GWTYPEInfo objGwtype = new GWTYPEInfo();
        GWTYPEController objcontrolGwtype = new GWTYPEController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        GWTYPEInfo objGWtype = new GWTYPEInfo();
        GWTYPEController objctrolGwtype = new GWTYPEController();
        ALLCODEInfo objAllcode = new ALLCODEInfo();
        ALLCODEController objCtrolallcode = new ALLCODEController();
        clsCheckInput objCheckInput = new clsCheckInput();
        GROUP_USERInfo ibjInfo = new GROUP_USERInfo();
        GROUP_USERController objctrol = new GROUP_USERController();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        private static string strGrouopOld;
        public static string strNodename;
        public static bool isUpdate = false;
        public static bool isDelete = false;
        //private bool NeedConfirm = true;
        //private static int iInquiry;
        private int iNodes_index;
        private int iNodes_index1;
        //private int iIndex;
        private int iSelect;
        private string pGroupIDD;        
        private string pgroupid;
        private string pgroupname;
        private string pGwtype;
        private string pdepartment;
        private string proll;
        private string pDescription;
        private string strNode_groupid;
        #endregion
        private static string pGroupid="";

        public frmGroup_User()
        {
            InitializeComponent();
        }
        /*---------------------------------------------------------------
         * Method           : frmGroup_User_Load(object sender, EventArgs e)
         * Muc dich         : form load
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void frmGroup_User_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            dtUser.Columns.Insert(0, new DataGridViewCheckBoxColumn());
            dtUser.Columns[0].HeaderCell = dgvColumnHeader;
            dtUser.Columns[0].HeaderText = "";
            dtUser.Columns[0].Width = 28;            
            //iIndex = 0;
            trvgroup.ForeColor = Color.Black;
            cmdEdit.Enabled = false;
            Getdata();
            Getdatacombo();
            txtgroupid.Enabled = false;
            txtgroupname.Enabled = false;
            cbGwtype.Enabled = false;
            cbdepartment.Enabled = false;
            cbroll.Enabled = false;
            txtDescription.Enabled = false;
            cmdCancel.Enabled = false;
            //iIndex = 1;
        }
        private void RefreshSave()
        {
            cmdAdd.Enabled = false;
            cmdEdit.Enabled = false;
            cmdDelete.Enabled = false;
            cmdSave.Enabled = true;
            cmdCancel.Enabled = true;
            txtgroupname.Focus();
        }
        /*---------------------------------------------------------------
         * Method           : EnabledControl()
         * Muc dich         : ham thuc hien an hay hien cac control 
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void EnabledControl()
        {
            try
            {
                if (isUpdate == false)
                {
                    txtgroupid.Enabled = false;
                    txtgroupname.Enabled = true;
                    cbGwtype.Enabled = true;
                    cbdepartment.Enabled = true;
                    cbroll.Enabled = true;
                    txtDescription.Enabled = true;
                    txtgroupname.Text = "";
                    txtDescription.Text = "";
                    cmdCancel.Enabled = true;
                    txtgroupname.Focus();
                }
                else
                {
                    txtgroupid.Enabled = false;
                    txtgroupname.Enabled = true;
                    cbGwtype.Enabled = true;
                    cbdepartment.Enabled = true;
                    cbroll.Enabled = true;
                    txtDescription.Enabled = true;
                    cmdCancel.Enabled = true;
                    txtgroupname.Focus();

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : Ham load thong tin tat ca cac nhom co trong he thong GW
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 30/05/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getdata()
        {
            try
            {
                trvgroup.Nodes.Clear();
                TreeNode[] parent = new TreeNode[100];
                parent[1] = new TreeNode("Group");
                trvgroup.Nodes.Add(parent[1]);
                MenuItem(parent[1]);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : MenuItem(TreeNode trvmenu)
         * Muc dich         : add cac du lieu lay duoc len cac node cua treeview
         * Tham so          : trvmenu
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void MenuItem(TreeNode trvmenu)
        {
            try
            {
                TreeNode[] parent = new TreeNode[100];
                DataSet datMenu = new DataSet();
                datMenu = objctrolGwtype.GetGwtype();
                int b = 0;
                while (b < datMenu.Tables[0].Rows.Count)
                {
                    string strCaption = datMenu.Tables[0].Rows[b]["GWTYPE"].ToString();
                    parent[b] = new TreeNode(strCaption);
                    trvmenu.Nodes.Add(parent[b]);
                    Submenu(parent[b], strCaption);
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : Submenu(TreeNode Node, string pGwtype)
         * Muc dich         : add cac du lieu lay duoc len cac node cua treeview
         * Tham so          : Node,pGwtype
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Submenu(TreeNode Node, string pGwtype)
        {
            try
            {
                TreeNode[] parent = new TreeNode[100];
                DataSet datMenu = new DataSet();
                datMenu = objctrol.GetGroup_Gwtype1(pGwtype);
                int b = 0;
                while (b < datMenu.Tables[0].Rows.Count)
                {
                    string strCaption = datMenu.Tables[0].Rows[b]["GROUPNAME"].ToString();
                    string strGroupID = datMenu.Tables[0].Rows[b]["GROUPID"].ToString();
                    parent[b] = new TreeNode(strCaption);
                    parent[b].Name = strGroupID;
                    Node.Nodes.Add(parent[b]);
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }



        /*---------------------------------------------------------------
         * Muc dich         : Lay thong tin khi click vao nut, goi ham lay du lieu len datagrid
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 30/05/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {

            if (e.Node.Text == "Group")
            { 
                iNodes_index1 = e.Node.Index;
                cmdAdd.Enabled = false;
            }
            else
            {
                cmdAdd.Enabled = true;
                if (e.Node.Parent.Text.Trim() == "Group")
                { 
                    iNodes_index = e.Node.Index;
                    pGroupid = "00";
                    cbGwtype.Text = e.Node.Text;
                    }
                else
                {
                    iNodes_index = e.Node.Parent.Index;
                    pGroupid = "11";
                }
            }
            strNodename = e.Node.Text;
            cmdSave.Enabled = false;
            cmdCancel.Enabled = false;
            try
            {
                if (strNodename == "Group")
                { cmdEdit.Enabled = false; cmdDelete.Enabled = false; }
                else
                {
                    DataSet datGwtype = new DataSet();
                    datGwtype = objcontrolGwtype.GetGwtype(strNodename);//tra xem co phai node do la kenh thanh toan hay khong
                    if (datGwtype.Tables[0].Rows.Count != 0)//neu node do la kenh thanh toan
                    {
                        #region
                        cmdEdit.Enabled = false;
                        cmdDelete.Enabled = false;
                        txtgroupid.Text = "";
                        txtgroupname.Text = "";
                        cbGwtype.Text = "";
                        cbdepartment.Text = "";
                        cbroll.Text = "";
                        txtDescription.Text = "";
                        dtUser.DataSource = 0;                        
                        #endregion
                    }
                    else//node chon khong phai la kenh thanh toan
                    {
                        strNode_groupid = e.Node.Name;
                        pGroupIDD = e.Node.Name;
                        //Lay du lieu
                        if (e.Node.Name == "") {
                            DataTable _dt = new DataTable();
                            _dt = objcontrolUser.Get_Groupid(e.Node.Text);
                            if (_dt.Rows.Count > 0)
                            {
                                strNode_groupid = _dt.Rows[0]["GROUPID"].ToString();
                                pGroupIDD = _dt.Rows[0]["GROUPID"].ToString();
                                e.Node.Name = _dt.Rows[0]["GROUPID"].ToString();
                            }
                        }
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdDelete.Enabled = true;
                        //------------------------------------
                        Getdatagird(strNodename);
                        Getdatatextbox(strNodename);//lay du lieu len textbox
                        #region
                        txtgroupid.Enabled = false;
                        txtgroupname.Enabled = false;
                        cbGwtype.Enabled = false;
                        cbdepartment.Enabled = false;
                        cbroll.Enabled = false;
                        txtDescription.Enabled = false;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : ham the hien group do thuoc kenh thanh toan nao
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/05/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getdatatextbox(string strNodename)
        {
            try
            {
                DataSet datGroupName = new DataSet();
                datGroupName = objcontrolGroup.GetGROUPNAME(strNodename);
                txtgroupid.Text = datGroupName.Tables[0].Rows[0]["GROUPID"].ToString();//groupID
                txtgroupname.Text = strNodename;//groupname
                txtDescription.Text = datGroupName.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                string strGwtype = datGroupName.Tables[0].Rows[0]["GWTYPE"].ToString();
                cbGwtype.Text = strGwtype;                                
                string strDepart = datGroupName.Tables[0].Rows[0]["DEPARTMENT"].ToString();                
                cbdepartment.Text = strDepart;//department
                string iRoll = datGroupName.Tables[0].Rows[0]["ISSUPERVISOR"].ToString();
                string strCdname = "Roll";
                DataSet datRoll = new DataSet();
                datRoll = objCtrolallcode.GetALLCODE_DATA(strCdname, iRoll);
                cbroll.Text = datRoll.Tables[0].Rows[0]["CONTENT"].ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        /*---------------------------------------------------------------
         * Muc dich         : ham lay du lieu len datagrid, nhung user thuoc group nao do
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 30/05/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getdatagird(string strNodename)
        {
            try
            {
                dtUser.DataSource = 0;
                //DataSet datGroupnode = new DataSet();
                //datGroupnode = objcontrolGroup.GetGROUPNAME(strNodename);
                //int iGroupid = Convert.ToInt32(datGroupnode.Tables[0].Rows[0]["GROUPID"].ToString());
                //lay thong tin ra luoi
                DataSet datGroup_user = new DataSet();
                datGroup_user = objcontrolUser.GetUSERS_GROUP(Convert.ToInt32(strNode_groupid));
                dtUser.DataSource = datGroup_user.Tables[0];
                FormatDataGrid();
                if (dtUser.Rows.Count == 0)
                { cmdRemove.Enabled = false; }
                else
                { cmdRemove.Enabled = true; }
                dtUser.Columns["USERID"].ReadOnly = true;
                dtUser.Columns["USERNAME"].ReadOnly = true;
                dtUser.Columns["STATUS"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void FormatDataGrid()
        {
            dtUser.Columns["userid"].Width = 80;
            dtUser.Columns["username"].Width = 260;
            dtUser.Columns["status"].Width = 100;
        }
        private void GetBox()
        {
            try
            {
                cbGwtype.Items.Clear();
                //---------lay thong tin cac kenh thanh toan--------------
                DataSet datGroup_type = new DataSet();
                datGroup_type = objctrolGwtype.GetGWTYPEName();
                int j = 0;
                while (j < datGroup_type.Tables[0].Rows.Count)
                {
                    string strGwtype = datGroup_type.Tables[0].Rows[j]["GWTYPE"].ToString();
                    cbGwtype.Items.Add(strGwtype);                    
                    j = j + 1;
                }
                cbGwtype.ValueMember = cbGwtype.Text;                
                //---------Lay thong tin Len combobox department---------------
                cbdepartment.Items.Clear();
                string strDepart = "Department";
                DataSet datGroup_department = new DataSet();
                datGroup_department = objCtrolallcode.GetALLCODE_D(strDepart);
                cbdepartment.DataSource = datGroup_department.Tables[0];
                cbdepartment.DisplayMember = "CONTENT";
                cbdepartment.ValueMember = "CDVAL";               
                //---------Lay thong tin Len combobox Roll---------------
                string strRoll = "Roll";
                DataSet datRoll = new DataSet();
                datRoll = objCtrolallcode.GetALLCODE_D(strRoll);
                cbroll.DataSource = datRoll.Tables[0];
                cbroll.DisplayMember = "CONTENT";
                cbroll.ValueMember = "CDVAL";
                //-------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : Lay thong tin len combobox TYPE
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 30/05/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Getdatacombo()
        {
            try
            {
                cbGwtype.Items.Clear();
                DataSet datGroup_type = new DataSet();
                datGroup_type = objctrolGwtype.GetGwtype();
                cbGwtype.DataSource = datGroup_type.Tables[0];
                cbGwtype.DisplayMember = "GWTYPE";
         
                string strDepart = "Department";
                DataSet datGroup_department = new DataSet();
                datGroup_department = objCtrolallcode.GetALLCODE_D(strDepart);
                cbdepartment.DataSource = datGroup_department.Tables[0];
                cbdepartment.DisplayMember = "CONTENT";
                cbdepartment.ValueMember = "CDVAL";
         
                string strRoll = "Roll";
                DataSet datRoll = new DataSet();
                datRoll = objCtrolallcode.GetALLCODE_D(strRoll);
                cbroll.DataSource = datRoll.Tables[0];
                cbroll.DisplayMember = "CONTENT";
                cbroll.ValueMember = "CDVAL";
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdgroupuser_Click(object sender, EventArgs e)
        {
            frgUser_group frmUser_group = new frgUser_group();
            frmUser_group.ShowDialog();

        }
        /*---------------------------------------------------------------
         * Muc dich         : lay du lieu len combobox khi themmoi thong tin
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 30/05/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------
            //if (txtgroupid.Text.Trim() == "")
            //{
            //        txtgroupid.Text= 
            //}
            pgroupid = txtgroupid.Text;
            pgroupname = txtgroupname.Text;
            pGwtype = cbGwtype.Text;
            pdepartment = cbdepartment.Text;
            proll = cbroll.Text;
            pDescription = txtDescription.Text;
            //-----------------------------------------------
            
            isUpdate = false;
            isDelete = false;
            trvgroup.Enabled = false;
            //Getdatacombo();
            EnabledControl();
            cbGwtype.Enabled = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*---------------------------------------------------------------
         * Muc dich         : Ham ghi du lieu vao trong database
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 06/06/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.iSconfirm == 0)
                {
                    trvgroup.SelectedNode.ForeColor = Color.Red;
                    RefreshSave(); return;
                }
                DataSet datGwtype = new DataSet();
                datGwtype = objcontrolGwtype.GetGwtype(txtgroupname.Text.Trim().ToUpper());
                if (datGwtype.Tables[0].Rows.Count != 0)//co tom tai group(click vao groiup)
                {
                    MessageBox.Show("Group name is not the same as channel!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtgroupname.Focus();
                    return;
                }
                if (txtgroupname.Text == "")
                {
                    MessageBox.Show("Group name is empty!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtgroupname.Focus();
                    RefreshSave();
                }
                else
                {
                    if (isUpdate == false && isDelete == false)//them moi du lieu
                    {
                        DataSet datG = new DataSet();
                        datG = objcontrolGroup.GetGROUPNAME(txtgroupname.Text);
                        if (datG.Tables[0].Rows.Count == 0)
                        {
                            objGroup.GROUPNAME = txtgroupname.Text;
                            objGroup.GWTYPE = cbGwtype.Text;
                            objGroup.DEPARTMENT = cbdepartment.Text;
                            objGroup.DESCRIPTION = txtDescription.Text;

                            objGroup.ISSUPERVISOR = Convert.ToInt32(cbroll.SelectedValue);

                            objcontrolGroup.AddGROUPS(objGroup);
                            //-----------------------------------------------------------
                            trvgroup.Nodes[0].Nodes[iNodes_index].Nodes.Add(txtgroupname.Text.Trim());

                            trvgroup.SelectedNode = trvgroup.Nodes[0].Nodes[iNodes_index].LastNode;
                           
                            trvgroup.SelectedNode.ForeColor = Color.Black;                            
                            trvgroup.Enabled = true;
                            trvgroup.Focus();
                            #region an cac control di------
                            cmdAdd.Enabled = true;
                            txtgroupname.Enabled = false;
                            cbGwtype.Enabled = false;
                            cbdepartment.Enabled = false;
                            cbroll.Enabled = false;
                            txtDescription.Enabled = false;
                            cmdCancel.Enabled = false;
                            #endregion
                            #region
                            string strUser = Common.strUsername;
                            DateTime dtDateLogin = DateTime.Now;
                            string strContent = "";
                            int iLoglevel = 1;
                            string strWorked = "Insert";
                            string strTable = "GROUPS";
                            string strOld_value = "";
                            string strNew_value = txtgroupname.Text + cbGwtype.Text + cbdepartment.Text + txtDescription.Text;
                            //goi ham ghilog
                            objLog.GhiLogUser(dtDateLogin, strUser, strContent, iLoglevel, 
                                strWorked, strTable, strOld_value, strNew_value);
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Group has already exited!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtgroupname.Text = "";
                            txtgroupname.Focus();
                            //trvgroup.SelectedNode.ForeColor = Color.Red;
                            cmdAdd.Enabled = false;
                            cmdEdit.Enabled = false;
                            cmdDelete.Enabled = false;
                            cmdSave.Enabled = true;
                            cmdCancel.Enabled = true;
                        }
                    }
                    if (isUpdate == true && isDelete == false)// Update du lieu
                    {
                        if (strNodename != "Group")
                        {                        
                            if (txtgroupname.Text.Trim() != strNodename)//neu thay doi ten group
                            {
                                DataSet datSetGroup = new DataSet();
                                datSetGroup = objcontrolGroup.GetGROUPNAME(txtgroupname.Text.Trim());
                                if (datSetGroup.Tables[0].Rows.Count == 0)
                                {
                                    DataSet datGroup = new DataSet();
                                    datGroup = objcontrolGroup.GetGROUPNAME(strNodename);
                                    trvgroup.SelectedNode.ForeColor = Color.Black;
                                    //---------------------------------------------------------------------------------
                                    int iGroupid = Convert.ToInt32(txtgroupid.Text.Trim());
                                    string strGROUPNAME = txtgroupname.Text;
                                    string strISSUPERVISOR = cbroll.Text;//cbGwtype
                                    string strGWTYPE = cbGwtype.Text;
                                    string strDEPARTMENT = cbdepartment.Text;
                                    //----------------------------------------------------------------------------------
                                    objGroup.GROUPID = iGroupid;
                                    objGroup.GROUPNAME = txtgroupname.Text;
                                    objGroup.GWTYPE = cbGwtype.Text;
                                    objGroup.DEPARTMENT = cbdepartment.Text;
                                    objGroup.DESCRIPTION = txtDescription.Text;
                                    objGroup.ISSUPERVISOR = Convert.ToInt32(cbroll.SelectedValue);
                                    objcontrolGroup.UpdateGROUPS(objGroup);
                                    trvgroup.SelectedNode.Text = txtgroupname.Text;
                                    strNodename = txtgroupname.Text.Trim();
                                    trvgroup.Focus();
                                    #region an cac control di------
                                    cmdAdd.Enabled = true;
                                    txtgroupname.Enabled = false;
                                    cbGwtype.Enabled = false;
                                    cbdepartment.Enabled = false;
                                    cbroll.Enabled = false;
                                    txtDescription.Enabled = false;
                                    cmdCancel.Enabled = false;
                                    #endregion -------------------------------
                                    #region lay du lieu de ghi log
                                    DateTime dtDateLogin = DateTime.Now;
                                    string strUser = Common.strUsername;
                                    string strContent = "";
                                    int iLoglevel = 1;
                                    string strWorked = "Update";
                                    string strTable = "GROUPS";
                                    string strOld_value = Convert.ToString(iGroupid) + strGROUPNAME + strISSUPERVISOR + strGWTYPE + strDEPARTMENT;
                                    string strNew_value = txtgroupname.Text + cbGwtype.Text + cbdepartment.Text + txtDescription.Text;
                                    //goi ham ghilog
                                    objLog.GhiLogUser(dtDateLogin, strUser, strContent, iLoglevel, 
                                        strWorked, strTable, strOld_value, strNew_value);
                                    #endregion
                                    trvgroup.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Group has already exited!", Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                    trvgroup.SelectedNode.ForeColor = Color.Red;
                                    txtgroupname.Text = strGrouopOld;
                                    txtgroupname.Focus();
                                    #region an controls
                                    cmdAdd.Enabled = false;
                                    cmdEdit.Enabled = false;
                                    cmdDelete.Enabled = false;
                                    cmdSave.Enabled = true;
                                    cmdCancel.Enabled = true;
                                    #endregion
                                }
                            }
                            else//neu thay doi ten group
                            {
                                DataSet datGroup = new DataSet();
                                datGroup = objcontrolGroup.GetGROUPNAME(strNodename);
                                //---------------------------------------------------------------------------------
                                int iGroupid = Convert.ToInt32(datGroup.Tables[0].Rows[0]["GROUPID"].ToString());
                                string strGROUPNAME = datGroup.Tables[0].Rows[0]["GROUPNAME"].ToString();
                                string strISSUPERVISOR = datGroup.Tables[0].Rows[0]["ISSUPERVISOR"].ToString();
                                string strGWTYPE = datGroup.Tables[0].Rows[0]["GWTYPE"].ToString();
                                string strDEPARTMENT = datGroup.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                                //----------------------------------------------------------------------------------
                                objGroup.GROUPID = iGroupid;
                                objGroup.GROUPNAME = txtgroupname.Text;
                                objGroup.GWTYPE = cbGwtype.Text;
                                objGroup.DEPARTMENT = cbdepartment.Text;
                                objGroup.DESCRIPTION = txtDescription.Text;
                                objGroup.ISSUPERVISOR = Convert.ToInt32(cbroll.SelectedValue);
                                objcontrolGroup.UpdateGROUPS(objGroup);
                                trvgroup.SelectedNode.ForeColor = Color.Black;
                                #region an cac control di------
                                cmdAdd.Enabled = true;
                                txtgroupname.Enabled = false;
                                cbGwtype.Enabled = false;
                                cbdepartment.Enabled = false;
                                cbroll.Enabled = false;
                                txtDescription.Enabled = false;
                                cmdCancel.Enabled = false;
                                trvgroup.Enabled = true;
                                #endregion------------------------------

                                #region lay du lieu de ghi log
                                DateTime dtDateLogin = DateTime.Now;
                                string strUser = Common.strUsername;
                                string strContent = "";
                                int iLoglevel = 1;
                                string strWorked = "Update";
                                string strTable = "GROUPS";
                                string strOld_value = Convert.ToString(iGroupid) + strGROUPNAME + strISSUPERVISOR + strGWTYPE + strDEPARTMENT;
                                string strNew_value = txtgroupname.Text + cbGwtype.Text + cbdepartment.Text + txtDescription.Text;                                
                                objLog.GhiLogUser(dtDateLogin, strUser, strContent, iLoglevel,
                                    strWorked, strTable, strOld_value, strNew_value);
                                #endregion
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            trvgroup.Focus();
        }
        
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------
            pgroupid = txtgroupid.Text;
            pgroupname = txtgroupname.Text;
            pGwtype = cbGwtype.Text;
            pdepartment = cbdepartment.Text;
            proll = cbroll.Text;
            pDescription = txtDescription.Text;
            //-----------------------------------------------
            trvgroup.SelectedNode.ForeColor = Color.Red;
            isUpdate = true;
            isDelete = false;
            trvgroup.Enabled = false;
            strGrouopOld = txtgroupname.Text;           
            EnabledControl();
        }
        /*---------------------------------------------------------------
         * Method           : cmdDelete_Click(object sender, EventArgs e)
         * Muc dich         : delete cac du lieu duoc chon
         * Tham so          : Node,pGwtype
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            isDelete = true;
            trvgroup.SelectedNode.ForeColor = Color.Red;
            if (strNodename == "Group" || strNodename == "IBPS" || strNodename == "SWIFT" || strNodename == "VCB" || strNodename == "TTSP" || strNodename == "TTSP" || strNodename == "IQS")
            {
            }
            else
            {
                if (Common.iSconfirm == 1)
                {
                    if (dtUser.Rows.Count == 0)
                    {
                        DataSet datGroupd = new DataSet();
                        datGroupd = objcontrolGroup.GetGROUPNAME(strNodename);
                        //-------------------------------------------------------------------------------------
                        int iGroupid = Convert.ToInt32(datGroupd.Tables[0].Rows[0]["GROUPID"].ToString());
                        string strGROUPNAME = datGroupd.Tables[0].Rows[0]["GROUPNAME"].ToString();
                        string strISSUPERVISOR = datGroupd.Tables[0].Rows[0]["ISSUPERVISOR"].ToString();
                        string strGWTYPE = datGroupd.Tables[0].Rows[0]["GWTYPE"].ToString();
                        string strDEPARTMENT = datGroupd.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                        //---------------------------------------------------------------------------------------
                        objcontrolGroup.DeleteGROUPS_Menu(iGroupid);
                        objcontrolGroup.DeleteGROUPS_(iGroupid);
                        trvgroup.SelectedNode.Remove();
                        DateTime dtDateLogin = DateTime.Now;
                        string strUser = Common.strUsername;
                        string strContent = "";
                        int iLoglevel = 1;
                        string strWorked = "DELETE";
                        string strTable = "GROUPS";
                        string strOld_value = "";
                        string strNew_value = "";
                        //goi ham ghilog
                        objLog.GhiLogUser(dtDateLogin, strUser, strContent, iLoglevel,
                            strWorked, strTable, strOld_value, strNew_value);

                    }
                    else
                    {
                        MessageBox.Show("Group can not be deleted because it is not empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    cmdSave.Enabled = false;
                    cmdCancel.Enabled = false;
                    cmdAdd.Enabled = true;
                    cmdEdit.Enabled = true;
                    cmdDelete.Enabled = true;
                }
            }
            trvgroup.SelectedNode.ForeColor = Color.Black;
            trvgroup.Focus();
            if (trvgroup.SelectedNode.Parent.Text.Trim() == "Group")
            {
                cmdDelete.Enabled = false;
                cmdEdit.Enabled = false;
            }
        }

        /*---------------------------------------------------------------
        * Method           : Messagebox()
        * Muc dich         : thong bao co hoi xoa hay khong
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private bool Messagebox()
        {
            bool iDelete = true;
            try
            {
                string Msg = "You are delete Group: '" + txtgroupname.Text + "'";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                { return iDelete = true; }
                else
                { return iDelete = false; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            return iDelete;
        }
       
        private void getdatacombob()
        {
            try
            {               
                cbdepartment.DataSource = 0;
                cbroll.Text = "";                
                DataSet datAllcodeGwtype = new DataSet();
                datAllcodeGwtype = objcontrolGwtype.GetGWTYPE();
                int k = 0;
                while (k < datAllcodeGwtype.Tables[0].Rows.Count)
                {
                    string strContent0 = datAllcodeGwtype.Tables[0].Rows[k]["GWTYPE"].ToString();
                    cbGwtype.Items.Add(strContent0);                    
                    cbGwtype.SelectedIndex = 0;
                    k = k + 1;
                }
                if (!objGetData.FillDataComboBox(cbdepartment, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME='Department' AND gwtype='SYSTEM'", "CONTENT", true, false, ""))
                    return;
                if (!objGetData.FillDataComboBox(cbroll, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME='Roll' AND gwtype='SYSTEM'", "CONTENT", true, false, ""))
                    return;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
       }
       

        private void frmGroup_User_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {

                SelectNextControl(this.ActiveControl, true, true, true, true);

                if ((this.ActiveControl) is Button)
                {
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frmGroup_User_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (!cmdSave.Enabled)
                return;
            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                #region an controls
                txtgroupid.Text = pgroupid;
                txtgroupname.Text = pgroupname;
                cbGwtype.Text = pGwtype;
                cbdepartment.Text = pdepartment;
                cbroll.Text = proll;
                txtDescription.Text = pDescription;                
                cmdAdd.Enabled = true;
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdSave.Enabled = false;
                cmdCancel.Enabled = false;
                txtgroupid.Enabled = false;
                txtgroupname.Enabled = false;
                cbGwtype.Enabled = false;
                cbdepartment.Enabled = false;
                cbroll.Enabled = false;
                txtDescription.Enabled = false;
                trvgroup.Enabled = true;
                #endregion
                trvgroup.SelectedNode.ForeColor = Color.Black;
                trvgroup.Focus();
                if (txtgroupid.Text.Trim() == "")
                { cmdEdit.Enabled = false; cmdDelete.Enabled = false; }
                else
                { cmdEdit.Enabled = true; cmdDelete.Enabled = true; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtgroupname_Leave(object sender, EventArgs e)
        {
            txtgroupname.Text = objCheckInput.ConvertVietnamese(txtgroupname.Text.Trim());
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            txtDescription.Text = objCheckInput.ConvertVietnamese(txtDescription.Text.Trim());
        }

        private void frmGroup_User_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void cmdDelete_MouseDown(object sender, MouseEventArgs e)
        {
            trvgroup.SelectedNode.ForeColor = Color.Red;
        }
        private void Check_Select_Rows()
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < dtUser.Rows.Count)
                {
                    if (dtUser.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dtUser.Rows[b].Cells[0].Value.ToString() == "True")
                        {
                            iSelect = 1;
                        }
                    }
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            try
            {
                //goi ham kiem tra xem co check dong nao khong
                Check_Select_Rows();//goi ham kiem tra xem co hang nao duoc chon khong
                if (iSelect == 1)
                {
                    int kk = 0;
                    while (kk < dtUser.Rows.Count)// duyet tung ban ghi trong Luoi
                    {
                        if (dtUser.Rows[kk].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dtUser.Rows[kk].Cells[0].Value.ToString() == "True")// dong duoc chon
                            {
                                string pUserID = dtUser.Rows[kk].Cells["UserID"].Value.ToString();
                                if (objctrol.DeleteGROUP_USER(Convert.ToInt32(pGroupIDD.Trim()), pUserID) == 1)
                                { dtUser.Rows.RemoveAt(kk); }
                                else
                                { kk = kk + 1; }
                            }
                            else
                            { kk = kk + 1; }
                        }
                        else
                        { kk = kk + 1; }
                    }
                }
                else
                {
                    MessageBox.Show("No user is selected",Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtUser_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    dtUser.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                }
            }
        }

        private void dtUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < this.dtUser.RowCount; i++)
                    {
                        this.dtUser.EndEdit();
                        string re_value = this.dtUser.Rows[i].Cells[0].EditedFormattedValue.ToString();
                    }
                }
            }
            if (e.RowIndex != -1)
            {
            }
        }

        private void dtUser_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void trvgroup_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
    }
}
