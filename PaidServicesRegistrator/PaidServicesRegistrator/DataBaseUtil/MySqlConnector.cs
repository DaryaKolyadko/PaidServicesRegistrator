using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PaidServicesRegistrator.DataBaseUtil
{
    public class MySqlConnector
    {
        private string server = "localhost";
        private string database = "paid_service_register";
        private string uid = "";
        private string password = "";
        private string connectionString;
        private MySqlConnection connection;
        
        public MySqlConnector(string uid, string password)
        {
            this.uid = uid;
            this.password = password;
            this.connectionString = "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + uid + ";" +
                "PASSWORD=" + password + ";";
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