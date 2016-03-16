namespace BR.BRIQS
{
    partial class frmIQSMsgCondNew
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
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.lbDirection = new System.Windows.Forms.Label();
            this.lbMsg_type = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(327, 101);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(241, 101);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMsg_type);
            this.groupBox1.Controls.Add(this.cbDirection);
            this.groupBox1.Controls.Add(this.lbDirection);
            this.groupBox1.Controls.Add(this.lbMsg_type);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 90);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg_type.Location = new System.Drawing.Point(111, 19);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(262, 23);
            this.txtMsg_type.TabIndex = 2;
            this.txtMsg_type.TextChanged += new System.EventHandler(this.txtMsg_type_TextChanged);
            // 
            // cbDirection
            // 
            this.cbDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Items.AddRange(new object[] {
            "TTSP",
            "VCB",
            "SWIFT"});
            this.cbDirection.Location = new System.Drawing.Point(111, 52);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(262, 24);
            this.cbDirection.TabIndex = 1;
            // 
            // lbDirection
            // 
            this.lbDirection.AutoSize = true;
            this.lbDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDirection.Location = new System.Drawing.Point(27, 55);
            this.lbDirection.Name = "lbDirection";
            this.lbDirection.Size = new System.Drawing.Size(67, 16);
            this.lbDirection.TabIndex = 0;
            this.lbDirection.Text = "Direction :";
            // 
            // lbMsg_type
            // 
            this.lbMsg_type.AutoSize = true;
            this.lbMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMsg_type.Location = new System.Drawing.Point(27, 19);
            this.lbMsg_type.Name = "lbMsg_type";
            this.lbMsg_type.Size = new System.Drawing.Size(68, 16);
            this.lbMsg_type.TabIndex = 0;
            this.lbMsg_type.Text = "Msg type :";
            // 
            // frmIQSMsgCondNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 138);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmIQSMsgCondNew";
            this.Text = "IQS Monitor";
            this.Load += new System.EventHandler(this.frmIQSMsgCondNew_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIQSMsgCondNew_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIQSMsgCondNew_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIQSMsgCondNew_KeyDown);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.ComboBox cbDirection;
        private System.Windows.Forms.Label lbDirection;
        private System.Windows.Forms.Label lbMsg_type;
    }
}