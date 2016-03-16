namespace BR.BRSYSTEM
{
    partial class frmParam
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
            this.cboOK = new System.Windows.Forms.Button();
            this.cboCalcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboOK
            // 
            this.cboOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOK.Location = new System.Drawing.Point(28, 20);
            this.cboOK.Name = "cboOK";
            this.cboOK.Size = new System.Drawing.Size(80, 26);
            this.cboOK.TabIndex = 0;
            this.cboOK.Text = "&OK";
            this.cboOK.UseVisualStyleBackColor = true;
            this.cboOK.Click += new System.EventHandler(this.cboOK_Click);
            // 
            // cboCalcel
            // 
            this.cboCalcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalcel.Location = new System.Drawing.Point(149, 20);
            this.cboCalcel.Name = "cboCalcel";
            this.cboCalcel.Size = new System.Drawing.Size(80, 26);
            this.cboCalcel.TabIndex = 1;
            this.cboCalcel.Text = "&Cancel";
            this.cboCalcel.UseVisualStyleBackColor = true;
            this.cboCalcel.Click += new System.EventHandler(this.cboCalcel_Click);
            // 
            // frmParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 58);
            this.Controls.Add(this.cboCalcel);
            this.Controls.Add(this.cboOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmParam";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.frmParam_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cboOK;
        private System.Windows.Forms.Button cboCalcel;
    }
}