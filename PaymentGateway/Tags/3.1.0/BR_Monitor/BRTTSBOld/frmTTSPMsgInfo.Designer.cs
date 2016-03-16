namespace BR.BRTTSB
{
    partial class frmTTSPMsgInfo
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
            this.grbtransaction = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lbccycd = new System.Windows.Forms.Label();
            this.txtdepartment = new System.Windows.Forms.TextBox();
            this.txtamount = new System.Windows.Forms.TextBox();
            this.lbDerpartment = new System.Windows.Forms.Label();
            this.txttran_date = new System.Windows.Forms.TextBox();
            this.lbamount = new System.Windows.Forms.Label();
            this.txtreceiving_bank = new System.Windows.Forms.TextBox();
            this.lbtran_date = new System.Windows.Forms.Label();
            this.lbRece_bankname = new System.Windows.Forms.Label();
            this.lbreceiving_bank = new System.Windows.Forms.Label();
            this.txtrm_no = new System.Windows.Forms.TextBox();
            this.lbSend_bankname = new System.Windows.Forms.Label();
            this.lbRm_no = new System.Windows.Forms.Label();
            this.txtRmno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsending_bank = new System.Windows.Forms.TextBox();
            this.lbsending_bank = new System.Windows.Forms.Label();
            this.tabInfomation = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grbcontent = new System.Windows.Forms.GroupBox();
            this.txtcontent = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txthistory = new System.Windows.Forms.TextBox();
            this.cmdiqs = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grbtransaction.SuspendLayout();
            this.tabInfomation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grbcontent.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(921, 551);
            this.cmdClose.TabIndex = 20;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(361, 605);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(275, 605);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(100, 605);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(186, 605);
            // 
            // grbtransaction
            // 
            this.grbtransaction.Controls.Add(this.dateTimePicker1);
            this.grbtransaction.Controls.Add(this.lbccycd);
            this.grbtransaction.Controls.Add(this.txtdepartment);
            this.grbtransaction.Controls.Add(this.txtamount);
            this.grbtransaction.Controls.Add(this.lbDerpartment);
            this.grbtransaction.Controls.Add(this.txttran_date);
            this.grbtransaction.Controls.Add(this.lbamount);
            this.grbtransaction.Controls.Add(this.txtreceiving_bank);
            this.grbtransaction.Controls.Add(this.lbtran_date);
            this.grbtransaction.Controls.Add(this.lbRece_bankname);
            this.grbtransaction.Controls.Add(this.lbreceiving_bank);
            this.grbtransaction.Controls.Add(this.txtrm_no);
            this.grbtransaction.Controls.Add(this.lbSend_bankname);
            this.grbtransaction.Controls.Add(this.lbRm_no);
            this.grbtransaction.Controls.Add(this.txtRmno);
            this.grbtransaction.Controls.Add(this.label1);
            this.grbtransaction.Controls.Add(this.txtsending_bank);
            this.grbtransaction.Controls.Add(this.lbsending_bank);
            this.grbtransaction.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbtransaction.Location = new System.Drawing.Point(6, 7);
            this.grbtransaction.Name = "grbtransaction";
            this.grbtransaction.Size = new System.Drawing.Size(500, 487);
            this.grbtransaction.TabIndex = 1;
            this.grbtransaction.TabStop = false;
            this.grbtransaction.Text = "Transaction";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(24, 318);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(159, 23);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // lbccycd
            // 
            this.lbccycd.AutoSize = true;
            this.lbccycd.ForeColor = System.Drawing.Color.Blue;
            this.lbccycd.Location = new System.Drawing.Point(190, 266);
            this.lbccycd.Name = "lbccycd";
            this.lbccycd.Size = new System.Drawing.Size(0, 16);
            this.lbccycd.TabIndex = 8;
            // 
            // txtdepartment
            // 
            this.txtdepartment.BackColor = System.Drawing.Color.White;
            this.txtdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdepartment.Location = new System.Drawing.Point(24, 373);
            this.txtdepartment.Name = "txtdepartment";
            this.txtdepartment.ReadOnly = true;
            this.txtdepartment.Size = new System.Drawing.Size(160, 23);
            this.txtdepartment.TabIndex = 6;
            // 
            // txtamount
            // 
            this.txtamount.BackColor = System.Drawing.Color.White;
            this.txtamount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamount.Location = new System.Drawing.Point(24, 261);
            this.txtamount.Name = "txtamount";
            this.txtamount.ReadOnly = true;
            this.txtamount.Size = new System.Drawing.Size(160, 23);
            this.txtamount.TabIndex = 3;
            // 
            // lbDerpartment
            // 
            this.lbDerpartment.AutoSize = true;
            this.lbDerpartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDerpartment.Location = new System.Drawing.Point(24, 354);
            this.lbDerpartment.Name = "lbDerpartment";
            this.lbDerpartment.Size = new System.Drawing.Size(58, 16);
            this.lbDerpartment.TabIndex = 0;
            this.lbDerpartment.Text = "Module :";
            // 
            // txttran_date
            // 
            this.txttran_date.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttran_date.Location = new System.Drawing.Point(24, 319);
            this.txttran_date.Name = "txttran_date";
            this.txttran_date.ReadOnly = true;
            this.txttran_date.Size = new System.Drawing.Size(160, 23);
            this.txttran_date.TabIndex = 5;
            this.txttran_date.Visible = false;
            // 
            // lbamount
            // 
            this.lbamount.AutoSize = true;
            this.lbamount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbamount.Location = new System.Drawing.Point(24, 242);
            this.lbamount.Name = "lbamount";
            this.lbamount.Size = new System.Drawing.Size(61, 16);
            this.lbamount.TabIndex = 0;
            this.lbamount.Text = "Amount :";
            // 
            // txtreceiving_bank
            // 
            this.txtreceiving_bank.BackColor = System.Drawing.Color.White;
            this.txtreceiving_bank.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiving_bank.Location = new System.Drawing.Point(24, 98);
            this.txtreceiving_bank.Name = "txtreceiving_bank";
            this.txtreceiving_bank.ReadOnly = true;
            this.txtreceiving_bank.Size = new System.Drawing.Size(160, 23);
            this.txtreceiving_bank.TabIndex = 2;
            // 
            // lbtran_date
            // 
            this.lbtran_date.AutoSize = true;
            this.lbtran_date.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtran_date.Location = new System.Drawing.Point(24, 299);
            this.lbtran_date.Name = "lbtran_date";
            this.lbtran_date.Size = new System.Drawing.Size(79, 16);
            this.lbtran_date.TabIndex = 0;
            this.lbtran_date.Text = "Trans date :";
            // 
            // lbRece_bankname
            // 
            this.lbRece_bankname.AutoSize = true;
            this.lbRece_bankname.ForeColor = System.Drawing.Color.Blue;
            this.lbRece_bankname.Location = new System.Drawing.Point(190, 104);
            this.lbRece_bankname.Name = "lbRece_bankname";
            this.lbRece_bankname.Size = new System.Drawing.Size(0, 16);
            this.lbRece_bankname.TabIndex = 0;
            // 
            // lbreceiving_bank
            // 
            this.lbreceiving_bank.AutoSize = true;
            this.lbreceiving_bank.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbreceiving_bank.Location = new System.Drawing.Point(24, 79);
            this.lbreceiving_bank.Name = "lbreceiving_bank";
            this.lbreceiving_bank.Size = new System.Drawing.Size(102, 16);
            this.lbreceiving_bank.TabIndex = 0;
            this.lbreceiving_bank.Text = "Receiving bank :";
            // 
            // txtrm_no
            // 
            this.txtrm_no.BackColor = System.Drawing.Color.White;
            this.txtrm_no.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrm_no.Location = new System.Drawing.Point(24, 211);
            this.txtrm_no.Name = "txtrm_no";
            this.txtrm_no.ReadOnly = true;
            this.txtrm_no.Size = new System.Drawing.Size(160, 23);
            this.txtrm_no.TabIndex = 4;
            // 
            // lbSend_bankname
            // 
            this.lbSend_bankname.AutoSize = true;
            this.lbSend_bankname.ForeColor = System.Drawing.Color.Blue;
            this.lbSend_bankname.Location = new System.Drawing.Point(190, 49);
            this.lbSend_bankname.Name = "lbSend_bankname";
            this.lbSend_bankname.Size = new System.Drawing.Size(0, 16);
            this.lbSend_bankname.TabIndex = 0;
            // 
            // lbRm_no
            // 
            this.lbRm_no.AutoSize = true;
            this.lbRm_no.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRm_no.Location = new System.Drawing.Point(24, 191);
            this.lbRm_no.Name = "lbRm_no";
            this.lbRm_no.Size = new System.Drawing.Size(54, 16);
            this.lbRm_no.TabIndex = 0;
            this.lbRm_no.Text = "Ref no :";
            // 
            // txtRmno
            // 
            this.txtRmno.BackColor = System.Drawing.Color.White;
            this.txtRmno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRmno.Location = new System.Drawing.Point(24, 151);
            this.txtRmno.Name = "txtRmno";
            this.txtRmno.ReadOnly = true;
            this.txtRmno.Size = new System.Drawing.Size(160, 23);
            this.txtRmno.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "RM no :";
            // 
            // txtsending_bank
            // 
            this.txtsending_bank.BackColor = System.Drawing.Color.White;
            this.txtsending_bank.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsending_bank.Location = new System.Drawing.Point(24, 43);
            this.txtsending_bank.Name = "txtsending_bank";
            this.txtsending_bank.ReadOnly = true;
            this.txtsending_bank.Size = new System.Drawing.Size(160, 23);
            this.txtsending_bank.TabIndex = 1;
            // 
            // lbsending_bank
            // 
            this.lbsending_bank.AutoSize = true;
            this.lbsending_bank.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsending_bank.Location = new System.Drawing.Point(24, 24);
            this.lbsending_bank.Name = "lbsending_bank";
            this.lbsending_bank.Size = new System.Drawing.Size(94, 16);
            this.lbsending_bank.TabIndex = 0;
            this.lbsending_bank.Text = "Sending bank :";
            // 
            // tabInfomation
            // 
            this.tabInfomation.Controls.Add(this.tabPage1);
            this.tabInfomation.Controls.Add(this.tabPage2);
            this.tabInfomation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfomation.Location = new System.Drawing.Point(11, 14);
            this.tabInfomation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInfomation.Name = "tabInfomation";
            this.tabInfomation.SelectedIndex = 0;
            this.tabInfomation.Size = new System.Drawing.Size(990, 530);
            this.tabInfomation.TabIndex = 2;
            this.tabInfomation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabInfomation_MouseDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grbcontent);
            this.tabPage1.Controls.Add(this.grbtransaction);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(982, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grbcontent
            // 
            this.grbcontent.Controls.Add(this.txtcontent);
            this.grbcontent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbcontent.Location = new System.Drawing.Point(512, 8);
            this.grbcontent.Name = "grbcontent";
            this.grbcontent.Size = new System.Drawing.Size(464, 485);
            this.grbcontent.TabIndex = 16;
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
            this.txtcontent.Size = new System.Drawing.Size(452, 457);
            this.txtcontent.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txthistory);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(982, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txthistory
            // 
            this.txthistory.BackColor = System.Drawing.Color.White;
            this.txthistory.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthistory.Location = new System.Drawing.Point(6, 7);
            this.txthistory.Multiline = true;
            this.txthistory.Name = "txthistory";
            this.txthistory.ReadOnly = true;
            this.txthistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txthistory.Size = new System.Drawing.Size(970, 487);
            this.txthistory.TabIndex = 0;
            // 
            // cmdiqs
            // 
            this.cmdiqs.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdiqs.Location = new System.Drawing.Point(693, 551);
            this.cmdiqs.Name = "cmdiqs";
            this.cmdiqs.Size = new System.Drawing.Size(80, 30);
            this.cmdiqs.TabIndex = 19;
            this.cmdiqs.Text = "&IQS Msg ";
            this.toolTip1.SetToolTip(this.cmdiqs, "IQS Message !");
            this.cmdiqs.UseVisualStyleBackColor = true;
            this.cmdiqs.Visible = false;
            this.cmdiqs.Click += new System.EventHandler(this.cmdiqs_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(835, 551);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 18;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Create";
            // 
            // frmTTSPMsgInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 593);
            this.Controls.Add(this.tabInfomation);
            this.Controls.Add(this.cmdiqs);
            this.Controls.Add(this.cmdPrint);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTTSPMsgInfo";
            this.Text = "TTSP messages";
            this.Load += new System.EventHandler(this.frmTTSPMsgInfo_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTTSPMsgInfo_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmTTSPMsgInfo_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTTSPMsgInfo_KeyDown);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.Controls.SetChildIndex(this.cmdiqs, 0);
            this.Controls.SetChildIndex(this.tabInfomation, 0);
            this.grbtransaction.ResumeLayout(false);
            this.grbtransaction.PerformLayout();
            this.tabInfomation.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grbcontent.ResumeLayout(false);
            this.grbcontent.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbtransaction;
        private System.Windows.Forms.TextBox txtdepartment;
        private System.Windows.Forms.TextBox txtamount;
        private System.Windows.Forms.Label lbDerpartment;
        private System.Windows.Forms.TextBox txttran_date;
        private System.Windows.Forms.Label lbamount;
        private System.Windows.Forms.TextBox txtreceiving_bank;
        private System.Windows.Forms.Label lbtran_date;
        private System.Windows.Forms.Label lbRece_bankname;
        private System.Windows.Forms.Label lbreceiving_bank;
        private System.Windows.Forms.TextBox txtrm_no;
        private System.Windows.Forms.Label lbSend_bankname;
        private System.Windows.Forms.Label lbRm_no;
        private System.Windows.Forms.TextBox txtsending_bank;
        private System.Windows.Forms.Label lbsending_bank;
        private System.Windows.Forms.TabControl tabInfomation;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grbcontent;
        private System.Windows.Forms.TextBox txtcontent;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txthistory;
        private System.Windows.Forms.Button cmdiqs;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Label lbccycd;
        private System.Windows.Forms.TextBox txtRmno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}