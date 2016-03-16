namespace BR.BRInterBank
{
    partial class frmIQSNew1
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
            this.txtRMNo = new System.Windows.Forms.TextBox();
            this.txtIQSTransNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTellerID = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblReceivingBranch = new System.Windows.Forms.Label();
            this.lblSendingBranch = new System.Windows.Forms.Label();
            this.txtTellerID = new System.Windows.Forms.TextBox();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtRefNumber = new System.Windows.Forms.TextBox();
            this.cboToBank = new System.Windows.Forms.ComboBox();
            this.cboFromBank = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIQSContent = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtVCBContent = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRMNo);
            this.groupBox1.Controls.Add(this.txtIQSTransNumber);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblTellerID);
            this.groupBox1.Controls.Add(this.lblAmount);
            this.groupBox1.Controls.Add(this.lblReceivingBranch);
            this.groupBox1.Controls.Add(this.lblSendingBranch);
            this.groupBox1.Controls.Add(this.txtTellerID);
            this.groupBox1.Controls.Add(this.dtpTransactionDate);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.txtRefNumber);
            this.groupBox1.Controls.Add(this.cboToBank);
            this.groupBox1.Controls.Add(this.cboFromBank);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 348);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IQS";
            // 
            // txtRMNo
            // 
            this.txtRMNo.Location = new System.Drawing.Point(136, 266);
            this.txtRMNo.Name = "txtRMNo";
            this.txtRMNo.Size = new System.Drawing.Size(200, 22);
            this.txtRMNo.TabIndex = 66;
            this.txtRMNo.TabStop = false;
            // 
            // txtIQSTransNumber
            // 
            this.txtIQSTransNumber.Location = new System.Drawing.Point(139, 320);
            this.txtIQSTransNumber.Name = "txtIQSTransNumber";
            this.txtIQSTransNumber.Size = new System.Drawing.Size(200, 22);
            this.txtIQSTransNumber.TabIndex = 37;
            this.txtIQSTransNumber.TabStop = false;
            this.txtIQSTransNumber.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "IQS trans number :";
            this.label7.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 266);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 65;
            this.label10.Text = "RM No :";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(139, 295);
            this.txtProductCode.MaxLength = 3;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(200, 22);
            this.txtProductCode.TabIndex = 0;
            this.txtProductCode.Visible = false;
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 16);
            this.label9.TabIndex = 43;
            this.label9.Text = "Product code :";
            this.label9.Visible = false;
            // 
            // lblTellerID
            // 
            this.lblTellerID.AutoSize = true;
            this.lblTellerID.Location = new System.Drawing.Point(339, 230);
            this.lblTellerID.Name = "lblTellerID";
            this.lblTellerID.Size = new System.Drawing.Size(0, 16);
            this.lblTellerID.TabIndex = 40;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.ForeColor = System.Drawing.Color.Blue;
            this.lblAmount.Location = new System.Drawing.Point(339, 130);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(0, 16);
            this.lblAmount.TabIndex = 39;
            // 
            // lblReceivingBranch
            // 
            this.lblReceivingBranch.AutoSize = true;
            this.lblReceivingBranch.ForeColor = System.Drawing.Color.Blue;
            this.lblReceivingBranch.Location = new System.Drawing.Point(243, 64);
            this.lblReceivingBranch.Name = "lblReceivingBranch";
            this.lblReceivingBranch.Size = new System.Drawing.Size(0, 16);
            this.lblReceivingBranch.TabIndex = 42;
            // 
            // lblSendingBranch
            // 
            this.lblSendingBranch.AutoSize = true;
            this.lblSendingBranch.ForeColor = System.Drawing.Color.Blue;
            this.lblSendingBranch.Location = new System.Drawing.Point(243, 29);
            this.lblSendingBranch.Name = "lblSendingBranch";
            this.lblSendingBranch.Size = new System.Drawing.Size(0, 16);
            this.lblSendingBranch.TabIndex = 41;
            // 
            // txtTellerID
            // 
            this.txtTellerID.Location = new System.Drawing.Point(136, 231);
            this.txtTellerID.Name = "txtTellerID";
            this.txtTellerID.Size = new System.Drawing.Size(200, 22);
            this.txtTellerID.TabIndex = 38;
            this.txtTellerID.TabStop = false;
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransactionDate.Location = new System.Drawing.Point(136, 161);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(200, 22);
            this.dtpTransactionDate.TabIndex = 36;
            this.dtpTransactionDate.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(136, 128);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 22);
            this.txtAmount.TabIndex = 35;
            this.txtAmount.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "Teller ID :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Transaction date :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Amount :";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(136, 195);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 22);
            this.dtpDate.TabIndex = 23;
            this.dtpDate.TabStop = false;
            // 
            // txtRefNumber
            // 
            this.txtRefNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefNumber.Location = new System.Drawing.Point(136, 95);
            this.txtRefNumber.Name = "txtRefNumber";
            this.txtRefNumber.Size = new System.Drawing.Size(200, 22);
            this.txtRefNumber.TabIndex = 30;
            this.txtRefNumber.TabStop = false;
            // 
            // cboToBank
            // 
            this.cboToBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboToBank.FormattingEnabled = true;
            this.cboToBank.Location = new System.Drawing.Point(136, 60);
            this.cboToBank.Name = "cboToBank";
            this.cboToBank.Size = new System.Drawing.Size(104, 24);
            this.cboToBank.TabIndex = 29;
            this.cboToBank.TabStop = false;
            // 
            // cboFromBank
            // 
            this.cboFromBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFromBank.FormattingEnabled = true;
            this.cboFromBank.Location = new System.Drawing.Point(136, 24);
            this.cboFromBank.Name = "cboFromBank";
            this.cboFromBank.Size = new System.Drawing.Size(104, 24);
            this.cboFromBank.TabIndex = 27;
            this.cboFromBank.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 25;
            this.label5.Text = "Date create:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Ref number :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "Receiving branch :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Sending branch :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIQSContent);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(4, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(895, 187);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IQS content";
            // 
            // txtIQSContent
            // 
            this.txtIQSContent.Location = new System.Drawing.Point(8, 19);
            this.txtIQSContent.Multiline = true;
            this.txtIQSContent.Name = "txtIQSContent";
            this.txtIQSContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIQSContent.Size = new System.Drawing.Size(880, 162);
            this.txtIQSContent.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(713, 553);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(80, 30);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(799, 552);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtVCBContent);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(525, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(374, 341);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "VCB content";
            // 
            // txtVCBContent
            // 
            this.txtVCBContent.Location = new System.Drawing.Point(9, 19);
            this.txtVCBContent.Multiline = true;
            this.txtVCBContent.Name = "txtVCBContent";
            this.txtVCBContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVCBContent.Size = new System.Drawing.Size(356, 316);
            this.txtVCBContent.TabIndex = 15;
            // 
            // frmIQSNew1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 594);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmIQSNew1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VCB_IQS message";
            this.Load += new System.EventHandler(this.frmIQSNew1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIQSNew1_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIQSNew1_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIQSNew1_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox txtIQSContent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtVCBContent;
        private System.Windows.Forms.Label lblTellerID;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblReceivingBranch;
        private System.Windows.Forms.Label lblSendingBranch;
        private System.Windows.Forms.TextBox txtTellerID;
        private System.Windows.Forms.TextBox txtIQSTransNumber;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtRefNumber;
        private System.Windows.Forms.ComboBox cboToBank;
        private System.Windows.Forms.ComboBox cboFromBank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRMNo;
        private System.Windows.Forms.Label label10;
    }
}