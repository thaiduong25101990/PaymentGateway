using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using utilities;
using BR.BRLib;
using BR.BRSYSTEM;
using BR.BRBusinessObject;
using System.IO;
using System.Text.RegularExpressions;

namespace BR.BRSWIFT
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmSwiftImp : frmBasedata
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
        /// 
        #region khai bao cac control
        private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel pnlCell;
		private System.Windows.Forms.Label lblCellOperatorIs;
		private System.Windows.Forms.Label lblGetValueData;
		private System.Windows.Forms.TextBox txtFilePath;
        public DataTable datGet = new DataTable();
		private DataTable _dt;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtRange;
		private System.Windows.Forms.TextBox txtPK;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSetPK;
		private System.Windows.Forms.Label lblCurrentPK;
		private System.Windows.Forms.Button btnOpenFileDlg;
        private GetData objGetData = new GetData();
        #endregion

        #region bien va ham
        EXCELInfo objExcel = new EXCELInfo();
        EXCELController objctrolExcel = new EXCELController();
        ALLCODEInfo objAllcode = new ALLCODEInfo();
        ALLCODEController objCtrolall = new ALLCODEController();
        SWIFT_BR_AUTOInfo ObjAuto = new SWIFT_BR_AUTOInfo();
        SWIFT_BR_AUTOController objCtrlauto = new SWIFT_BR_AUTOController();
        SWIFT_RMBR_AUTOInfo objRM = new SWIFT_RMBR_AUTOInfo();
        SWIFT_RMBR_AUTOController objCtrlRM = new SWIFT_RMBR_AUTOController();
        MSG_XLSInfo objMSG = new MSG_XLSInfo();
        MSG_XLSController objctrlMSG = new MSG_XLSController();
        USERSInfo objUser = new USERSInfo();
        USERSController ObjctrlUser = new USERSController();
        USER_MSG_LOGInfo objuser_msg_log = new USER_MSG_LOGInfo();
        USER_MSG_LOGController objcontroluser_msg_log = new USER_MSG_LOGController();
        private bool iColumns = false;
        private string strColumns_Currency;
        private string strColumns_Amount;        
        private string strGui;
        private string strNhan;
        private string strF20;      
        //----------------------------------------------------------------------------
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        string[] Excel_Colums = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string [] Excel = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","" };
        string[] Br_auto = { "", "", "" };
        string[] RMBr_auto = { "", "", "" };
        private int iRowscount;
        private int iSupervisor;
        private int iAmount = 0;
        private int iCcycd = 0;
        public string direction;
        private string Cells_columns;
        private static int iSave;
        private int iError;
        public int iSelect;
        public bool check=false ;
        private int iDelete;
        private int iExisted;
        private string pMSG_DIREC;
        #endregion

        #region variables
        private string _strExcelFilename = "";
		private System.Windows.Forms.Button btnGetData;
		private ExcelReader _exr=null;
        private System.Windows.Forms.Button btnSaveData;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.Label lblSheet;
		private System.Windows.Forms.ComboBox cboSheetnames;
		private System.Windows.Forms.TextBox txtCell;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox1;
        private Label lbType;
        private ComboBox cbType;
        private Button button2;
        private Button button1;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridView dataGrid1;
        private DataGridViewCheckBoxColumn Column1;
        private Label label2;
		private int _intPKCol=-1;
        private Button cmdPrint;
        private int iTong;
        public int Columns;//bien de quy dinh so cot cua file
        //private int iFormat;
		#endregion

		#region CTOR
        public frmSwiftImp()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
#endregion
		#region DTOR
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSwiftImp));
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.pnlCell = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCellOperatorIs = new System.Windows.Forms.Label();
            this.lblCurrentPK = new System.Windows.Forms.Label();
            this.lblGetValueData = new System.Windows.Forms.Label();
            this.txtCell = new System.Windows.Forms.TextBox();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.cboSheetnames = new System.Windows.Forms.ComboBox();
            this.lblSheet = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPK = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetPK = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.btnOpenFileDlg = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbType = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.pnlCell.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(775, 468);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(334, 544);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(248, 544);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(73, 544);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(159, 544);
            // 
            // btnGetData
            // 
            this.btnGetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetData.Location = new System.Drawing.Point(751, 44);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(80, 30);
            this.btnGetData.TabIndex = 4;
            this.btnGetData.Text = "&Import";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveData.Location = new System.Drawing.Point(692, 468);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(80, 30);
            this.btnSaveData.TabIndex = 8;
            this.btnSaveData.Text = "&Save";
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // pnlCell
            // 
            this.pnlCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlCell.Controls.Add(this.label1);
            this.pnlCell.Controls.Add(this.lblCellOperatorIs);
            this.pnlCell.Controls.Add(this.lblCurrentPK);
            this.pnlCell.Controls.Add(this.lblGetValueData);
            this.pnlCell.Controls.Add(this.txtCell);
            this.pnlCell.Controls.Add(this.txtRange);
            this.pnlCell.Controls.Add(this.cboSheetnames);
            this.pnlCell.Controls.Add(this.lblSheet);
            this.pnlCell.Controls.Add(this.label3);
            this.pnlCell.Controls.Add(this.txtPK);
            this.pnlCell.Controls.Add(this.label4);
            this.pnlCell.Controls.Add(this.btnSetPK);
            this.pnlCell.Location = new System.Drawing.Point(65, 183);
            this.pnlCell.Name = "pnlCell";
            this.pnlCell.Size = new System.Drawing.Size(662, 167);
            this.pnlCell.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Cell";
            // 
            // lblCellOperatorIs
            // 
            this.lblCellOperatorIs.Location = new System.Drawing.Point(144, 8);
            this.lblCellOperatorIs.Name = "lblCellOperatorIs";
            this.lblCellOperatorIs.Size = new System.Drawing.Size(8, 12);
            this.lblCellOperatorIs.TabIndex = 7;
            this.lblCellOperatorIs.Text = "=";
            // 
            // lblCurrentPK
            // 
            this.lblCurrentPK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentPK.Location = new System.Drawing.Point(362, 8);
            this.lblCurrentPK.Name = "lblCurrentPK";
            this.lblCurrentPK.Size = new System.Drawing.Size(72, 24);
            this.lblCurrentPK.TabIndex = 17;
            // 
            // lblGetValueData
            // 
            this.lblGetValueData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGetValueData.Location = new System.Drawing.Point(158, 10);
            this.lblGetValueData.Name = "lblGetValueData";
            this.lblGetValueData.Size = new System.Drawing.Size(144, 16);
            this.lblGetValueData.TabIndex = 5;
            // 
            // txtCell
            // 
            this.txtCell.Location = new System.Drawing.Point(77, 6);
            this.txtCell.MaxLength = 4;
            this.txtCell.Name = "txtCell";
            this.txtCell.Size = new System.Drawing.Size(48, 20);
            this.txtCell.TabIndex = 19;
            this.txtCell.Text = "A2";
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(302, 54);
            this.txtRange.MaxLength = 20;
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(72, 20);
            this.txtRange.TabIndex = 13;
            this.txtRange.Text = "A1:W500000";
            // 
            // cboSheetnames
            // 
            this.cboSheetnames.Location = new System.Drawing.Point(127, 54);
            this.cboSheetnames.Name = "cboSheetnames";
            this.cboSheetnames.Size = new System.Drawing.Size(121, 21);
            this.cboSheetnames.TabIndex = 18;
            // 
            // lblSheet
            // 
            this.lblSheet.Location = new System.Drawing.Point(63, 54);
            this.lblSheet.Name = "lblSheet";
            this.lblSheet.Size = new System.Drawing.Size(40, 23);
            this.lblSheet.TabIndex = 11;
            this.lblSheet.Text = "Sheet";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(254, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Range";
            // 
            // txtPK
            // 
            this.txtPK.Location = new System.Drawing.Point(467, 54);
            this.txtPK.MaxLength = 3;
            this.txtPK.Name = "txtPK";
            this.txtPK.Size = new System.Drawing.Size(47, 20);
            this.txtPK.TabIndex = 14;
            this.txtPK.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(385, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Prim. Key Col";
            // 
            // btnSetPK
            // 
            this.btnSetPK.Location = new System.Drawing.Point(523, 54);
            this.btnSetPK.Name = "btnSetPK";
            this.btnSetPK.Size = new System.Drawing.Size(76, 24);
            this.btnSetPK.TabIndex = 16;
            this.btnSetPK.Text = "Set PK";
            this.btnSetPK.Click += new System.EventHandler(this.btnSetPK_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(129, 51);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtFilePath.Size = new System.Drawing.Size(566, 23);
            this.txtFilePath.TabIndex = 2;
            // 
            // lblFilename
            // 
            this.lblFilename.Location = new System.Drawing.Point(44, 54);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(74, 16);
            this.lblFilename.TabIndex = 8;
            this.lblFilename.Text = "File path :";
            // 
            // btnOpenFileDlg
            // 
            this.btnOpenFileDlg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenFileDlg.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileDlg.Image")));
            this.btnOpenFileDlg.Location = new System.Drawing.Point(701, 49);
            this.btnOpenFileDlg.Name = "btnOpenFileDlg";
            this.btnOpenFileDlg.Size = new System.Drawing.Size(35, 25);
            this.btnOpenFileDlg.TabIndex = 3;
            this.btnOpenFileDlg.Text = "....";
            this.btnOpenFileDlg.Click += new System.EventHandler(this.btnOpenFileDlg_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbType);
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.lblFilename);
            this.groupBox1.Controls.Add(this.btnOpenFileDlg);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import file excel";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbType.Location = new System.Drawing.Point(44, 25);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(45, 16);
            this.lbType.TabIndex = 12;
            this.lbType.Text = "Type :";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(129, 21);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(210, 24);
            this.cbType.TabIndex = 1;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(609, 468);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "&Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(444, 468);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "&Approve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Column1";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.AllowUserToOrderColumns = true;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGrid1.Location = new System.Drawing.Point(8, 118);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersWidth = 30;
            this.dataGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid1.Size = new System.Drawing.Size(846, 344);
            this.dataGrid1.TabIndex = 230;
            this.dataGrid1.TabStop = false;
            this.dataGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGrid1_MouseMove);
            this.dataGrid1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid1_ColumnHeaderMouseClick);
            this.dataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellClick_1);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(5, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 16);
            this.label2.TabIndex = 231;
            this.label2.Text = "Total number of messages :";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Location = new System.Drawing.Point(526, 468);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(80, 30);
            this.cmdPrint.TabIndex = 5;
            this.cmdPrint.Text = "&Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // frmSwiftImp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(861, 508);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlCell);
            this.Controls.Add(this.btnSaveData);
            this.Name = "frmSwiftImp";
            this.Text = "Swift import file";
            this.Load += new System.EventHandler(this.frmReadWriteExcelDemo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSwiftImp_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSwiftImp_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSwiftImp_KeyDown);
            this.Controls.SetChildIndex(this.btnSaveData, 0);
            this.Controls.SetChildIndex(this.pnlCell, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmdPrint, 0);
            this.pnlCell.ResumeLayout(false);
            this.pnlCell.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmSwiftImp());
		}

		private void InitExcel(ref ExcelReader exr)
		{
			//Excel must be open
			if (exr == null)
			{
				exr = new ExcelReader();
				exr.ExcelFilename = _strExcelFilename;
				exr.Headers =false;
				exr.MixedData =true;
			}
			if  (_dt==null) _dt = new DataTable("par");			
			exr.KeepConnectionOpen =true;
			
			//Check excel sheetname is selected
			if (this.cboSheetnames.SelectedIndex>-1) 
				exr.SheetName = this.cboSheetnames.Text; 
			else
				throw new Exception("Select a sheet first!");

            //Set excel sheet range//A1:W1000// 
            exr.SheetRange = this.txtRange.Text;
			
		
		}

        //thong bao co hoi xoa hay khong
        private bool Messagebox()
        {
            bool iDelete = true;
            try
            {
                string Msg = "Do you want to Delete";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    return iDelete = true;
                }
                else
                {
                    return iDelete = false;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            return iDelete;
        }

		private void frmReadWriteExcelDemo_Load(object sender, System.EventArgs e)
		{
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                txtRange.Text = "A1:W50000";
                iSupervisor = 0;
                pnlCell.Enabled = false;
                cmdAdd.Visible = false;
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                cmdSave.Visible = false;
                cmdPrint.Enabled = false;
                iDelete = 0;
                label2.Text = "Total number of messages : 0";
                //----------------------------------------------------------------
                btnGetData.Enabled = false;
                btnSaveData.Enabled = false;
                button1.Enabled = false;                
                dataGrid1.Columns[0].HeaderCell = dgvColumnHeader;
                dataGrid1.Columns[0].HeaderText = "";
                //------------------------------------------------------------
                txtFilePath.Enabled = true;
                txtFilePath.ReadOnly = true;
                iInquiry = Common.iSelect;
                //--kiem tra xem la import tham so hay import file excel binh thuong
                if (Common.strSTATUS_IN == "Swift")//gionh thuong
                {
                    //Common.strSTATUS_IN == "Swift";
                }
                else if (Common.strSTATUS_IN == "Paramester")//Import tham so//cmdPrint.Location.X + 82
                {
                    cmdPrint.Location = new Point(cmdPrint.Location.X + cmdPrint.Width + 3, button1.Location.Y);
                    button1.Visible = false;
                }
                //------------------------------------------------------------                
                btnSaveData.Enabled = false;
                //this.textBox1.Text = Application.StartupPath + @"\Map1.xls";
                //_strExcelFilename = this.textBox1.Text;
                if (System.IO.File.Exists(this.txtFilePath.Text))
                    RetrieveSheetnames();
                
                //--------lay thong tin users vao form de kiem tra he thong-------
                string strUserID = Common.Userid;
                DataTable datUs = new DataTable();
                datUs = ObjctrlUser.GetRoll(strUserID.Trim(), Common.gGWTYPE);//issupervisor
                if (datUs.Rows.Count == 1)
                {
                    if (datUs.Rows[0]["issupervisor"].ToString().Trim() == "supervisor")
                    {
                        GetdatatExcel();
                        iSupervisor = 1;
                    }
                    else
                    {
                        iSupervisor = 0;
                    }
                }
                if (datUs.Rows.Count == 2)//co ca hai quyen Teller va Supervisor
                {
                    GetdatatExcel();
                    iSupervisor = 1;
                }
                Getcombobox();
            }
            catch
            {
            }
            if (cbType.Text.Trim() == "Inward" || cbType.Text.Trim() == "Outward")
            {
                dataGrid1.Columns["MSG_ID"].Visible = false;
            }
		}
        //---Kiem soat ReadOnly cua cac cot
        private void ColumnsRead(DataGridView Datagrid)
        {
            try
            {
                int b = 2;
                while (b < Datagrid.Columns.Count)
                {
                    Datagrid.Columns[b].ReadOnly = true;
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void GetdatatExcel()
        {
            try
            {
                //----goi ham lay thong tin quy dinh ao cot cua file excel lua chon
                DataTable datMsg = new DataTable();
                if (cbType.Text.Trim() == "Inward" )
                {
                    datMsg = objctrlMSG.MSG_XLS("SWIFT", "MT103","SWIFT-SIBS");
                }
                else if(cbType.Text.Trim() == "Outward")
                {
                    datMsg = objctrlMSG.MSG_XLS("SWIFT", "MT103", "SIBS-SWIFT");
                }
                //-----------------------------------------------------------------
                //DataTable datGet = new DataTable();
                datGet = objctrolExcel.GetEXCEL1(Common.Userid);
                if (datGet == null)
                {
                }
                else
                {
                    if (datGet.Rows.Count == 0)
                    {
                        dataGrid1.DataSource = datGet;
                        button1.Enabled = false;
                        button2.Enabled = false;
                        cmdPrint.Enabled = false;
                        
                    }
                    else
                    {
                        dataGrid1.DataSource = datGet;//day du lieu vao luoi
                        label2.Text = "Total number of messages : " + datGet.Rows.Count;
                        dataGrid1.Columns["F5"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                        ColumnsRead(dataGrid1);//goi ham de khong cho sua o cac cot
                        dataGrid1.Columns[0].Width = 30;
                        dataGrid1.Columns["MSG_ID"].Visible = false;
                        button1.Enabled = true;
                        button2.Enabled = true;
                        //cmdPrint.Enabled = true;
                        int k = 0;
                        while (k < datMsg.Rows.Count)
                        {
                            dataGrid1.Columns["F" + (k + 1)].HeaderText = datMsg.Rows[k]["FIELD"].ToString();
                            k = k + 1;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Getcombobox()
        {
            try
            {
                if (Common.strExcel == "2084")
                {
                    // Neu la import file Excel ---------------------------
                    ////string strCdname = "Type";
                    ////string strGưtype = "SWIFT";
                    ////DataSet datCombo = new DataSet();
                    ////datCombo = objCtrolall.GetALLCODE2(strCdname, strGưtype);
                    ////cbType.DataSource = datCombo.Tables[0];
                    ////cbType.DisplayMember = "CONTENT";
                    ////cbType.ValueMember = "CDVAL";
                    if (!objGetData.FillDataComboBox(cbType, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='SWIFT' AND CDNAME='Type'", "CONTENT", true, false, ""))
                        return;
                    //-------------------------------------------------------------
                }
                else
                {
                    //-----neu la import file tham so------------------------------
                    string strCdname1 = "PramType";
                    string strGưtype1 = "SWIFT";
                    DataSet datCombo1 = new DataSet();
                    datCombo1 = objCtrolall.GetALLCODE21(strCdname1, strGưtype1);
                    if (iSupervisor == 1)
                    {
                        cbType.DataSource = datCombo1.Tables[0];
                        cbType.DisplayMember = "CONTENT";
                        cbType.ValueMember = "CDVAL";
                        cbType.SelectedIndex = 0;                       
                    }
                    else if (iSupervisor == 0)
                    {
                        cbType.Items.Add("TFBr-Parameter");
                        cbType.SelectedIndex = 0;
                    }
                }
                //-------------------------------------------------------------              
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
		private void RetrieveSheetnames()
		{
			try
			{
				this.cboSheetnames.Items.Clear();
			
				if (_exr !=null)
				{
					_exr.Dispose();
					_exr=null;
				}
				
				_exr = new ExcelReader();
				_exr.ExcelFilename = _strExcelFilename;
				_exr.Headers =false;
				_exr.MixedData =true;
				string[] sheetnames = this._exr.GetExcelSheetNames();
				this.cboSheetnames.Items.AddRange(sheetnames); 
			}
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
		}


		

		private bool IsInt(string strNr)
		{
			try
			{
				int intNr= int.Parse(strNr);
				return true;

			}
			catch
			{
				return false;
			}
		}

		private void SetPK()
		{
			Cursor = Cursors.WaitCursor; 
			_intPKCol=-1;
			try
			{
				if (txtPK.Text.Length >0) 
				{
					if (IsInt(txtPK.Text)) 
						_intPKCol=Convert.ToInt32(txtPK.Text) ;
					else
					{
						if (_dt.Columns.Contains(txtPK.Text))
						{
							_intPKCol = _dt.Columns[txtPK.Text].Ordinal; 
						}
						else
						{
                            throw new Exception("Columnname is not present in the table.!");						
						}
					}
					if (_dt.Columns.Count<=_intPKCol)
					{
						_intPKCol=-1;
						Cursor = Cursors.Default;
						throw new Exception("Column does not exists!");
					}
				}
				Cursor = Cursors.Default;
			}
			
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
			}
		}

		private void btnSetPK_Click(object sender, System.EventArgs e)
		{
			SetPK();
		}

		private void btnOpenFileDlg_Click(object sender, System.EventArgs e)
		{
          
            
			OpenFileDialog f = new OpenFileDialog(); 
			//f.Filter ="Excel files | *.xls";
            //if (cbType.Text == "TEXT")
            //{
            //    if (f.ShowDialog() == DialogResult.OK && f.FileName != "")
            //        textBox1.Text = f.FileName;
            //}
            //else
            //{
                //f.InitialDirectory = Application.ExecutablePath;

                if (f.ShowDialog() == DialogResult.OK)
                    if (f.FileName != null && f.CheckFileExists == true)
                    {
                        this._strExcelFilename = f.FileName;
                        this.txtFilePath.Text = f.FileName;
                        RetrieveSheetnames();
                        if (this.cboSheetnames.Items.Count > 0)
                            cboSheetnames.SelectedIndex = 0;
                    }
                if (txtFilePath.Text.Trim() == "")
                {
                    btnGetData.Enabled = false;
                }
                else
                {
                    btnGetData.Enabled = true;  
                }
            //}
		}
        private void Columns_Header()
        {
            try
            {
                int t = 1;
                while (t < dataGrid1.Columns.Count)
                {
                    dataGrid1.Columns[t].HeaderText = "F" + Convert.ToString(t);
                    dataGrid1.Columns[t].Name = "F" + Convert.ToString(t);
                    t = t + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
		private void btnGetData_Click(object sender, System.EventArgs e)
		{
            try
            {
                direction = cbType.Text.ToString().Trim();//DatHM add   
                check = true;
                //---------------------------------------------------------------
                int g = 0;
                while (g < 17)
                {
                    Excel[g] = "";
                    Excel_Colums[g] = "";
                    g = g + 1;
                }
                //---------------------------------------------------------------
                button1.Enabled = false;
                iDelete = 1;
                //----goi ham lay thong tin quy dinh ao cot cua file excel lua chon
                DataTable datMsg = new DataTable();
                if (cbType.Text.Trim() == "Inward" )
                {
                    datMsg = objctrlMSG.MSG_XLS("SWIFT", "MT103","SWIFT-SIBS");
                }
                else if (cbType.Text.Trim() == "Outward")
                {
                    datMsg = objctrlMSG.MSG_XLS("SWIFT", "MT103","SIBS-SWIFT");
                }
                Columns = datMsg.Rows.Count;//lay ra so cot quy dinh cho tung loai file Excel
                //if (Columns != 0)
                //{                   
                //    _iColumns = Columns;
                //}
                //-----------------Lay du lieu voi truong CHK de do vao mang------------------
                int h = 0;
                while (h < Columns)
                {
                    Excel_Colums[h + 1] = datMsg.Rows[h]["CHK"].ToString();
                    h = h + 1;
                }
                //----------------------------------------------------------------------------
                Cursor = Cursors.WaitCursor;
                InitExcel(ref _exr);
                _dt = _exr.GetTable(Columns);
               
                //this.lblGetValueData.Text = _exr.GetValue(txtCell.Text).ToString();
                _exr.GetValue(txtCell.Text);
                if (cbType.Text.Trim() == "Inward" || cbType.Text.Trim() == "Outward")//neu chon loai MT103
                {
                    if (_dt.Columns.Count >= Columns)//so cot trong file Excel nho hon so cot quy dinh bao loi
                    {
                        //---doan code lay so cot dung dinh dang khi file co so cot lon hon dinh dang
                        int m = Columns;
                        while (m < _dt.Columns.Count)
                        {
                            _dt.Columns.RemoveAt(m);
                        }
                        //---------------------------------------------------------------------------
                        //----------day du lieu vao trong datagrid------------------------------------
                        if (_dt.Columns.Count <= 17)
                        {
                            this.dataGrid1.DataSource = _dt;
                            label2.Text = "Total number of messages : " + _dt.Rows.Count;
                            //--gan nhan check all vao he thong------------------------   
                            ColumnsRead(dataGrid1);
                            Columns_Header();
                            //dataGrid1.Columns["F5"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                            dataGrid1.Columns[0].ReadOnly = false;
                            dataGrid1.Columns[0].Width = 30;
                            dataGrid1.AllowUserToDeleteRows = true;
                            if (dataGrid1.Rows.Count == 0)
                            {
                                button2.Enabled = false;
                                cmdPrint.Enabled = false;
                            }
                            else
                            {
                                button2.Enabled = true;
                                cmdPrint.Enabled = true;
                            }
                            //----------goi ham fin ten cua header len luoi---------------
                            int k = 0;
                            while (k < datMsg.Rows.Count)
                            {
                                dataGrid1.Columns["F" + (k + 1)].HeaderText = datMsg.Rows[k]["FIELD"].ToString();
                                k = k + 1;
                            }
                            //-------------------------------------------------------------
                        }
                        else
                        {
                            Common.ShowError("The format of excel file is not correct!", 3, MessageBoxButtons.OK);                            
                        }
                        //------------------------------------------------------------------------------------
                    }
                    else if (_dt.Columns.Count < Columns) //neu khong dung dinh dang
                    {
                        //thong bao file khong dung dinh dang
                        Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);                        
                    }
                }
                else//neu chon Br_auto thi kiem tra
                {
                    if (_dt.Columns.Count == 2)
                    {
                        this.dataGrid1.DataSource = _dt;
                        label2.Text = "Total number of messages : " + _dt.Rows.Count;
                        //--gan nhan check all vao he thong------------------------   
                        ColumnsRead(dataGrid1);
                        dataGrid1.Columns[0].ReadOnly = false;
                        dataGrid1.Columns[0].Width = 27;
                        if (dataGrid1.Rows.Count == 0)
                        {
                            button2.Enabled = false;
                        }
                        else
                        {
                            button2.Enabled = true;
                        }
                        //---------------------------------------------------------
                    }
                    else
                    {
                        Common.ShowError("The format of excel file is not correct!", 3, MessageBoxButtons.OK);                         
                    }
                }
                
                //this.dataGrid1.ReadOnly = true;
                _exr.Close();
                _exr.Dispose();
                _exr = null;
                Cursor = Cursors.Default;
                //if (_dt !=null && this.txtPK.Text.Length >0) SetPK();
                if (dataGrid1.Rows.Count != 0)
                {
                    btnSaveData.Enabled = true;
                    iSave = 1;
                }
                else
                {
                    btnSaveData.Enabled = false;
                    iSave = 0;
                }
                //}
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK); 
            }
            if (Common.strExcel == "2084")//import Excel
            {
                if (dataGrid1.Rows.Count == 0)
                {
                    cmdPrint.Enabled = false;
                    button1.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdPrint.Enabled = true;
                    button1.Enabled = true;
                    cmdDelete.Enabled = true;
                    cmdSave.Enabled = true;
                }
            }
            else
            {
                if (dataGrid1.Rows.Count == 0)
                {
                    cmdPrint.Enabled = false;
                    button1.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdSave.Enabled = false;
                }
                else
                {
                    cmdPrint.Enabled = true;
                    button1.Enabled = false;
                    cmdDelete.Enabled = true;
                    cmdSave.Enabled = true;
                }
            }
		
        }
        //ham kiem tra xem chon mot hang hay nhieu hang
        private void Checked_Els()
        {
            try
            {
                int n = 0;
                while (n < dataGrid1.Rows.Count)
                {
                    if (dataGrid1.Rows[n].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGrid1.Rows[n].Cells[0].Value.ToString() == "True")
                        {
                            iRowscount = iRowscount + 1;//so dem de neu lon hon mot thi hien thi thong bao kieu form
                        }
                    }
                   n = n + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void SaveBrauto()//ham day du lieu vao bang SWIFT_BR_AUTO
        {
            string strWhere;
            string strtmtref;// string strtmprbr;
            try
            {
                if (dataGrid1.Rows.Count != 0)//neu luoi ko co du lieu
                {
                    if (dataGrid1.Columns.Count != 3)
                    {
                        MessageBox.Show("");//thong bao dinh dang file khong dung
                    }
                    else
                    {
                        iRowscount = 0;
                        Checked_Els();//kiem tra so hang duoc chon
                        if (iRowscount == 1)
                        {
                            int f = 0;
                            while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                            {
                                if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                                    {
                                        int k = 1;
                                        while (k < dataGrid1.Columns.Count)//duyet tung cot
                                        {
                                            Br_auto[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                            k = k + 1;
                                        }
                                        //----------------Xoa cac dong da insert-----------------------
                                                                          
                                        //------------------------------------------------------------
                                        ObjAuto.TMTREF = Br_auto[1];
                                        ObjAuto.TMPRBR = Br_auto[2];
                                        //---kiem tra xem co bi trung du lieu khong                                        
                                        if (Br_auto[1] == "") { strtmtref = ""; } else { strtmtref = "  Trim(tmtref) ='" + Br_auto[1].Trim() + "'"; }
                                        //if (Br_auto[2] == "") { strtmprbr = ""; } else { strtmprbr = " and  Trim(tmprbr) ='" + Br_auto[2].Trim() + "'"; }
                                        strWhere = "  Where  " + strtmtref; //+ strtmprbr;
                                        DataTable datBr = new DataTable();
                                        datBr = objCtrlauto.Check_Br(strWhere);
                                        if (datBr.Rows.Count == 0)//neu khong co du lieu trung
                                        {
                                            if (objCtrlauto.AddSWIFT_BR_AUTO(ObjAuto) == 1)
                                            {
                                                dataGrid1.Rows.RemoveAt(f);
                                                Common.ShowError("Message has just import successfull!", 1, MessageBoxButtons.OK);                                                
                                            }
                                            else
                                            {
                                                Common.ShowError("Message has not import !", 3, MessageBoxButtons.OK);                                                                                                
                                                f = f + 1;
                                            }
                                        }
                                        else
                                        {
                                            Common.ShowError("Parameter has already exist", 3, MessageBoxButtons.OK);                                                                                               
                                            
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        f = f + 1;
                                    }
                                }
                                else
                                {
                                    f = f + 1;
                                }
                                //f = f + 1;
                            }
                        }
                        else if (iRowscount > 1)
                        {
                            iExisted = 0;
                            iError = 0;
                            int f = 0;
                            while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                            {
                                if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                                    {
                                        int k = 1;
                                        while (k < dataGrid1.Columns.Count)//duyet tung cot
                                        {
                                            Br_auto[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                            k = k + 1;
                                        }
                                        //----------------Xoa cac dong da insert-----------------------
                                                                  
                                        //------------------------------------------------------------
                                        ObjAuto.TMTREF = Br_auto[1];
                                        ObjAuto.TMPRBR = Br_auto[2];
                                        //---kiem tra xem co bi trung du lieu khong                                        
                                        if (Br_auto[1] == "") { strtmtref = ""; } else { strtmtref = "  Trim(tmtref) ='" + Br_auto[1].Trim() + "'"; }
                                       // if (Br_auto[2] == "") { strtmprbr = ""; } else { strtmprbr = " and  Trim(tmprbr) ='" + Br_auto[2].Trim() + "'"; }
                                        strWhere = "  Where  " + strtmtref;
                                        DataTable datBr = new DataTable();
                                        datBr = objCtrlauto.Check_Br(strWhere);
                                        if (datBr.Rows.Count == 0)//neu khong co du lieu trung
                                        {
                                            if (objCtrlauto.AddSWIFT_BR_AUTO(ObjAuto) == 1)
                                            {
                                                dataGrid1.Rows.RemoveAt(f);
                                                iTong = iTong + 1;
                                            }
                                            else
                                            {
                                                Cells_columns = Cells_columns + "\r\n" + Br_auto[1] + "--" + Br_auto[2];
                                                iError = 1;
                                                f = f + 1;
                                            }
                                        }
                                        else
                                        {
                                            iExisted = iExisted + 1;
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        f = f + 1;
                                    }
                                }
                                else
                                {
                                    f = f + 1;
                                }
                                //f = f + 1;
                            }
                            if (iError == 0)//khong co du lieu nao loi thi thong bao
                            {
                                if (iExisted != 0)
                                {
                                    if (iTong == 0)
                                    {
                                        Common.ShowError(iExisted + " :Parameter has already exist", 3, MessageBoxButtons.OK);                                         
                                    }
                                    else if (iTong != 0)
                                    {
                                        Common.ShowError(iTong + " Message has just import successfull!" + "\r\n" + iExisted + " :Parameter has already exist", 1, MessageBoxButtons.OK);                                         
                                    }
                                }
                                else
                                {
                                    Common.ShowError(iTong + " Message has just import successfull!", 1, MessageBoxButtons.OK);                                      
                                }//Parameter has already exist
                            }
                            else//co it nhat mot loi
                            {
                                if (iExisted != 0)
                                {
                                    if (iTong == 0)
                                    {
                                        string Msg = iExisted + " :Parameter has already exist" + "\r\n" + "Do you want view message import error ?";
                                        string title = Common.sCaption;
                                        DialogResult DlgResult = new DialogResult();
                                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (DlgResult == DialogResult.Yes)
                                        {
                                            frmMessageImportError frmerror = new frmMessageImportError();
                                            frmerror.strCells_columns = Cells_columns;
                                            frmerror.ShowDialog();
                                        }                                       
                                    }
                                    else if (iTong != 0)
                                    {
                                        string Msg = iTong + " :Message has just import successfull!" + "\r\n" + iExisted + " :Parameter has already exist" + "\r\n" + "Do you want view message import error ?";
                                        string title = Common.sCaption;
                                        DialogResult DlgResult = new DialogResult();
                                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (DlgResult == DialogResult.Yes)
                                        {
                                            frmMessageImportError frmerror = new frmMessageImportError();
                                            frmerror.strCells_columns = Cells_columns;
                                            frmerror.ShowDialog();
                                        }
                                        else
                                        {
                                        }
                                    }
                                }
                                else if (iExisted == 0)
                                {
                                    string Msg = iTong + " Message has just import successfull!" + "\r\n" + "Do you want view message import error ?";
                                    string title = Common.sCaption;
                                    DialogResult DlgResult = new DialogResult();
                                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (DlgResult == DialogResult.Yes)
                                    {
                                        frmMessageImportError frmerror = new frmMessageImportError();
                                        frmerror.strCells_columns = Cells_columns;
                                        frmerror.ShowDialog();
                                    }                                    
                                }
                            }
                        }

                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void SaveRMBrauto()
        {
            try
            {
                string strORG_BRAN; string strRECEIVER_BRAN;
                if (dataGrid1.Rows.Count != 0)//neu luoi ko co du lieu
                {
                    iRowscount = 0;
                    Checked_Els();//kiem tra so hang duoc chon
                    if (iRowscount == 1)//chon mot hang de import
                    {
                        int f = 0;
                        while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                        {
                            if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                                {
                                    int k = 1;
                                    while (k < dataGrid1.Columns.Count)//duyet tung cot
                                    {
                                        RMBr_auto[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                        k = k + 1;
                                    }                                     
                                    
                                    objRM.ORG_BRAN = RMBr_auto[1];
                                    objRM.RECEIVER_BRAN = RMBr_auto[2];
                                    
                                    //check trung ---------------------------------------
                                    if (RMBr_auto[1] == "") { strORG_BRAN = ""; } else { strORG_BRAN = " trim(ORG_BRAN) ='" + RMBr_auto[1].Trim() + "'"; }
                                    if (RMBr_auto[2] == "") { strRECEIVER_BRAN = ""; } else { strRECEIVER_BRAN = " and  Trim(RECEIVER_BRAN) ='" + RMBr_auto[2].Trim() + "'"; }                                    
                                    DataTable datRMBR = new DataTable();
                                    datRMBR = objCtrlRM.Check_RMBr(RMBr_auto[1].Trim(), RMBr_auto[2].Trim());
                                    if (datRMBR.Rows.Count == 0)//khong co du lieu nao trung
                                    {
                                        if (objCtrlRM.ADDSWIFT_RMBR_AUTO(objRM) == 1)
                                        {
                                            dataGrid1.Rows.RemoveAt(f);
                                            Common.ShowError("Message has just import successfull!", 1, MessageBoxButtons.OK);                                            
                                        }
                                        else
                                        {
                                            Common.ShowError("Message has not import !", 2, MessageBoxButtons.OK);                                            
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        Common.ShowError("Parameter has already exist!", 3, MessageBoxButtons.OK);                                       
                                        f = f + 1;
                                    }
                                }
                                else
                                {
                                    f = f + 1;
                                }
                            }
                            else
                            {
                                f = f + 1;
                            }
                        }
                    }
                    else if (iRowscount > 1)
                    {
                        iExisted = 0;
                        iError = 0;
                        int f = 0;
                        while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                        {
                            if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                                {
                                    int k = 1;
                                    while (k < dataGrid1.Columns.Count)//duyet tung cot
                                    {
                                        RMBr_auto[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                        k = k + 1;
                                    }
                                    
                                    objRM.ORG_BRAN = RMBr_auto[1];
                                    objRM.RECEIVER_BRAN = RMBr_auto[2];

                                    //check trung ---------------------------------------
                                    if (RMBr_auto[1] == "") { strORG_BRAN = ""; } else { strORG_BRAN = " trim(ORG_BRAN) ='" + RMBr_auto[1].Trim() + "'"; }
                                    if (RMBr_auto[2] == "") { strRECEIVER_BRAN = ""; } else { strRECEIVER_BRAN = " and  Trim(RECEIVER_BRAN) ='" + RMBr_auto[2].Trim() + "'"; }
                                    DataTable datRMBR = new DataTable();
                                    datRMBR = objCtrlRM.Check_RMBr(RMBr_auto[1].Trim(), RMBr_auto[2].Trim());
                                    if (datRMBR.Rows.Count == 0)//khong co du lieu nao trung
                                    {                                        
                                        if (objCtrlRM.ADDSWIFT_RMBR_AUTO(objRM) == 1)
                                        {
                                            dataGrid1.Rows.RemoveAt(f);
                                            iTong = iTong + 1;
                                        }
                                        else
                                        {
                                            Cells_columns = Cells_columns + "\r\n" + RMBr_auto[1] + "---" + RMBr_auto[2];
                                            iError = 1;
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        iExisted = iExisted + 1;
                                        f = f + 1;
                                    }
                                }
                                else
                                {
                                    f = f + 1;
                                }
                            }
                            else
                            {
                                f = f + 1;
                            }
                        }
                        if (iError == 0)//khong co du lieu nao loi thi thong bao
                        {
                            if (iExisted != 0)
                            {
                                Common.ShowError(iTong + " Message has just import successfull!" + "\r\n" + iExisted + " :Parameter has already exist", 1, MessageBoxButtons.OK);                                
                            }
                            else
                            {
                                Common.ShowError(iTong + " Message has just import successfull!", 1, MessageBoxButtons.OK);                                
                            }

                        }
                        else//co it nhat mot loi
                        {                            
                            if (iExisted != 0)
                            {
                                string Msg = iTong + " Message has just import successfull!" + "\r\n" + iExisted + " :Parameter has already exist" + "\r\n" + "Do you want view message import error ?";
                                string title = Common.sCaption;
                                DialogResult DlgResult = new DialogResult();
                                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult == DialogResult.Yes)
                                {
                                    frmMessageImportError frmerror = new frmMessageImportError();
                                    frmerror.strCells_columns = Cells_columns;
                                    frmerror.ShowDialog();
                                }
                                else
                                {
                                }
                            }
                            else if (iExisted == 0)
                            {
                                string Msg = iTong + " Message has just import successfull!" + "\r\n" + "Do you want view message import error ?";
                                string title = Common.sCaption;
                                DialogResult DlgResult = new DialogResult();
                                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult == DialogResult.Yes)
                                {
                                    frmMessageImportError frmerror = new frmMessageImportError();
                                    frmerror.strCells_columns = Cells_columns;
                                    frmerror.ShowDialog();
                                }
                                else
                                {
                                }
                            }
                        }
                    }


                }
                else
                { 
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        // goi ham thuc hien lay du lieu vao database
		private void btnSaveData_Click(object sender, System.EventArgs e)
		{
            iTong = 0;
            iError = 0;
            try
            {
                string Msg;
                if (Common.strExcel == "2084")
                {
                    Msg = "Do you want to save messages?";
                }
                else
                {
                    Msg = "Do you want to save parameter?";
                }
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    iTong = 0;
                    Check_Select_Rows();//goi ham kiem tra xem co hang nao duoc chon khong
                    if (iSelect == 1)
                    {
                        if (iSave == 1)//lay du lieu day vao bang
                        {
                            string strFILEE = Path.GetFileName(txtFilePath.Text);
                            if (strFILEE.Length > 30)
                            {
                                Common.ShowError("The excel file name is not allow lengther than 30 character", 3, MessageBoxButtons.OK);                                
                            }
                            else
                            {
                                //btnSaveData.Enabled = false;
                                if (cbType.Text.Trim() == "RMBr-Parameter")//Tham số RMBr-Parameter lưu vào bảng Swift_RMBr_Auto
                                {
                                    SaveRMBrauto();//khong phai duyet
                                    DateTime dtDateLogin = DateTime.Now;
                                    string strContent = "Import excel" + "/" + cbType.Text + "/ File name:" + txtFilePath.Text.Trim() + "/" + "ORG_BRAN:=" + dataGrid1.Rows[0].Cells[1].Value.ToString() + "/" + "RECEIVER_BRAN:=" + dataGrid1.Rows[0].Cells[2].Value.ToString() + "/" + "Dia chi IP:" + Common.IpLocal;
                                    //string strContent = "Import excel";
                                    int iLoglevel = 1;
                                    string strWorked = "Insert";
                                    string strTable = "SWIFT_RMBR_AUTO";
                                    string strOld_value = "";
                                    string strNew_value = "";
                                    //goi ham ghilog
                                    Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                                }
                                else if (cbType.Text.Trim() == "TFBr-Parameter")//lưu vào bảng Swift_Br_Auto
                                {
                                    SaveBrauto();//khong phai duyet
                                    DateTime dtDateLogin = DateTime.Now;                                    
                                    string strContent = "Import excel" + "/" + cbType.Text + "/ File name:" + txtFilePath.Text.Trim() + "/" + "TMTREF:=" + dataGrid1.Rows[0].Cells[1].Value.ToString() + "/" + "TMPRBR:=" + dataGrid1.Rows[0].Cells[2].Value.ToString() + "/" + "Dia chi IP:" + Common.IpLocal;
                                    //string strContent = "Import excel";
                                    int iLoglevel = 1;
                                    string strWorked = "Insert";
                                    string strTable = "SWIFT_BR_AUTO";
                                    string strOld_value = "";
                                    string strNew_value = "";
                                    //goi ham ghilog
                                    Ghiloguser(dtDateLogin, Common.Userid, strContent, iLoglevel, strWorked, strTable, strOld_value, strNew_value);

                                }
                                else if (cbType.Text.Trim() != "Outward" || cbType.Text.Trim() != "Inward")//luu du lieu vao bang Excel
                                {
                                    SavaExcel();
                                }
                                if (dataGrid1.Rows.Count == 0)
                                {
                                    btnSaveData.Enabled = false;
                                    button2.Enabled = false;
                                    cmdPrint.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            Common.ShowError("No message is selected!", 3, MessageBoxButtons.OK);                            
                        }

                    }
                    else
                    {
                        Common.ShowError("No message is selected!", 3, MessageBoxButtons.OK);                        
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Common.ShowError(ex.Message, 3, MessageBoxButtons.OK);
            }
            label2.Text = "Total number of messages :" + dataGrid1.Rows.Count;
		}

        /*---------------------------------------------------------------
        * Method           : Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        * Muc dich         : Ham ghi log User dang nhap vao he thong
        * Tham so          : cac gia tri tuong ung voi bang User_msg_log
        * Tra ve           : 
        * Ngay tao         : 10/07/2008
        * Nguoi tao        : QuyND
        * Ngay cap nhat    : 10/07/2008
        * Nguoi cap nhat   : QuyND(Nguyen duc quy)
        *--------------------------------------------------------------*/
        private void Ghiloguser(DateTime Logdate, string strUsername, string strContent, int Log_level, string strWorked, string strTale_Access, string strOld_value, string strNew_value)
        {
            objuser_msg_log.LOG_DATE = Logdate;
            objuser_msg_log.USERID = strUsername;
            objuser_msg_log.CONTENT = strContent;
            objuser_msg_log.STATUS = Log_level;
            objuser_msg_log.WORKED = strWorked;
            objuser_msg_log.TABLE_ACCESS = strTale_Access;
            objuser_msg_log.OLD_VALUE = strOld_value;
            objuser_msg_log.NEW_VALUE = strNew_value;

            objcontroluser_msg_log.AddUSER_MSG_LOG1(objuser_msg_log);
        }


        private void Check_columns_ccycd(string pColumns)//kiem tra laoi tien
        {
            try
            {
                iCcycd = 0;
                int p = 0;
                
                while (p < dataGrid1.Rows.Count)
                {
                    string strNAME = dataGrid1.Rows[p].Cells[pColumns].Value.ToString();
                    if (strNAME.Trim().Length == 3)//do dai la 3
                    {
                        if (Regex.IsMatch(strNAME.Trim(), @"^[0-9]*\z") == true)//neu hoan toan la so
                        {
                            iCcycd = 1;//khong dung dinh dang
                            return;
                        }
                        else if (strNAME.Trim().Length != 3)
                        {
                            iCcycd = 1;
                            return;
                        }
                    }
                    else
                    {
                        iCcycd = 1;
                    }
                    p = p + 1;
                }

            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Check_columns_amount(string pColumns)//kieu dinh dang tien
        {
            try
            {
                iAmount = 0;
                int p = 0;
                while (p < dataGrid1.Rows.Count)
                {
                    string strNAME = dataGrid1.Rows[p].Cells[pColumns].Value.ToString();
                    if (strNAME.Trim() == "")
                    {
                        iAmount = 1;//khong duoc rong truong amount nay
                        return;
                    }
                    else if (Regex.IsMatch(strNAME.Trim().Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == false)
                    {
                        iAmount = 1;//khong duoc rong truong amount nay
                        return;
                    }
                    p = p + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //ham kiem tra null cua cac cot tren luoi
        private void Check_Null()
        {
            try
            {
                int m = 1;
                while (m < dataGrid1.Columns.Count)//duyet tung cot
                {
                    iColumns = false;
                    //kiem tra xem cot do truong CHK co == M hay khong 
                    //neu bang M thi phai check Null
                    if (Excel_Colums[m].Trim() == "M")//khong duoc phep null
                    {
                        int p = 0;
                        while (p < dataGrid1.Rows.Count)
                        {
                            if (dataGrid1.Rows[p].Cells[m].Value.ToString() == "")
                            {
                                iColumns = true;
                                break;
                            }
                            p = p + 1;
                        }
                        if (iColumns == true)
                        {
                            //dataGrid1.Rows[p].Cells[0].Value = null;
                            //iFormat = iFormat + 1;//moi them ngay 2008.09.26
                            break;
                        }
                    }
                    m = m + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void SavaExcel()
        {
            try
            {                
                if (cbType.Text.Trim() == "Inward")//Inward
                {
                    pMSG_DIREC = "SWIFT-SIBS";
                }
                else if (cbType.Text.Trim() == "Outward")
                {
                    pMSG_DIREC = "SIBS-SWIFT";
                }
                DataTable datF = new DataTable();
                datF = objctrlMSG.Columns_Check("SWIFT", "MT103", "H1", pMSG_DIREC);//ngan hang gui
                strGui = datF.Rows[0]["XLSCOL"].ToString().Trim();
                //-------------------------------------------------------------
                DataTable datF1 = new DataTable();
                datF1 = objctrlMSG.Columns_Check("SWIFT", "MT103", "H2", pMSG_DIREC);//ngan hang nhan
                strNhan = datF1.Rows[0]["XLSCOL"].ToString().Trim();
                //---------------------------------------------------------------
                DataTable datF2 = new DataTable();
                datF2 = objctrlMSG.Columns_Check("SWIFT", "MT103", "20", pMSG_DIREC);//F20
                strF20 = datF2.Rows[0]["XLSCOL"].ToString().Trim();
                //-----------------------------------------------------------------                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int iCOUNTNT = 0;
            string strF1; string strF2; string strF3; string strF4; string strF5;// string strF6; string strF7; string strF8; string strF9; string strF10; string strF11; string strF12; string strF13; string strF14; string strF15; string strF16; string strF17; string strFfile_name; string strFstatus; string strFgwtype; string strmsg_direction; string strtype;
            string strWhere;
            try
            {
                Check_Null();
                if (iColumns == false)//cac cot thoa man dien kien khong duoc rong
                {
                    //--lay du lieu tu bang ALLCODE-----------
                    DataTable datT = new DataTable();
                    datT = objCtrolall.GetALLCODE_SWIFT(cbType.Text.Trim(), "Type", "SWIFT");
                    string strCDVAL = datT.Rows[0]["CDVAL"].ToString();
                    //----------------kiem tra so tien va loai tien --------------------
                    DataTable datF = new DataTable();
                    datF = objctrlMSG.COLUMNS_MSG_XLS_SWIFT("SWIFT", "MT103", "AMOUNT", pMSG_DIREC);
                    strColumns_Amount = datF.Rows[0]["XLSCOL"].ToString().Trim();

                    Check_columns_amount(datF.Rows[0]["XLSCOL"].ToString().Trim());//loai tien//Check_columns_amount
                    //------------------------------------------------------------------
                    DataTable datF1 = new DataTable();
                    datF1 = objctrlMSG.COLUMNS_MSG_XLS_SWIFT("SWIFT", "MT103", "CURRENCY", pMSG_DIREC);
                    strColumns_Currency = datF1.Rows[0]["XLSCOL"].ToString().Trim();
                    Check_columns_ccycd(datF1.Rows[0]["XLSCOL"].ToString());//so tiem
                    //------------------------------------------------------------------
                    if (iAmount == 1 || iCcycd == 1)//khong dung dinh dang
                    {
                        Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);                        
                        return;
                    }
                    else //dung dinh dang
                    {
                        if (dataGrid1.Rows.Count == 0)
                        {
                        }
                        else
                        {
                            iRowscount = 0;
                            Checked_Els();//kiem tra so hang duoc chon
                            if (iRowscount == 1)
                            {
                                int f = 0;
                                while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                                {
                                    if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                                    {
                                        if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                                        {
                                            int k = 1;
                                            while (k < dataGrid1.Columns.Count)//duyet tung cot
                                            {
                                                Excel[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                                k = k + 1;
                                            }                                           
                                            //--check truong xem co dung dinh dang khong---------------
                                            if (Regex.IsMatch(Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                            {
                                                if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim().Length != 3)
                                                {
                                                    iCOUNTNT = 1;
                                                }
                                                else
                                                {
                                                    iCOUNTNT = 0;
                                                }
                                            }
                                            else
                                            {
                                                iCOUNTNT = 1;
                                            }
                                            //---------------------------------------------------------
                                            if (iCOUNTNT == 1)//dong nay sai khong insert vao datatbases nua
                                            {
                                                f = f + 1;
                                            }
                                            else//duoc phep insert vao trong datdabase
                                            {
                                                objExcel.F1 = Excel[1];
                                                objExcel.F2 = Excel[2];
                                                objExcel.F3 = Excel[3];
                                                objExcel.F4 = Excel[4];
                                                objExcel.F5 = Excel[5];
                                                objExcel.F6 = Excel[6];
                                                objExcel.F7 = Excel[7];
                                                objExcel.F8 = Excel[8];
                                                objExcel.F9 = Excel[9];
                                                objExcel.F10 = Excel[10];
                                                objExcel.F11 = Excel[11];
                                                objExcel.F12 = Excel[12];
                                                objExcel.F13 = Excel[13];
                                                objExcel.F14 = Excel[14];
                                                objExcel.F15 = Excel[15];
                                                objExcel.F16 = Excel[16];
                                                objExcel.F17 = Excel[17];
                                                objExcel.FILE_NAME = Path.GetFileName(txtFilePath.Text);                                               
                                                objExcel.STATUS = 0;
                                                objExcel.GWTYPE = Common.gGWTYPE;
                                                if (cbType.Text.Trim() == "Inward")
                                                {
                                                    objExcel.MSG_DIRECTION = "SWIFT-SIBS";// strDIREC;
                                                }
                                                if (cbType.Text.Trim() == "Outward")
                                                {
                                                    objExcel.MSG_DIRECTION = "SIBS-SWIFT";// strDIREC;
                                                }
                                                objExcel.TYPE = strCDVAL;
                                                objExcel.TELLER_ID = Common.Userid;
                                                objExcel.OFFICER_ID = "";
                                                //check trung ---------------------------------------                                                
                                                if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))] == "") { strF1 = ""; } else { strF1 = " trim(" + strColumns_Currency + ") ='" + Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))] == "") { strF2 = ""; } else { strF2 = " and  Trim(" + strColumns_Amount + ") ='" + Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strGui.Replace("F", ""))] == "") { strF3 = ""; } else { strF3 = " and  Trim(" + strGui + ") ='" + Excel[Convert.ToInt32(strGui.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strNhan.Replace("F", ""))] == "") { strF4 = ""; } else { strF4 = " and  Trim(" + strNhan + ") ='" + Excel[Convert.ToInt32(strNhan.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strF20.Replace("F", ""))] == "") { strF5 = ""; } else { strF5 = " and  Trim(" + strF20 + ") ='" + Excel[Convert.ToInt32(strF20.Replace("F", ""))].Trim() + "'"; }
                                                strWhere = "  Where  " + strF1 + strF2 + strF3 + strF4 + strF5 + " and Trim(STATUS) in ('1','0','2')";
                                                DataTable datCheck = new DataTable();
                                                datCheck = objctrolExcel.Check_Excel(strWhere);
                                                if (datCheck.Rows.Count == 0)//khong co du lieu nao trung
                                                {
                                                    if (objctrolExcel.AddExcel(objExcel) == 1)
                                                    {
                                                        dataGrid1.Rows.RemoveAt(f);
                                                        Common.ShowError("Message has just import successfull!", 1, MessageBoxButtons.OK);                                                        
                                                    }
                                                    else
                                                    {
                                                        Common.ShowError("Message has not import !", 2, MessageBoxButtons.OK);                                                        
                                                        f = f + 1;
                                                    }
                                                }
                                                else//dien bi trung thong bao co ghi hay khong
                                                {
                                                    string Msg = "Message has already exist!" + "\r\n" + "Do you want to proceed?";
                                                    string title = Common.sCaption;
                                                    DialogResult DlgResult = new DialogResult();
                                                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                                    if (DlgResult == DialogResult.Yes)//muon ghi them thong tin
                                                    {
                                                        if (objctrolExcel.AddExcel(objExcel) == 1)
                                                        {
                                                            dataGrid1.Rows.RemoveAt(f);
                                                        }
                                                        else
                                                        {
                                                            Common.ShowError("Message has not import !", 2, MessageBoxButtons.OK);                                                             
                                                            f = f + 1;
                                                        }
                                                    }
                                                    else//khong muon nghi them thong tin
                                                    {
                                                        f = f + 1;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        f = f + 1;
                                    }
                                }
                            }
                            else if (iRowscount > 1)
                            {
                                iExisted = 0;
                                iError = 0;
                                int f = 0;
                                while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                                {
                                    if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                                    {
                                        if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                                        {
                                            int k = 1;
                                            while (k < dataGrid1.Columns.Count)//duyet tung cot
                                            {
                                                Excel[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                                k = k + 1;
                                            }
                                            //--check truong xem co dung dinh dang khong---------------
                                            if (Regex.IsMatch(Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                            {
                                                if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim().Length != 3)
                                                {
                                                    iCOUNTNT = 1;
                                                }
                                                else
                                                {
                                                    iCOUNTNT = 0;
                                                }
                                            }
                                            else
                                            {
                                                iCOUNTNT = 1;
                                            }
                                            //---------------------------------------------------------
                                            if (iCOUNTNT == 1)//dong nay sai khong insert vao datatbases nua
                                            {
                                                f = f + 1;
                                            }
                                            else//duoc phep insert vao trong datdabase
                                            {
                                                //------------------------------------------------------------
                                                objExcel.F1 = Excel[1];
                                                objExcel.F2 = Excel[2];
                                                objExcel.F3 = Excel[3];
                                                objExcel.F4 = Excel[4];
                                                objExcel.F5 = Excel[5];
                                                objExcel.F6 = Excel[6];
                                                objExcel.F7 = Excel[7];
                                                objExcel.F8 = Excel[8];
                                                objExcel.F9 = Excel[9];
                                                objExcel.F10 = Excel[10];
                                                objExcel.F11 = Excel[11];
                                                objExcel.F12 = Excel[12];
                                                objExcel.F13 = Excel[13];
                                                objExcel.F14 = Excel[14];
                                                objExcel.F15 = Excel[15];
                                                objExcel.F16 = Excel[16];
                                                objExcel.F17 = Excel[17];
                                                objExcel.FILE_NAME = Path.GetFileName(txtFilePath.Text);                                                
                                                objExcel.STATUS = 0;
                                                objExcel.GWTYPE = Common.gGWTYPE;
                                                if (cbType.Text.Trim() == "Inward")
                                                {
                                                    objExcel.MSG_DIRECTION = "SWIFT-SIBS";// strDIREC;
                                                }
                                                if (cbType.Text.Trim() == "Outward")
                                                {
                                                    objExcel.MSG_DIRECTION = "SIBS-SWIFT";// strDIREC;
                                                }
                                                objExcel.TYPE = strCDVAL;
                                                objExcel.TELLER_ID = Common.Userid;
                                                objExcel.OFFICER_ID = "";
                                                //check trung ---------------------------------------
                                                if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))] == "") { strF1 = ""; } else { strF1 = " trim(" + strColumns_Currency + ") ='" + Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))] == "") { strF2 = ""; } else { strF2 = " and  Trim(" + strColumns_Amount + ") ='" + Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strGui.Replace("F", ""))] == "") { strF3 = ""; } else { strF3 = " and  Trim(" + strGui + ") ='" + Excel[Convert.ToInt32(strGui.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strNhan.Replace("F", ""))] == "") { strF4 = ""; } else { strF4 = " and  Trim(" + strNhan + ") ='" + Excel[Convert.ToInt32(strNhan.Replace("F", ""))].Trim() + "'"; }
                                                if (Excel[Convert.ToInt32(strF20.Replace("F", ""))] == "") { strF5 = ""; } else { strF5 = " and  Trim(" + strF20 + ") ='" + Excel[Convert.ToInt32(strF20.Replace("F", ""))].Trim() + "'"; }
                                                strWhere = "  Where  " + strF1 + strF2 + strF3 + strF4 + strF5 + " and Trim(STATUS) in ('1','0','2')"; 
                                                DataTable datCheck = new DataTable();
                                                datCheck = objctrolExcel.Check_Excel(strWhere);
                                                if (datCheck.Rows.Count == 0)//khong co du lieu nao trung
                                                {
                                                    if (objctrolExcel.AddExcel(objExcel) == 1)
                                                    {
                                                        iTong = iTong + 1;
                                                        dataGrid1.Rows.RemoveAt(f);
                                                    }
                                                    else
                                                    {
                                                        Cells_columns = Cells_columns + "\r\n" + Excel[1] + "---" + Excel[2] + "---" + Excel[3] + "---" + Excel[4] + "---" + Excel[5] + "---" + Excel[6] + "---" + Excel[7] + "---" + Excel[8] + "---" + Excel[9] + "---" + Excel[10] + "---" + Excel[11] + "---" + Excel[12] + "---" + Excel[13] + "---" + Excel[14] + "---" + Excel[15] + "---" + Excel[16] + "---" + Excel[17];
                                                        iError = 1;
                                                        f = f + 1;
                                                    }
                                                }
                                                else
                                                {
                                                    iExisted = iExisted + 1;
                                                    f = f + 1;
                                                }
                                            }
                                            //---------------------------------------------------
                                        }
                                        else
                                        {
                                            f = f + 1;
                                        }
                                    }
                                    else
                                    {
                                        f = f + 1;
                                    }
                                }
                                if (iError == 0)//khong co du lieu nao loi thi thong bao
                                {
                                    if (iExisted == 0)//khong co du lieu nao trung
                                    {
                                        Common.ShowError(iTong + "  Message has just import successfull!", 1, MessageBoxButtons.OK);                                         
                                    }
                                    else if (iExisted != 0)//co du lieu trung
                                    {
                                        if (iTong == 0)
                                        {
                                            string Msg1 = iExisted + " :Message has already exist" + "\r\n" + " Do you want to proceed?";
                                            string title1 = Common.sCaption;
                                            DialogResult DlgResult1 = new DialogResult();
                                            DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                            if (DlgResult1 == DialogResult.Yes)
                                            {
                                                //goi ham insert lai dien trung
                                                Insert_Exits();
                                            }
                                        }
                                        else if (iTong != 0)
                                        {
                                            string Msg1 = iTong + " :Message has just import successfull!" + "\r\n" + iExisted + " :Message has already exist" + "\r\n" + " Do you want to proceed?";
                                            string title1 = Common.sCaption;
                                            DialogResult DlgResult1 = new DialogResult();
                                            DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                            if (DlgResult1 == DialogResult.Yes)
                                            {
                                                //goi ham insert lai dien trung
                                                Insert_Exits();
                                            }
                                        }
                                    }
                                }
                                else//co it nhat mot loi
                                {
                                    if (iExisted == 0)//khong co du lieu nao trung
                                    {
                                        string Msg = iTong + " Message has just imported successfull!" + "\r\n" + "Do you want view message import error ?";
                                        string title = Common.sCaption;
                                        DialogResult DlgResult = new DialogResult();
                                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (DlgResult == DialogResult.Yes)
                                        {
                                            frmMessageImportError frmerror = new frmMessageImportError();
                                            frmerror.strCells_columns = Cells_columns;
                                            frmerror.ShowDialog();
                                        }                                        
                                    }
                                    else if (iExisted != 0)
                                    {
                                        //neu co dien lo ma co ca dien trung yeu cau hien thi dien loi truoc va tiep tuc Save sau
                                        string Msg = iTong + " Message has just import successfull!" + "\r\n" + iExisted + " :Message has already exist" + "\r\n" + " Do you want view message import error ?";
                                        string title = Common.sCaption;
                                        DialogResult DlgResult = new DialogResult();
                                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (DlgResult == DialogResult.Yes)
                                        {
                                            frmMessageImportError frmerror = new frmMessageImportError();
                                            frmerror.strCells_columns = Cells_columns;
                                            frmerror.ShowDialog();
                                        }
                                        // yeu cau co Save dien trung hay khong
                                        string Msg1 = iTong + " Message has just import successfull!" + "\r\n" + iExisted + " :Message has already exist" + "\r\n" + " Do you want to proceed?";
                                        string title1 = Common.sCaption;
                                        DialogResult DlgResult1 = new DialogResult();
                                        DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (DlgResult1 == DialogResult.Yes)
                                        {
                                            //goi ham insert lai dien trung
                                            Insert_Exits();
                                        }
                                    }
                                }
                            }

                        }
                    }//------------------------------------------------------------------------
                }
                else
                {
                    Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);                    
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            //dataGrid1.DataSource = _dt;
            label2.Text = "Total number of messages : " + dataGrid1.Rows.Count;
            if (dataGrid1.Rows.Count == 0)
            {
                cmdDelete.Enabled = false;
                cmdSave.Enabled = false;
            }
            
        }

        // ham thuc hien insert toan bo cac dien trung
        private void Insert_Exits()
        {
            try
            {
                iTong = 0;
                DataTable datT = new DataTable();
                datT = objCtrolall.GetALLCODE_code1(cbType.Text.Trim(), "Type");
                string strCDVAL = datT.Rows[0]["CDVAL"].ToString();
                //---------------------------------------------------------------
                int iCOUNTNT = 0;
                iError = 0;
                int f = 0;
                while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                {
                    if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            int k = 1;
                            while (k < dataGrid1.Columns.Count)//duyet tung cot
                            {
                                Excel[k] = dataGrid1.Rows[f].Cells[k].Value.ToString();
                                k = k + 1;
                            }
                            //----------------Xoa cac dong da insert-----------------------
                            if (cbType.Text.Trim() == "MT103")
                            {
                                if (Regex.IsMatch(Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Replace(",", "").Replace(".", ""), @"^[0-9]*\z") == true)//neu hoan toan la so
                                {
                                    if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim().Length != 3)
                                    {
                                        iCOUNTNT = 1;
                                    }
                                    else
                                    {
                                        iCOUNTNT = 0;
                                    }
                                }
                                else
                                {
                                    iCOUNTNT = 1;
                                }
                            }
                            if (iCOUNTNT == 1)//dong nay sai khong insert vao datatbases nua
                            {
                                f = f + 1;
                            }
                            else//duoc phep insert vao trong datdabase
                            {
                                //------------------------------------------------------------
                                objExcel.F1 = Excel[1];
                                objExcel.F2 = Excel[2];
                                objExcel.F3 = Excel[3];
                                objExcel.F4 = Excel[4];
                                objExcel.F5 = Excel[5];
                                objExcel.F6 = Excel[6];
                                objExcel.F7 = Excel[7];
                                objExcel.F8 = Excel[8];
                                objExcel.F9 = Excel[9];
                                objExcel.F10 = Excel[10];
                                objExcel.F11 = Excel[11];
                                objExcel.F12 = Excel[12];
                                objExcel.F13 = Excel[13];
                                objExcel.F14 = Excel[14];
                                objExcel.F15 = Excel[15];
                                objExcel.F16 = Excel[16];
                                objExcel.F17 = Excel[17];
                                objExcel.FILE_NAME = Path.GetFileName(txtFilePath.Text);
                                objExcel.TRANS_DATE = DateTime.Now;
                                objExcel.STATUS = 0;
                                objExcel.GWTYPE = Common.gGWTYPE;
                                if (cbType.Text.Trim() == "Inward")
                                {
                                    objExcel.MSG_DIRECTION = "SWIFT-SIBS";// strDIREC;
                                }
                                if (cbType.Text.Trim() == "Outward")
                                {
                                    objExcel.MSG_DIRECTION = "SIBS-SWIFT";// strDIREC;
                                }
                                objExcel.TYPE = strCDVAL;
                                objExcel.TELLER_ID = Common.Userid;
                                objExcel.OFFICER_ID = "";
                                if (objctrolExcel.AddExcel(objExcel) == 1)
                                {
                                    iTong = iTong + 1;
                                    dataGrid1.Rows.RemoveAt(f);
                                }
                                else
                                {
                                    Cells_columns = Cells_columns + "\r\n" + Excel[1] + "--" + Excel[2] + "--" + Excel[3] + "--" + Excel[4] + "--" + Excel[5] + "--" + Excel[6] + "--" + Excel[7] + "--" + Excel[8] + "--" + Excel[9] + "--" + Excel[10] + "--" + Excel[11] + "--" + Excel[12] + "--" + Excel[13] + "--" + Excel[14] + "--" + Excel[15] + "--" + Excel[16] + "--" + Excel[17];
                                    iError = 1;
                                    f = f + 1;
                                }
                            }
                        }
                        else
                        {
                            f = f + 1;
                        }
                    }
                    else
                    {
                        f = f + 1;
                    }
                }
                if (iError == 0)//khong co du lieu nao loi thi thong bao
                {
                    Common.ShowError(iTong + "  Message has just import successfull!", 1, MessageBoxButtons.OK);
                }
                else//co it nhat mot loi
                {
                    string Msg = iTong + " Message has just import successfull!" + "\r\n" + " Do you want view message import error ?";
                    string title = Common.sCaption;
                    DialogResult DlgResult = new DialogResult();
                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DlgResult == DialogResult.Yes)
                    {
                        frmMessageImportError frmerror = new frmMessageImportError();
                        frmerror.strCells_columns = Cells_columns;
                        frmerror.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReadWriteExcelDemo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                 Check_Select_Rows();//goi ham kiem tra xem co hang nao duoc chon khong
                 if (iSelect == 1)
                 {
                     if (Messagebox())
                     {
                         if (iDelete == 0)//delete trong co so du lieusau do load lai du lieu
                         {
                             int k = 0;
                             while (k < dataGrid1.Rows.Count)// duyet tung ban ghi trong Luoi
                             {
                                 if (dataGrid1.Rows[k].Cells[0].Value != null)// hang duoc chon
                                 {
                                     if (dataGrid1.Rows[k].Cells[0].Value.ToString() == "True")// dong duoc chon
                                     {
                                         string strMsg_id = dataGrid1.Rows[k].Cells[1].Value.ToString();
                                         objctrolExcel.DeleteRows(strMsg_id);
                                         dataGrid1.Rows.RemoveAt(k);
                                         //k = k + 1;
                                     }
                                     else
                                     {
                                         k = k + 1;
                                     }
                                 }
                                 else
                                 {
                                     k = k + 1;
                                 }
                             }
                             //GetdatatExcel();
                             label2.Text = "Total number of messages : " + dataGrid1.Rows.Count;
                             if (dataGrid1.Rows.Count == 0)
                             {
                                 cmdDelete.Enabled = false;
                                 button2.Enabled = false;
                                 button1.Enabled = false;
                                 btnSaveData.Enabled = false;
                                 cmdSave.Enabled = false;
                                 cmdPrint.Enabled = false;
                             }
                         }
                         else if (iDelete == 1)//delete tren luoi(hoi cu chuoi nhung giai phap tinh the)
                         {
                             int kk = 0;
                             while (kk < dataGrid1.Rows.Count)// duyet tung ban ghi trong Luoi
                             {
                                 if (dataGrid1.Rows[kk].Cells[0].Value != null)// hang duoc chon
                                 {
                                     if (dataGrid1.Rows[kk].Cells[0].Value.ToString() == "True")// dong duoc chon
                                     {
                                         //int dd = 0;
                                         //while (dd < _dt.Rows.Count)
                                         //{
                                         //    if (dd == kk)
                                         //    {
                                         //        _dt.Rows.RemoveAt(dd);
                                         //    }
                                         //    dd = dd + 1;
                                         //}
                                         dataGrid1.Rows.RemoveAt(kk);
                                     }
                                     else
                                     {
                                         kk = kk + 1;
                                     }
                                 }
                                 else
                                 {
                                     kk = kk + 1;
                                 }
                             }
                             //dataGrid1.DataSource = _dt;
                             label2.Text = "Total number of messages : " + dataGrid1.Rows.Count;
                             if (dataGrid1.Rows.Count == 0)
                             {
                                 cmdDelete.Enabled = false;
                                 button2.Enabled = false;
                                 button1.Enabled = false;
                                 btnSaveData.Enabled = false;
                                 cmdSave.Enabled = false;
                                 cmdPrint.Enabled = false;
                             }
                         }
                     }
                 }
                 else
                 {
                     Common.ShowError("No message is selected!", 3, MessageBoxButtons.OK);                     
                 }
            }
            catch(Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);  
            }
        }

        //ham kiem tra xem su lua chon cac o check box
        private void Check_Select_Rows()
        {
            iSelect = 0;
            try
            {
                int b = 0;
                while (b < dataGrid1.Rows.Count)
                {
                    if (dataGrid1.Rows[b].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGrid1.Rows[b].Cells[0].Value.ToString() == "True")
                        {
                            iSelect = 1;
                        }
                    }
                    b = b + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            iTong = 0;
            int iSucces = 0;            
            iExisted = 0;
            iError = 0;
            try
            {
                if (cbType.Text.Trim() == "Inward")//Inward
                {
                    pMSG_DIREC = "SWIFT-SIBS";
                }
                else if (cbType.Text.Trim() == "Outward")
                {
                    pMSG_DIREC = "SIBS-SWIFT";
                }
                //---------------------------------------------------------------
                int g = 0;
                while (g < 17)
                {
                    Excel[g] = "";
                    g = g + 1;
                }
                //---------------------------------------------------------------
                string strF1; string strF2; string strF3; string strF4; string strF5;
                string strWhere;
                //----------------kiem tra so tien va loai tien --------------------
                DataTable datF = new DataTable();
                datF = objctrlMSG.COLUMNS_MSG_XLS_SWIFT("SWIFT", "MT103", "AMOUNT", pMSG_DIREC);
                strColumns_Amount = datF.Rows[0]["XLSCOL"].ToString().Trim();

                Check_columns_amount(datF.Rows[0]["XLSCOL"].ToString().Trim());//loai tien//Check_columns_amount
                //------------------------------------------------------------------
                DataTable datF1 = new DataTable();
                datF1 = objctrlMSG.COLUMNS_MSG_XLS_SWIFT("SWIFT", "MT103", "CURRENCY", pMSG_DIREC);
                strColumns_Currency = datF1.Rows[0]["XLSCOL"].ToString().Trim();
                Check_columns_ccycd(datF1.Rows[0]["XLSCOL"].ToString());//so tiem
                //------------------------------------------------------------------
                //--------------------------------------------------------------------
                DataTable datF2 = new DataTable();
                datF2 = objctrlMSG.Columns_Check("SWIFT", "MT103", "H1", pMSG_DIREC);//ngan hang gui
                strGui = datF2.Rows[0]["XLSCOL"].ToString().Trim();
                //-------------------------------------------------------------
                DataTable datF3 = new DataTable();
                datF3 = objctrlMSG.Columns_Check("SWIFT", "MT103", "H2", pMSG_DIREC);//ngan hang nhan
                strNhan = datF3.Rows[0]["XLSCOL"].ToString().Trim();
                //---------------------------------------------------------------
                DataTable datF4 = new DataTable();
                datF4 = objctrlMSG.Columns_Check("SWIFT", "MT103", "20", pMSG_DIREC);//F20
                strF20 = datF4.Rows[0]["XLSCOL"].ToString().Trim();
                //-----------------------------------------------------------------   
                Check_Select_Rows();//goi ham kiem tra xem co hang nao duoc chon khong
                if (iSelect == 1)
                {
                    iRowscount = 0;
                    Checked_Els();//kiem tra so hang duoc chon
                    if (iRowscount == 1)
                    {
                        int k = 0;
                        while (k < dataGrid1.Rows.Count)// duyet tung ban ghi trong Luoi
                        {
                            if (dataGrid1.Rows[k].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dataGrid1.Rows[k].Cells[0].Value.ToString() == "True")// dong duoc chon
                                {
                                    //--------lay du lieu check trung--------------------------------------
                                    int f = 0;
                                    while (f < 17)//duyet tung cot
                                    {
                                        Excel[f] = dataGrid1.Rows[k].Cells[f+1].Value.ToString();
                                        f = f + 1;
                                    }
                                    //---------------------------------------------------------------------
                                    if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))] == "") { strF1 = ""; } else { strF1 = " trim(" + strColumns_Currency + ") ='" + Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))] == "") { strF2 = ""; } else { strF2 = " and  Trim(" + strColumns_Amount + ") ='" + Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strGui.Replace("F", ""))] == "") { strF3 = ""; } else { strF3 = " and  Trim(" + strGui + ") ='" + Excel[Convert.ToInt32(strGui.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strNhan.Replace("F", ""))] == "") { strF4 = ""; } else { strF4 = " and  Trim(" + strNhan + ") ='" + Excel[Convert.ToInt32(strNhan.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strF20.Replace("F", ""))] == "") { strF5 = ""; } else { strF5 = " and  Trim(" + strF20 + ") ='" + Excel[Convert.ToInt32(strF20.Replace("F", ""))].Trim() + "'"; }
                                    strWhere = "  Where  " + strF1 + strF2 + strF3 + strF4 + strF5 + " and Trim(STATUS) in ('1','0')";
                                    DataTable datCheck = new DataTable();
                                    datCheck = objctrolExcel.Check_Excel(strWhere);
                                    //---------------------------------------------------------------------
                                    string strMsg_id = dataGrid1.Rows[k].Cells[1].Value.ToString();
                                    if (datCheck.Rows.Count == 0)//khong co du lieu nao trung
                                    {
                                        if (objctrolExcel.UpdateStatus(strMsg_id, Common.Userid) == 1)
                                        {
                                            Common.ShowError("Approve message is successful!", 1, MessageBoxButtons.OK);                                            
                                            iSucces = iSucces + 1;
                                            dataGrid1.Rows.RemoveAt(k);                                            
                                        }
                                        else
                                        {
                                            iError = iError + 1;
                                            Common.ShowError("Approve message is Error!", 2, MessageBoxButtons.OK);                                            
                                            k = k + 1;
                                        }
                                    }
                                    else//da bi trung du lieu
                                    {
                                        string Msg = "Message has already exist!" + "\r\n" + "Do you want to proceed?";
                                        string title = Common.sCaption;
                                        DialogResult DlgResult = new DialogResult();
                                        DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                        if (DlgResult == DialogResult.Yes)//muon ghi them thong tin
                                        {
                                            if (objctrolExcel.UpdateStatus(strMsg_id, Common.Userid) == 1)
                                            {
                                                dataGrid1.Rows.RemoveAt(k);
                                            }
                                            else
                                            {
                                                Common.ShowError("Message has not Approve !", 2, MessageBoxButtons.OK);                                                
                                                k = k + 1;
                                            }
                                        }
                                        else//khong muon nghi them thong tin
                                        {
                                            k = k + 1;
                                        }
                                    }
                                }
                                else
                                {
                                    k = k + 1;
                                }
                            }
                            else
                            {
                                k = k + 1;
                            }
                        }
                    }
                    else if (iRowscount > 1)
                    {
                        int k = 0;
                        while (k < dataGrid1.Rows.Count)// duyet tung ban ghi trong Luoi
                        {
                            if (dataGrid1.Rows[k].Cells[0].Value != null)// hang duoc chon
                            {
                                if (dataGrid1.Rows[k].Cells[0].Value.ToString() == "True")// dong duoc chon
                                {
                                    //--------lay du lieu check trung--------------------------------------
                                    int f = 0;
                                    while (f < 17)//duyet tung cot
                                    {
                                        Excel[f] = dataGrid1.Rows[k].Cells[f+1].Value.ToString();
                                        f = f + 1;
                                    }
                                    //---------------------------------------------------------------------
                                    if (Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))] == "") { strF1 = ""; } else { strF1 = " trim(" + strColumns_Currency + ") ='" + Excel[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))] == "") { strF2 = ""; } else { strF2 = " and  Trim(" + strColumns_Amount + ") ='" + Excel[Convert.ToInt32(strColumns_Amount.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strGui.Replace("F", ""))] == "") { strF3 = ""; } else { strF3 = " and  Trim(" + strGui + ") ='" + Excel[Convert.ToInt32(strGui.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strNhan.Replace("F", ""))] == "") { strF4 = ""; } else { strF4 = " and  Trim(" + strNhan + ") ='" + Excel[Convert.ToInt32(strNhan.Replace("F", ""))].Trim() + "'"; }
                                    if (Excel[Convert.ToInt32(strF20.Replace("F", ""))] == "") { strF5 = ""; } else { strF5 = " and  Trim(" + strF20 + ") ='" + Excel[Convert.ToInt32(strF20.Replace("F", ""))].Trim() + "'"; }
                                    strWhere = "  Where  " + strF1 + strF2 + strF3 + strF4 + strF5 + " and Trim(STATUS) in ('1','0')";
                                    DataTable datCheck = new DataTable();
                                    datCheck = objctrolExcel.Check_Excel(strWhere);
                                    //---------------------------------------------------------------------
                                    string strMsg_id = dataGrid1.Rows[k].Cells[1].Value.ToString();
                                    if (datCheck.Rows.Count == 0)//khong co du lieu nao trung
                                    {
                                        if (objctrolExcel.UpdateStatus(strMsg_id, Common.Userid) == 1)
                                        {
                                            iSucces = iSucces + 1;
                                            iTong = iTong + 1;
                                            dataGrid1.Rows.RemoveAt(k);
                                        }
                                        else
                                        {
                                            Cells_columns = Cells_columns + "\r\n" + Excel[1] + "---" + Excel[2] + "---" + Excel[3] + "---" + Excel[4] + "---" + Excel[5] + "---" + Excel[6] + "---" + Excel[7] + "---" + Excel[8] + "---" + Excel[9] + "---" + Excel[10] + "---" + Excel[11] + "---" + Excel[12] + "---" + Excel[13] + "---" + Excel[14] + "---" + Excel[15] + "---" + Excel[16] + "---" + Excel[17];
                                            iError = 1;
                                            k = k + 1;                                            
                                        }
                                    }
                                    else
                                    {
                                        iExisted = iExisted + 1;
                                        k = k + 1;
                                    }
                                }
                                else
                                {
                                    k = k + 1;
                                }
                            }
                            else
                            {
                                k = k + 1;
                            }
                        }
                        if (iError == 0)//khong co du lieu nao loi thi thong bao
                        {
                            if (iExisted == 0)//khong co du lieu nao trung
                            {
                                Common.ShowError(iTong + "  Message has just import successfull!", 1, MessageBoxButtons.OK);                                
                            }
                            else if (iExisted != 0)//co du lieu trung
                            {
                                string Msg1 = iTong + " Message has just import successfull!" + "\r\n" + iExisted + " :Message has already exist" + "\r\n" + " Do you want to proceed?";
                                string title1 = Common.sCaption;
                                DialogResult DlgResult1 = new DialogResult();
                                DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (DlgResult1 == DialogResult.Yes)
                                {
                                    //goi ham insert lai dien trung
                                    Update_error();
                                }
                            }
                        }
                        else//co it nhat mot loi
                        {
                            if (iExisted == 0)//khong co du lieu nao trung
                            {
                                string Msg = iTong + " Approve message is successful!" + "\r\n" + "Do you want view message import error ?";
                                string title = Common.sCaption;
                                DialogResult DlgResult = new DialogResult();
                                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult == DialogResult.Yes)
                                {
                                    frmMessageImportError frmerror = new frmMessageImportError();
                                    frmerror.strCells_columns = Cells_columns;
                                    frmerror.ShowDialog();
                                }
                            }
                            else if (iExisted != 0)
                            {
                                //neu co dien lo ma co ca dien trung yeu cau hien thi dien loi truoc va tiep tuc Save sau
                                string Msg = iTong + " Approve message is successful!" + "\r\n" + iExisted + " :Message has already exist" + "\r\n" + " Do you want view message Approve error ?";
                                string title = Common.sCaption;
                                DialogResult DlgResult = new DialogResult();
                                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult == DialogResult.Yes)
                                {
                                    frmMessageImportError frmerror = new frmMessageImportError();
                                    frmerror.strCells_columns = Cells_columns;
                                    frmerror.ShowDialog();
                                }
                                // yeu cau co Save dien trung hay khong
                                string Msg1 = iTong + " :Approve message is successful!" + "\r\n" + iExisted + " :Message has already exist" + "\r\n" + " Do you want to proceed?";
                                string title1 = Common.sCaption;
                                DialogResult DlgResult1 = new DialogResult();
                                DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult1 == DialogResult.Yes)
                                {
                                    //goi ham insert lai dien trung
                                    Update_error();
                                }
                            }
                        }
                    }///////////////////////////////////////////////
                }
                else
                {
                    Common.ShowError("No message is selected!", 3, MessageBoxButtons.OK);                    
                }
                if (dataGrid1.Rows.Count == 0)
                {
                    button2.Enabled = false;
                    button1.Enabled = false;
                    btnSaveData.Enabled = false;
                    cmdPrint.Enabled = false;
                }
               
            }
            catch
            {
            }
            //GetdatatExcel();
            label2.Text = "Total number of messages : " + dataGrid1.Rows.Count;
        }

        private void Update_error()
        {
            try
            {
                iError = 0;
                int k = 0;
                while (k < dataGrid1.Rows.Count)// duyet tung ban ghi trong Luoi
                {
                    if (dataGrid1.Rows[k].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGrid1.Rows[k].Cells[0].Value.ToString() == "True")// dong duoc chon
                        {
                            string strMsg_id = dataGrid1.Rows[k].Cells[1].Value.ToString();
                            if (objctrolExcel.UpdateStatus(strMsg_id, Common.Userid) == 1)
                            {                                
                                iTong = iTong + 1;
                                dataGrid1.Rows.RemoveAt(k);
                            }
                            else
                            {
                                iError = 1;                                
                                k = k + 1;
                            }
                        }
                        else
                        {
                            k = k + 1;
                        }
                    }
                    else
                    {
                        k = k + 1;
                    }
                }
                if (iError == 0)//khong co du lieu nao loi thi thong bao
                {
                    Common.ShowError(iTong + " :Approve message is successful!", 1, MessageBoxButtons.OK);                    
                }
                else//co it nhat mot loi
                {
                    string Msg = iTong + " :Approve message is successful!" + "\r\n" + " Do you want view message import error ?";
                    string title = Common.sCaption;
                    DialogResult DlgResult = new DialogResult();
                    DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DlgResult == DialogResult.Yes)
                    {
                        frmMessageImportError frmerror = new frmMessageImportError();
                        frmerror.strCells_columns = Cells_columns;
                        frmerror.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //bat su kiem Cell click vao hang
       private void dataGrid1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                int iRows = e.RowIndex;
                if (e.ColumnIndex == 0)
                {
                    for (int i = 0; i < this.dataGrid1.RowCount; i++)
                    {
                        this.dataGrid1.EndEdit();
                        string re_value = this.dataGrid1.Rows[i].Cells[0].EditedFormattedValue.ToString();
                    }
                }
            }
            if (e.RowIndex != -1)
            {
            }
        }

       private void dataGrid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
       {
           if (e.ColumnIndex == 0)
           {
               for (int i = 0; i < dataGrid1.Rows.Count; i++)
               {
                   dataGrid1.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
               }
           }
       }
       //Muc dich: bat su kien khi nhan phím Enter
       //Nguoi tao: HaNTT10@fpt.com.vn (Nguyen Thi Thu Ha)
       //Ngay tao: 06/08/2008
       private void enterKey(object sender, KeyPressEventArgs e)
       {
           //khi nhan phim ESC
           if (e.KeyChar == (char)27)
           {
               this.Close();
           }
           //khi nhan phim Enter
           if (e.KeyChar == (char)13)
           {
               if (cbType.Focused)
               {
                   txtFilePath.Focus();
                   txtFilePath.SelectAll();
               }
               else if (txtFilePath.Focused)
               {
                   btnOpenFileDlg.Focus();
                   btnOpenFileDlg_Click(null, null);
               }

               //strSucess = true;
           }
       }

       private void frmSwiftImp_FormClosing(object sender, FormClosingEventArgs e)
       {
          
       }

       private void frmSwiftImp_KeyDown(object sender, KeyEventArgs e)
       {
           Common.bTimer = 1;
           if (e.KeyCode == Keys.Escape)
           {
               this.Close();
           }
           if (e.KeyCode == Keys.Return)
           {

               SelectNextControl(this.ActiveControl, true, true, true, true);

               if ((this.ActiveControl) is Button)
               {
               }
               if ((this.ActiveControl) is TextBox)
               {
                   (this.ActiveControl as TextBox).SelectAll();
               }

           }
       }

       private void cmdPrint_Click(object sender, EventArgs e)
       {
           //frmPrint frmPrint = new frmPrint();
           //string Print = "SWIFT_09";
           //frmPrint.PrintType = Print;           
           //frmPrint.ShowDialog();
           if (check == true)
           {
               DataTable datPrint = new DataTable();
               //DataRow[] datRow;
               DataRow datRow1;
               if (_dt.Rows.Count == 0)
               {
                   cmdPrint.Enabled = false;
               }
               if (_dt == null)
               {
                   cmdPrint.Enabled = false;
               }
               for (int i = 0; i < _dt.Columns.Count; i++)
               {
                   DataColumn datColum = new DataColumn(_dt.Columns[i].ColumnName, _dt.Columns[i].DataType);
                   //DataColumn datColum = new DataColumn(dataGrid1.Columns[i].Name, dataGrid1.Columns[i].GetType());
                   datPrint.Columns.Add(datColum);
               }

               try
               {
                   frmPrint frmPrint = new frmPrint();
                   Check_Select_Rows();
                   if (iSelect == 0)//khong click chon otext
                   {
                       frmPrint.HMdat = _dt;
                   }
                   else
                   {

                       int f = 0;
                       while (f < _dt.Rows.Count)
                       {
                           if (dataGrid1.Rows[f].Cells[0].Value != null && dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")// hang duoc chon
                           {

                               datRow1 = datPrint.NewRow();
                               for (int j = 0; j < dataGrid1.Columns.Count - 1; j++)
                               {
                                   datRow1[j] = dataGrid1.Rows[f].Cells[j + 1].Value;
                               }
                               datPrint.Rows.Add(datRow1);

                           }
                           f = f + 1;
                       }


                       frmPrint.HMdat = datPrint;
                   }
                   string Print = "SWIFT_09";
                   frmPrint.PrintType = Print;
                   frmPrint.direction_excell = direction; 
                   frmPrint.WindowState = FormWindowState.Maximized;
                   frmPrint.ShowDialog();
               }
               catch (Exception ex)
               {
                   Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
               }
           }
           else if (check == false)
           {
               DataTable datPrint = new DataTable();
               //DataRow[] datRow;
               DataRow datRow1;
               if (datGet.Rows.Count == 0)
               {
                   cmdPrint.Enabled = false;
               }
               if (datGet == null)
               {
                   cmdPrint.Enabled = false;
               }
               for (int i = 0; i < datGet.Columns.Count; i++)
               {
                   DataColumn datColum = new DataColumn(datGet.Columns[i].ColumnName, datGet.Columns[i].DataType);
                   //DataColumn datColum = new DataColumn(dataGrid1.Columns[i].Name, dataGrid1.Columns[i].GetType());
                   datPrint.Columns.Add(datColum);
               }

               try
               {
                   frmPrint frmPrint = new frmPrint();
                   Check_Select_Rows();
                   if (iSelect == 0)//khong click chon otext
                   {
                       frmPrint.HMdat = datGet;
                   }
                   else
                   {

                       int f = 0;
                       while (f < datGet.Rows.Count)
                       {
                           if (dataGrid1.Rows[f].Cells[0].Value != null && dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")// hang duoc chon
                           {

                               datRow1 = datPrint.NewRow();
                               for (int j = 0; j < dataGrid1.Columns.Count - 1; j++)
                               {
                                   datRow1[j] = dataGrid1.Rows[f].Cells[j + 1].Value;
                               }
                               datPrint.Rows.Add(datRow1);

                           }
                           f = f + 1;
                       }


                       frmPrint.HMdat = datPrint;
                   }
                   string Print = "SWIFT_09";
                   frmPrint.PrintType = Print;
                   //frmPrint.WindowState = FormWindowState.Maximized;
                   frmPrint.ShowDialog();
               }
               catch (Exception ex)
               {
                   Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
               }
           }
                   
       }

       private void button3_Click(object sender, EventArgs e)
       {
           
       }

       private void button3_Click_1(object sender, EventArgs e)
       {
       
       }

       private void cbType_SelectedIndexChanged(object sender, EventArgs e)
       {
           try
           {
               //----goi ham lay thong tin quy dinh ao cot cua file excel lua chon
               DataTable datMsg = new DataTable();
               if (cbType.Text.Trim() == "Inward" )
               {
                   datMsg = objctrlMSG.MSG_XLS("SWIFT", "MT103","SWIFT-SIBS");
               }
               else if(cbType.Text.Trim() == "Outward")
               {
                   datMsg = objctrlMSG.MSG_XLS("SWIFT", "MT103","SIBS-SWIFT");
               }
               //-----------------------------------------------------------------
               DataTable datGet = new DataTable();
               if (cbType.Text.Trim() == "Inward")
               {
                   datGet = objctrolExcel.GetEXCEL_APP(Common.Userid, "SWIFT-SIBS");
               }
               else if (cbType.Text.Trim() == "Outward")
               {
                   datGet = objctrolExcel.GetEXCEL_APP(Common.Userid, "SIBS-SWIFT");
               }
               if (datGet == null)
               {
               }
               else
               {
                   if (datGet.Rows.Count == 0)
                   {
                       dataGrid1.DataSource = datGet;
                       label2.Text = "Total number of messages : 0";
                       button1.Enabled = false;
                       button2.Enabled = false;
                       cmdPrint.Enabled = false;
                   }
                   else
                   {
                       dataGrid1.DataSource = datGet;//day du lieu vao luoi
                       label2.Text = "Total number of messages : " + datGet.Rows.Count;
                       ColumnsRead(dataGrid1);//goi ham de khong cho sua o cac cot
                       dataGrid1.Columns[0].Width = 30;
                       button1.Enabled = true;
                       button2.Enabled = true;
                       cmdPrint.Enabled = true;
                       int k = 0;
                       while (k < datMsg.Rows.Count)
                       {
                           dataGrid1.Columns["F" + (k + 1)].HeaderText = datMsg.Rows[k]["FIELD"].ToString();
                           k = k + 1;
                       }
                   }

               }
           }
           catch (Exception ex)
           {
               Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
           }
       }

       private void frmSwiftImp_MouseMove(object sender, MouseEventArgs e)
       {
           Common.bTimer = 1;
       }

       private void dataGrid1_MouseMove(object sender, MouseEventArgs e)
       {
           Common.bTimer = 1;
       }      


	}  

}
