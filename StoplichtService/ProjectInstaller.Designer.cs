namespace StoplichtService
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
            this.SLServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SLServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SLServiceProcessInstaller
            // 
            this.SLServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SLServiceProcessInstaller.Password = null;
            this.SLServiceProcessInstaller.Username = null;
            // 
            // SLServiceInstaller
            // 
            this.SLServiceInstaller.ServiceName = "SLService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SLServiceProcessInstaller,
            this.SLServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SLServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SLServiceInstaller;
    }
}