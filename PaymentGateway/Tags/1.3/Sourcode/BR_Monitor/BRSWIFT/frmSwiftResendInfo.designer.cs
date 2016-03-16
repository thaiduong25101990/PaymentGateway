namespace BR.BRSWIFT
{
    partial class frmSwiftResendInfo
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grbcontent = new System.Windows.Forms.GroupBox();
            this.txtcontent = new System.Windows.Forms.TextBox();
            this.grbtransaction = new System.Windows.Forms.GroupBox();
            this.txtRM_NUMBER = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCCYCD = new System.Windows.Forms.Label();
            this.txtdepartment = new System.Windows.Forms.TextBox();
            this.txtamount = new System.Windows.Forms.TextBox();
            this.lbDerpartment = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lbamount = new System.Windows.Forms.Label();
            this.txtreceiving_bank = new System.Windows.Forms.TextBox();
            this.lbtran_date = new System.Windows.Forms.Label();
            this.lbRecevi_name = new System.Windows.Forms.Label();
            this.lbreceiving_bank = new System.Windows.Forms.Label();
            this.txtMsgType = new System.Windows.Forms.TextBox();
            this.txtrm_no = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSend_bankname = new System.Windows.Forms.Label();
            this.lbRm_no = new System.Windows.Forms.Label();
            this.txtsending_bank = new System.Windows.Forms.TextBox();
            this.lbsending_bank = new System.Windows.Forms.Label();
            this.txthistory = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabInfomation = new System.Windows.Forms.TabControl();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.tabPage1.SuspendLayout();
            this.grbcontent.SuspendLayout();
            this.grbtransaction.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabInfomation.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(916, 528);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(380, 648);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(294, 648);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(119, 648);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(205, 648);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grbcontent);
            this.tabPage1.Controls.Add(this.grbtransaction);
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(983, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grbcontent
            // 
            this.grbcontent.Controls.Add(this.txtcontent);
            this.grbcontent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbcontent.Location = new System.Drawing.Point(519, 7);
            this.grbcontent.Name = "grbcontent";
            this.grbcontent.Size = new System.Drawing.Size(451, 465);
            this.grbcontent.TabIndex = 2;
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
            this.txtcontent.Size = new System.Drawing.Size(438, 437);
            this.txtcontent.TabIndex = 7;
            // 
            // grbtransaction
            // 
            this.grbtransaction.Controls.Add(this.txtRM_NUMBER);
            this.grbtransaction.Controls.Add(this.label2);
            this.grbtransaction.Controls.Add(this.lbCCYCD);
            this.grbtransaction.Controls.Add(this.txtdepartment);
            this.grbtransaction.Controls.Add(this.txtamount);
            this.grbtransaction.Controls.Add(this.lbDerpartment);
            this.grbtransaction.Controls.Add(this.txtStatus);
            this.grbtransaction.Controls.Add(this.lbamount);
            this.grbtransaction.Controls.Add(this.txtreceiving_bank);
            this.grbtransaction.Controls.Add(this.lbtran_date);
            this.grbtransaction.Controls.Add(this.lbRecevi_name);
            this.grbtransaction.Controls.Add(this.lbreceiving_bank);
            this.grbtransaction.Controls.Add(this.txtMsgType);
            this.grbtransaction.Controls.Add(this.txtrm_no);
            this.grbtransaction.Controls.Add(this.label1);
            this.grbtransaction.Controls.Add(this.lbSend_bankname);
            this.grbtransaction.Controls.Add(this.lbRm_no);
            this.grbtransaction.Controls.Add(this.txtsending_bank);
            this.grbtransaction.Controls.Add(this.lbsending_bank);
            this.grbtransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbtransaction.Location = new System.Drawing.Point(6, 7);
            this.grbtransaction.Name = "grbtransaction";
            this.grbtransaction.Size = new System.Drawing.Size(507, 465);
            this.grbtransaction.TabIndex = 2;
            this.grbtransaction.TabStop = false;
            this.grbtransaction.Text = "Transaction";
            // 
            // txtRM_NUMBER
            // 
            this.txtRM_NUMBER.BackColor = System.Drawing.Color.White;
            this.txtRM_NUMBER.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRM_NUMBER.Location = new System.Drawing.Point(25, 48);
            this.txtRM_NUMBER.Name = "txtRM_NUMBER";
            this.txtRM_NUMBER.ReadOnly = true;
            this.txtRM_NUMBER.Size = new System.Drawing.Size(159, 23);
            this.txtRM_NUMBER.TabIndex = 11;
            this.txtRM_NUMBER.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "RM no :";
            // 
            // lbCCYCD
            // 
            this.lbCCYCD.AutoSize = true;
            this.lbCCYCD.ForeColor = System.Drawing.Color.Blue;
            this.lbCCYCD.Location = new System.Drawing.Point(192, 260);
            this.lbCCYCD.Name = "lbCCYCD";
            this.lbCCYCD.Size = new System.Drawing.Size(0, 16);
            this.lbCCYCD.TabIndex = 9;
            // 
            // txtdepartment
            // 
            this.txtdepartment.BackColor = System.Drawing.Color.White;
            this.txtdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdepartment.Location = new System.Drawing.Point(25, 418);
            this.txtdepartment.Name = "txtdepartment";
            this.txtdepartment.ReadOnly = true;
            this.txtdepartment.Size = new System.Drawing.Size(159, 23);
            this.txtdepartment.TabIndex = 6;
            this.txtdepartment.TabStop = false;
            // 
            // txtamount
            // 
            this.txtamount.BackColor = System.Drawing.Color.White;
            this.txtamount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamount.Location = new System.Drawing.Point(25, 254);
            this.txtamount.Name = "txtamount";
            this.txtamount.ReadOnly = true;
            this.txtamount.Size = new System.Drawing.Size(159, 23);
            this.txtamount.TabIndex = 3;
            this.txtamount.TabStop = false;
            // 
            // lbDerpartment
            // 
            this.lbDerpartment.AutoSize = true;
            this.lbDerpartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDerpartment.Location = new System.Drawing.Point(25, 399);
            this.lbDerpartment.Name = "lbDerpartment";
            this.lbDerpartment.Size = new System.Drawing.Size(59, 16);
            this.lbDerpartment.TabIndex = 0;
            this.lbDerpartment.Text = "Module :";
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(25, 363);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(159, 23);
            this.txtStatus.TabIndex = 5;
            this.txtStatus.TabStop = false;
            // 
            // lbamount
            // 
            this.lbamount.AutoSize = true;
            this.lbamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbamount.Location = new System.Drawing.Point(25, 235);
            this.lbamount.Name = "lbamount";
            this.lbamount.Size = new System.Drawing.Size(59, 16);
            this.lbamount.TabIndex = 0;
            this.lbamount.Text = "Amount :";
            // 
            // txtreceiving_bank
            // 
            this.txtreceiving_bank.BackColor = System.Drawing.Color.White;
            this.txtreceiving_bank.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiving_bank.Location = new System.Drawing.Point(25, 200);
            this.txtreceiving_bank.Name = "txtreceiving_bank";
            this.txtreceiving_bank.ReadOnly = true;
            this.txtreceiving_bank.Size = new System.Drawing.Size(159, 23);
            this.txtreceiving_bank.TabIndex = 2;
            this.txtreceiving_bank.TabStop = false;
            // 
            // lbtran_date
            // 
            this.lbtran_date.AutoSize = true;
            this.lbtran_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtran_date.Location = new System.Drawing.Point(25, 344);
            this.lbtran_date.Name = "lbtran_date";
            this.lbtran_date.Size = new System.Drawing.Size(51, 16);
            this.lbtran_date.TabIndex = 0;
            this.lbtran_date.Text = "Status :";
            // 
            // lbRecevi_name
            // 
            this.lbRecevi_name.AutoSize = true;
            this.lbRecevi_name.ForeColor = System.Drawing.Color.Blue;
            this.lbRecevi_name.Location = new System.Drawing.Point(192, 206);
            this.lbRecevi_name.Name = "lbRecevi_name";
            this.lbRecevi_name.Size = new System.Drawing.Size(0, 16);
            this.lbRecevi_name.TabIndex = 0;
            // 
            // lbreceiving_bank
            // 
            this.lbreceiving_bank.AutoSize = true;
            this.lbreceiving_bank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbreceiving_bank.Location = new System.Drawing.Point(25, 181);
            this.lbreceiving_bank.Name = "lbreceiving_bank";
            this.lbreceiving_bank.Size = new System.Drawing.Size(108, 16);
            this.lbreceiving_bank.TabIndex = 0;
            this.lbreceiving_bank.Text = "Receiving bank :";
            // 
            // txtMsgType
            // 
            this.txtMsgType.BackColor = System.Drawing.Color.White;
            this.txtMsgType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsgType.Location = new System.Drawing.Point(25, 99);
            this.txtMsgType.Name = "txtMsgType";
            this.txtMsgType.ReadOnly = true;
            this.txtMsgType.Size = new System.Drawing.Size(159, 23);
            this.txtMsgType.TabIndex = 0;
            this.txtMsgType.TabStop = false;
            // 
            // txtrm_no
            // 
            this.txtrm_no.BackColor = System.Drawing.Color.White;
            this.txtrm_no.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrm_no.Location = new System.Drawing.Point(25, 309);
            this.txtrm_no.Name = "txtrm_no";
            this.txtrm_no.ReadOnly = true;
            this.txtrm_no.Size = new System.Drawing.Size(159, 23);
            this.txtrm_no.TabIndex = 4;
            this.txtrm_no.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Message Type :";
            // 
            // lbSend_bankname
            // 
            this.lbSend_bankname.AutoSize = true;
            this.lbSend_bankname.ForeColor = System.Drawing.Color.Blue;
            this.lbSend_bankname.Location = new System.Drawing.Point(192, 153);
            this.lbSend_bankname.Name = "lbSend_bankname";
            this.lbSend_bankname.Size = new System.Drawing.Size(0, 16);
            this.lbSend_bankname.TabIndex = 0;
            // 
            // lbRm_no
            // 
            this.lbRm_no.AutoSize = true;
            this.lbRm_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRm_no.Location = new System.Drawing.Point(25, 290);
            this.lbRm_no.Name = "lbRm_no";
            this.lbRm_no.Size = new System.Drawing.Size(53, 16);
            this.lbRm_no.TabIndex = 0;
            this.lbRm_no.Text = "Ref no :";
            // 
            // txtsending_bank
            // 
            this.txtsending_bank.BackColor = System.Drawing.Color.White;
            this.txtsending_bank.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsending_bank.Location = new System.Drawing.Point(25, 147);
            this.txtsending_bank.Name = "txtsending_bank";
            this.txtsending_bank.ReadOnly = true;
            this.txtsending_bank.Size = new System.Drawing.Size(159, 23);
            this.txtsending_bank.TabIndex = 1;
            this.txtsending_bank.TabStop = false;
            // 
            // lbsending_bank
            // 
            this.lbsending_bank.AutoSize = true;
            this.lbsending_bank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsending_bank.Location = new System.Drawing.Point(25, 128);
            this.lbsending_bank.Name = "lbsending_bank";
            this.lbsending_bank.Size = new System.Drawing.Size(97, 16);
            this.lbsending_bank.TabIndex = 0;
            this.lbsending_bank.Text = "Sending bank :";
            // 
            // txthistory
            // 
            this.txthistory.BackColor = System.Drawing.Color.White;
            this.txthistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthistory.Location = new System.Drawing.Point(6, 7);
            this.txthistory.Multiline = true;
            this.txthistory.Name = "txthistory";
            this.txthistory.ReadOnly = true;
            this.txthistory.Size = new System.Drawing.Size(971, 465);
            this.txthistory.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txthistory);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(983, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabInfomation
            // 
            this.tabInfomation.Controls.Add(this.tabPage1);
            this.tabInfomation.Controls.Add(this.tabPage2);
            this.tabInfomation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfomation.Location = new System.Drawing.Point(9, 13);
            this.tabInfomation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInfomation.Name = "tabInfomation";
            this.tabInfomation.SelectedIndex = 0;
            this.tabInfomation.Size = new System.Drawing.Size(991, 508);
            this.tabInfomation.TabIndex = 2;
            this.tabInfomation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabInfomation_MouseDown);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(830, 528);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 0;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // frmSwiftResendInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 578);
            this.Controls.Add(this.tabInfomation);
            this.Controls.Add(this.cmdPrint);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSwiftResendInfo";
            this.Text = "Swift message information";
            this.Load += new System.EventHandler(this.frmSwiftResendInfo_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSwiftResendInfo_MouseDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSwiftResendInfo_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftResendInfo_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftResendInfo_KeyDown);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.tabInfomation, 0);
            this.tabPage1.ResumeLayout(false);
            this.grbcontent.ResumeLayout(false);
            this.grbcontent.PerformLayout();
            this.grbtransaction.ResumeLayout(false);
            this.grbtransaction.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabInfomation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grbcontent;
        private System.Windows.Forms.TextBox txtcontent;
        private System.Windows.Forms.GroupBox grbtransaction;
        private System.Windows.Forms.TextBox txtdepartment;
        private System.Windows.Forms.TextBox txtamount;
        private System.Windows.Forms.Label lbDerpartment;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lbamount;
        private System.Windows.Forms.TextBox txtreceiving_bank;
        private System.Windows.Forms.Label lbtran_date;
        private System.Windows.Forms.Label lbRecevi_name;
        private System.Windows.Forms.Label lbreceiving_bank;
        private System.Windows.Forms.TextBox txtrm_no;
        private System.Windows.Forms.Label lbSend_bankname;
        private System.Windows.Forms.Label lbRm_no;
        private System.Windows.Forms.TextBox txtsending_bank;
        private System.Windows.Forms.Label lbsending_bank;
        private System.Windows.Forms.TextBox txthistory;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabInfomation;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Label lbCCYCD;
        private System.Windows.Forms.TextBox txtMsgType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRM_NUMBER;
        private System.Windows.Forms.Label label2;
    }
}