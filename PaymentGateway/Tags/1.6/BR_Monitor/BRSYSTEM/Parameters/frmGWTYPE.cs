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

namespace BR.BRSYSTEM
{
    public partial class frmGWTYPE : frmBasedata
    {
        #region
        public static bool isInsert = false;
        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private GWTYPEInfo objInfo = new GWTYPEInfo();
        private GWTYPEController objControl = new GWTYPEController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private clsCheckInput checkInput = new clsCheckInput();
        private int iID = 0;
        private int iRows;
        //private static bool strSucess = false;        
        private DataSet datDs = new DataSet();
        #endregion

        public frmGWTYPE()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                isInsert = true;
                frmGWTypeInfo GWTypeUpdate = new frmGWTypeInfo();
                GWTypeUpdate.ShowDialog();
                cmdAdd.Enabled = true; cmdEdit.Enabled = true;
                cmdSave.Enabled = false; cmdDelete.Enabled = true;
                LoadData();
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
                if (grdList.CurrentRow == null)
                {
                    grdList.Focus();
                }
                isInsert = false;
                frmGWTypeInfo cur = new frmGWTypeInfo();
                //----------------------------------------------------------------------
                cur.strGWTYPEID = grdList.Rows[iRows].Cells["GWTYPEID"].Value.ToString();
                cur.strGWTYPE = grdList.Rows[iRows].Cells["GWTYPE"].Value.ToString();
                cur.strGWTYPESTS = grdList.Rows[iRows].Cells["GWTYPESTS"].Value.ToString();
                cur.strMSG_NO = grdList.Rows[iRows].Cells["MSG_NO"].Value.ToString();
                cur.strDESCRIPTION = grdList.Rows[iRows].Cells["DESCRIPTION"].Value.ToString();
                cur.strCONNECTION = grdList.Rows[iRows].Cells["CONTENT"].Value.ToString();
                cur.strDBLINK = grdList.Rows[iRows].Cells["DBLINK"].Value.ToString();
                //-------------------------------------------------------------------------------
                cur.ShowDialog();
                LoadData();
                cboGWType.SelectedIndex = 0;
                cboConnect.SelectedIndex = 0;
                cmdAdd.Enabled = true;
                cmdEdit.Enabled = true;
                cmdSave.Enabled = false;
                cmdDelete.Enabled = true;

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
                if (Common.iSconfirm == 1)
                {
                    iID = Convert.ToInt32(grdList.Rows[iRows].Cells[0].Value.ToString());
                    string strGWTYPE = grdList.Rows[iRows].Cells[1].Value.ToString();
                    //CLOSED la trang thai bi khoa
                    if (grdList.Rows[iRows].Cells["GWTYPESTS"].Value.ToString() != "CLOSED")
                    {
                        Common.ShowError("Can not delete because the status of channel is not closed!",
                            3, MessageBoxButtons.OK);
                        cmdAdd.Enabled = true;
                        cmdEdit.Enabled = true;
                        cmdDelete.Enabled = true;
                    }
                    else
                    {
                        objControl = new GWTYPEController();
                        //Check xem con du lieu thuoc kenh nay ko?
                        if (!objControl.CheckChannelData(strGWTYPE))
                        {
                            objControl.DeleteGWTYPE(iID);
                            Common.ShowError("Data has deleted successfully!", 1, MessageBoxButtons.OK);

                            #region lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "Channel Parameter";
                            int Log_level = 1;
                            string strWorked = "Delete";
                            string strTable = "GWTYPE";
                            string strOld_value = iID.ToString() + "/" + strGWTYPE;
                            string strNew_value = "";
                            //-----------------------------------------
                            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                strWorked, strTable, strOld_value, strNew_value);
                            #endregion
                        }
                        else
                            Common.ShowError("Data existed many table.Please check again!",
                                3, MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }
                LoadData();
                cmdAdd.Enabled = true; cmdEdit.Enabled = true;
                cmdDelete.Enabled = true; cmdSave.Enabled = false;
            }
            catch
            {
                Common.ShowError("You must choose a row!", 3, MessageBoxButtons.OK);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGWTYPE_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
                this.Text = "Channel management";
                LoadData();
                Getcombo();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Getcombo()
        {
            try
            {
                //---lay du lieu len combobox GWTYPE
                DataSet dsAllcode = new DataSet();
                dsAllcode = datDs;
                cboGWType.Items.Add("ALL");
                int j = 0;
                while (j < dsAllcode.Tables[0].Rows.Count)
                {
                    string strContent = dsAllcode.Tables[0].Rows[j]["GWTYPE"].ToString();
                    string strCdval = dsAllcode.Tables[0].Rows[j]["GWTYPEID"].ToString();
                    cboGWType.Items.Add(strContent);
                    cboGWType.ValueMember = strCdval;
                    j = j + 1;
                }
                cboGWType.SelectedIndex = 0;
                //Load data cboGWType
                if (!objGetData.FillDataComboBox(cboConnect, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='SYSTEM' AND CDNAME='Connection'", "CONTENT", true, true, "ALL"))
                    return;
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
                string strGWType; string strConnect;
                if (cboGWType.Text == "ALL" || cboGWType.Text == "") 
                    strGWType = "";
                else 
                    strGWType = "  and upper(trim(gwt.GWTYPE)) like '%" + cboGWType.Text.Trim().ToUpper() + "%'";
                if (cboConnect.Text == "ALL" || cboConnect.Text == "") 
                    strConnect = "";
                else 
                    strConnect = " and  upper(trim(gwt.conection)) like '%" + cboConnect.SelectedIndex + "" + "%'";

                string strWHERE = strGWType + strConnect;

                datDs = objControl.GetGWTYPESearch(strWHERE);
                if (datDs.Tables[0].Rows.Count == 0)
                {   
                    Common.ShowError("There are no suitable channel to display!", 7, MessageBoxButtons.OK);
                    LoadGird(datDs);
                }
                else
                {
                    LoadGird(datDs);
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmGWTypeInfo cur = new frmGWTypeInfo();
                cur.strGWTYPEID = grdList.Rows[iRows].Cells["GWTYPEID"].Value.ToString();
                cur.strGWTYPE = grdList.Rows[iRows].Cells["GWTYPE"].Value.ToString();
                cur.strCONNECTION = grdList.Rows[iRows].Cells["CONTENT"].Value.ToString();
                cur.strGWTYPESTS = grdList.Rows[iRows].Cells["GWTYPESTS"].Value.ToString();
                cur.strMSG_NO = grdList.Rows[iRows].Cells["MSG_NO"].Value.ToString();
                cur.strDESCRIPTION = grdList.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                cur.strDBLINK = grdList.Rows[iRows].Cells["DBLINK"].Value.ToString();
                cur.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            LoadData();
           
        }
        private void LoadGird(DataSet datSearch)
        {
            try
            {
                grdList.DataSource = datSearch.Tables[0];
                grdList.Columns["GWTYPEID"].HeaderText = "Channel ID";
                grdList.Columns["GWTYPEID"].Width = 65;
                grdList.Columns["GWTYPEID"].Visible = false;
                grdList.Columns["GWTYPE"].HeaderText = "Channel name";
                grdList.Columns["GWTYPE"].Width = 100;
                grdList.Columns["ENCRYPT"].HeaderText = "Encrypt";
                grdList.Columns["ENCRYPT"].Width = 70;
                grdList.Columns["CONTENT"].HeaderText = "Connection";
                grdList.Columns["CONTENT"].Width = 100;
                grdList.Columns["GWTYPESTS"].HeaderText = "Channel status";
                grdList.Columns["GWTYPESTS"].Width = 110;
                grdList.Columns["GWTYPESTS1"].Visible = false;
                grdList.Columns["MSG_NO"].HeaderText = "Message number";
                grdList.Columns["MSG_NO"].Width = 100;
                grdList.Columns["DESCRIPTION"].HeaderText = "Description";
                grdList.Columns["DESCRIPTION"].Width = 200;
                grdList.Columns["DBLINK"].HeaderText = "DBlink";
                grdList.Columns["DBLINK"].Width = 200;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCD_FormClosing(null,null);
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (cboGWType.Focused)
                {
                    cboConnect.Focus();
                    cboConnect.SelectAll();
                }
                else if (cboConnect.Focused)
                {
                    cmdSearch.Focus();
                    cmdSearch_Click(null, null);
                }
                //strSucess = true;
            }
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {                
                Common.ShowError(ex.Message, 1, MessageBoxButtons.OK);
            }
        }

        private void grdList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) { iRows = e.RowIndex; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmGWTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmGWTYPE_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

    }
}
