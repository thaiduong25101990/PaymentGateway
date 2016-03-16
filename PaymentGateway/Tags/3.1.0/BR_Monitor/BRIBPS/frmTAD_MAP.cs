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
using BR.BRSYSTEM;
using System.Text.RegularExpressions;

namespace BR.BRIBPS
{
    public partial class frmTAD_MAP : Form
    {
        TADMAPController objControl = new TADMAPController();
        TADMAPInfo objInfo = new TADMAPInfo();
        private int iSelect;
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new BR.BRLib.Common.DGVColumnHeader();
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader1 = new BR.BRLib.Common.DGVColumnHeader();
        private int iRows;
        private int iRows1;

        public frmTAD_MAP()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTAD_MAP_Load(object sender, EventArgs e)
        {
            try
            {
                dtgTAD.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dtgTAD.Columns[0].HeaderCell = dgvColumnHeader;
                dtgTAD.Columns[0].Width = 26;
                dtgTADMAP.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dtgTADMAP.Columns[0].HeaderCell = dgvColumnHeader1;
                dtgTADMAP.Columns[0].Width = 26;    
                Load_data();               
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
                Load_combobox();
                Load_datadridview();                
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Load_datadridview()
        {
            try
            {
                dtgTAD.Rows.Clear();
                dtgTADMAP.Rows.Clear();
                DataSet _ds = new DataSet();
                if (cboTADHO.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    String[] M = cboTADHO.SelectedValue.ToString().Split(new String[] { "#" }, StringSplitOptions.None);//cat chuoi
                    _ds = objControl.LOAD_DATA(M[0], M[1]);
                    if (_ds != null)
                    {
                        DataTable _dtTAD = new DataTable();
                        _dtTAD = _ds.Tables["TAD"];
                        int h = 0;
                        while (h < _dtTAD.Rows.Count)
                        {
                            dtgTAD.Rows.Add();
                            dtgTAD.Rows[h].Cells["SIBS_BANK_CODE"].Value = _dtTAD.Rows[h]["SIBS_BANK_CODE"].ToString();
                            dtgTAD.Rows[h].Cells["GW_BANK_CODE"].Value = _dtTAD.Rows[h]["GW_BANK_CODE"].ToString();
                            dtgTAD.Rows[h].Cells["BANK_NAME"].Value = _dtTAD.Rows[h]["BANK_NAME"].ToString();                            
                            h = h + 1;
                        }
                        h = 1;
                        if (_dtTAD.Rows.Count>0)
                        {
                            while (h < dtgTAD.Columns.Count)
                            {
                                dtgTAD.Columns[h].ReadOnly = true;
                                h = h + 1;
                            }
                        }
                        h = 0;
                        DataTable _dtTADMAP = new DataTable();
                        _dtTADMAP = _ds.Tables["TADMAP"];
                        while (h < _dtTADMAP.Rows.Count)
                        {
                            dtgTADMAP.Rows.Add();
                            dtgTADMAP.Rows[h].Cells["SIBS_BANK_CODE1"].Value = _dtTADMAP.Rows[h]["SIBS_BANK_CODE"].ToString();
                            dtgTADMAP.Rows[h].Cells["GW_BANK_CODE1"].Value = _dtTADMAP.Rows[h]["GW_BANK_CODE"].ToString();
                            dtgTADMAP.Rows[h].Cells["BANK_NAME1"].Value = _dtTADMAP.Rows[h]["BANK_NAME"].ToString();
                            h = h + 1;
                        }
                        h = 1;
                        if (_dtTADMAP.Rows.Count > 0)
                        {
                            while (h < dtgTADMAP.Columns.Count)
                            {
                                dtgTADMAP.Columns[h].ReadOnly = true;
                                h = h + 1;
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
       
        private void Load_combobox()
        {
            try
            {
                DataTable _dt = new DataTable();
                _dt = objControl.LOAD_COMBO();
                if (_dt.Rows.Count > 0)
                {
                    cboTADHO.DataSource = _dt;
                    cboTADHO.DisplayMember = "TAD";
                    cboTADHO.ValueMember = "GW_BANK_CODE";                    
                    cboTADHO.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cboTADHO_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_datadridview();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Check_Select_Rows(dtgTAD);
                if (iSelect == 1)
                {
                    int z = 0;
                    while (z < dtgTAD.Rows.Count)
                    {
                        if (dtgTAD.Rows[z].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dtgTAD.Rows[z].Cells[0].Value.ToString() == "True")
                            {
                                string strSIBS_BANK_CODE = dtgTAD.Rows[z].Cells["SIBS_BANK_CODE"].Value.ToString();
                                string strBANK_NAME = dtgTAD.Rows[z].Cells["BANK_NAME"].Value.ToString();
                                string strGW_BANK_CODE = dtgTAD.Rows[z].Cells["GW_BANK_CODE"].Value.ToString();
                                //Goi ham ad du lieu
                                Add_TADHO(strSIBS_BANK_CODE, strGW_BANK_CODE, strBANK_NAME, dtgTADMAP);
                                dtgTAD.Rows.RemoveAt(z);
                            }
                            else
                            {
                                z = z + 1;
                            }
                        }
                        else
                        {
                            z = z + 1;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void Add_TADHO(string strSIBS_BANK_CODE, string strGW_BANK_CODE, string strBANK_NAME, DataGridView _dtg)
        {
            try
            {
                int count;
                if (_dtg.Rows.Count == 0)
                {
                    _dtg.Rows.Add();
                    _dtg.Rows[0].Cells["SIBS_BANK_CODE1"].Value = strSIBS_BANK_CODE;
                    _dtg.Rows[0].Cells["BANK_NAME1"].Value = strBANK_NAME;
                    _dtg.Rows[0].Cells["GW_BANK_CODE1"].Value = strGW_BANK_CODE;
                }
                else
                {
                    count = _dtg.Rows.Count; int k = 0;
                    while (k < count)
                    {
                        if (k == count - 1)
                        {
                            _dtg.Rows.Add();
                            _dtg.Rows[count].Cells["SIBS_BANK_CODE1"].Value = strSIBS_BANK_CODE;
                            _dtg.Rows[count].Cells["BANK_NAME1"].Value = strBANK_NAME;
                            _dtg.Rows[count].Cells["GW_BANK_CODE1"].Value = strGW_BANK_CODE;
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



        private void Check_Select_Rows(DataGridView _dtg)
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < _dtg.Rows.Count)
                {
                    if (_dtg.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (_dtg.Rows[b].Cells[0].Value.ToString() == "True")
                        { iSelect = 1; break; }
                    }
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                int z = 0;
                while (z < dtgTAD.Rows.Count)
                {
                    string strSIBS_BANK_CODE = dtgTAD.Rows[z].Cells["SIBS_BANK_CODE"].Value.ToString();
                    string strBANK_NAME = dtgTAD.Rows[z].Cells["BANK_NAME"].Value.ToString();
                    string strGW_BANK_CODE = dtgTAD.Rows[z].Cells["GW_BANK_CODE"].Value.ToString();
                    //Goi ham ad du lieu
                    Add_TADHO(strSIBS_BANK_CODE, strGW_BANK_CODE,strBANK_NAME, dtgTADMAP);
                    dtgTAD.Rows.RemoveAt(z);                   
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
                Check_Select_Rows(dtgTADMAP);//dtgTADMAP  dtgTAD
                if (iSelect == 1)
                {
                    int z = 0;
                    while (z < dtgTADMAP.Rows.Count)
                    {
                        if (dtgTADMAP.Rows[z].Cells[0].Value != null)// hang duoc chon
                        {
                            if (dtgTADMAP.Rows[z].Cells[0].Value.ToString() == "True")
                            {
                                string strSIBS_BANK_CODE = dtgTADMAP.Rows[z].Cells["SIBS_BANK_CODE1"].Value.ToString();
                                string strBANK_NAME = dtgTADMAP.Rows[z].Cells["BANK_NAME1"].Value.ToString();
                                string strGW_BANK_CODE = dtgTADMAP.Rows[z].Cells["GW_BANK_CODE1"].Value.ToString();
                                //Goi ham ad du lieu
                                Add_TAD(strSIBS_BANK_CODE, strGW_BANK_CODE,strBANK_NAME, dtgTAD);
                                dtgTADMAP.Rows.RemoveAt(z);
                            }
                            else
                            {
                                z = z + 1;
                            }   
                        }
                        else
                        {
                            z = z + 1;
                        }                           
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void Add_TAD(string strSIBS_BANK_CODE, string strGW_BANK_CODE,
            string strBANK_NAME, DataGridView _dtg)
        {
            try
            {
                int count;
                if (_dtg.Rows.Count == 0)
                {
                    _dtg.Rows.Add();

                    _dtg.Rows[0].Cells["SIBS_BANK_CODE"].Value = strSIBS_BANK_CODE;
                    _dtg.Rows[0].Cells["BANK_NAME"].Value = strBANK_NAME;
                    _dtg.Rows[0].Cells["GW_BANK_CODE"].Value = strGW_BANK_CODE;
                }
                else
                {
                    count = _dtg.Rows.Count; int k = 0;
                    while (k < count)
                    {
                        if (k == count - 1)
                        {
                            _dtg.Rows.Add();
                            _dtg.Rows[count].Cells["SIBS_BANK_CODE"].Value = strSIBS_BANK_CODE;
                            _dtg.Rows[count].Cells["BANK_NAME"].Value = strBANK_NAME;
                            _dtg.Rows[count].Cells["GW_BANK_CODE"].Value = strGW_BANK_CODE;
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

        private void cmdRemoAll_Click(object sender, EventArgs e)
        {
            try
            {
                int z = 0;
                while (z < dtgTADMAP.Rows.Count)
                {
                    string strSISB_BANK_CODE = dtgTADMAP.Rows[z].Cells["SIBS_BANK_CODE1"].Value.ToString();
                    string strBANK_NAME = dtgTADMAP.Rows[z].Cells["BANK_NAME1"].Value.ToString();
                    string strGW_BANK_CODE = dtgTADMAP.Rows[z].Cells["GW_BANK_CODE1"].Value.ToString();
                    //Goi ham ad du lieu
                    Add_TAD(strSISB_BANK_CODE, strGW_BANK_CODE,strBANK_NAME, dtgTAD);
                    dtgTADMAP.Rows.RemoveAt(z);                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dtgTAD_CellEnter(object sender, DataGridViewCellEventArgs e)
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

        private void dtgTADMAP_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows1 = e.RowIndex; }
        }

        private void dtgTAD_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < dtgTAD.Rows.Count; i++)
                    {
                        dtgTAD.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                    }
                }               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }   
        }

        private void dtgTADMAP_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < dtgTADMAP.Rows.Count; i++)
                    {
                        dtgTADMAP.Rows[i].Cells[0].Value = dgvColumnHeader1.CheckAll;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {

                string pGW_BANK_CODE = cboTADHO.SelectedValue.ToString();

                String[] N = pGW_BANK_CODE.Split(new String[] { "#" }, StringSplitOptions.None);//cat chuoi

                objControl.DELETE_TADMAP(N[0]);
                int i = 0;
                while (i < dtgTADMAP.Rows.Count)
                {
                    objInfo.TAD = dtgTADMAP.Rows[i].Cells["GW_BANK_CODE1"].Value.ToString();
                    objInfo.TADHO = N[0];//cboTADHO.SelectedValue.ToString();
                    objInfo.NOTE = dtgTADMAP.Rows[i].Cells["SIBS_BANK_CODE1"].Value.ToString();//;SIBS_BANK_CODE1
                    objControl.INSERT_TADMAP(objInfo);
                    i = i + 1;
                }
                MessageBox.Show("Insert data succesfull!", Common.sCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Load_datadridview();

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmTAD_MAP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCDInfo_FormClosing(null,null);
                this.Close();
            }           
        }

        private void dtgTAD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows = e.RowIndex; }
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < this.dtgTAD.RowCount; i++)
                    {
                        this.dtgTAD.EndEdit();
                        string re_value = this.dtgTAD.Rows[i].Cells[0].EditedFormattedValue.ToString();
                    }
                }
            }
        }

        private void dtgTADMAP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { iRows = e.RowIndex; }
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < this.dtgTADMAP.RowCount; i++)
                    {
                        this.dtgTADMAP.EndEdit();
                        string re_value = this.dtgTADMAP.Rows[i].Cells[0].EditedFormattedValue.ToString();
                    }
                }
            }
        }

        private void frmTAD_MAP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }            
        }

        private void dtgTADMAP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
    }
}
