namespace BR.BRSYSTEM
{
    partial class frmArea
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtProvinceCode = new System.Windows.Forms.TextBox();
            this.txtShortname = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFilename = new System.Windows.Forms.Label();
            this.dgArea = new System.Windows.Forms.DataGridView();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgArea)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(545, 442);
            this.cmdClose.TabIndex = 12;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(372, 442);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(286, 442);
            this.cmdDelete.TabIndex = 9;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(114, 442);
            this.cmdAdd.TabIndex = 7;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(200, 442);
            this.cmdEdit.TabIndex = 8;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.txtProvinceCode);
            this.groupBox1.Controls.Add(this.txtShortname);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblFilename);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(130, 73);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(378, 24);
            this.cboType.TabIndex = 4;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(130, 45);
            this.txtFullName.MaxLength = 100;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(378, 23);
            this.txtFullName.TabIndex = 3;
            this.txtFullName.Leave += new System.EventHandler(this.txtFullName_Leave);
            // 
            // txtProvinceCode
            // 
            this.txtProvinceCode.Location = new System.Drawing.Point(130, 16);
            this.txtProvinceCode.MaxLength = 2;
            this.txtProvinceCode.Name = "txtProvinceCode";
            this.txtProvinceCode.Size = new System.Drawing.Size(112, 23);
            this.txtProvinceCode.TabIndex = 1;
            this.txtProvinceCode.Leave += new System.EventHandler(this.txtProvinceCode_Leave);
            this.txtProvinceCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProvinceCode_KeyPress);
            // 
            // txtShortname
            // 
            this.txtShortname.Location = new System.Drawing.Point(360, 16);
            this.txtShortname.MaxLength = 5;
            this.txtShortname.Name = "txtShortname";
            this.txtShortname.Size = new System.Drawing.Size(148, 23);
            this.txtShortname.TabIndex = 2;
            this.txtShortname.Leave += new System.EventHandler(this.txtShortname_Leave);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(526, 16);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 5;
            this.cmdSearch.Text = "Sea&rch";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Full name:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(268, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Short name:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Province code:";
            // 
            // lblFilename
            // 
            this.lblFilename.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.Location = new System.Drawing.Point(13, 77);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(61, 19);
            this.lblFilename.TabIndex = 8;
            this.lblFilename.Text = "Type:";
            // 
            // dgArea
            // 
            this.dgArea.AllowUserToAddRows = false;
            this.dgArea.AllowUserToDeleteRows = false;
            this.dgArea.AllowUserToResizeRows = false;
            this.dgArea.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgArea.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgArea.Location = new System.Drawing.Point(12, 112);
            this.dgArea.MultiSelect = false;
            this.dgArea.Name = "dgArea";
            this.dgArea.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgArea.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgArea.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgArea.Size = new System.Drawing.Size(626, 320);
            this.dgArea.TabIndex = 6;
            this.dgArea.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgArea_CellClick);
            this.dgArea.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgArea_CellEnter);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(459, 442);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 11;
            this.cmdCancel.Text = "Canc&el ";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // frmArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 480);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.dgArea);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmArea";
            this.Text = "IBPS Area";
            this.Load += new System.EventHandler(this.frmArea_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmArea_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmArea_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.dgArea, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.DataGridView dgArea;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtProvinceCode;
        private System.Windows.Forms.TextBox txtShortname;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button cmdCancel;
    }
}