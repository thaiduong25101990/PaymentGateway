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

namespace BR.BRIBPS
{
	
	public class frmIBPSImpFile : frmBasedata
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
        private string pMSG_DIRECTION = "SIBS-IBPS";

        private string strColumns_Currency;
        private string strColumns_Amount;

        public int iSelect;
        BR.BRLib.Common.DGVColumnHeader dgvColumnHeader = new Common.DGVColumnHeader();
        //string[] Excel_Colums = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        //string[] Excel_Colums_M = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        //string[] Excel = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        //string[] Br_auto = { "", "", "" };

        private static DataTable datMsg = new DataTable();



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
		private System.Windows.Forms.Label lblFilefolder;
		private System.Windows.Forms.Label lblSheet;
		private System.Windows.Forms.ComboBox cboSheetnames;
		private System.Windows.Forms.TextBox txtCell;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox1;
		private int _intPKCol=-1;

        private int iDelete;


		#endregion

        private Button button2;
        private Label lblCountMessage;
        private DataGridView dataGrid1;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        

		#region CTOR
        public frmIBPSImpFile()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIBPSImpFile));
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
            this.lblFilefolder = new System.Windows.Forms.Label();
            this.btnOpenFileDlg = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblCountMessage = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.pnlCell.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
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
            this.btnGetData.Location = new System.Drawing.Point(789, 24);
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
            this.txtpath.Location = new System.Drawing.Point(133, 27);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(515, 23);
            this.txtpath.TabIndex = 2;
            // 
            // lblFilefolder
            // 
            this.lblFilefolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilefolder.Location = new System.Drawing.Point(30, 28);
            this.lblFilefolder.Name = "lblFilefolder";
            this.lblFilefolder.Size = new System.Drawing.Size(67, 20);
            this.lblFilefolder.TabIndex = 8;
            this.lblFilefolder.Text = "File path :";
            // 
            // btnOpenFileDlg
            // 
            this.btnOpenFileDlg.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFileDlg.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileDlg.Image")));
            this.btnOpenFileDlg.Location = new System.Drawing.Point(668, 25);
            this.btnOpenFileDlg.Name = "btnOpenFileDlg";
            this.btnOpenFileDlg.Size = new System.Drawing.Size(41, 26);
            this.btnOpenFileDlg.TabIndex = 3;
            this.btnOpenFileDlg.Text = "...";
            this.btnOpenFileDlg.Click += new System.EventHandler(this.btnOpenFileDlg_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtpath);
            this.groupBox1.Controls.Add(this.lblFilefolder);
            this.groupBox1.Controls.Add(this.btnOpenFileDlg);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(875, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import file excel";
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
            // lblCountMessage
            // 
            this.lblCountMessage.AutoSize = true;
            this.lblCountMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountMessage.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCountMessage.Location = new System.Drawing.Point(12, 85);
            this.lblCountMessage.Name = "lblCountMessage";
            this.lblCountMessage.Size = new System.Drawing.Size(174, 16);
            this.lblCountMessage.TabIndex = 21;
            this.lblCountMessage.Text = "Total number of messages :";
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.AllowUserToOrderColumns = true;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid1.ColumnHeadersHeight = 22;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid1.Location = new System.Drawing.Point(10, 104);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid1.Size = new System.Drawing.Size(875, 382);
            this.dataGrid1.TabIndex = 22;
            this.dataGrid1.TabStop = false;
            this.dataGrid1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid1_ColumnHeaderMouseClick);
            this.dataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellClick_1);
            // 
            // frmIBPSImpFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(895, 533);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.lblCountMessage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlCell);
            this.Controls.Add(this.btnSaveData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIBPSImpFile";
            this.Text = "IBPS Import file";
            this.Load += new System.EventHandler(this.frmReadWriteExcelDemo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterKey);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIBPSImpFile_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmIBPSImpFile_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIBPSImpFile_KeyDown);
            this.Controls.SetChildIndex(this.btnSaveData, 0);
            this.Controls.SetChildIndex(this.pnlCell, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdAdd, 0);
            this.Controls.SetChildIndex(this.cmdEdit, 0);
            this.Controls.SetChildIndex(this.cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.lblCountMessage, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.pnlCell.ResumeLayout(false);
            this.pnlCell.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		
		[STAThread]
		static void Main() 
		{
            Application.Run(new frmIBPSImpFile());
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
                //--------------------------------------------------------------
                cmdAdd.Visible = false;
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                cmdSave.Visible = false;
                btnSetPK.Enabled = false;
                pnlCell.Enabled = false;
                iDelete = 0;
                lblCountMessage.Text = "Total number of messages : 0";
                //------------------------------------------------------------
                btnGetData.Enabled = false;
                button2.Enabled = false;
                btnSaveData.Enabled = false;
                dataGrid1.Columns.Insert(0, new DataGridViewCheckBoxColumn());
                dataGrid1.Columns[0].HeaderCell = dgvColumnHeader;
                dataGrid1.Columns[0].HeaderText = "";
                //------------------------------------------------------------
                iInquiry = Common.iSelect;                
                btnSaveData.Enabled = false;                
                _strExcelFilename = this.txtpath.Text;
                if (System.IO.File.Exists(this.txtpath.Text))                    
                    RetrieveSheetnames();               
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
                { btnGetData.Enabled = false; }
                else
                { btnGetData.Enabled = true; }
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
            string pColAmount = "";
            try
            {

                datMsg = objctrlMSG.MSG_XLS_IBPS("IBPS", pMSG_DIRECTION);
                Columns = datMsg.Rows.Count;//So cot quy dinh cua file Excel             

                /* Lay du lieu trong file excel
                 *Day du lieu cua file excrel ra mot datatable*/
                Cursor = Cursors.WaitCursor;
                InitExcel(ref _exr);
                _dt = _exr.GetTable(Columns);
                _exr.GetValue(txtCell.Text);

                if (_dt.Columns.Count >= Columns)
                {
                    //Xoa du lieu nhung cot ma qua quy dinh
                    int m = Columns;
                    while (m < _dt.Columns.Count)
                    {
                        _dt.Columns.RemoveAt(m);
                    }
                    _dt.Rows[0].Delete();
                    this.dataGrid1.DataSource = _dt;
                    lblCountMessage.Text = "Total number of messages :" + _dt.Rows.Count;
                    //--gan nhan check all vao he thong------------------------   
                    ColumnsRead(dataGrid1);
                    Columns_Header();
                    dataGrid1.Columns[0].ReadOnly = false;
                    //Lay du lieu de format truong so tien theo dung dinh dang
                    for (int i = 0; i < datMsg.Rows.Count; i++)
                    {
                        string pFIELD = datMsg.Rows[i]["FIELD"].ToString();
                        if (pFIELD.Substring(1, 3).ToString() == "027")//truong tien
                        {
                            pColAmount = datMsg.Rows[i]["XLSCOL"].ToString().Trim();
                            break;
                        }
                    }
                    dataGrid1.Columns[pColAmount].DefaultCellStyle = Common.GetCelltypeNumber(Common.FORMAT_CURRENCY_DATAGRID);
                    dataGrid1.Columns[0].Width = 30;
                    // Lay ten len header cua luoi
                    int k = 1;
                    while (k < datMsg.Rows.Count)
                    {
                        dataGrid1.Columns["F" + (k + 1)].HeaderText = datMsg.Rows[k]["FIELD"].ToString();
                        k = k + 1;
                    }
                }
                else if (_dt.Columns.Count < Columns)
                {
                    Common.ShowError("Invalid the format of excel file!", 3, MessageBoxButtons.OK);
                }
                #region code -----------------------------------
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
                #endregion

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
        #region Check_Null()
        private void Check_Null()
        {           
            try
            {
                int m = 1;
                while (m < dataGrid1.Columns.Count-1)
                {
                    iColumns = false;
                    if (datMsg.Rows[m]["CHK"].ToString() == "M")
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
        #endregion

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
                    if (iColumns == false)
                    {
                        iTong = 0;
                        Check_Select_Rows();
                        if (iSelect == 1)
                        {
                            Check_Message_Exit();//Check trung
                            if (Check_Message == 0)
                            {
                                if (iSave == 1)//lay du lieu day vao bang
                                {                                   
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
                            else// Dien bi trung
                            {
                                string Msg1 = Check_Message_Count + " :Message has already exist!" + "\r\n" + " Do you want to proceed?";
                                string title1 = Common.sCaption;
                                DialogResult DlgResult1 = new DialogResult();
                                DlgResult1 = MessageBox.Show(Msg1, title1, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (DlgResult1 == DialogResult.Yes)
                                { 
                                    SavaExcel();
                                }                                
                            }
                        }
                        else
                        {
                            Common.ShowError("No message is selected!", 2, MessageBoxButtons.OK);
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
       
        private void Check_Message_Exit()
        {
            try
            {
                string pRelationNumber = "";
                string pCurrencyCode = "";
                string pTransferAmount = "";
                //Lay cac cot de check trung
                for(int i=0;i<datMsg.Rows.Count;i++)
                {
                    string pValues= datMsg.Rows[i]["FIELD"].ToString();
                    if (pValues.Substring(1, 4).ToString() == "#110")
                    {
                        pRelationNumber = datMsg.Rows[i]["XLSCOL"].ToString();
                    }
                    if (pValues.Substring(1, 4).ToString() == "#026")
                    {
                        pCurrencyCode = datMsg.Rows[i]["XLSCOL"].ToString();
                    }
                    if (pValues.Substring(1, 4).ToString() == "#027")
                    {
                        pTransferAmount = datMsg.Rows[i]["XLSCOL"].ToString();
                    }                    
                }

                string strWhere = "";
                Check_Message_Count = 0;
                Check_Message = 0;
                int f = 0;
                while (f < dataGrid1.Rows.Count)
                {
                    if (dataGrid1.Rows[f].Cells[0].Value != null)
                    {
                        if (dataGrid1.Rows[f].Cells[0].Value.ToString() == "True")
                        {
                            strWhere = " Where check_key = '" + dataGrid1.Rows[f].Cells[pRelationNumber] + dataGrid1.Rows[f].Cells[pCurrencyCode] + dataGrid1.Rows[f].Cells[pTransferAmount] + "' and FILE_TYPE = 'EXCEL'  ";
                            DataTable datCheck = new DataTable();
                            datCheck = objctrolExcel.Check_Excel_IBPS(strWhere);
                            if (datCheck.Rows.Count != 0)
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

        
        private void SavaExcel()//day du lieu vao bang Excel
        {           
            try
            {                
                if (dataGrid1.Rows.Count > 0)               
                {                    
                    iRowscount = 0;
                    Checked_Els();                    
                    if (iRowscount > 0)
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
                                    string pRN = "";
                                    string pCC = "";
                                    string pTA = "";
                                    //Lay du lieu de insert vao database
                                    string pCONTENT = "";
                                    string pFILE_NAME = Path.GetFileName(txtpath.Text);
                                    for (int j = 0; j <= datMsg.Rows.Count-1; j++)
                                    {
                                        string pFiel = datMsg.Rows[j]["FIELD"].ToString().Substring(1, 3);
                                        /* -----Lay du lieu checkey----------------------------------------*/
                                        if (pFiel == "110")
                                        {
                                            pRN = dataGrid1.Rows[f].Cells[j+1].Value.ToString();
                                        }
                                        if (pFiel == "026")
                                        {
                                            pCC = dataGrid1.Rows[f].Cells[j+1].Value.ToString();
                                        }
                                        if (pFiel == "027")
                                        {
                                            pTA = dataGrid1.Rows[f].Cells[j+1].Value.ToString();
                                        }
                                        /*--------------------------------------------------------------------*/
                                        pCONTENT = pCONTENT + "#" + pFiel + dataGrid1.Rows[f].Cells[j+1].Value.ToString();
                                    }
                                    string pCHECK_KEY = pRN + pCC + pTA;
                                    if (objctrolExcel.AddExcel_IBPS(pCHECK_KEY, pCONTENT, pFILE_NAME,Common.Userid,"EXCEL") == 1)
                                    {
                                        iTong = iTong + 1;
                                        dataGrid1.Rows.RemoveAt(f);
                                    }
                                    else
                                    {
                                        Cells_columns = Cells_columns + "\r\n" + pCONTENT;
                                        iError = 1;
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
                        if (iError == 0)
                        {
                            Common.ShowError(iTong + "  Message has just import successfull!", 1, MessageBoxButtons.OK);
                        }
                        else
                        {
                            string Msg = iTong + " Message has just import successfull!" + "\r\n" + " Do you want view message import error ?";
                            string title = Common.sCaption;
                            DialogResult DlgResult = new DialogResult();
                            DlgResult = MessageBox.Show(Msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (DlgResult == DialogResult.Yes)
                            {
                                BR.BRIBPS.frmMessageImportError frmerror = new BR.BRIBPS.frmMessageImportError();
                                frmerror.strCells_columns = Cells_columns;
                                frmerror.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
            lblCountMessage.Text = "Total number of messages : " + dataGrid1.Rows.Count;
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
                        
                        lblCountMessage.Text = "Total number of messages :" + dataGrid1.Rows.Count;
                        if (dataGrid1.Rows.Count == 0)
                        {
                            cmdDelete.Enabled = false;
                            button2.Enabled = false;

                            btnSaveData.Enabled = false;
                            cmdSave.Enabled = false;
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

        private void frmIBPSImpFile_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void frmIBPSImpFile_KeyDown(object sender, KeyEventArgs e)
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

        private void frmIBPSImpFile_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
        }

        private void dataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            Common.bTimer = 1;
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

        
	}  
}
