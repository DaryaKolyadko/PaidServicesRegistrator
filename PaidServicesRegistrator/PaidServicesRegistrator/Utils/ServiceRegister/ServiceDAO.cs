using MySql.Data.MySqlClient;
using PaidServicesRegistrator.DataBaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PaidServicesRegistrator.Utils.ServiceRegister
{
    public class ServiceDAO
    {
        private MySqlConnector connector;

        private string ADD_NEW_SERVICE = "INSERT INTO service (wsdl, token, name) VALUES (?wsdl, ?token, ?name)";
        private string GET_ALL_SERVICE_NAMES = "SELECT name FROM service";
        private string GET_NAME_BY_ID = "SELECT name FROM service WHERE id_service = @id";
        private string GET_ID_BY_NAME = "SELECT id_service FROM service WHERE name = @name";
        private string GET_ID_BY_TOKEN = "SELECT id_service FROM service WHERE token = @token";
        public string RegisterService(XDocument wsdl)
        {
            connector = new MySqlConnector();
            string name = WSDLParser.getNameFromWSDL(wsdl);

            byte[] blobData;
            using (MemoryStream ms = new MemoryStream()) 
            {
                wsdl.Save(ms);
                blobData = ms.ToArray();
            }

            string token = TokenUtil.GenerateServiceToken(name);

            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = ADD_NEW_SERVICE;
            cmd.Parameters.AddWithValue("?wsdl", blobData);
            cmd.Parameters.AddWithValue("?token", AesCryptUtil.Encrypt(token));
            cmd.Parameters.AddWithValue("?name", name);

            connection.Open();
            cmd.ExecuteNonQuery();
            
            connector.CloseConnection();
            return token;
        }

        public List<string> GetServiceNames()
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = GET_ALL_SERVICE_NAMES;

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> names = new List<string>();

            while (reader.Read())
            {
                names.Add(reader.GetString(0));
            }

            connection.Close();

            return names;
        }

        public string GetNameById(int serviceId)
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = GET_NAME_BY_ID;
            cmd.Parameters.AddWithValue("@id", serviceId);

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> names = new List<string>();

            while (reader.Read())
            {
                names.Add(reader.GetString(0));
            }

            connection.Close();

            if (names.Count > 0)
                return names[0];

            return null;
        }

        public int GetIdByName(string serviceName)
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = GET_ID_BY_NAME;
            cmd.Parameters.AddWithValue("@name", serviceName);

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<int> id = new List<int>();

            while (reader.Read())
            {
                id.Add(reader.GetInt32(0));
            }

            connection.Close();

            if(id.Count > 0)
                return id[0];

            return -1;
        }

        public int GetIdByToken(string token)
        {
            connector = new MySqlConnector();
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = GET_ID_BY_TOKEN;
            cmd.Parameters.AddWithValue("@token", AesCryptUtil.Encrypt(token));

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<int> id = new List<int>();

            while (reader.Read())
            {
                id.Add(reader.GetInt32(0));
            }

            connection.Close();

            if (id.Count > 0)
                return id[0];

            return -1;
        }
    }
}