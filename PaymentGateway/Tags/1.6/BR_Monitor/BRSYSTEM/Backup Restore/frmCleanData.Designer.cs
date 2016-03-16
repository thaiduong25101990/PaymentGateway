namespace BR.BRSYSTEM
{
    partial class frmCleanData
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dattable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmdclean = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dattable)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(705, 282);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click_1);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(308, 381);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(222, 381);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(47, 381);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(133, 381);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dattable);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dattable
            // 
            this.dattable.AllowUserToAddRows = false;
            this.dattable.AllowUserToDeleteRows = false;
            this.dattable.AllowUserToResizeRows = false;
            this.dattable.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dattable.ColumnHeadersHeight = 21;
            this.dattable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dattable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6});
            this.dattable.Location = new System.Drawing.Point(6, 15);
            this.dattable.Name = "dattable";
            this.dattable.RowHeadersWidth = 40;
            this.dattable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dattable.Size = new System.Drawing.Size(789, 226);
            this.dattable.TabIndex = 0;
            this.dattable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dattable_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Clean";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "                  Table name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 230;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "            File path";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "     Latest clean date";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "   Next auto clean date";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 150;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 258);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(792, 14);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 1;
            // 
            // cmdclean
            // 
            this.cmdclean.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdclean.Location = new System.Drawing.Point(623, 282);
            this.cmdclean.Name = "cmdclean";
            this.cmdclean.Size = new System.Drawing.Size(80, 30);
            this.cmdclean.TabIndex = 2;
            this.cmdclean.Text = "Cl&ean";
            this.cmdclean.UseVisualStyleBackColor = true;
            this.cmdclean.Click += new System.EventHandler(this.cmdclean_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            // 
            // frmCleanData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 318);
            this.Controls.Add(this.cmdclean);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCleanData";
            this.Text = "Clean table";
            this.Load += new System.EventHandler(this.frmCleanData_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCleanData_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCleanData_KeyDown);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.cmdclean, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dattable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dattable;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button cmdclean;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}