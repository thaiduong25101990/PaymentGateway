namespace BRVCBExport
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
            this.GWServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BRServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // GWServiceProcessInstaller
            // 
            this.GWServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.GWServiceProcessInstaller.Password = null;
            this.GWServiceProcessInstaller.Username = null;
            // 
            // BRServiceInstaller
            // 
            this.BRServiceInstaller.Description = "FPT BRIDGE VCB Export Service";
            this.BRServiceInstaller.DisplayName = "BR VCB Export";
            this.BRServiceInstaller.ServiceName = "BRVCBExport";
            this.BRServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.GWServiceProcessInstaller,
            this.BRServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller GWServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BRServiceInstaller;
    }
}