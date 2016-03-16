namespace BR.BRSYSTEM
{
    partial class frmServiceLog
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
            this.cmdsearch = new System.Windows.Forms.Button();
            this.datto = new System.Windows.Forms.DateTimePicker();
            this.datFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbServicename = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datGrService = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datGrService)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(632, 412);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(476, 610);
            this.cmdSave.Text = "Sa&ve";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(390, 610);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(215, 610);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(301, 610);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdsearch);
            this.groupBox1.Controls.Add(this.datto);
            this.groupBox1.Controls.Add(this.datFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbServicename);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 81);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // cmdsearch
            // 
            this.cmdsearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdsearch.Location = new System.Drawing.Point(609, 18);
            this.cmdsearch.Name = "cmdsearch";
            this.cmdsearch.Size = new System.Drawing.Size(80, 30);
            this.cmdsearch.TabIndex = 4;
            this.cmdsearch.Text = "&Search";
            this.cmdsearch.UseVisualStyleBackColor = true;
            this.cmdsearch.Click += new System.EventHandler(this.cmdsearch_Click);
            // 
            // datto
            // 
            this.datto.Checked = false;
            this.datto.CustomFormat = "dd/MM/yyyy";
            this.datto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datto.Location = new System.Drawing.Point(388, 48);
            this.datto.Name = "datto";
            this.datto.ShowCheckBox = true;
            this.datto.Size = new System.Drawing.Size(170, 23);
            this.datto.TabIndex = 3;
            this.datto.ValueChanged += new System.EventHandler(this.datto_ValueChanged);
            // 
            // datFrom
            // 
            this.datFrom.Checked = false;
            this.datFrom.CustomFormat = "dd/MM/yyyy";
            this.datFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFrom.Location = new System.Drawing.Point(118, 48);
            this.datFrom.Name = "datFrom";
            this.datFrom.ShowCheckBox = true;
            this.datFrom.Size = new System.Drawing.Size(170, 23);
            this.datFrom.TabIndex = 2;
            this.datFrom.ValueChanged += new System.EventHandler(this.datFrom_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "To date :";
            // 
            // cbServicename
            // 
            this.cbServicename.FormattingEnabled = true;
            this.cbServicename.Items.AddRange(new object[] {
            "ALL",
            "BRIBPSInquiry",
            "BRSWIFTRMInquiry",
            "BRSWIFTTFInquiry",
            "BRIBPSMaintaince",
            "BRSWIFTRMMaintaince",
            "BRSWIFTTFMaintaince",
            "BRIBPSSend",
            "BRSWIFTRMSend",
            "BRSWIFTTFSend",
            "BRIBPSImport",
            "BRSWIFTImport",
            "BRVCBImport",
            "BRIBPSExport",
            "GWSWIFTExport",
            "GWVCBExport"});
            this.cbServicename.Location = new System.Drawing.Point(118, 18);
            this.cbServicename.Name = "cbServicename";
            this.cbServicename.Size = new System.Drawing.Size(170, 24);
            this.cbServicename.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "From date :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service name :";
            // 
            // datGrService
            // 
            this.datGrService.AllowUserToAddRows = false;
            this.datGrService.AllowUserToDeleteRows = false;
            this.datGrService.AllowUserToResizeRows = false;
            this.datGrService.BackgroundColor = System.Drawing.Color.White;
            this.datGrService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datGrService.Location = new System.Drawing.Point(7, 92);
            this.datGrService.Name = "datGrService";
            this.datGrService.ReadOnly = true;
            this.datGrService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datGrService.Size = new System.Drawing.Size(705, 314);
            this.datGrService.TabIndex = 3;
            this.datGrService.MouseMove += new System.Windows.Forms.MouseEventHandler(this.datGrService_MouseMove);
            // 
            // frmServiceLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 449);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.datGrService);
            this.Name = "frmServiceLog";
            this.Text = "Service information";
            this.Load += new System.EventHandler(this.frmServiceLog_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmServiceLog_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmServiceLog_KeyDown);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.datGrService, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datGrService)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker datto;
        private System.Windows.Forms.DateTimePicker datFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbServicename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdsearch;
        private System.Windows.Forms.DataGridView datGrService;
    }
}