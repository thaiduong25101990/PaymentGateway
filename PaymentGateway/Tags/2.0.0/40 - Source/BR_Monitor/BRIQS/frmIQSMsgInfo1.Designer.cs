namespace BR.BRIQS
{
    partial class frmIQSMsgInfo1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grbcontent = new System.Windows.Forms.GroupBox();
            this.txtcontent = new System.Windows.Forms.TextBox();
            this.grbtransaction = new System.Windows.Forms.GroupBox();
            this.txtREFnumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReceivingBranch = new System.Windows.Forms.TextBox();
            this.txtSendingBranch = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblReceivingBranch = new System.Windows.Forms.Label();
            this.lblSendingBranch = new System.Windows.Forms.Label();
            this.txtTellerID = new System.Windows.Forms.TextBox();
            this.txtIQSTransNumber = new System.Windows.Forms.TextBox();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtRMRef = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txthistory = new System.Windows.Forms.TextBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grbcontent.SuspendLayout();
            this.grbtransaction.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(865, 447);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(286, 621);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(200, 621);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(25, 621);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(111, 621);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(939, 434);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grbcontent);
            this.tabPage1.Controls.Add(this.grbtransaction);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(931, 408);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grbcontent
            // 
            this.grbcontent.Controls.Add(this.txtcontent);
            this.grbcontent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbcontent.Location = new System.Drawing.Point(505, 7);
            this.grbcontent.Name = "grbcontent";
            this.grbcontent.Size = new System.Drawing.Size(419, 395);
            this.grbcontent.TabIndex = 17;
            this.grbcontent.TabStop = false;
            this.grbcontent.Text = "Content ";
            // 
            // txtcontent
            // 
            this.txtcontent.BackColor = System.Drawing.Color.White;
            this.txtcontent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontent.Location = new System.Drawing.Point(6, 22);
            this.txtcontent.Multiline = true;
            this.txtcontent.Name = "txtcontent";
            this.txtcontent.ReadOnly = true;
            this.txtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtcontent.Size = new System.Drawing.Size(401, 365);
            this.txtcontent.TabIndex = 4;
            this.txtcontent.TabStop = false;
            // 
            // grbtransaction
            // 
            this.grbtransaction.Controls.Add(this.txtREFnumber);
            this.grbtransaction.Controls.Add(this.label2);
            this.grbtransaction.Controls.Add(this.txtReceivingBranch);
            this.grbtransaction.Controls.Add(this.txtSendingBranch);
            this.grbtransaction.Controls.Add(this.txtStatus);
            this.grbtransaction.Controls.Add(this.label12);
            this.grbtransaction.Controls.Add(this.lblAmount);
            this.grbtransaction.Controls.Add(this.txtProductCode);
            this.grbtransaction.Controls.Add(this.label9);
            this.grbtransaction.Controls.Add(this.lblReceivingBranch);
            this.grbtransaction.Controls.Add(this.lblSendingBranch);
            this.grbtransaction.Controls.Add(this.txtTellerID);
            this.grbtransaction.Controls.Add(this.txtIQSTransNumber);
            this.grbtransaction.Controls.Add(this.dtpTransactionDate);
            this.grbtransaction.Controls.Add(this.txtAmount);
            this.grbtransaction.Controls.Add(this.label8);
            this.grbtransaction.Controls.Add(this.label7);
            this.grbtransaction.Controls.Add(this.label6);
            this.grbtransaction.Controls.Add(this.label3);
            this.grbtransaction.Controls.Add(this.dtpDate);
            this.grbtransaction.Controls.Add(this.txtRMRef);
            this.grbtransaction.Controls.Add(this.label5);
            this.grbtransaction.Controls.Add(this.label4);
            this.grbtransaction.Controls.Add(this.label10);
            this.grbtransaction.Controls.Add(this.label11);
            this.grbtransaction.Controls.Add(this.label1);
            this.grbtransaction.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbtransaction.Location = new System.Drawing.Point(6, 6);
            this.grbtransaction.Name = "grbtransaction";
            this.grbtransaction.Size = new System.Drawing.Size(493, 394);
            this.grbtransaction.TabIndex = 2;
            this.grbtransaction.TabStop = false;
            this.grbtransaction.Text = "Transaction";
            // 
            // txtREFnumber
            // 
            this.txtREFnumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtREFnumber.Location = new System.Drawing.Point(151, 63);
            this.txtREFnumber.Name = "txtREFnumber";
            this.txtREFnumber.ReadOnly = true;
            this.txtREFnumber.Size = new System.Drawing.Size(167, 23);
            this.txtREFnumber.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 49;
            this.label2.Text = "IQS trans number :";
            // 
            // txtReceivingBranch
            // 
            this.txtReceivingBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceivingBranch.Location = new System.Drawing.Point(151, 161);
            this.txtReceivingBranch.Name = "txtReceivingBranch";
            this.txtReceivingBranch.ReadOnly = true;
            this.txtReceivingBranch.Size = new System.Drawing.Size(62, 22);
            this.txtReceivingBranch.TabIndex = 48;
            this.txtReceivingBranch.TabStop = false;
            // 
            // txtSendingBranch
            // 
            this.txtSendingBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendingBranch.Location = new System.Drawing.Point(151, 127);
            this.txtSendingBranch.Name = "txtSendingBranch";
            this.txtSendingBranch.ReadOnly = true;
            this.txtSendingBranch.Size = new System.Drawing.Size(63, 22);
            this.txtSendingBranch.TabIndex = 47;
            this.txtSendingBranch.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(151, 227);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(167, 22);
            this.txtStatus.TabIndex = 46;
            this.txtStatus.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(23, 230);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 16);
            this.label12.TabIndex = 45;
            this.label12.Text = "Status :";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.ForeColor = System.Drawing.Color.Blue;
            this.lblAmount.Location = new System.Drawing.Point(322, 198);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(0, 16);
            this.lblAmount.TabIndex = 44;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductCode.Location = new System.Drawing.Point(151, 358);
            this.txtProductCode.MaxLength = 3;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.ReadOnly = true;
            this.txtProductCode.Size = new System.Drawing.Size(167, 22);
            this.txtProductCode.TabIndex = 25;
            this.txtProductCode.TabStop = false;
            this.txtProductCode.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 16);
            this.label9.TabIndex = 43;
            this.label9.Text = "Product code :";
            this.label9.Visible = false;
            // 
            // lblReceivingBranch
            // 
            this.lblReceivingBranch.AutoSize = true;
            this.lblReceivingBranch.ForeColor = System.Drawing.Color.Blue;
            this.lblReceivingBranch.Location = new System.Drawing.Point(217, 165);
            this.lblReceivingBranch.Name = "lblReceivingBranch";
            this.lblReceivingBranch.Size = new System.Drawing.Size(0, 16);
            this.lblReceivingBranch.TabIndex = 42;
            // 
            // lblSendingBranch
            // 
            this.lblSendingBranch.AutoSize = true;
            this.lblSendingBranch.ForeColor = System.Drawing.Color.Blue;
            this.lblSendingBranch.Location = new System.Drawing.Point(218, 130);
            this.lblSendingBranch.Name = "lblSendingBranch";
            this.lblSendingBranch.Size = new System.Drawing.Size(0, 16);
            this.lblSendingBranch.TabIndex = 41;
            // 
            // txtTellerID
            // 
            this.txtTellerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTellerID.Location = new System.Drawing.Point(151, 328);
            this.txtTellerID.Name = "txtTellerID";
            this.txtTellerID.ReadOnly = true;
            this.txtTellerID.Size = new System.Drawing.Size(167, 22);
            this.txtTellerID.TabIndex = 40;
            this.txtTellerID.TabStop = false;
            // 
            // txtIQSTransNumber
            // 
            this.txtIQSTransNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIQSTransNumber.Location = new System.Drawing.Point(151, 32);
            this.txtIQSTransNumber.Name = "txtIQSTransNumber";
            this.txtIQSTransNumber.ReadOnly = true;
            this.txtIQSTransNumber.Size = new System.Drawing.Size(167, 22);
            this.txtIQSTransNumber.TabIndex = 39;
            this.txtIQSTransNumber.TabStop = false;
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTransactionDate.Enabled = false;
            this.dtpTransactionDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransactionDate.Location = new System.Drawing.Point(151, 261);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(167, 22);
            this.dtpTransactionDate.TabIndex = 38;
            this.dtpTransactionDate.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(151, 195);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(167, 22);
            this.txtAmount.TabIndex = 37;
            this.txtAmount.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 331);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 16);
            this.label8.TabIndex = 36;
            this.label8.Text = "Teller ID :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 34;
            this.label7.Text = "REF number :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Transaction date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Amount :";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dtpDate.Enabled = false;
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(151, 295);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(167, 22);
            this.dtpDate.TabIndex = 24;
            this.dtpDate.TabStop = false;
            this.dtpDate.Value = new System.DateTime(2008, 8, 13, 8, 1, 0, 0);
            // 
            // txtRMRef
            // 
            this.txtRMRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRMRef.Location = new System.Drawing.Point(151, 95);
            this.txtRMRef.Name = "txtRMRef";
            this.txtRMRef.ReadOnly = true;
            this.txtRMRef.Size = new System.Drawing.Size(167, 22);
            this.txtRMRef.TabIndex = 32;
            this.txtRMRef.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Date create:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "RM number :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(23, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "Receiving branch :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Sending branch :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(341, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txthistory);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(931, 408);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txthistory
            // 
            this.txthistory.BackColor = System.Drawing.Color.White;
            this.txthistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthistory.Location = new System.Drawing.Point(6, 6);
            this.txthistory.Multiline = true;
            this.txthistory.Name = "txthistory";
            this.txthistory.ReadOnly = true;
            this.txthistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txthistory.Size = new System.Drawing.Size(923, 489);
            this.txthistory.TabIndex = 1;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(782, 447);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 0;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // frmIQSMsgInfo1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 489);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmdPrint);
            this.Name = "frmIQSMsgInfo1";
            this.Text = "IQS message";
            this.Load += new System.EventHandler(this.frmIQSMsgInfo1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIQSMsgInfo1_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIQSMsgInfo1_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIQSMsgInfo1_KeyDown);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grbcontent.ResumeLayout(false);
            this.grbcontent.PerformLayout();
            this.grbtransaction.ResumeLayout(false);
            this.grbtransaction.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grbtransaction;
        private System.Windows.Forms.GroupBox grbcontent;
        private System.Windows.Forms.TextBox txtcontent;
        private System.Windows.Forms.TextBox txthistory;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblReceivingBranch;
        private System.Windows.Forms.Label lblSendingBranch;
        private System.Windows.Forms.TextBox txtTellerID;
        private System.Windows.Forms.TextBox txtIQSTransNumber;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtRMRef;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSendingBranch;
        private System.Windows.Forms.TextBox txtReceivingBranch;
        private System.Windows.Forms.TextBox txtREFnumber;
        private System.Windows.Forms.Label label2;
    }
}