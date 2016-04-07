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
        private string uid { get; set; }
        private string pass { get; set; }

        private string ADD_NEW_SERVICE = "INSERT INTO service (wsdl, token, name) VALUES (?wsdl, ?token, ?name)";
        private string GET_ALL_SERVICE_NAMES = "SELECT name FROM service";
        private string GET_NAME_BY_ID = "SELECT name FROM service WHERE id_service = @id";

        public ServiceDAO(string uid, string pass)
        {
            this.uid = uid;
            this.pass = pass;
        }

        public string RegisterService(XDocument wsdl)
        {
            connector = new MySqlConnector(uid, pass);
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
            cmd.Parameters.Add("?wsdl", blobData);
            cmd.Parameters.Add("?token", token);
            cmd.Parameters.Add("?name", name);

            connection.Open();
            cmd.ExecuteNonQuery();
            
            connector.CloseConnection();
            return token;
        }

        public List<string> GetServiceNames()
        {
            connector = new MySqlConnector(uid, pass);
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = GET_ALL_SERVICE_NAMES;

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> names = new List<string>();

            while (reader.Read())
            {
                names.Add(reader[0].ToString());
            }

            connection.Close();

            return names;
        }

        public string GetNameById(int serviceId)
        {
            connector = new MySqlConnector(uid, pass);
            MySqlConnection connection = connector.GetConnection();
            MySqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = GET_NAME_BY_ID;
            cmd.Parameters.Add("@id", serviceId);

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> names = new List<string>();

            while (reader.Read())
            {
                names.Add(reader[0].ToString());
            }

            connection.Close();
            return names[0];
        }
    }
}