using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace DataAccessLayer
{
    public class Helper
    {
        public static EntityConnection GetConnection()
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
            entityBuilder.Metadata = "res://DataAccessLayer/Model.csdl|res://DataAccessLayer/Model.ssdl|res://DataAccessLayer/Model.msl";

            Console.WriteLine(entityBuilder.ToString());

            return new EntityConnection(entityBuilder.ToString());
        }

        public static void WriteEvent(DataAccessLayer.EventLog log)
        {
            using (EntityConnection conn = GetConnection()) 
            {
                conn.Open();
                SLServiceEntities sl = new SLServiceEntities(conn);
                sl.AddToEventLog(log);
                sl.SaveChanges();
                conn.Close();
            }
        }
    }
}
