namespace BR.BRSWIFT
{
    partial class frmSwiftMsgList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdremoveall = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.grbSearchnhanh = new System.Windows.Forms.GroupBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.cmdAdd1 = new System.Windows.Forms.Button();
            this.cmdremove = new System.Windows.Forms.Button();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.cbColumns = new System.Windows.Forms.ComboBox();
            this.dateValue = new System.Windows.Forms.DateTimePicker();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.txtreceiver = new System.Windows.Forms.TextBox();
            this.txtsender = new System.Windows.Forms.TextBox();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdAdvance = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.datDieukien = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grbDieukien = new System.Windows.Forms.GroupBox();
            this.cmdNornal = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.cbProcess_Status = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboResource = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbmsg_direction = new System.Windows.Forms.ComboBox();
            this.cbMsg_status = new System.Windows.Forms.ComboBox();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.txtOSN = new System.Windows.Forms.TextBox();
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbOSN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdview = new System.Windows.Forms.Button();
            this.dataMessage = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cbCheck = new System.Windows.Forms.ComboBox();
            this.cmdRepair = new System.Windows.Forms.Button();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VIEW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbSearchnhanh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).BeginInit();
            this.grbDieukien.SuspendLayout();
            this.grbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(923, 576);
            this.cmdClose.TabIndex = 11;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(435, 786);
            this.cmdSave.Text = "Save";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(349, 786);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(260, 786);
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(686, 68);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(163, 24);
            this.cbStatus.TabIndex = 12;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(755, 576);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 9;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdremoveall
            // 
            this.cmdremoveall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremoveall.Location = new System.Drawing.Point(273, 95);
            this.cmdremoveall.Name = "cmdremoveall";
            this.cmdremoveall.Size = new System.Drawing.Size(49, 23);
            this.cmdremoveall.TabIndex = 6;
            this.cmdremoveall.Text = "<<";
            this.cmdremoveall.UseVisualStyleBackColor = true;
            this.cmdremoveall.Click += new System.EventHandler(this.cmdremoveall_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(40, 107);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(191, 23);
            this.txtValue.TabIndex = 2;
            // 
            // grbSearchnhanh
            // 
            this.grbSearchnhanh.Controls.Add(this.cboStatus);
            this.grbSearchnhanh.Controls.Add(this.cmdAdd1);
            this.grbSearchnhanh.Controls.Add(this.txtValue);
            this.grbSearchnhanh.Controls.Add(this.cmdremoveall);
            this.grbSearchnhanh.Controls.Add(this.cmdremove);
            this.grbSearchnhanh.Controls.Add(this.cmdAdd);
            this.grbSearchnhanh.Controls.Add(this.cbOperator);
            this.grbSearchnhanh.Controls.Add(this.cbColumns);
            this.grbSearchnhanh.Controls.Add(this.dateValue);
            this.grbSearchnhanh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearchnhanh.Location = new System.Drawing.Point(15, -3);
            this.grbSearchnhanh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Name = "grbSearchnhanh";
            this.grbSearchnhanh.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Size = new System.Drawing.Size(351, 150);
            this.grbSearchnhanh.TabIndex = 0;
            this.grbSearchnhanh.TabStop = false;
            this.grbSearchnhanh.Text = "Searching condition";
            this.grbSearchnhanh.Controls.SetChildIndex(this.dateValue, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cbColumns, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cbOperator, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cmdAdd, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cmdremove, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cmdremoveall, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.txtValue, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cmdAdd1, 0);
            this.grbSearchnhanh.Controls.SetChildIndex(this.cboStatus, 0);
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(40, 106);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(191, 24);
            this.cboStatus.TabIndex = 3;
            // 
            // cmdAdd1
            // 
            this.cmdAdd1.Location = new System.Drawing.Point(274, 42);
            this.cmdAdd1.Name = "cmdAdd1";
            this.cmdAdd1.Size = new System.Drawing.Size(48, 23);
            this.cmdAdd1.TabIndex = 4;
            this.cmdAdd1.Text = ">";
            this.cmdAdd1.UseVisualStyleBackColor = true;
            this.cmdAdd1.Click += new System.EventHandler(this.cmdAdd1_Click);
            // 
            // cmdremove
            // 
            this.cmdremove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremove.Location = new System.Drawing.Point(273, 69);
            this.cmdremove.Name = "cmdremove";
            this.cmdremove.Size = new System.Drawing.Size(49, 23);
            this.cmdremove.TabIndex = 5;
            this.cmdremove.Text = "<";
            this.cmdremove.UseVisualStyleBackColor = true;
            this.cmdremove.Click += new System.EventHandler(this.cmdremove_Click);
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(40, 67);
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
            this.cbColumns.Location = new System.Drawing.Point(40, 26);
            this.cbColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(191, 24);
            this.cbColumns.TabIndex = 0;
            this.cbColumns.SelectedIndexChanged += new System.EventHandler(this.cbColumns_SelectedIndexChanged);
            // 
            // dateValue
            // 
            this.dateValue.CustomFormat = "dd/MM/yyyyy";
            this.dateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue.Location = new System.Drawing.Point(40, 137);
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(191, 23);
            this.dateValue.TabIndex = 1;
            this.dateValue.Visible = false;
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(686, 16);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(163, 24);
            this.cbdepartment.TabIndex = 10;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(382, 43);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(160, 23);
            this.txtAmount.TabIndex = 6;
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrefno.Location = new System.Drawing.Point(382, 17);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(160, 23);
            this.txtrefno.TabIndex = 5;
            // 
            // txtreceiver
            // 
            this.txtreceiver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiver.Location = new System.Drawing.Point(101, 95);
            this.txtreceiver.Name = "txtreceiver";
            this.txtreceiver.Size = new System.Drawing.Size(163, 23);
            this.txtreceiver.TabIndex = 3;
            // 
            // txtsender
            // 
            this.txtsender.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsender.Location = new System.Drawing.Point(101, 69);
            this.txtsender.Name = "txtsender";
            this.txtsender.Size = new System.Drawing.Size(163, 23);
            this.txtsender.TabIndex = 2;
            // 
            // dateto
            // 
            this.dateto.Checked = false;
            this.dateto.CustomFormat = "dd/MM/yyyyy";
            this.dateto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(101, 43);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(163, 23);
            this.dateto.TabIndex = 1;
            // 
            // datefrom
            // 
            this.datefrom.Checked = false;
            this.datefrom.CustomFormat = "dd/MM/yyyyy";
            this.datefrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(101, 17);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(163, 23);
            this.datefrom.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(583, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Module :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Receiver :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(583, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "Status :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Currency :";
            // 
            // cmdAdvance
            // 
            this.cmdAdvance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdvance.Location = new System.Drawing.Point(923, 84);
            this.cmdAdvance.Name = "cmdAdvance";
            this.cmdAdvance.Size = new System.Drawing.Size(80, 30);
            this.cmdAdvance.TabIndex = 4;
            this.cmdAdvance.Text = "&Advance";
            this.cmdAdvance.UseVisualStyleBackColor = true;
            this.cmdAdvance.Click += new System.EventHandler(this.cmdAdvance_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sender :";
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
            this.datDieukien.Location = new System.Drawing.Point(6, 17);
            this.datDieukien.Name = "datDieukien";
            this.datDieukien.RowHeadersVisible = false;
            this.datDieukien.Size = new System.Drawing.Size(534, 126);
            this.datDieukien.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(583, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Msg direction :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Amount :";
            // 
            // grbDieukien
            // 
            this.grbDieukien.Controls.Add(this.datDieukien);
            this.grbDieukien.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDieukien.Location = new System.Drawing.Point(369, -3);
            this.grbDieukien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Name = "grbDieukien";
            this.grbDieukien.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Size = new System.Drawing.Size(546, 150);
            this.grbDieukien.TabIndex = 1;
            this.grbDieukien.TabStop = false;
            this.grbDieukien.Text = "List of conditions";
            // 
            // cmdNornal
            // 
            this.cmdNornal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNornal.Location = new System.Drawing.Point(923, 48);
            this.cmdNornal.Name = "cmdNornal";
            this.cmdNornal.Size = new System.Drawing.Size(80, 30);
            this.cmdNornal.TabIndex = 3;
            this.cmdNornal.Text = "&Normal";
            this.cmdNornal.UseVisualStyleBackColor = true;
            this.cmdNornal.Click += new System.EventHandler(this.cmdNornal_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ref no :";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(923, 15);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "To :";
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.cbProcess_Status);
            this.grbSearch.Controls.Add(this.label15);
            this.grbSearch.Controls.Add(this.cboResource);
            this.grbSearch.Controls.Add(this.label14);
            this.grbSearch.Controls.Add(this.cbmsg_direction);
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
            this.grbSearch.Controls.Add(this.dateto);
            this.grbSearch.Controls.Add(this.datefrom);
            this.grbSearch.Controls.Add(this.label13);
            this.grbSearch.Controls.Add(this.label8);
            this.grbSearch.Controls.Add(this.label4);
            this.grbSearch.Controls.Add(this.label11);
            this.grbSearch.Controls.Add(this.label7);
            this.grbSearch.Controls.Add(this.label10);
            this.grbSearch.Controls.Add(this.label3);
            this.grbSearch.Controls.Add(this.lbOSN);
            this.grbSearch.Controls.Add(this.label9);
            this.grbSearch.Controls.Add(this.label6);
            this.grbSearch.Controls.Add(this.label5);
            this.grbSearch.Controls.Add(this.label2);
            this.grbSearch.Controls.Add(this.label1);
            this.grbSearch.Location = new System.Drawing.Point(37, 619);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(872, 150);
            this.grbSearch.TabIndex = 6;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Searching condition";
            // 
            // cbProcess_Status
            // 
            this.cbProcess_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcess_Status.FormattingEnabled = true;
            this.cbProcess_Status.Location = new System.Drawing.Point(686, 120);
            this.cbProcess_Status.Name = "cbProcess_Status";
            this.cbProcess_Status.Size = new System.Drawing.Size(163, 24);
            this.cbProcess_Status.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(583, 124);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 16);
            this.label15.TabIndex = 213;
            this.label15.Text = "Process status :";
            // 
            // cboResource
            // 
            this.cboResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResource.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboResource.FormattingEnabled = true;
            this.cboResource.Location = new System.Drawing.Point(382, 120);
            this.cboResource.Name = "cboResource";
            this.cboResource.Size = new System.Drawing.Size(160, 24);
            this.cboResource.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(298, 124);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 16);
            this.label14.TabIndex = 212;
            this.label14.Text = "Msg source :";
            // 
            // cbmsg_direction
            // 
            this.cbmsg_direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmsg_direction.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbmsg_direction.FormattingEnabled = true;
            this.cbmsg_direction.Location = new System.Drawing.Point(686, 42);
            this.cbmsg_direction.Name = "cbmsg_direction";
            this.cbmsg_direction.Size = new System.Drawing.Size(163, 24);
            this.cbmsg_direction.TabIndex = 11;
            // 
            // cbMsg_status
            // 
            this.cbMsg_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsg_status.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMsg_status.FormattingEnabled = true;
            this.cbMsg_status.Location = new System.Drawing.Point(686, 94);
            this.cbMsg_status.Name = "cbMsg_status";
            this.cbMsg_status.Size = new System.Drawing.Size(163, 24);
            this.cbMsg_status.TabIndex = 13;
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(382, 68);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(160, 24);
            this.cbCurrency.TabIndex = 7;
            // 
            // txtOSN
            // 
            this.txtOSN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOSN.Location = new System.Drawing.Point(382, 95);
            this.txtOSN.Name = "txtOSN";
            this.txtOSN.Size = new System.Drawing.Size(160, 23);
            this.txtOSN.TabIndex = 8;
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg_type.Location = new System.Drawing.Point(101, 121);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(163, 23);
            this.txtMsg_type.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 124);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "Msg type :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(583, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Msg status :";
            // 
            // lbOSN
            // 
            this.lbOSN.AutoSize = true;
            this.lbOSN.Location = new System.Drawing.Point(298, 98);
            this.lbOSN.Name = "lbOSN";
            this.lbOSN.Size = new System.Drawing.Size(42, 16);
            this.lbOSN.TabIndex = 0;
            this.lbOSN.Text = "OSN :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From :";
            // 
            // cmdview
            // 
            this.cmdview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdview.Location = new System.Drawing.Point(839, 576);
            this.cmdview.Name = "cmdview";
            this.cmdview.Size = new System.Drawing.Size(80, 30);
            this.cmdview.TabIndex = 10;
            this.cmdview.Text = "&View";
            this.cmdview.UseVisualStyleBackColor = true;
            this.cmdview.Click += new System.EventHandler(this.cmdview_Click);
            // 
            // dataMessage
            // 
            this.dataMessage.AllowUserToAddRows = false;
            this.dataMessage.AllowUserToDeleteRows = false;
            this.dataMessage.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataMessage.ColumnHeadersHeight = 21;
            this.dataMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataMessage.Location = new System.Drawing.Point(15, 166);
            this.dataMessage.Name = "dataMessage";
            this.dataMessage.RowHeadersWidth = 30;
            this.dataMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMessage.Size = new System.Drawing.Size(987, 404);
            this.dataMessage.TabIndex = 22;
            this.dataMessage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellDoubleClick);
            this.dataMessage.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataMessage_ColumnHeaderMouseClick);
            this.dataMessage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellClick);
            this.dataMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataMessage_KeyDown_1);
            this.dataMessage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellEnter);
            this.dataMessage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellContentClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(12, 146);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 16);
            this.label12.TabIndex = 10;
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
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(671, 576);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(80, 30);
            this.cmdExport.TabIndex = 8;
            this.cmdExport.Text = "&Exp Excel";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cbCheck
            // 
            this.cbCheck.FormattingEnabled = true;
            this.cbCheck.Location = new System.Drawing.Point(823, 742);
            this.cbCheck.Name = "cbCheck";
            this.cbCheck.Size = new System.Drawing.Size(121, 24);
            this.cbCheck.TabIndex = 7;
            this.cbCheck.Visible = false;
            // 
            // cmdRepair
            // 
            this.cmdRepair.Location = new System.Drawing.Point(587, 576);
            this.cmdRepair.Name = "cmdRepair";
            this.cmdRepair.Size = new System.Drawing.Size(80, 30);
            this.cmdRepair.TabIndex = 7;
            this.cmdRepair.Text = "&Repair";
            this.cmdRepair.UseVisualStyleBackColor = true;
            this.cmdRepair.Click += new System.EventHandler(this.cmdRepair_Click);
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
            this.VIEW.Width = 120;
            // 
            // VALUE
            // 
            this.VALUE.HeaderText = "Value";
            this.VALUE.Name = "VALUE";
            this.VALUE.Visible = false;
            // 
            // frmSwiftMsgList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 610);
            this.Controls.Add(this.cbCheck);
            this.Controls.Add(this.grbDieukien);
            this.Controls.Add(this.cmdNornal);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.cmdRepair);
            this.Controls.Add(this.cmdAdvance);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.dataMessage);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.grbSearchnhanh);
            this.Controls.Add(this.cmdview);
            this.Controls.Add(this.grbSearch);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSwiftMsgList";
            this.Text = "Swift message management";
            this.Load += new System.EventHandler(this.frmSwiftMsgList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftMsgList_KeyDown);
            this.Controls.SetChildIndex(this.grbSearch, 0);
            this.Controls.SetChildIndex(this.cmdview, 0);
            this.Controls.SetChildIndex(this.grbSearchnhanh, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.dataMessage, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            this.Controls.SetChildIndex(this.cmdAdvance, 0);
            this.Controls.SetChildIndex(this.cmdRepair, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSearch, 0);
            this.Controls.SetChildIndex(this.cmdNornal, 0);
            this.Controls.SetChildIndex(this.grbDieukien, 0);
            this.Controls.SetChildIndex(this.cbCheck, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.grbSearchnhanh.ResumeLayout(false);
            this.grbSearchnhanh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).EndInit();
            this.grbDieukien.ResumeLayout(false);
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdremoveall;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.GroupBox grbSearchnhanh;
        private System.Windows.Forms.Button cmdremove;
        //private System.Windows.Forms.Button cmdadd;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbColumns;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.TextBox txtreceiver;
        private System.Windows.Forms.TextBox txtsender;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdAdvance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView datDieukien;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grbDieukien;
        private System.Windows.Forms.Button cmdNornal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.ComboBox cbMsg_status;
        private System.Windows.Forms.TextBox txtOSN;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbOSN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdview;
        private System.Windows.Forms.DataGridView dataMessage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cmdAdd1;
        private System.Windows.Forms.DateTimePicker dateValue;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.ComboBox cboResource;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbProcess_Status;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.ComboBox cbCheck;
        private System.Windows.Forms.ComboBox cbmsg_direction;
        private System.Windows.Forms.Button cmdRepair;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIEW;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE;
    }
}