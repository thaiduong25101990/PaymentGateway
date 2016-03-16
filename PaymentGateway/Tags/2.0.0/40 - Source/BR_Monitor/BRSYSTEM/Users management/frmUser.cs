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
    public partial class frmUser : frmBasedata
    {
        #region dinh nghia cac ham va khai bao bien
        public static int iID;
        public static string strUserid;
        public static string UserIDD;

        private USERSInfo objuser = new USERSInfo();
        private GetData objGetData = new GetData();
        private USERSController objcontroluser = new USERSController();
        private GROUPSInfo objgroup = new GROUPSInfo();
        private GROUPSController objcontrolgroup = new GROUPSController();
        private SYSVARInfo objAllSysvar = new SYSVARInfo();
        private SYSVARController ObjctrLSysvar = new SYSVARController();
        private GROUP_USERInfo objgroup_user = new GROUP_USERInfo();
        private GROUP_USERController objcontrolgroup_user = new GROUP_USERController();
        private USER_PASSInfo objUser_pass = new USER_PASSInfo();
        private USER_PASSController UserCtrol = new USER_PASSController();
        private USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private ALLCODEInfo objAllcode = new ALLCODEInfo();
        private ALLCODEController objControlAllcode = new ALLCODEController();
        private clsCheckInput objCheckInput = new clsCheckInput();
        private ErrorProvider errorProvider = new ErrorProvider();
        private UserEncrypt Encrypt = new UserEncrypt();
                
        private int iNodes_index;
        //private int iIndex;
        private int iManue;
        private static bool strUpdate = false;
        private static bool strDelete = false;
        private static bool strInsert = false;
        private string pBranch_node;
        private string strUSERID1;
        private string strNodename;
        private string strHO;        
        private string pNode1;
        private string pNode_ID;
        private string pBRANCH;        
        //--------------------------------------------------------
        private string pBranch;
        private string pUserid;
        private string pUser_name;
        private string pPassword;
        private string pMobile;
        private string pEmail;
        private string pStatus;
        private string pTran_date;
        private string pLast_date;
        private string pDescript;
        #endregion

        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            //iIndex = 0;
            trvuser.ForeColor = Color.Black;
            Adtreeviewitem();
            tlbSave.Enabled = false;
            tlbDelete.Enabled = false;
            tlbEdit.Enabled = false;
            tlbCancel.Enabled = false;

            GetHO();
            EnabledTextBox();
            Getcombo();            
            this.cmdAdd.TabStop = false;
            this.cmdClose.TabStop = false;
            this.cmdDelete.TabStop = false;
            this.cmdEdit.TabStop = false;
            this.cmdSave.TabStop = false;
        }

        /*---------------------------------------------------------------
         * Method           : Getcombo()
         * Muc dich         : ham lay du lieu len combiobox
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Getcombo()
        {
            try
            {
                //Load data cbstatus       
                if (!objGetData.FillDataComboBox(cbstatus, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME='USERSTS' AND GWTYPE='SYSTEM'", "CONTENT", true, false, ""))
                    return;
                //Load data cbbranch            
                if (!objGetData.FillDataComboBox(cbbranch, "SIBS_BANK_CODE", "SIBS_BANK_CODE", "Branch",
                    "", "SIBS_BANK_CODE", true, false, ""))
                    return;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        /*---------------------------------------------------------------
         * Method           : EnabledTextBox()
         * Muc dich         : Enable = true,False tuy thuoc dieu kien
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void EnabledTextBox()
        {
            try
            {
                if (strInsert == true)
                {
                    cbbranch.Enabled = true; txtuserid.Enabled = true; txtusername.Enabled = true;
                    txtpassword.Enabled = true; txtconfimpass.Enabled = true; txtmobile.Enabled = true;
                    txtemail.Enabled = true; cbstatus.Enabled = true; txtdescription.Enabled = true;
                }
                else
                {
                    cbbranch.Enabled = false; txtuserid.Enabled = false; txtusername.Enabled = false;
                    txtpassword.Enabled = false; txtconfimpass.Enabled = false; txtmobile.Enabled = false;
                    txtemail.Enabled = false; cbstatus.Enabled = false; dtlasttrans.Enabled = false;
                    dtlastchange.Enabled = false; txtdescription.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        
        /*---------------------------------------------------------------
         * Method           : treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
         * Muc dich         : Bat su kien click len nut, lay ten cua nut, kiem tra node click co phai la Username khong
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
                pNode_ID = ""; trvuser.Nodes[iNodes_index].ForeColor = Color.Black;
                //iIndex = 1;
                if (Regex.IsMatch(e.Node.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                {
                    pBranch_node = Convert.ToString(e.Node.Text);
                    iNodes_index = e.Node.Index;//lay thong tin Index cua nodes cha
                }
                else
                {
                    pBranch_node = Convert.ToString(e.Node.Parent.Text);
                    iNodes_index = e.Node.Parent.Index; pNode_ID = e.Node.Name;
                }
                strNodename = e.Node.Text;
                pNode1 = e.Node.ImageKey;
                if (Regex.IsMatch(strNodename, @"^[0-9]*\z") == true)//neu node chi la so vd:990
                {
                    tlbEdit.Enabled = false;  tlbDelete.Enabled = false;
                }
                else
                {
                    tlbSave.Enabled = false;
                    if (e.Node.Name != "")
                    { Getdata(e.Node.Name); }
                    pBRANCH = cbbranch.Text.Trim();
                    #region an hien cac controls
                    tlbDelete.Enabled = true;
                    tlbEdit.Enabled = true;
                    tlbAdd.Enabled = true;
                    cbbranch.Enabled = false;
                    txtuserid.Enabled = false;
                    txtusername.Enabled = false;
                    txtpassword.Enabled = false;
                    txtconfimpass.Enabled = false;
                    txtmobile.Enabled = false;
                    txtemail.Enabled = false;
                    cbstatus.Enabled = false;
                    dtlasttrans.Enabled = false;
                    dtlastchange.Enabled = false;
                    txtdescription.Enabled = false;
                    #endregion
                }
                //iIndex = 0;
                cbbranch.Text = pBranch_node;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void GetHO()
        {
            DataSet dsSysTemp = new DataSet();
            SYSVARController objSysvarControl = new SYSVARController();
            dsSysTemp = objSysvarControl.GetSYSVAR_NAME("MAINBRANCODE", "IBPS");

            if (dsSysTemp.Tables[0].Rows.Count > 0)
            {
                strHO = dsSysTemp.Tables[0].Rows[0]["Value"].ToString().Trim();
            }
            else strHO = "";
        }
        /*---------------------------------------------------------------
         * Method           : Getdatabranch(string strUser)
         * Muc dich         : Get du lieu len combobox Branch
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Getdatabranch(string strUser)
        {
            
            //cbbranch.Items.Clear();
            try
            {
                DataSet datUser_branch = new DataSet();
                datUser_branch = objcontroluser.Userid_UD(strUser);
                int iBranch = Convert.ToInt32(datUser_branch.Tables[0].Rows[0]["BRANCH"].ToString());
                cbbranch.Text = Convert.ToString(iBranch);               

                Getdata(strUser);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Method           : Getdata(string strusername)
         * Muc dich         : Lay du lieu day len combobox va otext
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Getdata(string strusername)
        {
            try
            {                          
                DataSet datUsertext = new DataSet();
                datUsertext = objcontroluser.Userid_UD(strusername);
                if (datUsertext.Tables[0].Rows.Count != 0)
                {
                    string strUser = "";
                    cbbranch.Text = datUsertext.Tables[0].Rows[0]["BRANCH"].ToString();
                    iID = Convert.ToInt32(datUsertext.Tables[0].Rows[0]["ID"].ToString());
                    UserIDD = datUsertext.Tables[0].Rows[0]["USERID"].ToString();

                    // chinh sua lai cho truong hop UserID khong co 3 gia tri dau la ma chi nhanh
                    if (UserIDD.Substring(0, 3) == cbbranch.Text)
                    {
                        strUser = UserIDD.Substring(3);
                        txtuserid.Text = cbbranch.Text + "-" + strUser;
                    }
                    else
                    {
                        txtuserid.Text = UserIDD;
                    }
                   
                    txtusername.Text = datUsertext.Tables[0].Rows[0]["USERNAME"].ToString();
                    txtpassword.Text = datUsertext.Tables[0].Rows[0]["PASSWORD"].ToString();
                    txtconfimpass.Text = txtpassword.Text;
                    txtmobile.Text = datUsertext.Tables[0].Rows[0]["MOBILE"].ToString();
                    txtemail.Text = datUsertext.Tables[0].Rows[0]["EMAIL"].ToString();
                    string dtLastchange = datUsertext.Tables[0].Rows[0]["LASTDATE"].ToString();
                    if (dtLastchange == "1/1/0001 12:00:00 AM")
                    {
                        dtlastchange.Checked = false;
                    }
                    else
                    {
                        dtlasttrans.Value = Convert.ToDateTime(dtLastchange);
                    }
                    string strPasstime = datUsertext.Tables[0].Rows[0]["PASSTIME"].ToString();
                    if (strPasstime == "1/1/0001 12:00:00 AM")//1/1/0001 12:00:00 AM
                    { dtlasttrans.Checked = false; }
                    else
                    {
                        dtlastchange.Value = Convert.ToDateTime(strPasstime);
                    }                 
                    txtdescription.Text = datUsertext.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                    int iStatus = Convert.ToInt32(datUsertext.Tables[0].Rows[0]["STATUS"].ToString());
                    cbstatus.SelectedValue = iStatus;
                    strUserid = txtuserid.Text;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : ham lay thong cac chi nhanh len treeview
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 29/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Adtreeviewitem()
        {
            trvuser.Nodes.Clear();
            DataSet datuser_group = new DataSet();
            datuser_group = objcontroluser.GetUSERS_BRANCHT();
            try
            {
                TreeNode[] parent = new TreeNode[100];
                int i = 0;
                while (i < datuser_group.Tables[0].Rows.Count)
                {
                    // int userid = Convert.ToInt32(datuser_group.Tables[0].Rows[i]["USERID"].ToString());
                    string branch = datuser_group.Tables[0].Rows[i]["BRANCH"].ToString();
                    parent[i] = new TreeNode(branch);
                    parent[i].Name = branch;
                    parent[i].ForeColor = Color.Black;
                    trvuser.Nodes.Add(parent[i]);

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
         * Muc dich         : Lay thong tin user ad vao cac nhanh cua node tren treeview
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 29/54/2008
         * Nguoi cap nhat   : Quynd
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
                    string username = datuserdd.Tables[0].Rows[j]["USERNAME"].ToString();
                    parent[j] = new TreeNode(username);
                    parent[j].Name = datuserdd.Tables[0].Rows[j]["USERID"].ToString();
                    parent[j].ForeColor = Color.Black;
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
         * Muc dich         : Them moi Username
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 29/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void tlbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                cbbranch.Text = pBranch_node;
                //-----------------------------------------------
                pBranch = cbbranch.Text.Trim();
                pUserid = txtuserid.Text.Trim();
                pUser_name = txtusername.Text.Trim();
                pPassword = txtpassword.Text;
                pMobile = txtmobile.Text.Trim();
                pEmail = txtemail.Text.Trim();
                pStatus = cbstatus.Text.Trim();
                pTran_date = dtlasttrans.Value.ToString();
                pLast_date = dtlastchange.Value.ToString();
                pDescript = txtdescription.Text;
                #region
                //iIndex = 1;
                trvuser.Nodes[iNodes_index].ForeColor = Color.Red;
                strInsert = true; tlbDelete.Enabled = false; tlbEdit.Enabled = false;
                tlbSave.Enabled = true; tlbAdd.Enabled = false; tlbCancel.Enabled = true;                
                strInsert = true; strDelete = false; strUpdate = false;
                #endregion
                Getcombo();
                EnabledTextBox();
                #region
                txtusername.Text = "";
                txtpassword.Text = "";
                txtconfimpass.Text = "";
                txtmobile.Text = "";
                txtemail.Text = "";
                txtdescription.Text = "";
                txtpassword.Text = "";
                txtconfimpass.Text = "";
                #endregion
                dtlasttrans.Value = DateTime.Now;
                dtlastchange.Value = DateTime.Now;
                cbstatus.SelectedValue = Common.GW_USERS_STATUS_PENDING;
                cbstatus.Enabled = false;
                cbbranch.Text = strNodename;
                cbbranch.Focus();
                cbbranch.Text = pBRANCH;

                txtuserid.Text = "";
               // txtuserid.Text = cbbranch.Text + "-";
                trvuser.Enabled = false;
                //iIndex = 0;
                cbbranch.Text = pBranch_node;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        
        /*---------------------------------------------------------------
         * Method           : tlbEdit_Click(object sender, EventArgs e)
         * Muc dich         : An hay hien cac otext
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void tlbEdit_Click(object sender, EventArgs e)
        {
            if (pNode_ID.Trim() == Common.pUser_Addmin)
            {
                MessageBox.Show("Can not edit user admin!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
              
                pBranch = cbbranch.Text.Trim();
                pUserid = txtuserid.Text.Trim();
                pUser_name = txtusername.Text.Trim();
                pPassword = txtpassword.Text;
                pMobile = txtmobile.Text.Trim();
                pEmail = txtemail.Text.Trim();
                pStatus = cbstatus.Text.Trim();
                pTran_date = dtlasttrans.Value.ToString();
                pLast_date = dtlastchange.Value.ToString();
                pDescript = txtdescription.Text;
                //------------------------------------------
                //iIndex = 1;
                trvuser.SelectedNode.ForeColor = Color.Red;
                trvuser.Enabled = false;
                txtuserid.Enabled = false;
                cbbranch.Enabled = false;
                txtusername.Enabled = false;
                txtpassword.Enabled = false;
                txtconfimpass.Enabled = false;
                dtlasttrans.Enabled = false;
                dtlastchange.Enabled = false;
                //----------------------------
                strDelete = false;
                strUpdate = true;
                strInsert = false;
                tlbEdit.Enabled = false;
                tlbAdd.Enabled = false;
                tlbDelete.Enabled = false;
                tlbSave.Enabled = true;
                tlbCancel.Enabled = true;
                //----------------------- 
                txtusername.Enabled = true;
                txtmobile.Enabled = true;
                txtemail.Enabled = true;
                if (cbstatus.SelectedValue.ToString() == Common.GW_USERS_STATUS_PENDING)
                    cbstatus.Enabled = false;
                else
                    cbstatus.Enabled = true;
                txtdescription.Enabled = true;
                txtusername.Focus();
                cbbranch.Enabled = true;
                //------------------------
                //iIndex = 0;
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : Kiem tra trang thai cua cac co de xac dinh goi ham nao
         * Tra ve           : 
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void tlbSave_Click(object sender, EventArgs e)
        {
            try
            {
                cbbranch.Enabled = false;
                iManue = 0;
                if (!CheckInput())
                {
                }
                else
                {
                    if (strDelete == false && strUpdate == false && strInsert == true)
                    {
                        string Userii = "";
                        string Msg = "Do you want to insert data?";
                        string title = Common.sCaption;
                        DialogResult DlgResult = new DialogResult();
                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (DlgResult == DialogResult.Yes)
                        {
                            if (txtuserid.ToString().IndexOf("-") > 0)
                            {
                                string strDD = txtuserid.Text;
                                String[] N = strDD.Split(new String[] { "-" }, StringSplitOptions.None);//cat chuoi
                                Userii = N[0] + N[1];
                            }
                            else
                            {
                                Userii = txtuserid.Text;
                            }
                            DataSet datUser = new DataSet();
                            datUser = objcontroluser.GetUSERSUPDATEPASS(Userii);
                            if (datUser.Tables[0].Rows.Count == 0)
                            {
                                Savedata();
                                MessageBox.Show("Insert data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                int iCount_Nodes = trvuser.Nodes.Count;
                                int g = 0;
                                while (g < iCount_Nodes)
                                {
                                    if (cbbranch.Text.Trim() == trvuser.Nodes[g].Text.Trim())
                                    {
                                        iManue = 1;
                                        iNodes_index = g;
                                    }
                                    g = g + 1;
                                }
                                if (iManue == 1)//co du lieu trung
                                {
                                    trvuser.Nodes[iNodes_index].Nodes.Add(txtusername.Text.Trim());
                                    trvuser.SelectedNode = trvuser.Nodes[iNodes_index].LastNode; //txtuserid.Text.Trim().Replace("-","");
                                    trvuser.Nodes[iNodes_index].LastNode.Name = txtuserid.Text.Trim().Replace("-", "");
                                }
                                else if (iManue == 0)
                                {
                                    trvuser.Nodes.Add(cbbranch.Text.Trim());                                    
                                    trvuser.Nodes[iCount_Nodes].Nodes.Add(txtusername.Text.Trim());
                                    trvuser.Nodes[iCount_Nodes].LastNode.Name = txtuserid.Text.Trim().Replace("-", "");
                                    trvuser.SelectedNode = trvuser.Nodes[iCount_Nodes].LastNode;
                                    trvuser.Nodes[iCount_Nodes].LastNode.Name = txtuserid.Text.Trim().Replace("-", "");
                                    cmdEdit.Enabled = true;
                                    trvuser.Enabled = true;
                                }
                                if (iManue == 1)
                                {
                                    trvuser.Enabled = true;
                                    tlbCancel.Enabled = false;
                                    trvuser.Focus();
                                }
                                else
                                {
                                    strInsert = false;
                                    tlbAdd.Enabled = true;
                                    //tlbEdit.Enabled = false;
                                    tlbSave.Enabled = false;
                                    tlbCancel.Enabled = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("User ID has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    if (strDelete == false && strUpdate == true && strInsert == false)
                    {
                         string Userii="" ;
                        string Msg = "Do you want to save the changes ?";
                        string title = Common.sCaption;
                        DialogResult DlgResult = new DialogResult();
                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (DlgResult == DialogResult.Yes)
                        {
                            if (txtuserid.ToString().IndexOf("-") > 0)
                            {
                                string strDD = txtuserid.Text;
                                String[] N = strDD.Split(new String[] { "-" }, StringSplitOptions.None);//cat chuoi
                                 Userii = N[0] + N[1];
                            }
                            else
                            {
                                Userii = txtuserid.Text;
                            }

                            DataSet datUser = new DataSet();
                            datUser = objcontroluser.GetUSERSUPDATEPASS(Userii);
                            if (datUser.Tables[0].Rows.Count == 0)
                            {
                            }
                            else
                            {
                                Updatedata(iID);
                                trvuser.SelectedNode.Text = txtusername.Text;
                                trvuser.Focus();
                                trvuser.SelectedNode.ForeColor = Color.Black;
                                trvuser.Enabled = true;

                                MessageBox.Show("Update data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                strUpdate = false;
                                tlbAdd.Enabled = true;
                                tlbEdit.Enabled = true;
                                tlbSave.Enabled = false;
                                tlbDelete.Enabled = true;
                                tlbCancel.Enabled = false;
                                txtusername.Enabled = false;
                                txtmobile.Enabled = false;
                                txtemail.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            cmdEdit.Enabled = true;
        }
        /*---------------------------------------------------------------
         * Method           : Deletedata()
         * Muc dich         : Delete User
         * Tham so          : 
         * Tra ve           : 
         * Ngay tao         : 10/07/2008
         * Nguoi tao        : QuyND
         * Ngay cap nhat    : 10/07/2008
         * Nguoi cap nhat   : QuyND(Nguyen duc quy)
         *--------------------------------------------------------------*/
        private void Deletedata()
        {
            try
            {
                objUser_pass.USER_ID = UserIDD;
                UserCtrol.DeleteUSER_PASS(objUser_pass);
                if (objcontroluser.DeleteUSERS(Convert.ToInt32(UserIDD)) == -1)
                {
                    MessageBox.Show("Delete failed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Delete Successful", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    trvuser.SelectedNode.Remove();
                }
                if (Regex.IsMatch(strNodename, @"^[0-9]*\z") == true)//neu node chi la so vd:990
                {
                    //bo qua
                    tlbEdit.Enabled = false;
                    tlbSave.Enabled = false;
                    tlbDelete.Enabled = false;
                    tlbAdd.Enabled = true;

                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        /*---------------------------------------------------------------
         * Muc dich         : Ham kiem tra thong so dau vao
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Updatedata(int iID)
        {
            try
            {
                objuser.ID = iID;
                if (strUserid != txtuserid.Text)
                {
                    objuser.USERID = txtuserid.Text;
                }
                else
                {
                    if (txtuserid.Text.ToString().IndexOf("-") > 0)
                    {
                        String[] M = strUserid.Split(new String[] { "-" }, StringSplitOptions.None);//cat chuoi
                        objuser.USERID = M[0] + M[1];
                    }
                    else
                    {
                        objuser.USERID = txtuserid.Text.ToString();
                    }
                   
                }
                objuser.BRANCH = cbbranch.Text;
                objuser.USERNAME = txtusername.Text;
                objuser.PASSWORD = txtpassword.Text;
                objuser.MOBILE = txtmobile.Text;
                objuser.EMAIL = txtemail.Text;
                objuser.STATUS = Convert.ToInt32(cbstatus.SelectedValue);
                objuser.LASTDATE = DateTime.Now;
                objuser.DESCRIPTION = txtdescription.Text;
                objcontroluser.UpdateUSERS(objuser);               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            txtmobile.Enabled = false;
            txtemail.Enabled = false;
            cbstatus.Enabled = false;
            txtdescription.Enabled = false;
            //Adtreeviewitem();
         
        }
        /*---------------------------------------------------------------
         * Muc dich         : Ham kiem tra thong so dau vao
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private bool CheckInput()
        {
            bool result = true;
            //kiem tra do dai ky tu cua password va kiem tra do phuc tap cua pass
            int iPassLength = 0;
            if (!objcontroluser.CHECK_PASS_LENGTH(txtpassword.Text.Trim(), out iPassLength))
            {
                MessageBox.Show("The password is not allow lengther than " + iPassLength + " character!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpassword.Focus();
                return result = false;
            }
            //Check ky tu co ky tu 1-9, a-z, A-Z
            int iType = 0;
            if (!objcontroluser.CHECK_PASS_STRING(txtpassword.Text.Trim(), out iType))
            {
                if (iType == 0)
                    MessageBox.Show("Password input must be have character in  0-9, a-z, A-Z!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                   
                else if (iType == 1)
                    MessageBox.Show("Password input must be have character in 0-9!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                       
                else if (iType == 2)
                    MessageBox.Show("Password input must be have character in a-z, A-Z!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                       
                    
                return false;
            }
            //kiem tra UserID da ton tai hay chua           
           
            if (txtuserid.Text == "" )
            {
                MessageBox.Show("User is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtuserid.Focus();
                return result = false;
            }
            // 08/07/2010
            //Bo check User can toan so
            // Cho phep User co ca chu va so
            
            //if (Regex.IsMatch(txtusername.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
            //{
            //    MessageBox.Show("Invalid user name!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtusername.Focus();
            //    return result = false;
            //}
            if (strInsert == true)
            {
                if (txtuserid.Text != "")
                {
                    // 08/07/2010
                    //Bo check User can toan so
                    // Cho phep User co ca chu va so
                    //if (Regex.IsMatch(txtuserid.Text, @"^[0-9]*\z") == true)//neu hoan toan la so
                    //{
                    //    MessageBox.Show("Userid is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    txtuserid.Text = cbbranch.Text + "-";
                    //    txtuserid.Focus();
                    //    txtuserid.Select(4, 0);
                    //    //txtuserid.Focus();
                    //    return result = false;
                    //}
                    //else//neu co dang ky tu nua
                    //{
                    //    String strUserid = txtuserid.Text;
                    //    String[] M = strUserid.Split(new String[] { "-" }, StringSplitOptions.None);//cat chuoi
                    //    int k = M.Count<String>();
                    //    if (k == 1)//kiem tra co chuoi thu hai khong
                    //    {
                    //        MessageBox.Show("Userid is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        txtuserid.Text = cbbranch.Text + "-";
                    //        txtuserid.Focus();
                    //        txtuserid.Select(4, 0);
                    //        return result = false;
                    //    }
                    //    else
                    //    {
                    //        if (k == 2)
                    //        {
                    //            if (Regex.IsMatch(M[0], @"^[0-9]*\z") == true)//neu hoan toan la so
                    //            {
                    //                if (M[0] == cbbranch.Text)
                    //                {
                    //                    // 08/07/2010
                    //                    //Bo check User can toan so
                    //                    // Cho phep User co ca chu va so
                    //                    //if (Regex.IsMatch(M[1], @"^[0-9]*\z") == true)//neu hoan toan la so
                    //                    //{
                    //                        string strLenght = M[1].Length.ToString();
                    //                        if (strLenght == "10")
                    //                        {
                    //                            result = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            MessageBox.Show("Userid must be 10 characters!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                            txtuserid.Text = cbbranch.Text + "-";
                    //                            txtuserid.Focus();
                    //                            txtuserid.Select(10, 0);
                    //                            //txtuserid.Select(4, 0);
                    //                            return result = false;
                    //                        }
                    //                        //result = true;
                    //                    //}
                    //                    //else
                    //                    //{
                    //                    //    MessageBox.Show("Userid is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                    //    txtuserid.Text = cbbranch.Text + "-";
                    //                    //    txtuserid.Focus();
                    //                    //    txtuserid.Select(4, 0);
                    //                    //    return result = false;
                    //                    //}
                    //                }
                    //                else
                    //                {
                    //                    MessageBox.Show("Userid is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                    txtuserid.Text = cbbranch.Text + "-";
                    //                    txtuserid.Focus();
                    //                    txtuserid.Select(4, 0);
                    //                    return result = false;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                MessageBox.Show("Userid is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //                txtuserid.Text = cbbranch.Text + "-";
                    //                txtuserid.Focus();
                    //                txtuserid.Select(4, 0);
                    //                return result = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Userid is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //            txtuserid.Text = cbbranch.Text + "-";
                    //            txtuserid.Focus();
                    //            txtuserid.Select(4, 0);
                    //            return result = false;
                    //        }
                    //    }                        
                    //}
                }
            }

            if (txtusername.Text == "")
            {
                MessageBox.Show("Username is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtusername.Focus();
                return result = false;
            }
            if (txtpassword.Text == "")
            {
                MessageBox.Show("Password is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpassword.Focus();
                return result = false;
            }
            if (txtconfimpass.Text == "")
            {
                MessageBox.Show("Confirm password is empty!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtconfimpass.Focus();
                return result = false;
            }
                    
            
           
            if (txtpassword.Text != txtconfimpass.Text)
            {
                MessageBox.Show("Confirm password is not equal to password!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtconfimpass.Focus();
                txtconfimpass.SelectAll();
                return result = false;
            }
            if (txtmobile.Text != "")
            {
                if (!IsNumberic(txtmobile))
                {
                    MessageBox.Show("Mobile number is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmobile.Focus();
                    txtmobile.SelectAll();
                    return result = false;
                } 
            }
            if (txtemail.Text != "")
            {
                if (!IsValidEmail(txtemail))
                {
                    MessageBox.Show("Email is invalid!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtemail.Focus();
                    txtemail.SelectAll();
                    return result = false;
                }
            }
           return result;
        }
        // kiem tra Email nhap
        public bool IsValidEmail(TextBox txt)
        {   //Kiem tra hop le cua Email
            bool result = false;
            string email = txt.Text;
            if (email == "")
                return true; // Cho phep khong co dia chi email

            int first = email.IndexOf("@");
            int last = email.LastIndexOf("@");
            if (first == last)
                result = (Regex.IsMatch(email, @"(\w+)@(\w+)\.(\w+)"));
            if (result == false)
                errorProvider.SetError(txt, "Email is invalid!");
            else
                errorProvider.SetError(txt, string.Empty);
            return result;
        }
        // ham kiem tra so dien thoai
        public bool IsNumberic(Control Ctrl)
        {
            string d = txtusername.Text;
            bool result = false;
            string input = Ctrl.Text;
            result = Regex.IsMatch(input, @"^[0-9]*\z");

            if (result == false)
                errorProvider.SetError(Ctrl, "Is not numberic.");
            else
                errorProvider.SetError(Ctrl, string.Empty);
            return result;
        }
        /*---------------------------------------------------------------
         * Muc dich         : Them moi Username vao databases
         * Tra ve           : Mot danh sach cac File - List<FileInfo>
         * Ngay tao         : 26/54/2008
         * Nguoi tao        : Quynd
         * Ngay cap nhat    : 31/54/2008
         * Nguoi cap nhat   : Quynd
         *--------------------------------------------------------------*/
        private void Savedata()
        {
            String[] M;
            string strUserid_ = txtuserid.Text;
            if (strUserid_.IndexOf("-")>0)
            {
                M = strUserid_.Split(new String[] { "-" }, StringSplitOptions.None);//cat chuoi
            }
            else
            {
                M = new String[2];
                M[0]=strUserid_;
                M[1] = "";
             
            }
           
            try
            {
                DataSet datUsers = new DataSet();
                datUsers = objcontroluser.GetUSERSUPDATEPASS(M[0] + M[1]);
                if (datUsers.Tables[0].Rows.Count == 0)
                {
                    objuser.BRANCH = cbbranch.Text;
                    objuser.USERID = M[0] + M[1];
                    objuser.USERNAME = txtusername.Text;
                    objuser.PASSWORD = Encrypt.Encrypt(txtpassword.Text, Encrypt.sKeyUser);
                    objuser.MOBILE = txtmobile.Text;
                    objuser.EMAIL = txtemail.Text;

                    objuser.STATUS = Convert.ToInt32(cbstatus.SelectedValue);

                    if (dtlasttrans.Checked == true)
                    {
                        objuser.LASTDATE = dtlasttrans.Value;
                    }

                    if (dtlastchange.Checked == true)
                    {
                        objuser.PASSTIME = dtlastchange.Value;
                    }
                    objuser.DESCRIPTION = txtdescription.Text;
                    objcontroluser.AddUSERS(objuser);
                    //ghi du lieu vao bang User_pass
                    objUser_pass.USER_ID = M[0] + M[1];
                    objUser_pass.PRE_PASS = Encrypt.Encrypt(txtpassword.Text, Encrypt.sKeyUser);
                    objUser_pass.PASSTIME = DateTime.Now;
                    UserCtrol.AddUSER_PASS(objUser_pass);
                    //-----------dat an cac o text box----------------------
                    tlbDelete.Enabled = true;
                    tlbEdit.Enabled = true;
                    tlbAdd.Enabled = true;
                    cbbranch.Enabled = false;
                    txtuserid.Enabled = false;
                    txtusername.Enabled = false;
                    txtpassword.Enabled = false;
                    txtconfimpass.Enabled = false;
                    txtmobile.Enabled = false;
                    txtemail.Enabled = false;
                    cbstatus.Enabled = false;
                    dtlasttrans.Enabled = false;
                    dtlastchange.Enabled = false;
                    txtdescription.Enabled = false;
                    //-------------------------------------------------------
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void tlbDelete_Click(object sender, EventArgs e)
        {
            //-------------------Lay du lieu nhan dang do la user admin ---------------------------------
            DataSet dtAddmin = new DataSet();
            dtAddmin = ObjctrLSysvar.GetSYSVAR_NAME("ADMIN_ID", "SYSTEM");
            Common.pUser_Addmin = dtAddmin.Tables[0].Rows[0]["VALUE"].ToString();
            //-------------------------------------------------------------------------------------------
            if (pNode_ID.Trim() == Common.pUser_Addmin)
            {
                MessageBox.Show("Can not delete user admin!",Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //iIndex = 1;
                trvuser.SelectedNode.ForeColor = Color.Red;
                strDelete = true;
                tlbDelete.Enabled = false;
                tlbSave.Enabled = true;
                tlbEdit.Enabled = false;
                tlbAdd.Enabled = false;
                if (Messagebox())
                {
                    Deletedata();
                }
                else
                {
                    trvuser.SelectedNode.ForeColor = Color.Black;
                    tlbAdd.Enabled = true;
                    tlbEdit.Enabled = true;
                    tlbDelete.Enabled = true;
                    tlbSave.Enabled = false;
                }
                //iIndex = 0;
            }
        }
        //thong bao co hoi xoa hay khong
        private  bool Messagebox()
        {
          bool iDelete=true;
            try
            {
                string Msg = "Do you want to delete";
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
        private void cbbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                iManue = 0;
                trvuser.Nodes[iNodes_index].ForeColor = Color.Black;
                string strBranch = cbbranch.Text;
                //09/06/2010
                // Bo ma chi nhanh ghep vao UserID

                //txtuserid.Text = strBranch + "-";
                //het bo
                int iCount_Nodes = trvuser.Nodes.Count;
                int g = 0;
                while (g < iCount_Nodes)
                {
                    if (cbbranch.Text.Trim() == trvuser.Nodes[g].Text.Trim())
                    {
                        iManue = 1;
                        iNodes_index = g;
                        trvuser.Nodes[iNodes_index].ForeColor = Color.Red;
                    }
                    g = g + 1;
                }  
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (tlbSave.Enabled )
                    e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?",Common.sCaption);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void tlbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtuserid_TextChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    if (txtuserid.Text.Length <= 9)
            //    {
            //        strUSERID1 = txtuserid.Text.Trim();
            //    }
            //    if (txtuserid.Text.Length > 9)
            //    {
            //        txtuserid.Text = strUSERID1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            //}
        }
        /*---------------------------------------------------------------
        * Method           : enterKey(object sender, KeyEventArgs e)
        * Muc dich         : Bắt sự kiện KeyDown trên form
        * Tham so          :  
        * Tra ve           : 
        * Ngay tao         : 06/08/2008
        * Nguoi tao        : HueMT
        * Ngay cap nhat    : 
        * Nguoi cap nhat   : 
        *--------------------------------------------------------------*/
       
        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
            if (e.KeyCode  == Keys.Escape )
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Return )
            {                
                SelectNextControl(this.ActiveControl, true, true, true, true);
                if ((this.ActiveControl )  is TextBox )
                { 
                   (this.ActiveControl as TextBox).SelectAll();
                }            
            }
        }
    

        private void frmUser_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tlbCancel_Click(object sender, EventArgs e)
        {
            //---------------------------------------------------------
            cbbranch.Text = pBranch;
            txtuserid.Text = pUserid;
            txtusername.Text = pUser_name;
            txtpassword.Text = pPassword;
            txtconfimpass.Text = pPassword;
            txtmobile.Text = pMobile;
            txtemail.Text = pEmail;
            cbstatus.Text = pStatus;
            dtlasttrans.Value = Convert.ToDateTime(pTran_date);
            dtlastchange.Value = Convert.ToDateTime(pLast_date);
            txtdescription.Text = pDescript;
            //---------------------------------------------------------
            #region Enable cac control
            tlbAdd.Enabled = true;
            tlbEdit.Enabled = true;
            tlbDelete.Enabled = true;
            tlbSave.Enabled = false;
            tlbCancel.Enabled = false;
            cbbranch.Enabled = true;           
            cbstatus.Enabled = false;
            txtusername.Enabled = false;
            txtmobile.Enabled = false;
            txtemail.Enabled = false;
            txtdescription.Enabled = false;
            trvuser.Enabled = true;
            cbbranch.Enabled = false;
            cbbranch.Enabled = false;
            txtuserid.Enabled = false;
            txtusername.Enabled = false;
            txtpassword.Enabled = false;
            txtconfimpass.Enabled = false;
            txtmobile.Enabled = false;
            txtemail.Enabled = false;
            cbstatus.Enabled = false;
            dtlasttrans.Enabled = false;
            dtlastchange.Enabled = false;
            txtdescription.Enabled = false;
            #endregion
            trvuser.SelectedNode.ForeColor = Color.Black;

            //09/06/2010
            // Bo Check UserID la toan so

            //if (strInsert == true)
            //{
            //    if (Regex.IsMatch(strNodename.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
            //    { tlbDelete.Enabled = false; }
            //    else
            //    { tlbDelete.Enabled = true; }
            //}
            //else
            //{
            //    if (Regex.IsMatch(strNodename.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
            //    { tlbDelete.Enabled = false; }
            //    else
            //    { tlbDelete.Enabled = true; }
            //}       
            //Het cap nhat
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            txtusername.Text = objCheckInput.ConvertVietnamese(txtusername.Text.Trim());
        }

        private void txtdescription_Leave(object sender, EventArgs e)
        {
            txtdescription.Text = objCheckInput.ConvertVietnamese(txtdescription.Text.Trim());
        }

        private void frmUser_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void trvuser_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void trvuser_KeyDown(object sender, KeyEventArgs e)
        {
            cbbranch.Text = pBranch_node;
        }      
    }
}
