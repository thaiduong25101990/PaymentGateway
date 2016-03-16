namespace BR.BRSWIFT
{
    partial class frmSwiftRec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSwiftRec));
            this.cmdReconcile = new System.Windows.Forms.Button();
            this.lbtype = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbInformation = new System.Windows.Forms.Label();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.lbltotal = new System.Windows.Forms.Label();
            this.datMessage = new System.Windows.Forms.DataGridView();
            this.cbview = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbdepartment = new System.Windows.Forms.Label();
            this.lbdirection = new System.Windows.Forms.Label();
            this.lbdate = new System.Windows.Forms.Label();
            this.grReconcile = new System.Windows.Forms.GroupBox();
            this.pickerDate = new System.Windows.Forms.DateTimePicker();
            this.cbdirection = new System.Windows.Forms.ComboBox();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSIBS_TF = new System.Windows.Forms.TextBox();
            this.cbSIBSTF_GW = new System.Windows.Forms.ComboBox();
            this.txtTR_GW = new System.Windows.Forms.TextBox();
            this.cbTR_GW = new System.Windows.Forms.ComboBox();
            this.cbSWIFT_GW = new System.Windows.Forms.ComboBox();
            this.txtSWIFT_GW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btGetData = new System.Windows.Forms.Button();
            this.btCheckData = new System.Windows.Forms.Button();
            this.btSIBS_TFInfo = new System.Windows.Forms.Button();
            this.btInfoTR = new System.Windows.Forms.Button();
            this.btInfoSWIFT = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btSIBS_RMInfo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSIBS_RM = new System.Windows.Forms.TextBox();
            this.cbSIBSRM_GW = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btGW = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).BeginInit();
            this.grReconcile.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(690, 588);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(516, 645);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(401, 645);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(168, 645);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(283, 645);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdReconcile
            // 
            this.cmdReconcile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdReconcile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReconcile.Location = new System.Drawing.Point(686, 185);
            this.cmdReconcile.Margin = new System.Windows.Forms.Padding(4);
            this.cmdReconcile.Name = "cmdReconcile";
            this.cmdReconcile.Size = new System.Drawing.Size(102, 30);
            this.cmdReconcile.TabIndex = 4;
            this.cmdReconcile.Text = "&Reconcile";
            this.cmdReconcile.UseVisualStyleBackColor = true;
            this.cmdReconcile.Click += new System.EventHandler(this.cmdReconcile_Click);
            // 
            // lbtype
            // 
            this.lbtype.AutoSize = true;
            this.lbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtype.Location = new System.Drawing.Point(337, 58);
            this.lbtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbtype.Name = "lbtype";
            this.lbtype.Size = new System.Drawing.Size(45, 16);
            this.lbtype.TabIndex = 63;
            this.lbtype.Text = "Type :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbInformation);
            this.groupBox2.Controls.Add(this.cmdPrint);
            this.groupBox2.Controls.Add(this.lbltotal);
            this.groupBox2.Controls.Add(this.datMessage);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 269);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(783, 358);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // lbInformation
            // 
            this.lbInformation.AutoSize = true;
            this.lbInformation.Location = new System.Drawing.Point(7, 326);
            this.lbInformation.Name = "lbInformation";
            this.lbInformation.Size = new System.Drawing.Size(0, 13);
            this.lbInformation.TabIndex = 68;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(594, 319);
            this.cmdPrint.Margin = new System.Windows.Forms.Padding(4);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 6;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click_1);
            // 
            // lbltotal
            // 
            this.lbltotal.AutoSize = true;
            this.lbltotal.Location = new System.Drawing.Point(16, 357);
            this.lbltotal.Name = "lbltotal";
            this.lbltotal.Size = new System.Drawing.Size(0, 13);
            this.lbltotal.TabIndex = 11;
            // 
            // datMessage
            // 
            this.datMessage.AllowUserToAddRows = false;
            this.datMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.datMessage.BackgroundColor = System.Drawing.Color.White;
            this.datMessage.Location = new System.Drawing.Point(8, 13);
            this.datMessage.Margin = new System.Windows.Forms.Padding(4);
            this.datMessage.Name = "datMessage";
            this.datMessage.ReadOnly = true;
            this.datMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datMessage.Size = new System.Drawing.Size(767, 298);
            this.datMessage.TabIndex = 67;
            // 
            // cbview
            // 
            this.cbview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbview.FormattingEnabled = true;
            this.cbview.Location = new System.Drawing.Point(112, 93);
            this.cbview.Margin = new System.Windows.Forms.Padding(4);
            this.cbview.Name = "cbview";
            this.cbview.Size = new System.Drawing.Size(192, 24);
            this.cbview.TabIndex = 5;
            this.cbview.SelectedIndexChanged += new System.EventHandler(this.cbview_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "View :";
            // 
            // lbdepartment
            // 
            this.lbdepartment.AutoSize = true;
            this.lbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdepartment.Location = new System.Drawing.Point(337, 27);
            this.lbdepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdepartment.Name = "lbdepartment";
            this.lbdepartment.Size = new System.Drawing.Size(58, 16);
            this.lbdepartment.TabIndex = 62;
            this.lbdepartment.Text = "Module :";
            // 
            // lbdirection
            // 
            this.lbdirection.AutoSize = true;
            this.lbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdirection.Location = new System.Drawing.Point(16, 58);
            this.lbdirection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdirection.Name = "lbdirection";
            this.lbdirection.Size = new System.Drawing.Size(67, 16);
            this.lbdirection.TabIndex = 61;
            this.lbdirection.Text = "Direction :";
            // 
            // lbdate
            // 
            this.lbdate.AutoSize = true;
            this.lbdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdate.Location = new System.Drawing.Point(16, 25);
            this.lbdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbdate.Name = "lbdate";
            this.lbdate.Size = new System.Drawing.Size(43, 16);
            this.lbdate.TabIndex = 60;
            this.lbdate.Text = "Date :";
            // 
            // grReconcile
            // 
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
            this.grReconcile.Location = new System.Drawing.Point(8, 1);
            this.grReconcile.Margin = new System.Windows.Forms.Padding(4);
            this.grReconcile.Name = "grReconcile";
            this.grReconcile.Padding = new System.Windows.Forms.Padding(4);
            this.grReconcile.Size = new System.Drawing.Size(660, 125);
            this.grReconcile.TabIndex = 7;
            this.grReconcile.TabStop = false;
            // 
            // pickerDate
            // 
            this.pickerDate.CustomFormat = "dd/MM/yyyyy";
            this.pickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerDate.Location = new System.Drawing.Point(112, 27);
            this.pickerDate.Name = "pickerDate";
            this.pickerDate.Size = new System.Drawing.Size(192, 22);
            this.pickerDate.TabIndex = 0;
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
            this.cbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtype.FormattingEnabled = true;
            this.cbtype.Location = new System.Drawing.Point(419, 58);
            this.cbtype.Margin = new System.Windows.Forms.Padding(4);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(233, 24);
            this.cbtype.TabIndex = 3;
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.OnChangeType);
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(419, 25);
            this.cbdepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(233, 24);
            this.cbdepartment.TabIndex = 2;
            this.cbdepartment.SelectedIndexChanged += new System.EventHandler(this.OnChangeDP);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(685, 83);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 68;
            this.button1.Text = "&Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnSearch);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(376, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 98;
            this.label6.Text = "SWIFT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(289, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 97;
            this.label5.Text = "TR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 96;
            this.label3.Text = "RM";
            // 
            // txtSIBS_TF
            // 
            this.txtSIBS_TF.BackColor = System.Drawing.Color.White;
            this.txtSIBS_TF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSIBS_TF.Location = new System.Drawing.Point(158, 191);
            this.txtSIBS_TF.Name = "txtSIBS_TF";
            this.txtSIBS_TF.ReadOnly = true;
            this.txtSIBS_TF.Size = new System.Drawing.Size(128, 21);
            this.txtSIBS_TF.TabIndex = 87;
            // 
            // cbSIBSTF_GW
            // 
            this.cbSIBSTF_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSIBSTF_GW.DropDownWidth = 80;
            this.cbSIBSTF_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSIBSTF_GW.FormattingEnabled = true;
            this.cbSIBSTF_GW.Items.AddRange(new object[] {
            "SIBS.TF->BR",
            "SIBS.TF<-BR"});
            this.cbSIBSTF_GW.Location = new System.Drawing.Point(73, 191);
            this.cbSIBSTF_GW.Name = "cbSIBSTF_GW";
            this.cbSIBSTF_GW.Size = new System.Drawing.Size(82, 20);
            this.cbSIBSTF_GW.TabIndex = 88;
            // 
            // txtTR_GW
            // 
            this.txtTR_GW.BackColor = System.Drawing.Color.White;
            this.txtTR_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTR_GW.Location = new System.Drawing.Point(158, 225);
            this.txtTR_GW.Name = "txtTR_GW";
            this.txtTR_GW.ReadOnly = true;
            this.txtTR_GW.Size = new System.Drawing.Size(128, 21);
            this.txtTR_GW.TabIndex = 91;
            // 
            // cbTR_GW
            // 
            this.cbTR_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbTR_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTR_GW.FormattingEnabled = true;
            this.cbTR_GW.Items.AddRange(new object[] {
            "TR->BR",
            "TR<-BR"});
            this.cbTR_GW.Location = new System.Drawing.Point(73, 225);
            this.cbTR_GW.Name = "cbTR_GW";
            this.cbTR_GW.Size = new System.Drawing.Size(82, 20);
            this.cbTR_GW.TabIndex = 92;
            // 
            // cbSWIFT_GW
            // 
            this.cbSWIFT_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSWIFT_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSWIFT_GW.FormattingEnabled = true;
            this.cbSWIFT_GW.Items.AddRange(new object[] {
            "BR->SWIFT",
            "BR<-SWIFT"});
            this.cbSWIFT_GW.Location = new System.Drawing.Point(459, 191);
            this.cbSWIFT_GW.Name = "cbSWIFT_GW";
            this.cbSWIFT_GW.Size = new System.Drawing.Size(82, 20);
            this.cbSWIFT_GW.TabIndex = 90;
            // 
            // txtSWIFT_GW
            // 
            this.txtSWIFT_GW.BackColor = System.Drawing.Color.White;
            this.txtSWIFT_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSWIFT_GW.Location = new System.Drawing.Point(547, 191);
            this.txtSWIFT_GW.Name = "txtSWIFT_GW";
            this.txtSWIFT_GW.ReadOnly = true;
            this.txtSWIFT_GW.Size = new System.Drawing.Size(121, 21);
            this.txtSWIFT_GW.TabIndex = 89;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(333, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "BR";
            // 
            // btGetData
            // 
            this.btGetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btGetData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetData.Location = new System.Drawing.Point(687, 147);
            this.btGetData.Name = "btGetData";
            this.btGetData.Size = new System.Drawing.Size(100, 30);
            this.btGetData.TabIndex = 94;
            this.btGetData.Text = "GetData";
            this.btGetData.UseVisualStyleBackColor = false;
            this.btGetData.Click += new System.EventHandler(this.OnGetData);
            // 
            // btCheckData
            // 
            this.btCheckData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCheckData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCheckData.Location = new System.Drawing.Point(687, 221);
            this.btCheckData.Name = "btCheckData";
            this.btCheckData.Size = new System.Drawing.Size(100, 30);
            this.btCheckData.TabIndex = 95;
            this.btCheckData.Text = "CheckData";
            this.btCheckData.UseVisualStyleBackColor = true;
            this.btCheckData.Click += new System.EventHandler(this.OnCheckData);
            // 
            // btSIBS_TFInfo
            // 
            this.btSIBS_TFInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSIBS_TFInfo.BackgroundImage")));
            this.btSIBS_TFInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSIBS_TFInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSIBS_TFInfo.Location = new System.Drawing.Point(42, 191);
            this.btSIBS_TFInfo.Name = "btSIBS_TFInfo";
            this.btSIBS_TFInfo.Size = new System.Drawing.Size(25, 20);
            this.btSIBS_TFInfo.TabIndex = 99;
            this.btSIBS_TFInfo.UseVisualStyleBackColor = true;
            this.btSIBS_TFInfo.Click += new System.EventHandler(this.OnSIBSTF);
            // 
            // btInfoTR
            // 
            this.btInfoTR.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoTR.BackgroundImage")));
            this.btInfoTR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoTR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoTR.Location = new System.Drawing.Point(42, 225);
            this.btInfoTR.Name = "btInfoTR";
            this.btInfoTR.Size = new System.Drawing.Size(25, 20);
            this.btInfoTR.TabIndex = 100;
            this.btInfoTR.UseVisualStyleBackColor = true;
            this.btInfoTR.Click += new System.EventHandler(this.OnTR);
            // 
            // btInfoSWIFT
            // 
            this.btInfoSWIFT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btInfoSWIFT.BackgroundImage")));
            this.btInfoSWIFT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btInfoSWIFT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInfoSWIFT.Location = new System.Drawing.Point(427, 191);
            this.btInfoSWIFT.Name = "btInfoSWIFT";
            this.btInfoSWIFT.Size = new System.Drawing.Size(25, 20);
            this.btInfoSWIFT.TabIndex = 101;
            this.btInfoSWIFT.UseVisualStyleBackColor = true;
            this.btInfoSWIFT.Click += new System.EventHandler(this.OnSWIFTRec);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimers);
            // 
            // btSIBS_RMInfo
            // 
            this.btSIBS_RMInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSIBS_RMInfo.BackgroundImage")));
            this.btSIBS_RMInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSIBS_RMInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSIBS_RMInfo.Location = new System.Drawing.Point(42, 161);
            this.btSIBS_RMInfo.Name = "btSIBS_RMInfo";
            this.btSIBS_RMInfo.Size = new System.Drawing.Size(25, 20);
            this.btSIBS_RMInfo.TabIndex = 105;
            this.btSIBS_RMInfo.UseVisualStyleBackColor = true;
            this.btSIBS_RMInfo.Click += new System.EventHandler(this.OnSIBSRM);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(292, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 104;
            this.label4.Text = "SIBS";
            // 
            // txtSIBS_RM
            // 
            this.txtSIBS_RM.BackColor = System.Drawing.Color.White;
            this.txtSIBS_RM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSIBS_RM.Location = new System.Drawing.Point(158, 161);
            this.txtSIBS_RM.Name = "txtSIBS_RM";
            this.txtSIBS_RM.ReadOnly = true;
            this.txtSIBS_RM.Size = new System.Drawing.Size(128, 21);
            this.txtSIBS_RM.TabIndex = 102;
            // 
            // cbSIBSRM_GW
            // 
            this.cbSIBSRM_GW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSIBSRM_GW.DropDownWidth = 80;
            this.cbSIBSRM_GW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSIBSRM_GW.FormattingEnabled = true;
            this.cbSIBSRM_GW.Items.AddRange(new object[] {
            "SIBS.RM->BR",
            "SIBS.RM<-BR"});
            this.cbSIBSRM_GW.Location = new System.Drawing.Point(73, 161);
            this.cbSIBSRM_GW.Name = "cbSIBSRM_GW";
            this.cbSIBSRM_GW.Size = new System.Drawing.Size(82, 20);
            this.cbSIBSRM_GW.TabIndex = 103;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 106;
            this.label7.Text = "TF";
            // 
            // btGW
            // 
            this.btGW.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btGW.BackgroundImage")));
            this.btGW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btGW.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btGW.Location = new System.Drawing.Point(336, 170);
            this.btGW.Name = "btGW";
            this.btGW.Size = new System.Drawing.Size(25, 20);
            this.btGW.TabIndex = 107;
            this.btGW.UseVisualStyleBackColor = true;
            this.btGW.Click += new System.EventHandler(this.OnGW);
            // 
            // frmSwiftRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(798, 640);
            this.Controls.Add(this.btGW);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btSIBS_RMInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSIBS_RM);
            this.Controls.Add(this.cbSIBSRM_GW);
            this.Controls.Add(this.btInfoSWIFT);
            this.Controls.Add(this.btInfoTR);
            this.Controls.Add(this.btSIBS_TFInfo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSIBS_TF);
            this.Controls.Add(this.cbSIBSTF_GW);
            this.Controls.Add(this.txtTR_GW);
            this.Controls.Add(this.cbTR_GW);
            this.Controls.Add(this.cbSWIFT_GW);
            this.Controls.Add(this.txtSWIFT_GW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btGetData);
            this.Controls.Add(this.btCheckData);
            this.Controls.Add(this.grReconcile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdReconcile);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSwiftRec";
            this.Text = "Swift reconcile";
            this.Load += new System.EventHandler(this.frmSwiftRec_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftRec_MouseMove);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdReconcile, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.grReconcile, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.btCheckData, 0);
            this.Controls.SetChildIndex(this.btGetData, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtSWIFT_GW, 0);
            this.Controls.SetChildIndex(this.cbSWIFT_GW, 0);
            this.Controls.SetChildIndex(this.cbTR_GW, 0);
            this.Controls.SetChildIndex(this.txtTR_GW, 0);
            this.Controls.SetChildIndex(this.cbSIBSTF_GW, 0);
            this.Controls.SetChildIndex(this.txtSIBS_TF, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.btSIBS_TFInfo, 0);
            this.Controls.SetChildIndex(this.btInfoTR, 0);
            this.Controls.SetChildIndex(this.btInfoSWIFT, 0);
            this.Controls.SetChildIndex(this.cbSIBSRM_GW, 0);
            this.Controls.SetChildIndex(this.txtSIBS_RM, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.btSIBS_RMInfo, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.btGW, 0);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbltotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSIBS_TF;
        private System.Windows.Forms.ComboBox cbSIBSTF_GW;
        private System.Windows.Forms.TextBox txtTR_GW;
        private System.Windows.Forms.ComboBox cbTR_GW;
        private System.Windows.Forms.ComboBox cbSWIFT_GW;
        private System.Windows.Forms.TextBox txtSWIFT_GW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btGetData;
        private System.Windows.Forms.Button btCheckData;
        private System.Windows.Forms.Button btSIBS_TFInfo;
        private System.Windows.Forms.Button btInfoTR;
        private System.Windows.Forms.Button btInfoSWIFT;
        private System.Windows.Forms.Label lbInformation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btSIBS_RMInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSIBS_RM;
        private System.Windows.Forms.ComboBox cbSIBSRM_GW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btGW;
    }
}