using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRLib;
using BR.BRBusinessObject ;

namespace BR.BRSYSTEM
{
    public partial class frmRepostRule : BR.BRSYSTEM.frmBasedata  
    {
        RPTRULEController objrulecontrol = new RPTRULEController();
        RPTMASTERController objcontrol = new RPTMASTERController();
        GROUPSController objcontrolGroup = new GROUPSController();
        RPTRULEInfo objRptRule = new RPTRULEInfo();
        public bool iEdit = false;
        public frmRepostRule()
        {
            InitializeComponent();
        }

        private void frmRepostRule_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");
            GetGroup();
            
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        //private void GetGroup(string gwType)
        private void GetGroup()
        {
            dgvReportRule.Columns.Clear();
            try
            {               
                DataSet datGroup = new DataSet();
                //datGroup = objcontrol.GetGroup(gwType);
                datGroup = objcontrol.GetGroup();
                int i = 0;
                while (i < datGroup.Tables[0].Rows.Count)
                {
                    DataGridViewCheckBoxColumn column0 = new DataGridViewCheckBoxColumn();
                    string strGroupname = datGroup.Tables[0].Rows[i]["GROUPNAME"].ToString();
                    int groupID = Convert.ToInt32(datGroup.Tables[0].Rows[i]["GROUPID"].ToString()) ; 
                    column0.Name = strGroupname;
                    //column1.Name = "DisableCheckBoxes";
                    dgvReportRule.Columns.Add(column0);
                    dgvReportRule.Columns[i].Width = 100;
                    dgvReportRule.RowCount = 4;
                    dgvReportRule.Rows[0].HeaderCell.Value = "View";
                    dgvReportRule.Rows[1].HeaderCell.Value = "Refresh";
                    dgvReportRule.Rows[2].HeaderCell.Value = "Print";
                    dgvReportRule.Rows[3].HeaderCell.Value = "Export";
                    //---------------------------------
                    dgvReportRule.AutoSize = false;
                    //dgvReportRule.AllowUserToAddRows = false;
                    
                    DataSet dtRule = new DataSet();
                    dtRule = objrulecontrol.GetRule(groupID,frmReportList.strContent);
                    if (dtRule.Tables[0].Rows.Count > 0)
                    {
                        int view = Convert.ToInt32(dtRule.Tables[0].Rows[0]["ISVIEW"].ToString());
                        int add = Convert.ToInt32(dtRule.Tables[0].Rows[0]["ISREFRESH"].ToString());
                        int edit = Convert.ToInt32(dtRule.Tables[0].Rows[0]["ISPRINT"].ToString());
                        int delete = Convert.ToInt32(dtRule.Tables[0].Rows[0]["ISEXPORT"].ToString());
                        #region //View nhung group da cap quyen
                        if (view == 1)
                        {
                            dgvReportRule.Rows[0].Cells[i].Value = true;
                        }
                        else
                        {
                            dgvReportRule.Rows[0].Cells[i].Value = null;
                        }
                        if (add == 1)
                        {
                            dgvReportRule.Rows[1].Cells[i].Value = true;
                        }
                        else
                        {
                            dgvReportRule.Rows[1].Cells[i].Value = null;
                        }
                        if (edit == 1)
                        {
                            dgvReportRule.Rows[2].Cells[i].Value = true;
                        }
                        else
                        {
                            dgvReportRule.Rows[2].Cells[i].Value = null;
                        }
                        if (delete == 1)
                        {
                            dgvReportRule.Rows[3].Cells[i].Value = true;
                        }
                        else
                        {
                            dgvReportRule.Rows[3].Cells[i].Value = null;
                        }
                        #endregion
                    }
                    else
                    {
                        dgvReportRule.Rows[0].Cells[i].Value = null;
                        dgvReportRule.Rows[1].Cells[i].Value = null;
                        dgvReportRule.Rows[2].Cells[i].Value = null;
                        dgvReportRule.Rows[3].Cells[i].Value = null;
                    }
                    i = i + 1;
                }
                dgvReportRule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ;
                dgvReportRule.Rows[0].ReadOnly = true;
                dgvReportRule.Rows[1].ReadOnly = true;
                dgvReportRule.Rows[2].ReadOnly = true;
                dgvReportRule.Rows[3].ReadOnly = true;
                dgvReportRule.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; 
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
                int j = 0;
                while (j < dgvReportRule.Columns.Count)//theo cot truoc, duyet tung cot mot
                {

                    dgvReportRule.RefreshEdit();
                    #region dong 1
                    if ((dgvReportRule.Rows[0].Cells[j].Value == null))
                    {
                        objRptRule.ISVIEW  = 0;
                    }
                    else //((dgvReportRule.Rows[0].Cells[j].Value != null))
                    {
                        //objRptRule.ISVIEW = 1;
                        if (dgvReportRule.Rows[0].Cells[j].Value.ToString() == "True")
                        {
                            objRptRule.ISVIEW = 1;
                        }
                        else
                        {
                            objRptRule.ISVIEW = 0;
                        }
                    }
                    #endregion
                    #region dong 2
                    if ((dgvReportRule.Rows[1].Cells[j].Value == null))
                    {
                        objRptRule.ISREFRESH = 0;

                    }
                    else //((dgvReportRule.Rows[1].Cells[j].Value != null))
                    {
                        //objRptRule.ISREFRESH = 1;
                        if (dgvReportRule.Rows[1].Cells[j].Value.ToString() == "True")
                        {
                            objRptRule.ISREFRESH = 1;
                        }
                        else
                        {
                            objRptRule.ISREFRESH = 0;
                        }
                    }
                    #endregion
                    #region dong 3
                    if ((dgvReportRule.Rows[2].Cells[j].Value == null))
                    {
                        objRptRule.ISPRINT  = 0;

                    }
                    else //((dgvReportRule.Rows[2].Cells[j].Value != null))
                    {
                        //objRptRule.ISPRINT = 1;     
                        if (dgvReportRule.Rows[2].Cells[j].Value.ToString() == "True")
                        {
                            objRptRule.ISPRINT = 1;
                        }
                        else
                        {
                            objRptRule.ISPRINT = 0;
                        }
                    }
                    #endregion
                    #region dong 4
                    if ((dgvReportRule.Rows[3].Cells[j].Value == null))
                    {
                        objRptRule.ISEXPORT  = 0;
                    }
                    else  //((dgvReportRule.Rows[3].Cells[j].Value != null))
                    {
                        //objRptRule.ISEXPORT = 1;  
                        if (dgvReportRule.Rows[3].Cells[j].Value.ToString() == "True")
                        {
                            objRptRule.ISEXPORT = 1;
                        }
                        else
                        {
                            objRptRule.ISEXPORT = 0;
                        }
                    }
                    #endregion
                    string strGroupname = dgvReportRule.Columns[j].Name;
                    DataSet datGroup = new DataSet();
                    datGroup = objcontrolGroup.GetGROUPNAME(strGroupname);
                    int iGroupid = Convert.ToInt32(datGroup.Tables[0].Rows[0]["GROUPID"].ToString());
                    objRptRule.GROUPID = iGroupid;
                    objRptRule.RPTNAME = frmReportList.strContent.Trim();
                    objrulecontrol.DeleteRPTRULE(iGroupid,frmReportList.strContent.Trim());
                    if ((dgvReportRule.Rows[0].Cells[j].Value != null && dgvReportRule.Rows[0].Cells[j].Value.ToString() == "True") || (dgvReportRule.Rows[1].Cells[j].Value != null && dgvReportRule.Rows[1].Cells[j].Value.ToString() == "True") || (dgvReportRule.Rows[2].Cells[j].Value != null && dgvReportRule.Rows[2].Cells[j].Value.ToString() == "True") || (dgvReportRule.Rows[3].Cells[j].Value != null && dgvReportRule.Rows[3].Cells[j].Value.ToString() == "True"))
                    {
                        objrulecontrol.AddRPTRULE(objRptRule); 
                    }
                 
                    j = j + 1;
                }
                dgvReportRule.Rows[0].ReadOnly = true;
                dgvReportRule.Rows[1].ReadOnly = true;
                dgvReportRule.Rows[2].ReadOnly = true;
                dgvReportRule.Rows[3].ReadOnly = true;
                 
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            iEdit = true;
            dgvReportRule.Rows[0].ReadOnly = false ;
            dgvReportRule.Rows[1].ReadOnly = false ;
            dgvReportRule.Rows[2].ReadOnly = false ;
            dgvReportRule.Rows[3].ReadOnly = false ;
        }

        private void dgvReportRule_KeyDown(object sender, KeyEventArgs e)
        {
            
            try
            {
                int h = 0;
                int m = 0;
                if (iEdit == true)
                {
                    if (e.KeyData == Keys.Space)
                    {

                        foreach (DataGridViewCell selectedCell in dgvReportRule.SelectedCells)
                        {
                            h = selectedCell.RowIndex;
                            m = selectedCell.ColumnIndex;
                            if (dgvReportRule.Rows[h].Cells[m].Value != null)// hang duoc chon
                            {
                                if (dgvReportRule.Rows[h].Cells[m].Value.ToString() == "True")
                                {
                                    dgvReportRule.Rows[h].Cells[m].Value = null;
                                }
                                else
                                {
                                    dgvReportRule.Rows[h].Cells[m].Value = true;
                                }
                            }
                            else
                            {
                                dgvReportRule.Rows[h].Cells[m].Value = true;
                            }
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

        }

        private void frmRepostRule_KeyDown(object sender, KeyEventArgs e)
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

        private void frmRepostRule_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }      
       
    }
}
