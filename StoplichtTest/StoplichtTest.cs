﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using StoplichtService;
using System.Data.SqlClient;
using System.Data.EntityClient;

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

        static void LogEvent(SystemStateChange change) 
        {
            Console.WriteLine(change.Description);
        }

        static void RunService()
        {
            SLService service = new SLService();
            service.AddSessionChangeEventHandler(LogEvent);

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
                // Specify the provider name, server and database.
                string providerName = "System.Data.SqlClient";
                string serverName = @"EDWIN-PC\SQLEXPRESS";
                string databaseName = "SLService";

                // Initialize the connection string builder for the
                // underlying provider.
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

                // Set the properties for the data source.
                sqlBuilder.DataSource = serverName;
                sqlBuilder.InitialCatalog = databaseName;
                sqlBuilder.IntegratedSecurity = true;

                // Build the SqlConnection connection string.
                string providerString = sqlBuilder.ToString();

                // Initialize the EntityConnectionStringBuilder.
                EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

                //Set the provider name.
                entityBuilder.Provider = providerName;

                // Set the provider-specific connection string.
                entityBuilder.ProviderConnectionString = providerString;

                // Set the Metadata location.
                entityBuilder.Metadata = "res://DataLayer/Model.csdl|res://DataLayer/Model.ssdl|res://DataLayer/Model.msl";

                Console.WriteLine(entityBuilder.ToString());

                using (EntityConnection conn =
                new EntityConnection(entityBuilder.ToString()))
                {
                    conn.Open();
                    DataLayer.EventLog log = new DataLayer.EventLog();
                    log.Date = DateTime.Now;
                    log.Message = "Testing connection";
                    log.UserName = string.Join(";", StoplichtService.SystemInfo.CurrentActiveUsers());
                    DataLayer.SLServiceEntities sl = new DataLayer.SLServiceEntities(conn);
                    sl.AddToEventLog(log);
                    sl.SaveChanges();
                    conn.Close();

                    Console.WriteLine("Added 'Testing connection' to EventLog table.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test failed: " + ex.Message);
            }
        }
    }
}
