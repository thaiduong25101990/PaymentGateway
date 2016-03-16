namespace BR.BRSYSTEM
{
    partial class frmMsgExcel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cboSDirection = new System.Windows.Forms.ComboBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtMsgTypeSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGWTypeSearch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboDirection = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPartNum = new System.Windows.Forms.TextBox();
            this.txtRowNum = new System.Windows.Forms.TextBox();
            this.txtDefault = new System.Windows.Forms.TextBox();
            this.txtSwiftField = new System.Windows.Forms.TextBox();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.cboCheck = new System.Windows.Forms.ComboBox();
            this.txtMaxRow = new System.Windows.Forms.TextBox();
            this.txtMaxLength = new System.Windows.Forms.TextBox();
            this.txtRowBegin = new System.Windows.Forms.TextBox();
            this.txtFieldDecrip = new System.Windows.Forms.TextBox();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.txtExcelCol = new System.Windows.Forms.TextBox();
            this.txtMsgType = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboGWType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgdListMsg = new System.Windows.Forms.DataGridView();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdListMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(856, 669);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(684, 669);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(596, 669);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.TabIndex = 4;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click_1);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(420, 669);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.TabIndex = 2;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click_2);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(508, 669);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.TabIndex = 3;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cboSDirection);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.txtMsgTypeSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboGWTypeSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(940, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 54);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 16);
            this.label18.TabIndex = 4;
            this.label18.Text = "Direction:";
            // 
            // cboSDirection
            // 
            this.cboSDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSDirection.FormattingEnabled = true;
            this.cboSDirection.Location = new System.Drawing.Point(144, 50);
            this.cboSDirection.Name = "cboSDirection";
            this.cboSDirection.Size = new System.Drawing.Size(235, 24);
            this.cboSDirection.TabIndex = 3;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(828, 46);
            this.cmdSearch.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtMsgTypeSearch
            // 
            this.txtMsgTypeSearch.Location = new System.Drawing.Point(543, 51);
            this.txtMsgTypeSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtMsgTypeSearch.Name = "txtMsgTypeSearch";
            this.txtMsgTypeSearch.Size = new System.Drawing.Size(205, 22);
            this.txtMsgTypeSearch.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(428, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message type :";
            // 
            // cboGWTypeSearch
            // 
            this.cboGWTypeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGWTypeSearch.FormattingEnabled = true;
            this.cboGWTypeSearch.Location = new System.Drawing.Point(144, 17);
            this.cboGWTypeSearch.Margin = new System.Windows.Forms.Padding(4);
            this.cboGWTypeSearch.Name = "cboGWTypeSearch";
            this.cboGWTypeSearch.Size = new System.Drawing.Size(235, 24);
            this.cboGWTypeSearch.TabIndex = 0;
            this.cboGWTypeSearch.SelectedIndexChanged += new System.EventHandler(this.cboGWTypeSearch_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Channel :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboDirection);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtPartNum);
            this.groupBox2.Controls.Add(this.txtRowNum);
            this.groupBox2.Controls.Add(this.txtDefault);
            this.groupBox2.Controls.Add(this.txtSwiftField);
            this.groupBox2.Controls.Add(this.cboDataType);
            this.groupBox2.Controls.Add(this.cboCheck);
            this.groupBox2.Controls.Add(this.txtMaxRow);
            this.groupBox2.Controls.Add(this.txtMaxLength);
            this.groupBox2.Controls.Add(this.txtRowBegin);
            this.groupBox2.Controls.Add(this.txtFieldDecrip);
            this.groupBox2.Controls.Add(this.txtFieldName);
            this.groupBox2.Controls.Add(this.txtExcelCol);
            this.groupBox2.Controls.Add(this.txtMsgType);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboGWType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(17, 93);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(940, 287);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // cboDirection
            // 
            this.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirection.FormattingEnabled = true;
            this.cboDirection.Location = new System.Drawing.Point(706, 188);
            this.cboDirection.Margin = new System.Windows.Forms.Padding(4);
            this.cboDirection.Name = "cboDirection";
            this.cboDirection.Size = new System.Drawing.Size(202, 24);
            this.cboDirection.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(611, 224);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 16);
            this.label17.TabIndex = 29;
            this.label17.Text = "Row num :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(611, 192);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 16);
            this.label16.TabIndex = 28;
            this.label16.Text = "Direction :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(611, 256);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 16);
            this.label15.TabIndex = 27;
            this.label15.Text = "Part num :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 256);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 16);
            this.label14.TabIndex = 26;
            this.label14.Text = "Default value :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 224);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Swift field name :";
            // 
            // txtPartNum
            // 
            this.txtPartNum.Location = new System.Drawing.Point(706, 253);
            this.txtPartNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtPartNum.Name = "txtPartNum";
            this.txtPartNum.Size = new System.Drawing.Size(202, 22);
            this.txtPartNum.TabIndex = 14;
            this.txtPartNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPartNum_KeyPress);
            // 
            // txtRowNum
            // 
            this.txtRowNum.Location = new System.Drawing.Point(706, 221);
            this.txtRowNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtRowNum.Name = "txtRowNum";
            this.txtRowNum.Size = new System.Drawing.Size(202, 22);
            this.txtRowNum.TabIndex = 13;
            this.txtRowNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRowNum_KeyPress);
            // 
            // txtDefault
            // 
            this.txtDefault.Location = new System.Drawing.Point(144, 253);
            this.txtDefault.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Size = new System.Drawing.Size(399, 22);
            this.txtDefault.TabIndex = 6;
            // 
            // txtSwiftField
            // 
            this.txtSwiftField.Location = new System.Drawing.Point(144, 221);
            this.txtSwiftField.Margin = new System.Windows.Forms.Padding(4);
            this.txtSwiftField.Name = "txtSwiftField";
            this.txtSwiftField.Size = new System.Drawing.Size(399, 22);
            this.txtSwiftField.TabIndex = 5;
            // 
            // cboDataType
            // 
            this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataType.FormattingEnabled = true;
            this.cboDataType.Location = new System.Drawing.Point(706, 154);
            this.cboDataType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(202, 24);
            this.cboDataType.TabIndex = 11;
            // 
            // cboCheck
            // 
            this.cboCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheck.FormattingEnabled = true;
            this.cboCheck.Location = new System.Drawing.Point(706, 120);
            this.cboCheck.Margin = new System.Windows.Forms.Padding(4);
            this.cboCheck.Name = "cboCheck";
            this.cboCheck.Size = new System.Drawing.Size(202, 24);
            this.cboCheck.TabIndex = 10;
            // 
            // txtMaxRow
            // 
            this.txtMaxRow.Location = new System.Drawing.Point(706, 51);
            this.txtMaxRow.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxRow.Name = "txtMaxRow";
            this.txtMaxRow.Size = new System.Drawing.Size(202, 22);
            this.txtMaxRow.TabIndex = 8;
            this.txtMaxRow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxRow_KeyPress);
            // 
            // txtMaxLength
            // 
            this.txtMaxLength.Location = new System.Drawing.Point(706, 86);
            this.txtMaxLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxLength.Name = "txtMaxLength";
            this.txtMaxLength.Size = new System.Drawing.Size(202, 22);
            this.txtMaxLength.TabIndex = 9;
            this.txtMaxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxLength_KeyPress);
            // 
            // txtRowBegin
            // 
            this.txtRowBegin.Location = new System.Drawing.Point(706, 16);
            this.txtRowBegin.Margin = new System.Windows.Forms.Padding(4);
            this.txtRowBegin.Name = "txtRowBegin";
            this.txtRowBegin.Size = new System.Drawing.Size(202, 22);
            this.txtRowBegin.TabIndex = 7;
            this.txtRowBegin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRowBegin_KeyPress);
            // 
            // txtFieldDecrip
            // 
            this.txtFieldDecrip.Location = new System.Drawing.Point(144, 189);
            this.txtFieldDecrip.Margin = new System.Windows.Forms.Padding(4);
            this.txtFieldDecrip.Name = "txtFieldDecrip";
            this.txtFieldDecrip.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtFieldDecrip.Size = new System.Drawing.Size(399, 22);
            this.txtFieldDecrip.TabIndex = 4;
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(144, 155);
            this.txtFieldName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(399, 22);
            this.txtFieldName.TabIndex = 3;
            // 
            // txtExcelCol
            // 
            this.txtExcelCol.Location = new System.Drawing.Point(144, 121);
            this.txtExcelCol.Margin = new System.Windows.Forms.Padding(4);
            this.txtExcelCol.MaxLength = 3;
            this.txtExcelCol.Name = "txtExcelCol";
            this.txtExcelCol.Size = new System.Drawing.Size(235, 22);
            this.txtExcelCol.TabIndex = 2;
            // 
            // txtMsgType
            // 
            this.txtMsgType.Location = new System.Drawing.Point(144, 86);
            this.txtMsgType.Margin = new System.Windows.Forms.Padding(4);
            this.txtMsgType.Name = "txtMsgType";
            this.txtMsgType.Size = new System.Drawing.Size(235, 22);
            this.txtMsgType.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(611, 158);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 10;
            this.label12.Text = "Data type :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(611, 124);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "Check :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(611, 89);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 8;
            this.label10.Text = "Max length :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(611, 54);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "Max row :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(611, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "Row begin :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 192);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Field description :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 158);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Field name :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 124);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Excel column :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Message type :";
            // 
            // cboGWType
            // 
            this.cboGWType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGWType.FormattingEnabled = true;
            this.cboGWType.Location = new System.Drawing.Point(144, 50);
            this.cboGWType.Margin = new System.Windows.Forms.Padding(4);
            this.cboGWType.Name = "cboGWType";
            this.cboGWType.Size = new System.Drawing.Size(235, 24);
            this.cboGWType.TabIndex = 0;
            this.cboGWType.SelectedIndexChanged += new System.EventHandler(this.cboGWType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Channel :";
            // 
            // dgdListMsg
            // 
            this.dgdListMsg.AllowUserToAddRows = false;
            this.dgdListMsg.AllowUserToDeleteRows = false;
            this.dgdListMsg.AllowUserToResizeRows = false;
            this.dgdListMsg.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgdListMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdListMsg.Location = new System.Drawing.Point(17, 388);
            this.dgdListMsg.Margin = new System.Windows.Forms.Padding(4);
            this.dgdListMsg.Name = "dgdListMsg";
            this.dgdListMsg.ReadOnly = true;
            this.dgdListMsg.RowHeadersWidth = 23;
            this.dgdListMsg.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgdListMsg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgdListMsg.Size = new System.Drawing.Size(940, 273);
            this.dgdListMsg.TabIndex = 21;
            this.dgdListMsg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgdListMsg_MouseMove);
            this.dgdListMsg.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdListMsg_CellEnter);
            this.dgdListMsg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdListMsg_CellContentClick_1);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(770, 669);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 6;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click_1);
            // 
            // frmMsgExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(969, 708);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.dgdListMsg);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMsgExcel";
            this.Text = "MSG EXCEL";
            this.Load += new System.EventHandler(this.frmMsgExcel_Load_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMsgExcel_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.dgdListMsg, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdListMsg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtMsgTypeSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGWTypeSearch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboGWType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRowBegin;
        private System.Windows.Forms.TextBox txtFieldDecrip;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.TextBox txtExcelCol;
        private System.Windows.Forms.TextBox txtMsgType;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.ComboBox cboCheck;
        private System.Windows.Forms.TextBox txtMaxRow;
        private System.Windows.Forms.TextBox txtMaxLength;
        private System.Windows.Forms.TextBox txtPartNum;
        private System.Windows.Forms.TextBox txtRowNum;
        private System.Windows.Forms.TextBox txtDefault;
        private System.Windows.Forms.TextBox txtSwiftField;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dgdListMsg;
        private System.Windows.Forms.ComboBox cboDirection;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ComboBox cboSDirection;
        private System.Windows.Forms.Label label18;
    }
}