namespace BR.BRIBPS
{
    partial class frmTADInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTADInfo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSBV_TAD = new System.Windows.Forms.TextBox();
            this.txtDBLink = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkCentralized = new System.Windows.Forms.CheckBox();
            this.lblCentralized = new System.Windows.Forms.Label();
            this.cboFunction = new System.Windows.Forms.ComboBox();
            this.cboConnection = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblSIBSBankCode = new System.Windows.Forms.Label();
            this.lblGWBankCode = new System.Windows.Forms.Label();
            this.chkHVLVAllow = new System.Windows.Forms.CheckBox();
            this.cboCitadStatus = new System.Windows.Forms.ComboBox();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.txtSIBSBankCode = new System.Windows.Forms.TextBox();
            this.txtGWBankCode = new System.Windows.Forms.TextBox();
            this.txtCitadID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFunction = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mskHour = new System.Windows.Forms.MaskedTextBox();
            this.cboCCYCD = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.cmdExportPath = new System.Windows.Forms.Button();
            this.cmdImportPath = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTotalTAD = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtFTPDirectory = new System.Windows.Forms.TextBox();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.txtImportPath = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.cboAreaView = new System.Windows.Forms.ComboBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdHistory = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(778, 619);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdClose_MouseMove);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.cmdClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdClose_MouseUp);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(606, 619);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(523, 619);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(357, 619);
            this.cmdAdd.TabIndex = 0;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(440, 619);
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSBV_TAD);
            this.groupBox1.Controls.Add(this.txtDBLink);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkCentralized);
            this.groupBox1.Controls.Add(this.lblCentralized);
            this.groupBox1.Controls.Add(this.cboFunction);
            this.groupBox1.Controls.Add(this.cboConnection);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.lblSIBSBankCode);
            this.groupBox1.Controls.Add(this.lblGWBankCode);
            this.groupBox1.Controls.Add(this.chkHVLVAllow);
            this.groupBox1.Controls.Add(this.cboCitadStatus);
            this.groupBox1.Controls.Add(this.cboArea);
            this.groupBox1.Controls.Add(this.txtSIBSBankCode);
            this.groupBox1.Controls.Add(this.txtGWBankCode);
            this.groupBox1.Controls.Add(this.txtCitadID);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblFunction);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(849, 198);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "SBV Tad ID :";
            // 
            // txtSBV_TAD
            // 
            this.txtSBV_TAD.Location = new System.Drawing.Point(124, 100);
            this.txtSBV_TAD.Name = "txtSBV_TAD";
            this.txtSBV_TAD.Size = new System.Drawing.Size(145, 22);
            this.txtSBV_TAD.TabIndex = 3;
            this.txtSBV_TAD.TextChanged += new System.EventHandler(this.txtSBV_TAD_TextChanged);
            this.txtSBV_TAD.Leave += new System.EventHandler(this.txtSBV_TAD_Leave);
            // 
            // txtDBLink
            // 
            this.txtDBLink.AcceptsReturn = true;
            this.txtDBLink.Location = new System.Drawing.Point(671, 159);
            this.txtDBLink.Name = "txtDBLink";
            this.txtDBLink.Size = new System.Drawing.Size(166, 22);
            this.txtDBLink.TabIndex = 10;
            this.txtDBLink.Leave += new System.EventHandler(this.txtDBLink_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(567, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "DBLink Name :";
            // 
            // chkCentralized
            // 
            this.chkCentralized.AutoSize = true;
            this.chkCentralized.Location = new System.Drawing.Point(671, 104);
            this.chkCentralized.Name = "chkCentralized";
            this.chkCentralized.Size = new System.Drawing.Size(15, 14);
            this.chkCentralized.TabIndex = 8;
            this.chkCentralized.UseVisualStyleBackColor = true;
            // 
            // lblCentralized
            // 
            this.lblCentralized.AutoSize = true;
            this.lblCentralized.Location = new System.Drawing.Point(567, 103);
            this.lblCentralized.Name = "lblCentralized";
            this.lblCentralized.Size = new System.Drawing.Size(81, 16);
            this.lblCentralized.TabIndex = 12;
            this.lblCentralized.Text = "Centralized :";
            // 
            // cboFunction
            // 
            this.cboFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFunction.FormattingEnabled = true;
            this.cboFunction.Location = new System.Drawing.Point(672, 43);
            this.cboFunction.Name = "cboFunction";
            this.cboFunction.Size = new System.Drawing.Size(168, 24);
            this.cboFunction.TabIndex = 6;
            // 
            // cboConnection
            // 
            this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnection.FormattingEnabled = true;
            this.cboConnection.Location = new System.Drawing.Point(124, 158);
            this.cboConnection.Name = "cboConnection";
            this.cboConnection.Size = new System.Drawing.Size(241, 24);
            this.cboConnection.TabIndex = 5;
            this.cboConnection.SelectedIndexChanged += new System.EventHandler(this.cboConnection_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 162);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 16);
            this.label18.TabIndex = 9;
            this.label18.Text = "Connection :";
            // 
            // lblSIBSBankCode
            // 
            this.lblSIBSBankCode.AutoSize = true;
            this.lblSIBSBankCode.Location = new System.Drawing.Point(285, 75);
            this.lblSIBSBankCode.Name = "lblSIBSBankCode";
            this.lblSIBSBankCode.Size = new System.Drawing.Size(116, 16);
            this.lblSIBSBankCode.TabIndex = 4;
            this.lblSIBSBankCode.Text = "lblSIBSBankCode";
            // 
            // lblGWBankCode
            // 
            this.lblGWBankCode.AutoSize = true;
            this.lblGWBankCode.Location = new System.Drawing.Point(285, 47);
            this.lblGWBankCode.Name = "lblGWBankCode";
            this.lblGWBankCode.Size = new System.Drawing.Size(109, 16);
            this.lblGWBankCode.TabIndex = 4;
            this.lblGWBankCode.Text = "lblGWBankCode";
            // 
            // chkHVLVAllow
            // 
            this.chkHVLVAllow.AutoSize = true;
            this.chkHVLVAllow.Location = new System.Drawing.Point(671, 76);
            this.chkHVLVAllow.Name = "chkHVLVAllow";
            this.chkHVLVAllow.Size = new System.Drawing.Size(15, 14);
            this.chkHVLVAllow.TabIndex = 7;
            this.chkHVLVAllow.UseVisualStyleBackColor = true;
            // 
            // cboCitadStatus
            // 
            this.cboCitadStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCitadStatus.FormattingEnabled = true;
            this.cboCitadStatus.Location = new System.Drawing.Point(671, 128);
            this.cboCitadStatus.Name = "cboCitadStatus";
            this.cboCitadStatus.Size = new System.Drawing.Size(166, 24);
            this.cboCitadStatus.TabIndex = 9;
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(124, 128);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(241, 24);
            this.cboArea.TabIndex = 4;
            // 
            // txtSIBSBankCode
            // 
            this.txtSIBSBankCode.AcceptsReturn = true;
            this.txtSIBSBankCode.Location = new System.Drawing.Point(124, 72);
            this.txtSIBSBankCode.MaxLength = 3;
            this.txtSIBSBankCode.Name = "txtSIBSBankCode";
            this.txtSIBSBankCode.Size = new System.Drawing.Size(145, 22);
            this.txtSIBSBankCode.TabIndex = 2;
            this.txtSIBSBankCode.TextChanged += new System.EventHandler(this.txtSIBSBankCode_TextChanged);
            this.txtSIBSBankCode.Leave += new System.EventHandler(this.txtSIBSBankCode_Leave);
            // 
            // txtGWBankCode
            // 
            this.txtGWBankCode.Location = new System.Drawing.Point(124, 44);
            this.txtGWBankCode.Name = "txtGWBankCode";
            this.txtGWBankCode.Size = new System.Drawing.Size(145, 22);
            this.txtGWBankCode.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtGWBankCode, "Press F5 to display State Code ");
            this.txtGWBankCode.TextChanged += new System.EventHandler(this.txtGWBankCode_TextChanged);
            this.txtGWBankCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGWBankCode_KeyDown);
            this.txtGWBankCode.Leave += new System.EventHandler(this.txtGWBankCode_Leave);
            // 
            // txtCitadID
            // 
            this.txtCitadID.Location = new System.Drawing.Point(124, 16);
            this.txtCitadID.Name = "txtCitadID";
            this.txtCitadID.Size = new System.Drawing.Size(145, 22);
            this.txtCitadID.TabIndex = 0;
            this.txtCitadID.TextChanged += new System.EventHandler(this.txtCitadID_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(567, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Citad status :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Area :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(567, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "LV allow :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Receiver Branch :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "State Bank ID :";
            // 
            // lblFunction
            // 
            this.lblFunction.AutoSize = true;
            this.lblFunction.Location = new System.Drawing.Point(567, 47);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new System.Drawing.Size(64, 16);
            this.lblFunction.TabIndex = 0;
            this.lblFunction.Text = "Function :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Citad ID :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mskHour);
            this.groupBox2.Controls.Add(this.cboCCYCD);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtAmount);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(849, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HV/LV parameter";
            // 
            // mskHour
            // 
            this.mskHour.Location = new System.Drawing.Point(672, 15);
            this.mskHour.Mask = "00:00";
            this.mskHour.Name = "mskHour";
            this.mskHour.Size = new System.Drawing.Size(165, 22);
            this.mskHour.TabIndex = 13;
            this.mskHour.ValidatingType = typeof(System.DateTime);
            this.mskHour.Leave += new System.EventHandler(this.mskHour_Leave);
            // 
            // cboCCYCD
            // 
            this.cboCCYCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCCYCD.FormattingEnabled = true;
            this.cboCCYCD.Location = new System.Drawing.Point(455, 18);
            this.cboCCYCD.Name = "cboCCYCD";
            this.cboCCYCD.Size = new System.Drawing.Size(77, 24);
            this.cboCCYCD.TabIndex = 12;
            this.cboCCYCD.SelectedIndexChanged += new System.EventHandler(this.cboCCYCD_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(382, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 16);
            this.label17.TabIndex = 0;
            this.label17.Text = "Currency :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(567, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Time :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Amount of LV :";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(124, 19);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(241, 22);
            this.txtAmount.TabIndex = 11;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgView);
            this.groupBox3.Controls.Add(this.cmdExportPath);
            this.groupBox3.Controls.Add(this.cmdImportPath);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.lblTotalTAD);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUser);
            this.groupBox3.Controls.Add(this.txtFTPDirectory);
            this.groupBox3.Controls.Add(this.txtExportPath);
            this.groupBox3.Controls.Add(this.txtImportPath);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(849, 341);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Directory path ";
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            this.dgView.BackgroundColor = System.Drawing.Color.White;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgView.Location = new System.Drawing.Point(9, 148);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(831, 185);
            this.dgView.TabIndex = 17;
            this.dgView.TabStop = false;
            this.dgView.DoubleClick += new System.EventHandler(this.dgView_DoubleClick);
            this.dgView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellClick);
            this.dgView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellContentClick);
            // 
            // cmdExportPath
            // 
            this.cmdExportPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdExportPath.BackgroundImage")));
            this.cmdExportPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdExportPath.Location = new System.Drawing.Point(805, 43);
            this.cmdExportPath.Name = "cmdExportPath";
            this.cmdExportPath.Size = new System.Drawing.Size(32, 24);
            this.cmdExportPath.TabIndex = 3;
            this.cmdExportPath.UseVisualStyleBackColor = true;
            this.cmdExportPath.Click += new System.EventHandler(this.cmdExportPath_Click);
            // 
            // cmdImportPath
            // 
            this.cmdImportPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdImportPath.BackgroundImage")));
            this.cmdImportPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdImportPath.Location = new System.Drawing.Point(805, 16);
            this.cmdImportPath.Name = "cmdImportPath";
            this.cmdImportPath.Size = new System.Drawing.Size(32, 24);
            this.cmdImportPath.TabIndex = 1;
            this.cmdImportPath.UseVisualStyleBackColor = true;
            this.cmdImportPath.Click += new System.EventHandler(this.cmdImportPath_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(440, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 16);
            this.label15.TabIndex = 0;
            this.label15.Text = "Password :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 129);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(138, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Total number of TAD :";
            // 
            // lblTotalTAD
            // 
            this.lblTotalTAD.AutoSize = true;
            this.lblTotalTAD.Location = new System.Drawing.Point(150, 129);
            this.lblTotalTAD.Name = "lblTotalTAD";
            this.lblTotalTAD.Size = new System.Drawing.Size(0, 16);
            this.lblTotalTAD.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "User :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(520, 101);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(317, 22);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(124, 101);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(310, 22);
            this.txtUser.TabIndex = 5;
            this.txtUser.Leave += new System.EventHandler(this.txtUser_Leave);
            // 
            // txtFTPDirectory
            // 
            this.txtFTPDirectory.Location = new System.Drawing.Point(124, 73);
            this.txtFTPDirectory.Name = "txtFTPDirectory";
            this.txtFTPDirectory.Size = new System.Drawing.Size(662, 22);
            this.txtFTPDirectory.TabIndex = 4;
            this.txtFTPDirectory.Leave += new System.EventHandler(this.txtFTPDirectory_Leave);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(124, 44);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(662, 22);
            this.txtExportPath.TabIndex = 2;
            this.txtExportPath.Leave += new System.EventHandler(this.txtExportPath_Leave);
            // 
            // txtImportPath
            // 
            this.txtImportPath.Location = new System.Drawing.Point(124, 17);
            this.txtImportPath.Name = "txtImportPath";
            this.txtImportPath.Size = new System.Drawing.Size(662, 22);
            this.txtImportPath.TabIndex = 0;
            this.txtImportPath.Leave += new System.EventHandler(this.txtImportPath_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "FTP directory :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "Export path :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "Import path :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(12, 620);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 16);
            this.label19.TabIndex = 23;
            this.label19.Text = "Area view :";
            // 
            // cboAreaView
            // 
            this.cboAreaView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAreaView.FormattingEnabled = true;
            this.cboAreaView.Location = new System.Drawing.Point(90, 619);
            this.cboAreaView.Name = "cboAreaView";
            this.cboAreaView.Size = new System.Drawing.Size(161, 21);
            this.cboAreaView.TabIndex = 0;
            this.cboAreaView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cboAreaView_MouseUp);
            this.cboAreaView.SelectedIndexChanged += new System.EventHandler(this.cboAreaView_SelectedIndexChanged);
            this.cboAreaView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cboAreaView_MouseMove);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(692, 619);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdCancel_MouseMove);
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdHistory
            // 
            this.cmdHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHistory.Location = new System.Drawing.Point(267, 620);
            this.cmdHistory.Name = "cmdHistory";
            this.cmdHistory.Size = new System.Drawing.Size(80, 30);
            this.cmdHistory.TabIndex = 24;
            this.cmdHistory.Text = "&History";
            this.cmdHistory.UseVisualStyleBackColor = true;
            this.cmdHistory.Click += new System.EventHandler(this.cmdHistory_Click);
            // 
            // frmTADInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(874, 657);
            this.Controls.Add(this.cmdHistory);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cboAreaView);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmTADInfo";
            this.Text = "IBPS TAD Information";
            this.Load += new System.EventHandler(this.frmTADInfo_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTADInfo_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.cboAreaView, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.Controls.SetChildIndex(this.cmdHistory, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkHVLVAllow;
        private System.Windows.Forms.ComboBox cboCitadStatus;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.TextBox txtSIBSBankCode;
        private System.Windows.Forms.TextBox txtGWBankCode;
        private System.Windows.Forms.TextBox txtCitadID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFunction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblTotalTAD;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtFTPDirectory;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.TextBox txtImportPath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmdExportPath;
        private System.Windows.Forms.Button cmdImportPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblSIBSBankCode;
        private System.Windows.Forms.Label lblGWBankCode;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.ComboBox cboCCYCD;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboConnection;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cboAreaView;
        private System.Windows.Forms.MaskedTextBox mskHour;
        private System.Windows.Forms.CheckBox chkCentralized;
        private System.Windows.Forms.Label lblCentralized;
        private System.Windows.Forms.ComboBox cboFunction;
        private System.Windows.Forms.TextBox txtDBLink;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtSBV_TAD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdHistory;
    }
}