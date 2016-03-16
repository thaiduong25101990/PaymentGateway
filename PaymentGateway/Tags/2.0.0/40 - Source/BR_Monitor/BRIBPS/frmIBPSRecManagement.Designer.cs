namespace BR.BRIBPS
{
    partial class frmIBPSRecManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIBPSRecManagement));
            this.chkSI = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSI = new System.Windows.Forms.TextBox();
            this.txtIBPS = new System.Windows.Forms.TextBox();
            this.chkIBPS = new System.Windows.Forms.CheckBox();
            this.txtTTSP = new System.Windows.Forms.TextBox();
            this.chkTTSP = new System.Windows.Forms.CheckBox();
            this.txtST = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkST = new System.Windows.Forms.CheckBox();
            this.txtSWIFT = new System.Windows.Forms.TextBox();
            this.chkSWIFT = new System.Windows.Forms.CheckBox();
            this.txtSW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSW = new System.Windows.Forms.CheckBox();
            this.txtVCB = new System.Windows.Forms.TextBox();
            this.chkVCB = new System.Windows.Forms.CheckBox();
            this.txtSV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSV = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.txtIn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btSIBS = new System.Windows.Forms.Button();
            this.btSWIFT = new System.Windows.Forms.Button();
            this.btVCB = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pickerDate = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.btUnLock = new System.Windows.Forms.Button();
            this.btLockIBPS = new System.Windows.Forms.Button();
            this.btLockTTSP = new System.Windows.Forms.Button();
            this.btLockSWIFT = new System.Windows.Forms.Button();
            this.btLockVCB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkSI
            // 
            this.chkSI.AutoSize = true;
            this.chkSI.Location = new System.Drawing.Point(105, 93);
            this.chkSI.Name = "chkSI";
            this.chkSI.Size = new System.Drawing.Size(50, 17);
            this.chkSI.TabIndex = 0;
            this.chkSI.Text = "SIBS";
            this.chkSI.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IBPS:";
            // 
            // txtSI
            // 
            this.txtSI.Location = new System.Drawing.Point(170, 91);
            this.txtSI.Name = "txtSI";
            this.txtSI.Size = new System.Drawing.Size(68, 20);
            this.txtSI.TabIndex = 5;
            // 
            // txtIBPS
            // 
            this.txtIBPS.Location = new System.Drawing.Point(323, 91);
            this.txtIBPS.Name = "txtIBPS";
            this.txtIBPS.Size = new System.Drawing.Size(68, 20);
            this.txtIBPS.TabIndex = 7;
            // 
            // chkIBPS
            // 
            this.chkIBPS.AutoSize = true;
            this.chkIBPS.Location = new System.Drawing.Point(257, 93);
            this.chkIBPS.Name = "chkIBPS";
            this.chkIBPS.Size = new System.Drawing.Size(50, 17);
            this.chkIBPS.TabIndex = 6;
            this.chkIBPS.Text = "IBPS";
            this.chkIBPS.UseVisualStyleBackColor = true;
            // 
            // txtTTSP
            // 
            this.txtTTSP.Location = new System.Drawing.Point(332, 244);
            this.txtTTSP.Name = "txtTTSP";
            this.txtTTSP.Size = new System.Drawing.Size(68, 20);
            this.txtTTSP.TabIndex = 12;
            this.txtTTSP.Visible = false;
            // 
            // chkTTSP
            // 
            this.chkTTSP.AutoSize = true;
            this.chkTTSP.Location = new System.Drawing.Point(266, 246);
            this.chkTTSP.Name = "chkTTSP";
            this.chkTTSP.Size = new System.Drawing.Size(54, 17);
            this.chkTTSP.TabIndex = 11;
            this.chkTTSP.Text = "TTSP";
            this.chkTTSP.UseVisualStyleBackColor = true;
            this.chkTTSP.Visible = false;
            // 
            // txtST
            // 
            this.txtST.Location = new System.Drawing.Point(179, 244);
            this.txtST.Name = "txtST";
            this.txtST.Size = new System.Drawing.Size(68, 20);
            this.txtST.TabIndex = 10;
            this.txtST.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "TTSP:";
            this.label2.Visible = false;
            // 
            // chkST
            // 
            this.chkST.AutoSize = true;
            this.chkST.Location = new System.Drawing.Point(114, 246);
            this.chkST.Name = "chkST";
            this.chkST.Size = new System.Drawing.Size(50, 17);
            this.chkST.TabIndex = 8;
            this.chkST.Text = "SIBS";
            this.chkST.UseVisualStyleBackColor = true;
            this.chkST.Visible = false;
            // 
            // txtSWIFT
            // 
            this.txtSWIFT.Location = new System.Drawing.Point(323, 127);
            this.txtSWIFT.Name = "txtSWIFT";
            this.txtSWIFT.Size = new System.Drawing.Size(68, 20);
            this.txtSWIFT.TabIndex = 17;
            // 
            // chkSWIFT
            // 
            this.chkSWIFT.AutoSize = true;
            this.chkSWIFT.Location = new System.Drawing.Point(257, 129);
            this.chkSWIFT.Name = "chkSWIFT";
            this.chkSWIFT.Size = new System.Drawing.Size(60, 17);
            this.chkSWIFT.TabIndex = 16;
            this.chkSWIFT.Text = "SWIFT";
            this.chkSWIFT.UseVisualStyleBackColor = true;
            // 
            // txtSW
            // 
            this.txtSW.Location = new System.Drawing.Point(170, 127);
            this.txtSW.Name = "txtSW";
            this.txtSW.Size = new System.Drawing.Size(68, 20);
            this.txtSW.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "SWIFT:";
            // 
            // chkSW
            // 
            this.chkSW.AutoSize = true;
            this.chkSW.Location = new System.Drawing.Point(105, 129);
            this.chkSW.Name = "chkSW";
            this.chkSW.Size = new System.Drawing.Size(50, 17);
            this.chkSW.TabIndex = 13;
            this.chkSW.Text = "SIBS";
            this.chkSW.UseVisualStyleBackColor = true;
            // 
            // txtVCB
            // 
            this.txtVCB.Location = new System.Drawing.Point(323, 164);
            this.txtVCB.Name = "txtVCB";
            this.txtVCB.Size = new System.Drawing.Size(68, 20);
            this.txtVCB.TabIndex = 22;
            // 
            // chkVCB
            // 
            this.chkVCB.AutoSize = true;
            this.chkVCB.Location = new System.Drawing.Point(257, 166);
            this.chkVCB.Name = "chkVCB";
            this.chkVCB.Size = new System.Drawing.Size(47, 17);
            this.chkVCB.TabIndex = 21;
            this.chkVCB.Text = "VCB";
            this.chkVCB.UseVisualStyleBackColor = true;
            // 
            // txtSV
            // 
            this.txtSV.Location = new System.Drawing.Point(170, 164);
            this.txtSV.Name = "txtSV";
            this.txtSV.Size = new System.Drawing.Size(68, 20);
            this.txtSV.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "VCB:";
            // 
            // chkSV
            // 
            this.chkSV.AutoSize = true;
            this.chkSV.Location = new System.Drawing.Point(105, 166);
            this.chkSV.Name = "chkSV";
            this.chkSV.Size = new System.Drawing.Size(50, 17);
            this.chkSV.TabIndex = 18;
            this.chkSV.Text = "SIBS";
            this.chkSV.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 23;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnReset);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(352, 190);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 24;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnClose);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimer);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Out :";
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(124, 58);
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(114, 20);
            this.txtOut.TabIndex = 26;
            // 
            // txtIn
            // 
            this.txtIn.Location = new System.Drawing.Point(282, 61);
            this.txtIn.Name = "txtIn";
            this.txtIn.Size = new System.Drawing.Size(109, 20);
            this.txtIn.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "In :";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btSIBS
            // 
            this.btSIBS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSIBS.BackgroundImage")));
            this.btSIBS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSIBS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSIBS.Location = new System.Drawing.Point(87, 58);
            this.btSIBS.Name = "btSIBS";
            this.btSIBS.Size = new System.Drawing.Size(31, 20);
            this.btSIBS.TabIndex = 29;
            this.btSIBS.UseVisualStyleBackColor = true;
            this.btSIBS.Click += new System.EventHandler(this.OnSIBS);
            // 
            // btSWIFT
            // 
            this.btSWIFT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSWIFT.BackgroundImage")));
            this.btSWIFT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSWIFT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSWIFT.Location = new System.Drawing.Point(397, 127);
            this.btSWIFT.Name = "btSWIFT";
            this.btSWIFT.Size = new System.Drawing.Size(31, 20);
            this.btSWIFT.TabIndex = 30;
            this.btSWIFT.UseVisualStyleBackColor = true;
            this.btSWIFT.Click += new System.EventHandler(this.OnSWIFT);
            // 
            // btVCB
            // 
            this.btVCB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btVCB.BackgroundImage")));
            this.btVCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btVCB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btVCB.Location = new System.Drawing.Point(397, 163);
            this.btVCB.Name = "btVCB";
            this.btVCB.Size = new System.Drawing.Size(31, 20);
            this.btVCB.TabIndex = 31;
            this.btVCB.UseVisualStyleBackColor = true;
            this.btVCB.Click += new System.EventHandler(this.OnVCB);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "TRANS_DATE";
            // 
            // pickerDate
            // 
            this.pickerDate.CustomFormat = "dd/MM/yyyy";
            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerDate.Location = new System.Drawing.Point(97, 19);
            this.pickerDate.Name = "pickerDate";
            this.pickerDate.Size = new System.Drawing.Size(141, 20);
            this.pickerDate.TabIndex = 33;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(180, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 30);
            this.button3.TabIndex = 34;
            this.button3.Text = "Lock";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnLock);
            // 
            // btUnLock
            // 
            this.btUnLock.Location = new System.Drawing.Point(94, 190);
            this.btUnLock.Name = "btUnLock";
            this.btUnLock.Size = new System.Drawing.Size(80, 30);
            this.btUnLock.TabIndex = 35;
            this.btUnLock.Text = "UnLock";
            this.btUnLock.UseVisualStyleBackColor = true;
            this.btUnLock.Click += new System.EventHandler(this.OnUnLock);
            // 
            // btLockIBPS
            // 
            this.btLockIBPS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btLockIBPS.BackgroundImage")));
            this.btLockIBPS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLockIBPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLockIBPS.Location = new System.Drawing.Point(78, 91);
            this.btLockIBPS.Name = "btLockIBPS";
            this.btLockIBPS.Size = new System.Drawing.Size(21, 19);
            this.btLockIBPS.TabIndex = 36;
            this.btLockIBPS.UseVisualStyleBackColor = true;
            this.btLockIBPS.Visible = false;
            // 
            // btLockTTSP
            // 
            this.btLockTTSP.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btLockTTSP.BackgroundImage")));
            this.btLockTTSP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLockTTSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLockTTSP.Location = new System.Drawing.Point(87, 246);
            this.btLockTTSP.Name = "btLockTTSP";
            this.btLockTTSP.Size = new System.Drawing.Size(21, 20);
            this.btLockTTSP.TabIndex = 37;
            this.btLockTTSP.UseVisualStyleBackColor = true;
            this.btLockTTSP.Visible = false;
            // 
            // btLockSWIFT
            // 
            this.btLockSWIFT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btLockSWIFT.BackgroundImage")));
            this.btLockSWIFT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLockSWIFT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLockSWIFT.Location = new System.Drawing.Point(78, 127);
            this.btLockSWIFT.Name = "btLockSWIFT";
            this.btLockSWIFT.Size = new System.Drawing.Size(21, 20);
            this.btLockSWIFT.TabIndex = 38;
            this.btLockSWIFT.UseVisualStyleBackColor = true;
            this.btLockSWIFT.Visible = false;
            // 
            // btLockVCB
            // 
            this.btLockVCB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btLockVCB.BackgroundImage")));
            this.btLockVCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLockVCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLockVCB.Location = new System.Drawing.Point(78, 163);
            this.btLockVCB.Name = "btLockVCB";
            this.btLockVCB.Size = new System.Drawing.Size(21, 20);
            this.btLockVCB.TabIndex = 39;
            this.btLockVCB.UseVisualStyleBackColor = true;
            this.btLockVCB.Visible = false;
            // 
            // frmIBPSRecManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 232);
            this.Controls.Add(this.btLockVCB);
            this.Controls.Add(this.btLockSWIFT);
            this.Controls.Add(this.btLockTTSP);
            this.Controls.Add(this.btLockIBPS);
            this.Controls.Add(this.btUnLock);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pickerDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btVCB);
            this.Controls.Add(this.btSWIFT);
            this.Controls.Add(this.btSIBS);
            this.Controls.Add(this.txtIn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtVCB);
            this.Controls.Add(this.chkVCB);
            this.Controls.Add(this.txtSV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkSV);
            this.Controls.Add(this.txtSWIFT);
            this.Controls.Add(this.chkSWIFT);
            this.Controls.Add(this.txtSW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkSW);
            this.Controls.Add(this.txtTTSP);
            this.Controls.Add(this.chkTTSP);
            this.Controls.Add(this.txtST);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkST);
            this.Controls.Add(this.txtIBPS);
            this.Controls.Add(this.chkIBPS);
            this.Controls.Add(this.txtSI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmIBPSRecManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reconcile Management";
            this.Load += new System.EventHandler(this.frmIBPSRecManagement_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIBPSRecManagement_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIBPSRecManagement_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSI;
        private System.Windows.Forms.TextBox txtIBPS;
        private System.Windows.Forms.CheckBox chkIBPS;
        private System.Windows.Forms.TextBox txtTTSP;
        private System.Windows.Forms.CheckBox chkTTSP;
        private System.Windows.Forms.TextBox txtST;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkST;
        private System.Windows.Forms.TextBox txtSWIFT;
        private System.Windows.Forms.CheckBox chkSWIFT;
        private System.Windows.Forms.TextBox txtSW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSW;
        private System.Windows.Forms.TextBox txtVCB;
        private System.Windows.Forms.CheckBox chkVCB;
        private System.Windows.Forms.TextBox txtSV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSV;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.TextBox txtIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btSIBS;
        private System.Windows.Forms.Button btSWIFT;
        private System.Windows.Forms.Button btVCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker pickerDate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btUnLock;
        private System.Windows.Forms.Button btLockIBPS;
        private System.Windows.Forms.Button btLockTTSP;
        private System.Windows.Forms.Button btLockSWIFT;
        private System.Windows.Forms.Button btLockVCB;

    }
}