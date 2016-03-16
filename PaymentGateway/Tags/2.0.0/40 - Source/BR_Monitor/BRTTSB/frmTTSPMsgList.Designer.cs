namespace BR.BRTTSB
{
    partial class frmTTSPMsgList
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
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRMno = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cbMsgDirection = new System.Windows.Forms.ComboBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.txtreceiver = new System.Windows.Forms.TextBox();
            this.txtsend = new System.Windows.Forms.TextBox();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdremove = new System.Windows.Forms.Button();
            this.cmdremoveall = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmdadd1 = new System.Windows.Forms.Button();
            this.grbSearchnhanh = new System.Windows.Forms.GroupBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.cbColumns = new System.Windows.Forms.ComboBox();
            this.dateValue = new System.Windows.Forms.DateTimePicker();
            this.cmdAdvance = new System.Windows.Forms.Button();
            this.datDieukien = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbDieukien = new System.Windows.Forms.GroupBox();
            this.cmdNornal = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdIQS = new System.Windows.Forms.Button();
            this.cmdview = new System.Windows.Forms.Button();
            this.dataMessage = new System.Windows.Forms.DataGridView();
            this.Lbtong = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdRelease = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cbCheck = new System.Windows.Forms.ComboBox();
            this.grbSearch.SuspendLayout();
            this.grbSearchnhanh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).BeginInit();
            this.grbDieukien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(920, 571);
            this.cmdClose.TabIndex = 13;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(432, 669);
            this.cmdSave.Text = "Save";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(346, 669);
            this.cmdDelete.Text = "Delete";
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(171, 669);
            this.cmdAdd.Text = "Add";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(257, 669);
            this.cmdEdit.Text = "Edit";
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.txtMsg_type);
            this.grbSearch.Controls.Add(this.label13);
            this.grbSearch.Controls.Add(this.txtRMno);
            this.grbSearch.Controls.Add(this.label10);
            this.grbSearch.Controls.Add(this.cbCurrency);
            this.grbSearch.Controls.Add(this.label7);
            this.grbSearch.Controls.Add(this.cbStatus);
            this.grbSearch.Controls.Add(this.cbMsgDirection);
            this.grbSearch.Controls.Add(this.cbdepartment);
            this.grbSearch.Controls.Add(this.txtAmount);
            this.grbSearch.Controls.Add(this.txtrefno);
            this.grbSearch.Controls.Add(this.txtreceiver);
            this.grbSearch.Controls.Add(this.txtsend);
            this.grbSearch.Controls.Add(this.dateto);
            this.grbSearch.Controls.Add(this.datefrom);
            this.grbSearch.Controls.Add(this.label8);
            this.grbSearch.Controls.Add(this.label4);
            this.grbSearch.Controls.Add(this.label11);
            this.grbSearch.Controls.Add(this.label3);
            this.grbSearch.Controls.Add(this.label9);
            this.grbSearch.Controls.Add(this.label6);
            this.grbSearch.Controls.Add(this.label5);
            this.grbSearch.Controls.Add(this.label2);
            this.grbSearch.Controls.Add(this.label1);
            this.grbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearch.Location = new System.Drawing.Point(19, 694);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(891, 148);
            this.grbSearch.TabIndex = 0;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Searching condition";
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg_type.Location = new System.Drawing.Point(708, 118);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(163, 23);
            this.txtMsg_type.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(616, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 16);
            this.label13.TabIndex = 15;
            this.label13.Text = "Msg type :";
            // 
            // txtRMno
            // 
            this.txtRMno.Location = new System.Drawing.Point(386, 115);
            this.txtRMno.Name = "txtRMno";
            this.txtRMno.Size = new System.Drawing.Size(163, 22);
            this.txtRMno.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "RM no :";
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(386, 85);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(163, 24);
            this.cbCurrency.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Currency :";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(708, 54);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(163, 24);
            this.cbStatus.TabIndex = 23;
            // 
            // cbMsgDirection
            // 
            this.cbMsgDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsgDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMsgDirection.FormattingEnabled = true;
            this.cbMsgDirection.Location = new System.Drawing.Point(708, 24);
            this.cbMsgDirection.Name = "cbMsgDirection";
            this.cbMsgDirection.Size = new System.Drawing.Size(163, 24);
            this.cbMsgDirection.TabIndex = 22;
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(708, 85);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(163, 24);
            this.cbdepartment.TabIndex = 24;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(386, 55);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(163, 23);
            this.txtAmount.TabIndex = 19;
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrefno.Location = new System.Drawing.Point(386, 24);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(163, 23);
            this.txtrefno.TabIndex = 18;
            // 
            // txtreceiver
            // 
            this.txtreceiver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiver.Location = new System.Drawing.Point(88, 115);
            this.txtreceiver.Name = "txtreceiver";
            this.txtreceiver.Size = new System.Drawing.Size(163, 23);
            this.txtreceiver.TabIndex = 17;
            // 
            // txtsend
            // 
            this.txtsend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsend.Location = new System.Drawing.Point(88, 86);
            this.txtsend.Name = "txtsend";
            this.txtsend.Size = new System.Drawing.Size(163, 23);
            this.txtsend.TabIndex = 16;
            // 
            // dateto
            // 
            this.dateto.Checked = false;
            this.dateto.CustomFormat = "dd/MM/yyyyy";
            this.dateto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(88, 55);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(163, 23);
            this.dateto.TabIndex = 15;
            this.dateto.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // datefrom
            // 
            this.datefrom.Checked = false;
            this.datefrom.CustomFormat = "dd/MM/yyyyy";
            this.datefrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(88, 24);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(163, 23);
            this.datefrom.TabIndex = 14;
            this.datefrom.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(616, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Module :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Receiver :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(616, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "Status :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sender :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(616, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Msg direction :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Amount :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ref no :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From :";
            // 
            // cmdremove
            // 
            this.cmdremove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremove.Location = new System.Drawing.Point(284, 70);
            this.cmdremove.Name = "cmdremove";
            this.cmdremove.Size = new System.Drawing.Size(49, 23);
            this.cmdremove.TabIndex = 4;
            this.cmdremove.Text = "<";
            this.cmdremove.UseVisualStyleBackColor = true;
            this.cmdremove.Click += new System.EventHandler(this.cmdremove_Click);
            // 
            // cmdremoveall
            // 
            this.cmdremoveall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremoveall.Location = new System.Drawing.Point(284, 100);
            this.cmdremoveall.Name = "cmdremoveall";
            this.cmdremoveall.Size = new System.Drawing.Size(49, 23);
            this.cmdremoveall.TabIndex = 5;
            this.cmdremoveall.Text = "<<";
            this.cmdremoveall.UseVisualStyleBackColor = true;
            this.cmdremoveall.Click += new System.EventHandler(this.cmdremoveall_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(33, 100);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(216, 22);
            this.txtValue.TabIndex = 2;
            // 
            // cmdadd1
            // 
            this.cmdadd1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdadd1.Location = new System.Drawing.Point(284, 41);
            this.cmdadd1.Name = "cmdadd1";
            this.cmdadd1.Size = new System.Drawing.Size(49, 23);
            this.cmdadd1.TabIndex = 3;
            this.cmdadd1.Text = ">";
            this.cmdadd1.UseVisualStyleBackColor = true;
            this.cmdadd1.Click += new System.EventHandler(this.cmdadd_Click);
            // 
            // grbSearchnhanh
            // 
            this.grbSearchnhanh.Controls.Add(this.cboStatus);
            this.grbSearchnhanh.Controls.Add(this.txtValue);
            this.grbSearchnhanh.Controls.Add(this.cmdremoveall);
            this.grbSearchnhanh.Controls.Add(this.cmdremove);
            this.grbSearchnhanh.Controls.Add(this.cmdadd1);
            this.grbSearchnhanh.Controls.Add(this.cbOperator);
            this.grbSearchnhanh.Controls.Add(this.cbColumns);
            this.grbSearchnhanh.Controls.Add(this.dateValue);
            this.grbSearchnhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearchnhanh.Location = new System.Drawing.Point(13, 13);
            this.grbSearchnhanh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Name = "grbSearchnhanh";
            this.grbSearchnhanh.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Size = new System.Drawing.Size(364, 154);
            this.grbSearchnhanh.TabIndex = 11;
            this.grbSearchnhanh.TabStop = false;
            this.grbSearchnhanh.Text = "Searching condition";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(33, 101);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(216, 24);
            this.cboStatus.TabIndex = 6;
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(33, 69);
            this.cbOperator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(216, 24);
            this.cbOperator.TabIndex = 1;
            // 
            // cbColumns
            // 
            this.cbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbColumns.FormattingEnabled = true;
            this.cbColumns.Location = new System.Drawing.Point(33, 37);
            this.cbColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(216, 24);
            this.cbColumns.TabIndex = 0;
            this.cbColumns.SelectedIndexChanged += new System.EventHandler(this.cbbAmount_SelectedIndexChanged);
            // 
            // dateValue
            // 
            this.dateValue.CustomFormat = "dd/MM/yyyyy";
            this.dateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue.Location = new System.Drawing.Point(33, 131);
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(216, 22);
            this.dateValue.TabIndex = 2;
            this.dateValue.Visible = false;
            // 
            // cmdAdvance
            // 
            this.cmdAdvance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdvance.Location = new System.Drawing.Point(920, 174);
            this.cmdAdvance.Name = "cmdAdvance";
            this.cmdAdvance.Size = new System.Drawing.Size(80, 30);
            this.cmdAdvance.TabIndex = 9;
            this.cmdAdvance.Text = "&Advance";
            this.cmdAdvance.UseVisualStyleBackColor = true;
            this.cmdAdvance.Click += new System.EventHandler(this.cmdAdvance_Click);
            // 
            // datDieukien
            // 
            this.datDieukien.AllowUserToAddRows = false;
            this.datDieukien.BackgroundColor = System.Drawing.Color.White;
            this.datDieukien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datDieukien.ColumnHeadersVisible = false;
            this.datDieukien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.datDieukien.Location = new System.Drawing.Point(6, 21);
            this.datDieukien.Name = "datDieukien";
            this.datDieukien.RowHeadersVisible = false;
            this.datDieukien.Size = new System.Drawing.Size(520, 120);
            this.datDieukien.TabIndex = 6;
            this.datDieukien.MouseMove += new System.Windows.Forms.MouseEventHandler(this.datDieukien_MouseMove);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // grbDieukien
            // 
            this.grbDieukien.Controls.Add(this.datDieukien);
            this.grbDieukien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDieukien.Location = new System.Drawing.Point(384, 13);
            this.grbDieukien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Name = "grbDieukien";
            this.grbDieukien.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Size = new System.Drawing.Size(533, 154);
            this.grbDieukien.TabIndex = 18;
            this.grbDieukien.TabStop = false;
            this.grbDieukien.Text = "List of conditions";
            // 
            // cmdNornal
            // 
            this.cmdNornal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNornal.Location = new System.Drawing.Point(922, 53);
            this.cmdNornal.Name = "cmdNornal";
            this.cmdNornal.Size = new System.Drawing.Size(80, 30);
            this.cmdNornal.TabIndex = 8;
            this.cmdNornal.Text = "&Normal";
            this.cmdNornal.UseVisualStyleBackColor = true;
            this.cmdNornal.Click += new System.EventHandler(this.cmdNornal_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(922, 20);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 7;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(752, 571);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 10;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdIQS
            // 
            this.cmdIQS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIQS.Location = new System.Drawing.Point(488, 571);
            this.cmdIQS.Name = "cmdIQS";
            this.cmdIQS.Size = new System.Drawing.Size(80, 30);
            this.cmdIQS.TabIndex = 11;
            this.cmdIQS.Text = "&IQS Msg";
            this.toolTip1.SetToolTip(this.cmdIQS, "IQS Message !");
            this.cmdIQS.UseVisualStyleBackColor = true;
            this.cmdIQS.Visible = false;
            this.cmdIQS.Click += new System.EventHandler(this.cmdIQS_Click);
            // 
            // cmdview
            // 
            this.cmdview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdview.Location = new System.Drawing.Point(837, 571);
            this.cmdview.Name = "cmdview";
            this.cmdview.Size = new System.Drawing.Size(80, 30);
            this.cmdview.TabIndex = 12;
            this.cmdview.Text = "&View";
            this.cmdview.UseVisualStyleBackColor = true;
            this.cmdview.Click += new System.EventHandler(this.cmdview_Click);
            // 
            // dataMessage
            // 
            this.dataMessage.AllowUserToAddRows = false;
            this.dataMessage.AllowUserToDeleteRows = false;
            this.dataMessage.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataMessage.ColumnHeadersHeight = 21;
            this.dataMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataMessage.Location = new System.Drawing.Point(12, 210);
            this.dataMessage.Name = "dataMessage";
            this.dataMessage.RowHeadersWidth = 30;
            this.dataMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMessage.Size = new System.Drawing.Size(986, 355);
            this.dataMessage.TabIndex = 200;
            this.dataMessage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellDoubleClick);
            this.dataMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataMessage_MouseMove);
            this.dataMessage.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataMessage_ColumnHeaderMouseClick);
            this.dataMessage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellClick);
            this.dataMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataMessage_KeyDown);
            this.dataMessage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellEnter);
            // 
            // Lbtong
            // 
            this.Lbtong.AutoSize = true;
            this.Lbtong.ForeColor = System.Drawing.Color.Red;
            this.Lbtong.Location = new System.Drawing.Point(193, 188);
            this.Lbtong.Name = "Lbtong";
            this.Lbtong.Size = new System.Drawing.Size(0, 16);
            this.Lbtong.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(9, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 16);
            this.label12.TabIndex = 10;
            this.label12.Text = "Total number of messages :";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "Column1";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 10;
            this.toolTip1.ToolTipTitle = "Create";
            // 
            // cmdRelease
            // 
            this.cmdRelease.Location = new System.Drawing.Point(753, 658);
            this.cmdRelease.Name = "cmdRelease";
            this.cmdRelease.Size = new System.Drawing.Size(80, 30);
            this.cmdRelease.TabIndex = 201;
            this.cmdRelease.Text = "&Release";
            this.cmdRelease.UseVisualStyleBackColor = true;
            this.cmdRelease.Click += new System.EventHandler(this.cmdRelease_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(547, 676);
            this.dateTimePicker1.MaxDate = new System.DateTime(1983, 6, 20, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(1983, 6, 20, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker1.TabIndex = 202;
            this.dateTimePicker1.Value = new System.DateTime(1983, 6, 20, 0, 0, 0, 0);
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(669, 571);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(80, 30);
            this.cmdExport.TabIndex = 9;
            this.cmdExport.Text = "&Exp Excel";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cbCheck
            // 
            this.cbCheck.FormattingEnabled = true;
            this.cbCheck.Location = new System.Drawing.Point(44, 673);
            this.cbCheck.Name = "cbCheck";
            this.cbCheck.Size = new System.Drawing.Size(121, 24);
            this.cbCheck.TabIndex = 203;
            // 
            // frmTTSPMsgList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 621);
            this.Controls.Add(this.cbCheck);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cmdRelease);
            this.Controls.Add(this.dataMessage);
            this.Controls.Add(this.grbSearch);
            this.Controls.Add(this.grbSearchnhanh);
            this.Controls.Add(this.cmdAdvance);
            this.Controls.Add(this.grbDieukien);
            this.Controls.Add(this.cmdNornal);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdIQS);
            this.Controls.Add(this.cmdview);
            this.Controls.Add(this.Lbtong);
            this.Controls.Add(this.label12);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTTSPMsgList";
            this.Text = "TTSP Messages";
            this.Load += new System.EventHandler(this.frmTTSPMsgList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmTTSPMsgList_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTTSPMsgList_KeyDown);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.Lbtong, 0);
            this.Controls.SetChildIndex(this.cmdview, 0);
            this.Controls.SetChildIndex(this.cmdIQS, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.cmdSearch, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdNornal, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.grbDieukien, 0);
            this.Controls.SetChildIndex(this.cmdAdvance, 0);
            this.Controls.SetChildIndex(this.grbSearchnhanh, 0);
            this.Controls.SetChildIndex(this.grbSearch, 0);
            this.Controls.SetChildIndex(this.dataMessage, 0);
            this.Controls.SetChildIndex(this.cmdRelease, 0);
            this.Controls.SetChildIndex(this.dateTimePicker1, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            this.Controls.SetChildIndex(this.cbCheck, 0);
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            this.grbSearchnhanh.ResumeLayout(false);
            this.grbSearchnhanh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).EndInit();
            this.grbDieukien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ComboBox cbMsgDirection;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.TextBox txtreceiver;
        private System.Windows.Forms.TextBox txtsend;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdremove;
        private System.Windows.Forms.Button cmdremoveall;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button cmdadd1;
        private System.Windows.Forms.GroupBox grbSearchnhanh;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbColumns;
        private System.Windows.Forms.Button cmdAdvance;
        private System.Windows.Forms.DataGridView datDieukien;
        private System.Windows.Forms.GroupBox grbDieukien;
        private System.Windows.Forms.Button cmdNornal;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdIQS;
        private System.Windows.Forms.Button cmdview;
        private System.Windows.Forms.DataGridView dataMessage;
        private System.Windows.Forms.Label Lbtong;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dateValue;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRMno;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button cmdRelease;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.ComboBox cbCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}