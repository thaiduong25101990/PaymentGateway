﻿namespace GWIBPSExport
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
            this.BRServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BRServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BRServiceProcessInstaller
            // 
            this.BRServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BRServiceProcessInstaller.Password = null;
            this.BRServiceProcessInstaller.Username = null;
            this.BRServiceProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.BRServiceProcessInstaller_AfterInstall);
            // 
            // BRServiceInstaller
            // 
            this.BRServiceInstaller.Description = "FTP BRIDGE IBPS Export Service";
            this.BRServiceInstaller.DisplayName = "BRIBPSExport";
            this.BRServiceInstaller.ServiceName = "BRIBPSExport";
            this.BRServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BRServiceProcessInstaller,
            this.BRServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BRServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BRServiceInstaller;
    }
}