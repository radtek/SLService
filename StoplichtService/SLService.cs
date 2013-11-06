using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using DataAccessLayer;

namespace StoplichtService
{
    public partial class SLService : ServiceBase
    {
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
            /* todo ?? */
        }

        protected override void OnStop()
        {
            /* todo ?? */
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            DataAccessLayer.EventLog log = new DataAccessLayer.EventLog();
            log.UserSession = changeDescription.SessionId;
            log.UserName = string.Join(";",SystemInfo.GetCurrentActiveUsers());
            log.HostName = SystemInfo.GetHostName();
            log.Date = DateTime.Now;

            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogon:
                    // Write the string to a file.
                    //System.IO.StreamWriter file1 = System.IO.File.AppendText("c:\\Temp\\test.txt");
                    //file1.WriteLine("unlock");
                    //file1.Close();
                    log.Message = "logon";
                    break;
                case SessionChangeReason.SessionLogoff:
                    log.Message = "logoff";
                    break;
                case SessionChangeReason.SessionLock:
                    log.Message = "lock";
                    break;
                case SessionChangeReason.SessionUnlock:
                    log.Message = "unlock";                    
                    break;
                default:
                    var msg = changeDescription.Reason;
                    log.Message = Enum.GetName(msg.GetType(), msg);
                    break;
            }

            Helper.WriteEvent(log);

            base.OnSessionChange(changeDescription);
        }
    }
}
