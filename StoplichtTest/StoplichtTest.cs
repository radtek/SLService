using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using StoplichtService;
using System.Data.SqlClient;
using System.Data.EntityClient;
using DataAccessLayer;

namespace StoplichtTest
{
    class StoplichtTest
    {
        static void Main(string[] args)
        {
            bool argCheck = true;
            if (args.Count() >= 1)
            {
                switch (args[0])
                {
                    case "connection":
                        TestConnection();
                        break;
                    case "service":
                        RunService();
                        break;
                    default:
                        argCheck = false;
                        break;
                }
            }
            else
            {
                argCheck = false;
            }

            if (!argCheck)
            {
                Console.WriteLine("StoplichtTest [connection|service]");
                return;
            }
        }

        static void RunService()
        {
            SLService service = new SLService();

            if (Environment.UserInteractive)
            {
                service.Start();
                Console.WriteLine("Press any key to stop program");
                Console.Read();
                service.Stop();
            }
            else
            {
                ServiceBase.Run(service);
            }
        }

        static void TestConnection()
        {
            try
            {
                EventLog log = new EventLog();
                log.Date = DateTime.Now;
                log.Message = "Testing connection";
                log.UserName = string.Join(";", StoplichtService.SystemInfo.GetCurrentActiveUsers());
                Helper.WriteEvent(log);
                Console.WriteLine("Added 'Testing connection' to EventLog table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test failed: " + ex.Message);
            }
        }
    }
}
