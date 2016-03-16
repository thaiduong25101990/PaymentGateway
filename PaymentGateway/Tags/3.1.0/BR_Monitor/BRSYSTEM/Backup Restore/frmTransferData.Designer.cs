namespace BR.BRSYSTEM
{
    partial class frmTransferData
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
            this.grbsearch = new System.Windows.Forms.GroupBox();
            this.Lbsearch = new System.Windows.Forms.Label();
            this.cbBktime = new System.Windows.Forms.ComboBox();
            this.datTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdsearch = new System.Windows.Forms.Button();
            this.cmdbackup = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.grbsearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datTable)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(475, 276);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click_1);
            // 
            // grbsearch
            // 
            this.grbsearch.Controls.Add(this.Lbsearch);
            this.grbsearch.Controls.Add(this.cbBktime);
            this.grbsearch.Controls.Add(this.datTable);
            this.grbsearch.Controls.Add(this.cmdsearch);
            this.grbsearch.Location = new System.Drawing.Point(6, 3);
            this.grbsearch.Name = "grbsearch";
            this.grbsearch.Size = new System.Drawing.Size(555, 244);
            this.grbsearch.TabIndex = 0;
            this.grbsearch.TabStop = false;
            // 
            // Lbsearch
            // 
            this.Lbsearch.AutoSize = true;
            this.Lbsearch.Location = new System.Drawing.Point(7, 24);
            this.Lbsearch.Name = "Lbsearch";
            this.Lbsearch.Size = new System.Drawing.Size(81, 16);
            this.Lbsearch.TabIndex = 3;
            this.Lbsearch.Text = "Backup type:";
            // 
            // cbBktime
            // 
            this.cbBktime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBktime.FormattingEnabled = true;
            this.cbBktime.Location = new System.Drawing.Point(97, 20);
            this.cbBktime.Name = "cbBktime";
            this.cbBktime.Size = new System.Drawing.Size(162, 24);
            this.cbBktime.TabIndex = 1;
            this.cbBktime.SelectedIndexChanged += new System.EventHandler(this.cbBktime_SelectedIndexChanged);
            // 
            // datTable
            // 
            this.datTable.AllowUserToAddRows = false;
            this.datTable.AllowUserToDeleteRows = false;
            this.datTable.AllowUserToResizeRows = false;
            this.datTable.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datTable.ColumnHeadersHeight = 21;
            this.datTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.datTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.datTable.Location = new System.Drawing.Point(4, 56);
            this.datTable.Name = "datTable";
            this.datTable.RowHeadersWidth = 30;
            this.datTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.datTable.Size = new System.Drawing.Size(543, 182);
            this.datTable.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "    Back up";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "               Source table";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "              Destination table";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // cmdsearch
            // 
            this.cmdsearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdsearch.Location = new System.Drawing.Point(467, 20);
            this.cmdsearch.Name = "cmdsearch";
            this.cmdsearch.Size = new System.Drawing.Size(80, 30);
            this.cmdsearch.TabIndex = 2;
            this.cmdsearch.Text = "&Search";
            this.cmdsearch.UseVisualStyleBackColor = true;
            this.cmdsearch.Visible = false;
            this.cmdsearch.Click += new System.EventHandler(this.cmdsearch_Click);
            // 
            // cmdbackup
            // 
            this.cmdbackup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdbackup.Location = new System.Drawing.Point(392, 276);
            this.cmdbackup.Name = "cmdbackup";
            this.cmdbackup.Size = new System.Drawing.Size(80, 30);
            this.cmdbackup.TabIndex = 5;
            this.cmdbackup.Text = "&Back up";
            this.cmdbackup.UseVisualStyleBackColor = true;
            this.cmdbackup.Click += new System.EventHandler(this.cmdbackup_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 253);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(549, 17);
            this.progressBar1.TabIndex = 4;
            // 
            // frmTransferData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 313);
            this.Controls.Add(this.grbsearch);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cmdbackup);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTransferData";
            this.Text = "Transfer data";
            this.Load += new System.EventHandler(this.frmTransfer_data_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmTransferData_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTransferData_KeyDown);
            this.Controls.SetChildIndex(this.cmdbackup, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.grbsearch, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.grbsearch.ResumeLayout(false);
            this.grbsearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbsearch;
        private System.Windows.Forms.Label Lbsearch;
        private System.Windows.Forms.ComboBox cbBktime;
        private System.Windows.Forms.DataGridView datTable;
        private System.Windows.Forms.Button cmdsearch;
        private System.Windows.Forms.Button cmdbackup;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}