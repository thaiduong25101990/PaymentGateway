namespace BR.BRIBPS
{
    partial class frmIBPSMsgList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label12 = new System.Windows.Forms.Label();
            this.cmdview = new System.Windows.Forms.Button();
            this.cmdIQS = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.cmdNornal = new System.Windows.Forms.Button();
            this.grbDieukien = new System.Windows.Forms.GroupBox();
            this.datDieukien = new System.Windows.Forms.DataGridView();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VIEW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbSearchnhanh = new System.Windows.Forms.GroupBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.cmdAdd1 = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmdremoveall = new System.Windows.Forms.Button();
            this.cmdremove = new System.Windows.Forms.Button();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.cbColumns = new System.Windows.Forms.ComboBox();
            this.dateValue = new System.Windows.Forms.DateTimePicker();
            this.cmdAdvance = new System.Windows.Forms.Button();
            this.Lbtong = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.txtTrans_num = new System.Windows.Forms.TextBox();
            this.cbHV_LV = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSource_branch = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboFWStatus = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbMsgDirection = new System.Windows.Forms.ComboBox();
            this.cboMSG_SRC = new System.Windows.Forms.ComboBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cbtab = new System.Windows.Forms.ComboBox();
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
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataMessage = new System.Windows.Forms.DataGridView();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cbCheck = new System.Windows.Forms.ComboBox();
            this.grbDieukien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).BeginInit();
            this.grbSearchnhanh.SuspendLayout();
            this.grbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(923, 593);
            this.cmdClose.TabIndex = 19;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(473, 341);
            this.cmdSave.TabIndex = 123;
            this.cmdSave.Text = "Save";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(387, 341);
            this.cmdDelete.TabIndex = 121;
            this.cmdDelete.Text = "Delete";
            // 
            // cmdAdd
            // 
            this.cmdAdd.TabIndex = 120;
            this.cmdAdd.Text = "Add";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(298, 341);
            this.cmdEdit.TabIndex = 122;
            this.cmdEdit.Text = "Edit";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(12, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 16);
            this.label12.TabIndex = 108;
            this.label12.Text = "Total number of messages :";
            // 
            // cmdview
            // 
            this.cmdview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdview.Location = new System.Drawing.Point(840, 593);
            this.cmdview.Name = "cmdview";
            this.cmdview.Size = new System.Drawing.Size(80, 30);
            this.cmdview.TabIndex = 18;
            this.cmdview.Text = "&View";
            this.cmdview.UseVisualStyleBackColor = true;
            this.cmdview.Click += new System.EventHandler(this.cmdview_Click);
            // 
            // cmdIQS
            // 
            this.cmdIQS.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIQS.Location = new System.Drawing.Point(588, 593);
            this.cmdIQS.Name = "cmdIQS";
            this.cmdIQS.Size = new System.Drawing.Size(80, 30);
            this.cmdIQS.TabIndex = 17;
            this.cmdIQS.Text = "IQS &Msg";
            this.toolTip1.SetToolTip(this.cmdIQS, "Create IQS message !");
            this.cmdIQS.UseVisualStyleBackColor = true;
            this.cmdIQS.Visible = false;
            this.cmdIQS.Click += new System.EventHandler(this.cmdIQS_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(757, 593);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 16;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(922, 19);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 13;
            this.cmdSearch.Text = "Sea&rch";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.Search_Click);
            // 
            // cmdNornal
            // 
            this.cmdNornal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNornal.Location = new System.Drawing.Point(922, 53);
            this.cmdNornal.Name = "cmdNornal";
            this.cmdNornal.Size = new System.Drawing.Size(80, 30);
            this.cmdNornal.TabIndex = 14;
            this.cmdNornal.Text = "&Normal";
            this.cmdNornal.UseVisualStyleBackColor = true;
            this.cmdNornal.Click += new System.EventHandler(this.cmdNornal_Click);
            // 
            // grbDieukien
            // 
            this.grbDieukien.Controls.Add(this.datDieukien);
            this.grbDieukien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDieukien.Location = new System.Drawing.Point(346, 13);
            this.grbDieukien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Name = "grbDieukien";
            this.grbDieukien.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbDieukien.Size = new System.Drawing.Size(570, 154);
            this.grbDieukien.TabIndex = 100;
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
            this.datDieukien.Location = new System.Drawing.Point(6, 25);
            this.datDieukien.Name = "datDieukien";
            this.datDieukien.RowHeadersVisible = false;
            this.datDieukien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datDieukien.Size = new System.Drawing.Size(556, 120);
            this.datDieukien.TabIndex = 7;
            this.datDieukien.MouseMove += new System.Windows.Forms.MouseEventHandler(this.datDieukien_MouseMove);
            // 
            // CheckBox
            // 
            this.CheckBox.HeaderText = "CheckBox";
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            this.grbSearchnhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearchnhanh.Location = new System.Drawing.Point(13, 13);
            this.grbSearchnhanh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Name = "grbSearchnhanh";
            this.grbSearchnhanh.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbSearchnhanh.Size = new System.Drawing.Size(327, 154);
            this.grbSearchnhanh.TabIndex = 101;
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
            this.cboStatus.Location = new System.Drawing.Point(27, 92);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(191, 24);
            this.cboStatus.TabIndex = 3;
            // 
            // cmdAdd1
            // 
            this.cmdAdd1.Location = new System.Drawing.Point(252, 29);
            this.cmdAdd1.Name = "cmdAdd1";
            this.cmdAdd1.Size = new System.Drawing.Size(49, 23);
            this.cmdAdd1.TabIndex = 4;
            this.cmdAdd1.Text = ">";
            this.cmdAdd1.UseVisualStyleBackColor = true;
            this.cmdAdd1.Click += new System.EventHandler(this.cmdAdd1_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(27, 92);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(191, 22);
            this.txtValue.TabIndex = 3;
            this.txtValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            // 
            // cmdremoveall
            // 
            this.cmdremoveall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremoveall.Location = new System.Drawing.Point(252, 91);
            this.cmdremoveall.Name = "cmdremoveall";
            this.cmdremoveall.Size = new System.Drawing.Size(49, 23);
            this.cmdremoveall.TabIndex = 6;
            this.cmdremoveall.Text = "<<";
            this.cmdremoveall.UseVisualStyleBackColor = true;
            this.cmdremoveall.Click += new System.EventHandler(this.cmdremoveall_Click);
            // 
            // cmdremove
            // 
            this.cmdremove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdremove.Location = new System.Drawing.Point(252, 61);
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
            this.cbOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(27, 61);
            this.cbOperator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(191, 24);
            this.cbOperator.TabIndex = 2;
            // 
            // cbColumns
            // 
            this.cbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbColumns.FormattingEnabled = true;
            this.cbColumns.Location = new System.Drawing.Point(27, 29);
            this.cbColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(191, 24);
            this.cbColumns.TabIndex = 1;
            this.cbColumns.SelectedIndexChanged += new System.EventHandler(this.cbbAmount_SelectedIndexChanged);
            // 
            // dateValue
            // 
            this.dateValue.CustomFormat = "dd/MM/yyyyy";
            this.dateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue.Location = new System.Drawing.Point(27, 116);
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(191, 22);
            this.dateValue.TabIndex = 3;
            this.dateValue.Visible = false;
            // 
            // cmdAdvance
            // 
            this.cmdAdvance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdvance.Location = new System.Drawing.Point(923, 97);
            this.cmdAdvance.Name = "cmdAdvance";
            this.cmdAdvance.Size = new System.Drawing.Size(80, 30);
            this.cmdAdvance.TabIndex = 15;
            this.cmdAdvance.Text = "&Advance";
            this.cmdAdvance.UseVisualStyleBackColor = true;
            this.cmdAdvance.Click += new System.EventHandler(this.cmdAdvance_Click);
            // 
            // Lbtong
            // 
            this.Lbtong.AutoSize = true;
            this.Lbtong.ForeColor = System.Drawing.Color.Blue;
            this.Lbtong.Location = new System.Drawing.Point(182, 195);
            this.Lbtong.Name = "Lbtong";
            this.Lbtong.Size = new System.Drawing.Size(0, 16);
            this.Lbtong.TabIndex = 103;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 10;
            this.toolTip1.ToolTipTitle = "Create";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.NullValue = false;
            this.dataGridViewCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 60;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.Frozen = true;
            this.dataGridViewCheckBoxColumn2.HeaderText = "Column1";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn2.ThreeState = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.txtTrans_num);
            this.grbSearch.Controls.Add(this.cbHV_LV);
            this.grbSearch.Controls.Add(this.label15);
            this.grbSearch.Controls.Add(this.txtSource_branch);
            this.grbSearch.Controls.Add(this.label17);
            this.grbSearch.Controls.Add(this.label16);
            this.grbSearch.Controls.Add(this.label14);
            this.grbSearch.Controls.Add(this.cboFWStatus);
            this.grbSearch.Controls.Add(this.label13);
            this.grbSearch.Controls.Add(this.cbCurrency);
            this.grbSearch.Controls.Add(this.label7);
            this.grbSearch.Controls.Add(this.cbMsgDirection);
            this.grbSearch.Controls.Add(this.cboMSG_SRC);
            this.grbSearch.Controls.Add(this.cbStatus);
            this.grbSearch.Controls.Add(this.cbtab);
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
            this.grbSearch.Controls.Add(this.label10);
            this.grbSearch.Controls.Add(this.label9);
            this.grbSearch.Controls.Add(this.label6);
            this.grbSearch.Controls.Add(this.label5);
            this.grbSearch.Controls.Add(this.label2);
            this.grbSearch.Controls.Add(this.label1);
            this.grbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearch.Location = new System.Drawing.Point(40, 721);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(904, 175);
            this.grbSearch.TabIndex = 104;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Searching condition";
            // 
            // txtTrans_num
            // 
            this.txtTrans_num.Location = new System.Drawing.Point(716, 69);
            this.txtTrans_num.Name = "txtTrans_num";
            this.txtTrans_num.Size = new System.Drawing.Size(169, 22);
            this.txtTrans_num.TabIndex = 13;
            // 
            // cbHV_LV
            // 
            this.cbHV_LV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHV_LV.FormattingEnabled = true;
            this.cbHV_LV.Items.AddRange(new object[] {
            "ALL",
            "HV",
            "LV"});
            this.cbHV_LV.Location = new System.Drawing.Point(413, 94);
            this.cbHV_LV.Name = "cbHV_LV";
            this.cbHV_LV.Size = new System.Drawing.Size(163, 24);
            this.cbHV_LV.TabIndex = 121;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(293, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 16);
            this.label15.TabIndex = 120;
            this.label15.Text = "Transaction code :";
            // 
            // txtSource_branch
            // 
            this.txtSource_branch.Location = new System.Drawing.Point(716, 43);
            this.txtSource_branch.Name = "txtSource_branch";
            this.txtSource_branch.Size = new System.Drawing.Size(169, 22);
            this.txtSource_branch.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(609, 101);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 16);
            this.label17.TabIndex = 119;
            this.label17.Text = "Msg source:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(609, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 16);
            this.label16.TabIndex = 119;
            this.label16.Text = "Relation number:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(609, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(105, 16);
            this.label14.TabIndex = 119;
            this.label14.Text = "Source_branch :";
            // 
            // cboFWStatus
            // 
            this.cboFWStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFWStatus.FormattingEnabled = true;
            this.cboFWStatus.Location = new System.Drawing.Point(716, 17);
            this.cboFWStatus.Name = "cboFWStatus";
            this.cboFWStatus.Size = new System.Drawing.Size(169, 24);
            this.cboFWStatus.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(609, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 16);
            this.label13.TabIndex = 117;
            this.label13.Text = "Forward status :";
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(413, 42);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(163, 24);
            this.cbCurrency.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 114;
            this.label7.Text = "Currency :";
            // 
            // cbMsgDirection
            // 
            this.cbMsgDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMsgDirection.FormattingEnabled = true;
            this.cbMsgDirection.Location = new System.Drawing.Point(413, 119);
            this.cbMsgDirection.Name = "cbMsgDirection";
            this.cbMsgDirection.Size = new System.Drawing.Size(163, 24);
            this.cbMsgDirection.TabIndex = 9;
            // 
            // cboMSG_SRC
            // 
            this.cboMSG_SRC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMSG_SRC.FormattingEnabled = true;
            this.cboMSG_SRC.Location = new System.Drawing.Point(716, 95);
            this.cboMSG_SRC.Name = "cboMSG_SRC";
            this.cboMSG_SRC.Size = new System.Drawing.Size(169, 24);
            this.cboMSG_SRC.TabIndex = 14;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(413, 145);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(163, 24);
            this.cbStatus.TabIndex = 10;
            // 
            // cbtab
            // 
            this.cbtab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbtab.FormattingEnabled = true;
            this.cbtab.Location = new System.Drawing.Point(98, 119);
            this.cbtab.Name = "cbtab";
            this.cbtab.Size = new System.Drawing.Size(163, 24);
            this.cbtab.TabIndex = 4;
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(413, 68);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(163, 24);
            this.cbdepartment.TabIndex = 8;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(413, 18);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(163, 22);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // txtrefno
            // 
            this.txtrefno.Location = new System.Drawing.Point(98, 146);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(163, 22);
            this.txtrefno.TabIndex = 5;
            // 
            // txtreceiver
            // 
            this.txtreceiver.Location = new System.Drawing.Point(98, 95);
            this.txtreceiver.Name = "txtreceiver";
            this.txtreceiver.Size = new System.Drawing.Size(163, 22);
            this.txtreceiver.TabIndex = 3;
            // 
            // txtsend
            // 
            this.txtsend.Location = new System.Drawing.Point(98, 69);
            this.txtsend.Name = "txtsend";
            this.txtsend.Size = new System.Drawing.Size(163, 22);
            this.txtsend.TabIndex = 2;
            // 
            // dateto
            // 
            this.dateto.Checked = false;
            this.dateto.CustomFormat = "dd/MM/yyyyy";
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(98, 43);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(163, 22);
            this.dateto.TabIndex = 1;
            this.dateto.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // datefrom
            // 
            this.datefrom.Checked = false;
            this.datefrom.CustomFormat = "dd/MM/yyyyy";
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(98, 18);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(163, 22);
            this.datefrom.TabIndex = 0;
            this.datefrom.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(293, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 115;
            this.label8.Text = "Module :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 111;
            this.label4.Text = "Receiver :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(293, 149);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 107;
            this.label11.Text = "Status :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 110;
            this.label3.Text = "Sender :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 16);
            this.label10.TabIndex = 106;
            this.label10.Text = "Tad :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(293, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 16);
            this.label9.TabIndex = 116;
            this.label9.Text = "Msg direction :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 113;
            this.label6.Text = "Amount :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 112;
            this.label5.Text = "RM no :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 109;
            this.label2.Text = "To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 105;
            this.label1.Text = "From :";
            // 
            // dataMessage
            // 
            this.dataMessage.AllowUserToAddRows = false;
            this.dataMessage.AllowUserToDeleteRows = false;
            this.dataMessage.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMessage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataMessage.ColumnHeadersHeight = 22;
            this.dataMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataMessage.Location = new System.Drawing.Point(15, 214);
            this.dataMessage.Name = "dataMessage";
            this.dataMessage.RowHeadersWidth = 30;
            this.dataMessage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMessage.Size = new System.Drawing.Size(987, 373);
            this.dataMessage.TabIndex = 152;
            this.dataMessage.TabStop = false;
            this.dataMessage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataMessage_MouseMove);
            this.dataMessage.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataMessage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataMessage_KeyDown);
            this.dataMessage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataMessage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMessage_CellContentClick);
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(674, 593);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(80, 30);
            this.cmdExport.TabIndex = 15;
            this.cmdExport.Text = "&Exp Excel";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cbCheck
            // 
            this.cbCheck.FormattingEnabled = true;
            this.cbCheck.Location = new System.Drawing.Point(891, 768);
            this.cbCheck.Name = "cbCheck";
            this.cbCheck.Size = new System.Drawing.Size(121, 24);
            this.cbCheck.TabIndex = 153;
            // 
            // frmIBPSMsgList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(1014, 630);
            this.Controls.Add(this.cbCheck);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.dataMessage);
            this.Controls.Add(this.cmdAdvance);
            this.Controls.Add(this.grbDieukien);
            this.Controls.Add(this.grbSearchnhanh);
            this.Controls.Add(this.cmdNornal);
            this.Controls.Add(this.grbSearch);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.cmdIQS);
            this.Controls.Add(this.Lbtong);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmdview);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmIBPSMsgList";
            this.Text = "IBPS message management";
            this.Load += new System.EventHandler(this.frmIBPSMsgList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIBPSMsgList_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIBPSMsgList_KeyDown);
            this.Controls.SetChildIndex(this.cmdview, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.Lbtong, 0);
            this.Controls.SetChildIndex(this.cmdIQS, 0);
            this.Controls.SetChildIndex(this.cmdSearch, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.grbSearch, 0);
            this.Controls.SetChildIndex(this.cmdNornal, 0);
            this.Controls.SetChildIndex(this.grbSearchnhanh, 0);
            this.Controls.SetChildIndex(this.grbDieukien, 0);
            this.Controls.SetChildIndex(this.cmdAdvance, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.dataMessage, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            this.Controls.SetChildIndex(this.cbCheck, 0);
            this.grbDieukien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).EndInit();
            this.grbSearchnhanh.ResumeLayout(false);
            this.grbSearchnhanh.PerformLayout();
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cmdview;
        private System.Windows.Forms.Button cmdIQS;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Button cmdNornal;
        private System.Windows.Forms.GroupBox grbDieukien;
        private System.Windows.Forms.DataGridView datDieukien;
        private System.Windows.Forms.GroupBox grbSearchnhanh;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button cmdremoveall;
        private System.Windows.Forms.Button cmdremove;
        //private System.Windows.Forms.Button cmdadd;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbColumns;
        private System.Windows.Forms.Button cmdAdvance;
        private System.Windows.Forms.Label Lbtong;
        private System.Windows.Forms.Button cmdAdd1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DateTimePicker dateValue;
        public System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbMsgDirection;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ComboBox cbtab;
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource_branch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboFWStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataMessage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbHV_LV;
        private System.Windows.Forms.TextBox txtTrans_num;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.ComboBox cbCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIEW;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboMSG_SRC;
    }
}