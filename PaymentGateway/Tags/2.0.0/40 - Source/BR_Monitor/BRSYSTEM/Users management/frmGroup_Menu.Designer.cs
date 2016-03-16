namespace BR.BRSYSTEM
{
    partial class frmGroup_Menu
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
            this.grbMenu = new System.Windows.Forms.GroupBox();
            this.trvmenu = new System.Windows.Forms.TreeView();
            this.grbGroup_user = new System.Windows.Forms.GroupBox();
            this.dtgGroup = new System.Windows.Forms.DataGridView();
            this.lbDepart = new System.Windows.Forms.Label();
            this.cboDepart = new System.Windows.Forms.ComboBox();
            this.grbMenu.SuspendLayout();
            this.grbGroup_user.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(706, 449);
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(623, 449);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Location = new System.Drawing.Point(458, 449);
            this.cmdDelete.TabIndex = 5;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(415, 224);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEdit.Location = new System.Drawing.Point(541, 449);
            this.cmdEdit.TabIndex = 6;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            this.cmdEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdEdit_MouseDown);
            // 
            // grbMenu
            // 
            this.grbMenu.Controls.Add(this.trvmenu);
            this.grbMenu.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbMenu.Location = new System.Drawing.Point(8, 3);
            this.grbMenu.Name = "grbMenu";
            this.grbMenu.Size = new System.Drawing.Size(215, 440);
            this.grbMenu.TabIndex = 0;
            this.grbMenu.TabStop = false;
            this.grbMenu.Text = "List of menus";
            // 
            // trvmenu
            // 
            this.trvmenu.ForeColor = System.Drawing.Color.Black;
            this.trvmenu.Location = new System.Drawing.Point(6, 20);
            this.trvmenu.Name = "trvmenu";
            this.trvmenu.Size = new System.Drawing.Size(203, 410);
            this.trvmenu.TabIndex = 1;
            this.trvmenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.trvmenu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trvmenu_MouseMove);
            // 
            // grbGroup_user
            // 
            this.grbGroup_user.Controls.Add(this.dtgGroup);
            this.grbGroup_user.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGroup_user.Location = new System.Drawing.Point(229, 3);
            this.grbGroup_user.Name = "grbGroup_user";
            this.grbGroup_user.Size = new System.Drawing.Size(565, 440);
            this.grbGroup_user.TabIndex = 2;
            this.grbGroup_user.TabStop = false;
            this.grbGroup_user.Text = "List of groups";
            // 
            // dtgGroup
            // 
            this.dtgGroup.AllowUserToAddRows = false;
            this.dtgGroup.AllowUserToDeleteRows = false;
            this.dtgGroup.AllowUserToResizeRows = false;
            this.dtgGroup.BackgroundColor = System.Drawing.Color.White;
            this.dtgGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgGroup.Location = new System.Drawing.Point(6, 20);
            this.dtgGroup.Name = "dtgGroup";
            this.dtgGroup.RowHeadersWidth = 100;
            this.dtgGroup.Size = new System.Drawing.Size(551, 410);
            this.dtgGroup.TabIndex = 3;
            this.dtgGroup.Enter += new System.EventHandler(this.dtgGroup_Enter);
            this.dtgGroup.MouseLeave += new System.EventHandler(this.dtgGroup_MouseLeave);
            this.dtgGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgGroup_CellClick);
            this.dtgGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgGroup_KeyDown);
            // 
            // lbDepart
            // 
            this.lbDepart.AutoSize = true;
            this.lbDepart.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDepart.Location = new System.Drawing.Point(14, 450);
            this.lbDepart.Name = "lbDepart";
            this.lbDepart.Size = new System.Drawing.Size(69, 18);
            this.lbDepart.TabIndex = 4;
            this.lbDepart.Text = "Channel :";
            // 
            // cboDepart
            // 
            this.cboDepart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepart.FormattingEnabled = true;
            this.cboDepart.Location = new System.Drawing.Point(96, 449);
            this.cboDepart.Name = "cboDepart";
            this.cboDepart.Size = new System.Drawing.Size(121, 21);
            this.cboDepart.TabIndex = 4;
            this.cboDepart.SelectedIndexChanged += new System.EventHandler(this.cboDepart_SelectedIndexChanged);
            // 
            // frmGroup_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 483);
            this.Controls.Add(this.cboDepart);
            this.Controls.Add(this.lbDepart);
            this.Controls.Add(this.grbMenu);
            this.Controls.Add(this.grbGroup_user);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmGroup_Menu";
            this.Text = "Group menu";
            this.Load += new System.EventHandler(this.frmGroup_Menu_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGroup_Menu_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmGroup_Menu_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.grbGroup_user, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.grbMenu, 0);
            this.Controls.SetChildIndex(this.lbDepart, 0);
            this.Controls.SetChildIndex(this.cboDepart, 0);
            this.grbMenu.ResumeLayout(false);
            this.grbGroup_user.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMenu;
        private System.Windows.Forms.TreeView trvmenu;
        private System.Windows.Forms.GroupBox grbGroup_user;
        private System.Windows.Forms.DataGridView dtgGroup;
        private System.Windows.Forms.Label lbDepart;
        private System.Windows.Forms.ComboBox cboDepart;
    }
}