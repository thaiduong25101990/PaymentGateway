namespace BR.BRSWIFT
{
    partial class frmSwiftMsgManualInfo
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
            this.tabInfomation = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReceivedBranch = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.txtReceivedBranch = new System.Windows.Forms.TextBox();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbcontent = new System.Windows.Forms.GroupBox();
            this.txtcontent = new System.Windows.Forms.TextBox();
            this.grbtransaction = new System.Windows.Forms.GroupBox();
            this.txtMsgType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRM_NUMBER = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCCYCD = new System.Windows.Forms.Label();
            this.lbRecevi_name = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lbDerpartment = new System.Windows.Forms.Label();
            this.txtTransDate = new System.Windows.Forms.TextBox();
            this.lbamount = new System.Windows.Forms.Label();
            this.txtReceivingBank = new System.Windows.Forms.TextBox();
            this.lbtran_date = new System.Windows.Forms.Label();
            this.lbreceiving_bank = new System.Windows.Forms.Label();
            this.txtRMNo = new System.Windows.Forms.TextBox();
            this.lbRm_no = new System.Windows.Forms.Label();
            this.txtSendingBank = new System.Windows.Forms.TextBox();
            this.lbsending_bank = new System.Windows.Forms.Label();
            this.tabHis = new System.Windows.Forms.TabPage();
            this.txthistory = new System.Windows.Forms.TextBox();
            this.cmdApprove = new System.Windows.Forms.Button();
            this.cmdReject = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tabInfomation.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbcontent.SuspendLayout();
            this.grbtransaction.SuspendLayout();
            this.tabHis.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabInfomation
            // 
            this.tabInfomation.Controls.Add(this.tabInfo);
            this.tabInfomation.Controls.Add(this.tabHis);
            this.tabInfomation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfomation.Location = new System.Drawing.Point(2, 1);
            this.tabInfomation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInfomation.Name = "tabInfomation";
            this.tabInfomation.SelectedIndex = 0;
            this.tabInfomation.Size = new System.Drawing.Size(993, 562);
            this.tabInfomation.TabIndex = 2;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.groupBox1);
            this.tabInfo.Controls.Add(this.grbcontent);
            this.tabInfo.Controls.Add(this.grbtransaction);
            this.tabInfo.Location = new System.Drawing.Point(4, 25);
            this.tabInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInfo.Size = new System.Drawing.Size(985, 533);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "Information";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblReceivedBranch);
            this.groupBox1.Controls.Add(this.cboDepartment);
            this.groupBox1.Controls.Add(this.txtReceivedBranch);
            this.groupBox1.Controls.Add(this.lblBranchName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(467, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input data";
            // 
            // lblReceivedBranch
            // 
            this.lblReceivedBranch.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblReceivedBranch.AutoSize = true;
            this.lblReceivedBranch.ForeColor = System.Drawing.Color.Blue;
            this.lblReceivedBranch.Location = new System.Drawing.Point(428, 21);
            this.lblReceivedBranch.Name = "lblReceivedBranch";
            this.lblReceivedBranch.Size = new System.Drawing.Size(0, 16);
            this.lblReceivedBranch.TabIndex = 3;
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(116, 49);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(68, 24);
            this.cboDepartment.TabIndex = 2;
            this.cboDepartment.SelectedIndexChanged += new System.EventHandler(this.cboDepartment_SelectedIndexChanged);
            this.cboDepartment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cboDepartment_MouseDown);
            // 
            // txtReceivedBranch
            // 
            this.txtReceivedBranch.Location = new System.Drawing.Point(116, 21);
            this.txtReceivedBranch.Name = "txtReceivedBranch";
            this.txtReceivedBranch.Size = new System.Drawing.Size(68, 23);
            this.txtReceivedBranch.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtReceivedBranch, "Press F5 to view data");
            this.txtReceivedBranch.TextChanged += new System.EventHandler(this.txtReceivedBranch_TextChanged);
            this.txtReceivedBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReceivedBranch_KeyDown);
            this.txtReceivedBranch.Leave += new System.EventHandler(this.txtReceivedBranch_Leave);
            this.txtReceivedBranch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtReceivedBranch_MouseDown);
            // 
            // lblBranchName
            // 
            this.lblBranchName.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.ForeColor = System.Drawing.Color.Blue;
            this.lblBranchName.Location = new System.Drawing.Point(189, 25);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(0, 16);
            this.lblBranchName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Module :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Received branch :";
            // 
            // grbcontent
            // 
            this.grbcontent.Controls.Add(this.txtcontent);
            this.grbcontent.Location = new System.Drawing.Point(467, 99);
            this.grbcontent.Name = "grbcontent";
            this.grbcontent.Size = new System.Drawing.Size(512, 426);
            this.grbcontent.TabIndex = 2;
            this.grbcontent.TabStop = false;
            this.grbcontent.Text = "Content ";
            // 
            // txtcontent
            // 
            this.txtcontent.BackColor = System.Drawing.Color.White;
            this.txtcontent.Location = new System.Drawing.Point(6, 19);
            this.txtcontent.Multiline = true;
            this.txtcontent.Name = "txtcontent";
            this.txtcontent.ReadOnly = true;
            this.txtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtcontent.Size = new System.Drawing.Size(500, 401);
            this.txtcontent.TabIndex = 20;
            this.txtcontent.TabStop = false;
            // 
            // grbtransaction
            // 
            this.grbtransaction.Controls.Add(this.txtMsgType);
            this.grbtransaction.Controls.Add(this.label5);
            this.grbtransaction.Controls.Add(this.txtRM_NUMBER);
            this.grbtransaction.Controls.Add(this.label4);
            this.grbtransaction.Controls.Add(this.lbCCYCD);
            this.grbtransaction.Controls.Add(this.lbRecevi_name);
            this.grbtransaction.Controls.Add(this.label3);
            this.grbtransaction.Controls.Add(this.txtDepartment);
            this.grbtransaction.Controls.Add(this.txtAmount);
            this.grbtransaction.Controls.Add(this.lbDerpartment);
            this.grbtransaction.Controls.Add(this.txtTransDate);
            this.grbtransaction.Controls.Add(this.lbamount);
            this.grbtransaction.Controls.Add(this.txtReceivingBank);
            this.grbtransaction.Controls.Add(this.lbtran_date);
            this.grbtransaction.Controls.Add(this.lbreceiving_bank);
            this.grbtransaction.Controls.Add(this.txtRMNo);
            this.grbtransaction.Controls.Add(this.lbRm_no);
            this.grbtransaction.Controls.Add(this.txtSendingBank);
            this.grbtransaction.Controls.Add(this.lbsending_bank);
            this.grbtransaction.Location = new System.Drawing.Point(6, 7);
            this.grbtransaction.Name = "grbtransaction";
            this.grbtransaction.Size = new System.Drawing.Size(455, 517);
            this.grbtransaction.TabIndex = 1;
            this.grbtransaction.TabStop = false;
            this.grbtransaction.Text = "Transaction";
            // 
            // txtMsgType
            // 
            this.txtMsgType.BackColor = System.Drawing.Color.White;
            this.txtMsgType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsgType.Location = new System.Drawing.Point(15, 43);
            this.txtMsgType.Name = "txtMsgType";
            this.txtMsgType.ReadOnly = true;
            this.txtMsgType.Size = new System.Drawing.Size(168, 23);
            this.txtMsgType.TabIndex = 16;
            this.txtMsgType.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Message Type :";
            // 
            // txtRM_NUMBER
            // 
            this.txtRM_NUMBER.BackColor = System.Drawing.Color.White;
            this.txtRM_NUMBER.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRM_NUMBER.Location = new System.Drawing.Point(15, 90);
            this.txtRM_NUMBER.Name = "txtRM_NUMBER";
            this.txtRM_NUMBER.ReadOnly = true;
            this.txtRM_NUMBER.Size = new System.Drawing.Size(168, 23);
            this.txtRM_NUMBER.TabIndex = 14;
            this.txtRM_NUMBER.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "RM no :";
            // 
            // lbCCYCD
            // 
            this.lbCCYCD.AutoSize = true;
            this.lbCCYCD.ForeColor = System.Drawing.Color.Blue;
            this.lbCCYCD.Location = new System.Drawing.Point(186, 242);
            this.lbCCYCD.Name = "lbCCYCD";
            this.lbCCYCD.Size = new System.Drawing.Size(0, 16);
            this.lbCCYCD.TabIndex = 12;
            // 
            // lbRecevi_name
            // 
            this.lbRecevi_name.AutoSize = true;
            this.lbRecevi_name.ForeColor = System.Drawing.Color.Blue;
            this.lbRecevi_name.Location = new System.Drawing.Point(186, 194);
            this.lbRecevi_name.Name = "lbRecevi_name";
            this.lbRecevi_name.Size = new System.Drawing.Size(0, 16);
            this.lbRecevi_name.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(186, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 10;
            // 
            // txtDepartment
            // 
            this.txtDepartment.BackColor = System.Drawing.Color.White;
            this.txtDepartment.Location = new System.Drawing.Point(15, 388);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(168, 23);
            this.txtDepartment.TabIndex = 7;
            this.txtDepartment.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(15, 239);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(168, 23);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.TabStop = false;
            // 
            // lbDerpartment
            // 
            this.lbDerpartment.AutoSize = true;
            this.lbDerpartment.Location = new System.Drawing.Point(15, 369);
            this.lbDerpartment.Name = "lbDerpartment";
            this.lbDerpartment.Size = new System.Drawing.Size(58, 16);
            this.lbDerpartment.TabIndex = 0;
            this.lbDerpartment.Text = "Module :";
            // 
            // txtTransDate
            // 
            this.txtTransDate.BackColor = System.Drawing.Color.White;
            this.txtTransDate.Location = new System.Drawing.Point(15, 338);
            this.txtTransDate.Name = "txtTransDate";
            this.txtTransDate.ReadOnly = true;
            this.txtTransDate.Size = new System.Drawing.Size(168, 23);
            this.txtTransDate.TabIndex = 6;
            this.txtTransDate.TabStop = false;
            // 
            // lbamount
            // 
            this.lbamount.AutoSize = true;
            this.lbamount.Location = new System.Drawing.Point(15, 221);
            this.lbamount.Name = "lbamount";
            this.lbamount.Size = new System.Drawing.Size(61, 16);
            this.lbamount.TabIndex = 0;
            this.lbamount.Text = "Amount :";
            // 
            // txtReceivingBank
            // 
            this.txtReceivingBank.BackColor = System.Drawing.Color.White;
            this.txtReceivingBank.Location = new System.Drawing.Point(15, 190);
            this.txtReceivingBank.Name = "txtReceivingBank";
            this.txtReceivingBank.ReadOnly = true;
            this.txtReceivingBank.Size = new System.Drawing.Size(168, 23);
            this.txtReceivingBank.TabIndex = 3;
            this.txtReceivingBank.TabStop = false;
            // 
            // lbtran_date
            // 
            this.lbtran_date.AutoSize = true;
            this.lbtran_date.Location = new System.Drawing.Point(15, 319);
            this.lbtran_date.Name = "lbtran_date";
            this.lbtran_date.Size = new System.Drawing.Size(79, 16);
            this.lbtran_date.TabIndex = 0;
            this.lbtran_date.Text = "Trans date :";
            // 
            // lbreceiving_bank
            // 
            this.lbreceiving_bank.AutoSize = true;
            this.lbreceiving_bank.Location = new System.Drawing.Point(15, 171);
            this.lbreceiving_bank.Name = "lbreceiving_bank";
            this.lbreceiving_bank.Size = new System.Drawing.Size(102, 16);
            this.lbreceiving_bank.TabIndex = 0;
            this.lbreceiving_bank.Text = "Receiving bank :";
            // 
            // txtRMNo
            // 
            this.txtRMNo.BackColor = System.Drawing.Color.White;
            this.txtRMNo.Location = new System.Drawing.Point(15, 289);
            this.txtRMNo.Name = "txtRMNo";
            this.txtRMNo.ReadOnly = true;
            this.txtRMNo.Size = new System.Drawing.Size(168, 23);
            this.txtRMNo.TabIndex = 5;
            this.txtRMNo.TabStop = false;
            // 
            // lbRm_no
            // 
            this.lbRm_no.AutoSize = true;
            this.lbRm_no.Location = new System.Drawing.Point(15, 270);
            this.lbRm_no.Name = "lbRm_no";
            this.lbRm_no.Size = new System.Drawing.Size(88, 16);
            this.lbRm_no.TabIndex = 0;
            this.lbRm_no.Text = "REF Number :";
            // 
            // txtSendingBank
            // 
            this.txtSendingBank.BackColor = System.Drawing.Color.White;
            this.txtSendingBank.Location = new System.Drawing.Point(15, 140);
            this.txtSendingBank.Name = "txtSendingBank";
            this.txtSendingBank.ReadOnly = true;
            this.txtSendingBank.Size = new System.Drawing.Size(168, 23);
            this.txtSendingBank.TabIndex = 2;
            this.txtSendingBank.TabStop = false;
            // 
            // lbsending_bank
            // 
            this.lbsending_bank.AutoSize = true;
            this.lbsending_bank.Location = new System.Drawing.Point(15, 121);
            this.lbsending_bank.Name = "lbsending_bank";
            this.lbsending_bank.Size = new System.Drawing.Size(94, 16);
            this.lbsending_bank.TabIndex = 0;
            this.lbsending_bank.Text = "Sending bank :";
            // 
            // tabHis
            // 
            this.tabHis.Controls.Add(this.txthistory);
            this.tabHis.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabHis.Location = new System.Drawing.Point(4, 25);
            this.tabHis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabHis.Name = "tabHis";
            this.tabHis.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabHis.Size = new System.Drawing.Size(985, 533);
            this.tabHis.TabIndex = 1;
            this.tabHis.Text = "History";
            this.tabHis.UseVisualStyleBackColor = true;
            // 
            // txthistory
            // 
            this.txthistory.BackColor = System.Drawing.Color.White;
            this.txthistory.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txthistory.Location = new System.Drawing.Point(6, 7);
            this.txthistory.Multiline = true;
            this.txthistory.Name = "txthistory";
            this.txthistory.ReadOnly = true;
            this.txthistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txthistory.Size = new System.Drawing.Size(969, 519);
            this.txthistory.TabIndex = 0;
            // 
            // cmdApprove
            // 
            this.cmdApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdApprove.Location = new System.Drawing.Point(567, 566);
            this.cmdApprove.Name = "cmdApprove";
            this.cmdApprove.Size = new System.Drawing.Size(80, 30);
            this.cmdApprove.TabIndex = 4;
            this.cmdApprove.Text = "Approve";
            this.cmdApprove.UseVisualStyleBackColor = true;
            this.cmdApprove.MouseLeave += new System.EventHandler(this.cmdApprove_MouseLeave);
            this.cmdApprove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdApprove_MouseMove);
            this.cmdApprove.Click += new System.EventHandler(this.cmdApprove_Click);
            this.cmdApprove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdApprove_MouseDown);
            // 
            // cmdReject
            // 
            this.cmdReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReject.Location = new System.Drawing.Point(651, 566);
            this.cmdReject.Name = "cmdReject";
            this.cmdReject.Size = new System.Drawing.Size(80, 30);
            this.cmdReject.TabIndex = 5;
            this.cmdReject.Text = "&Reject";
            this.cmdReject.UseVisualStyleBackColor = true;
            this.cmdReject.MouseLeave += new System.EventHandler(this.cmdReject_MouseLeave);
            this.cmdReject.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdReject_MouseMove);
            this.cmdReject.Click += new System.EventHandler(this.cmdReject_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(735, 566);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(80, 30);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.MouseLeave += new System.EventHandler(this.cmdOK_MouseLeave);
            this.cmdOK.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdOK_MouseMove);
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(901, 566);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.MouseLeave += new System.EventHandler(this.cmdCancel_MouseLeave);
            this.cmdCancel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdCancel_MouseMove);
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(818, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmSwiftMsgManualInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 613);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdReject);
            this.Controls.Add(this.cmdApprove);
            this.Controls.Add(this.tabInfomation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSwiftMsgManualInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Swift Manual process - input data";
            this.Load += new System.EventHandler(this.frmSwiftMsgManualInfo_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSwiftMsgManualInfo_MouseDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmSwiftMsgManualInfo_Keypress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSwiftMsgManualInfo_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSwiftMsgManualInfo_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftMsgManualInfo_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftMsgManualInfo_KeyDown);
            this.tabInfomation.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbcontent.ResumeLayout(false);
            this.grbcontent.PerformLayout();
            this.grbtransaction.ResumeLayout(false);
            this.grbtransaction.PerformLayout();
            this.tabHis.ResumeLayout(false);
            this.tabHis.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabInfomation;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.GroupBox grbcontent;
        private System.Windows.Forms.TextBox txtcontent;
        private System.Windows.Forms.GroupBox grbtransaction;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lbDerpartment;
        private System.Windows.Forms.TextBox txtTransDate;
        private System.Windows.Forms.Label lbamount;
        private System.Windows.Forms.TextBox txtReceivingBank;
        private System.Windows.Forms.Label lbtran_date;
        private System.Windows.Forms.Label lbreceiving_bank;
        private System.Windows.Forms.TextBox txtRMNo;
        private System.Windows.Forms.Label lbRm_no;
        private System.Windows.Forms.TextBox txtSendingBank;
        private System.Windows.Forms.Label lbsending_bank;
        private System.Windows.Forms.TabPage tabHis;
        private System.Windows.Forms.TextBox txthistory;
        private System.Windows.Forms.Button cmdApprove;
        private System.Windows.Forms.Button cmdReject;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.TextBox txtReceivedBranch;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReceivedBranch;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbCCYCD;
        private System.Windows.Forms.Label lbRecevi_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRM_NUMBER;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMsgType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}