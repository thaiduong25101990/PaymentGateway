namespace BR.BRSWIFT
{
    partial class frmSWIWConditionMdl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSWIWConditionMdl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlbNew = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbEdit = new System.Windows.Forms.ToolStripButton();
            this.tlbClose = new System.Windows.Forms.ToolStripButton();
            this.dgdSCModule = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdSCModule)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(516, 425);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(401, 425);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(287, 425);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(53, 425);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(168, 425);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.Size = new System.Drawing.Size(107, 37);
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
            this.toolStrip1.Size = new System.Drawing.Size(841, 25);
            this.toolStrip1.TabIndex = 0;
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
            // dgdSCModule
            // 
            this.dgdSCModule.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgdSCModule.AllowUserToAddRows = false;
            this.dgdSCModule.AllowUserToDeleteRows = false;
            this.dgdSCModule.AllowUserToResizeColumns = false;
            this.dgdSCModule.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgdSCModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdSCModule.Location = new System.Drawing.Point(7, 29);
            this.dgdSCModule.Margin = new System.Windows.Forms.Padding(4);
            this.dgdSCModule.Name = "dgdSCModule";
            this.dgdSCModule.ReadOnly = true;
            this.dgdSCModule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgdSCModule.Size = new System.Drawing.Size(828, 337);
            this.dgdSCModule.TabIndex = 1;
            this.dgdSCModule.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgdSCModule_MouseDoubleClick);
            // 
            // frmSWIWConditionMdl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 373);
            this.Controls.Add(this.dgdSCModule);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSWIWConditionMdl";
            this.Text = "Swift - Criteria module management ";
            this.Load += new System.EventHandler(this.frmSWIWConditionMdl_Load_1);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSWIWConditionMdl_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSWIWConditionMdl_KeyDown);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.dgdSCModule, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdSCModule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlbNew;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tlbEdit;
        private System.Windows.Forms.DataGridView dgdSCModule;
        private System.Windows.Forms.ToolStripButton tlbClose;
    }
}