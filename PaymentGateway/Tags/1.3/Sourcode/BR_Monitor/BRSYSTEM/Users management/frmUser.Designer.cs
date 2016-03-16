namespace BR.BRSYSTEM
{
    partial class frmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            this.grblistuser = new System.Windows.Forms.GroupBox();
            this.trvuser = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbEdit = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.tlbCancel = new System.Windows.Forms.ToolStripButton();
            this.tlbClose = new System.Windows.Forms.ToolStripButton();
            this.grbuser = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtlastchange = new System.Windows.Forms.DateTimePicker();
            this.txtdescription = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txtmobile = new System.Windows.Forms.TextBox();
            this.txtconfimpass = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.dtlasttrans = new System.Windows.Forms.DateTimePicker();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.txtuserid = new System.Windows.Forms.TextBox();
            this.cbstatus = new System.Windows.Forms.ComboBox();
            this.cbbranch = new System.Windows.Forms.ComboBox();
            this.lbdescription = new System.Windows.Forms.Label();
            this.lbpassworddate = new System.Windows.Forms.Label();
            this.lbstatus = new System.Windows.Forms.Label();
            this.lbemail = new System.Windows.Forms.Label();
            this.lblastchange = new System.Windows.Forms.Label();
            this.lbpassword = new System.Windows.Forms.Label();
            this.lbmobile = new System.Windows.Forms.Label();
            this.lblasttransdate = new System.Windows.Forms.Label();
            this.lbusername = new System.Windows.Forms.Label();
            this.lbconfimpass = new System.Windows.Forms.Label();
            this.lbuserid = new System.Windows.Forms.Label();
            this.lbBranch = new System.Windows.Forms.Label();
            this.grblistuser.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grbuser.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(427, 612);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(341, 612);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(255, 612);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(80, 612);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(166, 612);
            // 
            // grblistuser
            // 
            this.grblistuser.Controls.Add(this.trvuser);
            this.grblistuser.Location = new System.Drawing.Point(12, 46);
            this.grblistuser.Name = "grblistuser";
            this.grblistuser.Size = new System.Drawing.Size(215, 393);
            this.grblistuser.TabIndex = 30;
            this.grblistuser.TabStop = false;
            this.grblistuser.Text = "List of users";
            // 
            // trvuser
            // 
            this.trvuser.ForeColor = System.Drawing.Color.Black;
            this.trvuser.Location = new System.Drawing.Point(6, 22);
            this.trvuser.Name = "trvuser";
            this.trvuser.Size = new System.Drawing.Size(203, 361);
            this.trvuser.TabIndex = 1;
            this.trvuser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.trvuser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trvuser_MouseMove);
            this.trvuser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvuser_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbAdd,
            this.tlbEdit,
            this.tlbDelete,
            this.tlbSave,
            this.tlbCancel,
            this.tlbClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(756, 40);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlbAdd
            // 
            this.tlbAdd.Image = global::BR.BRSYSTEM.Properties.Resources.Add_gw;
            this.tlbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbAdd.Name = "tlbAdd";
            this.tlbAdd.RightToLeftAutoMirrorImage = true;
            this.tlbAdd.Size = new System.Drawing.Size(50, 37);
            this.tlbAdd.Text = "&Add";
            this.tlbAdd.Click += new System.EventHandler(this.tlbAdd_Click);
            // 
            // tlbEdit
            // 
            this.tlbEdit.Image = global::BR.BRSYSTEM.Properties.Resources.app;
            this.tlbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbEdit.Name = "tlbEdit";
            this.tlbEdit.Size = new System.Drawing.Size(49, 37);
            this.tlbEdit.Text = "&Edit";
            this.tlbEdit.Click += new System.EventHandler(this.tlbEdit_Click);
            // 
            // tlbDelete
            // 
            this.tlbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tlbDelete.Image")));
            this.tlbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbDelete.Name = "tlbDelete";
            this.tlbDelete.Size = new System.Drawing.Size(64, 37);
            this.tlbDelete.Text = "&Delete";
            this.tlbDelete.Click += new System.EventHandler(this.tlbDelete_Click);
            // 
            // tlbSave
            // 
            this.tlbSave.Image = global::BR.BRSYSTEM.Properties.Resources.save_f2;
            this.tlbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSave.Name = "tlbSave";
            this.tlbSave.Size = new System.Drawing.Size(56, 37);
            this.tlbSave.Text = "&Save";
            this.tlbSave.Click += new System.EventHandler(this.tlbSave_Click);
            // 
            // tlbCancel
            // 
            this.tlbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlbCancel.Image")));
            this.tlbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbCancel.Name = "tlbCancel";
            this.tlbCancel.Size = new System.Drawing.Size(66, 37);
            this.tlbCancel.Text = "Ca&ncel";
            this.tlbCancel.Click += new System.EventHandler(this.tlbCancel_Click);
            // 
            // tlbClose
            // 
            this.tlbClose.Image = global::BR.BRSYSTEM.Properties.Resources.cpanel;
            this.tlbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClose.Name = "tlbClose";
            this.tlbClose.Size = new System.Drawing.Size(59, 37);
            this.tlbClose.Text = "&Close";
            this.tlbClose.Click += new System.EventHandler(this.tlbClose_Click);
            // 
            // grbuser
            // 
            this.grbuser.Controls.Add(this.label6);
            this.grbuser.Controls.Add(this.label5);
            this.grbuser.Controls.Add(this.label4);
            this.grbuser.Controls.Add(this.label3);
            this.grbuser.Controls.Add(this.label2);
            this.grbuser.Controls.Add(this.label1);
            this.grbuser.Controls.Add(this.dtlastchange);
            this.grbuser.Controls.Add(this.txtdescription);
            this.grbuser.Controls.Add(this.txtemail);
            this.grbuser.Controls.Add(this.txtmobile);
            this.grbuser.Controls.Add(this.txtconfimpass);
            this.grbuser.Controls.Add(this.txtpassword);
            this.grbuser.Controls.Add(this.dtlasttrans);
            this.grbuser.Controls.Add(this.txtusername);
            this.grbuser.Controls.Add(this.txtuserid);
            this.grbuser.Controls.Add(this.cbstatus);
            this.grbuser.Controls.Add(this.cbbranch);
            this.grbuser.Controls.Add(this.lbdescription);
            this.grbuser.Controls.Add(this.lbpassworddate);
            this.grbuser.Controls.Add(this.lbstatus);
            this.grbuser.Controls.Add(this.lbemail);
            this.grbuser.Controls.Add(this.lblastchange);
            this.grbuser.Controls.Add(this.lbpassword);
            this.grbuser.Controls.Add(this.lbmobile);
            this.grbuser.Controls.Add(this.lblasttransdate);
            this.grbuser.Controls.Add(this.lbusername);
            this.grbuser.Controls.Add(this.lbconfimpass);
            this.grbuser.Controls.Add(this.lbuserid);
            this.grbuser.Controls.Add(this.lbBranch);
            this.grbuser.Location = new System.Drawing.Point(233, 46);
            this.grbuser.Name = "grbuser";
            this.grbuser.Size = new System.Drawing.Size(512, 393);
            this.grbuser.TabIndex = 31;
            this.grbuser.TabStop = false;
            this.grbuser.Text = "User information";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(344, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(344, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(344, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(344, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(344, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(344, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "*";
            // 
            // dtlastchange
            // 
            this.dtlastchange.CustomFormat = "dd/MM/yyyy";
            this.dtlastchange.Enabled = false;
            this.dtlastchange.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtlastchange.Location = new System.Drawing.Point(138, 326);
            this.dtlastchange.Name = "dtlastchange";
            this.dtlastchange.ShowCheckBox = true;
            this.dtlastchange.Size = new System.Drawing.Size(200, 23);
            this.dtlastchange.TabIndex = 12;
            // 
            // txtdescription
            // 
            this.txtdescription.Location = new System.Drawing.Point(138, 355);
            this.txtdescription.Name = "txtdescription";
            this.txtdescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtdescription.Size = new System.Drawing.Size(361, 23);
            this.txtdescription.TabIndex = 13;
            this.txtdescription.Leave += new System.EventHandler(this.txtdescription_Leave);
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(138, 236);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(200, 23);
            this.txtemail.TabIndex = 9;
            // 
            // txtmobile
            // 
            this.txtmobile.Location = new System.Drawing.Point(138, 207);
            this.txtmobile.Name = "txtmobile";
            this.txtmobile.Size = new System.Drawing.Size(200, 23);
            this.txtmobile.TabIndex = 8;
            // 
            // txtconfimpass
            // 
            this.txtconfimpass.Location = new System.Drawing.Point(138, 178);
            this.txtconfimpass.Name = "txtconfimpass";
            this.txtconfimpass.PasswordChar = '*';
            this.txtconfimpass.Size = new System.Drawing.Size(200, 23);
            this.txtconfimpass.TabIndex = 7;
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(138, 149);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(200, 23);
            this.txtpassword.TabIndex = 6;
            // 
            // dtlasttrans
            // 
            this.dtlasttrans.CustomFormat = "dd/MM/yyyy";
            this.dtlasttrans.Enabled = false;
            this.dtlasttrans.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtlasttrans.Location = new System.Drawing.Point(138, 297);
            this.dtlasttrans.Name = "dtlasttrans";
            this.dtlasttrans.ShowCheckBox = true;
            this.dtlasttrans.Size = new System.Drawing.Size(200, 23);
            this.dtlasttrans.TabIndex = 11;
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(138, 120);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(200, 23);
            this.txtusername.TabIndex = 5;
            this.txtusername.Leave += new System.EventHandler(this.txtusername_Leave);
            // 
            // txtuserid
            // 
            this.txtuserid.Location = new System.Drawing.Point(138, 91);
            this.txtuserid.MaxLength = 14;
            this.txtuserid.Name = "txtuserid";
            this.txtuserid.Size = new System.Drawing.Size(200, 23);
            this.txtuserid.TabIndex = 4;
            this.txtuserid.TextChanged += new System.EventHandler(this.txtuserid_TextChanged);
            // 
            // cbstatus
            // 
            this.cbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbstatus.FormattingEnabled = true;
            this.cbstatus.Location = new System.Drawing.Point(138, 265);
            this.cbstatus.Name = "cbstatus";
            this.cbstatus.Size = new System.Drawing.Size(200, 24);
            this.cbstatus.TabIndex = 10;
            // 
            // cbbranch
            // 
            this.cbbranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbranch.FormattingEnabled = true;
            this.cbbranch.Location = new System.Drawing.Point(138, 61);
            this.cbbranch.Name = "cbbranch";
            this.cbbranch.Size = new System.Drawing.Size(200, 24);
            this.cbbranch.TabIndex = 3;
            this.cbbranch.SelectedIndexChanged += new System.EventHandler(this.cbbranch_SelectedIndexChanged);
            // 
            // lbdescription
            // 
            this.lbdescription.AutoSize = true;
            this.lbdescription.Location = new System.Drawing.Point(15, 367);
            this.lbdescription.Name = "lbdescription";
            this.lbdescription.Size = new System.Drawing.Size(76, 16);
            this.lbdescription.TabIndex = 30;
            this.lbdescription.Text = "Description:";
            // 
            // lbpassworddate
            // 
            this.lbpassworddate.AutoSize = true;
            this.lbpassworddate.Location = new System.Drawing.Point(15, 341);
            this.lbpassworddate.Name = "lbpassworddate";
            this.lbpassworddate.Size = new System.Drawing.Size(97, 16);
            this.lbpassworddate.TabIndex = 29;
            this.lbpassworddate.Text = "password date:";
            // 
            // lbstatus
            // 
            this.lbstatus.AutoSize = true;
            this.lbstatus.Location = new System.Drawing.Point(15, 269);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(49, 16);
            this.lbstatus.TabIndex = 27;
            this.lbstatus.Text = "Status:";
            // 
            // lbemail
            // 
            this.lbemail.AutoSize = true;
            this.lbemail.Location = new System.Drawing.Point(15, 239);
            this.lbemail.Name = "lbemail";
            this.lbemail.Size = new System.Drawing.Size(44, 16);
            this.lbemail.TabIndex = 26;
            this.lbemail.Text = "Email:";
            // 
            // lblastchange
            // 
            this.lblastchange.AutoSize = true;
            this.lblastchange.Location = new System.Drawing.Point(15, 325);
            this.lblastchange.Name = "lblastchange";
            this.lblastchange.Size = new System.Drawing.Size(76, 16);
            this.lblastchange.TabIndex = 0;
            this.lblastchange.Text = "Last change";
            // 
            // lbpassword
            // 
            this.lbpassword.AutoSize = true;
            this.lbpassword.Location = new System.Drawing.Point(15, 152);
            this.lbpassword.Name = "lbpassword";
            this.lbpassword.Size = new System.Drawing.Size(68, 16);
            this.lbpassword.TabIndex = 23;
            this.lbpassword.Text = "Password:";
            // 
            // lbmobile
            // 
            this.lbmobile.AutoSize = true;
            this.lbmobile.Location = new System.Drawing.Point(15, 210);
            this.lbmobile.Name = "lbmobile";
            this.lbmobile.Size = new System.Drawing.Size(50, 16);
            this.lbmobile.TabIndex = 25;
            this.lbmobile.Text = "Mobile:";
            // 
            // lblasttransdate
            // 
            this.lblasttransdate.AutoSize = true;
            this.lblasttransdate.Location = new System.Drawing.Point(15, 300);
            this.lblasttransdate.Name = "lblasttransdate";
            this.lblasttransdate.Size = new System.Drawing.Size(98, 16);
            this.lblasttransdate.TabIndex = 28;
            this.lblasttransdate.Text = "Last trans date:";
            // 
            // lbusername
            // 
            this.lbusername.AutoSize = true;
            this.lbusername.Location = new System.Drawing.Point(15, 123);
            this.lbusername.Name = "lbusername";
            this.lbusername.Size = new System.Drawing.Size(75, 16);
            this.lbusername.TabIndex = 22;
            this.lbusername.Text = "User name:";
            // 
            // lbconfimpass
            // 
            this.lbconfimpass.AutoSize = true;
            this.lbconfimpass.Location = new System.Drawing.Point(15, 181);
            this.lbconfimpass.Name = "lbconfimpass";
            this.lbconfimpass.Size = new System.Drawing.Size(112, 16);
            this.lbconfimpass.TabIndex = 24;
            this.lbconfimpass.Text = "Confim password:";
            // 
            // lbuserid
            // 
            this.lbuserid.AutoSize = true;
            this.lbuserid.Location = new System.Drawing.Point(15, 94);
            this.lbuserid.Name = "lbuserid";
            this.lbuserid.Size = new System.Drawing.Size(55, 16);
            this.lbuserid.TabIndex = 21;
            this.lbuserid.Text = "User ID:";
            // 
            // lbBranch
            // 
            this.lbBranch.AutoSize = true;
            this.lbBranch.Location = new System.Drawing.Point(15, 65);
            this.lbBranch.Name = "lbBranch";
            this.lbBranch.Size = new System.Drawing.Size(52, 16);
            this.lbBranch.TabIndex = 20;
            this.lbBranch.Text = "Branch:";
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 451);
            this.Controls.Add(this.grbuser);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grblistuser);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmUser";
            this.Text = "User informations";
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUser_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmUser_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownPress);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.grblistuser, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.grbuser, 0);
            this.grblistuser.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbuser.ResumeLayout(false);
            this.grbuser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grblistuser;
        private System.Windows.Forms.TreeView trvuser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox grbuser;
        private System.Windows.Forms.TextBox txtuserid;
        private System.Windows.Forms.ComboBox cbbranch;
        private System.Windows.Forms.Label lbdescription;
        private System.Windows.Forms.Label lbpassworddate;
        private System.Windows.Forms.Label lbstatus;
        private System.Windows.Forms.Label lbemail;
        private System.Windows.Forms.Label lblastchange;
        private System.Windows.Forms.Label lbpassword;
        private System.Windows.Forms.Label lbmobile;
        private System.Windows.Forms.Label lblasttransdate;
        private System.Windows.Forms.Label lbusername;
        private System.Windows.Forms.Label lbconfimpass;
        private System.Windows.Forms.Label lbuserid;
        private System.Windows.Forms.Label lbBranch;
        private System.Windows.Forms.DateTimePicker dtlastchange;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txtmobile;
        private System.Windows.Forms.TextBox txtconfimpass;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.DateTimePicker dtlasttrans;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.ComboBox cbstatus;
        private System.Windows.Forms.TextBox txtdescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbEdit;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbClose;
        private System.Windows.Forms.ToolStripButton tlbCancel;



    }
}