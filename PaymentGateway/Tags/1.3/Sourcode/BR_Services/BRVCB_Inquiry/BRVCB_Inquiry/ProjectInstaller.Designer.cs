namespace BRVCB_Inquiry
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BRserviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BRserviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BRserviceProcessInstaller
            // 
            this.BRserviceProcessInstaller.Password = null;
            this.BRserviceProcessInstaller.Username = null;
            // 
            // BRserviceInstaller
            // 
            this.BRserviceInstaller.ServiceName = "BRVCBInquiry";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BRserviceProcessInstaller,
            this.BRserviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BRserviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BRserviceInstaller;
    }
}