namespace BR.BRSYSTEM
{
    partial class frmMSGDef
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMsgID = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCHK = new System.Windows.Forms.TextBox();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.txtFieldDescription = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.txtFieldCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGWPosition = new System.Windows.Forms.TextBox();
            this.txtSIBSPosition = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.cboMSGDefID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(873, 531);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(709, 531);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(626, 531);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(460, 531);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(543, 531);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMsgID);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCHK);
            this.groupBox2.Controls.Add(this.cboDataType);
            this.groupBox2.Controls.Add(this.txtFieldName);
            this.groupBox2.Controls.Add(this.txtFieldDescription);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtLength);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDefaultValue);
            this.groupBox2.Controls.Add(this.txtFieldCode);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtGWPosition);
            this.groupBox2.Controls.Add(this.txtSIBSPosition);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(941, 143);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // txtMsgID
            // 
            this.txtMsgID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMsgID.FormattingEnabled = true;
            this.txtMsgID.Location = new System.Drawing.Point(136, 20);
            this.txtMsgID.Margin = new System.Windows.Forms.Padding(4);
            this.txtMsgID.Name = "txtMsgID";
            this.txtMsgID.Size = new System.Drawing.Size(177, 24);
            this.txtMsgID.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 16);
            this.label11.TabIndex = 51;
            this.label11.Text = "Msg definition ID :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(338, 79);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 16);
            this.label10.TabIndex = 49;
            this.label10.Text = "CHK :";
            // 
            // txtCHK
            // 
            this.txtCHK.Location = new System.Drawing.Point(431, 78);
            this.txtCHK.Margin = new System.Windows.Forms.Padding(4);
            this.txtCHK.Name = "txtCHK";
            this.txtCHK.Size = new System.Drawing.Size(177, 22);
            this.txtCHK.TabIndex = 6;
            this.txtCHK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCHK_KeyPress);
            // 
            // cboDataType
            // 
            this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataType.FormattingEnabled = true;
            this.cboDataType.Location = new System.Drawing.Point(431, 46);
            this.cboDataType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(177, 24);
            this.cboDataType.TabIndex = 5;
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(136, 50);
            this.txtFieldName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFieldName.MaxLength = 15;
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(177, 22);
            this.txtFieldName.TabIndex = 1;
            // 
            // txtFieldDescription
            // 
            this.txtFieldDescription.Location = new System.Drawing.Point(136, 110);
            this.txtFieldDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtFieldDescription.Name = "txtFieldDescription";
            this.txtFieldDescription.Size = new System.Drawing.Size(472, 22);
            this.txtFieldDescription.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(639, 79);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 34;
            this.label9.Text = "Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Field description :";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(739, 76);
            this.txtLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtLength.MaxLength = 10;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(177, 22);
            this.txtLength.TabIndex = 9;
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLength_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "Field name :";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Location = new System.Drawing.Point(431, 17);
            this.txtDefaultValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(177, 22);
            this.txtDefaultValue.TabIndex = 4;
            // 
            // txtFieldCode
            // 
            this.txtFieldCode.Location = new System.Drawing.Point(136, 80);
            this.txtFieldCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtFieldCode.MaxLength = 6;
            this.txtFieldCode.Name = "txtFieldCode";
            this.txtFieldCode.Size = new System.Drawing.Size(177, 22);
            this.txtFieldCode.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(639, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 38;
            this.label7.Text = "BR Position:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(338, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "Default value :";
            // 
            // txtGWPosition
            // 
            this.txtGWPosition.Location = new System.Drawing.Point(739, 47);
            this.txtGWPosition.Margin = new System.Windows.Forms.Padding(4);
            this.txtGWPosition.MaxLength = 10;
            this.txtGWPosition.Name = "txtGWPosition";
            this.txtGWPosition.Size = new System.Drawing.Size(177, 22);
            this.txtGWPosition.TabIndex = 8;
            this.txtGWPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGWPosition_KeyPress);
            // 
            // txtSIBSPosition
            // 
            this.txtSIBSPosition.Location = new System.Drawing.Point(739, 17);
            this.txtSIBSPosition.Margin = new System.Windows.Forms.Padding(4);
            this.txtSIBSPosition.MaxLength = 10;
            this.txtSIBSPosition.Name = "txtSIBSPosition";
            this.txtSIBSPosition.Size = new System.Drawing.Size(177, 22);
            this.txtSIBSPosition.TabIndex = 7;
            this.txtSIBSPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSIBSPosition_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(639, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "Host Position:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "Field code :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(338, 50);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 36;
            this.label8.Text = "Data type :";
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdList.Location = new System.Drawing.Point(12, 157);
            this.grdList.Margin = new System.Windows.Forms.Padding(4);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersWidth = 30;
            this.grdList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(941, 366);
            this.grdList.TabIndex = 28;
            this.grdList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellEnter);
            // 
            // cboMSGDefID
            // 
            this.cboMSGDefID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMSGDefID.FormattingEnabled = true;
            this.cboMSGDefID.Location = new System.Drawing.Point(135, 532);
            this.cboMSGDefID.Margin = new System.Windows.Forms.Padding(4);
            this.cboMSGDefID.Name = "cboMSGDefID";
            this.cboMSGDefID.Size = new System.Drawing.Size(161, 24);
            this.cboMSGDefID.TabIndex = 7;
            this.cboMSGDefID.SelectedIndexChanged += new System.EventHandler(this.cboMSGDefID_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 536);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 52;
            this.label1.Text = "Msg Definition ID:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(791, 531);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmMSGDef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(962, 582);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cboMSGDefID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMSGDef";
            this.Text = "Host message field standard ";
            this.Load += new System.EventHandler(this.frmMSGDef_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMSGDef_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMSGDef_KeyDown);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grdList, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cboMSGDefID, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCHK;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.TextBox txtFieldDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.TextBox txtFieldCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGWPosition;
        private System.Windows.Forms.TextBox txtSIBSPosition;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.ComboBox cboMSGDefID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ComboBox txtMsgID;
    }
}