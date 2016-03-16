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
    public partial class frgUser_group : frmBasedata
    {        
        public static int iID;
        public static string strNodename;
        public static string strNodeid;
        public static string iAddremove;
        public static string strFlag;

        private clsLog objLog = new clsLog();
        private USERSInfo objuser = new USERSInfo();
        private USERSController objcontroluser = new USERSController();
        private GROUPSInfo objgroup = new GROUPSInfo();
        private GROUPSController objcontrolgroup = new GROUPSController();
        private GROUP_USERInfo objgroup_user = new GROUP_USERInfo();
        private GROUP_USERController objcontrolgroup_user = new GROUP_USERController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private ImageList il = new ImageList();

        private int iNodes_index;
        private int iNodes_index1;
        private int iRans;
        //private bool NeedConfirm = true;        
        private static string  strNode1;        
        

        public frgUser_group()
        {
            InitializeComponent();
        }
        /*---------------------------------------------------------------
        * Method           : frgUser_group_Load(object sender, EventArgs e)
        * Muc dich         : form load du lieu
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void frgUser_group_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                iRans = 0;
                iNodes_index1 = 0;
                iNodes_index = 0;
                Adtreeviewitem();
                Getgroupuser();//goi ham lay du La cac GROUPNAME day vao datagird   
                dtggroup.Columns[1].ReadOnly = true;
                dtrgroup_user.Columns[1].ReadOnly = true;
                CommandStatus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void CommandStatus()
        {
            if (dtrgroup_user.Rows.Count == 0)
            { cmdRemove.Enabled = false; cmdRemoall.Enabled = false; }
            else
            { cmdRemove.Enabled = true; cmdRemoall.Enabled = true; }
            if (dtggroup.Rows.Count == 0)
            { cmdAdd.Enabled = false; cmdAddall.Enabled = false; }
            else
            { cmdAdd.Enabled = true; cmdAddall.Enabled = true; }
        }


        /*---------------------------------------------------------------
        * Method           : Adtreeviewitem()
        * Muc dich         : Lay thong tin trong bang users truong Branch de tao cac node trong treeview
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Adtreeviewitem()
        {
            DataSet datuser_group = new DataSet();
            datuser_group = objcontroluser.GetUSERS_BRANCHT();
            try
            {
                TreeNode[] parent = new TreeNode[100];
                int i = 0;
                while (i < datuser_group.Tables[0].Rows.Count)
                {                                     
                    string branch = datuser_group.Tables[0].Rows[i]["BRANCH"].ToString();
                    parent[i] = new TreeNode(branch);
                    trvuser.Nodes.Add(parent[i]);
                    parent[i].ImageIndex = 0;
                    Adsubtreeview(parent[i], branch);
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        
        /*---------------------------------------------------------------
        * Method           : Adsubtreeview(TreeNode user, string branch)
        * Muc dich         : Lay thong tin trong bang users truong USERNAME de tao cac node con trong treeview
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Adsubtreeview(TreeNode user, string branch)
        {
            DataSet datuserdd = new DataSet();
            datuserdd = objcontroluser.GetUSERSBR(branch);
            try
            {
                int j = 0;
                while (j < datuserdd.Tables[0].Rows.Count)
                {
                    TreeNode[] parent = new TreeNode[100];
                    string strUsername = datuserdd.Tables[0].Rows[j]["USERNAME"].ToString();
                    string strUserid = datuserdd.Tables[0].Rows[j]["USERID"].ToString();
                    string strNodename = strUserid + ":" + strUsername;
                    parent[j] = new TreeNode(strNodename);
                    user.Nodes.Add(parent[j]);
                    j = j + 1;

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
        * Method           : Getgroupuser()
        * Muc dich         : lay du lieu len datagridview thuoc group list of user
        * Tham so          : 
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Getgroupuser()
        {
            try
            {               
                dtggroup.Rows.Clear();
                DataSet datgroup = new DataSet();
                datgroup = objcontrolgroup.GetGROUP_ID();
                int cv = 0;
                while (cv < datgroup.Tables[0].Rows.Count)
                {
                    string Groupname = datgroup.Tables[0].Rows[cv]["GROUPNAME"].ToString();
                    string strGroupid = datgroup.Tables[0].Rows[cv]["GROUPID"].ToString();
                    dtggroup.Rows.Add();
                    dtggroup.Rows[cv].Cells[1].Value = Groupname;
                    dtggroup.Rows[cv].Cells[2].Value = strGroupid;
                    cv = cv + 1;
                }
                //dtggroup.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
         * Muc dich         : bat su kien khi click vao nut
         * Tham so          : 
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
                trvuser.Nodes[iNodes_index1].ForeColor = Color.Black;
                if (iRans == 0)
                {
                    trvuser.Nodes[iNodes_index1].Nodes[iNodes_index].ForeColor = Color.Black;
                }
                //------------------------------------------------------------------------
                if (Regex.IsMatch(e.Node.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    iNodes_index = e.Node.Index;//lay thong tin Index cua nodes cha  
                    iNodes_index1 = e.Node.Index; iRans = 1;
                }
                else
                {
                    iNodes_index = e.Node.Index;
                    iNodes_index1 = e.Node.Parent.Index;
                    iRans = 0;
                }                
                trvuser.Nodes[iNodes_index1].ForeColor = Color.Red;
                trvuser.SelectedNode.ForeColor = Color.Red;
                //------------------------------------------------------------------------  
                string strNode = e.Node.Text;
                strNode1 = e.Node.Text;
                if (Regex.IsMatch(strNode, @"^[0-9]*\z") == true)//neu node chi la so vd:990
                {
                    cmdAdd.Enabled = false;
                    cmdAddall.Enabled = false;
                    cmdRemoall.Enabled = false;
                    cmdRemove.Enabled = false;
                    dtrgroup_user.Rows.Clear();
                }
                else
                {                                     
                    
                    String[] M = strNode.Split(new String[] { ":" }, StringSplitOptions.None);//cat chuoi
                    strNodename = M[1];//Lay doan chuoi
                    strNodeid = M[0];
                    DataSet datgroup_userd = new DataSet();
                    datgroup_userd = objcontroluser.USERS_PASS(strNodeid);
                    iID = Convert.ToInt32(datgroup_userd.Tables[0].Rows[0]["ID"].ToString());
                    string strUserid = datgroup_userd.Tables[0].Rows[0]["USERID"].ToString();
                    if (dtrgroup_user.Rows.Count != 0)
                    {                   
                        dtrgroup_user.Rows.Clear();
                    }
                    Getdatagriduser_group(strUserid);//goi ham load du lieu
                    if (dtrgroup_user.Rows.Count == 0)
                    {
                        cmdRemove.Enabled = false;
                        cmdRemoall.Enabled = false;
                    }
                    else
                    {
                        cmdRemove.Enabled = true;
                        cmdRemoall.Enabled = true;
                    }
                    cmdAddall.Enabled = true;
                    cmdAdd.Enabled = true;   
                    Getgroupuser();
                    Remove_rows();
                    CommandStatus();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : Remove_rows()
         * Muc dich         : Xoa cac hang khi duoc chon trong luoi
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Remove_rows()
        {
            try
            {
                int ds = 0;
                while (ds < dtrgroup_user.Rows.Count)
                {
                    string strGroup_0 = dtrgroup_user.Rows[ds].Cells[1].Value.ToString();
                    int ff = 0;
                    while (ff < dtggroup.Rows.Count)
                    {
                        string strGroup_r = dtggroup.Rows[ff].Cells[1].Value.ToString();
                        if (strGroup_r == strGroup_0)
                        {
                            dtggroup.Rows.RemoveAt(ff);
                            ff = ff - 1;
                        }
                        ff = ff + 1;
                    }
                    ds = ds + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : Getdatagriduser_group(string strUserid)
         * Muc dich         : lay cac group thuon User duoc chon day vao luoi
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Getdatagriduser_group(string strUserid)
        {   
            try
            {
                DataSet group = new DataSet();
                group = objcontrolgroup.GetGROUP_USER1(strUserid);
                int nb = 0;
                while (nb < group.Tables[0].Rows.Count)
                {
                    string strGroup_n = group.Tables[0].Rows[nb]["GROUPNAME"].ToString();
                    string strGroupid = group.Tables[0].Rows[nb]["GROUPID"].ToString();
                    dtrgroup_user.Rows.Add();
                    dtrgroup_user.Rows[nb].Cells[1].Value = strGroup_n;
                    dtrgroup_user.Rows[nb].Cells[2].Value = strGroupid;
                    nb = nb + 1;
                }              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : cmdAdd_Click(object sender, EventArgs e)
         * Muc dich         : Add cac group vao luoi phia duoi
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void cmdAdd_Click(object sender, EventArgs e)
        {            
            cmdRemove.Enabled = true;
            cmdRemoall.Enabled = true;
            cmdAddall.Enabled = true;
            cmdAdd.Enabled = true;
            iAddremove = "Add";
            try
            {
                int s = 0;
                while (s < dtggroup.Rows.Count)//duyet tung dong tren ban ghi cua luoi
                {
                    if (dtggroup.Rows[s].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dtggroup.Rows[s].Cells[0].Value.ToString() == "True")
                        {
                            string strGroupName = dtggroup.Rows[s].Cells[1].Value.ToString();
                            string strGroupid = dtggroup.Rows[s].Cells[2].Value.ToString();
                            dtggroup.Rows.RemoveAt(s);
                            AddUser_group(strGroupName, strGroupid);
                            s = s-1;
                        }
                    }
                    s = s + 1;     
                }
                CommandStatus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : AddUser_group(string strGroupName)
         * Muc dich         : Add cac group vao luoi phia duoi
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void AddUser_group(string strGroupName, string strGroupid)
        {
            try
            {
                int count;
                if (dtrgroup_user.Rows.Count == 0)//neu datagrid chua co ban ghi nao
                {
                    dtrgroup_user.Rows.Add();
                    dtrgroup_user.Rows[0].Cells[1].Value = strGroupName;
                    dtrgroup_user.Rows[0].Cells[2].Value = strGroupid;     
                    dtrgroup_user.Columns[1].ReadOnly = true;                    
                }
                else//neu da co ban ghi roi
                {
                    count = dtrgroup_user.Rows.Count;
                    int a = 0;

                    while (a < count)//duyet tung dong trong datagrid
                    {
                        string strData = dtrgroup_user.Rows[a].Cells[1].Value.ToString();//lay ra du lieu
                        if (strData == strGroupName)//kiem tra co trung hay khong
                        { a = a + 1; }
                        else
                        {
                            if (a == count - 1)
                            {
                                dtrgroup_user.Rows.Add();
                                dtrgroup_user.Rows[count].Cells[1].Value = strGroupName;
                                dtrgroup_user.Rows[count].Cells[2].Value = strGroupid;  
                                dtrgroup_user.Columns[1].ReadOnly = true;
                            }
                            a = a + 1;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
                
        /*---------------------------------------------------------------
         * Method           : cmdAddall_Click(object sender, EventArgs e)
         * Muc dich         : Add cac toan bo cac group vao luoi phia duoi
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void cmdAddall_Click(object sender, EventArgs e)
        {
            try
            {
                #region Enable controls
                frmBasedata.iSadd = false;
                cmdRemove.Enabled = true;
                cmdRemoall.Enabled = true;
                cmdAdd.Enabled = true;
                cmdSave.Enabled = true;
                cmdAddall.Enabled = false;
                iAddremove = "Addall";
                #endregion
                add_all();//goi ham add du lieu
                CommandStatus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : cmdAddall_Click(object sender, EventArgs e)
         * Muc dich         : Ham thuc hien add du llieu
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void add_all()
        {
            try
            {
                int df = 0;
                while (df < dtggroup.Rows.Count)
                {
                    string strGr = dtggroup.Rows[df].Cells[1].Value.ToString();
                    string strGroupid = dtggroup.Rows[df].Cells[2].Value.ToString();
                    AddUser_group(strGr, strGroupid);
                    dtggroup.Rows.RemoveAt(df);
                    df = 0;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : cmdRemove_Click(object sender, EventArgs e)
         * Muc dich         : Xoa hang duoc chon(xoa User ra khoi group do)
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void cmdRemove_Click(object sender, EventArgs e)
        {            
            frmBasedata.iSadd = false;
            cmdRemoall.Enabled = true;
            cmdAdd.Enabled = true;
            cmdAddall.Enabled = true;
            cmdRemove.Enabled = false;
            cmdSave.Enabled = true;
            iAddremove = "Remove";
            remove_R();
            CommandStatus();
        }
        /*---------------------------------------------------------------
         * Method           : cmdRemove_Click(object sender, EventArgs e)
         * Muc dich         : Xoa user do ra khoi tat ca cac group
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void remove_R()
        {
            try
            {
                int dd = 0;
                while (dd < dtrgroup_user.Rows.Count)
                {
                    if (dtrgroup_user.Rows[dd].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dtrgroup_user.Rows[dd].Cells[0].Value.ToString() == "True")
                        {
                            string strGroupName = dtrgroup_user.Rows[dd].Cells[1].Value.ToString();
                            string strGroupid = dtrgroup_user.Rows[dd].Cells[2].Value.ToString();
                            dtrgroup_user.Rows.RemoveAt(dd);
                            AddGROUP(strGroupName, strGroupid);                            
                            dd = dd - 1;
                        }                        
                    }
                    dd = dd + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : AddGROUP(string strGroupName)
         * Muc dich         : Add ten group duoc chon phia tren cho user vao group phia duoi
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void AddGROUP(string strGroupName, string strGroupid)
        {
            try
            {
                int count;
                if (dtggroup.Rows.Count == 0)//neu datagrid chua co ban ghi nao
                {
                    dtggroup.Rows.Add();
                    dtggroup.Rows[0].Cells[1].Value = strGroupName;
                    dtggroup.Rows[0].Cells[2].Value = strGroupid;
                    dtggroup.Columns[0].Width = 60;
                    dtggroup.Columns[1].Width = 355;
                    dtggroup.Columns[1].ReadOnly = true;
                }
                else//neu da co ban ghi roi
                {
                    count = dtggroup.Rows.Count;
                    int a = 0;
                    while (a < count)//duyet tung dong trong datagrid
                    {
                        string strData = dtggroup.Rows[a].Cells[1].Value.ToString();//lay ra du lieu
                        if (strData == strGroupName)//kiem tra co trung hay khong
                        { a = a + 1; }
                        else
                        {
                            if (a == count - 1)
                            {
                                //ad du lieu vao combobox
                                dtggroup.Rows.Add();
                                dtggroup.Rows[count].Cells[1].Value = strGroupName;
                                dtggroup.Rows[count].Cells[2].Value = strGroupid;
                                dtggroup.Columns[0].Width = 60;
                                dtggroup.Columns[1].Width = 355;
                                dtggroup.Columns[1].ReadOnly = true;
                            }
                            a = a + 1;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }



        /*---------------------------------------------------------------
         * Muc dich         : xoa user do ra khoi cac nhom
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdRemoall_Click(object sender, EventArgs e)
        {
            //frmBasedata.iSupdate = false;
            frmBasedata.iSadd = false;

            cmdRemove.Enabled = true;            
            cmdAdd.Enabled = true;
            cmdAddall.Enabled = true;
            cmdSave.Enabled = true;
            cmdRemoall.Enabled = false;

            try
            {
                DATAGRID();
                //dtrgroup_user.DataSource = 0;
                int h = 0;
                while (h < dtrgroup_user.Rows.Count)
                {
                    dtrgroup_user.Rows.RemoveAt(h);
                    h = h + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }          
            iAddremove = "Removeall";
            CommandStatus();
        }
        private void DATAGRID()
        {
            try
            {
                int mds = 0;
                while (mds < dtrgroup_user.Rows.Count)
                {
                    string strGROUP = dtrgroup_user.Rows[mds].Cells[1].Value.ToString();
                    string strGROUPID = dtrgroup_user.Rows[mds].Cells[2].Value.ToString();
                    AddGROUP(strGROUP, strGROUPID);
                    dtrgroup_user.Rows.RemoveAt(mds);                   
                }                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : xac dinh ham de insert vao co so du lieu
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = false;
                cmdAddall.Enabled = true;
                cmdRemove.Enabled = true;
                cmdRemoall.Enabled = true;              
                if (frmBasedata.iSadd == false)
                {
                    string Msg = "Do you want to Save data?";
                    string title = Common.sCaption;
                    DialogResult DlgResult = new DialogResult();
                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DlgResult == DialogResult.Yes)
                    {
                        if (iAddremove == "Remove")
                        { Add(); DeleteExisted(); }
                        if (iAddremove == "Addall")
                        { Addall(); }
                        if (iAddremove == "Removeall")
                        { DeleteAll(); }
                        cmdSave.Enabled = false;
                    }
                    else
                    { cmdSave.Enabled = true; }
                }
                else
                {
                    Add();
                    DeleteExisted();
                }
                
              CommandStatus();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void DeleteAll()
        {
            if (dtrgroup_user.Rows.Count == 0)
            {
                String[] M = strNode1.Split(new String[] { ":" }, StringSplitOptions.None);//cat chuoi
                string strUser_Name = M[1];
                DataSet datUUU = new DataSet();
                datUUU = objcontroluser.GetUSERS_PASS(strUser_Name);
                string strUserID = datUUU.Tables[0].Rows[0]["USERID"].ToString();
                objcontrolgroup_user.DeleteGroupUser(strUserID);
            }            
        }
        private void DeleteExisted()
        {
            int dd = 0;
            while (dd < dtggroup.Rows.Count)
            {
                string strGroupName = dtggroup.Rows[dd].Cells[1].Value.ToString();               
                //--------------goi ham xoa du lieu-----------------
                String[] M = strNode1.Split(new String[] { ":" }, StringSplitOptions.None);//cat chuoi
                string strUserName = M[1];
                DataSet datUU = new DataSet();
                datUU = objcontroluser.GetUSERS_PASS(strUserName);
                string strUserID = datUU.Tables[0].Rows[0]["USERID"].ToString();
                DataSet datDD = new DataSet();
                datDD = objcontrolgroup.GetGROUPNAME(strGroupName);
                int strGroupID = Convert.ToInt32(datDD.Tables[0].Rows[0]["GROUPID"].ToString());

                objgroup_user.USERID = strUserID;
                objgroup_user.GROUPID = strGroupID;
                DataSet datUser_groups = new DataSet();
                datUser_groups = objcontrolgroup_user.GetGROUPDATA(strGroupID, strUserID);
                if (datUser_groups.Tables[0].Rows.Count > 0)//khong co du lieu nao trung
                {
                    objcontrolgroup_user.DeleteGROUP_USER(strGroupID, strUserID);
                }
                dd = dd + 1;
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : ad user vao cac nhom da duoc chon o tren
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Add()
        {
            try
            {
                if (Regex.IsMatch(strNodename, @"^[0-9]*\z") == true)//neu node chi la so vd:990
                {
                    MessageBox.Show("You must select User name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {                   
                    int k = 0;
                    while (k < dtrgroup_user.Rows.Count)
                    {                        
                        int groupid = Convert.ToInt32(dtrgroup_user.Rows[k].Cells[2].Value.ToString());
                        objgroup_user.USERID = strNodeid;
                        objgroup_user.GROUPID = groupid;
                        DataSet datUser_groups = new DataSet();
                        datUser_groups = objcontrolgroup_user.GetGROUPDATA(groupid, strNodeid);
                        if (datUser_groups.Tables[0].Rows.Count == 0)//khong co du lieu nao trung
                        {
                            objcontrolgroup_user.AddGROUP_USER(objgroup_user);
                        }
                        k = k + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : ad user vao tat ca cac nhom
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Addall()
        {
            try
            {
                if (Regex.IsMatch(strNodename, @"^[0-9]*\z") == true)//neu node chi la so vd:990
                {
                    MessageBox.Show("Phải chọn đúng tên User!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int bb = 0;
                    while (bb < dtrgroup_user.Rows.Count)//duyet tung hang  cua datagrid
                    {
                        //--------lay ra groupname----------------------------------------------
                        string strGroupname = dtrgroup_user.Rows[bb].Cells[1].Value.ToString();
                        DataSet datUsersD = new DataSet();
                        datUsersD = objcontroluser.GetUSERS_PASS(strNodename.Trim());
                        string strUserid1 = datUsersD.Tables[0].Rows[0]["USERID"].ToString();
                        //-----------------------------
                        DataSet dtgroupidd = new DataSet();
                        dtgroupidd = objcontrolgroup.GetGROUPNAME(strGroupname.Trim());
                        int groupid = Convert.ToInt32(dtgroupidd.Tables[0].Rows[0]["GROUPID"].ToString().Trim());
                        DataSet datT = new DataSet();
                        datT = objcontrolgroup_user.GetGroup_userDD(groupid, strUserid1.Trim());
                        if (datT.Tables[0].Rows.Count == 0)
                        {
                            objgroup_user.USERID = strUserid1;
                            objgroup_user.GROUPID = groupid;
                            objcontrolgroup_user.AddGROUP_USER(objgroup_user);
                            //lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = strUserid1;
                            string strConten = useride + "/" + groupid;
                            int Log_level = 1;
                            string strWorked = "Insert";
                            string strTable = "Group_user";
                            string strOld_value = "";
                            string strNew_value = useride + groupid;
                            //-----------------------------------------
                            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                strWorked, strTable, strOld_value, strNew_value);
                            //--------------------------------------------------
                        }
                        bb = bb + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : ham xoa cac nhom duoc chon ma user do thuoc
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Remove()
        {
            try
            {
                DataSet datus = new DataSet();
                datus = objcontroluser.GetUSERS_PASS(strNodename);
                string strUserid = datus.Tables[0].Rows[0]["USERID"].ToString();
                int m = 0;
                while (m < dtrgroup_user.Rows.Count)//duyet tung ban ghi trong datagrid
                {
                    if (dtrgroup_user.Rows[m].Cells[0].Value == null)// hang khong duoc chon
                    {
                    }
                    if (dtrgroup_user.Rows[m].Cells[0].Value != null)
                    {
                        string groupname = dtrgroup_user.Rows[m].Cells["GROUPNAME"].Value.ToString();
                        DataSet dtgro = new DataSet();
                        dtgro = objcontrolgroup.GetGROUPNAME(groupname);
                        int groupid = Convert.ToInt32(dtgro.Tables[0].Rows[0]["GROUPID"].ToString());

                        objcontrolgroup_user.DeleteGROUP_USER(groupid, strUserid);
                        //lay gia tri de ghi log
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string strConten = strUserid + "/" + groupid;
                        int Log_level = 1;
                        string strWorked = "Delete";
                        string strTable = "Group_user";
                        string strOld_value = "";
                        string strNew_value = strUserid + "/" + groupid;
                        //-----------------------------------------
                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                            strWorked, strTable, strOld_value, strNew_value);

                    }

                    m = m + 1;
                }
                Getdatagriduser_group(strUserid);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : ham xoa toan bo cac nhom ma user do thuoc
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Removeall()
        {
            try
            {
                DataSet datuss = new DataSet();
                datuss = objcontroluser.GetUSERS_PASS(strNodename);
                string strUserid = datuss.Tables[0].Rows[0]["USERID"].ToString();
                int n = 0;
                while (n < dtrgroup_user.Rows.Count)
                {
                    string groupname = dtrgroup_user.Rows[n].Cells["GROUPNAME"].Value.ToString();
                    //--------------------------------------
                    DataSet dtgrop = new DataSet();
                    dtgrop = objcontrolgroup.GetGROUPNAME(groupname);
                    //--------------------------------------
                    int groupid = Convert.ToInt32(dtgrop.Tables[0].Rows[0]["GROUPID"].ToString());
                    objcontrolgroup_user.DeleteGROUP_USER(groupid, strUserid);//goi ham xoa
                    //ghilog------------------
                    DateTime dtLog = DateTime.Now;
                    string strUser = BR.BRLib.Common.strUsername;
                    string strConten = strUserid + "/" + groupid;
                    int Log_level = 1;
                    string strWorked = "Insert";
                    string strTable = "Group_user";
                    string strOld_value = "";
                    string strNew_value = strUserid + groupid;
                    //-----------------------------------------
                    objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                        strWorked, strTable, strOld_value, strNew_value);
                    //--------------------------------------------

                    n = n + 1;
                }
                Getdatagriduser_group(strUserid);
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
        //su kien thoat khoi form
        private void frgUser_group_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cmdSave.Enabled)
                return;

            e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
        }

        private void frgUser_group_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);                
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }
            }
        }

        private void frgUser_group_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dtggroup_MouseDown(object sender, MouseEventArgs e)
        {
            //trvuser.Nodes[iNodes_index].ForeColor = Color.Red;
        }

        private void trvuser_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dtggroup_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dtrgroup_user_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }          


    }
}
