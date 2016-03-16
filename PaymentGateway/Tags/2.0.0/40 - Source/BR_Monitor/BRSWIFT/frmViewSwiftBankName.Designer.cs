namespace BR.BRSWIFT
{
    partial class frmViewSwiftBankName
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
            this.dgView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIBSbankcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SWIFTbankcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bankname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.SIBSbankcode,
            this.SWIFTbankcode,
            this.Bankname});
            this.dgView.Location = new System.Drawing.Point(6, 9);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.RowHeadersWidth = 30;
            this.dgView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(560, 283);
            this.dgView.TabIndex = 0;
            this.dgView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellDoubleClick);
            this.dgView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgView_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // SIBSbankcode
            // 
            this.SIBSbankcode.HeaderText = "SIBS bank code";
            this.SIBSbankcode.Name = "SIBSbankcode";
            this.SIBSbankcode.ReadOnly = true;
            // 
            // SWIFTbankcode
            // 
            this.SWIFTbankcode.HeaderText = "SWIFT bank code";
            this.SWIFTbankcode.Name = "SWIFTbankcode";
            this.SWIFTbankcode.ReadOnly = true;
            // 
            // Bankname
            // 
            this.Bankname.HeaderText = "Bank name";
            this.Bankname.Name = "Bankname";
            this.Bankname.ReadOnly = true;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelect.Location = new System.Drawing.Point(403, 294);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(80, 30);
            this.cmdSelect.TabIndex = 1;
            this.cmdSelect.Text = "&Select";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(486, 294);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "&Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmViewSwiftBankName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 328);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.dgView);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmViewSwiftBankName";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View bank name";
            this.Load += new System.EventHandler(this.frmViewSwiftBankName_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmViewSwiftBankName_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewSwiftBankName_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Button cmdSelect;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIBSbankcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SWIFTbankcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bankname;
    }
}