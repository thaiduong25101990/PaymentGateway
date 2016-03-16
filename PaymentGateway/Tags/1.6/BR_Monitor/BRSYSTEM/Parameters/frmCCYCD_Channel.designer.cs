namespace BR.BRSYSTEM
{
    partial class frmCCYCD_Channel
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
            this.cboPartner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCCYCD = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboGWType = new System.Windows.Forms.ComboBox();
            this.lblShortCD = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgdCurrency = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCurrency)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(650, 458);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(302, 202);
            this.cmdSave.Visible = false;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(564, 458);
            this.cmdDelete.TabIndex = 6;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(392, 458);
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(478, 458);
            this.cmdEdit.TabIndex = 5;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboPartner);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboCCYCD);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboGWType);
            this.groupBox1.Controls.Add(this.lblShortCD);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // cboPartner
            // 
            this.cboPartner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPartner.FormattingEnabled = true;
            this.cboPartner.Location = new System.Drawing.Point(446, 56);
            this.cboPartner.Name = "cboPartner";
            this.cboPartner.Size = new System.Drawing.Size(168, 24);
            this.cboPartner.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Partner :";
            // 
            // cboCCYCD
            // 
            this.cboCCYCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCCYCD.FormattingEnabled = true;
            this.cboCCYCD.Items.AddRange(new object[] {
            "ALL"});
            this.cboCCYCD.Location = new System.Drawing.Point(133, 25);
            this.cboCCYCD.Name = "cboCCYCD";
            this.cboCCYCD.Size = new System.Drawing.Size(181, 24);
            this.cboCCYCD.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 50;
            this.label4.Text = "Currency code:";
            // 
            // cboGWType
            // 
            this.cboGWType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGWType.FormattingEnabled = true;
            this.cboGWType.Location = new System.Drawing.Point(133, 56);
            this.cboGWType.Name = "cboGWType";
            this.cboGWType.Size = new System.Drawing.Size(181, 24);
            this.cboGWType.TabIndex = 1;
            // 
            // lblShortCD
            // 
            this.lblShortCD.AutoSize = true;
            this.lblShortCD.Location = new System.Drawing.Point(333, 32);
            this.lblShortCD.Name = "lblShortCD";
            this.lblShortCD.Size = new System.Drawing.Size(0, 16);
            this.lblShortCD.TabIndex = 4;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(638, 21);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 4;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(446, 25);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(168, 24);
            this.cboStatus.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "Channel :";
            // 
            // dgdCurrency
            // 
            this.dgdCurrency.AllowUserToAddRows = false;
            this.dgdCurrency.AllowUserToDeleteRows = false;
            this.dgdCurrency.AllowUserToResizeRows = false;
            this.dgdCurrency.BackgroundColor = System.Drawing.Color.White;
            this.dgdCurrency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdCurrency.Location = new System.Drawing.Point(12, 102);
            this.dgdCurrency.MultiSelect = false;
            this.dgdCurrency.Name = "dgdCurrency";
            this.dgdCurrency.ReadOnly = true;
            this.dgdCurrency.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgdCurrency.Size = new System.Drawing.Size(736, 348);
            this.dgdCurrency.TabIndex = 20;
            this.dgdCurrency.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdCurrency_CellDoubleClick);
            this.dgdCurrency.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgdCurrency_MouseMove);
            this.dgdCurrency.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdCurrency_CellClick);
            this.dgdCurrency.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdCurrency_CellEnter);
            // 
            // frmCCYCD_Channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(756, 494);
            this.Controls.Add(this.dgdCurrency);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCCYCD_Channel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IBPS Currency management";
            this.Load += new System.EventHandler(this.frmCCYCD_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCCYCD_Channel_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCCYCD_Channel_KeyDown);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.dgdCurrency, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCurrency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGWType;
        private System.Windows.Forms.Label lblShortCD;
        private System.Windows.Forms.DataGridView dgdCurrency;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboCCYCD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPartner;
    }
}