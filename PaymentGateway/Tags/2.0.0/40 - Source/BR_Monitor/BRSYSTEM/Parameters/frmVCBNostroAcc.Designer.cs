namespace BR.BRSYSTEM
{
    partial class frmVCBNostroAcc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mskAcctNo = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.lblBrName = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFilename = new System.Windows.Forms.Label();
            this.dgAcct = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCYCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmdReject = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAcct)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(450, 421);
            this.cmdClose.TabIndex = 11;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(284, 421);
            this.cmdSave.TabIndex = 9;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(201, 421);
            this.cmdDelete.TabIndex = 8;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(36, 421);
            this.cmdAdd.TabIndex = 6;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(118, 421);
            this.cmdEdit.TabIndex = 7;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mskAcctNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbCurrency);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // mskAcctNo
            // 
            this.mskAcctNo.Location = new System.Drawing.Point(149, 47);
            this.mskAcctNo.Name = "mskAcctNo";
            this.mskAcctNo.Size = new System.Drawing.Size(192, 23);
            this.mskAcctNo.TabIndex = 3;
            this.mskAcctNo.Validated += new System.EventHandler(this.mskAcctNo_Validated);
            this.mskAcctNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mskAcctNo_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Account number:";
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Items.AddRange(new object[] {
            "ALL"});
            this.cbCurrency.Location = new System.Drawing.Point(149, 17);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(192, 24);
            this.cbCurrency.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Currency code:";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(431, 7);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 4;
            this.cmdSearch.Text = "Sea&rch";
            this.cmdSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblBrName
            // 
            this.lblBrName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblBrName.Location = new System.Drawing.Point(193, 354);
            this.lblBrName.Name = "lblBrName";
            this.lblBrName.Size = new System.Drawing.Size(194, 31);
            this.lblBrName.TabIndex = 13;
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Items.AddRange(new object[] {
            "ALL"});
            this.cbBranch.Location = new System.Drawing.Point(124, 171);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(80, 24);
            this.cbBranch.TabIndex = 1;
            this.cbBranch.SelectedIndexChanged += new System.EventHandler(this.cbBranch_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(230, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Branch name:";
            // 
            // lblFilename
            // 
            this.lblFilename.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.Location = new System.Drawing.Point(26, 174);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(94, 19);
            this.lblFilename.TabIndex = 8;
            this.lblFilename.Text = "Branch:";
            // 
            // dgAcct
            // 
            this.dgAcct.AllowUserToAddRows = false;
            this.dgAcct.AllowUserToDeleteRows = false;
            this.dgAcct.AllowUserToOrderColumns = true;
            this.dgAcct.AllowUserToResizeRows = false;
            this.dgAcct.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAcct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgAcct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAcct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Branch,
            this.CCYCD,
            this.Account});
            this.dgAcct.Location = new System.Drawing.Point(7, 89);
            this.dgAcct.MultiSelect = false;
            this.dgAcct.Name = "dgAcct";
            this.dgAcct.ReadOnly = true;
            this.dgAcct.Size = new System.Drawing.Size(522, 323);
            this.dgAcct.TabIndex = 7;
            this.dgAcct.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAcct_CellClick);
            this.dgAcct.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAcct_CellEnter);
            this.dgAcct.SelectionChanged += new System.EventHandler(this.dgAcct_SelectionChanged);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Branch
            // 
            this.Branch.DataPropertyName = "Branch";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Branch.DefaultCellStyle = dataGridViewCellStyle2;
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            this.Branch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Branch.Width = 150;
            // 
            // CCYCD
            // 
            this.CCYCD.DataPropertyName = "CCYCD";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CCYCD.DefaultCellStyle = dataGridViewCellStyle3;
            this.CCYCD.HeaderText = "Currency code";
            this.CCYCD.Name = "CCYCD";
            this.CCYCD.ReadOnly = true;
            this.CCYCD.Width = 150;
            // 
            // Account
            // 
            this.Account.DataPropertyName = "Account";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Account.DefaultCellStyle = dataGridViewCellStyle4;
            this.Account.HeaderText = "Account number";
            this.Account.Name = "Account";
            this.Account.ReadOnly = true;
            this.Account.Width = 313;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(28, 552);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(49, 20);
            this.txtID.TabIndex = 15;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // cmdReject
            // 
            this.cmdReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReject.Location = new System.Drawing.Point(367, 421);
            this.cmdReject.Name = "cmdReject";
            this.cmdReject.Size = new System.Drawing.Size(80, 30);
            this.cmdReject.TabIndex = 10;
            this.cmdReject.Text = "Ca&ncel";
            this.cmdReject.UseVisualStyleBackColor = true;
            this.cmdReject.Click += new System.EventHandler(this.cmdReject_Click);
            // 
            // frmVCBNostroAcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 465);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.dgAcct);
            this.Controls.Add(this.cbBranch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblBrName);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.cmdReject);
            this.Name = "frmVCBNostroAcc";
            this.Text = "H.O’s Nostro Account at VCB";
            this.Load += new System.EventHandler(this.frmVCBNostroAcc_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVCBNostroAcc_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmVCBNostroAcc_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.cmdReject, 0);
            this.Controls.SetChildIndex(this.lblFilename, 0);
            this.Controls.SetChildIndex(this.lblBrName, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cbBranch, 0);
            this.Controls.SetChildIndex(this.dgAcct, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAcct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label lblBrName;
        private System.Windows.Forms.DataGridView dgAcct;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button cmdReject;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCYCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.MaskedTextBox mskAcctNo;
    }
}