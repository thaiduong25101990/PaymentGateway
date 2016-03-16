namespace BR.BRSWIFT
{
    partial class frmSwiftMsgManualDup
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
            this.tabInfomation = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatusDescription = new System.Windows.Forms.Label();
            this.grbcontent = new System.Windows.Forms.GroupBox();
            this.txtcontent = new System.Windows.Forms.TextBox();
            this.grbtransaction = new System.Windows.Forms.GroupBox();
            this.txtMsgType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRM_NUMBER = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lbDerpartment = new System.Windows.Forms.Label();
            this.lbCurrecy_name = new System.Windows.Forms.Label();
            this.txtTransDate = new System.Windows.Forms.TextBox();
            this.lbamount = new System.Windows.Forms.Label();
            this.txtReceivingBank = new System.Windows.Forms.TextBox();
            this.lbtran_date = new System.Windows.Forms.Label();
            this.lbReceiving_name = new System.Windows.Forms.Label();
            this.lbreceiving_bank = new System.Windows.Forms.Label();
            this.txtRMNo = new System.Windows.Forms.TextBox();
            this.lbSend_Bankname = new System.Windows.Forms.Label();
            this.lbRm_no = new System.Windows.Forms.Label();
            this.txtSendingBank = new System.Windows.Forms.TextBox();
            this.lbsending_bank = new System.Windows.Forms.Label();
            this.tabHis = new System.Windows.Forms.TabPage();
            this.txthistory = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdRelease = new System.Windows.Forms.Button();
            this.cmdReject = new System.Windows.Forms.Button();
            this.cmdApprove = new System.Windows.Forms.Button();
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
            this.tabInfomation.Location = new System.Drawing.Point(3, 3);
            this.tabInfomation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInfomation.Name = "tabInfomation";
            this.tabInfomation.SelectedIndex = 0;
            this.tabInfomation.Size = new System.Drawing.Size(1000, 538);
            this.tabInfomation.TabIndex = 0;
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
            this.tabInfo.Size = new System.Drawing.Size(992, 509);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "Information";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblStatusDescription);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(978, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input data";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(131, 20);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(140, 24);
            this.cboStatus.TabIndex = 0;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Process status :";
            // 
            // lblStatusDescription
            // 
            this.lblStatusDescription.AutoSize = true;
            this.lblStatusDescription.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusDescription.Location = new System.Drawing.Point(277, 24);
            this.lblStatusDescription.Name = "lblStatusDescription";
            this.lblStatusDescription.Size = new System.Drawing.Size(0, 16);
            this.lblStatusDescription.TabIndex = 0;
            // 
            // grbcontent
            // 
            this.grbcontent.Controls.Add(this.txtcontent);
            this.grbcontent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbcontent.Location = new System.Drawing.Point(501, 68);
            this.grbcontent.Name = "grbcontent";
            this.grbcontent.Size = new System.Drawing.Size(483, 434);
            this.grbcontent.TabIndex = 2;
            this.grbcontent.TabStop = false;
            this.grbcontent.Text = "Content ";
            // 
            // txtcontent
            // 
            this.txtcontent.BackColor = System.Drawing.Color.White;
            this.txtcontent.Location = new System.Drawing.Point(8, 14);
            this.txtcontent.Multiline = true;
            this.txtcontent.Name = "txtcontent";
            this.txtcontent.ReadOnly = true;
            this.txtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtcontent.Size = new System.Drawing.Size(469, 414);
            this.txtcontent.TabIndex = 22;
            this.txtcontent.TabStop = false;
            // 
            // grbtransaction
            // 
            this.grbtransaction.Controls.Add(this.txtMsgType);
            this.grbtransaction.Controls.Add(this.label3);
            this.grbtransaction.Controls.Add(this.txtRM_NUMBER);
            this.grbtransaction.Controls.Add(this.label1);
            this.grbtransaction.Controls.Add(this.txtDepartment);
            this.grbtransaction.Controls.Add(this.txtAmount);
            this.grbtransaction.Controls.Add(this.lbDerpartment);
            this.grbtransaction.Controls.Add(this.lbCurrecy_name);
            this.grbtransaction.Controls.Add(this.txtTransDate);
            this.grbtransaction.Controls.Add(this.lbamount);
            this.grbtransaction.Controls.Add(this.txtReceivingBank);
            this.grbtransaction.Controls.Add(this.lbtran_date);
            this.grbtransaction.Controls.Add(this.lbReceiving_name);
            this.grbtransaction.Controls.Add(this.lbreceiving_bank);
            this.grbtransaction.Controls.Add(this.txtRMNo);
            this.grbtransaction.Controls.Add(this.lbSend_Bankname);
            this.grbtransaction.Controls.Add(this.lbRm_no);
            this.grbtransaction.Controls.Add(this.txtSendingBank);
            this.grbtransaction.Controls.Add(this.lbsending_bank);
            this.grbtransaction.Location = new System.Drawing.Point(6, 68);
            this.grbtransaction.Name = "grbtransaction";
            this.grbtransaction.Size = new System.Drawing.Size(489, 434);
            this.grbtransaction.TabIndex = 0;
            this.grbtransaction.TabStop = false;
            this.grbtransaction.Text = "Transaction";
            // 
            // txtMsgType
            // 
            this.txtMsgType.BackColor = System.Drawing.Color.White;
            this.txtMsgType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsgType.Location = new System.Drawing.Point(23, 49);
            this.txtMsgType.Name = "txtMsgType";
            this.txtMsgType.ReadOnly = true;
            this.txtMsgType.Size = new System.Drawing.Size(138, 23);
            this.txtMsgType.TabIndex = 10;
            this.txtMsgType.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Message Type :";
            // 
            // txtRM_NUMBER
            // 
            this.txtRM_NUMBER.BackColor = System.Drawing.Color.White;
            this.txtRM_NUMBER.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRM_NUMBER.Location = new System.Drawing.Point(23, 96);
            this.txtRM_NUMBER.Name = "txtRM_NUMBER";
            this.txtRM_NUMBER.ReadOnly = true;
            this.txtRM_NUMBER.Size = new System.Drawing.Size(140, 23);
            this.txtRM_NUMBER.TabIndex = 8;
            this.txtRM_NUMBER.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "RM no :";
            // 
            // txtDepartment
            // 
            this.txtDepartment.BackColor = System.Drawing.Color.White;
            this.txtDepartment.Location = new System.Drawing.Point(23, 384);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(140, 23);
            this.txtDepartment.TabIndex = 6;
            this.txtDepartment.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(23, 240);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(140, 23);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.TabStop = false;
            // 
            // lbDerpartment
            // 
            this.lbDerpartment.AutoSize = true;
            this.lbDerpartment.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDerpartment.Location = new System.Drawing.Point(23, 368);
            this.lbDerpartment.Name = "lbDerpartment";
            this.lbDerpartment.Size = new System.Drawing.Size(58, 16);
            this.lbDerpartment.TabIndex = 0;
            this.lbDerpartment.Text = "Module :";
            // 
            // lbCurrecy_name
            // 
            this.lbCurrecy_name.AutoSize = true;
            this.lbCurrecy_name.ForeColor = System.Drawing.Color.Blue;
            this.lbCurrecy_name.Location = new System.Drawing.Point(171, 245);
            this.lbCurrecy_name.Name = "lbCurrecy_name";
            this.lbCurrecy_name.Size = new System.Drawing.Size(0, 16);
            this.lbCurrecy_name.TabIndex = 0;
            // 
            // txtTransDate
            // 
            this.txtTransDate.BackColor = System.Drawing.Color.White;
            this.txtTransDate.Location = new System.Drawing.Point(23, 337);
            this.txtTransDate.Name = "txtTransDate";
            this.txtTransDate.ReadOnly = true;
            this.txtTransDate.Size = new System.Drawing.Size(140, 23);
            this.txtTransDate.TabIndex = 5;
            this.txtTransDate.TabStop = false;
            // 
            // lbamount
            // 
            this.lbamount.AutoSize = true;
            this.lbamount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbamount.Location = new System.Drawing.Point(23, 224);
            this.lbamount.Name = "lbamount";
            this.lbamount.Size = new System.Drawing.Size(61, 16);
            this.lbamount.TabIndex = 0;
            this.lbamount.Text = "Amount :";
            // 
            // txtReceivingBank
            // 
            this.txtReceivingBank.BackColor = System.Drawing.Color.White;
            this.txtReceivingBank.Location = new System.Drawing.Point(23, 193);
            this.txtReceivingBank.Name = "txtReceivingBank";
            this.txtReceivingBank.ReadOnly = true;
            this.txtReceivingBank.Size = new System.Drawing.Size(140, 23);
            this.txtReceivingBank.TabIndex = 2;
            this.txtReceivingBank.TabStop = false;
            // 
            // lbtran_date
            // 
            this.lbtran_date.AutoSize = true;
            this.lbtran_date.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtran_date.Location = new System.Drawing.Point(23, 321);
            this.lbtran_date.Name = "lbtran_date";
            this.lbtran_date.Size = new System.Drawing.Size(77, 16);
            this.lbtran_date.TabIndex = 0;
            this.lbtran_date.Text = "Trans date :";
            // 
            // lbReceiving_name
            // 
            this.lbReceiving_name.AutoSize = true;
            this.lbReceiving_name.ForeColor = System.Drawing.Color.Blue;
            this.lbReceiving_name.Location = new System.Drawing.Point(171, 196);
            this.lbReceiving_name.Name = "lbReceiving_name";
            this.lbReceiving_name.Size = new System.Drawing.Size(0, 16);
            this.lbReceiving_name.TabIndex = 0;
            // 
            // lbreceiving_bank
            // 
            this.lbreceiving_bank.AutoSize = true;
            this.lbreceiving_bank.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbreceiving_bank.Location = new System.Drawing.Point(23, 176);
            this.lbreceiving_bank.Name = "lbreceiving_bank";
            this.lbreceiving_bank.Size = new System.Drawing.Size(103, 16);
            this.lbreceiving_bank.TabIndex = 0;
            this.lbreceiving_bank.Text = "Receiving bank :";
            // 
            // txtRMNo
            // 
            this.txtRMNo.BackColor = System.Drawing.Color.White;
            this.txtRMNo.Location = new System.Drawing.Point(23, 290);
            this.txtRMNo.Name = "txtRMNo";
            this.txtRMNo.ReadOnly = true;
            this.txtRMNo.Size = new System.Drawing.Size(140, 23);
            this.txtRMNo.TabIndex = 4;
            this.txtRMNo.TabStop = false;
            // 
            // lbSend_Bankname
            // 
            this.lbSend_Bankname.AutoSize = true;
            this.lbSend_Bankname.ForeColor = System.Drawing.Color.Blue;
            this.lbSend_Bankname.Location = new System.Drawing.Point(171, 149);
            this.lbSend_Bankname.Name = "lbSend_Bankname";
            this.lbSend_Bankname.Size = new System.Drawing.Size(0, 16);
            this.lbSend_Bankname.TabIndex = 0;
            // 
            // lbRm_no
            // 
            this.lbRm_no.AutoSize = true;
            this.lbRm_no.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRm_no.Location = new System.Drawing.Point(23, 271);
            this.lbRm_no.Name = "lbRm_no";
            this.lbRm_no.Size = new System.Drawing.Size(53, 16);
            this.lbRm_no.TabIndex = 0;
            this.lbRm_no.Text = "Ref no :";
            // 
            // txtSendingBank
            // 
            this.txtSendingBank.BackColor = System.Drawing.Color.White;
            this.txtSendingBank.Location = new System.Drawing.Point(23, 145);
            this.txtSendingBank.Name = "txtSendingBank";
            this.txtSendingBank.ReadOnly = true;
            this.txtSendingBank.Size = new System.Drawing.Size(140, 23);
            this.txtSendingBank.TabIndex = 1;
            this.txtSendingBank.TabStop = false;
            // 
            // lbsending_bank
            // 
            this.lbsending_bank.AutoSize = true;
            this.lbsending_bank.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsending_bank.Location = new System.Drawing.Point(23, 128);
            this.lbsending_bank.Name = "lbsending_bank";
            this.lbsending_bank.Size = new System.Drawing.Size(95, 16);
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
            this.tabHis.Size = new System.Drawing.Size(992, 509);
            this.tabHis.TabIndex = 1;
            this.tabHis.Text = "History";
            this.tabHis.UseVisualStyleBackColor = true;
            // 
            // txthistory
            // 
            this.txthistory.BackColor = System.Drawing.Color.White;
            this.txthistory.Location = new System.Drawing.Point(6, 7);
            this.txthistory.Multiline = true;
            this.txthistory.Name = "txthistory";
            this.txthistory.ReadOnly = true;
            this.txthistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txthistory.Size = new System.Drawing.Size(978, 495);
            this.txthistory.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(919, 549);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(836, 549);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(80, 30);
            this.cmdOK.TabIndex = 8;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdRelease
            // 
            this.cmdRelease.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRelease.Location = new System.Drawing.Point(753, 549);
            this.cmdRelease.Name = "cmdRelease";
            this.cmdRelease.Size = new System.Drawing.Size(80, 30);
            this.cmdRelease.TabIndex = 7;
            this.cmdRelease.Text = "&Release";
            this.cmdRelease.UseVisualStyleBackColor = true;
            this.cmdRelease.Click += new System.EventHandler(this.cmdRelease_Click);
            // 
            // cmdReject
            // 
            this.cmdReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReject.Location = new System.Drawing.Point(670, 549);
            this.cmdReject.Name = "cmdReject";
            this.cmdReject.Size = new System.Drawing.Size(80, 30);
            this.cmdReject.TabIndex = 6;
            this.cmdReject.Text = "&Reject";
            this.cmdReject.UseVisualStyleBackColor = true;
            this.cmdReject.Click += new System.EventHandler(this.cmdReject_Click);
            // 
            // cmdApprove
            // 
            this.cmdApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdApprove.Location = new System.Drawing.Point(586, 549);
            this.cmdApprove.Name = "cmdApprove";
            this.cmdApprove.Size = new System.Drawing.Size(80, 30);
            this.cmdApprove.TabIndex = 5;
            this.cmdApprove.Text = "&Approve";
            this.cmdApprove.UseVisualStyleBackColor = true;
            this.cmdApprove.Click += new System.EventHandler(this.cmdApprove_Click);
            // 
            // frmSwiftMsgManualDup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 601);
            this.Controls.Add(this.cmdApprove);
            this.Controls.Add(this.cmdReject);
            this.Controls.Add(this.cmdRelease);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.tabInfomation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSwiftMsgManualDup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SWIFT manual process - update status";
            this.Load += new System.EventHandler(this.frmSwiftMsgManualDup_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSwiftMsgManualDup_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSwiftMsgManualDup_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftMsgManualDup_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftMsgManualDup_KeyDown);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatusDescription;
        private System.Windows.Forms.GroupBox grbcontent;
        private System.Windows.Forms.TextBox txtcontent;
        private System.Windows.Forms.GroupBox grbtransaction;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lbDerpartment;
        private System.Windows.Forms.Label lbCurrecy_name;
        private System.Windows.Forms.TextBox txtTransDate;
        private System.Windows.Forms.Label lbamount;
        private System.Windows.Forms.TextBox txtReceivingBank;
        private System.Windows.Forms.Label lbtran_date;
        private System.Windows.Forms.Label lbReceiving_name;
        private System.Windows.Forms.Label lbreceiving_bank;
        private System.Windows.Forms.TextBox txtRMNo;
        private System.Windows.Forms.Label lbSend_Bankname;
        private System.Windows.Forms.Label lbRm_no;
        private System.Windows.Forms.TextBox txtSendingBank;
        private System.Windows.Forms.Label lbsending_bank;
        private System.Windows.Forms.TabPage tabHis;
        private System.Windows.Forms.TextBox txthistory;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TextBox txtRM_NUMBER;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMsgType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdRelease;
        private System.Windows.Forms.Button cmdReject;
        private System.Windows.Forms.Button cmdApprove;
    }
}