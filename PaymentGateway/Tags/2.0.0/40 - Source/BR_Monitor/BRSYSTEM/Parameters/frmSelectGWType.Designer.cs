namespace BR.BRSYSTEM
{
    partial class frmSelectGWType
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
            this.cmdok = new System.Windows.Forms.Button();
            this.cmdcancel = new System.Windows.Forms.Button();
            this.grbtype = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // cmdok
            // 
            this.cmdok.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdok.Location = new System.Drawing.Point(171, 104);
            this.cmdok.Name = "cmdok";
            this.cmdok.Size = new System.Drawing.Size(80, 30);
            this.cmdok.TabIndex = 1;
            this.cmdok.Text = "&OK";
            this.cmdok.UseVisualStyleBackColor = true;
            this.cmdok.Click += new System.EventHandler(this.cmdok_Click);
            // 
            // cmdcancel
            // 
            this.cmdcancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdcancel.Location = new System.Drawing.Point(171, 140);
            this.cmdcancel.Name = "cmdcancel";
            this.cmdcancel.Size = new System.Drawing.Size(80, 30);
            this.cmdcancel.TabIndex = 2;
            this.cmdcancel.Text = "&Cancel";
            this.cmdcancel.UseVisualStyleBackColor = true;
            this.cmdcancel.Click += new System.EventHandler(this.cmdcancel_Click);
            // 
            // grbtype
            // 
            this.grbtype.Location = new System.Drawing.Point(8, 2);
            this.grbtype.Name = "grbtype";
            this.grbtype.Size = new System.Drawing.Size(157, 172);
            this.grbtype.TabIndex = 0;
            this.grbtype.TabStop = false;
            // 
            // frmSelectGWType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 180);
            this.Controls.Add(this.cmdcancel);
            this.Controls.Add(this.cmdok);
            this.Controls.Add(this.grbtype);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmSelectGWType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Channel";
            this.Load += new System.EventHandler(this.frmSelectGWType_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectGWType_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdok;
        private System.Windows.Forms.Button cmdcancel;
        private System.Windows.Forms.GroupBox grbtype;
    }
}