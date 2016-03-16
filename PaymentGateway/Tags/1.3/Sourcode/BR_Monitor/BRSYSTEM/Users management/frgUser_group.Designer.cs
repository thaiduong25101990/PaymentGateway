namespace BR.BRSYSTEM
{
    partial class frgUser_group
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
            this.trvuser = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtrgroup_user = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtggroup = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdAddall = new System.Windows.Forms.Button();
            this.cmdRemoall = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtrgroup_user)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtggroup)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(743, 564);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(660, 564);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(696, 312);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdDelete.Visible = false;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(486, 280);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdAdd.TabIndex = 3;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(733, 320);
            this.cmdEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trvuser);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 554);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Users";
            // 
            // trvuser
            // 
            this.trvuser.Location = new System.Drawing.Point(6, 19);
            this.trvuser.Name = "trvuser";
            this.trvuser.Size = new System.Drawing.Size(347, 518);
            this.trvuser.TabIndex = 1;
            this.trvuser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.trvuser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trvuser_MouseMove);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtrgroup_user);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(369, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 246);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Group - Users";
            // 
            // dtrgroup_user
            // 
            this.dtrgroup_user.AllowUserToAddRows = false;
            this.dtrgroup_user.AllowUserToDeleteRows = false;
            this.dtrgroup_user.AllowUserToResizeRows = false;
            this.dtrgroup_user.BackgroundColor = System.Drawing.Color.White;
            this.dtrgroup_user.ColumnHeadersHeight = 21;
            this.dtrgroup_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtrgroup_user.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column6});
            this.dtrgroup_user.Location = new System.Drawing.Point(7, 19);
            this.dtrgroup_user.Name = "dtrgroup_user";
            this.dtrgroup_user.RowHeadersWidth = 30;
            this.dtrgroup_user.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtrgroup_user.Size = new System.Drawing.Size(470, 218);
            this.dtrgroup_user.TabIndex = 0;
            this.dtrgroup_user.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtrgroup_user_MouseMove);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Select";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Group name";
            this.Column4.Name = "Column4";
            this.Column4.Width = 358;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtggroup);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(369, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 270);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "List of Groups";
            // 
            // dtggroup
            // 
            this.dtggroup.AllowUserToAddRows = false;
            this.dtggroup.AllowUserToDeleteRows = false;
            this.dtggroup.AllowUserToResizeRows = false;
            this.dtggroup.BackgroundColor = System.Drawing.Color.White;
            this.dtggroup.ColumnHeadersHeight = 21;
            this.dtggroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtggroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5});
            this.dtggroup.Location = new System.Drawing.Point(6, 19);
            this.dtggroup.Name = "dtggroup";
            this.dtggroup.RowHeadersWidth = 30;
            this.dtggroup.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtggroup.Size = new System.Drawing.Size(471, 237);
            this.dtggroup.TabIndex = 0;
            this.dtggroup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtggroup_MouseDown);
            this.dtggroup.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtggroup_MouseMove);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Group name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 350;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // cmdRemove
            // 
            this.cmdRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemove.Location = new System.Drawing.Point(651, 280);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(80, 30);
            this.cmdRemove.TabIndex = 5;
            this.cmdRemove.Text = "&Remove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAddall
            // 
            this.cmdAddall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddall.Location = new System.Drawing.Point(569, 280);
            this.cmdAddall.Name = "cmdAddall";
            this.cmdAddall.Size = new System.Drawing.Size(80, 30);
            this.cmdAddall.TabIndex = 4;
            this.cmdAddall.Text = "A&dd all";
            this.cmdAddall.UseVisualStyleBackColor = true;
            this.cmdAddall.Click += new System.EventHandler(this.cmdAddall_Click);
            // 
            // cmdRemoall
            // 
            this.cmdRemoall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoall.Location = new System.Drawing.Point(733, 280);
            this.cmdRemoall.Name = "cmdRemoall";
            this.cmdRemoall.Size = new System.Drawing.Size(90, 30);
            this.cmdRemoall.TabIndex = 6;
            this.cmdRemoall.Text = "R&emove all";
            this.cmdRemoall.UseVisualStyleBackColor = true;
            this.cmdRemoall.Click += new System.EventHandler(this.cmdRemoall_Click);
            // 
            // frgUser_group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 601);
            this.Controls.Add(this.cmdRemoall);
            this.Controls.Add(this.cmdAddall);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frgUser_group";
            this.Text = "User group";
            this.Load += new System.EventHandler(this.frgUser_group_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frgUser_group_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frgUser_group_KeyDown);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmdRemove, 0);
            this.Controls.SetChildIndex(this.cmdAddall, 0);
            this.Controls.SetChildIndex(this.cmdRemoall, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtrgroup_user)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtggroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdAddall;
        private System.Windows.Forms.Button cmdRemoall;
        private System.Windows.Forms.TreeView trvuser;
        private System.Windows.Forms.DataGridView dtrgroup_user;
        private System.Windows.Forms.DataGridView dtggroup;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;



    }
}