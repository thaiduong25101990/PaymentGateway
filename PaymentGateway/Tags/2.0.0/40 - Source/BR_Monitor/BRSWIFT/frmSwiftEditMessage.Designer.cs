namespace BR.BRSWIFT
{
    partial class frmSwiftEditMessage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgOrgContent = new System.Windows.Forms.DataGridView();
            this.OrgContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgEditContent = new System.Windows.Forms.DataGridView();
            this.EditContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmsSave = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrgContent)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEditContent)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgOrgContent);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 485);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origin Message";
            // 
            // dtgOrgContent
            // 
            this.dtgOrgContent.AllowUserToAddRows = false;
            this.dtgOrgContent.AllowUserToResizeRows = false;
            this.dtgOrgContent.BackgroundColor = System.Drawing.Color.White;
            this.dtgOrgContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgOrgContent.ColumnHeadersVisible = false;
            this.dtgOrgContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrgContent});
            this.dtgOrgContent.GridColor = System.Drawing.Color.White;
            this.dtgOrgContent.Location = new System.Drawing.Point(6, 21);
            this.dtgOrgContent.Name = "dtgOrgContent";
            this.dtgOrgContent.RowHeadersVisible = false;
            this.dtgOrgContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgOrgContent.Size = new System.Drawing.Size(466, 458);
            this.dtgOrgContent.TabIndex = 0;
            // 
            // OrgContent
            // 
            this.OrgContent.HeaderText = "OrgContent";
            this.OrgContent.Name = "OrgContent";
            this.OrgContent.Width = 1000;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgEditContent);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(496, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 485);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit Message";
            // 
            // dtgEditContent
            // 
            this.dtgEditContent.AllowUserToAddRows = false;
            this.dtgEditContent.AllowUserToResizeRows = false;
            this.dtgEditContent.BackgroundColor = System.Drawing.Color.White;
            this.dtgEditContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgEditContent.ColumnHeadersVisible = false;
            this.dtgEditContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EditContent});
            this.dtgEditContent.GridColor = System.Drawing.Color.White;
            this.dtgEditContent.Location = new System.Drawing.Point(6, 21);
            this.dtgEditContent.Name = "dtgEditContent";
            this.dtgEditContent.RowHeadersVisible = false;
            this.dtgEditContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgEditContent.Size = new System.Drawing.Size(463, 458);
            this.dtgEditContent.TabIndex = 0;
            this.dtgEditContent.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEditContent_CellValidated);
            this.dtgEditContent.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEditContent_CellEnter);
            // 
            // EditContent
            // 
            this.EditContent.HeaderText = "EditContent";
            this.EditContent.Name = "EditContent";
            this.EditContent.Width = 1000;
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(883, 500);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmsSave
            // 
            this.cmsSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsSave.Location = new System.Drawing.Point(799, 500);
            this.cmsSave.Name = "cmsSave";
            this.cmsSave.Size = new System.Drawing.Size(80, 30);
            this.cmsSave.TabIndex = 1;
            this.cmsSave.Text = "&Save";
            this.cmsSave.UseVisualStyleBackColor = true;
            this.cmsSave.Click += new System.EventHandler(this.cmsSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Location = new System.Drawing.Point(716, 500);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(80, 30);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "&Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdd.Location = new System.Drawing.Point(633, 500);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(80, 30);
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Text = "&Add Row";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.OnUpdate);
            // 
            // frmSwiftEditMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 540);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmsSave);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmSwiftEditMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Swift Edit Message";
            this.Load += new System.EventHandler(this.frmSwiftEditMessage_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSwiftEditMessage_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrgContent)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgEditContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmsSave;
        private System.Windows.Forms.DataGridView dtgOrgContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrgContent;
        private System.Windows.Forms.DataGridView dtgEditContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn EditContent;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Timer timer1;
    }
}