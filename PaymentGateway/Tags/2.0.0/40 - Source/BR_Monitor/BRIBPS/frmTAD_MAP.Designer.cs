namespace BR.BRIBPS
{
    partial class frmTAD_MAP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTAD_MAP));
            this.dtgTAD = new System.Windows.Forms.DataGridView();
            this.SIBS_BANK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GW_BANK_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BANK_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgTADMAP = new System.Windows.Forms.DataGridView();
            this.SIBS_BANK_CODE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GW_BANK_CODE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BANK_NAME1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdAddAll = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdRemoAll = new System.Windows.Forms.Button();
            this.cboTADHO = new System.Windows.Forms.ComboBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTAD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTADMAP)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgTAD
            // 
            this.dtgTAD.AllowUserToAddRows = false;
            this.dtgTAD.ColumnHeadersHeight = 21;
            this.dtgTAD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgTAD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SIBS_BANK_CODE,
            this.GW_BANK_CODE,
            this.BANK_NAME});
            this.dtgTAD.Location = new System.Drawing.Point(8, 20);
            this.dtgTAD.Name = "dtgTAD";
            this.dtgTAD.RowHeadersWidth = 21;
            this.dtgTAD.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgTAD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgTAD.Size = new System.Drawing.Size(332, 329);
            this.dtgTAD.TabIndex = 1;
            this.dtgTAD.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgTAD_ColumnHeaderMouseClick);
            this.dtgTAD.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgTAD_CellClick);
            this.dtgTAD.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgTAD_CellEnter);
            // 
            // SIBS_BANK_CODE
            // 
            this.SIBS_BANK_CODE.HeaderText = "SIBS BANK CODE";
            this.SIBS_BANK_CODE.Name = "SIBS_BANK_CODE";
            this.SIBS_BANK_CODE.Width = 130;
            // 
            // GW_BANK_CODE
            // 
            this.GW_BANK_CODE.HeaderText = "GW BANK CODE";
            this.GW_BANK_CODE.Name = "GW_BANK_CODE";
            this.GW_BANK_CODE.Width = 130;
            // 
            // BANK_NAME
            // 
            this.BANK_NAME.HeaderText = "BANK NAME";
            this.BANK_NAME.Name = "BANK_NAME";
            this.BANK_NAME.Width = 200;
            // 
            // dtgTADMAP
            // 
            this.dtgTADMAP.AllowUserToAddRows = false;
            this.dtgTADMAP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgTADMAP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SIBS_BANK_CODE1,
            this.GW_BANK_CODE1,
            this.BANK_NAME1});
            this.dtgTADMAP.Location = new System.Drawing.Point(6, 21);
            this.dtgTADMAP.Name = "dtgTADMAP";
            this.dtgTADMAP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgTADMAP.Size = new System.Drawing.Size(321, 321);
            this.dtgTADMAP.TabIndex = 6;
            this.dtgTADMAP.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgTADMAP_ColumnHeaderMouseClick);
            this.dtgTADMAP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgTADMAP_CellClick);
            this.dtgTADMAP.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgTADMAP_CellEnter);
            this.dtgTADMAP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgTADMAP_CellContentClick);
            // 
            // SIBS_BANK_CODE1
            // 
            this.SIBS_BANK_CODE1.HeaderText = "SIBS BANK CODE";
            this.SIBS_BANK_CODE1.Name = "SIBS_BANK_CODE1";
            this.SIBS_BANK_CODE1.Width = 130;
            // 
            // GW_BANK_CODE1
            // 
            this.GW_BANK_CODE1.HeaderText = "GW BANK CODE";
            this.GW_BANK_CODE1.Name = "GW_BANK_CODE1";
            this.GW_BANK_CODE1.Width = 130;
            // 
            // BANK_NAME1
            // 
            this.BANK_NAME1.HeaderText = "BANK NAME";
            this.BANK_NAME1.Name = "BANK_NAME1";
            this.BANK_NAME1.Width = 200;
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(657, 389);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(354, 113);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(39, 37);
            this.cmdAdd.TabIndex = 2;
            this.cmdAdd.Text = "<";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdAddAll
            // 
            this.cmdAddAll.Location = new System.Drawing.Point(354, 156);
            this.cmdAddAll.Name = "cmdAddAll";
            this.cmdAddAll.Size = new System.Drawing.Size(39, 37);
            this.cmdAddAll.TabIndex = 3;
            this.cmdAddAll.Text = "<<";
            this.cmdAddAll.UseVisualStyleBackColor = true;
            this.cmdAddAll.Click += new System.EventHandler(this.cmdAddAll_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(354, 199);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(39, 37);
            this.cmdRemove.TabIndex = 4;
            this.cmdRemove.Text = ">";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdRemoAll
            // 
            this.cmdRemoAll.Location = new System.Drawing.Point(354, 242);
            this.cmdRemoAll.Name = "cmdRemoAll";
            this.cmdRemoAll.Size = new System.Drawing.Size(39, 37);
            this.cmdRemoAll.TabIndex = 5;
            this.cmdRemoAll.Text = ">>";
            this.cmdRemoAll.UseVisualStyleBackColor = true;
            this.cmdRemoAll.Click += new System.EventHandler(this.cmdRemoAll_Click);
            // 
            // cboTADHO
            // 
            this.cboTADHO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTADHO.FormattingEnabled = true;
            this.cboTADHO.Location = new System.Drawing.Point(64, 12);
            this.cboTADHO.Name = "cboTADHO";
            this.cboTADHO.Size = new System.Drawing.Size(275, 21);
            this.cboTADHO.TabIndex = 0;
            this.cboTADHO.SelectedIndexChanged += new System.EventHandler(this.cboTADHO_SelectedIndexChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(573, 389);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(80, 30);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgTADMAP);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 356);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tad Map";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgTAD);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(399, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 355);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tad List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "TAD :";
            // 
            // frmTAD_MAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(761, 422);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cboTADHO);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdRemoAll);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdAddAll);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTAD_MAP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tad Map";
            this.Load += new System.EventHandler(this.frmTAD_MAP_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTAD_MAP_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTAD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTADMAP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgTAD;
        private System.Windows.Forms.DataGridView dtgTADMAP;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdAddAll;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdRemoAll;
        private System.Windows.Forms.ComboBox cboTADHO;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIBS_BANK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GW_BANK_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BANK_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIBS_BANK_CODE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GW_BANK_CODE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BANK_NAME1;
    }
}