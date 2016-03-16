namespace BR.BRSWIFT
{
    partial class frmSwiftStatement
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
            this.txtTellerID = new System.Windows.Forms.TextBox();
            this.txtStatement = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.pickerTo = new System.Windows.Forms.DateTimePicker();
            this.pickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRPTType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdReprint = new System.Windows.Forms.Button();
            this.dgvSwift = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSwift)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(629, 491);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(307, 341);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(221, 341);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(46, 341);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(132, 341);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTellerID);
            this.groupBox1.Controls.Add(this.txtStatement);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.pickerTo);
            this.groupBox1.Controls.Add(this.pickerFrom);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(734, 91);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching condition";
            // 
            // txtTellerID
            // 
            this.txtTellerID.Location = new System.Drawing.Point(416, 54);
            this.txtTellerID.MaxLength = 8;
            this.txtTellerID.Name = "txtTellerID";
            this.txtTellerID.Size = new System.Drawing.Size(174, 23);
            this.txtTellerID.TabIndex = 3;
            // 
            // txtStatement
            // 
            this.txtStatement.Location = new System.Drawing.Point(136, 57);
            this.txtStatement.MaxLength = 10;
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.Size = new System.Drawing.Size(165, 23);
            this.txtStatement.TabIndex = 2;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(624, 17);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 4;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // pickerTo
            // 
            this.pickerTo.CustomFormat = "dd/MM/yyyy";
            this.pickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerTo.Location = new System.Drawing.Point(416, 21);
            this.pickerTo.Name = "pickerTo";
            this.pickerTo.Size = new System.Drawing.Size(173, 23);
            this.pickerTo.TabIndex = 1;
            this.pickerTo.ValueChanged += new System.EventHandler(this.pickerTo_ValueChanged);
            // 
            // pickerFrom
            // 
            this.pickerFrom.CustomFormat = "dd/MM/yyyy";
            this.pickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerFrom.Location = new System.Drawing.Point(136, 24);
            this.pickerFrom.Name = "pickerFrom";
            this.pickerFrom.Size = new System.Drawing.Size(165, 23);
            this.pickerFrom.TabIndex = 0;
            this.pickerFrom.ValueChanged += new System.EventHandler(this.pickerFrom_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(338, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Teller ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(338, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "To date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Statement No :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From date :";
            // 
            // cboRPTType
            // 
            this.cboRPTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRPTType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRPTType.FormattingEnabled = true;
            this.cboRPTType.Location = new System.Drawing.Point(136, 471);
            this.cboRPTType.Name = "cboRPTType";
            this.cboRPTType.Size = new System.Drawing.Size(170, 24);
            this.cboRPTType.TabIndex = 6;
            this.cboRPTType.SelectedIndexChanged += new System.EventHandler(this.cboRPTType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(38, 474);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Report type :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(12, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Total number of messages :";
            this.label5.Visible = false;
            // 
            // cmdReprint
            // 
            this.cmdReprint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReprint.Location = new System.Drawing.Point(543, 491);
            this.cmdReprint.Name = "cmdReprint";
            this.cmdReprint.Size = new System.Drawing.Size(80, 30);
            this.cmdReprint.TabIndex = 5;
            this.cmdReprint.Text = "&Reprint";
            this.cmdReprint.UseVisualStyleBackColor = true;
            this.cmdReprint.Click += new System.EventHandler(this.cmdReprint_Click);
            // 
            // dgvSwift
            // 
            this.dgvSwift.AllowUserToAddRows = false;
            this.dgvSwift.AllowUserToDeleteRows = false;
            this.dgvSwift.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSwift.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSwift.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSwift.Location = new System.Drawing.Point(6, 10);
            this.dgvSwift.Name = "dgvSwift";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSwift.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSwift.RowHeadersVisible = false;
            this.dgvSwift.Size = new System.Drawing.Size(722, 343);
            this.dgvSwift.TabIndex = 100;
            this.dgvSwift.TabStop = false;
            this.dgvSwift.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSwift_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSwift);
            this.groupBox2.Location = new System.Drawing.Point(5, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(734, 359);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.ForeColor = System.Drawing.Color.Blue;
            this.lblNumber.Location = new System.Drawing.Point(188, 111);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(0, 13);
            this.lblNumber.TabIndex = 8;
            // 
            // frmSwiftStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 528);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboRPTType);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdReprint);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSwiftStatement";
            this.Text = "Swift manual statement management";
            this.Load += new System.EventHandler(this.frmSwiftStatement_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftStatement_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftStatement_KeyDown);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdReprint, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.lblNumber, 0);
            this.Controls.SetChildIndex(this.cboRPTType, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSwift)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdReprint;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.DataGridView dgvSwift;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker pickerFrom;
        private System.Windows.Forms.DateTimePicker pickerTo;
        private System.Windows.Forms.TextBox txtTellerID;
        private System.Windows.Forms.TextBox txtStatement;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.ComboBox cboRPTType;
        private System.Windows.Forms.Label label6;
    }
}