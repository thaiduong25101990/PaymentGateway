namespace BR.BRSWIFT
{
    partial class frmSWIWConditionBr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSWIWConditionBr));
            this.dgdSCBranch = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlbNew = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbEdit = new System.Windows.Forms.ToolStripButton();
            this.tlbClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgdSCBranch)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgdSCBranch
            // 
            this.dgdSCBranch.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgdSCBranch.AllowUserToAddRows = false;
            this.dgdSCBranch.AllowUserToDeleteRows = false;
            this.dgdSCBranch.AllowUserToResizeColumns = false;
            this.dgdSCBranch.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgdSCBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdSCBranch.Location = new System.Drawing.Point(5, 35);
            this.dgdSCBranch.Margin = new System.Windows.Forms.Padding(4);
            this.dgdSCBranch.Name = "dgdSCBranch";
            this.dgdSCBranch.ReadOnly = true;
            this.dgdSCBranch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgdSCBranch.Size = new System.Drawing.Size(846, 319);
            this.dgdSCBranch.TabIndex = 3;
            this.dgdSCBranch.DoubleClick += new System.EventHandler(this.dgdSCBranch_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbNew,
            this.tlbDelete,
            this.tlbRefresh,
            this.tlbEdit,
            this.tlbClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(856, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlbNew
            // 
            this.tlbNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbNew.Image")));
            this.tlbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbNew.Name = "tlbNew";
            this.tlbNew.Size = new System.Drawing.Size(48, 22);
            this.tlbNew.Text = "&New";
            this.tlbNew.Click += new System.EventHandler(this.tlbNew_Click);
            // 
            // tlbDelete
            // 
            this.tlbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tlbDelete.Image")));
            this.tlbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbDelete.Name = "tlbDelete";
            this.tlbDelete.Size = new System.Drawing.Size(58, 22);
            this.tlbDelete.Text = "&Delete";
            this.tlbDelete.Click += new System.EventHandler(this.tlbDelete_Click);
            // 
            // tlbRefresh
            // 
            this.tlbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tlbRefresh.Image")));
            this.tlbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbRefresh.Name = "tlbRefresh";
            this.tlbRefresh.Size = new System.Drawing.Size(65, 22);
            this.tlbRefresh.Text = "&Refresh";
            this.tlbRefresh.Click += new System.EventHandler(this.tlbRefresh_Click);
            // 
            // tlbEdit
            // 
            this.tlbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tlbEdit.Image")));
            this.tlbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbEdit.Name = "tlbEdit";
            this.tlbEdit.Size = new System.Drawing.Size(45, 22);
            this.tlbEdit.Text = "&Edit";
            this.tlbEdit.Click += new System.EventHandler(this.tlbEdit_Click);
            // 
            // tlbClose
            // 
            this.tlbClose.Image = ((System.Drawing.Image)(resources.GetObject("tlbClose.Image")));
            this.tlbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClose.Name = "tlbClose";
            this.tlbClose.Size = new System.Drawing.Size(53, 22);
            this.tlbClose.Text = "&Close";
            this.tlbClose.Click += new System.EventHandler(this.tlbClose_Click_1);
            // 
            // frmSWIWConditionBr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 361);
            this.Controls.Add(this.dgdSCBranch);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmSWIWConditionBr";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Swift - Auto module and branch criteria management";
            this.Load += new System.EventHandler(this.frmSWIWConditionBr_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSWIWConditionBr_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSWIWConditionBr_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSWIWConditionBr_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgdSCBranch)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgdSCBranch;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlbNew;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tlbEdit;
        private System.Windows.Forms.ToolStripButton tlbClose;
    }
}