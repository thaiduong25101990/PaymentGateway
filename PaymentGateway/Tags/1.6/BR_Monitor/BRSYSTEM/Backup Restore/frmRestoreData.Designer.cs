namespace BR.BRSYSTEM
{
    partial class frmRestoreData
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
            this.datTable_Re = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cmdRestore = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.datTable_Re)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(574, 257);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // datTable_Re
            // 
            this.datTable_Re.AllowUserToAddRows = false;
            this.datTable_Re.AllowUserToDeleteRows = false;
            this.datTable_Re.AllowUserToResizeRows = false;
            this.datTable_Re.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datTable_Re.ColumnHeadersHeight = 21;
            this.datTable_Re.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.datTable_Re.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.datTable_Re.Location = new System.Drawing.Point(8, 12);
            this.datTable_Re.Name = "datTable_Re";
            this.datTable_Re.Size = new System.Drawing.Size(646, 218);
            this.datTable_Re.TabIndex = 0;
            this.datTable_Re.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datTable_Re_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = " Restore";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "              Table Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 220;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "                 File Name";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Select";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cmdRestore
            // 
            this.cmdRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestore.Location = new System.Drawing.Point(488, 257);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(80, 30);
            this.cmdRestore.TabIndex = 2;
            this.cmdRestore.Text = "&Restore";
            this.cmdRestore.UseVisualStyleBackColor = true;
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 236);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(646, 15);
            this.progressBar1.TabIndex = 1;
            // 
            // frmRestoreData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 294);
            this.Controls.Add(this.datTable_Re);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cmdRestore);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRestoreData";
            this.Text = "Restore table";
            this.Load += new System.EventHandler(this.frmRestoreData_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmRestoreData_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRestoreData_KeyDown);
            this.Controls.SetChildIndex(this.cmdRestore, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.datTable_Re, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.datTable_Re)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datTable_Re;
        private System.Windows.Forms.Button cmdRestore;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
    }
}