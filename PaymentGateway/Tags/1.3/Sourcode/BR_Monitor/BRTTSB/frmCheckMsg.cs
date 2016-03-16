using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BR.BRBusinessObject;
using System.Data.OracleClient;
using BR.BRLib;

namespace BR.BRTTSB
{
    public partial class frmCheckMsg : Form
    {
        string[] vTypes = new string[4] { "Error Message", "Duplicate Message", "Converted Message", "Sent Message"};
        string[] vDirections = new string[3] {"ALL","SIBS-TTSP", "TTSP-SIBS"};
        
        /**************************************
         * Khai bao doi tuong
         **************************************/






        /**************************************
         * Khai bao bien
         **************************************/
        string   vDirection = "";
        //DateTime dFromDate;
        //DateTime dToDate;
        //bool     bChkDate = false;





        
        
        
        public frmCheckMsg()
        {
            InitializeComponent();
        }
        
        /**************************************
         * Su kien Load Form
         **************************************/
        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                cbDirection.Items.Clear();
                cbDirection.Items.Add("SIBS-TTSP");
                cbDirection.Items.Add("TTSP-SIBS");
                cbDirection.Text = cbDirection.Items[0].ToString();
                
                cbType.Items.Clear();
                cbType.Items.Add("Error Message");
                cbType.Items.Add("Duplicate Message");
                cbType.Items.Add("Converted Message");
                cbType.Items.Add("Sent Message");
                cbType.Text = cbType.Items[0].ToString();

                //cbDirection.SelectedIndex = 0;
                //cbType.SelectedIndex = 0;

                chkDate.Checked = false;

                dtgMessage.ReadOnly = true;                
                OnLoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /********************************************
         * Su kien dong Form
         ********************************************/
        private void OnClose(object sender, EventArgs e)
        {
            Close();
        }

        /********************************************
         * Su kien Search
         ********************************************/
        private void OnSearch(object sender, EventArgs e)
        {
            OnLoadData();
        }
        /********************************
         * Timer
         ********************************/ 
        private void OnTimer(object sender, EventArgs e)
        {
            if (chkDate.Checked == true) { pFromDate.Enabled = true; pToDate.Enabled = true; }
            else { pFromDate.Enabled = false; pToDate.Enabled = false;}

            txtTotal.Text= dtgMessage.Rows.Count.ToString();
        }
        /********************************
         * Su kien Load Data
         ********************************/
        private void OnLoadData()
        { 
            string vType="";
            string vDate="";
            string vDirection ="";
            
            // Lay gia tri cac bien
            vType= cbType.Text;
            vDirection = cbDirection.Text;
            if (chkDate.Checked != false)
            {
                vDate  = " and to_char(trans_date,'YYYYMMDD')>=" + pFromDate.Value.Date.ToString("yyyyMMdd") + " ";
                vDate += " and to_char(trans_date,'YYYYMMDD')<=" + pToDate.Value.Date.ToString("yyyyMMdd") + " ";
            }
            else { vDate = " ";}

            dtgMessage.DataSource = null;
            // Tao SQL
            string strSQL = "";
            if (vType == "Error Message")
            {
                if (vDirection == "SIBS-TTSP")
                {
                    strSQL = " select a.query_id QUERY_ID,(select ltrim(rm_number,'0') from rm_sibs_query where query_id=A.Query_Id ) RM_NUMBER"
                           + " ,a.trans_date TRANSDATE from rm_sibs_msg_out A where status=-1 and gwtype='IBS' "
                           + vDate;                    
                }
                if (vDirection == "TTSP-SIBS")
                {
                    strSQL = " select err.query_id QUERY_ID, (select rm_number from rm_sibs_query where query_id=err.query_id and rownum=1) RM_NUMBER,ref_number REF from error_handle err"
                            +" where msg_direction='TTSP-SIBS' and status=-1 "
                            + vDate;
                }
            }
            if (vType == "Duplicate Message")
            {
                if (vDirection == "SIBS-TTSP")
                {
                    strSQL = " select a.query_id QUERY_ID   ,(select ltrim(rm_number,'0') from rm_sibs_query where query_id=A.Query_Id ) RM_NUMBER"
                           + " ,a.trans_date TRANSDATE from rm_sibs_msg_out  A where gwtype='IBS' and status=-2"
                           + vDate;
                }
                if (vDirection == "TTSP-SIBS")
                {
                    strSQL = " select (select rm_number from rm_sibs_query where query_id=err.query_id and rownum=1) RM_NUMBER,ref_number REF from error_handle err"
                            + " where msg_direction='TTSP-SIBS' and status=-2 "
                            + vDate;
                }
            }

            if (vType == "Converted Message")
            {
                if (vDirection == "SIBS-TTSP")
                {
                    strSQL = " select query_id QUERY_ID,rm_number RM_NUMBER,field20 FIELD20,trans_date TRANS_DATE from ttsp_msg_content "
                           + " where status=0 and msg_direction='SIBS-TTSP'"
                           + vDate;
                }
                if (vDirection == "TTSP-SIBS")
                {
                    strSQL = " select query_id QUERY_ID,rm_number RM_NUMBER,field20 FIELD20,trans_date TRANS_DATE from ttsp_msg_content "
                           + " where status=0 and msg_direction='TTSP-SIBS'"
                           + vDate;
                }

            }
            if (vType == "Sent Message")
            {
                if (vDirection == "SIBS-TTSP")
                {
                    strSQL = " select query_id QUERY_ID,rm_number RM_NUMBER,field20 FIELD20,trans_date TRANS_DATE from ttsp_msg_content "
                           + " where status=1 and msg_direction='SIBS-TTSP'"
                           + vDate;
                }
                if (vDirection == "TTSP-SIBS")
                {
                    strSQL = " select query_id QUERY_ID,rm_number RM_NUMBER,field20 FIELD20,trans_date TRANS_DATE from ttsp_msg_content "
                           + " where status=1 and msg_direction='TTSP-SIBS'"
                           + vDate;
                }
            }
          // Load Data            
            GetData.LoadData("Table", strSQL, dtgMessage);              
          // Chinh sua giao dien





        }
        /*********************************
         * Su kien thay doi cbType
         *********************************/ 
        private void OnChangeType(object sender, EventArgs e)
        {
            OnSearch(sender,e);        
        }

        private void lbTotal_Click(object sender, EventArgs e)
        {

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }
        /******************************************
         * Su kien thay doi Direction
         ******************************************/ 
        private void OnDirection(object sender, EventArgs e)
        {
            vDirection = cbDirection.Text;
            //OnLoadData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmCheckMsg_KeyDown(object sender, KeyEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }

        private void frmCheckMsg_MouseMove(object sender, MouseEventArgs e)
        {
            BR.BRLib.Common.bTimer = 1;
        }
    }
}
