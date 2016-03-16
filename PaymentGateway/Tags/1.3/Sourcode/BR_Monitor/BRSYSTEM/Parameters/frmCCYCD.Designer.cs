namespace BR.BRSYSTEM
{
    partial class frmCCYCD
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
            this.txtCcyName = new System.Windows.Forms.TextBox();
            this.txtMonCode = new System.Windows.Forms.TextBox();
            this.txtDecimal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblShortCD = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtCurrencyCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgdCurrency = new System.Windows.Forms.DataGridView();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCurrency)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(631, 400);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(459, 400);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(376, 400);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(204, 400);
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(290, 400);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCcyName);
            this.groupBox1.Controls.Add(this.txtMonCode);
            this.groupBox1.Controls.Add(this.txtDecimal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblShortCD);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.txtCurrencyCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // txtCcyName
            // 
            this.txtCcyName.Location = new System.Drawing.Point(419, 53);
            this.txtCcyName.MaxLength = 50;
            this.txtCcyName.Name = "txtCcyName";
            this.txtCcyName.Size = new System.Drawing.Size(172, 22);
            this.txtCcyName.TabIndex = 3;
            this.txtCcyName.Leave += new System.EventHandler(this.txtCcyName_Leave);
            // 
            // txtMonCode
            // 
            this.txtMonCode.AcceptsTab = true;
            this.txtMonCode.Location = new System.Drawing.Point(128, 53);
            this.txtMonCode.MaxLength = 3;
            this.txtMonCode.Name = "txtMonCode";
            this.txtMonCode.Size = new System.Drawing.Size(172, 22);
            this.txtMonCode.TabIndex = 1;
            this.txtMonCode.Leave += new System.EventHandler(this.txtMonCode_Leave);
            // 
            // txtDecimal
            // 
            this.txtDecimal.Location = new System.Drawing.Point(419, 26);
            this.txtDecimal.MaxLength = 1;
            this.txtDecimal.Name = "txtDecimal";
            this.txtDecimal.Size = new System.Drawing.Size(172, 22);
            this.txtDecimal.TabIndex = 2;
            this.txtDecimal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDecimal_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Currency name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Decimal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Currency code:";
            // 
            // lblShortCD
            // 
            this.lblShortCD.AutoSize = true;
            this.lblShortCD.Location = new System.Drawing.Point(232, 32);
            this.lblShortCD.Name = "lblShortCD";
            this.lblShortCD.Size = new System.Drawing.Size(0, 16);
            this.lblShortCD.TabIndex = 4;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(623, 26);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 4;
            this.cmdSearch.Text = "Sea&rch";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtCurrencyCode
            // 
            this.txtCurrencyCode.Location = new System.Drawing.Point(128, 26);
            this.txtCurrencyCode.MaxLength = 2;
            this.txtCurrencyCode.Name = "txtCurrencyCode";
            this.txtCurrencyCode.Size = new System.Drawing.Size(172, 22);
            this.txtCurrencyCode.TabIndex = 0;
            this.txtCurrencyCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrencyCode_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Currency number:";
            // 
            // dgdCurrency
            // 
            this.dgdCurrency.AllowUserToAddRows = false;
            this.dgdCurrency.AllowUserToDeleteRows = false;
            this.dgdCurrency.AllowUserToResizeRows = false;
            this.dgdCurrency.BackgroundColor = System.Drawing.Color.White;
            this.dgdCurrency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdCurrency.Location = new System.Drawing.Point(6, 95);
            this.dgdCurrency.Name = "dgdCurrency";
            this.dgdCurrency.ReadOnly = true;
            this.dgdCurrency.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgdCurrency.Size = new System.Drawing.Size(721, 295);
            this.dgdCurrency.TabIndex = 20;
            this.dgdCurrency.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgdCurrency_MouseMove);
            this.dgdCurrency.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdCurrency_CellEnter);
            this.dgdCurrency.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdCurrency_CellContentClick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(545, 400);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmCCYCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(733, 438);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.dgdCurrency);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCCYCD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IBPS Currency management";
            this.Load += new System.EventHandler(this.frmCCYCD_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCCYCD_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCCYCD_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCCYCD_KeyDown);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.dgdCurrency, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCurrency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtCurrencyCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblShortCD;
        private System.Windows.Forms.DataGridView dgdCurrency;
        private System.Windows.Forms.TextBox txtCcyName;
        private System.Windows.Forms.TextBox txtMonCode;
        private System.Windows.Forms.TextBox txtDecimal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdCancel;
    }
}