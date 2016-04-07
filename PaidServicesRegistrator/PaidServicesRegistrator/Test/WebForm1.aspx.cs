using MySql.Data.MySqlClient;
using PaidServicesRegistrator.DataBaseUtil;
using PaidServicesRegistrator.Utils;
using PaidServicesRegistrator.Utils.ClientRegister;
using PaidServicesRegistrator.Utils.ServiceRegister;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace PaidServicesRegistrator.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ClientDAO d = new ClientDAO("root", "318114275");
                //String tok = d.RegisterClient(2, TokenUtil.TokenType.OneOff);


                //XDocument doc = XDocument.Load("D:\\Programming\\6sem\\soa\\PaidServicesRegistrator\\PaidServicesRegistrator\\PaidServicesRegistrator\\Test\\ClubService.wsdl");
                //ServiceDAO dao = new ServiceDAO("root", "318114275");
                //dao.RegisterService(doc);
                //List<string> names = dao.GetServiceNames();

                //MySqlConnector mysqlConnector = new MySqlConnector("root", "darya");
                //MySqlConnection connection = mysqlConnector.GetConnection();

                //string cmd = "SELECT * FROM service;";
                //MySqlCommand command = new MySqlCommand(cmd, connection);
                //MySqlDataReader reader = command.ExecuteReader();

                //DataTable dt = new DataTable();
                //dt.Columns.Add("id_serv");
                //dt.Columns.Add("wsdl");
                //dt.Columns.Add("token");

                //while (reader.Read())
                //{
                //    DataRow newRow = dt.NewRow();
                //    newRow["id_serv"] = reader[0].ToString();
                //    newRow["wsdl"] = System.Text.Encoding.Default.GetString((byte[])reader[1]);
                //    newRow["token"] = reader[2].ToString();
                //    dt.Rows.Add(newRow);
                //}

                //GridView1.DataSource = dt;
                //GridView1.DataBind();
            }
        }
    }
}