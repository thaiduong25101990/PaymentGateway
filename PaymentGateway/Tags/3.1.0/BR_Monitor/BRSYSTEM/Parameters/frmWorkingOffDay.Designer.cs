namespace BR.BRSYSTEM.Parameters
{
    partial class frmWorkingOffDay
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
            this.grbText = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.dWorkingOffDay = new System.Windows.Forms.DateTimePicker();
            this.lblWorkingOff = new System.Windows.Forms.Label();
            this.dgrListView = new System.Windows.Forms.DataGridView();
            this.grbText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrListView)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(360, 364);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(274, 364);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(188, 364);
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(13, 364);
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(99, 364);
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // grbText
            // 
            this.grbText.Controls.Add(this.txtDescription);
            this.grbText.Controls.Add(this.lblDescription);
            this.grbText.Controls.Add(this.dWorkingOffDay);
            this.grbText.Controls.Add(this.lblWorkingOff);
            this.grbText.Location = new System.Drawing.Point(8, 9);
            this.grbText.Name = "grbText";
            this.grbText.Size = new System.Drawing.Size(431, 84);
            this.grbText.TabIndex = 2;
            this.grbText.TabStop = false;
            this.grbText.Text = "Detail";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(104, 42);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(291, 33);
            this.txtDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(9, 48);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description: ";
            // 
            // dWorkingOffDay
            // 
            this.dWorkingOffDay.CustomFormat = "dd/MM/yyyy";
            this.dWorkingOffDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dWorkingOffDay.Location = new System.Drawing.Point(104, 16);
            this.dWorkingOffDay.Name = "dWorkingOffDay";
            this.dWorkingOffDay.Size = new System.Drawing.Size(125, 20);
            this.dWorkingOffDay.TabIndex = 1;
            // 
            // lblWorkingOff
            // 
            this.lblWorkingOff.AutoSize = true;
            this.lblWorkingOff.Location = new System.Drawing.Point(9, 22);
            this.lblWorkingOff.Name = "lblWorkingOff";
            this.lblWorkingOff.Size = new System.Drawing.Size(89, 13);
            this.lblWorkingOff.TabIndex = 0;
            this.lblWorkingOff.Text = "Working Off Day:";
            // 
            // dgrListView
            // 
            this.dgrListView.AllowUserToAddRows = false;
            this.dgrListView.AllowUserToDeleteRows = false;
            this.dgrListView.AllowUserToOrderColumns = true;
            this.dgrListView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgrListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrListView.Location = new System.Drawing.Point(14, 111);
            this.dgrListView.Name = "dgrListView";
            this.dgrListView.ReadOnly = true;
            this.dgrListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrListView.Size = new System.Drawing.Size(425, 220);
            this.dgrListView.TabIndex = 3;
            this.dgrListView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrListView_CellContentClick);
            this.dgrListView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrListView_CellContentClick);
            this.dgrListView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrListView_CellContentClick);
            // 
            // frmWorkingOffDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 409);
            this.Controls.Add(this.dgrListView);
            this.Controls.Add(this.grbText);
            this.Name = "frmWorkingOffDay";
            this.Text = "Working off day";
            this.Load += new System.EventHandler(this.frmWorkingOffDay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmWorkingOffDay_KeyDown);
            this.Controls.SetChildIndex(this.grbText, 0);
            this.Controls.SetChildIndex(this.dgrListView, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.grbText.ResumeLayout(false);
            this.grbText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbText;
        private System.Windows.Forms.DateTimePicker dWorkingOffDay;
        private System.Windows.Forms.Label lblWorkingOff;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DataGridView dgrListView;


    }
}