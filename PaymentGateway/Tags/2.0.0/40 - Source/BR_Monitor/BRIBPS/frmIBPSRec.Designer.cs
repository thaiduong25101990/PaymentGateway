//namespace BR.BRIBPS
//{
//    partial class frmIBPSRec
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.grReconcile = new System.Windows.Forms.GroupBox();
//            this.cmdReconcile = new System.Windows.Forms.Button();
//            this.lbtype = new System.Windows.Forms.Label();
//            this.lbdepartment = new System.Windows.Forms.Label();
//            this.lbdirection = new System.Windows.Forms.Label();
//            this.lbdate = new System.Windows.Forms.Label();
//            this.cbdirection = new System.Windows.Forms.ComboBox();
//            this.cbtype = new System.Windows.Forms.ComboBox();
//            this.cbdepartment = new System.Windows.Forms.ComboBox();
//            this.groupBox2 = new System.Windows.Forms.GroupBox();
//            this.datMessage = new System.Windows.Forms.DataGridView();
//            this.cbview = new System.Windows.Forms.ComboBox();
//            this.label1 = new System.Windows.Forms.Label();
//            this.cmdPrint = new System.Windows.Forms.Button();
//            this.pickerDate = new System.Windows.Forms.DateTimePicker();
//            this.grReconcile.SuspendLayout();
//            this.groupBox2.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // cmdClose
//            // 
//            this.cmdClose.Location = new System.Drawing.Point(604, 461);
//            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
//            // 
//            // cmdSave
//            // 
//            this.cmdSave.Location = new System.Drawing.Point(332, 538);
//            // 
//            // cmdDelete
//            // 
//            this.cmdDelete.Location = new System.Drawing.Point(246, 538);
//            // 
//            // cmdAdd
//            // 
//            this.cmdAdd.Location = new System.Drawing.Point(71, 538);
//            // 
//            // cmdEdit
//            // 
//            this.cmdEdit.Location = new System.Drawing.Point(157, 538);
//            // 
//            // grReconcile
//            // 
//            this.grReconcile.Controls.Add(this.pickerDate);
//            this.grReconcile.Controls.Add(this.cmdReconcile);
//            this.grReconcile.Controls.Add(this.lbtype);
//            this.grReconcile.Controls.Add(this.lbdepartment);
//            this.grReconcile.Controls.Add(this.lbdirection);
//            this.grReconcile.Controls.Add(this.lbdate);
//            this.grReconcile.Controls.Add(this.cbdirection);
//            this.grReconcile.Controls.Add(this.cbtype);
//            this.grReconcile.Controls.Add(this.cbdepartment);
//            this.grReconcile.Location = new System.Drawing.Point(12, 12);
//            this.grReconcile.Name = "grReconcile";
//            this.grReconcile.Size = new System.Drawing.Size(673, 100);
//            this.grReconcile.TabIndex = 2;
//            this.grReconcile.TabStop = false;
//            // 
//            // cmdReconcile
//            // 
//            this.cmdReconcile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cmdReconcile.Location = new System.Drawing.Point(577, 54);
//            this.cmdReconcile.Name = "cmdReconcile";
//            this.cmdReconcile.Size = new System.Drawing.Size(80, 30);
//            this.cmdReconcile.TabIndex = 3;
//            this.cmdReconcile.Text = "Reconcile ";
//            this.cmdReconcile.UseVisualStyleBackColor = true;
//            this.cmdReconcile.Click += new System.EventHandler(this.cmdReconcile_Click);
//            // 
//            // lbtype
//            // 
//            this.lbtype.AutoSize = true;
//            this.lbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lbtype.Location = new System.Drawing.Point(291, 68);
//            this.lbtype.Name = "lbtype";
//            this.lbtype.Size = new System.Drawing.Size(45, 16);
//            this.lbtype.TabIndex = 2;
//            this.lbtype.Text = "Type :";
//            // 
//            // lbdepartment
//            // 
//            this.lbdepartment.AutoSize = true;
//            this.lbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lbdepartment.Location = new System.Drawing.Point(291, 28);
//            this.lbdepartment.Name = "lbdepartment";
//            this.lbdepartment.Size = new System.Drawing.Size(84, 16);
//            this.lbdepartment.TabIndex = 2;
//            this.lbdepartment.Text = "Department :";
//            // 
//            // lbdirection
//            // 
//            this.lbdirection.AutoSize = true;
//            this.lbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lbdirection.Location = new System.Drawing.Point(12, 68);
//            this.lbdirection.Name = "lbdirection";
//            this.lbdirection.Size = new System.Drawing.Size(67, 16);
//            this.lbdirection.TabIndex = 2;
//            this.lbdirection.Text = "Direction :";
//            // 
//            // lbdate
//            // 
//            this.lbdate.AutoSize = true;
//            this.lbdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lbdate.Location = new System.Drawing.Point(12, 28);
//            this.lbdate.Name = "lbdate";
//            this.lbdate.Size = new System.Drawing.Size(43, 16);
//            this.lbdate.TabIndex = 2;
//            this.lbdate.Text = "Date :";
//            // 
//            // cbdirection
//            // 
//            this.cbdirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cbdirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cbdirection.FormattingEnabled = true;
//            this.cbdirection.Location = new System.Drawing.Point(84, 60);
//            this.cbdirection.Name = "cbdirection";
//            this.cbdirection.Size = new System.Drawing.Size(176, 24);
//            this.cbdirection.TabIndex = 0;
//            // 
//            // cbtype
//            // 
//            this.cbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cbtype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cbtype.FormattingEnabled = true;
//            this.cbtype.Location = new System.Drawing.Point(375, 60);
//            this.cbtype.Name = "cbtype";
//            this.cbtype.Size = new System.Drawing.Size(176, 24);
//            this.cbtype.TabIndex = 0;
//            // 
//            // cbdepartment
//            // 
//            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cbdepartment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cbdepartment.FormattingEnabled = true;
//            this.cbdepartment.Location = new System.Drawing.Point(375, 20);
//            this.cbdepartment.Name = "cbdepartment";
//            this.cbdepartment.Size = new System.Drawing.Size(176, 24);
//            this.cbdepartment.TabIndex = 0;
//            // 
//            // groupBox2
//            // 
//            this.groupBox2.Controls.Add(this.datMessage);
//            this.groupBox2.Controls.Add(this.cbview);
//            this.groupBox2.Controls.Add(this.label1);
//            this.groupBox2.Location = new System.Drawing.Point(12, 118);
//            this.groupBox2.Name = "groupBox2";
//            this.groupBox2.Size = new System.Drawing.Size(672, 337);
//            this.groupBox2.TabIndex = 3;
//            this.groupBox2.TabStop = false;
//            // 
//            // datMessage
//            // 
//            this.datMessage.AllowUserToAddRows = false;
//            this.datMessage.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
//            this.datMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.datMessage.Location = new System.Drawing.Point(15, 19);
//            this.datMessage.Name = "datMessage";
//            this.datMessage.Size = new System.Drawing.Size(642, 258);
//            this.datMessage.TabIndex = 0;
//            // 
//            // cbview
//            // 
//            this.cbview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cbview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cbview.FormattingEnabled = true;
//            this.cbview.Location = new System.Drawing.Point(84, 292);
//            this.cbview.Name = "cbview";
//            this.cbview.Size = new System.Drawing.Size(213, 24);
//            this.cbview.TabIndex = 0;
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.label1.Location = new System.Drawing.Point(12, 300);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(45, 16);
//            this.label1.TabIndex = 2;
//            this.label1.Text = "View :";
//            // 
//            // cmdPrint
//            // 
//            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cmdPrint.Location = new System.Drawing.Point(518, 461);
//            this.cmdPrint.Name = "cmdPrint";
//            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
//            this.cmdPrint.TabIndex = 3;
//            this.cmdPrint.Text = "Print";
//            this.cmdPrint.UseVisualStyleBackColor = true;
//            // 
//            // pickerDate
//            // 
//            this.pickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.pickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
//            this.pickerDate.Location = new System.Drawing.Point(84, 20);
//            this.pickerDate.Name = "pickerDate";
//            this.pickerDate.Size = new System.Drawing.Size(176, 22);
//            this.pickerDate.TabIndex = 4;
//            // 
//            // frmIBPSRec
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(692, 497);
//            this.Controls.Add(this.cmdPrint);
//            this.Controls.Add(this.grReconcile);
//            this.Controls.Add(this.groupBox2);
//            this.Name = "frmIBPSRec";
//            this.Text = "IBPS Reconcile";
//            this.Load += new System.EventHandler(this.frmIBPSRec_Load);
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIBPSRec_FormClosing);
//            this.Controls.SetChildIndex(this.cmdEdit, 0);
//            this.Controls.SetChildIndex(this.cmdAdd, 0);
//            this.Controls.SetChildIndex(this.cmdSave, 0);
//            this.Controls.SetChildIndex(this.groupBox2, 0);
//            this.Controls.SetChildIndex(this.cmdDelete, 0);
//            this.Controls.SetChildIndex(this.cmdClose, 0);
//            this.Controls.SetChildIndex(this.grReconcile, 0);
//            this.Controls.SetChildIndex(this.cmdPrint, 0);
//            this.grReconcile.ResumeLayout(false);
//            this.grReconcile.PerformLayout();
//            this.groupBox2.ResumeLayout(false);
//            this.groupBox2.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.datMessage)).EndInit();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.GroupBox grReconcile;
//        private System.Windows.Forms.Label lbtype;
//        private System.Windows.Forms.Label lbdepartment;
//        private System.Windows.Forms.Label lbdirection;
//        private System.Windows.Forms.Label lbdate;
//        private System.Windows.Forms.ComboBox cbdirection;
//        private System.Windows.Forms.ComboBox cbtype;
//        private System.Windows.Forms.ComboBox cbdepartment;
//        private System.Windows.Forms.Button cmdReconcile;
//        private System.Windows.Forms.GroupBox groupBox2;
//        private System.Windows.Forms.DataGridView datMessage;
//        private System.Windows.Forms.ComboBox cbview;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Button cmdPrint;
//        private System.Windows.Forms.DateTimePicker pickerDate;
//    }
//}