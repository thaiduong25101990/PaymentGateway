namespace BR.BRSYSTEM
{
    partial class frmRestoreDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRestoreDB));
            this.label1 = new System.Windows.Forms.Label();
            this.txtfile = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmdBrows = new System.Windows.Forms.Button();
            this.cmdRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(352, 95);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "File path :";
            // 
            // txtfile
            // 
            this.txtfile.Location = new System.Drawing.Point(80, 18);
            this.txtfile.Name = "txtfile";
            this.txtfile.Size = new System.Drawing.Size(278, 23);
            this.txtfile.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(80, 62);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(278, 19);
            this.progressBar1.TabIndex = 2;
            // 
            // cmdBrows
            // 
            this.cmdBrows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdBrows.BackgroundImage")));
            this.cmdBrows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdBrows.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBrows.Location = new System.Drawing.Point(360, 17);
            this.cmdBrows.Name = "cmdBrows";
            this.cmdBrows.Size = new System.Drawing.Size(37, 24);
            this.cmdBrows.TabIndex = 1;
            this.cmdBrows.UseVisualStyleBackColor = true;
            this.cmdBrows.Click += new System.EventHandler(this.cmdBrows_Click);
            // 
            // cmdRestore
            // 
            this.cmdRestore.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestore.Location = new System.Drawing.Point(269, 95);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(80, 30);
            this.cmdRestore.TabIndex = 3;
            this.cmdRestore.Text = "&Restore";
            this.cmdRestore.UseVisualStyleBackColor = true;
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // frmRestoreDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 133);
            this.Controls.Add(this.txtfile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdBrows);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cmdRestore);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRestoreDB";
            this.Text = "Restore database";
            this.Load += new System.EventHandler(this.frmRestoreDB_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmRestoreDB_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRestoreDB_KeyDown);
            this.Controls.SetChildIndex(this.cmdRestore, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.cmdBrows, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtfile, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtfile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button cmdBrows;
        private System.Windows.Forms.Button cmdRestore;
    }
}