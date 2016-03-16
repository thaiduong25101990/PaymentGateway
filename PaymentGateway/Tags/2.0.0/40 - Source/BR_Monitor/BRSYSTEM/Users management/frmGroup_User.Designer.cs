namespace BR.BRSYSTEM
{
    partial class frmGroup_User
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
            this.trvgroup = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtUser = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbGwtype = new System.Windows.Forms.ComboBox();
            this.txtgroupid = new System.Windows.Forms.TextBox();
            this.txtgroupname = new System.Windows.Forms.TextBox();
            this.cbdepartment = new System.Windows.Forms.ComboBox();
            this.cbroll = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdgroupuser = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.Grbcontrol.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtUser)).BeginInit();
            this.SuspendLayout();
            // 
            // Grbcontrol
            // 
            this.Grbcontrol.Controls.Add(this.cmdCancel);
            this.Grbcontrol.Controls.Add(this.txtgroupid);
            this.Grbcontrol.Controls.Add(this.label1);
            this.Grbcontrol.Controls.Add(this.label2);
            this.Grbcontrol.Controls.Add(this.txtgroupname);
            this.Grbcontrol.Controls.Add(this.label3);
            this.Grbcontrol.Controls.Add(this.label4);
            this.Grbcontrol.Controls.Add(this.cbGwtype);
            this.Grbcontrol.Controls.Add(this.cbroll);
            this.Grbcontrol.Controls.Add(this.cbdepartment);
            this.Grbcontrol.Controls.Add(this.txtDescription);
            this.Grbcontrol.Controls.Add(this.label6);
            this.Grbcontrol.Controls.Add(this.label5);
            this.Grbcontrol.Location = new System.Drawing.Point(248, 8);
            this.Grbcontrol.Size = new System.Drawing.Size(501, 264);
            this.Grbcontrol.TabIndex = 2;
            this.Grbcontrol.Text = "Group informations";
            this.Grbcontrol.Controls.SetChildIndex(this.label5, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.label6, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.txtDescription, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cbdepartment, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cbroll, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cbGwtype, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.label4, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.label3, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.txtgroupname, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.label2, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cmdSave, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.label1, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.txtgroupid, 0);
            this.Grbcontrol.Controls.SetChildIndex(this.cmdCancel, 0);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdd.Location = new System.Drawing.Point(414, 16);
            this.cmdAdd.TabIndex = 6;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(414, 88);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Location = new System.Drawing.Point(414, 158);
            this.cmdDelete.TabIndex = 9;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            this.cmdDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdDelete_MouseDown);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEdit.Location = new System.Drawing.Point(414, 52);
            this.cmdEdit.TabIndex = 7;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trvgroup);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 486);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of groups";
            // 
            // trvgroup
            // 
            this.trvgroup.ForeColor = System.Drawing.Color.Black;
            this.trvgroup.Location = new System.Drawing.Point(6, 18);
            this.trvgroup.Name = "trvgroup";
            this.trvgroup.Size = new System.Drawing.Size(218, 462);
            this.trvgroup.TabIndex = 2;
            this.trvgroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.trvgroup.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trvgroup_MouseMove);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtUser);
            this.groupBox2.Location = new System.Drawing.Point(248, 270);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 224);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Group - Users";
            // 
            // dtUser
            // 
            this.dtUser.AllowUserToAddRows = false;
            this.dtUser.AllowUserToDeleteRows = false;
            this.dtUser.AllowUserToResizeRows = false;
            this.dtUser.BackgroundColor = System.Drawing.Color.White;
            this.dtUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtUser.Location = new System.Drawing.Point(6, 18);
            this.dtUser.Name = "dtUser";
            this.dtUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtUser.Size = new System.Drawing.Size(488, 200);
            this.dtUser.TabIndex = 14;
            this.dtUser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtUser_MouseMove);
            this.dtUser.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtUser_ColumnHeaderMouseClick);
            this.dtUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtUser_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Group ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Group name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Module :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Channel :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Role:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Descriptions:";
            // 
            // cbGwtype
            // 
            this.cbGwtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGwtype.FormattingEnabled = true;
            this.cbGwtype.Location = new System.Drawing.Point(100, 91);
            this.cbGwtype.Name = "cbGwtype";
            this.cbGwtype.Size = new System.Drawing.Size(217, 24);
            this.cbGwtype.TabIndex = 2;
            // 
            // txtgroupid
            // 
            this.txtgroupid.Location = new System.Drawing.Point(100, 24);
            this.txtgroupid.Name = "txtgroupid";
            this.txtgroupid.Size = new System.Drawing.Size(217, 23);
            this.txtgroupid.TabIndex = 0;
            // 
            // txtgroupname
            // 
            this.txtgroupname.Location = new System.Drawing.Point(100, 57);
            this.txtgroupname.Name = "txtgroupname";
            this.txtgroupname.Size = new System.Drawing.Size(217, 23);
            this.txtgroupname.TabIndex = 1;
            this.txtgroupname.Leave += new System.EventHandler(this.txtgroupname_Leave);
            // 
            // cbdepartment
            // 
            this.cbdepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdepartment.FormattingEnabled = true;
            this.cbdepartment.Location = new System.Drawing.Point(100, 126);
            this.cbdepartment.Name = "cbdepartment";
            this.cbdepartment.Size = new System.Drawing.Size(217, 24);
            this.cbdepartment.TabIndex = 3;
            // 
            // cbroll
            // 
            this.cbroll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbroll.FormattingEnabled = true;
            this.cbroll.Location = new System.Drawing.Point(100, 161);
            this.cbroll.Name = "cbroll";
            this.cbroll.Size = new System.Drawing.Size(217, 24);
            this.cbroll.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(100, 196);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(394, 60);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(662, 499);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 11;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdgroupuser
            // 
            this.cmdgroupuser.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdgroupuser.Location = new System.Drawing.Point(498, 499);
            this.cmdgroupuser.Name = "cmdgroupuser";
            this.cmdgroupuser.Size = new System.Drawing.Size(80, 30);
            this.cmdgroupuser.TabIndex = 10;
            this.cmdgroupuser.Text = "&Groups";
            this.cmdgroupuser.UseVisualStyleBackColor = true;
            this.cmdgroupuser.Click += new System.EventHandler(this.cmdgroupuser_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(414, 123);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 30);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Ca&ncel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(580, 499);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(80, 30);
            this.cmdRemove.TabIndex = 14;
            this.cmdRemove.Text = "&Remove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // frmGroup_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 540);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdgroupuser);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmGroup_User";
            this.ShowInTaskbar = false;
            this.Text = "User Group";
            this.Load += new System.EventHandler(this.frmGroup_User_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGroup_User_FormClosing_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmGroup_User_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGroup_User_KeyDown);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.Grbcontrol, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdgroupuser, 0);
            this.Controls.SetChildIndex(this.cmdRemove, 0);
            this.Grbcontrol.ResumeLayout(false);
            this.Grbcontrol.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtgroupid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtgroupname;
        private System.Windows.Forms.ComboBox cbGwtype;
        private System.Windows.Forms.ComboBox cbdepartment;
        private System.Windows.Forms.ComboBox cbroll;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DataGridView dtUser;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdgroupuser;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TreeView trvgroup;
        private System.Windows.Forms.Button cmdRemove;


    }
}