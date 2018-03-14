namespace QualisysServiceAudit
{
    partial class QualisysAuditServiceInstaller
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
            this.QualisysAuditProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.QualisysAuditInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // QualisysAuditProcessInstaller
            // 
            this.QualisysAuditProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.QualisysAuditProcessInstaller.Password = null;
            this.QualisysAuditProcessInstaller.Username = null;
            // 
            // QualisysAuditInstaller
            // 
            this.QualisysAuditInstaller.Description = "QualisysAuditService";
            this.QualisysAuditInstaller.DisplayName = "QualisysAuditService";
            this.QualisysAuditInstaller.ServiceName = "QualisysAuditService";
            this.QualisysAuditInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // QualisysAuditServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.QualisysAuditProcessInstaller,
            this.QualisysAuditInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller QualisysAuditProcessInstaller;
        private System.ServiceProcess.ServiceInstaller QualisysAuditInstaller;
    }
}