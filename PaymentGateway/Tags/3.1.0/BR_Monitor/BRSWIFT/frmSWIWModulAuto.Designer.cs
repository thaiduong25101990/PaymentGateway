namespace BR.BRSWIFT
{
    partial class frmSWIWModulAuto
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
            this.cboSubFunction = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmdValue = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboOperation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKeyword = new System.Windows.Forms.ComboBox();
            this.cboFunction = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdClearCriteria = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdClear1 = new System.Windows.Forms.Button();
            this.cboExpression = new System.Windows.Forms.ComboBox();
            this.txtCriteria = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.txtCriteriaMess = new System.Windows.Forms.TextBox();
            this.cmdValidate = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboModule = new System.Windows.Forms.ComboBox();
            this.txtPriority = new System.Windows.Forms.TextBox();
            this.txtCriteriaName = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.ttNote = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboSubFunction);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Controls.Add(this.cmdValue);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboOperation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboKeyword);
            this.groupBox1.Controls.Add(this.cboFunction);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(801, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // cboSubFunction
            // 
            this.cboSubFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubFunction.FormattingEnabled = true;
            this.cboSubFunction.Location = new System.Drawing.Point(149, 44);
            this.cboSubFunction.Name = "cboSubFunction";
            this.cboSubFunction.Size = new System.Drawing.Size(168, 24);
            this.cboSubFunction.TabIndex = 1;
            this.cboSubFunction.SelectedIndexChanged += new System.EventHandler(this.cboSubFunction_SelectedIndexChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(549, 45);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(196, 22);
            this.txtValue.TabIndex = 4;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // cmdValue
            // 
            this.cmdValue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdValue.Location = new System.Drawing.Point(752, 43);
            this.cmdValue.Margin = new System.Windows.Forms.Padding(4);
            this.cmdValue.Name = "cmdValue";
            this.cmdValue.Size = new System.Drawing.Size(40, 25);
            this.cmdValue.TabIndex = 10;
            this.cmdValue.Text = "...";
            this.ttNote.SetToolTip(this.cmdValue, "Value definition");
            this.cmdValue.UseVisualStyleBackColor = true;
            this.cmdValue.Click += new System.EventHandler(this.cmdValue_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(547, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(433, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Operation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Keyword";
            // 
            // cboOperation
            // 
            this.cboOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperation.FormattingEnabled = true;
            this.cboOperation.Location = new System.Drawing.Point(436, 44);
            this.cboOperation.Margin = new System.Windows.Forms.Padding(4);
            this.cboOperation.Name = "cboOperation";
            this.cboOperation.Size = new System.Drawing.Size(106, 24);
            this.cboOperation.TabIndex = 3;
            this.cboOperation.SelectedIndexChanged += new System.EventHandler(this.cboOperation_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Combine function";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Function";
            // 
            // cboKeyword
            // 
            this.cboKeyword.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKeyword.FormattingEnabled = true;
            this.cboKeyword.Location = new System.Drawing.Point(324, 44);
            this.cboKeyword.Margin = new System.Windows.Forms.Padding(4);
            this.cboKeyword.Name = "cboKeyword";
            this.cboKeyword.Size = new System.Drawing.Size(104, 24);
            this.cboKeyword.TabIndex = 2;
            this.cboKeyword.SelectedIndexChanged += new System.EventHandler(this.cboKeyword_SelectedIndexChanged);
            // 
            // cboFunction
            // 
            this.cboFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFunction.FormattingEnabled = true;
            this.cboFunction.Location = new System.Drawing.Point(8, 44);
            this.cboFunction.Margin = new System.Windows.Forms.Padding(4);
            this.cboFunction.Name = "cboFunction";
            this.cboFunction.Size = new System.Drawing.Size(134, 24);
            this.cboFunction.TabIndex = 0;
            this.cboFunction.SelectedIndexChanged += new System.EventHandler(this.cboFunction_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdClearCriteria);
            this.groupBox2.Controls.Add(this.cmdAdd);
            this.groupBox2.Controls.Add(this.cmdClear1);
            this.groupBox2.Controls.Add(this.cboExpression);
            this.groupBox2.Controls.Add(this.txtCriteria);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(8, 102);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(801, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Function description";
            // 
            // cmdClearCriteria
            // 
            this.cmdClearCriteria.Location = new System.Drawing.Point(736, 52);
            this.cmdClearCriteria.Name = "cmdClearCriteria";
            this.cmdClearCriteria.Size = new System.Drawing.Size(53, 28);
            this.cmdClearCriteria.TabIndex = 9;
            this.cmdClearCriteria.Text = "&Clear";
            this.cmdClearCriteria.UseVisualStyleBackColor = true;
            this.cmdClearCriteria.Click += new System.EventHandler(this.cmdClearCriteria_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(736, 19);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(53, 28);
            this.cmdAdd.TabIndex = 8;
            this.cmdAdd.Text = ">>";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdClear1
            // 
            this.cmdClear1.Location = new System.Drawing.Point(921, 21);
            this.cmdClear1.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClear1.Name = "cmdClear1";
            this.cmdClear1.Size = new System.Drawing.Size(84, 28);
            this.cmdClear1.TabIndex = 7;
            this.cmdClear1.Text = "Clear";
            this.cmdClear1.UseVisualStyleBackColor = true;
            this.cmdClear1.Click += new System.EventHandler(this.cmdClear1_Click);
            // 
            // cboExpression
            // 
            this.cboExpression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExpression.FormattingEnabled = true;
            this.cboExpression.Location = new System.Drawing.Point(104, 56);
            this.cboExpression.Margin = new System.Windows.Forms.Padding(4);
            this.cboExpression.Name = "cboExpression";
            this.cboExpression.Size = new System.Drawing.Size(170, 24);
            this.cboExpression.TabIndex = 6;
            this.cboExpression.SelectedIndexChanged += new System.EventHandler(this.cboExpression_SelectedIndexChanged);
            // 
            // txtCriteria
            // 
            this.txtCriteria.BackColor = System.Drawing.Color.White;
            this.txtCriteria.Location = new System.Drawing.Point(104, 19);
            this.txtCriteria.Margin = new System.Windows.Forms.Padding(4);
            this.txtCriteria.Multiline = true;
            this.txtCriteria.Name = "txtCriteria";
            this.txtCriteria.ReadOnly = true;
            this.txtCriteria.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCriteria.Size = new System.Drawing.Size(624, 29);
            this.txtCriteria.TabIndex = 5;
            this.txtCriteria.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 59);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Expression:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Criteria :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtKeyword);
            this.groupBox3.Controls.Add(this.txtCriteriaMess);
            this.groupBox3.Controls.Add(this.cmdValidate);
            this.groupBox3.Controls.Add(this.cmdClear);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cboModule);
            this.groupBox3.Controls.Add(this.txtPriority);
            this.groupBox3.Controls.Add(this.txtCriteriaName);
            this.groupBox3.Location = new System.Drawing.Point(8, 196);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(801, 181);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 85);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 16);
            this.label12.TabIndex = 16;
            this.label12.Text = "Keyword";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(9, 105);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(346, 22);
            this.txtKeyword.TabIndex = 15;
            // 
            // txtCriteriaMess
            // 
            this.txtCriteriaMess.Location = new System.Drawing.Point(376, 44);
            this.txtCriteriaMess.Margin = new System.Windows.Forms.Padding(4);
            this.txtCriteriaMess.Multiline = true;
            this.txtCriteriaMess.Name = "txtCriteriaMess";
            this.txtCriteriaMess.ReadOnly = true;
            this.txtCriteriaMess.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCriteriaMess.Size = new System.Drawing.Size(336, 127);
            this.txtCriteriaMess.TabIndex = 14;
            // 
            // cmdValidate
            // 
            this.cmdValidate.Location = new System.Drawing.Point(720, 81);
            this.cmdValidate.Margin = new System.Windows.Forms.Padding(4);
            this.cmdValidate.Name = "cmdValidate";
            this.cmdValidate.Size = new System.Drawing.Size(69, 28);
            this.cmdValidate.TabIndex = 13;
            this.cmdValidate.Text = "&Validate";
            this.cmdValidate.UseVisualStyleBackColor = true;
            this.cmdValidate.Click += new System.EventHandler(this.cmdValidate_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(720, 44);
            this.cmdClear.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(69, 28);
            this.cmdClear.TabIndex = 12;
            this.cmdClear.Text = "&Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(376, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "Criteria message";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(225, 24);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "Module";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 24);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Priority";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Criteria name";
            // 
            // cboModule
            // 
            this.cboModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModule.FormattingEnabled = true;
            this.cboModule.Location = new System.Drawing.Point(227, 44);
            this.cboModule.Margin = new System.Windows.Forms.Padding(4);
            this.cboModule.Name = "cboModule";
            this.cboModule.Size = new System.Drawing.Size(129, 24);
            this.cboModule.TabIndex = 11;
            // 
            // txtPriority
            // 
            this.txtPriority.Location = new System.Drawing.Point(130, 44);
            this.txtPriority.Margin = new System.Windows.Forms.Padding(4);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.Size = new System.Drawing.Size(89, 22);
            this.txtPriority.TabIndex = 10;
            this.txtPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPriority_KeyPress);
            // 
            // txtCriteriaName
            // 
            this.txtCriteriaName.Location = new System.Drawing.Point(9, 44);
            this.txtCriteriaName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCriteriaName.Name = "txtCriteriaName";
            this.txtCriteriaName.Size = new System.Drawing.Size(113, 22);
            this.txtCriteriaName.TabIndex = 9;
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(712, 386);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 30);
            this.cmdClose.TabIndex = 15;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(623, 386);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(80, 30);
            this.cmdSave.TabIndex = 14;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // ttNote
            // 
            this.ttNote.AutoPopDelay = 5000;
            this.ttNote.InitialDelay = 100;
            this.ttNote.ReshowDelay = 10;
            this.ttNote.ShowAlways = true;
            this.ttNote.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // frmSWIWModulAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(818, 422);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmSWIWModulAuto";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Swift - Auto message by module criteria definition";
            this.Load += new System.EventHandler(this.frmSWIWModulAuto_Load_1);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmSWIWModulAuto_KeyPress);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSWIWModulAuto_MouseMove);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOperation;
        private System.Windows.Forms.ComboBox cboKeyword;
        private System.Windows.Forms.ComboBox cboFunction;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdClear1;
        private System.Windows.Forms.ComboBox cboExpression;
        private System.Windows.Forms.TextBox txtCriteria;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboModule;
        private System.Windows.Forms.TextBox txtPriority;
        private System.Windows.Forms.TextBox txtCriteriaName;
        private System.Windows.Forms.Button cmdValidate;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCriteriaMess;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button cmdClearCriteria;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cboSubFunction;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolTip ttNote;
    }
}