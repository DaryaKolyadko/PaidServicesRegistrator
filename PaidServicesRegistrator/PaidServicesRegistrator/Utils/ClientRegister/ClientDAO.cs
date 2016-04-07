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

        private string ADD_NEW_USER = "INSERT INTO service_client (fk_id_service, finish_time, client_token) " +
             "VALUES (?id, ?time, ?token)";

        private string DELETE_USER = "DELETE FROM service_client WHERE client_token=?token";

        private string FIND_USER_TOKEN_INFO = "SELECT finish_time FROM service_client WHERE fk_id_service=?serviceId " +
                                              "AND client_token=?clientToken";

        public string RegisterClient(string serviceName, TokenUtil.TokenType tokenType) 
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            ServiceDAO serviceDao = new ServiceDAO();
            string finishTime = TokenUtil.GenerateUserTokenLifeTimeString(tokenType);
            string token = TokenUtil.GenerateUserToken(serviceName);

            cmd.CommandText = ADD_NEW_USER;
            cmd.Parameters.AddWithValue("?id", serviceDao.GetIdByName(serviceName));
            cmd.Parameters.AddWithValue("?time", AesCryptUtil.Encrypt(finishTime));
            cmd.Parameters.AddWithValue("?token", AesCryptUtil.Encrypt(token));

            connection.Open();
            cmd.ExecuteNonQuery();

            connector.CloseConnection();

            return token;
        }

        public string FindClientToken(int serviceId, String userToken)
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();
          
            cmd.CommandText = FIND_USER_TOKEN_INFO;
            cmd.Parameters.AddWithValue("?serviceId", serviceId);
            cmd.Parameters.AddWithValue("?clientToken", AesCryptUtil.Encrypt(userToken));

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<String> finishTimeStr = new List<String>();

            while (reader.Read())
            {
                finishTimeStr.Add(reader.GetString(0));
            }

            connection.Close();

            if (finishTimeStr.Count > 0)
                return AesCryptUtil.Decrypt(finishTimeStr[0]);

            return null;
        }

        public void DeleteClient(string userToken)
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();
           
            cmd.CommandText = DELETE_USER;
            cmd.Parameters.AddWithValue("?token", AesCryptUtil.Encrypt(userToken));

            connection.Open();
            cmd.ExecuteNonQuery();

            connector.CloseConnection();
        }
    }
}