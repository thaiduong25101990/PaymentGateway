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

namespace BR.BRSYSTEM
{
    public partial class frmCCYCDInfo : Form
    {
        public int i ;
        public string strMode;
        public bool[] isCheck2;

        public CURRENCY_CHANNELInfo objInfo = new CURRENCY_CHANNELInfo();
        public CURRENCY_CHANNELController objControl = new CURRENCY_CHANNELController();
        public CURRENCYInfo objCurrency = new CURRENCYInfo();
        public CURRENCYController objCurrControl = new CURRENCYController();
        
        private int iRows;
        private DataSet datDs = new DataSet();

        private clsLog objLog = new clsLog();
        private GetData objGetData = new GetData();
        private GWTYPEInfo objGWType = new GWTYPEInfo();
        private GWTYPEController objcontrolGWType = new GWTYPEController();
        private clsCheckInput check = new clsCheckInput();
        private ALLCODEController objAllcode = new ALLCODEController();        
        private ListViewItem item =new ListViewItem();
                
        private bool NeedConfirm = true;
        private static bool strSucess = false;        
        private string CCYCD1;
        private string SHORTCD1;
        private string CURRENCY1;
        private string DECIMALS1;
        private string STATUS1;
        private string PARTNER1;
        private string GWTYPE1;
        private string ID1;

        public frmCCYCDInfo()
        {
            InitializeComponent();
        }
        private bool  Verify()
        { 
            if (cboChannel.Text!=Common.GW_CHANNEL_TTSP)
            {
                return true;
            }

            for (int i = 0; i < dgvGWType.Rows.Count; i++)
            {
                if (dgvGWType.Rows[i].Cells[0].Value.ToString().ToLower()  == "true")
                {
                   objInfo.PARTNER = dgvGWType.Rows[i].Cells[1].Value.ToString();
                    return true;
                }
            }
            Common.ShowError("Please choose at least one partner!", 4, MessageBoxButtons.OK);
            return false;
        }

        private bool CheckExist(CURRENCY_CHANNELInfo objCurr)
        {
            DataSet ds = new  DataSet();
            ds = objControl.GetCurrency(objCurr);
            if (ds.Tables[0].Rows.Count > 0)
                return true;            
            return false;
        }

        private bool CheckExist_Edit(CURRENCY_CHANNELInfo objCurr)
        {
            DataSet ds = new  DataSet();
            ds = objControl.CheckEditCurrency(objCurr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }        

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LockTextBox(Boolean a)
        {
            cboCCYCD.Enabled = !a;
            txtDecimal.ReadOnly = a;
          
        }

        private void FormatDataGridView()
        {
            dgvGWType.RowHeadersVisible = false;           
            dgvGWType.Columns[0].Width = 20;
            dgvGWType.Columns[1].Width = 60;
            dgvGWType.Columns[1].ReadOnly = true;
            dgvGWType.Columns[2].Width = 220;
            dgvGWType.Columns[2].ReadOnly = true;
        }    


        private void LoadGWTypeChecked()
        {
            DataSet datDs1 = new DataSet();
            int iStatus=0;
            datDs1 = objControl.GetCurrency_code(objInfo.SHORTCD);
            int m = 0;
            while (m < datDs1.Tables[0].Rows.Count)
            {
                ////////////////////////////////////////////////////-/
                //Muc dich: Kiem tra status = 1 => checked column 0
                //Ngay sua:02/08/2008
                ////////////////////////////////////////////////////-/
                string strGwtype = datDs1.Tables[0].Rows[m]["GWTYPE"].ToString();                
                if (datDs1.Tables[0].Rows[m]["STATUS"]!=null && 
                    datDs1.Tables[0].Rows[m]["STATUS"].ToString()!="")
                {
                    iStatus = Convert.ToInt16(datDs1.Tables[0].Rows[m]["STATUS"]);
                }


                int b = 0;
                while (b < dgvGWType.Rows.Count)
                {
                    if (strGwtype == dgvGWType.Rows[b].Cells[2].Value.ToString() && iStatus ==1)
                    {
                        dgvGWType.Rows[b].Cells[0].Value = true;
                    }
                    b = b + 1;
                }
                m = m + 1;
            }

        }

        private void frmCurrency_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                //--------------------------
                CCYCD1 = objInfo.CCYCD;
                SHORTCD1 = objInfo.SHORTCD;
                CURRENCY1 = objInfo.CURRENCY;
                DECIMALS1 = Convert.ToString(objInfo.DECIMALS);
                STATUS1 = Convert.ToString(objInfo.STATUS);
                PARTNER1 = objInfo.PARTNER;
                GWTYPE1 = objInfo.GWTYPE;
                ID1 = Convert.ToString(objInfo.ID);                
                //--------------------------
                //Load combobox status;
                if (!objGetData.FillDataComboBox(cboStatus, "CONTENT", "CDVAL", "ALLCODE",
                    "CDNAME='CCYSTS' AND GWTYPE='SYSTEM'", "CONTENT", true, false, ""))
                    return;

                //Load combobox ccycd;
                ////if (!objGetData.FillDataComboBox(cboCCYCD, "CCYCD", "shortcd", "CURRENCYCODE",
                ////    "", "CCYCD", true, true, "ALL"))
                ////    return;
                CURRENCYController objCURRENCYController = new CURRENCYController();
                DataSet dsData = new DataSet();
                dsData = objCURRENCYController.GetCurrency3();
                cboCCYCD.SelectedItem = "ALL";
                for (i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    string strCCYCD = dsData.Tables[0].Rows[i]["CCYCD"].ToString();
                    string strSHORTCD = dsData.Tables[0].Rows[i]["SHORTCD"].ToString();
                    byte bytDecimals = Convert.ToByte(dsData.Tables[0].Rows[i]["DECIMALS"]);
                    CCYItem objComboItem = new CCYItem(strSHORTCD, strCCYCD, bytDecimals);
                    cboCCYCD.Items.Add(objComboItem);
                }
                cboCCYCD.SelectedItem = "ALL";                


                //Load combobox cboChannel;
                if (!objGetData.FillDataComboBox(cboChannel, "GWTYPE", "GWTYPE", "GWTYPE",
                    "", "GWTYPE", true, false, ""))
                    return;
                //Load datagridview
                LoadDatagrid();
                
                cboChannel_SelectedIndexChanged(null, null);               
                if (strMode == "VIEW")
                {
                    cboChannel.Enabled = false;
                    cboStatus.Enabled = false;
                    cboCCYCD.Enabled = false;
                    txtDecimal.Enabled = false;
                    dgvGWType.Enabled = false;
                    cmdSave.Enabled = false;
                }
                else if (strMode == "ADD")
                {
                    cboStatus.Enabled = true;
                    if (cboChannel.Text != Common.GW_CHANNEL_TTSP)                    
                        dgvGWType.Enabled = false;                   
                    else
                        dgvGWType.Enabled = true;

                    cboChannel.Enabled = true;
                    txtDecimal.Enabled = false;
                    cboCCYCD.Enabled = true;
                    cmdSave.Enabled = true;
                    cboStatus.Enabled = false;
                }
                else if (strMode == "EDIT")
                {
                    cboStatus.Enabled = true;
                    if (cboChannel.Text != Common.GW_CHANNEL_TTSP)
                        dgvGWType.Enabled = false;                   
                    else
                        dgvGWType.Enabled = true;

                    cboChannel.Enabled = false;
                    txtDecimal.Enabled = false;
                    cboCCYCD.Enabled = false;
                    cmdSave.Enabled = true;
                }

           
                if (((strMode == "VIEW") || (strMode == "EDIT")))
                {
                    //Load object
                    cboChannel.Text = objInfo.GWTYPE;
                    cboCCYCD.Text = objInfo.CCYCD;
                    cboStatus.SelectedValue = objInfo.STATUS;
                    txtDecimal.Text = Convert.ToString(objInfo.DECIMALS);
                    //Load partner
                    if (cboChannel.Text == Common.GW_CHANNEL_TTSP)
                        LoadPartner();
                    else
                        dgvGWType.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

          

        private void txtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;             
            }
        }

       
             
        private void KeyDownPress(object sender, KeyEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
            if (e.KeyCode == Keys.Escape)            
                this.Close();            
            if (e.KeyCode == Keys.Return)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);
                if ((this.ActiveControl) is Button)                
                    return;                
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                    return;
                }
                if ((this.ActiveControl) is MaskedTextBox)
                {
                    (this.ActiveControl as MaskedTextBox).SelectAll();
                    return;
                }
            }
        }

        private void frmCCYCDInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cmdSave.Enabled)
                return;
            try
            {
                if (strSucess == false)
                {
                    if (NeedConfirm == true)
                        e.Cancel = BR.BRLib.DynamicMenu.ConfirmBox_Box("Do you want to exit?", Common.sCaption);
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void LoadDatagrid()
        {
            BRANCHController objBRANCHController = new BRANCHController();
            DataSet dsData = new DataSet();
            string strWhere ="";

            if (((strMode == "VIEW")))
            {
                strWhere = " where trim(BRAN_TYPE)='" + Common.GW_BRANCH_BRAN_TYPE_TTSP + 
                    "' AND trim(SIBS_BANK_CODE) = '"+ objInfo.PARTNER +"'" ;
            }
            else
            {
                strWhere = " where trim(BRAN_TYPE)='" + Common.GW_BRANCH_BRAN_TYPE_TTSP + "'";
            }

            dsData = objBRANCHController.GetData(strWhere);
            dgvGWType.Rows.Clear();

            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
            {
                dgvGWType.Rows.Add();
                dgvGWType.Rows[i].Cells[0].Value = false;
                dgvGWType.Rows[i].Cells[1].Value = dsData.Tables[0].Rows[i]["SIBS_BANK_CODE"].ToString();
                dgvGWType.Rows[i].Cells[2].Value = dsData.Tables[0].Rows[i]["BRAN_NAME"].ToString();
            }
            FormatDataGridView();       
        }
        
        //Load partner cua mot ma tien te ung voi 1 kenh thanh toan, cu the la kenh TTSP
        private void LoadPartner()
        {
            
            DataSet dsData = new DataSet();
            try
            {
                string strCCYCD = cboCCYCD.Text ;
                string strChannel = cboChannel.Text.Trim() ;
                //|| (strMode == "EDIT")

                string strWhere = " ";
                if (((strMode == "VIEW") ))
                {
                    strWhere = " where ccy.CCYCD = '" + strCCYCD + "' and ccy.GWTYPE = '" + 
                        strChannel + "' and ccy.partner is not null And ccy.ID =" + objInfo.ID ;
                }
                else
                {
                    strWhere = " where ccy.CCYCD = '" + strCCYCD + "' and ccy.GWTYPE = '" + 
                        strChannel + "' and ccy.partner is not null ";
                }
                
                
                dsData = objControl.GetCurrencySearch(strWhere);
                if (dsData == null)
                    return;

                if (dsData.Tables[0].Rows.Count == 0)
                    return;

                // sua lai
                // khi view ra doi voi Edit chi cho chon 1 row tren parner
                // khi chon parner 

                if (((strMode == "EDIT")))
                {
                    for (i = 0; i < dgvGWType.Rows.Count; i++)
                    {
                        if (dgvGWType.Rows[i].Cells[1].Value.ToString() == objInfo.PARTNER)
                        {
                            dgvGWType.Rows[i].Cells[0].Value = true;
                        }
                    }
                }

               // het sua
                else
                {
                    for (i = 0; i < dgvGWType.Rows.Count; i++)
                    {
                        for (int j = 0; j < dsData.Tables[0].Rows.Count; j++)
                        {
                            if (dsData.Tables[0].Rows[j]["partner"].ToString() == 
                                dgvGWType.Rows[i].Cells[1].Value.ToString())
                                dgvGWType.Rows[i].Cells[0].Value = true;
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            finally
            {
                dsData.Dispose();
            }                 

            
        }
        private void cboChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChannel.Text  == BR.BRLib.Common.GW_CHANNEL_TTSP)
            {
                dgvGWType.Rows.Clear();
                LoadDatagrid();
                if (strMode != "VIEW")                
                    dgvGWType.Enabled = true;                                      
                else
                    dgvGWType.Enabled = false ;
            }
            else            
                dgvGWType.Rows.Clear();
        }

        private void cboCCYCD_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                if (cboCCYCD.Items.Count>0 && cboCCYCD.Text != "ALL")
                {                    
                    //txtDecimal.Text = cboCCYCD.SelectedValue.ToString();
                    CCYItem objComboItem = new CCYItem("", "", 0);
                    objComboItem = (cboCCYCD.SelectedItem as CCYItem);
                    double dblDecimal = objComboItem.value1();
                    txtDecimal.Text = Convert.ToString(dblDecimal);
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
                string strShortName;
                double dblDecimal;
                string strCCYCD;
                CCYItem objComboItem = new CCYItem("", "", 0);
                objComboItem = (cboCCYCD.SelectedItem as CCYItem);

                if (strMode == "EDIT")
                {
                    //if (!Verify())
                    //    return;                    
                    if (cboCCYCD.Items.Count > 0 && cboCCYCD.Text != "ALL")
                    {
                        ////strShortName = cboCCYCD.SelectedText.ToString();
                        ////dblDecimal = Convert.ToDouble(cboCCYCD.SelectedValue.ToString());
                        strShortName = objComboItem.value();
                        dblDecimal = objComboItem.value1();
                    }
                    else
                    {
                        strShortName = "";
                        dblDecimal = 0;
                    }                    
                     
                    strCCYCD = cboCCYCD.Text;
                    objInfo.STATUS = Convert.ToInt32(cboStatus.SelectedValue);
                    if (cboChannel.Text.Trim() == Common.GW_CHANNEL_TTSP)
                    {
                        for (i = 0; i < dgvGWType.Rows.Count; i++)
                        {
                            if (dgvGWType.Rows[i].Cells[0].Value.ToString().ToUpper()=="true".ToUpper())
                            {
                                objInfo.PARTNER = dgvGWType.Rows[i].Cells[1].Value.ToString();
                            }                            
                        }
                    }
                    if (CheckExist_Edit(objInfo))
                    {
                        Common.ShowError("Data has existed!", 4, MessageBoxButtons.OK);                        
                        return;
                    }//truoc ngay 30.08
                    if (objControl.UpdateCURRENCY_STATUS(objInfo) == 1)
                    {
                        Common.ShowError("Data has been updated successfully!", 1, MessageBoxButtons.OK);                        
                        cmdSave.Enabled = false;
                        //lay thong tin de ghilog----------------------
                        DateTime dtLog = DateTime.Now;
                        string strUser = BR.BRLib.Common.strUsername;
                        string useride = BR.BRLib.Common.Userid;
                        string strConten = "Currency Channel";
                        int Log_level = 1;
                        string strWorked = "Update";
                        string strTable = "CURRENCY";
                        string strOld_value = CCYCD1 + "/" + SHORTCD1 + "/" + CURRENCY1 + "/" + 
                            DECIMALS1 + "/" + STATUS1 + "/" + PARTNER1 + "/" + GWTYPE1;
                        string strNew_value = objInfo.CCYCD + "/" + objInfo.SHORTCD + "/" + 
                            objInfo.CURRENCY + "/" + objInfo.DECIMALS + "/" + objInfo.STATUS + 
                            "/" + objInfo.PARTNER + "/" + objInfo.GWTYPE;
                        //-----------------------------------------
                        objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                            strWorked, strTable, strOld_value, strNew_value);
                        //---------------------------------------------------------------                        
                        this.Close();
                    }
                    else
                    {
                        Common.ShowError("Error occured when updating!", 2, MessageBoxButtons.OK);                        
                    }

                }
                else//(strMode == "ADD")
                {
                    if (!Verify())
                        return;
                    if (cboCCYCD.Text != "ALL")// 1 loai tien te
                    {
                        if (cboCCYCD.Items.Count > 0 && cboCCYCD.Text != "ALL")
                        {
                            ////strShortName = cboCCYCD.SelectedText.ToString();
                            ////dblDecimal = Convert.ToDouble(cboCCYCD.SelectedValue.ToString());
                            strShortName = objComboItem.value();
                            dblDecimal = objComboItem.value1();
                        }
                        else
                        {
                            strShortName = "";
                            dblDecimal = 0;
                        } 
                        strCCYCD = cboCCYCD.Text;

                        objInfo.SHORTCD = strShortName;
                        objInfo.CCYCD = strCCYCD;
                        objInfo.DECIMALS = dblDecimal;
                        objInfo.STATUS = Convert.ToInt32(cboStatus.SelectedValue);                        
                        objInfo.GWTYPE = cboChannel.Text;

                        if (cboChannel.Text == Common.GW_CHANNEL_TTSP)
                        {
                            //Duyet Partner
                            for (int i = 0; i < dgvGWType.Rows.Count; i++)
                            {
                                if (dgvGWType.Rows[i].Cells[0].Value.ToString() != "True")
                                    continue;

                                objInfo.PARTNER = dgvGWType.Rows[i].Cells[1].Value.ToString();
                                //objControl.DeleteCURRENCY(objInfo.CCYCD, objInfo.GWTYPE, objInfo.PARTNER);
                                if (CheckExist(objInfo))
                                {
                                    Common.ShowError("Data has existed!", 4, MessageBoxButtons.OK);                                    
                                    return;
                                }
                                objControl.AddCURRENCY(objInfo);
                                //lay thong tin de ghilog----------------------
                                DateTime dtLog = DateTime.Now;
                                string strUser = BR.BRLib.Common.strUsername;
                                string useride = BR.BRLib.Common.Userid;
                                string strConten = "Currency Channel";
                                int Log_level = 1;
                                string strWorked = "Insert";
                                string strTable = "CURRENCY";
                                string strOld_value = "";
                                string strNew_value = objInfo.CCYCD + "/" + objInfo.SHORTCD + "/" + 
                                    objInfo.CURRENCY + "/" + objInfo.DECIMALS + "/" + objInfo.STATUS + 
                                    "/" + objInfo.PARTNER + "/" + objInfo.GWTYPE;
                                //-----------------------------------------
                                objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                    strWorked, strTable, strOld_value, strNew_value);
                                //---------------------------------------------------------------       
                            }
                        }
                        else
                        {
                            //objControl.DeleteCURRENCY(objInfo.CCYCD, objInfo.GWTYPE, objInfo.PARTNER);
                            if (CheckExist(objInfo))
                            {
                                Common.ShowError("Data has existed!", 4, MessageBoxButtons.OK);                                
                                return;
                            }
                            objControl.AddCURRENCY(objInfo);
                            //lay thong tin de ghilog----------------------
                            DateTime dtLog = DateTime.Now;
                            string strUser = BR.BRLib.Common.strUsername;
                            string useride = BR.BRLib.Common.Userid;
                            string strConten = "Currency Channel";
                            int Log_level = 1;
                            string strWorked = "Insert";
                            string strTable = "CURRENCY";
                            string strOld_value = "";
                            string strNew_value = objInfo.CCYCD + "/" + objInfo.SHORTCD + "/" + 
                                objInfo.CURRENCY + "/" + objInfo.DECIMALS + "/" + objInfo.STATUS + 
                                "/" + objInfo.PARTNER + "/" + objInfo.GWTYPE;
                            //-----------------------------------------
                            objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                strWorked, strTable, strOld_value, strNew_value);
                            //--------------------------------------------------------------- 
                        }
                    }
                    else //Tat ca cac loai tien te
                    {
                        DataSet ds;
                        ds = objCurrControl.GetCurrency();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                             strCCYCD = ds.Tables[0].Rows[i]["CCYCD"].ToString();
                             strShortName = ds.Tables[0].Rows[i]["SHORTCD"].ToString();
                            dblDecimal = double.Parse(ds.Tables[0].Rows[i]["DECIMALS"].ToString());

                            objInfo.SHORTCD = strShortName;
                            objInfo.CCYCD = strCCYCD;
                            objInfo.DECIMALS = dblDecimal;
                            objInfo.STATUS = Convert.ToInt32(cboStatus.SelectedValue);
                            objInfo.GWTYPE = cboChannel.Text;

                            if (cboChannel.Text == Common.GW_CHANNEL_TTSP)
                            {
                                //Duyet Partner
                                for (int j = 0; j < dgvGWType.Rows.Count; j++)
                                {
                                    if (dgvGWType.Rows[j].Cells[0].Value.ToString().ToUpper() != "True".ToUpper())
                                        continue;

                                    objInfo.PARTNER = dgvGWType.Rows[j].Cells[1].Value.ToString();
                                   //// objControl.DeleteCURRENCY(objInfo.CCYCD, objInfo.GWTYPE, objInfo.PARTNER);
                                    if (CheckExist(objInfo)==false)
                                   {
                                       objControl.AddCURRENCY(objInfo);
                                       //lay thong tin de ghilog----------------------
                                       DateTime dtLog = DateTime.Now;
                                       string strUser = BR.BRLib.Common.strUsername;
                                       string useride = BR.BRLib.Common.Userid;
                                       string strConten = "Currency Channel";
                                       int Log_level = 1;
                                       string strWorked = "Insert";
                                       string strTable = "CURRENCY";
                                       string strOld_value = "";
                                       string strNew_value = objInfo.CCYCD + "/" + objInfo.SHORTCD + 
                                           "/" + objInfo.CURRENCY + "/" + objInfo.DECIMALS + "/" + 
                                           objInfo.STATUS + "/" + objInfo.PARTNER + "/" + objInfo.GWTYPE;
                                       //-----------------------------------------
                                       objLog.GhiLogUser(dtLog, strUser, strConten, Log_level,
                                           strWorked, strTable, strOld_value, strNew_value);
                                       //---------------------------------------------------------------       
                                  
                                    }//truoc ngay 30.08
                                   
                                }
                            }
                            else
                            {
                               //// objControl.DeleteCURRENCY(objInfo.CCYCD, objInfo.GWTYPE, objInfo.PARTNER);
                               if (CheckExist(objInfo)==false)
                               {
                                   objControl.AddCURRENCY(objInfo);
                                   //lay thong tin de ghilog----------------------
                                   DateTime dtLog = DateTime.Now;
                                   string strUser = BR.BRLib.Common.strUsername;
                                   string useride = BR.BRLib.Common.Userid;
                                   string strConten = "Currency Channel";
                                   int Log_level = 1;
                                   string strWorked = "Insert";
                                   string strTable = "CURRENCY";
                                   string strOld_value = "";
                                   string strNew_value = objInfo.CCYCD + "/" + objInfo.SHORTCD + "/" + 
                                       objInfo.CURRENCY + "/" + objInfo.DECIMALS + "/" + 
                                       objInfo.STATUS + "/" + objInfo.PARTNER + "/" + objInfo.GWTYPE;
                                   //-----------------------------------------
                                   objLog.GhiLogUser(dtLog, strUser, strConten, Log_level, 
                                       strWorked, strTable, strOld_value, strNew_value);
                                   //---------------------------------------------------------------       
                               
                              } //truoc ngay 30.08
                              
                            }

                        }
                    }
                    Common.ShowError("Insert data successfully!", 1, MessageBoxButtons.OK);                    
                    cmdSave.Enabled = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }            
        }
        

        private void dgvGWType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (strMode == "ADD")
                    return;
                if (e.ColumnIndex != 0)
                    return;
                iRows = e.RowIndex;                
                if (dgvGWType.Rows[e.RowIndex].Cells[0].Value.ToString().ToLower() == "false")
                {
                    for (int i = 0; i < dgvGWType.Rows.Count; i++)
                    {
                        dgvGWType.Rows[i].Cells[0].Value = false;
                    }
                    dgvGWType.Rows[iRows].Cells[0].Value = true;
                }
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
                frmCCYCDInfo_FormClosing(null, null);
                this.Close();
            }
            //khi nhan phim Enter
            if (e.KeyChar == (char)13)
            {
                if (cboChannel.Focused)
                {
                    cboCCYCD.Focus();
                    cboCCYCD.SelectAll();
                }
                else if (cboCCYCD.Focused)
                {
                    txtDecimal.Focus();
                    txtDecimal.SelectAll();
                }
                else if (txtDecimal.Focused)
                {
                    cboStatus.Focus();
                    cboStatus.SelectAll();
                }
                else if (cboStatus.Focused)
                {
                    dgvGWType.Focus();
                    dgvGWType.SelectAll();
                }
                else if (dgvGWType.Focused)                
                    cmdSave.Focus();       

                strSucess = true;
            }
        }

        private void frmCCYCDInfo_MouseMove(object sender, MouseEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }

        private void dgvGWType_MouseMove(object sender, MouseEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }
        
    }

    # region //Classes
    public class CCYItem
    {
        private string _name;
        private string _value;
        private double _value1;

        public CCYItem(string value, string name, double value1)
        {
            _name = name;
            _value = value;
            _value1 = value1;
        }

        public override string ToString()
        {
            return _name;
        }


        public string value()
        {
            return _value;
        }

        public double value1()
        {
            return _value1;
        }


    }
    #endregion
       
}
