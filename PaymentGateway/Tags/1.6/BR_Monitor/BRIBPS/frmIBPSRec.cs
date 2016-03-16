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
using BR.BRSYSTEM;
using System.IO;
using BR.BRSYSTEM.Technique;
using BR.BRIBPS.Reconcile;
using System.Data.OracleClient;

namespace BR.BRIBPS
{
    public partial class frmIBPSRec : BR.BRSYSTEM.frmBasedata
    {
        /*********************************
         * Khai bao doi tuong 
         *********************************/
        private GetData objGetData = new GetData();
        ALLCODEInfo objallcode = new ALLCODEInfo();
        ALLCODEController objallcodecontrol = new ALLCODEController();

        MSG_CONTENT_TEMPInfo dsMsgTemp = new MSG_CONTENT_TEMPInfo();
        MSG_CONTENT_TEMPController dsMsgTempControl = new MSG_CONTENT_TEMPController();

        IBPS_MSG_REC_TEMPInfo dsMsgRec = new IBPS_MSG_REC_TEMPInfo();
        IBPS_MSG_REC_TEMPController dsMsgRecControl = new IBPS_MSG_REC_TEMPController();

        IBPS_MSG_REC_TADInfo dsMsgRecTad = new IBPS_MSG_REC_TADInfo();
        IBPS_MSG_REC_TADController dsMsgRecTadControl = new IBPS_MSG_REC_TADController();

        IBPS_RECONCILEInfo objIBPSRec = new IBPS_RECONCILEInfo();
        IBPS_RECONCILEController objIBPSRecControl = new IBPS_RECONCILEController();

        IBPS_MSG_CONTENTInfo objIBPS = new IBPS_MSG_CONTENTInfo();
        IBPS_MSG_CONTENTController objIBPSControl = new IBPS_MSG_CONTENTController();

        IBPS_BANK_MAPController objIBPSBankControl = new IBPS_BANK_MAPController();

        TADController objTadControl = new TADController();

        IQS_CONDITIONController objIQSControl = new IQS_CONDITIONController();

        SYSVARController objSysvarControl = new SYSVARController();
        frmProcess fProcess=null;

        /*******************************
         * Khai bao bien
         *******************************/
        //private bool NeedConfirm = true;
        public string strDirection;
        public string strDepartment;
        public string strExpre;
        public DateTime strDate;
        public string strType;
        public string strTAD;// => TAD
        int iSIBS = 0;// Tổng số điện ở SIBS
        //int iIBPS = 0;// Tổng số điện ở IBPS
        string vDBLINK_HO= "";// DBLink toi H.O
        string[] vTypeI = new string[2] {"IBPS-BR","BR-IBPS"};
        string[] vTypeS = new string[2] { "SIBS-BR", "BR-SIBS" };
        string[] vTypeR = new string[2] { "TR-BR", "BR-TR" };

        public int intI;
        public int intResult;
        public int intResult1;
        public string strExportFolder = ""; // Đường dẫn tới thư mục có chứa file RMRCON,TFRCON
        public string strFileName = ""; // Biến lưu trữ tên file
        string strPath = "";// Bien luu tru duong dan tuyet doi
        //string vResultSIBS = "";// Biến lưu trữ kết quả đối chiếu SIBS
        string vResultIBPS = "";// Biến lưu trữ kết quả đối chiếu IBPS
        public DataTable dtReconcile;//DatHM add them phuc vu cho in doi chieu

        
        bool bIsProcess = true;// Bien quan li tien trinh

        public frmIBPSRec()
        {
            InitializeComponent();
        }
        /**********************************************
         * Ham doi chieu voi cac cong TAD
         **********************************************/ 
        private int ReconcileTAD(DateTime dtmDate,string strDepart, string strDirec)
        {
            intResult = BR.BRLib.Common.GW_RECONCILE_SUCCESS;
            int j = 0;
            
            try
            {

                /* Insert cac dien da nhan dc/ da chuyen di vao bang Tmp*/                              
                DataSet dsTemp;
                /* Lay toan bo cong TAD dang hoat dong*/
                dsTemp = objTadControl.GetTAD_DBLink_Name();              
                if (dsTemp.Tables[0].Rows.Count > 0)// Quet dien theo cong TAD ket noi DBLINK co Connection=1,DBLINKNAME is not null
                {                    
                    //if (intResult < 0){ MessageBox.Show("Cannot Delete IBPS_MSG_CONTENT_TAD!"); return intResult; }
                   // else
                    //{
                   //     /*Xoa bang luu dien o cac cong CITAD*/
                        //intResult = objIBPSControl.DeleteIBPS_MSG_REC_TAD();
                   //     if (intResult < 0)
                   //     { MessageBox.Show("Cannot Delete IBPS_MSG_REC_TAD!"); return intResult; }
                   // }                    
                    //Insert dien da nhan vao GW vao bang IBPS_MSG_REC_TAD theo DBLINK va gw_bank_code lay dc
                    //string strDBLink_Name = this.strDBLINK;
                        
                    // Add TAD='XXX'
                    //DataRow dr = dsTemp.Tables[0].NewRow();
                    //dr["TAD"] = "TADXXX"; 
                    //dr["DBLINK"] = ""; 
                    //dr["GW_BANK_CODE"] = "XXX";
                    //dr["SIBS_BANK_CODE"] = "XXX";
                    //dsTemp.Tables[0].Rows.Add(dr);
                    vResultIBPS =  " Result Reconcile: " + DateTime.Now.ToString("dd/MM/yyyy") + " \n";
                    vResultIBPS += " TellerID:" + Common.strUsername + " Time: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n"; 
                    vResultIBPS += " IBPS: \n";
                    /* Doi chieu dien theo cong TAD */
                    while (j < dsTemp.Tables[0].Rows.Count)
                    {
                        //iIBPS = 0;
                        //Lay ve xau gia tri thong tin cong TAD. Info={TAD,GW_BANK_CODE,SIBS_BANK_CODE,DBLINK_NAME}
                        string strTAD = dsTemp.Tables[0].Rows[j]["TAD"].ToString();
                        string strDBLink_Name = dsTemp.Tables[0].Rows[j]["DBLINK"].ToString(); if (strDBLink_Name == "") { j = j + 1; vResultIBPS += " "+strTAD+" : 0 messanges[NO DBLINK] \n"; continue; };
                        string strGW_BANK_CODE = dsTemp.Tables[0].Rows[j]["GW_BANK_CODE"].ToString();
                        string strSIBS_BANK_CODE = dsTemp.Tables[0].Rows[j]["SIBS_BANK_CODE"].ToString();
                        
                        // Đẩy điện GW đã lưu vào cổng bảng content_TAD
                        intResult1 = objIBPSControl.InsertIBPS_MSG_CONTENT_Temp_TAD(dtmDate, strDepart, strTAD.Substring(3, 3), strGW_BANK_CODE, strSIBS_BANK_CODE);
                        if (intResult < 0 || intResult1 < 0) { MessageBox.Show("Cannot delete or insert ibps_content_tad. {TAD=" + strTAD + ",dblink =" + strDBLink_Name + ""); }
                        // Đẩy điện của ngân hàng nhà nước vào trong bảng Rec_TAD của TAD đấy
                        intResult = objIBPSControl.InsertIBPS_MSG_REC_TAD(dtmDate, strTAD.Substring(3, 3), strDBLink_Name, vDBLINK_HO);                        
                        if (intResult < 0){MessageBox.Show("Cannot insert into ibps_msg_rec_tad. {TAD="+strTAD+",dblink ="+strDBLink_Name+"");}                        

                        // Đối chiếu đẩy vào bảng IBPS_MSG_REC
                        
                        j = j + 1;
                    }
                        

                }
            }
            catch 
            {MessageBox.Show("Cannot reconcile!");return intResult;}
      
            if (intResult < 0)
            {MessageBox.Show("Cannot reconcile!");return intResult;}
            else return intResult;                
        }

   
        

        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIBPSRec));
            this.datMessage = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbInformation = new System.Windows.Forms.Label();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cbview = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grReconcile = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTAD = new System.Windows.Forms.ComboBox();
            this.pickerDate = new System.Windows.Forms.DateTimePicker();
            this.lbtype = new System.Windows.Forms.Label();
            this.lbdepartment = new System.Windows.Forms.Label();
            this.lbdirection = new System.Windows.Forms.Label();
            this.lbdate = new System.Windows.Forms.Label();
            this.cbdirection = new System.Windows.Forms.ComboBox();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdReconcile = new System.Windows.Forms.Button();
            this.txtSIBS_GW = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.cbSIBS_GW = new System.Windows.Forms.ComboBox();
            this.cbIBPS_GW = new System.Windows.Forms.ComboBox();
            this.cbTR_GW = new System.Windows.Forms.ComboBox();
            this.txtTR_GW = new System.Windows.Forms.TextBox();
            this.txtIBPS_GW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btGetData = new System.Windows.Forms.Button();
            this.btCheckData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btInfoIBPS = new System.Windows.Forms.Button();
            this.btInfoTR = new System.Windows.Forms.Button();
            this.btInfoSIBS = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cboTAD_HO = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grReconcile.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(731, 529);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Click += new System.EventHandler(this.OnClose);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(657, 572);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(569, 572);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(393, 572);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(481, 572);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            // 
            // datMessage
            // 
            this.datMessage.AllowUserToAddRows = false;
            this.datMessage.AllowUserToDeleteRows = false;
            this.datMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.datMessage.BackgroundColor = System.Drawing.Color.White;
            this.datMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datMessage.Location = new System.Drawing.Point(10, 13);
            this.datMessage.Margin = new System.Windows.Forms.Padding(4);
            this.datMessage.Name = "datMessage";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datMessage.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datMessage.Size = new System.Drawing.Size(799, 292);
            this.datMessage.TabIndex = 50;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbInformation);
            this.groupBox2.Controls.Add(this.cmdPrint);
            this.groupBox2.Controls.Add(this.datMessage);
            this.groupBox2.Location = new System.Drawing.Point(7, 220);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(818, 345);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // lbInformation
            // 
            this.lbInformation.AutoSize = true;
            this.lbInformation.Location = new System.Drawing.Point(14, 316);
            this.lbInformation.Name = "lbInformation";
            this.lbInformation.Size = new System.Drawing.Size(0, 16);
            this.lbInformation.TabIndex = 51;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(636, 310);
            this.cmdPrint.Margin = new System.Windows.Forms.Padding(4);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 6;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cbview
            // 
            this.cbview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbview.FormattingEnabled = true;
            this.cbview.Location = new System.Drawing.Point(112, 90);
            this.cbview.Margin = new System.Windows.Forms.Padding(4);
            this.cbview.Name = "cbview";
            this.cbview.Size = new System.Drawing.Size(192, 24);
            this.cbview.TabIndex = 5;
            this.cbview.SelectedIndexChanged += new System.EventHandler(this.OnChangeView);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "View :";
            // 
            // grReconcile
            // 
            this.grReconcile.Controls.Add(this.label4);
            this.grReconcile.Controls.Add(this.cbTAD);
            this.grReconcile.Controls.Add(this.pickerDate);
            this.grReconcile.Controls.Add(this.lbtype);
            this.grReconcile.Controls.Add(this.cbview);
            this.grReconcile.Controls.Add(this.label1);
            this.grReconcile.Controls.Add(this.lbdepartment);
            this.grReconcile.Controls.Add(this.lbdirection);
            this.grReconcile.Controls.Add(this.lbdate);
            this.grReconcile.Controls.Add(this.cbdirection);
            this.grReconcile.Controls.Add(this.cbtype);
            this.grReconcile.Controls.Add(this.cbdepartment);
            this.grReconcile.Location = new System.Drawing.Point(7, -1);
            this.grReconcile.Margin = new System.Windows.Forms.Padding(4);
            this.grReconcile.Name = "grReconcile";
            this.grReconcile.Padding = new System.Windows.Forms.Padding(4);
            this.grReconcile.Size = new System.Drawing.Size(692, 126);
            this.grReconcile.TabIndex = 10;
            this.grReconcile.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(367, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 67;
            this.label4.Text = "TAD :";
            // 
            // cbTAD
            // 
            this.cbTAD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTAD.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTAD.FormattingEnabled = true;
            this.cbTAD.Location = new System.Drawing.Point(449, 90);
            this.cbTAD.Margin = new System.Windows.Forms.Padding(4);
            this.cbTAD.Name = "cbTAD";
            this.cbTAD.Size = new System.Drawing.Size(233, 24);
            this.cbTAD.TabIndex = 66;
            this.cbTAD.SelectedIndexChanged += new System.EventHandler(this.OnTAD);
            // 
            // pickerDate
            // 
            this.pickerDate.CustomFormat = "dd/MM/yyyyy";
            this.pickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerDate.Location = new System.Drawing.Point(112, 26);
            this.pickerDate.Name = "pickerDate";
            this.pickerDate.Size = new System.Drawing.Size(192, 22);
            this.pickerDate.TabIndex = 0;
            this.pickerDate.ValueChanged += new System.EventHandler(this.OnDate);
            // 
            // lbtype
            // 
            this.lbtype.AutoSize = true;
            this.lbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtype.Location = new System.Drawing.Point(367, 62);
            this.lbtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbtype.Name = "lbtype";
            this.lbtype.Size = new System.Drawing.Size(45, 16);
            this.lbtype.TabIndex = 64;
            this.lbtype.Text = "Type :";
            // 
            // lbdepartment
            // 
            this.lbdepartment.AutoSize = true;
            this.lbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdepartment.Location = new System.Drawing.Point(367, 29);
            this.lbdepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdepartment.Name = "lbdepartment";
            this.lbdepartment.Size = new System.Drawing.Size(58, 16);
            this.lbdepartment.TabIndex = 63;
            this.lbdepartment.Text = "Module :";
            // 
            // lbdirection
            // 
            this.lbdirection.AutoSize = true;
            this.lbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdirection.Location = new System.Drawing.Point(16, 62);
            this.lbdirection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdirection.Name = "lbdirection";
            this.lbdirection.Size = new System.Drawing.Size(67, 16);
            this.lbdirection.TabIndex = 62;
            this.lbdirection.Text = "Direction :";
            // 
            // lbdate
            // 
            this.lbdate.AutoSize = true;
            this.lbdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdate.Location = new System.Drawing.Point(16, 29);
            this.lbdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdate.Name = "lbdate";
            this.lbdate.Size = new System.Drawing.Size(43, 16);
            this.lbdate.TabIndex = 61;
            this.lbdate.Text = "Date :";
            // 
            // cbdirection
            // 
            this.cbdirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdirection.FormattingEnabled = true;
            this.cbdirection.Location = new System.Drawing.Point(112, 58);
            this.cbdirection.Margin = new System.Windows.Forms.Padding(4);
            this.cbdirection.Name = "cbdirection";
            this.cbdirection.Size = new System.Drawing.Size(192, 24);
            this.cbdirection.TabIndex = 1;
            this.cbdirection.SelectedIndexChanged += new System.EventHandler(this.OnDirection);
            // 
            // cbtype
            // 
            this.cbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtype.FormattingEnabled = true;
            this.cbtype.Location = new System.Drawing.Point(449, 58);
            this.cbtype.Margin = new System.Windows.Forms.Padding(4);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(233, 24);
            this.cbtype.TabIndex = 3;
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.OnType);
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(449, 25);
            this.cbdepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(233, 24);
            this.cbdepartment.TabIndex = 2;
            this.cbdepartment.SelectedIndexChanged += new System.EventHandler(this.OnDepartment);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(711, 35);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 65;
            this.button1.Text = "&Search";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OnSearch);
            // 
            // cmdReconcile
            // 
            this.cmdReconcile.BackColor = System.Drawing.Color.Blue;
            this.cmdReconcile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdReconcile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReconcile.ForeColor = System.Drawing.Color.Yellow;
            this.cmdReconcile.Location = new System.Drawing.Point(711, 112);
            this.cmdReconcile.Margin = new System.Windows.Forms.Padding(4);
            this.cmdReconcile.Name = "cmdReconcile";
            this.cmdReconcile.Size = new System.Drawing.Size(100, 30);
            this.cmdReconcile.TabIndex = 4;
            this.cmdReconcile.Text = "&Reconcile";
            this.cmdReconcile.UseVisualStyleBackColor = false;
            this.cmdReconcile.Click += new System.EventHandler(this.cmdReconcile_Click);
            // 
            // txtSIBS_GW
            // 
            this.txtSIBS_GW.BackColor = System.Drawing.Color.White;
            this.txtSIBS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSIBS_GW.Location = new System.Drawing.Point(138, 143);
            this.txtSIBS_GW.Name = "txtSIBS_GW";
            this.txtSIBS_GW.ReadOnly = true;
            this.txtSIBS_GW.Size = new System.Drawing.Size(108, 21);
            this.txtSIBS_GW.TabIndex = 67;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimer);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnProcess);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Yellow;
            this.button2.Location = new System.Drawing.Point(711, 185);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 73;
            this.button2.Text = " &Extract";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.OnExtract);
            // 
            // cbSIBS_GW
            // 
            this.cbSIBS_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSIBS_GW.DropDownWidth = 80;
            this.cbSIBS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSIBS_GW.FormattingEnabled = true;
            this.cbSIBS_GW.Items.AddRange(new object[] {
            "SIBS->BR",
            "SIBS<-BR"});
            this.cbSIBS_GW.Location = new System.Drawing.Point(53, 143);
            this.cbSIBS_GW.Name = "cbSIBS_GW";
            this.cbSIBS_GW.Size = new System.Drawing.Size(82, 20);
            this.cbSIBS_GW.TabIndex = 76;
            // 
            // cbIBPS_GW
            // 
            this.cbIBPS_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbIBPS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIBPS_GW.FormattingEnabled = true;
            this.cbIBPS_GW.Items.AddRange(new object[] {
            "BR->IBPS",
            "BR<-IBPS"});
            this.cbIBPS_GW.Location = new System.Drawing.Point(413, 164);
            this.cbIBPS_GW.Name = "cbIBPS_GW";
            this.cbIBPS_GW.Size = new System.Drawing.Size(82, 20);
            this.cbIBPS_GW.TabIndex = 78;
            // 
            // cbTR_GW
            // 
            this.cbTR_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbTR_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTR_GW.FormattingEnabled = true;
            this.cbTR_GW.Items.AddRange(new object[] {
            "TR->BR"});
            this.cbTR_GW.Location = new System.Drawing.Point(52, 184);
            this.cbTR_GW.Name = "cbTR_GW";
            this.cbTR_GW.Size = new System.Drawing.Size(82, 20);
            this.cbTR_GW.TabIndex = 80;
            // 
            // txtTR_GW
            // 
            this.txtTR_GW.BackColor = System.Drawing.Color.White;
            this.txtTR_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTR_GW.Location = new System.Drawing.Point(137, 184);
            this.txtTR_GW.Name = "txtTR_GW";
            this.txtTR_GW.ReadOnly = true;
            this.txtTR_GW.Size = new System.Drawing.Size(108, 21);
            this.txtTR_GW.TabIndex = 79;
            // 
            // txtIBPS_GW
            // 
            this.txtIBPS_GW.BackColor = System.Drawing.Color.White;
            this.txtIBPS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIBPS_GW.Location = new System.Drawing.Point(501, 164);
            this.txtIBPS_GW.Name = "txtIBPS_GW";
            this.txtIBPS_GW.ReadOnly = true;
            this.txtIBPS_GW.Size = new System.Drawing.Size(108, 21);
            this.txtIBPS_GW.TabIndex = 77;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(304, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "BR";
            // 
            // btGetData
            // 
            this.btGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btGetData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetData.Location = new System.Drawing.Point(711, 73);
            this.btGetData.Name = "btGetData";
            this.btGetData.Size = new System.Drawing.Size(100, 30);
            this.btGetData.TabIndex = 82;
            this.btGetData.Text = "GetData";
            this.btGetData.UseVisualStyleBackColor = false;
            this.btGetData.Click += new System.EventHandler(this.OnGetData);
            // 
            // btCheckData
            // 
            this.btCheckData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCheckData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCheckData.Location = new System.Drawing.Point(711, 149);
            this.btCheckData.Name = "btCheckData";
            this.btCheckData.Size = new System.Drawing.Size(100, 30);
            this.btCheckData.TabIndex = 83;
            this.btCheckData.Text = "CheckData";
            this.btCheckData.UseVisualStyleBackColor = true;
            this.btCheckData.Click += new System.EventHandler(this.OnCheckData);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(252, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "SIBS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(254, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 85;
            this.label5.Text = "TR";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(341, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 86;
            this.label6.Text = "IBPS";
            // 
            // btInfoIBPS
            // 
            this.btInfoIBPS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoIBPS.BackgroundImage")));
            this.btInfoIBPS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoIBPS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoIBPS.Location = new System.Drawing.Point(382, 164);
            this.btInfoIBPS.Name = "btInfoIBPS";
            this.btInfoIBPS.Size = new System.Drawing.Size(25, 20);
            this.btInfoIBPS.TabIndex = 104;
            this.btInfoIBPS.UseVisualStyleBackColor = true;
            this.btInfoIBPS.Click += new System.EventHandler(this.OnIBPS);
            // 
            // btInfoTR
            // 
            this.btInfoTR.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoTR.BackgroundImage")));
            this.btInfoTR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoTR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoTR.Location = new System.Drawing.Point(21, 184);
            this.btInfoTR.Name = "btInfoTR";
            this.btInfoTR.Size = new System.Drawing.Size(25, 20);
            this.btInfoTR.TabIndex = 103;
            this.btInfoTR.UseVisualStyleBackColor = true;
            this.btInfoTR.Click += new System.EventHandler(this.OnTR);
            // 
            // btInfoSIBS
            // 
            this.btInfoSIBS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoSIBS.BackgroundImage")));
            this.btInfoSIBS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoSIBS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoSIBS.Location = new System.Drawing.Point(22, 143);
            this.btInfoSIBS.Name = "btInfoSIBS";
            this.btInfoSIBS.Size = new System.Drawing.Size(25, 20);
            this.btInfoSIBS.TabIndex = 102;
            this.btInfoSIBS.UseVisualStyleBackColor = true;
            this.btInfoSIBS.Click += new System.EventHandler(this.OnSIBS);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(307, 142);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 20);
            this.button3.TabIndex = 105;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnGW);
            // 
            // cboTAD_HO
            // 
            this.cboTAD_HO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTAD_HO.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTAD_HO.FormattingEnabled = true;
            this.cboTAD_HO.Location = new System.Drawing.Point(616, 162);
            this.cboTAD_HO.Margin = new System.Windows.Forms.Padding(4);
            this.cboTAD_HO.Name = "cboTAD_HO";
            this.cboTAD_HO.Size = new System.Drawing.Size(71, 24);
            this.cboTAD_HO.TabIndex = 1;
            this.cboTAD_HO.SelectedIndexChanged += new System.EventHandler(this.OnDirection);
            // 
            // frmIBPSRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(834, 569);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btInfoIBPS);
            this.Controls.Add(this.btInfoTR);
            this.Controls.Add(this.btInfoSIBS);
            this.Controls.Add(this.cboTAD_HO);
            this.Controls.Add(this.grReconcile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btGetData);
            this.Controls.Add(this.btCheckData);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdReconcile);
            this.Controls.Add(this.cbIBPS_GW);
            this.Controls.Add(this.txtIBPS_GW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbTR_GW);
            this.Controls.Add(this.txtTR_GW);
            this.Controls.Add(this.txtSIBS_GW);
            this.Controls.Add(this.cbSIBS_GW);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmIBPSRec";
            this.Text = "IBPS Reconcile";
            this.Load += new System.EventHandler(this.frmIBPSRec_Load_1);
            this.Controls.SetChildIndex(this.cbSIBS_GW, 0);
            this.Controls.SetChildIndex(this.txtSIBS_GW, 0);
            this.Controls.SetChildIndex(this.txtTR_GW, 0);
            this.Controls.SetChildIndex(this.cbTR_GW, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtIBPS_GW, 0);
            this.Controls.SetChildIndex(this.cbIBPS_GW, 0);
            this.Controls.SetChildIndex(this.cmdReconcile, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.btCheckData, 0);
            this.Controls.SetChildIndex(this.btGetData, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grReconcile, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cboTAD_HO, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.btInfoSIBS, 0);
            this.Controls.SetChildIndex(this.btInfoTR, 0);
            this.Controls.SetChildIndex(this.btInfoIBPS, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grReconcile.ResumeLayout(false);
            this.grReconcile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridView datMessage;
        private GroupBox groupBox2;
        private ComboBox cbview;
        private Label label1;
        private GroupBox grReconcile;
        private DateTimePicker pickerDate;
        private Button cmdReconcile;
        private Label lbtype;
        private Label lbdepartment;
        private Label lbdirection;
        private Label lbdate;
        private ComboBox cbdirection;
        private ComboBox cbtype;
        private ComboBox cbdepartment;

        /****************************************
         * Su kien Load Form      
         ****************************************/
        private void frmIBPSRec_Load_1(object sender, EventArgs e)
        {
            try
            {
                TADController objControl = new TADController();
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                // Thong tin doi chieu
                cbSIBS_GW.SelectedIndex = 0;
                cbIBPS_GW.SelectedIndex = 0;
                cbTR_GW.SelectedIndex = 0;
                cmdPrint.Enabled = false;//DatHM add
                datMessage.ReadOnly = true;
                pickerDate.MaxDate = DateTime.Now;
                if (!objGetData.FillDataComboBox(cbdepartment, "CONTENT", "CONTENT", "ALLCODE",
                    "GWTYPE='SYSTEM' AND cdname='Department'", "CONTENT", true, true, "ALL"))
                    return;
                if (!objGetData.FillDataComboBox(cbdirection, "CONTENT", "CONTENT", "ALLCODE",
                    "GWTYPE='IBPS' AND cdname='DIRECTION'", "CONTENT", true, true, "ALL"))
                    return;
                if (!objGetData.FillDataComboBox(cbtype, "CONTENT", "CONTENT", "ALLCODE",
                    "GWTYPE='IBPS' AND cdname='TYPE_REC'", "CONTENT", true, true, "ALL"))
                    return;
                DataTable _dt = new DataTable();
                _dt = objControl.GET_HO_TAD();
                // lay ma 3 so hoi so chinh
                cboTAD_HO.DataSource = _dt;
                cboTAD_HO.ValueMember = "SIBS_CODE";
                cboTAD_HO.DisplayMember = "SIBS_CODE";
                // het them
                cbTAD.Items.Add("ALL");
                DataSet dsTemp;
                /* Lay toan bo cong TAD dang hoat dong*/
                dsTemp = objTadControl.GetTAD_DBLink_Name();
                for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                {
                    cbTAD.Items.Add(dsTemp.Tables[0].Rows[i]["TAD"].ToString().Substring(3, 3));
                }
                cbTAD.SelectedIndex = 0;

                //strExportFolder = GetData.GetRecParameter("PATH_REC", "SYSTEM", "PATH");
                //if (strExportFolder =="" ) { MessageBox.Show("Cannot find PATH_REC SIBS-IBPS"); return; }

                //vDBLINK_HO = GetData.GetSelect("  ");
                //if (vDBLINK_HO == "") { MessageBox.Show("Cannot find DBLINK H.O"); return; }

                datMessage.DataSource = GetData.GetSelect(" '' RM_NUMBER,'' REF_NUMBER,'' AMOUNT,'' CCY,'' TAD ,'' SENDER,'' RECEIVER,'' TRANS_DATE from dual where rownum=0").Tables[0];

                pickerDate.Focus();
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        
        /****************************************
         * Su kien kich hoat nut ScanData
         ****************************************/
        private void cmdReconcile_Click(object sender, EventArgs e)
        {                     
            txtSIBS_GW.Text = "";            
            // Kiem tra xem chuc nang co busy ko
            
            // Lay thong tin truong Direction
            #region Direction
            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            }
            else strDirection = cbdirection.Text.Trim();
            #endregion
            // Lay thong tin truong Department
            #region Department
            strDepartment = cbdepartment.Text.Trim();
            #endregion            
            // Lay thong tin truong Date
            #region pDate
            strDate = pickerDate.Value;
            #endregion            
            // Lay thong tin truong Type
            #region Type
            strType = cbtype.Text.Trim();
            #endregion            
            // Lay thong tin truong View
            #region View
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            #endregion
            // Lay thong tin truong TAD
            #region TAD
            strTAD = cbTAD.Text;
            #endregion

            // Doi chieu dien SIBS-BR
            int iResult = clsIBPS_MSG_REC.RECONCILE_SIBS(pickerDate.Value,Common.Userid);
            if (iResult < 0) { MessageBox.Show(" cannot reconcile SIBS <-> BR !!", Common.sCaption); }            
            // Doi chieu dien TR
            int iResult1 = clsIBPS_MSG_REC.RECONCILE_TR(pickerDate.Value, Common.Userid);
            if (iResult1 < 0) { MessageBox.Show(" cannot reconcile TR <-> BR !!", Common.sCaption); }            
            // Doi chieu dien cua TAD
            int iResult2 = clsIBPS_MSG_REC.RECONCILE_IBPS(pickerDate.Value, cboTAD_HO.Text, Common.Userid);
            if (iResult2 < 0) { MessageBox.Show(" cannot reconcile IBPS <-> BR !!", Common.sCaption); }
            if ((iResult >= 0) && (iResult1 >= 0) && (iResult2 >= 0)) { MessageBox.Show("Reconcile successfully!!", Common.sCaption); }            
            
           
        }
        /************************************
         * Kiem tra cac comboBox da dc chon
         ************************************/ 
        private int Check()
        {
            if (string.IsNullOrEmpty(cbdirection.Text.Trim()))
            {
                MessageBox.Show("You must choice Direction");
                cbdirection.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbdepartment.Text.Trim()))
            {
                MessageBox.Show("You must choice Department");
                cbdepartment.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(strDate.ToString()))
            {
                MessageBox.Show("You must choice Date");
                pickerDate.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbtype.Text.Trim()))
            {
                MessageBox.Show("You must choice Type");
                cbtype.Focus();
                return -1;
            }
            else if (string.IsNullOrEmpty(cbview.Text.Trim()))
            {
                MessageBox.Show("You must choice View");
                cbview.Focus();
                return -1;
            }
            return 1;
        }

        /************************************
         * Ham load du lieu len tren luoi
         ************************************/
        private void LoadData()
        {
            // Kiem tra thong tin do user dien vao
            if (Check() < 0)
                return;
            // Lay thong tin truong Direction
            #region Direction
            string strDirection="ALL";
            if (cbdirection.Text == "SIBS-IBPS") { strDirection = "O"; }
            if (cbdirection.Text == "IBPS-SIBS") { strDirection = "I"; }
            #endregion
            // Lay thong tin truong Department
            #region Department
            strDepartment = cbdepartment.Text.Trim();
            #endregion
            // Lay thong tin truong Date
            #region pDate
            strDate = pickerDate.Value;
            #endregion
            // Lay thong tin truong Type
            #region Type
            strType = "ALL";
            if (cbtype.Text == vTypeS[0] || cbtype.Text == vTypeS[1]) { strType = "SIBS-BR"; }
            if (cbtype.Text == vTypeI[0] || cbtype.Text == vTypeI[1]) { strType = "IBPS-BR"; }
            if (cbtype.Text == vTypeR[0] || cbtype.Text == vTypeR[1]) { strType = "TR-BR"; }
            #endregion
            // Lay thong tin truong View
            #region View
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable dt1;
                dt1 = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = dt1.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = "ALL";
            #endregion
            // Lay thong tin truong TAD
            #region TAD
            strTAD = cbTAD.Text;
            #endregion
                        
            DataTable dt;            
            /* Lay du lieu theo ngay */
            dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, strType, strDirection, strExpre, strTAD,strDepartment);
            datMessage.DataSource = dt;
            try
            {
                dtReconcile = dt;//DatHM add de in Reconcile
                //DatHM add
                if (dt.Rows.Count > 0)
                {
                    cmdPrint.Enabled = true;
                }
                else
                {
                    cmdPrint.Enabled = false;
                }
                //DatHM add
            }
            catch
            {
                MessageBox.Show("No data found!", Common.sCaption);
            }            
        }
        
       
        
        
        private Button cmdPrint;
        private Button button1;

        /*******************************************
         * Su kien kich hoat nut Reconcile(Search)
         *******************************************/
        private void OnSearch(object sender, EventArgs e)
        {
            /// Kiem tra thong tin do user dien vao
            if (Check() < 0)
                return;
            // Lay thong tin truong Direction
            #region Direction
            if (cbdirection.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbdirection.Text.Trim(), "DIRECTION");
                strDirection = ds.Rows[0]["CDVAL"].ToString();
            }
            else strDirection = cbdirection.Text.Trim();
            #endregion
            // Lay thong tin truong Department
            #region Department
            strDepartment = cbdepartment.Text.Trim();
            #endregion
            // Lay thong tin truong Date
            #region pDate
            strDate = pickerDate.Value;
            #endregion
            // Lay thong tin truong Type
            #region Type
            strType = "ALL";
            if (cbtype.Text == vTypeI[0] || cbtype.Text == vTypeI[1]) { strType = "IBPS-BR"; }
            if (cbtype.Text == vTypeS[0] || cbtype.Text == vTypeS[1]) { strType = "SIBS-BR"; }

            #endregion
            // Lay thong tin truong View
            #region View
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
            #endregion
            // Lay thong tin truong TAD
            #region TAD
            strTAD = cbTAD.Text;
            #endregion
            
            try {LoadData();}
            catch { MessageBox.Show("No Data in pDate"); };
        }
        /***************************************
         * Su kien thay doi comboView
         ***************************************/
        private void OnChangeView(object sender, EventArgs e)
        {
            if (cbview.Text.Trim() == "System.Data.DataRowView")
                return;
            if (cbview.Text.Trim() != "ALL")
            {
                DataTable ds;
                ds = objallcodecontrol.GetALLCODE_code1(cbview.Text.Trim(), "RecView");
                strExpre = ds.Rows[0]["CDVAL"].ToString();
            }
            else strExpre = cbview.Text.Trim();
        }

        private Label label4;
        private ComboBox cbTAD;

        /***************************************
         * Su kien thay doi comboTAD
         ***************************************/
        private void OnTAD(object sender, EventArgs e)
        {






        }
        private TextBox txtSIBS_GW;
        private Timer timer1;
        private IContainer components;

        /***************************************
         * Timer
         ***************************************/
        private void OnTimer(object sender, EventArgs e)
        {
            try
            {
                lbInformation.Text = "Total Messages : "+datMessage.Rows.Count.ToString();    
            }
            catch 
            {
                lbInformation.Text = "";
            };
        }

        /****************************************
         * Thong bao dien ket qua doi chieu SIBS
         ****************************************/ 
        private void OnSIBS(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbSIBS_GW.Text == "SIBS->BR")
            {
                dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, "SIBS-BR", "O", "ALL","ALL","ALL");
            }
            if (cbSIBS_GW.Text == "SIBS<-BR")
            {
                dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, "SIBS-BR", "I", "ALL","ALL","ALL");
            }
            datMessage.DataSource = dt;
        }
        /****************************************
         * Thong bao dien ket qua doi chieu SIBS
         ****************************************/
        private void OnIBPS(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbIBPS_GW.Text == "BR->IBPS")
            {
                dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, "IBPS-BR", "O", "ALL", cboTAD_HO.Text, "ALL");
            }
            if (cbIBPS_GW.Text == "BR<-IBPS")
            {
                dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, "IBPS-BR", "I", "ALL", cboTAD_HO.Text, "ALL");
            }
            datMessage.DataSource = dt;
        }

        private BackgroundWorker backgroundWorker1;

        /************************************
         * Thuc hien quan li tac vu Process
         ************************************/ 
        private void OnProcess(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                while (bIsProcess == true)
                {
                    if (fProcess == null)
                    {
                        fProcess = new frmProcess();
                        fProcess.TopMost = true;
                        iSIBS = 0;
                    }
                    System.Threading.Thread.Sleep(50);
                    if (iSIBS < fProcess.progressBar1.Maximum) { fProcess.progressBar1.Value = iSIBS;}
                }
                if (fProcess != null) { fProcess.Close(); fProcess.Dispose(); fProcess = null; }
            }
        }

        private void OnDepartment(object sender, EventArgs e)
        {
            this.strDepartment = cbdepartment.Text;
        }

        private void OnDirection(object sender, EventArgs e)
        {
            strDirection = cbdirection.Text;

            if (strDirection == "SIBS-IBPS")
            {
                cbtype.DataSource = null;
                cbtype.Items.Clear();
                cbtype.Items.Add("ALL");
                cbtype.Items.Add("SIBS-BR");
                cbtype.Items.Add("BR-IBPS");
                cbtype.SelectedIndex = 0;
            }
            if (strDirection == "IBPS-SIBS")
            {
                cbtype.DataSource = null;
                cbtype.Items.Clear();
                cbtype.Items.Add("ALL");
                cbtype.Items.Add("IBPS-BR");
                cbtype.Items.Add("BR-SIBS");
                cbtype.SelectedIndex = 0;
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnDate(object sender, EventArgs e)
        {
            strDate = pickerDate.Value;
        }

        private void OnType(object sender, EventArgs e)
        {
            //cbview.Items.Clear();
            if (cbtype.Text.Trim() == "ALL")
            {
                if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                "gwtype='IBPS' and cdname='RecView'", "CONTENT", true, true, "ALL"))
                    return;                
            }
            else
            {
                string strType = "";
                if (cbtype.Text.Trim() == vTypeS[0] || cbtype.Text.Trim() == vTypeS[1]) { strType = "SIBS-BR"; }
                if (cbtype.Text.Trim() == vTypeI[0] || cbtype.Text.Trim() == vTypeI[1]) { strType = "IBPS-BR"; }
                if (!objGetData.FillDataComboBox(cbview, "CONTENT", "CONTENT", "ALLCODE",
                   "gwtype='IBPS' and cdname='RecView' and DIRECTION='" + strType + "'",
                   "CONTENT", true, true, "ALL"))
                    return;

            }
            this.strType = cbtype.Text.Trim();
        }
        private Button button2;

        /************************************
         * Hàm trích chọn dữ liệu về cho CN
         ************************************/
        private void OnExtract(object sender, EventArgs e)
        {
            Branch.frmExtractData fExtract = new BR.BRIBPS.Branch.frmExtractData();
            fExtract.ShowDialog();
        }

        
        public void OnShowResult(DateTime pDate)
        {
            //string vSQL = "";
            //string vText = "";
            //string vNumber = "";


            OracleParameter[] param = {new OracleParameter("pDate",OracleType.DateTime,20)
                                    ,new OracleParameter("vText",OracleType.VarChar,3000)};
            param[1].Direction = ParameterDirection.InputOutput;
            param[0].Value = pDate;
            param[1].Value = "";
            int iResult = GetData.ExcuteStore("gw_pk_rec_ibps.tbaoibps_sibs_gw", param);
            MessageBox.Show(param[1].Value.ToString(), Common.sCaption);

        }

        private void OnInOut(object sender, EventArgs e)
        {

        }
        private ComboBox cbSIBS_GW;
        private ComboBox cbIBPS_GW;
        private ComboBox cbTR_GW;
        private TextBox txtTR_GW;
        private TextBox txtIBPS_GW;
        private Label label2;
        private Button btGetData;
        private Button btCheckData;



        // Do du lieu vao cac bang tham gia doi chieu
        private void OnGetData(object sender, EventArgs e)
        {
            
            int iResult = clsIBPS_MSG_REC.InsertIBPS_MSG_CONTENT_Temp(pickerDate.Value);
            if (iResult == -1) { MessageBox.Show("Cannot insert_content_temp", Common.sCaption); }
            int iResult1 = clsIBPS_MSG_REC.InsertIBPS_MSG_CONTENT_TAD(pickerDate.Value, cboTAD_HO.Text);
            if (iResult1 == -1) { MessageBox.Show("Cannot insert_content_tad", Common.sCaption); }
            int iResult2 = clsIBPS_MSG_REC.InsertIBPS_MSG_REC_TAD(pickerDate.Value, cboTAD_HO.Text, "IBPS1");
            if (iResult2 == -1) { MessageBox.Show("Cannot insert_rec_tad", Common.sCaption); }
            int iResult3 = clsIBPS_MSG_REC.InsertIBPS_MSG_REC_Temp(pickerDate.Value);
            if (iResult3 == -1) { MessageBox.Show("Cannot insert_rec_temp", Common.sCaption); }
            if (iResult > 0 && iResult1 > 0 && iResult2 > 0 && iResult3 > 0) { MessageBox.Show("Get data successfully !! ", Common.sCaption); }
        }

        // Xem lai du lieu da doi chieu
        private void OnCheckData(object sender, EventArgs e)
        {
            txtSIBS_GW.Text = clsIBPS_MSG_REC.GetIBPS_Index(pickerDate.Value,cbSIBS_GW.Text,cboTAD_HO.Text);
            txtIBPS_GW.Text = clsIBPS_MSG_REC.GetIBPS_Index(pickerDate.Value, cbIBPS_GW.Text, cboTAD_HO.Text);
            txtTR_GW.Text = clsIBPS_MSG_REC.GetIBPS_Index(pickerDate.Value, cbTR_GW.Text, cboTAD_HO.Text);
        }

        private Label label3;
        private Label label5;
        private Label label6;
        private Label lbInformation;
        private Button btInfoIBPS;
        private Button btInfoTR;
        private Button btInfoSIBS;

        private void OnTR(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (cbTR_GW.Text == "TR->BR")
            {
                dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, "TR-BR", "O", "ALL", "ALL", "ALL");
            }
            if (cbTR_GW.Text == "TR<-BR")
            {
                dt = clsIBPS_MSG_REC.GetIBPS_MSG_REC(pickerDate.Value, "TR-BR", "I", "ALL", "ALL", "ALL");
            }
            datMessage.DataSource = dt;
        }

        private Button button3;

        private void OnGW(object sender, EventArgs e)
        {
            DataTable dt = clsIBPS_MSG_REC.GetIBPS_MSG_CONTENT(pickerDate.Value);
            datMessage.DataSource = dt;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrint frmPrint = new frmPrint();
                string Print = "IBPS_PRINT_RECONCILE";
                frmPrint.PrintType = Print;
                frmPrint.HMdat = dtReconcile;
                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Error:" + ex, Common.sCaption);  
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private ComboBox cboTAD_HO;       
            
        
    }
}
