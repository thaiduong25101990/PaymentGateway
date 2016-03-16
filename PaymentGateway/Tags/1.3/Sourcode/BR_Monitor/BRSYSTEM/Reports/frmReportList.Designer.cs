namespace BR.BRSYSTEM
{
    partial class frmReportList
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
            this.cmdPermission = new System.Windows.Forms.Button();
            this.dgwRPT = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRPT)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(721, 499);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(722, 524);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(635, 499);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(460, 499);
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(546, 499);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdPermission
            // 
            this.cmdPermission.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPermission.Location = new System.Drawing.Point(12, 499);
            this.cmdPermission.Name = "cmdPermission";
            this.cmdPermission.Size = new System.Drawing.Size(101, 30);
            this.cmdPermission.TabIndex = 0;
            this.cmdPermission.Text = "&Permission";
            this.cmdPermission.UseVisualStyleBackColor = true;
            this.cmdPermission.Click += new System.EventHandler(this.cmdPermission_Click);
            // 
            // dgwRPT
            // 
            this.dgwRPT.AllowUserToAddRows = false;
            this.dgwRPT.AllowUserToDeleteRows = false;
            this.dgwRPT.AllowUserToResizeRows = false;
            this.dgwRPT.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwRPT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwRPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwRPT.Location = new System.Drawing.Point(7, 6);
            this.dgwRPT.Name = "dgwRPT";
            this.dgwRPT.ReadOnly = true;
            this.dgwRPT.RowHeadersVisible = false;
            this.dgwRPT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwRPT.Size = new System.Drawing.Size(799, 487);
            this.dgwRPT.TabIndex = 100;
            this.dgwRPT.TabStop = false;
            this.dgwRPT.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwRPT_CellClick);
            // 
            // frmReportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 535);
            this.Controls.Add(this.dgwRPT);
            this.Controls.Add(this.cmdPermission);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReportList";
            this.Text = "Register report";
            this.Load += new System.EventHandler(this.frmReportList_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmReportList_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportList_KeyDown);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdPermission, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.dgwRPT, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgwRPT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdPermission;
        private System.Windows.Forms.DataGridView dgwRPT;
    }
}