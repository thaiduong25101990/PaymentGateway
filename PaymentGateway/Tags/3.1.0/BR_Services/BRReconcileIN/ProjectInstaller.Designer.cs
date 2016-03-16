namespace BRReconcileIN
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
            this.BRserviceProcess = new System.ServiceProcess.ServiceProcessInstaller();
            this.BRServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BRserviceProcess
            // 
            this.BRserviceProcess.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BRserviceProcess.Password = null;
            this.BRserviceProcess.Username = null;
            // 
            // BRServiceInstaller
            // 
            this.BRServiceInstaller.Description = "FPT BRIDGE Reconcile service";
            this.BRServiceInstaller.DisplayName = "BRReconcileIN";
            this.BRServiceInstaller.ServiceName = "BRReconcileIN";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BRserviceProcess,
            this.BRServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BRserviceProcess;
        private System.ServiceProcess.ServiceInstaller BRServiceInstaller;
    }
}