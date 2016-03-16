using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BR.BRLib;

namespace BR.BRSYSTEM
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PleaseWait : System.Windows.Forms.Form
	{
        private BR.BRSYSTEM.OSProgress osProgress1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private Timer timer1;
        private IContainer components;

		public PleaseWait()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PleaseWait));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.osProgress1 = new BR.BRSYSTEM.OSProgress();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(366, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(5, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(52, 49);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(144, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please wait ... working";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // osProgress1
            // 
            this.osProgress1.AutoProgress = true;
            this.osProgress1.AutoProgressSpeed = 225;
            this.osProgress1.BackColor = System.Drawing.Color.Transparent;
            this.osProgress1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.osProgress1.IndicatorColor = System.Drawing.Color.White;
            this.osProgress1.Location = new System.Drawing.Point(51, 21);
            this.osProgress1.Name = "osProgress1";
            this.osProgress1.NormalImage = ((System.Drawing.Image)(resources.GetObject("osProgress1.NormalImage")));
            this.osProgress1.PointImage = ((System.Drawing.Image)(resources.GetObject("osProgress1.PointImage")));
            this.osProgress1.Position = 1;
            this.osProgress1.ProgressBoxStyle = BR.BRSYSTEM.OSProgress.OSProgressBoxStyleConstants.osSOLIDBIGGER;
            this.osProgress1.ProgressType = BR.BRSYSTEM.OSProgress.OSProgressTypeConstants.osGRAPHICTYPE;
            this.osProgress1.Size = new System.Drawing.Size(315, 16);
            this.osProgress1.TabIndex = 0;
            // 
            // PleaseWait
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(409, 58);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.osProgress1);
            this.ForeColor = System.Drawing.Color.Green;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PleaseWait";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exp";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {                
                this.timer1.Enabled = true;
                this.timer1.Interval = 2000;
                this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }        

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Common.Export_excel == 1)
                {                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>		
	}
}
