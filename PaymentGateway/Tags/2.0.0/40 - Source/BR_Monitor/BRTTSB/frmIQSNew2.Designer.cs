﻿namespace BR.BRTTSB
{
    partial class frmIQSNew2
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIQSContent = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIQSContent);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(795, 301);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IQS content";
            // 
            // txtIQSContent
            // 
            this.txtIQSContent.Location = new System.Drawing.Point(7, 20);
            this.txtIQSContent.Multiline = true;
            this.txtIQSContent.Name = "txtIQSContent";
            this.txtIQSContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIQSContent.Size = new System.Drawing.Size(777, 275);
            this.txtIQSContent.TabIndex = 0;
            this.txtIQSContent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtIQSContent_MouseMove);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(717, 309);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(631, 310);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(80, 30);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Product code :";
            this.label1.Visible = false;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(125, 320);
            this.txtProductCode.MaxLength = 3;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(96, 20);
            this.txtProductCode.TabIndex = 0;
            this.txtProductCode.Visible = false;
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // frmIQSNew2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 354);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.groupBox2);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmIQSNew2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TTSP_IQS message";
            this.Load += new System.EventHandler(this.frmIQSNew2_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIQSNew2_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIQSNew2_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtIQSContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProductCode;
    }
}