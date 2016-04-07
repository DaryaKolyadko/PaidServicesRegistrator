using MySql.Data.MySqlClient;
using PaidServicesRegistrator.DataBaseUtil;
using PaidServicesRegistrator.Utils.ServiceRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaidServicesRegistrator.Utils.ClientRegister
{
    public class ClientDAO
    {
        private MySqlConnector connector;
        private string uid { get; set; }
        private string pass { get; set; }

        private string ADD_NEW_USER = "INSERT INTO service_client (fk_id_service, finish_time, client_token) " +
             "VALUES (?id, ?time, ?token)";

        public ClientDAO(string uid, string pass)
        {
            this.uid = uid;
            this.pass = pass;
        }

        public string RegisterClient(int serviceId, TokenUtil.TokenType tokenType) 
        {
            connector = new MySqlConnector(uid, pass);
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            ServiceDAO serviceDao = new ServiceDAO(uid, pass);
            string finishTime = TokenUtil.GenerateUserTokenLifeTimeString(tokenType);
            string token = TokenUtil.GenerateUserToken(serviceDao.GetNameById(serviceId));

            cmd.CommandText = ADD_NEW_USER;
            cmd.Parameters.Add("?id", serviceId);
            cmd.Parameters.Add("?time", finishTime);
            cmd.Parameters.Add("?token", token);

            connection.Open();
            cmd.ExecuteNonQuery();

            connector.CloseConnection();

            return token;
        }
    }
}