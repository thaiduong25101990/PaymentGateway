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
    public partial class frmViewLogInfo : Form
    {
        private clsLog objLog = new clsLog();
        private USER_MSG_LOGInfo objInfo = new USER_MSG_LOGInfo();
        private USER_MSG_LOGController objControl = new USER_MSG_LOGController();

        public frmViewLogInfo()
        {
            InitializeComponent();
        }

        private void frmViewLogInfo_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
        }
        private void LoadDataTAD()
        {
            try
            {
                DataSet datDs = new DataSet();
                datDs = objControl.GetViewLogInfo("IBPS TAD list");
                FomatGrid.ColumsHeaderDataGridView(dgView);
                if (datDs.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    dgView.DataSource = datDs.Tables[0];
                    //dgView.Columns[0].HeaderText = "State bank code";
                    dgView.Columns[0].Width = 110;
                    //dgView.Columns[1].HeaderText = "SIBS bank code";
                    dgView.Columns[1].Width = 110;
                    //dgView.Columns[2].HeaderText = "Bank name";
                    dgView.Columns[2].Width = 250;
                    //dgView.Columns[3].HeaderText = "Branch name";
                    dgView.Columns[3].Width = 250;
                    dgView.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }
}
