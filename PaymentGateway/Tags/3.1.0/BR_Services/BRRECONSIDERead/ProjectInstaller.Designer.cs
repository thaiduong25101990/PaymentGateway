namespace BRRECONSIDERead
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
            this.BRReconsideSIBS = new System.ServiceProcess.ServiceProcessInstaller();
            this.BRReconsideInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BRReconsideSIBS
            // 
            this.BRReconsideSIBS.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BRReconsideSIBS.Password = null;
            this.BRReconsideSIBS.Username = null;
            // 
            // BRReconsideInstaller
            // 
            this.BRReconsideInstaller.Description = "FPT BRIDGE Reconcile service";
            this.BRReconsideInstaller.ServiceName = "BRReconside";
            this.BRReconsideInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.BRReconsideInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.GWReconsideInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BRReconsideSIBS,
            this.BRReconsideInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BRReconsideSIBS;
        private System.ServiceProcess.ServiceInstaller BRReconsideInstaller;
    }
}