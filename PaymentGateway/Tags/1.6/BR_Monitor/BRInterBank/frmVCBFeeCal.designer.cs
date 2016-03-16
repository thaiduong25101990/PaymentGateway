namespace BR.BRInterBank
{
    partial class frmVCBFeeCal
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbCCYCD = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.cbFeeType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateto = new System.Windows.Forms.DateTimePicker();
            this.datefrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtFeeFixed = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmdPhi = new System.Windows.Forms.Button();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCYCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIXEDFEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MINFEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAXFEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPrintDetail = new System.Windows.Forms.Button();
            this.cmdPrintExcel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(557, 451);
            this.cmdClose.TabIndex = 14;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(212, 451);
            this.cmdSave.TabIndex = 12;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(334, 514);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(40, 451);
            this.cmdAdd.TabIndex = 10;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(126, 451);
            this.cmdEdit.TabIndex = 11;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbCCYCD);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbBranch);
            this.groupBox1.Controls.Add(this.cbFeeType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateto);
            this.groupBox1.Controls.Add(this.datefrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(364, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "CCYCD :";
            // 
            // cbCCYCD
            // 
            this.cbCCYCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCCYCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCCYCD.FormattingEnabled = true;
            this.cbCCYCD.Location = new System.Drawing.Point(451, 49);
            this.cbCCYCD.Name = "cbCCYCD";
            this.cbCCYCD.Size = new System.Drawing.Size(187, 24);
            this.cbCCYCD.TabIndex = 5;
            this.cbCCYCD.SelectedIndexChanged += new System.EventHandler(this.cbCCYCD_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Branch :";
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(125, 78);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(187, 24);
            this.cbBranch.TabIndex = 3;
            // 
            // cbFeeType
            // 
            this.cbFeeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFeeType.FormattingEnabled = true;
            this.cbFeeType.Location = new System.Drawing.Point(451, 19);
            this.cbFeeType.Name = "cbFeeType";
            this.cbFeeType.Size = new System.Drawing.Size(187, 24);
            this.cbFeeType.TabIndex = 4;
            this.cbFeeType.SelectedIndexChanged += new System.EventHandler(this.cbFeeType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(364, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Fee Type :";
            // 
            // dateto
            // 
            this.dateto.CustomFormat = "dd/MM/yyyy";
            this.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateto.Location = new System.Drawing.Point(125, 49);
            this.dateto.Name = "dateto";
            this.dateto.Size = new System.Drawing.Size(187, 22);
            this.dateto.TabIndex = 2;
            this.dateto.ValueChanged += new System.EventHandler(this.dateto_ValueChanged);
            // 
            // datefrom
            // 
            this.datefrom.CustomFormat = "dd/MM/yyyy";
            this.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrom.Location = new System.Drawing.Point(125, 19);
            this.datefrom.Name = "datefrom";
            this.datefrom.Size = new System.Drawing.Size(187, 22);
            this.datefrom.TabIndex = 1;
            this.datefrom.ValueChanged += new System.EventHandler(this.datefrom_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "To date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "From date:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 16);
            this.label9.TabIndex = 3;
            this.label9.Text = "Fixed fee:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(16, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Rate:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(364, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 16);
            this.label11.TabIndex = 3;
            this.label11.Text = "Min:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(364, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 3;
            this.label12.Text = "Max:";
            // 
            // txtMax
            // 
            this.txtMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(451, 47);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(187, 22);
            this.txtMax.TabIndex = 9;
            this.txtMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMax_KeyPress);
            // 
            // txtMin
            // 
            this.txtMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMin.Location = new System.Drawing.Point(451, 18);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(187, 22);
            this.txtMin.TabIndex = 8;
            this.txtMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMin_KeyPress);
            // 
            // txtRate
            // 
            this.txtRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Location = new System.Drawing.Point(125, 47);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(187, 22);
            this.txtRate.TabIndex = 7;
            this.txtRate.TextChanged += new System.EventHandler(this.txtRate_TextChanged);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            // 
            // txtFeeFixed
            // 
            this.txtFeeFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeeFixed.Location = new System.Drawing.Point(125, 18);
            this.txtFeeFixed.Name = "txtFeeFixed";
            this.txtFeeFixed.Size = new System.Drawing.Size(187, 22);
            this.txtFeeFixed.TabIndex = 6;
            this.txtFeeFixed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeeFixed_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtID);
            this.groupBox3.Controls.Add(this.txtFeeFixed);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtMin);
            this.groupBox3.Controls.Add(this.txtMax);
            this.groupBox3.Controls.Add(this.txtRate);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(11, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(652, 81);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(85, 40);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(22, 22);
            this.txtID.TabIndex = 13;
            this.txtID.Visible = false;
            // 
            // cmdPhi
            // 
            this.cmdPhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPhi.Location = new System.Drawing.Point(298, 451);
            this.cmdPhi.Name = "cmdPhi";
            this.cmdPhi.Size = new System.Drawing.Size(80, 30);
            this.cmdPhi.TabIndex = 13;
            this.cmdPhi.Text = "&Calculator";
            this.cmdPhi.UseVisualStyleBackColor = true;
            this.cmdPhi.Click += new System.EventHandler(this.cmdPhi_Click);
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
            this.CCYCD,
            this.FIXEDFEE,
            this.RATE,
            this.MINFEE,
            this.MAXFEE});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgView.Location = new System.Drawing.Point(11, 219);
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
            this.dgView.Size = new System.Drawing.Size(652, 220);
            this.dgView.TabIndex = 30;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // CCYCD
            // 
            this.CCYCD.HeaderText = "CCYCD";
            this.CCYCD.Name = "CCYCD";
            // 
            // FIXEDFEE
            // 
            this.FIXEDFEE.HeaderText = "FIXED FEE";
            this.FIXEDFEE.Name = "FIXEDFEE";
            // 
            // RATE
            // 
            this.RATE.HeaderText = "RATE";
            this.RATE.Name = "RATE";
            // 
            // MINFEE
            // 
            this.MINFEE.HeaderText = "MIN FEE";
            this.MINFEE.Name = "MINFEE";
            // 
            // MAXFEE
            // 
            this.MAXFEE.HeaderText = "MAX FEE";
            this.MAXFEE.Name = "MAXFEE";
            // 
            // cmdPrintDetail
            // 
            this.cmdPrintDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintDetail.Location = new System.Drawing.Point(385, 451);
            this.cmdPrintDetail.Name = "cmdPrintDetail";
            this.cmdPrintDetail.Size = new System.Drawing.Size(80, 30);
            this.cmdPrintDetail.TabIndex = 31;
            this.cmdPrintDetail.Text = "Cal Detail";
            this.cmdPrintDetail.UseVisualStyleBackColor = true;
            this.cmdPrintDetail.Click += new System.EventHandler(this.cmdPrintDetail_Click);
            // 
            // cmdPrintExcel
            // 
            this.cmdPrintExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintExcel.Location = new System.Drawing.Point(471, 451);
            this.cmdPrintExcel.Name = "cmdPrintExcel";
            this.cmdPrintExcel.Size = new System.Drawing.Size(80, 30);
            this.cmdPrintExcel.TabIndex = 32;
            this.cmdPrintExcel.Text = "Cal Excel";
            this.cmdPrintExcel.UseVisualStyleBackColor = true;
            this.cmdPrintExcel.Click += new System.EventHandler(this.cmdPrintExcel_Click);
            // 
            // frmVCBFeeCal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(677, 495);
            this.Controls.Add(this.cmdPrintExcel);
            this.Controls.Add(this.cmdPrintDetail);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.cmdPhi);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmVCBFeeCal";
            this.Text = "Free calculator";
            this.Load += new System.EventHandler(this.frmVCBFeeCal_Load);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.cmdPhi, 0);
            this.Controls.SetChildIndex(this.dgView, 0);
            this.Controls.SetChildIndex(this.cmdPrintDetail, 0);
            this.Controls.SetChildIndex(this.cmdPrintExcel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateto;
        private System.Windows.Forms.DateTimePicker datefrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.TextBox txtFeeFixed;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdPhi;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ComboBox cbFeeType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCCYCD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCYCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIXEDFEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MINFEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAXFEE;
        private System.Windows.Forms.Button cmdPrintDetail;
        private System.Windows.Forms.Button cmdPrintExcel;
    }
}