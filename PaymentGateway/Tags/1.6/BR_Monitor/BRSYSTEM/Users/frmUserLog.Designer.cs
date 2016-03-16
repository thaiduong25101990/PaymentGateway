namespace BR.BRSYSTEM
{
    partial class frmUserLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserLog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.lbTo_date = new System.Windows.Forms.Label();
            this.date_to = new System.Windows.Forms.DateTimePicker();
            this.date_from = new System.Windows.Forms.DateTimePicker();
            this.cbUser_name = new System.Windows.Forms.ComboBox();
            this.lbfrom_date = new System.Windows.Forms.Label();
            this.lbUser_name = new System.Windows.Forms.Label();
            this.datUser_logMsg = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datUser_logMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(793, 508);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(520, 282);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(434, 282);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(259, 282);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(345, 282);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFormName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.lbTo_date);
            this.groupBox1.Controls.Add(this.date_to);
            this.groupBox1.Controls.Add(this.date_from);
            this.groupBox1.Controls.Add(this.cbUser_name);
            this.groupBox1.Controls.Add(this.lbfrom_date);
            this.groupBox1.Controls.Add(this.lbUser_name);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(881, 125);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(123, 90);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(440, 23);
            this.txtFormName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Form name :";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(781, 25);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // lbTo_date
            // 
            this.lbTo_date.AutoSize = true;
            this.lbTo_date.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTo_date.Location = new System.Drawing.Point(327, 33);
            this.lbTo_date.Name = "lbTo_date";
            this.lbTo_date.Size = new System.Drawing.Size(61, 16);
            this.lbTo_date.TabIndex = 3;
            this.lbTo_date.Text = "To date :";
            // 
            // date_to
            // 
            this.date_to.CustomFormat = "dd/MM/yyyy";
            this.date_to.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_to.Location = new System.Drawing.Point(394, 30);
            this.date_to.Name = "date_to";
            this.date_to.Size = new System.Drawing.Size(169, 23);
            this.date_to.TabIndex = 2;
            this.date_to.ValueChanged += new System.EventHandler(this.date_to_ValueChanged);
            // 
            // date_from
            // 
            this.date_from.CustomFormat = "dd/MM/yyyy";
            this.date_from.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_from.Location = new System.Drawing.Point(123, 30);
            this.date_from.Name = "date_from";
            this.date_from.Size = new System.Drawing.Size(182, 23);
            this.date_from.TabIndex = 1;
            this.date_from.ValueChanged += new System.EventHandler(this.date_from_ValueChanged);
            // 
            // cbUser_name
            // 
            this.cbUser_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUser_name.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUser_name.FormattingEnabled = true;
            this.cbUser_name.Location = new System.Drawing.Point(123, 59);
            this.cbUser_name.Name = "cbUser_name";
            this.cbUser_name.Size = new System.Drawing.Size(440, 24);
            this.cbUser_name.TabIndex = 0;
            // 
            // lbfrom_date
            // 
            this.lbfrom_date.AutoSize = true;
            this.lbfrom_date.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbfrom_date.Location = new System.Drawing.Point(24, 32);
            this.lbfrom_date.Name = "lbfrom_date";
            this.lbfrom_date.Size = new System.Drawing.Size(76, 16);
            this.lbfrom_date.TabIndex = 0;
            this.lbfrom_date.Text = "From date :";
            // 
            // lbUser_name
            // 
            this.lbUser_name.AutoSize = true;
            this.lbUser_name.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUser_name.Location = new System.Drawing.Point(23, 63);
            this.lbUser_name.Name = "lbUser_name";
            this.lbUser_name.Size = new System.Drawing.Size(79, 16);
            this.lbUser_name.TabIndex = 0;
            this.lbUser_name.Text = "User name :";
            // 
            // datUser_logMsg
            // 
            this.datUser_logMsg.AllowUserToAddRows = false;
            this.datUser_logMsg.AllowUserToDeleteRows = false;
            this.datUser_logMsg.AllowUserToResizeRows = false;
            this.datUser_logMsg.BackgroundColor = System.Drawing.Color.White;
            this.datUser_logMsg.ColumnHeadersHeight = 21;
            this.datUser_logMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.datUser_logMsg.Location = new System.Drawing.Point(12, 134);
            this.datUser_logMsg.Name = "datUser_logMsg";
            this.datUser_logMsg.ReadOnly = true;
            this.datUser_logMsg.Size = new System.Drawing.Size(881, 360);
            this.datUser_logMsg.TabIndex = 4;
            this.datUser_logMsg.TabStop = false;
            // 
            // frmUserLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 550);
            this.Controls.Add(this.datUser_logMsg);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserLog";
            this.Text = "User log information";
            this.Load += new System.EventHandler(this.frmUserLog_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmUserLog_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmUserLog_KeyDown);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.datUser_logMsg, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datUser_logMsg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTo_date;
        private System.Windows.Forms.DateTimePicker date_from;
        private System.Windows.Forms.ComboBox cbUser_name;
        private System.Windows.Forms.Label lbfrom_date;
        private System.Windows.Forms.Label lbUser_name;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.DateTimePicker date_to;
        private System.Windows.Forms.DataGridView datUser_logMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFormName;
    }
}