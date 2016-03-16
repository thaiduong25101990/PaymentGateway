namespace BR.BRIBPS
{
    partial class frmIBPSMsgFwPreviousDay
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
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cmdForward = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.txtTAD = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataSearch = new System.Windows.Forms.DataGridView();
            this.GW_TRANS_NUM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RM_NUMBER1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SENDER1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIVER1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANS_DATE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AMOUNT1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCYCD1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAD1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANS_CODE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pre_Tad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG_ID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Query_Id1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grSearch = new System.Windows.Forms.GroupBox();
            this.TransTime = new System.Windows.Forms.DateTimePicker();
            this.comModeTranDate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAmount = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGW2 = new System.Windows.Forms.TextBox();
            this.txtGW1 = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReSender = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.cmdRemoveall_Message = new System.Windows.Forms.Button();
            this.cmdRemove_Message = new System.Windows.Forms.Button();
            this.cmdAddall_Message = new System.Windows.Forms.Button();
            this.cmdAdd_Message = new System.Windows.Forms.Button();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.GW_TRANS_NUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RM_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SENDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIVER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANS_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCYCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pre_Tad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Query_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataSearch)).BeginInit();
            this.grSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(939, 539);
            this.cmdClose.TabIndex = 15;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(469, 622);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(383, 622);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(208, 622);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(294, 622);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(556, 707);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(38, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "LV";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(555, 680);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(40, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "HV";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // cmdForward
            // 
            this.cmdForward.Location = new System.Drawing.Point(938, 96);
            this.cmdForward.Name = "cmdForward";
            this.cmdForward.Size = new System.Drawing.Size(80, 30);
            this.cmdForward.TabIndex = 11;
            this.cmdForward.Text = "&Forward";
            this.cmdForward.UseVisualStyleBackColor = true;
            this.cmdForward.Click += new System.EventHandler(this.cmdForward_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(530, 605);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Current TAD :";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefresh.Location = new System.Drawing.Point(277, 556);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(80, 30);
            this.cmdRefresh.TabIndex = 13;
            this.cmdRefresh.Text = "&Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            // 
            // txtTAD
            // 
            this.txtTAD.Location = new System.Drawing.Point(614, 603);
            this.txtTAD.Name = "txtTAD";
            this.txtTAD.Size = new System.Drawing.Size(100, 20);
            this.txtTAD.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(526, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(174, 16);
            this.label12.TabIndex = 110;
            this.label12.Text = "Total number of messages :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 16);
            this.label1.TabIndex = 158;
            this.label1.Text = "Total number of messages :";
            // 
            // dataSearch
            // 
            this.dataSearch.AllowUserToAddRows = false;
            this.dataSearch.BackgroundColor = System.Drawing.Color.White;
            this.dataSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GW_TRANS_NUM1,
            this.RM_NUMBER1,
            this.SENDER1,
            this.RECEIVER1,
            this.TRANS_DATE1,
            this.AMOUNT1,
            this.CCYCD1,
            this.STATUS1,
            this.TAD1,
            this.TRANS_CODE1,
            this.Pre_Tad1,
            this.MSG_ID1,
            this.Query_Id1});
            this.dataSearch.Location = new System.Drawing.Point(12, 152);
            this.dataSearch.Name = "dataSearch";
            this.dataSearch.RowHeadersWidth = 30;
            this.dataSearch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataSearch.Size = new System.Drawing.Size(464, 381);
            this.dataSearch.TabIndex = 12;
            this.dataSearch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataSearch_MouseMove);
            this.dataSearch.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataSearch_ColumnHeaderMouseClick);
            this.dataSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataSearch_CellClick);
            this.dataSearch.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataSearch_CellEnter);
            // 
            // GW_TRANS_NUM1
            // 
            this.GW_TRANS_NUM1.HeaderText = "RELATION NUMBER";
            this.GW_TRANS_NUM1.Name = "GW_TRANS_NUM1";
            this.GW_TRANS_NUM1.Width = 130;
            // 
            // RM_NUMBER1
            // 
            this.RM_NUMBER1.HeaderText = "RM NUMBER";
            this.RM_NUMBER1.Name = "RM_NUMBER1";
            // 
            // SENDER1
            // 
            this.SENDER1.HeaderText = "SENDER";
            this.SENDER1.Name = "SENDER1";
            // 
            // RECEIVER1
            // 
            this.RECEIVER1.HeaderText = "RECEIVER";
            this.RECEIVER1.Name = "RECEIVER1";
            // 
            // TRANS_DATE1
            // 
            this.TRANS_DATE1.HeaderText = "TRANS DATE";
            this.TRANS_DATE1.Name = "TRANS_DATE1";
            // 
            // AMOUNT1
            // 
            this.AMOUNT1.HeaderText = "AMOUNT";
            this.AMOUNT1.Name = "AMOUNT1";
            // 
            // CCYCD1
            // 
            this.CCYCD1.HeaderText = "CCYCD";
            this.CCYCD1.Name = "CCYCD1";
            // 
            // STATUS1
            // 
            this.STATUS1.HeaderText = "STATUS";
            this.STATUS1.Name = "STATUS1";
            // 
            // TAD1
            // 
            this.TAD1.HeaderText = "TAD";
            this.TAD1.Name = "TAD1";
            // 
            // TRANS_CODE1
            // 
            this.TRANS_CODE1.HeaderText = "TRANS CODE";
            this.TRANS_CODE1.Name = "TRANS_CODE1";
            // 
            // Pre_Tad1
            // 
            this.Pre_Tad1.HeaderText = "Pre_Tad";
            this.Pre_Tad1.Name = "Pre_Tad1";
            // 
            // MSG_ID1
            // 
            this.MSG_ID1.HeaderText = "MSG ID";
            this.MSG_ID1.Name = "MSG_ID1";
            // 
            // Query_Id1
            // 
            this.Query_Id1.HeaderText = "Query Id";
            this.Query_Id1.Name = "Query_Id1";
            // 
            // grSearch
            // 
            this.grSearch.Controls.Add(this.TransTime);
            this.grSearch.Controls.Add(this.comModeTranDate);
            this.grSearch.Controls.Add(this.label4);
            this.grSearch.Controls.Add(this.cbAmount);
            this.grSearch.Controls.Add(this.label9);
            this.grSearch.Controls.Add(this.txtGW2);
            this.grSearch.Controls.Add(this.txtGW1);
            this.grSearch.Controls.Add(this.txtAmount);
            this.grSearch.Controls.Add(this.label7);
            this.grSearch.Controls.Add(this.txtReSender);
            this.grSearch.Controls.Add(this.dateTimePicker1);
            this.grSearch.Controls.Add(this.label8);
            this.grSearch.Controls.Add(this.label6);
            this.grSearch.Controls.Add(this.label5);
            this.grSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grSearch.Location = new System.Drawing.Point(11, 1);
            this.grSearch.Name = "grSearch";
            this.grSearch.Size = new System.Drawing.Size(921, 125);
            this.grSearch.TabIndex = 20;
            this.grSearch.TabStop = false;
            this.grSearch.Text = "Search ";
            // 
            // TransTime
            // 
            this.TransTime.CustomFormat = "hh:mm tt";
            this.TransTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TransTime.Location = new System.Drawing.Point(688, 88);
            this.TransTime.Name = "TransTime";
            this.TransTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TransTime.ShowUpDown = true;
            this.TransTime.Size = new System.Drawing.Size(169, 22);
            this.TransTime.TabIndex = 8;
            // 
            // comModeTranDate
            // 
            this.comModeTranDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModeTranDate.FormattingEnabled = true;
            this.comModeTranDate.Items.AddRange(new object[] {
            "ALL",
            "=",
            "<",
            ">"});
            this.comModeTranDate.Location = new System.Drawing.Point(630, 87);
            this.comModeTranDate.Name = "comModeTranDate";
            this.comModeTranDate.Size = new System.Drawing.Size(52, 24);
            this.comModeTranDate.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(514, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 164;
            this.label4.Text = "Trans time :";
            // 
            // cbAmount
            // 
            this.cbAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAmount.FormattingEnabled = true;
            this.cbAmount.Items.AddRange(new object[] {
            "ALL",
            "=",
            ">=",
            "<="});
            this.cbAmount.Location = new System.Drawing.Point(197, 87);
            this.cbAmount.Name = "cbAmount";
            this.cbAmount.Size = new System.Drawing.Size(43, 24);
            this.cbAmount.TabIndex = 4;
            this.cbAmount.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(286, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 16);
            this.label9.TabIndex = 154;
            this.label9.Text = "<-->";
            // 
            // txtGW2
            // 
            this.txtGW2.Location = new System.Drawing.Point(317, 25);
            this.txtGW2.Name = "txtGW2";
            this.txtGW2.Size = new System.Drawing.Size(82, 22);
            this.txtGW2.TabIndex = 2;
            // 
            // txtGW1
            // 
            this.txtGW1.Location = new System.Drawing.Point(197, 25);
            this.txtGW1.Name = "txtGW1";
            this.txtGW1.Size = new System.Drawing.Size(87, 22);
            this.txtGW1.TabIndex = 1;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(246, 88);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(153, 22);
            this.txtAmount.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 16);
            this.label7.TabIndex = 152;
            this.label7.Text = "Amount :";
            // 
            // txtReSender
            // 
            this.txtReSender.Location = new System.Drawing.Point(197, 58);
            this.txtReSender.Name = "txtReSender";
            this.txtReSender.Size = new System.Drawing.Size(202, 22);
            this.txtReSender.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(630, 58);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(227, 22);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(514, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 160;
            this.label8.Text = "Date :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 16);
            this.label6.TabIndex = 151;
            this.label6.Text = "Receiving bank :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 150;
            this.label5.Text = "Relation number:";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(938, 60);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 9;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdRemoveall_Message
            // 
            this.cmdRemoveall_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoveall_Message.Location = new System.Drawing.Point(480, 259);
            this.cmdRemoveall_Message.Name = "cmdRemoveall_Message";
            this.cmdRemoveall_Message.Size = new System.Drawing.Size(44, 30);
            this.cmdRemoveall_Message.TabIndex = 12;
            this.cmdRemoveall_Message.Text = "<<";
            this.cmdRemoveall_Message.UseVisualStyleBackColor = true;
            this.cmdRemoveall_Message.Click += new System.EventHandler(this.cmdRemoveall_Message_Click);
            // 
            // cmdRemove_Message
            // 
            this.cmdRemove_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemove_Message.Location = new System.Drawing.Point(480, 223);
            this.cmdRemove_Message.Name = "cmdRemove_Message";
            this.cmdRemove_Message.Size = new System.Drawing.Size(44, 30);
            this.cmdRemove_Message.TabIndex = 11;
            this.cmdRemove_Message.Text = "<";
            this.cmdRemove_Message.UseVisualStyleBackColor = true;
            this.cmdRemove_Message.Click += new System.EventHandler(this.cmdRemove_Message_Click);
            // 
            // cmdAddall_Message
            // 
            this.cmdAddall_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddall_Message.Location = new System.Drawing.Point(480, 187);
            this.cmdAddall_Message.Name = "cmdAddall_Message";
            this.cmdAddall_Message.Size = new System.Drawing.Size(44, 30);
            this.cmdAddall_Message.TabIndex = 10;
            this.cmdAddall_Message.Text = ">>";
            this.cmdAddall_Message.UseVisualStyleBackColor = true;
            this.cmdAddall_Message.Click += new System.EventHandler(this.cmdAddall_Message_Click);
            // 
            // cmdAdd_Message
            // 
            this.cmdAdd_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdd_Message.Location = new System.Drawing.Point(480, 151);
            this.cmdAdd_Message.Name = "cmdAdd_Message";
            this.cmdAdd_Message.Size = new System.Drawing.Size(44, 30);
            this.cmdAdd_Message.TabIndex = 9;
            this.cmdAdd_Message.Text = ">";
            this.cmdAdd_Message.UseVisualStyleBackColor = true;
            this.cmdAdd_Message.Click += new System.EventHandler(this.cmdAdd_Message_Click);
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgView.ColumnHeadersHeight = 22;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GW_TRANS_NUM,
            this.RM_NUMBER,
            this.SENDER,
            this.RECEIVER,
            this.TRANS_DATE,
            this.AMOUNT,
            this.CCYCD,
            this.STATUS,
            this.TAD,
            this.TRANS_CODE,
            this.Pre_Tad,
            this.MSG_ID,
            this.Query_Id});
            this.dgView.Location = new System.Drawing.Point(528, 152);
            this.dgView.Name = "dgView";
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(490, 381);
            this.dgView.TabIndex = 13;
            this.dgView.TabStop = false;
            this.dgView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.data1_MouseMove);
            this.dgView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.data1_ColumnHeaderMouseClick);
            this.dgView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellClick_1);
            this.dgView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellEnter);
            // 
            // GW_TRANS_NUM
            // 
            this.GW_TRANS_NUM.HeaderText = "RELATION NUMBER";
            this.GW_TRANS_NUM.Name = "GW_TRANS_NUM";
            this.GW_TRANS_NUM.Width = 130;
            // 
            // RM_NUMBER
            // 
            this.RM_NUMBER.HeaderText = "RM NUMBER";
            this.RM_NUMBER.Name = "RM_NUMBER";
            // 
            // SENDER
            // 
            this.SENDER.HeaderText = "SENDER";
            this.SENDER.Name = "SENDER";
            // 
            // RECEIVER
            // 
            this.RECEIVER.HeaderText = "RECEIVER";
            this.RECEIVER.Name = "RECEIVER";
            // 
            // TRANS_DATE
            // 
            this.TRANS_DATE.HeaderText = "TRANS DATE";
            this.TRANS_DATE.Name = "TRANS_DATE";
            // 
            // AMOUNT
            // 
            this.AMOUNT.HeaderText = "AMOUNT";
            this.AMOUNT.Name = "AMOUNT";
            // 
            // CCYCD
            // 
            this.CCYCD.HeaderText = "CCYCD";
            this.CCYCD.Name = "CCYCD";
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.Name = "STATUS";
            // 
            // TAD
            // 
            this.TAD.HeaderText = "TAD";
            this.TAD.Name = "TAD";
            // 
            // TRANS_CODE
            // 
            this.TRANS_CODE.HeaderText = "TRANS CODE";
            this.TRANS_CODE.Name = "TRANS_CODE";
            // 
            // Pre_Tad
            // 
            this.Pre_Tad.HeaderText = "Pre_Tad";
            this.Pre_Tad.Name = "Pre_Tad";
            // 
            // MSG_ID
            // 
            this.MSG_ID.HeaderText = "MSG_ID";
            this.MSG_ID.Name = "MSG_ID";
            // 
            // Query_Id
            // 
            this.Query_Id.HeaderText = "Query_Id";
            this.Query_Id.Name = "Query_Id";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(853, 539);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 14;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmd_Pint_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.OnUpdate);
            // 
            // frmIBPSMsgFwPreviousDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(1030, 598);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.cmdForward);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.cmdRemoveall_Message);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.cmdRemove_Message);
            this.Controls.Add(this.cmdAddall_Message);
            this.Controls.Add(this.cmdAdd_Message);
            this.Controls.Add(this.grSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataSearch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtTAD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdRefresh);
            this.Name = "frmIBPSMsgFwPreviousDay";
            this.Text = "IBPS forward previous day  message";
            this.Load += new System.EventHandler(this.frmIBPSMsgFwPreviousDay_Load);
            this.Click += new System.EventHandler(this.cmdRemoveall_Message_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIBPSMsgFwPreviousDay_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIBPSMsgFwPreviousDay_KeyDown);
            this.Controls.SetChildIndex(this.cmdRefresh, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtTAD, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.dataSearch, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.grSearch, 0);
            this.Controls.SetChildIndex(this.cmdAdd_Message, 0);
            this.Controls.SetChildIndex(this.cmdAddall_Message, 0);
            this.Controls.SetChildIndex(this.cmdRemove_Message, 0);
            this.Controls.SetChildIndex(this.cmdSearch, 0);
            this.Controls.SetChildIndex(this.cmdRemoveall_Message, 0);
            this.Controls.SetChildIndex(this.dgView, 0);
            this.Controls.SetChildIndex(this.radioButton1, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.cmdForward, 0);
            this.Controls.SetChildIndex(this.radioButton2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataSearch)).EndInit();
            this.grSearch.ResumeLayout(false);
            this.grSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdForward;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox txtTAD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataSearch;
        private System.Windows.Forms.GroupBox grSearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGW2;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtGW1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReSender;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdRemoveall_Message;
        private System.Windows.Forms.Button cmdRemove_Message;
        private System.Windows.Forms.Button cmdAddall_Message;
        private System.Windows.Forms.Button cmdAdd_Message;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.ComboBox cbAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GW_TRANS_NUM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RM_NUMBER1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SENDER1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVER1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANS_DATE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AMOUNT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCYCD1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAD1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANS_CODE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pre_Tad1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG_ID1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Query_Id1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GW_TRANS_NUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RM_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn SENDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVER;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANS_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn AMOUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCYCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pre_Tad;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Query_Id;
        private System.Windows.Forms.DateTimePicker TransTime;
        private System.Windows.Forms.ComboBox comModeTranDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}