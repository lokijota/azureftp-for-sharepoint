namespace azureftp_for_sharepoint.server
{
    partial class ProjectInstaller
    {
        private System.ServiceProcess.ServiceInstaller _azureFtpForSharePointServer;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;

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
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this._azureFtpForSharePointServer = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // AzureFtpForSharePointServer
            // 
            this._azureFtpForSharePointServer.DisplayName = "Azure-Ftp-For-SharePoint Server";
            this._azureFtpForSharePointServer.ServiceName = "AzureFtpForSharePointServer";
            this._azureFtpForSharePointServer.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this._azureFtpForSharePointServer.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this._azureFtpForSharePointServer});

        }

        #endregion
    }
}