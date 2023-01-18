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
            this.watchDogProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.watchDogServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            this.watchDogProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.watchDogProcessInstaller.Password = null;
            this.watchDogProcessInstaller.Username = null;
            // 
            // serviceInstaller2
            this.watchDogServiceInstaller.Description = "WatchDog Service";
            this.watchDogServiceInstaller.DisplayName = "WatchDog";
            this.watchDogServiceInstaller.ServiceName = "WatchDogWindowsService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.watchDogProcessInstaller,
            this.watchDogServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller watchDogProcessInstaller;
        private System.ServiceProcess.ServiceInstaller watchDogServiceInstaller;
    }
}