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

namespace BR.BRSYSTEM
{
    public partial class frmParam : Form  
    {
        public string RTName;
        public string RptName;
        public string strValue;

        private clsLog objLog = new clsLog();
        private RPTMASTERController controlRPTMASTER = new RPTMASTERController();        

        public frmParam()
        {
            InitializeComponent();
        }

        private void cboCalcel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cboOK_Click(object sender, EventArgs e)
        {
            
            strValue = "";            
            frmReport frmRpt = new frmReport();
            frmParam frmPa = new frmParam();
            frmRpt.NameRPT = RTName;
            DataSet dsParam = new DataSet();
            dsParam = controlRPTMASTER.GetParam(RTName);            
            for (int i = 0; i <= dsParam.Tables[0].Rows.Count - 1; i++)
            {
                string strName = dsParam.Tables[0].Rows[i]["CRLNAME"].ToString();
                if ((strName.Trim()).ToLower() == "textbox")
                {
                    //strValue = strValue + frmPa.Controls["textbox0"].Text.Trim() + "|";
                    
                }
                if ((strName.Trim()).ToLower() == "combobox")
                {
                    //strValue = strValue + frmRpt.Controls["combobox" + i].Text.Trim() + "|";
                }
                if ((strName.Trim()).ToLower() == "picker")
                {
                    //strValue = strValue + frmRpt.Controls["picker" + i].Text.Trim() + "|";
                }
            }
            strValue = strValue.Substring(0, strValue.Length - 1);
            frmRpt.ShowDialog(); 
        }

        private void frmParam_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
        }
    }
}
