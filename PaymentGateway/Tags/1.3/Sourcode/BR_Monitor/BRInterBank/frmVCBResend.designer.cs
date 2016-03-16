namespace BR.BRInterBank
{
    partial class frmVCBResend
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
            this.datDieukien = new System.Windows.Forms.DataGridView();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VIEW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cmdremove = new System.Windows.Forms.Button();
            this.cbMsgDirection = new System.Windows.Forms.ComboBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.cmdremoveall = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.txtreceiver = new System.Windows.Forms.TextBox();
            this.txtsend = new System.Windows.Forms.TextBox();
            this.grbSearchnhanh = new System.Windows.Forms.GroupBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.cmdAdd1 = new System.Windows.Forms.Button();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.cbColumns = new System.Windows.Forms.ComboBox();
            this.dateValue = new System.Windows.Forms.DateTimePicker();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdAdvance = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboResource = new System.Windows.Forms.ComboBox();
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbDieukien = new System.Windows.Forms.GroupBox();
            this.cmdNornal = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdview = new System.Windows.Forms.Button();
            this.dataMessage = new System.Windows.Forms.DataGridView();
            this.Lbtong = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cbCheck = new System.Windows.Forms.ComboBox();
            this.cmdResend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).BeginInit();
            this.grbSearchnhanh.SuspendLayout();
            this.grbSearch.SuspendLayout();
            this.grbDieukien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(938, 547);
            this.cmdClose.TabIndex = 16;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(386, 665);
            this.cmdSave.TabIndex = 213;
            this.cmdSave.Text = "Save";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(300, 665);
            this.cmdDelete.TabIndex = 212;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(214, 665);
            this.cmdEdit.TabIndex = 211;
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
            this.datDieukien.Location = new System.Drawing.Point(6, 21);
            this.datDieukien.Name = "datDieukien";
            this.datDieukien.RowHeadersVisible = false;
            this.datDieukien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datDieukien.Size = new System.Drawing.Size(555, 120);
            this.datDieukien.TabIndex = 23;
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
            // 
            // VALUE
            // 
            this.VALUE.HeaderText = "Value";
            this.VALUE.Name = "VALUE";
            this.VALUE.Visible = false;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(710, 53);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(160, 24);
            this.cbStatus.TabIndex = 9;
            // 
            // cmdremove
            // 
            this.cmdremove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremove.Location = new System.Drawing.Point(253, 70);
            this.cmdremove.Name = "cmdremove";
            this.cmdremove.Size = new System.Drawing.Size(49, 23);
            this.cmdremove.TabIndex = 21;
            this.cmdremove.Text = "<";
            this.cmdremove.UseVisualStyleBackColor = true;
            this.cmdremove.Click += new System.EventHandler(this.cmdremove_Click);
            // 
            // cbMsgDirection
            // 
            this.cbMsgDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsgDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMsgDirection.FormattingEnabled = true;
            this.cbMsgDirection.Location = new System.Drawing.Point(710, 23);
            this.cbMsgDirection.Name = "cbMsgDirection";
            this.cbMsgDirection.Size = new System.Drawing.Size(160, 24);
            this.cbMsgDirection.TabIndex = 8;
            this.cbMsgDirection.SelectedIndexChanged += new System.EventHandler(this.cbMsgDirection_SelectedIndexChanged);
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(710, 84);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(160, 24);
            this.cbdepartment.TabIndex = 10;
            // 
            // cmdremoveall
            // 
            this.cmdremoveall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremoveall.Location = new System.Drawing.Point(253, 100);
            this.cmdremoveall.Name = "cmdremoveall";
            this.cmdremoveall.Size = new System.Drawing.Size(49, 23);
            this.cmdremoveall.TabIndex = 22;
            this.cmdremoveall.Text = "<<";
            this.cmdremoveall.UseVisualStyleBackColor = true;
            this.cmdremoveall.Click += new System.EventHandler(this.cmdremoveall_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(27, 100);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(191, 23);
            this.txtValue.TabIndex = 19;
            this.txtValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(398, 54);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(163, 23);
            this.txtAmount.TabIndex = 5;
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrefno.Location = new System.Drawing.Point(398, 24);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(163, 23);
            this.txtrefno.TabIndex = 4;
            // 
            // txtreceiver
            // 
            this.txtreceiver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiver.Location = new System.Drawing.Point(89, 115);
            this.txtreceiver.Name = "txtreceiver";
            this.txtreceiver.Size = new System.Drawing.Size(163, 23);
            this.txtreceiver.TabIndex = 3;
            // 
            // txtsend
            // 
            this.txtsend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsend.Location = new System.Drawing.Point(89, 85);
            this.txtsend.Name = "txtsend";
            this.txtsend.Size = new System.Drawing.Size(163, 23);
            this.txtsend.TabIndex = 2;
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
            this.grbSearchnhanh.Location = new System.Drawing.Point(12, 10);
            this.grbSearchnhanh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Name = "grbSearchnhanh";
            this.grbSearchnhanh.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Size = new System.Drawing.Size(337, 154);
            this.grbSearchnhanh.TabIndex = 217;
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
            this.cboStatus.Location = new System.Drawing.Point(27, 100);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(191, 24);
            this.cboStatus.TabIndex = 19;
            // 
            // cmdAdd1
            // 
            this.cmdAdd1.Location = new System.Drawing.Point(253, 40);
            this.cmdAdd1.Name = "cmdAdd1";
            this.cmdAdd1.Size = new System.Drawing.Size(49, 23);
            this.cmdAdd1.TabIndex = 20;
            this.cmdAdd1.Text = ">";
            this.cmdAdd1.UseVisualStyleBackColor = true;
            this.cmdAdd1.Click += new System.EventHandler(this.cmdAdd1_Click);
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(27, 69);
            this.cbOperator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(191, 24);
            this.cbOperator.TabIndex = 18;
            // 
            // cbColumns
            // 
            this.cbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbColumns.FormattingEnabled = true;
            this.cbColumns.Location = new System.Drawing.Point(27, 39);
            this.cbColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(191, 24);
            this.cbColumns.TabIndex = 17;
            this.cbColumns.SelectedIndexChanged += new System.EventHandler(this.cbbAmount_SelectedIndexChanged);
            // 
            // dateValue
            // 
            this.dateValue.CustomFormat = "dd/MM/yyyyy";
            this.dateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue.Location = new System.Drawing.Point(27, 129);
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(191, 23);
            this.dateValue.TabIndex = 19;
            this.dateValue.Visible = false;
            // 
            // dateto
            // 
            this.dateto.Checked = false;
            this.dateto.CustomFormat = "dd/MM/yyyyy";
            this.dateto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(89, 54);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(163, 23);
            this.dateto.TabIndex = 1;
            this.dateto.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // datefrom
            // 
            this.datefrom.Checked = false;
            this.datefrom.CustomFormat = "dd/MM/yyyyy";
            this.datefrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(89, 24);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(163, 23);
            this.datefrom.TabIndex = 0;
            this.datefrom.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(607, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 210;
            this.label8.Text = "Module :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 203;
            this.label4.Text = "Receiver :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(607, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 16);
            this.label11.TabIndex = 209;
            this.label11.Text = "Status :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 202;
            this.label3.Text = "Sender :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(607, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 16);
            this.label9.TabIndex = 208;
            this.label9.Text = "Msg direction :";
            // 
            // cmdAdvance
            // 
            this.cmdAdvance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdvance.Location = new System.Drawing.Point(938, 103);
            this.cmdAdvance.Name = "cmdAdvance";
            this.cmdAdvance.Size = new System.Drawing.Size(80, 30);
            this.cmdAdvance.TabIndex = 12;
            this.cmdAdvance.Text = "&Advance";
            this.cmdAdvance.UseVisualStyleBackColor = true;
            this.cmdAdvance.Click += new System.EventHandler(this.cmdAdvance_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(305, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 205;
            this.label6.Text = "Amount :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(305, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 204;
            this.label5.Text = "Ref no :";
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.cbCurrency);
            this.grbSearch.Controls.Add(this.label7);
            this.grbSearch.Controls.Add(this.cbStatus);
            this.grbSearch.Controls.Add(this.cbMsgDirection);
            this.grbSearch.Controls.Add(this.cboResource);
            this.grbSearch.Controls.Add(this.cbdepartment);
            this.grbSearch.Controls.Add(this.txtMsg_type);
            this.grbSearch.Controls.Add(this.txtAmount);
            this.grbSearch.Controls.Add(this.txtrefno);
            this.grbSearch.Controls.Add(this.txtreceiver);
            this.grbSearch.Controls.Add(this.txtsend);
            this.grbSearch.Controls.Add(this.dateto);
            this.grbSearch.Controls.Add(this.label10);
            this.grbSearch.Controls.Add(this.datefrom);
            this.grbSearch.Controls.Add(this.label13);
            this.grbSearch.Controls.Add(this.label8);
            this.grbSearch.Controls.Add(this.label4);
            this.grbSearch.Controls.Add(this.label11);
            this.grbSearch.Controls.Add(this.label3);
            this.grbSearch.Controls.Add(this.label9);
            this.grbSearch.Controls.Add(this.label6);
            this.grbSearch.Controls.Add(this.label5);
            this.grbSearch.Controls.Add(this.label2);
            this.grbSearch.Controls.Add(this.label1);
            this.grbSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearch.Location = new System.Drawing.Point(106, 678);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(902, 154);
            this.grbSearch.TabIndex = 214;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Searching condition";
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(398, 84);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(163, 24);
            this.cbCurrency.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(305, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 206;
            this.label7.Text = "Currency :";
            // 
            // cboResource
            // 
            this.cboResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResource.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboResource.FormattingEnabled = true;
            this.cboResource.Location = new System.Drawing.Point(710, 114);
            this.cboResource.Name = "cboResource";
            this.cboResource.Size = new System.Drawing.Size(160, 24);
            this.cboResource.TabIndex = 10;
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg_type.Location = new System.Drawing.Point(398, 115);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(163, 23);
            this.txtMsg_type.TabIndex = 7;
            this.txtMsg_type.TextChanged += new System.EventHandler(this.txtMsg_type_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(305, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 207;
            this.label10.Text = "Msg type :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(607, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 16);
            this.label13.TabIndex = 210;
            this.label13.Text = "Msg source :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 201;
            this.label2.Text = "To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 200;
            this.label1.Text = "From :";
            // 
            // grbDieukien
            // 
            this.grbDieukien.Controls.Add(this.datDieukien);
            this.grbDieukien.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDieukien.Location = new System.Drawing.Point(360, 13);
            this.grbDieukien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Name = "grbDieukien";
            this.grbDieukien.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Size = new System.Drawing.Size(572, 154);
            this.grbDieukien.TabIndex = 218;
            this.grbDieukien.TabStop = false;
            this.grbDieukien.Text = "List of conditions";
            // 
            // cmdNornal
            // 
            this.cmdNornal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNornal.Location = new System.Drawing.Point(938, 54);
            this.cmdNornal.Name = "cmdNornal";
            this.cmdNornal.Size = new System.Drawing.Size(80, 30);
            this.cmdNornal.TabIndex = 9;
            this.cmdNornal.Text = "&Normal";
            this.cmdNornal.UseVisualStyleBackColor = true;
            this.cmdNornal.Click += new System.EventHandler(this.cmdNornal_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(938, 20);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 11;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(772, 547);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 14;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdview
            // 
            this.cmdview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdview.Location = new System.Drawing.Point(856, 547);
            this.cmdview.Name = "cmdview";
            this.cmdview.Size = new System.Drawing.Size(80, 30);
            this.cmdview.TabIndex = 15;
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
            this.dataMessage.Location = new System.Drawing.Point(12, 194);
            this.dataMessage.MultiSelect = false;
            this.dataMessage.Name = "dataMessage";
            this.dataMessage.RowHeadersWidth = 30;
            this.dataMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMessage.Size = new System.Drawing.Size(1006, 347);
            this.dataMessage.TabIndex = 90;
            this.dataMessage.TabStop = false;
            this.dataMessage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellDoubleClick);
            this.dataMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataMessage_MouseMove);
            this.dataMessage.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataMessage_ColumnHeaderMouseClick);
            this.dataMessage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellClick);
            this.dataMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataMessage_KeyDown);
            this.dataMessage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellEnter);
            this.dataMessage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellContentClick);
            // 
            // Lbtong
            // 
            this.Lbtong.AutoSize = true;
            this.Lbtong.ForeColor = System.Drawing.Color.Blue;
            this.Lbtong.Location = new System.Drawing.Point(182, 175);
            this.Lbtong.Name = "Lbtong";
            this.Lbtong.Size = new System.Drawing.Size(0, 16);
            this.Lbtong.TabIndex = 216;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(12, 175);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 16);
            this.label12.TabIndex = 215;
            this.label12.Text = "Total number of messages :";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Create";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "SELECT";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(689, 547);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(80, 30);
            this.cmdExport.TabIndex = 13;
            this.cmdExport.Text = "&Exp Excel";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cbCheck
            // 
            this.cbCheck.FormattingEnabled = true;
            this.cbCheck.Location = new System.Drawing.Point(733, 678);
            this.cbCheck.Name = "cbCheck";
            this.cbCheck.Size = new System.Drawing.Size(121, 24);
            this.cbCheck.TabIndex = 219;
            // 
            // cmdResend
            // 
            this.cmdResend.Location = new System.Drawing.Point(605, 547);
            this.cmdResend.Name = "cmdResend";
            this.cmdResend.Size = new System.Drawing.Size(80, 30);
            this.cmdResend.TabIndex = 12;
            this.cmdResend.Text = "&Resend";
            this.cmdResend.UseVisualStyleBackColor = true;
            this.cmdResend.Click += new System.EventHandler(this.cmdResend_Click);
            // 
            // frmVCBResend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 594);
            this.Controls.Add(this.cbCheck);
            this.Controls.Add(this.grbSearchnhanh);
            this.Controls.Add(this.cmdAdvance);
            this.Controls.Add(this.grbDieukien);
            this.Controls.Add(this.cmdResend);
            this.Controls.Add(this.cmdNornal);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.cmdview);
            this.Controls.Add(this.dataMessage);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.Lbtong);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.grbSearch);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmVCBResend";
            this.Text = "VCB resend message";
            this.Load += new System.EventHandler(this.frmVCBResend_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVCBResend_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmVCBResend_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVCBResend_KeyDown);
            this.Controls.SetChildIndex(this.grbSearch, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.Lbtong, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            this.Controls.SetChildIndex(this.dataMessage, 0);
            this.Controls.SetChildIndex(this.cmdview, 0);
            this.Controls.SetChildIndex(this.cmdSearch, 0);
            this.Controls.SetChildIndex(this.cmdNornal, 0);
            this.Controls.SetChildIndex(this.cmdResend, 0);
            this.Controls.SetChildIndex(this.grbDieukien, 0);
            this.Controls.SetChildIndex(this.cmdAdvance, 0);
            this.Controls.SetChildIndex(this.grbSearchnhanh, 0);
            this.Controls.SetChildIndex(this.cbCheck, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).EndInit();
            this.grbSearchnhanh.ResumeLayout(false);
            this.grbSearchnhanh.PerformLayout();
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            this.grbDieukien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView datDieukien;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Button cmdremove;
        private System.Windows.Forms.ComboBox cbMsgDirection;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.Button cmdremoveall;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtAmount;
        //private System.Windows.Forms.Button cmdadd;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.TextBox txtreceiver;
        private System.Windows.Forms.TextBox txtsend;
        private System.Windows.Forms.GroupBox grbSearchnhanh;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbColumns;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cmdAdvance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbDieukien;
        private System.Windows.Forms.Button cmdNornal;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdview;
        private System.Windows.Forms.DataGridView dataMessage;
        private System.Windows.Forms.Label Lbtong;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cmdAdd1;
        private System.Windows.Forms.DateTimePicker dateValue;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboResource;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.ComboBox cbCheck;
        private System.Windows.Forms.Button cmdResend;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIEW;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE;
    }
}