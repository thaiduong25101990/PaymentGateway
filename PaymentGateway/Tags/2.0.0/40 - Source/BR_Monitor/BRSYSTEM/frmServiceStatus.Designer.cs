namespace BR.BRSYSTEM
{
    partial class frmServiceStatus
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.lbIpserver = new System.Windows.Forms.Label();
            this.cbSevice = new System.Windows.Forms.ComboBox();
            this.lbService_name = new System.Windows.Forms.Label();
            this.dtrService = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdRestart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtrService)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(384, 438);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(302, 482);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(216, 482);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(41, 482);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(127, 482);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.lbIpserver);
            this.groupBox1.Controls.Add(this.cbSevice);
            this.groupBox1.Controls.Add(this.lbService_name);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(116, 18);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(231, 23);
            this.txtIP.TabIndex = 0;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(378, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // lbIpserver
            // 
            this.lbIpserver.AutoSize = true;
            this.lbIpserver.Location = new System.Drawing.Point(17, 23);
            this.lbIpserver.Name = "lbIpserver";
            this.lbIpserver.Size = new System.Drawing.Size(66, 16);
            this.lbIpserver.TabIndex = 0;
            this.lbIpserver.Text = "IP Server:";
            // 
            // cbSevice
            // 
            this.cbSevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSevice.FormattingEnabled = true;
            this.cbSevice.Location = new System.Drawing.Point(116, 50);
            this.cbSevice.Name = "cbSevice";
            this.cbSevice.Size = new System.Drawing.Size(231, 24);
            this.cbSevice.TabIndex = 1;
            this.cbSevice.SelectedIndexChanged += new System.EventHandler(this.cbSevice_SelectedIndexChanged);
            // 
            // lbService_name
            // 
            this.lbService_name.AutoSize = true;
            this.lbService_name.Location = new System.Drawing.Point(17, 52);
            this.lbService_name.Name = "lbService_name";
            this.lbService_name.Size = new System.Drawing.Size(95, 16);
            this.lbService_name.TabIndex = 0;
            this.lbService_name.Text = "Service name :";
            // 
            // dtrService
            // 
            this.dtrService.AllowUserToAddRows = false;
            this.dtrService.AllowUserToDeleteRows = false;
            this.dtrService.AllowUserToResizeRows = false;
            this.dtrService.BackgroundColor = System.Drawing.Color.White;
            this.dtrService.ColumnHeadersHeight = 21;
            this.dtrService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtrService.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dtrService.Location = new System.Drawing.Point(6, 89);
            this.dtrService.Name = "dtrService";
            this.dtrService.RowHeadersWidth = 30;
            this.dtrService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtrService.Size = new System.Drawing.Size(466, 343);
            this.dtrService.TabIndex = 3;
            this.dtrService.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtrService_MouseMove);
            this.dtrService.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtrService_CellClick);
            this.dtrService.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtrService_CellEnter);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "                    Service name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 320;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "  Status";
            this.Column2.Name = "Column2";
            // 
            // cmdStop
            // 
            this.cmdStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStop.Location = new System.Drawing.Point(302, 438);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(80, 30);
            this.cmdStop.TabIndex = 5;
            this.cmdStop.Text = "S&top";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdStart
            // 
            this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.Location = new System.Drawing.Point(219, 438);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(80, 30);
            this.cmdStart.TabIndex = 4;
            this.cmdStart.Text = "St&art";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdRestart
            // 
            this.cmdRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestart.Location = new System.Drawing.Point(135, 438);
            this.cmdRestart.Name = "cmdRestart";
            this.cmdRestart.Size = new System.Drawing.Size(80, 30);
            this.cmdRestart.TabIndex = 3;
            this.cmdRestart.Text = "&Restart";
            this.cmdRestart.UseVisualStyleBackColor = true;
            this.cmdRestart.Click += new System.EventHandler(this.cmdRestart_Click);
            // 
            // frmServiceStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 474);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtrService);
            this.Controls.Add(this.cmdRestart);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.cmdStop);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmServiceStatus";
            this.Text = "Service status";
            this.Load += new System.EventHandler(this.frmServiceStatus_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmServiceStatus_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServiceStatus_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmServiceStatus_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmServiceStatus_KeyDown);
            this.Controls.SetChildIndex(this.cmdStop, 0);
            this.Controls.SetChildIndex(this.cmdStart, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdRestart, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.dtrService, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtrService)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbService_name;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.ComboBox cbSevice;
        private System.Windows.Forms.DataGridView dtrService;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdRestart;
        private System.Windows.Forms.Label lbIpserver;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox txtIP;
    }
}