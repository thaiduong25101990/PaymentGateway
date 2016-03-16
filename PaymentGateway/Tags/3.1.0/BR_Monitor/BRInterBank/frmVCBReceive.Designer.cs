namespace BR.BRInterBank
{
    partial class frmVCBReceive
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
            this.cbChennal = new System.Windows.Forms.ComboBox();
            this.txtBankcode = new System.Windows.Forms.TextBox();
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.cbbModule = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataSearch = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPARTMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BANK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GWTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(433, 478);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(268, 478);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(185, 478);
            this.cmdDelete.TabIndex = 6;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(20, 478);
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(102, 478);
            this.cmdEdit.TabIndex = 5;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbChennal);
            this.groupBox1.Controls.Add(this.txtBankcode);
            this.groupBox1.Controls.Add(this.txtMsg_type);
            this.groupBox1.Controls.Add(this.cbbModule);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cbChennal
            // 
            this.cbChennal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChennal.FormattingEnabled = true;
            this.cbChennal.Location = new System.Drawing.Point(337, 18);
            this.cbChennal.Name = "cbChennal";
            this.cbChennal.Size = new System.Drawing.Size(153, 24);
            this.cbChennal.TabIndex = 2;
            // 
            // txtBankcode
            // 
            this.txtBankcode.Location = new System.Drawing.Point(337, 50);
            this.txtBankcode.Name = "txtBankcode";
            this.txtBankcode.Size = new System.Drawing.Size(153, 22);
            this.txtBankcode.TabIndex = 4;
            this.txtBankcode.TextChanged += new System.EventHandler(this.txtBankcode_TextChanged);
            this.txtBankcode.Leave += new System.EventHandler(this.txtBankcode_Leave);
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Location = new System.Drawing.Point(84, 50);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(153, 22);
            this.txtMsg_type.TabIndex = 3;
            this.txtMsg_type.TextChanged += new System.EventHandler(this.txtMsg_type_TextChanged);
            // 
            // cbbModule
            // 
            this.cbbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbModule.FormattingEnabled = true;
            this.cbbModule.Location = new System.Drawing.Point(84, 18);
            this.cbbModule.Name = "cbbModule";
            this.cbbModule.Size = new System.Drawing.Size(153, 24);
            this.cbbModule.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bank code :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Msg type :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Channel :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataSearch);
            this.groupBox2.Location = new System.Drawing.Point(4, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 380);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // dataSearch
            // 
            this.dataSearch.AllowUserToAddRows = false;
            this.dataSearch.AllowUserToResizeRows = false;
            this.dataSearch.BackgroundColor = System.Drawing.Color.White;
            this.dataSearch.ColumnHeadersHeight = 21;
            this.dataSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.DEPARTMENT,
            this.MSG_TYPE,
            this.BANK_CODE,
            this.GWTYPE});
            this.dataSearch.Location = new System.Drawing.Point(6, 19);
            this.dataSearch.Name = "dataSearch";
            this.dataSearch.RowHeadersWidth = 30;
            this.dataSearch.Size = new System.Drawing.Size(512, 343);
            this.dataSearch.TabIndex = 10;
            this.dataSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataSearch.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // ID
            // 
            this.ID.HeaderText = " ID";
            this.ID.Name = "ID";
            this.ID.Width = 30;
            // 
            // DEPARTMENT
            // 
            this.DEPARTMENT.HeaderText = "MODULE";
            this.DEPARTMENT.Name = "DEPARTMENT";
            // 
            // MSG_TYPE
            // 
            this.MSG_TYPE.HeaderText = "MSG TYPE";
            this.MSG_TYPE.Name = "MSG_TYPE";
            // 
            // BANK_CODE
            // 
            this.BANK_CODE.HeaderText = "BANK CODE";
            this.BANK_CODE.Name = "BANK_CODE";
            this.BANK_CODE.Width = 130;
            // 
            // GWTYPE
            // 
            this.GWTYPE.HeaderText = "CHANNEL";
            this.GWTYPE.Name = "GWTYPE";
            this.GWTYPE.Width = 120;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(351, 478);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmVCBReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 515);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmVCBReceive";
            this.Text = "MSG Parameters";
            this.Load += new System.EventHandler(this.frmVCBReceive_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmVCBReceive_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVCBReceive_KeyDown);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBankcode;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.ComboBox cbbModule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ComboBox cbChennal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPARTMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BANK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GWTYPE;
    }
}