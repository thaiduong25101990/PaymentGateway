namespace BR.BRInterBank
{
    partial class frmSelectTypeIQS
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
            this.cmdcancel = new System.Windows.Forms.Button();
            this.cmdok = new System.Windows.Forms.Button();
            this.grbtype = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.grbtype.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdcancel
            // 
            this.cmdcancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdcancel.Location = new System.Drawing.Point(320, 93);
            this.cmdcancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdcancel.Name = "cmdcancel";
            this.cmdcancel.Size = new System.Drawing.Size(80, 30);
            this.cmdcancel.TabIndex = 8;
            this.cmdcancel.Text = "&Cancel";
            this.cmdcancel.UseVisualStyleBackColor = true;
            this.cmdcancel.Click += new System.EventHandler(this.cmdcancel_Click);
            // 
            // cmdok
            // 
            this.cmdok.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdok.Location = new System.Drawing.Point(232, 93);
            this.cmdok.Margin = new System.Windows.Forms.Padding(4);
            this.cmdok.Name = "cmdok";
            this.cmdok.Size = new System.Drawing.Size(80, 30);
            this.cmdok.TabIndex = 7;
            this.cmdok.Text = "&OK";
            this.cmdok.UseVisualStyleBackColor = true;
            this.cmdok.Click += new System.EventHandler(this.cmdok_Click);
            // 
            // grbtype
            // 
            this.grbtype.Controls.Add(this.label1);
            this.grbtype.Controls.Add(this.cboType);
            this.grbtype.Location = new System.Drawing.Point(7, 13);
            this.grbtype.Margin = new System.Windows.Forms.Padding(4);
            this.grbtype.Name = "grbtype";
            this.grbtype.Padding = new System.Windows.Forms.Padding(4);
            this.grbtype.Size = new System.Drawing.Size(424, 64);
            this.grbtype.TabIndex = 6;
            this.grbtype.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type:";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(105, 23);
            this.cboType.Margin = new System.Windows.Forms.Padding(4);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(288, 24);
            this.cboType.TabIndex = 0;
            // 
            // frmSelectTypeIQS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 136);
            this.Controls.Add(this.cmdcancel);
            this.Controls.Add(this.cmdok);
            this.Controls.Add(this.grbtype);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSelectTypeIQS";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select IQS type ";
            this.Load += new System.EventHandler(this.frmSelectTypeIQS_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectTypeIQS_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSelectTypeIQS_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSelectTypeIQS_KeyDown);
            this.grbtype.ResumeLayout(false);
            this.grbtype.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdcancel;
        private System.Windows.Forms.Button cmdok;
        private System.Windows.Forms.GroupBox grbtype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboType;
    }
}