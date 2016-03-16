namespace BR.BRSYSTEM
{
    partial class frmBackupDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupDB));
            this.txtPathfile = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.cmdbackup = new System.Windows.Forms.Button();
            this.LbPro = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.osProgress1 = new BR.BRSYSTEM.OSProgress();
            this.grFile_path = new System.Windows.Forms.GroupBox();
            this.grProcess = new System.Windows.Forms.GroupBox();
            this.lbPlease = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grFile_path.SuspendLayout();
            this.grProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(398, 117);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click_1);
            // 
            // txtPathfile
            // 
            this.txtPathfile.Location = new System.Drawing.Point(12, 23);
            this.txtPathfile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPathfile.Name = "txtPathfile";
            this.txtPathfile.Size = new System.Drawing.Size(357, 23);
            this.txtPathfile.TabIndex = 1;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.BackgroundImage")));
            this.cmdBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdBrowse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBrowse.Location = new System.Drawing.Point(387, 19);
            this.cmdBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(80, 30);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // cmdbackup
            // 
            this.cmdbackup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdbackup.Location = new System.Drawing.Point(398, 82);
            this.cmdbackup.Name = "cmdbackup";
            this.cmdbackup.Size = new System.Drawing.Size(80, 30);
            this.cmdbackup.TabIndex = 3;
            this.cmdbackup.Text = "Back &up";
            this.cmdbackup.UseVisualStyleBackColor = true;
            this.cmdbackup.Click += new System.EventHandler(this.cmdbackup_Click);
            // 
            // LbPro
            // 
            this.LbPro.AutoSize = true;
            this.LbPro.Location = new System.Drawing.Point(387, 290);
            this.LbPro.Name = "LbPro";
            this.LbPro.Size = new System.Drawing.Size(0, 16);
            this.LbPro.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(12, 22);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 49);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(316, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 49);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // osProgress1
            // 
            this.osProgress1.AutoProgressSpeed = 225;
            this.osProgress1.BackColor = System.Drawing.Color.Transparent;
            this.osProgress1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.osProgress1.IndicatorColor = System.Drawing.Color.White;
            this.osProgress1.Location = new System.Drawing.Point(57, 38);
            this.osProgress1.Name = "osProgress1";
            this.osProgress1.NormalImage = ((System.Drawing.Image)(resources.GetObject("osProgress1.NormalImage")));
            this.osProgress1.PointImage = ((System.Drawing.Image)(resources.GetObject("osProgress1.PointImage")));
            this.osProgress1.Position = 0;
            this.osProgress1.ProgressBoxStyle = BR.BRSYSTEM.OSProgress.OSProgressBoxStyleConstants.osSOLIDBIGGER;
            this.osProgress1.ProgressType = BR.BRSYSTEM.OSProgress.OSProgressTypeConstants.osGRAPHICTYPE;
            this.osProgress1.Size = new System.Drawing.Size(256, 16);
            this.osProgress1.TabIndex = 12;
            // 
            // grFile_path
            // 
            this.grFile_path.Controls.Add(this.txtPathfile);
            this.grFile_path.Controls.Add(this.cmdBrowse);
            this.grFile_path.Location = new System.Drawing.Point(11, 6);
            this.grFile_path.Name = "grFile_path";
            this.grFile_path.Size = new System.Drawing.Size(472, 60);
            this.grFile_path.TabIndex = 0;
            this.grFile_path.TabStop = false;
            this.grFile_path.Text = "File path";
            // 
            // grProcess
            // 
            this.grProcess.Controls.Add(this.lbPlease);
            this.grProcess.Controls.Add(this.osProgress1);
            this.grProcess.Controls.Add(this.pictureBox1);
            this.grProcess.Controls.Add(this.pictureBox2);
            this.grProcess.Location = new System.Drawing.Point(12, 67);
            this.grProcess.Name = "grProcess";
            this.grProcess.Size = new System.Drawing.Size(375, 80);
            this.grProcess.TabIndex = 7;
            this.grProcess.TabStop = false;
            this.grProcess.Text = "Process";
            // 
            // lbPlease
            // 
            this.lbPlease.AutoSize = true;
            this.lbPlease.Location = new System.Drawing.Point(132, 57);
            this.lbPlease.Name = "lbPlease";
            this.lbPlease.Size = new System.Drawing.Size(138, 16);
            this.lbPlease.TabIndex = 15;
            this.lbPlease.Text = "Please wait ... working";
            // 
            // frmBackupDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 161);
            this.Controls.Add(this.grProcess);
            this.Controls.Add(this.grFile_path);
            this.Controls.Add(this.LbPro);
            this.Controls.Add(this.cmdbackup);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmBackupDB";
            this.Text = "Backup database";
            this.Load += new System.EventHandler(this.frmBackup_database_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBackupDB_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmBackupDB_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBackupDB_KeyDown);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdbackup, 0);
            this.Controls.SetChildIndex(this.LbPro, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.grFile_path, 0);
            this.Controls.SetChildIndex(this.grProcess, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grFile_path.ResumeLayout(false);
            this.grFile_path.PerformLayout();
            this.grProcess.ResumeLayout(false);
            this.grProcess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathfile;
        private System.Windows.Forms.Button cmdBrowse;
        //private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button cmdbackup;
        private System.Windows.Forms.Label LbPro;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private OSProgress osProgress1;
        private System.Windows.Forms.GroupBox grFile_path;
        private System.Windows.Forms.GroupBox grProcess;
        private System.Windows.Forms.Label lbPlease;
    }
}