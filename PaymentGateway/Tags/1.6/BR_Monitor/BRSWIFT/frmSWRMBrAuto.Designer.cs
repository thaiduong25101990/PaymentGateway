namespace BR.BRSWIFT
{
    partial class frmSWRMBrAuto
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
            this.grdView = new System.Windows.Forms.DataGridView();
            this.ORG_BRAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIVER_BRAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.cboBranchDes = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBranchReceiver = new System.Windows.Forms.Label();
            this.lblBranchOri = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cmdcancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(441, 313);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdClose_MouseMove);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(276, 313);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.cmdSave.Leave += new System.EventHandler(this.cmdSave_Leave);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(193, 313);
            this.cmdDelete.TabIndex = 5;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(26, 313);
            this.cmdAdd.TabIndex = 3;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(110, 313);
            this.cmdEdit.TabIndex = 4;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // grdView
            // 
            this.grdView.AllowUserToAddRows = false;
            this.grdView.BackgroundColor = System.Drawing.Color.White;
            this.grdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORG_BRAN,
            this.RECEIVER_BRAN});
            this.grdView.Location = new System.Drawing.Point(3, 80);
            this.grdView.Name = "grdView";
            this.grdView.ReadOnly = true;
            this.grdView.RowHeadersWidth = 30;
            this.grdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdView.Size = new System.Drawing.Size(504, 215);
            this.grdView.TabIndex = 2;
            this.grdView.TabStop = false;
            this.grdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdView_CellClick);
            this.grdView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdView_CellEnter);
            // 
            // ORG_BRAN
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ORG_BRAN.DefaultCellStyle = dataGridViewCellStyle1;
            this.ORG_BRAN.HeaderText = "Original branch";
            this.ORG_BRAN.Name = "ORG_BRAN";
            this.ORG_BRAN.ReadOnly = true;
            this.ORG_BRAN.Width = 200;
            // 
            // RECEIVER_BRAN
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RECEIVER_BRAN.DefaultCellStyle = dataGridViewCellStyle2;
            this.RECEIVER_BRAN.HeaderText = "Receiver branch";
            this.RECEIVER_BRAN.Name = "RECEIVER_BRAN";
            this.RECEIVER_BRAN.ReadOnly = true;
            this.RECEIVER_BRAN.Width = 200;
            // 
            // cboBranch
            // 
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(108, 18);
            this.cboBranch.Margin = new System.Windows.Forms.Padding(4);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(121, 21);
            this.cboBranch.TabIndex = 1;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            this.cboBranch.Leave += new System.EventHandler(this.cboBranch_Leave);
            // 
            // cboBranchDes
            // 
            this.cboBranchDes.FormattingEnabled = true;
            this.cboBranchDes.Location = new System.Drawing.Point(108, 45);
            this.cboBranchDes.Margin = new System.Windows.Forms.Padding(4);
            this.cboBranchDes.Name = "cboBranchDes";
            this.cboBranchDes.Size = new System.Drawing.Size(121, 21);
            this.cboBranchDes.TabIndex = 2;
            this.cboBranchDes.SelectedIndexChanged += new System.EventHandler(this.cboBranchDes_SelectedIndexChanged);
            this.cboBranchDes.Leave += new System.EventHandler(this.cboBranchDes_Leave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBranchReceiver);
            this.panel1.Controls.Add(this.lblBranchOri);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.grdView);
            this.panel1.Controls.Add(this.cboBranch);
            this.panel1.Controls.Add(this.cboBranchDes);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 298);
            this.panel1.TabIndex = 21;
            // 
            // lblBranchReceiver
            // 
            this.lblBranchReceiver.AutoSize = true;
            this.lblBranchReceiver.Location = new System.Drawing.Point(236, 48);
            this.lblBranchReceiver.Name = "lblBranchReceiver";
            this.lblBranchReceiver.Size = new System.Drawing.Size(0, 13);
            this.lblBranchReceiver.TabIndex = 22;
            // 
            // lblBranchOri
            // 
            this.lblBranchOri.AutoSize = true;
            this.lblBranchOri.Location = new System.Drawing.Point(236, 21);
            this.lblBranchOri.Name = "lblBranchOri";
            this.lblBranchOri.Size = new System.Drawing.Size(0, 13);
            this.lblBranchOri.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Receiver branch";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 21);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Original branch";
            // 
            // cmdcancel
            // 
            this.cmdcancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdcancel.Location = new System.Drawing.Point(359, 313);
            this.cmdcancel.Name = "cmdcancel";
            this.cmdcancel.Size = new System.Drawing.Size(80, 30);
            this.cmdcancel.TabIndex = 7;
            this.cmdcancel.Text = "C&ancel";
            this.cmdcancel.UseVisualStyleBackColor = true;
            this.cmdcancel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdcancel_MouseMove);
            this.cmdcancel.Click += new System.EventHandler(this.cmdcancel_Click);
            // 
            // frmSWRMBrAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 357);
            this.Controls.Add(this.cmdcancel);
            this.Controls.Add(this.panel1);
            this.Name = "frmSWRMBrAuto";
            this.Text = "Swift - Branch receive RM message";
            this.Load += new System.EventHandler(this.frmSWRMBrAuto_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSWRMBrAuto_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSWRMBrAuto_KeyDown);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.cmdcancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdView;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.ComboBox cboBranchDes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBranchReceiver;
        private System.Windows.Forms.Label lblBranchOri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button cmdcancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORG_BRAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVER_BRAN;
    }
}