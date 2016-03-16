namespace BR.BRTTSB
{
    partial class frmTTSPRec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTTSPRec));
            this.cmdReconcile = new System.Windows.Forms.Button();
            this.lbtype = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbInformation = new System.Windows.Forms.Label();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.datMessage = new System.Windows.Forms.DataGridView();
            this.cbview = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbdepartment = new System.Windows.Forms.Label();
            this.lbdirection = new System.Windows.Forms.Label();
            this.lbdate = new System.Windows.Forms.Label();
            this.grReconcile = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMsgType = new System.Windows.Forms.ComboBox();
            this.pickerDate = new System.Windows.Forms.DateTimePicker();
            this.cbdirection = new System.Windows.Forms.ComboBox();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSIBS_GW = new System.Windows.Forms.TextBox();
            this.cbSIBS_GW = new System.Windows.Forms.ComboBox();
            this.txtIQS_GW = new System.Windows.Forms.TextBox();
            this.cbIQS_GW = new System.Windows.Forms.ComboBox();
            this.cbTTSP_GW = new System.Windows.Forms.ComboBox();
            this.txtTTSP_GW = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btGetData = new System.Windows.Forms.Button();
            this.btCheckData = new System.Windows.Forms.Button();
            this.btInfoTTSP = new System.Windows.Forms.Button();
            this.btInfoIQS = new System.Windows.Forms.Button();
            this.btInfoSIBS = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).BeginInit();
            this.grReconcile.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(716, 565);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(519, 679);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(404, 679);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(171, 679);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(285, 679);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdReconcile
            // 
            this.cmdReconcile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdReconcile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReconcile.Location = new System.Drawing.Point(705, 48);
            this.cmdReconcile.Margin = new System.Windows.Forms.Padding(4);
            this.cmdReconcile.Name = "cmdReconcile";
            this.cmdReconcile.Size = new System.Drawing.Size(100, 30);
            this.cmdReconcile.TabIndex = 4;
            this.cmdReconcile.Text = "&Reconcile";
            this.cmdReconcile.UseVisualStyleBackColor = true;
            this.cmdReconcile.Click += new System.EventHandler(this.cmdReconcile_Click);
            // 
            // lbtype
            // 
            this.lbtype.AutoSize = true;
            this.lbtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtype.Location = new System.Drawing.Point(339, 56);
            this.lbtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbtype.Name = "lbtype";
            this.lbtype.Size = new System.Drawing.Size(46, 16);
            this.lbtype.TabIndex = 63;
            this.lbtype.Text = "Type :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbInformation);
            this.groupBox2.Controls.Add(this.cmdPrint);
            this.groupBox2.Controls.Add(this.datMessage);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 223);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(797, 381);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // lbInformation
            // 
            this.lbInformation.AutoSize = true;
            this.lbInformation.Location = new System.Drawing.Point(7, 349);
            this.lbInformation.Name = "lbInformation";
            this.lbInformation.Size = new System.Drawing.Size(0, 13);
            this.lbInformation.TabIndex = 68;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(615, 342);
            this.cmdPrint.Margin = new System.Windows.Forms.Padding(4);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 6;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // datMessage
            // 
            this.datMessage.AllowUserToAddRows = false;
            this.datMessage.AllowUserToDeleteRows = false;
            this.datMessage.AllowUserToResizeRows = false;
            this.datMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.datMessage.BackgroundColor = System.Drawing.Color.White;
            this.datMessage.Location = new System.Drawing.Point(8, 13);
            this.datMessage.Margin = new System.Windows.Forms.Padding(4);
            this.datMessage.Name = "datMessage";
            this.datMessage.ReadOnly = true;
            this.datMessage.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.datMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datMessage.Size = new System.Drawing.Size(779, 324);
            this.datMessage.TabIndex = 67;
            // 
            // cbview
            // 
            this.cbview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbview.FormattingEnabled = true;
            this.cbview.Location = new System.Drawing.Point(112, 88);
            this.cbview.Margin = new System.Windows.Forms.Padding(4);
            this.cbview.Name = "cbview";
            this.cbview.Size = new System.Drawing.Size(192, 24);
            this.cbview.TabIndex = 5;
            this.cbview.SelectedIndexChanged += new System.EventHandler(this.OnView);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "View :";
            // 
            // lbdepartment
            // 
            this.lbdepartment.AutoSize = true;
            this.lbdepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdepartment.Location = new System.Drawing.Point(339, 22);
            this.lbdepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdepartment.Name = "lbdepartment";
            this.lbdepartment.Size = new System.Drawing.Size(59, 16);
            this.lbdepartment.TabIndex = 62;
            this.lbdepartment.Text = "Module :";
            // 
            // lbdirection
            // 
            this.lbdirection.AutoSize = true;
            this.lbdirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdirection.Location = new System.Drawing.Point(17, 59);
            this.lbdirection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdirection.Name = "lbdirection";
            this.lbdirection.Size = new System.Drawing.Size(67, 16);
            this.lbdirection.TabIndex = 61;
            this.lbdirection.Text = "Direction :";
            // 
            // lbdate
            // 
            this.lbdate.AutoSize = true;
            this.lbdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdate.Location = new System.Drawing.Point(17, 28);
            this.lbdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdate.Name = "lbdate";
            this.lbdate.Size = new System.Drawing.Size(43, 16);
            this.lbdate.TabIndex = 60;
            this.lbdate.Text = "Date :";
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
            this.grReconcile.Controls.Add(this.lbdirection);
            this.grReconcile.Controls.Add(this.lbdate);
            this.grReconcile.Controls.Add(this.cbdirection);
            this.grReconcile.Controls.Add(this.cbtype);
            this.grReconcile.Controls.Add(this.cbdepartment);
            this.grReconcile.Location = new System.Drawing.Point(13, 9);
            this.grReconcile.Margin = new System.Windows.Forms.Padding(4);
            this.grReconcile.Name = "grReconcile";
            this.grReconcile.Padding = new System.Windows.Forms.Padding(4);
            this.grReconcile.Size = new System.Drawing.Size(680, 130);
            this.grReconcile.TabIndex = 7;
            this.grReconcile.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(339, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "Msg Type :";
            // 
            // cbMsgType
            // 
            this.cbMsgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsgType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMsgType.FormattingEnabled = true;
            this.cbMsgType.Location = new System.Drawing.Point(441, 91);
            this.cbMsgType.Margin = new System.Windows.Forms.Padding(4);
            this.cbMsgType.Name = "cbMsgType";
            this.cbMsgType.Size = new System.Drawing.Size(233, 24);
            this.cbMsgType.TabIndex = 65;
            this.cbMsgType.SelectedIndexChanged += new System.EventHandler(this.OnMsgType);
            // 
            // pickerDate
            // 
            this.pickerDate.CustomFormat = "dd/MM/yyyyy";
            this.pickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerDate.Location = new System.Drawing.Point(112, 22);
            this.pickerDate.Name = "pickerDate";
            this.pickerDate.Size = new System.Drawing.Size(192, 22);
            this.pickerDate.TabIndex = 0;
            this.pickerDate.ValueChanged += new System.EventHandler(this.OnDate);
            // 
            // cbdirection
            // 
            this.cbdirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdirection.FormattingEnabled = true;
            this.cbdirection.Location = new System.Drawing.Point(112, 56);
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
            this.cbtype.Location = new System.Drawing.Point(441, 54);
            this.cbtype.Margin = new System.Windows.Forms.Padding(4);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(233, 24);
            this.cbtype.TabIndex = 3;
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.cbtype_SelectedIndexChanged);
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(441, 22);
            this.cbdepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(233, 24);
            this.cbdepartment.TabIndex = 2;
            this.cbdepartment.SelectedIndexChanged += new System.EventHandler(this.OnDepartment);
            // 
            // btSearch
            // 
            this.btSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSearch.Location = new System.Drawing.Point(705, 86);
            this.btSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(100, 30);
            this.btSearch.TabIndex = 64;
            this.btSearch.Text = "&Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimer);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(387, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 96;
            this.label6.Text = "TTSP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(268, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 95;
            this.label5.Text = "IQS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(266, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 94;
            this.label2.Text = "SIBS";
            // 
            // txtSIBS_GW
            // 
            this.txtSIBS_GW.BackColor = System.Drawing.Color.White;
            this.txtSIBS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSIBS_GW.Location = new System.Drawing.Point(152, 155);
            this.txtSIBS_GW.Name = "txtSIBS_GW";
            this.txtSIBS_GW.ReadOnly = true;
            this.txtSIBS_GW.Size = new System.Drawing.Size(108, 20);
            this.txtSIBS_GW.TabIndex = 87;
            // 
            // cbSIBS_GW
            // 
            this.cbSIBS_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSIBS_GW.DropDownWidth = 80;
            this.cbSIBS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSIBS_GW.FormattingEnabled = true;
            this.cbSIBS_GW.Items.AddRange(new object[] {
            "SIBS->BR",
            "SIBS<-BR"});
            this.cbSIBS_GW.Location = new System.Drawing.Point(67, 155);
            this.cbSIBS_GW.Name = "cbSIBS_GW";
            this.cbSIBS_GW.Size = new System.Drawing.Size(82, 20);
            this.cbSIBS_GW.TabIndex = 88;
            // 
            // txtIQS_GW
            // 
            this.txtIQS_GW.BackColor = System.Drawing.Color.White;
            this.txtIQS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIQS_GW.Location = new System.Drawing.Point(151, 196);
            this.txtIQS_GW.Name = "txtIQS_GW";
            this.txtIQS_GW.ReadOnly = true;
            this.txtIQS_GW.Size = new System.Drawing.Size(108, 20);
            this.txtIQS_GW.TabIndex = 91;
            // 
            // cbIQS_GW
            // 
            this.cbIQS_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbIQS_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIQS_GW.FormattingEnabled = true;
            this.cbIQS_GW.Items.AddRange(new object[] {
            "IQS->BR",
            "IQS<-BR"});
            this.cbIQS_GW.Location = new System.Drawing.Point(66, 196);
            this.cbIQS_GW.Name = "cbIQS_GW";
            this.cbIQS_GW.Size = new System.Drawing.Size(82, 20);
            this.cbIQS_GW.TabIndex = 92;
            // 
            // cbTTSP_GW
            // 
            this.cbTTSP_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbTTSP_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTTSP_GW.FormattingEnabled = true;
            this.cbTTSP_GW.Items.AddRange(new object[] {
            "BR->TTSP",
            "BR<-TTSP"});
            this.cbTTSP_GW.Location = new System.Drawing.Point(467, 176);
            this.cbTTSP_GW.Name = "cbTTSP_GW";
            this.cbTTSP_GW.Size = new System.Drawing.Size(82, 20);
            this.cbTTSP_GW.TabIndex = 90;
            // 
            // txtTTSP_GW
            // 
            this.txtTTSP_GW.BackColor = System.Drawing.Color.White;
            this.txtTTSP_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTTSP_GW.Location = new System.Drawing.Point(555, 176);
            this.txtTTSP_GW.Name = "txtTTSP_GW";
            this.txtTTSP_GW.ReadOnly = true;
            this.txtTTSP_GW.Size = new System.Drawing.Size(108, 20);
            this.txtTTSP_GW.TabIndex = 89;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(330, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 93;
            this.label4.Text = "BR";
            // 
            // btGetData
            // 
            this.btGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btGetData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetData.Location = new System.Drawing.Point(705, 159);
            this.btGetData.Name = "btGetData";
            this.btGetData.Size = new System.Drawing.Size(100, 30);
            this.btGetData.TabIndex = 98;
            this.btGetData.Text = "GetData";
            this.btGetData.UseVisualStyleBackColor = false;
            this.btGetData.Click += new System.EventHandler(this.OnGetData);
            // 
            // btCheckData
            // 
            this.btCheckData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCheckData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCheckData.Location = new System.Drawing.Point(705, 123);
            this.btCheckData.Name = "btCheckData";
            this.btCheckData.Size = new System.Drawing.Size(100, 30);
            this.btCheckData.TabIndex = 99;
            this.btCheckData.Text = "CheckData";
            this.btCheckData.UseVisualStyleBackColor = true;
            this.btCheckData.Click += new System.EventHandler(this.OnCheckData);
            // 
            // btInfoTTSP
            // 
            this.btInfoTTSP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoTTSP.BackgroundImage")));
            this.btInfoTTSP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoTTSP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoTTSP.Location = new System.Drawing.Point(436, 175);
            this.btInfoTTSP.Name = "btInfoTTSP";
            this.btInfoTTSP.Size = new System.Drawing.Size(25, 20);
            this.btInfoTTSP.TabIndex = 107;
            this.btInfoTTSP.UseVisualStyleBackColor = true;
            this.btInfoTTSP.Click += new System.EventHandler(this.btInfoTTSP_Click);
            // 
            // btInfoIQS
            // 
            this.btInfoIQS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoIQS.BackgroundImage")));
            this.btInfoIQS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoIQS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoIQS.Location = new System.Drawing.Point(29, 195);
            this.btInfoIQS.Name = "btInfoIQS";
            this.btInfoIQS.Size = new System.Drawing.Size(25, 20);
            this.btInfoIQS.TabIndex = 106;
            this.btInfoIQS.UseVisualStyleBackColor = true;
            this.btInfoIQS.Click += new System.EventHandler(this.OnTR);
            // 
            // btInfoSIBS
            // 
            this.btInfoSIBS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoSIBS.BackgroundImage")));
            this.btInfoSIBS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoSIBS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoSIBS.Location = new System.Drawing.Point(30, 154);
            this.btInfoSIBS.Name = "btInfoSIBS";
            this.btInfoSIBS.Size = new System.Drawing.Size(25, 20);
            this.btInfoSIBS.TabIndex = 105;
            this.btInfoSIBS.UseVisualStyleBackColor = true;
            this.btInfoSIBS.Click += new System.EventHandler(this.OnSIBS);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(333, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 20);
            this.button1.TabIndex = 108;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnGW);
            // 
            // frmTTSPRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(818, 610);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btInfoTTSP);
            this.Controls.Add(this.btInfoIQS);
            this.Controls.Add(this.btInfoSIBS);
            this.Controls.Add(this.btGetData);
            this.Controls.Add(this.btCheckData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSIBS_GW);
            this.Controls.Add(this.cbSIBS_GW);
            this.Controls.Add(this.txtIQS_GW);
            this.Controls.Add(this.cbIQS_GW);
            this.Controls.Add(this.cbTTSP_GW);
            this.Controls.Add(this.txtTTSP_GW);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grReconcile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.cmdReconcile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTTSPRec";
            this.Text = "TTSP Reconcile";
            this.Load += new System.EventHandler(this.frmTTSPRec_Load);
            this.Controls.SetChildIndex(this.cmdReconcile, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.btSearch, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.grReconcile, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtTTSP_GW, 0);
            this.Controls.SetChildIndex(this.cbTTSP_GW, 0);
            this.Controls.SetChildIndex(this.cbIQS_GW, 0);
            this.Controls.SetChildIndex(this.txtIQS_GW, 0);
            this.Controls.SetChildIndex(this.cbSIBS_GW, 0);
            this.Controls.SetChildIndex(this.txtSIBS_GW, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.btCheckData, 0);
            this.Controls.SetChildIndex(this.btGetData, 0);
            this.Controls.SetChildIndex(this.btInfoSIBS, 0);
            this.Controls.SetChildIndex(this.btInfoIQS, 0);
            this.Controls.SetChildIndex(this.btInfoTTSP, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).EndInit();
            this.grReconcile.ResumeLayout(false);
            this.grReconcile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdReconcile;
        private System.Windows.Forms.Label lbtype;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView datMessage;
        private System.Windows.Forms.ComboBox cbview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbdepartment;
        private System.Windows.Forms.Label lbdirection;
        private System.Windows.Forms.Label lbdate;
        private System.Windows.Forms.GroupBox grReconcile;
        private System.Windows.Forms.ComboBox cbdirection;
        private System.Windows.Forms.ComboBox cbtype;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.DateTimePicker pickerDate;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMsgType;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSIBS_GW;
        private System.Windows.Forms.ComboBox cbSIBS_GW;
        private System.Windows.Forms.TextBox txtIQS_GW;
        private System.Windows.Forms.ComboBox cbIQS_GW;
        private System.Windows.Forms.ComboBox cbTTSP_GW;
        private System.Windows.Forms.TextBox txtTTSP_GW;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btGetData;
        private System.Windows.Forms.Button btCheckData;
        private System.Windows.Forms.Label lbInformation;
        private System.Windows.Forms.Button btInfoTTSP;
        private System.Windows.Forms.Button btInfoIQS;
        private System.Windows.Forms.Button btInfoSIBS;
        private System.Windows.Forms.Button button1;
    }
}