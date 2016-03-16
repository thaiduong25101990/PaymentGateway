using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRBusinessObject;
using System.Text.RegularExpressions;

namespace BR.BRSYSTEM
{
    public partial class frmMenu : frmBasedata 
    {
        private bool isInsert = false;
        private DataSet datDs = new DataSet();
        private BR.BRBusinessObject.MENUInfo objInfo = new BR.BRBusinessObject.MENUInfo();       
        GWTYPEController objGwtype = new GWTYPEController();
        MENUController objMenu = new MENUController();
        private string iID;
        private clsCheckInput checkInput = new clsCheckInput();
        private string pMenuid;
        private string pNode_name = "";
        private string pAssembly;
        private string pID;
        private DataTable dtMenuid;
        private int iManue;
        private int iNodes_index;
        private bool bDelete = false;
        private int idelete = 0;

        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            Load_data();
        }

        private void Load_data()
        {
            try
            {
                txtMenuID.Enabled = false; txtParentID.Enabled = false;
                cbMethod.SelectedIndex = 0;
                Load_combo();
                LoadData();
                Load_Menuid();
                trvMenu.ForeColor = Color.Black;
                LockTextBox(true);
                trvMenu.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Load_Menuid()
        {
            try
            {
                dtMenuid = objMenu.select_menuid();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Load_combo()
        {
            try
            {
                DataSet _ds = new DataSet();
                _ds = objGwtype.GetGwtype();
                if (_ds != null)
                {
                    int i = 0;
                    while (i < _ds.Tables[0].Rows.Count)
                    {
                        string strGwtype = _ds.Tables[0].Rows[i]["GWTYPE"].ToString();
                        cbChannel.Items.Add(strGwtype);
                        i = i + 1;
                    }
                    cbChannel.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            txtCaption.Focus();
            isInsert = false;
            LockTextBox(false);
            trvMenu.Enabled = false;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            txtCaption.Focus();
            isInsert = true;
            ClearText();
            LockTextBox(false);
            if (pMenuid == null) { pMenuid = "0000"; }
            txtParentID.Text = pMenuid.Trim();
            check_Menuid();
            trvMenu.Enabled = false;
        }

        private void check_Menuid()
        {
            try
            {
                Load_Menuid();
                int icount = 0;
                string pdefault = "1000";
                string pMenuid_parent = ""; 
                if (txtParentID.Text.Trim() == "0000")
                {
                    int i=0;
                    while(i<9999)
                    {
                        pMenuid_parent = Convert.ToString(Convert.ToInt32(pdefault) + 1000);
                        int j = 0;
                        while (j < dtMenuid.Rows.Count)
                        {
                            if (pMenuid_parent == dtMenuid.Rows[j]["MENUID"].ToString())//da ton tai roi break
                            { icount = 1; j = j + 1; break; }
                            j = j + 1;
                        }
                        if (icount == 1) { icount = 0; i = i + 1; }
                        else 
                        { 
                            txtMenuID.Text = pMenuid_parent.Trim(); break; 
                        }
                        if (pMenuid_parent.Trim() == "9000")
                        { break; }
                        pdefault = Convert.ToString(Convert.ToInt32(pdefault) + 1000);
                    }
                    if (txtMenuID.Text.Trim() == "")
                    {
                        i = 0;
                        while (i < 9999)
                        {
                            pMenuid_parent = Convert.ToString(Convert.ToInt32(pdefault) + 100);
                            int j = 0;
                            while (j < dtMenuid.Rows.Count)
                            {
                                if (pMenuid_parent == dtMenuid.Rows[j]["MENUID"].ToString())//da ton tai roi break
                                { icount = 1; break; }
                                j = j + 1;
                            }
                            if (icount == 1) { icount = 0; i = i + 1; }
                            else
                            {
                                txtMenuID.Text = pMenuid_parent.Trim(); break;
                            }
                            if (pMenuid_parent.Trim() == "9000")
                            { break; }
                            pdefault = Convert.ToString(Convert.ToInt32(pdefault) + 100);
                        }
                    }
                }
                else
                {
                    string strMenuid = "";
                    int i = 0;
                    while (i < dtMenuid.Rows.Count)
                    {
                        strMenuid = dtMenuid.Rows[i]["MENUID"].ToString();
                        if (strMenuid.Substring(0, 1) == pMenuid.Substring(0, 1))
                        {
                            txtMenuID.Text = Convert.ToString(Convert.ToInt32(strMenuid) + 1);
                            break;
                        }
                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_data()
        {
            try
            {
                if (objMenu.Check_delete(pMenuid) == -1)//khong con du lieu con nao
                {
                    idelete = 1;
                }
                if (idelete == 1)
                {
                    if (objMenu.DeleteMENU(pMenuid) == 1)
                    {
                        MessageBox.Show("Delete data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (BR.BRLib.Common.iSconfirm == 1)
            {
                trvMenu.Enabled = false;
                Delete_data();
            }
            cmdSave.Enabled = false;
            LoadData();
            trvMenu.Enabled = true;
        }       

        private void Save_data()
        {
            try
            {   
                if (Check_data())
                {
                    #region truyen bien de insert hoan update
                    objInfo.MENUID = txtMenuID.Text.Trim();
                    objInfo.PARENTID = txtParentID.Text.Trim();
                    objInfo.CAPTION = txtCaption.Text.Trim();
                    objInfo.Assembly = txtAssembly.Text.Trim();
                    objInfo.ASSEMBLYFILE = txtAssemblyFile.Text.Trim();
                    if (chkAlt.Checked == true) { objInfo.ALT = 1; } else { objInfo.ALT = 0; }
                    if (chkCtrl.Checked == true) { objInfo.CTRL = 1; } else { objInfo.CTRL = 0; }
                    if (chkEnable.Checked == true) { objInfo.ENABLE = 1; } else { objInfo.ENABLE = 0; }
                    objInfo.KEY = txtKey.Text.Trim();
                    objInfo.TOOLTIPTEXT = txtTooltipText.Text.Trim();
                    objInfo.ORDERMENU = Convert.ToInt32(txtOrderMenu.Text.Trim());
                    if (cbChannel.Text.Trim().ToUpper() == "SYSTEM") { objInfo.GWTYPE = "GW" + cbChannel.Text.Trim().ToUpper(); }
                    else
                    { objInfo.GWTYPE = cbChannel.Text.Trim().ToUpper(); }
                    objInfo.METHOD = cbMethod.Text.Trim();
                    #endregion
                    if (isInsert == true)
                    {
                        if (objMenu.AddMENU(objInfo) == 1)
                        {
                            MessageBox.Show("Insert data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            cmdAdd.Enabled = true; cmdEdit.Enabled = true; cmdSave.Enabled = false;
                            cbChannel.Enabled = true; trvMenu.Enabled = true;
                            LockTextBox(true);
                            #region add menu vao treeviews
                            int iCount_Nodes = trvMenu.Nodes.Count;
                            LoadData();
                            //if (pID == "0")
                            //{
                            //    trvMenu.Nodes[iNodes_index].Nodes.Add(txtCaption.Text.Trim());
                            //    trvMenu.Nodes[iNodes_index].LastNode.Name = txtMenuID.Text.Trim() + "//" + txtMenuID.Text.Trim() + "//";
                            //    trvMenu.SelectedNode = trvMenu.Nodes[iNodes_index].LastNode;
                            //}
                            //else
                            //{
                            //    trvMenu.Nodes["1052//7906//"].Nodes.Add(txtCaption.Text.Trim());
                            //    trvMenu.Nodes[iNodes_index].LastNode.Name = txtMenuID.Text.Trim() + "//" + txtMenuID.Text.Trim() + "//";
                            //    trvMenu.SelectedNode = trvMenu.Nodes[iNodes_index].LastNode;
                                
                            //}                           
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Insert data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            cmdAdd.Enabled = false; cmdEdit.Enabled = false; cmdSave.Enabled = true;
                            cbChannel.Enabled = false; trvMenu.Enabled = true;
                            LockTextBox(false);
                        }
                    }
                    else 
                    {
                        objInfo.ID = Convert.ToInt32(pID);
                        if (objMenu.UpdateMENU(objInfo) == 1) 
                        {
                            MessageBox.Show("Update data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            cmdAdd.Enabled = true; cmdEdit.Enabled = true; cmdSave.Enabled = false;
                            cbChannel.Enabled = true; trvMenu.Enabled = true;
                            LockTextBox(true);
                            Load_data();
                        }
                        else 
                        {
                            MessageBox.Show("Update data successfully!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            cmdAdd.Enabled = false; cmdEdit.Enabled = false; cmdSave.Enabled = true;
                            cbChannel.Enabled = false; trvMenu.Enabled = true;
                            LockTextBox(false);
                        }
                    }
                }
                else
                {
                    cmdSave.Enabled = true; cmdAdd.Enabled = false; cmdEdit.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Check_data()
        {
            try
            {
                bool bResult = true;
                if (txtAssembly.Text.Trim() != "") { if (chkAlt.Checked == false && chkCtrl.Checked == false) { bResult = false; MessageBox.Show("Phai lua chon Check Box Alt hoac Ctrl!"); } }
                if (txtCaption.Text.Trim() == "") { bResult = false; txtCaption.Focus(); MessageBox.Show("Chua nhap Ten menu!"); }
                if (txtOrderMenu.Text.Trim() == "") { bResult = false; txtOrderMenu.Focus(); MessageBox.Show("Chua nhap thu tu menu!"); }
                int iResult = 0;
                iResult = objMenu.Check_input(txtParentID.Text.Trim());
                if (iResult == -1)
                {
                    if (txtParentID.Text.Trim() != "0000") 
                    { 
                        //MessageBox.Show("kho"); 
                        bResult = false; txtParentID.Focus(); }
                }
                iResult = 0;
                iResult = objMenu.Check_inputs( txtMenuID.Text.Trim());
                if (iResult == -1) 
                {
                    if (pMenuid != txtMenuID.Text.Trim())
                    {
                        //MessageBox.Show("Ton"); 
                        bResult = false; txtMenuID.Focus();
                    }
                }
                iResult = 0;
                iResult = objMenu.Check_inputst(txtAssembly.Text.Trim());
                if (iResult == -1) 
                {
                    if (pAssembly.Trim() != txtAssembly.Text.Trim())
                    {
                        MessageBox.Show("Assembly has already existed!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        bResult = false; txtAssembly.Focus();
                    }
                }
                return bResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }        

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (BR.BRLib.Common.iSconfirm == 1)
                {
                    Save_data();
                }
                else
                {
                    if (pNode_name == "Menu") { cmdEdit.Enabled = false; } else { cmdEdit.Enabled = true; }
                    cmdAdd.Enabled = true; cmdSave.Enabled = false;
                    LockTextBox(true);
                    trvMenu.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            trvMenu.Focus();
        }

        private void LockTextBox(Boolean a)
        {            
            txtAssembly.ReadOnly = a;
            txtAssemblyFile.ReadOnly = a;
            txtCaption.ReadOnly = a;
            txtKey.ReadOnly = a;
            //txtMenuID.ReadOnly = a;
            txtOrderMenu.ReadOnly = a;
            //txtParentID.ReadOnly = a;
            txtTooltipText.ReadOnly = a;
            chkEnable.Enabled = !a;
            chkCtrl.Enabled = !a;
            chkAlt.Enabled = !a;
            cbMethod.Enabled = !a;
            cbChannel.Enabled = !a;
        }

        private void ClearText()
        {
            txtTooltipText.Text = "";
            txtParentID.Text = "";
            txtOrderMenu.Text = "";  
            txtMenuID.Text = "";
            txtKey.Text = "";            
            txtCaption.Text = "";
            txtAssemblyFile.Text = "";
            txtAssembly.Text = "";
            chkAlt.Checked = false;
            chkCtrl.Checked = false;
            chkEnable.Checked = false;
        }

        #region Kiểm tra Mã Message Name đã có trong DB chưa
        private bool IDIsExisting()
        {
            //try
            //{
            //    string ID = txtMenuID.Text;
            //    DataAcess dacess = new DataAcess();
            //    string strSQL = "Select MENUID  from MENU WHERE MENUID ='" + ID + "'";
            //    OracleDataAdapter da = new OracleDataAdapter();
            //    da = dacess.Return_Adapter(strSQL);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
                    return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}
            //catch
            //{
            //    MessageBox.Show("Khong co thong tin can tim.");
            //    return false;
            //}
        }

        private bool CheckID()
        {
            bool result = true;
            string ID = txtMenuID.Text;
            if (String.IsNullOrEmpty(ID))
            {
                ID = "Ban phai nhap Message definition ID!";
                result = false;
            }
            else if (ID.Length > 20)
            {
                MessageBox.Show("Message Name phai co toi da 20 ky tu.");
                result = false;
            }
            //else if (IDIsExisting())
            //{
            //    MessageBox.Show("Message Name da ton tai.");
            //    txtMenuID.Text = "";
            //    txtMenuID.Focus();
            //    cmdSave.Enabled = true;
            //    cmdAdd.Enabled = false;
            //    result = false;
            //    if (!result)
            //    {
            //        MessageBox.Show("Try again!");
            //    }

            //}
            return result;
        }
        #endregion

        private void LoadData()
        {
            try
            {
                trvMenu.Nodes.Clear();
                TreeNode[] parent = new TreeNode[100];
                parent[1] = new TreeNode("Menu");
                parent[1].Name = "0//0000//";
                parent[1].ForeColor = Color.Black;
                trvMenu.Nodes.Add(parent[1]);
                MenuItem(parent[1], "0000");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItem(TreeNode ptrvmenu, string strParentID)
        {
            try
            {               
                TreeNode[] parent = new TreeNode[100];
                DataTable datMenu = new DataTable();
                datMenu = objMenu.Menu_get( strParentID.Trim());
                int b = 0;
                while (b < datMenu.Rows.Count)
                {
                    string strCaption = datMenu.Rows[b]["caption"].ToString();
                    string strID = datMenu.Rows[b]["ID"].ToString().Trim();
                    string strmenuid = datMenu.Rows[b]["menuid"].ToString().Trim();
                    string strAssembly = datMenu.Rows[b]["ASSEMBLY"].ToString().Trim();
                    string pNode_group = strID + "//" + strmenuid + "//" + strAssembly;
                    parent[b] = new TreeNode(strCaption);
                    parent[b].Name = pNode_group;                   
                    parent[b].ForeColor = Color.Black;
                    ptrvmenu.Nodes.Add(parent[b]);
                    Submenu(parent[b], strmenuid);
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Submenu(TreeNode Node, string strParentID)
        {
            try
            {
                //string pChannel="";
                //if (cbChannel.Text.Trim().ToUpper() == "SYSTEM") { pChannel = "GWSYSTEM"; }
                //else { pChannel = cbChannel.Text.Trim().ToUpper(); }
                TreeNode[] parent = new TreeNode[100];
                DataTable datMenu = new DataTable();
                datMenu = objMenu.Menu_get( strParentID.Trim());
                int b = 0;
                while (b < datMenu.Rows.Count)
                {
                    string strCaption = datMenu.Rows[b]["caption"].ToString();
                    string strID = datMenu.Rows[b]["ID"].ToString().Trim();
                    string strmenuid = datMenu.Rows[b]["menuid"].ToString().Trim();
                    string strAssembly = datMenu.Rows[b]["ASSEMBLY"].ToString().Trim();
                    string pNode_group = strID + "//" + strmenuid + "//" + strAssembly;
                    parent[b] = new TreeNode(strCaption);
                    parent[b].Name = pNode_group;
                    parent[b].ForeColor = Color.Black;
                    Node.Nodes.Add(parent[b]);
                    MenuItem(parent[b], strmenuid);
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtMenuID_Validated(object sender, EventArgs e)
        {
            if (txtMenuID.Text.Length > 4)
            {
                MessageBox.Show("The maximium length of Menu ID is 4", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                txtMenuID.Focus();
            }
        }

        private void txtParentID_Validated(object sender, EventArgs e)
        {
            if (txtParentID.Text.Length > 4)
            {
                MessageBox.Show("The maximium length of Parent ID is 4", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                txtParentID.Focus();
            }
        }

        private void txtCaption_Validated(object sender, EventArgs e)
        {
            if (txtCaption.Text.Length > 30)
            {
                MessageBox.Show("The maximium length of Caption is 30", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);               
                txtCaption.Focus();
            }
        }

        private void txtAssembly_Validated(object sender, EventArgs e)
        {
            if (txtAssembly.Text.Length > 100)
            {
                MessageBox.Show("The maximium length of Assembly is 100", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);               
                txtAssembly.Focus();
            }
        }

        private void txtAssemblyFile_Validated(object sender, EventArgs e)
        {
            if (txtAssemblyFile.Text.Length > 100)
            {
                MessageBox.Show("The maximium length of Assembly File is 100", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                txtAssemblyFile.Focus();
            }
        }

        private void txtMethod_Validated(object sender, EventArgs e)
        {
            //if (txtMethod.Text.Length > 30)
            //{
            //    MessageBox.Show("The maximium length of Method is 30", "Gateway", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtMethod.Text = "";
            //    txtMethod.Focus();
            //}
        }

        private void txtEnable_Validated(object sender, EventArgs e)
        {
            //if (!checkInput.IsNumeric(txtEnable.Text))
            //{
            //    MessageBox.Show("You must input a number.");
            //    txtEnable.Text = "";
            //    txtEnable.Focus();
            //}
            //else if (txtEnable.Text.Length > 1)
            //{
            //    MessageBox.Show("The maximium length of Enable is 1", "Gateway", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtEnable.Text = "";
            //    txtEnable.Focus();
            //}
        }

        private void txtCtrl_Validated(object sender, EventArgs e)
        {
            //if (!checkInput.IsNumeric(txtCtrl.Text))
            //{
            //    MessageBox.Show("You must input a number.");
            //    txtCtrl.Text = "";
            //    txtCtrl.Focus();
            //}
            //else if (txtCtrl.Text.Length > 1)
            //{
            //    MessageBox.Show("The maximium length of Ctrl is 1", "Gateway", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtCtrl.Text = "";
            //    txtCtrl.Focus();
            //}
        }

        private void txtAlt_Validated(object sender, EventArgs e)
        {
            //if (!checkInput.IsNumeric(txtAlt.Text))
            //{
            //    MessageBox.Show("You must input a number.");
            //    txtAlt.Text = "";
            //    txtAlt.Focus();
            //}
            //else if (txtAlt.Text.Length > 1)
            //{
            //    MessageBox.Show("The maximium length of  is 1", "Gateway", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtAlt.Text = "";
            //    txtAlt.Focus();
            //}
        }

        private void txtKey_Validated(object sender, EventArgs e)
        {
            if (txtKey.Text.Length > 5)
            {
                MessageBox.Show("The maximium length of Key is 5", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKey.Text = "";
                txtKey.Focus();
            }
        }

        private void txtGWType_Validated(object sender, EventArgs e)
        {
            //if (txtGWType.Text.Length > 10)
            //{
            //    MessageBox.Show("The maximium length of Gateway Type is 10", "Gateway", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtGWType.Text = "";
            //    txtGWType.Focus();
            //}
        }

        private void txtOptionData_Validated(object sender, EventArgs e)
        {
            //if (txtOptionData.Text.Length > 10)
            //{
            //    MessageBox.Show("The maximium length of Option Data is 10", "Gateway", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtOptionData.Text = "";
            //    txtOptionData.Focus();
            //}
        }

        private void txtTooltipText_Validated(object sender, EventArgs e)
        {
            if (txtTooltipText.Text.Length > 100)
            {
                MessageBox.Show("The maximium length of Tooltip Text is 100", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTooltipText.Text = "";
                txtTooltipText.Focus();
            }
        }

        private void txtOrderMenu_Validated(object sender, EventArgs e)
        {
            if (!checkInput.IsNumeric(txtOrderMenu.Text))
            {
                MessageBox.Show("You must input a number.");
                txtOrderMenu.Text = "";
                txtOrderMenu.Focus();
            }
            else if (txtOrderMenu.Text.Length > 3)
            {
                MessageBox.Show("The maximium length of Order Menu is 3", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrderMenu.Text = "";
                txtOrderMenu.Focus();
            }
        }

        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void trvMenu_Click(object sender, EventArgs e)
        {
           
        }

        private void trvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {  
                pNode_name = e.Node.Text.Trim();
                iNodes_index = e.Node.Index;
                if (e.Node.Text.Trim() != "Menu")
                {
                    String[] M = e.Node.Name.Split(new String[] { "//" }, StringSplitOptions.None);//cat chuoi
                    pID = M[0];
                    pMenuid = M[1];
                    pAssembly = M[2];
                    Get_data(pMenuid);
                    cmdDelete.Enabled = true;
                    cmdEdit.Enabled = true;
                }
                else
                {
                    String[] M = e.Node.Name.Split(new String[] { "//" }, StringSplitOptions.None);//cat chuoi
                    pID = M[0];
                    pMenuid = M[1];
                    cmdDelete.Enabled = false;
                    cmdEdit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Get_data(string pMenuid)
        {
            try
            {
                DataTable _dt = new DataTable();
                _dt = objMenu.Menu_data(pMenuid);
                if (_dt != null)
                {
                    txtMenuID.Text = pMenuid;
                    txtParentID.Text = _dt.Rows[0]["PARENTID"].ToString();
                    txtCaption.Text = _dt.Rows[0]["CAPTION"].ToString();
                    txtAssembly.Text = _dt.Rows[0]["ASSEMBLY"].ToString();
                    txtAssemblyFile.Text = _dt.Rows[0]["ASSEMBLYFILE"].ToString();
                    if (_dt.Rows[0]["ENABLE"].ToString().Trim() == "1") { chkEnable.Checked = true; }
                    else { chkEnable.Checked = false; }
                    if (_dt.Rows[0]["ALT"].ToString().Trim() == "1") { chkAlt.Checked = true; }
                    else { chkAlt.Checked = false; }
                    if (_dt.Rows[0]["CTRL"].ToString().Trim() == "1") { chkCtrl.Checked = true; }
                    else { chkCtrl.Checked = false; }
                    cbMethod.Text = _dt.Rows[0]["METHOD"].ToString();
                    txtKey.Text = _dt.Rows[0]["KEY"].ToString();
                    txtTooltipText.Text = _dt.Rows[0]["TOOLTIPTEXT"].ToString();
                    txtOrderMenu.Text = _dt.Rows[0]["ORDERMENU"].ToString();
                    if (_dt.Rows[0]["GWTYPE"].ToString().ToUpper() == "GWSYSTEM") { cbChannel.Text = "SYSTEM"; }
                    else
                    { cbChannel.Text = _dt.Rows[0]["GWTYPE"].ToString(); }
                  }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMenuID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string pMenuid = "";
                if (Regex.IsMatch(txtMenuID.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                { pMenuid = txtMenuID.Text.Trim(); }
                else
                { txtMenuID.Text = pMenuid; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void txtParentID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string pParentID = "";
                if (Regex.IsMatch(txtParentID.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                { pParentID = txtParentID.Text.Trim(); }
                else
                { txtParentID.Text = pParentID; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOrderMenu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string pOrderMenu = "";
                if (Regex.IsMatch(txtOrderMenu.Text.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                { pOrderMenu = txtOrderMenu.Text.Trim(); }
                else
                { txtOrderMenu.Text = pOrderMenu; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
