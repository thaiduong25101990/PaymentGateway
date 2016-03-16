using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using BR.BRSYSTEM;
using BR.BRLib;

namespace BR.BRSYSTEM
{
    public partial class frmCCYCD_Channel : frmBasedata 
    {
        public int d = 0;
        public static bool isInsert = false;

        private GetData objGetData = new GetData();
        private clsLog objLog = new clsLog();
        private DataSet datDs = new DataSet();
        private CURRENCY_CHANNELInfo objInfo = new CURRENCY_CHANNELInfo();
        private CURRENCY_CHANNELController objControl = new CURRENCY_CHANNELController();
        private ALLCODEController objAllcode = new ALLCODEController();
        private BRANCHController objControlBRANCH = new BRANCHController();

        private int iID = 0;
        private int iRows;                
        private string strCCYCD;
        private string strSHORTCD;
        private string strCURRENCY;
        private string strDECIMALS;
        private string strSTATUS;
        private string strPARTNER;
        private string strGWTYPE;

        public frmCCYCD_Channel()
        {
            InitializeComponent();
        }


        private void frmCCYCD_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                //Load data combobox status
                if (!objGetData.FillDataComboBox(cboStatus, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME='CCYSTS' AND GWTYPE='SYSTEM'", "CONTENT", true, true, "ALL"))
                    return;
                //Load data combobox ccycd
                if (!objGetData.FillDataComboBox(cboCCYCD, "CCYCD", "CCYCD", "Currencycode",
                    "", "CCYCD", true, true, "ALL"))
                    return;
                //Load data combobox gwtype
                if (!objGetData.FillDataComboBox(cboGWType, "GWTYPE", "GWTYPE", "GWTYPE",
                    "", "GWTYPE", true, true, "ALL"))
                    return;
                //Load data combobox Partner
                if (!objGetData.FillDataComboBox(cboPartner, "SIBS_BANK_CODE", "SIBS_BANK_CODE", 
                    "Branch","BRAN_TYPE = '2'", "SIBS_BANK_CODE", false , true, "ALL"))
                    return;                
                LoadDataGrid();

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }            
        }
      
        private void LoadDataGrid()
        {            
            //////////////////////////////////////////////////-/
            //Muc dich: Kiem tra rowcount>0            
            //Ngay sua: 02/08/2008
            SearchData();
            FormatDataGridView();
            if (dgdCurrency.RowCount > 0)
            { cmdDelete.Enabled = true; cmdEdit.Enabled = true; }
            else
            { cmdDelete.Enabled = false; cmdEdit.Enabled = false; }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                /////////////////////////////////////////////////////////
                //Muc dich: Kiem tra rowcount>0
                //          Kiem tra currentrow<> null
                //Ngay sua: 01/08/2008
                if (dgdCurrency.RowCount <= 0)                
                    return;                
                if (dgdCurrency.CurrentRow == null)                        
                    return;                
                /////////////////////////////////////////////////////////
                frmCCYCDInfo cur = new frmCCYCDInfo();
                cur.strMode = "EDIT";
              //  cur.objInfo.ID = Convert.ToInt32(dgdCurrency.CurrentRow.Cells[0].Value.ToString());
                if (iRows == -1)
                {
                    cur.objInfo.CCYCD = dgdCurrency.Rows[0].Cells["CCYCD"].Value.ToString();
                    cur.objInfo.SHORTCD = dgdCurrency.Rows[0].Cells["SHORTCD"].Value.ToString();
                    cur.objInfo.CURRENCY = dgdCurrency.Rows[0].Cells["CURRENCY"].Value.ToString();
                    cur.objInfo.DECIMALS = Convert.ToInt32(dgdCurrency.Rows[0].Cells["DECIMALS"].Value.ToString());
                    cur.objInfo.STATUS = Convert.ToInt32(dgdCurrency.Rows[0].Cells["STATUS"].Value.ToString());
                    cur.objInfo.PARTNER = dgdCurrency.Rows[0].Cells["PARTNER"].Value.ToString();
                    cur.objInfo.GWTYPE = dgdCurrency.Rows[0].Cells["GWTYPE"].Value.ToString();
                    cur.objInfo.ID = Convert.ToInt32(dgdCurrency.Rows[0].Cells["ID"].Value.ToString());
                }
                else
                {
                    cur.objInfo.CCYCD = dgdCurrency.Rows[iRows].Cells["CCYCD"].Value.ToString();
                    cur.objInfo.SHORTCD = dgdCurrency.Rows[iRows].Cells["SHORTCD"].Value.ToString();
                    cur.objInfo.CURRENCY = dgdCurrency.Rows[iRows].Cells["CURRENCY"].Value.ToString();
                    cur.objInfo.DECIMALS = Convert.ToInt32(dgdCurrency.Rows[iRows].Cells["DECIMALS"].Value.ToString());
                    cur.objInfo.STATUS = Convert.ToInt32(dgdCurrency.Rows[iRows].Cells["STATUS"].Value.ToString());
                    cur.objInfo.PARTNER = dgdCurrency.Rows[iRows].Cells["PARTNER"].Value.ToString();
                    cur.objInfo.GWTYPE = dgdCurrency.Rows[iRows].Cells["GWTYPE"].Value.ToString();
                    cur.objInfo.ID = Convert.ToInt32(dgdCurrency.Rows[iRows].Cells["ID"].Value.ToString());
                }

            
                isInsert = false;
                cmdAdd.Enabled = true;
                cmdEdit.Enabled = true;
                cmdSave.Enabled = false;
                cmdDelete.Enabled = true;
                cur.ShowDialog();
                //LoadData();
                SearchData();

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////
            //Muc dich: Kiem tra rowcount >0 moi xoa
            //          Hoi truoc khi xoa
            //          Kiem tra CurrentRow<>null
            //Ngay sua:01/08/2008            
            string strOut = "";
            int iReturn = 0;
            if (dgdCurrency.RowCount <= 0)            
                return;            
            if (dgdCurrency.CurrentRow == null)            
                return;            
            if (Common.iSconfirm !=1)            
                return;            
            ///////////////////////////////////////////////
            if (iRows == -1)
            {
                iID = Convert.ToInt32(dgdCurrency.Rows[0].Cells[8].Value.ToString());
                strCCYCD = dgdCurrency.Rows[0].Cells["CCYCD"].Value.ToString();
                strSHORTCD = dgdCurrency.Rows[0].Cells["SHORTCD"].Value.ToString();
                strCURRENCY = dgdCurrency.Rows[0].Cells["CURRENCY"].Value.ToString();
                strDECIMALS = dgdCurrency.Rows[0].Cells["DECIMALS"].Value.ToString();
                strSTATUS = dgdCurrency.Rows[0].Cells["STATUS"].Value.ToString();
                strPARTNER = dgdCurrency.Rows[0].Cells["PARTNER"].Value.ToString();
                strGWTYPE = dgdCurrency.Rows[0].Cells["GWTYPE"].Value.ToString();
            }
            else
            {
                iID = Convert.ToInt32(dgdCurrency.Rows[iRows].Cells[8].Value.ToString());
                strCCYCD = dgdCurrency.Rows[iRows].Cells["CCYCD"].Value.ToString();
                strSHORTCD = dgdCurrency.Rows[iRows].Cells["SHORTCD"].Value.ToString();
                strCURRENCY = dgdCurrency.Rows[iRows].Cells["CURRENCY"].Value.ToString();
                strDECIMALS = dgdCurrency.Rows[iRows].Cells["DECIMALS"].Value.ToString();
                strSTATUS = dgdCurrency.Rows[iRows].Cells["STATUS"].Value.ToString();
                strPARTNER = dgdCurrency.Rows[iRows].Cells["PARTNER"].Value.ToString();
                strGWTYPE = dgdCurrency.Rows[iRows].Cells["GWTYPE"].Value.ToString();                
            }
            try
            {             
                iReturn = objControl.DeleteCURRENCY(iID, out strOut);
                if (iReturn == -1 && strOut != "")
                {                    
                    Common.ShowError(strOut, 1, MessageBoxButtons.OK);
                    return;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }    
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = true;           
            LoadDataGrid();

            //lay thong tin de ghilog----------------------
            DateTime dtLog = DateTime.Now;
            string strUser = BR.BRLib.Common.strUsername;
            string useride = BR.BRLib.Common.Userid;
            string strConten = "Currency Channel";
            int Log_level = 1;
            string strWorked = "Delete";
            string strTable = "CURRENCY";
            string strOld_value = iID + "/" + strCCYCD + "/" + strSHORTCD + "/" + 
                strCURRENCY + "/" + strDECIMALS + "/" + strSTATUS + "/" + 
                strPARTNER + "/" + strGWTYPE;
            string strNew_value = iID.ToString();
            //-----------------------------------------
            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, strWorked, 
                strTable, strOld_value, strNew_value);
 
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            isInsert = true; 
            frmCCYCDInfo cur = new frmCCYCDInfo();
            cur.strMode = "ADD";
            cur.ShowDialog();
            cmdAdd.Enabled = true;
            cmdEdit.Enabled = true;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = true;
            //LoadData();
            SearchData();
        }

       
        private void dgdCurrency_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //////////////////////////////////////////////////-/
            //Muc dich: Kiem tra rowcount>0 => lay currentrow
            //Ngay sua: 02/08/2008
            if (dgdCurrency.RowCount <= 0)            
                return;            
            if (dgdCurrency.CurrentRow == null)            
                return;            
            ////////////////////////////////////////////////////
            try
            {
                frmCCYCDInfo cur = new frmCCYCDInfo();
                cur.strMode = "VIEW";
                cur.objInfo.CCYCD = dgdCurrency.CurrentRow.Cells["CCYCD"].Value.ToString();
                cur.objInfo.SHORTCD = dgdCurrency.CurrentRow.Cells["SHORTCD"].Value.ToString();
                cur.objInfo.CURRENCY = dgdCurrency.CurrentRow.Cells["CURRENCY"].Value.ToString();
                cur.objInfo.DECIMALS = Convert.ToInt32(dgdCurrency.CurrentRow.Cells["DECIMALS"].Value.ToString());
                cur.objInfo.STATUS = Convert.ToInt32(dgdCurrency.CurrentRow.Cells["STATUS"].Value.ToString());
                cur.objInfo.PARTNER = dgdCurrency.CurrentRow.Cells["PARTNER"].Value.ToString();
                cur.objInfo.GWTYPE = dgdCurrency.CurrentRow.Cells["GWTYPE"].Value.ToString();
                cur.ShowDialog(this);
                //LoadData();
                SearchData();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void FormatDataGridView()
        {
            try
            {
                dgdCurrency.RowHeadersVisible = true;
                dgdCurrency.ColumnHeadersVisible = true;
                dgdCurrency.Columns["CCYCD"].HeaderText = "Currency";
                dgdCurrency.Columns["CCYCD"].Width = 100;
                dgdCurrency.Columns["SHORTCD"].HeaderText = "SHORTCD";
                dgdCurrency.Columns["SHORTCD"].Visible = false;
                dgdCurrency.Columns["currency"].HeaderText = "Name";
                dgdCurrency.Columns["currency"].Width = 200;
                dgdCurrency.Columns["decimals"].HeaderText = "Decimals";
                dgdCurrency.Columns["decimals"].Width = 70;
                dgdCurrency.Columns[4].HeaderText = "Status code";
                dgdCurrency.Columns[4].Visible = false;
                dgdCurrency.Columns["CONTENT"].HeaderText = "Status";
                dgdCurrency.Columns["CONTENT"].Width = 100;
                dgdCurrency.Columns["partner"].HeaderText = "Partner";
                dgdCurrency.Columns["partner"].Width = 100;
                dgdCurrency.Columns["GWType"].HeaderText = "Channel";
                dgdCurrency.Columns["GWType"].Width = 100;
                dgdCurrency.Columns["ID"].HeaderText = "ID";
                dgdCurrency.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

       private void SearchData()
        {
            try
            {
                string strCurrencyCode; string strGWType; string strStatus;
                string strPartner; string strWHERE;
                if (cboCCYCD.Text == "ALL")                
                    strCurrencyCode = "";                
                else 
                    strCurrencyCode=cboCCYCD.Text;

                strWHERE= " where upper(trim(ccy.CCYCD)) like  upper(trim('%" + 
                          strCurrencyCode + "%')) ";
                
                if (cboGWType.Text != "ALL")
                {
                    strGWType = cboGWType.Text;
                    strWHERE =  strWHERE + "  and upper(trim(ccy.gwtype)) = " + 
                                "upper(trim('" + strGWType + "')) ";                
                }
                         
                if (cboStatus.Text != "ALL")
                {                   
                    strStatus = cboStatus.Text.ToString()  ;
                    strWHERE =  strWHERE + " and upper((select Allcode.Content " + 
                                "from Allcode where CDNAME = 'CCYSTS' and CDVAL = " + 
                                "ccy.status And rownum = 1)) = upper(trim('" + strStatus + "')) ";
           
                }

                if (cboPartner.Text != "ALL")
                {
                    strPartner = cboPartner.Text;
                    if (strPartner.Trim()=="")
                        strWHERE = strWHERE + "  and upper(trim(ccy.partner)) is null ";

                    else
                    strWHERE =  strWHERE + "  and upper(trim(ccy.partner)) = upper(trim('" + 
                                strPartner + "')) ";

                }              
                DataSet datSearch = new DataSet();
                datSearch = objControl.GetCurrencySearch(strWHERE);


                /////////////////////////////////////////////////////
                //Muc dich: Viet thanh procedure de goi lai
                //Dinh dang Grid
                //Ngay sua: 02/08/2008                
                if (datSearch == null)
                {
                    dgdCurrency.DataSource = 0;
                    return;
                }
                dgdCurrency.DataSource = datSearch.Tables[0];
                FormatDataGridView();
                //Muc dich: Kiem tra rowcount>0            
                //Ngay sua: 02/08/2008
                if (dgdCurrency.RowCount > 0)
                { cmdDelete.Enabled = true; cmdEdit.Enabled = true; }
                else
                { cmdDelete.Enabled = false; cmdEdit.Enabled = false; }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void txtMonCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                Common.ShowError("You must input a number!", 3, MessageBoxButtons.OK);
            }
        }

     
        //Muc dich: bat su kien khi nhan phím Enter
        //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
        //Ngay tao: 06/08/2008
        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)            
                this.Close();            
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (cboCCYCD.Focused)
                {
                    cboGWType.Focus();
                    cboGWType.SelectAll();
                }
                else if (cboGWType.Focused)
                {
                    cboStatus.Focus();
                    cboStatus.SelectAll();
                }
                else if (cboStatus.Focused)
                {
                    cboPartner.Focus();
                    cboPartner.SelectAll();
                }
                else if (cboPartner.Focused)                
                    cmdSearch_Click(null, null);                                
            }
        }

        private void frmCCYCD_Channel_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmCCYCD_Channel_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dgdCurrency_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dgdCurrency_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void dgdCurrency_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iRows = e.RowIndex;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


    }

       
}
