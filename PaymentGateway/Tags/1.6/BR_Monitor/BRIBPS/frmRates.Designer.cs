namespace BR.BRIBPS
{
    partial class frmRates
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbFeeType1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.cbFeeBranchType = new System.Windows.Forms.ComboBox();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCCYCD = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbTransType = new System.Windows.Forms.ComboBox();
            this.cbFeeDiscType = new System.Windows.Forms.ComboBox();
            this.dtFeeDiscTime = new System.Windows.Forms.DateTimePicker();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtFixed = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbFeeType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmdFeeCal = new System.Windows.Forms.Button();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANS_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEEDISC_TYPE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEEDISC_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEEDISC_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCYCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIXED_FEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATE_FEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MIN_FEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAX_FEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdCalDetail = new System.Windows.Forms.Button();
            this.cmdCalExcel = new System.Windows.Forms.Button();
            this.cbCCYCD1 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(632, 493);
            this.cmdClose.TabIndex = 16;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(288, 493);
            this.cmdSave.TabIndex = 14;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(334, 547);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(116, 493);
            this.cmdAdd.TabIndex = 12;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(202, 493);
            this.cmdEdit.TabIndex = 13;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cbCCYCD1);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cbFeeType1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbBranch);
            this.groupBox1.Controls.Add(this.cbFeeBranchType);
            this.groupBox1.Controls.Add(this.dtToDate);
            this.groupBox1.Controls.Add(this.dtFromDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(11, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 16);
            this.label14.TabIndex = 15;
            this.label14.Text = "Fee type: ";
            // 
            // cbFeeType1
            // 
            this.cbFeeType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeeType1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFeeType1.FormattingEnabled = true;
            this.cbFeeType1.Location = new System.Drawing.Point(150, 75);
            this.cbFeeType1.Name = "cbFeeType1";
            this.cbFeeType1.Size = new System.Drawing.Size(187, 24);
            this.cbFeeType1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(360, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Branch:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(395, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Branch Type:";
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(423, 47);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(265, 24);
            this.cbBranch.TabIndex = 11;
            this.cbBranch.SelectedIndexChanged += new System.EventHandler(this.cbBranch_SelectedIndexChanged);
            // 
            // cbFeeBranchType
            // 
            this.cbFeeBranchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeeBranchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFeeBranchType.FormattingEnabled = true;
            this.cbFeeBranchType.Location = new System.Drawing.Point(501, 19);
            this.cbFeeBranchType.Name = "cbFeeBranchType";
            this.cbFeeBranchType.Size = new System.Drawing.Size(187, 24);
            this.cbFeeBranchType.TabIndex = 10;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDate.Location = new System.Drawing.Point(150, 47);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(187, 22);
            this.dtToDate.TabIndex = 4;
            this.dtToDate.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDate.Location = new System.Drawing.Point(150, 19);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(187, 22);
            this.dtFromDate.TabIndex = 3;
            this.dtFromDate.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "To date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "From date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "CCYCD:";
            // 
            // cbCCYCD
            // 
            this.cbCCYCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCCYCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCCYCD.FormattingEnabled = true;
            this.cbCCYCD.Location = new System.Drawing.Point(150, 81);
            this.cbCCYCD.Name = "cbCCYCD";
            this.cbCCYCD.Size = new System.Drawing.Size(187, 24);
            this.cbCCYCD.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Transaction type: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(360, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Fee discount type:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(361, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Fee discount time:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 16);
            this.label9.TabIndex = 3;
            this.label9.Text = "Fixed fee:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Rate:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(459, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 16);
            this.label11.TabIndex = 3;
            this.label11.Text = "Min:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(455, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 3;
            this.label12.Text = "Max:";
            // 
            // cbTransType
            // 
            this.cbTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTransType.FormattingEnabled = true;
            this.cbTransType.Location = new System.Drawing.Point(150, 20);
            this.cbTransType.Name = "cbTransType";
            this.cbTransType.Size = new System.Drawing.Size(187, 24);
            this.cbTransType.TabIndex = 5;
            this.cbTransType.SelectedIndexChanged += new System.EventHandler(this.cbTransType_SelectedIndexChanged);
            // 
            // cbFeeDiscType
            // 
            this.cbFeeDiscType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeeDiscType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFeeDiscType.FormattingEnabled = true;
            this.cbFeeDiscType.Location = new System.Drawing.Point(501, 20);
            this.cbFeeDiscType.Name = "cbFeeDiscType";
            this.cbFeeDiscType.Size = new System.Drawing.Size(187, 24);
            this.cbFeeDiscType.TabIndex = 6;
            this.cbFeeDiscType.SelectedIndexChanged += new System.EventHandler(this.cbFeeDiscType_SelectedIndexChanged);
            // 
            // dtFeeDiscTime
            // 
            this.dtFeeDiscTime.CustomFormat = "hh/mm/ss";
            this.dtFeeDiscTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFeeDiscTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtFeeDiscTime.Location = new System.Drawing.Point(501, 50);
            this.dtFeeDiscTime.Name = "dtFeeDiscTime";
            this.dtFeeDiscTime.ShowUpDown = true;
            this.dtFeeDiscTime.Size = new System.Drawing.Size(187, 22);
            this.dtFeeDiscTime.TabIndex = 7;
            // 
            // txtMax
            // 
            this.txtMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(501, 47);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(187, 22);
            this.txtMax.TabIndex = 11;
            this.txtMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMax_KeyPress);
            // 
            // txtMin
            // 
            this.txtMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMin.Location = new System.Drawing.Point(501, 18);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(187, 22);
            this.txtMin.TabIndex = 10;
            this.txtMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMin_KeyPress);
            // 
            // txtRate
            // 
            this.txtRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Location = new System.Drawing.Point(150, 47);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(187, 22);
            this.txtRate.TabIndex = 9;
            // 
            // txtFixed
            // 
            this.txtFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFixed.Location = new System.Drawing.Point(150, 18);
            this.txtFixed.Name = "txtFixed";
            this.txtFixed.Size = new System.Drawing.Size(187, 22);
            this.txtFixed.TabIndex = 8;
            this.txtFixed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFixed_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cbFeeType);
            this.groupBox2.Controls.Add(this.cbFeeDiscType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbCCYCD);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbTransType);
            this.groupBox2.Controls.Add(this.dtFeeDiscTime);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(708, 113);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Declare fee:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(10, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 9;
            this.label13.Text = "Fee type: ";
            // 
            // cbFeeType
            // 
            this.cbFeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFeeType.FormattingEnabled = true;
            this.cbFeeType.Location = new System.Drawing.Point(150, 50);
            this.cbFeeType.Name = "cbFeeType";
            this.cbFeeType.Size = new System.Drawing.Size(187, 24);
            this.cbFeeType.TabIndex = 8;
            this.cbFeeType.SelectedIndexChanged += new System.EventHandler(this.cbFeeType_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtID);
            this.groupBox3.Controls.Add(this.txtFixed);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtMin);
            this.groupBox3.Controls.Add(this.txtMax);
            this.groupBox3.Controls.Add(this.txtRate);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(708, 78);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Level fee:";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(422, 28);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(26, 22);
            this.txtID.TabIndex = 12;
            this.txtID.Visible = false;
            // 
            // cmdFeeCal
            // 
            this.cmdFeeCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeeCal.Location = new System.Drawing.Point(374, 493);
            this.cmdFeeCal.Name = "cmdFeeCal";
            this.cmdFeeCal.Size = new System.Drawing.Size(80, 30);
            this.cmdFeeCal.TabIndex = 15;
            this.cmdFeeCal.Text = "&Calculator";
            this.cmdFeeCal.UseVisualStyleBackColor = true;
            this.cmdFeeCal.Click += new System.EventHandler(this.cmdFeeCal_Click);
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.TRANS_TYPE,
            this.FEEDISC_TYPE1,
            this.FEEDISC_TYPE,
            this.FEEDISC_TIME,
            this.CCYCD,
            this.FIXED_FEE,
            this.RATE_FEE,
            this.MIN_FEE,
            this.MAX_FEE});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgView.Location = new System.Drawing.Point(12, 318);
            this.dgView.Name = "dgView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(708, 169);
            this.dgView.TabIndex = 29;
            this.dgView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgView_ColumnHeaderMouseClick);
            this.dgView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellClick);
            this.dgView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellEnter);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // TRANS_TYPE
            // 
            this.TRANS_TYPE.HeaderText = "TRANS TYPE";
            this.TRANS_TYPE.Name = "TRANS_TYPE";
            this.TRANS_TYPE.Visible = false;
            // 
            // FEEDISC_TYPE1
            // 
            this.FEEDISC_TYPE1.HeaderText = "FEE DISCOUNT TYPE";
            this.FEEDISC_TYPE1.Name = "FEEDISC_TYPE1";
            this.FEEDISC_TYPE1.Width = 130;
            // 
            // FEEDISC_TYPE
            // 
            this.FEEDISC_TYPE.HeaderText = "FEEDISC_TYPE";
            this.FEEDISC_TYPE.Name = "FEEDISC_TYPE";
            this.FEEDISC_TYPE.Visible = false;
            // 
            // FEEDISC_TIME
            // 
            this.FEEDISC_TIME.HeaderText = "FEE DISCOUNT TIME";
            this.FEEDISC_TIME.Name = "FEEDISC_TIME";
            this.FEEDISC_TIME.Width = 130;
            // 
            // CCYCD
            // 
            this.CCYCD.HeaderText = "CCYCD";
            this.CCYCD.Name = "CCYCD";
            this.CCYCD.Width = 60;
            // 
            // FIXED_FEE
            // 
            this.FIXED_FEE.HeaderText = "FIXED FEE";
            this.FIXED_FEE.Name = "FIXED_FEE";
            // 
            // RATE_FEE
            // 
            this.RATE_FEE.HeaderText = "RATE";
            this.RATE_FEE.Name = "RATE_FEE";
            this.RATE_FEE.Width = 50;
            // 
            // MIN_FEE
            // 
            this.MIN_FEE.HeaderText = "MIN FEE";
            this.MIN_FEE.Name = "MIN_FEE";
            this.MIN_FEE.Width = 90;
            // 
            // MAX_FEE
            // 
            this.MAX_FEE.HeaderText = "MAX FEE";
            this.MAX_FEE.Name = "MAX_FEE";
            // 
            // cmdCalDetail
            // 
            this.cmdCalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCalDetail.Location = new System.Drawing.Point(460, 493);
            this.cmdCalDetail.Name = "cmdCalDetail";
            this.cmdCalDetail.Size = new System.Drawing.Size(80, 30);
            this.cmdCalDetail.TabIndex = 30;
            this.cmdCalDetail.Text = "Cal Detail";
            this.cmdCalDetail.UseVisualStyleBackColor = true;
            this.cmdCalDetail.Click += new System.EventHandler(this.cmdCalDetail_Click);
            // 
            // cmdCalExcel
            // 
            this.cmdCalExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCalExcel.Location = new System.Drawing.Point(546, 493);
            this.cmdCalExcel.Name = "cmdCalExcel";
            this.cmdCalExcel.Size = new System.Drawing.Size(80, 30);
            this.cmdCalExcel.TabIndex = 31;
            this.cmdCalExcel.Text = "Cal Excel";
            this.cmdCalExcel.UseVisualStyleBackColor = true;
            this.cmdCalExcel.Click += new System.EventHandler(this.cmdCalExcel_Click);
            // 
            // cbCCYCD1
            // 
            this.cbCCYCD1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCCYCD1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCCYCD1.FormattingEnabled = true;
            this.cbCCYCD1.Location = new System.Drawing.Point(501, 75);
            this.cbCCYCD1.Name = "cbCCYCD1";
            this.cbCCYCD1.Size = new System.Drawing.Size(187, 24);
            this.cbCCYCD1.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(433, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 16);
            this.label15.TabIndex = 17;
            this.label15.Text = "CCYCD:";
            // 
            // frmRates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(732, 532);
            this.Controls.Add(this.cmdCalExcel);
            this.Controls.Add(this.cmdCalDetail);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.cmdFeeCal);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmRates";
            this.Text = "Free calculator";
            this.Load += new System.EventHandler(this.frmRates_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmRates_MouseMove);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.cmdFeeCal, 0);
            this.Controls.SetChildIndex(this.dgView, 0);
            this.Controls.SetChildIndex(this.cmdCalDetail, 0);
            this.Controls.SetChildIndex(this.cmdCalExcel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbTransType;
        private System.Windows.Forms.ComboBox cbFeeDiscType;
        private System.Windows.Forms.DateTimePicker dtFeeDiscTime;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.TextBox txtFixed;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdFeeCal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCCYCD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.ComboBox cbFeeBranchType;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbFeeType;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbFeeType1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANS_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FEEDISC_TYPE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FEEDISC_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FEEDISC_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCYCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIXED_FEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RATE_FEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MIN_FEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAX_FEE;
        private System.Windows.Forms.Button cmdCalDetail;
        private System.Windows.Forms.Button cmdCalExcel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbCCYCD1;
    }
}