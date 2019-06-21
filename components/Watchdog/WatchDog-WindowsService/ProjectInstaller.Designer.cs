namespace WatchDog_WindowsService
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
            this.serviceProcessInstaller6 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller6 = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            this.serviceProcessInstaller6.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller6.Password = null;
            this.serviceProcessInstaller6.Username = null;
            // 
            // serviceInstaller2
            this.serviceInstaller6.Description = "WatchDog Service";
            this.serviceInstaller6.DisplayName = "WatchDog";
            this.serviceInstaller6.ServiceName = "Service6";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller6,
            this.serviceInstaller6});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller6;
        private System.ServiceProcess.ServiceInstaller serviceInstaller6;
    }
}