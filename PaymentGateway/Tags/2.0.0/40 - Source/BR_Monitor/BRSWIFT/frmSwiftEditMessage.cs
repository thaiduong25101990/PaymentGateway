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

namespace BR.BRSWIFT
{
    public partial class frmSwiftEditMessage : Form
    {
        public  string _Table = "";//bien luu gia tri dien do thuoc bang nao        
        public  string _Content = "";
        public  int _queryid = 0;
        public string _Processsts = "";
        private int iRows;
        private string _OrgContent = "";
        public bool bIsCloseForm = false;
        private bool iClose = false;
        private static bool strSucess = false;
        private bool NeedConfirm = true;
        public string _Msg_type = "";

        SWIFT_MSG_CONTENTInfo objInfo = new SWIFT_MSG_CONTENTInfo();
        SWIFT_MSG_CONTENTController objControl = new SWIFT_MSG_CONTENTController();

        SWIFT_MSG_LOGInfo objSWIFT_log = new SWIFT_MSG_LOGInfo();
        SWIFT_MSG_LOGController objcontrolSWIFT_log = new SWIFT_MSG_LOGController();

        public frmSwiftEditMessage()
        {
            InitializeComponent();
        }

        private void frmSwiftEditMessage_Load(object sender, EventArgs e)
        {
            try
            {
                Load_data();
                cmdAdd.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Load_data()
        {
            try
            {                
                if (_Table == "1")
                {
                    objInfo.Table_Name = "SWIFT_MSG_CONTENT";
                }
                else if (_Table == "2")
                {
                    objInfo.Table_Name = "SWIFT_MSG_ALL";
                }
                else if (_Table == "3")
                {
                    objInfo.Table_Name = "SWIFT_MSG_ALL_HIS";
                }
                if (_Processsts == Common.PROCESSSTS_REPAIR)//Neu dien da sua roi
                {
                    Load_Message_edit();
                }
                else//Neu dien chua sua lan nao
                {
                    Load_Message_New();
                }
                
                DataTable _dt = new DataTable();
                _dt = objControl.GET_MAP_FIELD(_Msg_type);
                Log_message(_dt);
                dtgEditContent.Rows[0].ReadOnly = true;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Log_message(DataTable _dt)
        {
            try
            {
                int vCount = 0;
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        string vField = _dt.Rows[i]["FIELD_CODE"].ToString().Trim();
                        for (int j = 0; j < dtgEditContent.Rows.Count; j++)
                        {
                            if (j == 0 || j == dtgEditContent.Rows.Count - 1)
                            {
                                dtgEditContent.Rows[j].ReadOnly = true;
                            }
                            else
                            {
                                string pValue = dtgEditContent.Rows[j].Cells["EditContent"].Value.ToString().Trim();
                                if (pValue == "")
                                {
                                    pValue = " ";
                                }

                                if (pValue.Substring(0, 1) == ":")
                                {
                                    if (pValue.Substring(1, vField.Length) == vField)
                                    {
                                        dtgEditContent.Rows[j].ReadOnly = true;
                                        vCount = 1;
                                    }
                                    else
                                    {
                                        vCount = 0;
                                    }
                                }
                                else
                                {
                                    if (vCount == 1)//Co the co dong thu hai cua field do
                                    {
                                        dtgEditContent.Rows[j].ReadOnly = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < dtgEditContent.Rows.Count; j++)
                    {
                        dtgEditContent.Rows[j].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Load_Message_edit()
        {
            try
            {
                int count = 0;
                if (dtgOrgContent.Rows.Count > 0) { dtgOrgContent.Rows.Clear(); }
                if (dtgEditContent.Rows.Count > 0) { dtgEditContent.Rows.Clear(); }
                DataTable _dt = new DataTable();
                _dt = objControl.GET_SWIFT_MSG_EDIT(_queryid);
                if (_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (dtgOrgContent.Rows.Count == 0)
                        {
                            /*Luoi dien Old*/
                            dtgOrgContent.Rows.Add();
                            dtgOrgContent.Rows[0].Cells["OrgContent"].Value = _dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString();
                            dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                            if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString() == "ADD")
                            {
                                dtgOrgContent.Rows[0].Visible = false;
                            }
                            /*Luoi dien Edit*/
                            dtgEditContent.Rows.Add();
                            dtgEditContent.Rows[0].Cells["EditContent"].Value = _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString();
                            //dtgEditContent.Columns["EditContent"].ReadOnly = true;/*Dau dien khong duoc sua*/
                            if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString().Trim() != _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString().Trim())
                            {
                                dtgEditContent.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            count = dtgOrgContent.Rows.Count;
                            int k = 0;
                            while (k < count)
                            {
                                if (k == count - 1)
                                {
                                    /*Luoi dien Old*/
                                    dtgOrgContent.Rows.Add();
                                    dtgOrgContent.Rows[count].Cells["OrgContent"].Value = _dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString();
                                    dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                                    if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString() == "ADD")
                                    {
                                        dtgOrgContent.Rows[count].Visible = false;
                                    }
                                    /*Luoi dien Edit*/
                                    dtgEditContent.Rows.Add();
                                    dtgEditContent.Rows[count].Cells["EditContent"].Value = _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString();
                                    if (_dt.Rows[i]["FIELD_CONTENT_ORIGIN"].ToString().Trim() != _dt.Rows[i]["FIELD_CONTENT_EDIT"].ToString().Trim())
                                    {
                                        dtgEditContent.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                }
                                k = k + 1;
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

        private void Load_Message_New()
        {
            try
            {
                int count = 0;
                objInfo.QUERY_ID = _queryid;
                DataTable _dt = new DataTable();
                _dt = objControl.GET_MESSAGE_EDIT(objInfo);
                string chrOrgContent = _dt.Rows[0]["CONTENT"].ToString();
                if (dtgOrgContent.Rows.Count > 0)
                {
                    dtgOrgContent.Rows.Clear();
                }
                if (dtgEditContent.Rows.Count > 0)
                {
                    dtgEditContent.Rows.Clear();
                }
                String[] N = chrOrgContent.Split(new String[] { "\r\n" }, StringSplitOptions.None);//cat chuoi
                for (int i = 0; i < N.Count<String>(); i++)
                {
                    if (dtgOrgContent.Rows.Count == 0)
                    {
                        /*Luoi dien Old*/
                        dtgOrgContent.Rows.Add();
                        dtgOrgContent.Rows[0].Cells["OrgContent"].Value = N[i];
                        dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                        /*Luoi dien Edit*/
                        dtgEditContent.Rows.Add();
                        dtgEditContent.Rows[0].Cells["EditContent"].Value = N[i];
                    }
                    else
                    {
                        count = dtgOrgContent.Rows.Count;
                        int k = 0;
                        while (k < count)
                        {
                            if (k == count - 1)
                            {
                                /*Luoi dien Old*/
                                dtgOrgContent.Rows.Add();
                                dtgOrgContent.Rows[count].Cells["OrgContent"].Value = N[i];
                                dtgOrgContent.Columns["OrgContent"].ReadOnly = true;
                                /*Luoi dien Edit*/
                                dtgEditContent.Rows.Add();
                                dtgEditContent.Rows[count].Cells["EditContent"].Value = N[i];
                            }
                            k = k + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            iClose = true; this.Close(); iClose = false;
        }

        private void cmsSave_Click(object sender, EventArgs e)
        {
            try
            {
                /*Kiem tra dien do xem trang thai do thay doi hay chua*/

                Save_data();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Save_data()
        {
            try
            {
                objInfo.PROCESSSTS = _Processsts;
                string OrgContent = "";
                string EditContent = "";
                /*Lay du lieu insert vao bang SWIFT_MSG_EDIT*/
                for (int i = 0; i < dtgOrgContent.Rows.Count; i++)
                {
                    if (dtgOrgContent.Rows[i].Cells["OrgContent"].Value.ToString().Trim() != "ADD")
                    {
                        if (i == dtgOrgContent.Rows.Count - 1)
                        {
                            OrgContent = OrgContent + dtgOrgContent.Rows[i].Cells["OrgContent"].Value.ToString().Trim().Replace("\r\n", "");
                        }
                        else
                        {
                            OrgContent = OrgContent + dtgOrgContent.Rows[i].Cells["OrgContent"].Value.ToString().Replace("\r\n", "").Trim() + "\r\n";
                        }
                    }
                }
                /*Lay du lieu insert vao bang SWIFT_MSG_EDIT*/
                for (int i = 0; i < dtgEditContent.Rows.Count; i++)
                {
                    if (dtgEditContent.Rows[i].Cells["EditContent"].Value != null)
                    {
                        if (dtgEditContent.Rows[i].Cells["EditContent"].Value.ToString().Trim() != "")
                        {

                            if (i == dtgEditContent.Rows.Count - 1)
                            {
                                EditContent = EditContent + dtgEditContent.Rows[i].Cells["EditContent"].Value.ToString().Replace("\r\n", "").Trim();
                            }
                            else
                            {
                                EditContent = EditContent + dtgEditContent.Rows[i].Cells["EditContent"].Value.ToString().Trim().Replace("\r\n", "") + "\r\n";
                            }
                        }
                    }
                }
                objInfo.QUERY_ID = _queryid;
                objInfo.CONTENT = EditContent;                
                objInfo.CONTENT_ORIGIN = OrgContent;
                objInfo.Table_Name = _Table;
                if (objControl.UPDATE_MSG_EDIT(objInfo,Common.PROCESSSTS_REPAIR) == -1)
                {
                    MessageBox.Show("Can not insert data", Common.sCaption);
                }
                else if (objControl.UPDATE_MSG_EDIT(objInfo, Common.PROCESSSTS_REPAIR) == 5)
                {
                    MessageBox.Show("message is being processed by other user!" + "\r\n" + "You are refresh data", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //Common.ShowError("message is being processed by other user!" + "\r\n" + "You are refresh data", 3, MessageBoxButtons.OK);                           
                }
                else
                {
                    string vOld = "";
                    string vEdit = "";
                    for (int p = 0; p < dtgOrgContent.Rows.Count; p++)
                    {
                        vOld = dtgOrgContent.Rows[p].Cells["OrgContent"].Value.ToString().Trim();
                        if (dtgEditContent.Rows[p].Cells["EditContent"].Value == null)
                        {
                            vEdit = "";
                        }
                        else
                        {
                            vEdit = dtgEditContent.Rows[p].Cells["EditContent"].Value.ToString().Trim();
                        }
                        if (objControl.INSERT_MESSAGE_EDIT(_queryid, vOld, vEdit, p) == -1)
                        {
                            MessageBox.Show("Can not insert data", Common.sCaption);
                        }
                    }
                    objSWIFT_log.DESCRIPTIONS = Common.Userid + ": Edit message";
                    objSWIFT_log.LOG_DATE = DateTime.Now;
                    objSWIFT_log.QUERY_ID = _queryid;
                    objSWIFT_log.STATUS = 1;
                    objcontrolSWIFT_log.ADD_SWIFT_MSG_LOG(objSWIFT_log);
                    MessageBox.Show("Repair message successfull!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iClose = true; this.Close(); iClose = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtgEditContent_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdAdd.Enabled = true;
                if (e.RowIndex == -1) { iRows = 0; }
                else
                {
                    iRows = e.RowIndex;
                }
                if (dtgEditContent.Rows[iRows].ReadOnly == true)
                {
                    cmdAdd.Enabled = false;
                }
                else
                {
                    cmdAdd.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dtgRowE = new DataGridViewRow();
                DataGridViewRow dtgRowO = new DataGridViewRow();
                dtgEditContent.Rows.Insert(iRows + 1, dtgRowE);
                dtgOrgContent.Rows.Insert(iRows + 1, dtgRowO);                            
                dtgOrgContent.Rows[iRows + 1].Cells["OrgContent"].Value = "ADD";
                dtgEditContent.Rows[iRows + 1].Cells["EditContent"].Value = "";
                dtgOrgContent.Rows[iRows + 1].Visible = false;
                string ff = dtgOrgContent.Rows[iRows+1].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgOrgContent.Rows[iRows].Cells["OrgContent"].Value.ToString().Trim() == "ADD")
                {
                    dtgOrgContent.Rows.RemoveAt(iRows);
                    dtgEditContent.Rows.RemoveAt(iRows);
                }
                else
                {
                    MessageBox.Show("You can not delete this row!",Common.sCaption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            
        }

        private void dtgEditContent_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            #region TransDate
            if (dtgOrgContent.Rows[iRows].Cells["OrgContent"].Value.ToString() == "Add")
            {
                dtgEditContent.Rows[iRows].DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                if (dtgEditContent.Rows[iRows].Cells["EditContent"].Value != null)
                {
                    if (dtgOrgContent.Rows[iRows].Cells["OrgContent"].Value.ToString().Trim() != dtgEditContent.Rows[iRows].Cells["EditContent"].Value.ToString())
                    {
                        dtgEditContent.Rows[iRows].DefaultCellStyle.ForeColor = Color.Red;
                        dtgEditContent.Refresh();
                    }
                    else
                    {
                        dtgEditContent.Rows[iRows].DefaultCellStyle.ForeColor = Color.Black;
                        dtgEditContent.Refresh();
                    }
                }
            }
            #endregion
        }

        private void frmSwiftEditMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iClose == false)
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                    {
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to close all SWIFT Message Information windows ?", Common.sCaption);
                        if (e.Cancel == true)
                        {
                            iClose = false;
                            bIsCloseForm = false;
                        }
                        else
                        {
                            bIsCloseForm = true;
                        }
                    }
                }
                else
                {
                    bIsCloseForm = false;
                }
            }
        }
    }
}
