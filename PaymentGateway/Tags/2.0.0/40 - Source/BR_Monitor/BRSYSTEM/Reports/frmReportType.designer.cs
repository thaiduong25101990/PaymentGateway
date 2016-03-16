namespace BR.BRSYSTEM
{
    partial class frmReportType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportType));
            this.cboCalcel = new System.Windows.Forms.Button();
            this.cboYes = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.dgvReportType = new System.Windows.Forms.DataGridView();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportType)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(602, 420);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(290, 433);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(204, 433);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(29, 433);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(115, 433);
            // 
            // cboCalcel
            // 
            this.cboCalcel.Location = new System.Drawing.Point(0, 0);
            this.cboCalcel.Name = "cboCalcel";
            this.cboCalcel.Size = new System.Drawing.Size(75, 23);
            this.cboCalcel.TabIndex = 0;
            this.cboCalcel.Click += new System.EventHandler(this.cboCalcel_Click);
            // 
            // cboYes
            // 
            this.cboYes.Location = new System.Drawing.Point(0, 0);
            this.cboYes.Name = "cboYes";
            this.cboYes.Size = new System.Drawing.Size(75, 23);
            this.cboYes.TabIndex = 0;
            this.cboYes.Click += new System.EventHandler(this.cboYes_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefresh.Location = new System.Drawing.Point(514, 420);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(80, 30);
            this.cmdRefresh.TabIndex = 0;
            this.cmdRefresh.Text = "&Report";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // dgvReportType
            // 
            this.dgvReportType.AllowUserToAddRows = false;
            this.dgvReportType.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReportType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportType.GridColor = System.Drawing.SystemColors.HighlightText;
            this.dgvReportType.Location = new System.Drawing.Point(6, 6);
            this.dgvReportType.MultiSelect = false;
            this.dgvReportType.Name = "dgvReportType";
            this.dgvReportType.RowHeadersVisible = false;
            this.dgvReportType.Size = new System.Drawing.Size(682, 408);
            this.dgvReportType.TabIndex = 100;
            this.dgvReportType.TabStop = false;
            this.dgvReportType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReportType_CellClick);
            // 
            // cmdExport
            // 
            this.cmdExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExport.Location = new System.Drawing.Point(272, 420);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(85, 30);
            this.cmdExport.TabIndex = 1;
            this.cmdExport.Text = "&Export File";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Visible = false;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(115, 420);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(75, 30);
            this.cmdPrint.TabIndex = 2;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            // 
            // frmReportType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 456);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.dgvReportType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportType";
            this.Text = "Report type";
            this.Load += new System.EventHandler(this.frmReportType_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmReportType_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportType_KeyDown);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.dgvReportType, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdRefresh, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.DataGridView dgvReportType;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.Button cmdPrint;
    }
}