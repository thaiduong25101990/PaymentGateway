namespace BR.BRSYSTEM
{
    partial class frmMSGList
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
            this.grdList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMsgDescription = new System.Windows.Forms.TextBox();
            this.txtSIBSMsgLength = new System.Windows.Forms.TextBox();
            this.txtGWMsgLength = new System.Windows.Forms.TextBox();
            this.txtMsgName = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(766, 497);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(597, 498);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(514, 498);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(348, 498);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(431, 498);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdList.ColumnHeadersHeight = 22;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdList.Location = new System.Drawing.Point(13, 124);
            this.grdList.Margin = new System.Windows.Forms.Padding(4);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersWidth = 30;
            this.grdList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(831, 365);
            this.grdList.TabIndex = 7;
            this.grdList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellEnter);
            this.grdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMsgDescription);
            this.groupBox1.Controls.Add(this.txtSIBSMsgLength);
            this.groupBox1.Controls.Add(this.txtGWMsgLength);
            this.groupBox1.Controls.Add(this.txtMsgName);
            this.groupBox1.Location = new System.Drawing.Point(13, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(831, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 83);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 16);
            this.label5.TabIndex = 129;
            this.label5.Text = "Message description :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 16);
            this.label4.TabIndex = 128;
            this.label4.Text = "Host message length :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 16);
            this.label2.TabIndex = 127;
            this.label2.Text = "BR message length :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 126;
            this.label1.Text = "Message name :";
            // 
            // txtMsgDescription
            // 
            this.txtMsgDescription.Location = new System.Drawing.Point(172, 79);
            this.txtMsgDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtMsgDescription.Name = "txtMsgDescription";
            this.txtMsgDescription.Size = new System.Drawing.Size(635, 22);
            this.txtMsgDescription.TabIndex = 2;
            // 
            // txtSIBSMsgLength
            // 
            this.txtSIBSMsgLength.Location = new System.Drawing.Point(172, 51);
            this.txtSIBSMsgLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtSIBSMsgLength.MaxLength = 5;
            this.txtSIBSMsgLength.Name = "txtSIBSMsgLength";
            this.txtSIBSMsgLength.Size = new System.Drawing.Size(219, 22);
            this.txtSIBSMsgLength.TabIndex = 1;
            this.txtSIBSMsgLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSIBSMsgLength_KeyPress);
            // 
            // txtGWMsgLength
            // 
            this.txtGWMsgLength.Location = new System.Drawing.Point(587, 49);
            this.txtGWMsgLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtGWMsgLength.MaxLength = 5;
            this.txtGWMsgLength.Name = "txtGWMsgLength";
            this.txtGWMsgLength.Size = new System.Drawing.Size(220, 22);
            this.txtGWMsgLength.TabIndex = 3;
            this.txtGWMsgLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGWMsgLength_KeyPress);
            // 
            // txtMsgName
            // 
            this.txtMsgName.Location = new System.Drawing.Point(172, 22);
            this.txtMsgName.Margin = new System.Windows.Forms.Padding(4);
            this.txtMsgName.Name = "txtMsgName";
            this.txtMsgName.Size = new System.Drawing.Size(219, 22);
            this.txtMsgName.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(683, 498);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmMSGList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(858, 541);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMSGList";
            this.Text = "Host message length standard ";
            this.Load += new System.EventHandler(this.frmMSGList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMSGList_KeyDown);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.grdList, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMsgDescription;
        private System.Windows.Forms.TextBox txtSIBSMsgLength;
        private System.Windows.Forms.TextBox txtGWMsgLength;
        private System.Windows.Forms.TextBox txtMsgName;
        private System.Windows.Forms.Button cmdCancel;
    }
}