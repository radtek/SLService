using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace StoplichtService
{
    public partial class SLService : ServiceBase
    {
        private Action<SystemStateChange> ChangeHandlers;

        public SLService()
        {
            InitializeComponent();
            this.CanHandleSessionChangeEvent = true;
        }

        protected override void OnStart(string[] args)
        {
            this.Start();
        }

        public void Start()
        {
            /* todo !! */
        }

        protected override void OnStop()
        {
            /* todo ?? */
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogon:
                    Debug.WriteLine(changeDescription.SessionId + " logon");
                    break;
                case SessionChangeReason.SessionLogoff:
                    Debug.WriteLine(changeDescription.SessionId + " logoff");
                    break;
                case SessionChangeReason.SessionLock:
                    Debug.WriteLine(changeDescription.SessionId + " lock");

                    // Write the string to a file.
                    System.IO.StreamWriter file1 = System.IO.File.AppendText("c:\\Temp\\test.txt");
                    file1.WriteLine("lock");
                    file1.Close();

                    this.ChangeHandlers.Invoke(new SystemStateChange() { Description = "lock" });
                    break;
                case SessionChangeReason.SessionUnlock:
                    Debug.WriteLine(changeDescription.SessionId + " unlock");

                    // Write the string to a file.
                    System.IO.StreamWriter file2 = System.IO.File.AppendText("c:\\Temp\\test.txt");
                    file2.WriteLine("unlock");
                    file2.Close();
                    
                    this.ChangeHandlers.Invoke(new SystemStateChange() { Description = "unlock" });
                    break;
            }

            base.OnSessionChange(changeDescription);
        }

        public void AddSessionChangeEventHandler(Action<SystemStateChange> ChangeHandler)
        {
            this.ChangeHandlers += ChangeHandler;
        }
    }
}
