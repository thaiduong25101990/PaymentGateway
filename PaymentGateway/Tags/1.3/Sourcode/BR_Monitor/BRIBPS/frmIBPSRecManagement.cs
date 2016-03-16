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

namespace BR.BRIBPS
{
    public partial class frmIBPSRecManagement : Form
    {
        public frmIBPSRecManagement()
        {
            InitializeComponent();
        }

        private void frmIBPSRecManagement_KeyDown(object sender, KeyEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void frmIBPSRecManagement_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void OnClose(object sender, EventArgs e)
        {
            Close();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            txtSI.Text = GetData.GetCount(" from ibps_msg_rec_temp where to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ").ToString();
            
            txtSW.Text = GetData.GetCount(" from swift_msg_rec_temp where rec_type='SIBS-BR' and to_char(trans_date,'DDMMYYYY')= '" + pickerDate.Value.ToString("ddMMyyyy") + "' ").ToString();
            txtSV.Text = GetData.GetCount(" from vcb_msg_rec_temp where rec_type='SIBS-BR' and to_char(trans_date,'DDMMYYYY')= '" + pickerDate.Value.ToString("ddMMyyyy") + "' ").ToString();

            txtIBPS.Text = GetData.GetCount(" from ibps_msg_rec_tad where to_char(trans_date,'DDMMYYYY')= '" + pickerDate.Value.ToString("ddMMyyyy") + "' ").ToString();
            
            txtSWIFT.Text = GetData.GetCount(" from swift_msg_rec_temp where rec_type='SWIFT-BR' and to_char(trans_date,'DDMMYYYY')= '" + pickerDate.Value.ToString("ddMMyyyy") + "' ").ToString();
            txtVCB.Text = GetData.GetCount(" from vcb_msg_rec_temp where rec_type='VCB-BR' and to_char(trans_date,'DDMMYYYY')= '" + pickerDate.Value.ToString("ddMMyyyy") + "' ").ToString();

            txtOut.Text = GetData.GetCount(" from rec_tb_sibs_out ").ToString();
            txtIn.Text  = GetData.GetCount(" from rec_tb_msgin ").ToString();

            try
            {
                int IsLockIBPS =(int)Convert.ToInt32(GetData.GetSelect(" content from rec_parameters where pname='IBPS_SIBS_LOCKED' and rownum<=1 ").Tables[0].Rows[0][0].ToString());
                
                int IsLockSWIFT = (int)Convert.ToInt32(GetData.GetSelect(" content from rec_parameters where pname='SWIFT_SIBS_LOCKED' and rownum<=1 ").Tables[0].Rows[0][0].ToString());
                int IsLockVCB = (int)Convert.ToInt32(GetData.GetSelect(" content from rec_parameters where pname='VCB_SIBS_LOCKED' and rownum<=1 ").Tables[0].Rows[0][0].ToString());
                if (IsLockIBPS == 0) { btLockIBPS.Visible = false; } else { btLockIBPS.Visible = true; }
               
                if (IsLockSWIFT == 0) { btLockSWIFT.Visible = false; } else { btLockSWIFT.Visible = true; }
                if (IsLockVCB == 0) { btLockVCB.Visible = false; } else { btLockVCB.Visible = true; }
            }
            catch
            { 
                
            }
        }

        private void OnReset(object sender, EventArgs e)
        {
            int iResult;
            if (chkSI.Checked)
            {
                iResult = GetData.ExcuteNonQuery("delete from ibps_msg_rec_temp where to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ");
                if (iResult < 0) { MessageBox.Show("Cannot reset SIBS reconcile messages of IBPS channel "); }
            }
           
            if (chkSW.Checked)
            {
                iResult = GetData.ExcuteNonQuery("delete from swift_msg_rec_temp where rec_type='SIBS-BR' and to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ");
                if (iResult < 0) { MessageBox.Show("Cannot reset SIBS reconcile messages of SWIFT channel "); }
            }
            if (chkSV.Checked)
            {
                iResult = GetData.ExcuteNonQuery("delete from vcb_msg_rec_temp where rec_type='SIBS-BR' and to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ");
                if (iResult < 0) { MessageBox.Show("Cannot reset SIBS reconcile messages of VCB channel "); }
            }
            if (chkSWIFT.Checked)
            {
                iResult = GetData.ExcuteNonQuery("delete from swift_msg_rec_temp where rec_type='SWIFT-BR' and to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ");
                if (iResult < 0) { MessageBox.Show("Cannot reset SWIFT reconcile messages of SWIFT channel "); }
            }
            if (chkVCB.Checked)
            {
                iResult = GetData.ExcuteNonQuery("delete from VCB_msg_rec_temp where rec_type='VCB-BR' and to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ");
                if (iResult < 0) { MessageBox.Show("Cannot reset VCB reconcile messages of VCB channel "); }
            }
            if (chkIBPS.Checked)
            {
                iResult = GetData.ExcuteNonQuery("delete from ibps_msg_rec_tad and to_char(trans_date,'DDMMYYYY')= '"+pickerDate.Value.ToString("ddMMyyyy")+"' ");
                if (iResult < 0) { MessageBox.Show("Cannot reset IBPS reconcile messages of IBPS channel "); }
            }
            
            chkSI.Checked = false; chkST.Checked = false; chkSW.Checked = false; chkSV.Checked = false;
            chkIBPS.Checked = false; chkSWIFT.Checked = false; chkVCB.Checked = false;
        }

        private void OnSIBS(object sender, EventArgs e)
        {
            openFileDialog1.FileName = @"\\10.53.253.15\c$\TESTREC\SIBS\*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
        }

        private void OnSWIFT(object sender, EventArgs e)
        {
            openFileDialog1.FileName = @"\\10.53.253.15\c$\TESTREC\SWIFT\*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
        }

        private void OnVCB(object sender, EventArgs e)
        {
            openFileDialog1.FileName = @"\\10.53.253.15\c$\TESTREC\VCB\*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
        }
        // Lock bang luu tru du lieu doi chieu
        private void OnLock(object sender, EventArgs e)
        {
            if(chkSI.Checked==true) {GetData.ExcuteNonQuery("update rec_parameters set content=1 where pname='IBPS_SIBS_LOCKED' and rownum<=1 ");}
            
            if(chkSW.Checked==true) {GetData.ExcuteNonQuery("update rec_parameters set content=1 where pname='SWIFT_SIBS_LOCKED' and rownum<=1 ");}
            if(chkSV.Checked==true) {GetData.ExcuteNonQuery("update rec_parameters set content=1 where pname='VCB_SIBS_LOCKED' and rownum<=1 "); }
        }
        // UnLock bang luu tru du lieu doi chieu
        private void OnUnLock(object sender, EventArgs e)
        {
            if (chkSI.Checked == true) { GetData.ExcuteNonQuery("update rec_parameters set content=0 where pname='IBPS_SIBS_LOCKED' and rownum<=1 "); }
            
            if (chkSW.Checked == true) { GetData.ExcuteNonQuery("update rec_parameters set content=0 where pname='SWIFT_SIBS_LOCKED' and rownum<=1 "); }
            if (chkSV.Checked == true) { GetData.ExcuteNonQuery("update rec_parameters set content=0 where pname='VCB_SIBS_LOCKED' and rownum<=1 "); }
        }

        private void frmIBPSRecManagement_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message,2, MessageBoxButtons.OK);
            }
        }
    }
}
