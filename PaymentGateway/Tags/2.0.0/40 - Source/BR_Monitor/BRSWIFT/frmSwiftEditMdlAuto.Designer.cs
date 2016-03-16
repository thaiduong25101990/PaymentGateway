namespace BR.BRSWIFT
{
    partial class frmSwiftEditMdlAuto
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCriteria = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.txtPriority = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.cboModule = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmdSave1 = new System.Windows.Forms.Button();
            this.cmdClose1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(427, 515);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(339, 515);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(231, 536);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(-2, 536);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Size = new System.Drawing.Size(107, 37);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(112, 536);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.Size = new System.Drawing.Size(107, 37);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtCriteria);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Controls.Add(this.txtPriority);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl1);
            this.groupBox1.Controls.Add(this.cboModule);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(495, 396);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Criteria description:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Criteria message:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(140, 237);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(348, 152);
            this.txtDescription.TabIndex = 6;
            // 
            // txtCriteria
            // 
            this.txtCriteria.BackColor = System.Drawing.Color.White;
            this.txtCriteria.Location = new System.Drawing.Point(140, 144);
            this.txtCriteria.MaxLength = 255;
            this.txtCriteria.Multiline = true;
            this.txtCriteria.Name = "txtCriteria";
            this.txtCriteria.ReadOnly = true;
            this.txtCriteria.Size = new System.Drawing.Size(348, 87);
            this.txtCriteria.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Keyword:";
            // 
            // txtKeyword
            // 
            this.txtKeyword.BackColor = System.Drawing.Color.White;
            this.txtKeyword.Location = new System.Drawing.Point(140, 115);
            this.txtKeyword.MaxLength = 20;
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.ReadOnly = true;
            this.txtKeyword.Size = new System.Drawing.Size(348, 22);
            this.txtKeyword.TabIndex = 4;
            // 
            // txtPriority
            // 
            this.txtPriority.Location = new System.Drawing.Point(140, 86);
            this.txtPriority.MaxLength = 3;
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.Size = new System.Drawing.Size(215, 22);
            this.txtPriority.TabIndex = 3;
            this.txtPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPriority_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Priority:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Module:";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(9, 25);
            this.lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(48, 16);
            this.lbl1.TabIndex = 5;
            this.lbl1.Text = "Name:";
            // 
            // cboModule
            // 
            this.cboModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModule.FormattingEnabled = true;
            this.cboModule.Location = new System.Drawing.Point(140, 54);
            this.cboModule.Margin = new System.Windows.Forms.Padding(4);
            this.cboModule.MaxLength = 10;
            this.cboModule.Name = "cboModule";
            this.cboModule.Size = new System.Drawing.Size(215, 24);
            this.cboModule.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 25);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.MaxLength = 20;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(348, 22);
            this.txtName.TabIndex = 1;
            // 
            // cmdSave1
            // 
            this.cmdSave1.Location = new System.Drawing.Point(341, 404);
            this.cmdSave1.Name = "cmdSave1";
            this.cmdSave1.Size = new System.Drawing.Size(80, 30);
            this.cmdSave1.TabIndex = 7;
            this.cmdSave1.Text = "&Save";
            this.cmdSave1.UseVisualStyleBackColor = true;
            this.cmdSave1.Click += new System.EventHandler(this.cmdSave1_Click);
            // 
            // cmdClose1
            // 
            this.cmdClose1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose1.Location = new System.Drawing.Point(427, 404);
            this.cmdClose1.Name = "cmdClose1";
            this.cmdClose1.Size = new System.Drawing.Size(80, 30);
            this.cmdClose1.TabIndex = 8;
            this.cmdClose1.Text = "&Close";
            this.cmdClose1.UseVisualStyleBackColor = true;
            this.cmdClose1.Click += new System.EventHandler(this.cmdClose1_Click_1);
            // 
            // frmSwiftEditMdlAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose1;
            this.ClientSize = new System.Drawing.Size(514, 440);
            this.Controls.Add(this.cmdClose1);
            this.Controls.Add(this.cmdSave1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSwiftEditMdlAuto";
            this.Text = "Swift - Edit auto module and branch condition";
            this.Load += new System.EventHandler(this.frmSwiftEditMdlAuto_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftEditMdlAuto_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftEditMdlAuto_KeyDown);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdSave1, 0);
            this.Controls.SetChildIndex(this.cmdClose1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCriteria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.TextBox txtPriority;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cboModule;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button cmdSave1;
        private System.Windows.Forms.Button cmdClose1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;

    }
}