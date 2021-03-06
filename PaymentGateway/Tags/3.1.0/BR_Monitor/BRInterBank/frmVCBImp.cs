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
//using Microsoft.Office;

namespace BR.BRInterBank
{
	
	public class frmVCBImp : frmBasedata
    {
		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel pnlCell;
		private System.Windows.Forms.Label lblCellOperatorIs;
		private System.Windows.Forms.Label lblGetValueData;
		private System.Windows.Forms.TextBox txtpath;

		private DataTable _dt;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtRange;
		private System.Windows.Forms.TextBox txtPK;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSetPK;
		private System.Windows.Forms.Label lblCurrentPK;
		private System.Windows.Forms.Button btnOpenFileDlg;
        private static int iSave;
        EXCELInfo objExcel = new EXCELInfo();
        EXCELController objctrolExcel = new EXCELController();
        ALLCODEInfo objAllcode = new ALLCODEInfo();
        ALLCODEController objCtrolall = new ALLCODEController();
        SWIFT_BR_AUTOInfo ObjAuto = new SWIFT_BR_AUTOInfo();
        SWIFT_BR_AUTOController objCtrlauto = new SWIFT_BR_AUTOController();
        MSG_XLSInfo objMSG = new MSG_XLSInfo();
        MSG_XLSController objctrlMSG = new MSG_XLSController();
        CURRENCY_CHANNELController ObjCCYCD = new CURRENCY_CHANNELController();
        private GetData objGetData = new GetData();

        private string strColumns_Currency;
        private string strColumns_Amount;

        public int iSelect;
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        string[] Excel_Colums = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] Excel_Colums_M = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] Excel = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        string[] Br_auto = { "", "", "" };
        private int iRowscount;
        private string Cells_columns;
        //private static int iSave;
        private int iError;
        private int iTong;
        //private bool NeedConfirm = true;
        //private static bool strSucess = false;
        private int Columns;//bien de quy dinh so cot cua file

        private int iAmount = 0;
        private int iCcycd = 0;
        private bool iColumns = false;
        private int iExisted;
        private string pSENDERS_REF;
        private int iCCYCD;
        private int iCCYCD_RETURN;
        private int Check_Message;
        private int Check_Message_Count;
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
        private DataGridView dataGrid1;
        private GroupBox groupBox1;
        private ComboBox cbType;
        private Label label2;
		private int _intPKCol=-1;

        private int iDelete;


		#endregion

        private Button button2;
        private Label label5;
        private ComboBox cbbDirection;
        private Label label6;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        

		#region CTOR
        public frmVCBImp()
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
		
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVCBImp));
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.pnlCell = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCellOperatorIs = new System.Windows.Forms.Label();
            this.lblGetValueData = new System.Windows.Forms.Label();
            this.txtCell = new System.Windows.Forms.TextBox();
            this.cboSheetnames = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.lblCurrentPK = new System.Windows.Forms.Label();
            this.txtPK = new System.Windows.Forms.TextBox();
            this.lblSheet = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetPK = new System.Windows.Forms.Button();
            this.txtpath = new System.Windows.Forms.TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.btnOpenFileDlg = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbDirection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(801, 492);
            this.cmdClose.Size = new System.Drawing.Size(86, 30);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(504, 545);
            this.cmdSave.TabIndex = 7;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(417, 545);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(242, 545);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(328, 545);
            // 
            // btnGetData
            // 
            this.btnGetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetData.Location = new System.Drawing.Point(789, 72);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(80, 30);
            this.btnGetData.TabIndex = 4;
            this.btnGetData.Text = "&Import";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveData.Location = new System.Drawing.Point(718, 492);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(80, 30);
            this.btnSaveData.TabIndex = 6;
            this.btnSaveData.Text = "&Save";
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // pnlCell
            // 
            this.pnlCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlCell.Controls.Add(this.label1);
            this.pnlCell.Controls.Add(this.lblCellOperatorIs);
            this.pnlCell.Controls.Add(this.lblGetValueData);
            this.pnlCell.Controls.Add(this.txtCell);
            this.pnlCell.Controls.Add(this.cboSheetnames);
            this.pnlCell.Controls.Add(this.label3);
            this.pnlCell.Controls.Add(this.txtRange);
            this.pnlCell.Controls.Add(this.lblCurrentPK);
            this.pnlCell.Controls.Add(this.txtPK);
            this.pnlCell.Controls.Add(this.lblSheet);
            this.pnlCell.Controls.Add(this.label4);
            this.pnlCell.Controls.Add(this.btnSetPK);
            this.pnlCell.Location = new System.Drawing.Point(125, 212);
            this.pnlCell.Name = "pnlCell";
            this.pnlCell.Size = new System.Drawing.Size(440, 113);
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
            // lblGetValueData
            // 
            this.lblGetValueData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGetValueData.Location = new System.Drawing.Point(183, 10);
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
            // cboSheetnames
            // 
            this.cboSheetnames.Location = new System.Drawing.Point(4, 74);
            this.cboSheetnames.Name = "cboSheetnames";
            this.cboSheetnames.Size = new System.Drawing.Size(121, 21);
            this.cboSheetnames.TabIndex = 18;
            this.cboSheetnames.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(361, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Range";
            this.label3.Visible = false;
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(147, 75);
            this.txtRange.MaxLength = 20;
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(72, 20);
            this.txtRange.TabIndex = 13;
            this.txtRange.Text = "A1:W1000";
            this.txtRange.Visible = false;
            // 
            // lblCurrentPK
            // 
            this.lblCurrentPK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentPK.Location = new System.Drawing.Point(98, 47);
            this.lblCurrentPK.Name = "lblCurrentPK";
            this.lblCurrentPK.Size = new System.Drawing.Size(72, 24);
            this.lblCurrentPK.TabIndex = 17;
            this.lblCurrentPK.Visible = false;
            // 
            // txtPK
            // 
            this.txtPK.Location = new System.Drawing.Point(302, 74);
            this.txtPK.MaxLength = 3;
            this.txtPK.Name = "txtPK";
            this.txtPK.Size = new System.Drawing.Size(47, 20);
            this.txtPK.TabIndex = 14;
            this.txtPK.Text = "0";
            this.txtPK.Visible = false;
            // 
            // lblSheet
            // 
            this.lblSheet.Location = new System.Drawing.Point(223, 47);
            this.lblSheet.Name = "lblSheet";
            this.lblSheet.Size = new System.Drawing.Size(40, 23);
            this.lblSheet.TabIndex = 11;
            this.lblSheet.Text = "Sheet";
            this.lblSheet.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(204, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Prim. Key Col";
            this.label4.Visible = false;
            // 
            // btnSetPK
            // 
            this.btnSetPK.Location = new System.Drawing.Point(364, 75);
            this.btnSetPK.Name = "btnSetPK";
            this.btnSetPK.Size = new System.Drawing.Size(76, 24);
            this.btnSetPK.TabIndex = 16;
            this.btnSetPK.Text = "Set PK";
            this.btnSetPK.Visible = false;
            // 
            // txtpath
            // 
            this.txtpath.Enabled = false;
            this.txtpath.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpath.Location = new System.Drawing.Point(133, 79);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(515, 23);
            this.txtpath.TabIndex = 2;
            // 
            // lblFilename
            // 
            this.lblFilename.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.Location = new System.Drawing.Point(30, 80);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(67, 20);
            this.lblFilename.TabIndex = 8;
            this.lblFilename.Text = "File path :";
            // 
            // btnOpenFileDlg
            // 
            this.btnOpenFileDlg.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFileDlg.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileDlg.Image")));
            this.btnOpenFileDlg.Location = new System.Drawing.Point(668, 77);
            this.btnOpenFileDlg.Name = "btnOpenFileDlg";
            this.btnOpenFileDlg.Size = new System.Drawing.Size(41, 26);
            this.btnOpenFileDlg.TabIndex = 3;
            this.btnOpenFileDlg.Text = "...";
            this.btnOpenFileDlg.Click += new System.EventHandler(this.btnOpenFileDlg_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.AllowUserToOrderColumns = true;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid1.ColumnHeadersHeight = 22;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid1.Location = new System.Drawing.Point(10, 148);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid1.Size = new System.Drawing.Size(875, 340);
            this.dataGrid1.TabIndex = 5;
            this.dataGrid1.TabStop = false;
            this.dataGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGrid1_MouseMove);
            this.dataGrid1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgView_ColumnHeaderMouseClick);
            this.dataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellClick);
            this.dataGrid1.AllowUserToDeleteRowsChanged += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbbDirection);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Controls.Add(this.txtpath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblFilename);
            this.groupBox1.Controls.Add(this.btnOpenFileDlg);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(875, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import file excel";
            // 
            // cbbDirection
            // 
            this.cbbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDirection.FormattingEnabled = true;
            this.cbbDirection.Location = new System.Drawing.Point(133, 49);
            this.cbbDirection.Name = "cbbDirection";
            this.cbbDirection.Size = new System.Drawing.Size(182, 24);
            this.cbbDirection.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Msg direction :";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(133, 19);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(182, 24);
            this.cbType.TabIndex = 1;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Type :";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(635, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "&Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Column1";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(10, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Total number of messages :";
            // 
            // frmVCBImp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(895, 533);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.pnlCell);
            this.Controls.Add(this.btnSaveData);
            this.Name = "frmVCBImp";
            this.Text = "VCB monitor";
            this.Load += new System.EventHandler(this.frmReadWriteExcelDemo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVCBImp_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmVCBImp_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVCBImp_KeyDown);
            this.Controls.SetChildIndex(this.btnSaveData, 0);
            this.Controls.SetChildIndex(this.pnlCell, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.pnlCell.ResumeLayout(false);
            this.pnlCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		
		[STAThread]
		static void Main() 
		{
            Application.Run(new frmVCBImp());
		}//

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

			//Set excel sheet range
			exr.SheetRange = this.txtRange.Text; 
			
		
		}
	
		

		private void frmReadWriteExcelDemo_Load(object sender, System.EventArgs e)
		{
            try
            {
                this.Icon = new Icon(Application.StartupPath + @"\BRIDGE.ico");   
                //----------------day du lieu len combobox-------------------
                cbbDirection.Items.Add("VCB-SIBS");
                cbbDirection.Items.Add("SIBS-VCB");
                cbbDirection.SelectedIndex = 0;
                //--------------------------------------------------------------
                cmdAdd.Visible = false;
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                cmdSave.Visible = false;
                btnSetPK.Enabled = false;
                pnlCell.Enabled = false;
                iDelete = 0;
                label5.Text = "Total number of messages : 0";
                //------------------------------------------------------------
                btnGetData.Enabled = false;
                button2.Enabled = false;
                btnSaveData.Enabled = false;
                dataGrid1.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataGrid1.Columns[0].HeaderCell = dgvColumnHeader;
                dataGrid1.Columns[0].HeaderText = "";
                //------------------------------------------------------------
                iInquiry = Common.iSelect;
                ////Getcombobox();
                if (!objGetData.FillDataComboBox(cbType, "CONTENT", "CDVAL", "ALLCODE",
                    "GWTYPE='VCB' AND CDNAME='Type'", "CONTENT", true, false, ""))
                    return;
                btnSaveData.Enabled = false;
                //this.txtpath.Text = Application.StartupPath  + @"\Map1.xls"; 
                _strExcelFilename = this.txtpath.Text;
                if (System.IO.File.Exists(this.txtpath.Text))
                    //txtpath.Text = "";
                    RetrieveSheetnames();               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
		}
       
        ////private void Getcombobox()
        ////{
        ////    try
        ////    {
        ////        //-----lay du lieu len combobox Type------------------
        ////        string strCdname = "Type";
        ////        string strGưtype = "VCB";
        ////        DataSet datCombo = new DataSet();
        ////        datCombo = objCtrolall.GetALLCODE2(strCdname, strGưtype);
        ////        cbType.DataSource = datCombo.Tables[0];
        ////        cbType.DisplayMember = "CONTENT";
        ////        cbType.ValueMember = "CDVAL";
        ////        //--------------------------------------------------------                
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
        ////    }
        ////}

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
				MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);    
			}
		}

       

		private void btnOpenFileDlg_Click(object sender, System.EventArgs e)
		{
            try
            {
                OpenFileDialog f = new OpenFileDialog();

                f.InitialDirectory = Application.ExecutablePath;

                if (f.ShowDialog() == DialogResult.OK)
                    if (f.FileName != null && f.CheckFileExists == true)
                    {
                        this._strExcelFilename = f.FileName;
                        this.txtpath.Text = f.FileName;
                        RetrieveSheetnames();
                        if (this.cboSheetnames.Items.Count > 0)
                            cboSheetnames.SelectedIndex = 0;
                    }
                if (txtpath.Text.Trim() == "")
                {
                    btnGetData.Enabled = false;
                }
                else
                {
                    btnGetData.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
		}
        //---Kiem soat ReadOnly cua cac cot
        private void ColumnsRead(DataGridView Datagrid)
        {
            try
            {
                int b = 1;
                while (b < Datagrid.Columns.Count)
                {
                    Datagrid.Columns[b].ReadOnly = true;
                    b = b + 1;
                }
            }
            catch
            {
            }
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
            iDelete = 1;
            try
            {
                //--------------dung vong lap giai phong cac bien mang ve rong-----
                int g = 0;
                while (g < 17)
                {
                    Excel[g] = "";
                    Excel_Colums[g] = "";
                    Excel_Colums_M[g] = "";
                    g = g + 1;
                }                
                DataTable datMsg = new DataTable();
                if (cbType.Text.Trim() == "MT103")
                {
                    datMsg = objctrlMSG.MSG_XLS_VCB("VCB", "EX103", cbbDirection.Text.Trim());
                }
                else if (cbType.Text.Trim() == "Salary")
                {
                    datMsg = objctrlMSG.MSG_XLS_VCB("VCB", "EXSAL", cbbDirection.Text.Trim());
                }
                Columns = datMsg.Rows.Count;//lay ra so cot quy dinh cho tung loai file Excel
                //-----------------------------------------------------------------
                int h = 0;
                while (h < Columns)
                {
                    if (datMsg.Rows[h]["CHK"].ToString().Trim() == "M")
                    {
                        Excel_Colums_M[h + 1] = datMsg.Rows[h]["XLSCOL"].ToString().Trim();
                    }
                    Excel_Colums[h + 1] = datMsg.Rows[h]["CHK"].ToString();
                    h = h + 1;
                }
                Cursor = Cursors.WaitCursor;
                InitExcel(ref _exr);
                _dt = _exr.GetTable(Columns);               
                _exr.GetValue(txtCell.Text);
                if (_dt.Columns.Count >= Columns)//so cot trong file Excel nho hon so cot quy dinh bao loi
                {
                    int m = Columns;
                    while (m < _dt.Columns.Count)
                    {
                        _dt.Columns.RemoveAt(m);                       
                    }

                    if (_dt.Columns.Count <= 17)
                    {
                        this.dataGrid1.DataSource = _dt;
                        label5.Text = "Total number of messages :" + _dt.Rows.Count;
                        //--gan nhan check all vao he thong------------------------   
                        ColumnsRead(dataGrid1);
                        Columns_Header();
                        dataGrid1.Columns[0].ReadOnly = false;
                        //if (cbType.Text.Trim() == "MT103")
                        //{
                        //    dataGrid1.Columns["F5"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                        //}
                        //else
                        //{
                        //    dataGrid1.Columns["F4"].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                        //}
                        dataGrid1.Columns[0].Width = 30;
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
                        Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);                       
                    }
                }
                else if (_dt.Columns.Count < Columns)
                {
                    Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);
                }
                //-----kiem tra du lieu tren luoi-----------------------------------
                if (dataGrid1.Rows.Count == 0)
                {
                    button2.Enabled = false;
                    btnSaveData.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                    btnSaveData.Enabled = true;
                }
                _exr.Close();
                _exr.Dispose();
                _exr = null;
                Cursor = Cursors.Default;
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
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);                
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
        private void Check_VCB_CCYCD()
        {
            try
            {                
                iCCYCD_RETURN = 0;
                //--Lay du lieu cua bang dinh nghia loai tien te de xac dinh du lieu co dung khong
                DataTable datVCB = new DataTable();
                datVCB = ObjCCYCD.GetCurrency_Import("VCB");
                //-------------------------------------------------------------------------------
                int f = 0;
                while (f < dataGrid1.Rows.Count)
                {
                    iCCYCD = 0;
                    int d = 0;
                    while (d < datVCB.Rows.Count)
                    {
                        if (dataGrid1.Rows[f].Cells[Convert.ToInt32(strColumns_Currency.Replace("F", ""))].Value.ToString() == datVCB.Rows[d]["CCYCD"].ToString())
                        {
                            iCCYCD = 1;                           
                        }                        
                        d = d + 1;
                    }
                    if (iCCYCD == 1)
                    {
                        iCCYCD_RETURN = 0;
                    }
                    else
                    {
                        iCCYCD_RETURN = 1;
                        break;
                    }
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //goi ham ghi du lieu sang bang Excel
		private void btnSaveData_Click(object sender, System.EventArgs e)
		{
            iTong = 0;
            iError = 0;
            pSENDERS_REF = "";
            Check_Null();
            try
            {
                string Msg = "Do you want to save messages?";
                string title = Common.sCaption;
                DialogResult DlgResult = new DialogResult();
                DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DlgResult == DialogResult.Yes)
                {
                    //-------------kiem tra toan bo cac cot cua file--------------------
                    if (iColumns == false)//cac cot thoa man dien kien khong duoc rong
                    {
                        //------------------------------------------------------------------
                        //---kiem tra xem ---cot dung dinh dang khong---------------------
                        if (cbType.Text.Trim() == "MT103")
                        {
                            DataTable datF = new DataTable();
                            datF = objctrlMSG.COLUMNS_MSG_XLS("VCB", "EX103", "AMOUNT", cbbDirection.Text.Trim());
                            strColumns_Amount = datF.Rows[0]["XLSCOL"].ToString().Trim();
                            Check_columns_amount(datF.Rows[0]["XLSCOL"].ToString().Trim());//so tiem
                            //----------------------------------------------------------------------
                            DataTable datF1 = new DataTable();
                            datF1 = objctrlMSG.COLUMNS_MSG_XLS("VCB", "EX103", "CURRENCY", cbbDirection.Text.Trim());
                            strColumns_Currency = datF1.Rows[0]["XLSCOL"].ToString().Trim();
                            Check_columns_ccycd(datF1.Rows[0]["XLSCOL"].ToString().Trim());//loai tien//objctrlMSG
                            //-----------------------------------------------------------------------
                            DataTable datF4 = new DataTable();
                            datF4 = objctrlMSG.COLUMNS_MSG_XLS("VCB", "EX103", "SENDERS REF", cbbDirection.Text.Trim());//Senders Ref
                            pSENDERS_REF = datF4.Rows[0]["XLSCOL"].ToString().Trim();
                        }
                        else
                        {
                            DataTable datF2 = new DataTable();
                            datF2 = objctrlMSG.COLUMNS_MSG_XLS("VCB", "EXSAL", "AMOUNT", cbbDirection.Text.Trim());
                            strColumns_Amount = datF2.Rows[0]["XLSCOL"].ToString().Trim();
                            Check_columns_amount(datF2.Rows[0]["XLSCOL"].ToString().Trim());//loai tien//Check_columns_amount
                            //------------------------------------------------------------------------
                            DataTable datF3 = new DataTable();
                            datF3 = objctrlMSG.COLUMNS_MSG_XLS("VCB", "EXSAL", "CURRENCY", cbbDirection.Text.Trim());
                            strColumns_Currency = datF3.Rows[0]["XLSCOL"].ToString().Trim();
                            Check_columns_ccycd(datF3.Rows[0]["XLSCOL"].ToString().Trim());//so tiem
                            //-------------------------------------------------------------------------
                            DataTable datF5 = new DataTable();
                            datF5 = objctrlMSG.COLUMNS_MSG_XLS("VCB", "EX103", "SENDERS REF", cbbDirection.Text.Trim());//Senders Ref
                            pSENDERS_REF = datF5.Rows[0]["XLSCOL"].ToString().Trim();
                        }
                        if (iAmount == 1 || iCcycd == 1)//khong dung dinh dang
                        {
                            Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);                            
                            return;
                        }
                        else //dung dinh dang
                        {
                            Check_VCB_CCYCD();
                            if (iCCYCD_RETURN == 0)//dung dinh dang(co mot laoi tien te khong duoc dinh nghia trong du lieu)
                            {
                                iTong = 0;
                                Check_Select_Rows();//goi ham kiem tra xem co hang nao duoc chon khong
                                if (iSelect == 1)
                                {
                                    Check_Message_Exit();//check trung toan bo
                                    if (Check_Message == 0)
                                    {
                                        if (iSave == 1)//lay du lieu day vao bang
                                        {
                                            //--kiem tra do dai ten file----------------------------
                                            string strFILEE = Path.GetFileName(txtpath.Text);
                                            if (strFILEE.Length > 30)
                                            {
                                                Common.ShowError("The excel file name is not allow lengther than 30 character!", 3, MessageBoxButtons.OK);                                               
                                            }
                                            else
                                            {
                                                SavaExcel();
                                            }                                            
                                            if (dataGrid1.Rows.Count == 0)
                                            {
                                                btnSaveData.Enabled = false;
                                                button2.Enabled = false;
                                            }
                                        }                                       
                                    }
                                    else
                                    {
                                        string Msg1 = Check_Message_Count + " :Message has already exist!" + "\r\n" + " Do you want to proceed?";
                                        string title1 = Common.sCaption;
                                        DialogResult DlgResult1 = new DialogResult();
                                        DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Information);                                        
                                        if (DlgResult1 == DialogResult.Yes)
                                        {   //goi ham insert lai dien trung
                                            SavaExcel();
                                        }
                                        else
                                        {
                                            Check_Message_Exit_Null();
                                        }
                                    }
                                }
                                else
                                {
                                    Common.ShowError("No message is selected!", 2, MessageBoxButtons.OK);                                   
                                }
                            }
                            else if (iCCYCD_RETURN == 1)
                            {
                                Common.ShowError("Invalid the format of excel file!" + "\r\n" + "Invalid currency!", 3, MessageBoxButtons.OK);                                
                            }
                        }
                    }
                    else
                    {
                        Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);                                                        
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);   
            }
		}
        private void SaveBrauto()//ham day du lieu vao bang SWIFT_BR_AUTO
        {
            string strWhere;
            string strtmtref; string strtmprbr;
            try
            {
                if (dataGrid1.Rows.Count != 0)//neu  co du lieu
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
                                        dataGrid1.Rows.RemoveAt(f);                                        
                                        //------------------------------------------------------------
                                        ObjAuto.TMTREF = Br_auto[1];
                                        ObjAuto.TMPRBR = Br_auto[2];
                                        //---kiem tra xem co bi trung du lieu khong                                        
                                        if (Br_auto[1] == "") { strtmtref = ""; } else { strtmtref = "  Trim(tmtref) ='" + Br_auto[1].Trim() + "'"; }
                                        if (Br_auto[2] == "") { strtmprbr = ""; } else { strtmprbr = " and  Trim(tmprbr) ='" + Br_auto[2].Trim() + "'"; }
                                        strWhere = "  Where  " + strtmtref + strtmprbr;
                                        DataTable datBr = new DataTable();
                                        datBr = objCtrlauto.Check_Br(strWhere);
                                        if (datBr.Rows.Count == 0)//neu khong co du lieu trung
                                        {
                                            if (objCtrlauto.AddSWIFT_BR_AUTO(ObjAuto) == 1)
                                            {
                                                Common.ShowError("Message has just import successfull!", 3, MessageBoxButtons.OK);                                                
                                            }
                                            else
                                            {
                                                Common.ShowError("Message has not import !", 3, MessageBoxButtons.OK);                                               
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
                            dataGrid1.DataSource = _dt;
                            if (dataGrid1.Rows.Count == 0)
                            {
                                cmdDelete.Enabled = false;
                                cmdSave.Enabled = false;
                            }
                        }
                        else if (iRowscount > 1)
                        {
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
                                        dataGrid1.Rows.RemoveAt(f);                                        
                                        ObjAuto.TMTREF = Br_auto[1];
                                        ObjAuto.TMPRBR = Br_auto[2];
                                         if (Br_auto[1] == "") { strtmtref = ""; } else { strtmtref = "  Trim(tmtref) ='" + Br_auto[1].Trim() + "'"; }
                                        if (Br_auto[2] == "") { strtmprbr = ""; } else { strtmprbr = " and  Trim(tmprbr) ='" + Br_auto[2].Trim() + "'"; }
                                        strWhere = "  Where  " + strtmtref + strtmprbr;
                                        DataTable datBr = new DataTable();
                                        datBr = objCtrlauto.Check_Br(strWhere);
                                        if (datBr.Rows.Count == 0)//neu khong co du lieu trung
                                        {
                                            if (objCtrlauto.AddSWIFT_BR_AUTO(ObjAuto) == 1)
                                            {                                                
                                                iTong = iTong + 1;
                                            }
                                            else
                                            {
                                                Cells_columns = Cells_columns + "\r\n" + Br_auto[1] + "--" + Br_auto[2];
                                                iError = 1;
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
                                Common.ShowError(iTong + " Message has just import successfull!", 3, MessageBoxButtons.OK);                                
                            }
                            else
                            {
                                string Msg =iTong + " Message has just import successfull!" + "\r\n" + " Do you want view message import error ?";
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
                            dataGrid1.DataSource = _dt;
                            if (dataGrid1.Rows.Count == 0)
                            {
                                cmdDelete.Enabled = false;
                                cmdSave.Enabled = false;
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
        private void Check_Message_Exit()
        {
            try
            {
                string strWhere = "";
                string strWhere1 = "";
                string strF1; string strF2; string strF3; string strF4; 
                string strF5; string strF6; string strF7; string strF8; 
                string strF9; string strF10; string strF11; string strF12; 
                string strF13; string strF14; string strF15; string strF16; 
                string strF17; string strmsg_direction; 
                //string strFfile_name; 
                //string strFstatus; 
                //string strTrans_date; 
                //string strFgwtype; 
                //string strtype;
                Check_Message_Count = 0;
                Check_Message = 0;
                int f = 0;
                while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                {
                    if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                        {                            
                                int m = 1;
                                while (m < 17)//khong duyet tung cot (duyet theo mang da co san va lay ten cot de lay du lieu)
                                {
                                    if (Excel_Colums_M[m].Trim() == "")
                                    {
                                    }
                                    else
                                    {
                                        Excel[m] = dataGrid1.Rows[f].Cells[Excel_Colums_M[m].Trim()].Value.ToString();//lay du klieu vao mang Excel                                       
                                    }
                                    m = m + 1;
                                }                           
                                if (Excel[1] == "") { strF1 = ""; } else { strF1 = "   and  trim(F1) ='" + Excel[1].Trim() + "'"; }
                                if (Excel[2] == "") { strF2 = ""; } else { strF2 = " and  Trim(F2) ='" + Excel[2].Trim().Replace("'", "") + "'"; }
                                if (Excel[3] == "") { strF3 = ""; } else { strF3 = " and  Trim(F3) ='" + Excel[3].Trim().Replace("'", "") + "'"; }
                                if (Excel[4] == "") { strF4 = ""; } else { strF4 = " and  Trim(F4) ='" + Excel[4].Trim().Replace("'", "") + "'"; }
                                if (Excel[5] == "") { strF5 = ""; } else { strF5 = " and  Trim(F5) ='" + Excel[5].Trim().Replace("'", "") + "'"; }
                                if (Excel[6] == "") { strF6 = ""; } else { strF6 = " and  Trim(F6) ='" + Excel[6].Trim().Replace("'", "") + "'"; }
                                if (Excel[7] == "") { strF7 = ""; } else { strF7 = " and  Trim(F7) ='" + Excel[7].Trim().Replace("'", "") + "'"; }
                                if (Excel[8] == "") { strF8 = ""; } else { strF8 = " and  Trim(F8) ='" + Excel[8].Trim().Replace("'", "") + "'"; }
                                if (Excel[9] == "") { strF9 = ""; } else { strF9 = " and  Trim(F9) ='" + Excel[9].Trim().Replace("'", "") + "'"; }
                                if (Excel[10] == "") { strF10 = ""; } else { strF10 = " and  Trim(F10) ='" + Excel[10].Trim().Replace("'", "") + "'"; }
                                if (Excel[11] == "") { strF11 = ""; } else { strF11 = " and  Trim(F11) ='" + Excel[11].Trim().Replace("'", "") + "'"; }
                                if (Excel[12] == "") { strF12 = ""; } else { strF12 = " and  Trim(F12) ='" + Excel[12].Trim().Replace("'", "") + "'"; }
                                if (Excel[13] == "") { strF13 = ""; } else { strF13 = " and  Trim(F13) ='" + Excel[13].Trim().Replace("'", "") + "'"; }
                                if (Excel[14] == "") { strF14 = ""; } else { strF14 = " and  Trim(F14) ='" + Excel[14].Trim().Replace("'", "") + "'"; }
                                if (Excel[15] == "") { strF15 = ""; } else { strF15 = " and  Trim(F15) ='" + Excel[15].Trim().Replace("'", "") + "'"; }
                                if (Excel[16] == "") { strF16 = ""; } else { strF16 = " and  Trim(F16) ='" + Excel[16].Trim().Replace("'", "") + "'"; }
                                if (Excel[17] == "") { strF17 = ""; } else { strF17 = " and  Trim(F17) ='" + Excel[17].Trim().Replace("'", "") + "'"; }
                            strmsg_direction = "  and  trim(MSG_DIRECTION) = '" + cbbDirection.Text.Trim() + "'";
                            //strFfile_name = " and  Trim(file_name) ='" + Path.GetFileName(txtpath.Text) + "'";//file_name
                            strWhere = strF1 + strF2 + strF3 + strF5 + strF6 + strF7 + strF8 + strF9 + strF10 + strF11 + strF12 + strF13 + strF14 + strF15 + strF16 + strF17 + strmsg_direction;
                            strWhere1 ="  Where "+ strWhere.Substring(6);
                            DataTable datCheck = new DataTable();
                            datCheck = objctrolExcel.Check_Excel_VCB(strWhere1);
                            if (datCheck.Rows.Count != 0)//co du lieu trung thi khong cho Import vao trong database
                            {
                                Check_Message_Count = Check_Message_Count + 1;
                                Check_Message = 1;                                
                            }
                        }
                    }
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void Check_Message_Exit_Null()
        {
            try
            {
                string strWhere = "";
                string strWhere1 = "";
                string strF1; 
                string strF2; 
                string strF3; 
                string strF4; 
                string strF5; 
                string strF6; 
                string strF7; 
                string strF8; 
                string strF9; 
                string strF10; 
                string strF11; 
                string strF12; 
                string strF13; 
                string strF14; 
                string strF15; 
                string strF16; 
                string strF17; 
                //string strFfile_name; 
                //string strFstatus; 
                //string strFgwtype; 
                string strmsg_direction; 
                //string strtype; 
                //string strTrans_date;
                Check_Message_Count = 0;
                Check_Message = 0;
                int f = 0;
                while (f < dataGrid1.Rows.Count)//duyet hang cot truoc
                {
                    if (dataGrid1.Rows[f].Cells[0].Value != null)// hang duoc chon
                    {
                        if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            
                                int m = 1;
                                while (m < 17)//khong duyet tung cot (duyet theo mang da co san va lay ten cot de lay du lieu)
                                {
                                    if (Excel_Colums_M[m].Trim() == "")
                                    {
                                    }
                                    else
                                    {
                                        Excel[m] = dataGrid1.Rows[f].Cells[Excel_Colums_M[m].Trim()].Value.ToString();//lay du klieu vao mang Excel                                       
                                    }
                                    m = m + 1;
                                }                           
                                if (Excel[1] == "") { strF1 = ""; } else { strF1 = "   and  trim(F1) ='" + Excel[1].Trim() + "'"; }
                            if (Excel[2] == "") { strF2 = ""; } else { strF2 = " and  Trim(F2) ='" + Excel[2].Trim() + "'"; }
                            if (Excel[3] == "") { strF3 = ""; } else { strF3 = " and  Trim(F3) ='" + Excel[3].Trim() + "'"; }
                            if (Excel[4] == "") { strF4 = ""; } else { strF4 = " and  Trim(F4) ='" + Excel[4].Trim() + "'"; }
                            if (Excel[5] == "") { strF5 = ""; } else { strF5 = " and  Trim(F5) ='" + Excel[5].Trim() + "'"; }
                            if (Excel[6] == "") { strF6 = ""; } else { strF6 = " and  Trim(F6) ='" + Excel[6].Trim() + "'"; }
                            if (Excel[7] == "") { strF7 = ""; } else { strF7 = " and  Trim(F7) ='" + Excel[7].Trim() + "'"; }
                            if (Excel[8] == "") { strF8 = ""; } else { strF8 = " and  Trim(F8) ='" + Excel[8].Trim() + "'"; }
                            if (Excel[9] == "") { strF9 = ""; } else { strF9 = " and  Trim(F9) ='" + Excel[9].Trim() + "'"; }
                            if (Excel[10] == "") { strF10 = ""; } else { strF10 = " and  Trim(F10) ='" + Excel[10].Trim() + "'"; }
                            if (Excel[11] == "") { strF11 = ""; } else { strF11 = " and  Trim(F11) ='" + Excel[11].Trim() + "'"; }
                            if (Excel[12] == "") { strF12 = ""; } else { strF12 = " and  Trim(F12) ='" + Excel[12].Trim() + "'"; }
                            if (Excel[13] == "") { strF13 = ""; } else { strF13 = " and  Trim(F13) ='" + Excel[13].Trim() + "'"; }
                            if (Excel[14] == "") { strF14 = ""; } else { strF14 = " and  Trim(F14) ='" + Excel[14].Trim() + "'"; }
                            if (Excel[15] == "") { strF15 = ""; } else { strF15 = " and  Trim(F15) ='" + Excel[15].Trim() + "'"; }
                            if (Excel[16] == "") { strF16 = ""; } else { strF16 = " and  Trim(F16) ='" + Excel[16].Trim() + "'"; }
                            if (Excel[17] == "") { strF17 = ""; } else { strF17 = " and  Trim(F17) ='" + Excel[17].Trim() + "'"; }
                            strmsg_direction = "  and  trim(MSG_DIRECTION) = '" + cbbDirection.Text.Trim() + "'";
                            
                            strWhere = strF1 + strF2 + strF3 + strF5 + strF6 + strF7 + strF8 + strF9 + strF10 + strF11 + strF12 + strF13 + strF14 + strF15 + strF16 + strF17 + strmsg_direction;
                            strWhere1 ="  Where "+ strWhere.Substring(6);
                            DataTable datCheck = new DataTable();
                            datCheck = objctrolExcel.Check_Excel_VCB(strWhere1);
                            if (datCheck.Rows.Count != 0)//co du lieu trung thi khong cho Import vao trong database
                            {
                                Check_Message_Count = Check_Message_Count + 1;
                                Check_Message = 1;                               
                            }
                            else
                            {
                                dataGrid1.Rows[f].Cells[0].Value = null;
                            }
                        }
                    }
                    f = f + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        private void SavaExcel()//day du lieu vao bang Excel
        {
            //int iCurrency;
            int iCOUNTNT = 0;
            string strF1; string strF2; string strF3; string strF4; 
            string strF5; string strF6; string strF7; string strF8; 
            string strF9; string strF10; string strF11; string strF12; 
            string strF13; string strF14; string strF15; string strF16; 
            string strF17; 
            //string strFfile_name; 
            //string strFstatus; 
            //string strFgwtype; 
            string strmsg_direction; 
            //string strtype; 
            //string strTrans_date;
            string strWhere;
            try
            {
                //--lay du lieu tu bang ALLCODE-----------
                DataTable datT = new DataTable();
                datT = objCtrolall.GetALLCODE_code1(cbType.Text.Trim(), "Type");
                string strCDVAL = datT.Rows[0]["CDVAL"].ToString();
                //--------------------------------------------------------------
                if (dataGrid1.Rows.Count == 0)
                {
                }
                else
                {
                    //-------------------------------------------------------------------
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
                                    //----------------Xoa cac dong da insert-----------------------
                                    //--kiem tra kieu tien va loai tien khong dung dinh dang bo qua--------
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
                                        objExcel.FILE_NAME = Path.GetFileName(txtpath.Text);
                                        //objExcel.TRANS_DATE = Sysdate;
                                        objExcel.STATUS = 0;
                                        objExcel.GWTYPE = Common.gGWTYPE;
                                        objExcel.MSG_DIRECTION = cbbDirection.Text.Trim();//"SIBS-VCB";// strDIREC;
                                        objExcel.TYPE = strCDVAL;
                                        objExcel.TELLER_ID = Common.Userid;
                                        objExcel.OFFICER_ID = "";
                                        //check trung
                                        if (Excel[1] == "") { strF1 = ""; } else { strF1 = " trim(F1) ='" + Excel[1].Trim() + "'"; }
                                        if (Excel[2] == "") { strF2 = ""; } else { strF2 = " and  Trim(F2) ='" + Excel[2].Trim() + "'"; }
                                        if (Excel[3] == "") { strF3 = ""; } else { strF3 = " and  Trim(F3) ='" + Excel[3].Trim() + "'"; }
                                        if (Excel[4] == "") { strF4 = ""; } else { strF4 = " and  Trim(F4) ='" + Excel[4].Trim() + "'"; }
                                        if (Excel[5] == "") { strF5 = ""; } else { strF5 = " and  Trim(F5) ='" + Excel[5].Trim() + "'"; }
                                        if (Excel[6] == "") { strF6 = ""; } else { strF6 = " and  Trim(F6) ='" + Excel[6].Trim() + "'"; }
                                        if (Excel[7] == "") { strF7 = ""; } else { strF7 = " and  Trim(F7) ='" + Excel[7].Trim() + "'"; }
                                        if (Excel[8] == "") { strF8 = ""; } else { strF8 = " and  Trim(F8) ='" + Excel[8].Trim() + "'"; }
                                        if (Excel[9] == "") { strF9 = ""; } else { strF9 = " and  Trim(F9) ='" + Excel[9].Trim() + "'"; }
                                        if (Excel[10] == "") { strF10 = ""; } else { strF10 = " and  Trim(F10) ='" + Excel[10].Trim() + "'"; }
                                        if (Excel[11] == "") { strF11 = ""; } else { strF11 = " and  Trim(F11) ='" + Excel[11].Trim() + "'"; }
                                        if (Excel[12] == "") { strF12 = ""; } else { strF12 = " and  Trim(F12) ='" + Excel[12].Trim() + "'"; }
                                        if (Excel[13] == "") { strF13 = ""; } else { strF13 = " and  Trim(F13) ='" + Excel[13].Trim() + "'"; }
                                        if (Excel[14] == "") { strF14 = ""; } else { strF14 = " and  Trim(F14) ='" + Excel[14].Trim() + "'"; }
                                        if (Excel[15] == "") { strF15 = ""; } else { strF15 = " and  Trim(F15) ='" + Excel[15].Trim() + "'"; }
                                        if (Excel[16] == "") { strF16 = ""; } else { strF16 = " and  Trim(F16) ='" + Excel[16].Trim() + "'"; }
                                        if (Excel[17] == "") { strF17 = ""; } else { strF17 = " and  Trim(F17) ='" + Excel[17].Trim() + "'"; }
                                        strmsg_direction = "  and  trim(MSG_DIRECTION) = '" + cbbDirection.Text.Trim() + "'";
                                        //strFfile_name = " and  Trim(file_name) ='" + Path.GetFileName(txtpath.Text) + "'";//file_name

                                        strWhere = "  Where  " + strF1 + strF2 + strF3 + strF5 + strF6 + strF7 + strF8 + strF9 + strF10 + strF11 + strF12 + strF13 + strF14 + strF15 + strF16 + strF17 + strmsg_direction;
                                        DataTable datCheck = new DataTable();
                                        datCheck = objctrolExcel.Check_Excel_VCB(strWhere);
                                        if (objctrolExcel.AddExcel(objExcel) == 1)
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
                                        objExcel.FILE_NAME = Path.GetFileName(txtpath.Text);
                                        //objExcel.TRANS_DATE = DateTime.Now;
                                        objExcel.STATUS = 0;
                                        objExcel.GWTYPE = Common.gGWTYPE;
                                        objExcel.MSG_DIRECTION = cbbDirection.Text.Trim(); //"SIBS-VCB";// strDIREC;
                                        objExcel.TYPE = strCDVAL;
                                        objExcel.TELLER_ID = Common.Userid;
                                        objExcel.OFFICER_ID = "";
                                        //--kiem tra du lieu co trung khong---------------

                                        if (Excel[1] == "") { strF1 = ""; } else { strF1 = "   trim(F1) ='" + Excel[1].Trim() + "'"; }
                                        if (Excel[2] == "") { strF2 = ""; } else { strF2 = " and  Trim(F2) ='" + Excel[2].Trim() + "'"; }
                                        if (Excel[3] == "") { strF3 = ""; } else { strF3 = " and  Trim(F3) ='" + Excel[3].Trim() + "'"; }
                                        if (Excel[4] == "") { strF4 = ""; } else { strF4 = " and  Trim(F4) ='" + Excel[4].Trim() + "'"; }
                                        if (Excel[5] == "") { strF5 = ""; } else { strF5 = " and  Trim(F5) ='" + Excel[5].Trim() + "'"; }
                                        if (Excel[6] == "") { strF6 = ""; } else { strF6 = " and  Trim(F6) ='" + Excel[6].Trim() + "'"; }
                                        if (Excel[7] == "") { strF7 = ""; } else { strF7 = " and  Trim(F7) ='" + Excel[7].Trim() + "'"; }
                                        if (Excel[8] == "") { strF8 = ""; } else { strF8 = " and  Trim(F8) ='" + Excel[8].Trim() + "'"; }
                                        if (Excel[9] == "") { strF9 = ""; } else { strF9 = " and  Trim(F9) ='" + Excel[9].Trim() + "'"; }
                                        if (Excel[10] == "") { strF10 = ""; } else { strF10 = " and  Trim(F10) ='" + Excel[10].Trim() + "'"; }
                                        if (Excel[11] == "") { strF11 = ""; } else { strF11 = " and  Trim(F11) ='" + Excel[11].Trim() + "'"; }
                                        if (Excel[12] == "") { strF12 = ""; } else { strF12 = " and  Trim(F12) ='" + Excel[12].Trim() + "'"; }
                                        if (Excel[13] == "") { strF13 = ""; } else { strF13 = " and  Trim(F13) ='" + Excel[13].Trim() + "'"; }
                                        if (Excel[14] == "") { strF14 = ""; } else { strF14 = " and  Trim(F14) ='" + Excel[14].Trim() + "'"; }
                                        if (Excel[15] == "") { strF15 = ""; } else { strF15 = " and  Trim(F15) ='" + Excel[15].Trim() + "'"; }
                                        if (Excel[16] == "") { strF16 = ""; } else { strF16 = " and  Trim(F16) ='" + Excel[16].Trim() + "'"; }
                                        if (Excel[17] == "") { strF17 = ""; } else { strF17 = " and  Trim(F17) ='" + Excel[17].Trim() + "'"; }
                                        strmsg_direction = "  and  trim(MSG_DIRECTION) = '" + cbbDirection.Text.Trim() + "'";
                                        //--------------------------------------------------------------------------------------------------------
                                        strWhere = "  Where  " + strF1 + strF2 + strF3 + strF5 + strF6 + strF7 + strF8 + strF9 + strF10 + strF11 + strF12 + strF13 + strF14 + strF15 + strF16 + strF17 + strmsg_direction;

                                        DataTable datCheck = new DataTable();
                                        datCheck = objctrolExcel.Check_Excel_VCB(strWhere);
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
                            if (iExisted == 0)//khong co du lieu nao trung
                            {
                                Common.ShowError(iTong + "  Message has just import successfull!", 1, MessageBoxButtons.OK);
                            }
                            else if (iExisted != 0)
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
                        else//co it nhat mot loi
                        {
                            if (iExisted == 0)//khong co du lieu nao trung
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
                                else
                                {

                                }
                            }
                            else if (iExisted != 0)//Do you want to proceed?
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
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            label5.Text = "Total number of messages : " + dataGrid1.Rows.Count;
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
                                objExcel.FILE_NAME = Path.GetFileName(txtpath.Text);
                                objExcel.TRANS_DATE = DateTime.Now;
                                objExcel.STATUS = 0;
                                objExcel.GWTYPE = Common.gGWTYPE;
                                objExcel.MSG_DIRECTION = cbbDirection.Text.Trim(); //"SIBS-VCB";// strDIREC;
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
                    Common.ShowError(iTong + "  Message has just import successfull!", 2, MessageBoxButtons.OK);                   
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
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        //xoa dong du lieu duoc chon
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
                        else if (iDelete == 1)//delete tren luoi(hoi cu chuoi nhung giai phap tinh the)
                        {

                            int kk = 0;
                            while (kk < dataGrid1.Rows.Count)// duyet tung ban ghi trong Luoi
                            {
                                if (dataGrid1.Rows[kk].Cells[0].Value != null)// hang duoc chon
                                {
                                    if (dataGrid1.Rows[kk].Cells[0].Value.ToString() == "True")// dong duoc chon
                                    {                                       
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
                           // dataGrid1.DataSource = _dt;
                            label5.Text = "Total number of messages :" + dataGrid1.Rows.Count;
                            if (dataGrid1.Rows.Count == 0)
                            {
                                cmdDelete.Enabled = false;
                                button2.Enabled = false;
                                
                                btnSaveData.Enabled = false;
                                cmdSave.Enabled = false;
                            }
                        }                        
                    }
                }
                else
                {
                    Common.ShowError("No message is selected!", 2, MessageBoxButtons.OK);                   
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }

        }
        //trang thai Appro
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void dgView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dataGrid1.Rows.Count; i++)
                {
                    dataGrid1.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                }
            }
        }

        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
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

        private void enterKey(object sender, KeyPressEventArgs e)
        {
            //khi nhan phim ESC
            if (e.KeyChar == (char)27)
            {
                //frmCCYCDInfo_FormClosing(null,null);
                this.Close();
            }
        }

        private void frmVCBImp_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void frmVCBImp_KeyDown(object sender, KeyEventArgs e)
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
                    if ((this.ActiveControl as Button).Name == "btnGetData")
                    {
                        btnGetData.Focus();
                        btnGetData_Click(null, null);
                    }
                }
                if ((this.ActiveControl) is TextBox)
                {
                    (this.ActiveControl as TextBox).SelectAll();
                }

            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                // You can change C:\Members.xlsx to any path where 
                // the file is located.
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;
               Data Source=D:\NGAYNAYVANGAYMAI\FILE TEST IMPORT DIEN DI1.xlsx;Extended Properties=""Excel 12.0;HDR=YES;""";

                // if you don't want to show the header row (first row)
                // use 'HDR=NO' in the string

                string strSQL = "SELECT * FROM [Sheet1$]";

                OleDbConnection excelConnection = new OleDbConnection(connectionString);
                excelConnection.Open(); // This code will open excel file.

                OleDbCommand dbCommand = new OleDbCommand(strSQL, excelConnection);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(dbCommand);

                // create data table
                DataTable dTable = new DataTable();
                dataAdapter.Fill(dTable);               
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

        private void frmVCBImp_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }
	}  
}
