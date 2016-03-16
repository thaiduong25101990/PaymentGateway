namespace BR.BRSWIFT
{
    partial class frmSWIFTBankMap
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
            this.label6 = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.cboView = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdGetBranch = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtSwiftBankCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSIBSBankCode = new System.Windows.Forms.TextBox();
            this.txtTellerID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmdHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(746, 554);
            this.cmdClose.TabIndex = 16;
            this.cmdClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdClose_MouseMove);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.cmdClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdClose_MouseUp);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(585, 554);
            this.cmdSave.TabIndex = 14;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(502, 554);
            this.cmdDelete.TabIndex = 13;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(336, 554);
            this.cmdAdd.TabIndex = 10;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(419, 554);
            this.cmdEdit.TabIndex = 11;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(10, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Total number of banks : ";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.Blue;
            this.lblTong.Location = new System.Drawing.Point(160, 138);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(0, 16);
            this.lblTong.TabIndex = 4;
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.BackgroundColor = System.Drawing.Color.White;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Location = new System.Drawing.Point(6, 157);
            this.dgView.MultiSelect = false;
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(822, 382);
            this.dgView.TabIndex = 20;
            this.dgView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellClick);
            this.dgView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellEnter);
            // 
            // cboView
            // 
            this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboView.FormattingEnabled = true;
            this.cboView.Location = new System.Drawing.Point(64, 554);
            this.cboView.Name = "cboView";
            this.cboView.Size = new System.Drawing.Size(165, 21);
            this.cboView.TabIndex = 8;
            this.cboView.SelectedIndexChanged += new System.EventHandler(this.cboView_SelectedIndexChanged);
            this.cboView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 554);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "View :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdGetBranch);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.txtSwiftBankCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBankName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSIBSBankCode);
            this.groupBox1.Controls.Add(this.txtTellerID);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(822, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmdGetBranch
            // 
            this.cmdGetBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetBranch.Location = new System.Drawing.Point(724, 50);
            this.cmdGetBranch.Name = "cmdGetBranch";
            this.cmdGetBranch.Size = new System.Drawing.Size(84, 30);
            this.cmdGetBranch.TabIndex = 7;
            this.cmdGetBranch.Text = "Get &Branch";
            this.cmdGetBranch.UseVisualStyleBackColor = true;
            this.cmdGetBranch.Visible = false;
            this.cmdGetBranch.Click += new System.EventHandler(this.cmdGetBranch_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(724, 15);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(84, 30);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "Sea&rch";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtSwiftBankCode
            // 
            this.txtSwiftBankCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSwiftBankCode.Location = new System.Drawing.Point(123, 50);
            this.txtSwiftBankCode.MaxLength = 12;
            this.txtSwiftBankCode.Name = "txtSwiftBankCode";
            this.txtSwiftBankCode.Size = new System.Drawing.Size(166, 22);
            this.txtSwiftBankCode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "SIBS bank code :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(310, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 24;
            this.label5.Text = "Bank name :";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(123, 81);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(576, 22);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(327, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 27;
            this.label4.Text = "Teller ID :";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Description :";
            // 
            // txtBankName
            // 
            this.txtBankName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankName.Location = new System.Drawing.Point(409, 19);
            this.txtBankName.MaxLength = 255;
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(292, 22);
            this.txtBankName.TabIndex = 2;
            this.txtBankName.Leave += new System.EventHandler(this.txtBankName_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Swift bank code:";
            // 
            // txtSIBSBankCode
            // 
            this.txtSIBSBankCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSIBSBankCode.Location = new System.Drawing.Point(123, 19);
            this.txtSIBSBankCode.MaxLength = 5;
            this.txtSIBSBankCode.Name = "txtSIBSBankCode";
            this.txtSIBSBankCode.Size = new System.Drawing.Size(166, 22);
            this.txtSIBSBankCode.TabIndex = 1;
            this.txtSIBSBankCode.Leave += new System.EventHandler(this.txtSIBSBankCode_Leave);
            // 
            // txtTellerID
            // 
            this.txtTellerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTellerID.Location = new System.Drawing.Point(409, 50);
            this.txtTellerID.MaxLength = 255;
            this.txtTellerID.Name = "txtTellerID";
            this.txtTellerID.Size = new System.Drawing.Size(167, 22);
            this.txtTellerID.TabIndex = 4;
            this.txtTellerID.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(668, 554);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Ca&ncel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCancel_MouseMove);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCancel_MouseUp);
            // 
            // cmdHistory
            // 
            this.cmdHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHistory.Location = new System.Drawing.Point(253, 554);
            this.cmdHistory.Name = "cmdHistory";
            this.cmdHistory.Size = new System.Drawing.Size(80, 30);
            this.cmdHistory.TabIndex = 9;
            this.cmdHistory.Text = "&History";
            this.cmdHistory.UseVisualStyleBackColor = true;
            this.cmdHistory.Click += new System.EventHandler(this.cmdHistory_Click);
            // 
            // frmSWIFTBankMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(836, 597);
            this.Controls.Add(this.cmdHistory);
            this.Controls.Add(this.lblTong);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboView);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgView);
            this.Name = "frmSWIFTBankMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SWIFT Bank code";
            this.Load += new System.EventHandler(this.frmSWIFTBankMap_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSWIFTBankMap_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSWIFTBankMap_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.dgView, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.cboView, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.lblTong, 0);
            this.Controls.SetChildIndex(this.cmdHistory, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.ComboBox cboView;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSwiftBankCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSIBSBankCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtTellerID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button cmdHistory;
        private System.Windows.Forms.Button cmdGetBranch;
    }
}