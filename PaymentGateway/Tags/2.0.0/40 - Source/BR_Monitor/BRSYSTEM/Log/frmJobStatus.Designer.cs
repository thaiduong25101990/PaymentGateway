namespace BR.BRSYSTEM
{
    partial class frmJobStatus
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
            this.cbJob = new System.Windows.Forms.ComboBox();
            this.grSearch = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbJob_name = new System.Windows.Forms.Label();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdRestart = new System.Windows.Forms.Button();
            this.cmdStart = new System.Windows.Forms.Button();
            this.datDieukien = new System.Windows.Forms.DataGridView();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(377, 396);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(303, 437);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(217, 437);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(42, 437);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(128, 437);
            // 
            // cbJob
            // 
            this.cbJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbJob.FormattingEnabled = true;
            this.cbJob.Location = new System.Drawing.Point(102, 24);
            this.cbJob.Name = "cbJob";
            this.cbJob.Size = new System.Drawing.Size(242, 21);
            this.cbJob.TabIndex = 0;
            // 
            // grSearch
            // 
            this.grSearch.Controls.Add(this.button1);
            this.grSearch.Controls.Add(this.cbJob);
            this.grSearch.Controls.Add(this.lbJob_name);
            this.grSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grSearch.Location = new System.Drawing.Point(7, 4);
            this.grSearch.Name = "grSearch";
            this.grSearch.Size = new System.Drawing.Size(466, 60);
            this.grSearch.TabIndex = 0;
            this.grSearch.TabStop = false;
            this.grSearch.Text = "Searching";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(370, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "&Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbJob_name
            // 
            this.lbJob_name.AutoSize = true;
            this.lbJob_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJob_name.Location = new System.Drawing.Point(16, 26);
            this.lbJob_name.Name = "lbJob_name";
            this.lbJob_name.Size = new System.Drawing.Size(74, 16);
            this.lbJob_name.TabIndex = 1;
            this.lbJob_name.Text = "Job name :";
            // 
            // cmdStop
            // 
            this.cmdStop.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStop.Location = new System.Drawing.Point(294, 396);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(80, 30);
            this.cmdStop.TabIndex = 4;
            this.cmdStop.Text = "S&top";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdRestart
            // 
            this.cmdRestart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestart.Location = new System.Drawing.Point(132, 396);
            this.cmdRestart.Name = "cmdRestart";
            this.cmdRestart.Size = new System.Drawing.Size(80, 30);
            this.cmdRestart.TabIndex = 2;
            this.cmdRestart.Text = "&Restart";
            this.cmdRestart.UseVisualStyleBackColor = true;
            this.cmdRestart.Click += new System.EventHandler(this.cmdRestart_Click);
            // 
            // cmdStart
            // 
            this.cmdStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.Location = new System.Drawing.Point(213, 396);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(80, 30);
            this.cmdStart.TabIndex = 3;
            this.cmdStart.Text = "St&art";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // datDieukien
            // 
            this.datDieukien.AllowUserToAddRows = false;
            this.datDieukien.BackgroundColor = System.Drawing.Color.White;
            this.datDieukien.ColumnHeadersHeight = 30;
            this.datDieukien.ColumnHeadersVisible = false;
            this.datDieukien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Order,
            this.NAME,
            this.STATUS});
            this.datDieukien.Location = new System.Drawing.Point(7, 70);
            this.datDieukien.Name = "datDieukien";
            this.datDieukien.RowHeadersVisible = false;
            this.datDieukien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datDieukien.Size = new System.Drawing.Size(466, 320);
            this.datDieukien.TabIndex = 9;
            this.datDieukien.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtrJob_MouseMove);
            this.datDieukien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtrJob_CellClick);
            this.datDieukien.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtrJob_CellEnter);
            this.datDieukien.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datDieukien_CellContentClick);
            // 
            // Order
            // 
            this.Order.HeaderText = "Column1";
            this.Order.Name = "Order";
            // 
            // NAME
            // 
            this.NAME.HeaderText = "Column2";
            this.NAME.Name = "NAME";
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "Column3";
            this.STATUS.Name = "STATUS";
            // 
            // frmJobStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 434);
            this.Controls.Add(this.datDieukien);
            this.Controls.Add(this.grSearch);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdRestart);
            this.Controls.Add(this.cmdStart);
            this.Name = "frmJobStatus";
            this.Text = "Job Status";
            this.Load += new System.EventHandler(this.frmJobStatus_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmJobStatus_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJobStatus_KeyDown);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdStart, 0);
            this.Controls.SetChildIndex(this.cmdRestart, 0);
            this.Controls.SetChildIndex(this.cmdStop, 0);
            this.Controls.SetChildIndex(this.grSearch, 0);
            this.Controls.SetChildIndex(this.datDieukien, 0);
            this.grSearch.ResumeLayout(false);
            this.grSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDieukien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbJob;
        private System.Windows.Forms.GroupBox grSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbJob_name;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdRestart;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.DataGridView datDieukien;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
    }
}