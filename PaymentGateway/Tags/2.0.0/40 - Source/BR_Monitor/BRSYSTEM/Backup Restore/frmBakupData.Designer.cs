namespace BR.BRSYSTEM
{
    partial class frmBackupData
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
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.rdoCurrentmonth = new System.Windows.Forms.RadioButton();
            this.rdopriviousmonth = new System.Windows.Forms.RadioButton();
            this.datgridTable = new System.Windows.Forms.DataGridView();
            this.clselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdsearch = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbFromdate = new System.Windows.Forms.Label();
            this.lbTodate = new System.Windows.Forms.Label();
            this.datetfrom = new System.Windows.Forms.DateTimePicker();
            this.datetto = new System.Windows.Forms.DateTimePicker();
            this.cmdBackup = new System.Windows.Forms.Button();
            this.chbfromdate_todate = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LbPro = new System.Windows.Forms.Label();
            this.grbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datgridTable)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(603, 348);
            this.cmdClose.TabIndex = 10;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click_1);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(313, 436);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(227, 436);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(52, 436);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(138, 436);
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.rdoCurrentmonth);
            this.grbSearch.Controls.Add(this.rdopriviousmonth);
            this.grbSearch.Controls.Add(this.datgridTable);
            this.grbSearch.Controls.Add(this.cmdsearch);
            this.grbSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearch.Location = new System.Drawing.Point(6, 5);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(677, 244);
            this.grbSearch.TabIndex = 0;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Searching";
            // 
            // rdoCurrentmonth
            // 
            this.rdoCurrentmonth.AutoSize = true;
            this.rdoCurrentmonth.Checked = true;
            this.rdoCurrentmonth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCurrentmonth.Location = new System.Drawing.Point(36, 24);
            this.rdoCurrentmonth.Name = "rdoCurrentmonth";
            this.rdoCurrentmonth.Size = new System.Drawing.Size(109, 20);
            this.rdoCurrentmonth.TabIndex = 1;
            this.rdoCurrentmonth.TabStop = true;
            this.rdoCurrentmonth.Text = "Current month";
            this.rdoCurrentmonth.UseVisualStyleBackColor = true;
            this.rdoCurrentmonth.CheckedChanged += new System.EventHandler(this.rdoCurrentmonth_CheckedChanged);
            // 
            // rdopriviousmonth
            // 
            this.rdopriviousmonth.AutoSize = true;
            this.rdopriviousmonth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdopriviousmonth.Location = new System.Drawing.Point(173, 24);
            this.rdopriviousmonth.Name = "rdopriviousmonth";
            this.rdopriviousmonth.Size = new System.Drawing.Size(120, 20);
            this.rdopriviousmonth.TabIndex = 2;
            this.rdopriviousmonth.Text = "Previous months";
            this.rdopriviousmonth.UseVisualStyleBackColor = true;
            this.rdopriviousmonth.CheckedChanged += new System.EventHandler(this.rdopriviousmonth_CheckedChanged);
            // 
            // datgridTable
            // 
            this.datgridTable.AllowUserToAddRows = false;
            this.datgridTable.AllowUserToDeleteRows = false;
            this.datgridTable.AllowUserToResizeRows = false;
            this.datgridTable.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.datgridTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datgridTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clselect,
            this.Column1,
            this.Column2,
            this.Column4});
            this.datgridTable.Location = new System.Drawing.Point(6, 60);
            this.datgridTable.MultiSelect = false;
            this.datgridTable.Name = "datgridTable";
            this.datgridTable.Size = new System.Drawing.Size(665, 177);
            this.datgridTable.TabIndex = 4;
            this.datgridTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datgridTable_CellClick);
            // 
            // clselect
            // 
            this.clselect.HeaderText = "Select";
            this.clselect.Name = "clselect";
            this.clselect.Width = 60;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "         Table name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 180;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "                    Path file";
            this.Column2.Name = "Column2";
            this.Column2.Width = 230;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Previous export date";
            this.Column4.Name = "Column4";
            this.Column4.Width = 150;
            // 
            // cmdsearch
            // 
            this.cmdsearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdsearch.Location = new System.Drawing.Point(591, 24);
            this.cmdsearch.Name = "cmdsearch";
            this.cmdsearch.Size = new System.Drawing.Size(80, 30);
            this.cmdsearch.TabIndex = 3;
            this.cmdsearch.Text = "&Search";
            this.cmdsearch.UseVisualStyleBackColor = true;
            this.cmdsearch.Visible = false;
            this.cmdsearch.Click += new System.EventHandler(this.cmdsearch_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 325);
            this.progressBar1.MarqueeAnimationSpeed = 500;
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(665, 17);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 8;
            // 
            // lbFromdate
            // 
            this.lbFromdate.AutoSize = true;
            this.lbFromdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFromdate.Location = new System.Drawing.Point(412, 258);
            this.lbFromdate.Name = "lbFromdate";
            this.lbFromdate.Size = new System.Drawing.Size(76, 16);
            this.lbFromdate.TabIndex = 1;
            this.lbFromdate.Text = "From date :";
            // 
            // lbTodate
            // 
            this.lbTodate.AutoSize = true;
            this.lbTodate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTodate.Location = new System.Drawing.Point(412, 287);
            this.lbTodate.Name = "lbTodate";
            this.lbTodate.Size = new System.Drawing.Size(61, 16);
            this.lbTodate.TabIndex = 1;
            this.lbTodate.Text = "To date :";
            // 
            // datetfrom
            // 
            this.datetfrom.CustomFormat = "dd/MM/yyyy";
            this.datetfrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetfrom.Location = new System.Drawing.Point(492, 255);
            this.datetfrom.Name = "datetfrom";
            this.datetfrom.Size = new System.Drawing.Size(191, 23);
            this.datetfrom.TabIndex = 6;
            // 
            // datetto
            // 
            this.datetto.CustomFormat = "dd/MM/yyyy";
            this.datetto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetto.Location = new System.Drawing.Point(492, 284);
            this.datetto.Name = "datetto";
            this.datetto.Size = new System.Drawing.Size(191, 23);
            this.datetto.TabIndex = 7;
            this.datetto.ValueChanged += new System.EventHandler(this.datetto_ValueChanged);
            // 
            // cmdBackup
            // 
            this.cmdBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBackup.Location = new System.Drawing.Point(517, 349);
            this.cmdBackup.Name = "cmdBackup";
            this.cmdBackup.Size = new System.Drawing.Size(80, 30);
            this.cmdBackup.TabIndex = 9;
            this.cmdBackup.Text = "&Backup";
            this.cmdBackup.UseVisualStyleBackColor = true;
            this.cmdBackup.Click += new System.EventHandler(this.cmdBackup_Click);
            // 
            // chbfromdate_todate
            // 
            this.chbfromdate_todate.AutoSize = true;
            this.chbfromdate_todate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbfromdate_todate.Location = new System.Drawing.Point(18, 256);
            this.chbfromdate_todate.Name = "chbfromdate_todate";
            this.chbfromdate_todate.Size = new System.Drawing.Size(130, 20);
            this.chbfromdate_todate.TabIndex = 5;
            this.chbfromdate_todate.TabStop = false;
            this.chbfromdate_todate.Text = "From date to date";
            this.chbfromdate_todate.UseVisualStyleBackColor = true;
            this.chbfromdate_todate.CheckedChanged += new System.EventHandler(this.CheckBox_change);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            // 
            // LbPro
            // 
            this.LbPro.AutoSize = true;
            this.LbPro.Location = new System.Drawing.Point(15, 325);
            this.LbPro.Name = "LbPro";
            this.LbPro.Size = new System.Drawing.Size(0, 13);
            this.LbPro.TabIndex = 5;
            // 
            // frmBackupData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 384);
            this.Controls.Add(this.LbPro);
            this.Controls.Add(this.chbfromdate_todate);
            this.Controls.Add(this.datetto);
            this.Controls.Add(this.datetfrom);
            this.Controls.Add(this.lbTodate);
            this.Controls.Add(this.lbFromdate);
            this.Controls.Add(this.cmdBackup);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.grbSearch);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmBackupData";
            this.Text = "Backup table";
            this.Load += new System.EventHandler(this.frmBackupData_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmBackupData_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBackupData_KeyDown);
            this.Controls.SetChildIndex(this.grbSearch, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.cmdBackup, 0);
            this.Controls.SetChildIndex(this.lbFromdate, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.lbTodate, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.datetfrom, 0);
            this.Controls.SetChildIndex(this.datetto, 0);
            this.Controls.SetChildIndex(this.chbfromdate_todate, 0);
            this.Controls.SetChildIndex(this.LbPro, 0);
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datgridTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.DataGridView datgridTable;
        private System.Windows.Forms.RadioButton rdoCurrentmonth;
        private System.Windows.Forms.RadioButton rdopriviousmonth;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbFromdate;
        private System.Windows.Forms.Label lbTodate;
        private System.Windows.Forms.Button cmdsearch;
        private System.Windows.Forms.DateTimePicker datetfrom;
        private System.Windows.Forms.DateTimePicker datetto;
        private System.Windows.Forms.Button cmdBackup;
        private System.Windows.Forms.CheckBox chbfromdate_todate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label LbPro;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}