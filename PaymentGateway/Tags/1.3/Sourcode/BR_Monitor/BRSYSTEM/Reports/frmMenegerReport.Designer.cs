namespace BR.BRSYSTEM
{
    partial class frmMenegerReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboTypeGW = new System.Windows.Forms.ComboBox();
            this.txtRptName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.grbReport = new System.Windows.Forms.GroupBox();
            this.grbReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(493, 104);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(407, 104);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(318, 291);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(143, 291);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(229, 291);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report name :";
            // 
            // cboTypeGW
            // 
            this.cboTypeGW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeGW.FormattingEnabled = true;
            this.cboTypeGW.Location = new System.Drawing.Point(379, 28);
            this.cboTypeGW.Name = "cboTypeGW";
            this.cboTypeGW.Size = new System.Drawing.Size(189, 21);
            this.cboTypeGW.TabIndex = 1;
            // 
            // txtRptName
            // 
            this.txtRptName.Location = new System.Drawing.Point(117, 28);
            this.txtRptName.MaxLength = 20;
            this.txtRptName.Name = "txtRptName";
            this.txtRptName.Size = new System.Drawing.Size(168, 20);
            this.txtRptName.TabIndex = 0;
            this.txtRptName.Leave += new System.EventHandler(this.txtRptName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(305, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Channel :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description : ";
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(117, 60);
            this.txtDetail.MaxLength = 100;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(451, 20);
            this.txtDetail.TabIndex = 2;
            this.txtDetail.Leave += new System.EventHandler(this.txtDetail_Leave);
            // 
            // grbReport
            // 
            this.grbReport.Controls.Add(this.txtDetail);
            this.grbReport.Controls.Add(this.label4);
            this.grbReport.Controls.Add(this.label3);
            this.grbReport.Controls.Add(this.txtRptName);
            this.grbReport.Controls.Add(this.cboTypeGW);
            this.grbReport.Controls.Add(this.label1);
            this.grbReport.Location = new System.Drawing.Point(5, 6);
            this.grbReport.Name = "grbReport";
            this.grbReport.Size = new System.Drawing.Size(588, 92);
            this.grbReport.TabIndex = 8;
            this.grbReport.TabStop = false;
            this.grbReport.Text = "Registry Content";
            // 
            // frmMenegerReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 141);
            this.Controls.Add(this.grbReport);
            this.Name = "frmMenegerReport";
            this.Text = "Report management";
            this.Load += new System.EventHandler(this.frmMenegerReport_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMenegerReport_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMenegerReport_KeyDown);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.grbReport, 0);
            this.grbReport.ResumeLayout(false);
            this.grbReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTypeGW;
        private System.Windows.Forms.TextBox txtRptName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.GroupBox grbReport;
    }
}