namespace BR.BRIBPS.Branch
{
    partial class frmExtractData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExtractData));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pickerDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgTAD = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkDBlink = new System.Windows.Forms.CheckBox();
            this.cmdBrows = new System.Windows.Forms.Button();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTAD)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pickerDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // pickerDate
            // 
            this.pickerDate.CustomFormat = "dd/MM/yyyy";
            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerDate.Location = new System.Drawing.Point(51, 21);
            this.pickerDate.Margin = new System.Windows.Forms.Padding(2);
            this.pickerDate.Name = "pickerDate";
            this.pickerDate.Size = new System.Drawing.Size(146, 22);
            this.pickerDate.TabIndex = 3;
            this.pickerDate.ValueChanged += new System.EventHandler(this.OnDate);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date:";
            // 
            // dtgTAD
            // 
            this.dtgTAD.AllowUserToAddRows = false;
            this.dtgTAD.AllowUserToDeleteRows = false;
            this.dtgTAD.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgTAD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgTAD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgTAD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgTAD.Location = new System.Drawing.Point(12, 102);
            this.dtgTAD.Margin = new System.Windows.Forms.Padding(2);
            this.dtgTAD.Name = "dtgTAD";
            this.dtgTAD.ReadOnly = true;
            this.dtgTAD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgTAD.Size = new System.Drawing.Size(357, 269);
            this.dtgTAD.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button1.Location = new System.Drawing.Point(382, 70);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Extract";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OnExtract);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(398, 343);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "&Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnClose);
            // 
            // chkDBlink
            // 
            this.chkDBlink.AutoSize = true;
            this.chkDBlink.Location = new System.Drawing.Point(382, 103);
            this.chkDBlink.Name = "chkDBlink";
            this.chkDBlink.Size = new System.Drawing.Size(62, 19);
            this.chkDBlink.TabIndex = 7;
            this.chkDBlink.Text = "DBlink";
            this.chkDBlink.UseVisualStyleBackColor = true;
            this.chkDBlink.CheckedChanged += new System.EventHandler(this.chkDBlink_CheckedChanged);
            // 
            // cmdBrows
            // 
            this.cmdBrows.Location = new System.Drawing.Point(297, 67);
            this.cmdBrows.Name = "cmdBrows";
            this.cmdBrows.Size = new System.Drawing.Size(36, 30);
            this.cmdBrows.TabIndex = 9;
            this.cmdBrows.Text = "  .....  ";
            this.cmdBrows.UseVisualStyleBackColor = true;
            this.cmdBrows.Click += new System.EventHandler(this.cmdBrows_Click);
            // 
            // txtFilepath
            // 
            this.txtFilepath.Location = new System.Drawing.Point(12, 71);
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(283, 21);
            this.txtFilepath.TabIndex = 8;
            this.txtFilepath.TextChanged += new System.EventHandler(this.txtFilepath_TextChanged);
            // 
            // frmExtractData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 382);
            this.Controls.Add(this.cmdBrows);
            this.Controls.Add(this.txtFilepath);
            this.Controls.Add(this.chkDBlink);
            this.Controls.Add(this.dtgTAD);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmExtractData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extract Data For Branch";
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTAD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;        
        private System.Windows.Forms.DataGridView dtgTAD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkDBlink;
        private System.Windows.Forms.Button cmdBrows;
        private System.Windows.Forms.TextBox txtFilepath;
    }
}