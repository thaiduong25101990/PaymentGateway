namespace BR.BRSWIFT
{
    partial class frmSwiftMsgManual
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
            this.grbDieukien = new System.Windows.Forms.GroupBox();
            this.datDieukien = new System.Windows.Forms.DataGridView();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VIEW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbSearchnhanh = new System.Windows.Forms.GroupBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.cmdRemoveAll = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.dateValue = new System.Windows.Forms.DateTimePicker();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmdadd = new System.Windows.Forms.Button();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.cbColumns = new System.Windows.Forms.ComboBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.cmdProcess = new System.Windows.Forms.Button();
            this.cmdview = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dataMessage = new System.Windows.Forms.DataGridView();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.cbProcess_Status = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cbMsg_status = new System.Windows.Forms.ComboBox();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.txtOSN = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.txtreceiver = new System.Windows.Forms.TextBox();
            this.txtsender = new System.Windows.Forms.TextBox();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbOSN = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdAdvance = new System.Windows.Forms.Button();
            this.cmdNornal = new System.Windows.Forms.Button();
            this.Lbtong = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmdExport = new System.Windows.Forms.Button();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.grbDieukien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).BeginInit();
            this.grbSearchnhanh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).BeginInit();
            this.grbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDieukien
            // 
            this.grbDieukien.Controls.Add(this.datDieukien);
            this.grbDieukien.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDieukien.Location = new System.Drawing.Point(330, -2);
            this.grbDieukien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Name = "grbDieukien";
            this.grbDieukien.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Size = new System.Drawing.Size(565, 144);
            this.grbDieukien.TabIndex = 2;
            this.grbDieukien.TabStop = false;
            this.grbDieukien.Text = "List of conditions";
            // 
            // datDieukien
            // 
            this.datDieukien.AllowUserToAddRows = false;
            this.datDieukien.BackgroundColor = System.Drawing.Color.White;
            this.datDieukien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datDieukien.ColumnHeadersVisible = false;
            this.datDieukien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox,
            this.VIEW,
            this.VALUE});
            this.datDieukien.Location = new System.Drawing.Point(6, 18);
            this.datDieukien.Name = "datDieukien";
            this.datDieukien.RowHeadersVisible = false;
            this.datDieukien.Size = new System.Drawing.Size(552, 120);
            this.datDieukien.TabIndex = 0;
            this.datDieukien.MouseMove += new System.Windows.Forms.MouseEventHandler(this.datDieukien_MouseMove);
            // 
            // CheckBox
            // 
            this.CheckBox.HeaderText = "CheckBox";
            this.CheckBox.Name = "CheckBox";
            // 
            // VIEW
            // 
            this.VIEW.HeaderText = "View";
            this.VIEW.Name = "VIEW";
            this.VIEW.Width = 300;
            // 
            // VALUE
            // 
            this.VALUE.HeaderText = "Value";
            this.VALUE.Name = "VALUE";
            this.VALUE.Visible = false;
            // 
            // grbSearchnhanh
            // 
            this.grbSearchnhanh.Controls.Add(this.cboStatus);
            this.grbSearchnhanh.Controls.Add(this.cmdRemoveAll);
            this.grbSearchnhanh.Controls.Add(this.cmdRemove);
            this.grbSearchnhanh.Controls.Add(this.dateValue);
            this.grbSearchnhanh.Controls.Add(this.txtValue);
            this.grbSearchnhanh.Controls.Add(this.cmdadd);
            this.grbSearchnhanh.Controls.Add(this.cbOperator);
            this.grbSearchnhanh.Controls.Add(this.cbColumns);
            this.grbSearchnhanh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearchnhanh.Location = new System.Drawing.Point(14, -2);
            this.grbSearchnhanh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Name = "grbSearchnhanh";
            this.grbSearchnhanh.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Size = new System.Drawing.Size(307, 144);
            this.grbSearchnhanh.TabIndex = 1;
            this.grbSearchnhanh.TabStop = false;
            this.grbSearchnhanh.Text = "Searching condition";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(27, 96);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(191, 24);
            this.cboStatus.TabIndex = 2;
            // 
            // cmdRemoveAll
            // 
            this.cmdRemoveAll.Location = new System.Drawing.Point(236, 100);
            this.cmdRemoveAll.Name = "cmdRemoveAll";
            this.cmdRemoveAll.Size = new System.Drawing.Size(56, 26);
            this.cmdRemoveAll.TabIndex = 6;
            this.cmdRemoveAll.Text = "<<";
            this.cmdRemoveAll.UseVisualStyleBackColor = true;
            this.cmdRemoveAll.Click += new System.EventHandler(this.cmdRemoveAll_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(237, 69);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(56, 26);
            this.cmdRemove.TabIndex = 5;
            this.cmdRemove.Text = "<";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // dateValue
            // 
            this.dateValue.CustomFormat = "dd/MM/yyyyy ";
            this.dateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue.Location = new System.Drawing.Point(27, 122);
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(191, 23);
            this.dateValue.TabIndex = 3;
            this.dateValue.Visible = false;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(27, 95);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(191, 23);
            this.txtValue.TabIndex = 3;
            // 
            // cmdadd
            // 
            this.cmdadd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdadd.Location = new System.Drawing.Point(237, 37);
            this.cmdadd.Name = "cmdadd";
            this.cmdadd.Size = new System.Drawing.Size(55, 25);
            this.cmdadd.TabIndex = 4;
            this.cmdadd.Text = ">";
            this.cmdadd.UseVisualStyleBackColor = true;
            this.cmdadd.Click += new System.EventHandler(this.cmdadd_Click);
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(27, 62);
            this.cbOperator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(191, 24);
            this.cbOperator.TabIndex = 1;
            // 
            // cbColumns
            // 
            this.cbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbColumns.FormattingEnabled = true;
            this.cbColumns.Location = new System.Drawing.Point(27, 30);
            this.cbColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(191, 24);
            this.cbColumns.TabIndex = 0;
            this.cbColumns.SelectedIndexChanged += new System.EventHandler(this.cboName_SelectedIndexChanged);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(901, 38);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdProcess
            // 
            this.cmdProcess.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdProcess.Location = new System.Drawing.Point(729, 601);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(80, 30);
            this.cmdProcess.TabIndex = 7;
            this.cmdProcess.Text = "P&rocess";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // cmdview
            // 
            this.cmdview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdview.Location = new System.Drawing.Point(815, 601);
            this.cmdview.Name = "cmdview";
            this.cmdview.Size = new System.Drawing.Size(80, 30);
            this.cmdview.TabIndex = 8;
            this.cmdview.Text = "&View";
            this.cmdview.UseVisualStyleBackColor = true;
            this.cmdview.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(901, 601);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dataMessage
            // 
            this.dataMessage.AllowUserToAddRows = false;
            this.dataMessage.AllowUserToDeleteRows = false;
            this.dataMessage.BackgroundColor = System.Drawing.Color.White;
            this.dataMessage.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataMessage.ColumnHeadersHeight = 21;
            this.dataMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataMessage.Location = new System.Drawing.Point(14, 163);
            this.dataMessage.Name = "dataMessage";
            this.dataMessage.RowHeadersWidth = 30;
            this.dataMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMessage.Size = new System.Drawing.Size(966, 432);
            this.dataMessage.TabIndex = 20;
            this.dataMessage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellDoubleClick);
            this.dataMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataMessage_MouseMove);
            this.dataMessage.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataMessage_ColumnHeaderMouseClick);
            this.dataMessage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellClick);
            this.dataMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataMessage_KeyDown);
            this.dataMessage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellEnter);
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.cbProcess_Status);
            this.grbSearch.Controls.Add(this.label15);
            this.grbSearch.Controls.Add(this.cbStatus);
            this.grbSearch.Controls.Add(this.cbMsg_status);
            this.grbSearch.Controls.Add(this.cbCurrency);
            this.grbSearch.Controls.Add(this.cbdepartment);
            this.grbSearch.Controls.Add(this.txtOSN);
            this.grbSearch.Controls.Add(this.txtAmount);
            this.grbSearch.Controls.Add(this.txtrefno);
            this.grbSearch.Controls.Add(this.txtMsg_type);
            this.grbSearch.Controls.Add(this.txtreceiver);
            this.grbSearch.Controls.Add(this.txtsender);
            this.grbSearch.Controls.Add(this.datefrom);
            this.grbSearch.Controls.Add(this.dateto);
            this.grbSearch.Controls.Add(this.label1);
            this.grbSearch.Controls.Add(this.label13);
            this.grbSearch.Controls.Add(this.label8);
            this.grbSearch.Controls.Add(this.label4);
            this.grbSearch.Controls.Add(this.label11);
            this.grbSearch.Controls.Add(this.label7);
            this.grbSearch.Controls.Add(this.label10);
            this.grbSearch.Controls.Add(this.label3);
            this.grbSearch.Controls.Add(this.lbOSN);
            this.grbSearch.Controls.Add(this.label6);
            this.grbSearch.Controls.Add(this.label5);
            this.grbSearch.Controls.Add(this.label2);
            this.grbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearch.Location = new System.Drawing.Point(41, 701);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(883, 144);
            this.grbSearch.TabIndex = 0;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Searching condition";
            // 
            // cbProcess_Status
            // 
            this.cbProcess_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcess_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProcess_Status.FormattingEnabled = true;
            this.cbProcess_Status.Location = new System.Drawing.Point(693, 63);
            this.cbProcess_Status.Name = "cbProcess_Status";
            this.cbProcess_Status.Size = new System.Drawing.Size(166, 24);
            this.cbProcess_Status.TabIndex = 216;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(596, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(102, 16);
            this.label15.TabIndex = 215;
            this.label15.Text = "Process status :";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(693, 12);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(166, 24);
            this.cbStatus.TabIndex = 12;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // cbMsg_status
            // 
            this.cbMsg_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsg_status.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMsg_status.FormattingEnabled = true;
            this.cbMsg_status.Location = new System.Drawing.Point(693, 37);
            this.cbMsg_status.Name = "cbMsg_status";
            this.cbMsg_status.Size = new System.Drawing.Size(166, 24);
            this.cbMsg_status.TabIndex = 11;
            this.cbMsg_status.SelectedIndexChanged += new System.EventHandler(this.cbMsg_status_SelectedIndexChanged);
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(388, 63);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(166, 24);
            this.cbCurrency.TabIndex = 8;
            this.cbCurrency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCurrency_KeyPress);
            this.cbCurrency.TextChanged += new System.EventHandler(this.cbCurrency_TextChanged);
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(388, 89);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(166, 24);
            this.cbdepartment.TabIndex = 9;
            // 
            // txtOSN
            // 
            this.txtOSN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOSN.Location = new System.Drawing.Point(388, 116);
            this.txtOSN.Name = "txtOSN";
            this.txtOSN.Size = new System.Drawing.Size(166, 23);
            this.txtOSN.TabIndex = 10;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(388, 38);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(166, 23);
            this.txtAmount.TabIndex = 7;
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrefno.Location = new System.Drawing.Point(388, 13);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(166, 23);
            this.txtrefno.TabIndex = 6;
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg_type.Location = new System.Drawing.Point(110, 116);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(163, 23);
            this.txtMsg_type.TabIndex = 5;
            // 
            // txtreceiver
            // 
            this.txtreceiver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiver.Location = new System.Drawing.Point(110, 90);
            this.txtreceiver.Name = "txtreceiver";
            this.txtreceiver.Size = new System.Drawing.Size(163, 23);
            this.txtreceiver.TabIndex = 3;
            // 
            // txtsender
            // 
            this.txtsender.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsender.Location = new System.Drawing.Point(110, 64);
            this.txtsender.Name = "txtsender";
            this.txtsender.Size = new System.Drawing.Size(163, 23);
            this.txtsender.TabIndex = 2;
            // 
            // datefrom
            // 
            this.datefrom.Checked = false;
            this.datefrom.CustomFormat = "dd/MM/yyyyy";
            this.datefrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(111, 13);
            this.datefrom.Name = "datefrom";
            this.datefrom.ShowCheckBox = true;
            this.datefrom.Size = new System.Drawing.Size(163, 23);
            this.datefrom.TabIndex = 0;
            this.datefrom.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // dateto
            // 
            this.dateto.Checked = false;
            this.dateto.CustomFormat = "dd/MM/yyyyy";
            this.dateto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(110, 38);
            this.dateto.Name = "dateto";
            this.dateto.ShowCheckBox = true;
            this.dateto.Size = new System.Drawing.Size(163, 23);
            this.dateto.TabIndex = 1;
            this.dateto.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(34, 119);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "Msg type :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(312, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Module :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Receiver :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(596, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "Status :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(312, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Currency :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(596, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Msg status :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sender :";
            // 
            // lbOSN
            // 
            this.lbOSN.AutoSize = true;
            this.lbOSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOSN.Location = new System.Drawing.Point(312, 119);
            this.lbOSN.Name = "lbOSN";
            this.lbOSN.Size = new System.Drawing.Size(43, 16);
            this.lbOSN.TabIndex = 0;
            this.lbOSN.Text = "OSN :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(312, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Amount :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(312, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ref no :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "To date :";
            // 
            // cmdAdvance
            // 
            this.cmdAdvance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdvance.Location = new System.Drawing.Point(901, 109);
            this.cmdAdvance.Name = "cmdAdvance";
            this.cmdAdvance.Size = new System.Drawing.Size(80, 30);
            this.cmdAdvance.TabIndex = 5;
            this.cmdAdvance.Text = "&Advance";
            this.cmdAdvance.UseVisualStyleBackColor = true;
            this.cmdAdvance.Click += new System.EventHandler(this.cmdAdvance_Click);
            // 
            // cmdNornal
            // 
            this.cmdNornal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNornal.Location = new System.Drawing.Point(901, 74);
            this.cmdNornal.Name = "cmdNornal";
            this.cmdNornal.Size = new System.Drawing.Size(80, 30);
            this.cmdNornal.TabIndex = 4;
            this.cmdNornal.Text = "&Normal";
            this.cmdNornal.UseVisualStyleBackColor = true;
            this.cmdNornal.Click += new System.EventHandler(this.cmdNornal_Click);
            // 
            // Lbtong
            // 
            this.Lbtong.AutoSize = true;
            this.Lbtong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbtong.ForeColor = System.Drawing.Color.Blue;
            this.Lbtong.Location = new System.Drawing.Point(187, 145);
            this.Lbtong.Name = "Lbtong";
            this.Lbtong.Size = new System.Drawing.Size(0, 16);
            this.Lbtong.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(17, 145);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(174, 16);
            this.label12.TabIndex = 24;
            this.label12.Text = "Total number of messages :";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Column1";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "  Select";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Width = 50;
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(643, 602);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(80, 30);
            this.cmdExport.TabIndex = 6;
            this.cmdExport.Text = "&Exp Excel";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // date1
            // 
            this.date1.Checked = false;
            this.date1.CustomFormat = "dd/MM/yyyyy";
            this.date1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date1.Location = new System.Drawing.Point(20, 336);
            this.date1.MaxDate = new System.DateTime(1983, 6, 20, 0, 0, 0, 0);
            this.date1.MinDate = new System.DateTime(1983, 6, 20, 0, 0, 0, 0);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(163, 23);
            this.date1.TabIndex = 0;
            this.date1.Value = new System.DateTime(1983, 6, 20, 0, 0, 0, 0);
            this.date1.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // frmSwiftMsgManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 647);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.Lbtong);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmdAdvance);
            this.Controls.Add(this.cmdNornal);
            this.Controls.Add(this.grbSearch);
            this.Controls.Add(this.dataMessage);
            this.Controls.Add(this.grbDieukien);
            this.Controls.Add(this.grbSearchnhanh);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdview);
            this.Controls.Add(this.cmdProcess);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.cmdSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSwiftMsgManual";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Swift outward manual process";
            this.Load += new System.EventHandler(this.frmSwiftMsgManual_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSwiftMsgManual_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftMsgManual_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftMsgManual_KeyDown);
            this.grbDieukien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).EndInit();
            this.grbSearchnhanh.ResumeLayout(false);
            this.grbSearchnhanh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).EndInit();
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDieukien;
        private System.Windows.Forms.DataGridView datDieukien;
        private System.Windows.Forms.GroupBox grbSearchnhanh;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button cmdadd;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbColumns;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.Button cmdview;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.DataGridView dataMessage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DateTimePicker dateValue;
        private System.Windows.Forms.Button cmdRemoveAll;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ComboBox cbMsg_status;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.TextBox txtOSN;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.TextBox txtreceiver;
        private System.Windows.Forms.TextBox txtsender;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbOSN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdAdvance;
        private System.Windows.Forms.Button cmdNornal;
        private System.Windows.Forms.Label Lbtong;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.ComboBox cbProcess_Status;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIEW;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE;
    }
}