namespace BR.BRSYSTEM
{
    partial class frmJobLog
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
            this.lbFrom_date = new System.Windows.Forms.Label();
            this.lbTo_date = new System.Windows.Forms.Label();
            this.lbMessage_ID = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lbJob_name = new System.Windows.Forms.Label();
            this.cmdSeach = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.cboGwtype = new System.Windows.Forms.ComboBox();
            this.cbJob_name = new System.Windows.Forms.ComboBox();
            this.date_to = new System.Windows.Forms.DateTimePicker();
            this.date_from = new System.Windows.Forms.DateTimePicker();
            this.dtg_MSG_LOG = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_MSG_LOG)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(644, 455);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(578, 587);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(492, 587);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(317, 587);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(403, 587);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbFrom_date);
            this.groupBox1.Controls.Add(this.lbTo_date);
            this.groupBox1.Controls.Add(this.lbMessage_ID);
            this.groupBox1.Controls.Add(this.lblChannel);
            this.groupBox1.Controls.Add(this.lbJob_name);
            this.groupBox1.Controls.Add(this.cmdSeach);
            this.groupBox1.Controls.Add(this.txtMessage);
            this.groupBox1.Controls.Add(this.cboGwtype);
            this.groupBox1.Controls.Add(this.cbJob_name);
            this.groupBox1.Controls.Add(this.date_to);
            this.groupBox1.Controls.Add(this.date_from);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(718, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // lbFrom_date
            // 
            this.lbFrom_date.AutoSize = true;
            this.lbFrom_date.Location = new System.Drawing.Point(21, 77);
            this.lbFrom_date.Name = "lbFrom_date";
            this.lbFrom_date.Size = new System.Drawing.Size(76, 16);
            this.lbFrom_date.TabIndex = 6;
            this.lbFrom_date.Text = "From date :";
            // 
            // lbTo_date
            // 
            this.lbTo_date.AutoSize = true;
            this.lbTo_date.Location = new System.Drawing.Point(291, 77);
            this.lbTo_date.Name = "lbTo_date";
            this.lbTo_date.Size = new System.Drawing.Size(52, 16);
            this.lbTo_date.TabIndex = 5;
            this.lbTo_date.Text = "To date";
            // 
            // lbMessage_ID
            // 
            this.lbMessage_ID.AutoSize = true;
            this.lbMessage_ID.Location = new System.Drawing.Point(291, 48);
            this.lbMessage_ID.Name = "lbMessage_ID";
            this.lbMessage_ID.Size = new System.Drawing.Size(83, 16);
            this.lbMessage_ID.TabIndex = 5;
            this.lbMessage_ID.Text = "Message ID :";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(21, 19);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(56, 16);
            this.lblChannel.TabIndex = 5;
            this.lblChannel.Text = "Chanel :";
            // 
            // lbJob_name
            // 
            this.lbJob_name.AutoSize = true;
            this.lbJob_name.Location = new System.Drawing.Point(21, 48);
            this.lbJob_name.Name = "lbJob_name";
            this.lbJob_name.Size = new System.Drawing.Size(72, 16);
            this.lbJob_name.TabIndex = 5;
            this.lbJob_name.Text = "Job name :";
            // 
            // cmdSeach
            // 
            this.cmdSeach.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSeach.Location = new System.Drawing.Point(632, 18);
            this.cmdSeach.Name = "cmdSeach";
            this.cmdSeach.Size = new System.Drawing.Size(80, 30);
            this.cmdSeach.TabIndex = 5;
            this.cmdSeach.Text = "Search";
            this.cmdSeach.UseVisualStyleBackColor = true;
            this.cmdSeach.Click += new System.EventHandler(this.cmdSeach_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(381, 45);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(161, 23);
            this.txtMessage.TabIndex = 3;
            // 
            // cboGwtype
            // 
            this.cboGwtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGwtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGwtype.FormattingEnabled = true;
            this.cboGwtype.Location = new System.Drawing.Point(100, 16);
            this.cboGwtype.Name = "cboGwtype";
            this.cboGwtype.Size = new System.Drawing.Size(161, 24);
            this.cboGwtype.TabIndex = 1;
            // 
            // cbJob_name
            // 
            this.cbJob_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJob_name.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbJob_name.FormattingEnabled = true;
            this.cbJob_name.Location = new System.Drawing.Point(100, 44);
            this.cbJob_name.Name = "cbJob_name";
            this.cbJob_name.Size = new System.Drawing.Size(161, 24);
            this.cbJob_name.TabIndex = 2;
            // 
            // date_to
            // 
            this.date_to.CustomFormat = "dd/MM/yyyy";
            this.date_to.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_to.Location = new System.Drawing.Point(381, 74);
            this.date_to.Name = "date_to";
            this.date_to.Size = new System.Drawing.Size(161, 23);
            this.date_to.TabIndex = 5;
            this.date_to.ValueChanged += new System.EventHandler(this.date_to_ValueChanged);
            // 
            // date_from
            // 
            this.date_from.CustomFormat = "dd/MM/yyyy";
            this.date_from.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_from.Location = new System.Drawing.Point(100, 74);
            this.date_from.Name = "date_from";
            this.date_from.Size = new System.Drawing.Size(161, 23);
            this.date_from.TabIndex = 4;
            this.date_from.ValueChanged += new System.EventHandler(this.date_from_ValueChanged);
            // 
            // dtg_MSG_LOG
            // 
            this.dtg_MSG_LOG.AllowUserToAddRows = false;
            this.dtg_MSG_LOG.AllowUserToDeleteRows = false;
            this.dtg_MSG_LOG.AllowUserToResizeRows = false;
            this.dtg_MSG_LOG.BackgroundColor = System.Drawing.Color.White;
            this.dtg_MSG_LOG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtg_MSG_LOG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dtg_MSG_LOG.Location = new System.Drawing.Point(6, 123);
            this.dtg_MSG_LOG.Name = "dtg_MSG_LOG";
            this.dtg_MSG_LOG.RowHeadersWidth = 30;
            this.dtg_MSG_LOG.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtg_MSG_LOG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtg_MSG_LOG.Size = new System.Drawing.Size(718, 326);
            this.dtg_MSG_LOG.TabIndex = 6;
            this.dtg_MSG_LOG.TabStop = false;
            this.dtg_MSG_LOG.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtg_MSG_LOG_MouseMove);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "             Job name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 160;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "              Date time";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "         Status";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "                                 Content";
            this.Column4.Name = "Column4";
            this.Column4.Width = 300;
            // 
            // frmJobLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 490);
            this.Controls.Add(this.dtg_MSG_LOG);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmJobLog";
            this.Text = "Job Information";
            this.Load += new System.EventHandler(this.frmJobLog_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmJobLog_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJobLog_KeyDown);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.dtg_MSG_LOG, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_MSG_LOG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSeach;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ComboBox cbJob_name;
        private System.Windows.Forms.DateTimePicker date_to;
        private System.Windows.Forms.DateTimePicker date_from;
        private System.Windows.Forms.Label lbJob_name;
        private System.Windows.Forms.Label lbFrom_date;
        private System.Windows.Forms.Label lbTo_date;
        private System.Windows.Forms.Label lbMessage_ID;
        private System.Windows.Forms.DataGridView dtg_MSG_LOG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.ComboBox cboGwtype;
    }
}