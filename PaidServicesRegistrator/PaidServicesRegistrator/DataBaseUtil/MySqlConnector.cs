using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PaidServicesRegistrator.DataBaseUtil
{
    public class MySqlConnector
    {
        private string connectionString;
        private MySqlConnection connection;
        
        public MySqlConnector()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"]
                .ConnectionString;
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return connection;
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}