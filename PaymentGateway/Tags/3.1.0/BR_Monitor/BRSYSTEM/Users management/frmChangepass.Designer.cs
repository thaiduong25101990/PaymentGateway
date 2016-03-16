namespace BR.BRSYSTEM
{
    partial class frmChangepass
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
            this.lbpasscu = new System.Windows.Forms.Label();
            this.txtoldpass = new System.Windows.Forms.TextBox();
            this.lbpassmoi = new System.Windows.Forms.Label();
            this.txtnewpass = new System.Windows.Forms.TextBox();
            this.lbconfigpass = new System.Windows.Forms.Label();
            this.txtconfimpass = new System.Windows.Forms.TextBox();
            this.cmdok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(222, 102);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.TabIndex = 20;
            // 
            // cmdDelete
            // 
            this.cmdDelete.TabIndex = 19;
            // 
            // cmdAdd
            // 
            this.cmdAdd.TabIndex = 10;
            // 
            // cmdEdit
            // 
            this.cmdEdit.TabIndex = 9;
            // 
            // lbpasscu
            // 
            this.lbpasscu.AutoSize = true;
            this.lbpasscu.Location = new System.Drawing.Point(12, 18);
            this.lbpasscu.Name = "lbpasscu";
            this.lbpasscu.Size = new System.Drawing.Size(91, 16);
            this.lbpasscu.TabIndex = 7;
            this.lbpasscu.Text = "Old password:";
            // 
            // txtoldpass
            // 
            this.txtoldpass.Location = new System.Drawing.Point(122, 15);
            this.txtoldpass.Name = "txtoldpass";
            this.txtoldpass.PasswordChar = '*';
            this.txtoldpass.Size = new System.Drawing.Size(180, 23);
            this.txtoldpass.TabIndex = 0;
            this.txtoldpass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            // 
            // lbpassmoi
            // 
            this.lbpassmoi.AutoSize = true;
            this.lbpassmoi.Location = new System.Drawing.Point(12, 47);
            this.lbpassmoi.Name = "lbpassmoi";
            this.lbpassmoi.Size = new System.Drawing.Size(97, 16);
            this.lbpassmoi.TabIndex = 8;
            this.lbpassmoi.Text = "New password:";
            // 
            // txtnewpass
            // 
            this.txtnewpass.Location = new System.Drawing.Point(122, 44);
            this.txtnewpass.Name = "txtnewpass";
            this.txtnewpass.PasswordChar = '*';
            this.txtnewpass.Size = new System.Drawing.Size(180, 23);
            this.txtnewpass.TabIndex = 1;
            this.txtnewpass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            // 
            // lbconfigpass
            // 
            this.lbconfigpass.AutoSize = true;
            this.lbconfigpass.Location = new System.Drawing.Point(12, 76);
            this.lbconfigpass.Name = "lbconfigpass";
            this.lbconfigpass.Size = new System.Drawing.Size(112, 16);
            this.lbconfigpass.TabIndex = 6;
            this.lbconfigpass.Text = "Confim password:";
            // 
            // txtconfimpass
            // 
            this.txtconfimpass.Location = new System.Drawing.Point(122, 73);
            this.txtconfimpass.Name = "txtconfimpass";
            this.txtconfimpass.PasswordChar = '*';
            this.txtconfimpass.Size = new System.Drawing.Size(180, 23);
            this.txtconfimpass.TabIndex = 2;
            this.txtconfimpass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            // 
            // cmdok
            // 
            this.cmdok.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdok.Location = new System.Drawing.Point(136, 102);
            this.cmdok.Name = "cmdok";
            this.cmdok.Size = new System.Drawing.Size(80, 30);
            this.cmdok.TabIndex = 3;
            this.cmdok.Text = "&OK";
            this.cmdok.UseVisualStyleBackColor = true;
            this.cmdok.Click += new System.EventHandler(this.cmdok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(307, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(307, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(306, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "*";
            // 
            // frmChangepass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 139);
            this.Controls.Add(this.txtnewpass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtconfimpass);
            this.Controls.Add(this.txtoldpass);
            this.Controls.Add(this.lbconfigpass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbpasscu);
            this.Controls.Add(this.lbpassmoi);
            this.Controls.Add(this.cmdok);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmChangepass";
            this.Text = "Change password";
            this.Load += new System.EventHandler(this.frmChangepass_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmChangepass_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChangepass_KeyDown);
            this.Controls.SetChildIndex(this.cmdok, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.lbpassmoi, 0);
            this.Controls.SetChildIndex(this.lbpasscu, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lbconfigpass, 0);
            this.Controls.SetChildIndex(this.txtoldpass, 0);
            this.Controls.SetChildIndex(this.txtconfimpass, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtnewpass, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbpasscu;
        private System.Windows.Forms.TextBox txtoldpass;
        private System.Windows.Forms.Label lbpassmoi;
        private System.Windows.Forms.TextBox txtnewpass;
        private System.Windows.Forms.Label lbconfigpass;
        private System.Windows.Forms.TextBox txtconfimpass;
        private System.Windows.Forms.Button cmdok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}