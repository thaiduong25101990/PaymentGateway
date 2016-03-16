namespace BR.BRInterBank
{
    partial class frmVCBRec
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVCBRec));
            this.grReconcile = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMsgType = new System.Windows.Forms.ComboBox();
            this.pickerDate = new System.Windows.Forms.DateTimePicker();
            this.lbtype = new System.Windows.Forms.Label();
            this.cbview = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbdepartment = new System.Windows.Forms.Label();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.lbdirection = new System.Windows.Forms.Label();
            this.lbdate = new System.Windows.Forms.Label();
            this.cbdirection = new System.Windows.Forms.ComboBox();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.cmdReconcile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.datMessage = new System.Windows.Forms.DataGridView();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btClose = new System.Windows.Forms.Button();
            this.lbInformation = new System.Windows.Forms.Label();
            this.btGW = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btInfoSWIFT = new System.Windows.Forms.Button();
            this.btTR = new System.Windows.Forms.Button();
            this.btSIBS_TFInfo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSIBS_GW = new System.Windows.Forms.TextBox();
            this.cbSIBS_GW = new System.Windows.Forms.ComboBox();
            this.txtTR_GW = new System.Windows.Forms.TextBox();
            this.cbTR_GW = new System.Windows.Forms.ComboBox();
            this.cbVCB_GW = new System.Windows.Forms.ComboBox();
            this.txtVCB_GW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btGetData = new System.Windows.Forms.Button();
            this.btCheckData = new System.Windows.Forms.Button();
            this.grReconcile.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(641, 653);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(311, 645);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(225, 645);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(50, 645);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(136, 645);
            // 
            // grReconcile
            // 
            this.grReconcile.Controls.Add(this.label3);
            this.grReconcile.Controls.Add(this.cbMsgType);
            this.grReconcile.Controls.Add(this.pickerDate);
            this.grReconcile.Controls.Add(this.lbtype);
            this.grReconcile.Controls.Add(this.cbview);
            this.grReconcile.Controls.Add(this.label1);
            this.grReconcile.Controls.Add(this.lbdepartment);
            this.grReconcile.Controls.Add(this.cbdepartment);
            this.grReconcile.Controls.Add(this.lbdirection);
            this.grReconcile.Controls.Add(this.lbdate);
            this.grReconcile.Controls.Add(this.cbdirection);
            this.grReconcile.Controls.Add(this.cbtype);
            this.grReconcile.Location = new System.Drawing.Point(8, 7);
            this.grReconcile.Name = "grReconcile";
            this.grReconcile.Size = new System.Drawing.Size(598, 131);
            this.grReconcile.TabIndex = 4;
            this.grReconcile.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(329, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "Msg Type :";
            // 
            // cbMsgType
            // 
            this.cbMsgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsgType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMsgType.FormattingEnabled = true;
            this.cbMsgType.Location = new System.Drawing.Point(404, 89);
            this.cbMsgType.Name = "cbMsgType";
            this.cbMsgType.Size = new System.Drawing.Size(187, 24);
            this.cbMsgType.TabIndex = 65;
            this.cbMsgType.SelectedIndexChanged += new System.EventHandler(this.OnMsgType);
            // 
            // pickerDate
            // 
            this.pickerDate.CustomFormat = "dd/MM/yyyyy";
            this.pickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerDate.Location = new System.Drawing.Point(84, 22);
            this.pickerDate.Name = "pickerDate";
            this.pickerDate.Size = new System.Drawing.Size(202, 22);
            this.pickerDate.TabIndex = 0;
            this.pickerDate.ValueChanged += new System.EventHandler(this.OnDate);
            // 
            // lbtype
            // 
            this.lbtype.AutoSize = true;
            this.lbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtype.Location = new System.Drawing.Point(329, 60);
            this.lbtype.Name = "lbtype";
            this.lbtype.Size = new System.Drawing.Size(45, 16);
            this.lbtype.TabIndex = 63;
            this.lbtype.Text = "Type :";
            // 
            // cbview
            // 
            this.cbview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbview.FormattingEnabled = true;
            this.cbview.Location = new System.Drawing.Point(84, 89);
            this.cbview.Name = "cbview";
            this.cbview.Size = new System.Drawing.Size(202, 24);
            this.cbview.TabIndex = 5;
            this.cbview.SelectedIndexChanged += new System.EventHandler(this.OnView);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "View :";
            // 
            // lbdepartment
            // 
            this.lbdepartment.AutoSize = true;
            this.lbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdepartment.Location = new System.Drawing.Point(329, 25);
            this.lbdepartment.Name = "lbdepartment";
            this.lbdepartment.Size = new System.Drawing.Size(58, 16);
            this.lbdepartment.TabIndex = 62;
            this.lbdepartment.Text = "Module :";
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(404, 21);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(187, 24);
            this.cbdepartment.TabIndex = 2;
            // 
            // lbdirection
            // 
            this.lbdirection.AutoSize = true;
            this.lbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdirection.Location = new System.Drawing.Point(12, 60);
            this.lbdirection.Name = "lbdirection";
            this.lbdirection.Size = new System.Drawing.Size(67, 16);
            this.lbdirection.TabIndex = 61;
            this.lbdirection.Text = "Direction :";
            // 
            // lbdate
            // 
            this.lbdate.AutoSize = true;
            this.lbdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdate.Location = new System.Drawing.Point(12, 25);
            this.lbdate.Name = "lbdate";
            this.lbdate.Size = new System.Drawing.Size(43, 16);
            this.lbdate.TabIndex = 60;
            this.lbdate.Text = "Date :";
            // 
            // cbdirection
            // 
            this.cbdirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdirection.FormattingEnabled = true;
            this.cbdirection.Location = new System.Drawing.Point(84, 56);
            this.cbdirection.Name = "cbdirection";
            this.cbdirection.Size = new System.Drawing.Size(202, 24);
            this.cbdirection.TabIndex = 1;
            this.cbdirection.SelectedIndexChanged += new System.EventHandler(this.OnDirection);
            // 
            // cbtype
            // 
            this.cbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtype.FormattingEnabled = true;
            this.cbtype.Location = new System.Drawing.Point(404, 56);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(187, 24);
            this.cbtype.TabIndex = 3;
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.OnType);
            // 
            // btSearch
            // 
            this.btSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSearch.Location = new System.Drawing.Point(655, 18);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(100, 30);
            this.btSearch.TabIndex = 64;
            this.btSearch.Text = "&Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // cmdReconcile
            // 
            this.cmdReconcile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdReconcile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReconcile.Location = new System.Drawing.Point(655, 88);
            this.cmdReconcile.Name = "cmdReconcile";
            this.cmdReconcile.Size = new System.Drawing.Size(100, 30);
            this.cmdReconcile.TabIndex = 4;
            this.cmdReconcile.Text = "&Reconcile";
            this.cmdReconcile.UseVisualStyleBackColor = true;
            this.cmdReconcile.Click += new System.EventHandler(this.cmdReconcile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.datMessage);
            this.groupBox2.Location = new System.Drawing.Point(10, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(744, 408);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // datMessage
            // 
            this.datMessage.AllowUserToAddRows = false;
            this.datMessage.AllowUserToDeleteRows = false;
            this.datMessage.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datMessage.ColumnHeadersHeight = 30;
            this.datMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.datMessage.EnableHeadersVisualStyles = false;
            this.datMessage.Location = new System.Drawing.Point(4, 14);
            this.datMessage.Name = "datMessage";
            this.datMessage.ReadOnly = true;
            this.datMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datMessage.Size = new System.Drawing.Size(734, 388);
            this.datMessage.TabIndex = 0;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(555, 609);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 6;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimer);
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.Location = new System.Drawing.Point(641, 609);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(80, 30);
            this.btClose.TabIndex = 8;
            this.btClose.Text = "&Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.OnClose);
            // 
            // lbInformation
            // 
            this.lbInformation.AutoSize = true;
            this.lbInformation.Location = new System.Drawing.Point(12, 616);
            this.lbInformation.Name = "lbInformation";
            this.lbInformation.Size = new System.Drawing.Size(0, 16);
            this.lbInformation.TabIndex = 9;
            // 
            // btGW
            // 
            this.btGW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btGW.BackgroundImage")));
            this.btGW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btGW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btGW.Location = new System.Drawing.Point(314, 147);
            this.btGW.Name = "btGW";
            this.btGW.Size = new System.Drawing.Size(25, 20);
            this.btGW.TabIndex = 121;
            this.btGW.UseVisualStyleBackColor = true;
            this.btGW.Click += new System.EventHandler(this.OnGW);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(252, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 120;
            this.label4.Text = "SIBS";
            // 
            // btInfoSWIFT
            // 
            this.btInfoSWIFT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoSWIFT.BackgroundImage")));
            this.btInfoSWIFT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoSWIFT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoSWIFT.Location = new System.Drawing.Point(405, 144);
            this.btInfoSWIFT.Name = "btInfoSWIFT";
            this.btInfoSWIFT.Size = new System.Drawing.Size(25, 20);
            this.btInfoSWIFT.TabIndex = 119;
            this.btInfoSWIFT.UseVisualStyleBackColor = true;
            this.btInfoSWIFT.Click += new System.EventHandler(this.OnVCB);
            // 
            // btTR
            // 
            this.btTR.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btTR.BackgroundImage")));
            this.btTR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btTR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btTR.Location = new System.Drawing.Point(20, 170);
            this.btTR.Name = "btTR";
            this.btTR.Size = new System.Drawing.Size(25, 20);
            this.btTR.TabIndex = 118;
            this.btTR.UseVisualStyleBackColor = true;
            this.btTR.Click += new System.EventHandler(this.OnTR);
            // 
            // btSIBS_TFInfo
            // 
            this.btSIBS_TFInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSIBS_TFInfo.BackgroundImage")));
            this.btSIBS_TFInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSIBS_TFInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSIBS_TFInfo.Location = new System.Drawing.Point(21, 144);
            this.btSIBS_TFInfo.Name = "btSIBS_TFInfo";
            this.btSIBS_TFInfo.Size = new System.Drawing.Size(25, 20);
            this.btSIBS_TFInfo.TabIndex = 117;
            this.btSIBS_TFInfo.UseVisualStyleBackColor = true;
            this.btSIBS_TFInfo.Click += new System.EventHandler(this.OnSIBS);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(354, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "VCB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(252, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "TR";
            this.label5.Visible = false;
            // 
            // txtSIBS_GW
            // 
            this.txtSIBS_GW.BackColor = System.Drawing.Color.White;
            this.txtSIBS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSIBS_GW.Location = new System.Drawing.Point(137, 144);
            this.txtSIBS_GW.Name = "txtSIBS_GW";
            this.txtSIBS_GW.ReadOnly = true;
            this.txtSIBS_GW.Size = new System.Drawing.Size(108, 21);
            this.txtSIBS_GW.TabIndex = 108;
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
            this.cbSIBS_GW.Location = new System.Drawing.Point(52, 144);
            this.cbSIBS_GW.Name = "cbSIBS_GW";
            this.cbSIBS_GW.Size = new System.Drawing.Size(82, 20);
            this.cbSIBS_GW.TabIndex = 109;
            // 
            // txtTR_GW
            // 
            this.txtTR_GW.BackColor = System.Drawing.Color.White;
            this.txtTR_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTR_GW.Location = new System.Drawing.Point(136, 170);
            this.txtTR_GW.Name = "txtTR_GW";
            this.txtTR_GW.ReadOnly = true;
            this.txtTR_GW.Size = new System.Drawing.Size(108, 21);
            this.txtTR_GW.TabIndex = 112;
            // 
            // cbTR_GW
            // 
            this.cbTR_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbTR_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTR_GW.FormattingEnabled = true;
            this.cbTR_GW.Items.AddRange(new object[] {
            "TR->BR"});
            this.cbTR_GW.Location = new System.Drawing.Point(51, 170);
            this.cbTR_GW.Name = "cbTR_GW";
            this.cbTR_GW.Size = new System.Drawing.Size(82, 20);
            this.cbTR_GW.TabIndex = 113;
            // 
            // cbVCB_GW
            // 
            this.cbVCB_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbVCB_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVCB_GW.FormattingEnabled = true;
            this.cbVCB_GW.Items.AddRange(new object[] {
            "BR->VCB",
            "BR<-VCB"});
            this.cbVCB_GW.Location = new System.Drawing.Point(437, 144);
            this.cbVCB_GW.Name = "cbVCB_GW";
            this.cbVCB_GW.Size = new System.Drawing.Size(82, 20);
            this.cbVCB_GW.TabIndex = 111;
            // 
            // txtVCB_GW
            // 
            this.txtVCB_GW.BackColor = System.Drawing.Color.White;
            this.txtVCB_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVCB_GW.Location = new System.Drawing.Point(525, 144);
            this.txtVCB_GW.Name = "txtVCB_GW";
            this.txtVCB_GW.ReadOnly = true;
            this.txtVCB_GW.Size = new System.Drawing.Size(108, 21);
            this.txtVCB_GW.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(311, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 114;
            this.label2.Text = "BR";
            // 
            // btGetData
            // 
            this.btGetData.BackColor = System.Drawing.SystemColors.Control;
            this.btGetData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetData.Location = new System.Drawing.Point(655, 53);
            this.btGetData.Name = "btGetData";
            this.btGetData.Size = new System.Drawing.Size(100, 30);
            this.btGetData.TabIndex = 122;
            this.btGetData.Text = "GetData";
            this.btGetData.UseVisualStyleBackColor = false;
            this.btGetData.Click += new System.EventHandler(this.OnGetData);
            // 
            // btCheckData
            // 
            this.btCheckData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCheckData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCheckData.Location = new System.Drawing.Point(655, 126);
            this.btCheckData.Name = "btCheckData";
            this.btCheckData.Size = new System.Drawing.Size(100, 30);
            this.btCheckData.TabIndex = 123;
            this.btCheckData.Text = "CheckData";
            this.btCheckData.UseVisualStyleBackColor = true;
            this.btCheckData.Click += new System.EventHandler(this.OnCheckData);
            // 
            // frmVCBRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(764, 642);
            this.Controls.Add(this.lbInformation);
            this.Controls.Add(this.btGetData);
            this.Controls.Add(this.btCheckData);
            this.Controls.Add(this.btGW);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btInfoSWIFT);
            this.Controls.Add(this.btTR);
            this.Controls.Add(this.btSIBS_TFInfo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSIBS_GW);
            this.Controls.Add(this.cbSIBS_GW);
            this.Controls.Add(this.txtTR_GW);
            this.Controls.Add(this.cbTR_GW);
            this.Controls.Add(this.cbVCB_GW);
            this.Controls.Add(this.txtVCB_GW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.cmdReconcile);
            this.Controls.Add(this.grReconcile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdPrint);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmVCBRec";
            this.Text = "VCB Reconcile";
            this.Load += new System.EventHandler(this.frmVCBRec_Load);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grReconcile, 0);
            this.Controls.SetChildIndex(this.cmdReconcile, 0);
            this.Controls.SetChildIndex(this.btSearch, 0);
            this.Controls.SetChildIndex(this.btClose, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtVCB_GW, 0);
            this.Controls.SetChildIndex(this.cbVCB_GW, 0);
            this.Controls.SetChildIndex(this.cbTR_GW, 0);
            this.Controls.SetChildIndex(this.txtTR_GW, 0);
            this.Controls.SetChildIndex(this.cbSIBS_GW, 0);
            this.Controls.SetChildIndex(this.txtSIBS_GW, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.btSIBS_TFInfo, 0);
            this.Controls.SetChildIndex(this.btTR, 0);
            this.Controls.SetChildIndex(this.btInfoSWIFT, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.btGW, 0);
            this.Controls.SetChildIndex(this.btCheckData, 0);
            this.Controls.SetChildIndex(this.btGetData, 0);
            this.Controls.SetChildIndex(this.lbInformation, 0);
            this.grReconcile.ResumeLayout(false);
            this.grReconcile.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grReconcile;
        private System.Windows.Forms.Button cmdReconcile;
        private System.Windows.Forms.Label lbtype;
        private System.Windows.Forms.Label lbdepartment;
        private System.Windows.Forms.Label lbdirection;
        private System.Windows.Forms.Label lbdate;
        private System.Windows.Forms.ComboBox cbdirection;
        private System.Windows.Forms.ComboBox cbtype;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbview;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.DateTimePicker pickerDate;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMsgType;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView datMessage;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lbInformation;
        private System.Windows.Forms.Button btGW;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btInfoSWIFT;
        private System.Windows.Forms.Button btTR;
        private System.Windows.Forms.Button btSIBS_TFInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSIBS_GW;
        private System.Windows.Forms.ComboBox cbSIBS_GW;
        private System.Windows.Forms.TextBox txtTR_GW;
        private System.Windows.Forms.ComboBox cbTR_GW;
        private System.Windows.Forms.ComboBox cbVCB_GW;
        private System.Windows.Forms.TextBox txtVCB_GW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btGetData;
        private System.Windows.Forms.Button btCheckData;
    }
}