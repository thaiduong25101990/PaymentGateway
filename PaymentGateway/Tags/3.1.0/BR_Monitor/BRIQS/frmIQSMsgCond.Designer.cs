namespace BR.BRIQS
{
    partial class frmIQSMsgCond
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
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtMsg_type = new System.Windows.Forms.TextBox();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.lbDirection = new System.Windows.Forms.Label();
            this.lbMsg_type = new System.Windows.Forms.Label();
            this.datIQS_CONDITION = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datIQS_CONDITION)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(470, 380);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(387, 380);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Location = new System.Drawing.Point(304, 380);
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdd.Location = new System.Drawing.Point(220, 380);
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(359, 509);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.txtMsg_type);
            this.groupBox1.Controls.Add(this.cbDirection);
            this.groupBox1.Controls.Add(this.lbDirection);
            this.groupBox1.Controls.Add(this.lbMsg_type);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 76);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(439, 22);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMsg_type
            // 
            this.txtMsg_type.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg_type.Location = new System.Drawing.Point(124, 16);
            this.txtMsg_type.Name = "txtMsg_type";
            this.txtMsg_type.Size = new System.Drawing.Size(205, 23);
            this.txtMsg_type.TabIndex = 2;
            // 
            // cbDirection
            // 
            this.cbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Items.AddRange(new object[] {
            "ALL",
            "TTSP",
            "VCB",
            "SWIFT"});
            this.cbDirection.Location = new System.Drawing.Point(124, 44);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(205, 24);
            this.cbDirection.TabIndex = 1;
            // 
            // lbDirection
            // 
            this.lbDirection.AutoSize = true;
            this.lbDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDirection.Location = new System.Drawing.Point(27, 48);
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
            // datIQS_CONDITION
            // 
            this.datIQS_CONDITION.AllowUserToAddRows = false;
            this.datIQS_CONDITION.BackgroundColor = System.Drawing.Color.White;
            this.datIQS_CONDITION.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datIQS_CONDITION.Location = new System.Drawing.Point(8, 81);
            this.datIQS_CONDITION.Name = "datIQS_CONDITION";
            this.datIQS_CONDITION.ReadOnly = true;
            this.datIQS_CONDITION.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datIQS_CONDITION.Size = new System.Drawing.Size(540, 294);
            this.datIQS_CONDITION.TabIndex = 3;
            this.datIQS_CONDITION.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datIQS_CONDITION_CellClick);
            this.datIQS_CONDITION.Click += new System.EventHandler(this.datIQS_CONDITION_Click);
            // 
            // frmIQSMsgCond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 415);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.datIQS_CONDITION);
            this.Name = "frmIQSMsgCond";
            this.Text = "IQS monitor";
            this.Load += new System.EventHandler(this.frmIQSMsgCond_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIQSMsgCond_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIQSMsgCond_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIQSMsgCond_KeyDown);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.datIQS_CONDITION, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datIQS_CONDITION)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMsg_type;
        private System.Windows.Forms.ComboBox cbDirection;
        private System.Windows.Forms.Label lbMsg_type;
        private System.Windows.Forms.Label lbDirection;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.DataGridView datIQS_CONDITION;
    }
}

