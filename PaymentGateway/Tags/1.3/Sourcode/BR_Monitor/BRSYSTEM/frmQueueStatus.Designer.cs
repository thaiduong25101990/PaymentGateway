namespace BR.BRSYSTEM
{
    partial class frmQueueStatus
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
            this.cbQueue = new System.Windows.Forms.ComboBox();
            this.lbqueue = new System.Windows.Forms.Label();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdRestart = new System.Windows.Forms.Button();
            this.datQueues = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datQueues)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(463, 399);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(325, 447);
            this.cmdSave.Text = "Save";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(239, 447);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(64, 447);
            this.cmdAdd.Text = "Add";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(150, 447);
            this.cmdEdit.Text = "Edit";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.cbQueue);
            this.groupBox1.Controls.Add(this.lbqueue);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Searching";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(457, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 30);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbQueue
            // 
            this.cbQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueue.FormattingEnabled = true;
            this.cbQueue.Location = new System.Drawing.Point(113, 18);
            this.cbQueue.Name = "cbQueue";
            this.cbQueue.Size = new System.Drawing.Size(243, 24);
            this.cbQueue.TabIndex = 0;
            // 
            // lbqueue
            // 
            this.lbqueue.AutoSize = true;
            this.lbqueue.Location = new System.Drawing.Point(20, 22);
            this.lbqueue.Name = "lbqueue";
            this.lbqueue.Size = new System.Drawing.Size(90, 16);
            this.lbqueue.TabIndex = 1;
            this.lbqueue.Text = "Queue name :";
            // 
            // cmdStart
            // 
            this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.Location = new System.Drawing.Point(381, 399);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(80, 30);
            this.cmdStart.TabIndex = 4;
            this.cmdStart.Text = "St&art";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStop.Location = new System.Drawing.Point(299, 399);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(80, 30);
            this.cmdStop.TabIndex = 3;
            this.cmdStop.Text = "St&op";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmdRestart
            // 
            this.cmdRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestart.Location = new System.Drawing.Point(217, 399);
            this.cmdRestart.Name = "cmdRestart";
            this.cmdRestart.Size = new System.Drawing.Size(80, 30);
            this.cmdRestart.TabIndex = 2;
            this.cmdRestart.Text = "R&estart";
            this.cmdRestart.UseVisualStyleBackColor = true;
            this.cmdRestart.Click += new System.EventHandler(this.button4_Click);
            // 
            // datQueues
            // 
            this.datQueues.AllowUserToAddRows = false;
            this.datQueues.AllowUserToDeleteRows = false;
            this.datQueues.AllowUserToResizeRows = false;
            this.datQueues.BackgroundColor = System.Drawing.Color.White;
            this.datQueues.ColumnHeadersHeight = 21;
            this.datQueues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.datQueues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4});
            this.datQueues.Location = new System.Drawing.Point(6, 65);
            this.datQueues.Name = "datQueues";
            this.datQueues.RowHeadersWidth = 30;
            this.datQueues.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.datQueues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datQueues.Size = new System.Drawing.Size(544, 328);
            this.datQueues.TabIndex = 4;
            this.datQueues.MouseMove += new System.Windows.Forms.MouseEventHandler(this.datQueues_MouseMove);
            this.datQueues.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datQueues_CellClick);
            this.datQueues.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.datQueues_CellEnter);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "             Queue Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "           Enqueue";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "        Dequeue";
            this.Column4.Name = "Column4";
            this.Column4.Width = 150;
            // 
            // frmQueueStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 436);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.datQueues);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdRestart);
            this.Name = "frmQueueStatus";
            this.Text = "Queue status";
            this.Load += new System.EventHandler(this.frmQueueStatus_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmQueueStatus_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmQueueStatus_KeyDown);
            this.Controls.SetChildIndex(this.cmdRestart, 0);
            this.Controls.SetChildIndex(this.cmdStop, 0);
            this.Controls.SetChildIndex(this.cmdStart, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.datQueues, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datQueues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.ComboBox cbQueue;
        private System.Windows.Forms.Label lbqueue;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdRestart;
        private System.Windows.Forms.DataGridView datQueues;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}